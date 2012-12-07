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
using BoUploads;

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
        string currentUserRole = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();

            userVo = (UserVo)Session["UserVo"];
            rmVo = (RMVo)Session["rmVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            hdnAdviserID.Value = advisorVo.advisorId.ToString();

            if (!String.IsNullOrEmpty(Session[SessionContents.CurrentUserRole].ToString()))
            {
                currentUserRole = Session[SessionContents.CurrentUserRole].ToString().ToLower();
            }


            if (!IsPostBack)
            {
                int requestId = 0;
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
                BindReasonAndStatus();
                BindBranchDropDown(currentUserRole);
                if (requestId != 0)
                {

                    //txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    //txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                    HideAndShowBasedOnRole(requestId);
                    trExistingCustomer.Visible = false;
                    //if (currentUserRole == "bm")
                    //{
                    //    //EnableCurrentStep(0);
                    //    EnableControlBasedOnUserRole();

                    //}
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CustomerSearch1retyui", "DisplayCustomerSearch('NEW');", true);
                    ResetControlsToInitialState();
                }

            }
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mykey", "SectionCollapseExpand();", true);
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

                if (customerVo.Mobile1.ToString() != "0" && customerVo.Mobile1.ToString() != "")
                    txtMobileNum.Text = customerVo.Mobile1.ToString();
                else
                {
                    txtMobileNum.Text = string.Empty;
                }
                txtEmailID.Text = customerVo.Email;

                //trNewCustomer.Visible = false;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Existing Customer Control State", " DisplayCustomerSearch('EXISTING');", true);
            }

            //MarkAllFieldEnableFalse();           

            //EnableCurrentStep(0);

            //SetStepStatusImage(100);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "StepOne event Fire", " StepEventFireCollapseExpand('one');", true);

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CustomerSearch", " HideCustomerSearch();", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Verification", " GetVerificationType('Normal');", true);
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
                customerVo.CustomerCategoryCode = Convert.ToInt16(ddlCustomerCategory.SelectedValue);
                customerIds = customerBo.CreateISACustomerRequest(customerVo, custCreateFlag,ddlPriority.SelectedValue);
                txtGenerateReqstNum.Text = customerIds[3].ToString();
                txtCustomerName.Text = customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName;
                txtCustomerName.Enabled = false;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Priority", " enableDisablePriority('false');", true);
            }
            else if (rdbNewCustomer.Checked == true && hidValidCheck.Value != "0")
            {
                custCreateFlag = 1;
                Int32 mobileNum = 0;
                Int32.TryParse(txtMobileNum.Text, out mobileNum);
                newCustomerVo.PANNum = txtPanNum.Text;
                newCustomerVo.FirstName = txtCustomerNameEntry.Text;
                if (!string.IsNullOrEmpty(txtMobileNum.Text.Trim()))
                    newCustomerVo.Mobile1 = Convert.ToInt64(txtMobileNum.Text.Trim());
                newCustomerVo.Email = txtEmailID.Text;
                newCustomerVo.RmId = rmVo.RMId;
                newCustomerVo.UserId = userVo.UserId;
                newCustomerVo.ProfilingDate = DateTime.Now;
                newCustomerVo.BranchId = int.Parse(ddlBMBranch.SelectedValue);
                newCustomerVo.CustomerCategoryCode =Convert.ToInt16(ddlCustomerCategory.SelectedValue);

                //newCustomerVo.BranchId = rmVo.BranchList
                customerIds = customerBo.CreateISACustomerRequest(newCustomerVo, custCreateFlag, ddlPriority.SelectedValue);
                txtGenerateReqstNum.Text = customerIds[3].ToString();
                customerVo = customerBo.GetCustomer(customerIds[1]);
                Session["customerVo"] = customerVo;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Priority", " enableDisablePriority('false');", true);

            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Pan Number Already Exists');", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Priority", " enableDisablePriority('true');", true);
            }
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CustomerSearch", " HideCustomerSearch();", true);
            if (customerIds.Count > 0)
            {
                HideAndShowBasedOnRole(customerIds[3]);
            }

            divSuccessMsg.Visible = true;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "StepOne event Fire", " StepEventFireCollapseExpand('one');", true);
        }

        private void BindBranchDropDown(string currentUserRole)
        {
            UploadCommonBo uploadCommonBo = new UploadCommonBo();
            DataSet dsBranchList = new DataSet();

            if (currentUserRole == "ops" || currentUserRole == "admin")
            {
                
                dsBranchList = uploadCommonBo.GetAdviserBranchList(advisorVo.advisorId, currentUserRole);
                ddlBMBranch.DataSource = dsBranchList.Tables[0];
                ddlBMBranch.DataTextField = "AB_BranchName";
                ddlBMBranch.DataValueField = "AB_BranchId";
                ddlBMBranch.DataBind();

            }
            else if (currentUserRole == "bm")
            {
                dsBranchList = advisorBranchBo.GetBranchsRMForBMDp(0, rmVo.RMId, 0);
                ddlBMBranch.DataSource = dsBranchList.Tables[1]; ;
                ddlBMBranch.DataTextField = "AB_BranchName";
                ddlBMBranch.DataValueField = "AB_BranchId";
                ddlBMBranch.DataBind();
            }

            DataTable dtCustomerCategory = new DataTable();
            dtCustomerCategory.Columns.Add("Category_Name");            
            dtCustomerCategory.Columns.Add("Category_Id");

            DataRow drCustomerCategory;

            drCustomerCategory = dtCustomerCategory.NewRow();
            drCustomerCategory["Category_Name"] = advisorVo.OrganizationName;
            drCustomerCategory["Category_Id"] = 1;

            dtCustomerCategory.Rows.Add(drCustomerCategory);

            drCustomerCategory = dtCustomerCategory.NewRow();
            drCustomerCategory["Category_Name"]="Non" + " " + advisorVo.OrganizationName;
            drCustomerCategory["Category_Id"]= 0;

            dtCustomerCategory.Rows.Add(drCustomerCategory);

     


            ddlCustomerCategory.DataSource = dtCustomerCategory;
            ddlCustomerCategory.DataTextField = "Category_Name";
            ddlCustomerCategory.DataValueField = "Category_Id";
            ddlCustomerCategory.DataBind();




        }

        protected void btnSubmitAddStage1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                customerBo.UpdateCustomerISAStageDetails(int.Parse(txtGenerateReqstNum.Text), ddlStatusStage1.SelectedValue, ddlPriority.SelectedValue, "ISARQ", ddlReasonStage1.SelectedValue, string.Empty, string.Empty);

                HideAndShowBasedOnRole(Convert.ToInt32(txtRequestNumber.Text));
                EnableControlBasedOnUserRole();
            }
        }

        protected void btnSubmitAddMoreStage1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                customerBo.UpdateCustomerISAStageDetails(int.Parse(txtGenerateReqstNum.Text), ddlStatusStage1.SelectedValue, ddlPriority.SelectedValue, "ISARQ", ddlReasonStage1.SelectedValue, string.Empty, string.Empty);
                ResetControlsToInitialState();
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

        protected void HideAndShowBasedOnRole(int requestId)
        {
            DataSet dsISARequestDetails = new DataSet();
            string stepCode = string.Empty;
            string stageStatus = string.Empty;
            int currentStep = 0;

            if (requestId != 0)
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
                            //------**************SET THE STEP STATUS IMAGE BASED ON ACTIVE STEP*********---------------//

                            if (Convert.ToBoolean(drISARequestDetails["AISAQD_IsActiveStep"]) == true)
                            {
                                currentStep = Convert.ToInt16(drISARequestDetails["WWFSM_StepOrder"].ToString());
                                stepCode = drISARequestDetails["WWFSM_StepCode"].ToString();
                                stageStatus = drISARequestDetails["AISAQD_Status"].ToString();

                            }


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
                                {
                                    txtRequestdate.Text = DateTime.Parse(drISARequestDetails["AISAQ_date"].ToString()).ToShortDateString();
                                    txtRequestTimeValue.Text = DateTime.Parse(drISARequestDetails["AISAQ_date"].ToString()).TimeOfDay.ToString();
                                }



                                if (!String.IsNullOrEmpty(drISARequestDetails["CustomerName"].ToString()))
                                    txtCusName.Text = drISARequestDetails["CustomerName"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQ_RequestQueueid"].ToString()))
                                    txtRequestNumber.Text = drISARequestDetails["AISAQ_RequestQueueid"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AB_BranchName"].ToString()))
                                    txtBranchCode.Text = drISARequestDetails["AB_BranchName"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQ_StatusName"].ToString()))
                                    txtlblISAGenerationStatus.Text = drISARequestDetails["AISAQ_StatusName"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["ACC_CustomerCategoryCode"].ToString()))
                                    txtRBLCode.Text = drISARequestDetails["ACC_CustomerCategoryCode"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_CreatedBy"].ToString()))
                                    txtRequestedByValue.Text = drISARequestDetails["AISAQD_CreatedBy"].ToString();



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

                                if (!String.IsNullOrEmpty(drISARequestDetails["C_Email"].ToString()))
                                    txtEmailID.Text = drISARequestDetails["C_Email"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["C_Mobile1"].ToString()))
                                    txtMobileNum.Text = drISARequestDetails["C_Mobile1"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQ_RequestQueueid"].ToString()))
                                    txtGenerateReqstNum.Text = drISARequestDetails["AISAQ_RequestQueueid"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQ_Priority"].ToString()))
                                {
                                    ddlPriority.SelectedValue = drISARequestDetails["AISAQ_Priority"].ToString();
                                    //if (ddlPriority.SelectedValue == "Normal")
                                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "VerificationHide", " GetVerificationType('Normal');", true);
                                    //else
                                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "VerificationHide", " GetVerificationType('Urgent');", true);
                                }

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_Status"].ToString()))
                                    ddlStatusStage1.SelectedValue = drISARequestDetails["AISAQD_Status"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_StatusReason"].ToString()))
                                    ddlReasonStage1.SelectedValue = drISARequestDetails["AISAQD_StatusReason"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AISAQD_StatusDate"].ToString()))
                                    txtClosingDate.Text = DateTime.Parse(drISARequestDetails["AISAQD_StatusDate"].ToString()).ToShortDateString();

                                ShowHideReasonDropListBasedOnStatus(drISARequestDetails["AISAQD_Status"].ToString(), 1);

                            }

                            //------------****************************STEP 2***********************------------------------------\\

                            if (drISARequestDetails["WWFSM_StepCode"].ToString() == "VERIfY" && drISARequestDetails["AISAQD_Status"].ToString() != null)
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

                                ShowHideReasonDropListBasedOnStatus(drISARequestDetails["AISAQD_Status"].ToString(), 2);

                            }

                            //------------****************************STEP 3***********************------------------------------\\

                            if (drISARequestDetails["WWFSM_StepCode"].ToString() == "CUSCR" && drISARequestDetails["AISAQD_Status"].ToString() != null)
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

                                ShowHideReasonDropListBasedOnStatus(drISARequestDetails["AISAQD_Status"].ToString(), 3);
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

                                ShowHideReasonDropListBasedOnStatus(drISARequestDetails["AISAQD_Status"].ToString(), 4);
                            }

                        }
                    }

                }
            }

            MarkAllFieldEnableFalse();
            if (currentStep == 4 && txtlblISAGenerationStatus.Text.Trim().ToLower() == "isa generated")
            {
                txtlblISAGenerationStatus.Style["color"] = "green";
                txtlblISAGenerationStatus.Style["font-weight"] = "bold";
                SetStepStatusImage(0);
                EnableCurrentStep(100); //False Case Make all enable false in case of completed ISA Request
            }
            else
            {
                SetStepStatusImage(currentStep);
                EnableCurrentStep(currentStep);
            }
            EnableControlBasedOnUserRole();




            //if (userRole == "BM" && stepCode == "" && stageStatus != "DO")
            //{

            /*
            if (currentUserRole == "BM" && requestId == 0)
            {

                Session.Remove("customerVo");
                //lnkBtnViewFormsandProofs.Enabled = false;
                //lnkbtnAddEditCustomerProfile.Enabled = false;
                //lnkBtnViewFormsandProofs.Attributes["disabled"] = "disabled";
                //lnkBtnViewFormsandProofs.Attributes.Add("disabled", "disabled");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISARQNewEntry');", true);
            }
            else if (currentUserRole == "BM" && stepCode == "ISARQ" && (stageStatus == "PD" || stageStatus == "RO" || stageStatus == "IP"))
            {
                //lnkBtnViewFormsandProofs.Attributes["disabled"] = "disabled";
                //lnkBtnViewFormsandProofs.Attributes.Add("disabled", "disabled");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISARQNewEntryDisable');", true);
            }
            else if (currentUserRole == "BM" && stepCode == "ISARQ" && stageStatus == "DO")
            {
                //lnkBtnViewFormsandProofs.Attributes["disabled"] = "disabled";
                //lnkBtnViewFormsandProofs.Attributes.Add("disabled", "disabled");

                //Will open the Step 1 in view state
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISARQStatusDone');", true);
            }
            else if (currentUserRole == "Ops" && stepCode == "ISARQ" && stageStatus == "DO")
            {
                //lnkBtnViewFormsandProofs.Attributes["disabled"] = "disabled";
                //lnkBtnViewFormsandProofs.Attributes.Add("disabled", "disabled");

                //Will open the Step 1 in view state
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('CUSCR');", true);
            }
            else if (currentUserRole == "Ops" && stepCode == "CUSCR" && userVo.PermisionList.Contains("Maker") && (stageStatus == "PD" || stageStatus == "RO" || stageStatus == "IP"))
            {
                //enable Step 2 and disable other Step
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('CUSCR');", true);
            }
            else if (currentUserRole == "Ops" && stepCode == "CUSCR" && userVo.PermisionList.Contains("Maker") && stageStatus == "DO")
            {
                //enable Step 3 and fill all the old steps data and disable other Step
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('VERIfY');", true);
            }
            else if (currentUserRole == "Ops" && stepCode == "VERIfY" && userVo.PermisionList.Contains("Maker") && (stageStatus == "PD" || stageStatus == "RO" || stageStatus == "IP"))
            {
                //enable Step 3 and disable other Step and fill old steps details

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('VERIfY');", true);
            }
            else if (currentUserRole == "Ops" && stepCode == "VERIfY" && userVo.PermisionList.Contains("Checker") && stageStatus == "DO")
            {
                //Disable Step 3 and disable other Step and fill old steps details
                if (userVo.PermisionList.Contains("Maker"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISAGE');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('VERIfYDisable');", true);
                }
            }
            else if (currentUserRole == "Ops" && stepCode == "ISAGE" && userVo.PermisionList.Contains("Checker") && stageStatus == "DO")
            {
                //disable Step 4 and disable other Step also
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISAGEDisable');", true);
            }

            else if (currentUserRole == "Ops" && stepCode == "ISAGE" && userVo.PermisionList.Contains("Checker") && (stageStatus == "PD" || stageStatus == "RO" || stageStatus == "IP"))
            {
                //enable Step 4 and disable other Step
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISAGE');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideAndShowBasedOnRole", " HideAndShowBasedOnRole('ISARQStatusDone');", true);
            }
             * 
             */




        }

        protected void btnSubmitStage2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                string stageToMarkReprocess = ddlBackToStepStage2.SelectedValue;
                customerBo.UpdateCustomerISAStageDetails(int.Parse(txtGenerateReqstNum.Text), ddlStatusStage2.SelectedValue, string.Empty, "VERIfY", ddlReasonStage2.SelectedValue, txtComments.Text, stageToMarkReprocess);
                //txtStatus2.Text = ddlStatusStage2.SelectedItem.ToString();
                //txtClosingDate2.Text = DateTime.Now.ToShortDateString();
                //HideAndShowBasedOnRole("Ops", "CUSCR", ddlStatusStage2.SelectedValue, int.Parse(txtGenerateReqstNum.Text));
                HideAndShowBasedOnRole(int.Parse(txtGenerateReqstNum.Text));

                //if (ddlStatusStage2.SelectedValue == "DO")
                //{
                //    string msg = "Your request is in next stage";
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('" + msg + "');", true);
                //}
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "StepTwo event Fire", " StepEventFireCollapseExpand('two');", true);
            }
        }

        protected void btnSubmitStage3_Click(object sender, EventArgs e)
        {
           
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                string stageToMarkReprocess = ddlBackToStepStage3.SelectedValue;
                if ((customerBo.CheckIfISAAccountGenerated(txtGenerateReqstNum.Text) && ddlStatusStage3.SelectedValue == "DO") || ddlStatusStage3.SelectedValue != "DO")
                {

                    customerBo.UpdateCustomerISAStageDetails(int.Parse(txtGenerateReqstNum.Text), ddlStatusStage3.SelectedValue, string.Empty, "CUSCR", ddlStatusReason3.SelectedValue, txtAddComments3.Text, stageToMarkReprocess);
                    //txtStatus3.Text = ddlStatusStage3.SelectedItem.ToString();
                    //txtClosingDate3.Text = DateTime.Now.ToShortDateString();
                    //HideAndShowBasedOnRole("Ops", "VERIfY", ddlStatusStage3.SelectedValue, int.Parse(txtGenerateReqstNum.Text));
                    lnkbtnViewCustomerProfile.Visible = true;
                }
                else
                {
                    string msg = "Please generate ISA Account";
                      ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('" + msg + "');", true);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Please generate ISA Account');", true);
                    
                }
                HideAndShowBasedOnRole(int.Parse(txtGenerateReqstNum.Text));

                //if (ddlStatusStage3.SelectedValue == "DO")
                //{
                //    string msg = "Your request is in next stage";
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('" + msg + "');", true);
                //}
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "StepThree event Fire", " StepEventFireCollapseExpand('three');", true);

            }
        }

        protected void btnSubmitStage4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                string stageToMarkReprocess = ddlBackToStepStage4.SelectedValue;
                customerBo.UpdateCustomerISAStageDetails(int.Parse(txtGenerateReqstNum.Text), ddlStatusStage4.SelectedValue, string.Empty, "ISAGE", ddlReasonStage4.SelectedValue, txtAddcommentsStage4.Text, stageToMarkReprocess);
                //txtStatusStage4.Text = ddlStatusStage4.SelectedItem.ToString();
                //txtClosingDateStage4.Text = DateTime.Now.ToShortDateString();
                //HideAndShowBasedOnRole("Ops", "ISAGE", ddlStatusStage4.SelectedValue, int.Parse(txtGenerateReqstNum.Text));
                HideAndShowBasedOnRole(int.Parse(txtGenerateReqstNum.Text));
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "StepFour event Fire", " StepEventFireCollapseExpand('four');", true);
            }
        }

        protected void SetStepStatusImage(int currentStepId)
        {
            switch (currentStepId)
            {
                case 0:
                    imgStepOneStatus.ImageUrl = "~/Images/StepCompleted.png";
                    imgStepTwoStatus.ImageUrl = "~/Images/StepCompleted.png";
                    imgStepThreeStatus.ImageUrl = "~/Images/StepCompleted.png";
                    imgStepFourStatus.ImageUrl = "~/Images/StepCompleted.png";
                    break;
                case 1:
                    imgStepOneStatus.ImageUrl = "~/Images/CurrentStep.png";
                    imgStepTwoStatus.ImageUrl = "~/Images/StepPending.png";
                    imgStepThreeStatus.ImageUrl = "~/Images/StepPending.png";
                    imgStepFourStatus.ImageUrl = "~/Images/StepPending.png";
                    break;
                case 2:
                    imgStepOneStatus.ImageUrl = "~/Images/StepCompleted.png";
                    imgStepTwoStatus.ImageUrl = "~/Images/CurrentStep.png";
                    imgStepThreeStatus.ImageUrl = "~/Images/StepPending.png";
                    imgStepFourStatus.ImageUrl = "~/Images/StepPending.png";
                    break;
                case 3:
                    imgStepOneStatus.ImageUrl = "~/Images/StepCompleted.png";
                    imgStepTwoStatus.ImageUrl = "~/Images/StepCompleted.png";
                    imgStepThreeStatus.ImageUrl = "~/Images/CurrentStep.png";
                    imgStepFourStatus.ImageUrl = "~/Images/StepPending.png";
                    break;
                case 4:
                    imgStepOneStatus.ImageUrl = "~/Images/StepCompleted.png";
                    imgStepTwoStatus.ImageUrl = "~/Images/StepCompleted.png";
                    imgStepThreeStatus.ImageUrl = "~/Images/StepCompleted.png";
                    imgStepFourStatus.ImageUrl = "~/Images/CurrentStep.png";
                    break;
                case 5:
                    Console.WriteLine(5);
                    break;
            }

        }

        protected void MarkAllFieldEnableFalse()
        {

            //**********------------------STEP ONE-------------*****************//

            tdCustomerSelection1.Visible = false;
            tdCustomerSelection2.Visible = false;
            //trExistingCustomer.Visible = false;
            tdUploadSection.Visible = false;
            trFormUpload.Visible = false;
           


            ddlCustomerCategory.Enabled = false;
            txtCustomerNameEntry.Enabled = false;

            ddlBMBranch.Enabled = false;
            txtCusName.Enabled = false;
            txtPanNum.Enabled = false;
            txtEmailID.Enabled = false;
            txtMobileNum.Enabled = false;
            txtGenerateReqstNum.Enabled = false;



            ddlPriority.Enabled = false;
            divSuccessMsg.Visible = false;
            //lbtnUploadISAForm.Enabled = false;
            //lbtnUploadPanProof.Enabled = false;
            //lbtnUploadAddressProof.Enabled = false;



            //trLinkToUpload.Visible = false;

            ddlStatusStage1.Enabled = false;
            ddlReasonStage1.Enabled = false;

            btnSubmitAddStage1.Visible = false;
            btnSubmitAddMoreStage1.Visible = false;
            btnGenerateReqstNum.Visible = false;
            lnkViewFormsAndProofBM.Visible = false;

            //**********------------------STEP TWO-OPS MAKER-------------*****************//
            ddlStatusStage2.Enabled = false;
            ddlReasonStage2.Enabled = false;
            ddlBackToStepStage2.Enabled = false;
            txtComments.Enabled = false;

            lnkViewFormsAndProofOPS.Visible = false;
            btnSubmitStage2.Visible = false;


            //**********------------------STEP TWO-OPS CHECKER-------------*****************//

            ddlStatusStage3.Enabled = false;
            ddlStatusReason3.Enabled = false;
            ddlBackToStepStage3.Enabled = false;
            txtAddComments3.Enabled = false;

            btnSubmitStage3.Visible = false;
            lnkbtnAddEditCustomerProfile.Visible = false;

            //**********------------------STEP TWO-OPS CHECKER-------------*****************//
            ddlStatusStage4.Enabled = false;
            ddlReasonStage4.Enabled = false;
            ddlBackToStepStage4.Enabled = false;
            txtAddcommentsStage4.Enabled = false;

            btnSubmitStage4.Visible = false;

            //**********------------------STEP FIVE--------------*****************//
            trStepFiveHeading.Visible = false;
            trStepFiveContent.Visible = false;


        }

        protected void EnableCurrentStep(int currentStepId)
        {

            if (currentUserRole == "bm")
            {
                trStepTwoHeading.Visible = false;
                trStepTwoContent.Visible = false;

                trStepThreeHeading.Visible = false;
                trStepThreeContent.Visible = false;

                trStepFourHeading.Visible = false;
                trStepFourContent.Visible = false;

                trStepFiveHeading.Visible = false;
                trStepFiveContent.Visible = false;
            }

            switch (currentStepId)
            {
                case 0:
                    if (userVo.RoleList.Contains("BM"))
                    {
                        trExistingCustomer.Visible = false;
                        trNewCustomer.Visible = true;
                        txtCustomerNameEntry.Enabled = true;
                        txtEmailID.Enabled = true;
                        txtMobileNum.Enabled = true;
                        txtPanNum.Enabled = true;
                        btnGenerateReqstNum.Visible = true;
                        ddlPriority.Enabled = true;

                    
                        lnkbtnViewCustomerProfile.Visible = false;
                    }

                    break;
                case 1:

                    if (userVo.RoleList.Contains("BM"))
                    {
                        ddlStatusStage1.Enabled = true;
                        ddlReasonStage1.Enabled = true;
                        //ddlPriority.Enabled = true;
                        btnSubmitAddStage1.Visible = true;
                        btnSubmitAddMoreStage1.Visible = true;

                        tdUploadSection.Visible = true;    
                        lnkbtnViewCustomerProfile.Visible = false;
                    }

                    break;
                case 2:

                    if (userVo.RoleList.Contains("Ops") && userVo.PermisionList.Contains("Maker"))
                    {
                        ddlStatusStage2.Enabled = true;
                        ddlReasonStage2.Enabled = true;
                        ddlBackToStepStage2.Enabled = true;
                        txtComments.Enabled = true;
                        btnSubmitStage2.Visible = true;
                        lnkbtnViewCustomerProfile.Visible = false;

                    }
                    break;
                case 3:

                    if (userVo.RoleList.Contains("Ops") && userVo.PermisionList.Contains("Maker"))
                    {
                        ddlStatusStage3.Enabled = true;
                        ddlStatusReason3.Enabled = true;
                        ddlBackToStepStage3.Enabled = true;
                        txtAddComments3.Enabled = true;

                        lnkbtnAddEditCustomerProfile.Visible = true;
                        btnSubmitStage3.Visible = true;

                        lnkbtnViewCustomerProfile.Visible = false;

                    }
                    break;
                case 4:
                    if (userVo.RoleList.Contains("Ops") && userVo.PermisionList.Contains("Checker"))
                    {
                        ddlStatusStage4.Enabled = true;
                        ddlReasonStage4.Enabled = true;
                        ddlBackToStepStage4.Enabled = true;
                        txtAddcommentsStage4.Enabled = true;

                        btnSubmitStage4.Visible = true;
                        lnkbtnAddEditCustomerProfile.Visible = false;

                        lnkbtnViewCustomerProfile.Visible = true;
                    }
                    break;
                case 5:
                    if (userVo.RoleList.Contains("Ops"))
                    {

                    }
                    break;
            }

        }

        protected void ddlPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPriority.SelectedValue == "Urgent")
            {
                tdUploadSection.Visible = true;

                lbtnUploadISAForm.Enabled = true;
                lbtnUploadPanProof.Enabled = true;
                lbtnUploadAddressProof.Enabled = true;
            }
            else
            {
                tdUploadSection.Visible = false;

            }

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "StepOne event Fire", " StepEventFireCollapseExpand('one');", true);

        }

        protected void EnableControlBasedOnUserRole()
        {
            switch (currentUserRole)
            {
                case "bm":
                    if (ddlPriority.SelectedValue.ToLower() == "urgent")
                    {
                        trFormUpload.Visible = true;

                        if (ddlCustomerCategory.SelectedValue == "0")
                        {
                            lbtnUploadAddressProof.Visible = true;
                            lbtnUploadPanProof.Visible = true;
                        }
                        else
                        {
                            lbtnUploadAddressProof.Visible = false;
                            lbtnUploadPanProof.Visible = false;
                        }
                        if (ddlStatusStage1.SelectedIndex == 0)
                        {                           
                            tdUploadSection.Visible = true;
                        }
                         lnkViewFormsAndProofBM.Visible = true;
                                               
                    }

                    break;
                case "ops":
                    if (ddlPriority.SelectedValue.ToLower() == "urgent")
                    {
                        lnkViewFormsAndProofOPS.Visible = true;
                    }
                    break;
            }

        }

        protected void rdbNewCustomer_CheckedChanged(object sender, EventArgs e)
        {
            trNewCustomer.Visible = true;
            trExistingCustomer.Visible = false;
            txtCustomerNameEntry.Enabled = true;
            txtEmailID.Enabled = true;
            txtMobileNum.Enabled = true;
            txtPanNum.Enabled = true;
            txtCustomerNameEntry.Text = string.Empty;
            txtPanNum.Text = string.Empty;
            txtEmailID.Text = string.Empty;
            txtMobileNum.Text = string.Empty;
            ddlBMBranch.SelectedIndex = 0;
            
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "StepOne event Fire", " StepEventFireCollapseExpand('one');", true);

        }

        protected void rdbExistingCustomer_CheckedChanged(object sender, EventArgs e)
        {
            trNewCustomer.Visible = false;
            trExistingCustomer.Visible = true;

            txtCustomerNameEntry.Enabled = false;
            txtEmailID.Enabled = false;
            txtMobileNum.Enabled = false;
            txtPanNum.Enabled = false;
            txtCustomerName.Text = string.Empty;
            txtPanNum.Text = string.Empty;
            txtEmailID.Text = string.Empty;
            txtMobileNum.Text = string.Empty;
            ddlBMBranch.SelectedIndex = 0;
            txtCustomerName.Enabled = true;

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "StepOne event Fire", " StepEventFireCollapseExpand('one');", true);

        }

        protected void ResetControlsToInitialState()
        {
            MarkAllFieldEnableFalse();
            trNewCustomer.Visible = true;
            trExistingCustomer.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Verification", " GetVerificationType('Normal');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Priority", " enableDisablePriority('true');", true);

            txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
            txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";

            divSuccessMsg.Visible = false;
            tdCustomerSelection1.Visible = true;
            tdCustomerSelection2.Visible = true;
            txtCustomerNameEntry.Enabled = true;
            ddlBMBranch.Enabled = true;
            ddlCustomerCategory.Enabled = true;
            ddlPriority.Enabled = true;
            ddlPriority.SelectedIndex = 0;
            ddlStatusStage1.SelectedIndex = 0;
            ddlReasonStage1.SelectedIndex = 0;

            txtMobileNum.Text = string.Empty;
            txtEmailID.Text = string.Empty;
            txtCusName.Text = string.Empty;
            txtRequestNumber.Text = string.Empty;
            txtBranchCode.Text = string.Empty;
            txtBranchCode.Text = string.Empty;
            txtRequestTimeValue.Text = string.Empty;
            txtlblISAGenerationStatus.Text = string.Empty;
            txtRequestdate.Text = string.Empty;
            txtRBLCode.Text = string.Empty;
            txtRequestedByValue.Text = string.Empty;
            txtPanNum.Text = string.Empty;
            txtCustomerNameEntry.Text = string.Empty;
            txtGenerateReqstNum.Text = string.Empty;
            ddlCustomerCategory.SelectedIndex = 0;
            ddlBMBranch.SelectedIndex = 0;
            rdbNewCustomer.Checked = true;
            rdbExistingCustomer.Checked = false;

            if (Session["customerVo"] != null)
                Session.Remove("customerVo");

            EnableCurrentStep(0);
            


        }

        protected void ShowHideReasonDropListBasedOnStatus(string statusCode, int currentStepId)
        {
            switch (currentStepId)
            {
                case 1:
                    if (statusCode == "DO")
                    {
                        lblReasonStage1.Visible = false;
                        ddlReasonStage1.Visible = false;
                    }
                    else
                    {
                        lblReasonStage1.Visible = true;
                        ddlReasonStage1.Visible = true;
                    }
                   
                    break;
                case 2:
                    if (statusCode == "DO")
                    {
                        lblReasonStage2.Visible = false;
                        ddlReasonStage2.Visible = false;
                    }
                    else
                    {
                        lblReasonStage2.Visible = true;
                        ddlReasonStage2.Visible = true;
                    }
                    break;
                case 3:
                    if (statusCode == "DO")
                    {
                        lblStatusReason3.Visible = false;
                        ddlStatusReason3.Visible = false;
                    }
                    else
                    {
                        lblStatusReason3.Visible = true;
                        ddlStatusReason3.Visible = true;
                    }
                    break;
                case 4:
                    if (statusCode == "DO")
                    {
                        lblReasonStage4.Visible = false;
                        ddlReasonStage4.Visible = false;
                    }
                    else
                    {
                        lblReasonStage4.Visible = true;
                        ddlReasonStage4.Visible = true;
                    }
                    break;
            }

        }

        protected void ddlStatusStage2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlBackToStepStage2.SelectedIndex = 0;
            ddlReasonStage2.SelectedIndex = 0;
            if (ddlStatusStage2.SelectedValue == "PD")
            {
                ddlBackToStepStage2.Enabled=true;
                ddlReasonStage2.Visible=true;
                lblReasonStage2.Visible = true;
            }
            else  if (ddlStatusStage2.SelectedValue == "DO")
            {
                ddlBackToStepStage2.Enabled=false;
                ddlReasonStage2.Visible = false;
                lblReasonStage2.Visible = false;
            }
            else  
            {  
                ddlBackToStepStage2.Enabled=false;
                ddlReasonStage2.Visible = true;
                lblReasonStage2.Visible = true;
            }
        }
        protected void ddlStatusStage3_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            ddlBackToStepStage3.SelectedIndex = 0;
            ddlStatusReason3.SelectedIndex = 0;
            if (ddlStatusStage3.SelectedValue == "PD")
            {
                ddlBackToStepStage3.Enabled = true;
                ddlStatusReason3.Visible = true;
                lblStatusReason3.Visible = true;
            }
            else if (ddlStatusStage2.SelectedValue == "DO")
            {
                ddlBackToStepStage3.Enabled = false;
                ddlStatusReason3.Visible = false;
                lblStatusReason3.Visible = false;
            }
            else
            {
                ddlBackToStepStage3.Enabled = false;
                ddlStatusReason3.Visible = true;
                lblStatusReason3.Visible = true;
            }
            
        }
        protected void ddlStatusStage4_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            ddlBackToStepStage4.SelectedIndex = 0;
            ddlReasonStage4.SelectedIndex = 0;
            if (ddlStatusStage4.SelectedValue == "PD")
            {
                ddlBackToStepStage4.Enabled = true;
                ddlReasonStage4.Visible = true;
                lblReasonStage4.Visible = true;
            }
            else if (ddlStatusStage2.SelectedValue == "DO")
            {
                ddlBackToStepStage4.Enabled = false;
                ddlReasonStage4.Visible = false;
                lblReasonStage4.Visible = false;
            }
            else
            {
                ddlBackToStepStage4.Enabled = false;
                ddlReasonStage4.Visible = true;
                lblReasonStage4.Visible = true;
            }
           
        }
       
    }
}