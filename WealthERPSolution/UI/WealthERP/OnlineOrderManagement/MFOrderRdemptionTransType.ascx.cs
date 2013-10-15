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
    public partial class MFOrderRdemptionTransType : System.Web.UI.UserControl
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
        DataTable dtgetfolioNo;
        int retVal;

        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session["userVo"];
            if (!IsPostBack)
            {
                dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(0, customerVo.CustomerId);
                AmcBind();
                txtRedeemTypeValue.Visible = false;
                lblOption.Visible = false;
                lblDividendType.Visible = false;
            }


        }
        protected void AmcBind()
        {
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
                //ResetControlDetails(sender, e);
                GetControlDetails(int.Parse(ddlScheme.SelectedValue), ddlFolio.SelectedValue.ToString());
                SetControlDetails();
            }
        }

        protected void ResetControlDetails(object sender, EventArgs e)
        {
            lblDividendType.Text = "";

            lbltime.Text = "";
            lbldftext.Text = "";
            txtRedeemTypeValue.Text = "";
            lblNavDisplay.Text = "";
            ddlAmc.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlFolio.SelectedIndex = 0;

            ddlDivType.SelectedIndex = 0;


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
            DataSet dsNav = commonLookupBo.GetLatestNav(int.Parse(ddlScheme.SelectedValue));
            string date = Convert.ToDateTime(dsNav.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy");
            lblNavDisplay.Text = dsNav.Tables[0].Rows[0][1] + " " + "As On " + " " + date;
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dtUnit = ds.Tables[1];
                foreach (DataRow drunits in dtUnit.Rows)
                {
                    if (!string.IsNullOrEmpty(drunits["CMFNP_NetHoldings"].ToString()))
                    {
                        lblUnitsheldDisplay.Text = drunits["CMFNP_NetHoldings"].ToString();
                    }
                    if (!string.IsNullOrEmpty(drunits["CMFNP_CurrentValue"].ToString()))
                    {
                        lblCurrentValueDisplay.Text = drunits["CMFNP_CurrentValue"].ToString();
                    }
                }
            }


        }
        protected void SetControlDetails()
        {
            lbltime.Visible = true;
            //lblDividendType.Visible = true;


            lblDivType.Visible = true;
            lblCurrentValueDisplay.Visible = true;
            lblUnitsheldDisplay.Visible = true;
            if (lblDividendType.Text == "Growth")
            {
                lblDividendFrequency.Visible = false;
                lbldftext.Visible = false;
                lblDivType.Visible = false;
                ddlDivType.Visible = false;
                RequiredFieldValidator3.Enabled = false;
            }
            else
            {
                lblDividendFrequency.Visible = true;
                lbldftext.Visible = true;
                lblDivType.Visible = true;
                ddlDivType.Visible = true;
                RequiredFieldValidator3.Enabled = true;

            }
        }
        protected void CategoryBind()
        {
            DataSet dsCategory = new DataSet();
            dsCategory = commonLookupBo.GetAllCategoryList();

            if (dsCategory.Tables[0].Rows.Count > 0)
            {
                ddlCategory.DataSource = dsCategory.Tables[0];
                ddlCategory.DataValueField = dsCategory.Tables[0].Columns["Category_Code"].ToString();
                ddlCategory.DataTextField = dsCategory.Tables[0].Columns["Category_Name"].ToString();
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("All", "0"));
            }
        }

        protected void SchemeBind(int amccode, string category, int customerid)
        {
            DataTable dtScheme = new DataTable();
            dtScheme = commonLookupBo.GetAmcSchemeList(amccode, category, customerid);
            if (dtScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtScheme;
                ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
            }

        }
        private void GetNetpositionValues(int folio, int scheme)
        {

        }
        private void BindFolioNumber(int amcCode)
        {
            DataTable dtScheme = new DataTable();
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
        protected void ddlRedeem_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRedeem.SelectedValue == "1")
            {
                lblRedeemType.Text = "Units";
                txtRedeemTypeValue.Text = null;
            }
            else if (ddlRedeem.SelectedValue == "2")
            {
                lblRedeemType.Text = "Amounts";
                txtRedeemTypeValue.Text = null;
            }
            else if (ddlRedeem.SelectedValue == "3")
            {
                lblRedeemType.Text = "All";
                txtRedeemTypeValue.Text = lblUnitsheldDisplay.Text;
            }
            txtRedeemTypeValue.Visible = true;
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
        protected void PurchaseOrderControlsEnable(bool enable)
        {
            if (!enable)
            {
                ddlAmc.Enabled = false;
                ddlCategory.Enabled = false;
                ddlScheme.Enabled = false;
                ddlFolio.Enabled = false;
                txtRedeemTypeValue.Enabled = false;
                ddlDivType.Enabled = false;
                lnkFactSheet.Enabled = false;
                btnSubmit.Enabled = false;
                ddlRedeem.Enabled = false;
            }
            else
            {
                ddlAmc.Enabled = true;
                ddlCategory.Enabled = true;
                ddlScheme.Enabled = true;
                ddlFolio.Enabled = true;
                txtRedeemTypeValue.Enabled = true;
                ddlDivType.Enabled = true;
                lnkFactSheet.Enabled = true;
                btnSubmit.Enabled = true;
                ddlRedeem.Enabled = true;


            }

        }
        protected void OnClick_Submit(object sender, EventArgs e)
        {
            List<int> OrderIds = new List<int>();

            onlinemforderVo.SchemePlanCode = Int32.Parse(ddlScheme.SelectedValue.ToString());

            onlinemforderVo.FolioNumber = ddlFolio.SelectedValue;
            onlinemforderVo.DividendType = ddlDivType.SelectedValue;
            onlinemforderVo.TransactionType = "Sel";
            if (ddlRedeem.SelectedValue == "1")
                if (!string.IsNullOrEmpty(txtRedeemTypeValue.Text))
                    onlinemforderVo.Redeemunits = double.Parse(txtRedeemTypeValue.Text);
                else
                    onlinemforderVo.Redeemunits = 0;
            else if (ddlRedeem.SelectedValue == "2")
                if (!string.IsNullOrEmpty(txtRedeemTypeValue.Text))
                    onlinemforderVo.Amount = double.Parse(txtRedeemTypeValue.Text);
                else
                    onlinemforderVo.Amount = 0;
            float amt;
            float minAmt;
            float multiAmt;
            DateTime Dt;


            amt = 0;



            minAmt = 0;
            multiAmt = 0;
            Dt = DateTime.Parse(lbltime.Text);

            //int retVal = commonLookupBo.IsRuleCorrect(amt, minAmt, amt, multiAmt, Dt);
            if (Dt.TimeOfDay < DateTime.Now.TimeOfDay)
            {
                retVal = 1;
            }
            if (retVal != 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please enter a valid amount');", true); return;
            }


            OrderIds = onlineMforderBo.CreateCustomerOnlineMFOrderDetails(onlinemforderVo, userVo.UserId, customerVo.CustomerId);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Your order added successfully.');", true);
            OrderId = int.Parse(OrderIds[0].ToString());
            PurchaseOrderControlsEnable(false);
        }



    }
}