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
using System.Configuration;
using BoOps;
using VOAssociates;
using Telerik.Web.UI;
namespace WealthERP.Advisor
{
    public partial class AdviserLandingPage : System.Web.UI.UserControl
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
                //ShowUnreadMessageAlert();
                ShowMessageBroadcast();
                BindProductwiseAuthenticated();

            }
            if (advisorVo.advisorId == Convert.ToInt32(ConfigurationSettings.AppSettings["ONLINE_ADVISER"]))
            {
                tdorderentry.Visible = false;
                tdfpclient.Visible = false;
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
        public void lnkbtnUploads_OnClick(object sender, EventArgs e)
        {
            if (advisorVo.IsOpsEnable == 1)
            {
                //Session["NodeType"] = "CustomerUpload";
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('CustomerUpload','login');", true);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Functionality is disabled for current Login. Please Contact Administrator');", true);
            }
            else
            {
                Session["NodeType"] = "CustomerUpload";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('CustomerUpload','login');", true);
            }
        }
        public void imgUploads_OnClick(object sender, EventArgs e)
        {
            if (advisorVo.IsOpsEnable == 1)
            {
                Session["NodeType"] = "CustomerUpload";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('CustomerUpload','login');", true);
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Functionality is disabled for current Login. Please Contact Administrator');", true);
            }
            else
            {
                Session["NodeType"] = "CustomerUpload";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('CustomerUpload','login');", true);
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
        public void imgBusinessMIS_OnClick(object sender, EventArgs e)
        {
            //if (advisorVo.IsOpsEnable == 1)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Functionality is disabled for current Login. Please Contact Administrator');", true);
            //}
            //else
            //{
                Session["NodeType"] = "MFDashBoard";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadBusinessMIS", "loadcontrol('MFDashBoard','login');", true);
            //}
        }
        public void lnkbtnBusinessMIS_OnClick(object sender, EventArgs e)
        {
            //if (advisorVo.IsOpsEnable == 1)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Functionality is disabled for current Login. Please Contact Administrator');", true);
            //}
            //else
            //{
            Session["NodeType"] = "MFDashBoard";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadBusinessMIS", "loadcontrol('MFDashBoard','login');", true);
            //}
        }
        public void lnkbtnInbox_OnClick(object sender, EventArgs e)
        {

            Session["NodeType"] = "CustomerReportsDashBoard";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('CommissionReconMIS','login');", true);

            //int flavourId = 0;
            //int.TryParse(hdfFlavourId.Value, out flavourId);

            //if (flavourId == 10)
            //{
            //    Session["NodeType"] = "CustomerReportsDashBoard";   

            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadInbox", "loadcontrol('CustomerReportsDashBoard','login');", true);
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Functionality is disabled for current Login. Please Contact Administrator');", true);

            //}
        }
        public void imgInbox_OnClick(object sender, EventArgs e)
        {
            //int flavourId = 0;
            //int.TryParse(hdfFlavourId.Value, out flavourId);

            //if (flavourId == 10)
            //{
            //    Session["NodeType"] = "CustomerReportsDashBoard";
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadInbox", "loadcontrol('CustomerReportsDashBoard','login');", true);
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Functionality is disabled for current Login. Please Contact Administrator');", true);

            //}
            Session["UserType"] = "adviser";
            //if (advisorVo.IsOpsEnable == 1)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Functionality is disabled for current Login. Please Contact Administrator');", true);
            //}
            //else
            //{
            //Session["NodeType"] = "CustomerReportsDashBoard";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('CommissionReconMIS','login');", true);
            //}
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

        private void ShowUnreadMessageAlert()
        {
            MessageBo msgBo = new MessageBo();

            // Get unread messages from the DB
            int intCount = 0;
            int flavourId = 0;
            intCount = msgBo.GetUnreadMessageCount(userVo.UserId, out flavourId);
            hdfFlavourId.Value = flavourId.ToString();
            if (intCount > 0)
            {
                lnkbtnInbox.Text = "Reports " + "(" + intCount + ")";
                imgInbox.ImageUrl = "~/Images/msgUnRead.png";
            }

            else
            {
                lnkbtnInbox.Text = "Reports " + "(" + intCount + ")";
                imgInbox.ImageUrl = "~/Images/messageread.png";
            }
        }

        protected void ShowMessageBroadcast()
        {
            DataSet dsMessage = advisermaintanencebo.GetMessageBroadcast(advisorVo.advisorId);
            if (dsMessage != null && dsMessage.Tables[0].Rows.Count > 0)
            {

                //MessageReceived.Visible = true;
                if (dsMessage.Tables[0].Rows[0]["ABM_IsActive"].ToString() == "1" && dsMessage.Tables[0].Rows[0]["ABM_BroadCastMessage"].ToString() != "")
                {
                    DateTime dtMessageDate = DateTime.Parse(dsMessage.Tables[0].Rows[0]["ABM_BroadCastMessageDate"].ToString());
                    lblSuperAdmnMessage.Text = dsMessage.Tables[0].Rows[0]["ABM_BroadCastMessage"].ToString() + Environment.NewLine + " Sent on:" + dtMessageDate.ToString();
                    //lblSuperAdmnMessage.Text+="\n Sent on:"+
                }
            }
            else
            {
                lblSuperAdmnMessage.Text = "No more Broadcast message from Super Admin.";
            }

        }
        protected void BindProductwiseAuthenticated()
        {
            string usertype = string.Empty, userType = string.Empty;
            DataTable dt;
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
            {
                dt = FIOrderBo.GetAuthenticate(advisorVo.advisorId, string.Empty);
                if (dt.Rows.Count > 0)
                {
                    lblAuthenticatedCount.Text = dt.Rows[0]["overall"].ToString();
                }
                gvAuthenticate.DataSource = dt;
                gvAuthenticate.Rebind();
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

