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
using System.Drawing;
using System.Configuration;

namespace WealthERP.OnlineOrderManagement
{
    public partial class NCDIssueTransact : System.Web.UI.UserControl
    {
        OnlineBondOrderBo OnlineBondBo = new OnlineBondOrderBo();
        OnlineBondOrderVo OnlineBondVo = new OnlineBondOrderVo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorVo adviserVo;
        UserVo userVo;
        int customerId;
        double sum = 0;
        int Quantity = 0;
        int orderId = 0;
        int IssuerId = 0;
        int seriesId = 0;
        int minQty = 0;
        int maxQty = 0;
        int EligblecatId = 0;
        //int selectedRowIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            customerVo = (CustomerVo)Session["customerVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            ShowAvailableLimits();
            BindKYCDetailDDl();
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
                    lblIssuer.Text = "Selected Issue Name:" + Issuername;
                    ViewState["orderId"] = orderId;
                    ViewState["IssuerId"] = IssuerId;
                    ViewState["seriesId"] = seriesId;
                    ddIssuerList.Visible = false;
                    btnConfirm.Visible = false;
                    tdsubmit.Visible = false;
                    lnkback.Visible = true;
                    trTermsCondition.Visible = false;
                    //tdsubmit.Visible = false;
                }
                else if (Request.QueryString["IssuerId"] != null && Request.QueryString["Issuename"] != null && Request.QueryString["minQty"] != null && Request.QueryString["maxQty"] != null)
                {
                    IssuerId = int.Parse(Request.QueryString["IssuerId"].ToString());
                    string Issuename = Request.QueryString["Issuename"].ToString();
                    minQty = int.Parse(Request.QueryString["minQty"].ToString());
                    maxQty = int.Parse(Request.QueryString["maxQty"].ToString());
                    lblIssuer.Text = "Selected Issue Name :" + Issuename;
                    ViewState["minQty"] = minQty;
                    ViewState["maxQty"] = maxQty;
                    ViewState["IssueId"] = IssuerId;
                    //int IssueIdN = Convert.ToInt32(IssueId);
                    ddIssuerList.Visible = false;
                    btnConfirm.Visible = false;

                    BindStructureRuleGrid(IssuerId);
                    BindStructureRuleGrid();
                }
                else if (Request.QueryString["IssueId"] != null && Request.QueryString["Issuename"] != null)
                {
                    IssuerId = int.Parse(Request.QueryString["IssueId"].ToString());
                    string Issuename = Request.QueryString["Issuename"].ToString();
                    lblIssuer.Text = "Selected Issue Name :" + Issuename;
                    ddIssuerList.Visible = false;
                    btnConfirm.Visible = false;
                    BindStructureRuleGrid(IssuerId);
                    BindStructureRuleGrid();
                }
                else
                {
                    BindDropDownListIssuer();
                    pnlIssuList.Visible = false;
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
                        trTermsCondition.Visible = false;
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
        protected void BindStructureRuleGrid()
        {
            DataTable dtIssue = new DataTable();

            //1--- For Curent Issues
            if (Cache["NCDIssueList" + userVo.UserId.ToString()] != null)
            {
                DataTable dtIssueList = (DataTable)Cache["NCDIssueList" + userVo.UserId.ToString()];
                dtIssueList.DefaultView.RowFilter = "AIM_IssueId=" + IssuerId.ToString();
                dtIssue = dtIssueList.DefaultView.ToTable();
            }
            else
                dtIssue = OnlineBondBo.GetAdviserIssuerList(adviserVo.advisorId, IssuerId, 1, customerVo.CustomerId, Session["PageDefaultSetting"] == null ? 1 : 0, customerVo.TaxStatusCustomerSubTypeId).Tables[0];

            if (dtIssue.Rows.Count > 0)
            {
                if (Cache["NCDIssueList" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("NCDIssueList" + userVo.UserId.ToString(), dtIssue);
                }
                else
                {
                    Cache.Remove("NCDIssueList" + userVo.UserId.ToString());
                    Cache.Insert("NCDIssueList" + userVo.UserId.ToString(), dtIssue);
                }
                ibtExportSummary.Visible = false;
                gvIssueList.DataSource = dtIssue;
                gvIssueList.DataBind();
            }
            else
            {
                ibtExportSummary.Visible = false;
                gvIssueList.DataSource = dtIssue;
                gvIssueList.DataBind();

            }


        }
        protected void BindStructureRuleGrid(int IssuerId)
        {
            DataSet dsStructureRules = OnlineBondBo.GetLiveBondTransaction(IssuerId, customerVo.CustomerId, customerVo.TaxStatusCustomerSubTypeId);
            DataTable dtTransact = dsStructureRules.Tables[0];
            if (dtTransact.Rows.Count > 0)
            {
                if (Cache["NCDTransactList" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("NCDTransactList" + userVo.UserId.ToString(), dtTransact);
                }
                else
                {
                    Cache.Remove("NCDTransactList" + userVo.UserId.ToString());
                    Cache.Insert("NCDTransactList" + userVo.UserId.ToString(), dtTransact);
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
            DataSet dsStructureRules = OnlineBondBo.GetLiveBondTransactionList(adviserVo.advisorId);
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
                //strbJointHolder.Append(dr["CustomerName"].ToString() + ",");
                strbNominee.Append(dr["AMFE_JointNomineeName"].ToString() + ",");

                //strbJointHolder.Append(dr["AMFE_JointNomineeName"].ToString() + ",");
                //strbNominee.Append(dr["AMFE_JointNomineeName"].ToString() + ",");
            }
            lblNomineeTwo.Text = strbNominee.ToString().TrimEnd(',');
            lblHolderTwo.Text = customerVo.FirstName + ' ' + customerVo.LastName;
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

        protected void lblAmount_TextChanged(object sender, EventArgs e)
        {




        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            int rowindex1 = ((GridDataItem)((TextBox)sender).NamingContainer).RowIndex;
            int rowindex = (rowindex1 / 2) - 1;
            int issueId = Convert.ToInt32(Request.QueryString["IssuerId"]);
            string catName = string.Empty;
            string Description = string.Empty;
            int catId = 0;
            int issuedetId = 0;
            double AIM_FaceValue = 0.0;
            TextBox txtQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[rowindex]["Quantity"].FindControl("txtQuantity");
            if (!string.IsNullOrEmpty(txtQuantity.Text))
            {
                string message = string.Empty;
                int rowno = 0;
                int PFISD_InMultiplesOf = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[rowindex]["AIM_TradingInMultipleOf"].ToString());
                // Regex re = new Regex(@"[@\\*+#^\\.\$\-?A-Za-z]+");
                Regex re = new Regex(@"^[1-9]\d*$");
                if (re.IsMatch(txtQuantity.Text))
                {
                    int Qty = Convert.ToInt32(txtQuantity.Text);
                    int Mod = Qty % PFISD_InMultiplesOf;
                    if (Mod != 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please enter quantity greater than or equal to min quantity required and in multiples of 1')", true);
                        txtQuantity.Text = "";
                        return;
                    }
                    AIM_FaceValue = Convert.ToDouble(gvCommMgmt.MasterTableView.DataKeyValues[rowindex]["AID_SeriesFaceValue"].ToString());
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
                        if (cbSelectOrder.Checked == true)
                            if (!string.IsNullOrEmpty(txtsumQuantity.Text) && !string.IsNullOrEmpty(txtsumAmount.Text))
                            {

                                Quantity = Quantity + Convert.ToInt32(txtsumQuantity.Text);
                                ViewState["Qty"] = Quantity;
                                sum = sum + Convert.ToInt32(txtsumAmount.Text);
                                ViewState["Sum"] = sum;
                                lblQty.Text = Quantity.ToString();
                                lblSum.Text = sum.ToString();

                                //  lb1AvailbleCat.Visible = true;
                                OnlineBondBo.GetCustomerCat(issueId, customerVo.CustomerId, adviserVo.advisorId, Convert.ToDouble(lblSum.Text), ref catName, ref issuedetId, ref catId, ref Description);

                                //OnlineBondBo.GetCustomerCat(issueId, customerVo.CustomerId, adviserVo.advisorId, Convert.ToDouble(lblSum.Text), ref catName, ref issuedetId, ref catId, ref Description);
                                ViewState["CustCat"] = catName;
                                //ViewState["Description"] = Description;
                                lb1AvailbleCat.Text = " You have applied this issue under category : " + catName + "-" + Description;
                                ShowMessage(lb1AvailbleCat.Text);

                                //if (catName == string.Empty)
                                //    ShowMessage("Bid category Not Available");
                                //txtTotAmt_ValueChanged(null, new EventArgs());
                            }
                        if (rowno < gvCommMgmt.MasterTableView.Items.Count)
                        {
                            rowno++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please enter only Valid Numbers & in multiples of 1')", true);

                }
            }
            else
            {
                foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
                {
                    TextBox txtsumQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[CBOrder.ItemIndex]["Quantity"].FindControl("txtQuantity");
                    TextBox txtsumAmount = (TextBox)gvCommMgmt.MasterTableView.Items[CBOrder.ItemIndex]["Amount"].FindControl("txtAmount");
                    GridFooterItem footerItem = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                    Label lblQty = (Label)footerItem.FindControl("lblQuantity");
                    GridFooterItem footerItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                    Label lblSum = (Label)footerItemAmount.FindControl("lblAmount");
                    txtQuantity.Text = "";
                    txtsumQuantity.Text = "";
                    txtsumAmount.Text = " ";
                    lblQty.Text = "";
                    lblSum.Text = "";
                }

            }
        }


        protected void txtTotAmt_ValueChanged(object sender, EventArgs e)
        {
            //GridFooterItem footerItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
            //Label lblSum = (Label)footerItemAmount.FindControl("lblAmount");
            //lb1AvailbleCat.Visible = true;
            //lb1AvailbleCat.Text = "Available are:";
        }
        private string CreateUserMessage(int orderId, int Applicationno, bool accountDebitStatus, string aplicationNoStatus)
        {
            string userMessage = string.Empty;
            string cutOffTimeType = string.Empty;

            if (orderId != 0 && accountDebitStatus == true)
            {

                cutOffTimeType = OnlineBondBo.GetCutOFFTimeForCurent(orderId);
                if (cutOffTimeType == "2")
                    //if (cutOffTime == "Closed")
                    userMessage = "Order placed successfully, Order reference no is " + orderId.ToString() + ", Order will process next business day";
                else
                    userMessage = "Order placed successfully, Order reference no. is " + orderId.ToString() + " & Application no. " + Applicationno.ToString();

                ShowAvailableLimits();
            }


            else if (orderId == 0 & lblAvailableLimits.Text == "0")
            {
                userMessage = "Order cannot be processed. Insufficient balance";

            }

            else if (aplicationNoStatus == "Refill")
            {
                userMessage = "Order cannot be placed , Application oversubscribed. Please contact your relationship manager or contact call centre";
                //  userMessage = "Please Contact sbi team to fill Aplications";

            }
            else if (accountDebitStatus == false)
            {
                userMessage = "NO Rms Response";

            }
            else if (orderId == 0)
            {
                userMessage = "Order cannot be processed. Issue Got Closed";

            }
            return userMessage;
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
            int issueDetId = 0;
            int catId = 0;
            //string Custcategory="";
            // if (ViewState["CustCat"] != null)                     
            // Custcategory = (string)ViewState["CustCat"];
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
                //}
                //else
                //{
                //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
                //}
                Button Button = (Button)sender;
                minQty = int.Parse(ViewState["minQty"].ToString());
                maxQty = int.Parse(ViewState["maxQty"].ToString());
                int MaxAppNo = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[0]["AIM_MaxApplNo"].ToString());
                int FaceValue = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[0]["AIM_FaceValue"].ToString());
                DataTable dt = new DataTable();
                bool isValid = false;
                //Need to be collect from Session...
                dt.Columns.Add("CustomerId");
                dt.Columns.Add("AID_IssueDetailId");
                dt.Columns.Add("AIM_IssueId");
                dt.Columns.Add("Qty");
                dt.Columns.Add("Amount");
                dt.Columns.Add("CatId");
                dt.Columns.Add("AcceptableCatId");
                dt.Columns.Add("userId");
                int rowNo = 0;
                int tableRow = 0;
                foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
                {

                    TextBox txtQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[rowNo]["Quantity"].FindControl("txtQuantity");
                    if (txtQuantity.Text == "0" || txtQuantity.Text == string.Empty)
                    {
                        if (rowNo < gvCommMgmt.MasterTableView.Items.Count)
                        {
                            rowNo = rowNo + 1;
                        }
                        continue;
                    }
                    OnlineBondVo.CustomerId = customerVo.CustomerId;
                    OnlineBondVo.BankAccid = 1002321521;
                    OnlineBondVo.PFISD_SeriesId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AID_IssueDetailId"].ToString());
                    OnlineBondVo.IssueId = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AIM_IssueId"].ToString());
                    CheckBox Check = (CheckBox)gvCommMgmt.MasterTableView.Items[rowNo]["Check"].FindControl("cbOrderCheck");
                    catId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AIDCSR_Id"].ToString());
                    if (Check.Checked == true)
                    {
                        if (!string.IsNullOrEmpty(txtQuantity.Text))
                        {
                            isValid = true;
                            txtQuantity.Enabled = true;

                            string catName = string.Empty;
                            string Description = string.Empty;
                            OnlineBondVo.Qty = Convert.ToInt32(txtQuantity.Text);
                            TextBox txtAmount = (TextBox)gvCommMgmt.MasterTableView.Items[rowNo]["Amount"].FindControl("txtAmount");
                            OnlineBondVo.Amount = Convert.ToDouble(txtAmount.Text);
                            dt.Rows.Add();
                            dt.Rows[tableRow]["CustomerId"] = OnlineBondVo.CustomerId;
                            dt.Rows[tableRow]["AID_IssueDetailId"] = OnlineBondVo.PFISD_SeriesId;
                            dt.Rows[tableRow]["AIM_IssueId"] = OnlineBondVo.IssueId;
                            dt.Rows[tableRow]["Qty"] = OnlineBondVo.Qty;
                            dt.Rows[tableRow]["Amount"] = OnlineBondVo.Amount;
                            dt.Rows[tableRow]["userid"] = userVo.UserId;
                            GridFooterItem footerItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                            Label lblSum = (Label)footerItemAmount.FindControl("lblAmount");


                            OnlineBondBo.GetCustomerCat(OnlineBondVo.IssueId, customerVo.CustomerId, adviserVo.advisorId, Convert.ToDouble(lblSum.Text), ref catName, ref issueDetId, ref EligblecatId, ref Description);

                            dt.Rows[tableRow]["CatId"] = catId;
                            dt.Rows[tableRow]["AcceptableCatId"] = EligblecatId;

                        }

                    }
                    if (rowNo < gvCommMgmt.MasterTableView.Items.Count)
                    {
                        if (dt.Rows.Count >= 1)
                        {
                            rowNo = rowNo + 1;
                            tableRow++;
                        }
                    }
                    else
                        break;
                }

                GridFooterItem ftItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                Label lbltotAmt = (Label)ftItemAmount.FindControl("lblAmount");

                if (isValid)
                {
                    Quantity = int.Parse(ViewState["Qty"].ToString());
                    sum = int.Parse(ViewState["Sum"].ToString());



                    if (Convert.ToDouble(lblAvailableLimits.Text) == 0)
                    {
                        trinsufficentmessage.Visible = true;
                        //lblinsufficent.Text = "Order cannot be processed due to insufficient balance";
                        //lblinsufficent.ForeColor = Color.Red;
                        //Color co = new Color();
                        msgRecordStatus.Visible = false;
                        //ShowMessage(lblinsufficent.Text);
                        ////  tdsubmit.Visible = false;
                        //  lnlBack.Visible = true;
                        //  Font testFont = new Font(fontName, 16.0f, FontStyle.Regular,
                        //GraphicsUnit.Pixel);
                    }
                    else if (ViewState["CustCat"] == null)
                    {

                        string category = (string)ViewState["CustCat"];
                        if (category == string.Empty)
                            ShowMessage("Please enter no of bonds within the range permissible.");


                    }
                    else if (Convert.ToDouble(lbltotAmt.Text) > Convert.ToDouble(lblAvailableLimits.Text))
                    {
                        ShowMessage("Order cannot be processed. Insufficient balance");
                        //  tdsubmit.Visible = false;
                        // lnlBack.Visible = true;

                    }
                    else if (FaceValue > sum)
                    {
                        ShowMessage("Application amount is less than face value.");
                        //  tdsubmit.Visible = false;
                        // lnlBack.Visible = true;

                    }
                    else if (Quantity < minQty)
                    {
                        foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
                        {
                            TextBox txtsumQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[CBOrder.ItemIndex]["Quantity"].FindControl("txtQuantity");
                            TextBox txtsumAmount = (TextBox)gvCommMgmt.MasterTableView.Items[CBOrder.ItemIndex]["Amount"].FindControl("txtAmount");
                            GridFooterItem footerItem = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                            Label lblQty = (Label)footerItem.FindControl("lblQuantity");
                            GridFooterItem footerItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                            Label lblSum = (Label)footerItemAmount.FindControl("lblAmount");
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Order cannot be processed.Please enter quantity greater than or equal to min quantity required')", true);
                            // ShowMessage(message);
                            txtsumQuantity.Text = "";
                            txtsumAmount.Text = "";
                            lblQty.Text = "";
                            lblSum.Text = "";
                        }
                    }
                    else if (Quantity > maxQty)
                    {
                        foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
                        {
                            TextBox txtsumQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[CBOrder.ItemIndex]["Quantity"].FindControl("txtQuantity");
                            TextBox txtsumAmount = (TextBox)gvCommMgmt.MasterTableView.Items[CBOrder.ItemIndex]["Amount"].FindControl("txtAmount");
                            GridFooterItem footerItem = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                            Label lblQty = (Label)footerItem.FindControl("lblQuantity");
                            GridFooterItem footerItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                            Label lblSum = (Label)footerItemAmount.FindControl("lblAmount");
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Order cannot be processed.Please enter quantity less than or equal to maximum quantity allowed for this issue')", true);
                            // ShowMessage(message);

                            txtsumQuantity.Text = "";
                            txtsumAmount.Text = "";
                            lblQty.Text = "";
                            lblSum.Text = "";
                        }
                    }
                    else
                    {
                        // placing order 
                        IDictionary<string, string> orderIds = new Dictionary<string, string>();
                        IssuerId = int.Parse(ViewState["IssueId"].ToString());
                        double availableBalance = Convert.ToDouble(OnlineBondBo.GetUserRMSAccountBalance(customerVo.AccountId));
                        int totalOrderAmt = int.Parse(ViewState["Sum"].ToString());
                        //availableBalance = 40000;
                        string message;
                        string aplicationNoStatus = string.Empty;
                        bool accountDebitStatus = false;
                        int Applicationno = 0;
                        int orderId = 0;
                        if (availableBalance >= totalOrderAmt)
                        {

                            orderIds = OnlineBondBo.onlineBOndtransact(dt, adviserVo.advisorId, IssuerId);
                            orderId = int.Parse(orderIds["Order_Id"].ToString());
                            if (orderId != 0)
                            {
                                accountDebitStatus = OnlineBondBo.DebitRMSUserAccountBalance(customerVo.AccountId, -totalOrderAmt, orderId);
                                ShowAvailableLimits();
                            }
                            Applicationno = int.Parse(orderIds["application"].ToString());
                            aplicationNoStatus = orderIds["aplicationNoStatus"].ToString();

                            ViewState["OrderId"] = orderId;
                            ViewState["application"] = Applicationno;

                            btnConfirmOrder.Enabled = false;
                            Label3.Visible = false;


                        }

                        tdsubmit.Visible = false;
                        message = CreateUserMessage(orderId, Applicationno, accountDebitStatus, aplicationNoStatus);
                        ShowMessage(message);
                        lnlBack.Visible = true;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Enter Quantity')", true);
                }
            }
        }
        protected void gvCommMgmt_ItemDataBound(object sender, GridItemEventArgs e)
        {

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
                trSubmit.Visible = true;
                foreach (GridDataItem gdi in gvCommMgmt.MasterTableView.Items)
                {
                    if (ronum < gvCommMgmt.MasterTableView.Items.Count)
                    {
                        TextBox txt = gvCommMgmt.Items[ronum].Cells[17].FindControl("txtQuantity") as TextBox;
                        //txt.Enabled = true;
                        ronum++;
                    }
                }
            }
            else
            {
                gvCommMgmt.DataSource = dsStructureRules.Tables[0];
                gvCommMgmt.DataBind();
                pnlNCDTransactact.Visible = true;
                trSubmit.Visible = false;
            }
        }

        protected void Editdetails(int IssuerId)
        {
            DataSet dsStructureRules = OnlineBondBo.GetNCDAllTransactOrder(orderId, IssuerId);
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
            dtIssueDetail = (DataTable)Cache["NCDTransactList" + userVo.UserId.ToString()];
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
        protected void lnlktoviewncdissue_Click(object sender, EventArgs e)
        {
            if (Session["PageDefaultSetting"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('NCDIssueList');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssueList", "loadcontrol('NCDIssueList');", true);
            }
        }
        protected void lblBack_click(object sender, EventArgs e)
        {
            if (Request.QueryString["strAction"] != null)
            {
                // int OrderId = int.Parse(Request.QueryString["OrderId"].ToString());
                string action = Request.QueryString["strAction"].ToString();
                DateTime todate = DateTime.Parse(Request.QueryString["todate"].ToString());
                DateTime fromdate = DateTime.Parse(Request.QueryString["fromdate"].ToString());
                string status = Request.QueryString["status"].ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssueBooks", "loadcontrol('NCDIssueBooks','&strAction=" + action + "&status=" + status + "&fromdate=" + fromdate + "&todate=" + todate + " ');", true);
            }
        }
    }

}


