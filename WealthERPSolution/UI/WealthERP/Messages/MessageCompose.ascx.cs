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
        string strCurrentUserRole;
        int userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            if (Session[SessionContents.CurrentUserRole] != null)
                strCurrentUserRole = Session[SessionContents.CurrentUserRole].ToString();
            userVo = (UserVo)Session[SessionContents.UserVo];
            userId = userVo.UserId;
            if (!IsPostBack)
            {
                strCurrentUserRole = String.Empty;
                RoleCheckboxAccessibility();
            }
        }

        private void RoleCheckboxAccessibility()
        {
            userVo = (UserVo)Session[SessionContents.UserVo];
            if (userVo.UserType.ToLower() == strUserTypeSuperAdmin)
            {
                // super admin logs in
                Session[SessionContents.CurrentUserRole] = "superadmin";
                chkbxAll.Visible = false;
                for (int i = 0; i < ChkBxRoleList.Items.Count; i++)
                {
                    int currentItem = Int32.Parse(ChkBxRoleList.Items[i].Value.ToString());
                    if (currentItem != 1000)
                    {
                        // Remove all check boxes except Advisor checkbox
                        ChkBxRoleList.Items.RemoveAt(i);
                        --i;
                    }
                }
            }
            else if (userVo.UserType.ToLower() == strUserTypeAdvisor)
            {
                if (userVo.RoleList.Contains(strUserRoleAdmin))
                {
                    // Advisor admin logged in
                    for (int i = 0; i < ChkBxRoleList.Items.Count; i++)
                    {
                        int currentItem = Int32.Parse(ChkBxRoleList.Items[i].Value.ToString());
                        if (currentItem == 1000)
                        {
                            // Remove Advisor checkbox
                            ChkBxRoleList.Items.RemoveAt(i);
                            --i;
                        }
                    }
                }
                else if (userVo.RoleList.Contains(strUserRoleBM))
                {
                    // Advisor BM logged in
                    for (int i = 0; i < ChkBxRoleList.Items.Count; i++)
                    {
                        int currentItem = Int32.Parse(ChkBxRoleList.Items[i].Value.ToString());
                        if (currentItem == 1000 || currentItem == 1002)
                        {
                            // Remove Advisor & BM checkbox
                            ChkBxRoleList.Items.RemoveAt(i);
                            --i;
                        }
                    }
                }
                //else if (userVo.RoleList.Contains(strUserRoleRM) && !(userVo.RoleList.Contains(strUserRoleResearch)))
                //{
                //    // Advisor RM logged in

                //}
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
            foreach (ListItem li in ChkBxRoleList.Items)
            {
                if (li.Selected)
                {
                    blBindUsers = true;
                    break;
                }
            }

            if (blBindUsers)
            {
                MessageBo msgBo = new MessageBo();
                DataTable dtUserList = new DataTable();
                StringBuilder sbRoleList = new StringBuilder();

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

                dtUserList = (msgBo.GetUserListSpecificToRole(strCurrentUserRole, userVo.UserId, sbRoleList.ToString())).Tables[0];

                if (dtUserList.Rows.Count > 0)
                {
                    LBUser.DataSource = dtUserList;
                    LBUser.DataTextField = "Name";
                    LBUser.DataValueField = "UserId";
                    LBUser.DataBind();
                }
            }
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
                    msgVo.Message = strLinksEncodedMessage;
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
        }

        //public struct LinkItem
        //{
        //    public string Href;
        //    public string Text;

        //    public override string ToString()
        //    {
        //        return Href + "\n\t" + Text;
        //    }
        //}

        //static class LinkFinder
        //{
        //    public static List<LinkItem> Find(string file)
        //    {
        //        List<LinkItem> list = new List<LinkItem>();

        //        // 1.
        //        // Find all matches in file.
        //        MatchCollection m1 = Regex.Matches(file, @"(<a.*?>.*?</a>)",
        //            RegexOptions.Singleline);

        //        // 2.
        //        // Loop over each match.
        //        foreach (Match m in m1)
        //        {
        //            string value = m.Groups[1].Value;
        //            LinkItem i = new LinkItem();

        //            // 3.
        //            // Get href attribute.
        //            Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
        //            RegexOptions.Singleline);
        //            if (m2.Success)
        //            {
        //                i.Href = m2.Groups[1].Value;
        //            }

        //            // 4.
        //            // Remove inner tags from text.
        //            string t = Regex.Replace(value, @"\s*<.*?>\s*", "",
        //            RegexOptions.Singleline);
        //            i.Text = t;

        //            list.Add(i);
        //        }
        //        return list;
        //    }
        //}
    }
}