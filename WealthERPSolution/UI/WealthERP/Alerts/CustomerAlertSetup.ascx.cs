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
    public partial class CustomerAlertSetup : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        AdvisorVo adviserVo = new AdvisorVo();
        AlertsBo alertBo = new AlertsBo();
        string userType;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userBo = new UserBo();
            userVo = (UserVo)Session[SessionContents.UserVo];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            CustomerVo cusromerVo = new CustomerVo();
            cusromerVo = (CustomerVo)Session["customerVo"];
            if (!IsPostBack)
            {
                if (Session["IsCustomerDrillDown"] != null)
                {
                    txtCustomerId.Value = cusromerVo.CustomerId.ToString();
                    BindCustomerAlertSetup();
                    lblType.Visible = false;
                    ddlType.Visible = false;
                    btngo.Visible = false;
                    //gvCustomerAlertSetup.MasterTableView.IsItemInserted = false; 
                    //GridEditableItem gridEditableItem = (GridEditableItem)e.(sender);
                    //gridEditableItem.OwnerTableView.IsItemInserted = false;
                    //this.gvCustomerAlertSetup.MasterTableView.Items[0].EditFormItem.Enabled = false;
                }
            }
            txtCustomerName_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
            txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
            txtPansearch_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
            txtPansearch_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerPan";
            txtClientCode_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
            txtClientCode_autoCompleteExtender.ServiceMethod = "GetCustCode";

        }
        protected void click_Go(object sender, EventArgs e)
        {
            BindCustomerAlertSetup();
        }
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearControl();
            if (ddlType.SelectedValue == "Name")
            {
                tdtxtCustomerName.Visible = true;
                tdtxtClientCode.Visible = false;
                tdtxtPansearch.Visible = false;
            }
            else if (ddlType.SelectedValue == "Panno")
            {
                tdtxtPansearch.Visible = true;
                tdtxtCustomerName.Visible = false;
                tdtxtClientCode.Visible = false;
            }
            else if (ddlType.SelectedValue == "Clientcode")
            {
                tdtxtClientCode.Visible = true;
                tdtxtPansearch.Visible = false;
                tdtxtCustomerName.Visible = false;
            }
            else
            {
                tdtxtClientCode.Visible = false;
                tdtxtPansearch.Visible = false;
                tdtxtCustomerName.Visible = false;

            }
        }
        protected void clearControl()
        {
            txtCustomerId.Value = null;
            txtPansearch.Text = "";
            txtClientCode.Text = "";
            txtCustomerName.Text = "";

        }
        protected void BindCustomerAlertSetup()
        {
            try
            {
                DataTable dtCustomerSIPAlert = alertBo.GetCustomerSIPAlert(int.Parse(txtCustomerId.Value));
                if (dtCustomerSIPAlert.Rows.Count > 0)
                {
                    if (Cache["CustomerSIPAlert" + adviserVo.advisorId] == null)
                    {
                        Cache.Insert("CustomerSIPAlert" + adviserVo.advisorId, dtCustomerSIPAlert);
                    }
                    else
                    {
                        Cache.Remove("CustomerSIPAlert" + adviserVo.advisorId);
                        Cache.Insert("CustomerSIPAlert" + adviserVo.advisorId, dtCustomerSIPAlert);
                    }
                    gvCustomerAlertSetup.DataSource = dtCustomerSIPAlert;
                    gvCustomerAlertSetup.DataBind();
                    tblCustomerAlertSetup.Visible = true;
                }
                else
                {
                    gvCustomerAlertSetup.DataSource = dtCustomerSIPAlert;
                    gvCustomerAlertSetup.DataBind();
                    tblCustomerAlertSetup.Visible = true;
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
        protected void gvCustomerAlertSetup_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlSIPDiscription = (DropDownList)gefi.FindControl("ddlSIPDiscription");
                DropDownList ddlAlert = (DropDownList)gefi.FindControl("ddlAlert");
                BindCustomerSIP(ddlSIPDiscription);
                BindCustomerSIPRule(ddlAlert);
            }
        }
        protected void gvCustomerAlertSetup_PreRender(object sender, EventArgs e)
        {
            if (Session["IsCustomerDrillDown"] != null)
            {
                GridCommandItem commandItem = gvCustomerAlertSetup.MasterTableView.GetItems(GridItemType.CommandItem)[0] as GridCommandItem;
                commandItem.FindControl("AddNewRecordButton").Visible = false;
                commandItem.FindControl("InitInsertButton").Visible = false;
            }

        }
        protected void BindCustomerSIP(DropDownList ddlSIPDiscription)
        {
            DataTable dt = alertBo.GetCustomerSIPList(int.Parse(txtCustomerId.Value));
            ddlSIPDiscription.DataSource = dt;
            ddlSIPDiscription.DataValueField = dt.Columns["CMFSS_SystematicSetupId"].ToString();
            ddlSIPDiscription.DataTextField = dt.Columns["PASP_SchemePlanName"].ToString();
            ddlSIPDiscription.DataBind();
            ddlSIPDiscription.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void BindCustomerSIPRule(DropDownList ddlAlert)
        {
            DataTable dtSIPAlert = new DataTable();
            dtSIPAlert = alertBo.GetSIPAlertCustomerconfig();
            ddlAlert.DataSource = dtSIPAlert;
            ddlAlert.DataValueField = dtSIPAlert.Columns["AAECR_RuleId"].ToString();
            ddlAlert.DataTextField = dtSIPAlert.Columns["AAECR_RuleName"].ToString();
            ddlAlert.DataBind();
            ddlAlert.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void gvCustomerAlertSetup_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtCustomerSIPAlert = new DataTable();
            dtCustomerSIPAlert = (DataTable)Cache["CustomerSIPAlert" + adviserVo.advisorId];
            if (dtCustomerSIPAlert != null)
            {
                gvCustomerAlertSetup.DataSource = dtCustomerSIPAlert;
            }
        }
        protected void gvCustomerAlertSetup_OnItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                DropDownList ddlSIPDiscription = (DropDownList)e.Item.FindControl("ddlSIPDiscription");
                DropDownList ddlAlert = (DropDownList)e.Item.FindControl("ddlAlert");
                TextBox txtMsg = (TextBox)e.Item.FindControl("txtMsg");
                RadDatePicker txtEventSubscription = (RadDatePicker)e.Item.FindControl("txtEventSubscription");
                int count = alertBo.SIPRuleCheck(int.Parse(ddlSIPDiscription.SelectedValue), int.Parse(ddlAlert.SelectedValue));
                if (count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Same rule exist for this Scheme!!');", true);
                    return;
                }
                else
                {
                    alertBo.CreateCustomerAlertConfiguration(int.Parse(ddlSIPDiscription.SelectedValue), int.Parse(ddlAlert.SelectedValue), txtMsg.Text, Convert.ToDateTime(txtEventSubscription.SelectedDate), userVo.UserId);
                }

            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                int alertId = Convert.ToInt32(gvCustomerAlertSetup.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AES_EventSetupID"].ToString());
                alertBo.DeleteCustomerAlertConfiguration(alertId);
            }
            BindCustomerAlertSetup();
        }
        protected void radMenu_ItemClick(object sender, RadMenuEventArgs e)
        {
            int radGridClickedRowIndex;
            radGridClickedRowIndex = Convert.ToInt32(Request.Form["radGridClickedRowIndex"]);
            switch (e.Item.Text)
            {
                case "Delete":
                    gvCustomerAlertSetup.Items[radGridClickedRowIndex].Edit = true;
                    gvCustomerAlertSetup.Rebind();
                    break;
                case "Add":
                    gvCustomerAlertSetup.MasterTableView.IsItemInserted = true;
                    gvCustomerAlertSetup.Rebind();
                    break;
            }
        }
    }
}