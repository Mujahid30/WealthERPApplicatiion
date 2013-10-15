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
        int AccountId = 0;
        DateTime fromDate;
        DateTime toDate;       
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVO = (CustomerVo)Session["customerVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();
            customerId = customerVO.CustomerId;
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
            BindSIPBook();
        }

        /// <summary>
        /// Get Folio Account for Customer
        /// </summary>
        private void BindFolioAccount()
        {
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
        protected void BindSIPBook()
        {
            DataSet dsSIPBookMIS = new DataSet();
            DataTable dtSIPBookMIS = new DataTable();
            if (txtFrom.SelectedDate != null)
                fromDate = DateTime.Parse(txtFrom.SelectedDate.ToString());
            if (txtTo.SelectedDate != null)
                toDate = DateTime.Parse(txtTo.SelectedDate.ToString());           
            dsSIPBookMIS = OnlineMFOrderBo.GetSIPBookMIS(advisorVo.advisorId, customerId, int.Parse(hdnAccount.Value), fromDate, toDate);
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
        private void GetMFOrderDetails(int orderId)
        {
            DataSet dsGetMFOrderDetails = mforderBo.GetCustomerMFOrderDetails(orderId);
            if (dsGetMFOrderDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsGetMFOrderDetails.Tables[0].Rows)
                {
                    orderVo.OrderId = int.Parse(dr["CO_OrderId"].ToString());
                    orderVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                    mforderVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                    mforderVo.CustomerName = dr["Customer_Name"].ToString();
                    mforderVo.RMName = dr["RM_Name"].ToString();
                    mforderVo.BMName = dr["AB_BranchName"].ToString();
                    mforderVo.PanNo = dr["C_PANNum"].ToString();
                    if (!string.IsNullOrEmpty(dr["PA_AMCCode"].ToString().Trim()))
                        mforderVo.Amccode = int.Parse(dr["PA_AMCCode"].ToString());
                    else
                        mforderVo.Amccode = 0;
                    if (!string.IsNullOrEmpty(dr["PAIC_AssetInstrumentCategoryCode"].ToString().Trim()))
                        mforderVo.category = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["PASP_SchemePlanCode"].ToString().Trim()))
                        mforderVo.SchemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                    mforderVo.OrderNumber = int.Parse(dr["CMFOD_OrderNumber"].ToString());
                    if (!string.IsNullOrEmpty(dr["CMFOD_Amount"].ToString().Trim()))
                        mforderVo.Amount = double.Parse(dr["CMFOD_Amount"].ToString());
                    else
                        mforderVo.Amount = 0;

                    if (int.Parse(dr["CMFA_accountid"].ToString()) != 0)
                        mforderVo.accountid = int.Parse(dr["CMFA_accountid"].ToString());
                    else
                        mforderVo.accountid = 0;
                    mforderVo.FolioNumber = dr["CMFA_FolioNum"].ToString();
                    mforderVo.TransactionCode = dr["WMTT_TransactionClassificationCode"].ToString();
                    orderVo.OrderDate = DateTime.Parse(dr["CO_OrderDate"].ToString());
                    mforderVo.IsImmediate = int.Parse(dr["CMFOD_IsImmediate"].ToString());
                    orderVo.ApplicationNumber = dr["CO_ApplicationNumber"].ToString();
                    if (!string.IsNullOrEmpty(dr["CO_ApplicationReceivedDate"].ToString()))
                    {
                        orderVo.ApplicationReceivedDate = DateTime.Parse(dr["CO_ApplicationReceivedDate"].ToString());
                    }
                    else
                        orderVo.ApplicationReceivedDate = DateTime.MinValue;
                    mforderVo.portfolioId = int.Parse(dr["CP_portfolioId"].ToString());
                    orderVo.PaymentMode = dr["XPM_PaymentModeCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["CO_ChequeNumber"].ToString()))
                        orderVo.ChequeNumber = dr["CO_ChequeNumber"].ToString();
                    else
                        orderVo.ChequeNumber = "";
                    if (!string.IsNullOrEmpty(dr["CO_PaymentDate"].ToString()))
                        orderVo.PaymentDate = DateTime.Parse(dr["CO_PaymentDate"].ToString());
                    else
                        orderVo.PaymentDate = DateTime.MinValue;

                    if (!string.IsNullOrEmpty(dr["AAC_AdviserAgentId"].ToString()))
                    {
                        orderVo.AgentId = Convert.ToInt32(dr["AAC_AdviserAgentId"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dr["AAC_AgentCode"].ToString()))
                    {
                        mforderVo.AgentCode = dr["AAC_AgentCode"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dr["CMFOD_FutureTriggerCondition"].ToString()))
                        mforderVo.FutureTriggerCondition = dr["CMFOD_FutureTriggerCondition"].ToString();
                    else
                        mforderVo.FutureTriggerCondition = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_FutureExecutionDate"].ToString()))
                        mforderVo.FutureExecutionDate = DateTime.Parse(dr["CMFOD_FutureExecutionDate"].ToString());
                    else
                        mforderVo.FutureExecutionDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(dr["PASP_SchemePlanSwitch"].ToString()))
                        mforderVo.SchemePlanSwitch = int.Parse(dr["PASP_SchemePlanSwitch"].ToString());
                    else
                        mforderVo.SchemePlanSwitch = 0;
                    if (!string.IsNullOrEmpty(dr["CB_CustBankAccId"].ToString()))
                        orderVo.CustBankAccId = int.Parse(dr["CB_CustBankAccId"].ToString());
                    else
                        orderVo.CustBankAccId = 0;
                    if (!string.IsNullOrEmpty(dr["CMFOD_BankName"].ToString()))
                        mforderVo.BankName = dr["CMFOD_BankName"].ToString();
                    else
                        mforderVo.BankName = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_BranchName"].ToString()))
                        mforderVo.BranchName = dr["CMFOD_BranchName"].ToString();
                    else
                        mforderVo.BranchName = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine1"].ToString()))
                        mforderVo.AddrLine1 = dr["CMFOD_AddrLine1"].ToString();
                    else
                        mforderVo.AddrLine1 = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine2"].ToString()))
                        mforderVo.AddrLine2 = dr["CMFOD_AddrLine2"].ToString();
                    else
                        mforderVo.AddrLine2 = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine3"].ToString()))
                        mforderVo.AddrLine3 = dr["CMFOD_AddrLine3"].ToString();
                    else
                        mforderVo.AddrLine3 = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_City"].ToString()))
                        mforderVo.City = dr["CMFOD_City"].ToString();
                    else
                        mforderVo.City = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_State"].ToString()))
                        mforderVo.State = dr["CMFOD_State"].ToString();
                    else
                        mforderVo.State = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_Country"].ToString()))
                        mforderVo.Country = dr["CMFOD_Country"].ToString();
                    else
                        mforderVo.Country = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_PinCode"].ToString()))
                        mforderVo.Pincode = dr["CMFOD_PinCode"].ToString();
                    else
                        mforderVo.Pincode = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_LivingScince"].ToString()))
                        mforderVo.LivingSince = DateTime.Parse(dr["CMFOD_LivingScince"].ToString());
                    else
                        mforderVo.LivingSince = DateTime.MinValue;

                    if (!string.IsNullOrEmpty(dr["XF_FrequencyCode"].ToString()))
                        mforderVo.FrequencyCode = dr["XF_FrequencyCode"].ToString();
                    else
                        mforderVo.FrequencyCode = "";
                    if (!string.IsNullOrEmpty(dr["CMFOD_StartDate"].ToString()))
                        mforderVo.StartDate = DateTime.Parse(dr["CMFOD_StartDate"].ToString());
                    else
                        mforderVo.StartDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(dr["CMFOD_EndDate"].ToString()))
                        mforderVo.EndDate = DateTime.Parse(dr["CMFOD_EndDate"].ToString());
                    else
                        mforderVo.EndDate = DateTime.MinValue;

                    if (!string.IsNullOrEmpty(dr["CMFOD_Units"].ToString()))
                        mforderVo.Units = double.Parse(dr["CMFOD_Units"].ToString());
                    else
                        mforderVo.Units = 0;

                    if (!string.IsNullOrEmpty(dr["CMFOD_ARNNo"].ToString()))
                    {
                        mforderVo.ARNNo = Convert.ToString(dr["CMFOD_ARNNo"]);
                    }

                }
                Session["orderVo"] = orderVo;
                Session["mforderVo"] = mforderVo;
            }
        }
        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {

            RadComboBox ddlAction = (RadComboBox)sender;
            GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            int selectedRow = gvr.ItemIndex + 1;

            string action = "";
            string orderId = gvSIPBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderId"].ToString();
            string customerId = gvSIPBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString();
            string assetGroupCode = gvSIPBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["PAG_AssetGroupCode"].ToString();

            if (ddlAction.SelectedItem.Value.ToString() == "Edit")
            {
                action = "Edit";
                if (assetGroupCode == "MF")
                {
                    //int mfOrderId = int.Parse(orderId);
                    //GetMFOrderDetails(mfOrderId);

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderSIPTransType','strAction=" + ddlAction.SelectedItem.Value.ToString() + "&orderId=" + orderId + "&customerId=" + customerId + "');", true);   

                }

            }

            if (ddlAction.SelectedItem.Value.ToString() == "Cancel")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
            }
        }
           
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
            gvSIPBookMIS.ExportSettings.FileName = "OrderBook Details";
            gvSIPBookMIS.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvSIPBookMIS.MasterTableView.ExportToExcel();
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