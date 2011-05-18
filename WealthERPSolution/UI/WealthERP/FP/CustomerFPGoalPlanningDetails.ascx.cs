using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoFPSuperlite;
using System.Data;
using VoUser;
using WealthERP.Base;
using System.Collections;
using System.Configuration;
using System.Collections.Specialized;
using BoCommon;
using VoFPSuperlite;
using VoUser;


namespace WealthERP.FP
{
    public partial class CustomerFPGoalPlanningDetails : System.Web.UI.UserControl
    {
        CustomerGoalPlanningBo goalPlanningBo = new CustomerGoalPlanningBo();
        CustomerGoalPlanningVo goalPlanningVo = new CustomerGoalPlanningVo();
        CustomerVo customerVo = new CustomerVo();
        RMVo rmVo = new RMVo();
        DataSet customerGoalDetailsDS;
        protected void Page_Load(object sender, EventArgs e)
        {
             customerVo=(CustomerVo)Session["customerVo"];
            if (!Page.IsPostBack)
            {
                customerGoalDetailsDS = goalPlanningBo.GetCustomerGoalDetails(customerVo.CustomerId);
                BindCustomerGoalDetailGrid(customerGoalDetailsDS);
            }
        }

        public void BindCustomerGoalDetailGrid(DataSet customerGoalDetailsDS)
        {
            DataTable dtCustomerGoalDetails = new DataTable();
            dtCustomerGoalDetails.Columns.Add("GoalType");
            dtCustomerGoalDetails.Columns.Add("ChildName");
            dtCustomerGoalDetails.Columns.Add("CostToday");
            dtCustomerGoalDetails.Columns.Add("GaolYear");

            dtCustomerGoalDetails.Columns.Add("GoalAmountInGoalYear");
            dtCustomerGoalDetails.Columns.Add("CorpusLeftBehind");
            dtCustomerGoalDetails.Columns.Add("GoalPriority");
            dtCustomerGoalDetails.Columns.Add("GoalFunded");

            dtCustomerGoalDetails.Columns.Add("EquityFundedAmount");
            dtCustomerGoalDetails.Columns.Add("DebtFundedAmount");
            dtCustomerGoalDetails.Columns.Add("CashFundedAmount");
            dtCustomerGoalDetails.Columns.Add("AlternateFundedAmount");

            dtCustomerGoalDetails.Columns.Add("TotalFundedAmount");
            dtCustomerGoalDetails.Columns.Add("GoalFundedGap");

            DataRow drCustomerGoalDetails;

            foreach (DataRow dr in customerGoalDetailsDS.Tables[0].Rows)
            {
                drCustomerGoalDetails = dtCustomerGoalDetails.NewRow();

                drCustomerGoalDetails["GoalType"] = dr["XG_GoalName"].ToString();
                drCustomerGoalDetails["ChildName"] = dr["ChildName"].ToString();
                drCustomerGoalDetails["CostToday"] =Math.Round(double.Parse(dr["CG_CostToday"].ToString()),2);
                drCustomerGoalDetails["GaolYear"] = dr["CG_GoalYear"].ToString();

                drCustomerGoalDetails["GoalAmountInGoalYear"] = Math.Round(double.Parse(dr["CG_FVofCostToday"].ToString()), 2);
                drCustomerGoalDetails["CorpusLeftBehind"] = 50000;
                drCustomerGoalDetails["GoalPriority"] = dr["CG_Priority"].ToString();
                drCustomerGoalDetails["GoalFunded"] = "No";

                drCustomerGoalDetails["EquityFundedAmount"] =0;
                drCustomerGoalDetails["DebtFundedAmount"] = 0;
                drCustomerGoalDetails["CashFundedAmount"] = 0;
                drCustomerGoalDetails["AlternateFundedAmount"] = 0;

                drCustomerGoalDetails["TotalFundedAmount"] = 0;
                drCustomerGoalDetails["GoalFundedGap"] = Math.Round(double.Parse(dr["CG_FVofCostToday"].ToString()), 2);
                dtCustomerGoalDetails.Rows.Add(drCustomerGoalDetails);
 
            }

            gvrGoalPlanning.DataSource = dtCustomerGoalDetails;
            gvrGoalPlanning.DataBind();
 
        }
    }
}