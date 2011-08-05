using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using VoReports;
using BoReports;
using BoCommon;
using VoUser;
using PCGMailLib;
using BoCustomerProfiling;
using System.Text;
using System.Net.Mail;
using BoAdvisorProfiling;
using VoAdvisorProfiling;
using System.IO;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using BoCustomerGoalProfiling;
using BoCustomerRiskProfiling;
using VoEmailSMS;
using System.Xml;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Security.AccessControl;



namespace WealthERP.Reports
{
    public partial class Display : System.Web.UI.Page
    {
        ReportDocument crmain;

        DateBo dtBo = new DateBo();
        DateTime dtTo = new DateTime();
        DateTime dtFrom = new DateTime();
        string ctrlPrefix = string.Empty;
        public string isMail = "0";
        bool CustomerLogIn = false;
        string PDFViewPath = "";

        RiskProfileBo riskprofilebo = new RiskProfileBo();
        RMVo customerRMVo = new RMVo();
        MFReportVo mfReport = new MFReportVo();
        EquityReportVo equityReport = new EquityReportVo();
        PortfolioReportVo portfolioReport = new PortfolioReportVo();
        FinancialPlanningVo financialPlanning = new FinancialPlanningVo();
        FPOfflineFormVo fpOfflineForm = new FPOfflineFormVo();
        AdvisorVo advisorVo = null;
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        RMVo rmVo = null;
        WERPReports CommonReport = new WERPReports();
        Dictionary<string, string> chkBoxsList = new Dictionary<string, string>();
        DataSet dsCustomerFPReportDetails;



        //DataTable dtFPReportText;
        //DataTable dtMonthlyGoalAmount;
        //DataTable dtRTGoalDetails;
        //DataTable dtCashFlows;
        //DataTable dtAssetAllocation;
        //DataTable dtHLVAnalysis;
        //DataTable dtAdvisorRiskClass;
        //DataTable dtPortfolioAllocation;
        string riskClass = string.Empty;
        double recEquity, recDebt, recCash, recAlternate, currEquity, currDebt, currCash, currAlternate = 0;
        private ReportType CurrentReportType
        {
            get
            {
                if (Session["ReportType"] == null)
                {
                    if (Request.Form["ctrl_EquityReports$btnView"] != null)
                    {
                        return ReportType.EquityReports;
                    }
                    else if (Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$btnViewReport"] != null)
                    {
                        return ReportType.MFReports;
                    }
                    else if (Request.Form["ctrl_PortfolioReports$btnView"] != null)
                    {
                        return ReportType.PortfolioReports;
                    }
                    else if (Request.Form["ctrl_FinancialPlanningReports$btnView"] != null)
                    {
                        return ReportType.FinancialPlanning;
                    }
                    else if (Request.Form["ctrl_FPSectional$btnViewReport"] != null || Request.Form["ctrl_FPSectional$btnViewInPDF"] != null)
                    {
                        return ReportType.FinancialPlanningSectional;
                    }
                    if (Request.Form["ctrl_OfflineForm$btnViewInPDF"] != null)
                    {
                        return ReportType.FPOfflineForm;
                    }
                    else
                        return ReportType.Invalid;
                }
                else
                    return (ReportType)Session["ReportType"];

            }
            set { Session["ReportType"] = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (Session["rmVo"] != null)
                rmVo = (RMVo)Session["rmVo"];

            if (!IsPostBack)
            {
                if (Request.QueryString["mail"] == "1")
                    isMail = "1";
            }
            if (Request.Form["ctrl_EquityReports$btnView"] != null || Request.Form["ctrl_EquityReports$btnMail"] != null)
            {
                CurrentReportType = ReportType.EquityReports;
                ctrlPrefix = "ctrl_EquityReports$";
            }

            //if (Request.Form["ctrl_MFReports$btnViewReport"] != null || Request.Form["ctrl_MFReports$btnEMailReport"] != null)
            if (Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$btnViewReport"] != null || Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlEmailReports$btnEmailReport"] != null || Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$btnExportToPDF"] != null || Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$btnCustomerViewReport"] != null || Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$btnCustomerExportToPDF"] != null)
            {
                CurrentReportType = ReportType.MFReports;
                if (Request.QueryString["Mail"] == "1")
                    ctrlPrefix = "ctrl_MFReports$tabViewAndEmailReports$tabpnlEmailReports$";
                else
                    ctrlPrefix = "ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$";
                if (Request.Form["ctrl_MFReports$hndCustomerLogin"] == "true" || Request.Form["ctrl_MFReports$hidBMLogin"] == "true")
                {
                    btnSendMail.Visible = false;

                }

            }
            if (Request.Form["ctrl_PortfolioReports$btnView"] != null || Request.Form["ctrl_PortfolioReports$btnMail"] != null)
            {
                CurrentReportType = ReportType.PortfolioReports;
                ctrlPrefix = "ctrl_PortfolioReports$";
            }
            if (Request.Form["ctrl_FinancialPlanningReports$btnView"] != null)
            {

                CurrentReportType = ReportType.FinancialPlanning;
                ctrlPrefix = "ctrl_FinancialPlanningReports$";
            }
            if (Request.Form["ctrl_FPSectional$btnViewReport"] != null || Request.Form["ctrl_FPSectional$btnViewInPDF"] != null)
            {
                btnSendMail.Visible = false;
                CurrentReportType = ReportType.FinancialPlanningSectional;
                ctrlPrefix = "ctrl_FPSectional$";
            }
            if (Request.Form["ctrl_OfflineForm$btnViewInPDF"] != null || Request.Form["ctrl_OfflineForm$btnViewReport"] != null)
            {
                btnSendMail.Visible = true;
                CurrentReportType = ReportType.FPOfflineForm;
                ctrlPrefix = "ctrl_OfflineForm$";
            }

            if (PreviousPage != null)
            {
                GetReportParameters();

            }
            // if (!Page.IsPostBack)
            //DisplayReport();


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && CurrentReportType == ReportType.FinancialPlanningSectional)
            {
                HideShowFPSection();
            }

            DisplayReport();

        }

        #region ReporDisplay Methods


        private void HideShowFPSection()
        {
            //if (Request.Form[ctrlPrefix + "chkCover_page"] == "on")                
            //    chkBoxsList.Add("chkCover_page", "Y");                
            //else
            //    chkBoxsList.Add("chkCover_page", "N");
            chkBoxsList.Add("chkCover_page", "Y");

            if (Request.Form[ctrlPrefix + "chkRM_Messgae"] == "on")
                chkBoxsList.Add("chkRM_Messgae", "Y");
            else
                chkBoxsList.Add("chkRM_Messgae", "N");

            if (Request.Form[ctrlPrefix + "chkImage"] == "on")
                chkBoxsList.Add("Image", "Y");
            else
                chkBoxsList.Add("Image", "N");

            if (Request.Form[ctrlPrefix + "chkFPIntroduction"] == "on")
                chkBoxsList.Add("FPIntroduction", "Y");
            else
                chkBoxsList.Add("FPIntroduction", "N");

            if (Request.Form[ctrlPrefix + "chkProfileSummary"] == "on")
                chkBoxsList.Add("ProfileSummary", "Y");
            else
                chkBoxsList.Add("ProfileSummary", "N");

            if (Request.Form[ctrlPrefix + "chkFinancialHealth"] == "on")
                chkBoxsList.Add("FinancialHealth", "Y");
            else
                chkBoxsList.Add("FinancialHealth", "N");

            //if (Request.Form[ctrlPrefix + "chkKeyAssumptions"] == "on")
            //    chkBoxsList.Add("KeyAssumptions", "Y");
            //else
            //    chkBoxsList.Add("KeyAssumptions", "N");

            chkBoxsList.Add("KeyAssumptions", "N");

            if (Request.Form[ctrlPrefix + "chkGoalProfile"] == "on")
                chkBoxsList.Add("GoalProfile", "Y");
            else
                chkBoxsList.Add("GoalProfile", "N");

            if (Request.Form[ctrlPrefix + "chkIncome_Expense"] == "on")
                chkBoxsList.Add("IncomeExpense", "Y");
            else
                chkBoxsList.Add("IncomeExpense", "N");

            if (Request.Form[ctrlPrefix + "chkCash_Flows"] == "on")
                chkBoxsList.Add("CashFlows", "Y");
            else
                chkBoxsList.Add("CashFlows", "N");

            if (Request.Form[ctrlPrefix + "chkNetWorthSummary"] == "on")
                chkBoxsList.Add("NetWorthSummary", "Y");
            else
                chkBoxsList.Add("NetWorthSummary", "N");

            if (Request.Form[ctrlPrefix + "chkRiskProfile"] == "on")
                chkBoxsList.Add("RiskProfile", "Y");
            else
                chkBoxsList.Add("RiskProfile", "N");

            if (Request.Form[ctrlPrefix + "chkInsurance"] == "on")
                chkBoxsList.Add("Insurance", "Y");
            else
                chkBoxsList.Add("Insurance", "N");

            if (Request.Form[ctrlPrefix + "chkGeneralInsurance"] == "on")
                chkBoxsList.Add("GeneralInsurance", "Y");
            else
                chkBoxsList.Add("GeneralInsurance", "N");

            if (Request.Form[ctrlPrefix + "chkCurrentObservation"] == "on")
                chkBoxsList.Add("CurrentObservation", "Y");
            else
                chkBoxsList.Add("CurrentObservation", "N");

            if (Request.Form[ctrlPrefix + "chkDisclaimer"] == "on")
                chkBoxsList.Add("Disclaimer", "Y");
            else
                chkBoxsList.Add("Disclaimer", "N");

            if (Request.Form[ctrlPrefix + "chkNotes"] == "on")
                chkBoxsList.Add("Notes", "Y");
            else
                chkBoxsList.Add("Notes", "N");


            ViewState["FPSelectedSectionList"] = chkBoxsList;
        }


        /// <summary>
        /// Call the overloaded DisplayReport() method based on ReportType.
        /// </summary>
        private void DisplayReport()
        {
            CrystalReportViewer1.Enabled = true;
            CrystalReportViewer1.Visible = true;

            crmain = new ReportDocument();

            if (CurrentReportType == ReportType.MFReports)
            {
                DisplayReport(mfReport);

            }
            else if (CurrentReportType == ReportType.EquityReports)
            {
                DisplayReport(equityReport);
            }
            else if (CurrentReportType == ReportType.PortfolioReports)
            {
                DisplayReport(portfolioReport);
            }
            else if (CurrentReportType == ReportType.FinancialPlanning)
            {
                DisplayReport(financialPlanning);
            }
            else if (CurrentReportType == ReportType.FinancialPlanningSectional)
            {
                DisplayReport(financialPlanning, 1);
            }
            else if (CurrentReportType == ReportType.FPOfflineForm)
            {
                DisplayReport(fpOfflineForm);
            }
            else
            {
                Response.Write("Invalid Report Type");
            }
            if (Request.QueryString["mail"] == "0" || Request.QueryString["mail"] == "1")
                FillEmailValues();
            else if (Request.QueryString["mail"] == "2")
            {
                string logoPath = "~/TempReports/ViewInPDF/" + PDFViewPath;
                if (PDFViewPath != "")
                {
                    Response.Redirect("TempReports/ViewInPDF/" + PDFViewPath);
                }
                else
                {

                }

                //if (Directory.Exists(Server.MapPath("~/Reports/TempReports/ViewInPDF")))
                //{
                //    DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Reports/TempReports/ViewInPDF"));

                //    FileInfo info = new FileInfo(Server.MapPath("~/Reports/TempReports/ViewInPDF")+ PDFViewPath);
                //    info.Delete();
                //    //foreach (FileInfo f in di.GetFiles())
                //    //{
                //    //    f.Delete();
                //    //}
                //}
            }
        }
        /// <summary >
        /// Exporting Disk For Viewing Report in PDF Format In browser : Author-Pramod
        /// </summary>
        private void ExportInPDF()
        {

            //if (Directory.Exists(Server.MapPath("~/Reports/TempReports/ViewInPDF/") + rmVo.RMId))
            //{
            //    DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Reports/TempReports/ViewInPDF/") + rmVo.RMId);

            //    foreach (FileInfo f in di.GetFiles())
            //    {
            //        f.Delete();
            //    }
            //}
            //else
            //{
            //    Directory.CreateDirectory(Server.MapPath("~/Reports/TempReports/ViewInPDF/") + rmVo.RMId);

            //    //DirectoryInfo dirInfo = new DirectoryInfo("~/Reports/TempReports/ViewInPDF/" + rmVo.RMId.ToString());
            //    //DirectorySecurity dSecurity = dirInfo.GetAccessControl();
            //    //dSecurity.AddAccessRule(new FileSystemAccessRule(,FileSystemRights.Delete,AccessControlType.Deny));
            //}
            try
            {
                string fileExtension = ".pdf";
                string exportFilename = string.Empty;
                System.Guid guid = System.Guid.NewGuid();
                //For PDF View In Browser
                string PDFNames = CurrentReportType.ToString() + "_" + guid + fileExtension;
                exportFilename = Server.MapPath("~/Reports/TempReports/ViewInPDF/") + PDFNames;
                PDFViewPath = PDFNames;
                crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        private void DisplayReport(FinancialPlanningVo report)
        {
            try
            {
                FinancialPlanningReportsBo financialPlanningReportsBo = new FinancialPlanningReportsBo();
                CustomerGoalSetupBo customerGoalsBo = new CustomerGoalSetupBo();
                //customerVo = customerBo.GetCustomer(int.Parse(Request.Form["ctrl_MFReports$hdnCustomerId1"]));
                //Session["customerVo"] = customerVo;
                report = (FinancialPlanningVo)Session["reportParams"];


                crmain.Load(Server.MapPath("FinancialPlanning.rpt"));
                DataSet DScurrentAsset = new DataSet();
                if (report.isProspect == 0)
                    DScurrentAsset = riskprofilebo.GetCurrentAssetAllocation(int.Parse(report.CustomerId), 0);
                else
                    DScurrentAsset = riskprofilebo.GetCurrentAssetAllocation(int.Parse(report.CustomerId), 1);

                DataSet dsEquitySectorwise = financialPlanningReportsBo.GetFinancialPlanningReport(report);
                DataTable dtEquitySectorwise = dsEquitySectorwise.Tables[0];
                setLogo();
                if (dsEquitySectorwise.Tables[1].Rows.Count > 0 || dsEquitySectorwise.Tables[0].Rows.Count > 0)
                {

                    if (dsEquitySectorwise.Tables[1].Rows.Count > 0 && double.Parse(dsEquitySectorwise.Tables[1].Rows[0]["CashPer"].ToString()) + double.Parse(dsEquitySectorwise.Tables[1].Rows[0]["DebtPer"].ToString()) + double.Parse(dsEquitySectorwise.Tables[1].Rows[0]["EquityPer"].ToString()) != 0)
                    {
                        crmain.Database.Tables["Goal"].SetDataSource(dsEquitySectorwise.Tables[0]);
                        crmain.Database.Tables["RiskProfile"].SetDataSource(dsEquitySectorwise.Tables[1]);
                        crmain.Database.Tables["FamilyDetails"].SetDataSource(dsEquitySectorwise.Tables[2]);
                        crmain.Database.Tables["CurrentAsset"].SetDataSource(DScurrentAsset.Tables[0]);

                        crmain.Subreports["Customer"].Database.Tables["Customer"].SetDataSource(dsEquitySectorwise.Tables[3]);
                        crmain.Subreports["Spouse"].Database.Tables["Spouse"].SetDataSource(dsEquitySectorwise.Tables[4]);
                        crmain.Subreports["Children"].Database.Tables["Child"].SetDataSource(dsEquitySectorwise.Tables[5]);
                        //crmain.Subreports[0].Database.Tables[0].SetDataSource(dsEquitySectorwise.Tables[2]);
                        //crmain.SetParameterValue("PreviousDate", DateBo.GetPreviousMonthLastDate(report.ToDate));
                        //crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                        AssignReportViewerProperties();
                        //crmain.SetParameterValue("CustomerName",);
                        //crmain.SetParameterValue("RTGoalDescription", customerGoalsBo.RTGoalDescriptionText(int.Parse(report.CustomerId)));
                        //crmain.SetParameterValue("RTGoalDescription", customerGoalsBo.GetHTMLString(1));
                        //AssignReportViewerProperties();
                        crmain.SetParameterValue("OtherGoalDescription", customerGoalsBo.OtherGoalDescriptionText(int.Parse(report.CustomerId)));
                        crmain.SetParameterValue("AssetDescription", riskprofilebo.GetAssetAllocationText(int.Parse(report.CustomerId)));
                        crmain.SetParameterValue("CustomerName", customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString());
                    }
                    else if (dsEquitySectorwise.Tables[0].Rows.Count > 0 || dsEquitySectorwise.Tables[2].Rows.Count > 0)
                    {
                        crmain.Database.Tables["Goal"].SetDataSource(dsEquitySectorwise.Tables[0]);
                        crmain.Database.Tables["RiskProfile"].SetDataSource(dsEquitySectorwise.Tables[1]);
                        crmain.Database.Tables["FamilyDetails"].SetDataSource(dsEquitySectorwise.Tables[2]);
                        crmain.Database.Tables["CurrentAsset"].SetDataSource(DScurrentAsset.Tables[0]);

                        crmain.Subreports["Customer"].Database.Tables["Customer"].SetDataSource(dsEquitySectorwise.Tables[3]);
                        crmain.Subreports["Spouse"].Database.Tables["Spouse"].SetDataSource(dsEquitySectorwise.Tables[4]);
                        crmain.Subreports["Children"].Database.Tables["Child"].SetDataSource(dsEquitySectorwise.Tables[5]);
                        //crmain.Subreports["Customer"].Database.Tables["Customer"].SetDataSource(dsEquitySectorwise.Tables[3]);
                        //crmain.Subreports["Customer"].Database.Tables["Spouse"].SetDataSource(dsEquitySectorwise.Tables[4]);
                        //crmain.Subreports["Customer"].Database.Tables["Child"].SetDataSource(dsEquitySectorwise.Tables[5]);
                        //crmain.Subreports[0].Database.Tables[0].SetDataSource(dsEquitySectorwise.Tables[2]);
                        //crmain.SetParameterValue("PreviousDate", DateBo.GetPreviousMonthLastDate(report.ToDate));
                        //crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                        AssignReportViewerProperties();
                        //crmain.SetParameterValue("CustomerName",);
                        //crmain.SetParameterValue("RTGoalDescription", customerGoalsBo.RTGoalDescriptionText(int.Parse(report.CustomerId)));

                        //crmain.SetParameterValue("RTGoalDescription", customerGoalsBo.GetHTMLString(1));
                        //AssignReportViewerProperties();
                        crmain.SetParameterValue("OtherGoalDescription", customerGoalsBo.OtherGoalDescriptionText(int.Parse(report.CustomerId)));
                        crmain.SetParameterValue("AssetDescription", riskprofilebo.GetAssetAllocationText(int.Parse(report.CustomerId)));
                        crmain.SetParameterValue("CustomerName", customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString());
                    }
                    else
                        SetNoRecords();
                }
                else
                    SetNoRecords();



            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void DisplayReport(FPOfflineFormVo report)
        {
            RMVo rmVo = (RMVo)Session["rmVo"];
            try
            {
                FinancialPlanningReportsBo financialPlanningReportsBo = new FinancialPlanningReportsBo();
                report = (FPOfflineFormVo)Session["reportParams"];
                crmain.Load(Server.MapPath("FPOfflineForm.rpt"));

                DataSet dsFPQuestionnaire = financialPlanningReportsBo.GetFPQuestionnaire(report, advisorVo.advisorId);
                DataTable dtFPQuestionnaire = dsFPQuestionnaire.Tables[0];
                if (dtFPQuestionnaire.Rows.Count > 0)
                {
                    crmain.SetDataSource(dtFPQuestionnaire);
                    setLogo();
                    //AssignReportViewerProperties();
                    crmain.SetParameterValue("RMName", "Advisor / Financial Planner: " + (rmVo.FirstName + " " + rmVo.MiddleName + " " + rmVo.LastName).Trim());

                    CrystalReportViewer1.ReportSource = crmain;
                    if (crmain.PrintOptions.PaperOrientation == PaperOrientation.Landscape)
                    {
                        CrystalReportViewer1.Attributes.Add("ToolbarStyle-Width", "900px");
                    }

                    CrystalReportViewer1.EnableDrillDown = true;
                    CrystalReportViewer1.HasCrystalLogo = false;
                }
                else
                    SetNoRecords();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            ExportInPDF();

        }

        private void DisplayReport(FinancialPlanningVo fpSectional, int test)
        {

            int financialAssetTotal = 0;
            double asset = 0;
            double liabilities = 0;
            double networth = 0;
            double currentAssetPer = 0;
            double recAssetPer = 0;
            double financialHealthTotal = 0;
            double totalAnnualIncome = 0;
            int dynamicRiskClass = 0;
            string fpImage = "SCBFPImage.jpg";
            string fpCoverHeaderImage = "FPReportHeader.jpg";
            string fpCoverFooterImage = "FPReportFooter.jpg";
            string logoPath = System.Web.HttpContext.Current.Request.MapPath("\\Images\\" + fpImage);
            //fpSectional = (MFReportVo)Session["reportParams"];
            fpSectional = (FinancialPlanningVo)Session["reportParams"];
            FinancialPlanningReportsBo financialPlanningReportsBo = new FinancialPlanningReportsBo();
            dsCustomerFPReportDetails = new DataSet();
            DataRow[] drOtherGoal;
            bool retFlag = false;
            //if (Session["FPDataSet"] != null)
            //{
            //    dsCustomerFPReportDetails = (DataSet)Session["FPDataSet"];
            //}
            //else 
            //{
            //    dsCustomerFPReportDetails = financialPlanningReportsBo.GetCustomerFPDetails(fpSectional, out asset, out liabilities, out networth, out riskClass, out dynamicRiskClass);
            //    Session["FPDataSet"] = dsCustomerFPReportDetails;
            //}
            if (Session["CusVo"] != null)
                customerVo = (CustomerVo)Session["CusVo"];
            else if (Session["customerVo"] != null)
                customerVo = (CustomerVo)Session["customerVo"];

            dsCustomerFPReportDetails = financialPlanningReportsBo.GetCustomerFPDetails(fpSectional, out asset, out liabilities, out networth, out riskClass, out dynamicRiskClass, out totalAnnualIncome, out financialAssetTotal);

            DataTable dtCustomerFamilyDetails = dsCustomerFPReportDetails.Tables["CustomerFamilyDetails"];
            DataTable dtAssetToal = dsCustomerFPReportDetails.Tables["AssetToal"];
            DataTable dtLiabilitiesTotal = dsCustomerFPReportDetails.Tables["LiabilitiesTotal"];
            DataTable dtKeyAssumption = dsCustomerFPReportDetails.Tables["KeyAssumption"];
            DataTable dtOtherGoal = dsCustomerFPReportDetails.Tables["OtherGoal"];
            DataTable dtRTGoal = dsCustomerFPReportDetails.Tables["RTGoal"];

            DataTable dtIncome = dsCustomerFPReportDetails.Tables["Income"];
            DataTable dtExpense = dsCustomerFPReportDetails.Tables["Expense"];
            DataTable dtCashFlow = dsCustomerFPReportDetails.Tables["CashFlow"];
            DataTable dtAssetDetails = dsCustomerFPReportDetails.Tables["AssetDetails"];
            DataTable dtLiabilitiesDetail = dsCustomerFPReportDetails.Tables["LiabilitiesDetail"];
            DataTable dtRiskProfile = dsCustomerFPReportDetails.Tables["RiskProfile"];
            DataTable dtLifeInsurance = dsCustomerFPReportDetails.Tables["LifeInsurance"];
            DataTable dtGeneralInsurance = dsCustomerFPReportDetails.Tables["GeneralInsurance"];
            DataTable dtHLVAnalysis = dsCustomerFPReportDetails.Tables["HLV"];

            DataTable dtAdvisorPortfolioAllocation = dsCustomerFPReportDetails.Tables["AdvisorPortfolioAllocation"];
            DataTable dtHLVBasedIncome = dsCustomerFPReportDetails.Tables["HLVBasedIncome"];
            DataTable dtCurrentObservation = dsCustomerFPReportDetails.Tables["CurrentObservation"];
            DataTable dtHealthAnalysis = dsCustomerFPReportDetails.Tables["HealthAnalysis"];
            DataTable dtCustomerRatio = dsCustomerFPReportDetails.Tables["CustomerFPRatio"];
            DataTable dtAdvisorRatioDetails = dsCustomerFPReportDetails.Tables["FPRatioDetails"];
            DataTable dtRMRecommendation = dsCustomerFPReportDetails.Tables["RMRecommendation"];
            //DataTable dtHLVAnalysis = dsCustomerFPReportDetails.Tables["HLVAnalysis"];



            //dtMonthlyGoalAmount = dsCustomerFPReportDetails.Tables[6];
            //dtRTGoalDetails = dsCustomerFPReportDetails.Tables[7];
            //dtCashFlows = dsCustomerFPReportDetails.Tables[10];
            //dtAssetAllocation = dsCustomerFPReportDetails.Tables[13];
            //dtHLVAnalysis = dsCustomerFPReportDetails.Tables[18];
            //dtAdvisorRiskClass = dsCustomerFPReportDetails.Tables[16];
            //dtPortfolioAllocation = dsCustomerFPReportDetails.Tables[17];

            dtAdvisorPortfolioAllocation = CreatePortfolioAllocationTable(dtAdvisorPortfolioAllocation, dynamicRiskClass);


            crmain.Load(Server.MapPath("FPSectionalReport.rpt"));

            this.CrystalReportViewer1.Navigate += new CrystalDecisions.Web.NavigateEventHandler(CrystalReportViewer1.OnNavigate);


            crmain.Subreports["ProfileSummary"].Database.Tables["CustomerFamilyDetails"].SetDataSource(dtCustomerFamilyDetails);
            crmain.Subreports["KeyAssumptions"].Database.Tables["WerpAssumptions"].SetDataSource(dtKeyAssumption);
            crmain.Subreports["GoalProfile"].Database.Tables["OtherGoal"].SetDataSource(dtOtherGoal);
            crmain.Subreports["GoalProfile"].Database.Tables["RTGoal"].SetDataSource(dtRTGoal);
            crmain.Subreports["RTGoalProfile"].Database.Tables["RTGoal"].SetDataSource(dtRTGoal);
            crmain.Subreports["Income"].Database.Tables["Income"].SetDataSource(dtIncome);
            crmain.Subreports["Expense"].Database.Tables["Expense"].SetDataSource(dtExpense);
            crmain.Subreports["CashFlows"].Database.Tables["CashFlows"].SetDataSource(dtCashFlow);
            crmain.Subreports["NetWorthSummary"].Database.Tables["NetWorth"].SetDataSource(dtAssetDetails);
            crmain.Subreports["LiabilitiesDetails"].Database.Tables["Liabilities"].SetDataSource(dtLiabilitiesDetail);
            crmain.Subreports["RiskProfile_PortfolioAllocation"].Database.Tables["AssetAllocation"].SetDataSource(dtRiskProfile);
            crmain.Subreports["Insurance"].Database.Tables["Insurance"].SetDataSource(dtLifeInsurance);
            crmain.Subreports["GeneralInsurance"].Database.Tables["GEInsurance"].SetDataSource(dtGeneralInsurance);
            crmain.Subreports["HLVAnalysis"].Database.Tables["HLVAnalysis"].SetDataSource(dtHLVAnalysis);
            crmain.Subreports["RiskProfile_PortfolioAllocationPartOne"].Database.Tables["PortfolioAllocation"].SetDataSource(dtAdvisorPortfolioAllocation);
            crmain.Subreports["HLVBasedIncome"].Database.Tables["HLVBasedIncome"].SetDataSource(dtHLVBasedIncome);
            crmain.Subreports["CurrentObservation"].Database.Tables["Observation"].SetDataSource(dtCurrentObservation);
            crmain.Subreports["FinancialHealth"].Database.Tables["FinancialHealth"].SetDataSource(dtHealthAnalysis);
            crmain.Subreports["FinancialHealth"].Database.Tables["CustomerFPRatio"].SetDataSource(dtCustomerRatio);
            setLogo();
            if (!File.Exists(logoPath))
                fpImage = "spacer.png";
            crmain.Subreports["Image"].Database.Tables["ImageSection"].SetDataSource(ImageSectionTable(System.Web.HttpContext.Current.Request.MapPath("\\Images\\" + fpImage)));

            crmain.Subreports["CoverPage"].Database.Tables["CoverPage"].SetDataSource(CreateCoverPageImageTable(System.Web.HttpContext.Current.Request.MapPath("\\Images\\" + fpCoverHeaderImage), System.Web.HttpContext.Current.Request.MapPath("\\Images\\" + fpCoverFooterImage)));


            string customerFullName = CustomerDataFormatFormat(customerVo);
            AssignReportViewerProperties();
            crmain.SetParameterValue("CustomerName", !string.IsNullOrEmpty(customerFullName.Trim()) ? customerFullName : string.Empty);
            crmain.SetParameterValue("CoverPageHeading", "Financial Plan For " + (!string.IsNullOrEmpty(customerFullName.Trim()) ? customerFullName : string.Empty));
         
            if (!string.IsNullOrEmpty(customerVo.Mobile1.ToString().Trim()))
                crmain.SetParameterValue("CustomerMobileNo", "Mobile No:" + "+91-" + customerVo.Mobile1.ToString());
            else
                crmain.SetParameterValue("CustomerMobileNo", string.Empty);
            crmain.SetParameterValue("Asset", convertUSCurrencyFormat(Math.Round(double.Parse(asset.ToString()), 0)));
            crmain.SetParameterValue("AssetTotal", Math.Round(double.Parse(asset.ToString()), 0).ToString());
            crmain.SetParameterValue("Liabilities", convertUSCurrencyFormat(Math.Round(double.Parse(liabilities.ToString()), 0)));
            crmain.SetParameterValue("Networth", convertUSCurrencyFormat(Math.Round(double.Parse(networth.ToString()), 0)));
            crmain.SetParameterValue("AnnualIncomeTotal", totalAnnualIncome);
            crmain.SetParameterValue("FinancialAssetTotal", Math.Round(double.Parse(financialAssetTotal.ToString()), 0).ToString());
            

            if (!string.IsNullOrEmpty(riskClass.Trim()))
                crmain.SetParameterValue("CustomerRiskClass", riskClass);
            else
                crmain.SetParameterValue("CustomerRiskClass", "  - -  ");

            crmain.SetParameterValue("SurpressPortfolioAlNonSCB", dynamicRiskClass.ToString());


            drOtherGoal = dsCustomerFPReportDetails.Tables[5].Select("GoalName='Retirement'");
            if (dsCustomerFPReportDetails.Tables[5].Rows.Count == 1 && drOtherGoal.Count() == 1)
                retFlag = true;
            //crmain.Database.Tables["ImageSection"].SetDataSource(ImageTable(System.Web.HttpContext.Current.Request.MapPath("\\Images\\" + fpImage)));

            if (dtOtherGoal.Rows.Count > 0)
            {
                if (retFlag == true)
                    crmain.SetParameterValue("OtherGoalSurpress", "0");
                else
                    crmain.SetParameterValue("OtherGoalSurpress", "1");
            }
            else
                crmain.SetParameterValue("OtherGoalSurpress", "0");

            if (dtRTGoal.Rows.Count > 0)
            {
                crmain.SetParameterValue("RTGoalSurpress", "1");
            }
            else
                crmain.SetParameterValue("RTGoalSurpress", "0");

            if (dtLiabilitiesDetail.Rows.Count > 0)
            {
                crmain.SetParameterValue("SurpressLiabilities", "1");
            }
            else
                crmain.SetParameterValue("SurpressLiabilities", "0");

            if (dtIncome.Rows.Count > 0)
            {
                crmain.SetParameterValue("SurpressIncome", "1");
            }
            else
                crmain.SetParameterValue("SurpressIncome", "0");

            if (dtExpense.Rows.Count > 0)
            {
                crmain.SetParameterValue("SurpressExpense", "1");
            }
            else
                crmain.SetParameterValue("SurpressExpense", "0");

            if (dtAssetDetails.Rows.Count > 0)
            {
                crmain.SetParameterValue("SurpressNetworthSummary", "1");
            }
            else
                crmain.SetParameterValue("SurpressNetworthSummary", "0");

            if (dtCustomerFamilyDetails.Rows.Count > 0)
            {
                crmain.SetParameterValue("SurpressFamilyDetails", "1");
            }
            else
                crmain.SetParameterValue("SurpressFamilyDetails", "0");

            if (dtLifeInsurance.Rows.Count > 0)
            {
                crmain.SetParameterValue("SurpressInsurance", "1");
            }
            else
                crmain.SetParameterValue("SurpressInsurance", "1");

            if (dtGeneralInsurance.Rows.Count > 0)
            {
                crmain.SetParameterValue("SurpressGeneralInsurance", "1");
            }
            else
                crmain.SetParameterValue("SurpressGeneralInsurance", "0");


            if (dtGeneralInsurance.Rows.Count > 0)
            {
                crmain.SetParameterValue("SurpressGEInsurance", "1");
            }
            else
                crmain.SetParameterValue("SurpressGEInsurance", "0");

            if (dtCashFlow.Rows.Count > 0)
            {
                crmain.SetParameterValue("SurpressCashFlow", "1");
            }
            else
                crmain.SetParameterValue("SurpressCashFlow", "0");

            if (dtCurrentObservation.Rows.Count > 0)
            {
                crmain.SetParameterValue("SurpressCurrentObservation", "1");
            }
            else
                crmain.SetParameterValue("SurpressCurrentObservation", "0");

            if (dtRMRecommendation.Rows.Count > 0)
            {
                crmain.SetParameterValue("RMRecSectionSurpress", "1");
            }
            else
                crmain.SetParameterValue("RMRecSectionSurpress", "0");

            foreach (DataRow dr in dtHLVAnalysis.Rows)
            {
                if (!string.IsNullOrEmpty(dr[1].ToString()))
                {
                    financialHealthTotal += double.Parse(dr[1].ToString());
                }

            }

            if (financialHealthTotal > 0)
            {
                crmain.SetParameterValue("SurpressFinancialHealth", "1");
            }
            else
                crmain.SetParameterValue("SurpressFinancialHealth", "0");


            foreach (DataRow dr in dtRiskProfile.Rows)
            {
                if (!string.IsNullOrEmpty(dr[2].ToString()))
                {
                    currentAssetPer += double.Parse(dr[2].ToString());
                }
                if (!string.IsNullOrEmpty(dr[1].ToString()))
                {
                    recAssetPer += double.Parse(dr[1].ToString());
                }
            }

            if (currentAssetPer > 0 || recAssetPer > 0)
            {
                crmain.SetParameterValue("SurpressRiskProfile", "1");
            }
            else
                crmain.SetParameterValue("SurpressRiskProfile", "0");


            DataRow[] drMediclaimRatio = dtAdvisorRatioDetails.Select("WFFR_RatioId=" + 11.ToString());

            if (drMediclaimRatio.Count() > 0)
            {
                crmain.SetParameterValue("SurpressMediclaim", "1");
            }
            else
                crmain.SetParameterValue("SurpressMediclaim", "0");



            if (ViewState["FPSelectedSectionList"] != null)
            {
                chkBoxsList = (Dictionary<string, string>)ViewState["FPSelectedSectionList"];
            }

            foreach (var pair in chkBoxsList)
            {
                switch (pair.Key)
                {
                    case "chkCover_page":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("CoverSection1", "1");
                            else
                                crmain.SetParameterValue("CoverSection1", "0");
                            break;
                        }
                    case "chkRM_Messgae":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("RMMessage", "1");
                            else
                                crmain.SetParameterValue("RMMessage", "0");
                            break;
                        }
                    case "Image":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("ImageSection", "1");
                            else
                                crmain.SetParameterValue("ImageSection", "0");
                            break;

                        }
                    case "FPIntroduction":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("FPIntroduction", "1");
                            else
                                crmain.SetParameterValue("FPIntroduction", "0");
                            break;

                        }
                    case "ProfileSummary":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("ProfileSummary", "1");
                            else
                                crmain.SetParameterValue("ProfileSummary", "0");
                            break;

                        }
                    case "FinancialHealth":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("FinancialHealth", "1");
                            else
                                crmain.SetParameterValue("FinancialHealth", "0");
                            break;

                        }
                    case "KeyAssumptions":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("KeyAssumptions", "1");
                            else
                                crmain.SetParameterValue("KeyAssumptions", "0");
                            break;

                        }
                    case "GoalProfile":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("GaolProfile", "1");
                            else
                                crmain.SetParameterValue("GaolProfile", "0");
                            break;

                        }
                    case "IncomeExpense":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("IncomeExpense", "1");
                            else
                                crmain.SetParameterValue("IncomeExpense", "0");
                            break;

                        }
                    case "GeneralInsurance":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("GeneralInsurance", "1");
                            else
                                crmain.SetParameterValue("GeneralInsurance", "0");
                            break;

                        }
                    case "CashFlows":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("CashFlows", "1");
                            else
                                crmain.SetParameterValue("CashFlows", "0");
                            break;

                        }
                    case "NetWorthSummary":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("NetWorthSummary", "1");
                            else
                                crmain.SetParameterValue("NetWorthSummary", "0");
                            break;

                        }
                    case "RiskProfile":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("RiskProfile", "1");
                            else
                                crmain.SetParameterValue("RiskProfile", "0");
                            break;

                        }
                    case "Insurance":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("Insurance", "1");
                            else
                                crmain.SetParameterValue("Insurance", "1");
                            break;

                        }
                    case "CurrentObservation":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("CurrentObservation", "1");
                            else
                                crmain.SetParameterValue("CurrentObservation", "0");
                            break;

                        }
                    case "Disclaimer":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("Disclaimer", "1");
                            else
                                crmain.SetParameterValue("Disclaimer", "0");
                            break;

                        }
                    case "Notes":
                        {
                            if (pair.Value == "Y")
                                crmain.SetParameterValue("Notes", "1");
                            else
                                crmain.SetParameterValue("Notes", "0");
                            break;

                        }

                    default:
                        break;
                }

            }

            AssignFPReportTextParameter(riskClass);

            if (Request.QueryString["mail"] == "2")
            {
                ExportInPDF();

            }
        }

        private DataTable CreatePortfolioAllocationTable(DataTable dtPortfolioAllocation, int dynamicRiskClass)
        {
            DataTable dtPortfolioAllocatonTable = new DataTable();

            dtPortfolioAllocatonTable.Columns.Add("AssetType");
            dtPortfolioAllocatonTable.Columns.Add("Conservative");
            dtPortfolioAllocatonTable.Columns.Add("ModeratelyConservative");
            dtPortfolioAllocatonTable.Columns.Add("Moderate");
            dtPortfolioAllocatonTable.Columns.Add("Aggressive");
            dtPortfolioAllocatonTable.Columns.Add("VeryAggressive");
            dtPortfolioAllocatonTable.Columns.Add("RiskAverse");

            DataRow[] drPortfolioAllocation;
            DataRow drPAllocation;
            string tempRiskClass = string.Empty;
            string tempRiskClass1 = string.Empty;
            if (dtPortfolioAllocation.Rows.Count > 0 && dynamicRiskClass == 1)
            {
                foreach (DataRow dr in dtPortfolioAllocation.Rows)
                {
                    if (tempRiskClass1 != dr["WAC_AssetClassification"].ToString().Trim())
                    {
                        if (dr["WAC_AssetClassification"].ToString() == "Alternates")
                        {
                            tempRiskClass1 = dr["WAC_AssetClassification"].ToString().Trim();
                            tempRiskClass = "Total Alternates";
                            drPortfolioAllocation = dtPortfolioAllocation.Select("WAC_AssetClassification='Alternates'");

                        }
                        else if (dr["WAC_AssetClassification"].ToString() == "Cash")
                        {
                            tempRiskClass = "Total Cash";
                            drPortfolioAllocation = dtPortfolioAllocation.Select("WAC_AssetClassification='Cash'");

                        }
                        else if (dr["WAC_AssetClassification"].ToString() == "Debt")
                        {
                            tempRiskClass = "Total Debt";
                            drPortfolioAllocation = dtPortfolioAllocation.Select("WAC_AssetClassification='Debt'");

                        }
                        else if (dr["WAC_AssetClassification"].ToString() == "Equity")
                        {
                            tempRiskClass = "Total Equity";
                            drPortfolioAllocation = dtPortfolioAllocation.Select("WAC_AssetClassification='Equity'");

                        }
                        else
                        {
                            drPortfolioAllocation = dtPortfolioAllocation.Select("WAC_AssetClassification=='Alternates'");
                        }

                        drPAllocation = dtPortfolioAllocatonTable.NewRow();
                        drPAllocation["AssetType"] = tempRiskClass;
                        foreach (DataRow row in drPortfolioAllocation)
                        {
                            switch (row["XRC_RiskClass"].ToString())
                            {

                                case "Aggressive":
                                    {
                                        drPAllocation["Aggressive"] = Math.Round(double.Parse(row["WAAR_AssetAllocationPercenatge"].ToString()), 0).ToString();
                                        break;

                                    }

                                case "Conservative":
                                    {
                                        drPAllocation["Conservative"] = Math.Round(double.Parse(row["WAAR_AssetAllocationPercenatge"].ToString()), 0).ToString();
                                        break;

                                    }
                                case "Moderately Aggressive":
                                    {
                                        drPAllocation["ModeratelyConservative"] = Math.Round(double.Parse(row["WAAR_AssetAllocationPercenatge"].ToString()), 0).ToString();
                                        break;

                                    }
                                case "Moderate":
                                    {
                                        drPAllocation["Moderate"] = Math.Round(double.Parse(row["WAAR_AssetAllocationPercenatge"].ToString()), 0).ToString();
                                        break;

                                    }
                                case "Very Aggressive":
                                    {
                                        drPAllocation["VeryAggressive"] = Math.Round(double.Parse(row["WAAR_AssetAllocationPercenatge"].ToString()), 0).ToString();
                                        break;

                                    }
                                case "Risk Averse":
                                    {
                                        drPAllocation["RiskAverse"] = Math.Round(double.Parse(row["WAAR_AssetAllocationPercenatge"].ToString()), 0).ToString();
                                        break;

                                    }
                                default:
                                    {
                                        break;
                                    }

                            }
                        }

                        dtPortfolioAllocatonTable.Rows.Add(drPAllocation);

                    }
                    else
                    {
                        break;
                    }
                }

                drPAllocation = dtPortfolioAllocatonTable.NewRow();
                drPAllocation["AssetType"] = "Total";
                drPAllocation["Aggressive"] = "100";
                drPAllocation["Conservative"] = "100";
                drPAllocation["ModeratelyConservative"] = "100";
                drPAllocation["Moderate"] = "100";
                drPAllocation["VeryAggressive"] = "100";
                drPAllocation["RiskAverse"] = "100";
                dtPortfolioAllocatonTable.Rows.Add(drPAllocation);
            }
            else
            {
                drPAllocation = dtPortfolioAllocatonTable.NewRow();
                drPAllocation["AssetType"] = "Total";
                drPAllocation["Aggressive"] = "100";
                drPAllocation["Conservative"] = "100";
                drPAllocation["ModeratelyConservative"] = "100";
                drPAllocation["Moderate"] = "100";
                drPAllocation["VeryAggressive"] = "100";
                drPAllocation["RiskAverse"] = "100";
                dtPortfolioAllocatonTable.Rows.Add(drPAllocation);

            }

            return dtPortfolioAllocatonTable;

        }
        public static DataTable ImageSectionTable(string ImageFile)
        {
            DataTable data = new DataTable();
            DataRow row;
            data.TableName = "ImageSection";
            data.Columns.Add("SectionImage", System.Type.GetType("System.Byte[]"));
            row = data.NewRow();
            if (ImageFile != string.Empty)
            {
                FileStream fs = new FileStream(ImageFile, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);

                row[0] = br.ReadBytes((int)br.BaseStream.Length);
                data.Rows.Add(row);
                br = null;
                fs.Close();
                fs = null;
            }
            else
            {
                row[0] = new byte[] { 0 };
                data.Rows.Add(row);
            }


            return data;
        }

        public static DataTable CreateCoverPageImageTable(string headerImage, string footerImage)
        {
            DataTable data = new DataTable();
            DataRow row;
            data.TableName = "CoverPage";
            data.Columns.Add("HeaderImage", System.Type.GetType("System.Byte[]"));
            data.Columns.Add("FooterImage", System.Type.GetType("System.Byte[]"));
            row = data.NewRow();
            if (headerImage != string.Empty)
            {
                FileStream fs = new FileStream(headerImage, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                row[0] = br.ReadBytes((int)br.BaseStream.Length);

                FileStream fs1 = new FileStream(footerImage, FileMode.Open);
                BinaryReader br1 = new BinaryReader(fs1);
                row[1] = br1.ReadBytes((int)br1.BaseStream.Length);

                data.Rows.Add(row);
                br = null;
                br1 = null;
                fs.Close();
                fs = null;
                br1 = null;
                fs1.Close();
            }
            else
            {
                row[0] = new byte[] { 0 };
                data.Rows.Add(row);
            }


            return data;
        }
        public void AssignFPReportTextParameter(string customerRiskClass)
        {

            RMVo rmVo = (RMVo)Session["rmVo"];

            AdvisorStaffBo adviserStaffBo = new AdvisorStaffBo();
            string state = "";
            if (Session["CusVo"] != null)
                customerVo = (CustomerVo)Session["CusVo"];
            else if (Session["customerVo"] != null)
                customerVo = (CustomerVo)Session["customerVo"];
            string strSection = string.Empty;
            string strRMMessage = string.Empty;
            string[] strRMmsgLine;

            string strFPIntroFinancialPlanning = string.Empty;
            string strFPIntroFinancialPlanner = string.Empty;

            string strImageOpeningLine = string.Empty;

            string strProfileSummaryText = string.Empty;
            string strFinancialhealthText = string.Empty;
            string strKeyAssumtionText = string.Empty;
            string strGoalOpeningLine = string.Empty;
            string strOtherGoalText = string.Empty;
            string[] strOtherGoalTextLines;
            string[] strRTGoalTextLines;
            string strRTOpeningLine = string.Empty;
            string strCashFlowsText = string.Empty;
            string strNetWorthOpeningLine = string.Empty;
            string strNetWorthSummary = string.Empty;
            string strRiskClassDescription = string.Empty;
            string[] strRiskClassInfoLines;
            string strRiskProfileText = string.Empty;
            string[] strRiskClassLines;
            string strRiskProfileAssetAllocationText = string.Empty;
            string strInsuranceText = string.Empty;
            Dictionary<string, string> dicRMMessageFieldCodes = new Dictionary<string, string>();
            Dictionary<string, string> dicOtherGoalCodes = new Dictionary<string, string>();
            Dictionary<string, string> dicRiskProfileCodes = new Dictionary<string, string>();
            StringBuilder strOtherGoalFinalText = new StringBuilder();
            string strRTGoalText = string.Empty;
            string strDisclaimer = string.Empty;
            string strIncomeExpenseSurplus = string.Empty;
            string strAssetAllocation = string.Empty;
            string strHLVNote = string.Empty;
            string strRMRecommendations = string.Empty;

            DataTable dtReportSectionAndText = dsCustomerFPReportDetails.Tables["ReportSection"];
            DataTable dtMonthlyGoalTotal = dsCustomerFPReportDetails.Tables["MonthlyGoalTotal"];
            DataTable dtRTGoal = dsCustomerFPReportDetails.Tables["RTGoal"];
            DataTable dtAdvisorRiskClass = dsCustomerFPReportDetails.Tables["AdvisorRiskClass"];
            DataTable dtAssetDetails = dsCustomerFPReportDetails.Tables["AssetDetails"];
            DataTable dtCashFlow = dsCustomerFPReportDetails.Tables["CashFlow"];
            DataTable dtRiskProfile = dsCustomerFPReportDetails.Tables["RiskProfile"];
            DataTable dtOtherGoalDetails = dsCustomerFPReportDetails.Tables["OtherGoal"];
            DataTable dtHLVAnalysis = dsCustomerFPReportDetails.Tables["OtherGoal"];
            DataTable dtIncome = dsCustomerFPReportDetails.Tables["Income"];
            DataTable dtExpense = dsCustomerFPReportDetails.Tables["Expense"];
            DataTable dtHLVAssumption = dsCustomerFPReportDetails.Tables["HLVAssumption"];
            DataTable dtAssetAllocation = dsCustomerFPReportDetails.Tables["RiskProfile"];
            DataTable dtRMRecommendation = dsCustomerFPReportDetails.Tables["RMRecommendation"];
            double totalIncome = 0;
            double totalExpense = 0;
            double totalMonthlySurplus = 0;
            double equityGapPercent = 0;
            

            if (dtIncome.Rows.Count>0)
            totalIncome = double.Parse(dtIncome.Compute("SUM(IncomeAmount)", "").ToString());
            if (dtExpense.Rows.Count > 0)
            totalExpense=double.Parse(dtExpense.Compute("SUM(ExpenseAmount)", "").ToString());
            totalMonthlySurplus = totalIncome - totalExpense;

            foreach (DataRow dr in dtReportSectionAndText.Rows)
            {

                switch (dr["SectionName"].ToString())
                {
                    case "RM_Messgae":
                        {
                            if (dr["TextPropuse"].ToString().Trim() == "RM_Message")
                            {
                                if (string.IsNullOrEmpty(strRMMessage.Trim()))
                                    strRMMessage = dr["TextParaGraph"].ToString();
                            }
                            if (dr["HasFieldCode"].ToString().Trim() == "Y")
                            {
                                dicRMMessageFieldCodes.Add(dr["FieldCode"].ToString(), dr["TextPropuse"].ToString());

                            }
                            break;
                        }
                    case "Image":
                        {
                            if (dr["TextPropuse"].ToString().Trim() == "Opening_Line" && int.Parse(dr["TextId"].ToString()) == 2)
                            {
                                if (string.IsNullOrEmpty(strImageOpeningLine.Trim()))
                                    strImageOpeningLine = dr["TextParaGraph"].ToString();
                            }

                            break;
                        }
                    case "FP_Introduction":
                        {
                            if (dr["TextPropuse"].ToString().Trim() == "Financial_Planning" && int.Parse(dr["TextId"].ToString()) == 3)
                            {
                                if (string.IsNullOrEmpty(strFPIntroFinancialPlanning.Trim()))
                                    strFPIntroFinancialPlanning = dr["TextParaGraph"].ToString();
                            }

                            break;
                        }
                    case "Profile_Summary":
                        {
                            if (dr["TextPropuse"].ToString().Trim() == "Profile_Summary_Text" && int.Parse(dr["TextId"].ToString()) == 5)
                            {
                                if (string.IsNullOrEmpty(strProfileSummaryText.Trim()))
                                    strProfileSummaryText = dr["TextParaGraph"].ToString();
                            }

                            break;
                        }
                    case "Financial_Health":
                        {
                            if (dr["TextPropuse"].ToString().Trim() == "Financial_Health_Text" && int.Parse(dr["TextId"].ToString()) == 6)
                            {
                                if (string.IsNullOrEmpty(strFinancialhealthText.Trim()))
                                    strFinancialhealthText = dr["TextParaGraph"].ToString();
                            }

                            break;
                        }
                    case "Key_Assumptions":
                        {
                            if (dr["TextPropuse"].ToString().Trim() == "Key_Assumption_Text" && int.Parse(dr["TextId"].ToString()) == 7)
                            {
                                if (string.IsNullOrEmpty(strKeyAssumtionText.Trim()))
                                    strKeyAssumtionText = dr["TextParaGraph"].ToString();
                            }

                            break;
                        }
                    case "Goal_Profiling":
                        {
                            if (dr["TextPropuse"].ToString().Trim() == "Goal _Opening_Line" && int.Parse(dr["TextId"].ToString()) == 8)
                            {
                                if (string.IsNullOrEmpty(strGoalOpeningLine.Trim()))
                                    strGoalOpeningLine = dr["TextParaGraph"].ToString();
                            }
                            if (dr["TextPropuse"].ToString().Trim() == "Customer_Other_Goal_Text" && int.Parse(dr["TextId"].ToString()) == 9)
                            {
                                if (string.IsNullOrEmpty(strOtherGoalText.Trim()))
                                    strOtherGoalText = dr["TextParaGraph"].ToString();
                            }
                            if (dr["TextPropuse"].ToString().Trim() == "Retirement_Opening_Line" && int.Parse(dr["TextId"].ToString()) == 10)
                            {
                                if (string.IsNullOrEmpty(strRTOpeningLine.Trim()))
                                    strRTOpeningLine = dr["TextParaGraph"].ToString();
                            }
                            if (dr["TextPropuse"].ToString().Trim() == "Customer_RT_Gaol_Text" && int.Parse(dr["TextId"].ToString()) == 11)
                            {
                                if (string.IsNullOrEmpty(strRTGoalText.Trim()))
                                    strRTGoalText = dr["TextParaGraph"].ToString();
                            }
                            if (dr["HasFieldCode"].ToString().Trim() == "Y")
                            {
                                dicOtherGoalCodes.Add(dr["FieldCode"].ToString(), dr["TextPropuse"].ToString());

                            }

                            break;
                        }
                    case "Income_Expense_Summary":
                        {
                            if (dr["TextPropuse"].ToString().Trim() == "Income_Expense_Summary" && int.Parse(dr["TextId"].ToString()) == 22)
                            {
                                if (string.IsNullOrEmpty(strIncomeExpenseSurplus.Trim()))
                                    strIncomeExpenseSurplus = dr["TextParaGraph"].ToString();
                            }

                            break;
                        }
                    case "Cash_Flows":
                        {
                            if (dr["TextPropuse"].ToString().Trim() == "CashFlow_Summary" && int.Parse(dr["TextId"].ToString()) == 13)
                            {
                                if (string.IsNullOrEmpty(strCashFlowsText.Trim()))
                                    strCashFlowsText = dr["TextParaGraph"].ToString();
                            }

                            break;
                        }
                    case "Net_Worth":
                        {
                            if (dr["TextPropuse"].ToString().Trim() == "Networth_Openning_Line" && int.Parse(dr["TextId"].ToString()) == 14)
                            {
                                if (string.IsNullOrEmpty(strNetWorthOpeningLine.Trim()))
                                    strNetWorthOpeningLine = dr["TextParaGraph"].ToString();
                            }
                            if (dr["TextPropuse"].ToString().Trim() == "NetWorth_Summary" && int.Parse(dr["TextId"].ToString()) == 21)
                            {
                                if (string.IsNullOrEmpty(strNetWorthSummary.Trim()))
                                    strNetWorthSummary = dr["TextParaGraph"].ToString();
                            }

                            break;
                        }
                    case "RiskProfile_ Portfolio_Allocation":
                        {
                            if (dr["TextPropuse"].ToString().Trim() == "Risk_Classes_Descprition" && int.Parse(dr["TextId"].ToString()) == 15)
                            {
                                if (string.IsNullOrEmpty(strRiskClassDescription.Trim()))
                                    strRiskClassDescription = dr["TextParaGraph"].ToString();
                            }

                            if (dr["TextPropuse"].ToString().Trim() == "Customer_Risk_Profile_Text" && int.Parse(dr["TextId"].ToString()) == 17)
                            {
                                if (string.IsNullOrEmpty(strRiskProfileText.Trim()))
                                    strRiskProfileText = dr["TextParaGraph"].ToString();
                            }

                            if (dr["TextPropuse"].ToString().Trim() == "Asset_Allocation_Text" && int.Parse(dr["TextId"].ToString()) == 23)
                            {
                                if (string.IsNullOrEmpty(strAssetAllocation.Trim()))
                                    strAssetAllocation = dr["TextParaGraph"].ToString();
                            }
                            if (dr["TextPropuse"].ToString().Trim() == "Customer_Portoflio_Allocation_Text" && int.Parse(dr["TextId"].ToString()) == 18)
                            {
                                if (string.IsNullOrEmpty(strRiskProfileAssetAllocationText.Trim()))
                                    strRiskProfileAssetAllocationText = dr["TextParaGraph"].ToString();
                            }

                            if (dr["TextPropuse"].ToString().Trim() == "Customer_Portoflio_Allocation_Text" && dr["HasFieldCode"].ToString().Trim() == "Y")
                            {
                                dicRiskProfileCodes.Add(dr["FieldCode"].ToString(), dr["TextPropuse"].ToString());

                            }


                            break;
                        }
                    case "Insurance_Details":
                        {
                            if (dr["TextPropuse"].ToString().Trim() == "Insurance_Text" && int.Parse(dr["TextId"].ToString()) == 19)
                            {
                                if (string.IsNullOrEmpty(strInsuranceText.Trim()))
                                    strInsuranceText = dr["TextParaGraph"].ToString();

                            }
                            else if (dr["TextPropuse"].ToString().Trim() == "HLV_Note" && int.Parse(dr["TextId"].ToString()) == 24)
                            {
                                if (string.IsNullOrEmpty(strHLVNote.Trim()))
                                    strHLVNote = dr["TextParaGraph"].ToString();
                            }

                            break;
                        }
                    
                    case "Disclaimer":
                        {
                            if (dr["TextPropuse"].ToString().Trim() == "Disclaimer_Text" && int.Parse(dr["TextId"].ToString()) == 20)
                            {
                                if (string.IsNullOrEmpty(strDisclaimer.Trim()))
                                    strDisclaimer = dr["TextParaGraph"].ToString();
                            }

                            break;
                        }
                    default:
                        break;

                }

            }

            string customerFullName = CustomerDataFormatFormat(customerVo);

            if (dicRMMessageFieldCodes.Count > 0)
            {
                foreach (var pair in dicRMMessageFieldCodes)
                {
                    if (pair.Key.Trim() == "#CustName#")
                        strRMMessage = strRMMessage.Replace(pair.Key, !string.IsNullOrEmpty(customerFullName.Trim()) ? customerFullName : string.Empty);
                    else if (pair.Key.Trim() == "#RMName#")
                        strRMMessage = strRMMessage.Replace(pair.Key, (customerRMVo.FirstName + " " + customerRMVo.MiddleName + " " + customerRMVo.LastName).Trim());
                    else if (pair.Key.Trim() == "#RMMobile#")
                    {
                        if (customerRMVo.Mobile != 0)
                            strRMMessage = strRMMessage.Replace(pair.Key, "+91-" + customerRMVo.Mobile);
                    }
                    else if (pair.Key.Trim() == "#TelePhoneNo#")
                    {
                        strRMMessage = strRMMessage.Replace(pair.Key, customerRMVo.OfficePhoneDirectStd + "-" + customerRMVo.OfficePhoneDirectNumber);

                    }
                    else if (pair.Key.Trim() == "#RMEmail#")
                    {
                        strRMMessage = strRMMessage.Replace(pair.Key, customerRMVo.Email);

                    }
                }
            }
            strRMmsgLine = strRMMessage.Split('~');
            strRMMessage = string.Empty;
            foreach (string str in strRMmsgLine)
            {
                if (!str.Contains("#RMMobile#"))
                {
                    strRMMessage += str;
                }
            }
            String[] strGoalText = strOtherGoalText.Split('~');
            string strFinalGoalText = "";
            string strChildText = "";
            double monthlySavingRequired = 0;
            double annualSavingsRequired = 0;
            foreach (string str in strGoalText)
            {
                if (str.Contains("#ChildName#") && str.Contains("#ChildEducationGoalTotal#"))
                {
                    foreach (DataRow dr in dtOtherGoalDetails.Rows)
                    {
                        strChildText = str;
                        if (dr["GoalCode"].ToString().Trim() != "RT")
                        {
                            monthlySavingRequired += Math.Round(double.Parse(dr["MonthlySavingsRequired"].ToString()), 2);
                            annualSavingsRequired += Math.Round(double.Parse(dr["yearlySavingsRequired"].ToString()), 2);
                        }
                        if (dr["GoalCode"].ToString().Trim() == "ED")
                        {
                            if (!string.IsNullOrEmpty(dr["ChildName"].ToString().Trim()))
                                strChildText = strChildText.Replace("#ChildName#", dr["ChildName"].ToString().Trim());
                            else
                                strChildText = strChildText.Replace("#ChildName#", "Child");

                            strChildText = strChildText.Replace("#ChildEducationGoalTotal#", convertUSCurrencyFormat(convertTo2Decimal(double.Parse(dr["MonthlySavingsRequired"].ToString().Trim()))));

                            strChildText = strChildText.Replace("#GoalYear#", dr["GoalYear"].ToString().Trim());

                            strFinalGoalText += strChildText;
                        }

                    }

                }

                else if (str.Contains("#ChildName#") && str.Contains("#ChildMarriageGoalTotal#"))
                {
                    foreach (DataRow dr in dtOtherGoalDetails.Rows)
                    {
                        strChildText = str;
                        if (dr["GoalCode"].ToString().Trim() == "MR")
                        {
                            if (!string.IsNullOrEmpty(dr["ChildName"].ToString().Trim()))
                                strChildText = strChildText.Replace("#ChildName#", dr["ChildName"].ToString().Trim());
                            else
                                strChildText = strChildText.Replace("#ChildName#", "Child");

                            strChildText = strChildText.Replace("#ChildMarriageGoalTotal#", convertUSCurrencyFormat(convertTo2Decimal(double.Parse(dr["MonthlySavingsRequired"].ToString().Trim()))));

                            strChildText = strChildText.Replace("#GoalYear#", dr["GoalYear"].ToString().Trim());

                            strChildText = strFinalGoalText += strChildText;
                        }

                    }
                }
                else
                    strFinalGoalText += str;

            }



            foreach (DataRow dr in dtMonthlyGoalTotal.Rows)
            {
                switch (dr[0].ToString())
                {
                    case "BH":
                        {
                            if (double.Parse(dr["MonthyTotal"].ToString()) != 0)
                            {
                                strFinalGoalText = strFinalGoalText.Replace("#BuyHomeGoalTotal#", convertUSCurrencyFormat(convertTo2Decimal(double.Parse(dr["MonthyTotal"].ToString()))));
                            }
                            break;

                        }
                    //case "ED":
                    //    {
                    //        if (double.Parse(dr["MonthyTotal"].ToString()) != 0)
                    //        {
                    //            strOtherGoalText = strOtherGoalText.Replace("#ChildEducationGoalTotal#", convertUSCurrencyFormat(convertTo2Decimal(double.Parse(dr["MonthyTotal"].ToString()))));
                    //        }
                    //        break;

                    //    }
                    //case "MR":
                    //    {
                    //        if (double.Parse(dr["MonthyTotal"].ToString()) != 0)
                    //        {
                    //            strOtherGoalText = strOtherGoalText.Replace("#ChildMarriageGoalTotal#", convertUSCurrencyFormat(convertTo2Decimal(double.Parse(dr["MonthyTotal"].ToString()))));
                    //        }
                    //        break;

                    //    }
                    case "OT":
                        {
                            if (double.Parse(dr["MonthyTotal"].ToString()) != 0)
                            {
                                strFinalGoalText = strFinalGoalText.Replace("#OtherGoalTotal#", convertUSCurrencyFormat(convertTo2Decimal(double.Parse(dr["MonthyTotal"].ToString()))));
                            }
                            break;

                        }

                    default:
                        break;

                }
            }

            strFinalGoalText = strFinalGoalText.Replace("#MonthlyTotalForGoal#", convertUSCurrencyFormat(convertTo2Decimal(monthlySavingRequired)));
            strFinalGoalText = strFinalGoalText.Replace("#AnnualTotalForGoal#", convertUSCurrencyFormat(convertTo2Decimal(annualSavingsRequired)));

            if (dtRTGoal.Rows.Count > 0)
            {
                double retCorps = 0;
                double currentInvestment = 0;
                double roiEarned = 0;
                double fvOnCurrentInvest = 0;
                double rtGapValues = 0;
                double monthlySavingsRequired = 0;
                int goalyear = 0;
                int.TryParse(dtRTGoal.Rows[0]["GoalYear"].ToString(), out goalyear);
                double.TryParse(dtRTGoal.Rows[0]["FVofCostToday"].ToString(), out retCorps);
                //double.TryParse(dtRTGoalDetails.Rows[0]["FVofCostToday"].ToString(), out fvCostOfToday);
                double.TryParse(dtRTGoal.Rows[0]["CurrentInvestment"].ToString(), out currentInvestment);
                double.TryParse(dtRTGoal.Rows[0]["ROIEarnedOnCurrInvest"].ToString(), out roiEarned);
                double.TryParse(dtRTGoal.Rows[0]["FutureValueOnCurrentInvest"].ToString(), out fvOnCurrentInvest);
                double.TryParse(dtRTGoal.Rows[0]["GapValues"].ToString(), out rtGapValues);
                double.TryParse(dtRTGoal.Rows[0]["MonthlySavingsRequired"].ToString(), out monthlySavingsRequired);

                foreach (var pair in dicOtherGoalCodes)
                {
                    if (pair.Value == "Customer_RT_Gaol_Text" && pair.Key == "#RetirementCorpus#")
                    {
                        strRTGoalText = strRTGoalText.Replace(pair.Key, convertUSCurrencyFormat(convertTo2Decimal(retCorps)));
                    }
                    else if (pair.Value == "Customer_RT_Gaol_Text" && pair.Key == "#RTGoalYear#")
                    {
                        strRTGoalText = strRTGoalText.Replace(pair.Key, goalyear.ToString());
                    }
                    else if (pair.Value == "Customer_RT_Gaol_Text" && pair.Key == "#RTCurrentInvestment#" && currentInvestment != 0)
                    {
                        strRTGoalText = strRTGoalText.Replace(pair.Key, convertUSCurrencyFormat(convertTo2Decimal(currentInvestment)));
                    }
                    else if (pair.Value == "Customer_RT_Gaol_Text" && pair.Key == "#RTEarnedPercentage#" && currentInvestment != 0)
                    {
                        strRTGoalText = strRTGoalText.Replace(pair.Key, roiEarned.ToString());
                    }
                    else if (pair.Value == "Customer_RT_Gaol_Text" && pair.Key == "#RTFVOnCurrentInvestment#" && currentInvestment != 0)
                    {
                        strRTGoalText = strRTGoalText.Replace(pair.Key, convertUSCurrencyFormat(convertTo2Decimal(fvOnCurrentInvest)));
                    }
                    else if (pair.Value == "Customer_RT_Gaol_Text" && pair.Key == "#RetirementCorpus# ")
                    {
                        strRTGoalText = strRTGoalText.Replace(pair.Key, convertUSCurrencyFormat(convertTo2Decimal(rtGapValues)));
                    }
                    else if (pair.Value == "Customer_RT_Gaol_Text" && pair.Key == "#RetirementGoalMonthlySavings#")
                    {
                        strRTGoalText = strRTGoalText.Replace(pair.Key, convertUSCurrencyFormat((convertTo2Decimal(monthlySavingsRequired))));
                    }
                    else if (pair.Value == "Customer_RT_Gaol_Text" && pair.Key == "#RTGapValues#")
                    {
                        strRTGoalText = strRTGoalText.Replace(pair.Key, convertUSCurrencyFormat(convertTo2Decimal(rtGapValues)));
                    }

                }
            }

            strOtherGoalTextLines = strFinalGoalText.Split('$');
            foreach (string str in strOtherGoalTextLines)
            {
                if (str.Contains("#ChildEducationGoalTotal#") || str.Contains("#ChildMarriageGoalTotal#") || str.Contains("#BuyHomeGoalTotal#") || str.Contains("#OtherGoalTotal#"))
                {

                }
                else
                {
                    strOtherGoalFinalText.Append(str);
                }


            }

            strRTGoalTextLines = strRTGoalText.Split('~');
            strRTGoalText = string.Empty;
            foreach (string str in strRTGoalTextLines)
            {
                if (str.Contains("#RTCurrentInvestment#") || str.Contains("#RTEarnedPercentage#"))
                {

                }
                else
                {
                    if (str.Contains("no investments"))
                    {
                        if (!strRTGoalText.Contains("already invested"))
                        {
                            strRTGoalText += str;
                        }

                    }
                    else
                    {
                        strRTGoalText += str;

                    }


                }
            }
            double yearlySurplusForCashFlow = 0;
            foreach (DataRow dr in dtCashFlow.Rows)
            {
                if (dr["CashCategory"].ToString() == "Surplus")
                {
                    if (!string.IsNullOrEmpty(dr["Amount"].ToString()))
                    {
                        yearlySurplusForCashFlow = double.Parse(dr["Amount"].ToString());
                        strCashFlowsText = strCashFlowsText.Replace("#AnnualSurplus#", convertUSCurrencyFormat(convertTo2Decimal(yearlySurplusForCashFlow)));

                    }
                    else
                        strCashFlowsText = strCashFlowsText.Replace("#AnnualSurplus#", "0");
                }

            }
            string[] strCashFlowsLines = strCashFlowsText.Split('~');
            strCashFlowsText = string.Empty;
            foreach (string str in strCashFlowsLines)
            {
                if (str.Contains("html"))
                {
                    strCashFlowsText += str;

                }
                else if (str.Contains("surplus"))
                {
                    if (yearlySurplusForCashFlow > 0)
                    {
                        strCashFlowsText += str;
 
                    }
 
                }
                else if (str.Contains("deficit"))
                {
                    if (yearlySurplusForCashFlow < 0)
                    {
                        strCashFlowsText += str;

                    }
                }
 
 
            }

            if (totalMonthlySurplus != 0)
            {
                strIncomeExpenseSurplus = strIncomeExpenseSurplus.Replace("#AnnualSurplus#", convertUSCurrencyFormat(convertTo2Decimal(totalMonthlySurplus * 12)));
                strIncomeExpenseSurplus = strIncomeExpenseSurplus.Replace("#MonthlySurplus#", convertUSCurrencyFormat(convertTo2Decimal(totalMonthlySurplus)));
            }
            else
            {
                strIncomeExpenseSurplus = strIncomeExpenseSurplus.Replace("#AnnualSurplus#", "0");
                strIncomeExpenseSurplus = strIncomeExpenseSurplus.Replace("#MonthlySurplus#", "0");
            }
            

            foreach (DataRow dr in dtRiskProfile.Rows)
            {
                if (dr["Class"].ToString() == "Equity")
                {
                    recEquity = double.Parse(dr["RecommendedPercentage"].ToString());
                    currEquity = double.Parse(dr["CurrentPercentage"].ToString());

                }
                else if (dr["Class"].ToString() == "Debt")
                {
                    recDebt = double.Parse(dr["RecommendedPercentage"].ToString());
                    currDebt = double.Parse(dr["CurrentPercentage"].ToString());
                }
                else if (dr["Class"].ToString() == "Cash")
                {
                    recCash = double.Parse(dr["RecommendedPercentage"].ToString());
                    currCash = double.Parse(dr["CurrentPercentage"].ToString());
                }
                else if (dr["Class"].ToString() == "Alternates")
                {
                    recAlternate = double.Parse(dr["RecommendedPercentage"].ToString());
                    currAlternate = double.Parse(dr["CurrentPercentage"].ToString());
                }

            }

            foreach (DataRow dr in dtAssetAllocation.Rows)
            {
                if (Convert.ToString(dr["Class"]) == "Equity")
                {
                    equityGapPercent = double.Parse(Convert.ToString(dr["ActionNeeded"]));
                   
                }
            }

            string[] strAssetAllocationLines = strRiskProfileAssetAllocationText.Split('~');
            strRiskProfileAssetAllocationText = string.Empty;
            foreach (string str in strAssetAllocationLines)
            {
                if (str.Contains("html"))
                    strRiskProfileAssetAllocationText += str;
                else if (str.Contains("A higher allocation"))
                {
                    if (equityGapPercent > 0)
                        strRiskProfileAssetAllocationText += str;
                }
                else if (str.Contains("A reduction in"))
                {
                    if (equityGapPercent < 0)
                        strRiskProfileAssetAllocationText += str;
                }
                else
                {
                    strRiskProfileAssetAllocationText += str;
                }

            }


            if (dicRiskProfileCodes.Count > 0)
            {
                foreach (var pair in dicRiskProfileCodes)
                {
                    if (pair.Key.Trim() == "#CurrEquity#")
                    {
                        strRiskProfileAssetAllocationText = strRiskProfileAssetAllocationText.Replace(pair.Key, currEquity.ToString() + "%");
                    }
                    else if (pair.Key.Trim() == "#CurrDebt#")
                    {
                        strRiskProfileAssetAllocationText = strRiskProfileAssetAllocationText.Replace(pair.Key, currDebt.ToString() + "%");
                    }
                    else if (pair.Key.Trim() == "#RecEquity#")
                    {
                        strRiskProfileAssetAllocationText = strRiskProfileAssetAllocationText.Replace(pair.Key, recEquity.ToString() + "%");

                    }
                    else if (pair.Key.Trim() == "#RecDebt#")
                    {
                        strRiskProfileAssetAllocationText = strRiskProfileAssetAllocationText.Replace(pair.Key, recDebt.ToString() + "%");

                    }
                    else if (pair.Key.Trim() == "#RecCash#")
                    {
                        strRiskProfileAssetAllocationText = strRiskProfileAssetAllocationText.Replace(pair.Key, recCash.ToString() + "%");

                    }
                    else if (pair.Key.Trim() == "#CurrCash#")
                    {
                        strRiskProfileAssetAllocationText = strRiskProfileAssetAllocationText.Replace(pair.Key, currCash.ToString() + "%");

                    }
                    else if (pair.Key.Trim() == "#CurrAlternate#")
                    {
                        strRiskProfileAssetAllocationText = strRiskProfileAssetAllocationText.Replace(pair.Key, currAlternate.ToString() + "%");

                    }
                    else if (pair.Key.Trim() == "#RecAlternate#")
                    {
                        strRiskProfileAssetAllocationText = strRiskProfileAssetAllocationText.Replace(pair.Key, recAlternate.ToString() + "%");

                    }
                    else if (pair.Key.Trim() == "#RecCashLessMore#")
                    {
                        if (currCash > recCash)
                        {
                            strRiskProfileAssetAllocationText = strRiskProfileAssetAllocationText.Replace(pair.Key, "more");
                        }
                        else
                        {
                            strRiskProfileAssetAllocationText = strRiskProfileAssetAllocationText.Replace(pair.Key, "less");
                        }


                    }
                }
            }

            strRiskProfileAssetAllocationText=strRiskProfileAssetAllocationText.Replace("#CustomerRiskClass#", customerRiskClass.ToUpper());
            //strRiskClassInfoLines = strRiskClassDescription.Split('~');
            //strRiskClassDescription = string.Empty;
            //DataRow drRiskClass = dtAdvisorRiskClass.NewRow();
            //foreach (string str in strRiskClassInfoLines)
            //{
            //    if (str.Contains("html"))
            //    {
            //        strRiskClassDescription = strRiskClassDescription + str;
            //    }
            //    else
            //    {
            //        foreach (DataRow dr in dtAdvisorRiskClass.Rows)
            //        {

            //            switch (dr["RiskCode"].ToString().Trim())
            //            {
            //                case "AG":
            //                    {
            //                        if (str.Contains("Aggressive_"))
            //                        {

            //                            strRiskClassDescription = strRiskClassDescription + str.Replace('_', ' ');
            //                            drRiskClass = dr;
            //                        }
            //                        else
            //                        {
            //                            DataRow drTest = dtAdvisorRiskClass.NewRow();
            //                            drTest["RiskCode"] = "WW";
            //                            drRiskClass = drTest;
            //                        }
            //                        break;

            //                    }
            //                case "CN":
            //                    {
            //                        if (str.Contains("Conservative"))
            //                        {
            //                            strRiskClassDescription = strRiskClassDescription + str;
            //                            drRiskClass = dr;
            //                        }
            //                        else
            //                        {
            //                            DataRow drTest = dtAdvisorRiskClass.NewRow();
            //                            drTest["RiskCode"] = "WW";
            //                            drRiskClass = drTest;
            //                        }
            //                        break;

            //                    }
            //                case "MA":
            //                    {
            //                        if (str.Contains("Moderately Aggressive"))
            //                        {
            //                            strRiskClassDescription = strRiskClassDescription + str;
            //                            drRiskClass = dr;
            //                        }
            //                        else
            //                        {
            //                            DataRow drTest = dtAdvisorRiskClass.NewRow();
            //                            drTest["RiskCode"] = "WW";
            //                            drRiskClass = drTest;
            //                        }
            //                        break;

            //                    }
            //                case "MD":
            //                    {
            //                        if (str.Contains("Moderate"))
            //                        {
            //                            strRiskClassDescription = strRiskClassDescription + str;
            //                            drRiskClass = dr;
            //                        }
            //                        else
            //                        {
            //                            DataRow drTest = dtAdvisorRiskClass.NewRow();
            //                            drTest["RiskCode"] = "WW";
            //                            drRiskClass = drTest;
            //                        }
            //                        break;

            //                    }
            //                case "RA":
            //                    {
            //                        if (str.Contains("Risk Averse"))
            //                        {
            //                            strRiskClassDescription = strRiskClassDescription + str;
            //                            drRiskClass = dr;
            //                        }
            //                        else
            //                        {
            //                            DataRow drTest = dtAdvisorRiskClass.NewRow();
            //                            drTest["RiskCode"] = "WW";
            //                            drRiskClass = drTest;
            //                        }
            //                        break;

            //                    }
            //                case "VA":
            //                    {
            //                        if (str.Contains("Very Aggressive"))
            //                        {
            //                            strRiskClassDescription = strRiskClassDescription + str;
            //                            drRiskClass = dr;
            //                        }
            //                        else
            //                        {
            //                            DataRow drTest = dtAdvisorRiskClass.NewRow();
            //                            drTest["RiskCode"] = "WW";
            //                            drRiskClass = drTest;
            //                        }
            //                        break;

            //                    }
            //                default:
            //                    {
            //                        DataRow drTest = dtAdvisorRiskClass.NewRow();
            //                        drTest["RiskCode"] = "WW";
            //                        drRiskClass = drTest;
            //                        break;
            //                    }
            //            }
            //        }
            //        if (drRiskClass.Table.Rows.Count != 0 && drRiskClass[0].ToString() != "WW")
            //            dtAdvisorRiskClass.Rows.Remove(drRiskClass);
            //    }
            //}


            foreach (DataRow dr in dtAdvisorRiskClass.Rows)
            {

                switch (dr["RiskCode"].ToString().Trim())
                {
                    case "AG":
                        {
                            strRiskClassDescription = strRiskClassDescription.Replace("#RiskClass1#", dr["XRC_RiskClass"].ToString().Trim());
                            strRiskProfileText = strRiskProfileText.Replace("#RiskClass1#", dr["XRC_RiskClass"].ToString().Trim());
                            break;

                        }
                    case "CN":
                        {
                            strRiskClassDescription = strRiskClassDescription.Replace("#RiskClass3#", dr["XRC_RiskClass"].ToString().Trim());
                            strRiskProfileText = strRiskProfileText.Replace("#RiskClass3#", dr["XRC_RiskClass"].ToString().Trim());
                            break;

                        }
                    case "MD":
                        {
                            strRiskClassDescription = strRiskClassDescription.Replace("#RiskClass2#", dr["XRC_RiskClass"].ToString().Trim());
                            strRiskProfileText = strRiskProfileText.Replace("#RiskClass2#", dr["XRC_RiskClass"].ToString().Trim());
                            break;

                        }
                    case "GW":
                        {
                            strRiskClassDescription = strRiskClassDescription.Replace("#RiskClass1#", dr["XRC_RiskClass"].ToString().Trim());
                            strRiskProfileText = strRiskProfileText.Replace("#RiskClass1#", dr["XRC_RiskClass"].ToString().Trim());
                            break;

                        }

                    case "BD":
                        {
                            strRiskClassDescription = strRiskClassDescription.Replace("#RiskClass2#", dr["XRC_RiskClass"].ToString().Trim());
                            strRiskProfileText = strRiskProfileText.Replace("#RiskClass2#", dr["XRC_RiskClass"].ToString().Trim());
                            break;

                        }

                }
            }
            
            strRiskClassLines = strRiskProfileText.Split('~');
            strRiskProfileText = string.Empty;
            foreach (string str in strRiskClassLines)
            {
                if (str.Contains(riskClass) || str.Contains("html"))
                {
                    if (!strRiskProfileText.Contains(riskClass.Trim()))
                        strRiskProfileText += str;
                }
            }

            strRiskProfileText = strRiskProfileText.Replace("#CustomerRiskClass#", customerRiskClass);


            foreach (DataRow dr in dtHLVAssumption.Rows)
            {
                if (Convert.ToString(dr["Assumption_Type"]) == "Investment Return")
                {
                    strHLVNote = strHLVNote.Replace("#InvestmentReturn#", dr["Assumption_Values"].ToString());
                }
                else if (Convert.ToString(dr["Assumption_Type"]) == "Inflation Rate")
                {
                    strHLVNote = strHLVNote.Replace("#InflationPercent#", dr["Assumption_Values"].ToString());
                }
                else if (Convert.ToString(dr["Assumption_Type"]) == "Discount Rate")
                {
                    strHLVNote = strHLVNote.Replace("#DiscountRate#", dr["Assumption_Values"].ToString());
                }
            }

           

            

            strOtherGoalText = strOtherGoalFinalText.ToString();
            //INTRODUCTION PAGE-RM MESSAGAE
            string[] strRmMessageLine= strRMMessage.Split('$');
            strRMMessage = string.Empty;
            foreach (string str in strRmMessageLine)
            {
                if (str.Contains("html"))
                    strRMMessage += str;
                else if (str.Contains("NISM"))
                {
                    if (advisorVo.OrganizationName.Contains("Ratnakar"))
                    {
                        strRMMessage += str;
                    }

                }
                else if (str.Contains("TALK TO US"))
                {
                    if (!advisorVo.OrganizationName.Contains("Ratnakar"))
                    {
                        strRMMessage += str;
                    }
                }
            }

            string[] strDisclaimers = strDisclaimer.Split('~');
            strDisclaimer=string.Empty;
            foreach (string str in strDisclaimers)
            {
                if (str.Contains("html"))
                    strDisclaimer += str;
                else if (str.Contains("Ratnakar"))
                {
                    if (advisorVo.OrganizationName.Contains("Ratnakar"))
                    {
                        strDisclaimer += str;
                    }
                   
                }
                else if (str.Contains("Advisor"))
                {
                    if (!advisorVo.OrganizationName.Contains("Ratnakar"))
                    {
                        strDisclaimer += str;
                    }
                }
            }
            if (dtRMRecommendation.Rows.Count > 0)
                strRMRecommendations = dtRMRecommendation.Rows[0][0].ToString();
            else
                strRMRecommendations = string.Empty;

            crmain.SetParameterValue("CustomerDOB", customerVo.Dob.Day + "-" + customerVo.Dob.ToString("MMM") + "-" + customerVo.Dob.Year.ToString());
            if (!string.IsNullOrEmpty(customerVo.Email.Trim()))
                crmain.SetParameterValue("CustomerEmail", customerVo.Email.Trim());
            else
                crmain.SetParameterValue("CustomerEmail", string.Empty);
            crmain.SetParameterValue("RMMessageParagraph", strRMMessage);
            crmain.SetParameterValue("ImageOpeningLine", strImageOpeningLine);
            crmain.SetParameterValue("FPIntroductionFplanning", strFPIntroFinancialPlanning);
            //crmain.SetParameterValue("FPIntroductionFplanner", strFPIntroFinancialPlanner);
            crmain.SetParameterValue("ProfileSummaryText", strProfileSummaryText);
            crmain.SetParameterValue("FinancialHealthText", strFinancialhealthText);
            crmain.SetParameterValue("KeyAssumptionsText", strKeyAssumtionText);
            crmain.SetParameterValue("GoalProfileOpeningLine", strGoalOpeningLine);
            crmain.SetParameterValue("OtherGoalsText", strOtherGoalText);
            crmain.SetParameterValue("RTOpeningLine", strRTOpeningLine);
            crmain.SetParameterValue("RTGoalText", strRTGoalText);
            crmain.SetParameterValue("CashFlowsText", strCashFlowsText);
            crmain.SetParameterValue("NetWorthopenningLine", strNetWorthOpeningLine);
            crmain.SetParameterValue("NetWorthSummaryText", strNetWorthSummary);
            crmain.SetParameterValue("RiskClassInformation", strRiskClassDescription);
            crmain.SetParameterValue("CustomerRiskClassDescription", strRiskProfileText);
            crmain.SetParameterValue("CustomerAssetAllocationText", strRiskProfileAssetAllocationText);
            crmain.SetParameterValue("InsuranceText", strInsuranceText);
            crmain.SetParameterValue("DisclaimerText", strDisclaimer);
            crmain.SetParameterValue("IncomeExpenseSummaryText", strIncomeExpenseSurplus);
            crmain.SetParameterValue("AssetAllocationText", strAssetAllocation);
            crmain.SetParameterValue("HLV_Note", strHLVNote);
            crmain.SetParameterValue("RatioTest", 65);
            crmain.SetParameterValue("RMRecommendations", strRMRecommendations);


        }
        private string convertUSCurrencyFormat(double value)
        {
            string strValues = string.Empty;
            if (value > 0)
                strValues = value.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            else
                strValues = value.ToString();
            return strValues;
        }

        private double convertTo2Decimal(double value)
        {
            value = Math.Round(value, 2);
            return value;
        }
        /// <summary>
        /// If there is no records to display in report , then hide the controls on page.
        /// </summary>
        private void SetNoRecords()
        {
            lblNoRecords.Visible = true;
            btnSendMail.Visible = false;
        }


        /// <summary>
        /// Display Portfolio Reports.
        /// </summary>
        private void DisplayReport(PortfolioReportVo report)
        {
            try
            {
                PortfolioReportsBo portfolioReports = new PortfolioReportsBo();
                report = (PortfolioReportVo)Session["reportParams"];
                RMVo rmVo = (RMVo)Session["rmvo"];
                switch (report.SubType)
                {

                    case "MULTI_ASSET_SUMMARY_REPORT":

                        //crmain.SetDatabaseLogon("sa", "pcg123#", "122.166.49.39", "wealtherpQA");
                        crmain.Load(Server.MapPath("MultiiAssetReport.rpt"));


                        DataSet dsEquitySectorwise = portfolioReports.GetPortfolioSummary(report, advisorVo.advisorId);
                        DataTable dtEquitySectorwise = dsEquitySectorwise.Tables[0];
                        if (dtEquitySectorwise.Rows.Count > 0)
                        {
                            crmain.Subreports["Liabilities"].Database.Tables[0].SetDataSource(dsEquitySectorwise.Tables[1]);
                            //crmain.Database.Tables["PortfolioSummary"].SetDataSource(dsEquitySectorwise.Tables[1]);
                            crmain.Subreports["NetWorth"].Database.Tables[0].SetDataSource(dsEquitySectorwise.Tables[2]);
                            crmain.Subreports["AssetBreakUp"].Database.Tables[0].SetDataSource(dsEquitySectorwise.Tables[0]);
                            crmain.Subreports["Asset"].Database.Tables[0].SetDataSource(dsEquitySectorwise.Tables[0]);
                            setLogo();
                            //crmain.SetParameterValue("RMName", "Advisor / Financial Planner :  " + rmVo.FirstName + " " + rmVo.LastName);
                            //crmain.SetParameterValue("RMContactDetails", "Email :  " + rmVo.Email);
                            //crmain.SetParameterValue("Organization", advisorVo.OrganizationName);
                            crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("PreviousDate", DateBo.GetPreviousMonthLastDate(report.ToDate));
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                            //crmain.SetParameterValue("DateRange", "Period: " + report.FromDate.ToShortDateString() + " to " + report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);

                            CrystalReportViewer1.ReportSource = crmain;
                            CrystalReportViewer1.EnableDrillDown = true;
                            CrystalReportViewer1.HasCrystalLogo = false;
                            //AssignReportViewerProperties();
                        }
                        else
                            SetNoRecords();
                        break;

                    case "ASSET_ALLOCATION_REPORT":

                        DataSet dsAssetAllocation = portfolioReports.GetCustomerAssetAllocationDetails(report, advisorVo.advisorId, report.SubType);
                        DataTable dtAssetSummary = dsAssetAllocation.Tables[0];
                        DataTable dtAssetDetails = dsAssetAllocation.Tables[1];
                        if (!string.IsNullOrEmpty(report.GroupHead))
                        {
                            crmain.Load(Server.MapPath("CustomerAssetAllocationDetails.rpt"));

                            if (dtAssetSummary.Rows.Count > 0 && dtAssetDetails.Rows.Count > 0)
                            {
                                crmain.Subreports["GroupMemberAssetSummary"].Database.Tables[0].SetDataSource(dtAssetSummary);
                                crmain.Subreports["CustomerAssetdetails"].Database.Tables[0].SetDataSource(dtAssetDetails);
                                setLogo();
                                crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                                AssignReportViewerProperties();
                                crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);

                                crmain.SetParameterValue("ReportHeader", "Group Asset Allocation Report");

                                CrystalReportViewer1.ReportSource = crmain;
                                CrystalReportViewer1.EnableDrillDown = true;
                                CrystalReportViewer1.HasCrystalLogo = false;

                            }
                            else
                                SetNoRecords();


                        }
                        else
                        {
                            crmain.Load(Server.MapPath("IndivisualCustomerAssetAllocationDetails.rpt"));
                            if (dtAssetDetails.Rows.Count > 0)
                            {
                                crmain.SetDataSource(dtAssetDetails);
                                setLogo();
                                crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                                AssignReportViewerProperties();
                                crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                                crmain.SetParameterValue("ReportHeader", "Asset Allocation Report");

                                CrystalReportViewer1.ReportSource = crmain;
                                CrystalReportViewer1.EnableDrillDown = true;
                                CrystalReportViewer1.HasCrystalLogo = false;

                            }
                            else
                                SetNoRecords();

                        }

                        break;




                    case "INVESTMENT_SUMMARY_REPORT":

                        DataSet dsInvestmentDetails = portfolioReports.GetCustomerAssetAllocationDetails(report, advisorVo.advisorId, report.SubType);
                        DataTable dtInvestmentSummary = dsInvestmentDetails.Tables[0];
                        DataTable dtInvestmentDetails = dsInvestmentDetails.Tables[1];

                        if (!string.IsNullOrEmpty(report.GroupHead))
                        {
                            crmain.Load(Server.MapPath("CustomerInvestmentDetails.rpt"));

                            if (dtInvestmentSummary.Rows.Count > 0 && dtInvestmentDetails.Rows.Count > 0)
                            {
                                crmain.Subreports["CustomerInvestmentSummary"].Database.Tables[0].SetDataSource(dtInvestmentSummary);
                                crmain.Subreports["CustomerInvestmentDetails"].Database.Tables[0].SetDataSource(dtInvestmentDetails);

                                setLogo();

                                crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                                AssignReportViewerProperties();
                                crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);

                                crmain.SetParameterValue("ReportHeader", "Group Investment Summary Report");

                                CrystalReportViewer1.ReportSource = crmain;
                                CrystalReportViewer1.EnableDrillDown = true;
                                CrystalReportViewer1.HasCrystalLogo = false;

                            }
                            else
                                SetNoRecords();
                        }
                        else
                        {
                            crmain.Load(Server.MapPath("IndivisualCustomerInvestmentDetails.rpt"));

                            if (dtInvestmentDetails.Rows.Count > 0)
                            {
                                crmain.SetDataSource(dtInvestmentDetails);

                                setLogo();
                                crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                                AssignReportViewerProperties();
                                crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);

                                crmain.SetParameterValue("ReportHeader", "Investment Summary Report");

                                CrystalReportViewer1.ReportSource = crmain;
                                CrystalReportViewer1.EnableDrillDown = true;
                                CrystalReportViewer1.HasCrystalLogo = false;

                            }
                            else
                                SetNoRecords();
                        }

                        break;

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// Display Equity Reports
        /// </summary>
        /// <param name="report"></param>
        private void DisplayReport(EquityReportVo report)
        {

            try
            {

                EquityReportsBo equityReports = new EquityReportsBo();
                report = (EquityReportVo)Session["reportParams"];

                if (Session["CusVo"] != null)
                    customerVo = (CustomerVo)Session["CusVo"];
                else if (Session["customerVo"] != null)
                    customerVo = (CustomerVo)Session["customerVo"];

                switch (report.SubType)
                {

                    case "EQUITY_SECTOR_WISE":

                        crmain.Load(Server.MapPath("EqSectorwise.rpt"));
                        DataTable dtEquitySectorwise = equityReports.GetEquityScripwiseSummary(report, advisorVo.advisorId);
                        if (dtEquitySectorwise.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtEquitySectorwise);
                            setLogo();
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();
                        }
                        else
                            SetNoRecords();
                        break;

                    case "EQUITY_TRANSACTION_WISE":
                        crmain.Load(Server.MapPath("EquityTransactionWise.rpt"));
                        DataTable dtEquitytransactionwise = equityReports.GetEquityTransaction(report, advisorVo.advisorId);
                        if (dtEquitytransactionwise.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtEquitytransactionwise);
                            setLogo();
                            AssignReportViewerProperties();
                            crmain.SetParameterValue("DateRange", "Period: " + report.FromDate.ToShortDateString() + " to " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString());

                        }
                        else
                            SetNoRecords();
                        break;

                    case "EQUITY_HOLDING_WISE":
                        crmain.Load(Server.MapPath("EquityHoldingWise.rpt"));
                        DataSet dsEquityholdingwise = equityReports.GetEquityHolding(report, advisorVo.advisorId);

                        if (dsEquityholdingwise.Tables[0].Rows.Count > 0)
                        {
                            //dsEquityholdingwise.Tables[0].TableName = "EquityHolding";
                            crmain.SetDataSource(dsEquityholdingwise.Tables[0]);

                            setLogo();
                            AssignReportViewerProperties();
                            crmain.SetParameterValue("DateRange", "Period: " + report.FromDate.ToShortDateString() + " to " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString());

                        }
                        else
                            SetNoRecords();
                        break;


                    case "EQ_TRANSACTION_REPORT":
                        crmain.Load(Server.MapPath("EqTransactionAll.rpt"));
                        DataTable dtEquityTransactionsAll = equityReports.GetEquityTransactionAll(report);

                        if (dtEquityTransactionsAll.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtEquityTransactionsAll);
                            setLogo();
                            if (PreviousPage != null)
                            {
                                if (Request.Form["ctrl_EquityReports$Filter"] == "rdoSpeculative")
                                {
                                    crmain.SetParameterValue("ReportType", "Yes");
                                    equityReport.isSpeculative = "Yes";
                                }
                                else if (Request.Form["ctrl_EquityReports$Filter"] == "rdoDerivative")
                                {
                                    equityReport.isSpeculative = "No";
                                    crmain.SetParameterValue("ReportType", "No");
                                }
                                else if (Request.Form["ctrl_EquityReports$Filter"] == "rdoAll")
                                {
                                    equityReport.isSpeculative = "All";
                                    crmain.SetParameterValue("ReportType", "All");
                                }
                                Session["reportParams"] = equityReport;
                            }
                            else
                                crmain.SetParameterValue("ReportType", report.isSpeculative);

                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();
                        }
                        else
                            SetNoRecords();
                        break;

                    case "EQ_PORTFOLIO_RETURNS_REPORT":

                        DataTable dtEquityPortfolioTransactions = equityReports.GetCustomerPortfolioEquityTransactions(report, advisorVo.advisorId);

                        if (dtEquityPortfolioTransactions.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtEquityPortfolioTransactions);
                            setLogo();
                            if (PreviousPage != null)
                            {
                                if (Request.Form["ctrl_EquityReports$Filter"] == "rdoSpeculative")
                                {
                                    crmain.SetParameterValue("ReportType", "Yes");
                                    equityReport.isSpeculative = "Yes";
                                }
                                else if (Request.Form["ctrl_EquityReports$Filter"] == "rdoDerivative")
                                {
                                    equityReport.isSpeculative = "No";
                                    crmain.SetParameterValue("ReportType", "No");
                                }
                                else if (Request.Form["ctrl_EquityReports$Filter"] == "rdoAll")
                                {
                                    equityReport.isSpeculative = "All";
                                    crmain.SetParameterValue("ReportType", "All");
                                }
                            }
                            else
                                crmain.SetParameterValue("ReportType", report.isSpeculative);

                            crmain.SetParameterValue("AsOnDate", report.FromDate.ToShortDateString());

                            AssignReportViewerProperties();
                        }
                        else
                            SetNoRecords();
                        break;



                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        /// <summary>
        /// Display MF Reports
        /// </summary>
        /// <param name="report"></param>
        private void DisplayReport(MFReportVo report)
        {

            try
            {
                MFReportsBo mfReports = new MFReportsBo();
                report = (MFReportVo)Session["reportParams"];

                if (Session["CusVo"] != null)
                    customerVo = (CustomerVo)Session["CusVo"];
                else if (Session["customerVo"] != null)
                    customerVo = (CustomerVo)Session["customerVo"];

                //Customer Individual LogIn...
                if (Session["hndCustomerLogin"].ToString() == "true")
                {
                    if (ViewState["CustomerId"] == null && Request.Form["ctrl_MFReports$hdnCustomerId1"] != null)
                        ViewState["CustomerId"] = Request.Form["ctrl_MFReports$hdnCustomerId1"];

                    customerVo = customerBo.GetCustomer(int.Parse(ViewState["CustomerId"].ToString()));
                    Session["customerVo"] = customerVo;


                }


                switch (report.SubType)
                {

                    //MF Reports 

                    case "CAPITAL_GAIN_SUMMARY":
                        crmain.Load(Server.MapPath("CapitalGainSummary.rpt"));
                        DataTable dtCapitalGainSummary = mfReports.GetCapitalGainSummaryReport(report);
                        if (dtCapitalGainSummary.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtCapitalGainSummary);
                            setLogo();
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("DateRange", "Period: " + report.FromDate.ToShortDateString() + " to " + report.ToDate.ToShortDateString());
                            //crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            //crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();

                            //For PDF View In Browser : Author-Pramod
                            if (Request.QueryString["mail"] == "2")
                            {
                                ExportInPDF();

                            }

                        }
                        else
                            SetNoRecords();
                        break;

                    case "CAPITAL_GAIN_DETAILS":
                        crmain.Load(Server.MapPath("CapitalGainDetails.rpt"));
                        DataTable dtCapitalGainDetails = mfReports.GetCapitalGainDetailsReport(report);
                        if (dtCapitalGainDetails.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtCapitalGainDetails);
                            setLogo();
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("DateRange", "Period: " + report.FromDate.ToShortDateString() + " to " + report.ToDate.ToShortDateString());
                            //crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            //crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();

                            //For PDF View In Browser : Author-Pramod
                            if (Request.QueryString["mail"] == "2")
                            {
                                ExportInPDF();
                            }
                        }
                        else
                            SetNoRecords();
                        break;


                    case "CATEGORY_WISE":
                        crmain.Load(Server.MapPath("MFFundSummary.rpt"));

                        DataSet dsMFFundSummary = mfReports.GetMFFundSummaryReport(report, advisorVo.advisorId);
                        if (dsMFFundSummary.Tables[0].Rows.Count > 0 || dsMFFundSummary.Tables[1].Rows.Count > 0)
                        {
                            crmain.Subreports["OpenPositionReport"].Database.Tables[0].SetDataSource(dsMFFundSummary.Tables[0]);
                            crmain.Subreports["AllPositionReport1"].Database.Tables[0].SetDataSource(dsMFFundSummary.Tables[1]);
                            crmain.Subreports["AllPositionReport2"].Database.Tables[0].SetDataSource(dsMFFundSummary.Tables[1]);
                            setLogo();
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("PreviousMonthDate", DateBo.GetPreviousMonthLastDate(report.FromDate).ToShortDateString());
                            crmain.SetParameterValue("AsOnDate", report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();

                            //For PDF View In Browser : Author-Pramod
                            if (Request.QueryString["mail"] == "2")
                            {
                                ExportInPDF();
                            }
                        }
                        else
                            SetNoRecords();
                        break;

                    case "TRANSACTION_REPORT":
                        crmain.Load(Server.MapPath("MFTransactions.rpt"));
                        DataTable dtTransactions = mfReports.GetTransactionReport(report);
                        if (dtTransactions.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtTransactions);
                            setLogo();
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("DateRange", "Period: " + report.FromDate.ToShortDateString() + " to " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();

                            //For PDF View In Browser : Author-Pramod
                            if (Request.QueryString["mail"] == "2")
                            {
                                ExportInPDF();
                            }
                        }
                        else
                            SetNoRecords();
                        break;

                    case "DIVIDEND_STATEMENT":
                        crmain.Load(Server.MapPath("MFDividend.rpt"));
                        DataTable dtDividend = mfReports.GetDivdendReport(report);
                        if (dtDividend.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtDividend);
                            setLogo();
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("DateRange", "Period: " + report.FromDate.ToShortDateString() + " to " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());

                            AssignReportViewerProperties();

                            //For PDF View In Browser : Author-Pramod
                            if (Request.QueryString["mail"] == "2")
                            {
                                ExportInPDF();
                            }
                        }
                        else
                            SetNoRecords();
                        break;

                    case "DIVIDEND_SUMMARY":
                        crmain.Load(Server.MapPath("MFDividendSummary.rpt"));
                        DataTable dtDividendSummary = mfReports.GetDivdendReport(report);
                        if (dtDividendSummary.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtDividendSummary);
                            setLogo();
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("DateRange", "Period: " + report.FromDate.ToShortDateString() + " to " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());

                            AssignReportViewerProperties();

                            //For PDF View In Browser : Author-Pramod
                            if (Request.QueryString["mail"] == "2")
                            {
                                ExportInPDF();
                            }
                        }
                        else
                            SetNoRecords();
                        break;

                    case "RETURNS_PORTFOLIO":
                        crmain.Load(Server.MapPath("MFReturns.rpt"));
                        DataTable dtReturnsPortfolio = mfReports.GetReturnSummaryReport(report, advisorVo.advisorId);
                        if (dtReturnsPortfolio.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtReturnsPortfolio);
                            setLogo();
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("AsOnDate", report.FromDate.ToShortDateString());
                            AssignReportViewerProperties();

                            //For PDF View In Browser
                            if (Request.QueryString["mail"] == "2")
                            {
                                ExportInPDF();
                            }
                        }
                        else
                            SetNoRecords();
                        break;
                    case "COMPREHENSIVE":
                        crmain.Load(Server.MapPath("MFPortfolioAnalytics.rpt"));

                        DataSet dsReturnsPortfolio = mfReports.GetPortfolioAnalyticsReport(report, advisorVo.advisorId);
                        if (dsReturnsPortfolio.Tables[0].Rows.Count > 0)
                        {
                            crmain.SetDataSource(dsReturnsPortfolio.Tables[0]);
                            crmain.Subreports["MFSchemePerformance"].Database.Tables[0].SetDataSource(dsReturnsPortfolio.Tables[1]);
                            crmain.Subreports["MFTopTenHoldings"].Database.Tables[0].SetDataSource(dsReturnsPortfolio.Tables[2]);
                            crmain.Subreports["MFTopTenSectors"].Database.Tables[0].SetDataSource(dsReturnsPortfolio.Tables[5]);

                            setLogo();
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("AsOnDate", report.FromDate.ToShortDateString());
                            AssignReportViewerProperties();

                            //For PDF View In Browser
                            if (Request.QueryString["mail"] == "2")
                            {
                                ExportInPDF();
                            }
                        }
                        else
                            SetNoRecords();
                        break;

                    //Added Three more cases for Display three new report : Author-Pramod
                    case "RETURNS_PORTFOLIO_REALIZED":
                        crmain.Load(Server.MapPath("MFReturnsRealized.rpt"));
                        DataTable dtReturnsREPortfolio = mfReports.GetMFReturnRESummaryReport(report, advisorVo.advisorId);
                        if (dtReturnsREPortfolio.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtReturnsREPortfolio);
                            setLogo();
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("AsOnDate", report.FromDate.ToShortDateString());
                            AssignReportViewerProperties();

                            //For PDF View In Browser : Author-Pramod
                            if (Request.QueryString["mail"] == "2")
                            {
                                ExportInPDF();
                            }
                        }
                        else
                            SetNoRecords();
                        break;

                    case "ELIGIBLE_CAPITAL_GAIN_DETAILS":
                        crmain.Load(Server.MapPath("EligibleCapitalGainsDetails.rpt"));
                        DataTable dtEligibleCapitalGainsDetails = mfReports.GetEligibleCapitalGainDetailsReport(report);
                        if (dtEligibleCapitalGainsDetails.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtEligibleCapitalGainsDetails);
                            setLogo();
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName.TrimEnd() + " " + customerVo.MiddleName.TrimEnd() + " " + customerVo.LastName.TrimEnd());
                            crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();

                            //For PDF View In Browser : Author-Pramod
                            if (Request.QueryString["mail"] == "2")
                            {
                                ExportInPDF();
                            }
                        }
                        else
                            SetNoRecords();
                        break;

                    case "ELIGIBLE_CAPITAL_GAIN_SUMMARY":
                        crmain.Load(Server.MapPath("EligibleCapitalGainsSummary.rpt"));
                        DataTable dtEligibleCapitalGainsSummary = mfReports.GetEligibleCapitalGainDetailsReport(report);
                        if (dtEligibleCapitalGainsSummary.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtEligibleCapitalGainsSummary);
                            setLogo();
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();

                            //For PDF View In Browser : Author-Pramod
                            if (Request.QueryString["mail"] == "2")
                            {
                                ExportInPDF();
                            }
                        }
                        else
                            SetNoRecords();
                        break;



                }



            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion


        #region Report Related Helper Methods

        /// <summary>
        /// Assign common parameter field values to reports. Also set Crystal Report Viewer properties.
        /// </summary>
        private void AssignReportViewerProperties()
        {
            RMVo rmVo = (RMVo)Session["rmVo"];
            AdvisorStaffBo adviserStaffBo = new AdvisorStaffBo();
            string state = "";
            if (Session["CusVo"] != null)
                customerVo = (CustomerVo)Session["CusVo"];
            else if (Session["customerVo"] != null)
                customerVo = (CustomerVo)Session["customerVo"];
            try
            {
                //string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                //setLogo();
                //if (advisorVo.State != null)
                //    state = CommonReport.GetState(path, advisorVo.State);
                customerRMVo = adviserStaffBo.GetAdvisorStaffDetails(customerVo.RmId);
                crmain.SetParameterValue("RMName", "Advisor / Financial Planner: " + (customerRMVo.FirstName + " " + customerRMVo.MiddleName + " " + customerRMVo.LastName).Trim());
                if (!string.IsNullOrEmpty(customerRMVo.Email))
                    crmain.SetParameterValue("OrgDetails", "Email :  " + customerRMVo.Email);
                else
                    crmain.SetParameterValue("OrgDetails", "Email :--");

                if (customerRMVo.Mobile != 0)
                {
                    crmain.SetParameterValue("OrgTelephone", "Mobile :  " + "+91-" + customerRMVo.Mobile);
                }
                else
                {
                    crmain.SetParameterValue("OrgTelephone", "Mobile :--");
                }
                crmain.SetParameterValue("OrgAddress", advisorVo.City.Trim() + ", " + state.Trim());
                //crmain.SetParameterValue("OrgDetails", "E-mail: " + advisorVo.Email);
                //crmain.SetParameterValue("OrgTelephone", "Phone: " + "+91-" + advisorVo.Phone1Std + "-" + advisorVo.Phone1Number);
                crmain.SetParameterValue("RMContactDetails", "E-mail: " + advisorVo.Email);
                crmain.SetParameterValue("MobileNo", "Phone: " + "+" + advisorVo.MobileNumber.ToString());

                if (!string.IsNullOrEmpty(customerVo.Adr1Line1.Trim()) && !string.IsNullOrEmpty(customerVo.Adr1City.Trim()))
                    crmain.SetParameterValue("CustomerAddress", customerVo.Adr1Line1.Trim() + " " + customerVo.Adr1City.Trim());
                else if (!string.IsNullOrEmpty(customerVo.Adr1Line1.Trim()) && string.IsNullOrEmpty(customerVo.Adr1City.Trim()))
                    crmain.SetParameterValue("CustomerAddress", customerVo.Adr1Line1.Trim());
                else if (!string.IsNullOrEmpty(customerVo.Adr1City.Trim()) && string.IsNullOrEmpty(customerVo.Adr1Line1.Trim()))
                    crmain.SetParameterValue("CustomerAddress", customerVo.Adr1City.Trim());
                else
                    crmain.SetParameterValue("CustomerAddress", "");

                crmain.SetParameterValue("CustomerEmail", "Email :  " + customerVo.Email);
                crmain.SetParameterValue("Organization", advisorVo.OrganizationName);




            }
            catch (Exception ex)
            {

            }

            CrystalReportViewer1.ReportSource = crmain;
            if (crmain.PrintOptions.PaperOrientation == PaperOrientation.Landscape)
            {
                CrystalReportViewer1.Attributes.Add("ToolbarStyle-Width", "900px");
            }

            CrystalReportViewer1.EnableDrillDown = true;
            CrystalReportViewer1.HasCrystalLogo = false;
        }


        /// <summary>
        /// Get Report Parameters and store it in to session.
        /// </summary>
        private void GetReportParameters()
        {
            CalculateDateRange(out dtFrom, out dtTo);
            if (CurrentReportType == ReportType.EquityReports)
            {
                equityReport.SubType = Request.Form[ctrlPrefix + "ddlReportSubType"];

                equityReport.FromDate = dtFrom;
                equityReport.ToDate = dtTo;

                equityReport.PortfolioIds = GetPortfolios();

                if (!String.IsNullOrEmpty(Request.Form[ctrlPrefix + "TabContainer1$TabPanel2$txtCustomerId"]))
                    equityReport.CustomerIds = Request.Form[ctrlPrefix + "TabContainer1$TabPanel$txtCustomerId"];

                if (!String.IsNullOrEmpty(Request.Form[ctrlPrefix + "TabContainer1$TabPanel1$txtParentCustomerId"]))
                    equityReport.GroupHead = Request.Form[ctrlPrefix + "TabContainer1$TabPanel1$txtParentCustomerId"];

                //txtParentCustomerId

                Session["reportParams"] = equityReport;
            }
            else if (CurrentReportType == ReportType.MFReports)
            {
                mfReport.SubType = Request.Form[ctrlPrefix + "ddlReportSubType"];
                if (Request.Form["ctrl_MFReports$hndCustomerLogin"] == "true" && Request.Form["ctrl_MFReports$hndSelfOrGroup"] == "self")
                {
                    mfReport.PortfolioIds = GetAllPortfolioOfCustomer(int.Parse(Request.Form["ctrl_MFReports$hdnCustomerId1"]));

                }
                else

                    mfReport.PortfolioIds = GetPortfolios();
                //MF Transaction report Fiter Creiteria 
                if (Request.Form[ctrlPrefix + "ddlMFTransactionType"] == "0")
                {
                    mfReport.FilterBy = "ALL";
                }
                else
                    mfReport.FilterBy = Request.Form[ctrlPrefix + "ddlMFTransactionType"];

                if (Request.Form[ctrlPrefix + "Transation"] == "rdScheme")
                {
                    mfReport.OrderBy = "Scheme";
                }
                else
                {
                    mfReport.OrderBy = "Date";
                }

                mfReport.FromDate = dtFrom;
                mfReport.ToDate = dtTo;

                if (!String.IsNullOrEmpty(Request.Form["ctrl_MFReports$hdnCustomerId1"]))
                {
                    mfReport.CustomerIds = Request.Form["ctrl_MFReports$hdnCustomerId1"];
                    mfReport.GroupHead = Request.Form["ctrl_MFReports$hdnCustomerId1"];

                }



                //if (!String.IsNullOrEmpty(Request.Form[ctrlPrefix + "TabContainer1$TabPanel1$txtParentCustomerId"]))
                //    mfReport.GroupHead = Request.Form[ctrlPrefix + "TabContainer1$TabPanel1$txtParentCustomerId"];

                Session["reportParams"] = mfReport;
            }
            else if (CurrentReportType == ReportType.PortfolioReports)
            {
                portfolioReport.SubType = Request.Form[ctrlPrefix + "ddlReportSubType"];
                portfolioReport.PortfolioIds = GetPortfolios();

                portfolioReport.FromDate = dtFrom;
                portfolioReport.ToDate = dtTo;


                if (!String.IsNullOrEmpty(Request.Form[ctrlPrefix + "TabContainer1$TabPanel1$txtParentCustomerId"]))
                    portfolioReport.GroupHead = Request.Form[ctrlPrefix + "TabContainer1$TabPanel1$txtParentCustomerId"];

                if (!String.IsNullOrEmpty(Request.Form[ctrlPrefix + "TabContainer1$TabPanel2$txtCustomerId"]))
                    portfolioReport.CustomerIds = Request.Form[ctrlPrefix + "TabContainer1$TabPanel2$txtCustomerId"];

                Session["reportParams"] = portfolioReport;
            }
            else if (CurrentReportType == ReportType.FinancialPlanning || CurrentReportType == ReportType.FinancialPlanningSectional)
            {
                //portfolioReport.SubType = Request.Form[ctrlPrefix + "ddlReportSubType"];
                //portfolioReport.PortfolioIds = GetPortfolios();

                //portfolioReport.FromDate = dtFrom;
                //portfolioReport.ToDate = dtTo;




                if (Session["CusVo"] != null)
                    customerVo = (CustomerVo)Session["CusVo"];
                else if (Session["customerVo"] != null)
                    customerVo = (CustomerVo)Session["customerVo"];
                if (customerVo.IsProspect == 1)
                    financialPlanning.isProspect = 1;
                else
                    financialPlanning.isProspect = 0;
                financialPlanning.CustomerId = customerVo.CustomerId.ToString();
                financialPlanning.advisorId = advisorVo.advisorId;

                Session["reportParams"] = financialPlanning;
            }
            else if (CurrentReportType == ReportType.FPOfflineForm)
            {
                fpOfflineForm.advisorId = advisorVo.advisorId;
                Session["reportParams"] = fpOfflineForm;
            }

        }
        /// <summary>
        ///When Customer Indivisual Login and report generating On self radioButton selction. This will give all portfolio of customer :Author Pramod
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private string GetAllPortfolioOfCustomer(int customerId)
        {
            string portfolioIDs = "";
            PortfolioBo portfolioBo = new PortfolioBo();
            if (!String.IsNullOrEmpty(customerId.ToString())) //Note : customer Id assigned to txtCustomerId(hidden field) when the user selects customer from customer name suggestion text box
            {
                //int customerId = Convert.ToInt32(txtParentCustomerId.Value);
                List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId); //Get all the portfolios of the selected customer.
                if (customerPortfolioVos != null && customerPortfolioVos.Count > 0) //One or more folios available for selected customer
                {

                    foreach (CustomerPortfolioVo custPortfolio in customerPortfolioVos)
                    {
                        if (Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$ddlPortfolioGroup"] == "ALL")
                        {
                            portfolioIDs = portfolioIDs + custPortfolio.PortfolioId;
                            portfolioIDs = portfolioIDs + ",";

                        }
                        else if (Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$ddlPortfolioGroup"] == "MANAGED")
                        {
                            if (custPortfolio.PortfolioName == "MyPortfolio")
                            {
                                portfolioIDs = portfolioIDs + custPortfolio.PortfolioId;
                                portfolioIDs = portfolioIDs + ",";
                            }
                        }
                        else if (Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$ddlPortfolioGroup"] == "UN_MANAGED")
                        {
                            if (custPortfolio.PortfolioName != "MyPortfolio")
                            {
                                portfolioIDs = portfolioIDs + custPortfolio.PortfolioId;
                                portfolioIDs = portfolioIDs + ",";
                            }

                        }
                        //checkbox.Append("<input type='checkbox' checked name='chk--" + custPortfolio.PortfolioId + "' id='chk--" + custPortfolio.PortfolioId + "'>" + custPortfolio.PortfolioName);
                        //checkboxList.Items.Add(new ListItem(custPortfolio.PortfolioName, custPortfolio.PortfolioId.ToString()));
                    }

                }

            }

            if (portfolioIDs.EndsWith(","))
                portfolioIDs = portfolioIDs.Substring(0, portfolioIDs.Length - 1);
            return portfolioIDs;


        }

        /// <summary>
        /// Get all the selected portfolio Ids and return it as a comma separated string. Author:Pramod
        /// </summary>
        private string GetPortfolios()
        {
            string portfolios = string.Empty;


            foreach (string strForm in Request.Params.AllKeys)
            {
                if (strForm.StartsWith("chk--"))
                {
                    portfolios += strForm.Replace("chk--", "") + ",";
                }

            }

            if (portfolios.EndsWith(","))
                portfolios = portfolios.Substring(0, portfolios.Length - 1);
            return portfolios;

        }

        /// <summary>
        /// Get the From and To Date of reports
        /// </summary>
        private void CalculateDateRange(out DateTime fromDate, out DateTime toDate)
        {
            string portfolioIds = "";
            string subReportType = "";
            DateTime CalculateFromDate = DateTime.MinValue;
            if (Request.Form["ctrl_MFReports$hidDateType"] == "DATE_RANGE" || Request.Form["ctrl_EquityReports$hidDateType"] == "DATE_RANGE")
            {

                fromDate = Convert.ToDateTime(Request.Form[ctrlPrefix + "txtFromDate"]);
                toDate = Convert.ToDateTime(Request.Form[ctrlPrefix + "txtToDate"]);
            }
            else if (Request.Form["ctrl_MFReports$hidDateType"] == "PERIOD" || Request.Form["ctrl_EquityReports$hidDateType"] == "PERIOD")
            {
                if (Request.Form[ctrlPrefix + "ddlPeriod"] == "15") //Calculate FromDate for Since Inception Option for Period Selection
                {
                    
                    portfolioIds = GetPortfolios();
                    subReportType = GetReportSubtype();
                    CalculateFromDate = GetCalculateFromDate(portfolioIds, subReportType);
                    fromDate = CalculateFromDate;
                    toDate = DateTime.Now;

                }
                else// Calculate ToDate And FromDate For Period Selection
                {
                    dtBo.CalculateFromToDatesUsingPeriod(Request.Form[ctrlPrefix + "ddlPeriod"], out dtFrom, out dtTo);
                    fromDate = dtFrom;
                    toDate = dtTo;
                }

            }
            else //if (Request.Form[ctrlPrefix + "hidDateType"] == "AS_ON")
            {
                

                fromDate = Convert.ToDateTime(Request.Form[ctrlPrefix + "txtAsOnDate"]);
                toDate = Convert.ToDateTime(Request.Form[ctrlPrefix + "txtAsOnDate"]);

            }
        }
        /// <summary>
        /// Calculate FromDate for Since Inception Option for Period Selection
        /// </summary>
        /// <param name="portfolioIDs"></param>
        /// <param name="subreportype"></param>
        /// <returns></returns>
        private DateTime GetCalculateFromDate(string portfolioIDs, string subreportype)
        {
            MFReportsBo mfReports = new MFReportsBo();
            DataTable dtCalculateFromDate = new DataTable();
            DateTime fromDate = DateTime.MinValue; ;

            try
            {
                fromDate = mfReports.GetCalculateFromDate(portfolioIDs, subreportype);
          
            }
            catch (BaseApplicationException Ex)
            {
                throw(Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "Display.aspx.cs:GetCalculateFromDate()");
                object[] objects = new object[2];
                objects[0] = portfolioIDs;
                objects[1] = subreportype;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return fromDate;

        }

     
        /// <summary>
        /// To Get The Report SubTypes
        /// </summary>
        /// <returns></returns>
        private string GetReportSubtype()
        {
            string subReortType = String.Empty;
            string subType = "";
            if (CurrentReportType == ReportType.EquityReports)
            {
                
               equityReport.SubType = Request.Form[ctrlPrefix + "ddlReportSubType"];
                switch (equityReport.SubType)
                {
                    case "EQUITY_SECTOR_WISE":
                        subType = "5";
                        break;
                    case "EQUITY_TRANSACTION_WISE":
                        subType = "6";
                        break;
                    case "EQUITY_HOLDING_WISE":
                        subType = "7";
                        break;
                }
                return subType;
            }
            else if (CurrentReportType == ReportType.MFReports)
            {
                mfReport.SubType = Request.Form[ctrlPrefix + "ddlReportSubType"];
                switch (mfReport.SubType)
                {
                    case "TRANSACTION_REPORT":
                        subType = "0";
                        break;
                    case "DIVIDEND_STATEMENT":
                        subType = "1";
                        break;
                    case "DIVIDEND_SUMMARY":
                        subType = "2";
                        break;
                    case "CAPITAL_GAIN_DETAILS":
                        subType = "3";
                        break;
                    case "CAPITAL_GAIN_SUMMARY":
                        subType = "4";
                        break;

                }
                return subType;
            }

            subReortType = subType;
            return subReortType;

        }
        
        #endregion


        #region Email Related Methods

        /// <summary>
        /// Pre Fill the Email sending form with values.
        /// </summary>
        private void FillEmailValues()
        {

            if (!IsPostBack)
            {
                CustomerBo customerBo = new CustomerBo();
                CustomerVo cust = null;

                string subType = string.Empty;
                DateTime fromDate = DateTime.MinValue;
                DateTime toDate = DateTime.MinValue;

                if (CurrentReportType == ReportType.MFReports)
                {
                    subType = mfReport.SubType;
                    fromDate = mfReport.FromDate;
                    toDate = mfReport.ToDate;

                    if (!String.IsNullOrEmpty(mfReport.GroupHead))
                        cust = customerBo.GetCustomer(Convert.ToInt32(mfReport.GroupHead));
                    else
                        cust = customerBo.GetCustomer(Convert.ToInt32(mfReport.CustomerIds));
                }
                else if (CurrentReportType == ReportType.EquityReports)
                {
                    subType = equityReport.SubType;
                    fromDate = equityReport.FromDate;
                    toDate = equityReport.ToDate;

                    if (!String.IsNullOrEmpty(equityReport.GroupHead))
                        cust = customerBo.GetCustomer(Convert.ToInt32(equityReport.GroupHead));
                    else
                        cust = customerBo.GetCustomer(Convert.ToInt32(equityReport.CustomerIds));
                }
                else if (CurrentReportType == ReportType.PortfolioReports)
                {
                    subType = portfolioReport.SubType;
                    fromDate = portfolioReport.FromDate;
                    toDate = portfolioReport.ToDate;

                    if (!String.IsNullOrEmpty(portfolioReport.GroupHead))
                        cust = customerBo.GetCustomer(Convert.ToInt32(portfolioReport.GroupHead));
                    else
                        cust = customerBo.GetCustomer(Convert.ToInt32(portfolioReport.CustomerIds));
                }
                else if (CurrentReportType == ReportType.FinancialPlanning || CurrentReportType == ReportType.FinancialPlanningSectional)
                {
                    subType = string.Empty;
                    fromDate = DateTime.Today;
                    toDate = DateTime.Today;
                    cust = customerBo.GetCustomer(Convert.ToInt32(financialPlanning.CustomerId));
                }
                else if (CurrentReportType == ReportType.FPOfflineForm)
                {
                    cust = new CustomerVo();
                    cust.Email = String.Empty;
                    cust.Salutation = string.Empty;
                    cust.FirstName = string.Empty;
                    cust.LastName = string.Empty;
                    subType = "FPOfflineForm";
                }

                if (!string.IsNullOrEmpty(txtCC.Text.Trim()))
                Session["hidCC"] = txtCC.Text;
                Session["hidTo"] = cust.Email;
                if (Session["hidTo"] != null)
                    Session["hidTo"] = txtTo.Text = cust.Email;

                Session["hidSubject"] = txtSubject.Text = GetReportSubject(subType, fromDate, toDate);
                if (cust.Salutation == string.Empty || cust.Salutation == "")
                {
                    Session["hidBody"] = txtBody.Text = GetReportBody(cust.FirstName + " " + cust.LastName, subType, fromDate, toDate).Replace("\r", "");

                }
                else
                {
                    Session["hidBody"] = txtBody.Text = GetReportBody(cust.Salutation + " " + cust.FirstName + " " + cust.LastName, subType, fromDate, toDate).Replace("\r", "");

                }


            }
            else
            {
                if (Session["hidSubject"] != null)
                    txtSubject.Text = Session["hidSubject"].ToString();
                if (Session["hidCC"] != null)
                    txtCC.Text = Session["hidCC"].ToString();
                if (Session["hidTo"] != null)
                    txtTo.Text = Session["hidTo"].ToString();
                if (Session["hidBody"] != null)
                    txtBody.Text = Session["hidBody"].ToString().Replace("\r", "");
                if (hidCCMe.Value == "true")
                    chkCopy.Checked = true;
                else
                    chkCopy.Checked = false;
            }
        }


        /// <summary>
        /// Get the email body of the report.
        /// </summary>
        private string GetReportBody(string customerName, string reportType, DateTime start, DateTime end)
        {
            StringBuilder strMail = new StringBuilder();
            RMVo rmVo = (RMVo)Session["rmVo"];

            string subject = string.Empty;
            if (CurrentReportType == ReportType.EquityReports)
                subject = "Equity Report";
            else if (CurrentReportType == ReportType.MFReports)
                subject = "MF Report";
            else if (CurrentReportType == ReportType.PortfolioReports)
                subject = "Portfolio Report";
            else if (CurrentReportType == ReportType.FinancialPlanning)
                subject = "Financial Planning Report";
            else if (CurrentReportType == ReportType.FPOfflineForm)
            {
                subject = "FP Offline Form";
                
            }

            if (CurrentReportType == ReportType.FPOfflineForm)
            {
                strMail.Append("Dear " + customerName + ",<br/>");
                strMail.Append("<br/>Please find attached " + subject + ".");
                if (!string.IsNullOrEmpty(advisorVo.Website))
                {
                    strMail.Append("<br/><br/>Regards,<br/>" + RMDataFormatFormat(rmVo) + "<br/>Mo: " + rmVo.Mobile + "<br/>Ph: +" + rmVo.OfficePhoneDirectStd +"-" + rmVo.OfficePhoneDirectNumber + "<br/>Website: " + advisorVo.Website);
                }
                else
                    strMail.Append("<br/><br/>Regards,<br/>" + RMDataFormatFormat(rmVo) + "<br/>Mo: " + rmVo.Mobile + "<br/>Ph: +" + rmVo.OfficePhoneDirectStd + "-" + rmVo.OfficePhoneDirectNumber);

            }
            else
            {

                strMail.Append("Dear " + customerName + ",<br/>");
                strMail.Append("<br/>Please find attached " + subject + ".");
                if (!string.IsNullOrEmpty(advisorVo.Website))
                {
                    strMail.Append("<br/><br/>Regards,<br/>" + customerVo.RMName + "<br/>Mo: " + customerVo.RMMobile + "<br/>Ph: +" + customerVo.RMOfficePhone + "<br/>Website: " + advisorVo.Website);
                }
                else
                    strMail.Append("<br/><br/>Regards,<br/>" + customerVo.RMName + "<br/>Mo: " + customerVo.RMMobile + "<br/>Ph: +" + customerVo.RMOfficePhone);
            }

            return strMail.ToString();

        }

        /// <summary>
        /// Get the subject of the report while mailing.
        /// </summary>
        private string GetReportSubject(string reportType, DateTime start, DateTime end)
        {
            string subject = string.Empty;

            switch (CurrentReportType)
            {
                case ReportType.EquityReports:
                    subject = "Equity Report - ";
                    break;
                case ReportType.MFReports:
                    {
                        //subject = "MF Report - ";
                        switch (reportType)
                        {
                            case "CATEGORY_WISE":
                                subject = "Mutual Fund Summary Report - ";
                                break;
                            case "RETURNS_PORTFOLIO":
                                subject = "Portfolio Return-Holding ";
                                break;
                            case "COMPREHENSIVE":
                                subject = "Comprehensive Report - ";
                                break;
                            case "RETURNS_PORTFOLIO_REALIZED":
                                subject = "Portfolio Returns Realized - ";
                                break;
                            case "TRANSACTION_REPORT":
                                subject = "Transaction Report - ";
                                break;
                            case "DIVIDEND_STATEMENT":
                                subject = "Dividend Statement - ";
                                break;
                            case "DIVIDEND_SUMMARY":
                                subject = "Dividend Summary - ";
                                break;
                            case "CAPITAL_GAIN_SUMMARY":
                                subject = "Capital Gain Summary - ";
                                break;
                            case "CAPITAL_GAIN_DETAILS":
                                subject = "Capital Gain Details - ";
                                break;
                            case "ELIGIBLE_CAPITAL_GAIN_DETAILS":
                                subject = "Eligible Capital Gain Details - ";
                                break;
                            case "ELIGIBLE_CAPITAL_GAIN_SUMMARY":
                                subject = "Eligible Capital Gain Summary - ";
                                break;
                           
                        }
                    }
                    break;
                case ReportType.PortfolioReports:
                    subject = "Portfolio Report - ";
                    break;
                case ReportType.FinancialPlanning:
                    subject = "Financial Planning Report-";
                    break;
                case ReportType.FPOfflineForm:
                    subject = "FP Offline Form";
                    break;
            }


            //if (CurrentReportType == ReportType.EquityReports)
            //    subject = "Equity Report - ";
            //else if (CurrentReportType == ReportType.MFReports)
            //    subject = "MF Report - ";
            //else if (CurrentReportType == ReportType.PortfolioReports)
            //    subject = "Portfolio Report - ";
            //else if (CurrentReportType == ReportType.FinancialPlanning)
            //    subject = "Financial Planning Report";
            if (reportType != "FPOfflineForm")
            {
                if (start.CompareTo(end) == 0)
                    subject = subject + start.ToShortDateString();
                else
                    subject = subject + start.ToShortDateString() + " To " + end.ToShortDateString();
            }

            return subject;

        }

        /// <summary>
        /// Send the mail with report to receipient(s)
        /// </summary>
        private bool SendMail(string reportFileName)
        {
            Emailer emailer = new Emailer();
            EmailMessage email = new EmailMessage();
            bool isMailSent = false;
            RMVo rmVo = (RMVo)Session["rmVo"];
            string logoPath = "";

            string senderName = rmVo.FirstName + " " + rmVo.LastName;

            try
            {
                string[] toAddresses = hidTo.Value.Split(new char[] { ',' });
                foreach (string toEmail in toAddresses)
                    email.To.Add(toEmail);

                if (hidCC.Value != string.Empty)
                {
                    string[] ccEmailIds = hidCC.Value.Split(new char[] { ',' });
                    foreach (string CCEmail in ccEmailIds)
                        email.CC.Add(CCEmail);
                }
                if (hidCCMe.Value == "true")
                {
                    try
                    {
                        if (!String.IsNullOrEmpty(rmVo.Email))
                            email.Bcc.Add(new MailAddress(rmVo.Email));
                    }
                    catch (Exception ex)
                    {

                    }
                }
                //Assign SMTP Credentials if configured.

                AdviserStaffSMTPBo adviserStaffSMTPBo = new AdviserStaffSMTPBo();
                AdviserStaffSMTPVo adviserStaffSMTPVo = adviserStaffSMTPBo.GetSMTPCredentials(rmVo.RMId);

                if (adviserStaffSMTPVo.HostServer != null && adviserStaffSMTPVo.HostServer != string.Empty)
                {
                    emailer.isDefaultCredentials = !Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired);

                    if (!String.IsNullOrEmpty(adviserStaffSMTPVo.Password))
                        emailer.smtpPassword = Encryption.Decrypt(adviserStaffSMTPVo.Password);
                    emailer.smtpPort = int.Parse(adviserStaffSMTPVo.Port);
                    emailer.smtpServer = adviserStaffSMTPVo.HostServer;
                    emailer.smtpUserName = adviserStaffSMTPVo.Email;

                    if (Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired))
                    {
                        email.From = new MailAddress(emailer.smtpUserName, rmVo.Email);
                    }
                }

                Attachment attachment = new Attachment(reportFileName);
                email.Attachments.Add(attachment);
                email.Subject = hidSubject.Value;
                hidSubject.Value = string.Empty;
                email.IsBodyHtml = true;
                String MailBody = hidBody.Value.Replace("\n", "<br/>");

                System.Net.Mail.AlternateView htmlView;
                System.Net.Mail.AlternateView plainTextView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("Text view", null, "text/plain");
                //System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(hidBody.Value.Trim() + "<image src=cid:HDIImage>", null, "text/html");
                htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<html><body " + "style='font-family:Tahoma, Arial; font-size: 10pt;'><p>" + MailBody + "</p><img src='cid:HDIImage'></body></html>", null, "text/html");
                //Add image to HTML version
                if (Session["advisorVo"] != null)
                    logoPath = "~/Images/" + ((AdvisorVo)Session["advisorVo"]).LogoPath;
                //System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath("~/Images/") + @"\3DSYRW_4009.JPG", "image/jpeg");
                if (!File.Exists(Server.MapPath(logoPath)))
                    logoPath = "~/Images/" + "spacer.jpg";

                System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath(logoPath), "image/jpeg");

                imageResource.ContentId = "HDIImage";
                htmlView.LinkedResources.Add(imageResource);
                //Add two views to message.
                email.AlternateViews.Add(plainTextView);
                email.AlternateViews.Add(htmlView);
                //Send message
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                //smtpClient.Send(email);


                isMailSent = emailer.SendMail(email);

                EmailSMSBo emailSMSBo = new EmailSMSBo();
                EmailVo emailVo = new EmailVo();
                emailVo.AdviserId = advisorVo.advisorId;
                emailVo.AttachmentPath = reportFileName;
                if (Session["CusVo"] != null)
                    emailVo.CustomerId = ((CustomerVo)Session["CusVo"]).CustomerId;
                else
                    emailVo.CustomerId = 0;
                emailVo.EmailQueueId = 0;
                emailVo.EmailType = "Report";
                string[] FileNames = reportFileName.Split('\\');
                emailVo.FileName = FileNames[FileNames.Count() - 1];
                emailVo.HasAttachment = 1;
                emailVo.ReportCode = 0;
                emailVo.SentDate = DateTime.Today;
                emailVo.Status = 1;
                emailSMSBo.AddToEmailLog(emailVo);


            }
            catch (Exception ex)
            {

            }
            finally
            {
                email.Dispose();
            }
            return isMailSent;
        }

        /// <summary>
        /// Convert the report to selected format and save it to disk(Reports/TempReports folder).
        /// </summary>
        /// <returns></returns>
        private string ExportToDisk()
        {
            string fileExtension = string.Empty;
            ExportFormatType formatType = ExportFormatType.PortableDocFormat;

            RMVo rmVo = (RMVo)Session["rmVo"];

            if (hidFormat.Value == "PDF")
            {
                fileExtension = ".pdf";
                formatType = ExportFormatType.PortableDocFormat;
            }
            else if (hidFormat.Value == "Excel")
            {
                fileExtension = ".xls";
                formatType = ExportFormatType.Excel;
            }
            else if (hidFormat.Value == "Word")
            {
                fileExtension = ".doc";
                formatType = ExportFormatType.WordForWindows;
            }
            else if (hidFormat.Value == "RTF")
            {
                fileExtension = ".rtf";
                formatType = ExportFormatType.RichText;
            }

            string exportFileName = Server.MapPath("~/Reports/TempReports/") + CurrentReportType.ToString() + "_" + DateTime.Now.Ticks.ToString() + fileExtension;

            crmain.ExportToDisk(formatType, exportFileName);
            return exportFileName;


        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            //DisplayReport();
            string reportFileName = ExportToDisk();

            bool isMailSent = SendMail(reportFileName);
            divMessage.Visible = true;
            if (isMailSent)
            {
                lblEmailStatus.Text = "Email sent successfully";
                lblEmailStatus.CssClass = "SuccessMsg";
            }
            else
            {
                lblEmailStatus.Text = "An error occurred while sending mail.";
                lblEmailStatus.CssClass = "Error";
            }
        }

        #endregion

        /// <summary>
        /// This will be executed when user clicks on paging button.
        /// </summary>
        protected void CrystalReportViewer1_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)
        {
            //Request.QueryString["mail"] = "0";
            DisplayReport();
        }

        #region Logo Related Methods

        private void setLogo()
        {

            string advisorLogo = "spacer.png";
            if (advisorVo.LogoPath != null && advisorVo.LogoPath != string.Empty)
                advisorLogo = advisorVo.LogoPath;

            string logoPath = System.Web.HttpContext.Current.Request.MapPath("\\Images\\" + advisorLogo);
            if (!File.Exists(logoPath))
                advisorLogo = "spacer.png";

            crmain.Database.Tables["Images"].SetDataSource(ImageTable(System.Web.HttpContext.Current.Request.MapPath("\\Images\\" + advisorLogo)));



        }


        public static DataTable ImageTable(string ImageFile)
        {
            DataTable data = new DataTable();
            DataRow row;
            data.TableName = "Images";
            data.Columns.Add("Logo", System.Type.GetType("System.Byte[]"));
            row = data.NewRow();
            if (ImageFile != string.Empty)
            {
                FileStream fs = new FileStream(ImageFile, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);

                row[0] = br.ReadBytes((int)br.BaseStream.Length);
                data.Rows.Add(row);
                br = null;
                fs.Close();
                fs = null;
            }
            else
            {
                row[0] = new byte[] { 0 };
                data.Rows.Add(row);
            }


            return data;
        }
        #endregion


        public string CustomerDataFormatFormat(CustomerVo customerVo)
        {
            string strFullCustomerName;
            if (!string.IsNullOrEmpty(customerVo.Salutation.Trim()) && !string.IsNullOrEmpty(customerVo.FirstName.Trim()) && !string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && !string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName = customerVo.Salutation.Trim() + " " + customerVo.FirstName.Trim() + " " + customerVo.MiddleName.Trim() + " " + customerVo.LastName.Trim();

            }
            else if (string.IsNullOrEmpty(customerVo.Salutation.Trim()) && !string.IsNullOrEmpty(customerVo.FirstName.Trim()) && !string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && !string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName =customerVo.FirstName.Trim() + " " + customerVo.MiddleName.Trim() + " " + customerVo.LastName.Trim();

            }
            else if (!string.IsNullOrEmpty(customerVo.Salutation.Trim()) && !string.IsNullOrEmpty(customerVo.FirstName.Trim()) && !string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName = customerVo.Salutation.Trim() + " " + customerVo.FirstName.Trim() + " " + customerVo.MiddleName.Trim();
            }
            else if (string.IsNullOrEmpty(customerVo.Salutation.Trim()) && !string.IsNullOrEmpty(customerVo.FirstName.Trim()) && !string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName = customerVo.FirstName.Trim() + " " + customerVo.MiddleName.Trim();
            }
            else if (!string.IsNullOrEmpty(customerVo.Salutation.Trim()) && !string.IsNullOrEmpty(customerVo.FirstName.Trim()) && !string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName = customerVo.Salutation.Trim() + " " + customerVo.FirstName.Trim() + " " + customerVo.MiddleName.Trim();
            }
            else if (string.IsNullOrEmpty(customerVo.Salutation.Trim()) && !string.IsNullOrEmpty(customerVo.FirstName.Trim()) && !string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName =customerVo.FirstName.Trim() + " " + customerVo.MiddleName.Trim();
            }
            else if (!string.IsNullOrEmpty(customerVo.Salutation.Trim()) && !string.IsNullOrEmpty(customerVo.FirstName.Trim()) && string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && !string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName = customerVo.Salutation.Trim() + " " + customerVo.FirstName.Trim() + " " + customerVo.LastName.Trim();
            }
            else if (string.IsNullOrEmpty(customerVo.Salutation.Trim()) && !string.IsNullOrEmpty(customerVo.FirstName.Trim()) && string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && !string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName =  customerVo.FirstName.Trim() + " " + customerVo.LastName.Trim();
            }
            else if (!string.IsNullOrEmpty(customerVo.Salutation.Trim()) && string.IsNullOrEmpty(customerVo.FirstName.Trim()) && !string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && !string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName = customerVo.Salutation.Trim() + " " + customerVo.MiddleName.Trim() + " " + customerVo.LastName.Trim();
            }
            else if (string.IsNullOrEmpty(customerVo.Salutation.Trim()) && string.IsNullOrEmpty(customerVo.FirstName.Trim()) && !string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && !string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName = customerVo.MiddleName.Trim() + " " + customerVo.LastName.Trim();
            }
            else if (!string.IsNullOrEmpty(customerVo.Salutation.Trim()) && !string.IsNullOrEmpty(customerVo.FirstName.Trim()) && string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName = customerVo.Salutation.Trim() + " " + customerVo.FirstName.Trim();
            }
            else if (string.IsNullOrEmpty(customerVo.Salutation.Trim()) && !string.IsNullOrEmpty(customerVo.FirstName.Trim()) && string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName = customerVo.FirstName.Trim();
            }
            else if (!string.IsNullOrEmpty(customerVo.Salutation.Trim()) && string.IsNullOrEmpty(customerVo.FirstName.Trim()) && !string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName = customerVo.Salutation.Trim() + " " + customerVo.MiddleName.Trim();
            }
            else if (string.IsNullOrEmpty(customerVo.Salutation.Trim()) && string.IsNullOrEmpty(customerVo.FirstName.Trim()) && !string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName = customerVo.MiddleName.Trim();
            }
            else if (!string.IsNullOrEmpty(customerVo.Salutation.Trim()) && string.IsNullOrEmpty(customerVo.FirstName.Trim()) && string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && !string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName = customerVo.Salutation.Trim() + " " + customerVo.LastName.Trim();
            }
            else if (string.IsNullOrEmpty(customerVo.Salutation.Trim()) && string.IsNullOrEmpty(customerVo.FirstName.Trim()) && string.IsNullOrEmpty(customerVo.MiddleName.Trim()) && !string.IsNullOrEmpty(customerVo.LastName.Trim()))
            {
                strFullCustomerName = customerVo.LastName.Trim();
            }
            else
                strFullCustomerName = string.Empty;

            return strFullCustomerName;
        }

        public string RMDataFormatFormat(RMVo rmVo)
        {
            string strFullCustomerName;
            if (!string.IsNullOrEmpty(rmVo.FirstName.Trim()) && !string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && !string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName =  rmVo.FirstName.Trim() + " " + rmVo.MiddleName.Trim() + " " + rmVo.LastName.Trim();

            }
            else if (!string.IsNullOrEmpty(rmVo.FirstName.Trim()) && !string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && !string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName = rmVo.FirstName.Trim() + " " + rmVo.MiddleName.Trim() + " " + rmVo.LastName.Trim();

            }
            else if (!string.IsNullOrEmpty(rmVo.FirstName.Trim()) && !string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName = rmVo.FirstName.Trim() + " " + rmVo.MiddleName.Trim();
            }
            else if (!string.IsNullOrEmpty(rmVo.FirstName.Trim()) && !string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName = rmVo.FirstName.Trim() + " " + rmVo.MiddleName.Trim();
            }
            else if (!string.IsNullOrEmpty(rmVo.FirstName.Trim()) && !string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName = rmVo.FirstName.Trim() + " " + rmVo.MiddleName.Trim();
            }
            else if (!string.IsNullOrEmpty(rmVo.FirstName.Trim()) && !string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName = rmVo.FirstName.Trim() + " " + rmVo.MiddleName.Trim();
            }
            else if (!string.IsNullOrEmpty(rmVo.FirstName.Trim()) && string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && !string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName =  rmVo.FirstName.Trim() + " " + rmVo.LastName.Trim();
            }
            else if (!string.IsNullOrEmpty(rmVo.FirstName.Trim()) && string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && !string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName = rmVo.FirstName.Trim() + " " + rmVo.LastName.Trim();
            }
            else if (string.IsNullOrEmpty(rmVo.FirstName.Trim()) && !string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && !string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName = rmVo.MiddleName.Trim() + " " + rmVo.LastName.Trim();
            }
            else if (string.IsNullOrEmpty(rmVo.FirstName.Trim()) && !string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && !string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName = rmVo.MiddleName.Trim() + " " + rmVo.LastName.Trim();
            }
            else if (!string.IsNullOrEmpty(rmVo.FirstName.Trim()) && string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName = rmVo.FirstName.Trim();
            }
            else if (!string.IsNullOrEmpty(rmVo.FirstName.Trim()) && string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName = rmVo.FirstName.Trim();
            }
            else if (string.IsNullOrEmpty(rmVo.FirstName.Trim()) && !string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName = rmVo.MiddleName.Trim();
            }
            else if (string.IsNullOrEmpty(rmVo.FirstName.Trim()) && !string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName = rmVo.MiddleName.Trim();
            }
            else if (string.IsNullOrEmpty(rmVo.FirstName.Trim()) && string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && !string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName =  rmVo.LastName.Trim();
            }
            else if (string.IsNullOrEmpty(rmVo.FirstName.Trim()) && string.IsNullOrEmpty(rmVo.MiddleName.Trim()) && !string.IsNullOrEmpty(rmVo.LastName.Trim()))
            {
                strFullCustomerName = rmVo.LastName.Trim();
            }
            else
                strFullCustomerName = string.Empty;

            return strFullCustomerName;
        }

    }

}

