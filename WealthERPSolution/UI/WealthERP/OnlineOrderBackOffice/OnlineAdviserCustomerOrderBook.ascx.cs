﻿using System;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVO = (CustomerVo)Session["customerVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();          
           // customerId = customerVO.CustomerId;                              
            if (!Page.IsPostBack)
            {
                BindAmc();
                BindOrderStatus();
                Cache.Remove("OrderList" + advisorVo.advisorId);               
                fromDate = DateTime.Now.AddMonths(-1);
                txtOrderFrom.SelectedDate = fromDate.Date;
                txtOrderTo.SelectedDate = DateTime.Now;
                
            }
           
        }

        protected void btnViewOrder_Click(object sender, EventArgs e)
        {
            SetParameter();
            BindOrderBook();
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
             if (txtOrderFrom.SelectedDate != null)
                 fromDate = DateTime.Parse(txtOrderFrom.SelectedDate.ToString());
             if (txtOrderTo.SelectedDate != null)
                 toDate = DateTime.Parse(txtOrderTo.SelectedDate.ToString());

            dsOrderBookMIS = OnlineOrderMISBo.GetOrderBookMIS(advisorVo.advisorId, int.Parse(hdnAmc.Value), hdnOrderStatus.Value, fromDate, toDate);
            dtOrderBookMIS = dsOrderBookMIS.Tables[0];
            if (dtOrderBookMIS.Rows.Count > 0)
            {
                gvOrderBookMIS.DataSource = dtOrderBookMIS;
                gvOrderBookMIS.DataBind();
                gvOrderBookMIS.Visible = true;
                pnlOrderBook.Visible = true;
                imgexportButton.Visible = true;
                trNoRecords.Visible = false;
                if (Cache["OrderList" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("OrderList" + advisorVo.advisorId, dtOrderBookMIS);
                }
                else
                {
                    Cache.Remove("OrderList" + advisorVo.advisorId);
                    Cache.Insert("OrderList" + advisorVo.advisorId, dtOrderBookMIS);
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
            dtOrderBookMIS = (DataTable)Cache["OrderList" + advisorVo.advisorId.ToString()];
            if (dtOrderBookMIS != null)

            {
                gvOrderBookMIS.DataSource = dtOrderBookMIS;                
                gvOrderBookMIS.Visible = true;
            }

        }


     protected void gvOrderBookMIS_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
      { 

            if (e.CommandName == "Edit")
            {
            string orderId = gvOrderBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString();
            string customerId = gvOrderBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["C_CustomerId"].ToString();
            string assetGroupCode = gvOrderBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PAG_AssetGroupCode"].ToString();
            string Code = gvOrderBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WMTT_TransactionClassificationCode"].ToString();
                if (assetGroupCode == "MF")
                {
                    if (Code == "BUY")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderPurchaseTransType", "loadcontrol('MFOrderPurchaseTransType','action=Edit');", true);
                    }
                    else if (Code == "ABY")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderAdditionalPurchase", "loadcontrol('MFOrderAdditionalPurchase','action=Edit');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderRdemptionTransType", "loadcontrol('MFOrderRdemptionTransType','action=Edit');", true);
                    }
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
