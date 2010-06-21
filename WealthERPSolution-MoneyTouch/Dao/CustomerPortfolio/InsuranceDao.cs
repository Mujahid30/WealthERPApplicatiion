using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using VoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace DaoCustomerPortfolio
{
    public class InsuranceDao
    {
        public int CreateInsurancePortfolio(InsuranceVo insuranceVo, int userId)
        {
            Database db;
            DbCommand createInsurancePortfolioCmd;
            int insuranceId = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createInsurancePortfolioCmd = db.GetStoredProcCommand("SP_CreateInsurancePortfolio");
                db.AddInParameter(createInsurancePortfolioCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, insuranceVo.AssetInstrumentCategoryCode);
                db.AddInParameter(createInsurancePortfolioCmd, "@PAG_AssetGroupCode", DbType.String, insuranceVo.AssetGroupCode);
                db.AddInParameter(createInsurancePortfolioCmd, "@CIA_AccountId", DbType.Int32, insuranceVo.AccountId);
                db.AddInParameter(createInsurancePortfolioCmd, "@XII_InsuranceIssuerCode", DbType.String, insuranceVo.InsuranceIssuerCode);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_Name", DbType.String, insuranceVo.Name);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_StartDate", DbType.DateTime, insuranceVo.StartDate);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_EndDate", DbType.DateTime, insuranceVo.EndDate);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_SumAssured", DbType.Decimal, insuranceVo.SumAssured);
                if (insuranceVo.ApplicationDate != DateTime.MinValue)
                    db.AddInParameter(createInsurancePortfolioCmd, "@CINP_ApplicationDate", DbType.DateTime, insuranceVo.ApplicationDate);
                else
                    db.AddInParameter(createInsurancePortfolioCmd, "@CINP_ApplicationDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_ApplicationNum", DbType.String, insuranceVo.ApplicationNumber);
                db.AddInParameter(createInsurancePortfolioCmd, "@XF_PremiumFrequencyCode", DbType.String, insuranceVo.PremiumFrequencyCode);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_PremiumAmount", DbType.Decimal, insuranceVo.PremiumAmount);
                if (insuranceVo.FirstPremiumDate != DateTime.MinValue)
                    db.AddInParameter(createInsurancePortfolioCmd, "@CINP_FirstPremiumDate", DbType.DateTime, insuranceVo.FirstPremiumDate);
                else
                    db.AddInParameter(createInsurancePortfolioCmd, "@CINP_FirstPremiumDate", DbType.DateTime, DBNull.Value);
                if (insuranceVo.LastPremiumDate != DateTime.MinValue)
                    db.AddInParameter(createInsurancePortfolioCmd, "@CINP_LastPremiumDate", DbType.DateTime, insuranceVo.LastPremiumDate);
                else
                    db.AddInParameter(createInsurancePortfolioCmd, "@CINP_LastPremiumDate", DbType.DateTime, DBNull.Value);


                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_PolicyPeriod", DbType.Decimal, insuranceVo.PolicyPeriod);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_PremiumAccumalated", DbType.Decimal, insuranceVo.PremiumAccumalated);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_PolicyEpisode", DbType.Decimal, insuranceVo.PolicyEpisode);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_BonusAccumalated", DbType.Decimal, insuranceVo.BonusAccumalated);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_SurrenderValue", DbType.Decimal, insuranceVo.SurrenderValue);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_MaturityValue", DbType.Decimal, insuranceVo.MaturityValue);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_ULIPCharges", DbType.Decimal, insuranceVo.ULIPCharges);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_Remark", DbType.String, insuranceVo.Remarks);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_GracePeriod", DbType.Decimal, insuranceVo.GracePeriod);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_PremiumPaymentDate", DbType.Int16, insuranceVo.PremiumPaymentDate);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_ModifiedBy", DbType.Int32, userId);
                db.AddOutParameter(createInsurancePortfolioCmd, "@InsuranceId", DbType.Int32, 5000);


                if (db.ExecuteNonQuery(createInsurancePortfolioCmd) != 0)
                    insuranceId = int.Parse(db.GetParameterValue(createInsurancePortfolioCmd, "InsuranceId").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceDao.cs:CreateInsurancePortfolio()");
                object[] objects = new object[2];
                objects[0] = insuranceVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            //   return bResult;
            return insuranceId;
        }

        public bool UpdateInsurancePortfolio(InsuranceVo insuranceVo, int userId)
        {
            bool bResult = false;

            Database db;
            DbCommand createInsurancePortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createInsurancePortfolioCmd = db.GetStoredProcCommand("SP_UpdateInsurancePortfolio");

                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_InsuranceNPId", DbType.Int32, insuranceVo.CustInsInvId);
                db.AddInParameter(createInsurancePortfolioCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, insuranceVo.AssetInstrumentCategoryCode);
                db.AddInParameter(createInsurancePortfolioCmd, "@PAG_AssetGroupCode", DbType.String, insuranceVo.AssetGroupCode);
                db.AddInParameter(createInsurancePortfolioCmd, "@CIA_AccountId", DbType.Int32, insuranceVo.AccountId);
                db.AddInParameter(createInsurancePortfolioCmd, "@XII_InsuranceIssuerCode", DbType.String, insuranceVo.InsuranceIssuerCode);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_Name", DbType.String, insuranceVo.Name);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_StartDate", DbType.DateTime, insuranceVo.StartDate);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_EndDate", DbType.DateTime, insuranceVo.EndDate);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_SumAssured", DbType.Decimal, insuranceVo.SumAssured);
                if (insuranceVo.ApplicationDate != DateTime.MinValue)
                    db.AddInParameter(createInsurancePortfolioCmd, "@CINP_ApplicationDate", DbType.DateTime, insuranceVo.ApplicationDate);
                else
                    db.AddInParameter(createInsurancePortfolioCmd, "@CINP_ApplicationDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_ApplicationNum", DbType.String, insuranceVo.ApplicationNumber);
                db.AddInParameter(createInsurancePortfolioCmd, "@XF_PremiumFrequencyCode", DbType.String, insuranceVo.PremiumFrequencyCode);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_PremiumAmount", DbType.Decimal, insuranceVo.PremiumAmount);
                if (insuranceVo.FirstPremiumDate != DateTime.MinValue)
                    db.AddInParameter(createInsurancePortfolioCmd, "@CINP_FirstPremiumDate", DbType.DateTime, insuranceVo.FirstPremiumDate);
                else
                    db.AddInParameter(createInsurancePortfolioCmd, "@CINP_FirstPremiumDate", DbType.DateTime, DBNull.Value);
                if (insuranceVo.FirstPremiumDate != DateTime.MinValue)
                    db.AddInParameter(createInsurancePortfolioCmd, "@CINP_LastPremiumDate", DbType.DateTime, insuranceVo.LastPremiumDate);
                else
                    db.AddInParameter(createInsurancePortfolioCmd, "@CINP_LastPremiumDate", DbType.DateTime, DBNull.Value);

                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_PolicyPeriod", DbType.Decimal, insuranceVo.PolicyPeriod);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_PremiumAccumalated", DbType.Decimal, insuranceVo.PremiumAccumalated);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_PolicyEpisode", DbType.Decimal, insuranceVo.PolicyEpisode);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_BonusAccumalated", DbType.Decimal, insuranceVo.BonusAccumalated);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_SurrenderValue", DbType.Decimal, insuranceVo.SurrenderValue);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_MaturityValue", DbType.Decimal, insuranceVo.MaturityValue);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_ULIPCharges", DbType.Decimal, insuranceVo.ULIPCharges);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_Remark", DbType.String, insuranceVo.Remarks);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_GracePeriod", DbType.Decimal, insuranceVo.GracePeriod);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_PremiumPaymentDate", DbType.Int16, insuranceVo.PremiumPaymentDate);
                db.AddInParameter(createInsurancePortfolioCmd, "@CINP_ModifiedBy", DbType.String, userId);

                if (db.ExecuteNonQuery(createInsurancePortfolioCmd) != 0)
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
                FunctionInfo.Add("Method", "InsuranceDao.cs:UpdateInsurancePortfolio()");
                object[] objects = new object[2];
                objects[0] = insuranceVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool DeleteInsurancePortfolio(int InsuranceId, int AccountId)
        {
            bool bResult = false;

            Database db;
            DbCommand deleteInsurancePortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteInsurancePortfolioCmd = db.GetStoredProcCommand("SP_DeleteInsuranceNetPosition");

                db.AddInParameter(deleteInsurancePortfolioCmd, "@CINP_InsuranceNPId", DbType.Int32, InsuranceId);
                db.AddInParameter(deleteInsurancePortfolioCmd, "@CIA_AccountId", DbType.Int32, AccountId);

                if (db.ExecuteNonQuery(deleteInsurancePortfolioCmd) != 0)
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
                FunctionInfo.Add("Method", "InsuranceDao.cs:UpdateInsurancePortfolio()");
                object[] objects = new object[2];
                objects[0] = InsuranceId;
                objects[1] = AccountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public List<InsuranceVo> GetInsurancePortfolio(int portfolioId, int currentPage, string sortExpression, out int Count)
        {
            List<InsuranceVo> insuranceList = null;
            InsuranceVo insuranceVo;
            Database db;
            DbCommand getInsurancePortfolioCmd;
            DataSet dsGetInsurancePortfolio;
            DataTable dtGetInsurancePortfolio;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getInsurancePortfolioCmd = db.GetStoredProcCommand("SP_GetInsurancePortfolio");
                db.AddInParameter(getInsurancePortfolioCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getInsurancePortfolioCmd, "@CurrentPage", DbType.Int32, currentPage);
                db.AddInParameter(getInsurancePortfolioCmd, "@SortOrder", DbType.String, sortExpression);

                dsGetInsurancePortfolio = db.ExecuteDataSet(getInsurancePortfolioCmd);
                if (dsGetInsurancePortfolio.Tables[0].Rows.Count > 0)
                {
                    dtGetInsurancePortfolio = dsGetInsurancePortfolio.Tables[0];

                    insuranceList = new List<InsuranceVo>();

                    foreach (DataRow dr in dtGetInsurancePortfolio.Rows)
                    {
                        insuranceVo = new InsuranceVo();
                        insuranceVo.AccountId = int.Parse(dr["CIA_AccountId"].ToString());
                        insuranceVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                        insuranceVo.AssetInstrumentCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        insuranceVo.AssetInstrumentCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        insuranceVo.BonusAccumalated = float.Parse(dr["CINP_BonusAccumalated"].ToString());
                        insuranceVo.CustInsInvId = int.Parse(dr["CINP_InsuranceNPId"].ToString());
                        if (dr["CINP_EndDate"].ToString() != string.Empty)
                            insuranceVo.EndDate = DateTime.Parse(dr["CINP_EndDate"].ToString());
                        insuranceVo.InsuranceIssuerCode = dr["XII_InsuranceIssuerCode"].ToString();
                        insuranceVo.MaturityValue = float.Parse(dr["CINP_MaturityValue"].ToString());
                        insuranceVo.Name = dr["CINP_Name"].ToString();
                        insuranceVo.PremiumAccumalated = float.Parse(dr["CINP_PremiumAccumalated"].ToString());
                        insuranceVo.PremiumAmount = float.Parse(dr["CINP_PremiumAmount"].ToString());

                        insuranceVo.PremiumFrequencyCode = dr["XF_PremiumFrequencyCode"].ToString();
                        if (dr["CINP_StartDate"].ToString() != string.Empty)
                            insuranceVo.StartDate = DateTime.Parse(dr["CINP_StartDate"].ToString());
                        insuranceVo.SumAssured = float.Parse(dr["CINP_SumAssured"].ToString());
                        insuranceVo.SurrenderValue = float.Parse(dr["CINP_SurrenderValue"].ToString());
                        insuranceVo.ULIPCharges = float.Parse(dr["CINP_ULIPCharges"].ToString());
                        insuranceVo.PremiumPaymentDate = Int16.Parse(dr["CINP_PremiumPaymentDate"].ToString());
                        if (dr["CINP_FirstPremiumDate"].ToString() != string.Empty)
                            insuranceVo.FirstPremiumDate = DateTime.Parse(dr["CINP_FirstPremiumDate"].ToString());
                        if (dr["CINP_LastPremiumDate"].ToString() != string.Empty)
                            insuranceVo.LastPremiumDate = DateTime.Parse(dr["CINP_LastPremiumDate"].ToString());
                        if (dr["CINP_PolicyPeriod"].ToString() != string.Empty)
                            insuranceVo.PolicyPeriod = float.Parse(dr["CINP_PolicyPeriod"].ToString());
                        if (dr["CINP_PolicyEpisode"].ToString() != string.Empty)
                            insuranceVo.PolicyEpisode = float.Parse(dr["CINP_PolicyEpisode"].ToString());
                        if (dr["CINP_GracePeriod"].ToString() != string.Empty)
                            insuranceVo.GracePeriod = float.Parse(dr["CINP_GracePeriod"].ToString());
                        if (dr["CINP_ApplicationDate"].ToString() != string.Empty)
                            insuranceVo.ApplicationDate = DateTime.Parse(dr["CINP_ApplicationDate"].ToString());

                        insuranceList.Add(insuranceVo);
                    }
                }

                if (dsGetInsurancePortfolio.Tables[1] != null && dsGetInsurancePortfolio.Tables[1].Rows.Count > 0)
                    Count = Int32.Parse(dsGetInsurancePortfolio.Tables[1].Rows[0][0].ToString());
                else
                    Count = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceDao.cs:GetInsurancePortfolio()");


                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return insuranceList;
        }

        public InsuranceVo GetInsuranceAsset(int insurancePortfolioId)
        {
            InsuranceVo insuranceVo = null;
            Database db;
            DbCommand getInsurancePortfolioCmd;
            DataSet dsGetInsurancePortfolio;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getInsurancePortfolioCmd = db.GetStoredProcCommand("SP_GetInsuranceAsset");
                db.AddInParameter(getInsurancePortfolioCmd, "@CINP_InsuranceNPId", DbType.Int32, insurancePortfolioId);
                dsGetInsurancePortfolio = db.ExecuteDataSet(getInsurancePortfolioCmd);

                if (dsGetInsurancePortfolio.Tables[0].Rows.Count > 0)
                {
                    insuranceVo = new InsuranceVo();
                    dr = dsGetInsurancePortfolio.Tables[0].Rows[0];
                    insuranceVo.AccountId = int.Parse(dr["CIA_AccountId"].ToString());
                    insuranceVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                    insuranceVo.AssetInstrumentCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();

                    insuranceVo.BonusAccumalated = float.Parse(dr["CINP_BonusAccumalated"].ToString());
                    insuranceVo.CustInsInvId = int.Parse(dr["CINP_InsuranceNPId"].ToString());
                    if (dr["CINP_EndDate"].ToString() != string.Empty)
                        insuranceVo.EndDate = DateTime.Parse(dr["CINP_EndDate"].ToString());
                    insuranceVo.InsuranceIssuerCode = dr["XII_InsuranceIssuerCode"].ToString();
                    insuranceVo.MaturityValue = float.Parse(dr["CINP_MaturityValue"].ToString());
                    insuranceVo.Name = dr["CINP_Name"].ToString();
                    insuranceVo.PremiumAccumalated = float.Parse(dr["CINP_PremiumAccumalated"].ToString());
                    insuranceVo.PremiumAmount = float.Parse(dr["CINP_PremiumAmount"].ToString());

                    insuranceVo.PremiumFrequencyCode = dr["XF_PremiumFrequencyCode"].ToString();
                    if (dr["CINP_StartDate"].ToString() != string.Empty)
                        insuranceVo.StartDate = DateTime.Parse(dr["CINP_StartDate"].ToString());
                    insuranceVo.SumAssured = float.Parse(dr["CINP_SumAssured"].ToString());
                    insuranceVo.SurrenderValue = float.Parse(dr["CINP_SurrenderValue"].ToString());
                    insuranceVo.ULIPCharges = float.Parse(dr["CINP_ULIPCharges"].ToString());
                    if (dr["CINP_PremiumPaymentDate"].ToString() != string.Empty)
                        insuranceVo.PremiumPaymentDate = Int16.Parse(dr["CINP_PremiumPaymentDate"].ToString());
                    if (dr["CINP_FirstPremiumDate"].ToString() != string.Empty)
                        insuranceVo.FirstPremiumDate = DateTime.Parse(dr["CINP_FirstPremiumDate"].ToString());
                    if (dr["CINP_LastPremiumDate"].ToString() != string.Empty)
                        insuranceVo.LastPremiumDate = DateTime.Parse(dr["CINP_LastPremiumDate"].ToString());

                    insuranceVo.PolicyPeriod = float.Parse(dr["CINP_PolicyPeriod"].ToString());
                    insuranceVo.PolicyEpisode = float.Parse(dr["CINP_PolicyEpisode"].ToString());
                    insuranceVo.GracePeriod = float.Parse(dr["CINP_GracePeriod"].ToString());
                    insuranceVo.ApplicationNumber = dr["CINP_ApplicationNum"].ToString();
                    if (dr["CINP_ApplicationDate"].ToString() != string.Empty)
                        insuranceVo.ApplicationDate = DateTime.Parse(dr["CINP_ApplicationDate"].ToString());
                    insuranceVo.Remarks = dr["CINP_Remark"].ToString();
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

                FunctionInfo.Add("Method", "InsuranceDao.cs:GetInsuranceAsset()");

                object[] objects = new object[1];
                objects[0] = insurancePortfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return insuranceVo;
        }

        public int ChkAccountAvail(int customerId)
        {
            int count = 0;
            Database db;
            DbCommand chkAccountAvailCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkAccountAvailCmd = db.GetStoredProcCommand("SP_ChkInsuranceAccount");
                db.AddInParameter(chkAccountAvailCmd, "@C_CustomerId", DbType.Int32, customerId);
                count = int.Parse(db.ExecuteScalar(chkAccountAvailCmd).ToString());


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceDao.cs:ChkAccountAvail()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return count;
        }

        public int CreateInsuranceAccount(InsuranceVo insuranceVo, int userId)
        {
            int id = 0;
            Database db;
            DbCommand createInsuranceAccountCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createInsuranceAccountCmd = db.GetStoredProcCommand("SP_CreateInsuranceAccount");
                //db.AddInParameter(createInsuranceAccountCmd, "@CP_PortfolioId", DbType.Int32, 'portfolioId');
                db.AddInParameter(createInsuranceAccountCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, insuranceVo.AssetInstrumentCategoryCode);
                db.AddInParameter(createInsuranceAccountCmd, "@PAG_AssetGroupCode", DbType.String, insuranceVo.AssetGroupCode);
                db.AddInParameter(createInsuranceAccountCmd, "@CIA_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(createInsuranceAccountCmd, "@CIA_CreatedBy", DbType.Int32, userId);
                db.AddOutParameter(createInsuranceAccountCmd, "@AccountId", DbType.Int32, 5000);
                if (db.ExecuteNonQuery(createInsuranceAccountCmd) != 0)
                    id = int.Parse(db.GetParameterValue(createInsuranceAccountCmd, "AccountId").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceDao.cs:CreateInsuranceAccount()");


                object[] objects = new object[2];
                objects[0] = insuranceVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return id;
        }

        public DataSet GetCustomerInsuranceAccounts(int customerId, string assetGroup)
        {
            Database db;
            DbCommand getCustomerInsuranceAccountsCmd;
            DataSet dsGetCustomerInsuranceAccounts = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerInsuranceAccountsCmd = db.GetStoredProcCommand("SP_GetCustomerInsuranceAccounts");
                db.AddInParameter(getCustomerInsuranceAccountsCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getCustomerInsuranceAccountsCmd, "@PAG_AssetGroupCode", DbType.String, assetGroup);
                dsGetCustomerInsuranceAccounts = db.ExecuteDataSet(getCustomerInsuranceAccountsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceDao.cs:GetCustomerInsuranceAccounts()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = assetGroup;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerInsuranceAccounts;
        }

        public bool CreateInsuranceULIPPlan(InsuranceULIPVo insuranceUlipVo)
        {
            bool bResult = false;
            Database db;
            DbCommand createInsuranceULIPPlanCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createInsuranceULIPPlanCmd = db.GetStoredProcCommand("SP_CreateInsuranceULIPPlan");
                db.AddInParameter(createInsuranceULIPPlanCmd, "@CINP_InsuranceNPId", DbType.Int32, insuranceUlipVo.CIP_CustInsInvId);
                db.AddInParameter(createInsuranceULIPPlanCmd, "@WUSP_ULIPSubPlanCode", DbType.Int32, insuranceUlipVo.WUP_ULIPSubPlaCode);
                db.AddInParameter(createInsuranceULIPPlanCmd, "@CIUP_AllocationPer", DbType.Double, insuranceUlipVo.CIUP_AllocationPer);
                db.AddInParameter(createInsuranceULIPPlanCmd, "@CIUP_Unit", DbType.Double, insuranceUlipVo.CIUP_Unit);
                db.AddInParameter(createInsuranceULIPPlanCmd, "@CIUP_PurchasePrice", DbType.Double, insuranceUlipVo.CIUP_PurchasePrice);
                db.AddInParameter(createInsuranceULIPPlanCmd, "@CIUP_CreatedBy", DbType.Int32, insuranceUlipVo.CIUP_CreatedBy);
                db.AddInParameter(createInsuranceULIPPlanCmd, "@CIUP_ModifiedBy", DbType.Int32, insuranceUlipVo.CIUP_ModifiedBy);
                if (db.ExecuteNonQuery(createInsuranceULIPPlanCmd) != 0)
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
                FunctionInfo.Add("Method", "InsuranceDao.cs:CreateInsuranceULIPPlan()");
                object[] objects = new object[1];
                objects[0] = insuranceUlipVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public bool UpdateInsuranceULIPPlan(InsuranceULIPVo insuranceUlipVo)
        {
            bool bResult = false;
            Database db;
            DbCommand updateInsuranceULIPPlanCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateInsuranceULIPPlanCmd = db.GetStoredProcCommand("SP_UpdateInsuranceULIPPlan");
                db.AddInParameter(updateInsuranceULIPPlanCmd, "@CIUP_ULIPPlanId", DbType.Int32, insuranceUlipVo.CIUP_ULIPPlanId);
                db.AddInParameter(updateInsuranceULIPPlanCmd, "@CIUP_AllocationPer", DbType.Double, insuranceUlipVo.CIUP_AllocationPer);
                db.AddInParameter(updateInsuranceULIPPlanCmd, "@CIUP_Unit", DbType.Double, insuranceUlipVo.CIUP_Unit);
                db.AddInParameter(updateInsuranceULIPPlanCmd, "@CIUP_PurchasePrice", DbType.Double, insuranceUlipVo.CIUP_PurchasePrice);
                db.AddInParameter(updateInsuranceULIPPlanCmd, "@CIUP_PurchaseDate", DbType.DateTime, insuranceUlipVo.CIUP_PurchaseDate);
                db.AddInParameter(updateInsuranceULIPPlanCmd, "@CIUP_ModifiedBy", DbType.Int32, insuranceUlipVo.CIUP_ModifiedBy);
                if (db.ExecuteNonQuery(updateInsuranceULIPPlanCmd) != 0)
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
                FunctionInfo.Add("Method", "InsuranceDao.cs:UpdateInsuranceULIPPlan()");
                object[] objects = new object[1];
                objects[0] = insuranceUlipVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public bool DeleteInsuranceUlipPlans(int InsuranceID)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteInsuranceULIPPlanCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteInsuranceULIPPlanCmd = db.GetStoredProcCommand("SP_DeleteInsuranceULIPPlan");
                db.AddInParameter(deleteInsuranceULIPPlanCmd, "@CINP_InsuranceNPId", DbType.Int32, InsuranceID);
                if (db.ExecuteNonQuery(deleteInsuranceULIPPlanCmd) != 0)
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
                FunctionInfo.Add("Method", "InsuranceDao.cs:DeleteInsuranceUlipPlans()");
                object[] objects = new object[1];
                objects[0] = InsuranceID;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public bool CreateMoneyBackEpisode(MoneyBackEpisodeVo moneyBackEpisodeVo)
        {
            bool bReslult = false;
            Database db;
            DbCommand createMoneybackEpisodeCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createMoneybackEpisodeCmd = db.GetStoredProcCommand("SP_CreateMoneyBackEpisode");
                db.AddInParameter(createMoneybackEpisodeCmd, "@CIMBE_RepaymentDate", DbType.DateTime, moneyBackEpisodeVo.CIMBE_RepaymentDate);
                db.AddInParameter(createMoneybackEpisodeCmd, "@CIMBE_RepaidPer", DbType.Double, moneyBackEpisodeVo.CIMBE_RepaidPer);
                db.AddInParameter(createMoneybackEpisodeCmd, "@CINP_InsuranceNPId", DbType.Int32, moneyBackEpisodeVo.CustInsInvId);
                db.AddInParameter(createMoneybackEpisodeCmd, "@CIMBE_CreatedBy", DbType.Int32, moneyBackEpisodeVo.CIMBE_CreatedBy);
                db.AddInParameter(createMoneybackEpisodeCmd, "@CIMBE_ModifiedBy", DbType.Int32, moneyBackEpisodeVo.CIMBE_ModifiedBy);
                if (db.ExecuteNonQuery(createMoneybackEpisodeCmd) != 0)
                    bReslult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceDao.cs:CreateMoneyBackEpisode()");
                object[] objects = new object[1];
                objects[0] = moneyBackEpisodeVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bReslult;
        }

        public bool UpdateMoneyBackEpisode(MoneyBackEpisodeVo moneyBackEpisodeVo)
        {
            bool bReslult = false;
            Database db;
            DbCommand updateMoneybackEpisodeCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateMoneybackEpisodeCmd = db.GetStoredProcCommand("SP_UpdateMoneyBackEpisode");
                db.AddInParameter(updateMoneybackEpisodeCmd, "@CIMBE_RepaymentDate", DbType.DateTime, moneyBackEpisodeVo.CIMBE_RepaymentDate);
                db.AddInParameter(updateMoneybackEpisodeCmd, "@CIMBE_RepaidPer", DbType.Double, moneyBackEpisodeVo.CIMBE_RepaidPer);
                db.AddInParameter(updateMoneybackEpisodeCmd, "@CIMBE_EpisodeId", DbType.Int32, moneyBackEpisodeVo.MoneyBackId);
                db.AddInParameter(updateMoneybackEpisodeCmd, "@CIMBE_ModifiedBy", DbType.Int32, moneyBackEpisodeVo.CIMBE_ModifiedBy);
                if (db.ExecuteNonQuery(updateMoneybackEpisodeCmd) != 0)
                    bReslult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceDao.cs:UpdateMoneyBackEpisode()");
                object[] objects = new object[1];
                objects[0] = moneyBackEpisodeVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bReslult;
        }

        public bool DeleteMoneyBackEpisode(MoneyBackEpisodeVo moneyBackEpisodeVo)
        {
            bool blResult = false;

            Database db;
            DbCommand deleteMoneybackEpisodeCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteMoneybackEpisodeCmd = db.GetStoredProcCommand("SP_DeleteMoneyBackEpisode");
                db.AddInParameter(deleteMoneybackEpisodeCmd, "@CIMBE_EpisodeId", DbType.Int32, moneyBackEpisodeVo.MoneyBackId);
                if (db.ExecuteNonQuery(deleteMoneybackEpisodeCmd) != 0)
                    blResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceDao.cs:DeleteMoneyBackEpisode()");
                object[] objects = new object[1];
                objects[0] = moneyBackEpisodeVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public List<InsuranceULIPVo> GetInsuranceULIPList(int insuranceId)
        {
            List<InsuranceULIPVo> insuranceUlipList = null;
            InsuranceULIPVo insuranceUlipVo;
            Database db;
            DbCommand getInsuranceUlipListCmd;
            DataSet getInsuranceUlipListDs;
            DataTable getInsuranceUlipListDt;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getInsuranceUlipListCmd = db.GetStoredProcCommand("SP_GetInsuranceULIPList");
                db.AddInParameter(getInsuranceUlipListCmd, "@CINP_InsuranceNPId", DbType.Int32, insuranceId);
                getInsuranceUlipListDs = db.ExecuteDataSet(getInsuranceUlipListCmd);
                if (getInsuranceUlipListDs.Tables[0].Rows.Count > 0)
                {
                    getInsuranceUlipListDt = getInsuranceUlipListDs.Tables[0];
                    insuranceUlipList = new List<InsuranceULIPVo>();
                    foreach (DataRow dr in getInsuranceUlipListDt.Rows)
                    {
                        insuranceUlipVo = new InsuranceULIPVo();
                        insuranceUlipVo.CIUP_ULIPPlanId = int.Parse(dr["CIUP_ULIPPlanId"].ToString());
                        insuranceUlipVo.CIUP_AllocationPer = float.Parse(dr["CIUP_AllocationPer"].ToString());
                        insuranceUlipVo.CIUP_CreatedBy = int.Parse(dr["CIUP_CreatedBy"].ToString());
                        insuranceUlipVo.CIUP_ModifiedBy = int.Parse(dr["CIUP_ModifiedBy"].ToString());
                        insuranceUlipVo.CIUP_PurchasePrice = float.Parse(dr["CIUP_PurchasePrice"].ToString());
                        insuranceUlipVo.CIUP_Unit = float.Parse(dr["CIUP_Unit"].ToString());
                        insuranceUlipVo.WUP_ULIPSubPlaCode = dr["WUSP_ULIPSubPlanCode"].ToString();
                        insuranceUlipList.Add(insuranceUlipVo);

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
                FunctionInfo.Add("Method", "InsuranceDao.cs:GetInsuranceULIPList()");
                object[] objects = new object[1];
                objects[0] = insuranceId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return insuranceUlipList;
        }

        public InsuranceULIPVo GetInsuranceULIPDetails(int insuranceUlipId)
        {
            InsuranceULIPVo insuranceUlipVo = null;
            DataSet getInsuranceUlipDs;
            Database db;
            DbCommand getInsuranceUlipCmd;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getInsuranceUlipCmd = db.GetStoredProcCommand("SP_GetInsuranceULIP");
                db.AddInParameter(getInsuranceUlipCmd, "@CIUP_ULIPPlanId", DbType.Int32, insuranceUlipId);
                getInsuranceUlipDs = db.ExecuteDataSet(getInsuranceUlipCmd);
                if (getInsuranceUlipDs.Tables[0].Rows.Count > 0)
                {
                    insuranceUlipVo = new InsuranceULIPVo();
                    dr = getInsuranceUlipDs.Tables[0].Rows[0];
                    insuranceUlipVo.CIP_CustInsInvId = int.Parse(dr["CIP_CustInsInvId"].ToString());
                    insuranceUlipVo.CIUP_AllocationPer = float.Parse(dr["CIUP_AllocationPer"].ToString());
                    insuranceUlipVo.CIUP_CreatedBy = int.Parse(dr["CIUP_CreatedBy"].ToString());
                    insuranceUlipVo.CIUP_ModifiedBy = int.Parse(dr["CIUP_ModifiedBy"].ToString());
                    insuranceUlipVo.CIUP_PurchasePrice = float.Parse(dr["CIUP_PurchasePrice"].ToString());
                    insuranceUlipVo.CIUP_Unit = float.Parse(dr["CIUP_Unit"].ToString());
                    insuranceUlipVo.WUP_ULIPSubPlaCode = dr["WUP_ULIPSubPlaCode"].ToString();
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
                FunctionInfo.Add("Method", "InsuranceDao.cs:GetInsuranceULIPDetails()");
                object[] objects = new object[1];
                objects[0] = insuranceUlipId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return insuranceUlipVo;
        }

        public List<MoneyBackEpisodeVo> GetMoneyBackEpisodeList(int insuranceId)
        {
            List<MoneyBackEpisodeVo> moneyBackEpisodeList = null;
            MoneyBackEpisodeVo moneyBackEpisodeVo;
            Database db;
            DbCommand getMoneyBackEpisodeCmd;
            DataSet getMoneyBackEpisodeDs;
            DataTable getMoneyBackEpisodeDt;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMoneyBackEpisodeCmd = db.GetStoredProcCommand("SP_GetMoneyBackEpisodeList");
                db.AddInParameter(getMoneyBackEpisodeCmd, "@CINP_InsuranceNPId", DbType.Int32, insuranceId);
                getMoneyBackEpisodeDs = db.ExecuteDataSet(getMoneyBackEpisodeCmd);
                if (getMoneyBackEpisodeDs.Tables[0].Rows.Count > 0)
                {
                    getMoneyBackEpisodeDt = getMoneyBackEpisodeDs.Tables[0];
                    moneyBackEpisodeList = new List<MoneyBackEpisodeVo>();
                    foreach (DataRow dr in getMoneyBackEpisodeDt.Rows)
                    {
                        moneyBackEpisodeVo = new MoneyBackEpisodeVo();
                        moneyBackEpisodeVo.MoneyBackId = Int32.Parse(dr["CIMBE_EpisodeId"].ToString());
                        moneyBackEpisodeVo.CustInsInvId = int.Parse(dr["CINP_InsuranceNPId"].ToString());
                        moneyBackEpisodeVo.CIMBE_RepaymentDate = DateTime.Parse(dr["CIMBE_RepaymentDate"].ToString());
                        moneyBackEpisodeVo.CIMBE_RepaidPer = float.Parse(dr["CIMBE_RepaidPer"].ToString());

                        moneyBackEpisodeList.Add(moneyBackEpisodeVo);
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
                FunctionInfo.Add("Method", "InsuranceDao.cs:GetMoneyBackEpisodeList()");
                object[] objects = new object[1];
                objects[0] = insuranceId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return moneyBackEpisodeList;
        }

        public DataSet GetUlipPlanCode(int InsuranceId)
        {
            Database db;
            DbCommand getUlipPlanCodeCmd;
            DataSet dsGetUlipPlanCode = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getUlipPlanCodeCmd = db.GetStoredProcCommand("SP_GetCustomerUlipPlanCode");
                db.AddInParameter(getUlipPlanCodeCmd, "@CINP_InsuranceNPId", DbType.Int32, InsuranceId);
                dsGetUlipPlanCode = db.ExecuteDataSet(getUlipPlanCodeCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceDao.cs:GetUlipPlanCode()");

                object[] objects = new object[1];
                objects[0] = InsuranceId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetUlipPlanCode;
        }

        public DataTable ChkGenInsurancePortfolioExist(int customerId)
        {
            Database db;
            DbCommand chkGenInsPortfolioExistCmd;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkGenInsPortfolioExistCmd = db.GetStoredProcCommand("SP_GetCustomerGIPortfolio");
                db.AddInParameter(chkGenInsPortfolioExistCmd, "@C_CustomerId", DbType.Int32, customerId);
                DataSet ds = db.ExecuteDataSet(chkGenInsPortfolioExistCmd);
                if (ds != null)
                {
                    dt = ds.Tables[0];
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

                FunctionInfo.Add("Method", "InsuranceDao.cs:chkGenInsPortfolioExistCmd()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public DataTable ChkLifeInsurancePortfolioExist(int customerId)
        {
            Database db;
            DbCommand chkLifeInsPortfolioExistCmd;
            DataTable dt = new DataTable();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkLifeInsPortfolioExistCmd = db.GetStoredProcCommand("SP_GetCustomerLIPortfolio");
                db.AddInParameter(chkLifeInsPortfolioExistCmd, "@C_CustomerId", DbType.Int32, customerId);
                DataSet ds = db.ExecuteDataSet(chkLifeInsPortfolioExistCmd);
                if (ds != null)
                {
                    dt = ds.Tables[0];
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

                FunctionInfo.Add("Method", "InsuranceDao.cs:chkLifeInsPortfolioExistCmd()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public int CreateCustomerGIPortfolio(int customerId, int userId)
        {
            int customerGIPortfolioId = 0;
            Database db;
            DbCommand createGIPortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createGIPortfolioCmd = db.GetStoredProcCommand("SP_CreateCustomerPortfolio");
                db.AddInParameter(createGIPortfolioCmd, "C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(createGIPortfolioCmd, "CP_IsMainPortfolio", DbType.Int16, 0);
                db.AddInParameter(createGIPortfolioCmd, "XPT_PortfolioTypeCode", DbType.String, "GIP");
                db.AddInParameter(createGIPortfolioCmd, "CP_PMSIdentifier", DbType.String, DBNull.Value);
                db.AddInParameter(createGIPortfolioCmd, "CP_PortfolioName", DbType.String, "Gen Ins Portfolio");
                db.AddInParameter(createGIPortfolioCmd, "CP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createGIPortfolioCmd, "CP_ModifiedBy", DbType.Int32, userId);
                db.AddOutParameter(createGIPortfolioCmd, "CP_PortfolioId", DbType.Int32, 10);

                if (db.ExecuteNonQuery(createGIPortfolioCmd) != 0)
                    customerGIPortfolioId = int.Parse(db.GetParameterValue(createGIPortfolioCmd, "CP_PortfolioId").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceDao.cs:CreateCustomerGIPortfolio()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerGIPortfolioId;
        }

        public int CreateCustomerLIPortfolio(int customerId, int userId)
        {
            int customerLIPortfolioId = 0;
            Database db;
            DbCommand createLIPortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createLIPortfolioCmd = db.GetStoredProcCommand("SP_CreateCustomerPortfolio");
                db.AddInParameter(createLIPortfolioCmd, "C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(createLIPortfolioCmd, "CP_IsMainPortfolio", DbType.Int16, 0);
                db.AddInParameter(createLIPortfolioCmd, "XPT_PortfolioTypeCode", DbType.String, "LIP");
                db.AddInParameter(createLIPortfolioCmd, "CP_PMSIdentifier", DbType.String, DBNull.Value);
                db.AddInParameter(createLIPortfolioCmd, "CP_PortfolioName", DbType.String, "Life Ins Portfolio");
                db.AddInParameter(createLIPortfolioCmd, "CP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createLIPortfolioCmd, "CP_ModifiedBy", DbType.Int32, userId);
                db.AddOutParameter(createLIPortfolioCmd, "CP_PortfolioId", DbType.Int32, 10);

                if (db.ExecuteNonQuery(createLIPortfolioCmd) != 0)
                    customerLIPortfolioId = int.Parse(db.GetParameterValue(createLIPortfolioCmd, "CP_PortfolioId").ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceDao.cs:CreateCustomerGIPortfolio()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerLIPortfolioId;
        }

        public int CreateCustomerGIAccount(GeneralInsuranceVo generalInsuranceVo, int userId)
        {
            int accountId = 0;
            Database db;
            DbCommand createGIAccountCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createGIAccountCmd = db.GetStoredProcCommand("SP_CreateCustomerGIAccount");
                db.AddInParameter(createGIAccountCmd, "@CP_PortfolioId", DbType.Int32, generalInsuranceVo.PortfolioId);
                db.AddInParameter(createGIAccountCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, generalInsuranceVo.AssetInstrumentCategoryCode);
                db.AddInParameter(createGIAccountCmd, "@PAG_AssetGroupCode", DbType.String, "GI");
                db.AddInParameter(createGIAccountCmd, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, generalInsuranceVo.AssetInstrumentSubCategoryCode);
                db.AddInParameter(createGIAccountCmd, "@CGIA_PolicyNum", DbType.String, generalInsuranceVo.PolicyNumber);
                db.AddInParameter(createGIAccountCmd, "@CGIA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createGIAccountCmd, "@CGIA_ModifiedBy", DbType.Int32, userId);
                db.AddOutParameter(createGIAccountCmd, "@CGIA_AccountId", DbType.Int32, 10);
                if (db.ExecuteNonQuery(createGIAccountCmd) != 0)
                    accountId = int.Parse(db.GetParameterValue(createGIAccountCmd, "@CGIA_AccountId").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceDao.cs:CreateCustomerGIAccount()");


                object[] objects = new object[2];
                objects[0] = generalInsuranceVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return accountId;
        }

        public DataSet GetInsurancePolicyIssuerList()
        {
            Database db;
            DbCommand getIssuersList;
            DataSet ds = new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getIssuersList = db.GetStoredProcCommand("SP_GetInsuranceIssuerList");
                ds = db.ExecuteDataSet(getIssuersList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceDao.cs:GetInsurancePolicyIssuerList()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataTable GetCustomerPropertyList(int customerId)
        {
            Database db;
            DbCommand getPropertyList;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getPropertyList = db.GetStoredProcCommand("SP_GetCustomerPropertyList");
                db.AddInParameter(getPropertyList, "@C_CustomerId", DbType.Int32, customerId);
                ds = db.ExecuteDataSet(getPropertyList);
                if (ds != null)
                {
                    dt = ds.Tables[0];
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

                FunctionInfo.Add("Method", "InsuranceDao.cs:GetCustomerPropertyList()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public DataTable GetCustomerCollectiblesList(int customerId)
        {
            Database db;
            DbCommand getCollectiblesList;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCollectiblesList = db.GetStoredProcCommand("SP_GetCustomerCollectiblesList");
                db.AddInParameter(getCollectiblesList, "@C_CustomerId", DbType.Int32, customerId);
                ds = db.ExecuteDataSet(getCollectiblesList);
                if (ds != null)
                {
                    dt = ds.Tables[0];
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

                FunctionInfo.Add("Method", "InsuranceDao.cs:GetCustomerCollectiblesList()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public DataTable GetCustomerGoldList(int customerId)
        {
            Database db;
            DbCommand getGoldList;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGoldList = db.GetStoredProcCommand("SP_GetCustomerGoldList");
                db.AddInParameter(getGoldList, "@C_CustomerId", DbType.Int32, customerId);
                ds = db.ExecuteDataSet(getGoldList);
                if (ds != null)
                {
                    dt = ds.Tables[0];
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

                FunctionInfo.Add("Method", "InsuranceDao.cs:GetCustomerGoldList()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public DataTable GetCustomerPersonalItemsList(int customerId, string AssCatCode)
        {
            Database db;
            DbCommand getPersonalItemsList;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getPersonalItemsList = db.GetStoredProcCommand("SP_GetCustomerPersonalItemsList");
                db.AddInParameter(getPersonalItemsList, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getPersonalItemsList, "@PAISC_AssetInstrumentCategoryCode", DbType.String, AssCatCode);
                ds = db.ExecuteDataSet(getPersonalItemsList);
                if (ds != null)
                {
                    dt = ds.Tables[0];
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

                FunctionInfo.Add("Method", "InsuranceDao.cs:GetCustomerPersonalItemsList()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public DataTable GetGIPolicyType()
        {
            Database db;
            DbCommand getPolicyType;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getPolicyType = db.GetStoredProcCommand("SP_GetInsurancePolicyType");
                ds = db.ExecuteDataSet(getPolicyType);
                if (ds != null)
                {
                    dt = ds.Tables[0];
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

                FunctionInfo.Add("Method", "InsuranceDao.cs:GetGIPolicyType()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public int CreateGINetPosition(GeneralInsuranceVo generalInsuranceVo, int userId)
        {

            Database db;
            DbCommand createGINetPosition;
            int insuranceId = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createGINetPosition = db.GetStoredProcCommand("SP_CreateGINetPosition");
                db.AddInParameter(createGINetPosition, "@CGIA_AccountId", DbType.Int32, generalInsuranceVo.AccountId);
                db.AddInParameter(createGINetPosition, "@XGII_GIIssuerCode", DbType.String, generalInsuranceVo.InsIssuerCode);
                db.AddInParameter(createGINetPosition, "@CGINP_PolicyParticular", DbType.String, generalInsuranceVo.PolicyParticular);
                if (generalInsuranceVo.OriginalStartDate != DateTime.MinValue)
                    db.AddInParameter(createGINetPosition, "@CGINP_OriginalStartDate", DbType.DateTime, generalInsuranceVo.OriginalStartDate);
                else
                    db.AddInParameter(createGINetPosition, "@CGINP_OriginalStartDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(createGINetPosition, "@CGINP_IsFamilyPolicy", DbType.Int32, generalInsuranceVo.IsFamilyPolicy);
                db.AddInParameter(createGINetPosition, "@XGIPT_PolicyTypeCode", DbType.String, generalInsuranceVo.PolicyTypeCode);
                db.AddInParameter(createGINetPosition, "@CGINP_SumAssured", DbType.Double, generalInsuranceVo.SumAssured);
                db.AddInParameter(createGINetPosition, "@CGINP_TPAName", DbType.String, generalInsuranceVo.TPAName);
                db.AddInParameter(createGINetPosition, "@CGINP_TPAContactNum", DbType.Double, generalInsuranceVo.TPAContactNumber);
                db.AddInParameter(createGINetPosition, "@CGINP_IsEligibleforFreeHealth", DbType.Int32, generalInsuranceVo.IsEligibleFreeHealth);
                if (generalInsuranceVo.CheckupDate != DateTime.MinValue)
                    db.AddInParameter(createGINetPosition, "@CGINP_CheckupDate", DbType.DateTime, generalInsuranceVo.CheckupDate);
                else
                    db.AddInParameter(createGINetPosition, "@CGINP_CheckupDate", DbType.DateTime, DBNull.Value);
                if (generalInsuranceVo.ProposalDate != DateTime.MinValue)
                    db.AddInParameter(createGINetPosition, "@CGINP_ProposalDate", DbType.DateTime, generalInsuranceVo.ProposalDate);
                else
                    db.AddInParameter(createGINetPosition, "@CGINP_ProposalDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(createGINetPosition, "@CGINP_ProposalNumber", DbType.String, generalInsuranceVo.ProposalNumber);
                if (generalInsuranceVo.PolicyValidityStartDate != DateTime.MinValue)
                    db.AddInParameter(createGINetPosition, "@CGINP_PolicyValidityStartDate", DbType.DateTime, generalInsuranceVo.PolicyValidityStartDate);
                else
                    db.AddInParameter(createGINetPosition, "@CGINP_PolicyValidityStartDate", DbType.DateTime, DBNull.Value);
                if (generalInsuranceVo.PolicyValidityEndDate != DateTime.MinValue)
                    db.AddInParameter(createGINetPosition, "@CGINP_PolicyValidityEndDate", DbType.DateTime, generalInsuranceVo.PolicyValidityEndDate);
                else
                    db.AddInParameter(createGINetPosition, "@CGINP_PolicyValidityEndDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(createGINetPosition, "@CGINP_PremiumAmount", DbType.Double, generalInsuranceVo.PremiumAmount);
                db.AddInParameter(createGINetPosition, "@XF_FrequencyCode", DbType.String, generalInsuranceVo.FrequencyCode);
                db.AddInParameter(createGINetPosition, "@CGINP_Remarks", DbType.String, generalInsuranceVo.Remarks);
                db.AddInParameter(createGINetPosition, "@CGINP_IsProvidedByEmployer", DbType.Int32, generalInsuranceVo.IsProvidedByEmployer);
                db.AddInParameter(createGINetPosition, "@CGINP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createGINetPosition, "@CGINP_ModifiedBy", DbType.Int32, userId);
                db.AddOutParameter(createGINetPosition, "@GeneralInsuranceId", DbType.Int32, 100);



                if (db.ExecuteNonQuery(createGINetPosition) != 0)
                    insuranceId = int.Parse(db.GetParameterValue(createGINetPosition, "@GeneralInsuranceId").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceDao.cs:CreateGINetPosition()");
                object[] objects = new object[2];
                objects[0] = generalInsuranceVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            //   return bResult;
            return insuranceId;
        }

        public bool UpdateGINetPosition(GeneralInsuranceVo generalInsuranceVo, int userId)
        {

            Database db;
            bool bresult = false;
            DbCommand updateGINetPosition;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateGINetPosition = db.GetStoredProcCommand("SP_UpdateGINetPosition");
                db.AddInParameter(updateGINetPosition, "@CGINP_Id", DbType.Int32, generalInsuranceVo.GINetPositionId);
                db.AddInParameter(updateGINetPosition, "@CGIA_AccountId", DbType.Int32, generalInsuranceVo.AccountId);
                db.AddInParameter(updateGINetPosition, "@XGII_GIIssuerCode", DbType.String, generalInsuranceVo.InsIssuerCode);
                db.AddInParameter(updateGINetPosition, "@CGINP_PolicyParticular", DbType.String, generalInsuranceVo.PolicyParticular);
                if (generalInsuranceVo.OriginalStartDate != DateTime.MinValue)
                    db.AddInParameter(updateGINetPosition, "@CGINP_OriginalStartDate", DbType.DateTime, generalInsuranceVo.OriginalStartDate);
                else
                    db.AddInParameter(updateGINetPosition, "@CGINP_OriginalStartDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(updateGINetPosition, "@CGINP_IsFamilyPolicy", DbType.Int32, generalInsuranceVo.IsFamilyPolicy);
                db.AddInParameter(updateGINetPosition, "@XGIPT_PolicyTypeCode", DbType.String, generalInsuranceVo.PolicyTypeCode);
                db.AddInParameter(updateGINetPosition, "@CGINP_SumAssured", DbType.Double, generalInsuranceVo.SumAssured);
                db.AddInParameter(updateGINetPosition, "@CGINP_TPAName", DbType.String, generalInsuranceVo.TPAName);
                db.AddInParameter(updateGINetPosition, "@CGINP_TPAContactNum", DbType.Double, generalInsuranceVo.TPAContactNumber);
                db.AddInParameter(updateGINetPosition, "@CGINP_IsEligibleforFreeHealth", DbType.Int32, generalInsuranceVo.IsEligibleFreeHealth);
                if (generalInsuranceVo.CheckupDate != DateTime.MinValue)
                    db.AddInParameter(updateGINetPosition, "@CGINP_CheckupDate", DbType.DateTime, generalInsuranceVo.CheckupDate);
                else
                    db.AddInParameter(updateGINetPosition, "@CGINP_CheckupDate", DbType.DateTime, DBNull.Value);
                if (generalInsuranceVo.ProposalDate != DateTime.MinValue)
                    db.AddInParameter(updateGINetPosition, "@CGINP_ProposalDate", DbType.DateTime, generalInsuranceVo.ProposalDate);
                else
                    db.AddInParameter(updateGINetPosition, "@CGINP_ProposalDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(updateGINetPosition, "@CGINP_ProposalNumber", DbType.String, generalInsuranceVo.ProposalNumber);
                if (generalInsuranceVo.PolicyValidityStartDate != DateTime.MinValue)
                    db.AddInParameter(updateGINetPosition, "@CGINP_PolicyValidityStartDate", DbType.DateTime, generalInsuranceVo.PolicyValidityStartDate);
                else
                    db.AddInParameter(updateGINetPosition, "@CGINP_PolicyValidityStartDate", DbType.DateTime, DBNull.Value);
                if (generalInsuranceVo.PolicyValidityEndDate != DateTime.MinValue)
                    db.AddInParameter(updateGINetPosition, "@CGINP_PolicyValidityEndDate", DbType.DateTime, generalInsuranceVo.PolicyValidityEndDate);
                else
                    db.AddInParameter(updateGINetPosition, "@CGINP_PolicyValidityEndDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(updateGINetPosition, "@CGINP_PremiumAmount", DbType.Double, generalInsuranceVo.PremiumAmount);
                db.AddInParameter(updateGINetPosition, "@XF_FrequencyCode", DbType.String, generalInsuranceVo.FrequencyCode);
                db.AddInParameter(updateGINetPosition, "@CGINP_Remarks", DbType.String, generalInsuranceVo.Remarks);
                db.AddInParameter(updateGINetPosition, "@CGINP_IsProvidedByEmployer", DbType.Int32, generalInsuranceVo.IsProvidedByEmployer);
                db.AddInParameter(updateGINetPosition, "@CGINP_ModifiedBy", DbType.Int32, userId);

                if (db.ExecuteNonQuery(updateGINetPosition) != 0)
                    bresult = true;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceDao.cs:UpdateGINetPosition()");
                object[] objects = new object[2];
                objects[0] = generalInsuranceVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            //   return bResult;
            return bresult;
        }

        public bool CreateGIAssetAssociation(GeneralInsuranceVo generalInsuranceVo, int userId)
        {
            bool bresult = false;
            Database db;
            DbCommand createGIAssetAsscociation;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createGIAssetAsscociation = db.GetStoredProcCommand("SP_CreateGIAssetAssociation");
                db.AddInParameter(createGIAssetAsscociation, "@CGINP_Id", DbType.Int32, generalInsuranceVo.GINetPositionId);
                db.AddInParameter(createGIAssetAsscociation, "@CGIAA_AssetGroup", DbType.String, generalInsuranceVo.AssetGroup);
                db.AddInParameter(createGIAssetAsscociation, "@CGIAA_AssetTable", DbType.String, generalInsuranceVo.AssetTable);
                db.AddInParameter(createGIAssetAsscociation, "@CGIAA_AssetId", DbType.Double, generalInsuranceVo.AssetId);
                db.AddInParameter(createGIAssetAsscociation, "@CGIAA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createGIAssetAsscociation, "@CGIAA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createGIAssetAsscociation) != 0)
                    bresult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceDao.cs:CreateGIAssetAssociation()");


                object[] objects = new object[2];
                objects[0] = generalInsuranceVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bresult;
        }

        public DataTable GetCustomerGIDetails(int customerId)
        {
            Database db;
            DbCommand getGIList;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGIList = db.GetStoredProcCommand("SP_GetCustomerGIDetails");
                db.AddInParameter(getGIList, "@C_CustomerId", DbType.Int32, customerId);
                ds = db.ExecuteDataSet(getGIList);
                if (ds != null)
                {
                    dt = ds.Tables[0];
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

                FunctionInfo.Add("Method", "InsuranceDao.cs:GetCustomerGIDetails()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public GeneralInsuranceVo GetGINetPositionDetails(int insuranceId)
        {
            GeneralInsuranceVo generalInsuranceVo = null;
            Database db;
            DbCommand getGINPDetailsCmd;
            DataSet dsGetGINPDetails;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGINPDetailsCmd = db.GetStoredProcCommand("SP_GetGINetPositionDetails");
                db.AddInParameter(getGINPDetailsCmd, "@GeneralInsuranceId", DbType.Int32, insuranceId);
                dsGetGINPDetails = db.ExecuteDataSet(getGINPDetailsCmd);

                if (dsGetGINPDetails.Tables[0].Rows.Count > 0)
                {
                    generalInsuranceVo = new GeneralInsuranceVo();
                    dr = dsGetGINPDetails.Tables[0].Rows[0];
                    generalInsuranceVo.AccountId = int.Parse(dr["CGIA_AccountId"].ToString());
                    generalInsuranceVo.InsIssuerCode = dr["XGII_GIIssuerCode"].ToString();
                    generalInsuranceVo.PolicyParticular = dr["CGINP_PolicyParticular"].ToString();
                    if (dr["CGINP_OriginalStartDate"].ToString() != string.Empty)
                        generalInsuranceVo.OriginalStartDate = Convert.ToDateTime(dr["CGINP_OriginalStartDate"].ToString());
                    if (dr["CGINP_IsFamilyPolicy"].ToString() != string.Empty)
                        generalInsuranceVo.IsFamilyPolicy = int.Parse(dr["CGINP_IsFamilyPolicy"].ToString());
                    generalInsuranceVo.PolicyTypeCode = dr["XGIPT_PolicyTypeCode"].ToString();
                    if (dr["CGINP_SumAssured"].ToString() != string.Empty)
                        generalInsuranceVo.SumAssured = double.Parse(dr["CGINP_SumAssured"].ToString());
                    generalInsuranceVo.TPAName = dr["CGINP_TPAName"].ToString();
                    generalInsuranceVo.TPAContactNumber = long.Parse(dr["CGINP_TPAContactNum"].ToString());
                    if (dr["CGINP_IsEligibleforFreeHealth"].ToString() != string.Empty)
                        generalInsuranceVo.IsEligibleFreeHealth = int.Parse(dr["CGINP_IsEligibleforFreeHealth"].ToString());
                    if (dr["CGINP_CheckupDate"].ToString() != string.Empty)
                        generalInsuranceVo.CheckupDate = Convert.ToDateTime(dr["CGINP_CheckupDate"].ToString());
                    if (dr["CGINP_ProposalDate"].ToString() != string.Empty)
                        generalInsuranceVo.ProposalDate = Convert.ToDateTime(dr["CGINP_ProposalDate"].ToString());
                    generalInsuranceVo.ProposalNumber = dr["CGINP_ProposalNumber"].ToString();
                    if (dr["CGINP_PolicyValidityStartDate"].ToString() != string.Empty)
                        generalInsuranceVo.PolicyValidityStartDate = Convert.ToDateTime(dr["CGINP_PolicyValidityStartDate"].ToString());
                    if (dr["CGINP_PolicyValidityEndDate"].ToString() != string.Empty)
                        generalInsuranceVo.PolicyValidityEndDate = Convert.ToDateTime(dr["CGINP_PolicyValidityEndDate"].ToString());
                    if (dr["CGINP_PremiumAmount"].ToString() != string.Empty)
                        generalInsuranceVo.PremiumAmount = double.Parse(dr["CGINP_PremiumAmount"].ToString());
                    generalInsuranceVo.FrequencyCode = dr["XF_FrequencyCode"].ToString();
                    generalInsuranceVo.Remarks = dr["CGINP_Remarks"].ToString();
                    if (dr["CGINP_IsProvidedByEmployer"].ToString() != string.Empty)
                        generalInsuranceVo.IsProvidedByEmployer = int.Parse(dr["CGINP_IsProvidedByEmployer"].ToString());
                    generalInsuranceVo.InsIssuerCode = dr["XGII_GIIssuerCode"].ToString();
                    generalInsuranceVo.InsIssuerCode = dr["XGII_GIIssuerCode"].ToString();

                    generalInsuranceVo.PolicyNumber = dr["CGIA_PolicyNum"].ToString();
                    generalInsuranceVo.AssetInstrumentCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    generalInsuranceVo.AssetInstrumentCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                    generalInsuranceVo.AssetInstrumentSubCategoryCode = dr["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                    generalInsuranceVo.AssetInstrumentSubCategoryName = dr["PAISC_AssetInstrumentSubCategoryName"].ToString();
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

                FunctionInfo.Add("Method", "InsuranceDao.cs:GetGINetPositionDetails()");

                object[] objects = new object[1];
                objects[0] = insuranceId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return generalInsuranceVo;
        }

        public bool CreateGIAccountAssociation(CustomerAccountAssociationVo customerAccountAssociationVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand createGIAccountAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createGIAccountAssociationCmd = db.GetStoredProcCommand("SP_CreateGIAccountAssociation");
                db.AddInParameter(createGIAccountAssociationCmd, "@CGIA_AccountId", DbType.Int32, customerAccountAssociationVo.AccountId);
                db.AddInParameter(createGIAccountAssociationCmd, "@CA_AssociationId", DbType.Int32, customerAccountAssociationVo.AssociationId);
                db.AddInParameter(createGIAccountAssociationCmd, "@CGIAA_AssociationType", DbType.String, customerAccountAssociationVo.AssociationType);
                db.AddInParameter(createGIAccountAssociationCmd, "@CGIAA_SumAssured", DbType.Int32, customerAccountAssociationVo.NomineeShare);
                db.AddInParameter(createGIAccountAssociationCmd, "@CGIAA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createGIAccountAssociationCmd, "@CGIAA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createGIAccountAssociationCmd) != 0)
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

                FunctionInfo.Add("Method", "InsuranceDao.cs:CreateGIAccountAssociation()");

                object[] objects = new object[2];
                objects[0] = customerAccountAssociationVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public DataTable GetGIAccountAssociation(int accountId)
        {
            Database db;
            DbCommand getGIAccountAssociation;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGIAccountAssociation = db.GetStoredProcCommand("SP_GetCustomerGIAccountAssociation");
                db.AddInParameter(getGIAccountAssociation, "@CGIA_AccountId", DbType.Int32, accountId);
                ds = db.ExecuteDataSet(getGIAccountAssociation);
                if (ds != null)
                {
                    dt = ds.Tables[0];
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

                FunctionInfo.Add("Method", "InsuranceDao.cs:GetGIAccountAssociation()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public bool DeleteGIAccountAssociation(int accountId)
        {
            Database db;
            DbCommand delGIAccountAssociation;
            bool bresult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                delGIAccountAssociation = db.GetStoredProcCommand("SP_DeleteCustomerGIAccountAssociation");
                db.AddInParameter(delGIAccountAssociation, "@CGIA_AccountId", DbType.Int32, accountId);

                if (db.ExecuteNonQuery(delGIAccountAssociation) != 0)
                    bresult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceDao.cs:DeleteGIAccountAssociation()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bresult;
        }

        public DataTable GetGIAssetAssociation(int insuranceId)
        {
            Database db;
            DbCommand getGIAssetAssociation;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGIAssetAssociation = db.GetStoredProcCommand("SP_GetCustomerGIAssetAssociation");
                db.AddInParameter(getGIAssetAssociation, "@CGINP_Id", DbType.Int32, insuranceId);
                ds = db.ExecuteDataSet(getGIAssetAssociation);
                if (ds != null)
                {
                    dt = ds.Tables[0];
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

                FunctionInfo.Add("Method", "InsuranceDao.cs:GetGIAssetAssociation()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public bool DeleteGIAssetAssociation(int insuranceId)
        {
            Database db;
            DbCommand delGIAssetAssociation;
            bool bresult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                delGIAssetAssociation = db.GetStoredProcCommand("SP_DeleteCustomerGIAssetAssociation");
                db.AddInParameter(delGIAssetAssociation, "@CGINP_Id", DbType.Int32, insuranceId);

                if (db.ExecuteNonQuery(delGIAssetAssociation) != 0)
                    bresult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceDao.cs:DeleteGIAssetAssociation()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bresult;
        }

        public DataSet GetGIIssuerList()
        {
            Database db;
            DbCommand getIssuersList;
            DataSet ds = new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getIssuersList = db.GetStoredProcCommand("SP_GetGIIssuerList");
                ds = db.ExecuteDataSet(getIssuersList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceDao.cs:GetGIIssuerList()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
    }

}
