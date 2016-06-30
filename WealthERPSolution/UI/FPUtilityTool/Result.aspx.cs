using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoFPUtility;
using System.Data;
using VOFPUtilityUser;
using System.Configuration;
using System.Web.Services;
using VoUser;
using BoCustomerProfiling;
using VoCustomerPortfolio;
using VoCustomerProfiling;
using System.Text;
using InfoSoftGlobal;

namespace FPUtilityTool
{
    public partial class Result : System.Web.UI.Page
    {
        FPUserBO fpUserBo = new FPUserBO();
        FPUserVo fpuserVo = new FPUserVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            FPUserBO.CheckSession();
            fpuserVo = (FPUserVo)Session["FPUserVo"];
            int adviserId = Convert.ToInt32(ConfigurationManager.AppSettings["ONLINE_ADVISER"]);
            lblUserName.Text = " " + fpuserVo.UserName;
            if (!IsPostBack)
            {
                divTncSuccess.Visible = false;
                //if (Request.UrlReferrer == null)
                //    Response.Redirect("Questionnaire.aspx");
                //else
                //{
                    DataSet dsRiskClass = fpUserBo.GetRiskClass(fpuserVo.UserId, adviserId);
                    if (dsRiskClass.Tables[0].Rows.Count > 0)
                    {
                        lblRiskClass.Text = dsRiskClass.Tables[0].Rows[0]["XRC_RiskClass"].ToString();
                        lblRiskText.Text = dsRiskClass.Tables[0].Rows[0]["ARC_RiskText"].ToString();
                    }
                    if (dsRiskClass.Tables[1].Rows.Count > 0)
                    {
                        BindAssetsPiaChart(dsRiskClass.Tables[1]);
                    }

                //}
            }


        }
        protected void BindAssetsPiaChart(DataTable dtAssetsPiaChart)
        {
            int count = 0;

            StringBuilder strXML3 = new StringBuilder();
            strXML3.Append(@"<chart caption='Assets Allocation' chartTopMargin='0' bgcolor='FFFFFF' showvalues='1' showpercentvalues='1'  showLegend='1' showHoverEffect='1' captionPadding='0' chartLeftMargin='0'
                        chartRightMargin='0' chartBottomMargin='0' showborder='0' use3dlighting='0' showshadow='0' showLabels='1' legendborder='0' legendposition='bottom' enablesmartlabels='1' legendbgcolor='#CCCCCC' legendbgalpha='20' legendborderalpha='0' legendshadow='0' legendnumcolumns='3'> ");
            if (dtAssetsPiaChart.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAssetsPiaChart.Rows)
                {
                    strXML3.AppendFormat(@"<set label='{0}' value='{1}' />", dr["WAC_AssetClassification"].ToString(), dr["AllocationPercentage"]);
                    count++;
                    if (count > 5)
                        break;
                }
                strXML3.Append(@"</chart>");
                ltrAssets.Text = FusionCharts.RenderChartHTML("../Content/Pie2D.swf", "", strXML3.ToString(), "asst", "100%", "350", false, true, true);
            }
            else
                ltrAssets.Text = "No record found";

        }
        protected void btnLogOut_OnClick(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
        protected void btnTnC_Click(object sender, EventArgs e)
        {
            CustomerVo customerVo = new CustomerVo();
            UserVo userVo = new UserVo();
            CustomerBo customerBo = new CustomerBo();
            CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
            List<int> customerIds = new List<int>();
            if (Page.IsValid)
            {
                if (fpuserVo.C_CustomerId == null || fpuserVo.C_CustomerId == 0)
                {
                    customerVo.RmId = 4682;
                    customerVo.BranchId = 1339;
                    customerVo.Type = "IND";
                    customerVo.FirstName = fpuserVo.UserName;
                    userVo.FirstName = fpuserVo.UserName;
                    customerVo.Email = fpuserVo.EMail;
                    customerVo.IsProspect = 1;
                    customerVo.IsFPClient = 1;
                    customerVo.IsActive = 1;
                    customerVo.PANNum = fpuserVo.Pan;
                    customerVo.Mobile1 = fpuserVo.MobileNo;
                    customerVo.ProspectAddDate = DateTime.Now;
                    userVo.Email = fpuserVo.EMail;
                    customerPortfolioVo.IsMainPortfolio = 0;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PortfolioName = "MyPortfolioProspect";
                    customerIds = customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, fpuserVo.UserId);
                    if (customerIds != null)
                    {
                        CustomerFamilyVo familyVo = new CustomerFamilyVo();
                        CustomerFamilyBo familyBo = new CustomerFamilyBo();
                        familyVo.AssociateCustomerId = customerIds[1];
                        familyVo.CustomerId = customerIds[1];
                        familyVo.Relationship = "SELF";
                        familyBo.CreateCustomerFamily(familyVo, customerIds[1], userVo.UserId);
                    }
                    if (UpdateCustomerIdInFPUserTable(fpuserVo.UserId, customerIds[1]))
                        divTncSuccess.Visible = true;
                }
                else
                {
                    if (UpdateCustomerIdInFPUserTable(fpuserVo.UserId, fpuserVo.C_CustomerId))
                        divTncSuccess.Visible = true;
                }


            }
        }
        private bool UpdateCustomerIdInFPUserTable(int fpUserId, int customerId)
        {
            bool result = false;
            result = fpUserBo.UpdateCustomerProspect(customerId, fpUserId);
            fpuserVo = fpUserBo.GetFPUser(fpUserId);
            Session["FPUserVo"] = fpuserVo;
            return result;
        }
        [WebMethod(EnableSession = true)]
        public static List<object> GetChartData()
        {
            FPUserBO fpUserBo = new FPUserBO();
            FPUserVo userVo = new FPUserVo();
            userVo = (FPUserVo)HttpContext.Current.Session["FPUserVo"];
            int adviserId = Convert.ToInt32(ConfigurationManager.AppSettings["ONLINE_ADVISER"]);
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
             {
               "WAC_AssetClassification", "AllocationPercentage"
              });
            DataSet dsRiskClass = fpUserBo.GetRiskClass(userVo.UserId, adviserId);
            foreach (DataRow dr in dsRiskClass.Tables[1].Rows)
            {
                chartData.Add(new object[]
                    {
                        dr["WAC_AssetClassification"], dr["AllocationPercentage"]
                    });
            }
            return chartData;

        }
    }

}
