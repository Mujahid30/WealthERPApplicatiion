using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using VoUser;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using WealthERP.Base;
using BoCommon;
using System.Globalization;
using System.Collections.Specialized;
using BoCustomerPortfolio;
using BoUploads;
using Telerik.Web.UI;

namespace WealthERP.BusinessMIS
{
    public partial class EquityTurnover : System.Web.UI.UserControl
    {
        AdvisorMISBo adviserMIS = new AdvisorMISBo();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        string path;
        string userType;
        int advisorId;
        int rmId = 0;
        DateTime LatestValuationdate = new DateTime();
        DataRow drAdvEQMIS;
        UserVo userVo = new UserVo();
        int bmID;
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        CustomerTransactionBo customertransactionbo = new CustomerTransactionBo();
        DataSet dsGetLastTradeDate;
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
             SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            advisorId = advisorVo.advisorId;
            PortfolioBo portfoliobo = new PortfolioBo();
            string valuedate = Convert.ToString(portfoliobo.GetLatestValuationDate(advisorId, "EQ"));
            hdnValuationDate.Value = valuedate.ToString();
            if (hdnValuationDate.Value == string.Empty)
            {
                ValuationNotDoneErrorMsg.Visible = true;
                if (userType == "advisor")
                {
                    BindBranchDropDown();
                    BindRMDropDown();
                }
                else if (userType == "rm")
                {
                    trBranchRmDpRow.Visible = false;

                }
                else if (userType == "bm")
                {
                    BindBranchForBMDropDown();
                    BindRMforBranchDropdown(0, bmID, 1);
                    trComSecWiseOptions.Visible = true;
                }
            }
            else
            {
                ValuationNotDoneErrorMsg.Visible = false;
                try
                {
                    /* For UserType */
                    advisorVo = (AdvisorVo)Session["advisorVo"];
                    path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

                    if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                        userType = "advisor";
                    else
                        userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

                    SessionBo.CheckSession();
                    userVo = (UserVo)Session["userVo"];
                    rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
                    bmID = rmVo.RMId;

                    if (!IsPostBack)
                    {

                        if (valuedate != "")
                        {
                            LatestValuationdate = Convert.ToDateTime(portfoliobo.GetLatestValuationDate(advisorVo.advisorId, "EQ"));
                            hdnValuationDate.Value = LatestValuationdate.ToString();
                        }
                        if (LatestValuationdate != DateTime.MinValue)
                        {
                            txtEQDate.Text = LatestValuationdate.Date.ToShortDateString();

                        }
                        BindMISTypeDropDown();
                    }

                    dsGetLastTradeDate = customertransactionbo.GetLastTradeDate();
                    DateTime dtLastTradeDate;

                    if (valuedate != "")
                    {
                        LatestValuationdate = Convert.ToDateTime(portfoliobo.GetLatestValuationDate(advisorVo.advisorId, "EQ"));
                        hdnValuationDate.Value = LatestValuationdate.ToString();
                    }
                    if (LatestValuationdate != DateTime.MinValue)
                    {
                        txtEQDate.Text = LatestValuationdate.Date.ToShortDateString();
                    }


                    if (dsGetLastTradeDate.Tables[0].Rows.Count != 0)
                    {
                        dtLastTradeDate = (DateTime)dsGetLastTradeDate.Tables[0].Rows[0]["WTD_Date"];

                        txtFrom.SelectedDate = DateTime.Parse(dtLastTradeDate.ToShortDateString());
                        txtTo.SelectedDate = DateTime.Parse(dtLastTradeDate.ToShortDateString());

                    }

                   if (!IsPostBack)
                    {
                        rbtnPickDate.Checked = true;
                        rbtnPickPeriod.Checked = false;
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
        }
        private void BindBranchDropDown()
        {

            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null)
                {
                    ddlBranchForEQ.DataSource = ds;
                    ddlBranchForEQ.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlBranchForEQ.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlBranchForEQ.DataBind();
                }
                ddlBranchForEQ.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", advisorVo.advisorId.ToString()));
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
                    ddlRMEQ.DataSource = dt;
                    ddlRMEQ.DataValueField = dt.Columns["AR_RMId"].ToString();
                    ddlRMEQ.DataTextField = dt.Columns["RMName"].ToString();
                    ddlRMEQ.DataBind();
                }
                ddlRMEQ.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "2"));
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
                    ddlBranchForEQ.DataSource = ds.Tables[1]; ;
                    ddlBranchForEQ.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                    ddlBranchForEQ.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                    ddlBranchForEQ.DataBind();
                }
                ddlBranchForEQ.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
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
        private void BindMISTypeDropDown()
        {
            if (txtEQDate.Text == string.Empty)
            {
                if ((userType == "advisor") || (userType == "bm"))
                {
                        ValuationNotDoneErrorMsg.Visible = true;
                        rbtnPickDate.Checked = true;
                        rbtnPickPeriod.Checked = false;
                        trPeriod.Visible = false;
                        trRange.Visible = true;
                        trRbtnPickDate.Visible = true;
                        tableComSecWiseOptions.Visible = false;
                        trBranchRmDpRow.Visible = true;
                 
                }
                else if (userType == "rm")
                {
                        ValuationNotDoneErrorMsg.Visible = true;
                        rbtnPickDate.Checked = true;
                        rbtnPickPeriod.Checked = false;
                        trPeriod.Visible = false;
                        trRange.Visible = true;
                        trRbtnPickDate.Visible = true;
                        tableComSecWiseOptions.Visible = false;
                        trBranchRmDpRow.Visible = false;
                    
                }

            }

            else
            {
                    LatestValuationdate = Convert.ToDateTime(txtEQDate.Text);
                    hdnValuationDate.Value = LatestValuationdate.ToString();
                     DateTime Valuation_Date = Convert.ToDateTime(hdnValuationDate.Value.ToString());
                    rbtnPickDate.Checked = true;
                    rbtnPickPeriod.Checked = false;
                    trPeriod.Visible = false;

                    trRange.Visible = true;
                    trRbtnPickDate.Visible = true;
                    //trPeriod.Visible = false;

                    tableComSecWiseOptions.Visible = false;
                    if ((!IsPostBack) && (userType == "advisor"))
                    {
                        BindBranchDropDown();
                        BindRMDropDown();
                    }
                    else if ((!IsPostBack) && (userType == "rm"))
                    {
                        trBranchRmDpRow.Visible = false;

                    }
                    else if ((!IsPostBack) && (userType == "bm"))
                    {
                        BindBranchForBMDropDown();
                        BindRMforBranchDropdown(0, bmID, 1);
                        trComSecWiseOptions.Visible = true;
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
                    ddlRMEQ.DataSource = ds.Tables[0]; ;
                    ddlRMEQ.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                    ddlRMEQ.DataTextField = ds.Tables[0].Columns["RM Name"].ToString();
                    ddlRMEQ.DataBind();
                }
                ddlRMEQ.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
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
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            dtPeriod = XMLBo.GetDatePeriod(path);

            ddlPeriod.DataSource = dtPeriod;
            ddlPeriod.DataTextField = "PeriodType";
            ddlPeriod.DataValueField = "PeriodCode";
            ddlPeriod.DataBind();
            ddlPeriod.Items.Insert(0, new ListItem("Select a Period", "Select a Period"));
            ddlPeriod.Items.RemoveAt(15);
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            GenerateEQMIS();
        }
        public void GenerateEQMIS()
        {
            CultureInfo ci = new CultureInfo("en-GB");

            DateTime convertedFromDate = new DateTime();
            DateTime convertedToDate = new DateTime();

            LatestValuationdate = Convert.ToDateTime(txtEQDate.Text);
            hdnValuationDate.Value = LatestValuationdate.ToString();
            DateTime Valuation_Date = Convert.ToDateTime(hdnValuationDate.Value.ToString());

            convertedFromDate = Convert.ToDateTime(txtFrom.SelectedDate, ci);
            convertedToDate = Convert.ToDateTime(txtTo.SelectedDate, ci);


            /* For Turn Over EQ */
            if (userType == "advisor")
            {
                if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex == 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "0";
                    hdnrmId.Value = "0";
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);
                }
                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex == 0))
                {

                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = "0";
                    hdnall.Value = "1";
                    hdnrmId.Value = "0";
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);

                }

                else if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "2";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);
                }

                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = "0";
                    hdnall.Value = "3";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);
                }
            }
            else if (userType == "rm")
            {
                rmId = rmVo.RMId;
                hdnbranchId.Value = "0";
                hdnbranchHeadId.Value = "0";
                hdnrmId.Value = rmId.ToString();
                hdnall.Value = "0";
                if (rbtnPickPeriod.Checked == true)
                    GereratePickPeriod();
                else
                    this.BindGrid(convertedFromDate, convertedToDate);
            }
            else if (userType == "bm")
            {
                if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex == 0))
                {

                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "2";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);
                }

                else if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "3";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);
                }

                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex == 0))
                {

                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "1";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);
                }

                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "0";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);
                }

            }
            /* ********** */
        }
        public void GereratePickPeriod()
        {
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();
            DateBo dtBo = new DateBo();

            if (ddlPeriod.SelectedIndex != 0)
            {
                dtBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue, out dtFrom, out dtTo);
                this.BindGrid(dtFrom, dtTo);
            }
        }
        protected void ddlBranchForEQ_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlBranchForEQ.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID, 1);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranchForEQ.SelectedValue.ToString()), 0, 0);
            }
            GenerateEQMIS();
        }
        private void BindGrid(DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsEQMIS = new DataSet();
            DataTable dtAdviserEQMIS = new DataTable();


            int ID = 0;

            if (userType == "advisor")
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
                if (userType == "bm")
                {
                    if (hdnall.Value == "0")
                    {
                        hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                        hdnrmId.Value = ddlRMEQ.SelectedValue;
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), 0, 0);
                    }
                    else if (hdnall.Value == "1")
                    {
                        hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, 0, int.Parse(hdnbranchId.Value.ToString()), 0, 1);
                    }
                    else if (hdnall.Value == "2")
                    {
                        hdnbranchHeadId.Value = ddlBranchForEQ.SelectedValue;
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, 0, 0, int.Parse(hdnbranchHeadId.Value.ToString()), 2);
                    }
                    else if (hdnall.Value == "3")
                    {
                        hdnbranchHeadId.Value = ddlBranchForEQ.SelectedValue;
                        hdnrmId.Value = ddlRMEQ.SelectedValue;
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, int.Parse(hdnrmId.Value.ToString()), 0, int.Parse(hdnbranchHeadId.Value.ToString()), 3);
                    }
                    //dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo,0, 0, 0, 0);
                }
            }

            try
            {
                if (userType == "advisor")
                {
                    if (hdnall.Value == "0")
                    {
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, 0, 0, int.Parse(hdnbranchHeadId.Value.ToString()), 0);
                    }
                    else if (hdnall.Value == "1")
                    {
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, 0, int.Parse(hdnbranchId.Value.ToString()), 0, 1);
                    }
                    else if (hdnall.Value == "2")
                    {
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, int.Parse(hdnrmId.Value.ToString()), 0, int.Parse(hdnbranchHeadId.Value.ToString()), 2);
                    }
                    else if (hdnall.Value == "3")
                    {
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), 0, 3);
                    }
                }
                if (userType == "rm")
                {
                    dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, 0, 0, 0, 0);
                }

                if (dsEQMIS.Tables[0].Rows.Count > 0)
                {
                    DataRow drAdvEQMIS;
                    drAdvEQMIS = dtAdviserEQMIS.NewRow();


                    dtAdviserEQMIS.Columns.Add("CName_Industry_Delby");
                    dtAdviserEQMIS.Columns.Add("Industry_MValue_DelSell");
                    dtAdviserEQMIS.Columns.Add("MValue_Percentage_SpecSell");
                    dtAdviserEQMIS.Columns.Add("MValue_Blank_SpecBuy");

                    int flag = 0;

                    for (int i = 0; i < dsEQMIS.Tables[0].Rows.Count; i++)
                    {
                        NumberFormatInfo nfi0 = new CultureInfo("en-US", false).NumberFormat;
                        NumberFormatInfo nfi1 = new CultureInfo("en-US", false).NumberFormat;
                        NumberFormatInfo nfi2 = new CultureInfo("en-US", false).NumberFormat;
                        NumberFormatInfo nfi3 = new CultureInfo("en-US", false).NumberFormat;

                        nfi0.NumberDecimalSeparator = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["DeliveryBuy"].ToString()).ToString("N", nfi0));
                        nfi1.NumberDecimalSeparator = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["DeliverySell"].ToString()).ToString("N", nfi1));
                        nfi2.NumberDecimalSeparator = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["SpeculativeSell"].ToString()).ToString("N", nfi2));
                        nfi3.NumberDecimalSeparator = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["SpeculativeBuy"].ToString()).ToString("N", nfi3));

                        drAdvEQMIS[0] = nfi0.NumberDecimalSeparator;
                        drAdvEQMIS[1] = nfi1.NumberDecimalSeparator;
                        drAdvEQMIS[2] = nfi2.NumberDecimalSeparator;
                        drAdvEQMIS[3] = nfi3.NumberDecimalSeparator;

                        dtAdviserEQMIS.Rows.Add(drAdvEQMIS);


                    }

                    if ((decimal.Parse(dsEQMIS.Tables[0].Rows[0]["DeliveryBuy"].ToString()) != 0) || (decimal.Parse(dsEQMIS.Tables[0].Rows[0]["DeliverySell"].ToString()) != 0) ||
                            (decimal.Parse(dsEQMIS.Tables[0].Rows[0]["SpeculativeSell"].ToString()) != 0) || (decimal.Parse(dsEQMIS.Tables[0].Rows[0]["SpeculativeBuy"].ToString()) != 0))
                    {
                        flag = 1;
                    }

                    if (flag == 0)
                    {
                        gvEQMIS.Visible = false;
                        ErrorMessage.Visible = true;
                    }
                    else
                    {
                        gvEQMIS.Visible = true;
                        ErrorMessage.Visible = false;
                        gvEQMIS.DataSource = dtAdviserEQMIS;
                        gvEQMIS.DataBind();
                        gvEQMIS.Columns[0].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                        gvEQMIS.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Right;


                        //Assigning Text To Header template

                        Label Hlb1 = (Label)gvEQMIS.HeaderRow.FindControl("lblCustIndDelby");
                        Hlb1.Text = "Delivery Buy Value";

                        Label Hlb2 = (Label)gvEQMIS.HeaderRow.FindControl("lblIndMValueDelSell");
                        Hlb2.Text = "Delivery Sell Value";

                        Label Hlb3 = (Label)gvEQMIS.HeaderRow.FindControl("lblMValuePerCSpecSell");
                        Hlb3.Text = "Speculative Sell Value";

                        Label Hlb4 = (Label)gvEQMIS.HeaderRow.FindControl("lblMvalueBlankSpecbuy");
                        Hlb4.Text = "Speculative Buy Value";

                        Label TotalText = (Label)gvEQMIS.FooterRow.FindControl("lblTotalText");

                        TotalText.Text = string.Empty;

                        Label TotalMText = (Label)gvEQMIS.FooterRow.FindControl("lblSectorWiseTotalText");

                        TotalMText.Text = string.Empty;

                        Label TotalMarketValue = (Label)gvEQMIS.FooterRow.FindControl("lblFooterItemMValue");

                        TotalMarketValue.Text = string.Empty;

                        Label TotalValue = (Label)gvEQMIS.FooterRow.FindControl("lblFooterItemMValue");

                        TotalValue.Text = string.Empty;
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
                FunctionInfo.Add("Method", "AdviserEQMIS.ascx.cs:BindGrid()");
                object[] objects = new object[2];
                objects[0] = dtFrom;
                objects[1] = dtTo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

    }
}