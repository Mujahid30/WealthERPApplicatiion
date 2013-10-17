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
    public partial class NCDIssueList : System.Web.UI.UserControl
    {

        OnlineBondOrderBo OnlineBondBo = new OnlineBondOrderBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CustId"] = "123456";
                BindStructureRuleGrid();
                BindDropDownListIssuer();
            }
        }
        protected void BindStructureRuleGrid()
        {
            DataSet dsStructureRules = OnlineBondBo.GetAdviserCommissionStructureRules(1, "2");
            if (dsStructureRules.Tables[0].Rows.Count > 0)
                ibtExportSummary.Visible = true;
            else
                ibtExportSummary.Visible = false;
            gvCommMgmt.DataSource = dsStructureRules.Tables[0];
            gvCommMgmt.DataBind();
        }
        protected void BindDropDownListIssuer()
        {
            //int IssuerId = Convert.ToInt32(ddIssuerList.SelectedValue.ToString());
            DataSet dsStructureRules = OnlineBondBo.GetLookupDataForReceivableSetUP(7, "9");

            ddlListOfBonds.DataTextField = dsStructureRules.Tables[0].Columns["PFIIM_IssuerName"].ToString();
            ddlListOfBonds.DataValueField = dsStructureRules.Tables[0].Columns["PFIIM_IssuerId"].ToString();
            ddlListOfBonds.DataSource = dsStructureRules.Tables[0];
            ddlListOfBonds.DataBind();
        }
        protected void llPurchase_Click(object sender, EventArgs e)
        {
            int rowindex1 = ((GridDataItem)((LinkButton)sender).NamingContainer).RowIndex;
            int rowindex = (rowindex1 / 2) - 1;
            LinkButton lbButton = (LinkButton)sender;
            GridDataItem item = (GridDataItem)lbButton.NamingContainer;
            string IssuerId = gvCommMgmt.MasterTableView.DataKeyValues[rowindex]["PFIIM_IssuerId"].ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueTransact','IssuerId=" + IssuerId + "');", true);

        }
        protected void btnEquityBond_Click(object sender, EventArgs e)
        {
            string CustId = Session["CustId"].ToString();
            string IssuerId = ddlListOfBonds.SelectedValue.ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueTransact','CustId=" + CustId + "'&'IssuerId=" + IssuerId + "');", true);

        }
    }
}