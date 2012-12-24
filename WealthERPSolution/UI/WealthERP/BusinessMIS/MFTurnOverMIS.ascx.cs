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
using BoAdvisorProfiling;
using System.Configuration;


namespace WealthERP.BusinessMIS
{
    public partial class MFTurnOverMIS : System.Web.UI.UserControl
    {
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        RMVo rmVo = new RMVo();
        UserVo userVo = new UserVo();
        DateTime dtTo = new DateTime();
        DateBo dtBo = new DateBo();
        DateTime dtFrom = new DateTime();

        int advisorId = 0;
        String userType;
        int rmId = 0;
        int bmID = 0;
        int all = 0;
        int branchId = 0;
        int branchHeadId = 0;
        string path = string.Empty;
        int gvAmcCode = 0;
        int gvSchemeCode = 0;
        DateTime LatestValuationdate;

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session["userVo"];
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            advisorId = advisorVo.advisorId;
            int RMId = rmVo.RMId;
            rmId = rmVo.RMId;
            bmID = rmVo.RMId;
            
            if (!IsPostBack)
            {
                rbtnPickDate.Checked = true;
                rbtnPickPeriod.Checked = false;
                trRange.Visible = true;
                trPeriod.Visible = false;
                dvProduct.Visible = true;
                dvOrganization.Visible = false;
                pnlAMC.Visible = false;
                pnlScheme.Visible = false;
                pnlBranch.Visible = false;
                pnlFolio.Visible = false;
                pnlCategory.Visible = false;
                pnlRM.Visible = false;
                BindCategory();
                if (userType == "advisor")
                {
                    BindBranchDropDown();
                    BindRMDropDown();
                }
                else if (userType == "rm")
                {
                    Action.Visible = false;
                    ddlAction.Visible = false;
                    trBranchRM.Visible = false;
                }
                if (userType == "bm")
                {
                    BindBranchForBMDropDown();
                    BindRMforBranchDropdown(0, bmID);
                }
                LatestValuationdate = adviserMISBo.GetLatestValuationDateFromHistory(advisorId, "MF");
                txtFromDate.SelectedDate = DateTime.Parse( LatestValuationdate.ToShortDateString());
                txtToDate.SelectedDate = DateTime.Parse(LatestValuationdate.ToShortDateString());
            }

        }

        private void BindCategory()
        {
            DataSet dsGetCategory = new DataSet();
            dsGetCategory = adviserMFMIS.getTurnOverCategoryList();
            if (dsGetCategory.Tables[0].Rows.Count > 0)
            {
                ddlCategory.DataSource = dsGetCategory.Tables[0];
                ddlCategory.DataValueField = dsGetCategory.Tables[0].Columns["Category_Code"].ToString();
                ddlCategory.DataTextField = dsGetCategory.Tables[0].Columns["Category_Name"].ToString();
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
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
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "2"));
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

                FunctionInfo.Add("Method", "AdviserEQMIS.ascx:BindRMforBranchDropdown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void rbtnDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPickDate.Checked == true)
            {
                trRange.Visible = true;
                trPeriod.Visible = false;
            }
            else if (rbtnPickPeriod.Checked == true)
            {
                trRange.Visible = false;
                trPeriod.Visible = true;
                BindPeriodDropDown();
            }
        }
        private void BindPeriodDropDown()
        {
            DataTable dtPeriod;
            dtPeriod = XMLBo.GetDatePeriod(path);

            ddlPeriod.DataSource = dtPeriod;
            ddlPeriod.DataTextField = "PeriodType";
            ddlPeriod.DataValueField = "PeriodCode";
            ddlPeriod.DataBind();
            ddlPeriod.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select a Period", "Select a Period"));
            ddlPeriod.Items.RemoveAt(15);
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

        protected void lnkPSummary_Click(object sender, EventArgs e)
        {
            SetParameters();
            showHideGrid("CategoryWise");
            BindCategoryWiseTransactionDetails();
            lblMFMISType.Text = "Category Wise";
        }

        protected void lnkBtnAMCWISEAUM_Click(object sender, EventArgs e)
        {
            SetParameters();
            showHideGrid("AMCWise");
            BindAMCWISETransactionDetails();
            lblMFMISType.Text = "AMC WISE";
        }

        private void BindAMCWISETransactionDetails()
        {
            int amcCode = 0;
            int amcCodeOld = 0;
            DataSet dsGetAMCTransactionDeatails = new DataSet();
            dsGetAMCTransactionDeatails = adviserMFMIS.GetAMCTransactionDeatails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value));

            DataTable dtGetAMCTransactionDeatails = new DataTable();

            dtGetAMCTransactionDeatails.Columns.Add("AMCCode");
            dtGetAMCTransactionDeatails.Columns.Add("AMC");
            //dtGetAMCTransactionDeatails.Columns.Add("Category");
            //dtGetAMCTransactionDeatails.Columns.Add("SubCategory");
            dtGetAMCTransactionDeatails.Columns.Add("BUYCount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("BUYAmount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("SELCount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("SELAmount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("DVRCount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("DVRAmount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("DVPCount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("DVPAmount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("SIPCount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("SIPAmount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("BCICount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("BCIAmount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("BCOCount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("BCOAmount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("STBCount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("STBAmount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("STSCount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("STSAmount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("SWBCount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("SWBAmount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("SWPCount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("SWPAmount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("SWSCount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("SWSAmount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("PRJCount", typeof(double));
            dtGetAMCTransactionDeatails.Columns.Add("PRJAmount", typeof(double));

            DataRow drGetAMCTransactionDeatails;
            DataRow[] drTransactionAMCWise;
            if (dsGetAMCTransactionDeatails.Tables[0]!=null)
            {
                DataTable dtGetAMCTransaction = dsGetAMCTransactionDeatails.Tables[0];

                foreach (DataRow drAMCTransaction in dtGetAMCTransaction.Rows)
                {

                    Int32.TryParse(drAMCTransaction["PA_AMCCode"].ToString(), out amcCode);
                    if (amcCode != amcCodeOld)
                    { //go for another row to find new customer
                        amcCodeOld = amcCode;
                        drGetAMCTransactionDeatails = dtGetAMCTransactionDeatails.NewRow();
                        if (amcCode != 0)
                        { // add row in manual datatable within this brace end
                            drTransactionAMCWise = dtGetAMCTransaction.Select("PA_AMCCode=" + amcCode.ToString());
                            drGetAMCTransactionDeatails["AMCCode"] = drAMCTransaction["PA_AMCCode"].ToString();
                            drGetAMCTransactionDeatails["AMC"] = drAMCTransaction["PA_AMCName"].ToString();
                            //drGetAMCTransactionDeatails["Category"] = drAMCTransaction["PAIC_AssetInstrumentCategoryName"].ToString();
                            //drGetAMCTransactionDeatails["SubCategory"] = drAMCTransaction["PAISC_AssetInstrumentSubCategoryName"].ToString();
                            if (drTransactionAMCWise.Count() > 0)
                            {
                                foreach (DataRow dr in drTransactionAMCWise)
                                {

                                    string transactiontype = dr["WMTT_TransactionClassificationCode"].ToString();
                                    switch (transactiontype)
                                    {
                                        case "BUY":
                                            {
                                                drGetAMCTransactionDeatails["BUYCount"] = dr["TrnsCount"].ToString();
                                                drGetAMCTransactionDeatails["BUYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SEL":
                                            {
                                                drGetAMCTransactionDeatails["SELCount"] = dr["TrnsCount"].ToString();
                                                drGetAMCTransactionDeatails["SELAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "DVR":
                                            {
                                                drGetAMCTransactionDeatails["DVRCount"] = dr["TrnsCount"].ToString();
                                                drGetAMCTransactionDeatails["DVRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "DVP":
                                            {
                                                drGetAMCTransactionDeatails["DVPCount"] = dr["TrnsCount"].ToString();
                                                drGetAMCTransactionDeatails["DVPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SIP":
                                            {
                                                drGetAMCTransactionDeatails["SIPCount"] = dr["TrnsCount"].ToString();
                                                drGetAMCTransactionDeatails["SIPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "BCI":
                                            {
                                                drGetAMCTransactionDeatails["BCICount"] = dr["TrnsCount"].ToString();
                                                drGetAMCTransactionDeatails["BCIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "BCO":
                                            {
                                                drGetAMCTransactionDeatails["BCOCount"] = dr["TrnsCount"].ToString();
                                                drGetAMCTransactionDeatails["BCOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "STB":
                                            {
                                                drGetAMCTransactionDeatails["STBCount"] = dr["TrnsCount"].ToString();
                                                drGetAMCTransactionDeatails["STBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "STS":
                                            {
                                                drGetAMCTransactionDeatails["STSCount"] = dr["TrnsCount"].ToString();
                                                drGetAMCTransactionDeatails["STSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SWB":
                                            {
                                                drGetAMCTransactionDeatails["SWBCount"] = dr["TrnsCount"].ToString();
                                                drGetAMCTransactionDeatails["SWBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SWP":
                                            {
                                                drGetAMCTransactionDeatails["SWPCount"] = dr["TrnsCount"].ToString();
                                                drGetAMCTransactionDeatails["SWPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SWS":
                                            {
                                                drGetAMCTransactionDeatails["SWSCount"] = dr["TrnsCount"].ToString();
                                                drGetAMCTransactionDeatails["SWSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "PRJ":
                                            {
                                                drGetAMCTransactionDeatails["PRJCount"] = dr["TrnsCount"].ToString();
                                                drGetAMCTransactionDeatails["PRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                    }
                                   
                                }
                            }
                            dtGetAMCTransactionDeatails.Rows.Add(drGetAMCTransactionDeatails);
                        }//*

                    }//**

                }//***
                gvAmcWise.DataSource = dtGetAMCTransactionDeatails;
                gvAmcWise.DataBind();
                gvAmcWise.Visible = true;
                if (Cache["gvAmcWiseAUMDetails" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("gvAmcWiseTransactionDetails" + userVo.UserId.ToString(), dtGetAMCTransactionDeatails);
                }
                else
                {
                    Cache.Remove("gvAmcWiseTransactionDetails" + userVo.UserId.ToString());
                    Cache.Insert("gvAmcWiseTransactionDetails" + userVo.UserId.ToString(), dtGetAMCTransactionDeatails);
                }
            }
        }

        protected void lnkBtnSCHEMEWISEAUM_Click(object sender, EventArgs e)
        {
            SetParameters();
            showHideGrid("SchemeWise");
            BindSchemeWiseTransactionDetails();
            lblMFMISType.Text = "SCHEME WISE";
        }

        private void BindSchemeWiseTransactionDetails()
        {
            int SchemeCode = 0;
            int SchemeCodeOld = 0;
            DataSet dsGetSchemeTransactionDeatails = new DataSet();
            dsGetSchemeTransactionDeatails = adviserMFMIS.GetSchemeTransactionDeatails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), gvAmcCode, hdnCategory.Value);

            DataTable dtGetSchemeTransactionDeatails = new DataTable();

            dtGetSchemeTransactionDeatails.Columns.Add("AMC");
            dtGetSchemeTransactionDeatails.Columns.Add("SchemeCode");
            dtGetSchemeTransactionDeatails.Columns.Add("Scheme");
            dtGetSchemeTransactionDeatails.Columns.Add("ExternalCode");
            dtGetSchemeTransactionDeatails.Columns.Add("Category");
            dtGetSchemeTransactionDeatails.Columns.Add("SubCategory");
            dtGetSchemeTransactionDeatails.Columns.Add("BUYCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("BUYAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("SELCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("SELAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("DVRCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("DVRAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("DVPCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("DVPAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("SIPCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("SIPAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("BCICount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("BCIAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("BCOCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("BCOAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("STBCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("STBAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("STSCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("STSAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("SWBCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("SWBAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("SWPCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("SWPAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("SWSCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("SWSAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("PRJCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("PRJAmount", typeof(double));

            DataRow drGetSchemeTransactionDeatails;
            DataRow[] drTransactionSchemeWise;
            if (dsGetSchemeTransactionDeatails.Tables[0] != null)
            {
                DataTable dtGetSchemeTransaction = dsGetSchemeTransactionDeatails.Tables[0];

                foreach (DataRow drAMCTransaction in dtGetSchemeTransaction.Rows)
                {

                    Int32.TryParse(drAMCTransaction["PASP_SchemePlanCode"].ToString(), out SchemeCode);

                    if (SchemeCode != SchemeCodeOld)
                    { //go for another row to find new customer
                        SchemeCodeOld = SchemeCode;
                        drGetSchemeTransactionDeatails = dtGetSchemeTransactionDeatails.NewRow();
                        if (SchemeCode != 0)
                        { // add row in manual datatable within this brace end
                            drTransactionSchemeWise = dtGetSchemeTransaction.Select("PASP_SchemePlanCode=" + SchemeCode.ToString());
                            drGetSchemeTransactionDeatails["AMC"] = drAMCTransaction["PA_AMCName"].ToString();
                            drGetSchemeTransactionDeatails["SchemeCode"] = drAMCTransaction["PASP_SchemePlanCode"].ToString();
                            drGetSchemeTransactionDeatails["Scheme"] = drAMCTransaction["PASP_SchemePlanName"].ToString();
                            drGetSchemeTransactionDeatails["ExternalCode"] = drAMCTransaction["PASC_AMC_ExternalCode"].ToString();
                            drGetSchemeTransactionDeatails["Category"] = drAMCTransaction["PAIC_AssetInstrumentCategoryName"].ToString();
                            drGetSchemeTransactionDeatails["SubCategory"] = drAMCTransaction["PAISC_AssetInstrumentSubCategoryName"].ToString();
                            if (drTransactionSchemeWise.Count() > 0)
                            {
                                foreach (DataRow dr in drTransactionSchemeWise)
                                {

                                    string transactiontype = dr["WMTT_TransactionClassificationCode"].ToString();
                                    switch (transactiontype)
                                    {
                                        case "BUY":
                                            {
                                                drGetSchemeTransactionDeatails["BUYCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["BUYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SEL":
                                            {
                                                drGetSchemeTransactionDeatails["SELCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["SELAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "DVR":
                                            {
                                                drGetSchemeTransactionDeatails["DVRCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["DVRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "DVP":
                                            {
                                                drGetSchemeTransactionDeatails["DVPCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["DVPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SIP":
                                            {
                                                drGetSchemeTransactionDeatails["SIPCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["SIPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "BCI":
                                            {
                                                drGetSchemeTransactionDeatails["BCICount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["BCIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "BCO":
                                            {
                                                drGetSchemeTransactionDeatails["BCOCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["BCOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "STB":
                                            {
                                                drGetSchemeTransactionDeatails["STBCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["STBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "STS":
                                            {
                                                drGetSchemeTransactionDeatails["STSCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["STSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SWB":
                                            {
                                                drGetSchemeTransactionDeatails["SWBCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["SWBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SWP":
                                            {
                                                drGetSchemeTransactionDeatails["SWPCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["SWPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SWS":
                                            {
                                                drGetSchemeTransactionDeatails["SWSCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["SWSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "PRJ":
                                            {
                                                drGetSchemeTransactionDeatails["PRJCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["PRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                    }

                                }
                            }
                            dtGetSchemeTransactionDeatails.Rows.Add(drGetSchemeTransactionDeatails);
                        }//*

                    }//**

                }//***
                gvSchemeWise.DataSource = dtGetSchemeTransactionDeatails;
                gvSchemeWise.DataBind();
                gvSchemeWise.Visible = true;
                if (Cache["SchemeTransactionDeatails" + userVo.UserId] == null)
                {
                    Cache.Insert("SchemeTransactionDeatails" + userVo.UserId, dtGetSchemeTransactionDeatails);
                }
                else
                {
                    Cache.Remove("SchemeTransactionDeatails" + userVo.UserId);
                    Cache.Insert("SchemeTransactionDeatails" + userVo.UserId, dtGetSchemeTransactionDeatails);
                }
                //ErrorMessage.Visible = false;
                //btnImagExport.Visible = true;
            }
        }

        protected void lnkBtnFOLIOWISEAUM_Click(object sender, EventArgs e)
        {
            SetParameters();
            showHideGrid("FolioWise");
            BindFolioWiseTransactionDetails();
            lblMFMISType.Text = "Customer/Folio WISE";
        }

        private void BindFolioWiseTransactionDetails()
        {
            int AcountId = 0;
            int AcountIdOld = 0;
            DataSet dsGetFolioTransactionDeatails = new DataSet();
            dsGetFolioTransactionDeatails = adviserMFMIS.GetFolioTransactionDeatails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), gvSchemeCode);

            DataTable dtGetFolioTransactionDeatails = new DataTable();

            dtGetFolioTransactionDeatails.Columns.Add("Customer");
            dtGetFolioTransactionDeatails.Columns.Add("BranchName");
            dtGetFolioTransactionDeatails.Columns.Add("RMName");
            dtGetFolioTransactionDeatails.Columns.Add("Folio");
            dtGetFolioTransactionDeatails.Columns.Add("BUYCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("BUYAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("SELCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("SELAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("DVRCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("DVRAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("DVPCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("DVPAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("SIPCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("SIPAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("BCICount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("BCIAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("BCOCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("BCOAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("STBCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("STBAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("STSCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("STSAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("SWBCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("SWBAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("SWPCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("SWPAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("SWSCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("SWSAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("PRJCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("PRJAmount", typeof(double));

            DataRow drGetFolioTransactionDeatails;
            DataRow[] drTransactionFolioWise;
            if (dsGetFolioTransactionDeatails.Tables[0] != null)
            {
                DataTable dtGetFolioTransaction = dsGetFolioTransactionDeatails.Tables[0];

                foreach (DataRow drFolioTransaction in dtGetFolioTransaction.Rows)
                {

                    Int32.TryParse(drFolioTransaction["CMFA_AccountId"].ToString(), out AcountId);

                    if (AcountId != AcountIdOld)
                    { //go for another row to find new customer
                        AcountIdOld = AcountId;
                        drGetFolioTransactionDeatails = dtGetFolioTransactionDeatails.NewRow();
                        if (AcountId != 0)
                        { // add row in manual datatable within this brace end
                            drTransactionFolioWise = dtGetFolioTransaction.Select("CMFA_AccountId=" + AcountId.ToString());
                            //drGetSchemeTransactionDeatails["PA_AMCName"] = drAMCTransaction["PA_AMCName"].ToString();
                            drGetFolioTransactionDeatails["Customer"] = drFolioTransaction["CustomerName"].ToString();
                            drGetFolioTransactionDeatails["BranchName"] = drFolioTransaction["AB_BranchName"].ToString();
                            drGetFolioTransactionDeatails["RMName"] = drFolioTransaction["RmName"].ToString();
                            drGetFolioTransactionDeatails["Folio"] = drFolioTransaction["CMFA_FolioNum"].ToString();
                            if (drTransactionFolioWise.Count() > 0)
                            {
                                foreach (DataRow dr in drTransactionFolioWise)
                                {

                                    string transactiontype = dr["WMTT_TransactionClassificationCode"].ToString();
                                    switch (transactiontype)
                                    {
                                        case "BUY":
                                            {
                                                drGetFolioTransactionDeatails["BUYCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["BUYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SEL":
                                            {
                                                drGetFolioTransactionDeatails["SELCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["SELAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "DVR":
                                            {
                                                drGetFolioTransactionDeatails["DVRCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["DVRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "DVP":
                                            {
                                                drGetFolioTransactionDeatails["DVPCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["DVPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SIP":
                                            {
                                                drGetFolioTransactionDeatails["SIPCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["SIPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "BCI":
                                            {
                                                drGetFolioTransactionDeatails["BCICount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["BCIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "BCO":
                                            {
                                                drGetFolioTransactionDeatails["BCOCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["BCOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "STB":
                                            {
                                                drGetFolioTransactionDeatails["STBCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["STBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "STS":
                                            {
                                                drGetFolioTransactionDeatails["STSCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["STSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SWB":
                                            {
                                                drGetFolioTransactionDeatails["SWBCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["SWBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SWP":
                                            {
                                                drGetFolioTransactionDeatails["SWPCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["SWPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SWS":
                                            {
                                                drGetFolioTransactionDeatails["SWSCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["SWSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "PRJ":
                                            {
                                                drGetFolioTransactionDeatails["PRJCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["PRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                    }

                                }
                            }
                            dtGetFolioTransactionDeatails.Rows.Add(drGetFolioTransactionDeatails);
                        }//*

                    }//**

                }//***
                gvFolioWise.DataSource = dtGetFolioTransactionDeatails;
                gvFolioWise.DataBind();
                gvFolioWise.Visible = true;
                if (Cache["FolioTransactionDeatails" + userVo.UserId] == null)
                {
                    Cache.Insert("FolioTransactionDeatails" + userVo.UserId, dtGetFolioTransactionDeatails);
                }
                else
                {
                    Cache.Remove("FolioTransactionDeatails" + userVo.UserId);
                    Cache.Insert("FolioTransactionDeatails" + userVo.UserId, dtGetFolioTransactionDeatails);
                }
                //ErrorMessage.Visible = false;
                //btnImagExport.Visible = true;
            }
        }

        protected void lnkOSummary_Click(object sender, EventArgs e)
        {
            SetParameters();
            showHideGrid("CategoryWise");
            BindCategoryWiseTransactionDetails();
            lblMFMISType.Text = "Category Wise";
        }

        protected void BindCategoryWiseTransactionDetails()
        {
            string subcategoryCode = string.Empty;
            string subCategoryCodeOld = string.Empty;
            DataSet dsGetCategoryTransactionDeatails = new DataSet();
            dsGetCategoryTransactionDeatails = adviserMFMIS.GetCategoryTransactionDeatails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), hdnCategory.Value);

            DataTable dtGetCategoryTransactionDeatails = new DataTable();

            dtGetCategoryTransactionDeatails.Columns.Add("Category");
            dtGetCategoryTransactionDeatails.Columns.Add("SubCategory");
            dtGetCategoryTransactionDeatails.Columns.Add("BUYCount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("BUYAmount",typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("SELCount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("SELAmount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("DVRCount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("DVRAmount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("DVPCount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("DVPAmount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("SIPCount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("SIPAmount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("BCICount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("BCIAmount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("BCOCount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("BCOAmount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("STBCount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("STBAmount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("STSCount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("STSAmount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("SWBCount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("SWBAmount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("SWPCount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("SWPAmount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("SWSCount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("SWSAmount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("PRJCount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("PRJAmount", typeof(double));
            DataRow[] drTransactionCategoryWise;
            DataRow drGetCategoryTransactionDeatails;
            if (dsGetCategoryTransactionDeatails.Tables[0] != null)
            {
                DataTable dtGetCategoryTransaction = dsGetCategoryTransactionDeatails.Tables[0];

                foreach (DataRow drCategoryTransaction in dtGetCategoryTransaction.Rows)
                {
                    subcategoryCode = drCategoryTransaction["PAISC_AssetInstrumentSubCategoryCode"].ToString();

                    if (subcategoryCode != subCategoryCodeOld)
                    { //go for another row to find new customer
                        subCategoryCodeOld = subcategoryCode;
                        drGetCategoryTransactionDeatails = dtGetCategoryTransactionDeatails.NewRow();
                    
                    // add row in manual datatable within this brace end
                    drTransactionCategoryWise = dtGetCategoryTransaction.Select("PAISC_AssetInstrumentSubCategoryCode=" +"'"+subcategoryCode+"'");

                    if (drTransactionCategoryWise.Count() > 0)
                    {
                        foreach (DataRow dr in drTransactionCategoryWise)
                    {
                    drGetCategoryTransactionDeatails["Category"] = drCategoryTransaction["PAIC_AssetInstrumentCategoryName"].ToString();
                    drGetCategoryTransactionDeatails["SubCategory"] = drCategoryTransaction["PAISC_AssetInstrumentSubCategoryName"].ToString();
                    string transactiontype = dr["WMTT_TransactionClassificationCode"].ToString();
                    switch (transactiontype)
                    {
                        case "BUY":
                            {
                                drGetCategoryTransactionDeatails["BUYCount"] = dr["TrnsCount"].ToString();
                                drGetCategoryTransactionDeatails["BUYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                break;
                            }
                        case "SEL":
                            {
                                drGetCategoryTransactionDeatails["SELCount"] = dr["TrnsCount"].ToString();
                                drGetCategoryTransactionDeatails["SELAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                break;
                            }
                        case "DVR":
                            {
                                drGetCategoryTransactionDeatails["DVRCount"] = dr["TrnsCount"].ToString();
                                drGetCategoryTransactionDeatails["DVRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                break;
                            }
                        case "DVP":
                            {
                                drGetCategoryTransactionDeatails["DVPCount"] = dr["TrnsCount"].ToString();
                                drGetCategoryTransactionDeatails["DVPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                break;
                            }
                        case "SIP":
                            {
                                drGetCategoryTransactionDeatails["SIPCount"] = dr["TrnsCount"].ToString();
                                drGetCategoryTransactionDeatails["SIPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                break;
                            }
                        case "BCI":
                            {
                                drGetCategoryTransactionDeatails["BCICount"] = dr["TrnsCount"].ToString();
                                drGetCategoryTransactionDeatails["BCIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                break;
                            }
                        case "BCO":
                            {
                                drGetCategoryTransactionDeatails["BCOCount"] = dr["TrnsCount"].ToString();
                                drGetCategoryTransactionDeatails["BCOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                break;
                            }
                        case "STB":
                            {
                                drGetCategoryTransactionDeatails["STBCount"] = dr["TrnsCount"].ToString();
                                drGetCategoryTransactionDeatails["STBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                break;
                            }
                        case "STS":
                            {
                                drGetCategoryTransactionDeatails["STSCount"] = dr["TrnsCount"].ToString();
                                drGetCategoryTransactionDeatails["STSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                break;
                            }
                        case "SWB":
                            {
                                drGetCategoryTransactionDeatails["SWBCount"] = dr["TrnsCount"].ToString();
                                drGetCategoryTransactionDeatails["SWBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                break;
                            }
                        case "SWP":
                            {
                                drGetCategoryTransactionDeatails["SWPCount"] = dr["TrnsCount"].ToString();
                                drGetCategoryTransactionDeatails["SWPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                break;
                            }
                        case "SWS":
                            {
                                drGetCategoryTransactionDeatails["SWSCount"] = dr["TrnsCount"].ToString();
                                drGetCategoryTransactionDeatails["SWSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                break;
                            }
                        case "PRJ":
                            {
                                drGetCategoryTransactionDeatails["PRJCount"] = dr["TrnsCount"].ToString();
                                drGetCategoryTransactionDeatails["PRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                break;
                            }

                    }
                    }
                    dtGetCategoryTransactionDeatails.Rows.Add(drGetCategoryTransactionDeatails);
                    }//*

                    }  //**

                }//***
                gvCategoryWise.DataSource = dtGetCategoryTransactionDeatails;
                gvCategoryWise.DataBind();
                gvCategoryWise.Visible = true;
                if (Cache["CategoryTransactionDeatails" + userVo.UserId] == null)
                {
                    Cache.Insert("CategoryTransactionDeatails" + userVo.UserId, dtGetCategoryTransactionDeatails);
                }
                else
                {
                    Cache.Remove("CategoryTransactionDeatails" + userVo.UserId);
                    Cache.Insert("CategoryTransactionDeatails" + userVo.UserId, dtGetCategoryTransactionDeatails);
                }
                //ErrorMessage.Visible = false;
                //btnImagExport.Visible = true;
            }
        }

        protected void lnkOBranch_Click(object sender, EventArgs e)
        {
            SetParameters();
            showHideGrid("BranchWise");
            BindBranchWiseTransactionDetails();
            lblMFMISType.Text = "Branch Wise";
        }

        private void BindBranchWiseTransactionDetails()
        {
            int BranchId = 0;
            int BranchIdOld = 0;
            DataSet dsGetBranchTransactionDeatails = new DataSet();
            dsGetBranchTransactionDeatails = adviserMFMIS.GetBranchTransactionDeatails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value));

            DataTable dtGetBranchTransactionDeatails = new DataTable();

            dtGetBranchTransactionDeatails.Columns.Add("Branch");
            dtGetBranchTransactionDeatails.Columns.Add("BUYCount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("BUYAmount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("SELCount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("SELAmount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("DVRCount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("DVRAmount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("DVPCount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("DVPAmount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("SIPCount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("SIPAmount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("BCICount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("BCIAmount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("BCOCount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("BCOAmount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("STBCount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("STBAmount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("STSCount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("STSAmount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("SWBCount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("SWBAmount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("SWPCount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("SWPAmount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("SWSCount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("SWSAmount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("PRJCount", typeof(double));
            dtGetBranchTransactionDeatails.Columns.Add("PRJAmount", typeof(double));

            DataRow drGetBranchTransactionDeatails;
            DataRow[] drTransactionBranchWise;
            if (dsGetBranchTransactionDeatails.Tables[0] != null)
            {
                DataTable dtGetBranchTransaction = dsGetBranchTransactionDeatails.Tables[0];

                foreach (DataRow drBranchTransaction in dtGetBranchTransaction.Rows)
                {

                    Int32.TryParse(drBranchTransaction["AB_BranchId"].ToString(), out BranchId);

                    if (BranchId != BranchIdOld)
                    { //go for another row to find new customer
                        BranchIdOld = BranchId;
                        drGetBranchTransactionDeatails = dtGetBranchTransactionDeatails.NewRow();
                        if (BranchId != 0)
                        { // add row in manual datatable within this brace end
                            drTransactionBranchWise = dtGetBranchTransaction.Select("AB_BranchId=" + BranchId.ToString());
                            //drGetSchemeTransactionDeatails["PA_AMCName"] = drAMCTransaction["PA_AMCName"].ToString();
                            drGetBranchTransactionDeatails["Branch"] = drBranchTransaction["BranchName"].ToString();
                            if (drTransactionBranchWise.Count() > 0)
                            {
                                foreach (DataRow dr in drTransactionBranchWise)
                                {

                                    string transactiontype = dr["WMTT_TransactionClassificationCode"].ToString();
                                    switch (transactiontype)
                                    {
                                        case "BUY":
                                            {
                                                drGetBranchTransactionDeatails["BUYCount"] = dr["TrnsCount"].ToString();
                                                drGetBranchTransactionDeatails["BUYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SEL":
                                            {
                                                drGetBranchTransactionDeatails["SELCount"] = dr["TrnsCount"].ToString();
                                                drGetBranchTransactionDeatails["SELAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "DVR":
                                            {
                                                drGetBranchTransactionDeatails["DVRCount"] = dr["TrnsCount"].ToString();
                                                drGetBranchTransactionDeatails["DVRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "DVP":
                                            {
                                                drGetBranchTransactionDeatails["DVPCount"] = dr["TrnsCount"].ToString();
                                                drGetBranchTransactionDeatails["DVPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SIP":
                                            {
                                                drGetBranchTransactionDeatails["SIPCount"] = dr["TrnsCount"].ToString();
                                                drGetBranchTransactionDeatails["SIPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "BCI":
                                            {
                                                drGetBranchTransactionDeatails["BCICount"] = dr["TrnsCount"].ToString();
                                                drGetBranchTransactionDeatails["BCIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "BCO":
                                            {
                                                drGetBranchTransactionDeatails["BCOCount"] = dr["TrnsCount"].ToString();
                                                drGetBranchTransactionDeatails["BCOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "STB":
                                            {
                                                drGetBranchTransactionDeatails["STBCount"] = dr["TrnsCount"].ToString();
                                                drGetBranchTransactionDeatails["STBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "STS":
                                            {
                                                drGetBranchTransactionDeatails["STSCount"] = dr["TrnsCount"].ToString();
                                                drGetBranchTransactionDeatails["STSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SWB":
                                            {
                                                drGetBranchTransactionDeatails["SWBCount"] = dr["TrnsCount"].ToString();
                                                drGetBranchTransactionDeatails["SWBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SWP":
                                            {
                                                drGetBranchTransactionDeatails["SWPCount"] = dr["TrnsCount"].ToString();
                                                drGetBranchTransactionDeatails["SWPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SWS":
                                            {
                                                drGetBranchTransactionDeatails["SWSCount"] = dr["TrnsCount"].ToString();
                                                drGetBranchTransactionDeatails["SWSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "PRJ":
                                            {
                                                drGetBranchTransactionDeatails["PRJCount"] = dr["TrnsCount"].ToString();
                                                drGetBranchTransactionDeatails["PRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                    }

                                }
                            }
                            dtGetBranchTransactionDeatails.Rows.Add(drGetBranchTransactionDeatails);
                        }//*

                    }//**

                }//***
                gvBranchWise.DataSource = dtGetBranchTransactionDeatails;
                gvBranchWise.DataBind();
                gvBranchWise.Visible = true;
                if (Cache["BranchTransactionDeatails" + userVo.UserId] == null)
                {
                    Cache.Insert("BranchTransactionDeatails" + userVo.UserId, dtGetBranchTransactionDeatails);
                }
                else
                {
                    Cache.Remove("BranchTransactionDeatails" + userVo.UserId);
                    Cache.Insert("BranchTransactionDeatails" + userVo.UserId, dtGetBranchTransactionDeatails);
                }
                //ErrorMessage.Visible = false;
                //btnImagExport.Visible = true;
            }
        }

        protected void lnkOCustomer_Click(object sender, EventArgs e)
        {
            SetParameters();
            showHideGrid("FolioWise");
            BindFolioWiseTransactionDetails();
            lblMFMISType.Text = "Customer/Folio WISE";
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
                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {

                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnAll.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRM.SelectedValue; ;
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "3";
                }
            }
            if (hdnbranchHeadId.Value == "")
                hdnbranchHeadId.Value = "0";

            if (hdnbranchId.Value == "")
                hdnbranchId.Value = "0";

            if (hdnadviserId.Value == "")
                hdnadviserId.Value = "0";

            if (hdnrmId.Value == "")
                hdnrmId.Value = "0";

            if (ddlCategory.SelectedIndex == 0)
                hdnCategory.Value = "";
            else
                hdnCategory.Value = ddlCategory.SelectedValue;

            if (rbtnPickDate.Checked)
            {
                hdnFromDate.Value = txtFromDate.SelectedDate.ToString();
                hdnToDate.Value = txtToDate.SelectedDate.ToString();
            }
            else
            {
                if (ddlPeriod.SelectedIndex != 0)
                {
                    dtBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue, out dtFrom, out dtTo);
                    hdnFromDate.Value = dtFrom.ToShortDateString();
                    hdnToDate.Value = dtTo.ToShortDateString();
                }
            }

        }

        public void showHideGrid(string gridName)
        {
            if (gridName == "AMCWise")
            {
                divGvAmcWise.Visible = true;
                dvScheme.Visible = false;
                divBranch.Visible = false;
                divFolioWise.Visible = false;
                divCategory.Visible = false;
                pnlAMC.Visible = true;
                pnlScheme.Visible = false;
                pnlBranch.Visible = false;
                pnlFolio.Visible = false;
                pnlCategory.Visible = false;
                btnAMCExport.Visible = true;
                btnSchemeExport.Visible = false;
                btnFolioExport.Visible = false;
                btnBranchExport.Visible = false;
                btnCategoryExport.Visible = false;
                btnRMExport.Visible = false;
                divRM.Visible = false;
                pnlRM.Visible = false;
            }
            else if (gridName == "SchemeWise")
            {
                dvScheme.Visible = true;
                divGvAmcWise.Visible = false;
                divBranch.Visible = false;
                divFolioWise.Visible = false;
                divCategory.Visible = false;
                pnlAMC.Visible = false;
                pnlScheme.Visible = true;
                pnlBranch.Visible = false;
                pnlFolio.Visible = false;
                pnlCategory.Visible = false;
                btnAMCExport.Visible = false;
                btnSchemeExport.Visible = true;
                btnFolioExport.Visible = false;
                btnBranchExport.Visible = false;
                btnCategoryExport.Visible = false;
                btnRMExport.Visible = false;
                divRM.Visible = false;
                pnlRM.Visible = false;
            }
            else if (gridName == "FolioWise")
            {
                dvScheme.Visible = false;
                divGvAmcWise.Visible = false;
                divBranch.Visible = false;
                divFolioWise.Visible = true;
                divCategory.Visible = false;
                pnlAMC.Visible = false;
                pnlScheme.Visible = false;
                pnlBranch.Visible = false;
                pnlFolio.Visible = true;
                pnlCategory.Visible = false;
                btnAMCExport.Visible = false;
                btnSchemeExport.Visible = false;
                btnFolioExport.Visible = true;
                btnBranchExport.Visible = false;
                btnCategoryExport.Visible = false;
                btnRMExport.Visible = false;
                divRM.Visible = false;
                pnlRM.Visible = false;
            }
            else if (gridName == "CategoryWise")
            {
                dvScheme.Visible = false;
                divGvAmcWise.Visible = false;
                divBranch.Visible = false;
                divFolioWise.Visible = false;
                divCategory.Visible = true;
                pnlAMC.Visible = false;
                pnlScheme.Visible = false;
                pnlBranch.Visible = false;
                pnlFolio.Visible = false;
                pnlCategory.Visible = true;
                btnAMCExport.Visible = false;
                btnSchemeExport.Visible = false;
                btnFolioExport.Visible = false;
                btnBranchExport.Visible = false;
                btnCategoryExport.Visible = true;
                btnRMExport.Visible = false;
                divRM.Visible = false;
                pnlRM.Visible = false;
            }
            else if (gridName == "BranchWise")
            {
                dvScheme.Visible = false;
                divGvAmcWise.Visible = false;
                divBranch.Visible = true;
                divFolioWise.Visible = false;
                divCategory.Visible = false;
                pnlAMC.Visible = false;
                pnlScheme.Visible = false;
                pnlBranch.Visible = true;
                pnlFolio.Visible = false;
                pnlCategory.Visible = false;
                btnAMCExport.Visible = false;
                btnSchemeExport.Visible = false;
                btnFolioExport.Visible = false;
                btnBranchExport.Visible = true;
                btnCategoryExport.Visible = false;
                btnRMExport.Visible = false;
                divRM.Visible = false;
                pnlRM.Visible = false;
            }
            else if (gridName == "RMWise")
            {
                dvScheme.Visible = false;
                divGvAmcWise.Visible = false;
                divBranch.Visible = false;
                divFolioWise.Visible = false;
                divCategory.Visible = false;
                pnlAMC.Visible = false;
                pnlScheme.Visible = false;
                pnlBranch.Visible = false;
                pnlFolio.Visible = false;
                pnlCategory.Visible = false;
                btnAMCExport.Visible = false;
                btnSchemeExport.Visible = false;
                btnFolioExport.Visible = false;
                btnBranchExport.Visible = false;
                btnCategoryExport.Visible = false;
                btnRMExport.Visible = true;
                divRM.Visible = true;
                pnlRM.Visible = true;
            }
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAction.SelectedValue == "Product")
            {
                dvProduct.Visible = true;
                dvOrganization.Visible = false;
            }
            else
            {
                dvProduct.Visible = false;
                dvOrganization.Visible = true;
            }
        }


        protected void gvAmcWise_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtTrailstransactionDet = new DataTable();
            dtTrailstransactionDet = (DataTable)Cache["gvAmcWiseTransactionDetails" + userVo.UserId.ToString()];
            gvAmcWise.DataSource = dtTrailstransactionDet;
            gvAmcWise.Visible = true;
        }
        protected void gvAmcWise_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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
                                    int selectedRow = gvr.ItemIndex+1 ;
                                    gvAmcCode = int.Parse(gvr.GetDataKeyValue("AMCCode").ToString());

                                    showHideGrid("SchemeWise");
                                    BindSchemeWiseTransactionDetails();
                                    lblMFMISType.Text = "Scheme Wise";
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

        }

        protected void gvSchemeWise_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetSchemeTransactionDeatails = new DataTable();
            dtGetSchemeTransactionDeatails = (DataTable)Cache["SchemeTransactionDeatails" + userVo.UserId];
            gvSchemeWise.DataSource = dtGetSchemeTransactionDeatails;
            gvSchemeWise.Visible = true;
        }
        protected void gvSchemeWise_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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
                                    gvSchemeCode = int.Parse(gvr.GetDataKeyValue("SchemeCode").ToString());

                                    showHideGrid("FolioWise");
                                    BindFolioWiseTransactionDetails();
                                    lblMFMISType.Text = "Folio Wise";
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

        }
        protected void gvBranchWise_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetBranchTransactionDeatails = new DataTable();
            dtGetBranchTransactionDeatails = (DataTable)Cache["BranchTransactionDeatails" + userVo.UserId];
            gvBranchWise.DataSource = dtGetBranchTransactionDeatails;
            gvBranchWise.Visible = true;
        }
        protected void gvFolioWise_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetFolioTransactionDeatails = new DataTable();
            dtGetFolioTransactionDeatails = (DataTable)Cache["FolioTransactionDeatails" + userVo.UserId];
            gvFolioWise.DataSource = dtGetFolioTransactionDeatails;
            gvFolioWise.Visible = true;
        }
        protected void gvCategoryWise_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetCategoryTransactionDeatails = new DataTable();
            dtGetCategoryTransactionDeatails = (DataTable)Cache["CategoryTransactionDeatails" + userVo.UserId];
            gvCategoryWise.DataSource = dtGetCategoryTransactionDeatails;
            gvCategoryWise.Visible = true;
        }
        protected void btnAMCExport_Click(object sender, ImageClickEventArgs e)
        {
            gvAmcWise.ExportSettings.OpenInNewWindow = true;
            gvAmcWise.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvAmcWise.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvAmcWise.MasterTableView.ExportToExcel();
        }
        protected void btnSchemeExport_Click(object sender, ImageClickEventArgs e)
        {
            gvSchemeWise.ExportSettings.OpenInNewWindow = true;
            gvSchemeWise.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvSchemeWise.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvSchemeWise.MasterTableView.ExportToExcel();
        }
        protected void btnFolioExport_Click(object sender, ImageClickEventArgs e)
        {
            gvFolioWise.ExportSettings.OpenInNewWindow = true;
            gvFolioWise.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvFolioWise.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvFolioWise.MasterTableView.ExportToExcel();
        }
        protected void btnBranchExport_Click(object sender, ImageClickEventArgs e)
        {
            gvBranchWise.ExportSettings.OpenInNewWindow = true;
            gvBranchWise.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvBranchWise.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvBranchWise.MasterTableView.ExportToExcel();
        }
        protected void btnCategoryExport_Click(object sender, ImageClickEventArgs e)
        {
            gvCategoryWise.ExportSettings.OpenInNewWindow = true;
            gvCategoryWise.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvCategoryWise.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvCategoryWise.MasterTableView.ExportToExcel();
        }

        protected void lnkRMWise_Click(object sender, EventArgs e)
        {
            SetParameters();
            showHideGrid("RMWise");
            BindRMWiseTransactionDetails();
            lblMFMISType.Text = "RM Wise";
        }

        private void BindRMWiseTransactionDetails()
        {
            int RMId = 0;
            int RMIdOld = 0;
            DataSet dsGetRMTransactionDeatails = new DataSet();
            dsGetRMTransactionDeatails = adviserMFMIS.GetRMTransactionDeatails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value));

            DataTable dtGetRMTransactionDeatails = new DataTable();

            dtGetRMTransactionDeatails.Columns.Add("RM");
            dtGetRMTransactionDeatails.Columns.Add("BUYCount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("BUYAmount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("SELCount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("SELAmount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("DVRCount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("DVRAmount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("DVPCount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("DVPAmount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("SIPCount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("SIPAmount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("BCICount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("BCIAmount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("BCOCount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("BCOAmount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("STBCount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("STBAmount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("STSCount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("STSAmount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("SWBCount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("SWBAmount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("SWPCount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("SWPAmount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("SWSCount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("SWSAmount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("PRJCount", typeof(double));
            dtGetRMTransactionDeatails.Columns.Add("PRJAmount", typeof(double));

            DataRow drGetRMTransactionDeatails;
            DataRow[] drTransactionRMWise;
            if (dsGetRMTransactionDeatails.Tables[0] != null)
            {
                DataTable dtGetRMTransaction = dsGetRMTransactionDeatails.Tables[0];

                foreach (DataRow drRMTransaction in dtGetRMTransaction.Rows)
                {

                    Int32.TryParse(drRMTransaction["AR_RMId"].ToString(), out RMId);

                    if (RMId != RMIdOld)
                    { //go for another row to find new customer
                        RMIdOld = RMId;
                        drGetRMTransactionDeatails = dtGetRMTransactionDeatails.NewRow();
                        if (RMId != 0)
                        { // add row in manual datatable within this brace end
                            drTransactionRMWise = dtGetRMTransaction.Select("AR_RMId=" + RMId.ToString());
                            
                            if (drTransactionRMWise.Count() > 0)
                            {
                                foreach (DataRow dr in drTransactionRMWise)
                                {
                                    drGetRMTransactionDeatails["RM"] = drRMTransaction["RM_Name"].ToString();
                                    string transactiontype = dr["WMTT_TransactionClassificationCode"].ToString();
                                    switch (transactiontype)
                                    {
                                        case "BUY":
                                            {
                                                drGetRMTransactionDeatails["BUYCount"] = dr["TrnsCount"].ToString();
                                                drGetRMTransactionDeatails["BUYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SEL":
                                            {
                                                drGetRMTransactionDeatails["SELCount"] = dr["TrnsCount"].ToString();
                                                drGetRMTransactionDeatails["SELAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "DVR":
                                            {
                                                drGetRMTransactionDeatails["DVRCount"] = dr["TrnsCount"].ToString();
                                                drGetRMTransactionDeatails["DVRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "DVP":
                                            {
                                                drGetRMTransactionDeatails["DVPCount"] = dr["TrnsCount"].ToString();
                                                drGetRMTransactionDeatails["DVPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SIP":
                                            {
                                                drGetRMTransactionDeatails["SIPCount"] = dr["TrnsCount"].ToString();
                                                drGetRMTransactionDeatails["SIPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "BCI":
                                            {
                                                drGetRMTransactionDeatails["BCICount"] = dr["TrnsCount"].ToString();
                                                drGetRMTransactionDeatails["BCIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "BCO":
                                            {
                                                drGetRMTransactionDeatails["BCOCount"] = dr["TrnsCount"].ToString();
                                                drGetRMTransactionDeatails["BCOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "STB":
                                            {
                                                drGetRMTransactionDeatails["STBCount"] = dr["TrnsCount"].ToString();
                                                drGetRMTransactionDeatails["STBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "STS":
                                            {
                                                drGetRMTransactionDeatails["STSCount"] = dr["TrnsCount"].ToString();
                                                drGetRMTransactionDeatails["STSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SWB":
                                            {
                                                drGetRMTransactionDeatails["SWBCount"] = dr["TrnsCount"].ToString();
                                                drGetRMTransactionDeatails["SWBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SWP":
                                            {
                                                drGetRMTransactionDeatails["SWPCount"] = dr["TrnsCount"].ToString();
                                                drGetRMTransactionDeatails["SWPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SWS":
                                            {
                                                drGetRMTransactionDeatails["SWSCount"] = dr["TrnsCount"].ToString();
                                                drGetRMTransactionDeatails["SWSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "PRJ":
                                            {
                                                drGetRMTransactionDeatails["PRJCount"] = dr["TrnsCount"].ToString();
                                                drGetRMTransactionDeatails["PRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                    }

                                }
                            }
                            dtGetRMTransactionDeatails.Rows.Add(drGetRMTransactionDeatails);
                        }//*

                    }//**

                }//***
                gvRM.DataSource = dtGetRMTransactionDeatails;
                gvRM.DataBind();
                gvRM.Visible = true;
                if (Cache["RMTransactionDeatails" + userVo.UserId] == null)
                {
                    Cache.Insert("RMTransactionDeatails" + userVo.UserId, dtGetRMTransactionDeatails);
                }
                else
                {
                    Cache.Remove("RMTransactionDeatails" + userVo.UserId);
                    Cache.Insert("RMTransactionDeatails" + userVo.UserId, dtGetRMTransactionDeatails);
                }
                
            }
        }

        protected void btnRMExport_Click(object sender, ImageClickEventArgs e)
        {
            gvRM.ExportSettings.OpenInNewWindow = true;
            gvRM.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvRM.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvRM.MasterTableView.ExportToExcel();
        }
        protected void gvRM_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetRMTransactionDeatails = new DataTable();
            dtGetRMTransactionDeatails = (DataTable)Cache["RMTransactionDeatails" + userVo.UserId];
            gvRM.DataSource = dtGetRMTransactionDeatails;
            gvRM.Visible = true;
        }
    }
}