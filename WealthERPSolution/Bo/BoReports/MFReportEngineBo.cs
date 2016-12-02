using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DaoReports;

namespace BoReports
{
    public class MFReportEngineBo
    {
        MFReportEngineDao MFReportEngineDao = new MFReportEngineDao();
        //----------Pairing Starts
        public DataSet CreateInvetmentsPairingAccountStockWise(DataTable dtTransactions)
        {
            string FolioNum = "";
            int ScripCode = 0;
            DataTable dtAccountList = new DataTable();
            DataTable dtBuyTransactions = new DataTable();
            DataTable dtSellTransactions = new DataTable();
            DataSet dsTransactoinsAfterPairing = new DataSet();
            DataSet ds = new DataSet();
            DataTable dtCustomerTransactionToProcess = new DataTable();
            DataTable dtAcountWiseBuyTransaction = new DataTable();
            DataTable dtAcountWiseSellTransaction = new DataTable();
            string[] columnNames = new string[] { "ScripCode", "FolioNum" };
            dtAccountList = new DataView(dtTransactions).ToTable(true, columnNames);
            dtCustomerTransactionToProcess = dtTransactions.Copy();
            foreach (DataRow drAccounts in dtAccountList.Rows)
            {

                FolioNum = drAccounts["FolioNum"].ToString();
                ScripCode = int.Parse(drAccounts["ScripCode"].ToString());

                try
                {
                    if (dtCustomerTransactionToProcess.Select("FolioNum='" + FolioNum + "' AND ScripCode='" + ScripCode + "' AND BuySell<>'S' ").Count() > 0)
                    {
                        dtAcountWiseBuyTransaction = dtCustomerTransactionToProcess.Select("FolioNum='" + FolioNum + "' AND ScripCode='" + ScripCode + "' AND BuySell<>'S' ", "TransactionDate ASC").CopyToDataTable();
                        if (dtCustomerTransactionToProcess.Select("FolioNum='" + FolioNum + "' AND ScripCode='" + ScripCode + "' AND BuySell='S' ").Count() > 0)
                        {
                            dtAcountWiseSellTransaction = dtCustomerTransactionToProcess.Select("FolioNum='" + FolioNum + "' AND ScripCode='" + ScripCode + "' AND BuySell='S' ", "TransactionDate ASC").CopyToDataTable();
                        }
                        else
                            dtAcountWiseSellTransaction = dtCustomerTransactionToProcess.Clone();
                        ds = CreateMFNetPositionPairing(dtAcountWiseBuyTransaction, dtAcountWiseSellTransaction);
                        dtBuyTransactions.Merge(ds.Tables[0]);
                        dtSellTransactions.Merge(ds.Tables[1]);
                        ds.Clear();
                    }

                    ScripCode = 0;
                    FolioNum = "";

                }
                catch (Exception ex)
                {
                }
            }
            dsTransactoinsAfterPairing.Tables.Add(dtBuyTransactions);
            dsTransactoinsAfterPairing.Tables.Add(dtSellTransactions);
            return dsTransactoinsAfterPairing;
        }
        public DataSet CreateMFNetPositionPairing(DataTable tempBuyTransaction, DataTable tempSellTransaction)
        {
            DataSet ds = new DataSet();
            decimal sellQuantity = 0;
            decimal buyQuantity = 0;
            int rowCountBuy = 0;
            foreach (DataRow drSell in tempSellTransaction.Rows)
            {
                sellQuantity = decimal.Parse(drSell["Units"].ToString());
                while (sellQuantity > 0)
                {
                    DataRow drBuy;
                    if (rowCountBuy < (tempBuyTransaction.Rows.Count - 1))
                        drBuy = tempBuyTransaction.Rows[rowCountBuy];
                    else
                        if (tempBuyTransaction.Rows.Count > 0)
                            drBuy = tempBuyTransaction.Rows[tempBuyTransaction.Rows.Count - 1];//tempBuyTransaction.Select("CET_IsSpeculative='" + tranxType + "'  ").Last();//
                        else
                            drBuy = null;

                    if (drBuy != null)
                    {
                        buyQuantity = decimal.Parse(drBuy["Units"].ToString());
                        if (buyQuantity >= sellQuantity)
                        {
                            buyQuantity = buyQuantity - sellQuantity;
                            sellQuantity = 0;
                            drBuy["Units"] = buyQuantity.ToString();
                            drSell["Units"] = 0;
                            if (buyQuantity == 0)
                                rowCountBuy = rowCountBuy + 1;
                        }
                        else
                        {
                            if (rowCountBuy >= (tempBuyTransaction.Rows.Count - 1))
                            {
                                buyQuantity = buyQuantity - sellQuantity;
                                drBuy["Units"] = buyQuantity.ToString();
                                drSell["Units"] = 0;
                                sellQuantity = 0;
                            }
                            else
                            {
                                sellQuantity = sellQuantity - buyQuantity;
                                drBuy["Units"] = 0;
                                drSell["Units"] = sellQuantity.ToString();
                                rowCountBuy = rowCountBuy + 1;
                            }

                        }
                    }
                    else
                    {
                        //Exception code goes here

                        drSell["Units"] = (-sellQuantity).ToString();
                        sellQuantity = -sellQuantity;
                    }

                }

            }
            ds.Tables.Add(tempBuyTransaction);
            ds.Tables.Add(tempSellTransaction);
            return ds;
        }

        public DataTable CreateCapitalGainDetails(DataTable dtTransactions)
        {

            int ScripCode = 0;
            string AssetType = string.Empty;
            string AssetSubType = string.Empty;
            DataTable dtCapitalGainDetailList = new DataTable();
            dtCapitalGainDetailList.Columns.Add("CustomerName", typeof(string));
            dtCapitalGainDetailList.Columns.Add("CustomerId");
            dtCapitalGainDetailList.Columns.Add("PortfolioName", typeof(string));
            dtCapitalGainDetailList.Columns.Add("PortfolioId");
            dtCapitalGainDetailList.Columns.Add("ScripName", typeof(string));
            dtCapitalGainDetailList.Columns.Add("ScripCode", typeof(string));
            dtCapitalGainDetailList.Columns.Add("FolioNum", typeof(string));
            dtCapitalGainDetailList.Columns.Add("Units", typeof(decimal));
            dtCapitalGainDetailList.Columns.Add("SellDate", typeof(DateTime));
            dtCapitalGainDetailList.Columns.Add("SellRate", typeof(decimal));
            dtCapitalGainDetailList.Columns.Add("SellAmount", typeof(decimal));
            dtCapitalGainDetailList.Columns.Add("BuyDate", typeof(DateTime));
            dtCapitalGainDetailList.Columns.Add("BuyRate", typeof(decimal));
            dtCapitalGainDetailList.Columns.Add("BuyAmount", typeof(decimal));
            dtCapitalGainDetailList.Columns.Add("Days", typeof(int));
            dtCapitalGainDetailList.Columns.Add("LTCG", typeof(decimal));
            dtCapitalGainDetailList.Columns.Add("STCG", typeof(decimal));
            dtCapitalGainDetailList.Columns.Add("PAIC_AssetInstrumentCategoryName", typeof(string));
            DataTable dtCapitalGainDetail = dtCapitalGainDetailList.Clone();

            string FolioNum = "";

            DataTable dtAccountList = new DataTable();
            DataTable dtBuyTransactions = new DataTable();
            DataTable dtSellTransactions = new DataTable();
            DataSet dsTransactoinsAfterPairing = new DataSet();
            DataTable dt = new DataTable();
            DataTable dtCustomerTransactionToProcess = new DataTable();
            DataTable dtAcountWiseBuyTransaction = new DataTable();
            DataTable dtAcountWiseSellTransaction = new DataTable();
            string[] columnNames = new string[] { "ScripCode", "FolioNum" };
            dtAccountList = new DataView(dtTransactions).ToTable(true, columnNames);
            dtCustomerTransactionToProcess = dtTransactions.Copy();
            foreach (DataRow drAccounts in dtAccountList.Rows)
            {

                FolioNum = drAccounts["FolioNum"].ToString();
                ScripCode = int.Parse(drAccounts["ScripCode"].ToString());

                try
                {
                    if (dtCustomerTransactionToProcess.Select("FolioNum='" + FolioNum + "' AND ScripCode='" + ScripCode + "' AND BuySell<>'S' ").Count() > 0 && dtCustomerTransactionToProcess.Select("FolioNum='" + FolioNum + "' AND ScripCode='" + ScripCode + "' AND BuySell='S' ").Count() > 0)
                    {
                        dtAcountWiseBuyTransaction = dtCustomerTransactionToProcess.Select("FolioNum='" + FolioNum + "' AND ScripCode='" + ScripCode + "' AND BuySell<>'S' ", "TransactionDate ASC").CopyToDataTable();
                        dtAcountWiseSellTransaction = dtCustomerTransactionToProcess.Select("FolioNum='" + FolioNum + "' AND ScripCode='" + ScripCode + "' AND BuySell='S' ", "TransactionDate ASC").CopyToDataTable();

                        dt = CreateCapitalGainDetailPairing(dtAcountWiseBuyTransaction, dtAcountWiseSellTransaction, dtCapitalGainDetailList);
                        dtCapitalGainDetail.Merge(dt);
                        dt.Clear();
                        dtCapitalGainDetailList.Clear();
                    }

                    ScripCode = 0;
                    FolioNum = "";

                }
                catch (Exception ex)
                {
                }

            }

            return dtCapitalGainDetail;
        }
        public DataTable CreateCapitalGainDetailPairing(DataTable tempBuyTransaction, DataTable tempSellTransaction, DataTable dtCapitalGainDetailList)
        {
            decimal sellQuantity = 0;
            decimal buyQuantity = 0;
            int rowCountBuy = 0;
            decimal realizedAcqCost = 0;
            decimal realizedRedeemedAmount = 0;
            decimal totalLTCG = 0;
            decimal totalSTCG = 0;
            bool isLTCG;
            int age = 0;
            foreach (DataRow drSell in tempSellTransaction.Rows)
            {

                sellQuantity = decimal.Parse(drSell["Units"].ToString());
                while (sellQuantity > 0)
                {

                    DataRow drBuy;
                    realizedAcqCost = 0;
                    realizedRedeemedAmount = 0;
                    age = 0;
                    isLTCG = false;

                    if (rowCountBuy < (tempBuyTransaction.Rows.Count - 1))
                        drBuy = tempBuyTransaction.Rows[rowCountBuy];
                    else
                        if (tempBuyTransaction.Rows.Count > 0)
                            drBuy = tempBuyTransaction.Rows[tempBuyTransaction.Rows.Count - 1];//tempBuyTransaction.Select("CET_IsSpeculative='" + tranxType + "'  ").Last();//
                        else
                            drBuy = null;

                    if (drBuy != null)
                    {
                        DataRow dr = dtCapitalGainDetailList.NewRow();
                        dr["CustomerName"] = drSell["CustomerName"].ToString();
                        dr["CustomerId"] = drSell["CustomerId"].ToString();
                        dr["PortfolioName"] = drSell["PortfolioName"].ToString();
                        dr["PortfolioId"] = drSell["PortfolioId"].ToString();
                        dr["ScripName"] = drSell["ScripName"].ToString();
                        dr["ScripCode"] = drSell["ScripCode"].ToString();
                        dr["FolioNum"] = drSell["FolioNum"].ToString();
                        dr["PAIC_AssetInstrumentCategoryName"] = drSell["PAIC_AssetInstrumentCategoryName"].ToString();
                        age = (DateTime.Parse(drSell["TransactionDate"].ToString()) - DateTime.Parse(drBuy["TransactionDate"].ToString())).Days;
                        if (drSell["PAIC_AssetInstrumentCategoryCode"].ToString() == "MFDT")
                        {
                            if (DateTime.Parse(drSell["TransactionDate"].ToString()) < DateTime.Parse("10 jul 14"))
                            {
                                if (age >= 365)
                                    isLTCG = true;
                            }
                            else
                            {
                                if (age >= 365 * 3)
                                    isLTCG = true;
                            }
                        }
                        else
                        {
                            if (age >= 365)
                                isLTCG = true;
                        }

                        dr["SellDate"] = DateTime.Parse(drSell["TransactionDate"].ToString());
                        dr["SellRate"] = decimal.Parse(drSell["Price"].ToString());
                        dr["BuyDate"] = DateTime.Parse(drBuy["TransactionDate"].ToString());
                        dr["BuyRate"] = decimal.Parse(drBuy["Price"].ToString());
                        dr["Days"] = age;
                        buyQuantity = decimal.Parse(drBuy["Units"].ToString());

                        if (buyQuantity >= sellQuantity)
                        {
                            realizedAcqCost += decimal.Parse(sellQuantity.ToString()) * decimal.Parse(drBuy["Price"].ToString());
                            realizedRedeemedAmount += decimal.Parse(sellQuantity.ToString()) * decimal.Parse(drSell["Price"].ToString());
                            dr["Units"] = sellQuantity;
                            buyQuantity = buyQuantity - sellQuantity;
                            sellQuantity = 0;
                            drBuy["Units"] = buyQuantity.ToString();
                            drSell["Units"] = 0;
                            dr["SellAmount"] = decimal.Parse(dr["Units"].ToString()) * decimal.Parse(dr["SellRate"].ToString());
                            dr["BuyAmount"] = decimal.Parse(dr["Units"].ToString()) * decimal.Parse(dr["BuyRate"].ToString());
                            if (buyQuantity == 0)
                                rowCountBuy = rowCountBuy + 1;

                        }
                        else
                        {
                            if (rowCountBuy >= (tempBuyTransaction.Rows.Count - 1))
                            {
                                realizedAcqCost += decimal.Parse(sellQuantity.ToString()) * decimal.Parse(drBuy["Price"].ToString());
                                realizedRedeemedAmount += decimal.Parse(sellQuantity.ToString()) * decimal.Parse(drSell["Price"].ToString());
                                dr["Units"] = sellQuantity;
                                buyQuantity = buyQuantity - sellQuantity;
                                drBuy["Units"] = buyQuantity.ToString();
                                drSell["Units"] = 0;
                                sellQuantity = 0;
                                dr["SellAmount"] = decimal.Parse(dr["Units"].ToString()) * decimal.Parse(dr["SellRate"].ToString());
                                dr["BuyAmount"] = decimal.Parse(dr["Units"].ToString()) * decimal.Parse(dr["BuyRate"].ToString());
                            }
                            else
                            {
                                realizedAcqCost += decimal.Parse(buyQuantity.ToString()) * decimal.Parse(drBuy["Price"].ToString());
                                realizedRedeemedAmount += decimal.Parse(buyQuantity.ToString()) * decimal.Parse(drSell["Price"].ToString());
                                sellQuantity = sellQuantity - buyQuantity;
                                drBuy["Units"] = 0;
                                drSell["Units"] = sellQuantity.ToString();
                                dr["Units"] = buyQuantity;
                                dr["SellAmount"] = decimal.Parse(dr["Units"].ToString()) * decimal.Parse(dr["SellRate"].ToString());
                                dr["BuyAmount"] = decimal.Parse(dr["Units"].ToString()) * decimal.Parse(dr["BuyRate"].ToString());
                                rowCountBuy = rowCountBuy + 1;
                            }

                        }
                        if (isLTCG)
                        {
                            dr["LTCG"] = realizedRedeemedAmount - realizedAcqCost;
                            totalLTCG += decimal.Parse(dr["LTCG"].ToString());
                            dr["STCG"] = 0;

                        }
                        else
                        {
                            dr["STCG"] = realizedRedeemedAmount - realizedAcqCost;
                            totalSTCG += decimal.Parse(dr["STCG"].ToString());
                            dr["LTCG"] = 0;
                        }

                        dtCapitalGainDetailList.Rows.Add(dr);
                    }

                    else
                    {

                        drSell["Units"] = (-sellQuantity).ToString();
                        sellQuantity = -sellQuantity;
                    }

                }


            }

            return dtCapitalGainDetailList;

        }
        public DataTable CreatePointToPointTable(DataTable dtTransactions, DataTable dtOpenTranxns, DataTable dtScrips)
        {
            DataTable dtClosingBalance = new DataTable();
            dtClosingBalance.Columns.Add("CustomerName");
            dtClosingBalance.Columns.Add("CustomerId");
            dtClosingBalance.Columns.Add("PortfolioName");
            dtClosingBalance.Columns.Add("PortfolioId");
            dtClosingBalance.Columns.Add("FolioNum");
            dtClosingBalance.Columns.Add("ScripName");
            dtClosingBalance.Columns.Add("TransactionDate");
            dtClosingBalance.Columns.Add("Units", typeof(decimal));
            dtClosingBalance.Columns.Add("Price", typeof(decimal));
            dtClosingBalance.Columns.Add("Amount", typeof(decimal));
            dtClosingBalance.Columns.Add("Open", typeof(decimal));
            dtClosingBalance.Columns.Add("TranxnType");
            dtClosingBalance.Columns.Add("BuySell");

            decimal BuyUnits = 0;
            decimal SellUnits = 0;
            int count = 0;

            foreach (DataRow dr in dtScrips.Rows)
            {
                if (dtTransactions.Select("PortfolioId='" + dr["PortfolioId"] + "' AND FolioNum='" + dr["FolioNum"] + "' AND ScripName='" + dr["ScripName"] + "'").Count() > 0)
                {
                    DataTable dt = dtTransactions.Select("PortfolioId='" + dr["PortfolioId"] + "' AND FolioNum='" + dr["FolioNum"] + "' AND ScripName='" + dr["ScripName"] + "'").CopyToDataTable();
                    DataTable dtOpen = new DataTable();
                    if (dtOpenTranxns.Select("PortfolioId='" + dr["PortfolioId"] + "' AND FolioNum='" + dr["FolioNum"] + "' AND ScripName='" + dr["ScripName"] + "'").Count() > 0)
                        dtOpen = dtOpenTranxns.Select("PortfolioId='" + dr["PortfolioId"] + "' AND FolioNum='" + dr["FolioNum"] + "' AND ScripName='" + dr["ScripName"] + "'").CopyToDataTable();
                    else
                        dtOpen = dtOpenTranxns.Clone();
                    count = 0;
                    foreach (DataRow drClosin in dt.Rows)
                    {
                        DataRow drNew = dtClosingBalance.NewRow();
                        BuyUnits = 0;
                        SellUnits = 0;
                        if (dtOpen.Select("BuySell='B'").Count() > 0)
                            BuyUnits = decimal.Parse(dtOpen.Compute("Sum(Units)", "BuySell='B'").ToString());
                        if (dtOpen.Select("BuySell='S' ").Count() > 0)
                            SellUnits = decimal.Parse(dtOpen.Compute("Sum(Units)", "BuySell='S'").ToString());
                        drNew["CustomerName"] = drClosin["CustomerName"].ToString();
                        drNew["CustomerId"] = drClosin["CustomerId"].ToString();
                        drNew["PortfolioName"] = dr["PortfolioName"].ToString();
                        drNew["PortfolioId"] = drClosin["PortfolioId"].ToString();
                        drNew["FolioNum"] = drClosin["FolioNum"].ToString();
                        drNew["ScripName"] = drClosin["ScripName"].ToString();
                        drNew["TransactionDate"] = DateTime.Parse(drClosin["TransactionDate"].ToString()).ToString("dd-MMM-yyyy");
                        drNew["Units"] = decimal.Parse(drClosin["Units"].ToString());
                        drNew["Price"] = decimal.Parse(drClosin["Price"].ToString());
                        drNew["Amount"] = decimal.Parse(drClosin["Amount"].ToString());
                        if (count == 0)
                            drNew["Open"] = BuyUnits - SellUnits;
                        else
                            drNew["Open"] = 0;
                        drNew["TranxnType"] = drClosin["TranxnTypeCode"].ToString();
                        drNew["BuySell"] = drClosin["BuySell"].ToString();
                        dtClosingBalance.Rows.Add(drNew);
                        count++;
                    }
                }
                else
                {
                    DataTable dtOpen = new DataTable();
                    if (dtOpenTranxns.Select("PortfolioId='" + dr["PortfolioId"] + "' AND FolioNum='" + dr["FolioNum"] + "' AND ScripName='" + dr["ScripName"] + "'").Count() > 0)
                    {
                        DataRow drNew = dtClosingBalance.NewRow();
                        dtOpen = dtOpenTranxns.Select("PortfolioId='" + dr["PortfolioId"] + "' AND FolioNum='" + dr["FolioNum"] + "' AND ScripName='" + dr["ScripName"] + "'").CopyToDataTable();
                        BuyUnits = 0;
                        SellUnits = 0;
                        if (dtOpen.Select("BuySell='B'").Count() > 0)
                            BuyUnits = decimal.Parse(dtOpen.Compute("Sum(Units)", "BuySell='B'").ToString());
                        if (dtOpen.Select("BuySell='S' ").Count() > 0)
                            SellUnits = decimal.Parse(dtOpen.Compute("Sum(Units)", "BuySell='S'").ToString());
                        drNew["CustomerName"] = dr["CustomerName"].ToString();
                        drNew["CustomerId"] = dr["CustomerId"].ToString();
                        drNew["PortfolioName"] = dr["PortfolioName"].ToString();
                        drNew["PortfolioId"] = dr["PortfolioId"].ToString();
                        drNew["FolioNum"] = dr["FolioNum"].ToString();
                        drNew["ScripName"] = dr["ScripName"].ToString();
                        drNew["TransactionDate"] = "";
                        drNew["Units"] = 0;
                        drNew["Price"] = 0;
                        drNew["Amount"] = 0;
                        drNew["Open"] = BuyUnits - SellUnits;
                        drNew["TranxnType"] = "Buy";
                        drNew["BuySell"] = "B";
                        dtClosingBalance.Rows.Add(drNew);
                    }
                }


            }


            return dtClosingBalance;



        }
        //----------Pairing Ends

        //----------DataTable Generations for Report Starts
        public DataSet CreateCustomerMFReturnsHolding(int C_CustomerId, string PortfolioIds, DateTime? FromDate, DateTime ToDate)
        {
            DataTable dtTranxn = MFReportEngineDao.GetCustomerMFTransactions(C_CustomerId, PortfolioIds, FromDate, ToDate).Tables[0];
            DataTable dtMFReturnsHolding = new DataTable();
            dtMFReturnsHolding.Columns.Add("CustomerName");
            dtMFReturnsHolding.Columns.Add("CustomerId");
            dtMFReturnsHolding.Columns.Add("PortfolioName");
            dtMFReturnsHolding.Columns.Add("PortfolioId");
            dtMFReturnsHolding.Columns.Add("ScripName");
            dtMFReturnsHolding.Columns.Add("FolioNum");
            dtMFReturnsHolding.Columns.Add("InvStartDate");
            dtMFReturnsHolding.Columns.Add("BalanceUnits", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("AvgPrice", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("InvestedCost", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("CurrentNAV", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("CurrentNAVDate");
            dtMFReturnsHolding.Columns.Add("CurrentValue", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("DVR", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("DVP", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("PL", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("ABS");
            dtMFReturnsHolding.Columns.Add("XIRR");
            dtMFReturnsHolding.Columns.Add("PAIC_AssetInstrumentCategoryName");
            DataTable dtMFCustomerTotalXIRR = new DataTable();
            dtMFCustomerTotalXIRR.Columns.Add("CustomerName");
            dtMFCustomerTotalXIRR.Columns.Add("PortfolioName");
            dtMFCustomerTotalXIRR.Columns.Add("XIRR");
            dtMFCustomerTotalXIRR.Columns.Add("ABS");
            DataSet dsMFHoldingReturns = new DataSet();
            DataTable dtAllXirr = new DataTable();
            if (dtTranxn.Rows.Count > 0)
            {
                DataSet ds = CreateInvetmentsPairingAccountStockWise(dtTranxn.Select("TranxnTypeCode <>'DVP'", "CustomerName ASC,ScripName ASC,TransactionDate ASC").CopyToDataTable());

                DataTable dtBuy = ds.Tables[0];
                dtBuy.Columns.Add("AmountNew", typeof(decimal), "Units*Price");
                dtBuy.Columns.Add("CurrentAmount", typeof(decimal), "Units*MktPrice");
                dtBuy.Columns.Add("CostOfSales", typeof(decimal), "(UnitsBefore-Units)*Price");
                dtTranxn.Columns.Add("AmountNew", typeof(decimal), "Amount");
                DataTable dtSchemeList = new DataView(dtBuy).ToTable(true, new[] { "CustomerName", "CustomerId", "PortfolioName", "PortfolioId", "FolioNum", "ScripCode", "ScripName", "MktPrice", "MktPriceDate", "PAIC_AssetInstrumentCategoryName" });
                DataTable dtCustomerList = new DataView(dtBuy).ToTable(true, new[] { "CustomerName", "CustomerId", "PortfolioName", "PortfolioId" });

                foreach (DataRow dr in dtSchemeList.Rows)
                {

                    decimal BalanceUnits = 0;
                    BalanceUnits = decimal.Parse(dtBuy.Compute("Sum(Units)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());
                    if (BalanceUnits > 1)
                    {
                        DataRow drNew = dtMFReturnsHolding.NewRow();
                        drNew["CustomerName"] = dr["CustomerName"].ToString();
                        drNew["CustomerId"] = dr["CustomerId"].ToString();
                        drNew["PortfolioName"] = dr["PortfolioName"].ToString();
                        drNew["PortfolioId"] = dr["PortfolioId"].ToString();
                        drNew["ScripName"] = dr["ScripName"].ToString();
                        drNew["FolioNum"] = dr["FolioNum"].ToString();
                        drNew["PAIC_AssetInstrumentCategoryName"] = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        drNew["InvStartDate"] = DateTime.Parse(dtBuy.Compute("MIN(TransactionDate)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND Units<>0").ToString()).ToString("dd-MMM-yy");
                        drNew["CurrentNAV"] = decimal.Parse(dr["MktPrice"].ToString());
                        if (dr["MktPriceDate"].ToString() != "")
                            drNew["CurrentNAVDate"] = DateTime.Parse(dr["MktPriceDate"].ToString()).ToString("dd-MMM-yy");

                        if (BalanceUnits > 0)
                        {
                            drNew["BalanceUnits"] = BalanceUnits.ToString();
                            drNew["InvestedCost"] = decimal.Parse(dtBuy.Compute("Sum(AmountNew)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' ").ToString());
                            drNew["AvgPrice"] = decimal.Parse(drNew["InvestedCost"].ToString()) / BalanceUnits;
                            drNew["CurrentValue"] = decimal.Parse(dr["MktPrice"].ToString()) * BalanceUnits;

                        }
                        else
                        {
                            drNew["InvestedCost"] = 0;
                            drNew["DVR"] = 0;
                            drNew["CurrentValue"] = 0;

                        }
                        drNew["PL"] = decimal.Parse(drNew["CurrentValue"].ToString()) - decimal.Parse(drNew["InvestedCost"].ToString());

                        if (dtBuy.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TranxnTypeCode='DVR'").Count() > 0)
                            drNew["DVR"] = decimal.Parse(dtBuy.Compute("Sum(AmountNew)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TranxnTypeCode='DVR'").ToString());



                        if (dtTranxn.Select("TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TransactionDate >='" + DateTime.Parse(drNew["InvStartDate"].ToString()).ToString("MM/dd/yyyy") + "'").Count() > 0)
                            drNew["DVP"] = decimal.Parse(dtTranxn.Compute("Sum(Amount)", "TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TransactionDate >='" + DateTime.Parse(drNew["InvStartDate"].ToString()).ToString("MM/dd/yyyy") + "'").ToString());
                       

                        if (decimal.Parse(drNew["InvestedCost"].ToString()) != 0)
                            drNew["ABS"] = Math.Round((decimal.Parse(drNew["PL"].ToString()) / (decimal.Parse(drNew["InvestedCost"].ToString()))) * 100, 2);
                        if (dtBuy.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND Units<>0 AND AmountNew<>0 AND TranxnTypeCode<>'DVR'").Count() > 0)
                        {
                            DataTable dtXirr = new DataView(dtBuy.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND Units<>0 AND AmountNew<>0 AND TranxnTypeCode<>'DVR'", string.Empty).CopyToDataTable()).ToTable(false, new string[] { "TransactionDate", "AmountNew", "BuySell", "PortfolioId" });
                            dtAllXirr.Merge(dtXirr);
                            if(dtTranxn.Select("TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TransactionDate >='" + DateTime.Parse(drNew["InvStartDate"].ToString()).ToString("MM/dd/yyyy") + "'").Count() > 0)
                            {
                                dtAllXirr.Merge(new DataView(dtTranxn.Select("TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TransactionDate >='" + DateTime.Parse(drNew["InvStartDate"].ToString()).ToString("MM/dd/yyyy") + "'", string.Empty).CopyToDataTable()).ToTable(false, new string[] { "TransactionDate", "AmountNew", "BuySell", "PortfolioId" }));
                                dtXirr.Merge(new DataView(dtTranxn.Select("TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TransactionDate >='" + DateTime.Parse(drNew["InvStartDate"].ToString()).ToString("MM/dd/yyyy") + "'", string.Empty).CopyToDataTable()).ToTable(false, new string[] { "TransactionDate", "AmountNew", "BuySell", "PortfolioId" }));
                            }
                            dtXirr.Columns["TransactionDate"].DataType = Type.GetType("System.DateTime");
                            dtXirr.DefaultView.Sort = "TransactionDate ASC";
                            DataTable dt = dtXirr.DefaultView.ToTable();
                            double[] transactionAmount = new double[dtXirr.Rows.Count + 1];
                            DateTime[] transactionDate = new DateTime[dtXirr.Rows.Count + 1];
                            int tempCount = 0;
                            foreach (DataRow drRow in dt.Rows)
                            {
                                if (drRow["BuySell"].ToString() != "S")
                                    transactionAmount[tempCount] = -(double.Parse(drRow["AmountNew"].ToString()));
                                else
                                    transactionAmount[tempCount] = (double.Parse(drRow["AmountNew"].ToString()));
                                transactionDate[tempCount] = DateTime.Parse(drRow["TransactionDate"].ToString());
                                tempCount++;
                            }

                            transactionAmount[tempCount] = double.Parse(drNew["CurrentValue"].ToString());
                            transactionDate[tempCount] = ToDate;

                            double xirr = CalculateXIRR(transactionAmount, transactionDate);
                            if (xirr < 10000)
                                drNew["XIRR"] = decimal.Parse(xirr.ToString());
                            else
                                drNew["XIRR"] = 0;
                        }
                        else
                            drNew["XIRR"] = 0;

                        dtMFReturnsHolding.Rows.Add(drNew);
                    }
                }
                dsMFHoldingReturns.Tables.Add(dtMFReturnsHolding);
                if (dtAllXirr.Rows.Count > 0 && dtMFReturnsHolding.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomerList.Rows)
                    {
                        if (dtAllXirr.Select("PortfolioId='" + dr["PortfolioId"].ToString() + "'").Count() > 0)
                        {
                            DataRow drPortfolioXirr = dtMFCustomerTotalXIRR.NewRow();
                            drPortfolioXirr["CustomerName"] = dr["CustomerName"].ToString();
                            drPortfolioXirr["PortfolioName"] = dr["PortfolioName"].ToString();

                            DataTable dtXirr = new DataView(dtAllXirr.Select("PortfolioId='" + dr["PortfolioId"].ToString() + "'", string.Empty).CopyToDataTable()).ToTable(false, new string[] { "TransactionDate", "AmountNew", "BuySell", "PortfolioId" });
                            dtXirr.Columns["TransactionDate"].DataType = Type.GetType("System.DateTime");
                            dtXirr.DefaultView.Sort = "TransactionDate ASC";
                            DataTable dt = dtXirr.DefaultView.ToTable();
                            double[] transactionAmount = new double[dtXirr.Rows.Count + 1];
                            DateTime[] transactionDate = new DateTime[dtXirr.Rows.Count + 1];
                            int tempCount = 0;
                            foreach (DataRow drRow in dt.Rows)
                            {
                                if (drRow["BuySell"].ToString() != "S")
                                    transactionAmount[tempCount] = -(double.Parse(drRow["AmountNew"].ToString()));
                                else
                                    transactionAmount[tempCount] = (double.Parse(drRow["AmountNew"].ToString()));
                                transactionDate[tempCount] = DateTime.Parse(drRow["TransactionDate"].ToString());
                                tempCount++;
                            }

                            transactionAmount[tempCount] = double.Parse(dtMFReturnsHolding.Compute("Sum(CurrentValue)", "PortfolioId='" + dr["PortfolioId"].ToString() + "'").ToString());
                            transactionDate[tempCount] = DateTime.Now;

                            double xirr = CalculateXIRR(transactionAmount, transactionDate);
                            if (xirr < 10000)
                                drPortfolioXirr["XIRR"] = decimal.Parse(xirr.ToString());
                            else
                                drPortfolioXirr["XIRR"] = 0;

                            drPortfolioXirr["ABS"] = Math.Round(double.Parse(dtMFReturnsHolding.Compute("Sum(PL)", "PortfolioId='" + dr["PortfolioId"].ToString() + "'").ToString()) * 100 / double.Parse(dtMFReturnsHolding.Compute("Sum(InvestedCost)", "PortfolioId='" + dr["PortfolioId"].ToString() + "'").ToString()), 2);
                            dtMFCustomerTotalXIRR.Rows.Add(drPortfolioXirr);
                        }
                    }

                }
                dsMFHoldingReturns.Tables.Add(dtMFCustomerTotalXIRR);
            }
            else
            {
                dsMFHoldingReturns.Tables.Add(dtMFReturnsHolding);
                dsMFHoldingReturns.Tables.Add(dtMFCustomerTotalXIRR);
            }
            return dsMFHoldingReturns;

        }
        public DataSet CreateCustomerMFComprehensive(int C_CustomerId, string PortfolioIds, DateTime? FromDate, DateTime ToDate)
        {
            DataTable dtTranxn = MFReportEngineDao.GetCustomerMFTransactions(C_CustomerId, PortfolioIds, FromDate, ToDate).Tables[0];
            DataSet dsMFComprehensive = new DataSet();
            DataTable dtMFReturnsHolding = new DataTable();
            dtMFReturnsHolding.Columns.Add("CustomerName");
            dtMFReturnsHolding.Columns.Add("CustomerId");
            dtMFReturnsHolding.Columns.Add("PortfolioName");
            dtMFReturnsHolding.Columns.Add("PortfolioId");
            dtMFReturnsHolding.Columns.Add("ScripName");
            dtMFReturnsHolding.Columns.Add("FolioNum");
            dtMFReturnsHolding.Columns.Add("InvStartDate");
            dtMFReturnsHolding.Columns.Add("BalanceUnits", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("AvgPrice", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("InvestedCost", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("CurrentNAV", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("CurrentNAVDate");
            dtMFReturnsHolding.Columns.Add("CurrentValue", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("RedeemedAmt", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("DVR", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("DVP", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("PL", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("ABS");
            dtMFReturnsHolding.Columns.Add("XIRR");
            dtMFReturnsHolding.Columns.Add("PAIC_AssetInstrumentCategoryName");
            DataTable dtMFCustomerTotalXIRR = new DataTable();
            dtMFCustomerTotalXIRR.Columns.Add("CustomerName");
            dtMFCustomerTotalXIRR.Columns.Add("PortfolioName");
            dtMFCustomerTotalXIRR.Columns.Add("XIRR");
            dtMFCustomerTotalXIRR.Columns.Add("ABS");
            if (dtTranxn.Rows.Count > 0)
            {
                DataSet ds = CreateInvetmentsPairingAccountStockWise(dtTranxn.Select("TranxnTypeCode <>'DVP'", "CustomerName ASC,ScripName ASC,TransactionDate ASC").CopyToDataTable());
                DataTable dtBuy = ds.Tables[0];
                DataTable dtSell = ds.Tables[1];
                dtBuy.Columns.Add("AmountNew", typeof(decimal), "Units*Price");
                dtBuy.Columns.Add("CurrentAmount", typeof(decimal), "Units*MktPrice");
                dtBuy.Columns.Add("CostOfSales", typeof(decimal), "(UnitsBefore-Units)*Price");
                DataTable dtSchemeList = new DataView(dtBuy).ToTable(true, new[] { "CustomerName", "CustomerId", "PortfolioName", "PortfolioId", "FolioNum", "ScripCode", "ScripName", "MktPrice", "MktPriceDate", "PAIC_AssetInstrumentCategoryName" });
                DataTable dtCustomerList = new DataView(dtBuy).ToTable(true, new[] { "CustomerName", "CustomerId", "PortfolioName", "PortfolioId" });

                foreach (DataRow dr in dtSchemeList.Rows)
                {
                    //RedeemedAmt
                    decimal BalanceUnits = 0;
                    BalanceUnits = decimal.Parse(dtBuy.Compute("Sum(Units)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());

                    DataRow drNew = dtMFReturnsHolding.NewRow();
                    drNew["CustomerName"] = dr["CustomerName"].ToString();
                    drNew["CustomerId"] = dr["CustomerId"].ToString();
                    drNew["PortfolioName"] = dr["PortfolioName"].ToString();
                    drNew["PortfolioId"] = dr["PortfolioId"].ToString();
                    drNew["ScripName"] = dr["ScripName"].ToString();
                    drNew["FolioNum"] = dr["FolioNum"].ToString();
                    drNew["PAIC_AssetInstrumentCategoryName"] = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                    drNew["InvStartDate"] = DateTime.Parse(dtBuy.Compute("MIN(TransactionDate)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString()).ToString("dd-MMM-yy");
                    drNew["CurrentNAV"] = decimal.Parse(dr["MktPrice"].ToString());
                    if (dr["MktPriceDate"].ToString() != "")
                        drNew["CurrentNAVDate"] = DateTime.Parse(dr["MktPriceDate"].ToString()).ToString("dd-MMM-yy");
                    else
                        drNew["CurrentNAVDate"] = "";
                    if (dtBuy.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").Count() > 0)
                        drNew["InvestedCost"] = decimal.Parse(dtBuy.Compute("Sum(Amount)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());
                    else
                        drNew["InvestedCost"] = 0;
                    if (BalanceUnits > 0)
                    {
                        drNew["BalanceUnits"] = BalanceUnits.ToString();
                        drNew["AvgPrice"] = decimal.Parse(dtBuy.Compute("Sum(AmountNew)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString()) / BalanceUnits;
                        drNew["CurrentValue"] = decimal.Parse(dr["MktPrice"].ToString()) * BalanceUnits;
                    }
                    else
                    {
                        drNew["CurrentValue"] = 0;
                    }


                    if (dtTranxn.Select("TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").Count() > 0)
                        drNew["DVP"] = decimal.Parse(dtTranxn.Compute("Sum(Amount)", "TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());
                    else
                        drNew["DVP"] = 0;

                    if (dtSell.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").Count() > 0)
                        drNew["RedeemedAmt"] = decimal.Parse(dtSell.Compute("Sum(Amount)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());
                    else
                        drNew["RedeemedAmt"] = 0;

                    if (dtBuy.Select("TranxnTypeCode ='DVR' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").Count() > 0)
                        drNew["DVR"] = decimal.Parse(dtBuy.Compute("Sum(Amount)", "TranxnTypeCode ='DVR' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());

                    drNew["PL"] = decimal.Parse(drNew["RedeemedAmt"].ToString()) + decimal.Parse(drNew["CurrentValue"].ToString()) - decimal.Parse(drNew["InvestedCost"].ToString()) + decimal.Parse(drNew["DVP"].ToString());

                    if (decimal.Parse(drNew["InvestedCost"].ToString()) != 0)
                        drNew["ABS"] = Math.Round((decimal.Parse(drNew["PL"].ToString()) / (decimal.Parse(drNew["InvestedCost"].ToString()))) * 100, 2);
                    if (dtTranxn.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TranxnTypeCode <>'DVR' ").Count() > 0)
                    {
                        DataTable dtXirr = new DataView(dtTranxn.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TranxnTypeCode <>'DVR' ", string.Empty).CopyToDataTable()).ToTable(false, new string[] { "TransactionDate", "Units", "Amount", "MktPrice", "BuySell", "PAIC_AssetInstrumentCategoryCode" });
                        dtXirr.Columns["TransactionDate"].DataType = Type.GetType("System.DateTime");
                        dtXirr.DefaultView.Sort = "TransactionDate ASC";
                        DataTable dt = dtXirr.DefaultView.ToTable();
                        double[] transactionAmount = new double[dtXirr.Rows.Count + 1];
                        DateTime[] transactionDate = new DateTime[dtXirr.Rows.Count + 1];
                        int tempCount = 0;
                        foreach (DataRow drRow in dt.Rows)
                        {
                            if (drRow["BuySell"].ToString() != "S")
                                transactionAmount[tempCount] = -(double.Parse(drRow["Amount"].ToString()));
                            else
                                transactionAmount[tempCount] = (double.Parse(drRow["Amount"].ToString()));
                            transactionDate[tempCount] = DateTime.Parse(drRow["TransactionDate"].ToString());
                            tempCount++;
                        }

                        transactionAmount[tempCount] = double.Parse(drNew["CurrentValue"].ToString());
                        transactionDate[tempCount] = ToDate;

                        double xirr = CalculateXIRR(transactionAmount, transactionDate);
                        if (xirr < 10000)
                            drNew["XIRR"] = decimal.Parse(xirr.ToString());
                        else
                            drNew["XIRR"] = 0;
                    }
                    else
                        drNew["XIRR"] = 0;

                    dtMFReturnsHolding.Rows.Add(drNew);
                }

                dsMFComprehensive.Tables.Add(dtMFReturnsHolding);
                foreach (DataRow dr in dtCustomerList.Rows)
                {

                    DataRow drPortfolioXirr = dtMFCustomerTotalXIRR.NewRow();
                    drPortfolioXirr["CustomerName"] = dr["CustomerName"].ToString();
                    drPortfolioXirr["PortfolioName"] = dr["PortfolioName"].ToString();
                    if (dtTranxn.Select("PortfolioId='" + dr["PortfolioId"].ToString() + "'  AND TranxnTypeCode <>'DVR' AND Amount<>0").Count() > 0)
                    {
                        DataTable dtXirr = new DataView(dtTranxn.Select("PortfolioId='" + dr["PortfolioId"].ToString() + "' AND TranxnTypeCode <>'DVR' AND Amount<>0 ", string.Empty).CopyToDataTable()).ToTable(false, new string[] { "TransactionDate", "Units", "Amount", "MktPrice", "BuySell" });
                        dtXirr.Columns["TransactionDate"].DataType = Type.GetType("System.DateTime");
                        dtXirr.DefaultView.Sort = "TransactionDate ASC";
                        DataTable dt = dtXirr.DefaultView.ToTable();
                        double[] transactionAmount = new double[dtXirr.Rows.Count + 1];
                        DateTime[] transactionDate = new DateTime[dtXirr.Rows.Count + 1];
                        int tempCount = 0;
                        foreach (DataRow drRow in dt.Rows)
                        {
                            if (drRow["BuySell"].ToString() != "S")
                                transactionAmount[tempCount] = -(double.Parse(drRow["Amount"].ToString()));
                            else
                                transactionAmount[tempCount] = (double.Parse(drRow["Amount"].ToString()));
                            transactionDate[tempCount] = DateTime.Parse(drRow["TransactionDate"].ToString());
                            tempCount++;
                        }

                        transactionAmount[tempCount] = double.Parse(dtMFReturnsHolding.Compute("Sum(CurrentValue)", "PortfolioId='" + dr["PortfolioId"].ToString() + "'").ToString());
                        transactionDate[tempCount] = DateTime.Now;

                        double xirr = CalculateXIRR(transactionAmount, transactionDate);
                        if (xirr < 10000)
                            drPortfolioXirr["XIRR"] = decimal.Parse(xirr.ToString());
                        else
                            drPortfolioXirr["XIRR"] = 0;
                    }
                    else
                        drPortfolioXirr["XIRR"] = 0;
                    drPortfolioXirr["ABS"] = Math.Round(double.Parse(dtMFReturnsHolding.Compute("Sum(PL)", "PortfolioId='" + dr["PortfolioId"].ToString() + "'").ToString()) * 100 / double.Parse(dtMFReturnsHolding.Compute("Sum(InvestedCost)", "PortfolioId='" + dr["PortfolioId"].ToString() + "'").ToString()), 2);
                    dtMFCustomerTotalXIRR.Rows.Add(drPortfolioXirr);
                }

                dsMFComprehensive.Tables.Add(dtMFCustomerTotalXIRR);
            }
            else
            {
                dsMFComprehensive.Tables.Add(dtMFReturnsHolding);
                dsMFComprehensive.Tables.Add(dtMFCustomerTotalXIRR);
            }
            return dsMFComprehensive;
        }
        public DataTable CreateCustomerCapitalGainDetails(int C_CustomerId, string PortfolioIds, DateTime? FromDate, DateTime ToDate)
        {
            DataTable dtTranxn = MFReportEngineDao.GetCustomerMFTransactions(C_CustomerId, PortfolioIds, null, ToDate).Tables[0];
            DataTable dtCapitalGainDetails = new DataTable();
            dtCapitalGainDetails.Columns.Add("CustomerName", typeof(string));
            dtCapitalGainDetails.Columns.Add("CustomerId");
            dtCapitalGainDetails.Columns.Add("PortfolioName", typeof(string));
            dtCapitalGainDetails.Columns.Add("PortfolioId");
            dtCapitalGainDetails.Columns.Add("ScripName", typeof(string));
            dtCapitalGainDetails.Columns.Add("ScripCode", typeof(string));
            dtCapitalGainDetails.Columns.Add("FolioNum", typeof(string));
            dtCapitalGainDetails.Columns.Add("Units", typeof(decimal));
            dtCapitalGainDetails.Columns.Add("SellDate", typeof(DateTime));
            dtCapitalGainDetails.Columns.Add("SellRate", typeof(decimal));
            dtCapitalGainDetails.Columns.Add("SellAmount", typeof(decimal));
            dtCapitalGainDetails.Columns.Add("BuyDate", typeof(DateTime));
            dtCapitalGainDetails.Columns.Add("BuyRate", typeof(decimal));
            dtCapitalGainDetails.Columns.Add("BuyAmount", typeof(decimal));
            dtCapitalGainDetails.Columns.Add("Days", typeof(int));
            dtCapitalGainDetails.Columns.Add("LTCG", typeof(decimal));
            dtCapitalGainDetails.Columns.Add("STCG", typeof(decimal));
            dtCapitalGainDetails.Columns.Add("PAIC_AssetInstrumentCategoryName", typeof(string));
            DataTable tempdtCapitalGainDetails = new DataTable();
            if (dtTranxn.Select("TranxnTypeCode <>'DVP'").Count() > 0)
            {
                tempdtCapitalGainDetails = CreateCapitalGainDetails(dtTranxn.Select("TranxnTypeCode <>'DVP'", "CustomerName ASC,ScripName ASC,TransactionDate ASC").CopyToDataTable());
                tempdtCapitalGainDetails.DefaultView.RowFilter = "SellDate >= '" + FromDate + "' AND SellDate <= '" + ToDate + "'";
                dtCapitalGainDetails.Merge(tempdtCapitalGainDetails.DefaultView.ToTable());
            }
            //tempdtCapitalGainDetails.DefaultView.RowFilter = "SellDate >= '01/13/2000 00:00:00' AND SellDate <= '01/13/2003 00:00:00'";
            return dtCapitalGainDetails;

        }
        public DataTable CreateCustomerCapitalGainSummary(int C_CustomerId, string PortfolioIds, DateTime? FromDate, DateTime ToDate)
        {
            DataTable dtTranxn = MFReportEngineDao.GetCustomerMFTransactions(C_CustomerId, PortfolioIds, null, ToDate).Tables[0];
            DataTable dtCapitalGainDetails = new DataTable();
            DataTable tempdtCapitalGainDetails = new DataTable();
            DataTable dtCapitalGainSummary = new DataTable();
            dtCapitalGainSummary.Columns.Add("CustomerName");
            dtCapitalGainSummary.Columns.Add("CustomerId");
            dtCapitalGainSummary.Columns.Add("PortfolioName");
            dtCapitalGainSummary.Columns.Add("PortfolioId");
            dtCapitalGainSummary.Columns.Add("ScripName");
            dtCapitalGainSummary.Columns.Add("ScripCode");
            dtCapitalGainSummary.Columns.Add("FolioNum");
            dtCapitalGainSummary.Columns.Add("LTCG", typeof(decimal));
            dtCapitalGainSummary.Columns.Add("STCG", typeof(decimal));
            dtCapitalGainSummary.Columns.Add("PAIC_AssetInstrumentCategoryName");
            if (dtTranxn.Select("TranxnTypeCode <>'DVP'").Count() > 0)
            {
                tempdtCapitalGainDetails = CreateCapitalGainDetails(dtTranxn.Select("TranxnTypeCode <>'DVP'", "CustomerName ASC,ScripName ASC,TransactionDate ASC").CopyToDataTable());
                tempdtCapitalGainDetails.DefaultView.RowFilter = "SellDate >= '" + FromDate + "' AND SellDate <= '" + ToDate + "'";
                dtCapitalGainDetails = tempdtCapitalGainDetails.DefaultView.ToTable();
                DataTable dtSchemeList = new DataView(dtCapitalGainDetails).ToTable(true, new[] { "CustomerName", "CustomerId", "PortfolioName", "PortfolioId", "FolioNum", "ScripCode", "ScripName", "PAIC_AssetInstrumentCategoryName" });
                foreach (DataRow dr in dtSchemeList.Rows)
                {
                    DataRow drNew = dtCapitalGainSummary.NewRow();
                    drNew["CustomerName"] = dr["CustomerName"].ToString();
                    drNew["CustomerId"] = dr["CustomerId"].ToString();
                    drNew["PortfolioName"] = dr["PortfolioName"].ToString();
                    drNew["PortfolioId"] = dr["PortfolioId"].ToString();
                    drNew["ScripName"] = dr["ScripName"].ToString();
                    drNew["ScripCode"] = dr["ScripCode"].ToString();
                    drNew["FolioNum"] = dr["FolioNum"].ToString();
                    drNew["PAIC_AssetInstrumentCategoryName"] = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                    drNew["LTCG"] = decimal.Parse(dtCapitalGainDetails.Compute("Sum(LTCG)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());
                    drNew["STCG"] = decimal.Parse(dtCapitalGainDetails.Compute("Sum(STCG)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());
                    dtCapitalGainSummary.Rows.Add(drNew);

                }
            }
            return dtCapitalGainSummary;
        }
        public DataTable CreateCustomerMFTraxnReport(int C_CustomerId, string PortfolioIds, DateTime? FromDate, DateTime ToDate)
        {
            DataTable dtTranxn = MFReportEngineDao.GetCustomerMFTraxnReport(C_CustomerId, PortfolioIds, FromDate, ToDate).Tables[0];
            return dtTranxn;
        }
        public DataSet CreateCustomerMFReturnsHoldingWithBankDetails(int C_CustomerId, string PortfolioIds, DateTime? FromDate, DateTime ToDate)
        {
            DataSet dsMFHoldingReturns = new DataSet();
            DataTable dtMFReturnsHolding = new DataTable();
            dtMFReturnsHolding.Columns.Add("CustomerName");
            dtMFReturnsHolding.Columns.Add("CustomerId");
            dtMFReturnsHolding.Columns.Add("PortfolioName");
            dtMFReturnsHolding.Columns.Add("PortfolioId");
            dtMFReturnsHolding.Columns.Add("ScripName");
            dtMFReturnsHolding.Columns.Add("FolioNum");
            dtMFReturnsHolding.Columns.Add("InvStartDate");
            dtMFReturnsHolding.Columns.Add("BalanceUnits", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("AvgPrice", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("InvestedCost", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("CurrentNAV", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("CurrentNAVDate");
            dtMFReturnsHolding.Columns.Add("CurrentValue", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("DVP", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("PL", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("ABS");
            dtMFReturnsHolding.Columns.Add("XIRR");
            dtMFReturnsHolding.Columns.Add("PAIC_AssetInstrumentCategoryName");
            dtMFReturnsHolding.Columns.Add("BankName");
            dtMFReturnsHolding.Columns.Add("MOH");
            //dtMFReturnsHolding.Columns.Add("AccountNum");
            DataTable dtMFCustomerTotalXIRR = new DataTable();
            dtMFCustomerTotalXIRR.Columns.Add("CustomerName");
            dtMFCustomerTotalXIRR.Columns.Add("PortfolioName");
            dtMFCustomerTotalXIRR.Columns.Add("XIRR");

            DataTable dtTranxn = MFReportEngineDao.GetCustomerMFTransactionsWithBankDetails(C_CustomerId, PortfolioIds, FromDate, ToDate).Tables[0];

            if (dtTranxn.Select("TranxnTypeCode <>'DVP'").Count() > 0)
            {
                DataSet ds = CreateInvetmentsPairingAccountStockWise(dtTranxn.Select("TranxnTypeCode <>'DVP'", "CustomerName ASC,ScripName ASC,TransactionDate ASC").CopyToDataTable());

                DataTable dtBuy = ds.Tables[0];
                dtBuy.Columns.Add("AmountNew", typeof(decimal), "Units*Price");
                dtBuy.Columns.Add("CurrentAmount", typeof(decimal), "Units*MktPrice");
                dtBuy.Columns.Add("CostOfSales", typeof(decimal), "(UnitsBefore-Units)*Price");
                DataTable dtSchemeList = new DataView(dtBuy).ToTable(true, new[] { "CustomerName", "CustomerId", "PortfolioName", "PortfolioId", "FolioNum", "ScripCode", "ScripName", "MktPrice", "MktPriceDate", "PAIC_AssetInstrumentCategoryName", "BankName", "MOH" });
                DataTable dtCustomerList = new DataView(dtBuy).ToTable(true, new[] { "CustomerName", "CustomerId", "PortfolioName", "PortfolioId" });

                if (dtTranxn.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSchemeList.Rows)
                    {

                        decimal BalanceUnits = 0;
                        BalanceUnits = decimal.Parse(dtBuy.Compute("Sum(Units)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());
                        if (BalanceUnits > 0)
                        {
                            DataRow drNew = dtMFReturnsHolding.NewRow();
                            drNew["CustomerName"] = dr["CustomerName"].ToString();
                            drNew["CustomerId"] = dr["CustomerId"].ToString();
                            drNew["PortfolioName"] = dr["PortfolioName"].ToString();
                            drNew["PortfolioId"] = dr["PortfolioId"].ToString();
                            drNew["ScripName"] = dr["ScripName"].ToString();
                            drNew["FolioNum"] = dr["FolioNum"].ToString();
                            drNew["PAIC_AssetInstrumentCategoryName"] = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                            drNew["BankName"] = dr["BankName"].ToString();
                            drNew["MOH"] = dr["MOH"].ToString();
                            //drNew["AccountNum"] = dr["AccountNum"].ToString();
                            drNew["InvStartDate"] = DateTime.Parse(dtBuy.Compute("MIN(TransactionDate)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND Units<>0").ToString()).ToString("dd-MMM-yy");
                            drNew["CurrentNAV"] = decimal.Parse(dr["MktPrice"].ToString());
                            drNew["CurrentNAVDate"] = DateTime.Parse(dr["MktPriceDate"].ToString()).ToString("dd-MMM-yy");

                            if (BalanceUnits > 0)
                            {
                                drNew["BalanceUnits"] = BalanceUnits.ToString();
                                drNew["InvestedCost"] = decimal.Parse(dtBuy.Compute("Sum(AmountNew)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());
                                drNew["AvgPrice"] = decimal.Parse(drNew["InvestedCost"].ToString()) / BalanceUnits;
                                drNew["CurrentValue"] = decimal.Parse(dr["MktPrice"].ToString()) * BalanceUnits;
                            }
                            else
                            {
                                drNew["InvestedCost"] = 0;
                                drNew["CurrentValue"] = 0;
                            }
                            drNew["PL"] = decimal.Parse(drNew["CurrentValue"].ToString()) - decimal.Parse(drNew["InvestedCost"].ToString());
                            if (dtTranxn.Select("TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").Count() > 0)
                                drNew["DVP"] = decimal.Parse(dtTranxn.Compute("Sum(Amount)", "TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());

                            if (decimal.Parse(drNew["InvestedCost"].ToString()) != 0)
                                drNew["ABS"] = Math.Round((decimal.Parse(drNew["PL"].ToString()) / (decimal.Parse(drNew["InvestedCost"].ToString()))) * 100, 2);

                            DataTable dtXirr = new DataView(dtTranxn.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'", string.Empty).CopyToDataTable()).ToTable(false, new string[] { "TransactionDate", "Units", "Amount", "MktPrice", "BuySell", "PAIC_AssetInstrumentCategoryCode" });
                            dtXirr.Columns["TransactionDate"].DataType = Type.GetType("System.DateTime");
                            dtXirr.DefaultView.Sort = "TransactionDate ASC";
                            DataTable dt = dtXirr.DefaultView.ToTable();
                            double[] transactionAmount = new double[dtXirr.Rows.Count + 1];
                            DateTime[] transactionDate = new DateTime[dtXirr.Rows.Count + 1];
                            int tempCount = 0;
                            foreach (DataRow drRow in dt.Rows)
                            {
                                if (drRow["BuySell"].ToString() != "S")
                                    transactionAmount[tempCount] = -(double.Parse(drRow["Amount"].ToString()));
                                else
                                    transactionAmount[tempCount] = (double.Parse(drRow["Amount"].ToString()));
                                transactionDate[tempCount] = DateTime.Parse(drRow["TransactionDate"].ToString());
                                tempCount++;
                            }

                            transactionAmount[tempCount] = double.Parse(drNew["CurrentValue"].ToString());
                            transactionDate[tempCount] = ToDate;

                            double xirr = CalculateXIRR(transactionAmount, transactionDate);
                            if (xirr < 10000)
                                drNew["XIRR"] = decimal.Parse(xirr.ToString());
                            else
                                drNew["XIRR"] = 0;

                            dtMFReturnsHolding.Rows.Add(drNew);
                        }
                    }
                    dsMFHoldingReturns.Tables.Add(dtMFReturnsHolding);
                    if (dtMFReturnsHolding.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCustomerList.Rows)
                        {
                            DataRow drPortfolioXirr = dtMFCustomerTotalXIRR.NewRow();
                            drPortfolioXirr["CustomerName"] = dr["CustomerName"].ToString();
                            drPortfolioXirr["PortfolioName"] = dr["PortfolioName"].ToString();
                            DataTable dtXirr = new DataView(dtTranxn.Select("PortfolioId='" + dr["PortfolioId"].ToString() + "'", string.Empty).CopyToDataTable()).ToTable(false, new string[] { "TransactionDate", "Units", "Amount", "MktPrice", "BuySell" });
                            dtXirr.Columns["TransactionDate"].DataType = Type.GetType("System.DateTime");
                            dtXirr.DefaultView.Sort = "TransactionDate ASC";
                            DataTable dt = dtXirr.DefaultView.ToTable();
                            double[] transactionAmount = new double[dtXirr.Rows.Count + 1];
                            DateTime[] transactionDate = new DateTime[dtXirr.Rows.Count + 1];
                            int tempCount = 0;
                            foreach (DataRow drRow in dt.Rows)
                            {
                                if (drRow["BuySell"].ToString() != "S")
                                    transactionAmount[tempCount] = -(double.Parse(drRow["Amount"].ToString()));
                                else
                                    transactionAmount[tempCount] = (double.Parse(drRow["Amount"].ToString()));
                                transactionDate[tempCount] = DateTime.Parse(drRow["TransactionDate"].ToString());
                                tempCount++;
                            }

                            transactionAmount[tempCount] = double.Parse(dtMFReturnsHolding.Compute("Sum(CurrentValue)", "PortfolioId='" + dr["PortfolioId"].ToString() + "'").ToString());
                            transactionDate[tempCount] = DateTime.Now;

                            double xirr = CalculateXIRR(transactionAmount, transactionDate);
                            if (xirr < 10000)
                                drPortfolioXirr["XIRR"] = decimal.Parse(xirr.ToString());
                            else
                                drPortfolioXirr["XIRR"] = 0;
                            dtMFCustomerTotalXIRR.Rows.Add(drPortfolioXirr);
                        }
                    }
                    dsMFHoldingReturns.Tables.Add(dtMFCustomerTotalXIRR);
                }
                else
                {
                    dsMFHoldingReturns.Tables.Add(dtMFReturnsHolding);
                    dsMFHoldingReturns.Tables.Add(dtMFCustomerTotalXIRR);
                }
            }
            else
            {
                dsMFHoldingReturns.Tables.Add(dtMFReturnsHolding);
                dsMFHoldingReturns.Tables.Add(dtMFCustomerTotalXIRR);
            }

            return dsMFHoldingReturns;

        }
        public DataTable CreateCustomerEligibleCapitalGainDetails(int C_CustomerId, string PortfolioIds, DateTime? FromDate, DateTime ToDate)
        {
            DataTable dtTranxn = MFReportEngineDao.GetCustomerMFTransactions(C_CustomerId, PortfolioIds, FromDate, ToDate).Tables[0];
            DataTable dtCapitalGainDetails = new DataTable();
            DataSet ds = CreateInvetmentsPairingAccountStockWise(dtTranxn);
            DataTable dtTransactionsNew = new DataTable();
            if (dtTranxn.Select("TranxnTypeCode <>'DVP'").Count() > 0)
            {
                if (ds.Tables[0].Select("Units<>0").Count() > 0)
                {
                    dtTransactionsNew = ds.Tables[0].Select("Units>0").CopyToDataTable();


                    DataTable dtAssetSubtypes = new DataView(dtTransactionsNew).ToTable(true, new[] { "CustomerName", "CustomerId", "PortfolioName", "PortfolioId", "FolioNum", "ScripCode", "ScripName", "MktPrice", "PAIC_AssetInstrumentCategoryCode", "PAIC_AssetInstrumentCategoryName" });
                    foreach (DataRow dr in dtAssetSubtypes.Rows)
                    {
                        DataRow drTranxnSell = dtTransactionsNew.NewRow();
                        drTranxnSell["CustomerName"] = dr["CustomerName"].ToString();
                        drTranxnSell["CustomerId"] = dr["CustomerId"].ToString();
                        drTranxnSell["PortfolioName"] = dr["PortfolioName"].ToString();
                        drTranxnSell["PortfolioId"] = dr["PortfolioId"].ToString();
                        drTranxnSell["ScripCode"] = dr["ScripCode"].ToString();
                        drTranxnSell["ScripName"] = dr["ScripName"].ToString();
                        drTranxnSell["FolioNum"] = dr["FolioNum"].ToString();
                        drTranxnSell["TransactionDate"] = DateTime.Now;
                        drTranxnSell["Units"] = decimal.Parse(dtTransactionsNew.Compute("Sum(Units)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString()).ToString();
                        drTranxnSell["UnitsBefore"] = decimal.Parse(drTranxnSell["Units"].ToString()).ToString();
                        drTranxnSell["Price"] = decimal.Parse(dr["MktPrice"].ToString()).ToString();
                        drTranxnSell["Amount"] = decimal.Parse(dr["MktPrice"].ToString()) * decimal.Parse(drTranxnSell["Units"].ToString());
                        drTranxnSell["TranxnTypeCode"] = "SEL";
                        drTranxnSell["BuySell"] = "S";
                        drTranxnSell["PAIC_AssetInstrumentCategoryCode"] = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        drTranxnSell["PAIC_AssetInstrumentCategoryName"] = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        dtTransactionsNew.Rows.Add(drTranxnSell);
                    }
                    dtCapitalGainDetails = CreateCapitalGainDetails(dtTransactionsNew.Select("TranxnTypeCode <>'DVP'", "CustomerName ASC,ScripName ASC,TransactionDate ASC").CopyToDataTable());
                }
            }
            return dtCapitalGainDetails;
        }
        public DataTable CreateCustomerEligibleCapitalGainSummary(int C_CustomerId, string PortfolioIds, DateTime? FromDate, DateTime ToDate)
        {
            DataTable dtTranxn = MFReportEngineDao.GetCustomerMFTransactions(C_CustomerId, PortfolioIds, FromDate, ToDate).Tables[0];
            DataTable dtCapitalGainSummary = new DataTable();
            dtCapitalGainSummary.Columns.Add("CustomerName");
            dtCapitalGainSummary.Columns.Add("CustomerId");
            dtCapitalGainSummary.Columns.Add("PortfolioName");
            dtCapitalGainSummary.Columns.Add("PortfolioId");
            dtCapitalGainSummary.Columns.Add("ScripName");
            dtCapitalGainSummary.Columns.Add("ScripCode");
            dtCapitalGainSummary.Columns.Add("FolioNum");
            dtCapitalGainSummary.Columns.Add("LTCG", typeof(decimal));
            dtCapitalGainSummary.Columns.Add("STCG", typeof(decimal));
            dtCapitalGainSummary.Columns.Add("PAIC_AssetInstrumentCategoryName");
            if (dtTranxn.Select("TranxnTypeCode <>'DVP'").Count() > 0)
            {
                DataSet ds = CreateInvetmentsPairingAccountStockWise(dtTranxn);
                DataTable dtTransactionsNew = new DataTable();
                if (ds.Tables[0].Select("Units<>0").Count() > 0)
                {
                    dtTransactionsNew = ds.Tables[0].Select("Units>0").CopyToDataTable();

                    DataTable dtAssetSubtypes = new DataView(dtTransactionsNew).ToTable(true, new[] { "CustomerName", "CustomerId", "PortfolioName", "PortfolioId", "FolioNum", "ScripCode", "ScripName", "MktPrice", "PAIC_AssetInstrumentCategoryCode", "PAIC_AssetInstrumentCategoryName" });
                    foreach (DataRow dr in dtAssetSubtypes.Rows)
                    {
                        DataRow drTranxnSell = dtTransactionsNew.NewRow();
                        drTranxnSell["CustomerName"] = dr["CustomerName"].ToString();
                        drTranxnSell["CustomerId"] = dr["CustomerId"].ToString();
                        drTranxnSell["PortfolioName"] = dr["PortfolioName"].ToString();
                        drTranxnSell["PortfolioId"] = dr["PortfolioId"].ToString();
                        drTranxnSell["ScripCode"] = dr["ScripCode"].ToString();
                        drTranxnSell["ScripName"] = dr["ScripName"].ToString();
                        drTranxnSell["FolioNum"] = dr["FolioNum"].ToString();
                        drTranxnSell["TransactionDate"] = DateTime.Now;
                        drTranxnSell["Units"] = decimal.Parse(dtTransactionsNew.Compute("Sum(Units)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString()).ToString();
                        drTranxnSell["UnitsBefore"] = decimal.Parse(drTranxnSell["Units"].ToString()).ToString();
                        drTranxnSell["Price"] = decimal.Parse(dr["MktPrice"].ToString()).ToString();
                        drTranxnSell["Amount"] = decimal.Parse(dr["MktPrice"].ToString()) * decimal.Parse(drTranxnSell["Units"].ToString());
                        drTranxnSell["TranxnTypeCode"] = "SEL";
                        drTranxnSell["BuySell"] = "S";
                        drTranxnSell["PAIC_AssetInstrumentCategoryCode"] = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        drTranxnSell["PAIC_AssetInstrumentCategoryName"] = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        dtTransactionsNew.Rows.Add(drTranxnSell);
                    }
                    DataTable dtCapitalGainDetails = CreateCapitalGainDetails(dtTransactionsNew.Select("TranxnTypeCode <>'DVP'", "CustomerName ASC,ScripName ASC,TransactionDate ASC").CopyToDataTable());

                    DataTable dtSchemeList = new DataView(dtCapitalGainDetails).ToTable(true, new[] { "CustomerName", "CustomerId", "PortfolioName", "PortfolioId", "FolioNum", "ScripCode", "ScripName", "PAIC_AssetInstrumentCategoryName" });
                    foreach (DataRow dr in dtSchemeList.Rows)
                    {
                        DataRow drNew = dtCapitalGainSummary.NewRow();
                        drNew["CustomerName"] = dr["CustomerName"].ToString();
                        drNew["CustomerId"] = dr["CustomerId"].ToString();
                        drNew["PortfolioName"] = dr["PortfolioName"].ToString();
                        drNew["PortfolioId"] = dr["PortfolioId"].ToString();
                        drNew["ScripName"] = dr["ScripName"].ToString();
                        drNew["ScripCode"] = dr["ScripCode"].ToString();
                        drNew["FolioNum"] = dr["FolioNum"].ToString();
                        drNew["PAIC_AssetInstrumentCategoryName"] = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        drNew["LTCG"] = decimal.Parse(dtCapitalGainDetails.Compute("Sum(LTCG)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());
                        drNew["STCG"] = decimal.Parse(dtCapitalGainDetails.Compute("Sum(STCG)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());
                        dtCapitalGainSummary.Rows.Add(drNew);

                    }
                }
            }
            return dtCapitalGainSummary;

        }

        public DataTable CreateCustomerClosingBalance(int C_CustomerId, string PortfolioIds, DateTime? FromDate, DateTime ToDate)
        {
            DataSet dsTranxns = MFReportEngineDao.GetCustomerMFClosingBalanceTransaction(C_CustomerId, PortfolioIds, FromDate, ToDate);
            return CreatePointToPointTable(dsTranxns.Tables[0], dsTranxns.Tables[1], dsTranxns.Tables[2]);
        }
        public DataSet CreateCustomerTranxnHolding(int C_CustomerId, string PortfolioIds, DateTime? FromDate, DateTime ToDate)
        {
            DataSet dsHoldTranxn = new DataSet();
            DataTable dtTranxn = MFReportEngineDao.GetCustomerMFTransactions(C_CustomerId, PortfolioIds, FromDate, ToDate).Tables[0];
            DataTable dtMFTranxnHolding = new DataTable();
            dtMFTranxnHolding.Columns.Add("CustomerName");
            dtMFTranxnHolding.Columns.Add("CustomerId");
            dtMFTranxnHolding.Columns.Add("PortfolioName");
            dtMFTranxnHolding.Columns.Add("PortfolioId");
            dtMFTranxnHolding.Columns.Add("ScripName");
            dtMFTranxnHolding.Columns.Add("FolioNum");
            dtMFTranxnHolding.Columns.Add("InvStartDate");
            dtMFTranxnHolding.Columns.Add("BalanceUnits", typeof(decimal));
            dtMFTranxnHolding.Columns.Add("AvgPrice", typeof(decimal));
            dtMFTranxnHolding.Columns.Add("InvestedCost", typeof(decimal));
            dtMFTranxnHolding.Columns.Add("CurrentNAV", typeof(decimal));
            dtMFTranxnHolding.Columns.Add("CurrentNAVDate");
            dtMFTranxnHolding.Columns.Add("CurrentValue", typeof(decimal));
            dtMFTranxnHolding.Columns.Add("PL", typeof(decimal));
            dtMFTranxnHolding.Columns.Add("ABS");
            dtMFTranxnHolding.Columns.Add("XIRR");
            dtMFTranxnHolding.Columns.Add("PAIC_AssetInstrumentCategoryName");
            dtMFTranxnHolding.Columns.Add("Days");
            if (dtTranxn.Select("TranxnTypeCode <>'DVP'").Count() > 0)
            {
                DataSet ds = CreateInvetmentsPairingAccountStockWise(dtTranxn.Select("TranxnTypeCode <>'DVP'", "CustomerName ASC,ScripName ASC,TransactionDate ASC").CopyToDataTable());
                if (ds.Tables[0].Select("Units>0").Count() > 0)
                {
                    DataTable dtBuy = ds.Tables[0].Select("Units>0", "CustomerName ASC,ScripName ASC,TransactionDate ASC").CopyToDataTable();
                    dtBuy.Columns.Add("AmountNew", typeof(decimal), "Units*Price");
                    dtBuy.Columns.Add("CurrentAmount", typeof(decimal), "Units*MktPrice");
                    foreach (DataRow dr in dtBuy.Rows)
                    {
                        DataRow drNew = dtMFTranxnHolding.NewRow();
                        drNew["CustomerName"] = dr["CustomerName"].ToString();
                        drNew["CustomerId"] = dr["CustomerId"].ToString();
                        drNew["PortfolioName"] = dr["PortfolioName"].ToString();
                        drNew["PortfolioId"] = dr["PortfolioId"].ToString();
                        drNew["ScripName"] = dr["ScripName"].ToString();
                        drNew["FolioNum"] = dr["FolioNum"].ToString();
                        drNew["PAIC_AssetInstrumentCategoryName"] = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        drNew["CurrentNAV"] = decimal.Parse(dr["MktPrice"].ToString());
                        if (dr["MktPriceDate"].ToString() != "")
                            drNew["CurrentNAVDate"] = DateTime.Parse(dr["MktPriceDate"].ToString()).ToString("dd-MMM-yy");
                        drNew["InvStartDate"] = DateTime.Parse(dr["TransactionDate"].ToString()).ToString("dd-MMM-yyyy");
                        drNew["BalanceUnits"] = decimal.Parse(dr["Units"].ToString());
                        drNew["InvestedCost"] = decimal.Parse(dr["AmountNew"].ToString());
                        drNew["AvgPrice"] = decimal.Parse(dr["Price"].ToString());
                        drNew["CurrentValue"] = decimal.Parse(dr["CurrentAmount"].ToString());
                        drNew["Days"] = (DateTime.Now - DateTime.Parse(dr["TransactionDate"].ToString())).Days;

                        drNew["PL"] = decimal.Parse(drNew["CurrentValue"].ToString()) - decimal.Parse(drNew["InvestedCost"].ToString());

                        if (decimal.Parse(drNew["InvestedCost"].ToString()) != 0)
                            drNew["ABS"] = Math.Round((decimal.Parse(drNew["PL"].ToString()) / (decimal.Parse(drNew["InvestedCost"].ToString()))) * 100, 2);

                        double[] transactionAmount = new double[2];
                        DateTime[] transactionDate = new DateTime[2];

                        transactionAmount[0] = -double.Parse(dr["AmountNew"].ToString());
                        transactionDate[0] = DateTime.Parse(dr["TransactionDate"].ToString());
                        transactionAmount[1] = double.Parse(dr["CurrentAmount"].ToString());
                        transactionDate[1] = DateTime.Now;

                        double xirr = CalculateXIRR(transactionAmount, transactionDate);
                        if (xirr < 10000)
                            drNew["XIRR"] = decimal.Parse(xirr.ToString());
                        else
                            drNew["XIRR"] = 0;

                        dtMFTranxnHolding.Rows.Add(drNew);

                    }
                    dsHoldTranxn.Tables.Add(dtMFTranxnHolding);
                    DataTable dtSchemeList = new DataView(dtBuy).ToTable(true, new[] { "CustomerName", "CustomerId", "PortfolioName", "PortfolioId", "FolioNum", "ScripCode", "ScripName", "MktPrice", "MktPriceDate", "PAIC_AssetInstrumentCategoryName" });
                    DataTable dtMFHoldingXIRR = new DataTable();
                    dtMFHoldingXIRR.Columns.Add("CustomerId");
                    dtMFHoldingXIRR.Columns.Add("PortfolioId");
                    dtMFHoldingXIRR.Columns.Add("ScripName");
                    dtMFHoldingXIRR.Columns.Add("FolioNum");
                    dtMFHoldingXIRR.Columns.Add("XIRR");
                    foreach (DataRow dr in dtSchemeList.Rows)
                    {
                        if (dtBuy.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND Units>0 AND AmountNew<>0").Count() > 0)
                        {
                            DataRow drNew = dtMFHoldingXIRR.NewRow();
                            drNew["CustomerId"] = dr["CustomerId"].ToString();
                            drNew["PortfolioId"] = dr["PortfolioId"].ToString();
                            drNew["ScripName"] = dr["ScripName"].ToString();
                            drNew["FolioNum"] = dr["FolioNum"].ToString();
                            DataTable dtXirr = new DataView(dtBuy.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND Units>0 AND AmountNew<>0", string.Empty).CopyToDataTable()).ToTable(false, new string[] { "TransactionDate", "AmountNew", "BuySell", "PortfolioId" });
                            dtXirr.DefaultView.Sort = "TransactionDate ASC";
                            DataTable dt = dtXirr.DefaultView.ToTable();
                            double[] transactionAmount = new double[dtXirr.Rows.Count + 1];
                            DateTime[] transactionDate = new DateTime[dtXirr.Rows.Count + 1];
                            int tempCount = 0;
                            foreach (DataRow drRow in dt.Rows)
                            {
                                if (drRow["BuySell"].ToString() != "S")
                                    transactionAmount[tempCount] = -(double.Parse(drRow["AmountNew"].ToString()));
                                else
                                    transactionAmount[tempCount] = (double.Parse(drRow["AmountNew"].ToString()));
                                transactionDate[tempCount] = DateTime.Parse(drRow["TransactionDate"].ToString());
                                tempCount++;
                            }
                            //double.Parse(dtBuy.Compute("Sum(CurrentValue)", "PortfolioId='" + dr["PortfolioId"].ToString() + "'").ToString());
                            transactionAmount[tempCount] = double.Parse(dtBuy.Compute("Sum(CurrentAmount)", "PortfolioId='" + dr["PortfolioId"].ToString() + "' AND FolioNum='" + dr["FolioNum"].ToString() + "'").ToString());
                            transactionDate[tempCount] = DateTime.Now;

                            double xirr = CalculateXIRR(transactionAmount, transactionDate);
                            if (xirr < 10000)
                                drNew["XIRR"] = decimal.Parse(xirr.ToString());
                            else
                                drNew["XIRR"] = 0;
                            dtMFHoldingXIRR.Rows.Add(drNew);
                        }
                    }
                    dsHoldTranxn.Tables.Add(dtMFHoldingXIRR);


                }


            }
            return dsHoldTranxn;
        }




        public static double CalculateXIRR(System.Collections.Generic.IEnumerable<double> values, System.Collections.Generic.IEnumerable<DateTime> date)
        {

            double result = 0;
            float guess = 0.1f;
            
            try
            {
                if (values.Sum() < 0)
                    guess = -guess;
                result = System.Numeric.Financial.XIrr(values, date);
                //This 'if' loop is a temporary fix for the error where calculation is done for XIRR instead of average
                if (result.ToString().Contains("E") || result.ToString().Contains("e"))
                {
                    result = 0;
                }
                return Math.Round(result * 100, 2);
            }
            catch (Exception ex)
            {
                try
                {
                    result = System.Numeric.Financial.XIrr(values, date, guess * 5);
                    if (result.ToString().Contains("E") || result.ToString().Contains("e"))
                    {
                        result = 0;
                    }
                    return Math.Round(result * 100, 2); ;
                }
                catch (Exception e)
                {
                    return Math.Round(result * 100, 2); ;
                }

            }

        }

        //----------DataTable Generations for Report Starts


        public DataSet GetCustomerReportHeaders(int A_AdviserrId, int C_CutomerId)
        {
            DataSet dsReportHeaders = MFReportEngineDao.GetCustomerReportHeaders(A_AdviserrId, C_CutomerId);
            return dsReportHeaders;
        }

    }
}
