using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WealthERP.Base;
using BoCommon;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Text;
using VoSuperAdmin;
using BoSuperAdmin;
using DaoSuperAdmin;
using System.Globalization;
using System.Configuration;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using System.Web.UI.HtmlControls;
using VoUser;

namespace WealthERP.SuperAdmin
{
    public partial class AddIssue : System.Web.UI.UserControl
    {
        IssueTrackerDao superAdmincsissueTrackerDao = new IssueTrackerDao();
        IssueTrackerVo superAdminCSIssueTrackerVo = new IssueTrackerVo();
        IssueTrackerBo superAdminOpsBo = new IssueTrackerBo();
        DataTable dtIssueTrackerDetails = new DataTable();
        int roleId = 0;
        int treeSubNodeId=0;
        int treeSubSubNodeId=0;
        int adviserId=0;
        int issueTypeId = 0;
        string strOrgName;
        string strddlLevel;
        string strActiveLevel;
        string strStatus;
        DateTime strCloseDate;
        int strCsId;
        DataSet dsTreeNode = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Drawing.Color color = System.Drawing.Color.FromArgb(255 - 0 - 0);
            lblOpenClose.Attributes.Add("style", "text-decoration:blink");
            lblTypes.Attributes.Add("style", "text-decoration:blink");
            lblOpenClose.BackColor.GetBrightness();
            lblTypes.BackColor.GetBrightness();
            txtVersionReadOnly.Enabled = false;
            tblErrorMassage.Visible = false;
            QueryStringSelection();
            txtIssueDate.Text = DateTime.Now.ToShortDateString();
            txtIssueDate.Enabled = false;
            txtIssueCode.Enabled = false;
            BindDDLPriority();
            BindDDLLevel();
            
            BindDDLStatus();
                        
            BindDDLCustomerPriority();
            if (!IsPostBack)
            {
                BindDDLTypes(issueTypeId);
                BindDDLAdviser(adviserId);
                BindDDLRoleList(dtIssueTrackerDetails);
                TreeNodeDropdown(roleId);
                TreeSubNodeDropdown(roleId,treeSubNodeId);
                TreeSubSubNodeDropdown(roleId, treeSubNodeId,treeSubSubNodeId);
                LevelSelectionFromQueryString();
            }

            if (ddlIssueStatus.SelectedValue.Equals("3"))
            {
                level1ControlDisable();
                level2ControlDisable();
                level3ControlDisable();
                btnSubmit.Visible = false;
                btnUpdate.Visible = false;
                QASubmit.Visible = false;
                btnQAUpdate.Visible = false;
                btnDEVSubmit.Visible = false;
                btnDevUpdate.Visible = false;
            }            
        }

        public void QueryStringSelection()
        {
            if (Request.QueryString["strCsId"] != null)
                strCsId = Int32.Parse(Request.QueryString["strCsId"].ToString());

            if (Request.QueryString["strActiveLevel"] != null)
                strActiveLevel = Request.QueryString["strActiveLevel"].ToString();

            if (Request.QueryString["strStatus"] != null)
                strStatus = Request.QueryString["strStatus"].ToString();
        }

        public void advisorControlDisable()
        {
            ddlAdviser.Enabled = false;
            txtCustomerName.Enabled = false;
            txtCustomerPhone.Enabled = false;
            ddlRole.Enabled = false;
            ddlTreeNode.Enabled = false;
            ddlSubNode.Enabled = false;
            ddlSubSubNode.Enabled = false;
            txtDescription.Enabled = false;
            txtIssueCode.Enabled = false;
            txtIssueDate.Enabled = false;
            ddlIssueType.Enabled = false;
            ddlReportedBy.Enabled = false;
            ddlCustomerPriority.Enabled = false;
            txtVersionReadOnly.Enabled = false;
        }

        public void advisorControlEnable()
        {
            ddlAdviser.Enabled = true;
            txtCustomerName.Enabled = true;
            txtCustomerPhone.Enabled = true;
            ddlRole.Enabled = true;
            ddlTreeNode.Enabled = true;
            ddlSubNode.Enabled = true;
            ddlSubSubNode.Enabled = true;
            txtDescription.Enabled = true;
            txtIssueCode.Enabled = true;
            txtIssueDate.Enabled = true;
            ddlIssueType.Enabled = true;
            ddlReportedBy.Enabled = true;
            ddlCustomerPriority.Enabled = true;
        }

        public void LevelSelectionFromQueryString()
        {
            if (Request.QueryString["strCsId"] != null)
            {
                if (strActiveLevel == "Level1")
                {
                    LevelSelection();  
                    level2ControlDisable();
                    level3ControlDisable();
                }

                else if (strActiveLevel == "Level2")
                {
                    LevelSelection();          
                    level1ControlDisable();
                    level3ControlDisable();
                }

                else if (strActiveLevel == "Level3")
                {
                    LevelSelection();  
                    level2ControlDisable();
                    level1ControlDisable();
                }
            }
            else
            {
                int result = 0;
                string strPrefix = "CS";
                result = superAdminOpsBo.autoIncrementcsiSSUECode();

                if (result > 0)
                {
                    txtIssueCode.Text = strPrefix + result;
                    btnUpdate.Visible = false;
                    btnQAUpdate.Visible = false;
                    btnDevUpdate.Visible = false;
                    dtSolveDate.Enabled = false;
                    dtSolveDate.SelectedDate = DateTime.Now;
                }
                else
                {
                    result = 1;
                    txtIssueCode.Text = strPrefix + result;
                    btnUpdate.Visible = false;
                    btnQAUpdate.Visible = false;
                    btnDevUpdate.Visible = false;
                }
            }
        }

        public void LevelSelection()
        {
            advisorControlDisable();
            DataSet ds = superAdminOpsBo.getCSIssueDataAccordingToCSId(strCsId);
            BindDDLRoleList(dtIssueTrackerDetails);
            TreeNodeDropdown(roleId);
            TreeSubNodeDropdown(roleId, treeSubNodeId);
            TreeSubSubNodeDropdown(roleId, treeSubNodeId, treeSubSubNodeId);
            BindDDLTypes(issueTypeId);
            advisorControlDisable();
            if (ds != null)
            {
                CsLevelDataInsertion();
                QaLevelDataInsertion();
                DevLevelDataInsertion();
                dtSolveDate.Enabled = false;
            }
            else
            {
                tblErrorMassage.Visible = true;
                ErrorMessage.InnerHtml = "Record not found";
            }
            lblTypes.Text = strActiveLevel.ToString();
            lblOpenClose.Text = strStatus.ToString();
        }

        public void CsLevelDataInsertion()
        {
            DataSet ds = superAdminOpsBo.getCSIssueDataAccordingToCSId(strCsId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlAdviser.SelectedValue = ds.Tables[0].Rows[0]["A_AdviserId"].ToString();
                AdviserPhoneNo.Text = ds.Tables[0].Rows[0]["A_Phone1Number"].ToString();
                AdviserEmail.Text = ds.Tables[0].Rows[0]["A_Email"].ToString();
                txtCustomerName.Text = ds.Tables[0].Rows[0]["CSI_ContactPerson"].ToString();
                txtCustomerPhone.Text = ds.Tables[0].Rows[0]["CSI_Phone"].ToString();
                txtCustomerEmail.Text = ds.Tables[0].Rows[0]["CSI_Email"].ToString();
                ddlRole.SelectedValue = ds.Tables[0].Rows[0]["UR_RoleName"].ToString();
                ddlRole.SelectedValue = ds.Tables[0].Rows[0]["UR_RoleId"].ToString();
                if (ddlRole.SelectedValue != "Select User Role")

                    TreeNodeDropdown(int.Parse(ddlRole.SelectedValue));
                ddlTreeNode.SelectedValue = ds.Tables[0].Rows[0]["WTN_TreeNodeId"].ToString();

                if (ddlTreeNode.SelectedValue != "Select Tree Node")

                    TreeSubNodeDropdown(int.Parse(ddlRole.SelectedValue), int.Parse(ddlTreeNode.SelectedValue));
                ddlSubNode.SelectedValue = ds.Tables[0].Rows[0]["WTSN_TreeSubNodeId"].ToString();

                if (ddlSubNode.SelectedValue != "Select Tree SubNode")

                    TreeSubSubNodeDropdown(int.Parse(ddlRole.SelectedValue), int.Parse(ddlTreeNode.SelectedValue), int.Parse(ddlSubNode.SelectedValue));
                ddlSubSubNode.SelectedValue = ds.Tables[0].Rows[0]["WTSSN_TreeSubSubNodeId"].ToString();

                txtDescription.Text = ds.Tables[0].Rows[0]["CSI_CustomerDescription"].ToString();
                txtIssueCode.Text = ds.Tables[0].Rows[0]["CSI_Code"].ToString();
                txtIssueDate.Text = ds.Tables[0].Rows[0]["CSI_issueAddedDate"].ToString();
                ddlIssueType.SelectedValue = ds.Tables[0].Rows[0]["XMLCST_Code"].ToString();
                ddlReportedBy.SelectedValue = ds.Tables[0].Rows[0]["CSI_reportedVia"].ToString();
                ddlCustomerPriority.SelectedValue = ds.Tables[0].Rows[0]["XMLACSP_Code"].ToString();
                txtComments.Text = ds.Tables[0].Rows[0]["CSILA_Comments"].ToString();
                txtReportedBy.Text = ds.Tables[0].Rows[0]["CSI_Atuhor"].ToString();
                txtReportedDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CSI_ReportedDate"].ToString());
                ddlReportFromCS.SelectedValue = ds.Tables[0].Rows[0]["XMLCSL_Code"].ToString();
                ddlPriority.SelectedValue = ds.Tables[0].Rows[0]["XMLCSP_Code"].ToString();
                ddlIssueStatus.SelectedValue = ds.Tables[0].Rows[0]["XMLCSS_Code"].ToString();
                txtVersionReadOnly.Text = ds.Tables[0].Rows[0]["CSILA_Version"].ToString();
                if (ds.Tables[0].Rows[0]["CSI_ResolvedDate"].ToString().Equals(""))
                    dtSolveDate.SelectedDate = DateTime.Now;//wfh
                else
                    dtSolveDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CSI_ResolvedDate"].ToString());
                btnSubmit.Visible = false;
                btnUpdate.Visible = true;
            }
            else
            {
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
            }

        }

        public  void DevLevelDataInsertion()
        {            
            DataSet dsDEV = superAdminOpsBo.GetDEVCSIssueDataAccordingToCSId(strCsId);
                if (dsDEV.Tables[0].Rows.Count > 0)
                        {
                            txtDevComments.Text = dsDEV.Tables[0].Rows[0]["CSILA_Comments"].ToString();
                            ddlReportDEV.SelectedValue = dsDEV.Tables[0].Rows[0]["XMLCSL_Code"].ToString();
                            txtDevRepliedBy.Text = dsDEV.Tables[0].Rows[0]["CSILA_RepliedBy"].ToString();
                            if (dsDEV.Tables[0].Rows[0]["CSILA_RepliedDate"].ToString() == "")
                                txtDEVReportedDate.SelectedDate = DateTime.Now;     
                            else
                                txtDEVReportedDate.SelectedDate = DateTime.Parse(dsDEV.Tables[0].Rows[0]["CSILA_RepliedDate"].ToString());                            

                            if (txtDevComments.Text == "")
                            {
                                btnDevUpdate.Visible = false;
                                btnDEVSubmit.Visible = true;
                            }
                            else
                            {
                                btnDevUpdate.Visible = true;
                                btnDEVSubmit.Visible = false;
                            }
                        }
                    else if (txtDevComments.Text=="")
                            {
                                btnDevUpdate.Visible = false;
                                btnDEVSubmit.Visible = true;
                            }
                           
                        else
                            {
                                 btnDEVSubmit.Visible = true;
                                btnDevUpdate.Visible = false;
                            }
        }

        public void QaLevelDataInsertion()
        {
            DataSet dsQA = superAdminOpsBo.GetQACSIssueDataAccordingToCSId(strCsId );

            if (dsQA.Tables[0].Rows.Count >0)
            {
                txtQAComments.Text = dsQA.Tables[0].Rows[0]["CSILA_Comments"].ToString();
                txtRepliedBy.Text = dsQA.Tables[0].Rows[0]["CSILA_RepliedBy"].ToString();
                if (dsQA.Tables[0].Rows[0]["CSILA_RepliedDate"].ToString().Equals(""))
                    txtQAReportedDate.SelectedDate = DateTime.Now;
                else
                    txtQAReportedDate.SelectedDate = DateTime.Parse(dsQA.Tables[0].Rows[0]["CSILA_RepliedDate"].ToString());
                ddlReportQA.SelectedValue = dsQA.Tables[0].Rows[0]["XMLCSL_Code"].ToString();
                if (dsQA.Tables[0].Rows[0]["CSILA_Version"].ToString().Equals(""))
                {
                    txtVersionReadOnly.Text = "";
                    txteleaseVersion.Text = "";
                }
                else
                {
                    txteleaseVersion.Text = dsQA.Tables[0].Rows[0]["CSILA_Version"].ToString();
                    txtVersionReadOnly.Text = dsQA.Tables[0].Rows[0]["CSILA_Version"].ToString();
                }
                if (txteleaseVersion.Text != null)
                    txtVersionReadOnly.Text = txteleaseVersion.Text;
                else
                    txtVersionReadOnly.Text = "";
  
                if (txtRepliedBy.Text == "")
                {
                    btnQAUpdate.Visible = false;
                    QASubmit.Visible = true;
                }
                else
                {
                    btnQAUpdate.Visible = true;
                    QASubmit.Visible = false;
                }
                
            }
            else if (txtRepliedBy.Text == "")
            {
                btnQAUpdate.Visible = false;
                QASubmit.Visible = true;
            }
            
            else
            {
                btnUpdate.Visible = false;
                QASubmit.Visible = true;

            }
        }

        private void BindDDLPriority()
        {
            Dictionary<string, string> genDictPriority = new Dictionary<string, string>();
            
            DataTable dt = superAdminOpsBo.GetPriorityList();
            if (dt.Rows.Count > 0)
            {                
                foreach (DataRow dr in dt.Rows)
                {
                    genDictPriority.Add(dr["XMLCSP_Name"].ToString(), dr["XMLCSP_Code"].ToString());
                }
                
                if (ddlPriority != null)
                {
                    ddlPriority.DataSource = genDictPriority;
                    ddlPriority.DataTextField = "Key";
                    ddlPriority.DataValueField = "Value";
                    ddlPriority.DataBind();
                    ddlPriority.SelectedIndex = 0;

                    ddlCustomerPriority.DataSource = genDictPriority;
                    ddlCustomerPriority.DataTextField = "Key";
                    ddlCustomerPriority.DataValueField = "Value";
                    ddlCustomerPriority.DataBind();
                    ddlCustomerPriority.SelectedIndex = 0;
                }              
            }            
        }
        
        private void BindDDLCustomerPriority()
        {
            Dictionary<string, string> genDictPriority = new Dictionary<string, string>();

            DataTable dt = superAdminOpsBo.GetCustomerPriorityList();
            if (dt.Rows.Count > 0)
            {                
                foreach (DataRow dr in dt.Rows)
                {
                    genDictPriority.Add(dr["XMLACSP_Name"].ToString(), dr["XMLACSP_Code"].ToString());
                }
                
                if (ddlPriority != null)
                {
                    ddlCustomerPriority.DataSource = genDictPriority;
                    ddlCustomerPriority.DataTextField = "Key";
                    ddlCustomerPriority.DataValueField = "Value";
                    ddlCustomerPriority.DataBind();
                    ddlCustomerPriority.SelectedIndex = 0;
                }              
            }            
        }

        private void BindDDLLevel()
        {
            Dictionary<string, string> genDictPriority = new Dictionary<string, string>();
           
            DataTable dt = superAdminOpsBo.GetLevelList();
            if (dt.Rows.Count > 0)
            {               
                foreach (DataRow dr in dt.Rows)
                {
                    genDictPriority.Add(dr["XMLCSL_Name"].ToString(), dr["XMLCSL_Code"].ToString());
                }

                if (ddlPriority != null)
                {
                    ddlReportFromCS.DataSource = genDictPriority;
                    ddlReportFromCS.DataTextField = "Key";
                    ddlReportFromCS.DataValueField = "Value";
                    ddlReportFromCS.DataBind();
                    ddlReportFromCS.SelectedIndex = 1;
                    
                    ddlReportQA.DataSource = genDictPriority;
                    ddlReportQA.DataTextField = "Key";
                    ddlReportQA.DataValueField = "Value";
                    ddlReportQA.DataBind();
                    ddlReportQA.SelectedIndex = 2;
                    
                    ddlReportDEV.DataSource = genDictPriority;
                    ddlReportDEV.DataTextField = "Key";
                    ddlReportDEV.DataValueField = "Value";
                    ddlReportDEV.DataBind();
                    ddlReportDEV.SelectedIndex = 0;
                    
                }
            }
        }

        private void BindDDLStatus()
        {
            Dictionary<string, string> genDictPriority = new Dictionary<string, string>();

            DataTable dt = superAdminOpsBo.GetStatusList();
            if (dt.Rows.Count > 0)
            {               
                foreach (DataRow dr in dt.Rows)
                {
                    genDictPriority.Add(dr["XMLCSS_Name"].ToString(), dr["XMLCSS_Code"].ToString());
                }

                if (ddlPriority != null)
                {
                    ddlIssueStatus.DataSource = genDictPriority;
                    ddlIssueStatus.DataTextField = "Key";
                    ddlIssueStatus.DataValueField = "Value";
                    ddlIssueStatus.DataBind();
                    ddlIssueStatus.SelectedIndex = 0;                 

                }
            }
        }

        private void BindDDLAdviser(int advisorId)
        {
            Dictionary<string, string> genDictPriority = new Dictionary<string, string>();

            DataTable dt = superAdminOpsBo.GetAdviserList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    genDictPriority.Add(dr["A_OrgName"].ToString(), dr["A_AdviserId"].ToString());
                }
                if (ddlAdviser != null)
                {                    
                    ddlAdviser.DataSource = genDictPriority;
                    ddlAdviser.DataTextField = "Key";
                    ddlAdviser.DataValueField = "Value";
                    ddlAdviser.DataBind();
                    ddlAdviser.Items.Insert(0, new ListItem("Select", "Select"));
                }                
            }
            else
            {
                ddlAdviser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }
        }

        private void BindTreeNodeList()
        {
            Dictionary<string, string> genDictPriority = new Dictionary<string, string>();

            DataTable dt = superAdminOpsBo.GetAdviserList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    genDictPriority.Add(dr["A_OrgName"].ToString(), dr["A_AdviserId"].ToString());
                }

                if (ddlAdviser != null)
                {
                    ddlAdviser.DataSource = genDictPriority;
                    ddlAdviser.DataTextField = "Key";
                    ddlAdviser.DataValueField = "Value";
                    ddlAdviser.DataBind();
                    ddlAdviser.SelectedIndex = 0;
                    ddlAdviser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                }
                
            }
        }

        private void BindTreeSubNodeList()
        {
            Dictionary<string, string> genDictPriority = new Dictionary<string, string>();

            DataTable dt = superAdminOpsBo.GetAdviserList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    genDictPriority.Add(dr["A_OrgName"].ToString(), dr["A_AdviserId"].ToString());
                }

                if (ddlAdviser != null)
                {
                    ddlAdviser.DataSource = genDictPriority;
                    ddlAdviser.DataTextField = "Key";
                    ddlAdviser.DataValueField = "Value";
                    ddlAdviser.DataBind();
                    ddlAdviser.SelectedIndex = 0;
                    ddlAdviser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                }
                
            }
        }

        private void BindTreeSubSubNodeList()
        {
            Dictionary<string, string> genDictPriority = new Dictionary<string, string>();

            DataTable dt = superAdminOpsBo.GetAdviserList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    genDictPriority.Add(dr["A_OrgName"].ToString(), dr["A_AdviserId"].ToString());
                }

                if (ddlAdviser != null)
                {
                    ddlAdviser.DataSource = genDictPriority;
                    ddlAdviser.DataTextField = "Key";
                    ddlAdviser.DataValueField = "Value";
                    ddlAdviser.DataBind();
                    ddlAdviser.SelectedIndex = 0;
                    ddlAdviser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                }
                
            }
        } 

        private void BindDDLRoleList(DataTable dtRoleDropDown)
        {
            dtRoleDropDown = superAdminOpsBo.GetRoleList();
            try
            {
                if (dtRoleDropDown != null)
                {
                    ddlRole.DataSource = dtRoleDropDown;
                    ddlRole.DataValueField = dtRoleDropDown.Columns["UR_RoleId"].ToString();
                    ddlRole.DataTextField = dtRoleDropDown.Columns["UR_RoleName"].ToString();
                    ddlRole.DataBind();
                }
                ddlRole.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select User Role", "Select User Role"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        private void BindDDLTypes(int issueType)
        {
            Dictionary<string, string> genDictPriority = new Dictionary<string, string>();

            DataTable dt = superAdminOpsBo.GetTypesList();
            if (dt.Rows.Count > 0)
            {               
                foreach (DataRow dr in dt.Rows)
                {
                    genDictPriority.Add(dr["XMLCST_Name"].ToString(), dr["XMLCST_Code"].ToString());
                }

                if (ddlPriority != null)
                {
                    ddlIssueType.DataSource = genDictPriority;
                    ddlIssueType.DataTextField = "Key";
                    ddlIssueType.DataValueField = "Value";
                    ddlIssueType.DataBind();
                    ddlIssueType.Items.Insert(0, new ListItem("Select", "Select"));
                }
            }
            else
            {
                ddlIssueType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }
        }

        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int result = 0;
            int statusResult = 0;
            superAdminCSIssueTrackerVo.A_AdviserId=int.Parse(ddlAdviser.SelectedValue);
            superAdminCSIssueTrackerVo.CSI_ContactPerson = txtCustomerName.Text;
            superAdminCSIssueTrackerVo.CSI_Phone = txtCustomerPhone.Text;
            superAdminCSIssueTrackerVo.CSI_Email = txtCustomerEmail.Text;
            if (ddlRole.SelectedValue != "Select User Role")
                superAdminCSIssueTrackerVo.UR_RoleId = int.Parse(ddlRole.SelectedValue);
            if (ddlTreeNode.SelectedValue != "Select Tree Node")
                superAdminCSIssueTrackerVo.WTN_TreeNodeId = int.Parse(ddlTreeNode.SelectedValue);
            if (ddlSubNode.SelectedValue != "Select Tree SubNode")
                superAdminCSIssueTrackerVo.WTSN_TreeSubNodeId = int.Parse(ddlSubNode.SelectedValue);
            if (ddlSubSubNode.SelectedValue != "Select Tree SubSubNode")
                superAdminCSIssueTrackerVo.WTSSN_TreeSubSubNodeId = int.Parse(ddlSubSubNode.SelectedValue);
            superAdminCSIssueTrackerVo.CSI_CustomerDescription = txtDescription.Text;
            superAdminCSIssueTrackerVo.CSI_Code = txtIssueCode.Text;
            superAdminCSIssueTrackerVo.CSI_issueAddedDate =DateTime.Parse(txtIssueDate.Text);
            superAdminCSIssueTrackerVo.XMLCST_Code = int.Parse(ddlIssueType.SelectedValue);
            superAdminCSIssueTrackerVo.CSI_ReportedVia = ddlReportedBy.SelectedValue;
            superAdminCSIssueTrackerVo.XMLACSP_Code = int.Parse(ddlCustomerPriority.SelectedValue);
            superAdminCSIssueTrackerVo.CSILA_Comments = txtComments.Text;
            superAdminCSIssueTrackerVo.CSI_Atuhor = txtReportedBy.Text;
            superAdminCSIssueTrackerVo.CSI_ReportedDate = txtReportedDate.SelectedDate.Value;
            if (dtSolveDate.SelectedDate != null)
                superAdminCSIssueTrackerVo.CSI_ResolvedDate = dtSolveDate.SelectedDate.Value;
            else

                superAdminCSIssueTrackerVo.CSI_ResolvedDate = DateTime.MinValue;
            superAdminCSIssueTrackerVo.XMLCSL_Code = int.Parse(ddlReportFromCS.SelectedValue);
            superAdminCSIssueTrackerVo.XMLCSP_Code = int.Parse(ddlPriority.SelectedValue);
            superAdminCSIssueTrackerVo.XMLCSS_Code = int.Parse(ddlIssueStatus.SelectedValue);
            
            try
            {
                if (ddlIssueStatus.SelectedValue.Equals("3"))
                {
                    if (dtSolveDate.SelectedDate != DateTime.MinValue)
                    {
                        DateTime dt = dtSolveDate.SelectedDate.Value;
                        if (superAdminCSIssueTrackerVo.CSI_ResolvedDate == DateTime.MinValue)
                        {
                            superAdminCSIssueTrackerVo.CSI_ResolvedDate = dt;
                        }
                    }
                    else
                    {
                        dtSolveDate.SelectedDate = DateTime.MinValue;
                    }
                    statusResult = superAdminOpsBo.CloseIssueOnLevel1(superAdminCSIssueTrackerVo);

                    if (statusResult > 0)
                    {

                        tblErrorMassage.Visible = true;
                        ErrorMessage.InnerHtml = "Issue not Closed";
                    }
                    else
                        tblErrorMassage.Visible = true;
                    ErrorMessage.InnerHtml = "Issue Closed Successfully";
                    ShowIssueDetailsPage();
                }   

                else 
                {
                    result = superAdminOpsBo.InsertIntoCSIssueLevel1ToLevel1(superAdminCSIssueTrackerVo);
                     if (result > 0 && statusResult > 0)
                    {

                        tblErrorMassage.Visible = true;
                        ErrorMessage.InnerHtml = "Record not submitted successfully";
                    }
                    else
                        tblErrorMassage.Visible = true;
                    ErrorMessage.InnerHtml = "Record submitted successfully";
                    ShowIssueDetailsPage();
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public void level1ControlDisable()
        {
            txtCustomerEmail.Enabled = false;
            txtReportedBy.Enabled = false;
            txtReportedDate.Enabled = false;
            txtComments.Enabled = false;            
            ddlReportFromCS.Enabled = false;
            ddlPriority.Enabled = false;
            ddlIssueStatus.Enabled = false;
            btnSubmit.Enabled = false;
            btnUpdate.Enabled = false;            
        }

        public void level1ControlEnable()
        {
            txtCustomerEmail.Enabled = true;
            txtReportedBy.Enabled = true;
            txtReportedDate.Enabled = true;
            txtComments.Enabled = true;            
            ddlReportFromCS.Enabled = true;
            ddlPriority.Enabled = true;
            ddlIssueStatus.Enabled = true;
            btnSubmit.Enabled = true;
            btnUpdate.Enabled = true;           
        }

        public void level2ControlDisable()
        {
            txtQAComments.Enabled = false;
            txtRepliedBy.Enabled = false;
            ddlReportQA.Enabled = false;;
            QASubmit.Enabled=false;
            btnQAUpdate.Enabled = false;
            txtQAReportedDate.Enabled = false;
            txteleaseVersion.Enabled = false;
        }

        public void level2ControlEnable()
        {
            txtQAComments.Enabled = true;
            txtRepliedBy.Enabled = true;
            ddlReportQA.Enabled = true;  
            txtQAReportedDate.Enabled = true;
            txteleaseVersion.Enabled = true;
        }

        public void level3ControlDisable()
        {
            txtDevComments.Enabled = false;            
            ddlReportDEV.Enabled = false;
            btnDEVSubmit.Enabled = false;
            btnDevUpdate.Enabled = false;
            txtDEVReportedDate.Enabled = false;
            txtDevRepliedBy.Enabled = false;
        }

        public void level3ControlEnable()
        {
            txtDevRepliedBy.Enabled = true;
            txtDevComments.Enabled = true;
            ddlReportDEV.Enabled = true;
            btnDEVSubmit.Enabled = true;
            btnDevUpdate.Enabled = true;
            txtDEVReportedDate.Enabled = true;
        }

       
        protected void QASubmit_Click(object sender, EventArgs e)
        {
            int result = 0;
            int csId = 0;
            csId = strCsId;

            superAdminCSIssueTrackerVo.CSILA_Comments = txtQAComments.Text;
            superAdminCSIssueTrackerVo.CSILA_RepliedBy = txtRepliedBy.Text;
            superAdminCSIssueTrackerVo.CSI_ReportedDate = txtQAReportedDate.SelectedDate.Value;
            superAdminCSIssueTrackerVo.XMLCSL_Code = int.Parse(ddlReportQA.SelectedValue);
            superAdminCSIssueTrackerVo.CSI_id = strCsId;
            superAdminCSIssueTrackerVo.CSILA_Version = txteleaseVersion.Text;

            try
            {                
                    result = superAdminOpsBo.InsertIntoCSIssueLevel2ToAnyLevel(superAdminCSIssueTrackerVo);
                    if (result > 0)
                    {
                        tblErrorMassage.Visible = true;
                        ErrorMessage.InnerHtml = "Record not submitted successfully";
                    }
                    else
                    {
                        tblErrorMassage.Visible = true;
                        ErrorMessage.InnerHtml = "Record submitted successfully";
                    }
                    ShowIssueDetailsPage();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        protected void ddlSubNode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubNode.SelectedIndex != 0)
            {
                roleId = int.Parse(ddlRole.SelectedValue);
                treeSubNodeId = int.Parse(ddlTreeNode.SelectedValue);
                treeSubSubNodeId = int.Parse(ddlSubNode.SelectedValue);
                TreeSubSubNodeDropdown(roleId, treeSubNodeId, treeSubSubNodeId);
            }
            else
            {
                ddlRole.SelectedIndex = 0;
            }

        }

        private void TreeSubSubNodeDropdown(int roleId,int treeSubNodeId,int treeSubSubNodeId)
        {
            DataTable dtTreeSubSubNode = new DataTable();
            try
            {
                dsTreeNode = superAdminOpsBo.GetTreeSubSubNodeList(roleId, treeSubNodeId, treeSubSubNodeId);
                if (dsTreeNode.Tables.Count > 0)
                    dtTreeSubSubNode = dsTreeNode.Tables[0];
                if (dtTreeSubSubNode.Rows.Count > 0)
                {
                    ddlSubSubNode.DataSource = dtTreeSubSubNode;
                    ddlSubSubNode.DataValueField = dtTreeSubSubNode.Columns["WTSSN_TreeSubSubNodeId"].ToString();
                    ddlSubSubNode.DataTextField = dtTreeSubSubNode.Columns["WTSSN_TreeSubSubNodeText"].ToString();
                    ddlSubSubNode.DataBind();
                    ddlSubSubNode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Tree SubSubNode", "Select Tree SubSubNode"));
                }
                else
                {
                    ddlSubSubNode.Items.Clear();
                    ddlSubSubNode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Tree SubSubNode", "Select Tree SubSubNode"));
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (ddlRole.SelectedIndex != 0)
            {
                roleId = int.Parse(ddlRole.SelectedValue);
                TreeNodeDropdown(roleId);
            }
            else
            {
                ddlRole.SelectedIndex = 0;
            }

        }



        private void TreeNodeDropdown(int roleId)
        {
            DataTable dtTreeNode = new DataTable();
            try
            {
                dsTreeNode = superAdminOpsBo.GetTreeNodeList(roleId);
                if (dsTreeNode.Tables.Count > 0)
                    dtTreeNode = dsTreeNode.Tables[0];
                if (dtTreeNode.Rows.Count > 0)
                {
                    ddlTreeNode.DataSource = dtTreeNode;  
                    ddlTreeNode.DataValueField = dtTreeNode.Columns["WTN_TreeNodeId"].ToString();
                    ddlTreeNode.DataTextField = dtTreeNode.Columns["WTN_TreeNodeText"].ToString();
                    ddlTreeNode.DataBind();
                    ddlTreeNode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Tree Node", "Select Tree Node"));
                }
                else
                    ddlTreeNode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Tree Node", "Select Tree Node"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        private void TreeSubNodeDropdown(int roleId,int treeSubNodeId)
        {
            DataTable dtTreeSubNode = new DataTable();
            try
            {
                dsTreeNode = superAdminOpsBo.GetTreeSubNodeList(roleId, treeSubNodeId);
                if (dsTreeNode.Tables.Count > 0)
                    dtTreeSubNode = dsTreeNode.Tables[0];
                if (dtTreeSubNode.Rows.Count > 0)
                {
                    ddlSubNode.DataSource = dtTreeSubNode;
                    ddlSubNode.DataValueField = dtTreeSubNode.Columns["WTSN_TreeSubNodeId"].ToString();
                    ddlSubNode.DataTextField = dtTreeSubNode.Columns["WTSN_TreeSubNodeText"].ToString();
                    ddlSubNode.DataBind();
                    ddlSubNode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Tree SubNode", "Select Tree SubNode"));
                }
                else
                {
                    ddlSubNode.Items.Clear();
                    ddlSubNode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Tree SubNode", "Select Tree SubNode"));
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        protected void ddlTreeNode_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlTreeNode.SelectedIndex!= 0)
            {
                roleId = int.Parse(ddlRole.SelectedValue);
                treeSubNodeId = int.Parse(ddlTreeNode.SelectedValue);
                TreeSubNodeDropdown(roleId, treeSubNodeId);
            }
            else
            {
                ddlTreeNode.SelectedIndex = 0;
            }           
        }

        protected void ddlAdviser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAdviser.SelectedIndex != 0)
            {
                strOrgName = ddlAdviser.SelectedValue;
                dsTreeNode = superAdminOpsBo.GetAdviserPhoneNOandEmailidAccordingToAdviserName(strOrgName);
                fillAdviserPhoneNoandEmail(dsTreeNode.Tables[0]);
            }
            else
            {
                ddlAdviser.SelectedIndex = 0;
            }     
        }

        public void fillAdviserPhoneNoandEmail(DataTable dtPhoneNoandEmail)
        {
            if (dtPhoneNoandEmail.Rows.Count > 0)
            {
                AdviserTextBoxesShow();
                string strCode = dtPhoneNoandEmail.Rows[0]["A_Phone1STD"].ToString();
                string strPhoneNo = dtPhoneNoandEmail.Rows[0]["A_Phone1Number"].ToString();
                AdviserPhoneNo.Text = strCode +"-"+ strPhoneNo;
                AdviserEmail.Text = dtPhoneNoandEmail.Rows[0]["A_Email"].ToString();
            }
        }

        public void AdviserTextBoxesShow()
        {
            AdviserPhoneNo.Visible = true;
            AdviserEmail.Visible = true;
            lblAdviserPhoneNumber.Visible = true;
            lblAdviserEmailId.Visible = true;
            AdviserPhoneNo.Enabled = false;
            AdviserEmail.Enabled = false;
        }

        protected void btnDEVSubmit_Click(object sender, EventArgs e)
        {

            int result = 0;
            int csId = 0;
            csId = strCsId;

            superAdminCSIssueTrackerVo.CSILA_Comments = txtDevComments.Text;
            superAdminCSIssueTrackerVo.CSILA_RepliedBy = txtDevRepliedBy.Text;
            superAdminCSIssueTrackerVo.CSI_ReportedDate = txtDEVReportedDate.SelectedDate.Value;
            superAdminCSIssueTrackerVo.XMLCSL_Code = int.Parse(ddlReportDEV.SelectedValue);
            superAdminCSIssueTrackerVo.CSI_id = strCsId;
            //if (txteleaseVersion.Text != "")
            //    superAdminCSIssueTrackerVo.CSILA_Version = txteleaseVersion.Text;
            //else
            //    txteleaseVersion.Text = "";

            try
            {                               
                            result = superAdminOpsBo.InsertIntoCSIssueLevel3ToAnyLevel(superAdminCSIssueTrackerVo);
                            if (result > 0)
                            {
                                tblErrorMassage.Visible = true;
                                ErrorMessage.InnerHtml = "Record not submitted successfully";
                            }
                            else
                            {
                                tblErrorMassage.Visible = true;
                                ErrorMessage.InnerHtml = "Record submitted successfully";
                            }
                            ShowIssueDetailsPage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        protected void btnQAUpdate_Click(object sender, EventArgs e)
        {
            int result = 0;
            int activeLevel;
            activeLevel = int.Parse(ddlReportQA.SelectedValue);
            superAdminCSIssueTrackerVo.CSI_id = strCsId;
            superAdminCSIssueTrackerVo.XMLCSL_Code = activeLevel;            
            superAdminCSIssueTrackerVo.CSILA_Comments = txtQAComments.Text;
            superAdminCSIssueTrackerVo.CSILA_RepliedBy = txtRepliedBy.Text;
            superAdminCSIssueTrackerVo.CSILA_RepliedDate = DateTime.Parse(txtQAReportedDate.SelectedDate.ToString());
            superAdminCSIssueTrackerVo.CSILA_Version = txteleaseVersion.Text;
            
                result = superAdminOpsBo.UpdateCSIssueLevelAssociationLevel2ToAnyLevel(superAdminCSIssueTrackerVo);
                if (result > 0)
                {
                    tblErrorMassage.Visible = true;
                    ErrorMessage.InnerHtml = "Record not updated successfully";
                }
                else
                {
                    tblErrorMassage.Visible = true;
                    ErrorMessage.InnerHtml = "Record updated successfully";
                }
                ShowIssueDetailsPage();
        }

        protected void btnDevUpdate_Click(object sender, EventArgs e)
        {
            int result = 0;
            int activeLevel;
            activeLevel = int.Parse(ddlReportDEV.SelectedValue);
            superAdminCSIssueTrackerVo.CSI_id = strCsId;
            superAdminCSIssueTrackerVo.XMLCSL_Code = activeLevel;
            superAdminCSIssueTrackerVo.CSILA_Comments = txtDevComments.Text;
            superAdminCSIssueTrackerVo.CSILA_RepliedBy = txtDevRepliedBy.Text;
            superAdminCSIssueTrackerVo.CSILA_RepliedDate = DateTime.Parse(txtDEVReportedDate.SelectedDate.ToString());
            superAdminCSIssueTrackerVo.CSILA_Version = txteleaseVersion.Text;
            
            

                result = superAdminOpsBo.UpdateCSIssueLevelAssociationLevel3ToAnyLevel(superAdminCSIssueTrackerVo);
                if (result > 0)
                {
                    tblErrorMassage.Visible = true;
                    ErrorMessage.InnerHtml = "Record not updated successfully";
                }
                else
                {
                    tblErrorMassage.Visible = true;
                    ErrorMessage.InnerHtml = "Record updated successfully";
                }
                ShowIssueDetailsPage();
        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            int result = 0;
            int statusResult = 0;
            int activeLevel = int.Parse(ddlReportFromCS.SelectedValue);
            activeLevel = int.Parse(ddlReportFromCS.SelectedValue);
            superAdminCSIssueTrackerVo.CSI_ContactPerson = txtCustomerName.Text;
            superAdminCSIssueTrackerVo.CSI_Phone = txtCustomerPhone.Text;
            superAdminCSIssueTrackerVo.CSI_Email = txtCustomerEmail.Text;
            superAdminCSIssueTrackerVo.UR_RoleId = int.Parse(ddlRole.SelectedValue);           
            superAdminCSIssueTrackerVo.CSI_Code = txtIssueCode.Text;
            superAdminCSIssueTrackerVo.CSI_issueAddedDate = DateTime.Parse(txtIssueDate.Text);         
            superAdminCSIssueTrackerVo.CSI_ReportedVia = ddlReportedBy.SelectedValue;
            
            superAdminCSIssueTrackerVo.A_AdviserId = int.Parse(ddlAdviser.SelectedValue);
            superAdminCSIssueTrackerVo.XMLCST_Code = int.Parse(ddlIssueType.SelectedValue);
            superAdminCSIssueTrackerVo.XMLACSP_Code = int.Parse(ddlCustomerPriority.SelectedValue);
            
            superAdminCSIssueTrackerVo.CSILA_Comments = txtComments.Text;
            superAdminCSIssueTrackerVo.CSI_Atuhor = txtReportedBy.Text;
            superAdminCSIssueTrackerVo.CSI_ReportedDate = txtReportedDate.SelectedDate.Value;
            superAdminCSIssueTrackerVo.XMLCSL_Code = activeLevel;
            superAdminCSIssueTrackerVo.XMLCSP_Code = int.Parse(ddlPriority.SelectedValue);
            superAdminCSIssueTrackerVo.XMLCSS_Code = int.Parse(ddlIssueStatus.SelectedValue);
            superAdminCSIssueTrackerVo.CSI_id = strCsId;           
            superAdminCSIssueTrackerVo.CSILA_Comments = txtComments.Text;
            superAdminCSIssueTrackerVo.CSILA_RepliedBy = txtReportedBy.Text;
            superAdminCSIssueTrackerVo.CSILA_RepliedDate = DateTime.Parse(txtReportedDate.SelectedDate.ToString());
            if (txteleaseVersion.Text != "")
                superAdminCSIssueTrackerVo.CSILA_Version = txteleaseVersion.Text;
            else
                superAdminCSIssueTrackerVo.CSILA_Version = "";

            if (ddlIssueStatus.SelectedValue.Equals("3"))
            {
                if (dtSolveDate.SelectedDate != DateTime.MinValue)
                {
                    DateTime dt = dtSolveDate.SelectedDate.Value;
                    if (superAdminCSIssueTrackerVo.CSI_ResolvedDate == DateTime.MinValue)
                    {
                        superAdminCSIssueTrackerVo.CSI_ResolvedDate = dt;
                    }
                }
                else
                {
                    dtSolveDate.SelectedDate = DateTime.MinValue;
                }
                statusResult = superAdminOpsBo.CloseIssue(superAdminCSIssueTrackerVo);

                if (statusResult > 0)
                {

                    tblErrorMassage.Visible = true;
                    ErrorMessage.InnerHtml = "Issue not Closed";
                }
                else
                    tblErrorMassage.Visible = true;
                ErrorMessage.InnerHtml = "Issue Closed Successfully";
                ShowIssueDetailsPage();
            }            
                else
                {
                    result = superAdminOpsBo.UpdateCSIssueLevelAssociationLevel1ToAnyLevel(superAdminCSIssueTrackerVo);
                    if (result > 0)
                    {
                        tblErrorMassage.Visible = true;
                        ErrorMessage.InnerHtml = "Record updated successfully";
                    }
                    else
                    {
                        tblErrorMassage.Visible = true;
                        ErrorMessage.InnerHtml = "Record updated successfully";
                    }
                    ShowIssueDetailsPage();
                }
            
            }

            public void ShowIssueDetailsPage()
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('ViewIssuseDetails');", true);   
            }
        }
    }
