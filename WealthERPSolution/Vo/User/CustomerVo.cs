using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoUser
{
    /// <summary>
    /// Class Containing Customer Details of particular Customer.
    /// </summary>
    public class CustomerVo:UserVo
    {
        #region Fields

        private int m_CustomerId;
        private string m_CustCode;
        private int m_RmId;
        private DateTime m_ProfilingDate;
        private string m_ParentCompany;
        private string m_Gender;
        private DateTime m_Dob;             
        private string m_Type;        
        private string m_SubType;        
        private string m_Salutation;        
        private string m_PANNum;        
        private string m_Adr1Line1;        
        private string m_Adr1Line2;        
        private string m_Adr1Line3;        
        private int m_Adr1PinCode;        
        private string m_Adr1City;        
        private string m_Adr1State;        
        private string m_Adr1Country;       
        private string m_Adr2Line1;        
        private string m_Adr2Line2;        
        private string m_Adr2Line3;       
        private int m_Adr2PinCode;        
        private string m_Adr2City;        
        private string m_Adr2State;       
        private string m_Adr2Country;        
        private int m_ResISDCode;        
        private int m_ResSTDCode;       
        private int m_ResPhoneNum;        
        private int m_OfcISDCode;        
        private int m_OfcSTDCode;
        private int m_OfcPhoneNum;
        private long m_Mobile1;        
        private long m_Mobile2;        
        private string m_Occupation;        
        private string m_Qualification;        
        private string m_MaritalStatus;       
        private string m_Nationality;        
        private string m_RBIRefNum;
        private DateTime m_RBIApprovalDate;   
        private string m_CompanyName;        
        private string m_OfcAdrLine1;        
        private string m_OfcAdrLine2;        
        private string m_OfcAdrLine3;        
        private int m_OfcAdrPinCode;        
        private string m_OfcAdrCity;       
        private string m_OfcAdrState;       
        private string m_OfcAdrCountry;
        private DateTime m_RegistrationDate;       
        private DateTime m_CommencementDate;             
        private string m_RegistrationPlace;      
        private string m_RegistrationNum;
        private string m_CompanyWebsite;     
        private string m_AltEmail;
        private int m_ISDFax;
        private int m_STDFax;
        private int m_Fax;
        private int m_OfcISDFax;
        private int m_OfcSTDFax;
        private int m_OfcFax;
        private string m_ParentCustomer;
        private int m_ProcessId;
        private string m_AssignedRM;
        private string m_ContactFirstName;
        private string m_ContactMiddleName;
        private string m_ContactLastName;
        private DateTime m_ResidenceLivingDate;
        private DateTime m_JobStartDate;
        private string m_MothersMaidenName;
        private int m_BranchId;
        private int m_AssociationId;
        private string m_BranchName;
        private string m_RelationShip;
        private DateTime m_MarriageDate;
        private int m_IsProspect=0;
        private int m_IsFPClient=0;
        private string m_RMName;
        private string m_RMEmail;
        private string m_RMOfficePhone;
        private long m_RMMobile;
        private int m_dummypan;
        private string m_advnote;
        private int m_custclassid;
        private int m_isact;


        public long RMMobile
        {
            get { return m_RMMobile; }
            set { m_RMMobile = value; }
        }
        public int CustomerClassificationID
        {
            get { return m_custclassid; }
            set { m_custclassid = value; }
        }
        public int IsActive
        {
            get { return m_isact; }
            set { m_isact = value; }
        }
        public string AdviseNote
        {
            get { return m_advnote; }
            set { m_advnote = value; }
        }
        public string RMOfficePhone
        {
            get { return m_RMOfficePhone; }
            set { m_RMOfficePhone = value; }
        }
        public string RMEmail
        {
            get { return m_RMEmail; }
            set { m_RMEmail = value; }
        }
        public string RMName
        {
            get { return m_RMName; }
            set { m_RMName = value; }
        }
        public DateTime MarriageDate
        {
            get { return m_MarriageDate; }
            set { m_MarriageDate = value; }
        }

        public string RelationShip
        {
            get { return m_RelationShip; }
            set { m_RelationShip = value; }
        }

        public string BranchName
        {
            get { return m_BranchName; }
            set { m_BranchName = value; }
        }

      
        #endregion Fields

        #region Properties
        public int AssociationId
        {
            get { return m_AssociationId; }
            set { m_AssociationId = value; }
        }
        public string MothersMaidenName
        {
            get { return m_MothersMaidenName; }
            set { m_MothersMaidenName = value; }
        }

       

        public DateTime JobStartDate
        {
            get { return m_JobStartDate; }
            set { m_JobStartDate = value; }
        }
        public DateTime ResidenceLivingDate
        {
            get { return m_ResidenceLivingDate; }
            set { m_ResidenceLivingDate = value; }
        }
        public int BranchId
        {
            get { return m_BranchId; }
            set { m_BranchId = value; }
        }

        public int DummyPAN
        {
            get { return m_dummypan; }
            set { m_dummypan = value; }
        }
        public string ContactFirstName
        {
            get { return m_ContactFirstName; }
            set { m_ContactFirstName = value; }
        }
        public string ContactLastName
        {
            get { return m_ContactLastName; }
            set { m_ContactLastName = value; }
        }
        public string ContactMiddleName
        {
            get { return m_ContactMiddleName; }
            set { m_ContactMiddleName = value; }
        }
        public DateTime RBIApprovalDate
        {
            get { return m_RBIApprovalDate; }
            set { m_RBIApprovalDate = value; }
        }
        public DateTime ProfilingDate
        {
            get { return m_ProfilingDate; }
            set { m_ProfilingDate = value; }
        }  
        public DateTime RegistrationDate
        {
            get { return m_RegistrationDate; }
            set { m_RegistrationDate = value; }
        }
        public DateTime CommencementDate
        {
            get { return m_CommencementDate; }
            set { m_CommencementDate = value; }
        }  

        public DateTime Dob
        {
            get { return m_Dob; }
            set { m_Dob = value; }
        } 
        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }

        public string CustCode
        {
            get { return m_CustCode; }
            set { m_CustCode = value; }
        }
        public int RmId
        {
            get { return m_RmId; }
            set { m_RmId = value; }
        }
       
        public string Gender
        {
            get { return m_Gender; }
            set { m_Gender = value; }
        }
       
        public string Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }
        public string SubType
        {
            get { return m_SubType; }
            set { m_SubType = value; }
        }
        public string Salutation
        {
            get { return m_Salutation; }
            set { m_Salutation = value; }
        }
        public string PANNum
        {
            get { return m_PANNum; }
            set { m_PANNum = value; }
        }
        public string Adr1Line1
        {
            get { return m_Adr1Line1; }
            set { m_Adr1Line1 = value; }
        }
        public string Adr1Line2
        {
            get { return m_Adr1Line2; }
            set { m_Adr1Line2 = value; }
        }
        public string Adr1Line3
        {
            get { return m_Adr1Line3; }
            set { m_Adr1Line3 = value; }
        }
        public int Adr1PinCode
        {
            get { return m_Adr1PinCode; }
            set { m_Adr1PinCode = value; }
        }
        public string Adr1City
        {
            get { return m_Adr1City; }
            set { m_Adr1City = value; }
        }
        public string Adr1State
        {
            get { return m_Adr1State; }
            set { m_Adr1State = value; }
        }
        public string Adr1Country
        {
            get { return m_Adr1Country; }
            set { m_Adr1Country = value; }
        }
        public string Adr2Line1
        {
            get { return m_Adr2Line1; }
            set { m_Adr2Line1 = value; }
        }
        public string Adr2Line2
        {
            get { return m_Adr2Line2; }
            set { m_Adr2Line2 = value; }
        }
        public string Adr2Line3
        {
            get { return m_Adr2Line3; }
            set { m_Adr2Line3 = value; }
        }
        public int Adr2PinCode
        {
            get { return m_Adr2PinCode; }
            set { m_Adr2PinCode = value; }
        }
        public string Adr2City
        {
            get { return m_Adr2City; }
            set { m_Adr2City = value; }
        }
        public string Adr2State
        {
            get { return m_Adr2State; }
            set { m_Adr2State = value; }
        }
        public string Adr2Country
        {
            get { return m_Adr2Country; }
            set { m_Adr2Country = value; }
        }
        public int ResISDCode
        {
            get { return m_ResISDCode; }
            set { m_ResISDCode = value; }
        }
        public int ResSTDCode
        {
            get { return m_ResSTDCode; }
            set { m_ResSTDCode = value; }
        }
        public int ResPhoneNum
        {
            get { return m_ResPhoneNum; }
            set { m_ResPhoneNum = value; }
        }
        public int OfcISDCode
        {
            get { return m_OfcISDCode; }
            set { m_OfcISDCode = value; }
        }
        public int OfcSTDCode
        {
            get { return m_OfcSTDCode; }
            set { m_OfcSTDCode = value; }
        }
        public int OfcPhoneNum
        {
            get { return m_OfcPhoneNum; }
            set { m_OfcPhoneNum = value; }
        }
        public long Mobile1
        {
            get { return m_Mobile1; }
            set { m_Mobile1 = value; }
        }
        public long Mobile2
        {
            get { return m_Mobile2; }
            set { m_Mobile2 = value; }
        }
        public string Occupation
        {
            get { return m_Occupation; }
            set { m_Occupation = value; }
        }
        public string Qualification
        {
            get { return m_Qualification; }
            set { m_Qualification = value; }
        }
        public string MaritalStatus
        {
            get { return m_MaritalStatus; }
            set { m_MaritalStatus = value; }
        }
        public string Nationality
        {
            get { return m_Nationality; }
            set { m_Nationality = value; }
        }
        public string RBIRefNum
        {
            get { return m_RBIRefNum; }
            set { m_RBIRefNum = value; }
        }
        
        public string CompanyName
        {
            get { return m_CompanyName; }
            set { m_CompanyName = value; }
        }
        public string OfcAdrLine1
        {
            get { return m_OfcAdrLine1; }
            set { m_OfcAdrLine1 = value; }
        }
        public string OfcAdrLine2
        {
            get { return m_OfcAdrLine2; }
            set { m_OfcAdrLine2 = value; }
        }
        public string OfcAdrLine3
        {
            get { return m_OfcAdrLine3; }
            set { m_OfcAdrLine3 = value; }
        }
        public int OfcAdrPinCode
        {
            get { return m_OfcAdrPinCode; }
            set { m_OfcAdrPinCode = value; }
        }
        public string OfcAdrCity
        {
            get { return m_OfcAdrCity; }
            set { m_OfcAdrCity = value; }
        }
        public string OfcAdrState
        {
            get { return m_OfcAdrState; }
            set { m_OfcAdrState = value; }
        }
        public string OfcAdrCountry
        {
            get { return m_OfcAdrCountry; }
            set { m_OfcAdrCountry = value; }
        }
       
        public string RegistrationPlace
        {
            get { return m_RegistrationPlace; }
            set { m_RegistrationPlace = value; }
        }
        public string RegistrationNum
        {
            get { return m_RegistrationNum; }
            set { m_RegistrationNum = value; }
        }
        public string CompanyWebsite
        {
            get { return m_CompanyWebsite; }
            set { m_CompanyWebsite = value; }
        } 
        public string AltEmail
        {
            get { return m_AltEmail; }
            set { m_AltEmail = value; }
        }
        public int ISDFax
        {
            get { return m_ISDFax; }
            set { m_ISDFax = value; }
        }
        public int STDFax
        {
            get { return m_STDFax; }
            set { m_STDFax = value; }
        }
        public int Fax
        {
            get { return m_Fax; }
            set { m_Fax = value; }
        }
        public int OfcISDFax
        {
            get { return m_OfcISDFax; }
            set { m_OfcISDFax = value; }
        }
        public int OfcSTDFax
        {
            get { return m_OfcSTDFax; }
            set { m_OfcSTDFax = value; }
        }
        public int OfcFax
        {
            get { return m_OfcFax; }
            set { m_OfcFax = value; }
        }
        public string ParentCustomer
        {
            get { return m_ParentCustomer; }
            set { m_ParentCustomer = value; }
        }

        public int ProcessId
        {
            get { return m_ProcessId; }
            set { m_ProcessId = value; }
        }

        public string ParentCompany
        {
            get { return m_ParentCompany; }
            set { m_ParentCompany = value; }
        }

        public string AssignedRM
        {
            get { return m_AssignedRM; }
            set { m_AssignedRM = value; }
        }

        public int IsFPClient
        {
            get { return m_IsFPClient; }
            set { m_IsFPClient = value; }
        }

        public int IsProspect
        {
            get { return m_IsProspect; }
            set { m_IsProspect = value; }
        }
        #endregion Properties

    }
}
