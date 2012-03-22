using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using System.Data;
using WealthERP.Base;
using VoUser;
using System.Text;
using System.Configuration;

namespace WealthERP.Admin
{
    public partial class ViewRepository : System.Web.UI.UserControl
    {
        UserVo userVo;
        int userId;
        string strRepositoryPath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            userId = userVo.UserId;
            strRepositoryPath = ConfigurationManager.AppSettings["RepositoryPath"].ToString();

            if (!IsPostBack)
            {
                BindListBoxes();
            }
        }

        private void BindListBoxes()
        {
            RepositoryBo repoBo = new RepositoryBo();
            DataSet ds = new DataSet();
            StringBuilder sbRoleList = new StringBuilder();

            // creating comma separated roleId list so that we can reuse the function which separates comma separated role list
            if (userVo.RoleList != null)
            {
                foreach (string str in userVo.RoleList)
                {
                    string strRoleId = string.Empty;
                    if (str.ToLower() == "admin")
                        strRoleId = "1000";
                    else if (str.ToLower() == "rm")
                        strRoleId = "1001";
                    else if (str.ToLower() == "bm")
                        strRoleId = "1002";
                    else if (str.ToLower() == "research")
                        strRoleId = "1005";
                    else if (str.ToLower() == "ops")
                        strRoleId = "1004";
                    else if (str.ToLower() == "customer")
                        strRoleId = "1003";
                    sbRoleList.Append(strRoleId);
                    sbRoleList.Append(",");
                }
            }
            else if (userVo.UserType.ToLower() == "customer")
            {
                string strRoleId = "1003";
                sbRoleList.Append(strRoleId);
                sbRoleList.Append(",");
            }

            sbRoleList.Remove(sbRoleList.Length - 1, 1);

            ds = repoBo.GetRepositoryView(userVo.UserId, sbRoleList.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                trNoRecords.Visible = false;
                int intAdviserId = 0;
                if (ds.Tables[1].Rows[0]["AdviserId"] != null)
                    intAdviserId = Int32.Parse(ds.Tables[1].Rows[0]["AdviserId"].ToString());
                string strPrevDataRowCategory = string.Empty;
                bool blNewCategory = false;
                int count = 0;
                string strCurrentPageHostAuthority = Request.Url.Authority;


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["ARC_RepositoryCategoryCode"].ToString() != strPrevDataRowCategory)
                    {
                        blNewCategory = true;
                    }

                    if (blNewCategory)
                    {
                        // increment the count to bind to the different lists based on ListId+count
                        count++;

                        // reset the new category flag
                        blNewCategory = false;

                        // store the new Category
                        strPrevDataRowCategory = dr["ARC_RepositoryCategoryCode"].ToString();

                        // Give the Category Name as the header
                        Label ctrlLabel = (Label)FindControl("lblCategory" + count);
                        if (ctrlLabel != null)
                        {
                            ctrlLabel.Visible = true;
                            ctrlLabel.Text = dr["ARC_RepositoryCategory"].ToString();
                        }
                    }

                    ListBox ctrlLstBx = (ListBox)FindControl("ListBox" + count);

                    if (ctrlLstBx != null)
                    {
                        ctrlLstBx.Visible = true;
                        string strLink = string.Empty;
                        if (dr["AR_IsFile"].ToString().ToLower() == Boolean.TrueString.ToLower())
                        {
                            strLink = "http:\\" + strRepositoryPath + "\\" + intAdviserId + "\\\\" + dr["AR_Filename"].ToString();
                            ctrlLstBx.Items.Add(new ListItem(dr["AR_HeadingText"].ToString(), strLink));
                        }
                        else
                        {
                            strLink = dr["AR_Link"].ToString();
                            ctrlLstBx.Items.Add(new ListItem(dr["AR_HeadingText"].ToString(), strLink));
                        }
                    }
                    else
                    {
                        // Error message List Box not found
                    }
                }
            }
            else
            {
                // Display no items on display
                trNoRecords.Visible = true;
            }
        }

        protected void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lstbx = (ListBox)sender;
            string RedirectUrl = lstbx.SelectedValue;
            lstbx.SelectedIndex = -1;
            Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "pageloadscript", "window.open('" + RedirectUrl + "')", true);
        }

        //protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string RedirectUrl = ListBox2.SelectedValue;
        //    ListBox2.SelectedIndex = -1;
        //    Page.ClientScript.RegisterStartupScript(this.GetType(),
        //            "pageloadscript", "window.open('" + RedirectUrl + "')", true);
        //}

        //protected void ListBox3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string RedirectUrl = ListBox3.SelectedValue;
        //    ListBox3.SelectedIndex = -1;
        //    Page.ClientScript.RegisterStartupScript(this.GetType(),
        //            "pageloadscript", "window.open('" + RedirectUrl + "')", true);
        //}

        //protected void ListBox4_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string RedirectUrl = ListBox4.SelectedValue;
        //    ListBox4.SelectedIndex = -1;
        //    Page.ClientScript.RegisterStartupScript(this.GetType(),
        //            "pageloadscript", "window.open('" + RedirectUrl + "')", true);
        //}

    }
}