using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WealthERP.Base;
using BoCommon;
using System.Configuration;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoCustomerProfiling;
using VoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
//using System.Threading;
//using System.Globalization;


namespace WealthERP.CustomerPortfolio
{
    public partial class CustomerAccountAdd : System.Web.UI.UserControl
    {
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();

        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        List<InsuranceULIPVo> insuranceUlipList = new List<InsuranceULIPVo>();
        AssetBo assetBo = new AssetBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        InsuranceBo insuranceBo = new InsuranceBo();
        DataSet dsAssetCategories;
        DataSet dsCustomerAssociates;
        DataSet dsAssetSubCategories;
        DataTable dtCustomerAssociatesRaw = new DataTable();
        DataTable dtCustomerAssociates = new DataTable();
        DataTable dtModeOfHolding;
        DataRow drCustomerAssociates;
        int accountId;

        string path;
        static int portfolioId;
        string group;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                insuranceUlipList = (List<InsuranceULIPVo>)Session["ulipList"];
                group = Session["action"].ToString().Trim();
                if(portfolioId==0 || portfolioId == null)
                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                this.Page.Culture = "en-GB";
                if (!IsPostBack)
                {
                    BindPortfolioDropDown();
                    SetFields();
                    LoadModeOfHolding(path);
                    LoadNominees();
                    ddlModeOfHolding.SelectedValue = "SI";
                    ddlModeOfHolding.Enabled = false;
                    if (group == "PG")
                    {
                        lblAssetGroupName.Text = "Pension And Gratuites";
                        lblAccountNum.Text = "Account Number:";
                        LoadPGContent();

                    }
                    else if (group == "CS")
                    {
                        lblAssetGroupName.Text = "Cash And Savings";
                        lblAccountNum.Text = "Account Number:";
                        lblAccountSource.Text = "Account with Bank";
                        LoadCategory();
                        LoadCSContent();
                    }
                    else if (group == "IN")
                    {
                        lblAssetGroupName.Text = "Insurance";
                        lblAccountNum.Text = "Policy Number:";
                        LoadInsuranceContent();
                    }
                    else if (group == "PR")
                    {
                        lblAssetGroupName.Text = "Property";
                        lblAccountNum.Text = "Asset Identifier:";
                        LoadPropContent();
                    }
                    else if (group == "GS")
                    {
                        lblAssetGroupName.Text = "Government Savings";
                        lblAccountNum.Text = "Account Number:";
                        LoadGovtSavingsContent();
                    }
                    else if (group == "FI")
                    {
                        lblAssetGroupName.Text = "Fixed Income";
                        LoadFixedIncomeContent();
                    }
                    else
                    {
                        gvJointHoldersList.Visible = false;
                        lblJointHolders.Visible = false;
                        gvNominees.Visible = false;
                        lblNominees.Visible = false;

                        lblCategory.Visible = false;
                        ddlCategory.Visible = false;
                        lblAccountNum.Visible = false;
                        txtAccountNumber.Visible = false;
                        lblAccountSource.Visible = false;
                    }
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
                FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:Page_Load()");
                object[] objects = new object[6];
                objects[0] = path;
                objects[1] = insuranceUlipList;
                objects[2] = customerVo;
                objects[3] = userVo;
                objects[4] = group;
                objects[5] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        private void BindPortfolioDropDown()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();
            //ddlPortfolio.Items.Insert(0, "Select the Portfolio");


            ddlPortfolio.SelectedValue = portfolioId.ToString();
        }

        private void LoadModeOfHolding(string path)
        {
            try
            {
                dtModeOfHolding = XMLBo.GetModeOfHolding(path);
                ddlModeOfHolding.DataSource = dtModeOfHolding;
                ddlModeOfHolding.DataTextField = "ModeOfHolding";
                ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
                ddlModeOfHolding.DataBind();
                ddlModeOfHolding.Items.Insert(0, new ListItem("Select Mode of Holding", "Select Mode of Holding"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:LoadModeOfHolding()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {

            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
        }

        public void SetFields()
        {
            trAccountNum.Visible = false;
            //trAccountOpeningDate.Visible = false;
            trAccountSource.Visible = false;
            trAssetGroup.Visible = false;
            trCategory.Visible = false;
            trJoingHolding.Visible = false;
            trJoinHolders.Visible = false;
            trModeOfHolding.Visible = false;
            trSubcategory.Visible = false;
            trError.Visible = false;
        }

        public void LoadInsuranceContent()
        {
            try
            {
                LoadCategory();
                if (insuranceUlipList != null)
                {
                    ddlCategory.SelectedValue = "ULIPS";
                }

                trCategory.Visible = true;
                trAccountNum.Visible = true;
                trSubcategory.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:LoadInsuranceContent()");
                object[] objects = new object[1];
                objects[0] = insuranceUlipList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        public void LoadCSContent()
        {

            trCategory.Visible = true;
            if (ddlCategory.SelectedItem.Value.ToString() == "CSCA")
            {
                trAccountNum.Visible = true;
                trAccountSource.Visible = true;
                trModeOfHolding.Visible = true;
                trJoingHolding.Visible = true;
            }
            else if (ddlCategory.SelectedItem.Value.ToString() == "CSLA")
            {
                trModeOfHolding.Visible = true;
                trJoingHolding.Visible = true;
            }
            else if (ddlCategory.SelectedItem.Value.ToString() == "CSSA")
            {
                trAccountNum.Visible = true;
                trAccountSource.Visible = true;
                trModeOfHolding.Visible = true;
                trJoingHolding.Visible = true;
                //trAccountOpeningDate.Visible = true;
            }
            else
            {
                // Display Invalid Selection
            }


        }

        public void LoadPGContent()
        {
            LoadCategory();

            trCategory.Visible = true;
            trAccountNum.Visible = true;
            trAccountSource.Visible = true;
            trModeOfHolding.Visible = true;
            //trAccountOpeningDate.Visible = true;
            trJoingHolding.Visible = true;
            trSubcategory.Visible = false;

        }

        public void LoadPropContent()
        {
            LoadCategory();

            trCategory.Visible = true;
            trSubcategory.Visible = true;
            trAccountNum.Visible = true;
            trModeOfHolding.Visible = true;
            //trAccountOpeningDate.Visible = true;
            trJoingHolding.Visible = true;
        }

        public void LoadGovtSavingsContent()
        {
            LoadCategory();

            trCategory.Visible = true;
            trAccountNum.Visible = true;
            trJoingHolding.Visible = true;
            trModeOfHolding.Visible = true;
            trAccountSource.Visible = true;
            //trAccountOpeningDate.Visible = true;
            trSubcategory.Visible = false;
        }

        public void LoadFixedIncomeContent()
        {
            LoadCategory();

            trCategory.Visible = true;
            trAccountNum.Visible = true;
            trJoingHolding.Visible = true;
            trModeOfHolding.Visible = true;
            trAccountSource.Visible = true;
            trSubcategory.Visible = false;
        }
        
        public void LoadNominees()
        {
            try
            {
                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];

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
                    //trNomineeCaption.Visible = true;
                    trNominees.Visible = true;
                }
                else
                {
                    trNoNominee.Visible = true;
                    //trNomineeCaption.Visible = false;
                    trNominees.Visible = false;
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool blResult = true;
            try
            {
                int i = 0;
                
                if (group == "PG")
                {
                    customerAccountsVo.CustomerId = customerVo.CustomerId;
                    customerAccountsVo.PortfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
                    customerAccountsVo.AccountNum = txtAccountNumber.Text;
                    customerAccountsVo.AssetClass = group;
                    customerAccountsVo.AssetCategory = ddlCategory.SelectedItem.Value.ToString().Trim();
                    customerAccountsVo.AccountSource = txtAccountSource.Text.ToString();
                    //if (txtAccountOpeningDate.Text != string.Empty)
                    //    customerAccountsVo.AccountOpeningDate = DateTime.Parse(txtAccountOpeningDate.Text.Trim());
                    customerAccountsVo.ModeOfHolding = ddlModeOfHolding.SelectedItem.Value.ToString().Trim();
                    if (rbtnNo.Checked)
                    {
                        customerAccountsVo.IsJointHolding = 0;
                    }
                    if (rbtnYes.Checked)
                    {
                        customerAccountsVo.IsJointHolding = 1;
                    }

                accountId = customerAccountBo.CreateCustomerPensionGratuitiesAccount(customerAccountsVo, userVo.UserId);
                    customerAccountAssociationVo.AccountId = accountId;

                    if (this.gvJointHoldersList.Rows.Count > 0)
                    {
                        foreach (GridViewRow gvr in this.gvJointHoldersList.Rows)
                        {
                            if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                            {
                                i++;
                                customerAccountAssociationVo.AssociationId = int.Parse(gvJointHoldersList.DataKeys[gvr.RowIndex].Values[1].ToString());
                                customerAccountAssociationVo.AssociationType = "Joint Holder";
                                customerAccountBo.CreatePensionGratuitiesAccountAssociation(customerAccountAssociationVo, userVo.UserId);
                            }
                        }
                    }
                    else
                    {
                        i = -1;
                    }

                    if (rbtnYes.Checked)
                    {   // If Mode of Holding is joint, then make selection of joint holders 
                        // mandatory according to sujith and Neha

                        if (i == 0)
                        {
                            trError.Visible = true;
                            lblError.Text = "Please select a Joint Holder";
                            blResult = false;
                        }
                        else
                        {
                            trError.Visible = false;
                        }
                    }

                    if (blResult)
                    {
                        Session["customerAccountVo"] = customerAccountBo.GetCustomerPensionAndGratuitiesAccount(accountId);
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PensionAndGratuities','?action=entry');", true);
                    }

                }

                else if (group == "CS")
                {
                    customerAccountsVo.CustomerId = customerVo.CustomerId;
                    customerAccountsVo.PortfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString()); ;
                    customerAccountsVo.AssetClass = group;
                    customerAccountsVo.AssetCategory = ddlCategory.SelectedValue.Trim();
                    customerAccountsVo.AssetCategoryName = ddlCategory.SelectedItem.Text.ToString().Trim();
                    customerAccountsVo.AccountNum = txtAccountNumber.Text;
                    customerAccountsVo.ModeOfHolding = ddlModeOfHolding.SelectedValue.ToString().Trim();

                    if (ddlCategory.SelectedValue == "CSCA")
                    {
                        customerAccountsVo.BankName = txtAccountSource.Text.Trim();
                    }
                    else if (ddlCategory.SelectedValue == "CSSA")
                    {
                        customerAccountsVo.BankName = txtAccountSource.Text.Trim();
                        //if (txtAccountOpeningDate.Text != "")
                        //    customerAccountsVo.AccountOpeningDate = DateTime.Parse(txtAccountOpeningDate.Text.Trim());
                        //else
                        //    customerAccountsVo.AccountOpeningDate = DateTime.MinValue;
                    }
                    else if (ddlCategory.SelectedValue == "CSLA")
                    {

                    }
                    else
                    {
                        // Display Invalid Selection
                    }

                    if (rbtnNo.Checked)
                    {
                        customerAccountsVo.IsJointHolding = 0;
                    }
                    if (rbtnYes.Checked)
                    {
                        customerAccountsVo.IsJointHolding = 1;
                    }
                    accountId = customerAccountBo.CreateCustomerCashSavingsAccount(customerAccountsVo, userVo.UserId);
                    customerAccountAssociationVo.AccountId = accountId;

                    if (this.gvJointHoldersList.Rows.Count > 0)
                    {
                        foreach (GridViewRow gvr in this.gvJointHoldersList.Rows)
                        {
                            if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                            {
                                i++;
                                customerAccountAssociationVo.AssociationId = int.Parse(gvJointHoldersList.DataKeys[gvr.RowIndex].Values[1].ToString());
                                customerAccountAssociationVo.AssociationType = "Joint Holder";
                                customerAccountBo.CreateCashSavingsAccountAssociation(customerAccountAssociationVo, userVo.UserId);
                            }
                        }
                    }
                    else
                    {
                        i = -1;
                    }
                    //foreach (GridViewRow gvr in this.gvNominees.Rows)
                    //{
                    //    if (((CheckBox)gvr.FindControl("chkId0")).Checked == true)
                    //    {
                    //        i++;
                    //        customerAccountAssociationVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[1].ToString());
                    //        customerAccountAssociationVo.AssociationType = "Nominee";
                    //        customerAccountBo.CreateCashSavingsAccountAssociation(customerAccountAssociationVo, userVo.UserId);//change after making all classes
                    //    }
                    //}

                    if (rbtnYes.Checked)
                    {
                        if (i == 0)
                        {
                            trError.Visible = true;
                            lblError.Text = "Please select a Joint Holder";
                            blResult = false;
                        }
                        else
                        {
                            trError.Visible = false;
                        }
                    }

                    if (blResult)
                    {
                        Session["customerAccountVo"] = customerAccountBo.GetCashAndSavingsAccount(accountId);
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioCashSavingsEntry','none');", true);
                    }
                }
                if (group == "IN")
                {
                    customerAccountsVo.AccountNum = txtAccountNumber.Text;
                    customerAccountsVo.CustomerId = customerVo.CustomerId;
                    customerAccountsVo.PortfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString()); ;
                    customerAccountsVo.PolicyNum = txtAccountNumber.Text;
                    customerAccountsVo.AssetCategory = ddlCategory.SelectedItem.Value.ToString();
                    customerAccountsVo.AssetClass = group;
                    accountId = customerAccountBo.CreateCustomerInsuranceAccount(customerAccountsVo, userVo.UserId);

                    Session["customerAccountVo"] = customerAccountBo.GetCustomerInsuranceAccount(accountId);

                    customerAccountAssociationVo.AccountId = accountId;
                    customerAccountAssociationVo.CustomerId = customerVo.CustomerId;

                    //foreach (GridViewRow gvr in this.gvNominees.Rows)
                    //{
                    //    if (((CheckBox)gvr.FindControl("chkId0")).Checked == true)
                    //    {
                    //        i++;
                    //        customerAccountAssociationVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[1].ToString());
                    //        customerAccountAssociationVo.AssociationType = "Nominee";
                    //        customerAccountBo.CreateInsuranceAccountAssociation(customerAccountAssociationVo, userVo.UserId);//change after making all classes
                    //    }
                    //}

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','?action=entry');", true);

                }
                else if (group == "PR")
                {
                    customerAccountsVo.AssetClass = group;
                    customerAccountsVo.AccountNum = txtAccountNumber.Text;
                    customerAccountsVo.AssetCategory = ddlCategory.SelectedItem.Value.ToString();
                    customerAccountsVo.AssetSubCategory = ddlSubCategory.SelectedItem.Value.ToString();
                    customerAccountsVo.CustomerId = customerVo.CustomerId;
                    customerAccountsVo.PortfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString()); ;
                    if (rbtnNo.Checked)
                    {
                        customerAccountsVo.IsJointHolding = 0;
                    }
                    if (rbtnYes.Checked)
                    {
                        customerAccountsVo.IsJointHolding = 1;
                    }
                    customerAccountsVo.ModeOfHolding = ddlModeOfHolding.SelectedItem.Value.ToString();

                    //if (txtAccountOpeningDate.Text != "")
                    //    customerAccountsVo.AccountOpeningDate = DateTime.Parse(txtAccountOpeningDate.Text.Trim());
                    //else
                    //    customerAccountsVo.AccountOpeningDate = DateTime.MinValue;

                    accountId = customerAccountBo.CreateCustomerPropertyAccount(customerAccountsVo, userVo.UserId);
                    customerAccountsVo.AccountId = accountId;

                    customerAccountAssociationVo.AccountId = accountId;
                    customerAccountAssociationVo.CustomerId = customerVo.CustomerId;

                    if (this.gvJointHoldersList.Rows.Count > 0)
                    {
                        foreach (GridViewRow gvr in this.gvJointHoldersList.Rows)
                        {
                            if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                            {
                                i++;
                                customerAccountAssociationVo.AssociationId = int.Parse(gvJointHoldersList.DataKeys[gvr.RowIndex].Values[1].ToString());
                                customerAccountAssociationVo.AssociationType = "Joint Holder";
                                customerAccountBo.CreatePropertyAccountAssociation(customerAccountAssociationVo, userVo.UserId);
                            }
                        }
                    }
                    else
                    {
                        i = -1;
                    }
                    //foreach (GridViewRow gvr in this.gvNominees.Rows)
                    //{
                    //    if (((CheckBox)gvr.FindControl("chkId0")).Checked == true)
                    //    {
                    //        i++;
                    //        customerAccountAssociationVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[1].ToString());
                    //        customerAccountAssociationVo.AssociationType = "Nominee";
                    //        customerAccountBo.CreatePropertyAccountAssociation(customerAccountAssociationVo, userVo.UserId);//change after making all classes
                    //    }
                    //}

                    if (rbtnYes.Checked)
                    {
                        if (i == 0)
                        {
                            trError.Visible = true;
                            lblError.Text = "Please select a Joint Holder";
                            blResult = false;
                        }
                        else
                        {
                            trError.Visible = false;
                        }
                    }

                    if (blResult)
                    {
                        Session["customerAccountVo"] = customerAccountsVo;
                        string url = Request.UrlReferrer.ToString();
                        if (url.EndsWith("TestMaster.aspx"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopUpScript", "showpop();", true);
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioPropertyEntry','?action=entry');", true);
                        }

                    }
                }

                else if (group == "GS")
                {
                    customerAccountsVo.AssetClass = group;
                    customerAccountsVo.AccountNum = txtAccountNumber.Text;
                    customerAccountsVo.AssetCategory = ddlCategory.SelectedItem.Value.ToString();
                    customerAccountsVo.CustomerId = customerVo.CustomerId;
                    customerAccountsVo.PortfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString()); ;
                    if (rbtnNo.Checked)
                    {
                        customerAccountsVo.IsJointHolding = 0;
                    }
                    if (rbtnYes.Checked)
                    {
                        customerAccountsVo.IsJointHolding = 1;
                    }
                    customerAccountsVo.ModeOfHolding = ddlModeOfHolding.SelectedItem.Value.ToString();
                    customerAccountsVo.AccountSource = txtAccountSource.Text;
                    //if (txtAccountOpeningDate != null && txtAccountOpeningDate.Text.Trim() != string.Empty)
                    //    customerAccountsVo.AccountOpeningDate = DateTime.Parse(txtAccountOpeningDate.Text.Trim());
                    accountId = customerAccountBo.CreateCustomerGovtSavingAccount(customerAccountsVo, userVo.UserId);
                    customerAccountsVo.AccountId = accountId;

                    customerAccountAssociationVo.AccountId = accountId;
                    customerAccountAssociationVo.CustomerId = customerVo.CustomerId;

                    if (this.gvJointHoldersList.Rows.Count > 0)
                    {
                        foreach (GridViewRow gvr in this.gvJointHoldersList.Rows)
                        {
                            if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                            {
                                i++;
                                customerAccountAssociationVo.AssociationId = int.Parse(gvJointHoldersList.DataKeys[gvr.RowIndex].Values[1].ToString());
                                customerAccountAssociationVo.AssociationType = "Joint Holder";
                                customerAccountBo.CreateGovtSavingsAccountAssociation(customerAccountAssociationVo, userVo.UserId);
                            }
                        }
                    }
                    else
                    {
                        i = -1;
                    }
                    //foreach (GridViewRow gvr in this.gvNominees.Rows)
                    //{
                    //    if (((CheckBox)gvr.FindControl("chkId0")).Checked == true)
                    //    {
                    //        i++;
                    //        customerAccountAssociationVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[1].ToString());
                    //        customerAccountAssociationVo.AssociationType = "Nominee";
                    //        customerAccountBo.CreateGovtSavingsAccountAssociation(customerAccountAssociationVo, userVo.UserId);//change after making all classes
                    //    }
                    //}

                    if (rbtnYes.Checked)
                    {
                        if (i == 0)
                        {
                            trError.Visible = true;
                            lblError.Text = "Please select a Joint Holder";
                            blResult = false;
                        }
                        else
                        {
                            trError.Visible = false;
                        }
                    }

                    if (blResult)
                    {
                        Session["customerAccountVo"] = customerAccountsVo;
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioGovtSavingsEntry','none');", true);
                    }
                }
                else if (group == "FI")
                {
                    customerAccountsVo.AssetClass = group;
                    customerAccountsVo.AccountNum = txtAccountNumber.Text;
                    customerAccountsVo.AssetCategory = ddlCategory.SelectedItem.Value.ToString();
                    customerAccountsVo.CustomerId = customerVo.CustomerId;
                    customerAccountsVo.PortfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString()); ;
                    if (rbtnNo.Checked)
                    {
                        customerAccountsVo.IsJointHolding = 0;
                    }
                    if (rbtnYes.Checked)
                    {
                        customerAccountsVo.IsJointHolding = 1;
                    }
                    customerAccountsVo.ModeOfHolding = ddlModeOfHolding.SelectedItem.Value.ToString();
                    customerAccountsVo.AccountSource = txtAccountSource.Text;
                    accountId = customerAccountBo.CreateCustomerFixedIncomeAccount(customerAccountsVo, userVo.UserId);

                    customerAccountsVo.AccountId = accountId;
                    customerAccountAssociationVo.AccountId = accountId;
                    customerAccountAssociationVo.CustomerId = customerVo.CustomerId;

                    if (this.gvJointHoldersList.Rows.Count > 0)
                    {
                        foreach (GridViewRow gvr in this.gvJointHoldersList.Rows)
                        {
                            if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                            {
                                i++;
                                customerAccountAssociationVo.AssociationId = int.Parse(gvJointHoldersList.DataKeys[gvr.RowIndex].Values[1].ToString());
                                customerAccountAssociationVo.AssociationType = "Joint Holder";
                                customerAccountBo.CreateFixedIncomeAccountAssociation(customerAccountAssociationVo, userVo.UserId);
                            }

                        }
                    }
                    else
                    {
                        i = -1;
                    }
                    //foreach (GridViewRow gvr in this.gvNominees.Rows)
                    //{
                    //    if (((CheckBox)gvr.FindControl("chkId0")).Checked == true)
                    //    {
                    //        i++;
                    //        customerAccountAssociationVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[1].ToString());
                    //        customerAccountAssociationVo.AssociationType = "Nominee";
                    //        customerAccountBo.CreateFixedIncomeAccountAssociation(customerAccountAssociationVo, userVo.UserId);//change after making all classes
                    //    }
                    //}

                    if (rbtnYes.Checked)
                    {
                        if (i == 0)
                        {
                            trError.Visible = true;
                            lblError.Text = "Please select a Joint Holder";
                            blResult = false;
                        }
                        else
                        {
                            trError.Visible = false;
                        }
                    }

                    if (blResult)
                    {
                        Session["customerAccountVo"] = customerAccountsVo;
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioFixedIncomeEntry','none');", true);
                    }
                }
                else if (group == "IN")
                {
                    customerAccountsVo.PolicyNum = txtAccountNumber.Text;
                    accountId = customerAccountBo.CreateCustomerInsuranceAccount(customerAccountsVo, userVo.UserId);

                    customerAccountAssociationVo.AccountId = accountId;
                    customerAccountAssociationVo.CustomerId = customerVo.CustomerId;
                    customerAccountsVo.PortfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString()); ;

                    if (this.gvJointHoldersList.Rows.Count > 0)
                    {
                        foreach (GridViewRow gvr in this.gvJointHoldersList.Rows)
                        {
                            if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                            {
                                i++;
                                customerAccountAssociationVo.AssociationId = int.Parse(gvJointHoldersList.DataKeys[gvr.RowIndex].Values[1].ToString());
                                customerAccountAssociationVo.AssociationType = "Joint Holder";
                                customerAccountBo.CreateInsuranceAccountAssociation(customerAccountAssociationVo, userVo.UserId);//change after making all classes
                            }
                        }
                    }
                    else
                    {
                        i = -1;
                    }
                    //foreach (GridViewRow gvr in this.gvNominees.Rows)
                    //{
                    //    if (((CheckBox)gvr.FindControl("chkId0")).Checked == true)
                    //    {
                    //        i++;
                    //        customerAccountAssociationVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[1].ToString());
                    //        customerAccountAssociationVo.AssociationType = "Nominee";
                    //        customerAccountBo.CreateInsuranceAccountAssociation(customerAccountAssociationVo, userVo.UserId);//change after making all classes
                    //    }
                    //}

                    if (rbtnYes.Checked)
                    {
                        if (i == 0)
                        {
                            trError.Visible = true;
                            lblError.Text = "Please select a Joint Holder";
                            blResult = false;
                        }
                        else
                        {
                            trError.Visible = false;
                        }
                    }

                    if (blResult)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','none');", true);
                    }
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
                FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:btnSubmit_Click()");
                object[] objects = new object[6];
                objects[0] = path;
                objects[1] = customerAccountAssociationVo;
                objects[2] = accountId;
                objects[3] = customerAccountsVo;
                objects[4] = group;
                objects[5] = blResult;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCategory.SelectedIndex != 0)
                {
                    if (group == "PR")
                    {
                        dsAssetSubCategories = assetBo.GetAssetInstrumentSubCategory("Prop", ddlCategory.SelectedValue.ToString());
                        ddlSubCategory.DataSource = dsAssetSubCategories.Tables[0];
                        ddlSubCategory.DataTextField = "PAISC_AssetInstrumentSubCategoryName";
                        ddlSubCategory.DataValueField = "PAISC_AssetInstrumentSubCategoryCode";
                        ddlSubCategory.DataBind();
                        ddlSubCategory.Items.Insert(0, new ListItem("Select a Sub-Category", "Select a Sub-Category"));
                        if (ddlSubCategory.Items.Count == 2) //If there is only one Item , then set the first item as default.
                            ddlSubCategory.SelectedIndex = 1;
                    }
                    else if (group == "CS")
                    {

                        SetFields();
                        LoadCSContent();
                    }
                    else if (group == "FI")
                    {
                        switch (ddlCategory.SelectedValue.ToString())
                        {
                            case "FICB":
                                {
                                    lblAccountNum.Text = "Certificate Number";
                                    trAccountSource.Visible = false;
                                    break;
                                }
                            case "FICD":
                                {
                                    lblAccountNum.Text = "Certificate Number";
                                    trAccountSource.Visible = false;
                                    break;
                                }
                            case "FIDB":
                                {
                                    lblAccountNum.Text = "Certificate Number";
                                    trAccountSource.Visible = false;
                                    break;
                                }
                            case "FIFD":
                                {
                                    lblAccountNum.Text = "Certificate Number";
                                    trAccountSource.Visible = false;
                                    break;
                                }
                            case "FIGS":
                                {
                                    lblAccountNum.Text = "Certificate Number";
                                    trAccountSource.Visible = false;
                                    break;
                                }
                            case "FIIB":
                                {
                                    lblAccountNum.Text = "Certificate Number";
                                    trAccountSource.Visible = true;
                                    break;
                                }
                            case "FIRD":
                                {
                                    lblAccountNum.Text = "Account Identifier";
                                    trAccountSource.Visible = false;
                                    break;
                                }
                            case "FISD":
                                {
                                    lblAccountNum.Text = "Certificate Number";
                                    trAccountSource.Visible = false;
                                    break;
                                }
                            case "FITB":
                                {
                                    lblAccountNum.Text = "Certificate Number";
                                    trAccountSource.Visible = true;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }
                    else
                    { }
                }
                else
                {
                    ddlSubCategory.Items.Clear();
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
                FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:ddlCategory_SelectedIndexChanged()");
                object[] objects = new object[1];
                objects[0] = group;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void rbtnYes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnYes.Checked)
                {
                    ddlModeOfHolding.Enabled = true;

                    dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                    dtCustomerAssociates.Rows.Clear();
                    dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];

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
                        drCustomerAssociates[3] = dr["XR_RelationshipCode"].ToString();
                        dtCustomerAssociates.Rows.Add(drCustomerAssociates);
                    }

                    if (dtCustomerAssociates.Rows.Count > 0)
                    {
                        trNoJointHolders.Visible = false;
                        trJoinHolders.Visible = true;
                        trJointHolderGrid.Visible = true;
                        gvJointHoldersList.DataSource = dtCustomerAssociates;
                        gvJointHoldersList.DataBind();
                        gvJointHoldersList.Visible = true;
                    }
                    else
                    {
                        trNoJointHolders.Visible = true;
                        trJoinHolders.Visible = false;
                        trJointHolderGrid.Visible = false;
                    }
                    ddlModeOfHolding.SelectedIndex = 0;
                }
                else
                {
                    ddlModeOfHolding.SelectedValue = "SI";
                    ddlModeOfHolding.Enabled = false;
                    trJoinHolders.Visible = false;
                    trJointHolderGrid.Visible = false;
                    trNoJointHolders.Visible = false;
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
                FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:rbtnYes_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void LoadCategory()
        {
            try
            {
                DataSet ds = assetBo.GetAssetInstrumentCategory(group); //Change to the respective GroupCode
                ddlCategory.DataSource = ds.Tables[0];
                ddlCategory.DataTextField = "PAIC_AssetInstrumentCategoryName";
                ddlCategory.DataValueField = "PAIC_AssetInstrumentCategoryCode";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select a Category", "Select a Category"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:LoadCategory()");
                object[] objects = new object[1];
                objects[0] = group;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlAssetGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
