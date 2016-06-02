using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCommon;
using System.Data.Common;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using BoOnlineOrderManagement;
using System.Configuration;
using VoUser;
using VoOnlineOrderManagemnet;

namespace WealthERP.OnlineOrderManagement
{
    public partial class MFOrderPurchaseTransactionType : System.Web.UI.UserControl
    {
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        OnlineMFOrderBo onlineMforderBo = new OnlineMFOrderBo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        CustomerVo customerVo = new CustomerVo();
        OnlineMFOrderVo onlinemforderVo = new OnlineMFOrderVo();
        UserVo userVo;
        string path;
        DataSet dsCustomerAssociates = new DataSet();
        DataTable dtCustomerAssociatesRaw = new DataTable();
        DataTable dtCustomerAssociates = new DataTable();
        DataRow drCustomerAssociates;
        int accountId;
        int OrderId;
        string clientMFAccessCode = string.Empty;
        int amcCode = 0;
        string category = string.Empty;
        string categoryname = string.Empty;
        string schemeName = string.Empty;
        string amcName = string.Empty;
        int scheme;
        string schemeDividendOption;
        string exchangeType = string.Empty;
        int debitstatus = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            OnlineUserSessionBo.CheckSession();
            customerVo = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session["userVo"];
            Session["OrderId"] = OrderId;
            RadInformation.VisibleOnPageLoad = false;
            int TOcpmaretime = int.Parse(DateTime.Now.ToShortTimeString().Split(':')[0]);
            if (TOcpmaretime >= int.Parse(ConfigurationSettings.AppSettings["START_TIME"]) && TOcpmaretime < int.Parse(ConfigurationSettings.AppSettings["END_TIME"]))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanel('MFOnlineSchemeManager')", true);
                return;
            }
            //if (Request.QueryString["exchangeType"] == null)
            //{
            //    exchangeType = "Online";

            //}
            //else
            //{
            //    exchangeType = Request.QueryString["exchangeType"].ToString();

            //}
            if (Session["ExchangeMode"] != null)
                exchangeType = Session["ExchangeMode"].ToString();
            else
                exchangeType = "Online";
            if (!IsPostBack)
            {
                BindKYCDetailDDl();

                clientMFAccessCode = onlineMforderBo.GetClientMFAccessStatus(customerVo.CustomerId);
                if (clientMFAccessCode == "FA")
                {
                    ShowAvailableLimits();

                    lblOption.Visible = false;
                    lblDividendType.Visible = false;
                    if ((Request.QueryString["accountId"] != null && Request.QueryString["SchemeCode"] != null) || Session["MFSchemePlan"] != null )
                    {
                        int accountId = 0;
                        int schemeCode = 0;
                        int amcCode = 0;
                        string category = string.Empty;
                        if (Request.QueryString["accountId"] != null)
                        {
                            schemeCode = int.Parse(Session["MFSchemePlan"].ToString());
                            accountId = int.Parse(Request.QueryString["accountId"].ToString());
                       
                            //commonLookupBo.GetSchemeAMCCategory(schemeCode, out amcCode, out category);exchangeType == "online" ? 1 : 0
                            commonLookupBo.GetSchemeAMCSchemeCategory(int.Parse(Session["MFSchemePlan"].ToString()), out amcCode, out category, out categoryname, out amcName, out schemeName);
                            lblAmc.Text = amcName;
                            lblCategory.Text = categoryname;
                            lblScheme.Text = schemeName;
                            BindFolioNumber(int.Parse(Session["MFSchemePlan"].ToString()));
                            ddlFolio.SelectedValue = accountId.ToString();
                            tdFolio.Visible = true;
                            DataSet ds = onlineMforderBo.GetCustomerSchemeFolioHoldings(customerVo.CustomerId, int.Parse(Session["MFSchemePlan"].ToString()), out schemeDividendOption, exchangeType == "Online" ? 1 : 0, accountId);
                            GetControlDetails(ds);
                            SetControlDetails();
                        }
                        else
                        {
                            if (exchangeType == "Online")
                                tdFolio.Visible = true;
                            else
                            {
                                DataSet ds;
                                ds = onlineMforderBo.GetControlDetails(int.Parse(Session["MFSchemePlan"].ToString()), null, exchangeType == "Online" ? 1 : 0);
                                lblUnitsheldDisplay.Visible = false;
                                GetControlDetails(ds);

                            }
                            scheme = int.Parse(Session["MFSchemePlan"].ToString());

                            //commonLookupBo.GetSchemeAMCCategory(38122, out amcCode, out category);
                            commonLookupBo.GetSchemeAMCSchemeCategory(int.Parse(Session["MFSchemePlan"].ToString()), out amcCode, out category, out categoryname, out amcName, out schemeName);
                            BindFolioNumber(int.Parse(Session["MFSchemePlan"].ToString()));
                            lblAmc.Text = amcName;
                            lblScheme.Text = schemeName;
                            lblCategory.Text = categoryname;
                            DataSet dst = onlineMforderBo.GetControlDetails(int.Parse(Session["MFSchemePlan"].ToString()), null, exchangeType == "Online" ? 1 : 0);
                            lblUnitsheldDisplay.Visible = false;
                            GetControlDetails(dst);
                            SetControlDetails();
                        }
                    }
                }
                else
                {
                    ShowMessage(onlineMforderBo.GetOnlineOrderUserMessage(clientMFAccessCode), 'I');
                    PurchaseOrderControlsEnable(false);
                    divControlContainer.Visible = false;
                }

            }



        }



        protected void ResetControlDetails(object sender, EventArgs e)
        {
            lblDividendType.Text = "";
            lblMintxt.Text = "";
            lblMulti.Text = "";
            lbltime.Text = "";
            lbldftext.Text = "";
            txtAmt.Text = "";
            lblNavDisplay.Text = "";
            ddlAmc.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlFolio.SelectedIndex = 0;

            ddlDivType.SelectedIndex = 0;


        }
        protected void ddlFolio_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            if (ddlFolio.SelectedValue != "New" && ddlFolio.SelectedValue != "0")
            {
                ds = onlineMforderBo.GetCustomerSchemeFolioHoldings(customerVo.CustomerId, int.Parse(Session["MFSchemePlan"].ToString()), out schemeDividendOption, exchangeType == "online" ? 1 : 0, int.Parse(ddlFolio.SelectedValue));

                GetControlDetails(ds);
                SetControlDetails();
            }
            else
            {

                ds = onlineMforderBo.GetControlDetails(int.Parse(Session["MFSchemePlan"].ToString()), null, exchangeType == "Online" ? 1 : 0);
                lblUnitsheldDisplay.Visible = false;
                GetControlDetails(ds);
                SetControlDetails();
            }
        }

        protected void GetControlDetails(DataSet ds)
        {

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > -1)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["PSLV_LookupValue"].ToString()))
                    {
                        lblDividendType.Text = dr["PSLV_LookupValue"].ToString();
                    }
                    if (ddlFolio.SelectedValue != "New" && ddlFolio.SelectedValue != "0" && ddlFolio.SelectedValue != "")
                    {
                        if (!string.IsNullOrEmpty(dr["AdditionalMinAmt"].ToString()))
                        {
                            lblMintxt.Text = dr["AdditionalMinAmt"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["AdditionalMultiAmt"].ToString()))
                        {
                            lblMulti.Text = dr["AdditionalMultiAmt"].ToString();
                        }

                        if (lblDividendType.Text == "Growth" & !string.IsNullOrEmpty(schemeDividendOption))
                        {
                            ddlDivType.SelectedValue = schemeDividendOption;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(dr["MinAmt"].ToString()))
                        {
                            lblMintxt.Text = dr["MinAmt"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["MultiAmt"].ToString()))
                        {
                            lblMulti.Text = dr["MultiAmt"].ToString();
                        }
                        DataSet dsNav = commonLookupBo.GetLatestNav(int.Parse(Session["MFSchemePlan"].ToString()));
                        if (dsNav.Tables[0].Rows.Count > 0)
                        {
                            string date = Convert.ToDateTime(dsNav.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy");
                            lblNavDisplay.Text = dsNav.Tables[0].Rows[0][1] + " " + "As On " + " " + date;
                        }
                    }
                    if (!string.IsNullOrEmpty(dr["CutOffTime"].ToString()))
                    {
                        lbltime.Text = dr["CutOffTime"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dr["divFrequency"].ToString()))
                    {
                        lbldftext.Text = dr["divFrequency"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["url"].ToString()))
                    {
                        lnkFactSheet.PostBackUrl = dr["url"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dr["AVSD_ExpiryDtae"].ToString()) && Convert.ToDateTime(dr["AVSD_ExpiryDtae"].ToString()) > DateTime.Now && Convert.ToInt16(dr["PMFRD_RatingOverall"].ToString()) > 0)
                    {
                        imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + dr["PMFRD_RatingOverall"].ToString() + ".png";

                        //Rating Overall
                        //imgRatingDetails.ImageUrl = @"../Images/MorningStarRating/RatingOverall/" + dr["PMFRD_RatingOverall"].ToString() + ".png";

                        ////Rating yearwise
                        //imgRating3yr.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + dr["PMFRD_Rating3Year"].ToString() + ".png";
                        //imgRating5yr.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + dr["PMFRD_Rating5Year"].ToString() + ".png";
                        //imgRating10yr.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + dr["PMFRD_Rating10Year"].ToString() + ".png";

                        //lblSchemeRetrun3yr.Text = dr["PMFRD_Return3Year"].ToString();
                        //lblSchemeRetrun5yr.Text = dr["PMFRD_Return5Year"].ToString();
                        //lblSchemeRetrun10yr.Text = dr["PMFRD_Return10Year"].ToString();

                        //lblSchemeRisk3yr.Text = dr["PMFRD_Risk3Year"].ToString();
                        //lblSchemeRisk5yr.Text = dr["PMFRD_Risk5Year"].ToString();
                        //lblSchemeRisk10yr.Text = dr["PMFRD_Risk10Year"].ToString();

                        if (!string.IsNullOrEmpty(dr["PMFRD_RatingDate"].ToString()))
                        {
                            lblSchemeRatingAsOn.Text = "As On " + Convert.ToDateTime(dr["PMFRD_RatingDate"].ToString()).ToShortDateString();
                            //lblRatingAsOnPopUp.Text = lblSchemeRatingAsOn.Text;
                            lblSchemeRatingAsOn.Visible = true;
                        }
                    }
                    else
                    {
                        //trSchemeRating.Visible = false;
                        lblSchemeRatingAsOn.Visible = false;
                        imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/0.png";
                        lblSchemeRatingAsOn.Visible = false;
                    }

                }

            }
            if (ds.Tables[4].Rows.Count > 0)
            {
                lblDemate.Text = ds.Tables[4].Rows[0][0].ToString();
                onlinemforderVo.BSESchemeCode = ds.Tables[4].Rows[0][0].ToString();
            }
            if (ddlFolio.SelectedValue != "New" && ddlFolio.SelectedValue != "0" && ddlFolio.SelectedValue != "")
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable dtUnit = ds.Tables[1];
                    foreach (DataRow drunits in dtUnit.Rows)
                    {
                        if (!string.IsNullOrEmpty(drunits["CMFNP_NetHoldings"].ToString()))
                        {
                            lblUnitsheldDisplay.Text = drunits["CMFNP_NetHoldings"].ToString();
                            lblUnitsheldDisplay.Visible = true;
                        }
                    }
                }

                //NAV SET----3

                if (ds.Tables[2].Rows.Count > 0)
                {
                    string date = Convert.ToDateTime(ds.Tables[2].Rows[0][0]).ToString("dd-MMM-yyyy");
                    lblNavDisplay.Text = ds.Tables[2].Rows[0][1] + " " + "As On " + " " + date;
                }
            }
        }
        private void BindFolioNumber(int schemecode)
        {
            DataTable dt;
            try
            {
                dt = onlineMforderBo.GetCustomerFolioSchemeWise(customerVo.CustomerId, schemecode);
                if (dt.Rows.Count > 0)
                {
                    ddlFolio.DataSource = dt;
                    ddlFolio.DataValueField = dt.Columns["CMFA_AccountId"].ToString();
                    ddlFolio.DataTextField = dt.Columns["CMFA_FolioNum"].ToString();
                    ddlFolio.DataBind();
                }
                //ddlFolio.Items.Insert(0, new ListItem("Select", "0"));
                ddlFolio.Items.Insert(0, new ListItem("New", "New"));
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }
        protected void SetControlDetails()
        {
            lbltime.Visible = true;
            lblMulti.Visible = true;
            lblMintxt.Visible = true;

            if (lblDividendType.Text == "Growth")
            {
                lblDividendFrequency.Visible = false;
                lbldftext.Visible = false;
                RequiredFieldValidator4.Enabled = false;
                divDVR.Visible = false;
            }
            else
            {
                //if (ddlScheme.SelectedIndex == 0) return;
                BindSchemeDividendTypes(int.Parse(Session["MFSchemePlan"].ToString()));
                divDVR.Visible = true;
                RequiredFieldValidator4.Enabled = true;

            }


        }
        protected void PurchaseOrderControlsEnable(bool enable)
        {
            if (!enable)
            {

                ddlFolio.Enabled = false;
                txtAmt.Enabled = false;
                ddlDivType.Enabled = false;
                lnkFactSheet.Enabled = false;
                //trTermsCondition.Visible = false;

                btnSubmit.Visible = false;
                //trNewOrder.Visible = true;

            }
            else
            {
                btnSubmit.Enabled = true;

                ddlFolio.Enabled = true;
                txtAmt.Enabled = true;
                ddlDivType.Enabled = true;
                lnkFactSheet.Enabled = true;
                btnSubmit.Enabled = true;

                btnSubmit.Visible = true;
                //trNewOrder.Visible = false;
            }

        }
        protected void LoadOrderDetails()
        {

            int ID = int.Parse(Session["OrderId"].ToString());
            onlinemforderVo = onlineMforderBo.GetOrderDetails(ID);


        }
        protected void lnkFactSheet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lnkFactSheet.PostBackUrl))
                Response.Write(@"<script language='javascript'>alert('The URL is not valid');</script>");
        }



        protected void OnClick_Submit(object sender, EventArgs e)
        {
            confirmMessage.Text = onlineMforderBo.GetOnlineOrderUserMessage("EUIN");
            string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);

        }

        protected void rbConfirm_OK_Click(object sender, EventArgs e)
        {
            CreatePurchaseOrderType();
        }

        private void CreatePurchaseOrderType()
        {
            List<int> OrderIds = new List<int>();
            bool accountDebitStatus = false;
            onlinemforderVo.SchemePlanCode = int.Parse(Session["MFSchemePlan"].ToString());
            if (!string.IsNullOrEmpty(txtAmt.Text.ToString()))
            {
                onlinemforderVo.Amount = double.Parse(txtAmt.Text.ToString());
            }
            else
                onlinemforderVo.Amount = 0.0;
            onlinemforderVo.DividendType = ddlDivType.SelectedValue;
            if (ddlFolio.SelectedValue != "New" && ddlFolio.SelectedValue != "0")
            {
                onlinemforderVo.TransactionType = "ABY";
                onlinemforderVo.FolioNumber = ddlFolio.SelectedValue;

            }
            else
            {
                onlinemforderVo.TransactionType = "BUY";
            }
            float amt;
            float minAmt;
            float multiAmt;
            DateTime Dt;

            if (string.IsNullOrEmpty(txtAmt.Text))
            {
                amt = 0;

            }
            else
            {
                amt = float.Parse(txtAmt.Text);
            }
            if (string.IsNullOrEmpty(lblMintxt.Text) && string.IsNullOrEmpty(lblMulti.Text) && string.IsNullOrEmpty(lbltime.Text))
            {
                minAmt = 0; multiAmt = 0; Dt = DateTime.MinValue;
            }
            else
            {

                minAmt = float.Parse(lblMintxt.Text);
                multiAmt = float.Parse(lblMulti.Text);
                Dt = DateTime.Parse(lbltime.Text);
            }
            int retVal = commonLookupBo.IsRuleCorrect(amt, minAmt, amt, multiAmt, Dt);
            if (retVal != 0)
            {
                if (retVal == -2)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You have entered amount less than Minimum Initial amount allowed');", true); return;
                }
                if (retVal == -1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You should enter the amount in multiples of Subsequent amount ');", true); return;
                }

            }
            decimal availableBalance = onlineMforderBo.GetUserRMSAccountBalance(customerVo.AccountId);
            //string message = string.Empty;

            //if (availableBalance >= Convert.ToDecimal(onlinemforderVo.Amount))
            //{
            //    OrderIds = onlineMforderBo.CreateCustomerOnlineMFOrderDetails(onlinemforderVo, userVo.UserId, customerVo.CustomerId, exchangeType);
            //    OrderId = int.Parse(OrderIds[0].ToString());

            //    if (OrderId != 0 && !string.IsNullOrEmpty(customerVo.AccountId))
            //    {
            //        accountDebitStatus = onlineMforderBo.DebitRMSUserAccountBalance(customerVo.AccountId, -onlinemforderVo.Amount, OrderId);
            //        ShowAvailableLimits();
            //    }

            //}
            onlinemforderVo.BSESchemeCode = lblDemate.Text;
            string message = string.Empty;
            char msgType = 'F';
            if (exchangeType == "Online")
            {
                onlinemforderVo.OrderType = 1;
                if (availableBalance >= Convert.ToDecimal(onlinemforderVo.Amount))
                {
                    OrderIds = onlineMforderBo.CreateCustomerOnlineMFOrderDetails(onlinemforderVo, userVo.UserId, customerVo.CustomerId);
                    OrderId = int.Parse(OrderIds[0].ToString());

                    if (OrderId != 0 && !string.IsNullOrEmpty(customerVo.AccountId))
                    {
                        accountDebitStatus = onlineMforderBo.DebitRMSUserAccountBalance(customerVo.AccountId, -onlinemforderVo.Amount, OrderId, out debitstatus);
                        ShowAvailableLimits();
                    }

                }

                message = CreateUserMessage(OrderId, accountDebitStatus, retVal == 1 ? true : false, out msgType);
            }
            else if (exchangeType == "Demat")
            {
                onlinemforderVo.OrderType = 0;
                if (availableBalance >= Convert.ToDecimal(onlinemforderVo.Amount))
                {
                    OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
                    message = OnlineMFOrderBo.BSEorderEntryParam(userVo.UserId, customerVo.CustCode, onlinemforderVo, customerVo.CustomerId, out msgType);
                }
                else
                {
                    message = "Order cannot be processed. Insufficient balance";
                }
            }
            PurchaseOrderControlsEnable(false);
            ShowMessage(message, msgType);

        }


        private string CreateUserMessage(int orderId, bool accountDebitStatus, bool isCutOffTimeOver, out char msgType)
        {
            string userMessage = string.Empty;
            msgType = 'S';
            if (orderId != 0 && accountDebitStatus == true)
            {
                if (isCutOffTimeOver)
                    userMessage = "    Order placed successfully, Order reference no is " + orderId.ToString() + ", Order will process next business day";
                else
                    userMessage = "    Order placed successfully, Order reference no is " + orderId.ToString();
            }
            else if (orderId != 0 && accountDebitStatus == false)
            {
                userMessage = "   Order placed successfully,Order will not process due to insufficient balance, Order reference no is " + orderId.ToString();
                msgType = 'F';
            }
            else if (orderId == 0)
            {
                userMessage = "  Order cannot be processed. Insufficient balance";
                msgType = 'F';
            }

            return userMessage;

        }

        private void ShowMessage(string msg, char type)
        {
            //--S(success)
            //--F(failure)
            //--W(warning)
            //--I(information)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wsedrftgyhjukloghjnnnghj", " showMsg('" + msg + "','" + type.ToString() + "');", true);
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        { }
        protected void lnkBack_Click(object sender, EventArgs e)
        { }

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

        private void ShowAvailableLimits()
        {
            if (!string.IsNullOrEmpty(customerVo.AccountId))
            {
                //lblAvailableLimits.Text = onlineMforderBo.GetUserRMSAccountBalance(customerVo.AccountId).ToString();
            }

        }

        protected void lnkNewOrder_Click(object sender, EventArgs e)
        {
            if (Session["PageDefaultSetting"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderPurchaseTransType')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "loadcontrol('MFOrderPurchaseTransType')", true);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('IPOIssueTransact','&issueId=" + issueId + "')", true);
            }
        }

        protected void BindSchemeDividendTypes(int schemeId)
        {
            DataTable dtSchemeDividendOption = commonLookupBo.GetMFSchemeDividentType(schemeId);
            ddlDivType.Items.Clear();
            if (dtSchemeDividendOption.Rows.Count > 0)
            {
                ddlDivType.DataSource = dtSchemeDividendOption;
                ddlDivType.DataValueField = dtSchemeDividendOption.Columns["PSLV_LookupValueCode"].ToString();
                ddlDivType.DataTextField = dtSchemeDividendOption.Columns["PSLV_LookupValue"].ToString();
                ddlDivType.DataBind();
                ddlDivType.Items.Insert(0, new ListItem("--SELECT--", "0"));

            }

        }
        protected void imgInformation_OnClick(object sender, EventArgs e)
        {
            RadInformation.VisibleOnPageLoad = true;

        }
        protected void BindKYCDetailDDl()
        {
            OnlineBondOrderBo OnlineBondBo = new OnlineBondOrderBo();
            DataSet dsNomineeAndJointHolders = OnlineBondBo.GetNomineeJointHolder(customerVo.CustomerId);
            StringBuilder strbNominee = new StringBuilder();
            StringBuilder strbJointHolder = new StringBuilder();

            foreach (DataRow dr in dsNomineeAndJointHolders.Tables[0].Rows)
            {
                //strbJointHolder.Append(dr["CustomerName"].ToString() + ",");
                string r = dr["CEDAA_AssociationType"].ToString();
                if (r != "Joint Holder")
                    strbNominee.Append(dr["AMFE_JointNomineeName"].ToString() + ",");
                else
                    strbJointHolder.Append(dr["AMFE_JointNomineeName"].ToString() + ",");
                //strbJointHolder.Append(dr["AMFE_JointNomineeName"].ToString() + ",");
                //strbNominee.Append(dr["AMFE_JointNomineeName"].ToString() + ",");
            }
            lblNomineeDisplay.Text = strbNominee.ToString().TrimEnd(',');
            lblHolderDisplay.Text = strbJointHolder.ToString().TrimEnd(',');

        }
    }
}
