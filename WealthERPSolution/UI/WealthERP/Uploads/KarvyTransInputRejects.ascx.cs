using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WealthERP.Customer;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoUser;
using VoUploads;
using BoUploads;
using BoCommon;
using WealthERP.Base;
using System.Configuration;
using Telerik.Web.UI;

namespace WealthERP.Uploads
{
    public partial class KarvyTransInputRejects : System.Web.UI.UserControl
    {
        UserVo userVo;
        RejectedRecordsBo rejectedRecordsBo = new RejectedRecordsBo();
        

        int processID;

       
        protected void Page_Load(object sender, EventArgs e)
        {
            userVo = (UserVo)Session[SessionContents.UserVo];

            SessionBo.CheckSession();

            if (!IsPostBack)
            {
                if (Request.QueryString["processId"]!=null)
                processID = Int32.Parse(Request.QueryString["processId"].ToString());
                
                BindRejectedRecordsGrid();
            }
        }

        void BindRejectedRecordsGrid()
        {
            DataSet dsRejectedRecords;
          
            try
            {
                dsRejectedRecords = rejectedRecordsBo.GetTransInputRejects(processID, "KA");


                if (Cache["gvRejectedRecords" + userVo.UserId] == null)
                {
                    Cache.Insert("gvRejectedRecords" + userVo.UserId, dsRejectedRecords.Tables[0]);
                }
                else
                {
                    Cache.Remove("gvRejectedRecords" + userVo.UserId);
                    Cache.Insert("gvRejectedRecords" + userVo.UserId, dsRejectedRecords.Tables[0]);
                }
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "KarvyTransInputRejects.ascx:BindRejectedRecordsGrid()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            if (dsRejectedRecords.Tables[0].Rows.Count > 0)
            {
                imgBtnrgHoldings.Visible = true;
                 gvRejectedRecords.DataSource = dsRejectedRecords.Tables[0];
                gvRejectedRecords.DataBind();
            }
            else
            {
                imgBtnrgHoldings.Visible = false;
                gvRejectedRecords.DataSource = null;
                gvRejectedRecords.DataBind();
            }
            divGvRejectedRecords.Visible = true;

        }
        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
        }
        protected void gvRejectedRecords_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtgvTransaction = new DataTable();

            dtgvTransaction = (DataTable)Cache["gvRejectedRecords" + userVo.UserId];
            gvRejectedRecords.DataSource = dtgvTransaction;
            gvRejectedRecords.Visible = true;


        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvRejectedRecords.ExportSettings.OpenInNewWindow = true;
            gvRejectedRecords.ExportSettings.IgnorePaging = true;
            gvRejectedRecords.ExportSettings.HideStructureColumns = true;
            gvRejectedRecords.ExportSettings.ExportOnlyData = true;
            gvRejectedRecords.ExportSettings.FileName = "Transaction Detail";
            gvRejectedRecords.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvRejectedRecords.MasterTableView.ExportToExcel();
        }
    }
}