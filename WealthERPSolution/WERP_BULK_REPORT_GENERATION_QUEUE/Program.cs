using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoReports;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace WERP_BULK_REPORT_GENERATION_QUEUE
{
    public class Program
    {

        
        static void Main(string[] args)
        {
            
            string adviserLogoPath = ConfigurationSettings.AppSettings["ADVISER_LOGO_PATH"].ToString();
            string reportFilePath = ConfigurationSettings.AppSettings["REPORT_RPT_FILE_PATH"].ToString();
            string reportSavePath = ConfigurationSettings.AppSettings["REPORT_SAVE_PATH"].ToString();
            BulkReportRenerationBo bulkReportRenerationBo = new BulkReportRenerationBo(reportFilePath, adviserLogoPath, reportSavePath);
            bulkReportRenerationBo.BulkReportProcessor();
            //int adviserId = 1064;
            //int rmId = 1097;
            //int customerId = 22728;
            //string portfolioIds = "22728," + "35848," + "36122";
            //DateTime dtfrom = DateTime.Now.AddYears(-2);
            //MFReportVo mfReportVo = new MFReportVo();
            //mfReportVo.CustomerIds = customerId.ToString();
            //mfReportVo.PortfolioIds = portfolioIds;
            //mfReportVo.SubType = "CAPITAL_GAIN_SUMMARY";
            //mfReportVo.FromDate = dtfrom;
            //mfReportVo.ToDate = DateTime.Now.AddDays(-1);
            //bulkReportRenerationBo.DisplayReport(mfReportVo,reportFilePath,adviserLogoPath,reportSavePath,adviserId,rmId,customerId);
            
            

        }

       

    }
}
