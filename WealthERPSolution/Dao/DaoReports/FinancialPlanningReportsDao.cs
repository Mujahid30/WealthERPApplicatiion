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
    public class FinancialPlanningReportsDao
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public DataSet GetFinancialPlanningReport(FinancialPlanningVo report)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;
            DataTable dtCustomerDetails = new DataTable();
            DataTable dtSpouseDetails = new DataTable();
            DataTable dtChildrenDetails = new DataTable();
            DataTable dtTempChildrenDetails = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_RPT_GetFinancialPlanningReport");
                db.AddInParameter(cmd, "@CustomerId", DbType.String, report.CustomerId);
                cmd.CommandTimeout = 60 * 60;
                ds = db.ExecuteDataSet(cmd);
                dtCustomerDetails.Columns.Add("Name", typeof(string));
                dtCustomerDetails.Columns.Add("Dob", typeof(string));

                dtSpouseDetails.Columns.Add("Name", typeof(string));
                dtSpouseDetails.Columns.Add("Dob", typeof(string));

                dtTempChildrenDetails.Columns.Add("Name", typeof(string));
                dtTempChildrenDetails.Columns.Add("Dob", typeof(string));
                dtTempChildrenDetails.Columns.Add("YearEducation", typeof(string));
                dtTempChildrenDetails.Columns.Add("YearMarriage", typeof(string));

                dtChildrenDetails.Columns.Add("Name", typeof(string));
                dtChildrenDetails.Columns.Add("Dob", typeof(string));
                dtChildrenDetails.Columns.Add("YearEducation", typeof(string));
                dtChildrenDetails.Columns.Add("YearMarriage", typeof(string));

                if (ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        if (dr["RelationShipCode"].ToString() == "SELF")
                        {
                            DataRow drCustomer = dtCustomerDetails.NewRow();

                            drCustomer["Name"] = dr["Name"].ToString();
                            if (!string.IsNullOrEmpty(dr["DOB"].ToString().Trim()))
                                drCustomer["Dob"] = String.Format("{0:dd MMM yyyy}", DateTime.Parse(dr["DOB"].ToString().Trim()));
                            else
                                drCustomer["Dob"] = "-";
                            dtCustomerDetails.Rows.Add(drCustomer);

                        }
                        else if (dr["RelationShipCode"].ToString() == "SP")
                        {
                            DataRow drSpouse = dtSpouseDetails.NewRow();
                            if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                                drSpouse["Name"] = dr["Name"].ToString();
                            else
                                drSpouse["Name"] = "-";
                            if (!string.IsNullOrEmpty(dr["DOB"].ToString().Trim()))
                                drSpouse["Dob"] = String.Format("{0:dd MMM yyyy}", DateTime.Parse(dr["DOB"].ToString().Trim()));
                            else
                                drSpouse["Dob"] = "-";
                            dtSpouseDetails.Rows.Add(drSpouse);

                        }
                        else if (dr["RelationShipCode"].ToString() == "CH" && (dr["IsGoalActive"].ToString() == "1" || string.IsNullOrEmpty(dr["IsGoalActive"].ToString())))
                        {
                            DataRow drChild = dtTempChildrenDetails.NewRow();
                            if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                                drChild["Name"] = dr["Name"].ToString();
                            else
                                drChild["Name"] = "-";
                            if (!string.IsNullOrEmpty(dr["DOB"].ToString().Trim()))
                                drChild["Dob"] = String.Format("{0:dd MMM yyyy}", DateTime.Parse(dr["DOB"].ToString().Trim()));
                            else
                                drChild["Dob"] = "-";
                            if (!string.IsNullOrEmpty(dr["YearOfEducation"].ToString()))
                                drChild["YearEducation"] = dr["YearOfEducation"].ToString();
                            else
                                drChild["YearEducation"] = "-";
                            if (!string.IsNullOrEmpty(dr["YearOfMarriage"].ToString()))
                                drChild["YearMarriage"] = dr["YearOfMarriage"].ToString();
                            else
                                drChild["YearMarriage"] = "-";
                            dtTempChildrenDetails.Rows.Add(drChild);

                        }


                    }

                }
                if (dtTempChildrenDetails.Rows.Count > 0)
                {
                    for (int rowNo = 0; rowNo < dtTempChildrenDetails.Rows.Count; rowNo++)
                    {
                        DataRow drChild = dtChildrenDetails.NewRow();
                        if (rowNo != dtTempChildrenDetails.Rows.Count)
                        {
                            if (rowNo == dtTempChildrenDetails.Rows.Count - 1)
                            {
                                drChild["Name"] = dtTempChildrenDetails.Rows[rowNo]["Name"].ToString();
                                drChild["Dob"] = dtTempChildrenDetails.Rows[rowNo]["Dob"].ToString();
                                drChild["YearEducation"] = dtTempChildrenDetails.Rows[rowNo]["YearEducation"].ToString();
                                drChild["YearMarriage"] = dtTempChildrenDetails.Rows[rowNo]["YearMarriage"].ToString();
                                dtChildrenDetails.Rows.Add(drChild);

                            }
                            else
                            {
                                if (dtTempChildrenDetails.Rows[rowNo]["Name"].ToString().Trim() == dtTempChildrenDetails.Rows[rowNo + 1]["Name"].ToString().Trim())
                                {
                                    drChild["Name"] = dtTempChildrenDetails.Rows[rowNo]["Name"].ToString();
                                    drChild["Dob"] = dtTempChildrenDetails.Rows[rowNo]["Dob"].ToString();
                                    if (dtTempChildrenDetails.Rows[rowNo]["YearEducation"].ToString()!="-")
                                        drChild["YearEducation"] = dtTempChildrenDetails.Rows[rowNo]["YearEducation"].ToString();
                                    else
                                        drChild["YearEducation"] = dtTempChildrenDetails.Rows[rowNo + 1]["YearEducation"].ToString();
                                    if (dtTempChildrenDetails.Rows[rowNo]["YearMarriage"].ToString()!="-")
                                        drChild["YearMarriage"] = dtTempChildrenDetails.Rows[rowNo]["YearMarriage"].ToString();
                                    else
                                        drChild["YearMarriage"] = dtTempChildrenDetails.Rows[rowNo + 1]["YearMarriage"].ToString();

                                    dtChildrenDetails.Rows.Add(drChild);
                                    rowNo++;
                                }
                                else
                                {
                                    drChild["Name"] = dtTempChildrenDetails.Rows[rowNo]["Name"].ToString();
                                    drChild["Dob"] = dtTempChildrenDetails.Rows[rowNo]["Dob"].ToString();
                                    drChild["YearEducation"] = dtTempChildrenDetails.Rows[rowNo]["YearEducation"].ToString();
                                    drChild["YearMarriage"] = dtTempChildrenDetails.Rows[rowNo]["YearMarriage"].ToString();
                                    dtChildrenDetails.Rows.Add(drChild);


                                }
                            }
                        }


                    }

                    //int rowCount = 0;
                    //foreach (DataRow dr in dtTempChildrenDetails.Rows)
                    //{
                    //    if (rowCount == 0)
                    //    {

                    //    }
                    //    DataRow drChild = dtChildrenDetails.NewRow();


                    //}
                }

                ds.Tables.Add(dtCustomerDetails);
                ds.Tables.Add(dtSpouseDetails);
                ds.Tables.Add(dtChildrenDetails);



                //if (ds.Tables[0].Rows.Count < 1)
                //{
                //    DataTable dtGoal = new DataTable();
                //    dtGoal.Columns.Add("GoalId");
                //    dtGoal.Columns.Add("GoalName");
                //    dtGoal.Columns.Add("ChildName");
                //    dtGoal.Columns.Add("CostToday", System.Type.GetType("System.Double"));
                //    dtGoal.Columns.Add("MonthlySavingsRequired", System.Type.GetType("System.Double"));
                //    dtGoal.Columns.Add("CalculatedOn", System.Type.GetType("System.DateTime"));
                //    dtGoal.Columns.Add("Year");

                //    DataRow drGoal = ds.Tables[0].NewRow();
                //    drGoal["GoalId"] = -1;
                //    drGoal["GoalName"] = "Test";
                //    drGoal["ChildName"] = "Test";
                //    drGoal["CostToday"] = 0.00;
                //    drGoal["MonthlySavingsRequired"] = 0;
                //    drGoal["CalculatedOn"] = DateTime.MinValue;
                //    //drGoal["Year"] =0;
                //    //dtGoal.Rows.Add(drGoal);
                //    ds.Tables[0].Rows.Add(drGoal);

                //}

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "Reports.cs:GetFinancialPlanningReport()");

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
        /// Get the Asset and Liability details for a customer.
        /// </summary>
        /// <param name="report"></param>
        /// <remarks>Get All the details of Financial Planning of customers</remarks>
        /// <returns></returns>
        public DataSet GetCustomerFPDetails(FinancialPlanningVo report,out double assetTotal,out double liabilitiesTotal,out double netWorthTotal,out string riskClass,out double sumAssuredLI)
        {
            Database db;
            DbCommand cmdCustomerFPReportDetails;
            DataSet dsCustomerFPReportDetails = null;
            DataTable dtAsset = new DataTable();
            DataTable dtLiabilities = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCustomerFPReportDetails = db.GetStoredProcCommand("SP_RPT_GetFPReportDetails");
                db.AddInParameter(cmdCustomerFPReportDetails, "@AdvisorId", DbType.Int32,report.advisorId);
                db.AddInParameter(cmdCustomerFPReportDetails, "@CustomerId", DbType.Int32, report.CustomerId);
                db.AddOutParameter(cmdCustomerFPReportDetails,"@RiskClass", DbType.String,20);
                db.AddOutParameter(cmdCustomerFPReportDetails, "@InsuranceSUMAssured", DbType.Decimal, 20);
                db.AddOutParameter(cmdCustomerFPReportDetails, "@AssetTotal", DbType.Decimal, 20);
                dsCustomerFPReportDetails = db.ExecuteDataSet(cmdCustomerFPReportDetails);

                 Object riskClassObj = db.GetParameterValue(cmdCustomerFPReportDetails, "@RiskClass");
                 if (riskClassObj != DBNull.Value)
                     riskClass = db.GetParameterValue(cmdCustomerFPReportDetails, "@RiskClass").ToString();
                 else
                     riskClass = string.Empty;

                 Object objSumAssuredLI = db.GetParameterValue(cmdCustomerFPReportDetails, "@InsuranceSUMAssured");
                 if (objSumAssuredLI != DBNull.Value)
                     sumAssuredLI = double.Parse(db.GetParameterValue(cmdCustomerFPReportDetails, "@InsuranceSUMAssured").ToString());
                 else
                     sumAssuredLI = 0;

                 Object objAssetTotal = db.GetParameterValue(cmdCustomerFPReportDetails, "@AssetTotal");
                 if (objAssetTotal != DBNull.Value)
                     assetTotal = double.Parse(db.GetParameterValue(cmdCustomerFPReportDetails, "@AssetTotal").ToString());
                 else
                     assetTotal = 0;

                dtAsset = dsCustomerFPReportDetails.Tables[2];
                dtLiabilities = dsCustomerFPReportDetails.Tables[3];               
                liabilitiesTotal = double.Parse(dtLiabilities.Rows[0][0].ToString());
                netWorthTotal = assetTotal - liabilitiesTotal;
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

                object[] objects = new object[2];
                objects[0] = report.CustomerId;
                objects[0] = report.CustomerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsCustomerFPReportDetails;
        }
     

    }
}
