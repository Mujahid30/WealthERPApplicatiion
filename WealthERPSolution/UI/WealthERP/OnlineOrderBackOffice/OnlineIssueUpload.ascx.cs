using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text;
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
        StringBuilder columnNameError = new StringBuilder();


        protected void Page_Load(object sender, EventArgs e)
        {
            OnlineUserSessionBo.CheckSession();
            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];

            if (!string.IsNullOrEmpty(Session["userVo"].ToString()))
                userVo = (UserVo)Session[SessionContents.UserVo];

            if (!IsPostBack)
            {
                if (Cache["UPLOAD" + userVo.UserId] != null) Cache.Remove("UPLOAD" + userVo.UserId);


            }

        }

        private void BindIssuerIssue(string productCategory)
        {
            try
            {
                DataSet dsIssuer = new DataSet();
                boNcdBackOff = new OnlineNCDBackOfficeBo();


                dsIssuer = boNcdBackOff.GetUploadIssue(productCategory, advisorVo.advisorId, "upload", 0);
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
            lblAllotementType.Visible = false;
            ddlAlltmntTyp.Visible = false;
            lblRgsttype.Visible = false;
            ddlRgsttype.Visible = false;
            RFVRgsttype.Enabled = false;
            RFVddlAlltmnt.Enabled = false;
            if (ddlFileType.SelectedValue == "18" || ddlFileType.SelectedValue == "25")
            {
                lblmsg.Text = ".txt format, pipe delimited, headers required";
            }
            else if (ddlFileType.SelectedValue == "12" || ddlFileType.SelectedValue == "13" || ddlFileType.SelectedValue == "14" || ddlFileType.SelectedValue == "15")
            {
                lblmsg.Text = " .CSV (MS-DOS) format, comma delimited, hearders required";
                lblAllotementType.Visible = true;
                ddlAlltmntTyp.Visible = true;
                lblRgsttype.Visible = true;
                ddlRgsttype.Visible = true;
                RFVRgsttype.Enabled = true;
                RFVddlAlltmnt.Enabled = true;
                btnUploadData.Visible = false;
                lblUploadData.Visible = false;
            }
            else if (ddlFileType.SelectedValue == "10")
            {
                lblmsg.Text = " .CSV (MS-DOS) format, comma delimited, hearders required";
                lblAllotementType.Visible = false;
                ddlAlltmntTyp.Visible = false;
                lblRgsttype.Visible = false;
                ddlRgsttype.Visible = false;
                RFVRgsttype.Enabled = false;
                RFVddlAlltmnt.Enabled = false;
                btnUploadData.Visible = true;
                lblUploadData.Visible = true;
            }

        }

        protected void Readcsvfile()
        {
            //StreamReader StreamReader=new StreamReader(
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            string fileTypeError;
            hdnsavePath.Value = Server.MapPath("UploadFiles/");
            DataTable dtUploadFile =new DataTable();
            DataTable dtValidatedData;
            OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
            if (FileUpload.HasFile)
            {
                String fileName = FileUpload.FileName;
                hdnsavePath.Value += advisorVo.advisorId.ToString() + userVo.UserId.ToString() + fileName;
                if (CheckUploadFileType(fileName, out fileTypeError))
                {

                    FileUpload.SaveAs(hdnsavePath.Value);
                    ShowMessage(fileName + "Uploaded","S");
                    if (onlineNCDBackOfficeBo == null) onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
                    if (ddlFileType.SelectedValue == "12" || ddlFileType.SelectedValue == "13" || ddlFileType.SelectedValue == "14" || ddlFileType.SelectedValue == "15")
                    {
                        dtUploadFile = onlineNCDBackOfficeBo.ReadCsvFile(hdnsavePath.Value, Convert.ToInt32(ddlRgsttype.SelectedValue));
                    }
                    else
                    {
                        dtUploadFile = onlineNCDBackOfficeBo.ReadCsvFile(hdnsavePath.Value, Convert.ToInt32(ddlFileType.SelectedValue));
                    }
                    if (File.Exists(hdnsavePath.Value)) File.Delete(hdnsavePath.Value);
                }
                else
                {
                    ShowMessage(fileTypeError,"F");
                    return;
                }

            }
            else
            {
                ShowMessage("Could not read the file","F");
                return;
            }

            if (dtUploadFile == null)
            {
                ShowMessage("Error in reading file","F");
                return;
            }

            if (dtUploadFile.Rows.Count <= 0)
            {
                ShowMessage("No data in the file","F");
                return;
            }
            if (boNcdBackOff == null) boNcdBackOff = new OnlineNCDBackOfficeBo();
            if (ddlFileType.SelectedValue == "12" || ddlFileType.SelectedValue == "13" || ddlFileType.SelectedValue == "14" || ddlFileType.SelectedValue == "15")
            {
                dtValidatedData = boNcdBackOff.ValidateUploadData(dtUploadFile, int.Parse(ddlRgsttype.SelectedValue), ddlAlltmntTyp.SelectedValue, ref columnNameError);
            }
            else
            {
                dtValidatedData = boNcdBackOff.ValidateUploadData(dtUploadFile, int.Parse(ddlFileType.SelectedValue), ddlSource.SelectedValue, ref columnNameError);
            }
            BindGrid(dtValidatedData);
            ToggleUpload(dtValidatedData);
        }
        private Boolean CheckUploadFileType(string fileName,out string fileTypErrorMessage)
        {
            fileTypErrorMessage = "";
            string FileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1);
            Boolean relVal = true;
            if ((ddlFileType.SelectedValue == "18" || ddlFileType.SelectedValue == "25") && (FileExtension.ToLower() != "txt"))
            {
                fileTypErrorMessage="Invalid File Type, Please Upload Data in Text Format,Pipe delimeter";
                relVal = false;
            }
            else if ((ddlFileType.SelectedValue == "12" || ddlFileType.SelectedValue == "13" || ddlFileType.SelectedValue == "14" || ddlFileType.SelectedValue == "15" || ddlFileType.SelectedValue == "10" || ddlFileType.SelectedValue == "22") && (FileExtension.ToLower() != "csv"))
            {
                fileTypErrorMessage = "Invalid File Type, Please Upload Data in .CSV (MS-DOS) format, comma delimited, hearders required ";
               relVal = false;
            }
            return relVal;

        }
        private void ToggleUpload(DataTable dtUpload)
        {
            bool bUpload = true;

            btnUploadData.Enabled = false;
            foreach (DataRow row in dtUpload.Rows)
            {
                if (string.IsNullOrEmpty(row["Remarks"].ToString().Trim())) continue;
                bUpload = false;
                ShowMessage("Please check the data in the file & re-upload","F");
                break;
            }


            if (columnNameError.ToString().Contains("Actual Name:"))
            {
                ShowMessage(columnNameError.ToString(),"F");
                bUpload = false;
                return;
            }
            if (ddlFileType.SelectedValue == "12" || ddlFileType.SelectedValue == "13" || ddlFileType.SelectedValue == "14" || ddlFileType.SelectedValue == "15" && bUpload)
            {
                UploadIssueAllotmentData(dtUpload, true);
            }
            else
            {
                btnUploadData.Enabled = true;
                ShowMessage("File data has been uploaded, click Upload Data button to upload","S");
            }

        }
        public void UploadIssueAllotmentData(DataTable dtUploadData, bool IsAllotmentUpload)
        {
            string isIssueAvailable = "";
            string result = "";
            ControlUploadMode(true);
            int nRows = 0;
            if (boNcdBackOff == null) boNcdBackOff = new OnlineNCDBackOfficeBo();
            DataTable dtAllotmentUpload = new DataTable();
            dtUploadData = CheckHeaders(dtUploadData);
            if (IsAllotmentUpload)
            {
                dtAllotmentUpload = boNcdBackOff.UploadAllotmentFile(dtUploadData, int.Parse(ddlFileType.SelectedValue), int.Parse(ddlIssueName.SelectedValue), ref isIssueAvailable, advisorVo.advisorId, ddlSource.SelectedValue, ref result, ddlProduct.SelectedValue, hdnsavePath.Value, userVo.UserId, Convert.ToInt32(ddlType.SelectedValue),hdnddlSubCategory.Value.ToString());
                if (dtAllotmentUpload.Rows.Count > 0)
                {
                    GetUploadData(dtAllotmentUpload);
                }
            }
            else
            {
                nRows = boNcdBackOff.UploadCheckOrderFile(dtUploadData, int.Parse(ddlFileType.SelectedValue), int.Parse(ddlIssueName.SelectedValue), ref isIssueAvailable, advisorVo.advisorId, ddlSource.SelectedValue, ref   result, ddlProduct.SelectedValue, hdnsavePath.Value, userVo.UserId,int.Parse(ddlType.SelectedValue));
            }
            if (isIssueAvailable == "NotEligble")
            {
                ShowMessage("Uploaded file Issue and Selected issue Does not match","F");
            }
            else if (result != string.Empty && result != "1")
            {
                ShowMessage(result,"W");
            }
            else
            {
                ShowMessage("data uploaded","S");

            }

            btnUploadData.Enabled = false;

        }

        private void ShowMessage(string msg,string type)
        {
            //tblMessage.Visible = true;
            //msgRecordStatus.InnerText = msg;
            ////--S(success)
            ////--F(failure)
            ////--W(warning)
            ////--I(information)
            ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wsedrftgyhjukloghjnnnghj", " showMsg('" + msg + "','W');", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mykey", "hide();", true);
            tblMessagee.Visible = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wsedrftgyhjukloghjnnnghj", " showMsg('" + msg + "','" + type + "');", true);
        }
        protected void ddSubCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddSubCategory.SelectedValue != "0")
            {
                hdnddlSubCategory.Value = ddSubCategory.SelectedValue;
                BindIssuerIssue(hdnddlSubCategory.Value);
                if (ddSubCategory.SelectedValue == "FICGCG" || ddSubCategory.SelectedValue == "FICDCD")
                    HideControls(ddSubCategory.SelectedValue,sender,e);
                
            }
        }
        private void HideControls(string categoryType, object sender, EventArgs e)
        {
            ddlSource.SelectedValue = "BSE";
            lblSource.Visible = false;
            ddlSource.Visible = false;
            tdlblSource.Visible = false;
            tdddlSource.Visible = false;
            SetFileType();
            ddlFileType.SelectedValue = "12";
            ddlFileType.Enabled = false;
            OnSelectedIndexChanged_ddlFileType(sender, e);
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            tdlblSubCategory.Visible = false;
            tdddSubCategory.Visible = false;
            hdnddlSubCategory.Value = "FIFIIP";
            if (ddlProduct.SelectedValue == "0")
                return;
            SetFileType();
            if (ddlProduct.SelectedValue == "FI")
            {
                SubCategory();
                tdlblSubCategory.Visible = true;
                tdddSubCategory.Visible = true;
            }
            else
            {
                BindIssuerIssue(hdnddlSubCategory.Value);
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
        protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page.Validate("FileType");

            if (!Page.IsValid)
            {
                ShowMessage("Please check all required fields","W");
                return;
            }
            SetFileType();
        }


        protected void gvOnlineIssueUpload_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtUpload = (DataTable)Cache["UPLOAD" + userVo.UserId];
            if (dtUpload != null) gvOnlineIssueUpload.DataSource = dtUpload;
        }
        protected void gvAllotmentUploadData_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtUpload = (DataTable)Cache["UPLOAD" + userVo.UserId];
            if (dtUpload != null) gvAllotmentUploadData.DataSource = dtUpload;
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

        private void GetExtractData(DataTable dtUploadFile)
        {
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
                ShowMessage("No data available","F");
                return;
            }
            if (dtUploadData.Rows.Count <= 0)
            {
                ShowMessage("No data available","F");
                return;
            }

            pnlOnlneIssueUpload.Visible = true;
        }
        protected void gvAllotmentUploadData_ItemDataBound(object sender, GridItemEventArgs e)
        {
            GridDataItem dataBoundItem = e.Item as GridDataItem;
            if (dataBoundItem != null)
            {
                if (dataBoundItem["Upload Status"].Text.ToString().Contains("Invalid"))
                {
                    dataBoundItem["Upload Status"].BackColor = System.Drawing.Color.Red;
                    dataBoundItem["Upload Status"].Font.Bold = true;
                }

            }
        }

        protected void gvOnlineIssueUpload_ItemDataBound(object sender, GridItemEventArgs e)
        {

            GridDataItem dataBoundItem = e.Item as GridDataItem;
            if (dataBoundItem != null)
            {
                if (dataBoundItem["Remarks"].Text.ToString().Contains("Error"))
                {
                    dataBoundItem["Remarks"].BackColor = System.Drawing.Color.Red;
                    dataBoundItem["Remarks"].Font.Bold = true;

                    string str = dataBoundItem["Remarks"].Text.ToString();
                    if (dataBoundItem["Remarks"].Text.ToString().Contains("Cheque Date(DD/MM/YYYY)"))
                    {
                        dataBoundItem["Cheque Date(DD/MM/YYYY)"].BackColor = System.Drawing.Color.Red;
                        dataBoundItem["Remarks"].Font.Bold = true;
                    }
                    else
                    {
                        dataBoundItem[str.Substring(9, str.IndexOf('(') - 9)].BackColor = System.Drawing.Color.Red;
                        dataBoundItem["Remarks"].Font.Bold = true;
                    }
                }
                else
                {
                    dataBoundItem["Remarks"].Text = "Verified";
                }
            }

        }
        private void AddHeaders(DataTable dtData)
        {
        }
        private DataTable CheckHeaders(DataTable dtUploadData)
        {
            boNcdBackOff = new OnlineNCDBackOfficeBo();
            List<OnlineIssueHeader> updHeaders;
            if (int.Parse(ddlFileType.SelectedValue) == 12 || ddlFileType.SelectedValue == "13" || ddlFileType.SelectedValue == "14" || ddlFileType.SelectedValue == "15")
            {
                updHeaders = boNcdBackOff.GetHeaderDetails(int.Parse(ddlRgsttype.SelectedValue), ddlAlltmntTyp.SelectedValue);
            }
            else
            {
                updHeaders = boNcdBackOff.GetHeaderDetails(int.Parse(ddlFileType.SelectedValue), ddlSource.SelectedValue);
            }
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

            if (Cache["UPLOAD" + userVo.UserId] == null)
            {
                ShowMessage("No data to upload","W");

                btnUploadData.Enabled = false;
                return;
            }

            else
            {
                DataTable dtUploadData = (DataTable)Cache["UPLOAD" + userVo.UserId];
                UploadIssueAllotmentData(dtUploadData, false);


            }
        }
        private void GetUploadData(DataTable DtUploadData)
        {
            gvOnlineIssueUpload.Visible = false;
            gvAllotmentUploadData.Visible = true;
            DtUploadData = boNcdBackOff.GetOnlineAllotment(int.Parse(ddlRgsttype.SelectedValue), ddlAlltmntTyp.SelectedValue, DtUploadData);
            gvAllotmentUploadData.DataSource = null;
            gvAllotmentUploadData.DataSource = DtUploadData;
            gvAllotmentUploadData.DataBind();
            if (Cache["UPLOAD" + userVo.UserId] != null) Cache.Remove("UPLOAD" + userVo.UserId);

            if (DtUploadData.Rows.Count > 0) Cache.Insert("UPLOAD" + userVo.UserId, DtUploadData);
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
        protected void ddlAlltmntTyp_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAlltmntTyp.SelectedValue != "select")
            {
                BindAllotmentFileType(ddlAlltmntTyp.SelectedValue, hdnddlSubCategory.Value.ToString());
            }
        }
        protected void BindAllotmentFileType(string fileType, string productType)
        {
            OnlineNCDBackOfficeBo OnlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
            DataTable dtAllotmentFileType;
            dtAllotmentFileType = OnlineNCDBackOfficeBo.GetNCDAllotmentFileType(fileType, productType);
            dtAllotmentFileType.Columns.Add("RegisterType", typeof(string), "PAG_AssetGroupName +' '+ XES_SourceName");
            if (dtAllotmentFileType.Rows.Count > 0)
            {
                ddlRgsttype.DataSource = dtAllotmentFileType;
                ddlRgsttype.DataValueField = dtAllotmentFileType.Columns["WEFT_Id"].ToString();
                ddlRgsttype.DataTextField = "RegisterType";
                ddlRgsttype.DataBind();
            }
            ddlRgsttype.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }
    }
}
