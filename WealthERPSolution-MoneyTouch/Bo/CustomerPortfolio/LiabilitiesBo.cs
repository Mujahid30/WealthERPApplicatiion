using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoCustomerPortfolio;
using System.Data;

using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoCustomerPortfolio;
using System.Data.SqlClient;

using VoUser;

namespace BoCustomerPortfolio
{
    public class LiabilitiesBo
    {
        #region robin

        public static DataSet GetLoanSchemes(int page, out int count)
        {
            return LiabilitiesDao.GetLoanSchemes(page, out count);
        }
        public static SchemeDetailsVo GetSchemeDetails(int schemeId)
        {
            SqlDataReader sdr = LiabilitiesDao.GetSchemeDetails(schemeId);
            SchemeDetailsVo schemeDetailsVo = new SchemeDetailsVo();
            while (sdr.Read())
            {
                schemeDetailsVo.LoanSchemeId = int.Parse(sdr["ALS_LoanSchemeId"].ToString());
                schemeDetailsVo.LoanSchemeName = sdr["ALS_LoanSchemeName"].ToString();
                schemeDetailsVo.MinimunLoanAmount = float.Parse(sdr["ALS_MinimunLoanAmount"].ToString());
                schemeDetailsVo.MaximumLoanAmount = float.Parse(sdr["ALS_MaximumLoanAmount"].ToString());
                schemeDetailsVo.MinimumLoanPeriod = int.Parse(sdr["ALS_MinimumLoanPeriod"].ToString());
                schemeDetailsVo.MaximumLoanPeriod = int.Parse(sdr["ALS_MaximumLoanPeriod"].ToString());
                schemeDetailsVo.PLR = float.Parse(sdr["ALS_PLR"].ToString());
                if (sdr["ALS_MarginMaintained"] != DBNull.Value)
                    schemeDetailsVo.MarginMaintained = float.Parse(sdr["ALS_MarginMaintained"].ToString());
                schemeDetailsVo.IsFloatingRateInterest = Convert.ToBoolean(sdr["ALS_IsFloatingRateInterest"]);
                schemeDetailsVo.MinimumAge = int.Parse(sdr["ALS_MinimumAge"].ToString());
                schemeDetailsVo.MaximumAge = int.Parse(sdr["ALS_MaximumAge"].ToString());
                schemeDetailsVo.MinimumSalary = float.Parse(sdr["ALS_MinimumSalary"].ToString());
                schemeDetailsVo.MinimumProfitAmount = float.Parse(sdr["ALS_MinimumProfitAmount"].ToString());
                schemeDetailsVo.MinimumProfitPeriod = int.Parse(sdr["ALS_MinimumProfitPeriod"].ToString());
                schemeDetailsVo.Remark = sdr["ALS_Remark"].ToString();

                schemeDetailsVo.CustomerCategory = int.Parse(sdr["XCC_CustomerCategoryCode"].ToString());
                schemeDetailsVo.LoanPartner = int.Parse(sdr["XLP_LoanPartnerCode"].ToString());
                schemeDetailsVo.LoanType = int.Parse(sdr["XLT_LoanTypeCode"].ToString());
            }
            return schemeDetailsVo;
        }


        public static DataSet GetInterestRateBySchemeId(int schemeId)
        {
            return LiabilitiesDao.GetInterestRateBySchemeId(schemeId);
        }
        public static bool AddInterestRate(SchemeInterestRateVo interestRateVo)
        {
            return LiabilitiesDao.AddInterestRate(interestRateVo);
        }
        public static bool UpdateInterestRate(SchemeInterestRateVo interestRateVo)
        {
            return LiabilitiesDao.UpdateInterestRate(interestRateVo);
        }

        public static int CreateScheme(SchemeDetailsVo schemeDetailsVo)
        {
            return LiabilitiesDao.CreateScheme(schemeDetailsVo);
        }
        public static bool UpdateScheme(SchemeDetailsVo schemeDetailsVo)
        {
            return LiabilitiesDao.UpdateScheme(schemeDetailsVo);
        }

        public static List<SchemeProof> GetDocumentsForBorrower(int schemeId, int customerType, int loanTypeCode)
        {
            DataSet dsDocuments = LiabilitiesDao.GetDocumentsForBorrower(schemeId, customerType, loanTypeCode);
            DataTable dtDocuments = dsDocuments.Tables[0];
            //int currentTypeCode = -1;
            //int previousTypeCode = 0;
            List<SchemeProof> schemeProofs = new List<SchemeProof>();
            SchemeProof schemeProof = new SchemeProof();
            for (int i = 0; i < dtDocuments.Rows.Count; i++)
            {
                DataRow dr = dtDocuments.Rows[i];
                Proof proof = new Proof();
                proof.proofCode = dr["XP_ProofCode"].ToString();
                proof.proofName = dr["XP_ProofName"].ToString();
                if (dr["ALSP_LoanSchemeProofId"] != DBNull.Value)
                    proof.isAdded = true;

                schemeProof.proofs.Add(proof);
                if ((i == dtDocuments.Rows.Count - 1) || int.Parse(dr["XPRT_ProofTypeCode"].ToString()) != int.Parse(dtDocuments.Rows[i + 1]["XPRT_ProofTypeCode"].ToString()))
                {
                    schemeProof.proofTypeCode = int.Parse(dr["XPRT_ProofTypeCode"].ToString());
                    schemeProof.proofTypeName = dr["XPRT_ProofType"].ToString();
                    schemeProofs.Add(schemeProof);
                    //schemeProof.proofs.Clear();
                    schemeProof = null;
                    schemeProof = new SchemeProof();
                }
            }
            return schemeProofs;

        }
        public static bool AddProofs(int schemeId, int proofCode, int createdBy)
        {
            return LiabilitiesDao.AddProofs(schemeId, proofCode, createdBy);
            //return false;
        }
        public static bool DeleteProofs(int schemeId)
        {
            return LiabilitiesDao.DeleteProofs(schemeId);
            //return false;
        }

        public static DataTable GetAllLoanTypes()
        {
            return LiabilitiesDao.GetAllLoanTypes();
        }
        public static DataTable GetAllCustomerTypes(int loanType)
        {
            return LiabilitiesDao.GetAllCustomerTypes(loanType);
        }

        #endregion

        #region Joshan Code - Created 16/9/2009

        public DataSet GetLoanScheme(int loanTypeId, int loanPartnerId, int rmId)
        {
            DataSet dsLoanScheme;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                dsLoanScheme = liabilitiesDao.GetLoanScheme(loanTypeId, loanPartnerId, rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetLoanScheme()");
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
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                dsClientListForScheme = liabilitiesDao.GetClientListForScheme(loanSchemeId);//, loanType
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetLoanScheme()");
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
            DataSet dsEligibility;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                dsEligibility = liabilitiesDao.GetLoanSchemeEligilityCriteria(schemeId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetLoanScheme()");
                object[] objects = new object[1];
                objects[0] = schemeId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsEligibility;
        }

        public DataSet GetLoanCustEligibiltyCriteria(int custId, List<int> borrowerIds, out Double TotalIncome, out Double TotalExpense, out Double TotalNetWorth)
        {
            DataSet dsEligibility;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            List<BorrowerLoanEligilityVo> bEligibilityVo = new List<BorrowerLoanEligilityVo>();
            BorrowerLoanEligilityVo bLoanEligilityVo = new BorrowerLoanEligilityVo();
            TotalIncome = 0;
            TotalExpense = 0;
            TotalNetWorth = 0;

            try
            {
                dsEligibility = liabilitiesDao.GetLoanCustEligibiltyCriteria(custId, borrowerIds, out bEligibilityVo);
                TotalIncome = Convert.ToDouble(dsEligibility.Tables[0].Rows[0]["Income"].ToString());
                TotalExpense = Convert.ToDouble(dsEligibility.Tables[0].Rows[0]["Expense"].ToString());
                TotalNetWorth = Convert.ToDouble(dsEligibility.Tables[0].Rows[0]["NetWorth"].ToString());
                for (int i = 0; i < bEligibilityVo.Count; i++)
                {
                    TotalIncome += bEligibilityVo[i].Income;
                    TotalExpense += bEligibilityVo[i].Expense;
                    TotalNetWorth += bEligibilityVo[i].NetWorth;
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
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetLoanCustEligibiltyCriteria()");
                object[] objects = new object[2];
                objects[0] = custId;
                objects[1] = borrowerIds;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsEligibility;
        }

        public DataSet GetLoanProcessDetails(int clientId, int loanTypeId, int loanPartnerId, int schemeId, int staffId)
        {
            DataSet dsLoanProcessDetails;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                dsLoanProcessDetails = liabilitiesDao.GetLoanProcessDetails(clientId, loanTypeId, loanPartnerId, schemeId, staffId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetLoanProcessDetails()");
                object[] objects = new object[5];
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
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                dsEntryStageDetails = liabilitiesDao.GetProposalStageDetails(loanPartnerId, loanTypeId, schemeId, clientId, associateId, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetProposalStageDetails()");
                object[] objects = new object[6];
                objects[0] = loanPartnerId;
                objects[1] = loanTypeId;
                objects[2] = schemeId;
                objects[3] = clientId;
                objects[4] = associateId;
                objects[5] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsEntryStageDetails;
        }

        public DataSet GetLoanSchemeDetails(int SchemeId)
        {
            DataSet ds = new DataSet();
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();

            try
            {
                ds = liabilitiesDao.GetLoanSchemeDetails(SchemeId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetSchemeDetails()");
                object[] objects = new object[1];
                objects[0] = SchemeId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetInterestDetails(string interestRateId)
        {
            DataSet ds = new DataSet();
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();

            try
            {
                ds = liabilitiesDao.GetInterestDetails(interestRateId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetSchemeDetails()");
                object[] objects = new object[1];
                objects[1] = interestRateId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public List<PropertyVo> GetCustomerAndAssociateProperty(int clientId, List<int> borrowerIds)
        {
            List<PropertyVo> listPropertyVo = new List<PropertyVo>();
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();

            try
            {
                listPropertyVo = liabilitiesDao.GetCustomerAndAssociateProperty(clientId, borrowerIds);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetSchemeDetails()");
                object[] objects = new object[2];
                objects[0] = clientId;
                objects[1] = borrowerIds;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return listPropertyVo;
        }

        public List<EQPortfolioVo> GetCustomerAndAssociateShares(int clientId, DateTime dt)
        {
            List<EQPortfolioVo> EQPortfolioListVo = new List<EQPortfolioVo>();
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();

            try
            {
                EQPortfolioListVo = liabilitiesDao.GetCustomerAndAssociateShares(clientId, dt);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetSchemeDetails()");
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
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();

            try
            {
                blResult = liabilitiesDao.AddLoanProposal(loanProposalVo, loanProposalStageVo, loanProposalDocVo, userId, out loanProposalId, out liabilitiesId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:AddLoanProposal()");
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
            DataSet dsClientListAll;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                dsClientListAll = liabilitiesDao.GetRMClientList(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetRMClientList()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsClientListAll;
        }

        public DataTable GetCustomerAssociates(int custId)//, int LoanTypeCode
        {
            DataTable dsClientAssociates;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                dsClientAssociates = liabilitiesDao.GetCustomerAssociates(custId);//, LoanTypeCode
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetCustomerAssociates()");
                object[] objects = new object[1];
                objects[0] = custId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsClientAssociates;
        }

        public List<LoanProposalVo> GetLoanProposalList(int rmId, int currentPage, out int Count, string sortExpression, string CustName, string LoanTypeId, string LoanStageId, out Dictionary<string, string> genDictLoanType, out Dictionary<string, string> genDictLoanStage)
        {
            List<LoanProposalVo> loanProposalList = null;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();

            genDictLoanType = new Dictionary<string, string>();
            genDictLoanStage = new Dictionary<string, string>();

            Count = 0;

            try
            {
                loanProposalList = liabilitiesDao.GetLoanProposalList(rmId, currentPage, out Count, sortExpression, CustName, LoanTypeId, LoanStageId, out genDictLoanType, out genDictLoanStage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:GetAdviserCustomerList()");
                object[] objects = new object[10];
                objects[0] = rmId;
                objects[1] = currentPage;
                objects[2] = Count;
                objects[3] = sortExpression;
                objects[4] = CustName;
                objects[5] = LoanTypeId;
                objects[6] = LoanTypeId;
                objects[7] = LoanStageId;
                objects[8] = genDictLoanType;
                objects[9] = genDictLoanStage;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return loanProposalList;
        }

        public DataSet GetLoanProposalDetails(int loanProposalId, int rmId)
        {
            DataSet ds = new DataSet();
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                ds = liabilitiesDao.GetLoanProposalDetails(loanProposalId, rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetLoanProposalDetails()");
                object[] objects = new object[2];
                objects[0] = loanProposalId;
                objects[1] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        public bool UpdateApplicationEntry(LoanApplicationEntryVo loanApplVo, int userId)
        {
            bool blResult = false;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();

            try
            {
                blResult = liabilitiesDao.UpdateApplicationEntry(loanApplVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:UpdateApplicationEntry()");
                object[] objects = new object[1];
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
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();

            try
            {
                blResult = liabilitiesDao.UpdateEligility(loanEligiVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:UpdateEligility()");
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
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();

            try
            {
                blResult = liabilitiesDao.UpdateLoanSanction(loanSancVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:UpdateLoanSanction()");
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
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();

            try
            {
                blResult = liabilitiesDao.UpdateLoanDisbursed(loanDisVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:UpdateLoanDisbursed()");
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
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();

            try
            {
                blResult = liabilitiesDao.UpdateLoanClosure(loanClosureVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:UpdateLoanClosure()");
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
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();

            try
            {
                blResult = liabilitiesDao.UpdateLoanProposal(loanProposalVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:UpdateLoanProposal()");
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
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            bool blResult = false;
            try
            {
                blResult = liabilitiesDao.DeleteLiabilityAssetAssociation(liabilityId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:DeleteLiabilityAssetAssociation()");
                object[] objects = new object[1];
                objects[0] = liabilityId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public DataSet GetDocCustomerDropDown(int loanProposalId)
        {
            DataSet ds = new DataSet();
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                ds = liabilitiesDao.GetDocCustomerDropDown(loanProposalId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetLoanProposalDetails()");
                object[] objects = new object[1];
                objects[0] = loanProposalId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetCustSubmittedDocs(int liabilitiesAssoId)
        {
            DataSet ds = new DataSet();
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                ds = liabilitiesDao.GetCustSubmittedDocs(liabilitiesAssoId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetLoanProposalDetails()");
                object[] objects = new object[1];
                objects[0] = liabilitiesAssoId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetSchemeDocs(int schemeId)
        {
            DataSet ds = new DataSet();
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                ds = liabilitiesDao.GetSchemeDocs(schemeId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetSchemeDocs()");
                object[] objects = new object[1];
                objects[0] = schemeId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public bool SubmitDocumentDetails(LoanProposalDocVo lpdVo, int userId)
        {
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            bool blResult = false;
            try
            {
                blResult = liabilitiesDao.SubmitDocumentDetails(lpdVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:SubmitDocumentDetails()");
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
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            bool blResult = false;
            try
            {
                blResult = liabilitiesDao.UpdateDocumentDetails(lpdVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:UpdateDocumentDetails()");
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
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            bool blResult = false;
            try
            {
                blResult = liabilitiesDao.DeleteDocumentDetails(proposalDocId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:DeleteDocumentDetails()");
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
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            bool blResult = false;
            try
            {
                blResult = liabilitiesDao.AddDocument(schemeId, proofTypeId, proofName, liabilitiesAssoId, loanProposalId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:AddDocument()");
                object[] objects = new object[3];
                objects[0] = schemeId;
                objects[1] = proofTypeId;
                objects[2] = proofName;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public DataSet GetCustAdditionalDocs(int loanAssocId)
        {
            DataSet ds = new DataSet();
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                ds = liabilitiesDao.GetCustAdditionalDocs(loanAssocId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetCustAdditionalDocs()");
                object[] objects = new object[1];
                objects[0] = loanAssocId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public bool AddCustAdditionalDocs(LoanProposalDocVo loanProposalDocVo, int userId)
        {
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            bool blResult = false;
            try
            {
                blResult = liabilitiesDao.AddCustAdditionalDocs(loanProposalDocVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:AddCustAdditionalDocs()");
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
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            bool blResult = false;
            try
            {
                blResult = liabilitiesDao.UpdateCustAdditionalDocs(loanProposalDocVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:UpdateCustAdditionalDocs()");
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


        #region SV's COde

        public DataTable GetEditLogInfo(int customerId)
        {

            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            DataTable dt = null;

            try
            {
                dt = liabilitiesDao.GetEditLogInfo(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetEditLogInfo()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dt;
        }
        public DataTable GetPropertyAccountAssociates(int PropertyId)
        {
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            DataTable dt = null;

            try
            {
                dt = liabilitiesDao.GetPropertyAccountAssociates(PropertyId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetPropertyAccountAssociates()");
                object[] objects = new object[1];
                objects[0] = PropertyId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dt;

        }
        public int CreateLiabilities(LiabilitiesVo liabilitiesVo)
        {
            int LiabilityId = 0;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();


            try
            {
                LiabilityId = liabilitiesDao.CreatLiabilities(liabilitiesVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:CreateLiabilities()");
                object[] objects = new object[1];
                objects[0] = liabilitiesVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return LiabilityId;
        }
        public bool CreateLiabilityAssociates(LiabilityAssociateVo liabilityAssociateVo)
        {
            bool blResult = false;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                blResult = liabilitiesDao.CreateLiabilityAssociates(liabilityAssociateVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:CreateLiabilityAssociates()");
                object[] objects = new object[1];
                objects[0] = liabilityAssociateVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return blResult;
        }
        public List<LiabilitiesVo> GetLiabilities(int customerId)
        {
            List<LiabilitiesVo> liabilitiesList = null;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                liabilitiesList = liabilitiesDao.GetLiabilities(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetLiabilities()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return liabilitiesList;
        }
        public LiabilitiesVo GetLiabilityDetails(int liabilityId)
        {

            LiabilitiesVo liabilitiesVo = null;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                liabilitiesVo = liabilitiesDao.GetLiabilityDetails(liabilityId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetLiabilityDetails()");
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
            int EditLogId = 0;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();


            try
            {
                EditLogId = liabilitiesDao.CreatEditLogInfo(editLogVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:CreatEditLogInfo()");
                object[] objects = new object[1];
                objects[0] = editLogVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return EditLogId;
        }
        public bool UpdatePropertyAccountAssociates(int liabilityId, float Share, int associationId, string customerType)
        {
            bool blResult = false;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                blResult = liabilitiesDao.UpdatePropertyAccountAssociates(liabilityId, Share, associationId, customerType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:UpdatePropertyAccountAssociates()");
                object[] objects = new object[1];
                objects[0] = liabilityId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return blResult;
        }
        public bool UpdateLiabilities(LiabilitiesVo liabilitiesVo)
        {
            bool blResult = false;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                blResult = liabilitiesDao.UpdateLiabilities(liabilitiesVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:UpdateLiabilities()");
                object[] objects = new object[1];
                objects[0] = liabilitiesVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return blResult;

        }
        public DataTable GetLiabilityAssociates(int LiabilityId)
        {
            DataTable dt = null;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                dt = liabilitiesDao.GetLiabilityAssociates(LiabilityId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetLiabilityAssociates()");
                object[] objects = new object[1];
                objects[0] = LiabilityId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dt;
        }
        public bool CreatLiabilityAssetAssociation(AssetAssociationVo assetAssociationVo)
        {
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            bool blResult = false;
            try
            {
                blResult = liabilitiesDao.CreatLiabilityAssetAssociation(assetAssociationVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:CreatLiabilityAssetAssociation()");
                object[] objects = new object[1];
                objects[0] = assetAssociationVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return blResult;
        }
        public bool UpdateLiabilityAssociates(LiabilityAssociateVo liabilityAssociateVo)
        {
            bool blResult = false;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                blResult = liabilitiesDao.UpdateLiabilityAssociates(liabilityAssociateVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:UpdateLiabilityAssociates()");
                object[] objects = new object[1];
                objects[0] = liabilityAssociateVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return blResult;
        }
        public DataTable GetAssetOwnerShip(int liabilityId)
        {
            DataTable dt = null;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                dt = liabilitiesDao.GetAssetOwnerShip(liabilityId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetAssetOwnerShip()");
                object[] objects = new object[1];
                objects[0] = liabilityId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dt;
        }
        public DataTable GetLiabilityAssetAssociation(int liabilityId)
        {
            DataTable dt = null;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                dt = liabilitiesDao.GetLiabilityAssetAssociation(liabilityId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetLiabilityAssetAssociation()");
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
            DataTable dt = null;
            LiabilitiesDao liabilitiesDao = new LiabilitiesDao();
            try
            {
                dt = liabilitiesDao.GetRMLoanProposalPendingCount(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesBo.cs:GetRMLoanProposalPendingCount()");
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

    }
}
