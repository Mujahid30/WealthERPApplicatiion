using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using VoUser;
using BoUser;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using WealthERP.Base;
using BoCommon;
using BoUploads;
using BoCustomerPortfolio;


namespace WealthERP.Advisor
{
    public partial class AdviserMFMIS : System.Web.UI.UserControl
    {
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        CustomerTransactionBo customertransactionbo = new CustomerTransactionBo();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        string path;
        string userType;
        int advisorId;
        int rmId, branchId;
        int ID = 0;
        int userId;
        DataSet dsMfMIS = new DataSet();
        DataSet dsLastTradeDate;
        DataTable dtAdviserMFMIS = new DataTable();

        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        UserVo userVo = new UserVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        int bmID;

        CultureInfo ci = new CultureInfo("en-GB");
        DateTime convertedFromDate = new DateTime();
        DateTime convertedToDate = new DateTime();
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();
        DateBo dtBo = new DateBo();

        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                advisorVo = (AdvisorVo)Session["advisorVo"];
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                userType = Session["UserType"].ToString().ToLower();
                dsLastTradeDate = customertransactionbo.GetLastTradeDate();

                SessionBo.CheckSession();
                userVo = (UserVo)Session["userVo"];
                rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
                bmID = rmVo.RMId;


                if (dsLastTradeDate.Tables[0].Rows.Count != 0)
                {
                    DateTime dtFromLastDate = (DateTime)dsLastTradeDate.Tables[0].Rows[0]["WTD_Date"];
                    DateTime dtToLastDate = (DateTime)dsLastTradeDate.Tables[0].Rows[0]["WTD_Date"];
                    txtFromDate.Text = dtFromLastDate.ToShortDateString();
                    txtToDate.Text = dtToLastDate.ToShortDateString();
                }
                if (txtFromDate.Text != null && txtFromDate.Text != "")
                {
                    convertedFromDate = Convert.ToDateTime(txtFromDate.Text.Trim(), ci);
                }
                else
                {
                    convertedFromDate = DateTime.MinValue;
                }
                if (txtToDate.Text != null && txtToDate.Text != "")
                {
                    convertedToDate = Convert.ToDateTime(txtToDate.Text.Trim(), ci);
                }
                else
                {
                    convertedToDate = DateTime.MinValue;
                }
                if (userType == "rm")
                {
                    spnBranch.Visible = false;
                    spnRM.Visible = false;
                }
                if (userType == "adviser")
                {
                    BindBranchDropDown();
                    BindRMDropDown();
                }
                if (userType == "bm")
                {
                    if (!IsPostBack)
                    {
                        BindBranchForBMDropDown();
                        BindRMforBranchDropdown(0, bmID, 1);
                    }
                    hdnbranchHeadId.Value = ddlBranch.SelectedValue;
                    hdnall.Value = "2";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                //to hide the dropdown selection for mis type for adviser and bm
                if (!IsPostBack)
                {
                    rbtnPickDate.Checked = true;
                    rbtnPickPeriod.Checked = false;
                    //trRange.Visible = true;
                    trPeriod.Visible = false;
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

                FunctionInfo.Add("Method", "AddBranch.ascx:PageLoad()");

                object[] objects = new object[2];
                objects[0] = advisorVo;
                objects[1] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void BindGrid(DateTime dtFrom, DateTime dtTo)
        {
            int.TryParse(ddlBranch.SelectedValue, out branchId);
            int.TryParse(ddlRM.SelectedValue, out rmId);

            if (userType == "adviser")
                ID = advisorVo.advisorId;
            else if (userType == "rm")
            {
                rmVo = (RMVo)Session[SessionContents.RmVo];
                ID = rmVo.RMId;
            }
            else if (userType == "bm")
            {
                rmVo = (RMVo)Session[SessionContents.RmVo];
                ID = rmVo.RMId;
            }

            try
            {
                if (userType == "rm")
                {
                    dsMfMIS = adviserMFMIS.GetMFMIS(userType, ID, dtFrom, dtTo, 0, 0, 0, 0);
                }
                else if (userType == "adviser")
                {
                    dsMfMIS = adviserMFMIS.GetMFMISAdviser(advisorVo.advisorId, branchId, rmId, dtFrom, dtTo);
                }
                else if (userType == "bm")
                {
                    if (hdnall.Value == "0")
                    {
                        hdnrmId.Value = ddlRM.SelectedValue;
                        hdnbranchId.Value = ddlBranch.SelectedValue;
                        dsMfMIS = adviserMFMIS.GetMFMIS(userType, ID, dtFrom, dtTo, int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), 0, 0);
                    }
                    else if (hdnall.Value == "1")
                    {
                        hdnbranchId.Value = ddlBranch.SelectedValue;
                        dsMfMIS = adviserMFMIS.GetMFMIS(userType, ID, dtFrom, dtTo, 0, int.Parse(hdnbranchId.Value.ToString()), 0, 1);
                    }
                    else if (hdnall.Value == "2")
                    {
                        hdnbranchHeadId.Value = ddlBranch.SelectedValue;
                        dsMfMIS = adviserMFMIS.GetMFMIS(userType, ID, dtFrom, dtTo, 0, 0, int.Parse(hdnbranchHeadId.Value.ToString()), 2);
                    }
                    else if (hdnall.Value == "3")
                    {
                        hdnbranchHeadId.Value = ddlBranch.SelectedValue;
                        hdnrmId.Value = ddlRM.SelectedValue;
                        dsMfMIS = adviserMFMIS.GetMFMIS(userType, ID, dtFrom, dtTo, int.Parse(hdnrmId.Value.ToString()), 0, int.Parse(hdnbranchHeadId.Value.ToString()), 3);

                    }


                }

                if (dsMfMIS.Tables[0].Rows.Count > 0)
                {
                    ErrorMessage.Visible = false;
                    dtAdviserMFMIS.Columns.Add("Category");
                    dtAdviserMFMIS.Columns.Add("BuyValue");
                    dtAdviserMFMIS.Columns.Add("SellValue");
                    dtAdviserMFMIS.Columns.Add("NoOfTrans");
                    dtAdviserMFMIS.Columns.Add("SIPValue");
                    dtAdviserMFMIS.Columns.Add("NoOfSIPs");

                    DataRow drAdvMFMIS;

                    for (int i = 0; i < dsMfMIS.Tables[0].Rows.Count; i++)
                    {
                        drAdvMFMIS = dtAdviserMFMIS.NewRow();

                        drAdvMFMIS[0] = dsMfMIS.Tables[0].Rows[i]["Category"].ToString();
                        drAdvMFMIS[1] = String.Format("{0:n2}", decimal.Parse(dsMfMIS.Tables[0].Rows[i]["BuyValue"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drAdvMFMIS[2] = String.Format("{0:n2}", decimal.Parse(dsMfMIS.Tables[0].Rows[i]["SellValue"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drAdvMFMIS[3] = dsMfMIS.Tables[0].Rows[i]["NoOfTrans"].ToString();
                        drAdvMFMIS[4] = String.Format("{0:n2}", decimal.Parse(dsMfMIS.Tables[0].Rows[i]["SIPValue"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drAdvMFMIS[5] = dsMfMIS.Tables[0].Rows[i]["NoOfSIPs"].ToString();

                        dtAdviserMFMIS.Rows.Add(drAdvMFMIS);
                    }
                    gvMFMIS.Visible = true;

                    gvMFMIS.DataSource = dtAdviserMFMIS;
                    gvMFMIS.DataBind();
                }
                else
                {
                    ErrorMessage.Visible = true;
                    gvMFMIS.Visible = false;
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
                FunctionInfo.Add("Method", "AdviserMFMIS.ascx.cs:BindGrid()");
                object[] objects = new object[2];
                objects[0] = dtFrom;
                objects[1] = dtTo;

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
                ErrorMessage.Visible = false;
            }
            else if (rbtnPickPeriod.Checked == true)
            {
                trRange.Visible = false;
                //tdFrmDate.Visible = false;
                //tdToDate.Visible = false;
                trPeriod.Visible = true;
                BindPeriodDropDown();
                ErrorMessage.Visible = false;
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
            ddlPeriod.Items.Insert(0, new ListItem("Select a Period", "Select a Period"));
        }

        protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnGo_Click(object sender, EventArgs e)
        {

            if (rbtnPickPeriod.Checked)
            {

                if (ddlPeriod.SelectedIndex != 0)
                {
                    dtBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue, out dtFrom, out dtTo);
                    this.BindGrid(dtFrom, dtTo);
                }
            }

            if (rbtnPickDate.Checked)
            {
                if (txtFromDate.Text != null && txtFromDate.Text != "")
                {
                    convertedFromDate = Convert.ToDateTime(txtFromDate.Text.Trim(), ci);
                }
                else
                {
                    convertedFromDate = DateTime.MinValue;
                }
                if (txtToDate.Text != null && txtToDate.Text != "")
                {
                    convertedToDate = Convert.ToDateTime(txtToDate.Text.Trim(), ci);
                }
                else
                {
                    convertedToDate = DateTime.MinValue;
                }
            }



            /* For BM Branch wise MIS */
            if (userType == "adviser")
            {
                dsMfMIS = adviserMFMIS.GetMFMISAdviser(advisorVo.advisorId, branchId, rmId, dtFrom, dtTo);
            }
            else if (userType == "bm")
            {
                if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "2";
                    hdnrmId.Value = "0";
                    //dsMfMIS = adviserMFMIS.GetMFMIS(userType, ID, dtFrom, dtTo, 0, 0, int.Parse(hdnbranchHeadId.Value.ToString()), 2);
                    this.BindGrid(convertedFromDate, convertedToDate);
                }
                else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "3";
                    hdnrmId.Value = ddlRM.SelectedValue;

                    //dsMfMIS = adviserMFMIS.GetMFMIS(userType, ID, dtFrom, dtTo, int.Parse(hdnrmId.Value.ToString()), 0, int.Parse(hdnbranchHeadId.Value.ToString()), 3);
                    this.BindGrid(convertedFromDate, convertedToDate);
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "1";
                    //dsMfMIS = adviserMFMIS.GetMFMIS(userType, ID, dtFrom, dtTo, 0, int.Parse(hdnbranchId.Value.ToString()), 0, 1);
                    this.BindGrid(convertedFromDate, convertedToDate);
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                {
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "0";
                    hdnrmId.Value = ddlRM.SelectedValue;

                    //dsMfMIS = adviserMFMIS.GetMFMIS(userType, ID, dtFrom, dtTo, int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), 0, 0);
                    this.BindGrid(convertedFromDate, convertedToDate);
                }
            }
            
        }

        /* ********************** */


        protected void ddlMISType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMISType.SelectedItem.Value.ToString() == "AMCWiseAUM")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMAMCwiseMIS','login');", true);
            }
            if (ddlMISType.SelectedItem.Value.ToString() == "AMCSchemeWiseAUM")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMAMCSchemewiseMIS','login');", true);
            }
            else if (ddlMISType.SelectedItem.Value.ToString() == "FolioWiseAUM")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMCustomerAMCSchemewiseMIS','login');", true);
            }
            else if (ddlMISType.SelectedItem.Value.ToString() == "TurnOverSummary")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserMFMIS','login');", true);
            }
        }

        private void BindBranchDropDown()
        {
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
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserMFMIS.ascx:BindBranchDropDown()");

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
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserMFMIS.ascx:BindRMDropDown()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable dt = new DataTable();
            //    AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            //    ddlRM.Items.Clear();
            //    if (ddlBranch.SelectedItem.Text != "All")
            //        dt = advisorStaffBo.GetRMListForBranchDP(int.Parse(ddlBranch.SelectedItem.Value.ToString()), 0, 0);
            //    else
            //        dt = advisorStaffBo.GetRMListForBranchDP(0, int.Parse(ddlBranch.SelectedValue.ToString()), 1);
            //    if (dt.Rows.Count > 0)
            //    {
            //        ddlRM.DataSource = dt;
            //        ddlRM.DataTextField = dt.Columns["RMName"].ToString();
            //        ddlRM.DataValueField = dt.Columns["RMID"].ToString();

            //        ddlRM.DataBind();
            //        ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            //    }
            //}
            //catch (BaseApplicationException Ex)
            //{
            //    throw Ex;
            //}
            //catch (Exception Ex)
            //{
            //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
            //    NameValueCollection FunctionInfo = new NameValueCollection();

            //    FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

            //    object[] objects = new object[4];

            //    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
            //    exBase.AdditionalInformation = FunctionInfo;
            //    ExceptionManager.Publish(exBase);
            //    throw exBase;
            //}
            if (ddlBranch.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID, 1);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0, 0);
            }

        }

        protected void ddlRM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /* For Binding the Branch Dropdowns */

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

                FunctionInfo.Add("Method", "AdviserMFMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        /* End For Binding the Branch Dropdowns */



        /* For Binding the RM Dropdowns */

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

                FunctionInfo.Add("Method", "AdviserMFMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        /* End For Binding the RM Dropdowns */
    }
}