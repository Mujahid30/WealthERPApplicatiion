using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data.Sql;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data.SqlClient;
using VoCustomerPortfolio;

namespace DaoCustomerPortfolio
{
    public class LiabilitiesDao
    {
        #region Joshan Code - Created 16/9/2009

        public DataSet GetLoanScheme(int loanTypeId, int loanPartnerId, int rmId)
        {
            DataSet dsLoanScheme;
            Database db;
            DbCommand getLoanSchemeCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanSchemeCmd = db.GetStoredProcCommand("SP_LoanGetScheme");
                db.AddInParameter(getLoanSchemeCmd, "@XLT_LoanTypeCode", DbType.Int32, loanTypeId);
                db.AddInParameter(getLoanSchemeCmd, "@XLP_LoanPartnerCode", DbType.Int32, loanPartnerId);
                db.AddInParameter(getLoanSchemeCmd, "@AR_RMId", DbType.Int32, rmId);

                dsLoanScheme = db.ExecuteDataSet(getLoanSchemeCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetLoanScheme()");
                object[] objects = new object[3];
                objects[0] = loanTypeId;
                objects[1] = loanPartnerId;
                objects[2] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLoanScheme;
        }

        public DataSet GetClientListForScheme(int loanSchemeId)//, int loanType
        {
            DataSet dsClientListForScheme;
            Database db;
            DbCommand getClientListForSchemeCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getClientListForSchemeCmd = db.GetStoredProcCommand("SP_LoanGetSchemeClientList_EligiCriteria");
                db.AddInParameter(getClientListForSchemeCmd, "@ALS_LoanSchemeId", DbType.Int32, loanSchemeId);
                //db.AddInParameter(getClientListForSchemeCmd, "@LoanTypeCode", DbType.Int32, loanSchemeId);
                dsClientListForScheme = db.ExecuteDataSet(getClientListForSchemeCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetClientListForScheme()");
                object[] objects = new object[1];
                objects[0] = loanSchemeId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsClientListForScheme;
        }

        public DataSet GetLoanSchemeEligilityCriteria(int schemeId)
        {
            DataSet dsSchemeEligilityCriteria;
            Database db;
            DbCommand getSchemeEligilityCriteriaCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSchemeEligilityCriteriaCmd = db.GetStoredProcCommand("SP_LoanGetSchemeEligiCriteria");
                db.AddInParameter(getSchemeEligilityCriteriaCmd, "@ALS_LoanSchemeId", DbType.Int32, schemeId);
                dsSchemeEligilityCriteria = db.ExecuteDataSet(getSchemeEligilityCriteriaCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetClientListForScheme()");
                object[] objects = new object[1];
                objects[0] = schemeId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSchemeEligilityCriteria;
        }

        public DataSet GetLoanCustEligibiltyCriteria(int custId, List<int> borrowerIds, out List<BorrowerLoanEligilityVo> bEligibilityVoList)
        {
            DataSet dsMainCustEligibiltyCriteria = null;
            DataSet dsBorrowerEligibilityCriteria = null;
            bEligibilityVoList = new List<BorrowerLoanEligilityVo>();
            BorrowerLoanEligilityVo bEligibilityVo;
            Database db;
            DbCommand getMainCustEligibiltyCriteriaCmd;
            DbCommand getBorrowerEligibiltyCriteriaCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMainCustEligibiltyCriteriaCmd = db.GetStoredProcCommand("SP_LoanGetCustomerEligilityCriteria");
                db.AddInParameter(getMainCustEligibiltyCriteriaCmd, "@C_CustomerId", DbType.Int32, custId);
                db.AddInParameter(getMainCustEligibiltyCriteriaCmd, "@IsMain", DbType.Int16, 1);
                dsMainCustEligibiltyCriteria = db.ExecuteDataSet(getMainCustEligibiltyCriteriaCmd);

                if (borrowerIds != null)
                {
                    for (int i = 0; i < borrowerIds.Count; i++)
                    {
                        getBorrowerEligibiltyCriteriaCmd = db.GetStoredProcCommand("SP_LoanGetCustomerEligilityCriteria");
                        db.AddInParameter(getBorrowerEligibiltyCriteriaCmd, "@C_CustomerId", DbType.Int32, borrowerIds[i]);
                        db.AddInParameter(getBorrowerEligibiltyCriteriaCmd, "@IsMain", DbType.Int16, 0);
                        dsBorrowerEligibilityCriteria = db.ExecuteDataSet(getBorrowerEligibiltyCriteriaCmd);

                        if (dsBorrowerEligibilityCriteria.Tables[0].Rows.Count > 0)
                        {
                            bEligibilityVo = new BorrowerLoanEligilityVo();

                            bEligibilityVo.Income = Convert.ToDouble(dsBorrowerEligibilityCriteria.Tables[0].Rows[0]["Income"].ToString());
                            bEligibilityVo.Expense = Convert.ToDouble(dsBorrowerEligibilityCriteria.Tables[0].Rows[0]["Expense"].ToString());
                            bEligibilityVo.NetWorth = Convert.ToDouble(dsBorrowerEligibilityCriteria.Tables[0].Rows[0]["NetWorth"].ToString());

                            bEligibilityVoList.Add(bEligibilityVo);
                        }
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetClientListForScheme()");
                object[] objects = new object[2];
                objects[0] = custId;
                objects[1] = borrowerIds;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsMainCustEligibiltyCriteria;
        }

        public DataSet GetLoanProcessDetails(int clientId, int loanTypeId, int loanPartnerId, int schemeId, int staffId)
        {
            DataSet dsLoanProcessDetails;
            Database db;
            DbCommand getLoanProcessDetailsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanProcessDetailsCmd = db.GetStoredProcCommand("SP_LoanGetProcessDetails");
                db.AddInParameter(getLoanProcessDetailsCmd, "@C_CustomerId", DbType.Int32, clientId);
                db.AddInParameter(getLoanProcessDetailsCmd, "@LoanTypeId", DbType.Int32, loanTypeId);
                db.AddInParameter(getLoanProcessDetailsCmd, "@LoanPartnerId", DbType.Int32, loanPartnerId);
                db.AddInParameter(getLoanProcessDetailsCmd, "@LoanSchemeId", DbType.Int32, schemeId);
                db.AddInParameter(getLoanProcessDetailsCmd, "@AR_RMId", DbType.Int32, staffId);

                dsLoanProcessDetails = db.ExecuteDataSet(getLoanProcessDetailsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetClientListForScheme()");
                object[] objects = new object[6];
                objects[0] = clientId;
                objects[1] = loanTypeId;
                objects[2] = loanPartnerId;
                objects[3] = schemeId;
                objects[4] = staffId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLoanProcessDetails;
        }

        public DataSet GetProposalStageDetails(string loanPartnerId, string loanTypeId, string schemeId, string clientId, string associateId, int adviserId)
        {
            DataSet dsEntryStageDetails;
            Database db;
            DbCommand getEntryStageDetailsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEntryStageDetailsCmd = db.GetStoredProcCommand("SP_LoanGetProposalStageDetails");
                db.AddInParameter(getEntryStageDetailsCmd, "@C_CustomerId", DbType.Int32, Int32.Parse(clientId));
                db.AddInParameter(getEntryStageDetailsCmd, "@LoanTypeId", DbType.Int32, Int32.Parse(loanTypeId));
                db.AddInParameter(getEntryStageDetailsCmd, "@LoanPartnerId", DbType.Int32, Int32.Parse(loanPartnerId));
                db.AddInParameter(getEntryStageDetailsCmd, "@LoanSchemeId", DbType.Int32, Int32.Parse(schemeId));
                db.AddInParameter(getEntryStageDetailsCmd, "@A_AdviserId", DbType.Int32, adviserId);
                if (associateId != "")
                    db.AddInParameter(getEntryStageDetailsCmd, "@AR_RMId", DbType.Int32, Int32.Parse(associateId));
                else
                    db.AddInParameter(getEntryStageDetailsCmd, "@AR_RMId", DbType.Int32, DBNull.Value);
                dsEntryStageDetails = db.ExecuteDataSet(getEntryStageDetailsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetClientListForScheme()");
                object[] objects = new object[6];
                objects[0] = clientId;
                objects[1] = loanTypeId;
                objects[2] = loanPartnerId;
                objects[3] = schemeId;
                objects[4] = adviserId;
                objects[5] = associateId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsEntryStageDetails;

        }

        public DataSet GetLoanSchemeDetails(int SchemeId)
        {
            DataSet dsGetSchemeDetails;
            Database db;
            DbCommand getSchemeDetailsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSchemeDetailsCmd = db.GetStoredProcCommand("SP_LoanGetSchemeDetails");
                db.AddInParameter(getSchemeDetailsCmd, "@LoanSchemeId", DbType.Int32, SchemeId);
                dsGetSchemeDetails = db.ExecuteDataSet(getSchemeDetailsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetClientListForScheme()");
                object[] objects = new object[1];
                objects[0] = SchemeId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSchemeDetails;
        }

        public DataSet GetInterestDetails(string interestRateId)
        {
            DataSet dsGetInterestDetails;
            Database db;
            DbCommand getInterestDetailsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getInterestDetailsCmd = db.GetStoredProcCommand("SP_LoanGetInterestDetails");
                db.AddInParameter(getInterestDetailsCmd, "@InterestRateId", DbType.Int32, Int32.Parse(interestRateId));
                dsGetInterestDetails = db.ExecuteDataSet(getInterestDetailsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetInterestDetails()");
                object[] objects = new object[1];
                objects[0] = interestRateId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetInterestDetails;
        }

        public List<PropertyVo> GetCustomerAndAssociateProperty(int clientId, List<int> borrowerIds)
        {
            DataSet dsGetCustomerAndAssociateProperty = null;
            PropertyVo propertyVo;
            Database db;
            DbCommand getCustomerAndAssociatePropertyCmd;
            List<PropertyVo> propertyListVo = new List<PropertyVo>();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                if (borrowerIds != null)
                {
                    for (int i = 0; i < borrowerIds.Count; i++)
                    {
                        getCustomerAndAssociatePropertyCmd = db.GetStoredProcCommand("SP_LoanGetCustomerAndAssociateProperty");
                        db.AddInParameter(getCustomerAndAssociatePropertyCmd, "@C_CustomerId", DbType.Int32, clientId);
                        db.AddInParameter(getCustomerAndAssociatePropertyCmd, "@BorrowerId", DbType.Int32, borrowerIds[i]);
                        dsGetCustomerAndAssociateProperty = db.ExecuteDataSet(getCustomerAndAssociatePropertyCmd);

                        if (dsGetCustomerAndAssociateProperty.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsGetCustomerAndAssociateProperty.Tables[0].Rows.Count; j++)
                            {
                                propertyVo = new PropertyVo();
                                propertyVo.PropertyId = Int32.Parse(dsGetCustomerAndAssociateProperty.Tables[0].Rows[j]["CPNP_PropertyNPId"].ToString());
                                propertyVo.Name = dsGetCustomerAndAssociateProperty.Tables[0].Rows[j]["CPNP_Name"].ToString();
                                propertyListVo.Add(propertyVo);
                            }
                        }
                    }
                }
                else
                {
                    getCustomerAndAssociatePropertyCmd = db.GetStoredProcCommand("SP_GetLoanAgainstProperties");
                    db.AddInParameter(getCustomerAndAssociatePropertyCmd, "@CustomerId", DbType.Int32, clientId);
                    dsGetCustomerAndAssociateProperty = db.ExecuteDataSet(getCustomerAndAssociatePropertyCmd);

                    if (dsGetCustomerAndAssociateProperty.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dsGetCustomerAndAssociateProperty.Tables[0].Rows.Count; j++)
                        {
                            propertyVo = new PropertyVo();
                            propertyVo.PropertyId = Int32.Parse(dsGetCustomerAndAssociateProperty.Tables[0].Rows[j]["CPNP_PropertyNPId"].ToString());
                            propertyVo.Name = dsGetCustomerAndAssociateProperty.Tables[0].Rows[j]["CPNP_Name"].ToString();
                            propertyListVo.Add(propertyVo);
                        }
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetCustomerAndAssociateProperty()");
                object[] objects = new object[2];
                objects[0] = clientId;
                objects[1] = borrowerIds;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return propertyListVo;
        }

        public List<EQPortfolioVo> GetCustomerAndAssociateShares(int clientId, DateTime dtLatestValuation)
        {
            DataSet dsGetCustomerAndAssociateShares = null;
            EQPortfolioVo EQPortfolioVo;
            Database db;
            DbCommand getCustomerAndAssociateSharesCmd;
            List<EQPortfolioVo> EQPortfolioListVo = new List<EQPortfolioVo>();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                getCustomerAndAssociateSharesCmd = db.GetStoredProcCommand("SP_LoanGetCustomerShares");
                db.AddInParameter(getCustomerAndAssociateSharesCmd, "@C_CustomerId", DbType.Int32, clientId);
                db.AddInParameter(getCustomerAndAssociateSharesCmd, "@CENP_ValuationDate", DbType.DateTime, dtLatestValuation);
                dsGetCustomerAndAssociateShares = db.ExecuteDataSet(getCustomerAndAssociateSharesCmd);

                if (dsGetCustomerAndAssociateShares.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < dsGetCustomerAndAssociateShares.Tables[0].Rows.Count; j++)
                    {
                        EQPortfolioVo = new EQPortfolioVo();
                        EQPortfolioVo.EQNetPositionId = Int32.Parse(dsGetCustomerAndAssociateShares.Tables[0].Rows[j]["CENP_EquityNPId"].ToString());
                        EQPortfolioVo.ScripName = dsGetCustomerAndAssociateShares.Tables[0].Rows[j]["ScripName"].ToString();
                        EQPortfolioListVo.Add(EQPortfolioVo);
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetCustomerAndAssociateShares()");
                object[] objects = new object[1];
                objects[0] = clientId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return EQPortfolioListVo;
        }

        public bool AddLoanProposal(LoanProposalVo loanProposalVo, LoanProposalStageVo loanProposalStageVo, LoanProposalDocVo loanProposalDocVo, int userId, out int loanProposalId, out int liabilitiesId)
        {
            bool blResult = false;
            loanProposalId = 0;
            liabilitiesId = 0;

            Database db;
            DbCommand addLoanProposalCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                addLoanProposalCmd = db.GetStoredProcCommand("SP_LoanAddProposal");

                db.AddInParameter(addLoanProposalCmd, "@LoanPartnerId", DbType.Int32, loanProposalVo.LoanPartnerId);
                db.AddInParameter(addLoanProposalCmd, "@LoanTypeId", DbType.Int32, loanProposalVo.LoanTypeId);
                db.AddInParameter(addLoanProposalCmd, "@LoanSchemeId", DbType.Int32, loanProposalVo.SchemeId);
                db.AddInParameter(addLoanProposalCmd, "@BranchId", DbType.Int32, loanProposalVo.BranchId);
                db.AddInParameter(addLoanProposalCmd, "@IsMinor", DbType.Int16, loanProposalVo.IsMainBorrowerMinor);
                db.AddInParameter(addLoanProposalCmd, "@ApplicationNum", DbType.Int32, loanProposalVo.ApplicationNum);
                db.AddInParameter(addLoanProposalCmd, "@AppliedLoanAmount", DbType.Double, loanProposalVo.AppliedLoanAmount);
                db.AddInParameter(addLoanProposalCmd, "@AppliedLoanPeriod", DbType.Int32, loanProposalVo.AppliedLoanPeriod);
                db.AddInParameter(addLoanProposalCmd, "@Introducer", DbType.String, loanProposalVo.Introducer);
                if (loanProposalVo.SanctionDate != DateTime.MinValue)
                    db.AddInParameter(addLoanProposalCmd, "@SanctionDate", DbType.DateTime, loanProposalVo.SanctionDate);
                else
                    db.AddInParameter(addLoanProposalCmd, "@SanctionDate", DbType.DateTime, DBNull.Value);

                db.AddInParameter(addLoanProposalCmd, "@BankReferenceNum", DbType.String, loanProposalVo.BankReferenceNum);
                db.AddInParameter(addLoanProposalCmd, "@SanctionAmount", DbType.Double, loanProposalVo.SanctionAmount);
                db.AddInParameter(addLoanProposalCmd, "@SanctionInterestRate", DbType.Double, loanProposalVo.SanctionInterestRate);
                db.AddInParameter(addLoanProposalCmd, "@EMIAmount", DbType.Double, loanProposalVo.EMIAmount);
                if (loanProposalVo.EMIDate != 0)
                    db.AddInParameter(addLoanProposalCmd, "@EMIDate", DbType.Int32, loanProposalVo.EMIDate);
                else
                    db.AddInParameter(addLoanProposalCmd, "@EMIDate", DbType.Int32, DBNull.Value);

                if (loanProposalVo.RepaymentType != null)
                    db.AddInParameter(addLoanProposalCmd, "@RepaymentType", DbType.String, loanProposalVo.RepaymentType);
                else
                    db.AddInParameter(addLoanProposalCmd, "@RepaymentType", DbType.String, DBNull.Value);

                if (loanProposalVo.EMIFrequency != null)
                    db.AddInParameter(addLoanProposalCmd, "@EMIFrequency", DbType.String, loanProposalVo.EMIFrequency);
                else
                    db.AddInParameter(addLoanProposalCmd, "@EMIFrequency", DbType.String, DBNull.Value);

                db.AddInParameter(addLoanProposalCmd, "@NoOfInstallments", DbType.Int32, loanProposalVo.NoOfInstallments);
                db.AddInParameter(addLoanProposalCmd, "@AmountPrepaid", DbType.Double, loanProposalVo.AmountPrepaid);

                if (loanProposalVo.InstallmentStartDate != DateTime.MinValue)
                    db.AddInParameter(addLoanProposalCmd, "@InstallmentStartDate", DbType.DateTime, loanProposalVo.InstallmentStartDate);
                else
                    db.AddInParameter(addLoanProposalCmd, "@InstallmentStartDate", DbType.DateTime, DBNull.Value);

                if (loanProposalVo.InstallmentStartDate != DateTime.MinValue)
                    db.AddInParameter(addLoanProposalCmd, "@InstallmentEndDate", DbType.DateTime, loanProposalVo.InstallmentEndDate);
                else
                    db.AddInParameter(addLoanProposalCmd, "@InstallmentEndDate", DbType.DateTime, DBNull.Value);

                db.AddInParameter(addLoanProposalCmd, "@Remark", DbType.String, loanProposalVo.Remark);
                db.AddInParameter(addLoanProposalCmd, "@IsFloatingRate", DbType.Int16, loanProposalVo.IsFloatingRate);
                db.AddInParameter(addLoanProposalCmd, "@InterestCategoryId", DbType.Int32, loanProposalVo.InterestCategoryId);

                /* Add Application Stage Parameters */
                db.AddInParameter(addLoanProposalCmd, "@ApplicationProposalStageId", DbType.Int32, loanProposalStageVo.Application_ProposalStageId);
                db.AddInParameter(addLoanProposalCmd, "@ApplicationStageRemark", DbType.String, loanProposalStageVo.Application_Remark);
                db.AddInParameter(addLoanProposalCmd, "@ApplicationDocumentCollection", DbType.Int16, loanProposalStageVo.Application_DocumentCollection);
                db.AddInParameter(addLoanProposalCmd, "@ApplicationEntry", DbType.Int16, loanProposalStageVo.Application_Entry);
                db.AddInParameter(addLoanProposalCmd, "@ApplicationIsOpen", DbType.Int16, loanProposalStageVo.Application_IsOpen);

                db.AddInParameter(addLoanProposalCmd, "@EligibilityIsOpen", DbType.Int16, loanProposalStageVo.Eligibility_IsOpen);
                db.AddInParameter(addLoanProposalCmd, "@BankSanctionIsOpen", DbType.Int16, loanProposalStageVo.BankSanction_IsOpen);
                db.AddInParameter(addLoanProposalCmd, "@DisbursalIsOpen", DbType.Int16, loanProposalStageVo.Disbursal_IsOpen);
                db.AddInParameter(addLoanProposalCmd, "@ClosureIsOpen", DbType.Int16, loanProposalStageVo.Closure_IsOpen);

                db.AddInParameter(addLoanProposalCmd, "@CreatedBy", DbType.Int32, userId);
                db.AddOutParameter(addLoanProposalCmd, "@ALP_LoanProposalId", DbType.Int32, 0);
                db.AddOutParameter(addLoanProposalCmd, "@CL_LiabilitiesId", DbType.Int32, 0);

                if (db.ExecuteNonQuery(addLoanProposalCmd) != 0)
                {
                    loanProposalId = Int32.Parse(db.GetParameterValue(addLoanProposalCmd, "ALP_LoanProposalId").ToString());
                    liabilitiesId = Int32.Parse(db.GetParameterValue(addLoanProposalCmd, "CL_LiabilitiesId").ToString());
                    blResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:AddLoanProposal()");
                object[] objects = new object[2];
                objects[0] = loanProposalVo;
                objects[1] = loanProposalStageVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public DataSet GetRMClientList(int rmId)
        {
            DataSet dsGetRMClientList;
            Database db;
            DbCommand getRMClientListCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRMClientListCmd = db.GetStoredProcCommand("SP_RMCustomersForDDL");
                db.AddInParameter(getRMClientListCmd, "@AR_RMId", DbType.Int32, rmId);
                dsGetRMClientList = db.ExecuteDataSet(getRMClientListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetClientListForScheme()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetRMClientList;
        }

        public DataTable GetCustomerAssociates(int custId)//, int LoanTypeCode
        {
            DataSet dsGetCustomerAssociates;
            Database db;
            DbCommand getRMClientListCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRMClientListCmd = db.GetStoredProcCommand("SP_CustAssociatesForDDL");
                db.AddInParameter(getRMClientListCmd, "@custId", DbType.Int32, custId);
                //db.AddInParameter(getRMClientListCmd, "@loanTypeCode", DbType.Int32, LoanTypeCode);
                dsGetCustomerAssociates = db.ExecuteDataSet(getRMClientListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetCustomerAssociates()");
                object[] objects = new object[1];
                objects[0] = custId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            if (dsGetCustomerAssociates != null)
                return dsGetCustomerAssociates.Tables[0];
            else
                return null;
        }

        public List<LoanProposalVo> GetLoanProposalList(int rmId, int currentPage, out int Count, string sortExpression, string CustName, string LoanTypeId, string LoanStageId, out Dictionary<string, string> genDictLoanType, out Dictionary<string, string> genDictLoanStage)
        {
            List<LoanProposalVo> loanProposalList = null;
            LoanProposalVo loanProposalVo;
            Database db;
            DbCommand getLoanProposalListCmd;
            DataSet getLoanProposalDs;

            genDictLoanType = new Dictionary<string, string>();
            genDictLoanStage = new Dictionary<string, string>();
            Count = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanProposalListCmd = db.GetStoredProcCommand("SP_GetLoanProposalList");
                db.AddInParameter(getLoanProposalListCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(getLoanProposalListCmd, "@CurrentPage", DbType.Int32, currentPage);
                db.AddInParameter(getLoanProposalListCmd, "@SortOrder", DbType.String, sortExpression);

                if (CustName != "")
                    db.AddInParameter(getLoanProposalListCmd, "@nameFilter", DbType.String, CustName);
                else
                    db.AddInParameter(getLoanProposalListCmd, "@nameFilter", DbType.String, DBNull.Value);
                if (LoanTypeId != "")
                    db.AddInParameter(getLoanProposalListCmd, "@loanTypeFilter", DbType.String, LoanTypeId);
                else
                    db.AddInParameter(getLoanProposalListCmd, "@loanTypeFilter", DbType.String, DBNull.Value);
                if (LoanStageId != "")
                    db.AddInParameter(getLoanProposalListCmd, "@loanStageFilter", DbType.String, LoanStageId);
                else
                    db.AddInParameter(getLoanProposalListCmd, "@loanStageFilter", DbType.String, DBNull.Value);

                getLoanProposalDs = db.ExecuteDataSet(getLoanProposalListCmd);

                if (getLoanProposalDs.Tables[0].Rows.Count > 0)
                {
                    loanProposalList = new List<LoanProposalVo>();
                    foreach (DataRow dr in getLoanProposalDs.Tables[0].Rows)
                    {
                        loanProposalVo = new LoanProposalVo();

                        loanProposalVo.LoanProposalId = Int32.Parse(dr["LoanProposalId"].ToString());
                        loanProposalVo.CustomerName = dr["CustomerName"].ToString();
                        loanProposalVo.LoanType = dr["LoanType"].ToString();
                        loanProposalVo.AppliedLoanAmount = Double.Parse(dr["LoanAmount"].ToString());
                        loanProposalVo.LoanPartner = dr["LenderName"].ToString();
                        loanProposalVo.LoanStage = dr["LoanStage"].ToString();
                        loanProposalVo.Remark = dr["Remarks"].ToString();
                        loanProposalVo.Commission = Double.Parse(dr["Commission"].ToString());

                        loanProposalList.Add(loanProposalVo);
                    }
                }
                else
                    loanProposalList = null;

                if (getLoanProposalDs.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in getLoanProposalDs.Tables[2].Rows)
                    {
                        genDictLoanType.Add(dr["LoanType"].ToString(), dr["LoanType"].ToString());
                    }
                }

                if (getLoanProposalDs.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in getLoanProposalDs.Tables[3].Rows)
                    {
                        genDictLoanStage.Add(dr["LoanStage"].ToString(), dr["LoanStage"].ToString());
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
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetLoanProposalList()");
                object[] objects = new object[9];
                objects[0] = rmId;
                objects[1] = currentPage;
                objects[2] = Count;
                objects[3] = sortExpression;
                objects[4] = CustName;
                objects[5] = LoanTypeId;
                objects[6] = LoanStageId;
                objects[7] = genDictLoanType;
                objects[8] = genDictLoanStage;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            if (getLoanProposalDs.Tables[1].Rows.Count > 0)
                Count = Int32.Parse(getLoanProposalDs.Tables[1].Rows[0]["CNT"].ToString());

            return loanProposalList;
        }

        public DataSet GetLoanProposalDetails(int loanProposalId, int rmId)
        {
            Database db;
            DbCommand cmdGetLoanProposalDetails;
            DataSet dsGetLoanProposalDetails;

            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetLoanProposalDetails = db.GetStoredProcCommand("SP_LoanGetProposalDetails");
                db.AddInParameter(cmdGetLoanProposalDetails, "@LoanProposalId", DbType.Int32, loanProposalId);
                db.AddInParameter(cmdGetLoanProposalDetails, "@AR_RMId", DbType.Int32, rmId);
                dsGetLoanProposalDetails = db.ExecuteDataSet(cmdGetLoanProposalDetails);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetLiabilityDetails()");
                object[] objects = new object[1];
                objects[0] = loanProposalId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetLoanProposalDetails;
        }

        public bool UpdateApplicationEntry(LoanApplicationEntryVo loanApplVo, int userId)
        {
            bool blResult = false;

            Database db;
            DbCommand updateApplEntryCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateApplEntryCmd = db.GetStoredProcCommand("SP_LoanStageUpdateStageOne");

                db.AddInParameter(updateApplEntryCmd, "@LoanProposalId", DbType.Int32, loanApplVo.LoanProposalId);
                db.AddInParameter(updateApplEntryCmd, "@ProposalStageId", DbType.Int32, loanApplVo.ProposalStageId);
                db.AddInParameter(updateApplEntryCmd, "@DocumentCollection", DbType.Int16, loanApplVo.DocumentCollection);
                db.AddInParameter(updateApplEntryCmd, "@Entry", DbType.Int16, loanApplVo.Entry);
                db.AddInParameter(updateApplEntryCmd, "@Remark", DbType.String, loanApplVo.Remark);
                db.AddInParameter(updateApplEntryCmd, "@UserId", DbType.Int32, userId);

                if (db.ExecuteNonQuery(updateApplEntryCmd) != 0)
                {
                    blResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:AddLoanProposal()");
                object[] objects = new object[2];
                objects[0] = loanApplVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool UpdateEligility(LoanEligibilityStatusVo loanEligiVo, int userId)
        {
            bool blResult = false;

            Database db;
            DbCommand updateEligibilityCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateEligibilityCmd = db.GetStoredProcCommand("SP_LoanStageUpdateStageTwoThree");

                db.AddInParameter(updateEligibilityCmd, "@LoanProposalId", DbType.Int32, loanEligiVo.LoanProposalId);
                db.AddInParameter(updateEligibilityCmd, "@ProposalStageId", DbType.Int32, loanEligiVo.ProposalStageId);
                db.AddInParameter(updateEligibilityCmd, "@DecisionCode", DbType.String, loanEligiVo.DecisionCode);
                if (loanEligiVo.DeclineReasonCode != 0)
                    db.AddInParameter(updateEligibilityCmd, "@DeclineReasonCode", DbType.Int32, loanEligiVo.DeclineReasonCode);
                else
                    db.AddInParameter(updateEligibilityCmd, "@DeclineReasonCode", DbType.Int32, DBNull.Value);
                db.AddInParameter(updateEligibilityCmd, "@Remark", DbType.String, loanEligiVo.Remark);
                db.AddInParameter(updateEligibilityCmd, "@UserId", DbType.Int32, userId);
                db.AddInParameter(updateEligibilityCmd, "@StageId", DbType.Int32, 2);

                if (db.ExecuteNonQuery(updateEligibilityCmd) != 0)
                {
                    blResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:UpdateEligility()");
                object[] objects = new object[2];
                objects[0] = loanEligiVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool UpdateLoanSanction(LoanSanctionVo loanSancVo, int userId)
        {
            bool blResult = false;

            Database db;
            DbCommand updateSanctionCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateSanctionCmd = db.GetStoredProcCommand("SP_LoanStageUpdateStageTwoThree");

                db.AddInParameter(updateSanctionCmd, "@LoanProposalId", DbType.Int32, loanSancVo.LoanProposalId);
                db.AddInParameter(updateSanctionCmd, "@ProposalStageId", DbType.Int32, loanSancVo.ProposalStageId);
                db.AddInParameter(updateSanctionCmd, "@DecisionCode", DbType.String, loanSancVo.DecisionCode);
                if (loanSancVo.DeclineReasonCode != 0)
                    db.AddInParameter(updateSanctionCmd, "@DeclineReasonCode", DbType.Int32, loanSancVo.DeclineReasonCode);
                else
                    db.AddInParameter(updateSanctionCmd, "@DeclineReasonCode", DbType.Int32, DBNull.Value);
                db.AddInParameter(updateSanctionCmd, "@Remark", DbType.String, loanSancVo.Remark);
                db.AddInParameter(updateSanctionCmd, "@UserId", DbType.Int32, userId);
                db.AddInParameter(updateSanctionCmd, "@StageId", DbType.Int32, 3);

                if (db.ExecuteNonQuery(updateSanctionCmd) != 0)
                {
                    blResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:UpdateLoanSanction()");
                object[] objects = new object[2];
                objects[0] = loanSancVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool UpdateLoanDisbursed(LoanDisbursalVo loanDisVo, int userId)
        {
            bool blResult = false;

            Database db;
            DbCommand updateDisbursedCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateDisbursedCmd = db.GetStoredProcCommand("SP_LoanStageUpdateStageFourFive");

                db.AddInParameter(updateDisbursedCmd, "@LoanProposalId", DbType.Int32, loanDisVo.LoanProposalId);
                db.AddInParameter(updateDisbursedCmd, "@ProposalStageId", DbType.Int32, loanDisVo.ProposalStageId);
                db.AddInParameter(updateDisbursedCmd, "@IsOpen", DbType.Int16, loanDisVo.IsOpen);
                db.AddInParameter(updateDisbursedCmd, "@Remark", DbType.String, loanDisVo.Remark);
                db.AddInParameter(updateDisbursedCmd, "@UserId", DbType.Int32, userId);
                db.AddInParameter(updateDisbursedCmd, "@StageId", DbType.Int32, 4);

                if (db.ExecuteNonQuery(updateDisbursedCmd) != 0)
                {
                    blResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:UpdateLoanDisbursed()");
                object[] objects = new object[2];
                objects[0] = loanDisVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool UpdateLoanClosure(LoanClosureVo loanClosureVo, int userId)
        {
            bool blResult = false;

            Database db;
            DbCommand updateClosureCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateClosureCmd = db.GetStoredProcCommand("SP_LoanStageUpdateStageFourFive");

                db.AddInParameter(updateClosureCmd, "@LoanProposalId", DbType.Int32, loanClosureVo.LoanProposalId);
                db.AddInParameter(updateClosureCmd, "@ProposalStageId", DbType.Int32, loanClosureVo.ProposalStageId);
                db.AddInParameter(updateClosureCmd, "@IsOpen", DbType.Int16, loanClosureVo.IsOpen);
                db.AddInParameter(updateClosureCmd, "@Remark", DbType.String, loanClosureVo.Remark);
                db.AddInParameter(updateClosureCmd, "@UserId", DbType.Int32, userId);
                db.AddInParameter(updateClosureCmd, "@StageId", DbType.Int32, 5);

                if (db.ExecuteNonQuery(updateClosureCmd) != 0)
                {
                    blResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:UpdateLoanClosure()");
                object[] objects = new object[2];
                objects[0] = loanClosureVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool UpdateLoanProposal(LoanProposalVo loanProposalVo, int userId)
        {
            bool blResult = false;

            Database db;
            DbCommand updateLoanProposalCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateLoanProposalCmd = db.GetStoredProcCommand("SP_LoanUpdateProposal");

                db.AddInParameter(updateLoanProposalCmd, "@LoanProposalId", DbType.Int32, loanProposalVo.LoanProposalId);
                db.AddInParameter(updateLoanProposalCmd, "@LoanPartnerId", DbType.Int32, loanProposalVo.LoanPartnerId);
                db.AddInParameter(updateLoanProposalCmd, "@LoanTypeId", DbType.Int32, loanProposalVo.LoanTypeId);
                db.AddInParameter(updateLoanProposalCmd, "@LoanSchemeId", DbType.Int32, loanProposalVo.SchemeId);
                db.AddInParameter(updateLoanProposalCmd, "@BranchId", DbType.Int32, loanProposalVo.BranchId);
                db.AddInParameter(updateLoanProposalCmd, "@IsMinor", DbType.Int16, loanProposalVo.IsMainBorrowerMinor);
                db.AddInParameter(updateLoanProposalCmd, "@ApplicationNum", DbType.Int32, loanProposalVo.ApplicationNum);
                db.AddInParameter(updateLoanProposalCmd, "@AppliedLoanAmount", DbType.Double, loanProposalVo.AppliedLoanAmount);
                db.AddInParameter(updateLoanProposalCmd, "@AppliedLoanPeriod", DbType.Int32, loanProposalVo.AppliedLoanPeriod);
                db.AddInParameter(updateLoanProposalCmd, "@Introducer", DbType.String, loanProposalVo.Introducer);
                if (loanProposalVo.SanctionDate != DateTime.MinValue)
                    db.AddInParameter(updateLoanProposalCmd, "@SanctionDate", DbType.DateTime, loanProposalVo.SanctionDate);
                else
                    db.AddInParameter(updateLoanProposalCmd, "@SanctionDate", DbType.DateTime, DBNull.Value);

                db.AddInParameter(updateLoanProposalCmd, "@BankReferenceNum", DbType.String, loanProposalVo.BankReferenceNum);
                db.AddInParameter(updateLoanProposalCmd, "@SanctionAmount", DbType.Double, loanProposalVo.SanctionAmount);
                db.AddInParameter(updateLoanProposalCmd, "@SanctionInterestRate", DbType.Double, loanProposalVo.SanctionInterestRate);
                db.AddInParameter(updateLoanProposalCmd, "@EMIAmount", DbType.Double, loanProposalVo.EMIAmount);
                if (loanProposalVo.EMIDate != 0)
                    db.AddInParameter(updateLoanProposalCmd, "@EMIDate", DbType.Int32, loanProposalVo.EMIDate);
                else
                    db.AddInParameter(updateLoanProposalCmd, "@EMIDate", DbType.Int32, DBNull.Value);

                if (loanProposalVo.RepaymentType != null)
                    db.AddInParameter(updateLoanProposalCmd, "@RepaymentType", DbType.String, loanProposalVo.RepaymentType);
                else
                    db.AddInParameter(updateLoanProposalCmd, "@RepaymentType", DbType.String, DBNull.Value);

                if (loanProposalVo.EMIFrequency != null)
                    db.AddInParameter(updateLoanProposalCmd, "@EMIFrequency", DbType.String, loanProposalVo.EMIFrequency);
                else
                    db.AddInParameter(updateLoanProposalCmd, "@EMIFrequency", DbType.String, DBNull.Value);

                db.AddInParameter(updateLoanProposalCmd, "@NoOfInstallments", DbType.Int32, loanProposalVo.NoOfInstallments);
                db.AddInParameter(updateLoanProposalCmd, "@AmountPrepaid", DbType.Double, loanProposalVo.AmountPrepaid);

                if (loanProposalVo.InstallmentStartDate != DateTime.MinValue)
                    db.AddInParameter(updateLoanProposalCmd, "@InstallmentStartDate", DbType.DateTime, loanProposalVo.InstallmentStartDate);
                else
                    db.AddInParameter(updateLoanProposalCmd, "@InstallmentStartDate", DbType.DateTime, DBNull.Value);

                if (loanProposalVo.InstallmentStartDate != DateTime.MinValue)
                    db.AddInParameter(updateLoanProposalCmd, "@InstallmentEndDate", DbType.DateTime, loanProposalVo.InstallmentEndDate);
                else
                    db.AddInParameter(updateLoanProposalCmd, "@InstallmentEndDate", DbType.DateTime, DBNull.Value);

                db.AddInParameter(updateLoanProposalCmd, "@Remark", DbType.String, loanProposalVo.Remark);
                db.AddInParameter(updateLoanProposalCmd, "@IsFloatingRate", DbType.Int16, loanProposalVo.IsFloatingRate);
                db.AddInParameter(updateLoanProposalCmd, "@InterestCategoryId", DbType.Int32, loanProposalVo.InterestCategoryId);
                db.AddInParameter(updateLoanProposalCmd, "@UserId", DbType.Int32, userId);

                if (db.ExecuteNonQuery(updateLoanProposalCmd) != 0)
                {
                    blResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:UpdateLoanProposal()");
                object[] objects = new object[2];
                objects[0] = loanProposalVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public bool DeleteLiabilityAssetAssociation(int liabilityId)
        {

            Database db;
            DbCommand cmdDeleteLiabilityAssetAsso;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdDeleteLiabilityAssetAsso = db.GetStoredProcCommand("SP_LoanDeleteLiabilityAssetAssoc");
                db.AddInParameter(cmdDeleteLiabilityAssetAsso, "@CL_LiabilitiesId", DbType.Int32, liabilityId);

                if (db.ExecuteNonQuery(cmdDeleteLiabilityAssetAsso) != 0)
                {
                    bResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:DeleteLiabilityAssetAssociation()");
                object[] objects = new object[1];
                objects[0] = liabilityId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public DataSet GetDocCustomerDropDown(int loanProposalId)
        {
            Database db;
            DbCommand cmdGetDocCustDropDown;
            DataSet dsGetDocCustDropDown;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetDocCustDropDown = db.GetStoredProcCommand("SP_LoanGetDocCustDropDown");
                db.AddInParameter(cmdGetDocCustDropDown, "@LoanProposalId", DbType.Int32, loanProposalId);
                dsGetDocCustDropDown = db.ExecuteDataSet(cmdGetDocCustDropDown);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetDocCustomerDropDown()");
                object[] objects = new object[1];
                objects[0] = loanProposalId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetDocCustDropDown;
        }

        public DataSet GetCustSubmittedDocs(int liabilitiesAssoId)
        {
            Database db;
            DbCommand cmdGetCustSubmittedDocs;
            DataSet dsGetCustSubmittedDocs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCustSubmittedDocs = db.GetStoredProcCommand("SP_LoanGetCustSubmittedDocs");
                db.AddInParameter(cmdGetCustSubmittedDocs, "@LiabilityAssoId", DbType.Int32, liabilitiesAssoId);
                dsGetCustSubmittedDocs = db.ExecuteDataSet(cmdGetCustSubmittedDocs);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetCustSubmittedDocs()");
                object[] objects = new object[1];
                objects[0] = liabilitiesAssoId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetCustSubmittedDocs;
        }

        public DataSet GetSchemeDocs(int schemeId)
        {
            Database db;
            DbCommand cmdGetSchemeDocs;
            DataSet dsGetSchemeDocs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetSchemeDocs = db.GetStoredProcCommand("SP_LoanGetSchemeRequiredDocs");
                db.AddInParameter(cmdGetSchemeDocs, "@schemeId", DbType.Int32, schemeId);
                dsGetSchemeDocs = db.ExecuteDataSet(cmdGetSchemeDocs);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetCustSubmittedDocs()");
                object[] objects = new object[1];
                objects[0] = schemeId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSchemeDocs;
        }

        public bool SubmitDocumentDetails(LoanProposalDocVo lpdVo, int userId)
        {
            bool blResult = false;

            Database db;
            DbCommand submitDocumentDetailsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                submitDocumentDetailsCmd = db.GetStoredProcCommand("SP_LoanSubmitDocumentDetails");

                db.AddInParameter(submitDocumentDetailsCmd, "@LoanProposalId", DbType.Int32, lpdVo.LoanProposalId);
                db.AddInParameter(submitDocumentDetailsCmd, "@LiabilitiesAssociationId", DbType.Int32, lpdVo.LiabilitiesAssociationId);
                db.AddInParameter(submitDocumentDetailsCmd, "@ProofTypeCode", DbType.Int16, lpdVo.DocProofTypeCode);
                db.AddInParameter(submitDocumentDetailsCmd, "@ProofName", DbType.String, lpdVo.DocProofName);
                db.AddInParameter(submitDocumentDetailsCmd, "@IsAccepted", DbType.Int16, lpdVo.IsAccepted);
                db.AddInParameter(submitDocumentDetailsCmd, "@AcceptedBy", DbType.String, lpdVo.DocAcceptedBy);
                db.AddInParameter(submitDocumentDetailsCmd, "@ProofCopyTypeCode", DbType.String, lpdVo.DocProofCopyTypeCode);
                db.AddInParameter(submitDocumentDetailsCmd, "@UserId", DbType.Int32, userId);

                if (db.ExecuteNonQuery(submitDocumentDetailsCmd) != 0)
                {
                    blResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:SubmitDocumentDetails()");
                object[] objects = new object[2];
                objects[0] = lpdVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool UpdateDocumentDetails(LoanProposalDocVo lpdVo, int userId)
        {
            bool blResult = false;

            Database db;
            DbCommand updateDocumentDetailsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateDocumentDetailsCmd = db.GetStoredProcCommand("SP_LoanUpdateDocumentDetails");

                db.AddInParameter(updateDocumentDetailsCmd, "@ProposalDocId", DbType.Int32, lpdVo.ProposalDocId);
                db.AddInParameter(updateDocumentDetailsCmd, "@IsAccepted", DbType.Int16, lpdVo.IsAccepted);
                db.AddInParameter(updateDocumentDetailsCmd, "@AcceptedBy", DbType.String, lpdVo.DocAcceptedBy);
                db.AddInParameter(updateDocumentDetailsCmd, "@CopyTypeCode", DbType.String, lpdVo.DocProofCopyTypeCode);
                db.AddInParameter(updateDocumentDetailsCmd, "@UserId", DbType.Int32, userId);

                if (db.ExecuteNonQuery(updateDocumentDetailsCmd) != 0)
                {
                    blResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:UpdateDocumentDetails()");
                object[] objects = new object[2];
                objects[0] = lpdVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool DeleteDocumentDetails(int proposalDocId)
        {
            bool blResult = false;
            Database db;
            DbCommand deleteDocumentDetailsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteDocumentDetailsCmd = db.GetStoredProcCommand("SP_LoanDeleteDocumentDetails");
                db.AddInParameter(deleteDocumentDetailsCmd, "@ProposalDocId", DbType.Int32, proposalDocId);
                if (db.ExecuteNonQuery(deleteDocumentDetailsCmd) != 0)
                {
                    blResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:DeleteDocumentDetails()");
                object[] objects = new object[1];
                objects[0] = proposalDocId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool AddDocument(int schemeId, int proofTypeId, string proofName, int liabilitiesAssoId, int loanProposalId)
        {
            bool blResult = false;
            Database db;
            DbCommand addDocumentDetailsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                addDocumentDetailsCmd = db.GetStoredProcCommand("SP_LoanAddProof");
                db.AddInParameter(addDocumentDetailsCmd, "@schemeId", DbType.Int32, schemeId);
                db.AddInParameter(addDocumentDetailsCmd, "@proofTypeId", DbType.Int32, proofTypeId);
                db.AddInParameter(addDocumentDetailsCmd, "@proofName", DbType.String, proofName);

                db.AddInParameter(addDocumentDetailsCmd, "@liabilitiesAssoId", DbType.Int32, liabilitiesAssoId);
                db.AddInParameter(addDocumentDetailsCmd, "@loanProposalId", DbType.String, loanProposalId);

                if (db.ExecuteNonQuery(addDocumentDetailsCmd) != 0)
                {
                    blResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:AddDocument()");
                object[] objects = new object[5];
                objects[0] = schemeId;
                objects[1] = proofTypeId;
                objects[2] = proofName;
                objects[3] = liabilitiesAssoId;
                objects[4] = loanProposalId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public DataSet GetCustAdditionalDocs(int loanAssocId)
        {
            Database db;
            DbCommand cmdGetAdditionalDocs;
            DataSet dsGetAdditionalDocs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetAdditionalDocs = db.GetStoredProcCommand("SP_LoanGetCustAdditionalDocs");
                db.AddInParameter(cmdGetAdditionalDocs, "@liabilitiesAssoId", DbType.Int32, loanAssocId);
                dsGetAdditionalDocs = db.ExecuteDataSet(cmdGetAdditionalDocs);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetCustAdditionalDocs()");
                object[] objects = new object[1];
                objects[0] = loanAssocId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAdditionalDocs;
        }

        public bool AddCustAdditionalDocs(LoanProposalDocVo loanProposalDocVo, int userId)
        {
            bool blResult = false;
            Database db;
            DbCommand addCustAdditionalDocsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                addCustAdditionalDocsCmd = db.GetStoredProcCommand("SP_LoanAddCustAdditionalProof");
                db.AddInParameter(addCustAdditionalDocsCmd, "@loanProposalId", DbType.Int32, loanProposalDocVo.LoanProposalId);
                db.AddInParameter(addCustAdditionalDocsCmd, "@liabilitiesAssocId", DbType.Int32, loanProposalDocVo.LiabilitiesAssociationId);
                db.AddInParameter(addCustAdditionalDocsCmd, "@proofTypeCode", DbType.Int32, loanProposalDocVo.DocProofTypeCode);
                db.AddInParameter(addCustAdditionalDocsCmd, "@proofCopyTypeCode", DbType.String, loanProposalDocVo.DocProofCopyTypeCode);
                db.AddInParameter(addCustAdditionalDocsCmd, "@proofName", DbType.String, loanProposalDocVo.DocProofName);
                db.AddInParameter(addCustAdditionalDocsCmd, "@submissionDate", DbType.DateTime, loanProposalDocVo.DocSubmissionDate);
                if (loanProposalDocVo.IsAccepted == 1)
                {
                    db.AddInParameter(addCustAdditionalDocsCmd, "@acceptedDate", DbType.DateTime, loanProposalDocVo.DocAcceptedDate);
                    db.AddInParameter(addCustAdditionalDocsCmd, "@acceptedBy", DbType.String, loanProposalDocVo.DocAcceptedBy);
                    db.AddInParameter(addCustAdditionalDocsCmd, "@isAccepted", DbType.Int16, loanProposalDocVo.IsAccepted);
                }
                else
                {
                    db.AddInParameter(addCustAdditionalDocsCmd, "@acceptedDate", DbType.DateTime, DBNull.Value);
                    db.AddInParameter(addCustAdditionalDocsCmd, "@acceptedBy", DbType.String, DBNull.Value);
                    db.AddInParameter(addCustAdditionalDocsCmd, "@isAccepted", DbType.Int16, loanProposalDocVo.IsAccepted);
                }

                db.AddInParameter(addCustAdditionalDocsCmd, "@userId", DbType.Int32, userId);

                if (db.ExecuteNonQuery(addCustAdditionalDocsCmd) != 0)
                {
                    blResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:AddDocument()");
                object[] objects = new object[2];
                objects[0] = loanProposalDocVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool UpdateCustAdditionalDocs(LoanProposalDocVo loanProposalDocVo, int userId)
        {
            bool blResult = false;
            Database db;
            DbCommand updateCustAdditionalDocsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCustAdditionalDocsCmd = db.GetStoredProcCommand("SP_LoanUpdateCustAdditionalProof");
                db.AddInParameter(updateCustAdditionalDocsCmd, "@proposalDocId", DbType.Int32, loanProposalDocVo.ProposalDocId);
                //db.AddInParameter(updateCustAdditionalDocsCmd, "@loanProposalId", DbType.Int32, loanProposalDocVo.LoanProposalId);
                //db.AddInParameter(updateCustAdditionalDocsCmd, "@liabilitiesAssocId", DbType.Int32, loanProposalDocVo.LiabilitiesAssociationId);
                db.AddInParameter(updateCustAdditionalDocsCmd, "@proofTypeCode", DbType.String, loanProposalDocVo.DocProofTypeCode);
                db.AddInParameter(updateCustAdditionalDocsCmd, "@proofCopyTypeCode", DbType.String, loanProposalDocVo.DocProofCopyTypeCode);
                db.AddInParameter(updateCustAdditionalDocsCmd, "@proofName", DbType.String, loanProposalDocVo.DocProofName);
                db.AddInParameter(updateCustAdditionalDocsCmd, "@submissionDate", DbType.DateTime, loanProposalDocVo.DocSubmissionDate);
                if (loanProposalDocVo.IsAccepted == 1)
                {
                    db.AddInParameter(updateCustAdditionalDocsCmd, "@acceptedDate", DbType.DateTime, loanProposalDocVo.DocAcceptedDate);
                    db.AddInParameter(updateCustAdditionalDocsCmd, "@acceptedBy", DbType.String, loanProposalDocVo.DocAcceptedBy);
                    db.AddInParameter(updateCustAdditionalDocsCmd, "@isAccepted", DbType.Int16, loanProposalDocVo.IsAccepted);
                }
                else
                {
                    db.AddInParameter(updateCustAdditionalDocsCmd, "@acceptedDate", DbType.DateTime, DBNull.Value);
                    db.AddInParameter(updateCustAdditionalDocsCmd, "@acceptedBy", DbType.String, DBNull.Value);
                    db.AddInParameter(updateCustAdditionalDocsCmd, "@isAccepted", DbType.Int16, loanProposalDocVo.IsAccepted);
                }
                db.AddInParameter(updateCustAdditionalDocsCmd, "@userId", DbType.Int32, userId);

                if (db.ExecuteNonQuery(updateCustAdditionalDocsCmd) != 0)
                {
                    blResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:UpdateCustAdditionalDocs()");
                object[] objects = new object[2];
                objects[0] = loanProposalDocVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        #endregion

        #region robin
        /// <summary>
        /// Returns all the loan schemes for the adviser.
        /// </summary>        
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static DataSet GetLoanSchemes(int page, out int count)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;
            count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Get all loan schemes for the adviser.
                cmd = db.GetStoredProcCommand("SP_LoanGetSchemes");

                //db.AddInParameter(cmd, "@AdviserId", DbType.Int32, advisorId);
                db.AddInParameter(cmd, "@CurrentPage", DbType.Int32, page);
                db.AddOutParameter(cmd, "@Count", DbType.Int32, count);

                ds = db.ExecuteDataSet(cmd);

                //Get the total  number of loan schemes for the customer.
                Object objCount = db.GetParameterValue(cmd, "@Count");
                if (objCount != DBNull.Value)
                    count = Convert.ToInt32(objCount);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("GetLoanSchemes", "LiabilitiesDao.cs:GetLoanSchemes()");

                object[] objects = new object[1];
                objects[0] = "SuperAdmin";

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;


        }
        public static SqlDataReader GetSchemeDetails(int schemeId)
        {
            Database db;
            DbCommand cmd;
            SqlDataReader sdr = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_LoanSchemeGetSchemeDetails");
                db.AddInParameter(cmd, "@ALS_LoanSchemeId", DbType.Int32, schemeId);
                sdr = (SqlDataReader)db.ExecuteReader(cmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LiabiliiesDao.cs:GetSchemeDetails()");

                object[] objects = new object[1];
                objects[0] = schemeId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return sdr;


        }

        public static DataSet GetInterestRateBySchemeId(int schemeId)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_LoanSchemeGetInterestRate");
                db.AddInParameter(cmd, "@SchemeId", DbType.Int32, schemeId);

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

                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetInterestRateBySchemeId()");

                object[] objects = new object[1];
                objects[0] = schemeId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;


        }
        public static bool UpdateInterestRate(SchemeInterestRateVo interestRateVo)
        {
            Database db;
            DbCommand updateCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SP_LoanSchemeUpdateInterestRate");

                db.AddInParameter(updateCmd, "@LoanSchemeInterestRateId", DbType.Int32, interestRateVo.LoanSchemeInterestRateId);
                db.AddInParameter(updateCmd, "@DifferentialInterestRate", DbType.Double, interestRateVo.DifferentialInterestRate);
                db.AddInParameter(updateCmd, "@InterestCategory", DbType.String, interestRateVo.InterestCategory);
                db.AddInParameter(updateCmd, "@MaximumFinance", DbType.Double, interestRateVo.MaximumFinance);
                db.AddInParameter(updateCmd, "@MaximumFinancePer", DbType.Double, interestRateVo.MaximumFinancePer);
                db.AddInParameter(updateCmd, "@MaximumPeriod", DbType.Int32, interestRateVo.MaximumPeriod);
                db.AddInParameter(updateCmd, "@MinimumFinance", DbType.Double, interestRateVo.MinimumFinance);
                db.AddInParameter(updateCmd, "@MinimumPeriod", DbType.Int32, interestRateVo.MinimumPeriod);
                db.AddInParameter(updateCmd, "@ModifiedBy", DbType.Int32, interestRateVo.ModifiedBy);

                db.AddInParameter(updateCmd, "@PreClosingCharges", DbType.Double, interestRateVo.PreClosingCharges);
                db.AddInParameter(updateCmd, "@ProcessingCharges", DbType.Double, interestRateVo.ProcessingCharges);

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

                FunctionInfo.Add("Method", "LiabilitiesDao.cs:UpdateInterestRate()");

                object[] objects = new object[1];
                objects[0] = interestRateVo;

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
        public static bool AddInterestRate(SchemeInterestRateVo interestRateVo)
        {
            Database db;
            DbCommand updateCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SP_LoanSchemeCreateInterestRate");

                db.AddInParameter(updateCmd, "@LoanSchemeId", DbType.Double, interestRateVo.LoanSchemeId);
                db.AddInParameter(updateCmd, "@DifferentialInterestRate", DbType.Double, interestRateVo.DifferentialInterestRate);
                db.AddInParameter(updateCmd, "@InterestCategory", DbType.String, interestRateVo.InterestCategory);
                db.AddInParameter(updateCmd, "@MaximumFinance", DbType.Double, interestRateVo.MaximumFinance);
                db.AddInParameter(updateCmd, "@MaximumFinancePer", DbType.Double, interestRateVo.MaximumFinancePer);
                db.AddInParameter(updateCmd, "@MaximumPeriod", DbType.Int32, interestRateVo.MaximumPeriod);
                db.AddInParameter(updateCmd, "@MinimumFinance", DbType.Double, interestRateVo.MinimumFinance);
                db.AddInParameter(updateCmd, "@MinimumPeriod", DbType.Int32, interestRateVo.MinimumPeriod);
                db.AddInParameter(updateCmd, "@CreatedBy", DbType.Int32, interestRateVo.CreatedBy);

                db.AddInParameter(updateCmd, "@PreClosingCharges", DbType.Double, interestRateVo.PreClosingCharges);
                db.AddInParameter(updateCmd, "@ProcessingCharges", DbType.Double, interestRateVo.ProcessingCharges);

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

                FunctionInfo.Add("Method", "LiabilitiesDao.cs:UpdateInterestRate()");

                object[] objects = new object[1];
                objects[0] = interestRateVo;

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

        public static int CreateScheme(SchemeDetailsVo schemeDetailsVo)
        {
            Database db;
            DbCommand updateCmd;
            int affectedRows = 0;
            int schemeId = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SP_LoanSchemeCreateScheme");
                //db.AddInParameter(updateCmd, "@A_AdviserId", DbType.Int32, schemeDetailsVo.AdviserId);
                db.AddInParameter(updateCmd, "@ALS_CreatedBy", DbType.Int32, schemeDetailsVo.CreatedBy);
                db.AddInParameter(updateCmd, "@ALS_IsFloatingRateInterest", DbType.Int16, schemeDetailsVo.IsFloatingRateInterest);
                db.AddInParameter(updateCmd, "@ALS_LoanSchemeName", DbType.String, schemeDetailsVo.LoanSchemeName);
                db.AddInParameter(updateCmd, "@ALS_MarginMaintained", DbType.Double, schemeDetailsVo.MarginMaintained);
                db.AddInParameter(updateCmd, "@ALS_MaximumAge", DbType.Int32, schemeDetailsVo.MaximumAge);
                db.AddInParameter(updateCmd, "@ALS_MaximumLoanAmount", DbType.Double, schemeDetailsVo.MaximumLoanAmount);
                db.AddInParameter(updateCmd, "@ALS_MaximumLoanPeriod", DbType.Int32, schemeDetailsVo.MaximumLoanPeriod);
                db.AddInParameter(updateCmd, "@ALS_MinimumAge", DbType.Int32, schemeDetailsVo.MinimumAge);
                db.AddInParameter(updateCmd, "@ALS_MinimumLoanPeriod", DbType.Int32, schemeDetailsVo.MinimumLoanPeriod);
                db.AddInParameter(updateCmd, "@ALS_MinimumProfitAmount", DbType.Double, schemeDetailsVo.MinimumProfitAmount);
                db.AddInParameter(updateCmd, "@ALS_MinimumProfitPeriod", DbType.Int32, schemeDetailsVo.MinimumProfitPeriod);
                db.AddInParameter(updateCmd, "@ALS_MinimumSalary", DbType.Double, schemeDetailsVo.MinimumSalary);
                db.AddInParameter(updateCmd, "@ALS_MinimunLoanAmount", DbType.Double, schemeDetailsVo.MinimunLoanAmount);
                db.AddInParameter(updateCmd, "@ALS_PLR", DbType.Double, schemeDetailsVo.PLR);
                db.AddInParameter(updateCmd, "@ALS_Remark", DbType.String, schemeDetailsVo.Remark);
                db.AddInParameter(updateCmd, "@ALS_SourceLoanSchemeCode", DbType.String, schemeDetailsVo.SourceLoanSchemeCode);
                db.AddInParameter(updateCmd, "@ALS_SourceName", DbType.String, schemeDetailsVo.SourceName);
                db.AddInParameter(updateCmd, "@XCC_CustomerCategoryCode", DbType.Int32, schemeDetailsVo.CustomerCategory);
                db.AddInParameter(updateCmd, "@XLP_LoanPartnerCode", DbType.Int32, schemeDetailsVo.LoanPartner);
                db.AddInParameter(updateCmd, "@XLT_LoanTypeCode", DbType.Int32, schemeDetailsVo.LoanType);
                db.AddOutParameter(updateCmd, "@SchemeId", DbType.Int32, 10);
                affectedRows = db.ExecuteNonQuery(updateCmd);
                schemeId = Convert.ToInt32(db.GetParameterValue(updateCmd, "@SchemeId"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LiabilitiesDao.cs:CreateScheme()");

                object[] objects = new object[1];
                objects[0] = schemeDetailsVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return schemeId;

        }
        public static bool UpdateScheme(SchemeDetailsVo schemeDetailsVo)
        {
            Database db;
            DbCommand updateCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SP_LoanSchemeUpdateScheme");
                db.AddInParameter(updateCmd, "@ALS_LoanSchemeId", DbType.Int32, schemeDetailsVo.LoanSchemeId);
                db.AddInParameter(updateCmd, "@ALS_IsFloatingRateInterest", DbType.Boolean, schemeDetailsVo.IsFloatingRateInterest);
                db.AddInParameter(updateCmd, "@ALS_LoanSchemeName", DbType.String, schemeDetailsVo.LoanSchemeName);
                db.AddInParameter(updateCmd, "@ALS_MarginMaintained", DbType.Double, schemeDetailsVo.MarginMaintained);
                db.AddInParameter(updateCmd, "@ALS_MaximumAge", DbType.Int32, schemeDetailsVo.MaximumAge);
                db.AddInParameter(updateCmd, "@ALS_MaximumLoanAmount", DbType.Double, schemeDetailsVo.MaximumLoanAmount);
                db.AddInParameter(updateCmd, "@ALS_MaximumLoanPeriod", DbType.Int32, schemeDetailsVo.MaximumLoanPeriod);
                db.AddInParameter(updateCmd, "@ALS_MinimumAge", DbType.Int32, schemeDetailsVo.MinimumAge);
                db.AddInParameter(updateCmd, "@ALS_MinimumLoanPeriod", DbType.Int32, schemeDetailsVo.MinimumLoanPeriod);
                db.AddInParameter(updateCmd, "@ALS_MinimumProfitAmount", DbType.Double, schemeDetailsVo.MinimumProfitAmount);
                db.AddInParameter(updateCmd, "@ALS_MinimumProfitPeriod", DbType.Int32, schemeDetailsVo.MinimumProfitPeriod);
                db.AddInParameter(updateCmd, "@ALS_MinimumSalary", DbType.Double, schemeDetailsVo.MinimumSalary);
                db.AddInParameter(updateCmd, "@ALS_MinimunLoanAmount", DbType.Double, schemeDetailsVo.MinimunLoanAmount);
                db.AddInParameter(updateCmd, "@ALS_ModifiedBy", DbType.Int32, schemeDetailsVo.ModifiedBy);
                db.AddInParameter(updateCmd, "@ALS_PLR", DbType.Double, schemeDetailsVo.PLR);
                db.AddInParameter(updateCmd, "@ALS_Remark", DbType.String, schemeDetailsVo.Remark);
                db.AddInParameter(updateCmd, "@ALS_SourceLoanSchemeCode", DbType.String, schemeDetailsVo.SourceLoanSchemeCode);
                db.AddInParameter(updateCmd, "@ALS_SourceName", DbType.String, schemeDetailsVo.SourceName);
                db.AddInParameter(updateCmd, "@XCC_CustomerCategoryCode", DbType.Int32, schemeDetailsVo.CustomerCategory);
                db.AddInParameter(updateCmd, "@XLP_LoanPartnerCode", DbType.Int32, schemeDetailsVo.LoanPartner);
                db.AddInParameter(updateCmd, "@XLT_LoanTypeCode", DbType.Int32, schemeDetailsVo.LoanType);

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

                FunctionInfo.Add("Method", "LiabilitiesDao.cs:UpdateScheme()");

                object[] objects = new object[1];
                objects[0] = schemeDetailsVo;

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

        public static DataSet GetDocumentsForBorrower(int schemeId, int customerType, int loanTypeCode)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                cmd = db.GetStoredProcCommand("SP_LoanSchemeGetDocumentsForBorrower");
                db.AddInParameter(cmd, "@SchemeId", DbType.Int32, schemeId);
                db.AddInParameter(cmd, "@CustomerType", DbType.Int32, customerType);
                db.AddInParameter(cmd, "@LoanTypeCode", DbType.Int32, loanTypeCode);

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

                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetDocumentsForBorrower()");
                object[] objects = new object[3];
                objects[0] = schemeId;
                objects[1] = customerType;
                objects[2] = loanTypeCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
        public static bool AddProofs(int schemeId, int proofCode, int createdBy)
        {

            Database db;
            DbCommand createCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SP_LoanSchemeCreateProofAssociation");
                db.AddInParameter(createCmd, "@SchemeId", DbType.Int32, schemeId);
                db.AddInParameter(createCmd, "@CreatedBy", DbType.Int32, createdBy);
                db.AddInParameter(createCmd, "@ProofCode", DbType.Int32, proofCode);

                affectedRows = db.ExecuteNonQuery(createCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LiabilitiesDao.cs:AddProofs()");

                object[] objects = new object[3];
                objects[0] = schemeId;
                objects[1] = createdBy;
                objects[2] = proofCode;

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
        public static bool DeleteProofs(int schemeId)
        {
            Database db;
            DbCommand deleteCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteCmd = db.GetStoredProcCommand("SP_LoanSchemeDeleteProofAssociation");
                db.AddInParameter(deleteCmd, "@SchemeId", DbType.Int32, schemeId);

                affectedRows = db.ExecuteNonQuery(deleteCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LiabilitiesDao.cs:DeleteProofs()");

                object[] objects = new object[1];
                objects[0] = schemeId;


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


        public static DataTable GetAllLoanTypes()
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_LoanSchemeGetLoanTypes");
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

                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetAllLoanTypes()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds.Tables[0];




        }
        public static DataTable GetAllCustomerTypes(int loanType)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_LoanSchemeGetCusomerTypes");
                db.AddInParameter(cmd, "@LoanType", DbType.Int32, loanType);
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

                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetAllCustomerTypes()");

                object[] objects = new object[1];
                objects[0] = loanType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds.Tables[0];


        }
        #endregion

        #region SV's COde

        public DataTable GetEditLogInfo(int LiabilityId)
        {
            Database db;
            DbCommand getEditLogInfoCmd;
            DataSet dsGetEditLogInfo;
            DataTable dt = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEditLogInfoCmd = db.GetStoredProcCommand("SP_LoanSchemeGetEditLog");
                db.AddInParameter(getEditLogInfoCmd, "@LiabilityId", DbType.Int32, LiabilityId);

                dsGetEditLogInfo = db.ExecuteDataSet(getEditLogInfoCmd);
                if (dsGetEditLogInfo.Tables[0].Rows.Count > 0)
                {
                    dt = dsGetEditLogInfo.Tables[0];
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetEditLogInfo()");
                object[] objects = new object[1];
                objects[0] = LiabilityId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dt;
        }

        public DataTable GetPropertyAccountAssociates(int PropertyId)
        {
            Database db;
            DbCommand getPropertyAccountAssociatesCmd;
            DataSet dsPropertyAccountAssociates;
            DataTable dt = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getPropertyAccountAssociatesCmd = db.GetStoredProcCommand("SP_LoanSchemeGetPropertyAccountAssociates");
                db.AddInParameter(getPropertyAccountAssociatesCmd, "@PropertyId", DbType.Int32, PropertyId);

                dsPropertyAccountAssociates = db.ExecuteDataSet(getPropertyAccountAssociatesCmd);
                if (dsPropertyAccountAssociates.Tables[0].Rows.Count > 0)
                {
                    dt = dsPropertyAccountAssociates.Tables[0];
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetPropertyAccountAssociates()");
                object[] objects = new object[1];
                objects[0] = PropertyId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dt;
        }

        public int CreatLiabilities(LiabilitiesVo liabilitiesVo)
        {

            Database db;
            DbCommand cmdCreateLiability;
            int LiabilityId = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateLiability = db.GetStoredProcCommand("SP_LoanSchemeCreateLiability");
                db.AddInParameter(cmdCreateLiability, "@XLP_LoanPartnerCode", DbType.Int16, liabilitiesVo.LoanPartnerCode);
                db.AddInParameter(cmdCreateLiability, "@XLT_LoanTypeCode", DbType.Int16, liabilitiesVo.LoanTypeCode);
                db.AddInParameter(cmdCreateLiability, "@CL_IsFloatingRateInterest", DbType.Int16, liabilitiesVo.IsFloatingRateInterest);
                db.AddInParameter(cmdCreateLiability, "@ALP_LoanProposalId", DbType.Int16, DBNull.Value);
                db.AddInParameter(cmdCreateLiability, "@CL_LoanAmount", DbType.Double, liabilitiesVo.LoanAmount);
                db.AddInParameter(cmdCreateLiability, "@CL_RateOfInterest", DbType.Decimal, liabilitiesVo.RateOfInterest);
                db.AddInParameter(cmdCreateLiability, "@CL_EMIAmount", DbType.Double, liabilitiesVo.EMIAmount);
                db.AddInParameter(cmdCreateLiability, "@CL_EMIDate", DbType.Int16, liabilitiesVo.EMIDate);
                db.AddInParameter(cmdCreateLiability, "@CL_NoOfInstallments", DbType.Int32, liabilitiesVo.NoOfInstallments);
                db.AddInParameter(cmdCreateLiability, "@CL_AmountPrepaid", DbType.Double, liabilitiesVo.AmountPrepaid);
                if(!string.IsNullOrEmpty(liabilitiesVo.RepaymentTypeCode))
                    db.AddInParameter(cmdCreateLiability, "@XRT_RepaymentTypeCode", DbType.String, liabilitiesVo.RepaymentTypeCode);
                else
                    db.AddInParameter(cmdCreateLiability, "@XRT_RepaymentTypeCode", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(liabilitiesVo.FrequencyCodeEMI))
                    db.AddInParameter(cmdCreateLiability, "@XF_FrequencyCodeEMI", DbType.String, liabilitiesVo.FrequencyCodeEMI);
                else
                    db.AddInParameter(cmdCreateLiability, "@XF_FrequencyCodeEMI", DbType.String, DBNull.Value);

                if (liabilitiesVo.InstallmentStartDate != DateTime.MinValue)
                    db.AddInParameter(cmdCreateLiability, "@CL_InstallmentStartDate", DbType.DateTime, liabilitiesVo.InstallmentStartDate);
                else
                    db.AddInParameter(cmdCreateLiability, "@CL_InstallmentStartDate", DbType.DateTime, DBNull.Value);
                if (liabilitiesVo.InstallmentEndDate!=DateTime.MinValue)
                    db.AddInParameter(cmdCreateLiability, "@CL_InstallmentEndDate", DbType.DateTime, liabilitiesVo.InstallmentEndDate);
                else
                    db.AddInParameter(cmdCreateLiability, "@CL_InstallmentEndDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(cmdCreateLiability, "@CL_IsInProcess", DbType.Int16, liabilitiesVo.IsInProcess);
                db.AddInParameter(cmdCreateLiability, "@CL_CreatedBy", DbType.Int32, liabilitiesVo.CreatedBy);
                db.AddInParameter(cmdCreateLiability, "@CL_ModifiedBy", DbType.Int32, liabilitiesVo.ModifiedBy);
                db.AddInParameter(cmdCreateLiability, "@CL_CommissionAmount", DbType.Double, liabilitiesVo.CommissionAmount);
                db.AddInParameter(cmdCreateLiability, "@CL_CommissionPer", DbType.Decimal, liabilitiesVo.CommissionPer);
                if(liabilitiesVo.LoanStartDate!=DateTime.MinValue)
                    db.AddInParameter(cmdCreateLiability, "@CL_LoanStartDate", DbType.DateTime, liabilitiesVo.LoanStartDate);
                else
                    db.AddInParameter(cmdCreateLiability, "@CL_LoanStartDate", DbType.DateTime, DBNull.Value);
                if (!string.IsNullOrEmpty(liabilitiesVo.OtherLenderName))
                    db.AddInParameter(cmdCreateLiability, "@CL_OtherLenderName", DbType.String, liabilitiesVo.OtherLenderName);
                else
                    db.AddInParameter(cmdCreateLiability, "@CL_OtherLenderName", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(liabilitiesVo.CompoundFrequency))
                    db.AddInParameter(cmdCreateLiability, "@CL_CompoundFrequency", DbType.String, liabilitiesVo.CompoundFrequency);
                else
                    db.AddInParameter(cmdCreateLiability, "@CL_CompoundFrequency", DbType.String, DBNull.Value);
                db.AddInParameter(cmdCreateLiability, "@XPO_PaymentOptionCode", DbType.Int16, liabilitiesVo.PaymentOptionCode);
                if(liabilitiesVo.InstallmentTypeCode!=0)
                    db.AddInParameter(cmdCreateLiability, "@XIT_InstallmentTypeCode", DbType.Int16, liabilitiesVo.InstallmentTypeCode);
                else
                    db.AddInParameter(cmdCreateLiability, "@XIT_InstallmentTypeCode", DbType.Int16, DBNull.Value);
                db.AddInParameter(cmdCreateLiability, "@CL_LumpsumRepaymentAmount", DbType.Double, liabilitiesVo.LumpsumRepaymentAmount);
                db.AddInParameter(cmdCreateLiability, "@CL_OutstandingAmount", DbType.Double, liabilitiesVo.OutstandingAmount);
                if (!string.IsNullOrEmpty(liabilitiesVo.Guarantor))
                    db.AddInParameter(cmdCreateLiability, "@CL_Guarantor", DbType.String, liabilitiesVo.Guarantor);
                else
                    db.AddInParameter(cmdCreateLiability, "@CL_Guarantor", DbType.String, DBNull.Value);
                db.AddInParameter(cmdCreateLiability, "@CL_Tenure", DbType.Int32, liabilitiesVo.Tenure);

                db.AddOutParameter(cmdCreateLiability, "@LiabilityId", DbType.Int32, 100);
                if (db.ExecuteNonQuery(cmdCreateLiability) != 0)
                {
                    LiabilityId = int.Parse(db.GetParameterValue(cmdCreateLiability, "LiabilityId").ToString());
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:CreateLiabilities()");
                object[] objects = new object[1];
                objects[0] = liabilitiesVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return LiabilityId;
        }

        public List<LiabilitiesVo> GetLiabilities(int customerId)
        {
            List<LiabilitiesVo> liabilitiesList = null;
            LiabilitiesVo liabilitiesVo;
            Database db;
            DbCommand cmdGetLiabilities;
            DataSet dsGetLiabilities;
            DataTable dtGetLiabilities;
            bool Result=false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetLiabilities = db.GetStoredProcCommand("SP_LoanSchemeGetLiabilities");
                db.AddInParameter(cmdGetLiabilities, "@CustomerId", DbType.Int32, customerId);
                dsGetLiabilities = db.ExecuteDataSet(cmdGetLiabilities);
                if (dsGetLiabilities.Tables[0].Rows.Count > 0)
                {
                    dtGetLiabilities = dsGetLiabilities.Tables[0];
                    liabilitiesList = new List<LiabilitiesVo>();
                    foreach (DataRow dr in dtGetLiabilities.Rows)
                    {
                        liabilitiesVo = new LiabilitiesVo();
                        liabilitiesVo.LiabilitiesId = int.Parse(dr["CL_LiabilitiesId"].ToString());
                        liabilitiesVo.LoanType = (dr["XLT_LoanType"].ToString());
                        liabilitiesVo.LoanPartner = dr["XLP_LoanPartner"].ToString();
                        if(!String.IsNullOrEmpty(dr["CL_LoanAmount"].ToString()))
                            liabilitiesVo.LoanAmount = double.Parse(dr["CL_LoanAmount"].ToString());
                        if (!String.IsNullOrEmpty(dr["CL_RateOfInterest"].ToString()))
                            liabilitiesVo.RateOfInterest = float.Parse(dr["CL_RateOfInterest"].ToString());
                        if (!String.IsNullOrEmpty(dr["XLP_LoanPartnerCode"].ToString()))
                            liabilitiesVo.LoanPartnerCode=int.Parse(dr["XLP_LoanPartnerCode"].ToString());
                        if (!String.IsNullOrEmpty(dr["XLT_LoanTypeCode"].ToString()))
			                liabilitiesVo.LoanTypeCode=int.Parse(dr["XLT_LoanTypeCode"].ToString());
                        if (!String.IsNullOrEmpty(dr["CL_IsFloatingRateInterest"].ToString()))
                            liabilitiesVo.IsFloatingRateInterest=int.Parse(dr["CL_IsFloatingRateInterest"].ToString());
                        if (!String.IsNullOrEmpty(dr["ALP_LoanProposalId"].ToString()))
                            liabilitiesVo.LoanProposalId=int.Parse(dr["ALP_LoanProposalId"].ToString());
                        if (!String.IsNullOrEmpty(dr["CL_EMIAmount"].ToString()))
                            liabilitiesVo.EMIAmount=double.Parse(dr["CL_EMIAmount"].ToString());
                        if (!String.IsNullOrEmpty(dr["CL_NoOfInstallments"].ToString()))
                            liabilitiesVo.NoOfInstallments=int.Parse(dr["CL_NoOfInstallments"].ToString());
                        if (!String.IsNullOrEmpty(dr["XRT_RepaymentTypeCode"].ToString()))
                            liabilitiesVo.RepaymentTypeCode=dr["XRT_RepaymentTypeCode"].ToString();
                        if (!String.IsNullOrEmpty(dr["XF_FrequencyCodeEMI"].ToString()))
                            liabilitiesVo.FrequencyCodeEMI=dr["XF_FrequencyCodeEMI"].ToString();
                        if (!String.IsNullOrEmpty(dr["CL_InstallmentStartDate"].ToString()))
                            liabilitiesVo.InstallmentStartDate=DateTime.Parse(dr["CL_InstallmentStartDate"].ToString());
                        if (!String.IsNullOrEmpty(dr["CL_InstallmentEndDate"].ToString()))
                            liabilitiesVo.InstallmentEndDate=DateTime.Parse(dr["CL_InstallmentEndDate"].ToString());
                        if (!String.IsNullOrEmpty(dr["CL_CommissionAmount"].ToString()))
                            liabilitiesVo.CommissionAmount=double.Parse(dr["CL_CommissionAmount"].ToString());
                        if (!String.IsNullOrEmpty(dr["CL_CommissionPer"].ToString()))
                            liabilitiesVo.CommissionPer=float.Parse(dr["CL_CommissionPer"].ToString());
                        if (!String.IsNullOrEmpty(dr["CL_LoanStartDate"].ToString()))
                            liabilitiesVo.LoanStartDate=DateTime.Parse(dr["CL_LoanStartDate"].ToString());
                        if (!String.IsNullOrEmpty(dr["CL_OtherLenderName"].ToString()))
                            liabilitiesVo.OtherLenderName=dr["CL_OtherLenderName"].ToString();
                        if (!String.IsNullOrEmpty(dr["CL_CompoundFrequency"].ToString()))
                            liabilitiesVo.CompoundFrequency=dr["CL_CompoundFrequency"].ToString();
                        if (!String.IsNullOrEmpty(dr["XPO_PaymentOptionCode"].ToString()))
                            liabilitiesVo.PaymentOptionCode=int.Parse(dr["XPO_PaymentOptionCode"].ToString());
                        if (!String.IsNullOrEmpty(dr["XIT_InstallmentTypeCode"].ToString()))
                            liabilitiesVo.InstallmentTypeCode=int.Parse(dr["XIT_InstallmentTypeCode"].ToString());
                        if (!String.IsNullOrEmpty(dr["CL_LumpsumRepaymentAmount"].ToString()))
                            liabilitiesVo.LumpsumRepaymentAmount=double.Parse(dr["CL_LumpsumRepaymentAmount"].ToString());
                        if (!String.IsNullOrEmpty(dr["CL_OutstandingAmount"].ToString()))
                            liabilitiesVo.OutstandingAmount=double.Parse(dr["CL_OutstandingAmount"].ToString());
                        if (!String.IsNullOrEmpty(dr["CL_Guarantor"].ToString()))
                            liabilitiesVo.Guarantor=dr["CL_Guarantor"].ToString();
                        if (!String.IsNullOrEmpty(dr["CL_Tenure"].ToString()))
                            liabilitiesVo.Tenure=int.Parse(dr["CL_Tenure"].ToString());                   
                        
			 
                        liabilitiesList.Add(liabilitiesVo);
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetLiabilities()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return liabilitiesList;
        }

        public bool CreateLiabilityAssociates(LiabilityAssociateVo liabilityAssociateVo)
        {
            bool blResult = false;
            Database db;
            DbCommand cmdCreateLiabilityAssociate;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateLiabilityAssociate = db.GetStoredProcCommand("SP_LoanSchemeCreateLiabilityAssociates");
                db.AddInParameter(cmdCreateLiabilityAssociate, "@CL_LiabilitiesId", DbType.Int32, liabilityAssociateVo.LiabilitiesId);
                db.AddInParameter(cmdCreateLiabilityAssociate, "@XLAT_LoanAssociateCode", DbType.String, liabilityAssociateVo.LoanAssociateCode);
                db.AddInParameter(cmdCreateLiabilityAssociate, "@CLA_LiabilitySharePer", DbType.Decimal, liabilityAssociateVo.LiabilitySharePer);
                db.AddInParameter(cmdCreateLiabilityAssociate, "@CLA_MarginPer", DbType.Decimal, liabilityAssociateVo.MarginPer);
                db.AddInParameter(cmdCreateLiabilityAssociate, "@CA_AssociationId", DbType.Int32, liabilityAssociateVo.AssociationId);
                db.AddInParameter(cmdCreateLiabilityAssociate, "@CLA_CreatedBy", DbType.Int32, liabilityAssociateVo.CreatedBy);
                db.AddInParameter(cmdCreateLiabilityAssociate, "@CLA_ModifiedBy", DbType.Int32, liabilityAssociateVo.ModifiedBy);
                if (db.ExecuteNonQuery(cmdCreateLiabilityAssociate) != 0)
                {
                    blResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:CreateLiabilityAssociates()");
                object[] objects = new object[1];
                objects[0] = liabilityAssociateVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public LiabilitiesVo GetLiabilityDetails(int liabilityId)
        {

            LiabilitiesVo liabilitiesVo = null;
            Database db;
            DbCommand cmdGetLiabilityDetails;
            DataSet dsGetLiabilityDetails;

            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetLiabilityDetails = db.GetStoredProcCommand("SP_LoanSchemeGetLiabilityDetails");
                db.AddInParameter(cmdGetLiabilityDetails, "@LiabilityId", DbType.Int32, liabilityId);
                dsGetLiabilityDetails = db.ExecuteDataSet(cmdGetLiabilityDetails);
                if (dsGetLiabilityDetails.Tables[0].Rows.Count > 0)
                {

                    dr = dsGetLiabilityDetails.Tables[0].Rows[0];

                    liabilitiesVo = new LiabilitiesVo();
                    liabilitiesVo.LiabilitiesId = int.Parse(dr["CL_LiabilitiesId"].ToString());
                    liabilitiesVo.LoanType = (dr["XLT_LoanType"].ToString());
                    liabilitiesVo.LoanPartner = dr["XLP_LoanPartner"].ToString();
                    if (!String.IsNullOrEmpty(dr["CL_LoanAmount"].ToString()))
                        liabilitiesVo.LoanAmount = double.Parse(dr["CL_LoanAmount"].ToString());
                    if (!String.IsNullOrEmpty(dr["CL_RateOfInterest"].ToString()))
                        liabilitiesVo.RateOfInterest = float.Parse(dr["CL_RateOfInterest"].ToString());
                    if (!String.IsNullOrEmpty(dr["XLP_LoanPartnerCode"].ToString()))
                        liabilitiesVo.LoanPartnerCode = int.Parse(dr["XLP_LoanPartnerCode"].ToString());
                    if (!String.IsNullOrEmpty(dr["XLT_LoanTypeCode"].ToString()))
                        liabilitiesVo.LoanTypeCode = int.Parse(dr["XLT_LoanTypeCode"].ToString());
                    if (!String.IsNullOrEmpty(dr["CL_IsFloatingRateInterest"].ToString()))
                        liabilitiesVo.IsFloatingRateInterest = int.Parse(dr["CL_IsFloatingRateInterest"].ToString());
                    if (!String.IsNullOrEmpty(dr["ALP_LoanProposalId"].ToString()))
                        liabilitiesVo.LoanProposalId = int.Parse(dr["ALP_LoanProposalId"].ToString());
                    if (!String.IsNullOrEmpty(dr["CL_EMIAmount"].ToString()))
                        liabilitiesVo.EMIAmount = double.Parse(dr["CL_EMIAmount"].ToString());
                    if (!String.IsNullOrEmpty(dr["CL_NoOfInstallments"].ToString()))
                        liabilitiesVo.NoOfInstallments = int.Parse(dr["CL_NoOfInstallments"].ToString());
                    if (!String.IsNullOrEmpty(dr["XRT_RepaymentTypeCode"].ToString()))
                        liabilitiesVo.RepaymentTypeCode = dr["XRT_RepaymentTypeCode"].ToString();
                    if (!String.IsNullOrEmpty(dr["XF_FrequencyCodeEMI"].ToString()))
                        liabilitiesVo.FrequencyCodeEMI = dr["XF_FrequencyCodeEMI"].ToString();
                    if (!String.IsNullOrEmpty(dr["CL_InstallmentStartDate"].ToString()))
                        liabilitiesVo.InstallmentStartDate = DateTime.Parse(dr["CL_InstallmentStartDate"].ToString());
                    if (!String.IsNullOrEmpty(dr["CL_InstallmentEndDate"].ToString()))
                        liabilitiesVo.InstallmentEndDate = DateTime.Parse(dr["CL_InstallmentEndDate"].ToString());
                    if (!String.IsNullOrEmpty(dr["CL_CommissionAmount"].ToString()))
                        liabilitiesVo.CommissionAmount = double.Parse(dr["CL_CommissionAmount"].ToString());
                    if (!String.IsNullOrEmpty(dr["CL_CommissionPer"].ToString()))
                        liabilitiesVo.CommissionPer = float.Parse(dr["CL_CommissionPer"].ToString());
                    if (!String.IsNullOrEmpty(dr["CL_LoanStartDate"].ToString()))
                        liabilitiesVo.LoanStartDate = DateTime.Parse(dr["CL_LoanStartDate"].ToString());
                    if (!String.IsNullOrEmpty(dr["CL_OtherLenderName"].ToString()))
                        liabilitiesVo.OtherLenderName = dr["CL_OtherLenderName"].ToString();
                    if (!String.IsNullOrEmpty(dr["CL_CompoundFrequency"].ToString()))
                        liabilitiesVo.CompoundFrequency = dr["CL_CompoundFrequency"].ToString();
                    if (!String.IsNullOrEmpty(dr["XPO_PaymentOptionCode"].ToString()))
                        liabilitiesVo.PaymentOptionCode = int.Parse(dr["XPO_PaymentOptionCode"].ToString());
                    if (!String.IsNullOrEmpty(dr["XIT_InstallmentTypeCode"].ToString()))
                        liabilitiesVo.InstallmentTypeCode = int.Parse(dr["XIT_InstallmentTypeCode"].ToString());
                    if (!String.IsNullOrEmpty(dr["CL_LumpsumRepaymentAmount"].ToString()))
                        liabilitiesVo.LumpsumRepaymentAmount = double.Parse(dr["CL_LumpsumRepaymentAmount"].ToString());
                    if (!String.IsNullOrEmpty(dr["CL_OutstandingAmount"].ToString()))
                        liabilitiesVo.OutstandingAmount = double.Parse(dr["CL_OutstandingAmount"].ToString());
                    if (!String.IsNullOrEmpty(dr["CL_Guarantor"].ToString()))
                        liabilitiesVo.Guarantor = dr["CL_Guarantor"].ToString();
                    if (!String.IsNullOrEmpty(dr["CL_Tenure"].ToString()))
                        liabilitiesVo.Tenure = int.Parse(dr["CL_Tenure"].ToString());  
                    
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetLiabilityDetails()");
                object[] objects = new object[1];
                objects[0] = liabilityId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return liabilitiesVo;
        }

        public int CreatEditLogInfo(EditLogVo editLogVo)
        {

            Database db;
            DbCommand cmdCreateEditLogInfo;
            int EditLogId = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateEditLogInfo = db.GetStoredProcCommand("SP_LoanSchemeCreateEditLog");
                db.AddInParameter(cmdCreateEditLogInfo, "@CL_LiabilitiesId", DbType.Int32, editLogVo.LiabilitiesId);
                db.AddInParameter(cmdCreateEditLogInfo, "@XLET_EditTypeCode", DbType.Int32, editLogVo.EditTypeCode);
                db.AddInParameter(cmdCreateEditLogInfo, "@CLEL_EditOccurenceDate", DbType.DateTime, editLogVo.EditOccurenceDate);
                db.AddInParameter(cmdCreateEditLogInfo, "@CLEL_EditAmount", DbType.Double, editLogVo.EditAmount);
                db.AddInParameter(cmdCreateEditLogInfo, "@CLEL_ReferenceNum", DbType.String, editLogVo.ReferenceNum);
                db.AddInParameter(cmdCreateEditLogInfo, "@CLEL_Remark", DbType.String, editLogVo.Remark);
                db.AddInParameter(cmdCreateEditLogInfo, "@CLEL_CreatedBy", DbType.Int32, editLogVo.CreatedBy);
                db.AddInParameter(cmdCreateEditLogInfo, "@CLEL_ModifiedBy", DbType.Int32, editLogVo.ModifiedBy);


                db.AddOutParameter(cmdCreateEditLogInfo, "@CLEL_LiabilitiesEditLogId", DbType.Int32, 100);
                if (db.ExecuteNonQuery(cmdCreateEditLogInfo) != 0)
                {
                    EditLogId = int.Parse(db.GetParameterValue(cmdCreateEditLogInfo, "CLEL_LiabilitiesEditLogId").ToString());
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:CreatEditLogInfo()");
                object[] objects = new object[1];
                objects[0] = editLogVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return EditLogId;
        }

        public bool CreatLiabilityAssetAssociation(AssetAssociationVo assetAssociationVo)
        {

            Database db;
            DbCommand cmdCreatLiabilityAssetAssociation;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreatLiabilityAssetAssociation = db.GetStoredProcCommand("SP_LoanSchemeCreateLiabilityAssetAssociation");
                db.AddInParameter(cmdCreatLiabilityAssetAssociation, "@CL_LiabilitiesId", DbType.Int32, assetAssociationVo.LiabilitiesId);
                db.AddInParameter(cmdCreatLiabilityAssetAssociation, "@CLAA_AssetId", DbType.Int32, assetAssociationVo.AssetId);
                db.AddInParameter(cmdCreatLiabilityAssetAssociation, "@PAG_AssetGroupCode", DbType.String, assetAssociationVo.AssetGroupCode);
                db.AddInParameter(cmdCreatLiabilityAssetAssociation, "@CLAA_AssetTable", DbType.String, assetAssociationVo.AssetTable);
                db.AddInParameter(cmdCreatLiabilityAssetAssociation, "@CLAA_CreatedBy", DbType.Int32, assetAssociationVo.CreatedBy);
                db.AddInParameter(cmdCreatLiabilityAssetAssociation, "@CLAA_ModifiedBy", DbType.Int32, assetAssociationVo.ModifiedBy);

                if (db.ExecuteNonQuery(cmdCreatLiabilityAssetAssociation) != 0)
                {
                    bResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:CreatLiabilityAssetAssociation()");
                object[] objects = new object[1];
                objects[0] = assetAssociationVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public bool UpdateLiabilities(LiabilitiesVo liabilitiesVo)
        {
            Database db;
            DbCommand cmdUpdateLiability;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateLiability = db.GetStoredProcCommand("SP_LoanSchemeUpdateLiability");
                db.AddInParameter(cmdUpdateLiability, "@XLP_LoanPartnerCode", DbType.Int16, liabilitiesVo.LoanPartnerCode);
                db.AddInParameter(cmdUpdateLiability, "@XLT_LoanTypeCode", DbType.Int16, liabilitiesVo.LoanTypeCode);
                db.AddInParameter(cmdUpdateLiability, "@CL_IsFloatingRateInterest", DbType.Int16, liabilitiesVo.IsFloatingRateInterest);
                db.AddInParameter(cmdUpdateLiability, "@ALP_LoanProposalId", DbType.Int16, DBNull.Value);
                db.AddInParameter(cmdUpdateLiability, "@CL_LoanAmount", DbType.Double, liabilitiesVo.LoanAmount);
                db.AddInParameter(cmdUpdateLiability, "@CL_RateOfInterest", DbType.Decimal, liabilitiesVo.RateOfInterest);
                db.AddInParameter(cmdUpdateLiability, "@CL_EMIAmount", DbType.Double, liabilitiesVo.EMIAmount);
                db.AddInParameter(cmdUpdateLiability, "@CL_EMIDate", DbType.Int16, liabilitiesVo.EMIDate);
                db.AddInParameter(cmdUpdateLiability, "@CL_NoOfInstallments", DbType.Int32, liabilitiesVo.NoOfInstallments);
                db.AddInParameter(cmdUpdateLiability, "@CL_AmountPrepaid", DbType.Double, liabilitiesVo.AmountPrepaid);
                db.AddInParameter(cmdUpdateLiability, "@XRT_RepaymentTypeCode", DbType.String, liabilitiesVo.RepaymentTypeCode);
                if(liabilitiesVo.FrequencyCodeEMI!=null)
                    db.AddInParameter(cmdUpdateLiability, "@XF_FrequencyCodeEMI", DbType.String, liabilitiesVo.FrequencyCodeEMI);
                else
                    db.AddInParameter(cmdUpdateLiability, "@XF_FrequencyCodeEMI", DbType.String, DBNull.Value);
                if(liabilitiesVo.InstallmentStartDate!=DateTime.MinValue)
                    db.AddInParameter(cmdUpdateLiability, "@CL_InstallmentStartDate", DbType.DateTime, liabilitiesVo.InstallmentStartDate);
                else
                    db.AddInParameter(cmdUpdateLiability, "@CL_InstallmentStartDate", DbType.DateTime, DBNull.Value);
                if (liabilitiesVo.InstallmentEndDate != DateTime.MinValue)
                    db.AddInParameter(cmdUpdateLiability, "@CL_InstallmentEndDate", DbType.DateTime, liabilitiesVo.InstallmentEndDate);
                else
                    db.AddInParameter(cmdUpdateLiability, "@CL_InstallmentEndDate", DbType.DateTime, DBNull.Value);

                db.AddInParameter(cmdUpdateLiability, "@CL_IsInProcess", DbType.Int16, liabilitiesVo.IsInProcess);
                db.AddInParameter(cmdUpdateLiability, "@CL_CreatedBy", DbType.Int32, liabilitiesVo.CreatedBy);
                db.AddInParameter(cmdUpdateLiability, "@CL_ModifiedBy", DbType.Int32, liabilitiesVo.ModifiedBy);
                db.AddInParameter(cmdUpdateLiability, "@CL_CommissionAmount", DbType.Double, liabilitiesVo.CommissionAmount);
                db.AddInParameter(cmdUpdateLiability, "@CL_CommissionPer", DbType.Decimal, liabilitiesVo.CommissionPer);
                if(liabilitiesVo.LoanStartDate!=DateTime.MinValue)
                    db.AddInParameter(cmdUpdateLiability, "@CL_LoanStartDate", DbType.DateTime, liabilitiesVo.LoanStartDate);
                else
                    db.AddInParameter(cmdUpdateLiability, "@CL_LoanStartDate", DbType.DateTime,DBNull.Value);
                db.AddInParameter(cmdUpdateLiability, "@CL_OtherLenderName", DbType.String, liabilitiesVo.OtherLenderName);
	            db.AddInParameter(cmdUpdateLiability, "@CL_CompoundFrequency", DbType.String, liabilitiesVo.CompoundFrequency);
	            db.AddInParameter(cmdUpdateLiability, "@XPO_PaymentOptionCode", DbType.Int16, liabilitiesVo.PaymentOptionCode);
                if(liabilitiesVo.InstallmentTypeCode==0)
	                db.AddInParameter(cmdUpdateLiability, "@XIT_InstallmentTypeCode", DbType.Int16, DBNull.Value);
                else
                    db.AddInParameter(cmdUpdateLiability, "@XIT_InstallmentTypeCode", DbType.Int16, liabilitiesVo.InstallmentTypeCode);
	            db.AddInParameter(cmdUpdateLiability, "@CL_LumpsumRepaymentAmount", DbType.Double, liabilitiesVo.LumpsumRepaymentAmount);
	            db.AddInParameter(cmdUpdateLiability, "@CL_OutstandingAmount", DbType.Double, liabilitiesVo.OutstandingAmount);
	            db.AddInParameter(cmdUpdateLiability, "@CL_Guarantor", DbType.String, liabilitiesVo.Guarantor);
                db.AddInParameter(cmdUpdateLiability, "@CL_Tenure", DbType.Int32, liabilitiesVo.Tenure);
                db.AddInParameter(cmdUpdateLiability, "@CL_LiabilitiesId", DbType.Decimal, liabilitiesVo.LiabilitiesId);

                if (db.ExecuteNonQuery(cmdUpdateLiability) != 0)
                {
                    bResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:UpdateLiabilities()");
                object[] objects = new object[1];
                objects[0] = liabilitiesVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public bool UpdatePropertyAccountAssociates(int liabilityId, float Share, int associationId, string customerType)
        {
            Database db;
            DbCommand cmdUpdatePropertyAccountAssociates;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdatePropertyAccountAssociates = db.GetStoredProcCommand("SP_LoanSchemeUpdateAssetOwnership");
                db.AddInParameter(cmdUpdatePropertyAccountAssociates, "@LiabilityId", DbType.Int32, liabilityId);
                db.AddInParameter(cmdUpdatePropertyAccountAssociates, "@Share", DbType.Decimal, Share);
                db.AddInParameter(cmdUpdatePropertyAccountAssociates, "@AssociationId", DbType.Int32, associationId);
                db.AddInParameter(cmdUpdatePropertyAccountAssociates, "@CustomerType", DbType.String, customerType);

                if (db.ExecuteNonQuery(cmdUpdatePropertyAccountAssociates) != 0)
                {
                    bResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:UpdatePropertyAccountAssociates()");
                object[] objects = new object[1];
                objects[0] = liabilityId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public DataTable GetLiabilityAssociates(int LiabilityId)
        {
            Database db;
            DbCommand cmdGetLiabilityAssociates;
            DataSet dsGetLiabilitityAssociates;
            DataTable dtGetLiabilityAssociates = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetLiabilityAssociates = db.GetStoredProcCommand("SP_LoanSchemeGetLiabilityAssociation");
                db.AddInParameter(cmdGetLiabilityAssociates, "@CL_LiabilityId", DbType.Int32, LiabilityId);
                dsGetLiabilitityAssociates = db.ExecuteDataSet(cmdGetLiabilityAssociates);
                if (dsGetLiabilitityAssociates.Tables[0].Rows.Count > 0)
                {
                    dtGetLiabilityAssociates = dsGetLiabilitityAssociates.Tables[0];

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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetLiabilityAssociates()");
                object[] objects = new object[1];
                objects[0] = LiabilityId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dtGetLiabilityAssociates;
        }

        public bool UpdateLiabilityAssociates(LiabilityAssociateVo liabilityAssociateVo)
        {
            Database db;
            DbCommand cmdUpdateLiabilityAssociates;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateLiabilityAssociates = db.GetStoredProcCommand("SP_LoanSchemeUpdateLiabilityAssociation");
                db.AddInParameter(cmdUpdateLiabilityAssociates, "@CLA_LiabilitiesAssociationId", DbType.Int32, liabilityAssociateVo.LiabilitiesAssociationId);
                db.AddInParameter(cmdUpdateLiabilityAssociates, "@XLAT_LoanAssociateCode", DbType.String, liabilityAssociateVo.LoanAssociateCode);
                db.AddInParameter(cmdUpdateLiabilityAssociates, "@CLA_LiabilitySharePer", DbType.Decimal, liabilityAssociateVo.LiabilitySharePer);
                db.AddInParameter(cmdUpdateLiabilityAssociates, "@CLA_MarginPer", DbType.Decimal, liabilityAssociateVo.MarginPer);
                db.AddInParameter(cmdUpdateLiabilityAssociates, "@CA_AssociationId", DbType.Int32, liabilityAssociateVo.AssociationId);
                db.AddInParameter(cmdUpdateLiabilityAssociates, "@CLA_CreatedBy", DbType.Int32, liabilityAssociateVo.CreatedBy);
                db.AddInParameter(cmdUpdateLiabilityAssociates, "@CLA_ModifiedBy", DbType.Int32, liabilityAssociateVo.ModifiedBy);

                if (db.ExecuteNonQuery(cmdUpdateLiabilityAssociates) != 0)
                {
                    bResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:UpdateLiabilityAssociates()");
                object[] objects = new object[1];
                objects[0] = liabilityAssociateVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public DataTable GetAssetOwnerShip(int liabilityId)
        {
            Database db;
            DbCommand cmdGetAssetOwnerShip;
            DataSet dsGetAssetOwnerShip;
            DataTable dtGetAssetOwnerShip = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetAssetOwnerShip = db.GetStoredProcCommand("SP_LoanSchemeGetAssetSharePer");
                db.AddInParameter(cmdGetAssetOwnerShip, "@CL_LiabilitiesId", DbType.Int32, liabilityId);


                dsGetAssetOwnerShip = db.ExecuteDataSet(cmdGetAssetOwnerShip);
                if (dsGetAssetOwnerShip.Tables[0].Rows.Count > 0)
                {
                    dtGetAssetOwnerShip = dsGetAssetOwnerShip.Tables[0];

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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetAssetOwnerShip()");
                object[] objects = new object[1];
                objects[0] = liabilityId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dtGetAssetOwnerShip;
        }

        public DataTable GetLiabilityAssetAssociation(int liabilityId)
        {


            Database db;
            DbCommand cmdGetLiabilityAssetAssociation;
            DataSet dsGetLiabilityAssetAssociation;
            DataTable dt = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetLiabilityAssetAssociation = db.GetStoredProcCommand("SP_LoanSchemeGetLiabilityAssetAssociation");
                db.AddInParameter(cmdGetLiabilityAssetAssociation, "@LiabilityId", DbType.Int32, liabilityId);
                dsGetLiabilityAssetAssociation = db.ExecuteDataSet(cmdGetLiabilityAssetAssociation);
                if (dsGetLiabilityAssetAssociation.Tables[0].Rows.Count > 0)
                {
                    dt = dsGetLiabilityAssetAssociation.Tables[0];
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetLiabilityAssetAssociation()");
                object[] objects = new object[1];
                objects[0] = liabilityId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dt;
        }

        public DataTable GetRMLoanProposalPendingCount(int rmId)
        {
            Database db;
            DbCommand cmdGetRMLoanProposalPendingCount;
            DataSet dsGetRMLoanProposalPendingCount;
            DataTable dt = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetRMLoanProposalPendingCount = db.GetStoredProcCommand("SP_LoanSchemeGetLoanProposalCount");
                db.AddInParameter(cmdGetRMLoanProposalPendingCount, "@RMId", DbType.Int32, rmId);
                dsGetRMLoanProposalPendingCount = db.ExecuteDataSet(cmdGetRMLoanProposalPendingCount);
                if (dsGetRMLoanProposalPendingCount.Tables[0].Rows.Count > 0)
                {
                    dt = dsGetRMLoanProposalPendingCount.Tables[0];
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetRMLoanProposalPendingCount()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dt;
        }
        #endregion SV's COde



        public bool DeleteLiabilityPortfolio(int liabilityId)
        {

            Database db;
            DbCommand cmdDeleteLiability;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdDeleteLiability = db.GetStoredProcCommand("SP_DeleteLiabilityPortfolio");
                db.AddInParameter(cmdDeleteLiability, "@CL_LiabilitiesId", DbType.Int32, liabilityId);

                if (db.ExecuteNonQuery(cmdDeleteLiability) != 0)
                {
                    bResult = true;
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
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:DeleteLiabilityPortfolio()");
                object[] objects = new object[1];
                objects[0] = liabilityId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }


    }
}
