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
        int userId;
        DataSet dsLastTradeDate;
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
                if (dsLastTradeDate.Tables[0].Rows.Count != 0)
                {
                    DateTime dtFromLastDate=(DateTime)dsLastTradeDate.Tables[0].Rows[0]["WTD_Date"];
                    DateTime dtToLastDate=(DateTime)dsLastTradeDate.Tables[0].Rows[0]["WTD_Date"];
                    txtFromDate.Text = dtFromLastDate.ToShortDateString();
                    txtToDate.Text = dtToLastDate.ToShortDateString();
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

                
                //to hide the dropdown selection for mis type for adviser and bm
                

                if(!IsPostBack)
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
            DataSet dsMfMIS = new DataSet();
            DataTable dtAdviserMFMIS = new DataTable();
            
            int rmId, branchId;
            int.TryParse(ddlBranch.SelectedValue, out branchId);
            int.TryParse(ddlRM.SelectedValue, out rmId);
            
            int ID = 0;

            if (userType == "adviser")
                ID = advisorVo.advisorId;
            else if (userType == "rm")
            {
                rmVo = (RMVo)Session[SessionContents.RmVo];
                ID = rmVo.RMId;
            }
            else if (userType == "bm")
            {
                ID = 0;
            }

            try
            {
                if (userType == "rm")
                    dsMfMIS = adviserMFMIS.GetMFMIS(userType, ID, dtFrom, dtTo);
                else
                    dsMfMIS = adviserMFMIS.GetMFMISAdviser(advisorVo.advisorId, branchId, rmId, dtFrom, dtTo);

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
            ddlPeriod.Items.Insert(0, new ListItem("Select a Period","Select a Period"));
        }

        protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {          
           
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            CultureInfo ci = new CultureInfo("en-GB");

            DateTime convertedFromDate = new DateTime();
            DateTime convertedToDate = new DateTime();
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();
            DateBo dtBo = new DateBo();
            if (rbtnPickDate.Checked)
            {
                if (txtFromDate.Text != null && txtFromDate.Text != "")
                {
                    convertedFromDate = Convert.ToDateTime(txtFromDate.Text.Trim(), ci);
                }
                else
                    convertedFromDate = DateTime.MinValue;
                if (txtToDate.Text != null && txtToDate.Text != "")
                {
                    convertedToDate = Convert.ToDateTime(txtToDate.Text.Trim(), ci);
                }
                else
                    convertedToDate = DateTime.MinValue;

                this.BindGrid(convertedFromDate, convertedToDate);
            }
            if (rbtnPickPeriod.Checked)
            {                

                if (ddlPeriod.SelectedIndex != 0)
                {
                    dtBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue, out dtFrom, out dtTo);
                    this.BindGrid(dtFrom, dtTo);
                }
            }
        }

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
            try
            {
                DataTable dt = new DataTable();
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                ddlRM.Items.Clear();
                if (ddlBranch.SelectedValue != "All")
                    dt = advisorStaffBo.GetBranchRMList(int.Parse(ddlBranch.SelectedValue));
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

                FunctionInfo.Add("Method", "AdviserMFMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }
}