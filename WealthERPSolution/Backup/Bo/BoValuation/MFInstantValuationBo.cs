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
using BoCommon;

namespace BoValuation
{
    public class MFInstantValuationBo
    {
        MFEngineDao mfEngineDao = new MFEngineDao();
        MFEngineBo mfEngineBo = new MFEngineBo();
        EmailSMSBo emailSMSBo = new EmailSMSBo(); 


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
                //mfEngineDao.MarkForDeleteMFBalanceData(accountId, schemePlanCode);

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

        protected DataTable MFInstantValuation(int accountId, int schemePlanCode, DateTime valuationDate)
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
                

                if (dtMFTransactionsToProcess != null)
                {

                    dsTransactionBalanceReadyToProcess.Tables.Add(dsMFTransactionBalance.Tables[0].DefaultView.ToTable());
                    isMFTractionSellPairRecreate = false;
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
            catch (BaseApplicationException Ex)
            {
                emailSMSBo.SendErrorExceptionMail(accountId, "AccountId", schemePlanCode, Ex.Message, "MFInstantValuationBo.Cs_MFInstantValuation");

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
