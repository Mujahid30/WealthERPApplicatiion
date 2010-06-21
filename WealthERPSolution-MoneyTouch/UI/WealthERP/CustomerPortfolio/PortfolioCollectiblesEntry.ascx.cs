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
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioCollectiblesEntry : System.Web.UI.UserControl
    {
        CollectiblesBo collectiblesBo = new CollectiblesBo();
        CollectiblesVo collectiblesVo = new CollectiblesVo();
        AssetBo assetBo = new AssetBo();
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        int portfolioId;
        DataSet dsAssetCategories;
        DataTable dtMeasureCode;
        string command;
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            CollectiblesVo collectibleVo;
            try
            {
                //cvPurchaseDate.ValueToCompare = DateTime.Now.ToShortDateString();
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                if (Request.QueryString["action"] != null)
                    command = Request.QueryString["action"].ToString();
                if (!IsPostBack)
                {
                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    BindPortfolioDropDown();
                }
                portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                if (command == "Col")
                {
                    lnkEdit.Visible = false;
                    btnSaveChanges.Visible = false;
                    LoadCategory();
                    SetFields();
                }
                else
                {
                    collectibleVo = (CollectiblesVo)Session["collectiblesVo"];

                    if (command == "EditCol")
                    {
                        lnkEdit.Visible = false;
                        btnSaveChanges.Visible = true;
                        txtCurrentValue.Text = collectibleVo.CurrentValue.ToString();
                        txtName.Text = collectibleVo.Name.ToString();
                        txtPurchaseValue.Text = collectibleVo.PurchaseValue.ToString();
                        txtRemarks.Text = collectibleVo.Remark.ToString();


                        if (collectibleVo.PurchaseDate != DateTime.MinValue)

                            txtPurchaseDate.Text = collectibleVo.PurchaseDate.ToShortDateString().ToString();

                        else
                            txtPurchaseDate.Text = "";
                        
                        btnSubmit.Visible = false;
                        LoadCategory();
                        ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue(collectibleVo.AssetCategoryCode.ToString()));

                        ddlCategory.Visible = true;

                    }
                    else if (command == "ViewCol")
                    {
                        lnkEdit.Visible = true;
                        btnSaveChanges.Visible = false;
                        btnSubmit.Visible = false;
                        txtCurrentValue.Text = collectibleVo.CurrentValue.ToString();
                        txtCurrentValue.Enabled = false;
                        txtName.Text = collectibleVo.Name.ToString();
                        txtName.Enabled = false;

                        
                            txtPurchaseValue.Text = collectibleVo.PurchaseValue.ToString();
                        
                        txtPurchaseValue.Enabled = false;
                        txtRemarks.Text = collectibleVo.Remark.ToString();
                        txtRemarks.Enabled = false;

                        if (collectibleVo.PurchaseDate != DateTime.MinValue)

                            txtPurchaseDate.Text = collectibleVo.PurchaseDate.ToShortDateString().ToString();

                        else
                            txtPurchaseDate.Text = "";
                        
                        txtPurchaseDate.Enabled = false;

                        LoadCategory();
                        ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue(collectibleVo.AssetCategoryCode.ToString()));
                        //ddlCategory.Items.Insert(0, collectibleVo.AssetCategoryCode.ToString());

                        ddlCategory.Enabled = false;
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

                FunctionInfo.Add("Method", "PortfolioCollectiblesEntry.ascx:Page_Load()");
                object[] objects = new object[5];
                objects[0] = customerVo;
                objects[1] = userVo;
                objects[2] = portfolioId;
                objects[3] = command;
                objects[4] = collectiblesVo;
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

        private void SetFields()
        {
            txtCurrentValue.Text = "";
            txtName.Text = "";
            txtPurchaseDate.Text = "";
            txtPurchaseValue.Text = "";
            txtRemarks.Text = "";
            lnkBtnBack.Visible = false;
            lnkEdit.Visible = false;
            LoadCategory();
            ddlCategory.Visible = true;
        }
        public void LoadCategory()
        {
            dsAssetCategories = assetBo.GetAssetInstrumentCategory("CL"); //Change to the respective GroupCode
            ddlCategory.DataSource = dsAssetCategories.Tables[0];
            ddlCategory.DataTextField = "PAIC_AssetInstrumentCategoryName";
            ddlCategory.DataValueField = "PAIC_AssetInstrumentCategoryCode";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Select a Category", "Select a Category"));

            if (command == "EditCol" || command == "ViewCol")
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                collectiblesVo.PortfolioId = portfolioId;
                //collectiblesVo.PortfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
                Session[SessionContents.PortfolioId] = collectiblesVo.PortfolioId;
                collectiblesVo.Name = txtName.Text.ToString();
                collectiblesVo.AssetGroupCode = "CL";
                collectiblesVo.AssetCategoryCode = ddlCategory.SelectedItem.Value.ToString();
                if (txtPurchaseDate.Text.ToString() != string.Empty)
                    collectiblesVo.PurchaseDate = DateTime.Parse(txtPurchaseDate.Text.Trim());
                else
                    collectiblesVo.PurchaseDate = DateTime.MinValue;
                if (txtPurchaseValue.Text!="")
                    collectiblesVo.PurchaseValue = int.Parse(txtPurchaseValue.Text.ToString());
                else
                    collectiblesVo.PurchaseValue = 0;
                collectiblesVo.CurrentValue = double.Parse(txtCurrentValue.Text.ToString());
                collectiblesVo.Remark = txtRemarks.Text.ToString();
                collectiblesBo.CreateCollectiblesPortfolio(collectiblesVo, userVo.UserId);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCollectiblesPortfolio','none');", true);
             }
                    
            
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioCollectiblesEntry.ascx:btnSubmit_Click()");
                object[] objects = new object[1];
                objects[0] = collectiblesVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            CollectiblesVo collectibleVo = new CollectiblesVo();
            try
            {
                collectibleVo = (CollectiblesVo)Session["collectiblesVo"];
                txtCurrentValue.Text = collectibleVo.CurrentValue.ToString();
                txtCurrentValue.Enabled = true;
                txtName.Text = collectibleVo.Name.ToString();
                txtName.Enabled = true;
                txtPurchaseValue.Text = collectibleVo.PurchaseValue.ToString();
                txtPurchaseValue.Enabled = true;
                txtRemarks.Text = collectibleVo.Remark.ToString();
                txtRemarks.Enabled = true;


                if (collectibleVo.PurchaseDate != DateTime.MinValue)

                txtPurchaseDate.Text = collectibleVo.PurchaseDate.ToShortDateString().ToString();

                else
                    txtPurchaseDate.Text = "";
                ddlCategory.Items.Insert(0, collectibleVo.AssetCategoryCode.ToString());
                LoadCategory();
                ddlCategory.Enabled = true;
                btnSaveChanges.Visible = true;
                lnkEdit.Visible = false;
                txtPurchaseDate.Enabled = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioCollectiblesEntry.ascx:btnSubmit_Click()");
                object[] objects = new object[1];
                objects[0] = collectibleVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            CollectiblesVo newCollectibleVo = new CollectiblesVo();
            try
            {
                newCollectibleVo = new CollectiblesVo();
                CollectiblesVo collectibleVo = (CollectiblesVo)Session["collectiblesVo"];
                newCollectibleVo.AssetGroupCode = "CL";
                newCollectibleVo.AssetCategoryCode = ddlCategory.SelectedItem.Value.ToString();
                newCollectibleVo.CollectibleId = collectibleVo.CollectibleId;
                //newCollectibleVo.CurrentPrice=float.Parse(
                newCollectibleVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                newCollectibleVo.PortfolioId = collectibleVo.PortfolioId;
                newCollectibleVo.Name = txtName.Text;
                if(txtPurchaseDate.Text.ToString()!=string.Empty)
                    newCollectibleVo.PurchaseDate = DateTime.Parse(txtPurchaseDate.Text.Trim());
                else
                    newCollectibleVo.PurchaseDate = DateTime.MinValue;
                if (txtPurchaseValue.Text.ToString() != string.Empty)
                    newCollectibleVo.PurchaseValue = float.Parse(txtPurchaseValue.Text.ToString());
                else
                    newCollectibleVo.PurchaseValue = 0;
                        newCollectibleVo.Remark = txtRemarks.Text.ToString();
                collectiblesBo.UpdateCollectiblesPortfolio(newCollectibleVo, userVo.UserId);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCollectiblesPortfolio','none');", true);
               }
            

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioCollectiblesEntry.ascx:btnSaveChanges_Click()");
                object[] objects = new object[1];
                objects[0] = newCollectibleVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCollectiblesPortfolio', 'none')", true);
        }

        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
        }
    }
}
