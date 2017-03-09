using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoCustomerPortfolio;
using VoUser;
using DaoCustomerPortfolio;
using BoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;


namespace BoCustomerPortfolio
{
    public class CustomerTransactionBo
    {
        #region Equity Transactions

        public List<CustomerAccountsVo> GetCustomerEQAccount(int PortfolioId)
        {
            List<CustomerAccountsVo> AccountList = new List<CustomerAccountsVo>();
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                AccountList = customerTransactionDao.GetCustomerEQAccount(PortfolioId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetCustomerMFFolios()");
                object[] objects = new object[2];
                objects[0] = PortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return AccountList;
        }
        public bool AddEquityTransaction(EQTransactionVo eqTransactionVo, int userId)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            bool bResult = false;
            try
            {
                bResult = customerTransactionDao.AddEquityTransaction(eqTransactionVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:AddEquityTransaction()");


                object[] objects = new object[2];
                objects[0] = eqTransactionVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public DataSet PopulateDDExchange(int portfolioId)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            DataSet dsPopulateDDExchange;
            try
            {
                dsPopulateDDExchange = customerTransactionDao.PopulateDDExchange(portfolioId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:PopulateDDExchange()");
                object[] objects = new object[1];
                objects[1] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsPopulateDDExchange;
        }

        public string getId()
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            string id = customerTransactionDao.getId();
            return id;
        }

        public List<EQTransactionVo> GetEquityTransactions(int customerId, int portfolioId, DateTime FromDate, DateTime ToDate)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

            List<EQTransactionVo> eqTransactionsList = new List<EQTransactionVo>();

            //genDictTranType = new Dictionary<string, string>();
            //genDictExchange = new Dictionary<string, string>();
            //genDictTradeDate = new Dictionary<string, string>();

            try
            {
                eqTransactionsList = customerTransactionDao.GetEquityTransactions(customerId, portfolioId, FromDate, ToDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetEquityTransactions()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return eqTransactionsList;
        }

        public List<EQTransactionVo> GetEquityTransactions(int customerId, int portfolioId)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

            List<EQTransactionVo> eqTransactionsList = new List<EQTransactionVo>();
            try
            {
                eqTransactionsList = customerTransactionDao.GetEquityTransactions(customerId, portfolioId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetEquityTransactions()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return eqTransactionsList;
        }

        public List<EQTransactionVo> GetEquityTransactions(int customerId, int portfolioId, int eqCode, DateTime tradeDate, int accountId)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

            List<EQTransactionVo> eqTransactionsList = new List<EQTransactionVo>();
            try
            {
                eqTransactionsList = customerTransactionDao.GetEquityTransactions(customerId, portfolioId, eqCode, tradeDate, accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetEquityTransactions()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = eqCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return eqTransactionsList;
        }

        public List<EQTransactionVo> GetEquityTransactions(int customerId, int eqCode, DateTime tradeDate)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

            List<EQTransactionVo> eqTransactionsList = new List<EQTransactionVo>();
            try
            {
                eqTransactionsList = customerTransactionDao.GetEquityTransactions(customerId, eqCode, tradeDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetEquityTransactions()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = eqCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return eqTransactionsList;
        }

        public bool UpdateEquityTransaction(EQTransactionVo eqTransactionVo, int userId)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            bool bResult = false;
            try
            {
                bResult = customerTransactionDao.UpdateEquityTransaction(eqTransactionVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:UpdateEquityTransaction()");
                object[] objects = new object[2];
                objects[0] = eqTransactionVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public EQTransactionVo GetEquityTransaction(int eqTransactionId)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

            EQTransactionVo eqTransactionVo = new EQTransactionVo();

            try
            {
                eqTransactionVo = customerTransactionDao.GetEquityTransaction(eqTransactionId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetEquityTransaction()");


                object[] objects = new object[1];
                objects[0] = eqTransactionId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return eqTransactionVo;
        }

        public bool DeleteEQTransaction(int eqTransactionId)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            bool bResult = false;
            try
            {
                bResult = customerTransactionDao.DeleteEQTransaction(eqTransactionId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:DeleteEQTransaction()");

                object[] objects = new object[1];
                objects[0] = eqTransactionId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public CustomerAccountsVo GetCustomerEQAccountDetails(int AccountId, int PortfolioId)
        {
            CustomerAccountsVo AccountVo = new CustomerAccountsVo();
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                AccountVo = customerTransactionDao.GetCustomerEQAccountDetails(AccountId, PortfolioId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetCustomerMFFolioDetails()");
                object[] objects = new object[1];
                objects[0] = AccountId;
                objects[1] = PortfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return AccountVo;
        }


        public bool UpdateCustomerEQAccountDetails(CustomerAccountsVo AccountVo, int UserId)
        {
            bool blResult = false;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                blResult = customerTransactionDao.UpdateCustomerEQAccountDetails(AccountVo, UserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:UpdateCustomerMFFolioDetails()");
                object[] objects = new object[2];
                objects[0] = AccountVo;
                objects[1] = UserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }

        #endregion Equity Transactions

        #region MF Transactions
        public bool RunMFTRansactionsCancellationJob()
        {
            bool bResult = false;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {
                bResult = customerTransactionDao.RunMFTRansactionsCancellationJob();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:RunMFTRansactionsCancellationJob()");


                object[] objects = new object[1];
                objects[0] = bResult;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public int AddMFTransaction(MFTransactionVo mfTransactionVo, int userId)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

            int transactionId;

            try
            {

                transactionId = customerTransactionDao.AddMFTransaction(mfTransactionVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:AddMFTransaction()");


                object[] objects = new object[2];
                objects[0] = mfTransactionVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return transactionId;
        }

        public List<MFTransactionVo> GetMFTransactions(int customerId, int portfolioId, DateTime FromDate, DateTime ToDate)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

            List<MFTransactionVo> mfTransactionsList = new List<MFTransactionVo>();
            //genDictTranType = new Dictionary<string, string>();
            //genDictTranTrigger = new Dictionary<string, string>();
            //genDictTranDate = new Dictionary<string, string>();

            try
            {
                mfTransactionsList = customerTransactionDao.GetMFTransactions(customerId, portfolioId, FromDate, ToDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetMFTransactions()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfTransactionsList;
        }

        public List<MFTransactionVo> GetMFTransactions(int customerId, int portfolioId)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

            List<MFTransactionVo> mfTransactionsList = new List<MFTransactionVo>();
            try
            {
                mfTransactionsList = customerTransactionDao.GetMFTransactions(customerId, portfolioId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetMFTransactions()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfTransactionsList;
        }

        public List<MFTransactionVo> GetMFTransactions(int customerId, int accountId, int mfCode, DateTime tradeDate)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

            List<MFTransactionVo> mfTransactionVoList = new List<MFTransactionVo>();
            try
            {
                mfTransactionVoList = customerTransactionDao.GetMFTransactions(customerId, accountId, mfCode, tradeDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetMFTransactions()");


                object[] objects = new object[4];
                objects[0] = customerId;
                objects[1] = accountId;
                objects[2] = mfCode;
                objects[3] = tradeDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfTransactionVoList;
        }

        public List<MFTransactionVo> GetMFTransactions(int customerId, int portfolioId, int accountId, int mfCode, DateTime tradeDate)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

            List<MFTransactionVo> mfTransactionVoList = new List<MFTransactionVo>();
            try
            {
                mfTransactionVoList = customerTransactionDao.GetMFTransactions(customerId, portfolioId, accountId, mfCode, tradeDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetMFTransactions()");


                object[] objects = new object[5];
                objects[0] = customerId;
                objects[1] = portfolioId;
                objects[2] = accountId;
                objects[3] = mfCode;
                objects[4] = tradeDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfTransactionVoList;
        }

        public List<MFTransactionVo> GetMFRejectTransactions(int portfolioId, int adviserId)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

            List<MFTransactionVo> mfTransactionsList = new List<MFTransactionVo>();
            try
            {
                mfTransactionsList = customerTransactionDao.GetMFRejectTransactions(portfolioId, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetMFRejectTransactions(int portfolioId)");


                object[] objects = new object[2];

                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfTransactionsList;
        }

        public List<MFTransactionVo> GetMFOriginalTransactions(int MFTransId)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

            List<MFTransactionVo> mfTransactionsList = new List<MFTransactionVo>();
            try
            {
                mfTransactionsList = customerTransactionDao.GetMFOriginalTransactions(MFTransId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetMFOriginalTransactions(int portfolioId)");


                object[] objects = new object[2];

                objects[0] = MFTransId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfTransactionsList;
        }

        public List<MFSystematicTransactionReportVo> GetMFSystematicTransactionsReport(int adviserId, DateTime fromDate, DateTime toDate, string customerSearch, string schemeSearch, string transType, string portfolioType, out List<string> transactionTypeList)
        {
            List<MFSystematicVo> mfSystematicVoList = new List<MFSystematicVo>();
            List<MFSystematicTransactionVo> mfSystematicTransactionVoList = new List<MFSystematicTransactionVo>();
            List<MFSystematicTransactionReportVo> mfSystematicTransactionReportVoList = new List<MFSystematicTransactionReportVo>();
            MFSystematicTransactionReportVo mfSystematicTransactionReportVo = new MFSystematicTransactionReportVo();
            DateTime TransDate = new DateTime();
            List<string> originalTransTypeList = new List<string>();
            List<string> sysTransTypeList = new List<string>();
            transactionTypeList = new List<string>();
            try
            {
                mfSystematicVoList = GetMFSystematicTransactionSetups(adviserId, fromDate, toDate, customerSearch, schemeSearch, transType, portfolioType, out sysTransTypeList);
                mfSystematicTransactionVoList = GetMFSystematicTransactions(adviserId, fromDate, toDate, customerSearch, schemeSearch, transType, portfolioType, out originalTransTypeList);

                if (sysTransTypeList.Count != 0 && originalTransTypeList.Count != 0)
                {
                    for (int tCount = 0; tCount < originalTransTypeList.Count; tCount++)
                    {
                        if (!sysTransTypeList.Contains(originalTransTypeList[tCount].ToString()))
                        {
                            sysTransTypeList.Add(originalTransTypeList[tCount].ToString());
                        }
                    }
                    transactionTypeList = sysTransTypeList;
                }
                else if (sysTransTypeList.Count != 0)
                {
                    transactionTypeList = sysTransTypeList;
                }
                else if (originalTransTypeList.Count != 0)
                {
                    transactionTypeList = originalTransTypeList;
                }



                #region Systematic Transaction Generation and Matching Original Transactions
                if (mfSystematicVoList != null)
                {
                    for (int i = 0; i < mfSystematicVoList.Count; i++)
                    {
                        //if (mfSystematicVoList[i].FolioNum == "404121257096")
                        //{
                        //}

                        DateTime StartDate = mfSystematicVoList[i].StartDate;
                        DateTime EndDate = mfSystematicVoList[i].EndDate;
                        DateTime TransStartDate = new DateTime();
                        DateTime TransEndDate = new DateTime();
                        DateTime tempTransStartDate = new DateTime();
                        bool skip = false;
                        if (fromDate > StartDate)
                        {
                            TransStartDate = fromDate;
                        }
                        else if (fromDate < StartDate)
                        {
                            TransStartDate = StartDate;
                        }
                        else
                        {
                            TransStartDate = StartDate;
                        }
                        if (toDate > EndDate)
                        {
                            TransEndDate = EndDate;
                        }
                        else if (toDate < EndDate)
                        {
                            TransEndDate = toDate;
                        }
                        else
                        {
                            TransEndDate = EndDate;
                        }
                        tempTransStartDate = TransStartDate;

                        while (tempTransStartDate <= TransEndDate)
                        {
                            //For Monthly, Half Yearly, Quarterly and Yearly Frequency
                            if (mfSystematicVoList[i].FrequencyCode == "MN" || mfSystematicVoList[i].FrequencyCode == "HY" || mfSystematicVoList[i].FrequencyCode == "QT" || mfSystematicVoList[i].FrequencyCode == "YR")
                            {
                                //Skipping first Date if Systematic Day comes after the Start date
                                if (mfSystematicVoList[i].SystematicDay < tempTransStartDate.Day && tempTransStartDate == TransStartDate)
                                {
                                    skip = true;
                                }
                                else
                                {
                                    if (mfSystematicVoList[i].SystematicDay == 31)
                                        TransDate = new DateTime(tempTransStartDate.Year, tempTransStartDate.Month, 1).AddMonths(1).AddDays(-1);
                                    else if ((mfSystematicVoList[i].SystematicDay == 30 || mfSystematicVoList[i].SystematicDay == 29) && tempTransStartDate.Month == 2)
                                        TransDate = new DateTime(tempTransStartDate.Year, tempTransStartDate.Month, 1).AddMonths(1).AddDays(-1);
                                    else
                                        TransDate = new DateTime(tempTransStartDate.Year, tempTransStartDate.Month, mfSystematicVoList[i].SystematicDay);

                                }
                            }
                            //For Daily, Weekly and FortNightly Frequency
                            else if (mfSystematicVoList[i].FrequencyCode == "DA" || mfSystematicVoList[i].FrequencyCode == "WK" || mfSystematicVoList[i].FrequencyCode == "FN")
                            {
                                TransDate = tempTransStartDate;
                            }
                            if (!skip)
                            {
                                //Non Switch Systematic Transactions Section
                                if (mfSystematicVoList[i].SystematicTypeCode != "STP")
                                {
                                    mfSystematicTransactionReportVo = new MFSystematicTransactionReportVo();
                                    mfSystematicTransactionReportVo.SchemePlanCode = mfSystematicVoList[i].SchemePlanCode;
                                    mfSystematicTransactionReportVo.SchemePlanName = mfSystematicVoList[i].SchemePlanName;
                                    mfSystematicTransactionReportVo.AccountId = mfSystematicVoList[i].AccountId;
                                    mfSystematicTransactionReportVo.CustomerId = mfSystematicVoList[i].CustomerId;
                                    mfSystematicTransactionReportVo.CustomerName = mfSystematicVoList[i].CustomerName;
                                    mfSystematicTransactionReportVo.FolioNum = mfSystematicVoList[i].FolioNum;
                                    mfSystematicTransactionReportVo.PortfolioId = mfSystematicVoList[i].PortfolioId;
                                    mfSystematicTransactionReportVo.SystematicAmount = mfSystematicVoList[i].Amount;
                                    mfSystematicTransactionReportVo.SystematicTransacionType = mfSystematicVoList[i].SystematicTypeCode;
                                    mfSystematicTransactionReportVo.SystematicTransactionDate = TransDate;
                                    //Matching Original Transactions
                                    //if (mfSystematicTransactionReportVo.SystematicTransactionDate == DateTime.Parse("28/05/2011"))
                                    //{

                                    //}

                                    if (mfSystematicTransactionVoList != null)
                                    {
                                        for (int j = 0; j < mfSystematicTransactionVoList.Count; j++)
                                        {
                                            //if (mfSystematicTransactionVoList[j].TransactionDate == DateTime.Parse("30/05/2011") && mfSystematicTransactionVoList[j].FolioNum == "404121257096")
                                            //{
                                            //}

                                            if (mfSystematicTransactionReportVo.AccountId == mfSystematicTransactionVoList[j].AccountId
                                                && mfSystematicTransactionReportVo.SchemePlanCode == mfSystematicTransactionVoList[j].SchemePlanCode
                                                && (((mfSystematicTransactionVoList[j].TransactionDate >= mfSystematicTransactionReportVo.SystematicTransactionDate) && (mfSystematicTransactionVoList[j].TransactionDate <= mfSystematicTransactionReportVo.SystematicTransactionDate.AddDays(5))) || (mfSystematicTransactionReportVo.SystematicTransactionDate == mfSystematicTransactionVoList[j].TransactionDate))
                                                && mfSystematicTransactionReportVo.SystematicAmount == Math.Round(mfSystematicTransactionVoList[j].Amount, 2)
                                                && !mfSystematicTransactionVoList[j].MatchFound)
                                            {
                                                mfSystematicTransactionReportVo.OriginalTransactionAmount = mfSystematicTransactionVoList[j].Amount;
                                                mfSystematicTransactionReportVo.OriginalTransactionDate = mfSystematicTransactionVoList[j].TransactionDate;
                                                mfSystematicTransactionReportVo.OriginalTransactionType = mfSystematicTransactionVoList[j].TransactionClassificationCode;
                                                mfSystematicTransactionVoList[j].MatchFound = true;
                                            }
                                        }
                                    }
                                    mfSystematicTransactionReportVoList.Add(mfSystematicTransactionReportVo);
                                }
                                //Switch Systematic Transactions Section
                                else
                                {
                                    // Switch Sell Section
                                    mfSystematicTransactionReportVo = new MFSystematicTransactionReportVo();
                                    mfSystematicTransactionReportVo.SchemePlanCode = mfSystematicVoList[i].SchemePlanCode;
                                    mfSystematicTransactionReportVo.SchemePlanName = mfSystematicVoList[i].SchemePlanName;
                                    mfSystematicTransactionReportVo.AccountId = mfSystematicVoList[i].AccountId;
                                    mfSystematicTransactionReportVo.CustomerId = mfSystematicVoList[i].CustomerId;
                                    mfSystematicTransactionReportVo.CustomerName = mfSystematicVoList[i].CustomerName;
                                    mfSystematicTransactionReportVo.FolioNum = mfSystematicVoList[i].FolioNum;
                                    mfSystematicTransactionReportVo.PortfolioId = mfSystematicVoList[i].PortfolioId;
                                    mfSystematicTransactionReportVo.SystematicAmount = mfSystematicVoList[i].Amount;
                                    mfSystematicTransactionReportVo.SystematicTransacionType = "STS";
                                    mfSystematicTransactionReportVo.SystematicTransactionDate = TransDate;
                                    //Matching Original Transactions
                                    if (mfSystematicTransactionVoList != null)
                                    {
                                        for (int j = 0; j < mfSystematicTransactionVoList.Count; j++)
                                        {
                                            if (mfSystematicTransactionReportVo.AccountId == mfSystematicTransactionVoList[j].AccountId
                                                && mfSystematicTransactionReportVo.SchemePlanCode == mfSystematicTransactionVoList[j].SchemePlanCode
                                                //&& (mfSystematicTransactionReportVo.SystematicTransactionDate == mfSystematicTransactionVoList[j].TransactionDate)
                                                && (((mfSystematicTransactionVoList[j].TransactionDate >= mfSystematicTransactionReportVo.SystematicTransactionDate) && (mfSystematicTransactionVoList[j].TransactionDate <= mfSystematicTransactionReportVo.SystematicTransactionDate.AddDays(5))) || (mfSystematicTransactionReportVo.SystematicTransactionDate == mfSystematicTransactionVoList[j].TransactionDate))
                                                && mfSystematicTransactionReportVo.SystematicAmount == Math.Round(mfSystematicTransactionVoList[j].Amount, 2)
                                                && !mfSystematicTransactionVoList[j].MatchFound)
                                            {
                                                mfSystematicTransactionReportVo.OriginalTransactionAmount = mfSystematicTransactionVoList[j].Amount;
                                                mfSystematicTransactionReportVo.OriginalTransactionDate = mfSystematicTransactionVoList[j].TransactionDate;
                                                mfSystematicTransactionReportVo.OriginalTransactionType = mfSystematicTransactionVoList[j].TransactionClassificationCode;
                                                mfSystematicTransactionVoList[j].MatchFound = true;
                                            }
                                        }
                                    }

                                    mfSystematicTransactionReportVoList.Add(mfSystematicTransactionReportVo);

                                    // Switch Buy Section
                                    mfSystematicTransactionReportVo = new MFSystematicTransactionReportVo();
                                    mfSystematicTransactionReportVo.SchemePlanCode = mfSystematicVoList[i].SwitchSchemePlanCode;
                                    mfSystematicTransactionReportVo.SchemePlanName = mfSystematicVoList[i].SwitchSchemePlanName;
                                    mfSystematicTransactionReportVo.AccountId = mfSystematicVoList[i].AccountId;
                                    mfSystematicTransactionReportVo.CustomerId = mfSystematicVoList[i].CustomerId;
                                    mfSystematicTransactionReportVo.CustomerName = mfSystematicVoList[i].CustomerName;
                                    mfSystematicTransactionReportVo.FolioNum = mfSystematicVoList[i].FolioNum;
                                    mfSystematicTransactionReportVo.PortfolioId = mfSystematicVoList[i].PortfolioId;
                                    mfSystematicTransactionReportVo.SystematicAmount = mfSystematicVoList[i].Amount;
                                    mfSystematicTransactionReportVo.SystematicTransacionType = "STB";
                                    mfSystematicTransactionReportVo.SystematicTransactionDate = TransDate;
                                    //Matching Original Transactions
                                    if (mfSystematicTransactionVoList != null)
                                    {
                                        for (int k = 0; k < mfSystematicTransactionVoList.Count; k++)
                                        {
                                            if (mfSystematicTransactionReportVo.AccountId == mfSystematicTransactionVoList[k].AccountId
                                                && mfSystematicTransactionReportVo.SchemePlanCode == mfSystematicTransactionVoList[k].SchemePlanCode
                                                //&& (mfSystematicTransactionReportVo.SystematicTransactionDate == mfSystematicTransactionVoList[k].TransactionDate)
                                                && (((mfSystematicTransactionVoList[k].TransactionDate >= mfSystematicTransactionReportVo.SystematicTransactionDate) && (mfSystematicTransactionVoList[k].TransactionDate <= mfSystematicTransactionReportVo.SystematicTransactionDate.AddDays(5))) || (mfSystematicTransactionReportVo.SystematicTransactionDate == mfSystematicTransactionVoList[k].TransactionDate))
                                                && mfSystematicTransactionReportVo.SystematicTransacionType == mfSystematicTransactionVoList[k].TransactionClassificationCode
                                                && mfSystematicTransactionReportVo.SystematicAmount == Math.Round(mfSystematicTransactionVoList[k].Amount, 2)
                                                && !mfSystematicTransactionVoList[k].MatchFound)
                                            {
                                                mfSystematicTransactionReportVo.OriginalTransactionAmount = mfSystematicTransactionVoList[k].Amount;
                                                mfSystematicTransactionReportVo.OriginalTransactionDate = mfSystematicTransactionVoList[k].TransactionDate;
                                                mfSystematicTransactionReportVo.OriginalTransactionType = mfSystematicTransactionVoList[k].TransactionClassificationCode;
                                                mfSystematicTransactionVoList[k].MatchFound = true;
                                            }
                                        }
                                    }
                                    mfSystematicTransactionReportVoList.Add(mfSystematicTransactionReportVo);
                                }
                            }
                            else
                            {
                                skip = false;
                            }

                            //Adding Dates based on Frequency
                            switch (mfSystematicVoList[i].FrequencyCode)
                            {
                                //Monthly
                                case "MN":

                                    tempTransStartDate = tempTransStartDate.AddMonths(1);
                                    if (tempTransStartDate.Day != mfSystematicVoList[i].SystematicDay)
                                    {
                                        if (mfSystematicVoList[i].SystematicDay == 31)
                                            tempTransStartDate = new DateTime(tempTransStartDate.Year, tempTransStartDate.Month, 1).AddMonths(1).AddDays(-1);
                                        else if (mfSystematicVoList[i].SystematicDay == 30 && tempTransStartDate.Month != 2)
                                            tempTransStartDate = new DateTime(tempTransStartDate.Year, tempTransStartDate.Month, 30);
                                        else if (mfSystematicVoList[i].SystematicDay == 29 && tempTransStartDate.Month != 2)
                                            tempTransStartDate = new DateTime(tempTransStartDate.Year, tempTransStartDate.Month, 29);
                                    }
                                    break;

                                //Yearly
                                case "YR":
                                    tempTransStartDate = tempTransStartDate.AddMonths(12);
                                    break;

                                //Quarterly
                                case "QT":
                                    tempTransStartDate = tempTransStartDate.AddMonths(3);
                                    break;

                                //Half Yearly
                                case "HY":
                                    tempTransStartDate = tempTransStartDate.AddMonths(6);
                                    break;

                                //Daily
                                case "DA":
                                    tempTransStartDate = tempTransStartDate.AddDays(1);
                                    break;

                                //Weekly
                                case "WK":
                                    tempTransStartDate = tempTransStartDate.AddDays(7);
                                    break;

                                //FortNightly
                                case "FN":
                                    tempTransStartDate = tempTransStartDate.AddDays(15);
                                    break;

                            }
                        }

                    }
                }

                #endregion Systematic Transaction Generation and Matching Original Transactions

                #region Adding Non Transactions with No Match Found
                if (mfSystematicTransactionVoList != null)
                {
                    for (int l = 0; l < mfSystematicTransactionVoList.Count; l++)
                    {
                        if (!mfSystematicTransactionVoList[l].MatchFound)
                        {
                            mfSystematicTransactionReportVo = new MFSystematicTransactionReportVo();
                            mfSystematicTransactionReportVo.AccountId = mfSystematicTransactionVoList[l].AccountId;
                            mfSystematicTransactionReportVo.CustomerId = mfSystematicTransactionVoList[l].CustomerId;
                            mfSystematicTransactionReportVo.CustomerName = mfSystematicTransactionVoList[l].CustomerName;
                            mfSystematicTransactionReportVo.FolioNum = mfSystematicTransactionVoList[l].FolioNum;
                            mfSystematicTransactionReportVo.OriginalTransactionAmount = mfSystematicTransactionVoList[l].Amount;
                            mfSystematicTransactionReportVo.OriginalTransactionDate = mfSystematicTransactionVoList[l].TransactionDate;
                            mfSystematicTransactionReportVo.OriginalTransactionType = mfSystematicTransactionVoList[l].TransactionClassificationCode;
                            mfSystematicTransactionReportVo.PortfolioId = mfSystematicTransactionVoList[l].PortfolioId;
                            mfSystematicTransactionReportVo.SchemePlanCode = mfSystematicTransactionVoList[l].SchemePlanCode;
                            mfSystematicTransactionReportVo.SchemePlanName = mfSystematicTransactionVoList[l].SchemePlanName;

                            mfSystematicTransactionReportVoList.Add(mfSystematicTransactionReportVo);
                        }
                    }
                }
                #endregion Adding Non Transactions with No Match Found
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetMFSystematicTransactionsReport(int adviserId, DateTime fromDate, DateTime toDate)");


                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = fromDate;
                objects[2] = toDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfSystematicTransactionReportVoList;
        }

        public List<MFSystematicTransactionVo> GetMFSystematicTransactions(int adviserId, DateTime fromDate, DateTime toDate, string customerSearch, string schemeSearch, string transType, string portfolioType, out List<string> transactionTypeList)
        {
            List<MFSystematicTransactionVo> mfSystematicTransactionVoList = new List<MFSystematicTransactionVo>();
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                mfSystematicTransactionVoList = customerTransactionDao.GetMFSystematicTransactions(adviserId, fromDate, toDate, customerSearch, schemeSearch, transType, portfolioType, out transactionTypeList);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetMFSystematicTransactionSetups(int adviserId, DateTime fromDate, DateTime toDate)");


                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = fromDate;
                objects[2] = toDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfSystematicTransactionVoList;

        }

        public List<MFSystematicVo> GetMFSystematicTransactionSetups(int adviserId, DateTime fromDate, DateTime toDate, string customerSearch, string schemeSearch, string transType, string portfolioType, out List<string> transactionTypeList)
        {
            List<MFSystematicVo> mfSystematicVoList = new List<MFSystematicVo>();
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                mfSystematicVoList = customerTransactionDao.GetMFSystematicTransactionSetups(adviserId, fromDate, toDate, customerSearch, schemeSearch, transType, portfolioType, out transactionTypeList);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetMFSystematicTransactionSetups(int adviserId, DateTime fromDate, DateTime toDate)");


                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = fromDate;
                objects[2] = toDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfSystematicVoList;
        }

        public bool UpdateRejectedTransactionStatus(int MFTransId, int OriginalTransactionNumber)
        {
            bool bResult = false;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                bResult = customerTransactionDao.UpdateRejectedTransactionStatus(MFTransId, OriginalTransactionNumber);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:UpdateRejectedTransactionStatus(int MFTransId, int OriginalTransactionNumber)");


                object[] objects = new object[2];
                objects[0] = MFTransId;
                objects[1] = OriginalTransactionNumber;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public MFTransactionVo GetMFTransaction(int mfTransactionId)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

            MFTransactionVo mfTransactionVo = new MFTransactionVo();
            try
            {
                mfTransactionVo = customerTransactionDao.GetMFTransaction(mfTransactionId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetMFTransaction()");


                object[] objects = new object[1];
                objects[0] = mfTransactionId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfTransactionVo;
        }

        public bool UpdateMFTransaction(MFTransactionVo mfTransactionVo, int userId)
        {
            bool bResult = false;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();



            try
            {

                bResult = customerTransactionDao.UpdateMFTransaction(mfTransactionVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:UpdateMFTransaction()");


                object[] objects = new object[2];
                objects[0] = mfTransactionVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool DeleteMFTransaction(int mfTransactionId)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            bool bResult = false;
            try
            {
                bResult = customerTransactionDao.DeleteMFTransaction(mfTransactionId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:DeleteMFTransaction()");

                object[] objects = new object[1];
                objects[0] = mfTransactionId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public int GetMFTransactions(int customerId, int portfolioId, string flag)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

            return customerTransactionDao.GetMFTransactions(customerId, portfolioId, flag);
        }
        public bool CancelMFTransaction(MFTransactionVo mfTransactionVo, int userId)
        {
            bool bResult = false;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                bResult = customerTransactionDao.CancelMFTransaction(mfTransactionVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:CancelMFTransaction(MFTransactionVo mfTransactionVo, int userId)");


                object[] objects = new object[2];
                objects[0] = mfTransactionVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool DeleteMFTransaction(MFTransactionVo mfTransactionVo, int adviserId, int UserId)
        {
            bool bResult = false;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                bResult = customerTransactionDao.DeleteMFTransaction(mfTransactionVo, adviserId, UserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:CancelMFTransaction(MFTransactionVo mfTransactionVo, int userId)");


                object[] objects = new object[2];
                objects[0] = mfTransactionVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        #endregion MF Transactions

        #region Multiple MF Transaction

        public DataSet GetLastTradeDate()
        {
            DataSet ds;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                ds = customerTransactionDao.GetLastTradeDate();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetLastTradeDate()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        public DataSet GetPortfolioType(string folionum)
        {
            DataSet ds;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                ds = customerTransactionDao.GetPortfolioType(folionum);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetPortfolioType()");
                object[] objects = new object[1];
                objects[0] = folionum;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        public DataSet GetRMCustomerTrailCommission(int RMId, int AdviserID, int GroupHeadId, DateTime From, DateTime To, int Manage, int AccountId,int SchemePlanCode, int AmcCode, string Category, int A_AgentCodeBased, string AgentCode, string UserType)
        {
            DataSet ds = new DataSet();
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                ds = customerTransactionDao.GetRMCustomerTrailCommission(RMId, AdviserID, GroupHeadId, From, To, Manage, AccountId,SchemePlanCode, AmcCode, Category, A_AgentCodeBased, AgentCode, UserType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetMFFolioAccountAssociates()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return ds;

        }

        public List<MFTransactionVo> GetRMCustomerMFTransactions(int RMId, int AdviserID, int GroupHeadId,int IsfolioOnline, DateTime From, DateTime To, int Manage, int AccountId, bool isCustomerTransactionOnly, int SchemePlanCode, int AmcCode, string Category, int A_AgentCodeBased, string AgentCode, string UserType,int agentType,int requestId)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            List<MFTransactionVo> mfTransactionsList = new List<MFTransactionVo>();
            try
            {

                mfTransactionsList = customerTransactionDao.GetRMCustomerMFTransactions(RMId, AdviserID, GroupHeadId, IsfolioOnline, From, To, Manage, AccountId, isCustomerTransactionOnly, SchemePlanCode, AmcCode, Category, A_AgentCodeBased, AgentCode, UserType, agentType, requestId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetRMCustomerMFTransactions()");
                object[] objects = new object[3];
                objects[0] = RMId;
                objects[1] = GroupHeadId;
                objects[2] = From;
                objects[3] = To;
                objects[4] = Manage;
                //objects[5] = CurrentPage;
                //objects[6] = CustomerName;
                //objects[7] = Scheme;
                //objects[8] = PasssedFolioValue; 
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfTransactionsList;

        }
        public List<MFTransactionVo> GetAdviserCustomerMFTransactions(out int Count, int CurrentPage, int adviserId, int GroupHeadId, DateTime From, DateTime To, int Manage, string CustomerName, string Scheme, string TranType, string transactionStatus, out Dictionary<string, string> genDictTranType, string FolioNumber, string PasssedFolioValue, string categoryCode, int AMCCode, out Dictionary<string, string> genDictCategory, out Dictionary<string, int> genDictAMC)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            List<MFTransactionVo> mfTransactionsList = new List<MFTransactionVo>();
            try
            {

                mfTransactionsList = customerTransactionDao.GetAdviserCustomerMFTransactions(out Count, CurrentPage, adviserId, GroupHeadId, From, To, Manage, CustomerName, Scheme, TranType, transactionStatus, out genDictTranType, FolioNumber, PasssedFolioValue, categoryCode, AMCCode, out genDictCategory, out genDictAMC);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetAdviserCustomerMFTransactions()");
                object[] objects = new object[9];
                objects[0] = adviserId;
                objects[1] = GroupHeadId;
                objects[2] = From;
                objects[3] = To;
                objects[4] = Manage;
                objects[5] = CurrentPage;
                objects[6] = CustomerName;
                objects[7] = Scheme;
                objects[8] = PasssedFolioValue;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfTransactionsList;

        }
        #endregion Multiple MF Transaction

        #region MFFolio

        public List<CustomerAccountsVo> GetCustomerMFFolios(int PortfolioId, int CustomerId)
        {
            List<CustomerAccountsVo> AccountList = new List<CustomerAccountsVo>();
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                AccountList = customerTransactionDao.GetCustomerMFFolios(PortfolioId, CustomerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetCustomerMFFolios()");
                object[] objects = new object[2];
                objects[0] = PortfolioId;
                objects[1] = CustomerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return AccountList;
        }

        public CustomerAccountsVo GetCustomerMFFolioDetails(int FolioId)
        {

            CustomerAccountsVo AccountVo = new CustomerAccountsVo();
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                AccountVo = customerTransactionDao.GetCustomerMFFolioDetails(FolioId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetCustomerMFFolioDetails()");
                object[] objects = new object[1];
                objects[0] = FolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return AccountVo;
        }
        public bool UpdateCustomerMFFolioDetails(CustomerAccountsVo AccountVo, int UserId)
        {
            bool blResult = false;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                blResult = customerTransactionDao.UpdateCustomerMFFolioDetails(AccountVo, UserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:UpdateCustomerMFFolioDetails()");
                object[] objects = new object[2];
                objects[0] = AccountVo;
                objects[1] = UserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }
        public DataSet GetMFFolioAccountAssociates(int FolioId, int CustomerId)
        {
            DataSet ds = new DataSet();
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                ds = customerTransactionDao.GetMFFolioAccountAssociates(FolioId, CustomerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetMFFolioAccountAssociates()");
                object[] objects = new object[2];
                objects[0] = FolioId;
                objects[1] = CustomerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return ds;
        }

        public bool DeleteMFFolioAccountAssociates(int FolioId)
        {
            bool blResult = false;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                blResult = customerTransactionDao.DeleteMFFolioAccountAssociates(FolioId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:DeleteMFFolioAccountAssociates()");
                object[] objects = new object[1];
                objects[0] = FolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }

        #endregion MFFolio

        #region Multiple MF Transaction



        public DataSet GetRMCustomerEqTransactions(int RMId, int adviserId, int GroupHeadId, DateTime From, DateTime To, int Manage)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            DataSet ds = null;
            try
            {

                ds = customerTransactionDao.GetRMCustomerEqTransactions(RMId, adviserId, GroupHeadId, From, To, Manage);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetRMCustomerMFTransactions()");
                object[] objects = new object[8];
                objects[0] = RMId;
                objects[1] = GroupHeadId;
                objects[2] = From;
                objects[3] = To;
                objects[4] = Manage;
                //objects[5] = CurrentPage;
                //objects[6] = CustomerName;
                //objects[7] = Scheme;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
        public DataSet GetAdviserCustomerEqTransactions(out int Count, int CurrentPage, int adviserId, int GroupHeadId, DateTime From, DateTime To, int Manage, string CustomerName, string Scheme, string TranType, out Dictionary<string, string> genDictTranType, string FolioNumber, int exportFlag)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            DataSet ds = null;
            try
            {

                ds = customerTransactionDao.GetAdviserCustomerEqTransactions(out Count, CurrentPage, adviserId, GroupHeadId, From, To, Manage, CustomerName, Scheme, TranType, out genDictTranType, FolioNumber, exportFlag);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetadviserCustomerMFTransactions()");
                object[] objects = new object[8];
                objects[0] = adviserId;
                objects[1] = GroupHeadId;
                objects[2] = From;
                objects[3] = To;
                objects[4] = Manage;
                objects[5] = CurrentPage;
                objects[6] = CustomerName;
                objects[7] = Scheme;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
        #endregion Multiple MF Transaction
        public string GetTransactionType(string transname)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            return customerTransactionDao.GetTransactionType(transname);
        }

        /// <summary>
        /// To check whether TradeAccount No. associated with EQ Transaction <<Vinayak Patil>>
        /// </summary>
        /// <param name="eqTradeAccId"></param>
        /// <returns></returns>

        public bool CheckEQTradeAccNoAssociatedWithTransactions(int eqTradeAccId)
        {
            bool blResult = false;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                blResult = customerTransactionDao.CheckEQTradeAccNoAssociatedWithTransactions(eqTradeAccId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:CheckEQTradeAccNoAssociatedWithTransactions()");
                object[] objects = new object[2];
                objects[0] = eqTradeAccId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }

        /// <summary>
        /// To Delete Trade Account <<Vinayak Patil>>
        /// </summary>
        /// <param name="eqTradeAccId"></param>
        /// <returns></returns>

        public bool DeleteTradeAccount(int eqTradeAccId)
        {
            bool blResult = false;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                blResult = customerTransactionDao.DeleteTradeAccount(eqTradeAccId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:CheckEQTradeAccNoAssociatedWithTransactions()");
                object[] objects = new object[2];
                objects[0] = eqTradeAccId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }

        /// <summary>
        /// To check whether MFFolio associated with MF Transaction <<Vinayak Patil>>
        /// </summary>
        /// <param name="FolioId"></param>
        /// <returns></returns>

        public bool CheckMFFOlioAssociatedWithTransactions(int FolioId)
        {
            bool blResult = false;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                blResult = customerTransactionDao.CheckMFFOlioAssociatedWithTransactions(FolioId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:CheckMFFOlioAssociatedWithTransactions()");
                object[] objects = new object[2];
                objects[0] = FolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }

        /// <summary>
        /// To Delete MF Folio <<Vinayak Patil>
        /// </summary>
        /// <param name="FolioId"></param>
        /// <returns></returns>

        public bool DeleteMFFolio(int FolioId)
        {
            bool blResult = false;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                blResult = customerTransactionDao.DeleteMFFolio(FolioId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:CheckEQTradeAccNoAssociatedWithTransactions()");
                object[] objects = new object[2];
                objects[0] = FolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }


        public bool CancelEquityTransaction(EQTransactionVo eqTransactionVo, int userId)
        {
            bool bResult = false;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                bResult = customerTransactionDao.CancelEquityTransaction(eqTransactionVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }

        public DataSet GetLastMFTradeDate()
        {
            DataSet ds;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                ds = customerTransactionDao.GetLastMFTradeDate();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetLastTradeDate()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        public List<MFTransactionVo> GetRMCustomerMFBalance(int RMId, int AdviserID, int GroupHeadId, DateTime From, DateTime To, int Manage, int AccountId,int SchemePlanCode, int AmcCode, string Category, int A_AgentCodeBased, string AgentCode, string UserType)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            List<MFTransactionVo> mfBalanceList = new List<MFTransactionVo>();
            try
            {

                mfBalanceList = customerTransactionDao.GetRMCustomerMFBalance(RMId, AdviserID, GroupHeadId, From, To, Manage, AccountId,SchemePlanCode, AmcCode, Category, A_AgentCodeBased, AgentCode, UserType);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetRMCustomerMFBalance()");
                object[] objects = new object[3];
                objects[0] = RMId;
                objects[1] = GroupHeadId;
                objects[2] = From;
                objects[3] = To;
                objects[4] = Manage;
                //objects[5] = CurrentPage;
                //objects[6] = CustomerName;
                //objects[7] = Scheme;
                //objects[8] = PasssedFolioValue; 
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfBalanceList;

        }



        public DataSet GetEquityLedgerMIS(int CustomerId, int TradeAccountId, int BankAccountId)
        {
            DataSet dsGetEqLedgerMIS;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                dsGetEqLedgerMIS = customerTransactionDao.GetEquityLedgerMIS(CustomerId, TradeAccountId, BankAccountId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetLastTradeDate()");
                object[] objects = new object[3];
                objects[0] = CustomerId;
                objects[1] = TradeAccountId;
                objects[2] = BankAccountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetEqLedgerMIS;
        }
        public DataSet GetTransactionType()
        {
            DataSet ds;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                ds = customerTransactionDao.GetTransactionType();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetTransactionType()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

       

        public bool MergeTrailDetailsWithTransaction(int accountId, int trailIdForMerge, int transactionIdForMerge, int IsCompleted, int isMergeManual, string folionoForMerge, int schemeplancodeForMerge, string transactionnoForMerge, double unitsForMerge, double amountForMerge, DateTime transactionDateForMerge, int adviserId)
        {
            bool bResult = false;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {
                bResult = customerTransactionDao.MergeTrailDetailsWithTransaction(accountId, trailIdForMerge, transactionIdForMerge, IsCompleted, isMergeManual, folionoForMerge, schemeplancodeForMerge, transactionnoForMerge, unitsForMerge, amountForMerge, transactionDateForMerge,adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
            
        }

        public DataSet GetTransactionDetailsForTrail(int AccountId,int trailIdForMerge,string folionoForMerge,int schemeplancodeForMerge,string transactionnoForMerge,int advisorId)
        {
            DataSet ds;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {
                ds = customerTransactionDao.GetTransactionDetailsForTrail(AccountId,trailIdForMerge,folionoForMerge,schemeplancodeForMerge,transactionnoForMerge,advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetTransactionDetailsForTrail(int AccountId)");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
        public DataSet GetMFTransactionType()
        {
            DataSet ds;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                ds = customerTransactionDao.GetMFTransactionType();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetTransactionType()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        public List<MFTransactionVo> GetCustomerTransactionsBookSIP(int AdviserID, int customerId, int SystematicId, int IsSourceAA, int AccountId, int SchemePlanCode, int amount, DateTime SIPStartDate)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            List<MFTransactionVo> mfTransactionsBookList = new List<MFTransactionVo>();
            try
            {

                mfTransactionsBookList = customerTransactionDao.GetCustomerTransactionsBookSIP(AdviserID, customerId, SystematicId, IsSourceAA, AccountId, SchemePlanCode, amount, SIPStartDate);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetRMCustomerMFTransactions()");
                object[] objects = new object[3];
                objects[0] = customerId;
                objects[1] = SystematicId;
                objects[2] = AccountId;
                //objects[5] = CurrentPage;
                //objects[6] = CustomerName;
                //objects[7] = Scheme;
                //objects[8] = PasssedFolioValue; 
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfTransactionsBookList;

        }
        public List<MFTransactionVo> GetCustomerTransactionsBook(int AdviserID, int CustomerId, DateTime From, DateTime To, int Manage, int AmcCode, int AccountId, int SchemePlanCode)
        {
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            List<MFTransactionVo> mfTransactionsBookList = new List<MFTransactionVo>();
            try
            {

                mfTransactionsBookList = customerTransactionDao.GetCustomerTransactionsBook(AdviserID, CustomerId, From, To, Manage, AmcCode, AccountId, SchemePlanCode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetRMCustomerMFTransactions()");
                object[] objects = new object[3];
                objects[0] = From;
                objects[1] = To;
                objects[2] = Manage;
                //objects[5] = CurrentPage;
                //objects[6] = CustomerName;
                //objects[7] = Scheme;
                //objects[8] = PasssedFolioValue; 
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfTransactionsBookList;

        }


        //add bo for migrate


        public CustomerAccountsVo GetEquityRateForTransaction(int Accountid, int TransactionMode, string TransactionType, DateTime TradeDate)
        {
            CustomerAccountsVo AccountVo = new CustomerAccountsVo();
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                AccountVo = customerTransactionDao.GetEquityRateForTransaction(Accountid, TransactionMode, TransactionType, TradeDate);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetCustomerMFFolioDetails()");
                object[] objects = new object[1];
                objects[0] = Accountid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return AccountVo;
        }


        public float GetEQScripPrice(int ScripCode, DateTime navDate, String Currency)
        {
            float ScripPrice = 0;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            ScripPrice = customerTransactionDao.GetEQScripPrice(ScripCode, navDate, Currency);
            return ScripPrice;
        }


        public void AddDividend(string CorpAxnCode, int scripcode, DateTime DivDeclaredDate, double DivPercentage, double facevalue)
        {


            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                customerTransactionDao.AddDividend(CorpAxnCode, scripcode, DivDeclaredDate, DivPercentage, facevalue);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetDividendHistory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        public int GetNoOfShares(int AccountId, int scripCode, DateTime TradeDate, int DematAccountNum, int managedby)
        {
            int NoOfShares;

            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                NoOfShares = customerTransactionDao.GetNoOfShares(AccountId, scripCode, TradeDate, DematAccountNum, managedby);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetDividendHistory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return NoOfShares;
        }
        public DataSet GetDividendHistory(int scripCode)
        {
            DataSet ds;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                ds = customerTransactionDao.GetDividendHistory(scripCode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetDividendHistory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
        public DataSet GetManagedby(int advisorid)
        {
            DataSet ds;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                ds = customerTransactionDao.GetManagedby(advisorid);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetManagedby()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
           public DataSet GetDematAccountNumber(int portfolioid)
        {
            DataSet ds;
            CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
            try
            {

                ds = customerTransactionDao.GetDematAccountNumber(portfolioid);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetDematAccountNumber()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
           public string GetDollarRate(DateTime TradeDate)
           {
               CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
               string Rate;
               try
               {
                   Rate = customerTransactionDao.GetDollarRate(TradeDate);
               }
               catch (BaseApplicationException Ex)
               {
                   throw Ex;
               }
               catch (Exception Ex)
               {
                   BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                   NameValueCollection FunctionInfo = new NameValueCollection();
                   FunctionInfo.Add("Method", "CustomerTransactionBo.cs:UpdateEquityTransaction()");
                   object[] objects = new object[2];
                   FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                   exBase.AdditionalInformation = FunctionInfo;
                   ExceptionManager.Publish(exBase);
                   throw exBase;

               }
               return Rate;
           }
           public DataSet GetType()
           {
               DataSet ds;
               CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
               try
               {

                   ds = customerTransactionDao.GetType();

               }
               catch (BaseApplicationException Ex)
               {
                   throw Ex;
               }
               catch (Exception Ex)
               {
                   BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                   NameValueCollection FunctionInfo = new NameValueCollection();
                   FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetManagedby()");
                   object[] objects = new object[0];
                   FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                   exBase.AdditionalInformation = FunctionInfo;
                   ExceptionManager.Publish(exBase);
                   throw exBase;

               }
               return ds;
           }
           public List<EQTransactionVo> GetEquityTransactions(int customerId, int portfolioId, int ScripCode, DateTime FromDate, DateTime ToDate, string Price)
           {
               CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

               List<EQTransactionVo> eqTransactionsList = new List<EQTransactionVo>();

               //genDictTranType = new Dictionary<string, string>();
               //genDictExchange = new Dictionary<string, string>();
               //genDictTradeDate = new Dictionary<string, string>();

               try
               {
                   eqTransactionsList = customerTransactionDao.GetEquityTransactions(customerId, portfolioId, ScripCode, FromDate, ToDate, Price);
               }
               catch (BaseApplicationException Ex)
               {
                   throw Ex;
               }
               catch (Exception Ex)
               {
                   BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                   NameValueCollection FunctionInfo = new NameValueCollection();

                   FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetEquityTransactions()");


                   object[] objects = new object[1];
                   objects[0] = customerId;

                   FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                   exBase.AdditionalInformation = FunctionInfo;
                   ExceptionManager.Publish(exBase);
                   throw exBase;

               }
               return eqTransactionsList;
           }
           public bool MapEQTransactionToCIAndPI(string EQTraxnIds, string Type)
           {
               CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
               bool bResult = false;
               try
               {
                   bResult = customerTransactionDao.MapEQTransactionToCIAndPI(EQTraxnIds, Type);
               }
               catch (BaseApplicationException Ex)
               {
                   throw Ex;
               }
               catch (Exception Ex)
               {
                   BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                   NameValueCollection FunctionInfo = new NameValueCollection();
                   FunctionInfo.Add("Method", "CustomerTransactionBo.cs:UpdateEquityTransaction()");
                   object[] objects = new object[2];
                   FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                   exBase.AdditionalInformation = FunctionInfo;
                   ExceptionManager.Publish(exBase);
                   throw exBase;

               }
               return bResult;
           }
           public bool MapEQToManager(string EQTraxnIds)
           {
               CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
               bool bResult = false;
               try
               {
                   bResult = customerTransactionDao.MapEQToManager(EQTraxnIds);
               }
               catch (BaseApplicationException Ex)
               {
                   throw Ex;
               }
               catch (Exception Ex)
               {
                   BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                   NameValueCollection FunctionInfo = new NameValueCollection();
                   FunctionInfo.Add("Method", "CustomerTransactionBo.cs:UpdateEquityTransaction()");
                   object[] objects = new object[2];
                   FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                   exBase.AdditionalInformation = FunctionInfo;
                   ExceptionManager.Publish(exBase);
                   throw exBase;

               }
               return bResult;
           }
           public bool DeleteEquityTransaction(int eqTransId)
           {
               bool bResult = false;
               CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
               try
               {

                   bResult = customerTransactionDao.DeleteEquityTransaction(eqTransId);

               }
               catch (BaseApplicationException Ex)
               {
                   throw Ex;
               }
               return bResult;
           }
           public bool BulkEqTransactionDeletion(string EQTraxnIds)
           {
               CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();
               bool bResult = false;
               try
               {
                   bResult = customerTransactionDao.BulkEqTransactionDeletion(EQTraxnIds);
               }
               catch (BaseApplicationException Ex)
               {
                   throw Ex;
               }
               catch (Exception Ex)
               {
                   BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                   NameValueCollection FunctionInfo = new NameValueCollection();
                   FunctionInfo.Add("Method", "CustomerTransactionBo.cs:UpdateEquityTransaction()");
                   object[] objects = new object[2];
                   FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                   exBase.AdditionalInformation = FunctionInfo;
                   ExceptionManager.Publish(exBase);
                   throw exBase;

               }
               return bResult;
           }
           public EQTransactionVo GetEquityTransaction(int eqTransactionId, String Currency)
           {
               CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

               EQTransactionVo eqTransactionVo = new EQTransactionVo();

               try
               {
                   eqTransactionVo = customerTransactionDao.GetEquityTransaction(eqTransactionId, Currency);
               }
               catch (BaseApplicationException Ex)
               {
                   throw Ex;
               }
               catch (Exception Ex)
               {
                   BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                   NameValueCollection FunctionInfo = new NameValueCollection();

                   FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetEquityTransaction()");


                   object[] objects = new object[1];
                   objects[0] = eqTransactionId;

                   FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                   exBase.AdditionalInformation = FunctionInfo;
                   ExceptionManager.Publish(exBase);
                   throw exBase;

               }
               return eqTransactionVo;
           }



           public void ExcelToExportData(DataTable dt, string fileName)
           {
               //Response.ClearContent();
               //Response.Buffer = true;
               //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName + ".xls"));
               //Response.ContentType = "application/ms-excel";

               string str = string.Empty;
               foreach (DataColumn dtcol in dt.Columns)
               {
                   //Response.Write(str + dtcol.ColumnName);
                   str = "\t";
               }
               //Response.Write("\n");
               foreach (DataRow dr in dt.Rows)
               {
                   str = "";
                   for (int j = 0; j < dt.Columns.Count; j++)
                   {
                       //Response.Write(str + Convert.ToString(dr[j]));
                       str = "\t";
                   }
                   //Response.Write("\n");
               }


               //Response.End();
           }
    }
}
