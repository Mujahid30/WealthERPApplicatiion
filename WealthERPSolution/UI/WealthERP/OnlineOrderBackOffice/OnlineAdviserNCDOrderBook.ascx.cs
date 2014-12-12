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
                    ViewState["OrderId"] = int.Parse(Request.QueryString["orderId"].ToString());
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
            if (Request.QueryString["orderId"] != null)
            {

                dtNCDOrder = onlineNCDBackOfficeBo.GetAdviserNCDOrderBook(advisorVo.advisorId, 0, "0", fromDate, toDate, int.Parse(ViewState["OrderId"].ToString()));

            }
            else
            {
                if (txtOrderFrom.SelectedDate != null)
                    fromDate = DateTime.Parse(txtOrderFrom.SelectedDate.ToString());
                if (txtOrderTo.SelectedDate != null)
                    toDate = DateTime.Parse(txtOrderTo.SelectedDate.ToString());
                dtNCDOrder = onlineNCDBackOfficeBo.GetAdviserNCDOrderBook(advisorVo.advisorId, Convert.ToInt32(ddlIssueName.SelectedValue.ToString()), hdnOrderStatus.Value, fromDate, toDate,0);
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
            else
            {
                ibtExportSummary.Visible = false;
                gvNCDOrderBook.DataSource = dtNCDOrder;
                gvNCDOrderBook.DataBind();
                pnlGrid.Visible = true;
            }
        }
        protected void gvNCDOrderBook_UpdateCommand(object source, GridCommandEventArgs e)
        {
            string strRemark = string.Empty;
            int IsMarked = 0;
            bool lbResult = false;
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditableItem editItem = e.Item as GridEditableItem;
                TextBox txtRemark = (TextBox)e.Item.FindControl("txtRemark");
                strRemark = txtRemark.Text;
                LinkButton buttonEdit = editItem["MarkAsReject"].Controls[0] as LinkButton;
             //   Label extractStepCode = editItem["WES_Code"].Controls[1] as Label;
                Int32 orderId = Convert.ToInt32(gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
                string extractionStepCode = gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WES_Code"].ToString();
                if (extractionStepCode == string.Empty)
                {
                    string AcntId = gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["C_CustCode"].ToString();
                    double AmountPayable = Convert.ToDouble(gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["BBAmounttoinvest"].ToString());



                    lbResult = BoOnlineBondOrder.cancelBondsBookOrder(orderId, 2, txtRemark.Text);
                    BoOnlineBondOrder.DebitRMSUserAccountBalance(AcntId, AmountPayable, 0);
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
                
                
                //IsMarked = mforderBo.MarkAsReject(orderId, txtRemark.Text);
                //BindMISGridView();

            }
        }
        protected void gvNCDOrderBook_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
          
            
        }
        public void gvNCDOrderBook_OnItemDataCommand(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                Boolean isCancel=false;
                GridDataItem dataItem = e.Item as GridDataItem;
                LinkButton lbtnMarkAsReject = dataItem["MarkAsReject"].Controls[0] as LinkButton;
                string OrderStepCode = gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WOS_OrderStep"].ToString();
                if(gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIM_IsCancelAllowed"].ToString()!="")
                {
                 isCancel=Convert.ToBoolean(gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIM_IsCancelAllowed"].ToString());

                    }
              //  string extractionStepCode = gvNCDOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WES_COde"].ToString();
                if (OrderStepCode == "INPROCESS" && isCancel != false)
                {                   
                        lbtnMarkAsReject.Visible = true;
                   
                }
                else
                {
                    lbtnMarkAsReject.Visible = false;
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
                    DataTable dtNCDOrderBook = onlineNCDBackOfficeBo.GetAdviserNCDOrderSubBook(advisorVo.advisorId, strIssuerId, orderId);
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
                    if( PnlChild.Visible == true)
                    PnlChild.Visible = false;
                    button.Text = "+";
                }
                button1.Text = "+";
            }
           
        }
    }
}