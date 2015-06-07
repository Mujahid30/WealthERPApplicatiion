using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoUser
{
    /// <summary>
    /// Class Containing RM Details of a particular RM.
    /// </summary>
    public class RMVo:UserVo
    {
        #region Fields

        private int m_RMId;
        private int m_AdviserId;
        private string m_StaffCode;
        private string m_OfficePhoneExtIsd;
        private string m_OfficePhoneExtStd;
        private int m_OfficePhoneExtNumber;
        private string m_OfficePhoneDirectIsd;
        private string m_OfficePhoneDirectStd;
        private int m_OfficePhoneDirectNumber;
        private int m_ResPhoneStd;
        private int m_ResPhoneIsd;
        private int m_ResPhoneNumber;
        private int m_FaxIsd;
        private int m_FaxStd;
        private int m_Fax;
        private long m_Mobile;
        private string m_RMRole;
        private string m_RoleList;
        private string m_MainBranch;
        private string m_BranchList;
        private int m_AdviserAgentId;
        private string m_EUIN;
        private string m_AAC_AgentCode;
        public Int32 HierarchyRoleId { get; set; }
        public Int32 ReportingManagerId { get; set; }
        public Int32 BranchId { get; set; }
        public Int16 HierarchyTitleId { get; set; }
        public Int16 HierarchyTeamId { get; set; }
        public bool IsAssociateUser { get; set; }
        public String StaffBranchAssociation { get; set; }
        public string userRole { get; set; }
        public short IsBranchOps { get; set; }
        #endregion Fields

        #region Properties
        public int AdviserAgentId
        {
            get { return m_AdviserAgentId; }
            set { m_AdviserAgentId = value; }
        }
        public string AAC_AgentCode
        {
            get { return m_AAC_AgentCode; }
            set { m_AAC_AgentCode = value; }
        }

        public string BranchList
        {
            get { return m_BranchList; }
            set { m_BranchList = value; }
        }
        public String StaffCode
        {
            get { return m_StaffCode; }
            set { m_StaffCode = value; }
        }
        public string MainBranch
        {
            get { return m_MainBranch; }
            set { m_MainBranch = value; }
        }

        public int RMId
        {
            get { return m_RMId; }
            set { m_RMId = value; }
        }


        public int AdviserId
        {
            get { return m_AdviserId; }
            set { m_AdviserId = value; }
        }
   
        public string OfficePhoneExtIsd
        {
            get { return m_OfficePhoneExtIsd; }
            set { m_OfficePhoneExtIsd = value; }
        }
       

        public string OfficePhoneExtStd
        {
            get { return m_OfficePhoneExtStd; }
            set { m_OfficePhoneExtStd = value; }
        }
       

        public int OfficePhoneExtNumber
        {
            get { return m_OfficePhoneExtNumber; }
            set { m_OfficePhoneExtNumber = value; }
        }
       
        public string OfficePhoneDirectIsd
        {
            get { return m_OfficePhoneDirectIsd; }
            set { m_OfficePhoneDirectIsd = value; }
        }
       
        public string OfficePhoneDirectStd
        {
            get { return m_OfficePhoneDirectStd; }
            set { m_OfficePhoneDirectStd = value; }
        }
       

        public int OfficePhoneDirectNumber
        {
            get { return m_OfficePhoneDirectNumber; }
            set { m_OfficePhoneDirectNumber = value; }
        }
      
        public int ResPhoneStd
        {
            get { return m_ResPhoneStd; }
            set { m_ResPhoneStd = value; }
        }
       

        public int ResPhoneIsd
        {
            get { return m_ResPhoneIsd; }
            set { m_ResPhoneIsd = value; }
        }
        
        public int ResPhoneNumber
        {
            get { return m_ResPhoneNumber; }
            set { m_ResPhoneNumber = value; }
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

        public long  Mobile
        {
            get { return m_Mobile; }
            set { m_Mobile = value; }
        }

        public string RMRole
        {
            get { return m_RMRole; }
            set { m_RMRole = value; }
        }
        public string RMRoleList
        {
            get { return m_RoleList; }
            set { m_RoleList = value; }
        }
        public string EUIN
        {
            get { return m_EUIN; }
            set { m_EUIN = value; }
        }
        public Int16 IsExternal { get; set; }

        public Double CTC { get; set; }

        public int departmentId { get; set; }

        public string roleIds { get; set; }
        public string Phone3Isd { get; set; }
        public string Phone3Std { get; set; }
        public string Phone3Number { get; set; }


        #endregion Properties
    }
}
