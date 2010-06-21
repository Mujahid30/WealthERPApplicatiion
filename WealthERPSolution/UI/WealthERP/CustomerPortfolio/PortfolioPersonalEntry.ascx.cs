using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WealthERP.Base;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoCustomerProfiling;
using VoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioPersonalEntry : System.Web.UI.UserControl
    {
        PersonalBo personalBo = new PersonalBo();
        PersonalVo personalVo = new PersonalVo();
        AssetBo assetBo = new AssetBo();
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        DataSet dsAssetCategories;
        DataSet dsAssetSubCategories;
        static int portfolioId;
        string Manage = string.Empty;
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];

                if (!IsPostBack)
                {
                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    BindPortfolioDropDown();
                    trSubCategory.Visible = false;
                    dsAssetCategories = assetBo.GetAssetInstrumentCategory("PI"); //Change to the respective GroupCode
                    ddlCategory.DataSource = dsAssetCategories.Tables[0];
                    ddlCategory.DataTextField = "PAIC_AssetInstrumentCategoryName";
                    ddlCategory.DataValueField = "PAIC_AssetInstrumentCategoryCode";
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, new ListItem("Select a Category", "Select a Category"));

                    personalVo = (PersonalVo)Session["personalVo"];

                    if (Request.QueryString["action"] != null)
                        Manage = Request.QueryString["action"].ToString();

                    if (personalVo != null)
                    {
                        if (Manage == "edit")
                        {
                            SetControls("edit", personalVo);
                        }
                        else if (Manage == "view")
                        {

                            SetControls("view", personalVo);
                        }
                    }
                    else
                    {

                        SetControls("entry", personalVo);
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
                FunctionInfo.Add("Method", "PortfolioPersonalEntry.ascx:Page_Load()");
                object[] objects = new object[4];
                objects[0] = customerVo;
                objects[1] = portfolioId;
                objects[2] = userVo;
                objects[3] = personalVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
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
        private void BindPortfolioDropDown()
        {
            customerVo = (CustomerVo)Session["customerVo"];
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();

            ddlPortfolio.SelectedValue = portfolioId.ToString();

        }
        private void BindDropDowns(PersonalVo personalVo)
        {
            try
            {
                // Bind the Asset Categories
                ddlCategory.Items.Clear();
                dsAssetCategories = assetBo.GetAssetInstrumentCategory("PI");
                ddlCategory.DataSource = dsAssetCategories.Tables[0];
                ddlCategory.DataTextField = "PAIC_AssetInstrumentCategoryName".Trim();
                ddlCategory.DataValueField = "PAIC_AssetInstrumentCategoryCode".Trim();
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select a Category", "Select a Category"));
                ddlCategory.SelectedValue = personalVo.AssetCategoryCode.ToString().Trim();

                // Bind the Asset Sub-Categories based on the selected Categories
                ddlSubCategory.Items.Clear();
                dsAssetSubCategories = assetBo.GetAssetInstrumentSubCategory("PI", ddlCategory.SelectedValue.ToString()); //Change to the respective GroupCode
                ddlSubCategory.DataSource = dsAssetSubCategories.Tables[0];
                ddlSubCategory.DataTextField = "PAISC_AssetInstrumentSubCategoryName";
                ddlSubCategory.DataValueField = "PAISC_AssetInstrumentSubCategoryCode";
                ddlSubCategory.DataBind();
                ddlSubCategory.Items.Insert(0, new ListItem("Select a Sub-Category", "Select a Sub-Category"));
                ddlSubCategory.SelectedValue = personalVo.AssetSubCategoryCode.ToString().Trim();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioPersonalEntry.ascx:BindDropDowns()");
                object[] objects = new object[1];
                objects[0] = personalVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void SetControls(string action, PersonalVo personalVo)
        {
            try
            {
                if (action == "entry")
                {
                    // Clear all controls here
                    txtName.Text = "";
                    txtQuantity.Text = "";
                    txtPurchasePrice.Text = "";
                    txtPurchaseValue.Text = "";
                    txtCurrentPrice.Text = "";
                    txtCurrentValue.Text = "";
                    txtPurchaseDate.Text = "";
                    btnSubmit.Visible = true;
                    btnUpdate.Visible = false;
                    trEditLink.Visible = false;
                    lnkBack.Visible = false;
                    lblHeader.Visible = true;
                    Label1.Visible = false;
                    Label2.Visible = false;
                }
                else
                {
                    // Bind the Asset Details to respective controls
                    lnkBack.Visible = true;
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                    txtName.Text = personalVo.Name;
                    txtQuantity.Text = personalVo.Quantity.ToString();
                    txtPurchasePrice.Text = personalVo.PurchasePrice.ToString();
                    txtPurchaseValue.Text = personalVo.PurchaseValue.ToString();
                    txtCurrentPrice.Text = personalVo.CurrentPrice.ToString();
                    txtCurrentValue.Text = personalVo.CurrentValue.ToString();
                    if (personalVo.PurchaseDate != DateTime.MinValue)
                        txtPurchaseDate.Text = personalVo.PurchaseDate.ToShortDateString();
                    else
                        txtPurchaseDate.Text = "";

                    BindDropDowns(personalVo);

                    if (action == "view")
                    {
                        // Set Enable = false for each control
                        trSubCategory.Visible = true;
                        txtName.Enabled = false;
                        ddlCategory.Enabled = false;
                        ddlSubCategory.Enabled = false;
                        txtQuantity.Enabled = false;
                        txtPurchasePrice.Enabled = false;
                        txtPurchaseValue.Enabled = false;
                        txtCurrentPrice.Enabled = false;
                        txtCurrentValue.Enabled = false;
                        txtPurchaseDate.Enabled = false;
                        trEditLink.Visible = true;
                        btnUpdate.Visible = false;
                        lblHeader.Visible = false;
                        Label1.Visible = true;
                        Label2.Visible = false;
                    }
                    else if (action == "edit")
                    {
                        // Set Enable = true for each control
                        trSubCategory.Visible = true;
                        trEditLink.Visible = false;
                        txtName.Enabled = true;
                        ddlCategory.Enabled = true;
                        ddlSubCategory.Enabled = true;
                        txtQuantity.Enabled = true;
                        txtPurchasePrice.Enabled = true;
                        txtPurchaseValue.Enabled = true;
                        txtCurrentPrice.Enabled = true;
                        txtCurrentValue.Enabled = true;
                        txtPurchaseDate.Enabled = true;
                        btnUpdate.Visible = true;
                        lblHeader.Visible = false;
                        Label1.Visible = false;
                        Label2.Visible = true;
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
                FunctionInfo.Add("Method", "PortfolioPersonalEntry.ascx:SetControls()");
                object[] objects = new object[2];
                objects[0] = personalVo;
                objects[1] = action;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedIndex != 0)
            {
                trSubCategory.Visible = true;

                dsAssetSubCategories = assetBo.GetAssetInstrumentSubCategory("PI", ddlCategory.SelectedItem.Value.ToString()); //Change to the respective GroupCode
                ddlSubCategory.DataSource = dsAssetSubCategories.Tables[0];
                ddlSubCategory.DataTextField = "PAISC_AssetInstrumentSubCategoryName";
                ddlSubCategory.DataValueField = "PAISC_AssetInstrumentSubCategoryCode";
                ddlSubCategory.DataBind();
                ddlSubCategory.Items.Insert(0, new ListItem("Select a Sub-Category", "Select a Sub-Category"));
            }
            else
            {
                trSubCategory.Visible = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int personalId = 0;
            try
            {
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];

                personalVo.PortfolioId = portfolioId;
                personalVo.AssetGroupCode = "PI";
                personalVo.Name = txtName.Text.ToString();
                personalVo.AssetCategoryCode = ddlCategory.SelectedItem.Value.ToString();
                personalVo.AssetSubCategoryCode = ddlSubCategory.SelectedItem.Value.ToString();
                if (txtQuantity.Text.ToString() != string.Empty)
                    personalVo.Quantity = float.Parse(txtQuantity.Text.ToString());
                else
                    personalVo.Quantity = 0;
                if (txtPurchaseDate.Text.Trim().ToString() != string.Empty)
                    personalVo.PurchaseDate = DateTime.Parse(txtPurchaseDate.Text.Trim());
                else
                    personalVo.PurchaseDate = DateTime.MinValue;
                personalVo.PurchasePrice = double.Parse(txtPurchasePrice.Text.ToString());
                personalVo.PurchaseValue = double.Parse(txtPurchaseValue.Text.ToString());
                if (txtCurrentPrice.Text.ToString() != string.Empty)
                    personalVo.CurrentPrice = double.Parse(txtCurrentPrice.Text.ToString());
                else
                    personalVo.CurrentPrice = 0;
                if (txtCurrentValue.Text.ToString() != string.Empty)
                    personalVo.CurrentValue = double.Parse(txtCurrentValue.Text.ToString());
                else
                    personalVo.CurrentValue = 0;

                personalId = personalBo.CreatePersonalPortfolio(personalVo, userVo.UserId);
                if (personalId != 0)
                {
                    if (Session["test"] != null)
                    {
                        Session["personalVo"] = personalBo.GetPersonalAsset(personalId);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LiabilitiesMaintenanceForm','none');", true);
                    }
                    else if (Session[SessionContents.LoanProcessTracking] != null)
                    {
                        Session["personalVo"] = personalBo.GetPersonalAsset(personalId);
                        Session["LoanProcessAction"] = "add";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('LoanProcessTracking','login');", true);
                    }
                    else
                    {
                       ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('PortfolioPersonal','none');", true);
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
                FunctionInfo.Add("Method", "PortfolioPersonalEntry.ascx:btnSubmit_Click()");
                object[] objects = new object[3];
                objects[0] = personalVo;
                objects[1] = userVo;
                objects[2] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }



        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        personalVo = (PersonalVo)Session["personalVo"];

        //        personalVo.AssetGroupCode = "PI";
        //        personalVo.Name = txtName.Text.ToString();
        //        personalVo.AssetCategoryCode = ddlCategory.SelectedItem.Value.ToString();
        //        personalVo.AssetSubCategoryCode = ddlSubCategory.SelectedItem.Value.ToString();
        //        personalVo.Quantity = int.Parse(txtQuantity.Text.ToString());
        //        personalVo.PurchaseDate = DateTime.Parse(txtPurchaseDate.Text.ToString());
        //        personalVo.PurchasePrice = int.Parse(txtPurchasePrice.Text.ToString());
        //        personalVo.PurchaseValue = int.Parse(txtPurchaseValue.Text.ToString());
        //        personalVo.CurrentPrice = int.Parse(txtCurrentPrice.Text.ToString());
        //        personalVo.CurrentValue = int.Parse(txtCurrentValue.Text.ToString());

        //        if (personalBo.UpdatePersonalPortfolio(personalVo, userVo.UserId))
        //        {
        //            // Do a RegisterClientScript here to redirect to the View Page
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioPersonal','none');", true);
        //        }
        //        else
        //        {
        //            // Show some error and display same page
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "PortfolioPersonalEntry.ascx:btnSubmit_Click()");
        //        object[] objects = new object[2];
        //        objects[0] = personalVo;
        //        objects[1] = userVo;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //}

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                personalVo = (PersonalVo)Session["personalVo"];
                SetControls("edit", personalVo);
                btnUpdate.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioPersonalEntry.ascx:lnkEdit_Click()");
                object[] objects = new object[1];
                objects[0] = personalVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void Page_Error(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('Userlogin');", true);
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
           ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "leftpane", "loadcontrol('PortfolioPersonal','none');", true);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                personalVo = (PersonalVo)Session["personalVo"];

                personalVo.AssetGroupCode = "PI";
                personalVo.Name = txtName.Text.ToString();
                personalVo.AssetCategoryCode = ddlCategory.SelectedItem.Value.ToString();
                personalVo.AssetSubCategoryCode = ddlSubCategory.SelectedItem.Value.ToString();
                personalVo.Quantity = float.Parse(txtQuantity.Text.ToString());
                if (txtPurchaseDate.Text.ToString() != string.Empty)
                    personalVo.PurchaseDate = DateTime.Parse(txtPurchaseDate.Text.ToString());
                else
                    personalVo.PurchaseDate = DateTime.MinValue;
                personalVo.PurchasePrice = double.Parse(txtPurchasePrice.Text.ToString());
                personalVo.PurchaseValue = double.Parse(txtPurchaseValue.Text.ToString());
                personalVo.CurrentPrice = double.Parse(txtCurrentPrice.Text.ToString());
                personalVo.CurrentValue = double.Parse(txtCurrentValue.Text.ToString());

                if (personalBo.UpdatePersonalPortfolio(personalVo, userVo.UserId))
                {
                    Session.Remove("personalVo");
                    // Do a RegisterClientScript here to redirect to the View Page
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioPersonal','none');", true);
                }
                else
                {
                    // Show some error and display same page
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
                FunctionInfo.Add("Method", "PortfolioPersonalEntry.ascx:btnSubmit_Click()");
                object[] objects = new object[2];
                objects[0] = personalVo;
                objects[1] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
      

    }
}