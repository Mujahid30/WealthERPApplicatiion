using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoCustomerProfiling;
using BoProductMaster;
using WealthERP.Base;
using VoUser;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioSystematicEntry : System.Web.UI.UserControl
    {
        SystematicSetupVo systematicSetupVo;
        SystematicSetupBo systematicSetupBo = new SystematicSetupBo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        ProductMFBo productMFBo = new ProductMFBo();
        AssetBo assetBo = new AssetBo();

        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        DataSet dsCustomerMFAccounts;
        DataTable dtSystematicTransactionType;
        DataTable dtFrequency;
        TimeSpan tsPeriod;


        int portfolioId;
        int schemePlanCode;
        int systematicSetupId;
        string path;
        string AssetGroupCode = "MF";
        string Manage = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            // Check Querystring to see if its an Edit/View/Entry
            if (Request.QueryString["action"] != null)
                Manage = Request.QueryString["action"].ToString();

            if (!IsPostBack)
            {
                userVo = (UserVo)Session[SessionContents.UserVo];
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                //customerAccountsVo = (CustomerAccountsVo)Session["customerAccountVo"];
                if (Session["systematicSetupVo"] != null)
                {
                    systematicSetupVo = (SystematicSetupVo)Session["systematicSetupVo"];
                }

                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());


                if (systematicSetupVo != null)
                {
                    if (Manage == "edit")
                    {
                        SetControls("edit", systematicSetupVo, path);
                    }
                    else if (Manage == "view")
                    {
                        SetControls("view", systematicSetupVo, path);
                    }
                    else if (Manage == "entry")
                    {
                        SetControls("entry", systematicSetupVo, path);
                    }
                }
                else
                {
                    SetControls("entry", systematicSetupVo, path);
                }
            }
        }

        /// <summary>
        /// Binds all the Dropdowns with the necessary data
        /// </summary>
        private void BindDropDowns(string path)
        {
            userVo = (UserVo)Session["userVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());

            //Bind the Systematic Transaction Types to dropdown
            dtSystematicTransactionType = XMLBo.GetSystematicTransactionType(path);
            ddlSystematicType.DataSource = dtSystematicTransactionType;
            ddlSystematicType.DataTextField = "SystematicType";
            ddlSystematicType.DataValueField = "SystemationTypeCode";
            ddlSystematicType.DataBind();
            ddlSystematicType.Items.Insert(0, "Select a Transaction Type");

            // Bind the Frequency to dropdown
            dtFrequency = assetBo.GetFrequencyCode(path);
            ddlFrequency.DataSource = dtFrequency;
            ddlFrequency.DataTextField = "Frequency";
            ddlFrequency.DataValueField = "FrequencyCode";
            ddlFrequency.DataBind();
            ddlFrequency.Items.Insert(0, "Select Frequency");

            //Bind the Customer Account Details to dropdown
            if(schemePlanCode!=0)
                dsCustomerMFAccounts = customerAccountBo.GetCustomerMFAccounts(portfolioId, AssetGroupCode, schemePlanCode);
            else
                dsCustomerMFAccounts = customerAccountBo.GetCustomerMFAccounts(portfolioId, AssetGroupCode, 0);

            ddlMFAccount.DataSource = dsCustomerMFAccounts.Tables[0];
            ddlMFAccount.DataTextField = "CMFA_FolioNum";
            ddlMFAccount.DataValueField = "CMFA_AccountId";
            ddlMFAccount.DataBind();
            ddlMFAccount.Items.Insert(0, "Select a Folio");

        }

        /// <summary>
        /// Sets the Controls with the approprite values based on the action to be performed (View, Edit or Entry)
        /// </summary>
        /// <param name="action">action to be performed</param>
        /// <param name="systematicSetupVo">Object that holds the data to be filled in the controls</param>
        /// <param name="customerAccountsVo"></param>
        /// <param name="path">path of XML</param>
        private void SetControls(string action, SystematicSetupVo systematicSetupVo, string path)
        {
            BindDropDowns(path);

            EnableDisableControls(action);

            if (action == "entry")
            {
                if (systematicSetupVo == null)
                {
                    txtStartDate_CalendarExtender.Enabled = true;
                    txtStartDate_TextBoxWatermarkExtender.Enabled = true;
                    ddlSystematicType.SelectedIndex = -1;
                    ddlMFAccount.SelectedIndex = -1;
                    txtStartDate.Text = "";
                    txtSystematicDate.Text = "";
                    ddlFrequency.SelectedIndex = -1;
                    txtAmount.Text = "";
                    txtPeriod.Text = "";
                }
                else
                {
                    txtStartDate_CalendarExtender.Enabled = true;
                    txtStartDate_TextBoxWatermarkExtender.Enabled = true;
                    ddlSystematicType.SelectedValue = systematicSetupVo.SystematicTypeCode.Trim();
                    ddlMFAccount.SelectedValue = systematicSetupVo.AccountId.ToString();
                    txtSearchScheme.Text = systematicSetupVo.SchemePlan.ToString();
                    txtSchemeCode.Value = systematicSetupVo.SchemePlanCode.ToString();
                    if (systematicSetupVo.SchemePlan != "")
                        txtSwitchSchemeCode_AutoCompleteExtender.ContextKey = txtSchemeCode.Value;
                    txtStartDate.Text = "";
                    txtSystematicDate.Text = "";
                    ddlFrequency.SelectedValue = "";
                    txtAmount.Text = systematicSetupVo.Amount.ToString();
                    //tsPeriod=systematicSetupVo.EndDate.Subtract(systematicSetupVo.StartDate);
                    txtPeriod.Text = "";
                    
                }
            }
            else
            {
                ddlSystematicType.SelectedValue = systematicSetupVo.SystematicTypeCode.Trim();
                if (systematicSetupVo.SystematicTypeCode.Trim() == "STP")
                {
                    trSwitchScheme.Visible = true;
                    if (systematicSetupVo.SchemePlanSwitch != "")
                    {
                        txtSwicthSchemeSearch.Text = systematicSetupVo.SchemePlanSwitch;
                        txtSwitchSchemeCode.Value = systematicSetupVo.SchemePlanCodeSwitch.ToString();
                    }
                }
                ddlMFAccount.SelectedValue = systematicSetupVo.AccountId.ToString();
                txtSearchScheme.Text = systematicSetupVo.SchemePlan.ToString();
                txtSchemeCode.Value = systematicSetupVo.SchemePlanCode.ToString();
                txtStartDate.Text = systematicSetupVo.StartDate.ToShortDateString();
                txtSystematicDate.Text = systematicSetupVo.SystematicDate.ToString();
                ddlFrequency.SelectedValue = systematicSetupVo.FrequencyCode.Trim();
                txtAmount.Text = systematicSetupVo.Amount.ToString();
                //tsPeriod=systematicSetupVo.EndDate.Subtract(systematicSetupVo.StartDate);
                txtPeriod.Text = (-12 * (systematicSetupVo.StartDate.Year - systematicSetupVo.EndDate.Year) + systematicSetupVo.StartDate.Month - systematicSetupVo.EndDate.Month).ToString();
                systematicSetupId = systematicSetupVo.SystematicSetupId;
            }

        }

        /// <summary>
        /// Function to Enable or Disable the controls based on action to be performed (View, Edit or Entry)
        /// </summary>
        /// <param name="action">Acoint to be performed</param>
        private void EnableDisableControls(string action)
        {
            if (action == "view")
            {
                ddlSystematicType.Enabled = false;
                ddlMFAccount.Enabled = false;
                txtSearchScheme.Enabled = false;
                txtStartDate.Enabled = false;
                //chkDate1.Enabled = false;
                //chkDate2.Enabled = false;
                //chkDate3.Enabled = false;
                //chkDate4.Enabled = false;
                //chkDate5.Enabled = false;
                //chkDate6.Enabled = false;
                //chkDate7.Enabled = false;
                //chkDate8.Enabled = false;
                //chkDate9.Enabled = false;
                //chkDate10.Enabled = false;
                //chkDate11.Enabled = false;
                //chkDate12.Enabled = false;
                //chkDate13.Enabled = false;
                //chkDate14.Enabled = false;
                //chkDate15.Enabled = false;
                //chkDate16.Enabled = false;
                //chkDate17.Enabled = false;
                //chkDate18.Enabled = false;
                //chkDate19.Enabled = false;
                //chkDate20.Enabled = false;
                //chkDate21.Enabled = false;
                //chkDate22.Enabled = false;
                //chkDate23.Enabled = false;
                //chkDate24.Enabled = false;
                //chkDate25.Enabled = false;
                //chkDate26.Enabled = false;
                //chkDate27.Enabled = false;
                //chkDate28.Enabled = false;
                //chkDate29.Enabled = false;
                //chkDate30.Enabled = false;
                //chkDate31.Enabled = false;
                trSystematicDateChk1.Visible = false;
                trSystematicDateChk2.Visible = false;
                trSystematicDateChk3.Visible = false;
                ddlFrequency.Enabled = false;
                txtAmount.Enabled = false;
                txtPeriod.Enabled = false;
                txtSystematicDate.Enabled = false;
                trSystematicDate.Visible = true;

                btnSubmit.Visible = false;

            }
            else if (action == "entry")
            {
                ddlSystematicType.Enabled = true;
                ddlMFAccount.Enabled = true;
                txtSearchScheme.Enabled = true;
                txtStartDate.Enabled = true;
                chkDate1.Enabled = true;
                chkDate2.Enabled = true;
                chkDate3.Enabled = true;
                chkDate4.Enabled = true;
                chkDate5.Enabled = true;
                chkDate6.Enabled = true;
                chkDate7.Enabled = true;
                chkDate8.Enabled = true;
                chkDate9.Enabled = true;
                chkDate10.Enabled = true;
                chkDate11.Enabled = true;
                chkDate12.Enabled = true;
                chkDate13.Enabled = true;
                chkDate14.Enabled = true;
                chkDate15.Enabled = true;
                chkDate16.Enabled = true;
                chkDate17.Enabled = true;
                chkDate18.Enabled = true;
                chkDate19.Enabled = true;
                chkDate20.Enabled = true;
                chkDate21.Enabled = true;
                chkDate22.Enabled = true;
                chkDate23.Enabled = true;
                chkDate24.Enabled = true;
                chkDate25.Enabled = true;
                chkDate26.Enabled = true;
                chkDate27.Enabled = true;
                chkDate28.Enabled = true;
                chkDate29.Enabled = true;
                chkDate30.Enabled = true;
                chkDate31.Enabled = true;
                ddlFrequency.Enabled = true;
                txtAmount.Enabled = true;
                txtPeriod.Enabled = true;
                trSystematicDate.Visible = false;
                trSystematicDateChk1.Visible = true;
                trSystematicDateChk2.Visible = true;
                trSystematicDateChk3.Visible = true;
                btnSubmit.Text = "Submit";
                btnSubmit.Visible = true;
            }

            else if (action == "edit")
            {
                ddlSystematicType.Enabled = true;
                ddlMFAccount.Enabled = true;
                txtSearchScheme.Enabled = true;
                txtStartDate.Enabled = true;
                //chkDate1.Enabled = true;
                //chkDate2.Enabled = true;
                //chkDate3.Enabled = true;
                //chkDate4.Enabled = true;
                //chkDate5.Enabled = true;
                //chkDate6.Enabled = true;
                //chkDate7.Enabled = true;
                //chkDate8.Enabled = true;
                //chkDate9.Enabled = true;
                //chkDate10.Enabled = true;
                //chkDate11.Enabled = true;
                //chkDate12.Enabled = true;
                //chkDate13.Enabled = true;
                //chkDate14.Enabled = true;
                //chkDate15.Enabled = true;
                //chkDate16.Enabled = true;
                //chkDate17.Enabled = true;
                //chkDate18.Enabled = true;
                //chkDate19.Enabled = true;
                //chkDate20.Enabled = true;
                //chkDate21.Enabled = true;
                //chkDate22.Enabled = true;
                //chkDate23.Enabled = true;
                //chkDate24.Enabled = true;
                //chkDate25.Enabled = true;
                //chkDate26.Enabled = true;
                //chkDate27.Enabled = true;
                //chkDate28.Enabled = true;
                //chkDate29.Enabled = true;
                //chkDate30.Enabled = true;
                //chkDate31.Enabled = true;
                ddlFrequency.Enabled = true;
                txtAmount.Enabled = true;
                txtPeriod.Enabled = true;
                trSystematicDate.Visible = true;
                txtSystematicDate.Enabled = true;
                trSystematicDateChk1.Visible = false;
                trSystematicDateChk2.Visible = false;
                trSystematicDateChk3.Visible = false;
                btnSubmit.Text = "Update";

                btnSubmit.Visible = true;
            }
        }

        /// <summary>
        /// Function to submit the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            userVo = (UserVo)Session["userVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            
            if (btnSubmit.Text == "Submit")
            {
                if (Session["systematicSetupVo"] != null)
                    systematicSetupVo = (SystematicSetupVo)Session["systematicSetupVo"];
                if (systematicSetupVo == null )
                {
                    systematicSetupVo = new SystematicSetupVo();
                    //systematicSetupVo.SchemePlanCode = int.Parse(txtSchemeCode.Value);
                }
               
                    
            }
            if (btnSubmit.Text == "Update")
                systematicSetupVo = (SystematicSetupVo)Session["systematicSetupVo"];
            systematicSetupVo.SchemePlanCode = int.Parse(txtSchemeCode.Value);
            systematicSetupVo.SystematicTypeCode = ddlSystematicType.SelectedItem.Value.ToString();
            systematicSetupVo.AccountId = int.Parse(ddlMFAccount.SelectedItem.Value.ToString());
            systematicSetupVo.StartDate = DateTime.Parse(txtStartDate.Text.ToString());
            systematicSetupVo.FrequencyCode = ddlFrequency.SelectedItem.Value.ToString();
            systematicSetupVo.Amount = int.Parse(txtAmount.Text.ToString());
            systematicSetupVo.EndDate = CalcEndDate(int.Parse(txtPeriod.Text.ToString()), DateTime.Parse(txtStartDate.Text.ToString()));
            systematicSetupVo.IsManual = 1;
            systematicSetupVo.SourceCode = "WP";
            if (systematicSetupVo.SystematicTypeCode == "STP" && txtSwicthSchemeSearch.Text!="")
            {
                systematicSetupVo.SchemePlanCodeSwitch = int.Parse(txtSwitchSchemeCode.Value);
                systematicSetupVo.SchemePlanSwitch = txtSwicthSchemeSearch.Text.ToString();
            }
            if (btnSubmit.Text == "Submit")
            {
                //CheckBox chk = new CheckBox();
                //for (int i = 1; i <= 31; i++)
                //{
                //    chk.ID = "chkDate" + i.ToString();
                if (chkDate1.Checked)
                {
                    systematicSetupVo.SystematicDate = 1;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate2.Checked)
                {
                    systematicSetupVo.SystematicDate = 2;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate3.Checked)
                {
                    systematicSetupVo.SystematicDate = 3;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate4.Checked)
                {
                    systematicSetupVo.SystematicDate = 5;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate5.Checked)
                {
                    systematicSetupVo.SystematicDate = 5;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate6.Checked)
                {
                    systematicSetupVo.SystematicDate = 6;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate7.Checked)
                {
                    systematicSetupVo.SystematicDate = 7;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate8.Checked)
                {
                    systematicSetupVo.SystematicDate = 8;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                } 
                if (chkDate9.Checked)
                {
                    systematicSetupVo.SystematicDate = 9;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate10.Checked)
                {
                    systematicSetupVo.SystematicDate = 10;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate11.Checked)
                {
                    systematicSetupVo.SystematicDate = 11;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate12.Checked)
                {
                    systematicSetupVo.SystematicDate = 12;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate13.Checked)
                {
                    systematicSetupVo.SystematicDate = 13;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate14.Checked)
                {
                    systematicSetupVo.SystematicDate = 14;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate15.Checked)
                {
                    systematicSetupVo.SystematicDate = 15;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate16.Checked)
                {
                    systematicSetupVo.SystematicDate = 16;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate17.Checked)
                {
                    systematicSetupVo.SystematicDate = 17;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate18.Checked)
                {
                    systematicSetupVo.SystematicDate = 18;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate19.Checked)
                {
                    systematicSetupVo.SystematicDate = 19;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate20.Checked)
                {
                    systematicSetupVo.SystematicDate = 20;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate21.Checked)
                {
                    systematicSetupVo.SystematicDate = 21;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate22.Checked)
                {
                    systematicSetupVo.SystematicDate = 22;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate23.Checked)
                {
                    systematicSetupVo.SystematicDate = 23;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate24.Checked)
                {
                    systematicSetupVo.SystematicDate = 24;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate25.Checked)
                {
                    systematicSetupVo.SystematicDate = 25;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate26.Checked)
                {
                    systematicSetupVo.SystematicDate = 26;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate27.Checked)
                {
                    systematicSetupVo.SystematicDate = 27;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate28.Checked)
                {
                    systematicSetupVo.SystematicDate = 28;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate29.Checked)
                {
                    systematicSetupVo.SystematicDate = 29;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate30.Checked)
                {
                    systematicSetupVo.SystematicDate = 30;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate31.Checked)
                {
                    systematicSetupVo.SystematicDate = 31;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                    
                }
                if (Session["SourcePage"] != null && Session["SourcePage"].ToString() == "ReconReport")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerMFSystematicTransactionReport','none');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioSystematicView','none');", true);

                }
                //}

            }
            if (btnSubmit.Text == "Update")
            {
                //systematicSetupVo.SystematicSetupId = systematicSetupId;
                systematicSetupVo.SystematicDate = int.Parse(txtSystematicDate.Text.ToString());
                systematicSetupBo.UpdateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioSystematicView','none');", true);

            }

        }

        /// <summary>
        /// Calculates the End Date based on the period by adding it to the Start Date
        /// </summary>
        /// <param name="period"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        private DateTime CalcEndDate(int period, DateTime startDate)
        {
            DateTime endDate = new DateTime();

            endDate = startDate.AddMonths(period);

            return endDate;
        }

        /// <summary>
        /// Function to get the Scheme Code from the selected scheme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtScheme_TextChanged(object sender, EventArgs e)
        {
            //lblScheme.Text = txtSearchScheme.Text;
            //schemePlanCode = productMFBo.GetScheme(txtScheme.Text.ToString());

        }
        protected void txtSchemeCode_ValueChanged(object sender, EventArgs e)
        {
            schemePlanCode = int.Parse(txtSchemeCode.Value.ToString());
            txtSwitchSchemeCode_AutoCompleteExtender.ContextKey = schemePlanCode.ToString();
            //path=Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            //BindDropDowns(path);
        }

        protected void ddlSystematicType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSystematicType.SelectedValue == "STP")
            {
                trSwitchScheme.Visible = true;

            }
            else
            {
                trSwitchScheme.Visible = false;
            }
        }
    }
}