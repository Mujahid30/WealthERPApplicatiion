using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.FSharp;
using System.Numeric;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using DaoValuation;

namespace BoValuation
{
    public class MFInstantValuationBo
    {
        MFEngineDao mfEngineDao = new MFEngineDao();
        MFEngineBo mfEngineBo = new MFEngineBo();
       
        bool isMFTractionSellPairRecreate = false;

        public void ProcessMFAccountScheme(int accountId, int schemePlanCode, DateTime valuationDate)
        {
            DataTable dtMFInstantNetPosition = new DataTable();
            try
            {
                dtMFInstantNetPosition = MFInstantValuation(accountId, schemePlanCode, valuationDate);
                //dsMFBalancedSellPairedForNetPosition.Tables[0].TableName="TransactionBalance";
                //dsMFBalancedSellPairedForNetPosition.Tables[1].TableName="TransactionSellPair";
                //dsMFBalancedSellPairedForNetPosition.Tables[2].TableName = "MutualFundNetPosition";
                mfEngineDao.CreateCustomerMFNetPosition(0, valuationDate, dtMFInstantNetPosition);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFInstantValuationBo.cs:ProcessMFAccountScheme()");

                object[] objects = new object[3];
                objects[0] = accountId;
                objects[1] = schemePlanCode;
                objects[2] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected DataTable MFInstantValuation(int accountId, int schemePlanCode,DateTime valuationDate)
        {
            DataTable dtInstantNetPosition = new DataTable();
            DataSet dsMFBalancedSellPairedForNetPosition = new DataSet();
            try
            {              
                    
                            DateTime dtMinDateTransToBeProcess = new DateTime();
                            DateTime dtMaxDateTransProcessed = new DateTime();
                            DataSet dsTransactionBalanceReadyToProcess = new DataSet();
                            DataTable dtMFTransactionBalance = new DataTable();
                            DataTable dtMFTransactionProcessedBalance = new DataTable();
                            DataTable dtMFTransactionProcessedBalanceTemp = new DataTable();
                            DataTable dtMFTransactionsToProcess = new DataTable();
                            DataSet dsMFBalanceSellpaired = new DataSet();                           
                            DataTable dtMFNetPositionFinalDataTable = new DataTable();
                            DataTable dtMFTransactionBalanceSellPaired = new DataTable();

                            dtMFTransactionProcessedBalance = mfEngineBo.CreateTransactionBalanceTable();
                            dtMFTransactionBalance = mfEngineBo.CreateTransactionBalanceTable();

                            DataSet dsMFTransactionBalance = mfEngineDao.GetMFTransactionsForBalanceCreation(accountId, schemePlanCode);
                            dtMFTransactionsToProcess = dsMFTransactionBalance.Tables[0];
                            dtMFTransactionBalance = dsMFTransactionBalance.Tables[1];
                            dtMFTransactionBalanceSellPaired=dsMFTransactionBalance.Tables[2];
                           
                            if (dtMFTransactionsToProcess != null)
                            {
                                if (dtMFTransactionsToProcess.Rows.Count > 0)
                                {

                                    DataView dvMFTransactionsProcessed = new DataView(dtMFTransactionsToProcess, "CMFT_IsValued='1'", "CMFT_TransactionDate", DataViewRowState.CurrentRows);
                                    DataView dvMFTransactionsToBeProcess = new DataView(dtMFTransactionsToProcess, "CMFT_IsValued='0'", "CMFT_TransactionDate", DataViewRowState.CurrentRows);
                                   

                                    if (dvMFTransactionsToBeProcess.ToTable().Rows.Count > 0)
                                    {
                                        dtMinDateTransToBeProcess = Convert.ToDateTime((dvMFTransactionsToBeProcess.ToTable().Compute("Min(CMFT_TransactionDate)", string.Empty)));
                                    }
                                    if (dvMFTransactionsProcessed.ToTable().Rows.Count > 0)
                                    {
                                        dtMaxDateTransProcessed = Convert.ToDateTime((dvMFTransactionsProcessed.ToTable().Compute("Max(CMFT_TransactionDate)", string.Empty)));
                                    }
                                    

                                    if (dtMinDateTransToBeProcess != DateTime.MinValue && dtMaxDateTransProcessed != DateTime.MinValue && (dtMinDateTransToBeProcess < dtMaxDateTransProcessed))
                                    {
                                        isMFTractionSellPairRecreate = true;
                                        dsTransactionBalanceReadyToProcess.Tables.Add(dtMFTransactionsToProcess.DefaultView.ToTable());

                                        if (dtMFTransactionBalance.TableName != "")
                                        {
                                            DataColumn dcInsertUpdate = new DataColumn("CMFTB_InsertUpdate_Flag");
                                            dcInsertUpdate.DataType = typeof(int);
                                            dcInsertUpdate.DefaultValue = 3; //3 is used to delete the balanced record from TransactionBalanced Table

                                            dtMFTransactionBalance.Columns.Remove("CMFTB_InsertUpdate_Flag");
                                            dtMFTransactionBalance.Columns.Add(dcInsertUpdate);
                                        }



                                    }
                                    else
                                    {
                                        isMFTractionSellPairRecreate = false;
                                        dsTransactionBalanceReadyToProcess.Tables.Add(dvMFTransactionsToBeProcess.ToTable());
                                        dsTransactionBalanceReadyToProcess.Tables.Add(dtMFTransactionBalance.DefaultView.ToTable());
                                        dsTransactionBalanceReadyToProcess.Tables[1].TableName = "Balance";
                                    }

                                    dsTransactionBalanceReadyToProcess.Tables[0].TableName = "Transaction";

                                    //if (commonId == 227617)
                                    //{

                                    //}

                                    if (dsTransactionBalanceReadyToProcess.Tables["Transaction"].Rows.Count > 0)
                                        dsMFBalanceSellpaired = mfEngineBo.TransactionBalanceProcess(dsTransactionBalanceReadyToProcess);
                                    else
                                    {
                                        dsMFBalanceSellpaired.Tables.Add(dtMFTransactionBalance.DefaultView.ToTable());
                                        dsMFBalanceSellpaired.Tables.Add(dtMFTransactionBalanceSellPaired.DefaultView.ToTable());
                                    }
                                    
                                    dsMFBalancedSellPairedForNetPosition = mfEngineBo.CreateMFNetPositionDataTable(dsMFBalanceSellpaired, valuationDate, schemePlanCode);
                                    dtMFNetPositionFinalDataTable = mfEngineBo.CreateMFNetPositionDataTable(dsMFBalancedSellPairedForNetPosition, valuationDate);

                                    //dsMFBalancedSellPairedForNetPosition.Tables.Add(dsMFBalanceSellpaired.Tables[0].DefaultView.ToTable());
                                    //dsMFBalancedSellPairedForNetPosition.Tables.Add(dsMFBalanceSellpaired.Tables[1].DefaultView.ToTable());
                                    dtInstantNetPosition.Merge(dtMFNetPositionFinalDataTable);
                                    
                                }

                            }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFEngineBo.cs:MFBalanceCreation()");


                object[] objects = new object[3];
                objects[0] = accountId;
                objects[1] = schemePlanCode;               
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dtInstantNetPosition;

        }

    }
}
