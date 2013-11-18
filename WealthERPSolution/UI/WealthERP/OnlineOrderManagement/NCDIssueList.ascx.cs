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
        AdvisorVo advisorVo = new AdvisorVo();
        CustomerVo customerVo = new CustomerVo();
        int adviserId;
        int customerId;
        string IssuerId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["customerVo"];
            adviserId = advisorVo.advisorId;
            customerId = customerVo.CustomerId;
            if (!IsPostBack)
            {
                //Session["CustId"] = "123456";
                BindStructureRuleGrid();
                BindDropDownListIssuer();
            }
        }
        protected void BindStructureRuleGrid()
        {
            DataSet dsStructureRules = OnlineBondBo.GetAdviserIssuerList(adviserId, IssuerId);
            DataTable dtIssue = dsStructureRules.Tables[0];
            if (dtIssue.Rows.Count > 0)
            {
                if (Cache["NCDIssueList" + advisorVo.advisorId.ToString()] == null)
                {
                    Cache.Insert("NCDIssueList" + advisorVo.advisorId.ToString(), dtIssue);
                }
                else
                {
                    Cache.Remove("NCDIssueList" + advisorVo.advisorId.ToString());
                    Cache.Insert("NCDIssueList" + advisorVo.advisorId.ToString(), dtIssue);
                }
                ibtExportSummary.Visible = true;
                gvCommMgmt.DataSource = dtIssue;
                gvCommMgmt.DataBind();
            }
            else
            {
                ibtExportSummary.Visible = false;
                gvCommMgmt.DataSource = dtIssue;
                gvCommMgmt.DataBind();

            }

            //FILING THE DATA FOR THE CHILD GRID
           // gvCommMgmt.MasterTableView.DetailTables[0].DataSource = dsStructureRules.Tables[1];


            
        }
        protected void gvCommMgmt_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //gvCommMgmt.MasterTableView.Items[0].Expanded = true;
                //gvCommMgmt.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = false;
            }
            
        }
        protected void BindDropDownListIssuer()
        {
            //int IssuerId = Convert.ToInt32(ddIssuerList.SelectedValue.ToString());
            //DataSet dsStructureRules = OnlineBondBo.GetBindIssuerList();
            //ddlListOfBonds.DataTextField = dsStructureRules.Tables[0].Columns["PFIIM_IssuerName"].ToString();
            //ddlListOfBonds.DataValueField = dsStructureRules.Tables[0].Columns["PFIIM_IssuerId"].ToString();
            //ddlListOfBonds.DataSource = dsStructureRules.Tables[0];
            //ddlListOfBonds.DataBind(); 
        }
        protected void llPurchase_Click(object sender, EventArgs e)
        {
            int rowindex1 = ((GridDataItem)((LinkButton)sender).NamingContainer).RowIndex;
            int rowindex = (rowindex1 / 2) - 1;
            LinkButton lbButton = (LinkButton)sender;
            GridDataItem item = (GridDataItem)lbButton.NamingContainer;
            string IssuerId = gvCommMgmt.MasterTableView.DataKeyValues[rowindex]["PFIIM_IssuerId"].ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueTransact','?IssuerId=" + IssuerId + "');", true);

        }
        protected void btnEquityBond_Click(object sender, EventArgs e)
        {   
            //string CustId = Session["CustId"].ToString();
            
            string IssuerId = ddlListOfBonds.SelectedValue.ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueTransact','&customerId=" + customerVo.CustomerId + "&IssuerId=" + IssuerId + " ');", true);
           

        }
        protected void gvCommMgmt_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //DataTable dtIssueDetail;
            //string strIssuerId = string.Empty;
            //if (e.Item is GridDataItem)
            //{
            //    strIssuerId = gvCommMgmt.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PFIIM_IssuerId"].ToString();
            //    RadGrid gvchildIssue = (RadGrid)e.Item.FindControl("gvChildDetails");
            //    LinkButton buttonlink = (LinkButton)e.Item.FindControl("Detailslink");                
            //    DataSet ds = OnlineBondBo.GetIssueDetail(strIssuerId);
            //    dtIssueDetail = ds.Tables[0];
            //    gvchildIssue.DataSource = dtIssueDetail;
            //    gvchildIssue.DataBind();
            //}
        }

         protected void btnExpandAll_Click(object sender, EventArgs e)
        {
           DataTable dtIssueDetail;
           string strIssuerId = null;
           LinkButton buttonlink = (LinkButton)sender;
           GridDataItem gdi;
           gdi = (GridDataItem)buttonlink.NamingContainer;
          //  foreach (GridDataItem gvr in this.gvCommMgmt.Items)
           // {
                strIssuerId = gvCommMgmt.MasterTableView.DataKeyValues[gdi.ItemIndex]["PFIIM_IssuerId"].ToString();
                RadGrid gvchildIssue = (RadGrid)gdi.FindControl("gvChildDetails");
                //LinkButton buttonlink = (LinkButton)gvr.FindControl("Detailslink");
                Panel pnlchild = (Panel)gdi.FindControl("pnlchild");
                //DataSet ds = OnlineBondBo.GetIssueDetail(strIssuerId);
                //dtIssueDetail = ds.Tables[0];
                //gvchildIssue.DataSource = dtIssueDetail;
                //gvchildIssue.DataBind();
                //gvchildIssue.Visible = true;
                //pnlchild.Visible = true;
            //}
                if (pnlchild.Visible == false)
                {
                    pnlchild.Visible = true;
                    buttonlink.Text = "-";
                }
                else if (pnlchild.Visible == true)
                {
                    pnlchild.Visible = false;
                    buttonlink.Text = "+";
                }
                DataSet ds = OnlineBondBo.GetIssueDetail(strIssuerId);
                dtIssueDetail = ds.Tables[0];
                gvchildIssue.DataSource = dtIssueDetail;
                gvchildIssue.DataBind();
         }
        protected void gvChildDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
        
        
        
        }
        protected void gvCommMgmt_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtIssueDetail;
            dtIssueDetail=(DataTable)Cache["NCDIssueList" + advisorVo.advisorId.ToString()];
            if (dtIssueDetail != null)
            {
                gvCommMgmt.DataSource = dtIssueDetail;
            }
            
        }
        protected void gvChildDetails_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid gvchildIssue = (RadGrid)sender; // Get reference to grid 
            GridDataItem nesteditem = (GridDataItem)gvchildIssue.NamingContainer;
            string strIssuerId = gvCommMgmt.MasterTableView.DataKeyValues[nesteditem.ItemIndex]["PFIIM_IssuerId"].ToString(); // Get the value 
            DataSet ds = OnlineBondBo.GetIssueDetail(strIssuerId);
            gvchildIssue.DataSource = ds.Tables[0]; 
        }
    }
}