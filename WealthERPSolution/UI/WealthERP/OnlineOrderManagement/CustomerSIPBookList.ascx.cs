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
    public partial class CustomerSIPBookList : System.Web.UI.UserControl
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
        int systematicId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            OnlineUserSessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVO = (CustomerVo)Session["customerVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();
            customerId = customerVO.CustomerId;
            BindAmc();
            BindOrderStatus();
            BindLink();
            //lbBack.Attributes.Add("onClick", "javascript:history.back(); return false;");
            if (!Page.IsPostBack)
            {
                fromDate = DateTime.Now.AddMonths(-1);
                txtFrom.SelectedDate = fromDate.Date;
                txtTo.SelectedDate = DateTime.Now;

            }

        }
        protected void BindLink()
        {
            if (Request.QueryString["systematicId"] != null)
            {
                int systematicId = int.Parse(Request.QueryString["systematicId"].ToString());
                int amcCode = int.Parse(Request.QueryString["AmcCode"].ToString());
                string systematictype = Request.QueryString["systematicType"].ToString();
                ViewState["systematicId"] = int.Parse(systematicId.ToString());
                string OrderStatus = string.Empty;

                if (Request.QueryString["OrderStatus"] != null)
                {
                    OrderStatus = Request.QueryString["OrderStatus"].ToString();
                    ViewState["OrderStatus"] = OrderStatus;
                }
                else
                {
                    ViewState["OrderStatus"] = null;
                }

                string fromdate = Request.QueryString["FromDate"].ToString();
                string todate = Request.QueryString["ToDate"].ToString();
                txtFrom.SelectedDate = DateTime.Parse(fromdate);
                hdnAmc.Value = "0";
                BindSIPBook(DateTime.Parse(fromdate), DateTime.Now);
                lbBack.Visible = true;
            }
        }

        protected void btnViewOrder_Click(object sender, EventArgs e)
        {
            SetParameter();
            if (txtFrom.SelectedDate != null)
                fromDate = DateTime.Parse(txtFrom.SelectedDate.ToString());
            if (txtTo.SelectedDate != null)
                toDate = DateTime.Parse(txtTo.SelectedDate.ToString());
            BindSIPBook(fromDate, toDate);
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


        /// <summary>
        /// get AMC
        /// </summary>
        protected void BindAmc()
        {
            ddlAMCCode.Items.Clear();
            DataSet ds = new DataSet();
            DataTable dtAmc = new DataTable();
            ds = OnlineMFOrderBo.GetSIPAmcDetails(customerId, "SIP");
            dtAmc = ds.Tables[0];
            if (dtAmc.Rows.Count > 0)
            {
                ddlAMCCode.DataSource = dtAmc;
                ddlAMCCode.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAMCCode.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAMCCode.DataBind();

            }
            ddlAMCCode.Items.Insert(0, new ListItem("All", "0"));
        }

        /// <summary>
        /// Get Folio Account for Customer
        /// </summary>
        //private void BindFolioAccount()
        //{
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
        //}
        /// <summary>
        /// Get Order Book MIS
        /// </summary>
        protected void BindSIPBook(DateTime fromDate, DateTime toDate)
        {
            DataSet dsSIPBookMIS = new DataSet();
            DataTable dtSIPBookMIS = new DataTable();
            //if (txtFrom.SelectedDate != null)
            //    fromDate = DateTime.Parse(txtFrom.SelectedDate.ToString());
            //if (txtTo.SelectedDate != null)
            //    toDate = DateTime.Parse(txtTo.SelectedDate.ToString());
            systematicId = Convert.ToInt32(ViewState["systematicId"]);
            if (ViewState["OrderStatus"] != null)
            {
                dsSIPBookMIS = OnlineMFOrderBo.GetSIPBookMIS(customerId, int.Parse(hdnAmc.Value), ViewState["OrderStatus"].ToString(), systematicId, fromDate, toDate);
            }
            else
            {
                dsSIPBookMIS = OnlineMFOrderBo.GetSIPBookMIS(customerId, int.Parse(hdnAmc.Value), hdnOrderStatus.Value, systematicId, fromDate, toDate);
            }
            dtSIPBookMIS = dsSIPBookMIS.Tables[0];
            if (dtSIPBookMIS.Rows.Count > 0)
            {
                if (Cache["SIPList" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("SIPList" + advisorVo.advisorId, dtSIPBookMIS);
                }
                else
                {
                    Cache.Remove("SIPList" + advisorVo.advisorId);
                    Cache.Insert("SIPList" + advisorVo.advisorId, dtSIPBookMIS);
                }
                gvSIPBookMIS.DataSource = dtSIPBookMIS;
                gvSIPBookMIS.DataBind();
                gvSIPBookMIS.Visible = true;
                pnlSIPBook.Visible = true;
                btnExport.Visible = true;
                trNoRecords.Visible = false;
                divNoRecords.Visible = false;

            }
            else
            {
                gvSIPBookMIS.DataSource = dtSIPBookMIS;
                gvSIPBookMIS.DataBind();
                //gvSIPBookMIS.Visible = false;
                pnlSIPBook.Visible = true;
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
        protected void gvSIPBookMIS_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string orderId = gvSIPBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString();
                string customerId = gvSIPBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["C_CustomerId"].ToString();
                string assetGroupCode = gvSIPBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PAG_AssetGroupCode"].ToString();
                if (assetGroupCode == "MF")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderSIPTransType','&orderId=" + orderId + "&customerId=" + customerId + "');", true);
                }
            }

        }
        //protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    RadComboBox ddlAction = (RadComboBox)sender;
        //    GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
        //    int selectedRow = gvr.ItemIndex + 1;

        //    string action = "";
        //    string orderId = gvSIPBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderId"].ToString();
        //    string customerId = gvSIPBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString();
        //    string assetGroupCode = gvSIPBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["PAG_AssetGroupCode"].ToString();

        //    if (ddlAction.SelectedItem.Value.ToString() == "Edit")
        //    {
        //        action = "Edit";
        //        if (assetGroupCode == "MF")
        //        {
        //            //int mfOrderId = int.Parse(orderId);
        //            //GetMFOrderDetails(mfOrderId);

        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderSIPTransType','strAction=" + ddlAction.SelectedItem.Value.ToString() + "&orderId=" + orderId + "&customerId=" + customerId + "');", true);   

        //        }

        //    }

        //    if (ddlAction.SelectedItem.Value.ToString() == "Cancel")
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
        //    }
        //}

        protected void gvSIPBookMIS_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            gvSIPBookMIS.Visible = true;
            DataTable dtOrderBookMIS = new DataTable();
            dtOrderBookMIS = (DataTable)Cache["SIPList" + advisorVo.advisorId.ToString()];
            if (dtOrderBookMIS != null)
            {
                gvSIPBookMIS.DataSource = dtOrderBookMIS;
                gvSIPBookMIS.Visible = true;
            }

        }
        protected void btnExportFilteredData_OnClick(object sender, EventArgs e)
        {
            gvSIPBookMIS.ExportSettings.OpenInNewWindow = true;
            gvSIPBookMIS.ExportSettings.IgnorePaging = true;
            gvSIPBookMIS.ExportSettings.HideStructureColumns = true;
            gvSIPBookMIS.ExportSettings.ExportOnlyData = true;
            gvSIPBookMIS.ExportSettings.FileName = "SIP OrderBook Details";
            gvSIPBookMIS.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvSIPBookMIS.MasterTableView.ExportToExcel();
        }
        protected void lbBack1_OnClick(object sender, EventArgs e)
        {
            if (Request.QueryString["systematicId"] != null)
            {
                int systematicId = int.Parse(Request.QueryString["systematicId"].ToString());
                int AmcCode = int.Parse(Request.QueryString["AmcCode"].ToString());
                string fromdate = Request.QueryString["FromDate"].ToString();
                string todate = Request.QueryString["ToDate"].ToString();
                string systematictype = Request.QueryString["systematicType"].ToString();
                ViewState["systematicId"] = int.Parse(systematicId.ToString());
                string OrderStatus = string.Empty;

                if (Request.QueryString["OrderStatus"] != null)
                {
                    OrderStatus = Request.QueryString["OrderStatus"].ToString();
                    ViewState["OrderStatus"] = OrderStatus;
                }
                else
                {
                    ViewState["OrderStatus"] = null;
                }
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "SIPBookSummmaryList", "loadcontrol('SIPBookSummmaryList','?systematicId=" + systematicId + "&OrderStatus=" + OrderStatus + "&AmcCode=" + AmcCode + "&FromDate=" + fromdate + "&ToDate=" + todate + "&systematicType=" + systematictype + "');", true);
            }
        }

        #region DDLVIEWEDITSELECTION

        protected void ddlAction_OnSelectedIndexChange(object sender, EventArgs e)
        {

            RadComboBox ddlAction = (RadComboBox)sender;

            GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            int selectedRow = gvr.ItemIndex + 1;
            string strAction = string.Empty;


            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderSIPTransType','strAction=" + ddlAction.SelectedValue + "');", true);


        }
        #endregion
    }
}