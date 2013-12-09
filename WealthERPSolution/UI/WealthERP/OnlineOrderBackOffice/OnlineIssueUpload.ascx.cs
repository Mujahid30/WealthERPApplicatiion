using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using BoOnlineOrderManagement;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineIssueUpload : System.Web.UI.UserControl
    {
        OnlineCommonBackOfficeBo boComBackOff;
        OnlineNCDBackOfficeBo boNcdBackOff;

        protected void Page_Load(object sender, EventArgs e)
        {
            //ddlFileType.Enabled = false;

        }
        protected void Readcsvfile()
        {
            //StreamReader StreamReader=new StreamReader(
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            String savePath = @"C:\Users\kbajpai.AMP\Desktop\Upload";
            DataTable dtUploadFile;

            if (FileUpload.HasFile) {
                String fileName = FileUpload.FileName;
                savePath += fileName;
                FileUpload.SaveAs(savePath);

                ShowMessage(fileName + "Uploaded");
                if (boComBackOff == null) boComBackOff = new OnlineCommonBackOfficeBo();

                dtUploadFile = boComBackOff.ReadCsvFile(savePath);
            }
            else {
                ShowMessage("Could not read the file");
                return;
            }

            if (dtUploadFile == null) {
                ShowMessage("Error in reading file");
                return;
            }

            if (dtUploadFile.Rows.Count <= 0) {
                ShowMessage("No data in the file");
                return;
            }

            if(boNcdBackOff == null) boNcdBackOff = new OnlineNCDBackOfficeBo();
            DataTable dtValidatedData = boNcdBackOff.ValidateUploadData(dtUploadFile, int.Parse(ddlFileType.SelectedValue), ddlSource.SelectedValue);
        }

        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mykey", "hide();", true);
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page.Validate("FileType");

            if (!Page.IsValid)
            {
                ShowMessage("Please check all required fields");
                return;
            }
            SetFileType();
        }

        protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page.Validate("FileType");

            if (!Page.IsValid)
            {
                ShowMessage("Please check all required fields");
                return;
            }
            SetFileType();
        }

        protected void gvOnlineIssueUpload_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
        }

        private void SetFileType()
        {
            if (boNcdBackOff == null) boNcdBackOff = new OnlineNCDBackOfficeBo();

            DataTable dtFileType = boNcdBackOff.GetFileTypeList(0, ddlSource.SelectedValue, 'U', ddlProduct.SelectedValue);

            DataRow drFileType = dtFileType.NewRow();
            drFileType["WEFT_Id"] = 0;
            drFileType["WEFT_FileType"] = "--SELECT--";
            dtFileType.Rows.InsertAt(drFileType, 0);

            ddlFileType.DataSource = dtFileType;
            ddlFileType.DataValueField = dtFileType.Columns["WEFT_Id"].ToString();
            ddlFileType.DataTextField = dtFileType.Columns["WEFT_FileType"].ToString();
            ddlFileType.DataBind();
        }
     }
}