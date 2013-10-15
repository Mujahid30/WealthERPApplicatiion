using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using WealthERP.Base;
using BoCommon;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using System.Data;

namespace WealthERP.OnlineOrderManagement
{
    public partial class NCDIssueBooks : System.Web.UI.UserControl
    {
        OnlineBondOrderBo OnlineBondBo = new OnlineBondOrderBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStructureRuleGrid();
            }
        }
        protected void BindStructureRuleGrid()
        {
            //DataSet dsStructureRules = OnlineBondBo.GetAdviserCommissionStructureRules(1,2);
            DataSet dsStructureRules = OnlineBondBo.GetAdviserCommissionStructureRules(1, 2);
            if (dsStructureRules.Tables[0].Rows.Count > 0)
                ibtExportSummary.Visible = true;
            else
                ibtExportSummary.Visible = false;

            gvCommMgmt.DataSource = dsStructureRules.Tables[0];
            gvCommMgmt.DataBind();
            //Cache.Insert(userVo.UserId.ToString() + "CommissionStructureRule", dsStructureRules.Tables[0]);
        }

        protected void llPurchase_Click(object sender, EventArgs e)
        {
            //string sActionName = ((DropDownList)sender).SelectedItem.Text;
            //string sStructId = ((DropDownList)sender).SelectedValue;
            //DropDownList ddlAction = (DropDownList)sender;
            LinkButton lbButton = (LinkButton)sender;
            GridDataItem item = (GridDataItem)lbButton.NamingContainer;
            int IssuerId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[item.ItemIndex]["PFIIM_IssuerId"].ToString());
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('ReceivableSetup','IssuerId=" + IssuerId + "');", true);
            //string prodType = this.ddProduct.SelectedValue;

            //switch (ddlAction.SelectedValue)
            //{
            //    case "ViewSTDetails":
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('ReceivableSetup','StructureId=" + structureId + "');", true);
            //        break;
            //    case "ManageSchemeMapping":
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('CommissionStructureToSchemeMapping','ID=" + structureId + "&p=" + prodType + "');", true);
            //        break;
            //    default:
            //        return;
            //}
        }

    }
}