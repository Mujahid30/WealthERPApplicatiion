using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUploads;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;



namespace DaoUploads
{
    public class KarvyUploadsDao
    {
        public List<KarvyUploadsVo> GetKarvyNewCustomers(int processId)
        {
            List<KarvyUploadsVo> uploadsCustomerList = new List<KarvyUploadsVo>();
            KarvyUploadsVo KarvyUploadsVo;
            Database db;
            DbCommand getNewCustomersCmd;
            DataSet getNewCustomersDs;
            //DataRow dr;

            try
            {
            db = DatabaseFactory.CreateDatabase("wealtherp");
            getNewCustomersCmd = db.GetStoredProcCommand("SP_UploadGetNewCustomersKarvyCombo");
            getNewCustomersDs = db.ExecuteDataSet(getNewCustomersCmd);
            //dr = getNewCustomersDs.Tables[0].Rows[0];
            foreach (DataRow dr in getNewCustomersDs.Tables[0].Rows)
            {
                KarvyUploadsVo = new KarvyUploadsVo();

                //KarvyUploadsVo.ProductCode = dr["CMFKXCS_ProductCode"].ToString();
                //KarvyUploadsVo.Fund = dr["CMFKXCS_Fund"].ToString();
                //KarvyUploadsVo.FolioNumber = dr["CMFKXCS_FolioNumber"].ToString();
                //KarvyUploadsVo.SchemeCode = dr["CMFKXCS_SchemeCode"].ToString();
                //KarvyUploadsVo.DividendOption = dr["CMFKXCS_DividendOption"].ToString();
                //KarvyUploadsVo.FundDescription = dr["CMFKXCS_FundDescription"].ToString();
                //KarvyUploadsVo.TransactionHead = dr["CMFKXCS_TransactionHead"].ToString();
                //KarvyUploadsVo.TransactionNumber = dr["CMFKXCS_TransactionNumber"].ToString();
                //KarvyUploadsVo.Switch_RefNo = dr["[CMFKXCS_Switch_RefNo ]"].ToString();
                //KarvyUploadsVo.InstrumentNumber = dr["CMFKXCS_InstrumentNumber"].ToString();
                KarvyUploadsVo.InvestorName = dr["CMFKXCS_InvestorName"].ToString();
                //KarvyUploadsVo.JointName1 = dr["CMFKXCS_JointName1"].ToString();
                //KarvyUploadsVo.JointName2 = dr["CMFKXCS_JointName2"].ToString();
                KarvyUploadsVo.Address1 = dr["CMFKXCS_Address#1"].ToString();
                KarvyUploadsVo.Address2 = dr["CMFKXCS_Address#2"].ToString();
                KarvyUploadsVo.Address3 = dr["CMFKXCS_Address#3"].ToString();
                KarvyUploadsVo.City = dr["CMFKXCS_City"].ToString();
                KarvyUploadsVo.Pincode = dr["CMFKXCS_Pincode"].ToString();
                KarvyUploadsVo.State = dr["CMFKXCS_State"].ToString();
                KarvyUploadsVo.Country = dr["CMFKXCS_Country"].ToString();
                KarvyUploadsVo.DateofBirth = dr["CMFKXCS_DateofBirth"].ToString();
                KarvyUploadsVo.PhoneResidence = dr["CMFKXCS_PhoneResidence"].ToString();
                //KarvyUploadsVo.PhoneRes1 = dr["CMFKXCS_PhoneRes#1"].ToString();
                //KarvyUploadsVo.PhoneRes2 = dr["CMFKXCS_PhoneRes#2"].ToString();
                KarvyUploadsVo.Mobile = dr["CMFKXCS_Mobile"].ToString();
                KarvyUploadsVo.PhoneOffice = dr["CMFKXCS_PhoneOffice"].ToString();
                //KarvyUploadsVo.PhoneOff1 = dr["CMFKXCS_PhoneOff#1"].ToString();
                //KarvyUploadsVo.PhoneOff2 = dr["CMFKXCS_PhoneOff#2"].ToString();
                KarvyUploadsVo.FaxResidence = dr["CMFKXCS_FaxResidence"].ToString();
                KarvyUploadsVo.FaxOffice = dr["CMFKXCS_FaxOffice"].ToString();
                //KarvyUploadsVo.TaxStatus = dr["CMFKXCS_TaxStatus"].ToString();
                //KarvyUploadsVo.OccCode = dr["CMFKXCS_OccCode"].ToString();
                KarvyUploadsVo.Email = dr["CMFKXCS_Email"].ToString();
                //KarvyUploadsVo.BankAccno = dr["CMFKXCS_BankAccno"].ToString();
                //KarvyUploadsVo.BankName = dr["CMFKXCS_BankName"].ToString();
                //KarvyUploadsVo.AccountType = dr["CMFKXCS_AccountType"].ToString();
                //KarvyUploadsVo.Branch = dr["CMFKXCS_Branch"].ToString();
                //KarvyUploadsVo.BankAddress1 = dr["CMFKXCS_BankAddress#1"].ToString();
                //KarvyUploadsVo.BankAddress2 = dr["CMFKXCS_BankAddress#2"].ToString();
                //KarvyUploadsVo.BankAddress3 = dr["CMFKXCS_BankAddress#3"].ToString();
                //KarvyUploadsVo.BankCity = dr["CMFKXCS_BankCity"].ToString();
                //KarvyUploadsVo.BankPhone = dr["CMFKXCS_BankPhone"].ToString();
                KarvyUploadsVo.PANNumber = dr["CMFKXCS_PANNumber"].ToString();
                //KarvyUploadsVo.TransactionMode = dr["CMFKXCS_TransactionMode"].ToString();
                //KarvyUploadsVo.TransactionStatus = dr["CMFKXCS_TransactionStatus"].ToString();
                //KarvyUploadsVo.BranchName = dr["CMFKXCS_BranchName"].ToString();
                //KarvyUploadsVo.BranchTransactionNo = dr["CMFKXCS_BranchTransactionNo"].ToString();
                //KarvyUploadsVo.TransactionDate = dr["CMFKXCS_TransactionDate"].ToString();
                //KarvyUploadsVo.ProcessDate = dr["CMFKXCS_ProcessDate"].ToString();
                //KarvyUploadsVo.Price = dr["CMFKXCS_Price"].ToString();
                //KarvyUploadsVo.LoadPercentage = dr["CMFKXCS_LoadPercentage"].ToString();
                //KarvyUploadsVo.Units = dr["CMFKXCS_Units"].ToString();
                //KarvyUploadsVo.Amount = dr["CMFKXCS_Amount"].ToString();
                //KarvyUploadsVo.LoadAmount = dr["CMFKXCS_LoadAmount"].ToString();
                //KarvyUploadsVo.AgentCode = dr["CMFKXCS_AgentCode"].ToString();
                //KarvyUploadsVo.SubBrokerCode = dr["CMFKXCS_Sub-BrokerCode"].ToString();
                //KarvyUploadsVo.BrokeragePercentage = dr["CMFKXCS_BrokeragePercentage"].ToString();
                //KarvyUploadsVo.Commission = dr["CMFKXCS_Commission"].ToString();
                //KarvyUploadsVo.InvestorID = dr["CMFKXCS_InvestorID"].ToString();
                //KarvyUploadsVo.ReportDate = dr["CMFKXCS_ReportDate"].ToString();
                //KarvyUploadsVo.ReportTime = dr["CMFKXCS_ReportTime"].ToString();
                //KarvyUploadsVo.TransactionSub = dr["CMFKXCS_TransactionSub"].ToString();
                //KarvyUploadsVo.ApplicationNumber = dr["CMFKXCS_ApplicationNumber"].ToString();
                //KarvyUploadsVo.TransactionID = dr["CMFKXCS_TransactionID"].ToString();
                //KarvyUploadsVo.TransactionDescription = dr["CMFKXCS_TransactionDescription"].ToString();
                //KarvyUploadsVo.TransactionType = dr["CMFKXCS_TransactionType"].ToString();
                //KarvyUploadsVo.OrgPurchaseDate = dr["CMFKXCS_OrgPurchaseDate"].ToString();
                //KarvyUploadsVo.OrgPurchaseAmount = dr["CMFKXCS_OrgPurchaseAmount"].ToString();
                //KarvyUploadsVo.OrgPurchaseUnits = dr["CMFKXCS_OrgPurchaseUnits"].ToString();
                //KarvyUploadsVo.TrTypeFlag = dr["CMFKXCS_TrTypeFlag"].ToString();
                //KarvyUploadsVo.SwitchFundDate = dr["CMFKXCS_SwitchFundDate"].ToString();
                //KarvyUploadsVo.InstrumentDate = dr["CMFKXCS_InstrumentDate"].ToString();
                //KarvyUploadsVo.InstrumentBank = dr["CMFKXCS_InstrumentBank"].ToString();
                //KarvyUploadsVo.Remarks = dr["CMFKXCS_Remarks"].ToString();
                //KarvyUploadsVo.Scheme = dr["CMFKXCS_Scheme"].ToString();
                //KarvyUploadsVo.Plan = dr["CMFKXCS_Plan"].ToString();
                //KarvyUploadsVo.NAV = dr["CMFKXCS_NAV"].ToString();
                //KarvyUploadsVo.AnnualizedPercentage = dr["CMFKXCS_Annualized%"].ToString();
                //KarvyUploadsVo.AnnualizedCommision = dr["CMFKXCS_AnnualizedCommision"].ToString();
                //KarvyUploadsVo.OrginalPurchaseTrnxNo = dr["CMFKXCS_OrginalPurchaseTrnxNo"].ToString();
                //KarvyUploadsVo.OrginalPurchaseBranch = dr["CMFKXCS_OrginalPurchaseBranch"].ToString();
                //KarvyUploadsVo.OldAcno = dr["CMFKXCS_OldAcno"].ToString();
                //KarvyUploadsVo.IHNo = dr["CMFKXCS_IHNo "].ToString();
                //KarvyUploadsVo.IsRejected = dr["CMFKXCS_IsRejected"].ToString();
                //KarvyUploadsVo.IsFolioNew = dr["CMFKXCS_IsFolioNew"].ToString();
                //KarvyUploadsVo.IsCustomerNew = dr["CMFKXCS_IsCustomerNew"].ToString();
                //KarvyUploadsVo.RejectedRemark = dr["CMFKXCS_RejectedRemark"].ToString();
                //KarvyUploadsVo.AdviserId = dr["CMFKXCS_AdviserId"].ToString();
                //KarvyUploadsVo.CustomerId = dr["CMFKXCS_CustomerId"].ToString();

                uploadsCustomerList.Add(KarvyUploadsVo);
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

                FunctionInfo.Add("Method", "KarvyUploadsDao.cs:GetKarvyNewCustomers()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return uploadsCustomerList;
        }
        public List<KarvyUploadsVo> GetKarvyProfNewCustomers(int processId)
        {
            List<KarvyUploadsVo> uploadsCustomerList = new List<KarvyUploadsVo>();
            KarvyUploadsVo KarvyUploadsVo;
            Database db;
            DbCommand getNewCustomersCmd;
            DataSet getNewCustomersDs;
            //DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getNewCustomersCmd = db.GetStoredProcCommand("SP_UploadGetNewCustomersKarvyProf");
                db.AddInParameter(getNewCustomersCmd, "@processId", DbType.Int32, processId);
                getNewCustomersDs = db.ExecuteDataSet(getNewCustomersCmd);
                //dr = getNewCustomersDs.Tables[0].Rows[0];
                foreach (DataRow dr in getNewCustomersDs.Tables[0].Rows)
                {
                    KarvyUploadsVo = new KarvyUploadsVo();

                    KarvyUploadsVo.InvestorName = dr["CMFKXPS_InvestorName"].ToString();
                    KarvyUploadsVo.Address1 = dr["CMFKXPS_Address#1"].ToString();
                    KarvyUploadsVo.Address2 = dr["CMFKXPS_Address#2"].ToString();
                    KarvyUploadsVo.Address3 = dr["CMFKXPS_Address#3"].ToString();
                    KarvyUploadsVo.City = dr["CMFKXPS_City"].ToString();
                    KarvyUploadsVo.Pincode = dr["CMFKXPS_Pincode"].ToString();
                    KarvyUploadsVo.State = dr["CMFKXPS_State"].ToString();
                    KarvyUploadsVo.Country = dr["CMFKXPS_Country"].ToString();
                    KarvyUploadsVo.DateofBirth = dr["CMFKXPS_DateofBirth"].ToString();
                    KarvyUploadsVo.PhoneResidence = dr["CMFKXPS_PhoneResidence"].ToString(); 
                    KarvyUploadsVo.Mobile = dr["CMFKXPS_Mobile"].ToString();
                    KarvyUploadsVo.PhoneOffice = dr["CMFKXPS_PhoneOffice"].ToString();
                    KarvyUploadsVo.FaxResidence = dr["CMFKXPS_FaxResidence"].ToString();
                    KarvyUploadsVo.FaxOffice = dr["CMFKXPS_FaxOffice"].ToString();
                    KarvyUploadsVo.Email = dr["CMFKXPS_Email"].ToString();
                    KarvyUploadsVo.PANNumber = dr["CMFKXPS_PANNumber"].ToString();
                    KarvyUploadsVo.OccCode = dr["OccupationCode"].ToString();
                    KarvyUploadsVo.TypeCode = dr["CustomerTypeCode"].ToString();
                    KarvyUploadsVo.SubTypeCode = dr["CustomerSubTypeCode"].ToString();
                    KarvyUploadsVo.AccountType = dr["BankAccountTypeCode"].ToString();
                    KarvyUploadsVo.ModeOfHolding = dr["ModeOfHoldingCode"].ToString();
             
                    uploadsCustomerList.Add(KarvyUploadsVo);
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

                FunctionInfo.Add("Method", "KarvyUploadsDao.cs:GetKarvyProfNewCustomers()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return uploadsCustomerList;
        }

        public bool UpdateCombinationStagingIsCustomerNew()
        {
            Database db;
            DbCommand updateStagingIsCustomerNew;

            bool result=false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateStagingIsCustomerNew = db.GetStoredProcCommand("SP_UpdateCombinationStagingIsCustomerNew");
                db.ExecuteNonQuery(updateStagingIsCustomerNew);
                result=true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFUploadDao.cs:UpdateCombinationStagingIsCustomerNew()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public bool UpdateCombinationStagingIsFolioNew()
        {
            Database db;
            DbCommand updateStagingIsFolioNew;

            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateStagingIsFolioNew = db.GetStoredProcCommand("SP_UpdateCombinationStagingIsFolioNew");
                db.ExecuteNonQuery(updateStagingIsFolioNew);
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

                FunctionInfo.Add("Method", "MFUploadDao.cs:UpdateCombinationStagingIsFolioNew()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public bool UpdateKarvyProfileStagingIsCustomerNew(int adviserId,int processId)
        {
            Database db;
            DbCommand updateStagingIsFolioNew;

            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateStagingIsFolioNew = db.GetStoredProcCommand("SP_UpdateKarvyProfileStagingIsCustomerNew");
                db.AddInParameter(updateStagingIsFolioNew, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(updateStagingIsFolioNew, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(updateStagingIsFolioNew);
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

                FunctionInfo.Add("Method", "MFUploadDao.cs:UpdateKarvyProfileStagingIsCustomerNew()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public bool UpdateKarvyProfileStagingIsFolioNew(int processId)
        {
            Database db;
            DbCommand updateStagingIsFolioNew;

            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateStagingIsFolioNew = db.GetStoredProcCommand("SP_UpdateKarvyProfileStagingIsFolioNew");
                db.AddInParameter(updateStagingIsFolioNew, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(updateStagingIsFolioNew);
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

                FunctionInfo.Add("Method", "MFUploadDao.cs:UpdateKarvyProfileStagingIsFolioNew()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public DataSet GetKarvyNewFolios(int adviserId)
        {
            DataSet uploadsFolioList = new DataSet();
            //KarvyUploadsVo KarvyUploadsVo;
            Database db;
            DbCommand getNewFoliosCmd;
            DataSet getNewFoliosDs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getNewFoliosCmd = db.GetStoredProcCommand("SP_UploadGetNewFolios");
                db.AddInParameter(getNewFoliosCmd, "@adviserId", DbType.Int32, adviserId);
                getNewFoliosDs = db.ExecuteDataSet(getNewFoliosCmd);
                //dr = getNewCustomersDs.Tables[0].Rows[0];
                
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFUploadDao.cs:GetKarvyNewFolios()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return getNewFoliosDs;
        }

        public DataSet GetKarvyProfileNewFolios(int processId)
        {
            DataSet uploadsFolioList = new DataSet();
            //KarvyUploadsVo KarvyUploadsVo;
            Database db;
            DbCommand getNewFoliosCmd;
            DataSet getNewFoliosDs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getNewFoliosCmd = db.GetStoredProcCommand("SP_UploadGetKarvyProfileNewFolios");
                db.AddInParameter(getNewFoliosCmd, "@processId", DbType.Int32, processId);
                getNewFoliosDs = db.ExecuteDataSet(getNewFoliosCmd);
                //dr = getNewCustomersDs.Tables[0].Rows[0];
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFUploadDao.cs:GetKarvyProfileNewFolios()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return getNewFoliosDs;
        }

        public bool createNewFolios(int portfolioId, string folioNum, int userId)
        {
            bool result = false;
            Database db;
            DbCommand createNewFoliosCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createNewFoliosCmd = db.GetStoredProcCommand("SP_CreateNewFolios");
                db.AddInParameter(createNewFoliosCmd, "@portfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(createNewFoliosCmd, "@folioNum", DbType.String, folioNum);
                db.AddInParameter(createNewFoliosCmd, "@userId", DbType.Int32, userId);
                db.ExecuteNonQuery(createNewFoliosCmd);
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

                FunctionInfo.Add("Method", "KarvyUploadsDao.cs:CreateNewFolios()");


                object[] objects = new object[3];
                objects[0] = portfolioId;
                objects[1] = folioNum;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public bool UploadsKarvyDataTranslationForReprocess(int processId)
        {
            Database db;
            DbCommand cmdCheckDataTrans;


            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCheckDataTrans = db.GetStoredProcCommand("SP_UploadsKarvyDataTranslationForReprocess");
                db.AddInParameter(cmdCheckDataTrans, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(cmdCheckDataTrans);
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

                FunctionInfo.Add("Method", "KarvyUploadsDao.cs:UploadsKarvyDataTranslationForReprocess()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

    }
}
