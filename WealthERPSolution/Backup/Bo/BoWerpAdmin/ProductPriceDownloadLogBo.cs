using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Data;
using VoWerpAdmin;
using DaoWerpAdmin;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace BoWerpAdmin
{
    public class ProductPriceDownloadLogBo
    {
        //get processLog data
        public DataSet GetProcessLog(int CurrentPage, out int Count)
        {
            DataSet ds = null;
            ProductPriceDownloadLogDao productPriceDownloadLogDao = new ProductPriceDownloadLogDao();
            try
            {
                ds = productPriceDownloadLogDao.GetProcessLog(CurrentPage, out Count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("GetProcessLog", "ProductPriceDownloadLogBo.cs:GetProcessLog()");

                //object[] objects = new object[1];
                //objects[0] = value;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        //Create New Process

        public int CreateProcessLog(AdminDownloadProcessLogVo processLog)
        {
            int processId = 0;
            ProductPriceDownloadLogDao productPriceDownloadLogDao = new ProductPriceDownloadLogDao();
            try
            {
                processId = productPriceDownloadLogDao.CreateProcessLog(processLog);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("CreateProcessLog", "ProductPriceDownloadLogBo.cs:CreateProcessLog()");

                object[] objects = new object[1];
                objects[0] = processLog;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return processId;
        }

        //Update the database for stages completed

        public bool UpdateProcessLog(AdminDownloadProcessLogVo processLog)
        {
            bool IsUpdated = false;
            ProductPriceDownloadLogDao productPriceDownloadLogDao = new ProductPriceDownloadLogDao();
            try
            {
                IsUpdated = productPriceDownloadLogDao.UpdateProcessLog(processLog);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("CreateProcessLog", "ProductPriceDownloadLogBo.cs:CreateProcessLog()");

                object[] objects = new object[1];
                objects[0] = processLog;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsUpdated;
        }
    }
}
