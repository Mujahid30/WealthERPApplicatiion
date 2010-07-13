using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoReports;
using BoCommon;
using VoUser;
using CrystalDecisions.Shared;
using System.Net.Mail;
using BoAdvisorProfiling;
using VoAdvisorProfiling;
using PCGMailLib;
using System.Text;
using BoCustomerProfiling;
using System.IO;
using System.Data;
using BoReports;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections;
using System.Net.Mime;

namespace WealthERP.Reports
{
    public partial class EmailReport : System.Web.UI.Page
    {
        ReportDocument crmain;

        DateBo dtBo = new DateBo();
        DateTime dtTo = new DateTime();
        DateTime dtFrom = new DateTime();
        string ctrlPrefix = "ctrl_MFReports$tabViewAndEmailReports$tabpnlEmailReports$";
        public string isMail = "0";
        int CountReport = 0;

        MFReportVo mfReport = new MFReportVo();
        EquityReportVo equityReport = new EquityReportVo();
        PortfolioReportVo portfolioReport = new PortfolioReportVo();
        FinancialPlanningVo financialPlanning = new FinancialPlanningVo();
        AdvisorVo advisorVo = null;
        RMVo rmVo = null;
        CustomerVo customerVo;
        ReportType CurrentReportType;


        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["advisorVo"] != null)
                Session["newAdvisorVo"] = Session["advisorVo"];
            if (Session["rmVo"] != null)
                Session["newRmVo"] = Session["rmVo"];


            advisorVo = (AdvisorVo)Session["newAdvisorVo"];
            rmVo = (RMVo)Session["newRmVo"];

            if (!IsPostBack)
            {
                if (Request.QueryString["mail"] == "1")
                    isMail = "1";

                if (Directory.Exists(Server.MapPath("~/Reports/TempReports/") + rmVo.RMId))
                {
                    DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Reports/TempReports/") + rmVo.RMId);
                    
                    foreach (FileInfo f in di.GetFiles())
                    {
                        f.Delete();
                    }
                }
                else
                    Directory.CreateDirectory(Server.MapPath("~/Reports/TempReports/") + rmVo.RMId);
            }
            ctrlPrefix = "ctrl_MFReports$tabViewAndEmailReports$tabpnlEmailReports$";
            CurrentReportType = ReportType.MFReports;
            

            if (PreviousPage != null)
            {
                if (Request.Form[ctrlPrefix + "chkMFSummary"] == "on")
                {
                    CountReport = CountReport + 1;
                    crmain = new ReportDocument();
                    GetReportParameters("CATEGORY_WISE");
                    DisplayReport(mfReport);
                    //FillEmailValues();
                }
                if (Request.Form[ctrlPrefix + "chkPortfolioReturns"] == "on")
                {
                    CountReport = CountReport + 1;
                    crmain = new ReportDocument();
                    GetReportParameters("RETURNS_PORTFOLIO");
                    DisplayReport(mfReport);
                    //FillEmailValues();
                }
                if (Request.Form[ctrlPrefix + "chkDividendDetail"] == "on")
                {
                    CountReport = CountReport + 1;
                    crmain = new ReportDocument();
                    GetReportParameters("DIVIDEND_STATEMENT");
                    DisplayReport(mfReport);
                    //FillEmailValues();
                }
                if (Request.Form[ctrlPrefix + "chkTransactionReport"] == "on")
                {
                    CountReport = CountReport + 1;
                    crmain = new ReportDocument();
                    GetReportParameters("TRANSACTION_REPORT");
                    DisplayReport(mfReport);
                }
                if (Request.Form[ctrlPrefix + "chkDividendSummary"] == "on")
                {
                    CountReport = CountReport + 1;
                    crmain = new ReportDocument();
                    GetReportParameters("DIVIDEND_SUMMARY");
                    DisplayReport(mfReport);
                }
                if (Request.Form[ctrlPrefix + "chkCapitalGainDetails"] == "on")
                {
                    CountReport = CountReport + 1;
                    crmain = new ReportDocument();
                    GetReportParameters("CAPITAL_GAIN_DETAILS");
                    DisplayReport(mfReport);
                }
                if (Request.Form[ctrlPrefix + "chkCapitalGainSummary"] == "on")
                {
                    CountReport = CountReport + 1;
                    crmain = new ReportDocument();
                    GetReportParameters("CAPITAL_GAIN_SUMMARY");
                    DisplayReport(mfReport);
                }
                FillEmailValues();
            }
        }

        //*************************************Code for Emailing reports********************************************

        #region ReportDisplay Methods

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
                string fileExtension = ".pdf";
                string exportFilename = string.Empty;
                

                switch (report.SubType)
                {

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
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat , exportFilename);
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
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);
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
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, exportFilename);
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
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);
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
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);
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
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);
                        }
                        else
                            SetNoRecords();
                        break;

                    case "DIVIDEND_SUMMARY":
                        crmain.Load(Server.MapPath("MFDividendSummary.rpt"));
                        DataTable dtDividendSummary = mfReports.GetDivdendReport(report);
                        customerVo = (CustomerVo)Session["CusVo"];
                        if (dtDividendSummary.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtDividendSummary);
                            setLogo();
                            //if (!String.IsNullOrEmpty(dtDividend.Rows[0]["Name"].ToString()))
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("FromDate",  report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());

                            AssignReportViewerProperties();
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId.ToString() + "/" + report.SubType.ToString() + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);
                        }
                        else
                            SetNoRecords();
                        break;

                       
                }
                //Filling Emails
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
        }

        /// <summary>
        /// Get Report Parameters and store it in to session.
        /// </summary>
        private void GetReportParameters(string reportSubType)
        {
            CalculateDateRange(reportSubType, out dtFrom, out dtTo);
        
            mfReport.SubType = reportSubType;
            mfReport.PortfolioIds = GetPortfolios();

            if (reportSubType == "CATEGORY_WISE" || reportSubType == "RETURNS_PORTFOLIO")
            {
                mfReport.FromDate = Convert.ToDateTime(Request.Form[ctrlPrefix + "txtEmailAsOnDate"]);
                mfReport.ToDate = Convert.ToDateTime(Request.Form[ctrlPrefix + "txtEmailAsOnDate"]);
            }
            else
            {
                mfReport.FromDate = dtFrom;
                mfReport.ToDate = dtTo;
            }
            

            if (!String.IsNullOrEmpty(Request.Form["ctrl_MFReports$hdnCustomerId1"]))
            {
                mfReport.CustomerIds = Request.Form["ctrl_MFReports$hdnCustomerId1"];
                mfReport.GroupHead = Request.Form["ctrl_MFReports$hdnCustomerId1"];
            }
            Session["reportParams"] = mfReport;
        }

        /// <summary>
        /// Get all the selected portfolio Ids and return it as a comma separated string.
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
        private void CalculateDateRange(string reportType,out DateTime fromDate, out DateTime toDate)
        {
            if (reportType == "RETURNS_PORTFOLIO" || reportType == "CATEGORY_WISE")
            {
                fromDate = Convert.ToDateTime(Request.Form[ctrlPrefix + "txtEmailAsOnDate"]);
                toDate = Convert.ToDateTime(Request.Form[ctrlPrefix + "txtEmailAsOnDate"]);
            }
            else if (Request.Form[ctrlPrefix + "DatePick"] == "rdoDatePeriod")
            {
                dtBo.CalculateFromToDatesUsingPeriod(Request.Form[ctrlPrefix + "ddlEmailDatePeriod"], out dtFrom, out dtTo);
                fromDate = dtFrom;
                toDate = dtTo;
            }
            else //if (Request.Form[ctrlPrefix + "hidDateType"] == "AS_ON")
            {
                fromDate = Convert.ToDateTime(Request.Form[ctrlPrefix + "txtEmailFromDate"]);
                toDate = Convert.ToDateTime(Request.Form[ctrlPrefix + "txtEmailToDate"]);
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
                Session["hidBody"] = txtBody.Text = GetReportBody(cust.Salutation + "." + " " + cust.FirstName + " " + cust.LastName, subType, fromDate, toDate).Replace("\r", "");
                //Session["hidBody"] = txtBody.Text = GetReportBody(cust.FirstName + " " + cust.LastName, subType, fromDate, toDate).Replace("\r", "");

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
            
            strMail.Append("<br/>&nbsp;&nbsp;&nbsp;&nbsp Please find attached " + subject + ".");
            //strMail.Append("<br/>Regards,<br/>" + rmVo.FirstName + " " + rmVo.LastName);
            strMail.Append("<br/><br/> <b> Regards,<br/>" + rmVo.FirstName + " " + rmVo.LastName + "<br/><i>Mo:" + rmVo.Mobile + "<br/>Ph:+" + rmVo.OfficePhoneExtStd + "-" + rmVo.OfficePhoneExtNumber + "</i></b>");

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
                        if (CountReport==1)
                        {
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
                        else
                            subject = "MF Report - ";
                    }
                    CountReport = 0;
                    break;
                case ReportType.PortfolioReports:
                    subject = "Portfolio Report - ";
                    break;
                case ReportType.FinancialPlanning:
                    subject = "Financial Planning Report";
                    break;
            }





            //string subject = string.Empty;
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
        private bool SendMail()
        {
            Emailer emailer = new Emailer();
            EmailMessage email = new EmailMessage();
            Attachment attachment = null;
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

                DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Reports/TempReports/") + rmVo.RMId);
                foreach (FileInfo f in di.GetFiles())
                {
                    attachment = new Attachment(Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + f.Name);
                    email.Attachments.Add(attachment);
                }

                //email.Subject = hidSubject.Value;
                //hidSubject.Value = string.Empty;
                //email.IsBodyHtml = true;
                //email.Body = hidBody.Value.Replace("\n", "<br/>");

                //isMailSent = emailer.SendMail(email);




                //Embaded Advisor Logo Along with mail..Modified by ******Pramoda Sahoo*******

                //Create two views, one text, one HTML.
                string MailBody = hidBody.Value.Replace("\n", "<br/>");

                System.Net.Mail.AlternateView htmlView;
                System.Net.Mail.AlternateView plainTextView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("Text view", null, "text/plain");
                //System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(hidBody.Value.Trim() + "<image src=cid:HDIImage>", null, "text/html");
                htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<html><body " + "style='font-family:Tahoma, Arial; font-size: 10pt;'><p>" + MailBody + "</p>'<img src='cid:HDIImage'></body></html>", null, "text/html");
                //Add image to HTML version
                if (Session["advisorVo"] != null)
                    logoPath = "Images/" + ((AdvisorVo)Session["advisorVo"]).LogoPath;
                //System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath("~/Images/") + @"\3DSYRW_4009.JPG", "image/jpeg");
                System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(logoPath, "image/jpeg");
                imageResource.ContentId = "HDIImage";
                htmlView.LinkedResources.Add(imageResource);
                //Add two views to message.
                email.AlternateViews.Add(plainTextView);
                email.AlternateViews.Add(htmlView);
                //Send message
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                smtpClient.Send(email);



                //email.Body = hidBody.Value.Replace("\n", "<br/>");
                //SmtpClient smtp = new SmtpClient();
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;

                //isMailSent = emailer.SendMail(email);



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

            string exportFileName = Server.MapPath("~/Reports/TempReports/") + CurrentReportType.ToString() + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
            //string exportFileName = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
            crmain.ExportToDisk(formatType, exportFileName);
            return exportFileName;
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {

            DirectoryInfo di=null;
            int reportExistFlag = 0;
            //string reportFileName = ExportToDisk();
            divMessage.Visible = true;
            if (Directory.Exists(Server.MapPath("~/Reports/TempReports/") + rmVo.RMId))
            {
                di = new DirectoryInfo(Server.MapPath("~/Reports/TempReports/") + rmVo.RMId);
                foreach (FileInfo f in di.GetFiles())
                {
                    reportExistFlag = 1;
                }
            }


            
            if (di.GetFiles().Length != 0)
            {
                bool isMailSent = SendMail();
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
            else
            {
                lblEmailStatus.Text = "No Report Created.Email not sent.";
                lblEmailStatus.CssClass = "Error";
            }

        }

        #endregion


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
