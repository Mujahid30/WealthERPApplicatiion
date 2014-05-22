﻿using System;
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
    public class OnlineOrderMISBo
    {
        public DataSet GetOrderBookMIS(int adviserId, int AmcCode, string OrderStatus, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsOrderBookMIS = null;
            OnlineOrderMISDao OnlineOrderMISDao = new OnlineOrderMISDao();
            try
            {
                dsOrderBookMIS = OnlineOrderMISDao.GetOrderBookMIS(adviserId, AmcCode, OrderStatus, dtFrom, dtTo);
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

        public DataSet GetSIPBookMIS(int adviserId, int AmcCode, string OrderStatus, int systematicId, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsSIPBookMIS = null;
            OnlineOrderMISDao OnlineOrderMISDao = new OnlineOrderMISDao();
            try
            {
                dsSIPBookMIS = OnlineOrderMISDao.GetSIPBookMIS(adviserId, AmcCode, OrderStatus, systematicId, dtFrom, dtTo);
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
        public DataSet GetSIPSummaryBookMIS(int adviserId, int AmcCode, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsSIPSummaryBookMIS = null;
            OnlineOrderMISDao OnlineOrderMISDao = new OnlineOrderMISDao();
            try
            {
                dsSIPSummaryBookMIS = OnlineOrderMISDao.GetSIPSummaryBookMIS(adviserId, AmcCode, dtFrom, dtTo);
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
        public DataSet GetSchemeMIS(string Assettype, int Onlinetype, string Status)
        {
            DataSet dsSchemeMIS;
            OnlineOrderMISDao onlineOrderMISDao = new OnlineOrderMISDao();

            try
            {

                dsSchemeMIS = onlineOrderMISDao.GetSchemeMIS(Assettype, Onlinetype, Status);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderMISBo.cs:GetMfOrderExtract()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSchemeMIS;
        }
        public DataTable GetAdviserCustomerTransaction(int adviserId, int AmcCode, DateTime dtFrom, DateTime dtTo, int PageSize, int CurrentPage, string CustomerNamefilter, string custCode, string panNo, string folioNo, string schemeName, string type, string dividentType, string fundName, int orderNo, out int RowCount)
        {
            DataTable dtGetAdviserCustomerTransaction;
            OnlineOrderMISDao OnlineOrderMISDao = new OnlineOrderMISDao();
            dtGetAdviserCustomerTransaction = OnlineOrderMISDao.GetAdviserCustomerTransaction(adviserId, AmcCode, dtFrom, dtTo, PageSize, CurrentPage, CustomerNamefilter,custCode,panNo,folioNo,schemeName,type,dividentType,fundName,orderNo, out RowCount);
            return dtGetAdviserCustomerTransaction;
        }
    }
}
