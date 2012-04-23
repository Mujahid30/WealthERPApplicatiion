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
using BoAdvisorProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace WealthERP.Admin
{
    public partial class ManageRepository : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        RepositoryVo repoVo;
        RepositoryBo repoBo;
        DataSet ds;
        string strRepositoryPath = string.Empty;
        string strGuid = string.Empty;
        string fileExtension = string.Empty;
        const string strSelectCategory = "Select a Category";
        const string strRepositoryCategoryTextField = "ARC_RepositoryCategory";
        const string strRepositoryCategoryValueField = "ARC_RepositoryCategoryCode";
        float fStorageBalance;

        public enum Constants
        {
            Add = 0,     // explicitly specifying the enum constant values will improve performance
            Update = 1,
            F = 2,
            L = 3
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];

            // Get the Repository Path in solution
            strRepositoryPath = ConfigurationManager.AppSettings["RepositoryPath"].ToString();
            fStorageBalance = advisorVo.SubscriptionVo.StorageBalance;

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
                ddlRCategory.Items.Insert(0, new ListItem(strSelectCategory, strSelectCategory));
            }
        }

        protected void ddlUploadDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUploadDataType.SelectedValue.Equals(Constants.F.ToString()))
                FileUploadVisibility(false, true);
            else if (ddlUploadDataType.SelectedValue.Equals(Constants.L.ToString()))
                FileUploadVisibility(true, false);
            else
                FileUploadVisibility(false, false);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text.Equals(Constants.Update.ToString()))
                UpdateClick();
            else
                AddClick();
        }

        private void AddClick()
        {
            repoVo = new RepositoryVo();
            bool blResult = false;
            bool blZeroBalance = false;
            bool blFileSizeExceeded = false;

            if (ddlUploadDataType.SelectedValue.Equals(Constants.F.ToString()))
            {
                if (fStorageBalance > 0)
                    blResult = AddFile(out blZeroBalance, out blFileSizeExceeded);
                else
                    blZeroBalance = true;
            }
            else if (ddlUploadDataType.SelectedValue.Equals(Constants.L.ToString()))
            {
                blResult = AddLink();
            }


            if (blZeroBalance)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ManageRepository", "alert('You do not have enough space. You have only " + fStorageBalance + " MB left in your account!');", true);
            else
            {
                if (blResult)
                {
                    ResetControls();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ManageRepository", "alert('Repository Item added successfully!');", true);
                }
                else
                {
                    if (blFileSizeExceeded)
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ManageRepository", "alert('Sorry your file size exceeds the allowable 2 MB limit!');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ManageRepository", "alert('Error adding repository item!');", true);
                }
            }
        }

        private bool AddFile(out bool blZeroBalance, out bool blFileSizeExceeded)
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
            strRepositoryPath = Server.MapPath(strRepositoryPath) + "\\" + advisorVo.advisorId;
            AdvisorBo advBo = new AdvisorBo();
            repoBo = new RepositoryBo();
            bool blResult = false;
            blZeroBalance = false;
            blFileSizeExceeded = false;

            try
            {
                // Reading File Upload Control
                if (radUploadRepoItem.UploadedFiles.Count != 0)
                {
                    // Put this part under a transaction scope
                    using (TransactionScope scope1 = new TransactionScope())
                    {
                        UploadedFile file = radUploadRepoItem.UploadedFiles[0];
                        float fileSize = float.Parse(file.ContentLength.ToString()) / 1048576; // Converting bytes to MB

                        if (fStorageBalance >= fileSize)
                        {
                            if (fileSize <= 2)   // If upload file size is less than 2 MB then upload
                            {
                                // Check if directory for advisor exists, and if not then create a new directoty
                                if (!Directory.Exists(strRepositoryPath))
                                {
                                    Directory.CreateDirectory(strRepositoryPath);
                                }
                                strGuid = Guid.NewGuid().ToString();
                                string newFileName = SaveFileIntoServer(file, strGuid, strRepositoryPath);

                                repoVo.AdviserId = advisorVo.advisorId;
                                repoVo.CategoryCode = ddlRCategory.SelectedValue;
                                repoVo.Description = txtDescription.Text.Trim();
                                repoVo.HeadingText = txtHeadingText.Text.Trim();
                                repoVo.IsFile = true;
                                repoVo.Link = newFileName;
                                blResult = repoBo.AddRepositoryItem(repoVo);

                                if (blResult)
                                {
                                    // Once the adding of repository is a success, then update the balance storage in advisor subscription table
                                    fStorageBalance = UpdateAdvisorStorageBalance(fileSize, 0, fStorageBalance);
                                }
                            }
                            else
                            {
                                blFileSizeExceeded = true;
                            }
                        }
                        else
                        {
                            blZeroBalance = true;
                        }

                        scope1.Complete();   // Commit the transaction scope if no errors
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a file to upload!');", true);
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                object[] objects = new object[2];
                objects[0] = repoVo;
                objects[1] = repoBo;
                PageException(objects, Ex, "ManageRepository.ascx:AddClick()");
            }
            return blResult;
        }

        private bool AddLink()
        {
            bool blResult = false;
            // If the upload type is link 
            repoVo.AdviserId = advisorVo.advisorId;
            repoVo.CategoryCode = ddlRCategory.SelectedValue;
            repoVo.Description = txtDescription.Text.Trim();
            repoVo.HeadingText = txtHeadingText.Text.Trim();
            repoVo.IsFile = false;
            repoVo.Link = txtOutsideLink.Text.Trim();

            blResult = AddUpdateRepositoryLink(blResult, Constants.Add.ToString(), repoVo);
            return blResult;
        }

        private void UpdateClick()
        {
            repoVo = new RepositoryVo();
            bool blResult = false;
            bool blZeroBalance = false;
            bool blFileSizeExceeded = false;
            repoVo = (RepositoryVo)Session[SessionContents.RepositoryVo];

            if (ddlUploadDataType.SelectedValue.Equals(Constants.F.ToString()))
            {
                if (fStorageBalance > 0)
                    blResult = UpdateFile(out blZeroBalance, out blFileSizeExceeded);
                else
                    blZeroBalance = true;
            }
            else if (ddlUploadDataType.SelectedValue.Equals(Constants.L.ToString()))
            {
                blResult = UpdateLink();
            }

            if (blResult)
            {
                Session[SessionContents.RepositoryVo] = null;
                ResetControls();
                // Change the tab
                ChangeTelerikRadTab(1);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ManageRepository", "alert('Repository Item updated successfully!');", true);
            }
            else
            {
                if (blZeroBalance)
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ManageRepository", "alert('You do not have enough space. You have only " + fStorageBalance + " MB left in your account!');", true);
                else if (blFileSizeExceeded)
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Sorry your repository file size exceeds the allowable 2 MB limit!');", true);
                else
                {
                    // Display error message
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ManageRepository", "alert('Error updating repository item!');", true);
                }
            }
        }

        private bool UpdateFile(out bool blZeroBalance, out bool blFileSizeExceeded)
        {
            repoBo = new RepositoryBo();
            bool blResult = false;
            blZeroBalance = false;
            blFileSizeExceeded = false;

            try
            {
                // Reading File Upload Control
                int intUploadedFileCount = radUploadRepoItem.UploadedFiles.Count;

                if (intUploadedFileCount == 0)
                {
                    // normal update
                    repoVo.Description = txtDescription.Text.Trim();
                    repoVo.HeadingText = txtHeadingText.Text.Trim();
                    repoVo.Link = String.Empty;
                    blResult = repoBo.UpdateRepositoryItem(repoVo);
                }
                else
                {
                    // delete existing file and update new file

                    // Put this part under a transaction scope
                    using (TransactionScope scope2 = new TransactionScope())
                    {
                        /* Perform transactional work here */

                        UploadedFile file = radUploadRepoItem.UploadedFiles[0];
                        float fileSize = float.Parse(file.ContentLength.ToString()) / 1048576; // Converting bytes to MB
                        float oldFileSize = 0.0F;

                        if (fileSize <= 2)
                        {
                            // If file size is less than 2 MB then upload

                            // Get the Repository Path in solution
                            string strFilePath = Server.MapPath(strRepositoryPath) + "\\" + advisorVo.advisorId + "\\" + repoVo.Link;
                            float fStorageBalance = advisorVo.SubscriptionVo.StorageBalance;
                            AdvisorBo advBo = new AdvisorBo();

                            // Delete file if it exists
                            if (File.Exists(strFilePath))
                            {
                                // Get the file size of the old file to calculate the balance storagee size
                                FileInfo f = new FileInfo(strFilePath);
                                long lSize = f.Length;
                                oldFileSize = (lSize / (float)1048576);
                                float flRemainingBal = fStorageBalance + oldFileSize;

                                if (flRemainingBal >= fileSize)
                                    File.Delete(strFilePath);
                                else
                                    blZeroBalance = true;
                            }

                            if (!blZeroBalance)
                            {
                                // Add new file
                                strRepositoryPath = Server.MapPath(strRepositoryPath) + "\\" + advisorVo.advisorId;
                                strGuid = Guid.NewGuid().ToString();

                                // Reading File Upload Control
                                string newFileName = SaveFileIntoServer(file, strGuid, strRepositoryPath);

                                // Update the DB with new details
                                repoVo.Description = txtDescription.Text.Trim();
                                repoVo.HeadingText = txtHeadingText.Text.Trim();
                                repoVo.Link = newFileName;

                                blResult = repoBo.UpdateRepositoryItem(repoVo);

                                if (blResult)
                                {
                                    // Once updating the repository is a success, then update the balance storage in advisor subscription table
                                    fStorageBalance = UpdateAdvisorStorageBalance(fileSize, oldFileSize, fStorageBalance);
                                }
                            }
                        }
                        else
                        {
                            blFileSizeExceeded = true;
                        }

                        scope2.Complete();
                    }
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                object[] objects = new object[2];
                objects[0] = repoVo;
                objects[1] = repoBo;
                PageException(objects, Ex, "ManageRepository.ascx:UpdateClick()");
            }
            return blResult;
        }

        private float UpdateAdvisorStorageBalance(float fileSize, float oldFileSize, float fStorageBalance)
        {
            AdvisorBo advBo = new AdvisorBo();
            fStorageBalance -= (fileSize - oldFileSize);
            advBo.UpdateAdviserStorageBalance(advisorVo.advisorId, fStorageBalance);
            advisorVo.SubscriptionVo.StorageBalance = fStorageBalance;
            Session[SessionContents.AdvisorVo] = advisorVo;
            return fStorageBalance;
        }

        private bool UpdateLink()
        {
            bool blResult = false;
            // If the upload type is link 
            repoVo.Description = txtDescription.Text.Trim();
            repoVo.HeadingText = txtHeadingText.Text.Trim();
            repoVo.Link = txtOutsideLink.Text.Trim();

            blResult = AddUpdateRepositoryLink(blResult, Constants.Update.ToString(), repoVo);
            return blResult;
        }

        private bool AddUpdateRepositoryLink(bool blResult, string strAction, RepositoryVo repoVo)
        {
            repoBo = new RepositoryBo();
            try
            {
                blResult = (strAction.Equals(Constants.Add.ToString())) ? repoBo.AddRepositoryItem(repoVo) : repoBo.UpdateRepositoryItem(repoVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                object[] objects = new object[1];
                objects[0] = repoVo;
                PageException(objects, Ex, "ManageRepository.ascx:AddClick()");
            }
            return blResult;
        }

        private static void PageException(object[] objects, Exception Ex, string strMethodPath)
        {
            BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
            NameValueCollection FunctionInfo = new NameValueCollection();
            FunctionInfo.Add("Method", strMethodPath);
            FunctionInfo = exBase.AddObject(FunctionInfo, objects);
            exBase.AdditionalInformation = FunctionInfo;
            ExceptionManager.Publish(exBase);
            throw exBase;
        }

        private string SaveFileIntoServer(UploadedFile file, string strGuid, string strPath)
        {
            fileExtension = file.GetExtension();
            string newFileName = advisorVo.advisorId + "_" + strGuid + "_" + file.GetName();
            // Save adviser repository file in the path
            file.SaveAs(strPath + "\\" + newFileName);
            return newFileName;
        }

        private void ResetControls()
        {
            rgRepositoryList.Rebind();
            ddlRCategory.SelectedIndex = ddlUploadDataType.SelectedIndex = 0;
            ddlRCategory.Enabled = ddlUploadDataType.Enabled = true;
            txtHeadingText.Text = txtDescription.Text = txtOutsideLink.Text = lblUploadedFile.Text = String.Empty;
            trUpload.Visible = trUploadedFileName.Visible = false;
            btnAdd.Text = Constants.Add.ToString();
        }

        protected void rgRepositoryList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            repoBo = new RepositoryBo();
            ds = new DataSet();
            ds = repoBo.GetAdviserRepositoryView(advisorVo.advisorId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                rgRepositoryList.DataSource = ds.Tables[0];
                SetContentVisibility(true);
                ViewState["dsRepository"] = ds;
            }
            else
            {
                // display no records found
                SetContentVisibility(false);
            }
        }

        private void SetContentVisibility(bool blState)
        {
            lblNoRecords.Visible = !blState;
            trNoRecords.Visible = !blState;
            trContentVR.Visible = blState;
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
                        int intFirstIndex = lbl.Text.IndexOf('_');
                        int intSecondIndex = lbl.Text.IndexOf('_', intFirstIndex + 1);
                        int intLength = lbl.Text.Length - (intSecondIndex + 1);
                        string strFileName = lbl.Text.Substring(intSecondIndex + 1, intLength);
                        lbl.Text = strFileName;
                    }
                }

                if (dataBoundItem["Description"].Text.Length > 50)
                {
                    dataBoundItem["Description"].Text = dataBoundItem["Description"].Text.Substring(0, 50) + "...";
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

            // Change the tab
            ChangeTelerikRadTab(0);

            BindEditFields();
        }

        /// <summary>
        /// Changes the Telerik Rad Tab
        /// </summary>
        /// <param name="intIndex"></param>
        private void ChangeTelerikRadTab(int intIndex)
        {
            RadTabStrip1.SelectedIndex = intIndex;
            rmpManageRepository.SelectedIndex = RadTabStrip1.SelectedTab.Index;
        }

        private void FileUploadVisibility(bool blLinkTRVisibility, bool blUploadTRVisibility)
        {
            trOutsideLink.Visible = blLinkTRVisibility;
            trUpload.Visible = blUploadTRVisibility;
        }

        private void BindEditFields()
        {
            repoVo = new RepositoryVo();
            repoVo = (RepositoryVo)Session[SessionContents.RepositoryVo];

            ddlRCategory.SelectedValue = repoVo.CategoryCode;
            ddlRCategory.Enabled = ddlUploadDataType.Enabled = false;
            txtHeadingText.Text = repoVo.HeadingText;
            txtDescription.Text = repoVo.Description;

            if (repoVo.IsFile)
            {
                ddlUploadDataType.SelectedValue = Constants.F.ToString();
                FileUploadVisibility(false, true);
                trUploadedFileName.Visible = true;
                string[] strUplFile = new string[3];
                strUplFile = repoVo.Link.Split('_');

                // The 3rd item in the array is the actual file name
                lblUploadedFile.Text = strUplFile[strUplFile.Length - 1];
            }
            else
            {
                ddlUploadDataType.SelectedValue = Constants.L.ToString();
                FileUploadVisibility(true, false);
                trUploadedFileName.Visible = false;
                txtOutsideLink.Text = repoVo.Link;
            }
            btnAdd.Text = Constants.Update.ToString();
        }

        protected void hdrCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridDataItem item in rgRepositoryList.Items)
            {
                CheckBox hdrCheckBx = (CheckBox)sender;
                CheckBox chkbxRow = (CheckBox)item.FindControl("chkbxRow");
                if (hdrCheckBx != null && chkbxRow != null)
                {
                    chkbxRow.Checked = hdrCheckBx.Checked;
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
            dt.Columns.Add("FileName");
            dt.Columns.Add("IsFile");
            DataRow drDelete;

            foreach (GridDataItem item in rgRepositoryList.Items)
            {
                CheckBox chkbxRow = (CheckBox)item.FindControl("chkbxRow");
                if (chkbxRow != null)
                {
                    if (chkbxRow.Checked)
                    {
                        // Update flag for validation message [select atleast one to delete]
                        blAnyChecked = true;

                        drDelete = dt.NewRow();
                        string strKeyValue = item.GetDataKeyValue("AR_RepositoryId").ToString();
                        string strIsFile = item.GetDataKeyValue("AR_IsFile").ToString();
                        string strFileName = item.GetDataKeyValue("AR_Filename").ToString();
                        drDelete[0] = strKeyValue;
                        drDelete[1] = strFileName;
                        drDelete[2] = strIsFile;
                        dt.Rows.Add(drDelete);
                    }
                }
            }

            dt.TableName = "Table";
            ds.Tables.Add(dt);
            ds.DataSetName = "Deleted";

            if (blAnyChecked)
            {
                #region Put Transaction Scope 3 here

                // Get the system file path. Delete for each id in the dataset
                float fBalanceStorage = 0.0F;
                string strPath = Server.MapPath(strRepositoryPath) + "\\" + advisorVo.advisorId + "\\";
                fBalanceStorage = DeleteRepositoryItems(strPath, ds);

                repoBo = new RepositoryBo();
                blResult = repoBo.DeleteRepositoryItems(ds.GetXml().ToString(), fBalanceStorage, advisorVo.advisorId);

                if (blResult)
                {
                    // Store the fBalanceStorage in advisor session and update the database
                    advisorVo.SubscriptionVo.StorageBalance = fBalanceStorage;
                    Session[SessionContents.AdvisorVo] = advisorVo;
                    rgRepositoryList.Rebind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Repository", "alert('Items deleted successfully!');", true);
                }
                else
                {
                    rgRepositoryList.Rebind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Repository", "alert('Error deleting items!');", true);
                }

                #endregion

            }
            else
            {
                // Display validation message that atleast one checkbox should be checked
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Repository", "alert('Please select an item!');", true);
            }
        }

        private float DeleteRepositoryItems(string strRepositoryPath, DataSet ds)
        {
            float oldFileSize = 0.0F;
            float flRemainingBal = 0.0F;
            flRemainingBal = fStorageBalance;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string strFilename = dr["FileName"].ToString();
                string strFilePath = strRepositoryPath + strFilename;

                if (File.Exists(strFilePath))
                {
                    // Get the file size of the old file to calculate the balance storage size
                    FileInfo f = new FileInfo(strFilePath);
                    long lSize = f.Length;
                    oldFileSize = (lSize / (float)1048576);
                    // Update the remaining balance storage after deletion
                    flRemainingBal += oldFileSize;

                    File.Delete(strFilePath);
                }
            }

            return flRemainingBal;
        }

    }
}