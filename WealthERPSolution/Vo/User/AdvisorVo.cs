﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoAdvisorProfiling;
using VoSuperAdmin;

namespace VoUser
{
    /// <summary>
    /// Class containing Advisor Details of a particular Advisor.
    /// </summary>
    public  class AdvisorVo:UserVo
    {
        #region Fields
        private int m_advisorId;       
        private string m_OrganizationName;
        private string m_BusinessCode;       
        private string m_AddressLine1;
        private string m_AddressLine2;
        private string m_AddressLine3;
        private string m_City;
        private string m_State;
        private int m_PinCode;
        private string m_Country;
        private int m_Phone1Std;
        private int m_Phone1Isd;
        private int m_Phone1Number;
        private int m_Phone2Std;
        private int m_Phone2Isd;
        private int m_Phone2Number;
        private long m_MobileNumber;       
        private string m_Email;
        private string m_Website;
        private int m_FaxStd;
        private int m_FaxIsd;
        private int m_Fax;
        private string m_ContactPersonFirstName;
        private string m_ContactPersonMiddleName;
        private string m_ContactPersonLastName;
        private int m_MultiBranch;
        private string m_LogoPath;
        private int m_Associates;
        private string m_Designation;
        private string s_Category;
        private string s_Status;
        private DateTime s_ActivationDate;
        private DateTime s_DeactivationDate;
        private Int16 s_IsActive;
        private string s_LOBAssetGroup;
        private Int16 s_IsDependent;
        private Int16 s_IsIPEnable;
        private Int16 s_IsOpsEnable;
        private float s_VaultSize;
        private string m_DomainName;
        private int m_HostId;
        private List<AdvisorLOBVo> advisorLOBVoList = new List<AdvisorLOBVo>();
        private AdviserSubscriptionVo advSubVo = new AdviserSubscriptionVo();
        private bool m_IsLoginWidgetEnable;
       

        #endregion Fields

        #region Properties
        public bool IsLoginWidgetEnable
        {
            get { return m_IsLoginWidgetEnable; }
            set { m_IsLoginWidgetEnable = value; }
        }

        public int Associates
        {
            get { return m_Associates; }
            set { m_Associates = value; }
        }
        public string AddressLine1
        {
            get { return m_AddressLine1; }
            set { m_AddressLine1 = value; }
        }
        
        public string AddressLine2
        {
            get { return m_AddressLine2; }
            set { m_AddressLine2 = value; }
        }
       
        public string AddressLine3
        {
            get { return m_AddressLine3; }
            set { m_AddressLine3 = value; }
        }
        
        public string City
        {
            get { return m_City; }
            set { m_City = value; }
        }
       
        public string State
        {
            get { return m_State; }
            set { m_State = value; }
        }
       
        public int PinCode
        {
            get { return m_PinCode; }
            set { m_PinCode = value; }
        }
       

        public string Country
        {
            get { return m_Country; }
            set { m_Country = value; }
        }
       

        public int Phone1Std
        {
            get { return m_Phone1Std; }
            set { m_Phone1Std = value; }
        }
       

        public int Phone1Isd
        {
            get { return m_Phone1Isd; }
            set { m_Phone1Isd = value; }
        }
       
        public int Phone1Number
        {
            get { return m_Phone1Number; }
            set { m_Phone1Number = value; }
        }
      
        public int Phone2Std
        {
            get { return m_Phone2Std; }
            set { m_Phone2Std = value; }
        }
       
        public int Phone2Isd
        {
            get { return m_Phone2Isd; }
            set { m_Phone2Isd = value; }
        }
       
        public int Phone2Number
        {
            get { return m_Phone2Number; }
            set { m_Phone2Number = value; }
        }
        public long MobileNumber
        {
            get { return m_MobileNumber; }
            set { m_MobileNumber = value; }
        }
       
     
        public int FaxIsd
        {
            get { return m_FaxIsd; }
            set { m_FaxIsd = value; }
        }
    

        public int FaxStd
        {
            get { return m_FaxStd; }
            set { m_FaxStd = value; }
        }
       

        public int Fax
        {
            get { return m_Fax; }
            set { m_Fax = value; }
        }
       
        public string ContactPersonFirstName
        {
            get { return m_ContactPersonFirstName; }
            set { m_ContactPersonFirstName = value; }
        }
       
        public string ContactPersonMiddleName
        {
            get { return m_ContactPersonMiddleName; }
            set { m_ContactPersonMiddleName = value; }
        }
       
        public string ContactPersonLastName
        {
            get { return m_ContactPersonLastName; }
            set { m_ContactPersonLastName = value; }
        }
     
        public int MultiBranch
        {
            get { return m_MultiBranch; }
            set { m_MultiBranch = value; }
        }
       
            
        public int advisorId
        {
            get { return m_advisorId; }
            set { m_advisorId = value; }
        }

        public string BusinessCode
        {
            get { return m_BusinessCode; }
            set { m_BusinessCode = value; }
        }
        public string OrganizationName
        {
            get { return m_OrganizationName; }
            set { m_OrganizationName = value; }
        }
        public string Website
        {
            get { return m_Website; }
            set { m_Website = value; }
        }
        public string LogoPath
        {
            get { return m_LogoPath; }
            set { m_LogoPath = value; }
        }

        public string Designation
        {
            get { return m_Designation; }
            set { m_Designation = value; }
        }
        public List<AdvisorLOBVo> AdvisorLOBVoList
        {
            get { return advisorLOBVoList; }
            set { advisorLOBVoList = value; }
        }
        public Int16 IsDependent
        {
            get { return s_IsDependent; }
            set { s_IsDependent = value; }
        }

        public Int16 IsIPEnable
        {
            get { return s_IsIPEnable; }
            set { s_IsIPEnable = value; }
        }

        public Int16 IsOpsEnable
        {
            get { return s_IsOpsEnable; }
            set { s_IsOpsEnable = value; }
        }

        public float VaultSize
        {
            get { return s_VaultSize; }
            set { s_VaultSize = value; }
        }

        public string LOBAssetGroup
        {
            get { return s_LOBAssetGroup; }
            set { s_LOBAssetGroup = value; }
        }
        public string Email1
        {
            get { return m_Email; }
            set { m_Email = value; }
        }
        public string Category
        {
            get { return s_Category; }
            set { s_Category = value; }
        }
        public string Status
        {
            get { return s_Status; }
            set { s_Status = value; }
        }
        public DateTime ActivationDate
        {
            get { return s_ActivationDate; }
            set { s_ActivationDate = value; }
        }
        public DateTime DeactivationDate
        {
            get { return s_DeactivationDate; }
            set { s_DeactivationDate = value; }
        }
        public Int16 IsActive
        {
            get { return s_IsActive; }
            set { s_IsActive = value; }
        }

        public string DomainName
        {
            get { return m_DomainName; }
            set { m_DomainName = value; }
        }

        public AdviserSubscriptionVo SubscriptionVo
        {
            get { return advSubVo; }
            set { advSubVo = value; }
        }

        public int HostId
        {
            get { return m_HostId; }
            set { m_HostId = value; }
        }

        #endregion Properties

    }

    public class AdviserIPVo
    {
        #region Fields
        private int m_advisorId;
        private int m_adviserPoolId;
        private string m_adviserIPs;
        private string m_AdviserIPComments;

        #endregion Fields



        #region Properties
        public int advisorId
        {
            get { return m_advisorId; }
            set { m_advisorId = value; }
        }
        public int advisorIPPoolId
        {
            get { return m_adviserPoolId; }
            set { m_adviserPoolId = value; }
        }

        public string AdviserIPs
        {
            get { return m_adviserIPs; }
            set { m_adviserIPs = value; }
        }

        public string AdviserIPComments
        {
            get { return m_AdviserIPComments; }
            set { m_AdviserIPComments = value; }
        }
        #endregion Properties
    }

    public class AdviserOnlineTransactionAMCLinksVo
    {
        #region Fields
        private int m_advisorId;
        private int m_AMCLinkId;
        private int m_AMCLinkUserCode;
        private int m_AMCLinkTypeCode;
        private string m_AMCLinks;
        private string m_AMCImagePath;
        private string m_ExternalLinkCode;
        private string m_AltLinkName;

       

        #endregion Fields



        #region Properties
        public int advisorId
        {
            get { return m_advisorId; }
            set { m_advisorId = value; }
        }
        public int AMCLinkId
        {
            get { return m_AMCLinkId; }
            set { m_AMCLinkId = value; }
        }

        public int AMCLinkUserCode
        {
            get { return m_AMCLinkUserCode; }
            set { m_AMCLinkUserCode = value; }
        }

        public int AMCLinkTypeCode
        {
            get { return m_AMCLinkTypeCode; }
            set { m_AMCLinkTypeCode = value; }
        }

        public string AMCLinks
        {
            get { return m_AMCLinks; }
            set { m_AMCLinks = value; }
        }

        public string AMCImagePath
        {
            get { return m_AMCImagePath; }
            set { m_AMCImagePath = value; }
        }

        public string ExternalLinkCode
        {
            get { return m_ExternalLinkCode; }
            set { m_ExternalLinkCode = value; }
        }
        public string AltLinkName
        {
            get { return m_AltLinkName; }
            set { m_AltLinkName = value; }
        }
        #endregion Properties
    }

    


}
