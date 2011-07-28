using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using VoCustomerPortfolio;
using VoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoCustomerPortfolio
{
    public class FixedIncomeDao
    {
        public bool CreateFixedIncomePortfolio(FixedIncomeVo fixedincomeVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand createFixedIncomePortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createFixedIncomePortfolioCmd = db.GetStoredProcCommand("SP_CreateFixedIncomeNetPosition");

               // db.AddInParameter(createFixedIncomePortfolioCmd, "@C_CustomerId", DbType.Int32, fixedincomeVo.CustomerId);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFIA_AccountId", DbType.Int32, fixedincomeVo.AccountId);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, fixedincomeVo.AssetInstrumentCategoryCode);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@PAG_AssetGroupCode", DbType.String, fixedincomeVo.AssetGroupCode);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@XDI_DebtIssuerCode", DbType.String, fixedincomeVo.DebtIssuerCode);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@XIB_InterestBasisCode", DbType.String, fixedincomeVo.InterestBasisCode);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@XF_CompoundInterestFrequencyCode", DbType.String, fixedincomeVo.CompoundInterestFrequencyCode);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@XF_InterestPayableFrequencyCode", DbType.String, fixedincomeVo.InterestPayableFrequencyCode);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_Name", DbType.String, fixedincomeVo.Name);
                if (fixedincomeVo.IssueDate != DateTime.MinValue)
                    db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_IssueDate", DbType.DateTime, fixedincomeVo.IssueDate);
                else
                    db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_IssueDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_PrincipalAmount", DbType.Decimal, fixedincomeVo.PrinciaplAmount);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_InterestAmtPaidOut", DbType.Decimal, fixedincomeVo.InterestAmtPaidOut);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_InterestAmtAcculumated", DbType.Decimal, fixedincomeVo.InterestAmtAccumulated);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_InterestRate", DbType.Decimal, fixedincomeVo.InterestRate);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_FaceValue", DbType.Decimal, fixedincomeVo.FaceValue);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_MaturityFaceValue", DbType.Decimal, fixedincomeVo.MaturityFaceValue);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_PurchasePrice", DbType.Decimal, fixedincomeVo.PurchasePrice);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_SubsequentDepositAmount", DbType.Decimal, fixedincomeVo.SubsequentDepositAmount);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@XF_DepositFrquencycode", DbType.String, fixedincomeVo.DepositFrequencyCode);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_DebentureNum", DbType.Int32, fixedincomeVo.DebentureNum);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_PurchaseValue", DbType.Decimal, fixedincomeVo.PurchaseValue);
                if (fixedincomeVo.PurchaseDate != DateTime.MinValue)
                    db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_PurchaseDate", DbType.DateTime, fixedincomeVo.PurchaseDate);
                else
                    db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_PurchaseDate", DbType.DateTime, DBNull.Value);
                if (fixedincomeVo.MaturityDate != DateTime.MinValue)
                    db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_MaturityDate", DbType.DateTime, fixedincomeVo.MaturityDate);
                else
                    db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_MaturityDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_MaturityValue", DbType.Decimal, fixedincomeVo.MaturityValue);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_IsInterestAccumulated", DbType.Int16, fixedincomeVo.IsInterestAccumulated);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_CurrentPrice", DbType.Decimal, fixedincomeVo.CurrentPrice);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_CurrentValue", DbType.Decimal, fixedincomeVo.CurrentValue);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_Remark", DbType.String, fixedincomeVo.Remark);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createFixedIncomePortfolioCmd, "@CFINP_ModifiedBy", DbType.Int32, userId);

                if(db.ExecuteNonQuery(createFixedIncomePortfolioCmd)!=0)
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

                FunctionInfo.Add("Method", "FixedIncomeDao.cs:CreateFixedIncomePortfolio()");


                object[] objects = new object[2];
                objects[0] = fixedincomeVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool UpdateFixedIncomePortfolio(FixedIncomeVo fixedIncomeVo,int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateFixedIncomePortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateFixedIncomePortfolioCmd = db.GetStoredProcCommand("SP_UpdateFixedIncomeNetPosition");
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_FINPId", DbType.Int32, fixedIncomeVo.FITransactionId);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFIA_AccountId", DbType.Int32, fixedIncomeVo.AccountId);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, fixedIncomeVo.AssetInstrumentCategoryCode);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@PAG_AssetGroupCode", DbType.String, fixedIncomeVo.AssetGroupCode);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@XDI_DebtIssuerCode", DbType.String, fixedIncomeVo.DebtIssuerCode);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@XIB_InterestBasisCode", DbType.String, fixedIncomeVo.InterestBasisCode);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@XF_CompoundInterestFrequencyCode", DbType.String, fixedIncomeVo.CompoundInterestFrequencyCode);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@XF_InterestPayableFrequencyCode", DbType.String, fixedIncomeVo.InterestPayableFrequencyCode);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_Name", DbType.String, fixedIncomeVo.Name);
                if (fixedIncomeVo.IssueDate != DateTime.MinValue)
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_IssueDate", DbType.DateTime, fixedIncomeVo.IssueDate);
                else
                    db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_IssueDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_PrincipalAmount", DbType.Decimal, fixedIncomeVo.PrinciaplAmount);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_InterestAmtPaidOut", DbType.Decimal, fixedIncomeVo.InterestAmtPaidOut);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_InterestAmtAcculumated", DbType.Decimal, fixedIncomeVo.InterestAmtAccumulated);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_InterestRate", DbType.Decimal, fixedIncomeVo.InterestRate);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_FaceValue", DbType.Decimal, fixedIncomeVo.FaceValue);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_MaturityFaceValue", DbType.Decimal, fixedIncomeVo.MaturityFaceValue);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_PurchasePrice", DbType.Decimal, fixedIncomeVo.PurchasePrice);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_SubsequentDepositAmount", DbType.Decimal, fixedIncomeVo.SubsequentDepositAmount);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@XF_DepositFrquencycode", DbType.String, fixedIncomeVo.DepositFrequencyCode);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_DebentureNum", DbType.Int32, fixedIncomeVo.DebentureNum);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_PurchaseValue", DbType.Decimal, fixedIncomeVo.PurchaseValue);
                if (fixedIncomeVo.PurchaseDate != DateTime.MinValue)
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_PurchaseDate", DbType.DateTime, fixedIncomeVo.PurchaseDate);
                else
                    db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_PurchaseDate", DbType.DateTime, DBNull.Value);
                if (fixedIncomeVo.MaturityDate != DateTime.MinValue)
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_MaturityDate", DbType.DateTime, fixedIncomeVo.MaturityDate);
                else
                    db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_MaturityDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_MaturityValue", DbType.Decimal, fixedIncomeVo.MaturityValue);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_IsInterestAccumulated", DbType.Int16, fixedIncomeVo.IsInterestAccumulated);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_CurrentPrice", DbType.Decimal, fixedIncomeVo.CurrentPrice);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_CurrentValue", DbType.Decimal, fixedIncomeVo.CurrentValue);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_Remark", DbType.String, fixedIncomeVo.Remark);
                db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_ModifiedBy", DbType.Int32, userId);
               if( db.ExecuteNonQuery(updateFixedIncomePortfolioCmd)!=0)
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

                FunctionInfo.Add("Method", "FixedIncomeDao.cs:CreateFixedIncomePortfolio()");


                object[] objects = new object[2];
                objects[0] = fixedIncomeVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool DeleteFixedIncomePortfolio(int fixedIncomeId, int accountId)
        {
            bool bResult = false;

            Database db;
            DbCommand deleteFixedIncomePortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteFixedIncomePortfolioCmd = db.GetStoredProcCommand("SP_DeleteFixedIncomeNetPosition");

                db.AddInParameter(deleteFixedIncomePortfolioCmd, "@CFINP_FINPId", DbType.Int32, fixedIncomeId);
                db.AddInParameter(deleteFixedIncomePortfolioCmd, "@CFIA_AccountId", DbType.Int32, accountId);

               if( db.ExecuteNonQuery(deleteFixedIncomePortfolioCmd)!=0)
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
                FunctionInfo.Add("Method", "FixedIncomeDao.cs:DeleteFixedIncomePortfolio()");
                object[] objects = new object[2];
                objects[0] = fixedIncomeId;
                objects[1] = accountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public FixedIncomeVo GetFixedIncomePortfolio(int portfolioId)
        {
            FixedIncomeVo fixedIncomeVo = null;
            Database db;
            DbCommand getFixedIncomePortfolioCmd;
            DataSet dsGetFixedIncomePortfolio;
            
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getFixedIncomePortfolioCmd = db.GetStoredProcCommand("SP_GetFixedIncomeNetPosition");
                db.AddInParameter(getFixedIncomePortfolioCmd, "@CFINP_FINPId", DbType.Int32, portfolioId);
                dsGetFixedIncomePortfolio = db.ExecuteDataSet(getFixedIncomePortfolioCmd);
                if (dsGetFixedIncomePortfolio.Tables[0].Rows.Count > 0)
                {
                    fixedIncomeVo = new FixedIncomeVo();
                    dr = dsGetFixedIncomePortfolio.Tables[0].Rows[0];

                    fixedIncomeVo.FITransactionId = int.Parse(dr["CFINP_FINPId"].ToString());
                    fixedIncomeVo.AccountId = int.Parse(dr["CFIA_AccountId"].ToString());
                    fixedIncomeVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                    fixedIncomeVo.AssetInstrumentCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    fixedIncomeVo.CompoundInterestFrequencyCode = dr["XF_CompoundInterestFrequencyCode"].ToString();
                    fixedIncomeVo.CurrentPrice = float.Parse(dr["CFINP_CurrentPrice"].ToString());
                    fixedIncomeVo.CurrentValue = float.Parse(dr["CFINP_CurrentValue"].ToString());
                    fixedIncomeVo.DebentureNum = int.Parse(dr["CFINP_DebentureNum"].ToString());
                    fixedIncomeVo.DebtIssuerCode = dr["XDI_DebtIssuerCode"].ToString();
                    fixedIncomeVo.DepositFrequencyCode = dr["XF_DepositFrquencycode"].ToString();
                    fixedIncomeVo.FaceValue = float.Parse(dr["CFINP_FaceValue"].ToString());
                    fixedIncomeVo.InterestAmtAccumulated = float.Parse(dr["CFINP_InterestAmtAcculumated"].ToString());
                    fixedIncomeVo.InterestAmtPaidOut = float.Parse(dr["CFINP_InterestAmtPaidOut"].ToString());
                    fixedIncomeVo.InterestBasisCode = dr["XIB_InterestBasisCode"].ToString();
                    fixedIncomeVo.InterestPayableFrequencyCode = dr["XF_InterestPayableFrequencyCode"].ToString();
                    fixedIncomeVo.InterestRate = float.Parse(dr["CFINP_InterestRate"].ToString());
                    fixedIncomeVo.IsInterestAccumulated = int.Parse(dr["CFINP_IsInterestAccumulated"].ToString());
                    if(dr["CFINP_IssueDate"].ToString()!=String.Empty)
                        fixedIncomeVo.IssueDate = DateTime.Parse(dr["CFINP_IssueDate"].ToString());
                    if (dr["CFINP_MaturityDate"].ToString() != String.Empty)
                        fixedIncomeVo.MaturityDate = DateTime.Parse(dr["CFINP_MaturityDate"].ToString());
                    fixedIncomeVo.MaturityFaceValue=float.Parse(dr["CFINP_MaturityFaceValue"].ToString());
                    fixedIncomeVo.MaturityValue = float.Parse(dr["CFINP_MaturityValue"].ToString());
                    fixedIncomeVo.Name = dr["CFINP_Name"].ToString();
                    fixedIncomeVo.PrinciaplAmount = float.Parse(dr["CFINP_PrincipalAmount"].ToString());
                    if (dr["CFINP_PurchaseDate"].ToString() != String.Empty)
                        fixedIncomeVo.PurchaseDate = DateTime.Parse(dr["CFINP_PurchaseDate"].ToString());
                    fixedIncomeVo.PurchasePrice = float.Parse(dr["CFINP_PurchasePrice"].ToString());
                    fixedIncomeVo.PurchaseValue = float.Parse(dr["CFINP_PurchaseValue"].ToString());
                    // fixedIncomeVo.Quantity=float.Parse(dr["CFINP_Quantity"].ToString());
                    fixedIncomeVo.Remark = dr["CFINP_Remark"].ToString();
                    fixedIncomeVo.SubsequentDepositAmount =double.Parse(dr["CFINP_SubsequentDepositAmount"].ToString());
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
                FunctionInfo.Add("Method", "FixedIncomeDao.cs:GetFixedIncomePortfolio()");
                object[] objects = new object[1];
                objects[0] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return fixedIncomeVo;
        }

        public List<FixedIncomeVo> GetFixedIncomePortfolioList(int portfolioId ,int CurrentPage , string SortOrder,out int Count)
        {
            List<FixedIncomeVo> FixedIncomeList=null;
            FixedIncomeVo fixedincomeVo;
            Database db;
            DbCommand getFixedIncomePortfolioListCmd;
            DataSet dsGetFixedIncomePortfolioList;
            DataTable dtGetFixedIncomePortfolioList;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getFixedIncomePortfolioListCmd = db.GetStoredProcCommand("SP_GetFixedIncomeList");
                db.AddInParameter(getFixedIncomePortfolioListCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getFixedIncomePortfolioListCmd, "@CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getFixedIncomePortfolioListCmd, "@SortOrder", DbType.String, SortOrder);
                dsGetFixedIncomePortfolioList = db.ExecuteDataSet(getFixedIncomePortfolioListCmd);
                if (dsGetFixedIncomePortfolioList.Tables[0].Rows.Count > 0)
                {
                    dtGetFixedIncomePortfolioList = dsGetFixedIncomePortfolioList.Tables[0];

                    FixedIncomeList = new List<FixedIncomeVo>();

                    foreach (DataRow dr in dtGetFixedIncomePortfolioList.Rows)
                    {
                        fixedincomeVo = new FixedIncomeVo();
                        fixedincomeVo.FITransactionId = int.Parse(dr["CFINP_FINPId"].ToString());
                        fixedincomeVo.AccountId = int.Parse(dr["CFIA_AccountId"].ToString());
                        fixedincomeVo.AssetInstrumentCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        fixedincomeVo.AssetInstrumentCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        fixedincomeVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                        fixedincomeVo.DebtIssuerCode = dr["XDI_DebtIssuerCode"].ToString();
                        fixedincomeVo.InterestBasisCode = dr["XIB_InterestBasisCode"].ToString();
                        fixedincomeVo.CompoundInterestFrequencyCode = dr["XF_CompoundInterestFrequencyCode"].ToString();
                        fixedincomeVo.InterestPayableFrequencyCode = dr["XF_InterestPayableFrequencyCode"].ToString();
                        fixedincomeVo.Name = dr["CFINP_Name"].ToString();
                        fixedincomeVo.PrinciaplAmount = float.Parse(dr["CFINP_PrincipalAmount"].ToString());
                        fixedincomeVo.InterestAmtPaidOut = float.Parse(dr["CFINP_InterestAmtPaidOut"].ToString());
                        fixedincomeVo.InterestAmtAccumulated = float.Parse(dr["CFINP_InterestAmtAcculumated"].ToString());
                        fixedincomeVo.InterestRate = float.Parse(dr["CFINP_InterestRate"].ToString());
                        fixedincomeVo.PurchasePrice = float.Parse(dr["CFINP_PurchasePrice"].ToString());
                        fixedincomeVo.PurchaseValue = float.Parse(dr["CFINP_PurchaseValue"].ToString());
                        if (dr["CFINP_PurchaseDate"].ToString() != String.Empty)
                            fixedincomeVo.PurchaseDate = DateTime.Parse(dr["CFINP_PurchaseDate"].ToString());
                        if (dr["CFINP_MaturityDate"].ToString() != String.Empty)
                            fixedincomeVo.MaturityDate = DateTime.Parse(dr["CFINP_MaturityDate"].ToString());
                        fixedincomeVo.MaturityValue = float.Parse(dr["CFINP_MaturityValue"].ToString());
                        fixedincomeVo.IsInterestAccumulated = int.Parse(dr["CFINP_IsInterestAccumulated"].ToString());
                        fixedincomeVo.CurrentPrice = float.Parse(dr["CFINP_CurrentPrice"].ToString());
                        fixedincomeVo.CurrentValue = float.Parse(dr["CFINP_CurrentValue"].ToString());

                        FixedIncomeList.Add(fixedincomeVo);
                    }
                }
                if (dsGetFixedIncomePortfolioList.Tables[1] != null && dsGetFixedIncomePortfolioList.Tables[1].Rows.Count > 0)
                    Count = Int32.Parse(dsGetFixedIncomePortfolioList.Tables[1].Rows[0][0].ToString());
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
                FunctionInfo.Add("Method", "FixedIncomeDao.cs:GetFixedIncomePortfolioList()");
                object[] objects = new object[1];
                objects[0] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return FixedIncomeList;

        }

        //public bool UpdateFixedIncomePortfolio(FixedIncomeVo fixedincomeVo, int userId)
        //{
        //    bool bResult = false;
        //    Database db;
        //    DbCommand updateFixedIncomePortfolioCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        updateFixedIncomePortfolioCmd = db.GetStoredProcCommand("SP_UpdateFixedIncomePortfolio");

        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@C_CustomerId", DbType.Int32, fixedincomeVo.CustomerId);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CA_AccountId", DbType.Int32, fixedincomeVo.AccountId);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, fixedincomeVo.AssetInstrumentCategoryCode);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@PAG_AssetGroupCode", DbType.String, fixedincomeVo.AssetGroupCode);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@WDI_DebtIssuerCode", DbType.String, fixedincomeVo.DebtIssuerCode);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@WIB_InterestBasisCode", DbType.String, fixedincomeVo.InterestBasisCode);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_CompoundInterestFrequencyCode", DbType.String, fixedincomeVo.CompoundInterestFrequencyCode);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_InterestPayableFrequencyCode", DbType.String, fixedincomeVo.InterestPayableFrequencyCode);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_Name", DbType.String, fixedincomeVo.Name);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_PrinciaplAmount", DbType.Decimal, fixedincomeVo.PrinciaplAmount);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_InterestAmtPaidOut", DbType.Decimal, fixedincomeVo.InterestAmtPaidOut);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_InterestAmtAcculumated", DbType.Decimal, fixedincomeVo.InterestAmtAccumulated);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_InterestRate", DbType.Decimal, fixedincomeVo.InterestRate);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_PurchasePrice", DbType.Decimal, fixedincomeVo.PurchasePrice);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_PurchaseValue", DbType.Decimal, fixedincomeVo.PurchaseValue);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_PurchaseDate", DbType.DateTime, fixedincomeVo.PurchaseDate);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_MaturityDate", DbType.DateTime, fixedincomeVo.MaturityDate);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_MaturityValue", DbType.Decimal, fixedincomeVo.MaturityValue);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_IsInterestAccumulated", DbType.Int16, fixedincomeVo.IsInterestAccumulated);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_CurrentPrice", DbType.Decimal, fixedincomeVo.CurrentPrice);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_CurrentValue", DbType.Decimal, fixedincomeVo.CurrentValue);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_Quantity", DbType.Decimal, fixedincomeVo.Quantity);
        //        db.AddInParameter(updateFixedIncomePortfolioCmd, "@CFINP_ModifiedBy", DbType.Int32, userId);

        //        db.ExecuteNonQuery(updateFixedIncomePortfolioCmd);
        //        bResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "FixedIncomeDao.cs:UpdateFixedIncomePortfolio()");


        //        object[] objects = new object[2];
        //        objects[0] = fixedincomeVo;
        //        objects[1] = userId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }

        //    return bResult;
        //}

        public bool  UpdateFixedIncomeAccount(CustomerAccountsVo customerAccountVo,int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateFixedIncomeAccCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateFixedIncomeAccCmd = db.GetStoredProcCommand("SP_UpdateFixedIncomeAccount");
                db.AddInParameter(updateFixedIncomeAccCmd, "@CFIA_AccountId", DbType.Int32, customerAccountVo.AccountId);
                db.AddInParameter(updateFixedIncomeAccCmd, "@CFIA_AccountNum", DbType.String, customerAccountVo.AccountNum);
                db.AddInParameter(updateFixedIncomeAccCmd, "@CFIA_AccountSource", DbType.String, customerAccountVo.AccountSource);
                db.AddInParameter(updateFixedIncomeAccCmd, "@XMOH_ModeOfHoldingCode", DbType.String, customerAccountVo.ModeOfHolding);
                db.AddInParameter(updateFixedIncomeAccCmd, "@CFIA_ModifiedBy", DbType.Int32, userId);
              if(  db.ExecuteNonQuery(updateFixedIncomeAccCmd)!=0)
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
                FunctionInfo.Add("Method", "FixedIncomeDao.cs:UpdateGovtSavingsAccount()");
                object[] objects = new object[2];
                objects[0] = customerAccountVo;
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
