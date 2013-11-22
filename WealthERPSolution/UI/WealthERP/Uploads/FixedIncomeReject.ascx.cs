using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoUploads;
using System.Data;
using WealthERP.Base;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoUploads;
using System.Configuration;
using BoCommon;
using Telerik.Web.UI;

namespace WealthERP.Uploads
{
    public partial class FixedIncomeReject : System.Web.UI.UserControl
    {
        AdvisorVo adviserVo = new AdvisorVo();
        UploadProcessLogVo processlogVo;
        RejectedRecordsBo rejectedRecordsBo;
        UploadCommonBo uploadsCommonBo;
        WerpUploadsBo werpUploadBo;
        RMVo rmVo;
        UserVo userVo;
        DataSet dsRejectedRecords;
        DataTable dtRejectReasonType;
        DateTime fromDate;
        DateTime toDate;
        int ProcessId = 0;
        int rejectReasonCode;
        DataView dvFixedIncomeReject;
        string configPath;
        protected void Page_Load(object sender, EventArgs e)
        {
            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            if (Request.QueryString["processId"] != null)
            {
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
                lnkViewInputRejects.Visible = true;
            }
            else
            {
                lnkViewInputRejects.Visible = false;
            }
            if (Session["userVo"] != null)
            {
            }
            else
            {
                Session.Clear();
                Session.Abandon();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscript", "loadcontrol('SessionExpired','');", true);
            }
            adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo]; 
            if (!IsPostBack)
            {
                DateTime fromDate = DateTime.Now.AddMonths(-1);
                txtFromFI.SelectedDate = fromDate.Date;
                txtToFI.SelectedDate = DateTime.Now;
                if (adviserVo.advisorId != 1000)
                {
                    BindddlRejectReason();
                    if (ProcessId != 0)
                    {
                        divConditional.Visible = false;
                        lnkViewInputRejects.Visible = true;
                        BindFixedIncomeGrid(ProcessId);
                    }
                    else
                    {
                        lnkViewInputRejects.Visible = false;
                        divConditional.Visible = true;
                    }
                }
                else
                {
                    if (ProcessId != 0)
                    {
                        divConditional.Visible = false;
                    }
                    else
                    {
                        tdBtnViewRejetcs.Visible = false;
                        tdTxtToDate.Visible = false;
                        tdlblToDate.Visible = false;
                        tdTxtFromDate.Visible = false;
                        tdlblFromDate.Visible = false;
                        tdlblRejectReason.Visible = false;
                        tdDDLRejectReason.Visible = false;
                        tdTxtFromDate.Visible = false;
                    }
                }
            }
        }
        public void BindddlRejectReason()
        {
            Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
            processlogVo = new UploadProcessLogVo();
            rejectedRecordsBo = new RejectedRecordsBo();
            DataSet ds = rejectedRecordsBo.GetRejectReasonMFList(1);
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
            ddlRejectReason.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void BindFixedIncomeGrid(int ProcessId)
        {
            if (ProcessId == null || ProcessId == 0)
            {
                if (txtFromFI.SelectedDate != null)
                    fromDate = DateTime.Parse(txtFromFI.SelectedDate.ToString());
                if (txtToFI.SelectedDate != null)
                    toDate = DateTime.Parse(txtToFI.SelectedDate.ToString());
                rejectReasonCode = int.Parse(ddlRejectReason.SelectedValue);
            }
            //Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
            //Dictionary<string, string> genDictRejectReason = new Dictionary<string, string>();
            rejectedRecordsBo = new RejectedRecordsBo();
            dsRejectedRecords = rejectedRecordsBo.GetRejectedFixedIncomeStaging(adviserVo.advisorId, ProcessId, fromDate, toDate, rejectReasonCode);
            if (dsRejectedRecords.Tables[0].Rows.Count > 0)
            {
                //trMessage.Visible = false;
                trReprocess.Visible = true;
                DivAction.Visible = true;
                msgDelete.Visible = false;

                if (Cache["RejectedFixedIncomeDetails" + adviserVo.advisorId.ToString()] == null)
                {
                    Cache.Insert("RejectedFixedIncomeDetails" + adviserVo.advisorId.ToString(), dsRejectedRecords);
                }
                else
                {
                    Cache.Remove("RejectedFixedIncomeDetails" + adviserVo.advisorId.ToString());
                    Cache.Insert("RejectedFixedIncomeDetails" + adviserVo.advisorId.ToString(), dsRejectedRecords);
                }
                gvWERPFI.CurrentPageIndex = 0;
                gvWERPFI.DataSource = dsRejectedRecords.Tables[0];
                gvWERPFI.DataBind();
                gvWERPFI.Visible = true;
                Panel2.Visible = true;
                msgDelete.Visible = false;
                btnExport.Visible = true;
            }
            else
            {
                gvWERPFI.DataSource = null;
                gvWERPFI.DataBind();
                Msgerror.Visible = true;
                //trMessage.Visible = true;
                DivAction.Visible = false;
                gvWERPFI.Visible = false;
                Panel2.Visible = false;
                trReprocess.Visible = false;
                btnExport.Visible = false;
            }
        }
        protected void btnViewFI_Click(object sender, EventArgs e)
        {
            if (txtFromFI.SelectedDate != null)
                fromDate = DateTime.Parse(txtFromFI.SelectedDate.ToString());
            if (txtToFI.SelectedDate != null)
                toDate = DateTime.Parse(txtToFI.SelectedDate.ToString());
            BindFixedIncomeGrid(ProcessId);
            msgDelete.Visible = false;
            msgReprocessincomplete.Visible = false;
            msgReprocessComplete.Visible = false;
            ViewState.Remove("RejectReasonCode");

        }
        protected void gvWERPFI_PreRender(object sender, EventArgs e)
        {
            if (gvWERPFI.MasterTableView.FilterExpression != string.Empty)
            {
                RefreshCombos();
            }
        }
        protected void RefreshCombos()
        {
            dsRejectedRecords = (DataSet)Cache["RejectedEquityDetails" + adviserVo.advisorId.ToString()];
            dtRejectReasonType = dsRejectedRecords.Tables[0];
            DataView view = new DataView(dtRejectReasonType);
            DataTable distinctValues = view.ToTable();
            DataRow[] rows = distinctValues.Select(gvWERPFI.MasterTableView.FilterExpression.ToString());
            gvWERPFI.MasterTableView.Rebind();
        }
        protected void gvWERPFI_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && e.Item.ItemIndex == -1)
            {
                DataTable dtgvWERPFI = new DataTable();
                GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                RadComboBox RadComboBoxRR = (RadComboBox)filterItem.FindControl("RadComboBoxRR");
                dsRejectedRecords = (DataSet)Cache["RejectedFixedIncomeDetails" + adviserVo.advisorId.ToString()];
                DataTable dtcustMIS = new DataTable();
                dtgvWERPFI = dsRejectedRecords.Tables[0];
                dtcustMIS.Columns.Add("WRR_RejectReasonDescription");
                dtcustMIS.Columns.Add("RejectReasonCode");
                DataRow drcustMIS;
                foreach (DataRow dr in dtgvWERPFI.Rows)
                {
                    drcustMIS = dtcustMIS.NewRow();
                    drcustMIS["WRR_RejectReasonDescription"] = dr["WRR_RejectReasonDescription"].ToString();
                    drcustMIS["RejectReasonCode"] = dr["RejectReasonCode"].ToString();
                    dtcustMIS.Rows.Add(drcustMIS);
                }
                DataView view = new DataView(dtgvWERPFI);
                DataTable distinctValues = view.ToTable(true, "WRR_RejectReasonDescription", "RejectReasonCode");
                RadComboBoxRR.DataSource = distinctValues;
                RadComboBoxRR.DataValueField = dtcustMIS.Columns["RejectReasonCode"].ToString();
                RadComboBoxRR.DataTextField = dtcustMIS.Columns["WRR_RejectReasonDescription"].ToString();
                RadComboBoxRR.DataBind();
            }
        }
        protected void NeedSource()
        {
            string rcbType = string.Empty;
            string tttype = string.Empty;
            DataSet dsFixedIncome = new DataSet();
            DataTable dtFixedIncome = new DataTable();
            dsFixedIncome = (DataSet)Cache["RejectedEquityDetails" + adviserVo.advisorId.ToString()];
            if (dsFixedIncome != null)
            {
                dtFixedIncome = dsFixedIncome.Tables[0];
                if (ViewState["RejectReasonCode"] != null)
                    rcbType = ViewState["RejectReasonCode"].ToString();

                if ((!string.IsNullOrEmpty(rcbType)))
                {
                    dvFixedIncomeReject = new DataView(dtFixedIncome, "RejectReasonCode = '" + rcbType + "'", "ADUL_ProcessId,CustomerName,CUS_PANno,CUS_SubBrokercode,CUS_BankSrNo,WUXFT_XMLFileName,Amount", DataViewRowState.CurrentRows);
                    gvWERPFI.DataSource = dvFixedIncomeReject.ToTable();
                }
                else
                {
                    gvWERPFI.DataSource = dtFixedIncome;
                    //  btnExport.Visible = true;
                }
            }
        }
        protected void gvWERPFI_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string rcbType = string.Empty;
            string pitype = string.Empty;
            string tttype = string.Empty;
            btnExport.Visible = true;
            DataSet dsFixedIncome = new DataSet();
            DataTable dtFixedIncome = new DataTable();
            dsFixedIncome = (DataSet)Cache["RejectedFixedIncomeDetails" + adviserVo.advisorId.ToString()];
            if (dsFixedIncome != null)
            {
                dtFixedIncome = dsFixedIncome.Tables[0];
                if (ViewState["RejectReasonCode"] != null)
                    rcbType = ViewState["RejectReasonCode"].ToString();
                if ((!string.IsNullOrEmpty(rcbType)))
                {
                    dvFixedIncomeReject = new DataView(dtFixedIncome, "RejectReasonCode = '" + rcbType + "'", "ADUL_ProcessId,CustomerName,CUS_PANno,CUS_SubBrokercode,CUS_BankSrNo,WUXFT_XMLFileName,Amount", DataViewRowState.CurrentRows);
                    gvWERPFI.DataSource = dvFixedIncomeReject.ToTable();
                }
                else
                {
                    gvWERPFI.DataSource = dtFixedIncome;
                    btnExport.Visible = true;
                }
            }
        }
        //protected void btnReprocess_Click(object sender, EventArgs e)
        //{
            //bool blResult = false;
            //uploadsCommonBo = new UploadCommonBo();

            //int countTransactionsInserted = 0;
            //int countRejectedRecords = 0;
            //string error = "";
            //int processIdReprocessAll = 0;

            //// BindGrid
            //if (Request.QueryString["processId"] != null)
            //{
            //    ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            //    blResult = MFWERPTransactionWERPInsertion(ProcessId, out countTransactionsInserted, out countRejectedRecords);
            //}
            //else
            //{
            //    #region removed coz its taking all the processids
            //    //DataSet ds = uploadsCommonBo.GetEquityTransactionStagingProcessId(adviserVo.advisorId);
            //    #endregion

            //    string StagingID = string.Empty;

            //    foreach (GridDataItem gvr in this.gvWERPFI.Items)
            //    {
            //        if (((CheckBox)gvr.FindControl("chkBxWPTrans")).Checked == true)
            //        {
            //            StagingID += Convert.ToString(gvWERPFI.MasterTableView.DataKeyValues[gvr.ItemIndex]["ProcessId"]) + "~";

            //        }
            //    }

            //    string[] a = StagingID.Split('~');


            //    foreach (string word in a)


            //    //foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (word != "")
            //        {
            //            //processIdReprocessAll = int.Parse(dr["ProcessId"].ToString());
            //            processIdReprocessAll = int.Parse(word);
            //            blResult = MFWERPTransactionWERPInsertion(processIdReprocessAll, out countTransactionsInserted, out countRejectedRecords);
            //        }
            //    }
            //}
            //if (blResult == false)
            //{
            //    error = error + "Error when reprocessing for the processid:" + processIdReprocessAll + ";";
            //}

            //if (blResult == true)
            //{

            //    NeedSource();
            //    gvWERPFI.MasterTableView.Rebind();
            //    msgReprocessComplete.Visible = true;
            //    msgDelete.Visible = false;


            //}
            //else
            //{
            //    // Failure Message
            //    //reprocessSucess.Visible = false;
            //    msgReprocessincomplete.Visible = true;
            //    //lblError.Text = "Reprocess Failure!";
            //}

            //BindEquityTransactionGrid(ProcessId);
       // }
        protected void rcbContinents1_PreRender(object sender, EventArgs e)
        {
        }
        protected void ddlRejectReason_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox dropdown = o as RadComboBox;
            ViewState["RejectReasonCode"] = dropdown.SelectedValue;
            if (ViewState["RejectReasonCode"] != "")
            {
                GridColumn column = gvWERPFI.MasterTableView.GetColumnSafe("RejectReasonCode");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvWERPFI.CurrentPageIndex = 0;
                gvWERPFI.MasterTableView.Rebind();
            }
            else
            {
                GridColumn column = gvWERPFI.MasterTableView.GetColumnSafe("RejectReasonCode");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvWERPFI.CurrentPageIndex = 0;
                gvWERPFI.MasterTableView.Rebind();

            }
        }
        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
        }
        protected void lnkViewInputRejects_OnClick(object sender, EventArgs e)
        {
            // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewEQTransactionInputrejects','processId=" + ProcessId + "');", true);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (GridDataItem gvr in this.gvWERPFI.Items)
            {
                if (((CheckBox)gvr.FindControl("chkBxWPTrans")).Checked == true)
                {
                    i = i + 1;
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select record to delete!');", true);
            }
            else
            {
                CustomerFIDelete();
                NeedSource();
                gvWERPFI.MasterTableView.Rebind();
                msgReprocessComplete.Visible = false;
                msgReprocessincomplete.Visible = false;
                msgDelete.Visible = true;
            }
        }

        private void CustomerFIDelete()
        {
           // string StagingID = string.Empty;
            foreach (GridDataItem gvr in this.gvWERPFI.Items)
            {
                if (((CheckBox)gvr.FindControl("chkBxWPTrans")).Checked == true)
                {
                    rejectedRecordsBo = new RejectedRecordsBo();
                    int StagingID = int.Parse(gvWERPFI.MasterTableView.DataKeyValues[gvr.ItemIndex]["CUS_Id"].ToString());

                    rejectedRecordsBo.DeleteWERPRejectedFixedIncome(StagingID);
                }
            }
           
            BindFixedIncomeGrid(ProcessId);
        }
      
       public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
        }
    }
}