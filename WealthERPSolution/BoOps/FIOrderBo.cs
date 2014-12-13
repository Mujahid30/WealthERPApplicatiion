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
        FIOrderDao fiOrderDao = new FIOrderDao();
         public void GetTenure(int seriesID,out int minTenure  , out int maxtenure)
        {
            try
            {
                fiOrderDao.GetTenure(seriesID, out minTenure, out maxtenure);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FiOrderBo.cs:GetOrderNumber()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            
        }
        //public int GetOrderNumber()
        //{
        //    int orderNumber = 0;
        //    try
        //    {
        //        orderNumber = fiOrderDao.GetOrderNumber();
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "FiOrderBo.cs:GetOrderNumber()");
        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //    return orderNumber;
        //}
        public bool UpdateFITransaction(int gvOrderId, String  gvSchemeCode, int gvaccountId, string gvTrxType, int gvPortfolioId, double gvAmount, DateTime gvOrderDate)
        {
            bool Result = false;
            try
            {
                Result = fiOrderDao.UpdateFITransactionForSynch(gvOrderId, gvSchemeCode, gvaccountId, gvTrxType, gvPortfolioId, gvAmount, out Result, gvOrderDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FIOrderBo.cs:UpdateFITransaction()");
                object[] objects = new object[7];
                objects[0] = gvOrderId;
                objects[1] = gvSchemeCode;
                objects[2] = gvaccountId;
                objects[3] = gvTrxType;
                objects[4] = gvPortfolioId;
                objects[5] = gvAmount;
                objects[6] = gvOrderDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return Result;
        }
        public DataSet GetCustomerBank(int customerId)
        {
            DataSet dsGetCustomerBank;
            try
            {
                dsGetCustomerBank = fiOrderDao.GetCustomerBank(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetCustomerBank;
        }
         public DataSet GetFIModeOfHolding()
        {
            DataSet dsGetFICategory;
            try
            {
                dsGetFICategory = fiOrderDao.GetFIModeOfHolding();
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetFICategory;

        }
        public DataSet GetFICategory()
        {
            DataSet dsGetFICategory;
            try
            {
                dsGetFICategory = fiOrderDao.GetFICategory();
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetFICategory;

        }
        public DataSet GetFIIssuer(int AdviserID, string CategoryCode)
        {
            DataSet dsGetFIIssuer;
            try
            {
                dsGetFIIssuer = fiOrderDao.GetFIIssuer(AdviserID, CategoryCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetFIIssuer;

        }

        public DataSet GetFIScheme(int AdviserID, string IssuerID)
        {
            DataSet dsGetFIScheme;
            try
            {
                dsGetFIScheme = fiOrderDao.GetFIScheme(AdviserID, IssuerID);
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
                dsGetFISeries = fiOrderDao.GetFISeries(SchemeID);
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
                dsSeriesDetailssDetails = fiOrderDao.GetFISeriesDetailssDetails(SchemeID);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsSeriesDetailssDetails;
        }
        public DataSet GetFIProof()
        {
            DataSet dsGetCustomerFIOrderDetails;
            try
            {
                dsGetCustomerFIOrderDetails = fiOrderDao.GetFIProof( );
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetCustomerFIOrderDetails;
        }
        
        public DataSet GetCustomerFIOrderDetails(int orderId)
        {
            DataSet dsGetCustomerFIOrderDetails;
            try
            {
                dsGetCustomerFIOrderDetails = fiOrderDao.GetCustomerFIOrderDetails(orderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetCustomerFIOrderDetails;
        }
        
        public DataSet GetCustomerFIOrderMIS(int AdviserId, DateTime FromDate, DateTime ToDate, string branchId, string rmId, string transactionType, string status, string orderType, string amcCode, string customerId)
        {
            DataSet dsGetFIOrderMIS;
            try
            {
                dsGetFIOrderMIS = fiOrderDao.GetCustomerFIOrderMIS(AdviserId, FromDate, ToDate, branchId, rmId, transactionType, status, orderType, amcCode, customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetFIOrderMIS;
        }
        public void CreateCustomerAssociation(int OrderId, String nomineeAssociationIds, string associateType)
        {
            try
            {

                fiOrderDao.CreateCustomerAssociation(OrderId, nomineeAssociationIds, associateType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            
        }
        //public void CreateCustomerOrderDocument(CustomerProofUploadsVO CPUVo, int OrderId)
        //{
        //    try
        //    {

        //        fiOrderDao.CreateCustomerOrderDocument(CPUVo, OrderId);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //}
        public DataTable GetOrderList(int advisorId, string rmId, string branchId, DateTime toDate, DateTime fromDate, string status, string customerId, string orderType, string usertype, int AgentId, string SubBrokerCode, string AgentCode)
        {
            DataTable dtOrder = null;
           
            try
            {
                dtOrder = fiOrderDao.GetOrderList(advisorId, rmId, branchId, toDate, fromDate, status, customerId, orderType, usertype, AgentId, SubBrokerCode, AgentCode);
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
                StrTaxStatus = fiOrderDao.GetTaxStatus(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return StrTaxStatus;



        }


        public Int64 GetFaceValue(int issueId)
        {


            Int64 Facevalue = 0;
            try
            {
                Facevalue = fiOrderDao.GetFaceValue(issueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
             

 

            return Facevalue;
        }




        public bool DeleteFIOrder(int orderId)
        {
            bool bResult = false;
            try
            {
                bResult = fiOrderDao.DeleteFIOrder (orderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return bResult;
        }

        public DataSet GetCustomerAssociates(int customerId)
        {
            DataSet dsCustomerAssociates;
            try
            {
                dsCustomerAssociates = fiOrderDao.GetCustomerAssociates(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsCustomerAssociates;
        }
        public List<int> CreateOrderFIDetails(OrderVo orderVo, FIOrderVo fiorderVo, int userId,string Mode)
        {
            List<int> orderIds = new List<int>();
            try
            {
                orderIds = fiOrderDao.CreateOrderFIDetails(orderVo, fiorderVo, userId, Mode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return orderIds;
        }

    }
}
