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
    public partial class CustomerFPGoalSetup : System.Web.UI.UserControl
    {
        CustomerGoalPlanningBo goalPlanningBo = new CustomerGoalPlanningBo();
        CustomerGoalPlanningVo goalPlanningVo = new CustomerGoalPlanningVo();
        CustomerVo customerVo = new CustomerVo();
        RMVo rmVo = new RMVo();
        DataSet dsAllDropDownDetails;
        protected void Page_Load(object sender, EventArgs e)
        {
            rtbNonRT.Attributes.Add("onClick", "javascript:ShowHideGaolType(value);");
            rtbRT.Attributes.Add("onClick", "javascript:ShowHideGaolType(value);");

            rmVo = (RMVo)Session[SessionContents.RmVo];
            customerVo=(CustomerVo)Session["customerVo"];
            
            if (!Page.IsPostBack)
            {
                dsAllDropDownDetails = goalPlanningBo.GetAllGoalDropDownDetails(customerVo.CustomerId);
                BindGoalObjectiveDropDown(dsAllDropDownDetails.Tables["GoalObjective"]);
                BindPickChildDropDown(dsAllDropDownDetails.Tables["ChildDetails"]);
                BindFrequencyDropDown(dsAllDropDownDetails.Tables["GoalFrequency"]);                
                BindGoalYearDropDown();

            }
        }
        
        private void BindGoalObjectiveDropDown(DataTable dtGoalObjective)
        {
            ddlGoalType.DataSource = dtGoalObjective;
            ddlGoalType.DataValueField = dtGoalObjective.Columns["XG_GoalCode"].ToString();
            ddlGoalType.DataTextField = dtGoalObjective.Columns["XG_GoalName"].ToString();
            ddlGoalType.DataBind();
            ddlGoalType.Items.Insert(0, new ListItem("Select", "Select"));
            ddlGoalType.SelectedIndex = 0;

        }

        private void BindPickChildDropDown(DataTable dtChildDetails)
        {
            ddlPickChild.DataSource = dtChildDetails;
            ddlPickChild.DataValueField = dtChildDetails.Columns["CA_AssociationId"].ToString();
            ddlPickChild.DataTextField = dtChildDetails.Columns["ChildName"].ToString();
            ddlPickChild.DataBind();
            ddlPickChild.Items.Insert(0, new ListItem("Select a Child", "Select"));
            ddlPickChild.SelectedIndex = 0;

        }

        private void BindFrequencyDropDown(DataTable dtFrequency)
        {
            ddlFrequency.DataSource = dtFrequency;
            ddlFrequency.DataValueField = dtFrequency.Columns["XGF_GoalFrequecnyId"].ToString();
            ddlFrequency.DataTextField = dtFrequency.Columns["XGF_GoalFrequecny"].ToString();
            ddlFrequency.DataBind();
            ddlFrequency.Items.Insert(0, new ListItem("Select a Frequency", "Select"));
            ddlFrequency.SelectedIndex = 0;

        }
        private void BindGoalYearDropDown()
        {
            int goalYear = DateTime.Now.Year;
            int lifeYear = goalYear + 75;
            int year = goalYear;
            for (; goalYear <= lifeYear; lifeYear--)
            {
                ddlGoalYear.Items.Add(year.ToString());
                year++;
            }
            ddlGoalYear.Items.Insert(0, new ListItem("Select", "Select"));
        }


        protected void btnNonRTSave_Click(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            goalPlanningVo.CustomerId = customerVo.CustomerId;
            goalPlanningVo.Goalcode = ddlGoalType.SelectedValue.ToString();
            goalPlanningVo.CostOfGoalToday = double.Parse(txtGoalCostToday.Text);
            goalPlanningVo.GoalDate = DateTime.Parse(txtGoalDate.Text);
            goalPlanningVo.Priority = ddlPriority.SelectedValue.ToString();
            if (ddlOccurrence.SelectedValue.ToString() == "Once")
            {
                goalPlanningVo.IsOnetimeOccurence = true;
            }
            else if (ddlOccurrence.SelectedValue.ToString() == "Recurring")
            {
                goalPlanningVo.Frequency = ddlFrequency.SelectedValue.ToString();
            }
            goalPlanningVo.GoalYear = int.Parse(ddlGoalYear.SelectedValue);
            goalPlanningVo.GoalDescription = txtGoalDescription.Text.ToString();
            
            if (ddlGoalType.SelectedValue == "ED" || ddlGoalType.SelectedValue == "MR")
            {
                goalPlanningVo.AssociateId = int.Parse(ddlPickChild.SelectedValue.ToString());
            } 
            if (txtComment.Text != "")
            {
                goalPlanningVo.Comments = txtComment.Text.ToString();

            }
            goalPlanningVo.CreatedBy = int.Parse(rmVo.RMId.ToString());

            goalPlanningBo.CreateCustomerGoalPlanning(goalPlanningVo, customerVo.CustomerId, 0);
            //goalPlanningBo.

        }

    }
}