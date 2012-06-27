using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoUser;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using System.Data;
using WealthERP.Customer;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.Base;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Text;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoCommon;
using VoCustomerProfiling;
using BoSuperAdmin;
using System.Configuration;
using Telerik.Web.UI;

namespace WealthERP.SuperAdmin
{
    public partial class DailyValuationMonitor : System.Web.UI.UserControl
    {
        string path = string.Empty;
        DateBo dtBo = new DateBo();
        DateTime dtTo = new DateTime();
        DateTime dtFrom = new DateTime();
        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();
        int count;

        //protected override void OnInit(EventArgs e)
        //{
        //    try
        //    {
        //        ((Pager)mypagerDuplicate).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
        //        mypagerDuplicate.EnableViewState = true;
        //        base.OnInit(e);

        //        //((Pager)mypagerAUM).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
        //        //mypagerAUM.EnableViewState = true;
        //        //base.OnInit(e);

        //        ((Pager)pgrReject).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
        //        pgrReject.EnableViewState = true;
        //        base.OnInit(e);

        //        ((Pager)myPagerNAV).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
        //        myPagerNAV.EnableViewState = true;
        //        base.OnInit(e);

        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "UnderConstruction.ascx.cs:OnInit()");
        //        object[] objects = new object[0];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        //public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        //{
        //    //try
        //    //{
        //    //    GetPageCountDuplicate();
        //    //    //GetPageCountAUM();
        //    //    if (ddlAction.SelectedValue == "DuplicateMis")
        //    //    {
        //    //        this.BindDuplicateGrid();
        //    //    }
        //    //    else if (ddlAction.SelectedValue == "AumMis")
        //    //    {
        //    //        this.BindAUMGrid();
        //    //    }
        //    //    else if (ddlAction.SelectedValue == "mfRejects")
        //    //    {
        //    //        this.BindMFRejectedGrid();
        //    //    }
        //    //    else if (ddlAction.SelectedValue == "NAVChange")
        //    //    {
        //    //        this.BindNAVPercentageChange();
        //    //    }

        //    //}

        //    //catch (BaseApplicationException Ex)
        //    //{
        //    //    throw Ex;
        //    //}
        //    //catch (Exception Ex)
        //    //{
        //    //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //    //    NameValueCollection FunctionInfo = new NameValueCollection();
        //    //    FunctionInfo.Add("Method", "UnderConstruction.ascx.cs:HandlePagerEvent()");
        //    //    object[] objects = new object[2];
        //    //    objects[0] = mypagerDuplicate.CurrentPage;
        //    //    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //    //    exBase.AdditionalInformation = FunctionInfo;
        //    //    ExceptionManager.Publish(exBase);
        //    //    throw exBase;
        //    //}
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            btnGo.Attributes.Add("onclick", "setTimeout(\"UpdateImg('Image1','/Images/Wait.gif');\",50);");
            tblMessage.Visible = false;
            gvAumMis.Visible = false;
            trbtnDelete.Visible = false;
            gvDuplicateCheck.Visible = false;
            gvMFRejectedDetails.Visible = false;
            gvNavChange.Visible = false;
            trExportFilteredNavData.Visible = false;
            trExportFilteredDupData.Visible = false;
            trExportFilteredAumData.Visible = false;
            trExportFilteredRejData.Visible = false;
            cvSelectDate.ValueToCompare = DateTime.Now.ToShortDateString();
            if (!Page.IsPostBack)
            {

                trRange.Visible = true;
                if (rbtnPickDate.Checked == true)
                {
                    trRange.Visible = true;
                    //txtFromDate.SelectedDate = DateTime.Parse(txtFromDate.SelectedDate.ToString());
                    //txtToDate.SelectedDate = DateTime.Parse(txtFromDate.SelectedDate.ToString());
                    trPeriod.Visible = false;
                }
                tblMessage.Visible = false;
                btnDelete.Visible = false;
                btnDeleteAll.Visible = false;
                //lblCurrentPage.Visible = false;
                //lblTotalRows.Visible = false;
                //lblPage.Visible = false;
                //lblTotalPage.Visible = false;
                //trpagerDuplicate.Visible = false;
                //trmypagerAUM.Visible = false;
                //trPagerReject.Visible = false;
                //trPagerNAV.Visible = false;
                //lblRejectCount.Visible = false;
                //lblRejectTotal.Visible = false;
                //lblNAVCount.Visible = false;
                //lblNAVTotal.Visible = false;
                hidDateType.Value = "DATE_RANGE";
                trDate.Visible = false;
                //pnlReject.Visible = false;
                BindPeriodDropDown();
            }
        }

        /// <summary>
        /// Bind the Period Drop down.
        /// </summary>
        private void BindPeriodDropDown()
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            DataTable dtPeriod;
            dtPeriod = XMLBo.GetDatePeriod(path);

            ddlPeriod.DataSource = dtPeriod;
            ddlPeriod.DataTextField = "PeriodType";
            ddlPeriod.DataValueField = "PeriodCode";
            ddlPeriod.DataBind();
            ddlPeriod.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            ddlPeriod.Items.RemoveAt(15);
        }
        protected void rbtnDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPickDate.Checked == true)
            {
                trRange.Visible = true;

                //txtFromDate.SelectedDate = DateTime.Parse(txtFromDate.SelectedDate.ToString());
                //txtToDate.SelectedDate = DateTime.Parse(txtToDate.SelectedDate.ToString());
                trPeriod.Visible = false;
            }
            else if (rbtnPickPeriod.Checked == true)
            {
                trRange.Visible = false;
                trPeriod.Visible = true;
                BindPeriodDropDown();
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {


            if (ddlAction.SelectedValue != "NAVChange")
            {
                CalculateDateRange(out dtFrom, out dtTo);
                hdnFromDate.Value = dtFrom.ToString();
                hdnToDate.Value = dtTo.ToString();
                hdnSelectDate.Value = txtDate.SelectedDate.ToString();
            }
            if (ddlAction.SelectedValue == "DuplicateMis")
            {
                trExportFilteredNavData.Visible = false;
                trExportFilteredDupData.Visible = false;
                trExportFilteredAumData.Visible = false;
                trExportFilteredRejData.Visible = false;
                gvAumMis.Visible = false;
                trbtnDelete.Visible = true;
                gvDuplicateCheck.Visible = true;
                gvMFRejectedDetails.Visible = false;
                gvNavChange.Visible = false;
                BindDuplicateGrid();

            }
            else if (ddlAction.SelectedValue == "AumMis")
            {

                trExportFilteredNavData.Visible = false;
                trExportFilteredDupData.Visible = false;
                trExportFilteredAumData.Visible = false;
                trExportFilteredRejData.Visible = false;
                BindAUMGrid();
                gvAumMis.Visible = true;
                trbtnDelete.Visible = false;
                gvDuplicateCheck.Visible = false;
                gvMFRejectedDetails.Visible = false;
                gvNavChange.Visible = false;
                btnDelete.Visible = false;
                btnDeleteAll.Visible = false;
            }
            else if (ddlAction.SelectedValue == "mfRejects")
            {
                trExportFilteredNavData.Visible = false;
                trExportFilteredDupData.Visible = false;
                trExportFilteredAumData.Visible = false;
                trExportFilteredRejData.Visible = false;
                BindMFRejectedGrid();
                gvAumMis.Visible = false;
                trbtnDelete.Visible = false;
                gvDuplicateCheck.Visible = false;
                gvMFRejectedDetails.Visible = true;
                gvNavChange.Visible = false;
                btnDelete.Visible = false;
                btnDeleteAll.Visible = false;
            }
            else if (ddlAction.SelectedValue == "NAVChange")
            {

                trExportFilteredNavData.Visible = false;
                trExportFilteredDupData.Visible = false;
                trExportFilteredAumData.Visible = false;
                trExportFilteredRejData.Visible = false;
                BindNAVPercentageChange();
                gvAumMis.Visible = false;
                trbtnDelete.Visible = false;
                gvDuplicateCheck.Visible = false;
                gvMFRejectedDetails.Visible = false;
                gvNavChange.Visible = true;
                btnDelete.Visible = false;
                btnDeleteAll.Visible = false;
            }

            gvAumMis_Init(sender, e);

        }

        private void BindNAVPercentageChange()
        {
            double NavPer;
            DataSet dsGetNAV;
            DataTable dtGetNAV;
            NavPer = double.Parse(ddlNavPer.SelectedValue);
            dsGetNAV = superAdminOpsBo.GetNAVPercentage(DateTime.Parse(txtDate.SelectedDate.ToString()), NavPer);
            dtGetNAV = dsGetNAV.Tables[0];
            try
            {
                if (dtGetNAV.Rows.Count > 0)
                {
                    trExportFilteredNavData.Visible = true;
                    DataTable dtGetNAVPercentageDetails = new DataTable();
                    dtGetNAVPercentageDetails.Columns.Add("SchemeCode");
                    dtGetNAVPercentageDetails.Columns.Add("SchemeName");
                    dtGetNAVPercentageDetails.Columns.Add("CurrentNAV");
                    dtGetNAVPercentageDetails.Columns.Add("PreviousNAV");
                    dtGetNAVPercentageDetails.Columns.Add("PercentChange");
                    if (Cache["duplicatecheckList"] == null)
                    {
                        Cache.Insert("dsGetNAVList", dsGetNAV);
                    }
                    else
                    {
                        Cache.Remove("dsGetNAVList");
                        Cache.Insert("dsGetNAVList", dsGetNAV);
                    }

                    DataRow drGetNAVPercentageDetails;

                    foreach (DataRow dr in dtGetNAV.Rows)
                    {
                        drGetNAVPercentageDetails = dtGetNAVPercentageDetails.NewRow();

                        drGetNAVPercentageDetails["SchemeCode"] = dr["SchemeCode"].ToString();
                        drGetNAVPercentageDetails["SchemeName"] = dr["PASP_SchemePlanName"].ToString();
                        drGetNAVPercentageDetails["CurrentNAV"] = dr["Todays_NAV"].ToString();
                        drGetNAVPercentageDetails["PreviousNAV"] = dr["lastday_Nav"].ToString();
                        drGetNAVPercentageDetails["PercentChange"] = double.Parse(dr["PerDiff"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        dtGetNAVPercentageDetails.Rows.Add(drGetNAVPercentageDetails);
                    }
                    //lblNAVTotal.Text = hdnRecordCount.Value = count.ToString();
                    gvNavChange.DataSource = dtGetNAVPercentageDetails;
                    gvNavChange.DataBind();
                    gvNavChange.Visible = true;
                    //this.GetPageCountNAV();
                    //lblNAVCount.Visible = true;
                    //lblNAVTotal.Visible = true;
                    gvDuplicateCheck.Visible = false;
                    //lblCurrentPage.Visible = false;
                    //lblTotalRows.Visible = false;
                    //lblPage.Visible = false;
                    //lblTotalPage.Visible = false;
                    //trpagerDuplicate.Visible = false;
                    //trmypagerAUM.Visible = false;
                    btnDelete.Visible = false;
                    btnDeleteAll.Visible = false;
                    gvMFRejectedDetails.Visible = false;
                    //trPagerNAV.Visible = true;

                }
                else
                {
                    gvNavChange.Visible = false;
                    tblMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    ErrorMessage.InnerText = "No Records Found...!";
                    gvDuplicateCheck.Visible = false;
                    trbtnDelete.Visible = false;
                    gvAumMis.Visible = false;
                    //lblCurrentPage.Visible = false;
                    //lblTotalRows.Visible = false;
                    //lblPage.Visible = false;
                    //lblTotalPage.Visible = false;
                    //trpagerDuplicate.Visible = false;
                    //trmypagerAUM.Visible = false;
                    btnDelete.Visible = false;
                    btnDeleteAll.Visible = false;
                    gvMFRejectedDetails.Visible = false;
                    //trPagerReject.Visible = false;
                    //lblRejectCount.Visible = false;
                    //lblRejectTotal.Visible = false;
                    //pnlReject.Visible = false;
                    //lblNAVCount.Visible = false;
                    //lblNAVTotal.Visible = false;
                    //trPagerNAV.Visible = false;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        ///  To Get All adviser's total AUM
        /// </summary>
        private void BindAUMGrid()
        {
            Dictionary<string, string> genOrganizationData = new Dictionary<string, string>();
            DataSet dsAumMis = new DataSet();
            DataTable dtAumMis;

            dsAumMis = superAdminOpsBo.GetAllAdviserAUM(DateTime.Parse(hdnFromDate.Value.ToString()), DateTime.Parse(hdnToDate.Value.ToString()), ddlMonitorfr.SelectedValue);
            dtAumMis = dsAumMis.Tables[0];
            if (Cache["AUMList"] == null)
            {
                Cache.Insert("AUMList", dsAumMis);
            }
            else
            {
                Cache.Remove("AUMList");
                Cache.Insert("AUMList", dsAumMis);
            }

            if (dtAumMis.Rows.Count > 0)
            {
                trExportFilteredAumData.Visible = true;
                //lblTotalPage.Text = hdnRecordCount.Value = count.ToString();
                gvAumMis.DataSource = dtAumMis;
                gvAumMis.DataBind();

                gvAumMis.Visible = true;
                trbtnDelete.Visible = false;
                gvDuplicateCheck.Visible = false;
                //lblCurrentPage.Visible = false;
                //lblTotalRows.Visible = false;
                //lblPage.Visible = true;
                //lblTotalPage.Visible = true;
                //trpagerDuplicate.Visible = false;
                //trmypagerAUM.Visible = true;
                btnDelete.Visible = false;
                btnDeleteAll.Visible = false;
                gvMFRejectedDetails.Visible = false;
                //tblMessage.Visible = false;
                //ErrorMessage.Visible = false;
                //foreach (DataRow dr in dsAumMis.Tables[0].Rows)
                //{
                //    genOrganizationData.Add(dr[0].ToString(), dr[0].ToString());
                //}
                //DropDownList ddlAdviserNameDate = GetOrganization();
                //if (ddlAdviserNameDate != null)
                //{
                //    ddlAdviserNameDate.DataSource = genOrganizationData;
                //    ddlAdviserNameDate.DataTextField = "Key";
                //    ddlAdviserNameDate.DataValueField = "Value";
                //    ddlAdviserNameDate.DataBind();
                //    ddlAdviserNameDate.Items.Insert(0, new ListItem("Select", "Select"));
                //}
                //if (hdnAdviserNameAUMFilter.Value != "")
                //{
                //    ddlAdviserNameDate.SelectedValue = hdnAdviserNameAUMFilter.Value;
                //}
                //this.GetPageCountAUM();
                //trPagerReject.Visible = false;
                //trPagerNAV.Visible = false;
                //lblRejectCount.Visible = false;
                //lblRejectTotal.Visible = false;
                //pnlReject.Visible = false;
                gvNavChange.Visible = false;
                //lblNAVCount.Visible = false;
                //lblNAVTotal.Visible = false;
            }
            else
            {
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
                gvDuplicateCheck.Visible = false;
                gvAumMis.Visible = false;
                trbtnDelete.Visible = false;
                //lblCurrentPage.Visible = false;
                //lblTotalRows.Visible = false;
                //lblPage.Visible = false;
                //lblTotalPage.Visible = false;
                //trpagerDuplicate.Visible = false;
                // trmypagerAUM.Visible = false;
                btnDelete.Visible = false;
                btnDeleteAll.Visible = false;
                gvMFRejectedDetails.Visible = false;
                //trPagerNAV.Visible = false;
                //trPagerReject.Visible = false;
                //lblRejectCount.Visible = false;
                //lblRejectTotal.Visible = false;
                //pnlReject.Visible = false;
                gvNavChange.Visible = false;
                //lblNAVCount.Visible = false;
                //lblNAVTotal.Visible = false;
            }
        }

        //private DropDownList GetOrganization()
        //{
        //    DropDownList ddl = new DropDownList();
        //    if ((DropDownList)gvAumMis.FindControl("ddlAdviserNameDate") != null)
        //    {
        //        ddl = (DropDownList)gvAumMis.FindControl("ddlAdviserNameDate");
        //    }
        //    return ddl;
        //}
        /// <summary>
        /// check the duplicate records
        /// </summary>

        private void BindDuplicateGrid()
        {
            Dictionary<string, string> genAdviserDdl = new Dictionary<string, string>();
            DataSet dsduplicatecheck;
            DataTable dtDuplicateCheck;
            dsduplicatecheck = superAdminOpsBo.GetAllAdviserDuplicateRecords(DateTime.Parse(hdnFromDate.Value.ToString()), DateTime.Parse(hdnToDate.Value.ToString()), hdnAdviserIdDupli.Value, hdnOrgNameDupli.Value, hdnFolioiNoDupli.Value, hdnSchemeDupli.Value);
            dtDuplicateCheck = dsduplicatecheck.Tables[0];
            if (dtDuplicateCheck.Rows.Count > 0)
            {
                //lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                trExportFilteredDupData.Visible = true;
                gvDuplicateCheck.DataSource = dtDuplicateCheck;
                gvDuplicateCheck.DataBind();
                gvDuplicateCheck.Visible = true;
                trbtnDelete.Visible = true;
                gvAumMis.Visible = false;
                tblMessage.Visible = false;
                ErrorMessage.Visible = false;
                //lblCurrentPage.Visible = true;
                if (Cache["duplicatecheckList"] == null)
                {
                    Cache.Insert("duplicatecheckList", dsduplicatecheck);
                }
                else
                {
                    Cache.Remove("duplicatecheckList");
                    Cache.Insert("duplicatecheckList", dsduplicatecheck);
                }

                //foreach (DataRow dr in dsduplicatecheck.Tables[1].Rows)
                //{
                //    genAdviserDdl.Add(dr[0].ToString(), dr[0].ToString());
                //}
                //DropDownList ddlOrganization = GetAdviserDdl();
                //if (ddlOrganization != null)
                //{
                //    ddlOrganization.DataSource = genAdviserDdl;
                //    ddlOrganization.DataTextField = "Key";
                //    ddlOrganization.DataValueField = "Value";
                //    ddlOrganization.DataBind();
                //    ddlOrganization.Items.Insert(0, new ListItem("Select", "Select"));
                //}
                //if (hdnOrgNameDupli.Value != "")
                //{
                //    ddlOrganization.SelectedValue = hdnOrgNameDupli.Value;
                //}
                //BindFolioNumber(dsduplicatecheck.Tables[2]);
                //BindSchemeName(dsduplicatecheck.Tables[3]);
                //BindAdviserIdDuplicate(dsduplicatecheck.Tables[4]);
                //this.GetPageCountDuplicate();
                //lblTotalRows.Visible = true;
                //lblPage.Visible = false;
                //lblTotalPage.Visible = false;
                //trmypagerAUM.Visible = false;
                //trpagerDuplicate.Visible = true;
                btnDelete.Visible = true;
                btnDeleteAll.Visible = true;
                gvMFRejectedDetails.Visible = false;
                //trPagerReject.Visible = false;
                //trPagerNAV.Visible = false;
                //lblRejectCount.Visible = false;
                //lblRejectTotal.Visible = false;
                //pnlReject.Visible = false;
                gvNavChange.Visible = false;
                //lblNAVCount.Visible = false;
                //lblNAVTotal.Visible = false;
            }
            else
            {
                gvDuplicateCheck.Visible = false;
                trbtnDelete.Visible = false;
                gvAumMis.Visible = false;
                gvMFRejectedDetails.Visible = false;
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
                //lblCurrentPage.Visible = false;
                //lblTotalRows.Visible = false;
                //lblPage.Visible = false;
                //lblTotalPage.Visible = false;
                //trmypagerAUM.Visible = false;
                //trpagerDuplicate.Visible = false;
                btnDelete.Visible = false;
                btnDeleteAll.Visible = false;
                //trPagerReject.Visible = false;
                //trPagerNAV.Visible = false;
                //lblRejectCount.Visible = false;
                //lblRejectTotal.Visible = false;
                //pnlReject.Visible = false;
                gvNavChange.Visible = false;
                //lblNAVCount.Visible = false;
                //lblNAVTotal.Visible = false;
            }
        }

        //private void BindAdviserIdDuplicate(DataTable dtAdviserDupli)
        //{
        //    Dictionary<string, string> genAdviserIdDupli = new Dictionary<string, string>();
        //    if (dtAdviserDupli.Rows.Count > 0)
        //    {
        //        // Get the Reject Reason Codes Available into Generic Dictionary
        //        foreach (DataRow dr in dtAdviserDupli.Rows)
        //        {
        //            genAdviserIdDupli.Add(dr[0].ToString(), dr[0].ToString());
        //        }

        //DropDownList ddlSchemeName = GetAdviserIdddlDupli();
        //if (ddlSchemeName != null)
        //{
        //    ddlSchemeName.DataSource = genAdviserIdDupli;
        //    ddlSchemeName.DataTextField = "Key";
        //    ddlSchemeName.DataValueField = "Value";
        //    ddlSchemeName.DataBind();
        //    ddlSchemeName.Items.Insert(0, new ListItem("Select", "Select"));
        //}

        //if (hdnAdviserIdDupli.Value != "")
        //{
        //    ddlSchemeName.SelectedValue = hdnAdviserIdDupli.Value.ToString();
        //}
        //   }
        // }

        //private DropDownList GetAdviserIdddlDupli()
        //{
        //    DropDownList ddl = new DropDownList();
        //    if ((DropDownList)gvDuplicateCheck.FindControl("ddlAdviserId") != null)
        //    {
        //        ddl = (DropDownList)gvDuplicateCheck.FindControl("ddlAdviserId");
        //    }
        //    return ddl;
        //}
        //private DropDownList GetAdviserDdl()
        //{
        //    DropDownList ddl = new DropDownList();
        //    if ((DropDownList)gvDuplicateCheck.FindControl("ddlOrganization") != null)
        //    {
        //        ddl = (DropDownList)gvDuplicateCheck.FindControl("ddlOrganization");
        //    }
        //    return ddl;
        //}


        /// <summary>
        /// Get the From and To Date of reports
        /// </summary>
        private void CalculateDateRange(out DateTime fromDate, out DateTime toDate)
        {
            if (rbtnPickDate.Checked == true)
            {
                fromDate = DateTime.Parse(txtFromDate.SelectedDate.ToString());
                toDate = DateTime.Parse(txtToDate.SelectedDate.ToString());
            }
            else if (rbtnPickPeriod.Checked == true)
            {
                dtBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue.ToString(), out dtFrom, out dtTo);
                fromDate = dtFrom;
                toDate = dtTo;
            }
            else
            {
                fromDate = DateTime.MinValue;
                toDate = DateTime.MinValue;
            }

        }
        //protected void gvDuplicateCheck_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label lblDuplicate = e.Row.FindControl("lblDuplicate") as Label;
        //        string duplicate = null;
        //        duplicate = lblDuplicate.Text;
        //        if (duplicate == "1")
        //            lblDuplicate.Text = "No";
        //        else
        //            lblDuplicate.Text = "Yes";
        //    }
        //}
        //private void GetPageCountDuplicate()
        //{
        //    string upperlimit = null;
        //    int rowCount = 0;
        //    int ratio = 0;
        //    string lowerlimit = null;
        //    string PageRecords = null;
        //    try
        //    {
        //        if (hdnRecordCount.Value.ToString() != "")
        //            rowCount = Convert.ToInt32(hdnRecordCount.Value);
        //        if (rowCount > 0)
        //        {
        //            ratio = rowCount / 10;
        //            mypagerDuplicate.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
        //            mypagerDuplicate.Set_Page(mypagerDuplicate.CurrentPage, mypagerDuplicate.PageCount);
        //            if (((mypagerDuplicate.CurrentPage - 1) * 10) != 0)
        //                lowerlimit = (((mypagerDuplicate.CurrentPage - 1) * 10) + 1).ToString();
        //            else
        //                lowerlimit = "1";
        //            upperlimit = (mypagerDuplicate.CurrentPage * 10).ToString();
        //            if (mypagerDuplicate.CurrentPage == mypagerDuplicate.PageCount)
        //                upperlimit = hdnRecordCount.Value;
        //            PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
        //            lblCurrentPage.Text = PageRecords;
        //            lblPage.Text = PageRecords;
        //            hdnCurrentPage.Value = mypagerDuplicate.CurrentPage.ToString();
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:GetPageCount()");

        //        object[] objects = new object[5];
        //        objects[0] = upperlimit;
        //        objects[1] = rowCount;
        //        objects[2] = ratio;
        //        objects[3] = lowerlimit;
        //        objects[4] = PageRecords;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}
        //private void BindFolioNumber(DataTable dtFolioNumber)
        //{
        //    Dictionary<string, string> genDictFolioNumber = new Dictionary<string, string>();
        //    if (dtFolioNumber.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dtFolioNumber.Rows)
        //        {
        //            genDictFolioNumber.Add(dr[0].ToString(), dr[0].ToString());
        //        }

        //        //DropDownList ddlFolioNumber = GetFolioNumberDupli();
        //        //if (ddlFolioNumber != null)
        //        //{
        //        //    ddlFolioNumber.DataSource = genDictFolioNumber;
        //        //    ddlFolioNumber.DataTextField = "Key";
        //        //    ddlFolioNumber.DataValueField = "Value";
        //        //    ddlFolioNumber.DataBind();
        //        //    ddlFolioNumber.Items.Insert(0, new ListItem("Select", "Select"));
        //        //}

        //        //if (hdnFolioiNoDupli.Value != "")
        //        //{
        //        //    ddlFolioNumber.SelectedValue = hdnFolioiNoDupli.Value.ToString();
        //        //}
        //    }
        //}
        //private DropDownList GetFolioNumberDupli()
        //{
        //    DropDownList ddl = new DropDownList();
        //    if ((DropDownList)gvDuplicateCheck.FindControl("ddlFolioNo") != null)
        //    {
        //        ddl = (DropDownList)gvDuplicateCheck.FindControl("ddlFolioNo");
        //    }
        //    return ddl;
        //}
        //private void BindSchemeName(DataTable dtSchemeName)
        //{
        //    Dictionary<string, string> genDictSchemeName = new Dictionary<string, string>();
        //    if (dtSchemeName.Rows.Count > 0)
        //    {
        //        // Get the Reject Reason Codes Available into Generic Dictionary
        //        foreach (DataRow dr in dtSchemeName.Rows)
        //        {
        //            genDictSchemeName.Add(dr[0].ToString(), dr[0].ToString());
        //        }

        //DropDownList ddlSchemeName = GetSchemeNameDDL();
        //if (ddlSchemeName != null)
        //{
        //    ddlSchemeName.DataSource = genDictSchemeName;
        //    ddlSchemeName.DataTextField = "Key";
        //    ddlSchemeName.DataValueField = "Value";
        //    ddlSchemeName.DataBind();
        //    ddlSchemeName.Items.Insert(0, new ListItem("Select", "Select"));
        //}

        //if (hdnSchemeDupli.Value != "")
        //{
        //    ddlSchemeName.SelectedValue = hdnSchemeDupli.Value.ToString();
        //}
        //    }
        //}
        //private DropDownList GetSchemeNameDDL()
        //{
        //    DropDownList ddl = new DropDownList();
        //    if ((DropDownList)gvDuplicateCheck.FindControl("ddlScheme") != null)
        //    {
        //        ddl = (DropDownList)gvDuplicateCheck.FindControl("ddlScheme");
        //    }
        //    return ddl;
        //}

        //protected void ddlAdviserNameDate_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList ddlAdviserName = GetOrganization();

        //    if (ddlAdviserName != null)
        //    {
        //        if (ddlAdviserName.SelectedIndex != 0)
        //        {   // Bind the Grid with Only Selected Values
        //            hdnAdviserNameAUMFilter.Value = ddlAdviserName.SelectedItem.Text;
        //            BindAUMGrid();
        //            // ddlAdviserName.SelectedItem.Text = hdnAdviserNameAUMFilter.Value;
        //        }
        //        else
        //        {   // Bind the Grid with Only All Values
        //            hdnAdviserNameAUMFilter.Value = "";
        //            BindAUMGrid();
        //        }
        //    }
        //}
        //private void GetPageCountAUM()
        //{
        //    string upperlimit = null;
        //    int rowCount = 0;
        //    int ratio = 0;
        //    string lowerlimit = null;
        //    string PageRecords = null;
        //    try
        //    {
        //        if (hdnRecordCount.Value.ToString() != "")
        //            rowCount = Convert.ToInt32(hdnRecordCount.Value);
        //        if (rowCount > 0)
        //        {
        //            ratio = rowCount / 10;
        //            mypagerAUM.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
        //            mypagerAUM.Set_Page(mypagerAUM.CurrentPage, mypagerAUM.PageCount);
        //            if (((mypagerDuplicate.CurrentPage - 1) * 10) != 0)
        //                lowerlimit = (((mypagerAUM.CurrentPage - 1) * 10) + 1).ToString();
        //            else
        //                lowerlimit = "1";
        //            upperlimit = (mypagerAUM.CurrentPage * 10).ToString();
        //            if (mypagerAUM.CurrentPage == mypagerAUM.PageCount)
        //                upperlimit = hdnRecordCount.Value;
        //            PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
        //            lblCurrentPage.Text = PageRecords;
        //            lblPage.Text = PageRecords;
        //            hdnCurrentPage.Value = mypagerAUM.CurrentPage.ToString();
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:GetPageCount()");

        //        object[] objects = new object[5];
        //        objects[0] = upperlimit;
        //        objects[1] = rowCount;
        //        objects[2] = ratio;
        //        objects[3] = lowerlimit;
        //        objects[4] = PageRecords;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //}
        /// <summary>
        /// Bind reject reason grid
        /// </summary>
        private void BindMFRejectedGrid()
        {
            Dictionary<string, string> genDictRejectReason = new Dictionary<string, string>();

            Dictionary<string, string> genDictProcessId = new Dictionary<string, string>();

            Dictionary<string, string> genDictAdviserId = new Dictionary<string, string>();

            DataSet dsRejectedRecords = new DataSet();
            dsRejectedRecords = superAdminOpsBo.GetMfrejectedDetails(DateTime.Parse(hdnFromDate.Value.ToString()), DateTime.Parse(hdnToDate.Value.ToString()), hdnRejectReasonFilter.Value, hdnAdviserIdFilter.Value, hdnProcessIdFilter.Value);

            if (dsRejectedRecords.Tables.Count > 0)
            {
                trExportFilteredRejData.Visible = true;
                //lblRejectTotal.Text = hdnRecordCount.Value = count.ToString();
                gvMFRejectedDetails.DataSource = dsRejectedRecords;
                gvMFRejectedDetails.DataBind();

                if (Cache["RejectedRecordsList"] == null)
                {
                    Cache.Insert("RejectedRecordsList", dsRejectedRecords);
                }
                else
                {
                    Cache.Remove("RejectedRecordsList");
                    Cache.Insert("RejectedRecordsList", dsRejectedRecords);
                }


                //foreach (DataRow dr in dsRejectedRecords.Tables[1].Rows)
                //{
                //    genDictRejectReason.Add(dr["WRR_RejectReasonDescription"].ToString(), dr[1].ToString());
                //}
                //DropDownList ddlRejectReason = GetRejectReasonDDL();
                //if (ddlRejectReason != null)
                //{
                //    ddlRejectReason.DataSource = genDictRejectReason;
                //    ddlRejectReason.DataTextField = "Key";
                //    ddlRejectReason.DataValueField = "Value";
                //    ddlRejectReason.DataBind();
                //    ddlRejectReason.Items.Insert(0, new ListItem("Select", "Select"));
                //}
                //foreach (DataRow dr in dsRejectedRecords.Tables[2].Rows)
                //{
                //    genDictProcessId.Add(dr[0].ToString(), dr[0].ToString());
                //}
                //DropDownList ddlProcessid = GetProcessIdDDL();
                //if (ddlRejectReason != null)
                //{
                //    ddlProcessid.DataSource = genDictProcessId;
                //    ddlProcessid.DataTextField = "Key";
                //    ddlProcessid.DataValueField = "Value";
                //    ddlProcessid.DataBind();
                //    ddlProcessid.Items.Insert(0, new ListItem("Select", "Select"));
                //}
                //foreach (DataRow dr in dsRejectedRecords.Tables[3].Rows)
                //{
                //    genDictAdviserId.Add(dr["A_AdviserId"].ToString(), dr[1].ToString());
                //}
                //DropDownList ddlAdviserId = GetAdviserIdDDL();
                //if (ddlAdviserId != null)
                //{
                //    ddlAdviserId.DataSource = genDictAdviserId;
                //    ddlAdviserId.DataTextField = "Key";
                //    ddlAdviserId.DataValueField = "Value";
                //    ddlAdviserId.DataBind();
                //    ddlAdviserId.Items.Insert(0, new ListItem("Select", "Select"));
                //}
                gvMFRejectedDetails.Visible = true;
                gvDuplicateCheck.Visible = false;
                //this.GetPageCountReject();
                gvAumMis.Visible = false;
                tblMessage.Visible = false;
                ErrorMessage.Visible = false;
                //lblCurrentPage.Visible = false;
                // lblTotalRows.Visible = false;
                //lblPage.Visible = false;
                //lblTotalPage.Visible = false;
                //trmypagerAUM.Visible = false;
                //trpagerDuplicate.Visible = false;
                //trPagerReject.Visible = true;
                //lblRejectCount.Visible = true;
                //lblRejectTotal.Visible = true;
                //pnlReject.Visible = true;
                gvNavChange.Visible = false;
                //lblNAVCount.Visible = false;
                //lblNAVTotal.Visible = false;
                //trPagerNAV.Visible = false;
                //btnDelete.Visible = false;
                //btnDeleteAll.Visible = false;

                //if (hdnRejectReasonFilter.Value != "")
                //{
                //    ddlRejectReason.SelectedValue = hdnRejectReasonFilter.Value;
                //}
                //if (hdnAdviserIdFilter.Value != "")
                //{
                //    ddlAdviserId.SelectedValue = hdnAdviserIdFilter.Value;
                //}

                //if (hdnProcessIdFilter.Value != "")
                //{
                //    ddlProcessid.SelectedValue = hdnProcessIdFilter.Value;
                //}
            }
            else
            {
                gvMFRejectedDetails.Visible = false;
                gvDuplicateCheck.Visible = false;
                trbtnDelete.Visible = false;
                gvAumMis.Visible = false;
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
                //lblCurrentPage.Visible = false;
                //lblTotalRows.Visible = false;
                //lblPage.Visible = false;
                //lblTotalPage.Visible = false;
                //trmypagerAUM.Visible = false;
                //trpagerDuplicate.Visible = false;
                btnDelete.Visible = false;
                btnDeleteAll.Visible = false;
                //trPagerReject.Visible = false;
                //lblRejectCount.Visible = false;
                //lblRejectTotal.Visible = false;
                //pnlReject.Visible = false;
                gvNavChange.Visible = false;
                //lblNAVCount.Visible = false;
                //lblNAVTotal.Visible = false;
                //trPagerNAV.Visible = false;
            }



        }
        private DropDownList GetRejectReasonDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvMFRejectedDetails.FindControl("ddlRejectReason") != null)
            {
                ddl = (DropDownList)gvMFRejectedDetails.FindControl("ddlRejectReason");
            }
            return ddl;
        }
        private DropDownList GetProcessIdDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvMFRejectedDetails.FindControl("ddlProcessId") != null)
            {
                ddl = (DropDownList)gvMFRejectedDetails.FindControl("ddlProcessId");
            }
            return ddl;
        }

        //private DropDownList GetAdviserIdDDL()
        //{
        //    DropDownList ddl = new DropDownList();
        //    if ((DropDownList)gvMFRejectedDetails.FindControl("ddlAdviserId") != null)
        //    {
        //        ddl = (DropDownList)gvMFRejectedDetails.FindControl("ddlAdviserId");
        //    }
        //    return ddl;
        //}
        protected void ddlRejectReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlReject = GetRejectReasonDDL();
            if (ddlReject != null)
            {
                if (ddlReject.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnRejectReasonFilter.Value = ddlReject.SelectedValue;
                    BindMFRejectedGrid();
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnRejectReasonFilter.Value = "";
                    BindMFRejectedGrid();
                }
            }
        }
        protected void ddlProcessId_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlProcessId = GetProcessIdDDL();
            if (ddlProcessId != null)
            {
                if (ddlProcessId.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnProcessIdFilter.Value = ddlProcessId.SelectedValue;
                    BindMFRejectedGrid();
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnProcessIdFilter.Value = "";
                    BindMFRejectedGrid();
                }
            }
        }
        //protected void ddlAdviserIdDuplicate_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList ddladviserIdDupli = GetAdviserIdddlDupli();
        //    if (ddladviserIdDupli != null)
        //    {
        //        if (ddladviserIdDupli.SelectedIndex != 0)
        //        {
        //            hdnAdviserIdDupli.Value = ddladviserIdDupli.SelectedValue;
        //            BindDuplicateGrid();
        //        }
        //        else
        //        {
        //            hdnAdviserIdDupli.Value = "";
        //            BindDuplicateGrid();
        //        }
        //    }
        //}

        //protected void ddlAdviserId_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList ddlAdviserId = GetAdviserIdDDL();
        //    if (ddlAdviserId != null)
        //    {
        //        if (ddlAdviserId.SelectedIndex != 0)
        //        {   // Bind the Grid with Only Selected Values
        //            hdnAdviserIdFilter.Value = ddlAdviserId.SelectedValue;
        //            BindMFRejectedGrid();
        //        }
        //        else
        //        {   // Bind the Grid with Only All Values
        //            hdnAdviserIdFilter.Value = "";
        //            BindMFRejectedGrid();
        //        }
        //    }
        //}

        //protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList ddlOrgDupli = GetAdviserDdl();
        //    if (ddlOrgDupli != null)
        //    {
        //        if (ddlOrgDupli.SelectedIndex != 0)
        //        {
        //            hdnOrgNameDupli.Value = ddlOrgDupli.SelectedValue;
        //            BindDuplicateGrid();
        //        }
        //        else
        //        {
        //            hdnOrgNameDupli.Value = "";
        //            BindDuplicateGrid();
        //        }
        //    }
        //}
        //protected void ddlFolioNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList ddlFolioNoDupli = GetFolioNumberDupli();
        //    if (ddlFolioNoDupli != null)
        //    {
        //        if (ddlFolioNoDupli.SelectedIndex != 0)
        //        {
        //            hdnFolioiNoDupli.Value = ddlFolioNoDupli.SelectedValue;
        //            BindDuplicateGrid();
        //        }
        //        else
        //        {
        //            hdnFolioiNoDupli.Value = "";
        //            BindDuplicateGrid();
        //        }
        //    }
        //}
        //protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList ddlSchemeNameDupli = GetSchemeNameDDL();
        //    if (ddlSchemeNameDupli != null)
        //    {
        //        if (ddlSchemeNameDupli.SelectedIndex != 0)
        //        {
        //            hdnSchemeDupli.Value = ddlSchemeNameDupli.SelectedValue;
        //            BindDuplicateGrid();
        //        }
        //        else
        //        {
        //            hdnSchemeDupli.Value = "";
        //            BindDuplicateGrid();
        //        }
        //    }
        //}
        protected void btnDelete_Click(object sender, EventArgs e)
        {

            int i = 0;
            int gvAdviserId = 0;
            int gvAccountId = 0;
            double gvNetHolding = 0;
            int gvSchemeCode = 0;
            int selectedRow = 0;
            DateTime gvValuationDate = new DateTime();

            foreach (GridDataItem gvRow in gvDuplicateCheck.Items)
            {

                CheckBox chk = (CheckBox)gvRow.FindControl("chkDelete");
                if (chk.Checked)
                {
                    i++;
                    gvAdviserId = int.Parse(gvRow.GetDataKeyValue("A_AdviserId").ToString());
                    gvAccountId = Convert.ToInt32(gvRow.GetDataKeyValue("CMFA_AccountId").ToString());
                    gvNetHolding = double.Parse(gvRow.GetDataKeyValue("CMFNP_NetHoldings").ToString());
                    gvSchemeCode = Convert.ToInt32(gvRow.GetDataKeyValue("PASP_SchemePlanCode").ToString());
                    gvValuationDate = DateTime.Parse(gvRow.GetDataKeyValue("CMFNP_ValuationDate").ToString());
                    //drDelete[0] = strKeyValue;
                    //drDelete[1] = chkbxRow.Checked.ToString().ToLower();
                    //dt.Rows.Add(drDelete);

                    superAdminOpsBo.DeleteDuplicateRecord(gvAdviserId, gvAccountId, gvNetHolding, gvSchemeCode, gvValuationDate);

                }


            }
            
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a record to delete!');", true);
            }
            else
            {
                //DuplicateDelete();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Record has been deleted successfully.');", true);
            }
            BindDuplicateGrid();
            
        }
        //private void DuplicateDelete()
        //{

        //    int gvAdviserId = 0;
        //    int gvAccountId = 0;
        //    double gvNetHolding = 0;
        //    int gvSchemeCode = 0;
        //    int selectedRow = 0;
        //    DateTime gvValuationDate = new DateTime();
        //    foreach ( gvRow in gvDuplicateCheck.Items)
        //    {


        //        if (((CheckBox)gvRow.FindControl("chkDelete")).Checked == true)
        //        {

        //            selectedRow = gvRow.ItemIndex + 1;

        //            gvAdviserId =int.Parse(gvDuplicateCheck.MasterTableView.DataKeyValues[selectedRow]["A_AdviserId"].ToString());
        //            gvAccountId = Convert.ToInt32(gvDuplicateCheck.MasterTableView.DataKeyValues[selectedRow-1]["CMFA_AccountId"].ToString());
        //            gvNetHolding = double.Parse(gvDuplicateCheck.MasterTableView.DataKeyValues[selectedRow - 1]["CMFNP_NetHoldings"].ToString());
        //            gvSchemeCode = Convert.ToInt32(gvDuplicateCheck.MasterTableView.DataKeyValues[selectedRow - 1]["PASP_SchemePlanCode"].ToString());
        //            gvValuationDate = DateTime.Parse(gvDuplicateCheck.MasterTableView.DataKeyValues[selectedRow - 1]["CMFNP_ValuationDate"].ToString());

        //            superAdminOpsBo.DeleteDuplicateRecord(gvAdviserId, gvAccountId, gvNetHolding, gvSchemeCode, gvValuationDate);


        //        }
        //    }
        //    BindDuplicateGrid();
        //}
        //private void GetPageCountReject()
        //{
        //    string upperlimit = null;
        //    int rowCount = 0;
        //    int ratio = 0;
        //    string lowerlimit = null;
        //    string PageRecords = null;
        //    try
        //    {
        //        if (hdnRecordCount.Value.ToString() != "")
        //            rowCount = Convert.ToInt32(hdnRecordCount.Value);
        //        if (rowCount > 0)
        //        {
        //            ratio = rowCount / 10;
        //            pgrReject.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
        //            pgrReject.Set_Page(pgrReject.CurrentPage, pgrReject.PageCount);
        //            if (((pgrReject.CurrentPage - 1) * 10) != 0)
        //                lowerlimit = (((pgrReject.CurrentPage - 1) * 10) + 1).ToString();
        //            else
        //                lowerlimit = "1";
        //            upperlimit = (pgrReject.CurrentPage * 10).ToString();
        //            if (pgrReject.CurrentPage == pgrReject.PageCount)
        //                upperlimit = hdnRecordCount.Value;
        //            PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
        //            lblCurrentPage.Text = PageRecords;
        //            //lblRejectCount.Text = PageRecords;
        //            hdnCurrentPage.Value = pgrReject.CurrentPage.ToString();
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:GetPageCount()");

        //        object[] objects = new object[5];
        //        objects[0] = upperlimit;
        //        objects[1] = rowCount;
        //        objects[2] = ratio;
        //        objects[3] = lowerlimit;
        //        objects[4] = PageRecords;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        //protected void btnSyncSIPToGoal_Click(object sender, EventArgs e)
        //{
        //    superAdminOpsBo.SyncSIPtoGoal();
        //}

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            superAdminOpsBo.DeleteAllDuplicates();
            BindDuplicateGrid();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Record has been deleted successfully.');", true);
        }

        //protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (IsPostBack)
        //    {
        //        tblMessage.Visible = false;
        //    }
        //}
        //private void GetPageCountNAV()
        //{
        //    string upperlimit = null;
        //    int rowCount = 0;
        //    int ratio = 0;
        //    string lowerlimit = null;
        //    string PageRecords = null;
        //    try
        //    {
        //        if (hdnRecordCount.Value.ToString() != "")
        //            rowCount = Convert.ToInt32(hdnRecordCount.Value);
        //        if (rowCount > 0)
        //        {
        //            ratio = rowCount / 10;
        //            myPagerNAV.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
        //            myPagerNAV.Set_Page(myPagerNAV.CurrentPage, myPagerNAV.PageCount);
        //            if (((myPagerNAV.CurrentPage - 1) * 10) != 0)
        //                lowerlimit = (((myPagerNAV.CurrentPage - 1) * 10) + 1).ToString();
        //            else
        //                lowerlimit = "1";
        //            upperlimit = (myPagerNAV.CurrentPage * 10).ToString();
        //            if (myPagerNAV.CurrentPage == myPagerNAV.PageCount)
        //                upperlimit = hdnRecordCount.Value;
        //            PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
        //            lblNAVCount.Text = PageRecords;
        //            hdnCurrentPage.Value = myPagerNAV.CurrentPage.ToString();
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:GetPageCount()");

        //        object[] objects = new object[5];
        //        objects[0] = upperlimit;
        //        objects[1] = rowCount;
        //        objects[2] = ratio;
        //        objects[3] = lowerlimit;
        //        objects[4] = PageRecords;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAction.SelectedIndex != 0)
            {
                if (ddlAction.SelectedValue == "NAVChange")
                {

                    trRadioDatePeriod.Visible = false;
                    trDate.Visible = true;
                    // txtPercentage.Text = "20";
                    txtDate.SelectedDate = DateTime.Now.AddDays(-1);
                    trRange.Visible = false;
                    trPeriod.Visible = false;

                }
                else
                {
                    trRadioDatePeriod.Visible = true;
                    if (rbtnPickDate.Checked == true )
                        trRange.Visible = true;
                    else if (rbtnPickPeriod.Checked == true)
                        trPeriod.Visible =true;  
                    
                    trDate.Visible = false;

                }
            }
        }

        protected void ddlMonitorfr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMonitorfr.SelectedIndex != 0)
            {
                if (ddlMonitorfr.SelectedValue == "EQ")
                {
                    string asset = ddlMonitorfr.SelectedValue;
                    // trdd1.Visible = false;
                    trRadioDatePeriod.Visible = true;
                    trRange.Visible = true;
                    trPeriod.Visible = false;
                    trDate.Visible = false;
                    //trequity.Visible = true;
                    ddlAction.Items[0].Enabled = true;
                    ddlAction.Items[1].Enabled = true;
                    ddlAction.Items[2].Enabled = false;
                    ddlAction.Items[3].Enabled = false;

                    ddlAction.Items[4].Enabled = false;
                }
                else
                {
                    ddlAction.Items[0].Enabled = true;
                    ddlAction.Items[1].Enabled = true;
                    ddlAction.Items[2].Enabled = true;
                    ddlAction.Items[3].Enabled = true;

                    ddlAction.Items[4].Enabled = true;
                    string asset = ddlMonitorfr.SelectedValue;
                    // trdd1.Visible = true;
                    trRadioDatePeriod.Visible = true;
                    trRange.Visible = true;
                    trPeriod.Visible = false;
                    trDate.Visible = false;
                    // trequity.Visible = false;
                }
            }
        }
        protected void gvAumMis_Init(object sender, System.EventArgs e)
        {
            GridFilterMenu menu = gvAumMis.FilterMenu;
            int i = 0;
            while (i < menu.Items.Count)
            {
                if (menu.Items[i].Text == "NoFilter" || menu.Items[i].Text == "Contains" || menu.Items[i].Text == "DoesNotContain" || menu.Items[i].Text == "StartsWith" || menu.Items[i].Text == "EndsWith")
                {
                    i++;
                }
                else
                {
                    menu.Items.RemoveAt(i);
                }
            }
        }
        //protected void ddEquity_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //  if (ddlEquity.SelectedIndex != 0)
        //  {
        //      if (ddlEquity.SelectedValue == "AumMis")
        //      {
        //          trdd1.Visible = false; 
        //      }
        //}

        protected void gvAumMis_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dsAumMis = new DataSet();
            gvAumMis.Visible = true;
            trbtnDelete.Visible = true;
            gvDuplicateCheck.Visible = false;
            gvMFRejectedDetails.Visible = false;
            trExportFilteredAumData.Visible = true;
            gvNavChange.Visible = false;
            dsAumMis = (DataSet)Cache["AUMList"];
            gvAumMis.DataSource = dsAumMis;
        }
        protected void gvMFRejectedDetails_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dsRejectedRecords = new DataSet();
            gvAumMis.Visible = false;
            trbtnDelete.Visible = false;
            gvDuplicateCheck.Visible = false;
            trExportFilteredRejData.Visible = true;
            gvMFRejectedDetails.Visible = true;
            gvNavChange.Visible = false;
            dsRejectedRecords = (DataSet)Cache["RejectedRecordsList"];
            gvMFRejectedDetails.DataSource = dsRejectedRecords;
        }
        protected void gvDuplicateCheck_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dsduplicatecheck = new DataSet();
            gvAumMis.Visible = false;
            trbtnDelete.Visible = true;
            gvDuplicateCheck.Visible = true;
            trExportFilteredDupData.Visible = true;
            gvMFRejectedDetails.Visible = false;
            gvNavChange.Visible = false;
            dsduplicatecheck = (DataSet)Cache["duplicatecheckList"];
            gvDuplicateCheck.DataSource = dsduplicatecheck;
        }
        protected void gvNavChange_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            gvAumMis.Visible = false;
            trbtnDelete.Visible = false;
            gvDuplicateCheck.Visible = false;
            gvMFRejectedDetails.Visible = false;
            gvNavChange.Visible = true;
            trExportFilteredNavData.Visible = true;
            DataSet dsGetNAV = new DataSet();
            dsGetNAV = (DataSet)Cache["dsGetNAVList"];
            gvNavChange.DataSource = dsGetNAV;
        }
        protected void btnExportFilteredNavData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvNavChange.ExportSettings.OpenInNewWindow = true;
            gvNavChange.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvNavChange.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvNavChange.MasterTableView.ExportToExcel();
        }
        protected void btnExportFilteredDupData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvDuplicateCheck.ExportSettings.OpenInNewWindow = true;
            gvDuplicateCheck.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvDuplicateCheck.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvDuplicateCheck.MasterTableView.ExportToCSV();
        }
        protected void btnExportFilteredRejData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvMFRejectedDetails.ExportSettings.OpenInNewWindow = true;
            gvMFRejectedDetails.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvMFRejectedDetails.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvMFRejectedDetails.MasterTableView.ExportToExcel();
        }
        protected void btnExportFilteredAumData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvAumMis.ExportSettings.OpenInNewWindow = true;
            gvAumMis.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvAumMis.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvAumMis.MasterTableView.ExportToExcel();
        }
    }
}