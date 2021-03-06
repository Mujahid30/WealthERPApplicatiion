﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.Sql;
using System.Reflection;
using VoOnlineOrderManagemnet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using DaoOnlineOrderManagement;
using BoOnlineOrderManagement.DemoBSEMFOrderEntry;
using System.Configuration;
using System.IO;
namespace BoOnlineOrderManagement
{
    public class OnlineMFOrderBo : OnlineOrderBo
    {
        public DataSet GetOrderBookMIS(int CustomerId, int AmcCode, string OrderStatus, DateTime dtFrom, DateTime dtTo, string orderType)
        {
            DataSet dsOrderBookMIS = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsOrderBookMIS = OnlineMFOrderDao.GetOrderBookMIS(CustomerId, AmcCode, OrderStatus, dtFrom, dtTo, orderType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetOrderStepsDetails()");

                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsOrderBookMIS;
        }

        public DataSet GetFolioAccount(int CustomerId, int exchangeType)
        {
            DataSet dsFolioAccount = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsFolioAccount = OnlineMFOrderDao.GetFolioAccount(CustomerId, exchangeType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OrderBo.cs:GetOrderStepsDetails()");

                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsFolioAccount;
        }
        public DataSet GetOrderStatus()
        {
            DataSet dsOrderStatus = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            {
                dsOrderStatus = OnlineMFOrderDao.GetOrderStatus();
            }

            return dsOrderStatus;
        }
        public DataSet GetControlDetails(int scheme, string folio, int demat)
        {
            DataSet ds = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                ds = OnlineMFOrderDao.GetControlDetails(scheme, folio, demat);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OrderBo.cs:GetOrderStepsDetails()");

                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
        public IDictionary<string, string> CreateOrderMFSipDetails(OnlineMFOrderVo onlineMFOrderVo, int userId)
        {
            IDictionary<string, string> sipOrderIds = new Dictionary<string, string>();
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();

            try
            {
                sipOrderIds = OnlineMFOrderDao.CreateOrderMFSipDetails(onlineMFOrderVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return sipOrderIds;
        }
        public List<int> CreateCustomerOnlineMFOrderDetails(OnlineMFOrderVo onlinemforderVo, int UserId, int CustomerId)
        {
            List<int> orderIds = new List<int>();
            OnlineMFOrderDao onlineOrderdao = new OnlineMFOrderDao();

            try
            {

                orderIds = onlineOrderdao.CreateCustomerOnlineMFOrderDetails(onlinemforderVo, UserId, CustomerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return orderIds;
        }
        public DataSet GetSIPBookMIS(int CustomerId, int AmcCode, string OrderStatus, int systematicId)
        {
            DataSet dsSIPBookMIS = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsSIPBookMIS = OnlineMFOrderDao.GetSIPBookMIS(CustomerId, AmcCode, OrderStatus, systematicId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetOrderStepsDetails()");

                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsSIPBookMIS;
        }
        public DataSet GetSIPSummaryBookMIS(int CustomerId, int AmcCode, string systematicType, string SIPMode)
        {
            DataSet dsSIPSummaryBookMIS = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsSIPSummaryBookMIS = OnlineMFOrderDao.GetSIPSummaryBookMIS(CustomerId, AmcCode, systematicType, SIPMode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OrderBo.cs:GetSIPSummaryBookMIS()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsSIPSummaryBookMIS;
        }
        public DataSet GetSipDetails(int SchemeId, string frequency, bool IsDemat)
        {
            DataSet dsSipDetails = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsSipDetails = OnlineMFOrderDao.GetSipDetails(SchemeId, frequency, IsDemat);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OrderBo.cs:GetSipDetails()");

                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsSipDetails;
        }
        /// <summary>
        /// Gets the Sip dates from the database for the scheme & sipFreq
        /// </summary>
        /// <param name="schemePlanCode"></param>
        /// <param name="sipFreqCode"></param>
        /// <returns></returns>
        public DateTime[] GetSipStartDates(int schemePlanCode, string sipFreqCode, bool IsDemat)
        {
            DataSet dsSipDetails = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            List<DateTime> lstSipStartDates = new List<DateTime>();

            try
            {
                dsSipDetails = OnlineMFOrderDao.GetSipDetails(schemePlanCode, sipFreqCode, IsDemat);
                if (dsSipDetails == null) return lstSipStartDates.ToArray();

                string sipStartDates = dsSipDetails.Tables[0].Rows[0]["PASPSD_StatingDates"].ToString();

                List<int> lstSipDates = new List<int>();
                string[] temp = sipStartDates.Split(';');
                foreach (string date in temp)
                {
                    if (!string.IsNullOrEmpty(date.Trim()))
                        lstSipDates.Add(int.Parse(date.Trim()));
                }


                DateTime dateCurr = DateTime.Now;
                if (DateTime.Now.TimeOfDay > System.TimeSpan.Parse("12:59:00"))
                    dateCurr = DateTime.Now.AddDays(1);

                while (dateCurr <= DateTime.Now.AddMonths(3))
                {
                    int res = lstSipDates.Find(delegate(int date)
                    {
                        return date == dateCurr.Day;
                    });

                    if (res > 0) lstSipStartDates.Add(dateCurr);
                    dateCurr = dateCurr.AddDays(1);
                }
                return lstSipStartDates.ToArray();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OrderBo.cs:GetSipStartDates(int schemePlanCode, string sipFreqCode)");
                object[] objects = new object[2];
                objects[0] = schemePlanCode;
                objects[1] = sipFreqCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        /// <summary>
        /// Gets SIP end date
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="SipFrequency"></param>
        /// <param name="installments"></param>
        /// <returns></returns>
        public DateTime GetSipEndDate(DateTime StartDate, string SipFrequency, int installments)
        {
            int multiplier = 0, addition = 0;

            switch (SipFrequency)
            {
                case "DA":
                    addition = 1;
                    return StartDate.AddDays(addition + installments);


                case "MN":
                    multiplier = 1;
                    return StartDate.AddMonths(multiplier * installments);

                case "QT":
                    multiplier = 3;
                    return StartDate.AddMonths(multiplier * installments);

                case "YR":
                    multiplier = 12;
                    return StartDate.AddMonths(multiplier * installments);

            }
            return StartDate;

        }
        public DataSet GetCustomerHoldingAMCList(int customerId, char type)
        {
            DataSet dsCustomerHoldingAMCList;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsCustomerHoldingAMCList = OnlineMFOrderDao.GetCustomerHoldingAMCList(customerId, type);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsCustomerHoldingAMCList;
        }
        public OnlineMFOrderVo GetOrderDetails(int Id)
        {
            OnlineMFOrderVo onlinemforderVo = new OnlineMFOrderVo();
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                onlinemforderVo = OnlineMFOrderDao.GetOrderDetails(Id);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return onlinemforderVo;
        }

        public void TriggerAutoOrderFromSIP()
        {

            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                OnlineMFOrderDao.TriggerAutoOrderFromSIP();
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }
        public DataSet GetSIPAmcDetails(int customerId, string systematicType)
        {
            DataSet dsGetSIPAmcDetails;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsGetSIPAmcDetails = OnlineMFOrderDao.GetSIPAmcDetails(customerId, systematicType);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetSIPAmcDetails;
        }
        public DataSet GetOrderAmcDetails(int customerId, bool IsRTA)
        {
            DataSet dsGetOrderAmcDetails;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsGetOrderAmcDetails = OnlineMFOrderDao.GetOrderAmcDetails(customerId, IsRTA);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetOrderAmcDetails;
        }
        public DataSet GetTransAllAmcDetails(int customerId)
        {
            DataSet dsGetTransAllAmcDetails;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsGetTransAllAmcDetails = OnlineMFOrderDao.GetTransAllAmcDetails(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetTransAllAmcDetails;
        }
        public bool UpdateCnacleRegisterSIP(int systematicId, int is_Cancel, string remark, int cancelBy)
        {
            bool bResult = false;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                bResult = OnlineMFOrderDao.UpdateCnacleRegisterSIP(systematicId, is_Cancel, remark, cancelBy);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBo.cs:UpdateCnacleRegisterSIP()");


                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public DataTable GetMFSchemeDetailsForLanding(int Schemeplancode, string category)
        {

            DataTable dtGetMFSchemeDetailsForLanding;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();

            try
            {
                dtGetMFSchemeDetailsForLanding = OnlineMFOrderDao.GetMFSchemeDetailsForLanding(Schemeplancode, category);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineMFOrderBo.cs:GetMFSchemeDetailsForLanding()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetMFSchemeDetailsForLanding;
        }

        public DataSet GetCustomerSchemeFolioHoldings(int customerId, int schemeId, out string schemeDividendOption, int Demate, int accountId)
        {
            DataSet ds = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                ds = OnlineMFOrderDao.GetCustomerSchemeFolioHoldings(customerId, schemeId, out schemeDividendOption, Demate, accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineMFOrderBo.cs:GetCustomerSchemeFolioHoldings(int customerId, int schemeId)");

                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
        public DataTable creataTableForSwitch(List<OnlineMFOrderVo> lsonlinemforder)
        {
            OnlineMFOrderVo onlineMFOrderVo = new OnlineMFOrderVo();
            DataTable dtSwitchOrder = new DataTable();
            dtSwitchOrder.Columns.Add("CMFSO_AccountId");
            dtSwitchOrder.Columns.Add("CMFSO_SchemePlanCode");
            dtSwitchOrder.Columns.Add("CMFSO_Amount");
            dtSwitchOrder.Columns.Add("CMFSO_TransactionType");
            dtSwitchOrder.Columns.Add("CMFSO_DivOption");
            dtSwitchOrder.Columns.Add("CO_OrderId");

            DataRow drOrderDetails;
            for (int i = 0; i < lsonlinemforder.Count; i++)
            {
                onlineMFOrderVo = lsonlinemforder[i];
                drOrderDetails = dtSwitchOrder.NewRow();
                drOrderDetails["CMFSO_AccountId"] = onlineMFOrderVo.AccountId.ToString();
                drOrderDetails["CMFSO_SchemePlanCode"] = onlineMFOrderVo.SchemePlanCode.ToString();
                drOrderDetails["CMFSO_Amount"] = onlineMFOrderVo.Amount.ToString();
                drOrderDetails["CMFSO_TransactionType"] = onlineMFOrderVo.TransactionType.ToString();
                if (onlineMFOrderVo.DivOption == string.Empty)
                    drOrderDetails["CMFSO_DivOption"] = onlineMFOrderVo.DivOption.ToString();
                else
                    drOrderDetails["CMFSO_DivOption"] = null;
                dtSwitchOrder.Rows.Add(drOrderDetails);
            }
            return dtSwitchOrder;

        }
        public List<int> CreateOnlineMFSwitchOrderDetails(List<OnlineMFOrderVo> lsonlinemforder, int userId, int customerId)
        {
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            DataTable dtSwitchOrder = new DataTable();
            List<int> OrderIds = new List<int>();

            try
            {
                dtSwitchOrder = creataTableForSwitch(lsonlinemforder);
                OrderIds = OnlineMFOrderDao.CreateOnlineMFSwitchOrderDetails(dtSwitchOrder, userId, customerId);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineMFOrderBo.cs:GetCustomerSchemeFolioHoldings(int customerId, int schemeId)");

                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return OrderIds;


        }
        public int CreateMandateOrder(int customerId, double Amount, int BankName, string BankBranch, int UserId, int mandateId,string BankAccNo,string IFSCCode)
        {
            int result = 0;
            try
            {
                OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
                {
                    result = OnlineMFOrderDao.CreateMandateOrder(customerId, Amount, BankName, BankBranch, UserId, mandateId, BankAccNo, IFSCCode);
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
                FunctionInfo.Add("Method", "CreateMandateOrder(int customerId, double Amount, string BankName, string BankBranch, int UserId)");

                object[] objects = new object[5];
                objects[0] = customerId;
                objects[1] = Amount;
                objects[2] = BankName;
                objects[3] = BankBranch;
                objects[4] = UserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }
        public DataSet GetOrderIssueStatus()
        {
            DataSet dsOrderStatus = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            {
                dsOrderStatus = OnlineMFOrderDao.GetOrderIssueStatus();
            }

            return dsOrderStatus;
        }
        public DataSet GetCustomerOrderBookTransaction(int customerId, int amcCode, int schemeCode, string orderType, int exchangeType)
        {
            DataSet dsGetCustomerOrderBookTransaction;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsGetCustomerOrderBookTransaction = OnlineMFOrderDao.GetCustomerOrderBookTransaction(customerId, amcCode, schemeCode, orderType, exchangeType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetCustomerOrderBookTransaction;
        }
        public DataTable GetCustomerFolioSchemeWise(int customerId, int schemeCode, int IsDemat)
        {
            DataTable dt;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();

            try
            {
                dt = OnlineMFOrderDao.GetCustomerFolioSchemeWise(customerId, schemeCode, IsDemat);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
        public bool DebitOrCreditRMSUserAccountBalance(int userId, string userAccountId, double amount, int rmsID, out int resultRmsId)
        {
            bool result = false;
            string Response = string.Empty;
            resultRmsId = 0;
            string rmsType = amount < 0 ? "Debit" : "Credit";
            DateTime requestTime = DateTime.Now;
            DateTime responseTime = DateTime.Now;
            OnlineOrderDao onlineOrderDao = new OnlineOrderDao();
            string rmsAPI = ConfigurationSettings.AppSettings["RMS_USER_ACCOUNT_BALANCE_API"];
            DataTable dt = new DataTable();

            try
            {
                rmsAPI = rmsAPI.Replace("#UserAccountId#", userAccountId);
                rmsAPI = rmsAPI.Replace("#Amount#", amount.ToString());
                requestTime = DateTime.Now;
                Response = TrigerAPI(rmsAPI);
                responseTime = DateTime.Now;
                StringReader theReader = new StringReader(Response);
                DataSet theDataSet = new DataSet();
                theDataSet.ReadXml(theReader);
                dt = theDataSet.Tables[0];
                if (Response.Contains("TRUE"))
                {
                    resultRmsId = onlineOrderDao.CreateOrUpdateRMSLog(userId, rmsID, 1, rmsType, amount, requestTime, responseTime, string.Empty, "RMSREsponse:-" + Response, dt.Rows[0]["ReferenceNumber"].ToString());
                    result = true;

                }
                if (Response.Contains("FALSE"))
                {
                    resultRmsId = onlineOrderDao.CreateOrUpdateRMSLog(userId, rmsID, 0, rmsType, amount, requestTime, responseTime, string.Empty, "RMSREsponse:-" + Response, dt.Rows[0]["ReferenceNumber"].ToString());
                    result = false;

                }
            }
            catch (Exception Ex)
            {
                resultRmsId = onlineOrderDao.CreateOrUpdateRMSLog(userId, rmsID, 0, rmsType, amount, requestTime, responseTime, string.Empty, ("RMSREsponse:-" + Response + "ERROR:-" + Ex.Message), dt.Rows[0]["ReferenceNumber"].ToString());
                result = false;
            }
            return result;

        }
        public string BSEorderEntryParam(int UserID, string ClientCode, OnlineMFOrderVo onlinemforderVo, int CustomerId, string DematAcctype, out char msgType)
        {
            DemoBSEMFOrderEntry.MFOrderEntryClient webOrderEntryClient = new DemoBSEMFOrderEntry.MFOrderEntryClient();
            List<int> orderIds = new List<int>();
            msgType = 'F';
            string message = string.Empty;
            bool isRMSDebited = false;
            int rmsId = 0;
            try
            {
                string PurchaseType = string.Empty;
                string purchase = string.Empty;
                OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
                string bseuserID = string.Empty;
                string bsepass = string.Empty;
                string bseMemberId = string.Empty;
                DataSet ds = OnlineMFOrderDao.GetAPICredentials("BSE", 1021);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    bseuserID = ds.Tables[0].Rows[0]["AEAC_Username"].ToString();
                    bsepass = ds.Tables[0].Rows[0]["AEAC_Password"].ToString();
                    bseMemberId = ds.Tables[0].Rows[0]["AEAC_MemberId"].ToString();
                }
                string passkey = "E234586789D12";
                string password = webOrderEntryClient.getPassword(bseuserID, bsepass, passkey);
                string[] bsePassArray = password.Split('|');
                if (bsePassArray[0].ToString() == "100")
                {
                    if (onlinemforderVo.DividendType != "0" && onlinemforderVo.DividendType != "")
                        onlinemforderVo.BSESchemeCode = OnlineMFOrderDao.BSESchemeCode(onlinemforderVo.SchemePlanCode, onlinemforderVo.DividendType);
                    string allRedeem = "N";
                    string DPCFlag = "Y";
                    string Amount = string.Empty;
                    string Unit = string.Empty;
                    if (DematAcctype == "CDSL" || DematAcctype == "")
                    {
                        DematAcctype = "C";
                    }
                    else if (DematAcctype == "NSDL")
                    {
                        DematAcctype = "N";
                    }
                    if (onlinemforderVo.TransactionType == "BUY")
                    {
                        Amount = onlinemforderVo.Amount.ToString();
                        PurchaseType = "FRESH";
                        purchase = "P";
                    }
                    else if (onlinemforderVo.TransactionType == "ABY")
                    {
                        Amount = onlinemforderVo.Amount.ToString();
                        PurchaseType = "ADDITIONAL";
                        purchase = "P";
                    }
                    else if (onlinemforderVo.TransactionType == "SEL")
                    {
                        if (onlinemforderVo.IsAllUnits)
                        {
                            allRedeem = "Y";
                        }
                        else
                            Unit = onlinemforderVo.Redeemunits.ToString();
                        PurchaseType = "FRESH";
                        purchase = "R";
                        DPCFlag = "N";
                    }
                    if (onlinemforderVo.TransactionType != "SEL")
                    {
                        isRMSDebited = DebitOrCreditRMSUserAccountBalance(UserID, ClientCode, -onlinemforderVo.Amount, rmsId, out  rmsId);
                    }
                    else if (onlinemforderVo.TransactionType == "SEL")
                    {
                        isRMSDebited = true;
                    }
                    if (isRMSDebited)
                    {
                        int transCode = OnlineMFOrderDao.BSEorderEntryParam("NEW", UserID, ClientCode, onlinemforderVo.BSESchemeCode, purchase, PurchaseType, DematAcctype, Amount, Unit, allRedeem, "", "Y", "", "", "E116327", "Y", "N", DPCFlag, "", rmsId);
                        string uniqueRefNo;
                        Random ran = new Random();
                        uniqueRefNo = transCode.ToString() + ran.Next().ToString();
                        string orderEntryresponse = webOrderEntryClient.orderEntryParam("NEW", uniqueRefNo, "", bseuserID, bseMemberId, ClientCode, onlinemforderVo.BSESchemeCode, purchase, PurchaseType, DematAcctype, Amount, Unit, allRedeem, "", "", "Y", "", "", "E116327", "Y", "N", DPCFlag, "", bsePassArray[1], passkey, "", "", "");
                        string[] bseorderEntryresponseArray = orderEntryresponse.Split('|');
                        OnlineMFOrderDao.BSEorderResponseParam(transCode, UserID, Convert.ToInt64(bseorderEntryresponseArray[2]), ClientCode, bseorderEntryresponseArray[6], bseorderEntryresponseArray[7], rmsId, uniqueRefNo);
                        if (Convert.ToInt32(bseorderEntryresponseArray[7]) == 1)
                        {
                            if (onlinemforderVo.TransactionType != "SEL")
                            {
                                DebitOrCreditRMSUserAccountBalance(UserID, ClientCode, onlinemforderVo.Amount, rmsId, out  rmsId);

                            }
                            message = "Order cannot be processed." + bseorderEntryresponseArray[6];
                        }
                        else if ((Convert.ToInt32(bseorderEntryresponseArray[7]) == 0))
                        {
                            orderIds = CreateCustomerOnlineMFOrderDetails(onlinemforderVo, UserID, CustomerId);
                            bool result = OnlineMFOrderDao.BSERequestOrderIdUpdate(orderIds[0], transCode, rmsId);
                            message = "Order placed successfully with Exchange. Order reference no is " + orderIds[0].ToString();
                            msgType = 'S';
                        }
                    }
                    else
                    {
                        message = "No response from RMS";

                    }
                }
                else
                    message = "Unable to process the order as Exchange is not available for Now.";
            }
            catch (Exception ex)
            {
                if (isRMSDebited && onlinemforderVo.TransactionType != "SEL")
                {
                    DebitOrCreditRMSUserAccountBalance(UserID, ClientCode, onlinemforderVo.Amount, rmsId, out  rmsId);
                }
                message = "Unable to process the order as Exchange is not available for Now." + ex.Message;
            }
            finally
            {
                webOrderEntryClient.Close();

            }

            return message;
        }


        public string BSESIPorderEntryParam(int UserID, string ClientCode, OnlineMFOrderVo onlinemforderVo, int CustomerId, string DematAcctype, out char msgType, out  IDictionary<string, string> sipOrderIds)
        {
            DemoBSEMFOrderEntry.MFOrderEntryClient webOrderEntryClient = new DemoBSEMFOrderEntry.MFOrderEntryClient();
            BSEMFSIPOdererVo bseMFSIPOdererVo = new BSEMFSIPOdererVo();
            
            msgType = 'F';
            string message = string.Empty;
            int systematicId = 0;
            int rmsId = 0;
           
            sipOrderIds = null;


            try
            {
                string orderEntryresponse = string.Empty;
                string PurchaseType = string.Empty;
                string purchase = string.Empty;
                OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
                string bseuserID = string.Empty;
                string bsepass = string.Empty;
                bool result=false;
                string bseMemberId = string.Empty;

                DataSet ds = OnlineMFOrderDao.GetAPICredentials("BSE", 1021);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    bseuserID = ds.Tables[0].Rows[0]["AEAC_Username"].ToString();
                    bsepass = ds.Tables[0].Rows[0]["AEAC_Password"].ToString();
                    bseMemberId = ds.Tables[0].Rows[0]["AEAC_MemberId"].ToString();
                }
                string passkey = "E234586789D12";
                string password = webOrderEntryClient.getPassword(bseuserID, bsepass, passkey);
                string[] bsePassArray = password.Split('|');

                if (bsePassArray[0].ToString() == "100")
                {
                    if (onlinemforderVo.DividendType != "0" && onlinemforderVo.DividendType != "")
                        onlinemforderVo.BSESchemeCode = OnlineMFOrderDao.BSESchemeCode(onlinemforderVo.SchemePlanCode, onlinemforderVo.DividendType);
                    if (DematAcctype == "CDSL" || DematAcctype == "")
                    {
                        DematAcctype = "C";
                    }
                    else if (DematAcctype == "NSDL")
                    {
                        DematAcctype = "N";
                    }
                    bseMFSIPOdererVo.Password = bsePassArray[1];
                    if (onlinemforderVo.IsCancelled != "True" && onlinemforderVo.IsCancelled==null)
                    {
                        bseMFSIPOdererVo.Transactioncode = "NEW";
                    }
                    else
                    {
                        bseMFSIPOdererVo.Transactioncode = "CXL";
                        systematicId = onlinemforderVo.SystematicId;
                       
                    }
                    bseMFSIPOdererVo.BSEOrderId = 0;
                    bseMFSIPOdererVo.BSEUserId = bseuserID;
                    bseMFSIPOdererVo.MemberId = bseMemberId;
                    bseMFSIPOdererVo.ClientCode = ClientCode;
                    bseMFSIPOdererVo.SchemeCode = onlinemforderVo.BSESchemeCode;
                    //bseMFSIPOdererVo.InternalReferenceNo = transCode;
                    bseMFSIPOdererVo.TransMode = "D";
                    bseMFSIPOdererVo.DPTransactionMode = DematAcctype;
                    bseMFSIPOdererVo.StartDate = String.Format("{0:d}", onlinemforderVo.StartDate);
                    bseMFSIPOdererVo.FrequenceType = GetBSESIPFrequencyCode(onlinemforderVo.FrequencyCode);
                    bseMFSIPOdererVo.FrequenceAllowed = "1";
                    bseMFSIPOdererVo.InstallmentAmount = Convert.ToInt16(onlinemforderVo.Amount).ToString();
                    bseMFSIPOdererVo.NoOfInstallments = onlinemforderVo.TotalInstallments.ToString();
                    bseMFSIPOdererVo.Remarks = string.Empty;
                    bseMFSIPOdererVo.FolioNo = string.Empty;// As its demat SIP
                    bseMFSIPOdererVo.FirstOrderFlag = "Y";
                    bseMFSIPOdererVo.SubBRCode = string.Empty;
                    bseMFSIPOdererVo.EUIN = "E116327";
                    bseMFSIPOdererVo.EUINDeclarationFlag = "Y";
                    bseMFSIPOdererVo.DPC = "Y";
                    bseMFSIPOdererVo.REGID = onlinemforderVo.IsCancelled == "True" ? onlinemforderVo.BSEREGID.ToString(): string.Empty;
                    bseMFSIPOdererVo.IPAddress = string.Empty;
                    bseMFSIPOdererVo.Password = bsePassArray[1];
                    bseMFSIPOdererVo.PassKey = passkey;
                    bseMFSIPOdererVo.Param1 = string.Empty;
                    bseMFSIPOdererVo.Param2 = string.Empty;
                    bseMFSIPOdererVo.Param3 = string.Empty;
                    bseMFSIPOdererVo.MandateId = onlinemforderVo.MandateId.ToString();
                    Random ran = new Random();

                    int transCode = OnlineMFOrderDao.BSEMFSIPorderResponseParam(bseMFSIPOdererVo, rmsId, UserID);

                    bseMFSIPOdererVo.UniqueReferanceNumber = transCode.ToString() + ran.Next().ToString();
                    bseMFSIPOdererVo.InternalReferenceNo = transCode.ToString();
                    if (onlinemforderVo.ModeTypeCode == "BXSIP")
                    {
                        orderEntryresponse = webOrderEntryClient.xsipOrderEntryParam(bseMFSIPOdererVo.Transactioncode,
                            bseMFSIPOdererVo.UniqueReferanceNumber, bseMFSIPOdererVo.SchemeCode, bseMFSIPOdererVo.MemberId, bseMFSIPOdererVo.ClientCode, bseMFSIPOdererVo.BSEUserId
                            , bseMFSIPOdererVo.InternalReferenceNo, bseMFSIPOdererVo.TransMode, bseMFSIPOdererVo.DPTransactionMode
                            , bseMFSIPOdererVo.StartDate, bseMFSIPOdererVo.FrequenceType, bseMFSIPOdererVo.FrequenceAllowed, bseMFSIPOdererVo.InstallmentAmount
                            , bseMFSIPOdererVo.NoOfInstallments, bseMFSIPOdererVo.Remarks, bseMFSIPOdererVo.FolioNo, bseMFSIPOdererVo.FirstOrderFlag
                            , bseMFSIPOdererVo.SubBRCode, bseMFSIPOdererVo.MandateId, bseMFSIPOdererVo.SubBRCode, bseMFSIPOdererVo.EUIN, bseMFSIPOdererVo.EUINDeclarationFlag, bseMFSIPOdererVo.DPC, bseMFSIPOdererVo.REGID
                             , bseMFSIPOdererVo.IPAddress, bseMFSIPOdererVo.Password, bseMFSIPOdererVo.PassKey,
                        bseMFSIPOdererVo.Param1, bseMFSIPOdererVo.Param2, bseMFSIPOdererVo.Param3);
                    }
                    else if (onlinemforderVo.ModeTypeCode == "BSSIP")
                    {
                        orderEntryresponse = webOrderEntryClient.sipOrderEntryParam(bseMFSIPOdererVo.Transactioncode,
                           bseMFSIPOdererVo.UniqueReferanceNumber, bseMFSIPOdererVo.SchemeCode, bseMFSIPOdererVo.MemberId, bseMFSIPOdererVo.ClientCode, bseMFSIPOdererVo.BSEUserId,
                           bseMFSIPOdererVo.InternalReferenceNo, bseMFSIPOdererVo.TransMode, bseMFSIPOdererVo.DPTransactionMode,
                           bseMFSIPOdererVo.StartDate, bseMFSIPOdererVo.FrequenceType, bseMFSIPOdererVo.FrequenceAllowed, bseMFSIPOdererVo.InstallmentAmount,
                           bseMFSIPOdererVo.NoOfInstallments, bseMFSIPOdererVo.Remarks, bseMFSIPOdererVo.FolioNo, bseMFSIPOdererVo.FirstOrderFlag,
                           bseMFSIPOdererVo.SubBRCode, bseMFSIPOdererVo.EUIN, bseMFSIPOdererVo.EUINDeclarationFlag, bseMFSIPOdererVo.DPC, bseMFSIPOdererVo.REGID,
                           bseMFSIPOdererVo.IPAddress, bseMFSIPOdererVo.Password, bseMFSIPOdererVo.PassKey,
                           bseMFSIPOdererVo.Param1, bseMFSIPOdererVo.Param2, bseMFSIPOdererVo.Param3);

                    }

                    string[] bseorderEntryresponseArray = orderEntryresponse.Split('|');
                    Int64 bseSIPId = 0;
                    Int64.TryParse(bseorderEntryresponseArray[5], out bseSIPId);
                    OnlineMFOrderDao.BSEMFSIPorderResponseParam(transCode, UserID, bseSIPId, bseMemberId, ClientCode, bseorderEntryresponseArray[6], bseorderEntryresponseArray[7], rmsId, bseMFSIPOdererVo.UniqueReferanceNumber);
                    if (bseorderEntryresponseArray[7] == "0")
                    {
                        if (onlinemforderVo.ModeTypeCode == "BXSIP" && onlinemforderVo.IsCancelled !="True")
                        {
                            OnlineMFOrderDao.UpdateSystematicStep(onlinemforderVo.SystematicId, "IP", UserID);
                            systematicId = onlinemforderVo.SystematicId;
                        }
                        else if (onlinemforderVo.ModeTypeCode == "BSSIP" && onlinemforderVo.IsCancelled != "True")
                        {
                            sipOrderIds = CreateOrderMFSipDetails(onlinemforderVo, UserID);
                            systematicId = Convert.ToInt32(sipOrderIds["SIPId"].ToString());
                        }
                        result = OnlineMFOrderDao.BSESIPRequestUpdate(systematicId, transCode);
                        msgType = 'S';
                    }
                    else if (bseorderEntryresponseArray[7] != "0" && onlinemforderVo.ModeTypeCode == "BXSIP")
                    {
                        OnlineMFOrderDao.UpdateSystematicStep(onlinemforderVo.SystematicId, "RJ", UserID);
                    }
                    message = bseorderEntryresponseArray[6];
                    //}
                    //else
                    //{
                    //    message = "No response from RMS";

                    //}

                }
                else
                {
                    message = "Unable to process the order as Exchange is not available for Now.";
                }
            }
             catch (Exception ex)
            {

                message = "Unable to process the order as Exchange is not available for Now." + ex.Message;
            }
            finally
            {
                webOrderEntryClient.Close();

            }

            return message;
        }

        private string GetBSESIPFrequencyCode(string frequencyCode)
        {
            string BSEFrequencyCode = string.Empty;
            switch (frequencyCode)
            {
                case "MN":
                    BSEFrequencyCode = "MONTHLY";
                    break;
                case "QA":
                    BSEFrequencyCode = "QUARTELY";
                    break;
                case "WK":
                    BSEFrequencyCode = "WEEKLY";
                    break;
                default:
                    BSEFrequencyCode = "MONTHLY";
                    break;
            }
            return BSEFrequencyCode;

        }

        public DataSet BindMandateddetailsDetails(int adviserId)
        {
            DataSet ds = null; ;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                ds = OnlineMFOrderDao.BindMandateddetailsDetails(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {

            }
            return ds;
        }

    }
}
