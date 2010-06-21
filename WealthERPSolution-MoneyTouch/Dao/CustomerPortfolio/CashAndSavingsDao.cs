using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace DaoCustomerPortfolio
{
    public class CashAndSavingsDao
    {
        public List<CashAndSavingsVo> GetCustomerCashSavings(int portfolioId, int CurrentPage, string sortOrder, out int Count)
        {
            List<CashAndSavingsVo> CustomerCashSavingsList = null;
            CashAndSavingsVo customerCashSavingsPortfolioVo;// = new CustomerCashSavingsPortfolioVo();
            Database db;
            DbCommand getCustomerCashSavingsCmd;
            DataSet dsGetCustomerCashSavings;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerCashSavingsCmd = db.GetStoredProcCommand("SP_GetCustomerCashSavingsList");
                db.AddInParameter(getCustomerCashSavingsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getCustomerCashSavingsCmd, "@CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getCustomerCashSavingsCmd, "@SortOrder", DbType.String, sortOrder);
                dsGetCustomerCashSavings = db.ExecuteDataSet(getCustomerCashSavingsCmd);
                if (dsGetCustomerCashSavings.Tables[0].Rows.Count > 0)
                {
                    CustomerCashSavingsList = new List<CashAndSavingsVo>();
                    foreach (DataRow dr in dsGetCustomerCashSavings.Tables[0].Rows)
                    {
                        customerCashSavingsPortfolioVo = new CashAndSavingsVo();


                        customerCashSavingsPortfolioVo.CashSavingsPortfolioId = int.Parse(dr["CCSNP_CashSavingsNPId"].ToString());
                        customerCashSavingsPortfolioVo.AccountId = Int32.Parse(dr["CCSA_AccountId"].ToString().Trim());
                        customerCashSavingsPortfolioVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString().Trim();
                        customerCashSavingsPortfolioVo.AssetInstrumentCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString().Trim();
                        customerCashSavingsPortfolioVo.CurrentValue = float.Parse(dr["CCSNP_CurrentValue"].ToString().Trim());
                        // customerCashSavingsPortfolioVo.CustomerId = Int32.Parse(dr["C_CustomerId"].ToString().Trim());
                        customerCashSavingsPortfolioVo.DebtIssuerCode = dr["XDI_DebtIssuerCode"].ToString().Trim();
                        customerCashSavingsPortfolioVo.DepositAmount = float.Parse(dr["CCSNP_DepositAmount"].ToString().Trim());
                        if (dr["CCSNP_DepositDate"].ToString() != "")
                            customerCashSavingsPortfolioVo.DepositDate = DateTime.Parse(dr["CCSNP_DepositDate"].ToString().Trim());
                        customerCashSavingsPortfolioVo.InterestBasisCode = dr["XIB_InterestBasisCode"].ToString().Trim();
                        customerCashSavingsPortfolioVo.IsInterestAccumalated = Int16.Parse(dr["CCSNP_IsInterestAccumulated"].ToString().Trim());
                        customerCashSavingsPortfolioVo.Remarks = dr["CCSNP_Remark"].ToString();
                        customerCashSavingsPortfolioVo.Name = dr["CCSNP_Name"].ToString();
                        customerCashSavingsPortfolioVo.AssetInstrumentCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();

                        if (dr["XF_CompoundInterestFrequencyCode"].ToString() != "")
                        {
                            customerCashSavingsPortfolioVo.CompoundInterestFrequencyCode = dr["XF_CompoundInterestFrequencyCode"].ToString().Trim();
                        }
                        if (dr["CCSNP_InterestAmntAccumulated"].ToString() != "")
                        {
                            customerCashSavingsPortfolioVo.InterestAmntAccumulated = float.Parse(dr["CCSNP_InterestAmntAccumulated"].ToString().Trim());
                        }
                        if (dr["CCSNP_InterestAmntPaidOut"].ToString() != "")
                        {
                            customerCashSavingsPortfolioVo.InterestAmntPaidOut = float.Parse(dr["CCSNP_InterestAmntPaidOut"].ToString().Trim());
                        }
                        if (dr["XF_InterestPayoutFrequencyCode"].ToString() != "")
                        {
                            customerCashSavingsPortfolioVo.InterestPayoutFrequencyCode = dr["XF_InterestPayoutFrequencyCode"].ToString().Trim();
                        }
                        if (dr["CCSNP_InterestRate"].ToString() != "")
                        {
                            customerCashSavingsPortfolioVo.InterestRate = float.Parse(dr["CCSNP_InterestRate"].ToString().Trim());
                        }

                        CustomerCashSavingsList.Add(customerCashSavingsPortfolioVo);
                    }
                }

                if (dsGetCustomerCashSavings.Tables[1] != null && dsGetCustomerCashSavings.Tables[0].Rows.Count > 0)
                    Count = Int32.Parse(dsGetCustomerCashSavings.Tables[1].Rows[0][0].ToString());
                else Count = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerCashSavingsPortfolioDao.cs:GetCustomerCashSavings()");

                object[] objects = new object[1];
                objects[0] = portfolioId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return CustomerCashSavingsList;
        }

        public bool AddCustomerCashSavingsDetails(CashAndSavingsVo custCashSavingsVo, int UserID)
        {
            bool blResult = false;
            Database db;
            DbCommand addCustCashSavingsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                addCustCashSavingsCmd = db.GetStoredProcCommand("SP_CreateCashSavingsNetPosition");

                db.AddInParameter(addCustCashSavingsCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, custCashSavingsVo.AssetInstrumentCategoryCode);
                db.AddInParameter(addCustCashSavingsCmd, "@PAG_AssetGroupCode", DbType.String, custCashSavingsVo.AssetGroupCode);
                db.AddInParameter(addCustCashSavingsCmd, "@CCSA_AccountId", DbType.Int32, custCashSavingsVo.AccountId);
                db.AddInParameter(addCustCashSavingsCmd, "@XDI_DebtIssuerCode", DbType.String, custCashSavingsVo.DebtIssuerCode);
                db.AddInParameter(addCustCashSavingsCmd, "@XIB_InterestBasisCode", DbType.String, custCashSavingsVo.InterestBasisCode);
                db.AddInParameter(addCustCashSavingsCmd, "@CCSNP_Name", DbType.String, custCashSavingsVo.Name);
                db.AddInParameter(addCustCashSavingsCmd, "@CCSNP_DepositAmount", DbType.Decimal, custCashSavingsVo.DepositAmount);
                if (custCashSavingsVo.DepositDate != DateTime.MinValue)
                    db.AddInParameter(addCustCashSavingsCmd, "@CCSNP_DepositDate", DbType.DateTime, custCashSavingsVo.DepositDate);
                else
                    db.AddInParameter(addCustCashSavingsCmd, "@CCSNP_DepositDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(addCustCashSavingsCmd, "@CCSNP_CurrentValue", DbType.Decimal, custCashSavingsVo.CurrentValue);
                db.AddInParameter(addCustCashSavingsCmd, "@CCSNP_InterestRate", DbType.Decimal, custCashSavingsVo.InterestRate);
                db.AddInParameter(addCustCashSavingsCmd, "@CCSNP_IsInterestAccumulated", DbType.Int16, custCashSavingsVo.IsInterestAccumalated);

                if (custCashSavingsVo.CompoundInterestFrequencyCode != null)
                    db.AddInParameter(addCustCashSavingsCmd, "@XF_CompoundInterestFrequencyCode", DbType.String, custCashSavingsVo.CompoundInterestFrequencyCode);
                else
                    db.AddInParameter(addCustCashSavingsCmd, "@XF_CompoundInterestFrequencyCode", DbType.String, DBNull.Value);

                if (custCashSavingsVo.InterestPayoutFrequencyCode != null)
                    db.AddInParameter(addCustCashSavingsCmd, "@XF_InterestPayoutFrequencyCode", DbType.String, custCashSavingsVo.InterestPayoutFrequencyCode);
                else
                    db.AddInParameter(addCustCashSavingsCmd, "@XF_InterestPayoutFrequencyCode", DbType.String, DBNull.Value);

                if (custCashSavingsVo.InterestAmntPaidOut != null)
                    db.AddInParameter(addCustCashSavingsCmd, "@CCSNP_InterestAmntPaidOut", DbType.String, custCashSavingsVo.InterestAmntPaidOut);
                else
                    db.AddInParameter(addCustCashSavingsCmd, "@CCSNP_InterestAmntPaidOut", DbType.String, DBNull.Value);

                if (custCashSavingsVo.InterestAmntAccumulated != null)
                    db.AddInParameter(addCustCashSavingsCmd, "@CCSNP_InterestAmntAccumulated", DbType.String, custCashSavingsVo.InterestAmntAccumulated);
                else
                    db.AddInParameter(addCustCashSavingsCmd, "@CCSNP_InterestAmntAccumulated", DbType.String, DBNull.Value);

                db.AddInParameter(addCustCashSavingsCmd, "@CCSNP_CreatedBy", DbType.Int32, UserID);
                db.AddInParameter(addCustCashSavingsCmd, "@CCSNP_ModifiedBy", DbType.Int32, UserID);
                db.AddInParameter(addCustCashSavingsCmd, "@CCSNP_Remark", DbType.String, custCashSavingsVo.Remarks);
                if (db.ExecuteNonQuery(addCustCashSavingsCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerCashSavingsPortfolioDao.cs:AddCustomerCashSavingsDetails()");


                object[] objects = new object[1];
                objects[0] = custCashSavingsVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public CashAndSavingsVo GetSpecificCashSavings(int CashSavingsPortfolioID, int CustomerID)
        {
            CashAndSavingsVo cashAndSavingsVo = null;
            Database db;
            DbCommand getSpecificCashSavingsCmd;
            DataSet dsGetSpecificCashSavings;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");


                getSpecificCashSavingsCmd = db.GetStoredProcCommand("SP_GetCashSavingsNetPosition");
                db.AddInParameter(getSpecificCashSavingsCmd, "@CCSNP_CashSavingsNPId", DbType.Int32, CashSavingsPortfolioID);

                dsGetSpecificCashSavings = db.ExecuteDataSet(getSpecificCashSavingsCmd);
                if (dsGetSpecificCashSavings.Tables[0].Rows.Count > 0)
                {
                    cashAndSavingsVo = new CashAndSavingsVo();

                    foreach (DataRow dr in dsGetSpecificCashSavings.Tables[0].Rows)
                    {

                        cashAndSavingsVo.AccountId = Int64.Parse(dr["CCSA_AccountId"].ToString());
                        //cashAndSavingsVo.AccountNumber = "";
                        cashAndSavingsVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                        cashAndSavingsVo.AssetInstrumentCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        cashAndSavingsVo.CashSavingsPortfolioId = Int32.Parse(dr["CCSNP_CashSavingsNPId"].ToString());

                        cashAndSavingsVo.CurrentValue = float.Parse(dr["CCSNP_CurrentValue"].ToString());
                        //cashAndSavingsVo.CustomerId = Int32.Parse(dr["C_CustomerId"].ToString());
                        cashAndSavingsVo.DebtIssuerCode = dr["XDI_DebtIssuerCode"].ToString();
                        cashAndSavingsVo.DepositAmount = float.Parse(dr["CCSNP_DepositAmount"].ToString());
                        if (dr["CCSNP_DepositDate"].ToString() != "")
                            cashAndSavingsVo.DepositDate = DateTime.Parse(dr["CCSNP_DepositDate"].ToString());

                        cashAndSavingsVo.InterestBasisCode = dr["XIB_InterestBasisCode"].ToString();

                        cashAndSavingsVo.InterestRate = float.Parse(dr["CCSNP_InterestRate"].ToString());
                        cashAndSavingsVo.IsInterestAccumalated = Int16.Parse(dr["CCSNP_IsInterestAccumulated"].ToString());
                        cashAndSavingsVo.Name = dr["CCSNP_Name"].ToString();

                        if (dr["XF_CompoundInterestFrequencyCode"].ToString() != "")
                            cashAndSavingsVo.CompoundInterestFrequencyCode = dr["XF_CompoundInterestFrequencyCode"].ToString();
                        if (dr["CCSNP_InterestAmntAccumulated"].ToString() != "")
                            cashAndSavingsVo.InterestAmntAccumulated = float.Parse(dr["CCSNP_InterestAmntAccumulated"].ToString());
                        if (dr["CCSNP_InterestAmntPaidOut"].ToString() != "")
                            cashAndSavingsVo.InterestAmntPaidOut = float.Parse(dr["CCSNP_InterestAmntPaidOut"].ToString());
                        if (dr["XF_InterestPayoutFrequencyCode"].ToString() != "")
                            cashAndSavingsVo.InterestPayoutFrequencyCode = dr["XF_InterestPayoutFrequencyCode"].ToString();
                        if (dr["CCSNP_InterestRate"].ToString() != "")
                            cashAndSavingsVo.InterestRate = float.Parse(dr["CCSNP_InterestRate"].ToString());
                        if (dr["CCSNP_Remark"].ToString() != "")
                            cashAndSavingsVo.Remarks = dr["CCSNP_Remark"].ToString();
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

                FunctionInfo.Add("Method", "CustomerCashSavingsPortfolioDao.cs:GetSpecificCashSavings()");


                object[] objects = new object[2];
                objects[0] = CustomerID;
                objects[1] = CashSavingsPortfolioID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return cashAndSavingsVo;
        }

        public bool UpdateCashSavingsAccount(CustomerAccountsVo customerAccountVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateCashSavingsAccCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCashSavingsAccCmd = db.GetStoredProcCommand("SP_UpdateCashSavingsAccount");
                db.AddInParameter(updateCashSavingsAccCmd, "@CCSA_AccountId", DbType.Int32, customerAccountVo.AccountId);
                db.AddInParameter(updateCashSavingsAccCmd, "@CCSA_AccountNum", DbType.String, customerAccountVo.AccountNum);
                db.AddInParameter(updateCashSavingsAccCmd, "@CCSA_BankName", DbType.String, customerAccountVo.BankName);
              //  db.AddInParameter(updateCashSavingsAccCmd, "@CCSA_AccountOpeningDate", DbType.Date, customerAccountVo.AccountOpeningDate);
                db.AddInParameter(updateCashSavingsAccCmd, "@XMOH_ModeOfHoldingCode", DbType.String, customerAccountVo.ModeOfHolding);
                db.AddInParameter(updateCashSavingsAccCmd, "@CCSA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(updateCashSavingsAccCmd) != 0)
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
                FunctionInfo.Add("Method", "CustomerCashSavingsPortfolioDao.cs:UpdateCashSavingsDetails()");
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

        public bool UpdateCashSavingsDetails(CashAndSavingsVo CashSavingsVo, int UserID)
        {
            bool blResult = false;
            Database db;
            DbCommand updateCustCashSavingsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCustCashSavingsCmd = db.GetStoredProcCommand("SP_UpdateCustomerCashSavings");



                db.AddInParameter(updateCustCashSavingsCmd, "@CCSNP_CashSavingsNPId", DbType.Int32, CashSavingsVo.CashSavingsPortfolioId);
                db.AddInParameter(updateCustCashSavingsCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, CashSavingsVo.AssetInstrumentCategoryCode);
                db.AddInParameter(updateCustCashSavingsCmd, "@PAG_AssetGroupCode", DbType.String, CashSavingsVo.AssetGroupCode);
                db.AddInParameter(updateCustCashSavingsCmd, "@CCSA_AccountId", DbType.Int64, CashSavingsVo.AccountId);
                db.AddInParameter(updateCustCashSavingsCmd, "@XDI_DebtIssuerCode", DbType.String, CashSavingsVo.DebtIssuerCode);
                db.AddInParameter(updateCustCashSavingsCmd, "@XIB_InterestBasisCode", DbType.String, CashSavingsVo.InterestBasisCode);
                db.AddInParameter(updateCustCashSavingsCmd, "@CCSNP_Name", DbType.String, CashSavingsVo.Name);
                db.AddInParameter(updateCustCashSavingsCmd, "@CCSNP_DepositAmount", DbType.Decimal, CashSavingsVo.DepositAmount);
                if (CashSavingsVo.DepositDate != DateTime.MinValue)
                    db.AddInParameter(updateCustCashSavingsCmd, "@CCSNP_DepositDate", DbType.DateTime, CashSavingsVo.DepositDate);
                else
                    db.AddInParameter(updateCustCashSavingsCmd, "@CCSNP_DepositDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(updateCustCashSavingsCmd, "@CCSNP_CurrentValue", DbType.Decimal, CashSavingsVo.CurrentValue);
                db.AddInParameter(updateCustCashSavingsCmd, "@CCSNP_InterestRate", DbType.Decimal, CashSavingsVo.InterestRate);
                db.AddInParameter(updateCustCashSavingsCmd, "@CCSNP_IsInterestAccumulated", DbType.String, CashSavingsVo.IsInterestAccumalated);

                if (CashSavingsVo.CompoundInterestFrequencyCode != null)
                    db.AddInParameter(updateCustCashSavingsCmd, "@XF_CompoundInterestFrequencyCode", DbType.String, CashSavingsVo.CompoundInterestFrequencyCode);
                else
                    db.AddInParameter(updateCustCashSavingsCmd, "@XF_CompoundInterestFrequencyCode", DbType.String, DBNull.Value);

                if (CashSavingsVo.InterestPayoutFrequencyCode != null)
                    db.AddInParameter(updateCustCashSavingsCmd, "@XF_InterestPayoutFrequencyCode", DbType.String, CashSavingsVo.InterestPayoutFrequencyCode);
                else
                    db.AddInParameter(updateCustCashSavingsCmd, "@XF_InterestPayoutFrequencyCode", DbType.String, DBNull.Value);

                if (CashSavingsVo.InterestAmntPaidOut != null)
                    db.AddInParameter(updateCustCashSavingsCmd, "@CCSNP_InterestAmntPaidOut", DbType.String, CashSavingsVo.InterestAmntPaidOut);
                else
                    db.AddInParameter(updateCustCashSavingsCmd, "@CCSNP_InterestAmntPaidOut", DbType.String, DBNull.Value);

                if (CashSavingsVo.InterestAmntAccumulated != null)
                    db.AddInParameter(updateCustCashSavingsCmd, "@CCSNP_InterestAmntAccumulated", DbType.String, CashSavingsVo.InterestAmntAccumulated);
                else
                    db.AddInParameter(updateCustCashSavingsCmd, "@CCSNP_InterestAmntAccumulated", DbType.String, DBNull.Value);

                db.AddInParameter(updateCustCashSavingsCmd, "@CCSNP_ModifiedBy", DbType.Int32, UserID);
                db.AddInParameter(updateCustCashSavingsCmd, "@CCSNP_Remark", DbType.String, CashSavingsVo.Remarks);
                if (db.ExecuteNonQuery(updateCustCashSavingsCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerCashSavingsPortfolioDao.cs:UpdateCashSavingsDetails()");


                object[] objects = new object[1];
                objects[0] = CashSavingsVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public bool DeleteCashSavingsPortfolio(int cashSavingsId, int accountId)
        {
            bool bResult = false;

            Database db;
            DbCommand deleteCashSavingsPortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteCashSavingsPortfolioCmd = db.GetStoredProcCommand("SP_DeleteCashandSavingsNetPostion");

                db.AddInParameter(deleteCashSavingsPortfolioCmd, "@CCSNP_CashSavingsNPId", DbType.Int32, cashSavingsId);

                if (db.ExecuteNonQuery(deleteCashSavingsPortfolioCmd) != 0)
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
                FunctionInfo.Add("Method", "CashAndSavingsBo.cs:DeleteCashSavingsPortfolio()");
                object[] objects = new object[2];
                objects[0] = cashSavingsId;
                objects[1] = accountId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }
    }
}
