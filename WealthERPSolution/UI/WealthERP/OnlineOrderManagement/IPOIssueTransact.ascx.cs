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
    public partial class IPOIssueTransact : System.Web.UI.UserControl
    {
        OnlineIPOOrderBo onlineIPOOrderBo = new OnlineIPOOrderBo();
        OnlineIPOOrderVo onlineIPOOrderVo = new OnlineIPOOrderVo();
        AdvisorVo advisorVo;
        CustomerVo customerVo;
        UserVo userVo;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["customerVo"];

            var issueId = string.Empty;
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["issueId"] != null)
                {
                    issueId = Request.QueryString["issueId"].ToString();
                    BindIPOIssueList(issueId);
                    BindIPOBidGrid(3);
                }
            }

        }

        private void BindIPOIssueList(string issueId)
        {
            DataTable dtOnlineIPOIssueList = onlineIPOOrderBo.GetIPOIssueList(advisorVo.advisorId, Convert.ToInt32(issueId));

            if (dtOnlineIPOIssueList.Rows.Count > 0)
            {
                if (Cache["IPOIssueList" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("IPOIssueList" + userVo.UserId.ToString(), dtOnlineIPOIssueList);
                }
                else
                {
                    Cache.Remove("IPOIssueList" + userVo.UserId.ToString());
                    Cache.Insert("IPOIssueList" + userVo.UserId.ToString(), dtOnlineIPOIssueList);
                }
                //ibtExportSummary.Visible = false;
                RadGridIPOIssueList.DataSource = dtOnlineIPOIssueList;
                RadGridIPOIssueList.DataBind();
            }
            else
            {
                //ibtExportSummary.Visible = false;
                RadGridIPOIssueList.DataSource = dtOnlineIPOIssueList;
                RadGridIPOIssueList.DataBind();

            }
        }

        private void BindIPOBidGrid(int noOfBid)
        {
            DataTable dtIPOBid = new DataTable();
            DataRow drIPOBid;
            dtIPOBid.Columns.Add("IssueBidNo");
            dtIPOBid.Columns.Add("BidOptions");
            dtIPOBid.Columns.Add("IsCutOff");
            dtIPOBid.Columns.Add("BidPrice");
            dtIPOBid.Columns.Add("BidQty");
            dtIPOBid.Columns.Add("BidAmount");

            for (int i = 1; i <= noOfBid; i++)
            {
                drIPOBid = dtIPOBid.NewRow();
                drIPOBid["IssueBidNo"] = i.ToString();
                drIPOBid["BidOptions"] = "Bid" + i.ToString();
                drIPOBid["IsCutOff"] = 0;
                drIPOBid["BidPrice"] = null;
                drIPOBid["BidQty"] = null;
                drIPOBid["BidAmount"] = null;
                dtIPOBid.Rows.Add(drIPOBid);

            }

            RadGridIPOBid.DataSource = dtIPOBid;
            RadGridIPOBid.DataBind();


        }

        protected void bidTextBoxs_TextChanged(object sender, EventArgs e)
        {
            int currentRowidex = (((GridDataItem)((TextBox)sender).NamingContainer).RowIndex / 2) - 1;
            ReseIssueBidValues(currentRowidex);

        }

        protected void CutOffCheckBox_Changed(object sender, EventArgs e)
        {
            int currentRowindex = (((GridDataItem)((CheckBox)sender).NamingContainer).RowIndex / 2) - 1;
            ReseIssueBidValues(currentRowindex);

        }

        protected void ReseIssueBidValues(int row)
        {
            CheckBox chkCutOff = (CheckBox)RadGridIPOBid.MasterTableView.Items[row]["CheckCutOff"].FindControl("cbCutOffCheck");
            TextBox txtBidQuantity = (TextBox)RadGridIPOBid.MasterTableView.Items[row]["BidQuantity"].FindControl("txtBidQuantity");
            TextBox txtBidPrice = (TextBox)RadGridIPOBid.MasterTableView.Items[row]["BidPrice"].FindControl("txtBidPrice");
            TextBox txtBidAmount = (TextBox)RadGridIPOBid.MasterTableView.Items[row]["BidAmount"].FindControl("txtBidAmount");
            decimal capPrice = Convert.ToDecimal(RadGridIPOIssueList.MasterTableView.Items[0]["AIM_CapPrice"].Text.Trim());

            if (chkCutOff.Checked)
            {
                txtBidPrice.Text = capPrice.ToString();
                txtBidPrice.Enabled = false;
            }
            else
            {
                txtBidPrice.Enabled = true;
                //txtBidPrice.Text = string.Empty;
                //txtBidAmount.Text = string.Empty;

            }

            if (!string.IsNullOrEmpty(txtBidQuantity.Text.Trim()) && !string.IsNullOrEmpty(txtBidPrice.Text.Trim()))
                txtBidAmount.Text = (Convert.ToInt32(txtBidQuantity.Text.Trim()) * Convert.ToDecimal(txtBidPrice.Text.Trim())).ToString();


        }


        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {

            confirmMessage.Text = "I/We here by confirm that this is an execution-only transaction without any iteraction or advice by the employee/relationship manager/sales person of the above distributor or notwithstanding the advice of in-appropriateness, if any, provided by the employee/relationship manager/sales person of the distributor and the distributor has not chargedany advisory fees on this transaction";
            string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);



        }

        private void CreateIPOOrder()
        {
            string userMessage = String.Empty;
            bool accountDebitStatus = false;
            int orderId = 0;
            double totalBidAmount = 0;
            double availableBalance = (double)onlineIPOOrderBo.GetUserRMSAccountBalance(customerVo.AccountId);

            int issueId = Convert.ToInt32(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_IssueId"].ToString());
            DataTable dtIPOBidTransactionDettails = new DataTable();
            dtIPOBidTransactionDettails.Columns.Add("IssueBidNo", typeof(Int16));
            dtIPOBidTransactionDettails.Columns.Add("IsCutOffApplicable", typeof(Int16));
            dtIPOBidTransactionDettails.Columns.Add("IPOIssueBidQuantity", typeof(Int16), null);
            dtIPOBidTransactionDettails.Columns.Add("IPOIssueBidPrice", typeof(decimal), null);
            dtIPOBidTransactionDettails.Columns.Add("IPOIssueBidAmount", typeof(decimal), null);
            dtIPOBidTransactionDettails.Columns.Add("TransactionStatusCode", typeof(Int16));
            DataRow drIPOBid;

            onlineIPOOrderVo.CustomerId = customerVo.CustomerId;
            onlineIPOOrderVo.IssueId = issueId;
            onlineIPOOrderVo.AssetGroup = "IP";
            onlineIPOOrderVo.IsOrderClosed = false;
            onlineIPOOrderVo.IsOnlineOrder = true;
            onlineIPOOrderVo.IsDeclarationAccepted = true;
            onlineIPOOrderVo.OrderDate = DateTime.Now;

            int radgridRowNo = 0;
            foreach (GridDataItem radItem in RadGridIPOBid.MasterTableView.Items)
            {
                drIPOBid = dtIPOBidTransactionDettails.NewRow();

                CheckBox chkCutOff = (CheckBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["CheckCutOff"].FindControl("cbCutOffCheck");
                TextBox txtBidQuantity = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidQuantity"].FindControl("txtBidQuantity");
                TextBox txtBidPrice = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidPrice"].FindControl("txtBidPrice");
                TextBox txtBidAmount = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidAmount"].FindControl("txtBidAmount");

                drIPOBid["IssueBidNo"] = RadGridIPOBid.MasterTableView.DataKeyValues[radgridRowNo]["IssueBidNo"].ToString();
                drIPOBid["IsCutOffApplicable"] = chkCutOff.Checked ? true : false;

                if (!string.IsNullOrEmpty(txtBidQuantity.Text.Trim()))
                    drIPOBid["IPOIssueBidQuantity"] = txtBidQuantity.Text.Trim();
                else
                    drIPOBid["IPOIssueBidQuantity"] = DBNull.Value;

                if (!string.IsNullOrEmpty(txtBidPrice.Text.Trim()))
                    drIPOBid["IPOIssueBidPrice"] = txtBidPrice.Text.Trim();
                else
                    drIPOBid["IPOIssueBidPrice"] = DBNull.Value;

                if (!string.IsNullOrEmpty(txtBidAmount.Text.Trim()))
                {
                    drIPOBid["IPOIssueBidAmount"] = txtBidAmount.Text.Trim();
                    totalBidAmount += Convert.ToDouble(txtBidAmount.Text.Trim());
                }
                else
                    drIPOBid["IPOIssueBidAmount"] = DBNull.Value;

                if (!string.IsNullOrEmpty(txtBidAmount.Text.Trim()))
                    drIPOBid["TransactionStatusCode"] = 1;
                else
                    drIPOBid["TransactionStatusCode"] = 5;


                dtIPOBidTransactionDettails.Rows.Add(drIPOBid);
                if (radgridRowNo < RadGridIPOBid.MasterTableView.Items.Count)
                    radgridRowNo++;
                else
                    break;
            }
            if (availableBalance >= totalBidAmount)
            {
                orderId = onlineIPOOrderBo.CreateIPOBidOrderDetails(advisorVo.advisorId, userVo.UserId, dtIPOBidTransactionDettails, onlineIPOOrderVo);
                if (orderId != 0 && !string.IsNullOrEmpty(customerVo.AccountId))
                {
                    accountDebitStatus = onlineIPOOrderBo.DebitRMSUserAccountBalance(customerVo.AccountId, -totalBidAmount, orderId);

                }

                userMessage = CreateUserMessage(orderId, true, false);
                ShowMessage(userMessage);
            }

        }

        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
        }

        private string CreateUserMessage(int orderId, bool accountDebitStatus, bool isCutOffTimeOver)
        {
            string userMessage = string.Empty;
            if (orderId != 0 && accountDebitStatus == true)
            {
                if (isCutOffTimeOver)
                    userMessage = "Order placed successfully, Order reference no is " + orderId.ToString() + ", Order will process next business day";
                else
                    userMessage = "Order placed successfully, Order reference no is " + orderId.ToString();
            }
            return userMessage;

        }
        private void ShowAvailableLimits()
        {
            if (!string.IsNullOrEmpty(customerVo.AccountId))
            {
                lblAvailableLimits.Text = onlineIPOOrderBo.GetUserRMSAccountBalance(customerVo.AccountId).ToString();
            }

        }

        protected void lnkTermsCondition_Click(object sender, EventArgs e)
        {
            rwTermsCondition.VisibleOnPageLoad = true;
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            rwTermsCondition.VisibleOnPageLoad = false;
            chkTermsCondition.Checked = true;
        }

        public void TermsConditionCheckBox(object o, ServerValidateEventArgs e)
        {
            if (chkTermsCondition.Checked)
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }

        protected void rbConfirm_OK_Click(object sender, EventArgs e)
        {
            CreateIPOOrder();
        }


    }
}