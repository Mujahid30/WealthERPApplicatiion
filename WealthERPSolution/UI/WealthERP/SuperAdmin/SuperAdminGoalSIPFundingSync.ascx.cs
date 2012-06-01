using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoSuperAdmin;
using System.Data;

namespace WealthERP.SuperAdmin
{
    public partial class SuperAdminGoalSIPFundingSync : System.Web.UI.UserControl
    {
        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDDLAdviserList();
            }
        }

        protected void BindDDLAdviserList()
        {
            DataTable dtAdviserList = new DataTable();
            dtAdviserList = superAdminOpsBo.GetAdviserListHavingSIPGoalFunding();
            
            if (dtAdviserList.Rows.Count > 0)
            {
                ddlAdviserList.DataSource = dtAdviserList;
                ddlAdviserList.DataTextField = "A_OrgName";
                ddlAdviserList.DataValueField = "A_AdviserId";
                ddlAdviserList.DataBind();
            }
                ddlAdviserList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }

        protected void btnSubmitSync_Click(object sender, EventArgs e)
        {
            int adviserId = int.Parse(ddlAdviserList.SelectedValue);
            bool result;
           result= superAdminOpsBo.SyncSIPtoGoal(adviserId);
           if (result)
           {
               // Success Message
               ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Sync Completed');", true);
               //msgGoalSynccomplete.Visible = true;
           }
           else
           {
               // Failure Message
              ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Transaction not found for Sync');", true);
               //msgGoalSyncincomplete.Visible = true;
           }

        }
        protected void btnSubmitfolio_Click(object sender, EventArgs e)
        {
            int adviserId = int.Parse(ddlAdviserList.SelectedValue);
            bool isComplete = false;
            isComplete = superAdminOpsBo.FolioStartDate(adviserId);
            if (isComplete == true)
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Folio Start Date Updated');", true);
                msgSyncComplete.Visible = true;
            else
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Folio Start Date not updated');", true);
                msgSyncincomplete.Visible = true;
         }
    }
}