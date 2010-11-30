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
using VoUser;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;


namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioPropertyEntry : System.Web.UI.UserControl
    {
        PropertyVo propertyVo;
        PropertyBo propertyBo = new PropertyBo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        AssetBo assetBo = new AssetBo();

        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        DataTable dtMeasureCode;
        DataTable dtStates;
        DataSet dsInstrumentCategory;
        DataSet dsInstrumentSubCategory;

        string path;
        string AssetGroupCode = "PR";
        string Manage = string.Empty;

        Mode mode = Mode.Add;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                cvPurchaseDate1.ValueToCompare = DateTime.Now.ToShortDateString();
                setMode(); //Set Add/Edit/View Mode based on the query string parameter.

                if (!IsPostBack)
                {
                    userVo = (UserVo)Session["userVo"];
                    customerVo = (CustomerVo)Session["CustomerVo"];
                    customerAccountsVo = (CustomerAccountsVo)Session["customerAccountVo"];
                    propertyVo = (PropertyVo)Session["propertyVo"];

                    path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

                    if (Request.QueryString["action"] != null)
                        Manage = Request.QueryString["action"].ToString();

                    if (propertyVo != null)
                    {
                        if (mode == Mode.Edit)
                        {
                            lblHeader.Text = "Edit Property";
                            SetControls("edit", propertyVo, customerAccountsVo, path);
                        }
                        else if (mode == Mode.View)
                        {
                            lblHeader.Text = "View Property";
                            SetControls("view", propertyVo, customerAccountsVo, path);
                        }
                        else if (mode == Mode.Add)
                        {
                            lblHeader.Text = "Add Property";
                            SetControls("entry", propertyVo, customerAccountsVo, path);
                        }
                    }
                    else
                    {
                        lblHeader.Text = "Add Property";
                        SetControls("entry", propertyVo, customerAccountsVo, path);
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
                FunctionInfo.Add("Method", "PortfolioPropertyEntry.ascx:Page_Load()");
                object[] objects = new object[6];
                objects[0] = userVo;
                objects[1] = customerVo;
                objects[2] = customerAccountsVo;
                objects[3] = propertyVo;
                objects[4] = path;
                objects[5] = Manage;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void SetControls(string action, PropertyVo propertyVo, CustomerAccountsVo customerAccountsVo, string path)
        {
            try
            {

                txtAccountNum.Enabled = false;
                txtModeofHolding.Enabled = false;
                //txtPurchaseDate.Enabled = false;
                txtModeofHolding.Text = XMLBo.GetModeOfHoldingName(path, customerAccountsVo.ModeOfHolding.ToString());
                txtAccountNum.Text = customerAccountsVo.AccountNum.ToString();
                if (customerAccountsVo.AccountOpeningDate != DateTime.MinValue)
                {
                    txtPurchaseDate.Text = customerAccountsVo.AccountOpeningDate.ToShortDateString();
                }

                //txtPropertyName.Text = customerAccountsVo.
                // Joint Holders
                // Nominees

                // Bind Initial Drop Downs
                BindDropDowns(propertyVo, customerAccountsVo, path);
                BindInstrumentSubCategory(customerAccountsVo);
                ddlInstrumentCat.SelectedValue = customerAccountsVo.AssetCategory.ToString().Trim();
                ddlInstrumentSubCat.SelectedValue = customerAccountsVo.AssetSubCategory.Trim();

                if (action == "entry")
                {
                    // Clear all controls here
                    txtPropertyName.Text = "";
                    txtAdrLine1.Text = "";
                    txtAdrLine2.Text = "";
                    txtAdrLine3.Text = "";
                    txtPinCode.Text = "";
                    txtCity.Text = "";
                    txtPurchaseDate.Text = "";
                    txtPurchaseDate_CalendarExtender.Enabled = true;
                    txtPurchaseDate_TextBoxWatermarkExtender.Enabled = true;
                    //imgPurchaseDate.Visible = true;
                    //dvPurchaseDate.Visible = true;
                    txtQuantity.Text = "";
                    txtPurchasePrice.Text = "";
                    txtPurchaseValue.Text = "";
                    txtCurrentPrice.Text = "";
                    txtCurrentValue.Text = "";
                    txtSaleDate.Text = "";
                    txtSaleDate_CalendarExtender.Enabled = true;
                    txtSaleDate_TextBoxWatermarkExtender.Enabled = true;
                    //imgSaleDate.Visible = true;
                    //dvSaleDate.Visible = true;
                    txtSaleRate.Text = "";
                    txtSaleProceeds.Text = "";
                    txtRemarks.Text = "";

                    // Enable/Disable Controls
                    ddlInstrumentCat.Enabled = false;
                    ddlInstrumentSubCat.Enabled = false;

                    txtPropertyName.Enabled = true;
                    txtAdrLine1.Enabled = true;
                    txtAdrLine2.Enabled = true;
                    txtAdrLine3.Enabled = true;
                    txtPinCode.Enabled = true;
                    txtCity.Enabled = true;
                    txtPurchaseDate.Enabled = true;
                    txtQuantity.Enabled = true;
                    txtPurchasePrice.Enabled = true;
                    txtPurchaseValue.Enabled = true;
                    txtCurrentPrice.Enabled = true;
                    txtCurrentValue.Enabled = true;
                    txtSaleDate.Enabled = true;
                    txtSaleRate.Enabled = true;
                    txtSaleProceeds.Enabled = true;
                    txtRemarks.Enabled = true;

                    trSubmitButton.Visible = true;
                    btnSubmit.Text = "Submit";

                    
                }
                else
                {
                    ddlInstrumentCat.Enabled = false;
                    ddlInstrumentSubCat.Enabled = false;

                    // Set the Drop Downs to respective values 

                    ddlMeasureCode.SelectedValue = propertyVo.MeasureCode.Trim();
                    ddlState.SelectedValue = propertyVo.PropertyState.ToString();
                    // Bind values to the respective controls
                    txtPropertyName.Text = propertyVo.Name.ToString().Trim();
                    txtAdrLine1.Text = propertyVo.PropertyAdrLine1.ToString().Trim();
                    txtAdrLine2.Text = propertyVo.PropertyAdrLine2.ToString().Trim();
                    txtAdrLine3.Text = propertyVo.PropertyAdrLine3.ToString().Trim();
                    txtPinCode.Text = propertyVo.PropertyPinCode.ToString();
                    txtCity.Text = propertyVo.PropertyCity.ToString().Trim(); ;
                    if(propertyVo.PurchaseDate != DateTime.MinValue)
                        txtPurchaseDate.Text = propertyVo.PurchaseDate.ToShortDateString().Trim(); ;
                    txtQuantity.Text = propertyVo.Quantity.ToString().Trim();
                    txtPurchasePrice.Text = propertyVo.PurchasePrice.ToString();
                    txtPurchaseValue.Text = propertyVo.PurchaseValue.ToString();
                    txtCurrentPrice.Text = propertyVo.CurrentPrice.ToString();
                    txtCurrentValue.Text = propertyVo.CurrentValue.ToString();
                    if (propertyVo.SellDate != DateTime.MinValue)
                        txtSaleDate.Text = propertyVo.SellDate.ToShortDateString();
                    txtSaleRate.Text = propertyVo.SellPrice.ToString();
                    txtSaleProceeds.Text = propertyVo.SellValue.ToString();
                    if (propertyVo.Remark != null)
                    {
                        txtRemarks.Text = propertyVo.Remark.ToString().Trim();
                    }
                    else
                    {
                        txtRemarks.Text = "";
                    }

                   

                    EnableDisableControls(action);
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
                FunctionInfo.Add("Method", "PortfolioPropertyEntry.ascx:SetControls()");
                object[] objects = new object[4];

                objects[0] = action;
                objects[1] = propertyVo;
                objects[2] = customerAccountsVo;
                objects[3] = path;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void EnableDisableControls(string action)
        {

            if (action == "view")
            {
                // Set the Enabled Property of each control
                txtPropertyName.Enabled = false;
                txtAdrLine1.Enabled = false;
                txtAdrLine2.Enabled = false;
                txtAdrLine3.Enabled = false;
                txtPinCode.Enabled = false;
                txtCity.Enabled = false;
                txtPurchaseDate.Enabled = false;
                txtQuantity.Enabled = false;
                txtPurchasePrice.Enabled = false;
                txtPurchaseValue.Enabled = false;
                txtCurrentPrice.Enabled = false;
                txtCurrentValue.Enabled = false;
                txtSaleDate.Enabled = false;
                txtSaleRate.Enabled = false;
                txtSaleProceeds.Enabled = false;
                txtRemarks.Enabled = false;
                ddlState.Enabled = false;
                ddlCountry.Enabled = false;
                ddlMeasureCode.Enabled = false;
                //Label1.Visible = false;
                //Label2.Visible = true;

                txtPurchaseDate_CalendarExtender.Enabled = false;
                txtPurchaseDate_TextBoxWatermarkExtender.Enabled = false;
                txtSaleDate_CalendarExtender.Enabled = false;
                txtSaleDate_TextBoxWatermarkExtender.Enabled = false;

                divEditButton.Visible = true;
                trEditSpace.Visible = true;
                trSubmitButton.Visible = false;
                btnSubmit.Text = "";

            }
            else if (action == "edit")
            {
                // Set the Enabled Property of each control
                txtPropertyName.Enabled = true;
                txtAdrLine1.Enabled = true;
                txtAdrLine2.Enabled = true;
                txtAdrLine3.Enabled = true;
                txtPinCode.Enabled = true;
                txtCity.Enabled = true;
                txtPurchaseDate.Enabled = true;
                txtQuantity.Enabled = true;
                txtPurchasePrice.Enabled = true;
                txtPurchaseValue.Enabled = true;
                txtCurrentPrice.Enabled = true;
                txtCurrentValue.Enabled = true;
                txtSaleDate.Enabled = true;
                txtSaleRate.Enabled = true;
                txtSaleProceeds.Enabled = true;
                txtRemarks.Enabled = true;
                ddlState.Enabled = true;
                ddlCountry.Enabled = true;
                ddlMeasureCode.Enabled = true;
                //Label1.Visible = true;
                //Label2.Visible = false;

                txtPurchaseDate_CalendarExtender.Enabled = true;
                txtPurchaseDate_TextBoxWatermarkExtender.Enabled = false;
                txtSaleDate_CalendarExtender.Enabled = true;
                txtSaleDate_TextBoxWatermarkExtender.Enabled = false;

                divEditButton.Visible = false;
                trEditSpace.Visible = false;
                trSubmitButton.Visible = true;
                btnSubmit.Text = "Update";
            }
        }

        private void BindDropDowns(PropertyVo propertyVo, CustomerAccountsVo customerAccountsVo, string path)
        {
            try
            {
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                dtMeasureCode = XMLBo.GetMeasureCode(path, AssetGroupCode);
                ddlMeasureCode.DataSource = dtMeasureCode;
                ddlMeasureCode.DataTextField = "Measure";
                ddlMeasureCode.DataValueField = "MeasureCode";
                ddlMeasureCode.DataBind();
                ddlMeasureCode.Items.Insert(0, new ListItem("Select a Measure Code", "Select a Measure Code"));

                // Bind the Instrument Category for an Asset
                dsInstrumentCategory = assetBo.GetAssetInstrumentCategory(AssetGroupCode);
                ddlInstrumentCat.DataSource = dsInstrumentCategory.Tables[0];
                ddlInstrumentCat.DataTextField = "PAIC_AssetInstrumentCategoryName";
                ddlInstrumentCat.DataValueField = "PAIC_AssetInstrumentCategoryCode";
                ddlInstrumentCat.DataBind();
                ddlInstrumentCat.Items.Insert(0, new ListItem("Select an Instrument Category", "Select an Instrument Category"));

                ddlInstrumentSubCat.Items.Clear();


                // Bind State Drop Downs
                dtStates = XMLBo.GetStates(path);
                ddlState.DataSource = dtStates;
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "StateCode";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("Select a State", "Select a State"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioPropertyEntry.ascx:BindDropDowns()");
                object[] objects = new object[5];
                objects[0] = userVo;
                objects[1] = propertyVo;
                objects[2] = customerAccountsVo;
                objects[3] = path;
                objects[4] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void BindInstrumentSubCategory(CustomerAccountsVo custAccountsVo)
        {
            try
            {
                // Bind the Instrument Sub-Category for an Asset Instrument Category
                dsInstrumentSubCategory = assetBo.GetAssetInstrumentSubCategory(AssetGroupCode, custAccountsVo.AssetCategory.ToString().Trim());
                ddlInstrumentSubCat.DataSource = dsInstrumentSubCategory.Tables[0];
                ddlInstrumentSubCat.DataTextField = "PAISC_AssetInstrumentSubCategoryName";
                ddlInstrumentSubCat.DataValueField = "PAISC_AssetInstrumentSubCategoryCode";
                ddlInstrumentSubCat.DataBind();
                ddlInstrumentSubCat.Items.Insert(0, new ListItem("Select an Instrument Sub-Category", "Select an Instrument Sub-Category"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioPropertyEntry.ascx:BindInstrumentSubCategory()");
                object[] objects = new object[2];
                objects[0] = custAccountsVo;
                objects[1] = AssetGroupCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                customerAccountsVo = (CustomerAccountsVo)Session["customerAccountVo"];

                propertyVo = new PropertyVo();

                if (btnSubmit.Text == "Submit")
                {
                    propertyVo.AccountId = Int32.Parse(customerAccountsVo.AccountId.ToString());
                    propertyVo.AssetSubCategoryCode = customerAccountsVo.AssetSubCategory;
                    propertyVo.AssetCategoryCode = customerAccountsVo.AssetCategory;
                    propertyVo.AssetGroupCode = "PR"; //Replace with the actual group code
                    propertyVo.MeasureCode = ddlMeasureCode.SelectedItem.Value.ToString();
                    propertyVo.Name = txtPropertyName.Text.ToString();
                    propertyVo.PropertyAdrLine1 = txtAdrLine1.Text.ToString();
                    propertyVo.PropertyAdrLine2 = txtAdrLine2.Text.ToString();
                    propertyVo.PropertyAdrLine3 = txtAdrLine3.Text.ToString();
                    propertyVo.PropertyCity = txtCity.Text.ToString();
                    if (txtPinCode.Text != string.Empty)
                        propertyVo.PropertyPinCode = int.Parse(txtPinCode.Text.ToString());
                    propertyVo.PropertyState = ddlState.SelectedItem.Value.ToString();
                    propertyVo.PropertyCountry = ddlCountry.SelectedItem.Value.ToString();

                    if (txtPurchaseDate.Text != string.Empty)
                        propertyVo.PurchaseDate = DateTime.Parse(txtPurchaseDate.Text);

                    if (txtPurchasePrice.Text != string.Empty)
                        propertyVo.PurchasePrice = float.Parse(txtPurchasePrice.Text);

                    if (txtQuantity.Text != string.Empty)
                        propertyVo.Quantity = float.Parse(txtQuantity.Text.ToString());
                    if (txtPurchaseValue.Text != string.Empty)
                        propertyVo.PurchaseValue = float.Parse(txtPurchaseValue.Text.ToString());
                    if (txtCurrentPrice.Text != string.Empty)
                        propertyVo.CurrentPrice = float.Parse(txtCurrentPrice.Text.ToString());
                    if (txtCurrentValue.Text != string.Empty)
                        propertyVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                    if (txtSaleDate.Text != "")
                        propertyVo.SellDate = Convert.ToDateTime(txtSaleDate.Text);
                    if (txtSaleRate.Text != "")
                        propertyVo.SellPrice = float.Parse(txtSaleRate.Text.ToString());
                    if (txtSaleProceeds.Text != "")
                        propertyVo.SellValue = float.Parse(txtSaleProceeds.Text.ToString());
                    propertyVo.Remark = txtRemarks.Text.ToString().Trim();
                    string url = Request.UrlReferrer.ToString();


                    int PropertyId = 0;
                    PropertyId = propertyBo.CreatePropertyPortfolio(propertyVo, userVo.UserId);
                    if (PropertyId != 0)
                    {
                        if (Session["test"] != null )//&& Request.QueryString["retURL"] == "liabilities")
                        {
                            Session["propertyVo"] = propertyBo.GetPropertyAsset(PropertyId);
                            Session["propertyVo"] = propertyBo.GetPropertyAsset(PropertyId);
                            Session["test"] = null;

                            string queryString = "?prevPage=propertyEntry";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LiabilitiesMaintenanceForm','" + queryString + "');", true);
                        }
                        else if (Session[SessionContents.LoanProcessTracking] != null)
                        {
                            Session["propertyVo"] = propertyBo.GetPropertyAsset(PropertyId);
                            Session["LoanProcessAction"] = "add";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('LoanProcessTracking','login');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioProperty','none');", true);
                        }
                    }

                }
                else if (btnSubmit.Text == "Update")
                {
                    propertyVo = (PropertyVo)Session["propertyVo"];

                    propertyVo.MeasureCode = ddlMeasureCode.SelectedItem.Value.ToString();
                    propertyVo.Name = txtPropertyName.Text.ToString();
                    propertyVo.PropertyAdrLine1 = txtAdrLine1.Text.ToString();
                    propertyVo.PropertyAdrLine2 = txtAdrLine2.Text.ToString();
                    propertyVo.PropertyAdrLine3 = txtAdrLine3.Text.ToString();
                    propertyVo.PropertyCity = txtCity.Text.ToString();
                    propertyVo.PropertyPinCode = int.Parse(txtPinCode.Text.ToString());
                    propertyVo.PropertyState = ddlState.SelectedItem.Value.ToString();
                    propertyVo.PropertyCountry = ddlCountry.SelectedItem.Value.ToString();
                    if (txtPurchaseDate.Text != string.Empty)
                        propertyVo.PurchaseDate = DateTime.Parse(txtPurchaseDate.Text);
                    if (txtPurchasePrice.Text != string.Empty)
                        propertyVo.PurchasePrice = float.Parse(txtPurchasePrice.Text.ToString());
                    if (txtQuantity.Text != string.Empty)
                        propertyVo.Quantity = float.Parse(txtQuantity.Text.ToString());
                    if (txtPurchaseValue.Text != string.Empty)
                        propertyVo.PurchaseValue = float.Parse(txtPurchaseValue.Text.ToString());
                    if (txtCurrentPrice.Text != string.Empty)
                        propertyVo.CurrentPrice = float.Parse(txtCurrentPrice.Text.ToString());
                    if (txtCurrentValue.Text != string.Empty)
                        propertyVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                    if (txtSaleDate.Text != "")
                        propertyVo.SellDate = Convert.ToDateTime(txtSaleDate.Text);
                    else
                        propertyVo.SellDate = DateTime.MinValue;
                    
                    if (txtSaleRate.Text != "")
                        propertyVo.SellPrice = float.Parse(txtSaleRate.Text.ToString());
                    if (txtSaleProceeds.Text != "")
                        propertyVo.SellValue = float.Parse(txtSaleProceeds.Text.ToString());
                    propertyVo.Remark = txtRemarks.Text.ToString().Trim();

                    if (propertyBo.UpdatePropertyPortfolio(propertyVo, userVo.UserId))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioProperty','none');", true);
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('PortfolioProperty','none');", true);
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
                FunctionInfo.Add("Method", "PortfolioPropertyEntry.ascx:btnSubmit_Click()");
                object[] objects = new object[5];
                objects[0] = propertyVo;
                objects[1] = AssetGroupCode;
                objects[2] = userVo;
                objects[3] = customerVo;
                objects[4] = customerAccountsVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            EnableDisableControls("edit");
        }

        protected void ddlInstrumentCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlInstrumentCat.SelectedIndex != 0)
                {
                    dsInstrumentSubCategory = assetBo.GetAssetInstrumentSubCategory(AssetGroupCode, ddlInstrumentCat.SelectedValue.Trim());
                    ddlInstrumentSubCat.DataSource = dsInstrumentSubCategory.Tables[0];
                    ddlInstrumentSubCat.DataTextField = "PAISC_AssetInstrumentSubCategoryName";
                    ddlInstrumentSubCat.DataValueField = "PAISC_AssetInstrumentSubCategoryCode";
                    ddlInstrumentSubCat.DataBind();
                    ddlInstrumentSubCat.Items.Insert(0, new ListItem("Select an Instrument Sub-Category", "Select an Instrument Sub-Category"));
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
                FunctionInfo.Add("Method", "PortfolioPropertyEntry.ascx:ddlInstrumentCat_SelectedIndexChanged()");
                object[] objects = new object[1];
                objects[0] = AssetGroupCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            
            if (mode == Mode.Edit || mode == Mode.View)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioProperty', 'none')", true);
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', 'action=PR')", true);
        
        }

        /// <summary>
        /// Set the mode to Edit/Add/View based on the Session["action"] value
        /// </summary>
        private void setMode()
        {
            if (Session["action"].ToString() == "Edit")
                mode = Mode.Edit;
            else if (Session["action"].ToString() == "View")
                mode = Mode.View;
            else
                mode = Mode.Add;
        }
    }
}


