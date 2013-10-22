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
using DaoReports;


namespace WealthERP.OnlineOrderManagement
{
    public partial class MFOrderAdditionalPurchase : System.Web.UI.UserControl
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
        DataSet ds;

        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session["userVo"];
            if (!IsPostBack)
            {
                AmcBind();
                CategoryBind();
               
                if (Request.QueryString["accountId"] != null && Request.QueryString["SchemeCode"] != null)
                {
                    int accountId = 0;
                    int schemeCode = 0;
                    int amcCode = 0;
                    string category = string.Empty;
                    accountId = int.Parse(Request.QueryString["accountId"].ToString());
                    schemeCode = int.Parse(Request.QueryString["SchemeCode"].ToString());
                    commonLookupBo.GetSchemeAMCCategory(schemeCode, out amcCode, out category);
                    SetSelectedDisplay(accountId, schemeCode, amcCode, category);
                }
                
                lblOption.Visible = false;
                lblDividendType.Visible = false;
            }


        }
        protected void BindAmcForDrillDown()
        {
            DataTable dtAmc = new DataTable();
            dtAmc = commonLookupBo.GetProdAmc();
            if (dtAmc.Rows.Count > 0)
            {
                ddlAmc.DataSource = dtAmc;
                ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAmc.DataBind();
                ddlAmc.Items.Insert(0, new ListItem("Select", "0"));
            }
        
        }
        protected void SetSelectedDisplay(int Accountid, int SchemeCode, int Amccode, string Category)
        {
            BindAmcForDrillDown();
            ddlAmc.SelectedValue= Amccode.ToString();
            ddlCategory.SelectedValue = Category;
           SchemeBind(Amccode, Category,0);
            BindFolioNumber(Amccode);
            ddlFolio.SelectedValue = Accountid.ToString();
            ddlScheme.SelectedValue = SchemeCode.ToString();

            ddlAmc.Enabled = false;
            ddlCategory.Enabled = false;
            ddlFolio.Enabled = false;
            ddlScheme.Enabled = false;
            GetControlDetails(SchemeCode, Accountid.ToString());


        }
        protected void BindNomineeAndJointHolders()
        {
            MFReportsDao MFReportsDao = new MFReportsDao();
            DataSet dsNomineeAndJointHolders;
            dsNomineeAndJointHolders = MFReportsDao.GetARNNoAndJointHoldings(customerVo.CustomerId, 0, ddlFolio.SelectedItem.ToString());
            StringBuilder strbNominee = new StringBuilder();
            StringBuilder strbJointHolder = new StringBuilder();

            foreach (DataRow dr in dsNomineeAndJointHolders.Tables[1].Rows)
            {
                strbJointHolder.Append(dr["JointHolderName"].ToString() + ",");
                strbNominee.Append(dr["JointHolderName"].ToString() + ",");
            }

            lblNomineeDisplay.Text = strbNominee.ToString();
            lblHolderDisplay.Text = strbJointHolder.ToString();
        }
        protected void AmcBind()
        {
            ddlAmc.Items.Clear();
            DataSet ds = new DataSet();
            DataTable dtAmc = new DataTable();
            ds = onlineMforderBo.GetRedeemAmcDetails(customerVo.CustomerId);
            dtAmc = ds.Tables[0];
            if (dtAmc.Rows.Count > 0)
            {
                ddlAmc.DataSource = dtAmc;
                ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAmc.DataBind();
                ddlAmc.Items.Insert(0, new ListItem("Select", "0"));

                BindFolioNumber(int.Parse(ddlAmc.SelectedValue));
            }
            else
            {
                PurchaseOrderControlsEnable(false);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('No existing Investment found');", true); return;
            }
        }

        public void ddlAmc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CategoryBind();
            SchemeBind(int.Parse(ddlAmc.SelectedValue), null, customerVo.CustomerId);
            BindFolioNumber(int.Parse(ddlAmc.SelectedValue));
        }

        public void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAmc.SelectedIndex != -1 && ddlCategory.SelectedIndex != -1)
            {
                int amcCode = int.Parse(ddlAmc.SelectedValue);
                string category = ddlCategory.SelectedValue.ToString();
                SchemeBind(amcCode, category, customerVo.CustomerId);
            }

        }

        protected void ddlScheme_onSelectedChanged(object sender, EventArgs e)
        {
            if (ddlScheme.SelectedIndex != -1)
            {
                // ResetControlDetails(sender,e);
                GetControlDetails(int.Parse(ddlScheme.SelectedValue), ddlFolio.SelectedValue.ToString());
                SetControlDetails();
            }
        }


        protected void GetControlDetails(int scheme, string folio)
        {
            DataSet ds = new DataSet();

            ds = onlineMforderBo.GetControlDetails(scheme, folio);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > -1)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["PSLV_LookupValue"].ToString()))
                    {
                        lblDividendType.Text = dr["PSLV_LookupValue"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["AdditionalMinAmt"].ToString()))
                    {
                        lblMintxt.Text = dr["AdditionalMinAmt"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["AdditionalMultiAmt"].ToString()))
                    {
                        lblMulti.Text = dr["AdditionalMultiAmt"].ToString();
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
                }
            }
            DataSet dsNav = new DataSet();

            dsNav = commonLookupBo.GetLatestNav(scheme);
            
               
                if (dsNav.Tables[0].Rows.Count > 0)
                {
                    string date = Convert.ToDateTime(dsNav.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy");
                    lblNavDisplay.Text = dsNav.Tables[0].Rows[0][1] + " " + "As On " + " " + date;
                }
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dtUnit = ds.Tables[1];
                foreach (DataRow drunits in dtUnit.Rows)
                {
                    if (!string.IsNullOrEmpty(drunits["CMFNP_NetHoldings"].ToString()))
                    {
                        lblUnitsheldDisplay.Text = drunits["CMFNP_NetHoldings"].ToString();
                    }
                }
            }



        }
        protected void SetControlDetails()
        {
            lbltime.Visible = true;
            //lblDividendType.Visible = true;
            lblMulti.Visible = true;
            lblMintxt.Visible = true;
            lblDivType.Visible = true;
            lblUnitsheldDisplay.Visible = true;
            if (lblDividendType.Text == "Growth")
            {
                lblDividendFrequency.Visible = false;
                lbldftext.Visible = false;
                lblDivType.Visible = false;
                ddlDivType.Visible = false;
                RequiredFieldValidator4.Enabled = false;

            }
            else
            {
                //lblDividendFrequency.Visible = true;
                //lbldftext.Visible = true;
                lblDivType.Visible = true;
                ddlDivType.Visible = true;
                RequiredFieldValidator4.Enabled = true;
            }

        }
        protected void CategoryBind()
        {
            ddlCategory.Items.Clear();
            DataSet dsCategory = new DataSet();
            dsCategory = commonLookupBo.GetAllCategoryList();

            if (dsCategory.Tables[0].Rows.Count > 0)
            {
                ddlCategory.DataSource = dsCategory.Tables[0];
                ddlCategory.DataValueField = dsCategory.Tables[0].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                ddlCategory.DataTextField = dsCategory.Tables[0].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("All", "0"));
            }
        }

        protected void SchemeBind(int amccode, string category, int customerid)
        {
            ddlScheme.Items.Clear();
            DataTable dtScheme = new DataTable();
            dtScheme = commonLookupBo.GetAmcSchemeList(amccode, category, customerid);
            if (dtScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtScheme;
                ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("Select", "0"));
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

        protected void PurchaseOrderControlsEnable(bool enable)
        {
            if (!enable)
            {
                ddlAmc.Enabled = false;
                ddlCategory.Enabled = false;
                ddlScheme.Enabled = false;
                ddlFolio.Enabled = false;
                txtAmt.Enabled = false;
                ddlDivType.Enabled = false;
                lnkFactSheet.Enabled = false;
                btnSubmit.Enabled = false;
            }
            else
            {
                ddlAmc.Enabled = true;
                ddlCategory.Enabled = true;
                ddlScheme.Enabled = true;
                ddlFolio.Enabled = true;
                txtAmt.Enabled = true;
                ddlDivType.Enabled = true;
                lnkFactSheet.Enabled = true;
                btnSubmit.Enabled = true;

            }

        }
        protected void OnClick_Submit(object sender, EventArgs e)
        {
            List<int> OrderIds = new List<int>();
            bool accountDebitStatus = false;
            onlinemforderVo.SchemePlanCode = Int32.Parse(ddlScheme.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtAmt.Text.ToString()))
            {
                onlinemforderVo.Amount = double.Parse(txtAmt.Text.ToString());
            }
            else
                onlinemforderVo.Amount = 0.0;
            onlinemforderVo.FolioNumber = ddlFolio.SelectedValue;
            onlinemforderVo.DividendType = ddlDivType.SelectedValue;
            onlinemforderVo.TransactionType = "ABY";
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

                if (retVal == -1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You should enter the amount in multiples of Subsequent amount');", true); return;
                }
                if (retVal == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('The CutOff time has been Reached ');", true); return;
                }
                if (retVal == -2)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You have entered amount less than Minimum Initial amount allowed');", true); return;
                }
            }

            OrderIds = onlineMforderBo.CreateCustomerOnlineMFOrderDetails(onlinemforderVo, userVo.UserId, customerVo.CustomerId);
            OrderId = int.Parse(OrderIds[0].ToString());
            if (OrderId != 0 && !string.IsNullOrEmpty(customerVo.AccountId))
            {
                accountDebitStatus = onlineMforderBo.DebitRMSUserAccountBalance(customerVo.AccountId, -onlinemforderVo.Amount, OrderId);
            }
            if ((OrderId != 0 && accountDebitStatus == true) || (OrderId != 0 && string.IsNullOrEmpty(customerVo.AccountId)))
            {
                string message = "Order placed successfully, Order reference no is " + OrderId.ToString();
                ShowMessage(message);
            }
            else if (OrderId != 0 && accountDebitStatus == false)
            {
                string message = "Order placed successfully,Order will not process due to insufficient balance, Order reference no is " + OrderId.ToString();
                ShowMessage(message);
            }

            PurchaseOrderControlsEnable(false);

        }
        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
        }
        private void BindFolioNumber(int amcCode)
        {
            DataTable dtScheme = new DataTable();
            DataTable dtgetfolioNo;
            try
            {
                dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(Convert.ToInt32(ddlAmc.SelectedValue), customerVo.CustomerId);

                if (dtgetfolioNo.Rows.Count > 0)
                {
                    ddlFolio.DataSource = dtgetfolioNo;
                    ddlFolio.DataTextField = dtgetfolioNo.Columns["CMFA_FolioNum"].ToString();
                    ddlFolio.DataValueField = dtgetfolioNo.Columns["CMFA_AccountId"].ToString();
                    ddlFolio.DataBind();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }


    }
}
