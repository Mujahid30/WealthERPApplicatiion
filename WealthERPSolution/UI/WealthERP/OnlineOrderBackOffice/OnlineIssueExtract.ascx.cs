using System;
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
        // OnlineOrderBackOfficeBo onlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        CommonLookupBo boCommon = new CommonLookupBo();
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
                CheckForBusinessDate();
            }
        }
        protected void OnSelectedIndexChanged_ddlFileType(object sender, EventArgs e)
        {
            btnPreview.Visible = true;
            if (ddlFileType.SelectedValue == "1")
            {
                lblMsg.Text = " .txt format, pipe delimited, headers not required";
            }
            if (ddlFileType.SelectedValue == "16")
            {
                lblMsg.Text = " .CSV (MS-DOS) format, comma delimited, headers required";
            }
            if (ddlFileType.SelectedValue == "11" || ddlFileType.SelectedValue == "5")
            {
                lblMsg.Text = ".xls format, headers required";
            }
            if (ddlFileType.SelectedValue == "55")
            {
                btnPreview.Visible = false;
            }
            pnlOnlneIssueExtract.Visible = false;

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

        private void BindExtSource(string product, int issueId)
        {
            DataSet dsIssuer = new DataSet();
            boNcdBackOff = new OnlineNCDBackOfficeBo();


            dsIssuer = boNcdBackOff.GetExtSource(product, issueId,ddSubCategory.SelectedValue);
            if (dsIssuer.Tables[0].Rows.Count > 0)
            {
                ddlExternalSource.DataSource = dsIssuer;
                ddlExternalSource.DataValueField = dsIssuer.Tables[0].Columns["XES_SourceCode"].ToString();
                ddlExternalSource.DataTextField = dsIssuer.Tables[0].Columns["XES_SourceCode"].ToString();
                ddlExternalSource.DataBind();
            }
            if (ddlType.SelectedValue=="0") ddlExternalSource.Items.FindByText("IOPS").Enabled = false;
            ddlExternalSource.Items.Insert(0, new ListItem("Select", "Select"));
        }

        protected void btnIssueExtract_Click(object sender, EventArgs e)
        {
            //Page.Validate("IssueExtract");
            //if (!Page.IsValid) {
            //    ShowMessage("Please check all required fields");
            //    return;
            //}
            int isextracted = 0;
            ddlExternalSource.SelectedValue = "IOPS";
            SetFileType(ddlExternalSource.SelectedValue);

            boNcdBackOff.GenerateOnlineNcdExtract(adviserVo.advisorId, userVo.UserId, ddlExternalSource.SelectedValue, ddlProduct.SelectedValue, Convert.ToInt32(ddlIssueName.SelectedValue), ref isextracted, Convert.ToInt32(ddlType.SelectedValue),ddSubCategory.SelectedValue);
            if (isextracted == 0)
                ShowMessage("Extraction Done Only On Business Days");
            else
                ShowMessage("Extraction Done For " + ddlIssueName.SelectedItem.Text);
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
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mykey", "hide();", true);
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

        private void ExportInExcel(string filename)
        {

            gvOnlineIssueExtract.ExportSettings.OpenInNewWindow = true;
            gvOnlineIssueExtract.ExportSettings.ExportOnlyData = true;
            gvOnlineIssueExtract.ExportSettings.IgnorePaging = true;
            gvOnlineIssueExtract.ExportSettings.HideStructureColumns = true;
            gvOnlineIssueExtract.ExportSettings.FileName = filename;
            gvOnlineIssueExtract.ExportSettings.Excel.Format    =Telerik.Web.UI.GridExcelExportFormat.ExcelML;
            gvOnlineIssueExtract.MasterTableView.ExportToExcel();


        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (ddlFileType.SelectedValue != "57")
            {
                string filename = "";
                string delimit = "";
                string format = "";


                DataTable dtExtractData = (DataTable)Cache["IssueExtract" + userVo.UserId];
                string extractStepCode = boNcdBackOff.GetExtractStepCode(Convert.ToInt32(ddlFileType.SelectedValue));

                boNcdBackOff.GetFileName(ddlExternalSource.SelectedValue, Convert.ToInt32(ddlFileType.SelectedValue), ref filename, ref delimit, ref format, ddlIssueName.SelectedItem.Text);

                if (format == ".xls")
                {
                    ExportInExcel(filename);
                }
                else
                {
                    filename = filename + format;
                    DownloadBidFile(dtExtractData, filename, delimit, extractStepCode);
                }
            }
            else if (ddlFileType.SelectedValue == "57")
            {
                string XMLText = boNcdBackOff.GetOnlineSGBExtractXMLText(rdpDownloadDate.SelectedDate.Value, adviserVo.advisorId, int.Parse(ddlFileType.SelectedValue), ddlExternalSource.SelectedValue, int.Parse(ddlIssueName.SelectedValue));
                string strIssue;
                if (ddlIssueName.SelectedItem.ToString().Length > 15)
                    strIssue = ddlIssueName.SelectedItem.ToString().Substring(0, 4);
                else
                    strIssue = ddlIssueName.SelectedItem.ToString();
                string filename = ddlExternalSource.SelectedValue + "_BidExtr_" + strIssue+"_" + rdpDownloadDate.SelectedDate.Value.ToString("ddMMyy") + ".xml";
                StreamWriter str = new StreamWriter(Server.MapPath(@"~/UploadFiles/" + filename), false, System.Text.Encoding.ASCII);
                str.WriteLine(XMLText);
                str.Flush();
                str.Close();
                #region download notepad or text file.
                Response.ContentType = "Text/Plain";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
                string aaa = Server.MapPath("~/UploadFiles/" + filename);
                Response.TransmitFile(Server.MapPath("~/UploadFiles/" + filename));
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
                #endregion
            }

         }


        private void SetFileType(string externalSource)
        {
            DataTable dtFileType = boNcdBackOff.GetFileTypeList(0, externalSource, 'E', ddlProduct.SelectedValue,ddSubCategory.SelectedValue);

            DataRow drFileType = dtFileType.NewRow();
            drFileType["WEFT_Id"] = 0;
            drFileType["WEFT_FileType"] = "--SELECT--";
            dtFileType.Rows.InsertAt(drFileType, 0);

            ddlFileType.DataSource = dtFileType;
            ddlFileType.DataValueField = dtFileType.Columns["WEFT_Id"].ToString();
            ddlFileType.DataTextField = dtFileType.Columns["WEFT_FileType"].ToString();
            ddlFileType.DataBind();
            if (ddlType.SelectedValue == "1" && externalSource != "IOPS" && ddSubCategory.SelectedValue != "FISSGB")
            {
                ddlFileType.Items.FindByText("Bid Order File-ASBA").Enabled = false;
                ddlFileType.Items.FindByText("Bid Order File-NASBA").Enabled = false;
               
            }
        }
        private void BindIssue(string product)
        {
            //try
            //{
            DataSet dsIssuer = new DataSet();
            boNcdBackOff = new OnlineNCDBackOfficeBo();


            dsIssuer = boNcdBackOff.GetUploadIssue(product, adviserVo.advisorId, "Extract", Convert.ToInt32(ddlType.SelectedValue));
            if (dsIssuer.Tables[0].Rows.Count > 0)
            {
                ddlIssueName.DataSource = dsIssuer;
                ddlIssueName.DataValueField = dsIssuer.Tables[0].Columns["AIM_IssueId"].ToString();
                ddlIssueName.DataTextField = dsIssuer.Tables[0].Columns["AIM_IssueName"].ToString();
                ddlIssueName.DataBind();
            }
            ddlIssueName.Items.Insert(0, new ListItem("Select", "Select"));

            //}
            //catch (BaseApplicationException Ex)
            //{
            //    throw Ex;
            //}


        }

        private void DownloadBidFile(DataTable dtExtractData, string filename, string delimit, string extractStepCode)
        {
            try
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

                StreamWriter str = new StreamWriter(Server.MapPath(@"~/UploadFiles/" + filename), false, System.Text.Encoding.Default);

                string Columns = string.Empty;

                foreach (DataColumn column in dtExtractData.Columns)
                    Columns += column.ColumnName + delimit;

                // Headers  For different types
                if (extractStepCode != "EB")
                    str.WriteLine(Columns.Remove(Columns.Length - 1, 1));

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
                            row += item.ToString().Trim() + delimit;
                        }
                        i++;
                    }
                    str.WriteLine(row.Remove(row.Length - 1, 1));
                }
                str.Flush();
                str.Close();
                string myFileData;
                // File in
                myFileData = File.ReadAllText(Server.MapPath("~/UploadFiles/" + filename));
                // Remove last CR/LF
                // 1) Check that the file has CR/LF at the end
                if (myFileData.EndsWith(Environment.NewLine))
                {
                    //2) Remove CR/LF from the end and write back to file (new file)
                    //File.WriteAllText(@"D:\test_backup.csv", myFileData.TrimEnd(null)); // Removes ALL white spaces from the end!
                    File.WriteAllText(Server.MapPath("~/UploadFiles/" + filename), myFileData.TrimEnd(Environment.NewLine.ToCharArray())); // Removes ALL CR/LF from the end!
                }
                #region download notepad or text file.
                Response.ContentType = "Text/Plain";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
                string aaa = Server.MapPath("~/UploadFiles/" + filename);
                Response.TransmitFile(Server.MapPath("~/UploadFiles/" + filename));
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
                #endregion
            }
            catch (Exception e)
            {

            }
            //finally
            //{
            //    System.IO.File.Delete(Server.MapPath("~/UploadFiles/" + filename));
            //}
        }

        protected void ddlExternalSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Page.Validate("FileType");

            //if (!Page.IsValid) {
            //    ShowMessage("Please check all required fields");
            //    return;
            //}
            btnPreview.Visible = true;
            if (ddlExternalSource.SelectedValue=="RBI")
            {
                btnPreview.Visible = false;
            }
            SetFileType(ddlExternalSource.SelectedValue);
        }

        protected void ddlIssueName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssueName.SelectedValue == "Select")
                return;

            BindExtSource(ddlProduct.SelectedValue, Convert.ToInt32(ddlIssueName.SelectedValue));
        }

        private void GetExtractData()
        {
            DataTable dtExtractData = new DataTable();

            dtExtractData = boNcdBackOff.GetOnlineNcdExtractPreview(rdpDownloadDate.SelectedDate.Value, adviserVo.advisorId, int.Parse(ddlFileType.SelectedValue), ddlExternalSource.SelectedValue, int.Parse(ddlIssueName.SelectedValue),ddlTransactionType.SelectedValue,int.Parse(ddlType.SelectedValue));

                if (dtExtractData == null) return;
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
            tdlblSubCategory.Visible = false;
            tdddSubCategory.Visible = false;
            hdnddlSubCategory.Value = "FIFIIP";
            if (ddlProduct.SelectedValue == "FI")
            {
                SubCategory();
                tdlblSubCategory.Visible = true;
                tdddSubCategory.Visible = true;
            }
            else
            {
                BindIssue(hdnddlSubCategory.Value);
            }
        }
        private void SubCategory()
        {
            DataTable dtCategory = new DataTable();
            dtCategory = boNcdBackOff.BindNcdCategory("SubInstrumentCat", "").Tables[0];
            if (dtCategory.Rows.Count > 0)
            {
                ddSubCategory.DataSource = dtCategory;
                ddSubCategory.DataValueField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddSubCategory.DataTextField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddSubCategory.DataBind();
            }
            ddSubCategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void ddSubCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddSubCategory.SelectedValue != "0")
            {
                hdnddlSubCategory.Value = ddSubCategory.SelectedValue;
                BindIssue(hdnddlSubCategory.Value);
            }
        }
        private void CheckForBusinessDate()
        {
            bool isBusinessDate = boCommon.CheckForBusinessDate(DateTime.Now);
            if (!isBusinessDate)
            {
                btnIssueExtract.Enabled = false;
                btnIssueExtract.ToolTip = "Today is not a valid business date";
            }
        }
        protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            tdlblTransaction.Visible = false;
            tdddlTransactionType.Visible = false;
            if (ddlType.SelectedValue == "0")
            {
                tdlblTransaction.Visible = true;
                tdddlTransactionType.Visible = true;
            }

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
