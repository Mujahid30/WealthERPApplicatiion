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

namespace WealthERP.Advisor
{
    public partial class AdviserLandingPage : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo;
        AdviserMaintenanceBo advisermaintanencebo = new AdviserMaintenanceBo();
        UserVo userVo;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            userVo = (UserVo)Session[SessionContents.UserVo];

            if (!IsPostBack)
            {
                ShowUnreadMessageAlert();
                ShowMessageBroadcast();
            }
        }
    

        public void imgClientsClick_OnClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadCustomerGrid", "loadcontrol('AdviserCustomer','login');", true);

        }
        public void lnkbtnClientLink_OnClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadCustomerGrid", "loadcontrol('AdviserCustomer','login');", true);

        }
        public void lnkbtnUploads_OnClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('CustomerUpload','login');", true);

        }
        public void imgUploads_OnClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadUploads", "loadcontrol('CustomerUpload','login');", true);

        }
        public void imgOrderentry_OnClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('OrderEntry','login');", true);

        }
        public void lnkbtnOrderEntry_OnClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('OrderEntry','login');", true);

        }
        public void imgBusinessMIS_OnClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadBusinessMIS", "loadcontrol('MultiProductMIS','login');", true);

        }
        public void lnkbtnBusinessMIS_OnClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadBusinessMIS", "loadcontrol('MultiProductMIS','login');", true);

        }
        public void lnkbtnInbox_OnClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadInbox", "loadcontrol('MessageInbox','login');", true);

        }
        public void imgInbox_OnClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadInbox", "loadcontrol('MessageInbox','login');", true);

        }

        private void ShowUnreadMessageAlert()
        {
            MessageBo msgBo = new MessageBo();

            // Get unread messages from the DB
            int intCount = 0;
            int flavourId = 0;
            intCount = msgBo.GetUnreadMessageCount(userVo.UserId, out flavourId);
            if (intCount > 0)
            {
                if (Session[SessionContents.UserTopRole] == "Admin" & flavourId == 10)
                {
                    lnkbtnInbox.Text = "Inbox " + "(" + intCount + ")";
                    imgInbox.ImageUrl = "~/Images/msgUnRead.png";
                }
            }
            else
            {
                lnkbtnInbox.Text = "Inbox " + "(" + intCount + ")";
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

    }
}