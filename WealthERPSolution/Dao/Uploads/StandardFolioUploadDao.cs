using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUploads;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;


namespace DaoUploads
{
    public class StandardFolioUploadDao
    {
        public List<StandardFolioUploadVo> GetNewFoliosStandard(int processId)
        {
            List<StandardFolioUploadVo> uploadsFolioList = new List<StandardFolioUploadVo>();
            StandardFolioUploadVo standardFolioUploadVo;
            Database db;
            DbCommand getNewFoliosCmd;
            DataSet getNewFoliosDs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getNewFoliosCmd = db.GetStoredProcCommand("SP_UploadGetNewFoliosStd");
                db.AddInParameter(getNewFoliosCmd, "@processId", DbType.Int32, processId);
                getNewFoliosDs = db.ExecuteDataSet(getNewFoliosCmd);

                foreach (DataRow dr in getNewFoliosDs.Tables[0].Rows)
                {
                    standardFolioUploadVo = new StandardFolioUploadVo();

                    standardFolioUploadVo.AdviserId = int.Parse(dr["A_AdviserId"].ToString());
                    standardFolioUploadVo.ProcessId = int.Parse(dr["ADUL_ProcessID"].ToString());
                    standardFolioUploadVo.AMCWerpCode = int.Parse(dr["PA_AMCCode"].ToString());
                    standardFolioUploadVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                    standardFolioUploadVo.ModeOfHoldingCode = dr["XMOH_ModeOfHoldingCode"].ToString();
                    standardFolioUploadVo.FolioNum = dr["CMFSFS_FolioNum"].ToString();
                    standardFolioUploadVo.AccountOpeningDate = DateTime.Parse(dr["CMFSFS_AccountOpeningDate"].ToString());

                    uploadsFolioList.Add(standardFolioUploadVo);
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "StandardFolioUploadDao.cs:GetNewFoliosStandard()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return uploadsFolioList;
        
        }

        public bool StdDeleteCommonStaging(int processId)
        {
            Database db;
            DbCommand deleteFolioStagingUnRejectedData;

            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteFolioStagingUnRejectedData = db.GetStoredProcCommand("SP_UploadsFolioCommonStagingDelete");
                db.AddInParameter(deleteFolioStagingUnRejectedData, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(deleteFolioStagingUnRejectedData);
                result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "StandardFolioUploadDao.cs:StdDeleteCommonStaging()");

                object[] objects = new object[0];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

    }
}
