﻿using System;
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
using Microsoft.ApplicationBlocks.ExceptionManagement;


 
using BoCommon;

 
using VoOnlineOrderManagemnet;

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

        private void BindIssuerIssue(string product)
        {
            try
            {
                DataSet dsIssuer = new DataSet();
                boNcdBackOff = new OnlineNCDBackOfficeBo();
              

                dsIssuer = boNcdBackOff.GetUploadIssue(product, advisorVo.advisorId);
                if (dsIssuer.Tables[0].Rows.Count > 0)
                {
                    ddlIssueName.DataSource = dsIssuer;
                    ddlIssueName.DataValueField = dsIssuer.Tables[0].Columns["AIM_IssueId"].ToString();
                    ddlIssueName.DataTextField = dsIssuer.Tables[0].Columns["AIM_IssueName"].ToString();
                    ddlIssueName.DataBind();
                }
               // ddlIssueName.Items.Insert(0, new ListItem("Select", "Select"));
                 
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            

        }

        protected void OnSelectedIndexChanged_ddlFileType(object sender, EventArgs e)
        {
            if (ddlFileType.SelectedValue =="18")
            {
                lblmsg.Text = ".txt format, pipe delimited, headers required";
            }
            if (ddlFileType.SelectedValue == "12" || ddlFileType.SelectedValue == "10")
            {
                lblmsg.Text = " .CSV (MS-DOS) format, comma delimited, hearders required";
            }
            //if (ddlFileType.SelectedValue == "10")
            //{
            //    lblmsg.Text = ".CSV (MS-DOS) format,comma delimited, headers required";
            //}
        }

        protected void Readcsvfile()
        {
            //StreamReader StreamReader=new StreamReader(
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            String savePath = Server.MapPath("UploadFiles/");
            DataTable dtUploadFile;
            OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
            if (FileUpload.HasFile) {
                String fileName = FileUpload.FileName;
                savePath += advisorVo.advisorId.ToString() + userVo.UserId.ToString() + fileName;
                FileUpload.SaveAs(savePath);

                ShowMessage(fileName + "Uploaded");
                if (onlineNCDBackOfficeBo == null) onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();

                dtUploadFile = onlineNCDBackOfficeBo.ReadCsvFile(savePath,Convert.ToInt32(ddlFileType.SelectedValue));

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

            
            //DataTable dtReqData = new DataTable();

            //var datatable = new DataTable();
            //var abccolumns = datatable.Columns.Cast<DataColumn>()
            //                                  .Where(c => c.ColumnName.StartsWith("abc"));

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
                ShowMessage("File data has been uploaded, click Upload Data button to upload");
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
            //Page.Validate("FileType");

            //if (!Page.IsValid)
            //{
            //    ShowMessage("Please check all required fields");
            //    return;
            //}
            if (ddlProduct.SelectedValue == "0")
                return;
            SetFileType();
            BindIssuerIssue(ddlProduct.SelectedValue );
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
             
        }

        private void AddHeaders(DataTable dtData)
        {
            //int nRows = 0;
            //boNcdBackOff = new OnlineNCDBackOfficeBo();
            //List<OnlineIssueHeader> updHeaders = boNcdBackOff.GetHeaderDetails(int.Parse(ddlFileType.SelectedValue), ddlSource.SelectedValue);
            //DataRow dr=dtData.Rows[0];
            //foreach (OnlineIssueHeader header in updHeaders)
            //{
            //    dtData.Rows.InsertAt(dr, 0);
            //}


        }
        private DataTable CheckHeaders(DataTable dtUploadData)
        {

            int nRows = 0;
            boNcdBackOff = new OnlineNCDBackOfficeBo();


            List<OnlineIssueHeader> updHeaders = boNcdBackOff.GetHeaderDetails(int.Parse(ddlFileType.SelectedValue), ddlSource.SelectedValue);

            foreach (OnlineIssueHeader header in updHeaders)
            {
                if (header.IsUploadRelated == true)
                {
                    if (dtUploadData.Columns.Contains(header.HeaderName))
                    {
                        dtUploadData.Columns[header.HeaderName].ColumnName = header.ColumnName;
                    }                    
                }
                else
                {
                    if (dtUploadData.Columns.Contains(header.HeaderName))
                    {
                        dtUploadData.Columns.Remove(dtUploadData.Columns[header.HeaderName]);

                    }                

                }

            }

            //dtUploadData.Columns.RemoveAt(0);
            //dtUploadData.Columns.RemoveAt(1);

            if (dtUploadData.Columns.Contains("SN"))
            {
                dtUploadData.Columns.Remove(dtUploadData.Columns["SN"]);

            }
            if (dtUploadData.Columns.Contains("Remarks"))
            {
                dtUploadData.Columns.Remove(dtUploadData.Columns["Remarks"]);

            }

         

            dtUploadData.AcceptChanges();

            return dtUploadData;

        }
        protected void btnUploadData_Click(object sender, EventArgs e)
        {
            string isIssueAvailable = "";
            string result="";
            int isIssueAlloted=0;
            ControlUploadMode(true);
            int nRows=0;
            if (Cache["UPLOAD" + userVo.UserId] == null) {
                ShowMessage("No data to upload");
                
                btnUploadData.Enabled = false;
                return;
            }

            boNcdBackOff.IsIssueAlloted(int.Parse(ddlIssueName.SelectedValue), ref   isIssueAlloted) ;
            if (isIssueAlloted == 0)
            {
                ShowMessage("Pls Fill Alootment Date");
            }
            else
            {

                DataTable dtUploadData = (DataTable)Cache["UPLOAD" + userVo.UserId];

                if (boNcdBackOff == null) boNcdBackOff = new OnlineNCDBackOfficeBo();
                dtUploadData = CheckHeaders(dtUploadData);

                nRows = boNcdBackOff.UploadCheckOrderFile(dtUploadData, int.Parse(ddlFileType.SelectedValue), int.Parse(ddlIssueName.SelectedValue), ref isIssueAvailable, advisorVo.advisorId, ddlSource.SelectedValue, ref   result);
                if (isIssueAvailable == "NotEligble")
                {
                    ShowMessage("Uploaded file Issue and Selected issue Does not match ");
                }
                else if (result != string.Empty)
                {
                    ShowMessage(result);
                }
                else
                {
                    ShowMessage("data uploaded");

                }

                btnUploadData.Enabled = false;
            }
        }
        private void ControlUploadMode(bool uploadMode)
        {
            ddlProduct.Enabled = false;
            ddlSource.Enabled = false;
            ddlFileType.Enabled = false;
            ddlIssueName.Enabled = false;
            FileUpload.Enabled = false;
            btnFileUpload.Enabled = false;
        }
        protected void lnkClick_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('OnlineIssueUpload','none');", true);
        }
     }
}
