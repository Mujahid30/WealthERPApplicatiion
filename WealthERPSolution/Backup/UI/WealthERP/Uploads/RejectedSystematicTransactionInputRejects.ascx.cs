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
using WealthERP.Base;
using System.Configuration;
using BoCommon;
using Telerik.Web.UI;

namespace WealthERP.Uploads
{
    public partial class RejectedSystematicTransactionInputRejects : System.Web.UI.UserControl
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
                processID = Int32.Parse(Request.QueryString["processId"].ToString());
                BindSIPCAMSInputRejectedRecordsGrid();
            }
        }
        public void BindSIPCAMSInputRejectedRecordsGrid()
        {
            DataSet dsRejectedRecords;          
            try
            {
                dsRejectedRecords = rejectedRecordsBo.GetSIPCAMSInputRejectedRecords(processID);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RejectedSystematicTransactionInputRejects.ascx:BindSIPCAMSInputRejectedRecordsGrid()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }           
            if (dsRejectedRecords.Tables[0].Rows.Count > 0)
            {
                gvSIPInputRejectDetails.DataSource = dsRejectedRecords.Tables[0];
                gvSIPInputRejectDetails.DataBind();

                if (Cache["SIPInputRejectDetails" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("SIPInputRejectDetails" + userVo.UserId.ToString(), dsRejectedRecords);
                }
                else
                {
                    Cache.Remove("SIPInputRejectDetails" + userVo.UserId.ToString());
                    Cache.Insert("SIPInputRejectDetails" + userVo.UserId.ToString(), dsRejectedRecords);
                }
            }
            else
            {
                gvSIPInputRejectDetails.DataSource = null;
                gvSIPInputRejectDetails.DataBind();
            }
        }

        protected void gvSIPInputRejectDetails_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dtGvSIPInputRejectDetails = new DataSet();
            dtGvSIPInputRejectDetails = (DataSet)Cache["SIPInputRejectDetails" + userVo.UserId.ToString()];
            gvSIPInputRejectDetails.DataSource = dtGvSIPInputRejectDetails;
        }
        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            DataSet dtGvSIPInputRejectDetails = new DataSet();
            dtGvSIPInputRejectDetails = (DataSet)Cache["SIPInputRejectDetails" + userVo.UserId.ToString()];
            gvSIPInputRejectDetails.DataSource = dtGvSIPInputRejectDetails;

            gvSIPInputRejectDetails.ExportSettings.OpenInNewWindow = true;
            gvSIPInputRejectDetails.ExportSettings.IgnorePaging = true;
            gvSIPInputRejectDetails.ExportSettings.HideStructureColumns = true;
            gvSIPInputRejectDetails.ExportSettings.ExportOnlyData = true;
            gvSIPInputRejectDetails.ExportSettings.FileName = "SIP Input Reject Details";
            gvSIPInputRejectDetails.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvSIPInputRejectDetails.MasterTableView.ExportToExcel();
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
        }
    }
}