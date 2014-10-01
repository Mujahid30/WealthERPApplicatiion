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

                fromdate = DateTime.Now.AddMonths(-1);
                txtFromDate.SelectedDate = fromdate;
                txtToDate.SelectedDate = DateTime.Now;

                //BindAdviserIssueAllotmentList();
                // BindDropDownListIssuer();
                //BindIssuerId();
            }

        }
        protected void BindRTAInitialReport()
        {
            try
            {
                DataTable dtBindRTAInitialReport;
                if (txtFromDate.SelectedDate != null)
                    fromdate = DateTime.Parse(txtFromDate.SelectedDate.ToString());
                if (txtToDate.SelectedDate != null)
                    todate = DateTime.Parse(txtToDate.SelectedDate.ToString());
                dtBindRTAInitialReport = onlineOrderBackOffice.GetRTAInitialReport(ddlType.SelectedValue.ToString(), fromdate, todate);
                if (dtBindRTAInitialReport.Rows.Count > 0)
                {
                    if (Cache["RTAInitialReport" + advisorVo.advisorId] == null)
                    {
                        Cache.Insert("RTAInitialReport" + advisorVo.advisorId, dtBindRTAInitialReport);
                    }
                    else
                    {
                        Cache.Remove("RTAInitialReport" + advisorVo.advisorId);
                        Cache.Insert("RTAInitialReport" + advisorVo.advisorId, dtBindRTAInitialReport);
                    }
                    gvOrderReport.DataSource = dtBindRTAInitialReport;
                    gvOrderReport.DataBind();
                    pnlOrderReport.Visible = true;
                }
                else
                {
                    gvOrderReport.DataSource = dtBindRTAInitialReport;
                    gvOrderReport.DataBind();
                    pnlOrderReport.Visible = true;
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
        protected void gvOrderReport_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet dtBindRTAInitialReport = new DataSet();
            dtBindRTAInitialReport = (DataSet)Cache["RTAInitialReport" + advisorVo.advisorId];

            if (dtBindRTAInitialReport != null)
            {
                gvOrderReport.DataSource = dtBindRTAInitialReport;
            }
        }
        protected void BindAdviserIssueAllotmentList()
        {
            try
            {
                DataSet dsGetAdviserissueallotmentList = new DataSet();


                // if(ddlIssuer.SelectedValue!=null)


                //DataTable dtGetAdviserissueallotmentList = new DataTable();
                dsGetAdviserissueallotmentList = OnlineNCDBackOfficeBo.GetAdviserissueallotmentList(advisorVo.advisorId, int.Parse(ddlIssuer.SelectedValue.ToString()), ddlType.SelectedValue.ToString(), fromdate, todate);
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
            OnlineBondOrderBo OnlineBondBo = new OnlineBondOrderBo();
            DataSet dsStructureRules = OnlineBondBo.GetLiveBondTransactionList(advisorVo.advisorId);
            ddlIssuer.DataTextField = dsStructureRules.Tables[0].Columns["PFIIM_IssuerId"].ToString();
            ddlIssuer.DataValueField = dsStructureRules.Tables[0].Columns["AIM_IssueId"].ToString();
            if (dsStructureRules.Tables[0].Rows.Count > 0)
            {
                ddlIssuer.DataSource = dsStructureRules.Tables[0];
                ddlIssuer.DataBind();
            }
            ddlIssuer.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void Go_OnClick(object sender, EventArgs e)
        {
            BindRTAInitialReport();
            //BindAdviserIssueAllotmentList();
            imgexportButton.Visible = true;

        }
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {

            gvOrderReport.ExportSettings.OpenInNewWindow = true;
            gvOrderReport.ExportSettings.IgnorePaging = true;
            gvOrderReport.ExportSettings.HideStructureColumns = true;
            gvOrderReport.ExportSettings.ExportOnlyData = true;
            gvOrderReport.ExportSettings.FileName = "Initial Order Report AMC/RTA Wise";
            gvOrderReport.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvOrderReport.MasterTableView.ExportToExcel();
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