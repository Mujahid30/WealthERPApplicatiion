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
        string goalAction = "";
        string goalCategory = "";
        int goalId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            rtbNonRT.Attributes.Add("onClick", "javascript:ShowHideGaolType(value);");
            rtbRT.Attributes.Add("onClick", "javascript:ShowHideGaolType(value);");

            rmVo = (RMVo)Session[SessionContents.RmVo];
            customerVo=(CustomerVo)Session["customerVo"];
            DataTable dtFPCalculationBasis;
            
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["GoalId"] != null)
                    goalId = int.Parse(Request.QueryString["GoalId"].ToString());
                if (Request.QueryString["GoalCategory"] != null)
                    goalCategory = Request.QueryString["GoalCategory"];
                if (Request.QueryString["goalAction"] != null)
                    goalAction = Request.QueryString["goalAction"];

                dtFPCalculationBasis = goalPlanningBo.GetCustomerFPCalculationBasis(customerVo.CustomerId);
                RTGoalPageLoadSetup(dtFPCalculationBasis);
                dsAllDropDownDetails = goalPlanningBo.GetAllGoalDropDownDetails(customerVo.CustomerId);
                BindGoalObjectiveDropDown(dsAllDropDownDetails.Tables["GoalObjective"]);
                BindPickChildDropDown(dsAllDropDownDetails.Tables["ChildDetails"]);
                BindFrequencyDropDown(dsAllDropDownDetails.Tables["GoalFrequency"]);                
                BindGoalYearDropDown();

                trPickChild.Visible = false;
                trFrequency.Visible = false;
                btnRTUpdate.Visible = false;
                btnNonRTUpdate.Visible = false;

               if(goalId!=0 && !string.IsNullOrEmpty(goalCategory) && !string.IsNullOrEmpty(goalAction))
               {
                   GoalViewEditByGoalId(goalId, goalAction, goalCategory);
                   rtbNonRT.Enabled = false;
                   rtbRT.Enabled = false;
               }
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
                if (ddlPickChild.SelectedIndex!=0)
                goalPlanningVo.AssociateId = int.Parse(ddlPickChild.SelectedValue.ToString());
            } 
            if (txtComment.Text != "")
            {
                goalPlanningVo.Comments = txtComment.Text.ToString();

            }
            goalPlanningVo.CreatedBy = int.Parse(rmVo.RMId.ToString());

            goalPlanningBo.CreateCustomerGoalPlanning(goalPlanningVo, customerVo.CustomerId,false);
            

        }

        protected void GoalViewEditByGoalId(int goalId, string goalAction,string goalCategory)
        {
            goalPlanningVo = goalPlanningBo.GetGoalDetails(goalId);
            ViewState["GoalId"] = goalId;
            if(goalCategory=="NonRT")
            {
                rtbNonRT.Checked = true;
                rtbRT.Checked = false;
                btnNonRTSave.Visible = false;
                btnRTUpdate.Visible = false;
                btnNonRTUpdate.Visible = true;
 

            if (goalPlanningVo != null)
            {
                ddlGoalType.SelectedValue = goalPlanningVo.Goalcode;
                txtGoalDescription.Text = goalPlanningVo.GoalDescription;
                txtGoalDate.Text = goalPlanningVo.GoalProfileDate.ToShortDateString();
                ddlPriority.SelectedValue = goalPlanningVo.Priority;
                if (goalPlanningVo.IsOnetimeOccurence == true)
                    ddlOccurrence.SelectedValue = "Once";
                else
                {
                    ddlOccurrence.SelectedValue = "Recurring";
                    trFrequency.Visible = true;
                    ddlFrequency.SelectedValue = goalPlanningVo.Frequency.Trim();
                }

                ddlGoalYear.SelectedValue = goalPlanningVo.GoalYear.ToString();
                txtGoalCostToday.Text = goalPlanningVo.CostOfGoalToday.ToString();
                txtComment.Text = goalPlanningVo.Comments;
                if (goalAction == "Edit")
                {
                    SetControlReadOnly(false);
                }
                else if (goalAction == "View")
                {
                    SetControlReadOnly(true);
 
                }
                
              } 
                
            }
            else if (goalCategory == "RT")
            {
                rtbRT.Checked = true;
                rtbNonRT.Checked = false;
                btnRTSave.Visible = false;
                btnRTUpdate.Visible = true;
                btnNonRTUpdate.Visible = false;
 
            }

            rtbRT.Enabled = false;
            rtbNonRT.Enabled = false;

 
        }

        protected void SetControlReadOnly(bool readOnly)
        {
            if (readOnly)
            {
                ddlGoalType.Enabled = false;
                txtGoalDescription.Enabled = false;
                txtGoalDate.Enabled = false;
                ddlPriority.Enabled = false;
                ddlOccurrence.Enabled = false;
                ddlFrequency.Enabled = false;
                ddlGoalYear.Enabled = false;
                txtGoalCostToday.Enabled = false;
                txtComment.Enabled = false;
                btnNonRTSave.Enabled = false;
                btnRTSave.Enabled = false;

                btnRTUpdate.Enabled = false;
                btnNonRTUpdate.Enabled = false;

                aplToolBar.Enabled = true;
                

            }
            else
            {
                ddlGoalType.Enabled = false;
                txtGoalDescription.Enabled = true;
                txtGoalDate.ReadOnly = true;
                ddlPriority.Enabled = true;
                ddlOccurrence.Enabled = true;
                ddlFrequency.Enabled = true;
                ddlGoalYear.Enabled = true;
                txtGoalCostToday.Enabled = true;
                txtComment.Enabled = true;
                btnNonRTSave.Enabled = true;
                btnRTSave.Enabled = true;          

                btnRTUpdate.Enabled = true;
                btnNonRTUpdate.Enabled = true;

                aplToolBar.Enabled = false;
 
            }
 
        }

        protected void aplToolBar_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            if (e.Item.Value == "Edit")
            {
                SetControlReadOnly(false);

            }
        }

        protected void btnNonRTUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                goalPlanningVo.GoalId = int.Parse(ViewState["GoalId"].ToString());
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
                    if (ddlPickChild.SelectedIndex != 0)
                        goalPlanningVo.AssociateId = int.Parse(ddlPickChild.SelectedValue.ToString());
                }
                if (txtComment.Text != "")
                {
                    goalPlanningVo.Comments = txtComment.Text.ToString();

                }
                goalPlanningBo.CreateCustomerGoalPlanning(goalPlanningVo, customerVo.CustomerId, true);

            }
            catch (Exception ee)
            {
 
            }


        }
            
        protected void ddlGoalType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGoalType.SelectedValue == "ED" || ddlGoalType.SelectedValue == "MR")
            {
                trPickChild.Visible = true;
            }
            else
            {
                trPickChild.Visible = false;
            }

        }

        protected void ddlOccurrence_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOccurrence.SelectedValue == "Once")
            {
                trFrequency.Visible = false;
            }
            else
            {
                trFrequency.Visible = true;

            }

        }

        protected void RTGoalPageLoadSetup(DataTable dtFPCalculationBasis)
        {

            string rtCalculationBasis = "";
            DataRow[] drFPCalculationBasis;
            int rtCalculationBasisId = 0;

            drFPCalculationBasis = dtFPCalculationBasis.Select();

            foreach (DataRow dr in drFPCalculationBasis)
            {
                if (dr["WFPC_CalculationId"].ToString() == "1")
                {                    
                    rtCalculationBasisId = int.Parse(dr["WFPCB_CalculationBasisId"].ToString());                   

                }
                else if (dr["WFPC_CalculationId"].ToString() == "2")
                {
                    rtCalculationBasis = dr["WFPCB_CalculationBasis"].ToString();
                    if (dr["WFPCB_CalculationBasisId"].ToString()=="3")
                      hidFPCalculationBasis.Value = "SELF";
                    else
                      hidFPCalculationBasis.Value = "WITH_SPOUSE";
                }


            }
            lblRTGoalBasis.Text += rtCalculationBasis;
            if (rtCalculationBasisId == 1)
            {
                trRTGoalCorpsToBeLeftBehind.Visible = false;
                hidRTGoalCorpsLeftBehind.Value = "0";
            }
            else
            {
                trRTGoalCorpsToBeLeftBehind.Visible = true;
                hidRTGoalCorpsLeftBehind.Value = "1";

            }
 
 
        }

        protected void btnRTSave_Click(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            goalPlanningVo.CustomerId = customerVo.CustomerId;
            if (customerVo.Dob != DateTime.MinValue)
            {
                goalPlanningVo.CustomerAge = (DateTime.Now.Year - customerVo.Dob.Year);
            }
            goalPlanningVo.Goalcode = "RT";
            goalPlanningVo.CostOfGoalToday = double.Parse(txtRTGoalCostToday.Text);
            goalPlanningVo.GoalDate =DateTime.Parse(DateTime.Now.ToShortDateString());
            if (!string.IsNullOrEmpty(hidRTGoalCorpsLeftBehind.Value.ToString()))
            {
                goalPlanningVo.CorpusToBeLeftBehind = true;
                goalPlanningVo.CorpusLeftBehind =int.Parse(txtRTCorpusToBeLeftBehind.Text);
                
            }
            goalPlanningBo.CreateCustomerGoalPlanning(goalPlanningVo, customerVo.CustomerId, false);

        }
        
    }
}