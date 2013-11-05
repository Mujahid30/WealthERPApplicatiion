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

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineOrderAccountingExtract : System.Web.UI.UserControl
    {
        DataSet dsextractType;
        OnlineOrderBackOfficeBo OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
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
            BindddlExtractType();
            if (!IsPostBack)
            {
                txtExtractDate.MaxDate = DateTime.Now;
                txtExtractDate.SelectedDate = DateTime.Now;
            }
        }

        protected void btnExtract_Click(object sender, EventArgs e)
        {
            if (ddlExtractType.SelectedValue != "0")
            {
                CreateFileForextractAndSaveinServer();
                GetExtractTypeDataForFileCreation();
            }
        }

        protected void CreateFileForextractAndSaveinServer()
        {

            SetFileNameAndDelimeter(Convert.ToInt32(ddlExtractType.SelectedValue));
            File.WriteAllText(Path.Combine(ExtractPath, filename), ", System.Text.Encoding.Default");
        }

        protected void GetExtractTypeDataForFileCreation()
        {
            orderDate = Convert.ToDateTime(txtExtractDate.SelectedDate);
            dsExtractTypeDataForFileCreation = OnlineOrderBackOfficeBo.GetExtractTypeDataForFileCreation(orderDate, advisorVo.advisorId,Convert.ToInt32(ddlExtractType.SelectedValue));
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

        protected void SetFileNameAndDelimeter(int FileID)
        {
            string strExtractDate = Convert.ToDateTime(txtExtractDate.SelectedDate).ToShortDateString();
            string[] strSplitExtractDate = strExtractDate.Split('/');

            string DD = strSplitExtractDate[0].ToString();
            string MM = strSplitExtractDate[1].ToString();
            string YYYY = strSplitExtractDate[2].ToString();

            if(FileID==37)
            {
                filename = "sbiemf" + DD + MM + ".txt";
                delimeter = "#";                
            }
            else if(FileID==38)
            {
                filename = "sbipay" + DD + MM + ".txt";
                delimeter = "   ";
            }
            else if(FileID==39)
            {
                filename = "HDFCPAY" + DD + MM + ".txt";
                delimeter = "        ";
            }
            else if(FileID==40)
            {
                filename = "eMF-InProcess" + DD + MM + YYYY + ".txt";
                delimeter = "|";
            }
            else if(FileID==41)
            {
                filename = "eMF-Executed" + DD + MM + YYYY + ".txt";
                delimeter = "|";
            }
            else if (FileID == 42)
            {
                filename = "SSL104" + DD + MM + ".txt";
                delimeter = ",";
            }
        }


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
                        row += items.ToString() + delimeter;
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

        protected void BindddlExtractType()
        {
            dsextractType = new DataSet();
            dsextractType = OnlineOrderBackOfficeBo.GetExtractType();

            int i = 0;
            int j = 1;
            foreach (DataRow dr in dsextractType.Tables[0].Rows)
            {
                dsextractType.Tables[0].Rows[i]["WUXFT_XMLFileName"] = j + " - " + dr["WUXFT_XMLFileName"].ToString();
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
    }
}
