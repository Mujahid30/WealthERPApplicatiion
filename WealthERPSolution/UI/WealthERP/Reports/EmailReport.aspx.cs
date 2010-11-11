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
using VoEmailSMS;
using System.Net.Mime;
using DanLudwig;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using WealthERP.Base;


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
        //CustomerVo customerVo;
        ReportType CurrentReportType;
        int CustomerId = 0;
        int GroupCustometId = 0;
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();
        bool GroupCustomer = true;
        string mFundSummary = string.Empty;
        string portfolioRHolding = string.Empty;
        string comprehensive = string.Empty;
        string eCapitalGainDetails = string.Empty;
        string eCapitalGainsSummary = string.Empty;
        string transactionReport = string.Empty;
        string dividendStatement = string.Empty;
        string dividendSummary = string.Empty;
        string capitalGainDetails = string.Empty;
        string capitalGainSummary = string.Empty;
        string mailSendStatus = "";
        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    if (Session["Theme"] == null || Session["Theme"].ToString() == string.Empty)
        //    {
        //        Session["Theme"] = "Maroon";
        //    }

        //    Page.Theme = Session["Theme"].ToString();
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        /// <summary>
        /// Page Load Functionality called from Source code 
        /// </summary>
        public void sendMailFunction()
        {
            Page.Response.BufferOutput = false;        
            if (Session["advisorVo"] != null)
                Session["newAdvisorVo"] = Session["advisorVo"];
            if (Session["rmVo"] != null)
                Session["newRmVo"] = Session["rmVo"];

            trCustomerlist.Visible = false;                    
            advisorVo = (AdvisorVo)Session["newAdvisorVo"];
            rmVo = (RMVo)Session["newRmVo"];

            if (!IsPostBack)
            {
                if (Request.QueryString["mail"] == "1")
                {
                    //Setting ClientId of EmailTab and report type as MFReports.
                    ctrlPrefix = "ctrl_MFReports$tabViewAndEmailReports$tabpnlEmailReports$";
                    CurrentReportType = ReportType.MFReports;

                  
                    //Getting all Selected CustomerID For Sending BULK E-Mail

                    String AllCustomerId = Request.Form["ctrl_MFReports$SelectedCustomets4Email"];
                    
                    char[] separator = new char[] { ',' };

                    string[] strSplitArr = AllCustomerId.Split(separator);

                    DataTable dtCustomerReportMailStatus = new DataTable();
                    DataRow drCustomerReportMailStatus;

                    if (strSplitArr.Count() > 0)
                    {
                        dtCustomerReportMailStatus.Columns.Add("CustometName");
                        dtCustomerReportMailStatus.Columns.Add("MFundSummary");
                        dtCustomerReportMailStatus.Columns.Add("PortfolioRHolding");
                        dtCustomerReportMailStatus.Columns.Add("Comprehensive");
                        dtCustomerReportMailStatus.Columns.Add("ECapitalGainDetails");
                        dtCustomerReportMailStatus.Columns.Add("ECapitalGainsSummary");
                        dtCustomerReportMailStatus.Columns.Add("TransactionReport");
                        dtCustomerReportMailStatus.Columns.Add("DividendStatement");
                        dtCustomerReportMailStatus.Columns.Add("DividendSummary");
                        dtCustomerReportMailStatus.Columns.Add("CapitalGainDetails");
                        dtCustomerReportMailStatus.Columns.Add("CapitalGainSummary");                     
 
                    }

                    foreach (string arrStr in strSplitArr)
                    {

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

                        if (!String.IsNullOrEmpty(arrStr))
                        {
                            CustomerId = int.Parse(arrStr);
                            //If Group Customer radio Button is selected then assign group HeadId Else GroupCustomer FLAG Make false 
                            if (Request.Form[ctrlPrefix + "EmailGrpOrInd"] == "rbnGroup")
                            {
                                GroupCustomer = true;
                                GroupCustometId = int.Parse(arrStr);
                            }
                            else
                                GroupCustomer = false;

                            customerVo = customerBo.GetCustomer(CustomerId);

                            if (!string.IsNullOrEmpty(customerVo.Email.Trim()))
                            {
                                clearAllReportString();
                                ExportTODisk();
                                MailSending();

                                drCustomerReportMailStatus = dtCustomerReportMailStatus.NewRow();
                                string CustometName = customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName;
                                drCustomerReportMailStatus["CustometName"] = CustometName;
                                if(!string.IsNullOrEmpty(mFundSummary))
                                    drCustomerReportMailStatus["MFundSummary"] = mFundSummary;
                                else
                                    drCustomerReportMailStatus["MFundSummary"] = mailSendStatus;

                                if (!string.IsNullOrEmpty(portfolioRHolding))
                                    drCustomerReportMailStatus["PortfolioRHolding"] = portfolioRHolding;
                                else
                                    drCustomerReportMailStatus["PortfolioRHolding"] = mailSendStatus;

                                if (!string.IsNullOrEmpty(comprehensive))
                                    drCustomerReportMailStatus["Comprehensive"] = comprehensive;
                                else
                                    drCustomerReportMailStatus["Comprehensive"] = mailSendStatus;

                                if (!string.IsNullOrEmpty(eCapitalGainDetails))
                                    drCustomerReportMailStatus["ECapitalGainDetails"] = eCapitalGainDetails;
                                else
                                    drCustomerReportMailStatus["ECapitalGainDetails"] = mailSendStatus;

                                if (!string.IsNullOrEmpty(eCapitalGainsSummary))
                                    drCustomerReportMailStatus["ECapitalGainsSummary"] = eCapitalGainsSummary;
                                else
                                    drCustomerReportMailStatus["ECapitalGainsSummary"] = mailSendStatus;

                                if (!string.IsNullOrEmpty(transactionReport))
                                    drCustomerReportMailStatus["TransactionReport"] = transactionReport;
                                else
                                    drCustomerReportMailStatus["TransactionReport"] = mailSendStatus;

                                if (!string.IsNullOrEmpty(dividendStatement))
                                    drCustomerReportMailStatus["DividendStatement"] = dividendStatement;
                                else
                                    drCustomerReportMailStatus["DividendStatement"] = mailSendStatus;

                                if (!string.IsNullOrEmpty(dividendSummary))
                                    drCustomerReportMailStatus["DividendSummary"] = dividendSummary;
                                else
                                    drCustomerReportMailStatus["DividendSummary"] = mailSendStatus;

                                if (!string.IsNullOrEmpty(capitalGainDetails))
                                    drCustomerReportMailStatus["CapitalGainDetails"] = capitalGainDetails;
                                else
                                    drCustomerReportMailStatus["CapitalGainDetails"] = mailSendStatus;

                                if (!string.IsNullOrEmpty(capitalGainSummary))
                                    drCustomerReportMailStatus["CapitalGainSummary"] = capitalGainSummary;
                                else
                                    drCustomerReportMailStatus["CapitalGainSummary"] = mailSendStatus;

                                dtCustomerReportMailStatus.Rows.Add(drCustomerReportMailStatus);                     
                                
                            }
                            else
                            {
                                drCustomerReportMailStatus = dtCustomerReportMailStatus.NewRow();
                                string CustometName=customerVo.FirstName+ " " + customerVo.MiddleName + " " + customerVo.LastName;
                                drCustomerReportMailStatus["CustometName"] = CustometName;
                                drCustomerReportMailStatus["MFundSummary"] = "Email-Id Not in Profile";
                                drCustomerReportMailStatus["PortfolioRHolding"] = "Email-Id Not in Profile";
                                drCustomerReportMailStatus["Comprehensive"] = "Email-Id Not in Profile";
                                drCustomerReportMailStatus["ECapitalGainDetails"] = "Email-Id Not in Profile";
                                drCustomerReportMailStatus["ECapitalGainsSummary"] = "Email-Id Not in Profile";
                                drCustomerReportMailStatus["TransactionReport"] = "Email-Id Not in Profile";
                                drCustomerReportMailStatus["DividendStatement"] = "Email-Id Not in Profile";
                                drCustomerReportMailStatus["DividendSummary"] = "Email-Id Not in Profile";
                                drCustomerReportMailStatus["CapitalGainDetails"] = "Email-Id Not in Profile";
                                drCustomerReportMailStatus["CapitalGainSummary"] = "Email-Id Not in Profile";
                                dtCustomerReportMailStatus.Rows.Add(drCustomerReportMailStatus);
                                
                            }

                        }

                    }
                    if (dtCustomerReportMailStatus.Rows.Count > 0)
                    {
                        
                        //lblEmailStatus.Text = lblEmailStatus.Text + "  " + " and Report not send to the following customers as E-Mail Id is not available in profile";
                        trCustomerlist.Visible = true;
                        gvEmailCustomerList.DataSource = dtCustomerReportMailStatus;
                        gvEmailCustomerList.DataBind();
                        ShowHideGridViewColumns(gvEmailCustomerList);
                    }
                   
                    isMail = "0";


                }

            }
           

            
        }
        /// <summary>
        /// hide Report columns in the gridview based on seleted report..
        /// </summary>
        private void ShowHideGridViewColumns(GridView gvEmailCustomerList)
        {
            if (Request.Form[ctrlPrefix + "chkMFSummary"] != "on")
            {
                gvEmailCustomerList.Columns[1].Visible = false;
            }
            if (Request.Form[ctrlPrefix + "chkPortfolioReturns"] != "on")
            {
                gvEmailCustomerList.Columns[2].Visible = false;
            }
            if (Request.Form[ctrlPrefix + "chkPortfolioAnalytics"] != "on")
            {
                gvEmailCustomerList.Columns[3].Visible = false;                
            }
                    
            if (Request.Form[ctrlPrefix + "chkEligibleCapitalgainsDetail"] != "on")
            {
                gvEmailCustomerList.Columns[4].Visible = false;
            }
            if (Request.Form[ctrlPrefix + "chkEligibleCapitalGainsSummary"] != "on")
            {
                gvEmailCustomerList.Columns[5].Visible = false;
            }
            if (Request.Form[ctrlPrefix + "chkTransactionReport"] != "on")
            {
                gvEmailCustomerList.Columns[6].Visible = false;
            }
            if (Request.Form[ctrlPrefix + "chkDividendDetail"] != "on")
            {
                gvEmailCustomerList.Columns[7].Visible = false;
            }
            if (Request.Form[ctrlPrefix + "chkDividendSummary"] != "on")
            {
                gvEmailCustomerList.Columns[8].Visible = false;

            }
            if (Request.Form[ctrlPrefix + "chkCapitalGainDetails"] != "on")
            {
                gvEmailCustomerList.Columns[9].Visible = false;

            }
            if (Request.Form[ctrlPrefix + "chkCapitalGainSummary"] != "on")
            {
                gvEmailCustomerList.Columns[10].Visible = false;

            }
            
        }

        /// <summary>
        /// Clearing all the string variable for next customer report generaion and mail send
        /// </summary>
        public void clearAllReportString()
        {
            mFundSummary = string.Empty;
            portfolioRHolding = string.Empty;
            comprehensive = string.Empty;
            eCapitalGainDetails = string.Empty;
            eCapitalGainsSummary = string.Empty;
            transactionReport = string.Empty;
            dividendStatement = string.Empty;
            dividendSummary = string.Empty;
            capitalGainDetails = string.Empty;
            capitalGainSummary = string.Empty;
            mailSendStatus = string.Empty;
        }

        /// <summary>
        /// This will Export report into disk based on CheckBox Selected from UI For Bulk-Email.
        /// </summary>
        private void ExportTODisk()
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
            if (Request.Form[ctrlPrefix + "chkPortfolioAnalytics"] == "on")
            {
                CountReport = CountReport + 1;
                crmain = new ReportDocument();
                GetReportParameters("COMPREHENSIVE");
                DisplayReport(mfReport);
                //FillEmailValues();
            }
            //Add For three more new Report :Author:--pramod
            if (Request.Form[ctrlPrefix + "chkPortfolioReturnRE"] == "on")
            {
                CountReport = CountReport + 1;
                crmain = new ReportDocument();
                GetReportParameters("RETURNS_PORTFOLIO_REALIZED");
                DisplayReport(mfReport);
                //FillEmailValues();
            }
            if (Request.Form[ctrlPrefix + "chkEligibleCapitalgainsDetail"] == "on")
            {
                CountReport = CountReport + 1;
                crmain = new ReportDocument();
                GetReportParameters("ELIGIBLE_CAPITAL_GAIN_DETAILS");
                DisplayReport(mfReport);
                //FillEmailValues();
            }
            if (Request.Form[ctrlPrefix + "chkEligibleCapitalGainsSummary"] == "on")
            {
                CountReport = CountReport + 1;
                crmain = new ReportDocument();
                GetReportParameters("ELIGIBLE_CAPITAL_GAIN_SUMMARY");
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
                //customerVo = (CustomerVo)Session["CusVo"];
                //if (Session["CusVo"] != null)
                //    customerVo = (CustomerVo)Session["CusVo"];
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
                            crmain.SetParameterValue("DateRange", "Period: " + report.FromDate.ToShortDateString() + " to " + report.ToDate.ToShortDateString());
                            //crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            //crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);
                        }
                        else
                        {
                            capitalGainSummary = "No Record Found";
                        }
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
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);
                        }
                        else
                        {
                            capitalGainDetails = "No Record Found";
                            
                        }
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
                            AssignReportViewerProperties();
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, exportFilename);
                        }
                        else
                        {
                            mFundSummary = "No Record Found";
                            
                        }
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
                            crmain.SetParameterValue("DateRange", "Period: " + report.FromDate.ToShortDateString() + " to " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());

                            AssignReportViewerProperties();
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);
                        }
                        else
                        {
                            transactionReport = "No Record Found";
                           
                        }
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
                            crmain.SetParameterValue("DateRange", "Period: " + report.FromDate.ToShortDateString() + " to " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());

                            AssignReportViewerProperties();
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);
                        }
                        else
                        {
                            dividendStatement = "No Record Found";
                            
                        }
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
                            crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);
                        }
                        else
                        {
                            portfolioRHolding = "No Record Found";
                            
                        }
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
                            crmain.SetParameterValue("AsOnDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);

                        }
                        else
                        {
                            comprehensive = "No Record Found";
                           
                        }
                        break;

                    case "DIVIDEND_SUMMARY":
                        crmain.Load(Server.MapPath("MFDividendSummary.rpt"));
                        DataTable dtDividendSummary = mfReports.GetDivdendReport(report);
                        //customerVo = (CustomerVo)Session["CusVo"];
                        if (dtDividendSummary.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtDividendSummary);
                            setLogo();
                            //if (!String.IsNullOrEmpty(dtDividend.Rows[0]["Name"].ToString()))
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("DateRange", "Period: " + report.FromDate.ToShortDateString() + " to " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                            crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());

                            AssignReportViewerProperties();
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId.ToString() + "/" + report.SubType.ToString() + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);
                        }
                        else
                        {
                            dividendSummary = "No Record Found";
                            
                        }
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
                            crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("AsOnDate", report.FromDate.ToShortDateString());
                            AssignReportViewerProperties();
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);
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
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                            //crmain.SetParameterValue("AsOnDate", report.FromDate.ToShortDateString());
                            AssignReportViewerProperties();
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);
                        }
                        else
                        {
                            eCapitalGainDetails = "No Record Found";
                            
                        }
                        break;
                      
                    case "ELIGIBLE_CAPITAL_GAIN_SUMMARY":
                        crmain.Load(Server.MapPath("EligibleCapitalGainsDetails.rpt"));
                        DataTable dtEligibleCapitalGainsSummary = mfReports.GetEligibleCapitalGainDetailsReport(report);
                        if (dtEligibleCapitalGainsSummary.Rows.Count > 0)
                        {
                            crmain.SetDataSource(dtEligibleCapitalGainsSummary);
                            setLogo();
                            crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                            //crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                            crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                            AssignReportViewerProperties();
                            exportFilename = Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + report.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                            crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportFilename);
                        }
                        else
                        {
                            eCapitalGainsSummary = "No Record Found";
                            
                        }
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
            //customerVo = (CustomerVo)Session["CusVo"];
            RMVo customerRMVo = new RMVo();
            AdvisorStaffBo adviserStaffBo = new AdvisorStaffBo();
            try
            {
                //setLogo();
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

                crmain.SetParameterValue("OrgAddress", advisorVo.City + ", " + advisorVo.State);
                //crmain.SetParameterValue("OrgDetails", advisorVo.Email + ", " + advisorVo.Website);
                //crmain.SetParameterValue("OrgTelephone", "+91-" + advisorVo.Phone1Std + "-" + advisorVo.Phone1Number);
                //crmain.SetParameterValue("Organization", advisorVo.OrganizationName);

                crmain.SetParameterValue("RMContactDetails", "E-mail: " + customerVo.RMEmail);
                crmain.SetParameterValue("MobileNo", "Phone: " + "+" + customerVo.RMOfficePhone);
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
            //pramod
            //mfReport.PortfolioIds = GetPortfolios();
            if (GroupCustomer == true)
                mfReport.PortfolioIds = GetAllPortfolioIds();
            else
            {
                string PortFolioIds = GetAllPortfolioOfCustomer(CustomerId);
                PortFolioIds = PortFolioIds.Substring(0, PortFolioIds.Length - 1);
                mfReport.PortfolioIds = PortFolioIds;
            }

            if (reportSubType == "CATEGORY_WISE" || reportSubType == "COMPREHENSIVE" || reportSubType == "RETURNS_PORTFOLIO" || reportSubType == "RETURNS_PORTFOLIO_REALIZED" || reportSubType == "ELIGIBLE_CAPITAL_GAIN_DETAILS" || reportSubType == "ELIGIBLE_CAPITAL_GAIN_SUMMARY")
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
                //comented by pramod
                //mfReport.CustomerIds = Request.Form["ctrl_MFReports$hdnCustomerId1"];
                //mfReport.GroupHead = Request.Form["ctrl_MFReports$hdnCustomerId1"];

                mfReport.CustomerIds =CustomerId.ToString();
                mfReport.GroupHead = GroupCustometId.ToString();


            }
            else if (Request.QueryString["mail"] == "1")
            {
                mfReport.CustomerIds = CustomerId.ToString();
                if(GroupCustomer==true)
                 mfReport.GroupHead = GroupCustometId.ToString();
 
            }
            Session["reportParams"] = mfReport;
        }

        /// <summary>
        /// This Returns all portfolio Id of all customers of One Group Head Author:Pramod
        /// </summary>
        /// <returns></returns>

        private string GetAllPortfolioIds()
        {
            string AllFolioIds = "";
            CustomerBo customerBo = new CustomerBo();
            CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
            
                DataTable dt = customerFamilyBo.GetAllCustomerAssociates(CustomerId);
                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        AllFolioIds = AllFolioIds + GetAllPortfolioOfCustomer(Convert.ToInt32(dr["C_AssociateCustomerId"]));
                        
                    }
                }
                AllFolioIds = AllFolioIds.Substring(0, AllFolioIds.Length - 1);
            
            return AllFolioIds;
        }

        /// <summary>
        /// This Returns all portfolio Id of a particular customer. Author:Pramod
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
                        if (custPortfolio.PortfolioName == "MyPortfolio")
                        {
                            portfolioIDs = portfolioIDs + custPortfolio.PortfolioId;
                            portfolioIDs = portfolioIDs + ",";
                        }
                        //checkbox.Append("<input type='checkbox' checked name='chk--" + custPortfolio.PortfolioId + "' id='chk--" + custPortfolio.PortfolioId + "'>" + custPortfolio.PortfolioName);
                        //checkboxList.Items.Add(new ListItem(custPortfolio.PortfolioName, custPortfolio.PortfolioId.ToString()));
                    }
                    
                }
              
            }

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
        private void CalculateDateRange(string reportType,out DateTime fromDate, out DateTime toDate)
        {
            if (reportType == "RETURNS_PORTFOLIO" || reportType == "COMPREHENSIVE" || reportType == "CATEGORY_WISE" || reportType == "RETURNS_PORTFOLIO_REALIZED" || reportType == "ELIGIBLE_CAPITAL_GAIN_DETAILS" || reportType == "ELIGIBLE_CAPITAL_GAIN_SUMMARY")
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
               
                CustomerVo cust = null;

                string subType = string.Empty;
                DateTime fromDate = DateTime.MinValue;
                DateTime toDate = DateTime.MinValue;

                if (CurrentReportType == ReportType.MFReports)
                {
                    subType = mfReport.SubType;
                    fromDate = mfReport.FromDate;
                    toDate = mfReport.ToDate;
                    cust = customerVo;

                    //if (!String.IsNullOrEmpty(mfReport.GroupHead))
                    //    cust = customerBo.GetCustomer(Convert.ToInt32(mfReport.GroupHead));
                    //else
                    //    cust = customerBo.GetCustomer(Convert.ToInt32(mfReport.CustomerIds));
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
                if(!string.IsNullOrEmpty(cust.Email))
                {
                    Session["hidTo"] = txtTo.Text = cust.Email;
                }
                //if (!string.IsNullOrEmpty(customerVo.Email))
                //{
                //    Session["hidTo"] = customerVo.Email;
                //}
                Session["hidSubject"] = txtSubject.Text = GetReportSubject(subType, fromDate, toDate);
                if (cust.Salutation == string.Empty || cust.Salutation == "")
                {
                    Session["hidBody"] = txtBody.Text = GetReportBody(cust.FirstName + " " + cust.LastName, subType, fromDate, toDate).Replace("\r", "");

                }
                else
                Session["hidBody"] = txtBody.Text = GetReportBody(cust.Salutation + " " + cust.FirstName + " " + cust.LastName, subType, fromDate, toDate).Replace("\r", "");
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
            
            strMail.Append("<br/> Please find attached " + subject + ".");
            //strMail.Append("<br/>Regards,<br/>" + rmVo.FirstName + " " + rmVo.LastName);
            if (advisorVo!=null)
                if (!string.IsNullOrEmpty(advisorVo.Website))
                {
                    strMail.Append("<br/><br/>Regards,<br/>" + customerVo.RMName + "<br/>Mo: " + customerVo.RMMobile + "<br/>Ph: +" + customerVo.RMOfficePhone + "<br/>Website: " + advisorVo.Website);
                }
                else
                    strMail.Append("<br/><br/>Regards,<br/>" + customerVo.RMName + "<br/>Mo: " + customerVo.RMMobile + "<br/>Ph: +" + customerVo.RMOfficePhone);

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
                                    subject = "Portfolio Return-Holding ";
                                    break;
                                case "COMPREHENSIVE":
                                    subject = "Comprehensive Report - ";
                                    break;
                                case "RETURNS_PORTFOLIO_REALIZED":
                                    subject = "Portfolio Returns Realized - ";
                                    break;
                                case "ELIGIBLE_CAPITAL_GAIN_DETAILS":
                                    subject = "Eligible Capital Gain Details - ";
                                    break;
                                case "ELIGIBLE_CAPITAL_GAIN_SUMMARY":
                                    subject = "Eligible Capital Gain Summary - ";
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
            AdvisorVo advisorVo = (AdvisorVo)Session["advisorVo"];
            string logoPath = "";
            string senderName = rmVo.FirstName + " " + rmVo.LastName;

            try
            {
                //string[] toAddresses = hidTo.Value.Split(new char[] { ',' });
                //foreach (string toEmail in toAddresses)
                //    email.To.Add(toEmail);
                if (!string.IsNullOrEmpty(Session["hidto"].ToString()))
                 email.To.Add(Session["hidto"].ToString());


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
                string attPath=Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/";
                string fileNames="";
                foreach (FileInfo f in di.GetFiles())
                {
                    attachment = new Attachment(Server.MapPath("~/Reports/TempReports/") + rmVo.RMId + "/" + f.Name);
                    email.Attachments.Add(attachment);
                    
                    fileNames=fileNames+f.Name+";";
                }

                email.Subject = Session["hidSubject"].ToString();
                //hidSubject.Value = string.Empty;
                //email.IsBodyHtml = true;
                //email.Body = hidBody.Value.Replace("\n", "<br/>");

                //isMailSent = emailer.SendMail(email);




                //Embaded Advisor Logo Along with mail..Modified by ******Pramoda Sahoo*******

                //Create two views, one text, one HTML.
                string MailBody = Session["hidBody"].ToString().Replace("\n", "<br/>");

                System.Net.Mail.AlternateView htmlView;
                System.Net.Mail.AlternateView plainTextView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("Text view", null, "text/plain");
                //System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(hidBody.Value.Trim() + "<image src=cid:HDIImage>", null, "text/html");
                htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<html><body " + "style='font-family:Tahoma, Arial; font-size: 10pt;'><p>" + MailBody + "</p><img src='cid:HDIImage'></body></html>", null, "text/html");
                //Add image to HTML version
                if (Session["advisorVo"] != null)
                    logoPath = "~/Images/" + ((AdvisorVo)Session["advisorVo"]).LogoPath;
                if (!File.Exists(Server.MapPath(logoPath)))
                   logoPath = "~/Images/spacer.png";
                //System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath("~/Images/") + @"\3DSYRW_4009.JPG", "image/jpeg");
                System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath(logoPath), "image/jpeg");
                imageResource.ContentId = "HDIImage";
                htmlView.LinkedResources.Add(imageResource);
                //Add two views to message.
                email.AlternateViews.Add(plainTextView);
                email.AlternateViews.Add(htmlView);
                //Send message
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                //smtpClient.Send(email);



                //email.Body = hidBody.Value.Replace("\n", "<br/>");
                //SmtpClient smtp = new SmtpClient();
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;

                isMailSent = emailer.SendMail(email);
                
                    EmailSMSBo emailSMSBo = new EmailSMSBo();
                    EmailVo emailVo = new EmailVo();
                    emailVo.AdviserId = advisorVo.advisorId;
                    emailVo.AttachmentPath = attPath;
                    if (Session["CusVo"] != null)
                        emailVo.CustomerId = ((CustomerVo)Session["CusVo"]).CustomerId;
                    else
                        emailVo.CustomerId = 0;
                    emailVo.EmailQueueId = 0;
                    emailVo.EmailType = "Report";
                    emailVo.FileName = fileNames;
                    emailVo.HasAttachment = 1;
                    emailVo.ReportCode = 0;
                    emailVo.SentDate = DateTime.Today;
                    emailVo.Status = 1;
                    emailSMSBo.AddToEmailLog(emailVo);
                
              

            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
            finally
            {
                email.Dispose();
            }
            return isMailSent;
                
        }

        /// <summary>
        /// Convert the report to selected format and save it to disk(Reports/TempReports folder).It is calling when Mail Sending From report Viewer.
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
            //divMessage.Visible = true;
            if (Directory.Exists(Server.MapPath("~/Reports/TempReports/") + rmVo.RMId))
            {
                di = new DirectoryInfo(Server.MapPath("~/Reports/TempReports/") + rmVo.RMId);
                foreach (FileInfo f in di.GetFiles())
                {
                    reportExistFlag = 1;
                }
            }


            
            //if (di.GetFiles().Length != 0)
            //{
            //    bool isMailSent = SendMail();
            //    if (isMailSent)
            //    {
            //        lblEmailStatus.Text = "Email sent successfully";
            //        lblEmailStatus.CssClass = "SuccessMsg";
            //    }
            //    else
            //    {
            //        lblEmailStatus.Text = "An error occurred while sending mail.";
            //        lblEmailStatus.CssClass = "Error";
            //    }
            //}
            //else
            //{
            //    lblEmailStatus.Text = "No Report Created.Email not sent.";
            //    lblEmailStatus.CssClass = "Error";
            //}

        }

        /// <summary>
        /// Mail Sending Functinality
        /// </summary>
        private void MailSending()
        {           
            DirectoryInfo di = null;
            int reportExistFlag = 0;
            //string reportFileName = ExportToDisk();
            //divMessage.Visible = true;
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
                    mailSendStatus = "Email sent successfully";
                    //lblEmailStatus.Text = "Email sent successfully";
                    //lblEmailStatus.CssClass = "SuccessMsg";
                }
                else
                {
                    mailSendStatus="Error occurred while sending mail";
                    //lblEmailStatus.Text = "An error occurred while sending mail.";
                    //lblEmailStatus.CssClass = "Error";
                }
            }
            else
            {
                //lblEmailStatus.Text = "No Report Created.Email not sent.";
                //lblEmailStatus.CssClass = "Error";
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
        }

      /*  private string GetAllPortfolioIdsOfGroupCustomer(int customerId)
        {
             CustomerBo customerBo = new CustomerBo();
             string allPortFolioIds;
             StringBuilder strAllPortfolioIds = new StringBuilder();
            if (!string.IsNullOrEmpty(customerId.ToString()))
            {
                CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
                DataTable dt = customerFamilyBo.GetAllCustomerAssociates(customerId));
                if (dt != null && dt.Rows.Count > 0)
                {
                   
                    //strCustomers.Append("<table border='0'>");

                    //strCustomers.Append("<tr><td colspan='3'><b>All Customers Under Group Head :</b></td></tr>");
                    ////strCustomers.Append("<tr><td>Customer Name</td><td>Customer Id</td><td>&nbsp;</td></tr>");

                    foreach (DataRow dr in dt.Rows)
                    {
                        //strCustomers.Append("<tr>");
                        //strCustomers.Append("<td>" + dr["CustomerName"].ToString() + "</td>");
                        //strCustomers.Append("<td>" + dr["C_AssociateCustomerId"].ToString() + "</td>");
                        //strCustomers.Append("<td>" + ShowGroupFolios(Convert.ToInt32(dr["C_AssociateCustomerId"])) + "</td>");
                        //strCustomers.Append("</tr>");
                        strAllPortfolioIds.Append(GetAllPortfolioOfCustomer(Convert.ToInt32(dr["C_AssociateCustomerId"].ToString())));
                        strAllPortfolioIds.Append(",");
                    }
                   
                }
                
                //DataRow dr = dt.Rows[0];

                //txtPanParent.Text = dr["C_PANNum"].ToString();
                //trCustomerDetails.Visible = true;
                //trPortfolioDetails.Visible = true;
                //ShowFolios();
            }
            allPortFolioIds=Convert.ToString(strAllPortfolioIds);
             if (allPortFolioIds.EndsWith(","))
                allPortFolioIds = allPortFolioIds.Substring(0, allPortFolioIds.Length - 1);
            return allPortFolioIds;
           
        }

        private string GetAllPortfolioOfACustomer(int customerId)
        {
            string portfolioIDs = "";
            PortfolioBo portfolioBo = new PortfolioBo();
            if (!String.IsNullOrEmpty(customerId.ToString())) 
            {
                //int customerId = Convert.ToInt32(txtParentCustomerId.Value);
                List<CustomerPortfolioVo> customerPortfolioVos = portfolioBo.GetCustomerPortfolios(customerId); //Get all the portfolios of the selected customer.
                if (customerPortfolioVos != null && customerPortfolioVos.Count > 0) //One or more folios available for selected customer
                {

                    foreach (CustomerPortfolioVo custPortfolio in customerPortfolioVos)
                    {

                        portfolioIDs = portfolioIDs + custPortfolio.PortfolioId;
                        portfolioIDs = portfolioIDs + ",";


                    //    if (Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$ddlPortfolioGroup"] == "ALL")
                    //    {
                    //        portfolioIDs = portfolioIDs + custPortfolio.PortfolioId;
                    //        portfolioIDs = portfolioIDs + ",";

                    //    }
                    //    else if (Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$ddlPortfolioGroup"] == "MANAGED")
                    //    {
                    //        if (custPortfolio.PortfolioName == "MyPortfolio")
                    //        {
                    //            portfolioIDs = portfolioIDs + custPortfolio.PortfolioId;
                    //            portfolioIDs = portfolioIDs + ",";
                    //        }
                    //    }
                    //    else if (Request.Form["ctrl_MFReports$tabViewAndEmailReports$tabpnlViewReports$ddlPortfolioGroup"] == "UN_MANAGED")
                    //    {
                    //        if (custPortfolio.PortfolioName != "MyPortfolio")
                    //        {
                    //            portfolioIDs = portfolioIDs + custPortfolio.PortfolioId;
                    //            portfolioIDs = portfolioIDs + ",";
                    //        }

                    //    }
                    //    //checkbox.Append("<input type='checkbox' checked name='chk--" + custPortfolio.PortfolioId + "' id='chk--" + custPortfolio.PortfolioId + "'>" + custPortfolio.PortfolioName);
                    //    //checkboxList.Items.Add(new ListItem(custPortfolio.PortfolioName, custPortfolio.PortfolioId.ToString()));
                    }

                }

            }

            if (portfolioIDs.EndsWith(","))
                portfolioIDs = portfolioIDs.Substring(0, portfolioIDs.Length - 1);
            return portfolioIDs;


        }


        */





    }
}
