using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using WealthERP.Base;
using BoCommon;
using System.Data;
using WealthERP.Base;
using BoProductMaster;
using BoWerpAdmin;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using System.Text.RegularExpressions;
using Telerik.Web.UI;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Configuration;
namespace WealthERP.OnlineOrderManagement
{
    public partial class MFSchemeRelateInformation : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        CustomerVo customerVo = new CustomerVo();
        OnlineMFOrderBo onlineMforderBo = new OnlineMFOrderBo();
        string path;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userBo = new UserBo();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            OnlineUserSessionBo.CheckSession();
            customerVo = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session["userVo"];
            if (!IsPostBack)
            {
                BindAMC();
                BindCategory();
                BindScheme();
                ShowAvailableLimits();
                
            }
        }
        protected void ddlAMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMC.SelectedIndex != 0)
            {
                BindScheme();
            }
        }
        private void BindAMC()
        {

            PriceBo priceBo = new PriceBo();
            DataTable dtGetAMCList = new DataTable();
            dtGetAMCList = priceBo.GetMutualFundList();
            ddlAMC.DataSource = dtGetAMCList;
            ddlAMC.DataTextField = dtGetAMCList.Columns["PA_AMCName"].ToString();
            ddlAMC.DataValueField = dtGetAMCList.Columns["PA_AMCCode"].ToString();
            ddlAMC.DataBind();
            ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }
        private void BindCategory()
        {
            DataSet dsProductAssetCategory;
            ProductMFBo productMFBo = new ProductMFBo();
            dsProductAssetCategory = productMFBo.GetProductAssetCategory();
            DataTable dtCategory = dsProductAssetCategory.Tables[0];
            ddlCategory.DataSource = dtCategory;
            ddlCategory.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }
        protected void BindScheme()
        {
            DataTable dt;
            ProductMFBo productMFBo = new ProductMFBo();
            if (ddlAMC.SelectedValue != "0")
            {
                dt = productMFBo.GetSchemePlanName(int.Parse(ddlAMC.SelectedValue));
                ddlScheme.DataSource = dt;
                ddlScheme.DataValueField = "PASP_SchemePlanCode";
                ddlScheme.DataTextField = "PASP_SchemePlanName";
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            else
            {
                //dt = productMFBo.GetSchemePlanName(int.Parse(ddlAMC.SelectedValue));
                //ddlScheme.DataSource = dt;
                //ddlScheme.DataValueField = "PASP_SchemePlanCode";
                //ddlScheme.DataTextField = "PASP_SchemePlanName";
                //ddlScheme.DataBind();
                //ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));

            }
        }
        protected void Go_OnClick(object sender, EventArgs e)
        {
            BindSchemeRelatedDetails();
        }
        protected void BindSchemeRelatedDetails()
        {
            OnlineOrderBackOfficeBo bo = new OnlineOrderBackOfficeBo();
            dvSchemeDetailsl.Visible = true;
            DataTable dtBindSchemeRelatedDetails = bo.GetSchemeDetails(int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), ddlCategory.SelectedValue);
            if (Cache["BindSchemeRelatedDetails" + userVo.UserId] != null)
            {
                Cache.Remove("BindSchemeRelatedDetails" + userVo.UserId);
            }
            Cache.Insert("BindSchemeRelatedDetails" + userVo.UserId, dtBindSchemeRelatedDetails);
            rpSchemeDetails.DataSource = dtBindSchemeRelatedDetails;
            rpSchemeDetails.DataBind();
            rpSchemeDetails.Visible = true;
        }
        private void ShowAvailableLimits()
        {
            if (!string.IsNullOrEmpty(customerVo.AccountId))
            {
                lblAvailableLimits.Text = onlineMforderBo.GetUserRMSAccountBalance(customerVo.AccountId).ToString();
            }

        }
        
        protected void rpSchemeDetails_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                string PASP_SchemePlanCode = e.CommandArgument.ToString();
                switch (e.CommandName)
                {
                    case "Buy":
                        if (Session["PageDefaultSetting"] != null)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderAdditionalPurchase','&SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFOrderAdditionalPurchase','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        break;
                    case "ABY":
                        if (Session["PageDefaultSetting"] != null)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderAdditionalPurchase','&SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFOrderAdditionalPurchase','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        break;
                    case "SIP":
                        if (Session["PageDefaultSetting"] != null)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderSIPTransType','&SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFOrderSIPTransType','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        break;
                    case "Sell":
                        if (Session["PageDefaultSetting"] != null)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderRdemptionTransType','&SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFOrderRdemptionTransType','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        break;
                    case "schemeDetails":

                        if (Session["PageDefaultSetting"] != null)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFSchemeDetails','&SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFSchemeDetails','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        break;
                    case "addToWatch":
                        CustomerAddMFSchemeToWatch(int.Parse(e.CommandArgument.ToString()), e);
                        break;
                       

                }
            }
        }
        private void CustomerAddMFSchemeToWatch(int SchemeCode,RepeaterCommandEventArgs e)
        {
            bool rResult = false;
            OnlineMFSchemeDetailsBo OnlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
            try
            {
                rResult=OnlineMFSchemeDetailsBo.CustomerAddMFSchemeToWatch(customerVo.CustomerId, SchemeCode, "MF",userVo.UserId);
                if (rResult == true)
                {
                    LinkButton lbViewWatch = (LinkButton)e.Item.FindControl("lbViewWatch");
                    LinkButton lbAddToWatch = (LinkButton)e.Item.FindControl("lbAddToWatch");
                    lbViewWatch.Visible = true;
                    lbAddToWatch.Visible = false;
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
                FunctionInfo.Add("Method", "OnlineMFSchemeDetailsDao.cs:CustomerAddMFSchemeToWatch()");
                object[] objects = new object[3];
                objects[0] = customerVo.CustomerId;
                objects[1] = SchemeCode;
                objects[2] = "MF";
                objects[3] = userVo.UserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }
       
        
        
    }
}
