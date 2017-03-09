using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Collections;
using WealthERP.Base;
using VoUploads;
using VoCustomerProfiling;
using VoUser;
using BoUploads;
using BoCustomerProfiling;
using BoUser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;

namespace WealthERP.Uploads
{
    public partial class RejectedMFAutoSIPTransactions : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        MFTransactionVo mfTransactionVo = new MFTransactionVo();
        UserVo userVo = new UserVo();
        UserVo rmUserVo = new UserVo();
        UserVo tempUserVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        UploadCommonBo uploadsCommonBo = new UploadCommonBo();

        DateTime fromDate;
        DateTime toDate;
        DataSet dsRejectedSIP = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            adviserVo = (AdvisorVo)Session["advisorVo"];
            pnlAutoSIP.Visible = false;

            if (!Page.IsPostBack)
            {
                
                DataSet dsRejectedSIPDetails = new DataSet();
                if (Cache["RejectedAutoSIPDetails" + adviserVo.advisorId.ToString()] != null)
                    Cache["RejectedAutoSIPDetails" + adviserVo.advisorId.ToString()] = dsRejectedSIPDetails;

                DateTime fromDate = DateTime.Now.AddMonths(-1);
                txtFromSIP.SelectedDate = fromDate.Date;
                txtToSIP.SelectedDate = DateTime.Now;

                //BindRejectedSIPGrid();
            }
        }

        private void BindRejectedSIPGrid()
        {
           if (txtFromSIP.SelectedDate != null)
                    fromDate = DateTime.Parse(txtFromSIP.SelectedDate.ToString());
                if (txtToSIP.SelectedDate != null)
                    toDate = DateTime.Parse(txtToSIP.SelectedDate.ToString());                
          
            DataSet dsRejectedSIP = new DataSet();
            Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
            Dictionary<string, string> genDictRejectReason = new Dictionary<string, string>();
            dsRejectedSIP = uploadsCommonBo.GetRejectedAutoSIPRecords(adviserVo.advisorId,fromDate, toDate);

            //if (dsRejectedSIP.Tables[0].Rows.Count > 0)
            //{
                if (Cache["RejectedAutoSIPDetails" + adviserVo.advisorId.ToString()] == null)
                {
                    Cache.Insert("RejectedAutoSIPDetails" + adviserVo.advisorId.ToString(), dsRejectedSIP);
                }
                else
                {
                    Cache.Remove("RejectedAutoSIPDetails" + adviserVo.advisorId.ToString());
                    Cache.Insert("RejectedAutoSIPDetails" + adviserVo.advisorId.ToString(), dsRejectedSIP);
                }
                gvAutoSIPReject.CurrentPageIndex = 0;
                gvAutoSIPReject.DataSource = dsRejectedSIP.Tables[0];
                gvAutoSIPReject.DataBind();
                gvAutoSIPReject.Visible = true;
                if (dsRejectedSIP.Tables[0].Rows.Count > 0)
                {
                    btnExport.Visible = true;
                    pnlAutoSIP.Visible = true;
                }

            //}
            //else
            //{
            //    gvAutoSIPReject.CurrentPageIndex = 0;               
            //    gvAutoSIPReject.DataSource = null;
            //    gvAutoSIPReject.DataBind();
            //    //gvAutoSIPReject.Visible = false;
                
            //}
        }

        protected void btnViewSIP_Click(object sender, EventArgs e)
        {
            // if (!string.IsNullOrEmpty(txtFromSIP.SelectedDate.ToString()))
            if (txtFromSIP.SelectedDate != null)
                fromDate = DateTime.Parse(txtFromSIP.SelectedDate.ToString());
            if (txtToSIP.SelectedDate != null)
                toDate = DateTime.Parse(txtToSIP.SelectedDate.ToString());

            BindRejectedSIPGrid();            
        }



        protected void gvAutoSIPReject_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && e.Item.ItemIndex == -1)
            {
                DataTable dtgvWERPSIP = new DataTable();
                GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                RadComboBox RadComboBoxRR = (RadComboBox)filterItem.FindControl("RadComboBoxRR");

                dsRejectedSIP = (DataSet)Cache["RejectedAutoSIPDetails" + adviserVo.advisorId.ToString()];
                DataTable dtSIP = new DataTable();
                dtgvWERPSIP = dsRejectedSIP.Tables[0];
                dtSIP.Columns.Add("WRR_RejectReasonDescription");
                dtSIP.Columns.Add("WRR_RejectReasonCode");
                DataRow drSIP;
                foreach (DataRow dr in dtgvWERPSIP.Rows)
                {
                    drSIP = dtSIP.NewRow();
                    drSIP["WRR_RejectReasonDescription"] = dr["WRR_RejectReasonDescription"].ToString();
                    drSIP["WRR_RejectReasonCode"] = int.Parse(dr["WRR_RejectReasonCode"].ToString());
                    dtSIP.Rows.Add(drSIP);
                }
                DataView view = new DataView(dtgvWERPSIP);
                DataTable distinctValues = view.ToTable(true, "WRR_RejectReasonDescription", "WRR_RejectReasonCode");
                RadComboBoxRR.DataSource = distinctValues;
                RadComboBoxRR.DataValueField = dtSIP.Columns["WRR_RejectReasonCode"].ToString();
                RadComboBoxRR.DataTextField = dtSIP.Columns["WRR_RejectReasonDescription"].ToString();
                RadComboBoxRR.DataBind();               
            }
        }
        protected void ddlRejectReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadComboBox dropdown = sender as RadComboBox;
            ViewState["WRR_RejectReasonCode"] = dropdown.SelectedValue;
            if (ViewState["WRR_RejectReasonCode"] != "")
            {
                GridColumn column = gvAutoSIPReject.MasterTableView.GetColumnSafe("WRR_RejectReasonCode");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvAutoSIPReject.CurrentPageIndex = 0;
                gvAutoSIPReject.MasterTableView.Rebind();
            }
            else
            {
                GridColumn column = gvAutoSIPReject.MasterTableView.GetColumnSafe("WRR_RejectReasonCode");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvAutoSIPReject.CurrentPageIndex = 0;
                gvAutoSIPReject.MasterTableView.Rebind();
            }
        }

        protected void rcbContinents1_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            if (ViewState["WRR_RejectReasonCode"] != null)
            {
                Combo.SelectedValue = ViewState["WRR_RejectReasonCode"].ToString();
            }
        }
        protected void gvSIPReject_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string rcbType = string.Empty;
            DataSet dsSIP = new DataSet();
            DataTable dtrr = new DataTable();
            dsSIP = (DataSet)Cache["RejectedAutoSIPDetails" + adviserVo.advisorId.ToString()];
            if (dsSIP != null)
            {
                dtrr = dsSIP.Tables[0];
                if (ViewState["WRR_RejectReasonCode"] != null)
                    rcbType = ViewState["WRR_RejectReasonCode"].ToString();
                
                if (!string.IsNullOrEmpty(rcbType))
                {
                    DataView dvStaffList = new DataView(dtrr, "WRR_RejectReasonCode = '" + rcbType + "'", "CMFSCS_InvName,CMFA_FolioNum,PASP_SchemePlanName", DataViewRowState.CurrentRows);
                    gvAutoSIPReject.DataSource = dvStaffList.ToTable();
                }
                else
                {
                    gvAutoSIPReject.DataSource = dtrr;                   
                }
                btnExport.Visible = true;
                pnlAutoSIP.Visible = true;
            }
        }

        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            gvAutoSIPReject.ExportSettings.OpenInNewWindow = true;
            gvAutoSIPReject.ExportSettings.IgnorePaging = true;
            gvAutoSIPReject.ExportSettings.HideStructureColumns = true;
            gvAutoSIPReject.ExportSettings.ExportOnlyData = true;
            gvAutoSIPReject.ExportSettings.FileName = "Rejected Auto SIP Details";
            gvAutoSIPReject.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvAutoSIPReject.MasterTableView.ExportToExcel();
        }
    }
}