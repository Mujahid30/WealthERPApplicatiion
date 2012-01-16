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
        AdvisorVo advisorVo = new AdvisorVo();
        ModelPortfolioBo modelPortfolioBo = new ModelPortfolioBo();
        RiskProfileBo riskprofilebo = new RiskProfileBo();
        private string gridMessage = null;
        int isRiskClass;

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            //if (IsPostBack)
            //{
                bindRadGrid1();                
            //}
            //if (!IsPostBack)
            //{
            //    RadGrid1.Visible = false;
            //}
        }

        public void bindRadGrid1()
        {
            DataTable dtRiskClass = new DataTable();
            
            ModelPortfolioBo modelPortfolioBo = new ModelPortfolioBo();

            //dtRiskClass = modelPortfolioBo.GetRiskGoalClassData(advisorVo.advisorId, Convert.ToInt16(ddlClassType.SelectedValue));
            /***********Here ! is hard coded for riskClasses
             you can change it by binding the dropdown in future & make executable above commented line....Date(16/01/2012)
             ***************/

            dtRiskClass = modelPortfolioBo.GetRiskGoalClassData(advisorVo.advisorId, 1);
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
        
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            DataSet dsRiskClass = new DataSet();
            if (e.Item is GridCommandItem)
            {
                GridCommandItem cmditm = (GridCommandItem)e.Item;
                //to hide AddNewRecord button
                //cmditm.FindControl("InitInsertButton").Visible = false;//hide the text
                //cmditm.FindControl("AddNewRecordButton").Visible = false;//hide the image

                //to hide Refresh button
                cmditm.FindControl("RefreshButton").Visible = false;//hide the text
                cmditm.FindControl("RebindGridButton").Visible = false;//hide the image
            }
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string riskClassName = dataItem["XRC_RiskClass"].Text;

                LinkButton button = dataItem["DeleteColumn"].Controls[0] as LinkButton;
                button.Attributes["onclick"] = "return confirm('Are you sure you want to delete " +
                riskClassName + "?')";
            }
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
                    //dsRiskClass = modelPortfolioBo.bindDdlPickRiskClass(adviserId, Convert.ToInt16(ddlClassType.SelectedValue));

                    dsRiskClass = modelPortfolioBo.bindDdlPickRiskClass(adviserId, 1);
                    if (dsRiskClass.Tables[0].Rows.Count > 0)
                    {
                        ddl.DataSource = dsRiskClass.Tables[0];
                        ddl.DataValueField = dsRiskClass.Tables[0].Columns["XRC_RiskClassCode"].ToString();
                        ddl.DataTextField = dsRiskClass.Tables[0].Columns["XRC_RiskClass"].ToString();
                        ddl.DataBind();
                        ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Risk Class", "Select Risk Class"));
                    }                   
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
                DropDownList ddl = (DropDownList)item.FindControl("ddlPickRiskClass");
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
                        //TextBox txtRiskCode = (TextBox)item.FindControl("txtRiskCode");
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

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {   
            if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
            {
            }
            else if (e.CommandName == RadGrid.RebindGridCommandName && e.Item.OwnerTableView.IsItemInserted)
            {
                e.Canceled = true;
            }
            else if (e.CommandName == RadGrid.EditCommandName)
            {
            }
            else if (e.CommandName == RadGrid.DeleteCommandName)
            {
            }  
            else
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
                if (!editColumn.Visible)
                    editColumn.Visible = true;
            }
        }

        protected void ddlClassType_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (ddlClassType.SelectedValue != "Select")
            //{
            //    bindRadGrid1();
            //    RadGrid1.Visible = true;
            //}
            //else
            //{
            //    RadGrid1.Visible = false;
            //}
        }
    }
}