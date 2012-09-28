using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using VoUser;
using WealthERP.Base;

namespace WealthERP.Customer
{
    public partial class CustomerDashBoardShortcut : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo;
        UserVo userVo;
        CustomerVo customerVo = new CustomerVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            userVo = (UserVo)Session[SessionContents.UserVo];
            customerVo = (CustomerVo)Session["CustomerVo"];

        }

        protected void lnkbtnCustomerDashBoard_Click(object sender, EventArgs e)
        {
            Session["NodeType"] = "CustomerDashBoard";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "AdvisorRMCustIndiDashboard", "loadcontrol('AdvisorRMCustIndiDashboard','login');", true);

        }
        protected void imgCusDashBoard_Click(object sender, ImageClickEventArgs e)
        {
            Session["NodeType"] = "CustomerDashBoard";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "AdvisorRMCustIndiDashboard", "loadcontrol('AdvisorRMCustIndiDashboard','login');", true);
        }

        protected void lnkbtnFPDashBoard_Click(object sender, EventArgs e)
        {
            Session["NodeType"] = "FPDashBoard";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CustomerFPDashBoard", "loadcontrol('CustomerFPDashBoard','login');", true);
        }
        protected void imgFP_Click(object sender, ImageClickEventArgs e)
        {
            Session["NodeType"] = "FPDashBoard";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CustomerFPDashBoard", "loadcontrol('CustomerFPDashBoard','login');", true);
        }

        protected void lnkbtnProductOrder_Click(object sender, EventArgs e)
        {
            Session["NodeType"] = "Order";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CustomerOrderList", "loadcontrol('CustomerOrderList','login');", true);
        }

        protected void lnkbtnMF_Click(object sender, EventArgs e)
        {
            Session["NodeType"] = "MF";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ViewMutualFundPortfolio", "loadcontrol('ViewMutualFundPortfolio','login');", true);
        }

        protected void lnkbtnEquity_Click(object sender, EventArgs e)
        {
            Session["NodeType"] = "Equity";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ViewEquityPortfolios", "loadcontrol('ViewEquityPortfolios','login');", true);
        }

        protected void lnkbtnAlerts_Click(object sender, EventArgs e)
        {
            Session["NodeType"] = "Notification";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMAlertNotifications", "loadcontrol('RMAlertNotifications','login');", true);
        }

        protected void imgOrderentry_Click(object sender, ImageClickEventArgs e)
        {
            Session["NodeType"] = "Order";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CustomerOrderList", "loadcontrol('CustomerOrderList','login');", true);
        }

        protected void imgbtnMF_Click(object sender, ImageClickEventArgs e)
        {
            Session["NodeType"] = "MF";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ViewMutualFundPortfolio", "loadcontrol('ViewMutualFundPortfolio','login');", true);
        }

        protected void imgbtnEquity_Click(object sender, ImageClickEventArgs e)
        {
            Session["NodeType"] = "Equity";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ViewEquityPortfolios", "loadcontrol('ViewEquityPortfolios','login');", true);
        }

        protected void imgbtnNotification_Click(object sender, ImageClickEventArgs e)
        {
            Session["NodeType"] = "Notification";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndi", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMAlertNotifications", "loadcontrol('RMAlertNotifications','login');", true);
        }

       

        

        
    }
}