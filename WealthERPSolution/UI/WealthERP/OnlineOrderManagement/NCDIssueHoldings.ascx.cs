using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using VoUser;
using WealthERP.Base;
using System.Data;
using BoCommon;

namespace WealthERP.OnlineOrderManagement
{
    public partial class NCDIssueHoldings : System.Web.UI.UserControl
    {
        UserVo userVo;
        CustomerVo customerVo;
        BoOnlineOrderManagement.OnlineBondOrderBo BoOnlineBondOrder = new BoOnlineOrderManagement.OnlineBondOrderBo();
        DateTime fromDate;
        DateTime toDate;
        AdvisorVo adviserVo = new AdvisorVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            customerVo = (CustomerVo)Session["customerVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                fromDate = DateTime.Now.AddMonths(-1);
                txtOrderFrom.SelectedDate = fromDate.Date;
                txtOrderTo.SelectedDate = DateTime.Now;
                if(Request.QueryString["BondType"]!=null)
                    BindHolding(Request.QueryString["BondType"]);
            }
        }
        protected void lbBuySell_Click(object sender, EventArgs e)
        {
            //int rowindex1 = ((GridDataItem)((LinkButton)sender).NamingContainer).RowIndex;
            //int rowindex = (rowindex1 / 2) - 1;
            //LinkButton lbButton = (LinkButton)sender;
            //GridDataItem item = (GridDataItem)lbButton.NamingContainer;
            //string IssuerId = gvBHList.MasterTableView.DataKeyValues[rowindex]["BHScrip"].ToString();
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueTransact','CustId=" + CustId + "'&'IssuerId=" + IssuerId + "');", true);
        }
        protected void btnViewOrder_Click(object sender, EventArgs e)
        {
            SetParameter();
            BindHolding(Request.QueryString["BondType"]);
        }
        private void SetParameter()
        {
            if (ddlAccount.SelectedIndex != 0)
            {
                hdnAccount.Value = ddlAccount.SelectedValue;
                ViewState["Account"] = hdnAccount.Value;
            }
            else
            {
                hdnAccount.Value = "0";
            }
        }
        protected void BindHolding(string productsubtype)
        {
            if (txtOrderFrom.SelectedDate != null)
                fromDate = DateTime.Parse(txtOrderFrom.SelectedDate.ToString());
            if (txtOrderTo.SelectedDate != null)
                toDate = DateTime.Parse(txtOrderTo.SelectedDate.ToString());
            DataTable dtNCDHoldingOrder;
            //dtNCDHoldingOrder = BoOnlineBondOrder.GetNCDHoldingOrder(customerVo.CustomerId, 0, fromDate, toDate);
            dtNCDHoldingOrder = BoOnlineBondOrder.GetNCDHoldingOrder(customerVo.CustomerId, adviserVo.advisorId, productsubtype);

            if (dtNCDHoldingOrder.Rows.Count >= 0)
            {
                if (Cache["NCDHoldingList" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("NCDHoldingList" + userVo.UserId.ToString(), dtNCDHoldingOrder);
                }
                else
                {
                    Cache.Remove("NCDHoldingList" + userVo.UserId.ToString());
                    Cache.Insert("NCDHoldingList" + userVo.UserId.ToString(), dtNCDHoldingOrder);
                }
                gvBHList.DataSource = dtNCDHoldingOrder;
                gvBHList.DataBind();
                ibtExportSummary.Visible = false;
                Div2.Visible = true;
            }
            else
            {
                ibtExportSummary.Visible = false;
                gvBHList.DataSource = dtNCDHoldingOrder;
                gvBHList.DataBind();
                Div2.Visible = true;
            }
        }
        protected void gvBHList_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Buy" || e.CommandName == "Sell")
            {
                int IssueId = int.Parse(gvBHList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIM_IssueId"].ToString());
                string Issuename = Convert.ToString(gvBHList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Scrip"].ToString());
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueTransact','&customerId=" + customerVo.CustomerId + "&IssueId=" + IssueId + "&Issuename=" + Issuename + "');", true);  

            }
        }
        public void gvBHList_OnItemDataBound(object sender, GridItemEventArgs e)
        {
        }
        protected void gvBHList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtNCDHoldingOrder;
            dtNCDHoldingOrder = (DataTable)Cache["NCDHoldingList" + userVo.UserId.ToString()];
            if (dtNCDHoldingOrder != null)
            {
                gvBHList.DataSource = dtNCDHoldingOrder;
            }
        }
        protected void btnExpandAll_Click(object sender, EventArgs e)
        {
            //DataTable dtIssueDetail;
            int strIssuerId = 0;
            int orderId = 0;
            LinkButton buttonlink = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)buttonlink.NamingContainer;

            strIssuerId = int.Parse(gvBHList.MasterTableView.DataKeyValues[gdi.ItemIndex]["AIM_IssueId"].ToString());
            orderId = int.Parse(gvBHList.MasterTableView.DataKeyValues[gdi.ItemIndex]["CO_OrderId"].ToString());
        
            RadGrid gvchildIssue = (RadGrid)gdi.FindControl("gvChildDetails");
            Panel pnlchild = (Panel)gdi.FindControl("pnlchild");

            if (pnlchild.Visible == false)
            {
                pnlchild.Visible = true;
                buttonlink.Text = "-";
            }
            else if (pnlchild.Visible == true)
            {
                pnlchild.Visible = false;
                buttonlink.Text = "+";
            }
            if (txtOrderFrom.SelectedDate != null)
                fromDate = DateTime.Parse(txtOrderFrom.SelectedDate.ToString());
            if (txtOrderTo.SelectedDate != null)
                toDate = DateTime.Parse(txtOrderTo.SelectedDate.ToString());
            DataTable dtIssueDetail;

            dtIssueDetail = BoOnlineBondOrder.GetNCDHoldingSeriesOrder(customerVo.CustomerId, adviserVo.advisorId, strIssuerId, orderId);
            //dtIssueDetail = dtNCDHoldingOrder.Tables[0];
            gvchildIssue.DataSource = dtIssueDetail;
            gvchildIssue.DataBind();
        }
        protected void gvChildDetails_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid gvchildIssue = (RadGrid)sender; // Get reference to grid 
            GridDataItem nesteditem = (GridDataItem)gvchildIssue.NamingContainer;
            int strIssuerId = int.Parse(gvBHList.MasterTableView.DataKeyValues[nesteditem.ItemIndex]["AIM_IssueId"].ToString()); // Get the value 
            int orderId = int.Parse(gvBHList.MasterTableView.DataKeyValues[nesteditem.ItemIndex]["CO_OrderId"].ToString());
            if (txtOrderFrom.SelectedDate != null)
                fromDate = DateTime.Parse(txtOrderFrom.SelectedDate.ToString());
            if (txtOrderTo.SelectedDate != null)
                toDate = DateTime.Parse(txtOrderTo.SelectedDate.ToString());
            DataTable dtIssueDetail;
            dtIssueDetail = BoOnlineBondOrder.GetNCDHoldingSeriesOrder(customerVo.CustomerId, adviserVo.advisorId, strIssuerId, orderId);
            //dtIssueDetail = dtNCDHoldingOrder.Tables[0];
            gvchildIssue.DataSource = dtIssueDetail;
        }
        protected void ibtExportSummary_OnClick(object sender, ImageClickEventArgs e)
        {
            gvBHList.ExportSettings.OpenInNewWindow = true;
            gvBHList.ExportSettings.IgnorePaging = true;
            gvBHList.ExportSettings.HideStructureColumns = true;
            gvBHList.ExportSettings.ExportOnlyData = true;
            gvBHList.ExportSettings.FileName = "Holding Details";
            gvBHList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvBHList.MasterTableView.ExportToExcel();
            
        }

    }
}