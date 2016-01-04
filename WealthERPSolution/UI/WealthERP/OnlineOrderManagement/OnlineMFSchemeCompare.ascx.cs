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
        List<int> schemeCompareList = new List<int>();
        string str = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            OnlineUserSessionBo.CheckSession();
            if (!IsPostBack)
            {
                BindAMC1();
                BindCategory();
                if (Request.QueryString["schemeCompareList"] != null)
                {
                    //schemeCompareList = (List<int>)Session["SchemeCompareList"];
                    GetSchemeCompareList(Getschemecode((List<int>)Session["SchemeCompareList"]), 0);
                }
            }
        }
        protected string Getschemecode(List<int> schemeList)
        {
            string CompareSchemeList = string.Empty;
            foreach (var item in schemeList)
                CompareSchemeList = CompareSchemeList + item + ",";
            return CompareSchemeList.TrimEnd(',');
        }
        protected void GetSchemeCompareList(string CompareScheme, int schemeColNo)
        {
            List<OnlineMFSchemeDetailsVo> OnlineMFSchemeDetailslist = new List<OnlineMFSchemeDetailsVo>();
            OnlineMFSchemeDetailslist = onlineMFSchemeDetailsBo.GetCompareMFSchemeDetails(CompareScheme);
            if (schemeColNo == 0)
            {
                //Session["SchemeCompareList"] = null;
                int schmeCompareNum = 1;
                foreach (OnlineMFSchemeDetailsVo onlineMFSchemeDetailsVo in OnlineMFSchemeDetailslist)
                {
                    BindSchemeDetais(onlineMFSchemeDetailsVo, schmeCompareNum);
                    schmeCompareNum += 1;
                }
            }
            else
            {
                if (OnlineMFSchemeDetailslist.Count > 0)
                {
                    OnlineMFSchemeDetailsVo onlineMFSchemeDetailsVo = OnlineMFSchemeDetailslist[0];
                    BindSchemeDetais(onlineMFSchemeDetailsVo, schemeColNo);
                }
            }
        }
        private void BindAMC1()
        {


            DataTable dtGetAMCList = new DataTable();
            CommonLookupBo commonLookupBo = new CommonLookupBo();
            dtGetAMCList = commonLookupBo.GetProdAmc(0, true);
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
            BindCategory();
        }
        protected void ddlCategory1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindScheme();
        }
        protected void ddlCategory2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindScheme2();
        }
        protected void ddlCategory3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindScheme3();
        }
        protected void ddlCategory4_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindScheme4();
        }
        protected void BindScheme()
        {
            DataTable dt;
            OnlineMFSchemeDetailsBo OnlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
            dt = OnlineMFSchemeDetailsBo.GetAMCandCategoryWiseScheme(int.Parse(ddlAMC1.SelectedValue), ddlCategory1.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                ddlSchemeList1.DataSource = dt;
                ddlSchemeList1.DataValueField = "PASP_SchemePlanCode";
                ddlSchemeList1.DataTextField = "PASP_SchemePlanName";
                ddlSchemeList1.DataBind();
                ddlSchemeList1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
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
            ddlCategory1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            ddlCategory2.DataSource = dtCategory;
            ddlCategory2.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory2.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory2.DataBind();
            ddlCategory2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            ddlCategory3.DataSource = dtCategory;
            ddlCategory3.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory3.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory3.DataBind();
            ddlCategory3.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            ddlCategory4.DataSource = dtCategory;
            ddlCategory4.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory4.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory4.DataBind();
            ddlCategory4.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }
        protected void ddlAMC2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCategory();
            //}
        }
        protected void ddlAMC3_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCategory();
            //}
        }
        protected void ddlAMC4_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCategory();
            ////}
        }
        protected void BindScheme2()
        {
            DataTable dt;
            OnlineMFSchemeDetailsBo OnlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
            dt = OnlineMFSchemeDetailsBo.GetAMCandCategoryWiseScheme(int.Parse(ddlAMC2.SelectedValue), ddlCategory2.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                ddlSchemeList2.DataSource = dt;
                ddlSchemeList2.DataValueField = "PASP_SchemePlanCode";
                ddlSchemeList2.DataTextField = "PASP_SchemePlanName";
                ddlSchemeList2.DataBind();
                ddlSchemeList2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
        }
        protected void BindScheme3()
        {
            DataTable dt;
            OnlineMFSchemeDetailsBo OnlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
            dt = OnlineMFSchemeDetailsBo.GetAMCandCategoryWiseScheme(int.Parse(ddlAMC3.SelectedValue), ddlCategory3.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                ddlSchemeList3.DataSource = dt;
                ddlSchemeList3.DataValueField = "PASP_SchemePlanCode";
                ddlSchemeList3.DataTextField = "PASP_SchemePlanName";
                ddlSchemeList3.DataBind();
                ddlSchemeList3.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
        }
        protected void BindScheme4()
        {
            DataTable dt;
            OnlineMFSchemeDetailsBo OnlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
            dt = OnlineMFSchemeDetailsBo.GetAMCandCategoryWiseScheme(int.Parse(ddlAMC4.SelectedValue), ddlCategory4.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                ddlSchemeList4.DataSource = dt;
                ddlSchemeList4.DataValueField = "PASP_SchemePlanCode";
                ddlSchemeList4.DataTextField = "PASP_SchemePlanName";
                ddlSchemeList4.DataBind();
                ddlSchemeList4.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
        }
        protected void ddlSchemeList1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchemeList1.SelectedValue != "")
            {
                GetSchemeCompareList(ddlSchemeList1.SelectedValue, 1);
            }
        }

        protected void ddlSchemeList2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchemeList2.SelectedValue != "")
            {
                GetSchemeCompareList(ddlSchemeList2.SelectedValue, 2);
            }
        }
        protected void ddlSchemeList3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchemeList3.SelectedValue != "")
            {
                GetSchemeCompareList(ddlSchemeList3.SelectedValue, 3);
            }
        }
        protected void ddlSchemeList4_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchemeList4.SelectedValue != "")
            {
                GetSchemeCompareList(ddlSchemeList4.SelectedValue, 4);
            }
        }
        protected void BindSchemeDetais(OnlineMFSchemeDetailsVo onlineMFSchemeDetailsVo, int schmeCompareNum)
        {
            switch (schmeCompareNum)
            {
                case 1:
                    ddlAMC1.Visible = false;
                    ddlCategory1.Visible = false;
                    ddlSchemeList1.Visible = false;
                    lnkDelete1.Visible = true;
                    ImgStyle1.Visible = true;
                    lblRating1.Visible = true;
                    lblSchemeName.Text = onlineMFSchemeDetailsVo.schemeName;
                    lblNAV.Text = onlineMFSchemeDetailsVo.NAV.ToString();
                    lblNAVDate.Text = onlineMFSchemeDetailsVo.navDate.ToString();
                    lblCategory.Text = onlineMFSchemeDetailsVo.category;
                    lblBanchMark.Text = onlineMFSchemeDetailsVo.schemeBanchMark;
                    lblFundManager.Text = onlineMFSchemeDetailsVo.fundManager;
                    lblFundReturn1styear.Text = onlineMFSchemeDetailsVo.SchemeReturn3Year.ToString();
                    lblFundReturn3rdyear.Text = onlineMFSchemeDetailsVo.SchemeReturn5Year.ToString();
                    lblFundReturn5thyear.Text = onlineMFSchemeDetailsVo.SchemeReturn10Year.ToString();
                    lblBenchmarkReturn.Text = onlineMFSchemeDetailsVo.benchmarkReturn1stYear;
                    lblBenchMarkReturn3rd.Text = onlineMFSchemeDetailsVo.benchmark3rhYear;
                    lblBenchMarkReturn5th.Text = onlineMFSchemeDetailsVo.benchmark5thdYear;
                    lblMinSIP.Text = onlineMFSchemeDetailsVo.minSIPInvestment.ToString();
                    lblSIPMultipleOf.Text = onlineMFSchemeDetailsVo.SIPmultipleOf.ToString();
                    lblExitLoad.Text = onlineMFSchemeDetailsVo.exitLoad.ToString();
                    lblMinInvestment.Text = onlineMFSchemeDetailsVo.minmumInvestmentAmount.ToString();
                    lblMinMultipleOf.Text = onlineMFSchemeDetailsVo.multipleOf.ToString();
                    if(onlineMFSchemeDetailsVo.isPurchaseAvaliable !=0)
                                       if (onlineMFSchemeDetailsVo.isSIPAvaliable != 0)
                  
                    if (onlineMFSchemeDetailsVo.mornigStar > 0)
                    {
                        imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + onlineMFSchemeDetailsVo.mornigStar + ".png";
                        ImgStyle1.ImageUrl = @"../Images/MorningStarRating/PMFRD_RatingOverall/" + onlineMFSchemeDetailsVo.mornigStar + ".png";
                    }
                    else
                    {
                        imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/0.png";
                        ImgStyle1.ImageUrl = @"../Images/MorningStarRating/PMFRD_RatingOverall/0.png";

                    }
                    break;
                case 2:
                    ddlAMC2.Visible = false;
                    ddlCategory2.Visible = false;
                    ddlSchemeList2.Visible = false;
                    lnkDelete2.Visible = true;
                    ImgStyle2.Visible = true;
                    lblRating1.Visible = true;

                    lblSchemeName1.Text = onlineMFSchemeDetailsVo.schemeName;
                    lblNAV1.Text = onlineMFSchemeDetailsVo.NAV.ToString();
                    lblNAVDate1.Text = onlineMFSchemeDetailsVo.navDate.ToString();
                    lblCategory1.Text = onlineMFSchemeDetailsVo.category;
                    lblBanchMark1.Text = onlineMFSchemeDetailsVo.schemeBanchMark;
                    lblFundManager1.Text = onlineMFSchemeDetailsVo.fundManager;
                    lblFundReturn1styear1.Text = onlineMFSchemeDetailsVo.SchemeReturn3Year.ToString();
                    lblFundReturn3rdyear1.Text = onlineMFSchemeDetailsVo.SchemeReturn5Year.ToString();
                    lblFundReturn5thyear1.Text = onlineMFSchemeDetailsVo.SchemeReturn10Year.ToString();
                    lblBenchmarkReturn1.Text = onlineMFSchemeDetailsVo.benchmarkReturn1stYear;
                    lblBenchMarkReturn3rd1.Text = onlineMFSchemeDetailsVo.benchmark3rhYear;
                    lblBenchMarkReturn5th1.Text = onlineMFSchemeDetailsVo.benchmark5thdYear;
                    lblMinSIP1.Text = onlineMFSchemeDetailsVo.minSIPInvestment.ToString();
                    lblSIPMultipleOf1.Text = onlineMFSchemeDetailsVo.SIPmultipleOf.ToString();
                    lblExitLoad1.Text = onlineMFSchemeDetailsVo.exitLoad.ToString();
                    lblMinInvestment1.Text = onlineMFSchemeDetailsVo.minmumInvestmentAmount.ToString();
                    lblMinMultipleOf1.Text = onlineMFSchemeDetailsVo.multipleOf.ToString();
                    if (onlineMFSchemeDetailsVo.isPurchaseAvaliable != 0)
                       
                    if (onlineMFSchemeDetailsVo.isSIPAvaliable != 0)
                     
                    if (onlineMFSchemeDetailsVo.mornigStar > 0)
                    {
                        imgSchemeRating1.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + onlineMFSchemeDetailsVo.mornigStar + ".png";
                        ImgStyle2.ImageUrl = @"../Images/MorningStarRating/PMFRD_RatingOverall/" + onlineMFSchemeDetailsVo.mornigStar + ".png";

                    }
                    else
                    {
                        imgSchemeRating1.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/0.png";
                        ImgStyle2.ImageUrl = @"../Images/MorningStarRating/PMFRD_RatingOverall/0.png";

                    }
                    break;
                case 3:
                    ddlAMC3.Visible = false;
                    ddlCategory3.Visible = false;
                    ddlSchemeList3.Visible = false;
                    lnkDelete3.Visible = true;
                    ImgStyle3.Visible = true;
                    lblRating1.Visible = true;

                    lblSchemeName2.Text = onlineMFSchemeDetailsVo.schemeName;
                    lblNAV2.Text = onlineMFSchemeDetailsVo.NAV.ToString();
                    lblNAVDate2.Text = onlineMFSchemeDetailsVo.navDate.ToString();
                    lblCategory2.Text = onlineMFSchemeDetailsVo.category;
                    lblBanchMark2.Text = onlineMFSchemeDetailsVo.schemeBanchMark;
                    lblFundManager2.Text = onlineMFSchemeDetailsVo.fundManager;
                    lblFundReturn1styear2.Text = onlineMFSchemeDetailsVo.SchemeReturn3Year.ToString();
                    lblFundReturn3rdyear2.Text = onlineMFSchemeDetailsVo.SchemeReturn5Year.ToString();
                    lblFundReturn5thyear2.Text = onlineMFSchemeDetailsVo.SchemeReturn10Year.ToString();
                    lblBenchmarkReturn2.Text = onlineMFSchemeDetailsVo.benchmarkReturn1stYear;
                    lblBenchMarkReturn3rd2.Text = onlineMFSchemeDetailsVo.benchmark3rhYear;
                    lblBenchMarkReturn5th2.Text = onlineMFSchemeDetailsVo.benchmark5thdYear;
                    lblMinSIP2.Text = onlineMFSchemeDetailsVo.minSIPInvestment.ToString();
                    lblSIPMultipleOf2.Text = onlineMFSchemeDetailsVo.SIPmultipleOf.ToString();
                    lblExitLoad2.Text = onlineMFSchemeDetailsVo.exitLoad.ToString();
                    lblMinInvestment2.Text = onlineMFSchemeDetailsVo.minmumInvestmentAmount.ToString();
                    lblMinMultipleOf2.Text = onlineMFSchemeDetailsVo.multipleOf.ToString();
                    if (onlineMFSchemeDetailsVo.isPurchaseAvaliable != 0)
                    
                    if (onlineMFSchemeDetailsVo.isSIPAvaliable != 0)
                   
                    if (onlineMFSchemeDetailsVo.mornigStar > 0)
                    {
                        imgSchemeRating2.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + onlineMFSchemeDetailsVo.mornigStar + ".png";
                        ImgStyle3.ImageUrl = @"../Images/MorningStarRating/PMFRD_RatingOverall/" + onlineMFSchemeDetailsVo.mornigStar + ".png";

                    }
                    else
                    {
                        imgSchemeRating2.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/0.png";
                        ImgStyle3.ImageUrl = @"../Images/MorningStarRating/PMFRD_RatingOverall/0.png";

                    }
                    break;
                case 4:
                    ddlAMC4.Visible = false;
                    ddlCategory4.Visible = false;
                    ddlSchemeList4.Visible = false;
                    lnkDelete4.Visible = true;
                    ImgStyle4.Visible = true;
                    lblRating1.Visible = true;

                    lblSchemeName3.Text = onlineMFSchemeDetailsVo.schemeName;
                    lblNAV3.Text = onlineMFSchemeDetailsVo.NAV.ToString();
                    lblNAVDate3.Text = onlineMFSchemeDetailsVo.navDate.ToString();
                    lblCategory3.Text = onlineMFSchemeDetailsVo.category;
                    lblBanchMark3.Text = onlineMFSchemeDetailsVo.schemeBanchMark;
                    lblFundManager3.Text = onlineMFSchemeDetailsVo.fundManager;
                    lblFundReturn1styear3.Text = onlineMFSchemeDetailsVo.SchemeReturn3Year.ToString();
                    lblFundReturn3rdyear3.Text = onlineMFSchemeDetailsVo.SchemeReturn5Year.ToString();
                    lblFundReturn5thyear3.Text = onlineMFSchemeDetailsVo.SchemeReturn10Year.ToString();
                    lblBenchmarkReturn3.Text = onlineMFSchemeDetailsVo.benchmarkReturn1stYear;
                    lblBenchMarkReturn3rd3.Text = onlineMFSchemeDetailsVo.benchmark3rhYear;
                    lblBenchMarkReturn5th3.Text = onlineMFSchemeDetailsVo.benchmark5thdYear;
                    lblMinSIP3.Text = onlineMFSchemeDetailsVo.minSIPInvestment.ToString();
                    lblSIPMultipleOf3.Text = onlineMFSchemeDetailsVo.SIPmultipleOf.ToString();
                    lblExitLoad3.Text = onlineMFSchemeDetailsVo.exitLoad.ToString();
                    lblMinInvestment3.Text = onlineMFSchemeDetailsVo.minmumInvestmentAmount.ToString();
                    lblMinMultipleOf3.Text = onlineMFSchemeDetailsVo.multipleOf.ToString();
                    if (onlineMFSchemeDetailsVo.isPurchaseAvaliable != 0)
                      
                    if (onlineMFSchemeDetailsVo.isSIPAvaliable != 0)
                        
                    if (onlineMFSchemeDetailsVo.mornigStar > 0)
                    {
                        imgSchemeRating3.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + onlineMFSchemeDetailsVo.mornigStar + ".png";
                        ImgStyle4.ImageUrl = @"../Images/MorningStarRating/PMFRD_RatingOverall/" + onlineMFSchemeDetailsVo.mornigStar + ".png";

                    }
                    else
                    {
                        imgSchemeRating3.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/0.png";
                        ImgStyle4.ImageUrl = @"../Images/MorningStarRating/PMFRD_RatingOverall/0.png";
                    }
                    break;

            }
        }



        protected void lnkDelete1_OnClick(object sender, EventArgs e)
        {
            ddlAMC1.Visible = true;
            ddlCategory1.Visible = true;
            ddlSchemeList1.Visible = true;
            lnkDelete1.Visible = false;
            ImgStyle1.Visible = false;
            BindAMC1();
            ClearAllField(1);
        }
        protected void lnkDelete2_OnClick(object sender, EventArgs e)
        {
            ddlAMC2.Visible = true;
            ddlCategory2.Visible = true;
            ddlSchemeList2.Visible = true;
            lnkDelete2.Visible = false;
            ImgStyle2.Visible = false;
            BindAMC1();
            ClearAllField(2);
        }
        protected void lnkDelete31_OnClick(object sender, EventArgs e)
        {
            ddlAMC3.Visible = true;
            ddlCategory3.Visible = true;
            ddlSchemeList3.Visible = true;
            lnkDelete3.Visible = false;
            ImgStyle3.Visible = false;
            BindAMC1();
            ClearAllField(3);
        }
        protected void lnkDelete4_OnClick(object sender, EventArgs e)
        {
            ddlAMC4.Visible = true;
            ddlCategory4.Visible = true;
            ddlSchemeList4.Visible = true;
            lnkDelete4.Visible = false;
            ImgStyle4.Visible = false;
            BindAMC1();
            ClearAllField(4);
        }
        protected void ClearAllField(int columnNo)
        {
            switch (columnNo)
            {
                case 1:
                    imgSchemeRating.ImageUrl = "";
                    lblSchemeName.Text = "";
                    lblNAV.Text = "";
                    lblNAVDate.Text = "";
                    lblCategory.Text = "";
                    lblBanchMark.Text = "";
                    lblFundManager.Text = "";
                    lblFundReturn1styear.Text = "";
                    lblFundReturn3rdyear.Text = "";
                    lblFundReturn5thyear.Text = "";
                    lblBenchmarkReturn.Text = "";
                    lblBenchMarkReturn3rd.Text = "";
                    lblBenchMarkReturn5th.Text = "";
                    lblMinSIP.Text = "";
                    lblSIPMultipleOf.Text = "";
                    lblExitLoad.Text = "";
                    lblMinInvestment.Text = "";
                    lblMinMultipleOf.Text = "";
                    imgSchemeRating.ImageUrl = "";
                    ddlCategory1.SelectedValue = "0";
                    ddlSchemeList1.SelectedValue = "0";
                    lblRating1.Visible = false;

                    break;
                case 2:
                    imgSchemeRating1.ImageUrl = "";
                    lblSchemeName1.Text = "";
                    lblNAV1.Text = "";
                    lblNAVDate1.Text = "";
                    lblCategory1.Text = "";
                    lblBanchMark1.Text = "";
                    lblFundManager1.Text = "";
                    lblFundReturn1styear1.Text = "";
                    lblFundReturn3rdyear1.Text = "";
                    lblFundReturn5thyear1.Text = "";
                    lblBenchmarkReturn1.Text = "";
                    lblBenchMarkReturn3rd1.Text = "";
                    lblBenchMarkReturn5th1.Text = "";
                    lblMinSIP1.Text = "";
                    lblSIPMultipleOf1.Text = "";
                    lblExitLoad1.Text = "";
                    lblMinInvestment1.Text = "";
                    lblMinMultipleOf1.Text = "";
                    imgSchemeRating1.ImageUrl = "";
                    ddlCategory2.SelectedValue = "0";
                    ddlSchemeList2.SelectedValue = "0";

                    break;
                case 3:
                    imgSchemeRating2.ImageUrl = "";
                    lblSchemeName2.Text = "";
                    lblNAV2.Text = "";
                    lblNAVDate2.Text = "";
                    lblCategory2.Text = "";
                    lblBanchMark2.Text = "";
                    lblFundManager2.Text = "";
                    lblFundReturn1styear2.Text = "";
                    lblFundReturn3rdyear2.Text = "";
                    lblFundReturn5thyear2.Text = "";
                    lblBenchmarkReturn2.Text = "";
                    lblBenchMarkReturn3rd2.Text = "";
                    lblBenchMarkReturn5th2.Text = "";
                    lblMinSIP2.Text = "";
                    lblSIPMultipleOf2.Text = "";
                    lblExitLoad2.Text = "";
                    lblMinInvestment2.Text = "";
                    lblMinMultipleOf2.Text = "";
                    imgSchemeRating2.ImageUrl = "";
                    ddlCategory3.SelectedValue = "0";
                    ddlSchemeList3.SelectedValue = "0";

                    break;
                case 4:
                    imgSchemeRating3.ImageUrl = "";
                    lblSchemeName3.Text = "";
                    lblNAV3.Text = "";
                    lblNAVDate3.Text = "";
                    lblCategory3.Text = "";
                    lblBanchMark3.Text = "";
                    lblFundManager3.Text = "";
                    lblFundReturn1styear3.Text = "";
                    lblFundReturn3rdyear3.Text = "";
                    lblFundReturn5thyear3.Text = "";
                    lblBenchmarkReturn3.Text = "";
                    lblBenchMarkReturn3rd3.Text = "";
                    lblBenchMarkReturn5th3.Text = "";
                    lblMinSIP3.Text = "";
                    lblSIPMultipleOf3.Text = "";
                    lblExitLoad3.Text = "";
                    lblMinInvestment3.Text = "";
                    lblMinMultipleOf3.Text = "";
                    imgSchemeRating3.ImageUrl = "";
                    ddlCategory4.SelectedValue = "0";
                    ddlSchemeList4.SelectedValue = "0";
                    break;
            }
        }

        protected void ddlAction_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlAction.SelectedValue)
            {
                case "Buy":
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanelFromMainPage('MFOrderPurchaseTransType','" + ddlSchemeList1.SelectedValue + "')", true);
                    break;
                case "SIP":
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewkv", "LoadTransactPanelFromMainPage('MFOrderSIPTransType','" + ddlSchemeList1.SelectedValue + "')", true);
                    break;
            }
        }
        protected void ddlAction1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlAction1.SelectedValue)
            {
                case "Buy":
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanelFromMainPage('MFOrderPurchaseTransType','" + ddlSchemeList2.SelectedValue + "')", true);
                    break;
                case "SIP":
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanelFromMainPage('MFOrderSIPTransType','" + ddlSchemeList2.SelectedValue + "')", true);
                    break;
            }
        }
        protected void ddlAction2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlAction2.SelectedValue)
            {
                case "Buy":
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanelFromMainPage('MFOrderPurchaseTransType','" + ddlSchemeList3.SelectedValue + "')", true);
                    break;
                case "SIP":
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanelFromMainPage('MFOrderSIPTransType','" + ddlSchemeList3.SelectedValue + "')", true);
                    break;
            }
        }
        protected void ddlAction3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlAction3.SelectedValue)
            {
                case "Buy":
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanelFromMainPage('MFOrderPurchaseTransType','" + ddlSchemeList4.SelectedValue + "')", true);
                    break;
                case "SIP":
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvwewv", "LoadTransactPanelFromMainPage('MFOrderSIPTransType','" + ddlSchemeList4.SelectedValue + "')", true);
                    break;
            }
        }
    }
}