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
    public class OnlineOrderBackOfficeBo
    {
        OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao;

        public DataSet GetExtractType()
        {
            DataSet dsExtractType;
            OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                dsExtractType = OnlineOrderBackOfficeDao.GetExtractType();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetMfOrderExtract()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsExtractType;
        }

        public DataSet GetExtractTypeDataForFileCreation(DateTime orderDate,int AdviserId)
        {
            DataSet dsExtractType;
            OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                dsExtractType = OnlineOrderBackOfficeDao.GetExtractTypeDataForFileCreation(orderDate, AdviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetMfOrderExtract()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsExtractType;
        }     
    }
}
