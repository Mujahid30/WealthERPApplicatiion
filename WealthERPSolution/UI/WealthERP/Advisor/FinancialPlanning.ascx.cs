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

namespace WealthERP.Advisor
{
    public partial class FinancialPlanning : System.Web.UI.UserControl
    {
        RiskProfileBo riskprofilebo = new RiskProfileBo();
        
        List<RiskOptionVo> listRiskOptionVo;
        DataSet dsGetRiskProfileQuestion;
        DataSet dsGetRiskProfileQuestionOption;
        DataSet dsGetCustomerDOBById;
        DataSet dsGetCustomerRiskProfile;
        AdvisorVo advisorVo = new AdvisorVo();
        int customerId;
        int GoalCount;
        int AdvisorRMId;
        string riskCode;
        CustomerGoalSetupBo GoalSetupBo = new CustomerGoalSetupBo();
        

        /// <summary>
        /// For Java script coding variables are declared here
        /// </summary>
        public int totalquestion;
        public int optioncount;
        /// <summary>
        /// Javascript coding declaration ends
        /// </summary>
        double cashPercentage;
        DataSet dsGetRiskProfileId;
        RMVo rmvo = new RMVo();
        protected void Page_Init(object sender, EventArgs e)
        {
            dsGetRiskProfileQuestion = riskprofilebo.GetRiskProfileQuestion();
            totalquestion = dsGetRiskProfileQuestion.Tables[0].Rows.Count;
            int optioncounttemp = 1;
            
            //PlaceHolder1.Controls.Add(new LiteralControl("<table>"));
            for (int i = 0; i < totalquestion; i++)
            {
                dsGetRiskProfileQuestionOption = riskprofilebo.GetQuestionOption(int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_QuestionId"].ToString()));
                
                PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl("<td colspan=\"6\"><hr /></td></tr>"));                
                PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl("<td colspan=\"6\">" + (i+1)+"."));
                Label lbl = new Label();
                lbl.ID = "lblQ" + (i+1);
                lbl.Text = "";
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
                    RadioButton rbtn = new RadioButton();
                    rbtn.ID = "rbtnQ" + (i+1) + "A" + (j+1);
                    rbtn.Text = "";
                    rbtn.CssClass = "txtField";
                    rbtn.GroupName = "Q" + (i + 1);
                    PlaceHolder1.Controls.Add(rbtn);
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
            //string querystring = Request.QueryString["pageid"].ToString();
            //if (querystring == "FinancialPlanning")
            //{
            //    Session.Remove("FP_UserName");
            //    Session.Remove("FP_UserID");                
            //}
            SessionBo.CheckSession();  
            if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
            {
                rmvo = (RMVo)Session[SessionContents.RmVo];
                AdvisorRMId = rmvo.RMId;
                txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetCustomerName";
            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
            {
                advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                AdvisorRMId = advisorVo.advisorId;
                txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
            }
            
            rmvo=(RMVo)Session["rmVo"];
            tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
            txtParentCustomer_autoCompleteExtender.ContextKey = AdvisorRMId.ToString(); ;
            try
            {
                dsGetRiskProfileQuestion = riskprofilebo.GetRiskProfileQuestion();


                if (!IsPostBack)
                {
                    tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;

                    //lblDate.Text = DateTime.Today.ToLongDateString();
                    //string xmlPath = Server.MapPath("\\LookUps\\Questions.xml");
                    string imgPath = Server.MapPath("/Images/QuestionImage/");
         
                    //xmlDoc.Load(xmlPath);
                    

                    listRiskOptionVo = new List<RiskOptionVo>();
                  

                    listRiskOptionVo.Clear();
                    for (int i = 0; i < totalquestion; i++)
                    {
                        string labelQuestion = "lblQ" + (i + 1);
                        //PlaceHolder plholder = (PlaceHolder)tabRiskProfiling.FindControl("PlaceHolder1");
                        Label lblQuestion = (Label)tabRiskProfiling.FindControl(labelQuestion);
                        lblQuestion.Text = dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_Question"].ToString();
                        dsGetRiskProfileQuestionOption = riskprofilebo.GetQuestionOption(int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_QuestionId"].ToString()));
                        for (int j = 0; j < dsGetRiskProfileQuestionOption.Tables[0].Rows.Count; j++)
                        {
                            RiskOptionVo riskOptionVo = new RiskOptionVo();
                            string radioBtn = "rbtnQ" + (i + 1) + "A" + (j + 1);
                            RadioButton rbtn = (RadioButton)tabRiskProfiling.FindControl(radioBtn);
                            rbtn.Visible = true;
                            rbtn.Text = dsGetRiskProfileQuestionOption.Tables[0].Rows[j]["QOM_Option"].ToString();
                            riskOptionVo.Option = "Q" + (i + 1) + "A" + (j + 1);
                            riskOptionVo.Value = int.Parse(dsGetRiskProfileQuestionOption.Tables[0].Rows[j]["QOM_Weightage"].ToString());
                            listRiskOptionVo.Add(riskOptionVo);
                        }
                    }

                    //Session["FP_UserName"]="";
                    //Session["FP_UserID"] = "";
                    ViewState["ListRiskOption"] = listRiskOptionVo;
                    AssetFormClear();                  
                   // btnSubmitRisk.Attributes.Add("onClick", "return optionvalidation()");
                    if ((string)Session["FP_UserID"] != "" && (string)Session["FP_UserName"]!= "")
                    {
                        txtPickCustomer.Text = (string)Session["FP_UserName"];
                        txtCustomerId.Value = (string)Session["FP_UserID"];
                        LoadRiskProfiling();
                        LoadAssetAllocation(riskCode);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void LoadAssetAllocation(string riskcode)
        {
            int age = 0;
            try
            {
                
                if (customerId != 0)
                {
                    if (riskcode != null)
                    {
                        dsGetCustomerDOBById = riskprofilebo.GetCustomerDOBById(customerId);

                        
                        if (dsGetCustomerDOBById.Tables[0].Rows[0]["C_DOB"].ToString() != "" && dsGetCustomerDOBById.Tables[0].Rows[0]["C_DOB"].ToString() != null)
                        {
                            DateTime bday = DateTime.Parse(dsGetCustomerDOBById.Tables[0].Rows[0]["C_DOB"].ToString());
                            DateTime now = DateTime.Today;
                            age = now.Year - bday.Year;
                            if (now < bday.AddYears(age))
                            {
                                age--;
                            }
                            lblAgeResult.Text = age.ToString();

                        }
                        else
                        {
                            age = 0;
                            lblAgeResult.Text = "No Age Set";
                        }
                        if (age != 0)
                        {
                            lblAgeResult.Text = age.ToString();

                            lblChartErrorDisplay.Visible = false;
                            if (lblRClass.Text != "" || lblRClass.Text != null)
                            {
                                lblRiskClass.Visible = true;
                                lblRiskScore.Visible = true;
                                lblRClassRs.Text = lblRClass.Text;
                                lblRscoreAA.Text = lblRScore.Text;

                            }
                            else
                            {
                                lblRClassRs.Text = "Fill Risk Profile to know Risk class and Risk Score";
                            }
                            DataSet dsGetAssetAllocationRules = riskprofilebo.GetAssetAllocationRules(riskcode);
                            cashPercentage = double.Parse(dsGetAssetAllocationRules.Tables[0].Rows[0]["WAAR_CashPer"].ToString());
                            double equityAdjustment = double.Parse(dsGetAssetAllocationRules.Tables[0].Rows[0]["WAAR_EquityAdjustmentPer"].ToString());
                            double equitycalc = 0.0;
                            if (lblRClassRs.Text == "Conservative")
                            {
                                //Equity %= (100-Cash)*((100-age)/100)+<equity adjustment from rules table)
                                //Debt %=100-Equity-Cash
                                equitycalc = double.Parse(((100 - double.Parse(age.ToString())) / 100).ToString());
                                txtRecommendedEquity.Text = (double.Parse(((100 - cashPercentage) * equitycalc + (equityAdjustment)).ToString())).ToString();
                                txtRecommendedDebt.Text = (100 - double.Parse(txtRecommendedEquity.Text) - cashPercentage).ToString();
                                txtRecommendedCash.Text = cashPercentage.ToString();
                                lblRiskProfilingParagraph.Text= riskprofilebo.GetRiskProfileText("Conservative");

                            }
                            else if (lblRClassRs.Text == "Moderate")
                            {
                                equitycalc = double.Parse(((100 - double.Parse(age.ToString())) / 100).ToString());
                                txtRecommendedEquity.Text = (double.Parse(((100 - cashPercentage) * equitycalc + (equityAdjustment)).ToString())).ToString();
                                txtRecommendedDebt.Text = (100 - double.Parse(txtRecommendedEquity.Text) - cashPercentage).ToString();
                                txtRecommendedCash.Text = cashPercentage.ToString();
                                lblRiskProfilingParagraph.Text = riskprofilebo.GetRiskProfileText("Moderate");
                            }
                            else if (lblRClassRs.Text == "Aggresive")
                            {
                                equitycalc = double.Parse(((100 - double.Parse(age.ToString())) / 100).ToString());
                                txtRecommendedEquity.Text = (double.Parse(((100 - cashPercentage) * equitycalc + (equityAdjustment)).ToString())).ToString();
                                txtRecommendedDebt.Text = (100 - double.Parse(txtRecommendedEquity.Text) - cashPercentage).ToString();
                                txtRecommendedCash.Text = cashPercentage.ToString();
                                lblRiskProfilingParagraph.Text = riskprofilebo.GetRiskProfileText("Moderate");
                            }

                            //================================
                            //
                            //Chart Control Part is show below
                            //
                            //================================

                            if (txtRecommendedCash.Text != "" && txtRecommendedDebt.Text != "" && txtRecommendedEquity.Text != "")
                            {
                                DataTable dt = new DataTable();
                                DataRow dr;
                                dt.Columns.Add("AssetType");
                                dt.Columns.Add("Value");

                                dr = dt.NewRow();
                                dr[0] = "Equity";
                                dr[1] = txtRecommendedEquity.Text;
                                dt.Rows.Add(dr);

                                dr = dt.NewRow();
                                dr[0] = "Debt";
                                dr[1] = txtRecommendedDebt.Text;
                                dt.Rows.Add(dr);

                                dr = dt.NewRow();
                                dr[0] = "Cash";
                                dr[1] = txtRecommendedCash.Text;
                                dt.Rows.Add(dr);

                                tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 1;
                                Series seriesAssets = new Series("sActualAsset");
                                seriesAssets.ChartType = SeriesChartType.Pie;
                                cActualAsset.Visible = true;
                                cActualAsset.Series.Clear();
                                cActualAsset.Series.Add(seriesAssets);
                                cActualAsset.DataSource = dt;
                                cActualAsset.Series[0].XValueMember = "AssetType";
                                cActualAsset.Series[0].YValueMembers = "Value";

                                // Enable X axis margin
                                cActualAsset.ChartAreas["caActualAsset"].AxisX.IsMarginVisible = true;
                                cActualAsset.BackColor = Color.Transparent;
                                cActualAsset.ChartAreas[0].BackColor = Color.Transparent;
                                cActualAsset.ChartAreas[0].Area3DStyle.Enable3D = true;
                                cActualAsset.ChartAreas[0].Area3DStyle.Perspective = 50;
                                cActualAsset.DataBind();

                            }
                        }
                        else
                        {
                            tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 1;
                            lblChartErrorDisplay.Visible = true;
                            AssetFormClear();
                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('No age set for this customer');", true);
                        }
                        trCurrentAssetAllocation.Visible = true;
                        ShowCurrentAssetAllocationPieChart();
                    }
                    else
                    {
                        tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
                        AssetFormClear();
                        trCurrentAssetAllocation.Visible = false;
                    }
                }

                # region Dont need this
               
                # endregion
                if (customerId != 0 && age!=0)
                {
                    lblCustomerParagraph.Text = riskprofilebo.GetAssetAllocationText(customerId);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SetCustomerId()
        {
            try
            {
                if (txtCustomerId.Value != "")
                {
                    customerId = int.Parse(txtCustomerId.Value);
                }
                dsGetCustomerRiskProfile = riskprofilebo.GetCustomerRiskProfile(customerId);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            
        }
        protected void btnSubmitRisk_Click(object sender, EventArgs e)
        {
           
            string tempRID = "";
            int rScore = 0;           
            DataSet dsGetRiskProfileRules;
            lblRiskProfileDate.Visible = true;
            lblRiskProfileDate.Text = DateTime.Now.ToShortDateString();
            listRiskOptionVo = (List<RiskOptionVo>)ViewState["ListRiskOption"];
            SetCustomerId();
            try
            {
                if (txtPickCustomer.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Choose Customer First');", true);
                    RiskFormClear();
                }
                else
                {

                    for (int i = 0; i < dsGetRiskProfileQuestion.Tables[0].Rows.Count; i++)
                    {
                        dsGetRiskProfileQuestionOption = riskprofilebo.GetQuestionOption(int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_QuestionId"].ToString()));
                        for (int j = 0; j < dsGetRiskProfileQuestionOption.Tables[0].Rows.Count; j++)
                        {
                            tempRID = "rbtnQ" + (i + 1) + "A" + (j + 1);
                            RadioButton rbtnQAns = (RadioButton)tabRiskProfiling.FindControl(tempRID);
                            if (rbtnQAns != null && rbtnQAns.Checked)
                            {
                                for (int rCount = 0; rCount < listRiskOptionVo.Count; rCount++)
                                {
                                    if (listRiskOptionVo[rCount].Option == "Q" + (i + 1) + "A" + (j + 1))
                                    {
                                        rScore = rScore + listRiskOptionVo[rCount].Value;
                                        break;
                                    }
                                }
                            }

                        }
                    }
                    dsGetRiskProfileRules = riskprofilebo.GetRiskProfileRules();
                    tblRiskScore.Visible = true;
                    lblRScore.Visible = true;
                    lblRClass.Visible = true;
                    lblRScore.Text = rScore.ToString();
                    for (int i = 0; i < dsGetRiskProfileRules.Tables[0].Rows.Count; i++)
                    {
                        int minLimit = int.Parse(dsGetRiskProfileRules.Tables[0].Rows[i]["WRPR_RiskScoreLowerLimit"].ToString());
                        int maxLimit = 0;
                        if (dsGetRiskProfileRules.Tables[0].Rows[i]["WRPR_RiskScoreUpperLimit"].ToString() != null && dsGetRiskProfileRules.Tables[0].Rows[i]["WRPR_RiskScoreUpperLimit"].ToString() !="" )
                        {
                            maxLimit = int.Parse(dsGetRiskProfileRules.Tables[0].Rows[i]["WRPR_RiskScoreUpperLimit"].ToString());
                        }
                        else
                        {
                            maxLimit = 35000;
                        }
                        if (rScore >= 14)
                        {
                            if (rScore >= minLimit && rScore <= maxLimit)
                            {
                                riskCode = dsGetRiskProfileRules.Tables[0].Rows[i]["XRC_RiskClassCode"].ToString();
                                break;
                            }
                        }
                        else
                        {
                            rScore = 14;
                            if (rScore >= minLimit && rScore <= maxLimit)
                            {
                                riskCode = dsGetRiskProfileRules.Tables[0].Rows[i]["XRC_RiskClassCode"].ToString();
                                break;
                            }
                        }
                    }
                    DataSet dsRiskClass = riskprofilebo.GetRiskClass(riskCode);
                    lblRClass.Text = dsRiskClass.Tables[0].Rows[0]["XRC_RiskClass"].ToString();

                    if (lblRClass.Text == "Aggresive")
                    {

                        lblRClass.BackColor = System.Drawing.Color.Green;
                        lblRScore.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (lblRClass.Text == "Moderate")
                    {


                        lblRClass.BackColor = System.Drawing.Color.Yellow;
                        lblRScore.ForeColor = System.Drawing.Color.Yellow;
                    }
                    else if (lblRClass.Text == "Conservative")
                    {
                        lblRClass.BackColor = System.Drawing.Color.Red;
                        lblRScore.ForeColor = System.Drawing.Color.Red;
                    }



                    //====================================
                    //
                    // Adding Customer Risk profile to database
                    //
                    //====================================
                    

                    riskprofilebo.AddCustomerRiskProfileDetails(customerId, rScore, DateTime.Now, riskCode, rmvo);
                    dsGetRiskProfileId = riskprofilebo.GetRpId(customerId);

                    //====================================
                    //
                    // Adding Risk response to question
                    //
                    //====================================
                    for (int i = 0; i < dsGetRiskProfileQuestion.Tables[0].Rows.Count; i++)
                    {
                        dsGetRiskProfileQuestionOption = riskprofilebo.GetQuestionOption(int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_QuestionId"].ToString()));
                        for (int j = 0; j < dsGetRiskProfileQuestionOption.Tables[0].Rows.Count; j++)
                        {

                            tempRID = "rbtnQ" + (i + 1) + "A" + (j + 1);

                            RadioButton rbtnQAns = (RadioButton)tabRiskProfiling.FindControl(tempRID);

                            if (rbtnQAns != null && rbtnQAns.Checked)
                            {
                                riskprofilebo.AddCustomerResponseToQuestion(int.Parse(dsGetRiskProfileId.Tables[0].Rows[0]["CRP_RiskProfileId"].ToString()), int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_QuestionId"].ToString()), int.Parse(dsGetRiskProfileQuestionOption.Tables[0].Rows[j]["QOM_OptionId"].ToString()), rmvo);
                            }
                        }
                    }
                    tblRiskScore.Focus();                    
                 
                        if (hidGoalCount.Value != "" && hidGoalCount.Value != "0")
                            GoalSetupBo.SetCustomerAllGoalDeActive(customerId);
                    

                    LoadAssetAllocation(riskCode);
                    AddToAssetAllocation();
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void ddlDependent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlGoal_SelectedIndexChanged(object sender, EventArgs e)
        {
            //divCalculator.Visible = true;
            //tblCalculator.Visible = true;
            //tblOutPut.Visible = false;
            //switch (ddlGoal.SelectedValue.ToString())
            //{
            //    case "Home":
            //        divRetirement.Visible = false;
            //        divCalculator.Visible = true;
            //        ddlDependent.Visible = false;
            //        lblObjective.Text = "Objective: Home";
            //        lblCostToday.Text = "Home Cost Today:";
            //        txtCostToday.Text = "";
            //        txtCurrentValue.Text = "";
            //        txtRateInterest.Text = "";
            //        txtRequiredAfter.Text = "";
            //        txtRequiredRate.Text = "";

            //        break;
            //    case "Education":
            //        divRetirement.Visible = false;
            //        divCalculator.Visible = true;
            //        ddlDependent.Visible = true;
            //        lblObjective.Text = "Objective: Education Of";
            //        lblCostToday.Text = "Education Cost Today:";
            //        txtCostToday.Text = "";
            //        txtCurrentValue.Text = "";
            //        txtRateInterest.Text = "";
            //        txtRequiredAfter.Text = "";
            //        txtRequiredRate.Text = "";
            //        break;
            //    case "Marriage":
            //        divRetirement.Visible = false;
            //        divCalculator.Visible = true;
            //        ddlDependent.Visible = true;
            //        lblObjective.Text = "Objective: Marriage Of";
            //        lblCostToday.Text = "Mariage Cost Today:";
            //        txtCostToday.Text = "";
            //        txtCurrentValue.Text = "";
            //        txtRateInterest.Text = "";
            //        txtRequiredAfter.Text = "";
            //        txtRequiredRate.Text = "";
            //        break;
            //    case "Retirement":
            //        divRetirement.Visible = true;
            //        divCalculator.Visible = false;
            //        ddlDependent.Visible = true;
                    
            //        break;
            //    default:

            //        break;
            //}

        }
        /// <summary>/// Calculates the present value of a loan based upon constant payments and a constant interest rate.
        /// </summary>
        /// <param name="rate">The interest rate.</param>
        /// <param name="nper">Total number of payments.</param>
        /// <param name="pmt">payment made each period</param>
        /// <param name="fv">Future value.</param>
        /// <param name="type">Indicates when payments are due. 0 = end of period, 1 = beginning of period.</param>
        /// <returns>The Present Value</returns>
        public static double PV(double rate, double nper, double pmt, double fv, double type)
        {
            if (rate == 0.0)
                return (-fv - (pmt * nper));
            else
                return (pmt * (1.0 + rate * type) * (1.0 - Math.Pow((1.0 + rate), nper)) / rate - fv) / Math.Pow((1.0 + rate), nper);
        }
        public double FutureValue(double rate, double nper, double pmt, double pv, int type)
        {
            double result = 0;
            result = System.Numeric.Financial.Fv(rate, nper, pmt, pv, 0);
            return result;
        }
        public double PMT(double rate, double nper, double pv, double fv, int type)
        {
            double result = 0;
            result = System.Numeric.Financial.Pmt(rate, nper, pv, fv, 0);
            return result;
        }
        protected void btnCalSubmit_Click(object sender, EventArgs e)
        {
        }      

        protected void btnRCalc_Click(object sender, EventArgs e)
        {
            //int yearsLeft = 0;
            //int targetRetirementYear = 0;
            //double amountRequired = 0;
            //double futureValue = 0;
            //double retirementCorpus = 0;
            //double monthlySavings = 0;

            //double annualRequirement=double.Parse(txtAnnReuirement.Text);
            //double currentValue=double.Parse(txtInvRetirement.Text);
            //double exRate=double.Parse(txtRateExInv.Text)/100;
            //double newRate=double.Parse(txtRateNewInv.Text)/100;
            //double retRetirementCorpus=double.Parse(txtRetRCorpus.Text)/100;

            //if (txtTRY.Text != "")
            //{
            //    targetRetirementYear = int.Parse(txtTRY.Text);
            //    yearsLeft = targetRetirementYear - DateTime.Now.Year;
            //}
            //amountRequired = annualRequirement / retRetirementCorpus;
            //futureValue = Math.Abs(FutureValue(exRate, yearsLeft, 0, currentValue, 0));
            //retirementCorpus = Math.Abs(FutureValue(0.06, yearsLeft, 0, amountRequired, 0));
            //monthlySavings = Math.Abs(PMT((newRate / 12), (yearsLeft * 12), 0, (retirementCorpus - futureValue), 0));

            //tblRetireOutPut.Visible = true;
            //lblYLRVal.Text = yearsLeft.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            //lblAmountRequiredVal.Text = amountRequired.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            //lblValCurrentInv.Text = "Value of current investments after " + yearsLeft + " Years:";
            //lblValCurrentInvVal.Text = futureValue.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            //lblRetCorpus.Text = "Retirement Corpus after " + yearsLeft + "  Years:";
            //lblRetCorpusVal.Text = retirementCorpus.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            //lblMonthlySavingsVal.Text = monthlySavings.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
        }
        protected void txtPickCustomer_TextChanged(object sender, EventArgs e)
        {
            trRiskProfilingParagraph.Visible = true;
            trCustomerAssetText.Visible = true;
            LoadRiskProfiling();         
       
        }
        protected void LoadRiskProfiling()
        {
            tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
            DataSet dsGetRiskClassForRisk;
            DataSet dsGetAssetAllocationDetails;            
            try
            {
                AssetFormClear();
                if (txtCustomerId.Value != "")
                {
                    SetCustomerId();
                    //dsGetCustomerIdByName = riskprofilebo.GetCustomerIdByName(txtPickCustomer.Text);                   
                    dsGetCustomerRiskProfile = riskprofilebo.GetCustomerRiskProfile(customerId);

                    if (dsGetCustomerRiskProfile.Tables[0].Rows.Count != 0 && dsGetCustomerRiskProfile != null)
                    {
                        dsGetAssetAllocationDetails = riskprofilebo.GetAssetAllocationDetails(int.Parse(dsGetCustomerRiskProfile.Tables[0].Rows[0]["CRP_RiskProfileId"].ToString()));
                        tblRiskScore.Visible = true;
                        lblRScore.Visible = true;
                        lblRScore.Text = dsGetCustomerRiskProfile.Tables[0].Rows[0]["CRP_Score"].ToString();
                        lblRClass.Visible = true;
                        riskCode = dsGetCustomerRiskProfile.Tables[0].Rows[0]["XRC_RiskClassCode"].ToString();
                        dsGetRiskClassForRisk = riskprofilebo.GetRiskClassForRisk(dsGetCustomerRiskProfile.Tables[0].Rows[0]["XRC_RiskClassCode"].ToString());
                        lblRClass.Text = dsGetRiskClassForRisk.Tables[0].Rows[0]["XRC_RiskClass"].ToString();
                        lblRiskProfileDate.Visible = true;
                        DateTime riskdate = (DateTime)dsGetCustomerRiskProfile.Tables[0].Rows[0]["CRP_Date"];
                        lblRiskProfileDate.Text = riskdate.ToShortDateString();


                        string tempRID = "";
                        for (int i = 1; i <= totalquestion; i++)
                        {
                            dsGetRiskProfileQuestionOption = riskprofilebo.GetQuestionOption(int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i - 1]["QM_QuestionId"].ToString()));
                            for (int j = 1; j <= dsGetRiskProfileQuestionOption.Tables[0].Rows.Count; j++)
                            {
                                tempRID = "rbtnQ" + i + "A" + j;

                                RadioButton rbtnQAns = (RadioButton)tabRiskProfiling.FindControl(tempRID);
                                if (rbtnQAns.Checked == true)
                                {
                                    rbtnQAns.Checked = false;
                                }
                                string questionoption = dsGetCustomerRiskProfile.Tables[1].Rows[i - 1]["QOM_Option"].ToString();
                                if (rbtnQAns.Text == questionoption)
                                {
                                    rbtnQAns.Checked = true;
                                }

                            }
                        }
                        if (dsGetAssetAllocationDetails.Tables[0].Rows.Count != 0)
                        {
                            DateTime approvedbycustomeron = (DateTime)dsGetAssetAllocationDetails.Tables[0].Rows[0]["CAA_ClientApprovedOn"];
                            txtApprovedByCustomerOn.Text = approvedbycustomeron.ToShortDateString();
                            LoadAssetAllocation(riskCode);
                        }
                        else
                        {
                            LoadAssetAllocation(riskCode);
                        }



                        if (lblRClass.Text == "Aggressive")
                        {

                            lblRClass.BackColor = System.Drawing.Color.Green;
                            lblRScore.ForeColor = System.Drawing.Color.Green;
                        }
                        else if (lblRClass.Text == "Moderate")
                        {


                            lblRClass.BackColor = System.Drawing.Color.Yellow;
                            lblRScore.ForeColor = System.Drawing.Color.Yellow;
                        }
                        else if (lblRClass.Text == "Conservative")
                        {
                            lblRClass.BackColor = System.Drawing.Color.Red;
                            lblRScore.ForeColor = System.Drawing.Color.Red;
                        }


                        //SetAdjustment();

                        Session["FP_UserName"] = txtPickCustomer.Text;
                        Session["FP_UserID"] = txtCustomerId.Value;
                        GoalCount = GoalSetupBo.CheckGoalProfile(customerId);
                        hidGoalCount.Value = GoalCount.ToString();
                    }

                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('No Risk profile for this customer');", true);
                        trRiskProfilingParagraph.Visible = false;
                        trCustomerAssetText.Visible = false;
                        Session["FP_UserName"] = txtPickCustomer.Text;
                        Session["FP_UserID"] = txtCustomerId.Value;
                        GoalCount = GoalSetupBo.CheckGoalProfile(customerId);
                        hidGoalCount.Value = GoalCount.ToString();
                        RiskFormClear();
                        AssetFormClear();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void AddToAssetAllocation()
        {
            string approvedon = txtApprovedByCustomerOn.Text;            
            DateTime now;
            //dsGetCustomerRiskProfile;
            //dsGetCustomerIdByName = riskprofilebo.GetCustomerIdByName(txtPickCustomer.Text);            
            //dsGetCustomerRiskProfile = riskprofilebo.GetCustomerRiskProfile(int.Parse(dsGetCustomerIdByName.Tables[0].Rows[0]["C_CustomerId"].ToString()));
            try
            {
                if (txtApprovedByCustomerOn.Text == "")
                {
                    now = DateTime.Now;
                }
                else
                {
                    now = DateTime.Parse(approvedon);
                }
                SetCustomerId();

                if (txtRecommendedCash.Text != "" && txtRecommendedEquity.Text != "" && txtRecommendedDebt.Text != "")
                {
                    riskprofilebo.AddAssetAllocationDetails(int.Parse(dsGetCustomerRiskProfile.Tables[0].Rows[0]["CRP_RiskProfileId"].ToString()), double.Parse(txtRecommendedCash.Text), double.Parse(txtRecommendedEquity.Text), double.Parse(txtRecommendedDebt.Text), now, rmvo);
                }
                else
                {
                    riskprofilebo.AddAssetAllocationDetails(int.Parse(dsGetCustomerRiskProfile.Tables[0].Rows[0]["CRP_RiskProfileId"].ToString()), 0.0, 0.0, 0.0, now, rmvo);
                }
                    
                    riskCode = dsGetCustomerRiskProfile.Tables[0].Rows[0]["XRC_RiskClassCode"].ToString();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string approvedon=txtApprovedByCustomerOn.Text;
            
            DateTime now;
            try
            {
                if (txtApprovedByCustomerOn.Text == "")
                {
                    now = DateTime.Now;
                }
                else
                {
                    now = DateTime.Parse(approvedon);
                }
                SetCustomerId();
                //dsGetCustomerIdByName = riskprofilebo.GetCustomerIdByName(txtPickCustomer.Text);
                if (txtRecommendedCash.Text != "" && txtRecommendedCash.Text != "" && txtRecommendedDebt.Text != "")
                {
                    riskprofilebo.UpdateAssetAllocationDetails(int.Parse(dsGetCustomerRiskProfile.Tables[0].Rows[0]["CRP_RiskProfileId"].ToString()), double.Parse(txtRecommendedCash.Text), double.Parse(txtRecommendedEquity.Text), double.Parse(txtRecommendedDebt.Text), now, rmvo);
                }
                else
                {
                    riskprofilebo.UpdateAssetAllocationDetails(int.Parse(dsGetCustomerRiskProfile.Tables[0].Rows[0]["CRP_RiskProfileId"].ToString()), 0.0,0.0,0.0, now, rmvo);
                }
                riskCode = dsGetCustomerRiskProfile.Tables[0].Rows[0]["XRC_RiskClassCode"].ToString();
                LoadAssetAllocation(riskCode);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RiskFormClear()
        {
            tblRiskScore.Visible = false;
            lblRScore.Visible = false;
            lblRScore.Text = "";
            lblRClass.Visible = false;
            lblRClass.Text = "";
            lblRiskProfileDate.Visible = false;
            lblRiskProfileDate.Text = "";
            string tempRID = "";

            for (int i = 1; i <= dsGetRiskProfileQuestion.Tables[0].Rows.Count; i++)
            {
                dsGetRiskProfileQuestionOption = riskprofilebo.GetQuestionOption(int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i - 1]["QM_QuestionId"].ToString()));
                for (int j = 1; j <= dsGetRiskProfileQuestionOption.Tables[0].Rows.Count; j++)
                {
                    tempRID = "rbtnQ" + i + "A" + j;

                    RadioButton rbtnQAns = (RadioButton)tabRiskProfiling.FindControl(tempRID);
                    if (rbtnQAns.Checked == true)
                    {
                        rbtnQAns.Checked = false;
                    }
                }
            }
        }

        protected void AssetFormClear()
        {
            
            lblAgeResult.Text = "";
            lblRiskClass.Visible = false;
            lblRiskScore.Visible = false;
            
            lblRClassRs.Text = "";
            lblRscoreAA.Text = "";
            txtApprovedByCustomerOn.Text = "";
            txtRecommendedEquity.Text = "";
            txtRecommendedDebt.Text = "";
            txtRecommendedCash.Text = "";
            cActualAsset.Visible = false;
            ChartCurrentAsset.Visible = false;
            txtCurrentCash.Text = "";
            txtCurrentDebt.Text = "";
            txtCurrentEquity.Text = "";

            
            
        }

        protected void ShowCurrentAssetAllocationPieChart()
        {
            DataSet DScurrentAsset=new DataSet();
            DScurrentAsset = riskprofilebo.GetCurrentAssetAllocation(customerId);
                DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("AssetType");
            dt.Columns.Add("Value");
            if (DScurrentAsset.Tables[0].Rows.Count > 0)
            {


                dr = dt.NewRow();
                dr[0] = "Equity";
                if (DScurrentAsset.Tables[0].Rows[0]["Equity"].ToString() != "")
                {
                    txtCurrentEquity.Text = Math.Round(double.Parse(DScurrentAsset.Tables[0].Rows[0]["Equity"].ToString()), 2).ToString();
                    dr[1] = DScurrentAsset.Tables[0].Rows[0]["Equity"];
                }
                else
                {
                    txtCurrentEquity.Text = "0";
                    dr[1] = "0";
                }
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[0] = "Debt";
                if (DScurrentAsset.Tables[0].Rows[0]["Debt"].ToString() != "")
                {
                    txtCurrentDebt.Text = Math.Round(double.Parse(DScurrentAsset.Tables[0].Rows[0]["Debt"].ToString()), 2).ToString();
                    dr[1] = DScurrentAsset.Tables[0].Rows[0]["Debt"];
                }
                else
                {
                    txtCurrentDebt.Text = "0";
                    dr[1] = "0";
                }

                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[0] = "Cash";
                if (DScurrentAsset.Tables[0].Rows[0]["Cash"].ToString() != "")
                {
                    txtCurrentCash.Text = Math.Round(double.Parse(DScurrentAsset.Tables[0].Rows[0]["Cash"].ToString()), 2).ToString();
                    dr[1] = DScurrentAsset.Tables[0].Rows[0]["Cash"];
                }
                else
                {
                    txtCurrentCash.Text = "0";
                    dr[1] = "0";
                }

                dt.Rows.Add(dr);

            }
            else
            {
                dr = dt.NewRow();
                dr[0] = "Equity";                
                txtCurrentEquity.Text = "0";
                dr[1] = "0";                
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[0] = "Debt";                
                txtCurrentDebt.Text = "0";
                dr[1] = "0";              
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[0] = "Cash";                
                txtCurrentCash.Text = "0";
                dr[1] = "0";              
                dt.Rows.Add(dr);
            }

            Series seriesAssets = new Series("sActualAsset");
            seriesAssets.ChartType = SeriesChartType.Pie;
            ChartCurrentAsset.Visible = true;
            ChartCurrentAsset.Series.Clear();
            ChartCurrentAsset.Series.Add(seriesAssets);
            ChartCurrentAsset.DataSource = dt;
            ChartCurrentAsset.Series[0].XValueMember = "AssetType";
            ChartCurrentAsset.Series[0].YValueMembers = "Value";

            // Enable X axis margin
            ChartCurrentAsset.ChartAreas["caActualAsset"].AxisX.IsMarginVisible = true;
            ChartCurrentAsset.BackColor = Color.Transparent;
            ChartCurrentAsset.ChartAreas[0].BackColor = Color.Transparent;
            ChartCurrentAsset.ChartAreas[0].Area3DStyle.Enable3D = true;
            ChartCurrentAsset.ChartAreas[0].Area3DStyle.Perspective = 50;
            ChartCurrentAsset.DataBind();
 
        }



    }
}
