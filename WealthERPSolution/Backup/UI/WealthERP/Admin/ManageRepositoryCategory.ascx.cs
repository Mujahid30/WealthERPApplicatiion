using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using System.Data;
using VoUser;
using WealthERP.Base;

namespace WealthERP.Admin
{
    public partial class ManageRepositoryCategory : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        RepositoryBo repoBo;
        UserVo userVo = new UserVo();
        string strAdviserAdminRoleId = "1000";
        string strRMRoleId = "1001";
        string strBMRoleId = "1002";
        string strCustomerRoleId = "1003";
        string strOpsRoleId = "1004";
        string strResearchRoleId = "1005";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            userVo = (UserVo)Session[SessionContents.UserVo];
            BindCategories();
        }

        private void BindCategories()
        {
            repoBo = new RepositoryBo();
            DataSet ds = new DataSet();
            ds = repoBo.GetRepositoryCategory(advisorVo.advisorId);
            int intRepoCount = ds.Tables[0].Rows.Count;
            int intRoleCount = ds.Tables[1].Rows.Count;
            ViewState["intRepoDSCount"] = intRepoCount;

            if (intRepoCount > 0)
            {
                for (int i = 0; i < intRepoCount; i++)
                {
                    Label lbl = (Label)this.FindControl("lblCategory" + (i + 1).ToString());
                    HiddenField hdn = (HiddenField)this.FindControl("hdnCat" + (i + 1).ToString());
                    TextBox txt = (TextBox)this.FindControl("txtCategory" + (i + 1).ToString());
                    CheckBoxList chkbxlst = (CheckBoxList)this.FindControl("CheckBoxList" + (i + 1).ToString());
                    lbl.Text = ds.Tables[0].Rows[i]["ARC_RepositoryCategory"].ToString();
                    hdn.Value = ds.Tables[0].Rows[i]["ARC_AdviserRepositoryCategoryId"].ToString();
                    txt.Text = ds.Tables[0].Rows[i]["ARC_RepositoryCategory"].ToString();

                    // Filter 2nd Dataset based on hdn.Value 
                    foreach (DataRow dr in ds.Tables[1].Select("ARC_AdviserRepositoryCategoryId = '" + hdn.Value + "'"))
                    {
                        if (dr["UR_RoleId"].ToString() == strAdviserAdminRoleId)
                        {
                            SelectCheckboxListItem(chkbxlst, strAdviserAdminRoleId);
                        }
                        else if (dr["UR_RoleId"].ToString() == strRMRoleId)
                        {
                            SelectCheckboxListItem(chkbxlst, strRMRoleId);
                        }
                        else if (dr["UR_RoleId"].ToString() == strBMRoleId)
                        {
                            SelectCheckboxListItem(chkbxlst, strBMRoleId);
                        }
                        else if (dr["UR_RoleId"].ToString() == strCustomerRoleId)
                        {
                            SelectCheckboxListItem(chkbxlst, strCustomerRoleId);
                        }
                        else if (dr["UR_RoleId"].ToString() == strOpsRoleId)
                        {
                            SelectCheckboxListItem(chkbxlst, strOpsRoleId);
                        }
                        else if (dr["UR_RoleId"].ToString() == strResearchRoleId)
                        {
                            SelectCheckboxListItem(chkbxlst, strResearchRoleId);
                        }
                    }
                }
            }
        }

        private void SelectCheckboxListItem(CheckBoxList chkbxlst, string strRoleId)
        {
            foreach (ListItem li in chkbxlst.Items)
            {
                if (li.Value == strRoleId)
                    li.Selected = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int intRepoCount = (int)ViewState["intRepoDSCount"];
            bool blChanged = false;
            bool blResult = false;
            DataSet dsChanges = new DataSet();
            DataSet dsCatRole = new DataSet();
            
            DataTable dtChanges = new DataTable();
            dtChanges.Columns.Add("AdviserRepositoryCategoryId");
            dtChanges.Columns.Add("RepositoryCategory");
            DataRow drChanges;

            DataTable dtCatRole = new DataTable();
            dtCatRole.Columns.Add("AdviserRepositoryCategoryId");
            dtCatRole.Columns.Add("RoleId");
            DataRow drCatRole;


            //for (int i = 0; i < intRepoCount; i++)
            //{
            //    Label lbl = (Label)this.FindControl("lblCategory" + (i + 1).ToString());
            //    HiddenField hdn = (HiddenField)this.FindControl("hdnCat" + (i + 1).ToString());
            //    TextBox txt = (TextBox)this.FindControl("txtCategory" + (i + 1).ToString());
            //    if (lbl.Text.Trim().ToLower() != txt.Text.Trim().ToLower())
            //    {
            //        blChanged = true;
            //        break;
            //    }
            //}

            //if (blChanged)
            //{

            // Update DB Changes
            for (int i = 0; i < intRepoCount; i++)
            {
                HiddenField hdn = (HiddenField)this.FindControl("hdnCat" + (i + 1).ToString());
                TextBox txt = (TextBox)this.FindControl("txtCategory" + (i + 1).ToString());
                drChanges = dtChanges.NewRow();
                drChanges[0] = hdn.Value.Trim();
                drChanges[1] = txt.Text.Trim();
                dtChanges.Rows.Add(drChanges);

                // Update Roles for the Category
                CheckBoxList chkbxlst = (CheckBoxList)this.FindControl("CheckBoxList" + (i + 1).ToString());
                foreach (ListItem li in chkbxlst.Items)
                {
                    if (li.Selected)
                    {
                        drCatRole = dtCatRole.NewRow();
                        drCatRole[0] = hdn.Value.Trim();
                        drCatRole[1] = li.Value.Trim();
                        dtCatRole.Rows.Add(drCatRole);
                    }
                }
            }

            dtChanges.TableName = "Table";
            dsChanges.Tables.Add(dtChanges);
            dsChanges.DataSetName = "Categories";

            dtCatRole.TableName = "Table2";
            dsCatRole.Tables.Add(dtCatRole);
            dsCatRole.DataSetName = "CategoryRole";

            repoBo = new RepositoryBo();
            blResult = repoBo.UpdateRepositoryCategoryNames(advisorVo.advisorId, dsChanges.GetXml().ToString(), dsCatRole.GetXml().ToString(), userVo.UserId);

            if (blResult)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Repository", "alert('Category names and associated roles updated successfully!');", true);
                BindCategories();
            }
            //}
        }
    }
}