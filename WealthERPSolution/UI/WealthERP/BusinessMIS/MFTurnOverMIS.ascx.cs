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

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session["userVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
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
            dtGetAMCTransactionDeatails.Columns.Add("BUYCount");
            dtGetAMCTransactionDeatails.Columns.Add("BUYAmount");
            dtGetAMCTransactionDeatails.Columns.Add("SELCount");
            dtGetAMCTransactionDeatails.Columns.Add("SELAmount");
            dtGetAMCTransactionDeatails.Columns.Add("DVRCount");
            dtGetAMCTransactionDeatails.Columns.Add("DVRAmount");
            dtGetAMCTransactionDeatails.Columns.Add("DVPCount");
            dtGetAMCTransactionDeatails.Columns.Add("DVPAmount");
            dtGetAMCTransactionDeatails.Columns.Add("SIPCount");
            dtGetAMCTransactionDeatails.Columns.Add("SIPAmount");
            dtGetAMCTransactionDeatails.Columns.Add("BCICount");
            dtGetAMCTransactionDeatails.Columns.Add("BCIAmount");
            dtGetAMCTransactionDeatails.Columns.Add("BCOCount");
            dtGetAMCTransactionDeatails.Columns.Add("BCOAmount");
            dtGetAMCTransactionDeatails.Columns.Add("STBCount");
            dtGetAMCTransactionDeatails.Columns.Add("STBAmount");
            dtGetAMCTransactionDeatails.Columns.Add("STSCount");
            dtGetAMCTransactionDeatails.Columns.Add("STSAmount");
            dtGetAMCTransactionDeatails.Columns.Add("SWBCount");
            dtGetAMCTransactionDeatails.Columns.Add("SWBAmount");
            dtGetAMCTransactionDeatails.Columns.Add("SWPCount");
            dtGetAMCTransactionDeatails.Columns.Add("SWPAmount");
            dtGetAMCTransactionDeatails.Columns.Add("SWSCount");
            dtGetAMCTransactionDeatails.Columns.Add("SWSAmount");
            dtGetAMCTransactionDeatails.Columns.Add("PRJCount");
            dtGetAMCTransactionDeatails.Columns.Add("PRJAmount");

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
            dsGetSchemeTransactionDeatails = adviserMFMIS.GetSchemeTransactionDeatails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), gvAmcCode);

            DataTable dtGetSchemeTransactionDeatails = new DataTable();

            dtGetSchemeTransactionDeatails.Columns.Add("SchemeCode");
            dtGetSchemeTransactionDeatails.Columns.Add("Scheme");
            dtGetSchemeTransactionDeatails.Columns.Add("ExternalCode");
            dtGetSchemeTransactionDeatails.Columns.Add("BUYCount");
            dtGetSchemeTransactionDeatails.Columns.Add("BUYAmount");
            dtGetSchemeTransactionDeatails.Columns.Add("SELCount");
            dtGetSchemeTransactionDeatails.Columns.Add("SELAmount");
            dtGetSchemeTransactionDeatails.Columns.Add("DVRCount");
            dtGetSchemeTransactionDeatails.Columns.Add("DVRAmount");
            dtGetSchemeTransactionDeatails.Columns.Add("DVPCount");
            dtGetSchemeTransactionDeatails.Columns.Add("DVPAmount");
            dtGetSchemeTransactionDeatails.Columns.Add("SIPCount");
            dtGetSchemeTransactionDeatails.Columns.Add("SIPAmount");
            dtGetSchemeTransactionDeatails.Columns.Add("BCICount");
            dtGetSchemeTransactionDeatails.Columns.Add("BCIAmount");
            dtGetSchemeTransactionDeatails.Columns.Add("BCOCount");
            dtGetSchemeTransactionDeatails.Columns.Add("BCOAmount");
            dtGetSchemeTransactionDeatails.Columns.Add("STBCount");
            dtGetSchemeTransactionDeatails.Columns.Add("STBAmount");
            dtGetSchemeTransactionDeatails.Columns.Add("STSCount");
            dtGetSchemeTransactionDeatails.Columns.Add("STSAmount");
            dtGetSchemeTransactionDeatails.Columns.Add("SWBCount");
            dtGetSchemeTransactionDeatails.Columns.Add("SWBAmount");
            dtGetSchemeTransactionDeatails.Columns.Add("SWPCount");
            dtGetSchemeTransactionDeatails.Columns.Add("SWPAmount");
            dtGetSchemeTransactionDeatails.Columns.Add("SWSCount");
            dtGetSchemeTransactionDeatails.Columns.Add("SWSAmount");
            dtGetSchemeTransactionDeatails.Columns.Add("PRJCount");
            dtGetSchemeTransactionDeatails.Columns.Add("PRJAmount");

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
                            //drGetSchemeTransactionDeatails["PA_AMCName"] = drAMCTransaction["PA_AMCName"].ToString();
                            drGetSchemeTransactionDeatails["SchemeCode"] = drAMCTransaction["PASP_SchemePlanCode"].ToString();
                            drGetSchemeTransactionDeatails["Scheme"] = drAMCTransaction["PASP_SchemePlanName"].ToString();
                            drGetSchemeTransactionDeatails["ExternalCode"] = drAMCTransaction["PASC_AMC_ExternalCode"].ToString();
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
            dtGetFolioTransactionDeatails.Columns.Add("Folio");
            dtGetFolioTransactionDeatails.Columns.Add("BUYCount");
            dtGetFolioTransactionDeatails.Columns.Add("BUYAmount");
            dtGetFolioTransactionDeatails.Columns.Add("SELCount");
            dtGetFolioTransactionDeatails.Columns.Add("SELAmount");
            dtGetFolioTransactionDeatails.Columns.Add("DVRCount");
            dtGetFolioTransactionDeatails.Columns.Add("DVRAmount");
            dtGetFolioTransactionDeatails.Columns.Add("DVPCount");
            dtGetFolioTransactionDeatails.Columns.Add("DVPAmount");
            dtGetFolioTransactionDeatails.Columns.Add("SIPCount");
            dtGetFolioTransactionDeatails.Columns.Add("SIPAmount");
            dtGetFolioTransactionDeatails.Columns.Add("BCICount");
            dtGetFolioTransactionDeatails.Columns.Add("BCIAmount");
            dtGetFolioTransactionDeatails.Columns.Add("BCOCount");
            dtGetFolioTransactionDeatails.Columns.Add("BCOAmount");
            dtGetFolioTransactionDeatails.Columns.Add("STBCount");
            dtGetFolioTransactionDeatails.Columns.Add("STBAmount");
            dtGetFolioTransactionDeatails.Columns.Add("STSCount");
            dtGetFolioTransactionDeatails.Columns.Add("STSAmount");
            dtGetFolioTransactionDeatails.Columns.Add("SWBCount");
            dtGetFolioTransactionDeatails.Columns.Add("SWBAmount");
            dtGetFolioTransactionDeatails.Columns.Add("SWPCount");
            dtGetFolioTransactionDeatails.Columns.Add("SWPAmount");
            dtGetFolioTransactionDeatails.Columns.Add("SWSCount");
            dtGetFolioTransactionDeatails.Columns.Add("SWSAmount");
            dtGetFolioTransactionDeatails.Columns.Add("PRJCount");
            dtGetFolioTransactionDeatails.Columns.Add("PRJAmount");

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
            dtGetCategoryTransactionDeatails.Columns.Add("BUYCount");
            dtGetCategoryTransactionDeatails.Columns.Add("BUYAmount");
            dtGetCategoryTransactionDeatails.Columns.Add("SELCount");
            dtGetCategoryTransactionDeatails.Columns.Add("SELAmount");
            dtGetCategoryTransactionDeatails.Columns.Add("DVRCount");
            dtGetCategoryTransactionDeatails.Columns.Add("DVRAmount");
            dtGetCategoryTransactionDeatails.Columns.Add("DVPCount");
            dtGetCategoryTransactionDeatails.Columns.Add("DVPAmount");
            dtGetCategoryTransactionDeatails.Columns.Add("SIPCount");
            dtGetCategoryTransactionDeatails.Columns.Add("SIPAmount");
            dtGetCategoryTransactionDeatails.Columns.Add("BCICount");
            dtGetCategoryTransactionDeatails.Columns.Add("BCIAmount");
            dtGetCategoryTransactionDeatails.Columns.Add("BCOCount");
            dtGetCategoryTransactionDeatails.Columns.Add("BCOAmount");
            dtGetCategoryTransactionDeatails.Columns.Add("STBCount");
            dtGetCategoryTransactionDeatails.Columns.Add("STBAmount");
            dtGetCategoryTransactionDeatails.Columns.Add("STSCount");
            dtGetCategoryTransactionDeatails.Columns.Add("STSAmount");
            dtGetCategoryTransactionDeatails.Columns.Add("SWBCount");
            dtGetCategoryTransactionDeatails.Columns.Add("SWBAmount");
            dtGetCategoryTransactionDeatails.Columns.Add("SWPCount");
            dtGetCategoryTransactionDeatails.Columns.Add("SWPAmount");
            dtGetCategoryTransactionDeatails.Columns.Add("SWSCount");
            dtGetCategoryTransactionDeatails.Columns.Add("SWSAmount");
            dtGetCategoryTransactionDeatails.Columns.Add("PRJCount");
            dtGetCategoryTransactionDeatails.Columns.Add("PRJAmount");
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
            dtGetBranchTransactionDeatails.Columns.Add("BUYCount");
            dtGetBranchTransactionDeatails.Columns.Add("BUYAmount");
            dtGetBranchTransactionDeatails.Columns.Add("SELCount");
            dtGetBranchTransactionDeatails.Columns.Add("SELAmount");
            dtGetBranchTransactionDeatails.Columns.Add("DVRCount");
            dtGetBranchTransactionDeatails.Columns.Add("DVRAmount");
            dtGetBranchTransactionDeatails.Columns.Add("DVPCount");
            dtGetBranchTransactionDeatails.Columns.Add("DVPAmount");
            dtGetBranchTransactionDeatails.Columns.Add("SIPCount");
            dtGetBranchTransactionDeatails.Columns.Add("SIPAmount");
            dtGetBranchTransactionDeatails.Columns.Add("BCICount");
            dtGetBranchTransactionDeatails.Columns.Add("BCIAmount");
            dtGetBranchTransactionDeatails.Columns.Add("BCOCount");
            dtGetBranchTransactionDeatails.Columns.Add("BCOAmount");
            dtGetBranchTransactionDeatails.Columns.Add("STBCount");
            dtGetBranchTransactionDeatails.Columns.Add("STBAmount");
            dtGetBranchTransactionDeatails.Columns.Add("STSCount");
            dtGetBranchTransactionDeatails.Columns.Add("STSAmount");
            dtGetBranchTransactionDeatails.Columns.Add("SWBCount");
            dtGetBranchTransactionDeatails.Columns.Add("SWBAmount");
            dtGetBranchTransactionDeatails.Columns.Add("SWPCount");
            dtGetBranchTransactionDeatails.Columns.Add("SWPAmount");
            dtGetBranchTransactionDeatails.Columns.Add("SWSCount");
            dtGetBranchTransactionDeatails.Columns.Add("SWSAmount");
            dtGetBranchTransactionDeatails.Columns.Add("PRJCount");
            dtGetBranchTransactionDeatails.Columns.Add("PRJAmount");

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
            }
            if (gridName == "SchemeWise")
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
            }
            if (gridName == "FolioWise")
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
            }
            if (gridName == "CategoryWise")
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
            }
            if (gridName == "BranchWise")
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
    }
}