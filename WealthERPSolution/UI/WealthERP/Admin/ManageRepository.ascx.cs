using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using System.Data;
using VoCommon;
using VoUser;
using WealthERP.Base;
using System.Configuration;
using System.IO;
using Telerik.Web.UI;
using System.Transactions;

namespace WealthERP.Admin
{
    public partial class ManageRepository : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        RepositoryBo repoBo;
        string strRepositoryPath = string.Empty;
        string strGuid = string.Empty;
        string fileExtension = string.Empty;
        string strUpdate = "Update";
        DataSet ds;
        string strRepositoryCategoryTextField = "ARC_RepositoryCategory";
        string strRepositoryCategoryValueField = "ARC_RepositoryCategoryCode";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];

            // Bind View Grid Filters
            rgRepositoryList_Init(sender, e);

            if (!IsPostBack)
            {
                // Manage Repository Methods
                BindCategory();

                // Clear Session
                Session[SessionContents.RepositoryVo] = null;
            }

            if (Session[SessionContents.RepositoryVo] != null)
            {
                BindEditFields();
            }
        }

        private void BindCategory()
        {
            repoBo = new RepositoryBo();
            ds = new DataSet();
            ds = repoBo.GetRepositoryCategory(advisorVo.advisorId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                // Bind Category DDL for Manage Tab
                ddlRCategory.DataSource = ds.Tables[0];
                ddlRCategory.DataTextField = strRepositoryCategoryTextField;
                ddlRCategory.DataValueField = strRepositoryCategoryValueField;
                ddlRCategory.DataBind();
                ddlRCategory.Items.Insert(0, new ListItem("Select a Category", "Select a Category"));
            }
        }

        protected void ddlUploadDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUploadDataType.SelectedValue == "F")
            {
                trOutsideLink.Visible = false;
                trUpload.Visible = true;
            }
            else if (ddlUploadDataType.SelectedValue == "L")
            {
                trOutsideLink.Visible = true;
                trUpload.Visible = false;
            }
            else
            {
                trOutsideLink.Visible = false;
                trUpload.Visible = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == strUpdate)
                UpdateClick();
            else
                AddClick();
        }

        private void AddClick()
        {
            RepositoryVo repoVo = new RepositoryVo();
            RepositoryBo repoBo = new RepositoryBo();
            bool blResult = false;

            if (ddlUploadDataType.SelectedValue == "F")
            {
                // If the upload type is file

                // We need to see if the adviser has a folder in Repository folder
                // Case 1: If not, then encode the adviser id and create a folder with the encoded id
                // then create a folder for the repository category within the encoded folder
                // then store the encoded adviserID + GUID + file name

                // Case 2: If folder exists, check if the category folder exists. 
                // If not then, create a folder with the category code and store the file as done above.
                // If yes, then just store the file as done above.

                // Once this is done, store the info in the DB with the file path.

                // Get the Repository Path in solution
                strRepositoryPath = ConfigurationManager.AppSettings["RepositoryPath"].ToString();
                strRepositoryPath = Server.MapPath(strRepositoryPath) + "\\" + advisorVo.advisorId;

                try
                {

                    // Reading File Upload Control
                    if (radUploadRepoItem.UploadedFiles.Count != 0)
                    {
                        // Put this part under a transaction scope
                        using (TransactionScope scope = new TransactionScope())
                        {

                            UploadedFile file = radUploadRepoItem.UploadedFiles[0];
                            float fileSize = float.Parse(file.ContentLength.ToString()) / 1048576; // Converting bytes to MB

                            if (fileSize < 2)
                            {
                                // If upload file size is less than 2 MB then upload

                                // Check if directory for advisor exists, and if not then create a new directoty
                                if (!Directory.Exists(strRepositoryPath))
                                {
                                    Directory.CreateDirectory(strRepositoryPath);
                                }

                                strGuid = Guid.NewGuid().ToString();

                                fileExtension = file.GetExtension();
                                string newFileName = advisorVo.advisorId + "_" + strGuid + "_" + file.GetName();
                                //FileIOPermission fp = new FileIOPermission(FileIOPermissionAccess.AllAccess, path);
                                //PermissionSet ps = new PermissionSet(PermissionState.None);
                                //ps.AddPermission(fp);

                                // Save adviser repository file in the path
                                file.SaveAs(strRepositoryPath + "\\" + newFileName);

                                repoVo.AdviserId = advisorVo.advisorId;
                                repoVo.CategoryCode = ddlRCategory.SelectedValue;
                                repoVo.Description = txtDescription.Text.Trim();
                                repoVo.HeadingText = txtHeadingText.Text.Trim();
                                repoVo.IsFile = true;
                                repoVo.Link = newFileName;

                                blResult = repoBo.AddRepositoryItem(repoVo);

                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Sorry your proof attachment size exceeds the allowable 2 MB limit..!');", true);
                            }

                            scope.Complete();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a file');", true);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else if (ddlUploadDataType.SelectedValue == "L")
            {
                // If the upload type is link 

                repoVo.AdviserId = advisorVo.advisorId;
                repoVo.CategoryCode = ddlRCategory.SelectedValue;
                repoVo.Description = txtDescription.Text.Trim();
                repoVo.HeadingText = txtHeadingText.Text.Trim();
                repoVo.IsFile = false;
                repoVo.Link = txtOutsideLink.Text.Trim();

                blResult = repoBo.AddRepositoryItem(repoVo);
            }

            if (blResult)
            {
                rgRepositoryList.Rebind();
                ClearFields();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Repository Item added successfully!');", true);
            }
            else
            {
                // Display error message
            }
        }

        private void UpdateClick()
        {
            RepositoryVo repoVo = new RepositoryVo();
            RepositoryBo repoBo = new RepositoryBo();
            bool blResult = false;
            repoVo = (RepositoryVo)Session[SessionContents.RepositoryVo];

            if (ddlUploadDataType.SelectedValue == "F")
            {
                // Reading File Upload Control
                int intUploadedFileCount = radUploadRepoItem.UploadedFiles.Count;
                if (intUploadedFileCount == 0)
                {
                    // normal update
                    repoVo.Description = txtDescription.Text.Trim();
                    repoVo.HeadingText = txtHeadingText.Text.Trim();
                    repoVo.Link = "";
                    blResult = repoBo.UpdateRepositoryItem(repoVo);
                }
                else
                {
                    // delete existing file and update new file

                    // Put this part under a transaction scope
                    using (TransactionScope scope = new TransactionScope())
                    {
                        /* Perform transactional work here */

                        UploadedFile file = radUploadRepoItem.UploadedFiles[0];
                        float fileSize = float.Parse(file.ContentLength.ToString()) / 1048576; // Converting bytes to MB

                        if (fileSize < 2)
                        {
                            // If file size is less than 2 MB then upload

                            // Get the Repository Path in solution
                            strRepositoryPath = ConfigurationManager.AppSettings["RepositoryPath"].ToString();
                            string strFilePath = Server.MapPath(strRepositoryPath) + "\\" + advisorVo.advisorId + "\\" + repoVo.Link;

                            // Delete file if it exists
                            if (File.Exists(strFilePath))
                            {
                                File.Delete(strFilePath);
                            }

                            // Add new file
                            strRepositoryPath = Server.MapPath(strRepositoryPath) + "\\" + advisorVo.advisorId;
                            strGuid = Guid.NewGuid().ToString();

                            // Reading File Upload Control
                            fileExtension = file.GetExtension();
                            string newFileName = advisorVo.advisorId + "_" + strGuid + "_" + file.GetName();

                            // Save adviser repository file in the path
                            file.SaveAs(strRepositoryPath + "\\" + newFileName);

                            // Update the DB with new details
                            repoVo.Description = txtDescription.Text.Trim();
                            repoVo.HeadingText = txtHeadingText.Text.Trim();
                            repoVo.Link = newFileName;
                            blResult = repoBo.UpdateRepositoryItem(repoVo);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Sorry your repository file size exceeds the allowable 2 MB limit!');", true);
                        }

                        scope.Complete();
                    }
                }
            }
            else if (ddlUploadDataType.SelectedValue == "L")
            {
                // If the upload type is link 
                repoVo.Description = txtDescription.Text.Trim();
                repoVo.HeadingText = txtHeadingText.Text.Trim();
                repoVo.Link = txtOutsideLink.Text.Trim();
                blResult = repoBo.UpdateRepositoryItem(repoVo);
            }

            if (blResult)
            {
                Session[SessionContents.RepositoryVo] = null;
                rgRepositoryList.Rebind();
                ClearFields();
                rmpManageRepository.PageViews[1].Selected = true;
                RadTabStrip1.TabIndex = 1;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Repository Item updated successfully!');", true);
            }
            else
            {
                // Display error message
            }
        }

        private void ClearFields()
        {
            ddlRCategory.SelectedIndex = 0;
            ddlRCategory.Enabled = ddlUploadDataType.Enabled = true;
            txtHeadingText.Text = String.Empty;
            ddlUploadDataType.SelectedIndex = 0;
            txtDescription.Text = String.Empty;
            txtOutsideLink.Text = String.Empty;
            btnAdd.Text = "Add";
        }

        protected void rgRepositoryList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            repoBo = new RepositoryBo();
            ds = new DataSet();
            ds = repoBo.GetAdviserRepositoryView(advisorVo.advisorId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                rgRepositoryList.DataSource = ds.Tables[0];
                trNoRecords.Visible = false;
                trContentVR.Visible = true;
                ViewState["dsRepository"] = ds;
            }
            else
            {
                // display no records found
                lblNoRecords.Visible = true;
                trNoRecords.Visible = true;
                trContentVR.Visible = false;
            }
        }

        protected void rgRepositoryList_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void rgRepositoryList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataBoundItem = e.Item as GridDataItem;
                Label lbl = (Label)dataBoundItem.FindControl("lbl");
                if (lbl != null)
                {
                    if (lbl.Text.Length > 0)
                    {
                        // split the string
                        string[] arrayStr = new string[3];
                        arrayStr = lbl.Text.Split('_');
                        int intArrLength = arrayStr.Length;
                        string strFileName = arrayStr[intArrLength - 1];
                        lbl.Text = strFileName;
                    }
                }
            }
        }

        protected void rgRepositoryList_Init(object sender, EventArgs e)
        {
            GridFilterMenu menu = rgRepositoryList.FilterMenu;
            int i = 0;
            while (i < menu.Items.Count)
            {
                if (menu.Items[i].Text == "NoFilter" || menu.Items[i].Text == "Contains" || menu.Items[i].Text == "EqualTo")
                {
                    i++;
                }
                else
                {
                    menu.Items.RemoveAt(i);
                }
            }
        }

        protected void lnkBtnFileNameClientListGrid_Click(object sender, EventArgs e)
        {

            LinkButton lnkBtn = (sender as LinkButton);
            GridDataItem dataItem = lnkBtn.NamingContainer as GridDataItem;
            int intKey = Int32.Parse(dataItem.GetDataKeyValue("AR_RepositoryId").ToString());

            // Get RepositoryVo
            RepositoryVo repoVo = new RepositoryVo();
            RepositoryBo repoBo = new RepositoryBo();
            repoVo = repoBo.GetRepositoryItem(intKey);
            Session[SessionContents.RepositoryVo] = repoVo;

            // Call script to load first tab
            rmpManageRepository.PageViews[0].Selected = true;
            RadTabStrip1.TabIndex = 0;
            BindEditFields();

            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AdvisorRMCustIndiDashboard','none');", true);
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerIndividualLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
        }

        private void BindEditFields()
        {
            RepositoryVo repoVo = new RepositoryVo();
            repoVo = (RepositoryVo)Session[SessionContents.RepositoryVo];

            ddlRCategory.SelectedValue = repoVo.CategoryCode;
            ddlRCategory.Enabled = false;
            ddlUploadDataType.Enabled = false;
            txtHeadingText.Text = repoVo.HeadingText;
            txtDescription.Text = repoVo.Description;

            if (repoVo.IsFile)
            {
                ddlUploadDataType.SelectedValue = "F";
                trUpload.Visible = true;
                trUploadedFileName.Visible = true;
                lblUploadedFile.Text = repoVo.Link;
                trOutsideLink.Visible = false;
            }
            else
            {
                ddlUploadDataType.SelectedValue = "L";
                trUpload.Visible = false;
                trUploadedFileName.Visible = false;
                trOutsideLink.Visible = true;
                txtOutsideLink.Text = repoVo.Link;
            }

            btnAdd.Text = strUpdate;
        }

    }
}