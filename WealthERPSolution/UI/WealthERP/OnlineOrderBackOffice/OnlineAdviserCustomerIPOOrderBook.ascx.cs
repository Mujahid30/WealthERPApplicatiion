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
    public partial class OnlineAdviserCustomerIPOOrderBook : System.Web.UI.UserControl
    {
        
        UserVo userVo;       
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        AdvisorVo advisorVo;       
        DateTime fromDate;
        DateTime toDate;
        OnlineIPOBackOfficeBo OnlineIPOBackOfficeBo = new OnlineIPOBackOfficeBo();
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
            DataTable dtIPOOrder;
            dtIPOOrder = OnlineIPOBackOfficeBo.GetAdviserIPOOrderBook(advisorVo.advisorId, hdnOrderStatus.Value, fromDate, toDate);
            if (dtIPOOrder.Rows.Count >= 0)
            {
                if (Cache["IPOBookList" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("IPOBookList" + userVo.UserId.ToString(), dtIPOOrder);
                }
                else
                {
                    Cache.Remove("IPOBookList" + userVo.UserId.ToString());
                    Cache.Insert("IPOBookList" + userVo.UserId.ToString(), dtIPOOrder);
                }
                gvIPOOrderBook.DataSource = dtIPOOrder;
                gvIPOOrderBook.DataBind();
                ibtExportSummary.Visible = true;
                pnlGrid.Visible = true;
            }
            else
            {
                ibtExportSummary.Visible = false;
                gvIPOOrderBook.DataSource = dtIPOOrder;
                gvIPOOrderBook.DataBind();
                pnlGrid.Visible = true;
            }
        }
        protected void gvIPOOrderBook_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //bool lbResult = false;           
              
                //if (e.CommandName == "Cancel")
                //{   int OrderId = int.Parse(gvIPOOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
                //    //lbResult = BoOnlineBondOrder.cancelBondsBookOrder(OrderId, 2);
                //    if (lbResult == true)
                //    {
                //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Cancelled Successfully');", true);
                //    }
                //    
                //}
            
        }
        public void gvIPOOrderBook_OnItemDataCommand(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //GridDataItem dataItem = (GridDataItem)e.Item;
                //string Iscancel = Convert.ToString(gvIPOOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WTS_TransactionStatusCode"]);
               // ImageButton imgCancel = (ImageButton)dataItem.FindControl("imgCancel");
               // if (Iscancel == "Cancelled")
               // {
                 //   imgCancel.Enabled = false;
               // }
            }
        }        
        protected void btnExpandAll_Click(object sender, EventArgs e)
        {
            int strIssuerId = 0;
            LinkButton buttonlink = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)buttonlink.NamingContainer;
            strIssuerId = int.Parse(gvIPOOrderBook.MasterTableView.DataKeyValues[gdi.ItemIndex]["AIM_IssueId"].ToString());
            int orderId = int.Parse(gvIPOOrderBook.MasterTableView.DataKeyValues[gdi.ItemIndex]["CO_OrderId"].ToString());
            RadGrid gvIPODetails = (RadGrid)gdi.FindControl("gvIPODetails");
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
            DataTable dtIPOOrderBook = OnlineIPOBackOfficeBo.GetAdviserIPOOrderSubBook(advisorVo.advisorId, strIssuerId, orderId);
            gvIPODetails.DataSource = dtIPOOrderBook;
            gvIPODetails.DataBind();
        }
        protected void gvIPOOrderBook_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtIPOOrderBook;
            dtIPOOrderBook = (DataTable)Cache["IPOBookList" + userVo.UserId.ToString()];
            if (dtIPOOrderBook != null)
            {
                gvIPOOrderBook.DataSource = dtIPOOrderBook;
            }

        }       
        public void ibtExport_OnClick(object sender, ImageClickEventArgs e)
        {
            gvIPOOrderBook.MasterTableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
            gvIPOOrderBook.ExportSettings.OpenInNewWindow = true;
            gvIPOOrderBook.ExportSettings.IgnorePaging = true;
            gvIPOOrderBook.ExportSettings.HideStructureColumns = true;
            gvIPOOrderBook.ExportSettings.ExportOnlyData = true;
            gvIPOOrderBook.ExportSettings.FileName = "IPO Order Book";
            gvIPOOrderBook.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvIPOOrderBook.MasterTableView.ExportToExcel();

        }
        }
    }
