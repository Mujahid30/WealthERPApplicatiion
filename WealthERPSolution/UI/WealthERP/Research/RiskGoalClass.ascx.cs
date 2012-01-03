using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using WealthERP.Base;
using VoUser;
using BoCustomerRiskProfiling;
using Telerik.Web.UI;
using BoResearch;
using System.Web.UI.HtmlControls;


namespace WealthERP.Research
{
    public partial class RiskGoalClass : System.Web.UI.UserControl
    {
        DataSet dsGlobal = new DataSet();
        AdvisorVo advisorVo = new AdvisorVo();
        ModelPortfolioBo modelPortfolioBo = new ModelPortfolioBo();
        RiskProfileBo riskprofilebo = new RiskProfileBo();
        private string gridMessage = null;
        int var = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            //BindRiskClasses();            
                bindRadGrid1(); 
        }

        public void bindRadGrid1()
        {
            DataTable dtRiskClass = new DataTable();
            DataTable dataTable = new DataTable();
            var = 1;
            ModelPortfolioBo modelPortfolioBo = new ModelPortfolioBo();
            dtRiskClass = modelPortfolioBo.GetRiskGoalClassData(advisorVo.advisorId);
            DataTable dtClass = new DataTable();
            dtClass.Columns.Add("XRC_RiskClass");
            dtClass.Columns.Add("XRC_RiskClassCode");
            dtClass.Columns.Add("ARC_RiskText");            

            DataRow drRiskClass;
            foreach (DataRow dr in dtRiskClass.Rows)
            {
                drRiskClass = dtClass.NewRow();
                drRiskClass["XRC_RiskClass"] = dr["XRC_RiskClass"].ToString();
                drRiskClass["XRC_RiskClassCode"] = dr["XRC_RiskClassCode"].ToString(); 
                drRiskClass["ARC_RiskText"] = dr["ARC_RiskText"].ToString();                
                dtClass.Rows.Add(drRiskClass);
            }
            RadGrid1.DataSource = dtClass;
            RadGrid1.DataSourceID = String.Empty;
            RadGrid1.DataBind();
        }
        
        protected void RadGrid1_DataBound(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(gridMessage))
            {
                DisplayMessage(gridMessage);
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                GridEditFormItem editform = (GridEditFormItem)e.Item;
                DropDownList ddl = (DropDownList)editform.FindControl("ddlPickRiskClass");
                int adviserId = advisorVo.advisorId;
                HtmlTableRow trRiskCodeddl = (HtmlTableRow)editform.FindControl("trRiskCodeddl");
                HtmlTableRow trRiskGoaltextBox = (HtmlTableRow)editform.FindControl("trRiskGoaltextBox");
           
                if (e.Item.RowIndex == -1)
                {
                    trRiskCodeddl.Visible = true;
                    trRiskGoaltextBox.Visible = false;
                    dsGlobal = modelPortfolioBo.bindDdlPickRiskClass(adviserId);
                    if (dsGlobal.Tables[0].Rows.Count > 0)
                    {
                        ddl.DataSource = dsGlobal.Tables[0];
                        ddl.DataValueField = dsGlobal.Tables[0].Columns["XRC_RiskClassCode"].ToString();
                        ddl.DataTextField = dsGlobal.Tables[0].Columns["XRC_RiskClass"].ToString();
                        ddl.DataBind();
                    }
                    ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Risk Class", "Select Risk Class"));
                }
                else
                {
                    trRiskCodeddl.Visible = false;
                    trRiskGoaltextBox.Visible = true;
                }
            }
        }

        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {                    
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                //string riskCode = item.GetDataKeyValue("XRC_RiskClassCode").ToString();
                DropDownList ddl = (DropDownList)item.FindControl("ddlPickRiskClass");
                //ddl.Visible = false;
                TextBox Description = (TextBox)item.FindControl("txtDescription");
                modelPortfolioBo.InsertUpdateRiskGoalClass(ddl.SelectedValue, Description.Text, advisorVo.advisorId);
                bindRadGrid1();
            }
            catch (Exception ex)
            {
                RadGrid1.Controls.Add(new LiteralControl("Unable to Insert Class. Reason: " + ex.Message));
                e.Canceled = true;
            }            
        }

        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        { 
            try
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                String riskCode = dataItem.GetDataKeyValue("XRC_RiskClassCode").ToString();
                //DropDownList ddl = (DropDownList)dataItem.FindControl("ddlPickRiskClass");
                modelPortfolioBo.DeleteRiskClass(riskCode, advisorVo.advisorId);
                bindRadGrid1();
            }
            catch (Exception ex)
            {
                RadGrid1.Controls.Add(new LiteralControl("Unable to Delete Class. Reason: " + ex.Message));
                e.Canceled = true;
            } 
        }

        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == RadGrid.UpdateCommandName)
                {
                    if (e.Item is GridEditFormItem)
                    {
                        GridEditFormItem item = (GridEditFormItem)e.Item;
                        string riskCode = item.GetDataKeyValue("XRC_RiskClassCode").ToString();
                        //DropDownList ddl = (DropDownList)item.FindControl("ddlPickRiskClass");
                        TextBox txtRiskCode = (TextBox)item.FindControl("txtRiskCode");
                        TextBox Description = (TextBox)item.FindControl("txtDescription");

                        modelPortfolioBo.InsertUpdateRiskGoalClass(riskCode, Description.Text, advisorVo.advisorId);
                        bindRadGrid1();
                    }
                }
            }
            catch (Exception ex)
            {
                RadGrid1.Controls.Add(new LiteralControl("Unable to Insert Class. Reason: " + ex.Message));
                e.Canceled = true;
            }
        }

        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(string.Format("<span style='color:red'>{0}</span>", text)));
        }

        private void SetMessage(string message)
        {
            gridMessage = message;
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {   
            if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
            {
                //GridEditableItem item = (GridEditableItem)e.Item;
                //TableRow tr1 = (TableRow)item.FindControl("trRiskCode");
                //TableRow tr2 = (TableRow)item.FindControl("trRiskGoal");
                //tr1.Visible = false;
                //tr2.Visible = true;
            }
            else if (e.CommandName == RadGrid.RebindGridCommandName && e.Item.OwnerTableView.IsItemInserted)
            {
                e.Canceled = true;
            }
            else if (e.CommandName == RadGrid.EditCommandName)
            {
                //GridEditableItem item = (GridEditableItem)e.Item;
                //TableRow tr1 = (TableRow)item.FindControl("trRiskCode");
                //TableRow tr2 = (TableRow)item.FindControl("trRiskGoal");
                //tr1.Visible = true;
                //tr2.Visible = false;
            }
            else if (e.CommandName == RadGrid.DeleteCommandName)
            {
            }  
            else
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
                if (!editColumn.Visible)
                    editColumn.Visible = true;
                //GridEditableItem tableItem = (GridEditableItem)e.Item;
                //GridDataItem item = (GridDataItem)e.Item;
                //string riskCode = item.GetDataKeyValue("XRC_RiskClassCode").ToString();
                ////string riskCode2 = tableItem.GetDataKeyValue("XRC_RiskClassCode").ToString();
                //DropDownList ddl1 = (DropDownList)tableItem.FindControl("ddlPickRiskClass");
                //ddl1.SelectedValue = riskCode;
                //ddl1.Enabled = false;
            }
            //if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            //{
            //    GridEditableItem editableItem = (GridEditableItem)e.Item;
            //    GridEditFormItem editform = (GridEditFormItem)e.Item;
            //    //string riskCode = editform.GetDataKeyValue("XRC_RiskClassCode").ToString();
            //    DropDownList ddl1 = (DropDownList)editableItem.FindControl("ddlPickRiskClass");
            //    //ddl1.SelectedValue = riskCode;
            //    ddl1.Enabled = false;

            //    //GridEditableItem tableItem = (GridEditableItem)e.Item;
            //    //GridDataItem item = (GridDataItem)e.Item;
            //    //string riskCode = item.GetDataKeyValue("XRC_RiskClassCode").ToString();
            //    ////string riskCode2 = tableItem.GetDataKeyValue("XRC_RiskClassCode").ToString();
            //    //DropDownList ddl1 = (DropDownList)tableItem.FindControl("ddlPickRiskClass");
            //    //ddl1.SelectedValue = riskCode;
            //    //ddl1.Enabled = false;
            //}
        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {            
        }
    }
}