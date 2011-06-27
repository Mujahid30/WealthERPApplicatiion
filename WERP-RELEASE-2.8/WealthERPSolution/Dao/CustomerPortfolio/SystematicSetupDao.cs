using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Sql;
using System.Data.Common;

namespace DaoCustomerPortfolio
{
    public class SystematicSetupDao
    {
        /// <summary>
        /// Function to create an Systematic Setup for a particular User    
        /// </summary>
        /// <param name="systematicSetupVo">Object that holds the Systematic for a particular customer</param>
        /// <param name="userId">UserIf of the creator</param>
        /// <returns>Returns a bool varible to tells if the process is successful or not</returns>
        public bool CreateSystematicSchemeSetup(SystematicSetupVo systematicSetupVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand createSystematicSchemeSetupCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createSystematicSchemeSetupCmd = db.GetStoredProcCommand("SP_CreateSystematicSetup");

                db.AddInParameter(createSystematicSchemeSetupCmd,"@PASP_SchemePlanCode",DbType.Int32,systematicSetupVo.SchemePlanCode);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@PASP_SchemePlanCodeSwitch",DbType.Int32,systematicSetupVo.SchemePlanCodeSwitch);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFA_AccountId",DbType.Int32,systematicSetupVo.AccountId);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@XSTT_SystematicTypeCode",DbType.String,systematicSetupVo.SystematicTypeCode);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFSS_StartDate",DbType.DateTime,systematicSetupVo.StartDate);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFSS_EndDate",DbType.DateTime,systematicSetupVo.EndDate);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFSS_SystematicDate",DbType.Int32,systematicSetupVo.SystematicDate);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFSS_Amount",DbType.Decimal,systematicSetupVo.Amount);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFSS_IsManual",DbType.Int32,systematicSetupVo.IsManual);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@XES_SourceCode",DbType.String,systematicSetupVo.SourceCode);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@XF_FrequencyCode",DbType.String,systematicSetupVo.FrequencyCode);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@XPM_PaymentModeCode",DbType.String,systematicSetupVo.PaymentModeCode);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFSS_CreatedBy",DbType.Int32,userId);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFSS_ModifiedBy",DbType.Int32,userId);

                db.ExecuteNonQuery(createSystematicSchemeSetupCmd);
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
                FunctionInfo.Add("Method", "SystematicSetupDao.cs:CreateSystematicSchemeSetup()");
                object[] objects = new object[2];
                objects[0] = systematicSetupVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        /// <summary>
        /// Function to Update the Systematic Scheme for a particular customer
        /// </summary>
        /// <param name="systematicSetupVo">Object that holds the data of the Systematic scheme that needs to b updated</param>
        /// <param name="userId">UserId of the user who does the update</param>
        /// <returns>Returns a bool varible to tells if the process is successful or not</returns>
        public bool UpdateSystematicSchemeSetup(SystematicSetupVo systematicSetupVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateSystematicSchemeSetupCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateSystematicSchemeSetupCmd = db.GetStoredProcCommand("SP_UpdateSystematicSetup");
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFSS_SystematicSetupId", DbType.Int32, systematicSetupVo.SystematicSetupId);
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@PASP_SchemePlanCode", DbType.Int32, systematicSetupVo.SchemePlanCode);
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@PASP_SchemePlanCodeSwitch", DbType.Int32, systematicSetupVo.SchemePlanCodeSwitch);
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFA_AccountId", DbType.Int32, systematicSetupVo.AccountId);
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@XSTT_SystematicTypeCode", DbType.String, systematicSetupVo.SystematicTypeCode);
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFSS_StartDate", DbType.DateTime, systematicSetupVo.StartDate);
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFSS_EndDate", DbType.DateTime, systematicSetupVo.EndDate);
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFSS_SystematicDate", DbType.Int32, systematicSetupVo.SystematicDate);
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFSS_Amount", DbType.Double, systematicSetupVo.Amount);
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFSS_IsManual", DbType.Int32, systematicSetupVo.IsManual);
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@XES_SourceCode", DbType.String, systematicSetupVo.SourceCode);
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@XF_FrequencyCode", DbType.String, systematicSetupVo.FrequencyCode);
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@XPM_PaymentModeCode", DbType.String, systematicSetupVo.PaymentModeCode);
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFSS_ModifiedBy", DbType.Int32, userId);

                db.ExecuteNonQuery(updateSystematicSchemeSetupCmd);
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
                FunctionInfo.Add("Method", "SystematicSetupDao.cs:UpdateSystematicSchemeSetup()");
                object[] objects = new object[2];
                objects[0] = systematicSetupVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        /// <summary>
        /// Function to get all the Systematic Setup Schemes for a particular Customer
        /// </summary>
        /// <param name="portfolioId">The portfolioId of a particular Customer</param>
        /// <param name="CurrentPage">Gives the current page in the grid (Parameters for paging)</param>
        /// <param name="sortOrder">Gives the sort order ASC or DSC (Parameters for paging)</param>
        /// <param name="count">Number of records per page (Parameters for paging)</param>
        /// <returns></returns>
        public List<SystematicSetupVo> GetSystematicSchemeSetupList(int portfolioId, int CurrentPage, string sortOrder, out int count)
        {
            SystematicSetupVo systematicSetupVo;
            List<SystematicSetupVo> systematicSetupList = null;
            Database db;
            DbCommand getSystematicSetupListCmd;
            DataSet dsSystematicSetups;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSystematicSetupListCmd = db.GetStoredProcCommand("SP_GetSystematicSetupSchemes");
                db.AddInParameter(getSystematicSetupListCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getSystematicSetupListCmd, "@CurrentPage", DbType.Int16, CurrentPage);
                db.AddInParameter(getSystematicSetupListCmd, "@SortOrder", DbType.String, sortOrder);

                dsSystematicSetups = db.ExecuteDataSet(getSystematicSetupListCmd);
                if (dsSystematicSetups.Tables[1] != null && dsSystematicSetups.Tables[1].Rows.Count > 0)
                    count = Int32.Parse(dsSystematicSetups.Tables[1].Rows[0][0].ToString());
                else
                    count = 0;
                if (dsSystematicSetups.Tables[0].Rows.Count > 0)
                {
                    systematicSetupList = new List<SystematicSetupVo>();

                    foreach (DataRow dr in dsSystematicSetups.Tables[0].Rows)
                    {
                        systematicSetupVo = new SystematicSetupVo();
                        systematicSetupVo.SystematicSetupId = int.Parse(dr["CMFSS_SystematicSetupId"].ToString());
                        systematicSetupVo.SchemePlan = dr["SchemeName"].ToString();
                        systematicSetupVo.SchemePlanCode = int.Parse(dr["SchemeCode"].ToString());
                        //systematicSetupVo.SchemePlanSwitch = dr["SchemeNameSwitch"].ToString();
                        systematicSetupVo.Folio = dr["CMFA_FolioNum"].ToString();
                        systematicSetupVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        systematicSetupVo.SystematicType = dr["XSTT_SystematicType"].ToString();
                        systematicSetupVo.SystematicTypeCode = dr["XSTT_SystematicTypeCode"].ToString();
                        systematicSetupVo.StartDate = DateTime.Parse(dr["CMFSS_StartDate"].ToString());
                        systematicSetupVo.EndDate = DateTime.Parse(dr["CMFSS_EndDate"].ToString());
                        systematicSetupVo.SystematicDate = int.Parse(dr["CMFSS_SystematicDate"].ToString());
                        systematicSetupVo.Amount = double.Parse(dr["CMFSS_Amount"].ToString());
                        systematicSetupVo.IsManual = int.Parse(dr["CMFSS_IsManual"].ToString());
                        systematicSetupVo.SourceName = dr["XES_SourceName"].ToString();
                        systematicSetupVo.SourceCode = dr["XES_SourceCode"].ToString();
                        systematicSetupVo.Frequency = dr["XF_Frequency"].ToString();
                        systematicSetupVo.FrequencyCode = dr["XF_FrequencyCode"].ToString();
                        //systematicSetupVo.PaymentMode = dr["XPM_PaymentMode"].ToString();

                        systematicSetupList.Add(systematicSetupVo);
                    }
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
                FunctionInfo.Add("Method", "SystematicSetupDao.cs:GetSystematicSchemeSetupList()");


                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return systematicSetupList;
        }

        /// <summary>
        /// Function to get the details of a particular Systematic Setup Scheme
        /// </summary>
        /// <param name="systematicSetupId"></param>
        /// <returns></returns>
        public SystematicSetupVo GetSystematicSchemeSetupDetails(int systematicSetupId)
        {
            SystematicSetupVo systematicSetupVo = new SystematicSetupVo();;
            Database db;
            DbCommand getSystematicSetupDetailsCmd;
            DataSet dsSystematicSetups;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSystematicSetupDetailsCmd = db.GetStoredProcCommand("SP_GetSystematicSetupDetails");
                db.AddInParameter(getSystematicSetupDetailsCmd, "@CMFSS_SystematicSetupId", DbType.Int32, systematicSetupId);


                dsSystematicSetups = db.ExecuteDataSet(getSystematicSetupDetailsCmd);
                if (dsSystematicSetups.Tables[0].Rows.Count > 0)
                {
                    
                    dr=dsSystematicSetups.Tables[0].Rows[0];

                    systematicSetupVo.SystematicSetupId = int.Parse(dr["CMFSS_SystematicSetupId"].ToString());
                    systematicSetupVo.SchemePlan = dr["PASP_SchemePlanName"].ToString();
                    systematicSetupVo.SchemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                    //systematicSetupVo.SystematicType = dr["XSTT_SystematicType"].ToString();
                    systematicSetupVo.SystematicTypeCode = dr["XSTT_SystematicTypeCode"].ToString();
                    //systematicSetupVo.Frequency = dr["XF_Frequency"].ToString();
                    systematicSetupVo.FrequencyCode = dr["XF_FrequencyCode"].ToString();
                    systematicSetupVo.Folio = dr["CMFA_FolioNum"].ToString();
                    systematicSetupVo.StartDate = DateTime.Parse(dr["CMFSS_StartDate"].ToString());
                    systematicSetupVo.EndDate = DateTime.Parse(dr["CMFSS_EndDate"].ToString());
                    systematicSetupVo.SystematicDate = int.Parse(dr["CMFSS_SystematicDate"].ToString());
                    systematicSetupVo.Amount = double.Parse(dr["CMFSS_Amount"].ToString());
                    systematicSetupVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                    systematicSetupVo.SchemePlanCodeSwitch = int.Parse(dr["SwitchSchemeCode"].ToString());
                    systematicSetupVo.SchemePlanSwitch = dr["SwitchSchemePlanName"].ToString();
                    //systematicSetupVo.IsManual = int.Parse(dr["CMFSS_IsManual"].ToString());
                    //systematicSetupVo.SourceName = dr["XES_SourceName"].ToString();
                    //systematicSetupVo.SourceCode = dr["XES_SourceCode"].ToString();
                    //systematicSetupVo.PaymentMode = dr["XPM_PaymentMode"].ToString();
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
                FunctionInfo.Add("Method", "SystematicSetupDao.cs:GetSystematicSchemeSetupDetails()");

                object[] objects = new object[1];
                objects[0] = systematicSetupId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return systematicSetupVo;
        }

    }
}
