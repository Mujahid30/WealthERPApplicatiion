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
    public class GovtSavingsDao
    {
        public bool CreateGovtSavingNetPosition(GovtSavingsVo govtSavingsVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand createGovtSavingsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createGovtSavingsCmd = db.GetStoredProcCommand("SP_CreateGovtSavingNetPosition");
                db.AddInParameter(createGovtSavingsCmd, "@CGSA_AccountId", DbType.Int32, govtSavingsVo.AccountId);

                if (govtSavingsVo.AssetInstrumentCategoryCode != "0")
                    db.AddInParameter(createGovtSavingsCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, govtSavingsVo.AssetInstrumentCategoryCode);
                if (govtSavingsVo.AssetGroupCode != "0")
                    db.AddInParameter(createGovtSavingsCmd, "@PAG_AssetGroupCode", DbType.String, govtSavingsVo.AssetGroupCode);
                if (govtSavingsVo.DebtIssuerCode != "0")
                    db.AddInParameter(createGovtSavingsCmd, "@XDI_DebtIssuerCode", DbType.String, govtSavingsVo.DebtIssuerCode);
                if (govtSavingsVo.InterestBasisCode != "0")
                    db.AddInParameter(createGovtSavingsCmd, "@XIB_InterestBasisCode", DbType.String, govtSavingsVo.InterestBasisCode);
                db.AddInParameter(createGovtSavingsCmd, "@XF_InterestPayableFrequencyCode", DbType.String, govtSavingsVo.InterestPayableFrequencyCode);
                if (govtSavingsVo.CompoundInterestFrequencyCode != "0")
                    db.AddInParameter(createGovtSavingsCmd, "@XF_CompoundInterestFrequencyCode", DbType.String, govtSavingsVo.CompoundInterestFrequencyCode);

                db.AddInParameter(createGovtSavingsCmd, "@CGSNP_InterestRate", DbType.Decimal, govtSavingsVo.InterestRate);
                db.AddInParameter(createGovtSavingsCmd, "@CGSNP_Name", DbType.String, govtSavingsVo.Name);
                if (govtSavingsVo.PurchaseDate != DateTime.MinValue)
                    db.AddInParameter(createGovtSavingsCmd, "@CGSNP_PurchaseDate", DbType.Date, govtSavingsVo.PurchaseDate);
                db.AddInParameter(createGovtSavingsCmd, "@CGSNP_DepositAmount", DbType.Decimal, govtSavingsVo.DepositAmt);
                if (govtSavingsVo.MaturityDate != DateTime.MinValue)
                    db.AddInParameter(createGovtSavingsCmd, "@CGSNP_MaturityDate", DbType.Date, govtSavingsVo.MaturityDate);
                db.AddInParameter(createGovtSavingsCmd, "@CGSNP_MaturityValue", DbType.Double, govtSavingsVo.MaturityValue);
                db.AddInParameter(createGovtSavingsCmd, "@CGSNP_CurrentValue", DbType.Double, govtSavingsVo.CurrentValue);
                db.AddInParameter(createGovtSavingsCmd, "@CGSNP_IsInterestAccumalated", DbType.Int16, govtSavingsVo.IsInterestAccumalated);
                db.AddInParameter(createGovtSavingsCmd, "@CGSNP_InterestAmtAccumalated", DbType.Decimal, govtSavingsVo.InterestAmtAccumalated);
                db.AddInParameter(createGovtSavingsCmd, "@CGSNP_InterestAmtPaidOut", DbType.Decimal, govtSavingsVo.InterestAmtPaidOut);

                db.AddInParameter(createGovtSavingsCmd, "@CGSNP_SubsqntDepositAmount", DbType.Decimal, govtSavingsVo.SubsqntDepositAmount);

                if (govtSavingsVo.SubsqntDepositDate != DateTime.MinValue)
                    db.AddInParameter(createGovtSavingsCmd, "@CGSNP_SubsqntDepositDate", DbType.Date, govtSavingsVo.SubsqntDepositDate);

                if (govtSavingsVo.DepositFrequencyCode != null && govtSavingsVo.DepositFrequencyCode != string.Empty)
                    db.AddInParameter(createGovtSavingsCmd, "@XF_DepositFrequencyCode", DbType.String, govtSavingsVo.DepositFrequencyCode);

                db.AddInParameter(createGovtSavingsCmd, "@CGSNP_Remark", DbType.String, govtSavingsVo.Remarks);
                db.AddInParameter(createGovtSavingsCmd, "@CGSNP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createGovtSavingsCmd, "@CGSNP_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createGovtSavingsCmd) == 1)
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
                FunctionInfo.Add("Method", "GovtSavingsDao.cs:CreateGovtSavingNetPosition()");
                object[] objects = new object[2];
                objects[0] = govtSavingsVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public List<GovtSavingsVo> GetGovtSavingsNPList(int portfolioId, int CurrentPage, string sortOrder, out int count)
        {
            GovtSavingsVo govtSavingsVo;
            List<GovtSavingsVo> govtSavingsList = null;
            Database db;
            DbCommand getGovtSavingsListCmd;
            DataSet dsGovtSavings;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGovtSavingsListCmd = db.GetStoredProcCommand("SP_GetGovtSavingsNetPosition");
                db.AddInParameter(getGovtSavingsListCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getGovtSavingsListCmd, "@CurrentPage", DbType.Int16, CurrentPage);
                db.AddInParameter(getGovtSavingsListCmd, "@SortOrder", DbType.String, sortOrder);
                dsGovtSavings = db.ExecuteDataSet(getGovtSavingsListCmd);
                if (dsGovtSavings.Tables[1] != null && dsGovtSavings.Tables[1].Rows.Count > 0)
                    count = Int32.Parse(dsGovtSavings.Tables[1].Rows[0][0].ToString());
                else
                    count = 0;
                if (dsGovtSavings.Tables[0].Rows.Count > 0)
                {
                    govtSavingsList = new List<GovtSavingsVo>();
                    foreach (DataRow dr in dsGovtSavings.Tables[0].Rows)
                    {
                        govtSavingsVo = new GovtSavingsVo();
                        govtSavingsVo.AccountId = int.Parse(dr["CGSA_AccountId"].ToString());
                        govtSavingsVo.GoveSavingsPortfolioId = int.Parse(dr["CGSNP_GovtSavingNPId"].ToString());
                        govtSavingsVo.AssetInstrumentCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        govtSavingsVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                        govtSavingsVo.DebtIssuerCode = dr["XDI_DebtIssuerCode"].ToString();
                        govtSavingsVo.InterestPayableFrequencyCode = dr["XF_InterestPayableFrequencyCode"].ToString();
                        govtSavingsVo.CompoundInterestFrequencyCode = dr["XF_CompoundInterestFrequencyCode"].ToString();
                        if (dr["CGSNP_InterestRate"] != DBNull.Value)
                            govtSavingsVo.InterestRate = float.Parse(dr["CGSNP_InterestRate"].ToString());
                        govtSavingsVo.Name = dr["CGSNP_Name"].ToString();
                        //govtSavingsVo.Quantity = float.Parse(dr["CGSNP_Quantity"].ToString());
                        if (dr["CGSNP_PurchaseDate"] != DBNull.Value)
                            govtSavingsVo.PurchaseDate = DateTime.Parse(dr["CGSNP_PurchaseDate"].ToString());
                        if (dr["CGSNP_DepositAmount"] != DBNull.Value)
                            govtSavingsVo.DepositAmt = float.Parse(dr["CGSNP_DepositAmount"].ToString());
                        if (dr["CGSNP_MaturityDate"] != DBNull.Value)
                            govtSavingsVo.MaturityDate = DateTime.Parse(dr["CGSNP_MaturityDate"].ToString());
                        govtSavingsVo.MaturityValue = float.Parse(dr["CGSNP_MaturityValue"].ToString());
                        govtSavingsVo.CurrentValue = float.Parse(dr["CGSNP_CurrentValue"].ToString());
                        govtSavingsVo.InterestBasisCode = dr["XIB_InterestBasisCode"].ToString();
                        govtSavingsVo.IsInterestAccumalated = int.Parse(dr["CGSNP_IsInterestAccumalated"].ToString());
                        govtSavingsVo.InterestAmtAccumalated = float.Parse(dr["CGSNP_InterestAmtAccumalated"].ToString());
                        govtSavingsVo.InterestAmtPaidOut = float.Parse(dr["CGSNP_InterestAmtPaidOut"].ToString());
                        govtSavingsVo.AssetInstrumentCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        govtSavingsList.Add(govtSavingsVo);
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
                FunctionInfo.Add("Method", "GovtSavingsDao.cs:GetGovtSavingsNPList()");


                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return govtSavingsList;




        }

        public bool UpdateGovtSavingsNP(GovtSavingsVo govtSavingsVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateGovtSavingsPortfolioCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateGovtSavingsPortfolioCmd = db.GetStoredProcCommand("SP_UpdateGovtSavingsNP");
                db.AddInParameter(updateGovtSavingsPortfolioCmd, "@CGSNP_GovtSavingNPId", DbType.Int32, govtSavingsVo.GoveSavingsPortfolioId);
                if (govtSavingsVo.DebtIssuerCode != "0")
                    db.AddInParameter(updateGovtSavingsPortfolioCmd, "@XDI_DebtIssuerCode", DbType.String, govtSavingsVo.DebtIssuerCode);
                if (govtSavingsVo.InterestBasisCode != "0")
                    db.AddInParameter(updateGovtSavingsPortfolioCmd, "@XIB_InterestBasisCode", DbType.String, govtSavingsVo.InterestBasisCode);
                if (govtSavingsVo.CompoundInterestFrequencyCode != "0")
                    db.AddInParameter(updateGovtSavingsPortfolioCmd, "@XF_CompoundInterestFrequencyCode", DbType.String, govtSavingsVo.CompoundInterestFrequencyCode);

                db.AddInParameter(updateGovtSavingsPortfolioCmd, "@XF_InterestPayableFrequencyCode", DbType.String, govtSavingsVo.InterestPayableFrequencyCode);
                db.AddInParameter(updateGovtSavingsPortfolioCmd, "@CGSNP_Name", DbType.String, govtSavingsVo.Name);
                if (govtSavingsVo.PurchaseDate != DateTime.MinValue)
                    db.AddInParameter(updateGovtSavingsPortfolioCmd, "@CGSNP_PurchaseDate", DbType.Date, govtSavingsVo.PurchaseDate);
                db.AddInParameter(updateGovtSavingsPortfolioCmd, "@CGSNP_CurrentValue", DbType.Double, govtSavingsVo.CurrentValue);
                if (govtSavingsVo.MaturityDate != DateTime.MinValue)
                    db.AddInParameter(updateGovtSavingsPortfolioCmd, "@CGSNP_MaturityDate", DbType.Date, govtSavingsVo.MaturityDate);
                db.AddInParameter(updateGovtSavingsPortfolioCmd, "@CGSNP_DepositAmount", DbType.Double, govtSavingsVo.DepositAmt);
                db.AddInParameter(updateGovtSavingsPortfolioCmd, "@CGSNP_MaturityValue", DbType.Double, govtSavingsVo.MaturityValue);
                db.AddInParameter(updateGovtSavingsPortfolioCmd, "@CGSNP_IsInterestAccumalated", DbType.Int32, govtSavingsVo.IsInterestAccumalated);
                db.AddInParameter(updateGovtSavingsPortfolioCmd, "@CGSNP_InterestAmtAccumalated", DbType.Double, govtSavingsVo.InterestAmtAccumalated);
                db.AddInParameter(updateGovtSavingsPortfolioCmd, "@CGSNP_InterestAmtPaidOut", DbType.Double, govtSavingsVo.InterestAmtPaidOut);
                db.AddInParameter(updateGovtSavingsPortfolioCmd, "@CGSNP_InterestRate", DbType.Double, govtSavingsVo.InterestRate);

                db.AddInParameter(updateGovtSavingsPortfolioCmd, "@CGSNP_SubsqntDepositAmount", DbType.Decimal, govtSavingsVo.SubsqntDepositAmount);

                if (govtSavingsVo.SubsqntDepositDate != DateTime.MinValue)
                    db.AddInParameter(updateGovtSavingsPortfolioCmd, "@CGSNP_SubsqntDepositDate", DbType.Date, govtSavingsVo.SubsqntDepositDate);

                if (govtSavingsVo.DepositFrequencyCode != null && govtSavingsVo.DepositFrequencyCode != string.Empty)
                    db.AddInParameter(updateGovtSavingsPortfolioCmd, "@XF_DepositFrequencyCode", DbType.String, govtSavingsVo.DepositFrequencyCode);

                db.AddInParameter(updateGovtSavingsPortfolioCmd, "@CGSNP_Remark", DbType.String, govtSavingsVo.Remarks);
                db.AddInParameter(updateGovtSavingsPortfolioCmd, "@CGSNP_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(updateGovtSavingsPortfolioCmd) == 1)
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
                FunctionInfo.Add("Method", "GovtSavingsDao.cs:UpdateGovtSavingsPortfolio()");
                object[] objects = new object[2];
                objects[0] = govtSavingsVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool DeleteGovtSavingsPortfolio(int govtSavingsId)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteGovtSavingsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteGovtSavingsCmd = db.GetStoredProcCommand("SP_DeleteGovtSavingsNetPostion");

                db.AddInParameter(deleteGovtSavingsCmd, "@CGSNP_GovtSavingNPId", DbType.Int32, govtSavingsId);
                //db.AddInParameter(deleteGovtSavingsCmd, "@CGSA_AccountId", DbType.Int32, accountId);

                if (db.ExecuteNonQuery(deleteGovtSavingsCmd) != 0)
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
                FunctionInfo.Add("Method", "GovtSavingsDao.cs:DeleteGovtSavingsPortfolio()");
                object[] objects = new object[1];
                objects[0] = govtSavingsId;
                //objects[1] = accountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool UpdateGovtSavingsAccount(CustomerAccountsVo govtSavingsAccVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateGovtSavingsAccCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateGovtSavingsAccCmd = db.GetStoredProcCommand("SP_UpdateGovtSavingsAccount");
                db.AddInParameter(updateGovtSavingsAccCmd, "@CGSA_AccountId", DbType.Int32, govtSavingsAccVo.AccountId);
                db.AddInParameter(updateGovtSavingsAccCmd, "@CGSA_AccountNum", DbType.String, govtSavingsAccVo.AccountNum);
                db.AddInParameter(updateGovtSavingsAccCmd, "@CGSA_AccountSource", DbType.String, govtSavingsAccVo.AccountSource);
                db.AddInParameter(updateGovtSavingsAccCmd, "@XMOH_ModeOfHoldingCode", DbType.String, govtSavingsAccVo.ModeOfHolding);
                if (govtSavingsAccVo.AccountOpeningDate != DateTime.MinValue)
                    db.AddInParameter(updateGovtSavingsAccCmd, "@CGSA_AccountOpeningDate", DbType.Date, govtSavingsAccVo.AccountOpeningDate);
                db.AddInParameter(updateGovtSavingsAccCmd, "@CGSA_ModifiedBy", DbType.Int32, userId);
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
                FunctionInfo.Add("Method", "GovtSavingsDao.cs:UpdateGovtSavingsAccount()");
                object[] objects = new object[2];
                objects[0] = govtSavingsAccVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public GovtSavingsVo GetGovtSavingsDetails(int govtSavingsNPId)
        {
            GovtSavingsVo govtSavingsVo = null;
            Database db;
            DbCommand getGovtSavingsListCmd;
            DataSet dsGovtSavings;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGovtSavingsListCmd = db.GetStoredProcCommand("SP_GetGovtSavingsDetails");
                db.AddInParameter(getGovtSavingsListCmd, "@CGSNP_GovtSavingNPId", DbType.Int16, govtSavingsNPId);
                dsGovtSavings = db.ExecuteDataSet(getGovtSavingsListCmd);
                if (dsGovtSavings.Tables[0].Rows.Count > 0)
                {
                    dr = dsGovtSavings.Tables[0].Rows[0];
                    govtSavingsVo = new GovtSavingsVo();
                    govtSavingsVo.GoveSavingsPortfolioId = int.Parse(dr["CGSNP_GovtSavingNPId"].ToString());
                    govtSavingsVo.AccountId = int.Parse(dr["CGSA_AccountId"].ToString());
                    govtSavingsVo.AssetInstrumentCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    govtSavingsVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                    govtSavingsVo.DebtIssuerCode = dr["XDI_DebtIssuerCode"].ToString();
                    govtSavingsVo.InterestPayableFrequencyCode = dr["XF_InterestPayableFrequencyCode"].ToString();
                    govtSavingsVo.CompoundInterestFrequencyCode = dr["XF_CompoundInterestFrequencyCode"].ToString();
                    govtSavingsVo.InterestRate = float.Parse(dr["CGSNP_InterestRate"].ToString());
                    govtSavingsVo.Name = dr["CGSNP_Name"].ToString();
                    govtSavingsVo.AssetInstrumentCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                    //  govtSavingsVo.Quantity = float.Parse(dr["CIGSP_Quantity"].ToString());
                    if (dr["CGSNP_PurchaseDate"] != DBNull.Value)
                        govtSavingsVo.PurchaseDate = DateTime.Parse(dr["CGSNP_PurchaseDate"].ToString());
                    if (dr["CGSNP_DepositAmount"] != DBNull.Value)
                        govtSavingsVo.DepositAmt = float.Parse(dr["CGSNP_DepositAmount"].ToString());
                    if (dr["CGSNP_MaturityDate"] != DBNull.Value)
                        govtSavingsVo.MaturityDate = DateTime.Parse(dr["CGSNP_MaturityDate"].ToString());
                    if (dr["CGSNP_SubsqntDepositAmount"] != DBNull.Value)
                        govtSavingsVo.SubsqntDepositAmount = float.Parse(dr["CGSNP_SubsqntDepositAmount"].ToString());
                    if (dr["XF_DepositFrequencyCode"] != DBNull.Value)
                        govtSavingsVo.DepositFrequencyCode = dr["XF_DepositFrequencyCode"].ToString();

                    if (dr["CGSNP_SubsqntDepositDate"] != DBNull.Value)
                        govtSavingsVo.SubsqntDepositDate = DateTime.Parse(dr["CGSNP_SubsqntDepositDate"].ToString());
                    if (dr["CGSNP_MaturityValue"] != DBNull.Value)
                        govtSavingsVo.MaturityValue = float.Parse(dr["CGSNP_MaturityValue"].ToString());
                    if (dr["CGSNP_CurrentValue"] != DBNull.Value)
                        govtSavingsVo.CurrentValue = float.Parse(dr["CGSNP_CurrentValue"].ToString());
                    govtSavingsVo.InterestBasisCode = dr["XIB_InterestBasisCode"].ToString();
                    if (dr["CGSNP_IsInterestAccumalated"] != DBNull.Value)
                        govtSavingsVo.IsInterestAccumalated = int.Parse(dr["CGSNP_IsInterestAccumalated"].ToString());
                    if (dr["CGSNP_InterestAmtAccumalated"] != DBNull.Value)
                        govtSavingsVo.InterestAmtAccumalated = float.Parse(dr["CGSNP_InterestAmtAccumalated"].ToString());
                    if (dr["CGSNP_InterestAmtPaidOut"] != DBNull.Value)
                        govtSavingsVo.InterestAmtPaidOut = float.Parse(dr["CGSNP_InterestAmtPaidOut"].ToString());
                    govtSavingsVo.Remarks = dr["CGSNP_Remark"].ToString();
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
                FunctionInfo.Add("Method", "GovtSavingsDao.cs:GetGovtSavingsDetails()");


                object[] objects = new object[1];
                objects[0] = govtSavingsNPId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return govtSavingsVo;




        }
    }
}
