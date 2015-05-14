using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoWerpAdmin;
using BoProductMaster;
using System.Data;
using VoOnlineOrderManagemnet;
using BoOnlineOrderManagement;
using BoCommon;
using VoUser;
using VoCustomerPortfolio;
using System.Web.Services;
using System.Web.Script.Services;
using System.Net;
using System.IO;
using System.Configuration;
namespace WealthERP.OnlineOrderManagement
{
    public partial class MFSchemeDetails : System.Web.UI.UserControl
    {
        OnlineMFSchemeDetailsBo onlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
        CustomerVo customerVo = new CustomerVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            OnlineUserSessionBo.CheckSession();
            customerVo = (CustomerVo)Session["CustomerVo"];
            if (!IsPostBack)
            {
                BindAMC();
            }
        }
        protected void ddlAMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMC.SelectedIndex != 0)
            {
                BindScheme();
                BindCategory();
                BindfundManagerDetails();
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
            ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
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
            }
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
        protected void Go_OnClick(object sender, EventArgs e)
        {
            GetAmcSchemeDetails();
        }
        public void GetAmcSchemeDetails()
        {
            Session["Schemedetails"] = onlineMFSchemeDetailsBo.GetSchemeDetails(int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), ddlCategory.SelectedValue);
            OnlineMFSchemeDetailsVo mfSchemeDetails = (OnlineMFSchemeDetailsVo)Session["Schemedetails"];
            lblSchemeName.Text = mfSchemeDetails.schemeName;
            lblAMC.Text = mfSchemeDetails.schemeName;
            lblNAV.Text = mfSchemeDetails.NAV.ToString();
            lblNAVDate.Text = mfSchemeDetails.navDate.ToString();
            lblCategory.Text = mfSchemeDetails.category;
            lblBanchMark.Text = mfSchemeDetails.schemeBanchMark;
            lblFundManager.Text = mfSchemeDetails.fundManager;
            lblFundReturn1styear.Text = mfSchemeDetails.fundReturn3rdYear.ToString();
            lblFundReturn3rdyear.Text = mfSchemeDetails.fundReturn5thtYear.ToString();
            lblFundReturn5thyear.Text = mfSchemeDetails.fundReturn10thYear.ToString();
            lblBenchmarkReturn.Text = mfSchemeDetails.benchmarkReturn1stYear;
            lblBenchMarkReturn3rd.Text = mfSchemeDetails.benchmark3rhYear;
            lblBenchMarkReturn5th.Text = mfSchemeDetails.benchmark5thdYear;
            lblMinSIP.Text = mfSchemeDetails.minSIPInvestment.ToString();
            lblSIPMultipleOf.Text = mfSchemeDetails.SIPmultipleOf.ToString();
            lblExitLoad.Text = mfSchemeDetails.exitLoad.ToString();
            if (mfSchemeDetails.mornigStar > 0)
            {
                imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + 4 + ".png";
                imgStyleBox.ImageUrl = @"../Images/MorningStarRating/StarStyleBox/" + 7 + ".png";
            }
            else
            {
                imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/0.png";

            }
        }
        protected void lbBuy_OnClick(object sender, EventArgs e)
        {
            if (Session["PageDefaultSetting"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderAdditionalPurchase','&Amc=" + ddlAMC.SelectedValue + "&SchemeCode=" + ddlScheme.SelectedValue + "&category=" + ddlCategory.SelectedValue + "');", true);

            }
            else
            {
                Response.Redirect("ControlHost.aspx?pageid=MFOrderAdditionalPurchase&Amc=" + ddlAMC.SelectedValue + "&SchemeCode=" + ddlScheme.SelectedValue + "&category=" + ddlCategory.SelectedValue + "", false);

            }
        }
        protected void lbAddPurchase_OnClick(object sender, EventArgs e)
        {
            if (Session["PageDefaultSetting"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderAdditionalPurchase','&Amc=" + ddlAMC.SelectedValue + "&SchemeCode=" + ddlScheme.SelectedValue + "&category=" + ddlCategory.SelectedValue + "');", true);

            }
            else
            {
                Response.Redirect("ControlHost.aspx?pageid=MFOrderAdditionalPurchase&Amc=" + ddlAMC.SelectedValue + "&SchemeCode=" + ddlScheme.SelectedValue + "&category=" + ddlCategory.SelectedValue + "", false);

            }
        }
        protected void lbSIP_OnClick(object sender, EventArgs e)
        {
            if (Session["PageDefaultSetting"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderSIPTransType','&Amc=" + ddlAMC.SelectedValue + "&SchemeCode=" + ddlScheme.SelectedValue + "&category=" + ddlCategory.SelectedValue + "');", true);

            }
            else
            {
                Response.Redirect("ControlHost.aspx?pageid=MFOrderSIPTransType&Amc=" + ddlAMC.SelectedValue + "&SchemeCode=" + ddlScheme.SelectedValue + "&category=" + ddlCategory.SelectedValue + "", false);

            }
        }
        protected void lbRedem_OnClick(object sender, EventArgs e)
        {
            if (Session["PageDefaultSetting"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderRdemptionTransType','&Amc=" + ddlAMC.SelectedValue + "&SchemeCode=" + ddlScheme.SelectedValue + "&category=" + ddlCategory.SelectedValue + "');", true);

            }
            else
            {
                Response.Redirect("ControlHost.aspx?pageid=MFOrderRdemptionTransType&Amc=" + ddlAMC.SelectedValue + "&SchemeCode=" + ddlScheme.SelectedValue + "&category=" + ddlCategory.SelectedValue + "", false);

            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        //public object GetData()
        //{
        protected void BindfundManagerDetails()
        {
            string cmotcode=onlineMFSchemeDetailsBo.GetCmotCode(int.Parse(ddlScheme.SelectedValue));
            string result;
            string FundManagerDetais =ConfigurationSettings.AppSettings["FUND_MANAGER_DETAILS"] + cmotcode + "/Pre";
            WebResponse response;
            WebRequest request = HttpWebRequest.Create(FundManagerDetais);
            response = request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
                reader.Close();
            }
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            StringReader theReader = new StringReader(result);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);
            foreach (DataRow dr in theDataSet.Tables[1].Rows)
            {
                lblFundMAnagername.Text = dr["FundManager"].ToString();
                lblQualification.Text = dr["Qualification"].ToString();
                lblDesignation.Text = dr["Designation"].ToString();
                lblExperience.Text = dr["experience"].ToString();
            }
        }
    }
}