using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoReports;
using BoCommon;


namespace DaoReports
{
    public class PortfolioReportsDao
    {

        /// <summary>
        /// Get the Asset and Liability details for a customer.
        /// </summary>
        /// <param name="report"></param>
        /// <remarks>Porfolio is per Portfolio but Liability is per Customer.</remarks>
        /// <returns></returns>
        public DataSet GetPortfolioSummary(PortfolioReportVo report, int adviserId)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_RPT_GetPortfolioSummary");
                db.AddInParameter(cmd, "@PortfolioIds", DbType.String, report.PortfolioIds);
                db.AddInParameter(cmd, "@StartDate", DbType.DateTime, DateBo.GetPreviousMonthLastDate(report.ToDate));
                db.AddInParameter(cmd, "@EndDate", DbType.DateTime, report.ToDate);
                db.AddInParameter(cmd, "@AdviserId", DbType.Int32, adviserId);
                
                ds = db.ExecuteDataSet(cmd);
                DataSet dsLiablities = GetLiabilities(report.PortfolioIds); //Get liability details (Customerwise)
                if (dsLiablities != null && dsLiablities.Tables[0].Rows.Count > 0 ) //Add liabilities data table to Portfolio dataset
                {
                    dsLiablities.Tables[0].TableName = "Liabilities";
                    ds.Tables.Add(dsLiablities.Tables[0].Copy());
                }
                else //If no liabilities present add a dummy liability row to avoid errors in Crystal Report.
                {
                    DataTable dtLiabilities = new DataTable();
                    dtLiabilities.Columns.Add("CustomerId", Type.GetType("System.Int64"));
                    dtLiabilities.Columns.Add("LoanType");
                    dtLiabilities.Columns.Add("LoanAmount", Type.GetType("System.Int64"));
                    dtLiabilities.Columns.Add("CustomerName");
                    DataRow drLiabilities = dtLiabilities.NewRow();
                    drLiabilities["CustomerId"] = "-1"; //customer Id is -1 so that it will not be displayed in report.
                    drLiabilities["CustomerName"] = "CustomerName";
                    drLiabilities["LoanType"] = "LoanType";
                    drLiabilities["LoanAmount"] = 0;
                    dtLiabilities.Rows.Add(drLiabilities);
                    ds.Tables.Add(dtLiabilities);
                   
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "Reports.cs:GetPortfolioSummary()");

                object[] objects = new object[1];
                objects[0] = report;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        /// <summary>
        /// Get all the liabilities of the customer
        /// </summary>
        /// <param name="customerIds"></param>
        /// <returns></returns>
        public DataSet GetLiabilities(string customerIds)
        {
            Database db;
            DbCommand cmdGetLiabilities;
            DataSet dsGetLiabilities;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetLiabilities = db.GetStoredProcCommand("SP_RPT_GetLiabilities");
                db.AddInParameter(cmdGetLiabilities, "@PortfolioIds", DbType.String, customerIds);
                dsGetLiabilities = db.ExecuteDataSet(cmdGetLiabilities);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilitiesDao.cs:GetLiabilities()");
                object[] objects = new object[1];
                objects[0] = customerIds;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetLiabilities;
        }

    }
}
