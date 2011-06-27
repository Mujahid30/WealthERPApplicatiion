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
using BoCommon;
using WealthERP.Base;
using VoCustomerProfiling;
using VoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioGoldEntry : System.Web.UI.UserControl
    {
        GoldVo goldVo = new GoldVo();
        GoldBo goldBo = new GoldBo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        AssetBo assetBo = new AssetBo();
        UserVo userVo = new UserVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerVo customerVo = new CustomerVo();
        DataSet dsAssetCategories;
        DataTable dtMeasureCode;
        static int portfolioId = 0;
        string path;
        string action;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                cvPurchaseDate.ValueToCompare = DateTime.Now.ToShortDateString();
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                if (!IsPostBack)
                {
                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    BindPortfolioDropDown();
                }
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                if (Request.QueryString["action"] != null)
                    action = Request.QueryString["action"].ToString();

                lnkEdit.Visible = false;
                if (action == "ViewGold")
                {
                    SetViewFields();

                }
                else if (action == "EditGold")
                {
                    SetEditFields();
                }
                else if (action == "GoldEntry")
                {
                    SetEntryFields();


                    LoadAssetCategories();
                    LoadMeasureCode();
                }
                if (Session["GoldActionStatus"] != null)
                {                    
                    if (Session["GoldActionStatus"].ToString() == "Edit")
                    {
                        SetEditFields();
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
                FunctionInfo.Add("Method", "PortfolioGoldEntry.ascx:Page_Load()");
                object[] objects = new object[5];
                objects[0] = customerVo;
                objects[1] = userVo;
                objects[2] = action;
                objects[3] = portfolioId;
                objects[4] = path;
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

        private void BindPortfolioDropDown()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();

            ddlPortfolio.SelectedValue = portfolioId.ToString();

        }

        private void SetEntryFields()
        {
            //lblHeader.Text = "Gold Details Entry Form";

            btnSaveChanges.Visible = false;
            lblAssetCategory.Visible = false;
        }

        private void SetEditFields()
        {
            try
            {
                //lblHeader.Text = "Gold Details Edit Form";
                goldVo = (GoldVo)Session["goldVo"];
                Session["GoldActionStatus"] = "Edit";
                btnSubmit.Visible = false;
                btnSaveChanges.Visible = true;
                

                // Asset Group - Gold

                // Asset Category 
                lblAssetCategory.Text = goldVo.AssetCategoryName.ToString().Trim();
                ddlCategory.Visible = false;
                if (lblAssetCategory.Text == "Gems & Jewellery -- Gold")
                {
                    btnUsePrice.Visible = true;
                    btnSellPrice.Visible = true;
                    btnSaleCost.Visible = true;
                    txtCurrentPrice.Enabled = false;


                }
                else
                {
                    btnUsePrice.Visible = false;
                    btnSellPrice.Visible = false;
                    btnSaleCost.Visible = false;
                    txtCurrentPrice.Enabled = true;
                    //txtCurrentValue.Enabled = true;
                }
                    

                    // Purchase Date
                    if (goldVo.PurchaseDate != null && goldVo.PurchaseDate != DateTime.MinValue)
                        txtPurchaseDate.Text = goldVo.PurchaseDate.ToShortDateString().ToString();
                    txtPurchaseDate.Enabled = true;
                    txtPurchaseDate_CalendarExtender.Enabled = true;

                    // Sale Date
                    if (goldVo.SellDate != DateTime.MinValue)
                        txtSaleDate.Text = goldVo.SellDate.ToShortDateString().ToString();
                    txtSaleDate.Enabled = true;
                    txtSaleDate_CalendarExtender.Enabled = true;

                    // Current Price
                    txtCurrentPrice.Text = goldVo.CurrentPrice.ToString();
                    //txtCurrentPrice.Enabled = true;

                    // Current Value 
                    txtCurrentValue.Text = goldVo.CurrentValue.ToString();
                    //txtCurrentValue.Enabled = true;

                    // Particulars / Name
                    txtName.Text = goldVo.Name.ToString();
                    txtName.Enabled = true;

                    // Purchase Price
                    txtPurchasePrice.Text = goldVo.PurchasePrice.ToString();
                    txtPurchasePrice.Enabled = true;

                    // Purchase Value
                    txtPurchaseValue.Text = goldVo.PurchaseValue.ToString();
                    txtPurchaseValue.Enabled = true;

                    // Quantity
                    txtQuantity.Text = goldVo.Quantity.ToString();
                    txtQuantity.Enabled = true;

                    // Remarks 
                    txtRemarks.Text = goldVo.Remarks.ToString();
                    txtRemarks.Enabled = true;

                    // Sales Rate / Sell Price
                    if (goldVo.SellPrice != 0)
                        txtSaleRate.Text = goldVo.SellPrice.ToString();

                    txtSaleRate.Enabled = true;
                    // Sale Value
                    if (goldVo.SellValue != 0)
                        txtSaleValue.Text = goldVo.SellValue.ToString();

                    txtSaleValue.Enabled = true;


                    // MeasureCode
                    LoadMeasureCode();
                    ddlMeasureCode.SelectedValue = goldVo.MeasureCode.ToString().Trim();
                    ddlMeasureCode.Enabled = true;


                }
            
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioGoldEntry.ascx:SetEditFields()");
                object[] objects = new object[1];
                objects[0] = goldVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void SetViewFields()
        {
            try
            {
                //lblHeader.Text = "Gold Details View Form";

                goldVo = (GoldVo)Session["goldVo"];

                lnkEdit.Visible = true;
                btnSaveChanges.Visible = false;
                btnSubmit.Visible = false;

                // Asset Group - Gold



                // Asset Category 
                lblAssetCategory.Text = goldVo.AssetCategoryName.ToString().Trim();
                ddlCategory.Visible = false;

                // Purchase Date
                if (goldVo.PurchaseDate != DateTime.MinValue)
                    txtPurchaseDate.Text = goldVo.PurchaseDate.ToShortDateString().ToString();
                txtPurchaseDate.Enabled = false;
                txtPurchaseDate_CalendarExtender.Enabled = false;

                // Sale Date
                if (goldVo.SellDate != DateTime.MinValue)
                {
                    txtSaleDate.Text = goldVo.SellDate.ToShortDateString().ToString();
                }
                txtSaleDate.Enabled = false;
                txtSaleDate_CalendarExtender.Enabled = false;

                // Current Price
                txtCurrentPrice.Text = goldVo.CurrentPrice.ToString();
                txtCurrentPrice.Enabled = false;

                // Current Value 
                txtCurrentValue.Text = goldVo.CurrentValue.ToString();
                txtCurrentValue.Enabled = false;

                // Particulars / Name
                txtName.Text = goldVo.Name.ToString();
                txtName.Enabled = false;

                // Purchase Price
                txtPurchasePrice.Text = goldVo.PurchasePrice.ToString();
                txtPurchasePrice.Enabled = false;

                // Purchase Value
                txtPurchaseValue.Text = goldVo.PurchaseValue.ToString();
                txtPurchaseValue.Enabled = false;

                // Quantity
                txtQuantity.Text = goldVo.Quantity.ToString();
                txtQuantity.Enabled = false;

                // Remarks 
                txtRemarks.Text = goldVo.Remarks.ToString();
                txtRemarks.Enabled = false;

                // Sales Rate / Sell Price
                if (goldVo.SellPrice != 0)
                {
                    txtSaleRate.Text = goldVo.SellPrice.ToString();
                }
                txtSaleRate.Enabled = false;

                // Sale Value 
                if (goldVo.SellValue != 0)
                {
                    txtSaleValue.Text = goldVo.SellValue.ToString();
                }
                txtSaleValue.Enabled = false;

                // MeasureCode
                LoadMeasureCode();
                ddlMeasureCode.SelectedValue = goldVo.MeasureCode.ToString().Trim();
                ddlMeasureCode.Enabled = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioGoldEntry.ascx:SetViewFields()");
                object[] objects = new object[1];
                objects[0] = goldVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        private void LoadMeasureCode()
        {
            try
            {
                dtMeasureCode = XMLBo.GetMeasureCode(path, "GD");
                ddlMeasureCode.DataSource = dtMeasureCode;
                ddlMeasureCode.DataTextField = "Measure";
                ddlMeasureCode.DataValueField = "MeasureCode";
                ddlMeasureCode.DataBind();
                ddlMeasureCode.Items.Insert(0, new ListItem("Select a Measure Code", "Select a Measure Code"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioGoldEntry.ascx:LoadMeasureCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void LoadAssetCategories()
        {
            dsAssetCategories = assetBo.GetAssetInstrumentCategory("GD"); //Change to the respective GroupCode
            ddlCategory.DataSource = dsAssetCategories.Tables[0];
            ddlCategory.DataTextField = "PAIC_AssetInstrumentCategoryName".ToString().Trim();
            ddlCategory.DataValueField = "PAIC_AssetInstrumentCategoryCode".ToString().Trim();
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Select a Category", "Select a Category"));
        }

        protected void Current_PriceChange(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter Quantity');", true);
            }
            else
            {
                txtCurrentValue.Text = (Math.Round((double.Parse(txtCurrentPrice.Text) * double.Parse(txtQuantity.Text)), 4)).ToString();
            }
        }
        protected void Button_SaleRate(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter Quantity');", true);
            }
            else
            {
                txtSaleValue.Text = (Math.Round((double.Parse(txtSaleRate.Text) * double.Parse(txtQuantity.Text)), 4)).ToString();
            }
        }

        protected void btn_costUpdate(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter Quantity');", true);
            }
            else
            {
                if (string.IsNullOrEmpty(txtPurchasePrice.Text))
                {
                    txtPurchasePrice.Text = "0";
                }
                txtPurchaseValue.Text = (Math.Round((double.Parse(txtPurchasePrice.Text) * double.Parse(txtQuantity.Text)), 4)).ToString();
            }
        }

        protected void btn_categorycheck(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue == "GDGJ" || ddlCategory.SelectedValue == "GDSR")
            {
                btnUsePrice.Visible = false;
                btnSellPrice.Visible = false ;
                btnSaleCost.Visible = false;
                txtCurrentPrice.Enabled = true;
            }
            else
            {
                btnUsePrice.Visible = true;
                btnSellPrice.Visible = true;
                btnSaleCost.Visible = true;
                txtCurrentPrice.Enabled = false;
            }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtQuantity.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter Quantity');", true);
                }
                else
                {

                    if (string.IsNullOrEmpty(txtPurchasePrice.Text))
                    {
                        //txtPurchaseValue.Text = "0.00";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter Purchase Rate');", true);

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtPurchaseValue.Text))
                        {
                            //txtPurchasePrice.Text = "0.00";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter Purchase Cost');", true);

                        }
                        else
                        {


                            goldVo.PortfolioId = portfolioId;
                            goldVo.Name = txtName.Text.ToString();
                           // goldVo.AssetGroupCode = "GD";
                            goldVo.AssetCategoryCode = ddlCategory.SelectedItem.Value.ToString().Trim();
                            //if (ddlMeasureCode.SelectedItem.Value != string.Empty && ddlMeasureCode.SelectedIndex >= 0)
                            //goldVo.MeasureCode = ddlMeasureCode.SelectedItem.Value.ToString().Trim();
                            goldVo.MeasureCode = "GR";
                            if (txtQuantity.Text.Trim() != string.Empty)
                                goldVo.Quantity = float.Parse(txtQuantity.Text.ToString());
                            // goldVo.PurchaseDate = DateTime.Parse(ddlDay.SelectedItem.Value.ToString() + "/" + ddlMonth.SelectedItem.Value.ToString() + "/" + ddlYear.SelectedItem.Value.ToString());
                            if (txtPurchaseDate.Text.Trim() != string.Empty)
                                goldVo.PurchaseDate = DateTime.Parse(txtPurchaseDate.Text);
                            if (txtPurchasePrice.Text.Trim() != string.Empty)
                                goldVo.PurchasePrice = float.Parse(txtPurchasePrice.Text.Trim());
                            if (txtPurchaseValue.Text.Trim() != string.Empty)
                                goldVo.PurchaseValue = float.Parse(txtPurchaseValue.Text.Trim());
                            if (txtCurrentPrice.Text.Trim() != string.Empty)
                                goldVo.CurrentPrice = float.Parse(txtCurrentPrice.Text.Trim());
                            if (txtCurrentValue.Text.Trim() != string.Empty)
                                goldVo.CurrentValue = float.Parse(txtCurrentValue.Text.Trim());
                            // goldVo.SellDate = DateTime.Parse(ddlSaleDay.SelectedItem.Value.ToString() + "/" + ddlSaleMonth.SelectedItem.Value.ToString() + "/" + ddlYear.SelectedItem.Value.ToString());
                            if (txtSaleDate.Text.Trim() != "")
                                goldVo.SellDate = DateTime.Parse(txtSaleDate.Text.Trim());
                            else
                                goldVo.SellDate = DateTime.MinValue;

                            if (txtSaleRate.Text.Trim() != "")
                                goldVo.SellPrice = float.Parse(txtSaleRate.Text.Trim());
                            else
                                goldVo.SellPrice = float.Parse("0");

                            if (txtSaleValue.Text.Trim() != "")
                                goldVo.SellValue = float.Parse(txtSaleValue.Text.Trim());
                            else
                                goldVo.SellValue = float.Parse("0");

                            goldVo.Remarks = txtRemarks.Text.ToString();

                            if (goldBo.CreateGoldNetPosition(goldVo, userVo.UserId))
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewGoldPortfolio', 'none')", true);
                                // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewGoldPortfolio','none');", true);
                            }
                        }
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
                FunctionInfo.Add("Method", "PortfolioGoldEntry.ascx:btnSubmit_Click()");
                object[] objects = new object[2];
                objects[0] = userVo;
                objects[1] = goldVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void btnUseSellPrice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter Quantity');", true);
            }
            else
            {
                //if (string.IsNullOrEmpty(txtCurrentPrice.Text))
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter Rate');", true);
                //}
                //else
                {

                    DataSet ds = new DataSet();
                    string txtPurchaseCurrentDate = Convert.ToString(DateTime.Now);
                    string price;
                    ds = portfolioBo.GetGoldPrice(DateTime.Parse(txtPurchaseCurrentDate));
                    price = ds.Tables[0].Rows[0][0].ToString();
                    txtCurrentValue.Text = (Math.Round((double.Parse(price) * double.Parse(txtQuantity.Text)), 4)).ToString();
                    txtCurrentPrice.Text = price;
                }
            }
        }

        protected void btnUseSellCost_Click(object sender, EventArgs e)
        {

            
                if (!string.IsNullOrEmpty(txtSaleDate.Text))
                {

                    DataSet ds = new DataSet();

                    ds = portfolioBo.GetGoldPrice(DateTime.Parse(txtSaleDate.Text));
                    string saleprice;
                    saleprice = ds.Tables[0].Rows[0][0].ToString();
                    txtSaleValue.Text = (Math.Round((double.Parse(saleprice) * double.Parse(txtQuantity.Text)), 4)).ToString();
                    txtSaleRate.Text = saleprice;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter Sale Date');", true);
                }
            }
           
            
        

        protected void btnUsePrice_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            ds = portfolioBo.GetGoldPrice(DateTime.Parse(txtPurchaseDate.Text));
            txtPurchasePrice.Text = ds.Tables[0].Rows[0][0].ToString();
            if (!string.IsNullOrEmpty(txtPurchaseDate.Text))
            {
                if (!string.IsNullOrEmpty(txtQuantity.Text))
                    txtPurchaseValue.Text = (Math.Round((double.Parse(txtPurchasePrice.Text) * double.Parse(txtQuantity.Text)), 4)).ToString();

                else
                {
                    txtPurchasePrice.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter Quantity');", true);
                }
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert('Please enter Quantity');", true);

                //txtPurchaseValue.Text = " ";
                if (!string.IsNullOrEmpty(txtQuantity.Text))
                {
                    DataSet ds1 = new DataSet();
                    ds1 = portfolioBo.GetGoldPriceCurrent(DateTime.Parse(txtPurchaseDate.Text));
                    //txtCurrentPrice.Text = ds1.Tables[0].Rows[0][0].ToString();
                    //txtCurrentValue.Text = (Math.Round((double.Parse(txtCurrentPrice.Text) * double.Parse(txtQuantity.Text)), 4)).ToString();
                }
                else
                {
                    txtPurchasePrice.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter Quantity');", true);
                }
            }
            else
            {
                txtPurchasePrice.Text = "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter Purchase Date');", true);
            }

            

        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            GoldVo newGoldVo = new GoldVo();
            try
            {
                if (string.IsNullOrEmpty(txtQuantity.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter Quantity');", true);
                }
                else
                {
                    if (string.IsNullOrEmpty(txtPurchasePrice.Text))
                    {
                        //txtPurchaseValue.Text = "0.00";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter Purchase Rate');", true);

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtPurchaseValue.Text))
                        {
                            //txtPurchasePrice.Text = "0.00";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please enter Purchase Cost');", true);

                        }
                        else
                        {
                            goldVo = (GoldVo)Session["goldVo"];
                            userVo = (UserVo)Session["userVo"];

                            newGoldVo.AssetCategoryCode = goldVo.AssetCategoryCode.ToString().Trim();
                            newGoldVo.AssetGroupCode = goldVo.AssetGroupCode.ToString().Trim();
                            if (txtCurrentPrice.Text.Trim() != string.Empty)
                                newGoldVo.CurrentPrice = float.Parse(txtCurrentPrice.Text);
                            if (txtCurrentValue.Text.Trim() != string.Empty)
                                newGoldVo.CurrentValue = float.Parse(txtCurrentValue.Text);
                            newGoldVo.PortfolioId = goldVo.PortfolioId;
                            newGoldVo.GoldNPId = goldVo.GoldNPId;
                            //newGoldVo.MeasureCode = ddlMeasureCode.SelectedValue.ToString().Trim();
                            newGoldVo.MeasureCode = "GR";
                            newGoldVo.Name = txtName.Text;
                            if (txtPurchaseDate.Text.Trim() != string.Empty)
                                newGoldVo.PurchaseDate = DateTime.Parse(txtPurchaseDate.Text);
                            if (txtPurchasePrice.Text.Trim() != string.Empty)
                                newGoldVo.PurchasePrice = float.Parse(txtPurchasePrice.Text);
                            if (txtPurchaseValue.Text.Trim() != string.Empty)
                                newGoldVo.PurchaseValue = float.Parse(txtPurchaseValue.Text);
                            if (txtQuantity.Text.Trim() != string.Empty)
                                newGoldVo.Quantity = float.Parse(txtQuantity.Text);
                            newGoldVo.Remarks = (txtRemarks.Text);

                            if (txtSaleDate.Text.Trim() != "")
                                newGoldVo.SellDate = DateTime.Parse(txtSaleDate.Text.Trim());
                            else
                                newGoldVo.SellDate = DateTime.MinValue;

                            if (txtSaleRate.Text.Trim() != "")
                                newGoldVo.SellPrice = float.Parse(txtSaleRate.Text.Trim());
                            else
                                newGoldVo.SellPrice = float.Parse("0");

                            if (txtSaleValue.Text.Trim() != "")
                                newGoldVo.SellValue = float.Parse(txtSaleValue.Text.Trim());
                            else
                                newGoldVo.SellValue = float.Parse("0");

                            if (goldBo.UpdateGoldNetPosition(newGoldVo, userVo.UserId))
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewGoldPortfolio', 'none')", true);
                                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewGoldPortfolio','none');", true);
                            }
                        }
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
                FunctionInfo.Add("Method", "PortfolioGoldEntry.ascx:btnSaveChanges_Click()");
                object[] objects = new object[3];
                objects[0] = userVo;
                objects[1] = goldVo;
                objects[2] = newGoldVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            SetEditFields();
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Session.Remove("GoldActionStatus");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewGoldPortfolio', 'none')", true);
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("GoldActionStatus");
            }
        }
    }
}