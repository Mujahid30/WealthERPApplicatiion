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



//using VoValuation;

namespace BoValuation
{
    public class MFEngineBo
    {

        MFEngineDao mfEngineDao = new MFEngineDao();
        List<int> AdviserCustomers = new List<int>();
        //List<CustomerPortfolioVo> customerPortfolioList = new List<CustomerPortfolioVo>();
        //CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
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

        public enum ValuationLabel
        {
            Advisor,
            Customer,
            Portfolio,
            AccountScheme,
        };

        public void MFBalanceCreation(int commonId, int schemePlanCode, ValuationLabel startFrom)
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
                                    if (customerId == 85747)
                                    {

                                    }
                                    MFBalanceCreation(customerId, 0, ValuationLabel.Customer);

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
                            dtCustomerTransactionBalance = CreateTransactionBalanceTable();
                            dtCustomerTransactionBalance = dsCustomerTransactionsDetails.Tables[3];
                            dtCustomerMFTransactionSellPaired = CreateSellPairedTable();
                            dtFinalCustomerMFTransactionBalance = CreateTransactionBalanceTable();
                            if (dtCustomerPortfolio.Rows.Count > 0)
                            {
                                foreach (DataRow drProftfolio in dtCustomerPortfolio.Rows)
                                {
                                    MFBalanceCreation(Convert.ToInt32(drProftfolio["CP_PortfolioId"].ToString()), 0, ValuationLabel.Portfolio);

                                }

                            }
                            DataSet dsCustomerMFTransBalanceSellPaired = new DataSet();
                            dsCustomerMFTransBalanceSellPaired.Tables.Add(dtFinalCustomerMFTransactionBalance);
                            dsCustomerMFTransBalanceSellPaired.Tables.Add(dtCustomerMFTransactionSellPaired);
                            dsCustomerMFTransBalanceSellPaired.Tables[0].TableName = "TransactionBalance";
                            dsCustomerMFTransBalanceSellPaired.Tables[1].TableName = "TransactionSellPair";
                            if (dsCustomerMFTransBalanceSellPaired.Tables[0].Rows.Count > 0 || dsCustomerMFTransBalanceSellPaired.Tables[1].Rows.Count > 0)
                                mfEngineDao.CreateCustomerMFTransactionBalance(dsCustomerMFTransBalanceSellPaired);

                            dtFinalCustomerMFTransactionBalance.Clear();
                            dtCustomerMFTransactionSellPaired.Clear();
                            dsCustomerMFTransBalanceSellPaired.Tables.Clear();

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
                                        MFBalanceCreation(Convert.ToInt32(drCustomerMFAccount["CMFA_AccountId"].ToString()), Convert.ToInt32(drCustomerMFAccount["PASP_SchemePlanCode"].ToString()), ValuationLabel.AccountScheme);
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
                            DataTable dtMFTransactionProcessedBalanceTemp = new DataTable();

                            dtMFTransactionProcessedBalance = CreateTransactionBalanceTable();
                            dtMFTransactionBalance = CreateTransactionBalanceTable();
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
                                        dtMFTransactionProcessedBalance = TransactionBalanceProcess(dsTransactionBalanceReadyToProcess);
                                    dtMFTransactionProcessedBalance.PrimaryKey = null;



                                    //dtMFTransactionProcessedBalance = dtMFTransactionBalance.Clone();
                                    //dtMFTransactionProcessedBalance.Columns["WTS_TransactionStatusCode"].DataType = dtMFTransactionBalance.Columns["WTS_TransactionStatusCode"].DataType;
                                    //dtMFTransactionProcessedBalance = dtMFTransactionProcessedBalanceTemp;



                                    if (dtMinDateTransToBeProcess != DateTime.MinValue && dtMaxDateTransProcessed != DateTime.MinValue && (dtMinDateTransToBeProcess < dtMaxDateTransProcessed))
                                    {
                                        dtMFTransactionProcessedBalance.Merge(dtMFTransactionBalance, false, MissingSchemaAction.Ignore);
                                    }

                                    if (dtMFTransactionProcessedBalance.Rows.Count > 0)
                                        dtFinalCustomerMFTransactionBalance.Merge(dtMFTransactionProcessedBalance, false, MissingSchemaAction.Ignore);

                                    dtMFTransactionProcessedBalance.Clear();
                                    dtMFTransactionBalance.Clear();

                                    dtCustomerMFTransactionSellPaired.Merge(dtMFTrasactionSellPair);
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

                FunctionInfo.Add("Method", "MFEngineBo.cs:MFBalanceCreation()");


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

        public DataTable CreateSellPairedTable()
        {
            DataTable dtSellPaired = new DataTable();
            dtSellPaired.Columns.Add("CMFT_MFTransIdSell", typeof(Int32));
            dtSellPaired.Columns.Add("CMFT_MFTransIdBuy", typeof(Int32));
            dtSellPaired.Columns.Add("CMFT_Age", typeof(Int32));
            dtSellPaired.Columns.Add("CMFT_LTG", typeof(double));
            dtSellPaired.Columns.Add("CMFT_STG", typeof(double));
            dtSellPaired.Columns.Add("CMFT_Gain_loss_Value", typeof(double));
            dtSellPaired.Columns.Add("CMFT_Units", typeof(double));
            dtSellPaired.Columns.Add("CMFT_PriceBuy", typeof(double));
            dtSellPaired.Columns.Add("CMFT_Action", typeof(Int16));

            return dtSellPaired;
        }


        #region BalanceCreation

        protected DataTable CreateTransactionBalanceTable()
        {
            DataTable dtTransactionDetails = new DataTable();
            dtTransactionDetails.Columns.Add("CMFT_MFTransId", typeof(System.Int32));
            dtTransactionDetails.Columns.Add("CMFA_AccountId", typeof(System.Int32));
            dtTransactionDetails.Columns.Add("PASP_SchemePlanCode", typeof(System.Int32));
            dtTransactionDetails.Columns.Add("CMFT_TransactionDate", typeof(System.DateTime));
            dtTransactionDetails.Columns.Add("CMFT_Price", typeof(System.Decimal));
            dtTransactionDetails.Columns.Add("CMFT_Units", typeof(System.Decimal));
            dtTransactionDetails.Columns.Add("CMFT_Amount", typeof(System.Decimal));
            dtTransactionDetails.Columns.Add("CMFT_STT", typeof(System.Decimal));
            dtTransactionDetails.Columns.Add("WTS_TransactionStatusCode", typeof(System.Int32));
            dtTransactionDetails.Columns.Add("WMTT_TransactionClassificationCode", typeof(System.String));

            dtTransactionDetails.Columns.Add("CMFTB_Id", typeof(System.Int32));
            dtTransactionDetails.Columns.Add("CMFTB_UnitBalanceTAX", typeof(System.Decimal));
            dtTransactionDetails.Columns.Add("CMFTB_TotalCostBalanceTAX", typeof(System.Decimal));
            dtTransactionDetails.Columns.Add("CMFTB_UnitBalanceRETURN", typeof(System.Decimal));
            dtTransactionDetails.Columns.Add("CMFTB_AvgCostBalRETURN", typeof(System.Decimal));
            dtTransactionDetails.Columns.Add("CMFTB_TotalCostBalRETURN", typeof(System.Decimal));
            dtTransactionDetails.Columns.Add("CMFTB_Age", typeof(System.Int32));
            dtTransactionDetails.Columns.Add("CMFTB_DivPayout", typeof(System.Decimal));
            dtTransactionDetails.Columns.Add("CMFTB_DVRUnitsAllocation_Share", typeof(System.Decimal));
            dtTransactionDetails.Columns.Add("CMFTB_DVRUnits_Contributed", typeof(System.Decimal));
            dtTransactionDetails.Columns.Add("CMFTB_InsertUpdate_Flag", typeof(System.Int32));

            return dtTransactionDetails;
        }

        protected DataTable TransactionBalanceProcess(DataSet dsTransactionList)
        {
            DataTable dtTransactionDetails = new DataTable();
            DataTable dtTransactionDetailsTemp = new DataTable();
            string transactionType;
            dtMFTrasactionSellPair = CreateSellPairedTable();

            dtTransactionDetails = CreateTransactionBalanceTable();

            //double avgValue=0;

            dtTransactionDetails.PrimaryKey = new DataColumn[] { dtTransactionDetails.Columns["CMFT_MFTransId"] };
            DataRow drTransactionDetails;
            DataTable dtDefaultView;
            if (dsTransactionList.Tables.Count > 1)
            {
                if (dsTransactionList.Tables["Balance"].Rows.Count > 0)
                {
                    dtTransactionDetails = dsTransactionList.Tables["Balance"].Copy();
                    dtTransactionDetailsTemp = dsTransactionList.Tables["Balance"].Copy();
                }
            }
            try
            {
                if (dsTransactionList.Tables["Transaction"].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTransactionList.Tables["Transaction"].Rows)
                    {
                        transactionType = dr["WMTT_TransactionClassificationCode"].ToString();
                        drTransactionDetails = dtTransactionDetails.NewRow();
                        TimeSpan span = new TimeSpan();

                        switch (transactionType)
                        {
                            case "BUY":
                                drTransactionDetails["CMFT_MFTransId"] = dr["CMFT_MFTransId"].ToString();
                                drTransactionDetails["WMTT_TransactionClassificationCode"] = dr["WMTT_TransactionClassificationCode"].ToString();
                                drTransactionDetails["CMFT_TransactionDate"] = Convert.ToDateTime(dr["CMFT_TransactionDate"].ToString());
                                drTransactionDetails["CMFT_Price"] = dr["CMFT_Price"];
                                drTransactionDetails["CMFT_Units"] = dr["CMFT_Units"].ToString();
                                drTransactionDetails["CMFT_Amount"] = dr["CMFT_Amount"].ToString();
                                drTransactionDetails["CMFT_STT"] = dr["CMFT_STT"].ToString();
                                drTransactionDetails["WTS_TransactionStatusCode"] = Convert.ToInt32(dr["WTS_TransactionStatusCode"].ToString());

                                span = DateTime.Today - DateTime.Parse(dr["CMFT_TransactionDate"].ToString()); // Need to change this as Valuation date
                                drTransactionDetails["CMFTB_Age"] = span.TotalDays;

                                drTransactionDetails["CMFTB_UnitBalanceTAX"] = dr["CMFT_Units"].ToString();
                                drTransactionDetails["CMFTB_TotalCostBalanceTAX"] = double.Parse(drTransactionDetails["CMFTB_UnitBalanceTAX"].ToString()) * double.Parse(dr["CMFT_Price"].ToString()); ;
                                drTransactionDetails["CMFTB_UnitBalanceRETURN"] = dr["CMFT_Units"].ToString();
                                drTransactionDetails["CMFTB_DivPayout"] = 0;
                                drTransactionDetails["CMFTB_AvgCostBalRETURN"] = dr["CMFT_Price"].ToString();
                                drTransactionDetails["CMFTB_TotalCostBalRETURN"] = dr["CMFT_Amount"].ToString();
                                drTransactionDetails["CMFTB_DVRUnitsAllocation_Share"] = 0;
                                drTransactionDetails["CMFTB_DVRUnits_Contributed"] = 0;
                                drTransactionDetails["CMFTB_InsertUpdate_Flag"] = 1;
                                dtTransactionDetails.Rows.Add(drTransactionDetails);
                                dtTransactionDetailsTemp = dtTransactionDetails.Copy();

                                break;

                            case "DVR":
                                double dvrUnits = 0;
                                drTransactionDetails["CMFT_MFTransId"] = dr["CMFT_MFTransId"].ToString();
                                drTransactionDetails["WMTT_TransactionClassificationCode"] = dr["WMTT_TransactionClassificationCode"].ToString();
                                drTransactionDetails["CMFT_TransactionDate"] = Convert.ToDateTime(dr["CMFT_TransactionDate"].ToString());

                                drTransactionDetails["CMFT_Price"] = dr["CMFT_Price"];
                                drTransactionDetails["CMFT_Units"] = dr["CMFT_Units"].ToString();
                                drTransactionDetails["CMFT_Amount"] = dr["CMFT_Amount"].ToString();
                                drTransactionDetails["CMFT_STT"] = dr["CMFT_STT"].ToString();
                                drTransactionDetails["WTS_TransactionStatusCode"] = Convert.ToInt32(dr["WTS_TransactionStatusCode"].ToString());
                                drTransactionDetails["CMFTB_DVRUnitsAllocation_Share"] = 0;
                                drTransactionDetails["CMFTB_DVRUnits_Contributed"] = 0;
                                span = DateTime.Today - DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                                drTransactionDetails["CMFTB_Age"] = span.TotalDays;

                                drTransactionDetails["CMFTB_UnitBalanceTAX"] = dr["CMFT_Units"].ToString();
                                dvrUnits = double.Parse(dr["CMFT_Units"].ToString());
                                drTransactionDetails["CMFTB_TotalCostBalanceTAX"] = double.Parse(drTransactionDetails["CMFTB_UnitBalanceTAX"].ToString()) * double.Parse(dr["CMFT_Price"].ToString());
                                drTransactionDetails["CMFTB_UnitBalanceRETURN"] = dr["CMFT_Units"].ToString();
                                drTransactionDetails["CMFTB_DivPayout"] = 0;
                                drTransactionDetails["CMFTB_AvgCostBalRETURN"] = dr["CMFT_Price"].ToString();
                                drTransactionDetails["CMFTB_TotalCostBalRETURN"] = 0;
                                drTransactionDetails["CMFTB_InsertUpdate_Flag"] = 1;
                                dtTransactionDetails.Rows.Add(drTransactionDetails);
                                if (dtTransactionDetails.Rows.Count > 1)
                                {
                                    double sum = 0;
                                    double avgCostReturn = 0;
                                    double totalCostBalanceReturn = 0;
                                    double unitBalanceReturnOld = 0;
                                    int count1 = 0;
                                    double dvrUnitsContribution = 0;
                                    double totalCost = 0;
                                    double unitBalanceReturnOldSum=0.0;

                                    DataRow[] drLastTransactionDetails;

                                    object sumObject;
                                    //sumObject= dtBalancedDetails.Compute("sum(" + "CMFT_Amount" + ")", "WMTT_TransactionClassificationCode = 'BUY'");

                                    sumObject = dtTransactionDetailsTemp.Compute("Sum([CMFTB_UnitBalanceRETURN])", string.Empty);
                                    double.TryParse(Convert.ToString(sumObject), out sum);

                                    sumObject = dtTransactionDetailsTemp.Compute("Sum([CMFTB_TotalCostBalRETURN])", string.Empty);
                                    double.TryParse(Convert.ToString(sumObject), out totalCost);                               

                                    dtTransactionDetails.DefaultView.RowFilter = expression;
                                    //  dtTransactionDetails = dtTransactionDetails.DefaultView.Table;
                                    dtDefaultView = dtTransactionDetails.DefaultView.ToTable();
                                    dtDefaultView.PrimaryKey = new DataColumn[] { dtDefaultView.Columns["CMFT_MFTransId"] };
                                    if (dtDefaultView.Rows.Count > 0)
                                    {
                                        foreach (DataRow dr3 in dtDefaultView.Rows)
                                        {
                                            //count1 is for finding the last row in the default view
                                            
                                            count1++;
                                            if (dr3["WMTT_TransactionClassificationCode"].ToString() != "SEL" && dr3["WMTT_TransactionClassificationCode"].ToString() != "DVP")
                                            {
                                                if (dr3["CMFTB_Id"].ToString() != null && dr3["CMFTB_Id"].ToString() != "")
                                                {
                                                    dr3["CMFTB_InsertUpdate_Flag"] = 2;
                                                }
                                                if (count1 < dtDefaultView.Rows.Count)
                                                {
                                                    // this is for executing the last balanced record in default view filtered data with Tax bal > 0 

                                                    drLastTransactionDetails = dtTransactionDetailsTemp.Select("CMFT_MFTransId=" + dr3["CMFT_MFTransId"].ToString());

                                                    if (drLastTransactionDetails.Count() > 0)
                                                    {
                                                        foreach (DataRow dr1 in drLastTransactionDetails)
                                                        {
                                                            totalCostBalanceReturn = double.Parse(dr1["CMFTB_TotalCostBalRETURN"].ToString());
                                                            unitBalanceReturnOld = double.Parse(dr1["CMFTB_UnitBalanceRETURN"].ToString());
                                                        }
                                                    }

                                                    if (sum != 0)
                                                        unitBalanceReturnOldSum = (unitBalanceReturnOld / sum);
                                                    else
                                                        unitBalanceReturnOldSum = unitBalanceReturnOld;

                                                    dvrUnitsContribution = dvrUnits * unitBalanceReturnOldSum;
                                                    avgCostReturn = totalCostBalanceReturn / (dvrUnitsContribution + double.Parse(dr3["CMFTB_UnitBalanceRETURN"].ToString()));

                                                    dr3["CMFTB_DVRUnitsAllocation_Share"] = unitBalanceReturnOldSum;
                                                    
                                                    dr3["CMFTB_DVRUnits_Contributed"] = dvrUnitsContribution;
                                                    dr3["CMFTB_AvgCostBalRETURN"] = avgCostReturn;
                                                    dr3["CMFTB_TotalCostBalRETURN"] = avgCostReturn * double.Parse(dr3["CMFTB_UnitBalanceRETURN"].ToString());
                                                }
                                                else
                                                {
                                                    // this is for executing the last balanced record in default view filtered data with Tax bal > 0 

                                                    double totalCostBalance = 0;
                                                    double avlCostDVR = 0;
                                                    foreach (DataRow dr4 in dtDefaultView.Rows)
                                                    {
                                                        totalCostBalance = totalCostBalance + double.Parse(dr4["CMFTB_TotalCostBalRETURN"].ToString());
                                                    }
                                                    avlCostDVR = totalCost - totalCostBalance;
                                                    dr3["CMFTB_TotalCostBalRETURN"] = avlCostDVR;
                                                    if (dvrUnits != 0)
                                                        dr3["CMFTB_AvgCostBalRETURN"] = avlCostDVR / dvrUnits;
                                                    else
                                                        dr3["CMFTB_AvgCostBalRETURN"] = 0;
                                                    dr3["CMFTB_DVRUnitsAllocation_Share"] = 0;
                                                    dr3["CMFTB_DVRUnits_Contributed"] = 0;
                                                }
                                            }

                                        }
                                        // dtTransactionDetails = dtTransactionDetails.DefaultView.Table.Copy();
                                        dtTransactionDetails.Merge(dtDefaultView, false);

                                    }
                                }
                                dtTransactionDetailsTemp = dtTransactionDetails.Copy();
                                break;
                            //case "DVR":
                            //    drTransactionDetails["CMFT_MFTransId"] = dr["CMFT_MFTransId"].ToString();
                            //    drTransactionDetails["WMTT_TransactionClassificationCode"] = dr["WMTT_TransactionClassificationCode"].ToString();
                            //    drTransactionDetails["CMFT_TransactionDate"] = dr["CMFT_TransactionDate"].ToString();
                            //    drTransactionDetails["CMFT_Price"] = dr["CMFT_Price"].ToString();
                            //    drTransactionDetails["CMFT_Units"] = dr["CMFT_Units"].ToString();
                            //    drTransactionDetails["CMFT_Amount"] = dr["CMFT_Amount"].ToString();
                            //    drTransactionDetails["CMFT_STT"] = dr["CMFT_STT"].ToString();
                            //    drTransactionDetails["WTS_TransactionStatusCode"] = dr["WTS_TransactionStatusCode"].ToString();

                            //    span = navDate - DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                            //    drTransactionDetails["CMFTB_Age"] = span.TotalDays;

                            //    drTransactionDetails["CMFTB_UnitBalanceTAX"] = dr["CMFT_Units"].ToString();
                            //    drTransactionDetails["CMFTB_TotalCostBalanceTAX"] = double.Parse(dr["CMFT_Units"].ToString()) * double.Parse(dr["CMFT_Price"].ToString());
                            //    drTransactionDetails["CMFTB_UnitBalanceRETURN"] = dr["CMFT_Units"].ToString();
                            //    drTransactionDetails["CMFTB_DivPayout"] = 0;
                            //    drTransactionDetails["CMFTB_AvgCostBalRETURN"] = dr["CMFT_Price"].ToString();
                            //    drTransactionDetails["CMFTB_TotalCostBalRETURN"] = dr["CMFT_Amount"].ToString();
                            //    dtTransactionDetails.Rows.Add(drTransactionDetails);
                            //    if (dtTransactionDetails.Rows.Count > 1)
                            //    {
                            //        double sum = 0;
                            //        double costReturn = 0;
                            //        double avgCostReturn = 0;
                            //        int count1 = 0;
                            //        foreach (DataRow dr2 in dtTransactionDetails.Rows)
                            //        {
                            //            sum = sum + double.Parse(dr2["CMFTB_UnitBalanceRETURN"].ToString());
                            //            count1++;
                            //            if (count1 < dtTransactionDetails.Rows.Count)
                            //            {
                            //                costReturn = costReturn + (double.Parse(dr2["CMFTB_UnitBalanceRETURN"].ToString()) * avgValue);
                            //            }
                            //        }

                            //        avgCostReturn = costReturn / sum;
                            //        foreach (DataRow dr3 in dtTransactionDetails.Rows)
                            //        {
                            //            dr3["CMFTB_AvgCostBalRETURN"] = avgCostReturn;
                            //            dr3["CMFTB_TotalCostBalRETURN"] = avgCostReturn * double.Parse(dr3["CMFTB_UnitBalanceRETURN"].ToString());

                            //        }

                            //    }

                            //    break;
                            case "DVP":
                                double amount = double.Parse(dr["CMFT_Amount"].ToString());
                                drTransactionDetails["CMFT_MFTransId"] = dr["CMFT_MFTransId"].ToString();
                                drTransactionDetails["WMTT_TransactionClassificationCode"] = dr["WMTT_TransactionClassificationCode"].ToString();
                                drTransactionDetails["CMFT_TransactionDate"] =Convert.ToDateTime(dr["CMFT_TransactionDate"].ToString());
                                drTransactionDetails["CMFT_Price"] = dr["CMFT_Price"];
                                drTransactionDetails["CMFT_Units"] = dr["CMFT_Units"].ToString();
                                drTransactionDetails["CMFT_Amount"] = dr["CMFT_Amount"].ToString();
                                drTransactionDetails["CMFT_STT"] = dr["CMFT_STT"].ToString();
                                drTransactionDetails["WTS_TransactionStatusCode"] = Convert.ToInt16(dr["WTS_TransactionStatusCode"].ToString());
                                span = DateTime.Today - DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                                drTransactionDetails["CMFTB_Age"] = span.TotalDays;
                                drTransactionDetails["CMFTB_UnitBalanceTAX"] = dr["CMFT_Units"].ToString();
                                drTransactionDetails["CMFTB_TotalCostBalanceTAX"] = double.Parse(drTransactionDetails["CMFTB_UnitBalanceTAX"].ToString()) * double.Parse(dr["CMFT_Price"].ToString());
                                drTransactionDetails["CMFTB_UnitBalanceRETURN"] = dr["CMFT_Units"].ToString();
                                drTransactionDetails["CMFTB_DivPayout"] = 0;
                                drTransactionDetails["CMFTB_AvgCostBalRETURN"] = dr["CMFT_Price"].ToString();
                                drTransactionDetails["CMFTB_TotalCostBalRETURN"] = 0;
                                drTransactionDetails["CMFTB_DVRUnitsAllocation_Share"] = 0;
                                drTransactionDetails["CMFTB_DVRUnits_Contributed"] = 0;
                                drTransactionDetails["CMFTB_InsertUpdate_Flag"] = 1;
                                dtTransactionDetails.Rows.Add(drTransactionDetails);

                                dtTransactionDetails.DefaultView.RowFilter = expression;
                                dtDefaultView = dtTransactionDetails.DefaultView.ToTable();
                                if (dtDefaultView.Rows.Count > 0)
                                {
                                    double sum = 0;
                                    int count1 = 0;

                                    dtDefaultView.PrimaryKey = new DataColumn[] { dtDefaultView.Columns["CMFT_MFTransId"] };
                                    //  dtTransactionDetails = dtTransactionDetails.DefaultView.Table;
                                    if (dtDefaultView.Rows.Count > 0)
                                    {
                                        object sumObject;

                                        sumObject = dtDefaultView.Compute("Sum([CMFTB_UnitBalanceRETURN])", string.Empty);
                                        double.TryParse(Convert.ToString(sumObject), out sum);

                                        //foreach (DataRow dr2 in dtDefaultView.Rows)
                                        //{
                                        //    sum = sum + double.Parse(dr2["CMFTB_UnitBalanceRETURN"].ToString());

                                        //}
                                        foreach (DataRow dr1 in dtDefaultView.Rows)
                                        {
                                            count1++;
                                            if (dr1["CMFTB_Id"].ToString() != null && dr1["CMFTB_Id"].ToString() != "")
                                            {
                                                dr1["CMFTB_InsertUpdate_Flag"] = 2;
                                            }
                                            if (dr1["WMTT_TransactionClassificationCode"].ToString() == "DVR" || dr1["WMTT_TransactionClassificationCode"].ToString() == "BUY")
                                                dr1["CMFTB_DivPayout"] = double.Parse(dr1["CMFTB_DivPayout"].ToString())+(double.Parse(dr1["CMFTB_UnitBalanceRETURN"].ToString()) / sum) * amount;
                                            //costReturn = costReturn + (double.Parse(dr2["CMFTB_UnitBalanceRETURN"].ToString()) * avgValue);


                                        }
                                    }

                                    dtTransactionDetails.Merge(dtDefaultView, false);
                                }

                                dtTransactionDetailsTemp = dtTransactionDetails.Copy();
                                break;

                            case "SEL":
                                DataTable dtmodifiedDetails = new DataTable();
                                drTransactionDetails["CMFT_MFTransId"] = dr["CMFT_MFTransId"].ToString();
                                drTransactionDetails["WMTT_TransactionClassificationCode"] = dr["WMTT_TransactionClassificationCode"].ToString();
                                drTransactionDetails["CMFT_TransactionDate"] =DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                                drTransactionDetails["CMFT_Price"] = dr["CMFT_Price"];
                                drTransactionDetails["CMFT_Units"] = dr["CMFT_Units"].ToString();
                                drTransactionDetails["CMFT_Amount"] = dr["CMFT_Amount"].ToString();
                                drTransactionDetails["CMFT_STT"] = dr["CMFT_STT"].ToString();
                                drTransactionDetails["WTS_TransactionStatusCode"] = Convert.ToInt32(dr["WTS_TransactionStatusCode"].ToString());
                                span = DateTime.Today - DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                                drTransactionDetails["CMFTB_Age"] = span.TotalDays;
                                drTransactionDetails["CMFTB_UnitBalanceTAX"] = 0;
                                drTransactionDetails["CMFTB_TotalCostBalanceTAX"] = 0;
                                drTransactionDetails["CMFTB_UnitBalanceRETURN"] = 0;
                                drTransactionDetails["CMFTB_DivPayout"] = 0;
                                drTransactionDetails["CMFTB_AvgCostBalRETURN"] = 0;
                                drTransactionDetails["CMFTB_DVRUnitsAllocation_Share"] = 0;
                                drTransactionDetails["CMFTB_DVRUnits_Contributed"] = 0;
                                drTransactionDetails["CMFTB_TotalCostBalRETURN"] = 0;
                                drTransactionDetails["CMFTB_InsertUpdate_Flag"] = 1;
                                dtTransactionDetails.Rows.Add(drTransactionDetails);
                                dtTransactionDetails.DefaultView.RowFilter = expression;
                                dtDefaultView = dtTransactionDetails.DefaultView.ToTable();
                               // dtDefaultView.PrimaryKey = new DataColumn[] { dtDefaultView.Columns["CMFT_MFTransId"] };
                                dtmodifiedDetails = GetTransactiondetailsAfterSell(dtDefaultView, double.Parse(dr["CMFT_Units"].ToString()), double.Parse(dr["CMFT_Price"].ToString()), int.Parse(dr["CMFT_MFTransId"].ToString()), DateTime.Parse(dr["CMFT_TransactionDate"].ToString()));
                                dtTransactionDetails.Merge(dtmodifiedDetails, false);
                                dtTransactionDetailsTemp = dtTransactionDetails.Copy();
                                break;
                        }
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

                FunctionInfo.Add("Method", "MFEngineBo.cs:TransactionBalanceProcess()");

                object[] objects = new object[2];
                objects[0] = dsTransactionList;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtTransactionDetails;
        }

        protected DataTable GetTransactiondetailsAfterSell(DataTable dt, double sellUnits, double sellPrice, int sellId, DateTime sellTransactiondate)
        {
            //DataTable dtSellPaired = new DataTable();          
            TimeSpan span = new TimeSpan();
            double buyUnits = 0;
            double age;
            //  double avgValue=0;
            double units = sellUnits;
            foreach (DataRow dr in dt.Rows)
            {

                if (dr["WMTT_TransactionClassificationCode"].ToString() != "SEL" && dr["WMTT_TransactionClassificationCode"].ToString() != "DVP")
                {
                    //avgValue = double.Parse(dr["CMFTB_AvgCostBalRETURN"].ToString());

                    // if CMFTB_Id is null then this will get insert in Database with flag 1
                    // else It will update with flag 2
                    // 0 for No change
                    if (dr["CMFTB_Id"].ToString() != null && dr["CMFTB_Id"].ToString() != "")
                    {
                        dr["CMFTB_InsertUpdate_Flag"] = 2;
                    }
                    buyUnits = double.Parse(dr["CMFTB_UnitBalanceTAX"].ToString());
                    if (buyUnits != 0)
                    {
                        if (buyUnits == sellUnits)
                        {
                            dr["CMFTB_UnitBalanceTAX"] = 0;
                            dr["CMFTB_TotalCostBalanceTAX"] = 0;
                            span = sellTransactiondate - DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                            age = span.TotalDays;
                            FillSellPairedDataSet(sellUnits, age, sellId, double.Parse(dr["CMFT_MFTransId"].ToString()), sellPrice, double.Parse(dr["CMFT_Price"].ToString()));
                            break;
                        }
                        else if (buyUnits > sellUnits)
                        {
                            dr["CMFTB_UnitBalanceTAX"] = buyUnits - sellUnits;
                            dr["CMFTB_TotalCostBalanceTAX"] = double.Parse(dr["CMFTB_UnitBalanceTAX"].ToString()) * double.Parse(dr["CMFT_Price"].ToString());
                            span = sellTransactiondate - DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                            age = span.TotalDays;
                            FillSellPairedDataSet(sellUnits, age, sellId, double.Parse(dr["CMFT_MFTransId"].ToString()), sellPrice, double.Parse(dr["CMFT_Price"].ToString()));

                            break;
                        }
                        else if (buyUnits < sellUnits)
                        {
                            dr["CMFTB_UnitBalanceTAX"]=0;
                            dr["CMFTB_TotalCostBalanceTAX"] = 0;
                            sellUnits = sellUnits - buyUnits;                           
                            span = sellTransactiondate - DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                            age = span.TotalDays;
                            FillSellPairedDataSet(buyUnits, age, sellId, double.Parse(dr["CMFT_MFTransId"].ToString()), sellPrice, double.Parse(dr["CMFT_Price"].ToString()));

                        }
                    }
                }
            }
            dt = UpdateBalancedTransactionDetails(dt, units);
            return dt;
        }

        protected void FillSellPairedDataSet(double units, double age, double sellId, double buyId, double sellPrice, double buyPrice)
        {
            //if (dtSellPaired.Rows.Count == 0)
            //{
            //    dtSellPaired.Columns.Add("CMFT_MFTransIdSell");
            //    dtSellPaired.Columns.Add("CMFT_MFTransIdBuy");
            //    dtSellPaired.Columns.Add("CMFT_Age");
            //    dtSellPaired.Columns.Add("CMFT_LTG");
            //    dtSellPaired.Columns.Add("CMFT_STG");
            //    dtSellPaired.Columns.Add("CMFT_Gain_loss_Value");
            //    dtSellPaired.Columns.Add("CMFT_Units");
            //    dtSellPaired.Columns.Add("CMFT_PriceBuy");
            //}
            double gainLossValue = 0;
            DataRow drSellPaired;
            drSellPaired = dtMFTrasactionSellPair.NewRow();

            drSellPaired["CMFT_MFTransIdSell"] = sellId;
            drSellPaired["CMFT_MFTransIdBuy"] = buyId;
            drSellPaired["CMFT_Age"] = age;
            drSellPaired["CMFT_Units"] = units;
            drSellPaired["CMFT_PriceBuy"] = buyPrice;
            gainLossValue = units * (sellPrice - buyPrice);
            drSellPaired["CMFT_Gain_loss_Value"] = gainLossValue;
            drSellPaired["CMFT_LTG"] = 0;
            drSellPaired["CMFT_STG"] = 0;
            if (isMFTractionSellPairRecreate == true)
            {
                drSellPaired["CMFT_Action"] = 3;
            }
            else
            {
                drSellPaired["CMFT_Action"] = 1;

            }
            if (gainLossValue > 0)
            {
                if (age > 365)
                {
                    drSellPaired["CMFT_LTG"] = gainLossValue;
                }
                else
                {
                    drSellPaired["CMFT_STG"] = gainLossValue;
                }
            }

            dtMFTrasactionSellPair.Rows.Add(drSellPaired);

            //return dtMFTrasactionSellPair;
        }

        protected DataTable UpdateBalancedTransactionDetails(DataTable dt, double units)
        {

            double sum = 0;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr2 in dt.Rows)
                {
                    sum = sum + double.Parse(dr2["CMFTB_UnitBalanceRETURN"].ToString());
                }
                foreach (DataRow dr1 in dt.Rows)
                {
                    double unitBalancereturnOld = double.Parse(dr1["CMFTB_UnitBalanceRETURN"].ToString());
                    dr1["CMFTB_UnitBalanceRETURN"] = double.Parse(dr1["CMFTB_UnitBalanceRETURN"].ToString()) - (double.Parse(dr1["CMFTB_UnitBalanceRETURN"].ToString()) / sum) * units;


                    if (dr1["WMTT_TransactionClassificationCode"].ToString() == "BUY" || dr1["WMTT_TransactionClassificationCode"].ToString() == "DVR")
                    {
                        dr1["CMFTB_DivPayout"] = (double.Parse(dr1["CMFTB_UnitBalanceRETURN"].ToString()) / unitBalancereturnOld) * double.Parse(dr1["CMFTB_DivPayout"].ToString());
                        dr1["CMFTB_TotalCostBalRETURN"] = double.Parse(dr1["CMFTB_AvgCostBalRETURN"].ToString()) * double.Parse(dr1["CMFTB_UnitBalanceRETURN"].ToString());

                    }
                    else
                    {
                        dr1["CMFTB_DivPayout"] = 0;
                        dr1["CMFTB_AvgCostBalRETURN"] = 0;
                    }
                }
            }
            return dt;
        }

        #endregion BalanceCreation

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
                            DataSet dsCustomerMFPortfolioAccountDetails = new DataSet();
                            dsCustomerMFPortfolioAccountDetails = mfEngineDao.GetCustomerAllMFPortfolioAccountForValution(commonId);
                            dtCustomerPortfolio = dsCustomerMFPortfolioAccountDetails.Tables[0];
                            dtCustomerAccount = dsCustomerMFPortfolioAccountDetails.Tables[1];
                            dtCustomerMFTransactionSellPaired = CreateSellPairedTable();
                            if (dtCustomerPortfolio.Rows.Count > 0)
                            {
                                foreach (DataRow drProftfolio in dtCustomerPortfolio.Rows)
                                {
                                    MFNetPositionCreation(Convert.ToInt32(drProftfolio["CP_PortfolioId"].ToString()), 0, ValuationLabel.Portfolio, valuationDate);

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
                                        MFNetPositionCreation(Convert.ToInt32(drCustomerMFAccount["CMFA_AccountId"].ToString()), Convert.ToInt32(drCustomerMFAccount["PASP_SchemePlanCode"].ToString()), ValuationLabel.AccountScheme, valuationDate);
                                    }
                                }

                            }



                            break;
                        }
                    case "AccountScheme":
                        {
                            //DateTime dtMinDateTransToBeProcess = new DateTime();
                            //DateTime dtMaxDateTransProcessed = new DateTime();
                            //DataSet dsTransactionBalanceReadyToProcess = new DataSet();
                            //DataTable dtMFTransactionBalance = new DataTable();
                            //DataTable dtMFTransactionProcessedBalance = new DataTable();

                            DataSet dsMFTransactionBalanceAndSellPair = new DataSet();
                            DataTable dtMFAccountSchemeNetPosition = new DataTable();
                            dsMFTransactionBalanceAndSellPair = mfEngineDao.GetMFTransactionBalanceAndSellPairAccountSchemeWise(commonId, schemePlanCode, valuationDate);
                            //if (commonId == 227769)
                            //{

                            //}
                            dtMFAccountSchemeNetPosition = CreateMFNetPositionDataTable(dsMFTransactionBalanceAndSellPair, valuationDate);
                            if (dtMFAccountSchemeNetPosition.Rows.Count > 0)
                                dtCustomerMutualFundNetPosition.Merge(dtMFAccountSchemeNetPosition);
                            dtMFAccountSchemeNetPosition.Clear();

                        }

                        break;
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

                FunctionInfo.Add("Method", "MFEngineBo.cs:MFNetPositionCreation()");

                object[] objects = new object[3];
                objects[0] = commonId;
                objects[1] = schemePlanCode;
                objects[2] = startFrom;
                objects[3] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        #endregion NetPositionCreation

        public DataTable CreateNetpositionTable()
        {
            DataTable dtMFNetPosition = new DataTable();
            dtMFNetPosition.Columns.Add("CMFA_AccountId", typeof(Int32));
            dtMFNetPosition.Columns.Add("PASP_SchemePlanCode", typeof(Int32));
            dtMFNetPosition.Columns.Add("CMFNP_MarketPrice", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_ValuationDate", typeof(DateTime));
            dtMFNetPosition.Columns.Add("CMFNP_NetHoldings", typeof(double));

            dtMFNetPosition.Columns.Add("CMFNP_SalesQuantity", typeof(double));  //1 done
            dtMFNetPosition.Columns.Add("CMFNP_RedeemedAmount", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_InvestedCost", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_CurrentValue", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_RET_ALL_TotalPL", typeof(double));

            dtMFNetPosition.Columns.Add("CMFNP_RET_ALL_AbsReturn", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_RET_ALL_TotalXIRR", typeof(double));  //2 nt req
            dtMFNetPosition.Columns.Add("CMFNP_RET_ALL_DVRAmt", typeof(double));   //3  done

            dtMFNetPosition.Columns.Add("CMFNP_RET_ALL_DVPAmt", typeof(double));   //4  done
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_AcqCost", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_TotalPL", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_AbsReturn", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_PurchaseUnit", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_DVRUnits", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_DVPAmt", typeof(double));  //5 done
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_XIRR", typeof(double));  //6  nt req


            dtMFNetPosition.Columns.Add("CMFNP_RET_Realized_InvestedCost", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Realized_DVPAmt", typeof(double)); //7 done
            dtMFNetPosition.Columns.Add("CMFNP_RET_Realized_DVRAmt", typeof(double));  //8   
            dtMFNetPosition.Columns.Add("CMFNP_RET_Realized_TotalPL", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Realized_AbsReturn", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Realized_XIRR", typeof(double));  //9  nt req


            dtMFNetPosition.Columns.Add("CMFNP_TAX_Hold_PurchaseUnits", typeof(double));  // 10
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Hold_BalanceAmt", typeof(double)); //11 done
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Hold_TotalPL", typeof(double)); //12 done
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Hold_EligibleSTCG", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Hold_EligibleLTCG", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Realized_TotalPL", typeof(double));  //13
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Realized_AcqCost", typeof(double));  //14
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Realized_STCG", typeof(double));
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Realized_LTCG", typeof(double));

            dtMFNetPosition.Columns.Add("CMFNP_CreatedBy", typeof(Int32));
            dtMFNetPosition.Columns.Add("CMFNP_CreatedOn", typeof(DateTime));
            dtMFNetPosition.Columns.Add("CMFNP_ModifiedOn", typeof(DateTime));
            dtMFNetPosition.Columns.Add("CMFNP_ModifiedBy", typeof(Int32));



            return dtMFNetPosition;

        }

        public DataTable CreateMFNetPositionDataTable(DataSet dsMFTransactionBalanceAndSellPair, DateTime valuationDate)
        {
            DataTable dtMFNetPosition = new DataTable();
            DataTable dtMFTransactionBalance = new DataTable();
            DataTable dtMFTransactionSellPair = new DataTable();
            dtMFTransactionBalance = dsMFTransactionBalanceAndSellPair.Tables[0];
            dtMFTransactionSellPair = dsMFTransactionBalanceAndSellPair.Tables[1];
            dtMFNetPosition = CreateNetpositionTable();
            DataRow drMFNetPosition;
            drMFNetPosition = dtMFNetPosition.NewRow();

            double returnInvestedCost = 0;
            double reedemedAmount = 0;
            double openUnits = 0;
            double totalDiv = 0;
            double returnPurchaseUnits = 0;
            double LTCG = 0;
            double STCG = 0;
            double eligibleLTCG = 0;
            double eligibleSTCG = 0;
            double currentValue = 0;
            double CMFNP_RET_ALL_TotalPL = 0;
            double avgCost = 0;
            double CMFNP_RET_Hold_AcqCost = 0;
            double returnHoldingTotalPL = 0;
            double returnRealisedInvestedCost = 0;
            double returnRealisedTotalPL = 0;
            double DVPAmount = 0;
            double DVRAmount = 0;
            double CMFTB_DivPayout = 0;
            double CMFNP_RET_Realized_DVPAmt = 0;
            double sellUnits = 0;
            double[] currentValueXIRR;
            DateTime[] tranDateXIRR;
            double CMFNP_TAX_Realized_AcqCost = 0;

            currentValueXIRR = new double[dtMFTransactionBalance.Rows.Count + 1];
            tranDateXIRR = new DateTime[dtMFTransactionBalance.Rows.Count + 1];


            try
            {

                if (dtMFTransactionBalance.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow drTransactionBalance in dtMFTransactionBalance.Rows)
                    {
                        currentValueXIRR[i] = double.Parse(drTransactionBalance["XIRR_ALL"].ToString());
                        tranDateXIRR[i] = DateTime.Parse(drTransactionBalance["CMFT_TransactionDate"].ToString());
                        i++;

                    }
                }

                if (dtMFTransactionBalance.Rows.Count > 0)
                {

                    drMFNetPosition["CMFNP_MarketPrice"] = dtMFTransactionBalance.Rows[0]["NAV"];
                    drMFNetPosition["CMFNP_RET_Hold_XIRR"] = 0; //Still Confusion
                    drMFNetPosition["CMFNP_RET_Realized_XIRR"] = 0; // Still Confusion
                    //drMFNetPosition["CMFNP_RET_ALL_TotalXIRR"] = 0;  // XIRR 
                    drMFNetPosition["CMFNP_ValuationDate"] = valuationDate;

                    drMFNetPosition["CMFNP_RET_Realized_DVRAmt"] = 0;  // DVR will be always zero for Realised

                    drMFNetPosition["PASP_SchemePlanCode"] = Convert.ToDouble(Convert.ToString(dtMFTransactionBalance.Rows[0]["PASP_SchemePlanCode"]));
                    drMFNetPosition["CMFA_AccountId"] = Convert.ToDouble(Convert.ToString(dtMFTransactionBalance.Rows[0]["CMFA_AccountId"]));

                    object sumObject;

                    sumObject = dtMFTransactionBalance.Compute("Sum([InvestedCostReturnHolding])", string.Empty);
                    double.TryParse(Convert.ToString(sumObject), out CMFNP_RET_Hold_AcqCost);   

                    sumObject = dtMFTransactionBalance.Compute("Sum([CMFT_Amount])", "WMTT_TransactionClassificationCode = 'BUY'");
                    double.TryParse(Convert.ToString(sumObject), out returnInvestedCost);

                    drMFNetPosition["CMFNP_InvestedCost"] = returnInvestedCost;
                    if (dtMFTransactionSellPair.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtMFTransactionSellPair.Rows)
                        {
                            DataRow[] drTransaction = dtMFTransactionBalance.Select("CMFT_MFTransId=" + dr["CMFSP_BuyID"].ToString());
                            CMFNP_TAX_Realized_AcqCost += (double.Parse(drTransaction[0]["CMFT_Price"].ToString()) * double.Parse(dr["CMFSP_Units"].ToString()));
                        }
                    }

                    drMFNetPosition["CMFNP_TAX_Realized_AcqCost"] = CMFNP_TAX_Realized_AcqCost;

                    sumObject = dtMFTransactionBalance.Compute("Sum(CMFT_Amount)", "WMTT_TransactionClassificationCode = 'SEL'");
                    double.TryParse(Convert.ToString(sumObject), out reedemedAmount);

                    drMFNetPosition["CMFNP_RedeemedAmount"] = reedemedAmount;
                    drMFNetPosition["CMFNP_TAX_Realized_TotalPL"] = reedemedAmount - CMFNP_TAX_Realized_AcqCost;
                    sumObject = dtMFTransactionBalance.Compute("Sum(CMFTB_UnitBalanceTAX)", string.Empty);
                    double.TryParse(Convert.ToString(sumObject), out openUnits);

                    drMFNetPosition["CMFNP_NetHoldings"] = openUnits;

                    sumObject = dtMFTransactionSellPair.Compute("Sum(CMFSP_Units)", string.Empty);
                    double.TryParse(Convert.ToString(sumObject), out sellUnits);

                    drMFNetPosition["CMFNP_SalesQuantity"] = sellUnits;

                    sumObject = dtMFTransactionBalance.Compute("Sum(CMFTB_TotalCostBalanceTAX)", string.Empty);
                    double.TryParse(Convert.ToString(sumObject), out totalDiv);

                    drMFNetPosition["CMFNP_TAX_Hold_BalanceAmt"] = totalDiv;
                    if (Math.Round(openUnits, 1) != 0)
                    {
                        currentValue = (!string.IsNullOrEmpty(Convert.ToString(dtMFTransactionBalance.Rows[0]["NAV"])) ? (Convert.ToDouble(dtMFTransactionBalance.Rows[0]["NAV"].ToString()) * openUnits) : returnInvestedCost);
                    }
                    else
                        currentValue = 0;
                    //*********************************XIRR****************************************
                    currentValueXIRR[(currentValueXIRR.Count()) - 1] = currentValue;
                    tranDateXIRR[(currentValueXIRR.Count()) - 1] = valuationDate;

                    drMFNetPosition["CMFNP_RET_ALL_TotalXIRR"] = CalculateXIRR(currentValueXIRR, tranDateXIRR);

                    drMFNetPosition["CMFNP_TAX_Hold_TotalPL"] = currentValue - totalDiv;

                    sumObject = dtMFTransactionBalance.Compute("Sum(CMFT_Amount)", "WMTT_TransactionClassificationCode = 'DVP'");
                    double.TryParse(Convert.ToString(sumObject), out DVPAmount);

                    drMFNetPosition["CMFNP_RET_ALL_DVPAmt"] = DVPAmount;

                    sumObject = dtMFTransactionBalance.Compute("Sum(CMFT_Amount)", "WMTT_TransactionClassificationCode = 'DVR'");
                    double.TryParse(Convert.ToString(sumObject), out DVRAmount);

                    drMFNetPosition["CMFNP_RET_ALL_DVRAmt"] = DVRAmount;

                    sumObject = dtMFTransactionBalance.Compute("Sum(CMFTB_DivPayout)", string.Empty);
                    double.TryParse(Convert.ToString(sumObject), out CMFTB_DivPayout);

                    drMFNetPosition["CMFNP_RET_Hold_DVPAmt"] = CMFTB_DivPayout;

                    CMFNP_RET_Realized_DVPAmt = DVPAmount - CMFTB_DivPayout;
                    drMFNetPosition["CMFNP_RET_Realized_DVPAmt"] = CMFNP_RET_Realized_DVPAmt;


                    drMFNetPosition["CMFNP_CurrentValue"] = currentValue;

                    CMFNP_RET_ALL_TotalPL = currentValue + reedemedAmount + DVPAmount - returnInvestedCost;
                    drMFNetPosition["CMFNP_RET_ALL_TotalPL"] = CMFNP_RET_ALL_TotalPL;

                    if (returnInvestedCost != 0)
                    {
                        drMFNetPosition["CMFNP_RET_ALL_AbsReturn"] = (CMFNP_RET_ALL_TotalPL / returnInvestedCost) * 100;
                    }
                    //valuationVo.ReturnAllTotalDiv = totalDiv;

                    //expression = "WMTT_TransactionClassificationCode <> 'SEL' and WMTT_TransactionClassificationCode <> 'DVP'";
                    //dtMFTransactionBalance.DefaultView.RowFilter = expression;
                    //DataTable dtProratedDetails = dtMFTransactionBalance.DefaultView.ToTable();
                    //DataRow lastRow = (DataRow)dtProratedDetails.Rows[dtProratedDetails.Rows.Count - 1];

                    //avgCost = Convert.ToDouble(lastRow["CMFTB_AvgCostBalRETURN"]);

                    //CMFNP_RET_Hold_AcqCost = openUnits * avgCost;
                    drMFNetPosition["CMFNP_RET_Hold_AcqCost"] = CMFNP_RET_Hold_AcqCost;  //  Return Holding   ----  Invested Cost
                    returnHoldingTotalPL = currentValue + CMFTB_DivPayout - CMFNP_RET_Hold_AcqCost;

                    drMFNetPosition["CMFNP_RET_Hold_TotalPL"] = returnHoldingTotalPL;

                    if (CMFNP_RET_Hold_AcqCost != 0)
                    {
                        drMFNetPosition["CMFNP_RET_Hold_AbsReturn"] = (returnHoldingTotalPL / CMFNP_RET_Hold_AcqCost) * 100;
                    }
                    returnRealisedInvestedCost = returnInvestedCost - CMFNP_RET_Hold_AcqCost;

                    drMFNetPosition["CMFNP_RET_Realized_InvestedCost"] = returnRealisedInvestedCost;

                    returnRealisedTotalPL = reedemedAmount + CMFNP_RET_Realized_DVPAmt - returnRealisedInvestedCost;
                    drMFNetPosition["CMFNP_RET_Realized_TotalPL"] = returnRealisedTotalPL;

                    if (returnRealisedInvestedCost != 0)
                    {
                        drMFNetPosition["CMFNP_RET_Realized_AbsReturn"] = (returnRealisedTotalPL / returnRealisedInvestedCost) * 100;
                    }

                    //Need to do some work in below code :-

                    //valuationVo.TaxHoldingAcqCost = totalDiv;
                    //valuationVo.TaxHoldingCurrentValue = valuationVo.ReturnOpenUnits * valuationVo.ReturnCurrentNAV;
                    //valuationVo.TaxHoldingUnrealisedPL = valuationVo.TaxHoldingCurrentValue - valuationVo.TaxHoldingAcqCost;

                    sumObject = dtMFTransactionBalance.Compute("Sum([CMFTB_UnitBalanceRETURN])", "WMTT_TransactionClassificationCode = 'BUY'");
                    double.TryParse(Convert.ToString(sumObject), out returnPurchaseUnits);

                    drMFNetPosition["CMFNP_RET_Hold_PurchaseUnit"] = returnPurchaseUnits;   // confusion  // Resolved
                    drMFNetPosition["CMFNP_RET_Hold_DVRUnits"] = openUnits - returnPurchaseUnits;

                    sumObject = dtMFTransactionBalance.Compute("Sum(EligibleLTG)", "CMFTB_UnitBalanceTAX > 0");
                    double.TryParse(Convert.ToString(sumObject), out eligibleLTCG);

                    sumObject = dtMFTransactionBalance.Compute("Sum(EligibleSTG)", "CMFTB_UnitBalanceTAX > 0");
                    double.TryParse(Convert.ToString(sumObject), out eligibleSTCG);

                    drMFNetPosition["CMFNP_TAX_Hold_EligibleLTCG"] = eligibleLTCG;
                    drMFNetPosition["CMFNP_TAX_Hold_EligibleSTCG"] = eligibleSTCG;

                    sumObject = dtMFTransactionSellPair.Compute("Sum(LTG)", string.Empty);
                    double.TryParse(Convert.ToString(sumObject), out LTCG);

                    sumObject = dtMFTransactionSellPair.Compute("Sum(STG)", string.Empty);
                    double.TryParse(Convert.ToString(sumObject), out STCG);

                    drMFNetPosition["CMFNP_TAX_Realized_LTCG"] = LTCG;
                    drMFNetPosition["CMFNP_TAX_Realized_STCG"] = STCG;

                    drMFNetPosition["CMFNP_CreatedBy"] = 1000;
                    drMFNetPosition["CMFNP_CreatedOn"] = DateTime.Now;
                    drMFNetPosition["CMFNP_ModifiedOn"] = DateTime.Now;
                    drMFNetPosition["CMFNP_ModifiedBy"] = 1000;


                    dtMFNetPosition.Rows.Add(drMFNetPosition);
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

                FunctionInfo.Add("Method", "MFEngineBo.cs:CreateMFNetPositionDataTable()");

                object[] objects = new object[3];
                objects[0] = dsMFTransactionBalanceAndSellPair;
                objects[1] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtMFNetPosition;

        }

        public double CalculateXIRR(System.Collections.Generic.IEnumerable<double> values, System.Collections.Generic.IEnumerable<DateTime> date)
        {

            double result = 0;
            try
            {
                result = Financial.XIrr(values, date);
                //This 'if' loop is a temporary fix for the error where calculation is done for XIRR instead of average
                if (Convert.ToInt64(result).ToString().Length > 3 || result.ToString().Contains("E") || result.ToString().Contains("e"))
                {
                    result = 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                string e = ex.ToString();
                return result;
            }

        }






    }






}
