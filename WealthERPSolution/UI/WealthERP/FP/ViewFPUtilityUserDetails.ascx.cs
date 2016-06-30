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
            DropDownList ddlAction = (DropDownList)sender;
            //RadComboBox ddlAction = (RadComboBox)sender;
            GridDataItem item = (GridDataItem)ddlAction.NamingContainer;
            ParentId = int.Parse(gvLeadList.MasterTableView.DataKeyValues[item.ItemIndex]["C_CustomerId"].ToString());

            Session["ParentIdForDelete"] = ParentId;
            customerVo = customerBo.GetCustomer(ParentId);
            Session["CustomerVo"] = customerVo;
            isGrpHead = customerBo.CheckCustomerGroupHead(ParentId);
            if (ddlAction.SelectedItem.Value.ToString() != "Profile")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
            }
            //to set portfolio Id and its details
            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
            Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
            Session["customerPortfolioVo"] = customerPortfolioVo;
            if (ddlAction.SelectedItem.Value.ToString() == "Profile")
            {
                Session["IsDashboard"] = "false";
                customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(ParentId);
                if (customerVo.IsProspect == 0)
                {
                    Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                    Session["customerPortfolioVo"] = customerPortfolioVo;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMCustomerIndividualDashboard", "loadcontrol('RMCustomerIndividualDashboard','login');", true);
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
}