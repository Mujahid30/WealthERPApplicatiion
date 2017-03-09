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
using BoSuperAdmin;

namespace WealthERP.Uploads
{
    public partial class TrailCommisionTransactionRejects : System.Web.UI.UserControl
    {
        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();
        UploadCommonBo uploadsCommonBo;
        AdvisorVo advisorVo;
        CustomerVo customerVo;
        int processId;
        string configPath;
        UploadProcessLogVo uploadProcessLogVo;
        DateTime fromDate;
        DateTime toDate;
        int rejectReasonCode;
        //int processId;
        int adviserId;
        int rmId;
        RMVo rmVo;

        protected void Page_Load(object sender, EventArgs e)
        {
            rmVo = (RMVo)Session[SessionContents.RmVo];
            customerVo = (CustomerVo)Session["customerVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            adviserId = advisorVo.advisorId;
            processId = 0;
            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            if (Request.QueryString["processId"] != null)
                processId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (Request.QueryString["adviserId"] != null)
            {
                adviserId = Int32.Parse(Request.QueryString["adviserId"].ToString());
                if (rmVo == null)
                {
                    DataSet dsAdviserRmDetails = superAdminOpsBo.GetAdviserRmDetails(adviserId);

                    if (dsAdviserRmDetails.Tables[0].Rows.Count > 0)
                    {
                        rmId = int.Parse(dsAdviserRmDetails.Tables[0].Rows[0]["ar_rmid"].ToString());
                        hfRmId.Value = rmId.ToString();
                    }

                }
            }

            if (adviserId == 1000)
            {
                if (ddlAdviser.SelectedValue != "Select" && ddlAdviser.SelectedValue != "")
                {
                    adviserId = Convert.ToInt32(ddlAdviser.SelectedValue.ToString());
                    if (hfRmId.Value != "")
                    {
                        rmId = Convert.ToInt32(hfRmId.Value);
                    }
                    Panel2.Visible = true;

                    Panel2.ScrollBars = ScrollBars.Horizontal;
                    Session["adviserId_Upload"] = adviserId;
                }
                else
                {
                    trReprocess.Visible = false;
                    Panel2.Visible = false;
                    Panel2.ScrollBars = ScrollBars.None;
                    // Panel2.Visible = false;
                    adviserId = 1000;
                    //rmId = rmVo.RMId;
                }
            }
            else
            {
                Session["adviserId_Upload"] = adviserId;

                if (advisorVo.advisorId != 1000)
                {
                    adviserId = advisorVo.advisorId;
                }
                if (rmVo != null)
                    rmId = rmVo.RMId;

                trReprocess.Visible = true;
                Panel2.Visible = true;
                Panel2.ScrollBars = ScrollBars.Horizontal;
                trAdviserSelection.Visible = false;
            }


            if (!IsPostBack)
            {
                Panel2.Visible = false;
                trReprocess.Visible = false;
                txtFromMFT.SelectedDate = DateTime.Now.AddMonths(-1).Date;
                txtToMFT.SelectedDate = DateTime.Now;
                if (adviserId != 1000)
                {
                    BindddlRejectReason();
                    trAdviserSelection.Visible = false;
                    if (processId != 0)
                    {
                        divConditional.Visible = false;
                        BindTrailCommissionRejectedGrid(processId);
                    }
                    else
                    {
                        divConditional.Visible = true;
                    }
                }
                else
                {
                    BindddlRejectReason();
                    BindAdviserDropDownList();
                    trAdviserSelection.Visible = true;
                    if (processId != 0)
                    {
                        divConditional.Visible = false;
                        BindTrailCommissionRejectedGrid(processId);

                        GVTrailTransactionRejects.Visible = true;
                        Panel2.Visible = true;
                    }
                    else
                    {

                        imgBtnrgHoldings.Visible = false;
                        GVTrailTransactionRejects.Visible = false;
                        Panel2.Visible = false;
                    }
                }
                //imgBtnrgHoldings.Visible = false;
                //Msgerror.Visible = false;
            }
        }

        protected void BindAdviserDropDownList()
        {
            DataTable dtAdviserList = new DataTable();
            dtAdviserList = superAdminOpsBo.BindAdviserForUpload();

            if (dtAdviserList.Rows.Count > 0)
            {
                ddlAdviser.DataSource = dtAdviserList;
                ddlAdviser.DataTextField = "A_OrgName";
                ddlAdviser.DataValueField = "A_AdviserId";
                ddlAdviser.DataBind();
            }
            ddlAdviser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }

        public void BindddlRejectReason()
        {
            Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
            uploadProcessLogVo = new UploadProcessLogVo();
            uploadsCommonBo = new UploadCommonBo();
            DataSet ds = uploadsCommonBo.GetRejectReasonTrailList(2);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    genDictIsRejected.Add(dr["WRR_RejectReasonDescription"].ToString(), dr["WRR_RejectReasonCode"].ToString());
                }

                if (ddlRejectReason != null)
                {
                    ddlRejectReason.DataSource = genDictIsRejected;
                    ddlRejectReason.DataTextField = "Key";
                    ddlRejectReason.DataValueField = "Value";
                    ddlRejectReason.DataBind();
                }
            }

            ddlRejectReason.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
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
                trReprocess.Visible = true;
                if (processId != 0)
                {
                    if (adviserId != 1000)
                        dsRejectedSIP = uploadsCommonBo.GetTrailCommissionRejectRejectDetails(adviserId, processId, fromDate, toDate, rejectReasonCode);
                    else
                        dsRejectedSIP = uploadsCommonBo.GetTrailCommissionRejectRejectDetails(adviserId, processId, fromDate, toDate, rejectReasonCode);
                }
                else
                {
                    rejectReasonCode = int.Parse(ddlRejectReason.SelectedValue);
                    fromDate = DateTime.Parse(txtFromMFT.SelectedDate.ToString());
                    toDate = DateTime.Parse(txtToMFT.SelectedDate.ToString());
                    if (adviserId != 1000)
                        dsRejectedSIP = uploadsCommonBo.GetTrailCommissionRejectRejectDetails(adviserId, processId, fromDate, toDate, rejectReasonCode);
                    else
                    {
                        if (Request.QueryString["processId"] != null)
                            dsRejectedSIP = uploadsCommonBo.GetTrailCommissionRejectRejectDetails(adviserId, processId, fromDate, toDate, rejectReasonCode);

                        else
                            dsRejectedSIP = uploadsCommonBo.GetTrailCommissionRejectRejectDetails(Convert.ToInt32(ddlAdviser.SelectedValue), processId, fromDate, toDate, rejectReasonCode);
                    }

                }
                if (dsRejectedSIP.Tables[0].Rows.Count > 0)
                {
                    if (Cache["TrailCommissionRejectDetails" + adviserId.ToString()] == null)
                    {
                        Cache.Insert("TrailCommissionRejectDetails" + adviserId.ToString(), dsRejectedSIP);
                    }
                    else
                    {
                        Cache.Remove("TrailCommissionRejectDetails" + adviserId.ToString());
                        Cache.Insert("TrailCommissionRejectDetails" + adviserId.ToString(), dsRejectedSIP);
                    }
                    GVTrailTransactionRejects.CurrentPageIndex = 0;
                    GVTrailTransactionRejects.DataSource = dsRejectedSIP;
                    GVTrailTransactionRejects.DataBind();
                    trReprocess.Visible = true;
                    GVTrailTransactionRejects.Visible = true;
                    Panel2.Visible = true;
                    imgBtnrgHoldings.Visible = true;
                    Msgerror.Visible = false;
                }
                else
                {
                    GVTrailTransactionRejects.Visible = false;
                    Panel2.Visible = false;
                    imgBtnrgHoldings.Visible = false;
                    Msgerror.Visible = true;
                    trReprocess.Visible = false;
                }
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
                NeedSource();
                GVTrailTransactionRejects.MasterTableView.Rebind();
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
        protected void btnViewTrail_Click(object sender, EventArgs e)
        {
            if (adviserId == 1000)
            {
                if (ddlAdviser.SelectedIndex == 0)
                {
                    Response.Write("<script>alert('Please select an adviser');</script>");
                }

                else
                {
                    BindTrailCommissionRejectedGrid(processId);
                    ViewState.Remove("RejectReasonCode");
                    msgDelete.Visible = false;
                    msgReprocessincomplete.Visible = false;
                }
            }
            else
            {
                BindTrailCommissionRejectedGrid(processId);
                ViewState.Remove("RejectReasonCode");
                msgDelete.Visible = false;
                msgReprocessincomplete.Visible = false;
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

                string StagingID = string.Empty;

                foreach (GridDataItem gvr in this.GVTrailTransactionRejects.Items)
                {
                    if (((CheckBox)gvr.FindControl("ChkOne")).Checked == true)
                    {
                        StagingID += Convert.ToString(GVTrailTransactionRejects.MasterTableView.DataKeyValues[gvr.ItemIndex]["Adul_ProcessId"]) + "~";

                    }
                }

                string[] a = StagingID.Split('~');


                foreach (string word in a)
                //foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (word != "")
                    {
                        //processIdReprocessAll = int.Parse(dr["WUPL_ProcessId"].ToString());
                        processIdReprocessAll = Convert.ToInt32(word);
                        processlogVo = uploadsCommonBo.GetProcessLogInfo(processIdReprocessAll);

                        blResult = MFTrailTransactionInsertion(processIdReprocessAll);

                        if (blResult == false)
                        {
                            error = error + "Error when reprocessing for the processid:" + processIdReprocessAll + ";";
                        }
                    }
                }
            }

            //    DataSet ds = uploadsCommonBo.GetSIPUploadRejectDistinctProcessIdForAdviser(advisorVo.advisorId);
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        processIdReprocessAll = int.Parse(dr["WUPL_ProcessId"].ToString());
            //        processlogVo = uploadsCommonBo.GetProcessLogInfo(processIdReprocessAll);

            //        blResult = MFTrailTransactionInsertion(processIdReprocessAll);

            //        if (blResult == false)
            //        {
            //            error = error + "Error when reprocessing for the processid:" + processIdReprocessAll + ";";
            //        }
            //    }

            //}

            if (error == "")
            {
                // Success Message
                //trErrorMessage.Visible = true;
                //lblError.Text = "Reprocess Done Successfully!";
                BindTrailCommissionRejectedGrid(processId);
                NeedSource();
                GVTrailTransactionRejects.MasterTableView.Rebind();
                msgReprocessComplete.Visible = true;

            }
            else
            {
                // Failure Message
                trErrorMessage.Visible = true;
                msgReprocessincomplete.Visible = true;
                lblError.Text = "ErrorStatus:" + error;
            }

            // BindTrailCommissionRejectedGrid(processId);
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

            TempletonTrailCommonStagingChk = uploadsCommonBo.TrailCommissionCommonStagingCheck(adviserId, UploadProcessId, packagePath, configPath);
            uploadProcessLogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(UploadProcessId, "TN");

            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(uploadProcessLogVo);
            if (TempletonTrailCommonStagingChk)
            {
                packagePath = Server.MapPath("\\UploadPackages\\TrailCommisionUploadPackage\\TrailCommissionUpload\\TrailCommissionUpload\\commonStagingToTrailSetUp.dtsx");
                //TempletonTrailCommonStagingToSetUp = camsUploadsBo.CamsSIPCommonStagingToWERP(UploadProcessId, packagePath, configPath);
                TempletonTrailCommonStagingToSetUp = camsUploadsBo.TempletonTrailCommissionCommonStagingChk(UploadProcessId, packagePath, configPath, "TN",adviserId);


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

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
        }
        protected void GVTrailTransactionRejects_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && e.Item.ItemIndex == -1)
            {
                DataSet dsRejectedSIP = new DataSet();
                DataTable dtRejectedSIP = new DataTable();
                GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                RadComboBox RadComboBoxRR = (RadComboBox)filterItem.FindControl("RadComboBoxRR");
                dsRejectedSIP = (DataSet)Cache["TrailCommissionRejectDetails" + adviserId.ToString()];
                dtRejectedSIP = dsRejectedSIP.Tables[0];
                DataTable dtTSIP = new DataTable();
                dtTSIP.Columns.Add("RejectReasonCode");
                dtTSIP.Columns.Add("WRR_RejectReasonDescription");
                DataRow drTSIP;
                foreach (DataRow dr in dtRejectedSIP.Rows)
                {
                    drTSIP = dtTSIP.NewRow();
                    drTSIP["RejectReasonCode"] = dr["RejectReasonCode"].ToString();
                    drTSIP["WRR_RejectReasonDescription"] = dr["WRR_RejectReasonDescription"].ToString();
                    dtTSIP.Rows.Add(drTSIP);
                }
                //combo.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("ALL", "0"));
                DataView view = new DataView(dtRejectedSIP);
                DataTable distinctValues = view.ToTable(true, "WRR_RejectReasonDescription", "RejectReasonCode");
                RadComboBoxRR.DataSource = distinctValues;
                RadComboBoxRR.DataValueField = dtTSIP.Columns["RejectReasonCode"].ToString();
                RadComboBoxRR.DataTextField = dtTSIP.Columns["WRR_RejectReasonDescription"].ToString();
                RadComboBoxRR.DataBind();

            }

        }
        protected void RadComboBoxRR_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox dropdown = o as RadComboBox;
            ViewState["RejectReasonCode"] = dropdown.SelectedValue.ToString();
            if (ViewState["RejectReasonCode"] != "")
            {
                GridColumn column = GVTrailTransactionRejects.MasterTableView.GetColumnSafe("RejectReasonCode");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                GVTrailTransactionRejects.MasterTableView.Rebind();
            }
            else
            {
                GridColumn column = GVTrailTransactionRejects.MasterTableView.GetColumnSafe("RejectReasonCode");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                GVTrailTransactionRejects.MasterTableView.Rebind();
            }
        }
        protected void rcbContinents1_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            ////persist the combo selected value  
            if (ViewState["RejectReasonCode"] != null)
            {
                Combo.SelectedValue = ViewState["RejectReasonCode"].ToString();
            }
        }
        protected void GVTrailTransactionRejects_PreRender(object sender, EventArgs e)
        {
            if (GVTrailTransactionRejects.MasterTableView.FilterExpression != string.Empty)
            {
                RefreshCombos();
            }
        }
        protected void RefreshCombos()
        {
            DataSet dsRejectedSIP = new DataSet();
            DataTable dtRejectedSIP = new DataTable();
            try
            {
                dsRejectedSIP = (DataSet)Cache["TrailCommissionRejectDetails" + adviserId.ToString()];
                dtRejectedSIP = dsRejectedSIP.Tables[0];
                DataView view = new DataView(dtRejectedSIP);
                DataTable distinctValues = view.ToTable();
                DataRow[] rows = distinctValues.Select(GVTrailTransactionRejects.MasterTableView.FilterExpression.ToString());
            }
            catch (Exception ex)
            {
            }
                finally{
            GVTrailTransactionRejects.MasterTableView.Rebind();
            }
        }
        protected void NeedSource()
        {

            string rcbType = string.Empty;
            DataSet dsRejectedSIP = new DataSet();
            DataTable dtrr = new DataTable();
            dsRejectedSIP = (DataSet)Cache["TrailCommissionRejectDetails" + adviserId.ToString()];
            if (dsRejectedSIP != null)
            {
                dtrr = dsRejectedSIP.Tables[0];
                if (ViewState["RejectReasonCode"] != null)
                    rcbType = ViewState["RejectReasonCode"].ToString();
                if (!string.IsNullOrEmpty(rcbType))
                {
                    DataView dvStaffList = new DataView(dtrr, "RejectReasonCode = '" + rcbType + "'", "Adul_ProcessId,ADUL_FileName,CMFA_FolioNum,PASP_SchemePlanName,CMFTTCS_TransactionType", DataViewRowState.CurrentRows);
                    GVTrailTransactionRejects.DataSource = dvStaffList.ToTable();
                }
                else
                {
                    GVTrailTransactionRejects.DataSource = dtrr;

                }
            }


        }


        protected void GVTrailTransactionRejects_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string rcbType = string.Empty;
            Msgerror.Visible = false;
            imgBtnrgHoldings.Visible = true;
            DataSet dsRejectedSIP = new DataSet();
            DataTable dtrr = new DataTable();
            dsRejectedSIP = (DataSet)Cache["TrailCommissionRejectDetails" + adviserId.ToString()];
            if (dsRejectedSIP != null)
            {
                dtrr = dsRejectedSIP.Tables[0];
                if (ViewState["RejectReasonCode"] != null)
                    rcbType = ViewState["RejectReasonCode"].ToString();
                if (!string.IsNullOrEmpty(rcbType))
                {
                    DataView dvStaffList = new DataView(dtrr, "RejectReasonCode = '" + rcbType + "'", "Adul_ProcessId,ADUL_FileName,CMFA_FolioNum,PASP_SchemePlanName,CMFTTCS_TransactionType", DataViewRowState.CurrentRows);
                    GVTrailTransactionRejects.DataSource = dvStaffList.ToTable();

                }
                else
                {
                    GVTrailTransactionRejects.DataSource = dtrr;
                    imgBtnrgHoldings.Visible = true;

                }
            }
        }
        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            DataSet dsRejectedSIP = new DataSet();

            dsRejectedSIP = (DataSet)Cache["TrailCommissionRejectDetails" + adviserId.ToString()];
        

            GVTrailTransactionRejects.ExportSettings.OpenInNewWindow = true;
            GVTrailTransactionRejects.ExportSettings.IgnorePaging = true;
            GVTrailTransactionRejects.ExportSettings.ExportOnlyData = true;
            GVTrailTransactionRejects.ExportSettings.FileName = "View Trail Exceptions Details";
            GVTrailTransactionRejects.ExportSettings.HideStructureColumns = true;
            GVTrailTransactionRejects.ExportSettings.ExportOnlyData = true;
            GVTrailTransactionRejects.ExportSettings.FileName = "View Transactions Details";
            GVTrailTransactionRejects.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            GVTrailTransactionRejects.MasterTableView.ExportToExcel();
        }


        protected void ddlAdviser_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Panel2.Visible = false;
            trReprocess.Visible = false;
            if (ddlAdviser.SelectedValue != "Select" && ddlAdviser.SelectedValue != "")
            {
                Session["adviserId_Upload"] = ddlAdviser.SelectedValue;
            }
        }
    }
}
