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
    public class PensionAndGratuitiesDao
    {
        public bool CreatePensionAndGratuities(PensionAndGratuitiesVo pensionAndGratuitiesVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand createPensionAndGratuitiesCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createPensionAndGratuitiesCmd = db.GetStoredProcCommand("SP_CreatePensionAndGratuities");
                db.AddInParameter(createPensionAndGratuitiesCmd, "@CPGA_AccountId", DbType.Int32, pensionAndGratuitiesVo.AccountId);
                db.AddInParameter(createPensionAndGratuitiesCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, pensionAndGratuitiesVo.AssetInstrumentCategoryCode);
                db.AddInParameter(createPensionAndGratuitiesCmd, "@PAG_AssetGroupCode", DbType.String, pensionAndGratuitiesVo.AssetGroupCode);
                db.AddInParameter(createPensionAndGratuitiesCmd, "@CPGNP_OrganizationName", DbType.String, pensionAndGratuitiesVo.OrganizationName);
                db.AddInParameter(createPensionAndGratuitiesCmd, "@CPGNP_DepositAmount", DbType.Double, pensionAndGratuitiesVo.DepositAmount);
                if (pensionAndGratuitiesVo.FiscalYearCode != "")
                    db.AddInParameter(createPensionAndGratuitiesCmd, "@XFY_FiscalYearCode", DbType.String, pensionAndGratuitiesVo.FiscalYearCode);
                else
                    db.AddInParameter(createPensionAndGratuitiesCmd, "@XFY_FiscalYearCode", DbType.String, DBNull.Value);
                db.AddInParameter(createPensionAndGratuitiesCmd, "@CPGNP_EmployeeContri", DbType.Double, pensionAndGratuitiesVo.EmployeeContribution);
                db.AddInParameter(createPensionAndGratuitiesCmd, "@CPGNP_EmployerContri", DbType.Double, pensionAndGratuitiesVo.EmployerContribution);
                db.AddInParameter(createPensionAndGratuitiesCmd, "@CPGNP_InterestRate", DbType.Double, pensionAndGratuitiesVo.InterestRate);
                if (pensionAndGratuitiesVo.InterestBasis != "")
                    db.AddInParameter(createPensionAndGratuitiesCmd, "@XIB_InterestBasisCode", DbType.String, pensionAndGratuitiesVo.InterestBasis);
                else
                    db.AddInParameter(createPensionAndGratuitiesCmd, "@XIB_InterestBasisCode", DbType.String, DBNull.Value);
                if (pensionAndGratuitiesVo.CompoundInterestFrequencyCode != "")
                    db.AddInParameter(createPensionAndGratuitiesCmd, "@XF_CompoundInterestFrequencyCode", DbType.String, pensionAndGratuitiesVo.CompoundInterestFrequencyCode);
                else
                    db.AddInParameter(createPensionAndGratuitiesCmd, "@XF_CompoundInterestFrequencyCode", DbType.String, DBNull.Value);

                if (pensionAndGratuitiesVo.InterestPayableFrequencyCode != "")
                    db.AddInParameter(createPensionAndGratuitiesCmd, "@XF_InterestPayableFrequencyCode", DbType.String, pensionAndGratuitiesVo.InterestPayableFrequencyCode);
                else
                    db.AddInParameter(createPensionAndGratuitiesCmd, "@XF_InterestPayableFrequencyCode", DbType.String, DBNull.Value);
                
                db.AddInParameter(createPensionAndGratuitiesCmd, "@CPGNP_IsInterestAccumalated", DbType.Int16, pensionAndGratuitiesVo.IsInterestAccumalated);
                db.AddInParameter(createPensionAndGratuitiesCmd, "@CPGNP_InterestAmtAccumalated", DbType.Double, pensionAndGratuitiesVo.InterestAmtAccumalated);
                db.AddInParameter(createPensionAndGratuitiesCmd, "@CPGNP_InterestAmtPaidOut", DbType.Double, pensionAndGratuitiesVo.InterestAmtPaidOut);
                db.AddInParameter(createPensionAndGratuitiesCmd, "@CPGNP_CurrentValue", DbType.Double, pensionAndGratuitiesVo.CurrentValue);
                db.AddInParameter(createPensionAndGratuitiesCmd, "@CPGNP_Remark", DbType.String, pensionAndGratuitiesVo.Remarks);
                db.AddInParameter(createPensionAndGratuitiesCmd, "@CPGNP_CreatedBy", DbType.Int32, userId);
                
                if(db.ExecuteNonQuery(createPensionAndGratuitiesCmd) != 0)
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
                FunctionInfo.Add("Method", "PensionAndGratuitiesDao.cs:CreatePensionAndGratuities()");
                object[] objects = new object[2];
                objects[0] = pensionAndGratuitiesVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public bool UpdatePensionAndGratuities(PensionAndGratuitiesVo pensionAndGratuitiesVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updatePensionAndGratuitiesCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updatePensionAndGratuitiesCmd = db.GetStoredProcCommand("SP_UpdatePensionAndGratuities");

                db.AddInParameter(updatePensionAndGratuitiesCmd, "@CPGNP_PensionGratutiesNPId", DbType.Int32, pensionAndGratuitiesVo.PensionGratuitiesPortfolioId);

                db.AddInParameter(updatePensionAndGratuitiesCmd, "@CPGNP_OrganizationName", DbType.String, pensionAndGratuitiesVo.OrganizationName);
                db.AddInParameter(updatePensionAndGratuitiesCmd, "@CPGNP_DepositAmount", DbType.Double, pensionAndGratuitiesVo.DepositAmount);
                if (pensionAndGratuitiesVo.FiscalYearCode != "")
                    db.AddInParameter(updatePensionAndGratuitiesCmd, "@XFY_FiscalYearCode", DbType.String, pensionAndGratuitiesVo.FiscalYearCode);
                else
                    db.AddInParameter(updatePensionAndGratuitiesCmd, "@XFY_FiscalYearCode", DbType.String, DBNull.Value);
                db.AddInParameter(updatePensionAndGratuitiesCmd, "@CPGNP_EmployeeContri", DbType.Double, pensionAndGratuitiesVo.EmployeeContribution);
                db.AddInParameter(updatePensionAndGratuitiesCmd, "@CPGNP_EmployerContri", DbType.Double, pensionAndGratuitiesVo.EmployerContribution);
                db.AddInParameter(updatePensionAndGratuitiesCmd, "@CPGNP_InterestRate", DbType.Double, pensionAndGratuitiesVo.InterestRate);
                if (pensionAndGratuitiesVo.InterestBasis != "")
                    db.AddInParameter(updatePensionAndGratuitiesCmd, "@XIB_InterestBasisCode", DbType.String, pensionAndGratuitiesVo.InterestBasis);
                else
                    db.AddInParameter(updatePensionAndGratuitiesCmd, "@XIB_InterestBasisCode", DbType.String, DBNull.Value);
                if (pensionAndGratuitiesVo.CompoundInterestFrequencyCode != "")
                    db.AddInParameter(updatePensionAndGratuitiesCmd, "@XF_CompoundInterestFrequencyCode", DbType.String, pensionAndGratuitiesVo.CompoundInterestFrequencyCode);
                else
                    db.AddInParameter(updatePensionAndGratuitiesCmd, "@XF_CompoundInterestFrequencyCode", DbType.String, DBNull.Value);
                if (pensionAndGratuitiesVo.InterestPayableFrequencyCode != "")
                    db.AddInParameter(updatePensionAndGratuitiesCmd, "@XF_InterestPayableFrequencyCode", DbType.String, pensionAndGratuitiesVo.InterestPayableFrequencyCode);
                else
                    db.AddInParameter(updatePensionAndGratuitiesCmd, "@XF_InterestPayableFrequencyCode", DbType.String, DBNull.Value);
                db.AddInParameter(updatePensionAndGratuitiesCmd, "@CPGNP_IsInterestAccumalated", DbType.Int16, pensionAndGratuitiesVo.IsInterestAccumalated);
                db.AddInParameter(updatePensionAndGratuitiesCmd, "@CPGNP_InterestAmtAccumalated", DbType.Double, pensionAndGratuitiesVo.InterestAmtAccumalated);
                db.AddInParameter(updatePensionAndGratuitiesCmd, "@CPGNP_InterestAmtPaidOut", DbType.Double, pensionAndGratuitiesVo.InterestAmtPaidOut);
                db.AddInParameter(updatePensionAndGratuitiesCmd, "@CPGNP_CurrentValue", DbType.Double, pensionAndGratuitiesVo.CurrentValue);
                db.AddInParameter(updatePensionAndGratuitiesCmd, "@CPGNP_Remark", DbType.String, pensionAndGratuitiesVo.Remarks);

                db.AddInParameter(updatePensionAndGratuitiesCmd, "@CPGNP_ModifiedBy", DbType.Int32, userId);
                if( db.ExecuteNonQuery(updatePensionAndGratuitiesCmd)!=0)
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
                FunctionInfo.Add("Method", "PensionAndGratuitiesDao.cs:UpdatePensionAndGratuities()");
                object[] objects = new object[2];
                objects[0] = pensionAndGratuitiesVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool DeletePensionAndGratuitiesPortfolio(int pensionId, int accountId)
        {
            bool bResult = false;
            Database db;
            DbCommand deletePensionAndGratuitiesCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deletePensionAndGratuitiesCmd = db.GetStoredProcCommand("SP_DeletePensionGratuitiesNetPostion");

                db.AddInParameter(deletePensionAndGratuitiesCmd, "@CPGNP_PensionGratutiesNPId", DbType.Int32, pensionId);
                db.AddInParameter(deletePensionAndGratuitiesCmd, "@CPGA_AccountId", DbType.Int32, accountId);
                
              if(  db.ExecuteNonQuery(deletePensionAndGratuitiesCmd)!=0)
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
                FunctionInfo.Add("Method", "PensionAndGratuitiesDao.cs:DeletePensionAndGratuitiesPortfolio()");
                object[] objects = new object[2];
                objects[0] = pensionId;
                objects[1] = accountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public List<PensionAndGratuitiesVo> GetPensionAndGratuitiesList(int portfolioId,int  CurrentPage, string SortOrder, out int count)
        {
            PensionAndGratuitiesVo pensionAndGratuitiesVo;
            List<PensionAndGratuitiesVo> pensionAndGratuitiesList = null;
            Database db;
            DbCommand getPensionAndGratuitiesListCmd;
            DataSet pensionAndGratuitiesDs;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getPensionAndGratuitiesListCmd = db.GetStoredProcCommand("SP_GetPensionAndGratuitiesList");
                db.AddInParameter(getPensionAndGratuitiesListCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getPensionAndGratuitiesListCmd, "@CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getPensionAndGratuitiesListCmd, "@SortOrder", DbType.String, SortOrder);
                pensionAndGratuitiesDs = db.ExecuteDataSet(getPensionAndGratuitiesListCmd);
                if (pensionAndGratuitiesDs.Tables[1] != null && pensionAndGratuitiesDs.Tables[1].Rows.Count > 0)
                    count = Int32.Parse(pensionAndGratuitiesDs.Tables[1].Rows[0][0].ToString());
                else
                    count = 0;
                if (pensionAndGratuitiesDs.Tables[0].Rows.Count > 0)
                {
                    pensionAndGratuitiesList = new List<PensionAndGratuitiesVo>();
                    foreach (DataRow dr in pensionAndGratuitiesDs.Tables[0].Rows)
                    {
                        pensionAndGratuitiesVo = new PensionAndGratuitiesVo();

                        pensionAndGratuitiesVo.PensionGratuitiesPortfolioId = Int32.Parse(dr["CPGNP_PensionGratutiesNPId"].ToString());
                        pensionAndGratuitiesVo.AccountId = int.Parse(dr["CPGA_AccountId"].ToString());
                        pensionAndGratuitiesVo.AssetInstrumentCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        pensionAndGratuitiesVo.AssetInstrumentCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        pensionAndGratuitiesVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                        pensionAndGratuitiesVo.FiscalYearCode = dr["XFY_FiscalYearCode"].ToString();
                        if (dr["CPGNP_EmployeeContri"].ToString() != "")
                            pensionAndGratuitiesVo.EmployeeContribution = float.Parse(dr["CPGNP_EmployeeContri"].ToString());
                        if (dr["CPGNP_EmployerContri"].ToString() != "")
                            pensionAndGratuitiesVo.EmployerContribution = float.Parse(dr["CPGNP_EmployerContri"].ToString());
                        pensionAndGratuitiesVo.InterestPayableFrequencyCode = dr["XF_InterestPayableFrequencyCode"].ToString();
                        pensionAndGratuitiesVo.CompoundInterestFrequencyCode = dr["XF_CompoundInterestFrequencyCode"].ToString();
                        pensionAndGratuitiesVo.InterestRate = float.Parse(dr["CPGNP_InterestRate"].ToString());
                        pensionAndGratuitiesVo.OrganizationName = dr["CPGNP_OrganizationName"].ToString();
                        pensionAndGratuitiesVo.DepositAmount = float.Parse(dr["CPGNP_DepositAmount"].ToString());
                        pensionAndGratuitiesVo.CurrentValue = float.Parse(dr["CPGNP_CurrentValue"].ToString());
                        pensionAndGratuitiesVo.InterestBasis = dr["XIB_InterestBasisCode"].ToString();
                        pensionAndGratuitiesVo.IsInterestAccumalated = int.Parse(dr["CPGNP_IsInterestAccumalated"].ToString());
                        pensionAndGratuitiesVo.InterestAmtAccumalated = float.Parse(dr["CPGNP_InterestAmtAccumalated"].ToString());
                        pensionAndGratuitiesVo.InterestAmtPaidOut = float.Parse(dr["CPGNP_InterestAmtPaidOut"].ToString());
                        pensionAndGratuitiesVo.Remarks = dr["CPGNP_Remark"].ToString();
                        pensionAndGratuitiesVo.AssetInstrumentCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();

                        pensionAndGratuitiesList.Add(pensionAndGratuitiesVo);
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
                FunctionInfo.Add("Method", "PensionAndGratuitiesDao.cs:GetPensionAndGratuities()");

                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return pensionAndGratuitiesList;
        }

        public PensionAndGratuitiesVo GetPensionAndGratuities(int portfolioId)
        {
            PensionAndGratuitiesVo pensionAndGratuitiesVo = null;
            Database db;
            DbCommand getPensionAndGratuitiesCmd;
            DataSet getPensionAndGratuitiesDs;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getPensionAndGratuitiesCmd = db.GetStoredProcCommand("SP_GetPensionAndGratuities");
                db.AddInParameter(getPensionAndGratuitiesCmd, "@CPGNP_PensionGratutiesNPId", DbType.Int32, portfolioId);
                getPensionAndGratuitiesDs = db.ExecuteDataSet(getPensionAndGratuitiesCmd);
                if (getPensionAndGratuitiesDs.Tables[0].Rows.Count > 0)
                {
                    pensionAndGratuitiesVo = new PensionAndGratuitiesVo();
                    dr = getPensionAndGratuitiesDs.Tables[0].Rows[0];

                    pensionAndGratuitiesVo.PensionGratuitiesPortfolioId = Int32.Parse(dr["CPGNP_PensionGratutiesNPId"].ToString());
                    pensionAndGratuitiesVo.AccountId = int.Parse(dr["CPGA_AccountId"].ToString());
                    pensionAndGratuitiesVo.AssetInstrumentCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    pensionAndGratuitiesVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();

                    pensionAndGratuitiesVo.FiscalYearCode = dr["XFY_FiscalYearCode"].ToString();
                    pensionAndGratuitiesVo.InterestPayableFrequencyCode = dr["XF_InterestPayableFrequencyCode"].ToString();
                    pensionAndGratuitiesVo.CompoundInterestFrequencyCode = dr["XF_CompoundInterestFrequencyCode"].ToString();
                    pensionAndGratuitiesVo.InterestRate = float.Parse(dr["CPGNP_InterestRate"].ToString());
                    pensionAndGratuitiesVo.OrganizationName = dr["CPGNP_OrganizationName"].ToString();
                    pensionAndGratuitiesVo.DepositAmount = float.Parse(dr["CPGNP_DepositAmount"].ToString());
                    pensionAndGratuitiesVo.CurrentValue = float.Parse(dr["CPGNP_CurrentValue"].ToString());
                    pensionAndGratuitiesVo.InterestBasis = dr["XIB_InterestBasisCode"].ToString();
                    pensionAndGratuitiesVo.IsInterestAccumalated = int.Parse(dr["CPGNP_IsInterestAccumalated"].ToString());
                    pensionAndGratuitiesVo.InterestAmtAccumalated = float.Parse(dr["CPGNP_InterestAmtAccumalated"].ToString());
                    pensionAndGratuitiesVo.InterestAmtPaidOut = float.Parse(dr["CPGNP_InterestAmtPaidOut"].ToString());

                    if (dr["CPGNP_EmployeeContri"].ToString() != "")
                        pensionAndGratuitiesVo.EmployeeContribution = float.Parse(dr["CPGNP_EmployeeContri"].ToString());
                    else
                        pensionAndGratuitiesVo.EmployeeContribution = 0;
                    if (dr["CPGNP_EmployerContri"].ToString() != "")
                        pensionAndGratuitiesVo.EmployerContribution = float.Parse(dr["CPGNP_EmployerContri"].ToString());
                    else
                        pensionAndGratuitiesVo.EmployerContribution = 0;
                    if (dr["CPGNP_Remark"].ToString() != "")
                        pensionAndGratuitiesVo.Remarks = dr["CPGNP_Remark"].ToString();
                    else
                        pensionAndGratuitiesVo.Remarks = "";

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
                FunctionInfo.Add("Method", "PensionAndGratuitiesDao.cs:GetPensionAndGratuities()");

                object[] objects = new object[1];
                objects[0] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return pensionAndGratuitiesVo;
        }
        public bool UpdatePensionandGratuitiesAccount(CustomerAccountsVo pensionAndGratuitiesAccVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateGovtSavingsAccCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateGovtSavingsAccCmd = db.GetStoredProcCommand("SP_UpdatePensionGratuitiesAccount");
                db.AddInParameter(updateGovtSavingsAccCmd, "@CPGA_AccountId", DbType.Int32, pensionAndGratuitiesAccVo.AccountId);
                db.AddInParameter(updateGovtSavingsAccCmd, "@CPGA_AccountNum", DbType.String, pensionAndGratuitiesAccVo.AccountNum);
                db.AddInParameter(updateGovtSavingsAccCmd, "@CPGA_AccountSource", DbType.String, pensionAndGratuitiesAccVo.AccountSource);
                db.AddInParameter(updateGovtSavingsAccCmd, "@XMOH_ModeOfHoldingCode", DbType.String, pensionAndGratuitiesAccVo.ModeOfHolding);
                if (pensionAndGratuitiesAccVo.AccountOpeningDate != DateTime.MinValue)
                    db.AddInParameter(updateGovtSavingsAccCmd, "@CPGA_AccountOpeningDate", DbType.Date, pensionAndGratuitiesAccVo.AccountOpeningDate);
                db.AddInParameter(updateGovtSavingsAccCmd, "@CPGA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(updateGovtSavingsAccCmd) == 1)
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
                FunctionInfo.Add("Method", "PensionAndGratuitiesDao.cs:UpdatePensionandGratuitiesAccount()");
                object[] objects = new object[2];
                objects[0] = pensionAndGratuitiesAccVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

    }
}
