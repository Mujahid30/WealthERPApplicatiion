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
using BoCalculator;
using VoCustomerPortfolio;
using BoCustomerPortfolio;

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
                cmd.CommandTimeout = 60 * 60;
                ds = db.ExecuteDataSet(cmd);
                DataSet dsLiablities = GetLiabilities(report.PortfolioIds); //Get liability details (Customerwise)
                if (dsLiablities != null && dsLiablities.Tables[0].Rows.Count > 0) //Add liabilities data table to Portfolio dataset
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
            int tempId = 0;
            Calculator calculator = new Calculator();
            List<LiabilitiesVo> liabilitiesVoList = new List<LiabilitiesVo>();
            LiabilitiesVo liabilityVo = new LiabilitiesVo();
            LiabilitiesBo liabilitiesBo = new LiabilitiesBo();
            double liabilityValue = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetLiabilities = db.GetStoredProcCommand("SP_RPT_GetLiabilities");
                db.AddInParameter(cmdGetLiabilities, "@PortfolioIds", DbType.String, customerIds);
                cmdGetLiabilities.CommandTimeout = 60 * 60;

                dsGetLiabilities = db.ExecuteDataSet(cmdGetLiabilities);
                if (dsGetLiabilities.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsGetLiabilities.Tables[0].Rows.Count; i++)
                    {
                        liabilityValue = 0;
                        liabilityVo = liabilitiesBo.GetLiabilityDetails(int.Parse(dsGetLiabilities.Tables[0].Rows[i]["CL_LiabilitiesId"].ToString()));
                        if (liabilityVo.PaymentOptionCode == 1)
                        {
                            liabilityValue = liabilityValue + calculator.GetLoanOutstanding(liabilityVo.CompoundFrequency, liabilityVo.LoanAmount, liabilityVo.InstallmentStartDate, liabilityVo.InstallmentEndDate, 1, liabilityVo.LumpsumRepaymentAmount, liabilityVo.NoOfInstallments);
                        }
                        else if (liabilityVo.PaymentOptionCode == 2)
                        {
                            liabilityValue = liabilityValue + calculator.GetLoanOutstanding(liabilityVo.FrequencyCodeEMI, liabilityVo.LoanAmount, liabilityVo.InstallmentStartDate, liabilityVo.InstallmentEndDate, 2, liabilityVo.EMIAmount, liabilityVo.NoOfInstallments);
                        }
                        dsGetLiabilities.Tables[0].Rows[i]["LoanAmount"] = liabilityValue.ToString();
                    }
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


        /// <summary>
        /// Get the Asset and Liability details for a customer.
        /// </summary>
        /// <param name="report"></param>
        /// <remarks>Porfolio is per Portfolio but Liability is per Customer.</remarks>
        /// <returns></returns>
        public DataSet GetCustomerAssetAllocationDetails(PortfolioReportVo report, int adviserId,string reportType)
        {
            Database db;
            DbCommand cmd;
            DataSet dsAssetAllocation = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_RPT_GetCustomerAssetAllocation");
                db.AddInParameter(cmd, "@PortfolioIds", DbType.String, report.PortfolioIds);
                db.AddInParameter(cmd, "@AdviserId", DbType.Int32, adviserId);
                if (!string.IsNullOrEmpty(reportType))
                    db.AddInParameter(cmd, "@ReportType", DbType.String, reportType);

                cmd.CommandTimeout = 60 * 60;
                dsAssetAllocation = db.ExecuteDataSet(cmd);                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "Reports.cs:GetCustomerAssetAllocationDetails()");

                object[] objects = new object[1];
                objects[0] = report;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAssetAllocation;
        }

    }
}
