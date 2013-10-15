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
            dsSIPBookMIS = OnlineMFOrderBo.GetSIPBookMIS(customerId, int.Parse(hdnAccount.Value), fromDate, toDate);
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