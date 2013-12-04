using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using VoUser;
using BoOnlineOrderManagement;
using WealthERP.Base;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineAdviserNCDOrderBook : System.Web.UI.UserControl
    {
        UserVo userVo;       
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        AdvisorVo advisorVo;       
        DateTime fromDate;
        DateTime toDate;
        BoOnlineOrderManagement.OnlineBondOrderBo BoOnlineBondOrder = new BoOnlineOrderManagement.OnlineBondOrderBo();
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            userVo = (UserVo)Session[SessionContents.UserVo];           
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                fromDate = DateTime.Now.AddMonths(-1);
                txtOrderFrom.SelectedDate = fromDate.Date;
                txtOrderTo.SelectedDate = DateTime.Now;
                BindOrderStatus();
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
        protected void btnViewOrder_Click(object sender, EventArgs e)
        {
            SetParameter();
            BindAdviserNCCOrderBook();
        }
        protected void BindAdviserNCCOrderBook()
        {
            if (txtOrderFrom.SelectedDate != null)
                fromDate = DateTime.Parse(txtOrderFrom.SelectedDate.ToString());
            if (txtOrderTo.SelectedDate != null)
                toDate = DateTime.Parse(txtOrderTo.SelectedDate.ToString());
            DataTable dtNCDOrder;
            dtNCDOrder = onlineNCDBackOfficeBo.GetAdviserNCDOrderBook(advisorVo.advisorId, hdnOrderStatus.Value, fromDate, toDate);
            if (dtNCDOrder.Rows.Count >= 0)
            {
                if (Cache["NCDBookList" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("NCDBookList" + userVo.UserId.ToString(), dtNCDOrder);
                }
                else
                {
                    Cache.Remove("NCDBookList" + userVo.UserId.ToString());
                    Cache.Insert("NCDBookList" + userVo.UserId.ToString(), dtNCDOrder);
                }
                gvNCDOrderBook.DataSource = dtNCDOrder;
                gvNCDOrderBook.DataBind();
                ibtExportSummary.Visible = true;
                pnlGrid.Visible = true;
            }
            else
            {
                ibtExportSummary.Visible = false;
                gvNCDOrderBook.DataSource = dtNCDOrder;
                gvNCDOrderBook.DataBind();
                pnlGrid.Visible = true;
            }
        }
        protected void gvNCDOrderBook_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool lbResult = false;           
              
                if (e.CommandName == "Cancel")
                {   int OrderId = int.Parse(gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
                    lbResult = BoOnlineBondOrder.cancelBondsBookOrder(OrderId, 2);
                    if (lbResult == true)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Cancelled Successfully');", true);
                    }
                    BindAdviserNCCOrderBook();
                }
            
        }
        public void gvNCDOrderBook_OnItemDataCommand(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                string Iscancel = Convert.ToString(gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WTS_TransactionStatusCode"]);
                ImageButton imgCancel = (ImageButton)dataItem.FindControl("imgCancel");
                if (Iscancel == "Cancelled")
                {
                    imgCancel.Enabled = false;
                }
            }
        }
        //protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    bool lbResult = false;
        //    string action = string.Empty;
        //    DropDownList ddlAction = (DropDownList)sender;
        //    GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
        //    RadGrid gvChildDetails = (RadGrid)gvr.FindControl("gvChildDetails");
        //    int selectedRow = gvr.ItemIndex + 1;
        //    int OrderId = int.Parse(gvNCDOrderBook.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderId"].ToString());
        //    int IssuerId = int.Parse(gvNCDOrderBook.MasterTableView.DataKeyValues[selectedRow - 1]["AIM_IssueId"].ToString());
        //    string Issuername = gvNCDOrderBook.MasterTableView.DataKeyValues[selectedRow - 1]["Scrip"].ToString();            
        //    if (ddlAction.SelectedItem.Value.ToString() == "View")
        //    {
        //        action = "View";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueTransact','&OrderId=" + OrderId + "&IssuerId=" + IssuerId + "&Issuername=" + Issuername + "&strAction=" + action + " ');", true);
        //    }
        //    if (ddlAction.SelectedItem.Value.ToString() == "Edit")
        //    {
        //        action = "Edit";
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueTransact','&OrderId=" + OrderId + "&IssuerId=" + IssuerId + "&Issuername=" + Issuername + "&strAction=" + action + " ');", true);
        //    }
        //    if (ddlAction.SelectedItem.Value.ToString() == "Cancel")
        //    {
        //        lbResult = BoOnlineBondOrder.cancelBondsBookOrder(OrderId, 2);
        //        if (lbResult == true)
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Cancelled Successfully');", true);
        //        }
        //        BindAdviserNCCOrderBook();
        //        ddlAction.Items.FindByText("Cancel").Attributes.Add("Disabled", "Disabled");
        //    }
        //}
        protected void btnExpandAll_Click(object sender, EventArgs e)
        {
            DataTable dtIssueDetail;
            int strIssuerId = 0;
            LinkButton buttonlink = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)buttonlink.NamingContainer;
            strIssuerId = int.Parse(gvNCDOrderBook.MasterTableView.DataKeyValues[gdi.ItemIndex]["AIM_IssueId"].ToString());
            int orderId = int.Parse(gvNCDOrderBook.MasterTableView.DataKeyValues[gdi.ItemIndex]["CO_OrderId"].ToString());
            RadGrid gvChildDetails = (RadGrid)gdi.FindControl("gvChildDetails");
            Panel PnlChild = (Panel)gdi.FindControl("pnlchild");
            if (PnlChild.Visible == false)
            {
                PnlChild.Visible = true;
                buttonlink.Text = "-";
            }
            else if (PnlChild.Visible == true)
            {
                PnlChild.Visible = false;
                buttonlink.Text = "+";
            }
            DataTable dtNCDOrderBook = onlineNCDBackOfficeBo.GetAdviserNCDOrderSubBook(advisorVo.advisorId, strIssuerId, orderId);
            dtIssueDetail = dtNCDOrderBook;
            gvChildDetails.DataSource = dtIssueDetail;
            gvChildDetails.DataBind();
        }
        protected void gvNCDOrderBook_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtIssueDetail;
            dtIssueDetail = (DataTable)Cache["NCDBookList" + userVo.UserId.ToString()];
            if (dtIssueDetail != null)
            {
                gvNCDOrderBook.DataSource = dtIssueDetail;
            }

        }
        protected void gvChildDetails_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //RadGrid gvChildDetails = (RadGrid)sender; // Get reference to grid 
            //GridDataItem nesteditem = (GridDataItem)gvChildDetails.NamingContainer;
            //int strIssuerId = int.Parse(gvNCDOrderBook.MasterTableView.DataKeyValues[nesteditem.ItemIndex]["AIM_IssueId"].ToString()); // Get the value 
            //int orderId = int.Parse(gvNCDOrderBook.MasterTableView.DataKeyValues[nesteditem.ItemIndex]["CO_OrderId"].ToString());
            //DataSet ds = BoOnlineBondOrder.GetOrderBondSubBook(customerVo.CustomerId, strIssuerId, orderId);
            //gvChildDetails.DataSource = ds.Tables[0];
        }
        public void ibtExport_OnClick(object sender, ImageClickEventArgs e)
        {
            gvNCDOrderBook.MasterTableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
            gvNCDOrderBook.ExportSettings.OpenInNewWindow = true;
            gvNCDOrderBook.ExportSettings.IgnorePaging = true;
            gvNCDOrderBook.ExportSettings.HideStructureColumns = true;
            gvNCDOrderBook.ExportSettings.ExportOnlyData = true;
            gvNCDOrderBook.ExportSettings.FileName = "NCD Order Book";
            gvNCDOrderBook.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvNCDOrderBook.MasterTableView.ExportToExcel();

        }
    }
}