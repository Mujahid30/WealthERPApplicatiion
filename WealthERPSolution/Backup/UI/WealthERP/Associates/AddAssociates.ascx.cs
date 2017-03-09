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
using PCGMailLib;
using System.IO;
using VoAdvisorProfiling;
using System.Net.Mail;

namespace WealthERP.Associates
{
    public partial class AddAssociates : System.Web.UI.UserControl
    {
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        AssociatesVO associatesVo = new AssociatesVO();
        AssociatesBo associatesBo = new AssociatesBo();
        CustomerBo customerBo = new CustomerBo();
        UserBo userBo = new UserBo();
        UserVo associateUserVo = new UserVo();

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
            hdnAdviserID.Value = advisorVo.advisorId.ToString();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session["userVo"];
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            if (Session["TempAssociatesVo"] != null)
            {
                associatesVo = (AssociatesVO)Session["TempAssociatesVo"];
            }

            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
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
                if (Request.QueryString["pageName"] != null)
                {
                    if (Request.QueryString["AssociationId"] != null)
                    {
                        requestId = int.Parse(Request.QueryString["AssociationId"].ToString());
                    }
                    if (txtGenerateReqstNum.Text != null)
                        HideAndShowBasedOnRole(requestId);
                    lnlStep2.Enabled = false;
                    lnkAgentCode.Enabled = true;
                    ddlstatus2.Enabled = true;
                    ddlReasonStage2.Enabled = true;
                    btnSubmitAddStage2.Visible = true;
                    btnSubmitAddStage2.Enabled = true;
                }
                if (Request.QueryString["fromPage"] != null)
                {
                    if (Request.QueryString["AssociationId"] != null)
                    {
                        requestId = int.Parse(Request.QueryString["AssociationId"].ToString());
                    }
                    if (txtGenerateReqstNum.Text != null)
                        HideAndShowBasedOnRole(requestId);
                    ddlStepstatus3.Enabled = true;
                    ddlReasonStep3.Enabled = true;
                }
                if (Request.QueryString["page"] != null)
                {
                    SetControls(associatesVo);
                    associatesVo = (AssociatesVO)Session["TempAssociatesVo"];
                    EnableCurrentStep(0);
                    requestId = associatesVo.AdviserAssociateId;
                    ddlstatus1.Enabled = false;
                    lnlStep2.Enabled = false;
                    ddlstatus2.Enabled = true;
                    
                }
                //if (userType == "advisor")
                //{
                //    BindBranchDropDown();
                //    //BindRMDropDown();
                //}
                //if (userType == "bm")
                //{
                //    BindBranchForBMDropDown();
                //    BindRMforBranchDropdown(0, bmID);
                //}
                BindHierarchyTitleDropList();
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
                                if (!String.IsNullOrEmpty(drISARequestDetails["AH_HierarchyId"].ToString()))
                                {
                                    ddlTitleList.SelectedValue = drISARequestDetails["AH_HierarchyId"].ToString();
                                    BindStaffDropList(Convert.ToInt32(drISARequestDetails["AH_HierarchyId"].ToString()));
                                }


                                if (!String.IsNullOrEmpty(drISARequestDetails["AR_RMId"].ToString()))
                                {
                                    ddlRM.SelectedValue = drISARequestDetails["AR_RMId"].ToString();
                                    BindStaffBranchDropList(Convert.ToInt32(drISARequestDetails["AR_RMId"].ToString()));
                                }

                                if (!String.IsNullOrEmpty(drISARequestDetails["AB_BranchId"].ToString()))
                                {
                                    ddlBranch.SelectedValue = drISARequestDetails["AB_BranchId"].ToString();
                                    //BindStaffBranchDropList(Convert.ToInt32(drISARequestDetails["AB_BranchId"].ToString()));
                                }


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
                                if (!String.IsNullOrEmpty(drISARequestDetails["AWFSD_ModifiedOn"].ToString()))
                                    txtClosingDate2.Text = drISARequestDetails["AWFSD_ModifiedOn"].ToString();
                                if (!String.IsNullOrEmpty(drISARequestDetails["WWFSM_StepName"].ToString()))
                                    txtStatus2.Text = drISARequestDetails["WWFSM_StepName"].ToString();
                                if (!String.IsNullOrEmpty(drISARequestDetails["AWFSD_Status"].ToString()))
                                    ddlstatus2.SelectedValue = drISARequestDetails["AWFSD_Status"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AWFSD_StatusReason"].ToString()))
                                    ddlReasonStage2.SelectedValue = drISARequestDetails["AWFSD_StatusReason"].ToString();
                                if (!String.IsNullOrEmpty(drISARequestDetails["AAC_AgentCode"].ToString()))
                                {
                                    lnlStep2.Enabled = false;
                                    lnkAgentCode.Enabled = false;
                                }
                                else
                                {
                                    lnlStep2.Enabled = false;
                                    lnkAgentCode.Enabled = true;
                                }
                                ShowHideReasonDropListBasedOnStatus(drISARequestDetails["AWFSD_Status"].ToString(), 2);

                            }

                            //------------****************************STEP 3***********************------------------------------\\

                            if (drISARequestDetails["WWFSM_StepCode"].ToString() == "ACGEN" && drISARequestDetails["AA_StepStatus"].ToString() != null)
                            {
                                if (!String.IsNullOrEmpty(drISARequestDetails["AWFSD_ModifiedOn"].ToString()))
                                    txtClosingDate3.Text = drISARequestDetails["AWFSD_ModifiedOn"].ToString();
                                if (!String.IsNullOrEmpty(drISARequestDetails["WWFSM_StepName"].ToString()))
                                    txtStatus3.Text = drISARequestDetails["WWFSM_StepName"].ToString();
                                if (!String.IsNullOrEmpty(drISARequestDetails["AWFSD_Status"].ToString()))
                                    ddlStepstatus3.SelectedValue = drISARequestDetails["AWFSD_Status"].ToString();

                                if (!String.IsNullOrEmpty(drISARequestDetails["AWFSD_StatusReason"].ToString()))
                                    ddlReasonStep3.SelectedValue = drISARequestDetails["AWFSD_StatusReason"].ToString();
                                ShowHideReasonDropListBasedOnStatus(drISARequestDetails["AWFSD_Status"].ToString(), 3);
                            }
                        }
                        

                        
                    }
                }
            }
            MarkAllFieldEnableFalse();
            if (currentStep == 3)
            {
                //txtlblISAGenerationStatus.Style["color"] = "green";
                //txtlblISAGenerationStatus.Style["font-weight"] = "bold";
                //SetStepStatusImage(0);
                EnableCurrentStep(3); //False Case Make all enable false in case of completed ISA Request
            }
            else
            {
                //SetStepStatusImage(currentStep);
                EnableCurrentStep(currentStep);
            }
        }

        private void MarkAllFieldEnableFalse()
        {
            //**********------------------STEP ONE-------------*****************//

            ddlBranch.Enabled = false;
            ddlRM.Enabled = false;
            ddlTitleList.Enabled = false;

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
                        lblStepStatus3.Visible = true;
                        ddlStepstatus3.Visible = true;
                        lblReasonStep3.Visible = false;
                        ddlReasonStep3.Visible = false;
                    }
                    else
                    {
                        lblStepStatus3.Visible = true;
                        ddlStepstatus3.Visible = true;
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
                        ddlTitleList.Enabled = false;

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
                        //lnlStep2.Enabled = true;
                        btnSubmitAddStage2.Visible = true;
                        btnSubmitAddStage2.Enabled = true;
                        //lnkAgentCode.Enabled = true;
                        //lnlStep2.Enabled = true;
                    }

                    break;
                case 3:
                    {
                        ddlStepstatus3.Enabled = true;
                        ddlReasonStep3.Enabled = true;
                        txtCommentStep3.Enabled = true;
                        btnSubmitStep3.Enabled = true;
                        btnSubmitStep3.Visible = true;
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
        private void BindRMDropDown(int staffId)
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
        protected void ddlRM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRM.SelectedIndex >0)
            {
                BindStaffBranchDropList(Convert.ToInt32(ddlRM.SelectedValue));
            }

        }

        private void BindStaffDropList(int hierarchyId)
        {

            DataSet ds = associatesBo.GetAdviserHierarchyStaffList(hierarchyId);
            if (ds != null)
            {
                ddlRM.DataSource = ds.Tables[0]; ;
                ddlRM.DataValueField = ds.Tables[0].Columns["AR_RMId"].ToString();
                ddlRM.DataTextField = ds.Tables[0].Columns["AR_RMName"].ToString();
                ddlRM.DataBind();
            }
            ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                associateUserVo = new UserVo();
                Random id = new Random();
                string password = id.Next(10000, 99999).ToString();

                associateUserVo.Password = password;
                associateUserVo.LoginId = txtEmailId.Text.ToString();
                associateUserVo.FirstName = txtAssociateName.Text.ToString();
                associateUserVo.Email = txtEmailId.Text.ToString();
                associateUserVo.UserType = "Associates";

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
                associatesVo.AAC_UserType = "Associates";
                Session["TempAssociatesVo"] = associatesVo;
                Session["AssociateUserVo"] = associateUserVo;
                associatesIds = associatesBo.CreateCompleteAssociates(associateUserVo, associatesVo, userVo.UserId);
                associatesVo.UserId = associatesIds[0];
                associatesVo.AdviserAssociateId = associatesIds[1];
                txtGenerateReqstNum.Text = associatesVo.AdviserAssociateId.ToString();
                //Session["userId"] = associatesVo.UserId;
                //Session["associatesId"] = associatesVo.AdviserAssociateId;
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
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Pan Number Already Exists');", true);
            }
        }

        private bool Validation()
        {
            bool result = false;
            //int adviserId = (int)Session["adviserId"];
            //if (associatesBo.PANNumberDuplicateCheck(adviserId, txtPanNum.Text.ToString(), associatesVo.AdviserAssociateId))
            //{
            //    result = false;
            //    lblPanDuplicate.Visible = true;
            //}
            //if (hidValidCheck.Value != "0")
            //{
                result = true;
            //}
            
            return result;
        }

        private void AssignHeaderInfo()
        {
            if (Session["TempAssociatesVo"] != null)
            {
                associatesVo = (AssociatesVO)Session["TempAssociatesVo"];
                txtAssoName.Text = associatesVo.ContactPersonName;
                txtBMName.Text = associatesVo.BMName;
                txtRMName.Text = associatesVo.RMNAme;
                txtRequestdate.Text = associatesVo.RequestDate.ToShortDateString();
            }
        }

        private void SetAccsessMode()
        {
            ddlTitleList.Enabled = false;
            ddlBranch.Enabled = false;
            ddlRM.Enabled = false;
            txtAssociateName.Enabled = false;
            txtEmailId.Enabled = false;
            btnSave.Visible = false;
            lnlStep2.Enabled = false;
            ddlstatus1.Enabled = true;
            //lnlStep3.Enabled = false;
            btnSubmitAddStage1.Visible = true;
            lblReasonStage1.Visible = false;
            ddlReasonStage1.Visible = false;

        }

        protected void lnlStep2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text.ToString().Trim()))
            {
                requestId = int.Parse(txtGenerateReqstNum.Text);
                GetAdviserAssociatesDetails(requestId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddAssociatesDetails','action=EditFromRequestPage');", true);
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
                Session["TempAssociatesVo"] = associatesVo;
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddBranchRMAgentAssociation','?AgentId=" + agentId + "');", true);
        }

        protected void ddlstatus1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if ((ddlstatus1.SelectedValue == "DO") ||(ddlstatus1.SelectedValue == "IP"))
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
                lblReasonStage1.Visible = true;
                ddlReasonStage1.Visible = true;
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
                ddlstatus1.SelectedValue = "IP";
                ddlstatus1.Items.RemoveAt(3);

                ddlstatus2.DataSource = ds.Tables[1];
                ddlstatus2.DataValueField = ds.Tables[1].Columns["XS_StatusCode"].ToString();
                ddlstatus2.DataTextField = ds.Tables[1].Columns["XS_Status"].ToString();
                ddlstatus2.DataBind();
                ddlstatus2.Items.Insert(0, new ListItem("Select", "Select"));
                ddlstatus2.Items.RemoveAt(4);

                ddlStepstatus3.DataSource = ds.Tables[1];
                ddlStepstatus3.DataValueField = ds.Tables[1].Columns["XS_StatusCode"].ToString();
                ddlStepstatus3.DataTextField = ds.Tables[1].Columns["XS_Status"].ToString();
                ddlStepstatus3.DataBind();
                ddlStepstatus3.Items.Insert(0, new ListItem("Select", "Select"));
                ddlStepstatus3.Items.RemoveAt(4);

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
            string comments;
            string reason = null;
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                comments = txtCommentstep1.Text;
                if (ddlReasonStage1.SelectedIndex != 0)
                    reason = ddlReasonStage1.SelectedValue;
                associatesBo.UpdateAssociatesWorkFlowStatusDetails(int.Parse(txtGenerateReqstNum.Text), ddlstatus1.SelectedValue, "AREQ", reason, comments);
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
            associatesBo.CreateAdviserAgentCode(associatesVo, agentId,advisorVo.advisorId);
        }

        protected void btnSubmitAddStage2_Click(object sender, EventArgs e)
        {
            string reason = string.Empty;
            string comments;
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                if (ddlReasonStage2.SelectedIndex != 0)
                    reason = ddlReasonStage2.SelectedValue;
                comments = txtComments2.Text;
                associatesBo.UpdateAssociatesWorkFlowStatusDetails(int.Parse(txtGenerateReqstNum.Text), ddlstatus2.SelectedValue, "ADETLS", reason, comments);
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
                    btnSubmitStep3.Enabled = true;
                }

            }
        }

        protected void lnkAgentCode_Click(object sender, EventArgs e)
        {
            int associationId = 0;
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
                associationId = int.Parse(txtGenerateReqstNum.Text);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddBranchRMAgentAssociation','?AssociationId=" + associationId + "');", true);
        }

        protected void ddlStepstatus3_SelectedIndexChanged(object sender, EventArgs e)
        {
             if (ddlStepstatus3.SelectedIndex != 0 && ddlStepstatus3.SelectedValue == "DO")
            {
                lblReasonStep3.Visible = false;
                ddlReasonStep3.Visible = false;
                //chkMailSend.Visible = true;
            }
        }

        protected void btnSubmitStep3_Click1(object sender, EventArgs e)
        {
            string reason = string.Empty;
            string comments;
            if (!string.IsNullOrEmpty(txtGenerateReqstNum.Text))
            {
                if (ddlReasonStep3.SelectedIndex != 0)
                    reason = ddlReasonStep3.SelectedValue;
                comments = txtCommentStep3.Text;
                associatesBo.UpdateAssociatesWorkFlowStatusDetails(int.Parse(txtGenerateReqstNum.Text), ddlStepstatus3.SelectedValue, "ACGEN", reason, comments);

                //if (chkMailSend.Checked == true)
                //{
                //    SendMail();
                //}
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewAssociates','');", true);
            }
        }

        private bool SendMail()
        {
            Emailer emailer = new Emailer();
            EmailMessage email = new EmailMessage();
            string logoPath = string.Empty;
            if (associateUserVo != null)
            {

                string userName = associateUserVo.FirstName + " " + associateUserVo.MiddleName + " " + associateUserVo.LastName;
                email.GetAdviserRMAccountMail("Asso" + associateUserVo.UserId.ToString(), associateUserVo.OriginalPassword, userName);



                email.Subject = email.Subject.Replace("WealthERP", advisorVo.OrganizationName);
                email.Subject = email.Subject.Replace("MoneyTouch", advisorVo.OrganizationName);

                email.Body = email.Body.Replace("[NAME]", userName);
                email.Body = email.Body.Replace("[ORGANIZATION]", advisorVo.OrganizationName);
                if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Wealtherp")
                {
                    email.Body = email.Body.Replace("[WEBSITE]", !string.IsNullOrEmpty(advisorVo.DomainName.Trim()) ? advisorVo.Website.Trim() : "https://app.wealtherp.com/");
                }
                else if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Citrus")
                {
                    email.Body = email.Body.Replace("[WEBSITE]", !string.IsNullOrEmpty(advisorVo.DomainName.Trim()) ? advisorVo.Website.Trim() : "https://www.citrusindiaonline.com/");
                }
                email.Body = email.Body.Replace("[CONTACTPERSON]", (!string.IsNullOrEmpty(advisorVo.ContactPersonFirstName.Trim()) ? advisorVo.ContactPersonFirstName.Trim() + " " : String.Empty) + (!string.IsNullOrEmpty(advisorVo.ContactPersonMiddleName) ? advisorVo.ContactPersonMiddleName.Trim() + " " : String.Empty) + (!string.IsNullOrEmpty(advisorVo.ContactPersonLastName) ? advisorVo.ContactPersonLastName.Trim() + " " : String.Empty));
                email.Body = email.Body.Replace("[DESIGNATION]", advisorVo.Designation);
                email.Body = email.Body.Replace("[PHONE]", advisorVo.Phone1Std.ToString() + "-" + advisorVo.Phone1Number.ToString());
                email.Body = email.Body.Replace("[EMAIL]", advisorVo.Email);
                email.Body = email.Body.Replace("[LOGO]", "<img src='cid:HDIImage' alt='Logo'>");

                System.Net.Mail.AlternateView htmlView;
                System.Net.Mail.AlternateView plainTextView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("Text view", null, "text/plain");
                //System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(hidBody.Value.Trim() + "<image src=cid:HDIImage>", null, "text/html");
                htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<html><body " + "style='font-family:Tahoma, Arial; font-size: 10pt;'><p>" + email.Body + "</p></body></html>", null, "text/html");
                //Add image to HTML version
                if (advisorVo != null)
                    logoPath = "~/Images/" + advisorVo.LogoPath;
                if (!File.Exists(Server.MapPath(logoPath)))
                    logoPath = "~/Images/spacer.png";
                //System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath("~/Images/") + @"\3DSYRW_4009.JPG", "image/jpeg");
                System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath(logoPath), "image/jpeg");
                imageResource.ContentId = "HDIImage";
                htmlView.LinkedResources.Add(imageResource);
                //Add two views to message.
                email.AlternateViews.Add(plainTextView);
                email.AlternateViews.Add(htmlView);
                email.To.Add(associateUserVo.Email);

                AdviserStaffSMTPBo adviserStaffSMTPBo = new AdviserStaffSMTPBo();
                AdviserStaffSMTPVo adviserStaffSMTPVo = adviserStaffSMTPBo.GetSMTPCredentials(((RMVo)Session["rmVo"]).RMId);
                if (adviserStaffSMTPVo.HostServer != null && adviserStaffSMTPVo.HostServer != string.Empty)
                {
                    emailer.isDefaultCredentials = !Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired);

                    if (!String.IsNullOrEmpty(adviserStaffSMTPVo.Password))
                        emailer.smtpPassword = Encryption.Decrypt(adviserStaffSMTPVo.Password);
                    emailer.smtpPort = int.Parse(adviserStaffSMTPVo.Port);
                    emailer.smtpServer = adviserStaffSMTPVo.HostServer;
                    emailer.smtpUserName = adviserStaffSMTPVo.Email;

                    if (Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired))
                    {
                        if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Wealtherp")
                        {
                            email.From = new MailAddress(emailer.smtpUserName, advisorVo.OrganizationName);
                        }
                        else if (ConfigurationSettings.AppSettings["HostName"].ToString() == "MoneyTouch")
                        {
                            email.From = new MailAddress(emailer.smtpUserName, advisorVo.OrganizationName);
                        }

                    }
                }
            }
            bool isMailSent = emailer.SendMail(email);
            return isMailSent;
        }

        protected void ddlTitleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTitleList.SelectedIndex != -1)
            {
                BindStaffDropList(Convert.ToInt32(ddlTitleList.SelectedValue));
            }

        }

        private void BindHierarchyTitleDropList()
        {
            DataTable dtAdviserHierachyTitleList = new DataTable();
            dtAdviserHierachyTitleList = associatesBo.GetAdviserHierarchyTitleList(advisorVo.advisorId);
            ddlTitleList.DataSource = dtAdviserHierachyTitleList;
            ddlTitleList.DataValueField = dtAdviserHierachyTitleList.Columns["AH_Id"].ToString();
            ddlTitleList.DataTextField = dtAdviserHierachyTitleList.Columns["AH_HierarchyName"].ToString();
            ddlTitleList.DataBind();
            ddlTitleList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
 
        }

        private void BindStaffBranchDropList(int staffId)
        {
            DataTable dtAdviserStaffBranchList = new DataTable();
            dtAdviserStaffBranchList = associatesBo.GetAdviserStaffBranchList(staffId);
            ddlBranch.DataSource = dtAdviserStaffBranchList;
            ddlBranch.DataValueField = dtAdviserStaffBranchList.Columns["AB_BranchId"].ToString();
            ddlBranch.DataTextField = dtAdviserStaffBranchList.Columns["AB_BranchName"].ToString();
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }

    }
}
