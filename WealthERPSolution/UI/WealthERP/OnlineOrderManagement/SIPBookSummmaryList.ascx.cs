using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using BoCommon;
using VoUser;
using WealthERP.Base;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using VoCustomerProfiling;
using BoCustomerProfiling;
using BoOnlineOrderManagement;
using VoOps;
using BoOps;

namespace WealthERP.OnlineOrderManagement
{
    public partial class SIPBookSummmaryList : System.Web.UI.UserControl
    {

        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        AdvisorVo advisorVo;
        CustomerVo customerVO = new CustomerVo();
        MFOrderBo mforderBo = new MFOrderBo();
        MFOrderVo mforderVo = new MFOrderVo();
        OrderVo orderVo = new OrderVo();
        string userType;
        int customerId = 0;       
        DateTime fromDate;
        DateTime toDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVO = (CustomerVo)Session["customerVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();
            customerId = customerVO.CustomerId;
            BindAmc();
            BindOrderStatus();
            if (!Page.IsPostBack)
            {
                fromDate = DateTime.Now.AddMonths(-1);
                txtFrom.SelectedDate = fromDate.Date;
                txtTo.SelectedDate = DateTime.Now;
            }
        }

        /// <summary>
        /// get AMC
        /// </summary>
        protected void BindAmc()
        {
            ddlAMCCode.Items.Clear();
            DataSet ds = new DataSet();
            DataTable dtAmc = new DataTable();
            ds = OnlineMFOrderBo.GetTransAllAmcDetails(customerId);
            dtAmc = ds.Tables[0];
            if (dtAmc.Rows.Count > 0)
            {
                ddlAMCCode.DataSource = dtAmc;
                ddlAMCCode.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAMCCode.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAMCCode.DataBind();
                //BindFolioNumber(int.Parse(ddlAmc.SelectedValue));

            }
            ddlAMCCode.Items.Insert(0, new ListItem("All", "0"));
        }

        /// <summary>
        /// Get Bind Orderstatus
        /// </summary>
        private void BindOrderStatus()
        {
            ddlOrderstatus.Items.Clear();
            DataSet dsOrderStatus;
            DataTable dtOrderStatus;
            dsOrderStatus = OnlineMFOrderBo.GetOrderStatus();
            dtOrderStatus = dsOrderStatus.Tables[0];
            if (dtOrderStatus.Rows.Count > 0)
            {
                ddlOrderstatus.DataSource = dtOrderStatus;
                ddlOrderstatus.DataTextField = dtOrderStatus.Columns["WOS_OrderStep"].ToString();
                ddlOrderstatus.DataValueField = dtOrderStatus.Columns["WOS_OrderStepCode"].ToString();
                ddlOrderstatus.DataBind();
            }
            ddlOrderstatus.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void btnViewOrder_Click(object sender, EventArgs e)
        {
            SetParameter();
            BindSIPSummaryBook();
        }

        /// <summary>
        /// Get Folio Account for Customer
        /// </summary>
        private void BindFolioAccount()
      {
        //    ddlAccount.Items.Clear();
        //    DataSet dsFolioAccount;
        //    DataTable dtFolioAccount;
        //    dsFolioAccount = OnlineMFOrderBo.GetFolioAccount(customerId);
        //    dtFolioAccount = dsFolioAccount.Tables[0];
        //    if (dtFolioAccount.Rows.Count > 0)
        //    {
        //        ddlAccount.DataSource = dsFolioAccount.Tables[0];
        //        ddlAccount.DataTextField = dtFolioAccount.Columns["CMFA_FolioNum"].ToString();
        //        ddlAccount.DataValueField = dtFolioAccount.Columns["CMFA_AccountId"].ToString();
        //        ddlAccount.DataBind();
        //    }
        //    ddlAccount.Items.Insert(0, new ListItem("All", "0"));
        }
        /// <summary>
        /// Get Order Book MIS
        /// </summary>
        protected void BindSIPSummaryBook()
        {
            DataSet dsSIPBookMIS = new DataSet();
            DataTable dtSIPBookMIS = new DataTable();
            if (txtFrom.SelectedDate != null)
                fromDate = DateTime.Parse(txtFrom.SelectedDate.ToString());
            if (txtTo.SelectedDate != null)
                toDate = DateTime.Parse(txtTo.SelectedDate.ToString());
            dsSIPBookMIS = OnlineMFOrderBo.GetSIPSummaryBookMIS(customerId,int.Parse(hdnAmc.Value),hdnOrderStatus.Value, fromDate, toDate);
            dtSIPBookMIS = dsSIPBookMIS.Tables[0];
            if (dtSIPBookMIS.Rows.Count > 0)
            {
                if (Cache["SIPSumList" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("SIPSumList" + advisorVo.advisorId, dtSIPBookMIS);
                }
                else
                {
                    Cache.Remove("SIPSumList" + advisorVo.advisorId);
                    Cache.Insert("SIPSumList" + advisorVo.advisorId, dtSIPBookMIS);
                }
                gvSIPSummaryBookMIS.DataSource = dtSIPBookMIS;
                gvSIPSummaryBookMIS.DataBind();
                gvSIPSummaryBookMIS.Visible = true;
                pnlSIPSumBook.Visible = true;
                btnExport.Visible = true;
                trNoRecords.Visible = false;
                divNoRecords.Visible = false;

            }
            else
            {
                gvSIPSummaryBookMIS.DataSource = dtSIPBookMIS;
                gvSIPSummaryBookMIS.DataBind();
                //gvSIPBookMIS.Visible = false;
                pnlSIPSumBook.Visible = true;
                trNoRecords.Visible = true;
                divNoRecords.Visible = true;
                btnExport.Visible = false;
            }
        }
        private void SetParameter()
        {
            if (ddlOrderstatus.SelectedIndex != 0)
            {
                hdnOrderStatus.Value = ddlOrderstatus.SelectedValue;
                ViewState["OrderstatusDropDown"] = hdnOrderStatus.Value;
            }
            else
            {
                hdnOrderStatus.Value = "0";
            }
            if (ddlAMCCode.SelectedIndex != 0)
            {
                hdnAmc.Value = ddlAMCCode.SelectedValue;
                ViewState["AMCDropDown"] = hdnAmc.Value;
            }
            else
            {
                hdnAmc.Value = "0";
            }
        }
        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            //RadComboBox ddlAction = (RadComboBox)sender;
            //GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            //int selectedRow = gvr.ItemIndex + 1;
            //string action = "";
            //string orderId = gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderId"].ToString();
            //string customerId = gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString();
            //string assetGroupCode = gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["PAG_AssetGroupCode"].ToString();
            //if (ddlAction.SelectedItem.Value.ToString() == "Edit")
            //{
            //    action = "Edit";
            //    if (assetGroupCode == "MF")
            //    {
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderSIPTransType','strAction=" + ddlAction.SelectedItem.Value.ToString() + "&orderId=" + orderId + "&customerId=" + customerId + "');", true);
            //    }
         }
        protected void gvSIPSummaryBookMIS_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            gvSIPSummaryBookMIS.Visible = true;
            DataTable dtOrderBookMIS = new DataTable();
            dtOrderBookMIS = (DataTable)Cache["SIPSumList" + advisorVo.advisorId.ToString()];
            if (dtOrderBookMIS != null)
            {
                gvSIPSummaryBookMIS.DataSource = dtOrderBookMIS;
                gvSIPSummaryBookMIS.Visible = true;
            }

        }
        protected void btnExportFilteredData_OnClick(object sender, EventArgs e)
        {
            gvSIPSummaryBookMIS.ExportSettings.OpenInNewWindow = true;
            gvSIPSummaryBookMIS.ExportSettings.IgnorePaging = true;
            gvSIPSummaryBookMIS.ExportSettings.HideStructureColumns = true;
            gvSIPSummaryBookMIS.ExportSettings.ExportOnlyData = true;
            gvSIPSummaryBookMIS.ExportSettings.FileName = "SIP Summary Book Details";
            gvSIPSummaryBookMIS.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvSIPSummaryBookMIS.MasterTableView.ExportToExcel();
        }
    }
}



