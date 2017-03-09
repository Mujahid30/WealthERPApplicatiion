using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using DaoOnlineOrderManagement;

namespace BoOnlineOrderManagement
{
    public class OnlineCommonBackOfficeBo
    {
        OnlineCommonBackOfficeDao OnlineCommonBackOfficeDao = new OnlineCommonBackOfficeDao();
        public DataSet GetSourceCode()
        {
            DataSet dsGetSourceCode;

            try
            {
                dsGetSourceCode = OnlineCommonBackOfficeDao.GetSourceCode();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetLiveBondTransactionList()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSourceCode;
        }
        public DataTable GetCategorySchemeRank(string category, int adviserId)
        {

            try
            {
              return  OnlineCommonBackOfficeDao.GetCategorySchemeRank(category, adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs: GetCategorySchemeRank(string category, int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public DataTable GetSchemeForRank(int adviserId, int amcCode, string Category, Boolean IsEdit)
        {
            try
            {
                return OnlineCommonBackOfficeDao. GetSchemeForRank( adviserId,  amcCode,  Category,IsEdit);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineCommonBackOfficeDao.cs:GetSchemeForRank");
                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = amcCode;
                objects[2] = Category;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
           
        }
        public int CUDSchemeRanking(int adviserId, int amcCode, int schemePlanCode, string Category, int schemeRank, int opType, int RankId)
        {
            try
            {
                return OnlineCommonBackOfficeDao.CUDSchemeRanking(adviserId, amcCode, schemePlanCode, Category, schemeRank, opType, RankId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:CUDSchemeRanking(int adviserId, int amcCode, int schemePlanCode, string Category, int schemeRank, int opType)");
                object[] objects = new object[6];
                objects[0] = adviserId;
                objects[1] = amcCode;
                objects[2] = schemePlanCode;
                objects[3] = Category;
                objects[4] = schemeRank;
                objects[5] = opType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public DataTable GetMFSchemeRanking(int adviserId)
        {
            try
            {
               return OnlineCommonBackOfficeDao.GetMFSchemeRanking(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetMFSchemeRanking(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public DataSet GetInternalHeaderMapping(string type)
        {
            DataSet dsGetInternalHeaderMapping;

            try
            {
                dsGetInternalHeaderMapping = OnlineCommonBackOfficeDao.GetInternalHeaderMapping(type);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetInternalHeaderMapping()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetInternalHeaderMapping;
        }
        public DataTable ReadCsvFile(string FilePath)
        {
            string[] allLines = File.ReadAllLines(FilePath);

            string[] headers = allLines[0].Split('|');

            DataTable dtUploadFile = new DataTable("Upload");

            foreach (string header in headers) dtUploadFile.Columns.Add(header);

            for (int i = 1; i < allLines.Length; i++)
            {
                string[] row = allLines[i].Split(',');
                dtUploadFile.Rows.Add(row);
            }

            return dtUploadFile;
        }
    }
}
