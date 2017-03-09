using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VoWerpAdmin;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoWerpAdmin
{
    public class UploadDao
    {

        public DataSet GetDownloadsByDate(DateTime dt, UploadType uploadType, AssetGroupType assetGroup)
        {
            DataSet ds = new DataSet();
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("marketdb");
                cmd = db.GetStoredProcCommand("SP_GetDownloadsByDate");
                db.AddInParameter(cmd, "@Date", DbType.DateTime, dt);
                db.AddInParameter(cmd, "@AssetGroup", DbType.String, assetGroup);
                db.AddInParameter(cmd, "@UploadType", DbType.String, uploadType);

                ds = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadDao.cs:GetDownloadsByDate()");

                object[] objects = new object[1];
                objects[0] = dt;
                objects[1] = uploadType;
                objects[2] = assetGroup;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DateTime GetPMTUploadStartDate(UploadType uploadType, AssetGroupType assetGroupType)
        {
            DateTime dtStartDate;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetPMTUploadStartTime");
                db.AddInParameter(cmd, "@UploadType", DbType.String, uploadType);
                db.AddInParameter(cmd, "@AssetGroup", DbType.String, assetGroupType);
                db.AddOutParameter(cmd, "@OutMaxDate", DbType.DateTime, 50);

                db.ExecuteReader(cmd);
                Object maxDate = db.GetParameterValue(cmd, "@OutMaxDate");
                if (maxDate != DBNull.Value)
                    dtStartDate = Convert.ToDateTime(maxDate);
                else
                    dtStartDate = DateTime.MinValue; //This should be considered as invalid date in the calling method

                return dtStartDate;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadBo.cs:GetStarDateAndEndDate()");

                object[] objects = new object[3];
                objects[0] = uploadType;
                objects[1] = assetGroupType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        public DateTime GetPMTUploadEndDate(UploadType uploadType, AssetGroupType assetGroupType)
        {
            DateTime dtLastPostDate;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("marketdb");
                cmd = db.GetStoredProcCommand("SP_GetLastDownloadedPostDate");
                db.AddInParameter(cmd, "@UploadType", DbType.String, uploadType);
                db.AddInParameter(cmd, "@AssetGroup", DbType.String, assetGroupType);
                db.AddOutParameter(cmd, "@OutMaxDate", DbType.DateTime, 50);

                db.ExecuteReader(cmd);
                Object maxDate = db.GetParameterValue(cmd, "@OutMaxDate");
                if (maxDate != DBNull.Value)
                    dtLastPostDate = Convert.ToDateTime(maxDate);
                else
                    dtLastPostDate = DateTime.MinValue; //This should be considered as invalid date in the calling method
                
                return dtLastPostDate;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadBo.cs:GetStarDateAndEndDate()");

                object[] objects = new object[3];
                objects[0] = uploadType;
                objects[1] = assetGroupType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        public bool SaveXmlValuesToTempTable(UploadType uploadType, AssetGroupType assetGroupType, DataTable dtData)
        {
            bool blResult = false;
            Database db;
            DbCommand deleteCmd;
            Dictionary<string,string> dicTbls = new Dictionary<string, string>();
            dicTbls.Add("productAMCTemp", "dbo.ProductAMCDownloadTemp");
            dicTbls.Add("productEQTemp", "dbo.ProductEQNSEDownloadTemp");

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteCmd = db.GetStoredProcCommand("SP_TruncateTempTable");
                db.AddInParameter(deleteCmd, "@UploadType", DbType.String, uploadType);
                db.AddInParameter(deleteCmd, "@AssetGroup", DbType.String, assetGroupType);

                db.ExecuteNonQuery(deleteCmd);
                if (uploadType == UploadType.Price && assetGroupType == AssetGroupType.MF)
                {
                    blResult = BulkCopy("dbo.ProductAMCDownloadTemp", dtData);
                }
                else if (uploadType == UploadType.Price && assetGroupType == AssetGroupType.Equity)
                {
                    blResult = BulkCopy("dbo.ProductEQDownloadTemp", dtData);
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
                FunctionInfo.Add("Method", "UploadDao.cs:SaveXmlValuesToTempTable()");
                object[] objects = new object[2];
                objects[0] = uploadType;
                objects[1] = assetGroupType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool UpdateTempTableWithWERPCode(UploadType uploadType, AssetGroupType assetGroupType)
        {
            Database db;
            DbCommand updateCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SP_UpdateTempTableWithWERPCode");
                db.AddInParameter(updateCmd, "@UploadType", DbType.String, uploadType);
                db.AddInParameter(updateCmd, "@AssetGroup", DbType.String, assetGroupType);

                affectedRows = db.ExecuteNonQuery(updateCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadDao.cs:UpdateTempTableWithWERPCode()");

                object[] objects = new object[2];
                objects[0] = uploadType;
                objects[1] = assetGroupType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            if (affectedRows > 0)
                return true;
            else
                return false;

        }

        public int UpdateSnapshotTable(UploadType uploadType, AssetGroupType assetGroup,int modifiedBy)
        {
            Database db;
            DbCommand updateCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SP_UpdateSnapshotTable");
                db.AddInParameter(updateCmd, "@UploadType", DbType.String, uploadType);
                db.AddInParameter(updateCmd, "@AssetGroup", DbType.String, assetGroup);
                db.AddInParameter(updateCmd, "@ModifiedBy", DbType.Int32, modifiedBy);
                db.AddOutParameter(updateCmd, "@ReturnValue", DbType.Int32,10);//(updateCmd, "@ReturnValue", DbType.Int32, ParameterDirection.ReturnValue);
                
                db.ExecuteNonQuery(updateCmd);
                affectedRows = Convert.ToInt32(db.GetParameterValue(updateCmd, "@ReturnValue"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadDao.cs:UpdateSnapshotTable()");

                object[] objects = new object[2];
               
                objects[0] = uploadType;
                objects[1] = assetGroup;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return affectedRows;

        }

        public int UpdateHistoryTable(UploadType uploadType, AssetGroupType assetGroup, int createdBy)
        {

            Database db;
            DbCommand updateCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SP_UpdateProductHistory");
                db.AddInParameter(updateCmd, "@UploadType", DbType.String, uploadType);
                db.AddInParameter(updateCmd, "@AssetGroup", DbType.String, assetGroup);
                db.AddInParameter(updateCmd, "@CreatedBy", DbType.Int32, createdBy);

                affectedRows = db.ExecuteNonQuery(updateCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadDao.cs:UpdateHistoryTable()");

                object[] objects = new object[3];
                objects[0] = uploadType;
                objects[1] = assetGroup;
                objects[2] = createdBy;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return affectedRows;

        }

        public bool BulkCopy(string destinationTable, DataTable dtData)
        {
            bool isSuccess = false;

            Database db;

            db = DatabaseFactory.CreateDatabase("wealtherp");
            //Open a connection with destination database;
            
            using (SqlConnection connection = new SqlConnection(db.ConnectionString))
            {
                connection.Open();
                //Open bulkcopy connection.
                using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connection))
                {
                    bulkcopy.DestinationTableName = destinationTable;//"dbo.ProductDownloadTemp";
                    
                    try
                    {
                        bulkcopy.WriteToServer(dtData);
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
            return isSuccess;
        }
        #region OldCommentedMethods
        //public int UploadToWERP(DateTime FromDate,DateTime ToDate,string AssetGroup, string Exchange)
        //{
        //    DataSet ds;
        //    Database db;
        //    DbCommand Cmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        Cmd = db.GetStoredProcCommand("SP_UploadPriceTOWERPNew");

        //        db.AddInParameter(Cmd, "@StartDate", DbType.DateTime, FromDate);
        //        db.AddInParameter(Cmd, "@EndDate", DbType.DateTime,ToDate);
        //        db.AddInParameter(Cmd, "@AssetGroup", DbType.String, AssetGroup);
        //        db.AddInParameter(Cmd, "@Exchange", DbType.String, Exchange);
        //        ds = db.ExecuteDataSet(Cmd);
        //        return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        //    }
        //    catch (SqlTypeException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }
        //}

        //public int RejectedRecordCount(DateTime FromDate, DateTime ToDate,int CurrentPage, string Exchange, string AssetGroup, string Flag)
        //{
        //    Database db;
        //    DbCommand cmd;
        //    DataSet ds;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        cmd = db.GetStoredProcCommand("SP_UpldPrcRejtedRcrdNew");
        //        db.AddInParameter(cmd, "@StartDate", DbType.DateTime, FromDate);
        //         db.AddInParameter(cmd, "@EndDate", DbType.DateTime,ToDate);
        //        db.AddInParameter(cmd, "@AssetGroup", DbType.String, AssetGroup);
        //        db.AddInParameter(cmd, "@Flag", DbType.String, Flag);
        //        db.AddInParameter(cmd, "@CurrentPage", DbType.Int32, CurrentPage);
        //        db.AddInParameter(cmd, "@Exchange", DbType.String, Exchange);
        //        ds = db.ExecuteDataSet(cmd);
        //        return Convert.ToInt32(ds.Tables[0].Rows[0][0]);

        //    }
        //    catch (SqlTypeException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }

        //}

        //public DataSet RejectedRecords(DateTime FromDate, DateTime ToDate, int CurrentPage,  string AssetGroup, string Flag)
        //{
        //    Database db;
        //    DbCommand cmd;
        //    DataSet ds;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        cmd = db.GetStoredProcCommand("SP_UpldPrcRejtedRcrdNew");
        //        db.AddInParameter(cmd, "@StartDate", DbType.DateTime, FromDate);
        //        db.AddInParameter(cmd, "@EndDate", DbType.DateTime, ToDate);
        //        db.AddInParameter(cmd, "@AssetGroup", DbType.String, AssetGroup);
        //        db.AddInParameter(cmd, "@Flag", DbType.String, Flag);
        //        db.AddInParameter(cmd, "@CurrentPage", DbType.Int32, CurrentPage);
        //        ds = db.ExecuteDataSet(cmd);
        //        return ds;

        //    }
        //    catch (SqlTypeException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }
        //}
        #endregion OldCommentedMethods

    }
}
