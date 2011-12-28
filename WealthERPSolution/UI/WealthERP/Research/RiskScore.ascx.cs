using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoResearch;
using System.Data;
using Telerik.Web.UI;
using BoCustomerRiskProfiling;

namespace WealthERP.Research
{
    public partial class RiskScore : System.Web.UI.UserControl
    {
        DataSet dsGlobal = new DataSet();
        AdvisorVo adviserVo = new AdvisorVo();
        AdviserFPConfigurationBo adviserFPConfigurationBo = new AdviserFPConfigurationBo();
        RiskProfileBo riskprofilebo = new RiskProfileBo();
        ModelPortfolioBo modelPortfolioBo = new ModelPortfolioBo();
        int expiryAge = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            adviserVo = (AdvisorVo)Session["AdvisorVo"];
            BindAdviserAssumptions();
        }

        public void BindAdviserAssumptions()
        {
            //DataSet dsRiskScore;
            DataTable dt = new DataTable();
            dt = adviserFPConfigurationBo.GetAdviserRiskScore(adviserVo.advisorId);
            DataTable dtRiskScore = new DataTable();
            dtRiskScore.Columns.Add("XRC_RiskClass");
            dtRiskScore.Columns.Add("WRPR_RiskScoreLowerLimit");
            dtRiskScore.Columns.Add("WRPR_RiskScoreUpperLimit");
            dtRiskScore.Columns.Add("XRC_RiskClassCode");
            DataRow drRiskScore;
            foreach (DataRow dr in dt.Rows)
            {
                drRiskScore = dtRiskScore.NewRow();
                drRiskScore["XRC_RiskClassCode"] = dr["XRC_RiskClassCode"].ToString();
                drRiskScore["XRC_RiskClass"] = dr["XRC_RiskClass"].ToString();
                drRiskScore["WRPR_RiskScoreLowerLimit"] = dr["WRPR_RiskScoreLowerLimit"].ToString();
                drRiskScore["WRPR_RiskScoreUpperLimit"] = dr["WRPR_RiskScoreUpperLimit"].ToString();
                dtRiskScore.Rows.Add(drRiskScore);
            }
            RadGrid1.DataSource = dtRiskScore;
            RadGrid1.DataBind();
        }

        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                decimal lowerLimit = 0;
                decimal upperLimit = 0;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                DropDownList ddl = (DropDownList)e.Item.FindControl("ddlPickRiskClass");
                TextBox txtLower = (TextBox)e.Item.FindControl("txtLowerrLimit");
                TextBox txtUpper = (TextBox)e.Item.FindControl("txtUpperLimit");
                lowerLimit = Convert.ToDecimal(txtLower.Text);
                upperLimit = Convert.ToDecimal(txtUpper.Text);
                adviserFPConfigurationBo.InsertUpdateRiskClassScore(ddl.SelectedValue, lowerLimit, upperLimit, adviserVo.advisorId, adviserVo.UserId);
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                RadGrid1.Controls.Add(new LiteralControl("Unable to update Employee. Reason: " + ex.Message));
                e.Canceled = true;
            } 
        }

        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                decimal lowerLimit = 0;
                decimal upperLimit = 0;                
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                DropDownList ddl = (DropDownList)e.Item.FindControl("ddlPickRiskClass");
                TextBox txtLower = (TextBox)e.Item.FindControl("txtLowerrLimit");
                TextBox txtUpper = (TextBox)e.Item.FindControl("txtUpperLimit");
                lowerLimit = Convert.ToDecimal(txtLower.Text);
                upperLimit = Convert.ToDecimal(txtUpper.Text);
                adviserFPConfigurationBo.InsertUpdateRiskClassScore(ddl.SelectedValue, lowerLimit, upperLimit, adviserVo.advisorId, adviserVo.UserId);
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                RadGrid1.Controls.Add(new LiteralControl("Unable to insert Employee. Reason: " + ex.Message));
                e.Canceled = true;
            } 
        }

        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {            
            try
            {              
                //GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                string riskClassCode = RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XRC_RiskClassCode"].ToString();
                adviserFPConfigurationBo.DeleteRiskClassScore(riskClassCode, adviserVo.advisorId);
                BindAdviserAssumptions();
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                RadGrid1.Controls.Add(new LiteralControl("Unable to delete Employee. Reason: " + ex.Message));
                e.Canceled = true;
            } 
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            //if (e.CommandName == RadGrid.InitInsertCommandName)
            //{
            //    e.Canceled = true;
            //    RadGrid1.EditIndexes.Clear();
            //    GridEditableItem item = (GridEditableItem)e.Item;
            //    DropDownList ddl = (DropDownList)e.Item.FindControl("ddlPickRiskClass");
            //    ddl.Enabled = false;
            //    e.Item.OwnerTableView.EditFormSettings.UserControlName = "ddlPickRiskClass";
            //    e.Item.OwnerTableView.InsertItem();
            //}
            //else if (e.CommandName == RadGrid.EditCommandName)
            //{
            //    DropDownList ddl = (DropDownList)e.Item.FindControl("ddlPickRiskClass");
            //    ddl.Enabled = false;
            //    e.Item.OwnerTableView.IsItemInserted = false;
            //    e.Item.OwnerTableView.EditFormSettings.UserControlName = "ddlPickRiskClass";
            //}
        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                e.Item.FindControl("InitInsertButton").Parent.Visible = false;
            }
        }
        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                GridEditFormItem editform = (GridEditFormItem)e.Item;
                DropDownList ddl = (DropDownList)editform.FindControl("ddlPickRiskClass");
                int adviserId = adviserVo.advisorId;

                dsGlobal = riskprofilebo.GetAdviserRiskClasses(adviserId);
                if (dsGlobal.Tables[0].Rows.Count > 0)
                {
                    ddl.DataSource = dsGlobal.Tables[0];
                    ddl.DataValueField = dsGlobal.Tables[0].Columns["XRC_RiskClassCode"].ToString();
                    ddl.DataTextField = dsGlobal.Tables[0].Columns["XRC_RiskClass"].ToString();
                    ddl.DataBind();
                }
                ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Risk Class", "Select Risk Class"));
            }
        }
    }
}