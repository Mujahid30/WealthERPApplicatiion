using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using VoUser;
using BoUploads;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using Telerik.Web.UI;
using VoAdvisorProfiling;
using BoCommon;
using System.Configuration;
using System.Globalization;
using VOAssociates;
using BOAssociates;
using BoSuperAdmin;
using System.Drawing;

namespace WealthERP.BusinessMIS
{
    public partial class MutualFundMIS : System.Web.UI.UserControl
    {

        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();
        DateBo dtBo = new DateBo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        DataSet dsMISReport;
        DataTable dtAllBranchRms;
        DataTable dt = new DataTable();
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
        AdvisorMISBo adviserMISBo = new AdvisorMISBo();
        RMVo rmVo = new RMVo();
        AssociatesVO associatesVo = new AssociatesVO();
        string userType;
        int advisorId;
        int userId;
        int branchHeadId;
        DateTime Valuationdate;
        int rmId, branchId;
        DateTime vlndte = new DateTime();
        int AdviserID;
        UserVo userVo = new UserVo();
        int bmID;
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        DateTime LatestValuationdate = new DateTime();
        bool GridViewCultureFlag = true;
        string path;
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();
        DateTime convertedFromDate = new DateTime();
        DateTime convertedToDate = new DateTime();
        CultureInfo ci = new CultureInfo("en-GB");
        DataSet dsMfMIS = new DataSet();
        DataTable dtAdviserMFMIS = new DataTable();

        DateTime dtFromHldDate = new DateTime();
        DateTime dtToHldDate = new DateTime();
        DataSet dsValidToandFromDates = new DataSet();

        DataSet dsValuationDates;
        int AmcCode = 0;
        int SchemeCode = 0;
        string type;
        string AgentCode;
        DateTime QSdtCurrentDate;
        DateTime QSdtPreviousDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            rmVo = new RMVo();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            advisorBranchVo = (AdvisorBranchVo)Session[SessionContents.AdvisorBranchVo];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session[SessionContents.UserVo];
            associatesVo = (AssociatesVO)Session["associatesVo"];


            if (Request.QueryString["QSdtCurrentDate"] != null)
            {

                QSdtCurrentDate = Convert.ToDateTime(Request.QueryString["QSdtCurrentDate"].ToString());
                txtDate.SelectedDate = QSdtCurrentDate;
                BindAMCWISEAUMDetails();
            }

            if (Request.QueryString["QSdtPreviousDate"] != null)
            {
                QSdtCurrentDate = Convert.ToDateTime(Request.QueryString["QSdtCurrentDate"].ToString());
                txtDate.SelectedDate = QSdtCurrentDate;
                BindAMCWISEAUMDetails();
            }

            if (advisorVo.A_AgentCodeBased == 0)
            {
                if (advisorVo.advisorId == 1000)
                {
                    //if (ddlAdviser.SelectedValue != "Select" && ddlAdviser.SelectedValue != "")
                    //{
                    //    advisorId = Convert.ToInt32(ddlAdviser.SelectedValue.ToString());
                    //    if (hfRmId.Value != "")
                    //    {
                    //        rmId = Convert.ToInt32(hfRmId.Value);
                    //    }
                    //}
                    //else
                    //{
                    advisorId = 1000;
                    //}
                }
                else
                {

                    bmID = rmVo.RMId;

                    if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                        userType = "advisor";
                    else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                        userType = "rm";
                    else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                        userType = "bm";
                    else
                        userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();
                    advisorId = advisorVo.advisorId;

                }
            }
            if (advisorVo.A_AgentCodeBased == 1)
            {
                bmID = rmVo.RMId;

                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    userType = "advisor";
                    AgentCode = "0";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                    userType = "rm";
                else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                    userType = "bm";
                else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
                {
                    //lblType.Visible = false;
                    //ddlType.Visible = false;
                    userType = "associates";
                    associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                    if (associateuserheirarchyVo.UserTitle == "SubBroker")
                    {
                        if (associateuserheirarchyVo.AgentCode != null)
                        {
                            AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                        }
                        else
                        {
                            AgentCode = "0";
                        }
                    }
                    else
                    {
                        if (associateuserheirarchyVo.AgentCode != null)
                        {
                            AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                        }
                        else
                        {
                            AgentCode = "0";
                        }
                    }
                }
                else
                    userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();
            }
            if (userType == "associates")
            {
                ddlType.Items[0].Enabled = false;
            }
            advisorId = advisorVo.advisorId;
            if (!IsPostBack)
            {
                if (advisorVo.A_AgentCodeBased == 0)
                {
                    if (advisorId != 1000)
                    {
                        divDateDetails.Visible = true;
                        divSelectionDetails.Visible = true;
                        divAdviserList.Visible = false;
                        if (userType == "advisor")
                        {

                            hidingConrolForRMAndBMLogin(userType);
                            BindBranchDropDown();
                            BindRMDropDown();
                        }
                        else if (userType == "rm")
                        {
                            hidingConrolForRMAndBMLogin(userType);
                        }
                        if (userType == "bm")
                        {
                            hidingConrolForRMAndBMLogin(userType);
                            BindBranchForBMDropDown();
                            bmID = rmVo.RMId;
                            BindRMforBranchDropdown(0, bmID, 1);

                            hdnbranchHeadId.Value = ddlBranch.SelectedValue;
                        }
                    }
                    else
                    {
                        divAdviserList.Visible = true;
                        BindAdviserDropDownList();

                    }
                }
                if (advisorVo.A_AgentCodeBased == 1)
                {
                    if (advisorId != 1000)
                    {
                        divDateDetails.Visible = true;
                        divSelectionDetails.Visible = true;
                        divAdviserList.Visible = false;
                        if (userType == "advisor")
                        {
                            hidingConrolForRMAndBMLogin(userType);
                            lblBranch.Visible = false;
                            ddlBranch.Visible = false;
                            ddlRM.Visible = false;
                            lblRM.Visible = false;
                            BindBranchDropDown();
                            BindRMDropDown();
                        }
                        else if (userType == "rm")
                        {
                            hidingConrolForRMAndBMLogin(userType);
                        }
                        if (userType == "bm")
                        {
                            hidingConrolForRMAndBMLogin(userType);
                            BindBranchForBMDropDown();
                            bmID = rmVo.RMId;
                            BindRMforBranchDropdown(0, bmID, 1);

                            hdnbranchHeadId.Value = ddlBranch.SelectedValue;
                        }
                        if (userType == "associates")
                        {
                            hidingConrolForRMAndBMLogin(userType);
                            BindBranchDropDown();
                            BindRMDropDown();
                        }
                    }
                    else
                    {
                        divAdviserList.Visible = true;
                        BindAdviserDropDownList();
                    }
                }


                //LatestValuationdate = adviserMISBo.GetLatestValuationDateFromHistory(advisorId,"MF");
                //hdnValuationDate.Value = LatestValuationdate.ToString("MM/dd/yyyy");  
                LatestValuationdate = adviserMISBo.GetLatestValuationDateFromHistory(advisorVo.advisorId, "MF");
                vlndte = LatestValuationdate;
                hdnValuationDate.Value = LatestValuationdate.ToString("MM/dd/yyyy");
                lblValDt.Text = vlndte.ToShortDateString();
                txtDate.SelectedDate = Convert.ToDateTime(vlndte.ToShortDateString());
                if (Request.QueryString["action"] != null)
                {
                    type = Request.QueryString["action"].ToString();
                    SetParameters();
                    if (type == "SchemeWise")
                    {
                        txtDate.SelectedDate = Convert.ToDateTime(vlndte.ToShortDateString());
                        showHideGrid("SchemeWise");
                        BindSCHEMEWISEAUMDetails();
                        lblMFMISType.Text = "SCHEME WISE AUM";
                    }
                    else if (type == "FolioWise")
                    {
                        txtDate.SelectedDate = Convert.ToDateTime(vlndte.ToShortDateString());
                        showHideGrid("FolioWise");
                        BindFOLIOWISEAUMDetails();
                        lblMFMISType.Text = "FOLIO WISE AUM";
                    }
                }


            }


            validateToFromDates();

        }

        protected void HideGridColumnonAgenetBasedCode()
        {
            if (advisorVo.A_AgentCodeBased == 0)
            {
                gvFolioWiseAUM.Columns[2].Visible = false;
                gvFolioWiseAUM.Columns[3].Visible = false;
                gvFolioWiseAUM.Columns[4].Visible = false;
                gvFolioWiseAUM.Columns[5].Visible = false;
                gvFolioWiseAUM.Columns[6].Visible = false;
                gvFolioWiseAUM.Columns[7].Visible = false;
            }

        }
        private void BindRMforBranchDropdown(int branchId, int branchHeadId, int all)
        {
            try
            {
                DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(branchId, branchHeadId, all);
                if (ds != null)
                {
                    ddlRM.DataSource = ds.Tables[0]; ;
                    ddlRM.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                    ddlRM.DataTextField = ds.Tables[0].Columns["RM Name"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserMFMIS.ascx:BindRMforBranchDropdown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlFilterSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFilterSelection.SelectedValue == "1")
            {
                lblTo.Visible = true;
                lblFrom.Visible = true;
                rdpFrom.Visible = true;
                rdpTo.Visible = true;
                txtDate.Visible = false;
                lnkBtnAMCWISEAUM.Visible = false;
            }
            else if (ddlFilterSelection.SelectedValue == "0")
            {
                lnkBtnAMCWISEAUM.Visible = true;
                lblTo.Visible = false;
                lblFrom.Visible = false;
                rdpFrom.Visible = false;
                rdpTo.Visible = false;
                txtDate.Visible = true;
            }
        }



        protected void ddlAdviser_SelectedIndexChanged(object sender, EventArgs e)
        {
            divDateDetails.Visible = true;
            divSelectionDetails.Visible = true;

            divGvAmcWiseAUM.Visible = false;
            divGvFolioWiseAUM.Visible = false;
            divGvSchemeWiseAUM.Visible = false;

            LatestValuationdate = adviserMISBo.GetLatestValuationDateFromHistory(Convert.ToInt32(ddlAdviser.SelectedValue), "MF");
            vlndte = LatestValuationdate;
            hdnValuationDate.Value = LatestValuationdate.ToString("MM/dd/yyyy");
            lblValDt.Text = vlndte.ToShortDateString();
            txtDate.SelectedDate = Convert.ToDateTime(vlndte.ToShortDateString());
        }

        private void BindBranchForBMDropDown()
        {
            try
            {
                DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(0, rmVo.RMId, 0);
                if (ds != null)
                {
                    ddlBranch.DataSource = ds.Tables[1]; ;
                    ddlBranch.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
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
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
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

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID, 1);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0, 0);
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
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
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
        #region binding grid for AUM

        public void lnkBtnAMCWISEAUM_OnClick(object sender, EventArgs e)
        {
            if (advisorVo.advisorId != 1000)
            {
                SetParameters();
                showHideGrid("AMCWise");
                BindAMCWISEAUMDetails();
                lblMFMISType.Text = "AMC WISE AUM";
            }
            else
            {
                showHideGrid("AMCWise");
                BindAMCWISEAUMDetails();
                lblMFMISType.Text = "AMC WISE AUM";
            }
        }

        private void SetParameters()
        {

            if (userType == "advisor")
            {
                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnAll.Value = "0";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRM.SelectedValue; ;
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "3";
                }

            }
            else if (userType == "rm")
            {
                hdnrmId.Value = rmVo.RMId.ToString();
                hdnAll.Value = "0";

            }
            else if (userType == "bm")
            {
                if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "3";
                }
            }
            else if (userType == "associates")
            {
                hdnAgentId.Value = associatesVo.AAC_AdviserAgentId.ToString();
                hdnbranchHeadId.Value = "0";
                hdnbranchId.Value = "0";
                hdnrmId.Value = "0";
            }
            else
            {
                if (hdnbranchHeadId.Value == "")
                    hdnbranchHeadId.Value = "0";

                if (hdnbranchId.Value == "")
                    hdnbranchId.Value = "0";

                if (hdnadviserId.Value == "")
                    hdnadviserId.Value = "0";

                if (hdnrmId.Value == "")
                    hdnrmId.Value = "0";
            }

        }

        public void lnkBtnSCHEMEWISEAUM_OnClick(object sender, EventArgs e)
        {
            if (advisorVo.advisorId != 1000)
            {
                if (ddlFilterSelection.SelectedValue == "0")
                {
                    SetParameters();
                    showHideGrid("SchemeWise");

                    BindSCHEMEWISEAUMDetails();
                }
                else
                {
                    if (validateToFromDates())
                    {
                        SetParameters();
                        showHideGrid("SchemeWise");
                        BindSCHEMEWISEAUMDetailsDaterange();
                    }
                    else
                    {
                    }
                }
                lblMFMISType.Text = "SCHEME WISE AUM";


            }
            else
            {
                if (ddlFilterSelection.SelectedValue == "0")
                {
                    showHideGrid("SchemeWise");

                    BindSCHEMEWISEAUMDetails();
                }
                else
                {
                    if (validateToFromDates())
                    {
                        SetParameters();
                        showHideGrid("SchemeWise");
                        BindSCHEMEWISEAUMDetailsDaterange();
                    }
                    else
                    {

                    }
                }
                lblMFMISType.Text = "SCHEME WISE AUM";
            }
        }

        public bool validateToFromDates()
        {
            bool isvalid = false;
            dtFromHldDate = Convert.ToDateTime(rdpFrom.SelectedDate);
            dtToHldDate = Convert.ToDateTime(rdpTo.SelectedDate);

            if ((dtToHldDate == DateTime.MinValue))
                dtToHldDate = DateTime.Now;

            if ((dtFromHldDate == DateTime.MinValue))
                dtFromHldDate = DateTime.Now;

            dsValidToandFromDates = adviserMISBo.validateToFromDates(dtFromHldDate, dtToHldDate);
            //if (Convert.ToDateTime(dsValidToandFromDates.Tables[0].Rows[0][0].ToString())<= Convert.ToDateTime(rdpTo.SelectedDate.ToString()))
            rdpTo.MaxDate = Convert.ToDateTime(dsValidToandFromDates.Tables[0].Rows[0][0].ToString());
            rdpTo.SelectedDate = rdpTo.MaxDate;
            isvalid = true;


            return isvalid;
        }

        public void lnkBtnFOLIOWISEAUM_OnClick(object sender, EventArgs e)
        {
            if (advisorVo.advisorId != 1000)
            {
                if (ddlFilterSelection.SelectedValue == "0")
                {
                    SetParameters();
                    showHideGrid("FolioWise");

                    BindFOLIOWISEAUMDetails();
                }
                else
                {
                    if (validateToFromDates())
                    {
                        SetParameters();
                        showHideGrid("FolioWise");
                        BindFOLIOWISEAUMDetailsDaterange();
                    }
                    else
                    {
                    }
                }

                lblMFMISType.Text = "FOLIO WISE AUM";
            }
            else
            {
                if (ddlFilterSelection.SelectedValue == "0")
                {
                    showHideGrid("FolioWise");
                    if (validateDate())
                        BindFOLIOWISEAUMDetails();
                }
                else
                {
                    if (validateToFromDates())
                    {
                        showHideGrid("FolioWise");
                        BindFOLIOWISEAUMDetailsDaterange();
                    }
                    else
                    {
                    }
                }
                lblMFMISType.Text = "FOLIO WISE AUM";
            }
        }

        protected bool validateDate()
        {
            bool isValid = false;
            dsValuationDates = new DataSet();
            dsValuationDates = adviserMISBo.validateDate(Convert.ToDateTime(rdpFrom.SelectedDate), Convert.ToDateTime(rdpTo.SelectedDate));

            return isValid;
        }

        public void showHideGrid(string gridName)
        {
            if (gridName == "AMCWise")
            {
                divGvAmcWiseAUM.Visible = true;
                divGvFolioWiseAUM.Visible = false;
                divGvSchemeWiseAUM.Visible = false;
                imgBtnGvFolioWiseAUM.Visible = false;
                imgBtnGvSchemeWiseAUM.Visible = false;
            }
            else if (gridName == "FolioWise")
            {
                divGvAmcWiseAUM.Visible = false;
                divGvFolioWiseAUM.Visible = true;
                divGvSchemeWiseAUM.Visible = false;
                imgBtnGvAmcWiseAUM.Visible = false;
                imgBtnGvSchemeWiseAUM.Visible = false;
            }
            else if (gridName == "SchemeWise")
            {
                divGvAmcWiseAUM.Visible = false;
                divGvFolioWiseAUM.Visible = false;
                divGvSchemeWiseAUM.Visible = true;
                imgBtnGvFolioWiseAUM.Visible = false;
                imgBtnGvAmcWiseAUM.Visible = false;
            }

        }


        public void BindAMCWISEAUMDetails()
        {
            divGvSchemeWiseAUM.Visible = false;
            divGvFolioWiseAUM.Visible = false;
            divRgvFolioWiseAUM.Visible = false;
            divRgvSchemeWiseAUM.Visible = false;


            int rmgerId = 0;
            int brId = 0;
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            DataSet dsMISReport = null;
            double totalAum = 0;
            decimal TotalAumPercentage = 0;
            DateTime Valuation_Date = new DateTime();
            hdnType.Value = "0";
            //if (!string.IsNullOrEmpty(ddlBranch.SelectedValue))
            //    brId = int.Parse(ddlBranch.SelectedValue);
            //if (userType == "rm")
            //    rmgerId = rmVo.RMId;
            //if (userType == "bm")
            //{
            //    branchHeadId = rmVo.RMId;
            //}
            //else if (userType == "advisor")
            //{
            //    if (!string.IsNullOrEmpty(ddlRM.SelectedValue))
            //        rmgerId = int.Parse(ddlRM.SelectedValue);
            //}
            Valuation_Date = DateTime.Parse(txtDate.SelectedDate.ToString());
            if (txtDate.SelectedDate.ToString() != "dd/mm/yyyy")
            {
                Valuation_Date = Convert.ToDateTime(txtDate.SelectedDate);
                if (advisorVo.advisorId != 1000)
                {
                    if (userType == "rm")
                    {
                        dsMISReport = adviserMISBo.GetAMCwiseAUMForRM(int.Parse(hdnrmId.Value), Valuation_Date);
                    }
                    else if (userType == "advisor")
                    {
                        dsMISReport = adviserMISBo.GetAMCwiseAUMForAdviser(int.Parse(hdnadviserId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnrmId.Value), Valuation_Date, int.Parse(ddlType.SelectedValue));
                    }
                    else if (userType == "bm")
                    {
                        if (string.IsNullOrEmpty(hdnadviserId.Value))
                            hdnadviserId.Value = "0";
                        dsMISReport = adviserMISBo.GetAUMForBM(advisorId, int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), Valuation_Date, int.Parse(hdnType.Value), AmcCode, SchemeCode, advisorVo.A_AgentCodeBased);
                    }
                    if (userType == "associates")
                    {
                        dsMISReport = adviserMISBo.GetAMCwiseAUMForAssociate(int.Parse(hdnAgentId.Value), Valuation_Date);
                    }
                }
                else
                {
                    dsMISReport = adviserMISBo.GetAMCwiseAUMForAdviser(Convert.ToInt32(ddlAdviser.SelectedValue), 0, 0, Valuation_Date, int.Parse(ddlType.SelectedValue));
                }
            }
            if (dsMISReport.Tables.Count == 0 || dsMISReport.Tables[0].Rows.Count < 1)
            {
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = "No records found for  AMC WISE AUM..";
                divGvAmcWiseAUM.Visible = false;
            }
            else
            {
                imgBtnGvAmcWiseAUM.Visible = true;
                lblErrorMsg.Visible = false;
                double aum = 0;
                decimal AumPercentage = 0;
                for (int i = 0; i < dsMISReport.Tables[0].Rows.Count; i++)
                {
                    dsMISReport.Tables[0].Rows[i]["AUM"] = decimal.Parse(dsMISReport.Tables[0].Rows[i]["AUM"].ToString());
                }
                foreach (DataRow dr in dsMISReport.Tables[0].Rows)
                {
                    aum = Convert.ToDouble(dr["AUM"].ToString());
                    AumPercentage = decimal.Parse(dr["Percentage"].ToString());
                    totalAum = totalAum + aum;
                    TotalAumPercentage = TotalAumPercentage + AumPercentage;

                }
                DataTable dtMISReport = new DataTable();
                dtMISReport.Columns.Add("AMC");
                dtMISReport.Columns.Add("AMCCode");
                dtMISReport.Columns.Add("AUM", typeof(double));
                dtMISReport.Columns.Add("Percentage", typeof(double));
                dtMISReport.Columns.Add("customer");
                DataRow drMISReport;
                for (int i = 0; i < dsMISReport.Tables[0].Rows.Count; i++)
                {
                    drMISReport = dtMISReport.NewRow();
                    drMISReport[0] = dsMISReport.Tables[0].Rows[i]["AMC"].ToString();
                    drMISReport[1] = dsMISReport.Tables[0].Rows[i]["AMCCode"].ToString();
                    drMISReport[2] = dsMISReport.Tables[0].Rows[i]["AUM"].ToString();
                    drMISReport[3] = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i]["Percentage"].ToString()), 2).ToString();
                    drMISReport[4] = dsMISReport.Tables[0].Rows[i]["customer"].ToString();
                    dtMISReport.Rows.Add(drMISReport);
                }
                gvAmcWiseAUM.DataSource = dtMISReport;
                gvAmcWiseAUM.DataBind();

                if (Cache["gvAmcWiseAUMDetails" + advisorVo.advisorId.ToString()] == null)
                {
                    Cache.Insert("gvAmcWiseAUMDetails" + advisorVo.advisorId.ToString(), dtMISReport);
                }
                else
                {
                    Cache.Remove("gvAmcWiseAUMDetails" + advisorVo.advisorId.ToString());
                    Cache.Insert("gvAmcWiseAUMDetails" + advisorVo.advisorId.ToString(), dtMISReport);
                }
            }
        }

        public void BindSCHEMEWISEAUMDetails()
        {
            divGvAmcWiseAUM.Visible = false;
            divGvFolioWiseAUM.Visible = false;
            divRgvFolioWiseAUM.Visible = false;
            divRgvSchemeWiseAUM.Visible = false;
            HideGridColumnonAgenetBasedCode();
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            DataSet dsMISReport = null;
            int rmId = 0;
            int branchId = 0;
            hdnType.Value = "1";
            //if (!string.IsNullOrEmpty(ddlBranch.SelectedValue))
            //    branchId = int.Parse(ddlBranch.SelectedValue);
            //if (userType == "rm")
            //    rmId = rmVo.RMId;
            //else if (userType == "advisor")
            //{
            //    if (ddlRM.SelectedValue != "0")
            //        rmId = int.Parse(ddlRM.SelectedValue);
            //}
            DateTime Valuation_Date = Convert.ToDateTime(txtDate.SelectedDate.ToString());
            if (advisorVo.advisorId != 1000)
            {
                if (userType == "rm")
                {
                    dsMISReport = adviserMISBo.GetAMCSchemewiseAUMForRM(int.Parse(hdnrmId.Value), Valuation_Date, AmcCode);
                }
                else if (userType == "bm")
                {
                    dsMISReport = adviserMISBo.GetAUMForBM(advisorId, int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), Valuation_Date, int.Parse(hdnType.Value), AmcCode, SchemeCode, advisorVo.A_AgentCodeBased);

                }
                else if (userType == "advisor")
                {
                    dsMISReport = adviserMISBo.GetAMCSchemewiseAUMForAdviser(advisorId, int.Parse(hdnbranchId.Value), int.Parse(hdnrmId.Value), Valuation_Date, AmcCode, AgentCode, advisorVo.A_AgentCodeBased, int.Parse(ddlType.SelectedValue));

                }
                if (userType == "associates")
                {
                    dsMISReport = adviserMISBo.GetAMCSchemewiseAUMForAssociate(advisorId, Valuation_Date, AmcCode, AgentCode, advisorVo.A_AgentCodeBased);
                }
            }
            else
                dsMISReport = adviserMISBo.GetAMCSchemewiseAUMForAdviser(Convert.ToInt32(ddlAdviser.SelectedValue), 0, 0, Valuation_Date, AmcCode, AgentCode, advisorVo.A_AgentCodeBased, int.Parse(ddlType.SelectedValue));

            if (dsMISReport.Tables.Count == 0 || dsMISReport.Tables[0].Rows.Count < 1)
            {
                lblErrorMsg.Text = "No records found for SCHEME WISE AUM..";
                lblErrorMsg.Visible = true;
                divGvSchemeWiseAUM.Visible = false;
            }
            else
            {

                imgBtnGvSchemeWiseAUM.Visible = true;
                lblErrorMsg.Visible = false;
                gvSchemeWiseAUM.DataSource = dsMISReport.Tables[0];
                gvSchemeWiseAUM.DataBind();
                if (Cache["gvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString()] == null)
                {
                    Cache.Insert("gvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString(), dsMISReport.Tables[0]);
                }
                else
                {
                    Cache.Remove("gvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString());
                    Cache.Insert("gvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString(), dsMISReport.Tables[0]);
                }
            }
        }

        public void BindFOLIOWISEAUMDetails()
        {
            divGvAmcWiseAUM.Visible = false;
            divGvSchemeWiseAUM.Visible = false;
            divRgvFolioWiseAUM.Visible = false;
            divRgvSchemeWiseAUM.Visible = false;
            HideGridColumnonAgenetBasedCode();
            dtFromHldDate = Convert.ToDateTime(rdpFrom.SelectedDate);
            dtTo = Convert.ToDateTime(rdpTo.SelectedDate);
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            DataSet dsMISReport = null;
            hdnType.Value = "2";
            //int.TryParse(ddlBranch.SelectedValue, out branchId);
            //if (userType == "rm")
            //    rmId = rmVo.RMId;
            //else if (userType == "advisor")
            //{
            //    int.TryParse(ddlRM.SelectedValue, out rmId);
            //}

            Valuationdate = DateTime.Parse(txtDate.SelectedDate.ToString());
            if (advisorVo.advisorId != 1000)
            {
                if (userType == "rm")
                {
                    dsMISReport = adviserMISBo.GetCustomerAMCSchemewiseAUMForRM(int.Parse(hdnrmId.Value), Valuationdate, SchemeCode);
                }
                else if (userType == "bm")
                {
                    //if (ddlBranch.SelectedValue != "0")
                    //    branchId = int.Parse(ddlBranch.SelectedValue);
                    //if (ddlRM.SelectedValue != "0")
                    //    rmId = int.Parse(ddlRM.SelectedValue);

                    dsMISReport = adviserMISBo.GetAUMForBM(advisorId, int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), Valuationdate, int.Parse(hdnType.Value), AmcCode, SchemeCode, advisorVo.A_AgentCodeBased);
                }
                else if (userType == "advisor")
                {
                    AgentCode = "0";
                    dsMISReport = adviserMISBo.GetCustomerAMCSchemewiseAUMForAdviser(int.Parse(hdnadviserId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnrmId.Value), Valuationdate, SchemeCode, AgentCode, advisorVo.A_AgentCodeBased, int.Parse(ddlType.SelectedValue));
                }
                if (userType == "associates")
                {
                    dsMISReport = adviserMISBo.GetCustomerAMCSchemewiseAUMForAssociate(advisorId, Valuationdate, SchemeCode, AgentCode, advisorVo.A_AgentCodeBased);
                }
            }
            else
                dsMISReport = adviserMISBo.GetCustomerAMCSchemewiseAUMForAdviser(int.Parse(ddlAdviser.SelectedValue), 0, 0, Valuationdate, SchemeCode, AgentCode, advisorVo.A_AgentCodeBased, int.Parse(ddlType.SelectedValue));

            if (dsMISReport.Tables.Count == 0 || dsMISReport.Tables[0].Rows.Count < 1)
            {
                lblErrorMsg.Text = "No records found for FOLIO WISE AUM..";
                lblErrorMsg.Visible = true;
                divGvFolioWiseAUM.Visible = false;
            }
            else
            {
                imgBtnGvFolioWiseAUM.Visible = true;
                lblErrorMsg.Visible = false;
                gvFolioWiseAUM.DataSource = dsMISReport.Tables[0];
                gvFolioWiseAUM.DataBind();
                if (Cache["gvFolioWiseAUMDetails" + advisorVo.advisorId.ToString()] == null)
                {
                    Cache.Insert("gvFolioWiseAUMDetails" + advisorVo.advisorId.ToString(), dsMISReport.Tables[0]);
                }
                else
                {
                    Cache.Remove("gvFolioWiseAUMDetails" + advisorVo.advisorId.ToString());
                    Cache.Insert("gvFolioWiseAUMDetails" + advisorVo.advisorId.ToString(), dsMISReport.Tables[0]);
                }
            }
        }


        #region date range
        public void BindSCHEMEWISEAUMDetailsDaterange()
        {
            divGvAmcWiseAUM.Visible = false;
            divGvFolioWiseAUM.Visible = false;
            divGvSchemeWiseAUM.Visible = false;
            divRgvFolioWiseAUM.Visible = false;
            HideGridColumnonAgenetBasedCode();
            dtFromHldDate = Convert.ToDateTime(rdpFrom.SelectedDate);
            dtTo = Convert.ToDateTime(rdpTo.SelectedDate);
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            DataSet dsMISReport = null;
            int rmId = 0;
            int branchId = 0;
            hdnType.Value = "1";
            //if (!string.IsNullOrEmpty(ddlBranch.SelectedValue))
            //    branchId = int.Parse(ddlBranch.SelectedValue);
            //if (userType == "rm")
            //    rmId = rmVo.RMId;
            //else if (userType == "advisor")
            //{
            //    if (ddlRM.SelectedValue != "0")
            //        rmId = int.Parse(ddlRM.SelectedValue);
            //}
            DateTime Valuation_Date = Convert.ToDateTime(txtDate.SelectedDate.ToString());
            if (advisorVo.advisorId != 1000)
            {
                if (userType == "rm")
                {
                    dsMISReport = adviserMISBo.GetAMCSchemewiseAUMForRMForDateRange(int.Parse(hdnrmId.Value), Valuation_Date, AmcCode, dtFromHldDate, dtToHldDate);
                }
                else if (userType == "bm")
                {
                    dsMISReport = adviserMISBo.GetAUMForBMForDateRange(advisorId, int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), Valuation_Date, int.Parse(hdnType.Value), AmcCode, SchemeCode, dtFromHldDate, dtToHldDate, advisorVo.A_AgentCodeBased);

                }
                else if (userType == "advisor" || userType == "associates")
                {
                    dsMISReport = adviserMISBo.GetAMCSchemewiseAUMForAdviserForDateRange(advisorId, int.Parse(hdnbranchId.Value), int.Parse(hdnrmId.Value), Valuation_Date, AmcCode, dtFromHldDate, dtToHldDate, AgentCode, advisorVo.A_AgentCodeBased, userType);

                }
            }
            else
                dsMISReport = adviserMISBo.GetAMCSchemewiseAUMForAdviserForDateRange(Convert.ToInt32(ddlAdviser.SelectedValue), 0, 0, Valuation_Date, AmcCode, dtFromHldDate, dtToHldDate, AgentCode, advisorVo.A_AgentCodeBased, userType);

            if (dsMISReport.Tables.Count == 0 || dsMISReport.Tables[0].Rows.Count < 1)
            {
                lblErrorMsg.Text = "No records found for SCHEME WISE AUM..";
                lblErrorMsg.Visible = true;
                divGvSchemeWiseAUM.Visible = false;
            }
            else
            {
                imgBtnGvSchemeWiseAUM.Visible = true;
                lblErrorMsg.Visible = false;
                rgvSchemeWiseAUM.DataSource = dsMISReport;
                rgvSchemeWiseAUM.DataBind();
                divRgvSchemeWiseAUM.Visible = true;


                if (Cache["rgvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString()] == null)
                {
                    Cache.Insert("rgvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString(), dsMISReport);
                }
                else
                {
                    Cache.Remove("rgvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString());
                    Cache.Insert("rgvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString(), dsMISReport);
                }
            }
        }

        public void BindFOLIOWISEAUMDetailsDaterange()
        {
            divGvAmcWiseAUM.Visible = false;
            divGvFolioWiseAUM.Visible = false;
            divGvSchemeWiseAUM.Visible = false;
            divRgvSchemeWiseAUM.Visible = false;
            HideGridColumnonAgenetBasedCode();
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            DataSet dsMISReport = null;
            hdnType.Value = "2";
            dtFromHldDate = Convert.ToDateTime(rdpFrom.SelectedDate);
            dtTo = Convert.ToDateTime(rdpTo.SelectedDate);
            //int.TryParse(ddlBranch.SelectedValue, out branchId);
            //if (userType == "rm")
            //    rmId = rmVo.RMId;
            //else if (userType == "advisor")
            //{
            //    int.TryParse(ddlRM.SelectedValue, out rmId);
            //}
            Valuationdate = DateTime.Parse(txtDate.SelectedDate.ToString());
            if (advisorVo.advisorId != 1000)
            {
                if (userType == "rm")
                {
                    dsMISReport = adviserMISBo.GetCustomerAMCSchemewiseAUMForRMForDateRange(int.Parse(hdnrmId.Value), Valuationdate, SchemeCode, dtFromHldDate, dtToHldDate);
                }
                else if (userType == "bm")
                {
                    //if (ddlBranch.SelectedValue != "0")
                    //    branchId = int.Parse(ddlBranch.SelectedValue);
                    //if (ddlRM.SelectedValue != "0")
                    //    rmId = int.Parse(ddlRM.SelectedValue);

                    dsMISReport = adviserMISBo.GetAUMForBMForDateRange(advisorId, int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), Valuationdate, int.Parse(hdnType.Value), AmcCode, SchemeCode, dtFromHldDate, dtToHldDate, advisorVo.A_AgentCodeBased);
                }
                else if (userType == "advisor" || userType == "associates")
                {
                    dsMISReport = adviserMISBo.GetCustomerAMCSchemewiseAUMForAdviserForDateRange(advisorId, int.Parse(hdnbranchId.Value), int.Parse(hdnrmId.Value), Valuationdate, SchemeCode, dtFromHldDate, dtToHldDate, AgentCode, advisorVo.A_AgentCodeBased, userType);
                }
            }
            else
                dsMISReport = adviserMISBo.GetCustomerAMCSchemewiseAUMForAdviserForDateRange(int.Parse(ddlAdviser.SelectedValue), 0, 0, Valuationdate, SchemeCode, dtFromHldDate, dtToHldDate, AgentCode, advisorVo.A_AgentCodeBased, userType);

            if (dsMISReport.Tables.Count == 0 || dsMISReport.Tables[0].Rows.Count < 1)
            {
                lblErrorMsg.Text = "No records found for FOLIO WISE AUM..";
                lblErrorMsg.Visible = true;
                divGvFolioWiseAUM.Visible = false;
            }
            else
            {
                imgBtnGvFolioWiseAUM.Visible = true;
                lblErrorMsg.Visible = false;
                rgvFolioWiseAUM.DataSource = dsMISReport;
                rgvFolioWiseAUM.DataBind();
                divRgvFolioWiseAUM.Visible = true;

                if (Cache["rgvFolioWiseAUMDetails" + advisorVo.advisorId.ToString()] == null)
                {
                    Cache.Insert("rgvFolioWiseAUMDetails" + advisorVo.advisorId.ToString(), dsMISReport);
                }
                else
                {
                    Cache.Remove("rgvFolioWiseAUMDetails" + advisorVo.advisorId.ToString());
                    Cache.Insert("rgvFolioWiseAUMDetails" + advisorVo.advisorId.ToString(), dsMISReport);
                }
            }
        }

        #endregion




        #endregion

        #region need data source

        protected void gvAmcWiseAUM_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtProcessLogDetails = new DataTable();
            dtProcessLogDetails = (DataTable)Cache["gvAmcWiseAUMDetails" + advisorVo.advisorId.ToString()];
            gvAmcWiseAUM.DataSource = dtProcessLogDetails;
        }

        protected void gvFolioWiseAUM_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtProcessLogDetails = new DataTable();
            dtProcessLogDetails = (DataTable)Cache["gvFolioWiseAUMDetails" + advisorVo.advisorId.ToString()];
            gvFolioWiseAUM.DataSource = dtProcessLogDetails;
        }

        protected void gvSchemeWiseAUM_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtProcessLogDetails = new DataTable();
            dtProcessLogDetails = (DataTable)Cache["gvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString()];
            gvSchemeWiseAUM.DataSource = dtProcessLogDetails;
        }

        #endregion

        protected void hidingConrolForRMAndBMLogin(string userType)
        {
            if (userType == "rm" || userType == "associates")
            {
                lblBranch.Visible = false;
                ddlBranch.Visible = false;
                lblRM.Visible = false;
                ddlRM.Visible = false;
            }
            else
            {
                lblBranch.Visible = true;
                ddlBranch.Visible = true;
                lblRM.Visible = true;
                ddlRM.Visible = true;
            }
        }

        protected void gvFolioWiseAUM_PreRender(object sender, EventArgs e)
        {
            if (userType == "rm")
            {
                foreach (GridColumn column in gvFolioWiseAUM.Columns)
                {
                    if (column.UniqueName == "RmName" || column.UniqueName == "BranchName")
                    {
                        column.Visible = false;
                    }

                }
            }
        }

        #region export for all grids

        public void imgBtnGvAmcWiseAUM_OnClick(object sender, ImageClickEventArgs e)
        {
            

            DataTable dt = (DataTable)Cache["gvAmcWiseAUMDetails" + advisorVo.advisorId.ToString()];
            CommonProgrammingBo commonProgrammingBo = new CommonProgrammingBo();
            Dictionary<string, string> dHeaderText = new Dictionary<string, string>();
            for (int i = 0; i < gvAmcWiseAUM.MasterTableView.Columns.Count; i++)
            {
                if (gvAmcWiseAUM.Columns[i].Visible == true)
                    dHeaderText.Add(rgvFolioWiseAUM.Columns[i].UniqueName, gvAmcWiseAUM.MasterTableView.Columns[i].HeaderText);
            }
            ExcelToExportData(dt, "AmcWise AUM Details");


        }

        public void imgBtnGvSchemeWiseAUM_OnClick(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Cache["gvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString()];
            CommonProgrammingBo commonProgrammingBo = new CommonProgrammingBo();
            Dictionary<string, string> dHeaderText = new Dictionary<string, string>();
            for (int i = 0; i < gvSchemeWiseAUM.MasterTableView.Columns.Count; i++)
            {
                if (gvSchemeWiseAUM.Columns[i].Visible == true)
                    dHeaderText.Add(gvSchemeWiseAUM.Columns[i].UniqueName, gvSchemeWiseAUM.MasterTableView.Columns[i].HeaderText);
            }
            dt = commonProgrammingBo.getHeaderNameNValue(dt, dHeaderText);
            ExcelToExportData(dt, "SchemeWise AUM Details");
        }

        public void imgBtnGvFolioWiseAUM_OnClick(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Cache["gvFolioWiseAUMDetails" + advisorVo.advisorId.ToString()];
            CommonProgrammingBo commonProgrammingBo = new CommonProgrammingBo();
            Dictionary<string, string> dHeaderText = new Dictionary<string, string>();
            for (int i = 0; i < rgvFolioWiseAUM.MasterTableView.Columns.Count; i++)
            {
                //if (rgvFolioWiseAUM.Columns[i].Visible == true)
                //    dHeaderText.Add(rgvFolioWiseAUM.Columns[i].UniqueName, rgvFolioWiseAUM.MasterTableView.Columns[i].HeaderText);
            }
            dt = commonProgrammingBo.getHeaderNameNValue(dt, dHeaderText);


           
            
            ExcelToExportData(dt, "FolioWise AUM Details");

        }

        private void ExcelToExportData(DataTable dt, string fileName)
        {

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName + ".xls"));
            Response.ContentType = "application/ms-excel";
            string str = string.Empty;
            foreach (DataColumn dtcol in dt.Columns)
            {
                Response.Write(str + dtcol.ColumnName);
                str = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in dt.Rows)
            {
                str = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Response.Write(str + Convert.ToString(dr[j]));
                    str = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }


        #endregion

        #region item command
        protected void gvAmcWiseAUM_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() != "Filter")
                {
                    if (e.CommandName.ToString() != "Sort")
                    {
                        if (e.CommandName.ToString() != "Page")
                        {
                            if (e.CommandName.ToString() != "ChangePageSize")
                            {
                                if (e.CommandName == "Select")
                                {
                                    GridDataItem gvr = (GridDataItem)e.Item;
                                    int selectedRow = gvr.ItemIndex + 1;
                                    AmcCode = int.Parse(gvr.GetDataKeyValue("AMCCode").ToString());

                                    showHideGrid("SchemeWise");
                                    BindSCHEMEWISEAUMDetails();
                                    lblMFMISType.Text = "AMC WISE AUM // SCHEME WISE AUM";
                                }
                            }
                        }
                    }
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolioUnrealized_RowCommand()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        protected void gvFolioWiseAUM_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() != "Filter")
                {
                    if (e.CommandName.ToString() != "Sort")
                    {
                        if (e.CommandName.ToString() != "Page")
                        {
                            if (e.CommandName.ToString() != "ChangePageSize")
                            {
                                GridDataItem gvr = (GridDataItem)e.Item;
                                int selectedRow = gvr.ItemIndex + 1;
                                int folio = int.Parse(gvr.GetDataKeyValue("CMFA_AccountId").ToString());
                                int SchemePlanCode = int.Parse(gvr.GetDataKeyValue("SchemePlanCode").ToString());
                                int AMC = int.Parse(gvr.GetDataKeyValue("AmcCode").ToString());
                                int SubBrokerCode = int.Parse(gvr.GetDataKeyValue("CMFA_SubBrokerCode").ToString());

                                if (e.CommandName == "Select")
                                {
                                    string name = "Select";
                                    Response.Redirect("ControlHost.aspx?pageid=RMMultipleTransactionView&folionum=" + folio + "&SchemePlanCode=" + SchemePlanCode + "&AMC=" + AMC + "&name=" + name + "&subBrokerCode=" + SubBrokerCode + "", false);
                                }
                            }
                        }
                    }
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolioUnrealized_RowCommand()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvSchemeWiseAUM_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void gvFolioWiseAUM_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        public void getValidAUMDate()
        {
            DateTime dtFromDate = new DateTime();
            DateTime dtToDate = new DateTime();


            adviserMISBo.getValidAUMDate(dtFrom, dtTo);
        }

        protected void BindAdviserDropDownList()
        {
            DataTable dtAdviserList = new DataTable();
            dtAdviserList = superAdminOpsBo.BindAdviserForUpload();

            if (dtAdviserList.Rows.Count > 0)
            {
                ddlAdviser.DataSource = dtAdviserList;
                ddlAdviser.DataTextField = "A_OrgName";
                ddlAdviser.DataValueField = "A_AdviserId";
                ddlAdviser.DataBind();
            }
            ddlAdviser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }


        protected void gvSchemeWiseAUM_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() != "Filter")
                {
                    if (e.CommandName.ToString() != "Sort")
                    {
                        if (e.CommandName.ToString() != "Page")
                        {
                            if (e.CommandName.ToString() != "ChangePageSize")
                            {
                                if (e.CommandName == "Select")
                                {
                                    GridDataItem gvr = (GridDataItem)e.Item;
                                    int selectedRow = gvr.ItemIndex + 1;
                                    SchemeCode = int.Parse(gvr.GetDataKeyValue("SchemePlanCode").ToString());
                                    showHideGrid("FolioWise");
                                    BindFOLIOWISEAUMDetails();
                                    lblMFMISType.Text = "AMC WISE AUM // SCHEME WISE AUM // FOLIO WISE AUM";
                                }
                            }
                        }
                    }
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolioUnrealized_RowCommand()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        #endregion

        #region turn over summary

        //protected void lnkBtnTURNOVERAUM_OnClick(object sender, EventArgs e)
        //{
        //    rWTurnOverAUM.Visible = true;
        //    rWTurnOverAUM.VisibleOnPageLoad = true;
        //    rWTurnOverAUM.Width = 500;
        //    divGvTurnOverSummary.Visible = false;
        //    rbtnDate_CheckedChanged(sender, e);
        //}

        //protected void rbtnDate_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rbtnPickDate.Checked == true)
        //    {
        //        divPickAdateRange.Visible = true;
        //        divPickAPeriod.Visible = false;
        //    }
        //    else if (rbtnPickPeriod.Checked == true)
        //    {
        //        divPickAdateRange.Visible = false;
        //        divPickAPeriod.Visible = true;
        //        BindPeriodDropDown();
        //    }
        //}

        //private void BindPeriodDropDown()
        //{
        //    DataTable dtPeriod;
        //    dtPeriod = XMLBo.GetDatePeriod(path);

        //    ddlPeriod.DataSource = dtPeriod;
        //    ddlPeriod.DataTextField = "PeriodType";
        //    ddlPeriod.DataValueField = "PeriodCode";
        //    ddlPeriod.DataBind();
        //    ddlPeriod.Items.Insert(0, new ListItem("Select a Period", "Select a Period"));
        //    ddlPeriod.Items.RemoveAt(15);
        //}

        protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            //if (rbtnPickPeriod.Checked)
            //{

            //    if (ddlPeriod.SelectedIndex != 0)
            //    {
            //        dtBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue, out dtFrom, out dtTo);
            //    }
            //}

            //if (rbtnPickDate.Checked)
            //{
            //    if (txtFromDate.Text != null && txtFromDate.Text != "")
            //    {
            //        convertedFromDate = Convert.ToDateTime(txtFromDate.Text.Trim(), ci);
            //    }
            //    else
            //    {
            //        convertedFromDate = DateTime.MinValue;
            //    }
            //    if (txtToDate.Text != null && txtToDate.Text != "")
            //    {
            //        convertedToDate = Convert.ToDateTime(txtToDate.Text.Trim(), ci);
            //    }
            //    else
            //    {
            //        convertedToDate = DateTime.MinValue;
            //    }
            //}



            /* For BM Branch wise MIS */
            //if (userType == "advisor" || userType == "ops")
            //{
            //    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
            //    {
            //        hdnbranchId.Value = "0";
            //        hdnAll.Value = "0";
            //        hdnrmId.Value = "0";
            //        if (rbtnPickPeriod.Checked)
            //            this.BindTurnOverSummaryDetails(dtFrom, dtTo);
            //        else
            //            this.BindTurnOverSummaryDetails(convertedFromDate, convertedToDate);
            //    }
            //    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
            //    {

            //        hdnbranchId.Value = ddlBranch.SelectedValue;
            //        hdnAll.Value = "1";
            //        hdnrmId.Value = "0";
            //        if (rbtnPickPeriod.Checked)
            //            this.BindTurnOverSummaryDetails(dtFrom, dtTo);
            //        else
            //            this.BindTurnOverSummaryDetails(convertedFromDate, convertedToDate);
            //    }
            //    else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
            //    {
            //        hdnbranchId.Value = "0";
            //        hdnAll.Value = "2";
            //        hdnrmId.Value = ddlRM.SelectedValue;
            //        if (rbtnPickPeriod.Checked)
            //            this.BindTurnOverSummaryDetails(dtFrom, dtTo);
            //        else
            //            this.BindTurnOverSummaryDetails(convertedFromDate, convertedToDate);
            //    }
            //    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
            //    {
            //        hdnbranchId.Value = ddlBranch.SelectedValue;
            //        hdnAll.Value = "3";
            //        hdnrmId.Value = ddlRM.SelectedValue;
            //        if (rbtnPickPeriod.Checked)
            //            this.BindTurnOverSummaryDetails(dtFrom, dtTo);
            //        else
            //            this.BindTurnOverSummaryDetails(convertedFromDate, convertedToDate);
            //    }


            //}
            //else if (userType == "bm")
            //{
            //    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
            //    {
            //        hdnbranchId.Value = "0";
            //        hdnbranchHeadId.Value = bmID.ToString();
            //        hdnAll.Value = "2";
            //        hdnrmId.Value = "0";
            //        //dsMfMIS = adviserMFMIS.GetMFMIS(userType, ID, dtFrom, dtTo, 0, 0, int.Parse(hdnbranchHeadId.Value.ToString()), 2);
            //        if (rbtnPickPeriod.Checked)
            //            this.BindTurnOverSummaryDetails(dtFrom, dtTo);
            //        else
            //            this.BindTurnOverSummaryDetails(convertedFromDate, convertedToDate);
            //    }
            //    else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
            //    {
            //        hdnbranchId.Value = "0";
            //        hdnbranchHeadId.Value = bmID.ToString();
            //        hdnAll.Value = "3";
            //        hdnrmId.Value = ddlRM.SelectedValue;

            //        //dsMfMIS = adviserMFMIS.GetMFMIS(userType, ID, dtFrom, dtTo, int.Parse(hdnrmId.Value.ToString()), 0, int.Parse(hdnbranchHeadId.Value.ToString()), 3);
            //        if (rbtnPickPeriod.Checked)
            //            this.BindTurnOverSummaryDetails(dtFrom, dtTo);
            //        else
            //            this.BindTurnOverSummaryDetails(convertedFromDate, convertedToDate);
            //    }
            //    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
            //    {
            //        hdnbranchId.Value = ddlBranch.SelectedValue;
            //        hdnbranchHeadId.Value = bmID.ToString();
            //        hdnAll.Value = "1";
            //        //dsMfMIS = adviserMFMIS.GetMFMIS(userType, ID, dtFrom, dtTo, 0, int.Parse(hdnbranchId.Value.ToString()), 0, 1);
            //        if (rbtnPickPeriod.Checked)
            //            this.BindTurnOverSummaryDetails(dtFrom, dtTo);
            //        else
            //            this.BindTurnOverSummaryDetails(convertedFromDate, convertedToDate);
            //    }
            //    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
            //    {
            //        hdnbranchId.Value = ddlBranch.SelectedValue;
            //        hdnbranchHeadId.Value = bmID.ToString();
            //        hdnAll.Value = "0";
            //        hdnrmId.Value = ddlRM.SelectedValue;

            //        //dsMfMIS = adviserMFMIS.GetMFMIS(userType, ID, dtFrom, dtTo, int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), 0, 0);
            //        if (rbtnPickPeriod.Checked)
            //            this.BindTurnOverSummaryDetails(dtFrom, dtTo);
            //        else
            //            this.BindTurnOverSummaryDetails(convertedFromDate, convertedToDate);
            //    }
            //}
            //else if (userType == "rm")
            //{
            //    if (rbtnPickPeriod.Checked)
            //        this.BindTurnOverSummaryDetails(dtFrom, dtTo);
            //    else
            //        this.BindTurnOverSummaryDetails(convertedFromDate, convertedToDate);

            //}
            //rWTurnOverAUM.VisibleOnPageLoad = false;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //rWTurnOverAUM.VisibleOnPageLoad = false;
        }
        #endregion

        protected void rgvSchemeWiseAUM_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() != "Filter")
                {
                    if (e.CommandName.ToString() != "Sort")
                    {
                        if (e.CommandName.ToString() != "Page")
                        {
                            if (e.CommandName.ToString() != "ChangePageSize")
                            {
                                if (e.CommandName == "Select")
                                {
                                    GridDataItem gvr = (GridDataItem)e.Item;
                                    int selectedRow = gvr.ItemIndex + 1;
                                    SchemeCode = int.Parse(gvr.GetDataKeyValue("SchemePlanCode").ToString());
                                    showHideGrid("FolioWise");
                                    BindFOLIOWISEAUMDetails();
                                    lblMFMISType.Text = "FOLIO WISE AUM";
                                }
                            }
                        }
                    }
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolioUnrealized_RowCommand()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void rgvFolioWiseAUM_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() != "Filter")
                {
                    if (e.CommandName.ToString() != "Sort")
                    {
                        if (e.CommandName.ToString() != "Page")
                        {
                            if (e.CommandName.ToString() != "ChangePageSize")
                            {
                                GridDataItem gvr = (GridDataItem)e.Item;
                                int selectedRow = gvr.ItemIndex + 1;
                                int folio = int.Parse(gvr.GetDataKeyValue("CMFA_AccountId").ToString());
                                if (e.CommandName == "Select")
                                {
                                    Response.Redirect("ControlHost.aspx?pageid=RMMultipleTransactionView&folionum=" + folio + "", false);
                                }
                            }
                        }
                    }
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolioUnrealized_RowCommand()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void rgvSchemeWiseAUM_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dtProcessLogDetails = new DataSet();
            dtProcessLogDetails = (DataSet)Cache["rgvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString()];
            rgvSchemeWiseAUM.DataSource = dtProcessLogDetails;
        }

        protected void rgvSchemeWiseAUM_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void rgvFolioWiseAUM_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                LinkButton lbtnMarkAsReject = (LinkButton)dataItem.FindControl("lnkprAmc");
                if (ddlType.SelectedValue == "1")
                {
                    lbtnMarkAsReject.Enabled = false;
                    lbtnMarkAsReject.ForeColor = Color.Black;
                    gvFolioWiseAUM.MasterTableView.GetColumn("CMFA_SubBrokerCode").Visible = false;
                    gvFolioWiseAUM.MasterTableView.GetColumn("AssociatesName").Visible = false;
                    gvFolioWiseAUM.MasterTableView.GetColumn("Titles").Visible = false;
                    gvFolioWiseAUM.MasterTableView.GetColumn("ReportingManagerName").Visible = false;
                    gvFolioWiseAUM.MasterTableView.GetColumn("UserType").Visible = false;
                    gvFolioWiseAUM.MasterTableView.GetColumn("ChannelName").Visible = false;
                    gvFolioWiseAUM.MasterTableView.GetColumn("ClusterManager").Visible = false;
                    gvFolioWiseAUM.MasterTableView.GetColumn("AreaManager").Visible = false;
                    gvFolioWiseAUM.MasterTableView.GetColumn("ZonalManagerName").Visible = false;
                    gvFolioWiseAUM.MasterTableView.GetColumn("DeputyHead").Visible = false;


                }
            }
        }
        protected void rgvFolioWiseAUM_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtProcessLogDetails = new DataTable();
            dtProcessLogDetails = (DataTable)Cache["rgvFolioWiseAUMDetails" + advisorVo.advisorId.ToString()];
            rgvFolioWiseAUM.DataSource = dtProcessLogDetails;
        }

        protected void rgvFolioWiseAUM_PreRender(object sender, EventArgs e)
        {
            if (userType == "rm")
            {
                foreach (GridColumn column in gvFolioWiseAUM.Columns)
                {
                    if (column.UniqueName == "RmName" || column.UniqueName == "BranchName")
                    {
                        column.Visible = false;
                    }

                }
            }
        }

    }
}
