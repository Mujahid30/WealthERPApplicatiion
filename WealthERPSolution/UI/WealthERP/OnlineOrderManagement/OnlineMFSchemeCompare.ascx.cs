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
namespace WealthERP.OnlineOrderManagement
{
    public partial class OnlineMFSchemeCompare : System.Web.UI.UserControl
    {
        OnlineMFSchemeDetailsBo onlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            OnlineUserSessionBo.CheckSession();
            if (!IsPostBack)
            {
                BindAMC1();
                BindCategory();
                if(Request.QueryString["information"]!=null)
                {
                    BindSchemeDetais1();
                }
            } 
        }
        private void BindAMC1()
        {

            PriceBo priceBo = new PriceBo();
            DataTable dtGetAMCList = new DataTable();
            dtGetAMCList = priceBo.GetMutualFundList();
            ddlAMC1.DataSource = dtGetAMCList;
            ddlAMC1.DataTextField = dtGetAMCList.Columns["PA_AMCName"].ToString();
            ddlAMC1.DataValueField = dtGetAMCList.Columns["PA_AMCCode"].ToString();
            ddlAMC1.DataBind();
            ddlAMC1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select AMC", "0"));
            ddlAMC2.DataSource = dtGetAMCList;
            ddlAMC2.DataTextField = dtGetAMCList.Columns["PA_AMCName"].ToString();
            ddlAMC2.DataValueField = dtGetAMCList.Columns["PA_AMCCode"].ToString();
            ddlAMC2.DataBind();
            ddlAMC2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select AMC", "0"));
            ddlAMC3.DataSource = dtGetAMCList;
            ddlAMC3.DataTextField = dtGetAMCList.Columns["PA_AMCName"].ToString();
            ddlAMC3.DataValueField = dtGetAMCList.Columns["PA_AMCCode"].ToString();
            ddlAMC3.DataBind();
            ddlAMC3.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select AMC", "0"));
            ddlAMC4.DataSource = dtGetAMCList;
            ddlAMC4.DataTextField = dtGetAMCList.Columns["PA_AMCName"].ToString();
            ddlAMC4.DataValueField = dtGetAMCList.Columns["PA_AMCCode"].ToString();
            ddlAMC4.DataBind();
            ddlAMC4.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select AMC", "0"));

        }
        protected void ddlAMC1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMC1.SelectedIndex != 0)
            {
                BindScheme();
            }
        }
        protected void BindScheme()
        {
            DataTable dt;
            ProductMFBo productMFBo = new ProductMFBo();
            if (ddlAMC1.SelectedValue != "0")
            {
                dt = productMFBo.GetSchemePlanName(int.Parse(ddlAMC1.SelectedValue));
                ddlSchemeList1.DataSource = dt;
                ddlSchemeList1.DataValueField = "PASP_SchemePlanCode";
                ddlSchemeList1.DataTextField = "PASP_SchemePlanName";
                ddlSchemeList1.DataBind();
                ddlSchemeList1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Scheme", "0"));
            }
        }
        private void BindCategory()
        {
            DataSet dsProductAssetCategory;
            ProductMFBo productMFBo = new ProductMFBo();
            dsProductAssetCategory = productMFBo.GetProductAssetCategory();
            DataTable dtCategory = dsProductAssetCategory.Tables[0];
            ddlCategory1.DataSource = dtCategory;
            ddlCategory1.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory1.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory1.DataBind();
            ddlCategory1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            ddlCategory2.DataSource = dtCategory;
            ddlCategory2.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory2.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory2.DataBind();
            ddlCategory2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            ddlCategory3.DataSource = dtCategory;
            ddlCategory3.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory3.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory3.DataBind();
            ddlCategory3.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            ddlCategory4.DataSource = dtCategory;
            ddlCategory4.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory4.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory4.DataBind();
            ddlCategory4.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }
        protected void ddlAMC2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMC2.SelectedIndex != 0)
            {
                BindScheme2();
            }
        }
        protected void ddlAMC3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMC3.SelectedIndex != 0)
            {
                BindScheme3();
            }
        }
        protected void ddlAMC4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMC4.SelectedIndex != 0)
            {
                BindScheme4();
            }
        }
        protected void BindScheme2()
        {
            DataTable dt;
            ProductMFBo productMFBo = new ProductMFBo();
            if (ddlAMC2.SelectedValue != "0")
            {
                dt = productMFBo.GetSchemePlanName(int.Parse(ddlAMC2.SelectedValue));
                ddlSchemeList2.DataSource = dt;
                ddlSchemeList2.DataValueField = "PASP_SchemePlanCode";
                ddlSchemeList2.DataTextField = "PASP_SchemePlanName";
                ddlSchemeList2.DataBind();
                ddlSchemeList2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Scheme", "0"));
            }
        }
        protected void BindScheme3()
        {
            DataTable dt;
            ProductMFBo productMFBo = new ProductMFBo();
            if (ddlAMC3.SelectedValue != "0")
            {
                dt = productMFBo.GetSchemePlanName(int.Parse(ddlAMC3.SelectedValue));
                ddlSchemeList3.DataSource = dt;
                ddlSchemeList3.DataValueField = "PASP_SchemePlanCode";
                ddlSchemeList3.DataTextField = "PASP_SchemePlanName";
                ddlSchemeList3.DataBind();
                ddlSchemeList3.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Scheme", "0"));
            }
        }
        protected void BindScheme4()
        {
            DataTable dt;
            ProductMFBo productMFBo = new ProductMFBo();
            if (ddlAMC4.SelectedValue != "0")
            {
                dt = productMFBo.GetSchemePlanName(int.Parse(ddlAMC4.SelectedValue));
                ddlSchemeList4.DataSource = dt;
                ddlSchemeList4.DataValueField = "PASP_SchemePlanCode";
                ddlSchemeList4.DataTextField = "PASP_SchemePlanName";
                ddlSchemeList4.DataBind();
                ddlSchemeList4.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Scheme", "0"));
            }
        }
        protected void ddlSchemeList1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchemeList1.SelectedValue != "")
            {
                BindSchemeDetais1(); 
            }
        }
        protected void BindSchemeDetais1()
        {
            ddlAMC1.Visible = false;
            ddlCategory1.Visible = false;
            ddlSchemeList1.Visible = false;
            lnkDelete1.Visible = true;
            if(Session["Schemedetails"] ==null)
            Session["Schemedetails"] = onlineMFSchemeDetailsBo.GetSchemeDetails(int.Parse(ddlAMC1.SelectedValue), int.Parse(ddlSchemeList1.SelectedValue), ddlCategory1.SelectedValue);
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
            lblMinInvestment.Text = mfSchemeDetails.minmumInvestmentAmount.ToString();
            lblMinMultipleOf.Text = mfSchemeDetails.multipleOf.ToString();
            if (mfSchemeDetails.mornigStar > 0)
            {
                imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + mfSchemeDetails.mornigStar + ".png";
            }
            else
            {
                imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/0.png";

            }
        }
        protected void ddlSchemeList2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchemeList2.SelectedValue != "")
            {

                ddlAMC2.Visible = false;
                ddlCategory2.Visible = false;
                ddlSchemeList2.Visible = false;
                lnkDelete2.Visible = true;
                Session["Schemedetails1"] = onlineMFSchemeDetailsBo.GetSchemeDetails(int.Parse(ddlAMC2.SelectedValue), int.Parse(ddlSchemeList2.SelectedValue), ddlCategory2.SelectedValue);
                OnlineMFSchemeDetailsVo mfSchemeDetails1 = (OnlineMFSchemeDetailsVo)Session["Schemedetails1"];
                lblSchemeName1.Text = mfSchemeDetails1.schemeName;
                lblAMC1.Text = mfSchemeDetails1.schemeName;
                lblNAV1.Text = mfSchemeDetails1.NAV.ToString();
                lblNAVDate1.Text = mfSchemeDetails1.navDate.ToString();
                lblCategory1.Text = mfSchemeDetails1.category;
                lblBanchMark1.Text = mfSchemeDetails1.schemeBanchMark;
                lblFundManager1.Text = mfSchemeDetails1.fundManager;
                lblFundReturn1styear1.Text = mfSchemeDetails1.fundReturn3rdYear.ToString();
                lblFundReturn3rdyear1.Text = mfSchemeDetails1.fundReturn5thtYear.ToString();
                lblFundReturn5thyear1.Text = mfSchemeDetails1.fundReturn10thYear.ToString();
                lblBenchmarkReturn1.Text = mfSchemeDetails1.benchmarkReturn1stYear;
                lblBenchMarkReturn3rd1.Text = mfSchemeDetails1.benchmark3rhYear;
                lblBenchMarkReturn5th1.Text = mfSchemeDetails1.benchmark5thdYear;
                lblMinSIP1.Text = mfSchemeDetails1.minSIPInvestment.ToString();
                lblSIPMultipleOf1.Text = mfSchemeDetails1.SIPmultipleOf.ToString();
                lblExitLoad1.Text = mfSchemeDetails1.exitLoad.ToString();
                lblMinInvestment1.Text = mfSchemeDetails1.minmumInvestmentAmount.ToString();
                lblMinMultipleOf1.Text = mfSchemeDetails1.multipleOf.ToString();
                if (mfSchemeDetails1.mornigStar > 0)
                {
                    imgSchemeRating1.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + mfSchemeDetails1.mornigStar + ".png";
                }
                else
                {
                    imgSchemeRating1.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/0.png";

                }
            }
        }
        protected void ddlSchemeList3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchemeList3.SelectedValue != "")
            {

                ddlAMC3.Visible = false;
                ddlCategory3.Visible = false;
                ddlSchemeList3.Visible = false;
                lnkDelete3.Visible = true;
                Session["Schemedetails2"] = onlineMFSchemeDetailsBo.GetSchemeDetails(int.Parse(ddlAMC3.SelectedValue), int.Parse(ddlSchemeList3.SelectedValue), ddlCategory3.SelectedValue);
                OnlineMFSchemeDetailsVo mfSchemeDetails2 = (OnlineMFSchemeDetailsVo)Session["Schemedetails2"];
                lblSchemeName2.Text = mfSchemeDetails2.schemeName;
                lblAMC2.Text = mfSchemeDetails2.schemeName;
                lblNAV2.Text = mfSchemeDetails2.NAV.ToString();
                lblNAVDate2.Text = mfSchemeDetails2.navDate.ToString();
                lblCategory2.Text = mfSchemeDetails2.category;
                lblBanchMark2.Text = mfSchemeDetails2.schemeBanchMark;
                lblFundManager2.Text = mfSchemeDetails2.fundManager;
                lblFundReturn1styear2.Text = mfSchemeDetails2.fundReturn3rdYear.ToString();
                lblFundReturn3rdyear2.Text = mfSchemeDetails2.fundReturn5thtYear.ToString();
                lblFundReturn5thyear2.Text = mfSchemeDetails2.fundReturn10thYear.ToString();
                lblBenchmarkReturn2.Text = mfSchemeDetails2.benchmarkReturn1stYear;
                lblBenchMarkReturn3rd2.Text = mfSchemeDetails2.benchmark3rhYear;
                lblBenchMarkReturn5th2.Text = mfSchemeDetails2.benchmark5thdYear;
                lblMinSIP2.Text = mfSchemeDetails2.minSIPInvestment.ToString();
                lblSIPMultipleOf2.Text = mfSchemeDetails2.SIPmultipleOf.ToString();
                lblExitLoad2.Text = mfSchemeDetails2.exitLoad.ToString();
                lblMinInvestment2.Text = mfSchemeDetails2.minmumInvestmentAmount.ToString();
                lblMinMultipleOf2.Text = mfSchemeDetails2.multipleOf.ToString();
                if (mfSchemeDetails2.mornigStar > 0)
                {
                    imgSchemeRating2.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + mfSchemeDetails2.mornigStar + ".png";
                }
                else
                {
                    imgSchemeRating2.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/0.png";

                }
            }
        }
        protected void ddlSchemeList4_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchemeList4.SelectedValue != "")
            {

                ddlAMC4.Visible = false;
                ddlCategory4.Visible = false;
                ddlSchemeList4.Visible = false;
                lnkDelete4.Visible = true;
                Session["Schemedetails3"] = onlineMFSchemeDetailsBo.GetSchemeDetails(int.Parse(ddlAMC4.SelectedValue), int.Parse(ddlSchemeList4.SelectedValue), ddlCategory4.SelectedValue);
                OnlineMFSchemeDetailsVo mfSchemeDetails3 = (OnlineMFSchemeDetailsVo)Session["Schemedetails3"];
                lblSchemeName3.Text = mfSchemeDetails3.schemeName;
                lblAMC3.Text = mfSchemeDetails3.schemeName;
                lblNAV3.Text = mfSchemeDetails3.NAV.ToString();
                lblNAVDate3.Text = mfSchemeDetails3.navDate.ToString();
                lblCategory3.Text = mfSchemeDetails3.category;
                lblBanchMark3.Text = mfSchemeDetails3.schemeBanchMark;
                lblFundManager3.Text = mfSchemeDetails3.fundManager;
                lblFundReturn1styear3.Text = mfSchemeDetails3.fundReturn3rdYear.ToString();
                lblFundReturn3rdyear3.Text = mfSchemeDetails3.fundReturn5thtYear.ToString();
                lblFundReturn5thyear3.Text = mfSchemeDetails3.fundReturn10thYear.ToString();
                lblBenchmarkReturn3.Text = mfSchemeDetails3.benchmarkReturn1stYear;
                lblBenchMarkReturn3rd3.Text = mfSchemeDetails3.benchmark3rhYear;
                lblBenchMarkReturn5th3.Text = mfSchemeDetails3.benchmark5thdYear;
                lblMinSIP3.Text = mfSchemeDetails3.minSIPInvestment.ToString();
                lblSIPMultipleOf3.Text = mfSchemeDetails3.SIPmultipleOf.ToString();
                lblExitLoad3.Text = mfSchemeDetails3.exitLoad.ToString();
                lblMinInvestment3.Text = mfSchemeDetails3.minmumInvestmentAmount.ToString();
                lblMinMultipleOf3.Text = mfSchemeDetails3.multipleOf.ToString();
                if (mfSchemeDetails3.mornigStar > 0)
                {
                    imgSchemeRating3.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + mfSchemeDetails3.mornigStar + ".png";
                }
                else
                {
                    imgSchemeRating3.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/0.png";

                }
            }
        }
        protected void lnkDelete1_OnClick(object sender, EventArgs e)
        {
            ddlAMC1.Visible = true;
            ddlCategory1.Visible = true;
            ddlSchemeList1.Visible = true;
            lnkDelete1.Visible = false;
        }
        protected void lnkDelete2_OnClick(object sender, EventArgs e)
        {
            ddlAMC2.Visible = true;
            ddlCategory2.Visible = true;
           ddlSchemeList2.Visible = true;
            lnkDelete2.Visible = false;
        }
        protected void lnkDelete31_OnClick(object sender, EventArgs e)
        {
            ddlAMC3.Visible = true;
            ddlCategory3.Visible = true;
            ddlSchemeList3.Visible = true;
            lnkDelete3.Visible = false;
        }
        protected void lnkDelete4_OnClick(object sender, EventArgs e)
        {
            ddlAMC4.Visible = true;
            ddlCategory4.Visible = true;
            ddlSchemeList4.Visible = true;
            lnkDelete4.Visible = false;
        }
    }
}