using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.Sql;
using VoOnlineOrderManagemnet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using DaoOnlineOrderManagement;

namespace BoOnlineOrderManagement
{
    public class OnlineMFOrderBo:OnlineOrderBo
    {
        public DataSet GetMfOrderExtract(DateTime dtFrom, int adviserId,string orderType)
        {
            DataSet dsMfOrderExtract = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsMfOrderExtract = OnlineMFOrderDao.GetMfOrderExtract(dtFrom, adviserId, orderType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetMfOrderExtract()");

                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsMfOrderExtract;
        }

        public DataSet GetOrderBookMIS(int CustomerId,int AmcCode,string OrderStatus, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsOrderBookMIS = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsOrderBookMIS = OnlineMFOrderDao.GetOrderBookMIS(CustomerId,AmcCode,OrderStatus,dtFrom, dtTo);
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

        public DataSet GetFolioAccount(int CustomerId)
        {
            DataSet dsFolioAccount = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsFolioAccount = OnlineMFOrderDao.GetFolioAccount(CustomerId);
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
        public DataSet GetControlDetails(int scheme, string folio)
        {
            DataSet ds = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                ds = OnlineMFOrderDao.GetControlDetails(scheme, folio);
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
        public List<int> CreateOrderMFSipDetails(OnlineMFOrderVo onlineMFOrderVo, int userId)
        {
            List<int> orderIds = new List<int>();
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();

            try
            {
                orderIds = OnlineMFOrderDao.CreateOrderMFSipDetails(onlineMFOrderVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return orderIds;
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
        public DataSet GetSIPBookMIS(int CustomerId, int AccountId, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsSIPBookMIS = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsSIPBookMIS = OnlineMFOrderDao.GetSIPBookMIS(CustomerId, AccountId, dtFrom, dtTo);
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
        public DataSet GetSIPSummaryBookMIS(int CustomerId, int AccountId, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsSIPSummaryBookMIS = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsSIPSummaryBookMIS = OnlineMFOrderDao.GetSIPSummaryBookMIS(CustomerId, AccountId, dtFrom, dtTo);
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
        public DataSet GetSipDetails(int SchemeId, string frequency)
        {
            DataSet dsSipDetails = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsSipDetails = OnlineMFOrderDao.GetSipDetails(SchemeId, frequency);
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
        public DateTime[] GetSipStartDates(int schemePlanCode, string sipFreqCode)
        {
            DataSet dsSipDetails = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            List<DateTime> lstSipStartDates = new List<DateTime>();

            try
            {
                dsSipDetails = OnlineMFOrderDao.GetSipDetails(schemePlanCode, sipFreqCode);
                if (dsSipDetails == null) return lstSipStartDates.ToArray();

                string sipStartDates = dsSipDetails.Tables[0].Rows[0]["PASPSD_StatingDates"].ToString();

                List<int> lstSipDates = new List<int>();
                string[] temp = sipStartDates.Split(';');
                foreach (string date in temp) {
                    if (!string.IsNullOrEmpty(date.Trim()))
                        lstSipDates.Add(int.Parse(date.Trim()));
                }


                DateTime dateCurr = DateTime.Now;
                while (dateCurr <= DateTime.Now.AddMonths(3)) {
                    int res = lstSipDates.Find(delegate(int date) {
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
            int multiplier = 0;

            switch (SipFrequency)
            {
                case "MN":
                    multiplier = 1;
                    break;
                case "QT":
                    multiplier = 3;
                    break;
            }

            return StartDate.AddMonths(multiplier * installments);
        }
        public DataSet GetRedeemAmcDetails(int customerId)
        {
            DataSet dsGetRedeemSchemeDetails;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsGetRedeemSchemeDetails = OnlineMFOrderDao.GetRedeemAmcDetails(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetRedeemSchemeDetails;
        }
        public OnlineMFOrderVo GetOrderDetails(int Id)
        {
            OnlineMFOrderVo onlinemforderVo=new OnlineMFOrderVo();
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
    }
}
