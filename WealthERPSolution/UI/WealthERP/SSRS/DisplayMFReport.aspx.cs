using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoReports;
using VoUser;
using Microsoft.Reporting.WebForms;
using System.Data;
using BoReports;
using System.IO;
using System.Configuration;
using BoAdvisorProfiling;

namespace WealthERP.SSRS
{
    public partial class DisplayMFReport : System.Web.UI.Page
    {
        AdvisorVo advisorVo = null;
        RMVo rmVo = null;
        MFReportVo mfReport = new MFReportVo();
        string ReportFormat = string.Empty;
        string path = string.Empty;
        string PDFNames = string.Empty;
        private ReportType CurrentReportType
        {
            get
            {
                if (Session["ReportType"] == null)
                {
                    if (Request.Form["ctrl_EquityReports$btnView"] != null || Request.Form["ctrl_EquityReports$btnViewInPDF"] != null || Request.Form["ctrl_EquityReports$btnViewInDOC"] != null)
                    {
                        return ReportType.EquityReports;
                    }
                    else if (Request.Form["ctrl_MFReportUI$btnViewInPDF"] != null || Request.Form["ctrl_MFReportUI$btnViewInExcel"] != null || Request.Form["ctrl_MFReportUI$btnView"] != null)
                    {
                        return ReportType.MFReports;
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
            if (PreviousPage != null)
            {
                GetReportParameters();

            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["format"] == "pdf")
                ReportFormat = "pdf";
            else if (Request.QueryString["format"] == "xls")
                ReportFormat = "xls";
            else
                ReportFormat = "view";
            if (CurrentReportType == ReportType.MFReports)
            {
                DisplayReport(mfReport, ReportFormat);

            }

        }
        private void GetReportParameters()
        {
            if (CurrentReportType == ReportType.MFReports)
            {
                mfReport.SubType = Request.Form["ctrl_MFReportUI$TabContainer1$TabPanel1$ddlReportSubType"];
                mfReport.CustomerId = int.Parse(Request.Form["ctrl_MFReportUI$hdnCustomerId1"].ToString());
                mfReport.PortfolioIds = GetPortfolios();
                if (Request.Form["ctrl_MFReportUI$hidDateType"] == "DATE_RANGE")
                {
                    mfReport.FromDate = Convert.ToDateTime(Request.Form["ctrl_MFReportUI$TabContainer1$TabPanel1$txtFromDate"]);
                    mfReport.ToDate = Convert.ToDateTime(Request.Form["ctrl_MFReportUI$TabContainer1$TabPanel1$txtToDate"]);

                }
                else
                {

                    mfReport.ToDate = Convert.ToDateTime(Request.Form["ctrl_MFReportUI$TabContainer1$TabPanel1$txtAsOnDate"]);
                }


                if (!String.IsNullOrEmpty(Request.Form["ctrl_MFReportUI$hdnCustomerId1"]))
                {
                    mfReport.CustomerIds = Request.Form["ctrl_MFReportUI$hdnCustomerId1"];
                    mfReport.GroupHead = Request.Form["ctrl_MFReportUI$hdnCustomerId1"];

                }

                Session["reportParams"] = mfReport;
            }
        }
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
        private void DisplayReport(MFReportVo report, string ReportFormat)
        {
            MFReportEngineBo MFReportEngineBo = new MFReportEngineBo();
            DataSet dsReportHeaders = MFReportEngineBo.GetCustomerReportHeaders(advisorVo.advisorId, report.CustomerId);
            //ReportDataSource CustomerDetails = new ReportDataSource("MFReturns_CustomerDetails", dsReportHeaders.Tables[0]);
            //ReportDataSource AdviserDetails = new ReportDataSource("MFReturns_AdviserDetails", );
            ReportDataSource ReportHeader = new ReportDataSource("MFReturns_ReportHeader", AssignReportHeaderParameter(dsReportHeaders.Tables[0], dsReportHeaders.Tables[1], dsReportHeaders.Tables[2]));
            rptViewer.ProcessingMode = ProcessingMode.Local;
            rptViewer.Reset();
            LocalReport LocalReport = rptViewer.LocalReport;
            String reportLogoPath = ConfigurationManager.AppSettings["adviserLogoPath"].ToString();
            reportLogoPath = System.Web.HttpContext.Current.Request.MapPath(reportLogoPath + advisorVo.LogoPath);
            if (!File.Exists(reportLogoPath))
            {
                reportLogoPath = string.Empty;
                //reportLogoPath = ConfigurationManager.AppSettings[@"//Images//"].ToString();
                reportLogoPath = Server.MapPath(@"//Images//spacer.png");
            }
            reportLogoPath = "file:///" + reportLogoPath;
            string ReportName = string.Empty;

            switch (report.SubType)
            {


                case "PORTFOLIO_RETURNS_HOLDING":

                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\MF_Returns_Holding_PDF.rdlc");
                    DataSet dsMFReturnsHolding = MFReportEngineBo.CreateCustomerMFReturnsHolding(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource MFReturnsHolding = new ReportDataSource("MFReturns_MFReturn", dsMFReturnsHolding.Tables[0]);
                    ReportDataSource PortfolioXIRRHolding = new ReportDataSource("MFReturns_PortfolioXIRR", dsMFReturnsHolding.Tables[1]);
                    LocalReport.EnableExternalImages = true;
                    LocalReport.DataSources.Add(MFReturnsHolding);
                    LocalReport.DataSources.Add(ReportHeader);
                    //LocalReport.DataSources.Add(AdviserDetails);
                    LocalReport.DataSources.Add(PortfolioXIRRHolding);
                    ReportParameter[] paramHolding = new ReportParameter[2];
                    paramHolding[0] = new ReportParameter("AdviserLogoPath", reportLogoPath);
                    paramHolding[1] = new ReportParameter("ToDate", report.ToDate.ToString("dd-MMM-yy"));

                    LocalReport.SetParameters(paramHolding);
                    ReportName = "MF Returns - Holding " + DateTime.Now.ToString("yyyyMMddhhmmss");
                    if (ReportFormat == "pdf")
                    {
                        ExportInPDF();
                        Response.Redirect(@"\Reports\TempReports\ViewInPDF\"+PDFNames);
                        //Download(path, ReportName);
                    }
                    else if (ReportFormat == "xls")
                    {
                        ExportInExcel();
                        Download(path, ReportName);
                    }
                    else
                    {
                        LocalReport.DisplayName = ReportName;
                        rptViewer.LocalReport.Refresh();
                        rptViewer.ZoomPercent = 110;
                        rptViewer.ShowPageNavigationControls = false;
                      

                    }
                    break;
                case "COMPREHENSIVE_MF_REPORT":

                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\MF_Comprehensive_PDF.rdlc");
                    DataSet dsMFComprehensive = MFReportEngineBo.CreateCustomerMFComprehensive(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource MFComprehensive = new ReportDataSource("MFReturns_MFReturn", dsMFComprehensive.Tables[0]);
                    ReportDataSource PortfolioXIRRComprehensive = new ReportDataSource("MFReturns_PortfolioXIRR", dsMFComprehensive.Tables[1]);
                    LocalReport.EnableExternalImages = true;
                    LocalReport.DataSources.Add(MFComprehensive);
                    LocalReport.DataSources.Add(ReportHeader);
                    //LocalReport.DataSources.Add(AdviserDetails);
                    LocalReport.DataSources.Add(PortfolioXIRRComprehensive);
                    ReportParameter[] paramComprehensive = new ReportParameter[2];
                    paramComprehensive[0] = new ReportParameter("AdviserLogoPath", reportLogoPath);
                    paramComprehensive[1] = new ReportParameter("ToDate", report.ToDate.ToString("dd-MMM-yy"));

                    LocalReport.SetParameters(paramComprehensive);
                    ReportName = "MF Comprehensive " + DateTime.Now.ToString("yyyyMMddhhmmss");
                    if (ReportFormat == "pdf")
                    {
                        ExportInPDF();
                        Response.Redirect(@"\Reports\TempReports\ViewInPDF\" + PDFNames);
                        //Download(path, ReportName);
                    }
                    else if (ReportFormat == "xls")
                    {
                        ExportInExcel();
                        Download(path, ReportName);
                    }
                    else
                    {
                        LocalReport.DisplayName = ReportName;
                        LocalReport.Refresh();
                        rptViewer.ZoomPercent = 110;
                        rptViewer.ShowPageNavigationControls = false;
                    }
                    break;
                case "CAPITAL_GAIN_DETAILS":
                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\MF_Capital_Gain_Details_PDF.rdlc");
                    DataTable dtCapitalGainDetails = MFReportEngineBo.CreateCustomerCapitalGainDetails(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource MFCapitalGainDetails = new ReportDataSource("MFReturns_MFCapitalGain", dtCapitalGainDetails);
                    LocalReport.EnableExternalImages = true;
                    LocalReport.DataSources.Add(MFCapitalGainDetails);
                    LocalReport.DataSources.Add(ReportHeader);
                    //LocalReport.DataSources.Add(AdviserDetails);
                    ReportParameter[] paramCapitalGainDetail = new ReportParameter[3];
                    paramCapitalGainDetail[0] = new ReportParameter("AdviserLogoPath", reportLogoPath);
                    paramCapitalGainDetail[1] = new ReportParameter("ToDate", report.ToDate.ToString("dd-MMM-yy"));
                    paramCapitalGainDetail[2] = new ReportParameter("FromDate", report.FromDate.ToString("dd-MMM-yy"));

                    LocalReport.SetParameters(paramCapitalGainDetail);
                    ReportName = "MF Capital Gain Details " + DateTime.Now.ToString("yyyyMMddhhmmss");
                    if (ReportFormat == "pdf")
                    {
                        ExportInPDF();
                        Response.Redirect(@"\Reports\TempReports\ViewInPDF\" + PDFNames);
                        //Download(path, ReportName);
                    }
                    else if (ReportFormat == "xls")
                    {
                        ExportInExcel();
                        Download(path, ReportName);
                    }
                    else
                    {
                        LocalReport.DisplayName = ReportName;
                        rptViewer.LocalReport.Refresh();
                        rptViewer.ZoomPercent = 110;
                        rptViewer.ShowPageNavigationControls = false;
                    }
                    break;
                case "CAPITAL_GAIN_SUMMARY":
                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\MF_Capital_Gain_Summary_PDF.rdlc");
                    DataTable dtCapitalGainSummary = MFReportEngineBo.CreateCustomerCapitalGainSummary(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource MFCapitalGainSummary = new ReportDataSource("MFReturns_MFCapitalGain", dtCapitalGainSummary);
                    LocalReport.EnableExternalImages = true;
                    LocalReport.DataSources.Add(MFCapitalGainSummary);
                    LocalReport.DataSources.Add(ReportHeader);
                    //LocalReport.DataSources.Add(AdviserDetails);
                    ReportParameter[] paramCapitalGainSummary = new ReportParameter[3];
                    paramCapitalGainSummary[0] = new ReportParameter("AdviserLogoPath", reportLogoPath);
                    paramCapitalGainSummary[1] = new ReportParameter("ToDate", report.ToDate.ToString("dd-MMM-yy"));
                    paramCapitalGainSummary[2] = new ReportParameter("FromDate", report.FromDate.ToString("dd-MMM-yy"));
                    LocalReport.SetParameters(paramCapitalGainSummary);
                    ReportName = "MF Capital Gain Summary " + DateTime.Now.ToString("yyyyMMddhhmmss");
                    if (ReportFormat == "pdf")
                    {
                        ExportInPDF();
                        Response.Redirect(@"\Reports\TempReports\ViewInPDF\" + PDFNames);
                        //Download(path, ReportName);
                    }
                    else if (ReportFormat == "xls")
                    {
                        ExportInExcel();
                        Download(path, ReportName);
                    }
                    else
                    {
                        LocalReport.DisplayName = ReportName;
                        rptViewer.LocalReport.Refresh();
                        rptViewer.ZoomPercent = 110;
                        rptViewer.ShowPageNavigationControls = false;
                    }
                    break;
                case "ELIGIBLE_CAPITAL_GAIN_DETAILS":
                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\MF_Capital_Gain_Details_PDF.rdlc");
                    DataTable dtEligibleCapitalGainDetails = MFReportEngineBo.CreateCustomerEligibleCapitalGainDetails(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource MFEligibleCapitalGainDetails = new ReportDataSource("MFReturns_MFCapitalGain", dtEligibleCapitalGainDetails);
                    LocalReport.EnableExternalImages = true;
                    LocalReport.DataSources.Add(MFEligibleCapitalGainDetails);
                    LocalReport.DataSources.Add(ReportHeader);
                    //LocalReport.DataSources.Add(AdviserDetails);
                    ReportParameter[] paramEligibleCapitalGainDetail = new ReportParameter[4];
                    paramEligibleCapitalGainDetail[0] = new ReportParameter("AdviserLogoPath", reportLogoPath);
                    paramEligibleCapitalGainDetail[1] = new ReportParameter("ToDate", report.ToDate.ToString("dd-MMM-yy"));
                    paramEligibleCapitalGainDetail[2] = new ReportParameter("ReportName", "Eligible");
                    paramEligibleCapitalGainDetail[3] = new ReportParameter("FromDate", report.FromDate.ToString("dd-MMM-yy"));

                    LocalReport.SetParameters(paramEligibleCapitalGainDetail);
                    ReportName = "MF Eligible Capital Gain Details " + DateTime.Now.ToString("yyyyMMddhhmmss");
                    if (ReportFormat == "pdf")
                    {
                        ExportInPDF();
                        Response.Redirect(@"\Reports\TempReports\ViewInPDF\" + PDFNames);
                        //Download(path, ReportName);
                    }
                    else if (ReportFormat == "xls")
                    {
                        ExportInExcel();
                        Download(path, ReportName);
                    }
                    else
                    {
                        LocalReport.DisplayName = ReportName;
                        rptViewer.LocalReport.Refresh();
                        rptViewer.ZoomPercent = 110;
                        rptViewer.ShowPageNavigationControls = false;
                    }
                    break;
                case "ELIGIBLE_CAPITAL_GAIN_SUMMARY":
                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\MF_Capital_Gain_Summary_PDF.rdlc");
                    DataTable dtEligibleCapitalGainSummary = MFReportEngineBo.CreateCustomerEligibleCapitalGainSummary(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource MFEligibleCapitalGainSummary = new ReportDataSource("MFReturns_MFCapitalGain", dtEligibleCapitalGainSummary);
                    LocalReport.EnableExternalImages = true;
                    LocalReport.DataSources.Add(MFEligibleCapitalGainSummary);
                    LocalReport.DataSources.Add(ReportHeader);
                    //LocalReport.DataSources.Add(AdviserDetails);
                    ReportParameter[] paramEligibleCapitalGainSummary = new ReportParameter[4];
                    paramEligibleCapitalGainSummary[0] = new ReportParameter("AdviserLogoPath", reportLogoPath);
                    paramEligibleCapitalGainSummary[1] = new ReportParameter("ToDate", report.ToDate.ToString("dd-MMM-yy"));
                    paramEligibleCapitalGainSummary[2] = new ReportParameter("ReportName", "Eligible");
                    paramEligibleCapitalGainSummary[3] = new ReportParameter("FromDate", report.FromDate.ToString("dd-MMM-yy"));
                    LocalReport.SetParameters(paramEligibleCapitalGainSummary);
                    ReportName = "MF Eligible Capital Gain Summary " + DateTime.Now.ToString("yyyyMMddhhmmss");
                    if (ReportFormat == "pdf")
                    {
                        ExportInPDF();
                        Response.Redirect(@"\Reports\TempReports\ViewInPDF\" + PDFNames);
                        //Download(path, ReportName);
                    }
                    else if (ReportFormat == "xls")
                    {
                        ExportInExcel();
                        Download(path, ReportName);
                    }
                    else
                    {
                        LocalReport.DisplayName = ReportName;
                        rptViewer.LocalReport.Refresh();
                        rptViewer.ZoomPercent = 110;
                        rptViewer.ShowPageNavigationControls = false;
                    }
                    break;
                case "MF_TRANSACTION":
                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\MF_Transaction_PDF.rdlc");
                    DataTable dtTransaction = MFReportEngineBo.CreateCustomerMFTraxnReport(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource MFTransaction = new ReportDataSource("MFReturns_MFTraxn", dtTransaction);
                    LocalReport.EnableExternalImages = true;
                    LocalReport.DataSources.Add(MFTransaction);
                    LocalReport.DataSources.Add(ReportHeader);
                    ReportParameter[] paramTraxn = new ReportParameter[3];
                    paramTraxn[0] = new ReportParameter("AdviserLogoPath", reportLogoPath);
                    paramTraxn[1] = new ReportParameter("FromDate", report.FromDate.ToString("dd-MMM-yy"));
                    paramTraxn[2] = new ReportParameter("ToDate", report.ToDate.ToString("dd-MMM-yy"));

                    LocalReport.SetParameters(paramTraxn);
                    ReportName = "MF Transaction " + DateTime.Now.ToString("yyyyMMddhhmmss");
                    if (ReportFormat == "pdf")
                    {
                        ExportInPDF();
                        Response.Redirect(@"\Reports\TempReports\ViewInPDF\" + PDFNames);
                        //Download(path, ReportName);
                    }
                    else if (ReportFormat == "xls")
                    {
                        ExportInExcel();
                        Download(path, ReportName);
                    }
                    else
                    {
                        LocalReport.DisplayName = ReportName;
                        rptViewer.LocalReport.Refresh();
                        rptViewer.ZoomPercent = 110;
                        rptViewer.ShowPageNavigationControls = false;

                    }
                    break;
                case "PORTFOLIO_RETURNS_WITHBANKDETAILS":

                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\MF_Returns_Holding_WithBankDetails_PDF.rdlc");
                    DataSet dsMFReturnsHoldingCustomRpt = MFReportEngineBo.CreateCustomerMFReturnsHoldingWithBankDetails(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource MFReturnsHoldingCustomRpt = new ReportDataSource("MFReturns_MFReturn", dsMFReturnsHoldingCustomRpt.Tables[0]);
                    ReportDataSource PortfolioXIRR = new ReportDataSource("MFReturns_PortfolioXIRR", dsMFReturnsHoldingCustomRpt.Tables[1]);
                    LocalReport.EnableExternalImages = true;
                    LocalReport.DataSources.Add(MFReturnsHoldingCustomRpt);
                    LocalReport.DataSources.Add(ReportHeader);
                    //LocalReport.DataSources.Add(AdviserDetails);
                    LocalReport.DataSources.Add(PortfolioXIRR);
                    ReportParameter[] paramHoldingCustomRpt = new ReportParameter[2];
                    paramHoldingCustomRpt[0] = new ReportParameter("AdviserLogoPath", reportLogoPath);
                    paramHoldingCustomRpt[1] = new ReportParameter("ToDate", report.ToDate.ToString("dd-MMM-yy"));

                    LocalReport.SetParameters(paramHoldingCustomRpt);
                    ReportName = "MF Returns - Holding " + DateTime.Now.ToString("yyyyMMddhhmmss");
                    if (ReportFormat == "pdf")
                    {
                        ExportInPDF();
                        Response.Redirect(@"\Reports\TempReports\ViewInPDF\" + PDFNames);
                        //Download(path, ReportName);
                    }
                    else if (ReportFormat == "xls")
                    {
                        ExportInExcel();
                        Download(path, ReportName);
                    }
                    else
                    {
                        LocalReport.DisplayName = ReportName;
                        rptViewer.LocalReport.Refresh();
                        rptViewer.ZoomPercent = 110;
                        rptViewer.ShowPageNavigationControls = false;
                    } 
                    break;
                case "MF_CLOSINGBALANCE":

                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\MF_ClosingBalance_PDF.rdlc");
                    DataTable dtClosingBalance = MFReportEngineBo.CreateCustomerClosingBalance(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource MFClosingBalanceRpt = new ReportDataSource("MFReturns_MFClosingBalance", dtClosingBalance);
                    LocalReport.EnableExternalImages = true;
                    LocalReport.DataSources.Add(MFClosingBalanceRpt);
                    LocalReport.DataSources.Add(ReportHeader);
                    ReportParameter[] paramClosingBalance = new ReportParameter[3];
                    paramClosingBalance[0] = new ReportParameter("AdviserLogoPath", reportLogoPath);
                    paramClosingBalance[1] = new ReportParameter("FromDate", report.FromDate.ToString("dd-MMM-yy"));
                    paramClosingBalance[2] = new ReportParameter("ToDate", report.ToDate.ToString("dd-MMM-yy"));
                    LocalReport.SetParameters(paramClosingBalance);
                    ReportName = "MF Closing Balance " + DateTime.Now.ToString("yyyyMMddhhmmss");
                    if (ReportFormat == "pdf")
                    {
                        ExportInPDF();
                        Response.Redirect(@"\Reports\TempReports\ViewInPDF\" + PDFNames);
                        //Download(path, ReportName);
                    }
                    else if (ReportFormat == "xls")
                    {
                        ExportInExcel();
                        Download(path, ReportName);
                    }
                    else
                    {
                        LocalReport.DisplayName = ReportName;
                        rptViewer.LocalReport.Refresh();
                        rptViewer.ZoomPercent = 110;
                        rptViewer.ShowPageNavigationControls = false;
                    } 
                    break;
                case "MF_TranxnHolding":

                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\MF_Holding_Transactions_PDF.rdlc");
                    DataSet dsHoldingTranxn = MFReportEngineBo.CreateCustomerTranxnHolding(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource MFHoldingTranxnRpt = new ReportDataSource("MFReturns_MFReturn", dsHoldingTranxn.Tables[0]);
                    ReportDataSource MFHoldingXIRR = new ReportDataSource("MFReturns_MFHoldingXIRR", dsHoldingTranxn.Tables[1]);
                    LocalReport.EnableExternalImages = true;
                    LocalReport.DataSources.Add(MFHoldingTranxnRpt);
                    LocalReport.DataSources.Add(MFHoldingXIRR);
                    LocalReport.DataSources.Add(ReportHeader);
                    ReportParameter[] paramTranxnHolding = new ReportParameter[2];
                    paramTranxnHolding[0] = new ReportParameter("AdviserLogoPath", reportLogoPath);
                    paramTranxnHolding[1] = new ReportParameter("ToDate", report.ToDate.ToString("dd-MMM-yy"));
                    LocalReport.SetParameters(paramTranxnHolding);
                    ReportName = "MF Holding Transactions " + DateTime.Now.ToString("yyyyMMddhhmmss");
                    if (ReportFormat == "pdf")
                    {
                        ExportInPDF();
                        Response.Redirect(@"\Reports\TempReports\ViewInPDF\" + PDFNames);
                        //Download(path, ReportName);
                    }
                    else if (ReportFormat == "xls")
                    {
                        ExportInExcel();
                        Download(path, ReportName);
                    }
                    else
                    {
                        LocalReport.DisplayName = ReportName;
                        rptViewer.LocalReport.Refresh();
                        rptViewer.ZoomPercent = 110;
                        rptViewer.ShowPageNavigationControls = false;
                    }
                    break;
                    
            }

        }
        private DataTable AssignReportHeaderParameter(DataTable dt1, DataTable dt2, DataTable dt3)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Header1");
            dt.Columns.Add("Header2");
            dt.Columns.Add("Header3");
            dt.Columns.Add("Header4");
            for (int i = 0; i < 5; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Header1"] = dt1.Columns[i].ColumnName;
                dr["Header2"] = dt1.Rows[0][i].ToString();
                string[] reportHeaderLineText = dt2.Rows[0][i].ToString().Split('~');
                switch (reportHeaderLineText[1].Trim())
                {
                    case "ONAME":
                        dr["Header3"] = reportHeaderLineText[0].ToString();
                        dr["Header4"] = advisorVo.OrganizationName;
                        break;
                    case "CRMNAME":
                        dr["Header3"] = reportHeaderLineText[0].ToString();
                        dr["Header4"] = dt3.Rows[0]["AR_FirstName"].ToString();
                        break;
                    case "ANAME":
                        dr["Header3"] = reportHeaderLineText[0].ToString();
                        dr["Header4"] = advisorVo.FirstName + " " + advisorVo.MiddleName + " " + advisorVo.LastName;
                        break;
                    case "CRME":
                        dr["Header3"] = reportHeaderLineText[0].ToString();
                        dr["Header4"] = dt3.Rows[0]["AR_Email"].ToString();
                        break;
                    case "AEMAIL":
                        dr["Header3"] = reportHeaderLineText[0].ToString();
                        dr["Header4"] = advisorVo.Email;
                        break;
                    case "AWEB":
                        dr["Header3"] = reportHeaderLineText[0].ToString();
                        dr["Header4"] = advisorVo.Website;
                        break;
                    case "AMOB":
                        dr["Header3"] = reportHeaderLineText[0].ToString();
                        dr["Header4"] = "+91-" + advisorVo.MobileNumber.ToString();
                        break;
                    case "CRMMOB":
                        dr["Header3"] = reportHeaderLineText[0].ToString();
                        dr["Header4"] = dt3.Rows[0]["AR_Mobile"].ToString();
                        break;
                }
                dt.Rows.Add(dr);

            }

            return dt;
        }
        private void ExportInPDF()
        {
            string fileExtension = ".pdf";
            string exportFilename = string.Empty;
            System.Guid guid = System.Guid.NewGuid();
            PDFNames = CurrentReportType.ToString() + "_" + guid + fileExtension;
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            byte[] bytes = rptViewer.LocalReport.Render(
            "PDF", null, out mimeType, out encoding, out filenameExtension,
            out streamids, out warnings);
            path = Server.MapPath("~/Reports/TempReports/ViewInPDF/") + PDFNames;
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }

        }
        private void ExportInExcel()
        {
            string fileExtension = ".xls";
            string exportFilename = string.Empty;
            System.Guid guid = System.Guid.NewGuid();
            PDFNames = CurrentReportType.ToString() + "_" + guid + fileExtension;
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            byte[] bytes = rptViewer.LocalReport.Render(
            "Excel", null, out mimeType, out encoding, out filenameExtension,
            out streamids, out warnings);
            path = Server.MapPath("~/Reports/TempReports/ViewInPDF/") + PDFNames;
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }

        }
        public void Download(string path, string Name)
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Name + "." + ReportFormat);
            Response.TransmitFile(path);
            Response.End();
        }
    }
}
