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
namespace WealthERP.OnlineOrderManagement
{
    public partial class MFSchemeRelateInformation : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        CustomerVo customerVo = new CustomerVo();
        string path;
        int PageSize = 10;
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
            }
        }
        protected void ddlAMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMC.SelectedIndex != 0)
            {
                //BindScheme();
            }
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
            ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Category", "0"));
        }
        protected void BindScheme()
        {
            DataTable dt;
            OnlineMFSchemeDetailsBo OnlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
            dt = OnlineMFSchemeDetailsBo.GetAMCandCategoryWiseScheme(int.Parse(ddlAMC.SelectedValue), ddlCategory.SelectedValue);
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
            ViewState["FilterType"] = "Schemes Details";
            hfAMCCode.Value = ddlAMC.SelectedValue;
            hfSchemeCode.Value = ddlScheme.SelectedValue;
            hfCategory.Value = ddlCategory.SelectedValue;
            hfCustomerId.Value = customerVo.CustomerId.ToString();
            hfIsSchemeDetails.Value = "1";
            hfNFOType.Value = "false";
            ddlNFOType.Visible = false;
            BindSchemeRelatedDetails(int.Parse(hfAMCCode.Value), int.Parse(hfSchemeCode.Value), hfCategory.Value, int.Parse(hfCustomerId.Value), Int16.Parse(hfIsSchemeDetails.Value), Boolean.Parse(hfNFOType.Value), 1);
            dvHeading.Visible = true;
            lblHeading.Text = "Schemes Details";

        }
        protected void BindSchemeRelatedDetails(int amcCode, int SchemeCode, string category, int customerId, Int16 isSchemeDetails, Boolean NFOType, int pageIndex)
        {
            OnlineOrderBackOfficeBo bo = new OnlineOrderBackOfficeBo();
            dvSchemeDetails.Visible = true;
            int recordCount = 0;
            DataTable dtBindSchemeRelatedDetails = bo.GetSchemeDetails(amcCode, SchemeCode, category, customerId, isSchemeDetails, NFOType, out recordCount, pageIndex, PageSize);
            if (Cache["BindSchemeRelatedDetails" + userVo.UserId] != null)
            {
                Cache.Remove("BindSchemeRelatedDetails" + userVo.UserId);
            }
            Cache.Insert("BindSchemeRelatedDetails" + userVo.UserId, dtBindSchemeRelatedDetails);
            rpSchemeDetails.DataSource = dtBindSchemeRelatedDetails;
            rpSchemeDetails.DataBind();
            rpSchemeDetails.Visible = true;
            this.PopulatePager(recordCount, 1);


        }
        private void PopulatePager(int recordCount, int currentPage)
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
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }
        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            BindSchemeRelatedDetails(int.Parse(hfAMCCode.Value), int.Parse(hfSchemeCode.Value), hfCategory.Value, int.Parse(hfCustomerId.Value), Int16.Parse(hfIsSchemeDetails.Value), Boolean.Parse(hfNFOType.Value),pageIndex);

        }

        protected void GetSchemeDetails(object sender, EventArgs e)
        {
            LinkButton lk = (LinkButton)sender;
            ViewState["FilterType"] = lk.ID.ToString();
            ddlNFOType.Visible = false;
            lbViewWatchList.ForeColor = System.Drawing.Color.Blue;
            lbNFOList.ForeColor = System.Drawing.Color.Blue;
            lbTopSchemes.ForeColor = System.Drawing.Color.Blue;
            hfCustomerId.Value = customerVo.CustomerId.ToString();
            switch (lk.ID.ToString())
            {
                case "lbViewWatchList": hfAMCCode.Value = "0";
                    hfSchemeCode.Value = "0";
                    hfCategory.Value = "0";
                    hfIsSchemeDetails.Value = "0";
                    hfNFOType.Value = ddlNFOType.SelectedValue;
                    dvHeading.Visible = true;
                    lblHeading.Text = "My Watch List";
                    lbViewWatchList.ForeColor = System.Drawing.Color.Black;
                    break;
                case "lbNFOList":
                    hfAMCCode.Value = "0";
                    hfSchemeCode.Value = "0";
                    hfCategory.Value = "0";
                    hfIsSchemeDetails.Value = "3";
                    hfNFOType.Value = ddlNFOType.SelectedValue;
                    dvHeading.Visible = true;
                    lblHeading.Text = "NFO Scheme";
                    ddlNFOType.Visible = true;
                    lbNFOList.ForeColor = System.Drawing.Color.Black;
                    break;
                case "lbTopSchemes":
                    hfAMCCode.Value = "0";
                    hfSchemeCode.Value = "0";
                    hfCategory.Value = "0";
                    hfIsSchemeDetails.Value = "2";
                    hfNFOType.Value = "false";
                    dvHeading.Visible = true;
                    lblHeading.Text = "Top Ten Schemes";
                    lbTopSchemes.ForeColor = System.Drawing.Color.Black;

                    break;
                case "lbSchemeDetails": dvDemo.Visible = true;
                    dvHeading.Visible = true;
                    lblHeading.Text = "Schemes Details";

                    break;

            }
            BindSchemeRelatedDetails(int.Parse(hfAMCCode.Value), int.Parse(hfSchemeCode.Value), hfCategory.Value, int.Parse(hfCustomerId.Value), Int16.Parse(hfIsSchemeDetails.Value), Boolean.Parse(hfNFOType.Value),1);
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
                            Session["MFSchemePlan"] = PASP_SchemePlanCode;
                            LoadMFTransactionPage("MFOrderPurchaseTransType", 2);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('MFOrderPurchaseTransType','SchemeCode=" + PASP_SchemePlanCode + "&AMCode=" + ddlAMC.SelectedValue + "&Category=" + ddlCategory.SelectedValue + "');", true);
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
                            Session["MFSchemePlan"] = PASP_SchemePlanCode;
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

        protected void ddlNFOType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindSchemeRelatedDetails(0, 0, "0", 0, 3, Boolean.Parse(ddlNFOType.SelectedValue),1);
            hfNFOType.Value = ddlNFOType.SelectedValue.ToString(); 
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
                tdNFOStrtDate.Visible = false;
                tdNFOEndDate.Visible = false;
                tdNFOAmt.Visible = false;
                tdNFOcategory.Visible = false;
                thNFOStrtDate.Visible = false;
                thNFOEndDate.Visible = false;
                thNFOAmt.Visible = false;
                thNFOcategory.Visible = false;
                thReturn.Visible = true;
                thNAV.Visible = true;
                tdNAV.Visible = true;
                tdReturn.Visible = true;
                tdSchemeRank.Visible = false;
                thSchemeRank.Visible = false;
                thSIP.Visible = true;
                tdSIP.Visible = true;
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



                }
                else if (ViewState["FilterType"].ToString() == "lbTopSchemes")
                {

                    tdSchemeRank.Visible = true;
                    thSchemeRank.Visible = true;

                }
            }
        }
    }
}
