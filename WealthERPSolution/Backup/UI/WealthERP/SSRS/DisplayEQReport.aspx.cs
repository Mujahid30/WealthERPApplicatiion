using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoReports;
using BoReports;
using Microsoft.Reporting.WebForms;
using BoAdvisorProfiling;
using VoUser;
using System.Data;
using System.IO;
using System.Configuration;

namespace WealthERP.SSRS
{
    public partial class DisplayEQReport : System.Web.UI.Page
    {

        AdvisorVo advisorVo = null;
        RMVo rmVo = null;
        MFReportVo eqReport = new MFReportVo();
        string ReportFormat = string.Empty;
        string path = string.Empty;
        string PDFNames = string.Empty;
        private ReportType CurrentReportType
        {
            get
            {
                if (Session["ReportType"] == null)
                {
                     if (Request.Form["ctrl_EQReportUI$btnViewInPDF"] != null || Request.Form["ctrl_EQReportUI$btnViewInExcel"] != null || Request.Form["ctrl_EQReportUI$btnView"] != null)
                    {
                        return ReportType.EquityReports;
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
            if (CurrentReportType == ReportType.EquityReports)
            {
                DisplayReport(eqReport, ReportFormat);

            }

        }
        private void GetReportParameters()
        {
            if (CurrentReportType == ReportType.EquityReports)
            {
                eqReport.SubType = Request.Form["ctrl_EQReportUI$TabContainer1$TabPanel1$ddlReportSubType"];
                eqReport.CustomerId = int.Parse(Request.Form["ctrl_EQReportUI$hdnCustomerId1"].ToString());
                eqReport.PortfolioIds = GetPortfolios();
                if (Request.Form["ctrl_EQReportUI$hidDateType"] == "DATE_RANGE")
                {
                    eqReport.FromDate = Convert.ToDateTime(Request.Form["ctrl_EQReportUI$TabContainer1$TabPanel1$txtFromDate"]);
                    eqReport.ToDate = Convert.ToDateTime(Request.Form["ctrl_EQReportUI$TabContainer1$TabPanel1$txtToDate"]);

                }
                else
                {
                    eqReport.ToDate = Convert.ToDateTime(Request.Form["ctrl_EQReportUI$TabContainer1$TabPanel1$txtAsOnDate"]);
                }
                //if (!String.IsNullOrEmpty(Request.Form["ctrl_MFReports$hdnCustomerId1"]))
                //{
                //    mfReport.CustomerIds = Request.Form["ctrl_MFReports$hdnCustomerId1"];
                //    mfReport.GroupHead = Request.Form["ctrl_MFReports$hdnCustomerId1"];

                //}

                Session["reportParams"] = eqReport;
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
            EQReportEngineBo EQReportEngineBo = new EQReportEngineBo();               
            DataSet dsReportHeaders = EQReportEngineBo.GetCustomerReportHeaders(advisorVo.advisorId, report.CustomerId);
            ReportDataSource ReportHeader = new ReportDataSource("EQDataSet_ReportHeader", AssignReportHeaderParameter(dsReportHeaders.Tables[0], dsReportHeaders.Tables[1], dsReportHeaders.Tables[2]));
            rptViewer.ProcessingMode = ProcessingMode.Local;
            rptViewer.Reset();
            LocalReport LocalReport = rptViewer.LocalReport;
            String reportLogoPath = ConfigurationManager.AppSettings["adviserLogoPath"].ToString();
            reportLogoPath = System.Web.HttpContext.Current.Request.MapPath(reportLogoPath + advisorVo.LogoPath);
            if (!File.Exists(reportLogoPath))
            {
                reportLogoPath = string.Empty;
                //reportLogoPath = ConfigurationManager.AppSettings["adviserLogoPath"].ToString();
                //reportLogoPath = System.Web.HttpContext.Current.Request.MapPath(reportLogoPath + "spacer.png");
                reportLogoPath = Server.MapPath(@"//Images//spacer.png");
            }
            reportLogoPath = "file:///" + reportLogoPath;
            string ReportName = string.Empty;

            switch (report.SubType)
            {


                case "EQ_HOLDING_REPORT":

                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\EQ_Holding_PDF.rdlc");
                    DataSet dsEQHolding = EQReportEngineBo.CreateCustomerEQReturnsHolding(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource EQHolding = new ReportDataSource("EQDataSet_EQHolding", dsEQHolding.Tables[0]);
                    ReportDataSource PortfolioXIRRHolding = new ReportDataSource("EQDataSet_PortfolioXIRR", dsEQHolding.Tables[1]);
                    LocalReport.EnableExternalImages = true;
                    LocalReport.DataSources.Add(EQHolding);
                    LocalReport.DataSources.Add(ReportHeader);
                    //LocalReport.DataSources.Add(AdviserDetails);
                    LocalReport.DataSources.Add(PortfolioXIRRHolding);
                    ReportParameter[] paramHolding = new ReportParameter[2];
                    paramHolding[0] = new ReportParameter("AdviserLogoPath", reportLogoPath);
                    paramHolding[1] = new ReportParameter("ToDate", report.ToDate.ToString("dd-MMM-yy"));

                    LocalReport.SetParameters(paramHolding);
                    ReportName = "Equity Holding Report " + DateTime.Now.ToString("yyyyMMddhhmmss");
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
                case "EQ_TRANSACTION":
                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\EQ_Transaction_PDF.rdlc");
                    DataTable dtTransaction = EQReportEngineBo.CreateCustomerEQTraxnReport(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource EQTransaction = new ReportDataSource("EQDataSet_EQTranxn", dtTransaction);
                    LocalReport.EnableExternalImages = true;
                    LocalReport.DataSources.Add(EQTransaction);
                    LocalReport.DataSources.Add(ReportHeader);
                    ReportParameter[] paramTraxn = new ReportParameter[3];
                    paramTraxn[0] = new ReportParameter("AdviserLogoPath", reportLogoPath);
                    paramTraxn[1] = new ReportParameter("FromDate", report.FromDate.ToString("dd-MMM-yy"));
                    paramTraxn[2] = new ReportParameter("ToDate", report.ToDate.ToString("dd-MMM-yy"));

                    LocalReport.SetParameters(paramTraxn);
                    ReportName = "Equity Transaction Report " + DateTime.Now.ToString("yyyyMMddhhmmss");
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
                case "CAPITAL_GAIN_DETAILS":
                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\EQ_Capital_Gain_Details_PDF.rdlc");
                    DataTable dtCapitalGainDetails = EQReportEngineBo.CreateCustomerCapitalGainDetails(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource EQCapitalGainDetails = new ReportDataSource("EQDataSet_EQCapitalGain", dtCapitalGainDetails);
                    LocalReport.EnableExternalImages = true;
                    LocalReport.DataSources.Add(EQCapitalGainDetails);
                    LocalReport.DataSources.Add(ReportHeader);
                    //LocalReport.DataSources.Add(AdviserDetails);
                    ReportParameter[] paramCapitalGainDetail = new ReportParameter[3];
                    paramCapitalGainDetail[0] = new ReportParameter("AdviserLogoPath", reportLogoPath);
                    paramCapitalGainDetail[1] = new ReportParameter("ToDate", report.ToDate.ToString("dd-MMM-yy"));
                    paramCapitalGainDetail[2] = new ReportParameter("FromDate", report.FromDate.ToString("dd-MMM-yy"));

                    LocalReport.SetParameters(paramCapitalGainDetail);
                    ReportName = "EQ Capital Gain Details " + DateTime.Now.ToString("yyyyMMddhhmmss");
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
                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\EQ_Capital_Gain_Summary_PDF.rdlc");
                    DataTable dtCapitalGainSummary = EQReportEngineBo.CreateCustomerCapitalGainSummary(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource EQCapitalGainSummary = new ReportDataSource("EQDataSet_EQCapitalGain", dtCapitalGainSummary);
                    LocalReport.EnableExternalImages = true;
                    LocalReport.DataSources.Add(EQCapitalGainSummary);
                    LocalReport.DataSources.Add(ReportHeader);
                    //LocalReport.DataSources.Add(AdviserDetails);
                    ReportParameter[] paramCapitalGainSummary = new ReportParameter[3];
                    paramCapitalGainSummary[0] = new ReportParameter("AdviserLogoPath", reportLogoPath);
                    paramCapitalGainSummary[1] = new ReportParameter("ToDate", report.ToDate.ToString("dd-MMM-yy"));
                    paramCapitalGainSummary[2] = new ReportParameter("FromDate", report.FromDate.ToString("dd-MMM-yy"));
                    LocalReport.SetParameters(paramCapitalGainSummary);
                    ReportName = "EQ Capital Gain Summary " + DateTime.Now.ToString("yyyyMMddhhmmss");
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
                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\EQ_Capital_Gain_Details_PDF.rdlc");
                    DataTable dtEligibleCapitalGainDetails = EQReportEngineBo.CreateCustomerEligibleCapitalGainDetails(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource MFEligibleCapitalGainDetails = new ReportDataSource("EQDataSet_EQCapitalGain", dtEligibleCapitalGainDetails);
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
                    ReportName = "EQ Eligible Capital Gain Details " + DateTime.Now.ToString("yyyyMMddhhmmss");
                    if (ReportFormat == "pdf")
                    {
                        ExportInPDF();
                        //Response.Redirect(@"\Reports\TempReports\ViewInPDF\" + PDFNames);
                        Download(path, ReportName);
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
                    LocalReport.ReportPath = Server.MapPath(@"SsrsReportingFiles\EQ_Capital_Gain_Summary_PDF.rdlc");
                    DataTable dtEligibleCapitalGainSummary = EQReportEngineBo.CreateCustomerEligibleCapitalGainSummary(report.CustomerId, report.PortfolioIds, report.FromDate, report.ToDate);
                    ReportDataSource MFEligibleCapitalGainSummary = new ReportDataSource("EQDataSet_EQCapitalGain", dtEligibleCapitalGainSummary);
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
                    ReportName = "EQ Eligible Capital Gain Summary " + DateTime.Now.ToString("yyyyMMddhhmmss");
                    if (ReportFormat == "pdf")
                    {
                        ExportInPDF();
                        //Response.Redirect(@"\Reports\TempReports\ViewInPDF\" + PDFNames);
                        Download(path, ReportName);
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
