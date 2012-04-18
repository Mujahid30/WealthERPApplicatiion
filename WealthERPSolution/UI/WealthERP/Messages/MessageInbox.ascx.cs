using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoCommon;
using System.Data;
using WealthERP.Base;
using Telerik.Web.UI;

namespace WealthERP.Messages
{
    public partial class MessageInbox : System.Web.UI.UserControl
    {
        UserVo userVo;
        MessageBo msgBo;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet dsInbox = new DataSet();
            msgBo = new MessageBo();

            dsInbox = msgBo.GetInboxRecords(userVo.UserId);

            if (dsInbox.Tables[0].Rows.Count > 0)
            {
                RadGrid1.DataSource = dsInbox.Tables[0];
                trContent.Visible = true;
                trNoRecords.Visible = false;
                ViewState["dsInbox"] = dsInbox;
            }
            else
            {
                // display no records found
                lblNoRecords.Visible = true;
                trContent.Visible = false;
                trNoRecords.Visible = true;
            }
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Read")
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string strKeyValue = dataItem.GetDataKeyValue("MR_MessageRecipientId").ToString();
                /*
                 *      check if the message has alreaDY been read by user. If yes, then do not update DB.
                 *      Else if not read then update DB flag.
                 */
                tblMessageHeaders.Visible = true;
                DataSet dsInbox = (DataSet)ViewState["dsInbox"];
                foreach (DataRow dr in dsInbox.Tables[0].Rows)
                {
                    if (dr["MR_MessageRecipientId"].ToString() == strKeyValue)
                    {
                        string strMessage = dr["Message"].ToString();
                        lblMessageContent.Text = strMessage;
                        lblSenderContent.Text = dr["Sender"].ToString();
                        lblSubjectContent.Text = dr["Subject"].ToString();
                        lblSentContent.Text = dataItem["Received"].Text;
                        ViewState["ReadMessRecpId"] = strKeyValue;
                        break;
                    }
                }

                if (!Boolean.Parse(dataItem["ReadByUser"].Text))
                {
                    // If message is read first time, update DB that the message is read.
                    Int64 intRecipientId = Int64.Parse(strKeyValue);
                    bool blResult = false;
                    // update DB with read flag
                    msgBo = new MessageBo();
                    blResult = msgBo.UpdateMessageReadFlag(intRecipientId);

                    if (blResult)
                    {
                        // this should retrieve only the flag values and update the existing dataset which is stored in the cache with new flag values.
                        RadGrid1.Rebind();
                    }
                }
            }
        }

        

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
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
                if (dataBoundItem["Subject"].Text.Length > 50)
                {
                    dataBoundItem["Subject"].Text = dataBoundItem["Subject"].Text.Substring(0, 50) + "...";
                }
                if (dataBoundItem["Received"].Text.Length > 30)
                {
                    dataBoundItem["Received"].Text = dataBoundItem["Received"].Text.Substring(0, 20) + "...";
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool blAnyChecked = false;
            bool blResult = false;
            bool blClear = false;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("IsDeleted");
            DataRow drDelete;

            foreach (GridDataItem item in RadGrid1.Items)
            {
                CheckBox chkbxRow = (CheckBox)item.FindControl("chkbxRow");
                if (chkbxRow != null)
                {
                    if (chkbxRow.Checked)
                    {
                        // Update flag for validation message [select atleast one to delete]
                        blAnyChecked = true;

                        drDelete = dt.NewRow();
                        string strKeyValue = item.GetDataKeyValue("MR_MessageRecipientId").ToString();
                        drDelete[0] = strKeyValue;
                        drDelete[1] = chkbxRow.Checked.ToString().ToLower();
                        dt.Rows.Add(drDelete);

                        if (ViewState["ReadMessRecpId"] != null)
                        {
                            string strKey = ViewState["ReadMessRecpId"].ToString();
                            if (strKey.Equals(strKeyValue))
                            {
                                blClear = true;
                            }
                        }
                    }
                }
            }

            dt.TableName = "Table";
            ds.Tables.Add(dt);
            ds.DataSetName = "Deleted";

            if (blAnyChecked)
            {
                msgBo = new MessageBo();
                blResult = msgBo.DeleteMessages(ds.GetXml().ToString(), 1);

                if (blResult)
                {
                    RadGrid1.Rebind();
                    if (blClear)
                    {
                        ClearMessageContents();
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Messages deleted successfully!');", true);
                }
                else
                {
                    RadGrid1.Rebind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Messages could not be deleted!');", true);
                }
            }
            else
            {
                // Display validation message that atleast one checkbox should be checked
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please select a message!');", true);
            }
        }

        private void ClearMessageContents()
        {
            lblMessageContent.Text = "";
            lblSenderContent.Text = "";
            lblSubjectContent.Text = "";
            lblSentContent.Text = "";
            tblMessageHeaders.Visible = false;
        }

        protected void hdrCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridDataItem item in RadGrid1.Items)
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