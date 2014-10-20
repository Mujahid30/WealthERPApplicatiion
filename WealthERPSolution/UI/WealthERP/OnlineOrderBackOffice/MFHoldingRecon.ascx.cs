using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.ExceptionManagement;


using BoCommon;
using BoOnlineOrderManagement;
using Telerik.Web.UI;
using VoUser;
using VoOnlineOrderManagemnet;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class MFHoldingRecon : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        OnlineOrderMISBo OnlineOrderMISBo = new OnlineOrderMISBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            AdvisorVo adviserVo = new AdvisorVo();
           
            BindRequestId();
            if (!Page.IsPostBack)
            {
                Label1.Visible = false;
                txtTo.Visible = false;
                btnSynch.Visible = false;
                 
            }

        }
        protected void BindRequestId()
        {
            DataTable dtGetIssueName = new DataTable();

            dtGetIssueName = OnlineOrderMISBo.GetMFHolding();
            ddlIssue.DataSource = dtGetIssueName;
            ddlIssue.DataValueField = dtGetIssueName.Columns["WR_RequestId"].ToString();
            ddlIssue.DataTextField = dtGetIssueName.Columns["WRD_InputParameterValue"].ToString();
            ddlIssue.DataBind();
            ddlIssue.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            try
            {  DataTable dtMFHoldingRecon = new DataTable();
                dtMFHoldingRecon = OnlineOrderMISBo.GetMFHoldingRecon(int.Parse(ddlIssue.SelectedValue));
                if (dtMFHoldingRecon.Rows.Count > 0)
                {
                    BindMFHoldingRecon();
                    Label1.Visible = true;
                    txtTo.Visible = true;
                    btnSynch.Visible = true;
                    trNoRecords.Visible = false;
                    divNoRecords.Visible = false;
                }
                else
                {
                    BindMFHoldingRecon();
                     Label1.Visible = false;
                     txtTo.Visible = false;
                     btnSynch.Visible = false;
                     trNoRecords.Visible = true;
                     divNoRecords.Visible = true;
                    
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
                FunctionInfo.Add("Method", "MFHoldingRecon.ascx.cs:btngo_Click()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        protected void btnSync_OnClick(object sender, EventArgs e)
        {
            BindMFHoldingReconAfterSync();
        }
        protected void BindMFHoldingRecon()
        {
            try
            {

                DataTable dtMFHoldingRecon = new DataTable();
                dtMFHoldingRecon = OnlineOrderMISBo.GetMFHoldingRecon(int.Parse(ddlIssue.SelectedValue));
                if (dtMFHoldingRecon.Rows.Count > 0)
                {
                    if (Cache["MFHoldingMIS" + userVo.UserId] == null)
                    {
                        Cache.Insert("MFHoldingMIS" + userVo.UserId, dtMFHoldingRecon);
                    }
                    else
                    {
                        Cache.Remove("MFHoldingMIS" + userVo.UserId);
                        Cache.Insert("MFHoldingMIS" + userVo.UserId, dtMFHoldingRecon);
                    }
                    gvMFHoldinfRecon.DataSource = dtMFHoldingRecon;
                    gvMFHoldinfRecon.DataBind();
                    MFHoldingRecons.Visible = true;
                    pnlMFHoldingRecons.Visible = true;

                    gvMFHoldinfRecon.MasterTableView.GetColumn("SchemePlanName").Display = false;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("Diff").Display = false;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemUnits").Display = false;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemNAV").Display = false;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemNAVDate").Display = false;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemAUM").Display = false;

                    //this.gvMFHoldinfRecon.ClientSettings.Scrolling.FrozenColumnsCount = 1;
                    //this.gvMFHoldinfRecon.ClientSettings.Scrolling.UseStaticHeaders = true;
                    //this.gvMFHoldinfRecon.ClientSettings.Scrolling.SaveScrollPosition = true;
                }
                else
                {
                    gvMFHoldinfRecon.DataSource = dtMFHoldingRecon;
                    gvMFHoldinfRecon.DataBind();
                    MFHoldingRecons.Visible = true;
                    pnlMFHoldingRecons.Visible = true;

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
                FunctionInfo.Add("Method", "MFHoldingRecon.ascx.cs:MFHoldingRecon()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void BindMFHoldingReconAfterSync()
        {
            try
            {

                DataTable dtMFHoldingReconSync = new DataTable();
                dtMFHoldingReconSync = OnlineOrderMISBo.GetMFHoldingReconAfterSync(int.Parse(ddlIssue.SelectedValue), Convert.ToDateTime(txtTo.SelectedDate));
                if (dtMFHoldingReconSync.Rows.Count > 0)
                {
                    if (Cache["MFHoldingMIS" + userVo.UserId] == null)
                    {
                        Cache.Insert("MFHoldingMIS" + userVo.UserId, dtMFHoldingReconSync);
                    }
                    else
                    {
                        Cache.Remove("MFHoldingMIS" + userVo.UserId);
                        Cache.Insert("MFHoldingMIS" + userVo.UserId, dtMFHoldingReconSync);
                    }
                    gvMFHoldinfRecon.DataSource = dtMFHoldingReconSync;
                    gvMFHoldinfRecon.DataBind();
                    MFHoldingRecons.Visible = true;
                    pnlMFHoldingRecons.Visible = true;

                    gvMFHoldinfRecon.MasterTableView.GetColumn("SchemePlanName").Display = true;

                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemUnits").Display = true;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemNAV").Display = true;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemNAVDate").Display = true;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("SystemAUM").Display = true;
                    gvMFHoldinfRecon.MasterTableView.GetColumn("Diff").Display = true;
              
                        //foreach (GridDataItem item in gvMFHoldinfRecon.Items)
                        //{
                        //    item["SchemePlanName"]. = System.Drawing.Color.Yellow;
                        //    item["SystemUnits"].BackColor = System.Drawing.Color.Yellow;
                        //    item["SystemNAV"].BackColor = System.Drawing.Color.Yellow;
                        //    item["SystemNAVDate"].BackColor = System.Drawing.Color.Yellow;
                        //    item["SystemAUM"].BackColor = System.Drawing.Color.Yellow;
                        //    item["Diff"].BackColor = System.Drawing.Color.Yellow;
                        //}
                }
                else
                {
                    gvMFHoldinfRecon.DataSource = dtMFHoldingReconSync;
                    gvMFHoldinfRecon.DataBind();
                    MFHoldingRecons.Visible = true;
                    pnlMFHoldingRecons.Visible = true;

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
                FunctionInfo.Add("Method", "MFHoldingRecon.ascx.cs:MFHoldingRecon()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void gvMFHoldinfRecon_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            DataTable dtMFHoldingRecon = new DataTable();
            dtMFHoldingRecon = (DataTable)Cache["MFHoldingMIS" + userVo.UserId];
            if (dtMFHoldingRecon != null)
            {
                gvMFHoldinfRecon.DataSource = dtMFHoldingRecon;
            }
        }
        protected void btnExportData_OnClick(object sender, EventArgs e)
        {
            gvMFHoldinfRecon.ExportSettings.OpenInNewWindow = true;
            gvMFHoldinfRecon.ExportSettings.IgnorePaging = true;
            gvMFHoldinfRecon.ExportSettings.HideStructureColumns = true;
            gvMFHoldinfRecon.ExportSettings.ExportOnlyData = true;
            gvMFHoldinfRecon.ExportSettings.FileName = "MF Holding Recon";
            gvMFHoldinfRecon.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvMFHoldinfRecon.MasterTableView.ExportToExcel();
        }
    }
}