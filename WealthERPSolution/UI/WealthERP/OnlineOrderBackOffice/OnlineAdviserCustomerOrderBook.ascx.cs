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
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoCustomerProfiling;
using BoOnlineOrderManagement;
using VoOps;
using BoOps;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineAdviserCustomerOrderBook : System.Web.UI.UserControl
    {
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        OnlineOrderMISBo OnlineOrderMISBo = new OnlineOrderMISBo();
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        AdvisorVo advisorVo;
        CustomerVo customerVO = new CustomerVo();
        MFOrderBo mforderBo = new MFOrderBo();
        MFOrderVo mforderVo = new MFOrderVo();
        OrderVo orderVo = new OrderVo();
        string userType;
        int customerId = 0;
        int AccountId = 0;
        DateTime fromDate;
        DateTime toDate;
        UserVo userVo;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVO = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            userType = Session[SessionContents.CurrentUserRole].ToString();
            // customerId = customerVO.CustomerId;                              
            if (!Page.IsPostBack)
            {
                BindAmc();
                BindOrderStatus();
                Cache.Remove("OrderList" + advisorVo.advisorId);

                if (Request.QueryString["orderId"] == null)
                {
                    if (ddlType.SelectedValue != "ON")
                    {
                        divConditional.Visible = true;
                        lblOrderNo.Visible = false;
                        txtOrderNo.Visible = false;
                        txtOrderNo.Text = "";
                        fromDate = DateTime.Now;
                        txtOrderFrom.SelectedDate = fromDate.Date;
                        txtOrderTo.SelectedDate = DateTime.Now;
                    }
                }
                if (Request.QueryString["orderId"] != null || Request.QueryString["txtFolioNo"] != null)
                {
                    ViewState["OrderId"] = int.Parse(Request.QueryString["orderId"].ToString());
                    ViewState["FolioNo"] = Request.QueryString["txtFolioNo"];
                    ViewState["IsDemat"] = Request.QueryString["IsDemat"];
                    BindOrderBook();
                    tblField.Visible = false;
                    tblOrder.Visible = false;
                    divConditional.Visible = false;
                }

            }

        }
        protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "ON")
            {
                lblOrderNo.Visible = true;
                txtOrderNo.Visible = true;
                divConditional.Visible = false;
            }
            else
            {
                divConditional.Visible = true;
                lblOrderNo.Visible = false;
                txtOrderNo.Visible = false;
                txtOrderNo.Text = "";
            }


        }
        protected void btnViewOrder_Click(object sender, EventArgs e)
        {
            SetParameter();
            BindOrderBook();
            if (ddlMode.SelectedValue == "1")
            {
                gvOrderBookMIS.Columns[2].Visible = true;
            }
            else
            {
                gvOrderBookMIS.Columns[2].Visible = false;
            }
        }

        /// <summary>
        /// Get Bind Orderstatus
        /// </summary>
        private void BindOrderStatus()
        {
            ddlOrderStatus.Items.Clear();
            DataSet dsOrderStatus;
            DataTable dtOrderStatus;
            dsOrderStatus = OnlineMFOrderBo.GetOrderStatus();
            dtOrderStatus = dsOrderStatus.Tables[0];
            if (dtOrderStatus.Rows.Count > 0)
            {
                ddlOrderStatus.DataSource = dtOrderStatus;
                ddlOrderStatus.DataTextField = dtOrderStatus.Columns["WOS_OrderStep"].ToString();
                ddlOrderStatus.DataValueField = dtOrderStatus.Columns["WOS_OrderStepCode"].ToString();
                ddlOrderStatus.DataBind();
            }
            ddlOrderStatus.Items.Insert(0, new ListItem("All", "0"));
        }


        protected void BindAmc()
        {
            ddlAmc.Items.Clear();
            if (ddlAmc.SelectedIndex == 0) return;

            DataTable dtAmc = commonLookupBo.GetProdAmc(0, true);
            if (dtAmc == null) return;

            if (dtAmc.Rows.Count > 0)
            {
                ddlAmc.DataSource = dtAmc;
                ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAmc.DataBind();
            }
            //DataSet ds = new DataSet();
            //DataTable dtAmc = new DataTable();
            //ds = OnlineMFOrderBo.GetOrderAmcDetails(customerId);
            //dtAmc = ds.Tables[0];
            //if (dtAmc.Rows.Count > 0)
            //{
            //    ddlAmc.DataSource = dtAmc;
            //    ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
            //    ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
            //    ddlAmc.DataBind();

            //    //BindFolioNumber(int.Parse(ddlAmc.SelectedValue));

            //}
            ddlAmc.Items.Insert(0, new ListItem("All", "0"));

        }

        /// <summary>
        /// Get Folio Account for Customer
        /// </summary>
        private void BindFolioAccount()
        {
            //ddlAccount.Items.Clear();
            //DataSet dsFolioAccount;
            //DataTable dtFolioAccount;
            //dsFolioAccount = OnlineMFOrderBo.GetFolioAccount(customerId);
            //dtFolioAccount = dsFolioAccount.Tables[0];
            //if (dtFolioAccount.Rows.Count > 0)
            //{
            //    ddlAccount.DataSource = dsFolioAccount.Tables[0];
            //    ddlAccount.DataTextField = dtFolioAccount.Columns["CMFA_FolioNum"].ToString();
            //    ddlAccount.DataValueField = dtFolioAccount.Columns["CMFA_AccountId"].ToString();
            //    ddlAccount.DataBind();
            //}
            //  ddlAccount.Items.Insert(0, new ListItem("All", "0"));
        }
        /// <summary>
        /// Get Order Book MIS
        /// </summary>
        protected void BindOrderBook()
        {
            DataSet dsOrderBookMIS = new DataSet();
            DataTable dtOrderBookMIS = new DataTable();

            if (Request.QueryString["orderId"] != null || Request.QueryString["txtFolioNo"] != null)
            {
                dsOrderBookMIS = OnlineOrderMISBo.GetOrderBookMIS(advisorVo.advisorId, 0, "0", fromDate, toDate, (!string.IsNullOrEmpty(ViewState["OrderId"].ToString())) ? int.Parse(ViewState["OrderId"].ToString()) : 0, (!string.IsNullOrEmpty(ViewState["FolioNo"].ToString())) ? ViewState["FolioNo"].ToString() : null,Convert.ToBoolean(ViewState["IsDemat"].ToString())==true?1:0);
            }
            else
            {
                if (ddlType.SelectedValue != "ON")
                {
                    if (txtOrderFrom.SelectedDate != null)
                        fromDate = DateTime.Parse(txtOrderFrom.SelectedDate.ToString());
                    if (txtOrderTo.SelectedDate != null)
                        toDate = DateTime.Parse(txtOrderTo.SelectedDate.ToString());
                }
                dsOrderBookMIS = OnlineOrderMISBo.GetOrderBookMIS(advisorVo.advisorId, int.Parse(hdnAmc.Value), hdnOrderStatus.Value, fromDate, toDate, (!string.IsNullOrEmpty(txtOrderNo.Text)) ? int.Parse(txtOrderNo.Text) : 0, null,int.Parse(ddlMode.SelectedValue));
            }
            dtOrderBookMIS = dsOrderBookMIS.Tables[0];
            if (dtOrderBookMIS.Rows.Count > 0)
            {
                gvOrderBookMIS.DataSource = dtOrderBookMIS;
                gvOrderBookMIS.DataBind();
                gvOrderBookMIS.Visible = true;
                pnlOrderBook.Visible = true;
                imgexportButton.Visible = true;
                trNoRecords.Visible = false;
                if (Cache["OrderList" + userVo.UserId] == null)
                {
                    Cache.Insert("OrderList" + userVo.UserId, dtOrderBookMIS);
                }
                else
                {
                    Cache.Remove("OrderList" + userVo.UserId);
                    Cache.Insert("OrderList" + userVo.UserId, dtOrderBookMIS);
                }

            }
            else
            {
                gvOrderBookMIS.DataSource = dtOrderBookMIS;
                gvOrderBookMIS.DataBind();
                pnlOrderBook.Visible = true;
                trNoRecords.Visible = true;
                imgexportButton.Visible = false;
            }
        }
        private void SetParameter()
        {
            if (ddlOrderStatus.SelectedIndex != 0)
            {
                hdnOrderStatus.Value = ddlOrderStatus.SelectedValue;
                ViewState["OrderstatusDropDown"] = hdnOrderStatus.Value;
            }
            else
            {
                hdnOrderStatus.Value = "0";
            }
            if (ddlAmc.SelectedIndex != 0)
            {
                hdnAmc.Value = ddlAmc.SelectedValue;
                ViewState["AMCDropDown"] = hdnAmc.Value;
            }
            else
            {
                hdnAmc.Value = "0";
            }

        }
        protected void gvOrderBookMIS_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            gvOrderBookMIS.Visible = true;
            DataTable dtOrderBookMIS = new DataTable();
            dtOrderBookMIS = (DataTable)Cache["OrderList" + userVo.UserId.ToString()];
            if (dtOrderBookMIS != null)
            {
                gvOrderBookMIS.DataSource = dtOrderBookMIS;
                gvOrderBookMIS.Visible = true;
            }

        }

        protected void gvOrderBookMIS_UpdateCommand(object source, GridCommandEventArgs e)
        {
            string strRemark = string.Empty;
            int IsMarked = 0;
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditableItem editItem = e.Item as GridEditableItem;
                TextBox txtRemark = (TextBox)e.Item.FindControl("txtRemark");
                strRemark = txtRemark.Text;
                LinkButton buttonEdit = editItem["MarkAsReject"].Controls[0] as LinkButton;
                Int32 orderId = Convert.ToInt32(gvOrderBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
                Int32 ExchangeNo = Convert.ToInt32(gvOrderBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["BMOERD_BSEOrderId"].ToString());
                IsMarked = mforderBo.MarkAsReject(orderId, txtRemark.Text);
                BindOrderBook();

            }
        }
        protected void gvOrderBookMIS_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string strRemark = string.Empty;
            int IsMarked = 0;
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditableItem editItem = e.Item as GridEditableItem;
                TextBox txtRemark = (TextBox)e.Item.FindControl("txtRemark");
                strRemark = txtRemark.Text;
                LinkButton buttonEdit = editItem["MarkAsReject"].Controls[0] as LinkButton;
                Int32 orderId = Convert.ToInt32(gvOrderBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
                Int32 ExchangeNo = Convert.ToInt32(gvOrderBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["BMOERD_BSEOrderId"].ToString());
                IsMarked = mforderBo.MarkAsReject(orderId, txtRemark.Text);
                BindOrderBook();

            }
            if (e.CommandName == "Select")
            {
                GridEditableItem editItem = e.Item as GridEditableItem;
                LinkButton lnkprAmcB = (LinkButton)editItem.FindControl("lnkprAmcB");
                Int32 orderId = Convert.ToInt32(gvOrderBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
                Int32 ExchangeNo = Convert.ToInt32(gvOrderBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["BMOERD_BSEOrderId"].ToString());
                OnlineOrderMISBo.UpdateOrderReverse(orderId, userVo.UserId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Order updated !!');", true);
                BindOrderBook();

            }
            //if (e.CommandName == "Edit")
            //{
            //string orderId = gvOrderBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString();
            //string customerId = gvOrderBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["C_CustomerId"].ToString();
            //string assetGroupCode = gvOrderBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PAG_AssetGroupCode"].ToString();
            //string Code = gvOrderBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WMTT_TransactionClassificationCode"].ToString();
            //    if (assetGroupCode == "MF")
            //    {
            //        if (Code == "BUY")
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderPurchaseTransType", "loadcontrol('MFOrderPurchaseTransType','action=Edit');", true);
            //        }
            //        else if (Code == "ABY")
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderAdditionalPurchase", "loadcontrol('MFOrderAdditionalPurchase','action=Edit');", true);
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderRdemptionTransType", "loadcontrol('MFOrderRdemptionTransType','action=Edit');", true);
            //        }
            //    }
            //}

        }
        protected void gvOrderBookMIS_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                LinkButton lbtnMarkAsReject = dataItem["MarkAsReject"].Controls[0] as LinkButton;
                LinkButton lnkprAmcB = (LinkButton)dataItem.FindControl("lnkprAmcB");
                string OrderStepCode = Convert.ToString(gvOrderBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XS_Status"]);
                if (OrderStepCode == "INPROCESS" || OrderStepCode == "EXECUTED")
                {
                    lbtnMarkAsReject.Visible = true;
                }
                else
                {
                    lbtnMarkAsReject.Visible = false;
                }
                if (OrderStepCode == "ACCEPTED")
                {
                    lnkprAmcB.Enabled = true;
                }
                else
                {
                    lnkprAmcB.ForeColor = System.Drawing.Color.Black;
                    lnkprAmcB.Enabled = false;
                    lnkprAmcB.OnClientClick = "";
                }

            }
        }
        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        //protected void btnExportFilteredData_OnClick(object sender, EventArgs e)
        //{
        //    gvOrderBookMIS.ExportSettings.OpenInNewWindow = true;
        //    gvOrderBookMIS.ExportSettings.IgnorePaging = true;
        //    gvOrderBookMIS.ExportSettings.HideStructureColumns = false;
        //    gvOrderBookMIS.ExportSettings.ExportOnlyData = true;
        //    gvOrderBookMIS.ExportSettings.FileName = "OrderBook Details";
        //    gvOrderBookMIS.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
        //    gvOrderBookMIS.MasterTableView.ExportToExcel();
        //}
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {

            gvOrderBookMIS.ExportSettings.OpenInNewWindow = true;
            gvOrderBookMIS.ExportSettings.IgnorePaging = true;
            //gvOrderBookMIS.ExportSettings.HideStructureColumns = false;
            gvOrderBookMIS.MasterTableView.GetColumn("C_CustCode").Display = true;
            gvOrderBookMIS.ExportSettings.ExportOnlyData = true;
            gvOrderBookMIS.ExportSettings.FileName = "OrderBook Details";
            gvOrderBookMIS.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvOrderBookMIS.MasterTableView.ExportToExcel();
        }
    }
}
