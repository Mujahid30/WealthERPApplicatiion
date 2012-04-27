using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using VoUploads;
using VoCustomerProfiling;
using VoUser;
using BoUploads;
using BoCustomerProfiling;
using BoUser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Collections;
using WealthERP.Base;
using BoCommon;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Globalization;

namespace WealthERP.Uploads
{
    public partial class TrailCommisionTransactionRejects : System.Web.UI.UserControl
    {
        UploadCommonBo uploadsCommonBo;
        AdvisorVo advisorVo;
        CustomerVo customerVo;
        int processId;
        string configPath;
        UploadProcessLogVo uploadProcessLogVo;

        protected void Page_Load(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            processId = 0;
            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            if (Request.QueryString["processId"] != null)
                processId = Int32.Parse(Request.QueryString["processId"].ToString());
            if (!Page.IsPostBack)
            {
                BindTrailCommissionRejectedGrid(processId);

            }
        }

        protected void ToggleRowSelection(object sender, EventArgs e)
        {
            ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
        }


        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            CheckBox headerCheckBox = (sender as CheckBox);
            foreach (GridDataItem dataItem in GVTrailTransactionRejects.MasterTableView.Items)
            {
                (dataItem.FindControl("ChkOne") as CheckBox).Checked = headerCheckBox.Checked;
            }
        }

        public void BindTrailCommissionRejectedGrid(int processId)
        {
            try
            {
                uploadsCommonBo = new UploadCommonBo();
                DataSet dsRejectedSIP = new DataSet();
                dsRejectedSIP = uploadsCommonBo.GetTrailCommissionRejectRejectDetails(advisorVo.advisorId, processId);
                GVTrailTransactionRejects.DataSource = dsRejectedSIP;
                GVTrailTransactionRejects.DataBind();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int i = 0;


            foreach (GridDataItem gvr in this.GVTrailTransactionRejects.Items)
            {
                if (((CheckBox)gvr.FindControl("ChkOne")).Checked == true)
                {
                    i = i + 1;
                }
            }

            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select record to delete!');", true);
                msgDelete.Visible = false;
            }
            else
            {
                msgDelete.Visible = true;
                CustomerTrailCommissionTransactionDelete();
                msgDelete.Visible = true;
            }

        }

        private void CustomerTrailCommissionTransactionDelete()
        {
            UploadCommonBo uploadsCommonBo = new UploadCommonBo();
            foreach (GridDataItem gvr in this.GVTrailTransactionRejects.Items)
            {
                if (((CheckBox)gvr.FindControl("ChkOne")).Checked == true)
                {
                    int selectedRow = gvr.ItemIndex + 1;
                    int StagingID = int.Parse((GVTrailTransactionRejects.MasterTableView.DataKeyValues[selectedRow - 1]["CMFTCCS_Id"].ToString()));
                    uploadsCommonBo.DeleteMFTrailTransactionStaging(StagingID);
                    BindTrailCommissionRejectedGrid(processId);
                }
            }

        }

        protected void btnReprocess_Click(object sender, EventArgs e)
        {
            bool blResult = false;
            uploadsCommonBo = new UploadCommonBo();
            UploadProcessLogVo processlogVo = new UploadProcessLogVo();
            string error = "";
            int processIdReprocessAll = 0;


            if (Request.QueryString["processId"] != null)
            {
                processId = Int32.Parse(Request.QueryString["processId"].ToString());
                processlogVo = uploadsCommonBo.GetProcessLogInfo(processId);
                blResult = MFTrailTransactionInsertion(processId);

            }

            else
            {
                DataSet ds = uploadsCommonBo.GetSIPUploadRejectDistinctProcessIdForAdviser(advisorVo.advisorId);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    processIdReprocessAll = int.Parse(dr["WUPL_ProcessId"].ToString());
                    processlogVo = uploadsCommonBo.GetProcessLogInfo(processIdReprocessAll);

                    blResult = MFTrailTransactionInsertion(processIdReprocessAll);

                    if (blResult == false)
                    {
                        error = error + "Error when reprocessing for the processid:" + processIdReprocessAll + ";";
                    }
                }

            }

            if (error == "")
            {
                // Success Message
                //trErrorMessage.Visible = true;
                //lblError.Text = "Reprocess Done Successfully!";
                msgReprocessComplete.Visible = true;

            }
            else
            {
                // Failure Message
                trErrorMessage.Visible = true;
                msgReprocessincomplete.Visible = true;
                lblError.Text = "ErrorStatus:" + error;
            }

            BindTrailCommissionRejectedGrid(processId);
        }


        public bool MFTrailTransactionInsertion(int UploadProcessId)
        {
            bool blResult = false;
            uploadProcessLogVo = new UploadProcessLogVo();
            uploadsCommonBo = new UploadCommonBo();
            CamsUploadsBo camsUploadsBo = new CamsUploadsBo();
            bool TempletonTrailCommonStagingChk = false;
            bool TempletonTrailCommonStagingToSetUp = false;
            bool updateProcessLog = false;
            string packagePath;


            uploadProcessLogVo = uploadsCommonBo.GetProcessLogInfo(UploadProcessId);

            packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\callOnReprocess.dtsx");

            TempletonTrailCommonStagingChk = uploadsCommonBo.TrailCommissionCommonStagingCheck(advisorVo.advisorId, UploadProcessId, packagePath, configPath);
            uploadProcessLogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(UploadProcessId, "TN");

            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(uploadProcessLogVo);
            if (TempletonTrailCommonStagingChk)
            {
                packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\commonStagingToTrailSetUp.dtsx");
                //TempletonTrailCommonStagingToSetUp = camsUploadsBo.CamsSIPCommonStagingToWERP(UploadProcessId, packagePath, configPath);
                TempletonTrailCommonStagingToSetUp = camsUploadsBo.TempletonTrailCommissionCommonStagingChk(UploadProcessId, packagePath, configPath, "TN");


                if (TempletonTrailCommonStagingToSetUp)
                {
                    uploadProcessLogVo.IsInsertionToWERPComplete = 1;
                    uploadProcessLogVo.EndTime = DateTime.Now;
                    uploadProcessLogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(UploadProcessId, "TN");
                    blResult = uploadsCommonBo.UpdateUploadProcessLog(uploadProcessLogVo);
                }
            }
            return blResult;
        }

        protected void GVTrailTransactionRejects_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            uploadsCommonBo = new UploadCommonBo();
            DataSet dsRejectedSIP = new DataSet();
            dsRejectedSIP = uploadsCommonBo.GetTrailCommissionRejectRejectDetails(advisorVo.advisorId, processId);
            GVTrailTransactionRejects.DataSource = dsRejectedSIP;
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
        }

    }
}