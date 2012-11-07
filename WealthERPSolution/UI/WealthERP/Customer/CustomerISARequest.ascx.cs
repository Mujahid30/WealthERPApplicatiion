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
        UserVo userVo = new UserVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        int customerId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();

            rmVo = (RMVo)Session["rmVo"];
            if (Session["advisorVo"] != null)
                advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["UserVo"];
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
                HideAndShowBasedOnRole(userType, stepCode, stageStatus,requestId);
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
                        if (Session["customerVo"] != null)
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
                customerBo.UpdateCustomerISAStageDetails(int.Parse(txtGenerateReqstNum.Text), ddlStatusStage1.SelectedValue, ddlPriority.SelectedValue, "ISARQ", ddlReasonStage1.SelectedValue,string.Empty,string.Empty);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISARQStatusDone');", true);
                if (ddlPriority.SelectedValue == "Normal")
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideVerification", " GetVerificationType('Normal');", true);
                else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowVerification", " GetVerificationType('Urgent');", true);
            }
        }

        protected void btnSubmitAddMoreStage1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                customerBo.UpdateCustomerISAStageDetails(int.Parse(txtGenerateReqstNum.Text), ddlStatusStage1.SelectedValue, ddlPriority.SelectedValue, "ISARQ", ddlReasonStage1.SelectedValue, string.Empty, string.Empty);
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

        protected void HideAndShowBasedOnRole(string userRole, string stepCode, string stageStatus, int requestId)
        {
            //if (stepCode == "" && userRole == "BM")
            //{
            //    stepCode = "ISARQ";
            //}
            //if(stepCode == 
            DataSet dsISARequestDetails = new DataSet();

            if (requestId!=0)
            {
                int customerId = 0;
                //lnkBtnViewFormsandProofs.Enabled = true;
                //lnkbtnAddEditCustomerProfile.Enabled = true;
                dsISARequestDetails = customerBo.GetISARequestDetails(requestId);
                if (dsISARequestDetails != null)
                {
                    if (dsISARequestDetails.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drISARequestDetails in dsISARequestDetails.Tables[0].Rows)
                        {

                            //---------**************Request Header Section**************--------------\\

                            if (drISARequestDetails["WWFSM_StepCode"].ToString() == "ISARQ")
                            {
                                if (!String.IsNullOrEmpty(drISARequestDetails["C_CustomerId"].ToString()))
                                {
                                    customerId = int.Parse(drISARequestDetails["C_CustomerId"].ToString());
                                    customerVo = customerBo.GetCustomer(customerId);
                                    Session["customerVo"] = customerVo;
                                }
                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQ_date"].ToString()))
                                    txtRequestdate.Text = DateTime.Parse(drISARequestDetails["AISAQ_date"].ToString()).ToShortDateString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["CustomerName"].ToString()))
                                    txtCusName.Text = drISARequestDetails["CustomerName"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQ_RequestQueueid"].ToString()))
                                    txtRequestNumber.Text = drISARequestDetails["AISAQ_RequestQueueid"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["CustomerName"].ToString()))
                                    txtBranchCode.Text = drISARequestDetails["CustomerName"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQ_StatusName"].ToString()))
                                    txtlblISAGenerationStatus.Text = drISARequestDetails["AISAQ_StatusName"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["ACC_CustomerCategoryCode"].ToString()))
                                    txtRBLCode.Text = drISARequestDetails["ACC_CustomerCategoryCode"].ToString();




                                //------------****************************STEP 1***********************------------------------------\\


                                if (!String.IsNullOrEmpty(drISARequestDetails["AB_BranchId"].ToString()))
                                    ddlBMBranch.SelectedValue = drISARequestDetails["AB_BranchId"].ToString();


                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_StatusName"].ToString()))
                                {
                                    lblHeaderStatusStage1.Text = drISARequestDetails["AISAQD_StatusName"].ToString();
                                }
                                if (!String.IsNullOrEmpty(drISARequestDetails["ACC_CustomerCategoryCodeId"].ToString()))
                                    ddlCustomerCategory.SelectedValue = drISARequestDetails["ACC_CustomerCategoryCodeId"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["CustomerName"].ToString()))
                                    txtCustomerNameEntry.Text = drISARequestDetails["CustomerName"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["C_PANNum"].ToString()))
                                    txtPanNum.Text = drISARequestDetails["C_PANNum"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQ_RequestQueueid"].ToString()))
                                    txtGenerateReqstNum.Text = drISARequestDetails["AISAQ_RequestQueueid"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQ_Priority"].ToString()))
                                {
                                    ddlPriority.SelectedValue = drISARequestDetails["AISAQ_Priority"].ToString();
                                    if(ddlPriority.SelectedValue =="Normal")
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "VerificationHide", " GetVerificationType('Normal');", true);
                                    else
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "VerificationHide", " GetVerificationType('Urgent');", true);
                                }

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_Status"].ToString()))
                                    ddlStatusStage1.SelectedValue = drISARequestDetails["AISAQD_Status"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_StatusReason"].ToString()))
                                    ddlReasonStage1.SelectedValue = drISARequestDetails["AISAQD_StatusReason"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_StatusDate"].ToString()))
                                    txtClosingDate.Text = DateTime.Parse(drISARequestDetails["AISAQD_StatusDate"].ToString()).ToShortDateString();

                            }

                            //------------****************************STEP 2***********************------------------------------\\

                            if (drISARequestDetails["WWFSM_StepCode"].ToString() == "CUSCR" && drISARequestDetails["AISAQD_Status"].ToString() != null)
                            {
                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_Status"].ToString()))
                                    ddlStatusStage2.SelectedValue = drISARequestDetails["AISAQD_Status"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_StatusReason"].ToString()))
                                    ddlReasonStage2.SelectedValue = drISARequestDetails["AISAQD_StatusReason"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_StatusName"].ToString()))                                
                                    txtStatus2.Text = drISARequestDetails["AISAQD_StatusName"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_Notes"].ToString()))
                                    txtComments.Text = drISARequestDetails["AISAQD_Notes"].ToString();
                                
                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_StatusDate"].ToString()))
                                    txtClosingDate2.Text = DateTime.Parse(drISARequestDetails["AISAQD_StatusDate"].ToString()).ToShortDateString();

                            }

                            //------------****************************STEP 3***********************------------------------------\\

                            if (drISARequestDetails["WWFSM_StepCode"].ToString() == "VERIfY" && drISARequestDetails["AISAQD_Status"].ToString() != null)
                            {
                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_Status"].ToString()))
                                    ddlStatusStage3.SelectedValue = drISARequestDetails["AISAQD_Status"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_StatusReason"].ToString()))
                                    ddlStatusReason3.SelectedValue = drISARequestDetails["AISAQD_StatusReason"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_StatusName"].ToString()))
                                    txtStatus3.Text = drISARequestDetails["AISAQD_StatusName"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_Notes"].ToString()))
                                    txtAddComments3.Text = drISARequestDetails["AISAQD_Notes"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_StatusDate"].ToString()))
                                    txtClosingDate3.Text = DateTime.Parse(drISARequestDetails["AISAQD_StatusDate"].ToString()).ToShortDateString();
                            }

                            //------------****************************STEP 4***********************------------------------------\\

                            if (drISARequestDetails["WWFSM_StepCode"].ToString() == "ISAGE" && drISARequestDetails["AISAQD_Status"].ToString() != null)
                            {
                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_Status"].ToString()))
                                    ddlStatusStage4.SelectedValue = drISARequestDetails["AISAQD_Status"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_StatusReason"].ToString()))
                                    ddlReasonStage4.SelectedValue = drISARequestDetails["AISAQD_StatusReason"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_StatusName"].ToString()))
                                    txtStatusStage4.Text = drISARequestDetails["AISAQD_StatusName"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_Notes"].ToString()))
                                    txtAddcommentsStage4.Text = drISARequestDetails["AISAQD_Notes"].ToString();


                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_StatusDate"].ToString()))
                                    txtClosingDateStage4.Text = DateTime.Parse(drISARequestDetails["AISAQD_StatusDate"].ToString()).ToShortDateString();
                            }

                        }
                    }

                }
            }
            //if (userRole == "BM" && stepCode == "" && stageStatus != "DO")
            //{
            if (userRole == "BM" && requestId == 0)
            {
            
                    Session.Remove("customerVo");
                    //lnkBtnViewFormsandProofs.Enabled = false;
                //lnkbtnAddEditCustomerProfile.Enabled = false;
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
            else if (userRole == "Ops" && stepCode == "ISARQ" && stageStatus == "DO")
            {
                //lnkBtnViewFormsandProofs.Attributes["disabled"] = "disabled";
                //lnkBtnViewFormsandProofs.Attributes.Add("disabled", "disabled");

                //Will open the Step 1 in view state
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('CUSCR');", true);
            }
            else if (userRole == "Ops" && stepCode == "CUSCR" && userVo.PermisionList.Contains("Maker") && (stageStatus == "PD" || stageStatus == "RO" || stageStatus == "IP"))
            {
                //enable Step 2 and disable other Step
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('CUSCR');", true);
            }
            else if (userRole == "Ops" && stepCode == "CUSCR" && userVo.PermisionList.Contains("Maker") && stageStatus == "DO")
            {
                //enable Step 3 and fill all the old steps data and disable other Step
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('VERIfY');", true);
            }
            else if (userRole == "Ops" && stepCode == "VERIfY" && userVo.PermisionList.Contains("Maker") && (stageStatus == "PD" || stageStatus == "RO" || stageStatus == "IP"))
            {
                //enable Step 3 and disable other Step and fill old steps details

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('VERIfY');", true);
            }
            else if (userRole == "Ops" && stepCode == "VERIfY" && userVo.PermisionList.Contains("Checker") && stageStatus == "DO")
            {
                //Disable Step 3 and disable other Step and fill old steps details
                if (advisorVo.PermisionList.Contains("Maker"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISAGE');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('VERIfYDisable');", true);
                }
            }
            else if (userRole == "Ops" && stepCode == "ISAGE" && userVo.PermisionList.Contains("Checker") && stageStatus == "DO")
            {
                //disable Step 4 and disable other Step also
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISAGEDisable');", true);
            }

            else if (userRole == "Ops" && stepCode == "ISAGE" && userVo.PermisionList.Contains("Checker") && (stageStatus == "PD" || stageStatus == "RO" || stageStatus == "IP"))
            {
                //enable Step 4 and disable other Step
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISAGE');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISARQStatusDone');", true);
            }

        }

        protected void btnSubmitStage2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                string stageToMarkReprocess = ddlBackToStepStage2.SelectedValue;
                customerBo.UpdateCustomerISAStageDetails(int.Parse(txtGenerateReqstNum.Text), ddlStatusStage2.SelectedValue, string.Empty, "CUSCR", ddlReasonStage2.SelectedValue, txtComments.Text, stageToMarkReprocess);
                txtStatus2.Text = ddlStatusStage2.SelectedItem.ToString();
                txtClosingDate2.Text = DateTime.Now.ToShortDateString();
                HideAndShowBasedOnRole("Ops", "CUSCR", ddlStatusStage2.SelectedValue, int.Parse(txtGenerateReqstNum.Text));

                if (ddlStatusStage2.SelectedValue == "DO")
                {
                    string msg = "Your request is in next stage";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('" + msg + "');", true);
                }
            }
        }

        protected void btnSubmitStage3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                string stageToMarkReprocess = ddlBackToStepStage3.SelectedValue;
                customerBo.UpdateCustomerISAStageDetails(int.Parse(txtGenerateReqstNum.Text), ddlStatusStage3.SelectedValue, string.Empty, "VERIfY", ddlStatusReason3.SelectedValue, txtAddComments3.Text, stageToMarkReprocess);
                txtStatus3.Text = ddlStatusStage3.SelectedItem.ToString();
                txtClosingDate3.Text = DateTime.Now.ToShortDateString();
                HideAndShowBasedOnRole("Ops", "VERIfY", ddlStatusStage3.SelectedValue, int.Parse(txtGenerateReqstNum.Text));

                if (ddlStatusStage3.SelectedValue == "DO")
                {
                    string msg = "Your request is in next stage";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('" + msg + "');", true);
                }
                        
            }
        }
        protected void btnSubmitStage4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                string stageToMarkReprocess = ddlBackToStepStage4.SelectedValue;
                customerBo.UpdateCustomerISAStageDetails(int.Parse(txtGenerateReqstNum.Text), ddlStatusStage4.SelectedValue, string.Empty, "ISAGE", ddlReasonStage4.SelectedValue, txtAddcommentsStage4.Text, stageToMarkReprocess);
                txtStatusStage4.Text = ddlStatusStage4.SelectedItem.ToString();
                txtClosingDateStage4.Text = DateTime.Now.ToShortDateString();
                HideAndShowBasedOnRole("Ops", "ISAGE", ddlStatusStage4.SelectedValue, int.Parse(txtGenerateReqstNum.Text));
            }
        }    
        
    }
}