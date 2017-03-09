using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoOnlineOrderManagement;
using System.Data;
using System.IO;
using BoCommon;
using VoCommon;
using VoUser;
using WealthERP.Base;
using System.Configuration;

using BoOps;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineOrderAccountingExtract : System.Web.UI.UserControl
    {
        CommonProgrammingBo commonProgrammingBo;
        DataSet dsextractType;
        OnlineOrderBackOfficeBo OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        OperationBo operationBo = new OperationBo();
        string fileExtension = string.Empty;
        string strGuid = string.Empty;
        string ExtractPath = string.Empty;
        AdvisorVo advisorVo = new AdvisorVo();
        DataSet dsExtractTypeDataForFileCreation;
        DateTime orderDate;
       
        DataTable dtTableForExtract;
        string filename;
        string delimeter;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            ExtractPath = Server.MapPath("UploadFiles");
           
            if (!IsPostBack)
            {
                //txtExtractDate.MaxDate = DateTime.Now;
               // fromdate = DateTime.Now.AddDays(-1);
                txtFromDate.SelectedDate = DateTime.Now.AddDays(-1); 
                txtToDate.SelectedDate = DateTime.Now;
                BindddlExtractType();
                txtExtractDate.SelectedDate = DateTime.Now;
                BindOrderStatus();
            }
        }

        protected void btnExtract_Click(object sender, EventArgs e)
        {
            DateTime fromdate;
            DateTime todate;
            DataSet dsBackOfficeAccountingExtract = new DataSet();
            //  DataTable dtBackOfficeAccountingExtract = new DataTable();


            if (ddlExtractType.SelectedValue == "0")
                return;


            if (ddlExtractType.SelectedValue == "43" || ddlExtractType.SelectedValue == "44" || ddlExtractType.SelectedValue == "45")
            {
                orderDate = Convert.ToDateTime(txtExtractDate.SelectedDate);
                fromdate = Convert.ToDateTime(txtFromDate.SelectedDate);
                todate = Convert.ToDateTime(txtToDate.SelectedDate);
                string filename = GetFilename(Convert.ToInt32(ddlExtractType.SelectedValue));
                string delimit = ",";

                dsBackOfficeAccountingExtract = OnlineOrderBackOfficeBo.GetExtractTypeDataForFileCreation(orderDate, advisorVo.advisorId, Convert.ToInt32(ddlExtractType.SelectedValue), Convert.ToDateTime(txtFromDate.SelectedDate),Convert.ToDateTime(txtToDate.SelectedDate), ddlOrderStatus.SelectedValue);
                if (dsBackOfficeAccountingExtract.Tables.Count <= 0)
                {
                    ShowMessage("No Data Available");
                    
                    return;
                }
                else if (dsBackOfficeAccountingExtract.Tables[0].Rows.Count <= 0)
                {
                    ShowMessage("No Data Available");
                    return;
                }

                DownloadBidFile(dsBackOfficeAccountingExtract.Tables[0], filename, delimit);
            }
            else
            {
                CreateFileForextractAndSaveinServer();
                GetExtractTypeDataForFileCreation();
            }
        }

        private string GetRandomNumber(DateTime dt)
        {
            string strMonth = string.Empty, strday = string.Empty, stryear=string.Empty, strRandomNo = string.Empty;

            int month = dt.Month;
            int day = dt.Day;
            

            if (month == 1)
            {
                strMonth = 1.ToString();
            }
            else
            {
                month = month - 1;
                strMonth = month.ToString();
            }

            if (day == 1)
            {
                strday = 1.ToString();
            }
            else
            {
                day = day - 1;
                strday = day.ToString();
            }
            //if (year == 1)
            //{
            //    stryear = 1.ToString();
            //}
            //else
            //{
            //    year = year - 1;
            //    stryear = year.ToString();
            //}
            // strRandomNo = strRandomNo.PadRight(4, '0') +strday+ strMonth;
            // strRandomNo = "0000"+strMonth + strday;


            strRandomNo = strday + strMonth ;
            if (strRandomNo.Length == 2)
                strRandomNo = "0" + strday + "0" + strMonth ;
            else
                strRandomNo = strday + strMonth;


            return strRandomNo;
        }

        //protected void lnkNCDIPO_Click(object sender, EventArgs e)
        //{
        //    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrder", "loadcontrol('MFOrderBuyTransTypeOffline','login');", true);
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('OnlineOrderNCDIPOAccountingExtract','login');", true);
        //}

        private string GetFilename(int fileId)
        {
            string fileName = string.Empty;
            int r = (new Random()).Next(100, 1000);

            if (fileId == 43)
            {
                fileName = "BackOfficeReport" + "-" + DateTime.Now.ToString("MMddyyyy") + GetRandomNumber(DateTime.Now) + ".csv";
            }
            else if (fileId == 44)
            {
                fileName = "BankSummaryReport" + "-" + DateTime.Now.ToString("MMddyyyy") + GetRandomNumber(DateTime.Now) + ".csv";
            }
            else 
            {
                fileName = "MFOrderReport" + "-" + DateTime.Now.ToString("MMddyyyy") + GetRandomNumber(DateTime.Now) + ".csv";
            }
            return fileName;

        }

        protected void CreateFileForextractAndSaveinServer()
        {
            commonProgrammingBo = new CommonProgrammingBo();
            string strFileNameAndDelimeter = string.Empty;

            strFileNameAndDelimeter = commonProgrammingBo.SetFileNameAndDelimeter(Convert.ToInt32(ddlExtractType.SelectedValue), Convert.ToDateTime(txtExtractDate.SelectedDate),null);
            SetFileNameAndDelimeter(strFileNameAndDelimeter);
            //SetFileNameAndDelimeter(Convert.ToInt32(ddlExtractType.SelectedValue));
            File.WriteAllText(Path.Combine(ExtractPath, filename), ", System.Text.Encoding.Default");
        }

        protected void GetExtractTypeDataForFileCreation()
        {
            orderDate = Convert.ToDateTime(txtExtractDate.SelectedDate);
            dsExtractTypeDataForFileCreation = OnlineOrderBackOfficeBo.GetExtractTypeDataForFileCreation(orderDate, advisorVo.advisorId, Convert.ToInt32(ddlExtractType.SelectedValue),  Convert.ToDateTime(txtFromDate.SelectedDate),Convert.ToDateTime(txtToDate.SelectedDate), ddlOrderStatus.SelectedValue);
            CreateDataTableForExtract();
        }

        protected void CreateDBFFile(int FileId)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=ExtractDetails.dbf");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
            int i = 0;
            foreach (DataColumn dc in dsExtractTypeDataForFileCreation.Tables[0].Columns)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(dsExtractTypeDataForFileCreation.Tables[0].Columns[i].ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
                i++;
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in dsExtractTypeDataForFileCreation.Tables[0].Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int ii = 0; ii < dsExtractTypeDataForFileCreation.Tables[0].Columns.Count; ii++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[ii].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        protected void SetFileNameAndDelimeter(string strFileNameAndDelimeter)
        {
            string[] FileNameAndDelimeter = strFileNameAndDelimeter.Split('~');
            filename = FileNameAndDelimeter[0];
            delimeter = FileNameAndDelimeter[1];
        }


        //protected void SetFileNameAndDelimeter(int FileID)
        //{


        //string strExtractDate = Convert.ToDateTime(txtExtractDate.SelectedDate).ToShortDateString();
        //string[] strSplitExtractDate = strExtractDate.Split('/');

        //string DD = strSplitExtractDate[0].ToString();
        //string MM = strSplitExtractDate[1].ToString();
        //string YYYY = strSplitExtractDate[2].ToString();

        //if(FileID==37)
        //{
        //    filename = "sbiemf" + DD + MM + ".txt";
        //    delimeter = "#";                
        //}
        //else if(FileID==38)
        //{
        //    filename = "sbipay" + DD + MM + ".txt";
        //    delimeter = "   ";
        //}
        //else if(FileID==39)
        //{
        //    filename = "HDFCPAY" + MM + DD + ".txt";
        //    delimeter = " ";
        //}
        //else if(FileID==40)
        //{
        //    filename = "eMF-InProcess" + DD + MM + YYYY + ".txt";
        //    delimeter = "|";
        //}
        //else if(FileID==41)
        //{
        //    filename = "eMF-Executed" + MM + DD + YYYY + ".txt";
        //    delimeter = "|";
        //}
        //else if (FileID == 42)
        //{
        //    filename = "SSL104" + DD + MM + ".txt";
        //    delimeter = ",";
        //}
        //}

        protected void CreateTextFile(int FileID)
        {
            string file = string.Empty;
            if (!string.IsNullOrEmpty(filename))
            {
                if (FileID == 42)
                {
                    string[] strName = filename.Split('.');

                    filename = strName[0] + ".001";

                }

                #region ExportDataTabletoFile
                StreamWriter str = new StreamWriter(Server.MapPath("UploadFiles/" + filename), false, System.Text.Encoding.Default);

                string Columns = string.Empty;

                if (FileID != 37)
                {
                    foreach (DataColumn column in dsExtractTypeDataForFileCreation.Tables[0].Columns)
                    {
                        Columns += column.ColumnName + delimeter;
                    }
                    str.WriteLine(Columns.Remove(Columns.Length - 1, 1));
                }


                foreach (DataRow datarow in dsExtractTypeDataForFileCreation.Tables[0].Rows)
                {
                    string row = string.Empty;
                    foreach (object items in datarow.ItemArray)
                    {
                        row += items.ToString().Trim() + delimeter;
                    }
                    str.WriteLine(row.Remove(row.Length - 1, 1));
                }
                str.Flush();
                str.Close();
                #endregion
                #region download notepad or text file.
                Response.ContentType = "application/octet-stream";



                Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
                string aaa = Server.MapPath("~/UploadFiles/" + filename);
                Response.TransmitFile(Server.MapPath("~/UploadFiles/" + filename));
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
                #endregion
            }
        }

        protected void CreateDataTableForExtract()
        {
            dtTableForExtract = new DataTable();
            if (ddlSaveAs.SelectedValue == "2")
            {
                CreateDBFFile(Convert.ToInt32(ddlExtractType.SelectedValue));
            }
            else if (ddlSaveAs.SelectedValue == "1")
            {
                CreateTextFile(Convert.ToInt32(ddlExtractType.SelectedValue));
            }
        }
        private void BindOrderStatus()
        
        {
            DataSet dsOrderStaus;
            DataTable dtOrderStatus;
            dsOrderStaus = operationBo.Get_Onl_OrderStatus();
            dtOrderStatus = dsOrderStaus.Tables[0];
            if (dtOrderStatus.Rows.Count > 0)
            {
                ddlOrderStatus.DataSource = dtOrderStatus;
                ddlOrderStatus.DataValueField = dtOrderStatus.Columns["WOS_OrderStepCode"].ToString();
                ddlOrderStatus.DataTextField = dtOrderStatus.Columns["WOS_OrderStep"].ToString();
                ddlOrderStatus.DataBind();
            }
            ddlOrderStatus.Items.Insert(0, new ListItem("Select", "Select"));
            ddlOrderStatus.Items.Insert(0, new ListItem("ALL", "ALL"));
            ddlOrderStatus.Items.FindByText("EXECUTED").Selected = true;
           

        }

        protected void ddlExtractType_Selectedindexchanged(object sender, EventArgs e)
        {
            DateTime fromdate;
            DateTime todate;
            ddlSaveAs.Enabled = false;

            if (ddlExtractType.SelectedValue == "45")
            {
                trOrderStatus.Visible = true;
                trFromToDate.Visible = true;
                if (txtFromDate.SelectedDate != null)
                    fromdate = DateTime.Parse(txtFromDate.SelectedDate.ToString());
                if (txtToDate.SelectedDate != null)
                    todate = DateTime.Parse(txtToDate.SelectedDate.ToString());
                trExtractDate.Visible = false;
                ddlSaveAs.SelectedValue = "2";
               
            }
            else
            {
                trOrderStatus.Visible = false;
                trFromToDate.Visible = false;
                trExtractDate.Visible = true;
                if (ddlExtractType.SelectedValue == "43" || ddlExtractType.SelectedValue == "44")
                {
                    ddlSaveAs.SelectedValue = "2";
                }
                else
                {
                    ddlSaveAs.SelectedValue = "1";
                }
                txtExtractDate.SelectedDate = DateTime.Now;
            }
        }

        protected void BindddlExtractType()
        {
            dsextractType = new DataSet();
            dsextractType = OnlineOrderBackOfficeBo.GetExtractType();

            int i = 0;
            int j = 1;
            foreach (DataRow dr in dsextractType.Tables[0].Rows)
            {
                dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileName"] = j + " - " + dr["WUXFT_XMLFileName"].ToString();

                if (Convert.ToInt32(dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileTypeId"]) == 37)
                    dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileName"] = dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileName"] + "(sbiemf)";
                else if (Convert.ToInt32(dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileTypeId"]) == 38)
                    dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileName"] = dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileName"] + "(sbipay)";
                else if (Convert.ToInt32(dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileTypeId"]) == 39)
                    dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileName"] = dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileName"] + "(HDFCPAY)";
                else if (Convert.ToInt32(dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileTypeId"]) == 40)
                    dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileName"] = dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileName"] + "(eMF-InProcess)";
                else if (Convert.ToInt32(dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileTypeId"]) == 41)
                    dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileName"] = dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileName"] + "(eMF-Executed)";
                else if (Convert.ToInt32(dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileTypeId"]) == 42)
                    dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileName"] = dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileName"] + "(SSL104)";


                i++;
                j++;

            }
            if (dsextractType != null && dsextractType.Tables[0].Rows.Count > 0)
            {
                ddlExtractType.DataSource = dsextractType;
                ddlExtractType.DataValueField = dsextractType.Tables[0].Columns["WUXFT_XMLFileTypeId"].ToString();
                ddlExtractType.DataTextField = dsextractType.Tables[0].Columns["WUXFT_XMLFileName"].ToString();
                ddlExtractType.DataBind();
            }
            ddlExtractType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
            ddlExtractType.SelectedIndex = 0;
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

        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mykey", "hide();", true);
        }

    }
}
