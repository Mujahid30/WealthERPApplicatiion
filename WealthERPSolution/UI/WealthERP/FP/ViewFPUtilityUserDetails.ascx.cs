using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCustomerRiskProfiling;
using BoCommon;
using BoUser;
using VoUser;
using Telerik.Web.UI;
using WealthERP.Base;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoCustomerProfiling;

namespace WealthERP.FP
{
    public partial class ViewFPUtilityUserDetails : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            if (!IsPostBack)
                BindGrid();
        }


        protected void gvLeadList_OnNeedDataSource(Object sender, GridNeedDataSourceEventArgs e)
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["gvLeadList"];
            gvLeadList.DataSource = ds;
        }
        protected void gvLeadList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //LinkButton EditLinkEmail = (LinkButton)e.Item.FindControl("EditLinkEmail");
                //EditLinkEmail.OnClientClick = String.Format("return ShowEditForm('{0}','{1}');", "?setupId=" + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CTNS_Id"] + "&FormatType=Email", e.Item.ItemIndex);
                //LinkButton EditLinkSMS = (LinkButton)e.Item.FindControl("EditLinkSMS");
                //EditLinkSMS.OnClientClick = String.Format("return ShowEditForm('{0}','{1}');", "?setupId=" + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CTNS_Id"] + "&FormatType=SMS", e.Item.ItemIndex);
                //LinkButton EditLinkDashBoard = (LinkButton)e.Item.FindControl("EditLinkDashBoard");
                //EditLinkDashBoard.OnClientClick = String.Format("return ShowEditForm('{0}','{1}');", "?setupId=" + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CTNS_Id"] + "&FormatType=DashBoard", e.Item.ItemIndex);


            }
        }
        protected void BindGrid()
        {
            RiskProfileBo bo = new RiskProfileBo();
            DataSet ds = new DataSet();
            ds = bo.GetFPUtilityUserDetailsList();
            gvLeadList.DataSource = ds;
            gvLeadList.DataBind();
            gvLeadList.Visible = true;
            if (Cache["gvLeadList"] == null)
            {
                Cache.Insert("gvLeadList", ds);
            }
            else
            {
                Cache.Remove("gvLeadList");
                Cache.Insert("gvLeadList", ds);
            }
        }
        protected void ddlActionForProspect_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int userId = 0;
            UserBo userBo = new UserBo();
            bool isGrpHead = false;
            CustomerVo customerVo = new CustomerVo();
            CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
            PortfolioBo portfolioBo = new PortfolioBo();
            CustomerBo customerBo = new CustomerBo();
            int ParentId;
            if (Session[SessionContents.PortfolioId] != null)
            {
                Session.Remove(SessionContents.PortfolioId);
            }
            LinkButton lnkAction = (LinkButton)sender;
            //RadComboBox ddlAction = (RadComboBox)sender;
            GridDataItem item = (GridDataItem)lnkAction.NamingContainer;
            ParentId = int.Parse(gvLeadList.MasterTableView.DataKeyValues[item.ItemIndex]["C_CustomerId"].ToString());

            Session["ParentIdForDelete"] = ParentId;
            customerVo = customerBo.GetCustomer(ParentId);
            Session["CustomerVo"] = customerVo;
            isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);

            //to set portfolio Id and its details
            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
            Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
            Session["customerPortfolioVo"] = customerPortfolioVo;

            Session["IsDashboard"] = "false";
            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
            if (customerVo.IsProspect == 0)
            {
                //Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                //Session["customerPortfolioVo"] = customerPortfolioVo;
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMCustomerIndividualDashboard", "loadcontrol('RMCustomerIndividualDashboard','login');", true);
            }
            else
            {
                isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
                if (isGrpHead == false)
                {
                    ParentId = customerBo.GetCustomerGroupHead(ParentId);
                }
                else
                {
                    ParentId = customerVo.CustomerId;
                }
                Session[SessionContents.FPS_ProspectList_CustomerId] = ParentId;
                Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddProspectList','login');", true);
                //Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
            }

        }
    }
}