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
                btnExport.Visible = true;
                trNoRecords.Visible = false;
               
                }
            else
            {
                gvOrderBookMIS.DataSource = dtOrderBookMIS;
                gvOrderBookMIS.DataBind();
                //gvOrderBookMIS.Visible = false;
                pnlOrderBook.Visible = true;
                trNoRecords.Visible = true;
                btnExport.Visible = false;
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
        private void GetMFOrderDetails(int orderId)
        {
            //DataSet dsGetMFOrderDetails = mforderBo.GetCustomerMFOrderDetails(orderId);
            //if (dsGetMFOrderDetails.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dsGetMFOrderDetails.Tables[0].Rows)
            //    {
            //        orderVo.OrderId = int.Parse(dr["CO_OrderId"].ToString());
            //        orderVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
            //        mforderVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
            //        mforderVo.CustomerName = dr["Customer_Name"].ToString();
            //        mforderVo.RMName = dr["RM_Name"].ToString();
            //        mforderVo.BMName = dr["AB_BranchName"].ToString();
            //        mforderVo.PanNo = dr["C_PANNum"].ToString();
            //        if (!string.IsNullOrEmpty(dr["PA_AMCCode"].ToString().Trim()))
            //            mforderVo.Amccode = int.Parse(dr["PA_AMCCode"].ToString());
            //        else
            //            mforderVo.Amccode = 0;
            //        if (!string.IsNullOrEmpty(dr["PAIC_AssetInstrumentCategoryCode"].ToString().Trim()))
            //            mforderVo.category = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
            //        if (!string.IsNullOrEmpty(dr["PASP_SchemePlanCode"].ToString().Trim()))
            //            mforderVo.SchemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
            //        mforderVo.OrderNumber = int.Parse(dr["CMFOD_OrderNumber"].ToString());
            //        if (!string.IsNullOrEmpty(dr["CMFOD_Amount"].ToString().Trim()))
            //            mforderVo.Amount = double.Parse(dr["CMFOD_Amount"].ToString());
            //        else
            //            mforderVo.Amount = 0;

            //        if (int.Parse(dr["CMFA_accountid"].ToString()) != 0)
            //            mforderVo.accountid = int.Parse(dr["CMFA_accountid"].ToString());
            //        else
            //            mforderVo.accountid = 0;
            //        mforderVo.FolioNumber = dr["CMFA_FolioNum"].ToString();
            //        mforderVo.TransactionCode = dr["WMTT_TransactionClassificationCode"].ToString();
            //        orderVo.OrderDate = DateTime.Parse(dr["CO_OrderDate"].ToString());
            //        mforderVo.IsImmediate = int.Parse(dr["CMFOD_IsImmediate"].ToString());
            //        orderVo.ApplicationNumber = dr["CO_ApplicationNumber"].ToString();
            //        if (!string.IsNullOrEmpty(dr["CO_ApplicationReceivedDate"].ToString()))
            //        {
            //            orderVo.ApplicationReceivedDate = DateTime.Parse(dr["CO_ApplicationReceivedDate"].ToString());
            //        }
            //        else
            //            orderVo.ApplicationReceivedDate = DateTime.MinValue;
            //        mforderVo.portfolioId = int.Parse(dr["CP_portfolioId"].ToString());
            //        orderVo.PaymentMode = dr["XPM_PaymentModeCode"].ToString();
            //        if (!string.IsNullOrEmpty(dr["CO_ChequeNumber"].ToString()))
            //            orderVo.ChequeNumber = dr["CO_ChequeNumber"].ToString();
            //        else
            //            orderVo.ChequeNumber = "";
            //        if (!string.IsNullOrEmpty(dr["CO_PaymentDate"].ToString()))
            //            orderVo.PaymentDate = DateTime.Parse(dr["CO_PaymentDate"].ToString());
            //        else
            //            orderVo.PaymentDate = DateTime.MinValue;

            //        if (!string.IsNullOrEmpty(dr["AAC_AdviserAgentId"].ToString()))
            //        {
            //            orderVo.AgentId = Convert.ToInt32(dr["AAC_AdviserAgentId"].ToString());
            //        }

            //        if (!string.IsNullOrEmpty(dr["AAC_AgentCode"].ToString()))
            //        {
            //            mforderVo.AgentCode = dr["AAC_AgentCode"].ToString();
            //        }

            //        if (!string.IsNullOrEmpty(dr["CMFOD_FutureTriggerCondition"].ToString()))
            //            mforderVo.FutureTriggerCondition = dr["CMFOD_FutureTriggerCondition"].ToString();
            //        else
            //            mforderVo.FutureTriggerCondition = "";
            //        if (!string.IsNullOrEmpty(dr["CMFOD_FutureExecutionDate"].ToString()))
            //            mforderVo.FutureExecutionDate = DateTime.Parse(dr["CMFOD_FutureExecutionDate"].ToString());
            //        else
            //            mforderVo.FutureExecutionDate = DateTime.MinValue;
            //        if (!string.IsNullOrEmpty(dr["PASP_SchemePlanSwitch"].ToString()))
            //            mforderVo.SchemePlanSwitch = int.Parse(dr["PASP_SchemePlanSwitch"].ToString());
            //        else
            //            mforderVo.SchemePlanSwitch = 0;
            //        if (!string.IsNullOrEmpty(dr["CB_CustBankAccId"].ToString()))
            //            orderVo.CustBankAccId = int.Parse(dr["CB_CustBankAccId"].ToString());
            //        else
            //            orderVo.CustBankAccId = 0;
            //        if (!string.IsNullOrEmpty(dr["CMFOD_BankName"].ToString()))
            //            mforderVo.BankName = dr["CMFOD_BankName"].ToString();
            //        else
            //            mforderVo.BankName = "";
            //        if (!string.IsNullOrEmpty(dr["CMFOD_BranchName"].ToString()))
            //            mforderVo.BranchName = dr["CMFOD_BranchName"].ToString();
            //        else
            //            mforderVo.BranchName = "";
            //        if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine1"].ToString()))
            //            mforderVo.AddrLine1 = dr["CMFOD_AddrLine1"].ToString();
            //        else
            //            mforderVo.AddrLine1 = "";
            //        if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine2"].ToString()))
            //            mforderVo.AddrLine2 = dr["CMFOD_AddrLine2"].ToString();
            //        else
            //            mforderVo.AddrLine2 = "";
            //        if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine3"].ToString()))
            //            mforderVo.AddrLine3 = dr["CMFOD_AddrLine3"].ToString();
            //        else
            //            mforderVo.AddrLine3 = "";
            //        if (!string.IsNullOrEmpty(dr["CMFOD_City"].ToString()))
            //            mforderVo.City = dr["CMFOD_City"].ToString();
            //        else
            //            mforderVo.City = "";
            //        if (!string.IsNullOrEmpty(dr["CMFOD_State"].ToString()))
            //            mforderVo.State = dr["CMFOD_State"].ToString();
            //        else
            //            mforderVo.State = "";
            //        if (!string.IsNullOrEmpty(dr["CMFOD_Country"].ToString()))
            //            mforderVo.Country = dr["CMFOD_Country"].ToString();
            //        else
            //            mforderVo.Country = "";
            //        if (!string.IsNullOrEmpty(dr["CMFOD_PinCode"].ToString()))
            //            mforderVo.Pincode = dr["CMFOD_PinCode"].ToString();
            //        else
            //            mforderVo.Pincode = "";
            //        if (!string.IsNullOrEmpty(dr["CMFOD_LivingScince"].ToString()))
            //            mforderVo.LivingSince = DateTime.Parse(dr["CMFOD_LivingScince"].ToString());
            //        else
            //            mforderVo.LivingSince = DateTime.MinValue;

            //        if (!string.IsNullOrEmpty(dr["XF_FrequencyCode"].ToString()))
            //            mforderVo.FrequencyCode = dr["XF_FrequencyCode"].ToString();
            //        else
            //            mforderVo.FrequencyCode = "";
            //        if (!string.IsNullOrEmpty(dr["CMFOD_StartDate"].ToString()))
            //            mforderVo.StartDate = DateTime.Parse(dr["CMFOD_StartDate"].ToString());
            //        else
            //            mforderVo.StartDate = DateTime.MinValue;
            //        if (!string.IsNullOrEmpty(dr["CMFOD_EndDate"].ToString()))
            //            mforderVo.EndDate = DateTime.Parse(dr["CMFOD_EndDate"].ToString());
            //        else
            //            mforderVo.EndDate = DateTime.MinValue;

            //        if (!string.IsNullOrEmpty(dr["CMFOD_Units"].ToString()))
            //            mforderVo.Units = double.Parse(dr["CMFOD_Units"].ToString());
            //        else
            //            mforderVo.Units = 0;

            //        if (!string.IsNullOrEmpty(dr["CMFOD_ARNNo"].ToString()))
            //        {
            //            mforderVo.ARNNo = Convert.ToString(dr["CMFOD_ARNNo"]);
            //        }

            //    }
            //    Session["orderVo"] = orderVo;
            //    Session["mforderVo"] = mforderVo;
            //}
        }
        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
         
          
                RadComboBox ddlAction = (RadComboBox)sender;
                GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;
                string action = "";
                string orderId = gvOrderBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderId"].ToString();
                string customerId = gvOrderBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString();
                string assetGroupCode = gvOrderBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["PAG_AssetGroupCode"].ToString();
                string Code = gvOrderBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["WMTT_TransactionClassificationCode"].ToString();
            
                if (ddlAction.SelectedItem.Value.ToString() == "Edit")
                {
                   action = "Edit";                  
                   if (assetGroupCode == "MF")
                    {
                        if (Code == "BUY")
                        //int mfOrderId = int.Parse(orderId);
                        //GetMFOrderDetails(mfOrderId);
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
              
                if (ddlAction.SelectedItem.Value.ToString() == "Cancel")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
                }
            }
           

        protected void btnExportFilteredData_OnClick(object sender, EventArgs e)
        {
            gvOrderBookMIS.ExportSettings.OpenInNewWindow = true;
            gvOrderBookMIS.ExportSettings.IgnorePaging = true;
            gvOrderBookMIS.ExportSettings.HideStructureColumns = true;
            gvOrderBookMIS.ExportSettings.ExportOnlyData = true;
            gvOrderBookMIS.ExportSettings.FileName = "OrderBook Details";
            gvOrderBookMIS.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvOrderBookMIS.MasterTableView.ExportToExcel();
        }
    }
}
