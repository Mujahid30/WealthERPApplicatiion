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

        string strVisibility = string.Empty;
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
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        DataSet dsCustomerAssociates;
        DataTable dtCustomerAssociates;
        DataTable dtCustomerAssociatesRaw;
        DataRow drCustomerAssociates;
        int bankId = 0;
        static int portfolioId;
        int custBankAccId;
        string path;
        DataRow dr;
        DataTable dt;
        string action;
        string mode;
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

            if (Request.QueryString["bankId"] != null)
            {
                bankId = int.Parse(Request.QueryString["bankId"].ToString());
            }
            if (Request.QueryString["action"] != null)
            {
                action = Request.QueryString["action"].ToString();
                strVisibility = "View";

            }
            if (Request.QueryString["strModeOfOperation"] != null)
            {
                mode = Request.QueryString["strModeOfOperation"].ToString();
            }
            if (!IsPostBack)
            {
                ViewState["Action"] = action;
                BindPortfolio();
                BindAccountType();
                BindModeofOperation();
                BindBankName();
                //BindState();
                //BindCity();
                //BindCountry();
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

            if (ViewState["Action"] != null)
            {
                if (ViewState["Action"].ToString() == "View")
                {
                    //lnkBack.Visible = true;
                    ViewBankAccountDetails();
                    BtnSetVisiblity(0);
                    rbtnomyes.Enabled = false;
                    rbtnomNo.Enabled = false;
                    ViewState["Action"] = null;
                }
                else if (ViewState["Action"].ToString() == "Edit")
                {
                    EditBankAccountDetails();
                    BtnSetVisiblity(1);
                }
            }
        }
        public void BindPortfolio()
        {
            if (customerVo.CustomerId == 0)
                return;

            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolioId.DataSource = ds;
            ddlPortfolioId.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolioId.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolioId.DataBind();
        }
        public void BindBankName()
        {
            DataTable dtBankName;
            dtBankName = commonLookupBo.GetWERPLookupMasterValueList(7000, 0);
            ddlBankName.DataSource = dtBankName;
            ddlBankName.DataValueField = "WCMV_LookupId";
            ddlBankName.DataTextField = "WCMV_Name";
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }
        public void BindAccountType()
        {
            DataTable dtAccType;
            dtAccType = commonLookupBo.GetWERPLookupMasterValueList(5000, 0);
            ddlAccountType.DataSource = dtAccType;
            ddlAccountType.DataValueField = "WCMV_LookupId";
            ddlAccountType.DataTextField = "WCMV_Name";
            ddlAccountType.DataBind();
            ddlAccountType.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }
        public void BindModeofOperation()
        {
            DataTable dtModeOfOpn;
            dtModeOfOpn = customerBankAccountBo.XMLModeOfHolding();
            ddlModeofOperation.DataSource = dtModeOfOpn;
            ddlModeofOperation.DataValueField = dtModeOfOpn.Columns["XMOH_ModeOfHoldingCode"].ToString();
            ddlModeofOperation.DataTextField = dtModeOfOpn.Columns["XMOH_ModeOfHolding"].ToString();
            ddlModeofOperation.DataBind();
            //ddlModeofOperation.SelectedValue = "SI";
            ddlModeofOperation.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }
//sr     //public void BindState()
        //{
        //    DataTable dtBankState;
        //    dtBankState = commonLookupBo.GetWERPLookupMasterValueList(9000, 0);
        //    ddlBankAdrState.DataSource = dtBankState;
        //    ddlBankAdrState.DataTextField = "WCMV_Name";
        //    ddlBankAdrState.DataValueField = "WCMV_LookupId";
        //    ddlBankAdrState.DataBind();
        //    ddlBankAdrState.Items.Insert(0, new ListItem("--SELECT--", "0"));

        //}
//sr        //public void BindCountry()
        //{
        //      DataTable dtBankCountry;
        //    dtBankCountry = commonLookupBo.GetWERPLookupMasterValueList(13000, 0);
        //    ddlBankAdrCountry.DataSource = dtBankCountry;
        //    ddlBankAdrCountry.DataTextField = "WCMV_Name";
        //    ddlBankAdrCountry.DataValueField = "WCMV_LookupId";
        //    ddlBankAdrCountry.DataBind();
        //    ddlBankAdrCountry.Items.Insert(0, new ListItem("--SELECT--", "0"));

        //}


//sr        //public void BindCity()
        //{
        //    DataTable dtBankCity;
        //    dtBankCity = commonLookupBo.GetWERPLookupMasterValueList(8000, 0);
        //    ddlBankAdrCity.DataSource = dtBankCity;
        //    ddlBankAdrCity.DataTextField = "WCMV_Name";
        //    ddlBankAdrCity.DataValueField = "WCMV_LookupId";
        //    ddlBankAdrCity.DataBind();
        //    ddlBankAdrCity.Items.Insert(0, new ListItem("--SELECT--", "0"));

        //}

        public void BindNominees()
        {
            try
            {
                //if (rbtnomyes.Checked == true)
                //    strVisibility = "Edit";
                //dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRelForCashAndSavings(customerVo.CustomerId, strVisibility);

                //string expressionForRowsWithoutFM;
                //expressionForRowsWithoutFM = "CCSAA_AssociationType LIKE" + "'%Nominee%'";


                //DataTable dtWithoutFM = new DataTable();

                //DataView dvMFTransactionsProcessed = new DataView(dsCustomerAssociates.Tables[0], expressionForRowsWithoutFM, "CCSAA_AssociationType", DataViewRowState.CurrentRows);
                //dtWithoutFM = dvMFTransactionsProcessed.ToTable();


                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                //dtCustomerAssociates.Rows.Clear();
                dtCustomerAssociatesRaw = new DataTable();
                //dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                //dtCustomerAssociates.Rows.Clear();
                dtCustomerAssociates = dsCustomerAssociates.Tables[0];

                dtCustomerAssociatesRaw.Columns.Clear();
                dtCustomerAssociatesRaw.Columns.Add("MemberCustomerId");
                dtCustomerAssociatesRaw.Columns.Add("AssociationId");
                dtCustomerAssociatesRaw.Columns.Add("Name");
                dtCustomerAssociatesRaw.Columns.Add("Relationship");

                foreach (DataRow dr in dtCustomerAssociates.Rows)
                {
                    drCustomerAssociates = dtCustomerAssociatesRaw.NewRow();
                    drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                    drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                    drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                    drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                    dtCustomerAssociatesRaw.Rows.Add(drCustomerAssociates);
                }
                if (dtCustomerAssociatesRaw.Rows.Count > 0)
                {
                    gvNominees.DataSource = dtCustomerAssociatesRaw;
                    gvNominees.DataBind();

                    DataTable dtNOM = (DataTable)ViewState["dtNOM"];
                    if (dtNOM != null)
                    {
                        DataRow[] drjointDt;
                        foreach (GridDataItem item in this.gvNominees.Items)
                        {
                            CheckBox chkId = (CheckBox)item.FindControl("chkId0");
                            int assosiationId = int.Parse(gvNominees.MasterTableView.DataKeyValues[item.ItemIndex]["AssociationId"].ToString());
                            drjointDt = dtNOM.Select("AssociationId=" + assosiationId.ToString());
                            if (drjointDt.Count() > 0)
                            {
                                chkId.Checked = true;
                            }

                        }
                    }
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
            //customerBankAccountVo.AccountType = ddlAccountType.SelectedValue.ToString();
            customerBankAccountVo.BankAccountNum = txtAccountNumber.Text.ToString();
            if (ddlModeofOperation.SelectedValue.ToString() != "Select a Mode of Holding")
                customerBankAccountVo.ModeOfOperation = ddlModeofOperation.SelectedValue.ToString();
            //customerBankAccountVo.BankName = ddlBankName.SelectedValue.ToString();
            customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
            customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
            customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
            customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
            customerBankAccountVo.BankCity = txtBankCity.Text.ToString();
            customerBankAccountVo.BankBranchCode = txtBankBranchCode.Text.ToString();
            if (txtBankAdrPinCode.Text.ToString() != "")
                customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
            //if (txtNEFTCode.Text.ToString() != "")
                customerBankAccountVo.NeftCode = txtNEFTCode.Text.ToString();
           //if (txtRTGSCode.Text.ToString() != "")
                customerBankAccountVo.RTGSCode = txtRTGSCode.Text.ToString();
            //customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();

            //if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
            //    customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();
            //customerBankAccountVo.BranchAdrCountry = "India";
            //if (txtMicr.Text.ToString() != "")
                customerBankAccountVo.MICR = txtMicr.Text.ToString();
            customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
            customerBankAccountVo.Balance = 0;

            customerBankAccountVo.BankAccTypeId = int.Parse(ddlAccountType.SelectedValue.ToString());
            customerBankAccountVo.BankId = int.Parse(ddlBankName.SelectedValue.ToString());

//sr        if (ddlBankAdrCity.SelectedIndex != 0)
            //    customerBankAccountVo.BranchAddCityId = int.Parse(ddlBankAdrCity.SelectedValue.ToString());
            //if (ddlBankAdrState.SelectedIndex != 0)
            //    customerBankAccountVo.BranchAddStateId = int.Parse(ddlBankAdrState.SelectedValue.ToString());
            //if (ddlBankAdrCountry.SelectedIndex != 0)
            //    customerBankAccountVo.BranchAddCountryId = int.Parse(ddlBankAdrCountry.SelectedValue.ToString());
            if (RadioButton1.Checked)
            {
                customerBankAccountVo.IsJointHolding = 0;
            }
            if (rbtnYes.Checked)
            {
                customerBankAccountVo.IsJointHolding = 1;
            }
            if (chk_Ismain.Checked)
            {
                customerBankAccountVo.IsCurrent = true;
            }
            else
            {
                customerBankAccountVo.IsCurrent = false;
            }
            accountId = customerBankAccountBo.CreateCustomerBankAccount(customerBankAccountVo, customerId, userId);
            customerAccountAssociationVo.AccountId = accountId;
            foreach (GridDataItem gvr in this.gvNominees.Items)
            {
                CheckBox chkIdn = (CheckBox)gvr.FindControl("chkId0");
                if (chkIdn.Checked)
                {
                    i++;
                    customerAccountAssociationVo.AssociationId = int.Parse(gvNominees.MasterTableView.DataKeyValues[gvr.ItemIndex]["AssociationId"].ToString());
                    //  customerAccountAssociationVo.AssociationId = AssociationId;
                    customerAccountAssociationVo.AssociationType = "Nominee";
                    customerAccountBo.CreatecustomerBankAccountAssociation(customerAccountAssociationVo, userVo.UserId);
                }
            }
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
            //txtAccountNumber.Text = "";
            //txtBankAdrLine1.Text = "";
            //txtBankAdrLine2.Text = "";
            //txtBankAdrLine3.Text = "";
            //txtBankAdrPinCode.Text = "";
            //txtBankAdrCity.Text = "";
            //ddlBankName.SelectedIndex = 0;
            //txtBranchName.Text = "";
            //txtIfsc.Text = "";
            //txtMicr.Text = "";
            //ddlAccountType.SelectedIndex = 0;
            //ddlModeofOperation.SelectedValue = "SI";
            //ddlModeofOperation.Enabled = false;
            //rbtnomNo.Checked = true;
            //RadioButton1.Checked = true;
            //trNomineeCaption.Visible = false;
            //trjointholder.Visible = false;
            //trgvjointHolder.Visible = false;
            //trgvNominees.Visible = false;
            //gvNominees.DataSource = null;
            //gvNominees.DataBind();
            //gvJointHolders.DataSource = null;
            //gvJointHolders.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mykey", "ClosePopUp();", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewBankDetails','');", true);

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int i = 0;
            int customerId = 0;
            //  bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());
            customerVo = (CustomerVo)Session["customerVo"];
            customerId = customerVo.CustomerId;
            customerBankAccountVo.BankAccountNum = txtAccountNumber.Text.ToString();
            //customerBankAccountVo.AccountType = ddlAccountType.SelectedItem.Value.ToString();
            customerBankAccountVo.CustBankAccId = bankId;
            if (RadioButton1.Checked)
            {
                customerBankAccountVo.IsJointHolding = 0;
            }
            if (rbtnYes.Checked)
            {
                customerBankAccountVo.IsJointHolding = 1;
            }
            customerBankAccountVo.ModeOfOperation = ddlModeofOperation.SelectedItem.Value.ToString();
            //customerBankAccountVo.BankName = ddlBankName.SelectedItem.Value.ToString();
            customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
            customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
            customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
            customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
            customerBankAccountVo.BankCity = txtBankCity.Text.ToString();
            customerBankAccountVo.BankBranchCode= txtBankBranchCode.Text.ToString();
            if (chk_Ismain.Checked)
            {
                customerBankAccountVo.IsCurrent = true;
            }
            else
            {
                customerBankAccountVo.IsCurrent = false;
            }
            if (txtBankAdrPinCode.Text.ToString() != "")
                customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
            else
                customerBankAccountVo.BranchAdrPinCode = 0;
            if (txtNEFTCode.Text.ToString() != "")
                customerBankAccountVo.NeftCode = txtNEFTCode.Text.ToString();
            else
                customerBankAccountVo.NeftCode = null;
            if (txtRTGSCode.Text.ToString() != "")
                customerBankAccountVo.RTGSCode = txtRTGSCode.Text.ToString();
            else
                customerBankAccountVo.RTGSCode = null;

            //customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
            //if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
            //customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();

            //customerBankAccountVo.CustBankAccId = bankId;
            //customerBankAccountVo.BranchAdrCountry = "India";

            customerBankAccountVo.BankAccTypeId = int.Parse(ddlAccountType.SelectedValue.ToString());
            customerBankAccountVo.BankId = int.Parse(ddlBankName.SelectedValue.ToString());

//sr         if (ddlBankAdrCity.SelectedIndex != 0)
            //    customerBankAccountVo.BranchAddCityId = int.Parse(ddlBankAdrCity.SelectedValue.ToString());
            //if (ddlBankAdrState.SelectedIndex != 0)
            //    customerBankAccountVo.BranchAddStateId = int.Parse(ddlBankAdrState.SelectedValue.ToString());
            //if (ddlBankAdrCountry.SelectedIndex != 0)
            //    customerBankAccountVo.BranchAddCountryId = int.Parse(ddlBankAdrCountry.SelectedValue.ToString());

            customerBankAccountVo.MICR = txtMicr.Text.ToString();
            customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
            //if (txtMicr.Text.ToString() != "")
            //    customerBankAccountVo.MICR = int.Parse(txtMicr.Text.ToString());
            //else
            //    customerBankAccountVo.MICR = 0;

            customerAccountAssociationVo.AccountId = bankId;
            if (customerBankAccountBo.UpdateCustomerBankAccount(customerBankAccountVo, customerId))
            {
                customerBankAccountBo.DeleteCustomerBankAccountAssociates(bankId);
                foreach (GridDataItem gvr in this.gvNominees.Items)
                {
                    CheckBox chkIdn = (CheckBox)gvr.FindControl("chkId0");
                    if (chkIdn.Checked && rbtnomyes.Checked)
                    {
                        i++;
                        customerAccountAssociationVo.AssociationId = int.Parse(gvNominees.MasterTableView.DataKeyValues[gvr.ItemIndex]["AssociationId"].ToString());
                        //  customerAccountAssociationVo.AssociationId = AssociationId;
                        customerAccountAssociationVo.AssociationType = "Nominee";
                        customerAccountBo.CreatecustomerBankAccountAssociation(customerAccountAssociationVo, userVo.UserId);
                    }
                }
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
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mykey", "ClosePopUp();", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewBankDetails','');", true);
        }
        protected void rbtnYes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnYes.Checked == true)
                {
                    ddlModeofOperation.Enabled = true;
                    //strVisibility = "Edit";
                    //dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRelForCashAndSavings(customerVo.CustomerId, strVisibility);

                    //string expressionForRowsWithoutFM;
                    //expressionForRowsWithoutFM = "CCSAA_AssociationType LIKE" + "'%Joint Holder%'";


                    //DataTable dtWithoutFM = new DataTable();

                    //DataView dvMFTransactionsProcessed = new DataView(dsCustomerAssociates.Tables[0], expressionForRowsWithoutFM, "CCSAA_AssociationType", DataViewRowState.CurrentRows);
                    //dtWithoutFM = dvMFTransactionsProcessed.ToTable();


                    dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                    //dtCustomerAssociates.Rows.Clear();
                    dtCustomerAssociatesRaw = new DataTable();
                    //dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                    //dtCustomerAssociates.Rows.Clear();
                    dtCustomerAssociates = dsCustomerAssociates.Tables[0];

                    dtCustomerAssociatesRaw.Columns.Clear();
                    dtCustomerAssociatesRaw.Columns.Add("MemberCustomerId");
                    dtCustomerAssociatesRaw.Columns.Add("AssociationId");
                    dtCustomerAssociatesRaw.Columns.Add("Name");
                    dtCustomerAssociatesRaw.Columns.Add("Relationship");

                    foreach (DataRow dr in dtCustomerAssociates.Rows)
                    {
                        drCustomerAssociates = dtCustomerAssociatesRaw.NewRow();
                        drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                        drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                        drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                        drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                        dtCustomerAssociatesRaw.Rows.Add(drCustomerAssociates);
                    }
                    if (dtCustomerAssociatesRaw.Rows.Count > 0)
                    {
                        //if (Cache["gvjoinholder" + customerVo.CustomerId] == null)
                        //{
                        //    Cache.Insert("gvjoinholder" + customerVo.CustomerId, dtCustomerAssociates);
                        //}
                        //else
                        //{
                        //    Cache.Remove("gvjoinholder" + customerVo.CustomerId);
                        //    Cache.Insert("gvjoinholder" + customerVo.CustomerId, dtCustomerAssociates);
                        //}
                        gvJointHolders.DataSource = dtCustomerAssociatesRaw;
                        gvJointHolders.DataBind();
                        Session["jointholder"] = dtCustomerAssociatesRaw;
                        gvJointHolders.Visible = true;
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
                if (RadioButton1.Checked == true)
                {
                    ddlModeofOperation.SelectedValue = "SI";
                    ddlModeofOperation.Enabled = false;
                    gvJointHolders.Visible = false;
                    trjointholder.Visible = false;
                    trgvjointHolder.Visible = false;
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
                //if (ViewState["Action"] != null)
                // {
                //    if (ViewState["Action"].ToString() != "Edit")
                //    {
                BindNominees();
                //    }

                // }

            }
            if (rbtnomNo.Checked == true)
            {
                trNomineeCaption.Visible = false;
                trgvNominees.Visible = false;
            }
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            rbtnomyes.Enabled = true;
            rbtnomNo.Enabled = true;

            dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
            dtCustomerAssociatesRaw = new DataTable();
            //dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
            //dtCustomerAssociates.Rows.Clear();
            dtCustomerAssociates = dsCustomerAssociates.Tables[0];

            dtCustomerAssociatesRaw.Columns.Clear();
            dtCustomerAssociatesRaw.Columns.Add("MemberCustomerId");
            dtCustomerAssociatesRaw.Columns.Add("AssociationId");
            dtCustomerAssociatesRaw.Columns.Add("Name");
            dtCustomerAssociatesRaw.Columns.Add("Relationship");

            foreach (DataRow dr in dtCustomerAssociates.Rows)
            {
                drCustomerAssociates = dtCustomerAssociatesRaw.NewRow();
                drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                dtCustomerAssociatesRaw.Rows.Add(drCustomerAssociates);
            }


            ViewState["Action"] = "Edit";
            ViewState["newTable"] = dtCustomerAssociatesRaw;

            strVisibility = "Edit";
            //dt=(DataTable)Cache["gvjoinholder" + customerVo.CustomerId];
            DataSet ds = new DataSet();
            dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRelForCashAndSavings(customerVo.CustomerId, "Edit");

            string strJH;
            strJH = "CCSAA_AssociationType LIKE" + "'%Joint Holder%'";


            DataTable dtJH = new DataTable();

            DataView dvJH = new DataView(dsCustomerAssociates.Tables[0], strJH, "CCSAA_AssociationType", DataViewRowState.CurrentRows);
            dtJH = dvJH.ToTable();

            string strNOM;
            strNOM = "CCSAA_AssociationType LIKE" + "'%Nominee%'";


            DataTable dtNOM = new DataTable();

            DataView dvNOM = new DataView(dsCustomerAssociates.Tables[0], strNOM, "CCSAA_AssociationType", DataViewRowState.CurrentRows);
            dtNOM = dvNOM.ToTable();

            if (rbtnYes.Checked == true)
            {
                ddlModeofOperation.Enabled = true;
                // ddlModeofOperation.SelectedItem.Value=customerBankAccountVo.ModeOfOperation;
                ddlModeofOperation.SelectedValue = mode;
                gvJointHolders.DataSource = dtCustomerAssociatesRaw;
                gvJointHolders.DataBind();
                DataTable dtJh = (DataTable)ViewState["dtJH"];
                if (dtJh != null)
                {
                    DataRow[] drjointDt;
                    foreach (GridDataItem item in this.gvJointHolders.Items)
                    {
                        CheckBox chkId = (CheckBox)item.FindControl("chkId");
                        int assosiationId = int.Parse(gvJointHolders.MasterTableView.DataKeyValues[item.ItemIndex]["AssociationId"].ToString());
                        drjointDt = dtJh.Select("AssociationId=" + assosiationId.ToString());
                        if (drjointDt.Count() > 0)
                        {
                            chkId.Checked = true;
                        }

                    }
                }
                trjointholder.Visible = true;
                trgvjointHolder.Visible = true;
                gvJointHolders.Visible = true;

            }
            else
            {
                ddlModeofOperation.Enabled = false;
                gvJointHolders.Visible = false;
                trgvjointHolder.Visible = false;
                trjointholder.Visible = false;
            }

            if (rbtnomyes.Checked == true)
            {
                ViewState["newTable"] = dtCustomerAssociatesRaw;
                //ddlModeofOperation.Enabled = true;
                //ddlModeofOperation.SelectedIndex = 0;

                gvNominees.DataSource = dtCustomerAssociatesRaw;
                gvNominees.DataBind();
                DataTable dtNom = (DataTable)ViewState["dtNOM"];
                if (dtNom != null)
                {
                    DataRow[] drNomineeDt;
                    foreach (GridDataItem item in this.gvNominees.Items)
                    {
                        CheckBox chkIdn = (CheckBox)item.FindControl("chkId0");
                        int assosiationId = int.Parse(gvNominees.MasterTableView.DataKeyValues[item.ItemIndex]["AssociationId"].ToString());
                        drNomineeDt = dtNom.Select("AssociationId=" + assosiationId.ToString());
                        if (drNomineeDt.Count() > 0)
                        {
                            chkIdn.Checked = true;
                        }

                    }
                }
                trgvNominees.Visible = true;
                gvNominees.Visible = true;
                trNomineeCaption.Visible = true;

            }
            else
            {
                // ddlModeofOperation.Enabled = false;
                gvNominees.Visible = false;
                trNomineeCaption.Visible = false;
                trgvNominees.Visible = false;
            }
            //ViewBankAccountDetails();
            BtnSetVisiblity(1);
            SetVisiblity(1);
        }
        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewBankDetails", "loadcontrol('ViewBankDetails','none');", true);

        }

        protected void EditBankAccountDetails()
        {
            //customerBankAccountVo = customerBankAccountBo.GetCusomerIndBankAccount(bankId);
            //ddlAccountType.SelectedValue = customerBankAccountVo.AccountType;
            //txtAccountNumber.Text = customerBankAccountVo.BankAccountNum;
            //ddlBankName.SelectedValue = customerBankAccountVo.BankName;
            ////if (customerBankAccountVo.IsJointHolding == 1)
            ////{                
            ////    rbtnYes.Checked = true;
            ////    RadioButton1.Checked = false;
            ////}
            ////else
            ////{
            ////    RadioButton1.Checked = true;
            ////    rbtnYes.Checked = false;
            ////}
            //txtBranchName.Text = customerBankAccountVo.BranchName;
            //txtBankAdrLine1.Text = customerBankAccountVo.BranchAdrLine1;
            //txtBankAdrLine2.Text = customerBankAccountVo.BranchAdrLine2;
            //txtBankAdrLine3.Text = customerBankAccountVo.BranchAdrLine3;
            //if (customerBankAccountVo.BranchAdrPinCode.ToString() != "")
            //{
            //    txtBankAdrPinCode.Text = customerBankAccountVo.BranchAdrPinCode.ToString();
            //}
            //else
            //    txtBankAdrPinCode.Text = "";
            //txtBankAdrCity.Text = customerBankAccountVo.BranchAdrCity;
            //if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrState))
            //{
            //    ddlBankAdrState.SelectedValue = customerBankAccountVo.BranchAdrState;
            //}
            //else
            //    ddlBankAdrState.SelectedValue = "Select";
            //txtMicr.Text = customerBankAccountVo.MICR.ToString();
            //txtIfsc.Text = customerBankAccountVo.IFSC;
            BtnSetVisiblity(1);
            SetVisiblity(1);
        }
        protected void ViewBankAccountDetails()
        {
            customerBankAccountVo = customerBankAccountBo.GetCusomerIndBankAccount(bankId);
            ddlPortfolioId.SelectedValue = customerBankAccountVo.PortfolioId.ToString();
            txtAccountNumber.Text = customerBankAccountVo.BankAccountNum;

            ddlAccountType.SelectedValue = customerBankAccountVo.BankAccTypeId.ToString();
            ddlBankName.SelectedValue = customerBankAccountVo.BankId.ToString();

//sr            if (customerBankAccountVo.BranchAddCityId != 0)
            //    ddlBankAdrCity.SelectedValue = customerBankAccountVo.BranchAddCityId.ToString();
            //if (customerBankAccountVo.BranchAddStateId != 0)
            //    ddlBankAdrState.SelectedValue = customerBankAccountVo.BranchAddStateId.ToString();
            //if (customerBankAccountVo.BranchAddCountryId != 0)
            //    ddlBankAdrCountry.SelectedValue = customerBankAccountVo.BranchAddCountryId.ToString();

            if (customerBankAccountVo.IsJointHolding == 1)
            {
                rbtnYes.Checked = true;
                ddlModeofOperation.SelectedValue = customerBankAccountVo.ModeOfOperation;
                RadioButton1.Checked = false;
                trjointholder.Visible = true;
                trgvjointHolder.Visible = true;
                gvJointHolders.Visible = true;

            }
            else
            {
                RadioButton1.Checked = true;
                ddlModeofOperation.SelectedValue = "SI";
                rbtnYes.Checked = false;
                trjointholder.Visible = false;
                trgvjointHolder.Visible = false;
                gvJointHolders.Visible = false;
            }
            if (customerBankAccountVo.IsCurrent!=false)
            {
                chk_Ismain.Checked = true;
            }
            txtBankCity.Text = customerBankAccountVo.BankCity;
            txtBranchName.Text = customerBankAccountVo.BranchName;
            txtBankAdrLine1.Text = customerBankAccountVo.BranchAdrLine1;
            txtBankAdrLine2.Text = customerBankAccountVo.BranchAdrLine2;
            txtBankAdrLine3.Text = customerBankAccountVo.BranchAdrLine3;
            txtBankCity.Text = customerBankAccountVo.BankCity;
            txtRTGSCode.Text = customerBankAccountVo.RTGSCode;
            txtNEFTCode.Text = customerBankAccountVo.NeftCode;
            txtBankBranchCode.Text = customerBankAccountVo.BankBranchCode;
            if (customerBankAccountVo.BranchAdrPinCode.ToString() != "")
            {
                txtBankAdrPinCode.Text = customerBankAccountVo.BranchAdrPinCode.ToString();
            }
            else
                txtBankAdrPinCode.Text = "";

           

            //txtBankAdrCity.Text = customerBankAccountVo.BranchAdrCity;
            //if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrState))
            //{
            //    ddlBankAdrState.SelectedValue = customerBankAccountVo.BranchAdrState;
            //}
            //else
            //    ddlBankAdrState.SelectedValue = "Select";
            if (!string.IsNullOrEmpty(customerBankAccountVo.MICR.ToString()))
            {
            txtMicr.Text = customerBankAccountVo.MICR.ToString();
            }
            else
            {
             txtMicr.Text="";
            }
                txtIfsc.Text = customerBankAccountVo.IFSC;

            DataSet ds = new DataSet();
            ds = dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRelForCashAndSavings(customerBankAccountVo.CustBankAccId, strVisibility);

            if (strVisibility == "View")
                ViewState["JointHold"] = ds;
            strVisibility = string.Empty;

            string expressionForRowsWithoutFM;
            expressionForRowsWithoutFM = "CCSAA_AssociationType LIKE" + "'%Joint Holder%'";

            string expressionForRowsWithoutFMNOM;
            expressionForRowsWithoutFMNOM = "CCSAA_AssociationType LIKE" + "'%Nominee%'";


            DataTable dtJH = new DataTable();

            DataTable dtNOM = new DataTable();

            DataView dvJH = new DataView(ds.Tables[0], expressionForRowsWithoutFM, "CCSAA_AssociationType", DataViewRowState.CurrentRows);
            dtJH = dvJH.ToTable();

            DataView dvNOM = new DataView(ds.Tables[0], expressionForRowsWithoutFMNOM, "CCSAA_AssociationType", DataViewRowState.CurrentRows);
            dtNOM = dvNOM.ToTable();


            gvJointHolders.DataSource = dtJH;
            ViewState["dtJH"] = dtJH;
            gvJointHolders.DataBind();
            if (dtJH != null)
            {
                DataRow[] drJointDt;
                foreach (GridDataItem item in this.gvJointHolders.Items)
                {
                    CheckBox chkId = (CheckBox)item.FindControl("chkId");
                    int assosiationId = int.Parse(gvJointHolders.MasterTableView.DataKeyValues[item.ItemIndex]["AssociationId"].ToString());
                    drJointDt = dtJH.Select("AssociationId=" + assosiationId.ToString());
                    if (drJointDt.Count() > 0)
                    {
                        chkId.Checked = true;
                    }


                    // if(gvNominees.MasterTableView.DataKeyValues[selectedRow-1]["AssociationId"].ToString()==dr["AssociationId"].ToString())
                    //if (gvNominees.MasterTableView.DataKeyValues[selectedRow - 1]["AssociationId"].ToString() == dr["AssociationId"].ToString())
                    //   chkBoxnom.Checked = true;


                }
            }

            //trjointholder.Visible = true;
            //trgvjointHolder.Visible = true;
            //gvJointHolders.Visible = true;

            gvNominees.DataSource = dtNOM;
            gvNominees.DataBind();
            ViewState["dtNOM"] = dtNOM;

            if (dtNOM != null)
            {
                DataRow[] drNomineeDt;
                foreach (GridDataItem item in this.gvNominees.Items)
                {
                    CheckBox chkIdn = (CheckBox)item.FindControl("chkId0");
                    int assosiationId = int.Parse(gvNominees.MasterTableView.DataKeyValues[item.ItemIndex]["AssociationId"].ToString());
                    drNomineeDt = dtNOM.Select("AssociationId=" + assosiationId.ToString());
                    if (drNomineeDt.Count() > 0)
                    {
                        chkIdn.Checked = true;
                    }
                }
            }
            if (dtNOM.Rows.Count > 0)
            {
                rbtnomyes.Checked = true;
                rbtnomNo.Checked = false;
                trNomineeCaption.Visible = true;
                trgvNominees.Visible = true;
                gvNominees.Visible = true;
                //trNomineeCaption.Visible = false;
                //trgvNominees.Visible = false;
                //gvNominees.Visible = false;
            }
            else
            {
                rbtnomyes.Checked = false;
                rbtnomNo.Checked = true;
                trNomineeCaption.Visible = false;
                trgvNominees.Visible = false;
                gvNominees.Visible = false;

            }
            //trNomineeCaption.Visible = true;
            //trgvNominees.Visible = true;
            //gvNominees.Visible = true;
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
                txtBankAdrLine1.Enabled = false;
                txtBankAdrLine2.Enabled = false;
                txtBankAdrLine3.Enabled = false;
                txtBankBranchCode.Enabled = false;
                txtBankAdrPinCode.Enabled = false;
                txtRTGSCode.Enabled = false;
                txtNEFTCode.Enabled = false;
                //txtBankAdrCity.Enabled = false;
               // ddlBankAdrCity.Enabled = false;
               // ddlBankAdrState.Enabled = false;
                RadioButton1.Enabled = false;
                rbtnYes.Enabled = false;
                txtMicr.Enabled = false;
                txtIfsc.Enabled = false;
                txtBankCity.Enabled = false;
                //ddlBankAdrCountry.Enabled = false;
                chk_Ismain.Enabled = false;
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
                txtBankBranchCode.Enabled = true;
                txtBankAdrPinCode.Enabled = true;
                txtNEFTCode.Enabled = true;
                txtRTGSCode.Enabled = true;
                //txtBankAdrCity.Enabled = true;
        //sr    ddlBankAdrState.Enabled = true;
                RadioButton1.Enabled = true;
                rbtnYes.Enabled = true;
                txtMicr.Enabled = true;
                txtIfsc.Enabled = true;
                txtBankCity.Enabled = true;
         //sr     ddlBankAdrCity.Enabled = true;
         //sr     ddlBankAdrCountry.Enabled = true;
                chk_Ismain.Enabled = true;

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

        protected void RadioButton1o_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void gvJointHolders_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    DataTable dt = new DataTable();
            //    if (strVisibility == "Edit")
            //    {
            //        gvJointHolders.MasterTableView.Columns[0].Visible = true;
            //        dt = (DataTable)ViewState["dtJH"];
            //        CheckBox chkBox = e.Item.FindControl("chkId") as CheckBox;

            //        int selectedRow = e.Item.ItemIndex + 1;
            //        if (dt != null)
            //        {
            //            foreach (DataRow dr in dt.Rows)
            //            {
            //                if (gvJointHolders.MasterTableView.DataKeyValues[selectedRow - 1]["AssociationId"].ToString() == dr["AssociationId"].ToString())
            //                    chkBox.Checked = true;

            //            }
            //        }

            //        //ViewState["dtJH"] = null;
            //    }
            //    else if (strVisibility == "")
            //    {
            //        //gvJointHolders.MasterTableView.Columns[0].Visible = false;
            //    }
            //}
        }

        protected void gvNominees_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //    if (e.Item is GridDataItem)
            //    {
            //        DataTable dt = new DataTable();
            //        if (strVisibility == "Edit")
            //        {
            //            gvNominees.MasterTableView.Columns[0].Visible = true;
            //            dt = (DataTable)ViewState["dtNOM"];
            //            CheckBox chkBoxnom = e.Item.FindControl("chkId0") as CheckBox;

            //            int selectedRow = e.Item.ItemIndex ;
            //            //if (dt != null)
            //            //{
            //            //    DataRow[] drNomineeDt;
            //            //    foreach (GridDataItem item in gvNominees.Items)
            //            //    {

            //            //        int assosiationId = int.Parse(gvNominees.MasterTableView.DataKeyValues[selectedRow - 1]["AssociationId"].ToString());
            //            //        drNomineeDt = dt.Select("AssociationId=" + assosiationId.ToString());
            //            //        selectedRow = selectedRow + 1;
            //            //        if (drNomineeDt.Count() > 0)
            //            //        {
            //            //            chkBoxnom.Checked = true;
            //            //        }


            //                   // if(gvNominees.MasterTableView.DataKeyValues[selectedRow-1]["AssociationId"].ToString()==dr["AssociationId"].ToString())
            //                    //if (gvNominees.MasterTableView.DataKeyValues[selectedRow - 1]["AssociationId"].ToString() == dr["AssociationId"].ToString())
            //                     //   chkBoxnom.Checked = true;


            //            //    }
            //            //}

            //            //ViewState["dtNOM"] = null;
            //        }
            //        else if (strVisibility == "")
            //        {
            //            //gvJointHolders.MasterTableView.Columns[0].Visible = false;
            //        }
            //    }
            //}
        }
    }
}