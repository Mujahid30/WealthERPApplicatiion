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

        MFReportVo mfReport = new MFReportVo();
        EquityReportVo equityReport = new EquityReportVo();
        PortfolioReportVo portfolioReport = new PortfolioReportVo();
        FinancialPlanningVo financialPlanning = new FinancialPlanningVo();
        AdvisorVo advisorVo = null;
        CustomerVo customerVo = new CustomerVo();

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
            if (Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$btnViewReport"] != null || Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlEmailReports$btnEmailReport"] != null)
            {
                CurrentReportType = ReportType.MFReports;
                if(Request.QueryString["Mail"] == "1")
                    ctrlPrefix = "ctrl_MFReports$tabViewAndEmailReports$tabpnlEmailReports$";
                else
                    ctrlPrefix = "ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$";
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
            
            if (PreviousPage != null)
            {
                GetReportParameters();

            }
            // if (!Page.IsPostBack)
            DisplayReport();

        }

        #region ReporDisplay Methods

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
            else
            {
                Response.Write("Invalid Report Type");
            }

            FillEmailValues();
        }

        private void DisplayReport(FinancialPlanningVo report)
        {
            try
            {
                FinancialPlanningReportsBo financialPlanningReportsBo = new FinancialPlanningReportsBo();
                report = (FinancialPlanningVo)Session["reportParams"];

                //switch (report.SubType)
                //{


                  //  case "FINANCIAL_PLANNING_REPORT":
                crmain.Load(Server.MapPath("FinancialPlanning.rpt"));
                        DataSet dsEquitySectorwise = financialPlanningReportsBo.GetFinancialPlanningReport(report);
                        DataTable dtEquitySectorwise = dsEquitySectorwise.Tables[0];
                        if (dsEquitySectorwise.Tables[1].Rows.Count > 0 ||  dsEquitySectorwise.Tables[0].Rows.Count > 0)
                        {
                            
                
                            crmain.Database.Tables["Goal"].SetDataSource(dsEquitySectorwise.Tables[0]);
                            crmain.Database.Tables["RiskProfile"].SetDataSource(dsEquitySectorwise.Tables[1]);
                            crmain.Database.Tables["FamilyDetails"].SetDataSource(dsEquitySectorwise.Tables[2]);
                            setLogo();
                            //crmain.Subreports[0].Database.Tables[0].SetDataSource(dsEquitySectorwise.Tables[2]);
                            //crmain.SetParameterValue("PreviousDate", DateBo.GetPreviousMonthLastDate(report.ToDate));
                            //crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();

                            //AssignReportViewerProperties();
                        }
                        else
                         SetNoRecords();
                    //    break;

                //}
            }
            catch (Exception ex)
            {
                throw (ex);
            }
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
                        crmain.Load(Server.MapPath("MultiAssetReport.rpt"));
                       
                        
                        DataSet dsEquitySectorwise = portfolioReports.GetPortfolioSummary(report, advisorVo.advisorId);
                        DataTable dtEquitySectorwise = dsEquitySectorwise.Tables[0];
                        if (dtEquitySectorwise.Rows.Count > 0)
                        {
                            crmain.Subreports[0].Database.Tables[0].SetDataSource(dsEquitySectorwise.Tables[1]);
                            crmain.Database.Tables["PortfolioSummary"].SetDataSource(dsEquitySectorwise.Tables[0]);
                            crmain.Subreports[1].Database.Tables[0].SetDataSource(dsEquitySectorwise.Tables[2]);

                            setLogo();
                            crmain.SetParameterValue("RMName", "Advisor :  " + rmVo.FirstName + " " + rmVo.LastName);
                            crmain.SetParameterValue("RMContactDetails", "Email :  " + rmVo.Email);
                            crmain.SetParameterValue("Organization", advisorVo.OrganizationName);
                            crmain.SetParameterValue("PreviousDate", DateBo.GetPreviousMonthLastDate(report.ToDate));
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                            //AssignReportViewerProperties();
                            CrystalReportViewer1.ReportSource = crmain;
                            CrystalReportViewer1.EnableDrillDown = true;
                            CrystalReportViewer1.HasCrystalLogo = false;
                            //AssignReportViewerProperties();
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
        /// Display Equity Reports
        /// </summary>
        /// <param name="report"></param>
        private void DisplayReport(EquityReportVo report)
        {
           
            try
            {
                EquityReportsBo equityReports = new EquityReportsBo();
                report = (EquityReportVo)Session["reportParams"];
              
                
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
                customerVo = (CustomerVo)Session["CusVo"];
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
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();
                            //crmain.ExportToDisk(ExportFormatType.PortableDocFormat,
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
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();
                        }
                        else
                            SetNoRecords();
                        break;


                    case "CATEGORY_WISE":
                        crmain.Load(Server.MapPath("MFFundSummary.rpt"));
                        
                        DataSet dsMFFundSummary = mfReports.GetMFFundSummaryReport(report, advisorVo.advisorId);
                        if (dsMFFundSummary.Tables.Count > 0)
                        {
                            crmain.Subreports["OpenPositionReport"].Database.Tables[0].SetDataSource(dsMFFundSummary.Tables[0]);
                            crmain.Subreports["AllPositionReport1"].Database.Tables[0].SetDataSource(dsMFFundSummary.Tables[1]);
                            crmain.Subreports["AllPositionReport2"].Database.Tables[0].SetDataSource(dsMFFundSummary.Tables[1]);
                            setLogo();                                       
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("PreviousMonthDate", DateBo.GetPreviousMonthLastDate(report.FromDate).ToShortDateString());
                            AssignReportViewerProperties();
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
                            //if (!String.IsNullOrEmpty(dtTransactions.Rows[0]["CustomerName"].ToString()))
                            // crmain.SetParameterValue("CustomerName", "Cust");
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());

                            AssignReportViewerProperties();
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
                            //if (!String.IsNullOrEmpty(dtDividend.Rows[0]["Name"].ToString()))
                            //crmain.SetParameterValue("CustomerName", "--");
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());

                            AssignReportViewerProperties();
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
                            //if (!String.IsNullOrEmpty(dtDividend.Rows[0]["Name"].ToString()))
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());

                            AssignReportViewerProperties();
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
                            //if (!String.IsNullOrEmpty(dtDividend.Rows[0]["Name"].ToString()))
                            //crmain.SetParameterValue("CustomerName", "--");
                            crmain.SetParameterValue("AsOnDate", report.FromDate.ToShortDateString());
                            AssignReportViewerProperties();
                        }
                        else
                            SetNoRecords();
                        break;

                    //case "RETURNS_TRANSACTION":
                    //    crmain.Load(Server.MapPath("MFReturnsTransactions.rpt"));
                    //    DataTable dtReturnsTransaction = mfReports.GetReturnTransactionSummaryReport(report);
                    //    if (dtReturnsTransaction.Rows.Count > 0)
                    //    {
                    //        crmain.SetDataSource(dtReturnsTransaction);
                    //        setLogo();
                    //        //if (!String.IsNullOrEmpty(dtDividend.Rows[0]["Name"].ToString()))
                    //        //crmain.SetParameterValue("CustomerName", "--");
                    //        crmain.SetParameterValue("AsOnDate", report.FromDate.ToShortDateString());
                    //        AssignReportViewerProperties();
                    //    }
                    //    else
                    //        SetNoRecords();
                    //    break;


                }
                //Filling Emails


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

            customerVo = (CustomerVo)Session["CusVo"];
            try
            {
                //setLogo();
                crmain.SetParameterValue("RMName", "Advisor :  " + rmVo.FirstName + " " + rmVo.LastName);
                crmain.SetParameterValue("RMContactDetails", "Email :  " + rmVo.Email);
                crmain.SetParameterValue("MobileNo", "Mobile :  " + rmVo.Mobile);

                crmain.SetParameterValue("OrgAddress", advisorVo.City + ", " + advisorVo.State);
                crmain.SetParameterValue("OrgDetails", advisorVo.Email + ", " + advisorVo.Website);
                crmain.SetParameterValue("OrgTelephone", "+91-" + advisorVo.Phone1Std + "-" + advisorVo.Phone1Number);
                crmain.SetParameterValue("Organization", advisorVo.OrganizationName);

                crmain.SetParameterValue("CustomerAddress", customerVo.Adr1Line1 + " " + customerVo.Adr1City);
                crmain.SetParameterValue("CustomerEmail", "Email :  " + customerVo.Email);
            }
            catch (Exception ex)
            {

            }

            CrystalReportViewer1.ReportSource = crmain;
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
                mfReport.PortfolioIds = GetPortfolios();
                //MF Transaction report Fiter Creiteria 
                mfReport.FilterBy= Request.Form[ctrlPrefix + "ddlMFTransactionType"];
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
            else if (CurrentReportType == ReportType.FinancialPlanning)
            {
                //portfolioReport.SubType = Request.Form[ctrlPrefix + "ddlReportSubType"];
                //portfolioReport.PortfolioIds = GetPortfolios();

                //portfolioReport.FromDate = dtFrom;
                //portfolioReport.ToDate = dtTo;





                financialPlanning.CustomerId = Request.Form[ctrlPrefix + "txtCustomerId"];

                    Session["reportParams"] = financialPlanning;
            }

        }

        /// <summary>
        /// Get all the selected portfolio Ids and return it as a comma separated string.
        /// </summary>
        private string GetPortfolios()
        {
            string portfolios = string.Empty;

            //foreach (string strForm in Request.Form.AllKeys)
            //{
            //    if (strForm.StartsWith("chk--"))
            //    {
            //        portfolios += strForm.Replace("chk--", "") + ",";
            //    }

            //}

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
            if (Request.Form["ctrl_MFReports$hidDateType"] == "DATE_RANGE")
            {

                fromDate = Convert.ToDateTime(Request.Form[ctrlPrefix + "txtFromDate"]);
                toDate = Convert.ToDateTime(Request.Form[ctrlPrefix + "txtToDate"]);
            }
            else if (Request.Form["ctrl_MFReports$hidDateType"] == "PERIOD")
            {

                dtBo.CalculateFromToDatesUsingPeriod(Request.Form[ctrlPrefix + "ddlPeriod"], out dtFrom, out dtTo);
                fromDate = dtFrom;
                toDate = dtTo;

            }
            else //if (Request.Form[ctrlPrefix + "hidDateType"] == "AS_ON")
            {
                fromDate = Convert.ToDateTime(Request.Form[ctrlPrefix + "txtAsOnDate"]);
                toDate = Convert.ToDateTime(Request.Form[ctrlPrefix + "txtAsOnDate"]);

            }
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
                else if (CurrentReportType == ReportType.FinancialPlanning)
                {
                    //subType = string.Empty;
                    //fromDate = DateTime.MinValue;
                    //toDate = DateTime.MinValue;
                    cust = customerBo.GetCustomer(Convert.ToInt32(financialPlanning.CustomerId));
                }
                Session["hidCC"] = txtCC.Text;
                Session["hidTo"] = txtTo.Text = cust.Email;
                Session["hidSubject"] = txtSubject.Text = GetReportSubject(subType, fromDate, toDate);
                if (cust.Salutation == string.Empty || cust.Salutation == "")
                {
                    Session["hidBody"] = txtBody.Text = GetReportBody(cust.FirstName + " " + cust.LastName, subType, fromDate, toDate).Replace("\r", "");
 
                }
                    Session["hidBody"] = txtBody.Text = GetReportBody(cust.Salutation + "." + " " +  cust.FirstName + " " + cust.LastName, subType, fromDate, toDate).Replace("\r","");

            }
            else
            {
                txtSubject.Text = Session["hidSubject"].ToString();
                txtCC.Text = Session["hidCC"].ToString();
                txtTo.Text = Session["hidTo"].ToString();
                txtBody.Text = Session["hidBody"].ToString().Replace("\r", "");
                //if (hidCCMe.Value == "true")
                chkCopy.Checked = true;
                //else
                //  chkCopy.Checked = false;
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

            strMail.Append("Dear " + customerName + ",<br/>");
            strMail.Append("<br/>Please find attached " + subject + ".");
            strMail.Append("<br/><br/>Regards,<br/>" + rmVo.FirstName + " " + rmVo.LastName + "<br/>Mo:" + rmVo.Mobile + "<br/>Ph:+" + rmVo.OfficePhoneExtStd + "-" + rmVo.OfficePhoneExtNumber);

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
                                subject = "Portfolio Returns - ";
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
                        }
                    }
                    break;
                case ReportType.PortfolioReports:
                    subject = "Portfolio Report - ";
                    break;
                case ReportType.FinancialPlanning:
                    subject = "Financial Planning Report";
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

            if (start.CompareTo(end) == 0)
                subject = subject + start.ToShortDateString();
            else
                subject = subject + start.ToShortDateString() + " To " + end.ToShortDateString();

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
                email.Body = hidBody.Value.Replace("\n", "<br/>");

                isMailSent = emailer.SendMail(email);

            }
            catch (Exception ex)
            {

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

    }
}

