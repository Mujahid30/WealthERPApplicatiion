using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using System.Data;
using BoResearch;
using Telerik.Web.UI;

namespace WealthERP.Research
{
    public partial class Assumptions : System.Web.UI.UserControl
    {
        AdvisorVo adviserVo = new AdvisorVo();
        AdviserFPConfigurationBo adviserFPConfigurationBo = new AdviserFPConfigurationBo();
        int expiryAge = 0;
        protected void Page_Load(object sender, EventArgs e)
        {            
            adviserVo = (AdvisorVo)Session["AdvisorVo"];
            BindAdviserAssumptions();           
        }

        public void BindAdviserAssumptions()
        {            
            DataSet dsAdviserAssumptions;
            DataTable dtAdviserAssumptions = new DataTable();
            dsAdviserAssumptions = adviserFPConfigurationBo.GetAdviserAssumptions(adviserVo.advisorId);            
            DataTable dtAssumption = new DataTable();
            dtAssumption.Columns.Add("WA_AssumptionName");
            dtAssumption.Columns.Add("AA_Value");
            dtAssumption.Columns.Add("WA_AssumptionId");
            DataRow drAssumption;
            foreach (DataRow drStaticAssumption in dsAdviserAssumptions.Tables[0].Rows)
            {
                drAssumption = dtAssumption.NewRow();
                if (drStaticAssumption["WA_AssumptionId"].ToString() == "LE" || drStaticAssumption["WA_AssumptionId"].ToString() == "RA")
                {
                    drAssumption["WA_AssumptionName"] = drStaticAssumption["WA_AssumptionName"].ToString();
                    drAssumption["AA_Value"] = Convert.ToInt32(drStaticAssumption["AA_Value"]);
                    drAssumption["WA_AssumptionId"] = drStaticAssumption["WA_AssumptionId"].ToString();
                    dtAssumption.Rows.Add(drAssumption);
                }
                else
                {
                    drAssumption["WA_AssumptionName"] = drStaticAssumption["WA_AssumptionName"].ToString();
                    drAssumption["AA_Value"] = drStaticAssumption["AA_Value"].ToString();
                    drAssumption["WA_AssumptionId"] = drStaticAssumption["WA_AssumptionId"].ToString();
                    dtAssumption.Rows.Add(drAssumption);
                }
            }
            RadGrid1.DataSource = dtAssumption;
            RadGrid1.DataBind();
        }

        protected void RadGrid1_ItemUpdated(object source, Telerik.Web.UI.GridUpdatedEventArgs e)
        { 
        }

        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {            
            decimal value = 0;
            if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
                editColumn.Visible = false;
            }
            else if (e.CommandName == RadGrid.RebindGridCommandName && e.Item.OwnerTableView.IsItemInserted)
            {
                e.Canceled = true;
            }
            else if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
                TextBox txt = (TextBox)e.Item.FindControl("txtAssumptionValue");
                if (txt.Text != "")
                    value = decimal.Parse(txt.Text);
                else
                    value = 0;
                string assumptionId = (RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WA_AssumptionId"].ToString());
                adviserFPConfigurationBo.UpdateAdviserAssumptions(adviserVo.advisorId, value, assumptionId);
                BindAdviserAssumptions();
            }
            else
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
                if (!editColumn.Visible)
                    editColumn.Visible = true;
            }
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
    }
}