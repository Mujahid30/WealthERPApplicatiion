using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoCommon;
using WealthERP.Base;
using System.Data;
using Telerik.Web.UI;

namespace WealthERP.Messages
{
    public partial class MessageOutbox : System.Web.UI.UserControl
    {
        UserVo userVo;
        MessageBo msgBo;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
        }

        protected void RadGridOutbox_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet dsInbox = new DataSet();
            msgBo = new MessageBo();
            dsInbox = msgBo.GetOutboxRecords(userVo.UserId);

            if (dsInbox.Tables[0].Rows.Count > 0)
            {
                RadGridOutbox.DataSource = dsInbox.Tables[0];
                trContent.Visible = true;
                trNoRecords.Visible = false;
            }
            else
            {
                // display no records found
                trContent.Visible = false;
                trNoRecords.Visible = true;
            }
        }

        protected void RadGridOutbox_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Read")
            {
                GridDataItem dataItem = e.Item as GridDataItem;

                /*
                 * check if the message has alreaDY been read by user. If yes, then do not update DB.
                 * Else if not read then update DB flag.
                 */
                tblMessageHeaders.Visible = true;
                lblMessageContent.Text = dataItem["Message"].Text;
                lblRecipientsContent.Text = dataItem["To"].Text;
                lblSubjectContent.Text = dataItem["Subject"].Text;
                lblSentContent.Text = dataItem["Sent"].Text;

            }
        }

        protected void RadGridOutbox_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox rbCmBx = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
                rbCmBx.Visible = false;

                Label lblPageSize = (Label)e.Item.FindControl("ChangePageSizeLabel");
                lblPageSize.Text = "";
            }
        }
    }
}