﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using Telerik.Web.UI;
using System.Data;
using VoUser;
using System.IO;
using BoCommon;



namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineOrderExtract : System.Web.UI.UserControl
    {
        OnlineMFOrderBo boOnlineOrder = new OnlineMFOrderBo();
        OnlineOrderBackOfficeBo boOnlineOrderBackOffice = new OnlineOrderBackOfficeBo();
        AdvisorVo advisorVo;
        CommonLookupBo boCommon = new CommonLookupBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            tblMessage.Visible = false;

            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];

            if (!Page.IsPostBack)
            {
                BindProductAmc();
                BindProduct();
                BindExternalSource();
                BindExtractDate();
            }
        }

        protected void btnGenerateFile_Click(object sender, EventArgs e)
        {
            boOnlineOrderBackOffice.GenerateOrderExtract();
            DateTime execDate = rdpExtractDate.SelectedDate.Value;

            DataTable orderExtractForRta = boOnlineOrderBackOffice.GetOrderExtractForRta(rdpExtractDate.SelectedDate.Value, advisorVo.advisorId, ddlExtractType.SelectedValue, ddlRnT.SelectedValue, int.Parse(ddlProductAmc.SelectedValue));
            DownloadCsvFile(orderExtractForRta);
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            boOnlineOrderBackOffice.GenerateOrderExtract();
            Page.Validate("ExtractData");
            if (!Page.IsValid)
            {
                ShowMessage("Please select required fields");
                return;
            }

            if (BindMisGridView() <= 0)
            {
                ShowMessage("No data available");
                return;
            }
            pnlExtractMIS.Visible = true;
        }
        protected void btnExtract_Click(object sender, EventArgs e)
        {
            Page.Validate("ExtractData");
            Page.Validate("PreviewData");
            if (!Page.IsValid)
            {
                ShowMessage("Please select required fields");
                return;
            }
 
            if (gvExtractMIS.MasterTableView.Items.Count <= 0) return;

            gvExtractMIS.ExportSettings.OpenInNewWindow = true;
            gvExtractMIS.ExportSettings.OpenInNewWindow = true;
            gvExtractMIS.ExportSettings.IgnorePaging = true;
            gvExtractMIS.ExportSettings.HideStructureColumns = true;
            gvExtractMIS.ExportSettings.ExportOnlyData = true;
            gvExtractMIS.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvExtractMIS.MasterTableView.ExportToExcel();
        }

        protected void btnAutoOrder_Click(object sender, EventArgs e)
        {
            boOnlineOrder.TriggerAutoOrderFromSIP();
            ShowMessage("Auto Order SIP triggered successfully");
        }

        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
        }

        protected void gvExtractMIS_ItemDataBound(object sender, GridItemEventArgs e)
        {
        }

        protected void gvExtractMIS_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {     
            DataTable dtOrderMIS = new DataTable();
            dtOrderMIS = (DataTable)Cache["OrderMIS"];

            if (dtOrderMIS.Rows.Count == 0) {
                gvExtractMIS.DataSource = new Object[] { };
                return;
            }  

            gvExtractMIS.DataSource = dtOrderMIS;
            gvExtractMIS.Visible = true;
        }
        
        private int BindMisGridView()
        {
            DataSet dsOrderMis = boOnlineOrderBackOffice.GetMfOrderExtract(rdpExtractDate.SelectedDate.Value, advisorVo.advisorId, ddlExtractType.SelectedValue, "CA", int.Parse(ddlProductAmc.SelectedValue));

            if (dsOrderMis == null) return 0;
            if (dsOrderMis.Tables.Count <= 0) return 0;

            DataTable dtOrderMIS = dsOrderMis.Tables[0];

            int cRows = dtOrderMIS.Rows.Count;
            if (cRows <= 0) return 0;
            
            gvExtractMIS.DataSource = dtOrderMIS;
            gvExtractMIS.DataBind();
            gvExtractMIS.Visible = true;
            Cache.Remove("OrderMIS");
            
            if (Cache["OrderMIS"] == null) Cache.Insert("OrderMIS", dtOrderMIS);

            return cRows;
        }

        void ShowValidationMessage()
        {
            //divValidation.Visible = true;
        }

        public void DownloadCsvFile(DataTable dtOrderExtract)
        {
            if (dtOrderExtract == null) 
            {
                ShowMessage("No data available");
                return; 
            }
            if (dtOrderExtract.Rows.Count <= 0)
            {
                ShowMessage("No data available");
                return;
            }

            string filename = "OrderExtract" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + ".txt";
            string file = string.Empty;

            #region ExportDataTabletoFile
            StreamWriter str = new StreamWriter(Server.MapPath("UploadFiles/" + filename), false, System.Text.Encoding.Default);

            string Columns = string.Empty;
            foreach (DataColumn column in dtOrderExtract.Columns)
            {
                Columns += column.ColumnName + "|";
            }
            str.WriteLine(Columns.Remove(Columns.Length - 1, 1));

            foreach (DataRow datarow in dtOrderExtract.Rows)
            {
                string row = string.Empty;
                foreach (object items in datarow.ItemArray)
                {
                    row += items.ToString() + "|";
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

        protected void BindExternalSource()
        {
            ddlRnT.Items.Clear();
            if (ddlRnT.SelectedIndex == 0) return;

            DataTable dtExtSrc = boCommon.GetExternalSource(null);
            if (dtExtSrc == null) return;

            if (dtExtSrc.Rows.Count > 0)
            {
                ddlRnT.DataSource = dtExtSrc;
                ddlRnT.DataValueField = dtExtSrc.Columns["XES_SourceCode"].ToString();
                ddlRnT.DataTextField = dtExtSrc.Columns["XES_SourceName"].ToString();
                ddlRnT.DataBind();
            }
            ddlRnT.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
            ddlRnT.SelectedIndex = 0;
        }

        protected void BindExtractDate()
        {
            rdpExtractDate.SelectedDate = DateTime.Now;
        }

        protected void BindProduct()
        {
            ddlProduct.Items.Clear();
            if (ddlProduct.SelectedIndex == 0) return;

            DataTable dtProd = boCommon.GetProductList();
            if (dtProd == null) return;

            if (dtProd.Rows.Count > 0)
            {
                ddlProduct.DataSource = dtProd;
                ddlProduct.DataValueField = dtProd.Columns["PAG_AssetGroupCode"].ToString();
                ddlProduct.DataTextField = dtProd.Columns["PAG_AssetGroupName"].ToString();
                ddlProduct.DataBind();
            }
            ddlProduct.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
            ddlProduct.SelectedIndex = 0;
            ddlProduct.SelectedIndex = 14;
            ddlProduct.Enabled = false;
        }

        protected void BindProductAmc()
        {
            ddlProductAmc.Items.Clear();
            if (ddlProductAmc.SelectedIndex == 0) return;

            DataTable dtAmc = boCommon.GetProdAmc(0, true);
            if (dtAmc == null) return;

            if (dtAmc.Rows.Count > 0)
            {
                ddlProductAmc.DataSource = dtAmc;
                ddlProductAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlProductAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlProductAmc.DataBind();
            }
            ddlProductAmc.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
            ddlProductAmc.SelectedIndex = 0;
        }
    }
}