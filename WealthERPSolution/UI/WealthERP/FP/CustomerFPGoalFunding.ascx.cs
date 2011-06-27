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
    public partial class CustomerFPGoalFunding : System.Web.UI.UserControl
    {
        CustomerGoalPlanningBo goalPlanningBo = new CustomerGoalPlanningBo();
        CustomerFPAnalyticsBo customerFPAnalyticsBo = new CustomerFPAnalyticsBo();
        CustomerVo customerVo;
        DataSet dsRebalancing;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["customerVo"]!=null)
            customerVo = (CustomerVo)Session["customerVo"];
            if (!Page.IsPostBack)
            {
                BindGoalListDropDown(customerVo.CustomerId);
                dsRebalancing = customerFPAnalyticsBo.FutureSurplusEngine(customerVo.CustomerId);

            }

        }

        protected void BindGoalListDropDown(int customerId)
        {
            DataSet dsGoalList = goalPlanningBo.GetCustomerGoalList(customerId);           
            ddlPickGoal.DataSource = dsGoalList.Tables[0];
            ddlPickGoal.DataTextField = dsGoalList.Tables[0].Columns["XG_GoalName"].ToString();
            ddlPickGoal.DataValueField = dsGoalList.Tables[0].Columns["CG_GoalId"].ToString();
            ddlPickGoal.DataBind();

        }

        protected void ddlPickGoal_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetThDetailsOfGoalFunding(Convert.ToInt32(ddlPickGoal.SelectedValue));
        }

        public void GetThDetailsOfGoalFunding(int gaolId)
        {
            int goalYear = 0;
            int goalAmount = 0;
            DataRow[] drGoal;
            DataRow[] drAssetDetails;
            customerVo = (CustomerVo)Session["customerVo"];
            DataSet dsGoalList = goalPlanningBo.GetCustomerGoalList(customerVo.CustomerId);
            dsRebalancing = customerFPAnalyticsBo.FutureSurplusEngine(customerVo.CustomerId);
            drGoal = dsGoalList.Tables[0].Select("CG_GoalId=" + gaolId.ToString());
            drAssetDetails = dsRebalancing.Tables[1].Select("Year=" + drGoal[0][2].ToString());
            txtEquityAvlCorps.Text = drAssetDetails[0]["PreviousYearClosingBalance"].ToString();
            txtDebtAvlCorps.Text = drAssetDetails[1]["PreviousYearClosingBalance"].ToString();
            txtCashAvlCorps.Text = drAssetDetails[2]["PreviousYearClosingBalance"].ToString();
            txtAlternateAvlCorps.Text = drAssetDetails[3]["PreviousYearClosingBalance"].ToString();
 
 
 
        }
    }
}