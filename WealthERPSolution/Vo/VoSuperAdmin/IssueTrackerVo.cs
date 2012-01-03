using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoSuperAdmin
{
    public class IssueTrackerVo
    {
        private int m_CSILA_ActiveLevel;
        private DateTime m_CSILA_RepliedDate;
        private string m_CSILA_RepliedBy;
        private string m_CSILA_Comments;
        private int m_CSI_id;

        private string m_CSI_Code;
        private int m_A_AdviserId;
        private string m_CSI_ContactPerson;
        private string m_CSI_Phone;
        private string m_CSI_Email;
        private int m_UR_RoleId;
        private int m_WTN_TreeNodeId;
        private int m_WTSN_TreeSubNodeId;
        private int m_WTSSN_TreeSubSubNodeId;
        private string m_CSI_CustomerDescription;
        private DateTime m_CSI_issueAddedDate;
        private string m_CSI_ReportedVia;
        private string m_CSI_CustomerSupportComments;
        private string m_CSI_Atuhor;
        private DateTime m_CSI_ReportedDate;
        private int m_XMLCSL_Code;
        private int m_XMLCSP_Code;
        private int m_XMLCSS_Code;
        private int m_XMLCST_Code;
        private int m_XMLACSP_Code;
        private string m_CSILA_Version;
        private DateTime m_CSI_ResolvedDate;

        public string CSILA_Version
        {
            get { return m_CSILA_Version; }
            set { m_CSILA_Version = value; }
        }

        public DateTime CSI_ResolvedDate
        {
            get { return m_CSI_ResolvedDate;}
            set { m_CSI_ResolvedDate=value;}
        }

        public int CSILA_ActiveLevel
        {
            get { return m_CSILA_ActiveLevel; }
            set { m_CSILA_ActiveLevel = value; }
        }
        public DateTime CSILA_RepliedDate
        {
            get { return m_CSILA_RepliedDate; }
            set { m_CSILA_RepliedDate = value; }
        }
        public string CSILA_RepliedBy
        {
            get { return m_CSILA_RepliedBy; }
            set { m_CSILA_RepliedBy = value; }
        }
        public string CSILA_Comments
        {
            get { return m_CSILA_Comments; }
            set { m_CSILA_Comments = value; }
        }

        public int CSI_id
        {
            get { return m_CSI_id; }
            set { m_CSI_id = value; }
        }
        public string CSI_Code
        {
            get { return m_CSI_Code; }
            set { m_CSI_Code = value; }
        }
        public int A_AdviserId
        {
            get { return m_A_AdviserId; }
            set { m_A_AdviserId = value; }
        }
        public string CSI_ContactPerson
        {
            get { return m_CSI_ContactPerson; }
            set { m_CSI_ContactPerson = value; }
        }
        public string CSI_Phone
        {
            get { return m_CSI_Phone; }
            set { m_CSI_Phone = value; }
        }
        public string CSI_Email
        {
            get { return m_CSI_Email; }
            set { m_CSI_Email = value; }
        }
        public int UR_RoleId
        {
            get { return m_UR_RoleId; }
            set { m_UR_RoleId = value; }
        }
        public int WTN_TreeNodeId
        {
            get { return m_WTN_TreeNodeId; }
            set { m_WTN_TreeNodeId = value; }
        }
        public int WTSN_TreeSubNodeId
        {
            get { return m_WTSN_TreeSubNodeId; }
            set { m_WTSN_TreeSubNodeId = value; }
        }
        public int WTSSN_TreeSubSubNodeId
        {
            get { return m_WTSSN_TreeSubSubNodeId; }
            set { m_WTSSN_TreeSubSubNodeId = value; }
        }
        public string CSI_CustomerDescription
        {
            get { return m_CSI_CustomerDescription; }
            set { m_CSI_CustomerDescription = value; }
        }
        public DateTime CSI_issueAddedDate
        {
            get { return m_CSI_issueAddedDate; }
            set { m_CSI_issueAddedDate = value; }
        }
        public string CSI_ReportedVia
        {
            get { return m_CSI_ReportedVia; }
            set { m_CSI_ReportedVia = value; }
        }
        public string CSI_CustomerSupportComments
        {
            get { return m_CSI_CustomerSupportComments; }
            set { m_CSI_CustomerSupportComments = value; }
        }
        public string CSI_Atuhor
        {
            get { return m_CSI_Atuhor; }
            set { m_CSI_Atuhor = value; }
        }
        public DateTime CSI_ReportedDate
        {
            get { return m_CSI_ReportedDate; }
            set { m_CSI_ReportedDate = value; }
        }
        
        public int XMLCSL_Code
        {
            get { return m_XMLCSL_Code; }
            set { m_XMLCSL_Code = value; }
        }
        public int XMLCSP_Code
        {
            get { return m_XMLCSP_Code; }
            set { m_XMLCSP_Code = value; }
        }
        public int XMLCSS_Code
        {
            get { return m_XMLCSS_Code; }
            set { m_XMLCSS_Code = value; }
        }
        public int XMLCST_Code
        {
            get { return m_XMLCST_Code; }
            set { m_XMLCST_Code = value; }
        }
        public int XMLACSP_Code
        {
            get { return m_XMLACSP_Code; }
            set { m_XMLACSP_Code = value; }
        }        
    }
}
