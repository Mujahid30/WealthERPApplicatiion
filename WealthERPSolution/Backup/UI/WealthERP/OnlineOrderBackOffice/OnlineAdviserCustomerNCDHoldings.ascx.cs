using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using WealthERP.Base;
using System.Collections.Specialized;
using System.Collections;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoUser;
using BoCommon;
using BoOnlineOrderManagement;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineAdviserCustomerNCDHoldings : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        int rowCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            BindIssueName();
        }
        protected void BindIssueName()
        {
            DataTable dtGetIssueName = new DataTable();

            dtGetIssueName = onlineNCDBackOfficeBo.GetIssueName(advisorVo.advisorId, "FI");
            ddlIssueName.DataSource = dtGetIssueName;
            ddlIssueName.DataValueField = dtGetIssueName.Columns["AIM_IssueId"].ToString();
            ddlIssueName.DataTextField = dtGetIssueName.Columns["AIM_IssueName"].ToString();
            ddlIssueName.DataBind();
            ddlIssueName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            BindIssueHoldings();
        }
        protected void BindIssueHoldings()
        {
            DataTable dtBindIssueHoldings;
            dtBindIssueHoldings = BindHoldinsGrid(advisorVo.advisorId, Convert.ToInt32(ddlIssueName.SelectedValue.ToString()), gvNCDIssueHoldings.PageSize, gvNCDIssueHoldings.CurrentPageIndex + 1, string.Empty, out rowCount);
            gvNCDIssueHoldings.DataSource = dtBindIssueHoldings;
            gvNCDIssueHoldings.VirtualItemCount = rowCount;
            gvNCDIssueHoldings.DataBind();
            pnlNCDIssueHoldings.Visible = true;
        }
        protected DataTable BindHoldinsGrid(int adviserId, int aimIssueId, int pageSize, int currentPage, string customerNamefilter, out int rowCount)
        {
            DataTable dtNCDIssueList = new DataTable();
            try
            {
                dtNCDIssueList = onlineNCDBackOfficeBo.GetNCDHoldings(adviserId, aimIssueId, pageSize, currentPage, customerNamefilter, out rowCount);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:BindCustomerGrid()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtNCDIssueList;
        }
        protected void gvNCDIssueHoldings_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtBindIssueHoldings;
            dtBindIssueHoldings = BindHoldinsGrid(advisorVo.advisorId, Convert.ToInt32(ddlIssueName.SelectedValue.ToString()), gvNCDIssueHoldings.PageSize, gvNCDIssueHoldings.CurrentPageIndex + 1, hdncustomerName.Value, out rowCount);
            gvNCDIssueHoldings.DataSource = dtBindIssueHoldings;
            gvNCDIssueHoldings.VirtualItemCount = rowCount;
            //DataTable dtBindIssueHoldings;
            //dtBindIssueHoldings = (DataTable)Cache["NCDIssueHoldings" + advisorVo.advisorId.ToString()];
            //if (dtBindIssueHoldings != null)
            //{
            //    gvNCDIssueHoldings.DataSource = dtBindIssueHoldings;
            //}
        }
        protected void gvNCDIssueHoldings_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.FilterCommandName)
            {

                GridFilteringItem item = gvNCDIssueHoldings.MasterTableView.GetItems(GridItemType.FilteringItem)[0] as GridFilteringItem;
                gvNCDIssueHoldings.CurrentPageIndex = 0;
                hdncustomerName.Value = (item["customername"].Controls[0] as TextBox).Text;
            }
        }
        protected void btnExpandAll_Click(object sender, EventArgs e)
        {
            int strIssuerId = 0;
            LinkButton buttonlink = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)buttonlink.NamingContainer;

            strIssuerId = int.Parse(gvNCDIssueHoldings.MasterTableView.DataKeyValues[gdi.ItemIndex]["AIM_IssueId"].ToString());
            RadGrid gvchildIssue = (RadGrid)gdi.FindControl("gvChildDetails");
            Panel pnlgvChildDetails = (Panel)gdi.FindControl("pnlgvChildDetails");

            if (pnlgvChildDetails.Visible == false)
            {
                pnlgvChildDetails.Visible = true;
                buttonlink.Text = "-";
            }
            else if (pnlgvChildDetails.Visible == true)
            {
                pnlgvChildDetails.Visible = false;
                buttonlink.Text = "+";
            }
            DataTable dtIssueDetail;
            DataSet dsBindIssueHoldingsSub;
            dsBindIssueHoldingsSub = onlineNCDBackOfficeBo.GetNCDSubHoldings(advisorVo.advisorId, strIssuerId);
            dtIssueDetail = dsBindIssueHoldingsSub.Tables[0];
            gvchildIssue.DataSource = dtIssueDetail;
            gvchildIssue.DataBind();
        }
        protected void gvChildDetails_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid gvchildIssue = (RadGrid)sender; // Get reference to grid 
            GridDataItem nesteditem = (GridDataItem)gvchildIssue.NamingContainer;
            int strIssuerId = int.Parse(gvNCDIssueHoldings.MasterTableView.DataKeyValues[nesteditem.ItemIndex]["AIM_IssueId"].ToString()); // Get the value 
            DataTable dtIssueDetail;
            DataSet dsBindIssueHoldingsSub;
            dsBindIssueHoldingsSub = onlineNCDBackOfficeBo.GetNCDSubHoldings(advisorVo.advisorId, strIssuerId);
            dtIssueDetail = dsBindIssueHoldingsSub.Tables[0];
            gvchildIssue.DataSource = dtIssueDetail;
        }
    }
}
