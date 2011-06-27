using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoWerpAdmin;
using System.Data;
using System.Web.Services;
using System.Xml;
using System.Web.Script.Services;
using System.Xml.Linq;

namespace WealthERP.SuperAdmin
{
    public partial class SuperAdminMessageBroadcast : System.Web.UI.UserControl
    {
        AdviserMaintenanceBo advisermaintanencebo = new AdviserMaintenanceBo();
        protected void Page_Init(object sender, EventArgs e)
        {


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dsMessage = advisermaintanencebo.GetMessageBroadcast();
            if (dsMessage != null && dsMessage.Tables[0].Rows.Count > 0)
            {
                MessageBroadcast.Visible = true;
                if (dsMessage.Tables[0].Rows[0]["ABM_IsActive"].ToString() == "1")
                {
                    DateTime dtMessageDate = DateTime.Parse(dsMessage.Tables[0].Rows[0]["ABM_BroadCastMessageDate"].ToString());
                    lblSuperAdmnMessage.Text = "Last Sent Message:" + dsMessage.Tables[0].Rows[0]["ABM_BroadCastMessage"].ToString() + Environment.NewLine + " Sent on:" + dtMessageDate.ToString();
                }
            }
        }

        protected void SendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                AdviserMaintenanceBo advisermaintanencebo = new AdviserMaintenanceBo();
                DateTime dtExpiry = new DateTime();
                bool result = false;
                result = DateTime.TryParse(txtExpiryDate.Text.ToString(), out dtExpiry);
                advisermaintanencebo.MessageBroadcastSendMessage(MessageBox.Text, DateTime.Now, dtExpiry);
                SuccessMessage.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}