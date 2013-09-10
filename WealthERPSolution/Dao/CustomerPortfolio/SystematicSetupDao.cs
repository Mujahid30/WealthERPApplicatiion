﻿using System;
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
                if(systematicSetupVo.SchemePlanCodeSwitch !=0)
                   db.AddInParameter(createSystematicSchemeSetupCmd,"@PASP_SchemePlanCodeSwitch",DbType.Int32,systematicSetupVo.SchemePlanCodeSwitch);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFA_AccountId",DbType.Int32,systematicSetupVo.AccountId);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@XSTT_SystematicTypeCode",DbType.String,systematicSetupVo.SystematicTypeCode);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFSS_StartDate",DbType.DateTime,systematicSetupVo.StartDate);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFSS_EndDate",DbType.DateTime,systematicSetupVo.EndDate);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFSS_SystematicDate",DbType.Int32,systematicSetupVo.SystematicDate);
                db.AddInParameter(createSystematicSchemeSetupCmd, "@CMFSS_Amount", DbType.Decimal, systematicSetupVo.Amount);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFSS_IsManual",DbType.Int32,systematicSetupVo.IsManual);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@XES_SourceCode",DbType.String,systematicSetupVo.SourceCode);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@XF_FrequencyCode",DbType.String,systematicSetupVo.FrequencyCode);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@XPM_PaymentModeCode",DbType.String,systematicSetupVo.PaymentModeCode);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFSS_CreatedBy",DbType.Int32,userId);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFSS_ModifiedBy",DbType.Int32,userId);
                db.AddInParameter(createSystematicSchemeSetupCmd,"@CMFSS_Tenure",DbType.Int32, systematicSetupVo.Period);
                db.AddInParameter(createSystematicSchemeSetupCmd, "@IsAutoTranx", DbType.Int32, systematicSetupVo.IsAutoTransaction);
                db.AddInParameter(createSystematicSchemeSetupCmd, "@TenureCycle", DbType.String, systematicSetupVo.PeriodSelection);

                if (systematicSetupVo.SipChequeDate!=DateTime.MinValue)
                    db.AddInParameter(createSystematicSchemeSetupCmd, "@CMFSS_SIPFirstChequeDate", DbType.DateTime, systematicSetupVo.SipChequeDate);    
                else
                    db.AddInParameter(createSystematicSchemeSetupCmd, "@CMFSS_SIPFirstChequeDate", DbType.DateTime, DBNull.Value);    
                if(systematicSetupVo.SipChequeNo!=0)
                    db.AddInParameter(createSystematicSchemeSetupCmd, "@CMFSS_SIPFirstChequeNo", DbType.Int64, systematicSetupVo.SipChequeNo);
                else
                    db.AddInParameter(createSystematicSchemeSetupCmd, "@CMFSS_SIPFirstChequeNo", DbType.Int64, DBNull.Value);
                if (systematicSetupVo.RegistrationDate != DateTime.MinValue)
                    db.AddInParameter(createSystematicSchemeSetupCmd, "@CMFSS_RegistrationDate", DbType.DateTime, systematicSetupVo.RegistrationDate);
                else
                    db.AddInParameter(createSystematicSchemeSetupCmd, "@CMFSS_RegistrationDate", DbType.DateTime, DBNull.Value);

                if (!string.IsNullOrEmpty(systematicSetupVo.Remarks))
                    db.AddInParameter(createSystematicSchemeSetupCmd, "@Remarks", DbType.String, systematicSetupVo.Remarks);
                else
                    db.AddInParameter(createSystematicSchemeSetupCmd, "@Remarks", DbType.String, DBNull.Value);
                if (systematicSetupVo.CeaseDate != DateTime.MinValue)
                    db.AddInParameter(createSystematicSchemeSetupCmd, "@CeaseDate", DbType.DateTime, systematicSetupVo.CeaseDate);
                else
                    db.AddInParameter(createSystematicSchemeSetupCmd, "@CeaseDate", DbType.DateTime, DBNull.Value);
                if (!string.IsNullOrEmpty(systematicSetupVo.SubBrokerCode))
                    db.AddInParameter(createSystematicSchemeSetupCmd, "@CMFSS_SubBrokerCode", DbType.String, systematicSetupVo.SubBrokerCode);
                else
                    db.AddInParameter(createSystematicSchemeSetupCmd, "@CMFSS_SubBrokerCode", DbType.String, DBNull.Value);

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
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@IsAutoTranx", DbType.Int32, systematicSetupVo.IsAutoTransaction);
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@TenureCycle", DbType.String, systematicSetupVo.PeriodSelection);
                db.AddInParameter(updateSystematicSchemeSetupCmd, "@Tenure", DbType.Int16, systematicSetupVo.Period);

                if (systematicSetupVo.SipChequeDate != DateTime.MinValue)
                    db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFSS_SIPFirstChequeDate", DbType.DateTime, systematicSetupVo.SipChequeDate);
                else
                    db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFSS_SIPFirstChequeDate", DbType.DateTime, DBNull.Value); 
                if (systematicSetupVo.SipChequeNo != 0)
                    db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFSS_SIPFirstChequeNo", DbType.Int64, systematicSetupVo.SipChequeNo);
                else
                    db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFSS_SIPFirstChequeNo", DbType.Int64, DBNull.Value);
                if (systematicSetupVo.RegistrationDate != DateTime.MinValue)
                    db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFSS_RegistrationDate", DbType.DateTime, systematicSetupVo.RegistrationDate);
                else
                    db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFSS_RegistrationDate", DbType.DateTime, DBNull.Value);

                if (!string.IsNullOrEmpty(systematicSetupVo.Remarks))
                    db.AddInParameter(updateSystematicSchemeSetupCmd, "@Remarks", DbType.String, systematicSetupVo.Remarks);
                else
                    db.AddInParameter(updateSystematicSchemeSetupCmd, "@Remarks", DbType.String, DBNull.Value);
                if (systematicSetupVo.CeaseDate != DateTime.MinValue)
                    db.AddInParameter(updateSystematicSchemeSetupCmd, "@CeaseDate", DbType.DateTime, systematicSetupVo.CeaseDate);
                else
                    db.AddInParameter(updateSystematicSchemeSetupCmd, "@CeaseDate", DbType.DateTime, DBNull.Value);
                if (!string.IsNullOrEmpty(systematicSetupVo.SubBrokerCode))
                    db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFSS_SubBrokerCode", DbType.String, systematicSetupVo.SubBrokerCode);
                else
                    db.AddInParameter(updateSystematicSchemeSetupCmd, "@CMFSS_SubBrokerCode", DbType.String, DBNull.Value);
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
        public List<SystematicSetupVo> GetSystematicSchemeSetupList(int portfolioId)
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
                //db.AddInParameter(getSystematicSetupListCmd, "@CurrentPage", DbType.Int16, CurrentPage);
                //db.AddInParameter(getSystematicSetupListCmd, "@SortOrder", DbType.String, sortOrder);

                dsSystematicSetups = db.ExecuteDataSet(getSystematicSetupListCmd);
                //if (dsSystematicSetups.Tables[1] != null && dsSystematicSetups.Tables[1].Rows.Count > 0)
                //    count = Int32.Parse(dsSystematicSetups.Tables[1].Rows[0][0].ToString());
                //else
                //    count = 0;
                if (dsSystematicSetups.Tables[0].Rows.Count > 0)
                {
                    systematicSetupList = new List<SystematicSetupVo>();

                    foreach (DataRow dr in dsSystematicSetups.Tables[0].Rows)
                    {
                        systematicSetupVo = new SystematicSetupVo();
                        systematicSetupVo.SystematicSetupId = int.Parse(dr["CMFSS_SystematicSetupId"].ToString());
                        systematicSetupVo.SchemePlan = dr["SchemeName"].ToString();
                        systematicSetupVo.SchemePlanCode = int.Parse(dr["SchemeCode"].ToString());
                        if (!string.IsNullOrEmpty(dr["SchemeNameSwitch"].ToString()))
                            systematicSetupVo.SchemePlanSwitch = dr["SchemeNameSwitch"].ToString();
                        else
                            systematicSetupVo.SchemePlanSwitch = "";
                        if (!string.IsNullOrEmpty(dr["SchemePlanSwitch"].ToString()))
                            systematicSetupVo.SchemePlanCodeSwitch = int.Parse(dr["SchemePlanSwitch"].ToString());
                        else
                            systematicSetupVo.SchemePlanCodeSwitch = 0;
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

                        if (!string.IsNullOrEmpty(dr["CMFSS_SIPFirstChequeDate"].ToString()))
                            systematicSetupVo.SipChequeDate = DateTime.Parse(dr["CMFSS_SIPFirstChequeDate"].ToString());
                        else
                            systematicSetupVo.SipChequeDate = DateTime.MinValue;
                        if (!string.IsNullOrEmpty(dr["CMFSS_SIPFirstChequeNo"].ToString()))
                            systematicSetupVo.SipChequeNo = Int64.Parse(dr["CMFSS_SIPFirstChequeNo"].ToString());
                        else
                            systematicSetupVo.SipChequeNo = 0;
                        if(!string.IsNullOrEmpty(dr["CMFSS_RegistrationDate"].ToString()))

                           systematicSetupVo.RegistrationDate = DateTime.Parse(dr["CMFSS_RegistrationDate"].ToString());
                        else
                            systematicSetupVo.RegistrationDate = DateTime.MinValue;
                        systematicSetupVo.PaymentModeCode = dr["XPM_PaymentModeCode"].ToString();
                        if(dr["CMFSS_Tenure"].ToString() != "")
                            systematicSetupVo.Period = int.Parse(dr["CMFSS_Tenure"].ToString());
                        systematicSetupVo.PeriodSelection = dr["CMFSS_TenureCycle"].ToString();

                        if (!string.IsNullOrEmpty(dr["CMFSS_REMARKS"].ToString()))
                            systematicSetupVo.Remarks = dr["CMFSS_REMARKS"].ToString();
                        else
                            systematicSetupVo.Remarks = "";
                        if (!string.IsNullOrEmpty(dr["CMFSS_CEASEDATE"].ToString()))

                            systematicSetupVo.CeaseDate = DateTime.Parse(dr["CMFSS_CEASEDATE"].ToString());
                        else
                            systematicSetupVo.CeaseDate = DateTime.MinValue;
                        if (!string.IsNullOrEmpty(dr["CMFSS_SubBrokerCode"].ToString()))
                            systematicSetupVo.SubBrokerCode = dr["CMFSS_SubBrokerCode"].ToString();
                        else
                            systematicSetupVo.SubBrokerCode = "";

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
                db.AddOutParameter(getSystematicSetupDetailsCmd, "@PortfolioId", DbType.Int32, 9999999);
                dsSystematicSetups = db.ExecuteDataSet(getSystematicSetupDetailsCmd);

                Object objportfolioId = db.GetParameterValue(getSystematicSetupDetailsCmd, "@PortfolioId");
                if (objportfolioId != DBNull.Value)
                    systematicSetupVo.PortfolioId = int.Parse(db.GetParameterValue(getSystematicSetupDetailsCmd, "@PortfolioId").ToString());
                else
                    systematicSetupVo.PortfolioId = 0;
                
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
                    if (!string.IsNullOrEmpty(dr["CMFSS_SystematicDate"].ToString()))
                    systematicSetupVo.SystematicDate = int.Parse(dr["CMFSS_SystematicDate"].ToString());
                    systematicSetupVo.Amount = double.Parse(dr["CMFSS_Amount"].ToString());
                    systematicSetupVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                    systematicSetupVo.SchemePlanCodeSwitch = int.Parse(dr["SwitchSchemeCode"].ToString());
                    systematicSetupVo.SchemePlanSwitch = dr["SwitchSchemePlanName"].ToString();
                    if (!string.IsNullOrEmpty(dr["CMFSS_SIPFirstChequeDate"].ToString()))
                        systematicSetupVo.SipChequeDate = DateTime.Parse(dr["CMFSS_SIPFirstChequeDate"].ToString());
                    else
                        systematicSetupVo.SipChequeDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(dr["CMFSS_SIPFirstChequeNo"].ToString()))
                        systematicSetupVo.SipChequeNo = Int64.Parse(dr["CMFSS_SIPFirstChequeNo"].ToString());
                    else
                        systematicSetupVo.SipChequeNo = 0;
                    if (!string.IsNullOrEmpty(dr["CMFSS_RegistrationDate"].ToString()))

                        systematicSetupVo.RegistrationDate = DateTime.Parse(dr["CMFSS_RegistrationDate"].ToString());
                    else
                        systematicSetupVo.RegistrationDate = DateTime.MinValue;

                    if (!string.IsNullOrEmpty(dr["CMFSS_Tenure"].ToString()))
                        systematicSetupVo.Period = int.Parse(dr["CMFSS_Tenure"].ToString());

                    if (!string.IsNullOrEmpty(dr["XPM_PaymentModeCode"].ToString()))
                        systematicSetupVo.PaymentModeCode = dr["XPM_PaymentModeCode"].ToString();
                    systematicSetupVo.PeriodSelection = dr["CMFSS_TenureCycle"].ToString();
                    if (!string.IsNullOrEmpty(dr["CMFSS_IsAutoTransaction"].ToString()))
                        systematicSetupVo.IsAutoTransaction = int.Parse(dr["CMFSS_IsAutoTransaction"].ToString());

                    if (!string.IsNullOrEmpty(dr["CMFSS_REMARKS"].ToString()))
                        systematicSetupVo.Remarks = dr["CMFSS_REMARKS"].ToString();
                    else
                        systematicSetupVo.Remarks = "";
                    if (!string.IsNullOrEmpty(dr["CMFSS_CEASEDATE"].ToString()))
                        systematicSetupVo.CeaseDate = DateTime.Parse(dr["CMFSS_CEASEDATE"].ToString());
                    else
                        systematicSetupVo.CeaseDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(dr["CMFSS_SubBrokerCode"].ToString()))
                        systematicSetupVo.SubBrokerCode = dr["CMFSS_SubBrokerCode"].ToString();
                    else
                        systematicSetupVo.SubBrokerCode = "";
                    

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
        public DataSet GetAllDropdownBinding(string strAmcCode)
        {
            Database db;

            DbCommand getAllDropdownBindingCmd;
            DataSet dsGetAllDropdownBinding;            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAllDropdownBindingCmd = db.GetStoredProcCommand("SP_GetAllDropDownBinding");
                db.AddInParameter(getAllDropdownBindingCmd, "@AMCCode", DbType.String, strAmcCode);
                dsGetAllDropdownBinding = db.ExecuteDataSet(getAllDropdownBindingCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupDao.cs:GetAllDropdownBinding()");
                object[] objects = new object[3];
                //objects[0] = systematicSetupId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAllDropdownBinding;
        }
        public DataSet GetSystematicMIS()
        {
            Database db;
            DbCommand getSystematicMISCmd;
            DataSet dsBindGvSystematicMIS;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSystematicMISCmd = db.GetStoredProcCommand("SP_GetSystematicMIS");
                dsBindGvSystematicMIS = db.ExecuteDataSet(getSystematicMISCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupDao.cs:GetSystematicMIS()");
                object[] objects = new object[3];
                //objects[0] = systematicSetupId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsBindGvSystematicMIS;

        }
        public DataSet GetCalenderDetailView()
        {
            Database db;
            DbCommand getCalenderDetailViewCmd;
            DataSet dsBindgvCalenderDetailView;
            try
            { 
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCalenderDetailViewCmd = db.GetStoredProcCommand("SP_GetCalenderDetailViewMIS");
                dsBindgvCalenderDetailView = db.ExecuteDataSet(getCalenderDetailViewCmd);
            }
            catch(BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch(Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupDao.cs:GetCalenderDetailView()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsBindgvCalenderDetailView;
        }

        /// <summary>
        /// To Get Systematic MIS Data  <<Kirteeshree>>
        /// </summary>
        /// <param name="UserType"></param>
        /// <param name="AdviserId"></param>
        /// <param name="RmId"></param>
        /// <param name="CustomerId"></param>
        /// <param name="BranchHeadId"></param>
        /// <param name="BranchId"></param>
        /// <param name="All"></param>
        /// <param name="Category"></param>
        /// <param name="SysType"></param>
        /// <param name="AmcCode"></param>
        /// <param name="SchemePlanCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <returns></returns>
        public DataSet GetAllSystematicMISData(string UserType, int AdviserId, int RmId, int CustomerId, int BranchHeadId, int BranchId, int All, string Category, string SysType, string AmcCode, string SchemePlanCode, string StartDate, string EndDate, DateTime dtFrom, DateTime dtTo, int isIndividualOrGroup, int Manage, string AgentCode)
        {
            DataSet dsGetSystematicMIS = new DataSet();
            Database db;
            DbCommand getSystemaicMISCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSystemaicMISCmd = db.GetStoredProcCommand("SP_GetSystematicMIS");
                db.AddInParameter(getSystemaicMISCmd, "@UserType", DbType.String, UserType);
                db.AddInParameter(getSystemaicMISCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(getSystemaicMISCmd, "@RMId", DbType.Int32, RmId);
                db.AddInParameter(getSystemaicMISCmd, "@CustomerId", DbType.Int32, CustomerId);
                db.AddInParameter(getSystemaicMISCmd, "@branchHeadId", DbType.Int32, BranchHeadId);
                db.AddInParameter(getSystemaicMISCmd, "@BranchId", DbType.Int32, BranchId);
                db.AddInParameter(getSystemaicMISCmd, "@all", DbType.Int32, All);
                db.AddInParameter(getSystemaicMISCmd, "@Manage", DbType.Int32, Manage);
                if (!string.IsNullOrEmpty(Category))
                    db.AddInParameter(getSystemaicMISCmd, "@Category", DbType.String, Category);
                if (!string.IsNullOrEmpty(SysType))
                    db.AddInParameter(getSystemaicMISCmd, "@SystematicType", DbType.String, SysType);
                if (!string.IsNullOrEmpty(AmcCode))
                    db.AddInParameter(getSystemaicMISCmd, "@AMCCode", DbType.String, AmcCode);
                if (!string.IsNullOrEmpty(SchemePlanCode))
                    db.AddInParameter(getSystemaicMISCmd, "@SchemePlanCode", DbType.String, SchemePlanCode);
                if (!string.IsNullOrEmpty(StartDate))
                    db.AddInParameter(getSystemaicMISCmd, "@Startdate", DbType.String, StartDate);
                if (!string.IsNullOrEmpty(EndDate))
                    db.AddInParameter(getSystemaicMISCmd, "@Enddate", DbType.String, EndDate);
                if (dtFrom != DateTime.MinValue)
                    db.AddInParameter(getSystemaicMISCmd, "@dtFrom", DbType.DateTime, dtFrom);
                else
                    dtFrom = DateTime.MinValue;
                if (dtTo != DateTime.MinValue)
                    db.AddInParameter(getSystemaicMISCmd, "@dtTo", DbType.DateTime, dtTo);
                else
                    dtTo = DateTime.MinValue;

                if(isIndividualOrGroup != 0)
                    db.AddInParameter(getSystemaicMISCmd, "@isIndividualOrGroup", DbType.Int16, isIndividualOrGroup);
                if (!string.IsNullOrEmpty(AgentCode))
                    db.AddInParameter(getSystemaicMISCmd, "@agentcode", DbType.String, SysType);
                else
                    db.AddInParameter(getSystemaicMISCmd, "@agentcode", DbType.String, DBNull.Value);

                dsGetSystematicMIS = db.ExecuteDataSet(getSystemaicMISCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupDao.cs:GetAllSystematicMISData()");
                object[] objects = new object[16];
                objects[0] = UserType;
                objects[1] = AdviserId;
                objects[2] = RmId;
                objects[3] = CustomerId;
                objects[4] = BranchHeadId;
                objects[5] = BranchId;
                objects[6] = All;
                objects[7] = Category;
                objects[8] = SysType;
                objects[9] = AmcCode;
                objects[10] = SchemePlanCode;
                objects[11] = StartDate;
                objects[12] = EndDate;
                objects[13] = dtFrom;
                objects[14] = dtTo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetSystematicMIS;
        }
        public DataSet GetCalenderSummaryView(string UserType, int AdviserId, int RmId, int CustomerId, int BranchHeadId, int BranchId, int All, string Category, string SysType, string AmcCode, string SchemePlanCode, DateTime dtFrom, DateTime dtTo, int isIndividualOrGroup, string StartDate, string EndDate,int Manage)
        {
            DataSet dsGetcalenderSummaryView = new DataSet();
            Database db;
            DbCommand getCalenderSummaryViewCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCalenderSummaryViewCmd = db.GetStoredProcCommand("SP_GetCalenderDetailView");
                db.AddInParameter(getCalenderSummaryViewCmd, "@UserType", DbType.String, UserType);
                db.AddInParameter(getCalenderSummaryViewCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(getCalenderSummaryViewCmd, "@RMId", DbType.Int32, RmId);
                db.AddInParameter(getCalenderSummaryViewCmd, "@CustomerId", DbType.Int32, CustomerId);
                db.AddInParameter(getCalenderSummaryViewCmd, "@branchHeadId", DbType.Int32, BranchHeadId);
                db.AddInParameter(getCalenderSummaryViewCmd, "@BranchId", DbType.Int32, BranchId);
                db.AddInParameter(getCalenderSummaryViewCmd, "@all", DbType.Int32, All);
                db.AddInParameter(getCalenderSummaryViewCmd, "@Manage", DbType.Int32, Manage);
                if (!string.IsNullOrEmpty(Category))
                    db.AddInParameter(getCalenderSummaryViewCmd, "@Category", DbType.String, Category);
                if (!string.IsNullOrEmpty(SysType))
                    db.AddInParameter(getCalenderSummaryViewCmd, "@SystematicType", DbType.String, SysType);
                if (!string.IsNullOrEmpty(AmcCode))
                    db.AddInParameter(getCalenderSummaryViewCmd, "@AMCCode", DbType.String, AmcCode);
                if (!string.IsNullOrEmpty(SchemePlanCode))
                    db.AddInParameter(getCalenderSummaryViewCmd, "@SchemePlanCode", DbType.String, SchemePlanCode);
                if (dtFrom != DateTime.MinValue)
                    db.AddInParameter(getCalenderSummaryViewCmd, "@dtFrom", DbType.DateTime, dtFrom);
                else
                    dtFrom = DateTime.MinValue;
                if (dtTo != DateTime.MinValue)
                    db.AddInParameter(getCalenderSummaryViewCmd, "@dtTo", DbType.DateTime, dtTo);
                else
                    dtTo = DateTime.MinValue;

                if (isIndividualOrGroup != 0)
                    db.AddInParameter(getCalenderSummaryViewCmd, "@isIndividualOrGroup", DbType.Int16, isIndividualOrGroup);
                if (!string.IsNullOrEmpty(StartDate))
                    db.AddInParameter(getCalenderSummaryViewCmd, "@Startdate", DbType.String, StartDate);
                if (!string.IsNullOrEmpty(EndDate))
                    db.AddInParameter(getCalenderSummaryViewCmd, "@Enddate", DbType.String, EndDate);

                dsGetcalenderSummaryView = db.ExecuteDataSet(getCalenderSummaryViewCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupBo.cs:GetCalenderSummaryView()");
                object[] objects = new object[13];
                objects[0] = UserType;
                objects[1] = AdviserId;
                objects[2] = RmId;
                objects[3] = CustomerId;
                objects[4] = BranchHeadId;
                objects[5] = BranchId;
                objects[6] = All;
                objects[7] = Category;
                objects[8] = SysType;
                objects[9] = AmcCode;
                objects[10] = SchemePlanCode;
                objects[11] = dtFrom;
                objects[12] = dtTo;
                objects[13] = isIndividualOrGroup;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetcalenderSummaryView;
        }


        public int GetAccountIdAccordingToFolio(string folioNo,int portfolioId)
        {
            int accountId = 0;
            Database db;
            DbCommand getAccountIdcmd;
            DataSet dsgetAccountId;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAccountIdcmd = db.GetStoredProcCommand("SP_GetAccountIdAccodingToFolio");
                db.AddInParameter(getAccountIdcmd, "@folioNo", DbType.String, folioNo);
                db.AddInParameter(getAccountIdcmd, "@portfolioId", DbType.Int32, portfolioId);
                dsgetAccountId = db.ExecuteDataSet(getAccountIdcmd);
                accountId = int.Parse(dsgetAccountId.Tables[0].Rows[0]["CMFA_AccountId"].ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return accountId;
        }

        public int GetPortFolioId(string portfolio, int customerId)
        {
            int portfolioId = 0;
            Database db;
            DbCommand cmdGetPortFolioId;
            DataSet dsGetPortFolioId;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetPortFolioId = db.GetStoredProcCommand("SP_GetPortFolioId");
                db.AddInParameter(cmdGetPortFolioId, "@portfolioname", DbType.String, portfolio);
                db.AddInParameter(cmdGetPortFolioId, "@customerId", DbType.Int32, customerId);
                dsGetPortFolioId = db.ExecuteDataSet(cmdGetPortFolioId);
                portfolioId = int.Parse(dsGetPortFolioId.Tables[0].Rows[0]["CP_PortfolioId"].ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return portfolioId;
        }

    }
}
