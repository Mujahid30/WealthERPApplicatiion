using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WealthERP.Base;
using BoCustomerProfiling;

namespace WealthERP.FP
{
    public partial class ProspectList : System.Web.UI.UserControl
    {
        CustomerBo customerbo = new CustomerBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
        }
        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }
        
        protected void ddlProspectList_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int customerId = 0;
            int selectedRow = 0;
            GridDataItem gdi;
            try
            {
                RadComboBox rcb = (RadComboBox)sender;
                gdi = (GridDataItem)rcb.NamingContainer;
                selectedRow = gdi.ItemIndex;
                customerId = int.Parse((RadGrid1.MasterTableView.DataKeyValues[selectedRow]["C_CustomerId"].ToString()));
                if (customerId != 0)
                {
                    Session[SessionContents.FPS_ProspectList_CustomerId] = customerId;
                }
                if (e.Value == "ViewProfile")
                {
                    Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddProspectList','login');", true);
                }
                if (e.Value == "FinancialPlanning")
                {
                    Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
                    Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "View";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerProspect','login');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        
    }
}