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

        // External File Types
        public const string UploadExternalTypeCAMS = "CA";
        public const string UploadExternalTypeKarvy = "KA";
        public const string UploadExternalTypeDeutsche = "DT";
        public const string UploadExternalTypeTemp = "TN";

        public const string UploadExternalTypeStandard = "WP";
        public const string UploadExternalTypeOdinNSE = "ODNSE";
        public const string UploadExternalTypeOdinBSE = "ODBSE";

        // File Type
        public const string UploadFileTypeProfile = "Profile";
        public const string UploadFileTypeFolio = "Folio";
        public const string UploadFileTypeSystematic = "Systematic";
        public const string UploadFileTypeTradeAccount = "Trade Account";
        public const string UploadFileTypeDematAccount = "Demat Account";
        public const string UploadFileTypeTransaction = "Transaction";

        public const string UploadFileTypeCombination = "Combination";
        public const string UploadExtensionTypeXLS = "xls";
        public const string UploadExtensionTypeCSV = "csv";
        public const string UploadExtensionTypeDBF = "dbf";

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
            DeutscheProfile = 18
        }
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

        public const string ProcessLogVo = "S_ProcessLogVo";
        public const string LoanProposalDataSet = "S_LoanProposalDataSet";
        public const string LoanProcessTracking = "S_LoanProcessTracking";

        public const string CurrentUserRole = "S_CurrentUserRole";


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
