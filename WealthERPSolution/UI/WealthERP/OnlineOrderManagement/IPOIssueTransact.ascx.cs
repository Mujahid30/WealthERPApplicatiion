﻿using System;
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
        DataTable dtOnlineIPOIssueList;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["customerVo"];
            //msgRecordStatus.InnerText = "Order placed successfully, Order reference no is " + 1234.ToString() + ", Order will process next business day";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wsedrftgyhjuklocvvvvvdddretyu", " showMsg('Order placed successfully, Order reference no is 1234 Order will process next business day','S');", true);

            var issueId = string.Empty;
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["issueId"] != null)
                {
                    issueId = Request.QueryString["issueId"].ToString();
                    BindIPOIssueList(issueId);
                    BindIPOBidGrid(3);
                    ShowAvailableLimits();
                }
            }

        }

        private void BindIPOIssueList(string issueId)
        {
            dtOnlineIPOIssueList = onlineIPOOrderBo.GetIPOIssueList(advisorVo.advisorId, Convert.ToInt32(issueId), 1, customerVo.CustomerId);

            RadGridIPOIssueList.DataSource = dtOnlineIPOIssueList;
            RadGridIPOIssueList.DataBind();

            //if (dtOnlineIPOIssueList.Rows.Count > 0)
            //{
            //    if (Cache["IPOIssueList" + userVo.UserId.ToString()] == null)
            //    {
            //        Cache.Insert("IPOIssueList" + userVo.UserId.ToString(), dtOnlineIPOIssueList);
            //    }
            //    else
            //    {
            //        Cache.Remove("IPOIssueList" + userVo.UserId.ToString());
            //        Cache.Insert("IPOIssueList" + userVo.UserId.ToString(), dtOnlineIPOIssueList);
            //    }
            //    //ibtExportSummary.Visible = false;
            //    RadGridIPOIssueList.DataSource = dtOnlineIPOIssueList;
            //    RadGridIPOIssueList.DataBind();
            //}
            //else
            //{
            //    //ibtExportSummary.Visible = false;
            //    RadGridIPOIssueList.DataSource = dtOnlineIPOIssueList;
            //    RadGridIPOIssueList.DataBind();

            //}
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
            dtIPOBid.Columns.Add("BidAmountPayable", typeof(double));
            dtIPOBid.Columns.Add("BidAmount", typeof(double));

            for (int i = 1; i <= noOfBid; i++)
            {
                drIPOBid = dtIPOBid.NewRow();
                drIPOBid["IssueBidNo"] = i.ToString();
                drIPOBid["BidOptions"] = "Bid Option" + i.ToString();
                drIPOBid["IsCutOff"] = 0;
                drIPOBid["BidPrice"] = null;
                drIPOBid["BidQty"] = null;
                drIPOBid["BidAmountPayable"] = 0;
                drIPOBid["BidAmount"] = 0;
                dtIPOBid.Rows.Add(drIPOBid);

            }

            RadGridIPOBid.DataSource = dtIPOBid;
            RadGridIPOBid.DataBind();


        }

        protected void BidQuantityPrice_TextChanged(object sender, EventArgs e)
        {
            int currentRowidex = (((GridDataItem)((TextBox)sender).NamingContainer).RowIndex / 2) - 1;
            ReseIssueBidValues(currentRowidex);

        }

        protected void CutOffCheckBox_Changed(object sender, EventArgs e)
        {
            int currentRowindex = (((GridDataItem)((CheckBox)sender).NamingContainer).RowIndex / 2) - 1;
            ReseIssueBidValues(currentRowindex);
            //CheckBox chkCutOff = (CheckBox)RadGridIPOBid.MasterTableView.Items[currentRowindex]["CheckCutOff"].FindControl("cbCutOffCheck");

        }

        protected void ReseIssueBidValues(int row)
        {
            double bidAmount = 0;
            double ipoPriceDiscountValue = 0;
            CheckBox chkCutOff = (CheckBox)RadGridIPOBid.MasterTableView.Items[row]["CheckCutOff"].FindControl("cbCutOffCheck");
            TextBox txtBidQuantity = (TextBox)RadGridIPOBid.MasterTableView.Items[row]["BidQuantity"].FindControl("txtBidQuantity");
            TextBox txtBidPrice = (TextBox)RadGridIPOBid.MasterTableView.Items[row]["BidPrice"].FindControl("txtBidPrice");
            TextBox txtBidAmount = (TextBox)RadGridIPOBid.MasterTableView.Items[row]["BidAmount"].FindControl("txtBidAmount");
            TextBox txtBidAmountPayable = (TextBox)RadGridIPOBid.MasterTableView.Items[row]["BidAmountPayable"].FindControl("txtBidAmountPayable");


            double capPrice = Convert.ToDouble(RadGridIPOIssueList.MasterTableView.Items[0]["AIM_CapPrice"].Text.Trim());
            string ipoPriceDiscountType = RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIIC_PriceDiscountType"].ToString();
            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIIC_PriceDiscountValue"].ToString()))
                ipoPriceDiscountValue = Convert.ToDouble(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIIC_PriceDiscountValue"].ToString());

            double bidAmountPayable = 0;
            if (chkCutOff.Checked)
            {
                txtBidPrice.Text = capPrice.ToString();
                txtBidPrice.Enabled = false;
                txtBidPrice.CssClass = "txtDisableField";
            }

            if (!string.IsNullOrEmpty(txtBidQuantity.Text.Trim()) && !string.IsNullOrEmpty(txtBidPrice.Text.Trim()))
            {
                txtBidAmount.Text = (Convert.ToInt32(txtBidQuantity.Text.Trim()) * Convert.ToDecimal(txtBidPrice.Text.Trim())).ToString();

                bidAmount = double.Parse(txtBidAmount.Text);
                bidAmountPayable = bidAmount;

                if (!string.IsNullOrEmpty(ipoPriceDiscountType.Trim()))
                {
                    switch (ipoPriceDiscountType)
                    {
                        case "AM":
                            {
                                bidAmountPayable = (Convert.ToDouble(txtBidPrice.Text.Trim()) - ipoPriceDiscountValue) * (Convert.ToInt32(txtBidQuantity.Text.Trim()));
                                break;
                            }
                        case "PE":
                            {
                                bidAmountPayable = (Convert.ToDouble(txtBidPrice.Text.Trim()) - ((ipoPriceDiscountValue / 100) * Convert.ToDouble(txtBidPrice.Text.Trim()))) * (Convert.ToInt32(txtBidQuantity.Text.Trim()));
                                break;
                            }
                    }
                }

                txtBidAmountPayable.Text = Math.Round(bidAmountPayable, 2).ToString();

            }
            else
            {
                txtBidAmount.Text = 0.ToString();
                txtBidAmountPayable.Text = 0.ToString();

            }

            if (chkCutOff.Checked)
                EnableDisableBids(true, 3);
            else
                EnableDisableBids(false, 3);

        }

        protected void EnableDisableBids(bool isChecked, int noOfBid)
        {
            double[] bidMaxPayableAmount = new double[noOfBid];
            int count = 0;
            double finalBidPayableAmount = 0;
            foreach (GridDataItem item in RadGridIPOBid.MasterTableView.Items)
            {
                CheckBox chkCutOff = (CheckBox)item.FindControl("cbCutOffCheck");
                TextBox txtBidQuantity = (TextBox)item.FindControl("txtBidQuantity");
                TextBox txtBidPrice = (TextBox)item.FindControl("txtBidPrice");

                TextBox txtBidAmount = (TextBox)item.FindControl("txtBidAmount");
                TextBox txtBidAmountPayable = (TextBox)item.FindControl("txtBidAmountPayable");


                if (isChecked)
                {
                    if (chkCutOff.Checked)
                    {
                        txtBidQuantity.Enabled = true;
                        txtBidQuantity.CssClass = "txtField";

                        txtBidPrice.Enabled = false;
                        txtBidPrice.CssClass = "txtDisableField";
                    }
                    else
                    {
                        chkCutOff.Enabled = false;

                        txtBidQuantity.Enabled = false;
                        txtBidQuantity.CssClass = "txtDisableField";

                        txtBidPrice.Enabled = false;
                        txtBidPrice.CssClass = "txtDisableField";

                        txtBidQuantity.Text = string.Empty;
                        txtBidPrice.Text = string.Empty;
                        txtBidAmount.Text = "0";
                        txtBidAmountPayable.Text = "0";

                    }


                }
                else
                {
                    chkCutOff.Enabled = true;

                    txtBidQuantity.Enabled = true;
                    txtBidQuantity.CssClass = "txtField";

                    txtBidPrice.Enabled = true;
                    txtBidPrice.CssClass = "txtField";

                }

                bidMaxPayableAmount[count] = Convert.ToDouble(txtBidAmountPayable.Text);
                count = count + 1;

            }

            finalBidPayableAmount = bidMaxPayableAmount.Max();

            foreach (GridFooterItem footeritem in RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer))
            {
                Label lblBidHighestValue = (Label)footeritem["BidAmountPayable"].FindControl("lblFinalBidAmountPayable");
                TextBox txtFinalBidAmount = (TextBox)footeritem["BidAmountPayable"].FindControl("txtFinalBidValue");

                lblBidHighestValue.Text = finalBidPayableAmount.ToString();
                txtFinalBidAmount.Text = lblBidHighestValue.Text.Trim();
            }


        }




        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            string errorMsg = string.Empty;
            bool isBidsVallid = false;
            isBidsVallid = ValidateIPOBids(out errorMsg);
            if (isBidsVallid)
            {
                confirmMessage.Text = "I/We here by confirm that this is an execution-only transaction without any iteraction or advice by the employee/relationship manager/sales person of the above distributor or notwithstanding the advice of in-appropriateness, if any, provided by the employee/relationship manager/sales person of the distributor and the distributor has not chargedany advisory fees on this transaction";
                string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('" + errorMsg + "');", true);
                return;

            }



        }

        private void CreateIPOOrder()
        {
            string userMessage = String.Empty;
            bool accountDebitStatus = false;
            int orderId = 0;
            double totalBidAmount = 0;
            string applicationNo = String.Empty;
            string apllicationNoStatus = String.Empty;
            double maxPaybleBidAmount = 0;

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

            foreach (GridFooterItem footeritem in RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer))
            {
                Label lblBidHighestValue = (Label)footeritem["BidAmountPayable"].FindControl("lblFinalBidAmountPayable");
                maxPaybleBidAmount = Convert.ToDouble(lblBidHighestValue.Text.Trim());
            }

            if (availableBalance >= maxPaybleBidAmount)
            {
                orderId = onlineIPOOrderBo.CreateIPOBidOrderDetails(advisorVo.advisorId, userVo.UserId, dtIPOBidTransactionDettails, onlineIPOOrderVo, ref applicationNo, ref   apllicationNoStatus);
                if (orderId != 0 && !string.IsNullOrEmpty(customerVo.AccountId))
                {
                    accountDebitStatus = onlineIPOOrderBo.DebitRMSUserAccountBalance(customerVo.AccountId, -maxPaybleBidAmount, orderId);
                    availableBalance = (double)onlineIPOOrderBo.GetUserRMSAccountBalance(customerVo.AccountId);
                    lblAvailableLimits.Text = Convert.ToInt64(availableBalance).ToString();
                }

                userMessage = CreateUserMessage(orderId, accountDebitStatus, false, applicationNo, apllicationNoStatus);
            }
            else
            {
                userMessage = CreateUserMessage(orderId, false, false, applicationNo, apllicationNoStatus);
            }

            ShowMessage(userMessage);

        }

        private void ShowMessage(string msg)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wsedrftgyhjukloghjnnn", " showMsg('" + msg + "','S');", true);

        }

        private string CreateUserMessage(int orderId, bool accountDebitStatus, bool isCutOffTimeOver, string applicationno, string aplicationNoStatus)
        {
            string userMessage = string.Empty;
            if (orderId != 0 && accountDebitStatus == true)
            {
                if (isCutOffTimeOver)
                    userMessage = "Order placed successfully, Order reference no is " + orderId.ToString() + ", Order will process next business day.";
                else
                    userMessage = "Order placed successfully, Order reference no is " + orderId.ToString() + " & Application no. " + applicationno;
            }
            else if (orderId == 0 & lblAvailableLimits.Text == "0")
            {
                userMessage = "Order cannot be processed. Insufficient balance";
            }

            else if (aplicationNoStatus == "Refill")
            {
                userMessage = "Order cannot be placed , Application oversubscribed. Please contact your relationship manager or contact call centre";

            }
            else if (accountDebitStatus == false)
            {
                userMessage = "NO Rms Response";
            }

            else if (orderId == 0)
            {
                userMessage = "Please allocate the adequate amount to place the order successfully.";
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
            ControlsVisblity(true);
        }

        private void ControlsVisblity(bool visble)
        {
            btnConfirmOrder.Visible = false;
        }

        private bool ValidateIPOBids(out string msg)
        {
            bool isBidValid = true;
            msg = string.Empty;
            foreach (GridDataItem item in RadGridIPOBid.MasterTableView.Items)
            {
                double bidAmountPayble = 0;
                //CheckBox chkCutOff = (CheckBox)item.FindControl("cbCutOffCheck");
                TextBox txtBidQuantity = (TextBox)item.FindControl("txtBidQuantity");
                TextBox txtBidPrice = (TextBox)item.FindControl("txtBidPrice");

                TextBox txtBidAmount = (TextBox)item.FindControl("txtBidAmount");
                TextBox txtBidAmountPayable = (TextBox)item.FindControl("txtBidAmountPayable");
                double.TryParse(txtBidAmountPayable.Text, out bidAmountPayble);
                if (bidAmountPayble <= 0 && int.Parse(item.GetDataKeyValue("IssueBidNo").ToString()) == 1)
                {
                    msg = "Please complete the Bid Option" + item.GetDataKeyValue("IssueBidNo").ToString() + " first";
                    isBidValid = false;
                    return isBidValid;
                }
                else if ((!string.IsNullOrEmpty(txtBidQuantity.Text) || !string.IsNullOrEmpty(txtBidPrice.Text)) && bidAmountPayble == 0)
                {
                    msg = "Please complete the Bid Option" + item.GetDataKeyValue("IssueBidNo").ToString();
                    isBidValid = false;
                    return isBidValid;
                }

            }

            return isBidValid;

        }

        protected void RadGridIPOBid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataform = (GridDataItem)e.Item;
                RangeValidator rvQuantity = (RangeValidator)dataform.FindControl("rvQuantity");
                RangeValidator rvBidPrice = (RangeValidator)dataform.FindControl("rvBidPrice");
                int minQuantity = 0;
                int maxQuantity = 0;
                double minBidPrice = 0;
                double maxBidPrice = 0;
                int.TryParse(dtOnlineIPOIssueList.Rows[0]["AIM_MInQty"].ToString(), out minQuantity);
                int.TryParse(dtOnlineIPOIssueList.Rows[0]["AIM_MaxQty"].ToString(), out maxQuantity);

                double.TryParse(dtOnlineIPOIssueList.Rows[0]["AIM_FloorPrice"].ToString(), out minBidPrice);
                double.TryParse(dtOnlineIPOIssueList.Rows[0]["AIM_CapPrice"].ToString(), out maxBidPrice);

                if (e.Item.RowIndex != -1)
                {
                    rvQuantity.MinimumValue = minQuantity.ToString();
                    rvQuantity.MaximumValue = maxQuantity.ToString();

                    rvBidPrice.MinimumValue = minBidPrice.ToString();
                    rvBidPrice.MaximumValue = maxBidPrice.ToString();


                }
            }
            //else if (e.Item is GridFooterItem)
            //{
            //    GridFooterItem footerItem = (GridFooterItem)e.Item;
            //    CompareValidator cmpMaxBidAmount = (CompareValidator)footerItem.FindControl("cmpFinalBidAmountPayable");

            //    cmpMaxBidAmount.ValueToCompare = 0.ToString();
            //}
        }


    }
}
