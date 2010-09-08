using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Sql;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
//using System.Web.ho


using VoAdvisorProfiling;

namespace DaoAdvisorProfiling
{
    public class AdvisorLOBDao
    {
       
        public bool CreateAdvisorLOB(AdvisorLOBVo advisorLOBVo, int advisorId,int userId) 
        {
            bool result = false;
            Database db;
            DbCommand createAdvisorLOBCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createAdvisorLOBCmd = db.GetStoredProcCommand("SP_CreateAdviserLOB");
                db.AddInParameter(createAdvisorLOBCmd, "@A_AdviserId", DbType.Int32, advisorId);
                db.AddInParameter(createAdvisorLOBCmd, "@AL_OrgName", DbType.String, advisorLOBVo.OrganizationName);
                db.AddInParameter(createAdvisorLOBCmd, "@AL_Identifier", DbType.String, advisorLOBVo.Identifier);
                db.AddInParameter(createAdvisorLOBCmd, "@XALC_LOBClassificationCode", DbType.String, advisorLOBVo.LOBClassificationCode);
                db.AddInParameter(createAdvisorLOBCmd, "@XALIT_IdentifierTypeCode", DbType.String, advisorLOBVo.IdentifierTypeCode);
                db.AddInParameter(createAdvisorLOBCmd, "@AL_LicenseNo", DbType.String, advisorLOBVo.LicenseNumber);
                if (advisorLOBVo.BrokerCode == "")
                {
                    db.AddInParameter(createAdvisorLOBCmd, "@BrokerCode", DbType.String, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(createAdvisorLOBCmd, "@BrokerCode", DbType.String, advisorLOBVo.BrokerCode);
                }
                if(advisorLOBVo.ValidityDate != DateTime.MinValue)
                    db.AddInParameter(createAdvisorLOBCmd, "@AL_Validity", DbType.DateTime, advisorLOBVo.ValidityDate);
                else
                    db.AddInParameter(createAdvisorLOBCmd, "@AL_Validity", DbType.DateTime, DBNull.Value);
                db.AddInParameter(createAdvisorLOBCmd, "@AL_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(createAdvisorLOBCmd, "@AL_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createAdvisorLOBCmd, "@XALAT_AgentTypeCode", DbType.String, advisorLOBVo.AgentType);
                db.AddInParameter(createAdvisorLOBCmd, "@AL_AgentNo", DbType.String, advisorLOBVo.AgentNum);
                db.AddInParameter(createAdvisorLOBCmd, "@AL_TargetAccounts", DbType.Double,advisorLOBVo.TargetAccount);
                db.AddInParameter(createAdvisorLOBCmd, "@AL_TargetAmount", DbType.Double,advisorLOBVo.TargetAmount);
                db.AddInParameter(createAdvisorLOBCmd, "@AL_TargetPremiumAmount", DbType.Double, advisorLOBVo.TargetPremiumAmount);
               if( db.ExecuteNonQuery(createAdvisorLOBCmd)!=0)
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

                FunctionInfo.Add("Method", "AdvisorLOBDao.cs:CreateAdvisorLOB()");


                object[] objects = new object[3];
                objects[0] = advisorLOBVo;
                objects[1] = advisorId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public bool UpdateLOB(AdvisorLOBVo advisorLOBVo,int adviserId,int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateLOBCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");                
                updateLOBCmd = db.GetStoredProcCommand("SP_UpdateLOB");                
                db.AddInParameter(updateLOBCmd, "@AL_LOBId", DbType.Int32, advisorLOBVo.LOBId);
                db.AddInParameter(updateLOBCmd,"@A_AdviserId",DbType.Int32,advisorLOBVo.AdviserId);
                db.AddInParameter(updateLOBCmd, "@AL_OrgName", DbType.String, advisorLOBVo.OrganizationName);
                db.AddInParameter(updateLOBCmd, "@AL_Identifier", DbType.String, advisorLOBVo.Identifier);
                db.AddInParameter(updateLOBCmd, "@XALC_LOBClassificationCode", DbType.String, advisorLOBVo.LOBClassificationCode);
                if(advisorLOBVo.IdentifierTypeCode=="")
                    db.AddInParameter(updateLOBCmd, "@XALIT_IdentifierTypeCode", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(updateLOBCmd, "@XALIT_IdentifierTypeCode", DbType.String, advisorLOBVo.IdentifierTypeCode);
                db.AddInParameter(updateLOBCmd, "@AL_LicenseNo", DbType.String, advisorLOBVo.LicenseNumber);
                if (advisorLOBVo.BrokerCode == "")
                {
                    db.AddInParameter(updateLOBCmd, "@XB_BrokerCode", DbType.String, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(updateLOBCmd, "@XB_BrokerCode", DbType.String, advisorLOBVo.BrokerCode);
                }
                if (advisorLOBVo.ValidityDate != DateTime.MinValue)
                    db.AddInParameter(updateLOBCmd, "@AL_Validity", DbType.DateTime, advisorLOBVo.ValidityDate);
                else
                    db.AddInParameter(updateLOBCmd, "@AL_Validity", DbType.DateTime, DBNull.Value);
                db.AddInParameter(updateLOBCmd, "@AL_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(updateLOBCmd, "@AL_ModifiedOn", DbType.DateTime, DateTime.Today);
                db.AddInParameter(updateLOBCmd, "@XALAT_AgentTypeCode", DbType.String, advisorLOBVo.AgentType);
                db.AddInParameter(updateLOBCmd, "@AL_AgentNo", DbType.String, advisorLOBVo.AgentNum);
                db.AddInParameter(updateLOBCmd, "@AL_TargetAccounts", DbType.Double, advisorLOBVo.TargetAccount);
                db.AddInParameter(updateLOBCmd, "@AL_TargetAmount", DbType.Double, advisorLOBVo.TargetAmount);
                db.AddInParameter(updateLOBCmd, "@AL_TargetPremiumAmount", DbType.Double, advisorLOBVo.TargetPremiumAmount);

                
                
                
               if( db.ExecuteNonQuery(updateLOBCmd)!=0)

                bResult = true;
                 
	
	
	
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorLOBDao.cs:UpdateLOB()");


                object[] objects = new object[1];
                objects[0] = advisorLOBVo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool DeleteLOB(int lobId)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteLOBCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteLOBCmd = db.GetStoredProcCommand("SP_DeleteLOB");
                db.AddInParameter(deleteLOBCmd, "@AL_LOBId", DbType.Int32, lobId);
               if( db.ExecuteNonQuery(deleteLOBCmd)!=0)
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorLOBDao.cs:DeleteLOB()");
                object[] objects = new object[1];
                objects[0] = lobId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public AdvisorLOBVo GetLOB(int LOBId)
        {

            AdvisorLOBVo advisorLOBVo = null;


            Database db;
            DbCommand getAdvisorLOBCmd;
            DataSet getAdvisorLOBDs;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdvisorLOBCmd = db.GetStoredProcCommand("SP_GetAdviserSingleLOB");
                db.AddInParameter(getAdvisorLOBCmd, "@AL_LOBId", DbType.Int32, LOBId);

                getAdvisorLOBDs = db.ExecuteDataSet(getAdvisorLOBCmd);
                foreach (DataRow dr in getAdvisorLOBDs.Tables[0].Rows)
                {
                    advisorLOBVo = new AdvisorLOBVo();
                    advisorLOBVo.LOBId = int.Parse(dr["AL_LOBId"].ToString());
                    advisorLOBVo.OrganizationName = dr["AL_OrgName"].ToString();
                    advisorLOBVo.LicenseNumber = dr["AL_LicenseNo"].ToString();
                    advisorLOBVo.AdviserId = Int32.Parse(dr["A_AdviserId"].ToString());
                    if (dr["AL_Validity"].ToString() != string.Empty && dr["AL_Validity"].ToString() != null)
                        advisorLOBVo.ValidityDate = DateTime.Parse(dr["AL_Validity"].ToString());
                    advisorLOBVo.LOBClassificationCode = dr["XALC_LOBClassificationCode"].ToString();
                    advisorLOBVo.IdentifierTypeCode = dr["XALIT_IdentifierTypeCode"].ToString();
                    advisorLOBVo.Identifier = dr["AL_Identifier"].ToString();
                    advisorLOBVo.BrokerCode = dr["XB_BrokerCode"].ToString();
                    advisorLOBVo.AgentType = dr["XALAT_AgentTypeCode"].ToString();
                    advisorLOBVo.AgentNum = dr["AL_AgentNo"].ToString();
                    if(dr["AL_TargetAccounts"].ToString()!=string.Empty)
                        advisorLOBVo.TargetAccount = float.Parse(dr["AL_TargetAccounts"].ToString());
                    if (dr["AL_TargetAmount"].ToString() != string.Empty)
                        advisorLOBVo.TargetAmount = double.Parse(dr["AL_TargetAmount"].ToString());
                    if (dr["AL_TargetPremiumAmount"].ToString() != string.Empty)
                        advisorLOBVo.TargetPremiumAmount = double.Parse(dr["AL_TargetPremiumAmount"].ToString());

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

                FunctionInfo.Add("Method", "AdvisorLOBDao.cs:GetLOB()");


                object[] objects = new object[1];
                objects[0] = LOBId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return advisorLOBVo;
        }

        public DataSet GetAdvisorLOBs(int advisorId, string NameFilter, string BusinessTypeFilter)
        {
            List<AdvisorLOBVo> advisorLOBList = new List<AdvisorLOBVo>();
            AdvisorLOBVo advisorLOBVo;
            Database db;
            DbCommand getAdvisorLOBCmd;
            DataSet getAdvisorLOBDs;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdvisorLOBCmd = db.GetStoredProcCommand("SP_GetAdviserLOB");
                db.AddInParameter(getAdvisorLOBCmd, "@A_AdviserId", DbType.Int32, advisorId);

                getAdvisorLOBDs = db.ExecuteDataSet(getAdvisorLOBCmd);

                foreach (DataRow dr in getAdvisorLOBDs.Tables[0].Rows)
                {
                    advisorLOBVo = new AdvisorLOBVo();
                    advisorLOBVo.LOBId = int.Parse(dr["AL_LOBId"].ToString());
                    advisorLOBVo.OrganizationName = dr["AL_OrgName"].ToString();
                    advisorLOBVo.LicenseNumber = dr["AL_LicenseNo"].ToString();
                    advisorLOBVo.AdviserId = advisorId;//Int32.Parse(dr["A_AdviserId"].ToString());
                    if (dr["AL_Validity"].ToString() != string.Empty && dr["AL_Validity"].ToString() != null)
                        advisorLOBVo.ValidityDate = DateTime.Parse(dr["AL_Validity"].ToString());
                    advisorLOBVo.LOBClassificationCode = dr["XALC_LOBClassificationCode"].ToString();
                    advisorLOBVo.IdentifierTypeCode = dr["XALIT_IdentifierTypeCode"].ToString();
                    advisorLOBVo.Identifier = dr["AL_Identifier"].ToString();
                    advisorLOBVo.BrokerCode=dr["XB_BrokerCode"].ToString();
                    advisorLOBVo.AgentType=dr["XALAT_AgentTypeCode"].ToString();
                    advisorLOBVo.AgentNum=dr["AL_AgentNo"].ToString();
                    if (dr["AL_TargetAccounts"].ToString() != string.Empty)
			            advisorLOBVo.TargetAccount=float.Parse(dr["AL_TargetAccounts"].ToString());
                    if (dr["AL_TargetAmount"].ToString() != string.Empty)
      		            advisorLOBVo.TargetAmount=double.Parse(dr["AL_TargetAmount"].ToString());
                    if (dr["AL_TargetPremiumAmount"].ToString() != string.Empty)
                        advisorLOBVo.TargetPremiumAmount=double.Parse(dr["AL_TargetPremiumAmount"].ToString());
			
                    advisorLOBList.Add(advisorLOBVo);
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

                FunctionInfo.Add("Method", "AdvisorLOBDao.cs:GetAdvisorLOBs()");


                object[] objects = new object[1];
                objects[0] = advisorId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return getAdvisorLOBDs;

        }

        public string GetLOBCode(string path,string assetClass, string category, string segment)
        {
            string LOBCode = "";


            DataSet ds;
            try
            {

                /*ds = new DataSet();
                ds.ReadXml(HostingEnvironment.ApplicationPhysicalPath + "lookup.xml");
                DataRow[] row = ds.Tables["LOBCode"].Select("AssetClass = '" + assetClass + "' and Category = '" + category + "' and Segment = '" + segment + "'");
                DataRow dr = row[0];
                LOBCode = dr["Code"].ToString();
                return LOBCode;*/
               ds = new DataSet();
                ds.ReadXml(path);
                
                DataRow[] row = ds.Tables["LOBCode"].Select("AssetClass = '" + assetClass + "' and Category = '" + category + "' and Segment = '" + segment + "'");
                

                DataRow dr = row[0];
                LOBCode = dr["Code"].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorLOBDao.cs:GetLOBCode()");


                object[] objects = new object[4];
                objects[0] = path;
                objects[1] = assetClass;
                objects[2] = category;
                objects[3] = segment;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return LOBCode;

        }

        public string GetBusinessType(string path,string LOBCode)
        {
         
            string businessType = "";
            DataSet ds ;
            DataRow[] row;
            DataRow dr;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                row = ds.Tables["LOBCode"].Select("Code='" + LOBCode.ToString() + "'");
                dr = row[0];
                businessType = dr["AssetClass"].ToString() + dr["Category"].ToString() + dr["Segment"].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorLOBDao.cs:GetBusinessType()");


                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = LOBCode;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return businessType;
        }

        public bool CheckLOBExistence(int advisorId, string classificationCode)
        {
            bool result = false;
            int res;
            Database db;
            DbCommand checkAdvisorLOBCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                checkAdvisorLOBCmd = db.GetStoredProcCommand("SP_CheckLOBExistence");
                db.AddInParameter(checkAdvisorLOBCmd, "@A_AdviserId", DbType.Int32, advisorId);
                db.AddInParameter(checkAdvisorLOBCmd, "@XALC_LOBClassificationCode", DbType.String, classificationCode);

                res = int.Parse(db.ExecuteScalar(checkAdvisorLOBCmd).ToString());
                if (res != 0)
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

                FunctionInfo.Add("Method", "AdvisorLOBDao.cs:CheckLOBExistence()");


                object[] objects = new object[2];
                objects[0] = classificationCode;
                objects[1] = advisorId;
                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

    }
}
