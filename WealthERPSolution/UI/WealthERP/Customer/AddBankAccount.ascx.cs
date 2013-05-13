using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCustomerProfiling;
using VoCustomerProfiling;
using VoUser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using System.Configuration;
using System.Data;
using WealthERP.Base;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;

namespace WealthERP.Customer
{
    public partial class AddBankAccount : System.Web.UI.UserControl
    {
        DataSet dsCustomerBankAccountDetails = new DataSet();
        UserVo userVo = null;
        RMVo rmVo = null;
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo advisorVo = new AdvisorVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
        CustomerBankAccountVo customerBankAccountVo = new CustomerBankAccountVo();
        CustomerAccountsVo customeraccountVo = new CustomerAccountsVo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        List<CustomerAccountsVo> TransactionList = new List<CustomerAccountsVo>();
        DataSet dsCustomerAssociates;
        DataTable dtCustomerAssociates;
        DataTable dtCustomerAssociatesRaw;
        DataRow drCustomerAssociates;
        int bankId;
        static int portfolioId;
        int custBankAccId;
        string path;
        DataRow dr;
        DataTable dt;
        string action;
        DataSet dsBankDetails;
        string currentUserRole = string.Empty;
        string viewForm = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            userVo = (UserVo)Session["userVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            RMVo customerRMVo = new RMVo();
            if (!String.IsNullOrEmpty(Session[SessionContents.CurrentUserRole].ToString()))
            {
                currentUserRole = Session[SessionContents.CurrentUserRole].ToString().ToLower();
            }
            if (Request.QueryString["bankId"] != null)
            {
                bankId = int.Parse(Request.QueryString["bankId"].ToString());
            }
            if (Request.QueryString["action"] != null)
            {
                action = Request.QueryString["action"].ToString();
            }
          
            if (!IsPostBack)
            {
                BindPortfolio();
                BindAccountType();
                BindModeofOperation();
                BindBankName();
                BindState();
                ddlModeofOperation.SelectedValue = "SI";
                ddlModeofOperation.Enabled = false;
            }
            //if (action == "Add")
            //{
            //    Session["customerBankAccountVo" + customerVo.CustomerId] = null;
            //}
            //if (Session["customerBankAccountVo" + customerVo.CustomerId] != null)
            //{
            //    customerBankAccountVo = (CustomerBankAccountVo)Session["customerBankAccountVo" + customerVo.CustomerId];
            //    ddlAccountType.SelectedValue = customerBankAccountVo.AccountType;
            //    txtAccountNumber.Text = customerBankAccountVo.BankAccountNum;
            //    ddlBankName.SelectedValue = customerBankAccountVo.BankName;
            //    txtBranchName.Text = customerBankAccountVo.BranchName;
            //    txtBankAdrLine1.Text = customerBankAccountVo.BranchAdrLine1;
            //    txtBankAdrLine2.Text = customerBankAccountVo.BranchAdrLine2;
            //    txtBankAdrLine3.Text = customerBankAccountVo.BranchAdrLine3;
            //    if (customerBankAccountVo.BranchAdrPinCode.ToString() != "")
            //    {
            //        txtBankAdrPinCode.Text = customerBankAccountVo.BranchAdrPinCode.ToString();
            //    }
            //    else
            //        txtBankAdrPinCode.Text = "";
            //    txtBankAdrCity.Text = customerBankAccountVo.BranchAdrCity;
            //    if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrState))
            //    {
            //        ddlBankAdrState.SelectedValue = customerBankAccountVo.BranchAdrState;
            //    }
            //    else
            //        ddlBankAdrState.SelectedValue = "Select";
            //    txtMicr.Text = customerBankAccountVo.MICR.ToString();
            //    txtIfsc.Text = customerBankAccountVo.IFSC;
            //    btnUpdate.Visible = true;
            //    btnSubmit.Visible = false;
            //    //s  SetVisiblity(0);
            //    // lnkBtnEdit.Visible = true;
            //}
          
                //Session["customerBankAccountVo" + customerVo.CustomerId] = null;
           
          if (action == "View")
            {
                BtnSetVisiblity(0);
                //lnkBack.Visible = true;
                ViewBankAccountDetails();
            }
            else if (action == "Edit")
            {
                BtnSetVisiblity(1);
                EditBankAccountDetails();
            }
        }
        public void BindPortfolio()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolioId.DataSource = ds;
            ddlPortfolioId.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolioId.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolioId.DataBind();
        }
        public void BindBankName()
        {
            DataTable dtBankName = new DataTable();
            dtBankName = customerBankAccountBo.GetALLBankName();
            ddlBankName.DataSource = dtBankName;
            ddlBankName.DataValueField = dtBankName.Columns["WERPBM_BankCode"].ToString();
            ddlBankName.DataTextField = dtBankName.Columns["WERPBM_BankName"].ToString();
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));
        }
        public void BindAccountType()
        {
            DataTable dtAccType = new DataTable();
            dtAccType = customerBankAccountBo.AssetBankaccountType();
            ddlAccountType.DataSource = dtAccType;
            ddlAccountType.DataValueField = dtAccType.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlAccountType.DataTextField = dtAccType.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlAccountType.DataBind();
            ddlAccountType.Items.Insert(0, new ListItem("Select", "Select"));
        }
        public void BindModeofOperation()
        {
            DataTable dtModeOfOpn = new DataTable();
            dtModeOfOpn = customerBankAccountBo.XMLModeOfHolding();
            ddlModeofOperation.DataSource = dtModeOfOpn;
            ddlModeofOperation.DataValueField = dtModeOfOpn.Columns["XMOH_ModeOfHoldingCode"].ToString();
            ddlModeofOperation.DataTextField = dtModeOfOpn.Columns["XMOH_ModeOfHolding"].ToString();
            ddlModeofOperation.DataBind();
            //ddlModeofOperation.SelectedValue = "SI";
            ddlModeofOperation.Items.Insert(0, new ListItem("Select", "Select"));
        }
        public void BindState()
        {
            DataTable dtBankState = new DataTable();
            dtBankState = XMLBo.GetStates(path);
            ddlBankAdrState.DataSource = dtBankState;
            ddlBankAdrState.DataTextField = "StateName";
            ddlBankAdrState.DataValueField = "StateCode";
            ddlBankAdrState.DataBind();
            ddlBankAdrState.Items.Insert(0, new ListItem("Select", "Select"));

        }
        public void BindNominees()
        {
            try
            {
                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];
                dtCustomerAssociates = new DataTable();
                dtCustomerAssociates.Columns.Add("MemberCustomerId");
                dtCustomerAssociates.Columns.Add("AssociationId");
                dtCustomerAssociates.Columns.Add("Name");
                dtCustomerAssociates.Columns.Add("Relationship");

                foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
                {

                    drCustomerAssociates = dtCustomerAssociates.NewRow();
                    drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                    drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                    drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                    drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                    dtCustomerAssociates.Rows.Add(drCustomerAssociates);
                }

                if (dtCustomerAssociates.Rows.Count > 0)
                {
                    gvNominees.DataSource = dtCustomerAssociates;
                    gvNominees.DataBind();
                    gvNominees.Visible = true;
                    trNoNominee.Visible = false;
                    trNomineeCaption.Visible = true;
                    trgvNominees.Visible = true;
                }
                else
                {
                    trNoNominee.Visible = true;
                    trNomineeCaption.Visible = false;
                    trgvNominees.Visible = false;
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
                FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:LoadNominees()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlPortfolio = (DropDownList)sender;
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int i = 0;
            int customerId = 0;
            int accountId = 0;
            RMVo rmVo = new RMVo();
            int userId;
            rmVo = (RMVo)Session["RmVo"];
            userId = rmVo.UserId;
            string chk;

            if (Session["Check"] != null)
            {
                chk = Session["Check"].ToString();
            }
            customerVo = (CustomerVo)Session["customerVo"];
            customerId = customerVo.CustomerId;
            customerBankAccountVo = new CustomerBankAccountVo();
            customerBankAccountVo.PortfolioId = int.Parse(ddlPortfolioId.SelectedItem.Value.ToString()); ;
            customerBankAccountVo.AccountType = ddlAccountType.SelectedValue.ToString();
            customerBankAccountVo.BankAccountNum = txtAccountNumber.Text.ToString();
            if (ddlModeofOperation.SelectedValue.ToString() != "Select a Mode of Holding")
                customerBankAccountVo.ModeOfOperation = ddlModeofOperation.SelectedValue.ToString();
            customerBankAccountVo.BankName = ddlBankName.SelectedValue.ToString();
            customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
            customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
            customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
            customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
            if (txtBankAdrPinCode.Text.ToString() != "")
                customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
            customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
            if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
                customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();
            customerBankAccountVo.BranchAdrCountry = "India";
            if (txtMicr.Text.ToString() != "")
                customerBankAccountVo.MICR = long.Parse(txtMicr.Text.ToString());
            customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
            customerBankAccountVo.Balance = 0;
            if (rbtnNo.Checked)
            {
                customerBankAccountVo.IsJointHolding = 0;
            }
            if (rbtnYes.Checked)
            {
                customerBankAccountVo.IsJointHolding = 1;
            }
            accountId = customerBankAccountBo.CreateCustomerBankAccount(customerBankAccountVo, customerId, userId);
            customerAccountAssociationVo.AccountId = accountId;

            if (gvJointHolders.Items.Count > 0)
            {
                foreach (GridDataItem item in gvJointHolders.Items)
                {
                    CheckBox chkId = (CheckBox)item.FindControl("chkId");
                    if (chkId.Checked)
                    {
                        i++;
                        customerAccountAssociationVo.AssociationId = int.Parse(gvJointHolders.MasterTableView.DataKeyValues[item.ItemIndex]["AssociationId"].ToString());
                        customerAccountAssociationVo.AssociationType = "Joint Holder";
                        customerAccountBo.CreatecustomerBankAccountAssociation(customerAccountAssociationVo, userVo.UserId);
                    }
                }
            }
            else
            {
                i = -1;
            }
            if (rbtnYes.Checked)
            {
                if (i == 0)
                {
                    trNoJointHolders.Visible = true;
                    lblNoJointHolders.Visible = true;
                    lblNoJointHolders.Text = "Please select a Joint Holder";
                    // blResult = false;
                }
                else
                {
                    trNoJointHolders.Visible = false;
                    // trError.Visible = false;
                    lblNoJointHolders.Visible = false;

                }
            }
            txtAccountNumber.Text = "";
            txtBankAdrLine1.Text = "";
            txtBankAdrLine2.Text = "";
            txtBankAdrLine3.Text = "";
            txtBankAdrPinCode.Text = "";
            txtBankAdrCity.Text = "";
            ddlBankName.SelectedIndex = 0;
            txtBranchName.Text = "";
            txtIfsc.Text = "";
            txtMicr.Text = "";
            ddlAccountType.SelectedIndex = 0;
            ddlModeofOperation.SelectedIndex = 0;

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewBankDetails','');", true);

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int customerId = 0;
            //  bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());
            customerVo = (CustomerVo)Session["customerVo"];
            customerId = customerVo.CustomerId;
            customerBankAccountVo.BankAccountNum = txtAccountNumber.Text.ToString();
            customerBankAccountVo.AccountType = ddlAccountType.SelectedItem.Value.ToString();
            if (rbtnNo.Checked)
            {
                customerBankAccountVo.IsJointHolding = 0;
            }
            if (rbtnYes.Checked)
            {
                customerBankAccountVo.IsJointHolding = 1;
            }
            customerBankAccountVo.ModeOfOperation = ddlModeofOperation.SelectedItem.Value.ToString();
            customerBankAccountVo.BankName = ddlBankName.SelectedItem.Value.ToString();
            customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
            customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
            customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
            customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
            if (txtBankAdrPinCode.Text.ToString() != "")
                customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
            else
                customerBankAccountVo.BranchAdrPinCode = 0;
            customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
            if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
                customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();

            customerBankAccountVo.CustBankAccId = bankId;
            customerBankAccountVo.BranchAdrCountry = "India";
            customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
            if (txtMicr.Text.ToString() != "")
                customerBankAccountVo.MICR = int.Parse(txtMicr.Text.ToString());
            else
                customerBankAccountVo.MICR = 0;
            customerBankAccountBo.UpdateCustomerBankAccount(customerBankAccountVo, customerId);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewBankDetails','');", true);
        }
        protected void rbtnYes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnYes.Checked == true)
                {
                    ddlModeofOperation.Enabled = true;
                    dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                    //dtCustomerAssociates.Rows.Clear();
                    dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];
                    dtCustomerAssociates = new DataTable();
                    dtCustomerAssociates.Columns.Clear();
                    dtCustomerAssociates.Columns.Add("MemberCustomerId");
                    dtCustomerAssociates.Columns.Add("AssociationId");
                    dtCustomerAssociates.Columns.Add("Name");
                    dtCustomerAssociates.Columns.Add("Relationship");
                    foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
                    {
                        drCustomerAssociates = dtCustomerAssociates.NewRow();
                        drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                        drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                        drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                        drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                        dtCustomerAssociates.Rows.Add(drCustomerAssociates);
                    }
                    if (dtCustomerAssociates.Rows.Count > 0)
                    {
                        if (Cache["gvjoinholder" + customerVo.CustomerId] == null)
                        {
                            Cache.Insert("gvjoinholder" + customerVo.CustomerId, dtCustomerAssociates);
                        }
                        else
                        {
                            Cache.Remove("gvjoinholder" + customerVo.CustomerId);
                            Cache.Insert("gvjoinholder" + customerVo.CustomerId, dtCustomerAssociates);
                        }
                        gvJointHolders.DataSource = dtCustomerAssociates;
                        gvJointHolders.DataBind();
                        trgvjointHolder.Visible = true;
                        trjointholder.Visible = true;
                    }
                    else
                    {
                        trjointholder.Visible = false;
                        trgvjointHolder.Visible = false;
                        gvJointHolders.Visible = false;

                    }
                    ddlModeofOperation.SelectedIndex = 0;
                }
                if (rbtnNo.Checked == true)
                {
                    ddlModeofOperation.SelectedValue = "SI";
                    ddlModeofOperation.Enabled = false;
                    gvJointHolders.Visible = false;
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
                FunctionInfo.Add("Method", "AddBankAccount.ascx:rbtnYes_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        protected void rbtnNominee_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnomyes.Checked == true)
            {
                trNomineeCaption.Visible = true;
                trgvNominees.Visible = true;
                BindNominees();
            }
            if (rbtnomNo.Checked == true)
            {
                trNomineeCaption.Visible = false;
                trgvNominees.Visible = false;
            }
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            EditBankAccountDetails();
        }
        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewBankDetails", "loadcontrol('ViewBankDetails','none');", true);

        }

        private void EditBankAccountDetails()
        {
            customerBankAccountVo = customerBankAccountBo.GetCusomerIndBankAccount(bankId);
            ddlAccountType.SelectedValue = customerBankAccountVo.AccountType;
            txtAccountNumber.Text = customerBankAccountVo.BankAccountNum;
            ddlBankName.SelectedValue = customerBankAccountVo.BankName;
            txtBranchName.Text = customerBankAccountVo.BranchName;
            txtBankAdrLine1.Text = customerBankAccountVo.BranchAdrLine1;
            txtBankAdrLine2.Text = customerBankAccountVo.BranchAdrLine2;
            txtBankAdrLine3.Text = customerBankAccountVo.BranchAdrLine3;
            if (customerBankAccountVo.BranchAdrPinCode.ToString() != "")
            {
                txtBankAdrPinCode.Text = customerBankAccountVo.BranchAdrPinCode.ToString();
            }
            else
                txtBankAdrPinCode.Text = "";
            txtBankAdrCity.Text = customerBankAccountVo.BranchAdrCity;
            if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrState))
            {
                ddlBankAdrState.SelectedValue = customerBankAccountVo.BranchAdrState;
            }
            else
                ddlBankAdrState.SelectedValue = "Select";
            txtMicr.Text = customerBankAccountVo.MICR.ToString();
            txtIfsc.Text = customerBankAccountVo.IFSC;
            BtnSetVisiblity(1);
            SetVisiblity(1);
        }
        private void ViewBankAccountDetails()
        {
            customerBankAccountVo = customerBankAccountBo.GetCusomerIndBankAccount(bankId);
            ddlAccountType.SelectedValue = customerBankAccountVo.AccountType;
            txtAccountNumber.Text = customerBankAccountVo.BankAccountNum;
            ddlBankName.SelectedValue = customerBankAccountVo.BankName;
            txtBranchName.Text = customerBankAccountVo.BranchName;
            txtBankAdrLine1.Text = customerBankAccountVo.BranchAdrLine1;
            txtBankAdrLine2.Text = customerBankAccountVo.BranchAdrLine2;
            txtBankAdrLine3.Text = customerBankAccountVo.BranchAdrLine3;
            if (customerBankAccountVo.BranchAdrPinCode.ToString() != "")
            {
                txtBankAdrPinCode.Text = customerBankAccountVo.BranchAdrPinCode.ToString();
            }
            else
            txtBankAdrPinCode.Text = "";
            txtBankAdrCity.Text = customerBankAccountVo.BranchAdrCity;
            if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrState))
            {
                ddlBankAdrState.SelectedValue = customerBankAccountVo.BranchAdrState;
            }
            else
             ddlBankAdrState.SelectedValue ="Select";
            txtMicr.Text = customerBankAccountVo.MICR.ToString();
            txtIfsc.Text = customerBankAccountVo.IFSC;
            SetVisiblity(0);
        }
        private void SetVisiblity(int p)
        {
            if (p == 0)
            {
                // For View Mode
                ddlPortfolioId.Enabled = false;
                ddlAccountType.Enabled = false;
                txtAccountNumber.Enabled = false;
                ddlBankName.Enabled = false;
                txtBranchName.Enabled = false;
                txtBankAdrLine1.Enabled =false;
                txtBankAdrLine2.Enabled=false;
                txtBankAdrLine3.Enabled = false;
                txtBankAdrPinCode.Enabled = false;
                txtBankAdrCity.Enabled = false;
                ddlBankAdrState.Enabled = false;
                txtMicr.Enabled = false;
                txtIfsc.Enabled = false;
            }
            else
            {
                //for Edit Mode
                ddlPortfolioId.Enabled = true;
                ddlAccountType.Enabled = true;
                txtAccountNumber.Enabled = true;
                ddlBankName.Enabled = true;
                txtBranchName.Enabled = true;
                txtBankAdrLine1.Enabled = true;
                txtBankAdrLine2.Enabled = true;
                txtBankAdrLine3.Enabled = true;
                txtBankAdrPinCode.Enabled = true;
                txtBankAdrCity.Enabled = true;
                ddlBankAdrState.Enabled = true;
                txtMicr.Enabled = true;
                txtIfsc.Enabled = true;


            }
        }
        private void BtnSetVisiblity(int p)
        {
            if (p == 0)
            {   //for view selected
                //lblView.Text = "Equity Account Details";
                // lblError.Visible = false;
                lnkBtnEdit.Visible = true;
                btnSubmit.Visible = false;
                btnUpdate.Visible = false;
                lnkBtnBack.Visible = false;

            }
            else
            {  //for Edit selected 
                //lblView.Text = "Modify Equity Account";
                //lblError.Visible = true;
                lnkBtnEdit.Visible = false;
                btnSubmit.Visible = false;
                btnUpdate.Visible = true;
                lnkBtnBack.Visible = true;
            }


        }
    }
}