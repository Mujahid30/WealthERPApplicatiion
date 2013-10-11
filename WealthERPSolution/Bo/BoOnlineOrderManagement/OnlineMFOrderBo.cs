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

        public DataSet GetOrderBookMIS(int adviserId, int CustomerId, int AccountId, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsOrderBookMIS = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsOrderBookMIS = OnlineMFOrderDao.GetOrderBookMIS(adviserId, CustomerId, AccountId, dtFrom, dtTo);
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
        public DataTable GetControlDetails(int scheme)
        {
            DataTable dt = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dt = OnlineMFOrderDao.GetControlDetails(scheme);
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
            return dt;
        }
        public int CreateCustomerOnlineMFOrderDetails(OnlineMFOrderVo onlinemforderVo, int UserId, int CustomerId)
        {
            OnlineMFOrderDao onlineOrderdao = new OnlineMFOrderDao();
            int orderId;
            try
            {
                orderId = onlineOrderdao.CreateCustomerOnlineMFOrderDetails(onlinemforderVo, UserId, CustomerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return orderId;
        }
        public DataSet GetSIPBookMIS(int adviserId, int CustomerId, int AccountId, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsSIPBookMIS = null;
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsSIPBookMIS = OnlineMFOrderDao.GetSIPBookMIS(adviserId, CustomerId, AccountId, dtFrom, dtTo);
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
    }
}
