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
    public class CamsUploadsDao
    {
        public List<CamsUploadsVo> GetCamsNewCustomers(int processId)
        {
            List<CamsUploadsVo> uploadsCustomerList = new List<CamsUploadsVo>();
            CamsUploadsVo CamsUploadsVo;
            Database db;
            DbCommand getNewCustomersCmd;
            DataSet getNewCustomersDs;  

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getNewCustomersCmd = db.GetStoredProcCommand("SP_UploadGetNewCustomersCams");
                db.AddInParameter(getNewCustomersCmd, "@processId", DbType.Int32, processId);
                getNewCustomersDs = db.ExecuteDataSet(getNewCustomersCmd);
                
                foreach (DataRow dr in getNewCustomersDs.Tables[0].Rows)
                {
                    CamsUploadsVo = new CamsUploadsVo();

                    CamsUploadsVo.InvestorName = dr["CMGCXPS_INV_NAME"].ToString();
                    CamsUploadsVo.Address1 = dr["CMGCXPS_ADDRESS1"].ToString();
                    CamsUploadsVo.Address2 = dr["CMGCXPS_ADDRESS2"].ToString();
                    CamsUploadsVo.Address3 = dr["CMGCXPS_ADDRESS3"].ToString();
                    CamsUploadsVo.City = dr["CMGCXPS_CITY"].ToString();
                    CamsUploadsVo.Pincode = dr["CMGCXPS_PINCODE"].ToString();
                    CamsUploadsVo.PhoneOffice = dr["CMGCXPS_PHONE_OFF"].ToString();
                    CamsUploadsVo.PhoneResidence = dr["CMGCXPS_PHONE_RES"].ToString();
                    CamsUploadsVo.Email = dr["CMGCXPS_EMAIL"].ToString();
                    CamsUploadsVo.PANNumber = dr["CMGCXPS_PAN_NO"].ToString();
                    CamsUploadsVo.Type = dr["CustomerTypeCode"].ToString();
                    CamsUploadsVo.SubType = dr["CustomerSubTypeCode"].ToString();
                                        
                    uploadsCustomerList.Add(CamsUploadsVo);
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

                FunctionInfo.Add("Method", "CamsUploadsDao.cs:GetCamsNewCustomers()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return uploadsCustomerList;
        }

        public bool UpdateProfileStagingIsCustomerNew(int adviserId,int processId)
        {
            Database db;
            DbCommand updateStagingIsCustomerNew;

            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateStagingIsCustomerNew = db.GetStoredProcCommand("SP_UpdateCamsProfileStagingIsCustomerNew");
                db.AddInParameter(updateStagingIsCustomerNew, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(updateStagingIsCustomerNew, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(updateStagingIsCustomerNew);
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

                FunctionInfo.Add("Method", "CamsUploadsDao.cs:UpdateProfileStagingIsCustomerNew()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public bool UpdateProfileStagingIsFolioNew(int processId)
        {
            Database db;
            DbCommand updateStagingIsFolioNew;

            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateStagingIsFolioNew = db.GetStoredProcCommand("SP_UpdateCamsProfileStagingIsFolioNew");
                db.AddInParameter(updateStagingIsFolioNew, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(updateStagingIsFolioNew);
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

                FunctionInfo.Add("Method", "CamsUploadsDao.cs:UpdateProfileStagingIsFolioNew()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public DataSet GetCamsNewFolios(int processId)
        {
            DataSet uploadsFolioList = new DataSet();
            //KarvyUploadsVo KarvyUploadsVo;
            Database db;
            DbCommand getNewFoliosCmd;
            DataSet getNewFoliosDs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getNewFoliosCmd = db.GetStoredProcCommand("SP_UploadCamsGetNewFolios");
                db.AddInParameter(getNewFoliosCmd, "@processId", DbType.Int32, processId);
                getNewFoliosDs = db.ExecuteDataSet(getNewFoliosCmd);
                //dr = getNewCustomersDs.Tables[0].Rows[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadsDao.cs:GetCamsNewFolios()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getNewFoliosDs;
        }
        
        public bool createCamsNewFolios(int portfolioId, string folioNum, int userId)
        {
            bool result = false;
            Database db;
            DbCommand createNewFoliosCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createNewFoliosCmd = db.GetStoredProcCommand("SP_CreateNewFolios");
                db.AddInParameter(createNewFoliosCmd, "@portfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(createNewFoliosCmd, "@folioNum", DbType.String, folioNum);
                db.AddInParameter(createNewFoliosCmd, "@userId", DbType.Int32, userId);
                db.ExecuteNonQuery(createNewFoliosCmd);
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

                FunctionInfo.Add("Method", "CamsUploadsDao.cs:CreateCamsNewFolios()");

                object[] objects = new object[3];
                objects[0] = portfolioId;
                objects[1] = folioNum;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public bool IsFolioNew(string FolioNumber)
        {
            Database db;
            DbCommand IsFolioNewCmd;
            DataSet dsFolio;

            bool result = true;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                IsFolioNewCmd = db.GetStoredProcCommand("SP_CheckIsFolioNew");
                db.AddInParameter(IsFolioNewCmd, "@FolioNumber", DbType.String, FolioNumber);
                dsFolio = db.ExecuteDataSet(IsFolioNewCmd);
                if (Int32.Parse(dsFolio.Tables[0].Rows[0]["CNT"].ToString()) > 0)
                    result = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadsDao.cs:IsFolioNew()");

                object[] objects = new object[1];
                objects[0] = FolioNumber;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public bool IsPANNew(string PAN)
        {
            Database db;
            DbCommand IsPANNewCmd;
            DataSet dsPAN;

            bool result = true;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                IsPANNewCmd = db.GetStoredProcCommand("SP_CheckIsPANNew");
                db.AddInParameter(IsPANNewCmd, "@PAN", DbType.String, PAN);
                dsPAN = db.ExecuteDataSet(IsPANNewCmd);
                if (Int32.Parse(dsPAN.Tables[0].Rows[0]["CNT"].ToString()) > 0)
                    result = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadsDao.cs:IsPANNew()");

                object[] objects = new object[1];
                objects[0] = PAN;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        /// <summary>
        /// Function to do Data translation check for reprocess
        /// </summary>
        /// <returns>Returns true if succesful, else returns false</returns>
        public bool UploadsCAMSDataTranslationForReprocess(int processId)
        {
            Database db;
            DbCommand cmdCheckDataTrans;
            

            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCheckDataTrans = db.GetStoredProcCommand("SP_UploadsCAMSDataTranslationForReprocess");
                db.AddInParameter(cmdCheckDataTrans, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(cmdCheckDataTrans);
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

                FunctionInfo.Add("Method", "CamsUploadsDao.cs:UploadsCAMSDataTranslationForReprocess()");

                object[] objects = new object[1];
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
