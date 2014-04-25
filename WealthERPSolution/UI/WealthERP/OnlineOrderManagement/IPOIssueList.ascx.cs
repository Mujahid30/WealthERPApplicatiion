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
    public partial class IPOIssueList : System.Web.UI.UserControl
    {
        OnlineIPOOrderBo onlineIPOOrderBo=new OnlineIPOOrderBo();
        AdvisorVo advisorVo;
        CustomerVo customerVo;
        UserVo userVo;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["customerVo"];
            if (!Page.IsPostBack)
            {
                ddlType.SelectedValue = "Curent";
                BindIPOIssueList(GetType(ddlType.SelectedValue));
            }

        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            int type = GetType(ddlType.SelectedValue);
            BindIPOIssueList( type);
        }

         private int GetType(string ddlSelection)
        {
            int type = 0;
            if (ddlSelection == "Curent")
            {
                type = 1;
            }
            else if (ddlSelection == "Closed")
            {
                type = 2;
            }
            else
            {
                type = 3;
            }
            return type;
        }
        protected void RadGridIPOIssueList_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            int issueId = 0;

            if (e.CommandName == "Buy")
            {
                //int rowindex1 = ((GridDataItem)((LinkButton)sender).NamingContainer).RowIndex;
                //int rowindex = (rowindex1 / 2) - 1;
                //LinkButton lbButton = (LinkButton)sender;
                //GridDataItem item = (GridDataItem)lbButton.NamingContainer;
                //int IssuerId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowindex]["AIM_IssueId"].ToString());
               // categoryId = Convert.ToInt32(rgEligibleInvestorCategories.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString());

                issueId = Convert.ToInt32(RadGridIPOIssueList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIM_IssueId"].ToString());
                if (Session["PageDefaultSetting"] != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('IPOIssueTransact','&issueId=" + issueId + "')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "loadcontrol('IPOIssueTransact','&issueId=" + issueId + "')", true);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('IPOIssueTransact','&issueId=" + issueId + "')", true);
                }

            }
        }

        private void BindIPOIssueList(int type )
        {
            DataTable dtOnlineIPOIssueList = onlineIPOOrderBo.GetIPOIssueList(advisorVo.advisorId, 0, type);

            if (dtOnlineIPOIssueList.Rows.Count > 0)
            {
                if (Cache["IPOIssueList" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("IPOIssueList" + userVo.UserId.ToString(), dtOnlineIPOIssueList);
                }
                else
                {
                    Cache.Remove("IPOIssueList" + userVo.UserId.ToString());
                    Cache.Insert("IPOIssueList" + userVo.UserId.ToString(), dtOnlineIPOIssueList);
                }
                //ibtExportSummary.Visible = false;
                RadGridIPOIssueList.DataSource = dtOnlineIPOIssueList;
                RadGridIPOIssueList.DataBind();
            }
            else
            {
                //ibtExportSummary.Visible = false;
                RadGridIPOIssueList.DataSource = dtOnlineIPOIssueList;
                RadGridIPOIssueList.DataBind();

            }
        }
    }
}