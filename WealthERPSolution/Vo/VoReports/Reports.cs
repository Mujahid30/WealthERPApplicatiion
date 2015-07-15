using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoReports
{
    public struct MFReportVo
    {

        public string ReportName;
        public int AdviserId;
        public int RMId;
        public int CustomerId;
        public int GroupHeadId;
        public string CustomerName;
        public string CustomerIds;
        public string PortfolioIds;
        public string GroupHead;
        public string Type;
        public string SubType;
        public DateTime FromDate;
        public DateTime ToDate;
        public string OrderBy;
        public string FilterBy;

    }
    public struct MFReportEmailVo
    {
        public int AdviserId;
        public int CustomerId;
        public string RMEmail;
        public string CustomerEmail;
        public string ReportTypeName;

    }

    public struct EquityReportVo
    {
        public string CustomerName;
        public string CustomerIds;
        public string PortfolioIds;
        public string GroupHead;
        public string Type;
        public string SubType;
        public DateTime FromDate;
        public DateTime ToDate;
        public string isSpeculative;

    }
    public struct PortfolioReportVo
    {
        public string CustomerName;
        public string CustomerIds;
        public string PortfolioIds;
        public string GroupHead;
        public string Type;
        public string SubType;
        public DateTime FromDate;
        public DateTime ToDate;

    }
    public struct FinancialPlanningVo
    {
        public string CustomerName;
        public string CustomerId;
        public int isProspect;
        public int advisorId;
    }
    public struct FPOfflineFormVo
    {
        public int advisorId;
    }
    public struct OrderTransactionSlipVo
    {
        public int advisorId;
        public int CustomerId;
        public string amcCode;
        public string FolioNo;
        public string Type;
        public int SchemeCode;
        public int orderId;
        public string accountId;
        public string portfolioId;
        //------------- For new Order  Transaction Report---------------
        public string Scheme;
        public string BankName;
        public string BranchName;
        public string Amount;
        public string StartDateSIP;
        public string StartDateSTP;
        public string NewAmount;
        public string EndDateSIP;
        public string EndDateSTP;
        public bool UnitsChk;
        public bool AmountsChk;
        public string  ChequeDate;
        public string ChequeNo;
        public string SchemeSwitch;
        public string ArnNo;
        
    }

    public enum ReportType
    {
        Invalid = 0,
        MFReports = 1,
        EquityReports = 2,
        PortfolioReports = 3,
        FinancialPlanning = 4,
        FinancialPlanningSectional=5,
        FPOfflineForm=6,
        OrderTransactionSlip=7,
        WelcomeNote=8
    }

    public enum ReportServerType
    {
        Invalid = 0,
       AssociateBrokerageComissionReport = 100,
        //AssociateBrokrageComissionDetailsReport=101
    }

    public struct ReportServerConfigVo
    {
        public string ReportServerURL;
        public string UserName;
        public string Password;
        public string Domain;
        public string ReportPath;

    }


}
