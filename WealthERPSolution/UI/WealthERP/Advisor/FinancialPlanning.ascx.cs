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
using BoFPSuperlite;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;


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
        DataTable dtRecommendedAllocation;
        DataTable dtCurrentAssetAllocation;
        AdvisorVo advisorVo = new AdvisorVo();
        int customerId;
        int GoalCount;
        int AdvisorRMId;
        string riskCode;
        int isProspect;
        CustomerVo customerVo;
        CustomerBo customerBo;
        CustomerGoalSetupBo GoalSetupBo = new CustomerGoalSetupBo();
        DataSet dsGlobal = new DataSet();
        DataSet dsGetRiskProfileRules;
        int rScore = 0;
        int age = 0;

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
        DataSet DScurrentAsset = new DataSet();
        protected void Page_Init(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            dsGetRiskProfileQuestion = riskprofilebo.GetRiskProfileQuestion(advisorVo.advisorId);
            totalquestion = dsGetRiskProfileQuestion.Tables[0].Rows.Count;
            int optioncounttemp = 1;
            hidGoalCount.Value = null;
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
                    RadioButton rbtn = new RadioButton();
                    rbtn.ID = "rbtnQ" + (i + 1) + "A" + (j + 1);
                    rbtn.Text = dsGetRiskProfileQuestionOption.Tables[0].Rows[j]["QOM_Option"].ToString();
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

        //protected void TriggerAnswers()
        // {
        //     advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
        //     dsGetRiskProfileQuestion = riskprofilebo.GetRiskProfileQuestion(advisorVo.advisorId);
        //     totalquestion = dsGetRiskProfileQuestion.Tables[0].Rows.Count;
        //     int optioncounttemp = 1;
        //     hidGoalCount.Value = null;
        //     if (!Page.IsPostBack)
        //     {
        //         if (Session[SessionContents.FPS_ProspectList_CustomerId] != null && Session[SessionContents.FPS_ProspectList_CustomerId].ToString() != "")
        //         {
        //             customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
        //             Session["FP_UserID"] = customerId;
        //         }
        //         customerBo = new CustomerBo();
        //         customerVo = customerBo.GetCustomer(customerId);
        //         Session[SessionContents.CustomerVo] = customerVo;
        //     }

        //     //PlaceHolder1.Controls.Add(new LiteralControl("<table>"));
        //     for (int i = 0; i < totalquestion; i++)
        //     {
        //         dsGetRiskProfileQuestionOption = riskprofilebo.GetQuestionOption(int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_QuestionId"].ToString()), advisorVo.advisorId);

        //         PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
        //         PlaceHolder1.Controls.Add(new LiteralControl("<td colspan=\"6\"><hr /></td></tr>"));
        //         PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
        //         PlaceHolder1.Controls.Add(new LiteralControl("<td colspan=\"6\">" + (i + 1) + "."));
        //         Label lbl = new Label();
        //         lbl.ID = "lblQ" + (i + 1);
        //         lbl.Text = dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_Question"].ToString();
        //         lbl.CssClass = "FieldName";
        //         PlaceHolder1.Controls.Add(lbl);
        //         PlaceHolder1.Controls.Add(new LiteralControl("</td></tr>"));
        //         if (int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_IsImageAttached"].ToString()) == 1)
        //         {
        //             PlaceHolder1.Controls.Add(new LiteralControl("<tr>"));
        //             PlaceHolder1.Controls.Add(new LiteralControl("<td colspan=\"6\">"));
        //             //<img id="imgPortfolios" src="/Images/Portfolios.jpg" />
        //             System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
        //             img.ID = "imgPortfolios" + (i + 1);
        //             img.ImageUrl = "/Images/QuestionImage/" + dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_ImageLocation"].ToString();
        //             //PlaceHolder1.Controls.Add(new LiteralControl("<img id=\"imgPortfolios\"" + (i + 1) + "src=/Images/QuestionImage/\"" + dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_ImageLocation"].ToString()));
        //             PlaceHolder1.Controls.Add(img);
        //             PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
        //             PlaceHolder1.Controls.Add(new LiteralControl("</tr>"));
        //         }
        //         PlaceHolder1.Controls.Add(new LiteralControl("<tr colspan=\"6\">"));
        //         optioncounttemp = 1;
        //         for (int j = 0; j < dsGetRiskProfileQuestionOption.Tables[0].Rows.Count; j++)
        //         {
        //             PlaceHolder1.Controls.Add(new LiteralControl("<td>"));
        //             RadioButton rbtn = new RadioButton();
        //             rbtn.ID = "rbtnQ" + (i + 1) + "A" + (j + 1);
        //             rbtn.Text = dsGetRiskProfileQuestionOption.Tables[0].Rows[j]["QOM_Option"].ToString(); 
        //             rbtn.CssClass = "txtField";
        //             rbtn.GroupName = "Q" + (i + 1);
        //             PlaceHolder1.Controls.Add(rbtn);
        //             PlaceHolder1.Controls.Add(new LiteralControl("</td>"));
        //             optioncounttemp++;
        //         }
        //         if (optioncount <= optioncounttemp)
        //         {
        //             optioncount = optioncounttemp;
        //         }

        //         PlaceHolder1.Controls.Add(new LiteralControl("</tr>"));
        //     }
        //     //PlaceHolder1.Controls.Add(new LiteralControl("</table>"));
        // }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
            {
                rmvo = (RMVo)Session[SessionContents.RmVo];
                AdvisorRMId = rmvo.RMId;
            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
            {

                AdvisorRMId = advisorVo.advisorId;
            }
            rmvo = (RMVo)Session["rmVo"];
            //tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
            AssetFormClear();

            try
            {
                if (Session[SessionContents.FPS_ProspectList_CustomerId] != null && Session[SessionContents.FPS_ProspectList_CustomerId].ToString() != "")
                {
                    customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                    Session["FP_UserID"] = customerId;
                }
                customerBo = new CustomerBo();
                customerVo = customerBo.GetCustomer(customerId);
                Session[SessionContents.CustomerVo] = customerVo;

                dsGetRiskProfileRules = riskprofilebo.GetRiskProfileRules(advisorVo.advisorId);
                
                if (!IsPostBack)
                {
                    Span12.Visible = false;
                    dsGetCustomerRiskProfile = riskprofilebo.GetCustomerRiskProfile(customerId, advisorVo.advisorId);
                    if (dsGetCustomerRiskProfile.Tables[0].Rows.Count > 0)
                        riskCode = dsGetCustomerRiskProfile.Tables[0].Rows[0]["XRC_RiskClassCode"].ToString();

                    dsGetRiskProfileId = riskprofilebo.GetRpId(customerId);
                    if (dsGetRiskProfileId.Tables[0].Rows[0]["CRP_RiskProfileId"].ToString() != "")
                    {
                        if (dsGetCustomerRiskProfile.Tables[1].Rows.Count > 0)
                        {
                            divQuestionAnswers.Visible = true;
                            tblRiskScore.Visible = true;
                            rbtnAnsQuestions.Checked = true;
                            lblRScore.Visible = true;
                            btnSubmitRisk.Visible = true;
                            trRiskProfiler.Visible = true;

                            tblPickRiskClass.Visible = false;
                            ddlPickRiskClass.Visible = false;
                            lblPickRiskPlass.Visible = false;
                            rbtnPickRiskclass.Checked = false;
                            Td1.Visible = true;
                            btnSubmitForPickRiskclass.Visible = false;
                            tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
                        }
                        else
                        {
                            tblPickRiskClass.Visible = true;
                            ddlPickRiskClass.Visible = true;
                            lblPickRiskPlass.Visible = true;
                            rbtnPickRiskclass.Checked = true;
                            btnSubmitForPickRiskclass.Visible = true;


                            BindRiskClasses();
                            if (dsGetCustomerRiskProfile.Tables[0].Rows[0]["XRC_RiskClassCode"].ToString() != "")
                                ddlPickRiskClass.SelectedValue = dsGetCustomerRiskProfile.Tables[0].Rows[0]["XRC_RiskClassCode"].ToString();

                            divQuestionAnswers.Visible = false;
                            tblRiskScore.Visible = false;
                            rbtnAnsQuestions.Checked = false;
                            trRiskProfiler.Visible = false;
                            lblRScore.Visible = false;
                            Td1.Visible = false;
                            btnSubmitRisk.Visible = false;

                            tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
                        }

                    }
                    else
                    {
                        divQuestionAnswers.Visible = false;
                        tblPickRiskClass.Visible = false;
                        btnSubmitRisk.Visible = false;
                        trRiskProfiler.Visible = false;
                        btnSubmitForPickRiskclass.Visible = false;

                    }
                    tblPickOptions.Visible = true;
                    //ddlPickRiskClass.Visible = false;
                    //trRiskProfiler.Visible = false;
                    //lblPickRiskPlass.Visible = false;
                    //btnSubmitRisk.Visible = false;
                    //btnSubmitForPickRiskclass.Visible = false;
                    PickQuestions();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void PickQuestions()
        {
            dsGetRiskProfileQuestion = riskprofilebo.GetRiskProfileQuestion(advisorVo.advisorId);

            tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
            string imgPath = Server.MapPath("/Images/QuestionImage/");

            listRiskOptionVo = new List<RiskOptionVo>();

            listRiskOptionVo.Clear();
            for (int i = 0; i < totalquestion; i++)
            {
                string labelQuestion = "lblQ" + (i + 1);
                Label lblQuestion = (Label)tabRiskProfiling.FindControl(labelQuestion);
                lblQuestion.Text = dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_Question"].ToString();
                dsGetRiskProfileQuestionOption = riskprofilebo.GetQuestionOption(int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_QuestionId"].ToString()), advisorVo.advisorId);
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
            ViewState["ListRiskOption"] = listRiskOptionVo;

            if (Session["FP_UserID"] != null && Session["FP_UserID"].ToString() != "")
            {
                cActualAsset.Visible = true;
                ChartCurrentAsset.Visible = true;
                LoadRiskProfiling();
                //LoadAssetAllocation(riskCode);
            }
            Trigger();
        }


        // ..... Commented by Vinayak Patil ..... //
        // ..... Reason: From RE: 3.0 Recomonded asset allocation assets calculation getting from database So UI Calculation logic has been removed ..... //

        // Comment starts .. 
        // {

        //public void LoadAssetAllocation(string riskcode)
        //{
        //    int age = 0;
        //    try
        //    {
        //        if (customerId != 0)
        //        {
        //            if (riskcode != null)
        //            {
        //                dsGetCustomerDOBById = riskprofilebo.GetCustomerDOBById(customerId);

        //                if (dsGetCustomerDOBById.Tables[0].Rows[0]["C_DOB"].ToString() != "" && dsGetCustomerDOBById.Tables[0].Rows[0]["C_DOB"].ToString() != null)
        //                {
        //                    DateTime bday = DateTime.Parse(dsGetCustomerDOBById.Tables[0].Rows[0]["C_DOB"].ToString());
        //                    DateTime now = DateTime.Today;
        //                    age = now.Year - bday.Year;
        //                    if (now < bday.AddYears(age))
        //                    {
        //                        age--;
        //                    }
        //                    lblAgeResult.Text = age.ToString();

        //                }
        //                else
        //                {
        //                    age = 0;
        //                    lblAgeResult.Text = "No Age Set";
        //                    lblAgeErrormsg.Visible = true;
        //                }
        //                lblAgeResult.Text = age.ToString();

        //                lblChartErrorDisplay.Visible = false;
        //                if (lblRClass.Text != "" || lblRClass.Text != null)
        //                {

        //                    lblRiskClass.Visible = true;
        //                    lblRiskScore.Visible = true;
        //                    lblRClassRs.Text = lblRClass.Text;
        //                    lblRscoreAA.Text = lblRScore.Text;
        //                    Session["Score"] = lblRscoreAA.Text;
        //                }
        //                else
        //                {
        //                    lblRClassRs.Text = "Fill Risk Profile to know Risk class and Risk Score";
        //                }
        //                DataSet dsGetAssetAllocationRules = riskprofilebo.GetAssetAllocationRules(riskcode, advisorVo.advisorId);
        //                DataTable dtAsset = new DataTable();

        //                dtAsset.Columns.Add("AssetType");
        //                dtAsset.Columns.Add("Percentage");
        //                dtAsset.Columns.Add("AssetTypeCode");
        //                DataRow drAsset;
        //                double equityAdjustment = 0;
        //                double equitycalc = 0.0;
        //                double equityPercentage = 0;
        //                double debtpercentage = 0;
        //                if (age != 0 && (dsGetAssetAllocationRules.Tables[0].Rows[0]["A_AdviserId"].ToString() == advisorVo.advisorId.ToString() || dsGetAssetAllocationRules.Tables[0].Rows[0]["A_AdviserId"].ToString() == "1000"))
        //                {
        //                    if (dsGetAssetAllocationRules != null && dsGetAssetAllocationRules.Tables[0].Rows[0]["A_AdviserId"].ToString() != advisorVo.advisorId.ToString())
        //                    {

        //                        foreach (DataRow dr in dsGetAssetAllocationRules.Tables[0].Rows)
        //                        {
        //                            if (dr["WAC_AssetClassification"].ToString() == "Cash")
        //                            {
        //                                cashPercentage = double.Parse(dr["WAAR_AssetAllocationPercenatge"].ToString());
        //                            }
        //                            else if (dr["WAC_AssetClassification"].ToString() == "Equity")
        //                            {
        //                                equityAdjustment = double.Parse(dr["WAAR_Adjustment"].ToString());
        //                            }
        //                        }
        //                        equitycalc = double.Parse(((100 - double.Parse(age.ToString())) / 100).ToString());
        //                        equityPercentage = (((100 - cashPercentage) * equitycalc + (equityAdjustment)));
        //                        debtpercentage = (100 - equityPercentage - cashPercentage);
        //                        drAsset = dtAsset.NewRow();
        //                        drAsset[0] = "Equity";
        //                        drAsset[1] = equityPercentage.ToString();
        //                        drAsset[2] = 1;
        //                        dtAsset.Rows.Add(drAsset);
        //                        drAsset = dtAsset.NewRow();
        //                        drAsset[0] = "Debt";
        //                        drAsset[1] = debtpercentage.ToString();
        //                        drAsset[2] = 2;
        //                        dtAsset.Rows.Add(drAsset);
        //                        drAsset = dtAsset.NewRow();
        //                        drAsset[0] = "Cash";
        //                        drAsset[1] = cashPercentage.ToString();
        //                        drAsset[2] = 3;
        //                        dtAsset.Rows.Add(drAsset);



        //                    }
        //                    else
        //                    {
        //                        foreach (DataRow dr in dsGetAssetAllocationRules.Tables[0].Rows)
        //                        {
        //                            drAsset = dtAsset.NewRow();

        //                            drAsset[0] = dr["WAC_AssetClassification"].ToString();
        //                            if (dr["WAAR_AssetAllocationPercenatge"] != null)
        //                                drAsset[1] = dr["WAAR_AssetAllocationPercenatge"].ToString();
        //                            else
        //                                drAsset[1] = 0;
        //                            drAsset[2] = dr["WAC_AssetClassificationCode"].ToString();
        //                            dtAsset.Rows.Add(drAsset);
        //                        }
        //                    }
        //                    dtRecommendedAllocation = dtAsset;
        //                    //gvRecommendedAssetAllocation.DataSource = dtAsset;
        //                    //gvRecommendedAssetAllocation.DataBind();
        //                    //================================
        //                    //
        //                    //Chart Control Part is show below
        //                    //
        //                    //================================    

        //                    if ((dtAsset.Rows.Count > 0) && (dtAsset.ToString() != null))
        //                    {

        //                        tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 1;
        //                        Legend ShowRecomondedAssetAlllegend = null;
        //                        ShowRecomondedAssetAlllegend = new Legend("ShowRecomondedAssetAlllegendLegends");
        //                        ShowRecomondedAssetAlllegend.Enabled = true;

        //                        Series seriesAssets = new Series("sActualAsset");
        //                        seriesAssets.ChartType = SeriesChartType.Pie;
        //                        cActualAsset.Visible = true;
        //                        lblRecommondedChart.Visible = true;
        //                        cActualAsset.Series.Clear();
        //                        cActualAsset.Series.Add(seriesAssets);
        //                        cActualAsset.DataSource = dtAsset;
        //                        cActualAsset.Series[0].XValueMember = "AssetType";
        //                        cActualAsset.Series[0].YValueMembers = "Percentage";
        //                        cActualAsset.Series[0].ToolTip = "#VALX: #PERCENT";
        //                        cActualAsset.Series[0]["PieLabelStyle"] = "Disabled";

        //                        cActualAsset.Legends.Add(ShowRecomondedAssetAlllegend);
        //                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].Title = "Assets";
        //                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleAlignment = StringAlignment.Center;
        //                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleSeparator = LegendSeparatorStyle.None;
        //                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].Alignment = StringAlignment.Center;
        //                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleSeparator = LegendSeparatorStyle.GradientLine;
        //                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleSeparatorColor = Color.Black;

        //                        // Enable X axis margin
        //                        LegendCellColumn colorColumn = new LegendCellColumn();
        //                        colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
        //                        colorColumn.HeaderBackColor = Color.WhiteSmoke;
        //                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].CellColumns.Add(colorColumn);
        //                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].BackColor = Color.FloralWhite;
        //                        LegendCellColumn totalColumn = new LegendCellColumn();
        //                        totalColumn.Alignment = ContentAlignment.MiddleLeft;

        //                        totalColumn.Text = "#VALX: #PERCENT";
        //                        totalColumn.Name = "AssetsColumn";
        //                        totalColumn.HeaderBackColor = Color.WhiteSmoke;
        //                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].CellColumns.Add(totalColumn);
        //                        cActualAsset.Series[0]["PieLabelStyle"] = "Disabled";

        //                        // Enable X axis margin
        //                        cActualAsset.ChartAreas["caActualAsset"].AxisX.IsMarginVisible = true;
        //                        cActualAsset.BackColor = Color.Transparent;
        //                        cActualAsset.ChartAreas[0].BackColor = Color.Transparent;
        //                        cActualAsset.ChartAreas[0].Area3DStyle.Enable3D = true;
        //                        cActualAsset.ChartAreas[0].Area3DStyle.Perspective = 50;
        //                        cActualAsset.DataBind();
        //                        tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 1;
        //                    }
        //                    else
        //                    {
        //                        tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
        //                        lblChartErrorDisplay.Visible = true;
        //                        AssetFormClear();
        //                    }
        //                    trCurrentAssetAllocation.Visible = true;
        //                    ShowCurrentAssetAllocationPieChart();
        //                }
        //                else
        //                {
        //                    AssetFormClear();
        //                    ShowCurrentAssetAllocationPieChart();
        //                    if (DScurrentAsset.Tables[0].Rows.Count > 0)
        //                        tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 1;
        //                    else
        //                        tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
        //                }
        //            }
        //        }

        //        # region Dont need this

        //        # endregion
        //        if (customerId != 0 && age != 0)
        //        {
        //            if (trCustomerAssetText.Visible == false)
        //                trCustomerAssetText.Visible = true;
        //            lblCustomerParagraph.Text = riskprofilebo.GetAssetAllocationText(customerId);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        // }
        // End ..


        // Added to show the Recomonded Asset allocation chart
        // Added by Vinayak Patil for RE: 3.0

        public void LoadAssetAllocation(string riskcode)
        {
            DataTable dtAsset = new DataTable();

            if (customerVo.Dob != DateTime.MinValue)
            {
                DateTime bday = customerVo.Dob;
                DateTime now = DateTime.Today;
                age = now.Year - bday.Year;
                if (now < bday.AddYears(age))
                {
                    age--;
                }

                lblAgeResult.Text = age.ToString();

                lblChartErrorDisplay.Visible = false;
                if (lblRClass.Text != "" || lblRClass.Text != null)
                {
                    lblRiskClass.Visible = true;
                    lblRiskScore.Visible = true;
                    lblRClassRs.Text = lblRClass.Text;
                    lblRscoreAA.Text = lblRScore.Text;
                    Session["Score"] = lblRscoreAA.Text;
                }
                else
                {
                    lblRClassRs.Text = "Fill Risk Profile to know Risk class and Risk Score";
                }

                DataSet dsGetRecomondedAssetAllocationData = riskprofilebo.GetAssetAllocationData(customerVo.CustomerId);
                if (dsGetRecomondedAssetAllocationData.Tables.Count > 0)
                {
                    dtAsset = dsGetRecomondedAssetAllocationData.Tables[0];

                    if ((dtAsset.Rows.Count > 0) && (dtAsset.ToString() != null))
                    {
                        // ***** Chart Binding Starts here ***** //

                        tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 1;
                        Legend ShowRecomondedAssetAlllegend = null;
                        ShowRecomondedAssetAlllegend = new Legend("ShowRecomondedAssetAlllegendLegends");
                        ShowRecomondedAssetAlllegend.Enabled = true;

                        Series seriesAssets = new Series("sActualAsset");
                        seriesAssets.ChartType = SeriesChartType.Pie;
                        cActualAsset.Visible = true;
                        lblRecommondedChart.Visible = true;
                        cActualAsset.Series.Clear();
                        cActualAsset.Series.Add(seriesAssets);
                        cActualAsset.DataSource = dtAsset;
                        cActualAsset.Series[0].XValueMember = "AssetType";
                        cActualAsset.Series[0].YValueMembers = "Percentage";
                        cActualAsset.Series[0].ToolTip = "#VALX: #PERCENT";
                        cActualAsset.Series[0]["PieLabelStyle"] = "Disabled";

                        cActualAsset.Palette = ChartColorPalette.Pastel;
                        cActualAsset.PaletteCustomColors = new Color[]{Color.LimeGreen, Color.Yellow, Color.LightBlue, Color.Purple, Color.Goldenrod, Color.Blue, Color.BurlyWood,
                                                                          Color.Chocolate, Color.DeepPink, Color.Plum, Color.Violet, Color.Gainsboro, Color.Tomato, Color.Teal, Color.BlanchedAlmond, Color.Cornsilk};


                        cActualAsset.Legends.Add(ShowRecomondedAssetAlllegend);
                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].Title = "Assets";
                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleAlignment = StringAlignment.Center;
                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleSeparator = LegendSeparatorStyle.None;
                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].Alignment = StringAlignment.Center;
                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleSeparator = LegendSeparatorStyle.GradientLine;
                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleSeparatorColor = Color.Black;

                        // Enable X axis margin
                        LegendCellColumn colorColumn = new LegendCellColumn();
                        colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                        colorColumn.HeaderBackColor = Color.WhiteSmoke;
                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].CellColumns.Add(colorColumn);
                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].BackColor = Color.FloralWhite;
                        LegendCellColumn totalColumn = new LegendCellColumn();
                        totalColumn.Alignment = ContentAlignment.MiddleLeft;

                        totalColumn.Text = "#VALX: #PERCENT";
                        totalColumn.Name = "AssetsColumn";
                        totalColumn.HeaderBackColor = Color.WhiteSmoke;
                        cActualAsset.Legends["ShowRecomondedAssetAlllegendLegends"].CellColumns.Add(totalColumn);
                        cActualAsset.Series[0]["PieLabelStyle"] = "Disabled";

                        // Enable X axis margin
                        cActualAsset.ChartAreas["caActualAsset"].AxisX.IsMarginVisible = true;
                        cActualAsset.BackColor = Color.Transparent;
                        cActualAsset.ChartAreas[0].BackColor = Color.Transparent;
                        cActualAsset.ChartAreas[0].Area3DStyle.Enable3D = true;
                        cActualAsset.ChartAreas[0].Area3DStyle.Perspective = 50;
                        cActualAsset.DataBind();
                        tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 1;

                        //     ***** End *****        //
                    }
                }
                else
                {
                    tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
                    lblChartErrorDisplay.Visible = true;
                    AssetFormClear();
                }
                trCurrentAssetAllocation.Visible = true;
                ShowCurrentAssetAllocationPieChart();
            }
        }
        public void SetCustomerId()
        {
            try
            {
                if (Session[SessionContents.FPS_ProspectList_CustomerId] != null && Session[SessionContents.FPS_ProspectList_CustomerId].ToString() != "")
                {
                    customerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                }
                advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                dsGetCustomerRiskProfile = riskprofilebo.GetCustomerRiskProfile(customerId, advisorVo.advisorId);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        //{

        //}
        protected void btnSubmitRisk_Click(object sender, EventArgs e)
        {

            string tempRID = "";


           
            
            listRiskOptionVo = (List<RiskOptionVo>)ViewState["ListRiskOption"];
            SetCustomerId();
            try
            {
                lblRiskProfilingParagraph.Visible = true;
                for (int i = 0; i < dsGetRiskProfileQuestion.Tables[0].Rows.Count; i++)
                {
                    dsGetRiskProfileQuestionOption = riskprofilebo.GetQuestionOption(int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_QuestionId"].ToString()), advisorVo.advisorId);
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

                
                for (int i = 0; i < dsGetRiskProfileRules.Tables[0].Rows.Count; i++)
                {
                    int minLimit = int.Parse(dsGetRiskProfileRules.Tables[0].Rows[i]["WRPR_RiskScoreLowerLimit"].ToString());
                    int maxLimit = 0;
                    if (dsGetRiskProfileRules.Tables[0].Rows[i]["WRPR_RiskScoreUpperLimit"].ToString() != null && dsGetRiskProfileRules.Tables[0].Rows[i]["WRPR_RiskScoreUpperLimit"].ToString() != "")
                    {
                        maxLimit = int.Parse(dsGetRiskProfileRules.Tables[0].Rows[i]["WRPR_RiskScoreUpperLimit"].ToString());
                    }
                    if (rScore >= minLimit && (rScore <= maxLimit || maxLimit == 0))
                    {
                        riskCode = dsGetRiskProfileRules.Tables[0].Rows[i]["XRC_RiskClassCode"].ToString();
                        lblRClass.Text = dsGetRiskProfileRules.Tables[0].Rows[i]["XRC_RiskClass"].ToString();
                        lblRiskProfilingParagraph.Text = dsGetRiskProfileRules.Tables[0].Rows[i]["ARC_RiskText"].ToString();
                        break;


                    }

                }
                if (customerVo.Dob != DateTime.MinValue)
                {
                    lblRiskProfileDate.Visible = true;
                    lblRiskProfileDate.Text = DateTime.Now.ToShortDateString();
                    tblRiskScore.Visible = true;
                    lblRScore.Visible = true;
                    lblRClass.Visible = true;
                    lblRScore.Text = rScore.ToString();

                    riskprofilebo.AddCustomerRiskProfileDetails(advisorVo.advisorId, customerId, rScore, DateTime.Now, riskCode, rmvo, 0, customerVo.Dob);
                    dsGetRiskProfileId = riskprofilebo.GetRpId(customerId);
                }
                else
                {
                    lblRiskProfileDate.Visible = false;
                    tblRiskScore.Visible = false;
                    lblRScore.Visible = false;
                    lblRClass.Visible = false;

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please fill DOB to create Risk profile.');", true);
                    return;
                }

                //====================================
                //
                // Adding Risk response to question
                //
                //====================================
                for (int i = 0; i < dsGetRiskProfileQuestion.Tables[0].Rows.Count; i++)
                {
                    dsGetRiskProfileQuestionOption = riskprofilebo.GetQuestionOption(int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i]["QM_QuestionId"].ToString()), advisorVo.advisorId);
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
                //AddToAssetAllocation();
                tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 1;
                if (customerId != 0 && age != 0)
                {
                    if (trCustomerAssetText.Visible == false)
                        trCustomerAssetText.Visible = true;
                    lblCustomerParagraph.Text = riskprofilebo.GetAssetAllocationText(customerId);
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
        /// <param name="type">Indicates when payment
        /// s are due. 0 = end of period, 1 = beginning of period.</param>
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
        protected void Trigger()
        {
            trRiskProfilingParagraph.Visible = true;
            trCustomerAssetText.Visible = true;
            //LoadRiskProfiling();

        }
        protected void LoadRiskProfiling()
        {
            tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
            DataSet dsGetRiskClassForRisk;
            DataSet dsGetAssetAllocationDetails;
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            try
            {
                AssetFormClear();
                if (Session[SessionContents.FPS_ProspectList_CustomerId] != null && Session[SessionContents.FPS_ProspectList_CustomerId].ToString() != "")
                {
                    SetCustomerId();
                    //dsGetCustomerIdByName = riskprofilebo.GetCustomerIdByName(txtPickCustomer.Text);                   
                    dsGetCustomerRiskProfile = riskprofilebo.GetCustomerRiskProfile(customerId, advisorVo.advisorId);

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
                            dsGetRiskProfileQuestionOption = riskprofilebo.GetQuestionOption(int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i - 1]["QM_QuestionId"].ToString()), advisorVo.advisorId);
                            for (int j = 1; j <= dsGetRiskProfileQuestionOption.Tables[0].Rows.Count; j++)
                            {
                                tempRID = "rbtnQ" + i + "A" + j;

                                RadioButton rbtnQAns = (RadioButton)tabRiskProfiling.FindControl(tempRID);
                                if (rbtnQAns.Checked == true)
                                {
                                    rbtnQAns.Checked = false;
                                }
                                if (dsGetCustomerRiskProfile.Tables[1].Rows.Count > 0)
                                {
                                    string questionoption = dsGetCustomerRiskProfile.Tables[1].Rows[i - 1]["QOM_Option"].ToString();

                                    if (rbtnQAns.Text == questionoption)
                                    {
                                        rbtnQAns.Checked = true;
                                    }
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

                        trRiskProfilingParagraph.Visible = true;
                        lblRiskProfilingParagraph.Text = dsGetCustomerRiskProfile.Tables[0].Rows[0]["ARC_RiskText"].ToString();

                        Session["FP_UserID"] = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                        GoalCount = GoalSetupBo.CheckGoalProfile(customerId);
                        hidGoalCount.Value = GoalCount.ToString();
                    }

                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('No Risk profile for this customer');", true);
                        trRiskProfilingParagraph.Visible = false;
                        trCustomerAssetText.Visible = false;
                        Session["FP_UserID"] = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
                        GoalCount = GoalSetupBo.CheckGoalProfile(customerId);
                        hidGoalCount.Value = GoalCount.ToString();
                        RiskFormClear();
                        AssetFormClear();
                        ShowCurrentAssetAllocationPieChart();
                        //if (DScurrentAsset.Tables[0].Rows.Count > 0)
                        //    tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 1;
                        //else
                        //    tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // ..... Commented by Vinayak Patil ..... //
        // ..... Reason: From RE: 3.0 Adding Asset Allocation from Database So Removed the Adding asset allocation logic from UI ..... //

        // Comment starts .. 
        // {

        //protected void AddToAssetAllocation()
        //{
        //    string approvedon = txtApprovedByCustomerOn.Text;
        //    DateTime now;
        //    try
        //    {
        //        if (txtApprovedByCustomerOn.Text == "")
        //        {
        //            now = DateTime.Now;
        //        }
        //        else
        //        {
        //            now = DateTime.Parse(approvedon);
        //        }
        //        SetCustomerId();

        //        if (dtRecommendedAllocation != null && dtRecommendedAllocation.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dtRecommendedAllocation.Rows)
        //            {
        //                riskprofilebo.AddAssetAllocationDetails(int.Parse(dsGetCustomerRiskProfile.Tables[0].Rows[0]["CRP_RiskProfileId"].ToString()), int.Parse(dr["AssetTypeCode"].ToString()), double.Parse(dr["Percentage"].ToString()), 0, now, rmvo);
        //            }
        //        }


        //        riskCode = dsGetCustomerRiskProfile.Tables[0].Rows[0]["XRC_RiskClassCode"].ToString();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        // } 
        // End ...


        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    string approvedon = txtApprovedByCustomerOn.Text;

        //    DateTime now;
        //    try
        //    {
        //        if (txtApprovedByCustomerOn.Text == "")
        //        {
        //            now = DateTime.Now;
        //        }
        //        else
        //        {
        //            now = DateTime.Parse(approvedon);
        //        }
        //        SetCustomerId();
        //        //dsGetCustomerIdByName = riskprofilebo.GetCustomerIdByName(txtPickCustomer.Text);
        //        //if (gvRecommendedAssetAllocation.Rows.Count > 0)
        //        //{
        //        //    for (int i = 0; i < gvRecommendedAssetAllocation.Rows.Count; i++)
        //        //    {
        //        //        if (gvRecommendedAssetAllocation.Rows[i].RowType == DataControlRowType.DataRow)
        //        //        {
        //        //            GridViewRow grv = gvRecommendedAssetAllocation.Rows[i];
        //        //            int assetClassificationCode = int.Parse(gvRecommendedAssetAllocation.DataKeys[i][0].ToString());
        //        //            double recommendedPercentage = double.Parse(grv.Cells[1].Text.ToString());
        //        //            riskprofilebo.UpdateAssetAllocationDetails(int.Parse(dsGetCustomerRiskProfile.Tables[0].Rows[0]["CRP_RiskProfileId"].ToString()), assetClassificationCode, recommendedPercentage, 0, now, rmvo);
        //        //        }
        //        //    }
        //        //}

        //        riskCode = dsGetCustomerRiskProfile.Tables[0].Rows[0]["XRC_RiskClassCode"].ToString();
        //        LoadAssetAllocation(riskCode);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
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
                dsGetRiskProfileQuestionOption = riskprofilebo.GetQuestionOption(int.Parse(dsGetRiskProfileQuestion.Tables[0].Rows[i - 1]["QM_QuestionId"].ToString()), advisorVo.advisorId);
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
            cActualAsset.Visible = false;
            ChartCurrentAsset.Visible = false;
            lblRecommondedChart.Visible = false;
            lblCurrentChart.Visible = false;

        }

        protected void ShowCurrentAssetAllocationPieChart()
        {
            string CurrEquity = "0";
            string CurrDebt = "0";
            string CurrCash = "0";
            string CurrAlternates = "0";
            DataRow drChartCurrAsset;
            DataTable dtChartCurrAsset = new DataTable();

            customerVo = new CustomerVo();
            if (Session[SessionContents.CustomerVo] != null && Session[SessionContents.CustomerVo].ToString() != "")
            {
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
            }
            if (customerVo.IsProspect == 1)
                DScurrentAsset = riskprofilebo.GetCurrentAssetAllocation(customerId, 1);
            else
                DScurrentAsset = riskprofilebo.GetCurrentAssetAllocation(customerId, 0);

            BindGridViewAssetAllocation(DScurrentAsset);

            //ChartCurrentAsset.Visible = true;
            if (DScurrentAsset != null && DScurrentAsset.Tables[0].Rows.Count > 0)
            {
                ChartCurrentAsset.Visible = true;
                lblCurrentChart.Visible = true;
                DataTable dtCurrentAssetAllocation = new DataTable();
                dtCurrentAssetAllocation = DScurrentAsset.Tables[0];
                dtChartCurrAsset.Columns.Add("AssetType");
                dtChartCurrAsset.Columns.Add("Percentage");
                foreach (DataRow dr in DScurrentAsset.Tables[0].Rows)
                {
                    drChartCurrAsset = dtChartCurrAsset.NewRow();
                    if (dr["AssetType"].ToString() == "Equity")
                    {
                        if (double.Parse(dr["Percentage"].ToString()) > 0)
                        {
                            drChartCurrAsset["AssetType"] = dr["AssetType"].ToString();
                            drChartCurrAsset["Percentage"] = dr["Percentage"].ToString();
                            CurrEquity = drChartCurrAsset["Percentage"].ToString();
                            dtChartCurrAsset.Rows.Add(drChartCurrAsset);
                        }
                    }
                    if (dr["AssetType"].ToString() == "Debt")
                    {
                        if (double.Parse(dr["Percentage"].ToString()) > 0)
                        {
                            drChartCurrAsset["AssetType"] = dr["AssetType"].ToString();
                            drChartCurrAsset["Percentage"] = dr["Percentage"].ToString();
                            CurrDebt = drChartCurrAsset["Percentage"].ToString();
                            dtChartCurrAsset.Rows.Add(drChartCurrAsset);
                        }
                    }
                    if (dr["AssetType"].ToString() == "Cash")
                    {
                        if (double.Parse(dr["Percentage"].ToString()) > 0)
                        {
                            drChartCurrAsset["AssetType"] = dr["AssetType"].ToString();
                            drChartCurrAsset["Percentage"] = dr["Percentage"].ToString();
                            CurrCash = drChartCurrAsset["Percentage"].ToString();
                            dtChartCurrAsset.Rows.Add(drChartCurrAsset);
                        }
                    }
                    if (dr["AssetType"].ToString() == "Alternates")
                    {
                        if (double.Parse(dr["Percentage"].ToString()) > 0)
                        {
                            drChartCurrAsset["AssetType"] = dr["AssetType"].ToString();
                            drChartCurrAsset["Percentage"] = dr["Percentage"].ToString();
                            CurrAlternates = drChartCurrAsset["Percentage"].ToString();
                            dtChartCurrAsset.Rows.Add(drChartCurrAsset);
                        }
                    }
                }
                if ((CurrEquity != "0") || (CurrDebt != "0") || (CurrCash != "0") || (CurrAlternates != "0"))
                {
                    //gvCurrentAssetAllocation.DataSource = dtChartCurrAsset;
                    //gvCurrentAssetAllocation.DataBind();
                    lblCurrentChart.Visible = true;
                    Legend ShowCurrentAssetAlllegend = null;
                    ShowCurrentAssetAlllegend = new Legend("ShowCurrentAssetAlllegendLegends");
                    ShowCurrentAssetAlllegend.Enabled = true;

                    Series seriesAssets = new Series("CurrentAsset");
                    seriesAssets.ChartType = SeriesChartType.Pie;
                    ChartCurrentAsset.Visible = true;
                    ChartCurrentAsset.Series.Clear();
                    ChartCurrentAsset.Series.Add(seriesAssets);
                    ChartCurrentAsset.DataSource = dtChartCurrAsset;
                    ChartCurrentAsset.Series[0].XValueMember = "AssetType";
                    ChartCurrentAsset.Series[0].YValueMembers = "Percentage";
                    ChartCurrentAsset.Series[0].ToolTip = "#VALX: #PERCENT";
                    ChartCurrentAsset.Series[0]["PieLabelStyle"] = "Disabled";

                    ChartCurrentAsset.Palette = ChartColorPalette.Pastel;
                    ChartCurrentAsset.PaletteCustomColors = new Color[]{Color.LimeGreen, Color.Yellow, Color.LightBlue, Color.Purple, Color.Goldenrod, Color.Blue, Color.BurlyWood,
                                                                          Color.Chocolate, Color.DeepPink, Color.Plum, Color.Violet, Color.Gainsboro, Color.Tomato, Color.Teal, Color.BlanchedAlmond, Color.Cornsilk};


                    ChartCurrentAsset.Legends.Add(ShowCurrentAssetAlllegend);
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].Title = "Assets";
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].TitleAlignment = StringAlignment.Center;
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].TitleSeparator = LegendSeparatorStyle.None;
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].Alignment = StringAlignment.Center;
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].TitleSeparator = LegendSeparatorStyle.GradientLine;
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].TitleSeparatorColor = Color.Black;

                    /******** Enable X axis margin *********/
                    LegendCellColumn colorColumn = new LegendCellColumn();
                    colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                    colorColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].CellColumns.Add(colorColumn);
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].BackColor = Color.FloralWhite;
                    LegendCellColumn totalColumn = new LegendCellColumn();
                    totalColumn.Alignment = ContentAlignment.MiddleLeft;

                    totalColumn.Text = "#VALX: #PERCENT";
                    totalColumn.Name = "AssetsColumn";
                    totalColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].CellColumns.Add(totalColumn);
                    ChartCurrentAsset.Series[0]["PieLabelStyle"] = "Disabled";

                    // Enable X axis margin
                    ChartCurrentAsset.ChartAreas["caActualAsset"].AxisX.IsMarginVisible = true;
                    ChartCurrentAsset.BackColor = Color.Transparent;
                    ChartCurrentAsset.ChartAreas[0].BackColor = Color.Transparent;
                    ChartCurrentAsset.ChartAreas[0].Area3DStyle.Enable3D = true;
                    ChartCurrentAsset.ChartAreas[0].Area3DStyle.Perspective = 50;
                    //ChartCurrentAsset.Series[0]["PieLabelStyle"] = "Disabled";
                    //ChartCurrentAsset.Series[0]["IsValueShownAsLabel"] = "true";
                    ChartCurrentAsset.DataBind();
                }
                else
                {
                    ChartCurrentAsset.Visible = false;
                    lblCurrentChart.Visible = false;
                }
            }
            else
            {
                lblCurrentChart.Visible = false;
                ChartCurrentAsset.Visible = false;
            }
        }

        protected void rbtnPickRiskclass_CheckedChanged(object sender, EventArgs e)
        {
            btnSubmitForPickRiskclass.Visible = true;
            btnSubmitRisk.Visible = false;
            ddlPickRiskClass.Visible = true;
            lblPickRiskPlass.Visible = true;
            BindRiskClasses();
            trRiskProfiler.Visible = false;
            rbtnPickRiskclass.Checked = true;
            tblPickRiskClass.Visible = true;
            divQuestionAnswers.Visible = false;
            Span12.Visible = true;
            Td1.Visible = false;
            tblRiskScore.Visible = true;
            lblRiskProfilingParagraph.Visible = false;
            dsGetCustomerRiskProfile = riskprofilebo.GetCustomerRiskProfile(customerId, advisorVo.advisorId);
            if (dsGetCustomerRiskProfile.Tables[0].Rows.Count > 0)
                riskCode = dsGetCustomerRiskProfile.Tables[0].Rows[0]["XRC_RiskClassCode"].ToString();

            dsGetRiskProfileId = riskprofilebo.GetRpId(customerId);
            if (dsGetRiskProfileId.Tables[0].Rows[0]["CRP_RiskProfileId"].ToString() != "")
            {
                LoadAssetAllocation(riskCode);
                //AddToAssetAllocation();
                tblRiskScore.Visible = true;
                //tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
            }
            else
            {
                tblRiskScore.Visible = false;
                ShowCurrentAssetAllocationPieChart();
                //if (DScurrentAsset.Tables[0].Rows.Count > 0)
                //    tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 1;
                //else
                //    tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
            }
            tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
        }

        protected void rbtnAnsQuestions_CheckedChanged(object sender, EventArgs e)
        {
            btnSubmitForPickRiskclass.Visible = false;
            btnSubmitRisk.Visible = true;
            ddlPickRiskClass.Visible = false;
            trRiskProfiler.Visible = true;
            tblPickRiskClass.Visible = false;
            divQuestionAnswers.Visible = true;
            lblRiskProfilingParagraph.Visible = true;
            Td1.Visible = true;
            lblRScore.Visible = true;
            if (lblRScore.Text == "")
                lblRScore.Text = "0";
            dsGetCustomerRiskProfile = riskprofilebo.GetCustomerRiskProfile(customerId, advisorVo.advisorId);
            if (dsGetCustomerRiskProfile.Tables[0].Rows.Count > 0)
                riskCode = dsGetCustomerRiskProfile.Tables[0].Rows[0]["XRC_RiskClassCode"].ToString();

            dsGetRiskProfileId = riskprofilebo.GetRpId(customerId);
            if (dsGetRiskProfileId.Tables[0].Rows[0]["CRP_RiskProfileId"].ToString() != "")
            {
                LoadAssetAllocation(riskCode);
                //AddToAssetAllocation();
                tblRiskScore.Visible = true;
                //tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 1;
            }
            else
            {
                tblRiskScore.Visible = false;
                ShowCurrentAssetAllocationPieChart();
                //if (DScurrentAsset.Tables[0].Rows.Count > 0)
                //    tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 1;
                //else
                //    tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
            }
            tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 0;
        }

        /// <summary>
        /// Binding the Dropdown to get Risk Clasees for Adviser
        /// 
        /// Dev By: Vinayak Patil
        /// </summary>
        private void BindRiskClasses()
        {
            try
            {
                int adviserId = advisorVo.advisorId;

                dsGlobal = riskprofilebo.GetAdviserRiskClasses(adviserId);
                if (dsGlobal.Tables[0].Rows.Count > 0)
                {
                    ddlPickRiskClass.DataSource = dsGlobal.Tables[0]; ;
                    ddlPickRiskClass.DataValueField = dsGlobal.Tables[0].Columns["XRC_RiskClassCode"].ToString();
                    ddlPickRiskClass.DataTextField = dsGlobal.Tables[0].Columns["XRC_RiskClass"].ToString();
                    ddlPickRiskClass.DataBind();
                }
                ddlPickRiskClass.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Risk Class", "Select Risk Class"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "FinancialPlanning.ascx:BindRiskClasses()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnSubmitForPickRiskclass_Click(object sender, EventArgs e)
        {

            //dsGlobal = riskprofilebo.GetAdviserRiskClasses(advisorVo.advisorId);
            riskCode = ddlPickRiskClass.SelectedValue;
            //Session["riskCode"] = riskCode;

            
            Session["btnSubmitForPickRiskclass"] = null;

            if (customerVo.Dob != DateTime.MinValue)
            {
                tblRiskScore.Visible = true;
                lblRClass.Visible = true;
                lblRiskProfileDate.Visible = true;
                trRiskProfilingParagraph.Visible = true;
                lblRClass.Visible = true;
                lblRClass.Text = ddlPickRiskClass.SelectedItem.ToString();

                riskprofilebo.AddCustomerRiskProfileDetails(advisorVo.advisorId, customerId, 0, DateTime.Now, riskCode, rmvo, 1, customerVo.Dob);
            }
            else
            {
                tblRiskScore.Visible = false;
                lblRClass.Visible = false;
                lblRiskProfileDate.Visible = false;
                trRiskProfilingParagraph.Visible = false;
                lblRClass.Visible = false;

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please fill DOB to create Risk profile.');", true);
                return;
            }
            
            lblRScore.Visible = false;

            LoadAssetAllocation(riskCode);
            //AddToAssetAllocation();
            lblRiskProfilingParagraph.Visible = true;
            if (dsGetRiskProfileRules.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dr in dsGetRiskProfileRules.Tables[0].Rows)
                {
                    if (dr["XRC_RiskClassCode"].ToString() == riskCode)
                        {
                            lblRiskProfilingParagraph.Text = dr["ARC_RiskText"].ToString();
                        }
                    }
                //lblRiskProfilingParagraph.Text = dsGetRiskProfileRules.Tables[0].Rows[i]["ARC_RiskText"].ToString();
            }
            tabRiskProfilingAndAssetAllocation.ActiveTabIndex = 1;
            if (customerId != 0 && customerVo.Dob != DateTime.MinValue)
            {
                if (trCustomerAssetText.Visible == false)
                    trCustomerAssetText.Visible = true;
                lblCustomerParagraph.Text = riskprofilebo.GetAssetAllocationText(customerId);
            }
        }

        public void BindGridViewAssetAllocation(DataSet dsAssetAllocationDetails)
        {
            DataTable dtCurrentAssetAllocation;
            DataTable dtRecomAssetAllocation;            
            
            try
            {
                decimal financialAssetTotal=0;

                DataTable dtAssetAllocation = new DataTable();
                dtAssetAllocation.Columns.Add("Class");
                dtAssetAllocation.Columns.Add("CurrentPercentage");
                dtAssetAllocation.Columns.Add("RecommendedPercentage");
                dtAssetAllocation.Columns.Add("ActionNeeded");
                dtAssetAllocation.Columns.Add("CurrentRs");
                dtAssetAllocation.Columns.Add("RecommendedRs");
                dtAssetAllocation.Columns.Add("ActionRs");

                dtCurrentAssetAllocation = dsAssetAllocationDetails.Tables[0];
                dtRecomAssetAllocation= dsAssetAllocationDetails.Tables[1];
                if (dsAssetAllocationDetails.Tables.Count > 2)
                    financialAssetTotal = decimal.Parse(dsAssetAllocationDetails.Tables[2].Rows[0]["Financial_Asset"].ToString());
              
                DataRow drAssetAllocation;
                foreach (DataRow dr in dtCurrentAssetAllocation.Rows)
                {
                    drAssetAllocation = dtAssetAllocation.NewRow();
                    drAssetAllocation["Class"] = dr["AssetType"].ToString();
                    drAssetAllocation["CurrentPercentage"] =Math.Round(decimal.Parse(dr["Percentage"].ToString()),2).ToString();

                    DataRow[] drAssetType;                    
                    drAssetType = dtRecomAssetAllocation.Select("AssetType='" + dr["AssetType"].ToString()+"'");
                    if (drAssetType.Count() > 0)
                    {
                        drAssetAllocation["RecommendedPercentage"] = drAssetType[0][2].ToString();
                        drAssetAllocation["RecommendedRs"] = String.Format("{0:n2}", ((financialAssetTotal * Math.Round(decimal.Parse(drAssetType[0][2].ToString()), 2)) / 100).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drAssetAllocation["ActionNeeded"] = (Math.Round(decimal.Parse(drAssetType[0][2].ToString()),2) - Math.Round(decimal.Parse(dr["Percentage"].ToString()),2)).ToString();                       
                        
                    }
                    else
                    {
                        drAssetAllocation["RecommendedPercentage"] = 0;
                        drAssetAllocation["RecommendedRs"] = String.Format("{0:n2}", ((financialAssetTotal * Math.Round(decimal.Parse(drAssetAllocation["RecommendedPercentage"].ToString()), 2)) / 100).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drAssetAllocation["ActionNeeded"] = (Math.Round(decimal.Parse(drAssetAllocation["RecommendedPercentage"].ToString()), 2) - Math.Round(decimal.Parse(dr["Percentage"].ToString()),2)).ToString();
                    }

                    drAssetAllocation["CurrentRs"] = String.Format("{0:n2}", ((financialAssetTotal * Math.Round(decimal.Parse(dr["Percentage"].ToString()), 2)) / 100).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    drAssetAllocation["ActionRs"] = String.Format("{0:n2}",(decimal.Parse(drAssetAllocation["RecommendedRs"].ToString()) - decimal.Parse(drAssetAllocation["CurrentRs"].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                    dtAssetAllocation.Rows.Add(drAssetAllocation);                    
                }
                gvAssetAllocation.DataSource = dtAssetAllocation;
                gvAssetAllocation.DataBind();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FinancialPlanning.ascx:BindGridViewAssetAllocation()");
                object[] objects = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void gvAssetAllocation_RowDataBound(object sender, GridViewRowEventArgs e)
        {           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal currentPctg = 0;
                decimal recommendedPctg = 0;

                System.Web.UI.WebControls.Image imgActionIndicator = e.Row.FindControl("imgActionIndicator") as System.Web.UI.WebControls.Image;
                Label lblRecommendedPctg = e.Row.FindControl("lblRecommendedPctg") as Label;
                Label lblCurrentPctg = e.Row.FindControl("lblCurrentPctg") as Label;
                currentPctg = decimal.Parse(lblCurrentPctg.Text);
                recommendedPctg = decimal.Parse(lblRecommendedPctg.Text);

                if (recommendedPctg > currentPctg)
                {
                    imgActionIndicator.ImageUrl = "~/Images/GreenUpArrow.png";
                }
                else if (recommendedPctg < currentPctg)
                {
                    imgActionIndicator.ImageUrl = "~/Images/RedDownArrow.png";
                }
                else if (recommendedPctg == currentPctg)
                {
                    imgActionIndicator.Visible = false;
                }
            }
        }
    }
}
