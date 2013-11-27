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

namespace WealthERP.OnlineOrderManagement
{
    public partial class NCDIssueBooks : System.Web.UI.UserControl
    {
        UserVo userVo;
        CustomerVo customerVo = new CustomerVo();
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        AdvisorVo advisorVo;
        int customerId;
        DateTime fromDate;
        DateTime toDate;
        //string CustId = "7709";
        BoOnlineOrderManagement.OnlineBondOrderBo BoOnlineBondOrder = new BoOnlineOrderManagement.OnlineBondOrderBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            userVo = (UserVo)Session[SessionContents.UserVo];
            customerVo = (CustomerVo)Session["customerVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];

            if (!IsPostBack)
            {
                fromDate = DateTime.Now.AddMonths(-1);
                txtOrderFrom.SelectedDate = fromDate.Date;
                Cache.Remove("NCDBookList" + advisorVo.advisorId.ToString());
                txtOrderTo.SelectedDate = DateTime.Now;
                BindOrderStatus();
                if (Request.QueryString["customerId"] != null)
                {
                    customerId = int.Parse(Request.QueryString["customerId"].ToString());
                    hdnOrderStatus.Value = "0";
                    BindBBGV(customerId);
                }
                //else
                //{
                //    //CustId = Session["CustId"].ToString();
                //    BindBBGV(customerVo.CustomerId);
                //}
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
        protected void btnViewOrder_Click(object sender, EventArgs e)
        {
            SetParameter();
            BindBBGV(customerVo.CustomerId);
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
        protected void BindBBGV(int customerId)
        {
            if (txtOrderFrom.SelectedDate != null)
                fromDate = DateTime.Parse(txtOrderFrom.SelectedDate.ToString());
            if (txtOrderTo.SelectedDate != null)
                toDate = DateTime.Parse(txtOrderTo.SelectedDate.ToString());
            DataSet dsbondsBook = BoOnlineBondOrder.GetOrderBondBook(customerId, hdnOrderStatus.Value, fromDate, toDate);
            DataTable dtbondsBook = dsbondsBook.Tables[0];
            if (dtbondsBook.Rows.Count > 0)
            {
                if (Cache["NCDBookList" + advisorVo.advisorId.ToString()] == null)
                {
                    Cache.Insert("NCDBookList" + advisorVo.advisorId.ToString(), dtbondsBook);
                }
                else
                {
                    Cache.Remove("NCDBookList" + advisorVo.advisorId.ToString());
                    Cache.Insert("NCDBookList" + advisorVo.advisorId.ToString(), dtbondsBook);
                }
                gvBBList.DataSource = dtbondsBook;
                gvBBList.DataBind();
                ibtExportSummary.Visible = true;
                pnlGrid.Visible = true;
                // pnlGrid.Visible = true;
            }
            else
            {
                ibtExportSummary.Visible = false;
                gvBBList.DataSource = dtbondsBook;
                gvBBList.DataBind();
                pnlGrid.Visible = true;
            }

        }

        protected void ibtExportSummary_OnClick(object sender, ImageClickEventArgs e)
        {
            //DataTable dtCommMgmt = new DataTable();
            //dtCommMgmt = (DataTable)Cache[userVo.UserId.ToString() + "CommissionStructureRule"];
            //if (dtCommMgmt == null)
            //    return;
            //else if (dtCommMgmt.Rows.Count < 1)
            //    return;
            //gvBBList.DataSource = dtCommMgmt;
            //gvBBList.ExportSettings.OpenInNewWindow = true;
            //gvBBList.ExportSettings.IgnorePaging = true;
            //gvBBList.ExportSettings.HideStructureColumns = true;
            //gvBBList.ExportSettings.ExportOnlyData = true;
            //gvBBList.ExportSettings.FileName = "Details";
            //gvBBList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            //gvBBList.MasterTableView.ExportToExcel();
            //BindStructureRuleGrid();
        }

        protected void ddlMenu_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //string sActionName = ((DropDownList)sender).SelectedItem.Text;
            //string sStructId = ((DropDownList)sender).SelectedValue;

            RadComboBox ddlAction = (RadComboBox)sender;
            //GridDataItem item = (GridDataItem)ddlAction.NamingContainer;
            //int structureId = int.Parse(gvBBList.MasterTableView.DataKeyValues[item.ItemIndex]["StructureId"].ToString());
            //string prodType = this.ddProduct.SelectedValue;

            //switch (ddlAction.SelectedValue)
            //{
            //    case "Cancel":
            //        BoOnlineBondOrder.cancelBondsBookOrder("");
            //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('ReceivableSetup','StructureId=1');", true);
            //        break;
            //    default:
            //        return;
            //}
        }

        protected void gvBBList_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            gvBBList.CurrentPageIndex = e.NewPageIndex;
            int rowindex1 = ((GridDataItem)((DropDownList)sender).NamingContainer).RowIndex;

            int rowindex = (rowindex1 / 2) - 1;
            if (Request.QueryString["customerId"] != null)
            {
                customerId = int.Parse(Request.QueryString["customerId"].ToString());
                BindjointNominee(customerId);
            }
            else
            {
                customerId = customerVo.CustomerId;
                BindjointNominee(customerId);
            }
        }
        protected void BindjointNominee(int customerId)
        {
            DataSet dsjointNominee = BoOnlineBondOrder.GetNomineeJointHolder(customerId);

            if (dsjointNominee.Tables[0].Rows.Count > 0)
                ibtExportSummary.Visible = true;
            else
                ibtExportSummary.Visible = false;

            gvBBList.DataSource = dsjointNominee;
            gvBBList.DataBind();

            Cache.Insert(userVo.UserId.ToString() + "NomineeJointHolder", dsjointNominee.Tables[0]);
        }
        protected void btnExpandAll_Click(object sender, EventArgs e)
        {
            DataTable dtIssueDetail;
            int strIssuerId = 0;
            LinkButton buttonlink = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)buttonlink.NamingContainer;
            strIssuerId = int.Parse(gvBBList.MasterTableView.DataKeyValues[gdi.ItemIndex]["AIM_IssueId"].ToString());
            int orderId = int.Parse(gvBBList.MasterTableView.DataKeyValues[gdi.ItemIndex]["CO_OrderId"].ToString());
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
            DataSet ds = BoOnlineBondOrder.GetOrderBondSubBook(customerVo.CustomerId, strIssuerId, orderId);
            dtIssueDetail = ds.Tables[0];
            gvChildDetails.DataSource = dtIssueDetail;
            gvChildDetails.DataBind();
        }
        protected void gvBBList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtIssueDetail;
            dtIssueDetail = (DataTable)Cache["NCDBookList" + advisorVo.advisorId.ToString()];
            if (dtIssueDetail != null)
            {
                gvBBList.DataSource = dtIssueDetail;
            }

        }
        protected void gvChildDetails_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid gvChildDetails = (RadGrid)sender; // Get reference to grid 
            GridDataItem nesteditem = (GridDataItem)gvChildDetails.NamingContainer;
            int strIssuerId = int.Parse(gvBBList.MasterTableView.DataKeyValues[nesteditem.ItemIndex]["AIM_IssueId"].ToString()); // Get the value 
            int orderId = int.Parse(gvBBList.MasterTableView.DataKeyValues[nesteditem.ItemIndex]["CO_OrderId"].ToString());
            DataSet ds = BoOnlineBondOrder.GetOrderBondSubBook(customerVo.CustomerId, strIssuerId, orderId);
            gvChildDetails.DataSource = ds.Tables[0];
        }
        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool lbResult = false;
            string action = string.Empty;
            DropDownList ddlAction = (DropDownList)sender;
            GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            RadGrid gvChildDetails = (RadGrid)gvr.FindControl("gvChildDetails");
            int selectedRow = gvr.ItemIndex + 1;
            int OrderId = int.Parse(gvBBList.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderId"].ToString());
            int IssuerId = int.Parse(gvBBList.MasterTableView.DataKeyValues[selectedRow - 1]["AIM_IssueId"].ToString());
            string Issuername = gvBBList.MasterTableView.DataKeyValues[selectedRow - 1]["Scrip"].ToString();
            //Session["NCDTransact"] = BoOnlineBondOrder.GetNCDTransactOrder(OrderId, IssuerId);
            if (ddlAction.SelectedItem.Value.ToString() == "View")
            {
                action = "View";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueTransact','&OrderId=" + OrderId + "&IssuerId=" + IssuerId + "&Issuername=" + Issuername + "&strAction=" + action + " ');", true);
            }
            if (ddlAction.SelectedItem.Value.ToString() == "Edit")
            {
                action = "Edit";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueTransact','&OrderId=" + OrderId + "&IssuerId=" + IssuerId + "&Issuername=" + Issuername + "&strAction=" + action + " ');", true);
            }
            if (ddlAction.SelectedItem.Value.ToString() == "Cancel")
            {
                lbResult = BoOnlineBondOrder.cancelBondsBookOrder(OrderId, 2);
                if (lbResult == true)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Cancelled Successfully');", true);

                }
                BindBBGV(customerVo.CustomerId);
            }
        }
        protected void gvBBList_UpdateCommand(object source, GridCommandEventArgs e)
        {
            //string strRemark = string.Empty;
            //if (e.CommandName == RadGrid.UpdateCommandName)
            //{
            //    GridEditableItem editItem = e.Item as GridEditableItem;
            //    TextBox txtRemark = (TextBox)e.Item.FindControl("txtRemark");
            //    strRemark = txtRemark.Text;
            //    //LinkButton buttonEdit = editItem["editColumn"].Controls[0] as LinkButton;
            //    Int32 systematicId = Convert.ToInt32(gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CMFSS_SystematicSetupId"].ToString());
            //    OnlineMFOrderBo.UpdateCnacleRegisterSIP(systematicId, 1, strRemark, userVo.UserId);
            //    BindSIPSummaryBook();
            //    //buttonEdit.Enabled = false;
            //}

        }


        public void ibtExport_OnClick(object sender, ImageClickEventArgs e)
        {
            Button Button = (Button)sender;
            GridDataItem gvr = (GridDataItem)Button.NamingContainer;
            RadGrid gvChildDetails = (RadGrid)gvr.FindControl("gvChildDetails");
            gvBBList.ExportSettings.OpenInNewWindow = true;
            gvBBList.ExportSettings.IgnorePaging = true;
            gvBBList.ExportSettings.HideStructureColumns = true;
            gvBBList.ExportSettings.ExportOnlyData = true;
            gvBBList.ExportSettings.FileName = "NCD Order Book";
            gvBBList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvBBList.MasterTableView.ExportToExcel();
            gvChildDetails.ExportSettings.OpenInNewWindow = true;
            gvChildDetails.ExportSettings.IgnorePaging = true;
            gvChildDetails.ExportSettings.HideStructureColumns = true;
            gvChildDetails.ExportSettings.ExportOnlyData = true;
            gvChildDetails.ExportSettings.FileName = "NCD Order Book";
            gvChildDetails.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvChildDetails.MasterTableView.ExportToExcel();
        }
    }
}
