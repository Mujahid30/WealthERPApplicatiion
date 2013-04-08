using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoReports;
using BoCommon;
using BoCustomerProfiling;
using VoUser;
using CrystalDecisions.Shared;
using System.Net.Mail;
using BoAdvisorProfiling;
using VoAdvisorProfiling;
//using PCGMailLib;
using System.Text;
//using BoCustomerProfiling;
using System.IO;
using System.Data;
using BoReports;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections;
//using VoEmailSMS;
using System.Net.Mime;
//using BoCustomerPortfolio;
//using VoCustomerPortfolio;
//using WealthERP.Base;
//using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoReports;
using BoCustomerPortfolio;



namespace WERP_BULK_REPORT_GENERATION_QUEUE
{
    public class BulkReportRenerationBo
    {
        ReportDocument crmain;
        AdvisorVo advisorVo = null;
        RMVo rmVo = new RMVo();
        RMVo customerRMVo = new RMVo();
        AdvisorStaffBo adviserStaffBo = new AdvisorStaffBo();
        CustomerVo customerVo = new CustomerVo();
        string daemonCode;
        BulkReportGenerationDao bulkReportGenerationDao = new BulkReportGenerationDao();
        MFReportVo mfReportVo;
        string reportFilePath, adviserLogoPath, savedLocation;
        string reportFileName = string.Empty;

        public BulkReportRenerationBo()
        {

        }

        public BulkReportRenerationBo(string filePath, string logoPath, string storeLocation)
        {
            daemonCode = Environment.MachineName + "BULKREPORT";
            Trace("Starting BULKREPORT processor...");

            reportFilePath = filePath;
            adviserLogoPath = logoPath;
            savedLocation = storeLocation;
        }

        private void Trace(string Msg)
        {
            Utils.Trace(daemonCode + ": " + Msg);
        }

        public void BulkReportProcessor()
        {
            DataTable dtBulkReportRequestList = bulkReportGenerationDao.GetTheBulkReportRequestList(daemonCode);
            foreach (DataRow dr in dtBulkReportRequestList.Rows)
            {
                int requestId = Convert.ToInt32(dr["WR_RequestId"].ToString());
                int logId = 0;
                try
                {
                    bulkReportGenerationDao.CreateTaskRequestLOG(requestId, out logId);
                    BulkReportSubProcessor(requestId);
                    bulkReportGenerationDao.UpdateTaskRequestLOG(logId, "SUCCESS");
                    bulkReportGenerationDao.UpdateTaskRequestStatus(requestId, 1);
                }
                catch (Exception ex)
                {
                    bulkReportGenerationDao.UpdateTaskRequestLOG(logId, ex.Message);
                }
                finally
                {


                }


            }
        }

        private void BulkReportSubProcessor(int parentRequestId)
        {
            int requestLogId = 0;
            DataSet dtBulkReportRequest = bulkReportGenerationDao.GetTheSubBulkReportRequestList(parentRequestId, daemonCode, out requestLogId);
            DataTable dtSubRequestList = dtBulkReportRequest.Tables[0];
            DataTable dtSubParameterValues = dtBulkReportRequest.Tables[1];
            try
            {

                foreach (DataRow dr in dtSubRequestList.Rows)
                {
                    int subRequestId = Convert.ToInt32(dr["WR_SubRequestId"].ToString());
                    try
                    {
                        reportFileName = string.Empty;
                        DataView dvRReportParamerValues = new DataView(dtSubParameterValues, "WR_RequestId='" + subRequestId.ToString() + "'", "WR_RequestId", DataViewRowState.CurrentRows);
                        ProcessBulkReport(dvRReportParamerValues.ToTable());
                        bulkReportGenerationDao.InsertRequestOutputData(subRequestId, reportFileName);
                        bulkReportGenerationDao.UpdateTaskRequestAndLOG(subRequestId, requestLogId, "SUCCESS");
                        bulkReportGenerationDao.UpdateTaskRequestStatus(subRequestId, 1);
                    }
                    catch (Exception ex)
                    {
                        bulkReportGenerationDao.UpdateTaskRequestAndLOG(subRequestId, requestLogId, ex.Message);

                    }
                    finally
                    {

                    }

                }
                bulkReportGenerationDao.UpdateTaskRequestAndLOG(parentRequestId, requestLogId, "SUCCESS");
            }
            catch (Exception ex)
            {
                bulkReportGenerationDao.UpdateTaskRequestAndLOG(parentRequestId, requestLogId, ex.Message);

            }
            finally
            {

            }


        }

        private void ProcessBulkReport(DataTable dtReportParamerValues)
        {
            mfReportVo = new MFReportVo();
            mfReportVo = FillReportParamerValues(dtReportParamerValues);
            ExportToPDF(mfReportVo);

        }

        private MFReportVo FillReportParamerValues(DataTable dtReportParamerValues)
        {
            mfReportVo = new MFReportVo();
            foreach (DataRow dr in dtReportParamerValues.Rows)
            {

                switch (dr["WP_ParameterCode"].ToString())
                {
                    case "STARTDT":
                        mfReportVo.FromDate = DateTime.Parse(dr["WRD_InputParameterValue"].ToString());
                        break;
                    case "ENDDT":
                        mfReportVo.ToDate = DateTime.Parse(dr["WRD_InputParameterValue"].ToString());
                        break;
                    case "RT":
                        mfReportVo.SubType = dr["WRD_InputParameterValue"].ToString();
                        break;
                    case "AID":
                        mfReportVo.AdviserId = int.Parse(dr["WRD_InputParameterValue"].ToString());
                        break;
                    case "RMID":
                        mfReportVo.RMId = int.Parse(dr["WRD_InputParameterValue"].ToString());
                        break;
                    case "CID":
                        mfReportVo.CustomerIds += dr["WRD_InputParameterValue"].ToString() + ",";
                        break;
                    case "PID":
                        mfReportVo.PortfolioIds += dr["WRD_InputParameterValue"].ToString() + ",";
                        break;
                }
            }


            if (mfReportVo.CustomerIds.EndsWith(","))
                mfReportVo.CustomerIds = mfReportVo.CustomerIds.Substring(0, mfReportVo.CustomerIds.Length - 1);

            if (mfReportVo.PortfolioIds.EndsWith(","))
                mfReportVo.PortfolioIds = mfReportVo.PortfolioIds.Substring(0, mfReportVo.PortfolioIds.Length - 1);

            return mfReportVo;
        }

        private void setLogo(string logoPath)
        {
            string advisorLogo = "spacer.png";
            if (advisorVo.LogoPath != null && advisorVo.LogoPath != string.Empty)
                advisorLogo = advisorVo.LogoPath;

            string adviserLogoPath = logoPath + advisorLogo;
            if (!File.Exists(logoPath))
                adviserLogoPath = logoPath + @"\spacer.png";

            crmain.Database.Tables["Images"].SetDataSource(ImageTable(adviserLogoPath));

        }

        private static DataTable ImageTable(string ImageFile)
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

        private void AssignReportViewerProperties()
        {

            string state = "";

            try
            {
                //string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                //setLogo();
                //if (advisorVo.State != null)
                //    state = CommonReport.GetState(path, advisorVo.State);

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

                string formatstring = "";
                if (!string.IsNullOrEmpty(customerVo.Adr1Line1.Trim()))
                    formatstring = customerVo.Adr1Line1.Trim();
                //array[0] = customerVo.Adr1Line1.Trim();
                if (!string.IsNullOrEmpty(customerVo.Adr1Line2.Trim()))
                {
                    if (formatstring == "")
                    {
                        formatstring = customerVo.Adr1Line2.Trim();
                    }
                    else
                    {
                        formatstring = formatstring + "," + customerVo.Adr1Line2.Trim();
                        //array[1] = customerVo.Adr1Line2.Trim();
                    }
                }
                if (!string.IsNullOrEmpty(customerVo.Adr1City.Trim()))
                    if (formatstring == "")
                    {
                        formatstring = customerVo.Adr1City.Trim();
                    }
                    else
                    {
                        formatstring = formatstring + "," + customerVo.Adr1City.Trim();
                        //array[1] = customerVo.Adr1Line2.Trim();
                    }
                if (!string.IsNullOrEmpty(customerVo.Adr1PinCode.ToString()))
                    if (formatstring == "")
                    {
                        formatstring = customerVo.Adr1PinCode.ToString();
                    }
                    else
                    {
                        formatstring = formatstring + "," + customerVo.Adr1PinCode;
                        //array[1] = customerVo.Adr1Line2.Trim();
                    }
                crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                crmain.SetParameterValue("CustomerAddress", formatstring);
                crmain.SetParameterValue("CustomerEmail", "Email :  " + customerVo.Email);
                crmain.SetParameterValue("Organization", advisorVo.OrganizationName);




            }
            catch (Exception ex)
            {

            }


        }

        private bool ExportToPDF(MFReportVo reportVo)
        {
            MFReportsBo mfReports = new MFReportsBo();
            CustomerBo customerBo = new CustomerBo();
            AdvisorBo advisorBo = new AdvisorBo();
            ////////////////////////////report = (MFReportVo)Session["reportParams"];
            //customerVo = (CustomerVo)Session["CusVo"];
            //if (Session["CusVo"] != null)
            //    customerVo = (CustomerVo)Session["CusVo"];
            string fileExtension = ".pdf";            
            string finalReportPath = string.Empty;
            string exportReportFullPath = string.Empty;
            crmain = new ReportDocument();
            try
            {


                advisorVo = advisorBo.GetAdvisor(reportVo.AdviserId);
                customerVo = customerBo.GetCustomer(Convert.ToInt32(reportVo.CustomerIds));
                customerRMVo = adviserStaffBo.GetAdvisorStaffDetails(reportVo.RMId);

                switch (reportVo.SubType)
                {

                    case "CAPITAL_GAIN_SUMMARY":
                        {
                            finalReportPath = reportFilePath + @"\CapitalGainSummary.rpt";
                            crmain.Load(finalReportPath);
                            DataTable dtCapitalGainSummary = mfReports.GetCapitalGainSummaryReport(reportVo);
                            if (dtCapitalGainSummary.Rows.Count > 0)
                            {
                                crmain.SetDataSource(dtCapitalGainSummary);
                                setLogo(adviserLogoPath);

                                crmain.SetParameterValue("DateRange", "Period: " + reportVo.FromDate.ToShortDateString() + " to " + reportVo.ToDate.ToShortDateString());
                                //crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                                //crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                                AssignReportViewerProperties();
                                reportFileName =  reportVo.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                                exportReportFullPath = savedLocation + @"/" + reportFileName;
                                crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportReportFullPath);
                            }
                            break;
                        }
                    case "CAPITAL_GAIN_DETAILS":
                        {
                            finalReportPath = reportFilePath + @"\CapitalGainDetails.rpt";
                            crmain.Load(finalReportPath);
                            DataTable dtCapitalGainDetails = mfReports.GetCapitalGainDetailsReport(reportVo);
                            if (dtCapitalGainDetails.Rows.Count > 0)
                            {
                                crmain.SetDataSource(dtCapitalGainDetails);
                                setLogo(adviserLogoPath);
                                crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                                crmain.SetParameterValue("DateRange", "Period: " + reportVo.FromDate.ToShortDateString() + " to " + reportVo.ToDate.ToShortDateString());
                                //crmain.SetParameterValue("FromDate", report.FromDate.ToShortDateString());
                                //crmain.SetParameterValue("ToDate", report.ToDate.ToShortDateString());
                                AssignReportViewerProperties();
                                reportFileName = reportVo.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                                exportReportFullPath = savedLocation + @"/" + reportFileName;
                                crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportReportFullPath);
                            }
                            break;
                        }


                    case "CATEGORY_WISE":
                        {
                            finalReportPath = reportFilePath + @"\MFFundSummary.rpt";
                            crmain.Load(finalReportPath);
                            DataSet dsMFFundSummary = mfReports.GetMFFundSummaryReport(reportVo, advisorVo.advisorId);
                            if (dsMFFundSummary.Tables[0].Rows.Count > 0 || dsMFFundSummary.Tables[1].Rows.Count > 0)
                            {
                                crmain.Subreports["OpenPositionReport"].Database.Tables[0].SetDataSource(dsMFFundSummary.Tables[0]);
                                crmain.Subreports["AllPositionReport1"].Database.Tables[0].SetDataSource(dsMFFundSummary.Tables[1]);
                                crmain.Subreports["AllPositionReport2"].Database.Tables[0].SetDataSource(dsMFFundSummary.Tables[1]);
                                setLogo(adviserLogoPath);
                                crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                                crmain.SetParameterValue("DateRange", "As on: " + reportVo.ToDate.ToShortDateString());
                                crmain.SetParameterValue("FromDate", reportVo.FromDate.ToShortDateString());
                                crmain.SetParameterValue("ToDate", reportVo.FromDate.ToShortDateString());
                                crmain.SetParameterValue("PreviousMonthDate", DateBo.GetPreviousMonthLastDate(reportVo.FromDate).ToShortDateString());
                                crmain.SetParameterValue("AsOnDate", reportVo.ToDate.ToShortDateString());
                                AssignReportViewerProperties();
                                reportFileName = reportVo.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                                exportReportFullPath = savedLocation + @"/" + reportFileName;
                                crmain.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, exportReportFullPath);
                            }

                            break;
                        }

                    case "TRANSACTION_REPORT":
                        {
                            finalReportPath = reportFilePath + @"\MFTransactions.rpt";
                            crmain.Load(finalReportPath);
                            DataTable dtTransactions = mfReports.GetTransactionReport(reportVo);
                            if (dtTransactions.Rows.Count > 0)
                            {
                                crmain.SetDataSource(dtTransactions);
                                setLogo(adviserLogoPath);
                                crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                                //if (!String.IsNullOrEmpty(dtTransactions.Rows[0]["CustomerName"].ToString()))
                                // crmain.SetParameterValue("CustomerName", "Cust");
                                crmain.SetParameterValue("DateRange", "Period: " + reportVo.FromDate.ToShortDateString() + " to " + reportVo.ToDate.ToShortDateString());
                                crmain.SetParameterValue("FromDate", reportVo.FromDate.ToShortDateString());
                                crmain.SetParameterValue("ToDate", reportVo.ToDate.ToShortDateString());

                                AssignReportViewerProperties();
                                reportFileName = reportVo.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                                exportReportFullPath = savedLocation + @"/" + reportFileName;
                                crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportReportFullPath);
                            }
                            break;
                        }
                    case "DIVIDEND_STATEMENT":
                        {
                            finalReportPath = reportFilePath + @"\MFDividend.rpt";
                            crmain.Load(finalReportPath);
                            DataTable dtDividend = mfReports.GetDivdendReport(reportVo);
                            if (dtDividend.Rows.Count > 0)
                            {
                                crmain.SetDataSource(dtDividend);
                                setLogo(adviserLogoPath);
                                crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                                //if (!String.IsNullOrEmpty(dtDividend.Rows[0]["Name"].ToString()))
                                //crmain.SetParameterValue("CustomerName", "--");
                                crmain.SetParameterValue("DateRange", "Period: " + reportVo.FromDate.ToShortDateString() + " to " + reportVo.ToDate.ToShortDateString());
                                crmain.SetParameterValue("FromDate", reportVo.FromDate.ToShortDateString());
                                crmain.SetParameterValue("ToDate", reportVo.ToDate.ToShortDateString());
                                AssignReportViewerProperties();
                                reportFileName = reportVo.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                                exportReportFullPath = savedLocation + @"/" + reportFileName;
                                crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportReportFullPath);
                            }
                            break;
                        }

                    case "RETURNS_PORTFOLIO":
                        {
                            finalReportPath = reportFilePath + @"\MFReturns.rpt";
                            crmain.Load(finalReportPath);
                            //DataTable dtDividendReturnHolding = mfReports.GetDivdendReport(reportVo);
                            CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
                            DataTable dtReturnsPortfolio = mfReports.GetReturnSummaryReport(reportVo, advisorVo.advisorId);

                            DataTable dtPortfolioXIRR = customerPortfolioBo.GetCustomerPortfolioLabelXIRR(reportVo.PortfolioIds);
                            dtPortfolioXIRR = GetAbsolutereturnToXIRRDt(dtPortfolioXIRR, dtReturnsPortfolio);

                            if (dtReturnsPortfolio.Rows.Count > 0)
                            {
                                crmain.SetDataSource(dtReturnsPortfolio);
                                setLogo(adviserLogoPath);
                                crmain.Subreports["PortfolioXIRR"].Database.Tables["PortfolioXIRR"].SetDataSource(dtPortfolioXIRR);
                                crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                                //if (!String.IsNullOrEmpty(dtDividend.Rows[0]["Name"].ToString()))
                                //crmain.SetParameterValue("CustomerName", "--");
                                crmain.SetParameterValue("AsOnDate", reportVo.FromDate.ToShortDateString());
                                crmain.SetParameterValue("DateRange", "As on: " + reportVo.ToDate.ToShortDateString());
                                AssignReportViewerProperties();
                                reportFileName = reportVo.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                                exportReportFullPath = savedLocation + @"/" + reportFileName;
                                crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportReportFullPath);
                            }

                            break;
                        }
                    case "COMPREHENSIVE":
                        {
                            finalReportPath = reportFilePath + @"\ComprehensiveMFReport.rpt";
                            crmain.Load(finalReportPath);
                            CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
                            DataSet dsReturnsPortfolio = mfReports.GetPortfolioAnalyticsReport(reportVo, advisorVo.advisorId);
                            DataTable dtReturnsPortfolio = dsReturnsPortfolio.Tables[0];

                            DataTable dtPortfolioXIRRComp = customerPortfolioBo.GetCustomerPortfolioLabelXIRR(reportVo.PortfolioIds);

                            dtReturnsPortfolio = dsReturnsPortfolio.Tables[1];
                            DataTable dtPortfolioXIRR = GetAbsolutereturnToXIRRDt(dtPortfolioXIRRComp, dtReturnsPortfolio);
                            if (dsReturnsPortfolio.Tables[0].Rows.Count > 0)
                            {
                                crmain.SetDataSource(dsReturnsPortfolio.Tables[0]);
                                crmain.Subreports["Portfolio_XIRR"].Database.Tables["PortfolioXIRR"].SetDataSource(dtPortfolioXIRRComp);
                                //crmain.Subreports["MFSchemePerformance"].Database.Tables[0].SetDataSource(dsReturnsPortfolio.Tables[1]);
                                //crmain.Subreports["MFTopTenHoldings"].Database.Tables[0].SetDataSource(dsReturnsPortfolio.Tables[2]);
                                //crmain.Subreports["MFTopTenSectors"].Database.Tables[0].SetDataSource(dsReturnsPortfolio.Tables[5]);

                                setLogo(adviserLogoPath);
                                crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                                crmain.SetParameterValue("AsOnDate", reportVo.FromDate.ToShortDateString());
                                crmain.SetParameterValue("DateRange", "As on: " + reportVo.ToDate.ToShortDateString());
                                AssignReportViewerProperties();
                                reportFileName = reportVo.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                                exportReportFullPath = savedLocation + @"/" + reportFileName;
                                crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportReportFullPath);

                            }

                            break;
                        }

                    case "DIVIDEND_SUMMARY":
                        {
                            finalReportPath = reportFilePath + @"\MFDividendSummary.rpt";
                            crmain.Load(finalReportPath);
                            DataTable dtDividendSummary = mfReports.GetDivdendReport(reportVo);
                            //customerVo = (CustomerVo)Session["CusVo"];
                            if (dtDividendSummary.Rows.Count > 0)
                            {
                                crmain.SetDataSource(dtDividendSummary);
                                setLogo(adviserLogoPath);
                                //if (!String.IsNullOrEmpty(dtDividend.Rows[0]["Name"].ToString()))
                                crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                                crmain.SetParameterValue("DateRange", "Period: " + reportVo.FromDate.ToShortDateString() + " to " + reportVo.ToDate.ToShortDateString());
                                crmain.SetParameterValue("FromDate", reportVo.FromDate.ToShortDateString());
                                crmain.SetParameterValue("ToDate", reportVo.ToDate.ToShortDateString());

                                AssignReportViewerProperties();
                                reportFileName = reportVo.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                                exportReportFullPath = savedLocation + @"/" + reportFileName;
                                crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportReportFullPath);
                            }

                            break;
                        }
                    //Added Three more cases for Display three new report : Author-Pramod
                    case "RETURNS_PORTFOLIO_REALIZED":
                        {
                            finalReportPath = reportFilePath + @"\MFReturnsRealized.rpt";
                            crmain.Load(finalReportPath);
                            DataTable dtReturnsREPortfolio = mfReports.GetMFReturnRESummaryReport(reportVo, advisorVo.advisorId);
                            if (dtReturnsREPortfolio.Rows.Count > 0)
                            {
                                crmain.SetDataSource(dtReturnsREPortfolio);
                                setLogo(adviserLogoPath);
                                crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                                crmain.SetParameterValue("DateRange", "As on: " + reportVo.ToDate.ToShortDateString());
                                crmain.SetParameterValue("AsOnDate", reportVo.FromDate.ToShortDateString());
                                AssignReportViewerProperties();
                                reportFileName = reportVo.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                                exportReportFullPath = savedLocation + @"/" + reportFileName;
                                crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportReportFullPath);
                            }

                            break;
                        }

                    case "ELIGIBLE_CAPITAL_GAIN_DETAILS":
                        {
                            finalReportPath = reportFilePath + @"\EligibleCapitalGainsDetails.rpt";
                            crmain.Load(finalReportPath);
                            DataTable dtEligibleCapitalGainsDetails = mfReports.GetEligibleCapitalGainDetailsReport(reportVo);
                            if (dtEligibleCapitalGainsDetails.Rows.Count > 0)
                            {
                                crmain.SetDataSource(dtEligibleCapitalGainsDetails);
                                setLogo(adviserLogoPath);
                                crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                                crmain.SetParameterValue("DateRange", "As on: " + reportVo.ToDate.ToShortDateString());
                                //crmain.SetParameterValue("AsOnDate", report.FromDate.ToShortDateString());
                                AssignReportViewerProperties();
                                reportFileName = reportVo.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                                exportReportFullPath = savedLocation + @"/" + reportFileName;
                                crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportReportFullPath);
                            }

                            break;
                        }
                    case "ELIGIBLE_CAPITAL_GAIN_SUMMARY":                 
                        {
                            finalReportPath = reportFilePath + @"\EligibleCapitalGainsDetails.rpt";
                            crmain.Load(finalReportPath);
                            DataTable dtEligibleCapitalGainsSummary = mfReports.GetEligibleCapitalGainDetailsReport(reportVo);
                            if (dtEligibleCapitalGainsSummary.Rows.Count > 0)
                            {
                                crmain.SetDataSource(dtEligibleCapitalGainsSummary);
                                setLogo(adviserLogoPath);
                                crmain.SetParameterValue("CustomerName", customerVo.FirstName + " " + customerVo.MiddleName + " " + customerVo.LastName);
                                //crmain.SetParameterValue("DateRange", "As on: " + report.ToDate.ToShortDateString());
                                crmain.SetParameterValue("DateRange", "As on: " + reportVo.ToDate.ToShortDateString());
                                AssignReportViewerProperties();
                                reportFileName = reportVo.SubType + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
                                exportReportFullPath = savedLocation + @"/" + reportFileName;
                                crmain.ExportToDisk(ExportFormatType.PortableDocFormat, exportReportFullPath);
                            }
                        }
                        break;

                }
                //Filling Emails
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (!string.IsNullOrEmpty(reportFileName))
            {
                return true;
            }
            else
                return false;
        }

        private DataTable GetAbsolutereturnToXIRRDT(DataTable dtPortfolioXIRR, DataTable dtReturnsPortfolio)
        {
            try
            {
                dtPortfolioXIRR.Columns.Add("AbsoluteReturn", typeof(Int64));
                int portfolioId = 0;
                String NA = "NA";
                Double XIRR;
                DataRow[] drAbsolutereturn;
                DataRow[] drXirr;
                foreach (DataRow dr in dtPortfolioXIRR.Rows)
                {
                    portfolioId = Convert.ToInt32(dr["PortfolioId"].ToString());
                    //XIRR = Convert.ToDouble(dr["XIRR"].ToString());
                    drAbsolutereturn = dtReturnsPortfolio.Select("CP_PortfolioId=" + portfolioId.ToString());
                    //drXirr = dtReturnsPortfolio.Select("XIRR=" + XIRR.ToString());
                    foreach (DataRow drAbs in drAbsolutereturn)
                    {
                        dr["AbsoluteReturn"] = drAbs["absoluteReturn"];
                    }
                    //foreach (DataRow drXIrr in drXirr)
                    //{
                    //    if (drXIrr["XIRR"] == null)
                    //    {
                    //        dr["XIRR"] = "NA"; 
                    //    }
                    //    else
                    //    {
                    //        dr["XIRR"] = drXIrr["XIRR"];
                    //    }
                    //}


                }
                return dtPortfolioXIRR;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable GetAbsolutereturnToXIRRDt(DataTable dtPortfolioXIRR, DataTable dtReturnsPortfolio)
        {
            try
            {
                dtPortfolioXIRR.Columns.Add("AbsoluteReturn", typeof(Int64));
                int portfolioId = 0;
                String NA = "NA";
                Double XIRR;
                DataRow[] drAbsolutereturn;
                DataRow[] drXirr;
                foreach (DataRow dr in dtPortfolioXIRR.Rows)
                {
                    portfolioId = Convert.ToInt32(dr["PortfolioId"].ToString());
                    //XIRR = Convert.ToDouble(dr["XIRR"].ToString());
                    drAbsolutereturn = dtReturnsPortfolio.Select("CP_PortfolioId=" + portfolioId.ToString());
                    //drXirr = dtReturnsPortfolio.Select("XIRR=" + XIRR.ToString());
                    foreach (DataRow drAbs in drAbsolutereturn)
                    {
                        dr["AbsoluteReturn"] = drAbs["absoluteReturn"];
                    }
                    //foreach (DataRow drXIrr in drXirr)
                    //{
                    //    if (drXIrr["XIRR"] == null)
                    //    {
                    //        dr["XIRR"] = "NA"; 
                    //    }
                    //    else
                    //    {
                    //        dr["XIRR"] = drXIrr["XIRR"];
                    //    }
                    //}


                }
                return dtPortfolioXIRR;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }


}
