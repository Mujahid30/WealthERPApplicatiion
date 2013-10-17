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

namespace WealthERP.OnlineOrderManagement
{
    public partial class CustomerMFOrderBookList : System.Web.UI.UserControl
    {
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        AdvisorVo advisorVo;    
        CustomerVo customerVO = new CustomerVo();
        MFOrderBo mforderBo = new MFOrderBo();
        MFOrderVo mforderVo = new MFOrderVo();
        OrderVo orderVo = new OrderVo();
        string userType;
        int customerId = 0;
        int AccountId=0;
        DateTime fromDate;
        DateTime toDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVO = (CustomerVo)Session["customerVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();          
            customerId = customerVO.CustomerId;
            customerAccountsVo = (CustomerAccountsVo)Session["FolioVo"];
            BindFolioAccount();
            if (!Page.IsPostBack)
            {
                hdnAccount.Value = "0";
                fromDate = DateTime.Now.AddMonths(-1);
                txtFrom.SelectedDate = fromDate.Date;
                txtTo.SelectedDate = DateTime.Now;
                
            }
           
        }

        protected void btnViewOrder_Click(object sender, EventArgs e)
        {
            SetParameter();
            BindOrderBook();
        }

        /// <summary>
        /// Get Folio Account for Customer
        /// </summary>
        private void BindFolioAccount()
        {
            ddlAccount.Items.Clear();
            DataSet dsFolioAccount;
            DataTable dtFolioAccount;
            dsFolioAccount = OnlineMFOrderBo.GetFolioAccount(customerId);
            dtFolioAccount = dsFolioAccount.Tables[0];
            if (dtFolioAccount.Rows.Count > 0)
            {
                ddlAccount.DataSource = dsFolioAccount.Tables[0];
                ddlAccount.DataTextField = dtFolioAccount.Columns["CMFA_FolioNum"].ToString();
                ddlAccount.DataValueField = dtFolioAccount.Columns["CMFA_AccountId"].ToString();
                ddlAccount.DataBind();
            }
              ddlAccount.Items.Insert(0, new ListItem("All", "0"));
        }
        /// <summary>
        /// Get Order Book MIS
        /// </summary>
        protected void BindOrderBook()
        {
            //int IsOnline = 0; 
             DataSet dsOrderBookMIS = new DataSet();
             DataTable dtOrderBookMIS = new DataTable();
             if (txtFrom.SelectedDate != null)
             fromDate = DateTime.Parse(txtFrom.SelectedDate.ToString());
             if (txtTo.SelectedDate != null)
             toDate = DateTime.Parse(txtTo.SelectedDate.ToString());
             //AccountId = int.Parse(ViewState["AccountDropDown"].ToString());
             //if (customerAccountsVo.IsOnline == 0)
             //{
             //    hdnAccount.Value = "0"; 
             //}

            dsOrderBookMIS = OnlineMFOrderBo.GetOrderBookMIS(customerId, int.Parse(hdnAccount.Value), fromDate, toDate);
            dtOrderBookMIS = dsOrderBookMIS.Tables[0];
            if (dtOrderBookMIS.Rows.Count > 0)
            {
                if (Cache["OrderList" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("OrderList" + advisorVo.advisorId, dtOrderBookMIS);
                }
                else
                {
                    Cache.Remove("OrderList" + advisorVo.advisorId);
                    Cache.Insert("OrderList" + advisorVo.advisorId, dtOrderBookMIS);
                }
                gvOrderBookMIS.DataSource = dtOrderBookMIS;
                gvOrderBookMIS.DataBind();
                gvOrderBookMIS.Visible = true;
                pnlOrderBook.Visible = true;
               // btnExport.Visible = true;
                trNoRecords.Visible = false;
               
                }
            else
            {
                gvOrderBookMIS.DataSource = dtOrderBookMIS;
                gvOrderBookMIS.DataBind();
                //gvOrderBookMIS.Visible = false;
                pnlOrderBook.Visible = true;
                trNoRecords.Visible = true;
               // btnExport.Visible = false;
            }
        }
        private void SetParameter()
        {
            if (ddlAccount.SelectedIndex != 0)
            {
                hdnAccount.Value = ddlAccount.SelectedValue;
                ViewState["AccountDropDown"] = hdnAccount.Value;
            }
            else
            {
                hdnAccount.Value = "0";
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
        {   GridDataItem gvr = (GridDataItem)e.Item;
            string orderId = gvOrderBookMIS.MasterTableView.DataKeyValues[gvr.ItemIndex]["CO_OrderId"].ToString();
            string customerId = gvOrderBookMIS.MasterTableView.DataKeyValues[gvr.ItemIndex]["C_CustomerId"].ToString();
            string assetGroupCode = gvOrderBookMIS.MasterTableView.DataKeyValues[gvr.ItemIndex]["PAG_AssetGroupCode"].ToString();
            string Code = gvOrderBookMIS.MasterTableView.DataKeyValues[gvr.ItemIndex]["WMTT_TransactionClassificationCode"].ToString();

            if (e.CommandName == "Edit")
            {
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
        //protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        //{
         
          
        //        RadComboBox ddlAction = (RadComboBox)sender;
        //        GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
        //        int selectedRow = gvr.ItemIndex + 1;
        //        string action = "";
        //        string orderId = gvOrderBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderId"].ToString();
        //        string customerId = gvOrderBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString();
        //        string assetGroupCode = gvOrderBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["PAG_AssetGroupCode"].ToString();
        //        string Code = gvOrderBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["WMTT_TransactionClassificationCode"].ToString();
            
        //        if (ddlAction.SelectedItem.Value.ToString() == "Edit")
        //        {
        //           action = "Edit";                  
        //           if (assetGroupCode == "MF")
        //            {
        //                if (Code == "BUY")
        //                //int mfOrderId = int.Parse(orderId);
        //                //GetMFOrderDetails(mfOrderId);
        //                {
        //                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderPurchaseTransType", "loadcontrol('MFOrderPurchaseTransType','action=Edit');", true);
        //                }
        //                else if (Code == "ABY")
        //                {
        //                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderAdditionalPurchase", "loadcontrol('MFOrderAdditionalPurchase','action=Edit');", true);
        //                }
        //                else
        //                {
        //                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderRdemptionTransType", "loadcontrol('MFOrderRdemptionTransType','action=Edit');", true);
        //                }
        //            }
                   
        //        }
              
        //        if (ddlAction.SelectedItem.Value.ToString() == "Cancel")
        //        {
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
        //        }
        //    }
           

        //protected void btnExportFilteredData_OnClick(object sender, EventArgs e)
        //{
        //    gvOrderBookMIS.ExportSettings.OpenInNewWindow = true;
        //    gvOrderBookMIS.ExportSettings.IgnorePaging = true;
        //    gvOrderBookMIS.ExportSettings.HideStructureColumns = true;
        //    gvOrderBookMIS.ExportSettings.ExportOnlyData = true;
        //    gvOrderBookMIS.ExportSettings.FileName = "OrderBook Details";
        //    gvOrderBookMIS.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
        //    gvOrderBookMIS.MasterTableView.ExportToExcel();
        //}
    }
}
