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
        string productsubtype = string.Empty;
        //string CustId = "7709";
        BoOnlineOrderManagement.OnlineBondOrderBo BoOnlineBondOrder = new BoOnlineOrderManagement.OnlineBondOrderBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            customerVo = (CustomerVo)Session["customerVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                fromDate = DateTime.Now.AddMonths(-1);
                txtOrderFrom.SelectedDate = fromDate.Date;

                txtOrderTo.SelectedDate = DateTime.Now;
                BindOrderStatus();
                if (Request.QueryString["BondType"] != null)
                {
                    ViewState["productsubtype"] = Request.QueryString["BondType"];
                    BindIssueName(Request.QueryString["BondType"]);
                }
                if (Request.QueryString["customerId"] != null)
                {
                    customerId = int.Parse(Request.QueryString["customerId"].ToString());
                    hdnOrderStatus.Value = "0";
                    BindBBGV(customerId, productsubtype);
                }
                DateTime todate;
                DateTime fromdate;
                if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null && Request.QueryString["BondType"] != null)
                {
                    string action = Request.QueryString["strAction"].ToString();
                    todate = DateTime.Parse(Request.QueryString["todate"].ToString());
                    fromdate = DateTime.Parse(Request.QueryString["fromdate"].ToString());
                    string status = Request.QueryString["status"].ToString();
                    productsubtype = Request.QueryString["BondType"];
                    hdnOrderStatus.Value = status;
                    // ddlOrderStatus.SelectedValue = status;
                    txtOrderFrom.SelectedDate = fromdate;
                    txtOrderTo.SelectedDate = todate;
                    // SetParameter();
                    BindBBGV(customerVo.CustomerId, ViewState["productsubtype"].ToString());
                }
                //else
                //{
                //    //CustId = Session["CustId"].ToString();
                //    BindBBGV(customerVo.CustomerId);
                //}
            }
        }
        protected void BindIssueName(string productSubType)
        {
            DataTable dt;
            dt = BoOnlineBondOrder.GetCustomerIssueName(customerVo.CustomerId, productSubType);
            ddlIssueName.DataSource = dt;
            ddlIssueName.DataValueField = dt.Columns["AIM_IssueId"].ToString();
            ddlIssueName.DataTextField = dt.Columns["AIM_IssueName"].ToString();
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
        protected void btnViewOrder_Click(object sender, EventArgs e)
        {
            SetParameter();
            BindBBGV(customerVo.CustomerId, ViewState["productsubtype"].ToString());
        }
        /// <summary>
        /// Get Bind Orderstatus
        /// </summary>
        private void BindOrderStatus()
        {
            ddlOrderStatus.Items.Clear();
            DataSet dsOrderStatus;
            DataTable dtOrderStatus;
            if (Request.QueryString["BondType"] == "FISDSD")
                lblProductType.Text = "NCD Book";
            else if (Request.QueryString["BondType"] == "FITFTF")
                lblProductType.Text = "TAX Free Book";
            else if (Request.QueryString["BondType"] == "FISSGB")
                lblProductType.Text = "SGB Book";

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
        protected void BindBBGV(int customerId, string subtype)
        {
            if (txtOrderFrom.SelectedDate != null)
                fromDate = DateTime.Parse(txtOrderFrom.SelectedDate.ToString());
            if (txtOrderTo.SelectedDate != null)
                toDate = DateTime.Parse(txtOrderTo.SelectedDate.ToString());
            DataSet dsbondsBook = BoOnlineBondOrder.GetOrderBondBook(customerId, Convert.ToInt32(ddlIssueName.SelectedValue.ToString()), hdnOrderStatus.Value, fromDate, toDate, advisorVo.advisorId, subtype);
            DataTable dtbondsBook = dsbondsBook.Tables[0];
            if (dtbondsBook.Rows.Count > 0)
            {

                var page = 0;
                gvBBList.CurrentPageIndex = page;
                gvBBList.DataSource = dtbondsBook;
                gvBBList.DataBind();
                ibtExportSummary.Visible = true;
                Div2.Visible = true;
                // pnlGrid.Visible = true;
            }
            else
            {
                ibtExportSummary.Visible = false;
                gvBBList.DataSource = dtbondsBook;
                gvBBList.DataBind();
                Div2.Visible = true;
            }
            if (Cache[userVo.UserId.ToString() + "NCDBookList"] != null)
                Cache.Remove(userVo.UserId.ToString() + "NCDBookList");
            Cache.Insert(userVo.UserId.ToString() + "NCDBookList", dtbondsBook);

            //if (Cache["NCDBookList" + advisorVo.advisorId.ToString()] != null)
            //{
            //    Cache.Remove("NCDBookList" + advisorVo.advisorId.ToString());
            //    Cache.Insert("NCDBookList" + advisorVo.advisorId.ToString(), dtbondsBook);
            //}


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
                buttonlink.Text = "Bid Details";
            }
            else if (PnlChild.Visible == true)
            {
                PnlChild.Visible = false;
                buttonlink.Text = "Bid Details";
            }
            DataSet ds = BoOnlineBondOrder.GetOrderBondSubBook(customerVo.CustomerId, strIssuerId, orderId);
            dtIssueDetail = ds.Tables[0];
            gvChildDetails.DataSource = dtIssueDetail;
            gvChildDetails.DataBind();
        }
        protected void gvBBList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtIssueDetail = new DataTable();
            dtIssueDetail = (DataTable)Cache[userVo.UserId.ToString() + "NCDBookList"];
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
        //protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    bool lbResult = false;
        //    string action = string.Empty;
        //    DropDownList ddlAction = (DropDownList)sender;
        //    GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
        //    RadGrid gvChildDetails = (RadGrid)gvr.FindControl("gvChildDetails");
        //    int selectedRow = gvr.ItemIndex + 1;
        //    int OrderId = int.Parse(gvBBList.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderId"].ToString());
        //    int IssuerId = int.Parse(gvBBList.MasterTableView.DataKeyValues[selectedRow - 1]["AIM_IssueId"].ToString());
        //    string Issuername = gvBBList.MasterTableView.DataKeyValues[selectedRow - 1]["Scrip"].ToString();
        //    //Session["NCDTransact"] = BoOnlineBondOrder.GetNCDTransactOrder(OrderId, IssuerId);
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
        //        BindBBGV(customerVo.CustomerId);
        //        ddlAction.Items.FindByText("Cancel").Attributes.Add("Disabled", "Disabled");
        //    }
        //}
        //protected void gvBBList_UpdateCommand(object source, GridCommandEventArgs e)
        //{
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

        //}
        protected void gvBBList_UpdateCommand(object source, GridCommandEventArgs e)
        {
            string strRemark = string.Empty;
            int IsMarked = 0;
            bool lbResult = false;
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditableItem editItem = e.Item as GridEditableItem;
                TextBox txtRemark = (TextBox)e.Item.FindControl("txtRemark");
                strRemark = txtRemark.Text;
                //LinkButton buttonEdit = editItem["MarkAsReject"].Controls[0] as LinkButton;
                Int32 orderId = Convert.ToInt32(gvBBList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
                //string extractionStepCode = gvBBList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WES_COde"].ToString();
                string extractionStepCode = gvBBList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WES_Code"].ToString();
                if (extractionStepCode == string.Empty)
                {
                    double AmountPayable = Convert.ToDouble(gvBBList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["BBAmounttoinvest"].ToString());

                    lbResult = BoOnlineBondOrder.cancelBondsBookOrder(orderId, 2, txtRemark.Text);
                    //BoOnlineBondOrder.DebitRMSUserAccountBalance(customerVo.AccountId, AmountPayable, 0);
                    if (lbResult == true)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Cancelled Successfully');", true);
                    }
                    BindBBGV(customerVo.CustomerId, ViewState["productsubtype"].ToString());
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Cant be Cancelled as it is Extracted.');", true);

                }
                //IsMarked = mforderBo.MarkAsReject(orderId, txtRemark.Text);
                //BindMISGridView();

            }
        }
        protected void gvBBList_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool lbResult = false;
            int OrderId = 0;
            int IssuerId = 0;
            string Issuername = string.Empty;
            string action;
            if (e.CommandName == "View" || e.CommandName == "Edit" || e.CommandName == "Cancel")
            {
                OrderId = int.Parse(gvBBList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
                IssuerId = int.Parse(gvBBList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIM_IssueId"].ToString());
                Issuername = gvBBList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Scrip"].ToString();
            }
            if (e.CommandName == "View")
            {
                action = "View";
                if (Session["PageDefaultSetting"] != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('NCDIssueTransact','&OrderId=" + OrderId + "&IssuerId=" + IssuerId + "&Issuername=" + Issuername + "&strAction=" + action +
                        "&status=" + ddlOrderStatus.SelectedValue.ToString() + "&fromdate=" + txtOrderFrom.SelectedDate + "&todate=" + txtOrderTo.SelectedDate + "&BondType=" + ViewState["productsubtype"].ToString() + "');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueTransact','&OrderId=" + OrderId + "&IssuerId=" + IssuerId + "&Issuername=" + Issuername + "&strAction=" + action +
                        "&status=" + ddlOrderStatus.SelectedValue.ToString() + "&fromdate=" + txtOrderFrom.SelectedDate + "&todate=" + txtOrderTo.SelectedDate + "&BondType=" + ViewState["productsubtype"].ToString() + " ');", true);
                }
            }
            if (e.CommandName == "Edit")
            {
                //action = "Edit";
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueTransact','&OrderId=" + OrderId + "&IssuerId=" + IssuerId + "&Issuername=" + Issuername + "&strAction=" + action + " ');", true);
            }
            if (e.CommandName == "Cancel")
            {
                //lbResult = BoOnlineBondOrder.cancelBondsBookOrder(OrderId, 2,"");
                //if (lbResult == true)
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Cancelled Successfully');", true);
                //}
                //BindBBGV(customerVo.CustomerId);
            }
        }

        public void gvBBList_OnItemDataCommand(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                LinkButton lbtnMarkAsReject = (LinkButton)dataItem.FindControl("lbtnMarkAsReject");
                //dataItem["MarkAsReject"].Controls[0] as LinkButton;
                string OrderStepCode = gvBBList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WOS_OrderStepCode"].ToString();
                string iscancil = gvBBList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIM_IsCancelAllowed"].ToString();
                if (OrderStepCode.Trim() == "AL" && iscancil != "False")
                {
                    lbtnMarkAsReject.Visible = true;
                }
                else
                {
                    lbtnMarkAsReject.Visible = false;
                }
            }

        }
        public void ibtExport_OnClick(object sender, ImageClickEventArgs e)
        {

            DataTable dtd = CreateIPOBookDataTable();
            DataTable dts = (DataTable)Cache[userVo.UserId.ToString() + "NCDBookList"];

            System.Data.DataView view = new System.Data.DataView(dts);
            System.Data.DataTable selected =
                    view.ToTable("Selected", false, "Scrip", "CO_OrderDate", "CO_OrderId", "AIM_MaxApplNo", "BBStartDate", "BBEndDate", "BBAmounttoinvest", "WOS_OrderStep");

            foreach (DataRow sourcerow in dts.Rows)
            {
                DataRow destRow = dtd.NewRow();
                destRow["Issue Name"] = sourcerow["Scrip"];
                destRow["Transaction Date"] = sourcerow["CO_OrderDate"];
                destRow["Transaction No"] = sourcerow["CO_OrderId"];
                destRow["Application No"] = sourcerow["AIM_MaxApplNo"];
                destRow["Start Date"] = sourcerow["BBStartDate"];
                destRow["End Date"] = sourcerow["BBEndDate"];
                destRow["Amount Invested"] = sourcerow["BBAmounttoinvest"];
                destRow["Status"] = sourcerow["WOS_OrderStep"];
                dtd.Rows.Add(destRow);
            }
            if (dtd.Rows.Count > 0)
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "CustomerNCD/SGBOrderBook.xls"));
                Response.ContentType = "application/ms-excel";

                string str = string.Empty;
                foreach (DataColumn dtcol in dtd.Columns)
                {
                    Response.Write(str + dtcol.ColumnName);
                    str = "\t";
                }
                Response.Write("\n");
                foreach (DataRow dr in dtd.Rows)
                {
                    str = "";
                    for (int j = 0; j < dtd.Columns.Count; j++)
                    {
                        Response.Write(str + Convert.ToString(dr[j]));
                        str = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
        }
        protected DataTable CreateIPOBookDataTable()
        {
            DataTable dtIPOOrderBook = new DataTable();
            dtIPOOrderBook.Columns.Add("Transaction Date", typeof(DateTime));
            dtIPOOrderBook.Columns.Add("Transaction No");
            dtIPOOrderBook.Columns.Add("Issue Name");
            dtIPOOrderBook.Columns.Add("Application No");
            dtIPOOrderBook.Columns.Add("Start Date", typeof(DateTime));
            dtIPOOrderBook.Columns.Add("End Date", typeof(DateTime));
            dtIPOOrderBook.Columns.Add("Amount Invested", typeof(double));
            dtIPOOrderBook.Columns.Add("Status");
            return dtIPOOrderBook;

        }
        public void gvChildDetails_OnItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                System.Web.UI.HtmlControls.HtmlGenericControl nomineedetails = (System.Web.UI.HtmlControls.HtmlGenericControl)dataItem.FindControl("nomineedetails");
                if (Request.QueryString["BondType"] == "FISSGB")
                    nomineedetails.Visible = true;
                else
                    nomineedetails.Visible = false;
            }
        }

    }
}
