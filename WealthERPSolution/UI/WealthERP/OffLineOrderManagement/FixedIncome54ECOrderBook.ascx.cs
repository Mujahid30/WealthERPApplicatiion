using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using VoUser;
using WealthERP.Base;
using System.IO;
using System.Data;
using BoCommon;
using VoUser;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using BoOfflineOrderManagement;
using BoOnlineOrderManagement;
using VOAssociates;
namespace WealthERP.OffLineOrderManagement
{
    public partial class FixedIncome54ECOrderBook : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo;
        CustomerVo customerVO = new CustomerVo();
        UserVo userVo;
        string userType;
        OfflineBondOrderBo OfflineBondOrderBo = new OfflineBondOrderBo();

        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        DateTime fromDate, toDate;
        AssociatesUserHeirarchyVo associateuserheirarchyVo;
        string UserTitle;
        string AgentCode;
        int orderNo = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVO = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            userType = Session[SessionContents.CurrentUserRole].ToString();

            //BindIssue();
            associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
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
                fromDate = DateTime.Now.AddMonths(-1);
                txtOrderFrom.SelectedDate = fromDate.Date;
                txtOrderTo.SelectedDate = DateTime.Now;
                BindNcdCategory();
                BindOrderStatus();
                if (Request.QueryString["FDOrderBook"] == "FICDCD")
                {
                    lblProductTypess.Text = "FD Order Book";
                    ddlCategory.Items.FindByValue("FICGCG").Enabled = false;
                }
                else
                {
                    lblProductTypess.Text = "54EC Order Book";
                    ddlCategory.Items.FindByValue("FICDCD").Enabled = false;
                }
                if (Request.QueryString["category"] != null)
                {
                    string categorywise = Request.QueryString["category"].ToString().TrimEnd(' ');
                    ViewState["category"] = categorywise;
                    if (Request.QueryString["orderId"] != null)
                        orderNo = int.Parse(Request.QueryString["orderId"].ToString());
                    //ddlCategory.SelectedValue = categorywise;
                    //ddlAuthenticate.SelectedValue = "0";
                    //BindIssue(categorywise);
                    lblIssue.Visible = true;
                    ddlIssue.Visible = true;
                    trdate.Visible = false;
                    trProduct.Visible = false;
                    BindAdviserFDrderBook();
                }
            }
        }
        protected void BindIssue(string Category)
        {
            DataTable dt = OfflineBondOrderBo.GetFDIddueList(Category);
            ddlIssue.DataSource = dt;
            ddlIssue.DataValueField = "AIM_IssueId";
            ddlIssue.DataTextField = "AIM_IssueName";
            ddlIssue.DataBind();
            ddlIssue.Items.Insert(0, new ListItem("All", "0"));
        }
        private void BindNcdCategory()
        {
            DataTable dtCategory = new DataTable();
            OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
            dtCategory = onlineNCDBackOfficeBo.BindNcdCategory("SubInstrumentCat", "").Tables[0];
            if (dtCategory.Rows.Count > 0)
            {
                ddlCategory.DataSource = dtCategory;
                ddlCategory.DataValueField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlCategory.DataTextField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlCategory.DataBind();
                ddlCategory.Items.FindByText("NCD").Enabled = false;
            }
            ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));

        }
        protected void ddlCategory_Selectedindexchanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue != "Select")
            {
                lblIssue.Visible = true;
                ddlIssue.Visible = true;
                BindIssue(ddlCategory.SelectedValue);
            }
        }
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            BindAdviserFDrderBook();
        }

        protected void BindAdviserFDrderBook()
        {

            DataTable dt54FDOrderBook;
            if (Request.QueryString["category"] != null)
            {
                dt54FDOrderBook = OfflineBondOrderBo.GetFD54IssueOrder(advisorVo.advisorId, DateTime.MinValue, DateTime.Now, "0", 0, userType, AgentCode, ViewState["category"].ToString(), 0, orderNo, userVo.UserId);
            }
            else
            {
                if (txtOrderFrom.SelectedDate != null)
                    fromDate = DateTime.Parse(txtOrderFrom.SelectedDate.ToString());
                if (txtOrderTo.SelectedDate != null)
                    toDate = DateTime.Parse(txtOrderTo.SelectedDate.ToString());

                dt54FDOrderBook = OfflineBondOrderBo.GetFD54IssueOrder(advisorVo.advisorId, fromDate, Convert.ToDateTime(txtOrderTo.SelectedDate), ddlOrderStatus.SelectedValue, int.Parse(ddlIssue.SelectedValue), userType, AgentCode, ddlCategory.SelectedValue, int.Parse(ddlAuthenticate.SelectedValue), orderNo,userVo.UserId);
            }
            if (dt54FDOrderBook.Rows.Count >= 0)
            {
                if (Cache["FDBookList" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("FDBookList" + userVo.UserId.ToString(), dt54FDOrderBook);
                }
                else
                {
                    Cache.Remove("FDBookList" + userVo.UserId.ToString());
                    Cache.Insert("FDBookList" + userVo.UserId.ToString(), dt54FDOrderBook);
                }
                gv54FDOrderBook.DataSource = dt54FDOrderBook;
                gv54FDOrderBook.DataBind();
                imgexportButton.Visible = true;
                pnlGrid.Visible = true;
            }
            else
            {

                gv54FDOrderBook.DataSource = dt54FDOrderBook;
                gv54FDOrderBook.DataBind();
                pnlGrid.Visible = true;
            }
        }

        protected void gv54FDOrderBook_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt54FDOrderBook;
            dt54FDOrderBook = (DataTable)Cache["FDBookList" + userVo.UserId.ToString()];
            if (dt54FDOrderBook != null)
            {
                gv54FDOrderBook.DataSource = dt54FDOrderBook;
            }

        }
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            //  gvIPOOrderBook.MasterTableView.DetailTables[0].HierarchyDefaultExpanded = true;
            gv54FDOrderBook.MasterTableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
            gv54FDOrderBook.ExportSettings.OpenInNewWindow = true;
            gv54FDOrderBook.ExportSettings.IgnorePaging = true;
            gv54FDOrderBook.ExportSettings.HideStructureColumns = true;
            gv54FDOrderBook.ExportSettings.ExportOnlyData = true;
            gv54FDOrderBook.ExportSettings.FileName = "Order Book";
            gv54FDOrderBook.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gv54FDOrderBook.MasterTableView.ExportToExcel();

        }
        protected void ddlAction_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAction = (DropDownList)sender;
            GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            Int32 orderId = Convert.ToInt32(gv54FDOrderBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["CO_OrderId"].ToString());
            Int32 customeId = Convert.ToInt32(gv54FDOrderBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["C_CustomerId"].ToString());
            string agentcode = gv54FDOrderBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["AAC_AgentCode"].ToString();
            string associatename = gv54FDOrderBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["AssociatesName"].ToString();
            string OrderStepCode = Convert.ToString(gv54FDOrderBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["Orderstep"]);
            string CloseDate = Convert.ToString(gv54FDOrderBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["AIM_CloseDate"]);
            int EmpId = Convert.ToInt32(gv54FDOrderBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["AR_StaffCode"].ToString());
            string EmpName = gv54FDOrderBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["AR_FirstName"].ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FixedIncome54ECOrderEntry", "loadcontrol( 'FixedIncome54ECOrderEntry','action=" + ddlAction.SelectedItem.Value.ToString() + "&orderId=" + orderId + "&customeId=" + customeId + "&agentcode=" + agentcode + "&associatename=" + associatename + "&OrderStepCode=" + OrderStepCode + "&CloseDate=" + CloseDate + "&EmpId=" + EmpId + "&EmpName=" + EmpName + " &FDOrder=" + ddlCategory.SelectedValue + "');", true);
        }
        private void BindOrderStatus()
        {
            ddlOrderStatus.Items.Clear();
            DataSet dsOrderStatus;
            DataTable dtOrderStatus;
            dsOrderStatus = OnlineMFOrderBo.GetOrderIssueStatus();
            dtOrderStatus = dsOrderStatus.Tables[0];
            if (dtOrderStatus.Rows.Count > 0)
            {

                for (int i = dtOrderStatus.Rows.Count - 1; i >= 0; i--)
                {
                    if (dtOrderStatus.Rows[i][1].ToString() == "INPROCESS" || dtOrderStatus.Rows[i][1].ToString() == "EXECUTED" )
                        dtOrderStatus.Rows[i].Delete();
                }
                dtOrderStatus.AcceptChanges();
                ddlOrderStatus.DataSource = dtOrderStatus;
                ddlOrderStatus.DataTextField = dtOrderStatus.Columns["WOS_OrderStep"].ToString();
                ddlOrderStatus.DataValueField = dtOrderStatus.Columns["WOS_OrderStepCode"].ToString();
                ddlOrderStatus.DataBind();
            }
            ddlOrderStatus.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void gv54FDOrderBook_UpdateCommand(object source, GridCommandEventArgs e)
        {
           
            bool lbResult = false;
            string strRemark = string.Empty;
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditableItem editItem = e.Item as GridEditableItem;
                TextBox txtRemark = (TextBox)e.Item.FindControl("txtRemark");
                strRemark = txtRemark.Text;
                LinkButton buttonEdit = editItem["MarkAsReject"].Controls[0] as LinkButton;
                Int32 orderId = Convert.ToInt32(gv54FDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
                lbResult = OfflineBondOrderBo.CancelBondsFDBookOrder(orderId, txtRemark.Text,userVo.UserId,false);
                    if (lbResult == true)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Rejected Successfully');", true);
                    }
                    BindAdviserFDrderBook();
            }
        }
        protected void gv54FDOrderBook_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                LinkButton lbtnMarkAsReject = dataItem["MarkAsReject"].Controls[0] as LinkButton;
                DropDownList ddlAction = (DropDownList)dataItem.FindControl("ddlAction");
                string OrderStepCode = Convert.ToString(gv54FDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Orderstep"]);
                string isCancel = Convert.ToString(gv54FDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_IsAuthenticated"]);

                if (OrderStepCode == "ORDERED" && isCancel != "Yes")
                {
                    lbtnMarkAsReject.Visible = true;
                }
                else
                {
                    lbtnMarkAsReject.Visible = false;
                   
                }
                if (OrderStepCode == "REJECTED" || OrderStepCode == "ORDERED")
                {
                    //ddlAction.Items[1].Enabled = false;
                    ddlAction.Items[2].Enabled = false;
                }
                else
                {
                    ddlAction.Items[1].Enabled = true;
                    ddlAction.Items[2].Enabled = true;
                }
                if (ddlCategory.SelectedValue == "FICGCG")
                {
                    gv54FDOrderBook.MasterTableView.GetColumn("CFIOD_TransactionType").Visible = false;
                }
                else
                {
                    gv54FDOrderBook.MasterTableView.GetColumn("CFIOD_TransactionType").Visible = true;
                }
            }
        }
    }
}