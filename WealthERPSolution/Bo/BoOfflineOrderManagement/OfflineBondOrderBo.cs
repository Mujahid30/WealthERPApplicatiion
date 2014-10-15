﻿using System;
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

namespace BoOfflineOrderManagement
{
    public class OfflineBondOrderBo
    {
        
        public DataSet GetOfflineAdviserIssuerList(int adviserId, int issueId, int type, int custmerId, int customerSubtype)
        {
            OfflineBondOrderDao offlineBondDao = new OfflineBondOrderDao();
            DataSet dsCommissionStructureRules = new DataSet();
            
            try
            {
                dsCommissionStructureRules = offlineBondDao.GetOfflineAdviserIssuerList(adviserId, issueId, type, custmerId,customerSubtype);

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
        public DataSet GetOfflineLiveBondTransaction(int SeriesId, int customerId, int customerSubType)
        {
            OfflineBondOrderDao offlineBondDao = new OfflineBondOrderDao();
            DataSet dsCommissionStructureRules = new DataSet();
            try
            {
                dsCommissionStructureRules = offlineBondDao.GetOfflineLiveBondTransaction(SeriesId, customerId, customerSubType);

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
        public IDictionary<string, string> OfflineBOndtransact(DataTable OnlineBondOrder, int adviserId, int IssuerId, int agentId, string agentCode, int userId)
        {
            IDictionary<string, string> OrderIds = new Dictionary<string, string>();
            OfflineBondOrderDao offlineBondDao = new OfflineBondOrderDao();
            //bool result = false;
            //int orderIds = 0; 
            try
            {
                OrderIds = offlineBondDao.CreateOfflineBondTransact(OnlineBondOrder, adviserId, IssuerId, agentId, agentCode, userId);

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
    }
}