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
                ViewState["dsOutbox"] = dsInbox;
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
                string strKeyValue = dataItem.GetDataKeyValue("M_MessageId").ToString();

                /*
                 * check if the message has alreaDY been read by user. If yes, then do not update DB.
                 * Else if not read then update DB flag.
                 */
                tblMessageHeaders.Visible = true;
                DataSet dsOutbox = (DataSet)ViewState["dsOutbox"];
                foreach (DataRow dr in dsOutbox.Tables[0].Rows)
                {
                    if (dr["M_MessageId"].ToString() == strKeyValue)
                    {
                        string strMessage = dr["Message"].ToString();
                        string result = string.Empty;
                        for (int i = 0; i < strMessage.Length; i++)
                            result += (i % 100 == 0 && i != 0) ? (strMessage[i].ToString() + "<br/>") : strMessage[i].ToString();
                        lblMessageContent.Text = result;
                        lblRecipientsContent.Text = dr["Recipients"].ToString();
                        lblSubjectContent.Text = dr["Subject"].ToString();
                        lblSentContent.Text = dataItem["Sent"].Text;
                        break;
                    }
                }
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
            else if (e.Item is GridDataItem)
            {
                GridDataItem dataBoundItem = e.Item as GridDataItem;
                if (dataBoundItem["Subject"].Text.Length > 75)
                {
                    dataBoundItem["Subject"].Text = dataBoundItem["Subject"].Text.Substring(0, 75) + ".....";
                }
                if (dataBoundItem["To"].Text.Length > 30)
                {
                    dataBoundItem["To"].Text = dataBoundItem["To"].Text.Substring(0, 30) + ".....";
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool blAnyChecked = false;
            bool blResult = false;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("IsDeleted");
            DataRow drDelete;

            foreach (GridDataItem item in RadGridOutbox.Items)
            {
                CheckBox chkbxRow = (CheckBox)item.FindControl("chkbxRow");
                if (chkbxRow != null)
                {
                    if (chkbxRow.Checked)
                    {
                        // Update flag for validation message [select atleast one to delete]
                        blAnyChecked = true;

                        drDelete = dt.NewRow();
                        string strKeyValue = item.GetDataKeyValue("M_MessageId").ToString();
                        drDelete[0] = strKeyValue;
                        drDelete[1] = chkbxRow.Checked.ToString().ToLower();
                        dt.Rows.Add(drDelete);
                    }
                }
            }
            dt.TableName = "Table";
            ds.Tables.Add(dt);
            ds.DataSetName = "Deleted";

            if (blAnyChecked)
            {
                msgBo = new MessageBo();
                blResult = msgBo.DeleteMessages(ds.GetXml().ToString(), 0);

                if (blResult)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Messages deleted successfully!');", true);
                    RadGridOutbox.Rebind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Messages could not be deleted!');", true);
                    RadGridOutbox.Rebind();
                }
            }
            else
            {
                // Display validation message that atleast one checkbox should be checked
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please select a message!');", true);
            }
        }

        protected void hdrCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridDataItem item in RadGridOutbox.Items)
            {
                CheckBox hdrCheckBx = (CheckBox)sender;
                CheckBox chkbxRow = (CheckBox)item.FindControl("chkbxRow");
                if (hdrCheckBx != null && chkbxRow != null)
                {
                    chkbxRow.Checked = hdrCheckBx.Checked;
                }
            }
        }

    }
}