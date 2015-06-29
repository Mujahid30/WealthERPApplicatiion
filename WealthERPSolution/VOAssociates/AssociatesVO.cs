using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VOAssociates
{
    public class AssociatesVO
    {
        #region Fields

        private int m_AdviserAssociateId;
        private int m_UserId;
        private int m_RMId;
        private int m_BranchId;
        private string m_ContactPersonName;
        private int m_UserRoleId;
        private string m_Email;

        private int m_ResSTDCode;
        private int m_OfcSTDCode;
        private int m_ResPhoneNo;
        private int m_ResISDCode;
        private int m_OfcISDCode;
        private int m_OfficePhoneNo;
        private int m_ResFaxStd;
        private int m_ResFaxNumber;
        private int m_OfcFaxSTD;
        private int m_OfcFaxNumber;
        private long m_Mobile;


        private string m_CorrAdrLine1;
        private string  m_CorrAdrLine2;
        private string m_CorrAdrLine3;
        private int m_CorrAdrPinCode;
        private string m_CorrAdrCity;
        private string m_CorrAdrState;
        private string m_CorrAdrCountry;

        private string m_PerAdrLine1;
        private string m_PerAdrLine2;
        private string m_PerAdrLine3;
        private int m_PerAdrPinCode;
        private string m_PerAdrCity;
        private string m_PerAdrState;
        private string m_PerAdrCountry;

        private string m_MaritalStatusCode;
        private string m_Gender;
        private string m_QualificationCode;
        private DateTime  m_DOB;

        private string m_BankCode;
        private string m_BankAccountTypeCode;
        private string m_BranchName;
        private string m_AccountNum;
        private string m_BranchAdrLine1;
        private string m_BranchAdrLine2;
        private string m_BranchAdrLine3;
        private string m_BranchAdrCity;
        private string m_BranchAdrState;
        private string m_BranchAdrCountry;
        private string m_MICR;
        private string m_IFSC;


        private int m_AdviserAgentId;
        private string m_AgentCode;
        private string m_UserType;
        private int m_AgentCreatedBy;
        private int m_AgentModifiedBy;

        private string m_RMName;
        private string m_BMName;

        private string m_CurrentStatus;
        private string m_Stats;
        private string m_StatusCode;
        private string m_StepCode;
        private string m_StepName;
        

        private int m_ModifiedBy;
        private int m_CreatedBy;
        private string m_PanNo;
        private DateTime m_RequestDate;

        private string m_NomineeName;
        private string m_RelationshipCode;
        private string m_NomineeAddres;
        private int m_NomineeTelNo;
        private string m_GuardianName;
        private string m_GuardianRelationship;
        private string m_GuardianAddress;
        private int m_GuardianTelNo;

        private string m_assetGroupCode;
        private string m_Registrationumber;
        private DateTime m_ExpiryDate;

        private string m_AdviserCategory;

        private string m_AMFIregistrationNo;
        private DateTime m_StartDate;
        private DateTime m_EndDate;
        private int m_NoOfBranches;
        private int m_NoOfSalesEmployees;
        private int m_NoOfSubBrokers;
        private int m_NoOfClients;
        private DateTime m_AssociationExpairyDate;
        public int adviserhirerchi { get; set; }
        public string Roleid { get; set; }
        public int Departmrntid { get; set; }
        public int IsActive { get; set; }
        public int IsDummy { get; set; }
        private string m_WelcomeNotePath;
        private int m_categoryId { get; set; }
        #endregion

        #region Properties

        
        //public string[] PermisionList { get; set; } 
        public int categoryId
        {
            get { return m_categoryId; }
            set { m_categoryId = value; }
        }
        public int AdviserAssociateId
        {
            get { return m_AdviserAssociateId; }
            set { m_AdviserAssociateId = value; }
        }
        public int UserId
        {
            get { return m_UserId; }
            set { m_UserId = value; }
        }
        public int RMId
        {
            get { return m_RMId; }
            set { m_RMId = value; }
        }
        public int BranchId
        {
            get { return m_BranchId; }
            set { m_BranchId = value; }
        }
        public string ContactPersonName
        {
            get { return m_ContactPersonName; }
            set { m_ContactPersonName = value; }
        }
        public int UserRoleId
        {
            get { return m_UserRoleId; }
            set { m_UserRoleId = value; }
        }
        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }
        public string WelcomeNotePath
        {
            get { return m_WelcomeNotePath; }
            set { m_WelcomeNotePath = value; }
        }
        public int  ResPhoneNo
        {
            get { return m_ResPhoneNo; }
            set { m_ResPhoneNo = value; }
        }
        public int ResISDCode
        {
            get { return m_ResISDCode; }
            set { m_ResISDCode = value; }
        }
        public int OfcISDCode
        {
            get { return m_OfcISDCode; }
            set { m_OfcISDCode = value; }
        }
        public int OfficePhoneNo
        {
            get { return m_OfficePhoneNo; }
            set { m_OfficePhoneNo = value; }
        }
        public long Mobile
        {
            get { return m_Mobile; }
            set { m_Mobile = value; }
        }
        public int ResSTDCode
        {
            get { return m_ResSTDCode; }
            set { m_ResSTDCode = value; }
        }
        public int OfcSTDCode
        {
            get { return m_OfcSTDCode; }
            set { m_OfcSTDCode = value; }
        }
        public int ResFaxStd
        {
            get { return m_ResFaxStd; }
            set { m_ResFaxStd = value; }
        }
        public int ResFaxNumber
        {
            get { return m_ResFaxNumber; }
            set { m_ResFaxNumber = value; }
        }
        public int OfcFaxSTD
        {
            get { return m_OfcFaxSTD; }
            set { m_OfcFaxSTD = value; }
        }
        public int OfcFaxNumber
        {
            get { return m_OfcFaxNumber; }
            set { m_OfcFaxNumber = value; }
        }

        public string CorrAdrLine1
        {
            get { return m_CorrAdrLine1; }
            set { m_CorrAdrLine1 = value; }
        }

        public string CorrAdrLine2
        {
            get { return m_CorrAdrLine2; }
            set { m_CorrAdrLine2 = value; }
        }

        public string CorrAdrLine3
        {
            get { return m_CorrAdrLine3; }
            set { m_CorrAdrLine3 = value; }
        }

        public int CorrAdrPinCode
        {
            get { return m_CorrAdrPinCode; }
            set { m_CorrAdrPinCode = value; }
        }

        public string CorrAdrCity
        {
            get { return m_CorrAdrCity; }
            set { m_CorrAdrCity = value; }
        }
        public string CorrAdrState
        {
            get { return m_CorrAdrState; }
            set { m_CorrAdrState = value; }
        }

        public string CorrAdrCountry
        {
            get { return m_CorrAdrCountry; }
            set { m_CorrAdrCountry = value; }
        }

        public string PerAdrLine1
        {
            get { return m_PerAdrLine1; }
            set { m_PerAdrLine1 = value; }
        }

        public string PerAdrLine2
        {
            get { return m_PerAdrLine2; }
            set { m_PerAdrLine2 = value; }
        }

        public string PerAdrLine3
        {
            get { return m_PerAdrLine3; }
            set { m_PerAdrLine3 = value; }
        }

        public int PerAdrPinCode
        {
            get { return m_PerAdrPinCode; }
            set { m_PerAdrPinCode = value; }
        }

        public string PerAdrCity
        {
            get { return m_PerAdrCity; }
            set { m_PerAdrCity = value; }
        }
        public string PerAdrState
        {
            get { return m_PerAdrState; }
            set { m_PerAdrState = value; }
        }

        public string PerAdrCountry
        {
            get { return m_PerAdrCountry; }
            set { m_PerAdrCountry = value; }
        }
        public string MaritalStatusCode
        {
            get { return m_MaritalStatusCode; }
            set { m_MaritalStatusCode = value; }
        }

        public string Gender
        {
            get { return m_Gender; }
            set { m_Gender = value; }
        }
        public string QualificationCode
        {
            get { return m_QualificationCode; }
            set { m_QualificationCode = value; }
        }

        public DateTime DOB
        {
            get { return m_DOB; }
            set { m_DOB = value; }
        }

        public string BankCode
        {
            get { return m_BankCode; }
            set { m_BankCode = value; }
        }
        public string BankAccountTypeCode
        {
            get { return m_BankAccountTypeCode; }
            set { m_BankAccountTypeCode = value; }
        }
        public string BranchName
        {
            get { return m_BranchName; }
            set { m_BranchName = value; }
        }
        public string AccountNum
        {
            get { return m_AccountNum; }
            set { m_AccountNum = value; }
        }
        public string BranchAdrLine1
        {
            get { return m_BranchAdrLine1; }
            set { m_BranchAdrLine1 = value; }
        }
        public string BranchAdrLine2
        {
            get { return m_BranchAdrLine2; }
            set { m_BranchAdrLine2 = value; }
        }
        public string BranchAdrLine3
        {
            get { return m_BranchAdrLine3; }
            set { m_BranchAdrLine3 = value; }
        }
        public string BranchAdrCity
        {
            get { return m_BranchAdrCity; }
            set { m_BranchAdrCity = value; }
        }
        public string BranchAdrState
        {
            get { return m_BranchAdrState; }
            set { m_BranchAdrState = value; }
        }
        
        public string BranchAdrCountry
        {
            get { return m_BranchAdrCountry; }
            set { m_BranchAdrCountry = value; }
        }
        public string MICR
        {
            get { return m_MICR; }
            set { m_MICR = value; }
        }
        public string IFSC
        {
            get { return m_IFSC; }
            set { m_IFSC = value; }
        }
        public int AAC_AdviserAgentId
        {
            get { return m_AdviserAgentId; }
            set { m_AdviserAgentId = value; }
        }
        public string AAC_AgentCode 
        {
            get { return m_AgentCode; }
            set { m_AgentCode = value; }
        }

        public string AAC_UserType
        {
            get { return m_UserType; }
            set { m_UserType = value; }
        }
        public int AAC_CreatedBy
        {
            get { return m_AgentCreatedBy; }
            set { m_AgentCreatedBy = value; }
        }
        public int AAC_ModifiedBy
        {
            get { return m_AgentModifiedBy; }
            set { m_AgentModifiedBy = value; }
        }
        public string RMNAme
        {
            get { return m_RMName; }
            set { m_RMName = value; }
        }
        public string BMName
        {
            get { return m_BMName; }
            set { m_BMName = value; }
        }
        //----------------------------------------Status details----------------------------------
        public string CurrentStatus
        {
            get { return m_CurrentStatus; }
            set { m_CurrentStatus = value; }
        }
        public string Status
        {
            get { return m_Stats; }
            set { m_Stats = value; }
        }
        public string StatusCode
        {
            get { return m_StatusCode; }
            set { m_StatusCode = value; }
        }
        public string StepCode
        {
            get { return m_StepCode; }
            set { m_StepCode = value; }
        }
        public string StepName
        {
            get { return m_StepName; }
            set { m_StepName = value; }
        }

        public int CreatedBy
        {
            get { return m_CreatedBy; }
            set { m_CreatedBy = value; }
        }
        public int ModifiedBy
        {
            get { return m_ModifiedBy; }
            set { m_ModifiedBy = value; }
        }
        public string PanNo
        {
            get { return m_PanNo; }
            set { m_PanNo = value; }
        }
        public DateTime RequestDate
        {
            get { return m_RequestDate; }
            set { m_RequestDate = value; }
        }
        public string NomineeName
        {
            get { return m_NomineeName; }
            set { m_NomineeName = value; }
        }
        public string RelationshipCode
        {
            get { return m_RelationshipCode; }
            set { m_RelationshipCode = value; }
        }
        public string NomineeAddres
        {
            get { return m_NomineeAddres; }
            set { m_NomineeAddres = value; }
        }
        public int NomineeTelNo
        {
            get { return m_NomineeTelNo; }
            set { m_NomineeTelNo = value; }
        }
        public string GuardianName
        {
            get { return m_GuardianName; }
            set { m_GuardianName = value; }
        }
        public string GuardianRelationship
        {
            get { return m_GuardianRelationship; }
            set { m_GuardianRelationship = value; }
        }
        public string GuardianAddress
        {
            get { return m_GuardianAddress; }
            set { m_GuardianAddress = value; }
        }
        public string assetGroupCode
        {
            get { return m_assetGroupCode; }
            set { m_assetGroupCode = value; }
        }
        public int GuardianTelNo
        {
            get { return m_GuardianTelNo; }
            set { m_GuardianTelNo = value; }
        }
        public string Registrationumber
        {
            get { return m_Registrationumber; }
            set { m_Registrationumber = value; }
        }
        public DateTime ExpiryDate
        {
            get { return m_ExpiryDate; }
            set { m_ExpiryDate = value; }
        }
        public string AdviserCategory
        {
            get { return m_AdviserCategory; }
            set { m_AdviserCategory = value; }
        }
        //--------------------------------------- Business Details----------------------------------

        public string AMFIregistrationNo
        {
            get { return m_AMFIregistrationNo; }
            set { m_AMFIregistrationNo = value; }
        }
        public DateTime StartDate
        {
            get { return m_StartDate; }
            set { m_StartDate = value; }
        }
        public DateTime EndDate
        {
            get { return m_EndDate; }
            set { m_EndDate = value; }
        }
        public int NoOfBranches
        {
            get { return m_NoOfBranches; }
            set { m_NoOfBranches = value; }
        }
        public int NoOfSalesEmployees
        {
            get { return m_NoOfSalesEmployees; }
            set { m_NoOfSalesEmployees = value; }
        }
        public int NoOfSubBrokers
        {
            get { return m_NoOfSubBrokers; }
            set { m_NoOfSubBrokers = value; }
        }
        public int NoOfClients
        {
            get { return m_NoOfClients; }
            set { m_NoOfClients = value; }
        }
        public DateTime AssociationExpairyDate
        {
            get { return m_AssociationExpairyDate; }
            set { m_AssociationExpairyDate = value; }
        }

        public string EUIN { get; set; }
        public string AssociateType { get; set; }
        public string AssociateSubType { get; set; }

        #endregion
    }
}
