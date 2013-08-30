using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using DaoOps;
using VoOps;
namespace BoOps
{
    public class FIOrderBo : OrderBo
    {
        FIOrderDao mfOrderDao = new FIOrderDao();
        public int GetOrderNumber()
        {
            int orderNumber = 0;
            try
            {
                orderNumber = mfOrderDao.GetOrderNumber();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetOrderNumber()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return orderNumber;
        }
        public DataSet GetCustomerBank(int customerId)
        {
            DataSet dsGetCustomerBank;
            try
            {
                dsGetCustomerBank = mfOrderDao.GetCustomerBank(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetCustomerBank;
        }

        public DataSet GetFICategory()
        {
            DataSet dsGetFICategory;
            try
            {
                dsGetFICategory = mfOrderDao.GetFICategory();
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetFICategory;

        }
        public DataSet GetFIIssuer(int AdviserID)
        {
            DataSet dsGetFIIssuer;
            try
            {
                dsGetFIIssuer = mfOrderDao.GetFIIssuer(AdviserID);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetFIIssuer;

        }

        public DataSet GetFIScheme(int AdviserID, int IssuerID)
        {
            DataSet dsGetFIScheme;
            try
            {
                dsGetFIScheme = mfOrderDao.GetFIScheme(AdviserID, IssuerID);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetFIScheme;
        }
        public DataSet GetFISeries(int SchemeID)
        {
            DataSet dsGetFISeries;
            try
            {
                dsGetFISeries = mfOrderDao.GetFISeries(SchemeID);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetFISeries;

        }
        public DataSet GetFISeriesDetailssDetails(int SchemeID)
        {
            DataSet dsSeriesDetailssDetails;
            try
            {
                dsSeriesDetailssDetails = mfOrderDao.GetFISeriesDetailssDetails(SchemeID);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsSeriesDetailssDetails;
        }
        public DataTable GetOrderList(int advisorId, string rmId, string branchId, DateTime toDate, DateTime fromDate, string status, string customerId, string orderType, string usertype, int AgentId, string SubBrokerCode, string AgentCode)
        {
            DataTable dtOrder = null;
           
            try
            {
                dtOrder = mfOrderDao.GetOrderList(advisorId, rmId, branchId, toDate, fromDate, status, customerId, orderType, usertype, AgentId, SubBrokerCode, AgentCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetOrderList()");

                object[] objects = new object[1];
                objects[0] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtOrder;
        }
        public String GetTaxStatus(int customerId)
        {

            String StrTaxStatus;
            try
            {
                StrTaxStatus = mfOrderDao.GetTaxStatus(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return StrTaxStatus;



        }

        public DataSet GetCustomerAssociates(int customerId)
        {
            DataSet dsCustomerAssociates;
            try
            {
                dsCustomerAssociates = mfOrderDao.GetCustomerAssociates(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsCustomerAssociates;
        }
        public List<int> CreateOrderFIDetails(OrderVo orderVo, FIOrderVo fiorderVo, int userId)
        {
            List<int> orderIds = new List<int>();
            try
            {
                orderIds = mfOrderDao.CreateOrderFIDetails(orderVo, fiorderVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return orderIds;
        }

    }
}
