using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using BoAdvisorProfiling;
using VoUser;
using BoUploads;
using BoCustomerGoalProfiling;
using Telerik.Web.UI;
using BoCommon;
using System.Configuration;
using VOAssociates;
using BOAssociates;
using BoCustomerProfiling;
using BoUser;

namespace WealthERP.Associates
{
    public partial class AddAssociates : System.Web.UI.UserControl
    {
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        AssociatesVO associatesVo= new AssociatesVO();
        AssociatesBo associatesBo = new AssociatesBo();
        CustomerBo customerBo = new CustomerBo();
        UserBo userBo = new UserBo();

        List<int> associatesIds;
        string path = string.Empty;
        int advisorId = 0;
        String userType;
        int bmID = 0;
        string currentUserRole;
        int requestId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session["userVo"];
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            if (Session["associatesVo"] != null)
            {
                associatesVo = (AssociatesVO)Session["associatesVo"];
            }

            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            if (!String.IsNullOrEmpty(Session[SessionContents.CurrentUserRole].ToString()))
            {
                currentUserRole = Session[SessionContents.CurrentUserRole].ToString().ToLower();
            }

            advisorId = advisorVo.advisorId;
            bmID = rmVo.RMId;
            //EnableCurrentStep(1);
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
                if (Request.QueryString["page"] != null)
                {
                    SetControls(associatesVo);
                    associatesVo=(AssociatesVO)Session["associatesVo"] ;
                    EnableCurrentStep(0);
                    requestId = associatesVo.AdviserAssociateId;
                    ddlstatus1.Enabled = false;
                    lnlStep2.Enabled = false;
                    ddlstatus2.Enabled = true;
                }
                if (userType == "advisor")
                {
                    BindBranchDropDown();
                    BindRMDropDown();
                }
                if (userType == "bm")
                {
                    BindBranchForBMDropDown();
                    BindRMforBranchDropdown(0, bmID);
                }
                BindReasonAndStatus();
                if (requestId != 0)
                {
                    HideAndShowBasedOnRole(requestId);
                }
            }
        }

        private void HideAndShowBasedOnRole(int requestId)
        {
             DataSet dsAssociatesStepDetails = new DataSet();
            string stepCode = string.Empty;
            string stageStatus = string.Empty;
            int currentStep = 0;

            if (requestId != 0)
            {
                dsAssociatesStepDetails = associatesBo.GetAssociatesStepDetails(requestId);
                if (dsAssociatesStepDetails != null)
                {
                    if (dsAssociatesStepDetails.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drISARequestDetails in dsAssociatesStepDetails.Tables[0].Rows)
                        {
                            //------**************SET THE STEP STATUS IMAGE BASED ON ACTIVE STEP*********---------------//

                            if (Convert.ToBoolean(drISARequestDetails["AWFSD_IsActivestep"]) == true)
                            {
                                currentStep = Convert.ToInt16(drISARequestDetails["WWFSM_StepOrder"].ToString());
                                stepCode = drISARequestDetails["WWFSM_StepCode"].ToString();
                                stageStatus = drISARequestDetails["AWFSD_StatusReason"].ToString();

                            }


                            //---------**************Request Header Section**************--------------\\

                            if (drISARequestDetails["WWFSM_StepCode"].ToString() == "AREQ")
                            {


                                if (!String.IsNullOrEmpty(drISARequestDetails["AA_ContactPersonName"].ToString()))
                                    txtAssoName.Text = drISARequestDetails["AA_ContactPersonName"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AA_AdviserAssociateId"].ToString()))
                                    txtRequestNumber.Text = drISARequestDetails["AA_AdviserAssociateId"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AB_BranchName"].ToString()))
                                    txtBMName.Text = drISARequestDetails["AB_BranchName"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AR_FirstName"].ToString()))
                                    txtRMName.Text = drISARequestDetails["AR_FirstName"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["WWFSM_StepName"].ToString()))
                                    txtFinalStatus.Text = drISARequestDetails["WWFSM_StepName"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AA_RequestDate"].ToString()))
                                    txtRequestdate.Text = drISARequestDetails["AA_RequestDate"].ToString();



                                //------------****************************STEP 1***********************------------------------------\\


                                if (!String.IsNullOrEmpty(drISARequestDetails["AB_BranchId"].ToString()))
                                    ddlBranch.SelectedValue = drISARequestDetails["AB_BranchId"].ToString();


                                if (!String.IsNullOrEmpty(drISARequestDetails["WWFSM_StepName"].ToString()))
                                {
                                    lblHeaderStatusStage1.Text = drISARequestDetails["WWFSM_StepName"].ToString();
                                }
                                if (!String.IsNullOrEmpty(drISARequestDetails["AA_RequestDate"].ToString()))
                                    txtClosingDate.Text = drISARequestDetails["AA_RequestDate"].ToString();
                                if (!String.IsNullOrEmpty(drISARequestDetails["AR_RMId"].ToString()))
                                    ddlRM.SelectedValue = drISARequestDetails["AR_RMId"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AA_ContactPersonName"].ToString()))
                                    txtAssociateName.Text = drISARequestDetails["AA_ContactPersonName"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AA_PAN"].ToString()))
                                    txtPanNum.Text = drISARequestDetails["AA_PAN"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AA_Email"].ToString()))
                                    txtEmailId.Text = drISARequestDetails["AA_Email"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AA_Mobile"].ToString()))
                                    txtMobileNum.Text = drISARequestDetails["AA_Mobile"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AA_AdviserAssociateId"].ToString()))
                                    txtGenerateReqstNum.Text = drISARequestDetails["AA_AdviserAssociateId"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AWFSD_Status"].ToString()))
                                    ddlstatus1.SelectedValue = drISARequestDetails["AWFSD_Status"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AWFSD_StatusReason"].ToString()))
                                    ddlReasonStage1.SelectedValue = drISARequestDetails["AWFSD_StatusReason"].ToString();

                                ShowHideReasonDropListBasedOnStatus(drISARequestDetails["AWFSD_Status"].ToString(), 1);

                            }

                            //------------****************************STEP 2***********************------------------------------\\

                            if (drISARequestDetails["WWFSM_StepCode"].ToString() == "ADETLS" && drISARequestDetails["AA_StepStatus"].ToString() != null)
                            {

                                ShowHideReasonDropListBasedOnStatus(drISARequestDetails["AWFSD_Status"].ToString(), 2);

                            }

                            //------------****************************STEP 3***********************------------------------------\\

                            if (drISARequestDetails["WWFSM_StepCode"].ToString() == "ACGEN" && drISARequestDetails["AA_StepStatus"].ToString() != null)
                            {
                                ShowHideReasonDropListBasedOnStatus(drISARequestDetails["AWFSD_Status"].ToString(), 3);
                            }
                            MarkAllFieldEnableFalse();
                            if (currentStep == 3)
                            {
                                //txtlblISAGenerationStatus.Style["color"] = "green";
                                //txtlblISAGenerationStatus.Style["font-weight"] = "bold";
                                //SetStepStatusImage(0);
                                EnableCurrentStep(100); //False Case Make all enable false in case of completed ISA Request
                            }
                            else
                            {
                                //SetStepStatusImage(currentStep);
                                EnableCurrentStep(currentStep);
                            }

                       }
                    }
                }
            }
        }

        private void MarkAllFieldEnableFalse()
        {
            //**********------------------STEP ONE-------------*****************//

            ddlBranch.Enabled = false;
            ddlRM.Enabled = false;

            txtAssociateName.Enabled = false;
            txtPanNum.Enabled = false;
            txtPanNum.Enabled = false;
            txtEmailId.Enabled = false;
            txtMobileNum.Enabled = false;
            txtGenerateReqstNum.Enabled = false;

            btnSave.Visible = false;
            btnSubmitAddStage1.Visible = false;

            ddlstatus1.Enabled = false;
            ddlReasonStage1.Enabled = false;
            txtCommentstep1.Enabled = false;


            //**********------------------STEP TWO-------------*****************//

            ddlstatus2.Enabled = false;
            ddlReasonStage2.Enabled = false;
            txtComments2.Enabled = false;
            lnlStep2.Enabled = false;
            btnSubmitAddStage2.Visible = false;


            //**********------------------STEP THREE-------------*****************//

            ddlStepstatus3.Enabled = false;
            ddlReasonStep3.Enabled = false;
            txtCommentStep3.Enabled = false;
            btnSubmitStep3.Enabled = false;

        }

        private void ShowHideReasonDropListBasedOnStatus(string statusCode, int currentStepId)
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
                        lblStepStatus3.Visible = false;
                        ddlStepstatus3.Visible = false;
                    }
                    else
                    {
                        lblReasonStep3.Visible = true;
                        ddlReasonStep3.Visible = true;
                    }
                    break;
            }
        }

        private void EnableCurrentStep(int currentStepId)
        {
            switch (currentStepId)
            {
                case 1:
                    {
                        ddlBranch.Enabled = false;
                        ddlRM.Enabled = false;

                        txtAssociateName.Enabled = true;
                        txtPanNum.Enabled = true;
                        txtPanNum.Enabled = true;
                        txtEmailId.Enabled = true;
                        txtMobileNum.Enabled = true;
                        txtGenerateReqstNum.Enabled = false;

                        btnSave.Visible = true;
                        btnSubmitAddStage1.Visible = true;

                        ddlstatus1.Enabled = true;
                        ddlReasonStage1.Enabled = true;
                        txtCommentstep1.Enabled = true;
                        if (ddlstatus1.SelectedValue == "DO")
                        {
                            ddlReasonStage1.Visible = false;
                            lblReasonStage1.Visible = false;
                            lnlStep2.Enabled = true;
                        }

                   }

                    break;
                case 2:
                    {
                        ddlstatus2.Enabled = true;
                        ddlReasonStage2.Enabled = true;
                        txtComments2.Enabled = true;
                        lnlStep2.Enabled = true;
                        btnSubmitAddStage2.Visible = true;
                    }

                    break;
                case 3:
                    {
                        ddlStepstatus3.Enabled = true;
                        ddlReasonStep3.Enabled = true;
                        txtCommentStep3.Enabled = true;
                        btnSubmitStep3.Enabled = true;

                    }
                    break;
            }
        }
        private void BindBranchDropDown()
        {

            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null)
                {
                    ddlBranch.DataSource = ds;
                    ddlBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", bmID.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindRMDropDown()
        {
            try
            {
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
                if (dt.Rows.Count > 0)
                {
                    ddlRM.DataSource = dt;
                    ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
                    ddlRM.DataTextField = dt.Columns["RMName"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindRMDropDown()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindBranchForBMDropDown()
        {
            try
            {
                DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(0, bmID, 0);
                if (ds != null)
                {
                    ddlBranch.DataSource = ds.Tables[1]; ;
                    ddlBranch.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", bmID.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        {

            try
            {

                DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
                if (ds != null)
                {
                    ddlRM.DataSource = ds.Tables[0]; ;
                    ddlRM.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                    ddlRM.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserEQMIS.ascx:BindRMforBranchDropdown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0);
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Random id = new Random();
            string password = id.Next(10000, 99999).ToString();

            userVo.Password = password;
            userVo.LoginId = txtEmailId.Text.ToString();
            userVo.FirstName = txtAssociateName.Text.ToString();
            userVo.Email = txtEmailId.Text.ToString();

            associatesVo.ContactPersonName = txtAssociateName.Text;
            associatesVo.BranchId = int.Parse(ddlBranch.SelectedValue);
            associatesVo.BMName = ddlBranch.SelectedItem.Text;
            associatesVo.RMId = int.Parse(ddlRM.SelectedValue);
            associatesVo.RMNAme = ddlRM.SelectedItem.Text;
            associatesVo.UserRoleId = 1009;
            associatesVo.Email = txtEmailId.Text;
            associatesVo.PanNo = txtPanNum.Text;
            if (!string.IsNullOrEmpty(txtMobileNum.Text))
                associatesVo.Mobile = long.Parse(txtMobileNum.Text);
            else
                associatesVo.Mobile = 0;
            associatesVo.RequestDate = DateTime.Now;
            associatesVo.AAC_UserType = "Agent";
            Session["AssociatesVo"] = associatesVo;
            associatesIds = associatesBo.CreateCompleteAssociates(userVo, associatesVo, userVo.UserId);
            associatesVo.UserId = associatesIds[0];
            associatesVo.AdviserAssociateId = associatesIds[1];
            txtGenerateReqstNum.Text = associatesVo.AdviserAssociateId.ToString();
            Session["userId"] = associatesVo.UserId;
            Session["associatesId"] = associatesVo.AdviserAssociateId;
            Session["AdviserAgentId"] = associatesVo.AAC_AdviserAgentId;
            //------------------------ To create User role Association-----------------------
            userBo.CreateRoleAssociation(associatesVo.UserId, 1009);

            if (associatesIds.Count > 0)
            {
                HideAndShowBasedOnRole(associatesIds[1]);
            }

            AssignHeaderInfo();
            SetAccsessMode();
            //txtRequestNumber.Text = associatesVo.AdviserAssociateId.ToString();
            divStep1SuccMsg.Visible = true;
        }

        private void AssignHeaderInfo()
        {
            if (Session["AssociatesVo"] != null)
            {
                associatesVo=(AssociatesVO)Session["AssociatesVo"];
                txtAssoName.Text = associatesVo.ContactPersonName;
                txtBMName.Text = associatesVo.BMName;
                txtRMName.Text = associatesVo.RMNAme;
                txtRequestdate.Text = associatesVo.RequestDate.ToShortDateString();
            }
        }

        private void SetAccsessMode()
        {
            ddlBranch.Enabled = false;
            ddlRM.Enabled = false;
            txtAssociateName.Enabled = false;
            txtEmailId.Enabled = false;
            btnSave.Visible = false;
            lnlStep2.Enabled = false;
            ddlstatus1.Enabled = true;
            //lnlStep3.Enabled = false;
            btnSubmitAddStage1.Visible = true;
            

        }

        protected void Step2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text.ToString().Trim()))
            {
                requestId = int.Parse(txtGenerateReqstNum.Text);
                GetAdviserAssociatesDetails(requestId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddAssociatesDetails','action=View');", true);
                ddlstatus2.Enabled = true;
                ddlReasonStage2.Enabled = true;
            }
        }

        private void GetAdviserAssociatesDetails(int associateId)
        {
            DataSet dsGetAssociatesDetails;
            associatesVo.AdviserAssociateId = associateId;
            dsGetAssociatesDetails = associatesBo.GetAdviserAssociatesDetails(associateId);

            if (dsGetAssociatesDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsGetAssociatesDetails.Tables[0].Rows)
                {
                    associatesVo.AdviserAssociateId = int.Parse(dr["AA_AdviserAssociateId"].ToString());
                    associatesVo.AAC_AdviserAgentId = int.Parse(dr["AAC_AdviserAgentId"].ToString());
                    if (!string.IsNullOrEmpty(dr["AAC_AdviserAgentId"].ToString().Trim()))
                        associatesVo.AAC_AdviserAgentId = int.Parse(dr["AAC_AdviserAgentId"].ToString());
                    else
                        associatesVo.AAC_AdviserAgentId = 0;
                    associatesVo.ContactPersonName = dr["AA_ContactPersonName"].ToString();
                    associatesVo.BMName = dr["AB_BranchName"].ToString();
                    associatesVo.RMId = int.Parse(dr["AR_RMId"].ToString());
                    associatesVo.RMNAme = dr["RM_Name"].ToString();
                    associatesVo.BranchId = int.Parse(dr["AB_BranchId"].ToString());
                    associatesVo.Email = dr["AA_Email"].ToString();
                    associatesVo.UserId = int.Parse(dr["U_UserId"].ToString());

                }
                Session["associatesVo"] = associatesVo;
            }
        }

        protected void lnlStep3_Click(object sender, EventArgs e)
        {
            int agentId = 0;
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text.ToString().Trim()))
            {
                requestId = int.Parse(txtGenerateReqstNum.Text);
                GetAdviserAssociatesDetails(requestId);
            }
            agentId = associatesVo.AAC_AdviserAgentId;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddBranchRMAgentAssociation','?AgentId=" + agentId +"');", true);
        }

        protected void ddlstatus1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlstatus1.SelectedIndex != 0 && ddlstatus1.SelectedValue == "DO")
            {
                lnlStep2.Enabled = true;
                associatesVo.StatusCode = ddlstatus1.SelectedValue;
                lblReasonStage1.Visible = false;
                ddlReasonStage1.Visible = false;
                txtCommentstep1.Enabled = true;
            }
            else
            {
                ddlReasonStage1.Enabled = true;
                txtCommentstep1.Enabled = true;
            }
        }

        protected void ddlstatus2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlstatus2.SelectedIndex != 0 && ddlstatus2.SelectedValue == "DO")
            {
                lblReasonStage2.Visible = false;
                ddlReasonStage2.Visible = false;
            }
            btnSubmitAddStage2.Visible = true;
        }

        private void SetControls(AssociatesVO associatesVo)
        {
            ddlBranch.SelectedValue = associatesVo.BranchId.ToString();
            ddlRM.SelectedValue = associatesVo.RMId.ToString();
            txtAssociateName.Text = associatesVo.ContactPersonName;
            txtEmailId.Text = associatesVo.Email;
            if (ddlstatus1.SelectedValue == "DO")
                ddlReasonStage1.Visible = false;
            if (associatesVo.PanNo != null)
                txtPanNum.Text = associatesVo.PanNo;
            if (associatesVo.Mobile != 0)
                txtMobileNum.Text = associatesVo.Mobile.ToString();
            if (associatesVo.AdviserAssociateId != 0)
                txtRequestNumber.Text = associatesVo.AdviserAssociateId.ToString();
            btnSave.Visible = false;
            AssignHeaderInfo();
        }
        protected void BindReasonAndStatus()
        {
            DataSet ds = customerBo.GetReasonAndStatus("ISA");
            if (ds != null)
            {
                ddlstatus1.DataSource = ds.Tables[1];
                ddlstatus1.DataValueField = ds.Tables[1].Columns["XS_StatusCode"].ToString();
                ddlstatus1.DataTextField = ds.Tables[1].Columns["XS_Status"].ToString();
                ddlstatus1.DataBind();
                ddlstatus1.Items.Insert(0, new ListItem("Select", "Select"));

                ddlstatus2.DataSource = ds.Tables[1];
                ddlstatus2.DataValueField = ds.Tables[1].Columns["XS_StatusCode"].ToString();
                ddlstatus2.DataTextField = ds.Tables[1].Columns["XS_Status"].ToString();
                ddlstatus2.DataBind();
                ddlstatus2.Items.Insert(0, new ListItem("Select", "Select"));

                ddlStepstatus3.DataSource = ds.Tables[1];
                ddlStepstatus3.DataValueField = ds.Tables[1].Columns["XS_StatusCode"].ToString();
                ddlStepstatus3.DataTextField = ds.Tables[1].Columns["XS_Status"].ToString();
                ddlStepstatus3.DataBind();
                ddlStepstatus3.Items.Insert(0, new ListItem("Select", "Select"));

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

                ddlReasonStep3.DataSource = ds.Tables[0];
                ddlReasonStep3.DataValueField = ds.Tables[0].Columns["XSR_StatusReasonCode"].ToString();
                ddlReasonStep3.DataTextField = ds.Tables[0].Columns["XSR_StatusReason"].ToString();
                ddlReasonStep3.DataBind();
                ddlReasonStep3.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }

        protected void btnSubmitAddStage1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                associatesBo.UpdateAssociatesWorkFlowStatusDetails(int.Parse(txtGenerateReqstNum.Text), ddlstatus1.SelectedValue, "AREQ", ddlReasonStage1.SelectedValue);
                btnSubmitAddStage1.Visible = false;
                ddlstatus1.Enabled = false;
                HideAndShowBasedOnRole(int.Parse(txtGenerateReqstNum.Text));

                if (ddlstatus1.SelectedValue == "DO")
                {
                    ddlReasonStage1.Visible = false;
                    ddlstatus2.Enabled = false;
                    ddlReasonStage2.Enabled = false;
                    lnlStep2.Enabled = true;
                }

            }
        }

        protected void btnSubmitAgentCode_Click(object sender, EventArgs e)
        {
            int agentId = 0;
            GetAdviserAssociatesDetails(associatesVo.AdviserAssociateId);
            agentId = associatesVo.AAC_AdviserAgentId;
            //associatesVo.AAC_AgentCode = txtAgentCode.Text;
            associatesVo.AAC_CreatedBy = userVo.UserId;
            associatesVo.AAC_ModifiedBy = userVo.UserId;
            associatesBo.CreateAdviserAgentCode(associatesVo, agentId);
        }

        protected void btnSubmitAddStage2_Click(object sender, EventArgs e)
        {
            string reason = string.Empty;
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                if (ddlReasonStage2.SelectedIndex != 0)
                    reason = ddlReasonStage2.SelectedValue;
                associatesBo.UpdateAssociatesWorkFlowStatusDetails(int.Parse(txtGenerateReqstNum.Text), ddlstatus2.SelectedValue, "ADETLS", reason);
                btnSubmitAddStage2.Visible = false;
                ddlstatus2.Enabled = false;
                if (ddlstatus2.SelectedValue == "DO")
                {
                    ddlReasonStage2.Visible = false;
                    lblReasonStage2.Visible = false;
                    ddlstatus2.Enabled = false;
                    ddlReasonStage2.Enabled = false;
                    ddlStepstatus3.Enabled = true;
                    ddlReasonStep3.Enabled = true;
                    lnlStep2.Enabled = false;
                    btnSubmitStep3.Visible = true;
                }

            }
        }

        protected void btnSubmitStep3_Click(object sender, EventArgs e)
        {
            string reason = string.Empty; ;
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                if (ddlReasonStep3.SelectedIndex != 0)
                    reason = ddlReasonStep3.SelectedValue;
                associatesBo.UpdateAssociatesWorkFlowStatusDetails(int.Parse(txtGenerateReqstNum.Text), ddlStepstatus3.SelectedValue, "ACGEN", reason);
                btnSubmitStep3.Visible = false;
                ddlStepstatus3.Enabled = true;
                ddlReasonStep3.Enabled = true;
            }
        }
    }
}