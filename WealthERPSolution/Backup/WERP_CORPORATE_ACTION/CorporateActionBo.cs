using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WERP_CORPORATE_ACTION
{
  public class CorporateActionBo
    {
      CorporateActionDao corporateActionDao = new CorporateActionDao();

      public void ProcessCorporateAction()
      {
          DataSet dsCorporateActionTransactionDetails = new DataSet();
          dsCorporateActionTransactionDetails = corporateActionDao.GetCATransactionDetails();
          
          // need to get caFactor from table. 
          int eqTransId =0 ;
          
          double caFactor = 5, sumSellQuantity = 0, buyQuantity = 0, balanceAmountForCA = 0;
          object sumObject;
          int eqTransIdOld = 0;
          DataTable dtAddFullTransactionAfterSplit = new DataTable();

        #region DataTable Columns Defination
          dtAddFullTransactionAfterSplit.Columns.Add("CET_EqTransId", typeof(System.Int32));
          dtAddFullTransactionAfterSplit.Columns.Add("CETA_AccountId", typeof(System.Int32));
          dtAddFullTransactionAfterSplit.Columns.Add("PEM_ScripCode", typeof(System.Int32));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_TradeDate");
          dtAddFullTransactionAfterSplit.Columns.Add("CET_Rate", typeof(System.Decimal));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_BuySell");
          dtAddFullTransactionAfterSplit.Columns.Add("CET_Quantity", typeof(System.Decimal));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_TradeTotal", typeof(System.Decimal));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_TradeNum", typeof(System.Decimal));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_OrderNum", typeof(System.Decimal));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_Brokerage", typeof(System.Decimal));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_ServiceTax", typeof(System.Decimal));

          dtAddFullTransactionAfterSplit.Columns.Add("CET_EducationCess", typeof(System.Decimal));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_STT", typeof(System.Decimal));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_OtherCharges", typeof(System.Decimal));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_RateInclBrokerage", typeof(System.Decimal));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_ExternalBrokerageAmount", typeof(System.Decimal));


          dtAddFullTransactionAfterSplit.Columns.Add("CET_InternalBrokerageAmount", typeof(System.Decimal));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_InternalBrokeragePer", typeof(System.Decimal));


          dtAddFullTransactionAfterSplit.Columns.Add("CET_ExternalBrokeragePer", typeof(System.Decimal));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_SplitCustEqTransId", typeof(System.Int32));


          dtAddFullTransactionAfterSplit.Columns.Add("ADUL_ProcessId", typeof(System.Int32));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_TransactionStatusChangeDate", typeof(System.DateTime));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_OriginalTransactionNumber", typeof(System.Int32));
          dtAddFullTransactionAfterSplit.Columns.Add("CEDA_DematAccountId");

          dtAddFullTransactionAfterSplit.Columns.Add("CET_ModifiedOn", typeof(System.DateTime));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_ModifiedBy", typeof(System.Int32));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_CreatedOn", typeof(System.DateTime));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_CreatedBy", typeof(System.Int32));

          dtAddFullTransactionAfterSplit.Columns.Add("CET_IsSpeculative", typeof(System.Int32));
          dtAddFullTransactionAfterSplit.Columns.Add("XE_ExchangeCode", typeof(System.String));
          dtAddFullTransactionAfterSplit.Columns.Add("InsertUpdateFlag", typeof(System.Int32));
          dtAddFullTransactionAfterSplit.Columns.Add("WTS_TransactionStatusCode", typeof(System.Int32));
          dtAddFullTransactionAfterSplit.Columns.Add("WETT_TransactionCode", typeof(System.Int32));
          dtAddFullTransactionAfterSplit.Columns.Add("CET_IsSourceManual", typeof(System.Int32));

          dtAddFullTransactionAfterSplit.Columns.Add("XES_SourceCode", typeof(System.String));


        #endregion 
         

          DataRow[] drTransIdDetails;
          if (dsCorporateActionTransactionDetails.Tables[0].Rows.Count > 0)
          {
              foreach (DataRow drCADetails in dsCorporateActionTransactionDetails.Tables[0].Rows)
              {
                  eqTransId = int.Parse(drCADetails["BuyId"].ToString());

                  if (eqTransId != eqTransIdOld)
                  {
                      drTransIdDetails = dsCorporateActionTransactionDetails.Tables[0].Select("BuyId=" + eqTransId.ToString());

                      if (drTransIdDetails.Count() > 0)
                      {
                          eqTransIdOld = eqTransId;

                          foreach (DataRow dr in drTransIdDetails)
                          {
                              DataRow drCA = dr;
                              ///Check for Fully Redeemed
                              #region Fully Redeemed transactions with Partial Sell and Zero Net Shares

                              if (!string.IsNullOrEmpty(dr["BuyId"].ToString()) && !string.IsNullOrEmpty(dr["SellId"].ToString()))
                              {
                                  // 1 OK  2 Cancel 3 Original 4 Rebuilt
                                  // Insert Update Flag :-     1 for Update , 0 for Insert                                                   
                                  //sumObject= dtBalancedDetails.Compute("sum(" + "CMFT_Amount" + ")", "WMTT_TransactionClassificationCode = 'BUY'");                              
                                  if (!string.IsNullOrEmpty(dr["CET_Quantity"].ToString()))
                                  {
                                      sumObject = dsCorporateActionTransactionDetails.Tables[0].Compute("Sum([Share])", "BuyId ='" + eqTransId + "'");
                                      double.TryParse(Convert.ToString(sumObject), out sumSellQuantity);

                                      buyQuantity = double.Parse(dr["CET_Quantity"].ToString());

                                      if (buyQuantity == sumSellQuantity)
                                      {
                                          break;  // No Available unit for Corporate action (Total Sum of sell Quantity Against the Same buy)
                                      }
                                      else if (buyQuantity > sumSellQuantity)
                                      {
                                          #region Partial Sell with holding in net Shares

                                          balanceAmountForCA = buyQuantity - sumSellQuantity;
                                          DataRow drSplittedRow;

                                          for (int i = 0; i < 6; i++)
                                          {
                                              drSplittedRow = dtAddFullTransactionAfterSplit.NewRow();
                                              AssignDefaultTransactionValueToDataTable(ref drSplittedRow, ref drCA);
                                              switch (i)
                                              {
                                                  case 0:
                                                      drSplittedRow["InsertUpdateFlag"] = "1";
                                                      drSplittedRow["CET_EqTransId"] = eqTransId.ToString();
                                                      drSplittedRow["CET_Rate"] = dr["CET_Rate"].ToString();
                                                      drSplittedRow["CET_Quantity"] = dr["CET_Quantity"].ToString();
                                                      drSplittedRow["CET_TradeTotal"] = dr["CET_TradeTotal"].ToString();
                                                      drSplittedRow["WTS_TransactionStatusCode"] = "3";
                                                      drSplittedRow["WETT_TransactionCode"] = dr["WETT_TransactionCode"].ToString();
                                                      break;
                                                  case 1:
                                                      drSplittedRow["InsertUpdateFlag"] = "0";
                                                      drSplittedRow["CET_EqTransId"] = eqTransId.ToString();
                                                      drSplittedRow["CET_Rate"] = dr["CET_Rate"].ToString();
                                                      drSplittedRow["CET_Quantity"] = double.Parse(dr["CET_Quantity"].ToString()) * (-1);
                                                      drSplittedRow["CET_TradeTotal"] = dr["CET_TradeTotal"].ToString();
                                                      drSplittedRow["CET_OriginalTransactionNumber"] = eqTransId.ToString();
                                                      drSplittedRow["WTS_TransactionStatusCode"] = "2";
                                                      drSplittedRow["WETT_TransactionCode"] = dr["WETT_TransactionCode"].ToString();
                                                      break;
                                                  case 2:
                                                      drSplittedRow["InsertUpdateFlag"] = "0";
                                                      drSplittedRow["CET_EqTransId"] = eqTransId.ToString();
                                                      drSplittedRow["CET_Rate"] = double.Parse(dr["CET_Rate"].ToString());
                                                      drSplittedRow["CET_Quantity"] = sumSellQuantity;
                                                      drSplittedRow["CET_TradeTotal"] = double.Parse(drSplittedRow["CET_Rate"].ToString()) * sumSellQuantity;
                                                      drSplittedRow["WTS_TransactionStatusCode"] = "1";
                                                      drSplittedRow["WETT_TransactionCode"] = dr["WETT_TransactionCode"].ToString();
                                                      break;
                                                  case 3:
                                                      drSplittedRow["InsertUpdateFlag"] = "0";
                                                      drSplittedRow["CET_EqTransId"] = eqTransId.ToString();
                                                      drSplittedRow["CET_Rate"] = dr["CET_Rate"].ToString();
                                                      drSplittedRow["CET_Quantity"] = balanceAmountForCA;
                                                      drSplittedRow["CET_TradeTotal"] = balanceAmountForCA * double.Parse(drSplittedRow["CET_Rate"].ToString());
                                                      drSplittedRow["WTS_TransactionStatusCode"] = "3";
                                                      drSplittedRow["WETT_TransactionCode"] = dr["WETT_TransactionCode"].ToString();
                                                      break;
                                                  case 4:
                                                      drSplittedRow["InsertUpdateFlag"] = "0";
                                                      drSplittedRow["CET_EqTransId"] = eqTransId.ToString();
                                                      drSplittedRow["CET_Rate"] = dr["CET_Rate"].ToString();
                                                      drSplittedRow["CET_Quantity"] = balanceAmountForCA * (-1);
                                                      drSplittedRow["CET_TradeTotal"] = balanceAmountForCA * double.Parse(drSplittedRow["CET_Rate"].ToString());
                                                      drSplittedRow["WTS_TransactionStatusCode"] = "2";
                                                      drSplittedRow["WETT_TransactionCode"] = dr["WETT_TransactionCode"].ToString();
                                                      break;
                                                  case 5:
                                                      drSplittedRow["InsertUpdateFlag"] = "0";
                                                      drSplittedRow["CET_EqTransId"] = eqTransId.ToString();
                                                      drSplittedRow["CET_Rate"] = double.Parse(dr["CET_Rate"].ToString()) / caFactor;
                                                      drSplittedRow["CET_Quantity"] = balanceAmountForCA * caFactor;
                                                      drSplittedRow["CET_TradeTotal"] = double.Parse(drSplittedRow["CET_Rate"].ToString()) * double.Parse(drSplittedRow["CET_Quantity"].ToString());
                                                      drSplittedRow["WTS_TransactionStatusCode"] = "1";
                                                      drSplittedRow["WETT_TransactionCode"] = "3";
                                                      break;
                                              }
                                              dtAddFullTransactionAfterSplit.Rows.Add(drSplittedRow);
                                          }
                                          #endregion

                                          break;
                                      }


                                  }
                              }
                              #endregion


                              // Fully Buy without Sell      
                              #region Sell Id Is Null then Direct Apply CA on BUY Units

                              if (string.IsNullOrEmpty(dr["SellId"].ToString()))
                              {
                                  // 1 OK  2 Cancel 3 Original 4 Rebuilt
                                  // Insert Update Flag :-     1 for Update , 0 for Insert
                                  DataRow drSplittedRow;

                                  for (int i = 0; i < 3; i++)
                                  {
                                      drSplittedRow = dtAddFullTransactionAfterSplit.NewRow();
                                      AssignDefaultTransactionValueToDataTable(ref drSplittedRow, ref drCA);
                                      switch (i)
                                      {
                                          case 0:
                                              drSplittedRow["InsertUpdateFlag"] = "1";
                                              drSplittedRow["CET_EqTransId"] = eqTransId.ToString();
                                              drSplittedRow["CET_Rate"] = dr["CET_Rate"].ToString();
                                              drSplittedRow["CET_Quantity"] = dr["CET_Quantity"].ToString();
                                              drSplittedRow["CET_TradeTotal"] = dr["CET_TradeTotal"].ToString();
                                              drSplittedRow["WTS_TransactionStatusCode"] = "3";
                                              drSplittedRow["WETT_TransactionCode"] = dr["WETT_TransactionCode"].ToString();
                                              break;
                                          case 1:
                                              drSplittedRow["InsertUpdateFlag"] = "0";
                                              drSplittedRow["CET_EqTransId"] = eqTransId.ToString();
                                              drSplittedRow["CET_Rate"] = dr["CET_Rate"].ToString();
                                              drSplittedRow["CET_Quantity"] = double.Parse(dr["CET_Quantity"].ToString()) * -1;
                                              drSplittedRow["CET_TradeTotal"] = dr["CET_TradeTotal"].ToString();
                                              drSplittedRow["CET_OriginalTransactionNumber"] = eqTransId.ToString();
                                              drSplittedRow["WTS_TransactionStatusCode"] = "2";
                                              drSplittedRow["WETT_TransactionCode"] = dr["WETT_TransactionCode"].ToString();
                                              break;
                                          case 2:
                                              drSplittedRow["InsertUpdateFlag"] = "0";
                                              drSplittedRow["CET_EqTransId"] = eqTransId.ToString();
                                              drSplittedRow["CET_Rate"] = double.Parse(dr["CET_Rate"].ToString()) / caFactor;
                                              drSplittedRow["CET_Quantity"] = double.Parse(dr["CET_Quantity"].ToString()) * caFactor;
                                              drSplittedRow["CET_TradeTotal"] = double.Parse(drSplittedRow["CET_Rate"].ToString()) * double.Parse(drSplittedRow["CET_Quantity"].ToString());
                                              drSplittedRow["WTS_TransactionStatusCode"] = "1";
                                              drSplittedRow["WETT_TransactionCode"] = "3";
                                              break;
                                      }
                                      dtAddFullTransactionAfterSplit.Rows.Add(drSplittedRow);
                                  }
                              }
                              #endregion

                              //Partial Sell 
                              #region Partial Sell with holding in net Shares

                              //if (!string.IsNullOrEmpty(dr["SellId"].ToString()))
                              //{
                              //    // 1 OK  2 Cancel 3 Original 4 Rebuilt
                              //    // Insert Update Flag :-     1 for Update , 0 for Insert
                              //    DataRow drSplittedRow;

                              //    for (int i = 0; i < 3; i++)
                              //    {
                              //        drSplittedRow = dtAddFullTransactionAfterSplit.NewRow();
                              //        switch (i)
                              //        {
                              //            case 0:
                              //                drSplittedRow["InsertUpdateFlag"] = "1";
                              //                drSplittedRow["CET_EqTransId"] = eqTransId.ToString();
                              //                drSplittedRow["CET_Rate"] = dr["CET_Rate"].ToString();
                              //                drSplittedRow["CET_Quantity"] = dr["CET_Quantity"].ToString();
                              //                drSplittedRow["CET_TradeTotal"] = dr["CET_TradeTotal"].ToString();
                              //                drSplittedRow["WTS_TransactionStatusCode"] = "3";
                              //                break;
                              //            case 1:
                              //                drSplittedRow["InsertUpdateFlag"] = "0";
                              //                drSplittedRow["CET_Rate"] = dr["CET_Rate"].ToString();
                              //                drSplittedRow["CET_Quantity"] = dr["CET_Quantity"].ToString();
                              //                drSplittedRow["CET_TradeTotal"] = dr["CET_TradeTotal"].ToString();
                              //                drSplittedRow["WTS_TransactionStatusCode"] = "2";
                              //                break;
                              //            case 2:
                              //                drSplittedRow["InsertUpdateFlag"] = "0";
                              //                drSplittedRow["CET_Rate"] = double.Parse(dr["CET_Rate"].ToString()) / caFactor;
                              //                drSplittedRow["CET_Quantity"] = double.Parse(dr["CET_Quantity"].ToString()) * caFactor;
                              //                drSplittedRow["CET_TradeTotal"] = double.Parse(drSplittedRow["CET_Rate"].ToString()) * double.Parse(drSplittedRow["CET_Quantity"].ToString());
                              //                drSplittedRow["WTS_TransactionStatusCode"] = "1";
                              //                break;
                              //        }
                              //        dtAddFullTransactionAfterSplit.Rows.Add(drSplittedRow);
                              //    }
                              //}
                              #endregion

                          }
                      }
                  }



              }

              corporateActionDao.CreateAdviserEQSplittedTransactions(1111, dtAddFullTransactionAfterSplit);
          }
          ///Write logic here
          /// CA >> Corporate Action
      }

      protected void AssignDefaultTransactionValueToDataTable(ref DataRow drSplittedRow, ref DataRow dr)
      {
          drSplittedRow["CETA_AccountId"] = dr["CETA_AccountId"].ToString();
          drSplittedRow["PEM_ScripCode"] = dr["PEM_ScripCode"].ToString();
          drSplittedRow["CET_TradeDate"] = dr["CET_TradeDate"].ToString();

          drSplittedRow["CET_BuySell"] = dr["CET_BuySell"].ToString();
          drSplittedRow["CET_TradeNum"] = dr["CET_TradeNum"].ToString();
          drSplittedRow["CET_OrderNum"] = dr["CET_OrderNum"].ToString();


          drSplittedRow["CET_Brokerage"] = dr["CET_Brokerage"].ToString();
          drSplittedRow["CET_ServiceTax"] = dr["CET_ServiceTax"].ToString();
          drSplittedRow["CET_EducationCess"] = dr["CET_EducationCess"].ToString();


          drSplittedRow["CET_STT"] = dr["CET_STT"].ToString();
          drSplittedRow["CET_OtherCharges"] = dr["CET_OtherCharges"].ToString();
          drSplittedRow["CET_RateInclBrokerage"] = dr["CET_RateInclBrokerage"].ToString();


          drSplittedRow["CET_ExternalBrokerageAmount"] = dr["CET_ExternalBrokerageAmount"].ToString();
          drSplittedRow["CET_InternalBrokerageAmount"] = dr["CET_InternalBrokerageAmount"].ToString();
          drSplittedRow["CET_InternalBrokeragePer"] = dr["CET_InternalBrokeragePer"].ToString();


          drSplittedRow["CET_ExternalBrokeragePer"] = dr["CET_ExternalBrokeragePer"].ToString();
          drSplittedRow["CET_SplitCustEqTransId"] = dr["CET_SplitCustEqTransId"].ToString();
          drSplittedRow["ADUL_ProcessId"] = dr["ADUL_ProcessId"].ToString();


          drSplittedRow["CET_TransactionStatusChangeDate"] = DateTime.Now;       
          drSplittedRow["CEDA_DematAccountId"] = dr["CEDA_DematAccountId"].ToString();


          drSplittedRow["CET_ModifiedOn"] = DateTime.Now; 
          drSplittedRow["CET_ModifiedBy"] = dr["CET_ModifiedBy"].ToString();
          drSplittedRow["CET_CreatedOn"] = DateTime.Now; 
          drSplittedRow["CET_CreatedBy"] = dr["CET_CreatedBy"].ToString();



          drSplittedRow["CET_IsSpeculative"] = dr["CET_IsSpeculative"].ToString();
          drSplittedRow["XE_ExchangeCode"] = dr["XE_ExchangeCode"].ToString();          
          drSplittedRow["CET_IsSourceManual"] = dr["CET_IsSourceManual"].ToString();   

      }



    }
}
