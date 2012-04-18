using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using WealthERP.Base;
using BoCommon;
using DanLudwig;
using System.Data;
using System.Text;
using VoCommon;
using System.Text.RegularExpressions;

namespace WealthERP.Messages
{
    public partial class MessageCompose : System.Web.UI.UserControl
    {
        UserVo userVo;
        string strUserTypeSuperAdmin = "superadmin";
        string strUserTypeAdvisor = "advisor";
        string strUserRoleAdmin = "Admin";
        string strUserRoleBM = "BM";
        string strUserRoleRM = "RM"; // Future use
        string strUserRoleResearch = "Research";
        string strUserRoleOps = "Ops";
        int userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            userId = userVo.UserId;
            if (!IsPostBack)
            {
                RoleCheckboxAccessibility();
            }
        }

        private void RoleCheckboxAccessibility()
        {
            if (userVo.UserType.ToLower().Equals(strUserTypeSuperAdmin))
            {
                // super admin logs in
                BindDefaultUsers(strUserTypeSuperAdmin);
            }
            else if (userVo.UserType.ToLower().Equals(strUserTypeAdvisor))
            {
                if (userVo.RoleList.Contains(strUserRoleAdmin))
                {
                    // Advisor admin logged in
                    List<int> lstRolesIds = new List<int>();
                    lstRolesIds.Add(1000);
                    lstRolesIds.Add(1003);
                    RemoveRoleCheckboxes(lstRolesIds);
                }
                else if (userVo.RoleList.Contains(strUserRoleBM))
                {
                    // Advisor BM logged in
                    List<int> lstRolesIds = new List<int>();
                    lstRolesIds.Add(1000);
                    lstRolesIds.Add(1002);
                    lstRolesIds.Add(1003);
                    RemoveRoleCheckboxes(lstRolesIds);
                }
                else if (userVo.RoleList.Contains(strUserRoleRM))
                {
                    // Advisor RM logged in
                    BindDefaultUsers(strUserRoleRM);
                }
                else if (userVo.RoleList.Contains(strUserRoleOps))
                {
                    // Advisor Ops logged in
                    List<int> lstRolesIds = new List<int>();
                    lstRolesIds.Add(1003);
                    lstRolesIds.Add(1004);
                    RemoveRoleCheckboxes(lstRolesIds);
                }
            }
        }

        private void BindDefaultUsers(string strSessionRole)
        {
            Session[SessionContents.CurrentUserRole] = strSessionRole;
            chkbxAll.Visible = false;
            trSelectStaff.Visible = false;
            BindUserList();
        }

        private void RemoveRoleCheckboxes(List<int> lstRolesIds)
        {
            for (int i = 0; i < ChkBxRoleList.Items.Count; i++)
            {
                int currentItem = Int32.Parse(ChkBxRoleList.Items[i].Value.ToString());
                foreach (int intRole in lstRolesIds)
                {
                    if (currentItem == intRole)
                    {
                        // Remove Advisor checkbox
                        ChkBxRoleList.Items.RemoveAt(i);
                        --i;
                    }
                }
            }
        }

        protected void ChkBxRoleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LBUser.Items.Clear();
            LBSelectedUser.Items.Clear();


            BindUserList();
        }

        private void BindUserList()
        {
            // Check if any of the checkboxes are checked. If yes, bind. Else dont do anything
            bool blBindUsers = false;
            StringBuilder sbRoleList = new StringBuilder();

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower().Equals(strUserTypeSuperAdmin))
            {
                // If Superadmin, then bind all advisers by default
                blBindUsers = AddDefaultUserRole(sbRoleList, "1000");
            }
            else if (Session[SessionContents.CurrentUserRole].ToString().Equals(strUserRoleRM))
            {
                // If RM, then bind all customers by default
                blBindUsers = AddDefaultUserRole(sbRoleList, "1003");
            }
            else
            {
                foreach (ListItem li in ChkBxRoleList.Items)
                {
                    if (li.Selected)
                    {
                        blBindUsers = true;
                        break;
                    }
                }
            }

            if (blBindUsers)
            {
                MessageBo msgBo = new MessageBo();
                DataTable dtUserList = new DataTable();

                if (!(Session[SessionContents.CurrentUserRole].ToString().Equals(strUserTypeSuperAdmin)) && !(Session[SessionContents.CurrentUserRole].ToString().Equals(strUserRoleRM)))
                {
                    foreach (ListItem li in ChkBxRoleList.Items)
                    {
                        if (li.Selected)
                        {
                            if (li.Value != "0")
                            {
                                sbRoleList.Append(li.Value);
                                sbRoleList.Append(",");
                            }
                        }
                    }
                    sbRoleList.Remove(sbRoleList.Length - 1, 1);
                }

                dtUserList = (msgBo.GetUserListSpecificToRole(Session[SessionContents.CurrentUserRole].ToString(), userVo.UserId, sbRoleList.ToString())).Tables[0];

                if (dtUserList.Rows.Count > 0)
                {
                    LBUser.DataSource = dtUserList;
                    LBUser.DataTextField = "Name";
                    LBUser.DataValueField = "UserId";
                    LBUser.DataBind();
                }
            }
        }

        private bool AddDefaultUserRole(StringBuilder sbRoleList, string strRole)
        {
            bool blBindUsers = true;
            sbRoleList.Append(strRole);
            return blBindUsers;
        }

        protected void AddSelected_Click(object sender, EventArgs e)
        {
            this.moveSelectedItems(LBUser, LBSelectedUser, false);
            SelectLastItem(LBSelectedUser);
        }

        protected void RemoveSelected_Click(object sender, EventArgs e)
        {
            this.moveSelectedItems(LBSelectedUser, LBUser, false);
            SelectLastItem(LBSelectedUser);
        }

        protected void SelectAll_Click(object sender, EventArgs e)
        {
            this.moveSelectedItems(LBUser, LBSelectedUser, true);
            LBSelectedUser.Items[0].Selected = true;
        }

        protected void RemoveAll_Click(object sender, EventArgs e)
        {
            this.moveSelectedItems(LBSelectedUser, LBUser, true);
        }

        /// <summary>
        /// This will add selected list Items(Customer) From One Lst to Other List. Author:Pramod 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="moveAllItems"></param>
        private void moveSelectedItems(DanLudwig.Controls.Web.ListBox source, DanLudwig.Controls.Web.ListBox target, bool moveAllItems)
        {
            // loop through all source items to find selected ones
            for (int i = source.Items.Count - 1; i >= 0; i--)
            {
                ListItem item = source.Items[i];
                if (moveAllItems)
                    item.Selected = true;

                if (item.Selected)
                {
                    // if the target already contains items, loop through
                    // them to place this new item in correct sorted order
                    if (target.Items.Count > 0)
                    {
                        for (int ii = 0; ii < target.Items.Count; ii++)
                        {
                            if (target.Items[ii].Text.CompareTo(item.Text) > 0)
                            {
                                target.Items.Insert(ii, item);
                                item.Selected = false;
                                break;
                            }
                        }
                    }

                    // if item is still selected, it must be appended
                    if (item.Selected)
                    {
                        target.Items.Add(item);
                        item.Selected = false;
                    }

                    // remove the item from the source list
                    source.Items.Remove(item);
                }
            }
        }

        public void SelectLastItem(DanLudwig.Controls.Web.ListBox ListBox1)
        {
            for (int i = ListBox1.Items.Count - 1; i >= 0; i--)
            {
                ListItem item = ListBox1.Items[i];
                if (i == ListBox1.Items.Count - 1)
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                DataSet dsUsers = new DataSet();
                string strUsersXML = string.Empty;
                bool blResult = false;
                DataTable dtUsers = new DataTable();
                dtUsers.Columns.Add("RecipientId");
                DataRow drUsers;
                MessageBo msgBo = new MessageBo();
                MessageVo msgVo = new MessageVo();
                string strLinksEncodedMessage = FindLinksInText(txtMessage.Text.Trim());
                string strResult = string.Empty;

                // This part can be done during Message Compose
                strResult = InsertLineBreaks(strLinksEncodedMessage, 100, new char[] { ' ', ',', ';' });
                // This part can be done during Message Compose

                // Add selected users into a dataset and retrieve the xml version of the dataset
                if (LBSelectedUser.Items.Count != 0)
                {
                    foreach (ListItem listItem in LBSelectedUser.Items)
                    {
                        drUsers = dtUsers.NewRow();
                        drUsers[0] = listItem.Value.ToString();
                        dtUsers.Rows.Add(drUsers);
                    }
                    dtUsers.TableName = "Table";
                    dsUsers.Tables.Add(dtUsers);
                    dsUsers.DataSetName = "Recipients";

                    // Adding details into MessageVo
                    msgVo.UserId = userId;
                    msgVo.Subject = txtSubject.Text.Trim();
                    msgVo.Message = strResult;
                    msgVo.strXMLRecipientIds = dsUsers.GetXml().ToString();

                    if (msgBo.InsertComposedMessage(msgVo))
                        blResult = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please select recipients!');", true);
                }

                if (blResult)
                {
                    ClearFields();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Message sent successfully!');", true);
                }
            }
        }

        private string FindLinksInText(string txt)
        {
            Regex regx = new Regex("http://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?", RegexOptions.IgnoreCase);
            MatchCollection mactches = regx.Matches(txt);

            foreach (Match match in mactches)
            {
                txt = txt.Replace(match.Value, "<a href='" + match.Value + "' target='_blank'>" + match.Value + "</a>");
            }

            return txt;
        }

        static string InsertLineBreaks(string text, int cutOffLength, char[] separators)
        {
            cutOffLength = cutOffLength > text.Length ? text.Length : cutOffLength;
            int separatorIndex = text.Substring(0, cutOffLength).LastIndexOfAny(separators);

            if (separatorIndex > 0)
                return text.Insert(separatorIndex, "<br />");
            return text.Insert(cutOffLength, "<br />");
        }

        private void ClearFields()
        {
            txtSubject.Text = String.Empty;
            txtMessage.Text = String.Empty;
            chkbxAll.Checked = false;
            foreach (ListItem li in ChkBxRoleList.Items)
            {
                li.Selected = false;
            }
            LBUser.Items.Clear();
            LBSelectedUser.Items.Clear();
            string strCurrentRole = Session[SessionContents.CurrentUserRole].ToString();
            if (strCurrentRole.ToLower().Equals(strUserTypeSuperAdmin) || strCurrentRole.Equals(strUserRoleRM))
                BindUserList();
        }
    }
}