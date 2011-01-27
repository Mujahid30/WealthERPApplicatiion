using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Configuration;
using Microsoft.FSharp;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Data;
using VoCustomerRiskProfiling;
using BoCustomerRiskProfiling;
using VoUser;
using BoCustomerGoalProfiling;
using WealthERP.Base;
using BoCommon;
using BoCustomerProfiling;

namespace WealthERP.FP
{
    public partial class OfflineForm : System.Web.UI.Page
    {
        RiskProfileBo riskprofilebo = new RiskProfileBo();

        //List<RiskOptionVo> listRiskOptionVo;
        DataSet dsGetRiskProfileQuestion;
        DataSet dsGetRiskProfileQuestionOption;
        //DataSet dsGetCustomerRiskProfile;
        AdvisorVo advisorVo = new AdvisorVo();
        int customerId;
        //int GoalCount;
        //int AdvisorRMId;
        //string riskCode;
        //int isProspect;
        CustomerVo customerVo;
        CustomerBo customerBo;
        CustomerGoalSetupBo GoalSetupBo = new CustomerGoalSetupBo();
            
        public int totalquestion;
        public int optioncount;
      
        //double cashPercentage;
        //DataSet dsGetRiskProfileId;
        RMVo rmvo = new RMVo();
        protected void Page_Init(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            rmvo = (RMVo)Session["rmVo"];
            lblRMName.Text = rmvo.FirstName + " " + rmvo.MiddleName + " " + rmvo.LastName;
            lblRMNameFamily.Text = rmvo.FirstName + " " + rmvo.MiddleName + " " + rmvo.LastName;
            lblRmNameExpanse.Text = rmvo.FirstName + " " + rmvo.MiddleName + " " + rmvo.LastName;
            lblRMRisk.Text = rmvo.FirstName + " " + rmvo.MiddleName + " " + rmvo.LastName;
            lblRMRiskPro.Text = rmvo.FirstName + " " + rmvo.MiddleName + " " + rmvo.LastName;
            //Img1.ImageUrl = advisorVo.LogoPath;
            lblOrgName.Text = advisorVo.OrganizationName;
            lblOrgnameGoal.Text = advisorVo.OrganizationName;
            lblOrgname1.Text = advisorVo.OrganizationName;
            lblOrgRisk.Text = advisorVo.OrganizationName;
            lblOrgQues.Text = advisorVo.OrganizationName;
            DateTime dtToday=DateTime.Now;
            lblDate1.Text = dtToday.Day + " " + dtToday.ToString("MMMM") + " " + dtToday.Year.ToString();
            lblDate2.Text = dtToday.Day + " " + dtToday.ToString("MMMM") + " " + dtToday.Year.ToString();
            lblDate3.Text = dtToday.Day + " " + dtToday.ToString("MMMM") + " " + dtToday.Year.ToString();
            lblDate4.Text = dtToday.Day + " " + dtToday.ToString("MMMM") + " " + dtToday.Year.ToString();
            lblDate5.Text = dtToday.Day + " " + dtToday.ToString("MMMM") + " " + dtToday.Year.ToString();
            dsGetRiskProfileQuestion = riskprofilebo.GetRiskProfileQuestion(advisorVo.advisorId);
            totalquestion = dsGetRiskProfileQuestion.Tables[0].Rows.Count;
            int optioncounttemp = 1;
            //hidGoalCount.Value = null;
            if (!Page.IsPostBack)
            {
                if (Session[SessionContents.FPS_ProspectList_CustomerId] != null && Session[SessionContents.FPS_ProspectList_CustomerId].ToString() != "")
                {
                    customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                    Session["FP_UserID"] = customerId;
                }
                customerBo = new CustomerBo();
                customerVo = customerBo.GetCustomer(customerId);
                Session[SessionContents.CustomerVo] = customerVo;
            }

            //PlaceHolder1.Controls.Add(new LiteralControl("<table>"));
            for (int i = 0; i < totalquestion; i++)
            {
                dsGetRiskProfileQuestionOption = riskprofilebo.GetQuestionOption(int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_QuestionId"].ToString()), advisorVo.advisorId);

                PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl("<td colspan=\"6\"><hr /></td></tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl("<td colspan=\"6\">" + (i + 1) + "."));
                Label lbl = new Label();
                lbl.ID = "lblQ" + (i + 1);
                lbl.Text = dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_Question"].ToString();
                lbl.CssClass = "FieldName";
                PlaceHolder1.Controls.Add(lbl);
                PlaceHolder1.Controls.Add(new LiteralControl("</td></tr>"));
                if (int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_IsImageAttached"].ToString()) == 1)
                {
                    PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
                    PlaceHolder1.Controls.Add(new LiteralControl("<td colspan=\"6\">"));
                    //<img id="imgPortfolios" src="/Images/Portfolios.jpg" />
                    System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                    img.ID = "imgPortfolios" + (i + 1);
                    img.ImageUrl = "/Images/QuestionImage/" + dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_ImageLocation"].ToString();
                    //PlaceHolder1.Controls.Add(new LiteralControl("<img id=\"imgPortfolios\"" + (i + 1) + "src=/Images/QuestionImage/\"" + dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_ImageLocation"].ToString()));
                    PlaceHolder1.Controls.Add(img);
                    PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
                    PlaceHolder1.Controls.Add(new LiteralControl("</tr>"));
                }
                PlaceHolder1.Controls.Add(new LiteralControl("<tr colspan=\"6\">"));
                optioncounttemp = 1;
                for (int j = 0; j < dsGetRiskProfileQuestionOption.Tables[0].Rows.Count; j++)
                {
                    PlaceHolder1.Controls.Add(new LiteralControl("<td>"));
                    CheckBox rbtn = new CheckBox();
                    rbtn.ID = "rbtnQ" + (i + 1) + "A" + (j + 1);
                    rbtn.Text = dsGetRiskProfileQuestionOption.Tables[0].Rows[j]["QOM_Option"].ToString();  
                    rbtn.CssClass = "txtField";                    
                    PlaceHolder1.Controls.Add(rbtn);
                    PlaceHolder1.Controls.Add(new LiteralControl("<br/>"));
                    PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
                    
                    optioncounttemp++;
                }
                if (optioncount <= optioncounttemp)
                {
                    optioncount = optioncounttemp;
                }

                PlaceHolder1.Controls.Add(new LiteralControl("</tr>"));
            }
            //PlaceHolder1.Controls.Add(new LiteralControl("</table>"));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
                   
        }
    }
}
