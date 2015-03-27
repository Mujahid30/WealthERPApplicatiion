using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using VoUser;
using WealthERP.Base;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoWerpAdmin;
using System.Data;
using BoOps;
using Telerik.Web.UI;
using VOAssociates;

namespace WealthERP.Advisor
{
    public partial class SalesDashBoard : System.Web.UI.UserControl
    {

        AdvisorVo advisorVo;
        AdviserMaintenanceBo advisermaintanencebo = new AdviserMaintenanceBo();
        UserVo userVo;
        FIOrderBo FIOrderBo = new FIOrderBo();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            userVo = (UserVo)Session[SessionContents.UserVo];
            if (!IsPostBack)
            {
                BindProductwiseAuthenticated();

            }
            
        }


        public void imgClientsClick_OnClick(object sender, EventArgs e)
        {
            Session["Customer"] = "Customer";
            Session["NodeType"] = "AdviserCustomer";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            if (advisorVo.IsOpsEnable == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadCustomerGrid", "loadcontrol('AdviserCustomer','login');", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadCustomerGrid", "loadcontrol('AdviserCustomer','login');", true);
            }
        }
        public void lnkbtnClientLink_OnClick(object sender, EventArgs e)
        {
            Session["Customer"] = "Customer";
            Session["NodeType"] = "AdviserCustomer";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            if (advisorVo.IsOpsEnable == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadCustomerGrid", "loadcontrol('AdviserCustomer','login');", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadCustomerGrid", "loadcontrol('AdviserCustomer','login');", true);
            }
        }
      
        public void imgOrderentry_OnClick(object sender, EventArgs e)
        {
            Session["NodeType"] = "MFOrderEntry";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('MFOrderEntry','login');", true);

        }
        public void lnkbtnOrderEntry_OnClick(object sender, EventArgs e)
        {
            Session["NodeType"] = "MFOrderEntry";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('MFOrderEntry','login');", true);

        }
        public void ImageNCDORder_OnClick(object sender, EventArgs e)
        {
            Session["NodeType"] = "MFOrderEntry";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('NCDIssueTransactOffline','login');", true);

        }
        public void lnkNCDOrder_OnClick(object sender, EventArgs e)
        {
            Session["NodeType"] = "MFOrderEntry";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('NCDIssueTransactOffline','login');", true);

        }
        public void imgIPOOrder_OnClick(object sender, EventArgs e)
        {
            Session["NodeType"] = "MFOrderEntry";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('IPOIssueTransactOffline','login');", true);

        }
        public void lnkIPOOrder_OnClick(object sender, EventArgs e)
        {
            Session["NodeType"] = "MFOrderEntry";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('IPOIssueTransactOffline','login');", true);

        }
        public void Img54ECEntry_OnClick(object sender, EventArgs e)
        {
            Session["NodeType"] = "MFOrderEntry";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('FixedIncome54ECOrderEntry','login');", true);

        }
        public void lnk54ECOrder_OnClick(object sender, EventArgs e)
        {
            Session["NodeType"] = "MFOrderEntry";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('FixedIncome54ECOrderEntry','login');", true);

        }
        public void imgbtnFPClients_OnClick(object sender, EventArgs e)
        {
            Session["NodeType"] = "AddProspectList";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('AddProspectList','login');", true);

        }
        public void lnkbtnFPClients_OnClick(object sender, EventArgs e)
        {

            Session["NodeType"] = "AddProspectList";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('AddProspectList','login');", true);

        }
        protected void BindProductwiseAuthenticated()
        {
            string usertype = string.Empty, userType = string.Empty;
            DataTable dt;
          
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
                {
                    userType = "associates";
                    associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                    if (associateuserheirarchyVo.AgentCode != null)
                    {
                        usertype = FIOrderBo.GetUserType(advisorVo.advisorId, associateuserheirarchyVo.AdviserAgentId);
                        if (usertype == "RM" || usertype == "BM")
                        {
                            dt = FIOrderBo.GetAuthenticate(advisorVo.advisorId, associateuserheirarchyVo.AgentCode);
                            if (dt.Rows.Count > 0)
                            {
                                lblAuthenticatedCount.Text = dt.Rows[0]["overall"].ToString();
                            }
                            gvAuthenticate.DataSource = dt;
                            gvAuthenticate.Rebind();
                            tdHeader.Visible = true;
                        }
                        else
                        {
                            tdHeader.Visible = false;
                        }

                    }
                }
            
        }
        protected void lnkProductWise_OnClick(object sender, EventArgs e)
        {
            LinkButton lnkProductWise = (LinkButton)sender;
            GridDataItem gdi = (GridDataItem)lnkProductWise.NamingContainer;
            string category = gvAuthenticate.MasterTableView.DataKeyValues[gdi.ItemIndex]["PAIC_AssetInstrumentCategoryCode"].ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FixedIncome54ECOrderBook", "loadcontrol('FixedIncome54ECOrderBook','&category=" + category + " ');", true);
        }
    }
}