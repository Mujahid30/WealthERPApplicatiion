﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoReports
{
    public struct MFReportVo
    {
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
        public string Type;
        public string SchemeCode;
        public int orderId;
        public string accountId;
        public string portfolioId;
        public string amcCode;
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
        OrderTransactionSlip=7
    }


}
