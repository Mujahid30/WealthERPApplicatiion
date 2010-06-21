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
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_RPT_GetFinancialPlanningReport");
                db.AddInParameter(cmd, "@CustomerId", DbType.String, report.CustomerId);
                
                ds = db.ExecuteDataSet(cmd);
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

     

    }
}
