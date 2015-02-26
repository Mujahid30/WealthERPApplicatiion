using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoOfflineOrderManagement;
using VoOnlineOrderManagemnet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoOnlineOrderManagemnet;
namespace BoOfflineOrderManagement
{
    public class OfflineBondOrderBo
    {

        public DataSet GetOfflineAdviserIssuerList(int adviserId, int issueId, int type, int customerSubtype)
        {
            OfflineBondOrderDao offlineBondDao = new OfflineBondOrderDao();
            DataSet dsCommissionStructureRules = new DataSet();

            try
            {
                dsCommissionStructureRules = offlineBondDao.GetOfflineAdviserIssuerList(adviserId, issueId, type, customerSubtype);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetOfflineAdviserIssuerList(adviserId, issueId, type, custmerId,customerSubtype)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsCommissionStructureRules;
        }
        public DataSet GetOfflineLiveBondTransaction(int SeriesId, int customerSubType)
        {
            OfflineBondOrderDao offlineBondDao = new OfflineBondOrderDao();
            DataSet dsCommissionStructureRules = new DataSet();
            try
            {
                dsCommissionStructureRules = offlineBondDao.GetOfflineLiveBondTransaction(SeriesId, customerSubType);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetOfflineLiveBondTransaction(int SeriesId, int customerId, int customerSubType)");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsCommissionStructureRules;
        }
        public void CreateOfflineCustomerOrderAssociation(DataTable OrderAssociates, int userId, int orderId)
        {
            OfflineBondOrderDao OfflineBondOrderDao = new OfflineBondOrderDao();
            try
            {
                OfflineBondOrderDao.CreateOfflineCustomerOrderAssociation(OrderAssociates, userId, orderId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CreateOfflineCustomerOrderAssociation(OrderAssociates, userId, orderId);");
                object[] objects = new object[1];
                objects[0] = OrderAssociates;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public IDictionary<string, string> OfflineBOndtransact(DataTable OnlineBondOrder, int adviserId, OnlineBondOrderVo OnlineBondVo, int agentId, string agentCode, int userId)
        {
            IDictionary<string, string> OrderIds = new Dictionary<string, string>();
            OfflineBondOrderDao offlineBondDao = new OfflineBondOrderDao();
            //bool result = false;
            //int orderIds = 0; 
            try
            {
                OrderIds = offlineBondDao.CreateOfflineBondTransact(OnlineBondOrder, adviserId, OnlineBondVo, agentId, agentCode, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:onlineBOndtransact(OnlineBondOrderVo OnlineBondOrder)");
                object[] objects = new object[1];
                objects[0] = OnlineBondOrder;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return OrderIds;
        }
        public DataTable GetFDIddueList(string Category)
        {
            DataTable dt;
            OfflineBondOrderDao offlineBondDao = new OfflineBondOrderDao();
            try
            {
                dt = offlineBondDao.GetFDIddueList(Category);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
        public DataTable GetFD54IssueOrder(int adviserId, DateTime fromDate, DateTime toDate, string status, int issueId, string userType, string AgentCode, string category, int AuthenticateStatus, int orderNo)
        {
            DataTable dt;
            OfflineBondOrderDao offlineBondDao = new OfflineBondOrderDao();
            try
            {
                dt = offlineBondDao.GetFD54IssueOrder(adviserId, fromDate, toDate, status, issueId, userType, AgentCode, category, AuthenticateStatus, orderNo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
        public bool CancelBondsFDBookOrder(int orderId, string remarks, int userId, Boolean IsAuthenticated, string brokerCode)
        {
            bool bResult = false;
            OfflineBondOrderDao offlineBondDao = new OfflineBondOrderDao();

            try
            {
                bResult = offlineBondDao.CancelBondsFDBookOrder(orderId, remarks, userId, IsAuthenticated, brokerCode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:cancelBondsBookOrder(string id)");
                object[] objects = new object[1];
                objects[0] = orderId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }
        public void GetCustomerCat(int issueId, int adviserId,int customerSubType, double amt, ref string catName, ref int issueDetId, ref int categoryId, ref string Description)
        {
            // bool result = false;
            OfflineBondOrderDao offlineBondDao = new OfflineBondOrderDao();

            try
            {
                offlineBondDao.GetCustomerCat(issueId, adviserId,customerSubType, amt, ref catName, ref issueDetId, ref categoryId, ref Description);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }

    }
}
