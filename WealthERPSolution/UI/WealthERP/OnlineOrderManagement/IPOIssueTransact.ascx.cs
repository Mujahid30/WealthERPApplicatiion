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
using System.Configuration;

namespace WealthERP.OnlineOrderManagement
{
    public partial class IPOIssueTransact : System.Web.UI.UserControl
    {
        OnlineIPOOrderBo onlineIPOOrderBo = new OnlineIPOOrderBo();
        OnlineIPOOrderVo onlineIPOOrderVo = new OnlineIPOOrderVo();
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
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
            int TOcpmaretime = int.Parse(DateTime.Now.ToShortTimeString().Split(':')[0]);
            if (TOcpmaretime >= int.Parse(ConfigurationSettings.AppSettings["START_TIME"]) && TOcpmaretime <= int.Parse(ConfigurationSettings.AppSettings["END_TIME"]))
            {
                if (Session["PageDefaultSetting"] != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOnlineSchemeManager')", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "loadcontrol('MFOnlineSchemeManager')", true);
                    return;
                }
            }
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
                if (Request.QueryString["orderId"] != null)
                {
                    trTermsCondition.Visible = false;
                    BindIPOIssueList(Request.QueryString["issueIds"]);
                    hdneligible.Value = "Edit";
                    ViewIPOOrder(int.Parse(Request.QueryString["orderId"]));
                    if (Convert.ToBoolean(ViewState["orderIscanclled"].ToString()))
                    {
                        btnOrderCancel.Visible = false;
                        btnUpdateIPOdrder.Visible = false;
                    }
                    else
                    {
                        if (ViewState["OderExtractStep"].ToString() == "" || ViewState["OderExtractStep"].ToString() == "UB")
                        {
                            btnOrderCancel.Visible = false;
                            btnUpdateIPOdrder.Visible = true;
                        }
                    }
                    ShowAvailableLimits();
                    btnConfirmOrder.Visible = false;
                }
            }

        }

        private void BindIPOIssueList(string issueId)
        {
            dtOnlineIPOIssueList = onlineIPOOrderBo.GetIPOIssueList(advisorVo.advisorId, Convert.ToInt32(issueId), 1, customerVo.CustomerId);

            if (dtOnlineIPOIssueList.Rows.Count > 0)
            {
                if (Cache["IPOTransactIssueList" + userVo.UserId.ToString()] != null)
                {
                    Cache.Remove("IPOTransactIssueList" + userVo.UserId.ToString());
                }
                Cache.Insert("IPOTransactIssueList" + userVo.UserId.ToString(), dtOnlineIPOIssueList);


                RadGridIPOIssueList.DataSource = dtOnlineIPOIssueList;
                RadGridIPOIssueList.DataBind();
            }

            
        }

        protected void RadGridIPOIssueList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtOnlineIPOIssueList;
            dtOnlineIPOIssueList = (DataTable)Cache["IPOTransactIssueList" + userVo.UserId.ToString()];
            if (dtOnlineIPOIssueList != null)
            {
                RadGridIPOIssueList.DataSource = dtOnlineIPOIssueList;
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
            dtIPOBid.Columns.Add("BidAmountPayable", typeof(double));
            dtIPOBid.Columns.Add("BidAmount", typeof(double));
            dtIPOBid.Columns.Add("COID_TransactionType");
            dtIPOBid.Columns.Add("COID_DetailsId");
            dtIPOBid.Columns.Add("COID_IsCutOffApplicable");


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
                drIPOBid["COID_TransactionType"] = "N";
                drIPOBid["COID_DetailsId"] = 0;
                drIPOBid["COID_IsCutOffApplicable"] = 0;


            }

            RadGridIPOBid.DataSource = dtIPOBid;
            RadGridIPOBid.DataBind();


        }
        protected void CVBidQtyMultiple_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            int issueQtyMultiple = 0;
            int issueMinQty = 0;
            int issueMaxQty = 0;
            int bidQuantity = Convert.ToInt32(args.Value.ToString());

            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_TradingInMultipleOf"].ToString()))
                issueQtyMultiple = Convert.ToInt16(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_TradingInMultipleOf"].ToString());
            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MInQty"].ToString()))
                issueMinQty = Convert.ToInt32(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MInQty"].ToString());
            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MaxQty"].ToString()))
                issueMaxQty = Convert.ToInt32(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MaxQty"].ToString());

            if ((bidQuantity - issueMinQty) % issueQtyMultiple != 0 && bidQuantity != issueMinQty && bidQuantity != issueMaxQty)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }


        }

        protected void BidQuantity_TextChanged(object sender, EventArgs e)
        {
            int currentRowidex = (((GridDataItem)((TextBox)sender).NamingContainer).RowIndex / 2) - 1;
            ReseIssueBidValues(currentRowidex, false);

            Page.Validate("btnConfirmOrder");

        }

        protected void BidPrice_TextChanged(object sender, EventArgs e)
        {
            int currentRowidex = (((GridDataItem)((TextBox)sender).NamingContainer).RowIndex / 2) - 1;
            ReseIssueBidValues(currentRowidex, true);
            CustomValidator1.IsValid = true;
            Page.Validate("btnConfirmOrder");

        }

        protected void CutOffCheckBox_Changed(object sender, EventArgs e)
        {
            int currentRowindex = (((GridDataItem)((CheckBox)sender).NamingContainer).RowIndex / 2) - 1;
            ReseIssueBidValues(currentRowindex, false);
            //CheckBox chkCutOff = (CheckBox)RadGridIPOBid.MasterTableView.Items[currentRowindex]["CheckCutOff"].FindControl("cbCutOffCheck");
            Page.Validate("btnConfirmOrder");

        }

        protected void ReseIssueBidValues(int row, bool isBidPriceChange)
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
                
                bidAmount=Convert.ToInt32(txtBidQuantity.Text.Trim()) * Convert.ToDouble(txtBidPrice.Text.Trim());
                txtBidAmount.Text = string.Format("{0:0.00}", Math.Round(bidAmount, 2));
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
               
                txtBidAmountPayable.Text=string.Format("{0:0.00}", Math.Round(bidAmountPayable, 2)) ;
               

            }
            else
            {
                txtBidAmount.Text = 0.ToString();
                txtBidAmountPayable.Text = 0.ToString();

            }

            if (chkCutOff.Checked)
                EnableDisableBids(true, 3, row, isBidPriceChange);
            else
                EnableDisableBids(false, 3, row, isBidPriceChange);

        }

        protected void EnableDisableBids(bool isChecked, int noOfBid, int rowNum, bool isBidPriceChange)
        {
            decimal[] bidMaxPayableAmount = new decimal[noOfBid];
            int count = 0;
            decimal finalBidPayableAmount = 0;
            List<string> iPOBids = new List<string>();
            string bidDuplicateChk = string.Empty;
             decimal minBidAmount=0;
             decimal maxBidAmount =0;
             bool isBidValid = true;
            foreach (GridDataItem item in RadGridIPOIssueList.MasterTableView.Items)
            {
               minBidAmount = Convert.ToDecimal(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIIC_MInBidAmount"].ToString());
               maxBidAmount = Convert.ToDecimal(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIIC_MaxBidAmount"].ToString());
            }
            foreach (GridDataItem item in RadGridIPOBid.MasterTableView.Items)
            {
                CheckBox chkCutOff = (CheckBox)item.FindControl("cbCutOffCheck");
                TextBox txtBidQuantity = (TextBox)item.FindControl("txtBidQuantity");
                TextBox txtBidPrice = (TextBox)item.FindControl("txtBidPrice");

                TextBox txtBidAmount = (TextBox)item.FindControl("txtBidAmount");
                TextBox txtBidAmountPayable = (TextBox)item.FindControl("txtBidAmountPayable");
                ImageButton btnOrderEdit = (ImageButton)item.FindControl("btnOrderEdit");
                ImageButton btnOrderCancel = (ImageButton)item.FindControl("btnOrderCancel");
             
                if (!string.IsNullOrEmpty(txtBidQuantity.Text.Trim()) && !string.IsNullOrEmpty(txtBidPrice.Text.Trim()))
                {
                    bidDuplicateChk = txtBidQuantity.Text.Trim() + "-" + txtBidPrice.Text.Trim();
                    if (!iPOBids.Contains(bidDuplicateChk))
                    {
                        iPOBids.Add(bidDuplicateChk);
                    }
                    else
                    {
                        TextBox txtBidQuantity1 = (TextBox)RadGridIPOBid.MasterTableView.Items[rowNum]["BidQuantity"].FindControl("txtBidQuantity");
                        TextBox txtBidPrice1 = (TextBox)RadGridIPOBid.MasterTableView.Items[rowNum]["BidPrice"].FindControl("txtBidPrice");
                        TextBox txtBidAmount1 = (TextBox)RadGridIPOBid.MasterTableView.Items[rowNum]["BidAmount"].FindControl("txtBidAmount");
                        TextBox txtBidAmountPayable1 = (TextBox)RadGridIPOBid.MasterTableView.Items[rowNum]["BidAmountPayable"].FindControl("txtBidAmountPayable");
                        if (isBidPriceChange)
                        {
                            txtBidPrice1.Text = string.Empty;
                            txtBidPrice1.Focus();
                        }
                        else
                        {
                            txtBidQuantity1.Text = string.Empty;
                            txtBidQuantity1.Focus();
                        }
                        txtBidAmount1.Text = "0";
                        txtBidAmountPayable1.Text = "0";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Duplicate bids found.Each bid should have unique combination of price and quantity!');", true);
                        //return;
                    }
                }

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
                    if (Request.QueryString["orderId"] != null)
                    {
                        btnOrderEdit.Visible = false;
                        //btnOrderCancel.Visible = true;
                    }

                }
                if (!string.IsNullOrEmpty(txtBidAmountPayable.Text.Trim()))
                {
                    bidMaxPayableAmount[count] = Convert.ToDecimal(txtBidAmountPayable.Text);
                    count = count + 1;
                }

            }

            finalBidPayableAmount = bidMaxPayableAmount.Max();

            foreach (GridFooterItem footeritem in RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer))
            {
                Label lblBidHighestValue = (Label)footeritem["BidAmountPayable"].FindControl("lblFinalBidAmountPayable");
                TextBox txtFinalBidAmount = (TextBox)footeritem["BidAmountPayable"].FindControl("txtFinalBidValue");
                if ((minBidAmount > finalBidPayableAmount || maxBidAmount < finalBidPayableAmount) && finalBidPayableAmount != 0)
                {
                    lblBidHighestValue.Text = string.Format("{0:0.00}", finalBidPayableAmount);// finalBidPayableAmount.ToString();
                    txtFinalBidAmount.Text = string.Format("{0:0.00}", lblBidHighestValue.Text.Trim());// lblBidHighestValue.Text.Trim();
                    //Session["finalprice"] = lblBidHighestValue.Text;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Bid Value (Amount Payable) should be greater than the Min bid amount and less than the Max bid amount');", true);
                    return;
                }
                else
                {
                    lblBidHighestValue.Text = string.Format("{0:0.00}", finalBidPayableAmount);// finalBidPayableAmount.ToString();
                    txtFinalBidAmount.Text = string.Format("{0:0.00}", lblBidHighestValue.Text.Trim());// lblBidHighestValue.Text.Trim();
                }
               
            }


        }




        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            //string confirmValue = "Yes";
            //if (confirmValue == "Yes")
            //{
            string errorMsg = string.Empty;
            bool isBidsVallid = false;
            Page.Validate();
            if (Page.IsValid)
            {
                isBidsVallid = ValidateIPOBids(out errorMsg, 0);
                if (!isBidsVallid)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('" + errorMsg + "');", true);
                    return;
                }
                else
                {
                    CreateIPOOrder();
                    ControlsVisblity(true);

                }
            }

            //}

        }

        private void CreateIPOOrder()
        {
            string userMessage = String.Empty;
            bool accountDebitStatus = false;
            int orderId = 0;
            double totalBidAmount = 0;
            double totalBidAmountPayable = 0;
            string applicationNo = String.Empty;
            string apllicationNoStatus = String.Empty;
            double maxPaybleBidAmount = 0;
            DateTime cutOff = DateTime.Now;
            bool isCutOffTimeOver = false;


            double availableBalance = (double)onlineIPOOrderBo.GetUserRMSAccountBalance(customerVo.AccountId);

            int issueId = Convert.ToInt32(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_IssueId"].ToString());
            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_CutOffTime"].ToString()))
                cutOff = Convert.ToDateTime(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_CutOffTime"].ToString());
            DataTable dtIPOBidTransactionDettails = new DataTable();
            dtIPOBidTransactionDettails.Columns.Add("IssueBidNo", typeof(Int16));
            dtIPOBidTransactionDettails.Columns.Add("IsCutOffApplicable", typeof(Int16));
            dtIPOBidTransactionDettails.Columns.Add("IPOIssueBidQuantity", typeof(Int64), null);
            dtIPOBidTransactionDettails.Columns.Add("IPOIssueBidPrice", typeof(decimal), null);
            dtIPOBidTransactionDettails.Columns.Add("IPOIssueBidAmount", typeof(decimal), null);
            dtIPOBidTransactionDettails.Columns.Add("IPOIssueBidAmountPayable", typeof(decimal), null);
            dtIPOBidTransactionDettails.Columns.Add("TransactionStatusCode", typeof(Int16));
            dtIPOBidTransactionDettails.Columns.Add("MaxBidAmount", typeof(decimal), null);
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
                TextBox txtBidAmountPayable = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidAmountPayable"].FindControl("txtBidAmountPayable");
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

                if (!string.IsNullOrEmpty(txtBidAmountPayable.Text.Trim()))
                {
                    drIPOBid["IPOIssueBidAmountPayable"] = txtBidAmountPayable.Text.Trim();
                    totalBidAmountPayable += Convert.ToDouble(txtBidAmountPayable.Text.Trim());
                }
                else
                    drIPOBid["IPOIssueBidAmountPayable"] = DBNull.Value;


                if (!string.IsNullOrEmpty(txtBidAmount.Text.Trim()))
                    drIPOBid["TransactionStatusCode"] = 1;
                else
                    drIPOBid["TransactionStatusCode"] = 5;
                //GridFooterItem footeritem = (GridFooterItem)RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer)[4];
                GridFooterItem footer = (GridFooterItem)RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer)[0];
                Label lblFinalBidAmountPayable = (Label)footer.FindControl("lblFinalBidAmountPayable");
                drIPOBid["MaxBidAmount"] = lblFinalBidAmountPayable.Text;
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
            if (DateTime.Now.TimeOfDay > cutOff.TimeOfDay && cutOff.TimeOfDay < System.TimeSpan.Parse("23:59:59"))
                isCutOffTimeOver = true;

            if (availableBalance >= maxPaybleBidAmount)
            {
                orderId = onlineIPOOrderBo.CreateIPOBidOrderDetails(advisorVo.advisorId, userVo.UserId, dtIPOBidTransactionDettails, onlineIPOOrderVo, ref applicationNo, ref   apllicationNoStatus);
                if (orderId != 0 && !string.IsNullOrEmpty(customerVo.AccountId))
                {
                    accountDebitStatus = onlineIPOOrderBo.DebitRMSUserAccountBalance(customerVo.AccountId, -maxPaybleBidAmount, orderId);
                    availableBalance = (double)onlineIPOOrderBo.GetUserRMSAccountBalance(customerVo.AccountId);
                    lblAvailableLimits.Text = availableBalance.ToString();
                }

                userMessage = CreateUserMessage(orderId, accountDebitStatus, isCutOffTimeOver, applicationNo, apllicationNoStatus);
            }
            else
            {
                userMessage = CreateUserMessage(orderId, false, isCutOffTimeOver, applicationNo, apllicationNoStatus);
            }
            if(accountDebitStatus==true)
            ShowMessage(userMessage,"S");
            else
                ShowMessage(userMessage, "F");


        }

        private void ShowMessage(string msg, string type)//" + type + "
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wsedrftgyhjukloghjnnn", " showMsg('" + msg + "','" + type + "');", true);

        }

        private string CreateUserMessage(int orderId, bool accountDebitStatus, bool isCutOffTimeOver, string applicationno, string aplicationNoStatus)
        {
            string userMessage = string.Empty;
            if (orderId != 0 && accountDebitStatus == true)
            {
                if (isCutOffTimeOver)
                    userMessage = "Order placed successfully, Order reference no is " + orderId.ToString() + " & Application no. " + applicationno + ", Order will process next business day.";
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
            else if (orderId != 0 && accountDebitStatus == false)
            {
                userMessage = "Order Cannot be Placed, due to Insufficient Balance.";
            }
            else if (orderId == 0)
            {
                userMessage = "Order Cannot be Placed, due to Insufficient Balance.";
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

        }

        private void ControlsVisblity(bool visble)
        {
            btnConfirmOrder.Visible = false;
            lnlBack.Visible = true;
        }

        private bool ValidateIPOBids(out string msg, int typeOfvalidation)
        {
            bool isBidValid = true;
            msg = string.Empty;
            int validBidSum = 0;
            int issueQtyMultiple = 0;
            int issueMinQty = 0;
            int issueMaxQty = 0;
            int bidId = 1;
            DateTime dtCloseDate = Convert.ToDateTime(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_CloseDate"].ToString());
            //dtCloseDate = DateTime.Now.AddHours(-1);
            decimal minBidAmount = Convert.ToDecimal(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIIC_MInBidAmount"].ToString());
            decimal maxBidAmount = Convert.ToDecimal(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIIC_MaxBidAmount"].ToString());
            GridFooterItem footerItem = (GridFooterItem)RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer)[0];
            Label lblFinalBidAmountPayable = (Label)footerItem["BidAmountPayable"].FindControl("lblFinalBidAmountPayable");
            decimal maxPaybleAmount1 = Convert.ToDecimal(lblFinalBidAmountPayable.Text);
            decimal maxPaybleAmount = Convert.ToDecimal(((TextBox)footerItem.FindControl("txtFinalBidValue")).Text);//accessing Button inside 
            Boolean isMultipleApplicationAllowed = Convert.ToBoolean(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_IsMultipleApplicationsallowed"].ToString());
            int issueId = int.Parse(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_IssueId"].ToString());
            if (Request.QueryString["orderId"] == null)
            {
                if (isMultipleApplicationAllowed == false)
                {
                    int issueApplicationSubmitCount = onlineNCDBackOfficeBo.CustomerMultipleOrder(customerVo.CustomerId, issueId);
                    if (issueApplicationSubmitCount > 0)
                    {
                        msg = "You have already invested in selected issue, Please check the order book for the status.Multiple Investment is not allowed in same issue";
                        isBidValid = false;
                        return isBidValid;
                    }
                }
            }
            if (dtCloseDate < DateTime.Now)
            {
                msg = "Issue is closed now, order can not accept";
                isBidValid = false;
                return isBidValid;
            }

            if (maxPaybleAmount > 0)
            {
                if (minBidAmount > maxPaybleAmount || maxBidAmount < maxPaybleAmount)
                {
                    msg = "Bid Value (Amount Payable) should be greater than the Min bid amount and less than the Max bid amount";
                    isBidValid = false;
                    return isBidValid;
                }
            }
            else if (typeOfvalidation != 1 && maxPaybleAmount < 0)
            {
                if (minBidAmount > maxPaybleAmount1 || maxBidAmount < maxPaybleAmount1)
                {
                    msg = "Bid Value (Amount Payable) should be greater than the Min bid amount and less than the Max bid amount";
                    isBidValid = false;
                    return isBidValid;
                }
            }
            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_TradingInMultipleOf"].ToString()))
                issueQtyMultiple = Convert.ToInt16(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_TradingInMultipleOf"].ToString());
            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MInQty"].ToString()))
                issueMinQty = Convert.ToInt32(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MInQty"].ToString());
            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MaxQty"].ToString()))
                issueMaxQty = Convert.ToInt32(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MaxQty"].ToString());
            foreach (GridDataItem item in RadGridIPOBid.MasterTableView.Items)
            {
                double bidAmountPayble = 0;

                //CheckBox chkCutOff = (CheckBox)item.FindControl("cbCutOffCheck");
                TextBox txtBidQuantity = (TextBox)item.FindControl("txtBidQuantity");
                TextBox txtBidPrice = (TextBox)item.FindControl("txtBidPrice");

                TextBox txtBidAmount = (TextBox)item.FindControl("txtBidAmount");
                TextBox txtBidAmountPayable = (TextBox)item.FindControl("txtBidAmountPayable");
                double.TryParse(txtBidAmountPayable.Text, out bidAmountPayble);
                if (!string.IsNullOrEmpty(txtBidQuantity.Text))
                {
                    //Bid Quantity Multiple Validation

                    if ((Convert.ToInt64(txtBidQuantity.Text) - issueMinQty) % issueQtyMultiple != 0 && Convert.ToInt64(txtBidQuantity.Text) != issueMinQty && Convert.ToInt64(txtBidQuantity.Text) != issueMaxQty)
                    {
                        msg = "Please enter Quantity in multiples permissibile for this issue";
                        isBidValid = false;
                        return isBidValid;
                    }
                }
                if (bidAmountPayble > 0)
                    validBidSum += int.Parse(item.GetDataKeyValue("IssueBidNo").ToString());

                if (typeOfvalidation != 1 && bidAmountPayble <= 0 && int.Parse(item.GetDataKeyValue("IssueBidNo").ToString()) == 1)
                {
                    msg = "Bid found missing.Please enter the bids in sequence starting from the top!";
                    isBidValid = false;
                    return isBidValid;
                }
                else if ((!string.IsNullOrEmpty(txtBidQuantity.Text) || !string.IsNullOrEmpty(txtBidPrice.Text)) && bidAmountPayble == 0)
                {
                    msg = "Please complete the Bid Option" + item.GetDataKeyValue("IssueBidNo").ToString();
                    isBidValid = false;
                    return isBidValid;
                }
                bidId++;

            }
            if (validBidSum == 4)
            {
                msg = "Bid found missing.Please enter the bids in sequence starting from the top!";
                isBidValid = false;
                return isBidValid;
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
                string basic = dtOnlineIPOIssueList.Rows[0]["AIM_IsBookBuilding"].ToString();
                double.TryParse(dtOnlineIPOIssueList.Rows[0]["AIM_FloorPrice"].ToString(), out minBidPrice);
                double.TryParse(dtOnlineIPOIssueList.Rows[0]["AIM_CapPrice"].ToString(), out maxBidPrice);
                ImageButton btnOrderEdit = (ImageButton)dataform.FindControl("btnOrderEdit");
                //ImageButton btnOrderCancel = (ImageButton)dataform.FindControl("btnOrderCancel");
                string transactionType = Convert.ToString(RadGridIPOBid.MasterTableView.DataKeyValues[e.Item.ItemIndex]["COID_TransactionType"]);
                int chk = 0;
                if (e.Item.RowIndex != -1)
                {
                    rvQuantity.MinimumValue = minQuantity.ToString();
                    rvQuantity.MaximumValue = maxQuantity.ToString();

                    rvBidPrice.MinimumValue = minBidPrice.ToString();
                    rvBidPrice.MaximumValue = maxBidPrice.ToString();

                    if (basic == "Fixed" && dataform.RowIndex == 4)
                    {
                        int currentRowindex = (dataform.RowIndex / 4) - 1;
                        CheckBox chkCutOff = (CheckBox)RadGridIPOBid.MasterTableView.Items[currentRowindex]["CheckCutOff"].FindControl("cbCutOffCheck");
                        chkCutOff.Checked = true;
                        chkCutOff.Enabled = false;
                        ReseIssueBidValues(currentRowindex, false);

                    }
                }
                if (Request.QueryString["orderId"] != null && (e.Item.ItemIndex != -1))
                {
                    RadGridIPOBid.MasterTableView.GetColumn("COID_ExchangeRefrenceNo").Visible = true;
                    RadGridIPOBid.MasterTableView.GetColumn("TransactionType").Visible = true;
                    CheckBox chkCutOff = (CheckBox)dataform.FindControl("cbCutOffCheck");
                    TextBox txtBidQuantity = (TextBox)dataform.FindControl("txtBidQuantity");
                    TextBox txtBidPrice = (TextBox)dataform.FindControl("txtBidPrice");
                    int bidNo = int.Parse(RadGridIPOBid.MasterTableView.DataKeyValues[e.Item.ItemIndex]["IssueBidNo"].ToString());

                    int count = 0;
                    //if (ViewState["Checked"] == null)
                    //{
                    //    if (chkCutOff.Checked)
                    //        
                    //}
                    if (chkCutOff.Checked)
                    {
                        ViewState["Checked"] = 1;
                    }
                    if ((bidNo == 3 || bidNo == 2) && ViewState["Checked"] != null)
                    {
                        txtBidQuantity.Enabled = false;
                        txtBidPrice.Enabled = false;
                    }
                    if (chkCutOff.Checked == true && bidNo == 1)
                        txtBidPrice.Enabled = false;
                    if (ViewState["OderExtractStep"].ToString() != "" && ViewState["OderExtractStep"].ToString() != "UB")
                    {
                        btnOrderEdit.Visible = false;
                        lnkTermsCondition.Enabled = false;
                    }


                }

            }
            //else if (e.Item is GridFooterItem)
            //{
            //    GridFooterItem footerItem = (GridFooterItem)e.Item;
            //    CompareValidator cmpMaxBidAmount = (CompareValidator)footerItem.FindControl("cmpFinalBidAmountPayable");

            //    cmpMaxBidAmount.ValueToCompare = 0.ToString();
            //}
        }
        protected void lnlktoviewIPOissue_Click(object sender, EventArgs e)
        {
            if (Session["PageDefaultSetting"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('IPOIssueList');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "IPOIssueList", "loadcontrol('IPOIssueList');", true);
            }
        }
        protected void ViewIPOOrder(int orderId)
        {
            bool isRMSDebit; bool orderIscanclled; string OderExtractStep = string.Empty;
            DataTable dtIPOORder = onlineIPOOrderBo.GetIPOIOrderList(orderId, out isRMSDebit, out orderIscanclled, out OderExtractStep);
            ViewState["DtIPOViewOrder"] = dtIPOORder;
            ViewState["orderIscanclled"] = orderIscanclled;
            ViewState["OderExtractStep"] = OderExtractStep;
            BindIPOBidGrid(3);
            ViewState["isRMSDebit"] = isRMSDebit;
            if (dtIPOORder.Rows.Count > 0)
            {
                if (Cache["IPOTransactList" + userVo.UserId.ToString()] != null)
                {
                    Cache.Remove("IPOTransactList" + userVo.UserId.ToString());
                }
                Cache.Insert("IPOTransactList" + userVo.UserId.ToString(), dtIPOORder);

                RadGridIPOBid.DataSource = dtIPOORder;
                RadGridIPOBid.DataBind();
                foreach (DataRow dr1 in dtIPOORder.Rows)
                {
                    GridFooterItem ftItemAmount = (GridFooterItem)RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer)[0];
                    //string transactionType = Convert.ToString(RadGridIPOBid.MasterTableView.DataKeyValues[RadGridIPO.ItemIndex]["COID_TransactionType"]);
                    //&& (transactionType !="D" || transactionType !="ND")
                    Label lblFinalBidAmountPayable = (Label)ftItemAmount["BidAmountPayable"].FindControl("lblFinalBidAmountPayable");
                    lblFinalBidAmountPayable.Text = dr1["payable"].ToString();
                    decimal maxPaybleAmount = Convert.ToDecimal(((TextBox)ftItemAmount.FindControl("txtFinalBidValue")).Text);//accessing Button inside 
                    if (!string.IsNullOrEmpty(dr1["payable"].ToString()))
                        maxPaybleAmount = Convert.ToDecimal(dr1["payable"].ToString());
                    ViewState["maxPaybleAmount"] = maxPaybleAmount;
                }
            }
        }
        protected void btnOrderEdit_OnClick(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgbutton = (ImageButton)sender;
                GridDataItem gvr = (GridDataItem)imgbutton.NamingContainer;
                TextBox txtBidQuantity = (TextBox)gvr.FindControl("txtBidQuantity");
                TextBox txtBidPrice = (TextBox)gvr.FindControl("txtBidPrice");
                CheckBox chkCutOff = (CheckBox)gvr.FindControl("cbCutOffCheck");
                txtBidQuantity.Enabled = true;
                txtBidPrice.Enabled = true;
                chkCutOff.Enabled = true;
                if (chkCutOff.Checked == true)
                    txtBidPrice.Enabled = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OnlineOrderBo.cs:GetClientMFAccessStatus()");

                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }


        protected void btnUpdateIPOdrder_OnClick(object sender, EventArgs e)
        {
            chkTermsCondition.Checked = true;
            UpdateIPOOrder();
        }
        protected void UpdateIPOOrder()
        {
            string userMessage = String.Empty;
            bool accountDebitStatus = false;
            double totalBidAmount = 0;
            double totalBidAmountPayable = 0;
            string applicationNo = String.Empty;
            string apllicationNoStatus = String.Empty;
            double maxPaybleBidAmount = 0;
            DateTime cutOff = DateTime.Now;
            bool isCutOffTimeOver = false;
            int result = 0;
            string errorMsg = string.Empty;
            string message = string.Empty;
            bool isBidsVallid = false;
            Page.Validate();
            if (Page.IsValid)
            {
                isBidsVallid = ValidateIPOBids(out errorMsg, 1);
                if (!isBidsVallid)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('" + errorMsg + "');", true);
                    return;
                }
                else
                {

                    double availableBalance = (double)onlineIPOOrderBo.GetUserRMSAccountBalance(customerVo.AccountId);

                    int issueId = Convert.ToInt32(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_IssueId"].ToString());
                    if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_CutOffTime"].ToString()))
                        cutOff = Convert.ToDateTime(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_CutOffTime"].ToString());
                    DataTable dtIPOBidTransactionDettails = new DataTable();
                    dtIPOBidTransactionDettails.Columns.Add("IssueBidNo", typeof(Int16));
                    dtIPOBidTransactionDettails.Columns.Add("IsCutOffApplicable", typeof(Int16));
                    dtIPOBidTransactionDettails.Columns.Add("IPOIssueBidQuantity", typeof(Int64), null);
                    dtIPOBidTransactionDettails.Columns.Add("IPOIssueBidPrice", typeof(decimal), null);
                    dtIPOBidTransactionDettails.Columns.Add("IPOIssueBidAmount", typeof(decimal), null);
                    dtIPOBidTransactionDettails.Columns.Add("IPOIssueBidAmountPayable", typeof(decimal), null);
                    dtIPOBidTransactionDettails.Columns.Add("TransactionStatusCode", typeof(Int16));
                    dtIPOBidTransactionDettails.Columns.Add("COID_DetailsId", typeof(Int16));
                    dtIPOBidTransactionDettails.Columns.Add("MaxBidAmount", typeof(decimal), null);
                    DataRow drIPOBid;

                    //onlineIPOOrderVo.CustomerId = customerVo.CustomerId;
                    //onlineIPOOrderVo.IssueId = issueId;
                    //onlineIPOOrderVo.AssetGroup = "IP";
                    //onlineIPOOrderVo.IsOrderClosed = false;
                    //onlineIPOOrderVo.IsOnlineOrder = true;
                    //onlineIPOOrderVo.IsDeclarationAccepted = true;
                    //onlineIPOOrderVo.OrderDate = DateTime.Now;

                    int radgridRowNo = 0;
                    foreach (GridDataItem radItem in RadGridIPOBid.MasterTableView.Items)
                    {
                        drIPOBid = dtIPOBidTransactionDettails.NewRow();

                        CheckBox chkCutOff = (CheckBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["CheckCutOff"].FindControl("cbCutOffCheck");
                        TextBox txtBidQuantity = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidQuantity"].FindControl("txtBidQuantity");
                        TextBox txtBidPrice = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidPrice"].FindControl("txtBidPrice");
                        TextBox txtBidAmount = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidAmount"].FindControl("txtBidAmount");
                        TextBox txtBidAmountPayable = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidAmountPayable"].FindControl("txtBidAmountPayable");
                        drIPOBid["IssueBidNo"] = RadGridIPOBid.MasterTableView.DataKeyValues[radgridRowNo]["IssueBidNo"].ToString();
                        drIPOBid["COID_DetailsId"] = int.Parse(RadGridIPOBid.MasterTableView.DataKeyValues[radgridRowNo]["COID_DetailsId"].ToString());
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

                        if (!string.IsNullOrEmpty(txtBidAmountPayable.Text.Trim()))
                        {
                            drIPOBid["IPOIssueBidAmountPayable"] = txtBidAmountPayable.Text.Trim();
                            totalBidAmountPayable += Convert.ToDouble(txtBidAmountPayable.Text.Trim());
                        }
                        else
                            drIPOBid["IPOIssueBidAmountPayable"] = DBNull.Value;
                        if (!string.IsNullOrEmpty(txtBidAmount.Text.Trim()))
                            drIPOBid["TransactionStatusCode"] = 1;
                        else
                            drIPOBid["TransactionStatusCode"] = 5;
                        GridFooterItem footer = (GridFooterItem)RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer)[0];
                        Label lblFinalBidAmountPayable = (Label)footer.FindControl("lblFinalBidAmountPayable");
                        drIPOBid["MaxBidAmount"] = lblFinalBidAmountPayable.Text;

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

                    if (DateTime.Now.TimeOfDay > cutOff.TimeOfDay && cutOff.TimeOfDay < System.TimeSpan.Parse("23:59:59"))
                        isCutOffTimeOver = true;
                    if (!CheckIPOBidOrderChange(dtIPOBidTransactionDettails, out message))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Can not update. No changes have been made to any of the bid');", true);
                        return;
                    }

                    if (ViewState["Update"] == null)
                        if (!ValidateIPOOrderUpdate(dtIPOBidTransactionDettails, out message))
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('" + message + "');", true);
                            return;
                        }
                    if (Convert.ToBoolean(ViewState["isRMSDebit"].ToString()))
                    {
                        if (maxPaybleBidAmount != double.Parse(ViewState["maxPaybleAmount"].ToString()))
                        {
                            if (maxPaybleBidAmount > double.Parse(ViewState["maxPaybleAmount"].ToString()))
                            {
                                double balance = maxPaybleBidAmount - double.Parse(ViewState["maxPaybleAmount"].ToString());
                                accountDebitStatus = onlineIPOOrderBo.DebitRMSUserAccountBalanceOrderUpdate(customerVo.AccountId, -balance, int.Parse(Request.QueryString["orderId"]));
                                if (accountDebitStatus == true)
                                    result = onlineIPOOrderBo.UpdateIPOBidOrderDetails(userVo.UserId, dtIPOBidTransactionDettails, int.Parse(Request.QueryString["orderId"]), maxPaybleBidAmount - double.Parse(ViewState["maxPaybleAmount"].ToString()));
                                else
                                {
                                    ShowMessage("Order Cannot be Modified, due to Insufficient Balance.", "F");
                                    return;
                                }
                            }
                            else
                            {
                                result = onlineIPOOrderBo.UpdateIPOBidOrderDetails(userVo.UserId, dtIPOBidTransactionDettails, int.Parse(Request.QueryString["orderId"]), maxPaybleBidAmount - double.Parse(ViewState["maxPaybleAmount"].ToString()));
                                double balance = double.Parse(ViewState["maxPaybleAmount"].ToString()) - maxPaybleBidAmount;
                                accountDebitStatus = onlineIPOOrderBo.DebitRMSUserAccountBalance(customerVo.AccountId, +balance, int.Parse(Request.QueryString["orderId"]));
                            }

                            availableBalance = (double)onlineIPOOrderBo.GetUserRMSAccountBalance(customerVo.AccountId);
                            lblAvailableLimits.Text = availableBalance.ToString();
                        }
                        else
                        {
                            result = onlineIPOOrderBo.UpdateIPOBidOrderDetails(userVo.UserId, dtIPOBidTransactionDettails, int.Parse(Request.QueryString["orderId"]), maxPaybleBidAmount - double.Parse(ViewState["maxPaybleAmount"].ToString()));
                        }
                        btnUpdateIPOdrder.Visible = false;
                        btnOrderCancel.Visible = false;
                        ShowMessage("IPO Order Updated Successfully,Order reference no. is " + Request.QueryString["orderId"],"S");
                    }
                    else
                    {
                        if (maxPaybleBidAmount != double.Parse(ViewState["maxPaybleAmount"].ToString()))
                        {
                            double balance = maxPaybleBidAmount - double.Parse(ViewState["maxPaybleAmount"].ToString());
                            if (maxPaybleBidAmount > double.Parse(ViewState["maxPaybleAmount"].ToString()))
                            {
                                accountDebitStatus = onlineIPOOrderBo.DebitRMSUserAccountBalanceOrderUpdate(customerVo.AccountId, -balance, int.Parse(Request.QueryString["orderId"]));
                                if (accountDebitStatus == true)
                                {
                                    result = onlineIPOOrderBo.UpdateIPOBidOrderDetails(userVo.UserId, dtIPOBidTransactionDettails, int.Parse(Request.QueryString["orderId"]), maxPaybleBidAmount - double.Parse(ViewState["maxPaybleAmount"].ToString()));
                                    ShowMessage("IPO Order Updated Successfully,Order reference no. is " + Request.QueryString["orderId"],"S");
                                }
                                else
                                {
                                    ShowMessage("Order Cannot be Modified, due to Insufficient Balance.", "F");
                                    return;
                                }
                                availableBalance = (double)onlineIPOOrderBo.GetUserRMSAccountBalance(customerVo.AccountId);
                                lblAvailableLimits.Text = availableBalance.ToString();
                                btnUpdateIPOdrder.Visible = false;
                                btnOrderCancel.Visible = false;
                            }
                            else
                            {
                                string returnbalance = balance.ToString();
                                result = onlineIPOOrderBo.UpdateIPOBidOrderDetails(userVo.UserId, dtIPOBidTransactionDettails, int.Parse(Request.QueryString["orderId"]), maxPaybleBidAmount - double.Parse(ViewState["maxPaybleAmount"].ToString()));
                                btnUpdateIPOdrder.Visible = false;
                                btnOrderCancel.Visible = false;
                                ShowMessage("Note that amount of RS  " + returnbalance.TrimStart('-') + "  will credited from Registrar.","S");
                            }
                        }
                        else
                        {
                            result = onlineIPOOrderBo.UpdateIPOBidOrderDetails(userVo.UserId, dtIPOBidTransactionDettails, int.Parse(Request.QueryString["orderId"]), maxPaybleBidAmount - double.Parse(ViewState["maxPaybleAmount"].ToString()));
                            ShowMessage("IPO Order Updated Successfully,Order reference no. is " + Request.QueryString["orderId"],"S");
                        }
                    }
                }
            }
        }
        protected void btnOrderCancel_OnClick(object sender, EventArgs e)
        {
            //ImageButton btnDelete = (ImageButton)sender;
            //GridDataItem RadGridIPO = (GridDataItem)btnDelete.NamingContainer;
            try
            {
                string confirmValue = hdnOrderCancel.Value;
                if (confirmValue == "Yes")
                {
                    foreach (GridDataItem radItem in RadGridIPOBid.MasterTableView.Items)
                    {
                        TextBox txtBidQuantity = (TextBox)radItem.FindControl("txtBidQuantity");
                        TextBox txtBidPrice = (TextBox)radItem.FindControl("txtBidPrice");
                        TextBox txtBidAmountPayable = (TextBox)radItem.FindControl("txtBidAmountPayable");
                        TextBox txtBidAmount = (TextBox)radItem.FindControl("txtBidAmount");

                        txtBidAmountPayable.Text = "";
                        txtBidAmount.Text = "";
                        txtBidQuantity.Text = "";
                        txtBidPrice.Text = "";
                        txtBidQuantity.Enabled = false;
                        txtBidPrice.Enabled = false;
                        double[] bidMaxPayableAmount = new double[3];
                        int count = 0;
                        double finalBidPayableAmount = 0;
                        foreach (GridDataItem item in RadGridIPOBid.MasterTableView.Items)
                        {
                            TextBox txtBidAmountPayabl = (TextBox)item.FindControl("txtBidAmountPayable");
                            string transactionType = Convert.ToString(RadGridIPOBid.MasterTableView.DataKeyValues[count]["COID_TransactionType"]);

                            if (!string.IsNullOrEmpty(txtBidAmountPayabl.Text.Trim()) && (transactionType == "N" || transactionType == "NC" || transactionType == "M"))
                            {
                                bidMaxPayableAmount[count] = Convert.ToDouble(txtBidAmountPayabl.Text);
                            }
                            count = count + 1;

                        }

                        finalBidPayableAmount = bidMaxPayableAmount.Max();
                        GridFooterItem ftItemAmount = (GridFooterItem)RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer)[0];
                        Label lblFinalBidAmountPayable = (Label)ftItemAmount["BidAmountPayable"].FindControl("lblFinalBidAmountPayable");
                        lblFinalBidAmountPayable.Text = finalBidPayableAmount.ToString();


                    }
                    ViewState["Update"] = 1;
                    chkTermsCondition.Checked = true;
                    UpdateIPOOrder();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OnlineOrderBo.cs:GetClientMFAccessStatus()");

                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        protected bool ValidateIPOOrderUpdate(DataTable dtorder, out string message)
        {
            message = string.Empty;
            bool isvalidateUpdate = true;
            DataTable dtIPOORder = (DataTable)ViewState["DtIPOViewOrder"];
            foreach (DataRow dtsourceorder in dtIPOORder.Rows)
            {
                foreach (DataRow dtDestination in dtorder.Rows)
                {
                    if (dtsourceorder["BidPrice"].ToString() != "" && dtDestination["IPOIssueBidPrice"].ToString() == "" && dtsourceorder["IssueBidNo"].ToString() == dtDestination["IssueBidNo"].ToString())
                    {
                        message = "You cannot make BidOption " + dtsourceorder["IssueBidNo"].ToString() + " blank";
                        isvalidateUpdate = false;
                        break;
                    }

                }
            }
            return isvalidateUpdate;
        }
        protected bool CheckIPOBidOrderChange(DataTable dtorder, out string message)
        {
            message = string.Empty;
            bool isvalidateUpdate = true;
            DataTable dtIPOORder = (DataTable)ViewState["DtIPOViewOrder"];
            {
                DataTable dtMerged = (from a in dtorder.AsEnumerable()
                                      join b in dtIPOORder.AsEnumerable()
                                      on new { X = a["IPOIssueBidPrice"].ToString(), Y = a["IPOIssueBidQuantity"].ToString() } equals new { X = b["BidPrice"].ToString(), Y = b["BidQty"].ToString() }
                                      into g
                                      where g.Count() > 0
                                      select a).CopyToDataTable();

                int count = dtMerged.Rows.Count;
                if (count == 3)
                    isvalidateUpdate = false;
            }
            //foreach (DataRow dtsourceorder in dtIPOORder.Rows)
            //{
            //    foreach (DataRow dtDestination in dtorder.Rows)
            //    {
            //        if ((dtsourceorder["BidPrice"].ToString() == dtDestination["IPOIssueBidPrice"].ToString() || dtsourceorder["BidQty"].ToString() == dtDestination["IPOIssueBidQuantity"].ToString()) && dtsourceorder["IssueBidNo"].ToString() == dtDestination["IssueBidNo"].ToString())
            //        {
            //            message = "You have not made any change to bid";
            //            isvalidateUpdate = false;
            //            break;
            //        }

            //    }
            //}
            return isvalidateUpdate;
        }
    }
}
