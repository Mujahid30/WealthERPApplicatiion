﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using BoOnlineOrderManagement;
using VoUser;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.IO;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineIssueExtract : System.Web.UI.UserControl
    {
        OnlineNCDBackOfficeBo boNcdBackOff = new OnlineNCDBackOfficeBo();
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            
            //if (Cache["IssueExtract" + userVo.UserId] != null) Cache.Remove("IssueExtract" + userVo.UserId);

            if (!IsPostBack)
            {
                if (Cache["IssueExtract" + userVo.UserId] != null) Cache.Remove("IssueExtract" + userVo.UserId);
                SetDownloadDate();
            }
        }

        protected void BindIssueExtract()
        {
            pnlOnlneIssueExtract.Visible = false;

            GetExtractData();

            DataTable dtExtractData = (DataTable)Cache["IssueExtract" + userVo.UserId];
            
            gvOnlineIssueExtract.DataSource = dtExtractData;
            gvOnlineIssueExtract.DataBind();

            if (dtExtractData == null)
            {
                ShowMessage("No data available");
                return;
            }
            if (dtExtractData.Rows.Count <= 0)
            {
                ShowMessage("No data available");
                return;
            }
            
            pnlOnlneIssueExtract.Visible = true;
        }


        protected void gvOnlineIssueExtract_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtExtractData = (DataTable)Cache["IssueExtract" + userVo.UserId];

            if (dtExtractData != null) gvOnlineIssueExtract.DataSource = dtExtractData;
        }

        protected void btnIssueExtract_Click(object sender, EventArgs e)
        {
            Page.Validate("IssueExtract");
            if (!Page.IsValid) {
                ShowMessage("Please check all required fields");
                return;
            }

            boNcdBackOff.GenerateOnlineNcdExtract(adviserVo.advisorId, userVo.UserId, ddlExternalSource.SelectedValue, ddlProduct.SelectedValue,Convert.ToInt32(ddlIssueName.SelectedValue));

            ShowMessage("Extraction Done For "+ddlIssueName.SelectedItem.Text);
            //lnkClick.Visible = true;
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Page.Validate("IssueExtract");
            if (!Page.IsValid)
            {
                ShowMessage("Please check all required fields");
                return;
            }
            BindIssueExtract();
        }

        private void SetDownloadDate()
        {
            rdpDownloadDate.SelectedDate = DateTime.Now;
        }

        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mykey", "hide();", true);
        }

        protected void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvOnlineIssueExtract.ExportSettings.OpenInNewWindow = true;
            gvOnlineIssueExtract.ExportSettings.ExportOnlyData = true;
            gvOnlineIssueExtract.ExportSettings.IgnorePaging = true;
            gvOnlineIssueExtract.ExportSettings.FileName = "IssueExtract";
            gvOnlineIssueExtract.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvOnlineIssueExtract.MasterTableView.ExportToExcel();
        }

        protected void btnDownload_Click(object sender, EventArgs e)

        {
           
            GetExtractData();

            DataTable dtExtractData = (DataTable)Cache["IssueExtract" + userVo.UserId];
            string filename = ddlExternalSource.SelectedValue + ddlProduct.SelectedItem.Text + DateTime.Now.ToString("ddMMyy") + ".csv";
            string delimit = ",";
           // ControlExtractMode(false);
            DownloadBidFile(dtExtractData, filename, delimit);
          
          
        }

        private void SetFileType()
        {
            DataTable dtFileType = boNcdBackOff.GetFileTypeList(0, ddlExternalSource.SelectedValue, 'E', ddlProduct.SelectedValue);

            DataRow drFileType = dtFileType.NewRow();
            drFileType["WEFT_Id"] = 0;
            drFileType["WEFT_FileType"] = "--SELECT--";
            dtFileType.Rows.InsertAt(drFileType, 0);

            ddlFileType.DataSource = dtFileType;
            ddlFileType.DataValueField = dtFileType.Columns["WEFT_Id"].ToString();
            ddlFileType.DataTextField = dtFileType.Columns["WEFT_FileType"].ToString();
            ddlFileType.DataBind();
        }
        private void BindIssue(string product)
        {
            //try
            //{
                DataSet dsIssuer = new DataSet();
                boNcdBackOff = new OnlineNCDBackOfficeBo();


                dsIssuer = boNcdBackOff.GetUploadIssue(product, adviserVo.advisorId);
                if (dsIssuer.Tables[0].Rows.Count > 0)
                {
                    ddlIssueName.DataSource = dsIssuer;
                    ddlIssueName.DataValueField = dsIssuer.Tables[0].Columns["AIM_IssueId"].ToString();
                    ddlIssueName.DataTextField = dsIssuer.Tables[0].Columns["AIM_IssueName"].ToString();
                    ddlIssueName.DataBind();
                }
                // ddlIssueName.Items.Insert(0, new ListItem("Select", "Select"));

            //}
            //catch (BaseApplicationException Ex)
            //{
            //    throw Ex;
            //}


        }
        private void DownloadBidFile(DataTable dtExtractData, string filename, string delimit)
        {
            if (dtExtractData == null)
            {
                ShowMessage("No data available");
                 return;
                 
            }
            if (dtExtractData.Rows.Count <= 0)
            {
                ShowMessage("No data available");
                return;
              
            
            }

            string dateFormat = "dd-mm-yyyy";

            StringWriter sWriter = new StringWriter();

            string Columns = string.Empty;
            
            foreach (DataColumn column in dtExtractData.Columns) Columns += column.ColumnName + delimit;
            
            sWriter.WriteLine(Columns.Remove(Columns.Length - 1, 1));

            DataColumn[] arrCols = new DataColumn[dtExtractData.Columns.Count];
            dtExtractData.Columns.CopyTo(arrCols, 0);
            foreach (DataRow datarow in dtExtractData.Rows)
            {
                string row = string.Empty;
                int i = 0;
                foreach (object item in datarow.ItemArray)
                {
                    if (arrCols[i].DataType.FullName == "System.DateTime")
                    {
                        string strDate = string.IsNullOrEmpty(item.ToString()) ? "" : DateTime.Parse(item.ToString()).ToString(dateFormat);
                        row += strDate + delimit;
                    }
                    else
                    {
                        row += item.ToString() + delimit;
                    }
                    i++;
                }
                sWriter.WriteLine(row.Remove(row.Length - 1, 1));
            }
            Response.ContentType = "text/plain";

            Response.AddHeader("content-disposition", "attachment;filename=" + string.Format(filename, string.Format("{0:ddMMyyyy}", DateTime.Today)));
            Response.Clear();

            using (StreamWriter writer = new StreamWriter(Response.OutputStream, System.Text.Encoding.UTF8))
            {
                writer.Write(sWriter.ToString());
            }
            Response.End();
            
            sWriter.Flush();
            sWriter.Close();
        }

        protected void ddlExternalSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page.Validate("FileType");

            if (!Page.IsValid) {
                ShowMessage("Please check all required fields");
                return;
            }
            SetFileType();
        }

        private void GetExtractData()
        {
            DataTable dtExtractData = boNcdBackOff.GetOnlineNcdExtractPreview(rdpDownloadDate.SelectedDate.Value, adviserVo.advisorId, int.Parse(ddlFileType.SelectedValue), ddlExternalSource.SelectedValue,int.Parse(ddlIssueName.SelectedValue));

            if (Cache["IssueExtract" + userVo.UserId] != null) Cache.Remove("IssueExtract" + userVo.UserId);
            if (dtExtractData.Rows.Count > 0) Cache.Insert("IssueExtract" + userVo.UserId, dtExtractData);
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
            BindIssue(ddlProduct.SelectedValue);
        }
        //protected void lnkClick_Click(object sender, EventArgs e)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('OnlineIssueExtract','none');", true);
        //}
        //private void ControlExtractMode(bool Extractmode)
        //{
        //    ddlProduct.Enabled = Extractmode;
        //    ddlIssueName.Enabled = Extractmode;
        //    ddlFileType.Enabled = Extractmode;
        //    ddlExternalSource.Enabled = Extractmode;
        //    btnIssueExtract.Enabled = Extractmode;
        //    btnPreview.Enabled = Extractmode;
        //    btnDownload.Enabled = Extractmode;
        //}
    }
}
