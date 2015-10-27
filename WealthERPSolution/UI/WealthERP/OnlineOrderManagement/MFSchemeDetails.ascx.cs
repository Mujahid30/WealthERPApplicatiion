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
using System.Text;
using InfoSoftGlobal;
using System.Drawing;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Web.UI.DataVisualization.Charting;
namespace WealthERP.OnlineOrderManagement
{
    public partial class MFSchemeDetails : System.Web.UI.UserControl
    {
        OnlineMFSchemeDetailsBo onlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
        CustomerVo customerVo = new CustomerVo();
        List<int> schemeCompareList = new List<int>();
        OnlineMFSchemeDetailsVo onlineMFSchemeDetailsVo;
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            OnlineUserSessionBo.CheckSession();
            customerVo = (CustomerVo)Session["CustomerVo"];
            if (!IsPostBack)
            {
                BindAMC();
                if (Request.QueryString["schemeCode"] != null)
                {
                    //if (Request.QueryString["schemeCode"] != null)//Session["MFSchemePlan"] != null || 
                    //    Session["MFSchemePlan"] = Request.QueryString["schemeCode"];
                    int amcCode = 0;
                    string category = string.Empty;
                    BindCategory();
                    //if (Session["MFSchemePlan"] != null && Session["MFSchemePlan"] != "")
                    //{
                    commonLookupBo.GetSchemeAMCCategory(int.Parse(Request.QueryString["schemeCode"].ToString()), out amcCode, out category);
                    int schemecode = int.Parse(Request.QueryString["schemeCode"].ToString());
                    ddlAMC.SelectedValue = amcCode.ToString();
                    ddlCategory.SelectedValue = category;
                    BindScheme();
                    ddlScheme.SelectedValue = schemecode.ToString();
                    GetAmcSchemeDetails();
                    BindschemedetailsNAV();
                    hidCurrentScheme.Value = ddlScheme.SelectedValue;

                    //}
                }
            }
        }
        protected void ddlAMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMC.SelectedIndex != 0)
            {
                BindCategory();
                BindScheme();
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
                ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Scheme", "0"));
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
            BindschemedetailsNAV();
            hidCurrentScheme.Value = ddlScheme.SelectedValue;
            Session["MFSchemePlan"] = ddlScheme.SelectedValue;
        }

        protected void BindschemedetailsNAV()
        {
            DataTable dtBindschemedetailsNAV = new DataTable();
            dtBindschemedetailsNAV = onlineMFSchemeDetailsBo.GetschemedetailsNAV(int.Parse(ddlScheme.SelectedValue));
            Div2.Visible = true;
            if (dtBindschemedetailsNAV.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBindschemedetailsNAV.Rows)
                {
                    if (Convert.ToDouble(dr["Diff"].ToString()) < 0.000)
                    {
                        lblNAV.Text = dr["PSP_NetAssetValue"].ToString();
                        lblNAVDiff.Text = " " + dr["Diff"].ToString().TrimStart('-') + " (" + dr["Percentage"].ToString() + " %)";
                        lblNAV.Style["font-size"] = "large";
                        lblNAV.Style["font-weight"] = "bold";
                        lblNAVDiff.Style["font-size"] = "small";
                        lblAsonDate.Text = "(As on Date:" + dr["PSP_PostDate"].ToString() + ")";
                        lblAsonDate.Style["font-size"] = "xx-small";
                        ImagNAV.ImageUrl = @"../Images/arrow.png";
                        lblNAVDiff.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblNAV.Text = dr["PSP_NetAssetValue"].ToString();
                        lblNAVDiff.Text = " " + dr["Diff"].ToString().TrimStart('-') + " (" + dr["Percentage"].ToString() + " %)";
                        lblNAV.Style["font-size"] = "large";
                        lblNAVDiff.Style["font-size"] = "small";
                        lblNAV.Style["font-weight"] = "bold";
                        lblAsonDate.Text = "(As on Date:" + dr["PSP_PostDate"].ToString() + ")";
                        lblAsonDate.Style["font-size"] = "xx-small";
                        ImagNAV.ImageUrl = @"../Images/down_NAVarrow.png";
                        lblNAVDiff.ForeColor = Color.Green;
                    }
                }
            }
        }
        protected void btnHistory_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender as Button;
            switch (btn.ID)
            {
                case "btn1m":
                    GetSchemeNAVHistory(DateTime.Now.AddMonths(-1), DateTime.Now);
                    break;
                case "btn3m":
                    GetSchemeNAVHistory(DateTime.Now.AddMonths(-3), DateTime.Now);
                    break;
                case "btn6m":
                    GetSchemeNAVHistory(DateTime.Now.AddMonths(-6), DateTime.Now);
                    break;
                case "btn1y":
                    GetSchemeNAVHistory(DateTime.Now.AddYears(-1), DateTime.Now);
                    break;
                case "btn2y":
                    GetSchemeNAVHistory(DateTime.Now.AddYears(-2), DateTime.Now);
                    break;
            }
        }
        protected void btnHistoryChat_OnClick(object sender, EventArgs e)
        {
            GetSchemeNAVHistory(rdpFromDate.SelectedDate, rdpToDate.SelectedDate);
        }
        public void GetSchemeNAVHistory(DateTime? fromDate, DateTime? toDate)
        {
            int schemePlanCode = int.Parse(hidCurrentScheme.Value);
            DataTable dt = onlineMFSchemeDetailsBo.GetSchemeNavHistory(schemePlanCode, fromDate, toDate);
            LoadNAVHistoryChat(dt);
        }
        public void LoadNAVHistoryChat(DataTable dtNavDetails)
        {
            divChart.Visible = true;
            StringBuilder strXML = new StringBuilder();
            strXML.Append(@"<chart caption='NAV History' chartTopMargin='0' xAxisName='Date' toolText='NAV' flatscrollbars='1' scrollshowbuttons='0' scrollshowbuttons='0' useCrossLine='1' yAxisName='NAV' anchorBgColor='FFFFFF' bgColor='FFFFFF' showBorder='0'  canvasBgColor='FFFFFF' lineColor='2480C7'  >");
            strXML.Append(@" <categories>");
            foreach (DataRow dr in dtNavDetails.Rows)
            {
                strXML.AppendFormat("<category label ='{0}'/>", dr["PSP_Date"]);
            }
            strXML.AppendFormat(@" </categories> <dataset seriesName='{0}'>", ViewState["schemeName"]);
            foreach (DataRow dr in dtNavDetails.Rows)
            {
                strXML.AppendFormat("<set value ='{0}'  />", dr["PSP_NetAssetValue"].ToString());
            }
            strXML.Append("</dataset>");

            strXML.Append(@"<vTrendlines>  <line startValue='895' color='FF0000' toolText='NAV' displayValue='Average' showOnTop='1' /></vTrendlines> </chart>");
            Literal1.Text = FusionCharts.RenderChartHTML("../FusionCharts/ZoomLine.swf", "", strXML.ToString(), "FactorySum", "100%", "400", false, true, false);


        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            if (divChart.Visible == true)
            {
                divChart.Visible = false;
                DivReturnChat.Visible = true;
            }
            else
            {
                divChart.Visible =true ;
                DivReturnChat.Visible = false;
            }
        }
        protected void BindndReturn()
        {
            StringBuilder strXML = new StringBuilder();
            strXML.Append(@"<chart caption='Scheme Return' >
              <categories>
              <category label='Return yr1'/>
              <category label='Return yr2' />
              <category label='Return yr3' />
              </categories>"
                );
            strXML.Append(@"<dataset seriesname='BenchMark'>");
            strXML.AppendFormat(@"<set value ='{0}'  />", onlineMFSchemeDetailsVo.benchmarkReturn1stYear);
            strXML.AppendFormat(@"<set value ='{0}'  />", onlineMFSchemeDetailsVo.benchmark3rhYear);
            strXML.AppendFormat(@"<set value ='{0}'  />", onlineMFSchemeDetailsVo.benchmark5thdYear);
            strXML.Append(@"</dataset>");
            strXML.Append(@"<dataset seriesname='Return'>");
            strXML.AppendFormat(@"<set value ='{0}'  />", onlineMFSchemeDetailsVo.SchemeReturn3Year.ToString());
            strXML.AppendFormat(@"<set value ='{0}'  />", onlineMFSchemeDetailsVo.SchemeReturn5Year.ToString());
            strXML.AppendFormat(@"<set value ='{0}'  />", onlineMFSchemeDetailsVo.SchemeReturn10Year.ToString());
            strXML.Append(@"</dataset>");
            strXML.Append(@"</chart>");
            
                ltrReturn.Text = FusionCharts.RenderChartHTML("../FusionCharts/MSColumn3D.swf", "", strXML.ToString(), "FactorySum", "100%", "400", false, true, false);
        }


        public void GetAmcSchemeDetails()
        {
            DataTable dtNavDetails = null;
            try
            {
                BindfundManagerDetails();
                BindSectoreDetails();
                BindHoldingDetails();
                BindAssetsAllocation();
                divAction.Visible = true;
                onlineMFSchemeDetailsVo = onlineMFSchemeDetailsBo.GetSchemeDetails(int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), ddlCategory.SelectedValue, out  dtNavDetails);
                ViewState["schemeName"] = onlineMFSchemeDetailsVo.schemeName;
                LoadNAVHistoryChat(dtNavDetails);
                BindndReturn();
                DivReturnChat.Visible = false;
                lblSchemeName.Text = onlineMFSchemeDetailsVo.schemeName;
                lblAMC.Text = onlineMFSchemeDetailsVo.amcName;
                //lblNAV.Text = onlineMFSchemeDetailsVo.NAV.ToString();
                //if (!string.IsNullOrEmpty(onlineMFSchemeDetailsVo.navDate))
                //    lblNAVDate.Text = onlineMFSchemeDetailsVo.navDate.ToString();
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
                imgRating3yr.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + onlineMFSchemeDetailsVo.SchemeRating3Year + ".png";
                imgRating5yr.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + onlineMFSchemeDetailsVo.SchemeRating5Year + ".png";
                imgRating10yr.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + onlineMFSchemeDetailsVo.SchemeRating10Year + ".png";
                imgRatingOvelAll.ImageUrl = @"../Images/MorningStarRating/RatingOverall/" + onlineMFSchemeDetailsVo.overAllRating + ".png";
                lblSchemeRetrun3yr.Text = onlineMFSchemeDetailsVo.SchemeReturn3Year.ToString();
                lblSchemeRetrun5yr.Text = onlineMFSchemeDetailsVo.SchemeReturn5Year.ToString();
                lblSchemeRetrun10yr.Text = onlineMFSchemeDetailsVo.SchemeReturn10Year.ToString();
                lblSchemeRisk3yr.Text = onlineMFSchemeDetailsVo.SchemeRisk3Year;
                lblSchemeRisk5yr.Text = onlineMFSchemeDetailsVo.SchemeRisk5Year;
                lblSchemeRisk10yr.Text = onlineMFSchemeDetailsVo.SchemeRisk10Year;
                if (onlineMFSchemeDetailsVo.mornigStar > 0)
                {
                    imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + onlineMFSchemeDetailsVo.mornigStar + ".png";
                    imgStyleBox.ImageUrl = @"../Images/MorningStarRating/StarStyleBox/" + onlineMFSchemeDetailsVo.schemeBox + ".png";
                }
                else
                {
                    imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/0.png";
                    imgStyleBox.ImageUrl = @"../Images/MorningStarRating/StarStyleBox/0.png";

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
                FunctionInfo.Add("Method", "MFSchemeDetails.ascx.cs:BindfundManagerDetails()");
                object[] objParams = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void lbBuy_OnClick(object sender, EventArgs e)
        {
            if (ddlScheme.SelectedValue != "")
            {
                if (Session["PageDefaultSetting"] != null)
                {
                    Session["MFSchemePlan"] = ddlScheme.SelectedValue;
                    LoadMFTransactionPage("MFOrderPurchaseTransType", 2);

                }
                else
                {
                    Response.Redirect("ControlHost.aspx?pageid=MFOrderPurchaseTransType&Amc=" + ddlAMC.SelectedValue + "&SchemeCode=" + ddlScheme.SelectedValue + "&category=" + ddlCategory.SelectedValue + "", false);

                }
            }
        }
        protected void lbAddPurchase_OnClick(object sender, EventArgs e)
        {
            if (ddlScheme.SelectedValue != "")
            {
                if (Session["PageDefaultSetting"] != null)
                {
                    Session["MFSchemePlan"] = ddlScheme.SelectedValue;
                    LoadMFTransactionPage("MFOrderAdditionalPurchase", 2);

                }
                else
                {
                    Response.Redirect("ControlHost.aspx?pageid=MFOrderAdditionalPurchase&Amc=" + ddlAMC.SelectedValue + "&SchemeCode=" + ddlScheme.SelectedValue + "&category=" + ddlCategory.SelectedValue + "", false);

                }
            }
        }
        protected void lbSIP_OnClick(object sender, EventArgs e)
        {
            if (ddlScheme.SelectedValue != "")
            {
                if (Session["PageDefaultSetting"] != null)
                {
                    Session["MFSchemePlan"] = ddlScheme.SelectedValue;
                    LoadMFTransactionPage("MFOrderSIPTransType", 2);

                }
                else
                {
                    Response.Redirect("ControlHost.aspx?pageid=MFOrderSIPTransType&Amc=" + ddlAMC.SelectedValue + "&SchemeCode=" + ddlScheme.SelectedValue + "&category=" + ddlCategory.SelectedValue + "", false);

                }
            }
        }
        protected void lbRedem_OnClick(object sender, EventArgs e)
        {
            if (ddlScheme.SelectedValue != "")
            {
                if (Session["PageDefaultSetting"] != null)
                {
                    Session["MFSchemePlan"] = ddlScheme.SelectedValue;
                    LoadMFTransactionPage("MFOrderRdemptionTransType", 2);

                }
                else
                {
                    Response.Redirect("ControlHost.aspx?pageid=MFOrderRdemptionTransType&Amc=" + ddlAMC.SelectedValue + "&SchemeCode=" + ddlScheme.SelectedValue + "&category=" + ddlCategory.SelectedValue + "", false);

                }
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        //public object GetData()
        //{
        protected void BindfundManagerDetails()
        {
            try
            {
                string cmotcode = onlineMFSchemeDetailsBo.GetCmotCode(int.Parse(ddlScheme.SelectedValue));
                ViewState["cmotcode"] = cmotcode;
                if (cmotcode != "")
                {
                    DataSet theDataSet = onlineMFSchemeDetailsBo.GetAPIData(ConfigurationSettings.AppSettings["FUND_MANAGER_DETAILS"] + cmotcode + "/Pre");
                    foreach (DataRow dr in theDataSet.Tables[1].Rows)
                    {
                        lblFundMAnagername.Text = dr["FundManager"].ToString();
                        lblQualification.Text = dr["Qualification"].ToString();
                        lblDesignation.Text = dr["Designation"].ToString();
                        lblExperience.Text = dr["experience"].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
            }
        }
        protected void BindHoldingDetails()
        {
            try
            {
                if (ViewState["cmotcode"] != null)
                {
                    DataSet theDataSet = onlineMFSchemeDetailsBo.GetAPIData(ConfigurationSettings.AppSettings["HOLDING_DETAILS"] + ViewState["cmotcode"] + "/" + ConfigurationSettings.AppSettings["TOP_Scheme"]);
                    rpSchemeDetails.DataSource = theDataSet.Tables[1];
                    rpSchemeDetails.DataBind();
                    ViewState["HoldingDetails"] = theDataSet.Tables[1];
                    if (theDataSet.Tables[1].Rows.Count > 0)
                        BindHoldingPiaChart(theDataSet.Tables[1]);
                }
            }

            catch (Exception Ex)
            {

            }
        }
        protected void BindSectoreDetails()
        {
            try
            {
                if (ViewState["cmotcode"] != null)
                {
                    DataSet theDataSet = onlineMFSchemeDetailsBo.GetAPIData(ConfigurationSettings.AppSettings["SECTOR_DETAILS"] + ViewState["cmotcode"] + "/" + ConfigurationSettings.AppSettings["SECTOR_DETAILS_COUNT"] + "?responsetype=xml");
                    RepSector.DataSource = theDataSet.Tables[3];
                    RepSector.DataBind();
                    if (theDataSet.Tables[3].Rows.Count > 0)
                        BindSectorPiaChart(theDataSet.Tables[3]);

                }
            }

            catch (Exception Ex)
            {

            }
        }
        protected void BindAssetsAllocation()
        {

            try
            {
                DataSet theDataSet = onlineMFSchemeDetailsBo.GetAPIData(ConfigurationSettings.AppSettings["ASSET_ALLOCATION"] + ViewState["cmotcode"] + "?responsetype=xml");
                RepAsset.DataSource = theDataSet.Tables[3];
                RepAsset.DataBind();
                if (theDataSet.Tables[3].Rows.Count > 0)
                    BindAssetsPiaChart(theDataSet.Tables[3]);
            }

            catch (Exception Ex)
            {

            }
        }
        protected void lnkAddToCompare_OnClick(object sender, EventArgs e)
        {
            if (ddlScheme.SelectedValue != "")
            {
                //if (Session["SchemeCompareList"] != null)
                //{
                //schemeCompareList = (List<int>)Session["SchemeCompareList"];
                //if (schemeCompareList[0] != Convert.ToInt32(hidCurrentScheme.Value))
                //{
                schemeCompareList.Add(Convert.ToInt32(hidCurrentScheme.Value));
                Session["SchemeCompareList"] = schemeCompareList;

                //if (schemeCompareList.Count > 0)
                //{
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('OnlineMFSchemeCompare','&schemeCompareList=" + schemeCompareList + "');", true);
                LoadMFTransactionPage("OnlineMFSchemeCompare", 1);

                //    }
                //}
                //else
                //{
                //    ShowMessage("Scheme already added for compare!!", 'I');
                //}

                //}
                //else
                //{
                //    schemeCompareList.Add(Convert.ToInt32(hidCurrentScheme.Value));
                //    Session["SchemeCompareList"] = schemeCompareList;
                //}
            }
        }
        private void ShowMessage(string msg, char type)
        {
            //--S(success)
            //--F(failure)
            //--W(warning)
            //--I(information)
            trMessage.Visible = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wsedrftgyhjukloghjnnnghj", " showMsg('" + msg + "','" + type.ToString() + "');", true);
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
        protected void BindHoldingPiaChart(DataTable dtHoldingPiaChart)
        {
            StringBuilder strXML1 = new StringBuilder();
            strXML1.Append(@"<chart caption='Fund Holding' chartTopMargin='0' bgcolor='#ffffff' showHoverEffect='1' exportenabled='1' captionPadding='0' chartLeftMargin='0'
                        chartRightMargin='0' chartBottomMargin='0' showborder='0' use3dlighting='0' showshadow='0'> ");

            foreach (DataRow dr in dtHoldingPiaChart.Rows)
            {
                strXML1.AppendFormat(@"<set label='{0}' value='{1}' />", dr["co_name"], dr["perc_hold"]);
            }
            strXML1.Append(@"</chart>");
            ltrHolding.Text = FusionCharts.RenderChartHTML("../FusionCharts/Pie2D.swf", "", strXML1.ToString(), "FactorySum1", "100%", "150", false, true, false);


        }
        protected void BindSectorPiaChart(DataTable dtSectorPiaChart)
        {
            StringBuilder strXML2 = new StringBuilder();
            strXML2.Append(@"<chart caption='Fund Sector' chartTopMargin='0' bgcolor='#ffffff' showHoverEffect='1' exportenabled='1' captionPadding='0' chartLeftMargin='0'
                        chartRightMargin='0' chartBottomMargin='0' showborder='0' use3dlighting='0' showshadow='0'> ");

            foreach (DataRow dr in dtSectorPiaChart.Rows)
            {
                strXML2.AppendFormat(@"<set label='{0}' value='{1}' />", dr["sector"], dr["holdingpercentage"]);
            }
            strXML2.Append(@"</chart>");
            ltrSector.Text = FusionCharts.RenderChartHTML("../FusionCharts/Pie2D.swf", "", strXML2.ToString(), "FactorySum2", "100%", "150", false, true, false);
        }
        protected void BindAssetsPiaChart(DataTable dtAssetsPiaChart)
        {
            StringBuilder strXML3 = new StringBuilder();
            strXML3.Append(@"<chart caption='Assets Allocation' chartTopMargin='0' bgcolor='#ffffff' showHoverEffect='1' exportenabled='1' captionPadding='0' chartLeftMargin='0'
                        chartRightMargin='0' chartBottomMargin='0' showborder='0' use3dlighting='0' showshadow='0'> ");

            foreach (DataRow dr in dtAssetsPiaChart.Rows)
            {
                strXML3.AppendFormat(@"<set label='{0}' value='{1}' />", dr["asset"].ToString().Replace("&", "and"), dr["assetvalue"]);
            }
            strXML3.Append(@"</chart>");
            raj.Text = FusionCharts.RenderChartHTML("../FusionCharts/Pie2D.swf", "", strXML3.ToString(), "FactorySum3", "100%", "150", false, true, false);

            //ltrAssets.Text = FusionCharts.RenderChartHTML("../FusionCharts/Pie3D.swf", "", strXML3.ToString(), "FactorySum3", "100%", "150", false, true, false);

        }

    }
}
