using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using VoUser;
using BoCommon;
using BoWerpAdmin;
using BoOnlineOrderManagement;
using BoOfflineOrderManagement;
using WealthERP.Base;
using VOAssociates;
using VoOps;
using BoOps;

namespace WealthERP.OffLineOrderManagement
{
    public partial class OfflineCustomersNCDOrderBook : System.Web.UI.UserControl
    {
        UserVo userVo;
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        OrderBo orderbo = new OrderBo();
        AdvisorVo advisorVo;
        DateTime fromDate;
        DateTime toDate;
        string userType;
        string UserTitle;
        string AgentCode;
        string agentCode;
        int orderno = 0;
        BoOnlineOrderManagement.OnlineBondOrderBo BoOnlineBondOrder = new BoOnlineOrderManagement.OnlineBondOrderBo();
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        OfflineNCDIPOBackOfficeBo offlineNCDBackOfficeBo = new OfflineNCDIPOBackOfficeBo();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();
            associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
            {
                userType = "advisor";
            }
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
            {
                userType = "bm";

            }
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
            {
                userType = "rm";

            }
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
            {
                userType = "associates";
                if (UserTitle == "SubBroker")
                {
                    associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                    if (associateuserheirarchyVo.AgentCode != null)
                    {
                        AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                    }
                    else
                        AgentCode = "0";
                }
                else
                {
                    associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                    if (associateuserheirarchyVo.AgentCode != null)
                    {
                        AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                    }
                    else
                        AgentCode = "0";
                }
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["orderId"] == null)
                {
                    fromDate = DateTime.Now.AddMonths(-1);
                    txtOrderFrom.SelectedDate = fromDate.Date;
                    txtOrderTo.SelectedDate = DateTime.Now;
                }
                BindOrderStatus();
                BindIssueName();
                if (Request.QueryString["orderId"] != null)
                {
                    orderno = int.Parse(Request.QueryString["orderId"].ToString());
                    ViewState["OrderId"] = orderno;
                    BindAdviserNCCOrderBook();
                    divConditional.Visible = false;
                }
            }
        }
        protected void BindIssueName()
        {
            DataTable dtGetIssueName = new DataTable();
            dtGetIssueName = onlineNCDBackOfficeBo.GetIssueName(advisorVo.advisorId, "FI");
            ddlIssueName.DataSource = dtGetIssueName;
            ddlIssueName.DataValueField = dtGetIssueName.Columns["AIM_IssueId"].ToString();
            ddlIssueName.DataTextField = dtGetIssueName.Columns["AIM_IssueName"].ToString();
            ddlIssueName.DataBind();
            ddlIssueName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
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
                dtOrderStatus.AcceptChanges();
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
            DataTable dtNCDOrder = new DataTable();
            userType = Session[SessionContents.CurrentUserRole].ToString();
            if (Request.QueryString["orderId"] != null)
            {
                dtNCDOrder = offlineNCDBackOfficeBo.GetOfflineCustomerNCDOrderBook(advisorVo.advisorId, 0, "0", fromDate, toDate, userType, AgentCode, int.Parse(ViewState["OrderId"].ToString()), ddlBidType.SelectedValue);
            }
            else
            {
                if (txtOrderFrom.SelectedDate != null)
                    fromDate = DateTime.Parse(txtOrderFrom.SelectedDate.ToString());
                if (txtOrderTo.SelectedDate != null)
                    toDate = DateTime.Parse(txtOrderTo.SelectedDate.ToString());
                dtNCDOrder = offlineNCDBackOfficeBo.GetOfflineCustomerNCDOrderBook(advisorVo.advisorId, Convert.ToInt32(ddlIssueName.SelectedValue.ToString()), hdnOrderStatus.Value, fromDate, toDate, userType, AgentCode, 0, ddlBidType.SelectedValue);
            }
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
        }
        protected void gvNCDOrderBook_UpdateCommand(object source, GridCommandEventArgs e)
        {
            string strRemark = string.Empty;
            bool lbResult = false;
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditableItem editItem = e.Item as GridEditableItem;
                TextBox txtRemark = (TextBox)e.Item.FindControl("txtRemark");
                strRemark = txtRemark.Text;
                LinkButton buttonEdit = editItem["MarkAsReject"].Controls[0] as LinkButton;
                Int32 orderId = Convert.ToInt32(gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
                string extractionStepCode = gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WES_Code"].ToString();
                if (extractionStepCode == string.Empty)
                {
                    string AcntId = gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["C_CustCode"].ToString();
                    double AmountPayable = Convert.ToDouble(gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["BBAmounttoinvest"].ToString());
                    lbResult = BoOnlineBondOrder.cancelBondsBookOrder(orderId, 2, txtRemark.Text);
                    if (lbResult == true)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Cancelled Successfully');", true);
                    }
                    BindAdviserNCCOrderBook();
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Cant be Cancelled as it is Extracted.');", true);
                }
            }
        }
        public void gvNCDOrderBook_OnItemDataCommand(object sender, GridItemEventArgs e)
        {
            Boolean isCancel = false;
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                DropDownList ddlAction = (DropDownList)dataItem.FindControl("ddlAction");
                LinkButton lbtnMarkAsReject = dataItem["MarkAsReject"].Controls[0] as LinkButton;
                ddlAction.Items[1].Enabled = false;
                ddlAction.Items[2].Enabled = false;
                lbtnMarkAsReject.Visible = false;
                string OrderStepCode = gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WOS_OrderStep"].ToString();
                if (gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIM_IsCancelAllowed"].ToString() != string.Empty)
                    isCancel = Convert.ToBoolean(gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIM_IsCancelAllowed"].ToString());
                string authenticated = gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_IsAuthenticated"].ToString();
                DateTime closeDateTime = Convert.ToDateTime(gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["BBEndDate"].ToString());
                if (OrderStepCode == "INPROCESS" && isCancel != false)
                {
                    lbtnMarkAsReject.Visible = true;
                    ddlAction.Items[1].Enabled = true;
                    ddlAction.Items[2].Enabled = true;
                }
                if (OrderStepCode == "CANCELLED")
                {
                    ddlAction.Items[1].Enabled = false;
                    ddlAction.Items[2].Enabled = false;
                }
                if (OrderStepCode == "EXECUTED")
                {
                    ddlAction.Items[1].Enabled = true;
                    ddlAction.Items[2].Enabled = false;
                    ddlAction.ToolTip = "Order Cannot Be Modified in Executed Status";
                }
                if ((OrderStepCode == "REJECTED") || (OrderStepCode == "ACCEPTED"))
                {
                    ddlAction.Items[1].Enabled = true;
                    ddlAction.Items[2].Enabled = false;
                }
                if (OrderStepCode == "ORDERED")
                {

                    if (DateTime.Now > closeDateTime)
                    {
                        lbtnMarkAsReject.Visible = true;
                        ddlAction.Items[1].Enabled = true;
                        ddlAction.Items[2].Enabled = false;

                    }
                    else
                    {
                        ddlAction.Items[1].Enabled = true;
                        ddlAction.Items[2].Enabled = true;
                        lbtnMarkAsReject.Visible = false;
                    }
                }
            }
        }
        protected void btnExpandAll_Click(object sender, EventArgs e)
        {

            int count = gvNCDOrderBook.MasterTableView.Items.Count;
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
            DataTable dtNCDOrderBook = offlineNCDBackOfficeBo.GetAdviserNCDOrderSubBook(advisorVo.advisorId, strIssuerId, orderId);
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
        protected void btnExpand_Click(object sender, EventArgs e)
        {
            LinkButton button1 = (LinkButton)sender;
            if (button1.Text == "+")
            {
                foreach (GridDataItem gvr in this.gvNCDOrderBook.Items)
                {
                    DataTable dtIssueDetail;
                    int strIssuerId = 0;
                    LinkButton button = (LinkButton)gvr.FindControl("lbDetails");
                    RadGrid gvChildDetails = (RadGrid)gvr.FindControl("gvChildDetails");
                    Panel PnlChild = (Panel)gvr.FindControl("pnlchild");
                    strIssuerId = int.Parse(gvNCDOrderBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["AIM_IssueId"].ToString());
                    int orderId = int.Parse(gvNCDOrderBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["CO_OrderId"].ToString());
                    DataTable dtNCDOrderBook = offlineNCDBackOfficeBo.GetAdviserNCDOrderSubBook(advisorVo.advisorId, strIssuerId, orderId);
                    dtIssueDetail = dtNCDOrderBook;
                    gvChildDetails.DataSource = dtIssueDetail;
                    gvChildDetails.DataBind();
                    if (PnlChild.Visible == false)
                    {
                        PnlChild.Visible = true;
                        button.Text = "-";
                    }
                }
                button1.Text = "-";
            }
            else
            {
                foreach (GridDataItem gvr in this.gvNCDOrderBook.Items)
                {
                    LinkButton button = (LinkButton)gvr.FindControl("lbDetails");
                    Panel PnlChild = (Panel)gvr.FindControl("pnlchild");
                    if (PnlChild.Visible == true)
                        PnlChild.Visible = false;
                    button.Text = "+";
                }
                button1.Text = "+";
            }
        }
        protected void ddlAction_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAction = (DropDownList)sender;
            GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            Int32 orderId = Convert.ToInt32(gvNCDOrderBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["CO_OrderId"].ToString());
            int associateid = Convert.ToInt32(gvNCDOrderBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["AgenId"].ToString());
            string agentId = gvNCDOrderBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["AAC_AgentCode"].ToString();
            string OrderStepCode = gvNCDOrderBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["WOS_OrderStep"].ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssueTransactOffline", "loadcontrol( 'NCDIssueTransactOffline','action=" + ddlAction.SelectedItem.Value.ToString() + "&orderId=" + orderId + "&associateid=" + associateid + "&agentId=" + agentId + "&OrderStepCode=" + OrderStepCode + "');", true);
        }
    }
}
