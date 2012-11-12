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

namespace WealthERP.BusinessMIS
{
    public partial class MutualFundMIS : System.Web.UI.UserControl
    {
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

        int AmcCode = 0;
        int SchemeCode = 0;
        string type;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            rmVo = new RMVo();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            advisorBranchVo = (AdvisorBranchVo)Session[SessionContents.AdvisorBranchVo];
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

            if (!IsPostBack)
            {
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
                    BindRMforBranchDropdown(0, bmID,1);

                    hdnbranchHeadId.Value = ddlBranch.SelectedValue;
                }

              //LatestValuationdate = adviserMISBo.GetLatestValuationDateFromHistory(advisorId,"MF");
              //hdnValuationDate.Value = LatestValuationdate.ToString("MM/dd/yyyy");  
                LatestValuationdate = adviserMISBo.GetLatestValuationDateFromHistory(advisorVo.advisorId, "MF");
                vlndte = LatestValuationdate;
                hdnValuationDate.Value = LatestValuationdate.ToString("MM/dd/yyyy");
                lblValDt.Text = vlndte.ToShortDateString();
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
                BindRMforBranchDropdown(0, bmID,1);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0,0);
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
       
            SetParameters();
            showHideGrid("AMCWise");
            BindAMCWISEAUMDetails();
            lblMFMISType.Text = "AMC WISE AUM";
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
            else if ( userType == "rm")
            {
                hdnrmId.Value = rmVo.RMId.ToString();
                hdnAll.Value = "0";

            }
            else if ( userType == "bm")
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

        }

        public void lnkBtnSCHEMEWISEAUM_OnClick(object sender, EventArgs e)
        {
            SetParameters();
            showHideGrid("SchemeWise");
            BindSCHEMEWISEAUMDetails();
            lblMFMISType.Text = "SCHEME WISE AUM";
        }

        public void lnkBtnFOLIOWISEAUM_OnClick(object sender, EventArgs e)
        {
            SetParameters();
            showHideGrid("FolioWise");
            BindFOLIOWISEAUMDetails();
            lblMFMISType.Text = "FOLIO WISE AUM";
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
                if (userType == "rm")
                {
                    dsMISReport = adviserMISBo.GetAMCwiseAUMForRM(int.Parse(hdnrmId.Value), Valuation_Date);
                }
                else if (userType == "advisor")
                {
                    dsMISReport = adviserMISBo.GetAMCwiseAUMForAdviser(int.Parse(hdnadviserId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnrmId.Value), Valuation_Date);
                }
                else if (userType == "bm")
                {
                    dsMISReport = adviserMISBo.GetAUMForBM(int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), Valuation_Date,int.Parse(hdnType.Value),AmcCode,SchemeCode);
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
                dtMISReport.Columns.Add("AUM");
                dtMISReport.Columns.Add("Percentage");
                DataRow drMISReport;
                for (int i = 0; i < dsMISReport.Tables[0].Rows.Count; i++)
                {
                    drMISReport = dtMISReport.NewRow();
                    drMISReport[0] = dsMISReport.Tables[0].Rows[i]["AMC"].ToString();
                    drMISReport[1] = dsMISReport.Tables[0].Rows[i]["AMCCode"].ToString();
                    drMISReport[2] = dsMISReport.Tables[0].Rows[i]["AUM"].ToString();
                    drMISReport[3] = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i]["Percentage"].ToString()), 2).ToString();
                    dtMISReport.Rows.Add(drMISReport);
                }
                gvAmcWiseAUM.DataSource = dtMISReport;
                gvAmcWiseAUM.DataBind();

                if (Cache["gvAmcWiseAUMDetails" + advisorVo.advisorId.ToString()] == null)
                {
                    Cache.Insert("gvAmcWiseAUMDetails" + advisorVo.advisorId.ToString(), dsMISReport);
                }
                else
                {
                    Cache.Remove("gvAmcWiseAUMDetails" + advisorVo.advisorId.ToString());
                    Cache.Insert("gvAmcWiseAUMDetails" + advisorVo.advisorId.ToString(), dsMISReport);
                }
            }
        }

        public void BindSCHEMEWISEAUMDetails()
        {
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
            if (userType == "rm")
            {
                dsMISReport = adviserMISBo.GetAMCSchemewiseAUMForRM(int.Parse(hdnrmId.Value), Valuation_Date, AmcCode);
            }
            else if (userType == "bm")
            {
                dsMISReport = adviserMISBo.GetAUMForBM(int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), Valuation_Date,int.Parse(hdnType.Value),AmcCode,SchemeCode);

            }
            else if (userType == "advisor")
            {
                dsMISReport = adviserMISBo.GetAMCSchemewiseAUMForAdviser(int.Parse(hdnadviserId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnrmId.Value), Valuation_Date, AmcCode);

            }
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
                gvSchemeWiseAUM.DataSource = dsMISReport;
                gvSchemeWiseAUM.DataBind();
                if (Cache["gvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString()] == null)
                {
                    Cache.Insert("gvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString(), dsMISReport);
                }
                else
                {
                    Cache.Remove("gvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString());
                    Cache.Insert("gvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString(), dsMISReport);
                }
            }
        }

        public void BindFOLIOWISEAUMDetails()
        {
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
            if (userType == "rm")
            {
                dsMISReport = adviserMISBo.GetCustomerAMCSchemewiseAUMForRM(int.Parse(hdnrmId.Value), Valuationdate,SchemeCode);
            }
            else if (userType == "bm")
            {
                //if (ddlBranch.SelectedValue != "0")
                //    branchId = int.Parse(ddlBranch.SelectedValue);
                //if (ddlRM.SelectedValue != "0")
                //    rmId = int.Parse(ddlRM.SelectedValue);

                dsMISReport = adviserMISBo.GetAUMForBM(int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), Valuationdate, int.Parse(hdnType.Value),AmcCode,SchemeCode);
            }
            else if (userType == "advisor")
            {
                dsMISReport = adviserMISBo.GetCustomerAMCSchemewiseAUMForAdviser(int.Parse(hdnadviserId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnrmId.Value), Valuationdate,SchemeCode);
            }
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
                gvFolioWiseAUM.DataSource = dsMISReport;
                gvFolioWiseAUM.DataBind();
                if (Cache["gvFolioWiseAUMDetails" + advisorVo.advisorId.ToString()] == null)
                {
                    Cache.Insert("gvFolioWiseAUMDetails" + advisorVo.advisorId.ToString(), dsMISReport);
                }
                else
                {
                    Cache.Remove("gvFolioWiseAUMDetails" + advisorVo.advisorId.ToString());
                    Cache.Insert("gvFolioWiseAUMDetails" + advisorVo.advisorId.ToString(), dsMISReport);
                }
            }
        }

        


        #endregion

        #region need data source

        protected void gvAmcWiseAUM_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dtProcessLogDetails = new DataSet();
            dtProcessLogDetails = (DataSet)Cache["gvAmcWiseAUMDetails" + advisorVo.advisorId.ToString()];
            gvAmcWiseAUM.DataSource = dtProcessLogDetails;
        }

        protected void gvFolioWiseAUM_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dtProcessLogDetails = new DataSet();
            dtProcessLogDetails = (DataSet)Cache["gvFolioWiseAUMDetails" + advisorVo.advisorId.ToString()];
            gvFolioWiseAUM.DataSource = dtProcessLogDetails;
        }

        protected void gvSchemeWiseAUM_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dtProcessLogDetails = new DataSet();
            dtProcessLogDetails = (DataSet)Cache["gvSchemeWiseAUMDetails" + advisorVo.advisorId.ToString()];
            gvSchemeWiseAUM.DataSource = dtProcessLogDetails;
        }

        #endregion

        protected void hidingConrolForRMAndBMLogin(string userType)
        {
            if (userType == "rm")
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
            gvAmcWiseAUM.ExportSettings.OpenInNewWindow = true;
            gvAmcWiseAUM.ExportSettings.IgnorePaging = true;
            gvAmcWiseAUM.ExportSettings.HideStructureColumns = true;
            gvAmcWiseAUM.ExportSettings.ExportOnlyData = true;
            gvAmcWiseAUM.ExportSettings.FileName = "AmcWise AUM Details";
            gvAmcWiseAUM.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvAmcWiseAUM.MasterTableView.ExportToExcel();
        }

        public void imgBtnGvSchemeWiseAUM_OnClick(object sender, ImageClickEventArgs e)
        {
            gvSchemeWiseAUM.ExportSettings.OpenInNewWindow = true;
            gvSchemeWiseAUM.ExportSettings.IgnorePaging = true;
            gvSchemeWiseAUM.ExportSettings.HideStructureColumns = true;
            gvSchemeWiseAUM.ExportSettings.ExportOnlyData = true;
            gvSchemeWiseAUM.ExportSettings.FileName = "SchemeWise AUM Details";
            gvSchemeWiseAUM.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvSchemeWiseAUM.MasterTableView.ExportToExcel();
        }

        public void imgBtnGvFolioWiseAUM_OnClick(object sender, ImageClickEventArgs e)
        {
            gvFolioWiseAUM.ExportSettings.OpenInNewWindow = true;
            gvFolioWiseAUM.ExportSettings.IgnorePaging = true;
            gvFolioWiseAUM.ExportSettings.HideStructureColumns = true;
            gvFolioWiseAUM.ExportSettings.ExportOnlyData = true;
            gvFolioWiseAUM.ExportSettings.FileName = "FolioWise AUM Details";
            gvFolioWiseAUM.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvFolioWiseAUM.MasterTableView.ExportToExcel();

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
                                    AmcCode = int.Parse( gvr.GetDataKeyValue("AMCCode").ToString());

                                    showHideGrid("SchemeWise");
                                    BindSCHEMEWISEAUMDetails();
                                    lblMFMISType.Text = "SCHEME WISE AUM";
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
                                string folio = gvr.GetDataKeyValue("FolioNum").ToString();
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
    }
}
