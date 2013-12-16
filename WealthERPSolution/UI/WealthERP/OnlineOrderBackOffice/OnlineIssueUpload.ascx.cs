using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using BoOnlineOrderManagement;
using VoUser;
using WealthERP.Base;
using Telerik.Web.UI;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineIssueUpload : System.Web.UI.UserControl
    {
        OnlineCommonBackOfficeBo boComBackOff;
        OnlineNCDBackOfficeBo boNcdBackOff;
        AdvisorVo advisorVo;
        UserVo userVo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];

            if (!string.IsNullOrEmpty(Session["userVo"].ToString()))
                userVo = (UserVo)Session[SessionContents.UserVo];

            if (!IsPostBack) {
                if (Cache["UPLOAD" + userVo.UserId] != null) Cache.Remove("UPLOAD" + userVo.UserId);
            }

        }
        protected void Readcsvfile()
        {
            //StreamReader StreamReader=new StreamReader(
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            String savePath = Server.MapPath("UploadFiles/");
            DataTable dtUploadFile;

            if (FileUpload.HasFile) {
                String fileName = FileUpload.FileName;
                savePath += advisorVo.advisorId.ToString() + userVo.UserId.ToString() + fileName;
                FileUpload.SaveAs(savePath);

                ShowMessage(fileName + "Uploaded");
                if (boComBackOff == null) boComBackOff = new OnlineCommonBackOfficeBo();

                dtUploadFile = boComBackOff.ReadCsvFile(savePath);
                if(File.Exists(savePath)) File.Delete(savePath);
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

            BindGrid(dtValidatedData);
            ToggleUpload(dtValidatedData);
        }

        private void ToggleUpload(DataTable dtUpload)
        {
            bool bUpload = true;
            
            btnUploadData.Enabled = false;
            foreach(DataRow row in dtUpload.Rows) {
                if(string.IsNullOrEmpty(row["Remarks"].ToString().Trim())) continue;
                bUpload = false;
                ShowMessage("Please check the data in the file & re-upload");
                break;
            }
            if (bUpload) {
                btnUploadData.Enabled = true;
                ShowMessage("Please file data has been uploaded, click Upload Data button to upload");
            }
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
            DataTable dtUpload = (DataTable)Cache["UPLOAD" + userVo.UserId];
            if (dtUpload != null) gvOnlineIssueUpload.DataSource = dtUpload;
        }

        private void SetFileType() {
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

        private void GetExtractData(DataTable dtUploadFile) {
            if (Cache["UPLOAD" + userVo.UserId] != null) Cache.Remove("UPLOAD" + userVo.UserId);
            if (dtUploadFile.Rows.Count > 0) Cache.Insert("UPLOAD" + userVo.UserId, dtUploadFile);
        }

        private void BindGrid(DataTable dtUploadFile)
        {
            pnlOnlneIssueUpload.Visible = false;

            GetExtractData(dtUploadFile);

            DataTable dtUploadData = (DataTable)Cache["UPLOAD" + userVo.UserId];

            gvOnlineIssueUpload.DataSource = dtUploadData;
            gvOnlineIssueUpload.DataBind();

            if (dtUploadData == null)
            {
                ShowMessage("No data available");
                return;
            }
            if (dtUploadData.Rows.Count <= 0)
            {
                ShowMessage("No data available");
                return;
            }

            pnlOnlneIssueUpload.Visible = true;
        }

        protected void gvOnlineIssueUpload_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem) {
            //    GridDataItem item = e.Item as GridDataItem;
            //    ite
            //}
            //if(e.
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem item = e.Item as GridDataItem;
            //    DropDownList myDD = item.FindControl("ddAction") as DropDownList;
            //    string itemVal = ((DataRowView)e.Item.DataItem).Row["StructureId"].ToString();
            //    myDD.Items.Insert(0, "Action");
            //    myDD.Items.Insert(1, new ListItem("View Details", itemVal));
            //    myDD.Items.Insert(2, new ListItem("View Mapped Schemes", itemVal));
            //    myDD.SelectedIndexChanged += new EventHandler(ddAction_OnSelectedIndexChanged);
            //}
        }

        protected void btnUploadData_Click(object sender, EventArgs e)
        {
            int nRows=0;
            if (Cache["UPLOAD" + userVo.UserId] == null) {
                ShowMessage("No data to upload");
                return;
            }
            DataTable dtUploadData = (DataTable)Cache["UPLOAD" + userVo.UserId];

            if (boNcdBackOff == null) boNcdBackOff = new OnlineNCDBackOfficeBo();
           nRows= boNcdBackOff.UploadCheckOrderFile(dtUploadData, int.Parse(ddlFileType.SelectedValue), ddlSource.SelectedValue);
           ShowMessage("data uploaded");
        }
     }
}