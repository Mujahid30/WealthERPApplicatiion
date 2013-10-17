using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace BoOnlineOrderManagement
{
    public class OnlineBondOrderBo
    {
        OnlineBondOrderDao onlineBondDao = new OnlineBondOrderDao();
        DataSet dsCommissionStructureRules;


        public DataSet GetLookupDataForReceivableSetUP(int adviserId, string structureId)
        {
            //CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = onlineBondDao.GetLookupDataForReceivableSetUP(adviserId, structureId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetLookupDataForReceivableSetUP(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }

        public DataSet GetAdviserCommissionStructureRules(int adviserId, string structureId)
        {

            try
            {
                dsCommissionStructureRules = onlineBondDao.GetAdviserCommissionStructureRules(adviserId, structureId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetAdviserCommissionStructureRules(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsCommissionStructureRules;
        }

        public bool onlineBOndtransact(DataTable OnlineBondOrder)
        {
            bool result = false;
            try
            {
                result = onlineBondDao.UpdateOnlineBondTransact(OnlineBondOrder);

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
            return result;
        }

        public DataSet getBondsBookview(int input, string CustId)
        {
            OnlineBondOrderDao OnlineBondDao = new OnlineBondOrderDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = OnlineBondDao.GetOrderBondsBook(input, CustId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:getBondsBookview(int input)");
                object[] objects = new object[1];
                objects[0] = input;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }

        public void cancelBondsBookOrder(string id)
        {
            OnlineBondOrderDao OnlineBondDao = new OnlineBondOrderDao();
            try
            {
                OnlineBondDao.CancelBondsBookOrder(id);

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
                objects[0] = id;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }


        public string GetMAXTransactNO()
        {
            OnlineBondOrderDao OnlineBondDao = new OnlineBondOrderDao();
           
            string maxDB=string.Empty;
            try
            {
                maxDB = OnlineBondDao.GetMAXTransactNO();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:getBondsBookview(int input)");
                object[] objects = new object[1];
               
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return maxDB;
        }

    }
}
