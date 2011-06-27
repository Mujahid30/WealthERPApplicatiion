using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace VoAdvisorProfiling
{
    /// <summary>
    /// Class Containing Advisor's Branch Details
    /// </summary>
    public class AdvisorBranchVo
    {
        #region Fields
        private int m_AdviserId;

       
        private int m_BranchId;
        private string m_BranchCode;
        private string m_BranchName;
        private string m_AddressLine1;
        private string m_AddressLine2;
        private string m_AddressLine3;
        private string m_City;
        private int m_PinCode;
        private string m_State;
        private string m_Country;
        private int m_BranchHeadId;       
        private string m_Email;
        private int m_Phone1Std;
        private int m_Phone2Std;
        private int m_FaxStd;
        private int m_Phone1Number;
        private int m_Phone2Number;
        private int m_Fax;
        private int m_Phone1Isd;
        private int m_Phone2Isd;
        private int m_FaxIsd;
        private long m_MobileNumber;
        private int m_IsHeadBranch;

       
        public string LogoPath { get; set; }
        public string AssociateCategory { get; set; }
        public int AssociateCategoryId { get; set; }

        #endregion Fields

        #region Properties
        public int AdviserId
        {
            get { return m_AdviserId; }
            set { m_AdviserId = value; }
        }
        public int IsHeadBranch
        {
            get { return m_IsHeadBranch; }
            set { m_IsHeadBranch = value; }
        }
        public long MobileNumber
        {
            get { return m_MobileNumber; }
            set { m_MobileNumber = value; }
        }

        public int BranchId
        {
            get { return m_BranchId; }
            set { m_BranchId = value; }
        }
        public int BranchHeadId
        {
            get { return m_BranchHeadId; }
            set { m_BranchHeadId = value; }
        }
        public string BranchCode
        {
            get { return m_BranchCode; }
            set { m_BranchCode = value; }
        }
     
        public string BranchName
        {
            get { return m_BranchName; }
            set { m_BranchName = value; }
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
     
        public int PinCode
        {
            get { return m_PinCode; }
            set { m_PinCode = value; }
        }
      

        public string State
        {
            get { return m_State; }
            set { m_State = value; }
        }
        
        public string Country
        {
            get { return m_Country; }
            set { m_Country = value; }
        }
       
        
      
        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }
      
        public int Phone1Std
        {
            get { return m_Phone1Std; }
            set { m_Phone1Std = value; }
        }
      
        public int Phone2Std
        {
            get { return m_Phone2Std; }
            set { m_Phone2Std = value; }
        }
       


        public int FaxStd
        {
            get { return m_FaxStd; }
            set { m_FaxStd = value; }
        }
      

        public int Phone1Isd
        {
            get { return m_Phone1Isd; }
            set { m_Phone1Isd = value; }
        }
      
        public int Phone2Isd
        {
            get { return m_Phone2Isd; }
            set { m_Phone2Isd = value; }
        }
      
        public int FaxIsd
        {
            get { return m_FaxIsd; }
            set { m_FaxIsd = value; }
        }
       

        public int Phone1Number
        {
            get { return m_Phone1Number; }
            set { m_Phone1Number = value; }
        }
       
        public int Phone2Number
        {
            get { return m_Phone2Number; }
            set { m_Phone2Number = value; }
        }
        
        public int Fax
        {
            get { return m_Fax; }
            set { m_Fax = value; }
        }

        public string BranchType { get; set; }

        public string BranchHead { get; set; }

        public int BranchTypeCode { get; set; }

        #endregion Properties

     


    

    }
}
