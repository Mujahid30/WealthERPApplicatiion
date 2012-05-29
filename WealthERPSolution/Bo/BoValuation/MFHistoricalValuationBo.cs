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
using BoValuation;

namespace BoValuation
{
  public class MFHistoricalValuationBo
    {
      MFEngineDao mfEngineDao = new MFEngineDao();
      MFEngineBo mfEngineBo = new MFEngineBo();
      List<int> AdviserCustomers = new List<int>();

      DataSet dsCustomerTransactionsDetails = new DataSet();

      DataTable dtCustomerPortfolio = new DataTable();
      DataTable dtCustomerAccount = new DataTable();
      DataTable dtCustomerTransactionsToProcess = new DataTable();
      DataTable dtCustomerTransactionBalance = new DataTable();

      DataTable dtMFAccount = new DataTable();
      DataTable dtMFTransactionsToProcess = new DataTable();
      DataTable dtMFTransactionBalance = new DataTable();

      //DataTable dtSellPaired = new DataTable();
      DataTable dtMFTrasactionSellPair = new DataTable();
      DataTable dtCustomerMFTransactionSellPaired = new DataTable();
      string expression = "CMFTB_UnitBalanceRETURN" + ">0";
      DataTable dtFinalCustomerMFTransactionBalance = new DataTable();
      DataTable dtCustomerMutualFundNetPosition = new DataTable();
      bool isMFTractionSellPairRecreate;
      DataTable dtMFAccountSchemeNetPosition = new DataTable();


      public enum ValuationLabel
      {
          Advisor,
          Customer,
          Portfolio,
          AccountScheme,
      };
      #region NetPositionCreation

      public void MFNetPositionCreation(int commonId, int schemePlanCode, ValuationLabel startFrom, DateTime valuationDate)
      {
          try
          {
              switch (startFrom.ToString())
              {

                  case "Advisor":
                      {
                          AdviserCustomers = mfEngineDao.GetAdviserCustomerList_MF(commonId);
                          if (AdviserCustomers != null)
                          {
                              foreach (int customerId in AdviserCustomers)
                              {
                                  //if (customerId == 85747)
                                  //{

                                  //}
                                  MFNetPositionCreation(customerId, 0, ValuationLabel.Customer, valuationDate);
                                  if (dtCustomerMutualFundNetPosition.Rows.Count > 0)
                                  mfEngineDao.CreateCustomerMFNetPosition(customerId, valuationDate, dtCustomerMutualFundNetPosition);
                                  dtCustomerMutualFundNetPosition.Clear();

                              }
                          }
                          break;
                      }
                  case "Customer":
                      {
                          dsCustomerTransactionsDetails = mfEngineDao.GetCustomerTransactionsForBalanceCreation(commonId);
                          dtCustomerPortfolio = dsCustomerTransactionsDetails.Tables[0];
                          dtCustomerAccount = dsCustomerTransactionsDetails.Tables[1];
                          dtCustomerTransactionsToProcess = dsCustomerTransactionsDetails.Tables[2];
                          dtCustomerTransactionBalance = mfEngineBo.CreateTransactionBalanceTable();
                          dtCustomerTransactionBalance = dsCustomerTransactionsDetails.Tables[3];
                          dtCustomerMFTransactionSellPaired = mfEngineBo.CreateSellPairedTable();
                          dtFinalCustomerMFTransactionBalance = mfEngineBo.CreateTransactionBalanceTable();
                          if (dtCustomerPortfolio.Rows.Count > 0)
                          {
                              foreach (DataRow drProftfolio in dtCustomerPortfolio.Rows)
                              {
                                  MFNetPositionCreation(Convert.ToInt32(drProftfolio["CP_PortfolioId"].ToString()), 0, ValuationLabel.Portfolio,valuationDate);

                              }

                          }                         

                          break;

                      }
                  case "Portfolio":
                      {
                          if (dtCustomerAccount != null)
                          {
                              if (dtCustomerAccount.Rows.Count > 0)
                              {
                                  dtCustomerAccount.DefaultView.RowFilter = "CP_PortfolioId=" + commonId.ToString();
                                  dtMFAccount = dtCustomerAccount.DefaultView.ToTable();
                                  foreach (DataRow drCustomerMFAccount in dtMFAccount.Rows)
                                  {
                                      MFNetPositionCreation(Convert.ToInt32(drCustomerMFAccount["CMFA_AccountId"].ToString()), Convert.ToInt32(drCustomerMFAccount["PASP_SchemePlanCode"].ToString()), ValuationLabel.AccountScheme,valuationDate);
                                  }
                              }

                          }
                          break;
                      }
                  case "AccountScheme":
                      {
                          DateTime dtMinDateTransToBeProcess = new DateTime();
                          DateTime dtMaxDateTransProcessed = new DateTime();
                          DataSet dsTransactionBalanceReadyToProcess = new DataSet();
                          DataTable dtMFTransactionBalance = new DataTable();
                          DataTable dtMFTransactionProcessedBalance = new DataTable();
                          DataSet dsMFTransactionProcessedBalance = new DataSet();
                          DataTable dtMFTransactionProcessedBalanceTemp = new DataTable();
                          DataTable dtMfNetPositionFinalDataTable = new DataTable();
                          DataSet dsMfBalancedSellPairedForNetPosition = new DataSet();

                          //dtMFTransactionProcessedBalance = mfEngineBo.CreateTransactionBalanceTable();
                          //dtMFTransactionBalance = mfEngineBo.CreateTransactionBalanceTable();
                          if (dtCustomerTransactionsToProcess != null)
                          {
                              if (dtCustomerTransactionsToProcess.Rows.Count > 0)
                              {

                                  dtCustomerTransactionsToProcess.DefaultView.RowFilter = "CMFA_AccountId=" + commonId.ToString() + " AND " + "PASP_SchemePlanCode=" + schemePlanCode.ToString();
                                  dtMFTransactionsToProcess = dtCustomerTransactionsToProcess.DefaultView.ToTable();
                                  DataView dvMFTransactionsProcessed = new DataView(dtMFTransactionsToProcess, "CMFT_IsValued='1'", "CMFT_TransactionDate", DataViewRowState.CurrentRows);
                                  DataView dvMFTransactionsToBeProcess = new DataView(dtMFTransactionsToProcess, "CMFT_IsValued='0'", "CMFT_TransactionDate", DataViewRowState.CurrentRows);
                                  //dvMFTransactionsProcessed.RowFilter = "CMFT_IsValued='1'"; 
                                  //dvMFTransactionsToBeProcess.RowFilter = "CMFT_IsValued='0'";
                                  if (dvMFTransactionsToBeProcess.ToTable().Rows.Count > 0)
                                  {
                                      dtMinDateTransToBeProcess = Convert.ToDateTime((dvMFTransactionsToBeProcess.ToTable().Compute("Min(CMFT_TransactionDate)", string.Empty)));
                                  }
                                  if (dvMFTransactionsProcessed.ToTable().Rows.Count > 0)
                                  {
                                      dtMaxDateTransProcessed = Convert.ToDateTime((dvMFTransactionsProcessed.ToTable().Compute("Max(CMFT_TransactionDate)", string.Empty)));
                                  }
                                  //dtMinDateForNotBalancedCreated=dtMFTransactionsToProcess.

                                  if (dtCustomerTransactionBalance != null)
                                  {
                                      if (dtCustomerTransactionBalance.Rows.Count > 0)
                                      {
                                          dtCustomerTransactionBalance.DefaultView.RowFilter = "CMFA_AccountId=" + commonId.ToString() + " AND " + "PASP_SchemePlanCode=" + schemePlanCode.ToString();

                                          dtMFTransactionBalance = dtCustomerTransactionBalance.DefaultView.ToTable();
                                      }
                                  }

                                  if (dtMinDateTransToBeProcess != DateTime.MinValue && dtMaxDateTransProcessed != DateTime.MinValue && (dtMinDateTransToBeProcess < dtMaxDateTransProcessed))
                                  {
                                      isMFTractionSellPairRecreate = true;
                                      dsTransactionBalanceReadyToProcess.Tables.Add(dtMFTransactionsToProcess);

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
                                      dsTransactionBalanceReadyToProcess.Tables.Add(dtMFTransactionBalance);
                                      dsTransactionBalanceReadyToProcess.Tables[1].TableName = "Balance";
                                  }

                                  dsTransactionBalanceReadyToProcess.Tables[0].TableName = "Transaction";

                                  //if (commonId == 227617)
                                  //{

                                  //}

                                  if (dsTransactionBalanceReadyToProcess.Tables["Transaction"].Rows.Count > 0)
                                      dsMFTransactionProcessedBalance =mfEngineBo.TransactionBalanceProcess(dsTransactionBalanceReadyToProcess);


                                  //For creating DataTable As per Stored Procedure
                                  dsMfBalancedSellPairedForNetPosition = mfEngineBo.CreateMFNetPositionDataTable(dsMFTransactionProcessedBalance, valuationDate, schemePlanCode);

                                  //For creating NetPosition details 
                                  dtMfNetPositionFinalDataTable = mfEngineBo.CreateMFNetPositionDataTable(dsMfBalancedSellPairedForNetPosition, valuationDate); 
                                  //dtCustomerMutualFundNetPosition.Merge(dtMFAccountSchemeNetPosition);
                                  
                                  if (dtMFAccountSchemeNetPosition.Rows.Count > 0)
                                      dtCustomerMutualFundNetPosition.Merge(dtMFAccountSchemeNetPosition);
                                  dtMFAccountSchemeNetPosition.Clear();

                                  //if (dtMinDateTransToBeProcess != DateTime.MinValue && dtMaxDateTransProcessed != DateTime.MinValue && (dtMinDateTransToBeProcess < dtMaxDateTransProcessed))
                                  //{
                                  //    dtMFTransactionProcessedBalance.Merge(dtMFTransactionBalance, false, MissingSchemaAction.Ignore);
                                  //}

                                  //if (dtMFTransactionProcessedBalance.Rows.Count > 0)
                                  //    dtFinalCustomerMFTransactionBalance.Merge(dtMFTransactionProcessedBalance, false, MissingSchemaAction.Ignore);

                                  dtMFTransactionProcessedBalance.Clear();
                                  dtMFTransactionBalance.Clear();

                                 // dtCustomerMFTransactionSellPaired.Merge(dtMFTrasactionSellPair);
                                  dtMFTrasactionSellPair.Clear();

                              }

                          }

                          break;
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

              FunctionInfo.Add("Method", "MFHistoricalValuationBo.cs:MFNetPositionCreation()");


              object[] objects = new object[3];
              objects[0] = commonId;
              objects[1] = schemePlanCode;
              objects[2] = startFrom;
              FunctionInfo = exBase.AddObject(FunctionInfo, objects);
              exBase.AdditionalInformation = FunctionInfo;
              ExceptionManager.Publish(exBase);
              throw exBase;

          }

      }

    
      #endregion NetPositionCreation
       
     
 
    }
}
