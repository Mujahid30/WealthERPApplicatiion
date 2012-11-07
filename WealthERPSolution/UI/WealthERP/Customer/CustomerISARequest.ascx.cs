using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using VoUser;
using BoCustomerProfiling;
using System.Text;
using WealthERP.Base;
using BoAdvisorProfiling;
using System.Data;

namespace WealthERP.Customer
{
    public partial class CustomerISARequest : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo;
        RMVo rmVo = new RMVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        int customerId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();

            rmVo = (RMVo)Session["rmVo"];
            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];
            hdnAdviserID.Value = advisorVo.advisorId.ToString();
            string userType = "";
            if (!string.IsNullOrEmpty(Session[SessionContents.RmVo].ToString()))
                rmVo = (RMVo)Session[SessionContents.RmVo];
            if (!IsPostBack)
            {
                int requestId=0;
                string stepCode = "";
                string stageStatus = "";

                if (Request.QueryString["RequestId"] != null)
                {
                    requestId = int.Parse(Request.QueryString["RequestId"].ToString());
                }
                if (Request.QueryString["stepCode"] != null)
                {
                    stepCode = Request.QueryString["stepCode"].ToString();
                }

                if (Request.QueryString["StatusCode"] != null)
                {
                    stageStatus = Request.QueryString["StatusCode"].ToString();
                }
                if (!String.IsNullOrEmpty(Session[SessionContents.CurrentUserRole].ToString()))
                {
                    userType = Session[SessionContents.CurrentUserRole].ToString();
                }
                
                BindReasonAndStatus();
                BindBranchDropDown();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Verification", " GetVerificationType('Normal');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CustomerSearch1", " DisplayCustomerSearch();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Priority", " enableDisablePriority('true');", true);

                txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                //txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                //txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                HideAndShowBasedOnRole(userType, stepCode,stageStatus);
            }
        }

        protected void txtCustomerId_ValueChanged1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {
                customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
                Session["customerVo"] = customerVo;
                lblGetBranch.Text = customerVo.BranchName;
                lblGetRM.Text = customerVo.RMName;
                txtPanNum.Text = customerVo.PANNum;
                hdnCustomerId.Value = txtCustomerId.Value;
                customerId = int.Parse(txtCustomerId.Value);
                txtPanNum.Enabled = false;
                if (customerVo.Mobile1.ToString() != "0" && customerVo.Mobile1.ToString() != "")
                    txtMobileNum.Text = customerVo.Mobile1.ToString();
                else
                {
                    txtMobileNum.Text = string.Empty;
                }
                txtMobileNum.Enabled = false;
                txtEmailID.Text = customerVo.Email;
                txtEmailID.Enabled = false;
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CustomerSearch", " HideCustomerSearch();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Verification", " GetVerificationType('Normal');", true);
        }

        protected void btnGenerateReqstNum_OnClick(object sender, EventArgs e)
        {
            CustomerVo newCustomerVo = new CustomerVo();
            UserVo userVo = (UserVo)Session["userVo"];
            List<int> customerIds = new List<int>();
            int custCreateFlag;
            if (String.IsNullOrEmpty(txtPanNum.Text))
            {
                hidValidCheck.Value = "";
            }
             
                    if (rdbExistingCustomer.Checked == true)
                    {
                        custCreateFlag = 0;
                        if (!string.IsNullOrEmpty(Session["customerVo"].ToString()))
                            customerVo = (CustomerVo)Session["customerVo"];
                        customerVo.CustomerCategoryCode = ddlCustomerCategory.SelectedValue;
                        customerIds = customerBo.CreateISACustomerRequest(customerVo, custCreateFlag);
                        txtGenerateReqstNum.Text = customerIds[3].ToString();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Priority", " enableDisablePriority('false');", true);
                    }
                    else if (rdbNewCustomer.Checked == true && hidValidCheck.Value != "0")
                    {
                        custCreateFlag = 0;
                        Int32 mobileNum = 0;
                        Int32.TryParse(txtMobileNum.Text, out mobileNum);
                        newCustomerVo.PANNum = txtPanNum.Text;
                        newCustomerVo.FirstName = txtCustomerNameEntry.Text;
                        newCustomerVo.Mobile1 = mobileNum;
                        newCustomerVo.Email = txtEmailID.Text;
                        newCustomerVo.RmId = rmVo.RMId;
                        newCustomerVo.UserId = userVo.UserId;
                        newCustomerVo.ProfilingDate = DateTime.Now;
                        newCustomerVo.BranchId = int.Parse(ddlBMBranch.SelectedValue);
                        newCustomerVo.CustomerCategoryCode = ddlCustomerCategory.SelectedValue;

                        //newCustomerVo.BranchId = rmVo.BranchList
                        customerIds = customerBo.CreateISACustomerRequest(newCustomerVo, custCreateFlag);
                        txtGenerateReqstNum.Text = customerIds[3].ToString();
                        customerVo = customerBo.GetCustomer(customerIds[1]);
                        Session["customerVo"] = customerVo;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Priority", " enableDisablePriority('false');", true);
                    }
                    else
                    {                        
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Pan Number Already Exists');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Priority", " enableDisablePriority('true');", true);                  
                    }                    
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CustomerSearch", " HideCustomerSearch();", true);
        }
        
        private void BindBranchDropDown()
        {
            DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(0, rmVo.RMId, 0);
            if (ds != null)
            {
                ddlBMBranch.DataSource = ds.Tables[1]; ;
                ddlBMBranch.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                ddlBMBranch.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                ddlBMBranch.DataBind();
            }
        }

        protected void btnSubmitAddStage1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                customerBo.UpdateCustomerISAStageDetails(int.Parse(txtGenerateReqstNum.Text), ddlStatusStage1.SelectedValue, ddlPriority.SelectedValue, "ISARQ", ddlReasonStage1.SelectedValue, string.Empty);
                lblHeaderStatusStage1.Text = ddlStatusStage1.SelectedItem.ToString();
                txtClosingDate.Text = DateTime.Now.ToShortDateString();
                if (!string.IsNullOrEmpty(Session["customerVo"].ToString()))
                    customerVo = (CustomerVo)Session["customerVo"];                         
                        
                txtCusName.Text = customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName;
                txtBranchCode.Text = ddlBMBranch.SelectedItem.Text;
                txtRBLCode.Text = ddlCustomerCategory.SelectedItem.ToString();
                txtRequestNumber.Text = txtGenerateReqstNum.Text;
                txtRequestdate.Text = DateTime.Now.ToShortDateString();
                txtlblISAGenerationStatus.Text = "In Process";
                string msg = "Your request has been sent to Ops. Your Request number is" + " " + txtGenerateReqstNum.Text ;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('" + msg + "');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CustomerSearch", " HideCustomerSearch();", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Priority", " enableDisablePriority('false');", true);
            }
        }

        protected void btnSubmitAddMoreStage1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                customerBo.UpdateCustomerISAStageDetails(int.Parse(txtGenerateReqstNum.Text), ddlStatusStage1.SelectedValue, ddlPriority.SelectedValue, "ISARQ",ddlReasonStage1.SelectedValue, string.Empty);
                ResetPageState();
                if (!string.IsNullOrEmpty(Session["customerVo"].ToString()))
                    customerVo = (CustomerVo)Session["customerVo"];                         
                
                txtCusName.Text = customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName;
                if (!string.IsNullOrEmpty(Session["customerVo"].ToString()))
                    Session.Remove("customerVo");

                lblHeaderStatusStage1.Text = ddlStatusStage1.SelectedItem.ToString();
                txtClosingDate.Text = DateTime.Now.ToString();
            }            
        }

        protected void ResetPageState()
          {
              ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ResetPageLoad", " ResetPagetoFirstLoadState();", true);
          }
        protected void BindReasonAndStatus()
        {
            DataSet ds = customerBo.GetReasonAndStatus("ISA");
            if (ds != null)
            {
                ddlStatusStage1.DataSource = ds.Tables[1];
                ddlStatusStage1.DataValueField = ds.Tables[1].Columns["XS_StatusCode"].ToString();
                ddlStatusStage1.DataTextField = ds.Tables[1].Columns["XS_Status"].ToString();
                ddlStatusStage1.DataBind();
                ddlStatusStage1.Items.Insert(0, new ListItem("Select", "Select"));

                ddlStatusStage2.DataSource = ds.Tables[1];
                ddlStatusStage2.DataValueField = ds.Tables[1].Columns["XS_StatusCode"].ToString();
                ddlStatusStage2.DataTextField = ds.Tables[1].Columns["XS_Status"].ToString();
                ddlStatusStage2.DataBind();
                ddlStatusStage2.Items.Insert(0, new ListItem("Select", "Select"));

                ddlStatusStage3.DataSource = ds.Tables[1];
                ddlStatusStage3.DataValueField = ds.Tables[1].Columns["XS_StatusCode"].ToString();
                ddlStatusStage3.DataTextField = ds.Tables[1].Columns["XS_Status"].ToString();
                ddlStatusStage3.DataBind();
                ddlStatusStage3.Items.Insert(0, new ListItem("Select", "Select"));

                ddlStatusStage4.DataSource = ds.Tables[1];
                ddlStatusStage4.DataValueField = ds.Tables[1].Columns["XS_StatusCode"].ToString();
                ddlStatusStage4.DataTextField = ds.Tables[1].Columns["XS_Status"].ToString();
                ddlStatusStage4.DataBind();
                ddlStatusStage4.Items.Insert(0, new ListItem("Select", "Select"));

                ddlReasonStage1.DataSource = ds.Tables[0];
                ddlReasonStage1.DataValueField = ds.Tables[0].Columns["XSR_StatusReasonCode"].ToString();
                ddlReasonStage1.DataTextField = ds.Tables[0].Columns["XSR_StatusReason"].ToString();
                ddlReasonStage1.DataBind();
                ddlReasonStage1.Items.Insert(0, new ListItem("Select", "Select"));

                ddlReasonStage2.DataSource = ds.Tables[0];
                ddlReasonStage2.DataValueField = ds.Tables[0].Columns["XSR_StatusReasonCode"].ToString();
                ddlReasonStage2.DataTextField = ds.Tables[0].Columns["XSR_StatusReason"].ToString();
                ddlReasonStage2.DataBind();
                ddlReasonStage2.Items.Insert(0, new ListItem("Select", "Select"));

                ddlStatusReason3.DataSource = ds.Tables[0];
                ddlStatusReason3.DataValueField = ds.Tables[0].Columns["XSR_StatusReasonCode"].ToString();
                ddlStatusReason3.DataTextField = ds.Tables[0].Columns["XSR_StatusReason"].ToString();
                ddlStatusReason3.DataBind();
                ddlStatusReason3.Items.Insert(0, new ListItem("Select", "Select"));

                ddlReasonStage4.DataSource = ds.Tables[0];
                ddlReasonStage4.DataValueField = ds.Tables[0].Columns["XSR_StatusReasonCode"].ToString();
                ddlReasonStage4.DataTextField = ds.Tables[0].Columns["XSR_StatusReason"].ToString();
                ddlReasonStage4.DataBind();
                ddlReasonStage4.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }

        protected void HideAndShowBasedOnRole(string userRole, string stepCode, string stageStatus)
        {
            //if (stepCode == "" && userRole == "BM")
            //{
            //    stepCode = "ISARQ";
            //}
            //if(stepCode == 
            DataSet dsISARequestDetails = new DataSet();
 
              if (!string.IsNullOrEmpty(Session["customerVo"].ToString()))
                    customerVo = (CustomerVo)Session["customerVo"];
              dsISARequestDetails = customerBo.GetISARequestDetails(customerVo.CustomerId);

            if (userRole == "BM" && stepCode == "" && stageStatus != "DO")
            {
                //lnkBtnViewFormsandProofs.Attributes["disabled"] = "disabled";
                //lnkBtnViewFormsandProofs.Attributes.Add("disabled", "disabled");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISARQNewEntry');", true);
            }
            else if (userRole == "BM" && stepCode == "ISARQ" && (stageStatus == "PD" || stageStatus == "RO" || stageStatus == "IP"))
            {
                //lnkBtnViewFormsandProofs.Attributes["disabled"] = "disabled";
                //lnkBtnViewFormsandProofs.Attributes.Add("disabled", "disabled");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISARQNewEntryDisable');", true);
            }
            else if (userRole == "BM" && stepCode == "ISARQ" && stageStatus=="DO")
            {
                //lnkBtnViewFormsandProofs.Attributes["disabled"] = "disabled";
                //lnkBtnViewFormsandProofs.Attributes.Add("disabled", "disabled");

                //Will open the Step 1 in view state
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISARQStatusDone');", true);
            }
            else if (userRole == "OPS" && stepCode == "CUSCR" && advisorVo.PermisionList.Contains("checker") && (stageStatus == "PD" || stageStatus == "RO" || stageStatus == "IP"))
            {
                //enable Step 2 and disable other Step
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('CUSCR');", true);
            }
            else if (userRole == "OPS" && stepCode == "CUSCR" && advisorVo.PermisionList.Contains("checker") && stageStatus == "DO")
            {
                //enable Step 3 and fill all the old steps data and disable other Step
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('VERIfY');", true);
            }
            else if (userRole == "OPS" && stepCode == "VERIfY" && advisorVo.PermisionList.Contains("checker") && (stageStatus == "PD" || stageStatus == "RO" || stageStatus == "IP"))
            {
                //enable Step 3 and disable other Step and fill old steps details

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('VERIfY');", true);
            }
            else if (userRole == "OPS" && stepCode == "VERIfY" && advisorVo.PermisionList.Contains("checker") && stageStatus == "DO")
            {
                //Disable Step 3 and disable other Step and fill old steps details
                if (advisorVo.PermisionList.Contains("maker"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISAGE');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('VERIfYDisable');", true);
                }
            }
            else if (userRole == "OPS" && stepCode == "ISAGE" && advisorVo.PermisionList.Contains("maker") && stageStatus == "DO")
            {
                //disable Step 4 and disable other Step also
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISAGEDisable');", true);
            }

            else if (userRole == "OPS" && stepCode == "ISAGE" && advisorVo.PermisionList.Contains("maker") && (stageStatus == "PD" || stageStatus == "RO" || stageStatus == "IP"))
            {
                //enable Step 4 and disable other Step
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISAGE');", true);
            }           

        }

        protected void btnSubmitStage2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                customerBo.UpdateCustomerISAStageDetails(int.Parse(txtGenerateReqstNum.Text), ddlStatusStage2.SelectedValue, string.Empty, "CUSCR", ddlReasonStage2.SelectedValue,txtComments.Text);
                txtStatus2.Text = ddlStatusStage2.SelectedItem.ToString();
                txtClosingDate2.Text = DateTime.Now.ToShortDateString();                        
            }
        }

        protected void btnSubmitStage3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                customerBo.UpdateCustomerISAStageDetails(int.Parse(txtGenerateReqstNum.Text), ddlStatusStage3.SelectedValue, string.Empty, "CUSCR", ddlStatusReason3.SelectedValue, txtAddComments3.Text);
                txtStatus3.Text = ddlStatusStage3.SelectedItem.ToString();
                txtClosingDate3.Text = DateTime.Now.ToShortDateString();
                        
            }
        }
        protected void btnSubmitStage4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                customerBo.UpdateCustomerISAStageDetails(int.Parse(txtGenerateReqstNum.Text), ddlStatusStage4.SelectedValue, string.Empty, "CUSCR", ddlReasonStage4.SelectedValue, txtAddcommentsStage4.Text);
                txtStatusStage4.Text = ddlStatusStage4.SelectedItem.ToString();
                txtClosingDateStage4.Text = DateTime.Now.ToShortDateString();

            }
        }    
        
    }
}