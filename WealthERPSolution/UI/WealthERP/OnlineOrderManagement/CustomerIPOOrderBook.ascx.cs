using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using BoUser;
using VoUser;
using WealthERP.Base;
using BoCommon;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using System.Data;

namespace WealthERP.OnlineOrderManagement
{
    public partial class CustomerIPOOrderBook : System.Web.UI.UserControl
    {
        BoOnlineOrderManagement.OnlineBondOrderBo BoOnlineBondOrder = new BoOnlineOrderManagement.OnlineBondOrderBo();
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        OnlineIPOOrderBo onlineIPOOrderBo = new OnlineIPOOrderBo();
        AdvisorVo advisorVo;
        CustomerVo customerVo;
        UserVo userVo;
        DateTime fromDate;
        DateTime toDate;
        int AIMissueId = 0;
        int orderId = 0;
        string orderStep = string.Empty;
        int debitstatus = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["customerVo"];
            fromDate = DateTime.Now.AddMonths(-1);
            txtOrderFrom.SelectedDate = fromDate.Date;
            txtOrderTo.SelectedDate = DateTime.Now;
            BindOrderStatus();
            BindIssueName();
            if (!Page.IsPostBack)
            {
                //if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
                //{
                if (Request.QueryString["AIMissueId"] != null && Request.QueryString["orderId"] != null && Request.QueryString["fromDate"] != null && Request.QueryString["toDate"] != null)
                {
                    AIMissueId = int.Parse(Request.QueryString["AIMissueId"].ToString());
                    orderId = int.Parse(Request.QueryString["orderId"].ToString());
                    fromDate = Convert.ToDateTime(Request.QueryString["fromDate"].ToString());
                    toDate = Convert.ToDateTime(Request.QueryString["toDate"].ToString());
                    trIPOorderbook.Visible = false;
                    //txtOrderFrom.SelectedDate = fromDate;
                    //txtOrderTo.SelectedDate = toDate;
                    //ddlOrderStatus.SelectedValue ="PR";
                    //ddlIssueName.SelectedValue=AIMissueId.ToString();
                    //hdnOrderStatus.Value = "PR";
                    BindCustomerIssueIPOBook();
                    //ddlOrderStatus.Enabled = false;
                    //ddlIssueName.Enabled = false;
                    //txtOrderFrom.Enabled = false;
                    //txtOrderTo.Enabled = false;
                    //btnViewOrder.Enabled = false;

                }
                //  }
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
        /// 
        protected void BindIssueName()
        {
            DataTable dt;
            dt = BoOnlineBondOrder.GetCustomerIssueName(customerVo.CustomerId, "IP");
            ddlIssueName.DataSource = dt;
            ddlIssueName.DataValueField = dt.Columns["AIM_IssueId"].ToString();
            ddlIssueName.DataTextField = dt.Columns["AIM_IssueName"].ToString();
            ddlIssueName.DataBind();
            ddlIssueName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }
        private void BindOrderStatus()
        {
            OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
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
            BindCustomerIssueIPOBook();
        }
        private void BindCustomerIssueIPOBook()
        {
            DataTable dtCustomerIssueIPOBook;

            if (txtOrderFrom.SelectedDate != null)
                fromDate = DateTime.Parse(txtOrderFrom.SelectedDate.ToString());
            if (txtOrderTo.SelectedDate != null)
                toDate = DateTime.Parse(txtOrderTo.SelectedDate.ToString());
            dtCustomerIssueIPOBook = onlineIPOOrderBo.GetCustomerIPOIssueBook(customerVo.CustomerId, Convert.ToInt32(ddlIssueName.SelectedValue.ToString()), ddlOrderStatus.SelectedValue, fromDate, toDate, orderId, out  orderStep);

            if (dtCustomerIssueIPOBook.Rows.Count > 0)
            {

                if (Cache["CustomerIPOIssueBook" + userVo.UserId.ToString()] != null)
                {
                    Cache.Remove("CustomerIPOIssueBook" + userVo.UserId.ToString());
                }
                Cache.Insert("CustomerIPOIssueBook" + userVo.UserId.ToString(), dtCustomerIssueIPOBook);


                RadGridIssueIPOBook.DataSource = dtCustomerIssueIPOBook;
                RadGridIssueIPOBook.DataBind();
                ibtExportSummary.Visible = true;
                Div2.Visible = true;
            }
            else
            {
                RadGridIssueIPOBook.DataSource = dtCustomerIssueIPOBook;
                RadGridIssueIPOBook.DataBind();
                Div2.Visible = true;
            }
        }
        protected void RadGridIssueIPOBook_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtCustomerIssueIPOBook;
            dtCustomerIssueIPOBook = (DataTable)Cache["CustomerIPOIssueBook" + userVo.UserId.ToString()];
            if (dtCustomerIssueIPOBook != null)
            {
                RadGridIssueIPOBook.DataSource = dtCustomerIssueIPOBook;
            }
        }
        public void RadGridIssueIPOBook_OnItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                string Iscancel = Convert.ToString(RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WOS_OrderStep"]);

                LinkButton lnkModify = (LinkButton)dataItem.FindControl("lnkModify");
                LinkButton lnkCancel = (LinkButton)dataItem.FindControl("lnkCancel");
                LinkButton lbtnMarkAsReject = (LinkButton)dataItem.FindControl("lbtnMarkAsReject");
                string extractionStepCode = orderStep;
                bool IsCancelAllowed = Convert.ToBoolean(RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIM_IsCancelAllowed"]);
                bool isModification = Convert.ToBoolean(RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIM_IsModificationAllowed"]);
                string CloseDate = Convert.ToString(RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["IssueEndDateANDTime"]);
                bool IsCancelled = Convert.ToBoolean(RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_IsCancelled"]);
                orderId = int.Parse(RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
                string orderstep = onlineIPOOrderBo.IPOOrderExtractStep(orderId);
                if (IsCancelled == true)
                {
                    lnkModify.Visible = false;
                    lnkCancel.Visible = false;
                    lnkCancel.OnClientClick = "";

                }
                if (orderstep != "" && orderstep != "UB")
                {
                    lnkModify.Visible = false;
                    lnkCancel.Visible = false;
                    lnkCancel.OnClientClick = "";

                }
                if (IsCancelAllowed == false)
                    lnkCancel.Visible = false;
                if (isModification == false)
                {
                    lnkModify.Visible = false;
                }
                if (Iscancel == "CANCELLED" || Iscancel == "EXECUTED" || Iscancel == "ACCEPTED" || Iscancel == "REJECTED")
                {
                    lbtnMarkAsReject.Visible = false;
                    lnkModify.Visible = false;
                    lnkCancel.Visible = false;
                    lnkCancel.OnClientClick = "";
                }

                if (isModification != false && Iscancel != "CANCELLED" && Iscancel != "EXECUTED" && Iscancel != "ACCEPTED" && Iscancel != "REJECTED" && (orderstep == "" || orderstep == "UB"))
                {
                    lnkModify.Visible = true;
                }
                if (IsCancelAllowed != false && IsCancelled != true && Iscancel != "CANCELLED" && Iscancel != "EXECUTED" && Iscancel != "ACCEPTED" && Iscancel != "REJECTED" && (orderstep == "" || orderstep == "UB"))
                {
                    lnkCancel.Visible = true;

                }

                if (Iscancel == "ORDERED" && isModification != false)
                {
                    lnkModify.Visible = true;
                }
                if (Iscancel == "ORDERED" && IsCancelAllowed != false)
                {
                    lnkCancel.Visible = true;

                }
                if (Convert.ToDateTime(CloseDate) <= DateTime.Now)
                {
                    lnkModify.Visible = false;
                    lnkCancel.Visible = false;
                    lbtnMarkAsReject.Visible = false;
                    lnkCancel.OnClientClick = "";

                }
            }
        }
        protected void btnExpandAll_Click(object sender, EventArgs e)
        {
            int strIssuerId = 0;
            LinkButton buttonlink = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)buttonlink.NamingContainer;
            strIssuerId = int.Parse(RadGridIssueIPOBook.MasterTableView.DataKeyValues[gdi.ItemIndex]["AIM_IssueId"].ToString());
            int orderId = int.Parse(RadGridIssueIPOBook.MasterTableView.DataKeyValues[gdi.ItemIndex]["CO_OrderId"].ToString());
            RadGrid gvIPODetails = (RadGrid)gdi.FindControl("gvIPODetails");
            Panel PnlChild = (Panel)gdi.FindControl("pnlchild");
            if (PnlChild.Visible == false)
            {
                PnlChild.Visible = true;

            }
            else if (PnlChild.Visible == true)
            {
                PnlChild.Visible = false;

            }
            DataTable dtIPOOrderBook = onlineIPOOrderBo.GetCustomerIPOIssueSubBook(customerVo.CustomerId, strIssuerId, orderId);
            gvIPODetails.DataSource = dtIPOOrderBook;
            gvIPODetails.DataBind();
        }
        protected void gvIPOOrderBook_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtIPOOrderBook;
            dtIPOOrderBook = (DataTable)Cache["CustomerIPOIssueBook" + userVo.UserId.ToString()];
            if (dtIPOOrderBook != null)
            {
                RadGridIssueIPOBook.DataSource = dtIPOOrderBook;
            }

        }
        protected void RadGridIssueIPOBook_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool lbResult = false;
            string strRemark = string.Empty;
            //if (e.CommandName == RadGrid.UpdateCommandName)
            //{
            //    int OrderId = int.Parse(gvIPOOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
            //    int Amount = int.Parse(gvIPOOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Amount"].ToString());
            //    //lbResult = BoOnlineBondOrder.cancelBondsBookOrder(OrderId, 2);
            //    if (lbResult == true)
            //    {
            //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Cancelled Successfully');", true);
            //    }

            //}
            if (e.CommandName == "Update")
            {
                GridEditableItem editItem = e.Item as GridEditableItem;
                TextBox txtRemark = (TextBox)e.Item.FindControl("txtRemark");
                strRemark = txtRemark.Text;
                //LinkButton buttonEdit = editItem["MarkAsReject"].Controls[0] as LinkButton;
                //   Label extractStepCode = editItem["WES_Code"].Controls[1] as Label;
                Int32 orderId = Convert.ToInt32(RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
                string extractionStepCode = RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WES_Code"].ToString();
                double AmountPayable = Convert.ToDouble(RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Amounttoinvest"].ToString());
                string CloseDate = Convert.ToString(RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["IssueEndDateANDTime"]);
                hdnAmount.Value = AmountPayable.ToString();
                if (DateTime.Now <= Convert.ToDateTime(CloseDate))
                {
                    if (extractionStepCode == string.Empty)
                    {
                        string AcntId = RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["C_CustCode"].ToString();
                        lbResult = BoOnlineBondOrder.cancelBondsBookOrder(orderId, 2, txtRemark.Text);
                        onlineIPOOrderBo.DebitRMSUserAccountBalance(customerVo.AccountId, +AmountPayable, orderId, out debitstatus);
                        //BoOnlineBondOrder.DebitRMSUserAccountBalance(AcntId, AmountPayable, 0);
                        if (lbResult == true)
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Cancelled Successfully');", true);
                        }
                    }
                    else
                    {
                        //string confirmValue = Request.Form["confirm_value"];
                        //if (confirmValue == "Yes")
                        //{
                        lbResult = BoOnlineBondOrder.cancelBondsBookOrder(orderId, 2, txtRemark.Text);
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Differential Amt. will be credited from registrar');", true);
                        //}
                    }
                    BindCustomerIssueIPOBook();
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Cant be Cancelled Issue is closed.');", true);

                }
            }
        }
        public void ibtExport1_OnClick(object sender, ImageClickEventArgs e)
        {
          
            DataTable dtd = CreateIPOBookDataTable();
            DataTable dts = (DataTable)Cache["CustomerIPOIssueBook" + userVo.UserId.ToString()];
            System.Data.DataView view = new System.Data.DataView(dts);
            System.Data.DataTable selected =
                    view.ToTable("Selected", false, "AIM_IssueName", "CO_OrderDate", "CO_OrderId", "CO_ApplicationNo", "IssueStartDateANDTime", "IssueEndDateANDTime", "Amounttoinvest", "AmountBid", "WOS_OrderStep", "Bidding_Exchange", "COS_Reason");
           
            foreach (DataRow sourcerow in dts.Rows)
            {
                DataRow destRow = dtd.NewRow();
                destRow["Scrip Name"] = sourcerow["AIM_IssueName"];
                destRow["Transaction Date"] = sourcerow["CO_OrderDate"];
                destRow["Transaction No"] = sourcerow["CO_OrderId"];
                destRow["Application No"] = sourcerow["CO_ApplicationNo"];
                destRow["Start Date"] = sourcerow["IssueStartDateANDTime"];
                destRow["End Date"] = sourcerow["IssueEndDateANDTime"];
                destRow["Amount Invested"] = sourcerow["Amounttoinvest"];
                destRow["Bid Amount"] = sourcerow["AmountBid"];
                destRow["Status"] = sourcerow["WOS_OrderStep"];
                destRow["Bidding Exchange"] = sourcerow["Bidding_Exchange"];
                destRow["Reject Reason"] = sourcerow["COS_Reason"];
                dtd.Rows.Add(destRow);
            }
            if (dtd.Rows.Count > 0)
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "CustomerIPOOrderBook.xls"));
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
            dtIPOOrderBook.Columns.Add("Scrip Name");
            dtIPOOrderBook.Columns.Add("Application No");
            dtIPOOrderBook.Columns.Add("Start Date", typeof(DateTime));
            dtIPOOrderBook.Columns.Add("End Date", typeof(DateTime));
            dtIPOOrderBook.Columns.Add("Amount Invested", typeof(double));
            dtIPOOrderBook.Columns.Add("Bid Amount", typeof(double));
            dtIPOOrderBook.Columns.Add("Status");
            dtIPOOrderBook.Columns.Add("Bidding Exchange");
            dtIPOOrderBook.Columns.Add("Reject Reason");
            return dtIPOOrderBook;

        }



        //protected void ddlAction_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        protected void btnEditModiY_Click(object sender, EventArgs e)
        {
            LinkButton lnkbutton = (LinkButton)sender;
            GridDataItem gvr = (GridDataItem)lnkbutton.NamingContainer;
            int orderId = int.Parse(RadGridIssueIPOBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["CO_OrderId"].ToString());
            string OrderStepCode = Convert.ToString(RadGridIssueIPOBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["WOS_OrderStepCode"]).Trim();
            string CloseDate = Convert.ToString(RadGridIssueIPOBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["IssueEndDateANDTime"]);
            int issueId = int.Parse(RadGridIssueIPOBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["AIM_IssueId"].ToString());
            string Iscancel = Convert.ToString(RadGridIssueIPOBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["WOS_OrderStep"]);
            double maxamount = Convert.ToDouble(RadGridIssueIPOBook.MasterTableView.DataKeyValues[gvr.ItemIndex]["COID_MaxBidAmt"]);
            string orderstep = onlineIPOOrderBo.IPOOrderExtractStep(orderId);
            bool result = false;
            bool accountDebitStatus = false;
            switch (lnkbutton.ID)
            {

                case "lnkModify":
                    if (orderstep == "" || orderstep == "UB")
                    {
                        if (Session["PageDefaultSetting"] != null)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl( 'IPOIssueTransact','action=" + "Edit" + "&orderId=" + orderId + "&OrderStepCode=" + OrderStepCode + "&CloseDate=" + CloseDate + "&issueIds=" + issueId + "&Iscancel=" + Iscancel + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "IPOIssueTransact", "loadcontrol( 'IPOIssueTransact','action=" + "Edit" + "&orderId=" + orderId + "&OrderStepCode=" + OrderStepCode + "&CloseDate=" + CloseDate + "&issueIds=" + issueId + "&Iscancel=" + Iscancel + "');", true);
                        }
                    }
                    break;
                case "lnkCancel":
                    hdneligible.Value = "Yes";
                    string confirmValue = hdnAmount.Value;
                    if (confirmValue == "Yes")
                    {
                        if (Iscancel == "ORDERED")
                        {
                            result = onlineIPOOrderBo.CustomerIPOOrderCancelle(orderId, "ORDERED");
                            if (result == true)
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('IPO Order is cancelled, amount of RS  " + maxamount + "  will credited from Registrar.');", true);
                            }
                        }
                        else
                        {
                            result = onlineIPOOrderBo.CustomerIPOOrderCancelle(orderId, "INPROCESS");
                            if (result == true)
                            {
                                accountDebitStatus = onlineIPOOrderBo.DebitRMSUserAccountBalance(customerVo.AccountId, +maxamount, orderId, out debitstatus);
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('IPO Order is cancelled, Order reference no. is  " + orderId + ".');", true);
                            }
                        }
                    }
                    break;
            }
            BindCustomerIssueIPOBook();
        }
    }
}
