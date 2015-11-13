using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using VoUser;
using System.Data;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using Telerik.Web.UI;


namespace WealthERP.OnlineOrderBackOffice
{
    public partial class HoldingIssueAllotment : System.Web.UI.UserControl
    {
        OnlineNCDBackOfficeBo OnlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        OnlineOrderBackOfficeBo onlineOrderBackOffice = new OnlineOrderBackOfficeBo();
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo = new AdvisorVo();
        DateTime fromdate;
        DateTime todate;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            // BindRTAInitialReport();
            if (!IsPostBack)
            {

                fromdate = DateTime.Now.AddDays(-1);
                txtFromDate.SelectedDate = fromdate;
                txtToDate.SelectedDate = DateTime.Now;
                BindAMC();
                //BindAdviserIssueAllotmentList();
                // BindDropDownListIssuer();
                //BindIssuerId();
            }

        }
        protected void BindAMC()
        {
            try
            {
                ddlAMC.Items.Clear();
                DataSet ds = new DataSet();
                DataTable dtAmc = new DataTable();
                dtAmc = commonLookupBo.GetProdAmc();
                if (dtAmc.Rows.Count > 0)
                {
                    ddlAMC.DataSource = dtAmc;
                    ddlAMC.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                    ddlAMC.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                    ddlAMC.DataBind();
                    //BindFolioNumber(int.Parse(ddlAmc.SelectedValue));

                }
                ddlAMC.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineSchemeSetUp.ascx:BindAmcDropDown()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void BindRTAInitialReport()
        {
            try
            {
                pnlOrderReport.Visible = false;
                pnlFATCA.Visible = false;
                DataTable dtBindRTAInitialReport;
                if (txtFromDate.SelectedDate != null)
                    fromdate = DateTime.Parse(txtFromDate.SelectedDate.ToString());
                if (txtToDate.SelectedDate != null)
                    todate = DateTime.Parse(txtToDate.SelectedDate.ToString());
               
                if (!Boolean.Parse(ddlOrderType.SelectedValue))
                {
                    dtBindRTAInitialReport = onlineOrderBackOffice.GetRTAInitialReport(ddlType.SelectedValue.ToString(), fromdate, todate, Boolean.Parse(ddlOrderType.SelectedValue), int.Parse(ddlAMC.SelectedValue));
                    if (Cache["RTAInitialReport" + advisorVo.advisorId] != null)
                    {
                        Cache.Remove("RTAInitialReport" + advisorVo.advisorId);
                    }
                    Cache.Insert("RTAInitialReport" + advisorVo.advisorId, dtBindRTAInitialReport);

                    gvOrderReport.DataSource = dtBindRTAInitialReport;
                    gvOrderReport.DataBind();
                    pnlOrderReport.Visible = true;

                }
                else
                {
                    dtBindRTAInitialReport = onlineOrderBackOffice.GetRTAInitialReport(ddlType.SelectedValue.ToString(), fromdate, todate, Boolean.Parse(ddlOrderType.SelectedValue), int.Parse(ddlAMC.SelectedValue));
                    if (Cache["FATCAReport" + advisorVo.advisorId] != null)
                    {
                        Cache.Remove("FATCAReport" + advisorVo.advisorId);
                    }
                    Cache.Insert("FATCAReport" + advisorVo.advisorId, dtBindRTAInitialReport);

                    rgFATCA.DataSource = dtBindRTAInitialReport;
                    rgFATCA.DataBind();
                    pnlFATCA.Visible = true;
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
                FunctionInfo.Add("Method", "OnlineClientAccess.ascx.cs:BindRTAInitialReport()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }
        protected void ddlOrderType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            tdlblAMC.Visible = false;
            tdddlAMC.Visible = false;
            tdddlType.Visible = false;
            tdlblType.Visible = false;
            if (Boolean.Parse(ddlOrderType.SelectedValue))
            {
                tdlblAMC.Visible = true;
                tdddlAMC.Visible = true;
            }
            else
            {
                tdddlType.Visible =true;
                tdlblType.Visible = true;
            }
        }
        protected void gvOrderReport_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtBindRTAInitialReport = new DataTable();
            dtBindRTAInitialReport = (DataTable)Cache["RTAInitialReport" + advisorVo.advisorId];

            if (dtBindRTAInitialReport != null)
            {
                gvOrderReport.DataSource = dtBindRTAInitialReport;
            }
        }
        protected void rgFATCA_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtFATCAReport = new DataTable();
            dtFATCAReport = (DataTable)Cache["FATCAReport" + advisorVo.advisorId];

            if (dtFATCAReport != null)
            {
                rgFATCA.DataSource = dtFATCAReport;
            }
        }
        protected void BindAdviserIssueAllotmentList()
        {
            try
            {
                DataSet dsGetAdviserissueallotmentList = new DataSet();
                dsGetAdviserissueallotmentList = OnlineNCDBackOfficeBo.GetAdviserissueallotmentList(advisorVo.advisorId, 1, ddlType.SelectedValue.ToString(), fromdate, todate);
                if (dsGetAdviserissueallotmentList.Tables[0].Rows.Count > 0)
                {
                    if (Cache["AdviserIssueList" + advisorVo.advisorId] == null)
                    {
                        Cache.Insert("AdviserIssueList" + advisorVo.advisorId, dsGetAdviserissueallotmentList);
                    }
                    else
                    {
                        Cache.Remove("AdviserIssueList" + advisorVo.advisorId);
                        Cache.Insert("AdviserIssueList" + advisorVo.advisorId, dsGetAdviserissueallotmentList);
                    }
                    gvAdviserIssueList.DataSource = dsGetAdviserissueallotmentList;
                    gvAdviserIssueList.DataBind();
                    AdviserIssueList.Visible = true;
                }
                else
                {
                    gvAdviserIssueList.DataSource = dsGetAdviserissueallotmentList;
                    gvAdviserIssueList.DataBind();
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
                FunctionInfo.Add("Method", "OnlineClientAccess.ascx.cs:BindAdviserClientKYCStatusList()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        //protected void BindIssuerId()
        //{
        //    OnlineBondOrderBo OnlineBondOrderBo = new OnlineBondOrderBo();
        //    try
        //    {

        //        DataTable dtissuerid;
        //        dtissuerid = OnlineBondOrderBo.GetIssuerid(adviserid);
        //        if (dtissuerid.Rows.Count > 0)
        //        {
        //            ddlIssuer.DataSource = dtissuerid;
        //            ddlIssuer.DataValueField = dtissuerid.Columns["PI_IssuerId"].ToString();
        //            ddlIssuer.DataBind();
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

        //        FunctionInfo.Add("Method", "OnlineSchemeSetUp.ascx:BindCategoryDropDown()");

        //        object[] objects = new object[3];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}


        protected void BindDropDownListIssuer()
        {
            //OnlineBondOrderBo OnlineBondBo = new OnlineBondOrderBo();
            //DataSet dsStructureRules = OnlineBondBo.GetLiveBondTransactionList(advisorVo.advisorId);
            //ddlIssuer.DataTextField = dsStructureRules.Tables[0].Columns["PFIIM_IssuerId"].ToString();
            //ddlIssuer.DataValueField = dsStructureRules.Tables[0].Columns["AIM_IssueId"].ToString();
            //if (dsStructureRules.Tables[0].Rows.Count > 0)
            //{
            //    ddlIssuer.DataSource = dsStructureRules.Tables[0];
            //    ddlIssuer.DataBind();
            //}
            //ddlIssuer.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void Go_OnClick(object sender, EventArgs e)
        {
            BindRTAInitialReport();
            //BindAdviserIssueAllotmentList();
            imgexportButton.Visible = true;

        }
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!Boolean.Parse(ddlOrderType.SelectedValue))
            {
                gvOrderReport.ExportSettings.OpenInNewWindow = true;
                gvOrderReport.ExportSettings.IgnorePaging = true;
                gvOrderReport.ExportSettings.HideStructureColumns = true;
                gvOrderReport.ExportSettings.ExportOnlyData = true;
                gvOrderReport.ExportSettings.FileName = "Initial Order Report AMC/RTA Wise";
                gvOrderReport.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gvOrderReport.MasterTableView.ExportToExcel();
            }
            else
            {
                if (txtFromDate.SelectedDate != null)
                    fromdate = DateTime.Parse(txtFromDate.SelectedDate.ToString());
                if (txtToDate.SelectedDate != null)
                    todate = DateTime.Parse(txtToDate.SelectedDate.ToString());
                rgFATCA.ExportSettings.OpenInNewWindow = true;
                rgFATCA.ExportSettings.IgnorePaging = true;
                rgFATCA.ExportSettings.HideStructureColumns = true;
                rgFATCA.ExportSettings.ExportOnlyData = true;
                rgFATCA.ExportSettings.FileName = "FATCA Report For " + ddlAMC.SelectedItem.Text + " " + fromdate.ToShortDateString() + "-" + todate.ToShortDateString();
                rgFATCA.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                rgFATCA.MasterTableView.ExportToExcel();
            }
        }
        protected void gvAdviserIssueList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet dsGetAdviserissueallotmentList = new DataSet();
            dsGetAdviserissueallotmentList = (DataSet)Cache["AdviserIssueList" + advisorVo.advisorId];
            if (dsGetAdviserissueallotmentList != null)
            {
                gvAdviserIssueList.DataSource = dsGetAdviserissueallotmentList;
            }
        }
    }
}