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
using VOAssociates;
using BOAssociates;
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
        AssociatesVO associatesVo = new AssociatesVO();
        UserVo userVo = new UserVo();
        DateTime dtTo = new DateTime();
        DateBo dtBo = new DateBo();
        DateTime dtFrom = new DateTime();

        int advisorId = 0;
        String userType;
        int rmId = 0;
        int bmID = 0;
        int all = 0;
        int AgentId = 0;
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
            associatesVo = (AssociatesVO)Session["associatesVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
                userType = "associates";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            advisorId = advisorVo.advisorId;
            int RMId = rmVo.RMId;
            
            rmId = rmVo.RMId;
            bmID = rmVo.RMId;
            //AgentId = associatesVo.AAC_AdviserAgentId;
            if (!IsPostBack)
            {
                if (!Convert.ToBoolean(advisorVo.MultiBranch))
                {
                    ddlAction.Items.RemoveAt(0);

                }

                rbtnPickDate.Checked = true;
                rbtnPickPeriod.Checked = false;
                divDateRange.Visible = true;
                divDatePeriod.Visible = false;

                //trDivProduct.Visible = true;
                //trDivOrganization.Visible = false;
                //ShowHideMISMenu("Organization");
               
                trPnlAMC.Visible = false;
                trPnlScheme.Visible = false;
                trPnlBranch.Visible = false;
                trPnlFolio.Visible = false;
                trPnlCategory.Visible = false;
                trPnlRM.Visible = false;
                trPnlZoneCluster.Visible = false;
                BindCategory();
                if (userType == "advisor" || userType == "rm")
                {
                    BindBranchDropDown();
                    BindRMDropDown();
                    if (userType == "rm")
                    {
                        ddlBranch.Enabled = false;
                        ddlRM.SelectedValue = rmVo.RMId.ToString();
                        ddlRM.Enabled = false;
                        //ddlAction.SelectedIndex = 1;
                        if (Convert.ToBoolean(advisorVo.MultiBranch))
                        {
                            ddlAction.Items.RemoveAt(0);

                        }
                        

                        //Action.Visible = false;
                        //ddlAction.Visible = false;
                        //  trBranchRM.Visible = false;
                    }
                }
                else if (userType == "rm")
                {
                    //ddlBranch.SelectedValue = rmVo.RMId.ToString();
                    // ddlRM.SelectedValue=rmVo.RMId.ToString();
                    //Action.Visible = false;
                    //ddlAction.Visible = false;
                  //  trBranchRM.Visible = false;
                }
                if (userType == "bm")
                {
                    BindBranchForBMDropDown();
                    BindRMforBranchDropdown(0, bmID);
                    if (Convert.ToBoolean(advisorVo.MultiBranch))
                    {
                        ddlAction.Items.RemoveAt(0);

                    }
                }
                if (userType == "associates")
                {
                    BindBranchDropDown();
                    BindRMDropDown();
                    if (Convert.ToBoolean(advisorVo.MultiBranch))
                    {
                        ddlAction.Items.RemoveAt(0);

                    }
                    trBranchRM.Visible = false;
                }
                //LatestValuationdate = adviserMISBo.GetLatestValuationDateFromHistory(advisorId, "MF");
                txtFromDate.SelectedDate =DateTime.Now;
                txtToDate.SelectedDate = DateTime.Now;
                ShowHideMISMenu(ddlAction.SelectedValue.ToString());
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
                divDateRange.Visible = true;
                divDatePeriod.Visible = false;
            }
            else if (rbtnPickPeriod.Checked == true)
            {
                divDateRange.Visible = false;
                divDatePeriod.Visible = true;
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
            dsGetAMCTransactionDeatails = adviserMFMIS.GetAMCTransactionDeatails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), int.Parse(hdnAgentId.Value));

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
            if (dsGetAMCTransactionDeatails.Tables[0] != null)
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
            lblMFMISType.Text = "AMC/Scheme/Category Wise";
        }

        private void BindSchemeWiseTransactionDetails()
        {
            int SchemeCode = 0;
            int SchemeCodeOld = 0;
            DataSet dsGetSchemeTransactionDeatails = new DataSet();
            dsGetSchemeTransactionDeatails = adviserMFMIS.GetSchemeTransactionDeatails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), gvAmcCode, hdnCategory.Value, int.Parse(hdnAgentId.Value));

            DataTable dtGetSchemeTransactionDeatails = new DataTable();

            dtGetSchemeTransactionDeatails.Columns.Add("AMC");
            dtGetSchemeTransactionDeatails.Columns.Add("SchemeCode");
            dtGetSchemeTransactionDeatails.Columns.Add("Scheme");
            //dtGetSchemeTransactionDeatails.Columns.Add("ExternalCode");
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


            #region newly added
            dtGetSchemeTransactionDeatails.Columns.Add("ABYCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("ABYAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("BIRCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("BIRAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("BNSCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("BNSAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("CNICount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("CNIAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("CNOCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("CNOAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("DSICount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("DSIAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("DSOCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("DSOAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("HLDCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("HLDAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("NFOCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("NFOAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("RRJCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("RRJAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("SRJCount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("SRJAmount", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("GrossRedemption", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("GrossInvestment", typeof(double));
            dtGetSchemeTransactionDeatails.Columns.Add("Net", typeof(double));
            #endregion



            //--------------------Default Value ------------------

            #region Data Table Default value

            dtGetSchemeTransactionDeatails.Columns["GrossRedemption"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["GrossInvestment"].DefaultValue = 0;

            dtGetSchemeTransactionDeatails.Columns["Net"].DefaultValue = 0;

            dtGetSchemeTransactionDeatails.Columns["ABYCount"].DefaultValue = 0;

            dtGetSchemeTransactionDeatails.Columns["ABYAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["BIRCount"].DefaultValue = 0;

            dtGetSchemeTransactionDeatails.Columns["BIRAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["BNSCount"].DefaultValue = 0;

            dtGetSchemeTransactionDeatails.Columns["BNSAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["CNICount"].DefaultValue = 0;

            dtGetSchemeTransactionDeatails.Columns["CNIAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["CNOCount"].DefaultValue = 0;

            dtGetSchemeTransactionDeatails.Columns["CNOAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["DSICount"].DefaultValue = 0;

            dtGetSchemeTransactionDeatails.Columns["DSIAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["DSOCount"].DefaultValue = 0;

            dtGetSchemeTransactionDeatails.Columns["DSOAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["HLDCount"].DefaultValue = 0;

            dtGetSchemeTransactionDeatails.Columns["HLDAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["NFOCount"].DefaultValue = 0;

            dtGetSchemeTransactionDeatails.Columns["NFOAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["RRJCount"].DefaultValue = 0;

            dtGetSchemeTransactionDeatails.Columns["RRJAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["SRJCount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["SRJAmount"].DefaultValue = 0;


            dtGetSchemeTransactionDeatails.Columns["DVRCount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["DVRAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["DVPCount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["DVPAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["SIPCount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["SIPAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["BCICount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["BCIAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["BCOCount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["BCOAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["STBCount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["STBAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["STSCount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["STSAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["SWBCount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["SWBAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["SWPCount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["SWPAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["SWSCount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["SWSAmount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["PRJCount"].DefaultValue = 0;
            dtGetSchemeTransactionDeatails.Columns["PRJAmount"].DefaultValue = 0;

            #endregion



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
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossInvestment"] = double.Parse(drGetSchemeTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["BUYAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SEL":
                                            {
                                                drGetSchemeTransactionDeatails["SELCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["SELAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossRedemption"] = double.Parse(drGetSchemeTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["SELAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "DVR":
                                            {
                                                drGetSchemeTransactionDeatails["DVRCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["DVRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossInvestment"] = double.Parse(drGetSchemeTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["DVRAmount"].ToString());
                                                }
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
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossInvestment"] = double.Parse(drGetSchemeTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["SIPAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "BCI":
                                            {
                                                drGetSchemeTransactionDeatails["BCICount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["BCIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossInvestment"] = double.Parse(drGetSchemeTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["BCIAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "BCO":
                                            {
                                                drGetSchemeTransactionDeatails["BCOCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["BCOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossRedemption"] = double.Parse(drGetSchemeTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["BCOAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "STB":
                                            {
                                                drGetSchemeTransactionDeatails["STBCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["STBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossInvestment"] = double.Parse(drGetSchemeTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["STBAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "STS":
                                            {
                                                drGetSchemeTransactionDeatails["STSCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["STSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossRedemption"] = double.Parse(drGetSchemeTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["STSAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SWB":
                                            {
                                                drGetSchemeTransactionDeatails["SWBCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["SWBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossInvestment"] = double.Parse(drGetSchemeTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["SWBAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SWP":
                                            {
                                                drGetSchemeTransactionDeatails["SWPCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["SWPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossRedemption"] = double.Parse(drGetSchemeTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["SWPAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SWS":
                                            {
                                                drGetSchemeTransactionDeatails["SWSCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["SWSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossRedemption"] = double.Parse(drGetSchemeTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["SWSAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "PRJ":
                                            {
                                                drGetSchemeTransactionDeatails["PRJCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["PRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        #region newly added
                                        case "ABY":
                                            {
                                                drGetSchemeTransactionDeatails["ABYCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["ABYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossInvestment"] = double.Parse(drGetSchemeTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["ABYAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "BIR":
                                            {
                                                drGetSchemeTransactionDeatails["BIRCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["BIRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossInvestment"] = double.Parse(drGetSchemeTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["BIRAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "BNS":
                                            {
                                                drGetSchemeTransactionDeatails["BNSCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["BNSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossInvestment"] = double.Parse(drGetSchemeTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["BNSAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "CNI":
                                            {
                                                drGetSchemeTransactionDeatails["CNICount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["CNIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossInvestment"] = double.Parse(drGetSchemeTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["CNIAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "CNO":
                                            {
                                                drGetSchemeTransactionDeatails["CNOCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["CNOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossRedemption"] = double.Parse(drGetSchemeTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["CNOAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "DSI":
                                            {
                                                drGetSchemeTransactionDeatails["DSICount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["DSIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossInvestment"] = double.Parse(drGetSchemeTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["DSIAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "DSO":
                                            {
                                                drGetSchemeTransactionDeatails["DSOCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["DSOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossRedemption"] = double.Parse(drGetSchemeTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["DSOAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "HLD":
                                            {
                                                drGetSchemeTransactionDeatails["HLDCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["HLDAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossInvestment"] = double.Parse(drGetSchemeTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["HLDAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "NFO":
                                            {
                                                drGetSchemeTransactionDeatails["NFOCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["NFOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossInvestment"] = double.Parse(drGetSchemeTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["NFOAmount"].ToString());
                                                }

                                                break;
                                            }


                                        case "RRJ":
                                            {
                                                drGetSchemeTransactionDeatails["RRJCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["RRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossInvestment"] = double.Parse(drGetSchemeTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["RRJAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "SRJ":
                                            {
                                                drGetSchemeTransactionDeatails["SRJCount"] = dr["TrnsCount"].ToString();
                                                drGetSchemeTransactionDeatails["SRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetSchemeTransactionDeatails["GrossRedemption"] = double.Parse(drGetSchemeTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetSchemeTransactionDeatails["SRJAmount"].ToString());
                                                }

                                                break;
                                            }
                                        #endregion
                                    }

                                }
                            }

                            drGetSchemeTransactionDeatails["Net"] = double.Parse(drGetSchemeTransactionDeatails["GrossInvestment"].ToString()) - double.Parse(drGetSchemeTransactionDeatails["GrossRedemption"].ToString());


                            dtGetSchemeTransactionDeatails.Rows.Add(drGetSchemeTransactionDeatails);
                        }//*

                    }//**

                }//***
                gvSchemeWise.DataSource = dtGetSchemeTransactionDeatails;
                gvSchemeWise.DataBind();
                gvSchemeWise.Visible = true;
                pnlScheme.Visible = true;
                this.gvSchemeWise.GroupingSettings.RetainGroupFootersVisibility = true;
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
            lblMFMISType.Text = "Staff/Customer/Folio Wise";
        }

        private void BindFolioWiseTransactionDetails()
        {
            int AcountId = 0;
            int AcountIdOld = 0;
            DataSet dsGetFolioTransactionDeatails = new DataSet();
            dsGetFolioTransactionDeatails = adviserMFMIS.GetFolioTransactionDeatails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), gvSchemeCode,int.Parse(hdnAgentId.Value));

            DataTable dtGetFolioTransactionDeatails = new DataTable();

            #region newly added
            dtGetFolioTransactionDeatails.Columns.Add("Net", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("GrossInvestment", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("GrossRedemption", typeof(double));
            #endregion

            dtGetFolioTransactionDeatails.Columns.Add("Customer");
            dtGetFolioTransactionDeatails.Columns.Add("Parent");
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

            #region newly added
            dtGetFolioTransactionDeatails.Columns.Add("ABYCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("ABYAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("BIRCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("BIRAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("BNSCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("BNSAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("CNICount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("CNIAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("CNOCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("CNOAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("DSICount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("DSIAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("DSOCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("DSOAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("HLDCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("HLDAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("NFOCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("NFOAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("RRJCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("RRJAmount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("SRJCount", typeof(double));
            dtGetFolioTransactionDeatails.Columns.Add("SRJAmount", typeof(double));
            #endregion



            //--------------------Default Value ------------------

            #region Data Table Default value

            dtGetFolioTransactionDeatails.Columns["GrossRedemption"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["GrossInvestment"].DefaultValue = 0;

            dtGetFolioTransactionDeatails.Columns["Net"].DefaultValue = 0;

            dtGetFolioTransactionDeatails.Columns["ABYCount"].DefaultValue = 0;

            dtGetFolioTransactionDeatails.Columns["ABYAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["BIRCount"].DefaultValue = 0;

            dtGetFolioTransactionDeatails.Columns["BIRAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["BNSCount"].DefaultValue = 0;

            dtGetFolioTransactionDeatails.Columns["BNSAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["CNICount"].DefaultValue = 0;

            dtGetFolioTransactionDeatails.Columns["CNIAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["CNOCount"].DefaultValue = 0;

            dtGetFolioTransactionDeatails.Columns["CNOAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["DSICount"].DefaultValue = 0;

            dtGetFolioTransactionDeatails.Columns["DSIAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["DSOCount"].DefaultValue = 0;

            dtGetFolioTransactionDeatails.Columns["DSOAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["HLDCount"].DefaultValue = 0;

            dtGetFolioTransactionDeatails.Columns["HLDAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["NFOCount"].DefaultValue = 0;

            dtGetFolioTransactionDeatails.Columns["NFOAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["RRJCount"].DefaultValue = 0;

            dtGetFolioTransactionDeatails.Columns["RRJAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["SRJCount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["SRJAmount"].DefaultValue = 0;


            dtGetFolioTransactionDeatails.Columns["DVRCount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["DVRAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["DVPCount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["DVPAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["SIPCount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["SIPAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["BCICount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["BCIAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["BCOCount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["BCOAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["STBCount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["STBAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["STSCount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["STSAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["SWBCount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["SWBAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["SWPCount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["SWPAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["SWSCount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["SWSAmount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["PRJCount"].DefaultValue = 0;
            dtGetFolioTransactionDeatails.Columns["PRJAmount"].DefaultValue = 0;

             dtGetFolioTransactionDeatails.Columns["BUYCount"].DefaultValue = 0;
             dtGetFolioTransactionDeatails.Columns["BUYAmount"].DefaultValue = 0;
             dtGetFolioTransactionDeatails.Columns["SELCount"].DefaultValue = 0;
             dtGetFolioTransactionDeatails.Columns["SELAmount"].DefaultValue = 0;

            #endregion


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


                            //DataRow drCluster = dr;
                            //string categoryCode = dr["CategoryCode"].ToString();

                            //drCheckIfCategoryExistForCluster = dtGetClusterTransactionDeatails.Select("CategoryCode= '" + categoryCode + "'" + "AND " + "ZoneClusterId='" + branchId + "'");

                            //if (drCheckIfCategoryExistForCluster.Count() > 0)
                            //{
                           // string transactiontype = drFolioTransaction["WMTT_TransactionClassificationCode"].ToString();
                            //SwitchCaseOnTransactionClassificationCode(transactiontype, ref drFolioTransaction, ref drCluster);
                            //}


                            drTransactionFolioWise = dtGetFolioTransaction.Select("CMFA_AccountId=" + AcountId.ToString());
                            //drGetSchemeTransactionDeatails["PA_AMCName"] = drAMCTransaction["PA_AMCName"].ToString();
                            drGetFolioTransactionDeatails["Customer"] = drFolioTransaction["CustomerName"].ToString();
                            drGetFolioTransactionDeatails["Parent"] = drFolioTransaction["Parent"].ToString();
                            drGetFolioTransactionDeatails["RMName"] = drFolioTransaction["RmName"].ToString();
                            drGetFolioTransactionDeatails["Folio"] = drFolioTransaction["CMFA_FolioNum"].ToString();
                            //if (drGetFolioTransactionDeatails["Folio"].ToString() == "FEDLE")
                            //{

                            //}
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
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossInvestment"] = double.Parse(drGetFolioTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetFolioTransactionDeatails["BUYAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SEL":
                                            {
                                                drGetFolioTransactionDeatails["SELCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["SELAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossRedemption"] = double.Parse(drGetFolioTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetFolioTransactionDeatails["SELAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "DVR":
                                            {
                                                drGetFolioTransactionDeatails["DVRCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["DVRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossInvestment"] = double.Parse(drGetFolioTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetFolioTransactionDeatails["DVRAmount"].ToString());
                                                }
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
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossInvestment"] = double.Parse(drGetFolioTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetFolioTransactionDeatails["SIPAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "BCI":
                                            {
                                                drGetFolioTransactionDeatails["BCICount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["BCIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossInvestment"] = double.Parse(drGetFolioTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetFolioTransactionDeatails["BCIAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "BCO":
                                            {
                                                drGetFolioTransactionDeatails["BCOCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["BCOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossRedemption"] = double.Parse(drGetFolioTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetFolioTransactionDeatails["BCOAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "STB":
                                            {
                                                drGetFolioTransactionDeatails["STBCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["STBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossInvestment"] = double.Parse(drGetFolioTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetFolioTransactionDeatails["STBAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "STS":
                                            {
                                                drGetFolioTransactionDeatails["STSCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["STSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossRedemption"] = double.Parse(drGetFolioTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetFolioTransactionDeatails["STSAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SWB":
                                            {
                                                drGetFolioTransactionDeatails["SWBCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["SWBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossInvestment"] = double.Parse(drGetFolioTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetFolioTransactionDeatails["SWBAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SWP":
                                            {
                                                drGetFolioTransactionDeatails["SWPCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["SWPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossRedemption"] = double.Parse(drGetFolioTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetFolioTransactionDeatails["SWPAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SWS":
                                            {
                                                drGetFolioTransactionDeatails["SWSCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["SWSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossRedemption"] = double.Parse(drGetFolioTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetFolioTransactionDeatails["SWSAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "PRJ":
                                            {
                                                drGetFolioTransactionDeatails["PRJCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["PRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }

                                        #region newly added
                                        case "ABY":
                                            {
                                                drGetFolioTransactionDeatails["ABYCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["ABYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossInvestment"] = double.Parse(drGetFolioTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetFolioTransactionDeatails["ABYAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "BIR":
                                            {
                                                drGetFolioTransactionDeatails["BIRCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["BIRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossInvestment"] = double.Parse(drGetFolioTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetFolioTransactionDeatails["BIRAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "BNS":
                                            {
                                                drGetFolioTransactionDeatails["BNSCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["BNSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossInvestment"] = double.Parse(drGetFolioTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetFolioTransactionDeatails["BNSAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "CNI":
                                            {
                                                drGetFolioTransactionDeatails["CNICount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["CNIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossInvestment"] = double.Parse(drGetFolioTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetFolioTransactionDeatails["CNIAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "CNO":
                                            {
                                                drGetFolioTransactionDeatails["CNOCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["CNOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossRedemption"] = double.Parse(drGetFolioTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetFolioTransactionDeatails["CNOAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "DSI":
                                            {
                                                drGetFolioTransactionDeatails["DSICount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["DSIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossInvestment"] = double.Parse(drGetFolioTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetFolioTransactionDeatails["DSIAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "DSO":
                                            {
                                                drGetFolioTransactionDeatails["DSOCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["DSOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossRedemption"] = double.Parse(drGetFolioTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetFolioTransactionDeatails["DSOAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "HLD":
                                            {
                                                drGetFolioTransactionDeatails["HLDCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["HLDAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossInvestment"] = double.Parse(drGetFolioTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetFolioTransactionDeatails["HLDAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "NFO":
                                            {
                                                drGetFolioTransactionDeatails["NFOCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["NFOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossInvestment"] = double.Parse(drGetFolioTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetFolioTransactionDeatails["NFOAmount"].ToString());
                                                }

                                                break;
                                            }


                                        case "RRJ":
                                            {
                                                drGetFolioTransactionDeatails["RRJCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["RRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossInvestment"] = double.Parse(drGetFolioTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetFolioTransactionDeatails["RRJAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "SRJ":
                                            {
                                                drGetFolioTransactionDeatails["SRJCount"] = dr["TrnsCount"].ToString();
                                                drGetFolioTransactionDeatails["SRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetFolioTransactionDeatails["GrossRedemption"] = double.Parse(drGetFolioTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetFolioTransactionDeatails["SRJAmount"].ToString());
                                                }

                                                break;
                                            }
                                        #endregion
                                    }

                                }
                            }
                            drGetFolioTransactionDeatails["Net"] = double.Parse(drGetFolioTransactionDeatails["GrossInvestment"].ToString()) - double.Parse(drGetFolioTransactionDeatails["GrossRedemption"].ToString());

                            dtGetFolioTransactionDeatails.Rows.Add(drGetFolioTransactionDeatails);


                        }//*

                    }//**

                }//***
                gvFolioWise.DataSource = dtGetFolioTransactionDeatails;
                gvFolioWise.DataBind();
                gvFolioWise.Visible = true;
                pnlFolio.Visible = true;

                this.gvFolioWise.GroupingSettings.RetainGroupFootersVisibility = true;
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
            dsGetCategoryTransactionDeatails = adviserMFMIS.GetCategoryTransactionDeatails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), hdnCategory.Value, int.Parse(hdnAgentId.Value));

            DataTable dtGetCategoryTransactionDeatails = new DataTable();

            dtGetCategoryTransactionDeatails.Columns.Add("Category");
            dtGetCategoryTransactionDeatails.Columns.Add("SubCategory");
            dtGetCategoryTransactionDeatails.Columns.Add("BUYCount", typeof(double));
            dtGetCategoryTransactionDeatails.Columns.Add("BUYAmount", typeof(double));
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
                        drTransactionCategoryWise = dtGetCategoryTransaction.Select("PAISC_AssetInstrumentSubCategoryCode=" + "'" + subcategoryCode + "'");

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

        protected void lnkZoneCluster_Click(object sender, EventArgs e)
        {
            SetParameters();
            showHideGrid("ZoneClusterWise");
            BindZoneClusterTransactionDetails();
            lblMFMISType.Text = "Zone/Cluster/Branch Wise";

        }

        private void BindBranchWiseTransactionDetails()
        {
            int BranchId = 0;
            int BranchIdOld = 0;
            DataSet dsGetBranchTransactionDeatails = new DataSet();
            dsGetBranchTransactionDeatails = adviserMFMIS.GetBranchTransactionDeatails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value),int.Parse(hdnAgentId.Value));

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
            lblMFMISType.Text = "Staff/Customer/Folio Wise";
        }

        private void SetParameters()
        {
            if (userType == "advisor")
            {
                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnAll.Value = "0";
                    hdnAgentId.Value = "0";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAgentId.Value = "0";
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = "0";
                    hdnAgentId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRM.SelectedValue; ;
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAgentId.Value = "0";
                    hdnAll.Value = "3";
                }

            }
            else if (userType == "rm")
            {
                hdnrmId.Value = rmVo.RMId.ToString();
                hdnAgentId.Value = "0";
                hdnAll.Value = "0";

            }
            else if (userType == "bm")
            {
                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {

                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnAll.Value = "0";
                    hdnrmId.Value = "0";
                    hdnAgentId.Value = "0";
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                    hdnAgentId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnAgentId.Value = "0";
                    hdnrmId.Value = ddlRM.SelectedValue; ;
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAgentId.Value = "0";
                    hdnAll.Value = "3";
                }
            }
            else if (userType == "associates")
            {
                hdnAgentId.Value = associatesVo.AAC_AdviserAgentId.ToString();
                hdnadviserId.Value = "0";
                hdnbranchHeadId.Value = "0";
                hdnbranchId.Value = "0";
                hdnrmId.Value = "0";
                hdnAll.Value = "0";

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
                trPnlAMC.Visible = true;
                trPnlScheme.Visible = false;
                trPnlBranch.Visible = false;
                trPnlFolio.Visible = false;
                trPnlCategory.Visible = false;
                btnAMCExport.Visible = true;
                btnSchemeExport.Visible = false;
                btnFolioExport.Visible = false;
                btnBranchExport.Visible = false;
                btnCategoryExport.Visible = false;
                btnRMExport.Visible = false;
                divRM.Visible = false;
                trPnlRM.Visible = false;
                btnClusterZoneExport.Visible = false;
                trPnlZoneCluster.Visible = false;
                divZoneWise.Visible = false;
            }
            else if (gridName == "SchemeWise")
            {
                dvScheme.Visible = true;
                divGvAmcWise.Visible = false;
                divBranch.Visible = false;
                divFolioWise.Visible = false;
                divCategory.Visible = false;
                trPnlAMC.Visible = false;
                trPnlScheme.Visible = true;
                trPnlBranch.Visible = false;
                trPnlFolio.Visible = false;
                trPnlCategory.Visible = false;
                btnAMCExport.Visible = false;
                btnSchemeExport.Visible = true;
                btnFolioExport.Visible = false;
                btnBranchExport.Visible = false;
                btnCategoryExport.Visible = false;
                btnRMExport.Visible = false;
                divRM.Visible = false;
                trPnlRM.Visible = false;
                btnClusterZoneExport.Visible = false;
                trPnlZoneCluster.Visible = false;
                divZoneWise.Visible = false;
            }
            else if (gridName == "FolioWise")
            {
                dvScheme.Visible = false;
                divGvAmcWise.Visible = false;
                divBranch.Visible = false;
                divFolioWise.Visible = true;
                divCategory.Visible = false;
                trPnlAMC.Visible = false;
                trPnlScheme.Visible = false;
                trPnlBranch.Visible = false;
                trPnlFolio.Visible = true;
                trPnlCategory.Visible = false;
                btnAMCExport.Visible = false;
                btnSchemeExport.Visible = false;
                btnFolioExport.Visible = true;
                btnBranchExport.Visible = false;
                btnCategoryExport.Visible = false;
                btnRMExport.Visible = false;
                divRM.Visible = false;
                trPnlRM.Visible = false;
                btnClusterZoneExport.Visible = false;
                trPnlZoneCluster.Visible = false;
                divZoneWise.Visible = false;
            }
            else if (gridName == "CategoryWise")
            {
                dvScheme.Visible = false;
                divGvAmcWise.Visible = false;
                divBranch.Visible = false;
                divFolioWise.Visible = false;
                divCategory.Visible = true;
                trPnlAMC.Visible = false;
                trPnlScheme.Visible = false;
                trPnlBranch.Visible = false;
                trPnlFolio.Visible = false;
                trPnlCategory.Visible = true;
                btnAMCExport.Visible = false;
                btnSchemeExport.Visible = false;
                btnFolioExport.Visible = false;
                btnBranchExport.Visible = false;
                btnCategoryExport.Visible = true;
                btnRMExport.Visible = false;
                divRM.Visible = false;
                trPnlRM.Visible = false;
                btnClusterZoneExport.Visible = false;
                trPnlZoneCluster.Visible = false;
                divZoneWise.Visible = false;
            }
            else if (gridName == "BranchWise")
            {
                dvScheme.Visible = false;
                divGvAmcWise.Visible = false;
                divBranch.Visible = true;
                divFolioWise.Visible = false;
                divCategory.Visible = false;
                trPnlAMC.Visible = false;
                trPnlScheme.Visible = false;
                trPnlBranch.Visible = true;
                trPnlFolio.Visible = false;
                trPnlCategory.Visible = false;
                btnAMCExport.Visible = false;
                btnSchemeExport.Visible = false;
                btnFolioExport.Visible = false;
                btnBranchExport.Visible = true;
                btnCategoryExport.Visible = false;
                btnRMExport.Visible = false;
                divRM.Visible = false;
                trPnlRM.Visible = false;
                btnClusterZoneExport.Visible = false;
                trPnlZoneCluster.Visible = false;
                divZoneWise.Visible = false;
            }
            else if (gridName == "RMWise")
            {
                dvScheme.Visible = false;
                divGvAmcWise.Visible = false;
                divBranch.Visible = false;
                divFolioWise.Visible = false;
                divCategory.Visible = false;
                trPnlAMC.Visible = false;
                trPnlScheme.Visible = false;
                trPnlBranch.Visible = false;
                trPnlFolio.Visible = false;
                trPnlCategory.Visible = false;
                btnAMCExport.Visible = false;
                btnSchemeExport.Visible = false;
                btnFolioExport.Visible = false;
                btnBranchExport.Visible = false;
                btnCategoryExport.Visible = false;
                btnRMExport.Visible = true;
                divRM.Visible = true;
                trPnlRM.Visible = true;
                btnClusterZoneExport.Visible = false;
                trPnlZoneCluster.Visible = false;
                divZoneWise.Visible = false;
            }
            else if (gridName == "ZoneClusterWise")
            {
                dvScheme.Visible = false;
                divGvAmcWise.Visible = false;
                divBranch.Visible = false;
                divZoneWise.Visible = true;
                divFolioWise.Visible = false;
                divRM.Visible = false;
                divCategory.Visible = false;

                trPnlAMC.Visible = false;
                trPnlScheme.Visible = false;
                trPnlZoneCluster.Visible = true;
                trPnlFolio.Visible = false;
                trPnlCategory.Visible = false;
                trPnlRM.Visible = false;

                btnAMCExport.Visible = false;
                btnSchemeExport.Visible = false;
                btnFolioExport.Visible = false;
                btnBranchExport.Visible = false;
                btnCategoryExport.Visible = false;
                btnRMExport.Visible = false;
                btnClusterZoneExport.Visible = true;


            }

        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dvSectionHeading.Visible = false;
            //gvSchemeWise.Visible = false;
            pnlZoneCluster.Visible = false;
            pnlFolio.Visible = false;
            pnlScheme.Visible = false;

            if (ddlAction.SelectedValue == "Product")
            {
                ShowHideMISMenu("Product");
                showHideGrid("SchemeWise");
            }
            else if (ddlAction.SelectedValue == "Organization")
            {
                ShowHideMISMenu("Organization");
                showHideGrid("ZoneClusterWise");
            }
            else if (ddlAction.SelectedValue == "Staff")
            {
                ShowHideMISMenu("Staff");
                showHideGrid("FolioWise");
            }
        }

        protected void ShowHideMISMenu(string misType)
        {
            if (misType == "Product")
            {
                //trDivProduct.Visible = true;
                //trDivOrganization.Visible = false;
                //trDivStaff.Visible = false;

                lblCategory.Visible = false;
                ddlCategory.Visible = false;
            }
            else if (misType == "Organization")
            {
                //trDivProduct.Visible = false;
                //trDivOrganization.Visible = true;
                //trDivStaff.Visible = false;

                lblCategory.Visible = false;
                ddlCategory.Visible = false;

                hdnCategory.Value = "";
            }
            else if (misType == "Staff")
            {
                //trDivProduct.Visible = false;
                //trDivOrganization.Visible = false;
                //trDivStaff.Visible = true;

                lblCategory.Visible = false;
                ddlCategory.Visible = false;
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
                                    int selectedRow = gvr.ItemIndex + 1;
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
            dsGetRMTransactionDeatails = adviserMFMIS.GetRMTransactionDeatails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value),int.Parse(hdnAgentId.Value));

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

        protected void btnClusterZoneExport_Click(object sender, ImageClickEventArgs e)
        {
            gvZoneClusterWise.ExportSettings.OpenInNewWindow = true;
            gvZoneClusterWise.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvZoneClusterWise.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvZoneClusterWise.MasterTableView.ExportToExcel();
        }

        protected void gvRM_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetRMTransactionDeatails = new DataTable();
            dtGetRMTransactionDeatails = (DataTable)Cache["RMTransactionDeatails" + userVo.UserId];
            gvRM.DataSource = dtGetRMTransactionDeatails;
            gvRM.Visible = true;
        }

        private void BindZoneClusterTransactionDetails()
        {
            int branchId = 0;
            int branchIdOld = 0;
            DataSet dsGetClusterTransactionDeatails = new DataSet();


            dsGetClusterTransactionDeatails = adviserMFMIS.GetAllClusterTransactionDeatails(int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), hdnCategory.Value);
            DataTable dtGetClusterTransactionDeatails = new DataTable();


            #region Data Table Column Defination

            dtGetClusterTransactionDeatails.Columns.Add("ZoneName");
            dtGetClusterTransactionDeatails.Columns.Add("ClusterName");
            dtGetClusterTransactionDeatails.Columns.Add("ZoneClusterId");
            dtGetClusterTransactionDeatails.Columns.Add("BranchName");
            dtGetClusterTransactionDeatails.Columns.Add("BranchId");
            dtGetClusterTransactionDeatails.Columns.Add("CategoryName");
            dtGetClusterTransactionDeatails.Columns.Add("CategoryCode");

            dtGetClusterTransactionDeatails.Columns.Add("Net", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("GrossInvestment", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("GrossRedemption", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("DVPCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("DVPAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("BUYCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("BUYAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("SELCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("SELAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("DVRCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("DVRAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("SIPCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("SIPAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("BCICount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("BCIAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("BCOCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("BCOAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("STBCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("STBAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("STSCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("STSAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("SWBCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("SWBAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("SWPCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("SWPAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("SWSCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("SWSAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("PRJCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("PRJAmount", typeof(double));



            //------------------New Trxn Type Added ---------------------\\

            dtGetClusterTransactionDeatails.Columns.Add("AddCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("AddAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("BIRCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("BIRAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("BNSCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("BNSAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("CNICount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("CNIAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("DSICount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("DSIAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("HLDCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("HLDAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("NFOCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("NFOAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("RRJCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("RRJAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("CNOCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("CNOAmount", typeof(double));


            dtGetClusterTransactionDeatails.Columns.Add("DSOCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("DSOAmount", typeof(double));

            dtGetClusterTransactionDeatails.Columns.Add("SRJCount", typeof(double));
            dtGetClusterTransactionDeatails.Columns.Add("SRJAmount", typeof(double));

            #endregion Data Table Column Defination

            //--------------------Default Value ------------------

            #region Data Table Default value

            dtGetClusterTransactionDeatails.Columns["DVPCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["DVPAmount"].DefaultValue = 0;

            dtGetClusterTransactionDeatails.Columns["SRJCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["SRJAmount"].DefaultValue = 0;

            dtGetClusterTransactionDeatails.Columns["DSOCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["DSOAmount"].DefaultValue = 0;

            dtGetClusterTransactionDeatails.Columns["CNOAmount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["CNOCount"].DefaultValue = 0;

            dtGetClusterTransactionDeatails.Columns["RRJAmount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["RRJCount"].DefaultValue = 0;

            dtGetClusterTransactionDeatails.Columns["NFOAmount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["NFOCount"].DefaultValue = 0;

            dtGetClusterTransactionDeatails.Columns["HLDCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["HLDAmount"].DefaultValue = 0;

            dtGetClusterTransactionDeatails.Columns["DSICount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["DSIAmount"].DefaultValue = 0;

            dtGetClusterTransactionDeatails.Columns["CNICount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["CNIAmount"].DefaultValue = 0;

            dtGetClusterTransactionDeatails.Columns["BNSCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["BNSAmount"].DefaultValue = 0;

            dtGetClusterTransactionDeatails.Columns["BIRCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["BIRAmount"].DefaultValue = 0;

            dtGetClusterTransactionDeatails.Columns["AddCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["AddAmount"].DefaultValue = 0;


            //------------------------------------------------------------------\\

            dtGetClusterTransactionDeatails.Columns["BUYCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["BUYAmount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["SELCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["SELAmount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["DVRCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["DVRAmount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["DVPCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["DVPAmount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["SIPCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["SIPAmount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["BCICount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["BCIAmount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["BCOCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["BCOAmount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["STBCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["STBAmount"].DefaultValue = 0;


            dtGetClusterTransactionDeatails.Columns["STSCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["STSAmount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["SWBCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["SWBAmount"].DefaultValue = 0;


            dtGetClusterTransactionDeatails.Columns["SWPCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["SWPAmount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["SWSCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["SWSAmount"].DefaultValue = 0;

            dtGetClusterTransactionDeatails.Columns["PRJCount"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["PRJAmount"].DefaultValue = 0;

            dtGetClusterTransactionDeatails.Columns["Net"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["GrossInvestment"].DefaultValue = 0;
            dtGetClusterTransactionDeatails.Columns["GrossRedemption"].DefaultValue = 0;

            #endregion Data Table Default value

            DataRow drGetClusterTransactionDeatails = null;
            DataRow[] drTransactionClusterWise;
            DataRow[] drCheckIfCategoryExistForCluster;
            if (dsGetClusterTransactionDeatails.Tables[0] != null)
            {
                DataTable dtgetClusterTransactions = dsGetClusterTransactionDeatails.Tables[0];

                foreach (DataRow drClusterTransaction in dtgetClusterTransactions.Rows)
                {

                    Int32.TryParse(drClusterTransaction["AB_BranchId"].ToString(), out branchId);

                    if (branchId != branchIdOld)
                    { //go for another row to find new customer
                        branchIdOld = branchId;
                        if (branchId != 0)
                        { // add row in manual datatable within this brace end
                            //drTransactionClusterWise = dtgetClusterTransactions.Select("AZOC_ZoneClusterId= '" + ClusterId + "'" + "AND " + "AZOC_Type='" + "Cluster" + "'");
                            drTransactionClusterWise = dtgetClusterTransactions.Select("AB_BranchId= '" + branchId + "'");
                            //drGetSchemeTransactionDeatails["PA_AMCName"] = drAMCTransaction["PA_AMCName"].ToString();

                            if (drTransactionClusterWise.Count() > 0)
                            {
                                foreach (DataRow dr in drTransactionClusterWise)
                                {
                                    DataRow drCluster = dr;
                                    string categoryCode = dr["CategoryCode"].ToString();

                                    drCheckIfCategoryExistForCluster = dtGetClusterTransactionDeatails.Select("CategoryCode= '" + categoryCode + "'" + "AND " + "branchId='" + branchId + "'");

                                    if (drCheckIfCategoryExistForCluster.Count() > 0)
                                    {
                                        string transactiontype = dr["WMTT_TransactionClassificationCode"].ToString();
                                        SwitchCaseOnTransactionClassificationCode(transactiontype, ref drGetClusterTransactionDeatails, ref drCluster);
                                        drGetClusterTransactionDeatails["Net"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) - double.Parse(drGetClusterTransactionDeatails["GrossRedemption"].ToString());
                                    }
                                    else
                                    {
                                        drGetClusterTransactionDeatails = dtGetClusterTransactionDeatails.NewRow();

                                        drGetClusterTransactionDeatails["ZoneName"] = drClusterTransaction["ZoneName"].ToString();
                                        drGetClusterTransactionDeatails["ClusterName"] = drClusterTransaction["ClusterName"].ToString();
                                        drGetClusterTransactionDeatails["ZoneClusterId"] = drClusterTransaction["AZOC_ZoneClusterId"].ToString();
                                        drGetClusterTransactionDeatails["BranchName"] = drClusterTransaction["AB_BranchName"].ToString();
                                        drGetClusterTransactionDeatails["BranchId"] = drClusterTransaction["AB_BranchId"].ToString();
                                        drGetClusterTransactionDeatails["CategoryCode"] = dr["CategoryCode"].ToString();
                                        drGetClusterTransactionDeatails["CategoryName"] = dr["CategoryName"].ToString();
                                        string transactiontype = dr["WMTT_TransactionClassificationCode"].ToString();
                                        SwitchCaseOnTransactionClassificationCode(transactiontype, ref drGetClusterTransactionDeatails, ref drCluster);
                                        drGetClusterTransactionDeatails["Net"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) - double.Parse(drGetClusterTransactionDeatails["GrossRedemption"].ToString());
                                        dtGetClusterTransactionDeatails.Rows.Add(drGetClusterTransactionDeatails);
                                    }
                                }
                            }

                        }//*

                    }//**

                }//***

                if (Cache["ZoneClusterTransactionDeatails" + userVo.UserId] == null)
                {
                    Cache.Insert("ZoneClusterTransactionDeatails" + userVo.UserId, dtGetClusterTransactionDeatails);
                }
                else
                {
                    Cache.Remove("ZoneClusterTransactionDeatails" + userVo.UserId);
                    Cache.Insert("ZoneClusterTransactionDeatails" + userVo.UserId, dtGetClusterTransactionDeatails);
                }
                gvZoneClusterWise.DataSource = dtGetClusterTransactionDeatails;
                gvZoneClusterWise.DataBind();
                gvZoneClusterWise.Visible = true;
                pnlZoneCluster.Visible = true;
                trPnlZoneCluster.Visible = true;
                divZoneWise.Visible = true;
                this.gvZoneClusterWise.GroupingSettings.RetainGroupFootersVisibility = true;
                //GridGroupByExpression expression = new GridGroupByExpression();
                //GridGroupByField gridGroupByField = new GridGroupByField();
                //gridGroupByField.FieldName = "ClusterName";
                //expression.GroupByFields.Add( gridGroupByField );
                //gvFolioWise.GroupingEnabled = true;



                //  gvFolioWise.MasterTableView.GroupByExpressions.Add(new GridGroupByExpression("ClusterName Group By ClusterName"));


                //ErrorMessage.Visible = false;
                //btnImagExport.Visible = true;
            }
        }

        protected void SwitchCaseOnTransactionClassificationCode(string transactiontype, ref DataRow drGetClusterTransactionDeatails, ref DataRow dr)
        {
            switch (transactiontype)
            {
                case "BUY":
                    {
                        drGetClusterTransactionDeatails["BUYCount"] = double.Parse(drGetClusterTransactionDeatails["BUYCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["BUYAmount"] = double.Parse(drGetClusterTransactionDeatails["BUYAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossInvestment"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossInvestment"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetClusterTransactionDeatails["BUYAmount"].ToString());
                        }

                        break;
                    }
                case "SEL":
                    {
                        drGetClusterTransactionDeatails["SELCount"] = double.Parse(drGetClusterTransactionDeatails["SELCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["SELAmount"] = double.Parse(drGetClusterTransactionDeatails["SELAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossRedemption"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossRedemption"] = double.Parse(drGetClusterTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetClusterTransactionDeatails["SELAmount"].ToString());
                        }

                        break;
                    }
                case "DVR":
                    {
                        drGetClusterTransactionDeatails["DVRCount"] = double.Parse(drGetClusterTransactionDeatails["DVRCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["DVRAmount"] = double.Parse(drGetClusterTransactionDeatails["DVRAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossInvestment"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossInvestment"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetClusterTransactionDeatails["DVRAmount"].ToString());
                        }

                        break;
                    }
                case "DVP":
                    {
                        drGetClusterTransactionDeatails["DVPCount"] = double.Parse(drGetClusterTransactionDeatails["DVPCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["DVPAmount"] = double.Parse(drGetClusterTransactionDeatails["DVPAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());
                        break;
                    }
                case "SIP":
                    {
                        drGetClusterTransactionDeatails["SIPCount"] = double.Parse(drGetClusterTransactionDeatails["SIPCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["SIPAmount"] = double.Parse(drGetClusterTransactionDeatails["SIPAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossInvestment"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossInvestment"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetClusterTransactionDeatails["SIPAmount"].ToString());
                        }

                        break;
                    }
                case "BCI":
                    {
                        drGetClusterTransactionDeatails["BCICount"] = double.Parse(drGetClusterTransactionDeatails["BCICount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["BCIAmount"] = double.Parse(drGetClusterTransactionDeatails["BCIAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossInvestment"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossInvestment"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetClusterTransactionDeatails["BCIAmount"].ToString());
                        }

                        break;
                    }
                case "BCO":
                    {
                        drGetClusterTransactionDeatails["BCOCount"] = double.Parse(drGetClusterTransactionDeatails["BCOCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["BCOAmount"] = double.Parse(drGetClusterTransactionDeatails["BCOAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossRedemption"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossRedemption"] = double.Parse(drGetClusterTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetClusterTransactionDeatails["BCOAmount"].ToString());
                        }

                        break;
                    }
                case "STB":
                    {
                        drGetClusterTransactionDeatails["STBCount"] = double.Parse(drGetClusterTransactionDeatails["STBCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["STBAmount"] = double.Parse(drGetClusterTransactionDeatails["STBAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossInvestment"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossInvestment"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetClusterTransactionDeatails["STBAmount"].ToString());
                        }

                        break;
                    }
                case "STS":
                    {
                        drGetClusterTransactionDeatails["STSCount"] = double.Parse(drGetClusterTransactionDeatails["STSCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["STSAmount"] = double.Parse(drGetClusterTransactionDeatails["STSAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossRedemption"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossRedemption"] = double.Parse(drGetClusterTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetClusterTransactionDeatails["STSAmount"].ToString());
                        }

                        break;
                    }
                case "SWB":
                    {
                        drGetClusterTransactionDeatails["SWBCount"] = double.Parse(drGetClusterTransactionDeatails["SWBCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["SWBAmount"] = double.Parse(drGetClusterTransactionDeatails["SWBAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossInvestment"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossInvestment"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetClusterTransactionDeatails["SWBAmount"].ToString());
                        }

                        break;
                    }
                case "SWP":
                    {
                        drGetClusterTransactionDeatails["SWPCount"] = double.Parse(drGetClusterTransactionDeatails["SWPCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["SWPAmount"] = double.Parse(drGetClusterTransactionDeatails["SWPAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossRedemption"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossRedemption"] = double.Parse(drGetClusterTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetClusterTransactionDeatails["SWPAmount"].ToString());
                        }

                        break;
                    }
                case "SWS":
                    {
                        drGetClusterTransactionDeatails["SWSCount"] = double.Parse(drGetClusterTransactionDeatails["SWSCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["SWSAmount"] = double.Parse(drGetClusterTransactionDeatails["SWSAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossRedemption"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossRedemption"] = double.Parse(drGetClusterTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetClusterTransactionDeatails["SWSAmount"].ToString());
                        }


                        break;
                    }
                case "PRJ":
                    {
                        drGetClusterTransactionDeatails["PRJCount"] = double.Parse(drGetClusterTransactionDeatails["PRJCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["PRJAmount"] = double.Parse(drGetClusterTransactionDeatails["PRJAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossInvestment"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossInvestment"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetClusterTransactionDeatails["PRJAmount"].ToString());
                        }

                        break;
                    }


                //----------------------->>>

                case "ABY":
                    {
                        drGetClusterTransactionDeatails["AddCount"] = double.Parse(drGetClusterTransactionDeatails["AddCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["AddAmount"] = double.Parse(drGetClusterTransactionDeatails["AddAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossInvestment"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossInvestment"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetClusterTransactionDeatails["AddAmount"].ToString());
                        }

                        break;
                    }
                case "BIR":
                    {
                        drGetClusterTransactionDeatails["BIRCount"] = double.Parse(drGetClusterTransactionDeatails["BIRCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["BIRAmount"] = double.Parse(drGetClusterTransactionDeatails["BIRAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossInvestment"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossInvestment"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetClusterTransactionDeatails["BIRAmount"].ToString());
                        }

                        break;
                    }
                case "BNS":
                    {
                        drGetClusterTransactionDeatails["BNSCount"] = double.Parse(drGetClusterTransactionDeatails["BNSCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["BNSAmount"] = double.Parse(drGetClusterTransactionDeatails["BNSAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());


                        if (dr["WMTT_GrossInvestment"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossInvestment"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetClusterTransactionDeatails["BNSAmount"].ToString());
                        }

                        break;
                    }
                case "CNI":
                    {
                        drGetClusterTransactionDeatails["CNICount"] = double.Parse(drGetClusterTransactionDeatails["CNICount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["CNIAmount"] = double.Parse(drGetClusterTransactionDeatails["CNIAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());
                        if (dr["WMTT_GrossInvestment"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossInvestment"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetClusterTransactionDeatails["CNIAmount"].ToString());
                        }

                        break;
                    }
                case "CNO":
                    {
                        drGetClusterTransactionDeatails["CNOCount"] = double.Parse(drGetClusterTransactionDeatails["CNOCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["CNOAmount"] = double.Parse(drGetClusterTransactionDeatails["CNOAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossRedemption"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossRedemption"] = double.Parse(drGetClusterTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetClusterTransactionDeatails["CNOAmount"].ToString());
                        }


                        break;
                    }
                case "DSI":
                    {
                        drGetClusterTransactionDeatails["DSICount"] = double.Parse(drGetClusterTransactionDeatails["DSICount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["DSIAmount"] = double.Parse(drGetClusterTransactionDeatails["DSIAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());
                        if (dr["WMTT_GrossInvestment"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossInvestment"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetClusterTransactionDeatails["DSIAmount"].ToString());
                        }

                        break;
                    }
                case "DSO":
                    {
                        drGetClusterTransactionDeatails["DSOCount"] = double.Parse(drGetClusterTransactionDeatails["DSOCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["DSOAmount"] = double.Parse(drGetClusterTransactionDeatails["DSOAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossRedemption"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossRedemption"] = double.Parse(drGetClusterTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetClusterTransactionDeatails["DSOAmount"].ToString());
                        }


                        break;
                    }
                case "HLD":
                    {
                        drGetClusterTransactionDeatails["HLDCount"] = double.Parse(drGetClusterTransactionDeatails["HLDCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["HLDAmount"] = double.Parse(drGetClusterTransactionDeatails["HLDAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());
                        if (dr["WMTT_GrossInvestment"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossInvestment"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetClusterTransactionDeatails["HLDAmount"].ToString());
                        }

                        break;
                    }
                case "NFO":
                    {
                        drGetClusterTransactionDeatails["NFOCount"] = double.Parse(drGetClusterTransactionDeatails["NFOCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["NFOAmount"] = double.Parse(drGetClusterTransactionDeatails["NFOAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossInvestment"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossInvestment"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetClusterTransactionDeatails["NFOAmount"].ToString());
                        }

                        break;
                    }


                case "RRJ":
                    {
                        drGetClusterTransactionDeatails["RRJCount"] = double.Parse(drGetClusterTransactionDeatails["RRJCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["RRJAmount"] = double.Parse(drGetClusterTransactionDeatails["RRJAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossInvestment"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossInvestment"] = double.Parse(drGetClusterTransactionDeatails["GrossInvestment"].ToString()) + double.Parse(drGetClusterTransactionDeatails["RRJAmount"].ToString());
                        }

                        break;
                    }
                case "SRJ":
                    {
                        drGetClusterTransactionDeatails["SRJCount"] = double.Parse(drGetClusterTransactionDeatails["SRJCount"].ToString()) + double.Parse(dr["TrxnCount"].ToString());
                        drGetClusterTransactionDeatails["SRJAmount"] = double.Parse(drGetClusterTransactionDeatails["SRJAmount"].ToString()) + double.Parse(dr["TrxnAmt"].ToString());

                        if (dr["WMTT_GrossRedemption"].ToString() == "1")
                        {
                            drGetClusterTransactionDeatails["GrossRedemption"] = double.Parse(drGetClusterTransactionDeatails["GrossRedemption"].ToString()) + double.Parse(drGetClusterTransactionDeatails["SRJAmount"].ToString());
                        }

                        break;
                    }

                //--------------------------->>>>>>
            }
        }


        protected void gvZoneClusterWise_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetFolioTransactionDeatails = new DataTable();
            dtGetFolioTransactionDeatails = (DataTable)Cache["ZoneClusterTransactionDeatails" + userVo.UserId];
            gvZoneClusterWise.DataSource = dtGetFolioTransactionDeatails;
            gvZoneClusterWise.Visible = true;
            //this.gvFolioWise.GroupingSettings.RetainGroupFootersVisibility = true;
        }


        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (ddlAction.SelectedValue == "Product")
            {
                SetParameters();
                showHideGrid("SchemeWise");
                BindSchemeWiseTransactionDetails();
                lblMFMISType.Text = "AMC/Scheme/Category Wise";

                ShowHideMISMenu("Product");
            }
            else if (ddlAction.SelectedValue == "Organization")
            {
                SetParameters();
                showHideGrid("ZoneClusterWise");
                BindZoneClusterTransactionDetails();
                lblMFMISType.Text = "Zone/Cluster/Branch Wise";

                ShowHideMISMenu("Organization");
            }
            else if (ddlAction.SelectedValue == "Staff")
            {
                SetParameters();
                showHideGrid("FolioWise");
                BindFolioWiseTransactionDetails();
                lblMFMISType.Text = "Staff/Customer/Folio Wise";

                ShowHideMISMenu("Staff");
            }
        }


        //protected void gvZoneClusterWise_PreRender(object sender, EventArgs e)
        //{
        //    int i = 0;
        //    foreach (GridGroupHeaderItem groupHeader in gvZoneClusterWise.MasterTableView.GetItems(GridItemType.GroupHeader))//accessing header
        //    {
        //        i++;
        //        GridItem[] children = groupHeader.GetChildItems();//accessing child items
        //        foreach (GridDataItem child in children)
        //        {
        //            GridDataItem childItem = child as GridDataItem;
        //            if (i % 2 == 0)
        //            {
        //                childItem.BackColor = System.Drawing.Color.Red;
        //            }
        //            else
        //            {
        //                childItem.BackColor = System.Drawing.Color.Black;
        //            }
        //        }
        //    }
        //}

    }
}
