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
    public partial class OnlineAdviserCustomerIPOHoldings : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        OnlineIPOBackOfficeBo OnlineIPOBackOfficeBo = new OnlineIPOBackOfficeBo();
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

            dtGetIssueName = onlineNCDBackOfficeBo.GetIssueName(advisorVo.advisorId, "IP");
            ddlIssueName.DataSource = dtGetIssueName;
            ddlIssueName.DataValueField = dtGetIssueName.Columns["AIM_IssueId"].ToString();
            ddlIssueName.DataTextField = dtGetIssueName.Columns["AIM_IssueName"].ToString();
            ddlIssueName.DataBind();
            ddlIssueName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            BindIPOIssueHoldings();
        }
        protected void BindIPOIssueHoldings()
        {
            DataTable dtBindIPOIssueHoldings;
            dtBindIPOIssueHoldings = BindIPOHoldinsGrid(advisorVo.advisorId, Convert.ToInt32(ddlIssueName.SelectedValue.ToString()), gvIPOIssueHoldings.PageSize, gvIPOIssueHoldings.CurrentPageIndex + 1, string.Empty, out rowCount);
            gvIPOIssueHoldings.DataSource = dtBindIPOIssueHoldings;
            gvIPOIssueHoldings.VirtualItemCount = rowCount;
            gvIPOIssueHoldings.DataBind();
            pnlIPOIssueHoldings.Visible = true;
        }
        protected DataTable BindIPOHoldinsGrid(int adviserId, int aimIssueId, int pageSize, int currentPage, string customerNamefilter, out int rowCount)
        {
            DataTable dtIPOIssueList = new DataTable();
            try
            {
                dtIPOIssueList = OnlineIPOBackOfficeBo.GetIPOHoldings(adviserId, aimIssueId, pageSize, currentPage, customerNamefilter, out rowCount);
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
            return dtIPOIssueList;
        }
        protected void gvIPOIssueHoldings_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtBindIPOIssueHoldings;
            dtBindIPOIssueHoldings = BindIPOHoldinsGrid(advisorVo.advisorId, Convert.ToInt32(ddlIssueName.SelectedValue.ToString()), gvIPOIssueHoldings.PageSize, gvIPOIssueHoldings.CurrentPageIndex + 1, hdncustomerName.Value, out rowCount);
            gvIPOIssueHoldings.DataSource = dtBindIPOIssueHoldings;
            gvIPOIssueHoldings.VirtualItemCount = rowCount;
            //dtBindIPOIssueHoldings = (DataTable)Cache["IPOIssueHoldings" + advisorVo.advisorId.ToString()];
            //if (dtBindIPOIssueHoldings != null)
            //{
            //    gvIPOIssueHoldings.DataSource = dtBindIPOIssueHoldings;
            //}
        }
        //protected void btnExpandAll_Click(object sender, EventArgs e)
        //{

        //}
        protected void gvIPOIssueHoldings_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gvr = (GridDataItem)e.Item;
                int AIMissueId = int.Parse(gvr.GetDataKeyValue("AIM_IssueId").ToString());
                int orderId = int.Parse(gvr.GetDataKeyValue("CO_OrderId").ToString());
                DateTime fromDate = Convert.ToDateTime(gvr.GetDataKeyValue("AIM_OpenDate").ToString());
                DateTime toDate = Convert.ToDateTime(gvr.GetDataKeyValue("AIA_AllotmentDate").ToString());

                if (e.CommandName == "Select")
                {

                    Response.Redirect("ControlHost.aspx?pageid=OnlineAdviserCustomerIPOOrderBook&AIMissueId=" + AIMissueId + "&orderId=" + orderId + "&fromDate=" + fromDate + "&toDate=" + toDate + "", false);

                }
            }
        }
    }
}