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
using System.Text.RegularExpressions;


namespace WealthERP.OnlineOrderManagement
{
    public partial class NCDIssueTransact : System.Web.UI.UserControl
    {
        OnlineBondOrderBo OnlineBondBo = new OnlineBondOrderBo();
        OnlineBondOrderVo OnlineBondVo = new OnlineBondOrderVo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorVo adviserVo;
        bool RESULT = false;
        int customerId;
        double sum = 0;
        int Quantity = 0;
        int orderId = 0;
        int IssuerId = 0;
        int seriesId = 0;
        //int selectedRowIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            ShowAvailableLimits();            
            if (!IsPostBack)
            {
                Session["sum"] = null;
                Session["Qty"] = null;
                BindKYCDetailDDl();
                if (Request.QueryString["OrderId"] != null && Request.QueryString["IssuerId"] != null && Request.QueryString["Issuername"] != null)
                {
                    orderId = int.Parse(Request.QueryString["OrderId"].ToString());
                    IssuerId = int.Parse(Request.QueryString["IssuerId"].ToString());
                    string Issuername = Request.QueryString["Issuername"].ToString();
                    lblIssuer.Text = "Selected Issue Name :" + Issuername;
                    ViewState["orderId"] = orderId;
                    ViewState["IssuerId"] = IssuerId;
                    ViewState["seriesId"] = seriesId;
                    ddIssuerList.Visible = false;
                    btnConfirm.Visible = false;
                    tdsubmit.Visible = false;
                }
                else if (Request.QueryString["IssuerId"] != null && Request.QueryString["Issuename"] != null)
                {
                    IssuerId = int.Parse(Request.QueryString["IssuerId"].ToString());
                    string Issuename = Request.QueryString["Issuename"].ToString();
                    lblIssuer.Text = "Selected Issue Name :" + Issuename;
                    //int IssueIdN = Convert.ToInt32(IssueId);
                    ddIssuerList.Visible = false;
                    btnConfirm.Visible = false;
                    BindStructureRuleGrid(IssuerId);
                }
                else
                {
                    BindDropDownListIssuer();
                    lblIssuer.Text = "Kindly Select Issue Name:";
                    btnConfirm.Enabled = true;
                }
                if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
                {
                    if (Request.QueryString["strAction"].Trim() == "View")
                    {
                        Viewdetails(IssuerId);
                        btnConfirm.Visible = false;
                        tdsubmit.Visible = false;
                    }
                    if (Request.QueryString["strAction"].Trim() == "Edit")
                    {
                        Editdetails(IssuerId);
                        //btnConfirm.Visible = true;
                        //tdsubmit.Visible = true;
                        tdupdate.Visible = true;

                        // Viewdetails(IssuerId);
                    }
                }

            }
            if (Request.QueryString["customerId"] != null)
            {
                customerId = Convert.ToInt32(Request.QueryString["customerId"].ToString());
            }
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            IssuerId = int.Parse(ddIssuerList.SelectedValue.ToString());
            BindStructureRuleGrid(IssuerId);
        }
        protected void BindStructureRuleGrid(int IssuerId)
        {
            DataSet dsStructureRules = OnlineBondBo.GetLiveBondTransaction(IssuerId);
            DataTable dtTransact = dsStructureRules.Tables[0];
            if (dtTransact.Rows.Count > 0)
            {
                if (Cache["NCDTransactList" + adviserVo.advisorId.ToString()] == null)
                {
                    Cache.Insert("NCDTransactList" + adviserVo.advisorId.ToString(), dtTransact);
                }
                else
                {
                    Cache.Remove("NCDTransactList" + adviserVo.advisorId.ToString());
                    Cache.Insert("NCDTransactList" + adviserVo.advisorId.ToString(), dtTransact);
                }
                gvCommMgmt.DataSource = dtTransact;
                ViewState["Transact"] = dtTransact;
                gvCommMgmt.DataBind();
                pnlNCDTransactact.Visible = true;
                //ibtExportSummary.Visible = true;
                trSubmit.Visible = true;
            }
            else
            {
               // ibtExportSummary.Visible = false;
                gvCommMgmt.DataSource = dtTransact;
                gvCommMgmt.DataBind();
                pnlNCDTransactact.Visible = true;
                trSubmit.Visible = false;
            }
        }
        protected void BindDropDownListIssuer()
        {
            DataSet dsStructureRules = OnlineBondBo.GetLiveBondTransactionList();
            ddIssuerList.DataTextField = dsStructureRules.Tables[0].Columns["PFIIM_IssuerId"].ToString();
            ddIssuerList.DataValueField = dsStructureRules.Tables[0].Columns["AIM_IssueId"].ToString();
            ddIssuerList.DataSource = dsStructureRules.Tables[0];
            ddIssuerList.DataBind();
        }
        protected void BindKYCDetailDDl()
        {
            DataSet dsNomineeAndJointHolders = OnlineBondBo.GetNomineeJointHolder(customerVo.CustomerId);
            StringBuilder strbNominee = new StringBuilder();
            StringBuilder strbJointHolder = new StringBuilder();

            foreach (DataRow dr in dsNomineeAndJointHolders.Tables[0].Rows)
            {
                strbJointHolder.Append(dr["AMFE_JointNomineeName"].ToString() + ",");
                strbNominee.Append(dr["AMFE_JointNomineeName"].ToString() + ",");
            }
            lblNomineeTwo.Text = strbNominee.ToString().TrimEnd(',');
            lblHolderTwo.Text = strbJointHolder.ToString().TrimEnd(',');
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            int rowindex1 = ((GridDataItem)((TextBox)sender).NamingContainer).RowIndex;
            int rowindex = (rowindex1 / 2) - 1;
            TextBox txtQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[rowindex]["Quantity"].FindControl("txtQuantity");

            if (!string.IsNullOrEmpty(txtQuantity.Text))
            {
                string message = string.Empty;
                int rowno = 0;
                int PFISD_BidQty = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[rowindex]["AIM_BestBidQuantity"].ToString());
                int PFISD_InMultiplesOf = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[rowindex]["AIM_TradingInMultipleOf"].ToString());
                Regex re = new Regex(@"[@\\*+#^\\.\$]+");
                if (re.IsMatch(txtQuantity.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter only Valid Numbers & in multiples of 1')", true);
                    txtQuantity.Text = "";
                }
                else
                {
                    int Qty = Convert.ToInt32(txtQuantity.Text);

                    //if (Qty < PFISD_BidQty)
                    //{
                    //    // lblMSG.Text = "Bid Quantity should not be less than the Minimum BID Qty i.e." + PFISD_BidQty.ToString();
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Bid Quantity should not be less than the Minimum BID Qty')+'?PFISD_BidQty=" + PFISD_BidQty + "'", true);
                    //    txtQuantity.Text = "";
                    //    return;
                    //}
                    int Mod = Qty % PFISD_InMultiplesOf;
                    if (Mod != 0)
                    {
                        //lblMSG.Text = "Bid Quantity should be in Allowed Multiplication i.e. i.e." + PFISD_InMultiplesOf.ToString();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Bid Quantity should be in Allowed Multiplication')+'?PFISD_InMultiplesOf=" + PFISD_InMultiplesOf + "'", true);
                        txtQuantity.Text = "";
                        return;
                    }
                    double AIM_FaceValue = Convert.ToDouble(gvCommMgmt.MasterTableView.DataKeyValues[rowindex]["AIM_FaceValue"].ToString());
                    TextBox txtAmount = (TextBox)gvCommMgmt.MasterTableView.Items[rowindex]["Amount"].FindControl("txtAmount");
                    txtAmount.Text = Convert.ToString(Qty * AIM_FaceValue);
                    CheckBox cbSelectOrder = (CheckBox)gvCommMgmt.MasterTableView.Items[rowindex]["Check"].FindControl("cbOrderCheck");
                    cbSelectOrder.Checked = true;
                    
                        foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
                        {
                            TextBox txtsumQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[rowno]["Quantity"].FindControl("txtQuantity");
                            TextBox txtsumAmount = (TextBox)gvCommMgmt.MasterTableView.Items[rowno]["Amount"].FindControl("txtAmount");
                            GridFooterItem footerItem = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                            Label lblQty = (Label)footerItem.FindControl("lblQuantity");
                            GridFooterItem footerItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                            Label lblSum = (Label)footerItemAmount.FindControl("lblAmount");
                            if(cbSelectOrder.Checked == true)
                                if (!string.IsNullOrEmpty(txtsumQuantity.Text) && !string.IsNullOrEmpty(txtsumAmount.Text))
                                {    //if (Session["Qty"] == null)
                                    //    Session["Qty"] = 0;
                                    Quantity = Quantity + Convert.ToInt32(txtsumQuantity.Text);
                                    //Session["Qty"] = Quantity;
                                    // if (Session["sum"] == null)
                                    //     Session["sum"] = 0;
                                    sum = sum + Convert.ToInt32(txtsumAmount.Text);
                                    //  Session["sum"] = sum;
                                    // ViewState["sum"] = sum;
                                    //GridFooterItem footerItem = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                                    //Label lblQty = (Label)footerItem.FindControl("lblQuantity");
                                    lblQty.Text = Quantity.ToString();

                                    //GridFooterItem footerItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                                    //Label lblSum = (Label)footerItemAmount.FindControl("lblAmount");
                                    lblSum.Text = sum.ToString();
                                }
                            if (Quantity > PFISD_BidQty)
                            {
                                message = "Order cannot be processed.Quantity should not be greater than Bid Quantity";
                               // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Bid Quantity should not be less than the Minimum BID Qty')+'?PFISD_BidQty=" + PFISD_BidQty + "'", true);
                                ShowMessage(message);
                                txtsumQuantity.Text = "";
                                txtsumAmount.Text = "";
                                lblQty.Text = "";
                                lblSum.Text = "";
                                return;
                            }
                            if (rowno < gvCommMgmt.MasterTableView.Items.Count)
                                rowno++;
                            else
                                break;
                        }                    
                }

            }
        }
        protected void calculate()
        {
           
        
        }

        private string CreateUserMessage(int orderId, bool accountDebitStatus)
        {
            string userMessage = string.Empty;
            orderId = int.Parse(ViewState["OrderId"].ToString());
            if (orderId != 0 && accountDebitStatus == true)
            {
                //if (isCutOffTimeOver)
                //     userMessage = "Order placed successfully, Order reference no is " + orderId.ToString() + ", Order will process next business day";
                //else
                userMessage = "Order placed successfully, Order reference no is " + orderId.ToString();
            }
            else if (orderId != 0 && accountDebitStatus == false)
            {
                userMessage = "Order placed successfully,Order will not process due to insufficient balance, Order reference no is " + orderId.ToString();
            }
            else if (orderId == 0)
            {
                userMessage = "Order cannot be processed. Insufficient balance";

            }
            return userMessage;
        }

        protected void CreateNCDOrder()
        {
            bool accountDebitStatus = false;
            decimal availableBalance = OnlineBondBo.GetUserRMSAccountBalance(customerVo.AccountId);
            int OrderId;
            string message = string.Empty;            
            if (availableBalance >= 0)
            {
                OrderId = int.Parse(ViewState["OrderId"].ToString());               

                if (OrderId != 0 && !string.IsNullOrEmpty(customerVo.AccountId))
                {
                    accountDebitStatus = OnlineBondBo.DebitRMSUserAccountBalance(customerVo.AccountId, -OnlineBondVo.Amount, orderId);
                }
                if (OrderId != 0)
                {
                    ShowAvailableLimits();
                    message = CreateUserMessage(orderId, accountDebitStatus);
                    ShowMessage(message);
                }
            }
            else
            {
                message = CreateUserMessage(0, false);
                ShowMessage(message);
            }
        }
        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
        }
        protected void lbconfirmOrder_Click(object sender, EventArgs e)
        {

        }
        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            Button Button = (Button)sender;
            int MaxAppNo = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[0]["AIM_MaxApplNo"].ToString());
            DataTable dt = new DataTable();

            //Need to be collect from Session...
            dt.Columns.Add("CustomerId");
            dt.Columns.Add("AID_IssueDetailId");
            dt.Columns.Add("AIM_IssueId");            
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");           
            int rowNo = 0;
            int tableRow = 0;
            foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
            {
                TextBox txtQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[rowNo]["Quantity"].FindControl("txtQuantity");
                OnlineBondVo.CustomerId = customerVo.CustomerId;
                OnlineBondVo.BankAccid = 1002321521;
                OnlineBondVo.PFISD_SeriesId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AID_IssueDetailId"].ToString());
                OnlineBondVo.IssuerId = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AIM_IssueId"].ToString());
               
                CheckBox Check = (CheckBox)gvCommMgmt.MasterTableView.Items[rowNo]["Check"].FindControl("cbOrderCheck");
                if (Check.Checked == true)
                {
                    if (!string.IsNullOrEmpty(txtQuantity.Text))
                    {
                        OnlineBondVo.Qty = Convert.ToInt32(txtQuantity.Text);
                        TextBox txtAmount = (TextBox)gvCommMgmt.MasterTableView.Items[rowNo]["Amount"].FindControl("txtAmount");
                        OnlineBondVo.Amount = Convert.ToDouble(txtAmount.Text);                       
                        dt.Rows.Add();
                        dt.Rows[tableRow]["CustomerId"] = OnlineBondVo.CustomerId;
                        dt.Rows[tableRow]["AID_IssueDetailId"] = OnlineBondVo.PFISD_SeriesId;
                        dt.Rows[tableRow]["AIM_IssueId"] = OnlineBondVo.IssuerId;                       
                        dt.Rows[tableRow]["Qty"] = OnlineBondVo.Qty;
                        dt.Rows[tableRow]["Amount"] = OnlineBondVo.Amount;
                    }
                    tableRow++;
                }
                if (rowNo < gvCommMgmt.MasterTableView.Items.Count)
                    rowNo++;
                else
                    break;

                if (!string.IsNullOrEmpty(txtQuantity.Text))
                {
                    int OrderIds;
                    OrderIds = OnlineBondBo.onlineBOndtransact(dt, adviserVo.advisorId);
                    ViewState["OrderId"] = OrderIds;
                    CreateNCDOrder();
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Please Enter Quantity');", true);
                }
            }
           
            //int OrderIds;
            //OrderIds = OnlineBondBo.onlineBOndtransact(dt, adviserVo.advisorId);
            //ViewState["OrderId"] = OrderIds;
            //CreateNCDOrder();
            // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Placed Successfully');", true);
            // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Order Placed Successfully')", true);
            //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueBooks','?customerId=" + customerVo.CustomerId + "');", true);
            // }
        }
        //protected Dictionary<string, double> CalculateFooterTotal()
      
        // }
        protected void gvCommMgmt_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //double sum = 0;
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem dataItem = (GridDataItem)e.Item;
            //    { if()

            //    sum += double.Parse((dataItem["txtAmount"].FindControl("txtAmount") as RadNumericTextBox).Value.ToString());
            //}
            //else if (e.Item is GridFooterItem)
            //{
            //    GridFooterItem footer = (GridFooterItem)e.Item;
            //    footer["Quantity"].Controls.Add(new LiteralControl("<span style='color: Black; font-weight: bold;'>Total freight on this page is:</span> "));
            //    (footer["Template1"].FindControl("txtAmount") as RadNumericTextBox).Value = Double.Parse(sum.ToString());
            //}


            //if (e.Item is GridFooterItem)
            //{
            //  GridFooterItem footer = (GridFooterItem)e.Item;
            // if (gvCommMgmt.Items.Count > 0)
            // {
            //   Dictionary<string, double> dic = CalculateFooterTotal();
            //   if (dic.Count > 0)
            //  {
            //      var keyValuePairAcquisitionCost = dic.FirstOrDefault(x => x.Key == "Amount");
            //      footer["Amount"].Text = keyValuePairAcquisitionCost.Value.ToString();

            //  }
            // }
            // }
        }
        protected void Viewdetails(int IssuerId)
        {
            DataSet dsStructureRules = OnlineBondBo.GetNCDTransactOrder(orderId, IssuerId);
            int ronum = 0;
            if (dsStructureRules.Tables[0].Rows.Count > 0)
            {
                gvCommMgmt.DataSource = dsStructureRules.Tables[0];
                gvCommMgmt.DataBind();
                pnlNCDTransactact.Visible = true;
              //  ibtExportSummary.Visible = true;
                trSubmit.Visible = true;
                foreach (GridDataItem gdi in gvCommMgmt.MasterTableView.Items)
                {
                    if (ronum < gvCommMgmt.MasterTableView.Items.Count)
                    {
                        TextBox txt = gvCommMgmt.Items[ronum].Cells[17].FindControl("txtQuantity") as TextBox;
                        txt.Enabled = false;
                        ronum++;
                    }
                }
                //GridFooterItem footerItem = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                //Label lblQty = (Label)footerItem.FindControl("lblQuantity");
                //Quantity = Convert.ToInt32(Session["Qty"]);
                //lblQty.Text = Quantity.ToString();

                //GridFooterItem footerItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                //Label lblSum = (Label)footerItemAmount.FindControl("lblAmount");
                //sum = Convert.ToInt32(ViewState["sum"]); 
                //lblSum.Text = sum.ToString();


            }
            else
            {
              //  ibtExportSummary.Visible = false;
                gvCommMgmt.DataSource = dsStructureRules.Tables[0];
                gvCommMgmt.DataBind();
                pnlNCDTransactact.Visible = true;
                trSubmit.Visible = false;
            }
        }

        protected void Editdetails(int IssuerId)
        {
            DataSet dsStructureRules = OnlineBondBo.GetNCDAllTransactOrder(orderId, IssuerId);
            //int ronum = 0;
            if (dsStructureRules.Tables[0].Rows.Count > 0)
            {
                gvCommMgmt.DataSource = dsStructureRules.Tables[0];
                gvCommMgmt.DataBind();
                pnlNCDTransactact.Visible = true;
                ibtExportSummary.Visible = true;
                trSubmit.Visible = true;
               
            }
            else
            {
                ibtExportSummary.Visible = false;
                gvCommMgmt.DataSource = dsStructureRules.Tables[0];
                gvCommMgmt.DataBind();
                pnlNCDTransactact.Visible = true;
                trSubmit.Visible = false;
            }
        }

        private void ShowAvailableLimits()
        {
            if (!string.IsNullOrEmpty(customerVo.AccountId))
            {
                lblAvailableLimits.Text = OnlineBondBo.GetUserRMSAccountBalance(customerVo.AccountId).ToString();
            }

        }
        protected void gvCommMgmt_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtIssueDetail;
            dtIssueDetail = (DataTable)Cache["NCDTransactList" + adviserVo.advisorId.ToString()];
            if (dtIssueDetail != null)
            {
                gvCommMgmt.DataSource = dtIssueDetail;
            }

        }
        protected void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            //orderId=int.Parse(ViewState["orderId"].ToString());
            //IssuerId = int.Parse(ViewState["IssuerId"].ToString());
            //seriesId = 0; 

            //Button Button = (Button)sender;


            ////GridDataItem gdi = (GridDataItem)Button.NamingContainer;
            //int MaxAppNo = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[0]["AIM_MaxApplNo"].ToString());

            //DataTable dt = new DataTable();

            ////Need to be collect from Session...
            //dt.Columns.Add("CustomerId");
            //dt.Columns.Add("PFISD_SeriesId");
            //dt.Columns.Add("AIM_IssueId");
            //dt.Columns.Add("PFISM_SchemeId");
            //dt.Columns.Add("Qty");
            //dt.Columns.Add("Amount");
            //// dt.Columns.Add("AppLicationNo");
            //int rowNo = 0;
            //int tableRow = 0;
            ////if (seriesId == 0)
            ////{
            //    foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
            //    {
            //        TextBox txtQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[rowNo]["Quantity"].FindControl("txtQuantity");
            //        //OnlineBondVo.CustomerId = "ESI123456".ToString();
            //        OnlineBondVo.CustomerId = customerVo.CustomerId;
            //        OnlineBondVo.BankAccid = 1002321521;
            //        OnlineBondVo.PFISD_SeriesId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AID_Sequence"].ToString());
            //        OnlineBondVo.IssuerId = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AIM_IssueId"].ToString());
            //        OnlineBondVo.PFISM_SchemeId = 0;
            //            //int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["PFISM_SchemeId"].ToString());
            //        CheckBox Check = (CheckBox)gvCommMgmt.MasterTableView.Items[rowNo]["Check"].FindControl("cbOrderCheck");
            //        if (Check.Checked == true)
            //        {
            //            if (!string.IsNullOrEmpty(txtQuantity.Text))
            //            {
            //                OnlineBondVo.Qty = Convert.ToInt32(txtQuantity.Text);
            //                TextBox txtAmount = (TextBox)gvCommMgmt.MasterTableView.Items[rowNo]["Amount"].FindControl("txtAmount");
            //                OnlineBondVo.Amount = Convert.ToDouble(txtAmount.Text);
            //                //TextBox txtAmountAtMat = (TextBox)gvCommMgmt.MasterTableView.Items[0]["AmountAtMaturity"].FindControl("txtAmountAtMaturity");
            //                //OnlineBondVo.AmountAtMat = Convert.ToDouble(txtAmountAtMat.Text);
            //                dt.Rows.Add();
            //                dt.Rows[tableRow]["CustomerId"] = OnlineBondVo.CustomerId;
            //                dt.Rows[tableRow]["AID_Sequence"] = OnlineBondVo.PFISD_SeriesId;
            //                dt.Rows[tableRow]["AIM_IssueId"] = OnlineBondVo.IssuerId;
            //                dt.Rows[tableRow]["PFISM_SchemeId"] = OnlineBondVo.PFISM_SchemeId;
            //                dt.Rows[tableRow]["Qty"] = OnlineBondVo.Qty;
            //                dt.Rows[tableRow]["Amount"] = OnlineBondVo.Amount;

            //                OnlineBondVo.Qty = Convert.ToInt32(txtQuantity.Text);                           
            //                OnlineBondVo.Amount = Convert.ToDouble(txtAmount.Text);
            //            }
            //            tableRow++;
            //        }
            //        if (rowNo < gvCommMgmt.MasterTableView.Items.Count)
            //            rowNo++;
            //        else
            //            break;
            //    }
            // }
            //else
            //{
            //    foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
            //    {
            //        TextBox txtQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[rowNo]["Quantity"].FindControl("txtQuantity");
            //        //OnlineBondVo.CustomerId = "ESI123456".ToString();
            //        OnlineBondVo.CustomerId = customerVo.CustomerId;
            //        OnlineBondVo.BankAccid = 1002321521;
            //        OnlineBondVo.PFISD_SeriesId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["PFISD_SeriesId"].ToString());
            //        OnlineBondVo.IssuerId = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AIM_IssueId"].ToString());
            //        OnlineBondVo.PFISM_SchemeId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["PFISM_SchemeId"].ToString());
            //        CheckBox Check = (CheckBox)gvCommMgmt.MasterTableView.Items[rowNo]["Check"].FindControl("cbOrderCheck");
            //        if (Check.Checked == true)
            //        {
            //            if (!string.IsNullOrEmpty(txtQuantity.Text))
            //            {
            //                OnlineBondVo.Qty = Convert.ToInt32(txtQuantity.Text);
            //                TextBox txtAmount = (TextBox)gvCommMgmt.MasterTableView.Items[rowNo]["Amount"].FindControl("txtAmount");
            //                OnlineBondVo.Amount = Convert.ToDouble(txtAmount.Text);
            //            }
            //            tableRow++;
            //        }
            //        if (rowNo < gvCommMgmt.MasterTableView.Items.Count)
            //            rowNo++;
            //        else
            //            break;
            //    }
            //}
            //    IDictionary<string, string> OrderIds = new Dictionary<string, string>();
            //    OrderIds = OnlineBondBo.UpdateTransactOrder(dt, OnlineBondVo, adviserVo.advisorId, IssuerId, orderId, OnlineBondVo.PFISD_SeriesId);

            //   ViewState["OrderId"] = OrderIds;
            ////string CustId = Session["CustId"].ToString();
            //  if (OrderIds != null)
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueBooks','&customerId=" + customerVo.CustomerId + "');", true);
        }

    }

}


