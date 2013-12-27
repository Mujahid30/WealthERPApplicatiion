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


//using VoValuation;

namespace BoValuation
{
    public class MFEngineBo
    {

        MFEngineDao mfEngineDao = new MFEngineDao();
        EmailSMSBo emailSMSBo = new EmailSMSBo(); 
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

        DataTable dtCustomerMFTransactionBalanceForNP = new DataTable();
        DataTable dtCustomerMFTransactionSellPairedForNP = new DataTable();
        DataTable dtAdviserMFNetPosition = new DataTable();
        int adviserId = 0;


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
                                    //if (customerId == 85747)
                                    //{

                                    //}
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
                                mfEngineDao.CreateCustomerMFTransactionBalance(dsCustomerMFTransBalanceSellPaired,commonId);

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
                            DataSet dsMFTransactionProcessedBalance = new DataSet();

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

                                    //if (commonId == 231118 && schemePlanCode == 31914)
                                    //{

                                    //}
                                    if (dsTransactionBalanceReadyToProcess.Tables["Transaction"].Rows.Count > 0)
                                        dsMFTransactionProcessedBalance = TransactionBalanceProcess(dsTransactionBalanceReadyToProcess);
                                    if (dsMFTransactionProcessedBalance.Tables.Count > 0)
                                    {
                                        if (dsMFTransactionProcessedBalance.Tables[0] != null)
                                            dtMFTransactionProcessedBalance = dsMFTransactionProcessedBalance.Tables[0];
                                    }
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
                emailSMSBo.SendErrorExceptionMail(commonId, startFrom.ToString(),schemePlanCode, Ex.Message, "MFEngineBo.cs_MFBalanceCreation");
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
            dtSellPaired.Columns.Add("CMFT_TransactionDate", typeof(DateTime));
            dtSellPaired.Columns.Add("CMFT_LTG", typeof(double));
            dtSellPaired.Columns.Add("CMFT_STG", typeof(double));
            dtSellPaired.Columns.Add("CMFT_Gain_loss_Value", typeof(double));
            dtSellPaired.Columns.Add("CMFT_Units", typeof(double));
            dtSellPaired.Columns.Add("CMFT_PriceBuy", typeof(double));
            dtSellPaired.Columns.Add("CMFT_Action", typeof(Int16));

            return dtSellPaired;
        }


        #region BalanceCreation

        public DataTable CreateTransactionBalanceTable()
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
            //dtTransactionDetails.Columns.Add("CMFNP_RET_Hold_DVRAmounts", typeof(System.Decimal));
            dtTransactionDetails.Columns.Add("CMFTB_InsertUpdate_Flag", typeof(System.Int32));

            return dtTransactionDetails;
        }

        public DataSet TransactionBalanceProcess(DataSet dsTransactionList)
        {
            DataSet dsBalancedSellPairedDetails = new DataSet();
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
                                drTransactionDetails["CMFA_AccountId"] = dr["CMFA_AccountId"].ToString();
                                drTransactionDetails["PASP_SchemePlanCode"] = dr["PASP_SchemePlanCode"].ToString();
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
                                drTransactionDetails["CMFTB_TotalCostBalanceTAX"] = Math.Round((double.Parse(drTransactionDetails["CMFTB_UnitBalanceTAX"].ToString()) * double.Parse(dr["CMFT_Price"].ToString())), 4);
                                drTransactionDetails["CMFTB_UnitBalanceRETURN"] = dr["CMFT_Units"].ToString();
                                drTransactionDetails["CMFTB_DivPayout"] = 0;
                                drTransactionDetails["CMFTB_AvgCostBalRETURN"] = dr["CMFT_Price"].ToString();
                                drTransactionDetails["CMFTB_TotalCostBalRETURN"] = dr["CMFT_Amount"].ToString();
                                drTransactionDetails["CMFTB_DVRUnitsAllocation_Share"] = 0;
                                drTransactionDetails["CMFTB_DVRUnits_Contributed"] = 0;
                                drTransactionDetails["CMFTB_InsertUpdate_Flag"] = 1;

                                //drTransactionDetails["CMFNP_RET_Hold_DVRAmounts"] = 0;

                                dtTransactionDetails.Rows.Add(drTransactionDetails);
                                dtTransactionDetailsTemp = dtTransactionDetails.Copy();

                                break;

                            case "DVR":
                            case "BNS":
                                double dvrUnits = 0;
                               
                                drTransactionDetails["CMFT_MFTransId"] = dr["CMFT_MFTransId"].ToString();
                                drTransactionDetails["CMFA_AccountId"] = dr["CMFA_AccountId"].ToString();
                                drTransactionDetails["PASP_SchemePlanCode"] = dr["PASP_SchemePlanCode"].ToString();
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
                                if (transactionType == "DVR")
                                    drTransactionDetails["CMFTB_TotalCostBalanceTAX"] = Math.Round((double.Parse(drTransactionDetails["CMFTB_UnitBalanceTAX"].ToString()) * double.Parse(dr["CMFT_Price"].ToString())), 4);
                                else
                                    drTransactionDetails["CMFTB_TotalCostBalanceTAX"] = 0;
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
                                    double unitBalanceReturnOldSum = 0.0;

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

                                                    dr3["CMFTB_DVRUnitsAllocation_Share"] = Math.Round(unitBalanceReturnOldSum, 4);

                                                    dr3["CMFTB_DVRUnits_Contributed"] = Math.Round(dvrUnitsContribution, 4);

                                                    //added by gobinda
                                                    if (avgCostReturn.ToString() != "Infinity")
                                                    {
                                                        if (avgCostReturn.ToString() != "-Infinity")
                                                        {
                                                            dr3["CMFTB_AvgCostBalRETURN"] = Math.Round(avgCostReturn, 4);
                                                            dr3["CMFTB_TotalCostBalRETURN"] = Math.Round(avgCostReturn * double.Parse(dr3["CMFTB_UnitBalanceRETURN"].ToString()), 4);

                                                        }
                                                    }

                                                    //dr3["CMFTB_AvgCostBalRETURN"] = Math.Round(avgCostReturn, 4);
                                                    //dr3["CMFTB_TotalCostBalRETURN"] = Math.Round(avgCostReturn * double.Parse(dr3["CMFTB_UnitBalanceRETURN"].ToString()), 4);
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
                                                    dr3["CMFTB_TotalCostBalRETURN"] = Math.Round(avlCostDVR, 4);
                                                    if (dvrUnits != 0)
                                                        dr3["CMFTB_AvgCostBalRETURN"] = Math.Round((avlCostDVR / dvrUnits), 4);
                                                    else
                                                        dr3["CMFTB_AvgCostBalRETURN"] = 0;
                                                    dr3["CMFTB_DVRUnitsAllocation_Share"] = 0;
                                                    dr3["CMFTB_DVRUnits_Contributed"] = 0;
                                                }
                                                // drTransactionDetails["CMFNP_RET_Hold_DVRAmounts"] = Math.Round((double.Parse(drTransactionDetails["CMFTB_UnitBalanceRETURN"].ToString()) * double.Parse(dr["CMFT_Price"].ToString())), 4);
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
                                drTransactionDetails["CMFA_AccountId"] = dr["CMFA_AccountId"].ToString();
                                drTransactionDetails["PASP_SchemePlanCode"] = dr["PASP_SchemePlanCode"].ToString();
                                drTransactionDetails["WMTT_TransactionClassificationCode"] = dr["WMTT_TransactionClassificationCode"].ToString();
                                drTransactionDetails["CMFT_TransactionDate"] = Convert.ToDateTime(dr["CMFT_TransactionDate"].ToString());
                                drTransactionDetails["CMFT_Price"] = dr["CMFT_Price"];
                                drTransactionDetails["CMFT_Units"] = dr["CMFT_Units"].ToString();
                                drTransactionDetails["CMFT_Amount"] = dr["CMFT_Amount"].ToString();
                                drTransactionDetails["CMFT_STT"] = dr["CMFT_STT"].ToString();
                                drTransactionDetails["WTS_TransactionStatusCode"] = Convert.ToInt16(dr["WTS_TransactionStatusCode"].ToString());
                                span = DateTime.Today - DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                                drTransactionDetails["CMFTB_Age"] = span.TotalDays;
                                drTransactionDetails["CMFTB_UnitBalanceTAX"] = dr["CMFT_Units"].ToString();
                                drTransactionDetails["CMFTB_TotalCostBalanceTAX"] = Math.Round(double.Parse(drTransactionDetails["CMFTB_UnitBalanceTAX"].ToString()) * double.Parse(dr["CMFT_Price"].ToString()), 4);
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
                                            if (dr1["WMTT_TransactionClassificationCode"].ToString() == "DVR" || dr1["WMTT_TransactionClassificationCode"].ToString() == "BUY" || dr1["WMTT_TransactionClassificationCode"].ToString() == "BNS")
                                                dr1["CMFTB_DivPayout"] = Math.Round((double.Parse(dr1["CMFTB_DivPayout"].ToString()) + (double.Parse(dr1["CMFTB_UnitBalanceRETURN"].ToString()) / sum) * amount), 4);
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
                                drTransactionDetails["CMFA_AccountId"] = dr["CMFA_AccountId"].ToString();
                                drTransactionDetails["PASP_SchemePlanCode"] = dr["PASP_SchemePlanCode"].ToString();
                                drTransactionDetails["WMTT_TransactionClassificationCode"] = dr["WMTT_TransactionClassificationCode"].ToString();
                                drTransactionDetails["CMFT_TransactionDate"] = DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
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
                                //dtTransactionDetails.DefaultView.RowFilter = expression;
                                //drTransactionDetails["CMFTB_UnitBalanceRETURN"] = 0;
                               // dtDefaultView = dtTransactionDetails.DefaultView.ToTable();
                               
                                // dtDefaultView.PrimaryKey = new DataColumn[] { dtDefaultView.Columns["CMFT_MFTransId"] };                               
                                dtmodifiedDetails = GetTransactiondetailsAfterSell(dtTransactionDetails, double.Parse(dr["CMFT_Units"].ToString()), double.Parse(dr["CMFT_Price"].ToString()), int.Parse(dr["CMFT_MFTransId"].ToString()), DateTime.Parse(dr["CMFT_TransactionDate"].ToString()));
                                dtmodifiedDetails.PrimaryKey = new DataColumn[] { dtmodifiedDetails.Columns["CMFT_MFTransId"] };
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
            dsBalancedSellPairedDetails.Tables.Add(dtTransactionDetails);
            dsBalancedSellPairedDetails.Tables.Add(dtMFTrasactionSellPair);
            return dsBalancedSellPairedDetails;
        }

        protected DataTable GetTransactiondetailsAfterSell(DataTable dt, double sellUnits, double sellPrice, int sellId, DateTime sellTransactiondate)
        {
            //DataTable dtSellPaired = new DataTable();          
            TimeSpan span = new TimeSpan();
            double buyUnits = 0;
            double buyReturnUnits = 0;
            double age;
            //  double avgValue=0;
            double units = sellUnits;
            double returnSellUnits = sellUnits;
            double buyCount = 0;
            double totalCount = 0;
            double count = 0;
            object sumObject = dt.Compute("Count([CMFT_MFTransId])", "WMTT_TransactionClassificationCode = 'BUY'");
            double.TryParse(Convert.ToString(sumObject), out buyCount);
            sumObject = dt.Compute("Count([CMFT_MFTransId])", string.Empty);
            double.TryParse(Convert.ToString(sumObject), out totalCount);

            string expression1 = "WMTT_TransactionClassificationCode <> 'SEL' and WMTT_TransactionClassificationCode <> 'DVP'";
            dt.DefaultView.RowFilter = expression1;
            DataTable dtProratedDetails = dt.DefaultView.ToTable();
            DataRow lastRow = dtProratedDetails.NewRow();
             Int64 mfTransId=0;
            if (dtProratedDetails.Rows.Count > 0)
            {
                lastRow = (DataRow)dtProratedDetails.Rows[dtProratedDetails.Rows.Count - 1];

                mfTransId = Convert.ToInt64(lastRow["CMFT_MFTransId"]);
            }
            foreach (DataRow dr in dt.Rows)
            {
                count++;
                if (dr["WMTT_TransactionClassificationCode"].ToString() != "SEL" && dr["WMTT_TransactionClassificationCode"].ToString() != "DVP")
                {
                    //avgValue = double.Parse(dr["CMFTB_AvgCostBalRETURN"].ToString());

                    // if CMFTB_Id is null then this will get insert in Database with flag 1
                    // else It will update with flag 2
                    // 0 for No change
                    //if (dr["CMFTB_Id"].ToString() != null && dr["CMFTB_Id"].ToString() != "")
                    //{
                    //    dr["CMFTB_InsertUpdate_Flag"] = 2;
                    //}

                    //----------------->>>> Tax FIFO <<<<-----------------\\


                    buyUnits = double.Parse(dr["CMFTB_UnitBalanceTAX"].ToString());
                    if (buyUnits != 0)
                    {
                        if (buyUnits == sellUnits)
                        {
                            dr["CMFTB_UnitBalanceTAX"] = 0;
                            dr["CMFTB_TotalCostBalanceTAX"] = 0;
                            span = sellTransactiondate - DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                            age = span.TotalDays;
                            FillSellPairedDataSet(sellUnits, age, sellId, double.Parse(dr["CMFT_MFTransId"].ToString()), sellPrice, double.Parse(dr["CMFT_Price"].ToString()), DateTime.Parse(dr["CMFT_TransactionDate"].ToString()));
                            break;
                        }
                        else if (buyUnits > sellUnits)
                        {
                            dr["CMFTB_UnitBalanceTAX"] = buyUnits - sellUnits;
                            dr["CMFTB_TotalCostBalanceTAX"] = double.Parse(dr["CMFTB_UnitBalanceTAX"].ToString()) * double.Parse(dr["CMFT_Price"].ToString());
                            span = sellTransactiondate - DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                            age = span.TotalDays;
                            FillSellPairedDataSet(sellUnits, age, sellId, double.Parse(dr["CMFT_MFTransId"].ToString()), sellPrice, double.Parse(dr["CMFT_Price"].ToString()), DateTime.Parse(dr["CMFT_TransactionDate"].ToString()));

                            break;
                        }
                        else if (buyUnits < sellUnits)
                        {
                            if (Int64.Parse(dr["CMFT_MFTransId"].ToString()) == mfTransId)
                            {
                                dr["CMFTB_UnitBalanceTAX"] = buyUnits - sellUnits;  
                            }
                            else
                            {
                                dr["CMFTB_UnitBalanceTAX"] = 0;                                                        
                            }

                            dr["CMFTB_TotalCostBalanceTAX"] = double.Parse(dr["CMFTB_UnitBalanceTAX"].ToString()) * double.Parse(dr["CMFT_Price"].ToString());
                            sellUnits = sellUnits - buyUnits;
                            span = sellTransactiondate - DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                            age = span.TotalDays;
                            FillSellPairedDataSet(buyUnits, age, sellId, double.Parse(dr["CMFT_MFTransId"].ToString()), sellPrice, double.Parse(dr["CMFT_Price"].ToString()), DateTime.Parse(dr["CMFT_TransactionDate"].ToString()));

                        }
                    }

                    //----------------->>>> Tax FIFO END<<<<-----------------\\




                    //----------------->>>> Return FIFO <<<<-----------------\\
                }

                else if(dr["WMTT_TransactionClassificationCode"].ToString() == "SEL" && buyCount < 1 )
                {
                    if(count == totalCount)
                    dr["CMFTB_UnitBalanceTAX"] = -1 * sellUnits;
                }
            }
            count = 0;
            foreach (DataRow drReturns in dt.Rows)
            {
                count++;
                if (drReturns["WMTT_TransactionClassificationCode"].ToString() != "SEL" && drReturns["WMTT_TransactionClassificationCode"].ToString() != "DVP")
                {
                  
                    buyReturnUnits = double.Parse(drReturns["CMFTB_UnitBalanceRETURN"].ToString());
                    if (buyReturnUnits != 0)
                    {
                        if (buyReturnUnits == returnSellUnits)
                        {
                            drReturns["CMFTB_DivPayout"] = 0;
                            drReturns["CMFTB_UnitBalanceRETURN"] = 0;
                            drReturns["CMFTB_TotalCostBalRETURN"] = 0;
                            break;
                        }
                        else if (buyReturnUnits > returnSellUnits)
                        {
                            drReturns["CMFTB_DivPayout"] = Math.Round((((buyReturnUnits - returnSellUnits) / double.Parse(drReturns["CMFTB_UnitBalanceRETURN"].ToString())) * double.Parse(drReturns["CMFTB_DivPayout"].ToString())), 4);
                            drReturns["CMFTB_UnitBalanceRETURN"] = buyReturnUnits - returnSellUnits;
                            drReturns["CMFTB_TotalCostBalRETURN"] = double.Parse(drReturns["CMFTB_UnitBalanceRETURN"].ToString()) * double.Parse(drReturns["CMFT_Price"].ToString());
                            break;
                        }
                        else if (buyReturnUnits < returnSellUnits)
                        {
                            drReturns["CMFTB_DivPayout"] = 0;
                            if (Int64.Parse(drReturns["CMFT_MFTransId"].ToString()) == mfTransId)
                            {
                                drReturns["CMFTB_UnitBalanceRETURN"] = buyReturnUnits - returnSellUnits;
                            }
                            else
                            {
                                drReturns["CMFTB_UnitBalanceRETURN"] = 0;
                            }

                            drReturns["CMFTB_TotalCostBalRETURN"] = double.Parse(drReturns["CMFTB_UnitBalanceRETURN"].ToString()) * double.Parse(drReturns["CMFT_Price"].ToString());
                            returnSellUnits = returnSellUnits - buyReturnUnits;
                        }
                        if (drReturns["CMFTB_Id"].ToString() != null && drReturns["CMFTB_Id"].ToString() != "")
                        {
                            drReturns["CMFTB_InsertUpdate_Flag"] = 2;
                        }
                    }
                }
                else if (drReturns["WMTT_TransactionClassificationCode"].ToString() == "SEL" && buyCount < 1)
                {
                    if (count == totalCount)
                    drReturns["CMFTB_UnitBalanceRETURN"] = -1 * sellUnits;
                }
            }



            //----------------->>>> Return FIFO End <<<<-----------------\\




            //  dt = UpdateBalancedTransactionDetails(dt, units);
            return dt;
        }

        protected void FillSellPairedDataSet(double units, double age, double sellId, double buyId, double sellPrice, double buyPrice, DateTime transactionDate)
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
            drSellPaired["CMFT_TransactionDate"] = transactionDate;
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
                    drSellPaired["CMFT_LTG"] = Math.Round(gainLossValue, 4);
                }
                else
                {
                    drSellPaired["CMFT_STG"] = Math.Round(gainLossValue, 4);
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
                    dr1["CMFTB_UnitBalanceRETURN"] = Math.Round((double.Parse(dr1["CMFTB_UnitBalanceRETURN"].ToString()) - (double.Parse(dr1["CMFTB_UnitBalanceRETURN"].ToString()) / sum) * units), 10);

                    // if CMFTB_Id is null then this will get insert in Database with flag 1
                    // else It will update with flag 2
                    // 0 for No change
                    if (dr1["CMFTB_Id"].ToString() != null && dr1["CMFTB_Id"].ToString() != "")
                    {
                        dr1["CMFTB_InsertUpdate_Flag"] = 2;
                    }
                    if (dr1["WMTT_TransactionClassificationCode"].ToString() == "BUY" || dr1["WMTT_TransactionClassificationCode"].ToString() == "DVR")
                    {
                        dr1["CMFTB_DivPayout"] = Math.Round(((double.Parse(dr1["CMFTB_UnitBalanceRETURN"].ToString()) / unitBalancereturnOld) * double.Parse(dr1["CMFTB_DivPayout"].ToString())), 4);
                        dr1["CMFTB_TotalCostBalRETURN"] = Math.Round((double.Parse(dr1["CMFTB_AvgCostBalRETURN"].ToString()) * double.Parse(dr1["CMFTB_UnitBalanceRETURN"].ToString())), 4);

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
                            adviserId = commonId;
                            dtCustomerMutualFundNetPosition = CreateNetpositionTable();

                            AdviserCustomers = mfEngineDao.GetAdviserCustomerList_MF(commonId);
                            if (AdviserCustomers != null)
                            {
                                dtAdviserMFNetPosition = CreateNetpositionTable();
                                foreach (int customerId in AdviserCustomers)
                                {

                                    MFNetPositionCreation(customerId, 0, ValuationLabel.Customer, valuationDate);
                                    if (dtCustomerMutualFundNetPosition.Rows.Count > 0)
                                    {
                                        dtAdviserMFNetPosition.Merge(dtCustomerMutualFundNetPosition);
                                        //mfEngineDao.CreateCustomerMFNetPosition(customerId, valuationDate, dtCustomerMutualFundNetPosition);
                                    }
                                    dtCustomerMutualFundNetPosition.Clear();

                                }
                                
                                mfEngineDao.CreateAdviserMFNetPosition(adviserId, valuationDate, dtAdviserMFNetPosition, 1);
                                //DataSet ds = new DataSet();
                                //ds.WriteXml(Server..MapPath("UploadFiles") + "\\" + processlogVo.ProcessId + ".xml", XmlWriteMode.WriteSchema);

                                dtAdviserMFNetPosition.Clear();
                            }
                            break;
                        }
                    case "Customer":
                        {
                            DataSet dsCustomerMFPortfolioAccountDetails = new DataSet();
                            dsCustomerMFPortfolioAccountDetails = mfEngineDao.GetCustomerAllMFPortfolioAccountForValution(commonId);
                            DataSet dsCustomerMFTransactionBalanceAndSellPair = mfEngineDao.GetCustomerMFTransactionBalanceAndSellPair(commonId, valuationDate);
                            dtCustomerPortfolio = dsCustomerMFPortfolioAccountDetails.Tables[0];
                            dtCustomerAccount = dsCustomerMFPortfolioAccountDetails.Tables[1];
                            dtCustomerMFTransactionSellPaired = CreateSellPairedTable();

                            dtCustomerMFTransactionBalanceForNP = dsCustomerMFTransactionBalanceAndSellPair.Tables[0];
                            dtCustomerMFTransactionSellPairedForNP = dsCustomerMFTransactionBalanceAndSellPair.Tables[1];

                            if (dtCustomerPortfolio.Rows.Count > 0)
                            {
                                foreach (DataRow drProftfolio in dtCustomerPortfolio.Rows)
                                {
                                    MFNetPositionCreation(Convert.ToInt32(drProftfolio["CP_PortfolioId"].ToString()), 0, ValuationLabel.Portfolio, valuationDate);

                                }

                            }
                            dtCustomerMFTransactionBalanceForNP.Clear();
                            dtCustomerMFTransactionSellPairedForNP.Clear();

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
                            //DataTable dtMFTransactionBalance = new DataTable();
                            //DataTable dtMFTransactionSellPaired= new DataTable();

                            //dsMFTransactionBalanceAndSellPair = mfEngineDao.GetMFTransactionBalanceAndSellPairAccountSchemeWise(commonId, schemePlanCode, valuationDate);

                            dtCustomerMFTransactionBalanceForNP.DefaultView.RowFilter = "CMFA_AccountId=" + commonId.ToString() + " AND " + "PASP_SchemePlanCode=" + schemePlanCode.ToString();
                            dtCustomerMFTransactionBalanceForNP.DefaultView.RowFilter = "CMFA_AccountId=" + commonId.ToString() + " AND " + "PASP_SchemePlanCode=" + schemePlanCode.ToString();

                            dtCustomerMFTransactionSellPairedForNP.DefaultView.RowFilter = "CMFA_AccountId=" + commonId.ToString() + " AND " + "PASP_SchemePlanCode=" + schemePlanCode.ToString();
                            dtCustomerMFTransactionSellPairedForNP.DefaultView.RowFilter = "CMFA_AccountId=" + commonId.ToString() + " AND " + "PASP_SchemePlanCode=" + schemePlanCode.ToString();


                            dsMFTransactionBalanceAndSellPair.Tables.Add(dtCustomerMFTransactionBalanceForNP.DefaultView.ToTable());
                            dsMFTransactionBalanceAndSellPair.Tables.Add(dtCustomerMFTransactionSellPairedForNP.DefaultView.ToTable());

                            if (commonId == 568357 & schemePlanCode == 3255)
                            {

                            }
                            dtMFAccountSchemeNetPosition = CreateNetpositionTable();
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
                emailSMSBo.SendErrorExceptionMail(commonId, startFrom.ToString(), schemePlanCode, Ex.Message, "MFEngineBo.cs_MFNetPositionCreation");
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
            dtMFNetPosition.Columns.Add("CMFNP_MarketPrice", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_ValuationDate", typeof(DateTime));
            dtMFNetPosition.Columns.Add("CMFNP_NetHoldings", typeof(decimal));

            dtMFNetPosition.Columns.Add("CMFNP_SalesQuantity", typeof(decimal));  //1 done
            dtMFNetPosition.Columns.Add("CMFNP_RedeemedAmount", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_InvestedCost", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_CurrentValue", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_RET_ALL_TotalPL", typeof(decimal));

            dtMFNetPosition.Columns.Add("CMFNP_RET_ALL_AbsReturn", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_RET_ALL_TotalXIRR", typeof(decimal));  //2 nt req
            dtMFNetPosition.Columns.Add("CMFNP_RET_ALL_DVRAmt", typeof(decimal));   //3  done

            dtMFNetPosition.Columns.Add("CMFNP_RET_ALL_DVPAmt", typeof(decimal));   //4  done
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_AcqCost", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_TotalPL", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_AbsReturn", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_PurchaseUnit", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_DVRUnits", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_DVPAmt", typeof(decimal));  //5 done
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_XIRR", typeof(decimal));  //6  nt req


            dtMFNetPosition.Columns.Add("CMFNP_RET_Realized_InvestedCost", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Realized_DVPAmt", typeof(decimal)); //7 done
            dtMFNetPosition.Columns.Add("CMFNP_RET_Realized_DVRAmt", typeof(decimal));  //8   
            dtMFNetPosition.Columns.Add("CMFNP_RET_Realized_TotalPL", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Realized_AbsReturn", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Realized_XIRR", typeof(decimal));  //9  nt req


            dtMFNetPosition.Columns.Add("CMFNP_TAX_Hold_PurchaseUnits", typeof(decimal));  // 10
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Hold_BalanceAmt", typeof(decimal)); //11 done
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Hold_TotalPL", typeof(decimal)); //12 done
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Hold_EligibleSTCG", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Hold_EligibleLTCG", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Realized_TotalPL", typeof(decimal));  //13
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Realized_AcqCost", typeof(decimal));  //14
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Realized_STCG", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_TAX_Realized_LTCG", typeof(decimal));

            dtMFNetPosition.Columns.Add("CMFNP_CreatedBy", typeof(Int32));
            dtMFNetPosition.Columns.Add("CMFNP_CreatedOn", typeof(DateTime));
            dtMFNetPosition.Columns.Add("CMFNP_ModifiedOn", typeof(DateTime));
            dtMFNetPosition.Columns.Add("CMFNP_ModifiedBy", typeof(Int32));

            dtMFNetPosition.Columns.Add("CMFNP_InvestmentStartDate", typeof(DateTime));

            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_DVRAmounts", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_NAVDate", typeof(DateTime));

            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_AnnualisedReturns", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_WeightageNAV", typeof(decimal));
            dtMFNetPosition.Columns.Add("CMFNP_RET_Hold_WeightageDays", typeof(decimal));

            //dtMFNetPosition.Columns.Add("CMFT_Price", typeof(decimal));

            //DataColumn CMFNP_RET_Hold_DVRAmounts = new DataColumn();
            //CMFNP_RET_Hold_DVRAmounts.DataType = System.Type.GetType("System.Decimal");
            //CMFNP_RET_Hold_DVRAmounts.ColumnName = "CMFNP_RET_Hold_DVRAmounts";
            //CMFNP_RET_Hold_DVRAmounts.Expression = "price + tax";

            //dtMFNetPosition.Columns.Add(CMFNP_RET_Hold_DVRAmounts);
            return dtMFNetPosition;

        }

        public DataTable CreateMFNetPositionDataTable(DataSet dsMFTransactionBalanceAndSellPair, DateTime valuationDate)
        {
            DataTable dtMFNetPosition = new DataTable();
            DataTable dtMFTransactionBalance = new DataTable();
            DataTable dtMFTransactionSellPair = new DataTable();
            dtMFTransactionBalance = dsMFTransactionBalanceAndSellPair.Tables[0];
            dtMFTransactionSellPair = dsMFTransactionBalanceAndSellPair.Tables[1];

            dtMFTransactionBalance.Columns.Add("WeightageInvestedCost",typeof(double));
            dtMFTransactionBalance.Columns.Add("WeightageReturns", typeof(double));
            dtMFTransactionBalance.Columns.Add("WeightageNAV", typeof(double));
            dtMFTransactionBalance.Columns.Add("WeightageDays", typeof(double));

            dtMFNetPosition = CreateNetpositionTable();
            DataRow drMFNetPosition;
            drMFNetPosition = dtMFNetPosition.NewRow();


            object sumObject;
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
            double annualizedReturns = 0;
            double WeightageNAV = 0;
            double WeightageDays = 0;
            double[] currentValueXIRR;
            DateTime[] tranDateXIRR;
            double CMFNP_TAX_Realized_AcqCost = 0;
            double totalCostReturn = 0;

            currentValueXIRR = new double[dtMFTransactionBalance.Rows.Count + 1];
            tranDateXIRR = new DateTime[dtMFTransactionBalance.Rows.Count + 1];


            try
            {
                TimeSpan span = new TimeSpan();
                if (dtMFTransactionBalance.Rows.Count > 0)
                {
                    int i = 0;

                    sumObject = dtMFTransactionBalance.Compute("Sum(CMFTB_UnitBalanceTAX)", string.Empty);
                    double.TryParse(Convert.ToString(sumObject), out openUnits);

                    //common Fields for open units check   -- Start
                    drMFNetPosition["CMFNP_NetHoldings"] = openUnits;

                    drMFNetPosition["CMFNP_MarketPrice"] = dtMFTransactionBalance.Rows[0]["NAV"];

                    drMFNetPosition["CMFNP_NAVDate"] = dtMFTransactionBalance.Rows[0]["NAVDate"];

                    drMFNetPosition["CMFNP_CreatedBy"] = 1000;
                    drMFNetPosition["CMFNP_CreatedOn"] = DateTime.Now;
                    drMFNetPosition["CMFNP_ModifiedOn"] = DateTime.Now;
                    drMFNetPosition["CMFNP_ModifiedBy"] = 1000;
                    drMFNetPosition["CMFNP_ValuationDate"] = valuationDate;

                    if (drMFNetPosition["CMFNP_NAVDate"].ToString() == "" || DateTime.Parse(drMFNetPosition["CMFNP_NAVDate"].ToString()) == DateTime.MinValue)
                    {
                        drMFNetPosition["CMFNP_NAVDate"] = DBNull.Value;
                    }
                    drMFNetPosition["PASP_SchemePlanCode"] = Convert.ToDouble(Convert.ToString(dtMFTransactionBalance.Rows[0]["PASP_SchemePlanCode"]));
                    drMFNetPosition["CMFA_AccountId"] = Convert.ToDouble(Convert.ToString(dtMFTransactionBalance.Rows[0]["CMFA_AccountId"]));

                    //common Fields for open units check  -- End

                    if (openUnits >= 0)
                    {

                        sumObject = dtMFTransactionBalance.Compute("Sum(CMFTB_TotalCostBalanceTAX)", string.Empty);
                        double.TryParse(Convert.ToString(sumObject), out totalDiv);

                        sumObject = dtMFTransactionBalance.Compute("Sum(CMFTB_TotalCostBalRETURN)", string.Empty);
                        double.TryParse(Convert.ToString(sumObject), out totalCostReturn);


                        foreach (DataRow drTransactionBalance in dtMFTransactionBalance.Rows)
                        {
                            currentValueXIRR[i] = double.Parse(drTransactionBalance["XIRR_ALL"].ToString());
                            tranDateXIRR[i] = DateTime.Parse(drTransactionBalance["CMFT_TransactionDate"].ToString());

                            if (!String.IsNullOrEmpty(drTransactionBalance["CMFTB_TotalCostBalRETURN"].ToString()))
                            {
                                span = valuationDate - DateTime.Parse(drTransactionBalance["CMFT_TransactionDate"].ToString());

                                if (totalCostReturn != 0)
                                {
                                    drTransactionBalance["WeightageInvestedCost"] = Convert.ToDouble(drTransactionBalance["CMFTB_TotalCostBalRETURN"].ToString()) / totalCostReturn;
                                    drTransactionBalance["WeightageReturns"] = Convert.ToDouble(drTransactionBalance["ABS_ReturnPA"].ToString()) * Convert.ToDouble(drTransactionBalance["WeightageInvestedCost"].ToString());
                                    drTransactionBalance["WeightageNAV"] = Convert.ToDouble(drTransactionBalance["CMFT_Price"].ToString()) * Convert.ToDouble(drTransactionBalance["WeightageInvestedCost"].ToString());
                                    drTransactionBalance["WeightageDays"] = span.TotalDays * Convert.ToDouble(drTransactionBalance["WeightageInvestedCost"].ToString());

                                }
                                else
                                {
                                    drTransactionBalance["WeightageInvestedCost"] = 0;
                                    drTransactionBalance["WeightageReturns"] = 0;
                                    drTransactionBalance["WeightageNAV"] = 0;
                                    drTransactionBalance["WeightageDays"] = 0;
                                }

                            }
                            else
                            {
                                drTransactionBalance["WeightageInvestedCost"] = 0;
                                drTransactionBalance["WeightageReturns"] = 0;
                                drTransactionBalance["WeightageNAV"] = 0;
                                drTransactionBalance["WeightageDays"] = 0;
                            }
                            i++;
                        }                       

                      

                        drMFNetPosition["CMFNP_RET_Hold_XIRR"] = 0; //Still Confusion
                        drMFNetPosition["CMFNP_RET_Realized_XIRR"] = 0; // Still Confusion
                        //drMFNetPosition["CMFNP_RET_ALL_TotalXIRR"] = 0;  // XIRR 
                      

                        drMFNetPosition["CMFNP_RET_Realized_DVRAmt"] = 0;  // DVR will be always zero for Realised

                        
                        sumObject = dtMFTransactionBalance.Compute("Sum([InvestedCostReturnHolding])", string.Empty);
                        double.TryParse(Convert.ToString(sumObject), out CMFNP_RET_Hold_AcqCost);

                        sumObject = dtMFTransactionBalance.Compute("Sum([WeightageReturns])", string.Empty);
                        double.TryParse(Convert.ToString(sumObject), out annualizedReturns);

                        sumObject = dtMFTransactionBalance.Compute("Sum([WeightageNAV])", string.Empty);
                        double.TryParse(Convert.ToString(sumObject), out WeightageNAV);

                        sumObject = dtMFTransactionBalance.Compute("Sum([WeightageDays])", string.Empty);
                        double.TryParse(Convert.ToString(sumObject), out WeightageDays);

                        sumObject = dtMFTransactionBalance.Compute("Sum([CMFT_Amount])", "WMTT_TransactionClassificationCode = 'BUY'");
                        double.TryParse(Convert.ToString(sumObject), out returnInvestedCost);

                        drMFNetPosition["CMFNP_InvestedCost"] = returnInvestedCost;
                        drMFNetPosition["CMFNP_RET_Hold_AnnualisedReturns"] = annualizedReturns;
                        drMFNetPosition["CMFNP_RET_Hold_WeightageNAV"] = WeightageNAV;
                        drMFNetPosition["CMFNP_RET_Hold_WeightageDays"] = WeightageDays;


                        if (dtMFTransactionSellPair.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtMFTransactionSellPair.Rows)
                            {
                                DataRow[] drTransaction = dtMFTransactionBalance.Select("CMFT_MFTransId=" + dr["CMFSP_BuyID"].ToString());
                                if (drTransaction.Count() > 0)
                                {
                                    CMFNP_TAX_Realized_AcqCost += (double.Parse(drTransaction[0]["CMFT_Price"].ToString()) * double.Parse(dr["CMFSP_Units"].ToString()));
                                }
                                //CMFNP_TAX_Realized_AcqCost += (double.Parse(drTransaction[0]["CMFT_Price"].ToString()) * double.Parse(dr["CMFSP_Units"].ToString()));
                            }
                        }

                        drMFNetPosition["CMFNP_TAX_Realized_AcqCost"] = CMFNP_TAX_Realized_AcqCost;

                        sumObject = dtMFTransactionBalance.Compute("Sum(CMFT_Amount)", "WMTT_TransactionClassificationCode = 'SEL'");
                        double.TryParse(Convert.ToString(sumObject), out reedemedAmount);

                        drMFNetPosition["CMFNP_RedeemedAmount"] = reedemedAmount;
                        drMFNetPosition["CMFNP_TAX_Realized_TotalPL"] = reedemedAmount - CMFNP_TAX_Realized_AcqCost;


                        //drMFNetPosition["CMFNP_NetHoldings"] = openUnits;

                        sumObject = dtMFTransactionSellPair.Compute("Sum(CMFSP_Units)", string.Empty);
                        double.TryParse(Convert.ToString(sumObject), out sellUnits);

                        drMFNetPosition["CMFNP_SalesQuantity"] = sellUnits;



                        drMFNetPosition["CMFNP_TAX_Hold_BalanceAmt"] = totalDiv;
                        //Open units with fraction value issue 
                        if (Math.Round(openUnits, 4) != 0)
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

                        drMFNetPosition["CMFNP_RET_Hold_TotalPL"] = Math.Round(returnHoldingTotalPL, 4);

                        if (CMFNP_RET_Hold_AcqCost != 0)
                        {
                            drMFNetPosition["CMFNP_RET_Hold_AbsReturn"] = (returnHoldingTotalPL / CMFNP_RET_Hold_AcqCost) * 100;
                        }
                        returnRealisedInvestedCost = returnInvestedCost - CMFNP_RET_Hold_AcqCost;
                        if (returnRealisedInvestedCost < .5)
                            returnRealisedInvestedCost = Math.Round(returnRealisedInvestedCost);

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

                        double holdingDvrUnits = 0;
                        sumObject = dtMFTransactionBalance.Compute("Sum(CMFTB_UnitBalanceTAX)", "WMTT_TransactionClassificationCode = 'DVR'");
                        double.TryParse(Convert.ToString(sumObject), out holdingDvrUnits);

                        double holdingDvrAmount = 0;
                        sumObject = dtMFTransactionBalance.Compute("Sum(CMFNP_RET_Hold_DVRAmounts)", "WMTT_TransactionClassificationCode = 'DVR'");
                        double.TryParse(Convert.ToString(sumObject), out holdingDvrAmount);
                        drMFNetPosition["CMFNP_RET_Hold_DVRAmounts"] = holdingDvrAmount;
                        drMFNetPosition["CMFNP_RET_Hold_DVRUnits"] = holdingDvrUnits;

                        //if (returnPurchaseUnits > 0)
                        //{
                        //    if (openUnits > returnPurchaseUnits)
                        //    {
                        //        drMFNetPosition["CMFNP_RET_Hold_DVRUnits"] = openUnits - returnPurchaseUnits;
                        //    }
                        //    else
                        //        drMFNetPosition["CMFNP_RET_Hold_DVRUnits"] = 0;
                        //}
                        //else
                        //    drMFNetPosition["CMFNP_RET_Hold_DVRUnits"] = 0;
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

                   

                        object holdingStartDate = dtMFTransactionBalance.Compute("min(CMFT_TransactionDate)", "CMFTB_UnitBalanceTAX > 0");
                        if (holdingStartDate != DBNull.Value)
                        {
                            drMFNetPosition["CMFNP_InvestmentStartDate"] = Convert.ToDateTime(holdingStartDate).ToShortDateString();
                        }

                        if (openUnits != 0)
                            drMFNetPosition["CMFNP_RET_Hold_WeightageNAV"] = totalCostReturn / openUnits;
                        else
                        {
                            drMFNetPosition["CMFNP_RET_Hold_WeightageNAV"] = 0;
                        }
                    }
                    else // openUnits <0
                    { 
                        drMFNetPosition["CMFNP_SalesQuantity"] = 0;
                        drMFNetPosition["CMFNP_RedeemedAmount"] = 0;
                        drMFNetPosition["CMFNP_InvestedCost"] = 0;
                        drMFNetPosition["CMFNP_CurrentValue"] = 0;
                        drMFNetPosition["CMFNP_RET_ALL_TotalPL"] = 0;
                        drMFNetPosition["CMFNP_RET_ALL_AbsReturn"] = 0;
                        drMFNetPosition["CMFNP_RET_ALL_TotalXIRR"] = 0;
                        drMFNetPosition["CMFNP_RET_ALL_DVRAmt"] = 0;
                        drMFNetPosition["CMFNP_RET_ALL_DVPAmt"] = 0;
                        drMFNetPosition["CMFNP_RET_Hold_AcqCost"] = 0;
                        drMFNetPosition["CMFNP_RET_Hold_TotalPL"] = 0;
                        drMFNetPosition["CMFNP_RET_Hold_AbsReturn"] = 0;
                        drMFNetPosition["CMFNP_RET_Hold_PurchaseUnit"] = 0;
                        drMFNetPosition["CMFNP_RET_Hold_DVRUnits"] = 0;
                        drMFNetPosition["CMFNP_RET_Hold_DVPAmt"] = 0;
                        drMFNetPosition["CMFNP_RET_Hold_XIRR"] = 0;
                        drMFNetPosition["CMFNP_RET_Realized_InvestedCost"] = 0;
                        drMFNetPosition["CMFNP_RET_Realized_DVPAmt"] = 0;
                        drMFNetPosition["CMFNP_RET_Realized_DVRAmt"] = 0;
                        drMFNetPosition["CMFNP_RET_Realized_TotalPL"] = 0;
                        drMFNetPosition["CMFNP_RET_Realized_AbsReturn"] = 0;
                        drMFNetPosition["CMFNP_RET_Realized_XIRR"] = 0;
                        drMFNetPosition["CMFNP_TAX_Hold_PurchaseUnits"] = 0;
                        drMFNetPosition["CMFNP_TAX_Hold_BalanceAmt"] = 0;
                        drMFNetPosition["CMFNP_TAX_Hold_TotalPL"] = 0;
                        drMFNetPosition["CMFNP_TAX_Hold_EligibleSTCG"] = 0;
                        drMFNetPosition["CMFNP_TAX_Hold_EligibleLTCG"] = 0;
                        drMFNetPosition["CMFNP_TAX_Realized_TotalPL"] = 0;
                        drMFNetPosition["CMFNP_TAX_Realized_AcqCost"] = 0;
                        drMFNetPosition["CMFNP_TAX_Realized_STCG"] = 0;
                        drMFNetPosition["CMFNP_TAX_Realized_LTCG"] = 0;
                        drMFNetPosition["CMFNP_RET_Hold_DVRAmounts"] = 0;
                        drMFNetPosition["CMFNP_RET_Hold_AnnualisedReturns"] = 0;
                        drMFNetPosition["CMFNP_RET_Hold_WeightageNAV"] = 0;
                        drMFNetPosition["CMFNP_RET_Hold_WeightageDays"] = 0;

                    }

                    dtMFNetPosition.Rows.Add(drMFNetPosition);
                }

            }
            catch (BaseApplicationException Ex)
            {
                emailSMSBo.SendErrorExceptionMail(int.Parse(drMFNetPosition["CMFA_AccountId"].ToString()), "AccountId", int.Parse(drMFNetPosition["PASP_SchemePlanCode"].ToString()), Ex.Message, "MFEngineBo.cs_CreateMFNetPositionDataTable");
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
                if (result.ToString().Contains("E") || Convert.ToInt64(result).ToString().Length > 3 || result.ToString().Contains("e"))
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


        public DataSet CreateMFNetPositionDataTable(DataSet dsMFTransactionBalanceAndSellPair, DateTime valuationDate, int schemePlanCode)
        {
            DataSet dsMfNetPositionValue = new DataSet();
            DataTable dtMFTransactionProcessedBalance = new DataTable();
            double nav = 0;
            DateTime navDate = new DateTime();
            DataTable dtBalanceRecord = new DataTable();
            DataTable dtSellPaired = new DataTable();

            dtBalanceRecord = CreateBalanceTable();
            dtSellPaired = CreateSellPairedDataTable();

            TimeSpan span = new TimeSpan();

            dtMFTransactionProcessedBalance = dsMFTransactionBalanceAndSellPair.Tables[0];
            dtCustomerMFTransactionSellPaired = dsMFTransactionBalanceAndSellPair.Tables[1];
            DataSet dsSchemeNavDetail = new DataSet();

            DataRow drBalanceRecord;
            DataRow drSellPairedDetails;

            dsSchemeNavDetail = mfEngineDao.GetSchemeNavDetail(schemePlanCode, valuationDate);

            if (dsSchemeNavDetail != null)
            {
                if (dsSchemeNavDetail.Tables.Count > 0)
                {
                    if (dsSchemeNavDetail.Tables[0].Rows.Count > 0)
                    {
                        nav = double.Parse(dsSchemeNavDetail.Tables[0].Rows[0]["NAV"].ToString());
                    }
                    if (dsSchemeNavDetail.Tables[1].Rows.Count > 0)
                    {
                        navDate = DateTime.Parse(dsSchemeNavDetail.Tables[1].Rows[0]["PSP_Date"].ToString());
                    }
                }
            }
            if (dtMFTransactionProcessedBalance.Rows.Count > 0)
            {
                foreach (DataRow drMFBalanced in dtMFTransactionProcessedBalance.Rows)
                {
                    drBalanceRecord = dtBalanceRecord.NewRow();
                    drBalanceRecord["CMFT_MFTransId"] = drMFBalanced["CMFT_MFTransId"].ToString();
                    drBalanceRecord["CMFT_Amount"] = drMFBalanced["CMFT_Amount"].ToString();
                    drBalanceRecord["CMFA_AccountId"] = drMFBalanced["CMFA_AccountId"].ToString();
                    drBalanceRecord["PASP_SchemePlanCode"] = drMFBalanced["PASP_SchemePlanCode"].ToString();
                    drBalanceRecord["CMFT_TransactionDate"] = drMFBalanced["CMFT_TransactionDate"].ToString();
                   
                    drBalanceRecord["CMFTB_TotalCostBalanceTAX"] = drMFBalanced["CMFTB_TotalCostBalanceTAX"].ToString();
                    drBalanceRecord["CMFTB_UnitBalanceTAX"] = drMFBalanced["CMFTB_UnitBalanceTAX"].ToString();
                    if (drMFBalanced["WMTT_TransactionClassificationCode"].ToString() != "SEL")
                    {                      
                        drBalanceRecord["CMFTB_UnitBalanceRETURN"] = drMFBalanced["CMFTB_UnitBalanceRETURN"].ToString();
                    }
                    else
                    {
                        drBalanceRecord["CMFTB_UnitBalanceRETURN"] = 0;
                    }
                   
                    drBalanceRecord["CMFTB_AvgCostBalRETURN"] = drMFBalanced["CMFTB_AvgCostBalRETURN"].ToString();
                    drBalanceRecord["CMFTB_TotalCostBalRETURN"] = drMFBalanced["CMFTB_TotalCostBalRETURN"].ToString();
                    drBalanceRecord["CMFTB_DivPayout"] = drMFBalanced["CMFTB_DivPayout"].ToString();
                    drBalanceRecord["NAV"] = nav;
                    drBalanceRecord["NAVDate"] = navDate;
                    drBalanceRecord["CurrentValueTax"] = double.Parse(drMFBalanced["CMFTB_UnitBalanceTAX"].ToString()) * nav;
                    span = valuationDate - DateTime.Parse(drMFBalanced["CMFT_TransactionDate"].ToString());
                    drBalanceRecord["AGE"] = span.TotalDays;
                    drBalanceRecord["WMTT_TransactionClassificationCode"] = drMFBalanced["WMTT_TransactionClassificationCode"].ToString();
                    drBalanceRecord["CurrentValueReturn"] = double.Parse(drBalanceRecord["CMFTB_UnitBalanceRETURN"].ToString()) * nav;

                    if (drMFBalanced["WMTT_TransactionClassificationCode"].ToString() != "SEL")
                    {
                        if (span.TotalDays > 365)
                        {
                            drBalanceRecord["EligibleLTG"] = (double.Parse(drMFBalanced["CMFTB_UnitBalanceTAX"].ToString()) * nav) - double.Parse(drMFBalanced["CMFT_Amount"].ToString());
                            drBalanceRecord["EligibleSTG"] = 0;
                        }
                        else
                        {
                            drBalanceRecord["EligibleSTG"] = (double.Parse(drMFBalanced["CMFTB_UnitBalanceTAX"].ToString()) * nav) - double.Parse(drMFBalanced["CMFT_Amount"].ToString());
                            drBalanceRecord["EligibleLTG"] = 0;
                        }

                    }
                    else
                    {
                        drBalanceRecord["EligibleLTG"] = 0;
                        drBalanceRecord["EligibleSTG"] = 0;
                    }

                    if (drMFBalanced["WMTT_TransactionClassificationCode"].ToString() == "BUY")
                    {
                        drBalanceRecord["XIRR_ALL"] = -double.Parse(drMFBalanced["CMFT_Amount"].ToString());
                    }
                    else if (drMFBalanced["WMTT_TransactionClassificationCode"].ToString() == "SEL" || drMFBalanced["WMTT_TransactionClassificationCode"].ToString() == "DVP")
                    {
                        drBalanceRecord["XIRR_ALL"] = double.Parse(drMFBalanced["CMFT_Amount"].ToString());
                    }
                    else if (drMFBalanced["WMTT_TransactionClassificationCode"].ToString() == "DVR" || drMFBalanced["WMTT_TransactionClassificationCode"].ToString() == "BNS")
                    {
                        drBalanceRecord["XIRR_ALL"] = 0;
                    }

                    drBalanceRecord["CMFT_Price"] = drMFBalanced["CMFT_Price"].ToString();

                    drBalanceRecord["InvestedCostReturnHolding"] = double.Parse(drMFBalanced["CMFTB_AvgCostBalRETURN"].ToString()) * double.Parse(drMFBalanced["CMFTB_UnitBalanceRETURN"].ToString());

                    if (drMFBalanced["WMTT_TransactionClassificationCode"].ToString() != "SEL")
                    {
                    drBalanceRecord["CMFNP_RET_Hold_DVRAmounts"] = double.Parse(drMFBalanced["CMFT_Price"].ToString()) * double.Parse(drMFBalanced["CMFTB_UnitBalanceRETURN"].ToString());
                    }
                    else
                    {
                        drBalanceRecord["CMFNP_RET_Hold_DVRAmounts"] = 0;
                    }
                    if (Math.Round(decimal.Parse(drMFBalanced["CMFTB_TotalCostBalRETURN"].ToString()),0) != 0 && span.TotalDays > 0 )
                        drBalanceRecord["ABS_ReturnPA"] = (((double.Parse(drMFBalanced["CMFTB_UnitBalanceRETURN"].ToString()) * nav) - double.Parse(drMFBalanced["CMFTB_TotalCostBalRETURN"].ToString())) / double.Parse(drMFBalanced["CMFTB_TotalCostBalRETURN"].ToString())) * 36500 / span.TotalDays;
                    //AS ABS_ReturnPA
                    else
                        drBalanceRecord["ABS_ReturnPA"] = 0;


                    drBalanceRecord["CMFNP_RET_Hold_DVRAmounts"] = double.Parse(drMFBalanced["CMFT_Price"].ToString()) * double.Parse(drBalanceRecord["CMFTB_UnitBalanceRETURN"].ToString());

                    dtBalanceRecord.Rows.Add(drBalanceRecord);
                }
            }

            if (dtCustomerMFTransactionSellPaired.Rows.Count > 0)
            {
                foreach (DataRow drSellPaired in dtCustomerMFTransactionSellPaired.Rows)
                {
                    drSellPairedDetails = dtSellPaired.NewRow();

                    drSellPairedDetails["CMFSP_SellID"] = drSellPaired["CMFT_MFTransIdSell"].ToString();
                    drSellPairedDetails["CMFSP_BuyID"] = drSellPaired["CMFT_MFTransIdBuy"].ToString();
                    drSellPairedDetails["CMFSP_Units"] = drSellPaired["CMFT_Units"].ToString();

                    span = valuationDate - DateTime.Parse(drSellPaired["CMFT_TransactionDate"].ToString());

                    drSellPairedDetails["AGE"] = span.TotalDays;

                    if (span.TotalDays > 365)
                    {
                        drSellPairedDetails["LTG"] = drSellPaired["CMFT_Gain_loss_Value"].ToString();
                        drSellPairedDetails["STG"] = 0;
                    }
                    else
                    {
                        drSellPairedDetails["STG"] = drSellPaired["CMFT_Gain_loss_Value"].ToString();
                        drSellPairedDetails["LTG"] = 0;
                    }
                    drSellPairedDetails["CMFSP_LTG"] = drSellPaired["CMFT_LTG"].ToString();
                    drSellPairedDetails["CMFSP_STG"] = drSellPaired["CMFT_STG"].ToString();
                    drSellPairedDetails["CMFSP_GainLossValue"] = drSellPaired["CMFT_Gain_loss_Value"].ToString();
                    dtSellPaired.Rows.Add(drSellPairedDetails);

                }
            }

            dsMfNetPositionValue.Tables.Add(dtBalanceRecord);
            dsMfNetPositionValue.Tables.Add(dtSellPaired);
            return dsMfNetPositionValue;
        }

        public DataTable CreateBalanceTable()
        {
            DataTable dtMFBalanced = new DataTable();
            dtMFBalanced.Columns.Add("CMFA_AccountId", typeof(double));
            dtMFBalanced.Columns.Add("PASP_SchemePlanCode", typeof(double));
            dtMFBalanced.Columns.Add("CMFT_Amount", typeof(double));
            dtMFBalanced.Columns.Add("CMFT_MFTransId", typeof(double));
            dtMFBalanced.Columns.Add("CMFT_TransactionDate", typeof(System.DateTime));
            dtMFBalanced.Columns.Add("CMFTB_UnitBalanceTAX", typeof(double));
            dtMFBalanced.Columns.Add("CMFTB_TotalCostBalanceTAX", typeof(double));
            dtMFBalanced.Columns.Add("CMFTB_UnitBalanceRETURN", typeof(double));
            dtMFBalanced.Columns.Add("CMFTB_AvgCostBalRETURN", typeof(double));
            dtMFBalanced.Columns.Add("CMFTB_TotalCostBalRETURN", typeof(double));

            dtMFBalanced.Columns.Add("CMFTB_DivPayout", typeof(double));
            dtMFBalanced.Columns.Add("NAV", typeof(double));
            dtMFBalanced.Columns.Add("NAVDate", typeof(System.DateTime));
            dtMFBalanced.Columns.Add("CurrentValueTax", typeof(double));

            dtMFBalanced.Columns.Add("AGE", typeof(double));
            dtMFBalanced.Columns.Add("EligibleLTG", typeof(double));
            dtMFBalanced.Columns.Add("EligibleSTG", typeof(double));
            dtMFBalanced.Columns.Add("CurrentValueReturn", typeof(double));
            dtMFBalanced.Columns.Add("CMFT_Price", typeof(double));

            dtMFBalanced.Columns.Add("WMTT_TransactionClassificationCode", typeof(System.String));
            dtMFBalanced.Columns.Add("XIRR_ALL", typeof(double));
            dtMFBalanced.Columns.Add("InvestedCostReturnHolding", typeof(double));
            dtMFBalanced.Columns.Add("CMFNP_RET_Hold_DVRAmounts", typeof(double));
            dtMFBalanced.Columns.Add("ABS_ReturnPA", typeof(double));
            dtMFBalanced.Columns.Add("AverageNAV", typeof(double));

            return dtMFBalanced;

        }

        public DataTable CreateSellPairedDataTable()
        {
            DataTable dtMFSellPaired = new DataTable();
            dtMFSellPaired.Columns.Add("CMFSP_SellID", typeof(double));
            dtMFSellPaired.Columns.Add("CMFSP_BuyID", typeof(double));
            dtMFSellPaired.Columns.Add("CMFSP_Units", typeof(double));
            dtMFSellPaired.Columns.Add("AGE", typeof(double));
            dtMFSellPaired.Columns.Add("CMFT_TransactionDate", typeof(System.DateTime));
            dtMFSellPaired.Columns.Add("CMFSP_LTG", typeof(double));
            dtMFSellPaired.Columns.Add("CMFSP_STG", typeof(double));
            dtMFSellPaired.Columns.Add("CMFSP_GainLossValue", typeof(double));
            dtMFSellPaired.Columns.Add("LTG", typeof(double));
            dtMFSellPaired.Columns.Add("STG", typeof(double));

            return dtMFSellPaired;

        }




    }
}
