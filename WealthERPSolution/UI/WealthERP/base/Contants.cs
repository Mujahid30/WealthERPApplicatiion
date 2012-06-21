using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WealthERP.Base
{
    public class Contants
    {
        // Uploads Constants
        public const string NSE = "NSE";
        public const string BSE = "BSE";
        public const string CERC = "CERC";
        public const string CAMS = "CAMS";
        public const string KARVY = "KARVY";
        public const string AMFI = "AMFI";
        public const string WERP = "WERP";
        public const string RejectedRecord = "RejectedRecord";

        // Extract Types
        public const string ExtractTypeProfile = "P";
        public const string ExtractTypeProfileFolio = "PMFF";
        public const string ExtractTypeFolio = "MFF";
        public const string ExtractTypeMFTransaction = "MFT";
        public const string ExtractTypeEQTradeAccount = "EQTA";
        public const string ExtractTypeEQDematAccount = "EQDA";
        public const string ExtractTypeEQTransaction = "EQT";
        public const string ExtractTypeMFSystematic = "MFSS";
        public const string ExtractTypeMFFolio = "MFFO";


        // External File Types
        public const string UploadExternalTypeCAMS = "CA";
        public const string UploadExternalTypeKarvy = "KA";
        public const string UploadExternalTypeDeutsche = "DT";
        public const string UploadExternalTypeTemp = "TN";

        public const string UploadExternalTypeStandard = "WP";
        public const string UploadExternalTypeIIFL = "IIFL";
        public const string UploadExternalTypeODIN = "ODIN";
        public const string UploadExternalTypeOdinNSE = "ODNSE";
        public const string UploadExternalTypeOdinBSE = "ODBSE";

        // File Type
        public const string UploadFileTypeProfile = "Profile";
        public const string UploadFileTypeFolio = "Folio";
        public const string UploadFileTypeSystematic = "Systematic";
        public const string UploadFileTypeTradeAccount = "Trade Account";
        public const string UploadFileTypeStdFolio = "Standard Folio";
        public const string UploadFileTypeDematAccount = "Demat Account";
        public const string UploadFileTypeTransaction = "Transaction";

        public const string UploadFileTypeCombination = "Combination";
        public const string UploadExtensionTypeXLS = "xls";
        public const string UploadExtensionTypeCSV = "csv";
        public const string UploadExtensionTypeDBF = "dbf";
        public const string UploadExtensionTypeTXT = "txt";

        public const string Reprocess = "Reprocess";
        public const string RollBack = "RollBack";
        public const string ManageRejects = "Manage Rejects";

        public const string Collectibles = "CL";
        public const string CashSavings = "CS";
        public const string DirectEquity = "DE";
        public const string FixedIncome = "FI";
        public const string Gold = "GD";
        public const string GovernmentSavings = "GS";
        public const string Insurance = "IN";
        public const string MutualFund = "MF";
        public const string PensionGratuities = "PG";
        public const string Personal = "PI";
        public const string PMS = "PM";
        public const string Property = "PR";

        public const string DecisionApproved = "AP";
        public const string DecisionDeclined = "DC";
        public const string DecisionAdditionalInfo = "AIR";




        public enum Source
        {
            Equity = 0,
            MF = 1
        }

        public enum Mode
        {
            New = 0,
            Edit = 1,
            View = 2
        }

        public enum ProductMaster
        {
            ScripMaster = 0,
            SchemeMaster = 1
        }

        public enum UploadTypes
        {
            CAMSTransaction = 1,
            CAMSProfile = 2,
            KarvyTransaction = 3,
            KarvyProfile = 4,
            KarvyCombination = 5,
            MFStandardTransaction = 6,
            StandardProfile = 7,
            EquityStandardTransaction = 8,
            MFStandardSystematicSetup = 9,
            OdinNSETransaction = 10,
            OdinBSETransaction = 11,
            EquityStandardDematAccount = 12,
            EquityStandardTradeAccount = 13,
            MFstandardFolio = 14,
            TempletonTransaction = 15,
            TempletonProfile = 16,
            DeutscheTransaction = 17,
            DeutscheProfile = 18,
            IIFLTransaction=19,
            ODINTransaction=20

        }

        #region Interest Calculator Constants

        public const string cStr_InstrumentType = "ITM_InstrumentType";
        public const string cStr_Id = "ITM_Id";
        public const string cStr_InstrumentType_DefaultMsg = "-Select Instrument Type-";
        public const string cStr_InstrumentType_DefaultMsg_Value = "-1";
        public const string cStr_InstrumentTypeId = "OM_InstrumentTypeId";
        public const string cStr_OutputType = "OM_OutputType";
        public const string cStr_OutputId = "OM_OutputId";
        public const string cStr_IOM_InstrumentTypeId = "IOM_InstrumentTypeId";
        public const string cStr_IOM_OutputTypeId = "IOM_OutputTypeId";
        public const string cStr_IM_InputType = "IM_InputType";
        public const string cStr_IM_Abbrevation = "IM_Abbrevation";
        public const string cStr_IOM_InputFlag = "IOM_InputFlag";
        public const string cStr_IOM_FieldType = "IOM_FieldType";
        public const string cStr_OFM_InstrumentTypeId = "OFM_InstrumentTypeId";
        public const string cStr_IF_InterestFreqauency = "IF_InterestFreqauency";
        public const string cStr_TblInputField = "tblInputField";

        public const int cStr_IT_KVP = 1;
        public const int cStr_IT_NSS = 2;
        public const int cStr_IT_NSC = 3;
        public const int cStr_IT_FixedDeposits = 4;
        public const int cStr_IT_CompanyFD = 5;
        public const int cStr_IT_GOIReliefBonds = 6;
        public const int cStr_IT_GOITaxSavingBonds = 7;
        public const int cStr_IT_TaxSavingBonds = 8;
        public const int cStr_IT_SeniorCitizensSavingsScheme = 9;
        public const int cStr_IT_PostOfficeSavingsBankAcc = 10;
        public const int cStr_IT_PostOfficeMIS = 11;
        public const int cStr_IT_Gratuity = 12;
        public const int cStr_IT_Annuity = 13;
        public const int cStr_IT_EPF = 14;
        public const int cStr_IT_PPF = 15;
        public const int cStr_IT_Superannuation = 16;
        public const int cStr_IT_SavingsAccount = 17;
        public const int cStr_IT_CurrentAccount = 18;
        public const int cStr_IT_CashAtHand = 19;
        public const int cStr_IT_LoansAndAdvances = 20;
        public const int cStr_IT_CorporateBonds = 21;

        public const int cStr_IO_CurrentValue = 1;
        public const int cStr_IO_MaturityValue = 2;
        public const int cStr_IO_InterestAccumulatedEarnedTillDate = 4;
        public const int cStr_IO_InterestAccumulatedEarnedTillMaturity = 5;

        public const int cStr_IO_InterestOnAccumalatedAmountAsOnLastFiscalYear = 9;
        public const int cStr_IO_InterestOnEmployeeContributionForCurrentFiscalYeare = 10;
        public const int cStr_IO_InterestOnEmployerContributionForCurrentFiscalYear = 11;
        public const int cStr_IO_InterestOnYearlyContributionForCurrentFiscalYear = 12;

        public const int cStr_IO_GratuityAmountWhenCoveredUnderGratuityAct = 13;
        public const int cStr_IO_GratuityAmountWhenNotCoveredUnderGratuityAct = 14;
        public const int cStr_IO_VestingValue = 15;
        public const int cStr_IO_WithdrawlAmount = 16;

        public const string cStr_Formula_InterestAccumulatedEarnedTillDate = "3";
        public const string cStr_Formula_SimpleInterestBasis = "9";
        public const string cStr_Formula_CompoundInterestAccumulatedEarnedTillDate = "1";
        public const string cStr_Formula_CurrentValueOrMaturityValue = "2";

        public const string cStr_FormulaMaster = "FormulaMaster";
        public const string cStr_InputMaster = "InputMaster";
        public const string cStr_InputOutputMapping = "InputOutputMapping";
        public const string cStr_InstrumentTypeMaster = "InstrumentTypeMaster";
        public const string cStr_InterestFrequency = "InterestFrequency";
        public const string cStr_OutputFormulaMapping = "OutputFormulaMapping";
        public const string cStr_OutputMapping = "OutputMapping";
        public const string cStr_OutputMaster = "OutputMaster";

        #endregion

    }

    public class SessionContents
    {
        // Session variable that holds the Current ControlID
        public const string Current_PageID = "Current_PageID";
        public const string SessionKey = "Sessionkey";
        public const string SessionValue = "Sessionvalue";
        public const string SessionLinksKey = "SessionLinksKey";
        public const string SessionLinksValue = "SessionLinksValue";

        public const string RmVo = "rmVo";
        public const string AdvisorId = "advisorId";
        public const string UserVo = "UserVo";
        public const string AdvisorBranchVo = "advisorBranchVo";
        public const string AdvisorVo = "advisorVo";
        public const string CustomerVo = "CustomerVo";
        public const string EventID = "S_EventID";
        public const string EventTransactionType = "S_EventTransactionType";
        public const string EventCode = "S_EventCode";
        public const string Reminder = "S_Reminder";
        public const string PortfolioId = "S_PortfolioId";
        public const string UploadProcessId = "S_UploadProcesId";
        public const string CustomerMFAccount = "S_CustomerMFAccount";
        public const string UploadFileTypeId = "S_UploadFileTypeID";
        public const string LogoPath = "S_LogoPath";
        public const string BranchLogoPath = "S_BranchLogoPath";
        public const string RepositoryVo = "S_RepositoryVo";

        public const string ProcessLogVo = "S_ProcessLogVo";
        public const string LoanProposalDataSet = "S_LoanProposalDataSet";
        public const string LoanProcessTracking = "S_LoanProcessTracking";

        public const string CurrentUserRole = "S_CurrentUserRole";
        public const string UserTopRole = "S_UserTopRole";


        //Session contents for FPSuperLite
        //===================================================================================================================================
        //Session contents for Add Prospect List
        public const string FPS_AddProspect_DataTable = "addProspectDataTable";

        public const string FPS_AddProspectListActionStatus = "addProspectListActionStatus";

        //Session contents for Prospect List
        public const string FPS_ProspectList_CustomerId = "prospectListCustomerId";

        //Session Contents for CustomerProspect Screen
        public const string FPS_CustomerPospect_ActionStatus = "customerProspectActionStatus";
        public const string FPS_CustomerProspect_CustomerVo = "customerProspectCustomerVo";
        public const string FPS_TreeView_Status = "FPS_TreeView_Status";
        //public const string FPS_CustomerProspect_CustomerFamilyDataTable = "customerProspectCustomerFamilyDataTablet";
        //===================================================================================================================================
        //Session contents for SuperAdmin Configuration
        //===================================================================================================================================
        public const string SAC_HostGeneralDetails = "hostgeneraldetails";

        //===================================================================================================================================
        
        // Session Contents for Valuation Date - Created on 27.03.2012, By: Joshan John
        public const string ValuationDate = "ValuationDate";

    }

    public static class Resources
    {
        private static string xml_path = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

        public static string XMLPath
        {
            get { return xml_path; }
        }
    }
}
