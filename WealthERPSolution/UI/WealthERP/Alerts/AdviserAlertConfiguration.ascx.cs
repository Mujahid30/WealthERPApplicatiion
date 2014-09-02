using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoCommon;
using BoUser;
using WealthERP.Base;
using BoAlerts;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
namespace WealthERP.Alerts
{
    public partial class AdviserAlertConfiguration : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        AdvisorVo adviserVo = new AdvisorVo();
        AlertsBo alertBo = new AlertsBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userBo = new UserBo();
            userVo = (UserVo)Session[SessionContents.UserVo];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            BindSIPDescription();
        }

        private void BindSIP(DropDownList ddlSIPDiscription)
        {
            DataTable dtSIP = new DataTable();
            dtSIP = alertBo.GetSIPdescription();
            ddlSIPDiscription.DataSource = dtSIP;
            ddlSIPDiscription.DataValueField = dtSIP.Columns["AEL_EventID"].ToString();
            ddlSIPDiscription.DataTextField = dtSIP.Columns["AEL_EventDescription"].ToString();
            ddlSIPDiscription.DataBind();
            ddlSIPDiscription.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void ddlSIPDiscription_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void BindAlert(DropDownList ddlAlert)
        {
            DataTable dtSIPAlert = new DataTable();
            dtSIPAlert = alertBo.GetSIPAlert(1);
            ddlAlert.DataSource = dtSIPAlert;
            ddlAlert.DataValueField = dtSIPAlert.Columns["EventOperatorDataID"].ToString();
            ddlAlert.DataTextField = dtSIPAlert.Columns["EventOperatorDataID"].ToString();
            ddlAlert.DataBind();
            ddlAlert.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void BindSIPDescription()
        {
            try
            {
                DataTable dtBindSIPDescription = new DataTable();
                dtBindSIPDescription = alertBo.GetAdviserAlertConfiguration(adviserVo.advisorId);
                if (dtBindSIPDescription.Rows.Count > 0)
                {
                    if (Cache["SIPAlert" + adviserVo.advisorId] == null)
                    {
                        Cache.Insert("SIPAlert" + adviserVo.advisorId, dtBindSIPDescription);
                    }
                    else
                    {
                        Cache.Remove("SIPAlert" + adviserVo.advisorId);
                        Cache.Insert("SIPAlert" + adviserVo.advisorId, dtBindSIPDescription);
                    }
                    gvAdviserAlert.DataSource = dtBindSIPDescription;
                    gvAdviserAlert.DataBind();
                }
                else
                {
                    gvAdviserAlert.DataSource = dtBindSIPDescription;
                    gvAdviserAlert.DataBind();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserAlertConfiguration.ascx.cs:BindSIPDescription()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void gvAdviserAlert_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlSIPDiscription = (DropDownList)gefi.FindControl("ddlSIPDiscription");
                DropDownList ddlAlert = (DropDownList)gefi.FindControl("ddlAlert");
                BindSIP(ddlSIPDiscription);
                BindAlert(ddlAlert);
            }
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                int ruleId = Convert.ToInt32(gvAdviserAlert.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AAECR_RuleId"].ToString());
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlSIPDiscription = (DropDownList)gefi.FindControl("ddlSIPDiscription");
                DropDownList ddlAlert = (DropDownList)gefi.FindControl("ddlAlert");
                TextBox txtRuleType = (TextBox)e.Item.FindControl("txtRuleType");
                CheckBox chkActive = (CheckBox)e.Item.FindControl("chkActive");
                CheckBox chkOverrite = (CheckBox)e.Item.FindControl("chkOverrite");
                FillAdviserrole(ruleId, ddlSIPDiscription, ddlAlert, txtRuleType, chkActive, chkOverrite);
            }
            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;
                gridItem.ToolTip = "Right click to Add/Edit";
            }
        }
        private void FillAdviserrole(int ruleId, DropDownList ddlSIPDiscription, DropDownList ddlAlert, TextBox txtRuleType, CheckBox chkActive, CheckBox chkOverrite)
        {
            DataTable dtGetAdviserAlertConfigurationData;
            BindSIP(ddlSIPDiscription);
            BindAlert(ddlAlert);
            dtGetAdviserAlertConfigurationData = alertBo.GetAdviserAlertConfigurationData(adviserVo.advisorId, ruleId);
            if (dtGetAdviserAlertConfigurationData.Rows.Count > 0)
            {
                foreach (DataRow dr in dtGetAdviserAlertConfigurationData.Rows)
                {
                    ddlSIPDiscription.SelectedValue = dr["AEL_EventId"].ToString();
                    ddlAlert.SelectedValue = dr["AAECR_DefaultReminderDay"].ToString();
                    txtRuleType.Text = dr["AAECR_RuleName"].ToString();
                    if (dr["AAECR_IsActive"].ToString().ToLower() != "false")
                    {
                        chkActive.Checked = true;
                    }
                    if (dr["AAECR_IsOverride"].ToString().ToLower() != "false")
                    {
                        chkOverrite.Checked = true;
                    }
                }
            }

        }
        protected void gvAdviserAlert_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtBindSIPDescription = new DataTable();
            dtBindSIPDescription = (DataTable)Cache["SIPAlert" + adviserVo.advisorId];
            if (dtBindSIPDescription != null)
            {
                gvAdviserAlert.DataSource = dtBindSIPDescription;
            }
        }
        protected void gvAdviserAlert_OnItemCommand(object source, GridCommandEventArgs e)
        {
            int chkActivevalue = 0;
            int chkOverritevalue = 0;
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                DropDownList ddlSIPDiscription = (DropDownList)e.Item.FindControl("ddlSIPDiscription");
                DropDownList ddlAlert = (DropDownList)e.Item.FindControl("ddlAlert");
                TextBox txtRuleType = (TextBox)e.Item.FindControl("txtRuleType");
                CheckBox chkActive = (CheckBox)e.Item.FindControl("chkActive");
                CheckBox chkOverrite = (CheckBox)e.Item.FindControl("chkOverrite");
                if (chkActive.Checked)
                    chkActivevalue = 1;
                if (chkOverrite.Checked)
                    chkOverritevalue = 1;
                alertBo.CreateAdviserAlertConfiguration(adviserVo.advisorId, int.Parse(ddlSIPDiscription.SelectedValue), int.Parse(ddlAlert.SelectedValue), txtRuleType.Text, chkActivevalue, chkOverritevalue, userVo.UserId);
            }
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                int ruleId = Convert.ToInt32(gvAdviserAlert.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AAECR_RuleId"].ToString());
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                DropDownList ddlSIPDiscription = (DropDownList)e.Item.FindControl("ddlSIPDiscription");
                DropDownList ddlAlert = (DropDownList)e.Item.FindControl("ddlAlert");
                TextBox txtRuleType = (TextBox)e.Item.FindControl("txtRuleType");
                CheckBox chkActive = (CheckBox)e.Item.FindControl("chkActive");
                CheckBox chkOverrite = (CheckBox)e.Item.FindControl("chkOverrite");
                if (chkActive.Checked)
                    chkActivevalue = 1;
                if (chkOverrite.Checked)
                    chkOverritevalue = 1;
                alertBo.UpdateAdviserAlertConfiguration(ruleId, int.Parse(ddlSIPDiscription.SelectedValue), int.Parse(ddlAlert.SelectedValue), txtRuleType.Text, chkActivevalue, chkOverritevalue, userVo.UserId);
            }
            BindSIPDescription();
        }
        protected void radMenu_ItemClick(object sender, RadMenuEventArgs e)
        {
            int radGridClickedRowIndex;
            radGridClickedRowIndex = Convert.ToInt32(Request.Form["radGridClickedRowIndex"]);
            switch (e.Item.Text)
            {
                case "Edit":
                    gvAdviserAlert.Items[radGridClickedRowIndex].Edit = true;
                    gvAdviserAlert.Rebind();
                    break;
                case "Add":
                    gvAdviserAlert.MasterTableView.IsItemInserted = true;
                    gvAdviserAlert.Rebind();
                    break;
            }
        }
    }
}