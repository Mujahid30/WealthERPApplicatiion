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
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
namespace WealthERP.OnlineOrderManagement
{
    public partial class MFSchemeRelateInformation : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        CustomerVo customerVo = new CustomerVo();
        string path;
        int PageSize = 5;
        OnlineMFSchemeDetailsBo OnlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
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
                BindCategory(ddlCategory);
                BindCategory(ddlTopCategory);
                BindCategory(ddlMarketCategory);
                BindScheme();
                BindSortList(ddlTopRatedSort);
                BindSortList(ddlMarketDataSort);
                if (Request.QueryString["FilterType"] == "NFO")
                {
                    SetParametersForAdminGrid("lbNFOList");
                    dvFundFilter.Visible = false;
                }
                else if (Request.QueryString["FilterType"] == "watchList")
                {
                    SetParametersForAdminGrid("lbViewWatchList");
                    dvFundFilter.Visible = false;
                }
                else
                {
                    SetParametersForAdminGrid("lbTopSchemes");
                    BindTopMarketSchemes(ddlMarketCategory.SelectedValue, Boolean.Parse(ddlSIP.SelectedValue), int.Parse(ddlReturns.SelectedValue), customerVo.CustomerId,1,int.Parse(ddlMarketDataSort.SelectedValue));
                    dvFundFilter.Visible = true;
                }
            }
        }
        protected void clearAllControls()
        {
            hfAMCCode.Value = null;
            hfSchemeCode.Value = null;
            hfCategory.Value = null;
            hfCustomerId.Value = null;
            hfIsSchemeDetails.Value = null;
            hfNFOType.Value = null;
            hfIsSIP.Value = null;
            RadTabStripAdsUpload.Tabs[0].Visible = true;
        }
        protected void ddlAMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMC.SelectedIndex != 0)
            {
                BindScheme();
                BindCategory(ddlCategory);

            }
        }
        private void BindSortList(DropDownList ddl)
        {
            Dictionary<string, int> dcSortList = new Dictionary<string, int>();
            dcSortList = OnlineMFSchemeDetailsBo.GetSortList(ddl.ID);
            ddl.DataSource = dcSortList;
            ddl.DataTextField = "Key";
            ddl.DataValueField = "Value";
            ddl.DataBind();
            ddl.SelectedValue = "2";
        }
        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindScheme();
        }
        private void BindAMC()
        {

            DataTable dtGetAMCList = new DataTable();
            CommonLookupBo commonLookupBo = new CommonLookupBo();
            dtGetAMCList = commonLookupBo.GetProdAmc(0, true);
            ddlAMC.DataSource = dtGetAMCList;
            ddlAMC.DataTextField = dtGetAMCList.Columns["PA_AMCName"].ToString();
            ddlAMC.DataValueField = dtGetAMCList.Columns["PA_AMCCode"].ToString();
            ddlAMC.DataBind();
            ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select AMC", "0"));
        }
        private void BindCategory(DropDownList ddlCategory)
        {
            DataSet dsProductAssetCategory;
            ProductMFBo productMFBo = new ProductMFBo();
            dsProductAssetCategory = productMFBo.GetProductAssetCategory();
            DataTable dtCategory = dsProductAssetCategory.Tables[0];
            ddlCategory.DataSource = dtCategory;
            ddlCategory.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory.DataBind();
            if (ddlCategory.ID.ToString() == "ddlCategory")
            {
                ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Categories", "0"));
            }
            else
            {
                ddlCategory.SelectedValue = "MFEQ";
            }
            

        }
        protected void BindScheme()
        {
            DataTable dt;
            OnlineMFSchemeDetailsBo OnlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
            dt = OnlineMFSchemeDetailsBo.GetAMCandCategoryWiseScheme(int.Parse(ddlAMC.SelectedValue), ddlCategory.SelectedValue);
            ddlScheme.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                ddlScheme.DataSource = dt;
                ddlScheme.DataValueField = "PASP_SchemePlanCode";
                ddlScheme.DataTextField = "PASP_SchemePlanName";
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Schemes", "0"));
            }
        }
        protected void Go_OnClick(object sender, EventArgs e)
        {
            clearAllControls();
            ViewState["FilterType"] = "Schemes Details";
            hfAMCCode.Value = ddlAMC.SelectedValue;
            hfSchemeCode.Value = ddlScheme.SelectedValue==""?"0":ddlScheme.SelectedValue;
            hfCategory.Value = ddlCategory.SelectedValue;
            hfCustomerId.Value = customerVo.CustomerId.ToString();
            hfIsSchemeDetails.Value = "1";
            hfNFOType.Value = "false";
            rblNFOType.Visible = false;
            hfIsSIP.Value =" true";
            BindSchemeRelatedDetails(int.Parse(hfAMCCode.Value), int.Parse(hfSchemeCode.Value), hfCategory.Value, int.Parse(hfCustomerId.Value), Int16.Parse(hfIsSchemeDetails.Value), Boolean.Parse(hfNFOType.Value), 1, Boolean.Parse(hfIsSIP.Value),int.Parse(hfSortOn.Value));
            lblHeading.Text = "Schemes Details";
            RadTabStripAdsUpload.Tabs[0].Visible = false;
            RadTabStripAdsUpload.Tabs[1].Text = "Scheme Details";
            RadTabStripAdsUpload.Tabs[1].Selected = true;
            multipageAdsUpload.SelectedIndex = 1;
            dvfilterTopRated.Visible = false;


        }

        protected void BindSchemeRelatedDetails(int amcCode, int SchemeCode, string category, int customerId, Int16 isSchemeDetails, Boolean NFOType, int pageIndex, Boolean isSIP, int SortOn)
        {
            OnlineOrderBackOfficeBo bo = new OnlineOrderBackOfficeBo();
            int recordCount = 0;
            DataTable dtBindSchemeRelatedDetails = bo.GetSchemeDetails(amcCode, SchemeCode, category, customerId, isSchemeDetails, NFOType, out recordCount, pageIndex, PageSize, isSIP, SortOn);
            if (Cache["BindSchemeRelatedDetails" + userVo.UserId] != null)
            {
                Cache.Remove("BindSchemeRelatedDetails" + userVo.UserId);
            }
            Cache.Insert("BindSchemeRelatedDetails" + userVo.UserId, dtBindSchemeRelatedDetails);
            rpSchemeDetails.DataSource = dtBindSchemeRelatedDetails;
            rpSchemeDetails.DataBind();
            rpSchemeDetails.Visible = true;
            this.PopulatePager(recordCount, pageIndex, rptPager);


        }
        private void PopulatePager(int recordCount, int currentPage, Repeater rptPaging)
        {
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    
                }
            }
          
            rptPaging.DataSource = pages;
            rptPaging.DataBind();
        }
        protected void rpTopPager_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            BindTopMarketSchemes(ddlMarketCategory.SelectedValue, Boolean.Parse(ddlSIP.SelectedValue), int.Parse(ddlReturns.SelectedValue), customerVo.CustomerId, pageIndex, int.Parse(ddlMarketDataSort.SelectedValue));

        }
        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            BindSchemeRelatedDetails(int.Parse(hfAMCCode.Value), int.Parse(hfSchemeCode.Value), hfCategory.Value, int.Parse(hfCustomerId.Value), Int16.Parse(hfIsSchemeDetails.Value), Boolean.Parse(hfNFOType.Value), pageIndex, Boolean.Parse(hfIsSIP.Value), int.Parse(hfSortOn.Value));

        }
        protected void btnTopPeformers_OnClick(object sender, EventArgs e)
        {
            clearAllControls();
            SetParametersForAdminGrid("lbTopSchemes");

        }
        protected void btnTopRated_OnClick(object sender, EventArgs e)
        {
            BindTopMarketSchemes(ddlMarketCategory.SelectedValue, Boolean.Parse(ddlSIP.SelectedValue), int.Parse(ddlReturns.SelectedValue), customerVo.CustomerId, 1, int.Parse(ddlMarketDataSort.SelectedValue));
        }

        protected void GetSchemeDetails(object sender, EventArgs e)
        {
            clearAllControls();
            LinkButton lk = (LinkButton)sender;
            ViewState["FilterType"] = lk.ID.ToString();
            rblNFOType.Visible = false;
            lbViewWatchList.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0396CC");
            lbNFOList.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0396CC");
            lbTopSchemes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0396CC");
          
            SetParametersForAdminGrid(lk.ID.ToString());
           
        }

        private void SetParametersForAdminGrid(string Action)
        {
            ViewState["FilterType"] = Action;
            rblNFOType.Visible = false;
            hfCustomerId.Value = customerVo.CustomerId.ToString();
            RadTabStripAdsUpload.Tabs[0].Visible = true;
            dvfilterTopRated.Visible = false;
            switch (Action)
            {
                case "lbViewWatchList": hfAMCCode.Value = "0";
                    hfSchemeCode.Value = "0";
                    hfCategory.Value = "0";
                    hfIsSchemeDetails.Value = "0";
                    hfNFOType.Value = rblNFOType.SelectedValue;
                    hfIsSIP.Value = "false";
                    lblHeading.Text = "My Watch List";
                    RadTabStripAdsUpload.Tabs[0].Visible = false;
                    RadTabStripAdsUpload.Tabs[1].Text = "My Watch List";
                    RadTabStripAdsUpload.Tabs[1].Selected = true;
                    multipageAdsUpload.SelectedIndex = 1;
                    lbViewWatchList.ForeColor = System.Drawing.ColorTranslator.FromHtml("#07090A");
                    hfSortOn.Value = ddlTopRatedSort.SelectedValue;
                    break;
                case "lbNFOList":
                    hfAMCCode.Value = "0";
                    hfSchemeCode.Value = "0";
                    hfCategory.Value = "0";
                    hfIsSchemeDetails.Value = "3";
                    hfNFOType.Value = rblNFOType.SelectedValue;
                    hfIsSIP.Value = "false";
                    lblHeading.Text = "NFO Scheme";
                    rblNFOType.Visible = true;
                    RadTabStripAdsUpload.Tabs[0].Visible = false;
                    RadTabStripAdsUpload.Tabs[1].Text = "NFO Scheme";
                    RadTabStripAdsUpload.Tabs[1].Selected = true;
                    multipageAdsUpload.SelectedIndex = 1;
                    lbNFOList.ForeColor = System.Drawing.ColorTranslator.FromHtml("#07090A");
                    hfSortOn.Value = ddlTopRatedSort.SelectedValue;
                    break;
                case "lbTopSchemes":
                    hfAMCCode.Value = "0";
                    hfSchemeCode.Value = "0";
                    hfCategory.Value = ddlTopCategory.SelectedValue;
                    hfIsSchemeDetails.Value = "2";
                    hfNFOType.Value = "false";
                    hfIsSIP.Value = ddlTopRatedType.SelectedValue;
                    lblHeading.Text = "Top Ten Schemes";
                    lbTopSchemes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#07090A");
                    dvfilterTopRated.Visible = true;
                    hfSortOn.Value = ddlTopRatedSort.SelectedValue;
                    break;
                case "lbSchemeDetails": dvDemo.Visible = true;
                    lblHeading.Text = "Schemes Details";
                    hfIsSIP.Value = "false";
                    RadTabStripAdsUpload.Tabs[0].Visible = false;
                    RadTabStripAdsUpload.Tabs[1].Text = "Schemes Details";
                    hfSortOn.Value = ddlTopRatedSort.SelectedValue;
                    break;

            }
            BindSchemeRelatedDetails(int.Parse(hfAMCCode.Value), int.Parse(hfSchemeCode.Value), hfCategory.Value, int.Parse(hfCustomerId.Value), Int16.Parse(hfIsSchemeDetails.Value), Boolean.Parse(hfNFOType.Value), 1,Boolean.Parse(hfIsSIP.Value), int.Parse(hfSortOn.Value));
        }
        protected void ddlAction_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            string selectedValue = ddl.SelectedValue;
            RepeaterItem itm = (RepeaterItem)ddl.NamingContainer;
            if (itm != null)
            {
                Label lblscheme = (Label)itm.FindControl("lblSchemeCode");
                for (int i = 0; i < rpSchemeDetails.Items.Count; i++)
                {
                    Label dkSchemeCode = rpSchemeDetails.Items[i].FindControl("lblSchemeCode") as Label;
                    DropDownList DDLValue = rpSchemeDetails.Items[i].FindControl("ddlAction") as DropDownList;
                    if (lblscheme.Text.Trim() != dkSchemeCode.Text.Trim())
                    {
                        DDLValue.SelectedValue = "0";
                    }
                }
                if (ViewState["FilterType"].ToString() == "lbNFOList")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwe1v", "LoadTransactPanelFromMainPage('MFOrderNFOTransType','" + lblscheme.Text + "')", true);
                }
                else
                {
                    if (ddl.SelectedValue == "Buy")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptv2ewv", "LoadTransactPanelFromMainPage('MFOrderPurchaseTransType','" + lblscheme.Text + "')", true);
                    }
                    else if (ddl.SelectedValue == "SIP")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvw4v", "LoadTransactPanelFromMainPage('MFOrderSIPTransType','" + lblscheme.Text + "')", true);
                    }
                }
            }
        }
        protected void ddlMarketlAction_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            string selectedValue = ddl.SelectedValue;
            RepeaterItem itm = (RepeaterItem)ddl.NamingContainer;
            string id = ddl.ID;
            if (itm != null)
            {
                Label lblscheme = (Label)itm.FindControl("lblschemeCode");
                for (int i = 0; i < rptTopMarketSchemes.Items.Count; i++)
                {
                    Label dkSchemeCode = rptTopMarketSchemes.Items[i].FindControl("lblschemeCode") as Label;
                    DropDownList DDLValue = rptTopMarketSchemes.Items[i].FindControl("ddlMarketlAction") as DropDownList;
                    if (lblscheme.Text.Trim() != dkSchemeCode.Text.Trim())
                    {
                        DDLValue.SelectedValue = "0";
                    }
                }
                    if (ddl.SelectedValue == "Buy")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanelFromMainPage('MFOrderPurchaseTransType','" + lblscheme.Text + "')", true);
                    }
                    else if (ddl.SelectedValue == "SIP")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptSIP", "LoadTransactPanelFromMainPage('MFOrderSIPTransType','" + lblscheme.Text + "')", true);
                    }
            }
        }
        protected void rpSchemeDetails_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                string PASP_SchemePlanCode = e.CommandArgument.ToString();
                DropDownList dropDownList = (DropDownList)e.Item.FindControl("ddlAction");

                switch (e.CommandName)
                {
                    case "Buy":
                        if (Session["PageDefaultSetting"] != null)
                        {
                            Session["MFSchemePlan"] = PASP_SchemePlanCode;
                            if (ViewState["FilterType"].ToString() != "lbNFOList")
                            {
                                LoadMFTransactionPage("MFOrderPurchaseTransType", 2);
                            }
                            else
                            {
                                LoadMFTransactionPage("MFOrderNFOTransType", 2);
                            }

                        }
                        else
                        {
                            if (ViewState["FilterType"].ToString() != "lbNFOList")
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFOrderPurchaseTransType','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFOrderNFOTransType','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);

                            }
                        }
                        break;
                    case "ABY":
                        if (Session["PageDefaultSetting"] != null)
                        {
                            Session["MFSchemePlan"] = PASP_SchemePlanCode;
                            LoadMFTransactionPage("MFOrderAdditionalPurchase", 2);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFOrderAdditionalPurchase','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        break;
                    case "SIP":
                        if (Session["PageDefaultSetting"] != null)
                        {
                            Session["MFSchemePlan"] = PASP_SchemePlanCode;
                            LoadMFTransactionPage("MFOrderSIPTransType", 2);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFOrderSIPTransType','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        break;
                    case "Sell":
                        if (Session["PageDefaultSetting"] != null)
                        {
                            Session["MFSchemePlan"] = PASP_SchemePlanCode;
                            LoadMFTransactionPage("MFOrderRdemptionTransType", 2);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFOrderRdemptionTransType','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        break;
                    case "schemeDetails":

                        if (Session["PageDefaultSetting"] != null)
                        {
                            //Session["MFSchemePlan"] = PASP_SchemePlanCode;
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFSchemeDetails','&schemeCode=" + PASP_SchemePlanCode + "');", true);
                            LoadMFTransactionPage("MFSchemeDetails", 1);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFSchemeDetails','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        break;
                    case "addToWatch":
                        CustomerAddMFSchemeToWatch(int.Parse(e.CommandArgument.ToString()), e);
                        break;

                    case "RemoveFrmWatch": DeleteSchemeFromCustomerWatch(int.Parse(e.CommandArgument.ToString()), e, customerVo.CustomerId);
                        BindSchemeRelatedDetails(int.Parse(hfAMCCode.Value), int.Parse(hfSchemeCode.Value), hfCategory.Value, int.Parse(hfCustomerId.Value), Int16.Parse(hfIsSchemeDetails.Value), Boolean.Parse(hfNFOType.Value), 1, Boolean.Parse(hfIsSIP.Value), int.Parse(hfSortOn.Value));
                        break;



                }
            }
        }
        private void DeleteSchemeFromCustomerWatch(int SchemeCode, RepeaterCommandEventArgs e, int customerId)
        {
            bool rResult = false;
            OnlineMFSchemeDetailsBo OnlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
            try
            {
                rResult = OnlineMFSchemeDetailsBo.DeleteSchemeFromCustomerWatch(SchemeCode, customerId, "MF");
                if (rResult == true)
                {
                    LinkButton lbViewWatch = (LinkButton)e.Item.FindControl("lbRemoveWatch");
                    LinkButton lbAddToWatch = (LinkButton)e.Item.FindControl("lbAddToWatch");
                    lbViewWatch.Visible = false;
                    lbAddToWatch.Visible = true;
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
                FunctionInfo.Add("Method", " DeleteSchemeFromCustomerWatch(int SchemeCode, RepeaterCommandEventArgs e, int customerId)");
                object[] objects = new object[4];
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
        private void CustomerAddMFSchemeToWatch(int SchemeCode, RepeaterCommandEventArgs e)
        {
            bool rResult = false;
            OnlineMFSchemeDetailsBo OnlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
            try
            {
                rResult = OnlineMFSchemeDetailsBo.CustomerAddMFSchemeToWatch(customerVo.CustomerId, SchemeCode, "MF", userVo.UserId);
                if (rResult == true)
                {
                    LinkButton lbViewWatch = (LinkButton)e.Item.FindControl("lbRemoveWatch");
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
                object[] objects = new object[4];
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

        protected void LoadMFTransactionPage(string pageId, int investerpage)
        {
            Dictionary<string, string> defaultProductPageSetting = new Dictionary<string, string>();

            defaultProductPageSetting.Clear();
            if (investerpage == 1)
            {
                defaultProductPageSetting.Add("ProductType", "MF");
                defaultProductPageSetting.Add("ProductMenu", "trMFOrderMenuMarketTab");
                defaultProductPageSetting.Add("ProductMenuItem", "RTSMFOrderMenuHomeMarket");
                defaultProductPageSetting.Add("ProductMenuItemPage", pageId);
            }
            else
            {
                defaultProductPageSetting.Add("ProductType", "MF");
                defaultProductPageSetting.Add("ProductMenu", "trMFOrderMenuTransactTab");
                defaultProductPageSetting.Add("ProductMenuItem", "RTSMFOrderMenuTransact");
                defaultProductPageSetting.Add("ProductMenuItemPage", pageId);
            }
            Session["PageDefaultSetting"] = defaultProductPageSetting;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadTopPanelControl('OnlineOrderTopMenu','login');", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscriptabcd", "LoadTopPanelDefault('OnlineOrderTopMenu');", true);

        }

        protected void rblNFOType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindSchemeRelatedDetails(0, 0, "0", 0, 3, Boolean.Parse(rblNFOType.SelectedValue), 1, false, int.Parse(hfSortOn.Value));
            hfNFOType.Value = rblNFOType.SelectedValue.ToString();
        }
        protected void rpSchemeDetails_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                HtmlTableCell tdNFOStrtDate = (HtmlTableCell)e.Item.FindControl("tdNFOStrtDate");
                HtmlTableCell tdNFOEndDate = (HtmlTableCell)e.Item.FindControl("tdNFOEndDate");
                HtmlTableCell tdNFOAmt = (HtmlTableCell)e.Item.FindControl("tdNFOAmt");
                HtmlTableCell tdNFOcategory = (HtmlTableCell)e.Item.FindControl("tdNFOcategory");
                HtmlTableCell tdNAV = (HtmlTableCell)e.Item.FindControl("tdNAV");
                HtmlTableCell tdReturn = (HtmlTableCell)e.Item.FindControl("tdReturn");
                HtmlTableCell tdSchemeRank = (HtmlTableCell)e.Item.FindControl("tdSchemeRank");
                HtmlTableCell tdSIP = (HtmlTableCell)e.Item.FindControl("tdSIP");
                HtmlTableCell tdBuy = (HtmlTableCell)e.Item.FindControl("tdBuy");
                HtmlTableCell tdAction = (HtmlTableCell)e.Item.FindControl("tdAction");
                HtmlTableCell tdWatch = (HtmlTableCell)e.Item.FindControl("tdWatch");
                LinkButton  lbSchemeName=(LinkButton)e.Item.FindControl("lbSchemeName");
                Label IsSchemePurchege = (Label)e.Item.FindControl("lblIsPurchase");
                Label IsSchemeSIPType = (Label)e.Item.FindControl("lblIsSIP");
                DropDownList ddlAction = (DropDownList)e.Item.FindControl("ddlAction");
                tdNFOStrtDate.Visible = false;
                tdNFOEndDate.Visible = false;
                tdNFOAmt.Visible = false;
                tdNFOcategory.Visible = false;
                thNFOStrtDate.Visible = false;
                thNFOEndDate.Visible = false;
                thNFOAmt.Visible = false;
                thNFOcategory.Visible = false;
                thReturn.Visible = true;
                thWatch.Visible = true;
                tdWatch.Visible = true;
                thNAV.Visible = true;
                tdNAV.Visible = true;
                tdReturn.Visible = true;
                tdSchemeRank.Visible = false;
                thSchemeRank.Visible = false;
                thSIP.Visible = false;
                tdSIP.Visible = false;
                thBuy.Visible = false;
                tdBuy.Visible = false;
                th1.Visible = true;
                if (!Boolean.Parse(IsSchemePurchege.Text))
                {
                    ddlAction.Items[1].Enabled = false;
                }
                if (!Boolean.Parse(IsSchemeSIPType.Text))
                {
                    ddlAction.Items[2].Enabled = false;
                }
                if (ViewState["FilterType"].ToString() == "lbNFOList")
                {
                    tdNFOStrtDate.Visible = true;
                    tdNFOEndDate.Visible = true;
                    tdNFOAmt.Visible = true;
                    tdNFOcategory.Visible = true;
                    thNFOStrtDate.Visible = true;
                    thNFOEndDate.Visible = true;
                    thNFOAmt.Visible = true;
                    thNFOcategory.Visible = true;
                    thReturn.Visible = false;
                    thNAV.Visible = false;
                    tdNAV.Visible = false;
                    tdReturn.Visible = false;
                    tdSchemeRank.Visible = false;
                    thSchemeRank.Visible = false;
                    thSIP.Visible = false;
                    tdSIP.Visible = false;
                    tdAction.Visible = true;
                    th1.Visible = true;
                    thWatch.Visible = false;
                    tdWatch.Visible = false;
                    lbSchemeName.Enabled = false;
                    if (!Convert.ToBoolean(rblNFOType.SelectedValue)){

                        thBuy.Visible = false;
                        tdBuy.Visible = false;
                        tdAction.Visible = false;
                        th1.Visible = false;
                    }



                }
                else if (ViewState["FilterType"].ToString() == "lbTopSchemes")
                {

                    tdSchemeRank.Visible = true;
                    thSchemeRank.Visible = true;

                }
            }
        }

        protected void BindTopMarketSchemes(string category, Boolean IsSIP, int Returns, int customerId, int PageIndex,int sortOn)
        {
            OnlineOrderBackOfficeBo boOnlineOrderBackOffice = new OnlineOrderBackOfficeBo();
            int recordCount = 0;
            DataTable dtTopMarketSchemes = boOnlineOrderBackOffice.GetTopMarketSchemes(category, IsSIP, Returns, customerId, int.Parse(ddlCompare.SelectedValue), double.Parse(txtcmpvalue.Text), out  recordCount, PageIndex, PageSize,sortOn);
           if (Cache["TopMarketSchemes" + userVo.UserId] != null)
           {
               Cache.Remove("TopMarketSchemes" + userVo.UserId);
           }
           Cache.Insert("TopMarketSchemes" + userVo.UserId, dtTopMarketSchemes);
           rptTopMarketSchemes.DataSource = dtTopMarketSchemes;
           rptTopMarketSchemes.DataBind();
           rptTopMarketSchemes.Visible = true;
           this.PopulatePager(recordCount, PageIndex, rpTopPager);


        }
        protected void lnkMoreNews_lnkMoreNews(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('ProductOnlineFundNews')", true);
        }
        protected void repFundDetails_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                int NewsDetsiisId = int.Parse(e.CommandArgument.ToString());
                if (e.CommandName == "NewsDetailsLnk")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('ProductOnlineFundNews','&NewsDetsiisId=" + NewsDetsiisId + "');", true);
                }
            }
        }


        protected void ddlMarketCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindTopMarketSchemes(ddlMarketCategory.SelectedValue, Boolean.Parse(ddlSIP.SelectedValue), int.Parse(ddlReturns.SelectedValue), customerVo.CustomerId, 1, int.Parse(ddlMarketDataSort.SelectedValue));
        }
        protected void ddlSIP_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindTopMarketSchemes(ddlMarketCategory.SelectedValue, Boolean.Parse(ddlSIP.SelectedValue), int.Parse(ddlReturns.SelectedValue), customerVo.CustomerId, 1, int.Parse(ddlMarketDataSort.SelectedValue));
        }
        protected void ddlReturns_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindTopMarketSchemes(ddlMarketCategory.SelectedValue, Boolean.Parse(ddlSIP.SelectedValue), int.Parse(ddlReturns.SelectedValue), customerVo.CustomerId, 1, int.Parse(ddlMarketDataSort.SelectedValue));
        }
        protected void rptTopMarketSchemes_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                string PASP_SchemePlanCode = e.CommandArgument.ToString();
                DropDownList dropDownList = (DropDownList)e.Item.FindControl("ddlAction");

                switch (e.CommandName)
                {
                    case "Buy":
                        if (Session["PageDefaultSetting"] != null)
                        {
                            Session["MFSchemePlan"] = PASP_SchemePlanCode;
                            if (ViewState["FilterType"].ToString() != "lbNFOList")
                            {
                                LoadMFTransactionPage("MFOrderPurchaseTransType", 2);
                            }
                            else
                            {
                                LoadMFTransactionPage("MFOrderNFOTransType", 2);
                            }

                        }
                        else
                        {
                            if (ViewState["FilterType"].ToString() != "lbNFOList")
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFOrderPurchaseTransType','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFOrderNFOTransType','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);

                            }
                        }
                        break;
                    case "ABY":
                        if (Session["PageDefaultSetting"] != null)
                        {
                            Session["MFSchemePlan"] = PASP_SchemePlanCode;
                            LoadMFTransactionPage("MFOrderAdditionalPurchase", 2);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFOrderAdditionalPurchase','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        break;
                    case "SIP":
                        if (Session["PageDefaultSetting"] != null)
                        {
                            Session["MFSchemePlan"] = PASP_SchemePlanCode;
                            LoadMFTransactionPage("MFOrderSIPTransType", 2);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFOrderSIPTransType','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        break;
                    case "Sell":
                        if (Session["PageDefaultSetting"] != null)
                        {
                            Session["MFSchemePlan"] = PASP_SchemePlanCode;
                            LoadMFTransactionPage("MFOrderRdemptionTransType", 2);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFOrderRdemptionTransType','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        break;
                    case "schemeDetails":

                        if (Session["PageDefaultSetting"] != null)
                        {
                            //Session["MFSchemePlan"] = PASP_SchemePlanCode;
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFSchemeDetails','&schemeCode=" + PASP_SchemePlanCode + "');", true);
                            LoadMFTransactionPage("MFSchemeDetails", 1);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFSchemeDetails','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
                        }
                        break;
                    case "addToWatch":
                        CustomerAddMFSchemeToWatch(int.Parse(e.CommandArgument.ToString()), e);
                        break;

                    case "RemoveFrmWatch": DeleteSchemeFromCustomerWatch(int.Parse(e.CommandArgument.ToString()), e, customerVo.CustomerId);
                        BindSchemeRelatedDetails(int.Parse(hfAMCCode.Value), int.Parse(hfSchemeCode.Value), hfCategory.Value, int.Parse(hfCustomerId.Value), Int16.Parse(hfIsSchemeDetails.Value), Boolean.Parse(hfNFOType.Value), 1, Boolean.Parse(hfIsSIP.Value), int.Parse(hfSortOn.Value));
                        break;



                }
            }

        }
        protected void ddlMarketDataSort_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindTopMarketSchemes(ddlMarketCategory.SelectedValue, Boolean.Parse(ddlSIP.SelectedValue), int.Parse(ddlReturns.SelectedValue), customerVo.CustomerId, 1, int.Parse(ddlMarketDataSort.SelectedValue));
            hfMarketSortOn.Value = ddlMarketDataSort.SelectedValue;
        }
        protected void ddlTopRatedSort_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindSchemeRelatedDetails(int.Parse(hfAMCCode.Value), int.Parse(hfSchemeCode.Value), hfCategory.Value, int.Parse(hfCustomerId.Value), Int16.Parse(hfIsSchemeDetails.Value), Boolean.Parse(hfNFOType.Value), 1, Boolean.Parse(hfIsSIP.Value), int.Parse(ddlTopRatedSort.SelectedValue));
            hfSortOn.Value = ddlTopRatedSort.SelectedValue;
        }
        protected void ddlTopPerformerSort_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void rptTopMarketSchemes_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label IsSchemePurchege = (Label)e.Item.FindControl("lblIsPurchase");
                Label IsSchemeSIPType = (Label)e.Item.FindControl("lblIsSIP");
                DropDownList ddlMarketlAction = (DropDownList)e.Item.FindControl("ddlMarketlAction");
                if (!Boolean.Parse(IsSchemePurchege.Text))
                {
                    ddlMarketlAction.Items[1].Enabled = false;
                }
                if (!Boolean.Parse(IsSchemeSIPType.Text))
                {
                    ddlMarketlAction.Items[2].Enabled = false;
                }

            }
       }
    }
}
