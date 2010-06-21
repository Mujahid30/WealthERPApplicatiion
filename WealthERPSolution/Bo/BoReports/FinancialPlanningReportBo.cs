using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

using System.Collections.Specialized;
using VoReports;
using DaoReports;


namespace BoReports
{
    public class FinancialPlanningReportsBo
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public DataSet GetFinancialPlanningReport(FinancialPlanningVo report)
        {
            FinancialPlanningReportsDao financialPlanningReports  = new FinancialPlanningReportsDao();
            return financialPlanningReports.GetFinancialPlanningReport(report);
        }

     

    }
}
