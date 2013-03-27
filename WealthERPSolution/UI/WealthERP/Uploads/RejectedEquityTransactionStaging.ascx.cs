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

    public partial class RejectedEquityTransactionStaging : System.Web.UI.UserControl
    {
        DataTable dtTransactionTypes = new DataTable();
        DataTable dtFilterTransactionTypes = new DataTable();

        AdvisorVo adviserVo = new AdvisorVo();
        UploadProcessLogVo processlogVo;
        RejectedRecordsBo rejectedRecordsBo;
        UploadCommonBo uploadsCommonBo;
        WerpUploadsBo werpUploadBo;
        RMVo rmVo;
        UserVo userVo;
        DataSet dsRejectedRecords;
        DateTime fromDate;
        DateTime toDate;
        int ProcessId;
       // int adviserId;
        int rmId;
        DataView dvEquityReject;
        int rejectReasonCode;
        string configPath;
        string xmlFileName = string.Empty;

        protected override void OnInit(EventArgs e)
        {
            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            ProcessId = 0;
            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            GetPageCount();
           // this.BindEquityTransactionGrid(ProcessId);
        }

        private void GetPageCount()
        {
            string upperlimit;
            string lowerlimit;
            int rowCount = 0;
            if (hdnRecordCount.Value != "")
                rowCount = Convert.ToInt32(hdnRecordCount.Value);
            if (rowCount > 0)
            {
                int ratio = rowCount / 10;
                mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                lowerlimit = (((mypager.CurrentPage - 1) * 10) + 1).ToString();
                upperlimit = (mypager.CurrentPage * 10).ToString();
                if (mypager.CurrentPage == mypager.PageCount)
                    upperlimit = hdnRecordCount.Value;
                string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                lblCurrentPage.Text = PageRecords;

                hdnCurrentPage.Value = mypager.CurrentPage.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {   ProcessId = 0;
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
               // DataSet dsEquity = new DataSet();
               // if (Cache["RejectedEquityDetails" + adviserVo.advisorId.ToString()] != null)
                   // Cache["RejectedEquityDetails" + adviserVo.advisorId.ToString()] = dsEquity;

                DateTime fromDate = DateTime.Now.AddMonths(-1);
                txtFromTran.SelectedDate = fromDate.Date;
                txtToTran.SelectedDate = DateTime.Now;
               if (adviserVo.advisorId != 1000)
               {
                   BindddlRejectReason();
                    if (ProcessId!= 0)
                    {
                        divConditional.Visible = false;
                        lnkViewInputRejects.Visible = true;
                        BindEquityTransactionGrid(ProcessId);
                    }
                    else
                    {
                        lnkViewInputRejects.Visible = false;
                        divConditional.Visible = true;
                    }
                   
                    //BindEquityTransactionGrid(ProcessId);
                }
                else
                {
                    if (ProcessId!= 0)
                    {
                        divConditional.Visible = false;
                       // BindEquityTransactionGrid(ProcessId);
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
                       // lblEmptyMsg.Visible = false;
                      //  gvWERPTrans.Visible = false;
                        Panel2.Visible = false;
                        BindAdviserDropDownList();
                        tdTxtFromDate.Visible = false;
                  }
                                      
                }              

            }
           
           // btnExport.Visible = false;
            //btnExport.Visible = false;
            Msgerror.Visible = false;
            
       }
        
     private void BindEquityTransactionGrid(int ProcessId)
        {
            if (ProcessId == null || ProcessId == 0)
            {
                if (txtFromTran.SelectedDate != null)
                    fromDate = DateTime.Parse(txtFromTran.SelectedDate.ToString());
                if (txtToTran.SelectedDate != null)
                    toDate = DateTime.Parse(txtToTran.SelectedDate.ToString());
             
                rejectReasonCode = int.Parse(ddlRejectReason.SelectedValue);
             }
            Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
            Dictionary<string, string> genDictRejectReason = new Dictionary<string, string>();

            //if (hdnCurrentPage.Value.ToString() != "")
            //{
            //    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
            //    hdnCurrentPage.Value = "";
            //}
            rejectedRecordsBo = new RejectedRecordsBo();
            dsRejectedRecords = rejectedRecordsBo.GetRejectedEquityTransactionsStaging(adviserVo.advisorId, ProcessId, fromDate, toDate, rejectReasonCode);         
            if (dsRejectedRecords.Tables[0].Rows.Count > 0)
            { 
                //trMessage.Visible = false;
                trReprocess.Visible = true;
                DivAction.Visible = true;
               // msgDelete.Visible = false;

                if (Cache["RejectedEquityDetails" + adviserVo.advisorId.ToString()] == null)
                {
                    Cache.Insert("RejectedEquityDetails" + adviserVo.advisorId.ToString(), dsRejectedRecords);
                }
                else
                {
                    Cache.Remove("RejectedEquityDetails" + adviserVo.advisorId.ToString());
                    Cache.Insert("RejectedEquityDetails" + adviserVo.advisorId.ToString(), dsRejectedRecords);
                }

                gvWERPTrans.CurrentPageIndex = 0;
                gvWERPTrans.DataSource = dsRejectedRecords.Tables[0];
                gvWERPTrans.DataBind();
                gvWERPTrans.Visible = true;
                Panel2.Visible = true;
                msgDelete.Visible = false;
                btnExport.Visible = true;
            }
            else
            {
                hdnRecordCount.Value = "0";
                gvWERPTrans.DataSource = null;
                gvWERPTrans.DataBind();
                Msgerror.Visible = true;
                //trMessage.Visible = true;
                DivAction.Visible = false;
                gvWERPTrans.Visible = false;
                Panel2.Visible = false;
                trReprocess.Visible = false;
                btnExport.Visible = false;
            }
        }

        protected void BindAdviserDropDownList()
        {
            //DataTable dtAdviserList = new DataTable();
            //dtAdviserList = superAdminOpsBo.BindAdviserForUpload();

            //if (dtAdviserList.Rows.Count > 0)
            //{
            //    ddlAdviser.DataSource = dtAdviserList;
            //    ddlAdviser.DataTextField = "A_OrgName";
            //    ddlAdviser.DataValueField = "A_AdviserId";
            //    ddlAdviser.DataBind();
            //}
            //ddlAdviser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }
        protected void btnViewTran_Click(object sender, EventArgs e)
        {
          //  if (!string.IsNullOrEmpty(txtFromTran.SelectedDate.ToString()))
            if (txtFromTran.SelectedDate != null)
                fromDate = DateTime.Parse(txtFromTran.SelectedDate.ToString());
            if (txtToTran.SelectedDate != null)
                toDate = DateTime.Parse(txtToTran.SelectedDate.ToString());
            BindEquityTransactionGrid(ProcessId);
            msgDelete.Visible = false;
            msgReprocessincomplete.Visible = false;
            msgReprocessComplete.Visible = false;
           // ViewState.Remove("ProcessId");
            ViewState.Remove("RejectReasonCode");
            ViewState.Remove("TransactionTypeCode");
            
          
            //divLobAdded.Visible = false;
        }
        //********** Code implented by bhoopendra for adding a dropdown filter of process id.*************//
        //********** Code Starts *************//
        private void BindProcessId(DataTable dtProcessId)
        {
            Dictionary<string, string> genDictPanNum = new Dictionary<string, string>();
            if (dtProcessId.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtProcessId.Rows)
                {
                  //  genDictPanNum.Add(dr["ProcessId"].ToString(), dr["ProcessId"].ToString());
                }

                DropDownList ddlProcessId = GetProcessIdDDL();
                if (ddlProcessId != null)
                {
                    ddlProcessId.DataSource = genDictPanNum;
                    ddlProcessId.DataTextField = "Key";
                    ddlProcessId.DataValueField = "Value";
                    ddlProcessId.DataBind();
                  //  ddlProcessId.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnProcessIdFilter.Value != "")
                {
                    ddlProcessId.SelectedValue = hdnProcessIdFilter.Value.ToString().Trim();
                }
            }
        }

         public void BindddlRejectReason()
        {
            Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
            processlogVo = new UploadProcessLogVo();
            rejectedRecordsBo = new RejectedRecordsBo();
            DataSet ds = rejectedRecordsBo.GetRejectReasonEquityList(1);
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
         protected void ddlProcessId_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
         {  
            //  RadComboBox dropdown = o as RadComboBox;                          
            //  ViewState["ProcessId"] = dropdown.SelectedValue.ToString();
            //if (ViewState["ProcessId"] != "")
            //{
            //    GridColumn column = gvWERPTrans.MasterTableView.GetColumnSafe("ProcessId");
            //    column.CurrentFilterFunction = GridKnownFunction.EqualTo;
            //    gvWERPTrans.CurrentPageIndex = 0;
            //    gvWERPTrans.MasterTableView.Rebind();
                               
            //}
            //else
            //{
            //    GridColumn column = gvWERPTrans.MasterTableView.GetColumnSafe("ProcessId");
            //    column.CurrentFilterFunction = GridKnownFunction.EqualTo;
            //    gvWERPTrans.CurrentPageIndex = 0;
            //    gvWERPTrans.MasterTableView.Rebind();
              
            //}

        }
        private DropDownList GetProcessIdDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvWERPTrans.FindControl("ddlProcessId") != null)
            {
                ddl = (DropDownList)gvWERPTrans.FindControl("ddlProcessId");
            }
            return ddl;
        }

        /*************To delete the selected records ****************/

        protected void btnDelete_Click(object sender, EventArgs e)
        {
           
          int i=0;
          foreach (GridDataItem gvr in this.gvWERPTrans.Items)
           {
               if (((CheckBox)gvr.FindControl("chkBxWPTrans")).Checked == true)          
               {     i=i+1;
                    
               }              
           }
          if (i == 0)
          {
              ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select record to delete!');", true);

          }
          else
          {
             // DataView dvEquityReject=new DataView();
              CustomerTransactionDelete();
              NeedSource();
              gvWERPTrans.MasterTableView.Rebind();
              msgReprocessComplete.Visible = false;
              msgReprocessincomplete.Visible = false;
              msgDelete.Visible = true;
             
             
              //gvWERPTrans.MasterTableView.Rebind();
          } 
             
           }            
     
        private void CustomerTransactionDelete()
        {
        string StagingID = string.Empty;
        foreach (GridDataItem gvr in this.gvWERPTrans.Items)
        {
            if (((CheckBox)gvr.FindControl("chkBxWPTrans")).Checked == true)
             {
                rejectedRecordsBo = new RejectedRecordsBo();
                StagingID += Convert.ToString(gvWERPTrans.MasterTableView.DataKeyValues[gvr.ItemIndex]["WERPTransactionId"]) + "~";
               
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedEquityTransactionStaging','login');", true);
            }
            
        }

        rejectedRecordsBo.DeleteRejectsEquityTransactionsStaging(StagingID);
        BindEquityTransactionGrid(ProcessId);
        
    }
      
        //************** Code End  ***********************//

        protected void ddlPanNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlPanNum = GetPanNumDDL();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (ddlPanNum != null)
            {
                if (ddlPanNum.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnPanNumberFilter.Value = ddlPanNum.SelectedValue;
                    BindEquityTransactionGrid(ProcessId);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnPanNumberFilter.Value = "";
                    BindEquityTransactionGrid(ProcessId);
                }
            }
        }

        protected void ddlTransactionType_SelectedIndexChanged (object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox dropdown = o as RadComboBox;

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

                ViewState["TransactionTypeCode"] = dropdown.SelectedValue;
                if (ViewState["TransactionTypeCode"] != "")
            {
                GridColumn column = gvWERPTrans.MasterTableView.GetColumnSafe("TransactionTypeCode");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvWERPTrans.CurrentPageIndex = 0;
                gvWERPTrans.MasterTableView.Rebind();
                               
            }
            else
            {
                GridColumn column = gvWERPTrans.MasterTableView.GetColumnSafe("TransactionTypeCode");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvWERPTrans.CurrentPageIndex = 0;
                gvWERPTrans.MasterTableView.Rebind();
              
            }

        }
        private void BindPanNumber(DataTable dtPanNumber)
        {
            Dictionary<string, string> genDictPanNum = new Dictionary<string, string>();
            if (dtPanNumber.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtPanNumber.Rows)
                {
                    genDictPanNum.Add(dr["PANNumber"].ToString(), dr["PANNumber"].ToString());
                }

                DropDownList ddlPanNum = GetPanNumDDL();
                if (ddlPanNum != null)
                {
                    ddlPanNum.DataSource = genDictPanNum;
                    ddlPanNum.DataTextField = "Key";
                    ddlPanNum.DataValueField = "Value";
                    ddlPanNum.DataBind();
                    ddlPanNum.Items.Insert(0, new ListItem("Select PAN Number", "Select PAN Number"));
                }

                if (hdnPanNumberFilter.Value != "")
                {
                    ddlPanNum.SelectedValue = hdnPanNumberFilter.Value.ToString().Trim();
                }
            }
        }
        private void BindTranscationType(DataTable dtTransactionType)
        {
            Dictionary<string, string> genDictTrxnType = new Dictionary<string, string>();
            if (dtTransactionType.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtTransactionType.Rows)
                {
                    genDictTrxnType.Add(dr["WETT_TransactionTYpeName"].ToString(), dr["WETT_TransactionCode"].ToString());
                }

                DropDownList ddlTrxnType = GetTransactionTypeDdl();
                if (ddlTrxnType != null)
                {
                    ddlTrxnType.DataSource = genDictTrxnType;
                    ddlTrxnType.DataTextField = "Key";
                    ddlTrxnType.DataValueField = "Value";
                    ddlTrxnType.DataBind();
                    ddlTrxnType.Items.Insert(0, new ListItem("Select Transaction Type", "Select Transaction Type"));
                }

                if (hdnTransactionTypeFilter.Value != "")
                {
                    ddlTrxnType.SelectedValue = hdnTransactionTypeFilter.Value.ToString().Trim();
                }
            }
        }
        private DropDownList GetPanNumDDL()
        {
            DropDownList ddl = new DropDownList();
         //   if ((DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlPanNumber") != null)
            {
          //      ddl = (DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlPanNumber");
            }
            return ddl;
        }

        private DropDownList GetTransactionTypeDdl()
        {
            DropDownList ddl = new DropDownList();
        //    if ((DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlTransactionType") != null)
            {
          //      ddl = (DropDownList)gvWERPTrans.HeaderRow.FindControl("ddlTransactionType");
            }
            return ddl;
        }

        protected void btnEditSelectedWPTrans_Click(object sender, EventArgs e)
        {
            //int Id = 0;
            string panNumber = string.Empty;
            string tradeAccountNumber = string.Empty;
            string scripCode = string.Empty;
            string newScripCode = string.Empty;
            string exchange = string.Empty;
            string price = string.Empty;
            string newPrice = string.Empty;

            string transactionType = string.Empty;
            string newTransactionType = string.Empty;
            bool blResult = false;
            rejectedRecordsBo = new RejectedRecordsBo();

            // Gets the footer row directly Cool right! 
          //  GridViewRow footerRow = gvWERPTrans.FooterRow;
            GridFooterItem footerItem = sender as GridFooterItem;

            string newPanNumber = ((TextBox)footerItem.FindControl("txtPanNumberMultiple")).Text;

            if (((TextBox)footerItem.FindControl("txtScripCodeMultiple")).Text != string.Empty)
                newScripCode = ((TextBox)footerItem.FindControl("txtScripCodeMultiple")).Text;

            string newExchange = ((TextBox)footerItem.FindControl("txtExchangeMultiple")).Text;

            if (((DropDownList)footerItem.FindControl("ddlTransactionType")).SelectedValue != "-1")
                newTransactionType = ((DropDownList)footerItem.FindControl("ddlTransactionType")).SelectedValue;

            if (((TextBox)footerItem.FindControl("txtPriceMultiple")).Text != string.Empty)
                newPrice = ((TextBox)footerItem.FindControl("txtPriceMultiple")).Text;




            foreach (GridDataItem dr in gvWERPTrans.Items)
            {
                CheckBox checkBox = (CheckBox)dr.FindControl("chkBxWPTrans");
                if (checkBox.Checked)
                {
                    if (newPanNumber != "" || newScripCode != "" || newExchange != "" || newPrice != "" || newTransactionType != "") //Change this logic
                    {
                        panNumber = newPanNumber;
                        scripCode = newScripCode;
                        exchange = newExchange;
                        transactionType = newTransactionType;
                        price = newPrice;
                    }
                    else
                    {
                        panNumber = ((TextBox)dr.FindControl("txtPanNumber")).Text;
                        if (((TextBox)dr.FindControl("txtScripCode")).Text != string.Empty)
                            scripCode = ((TextBox)dr.FindControl("txtScripCode")).Text;
                        exchange = ((TextBox)dr.FindControl("txtExchange")).Text;
                        transactionType = ((DropDownList)dr.FindControl("ddlTransactionType")).SelectedValue;
                        // price = Convert.ToDouble(((TextBox)dr.FindControl("txtPrice")).Text);
                        price = ((TextBox)dr.FindControl("txtPrice")).Text;
                    }
                    int id = Convert.ToInt32(gvWERPTrans.MasterTableView.DataKeyNames[dr.RowIndex].ToString());


                    blResult = rejectedRecordsBo.UpdateRejectedEquityTransactionStaging(id, panNumber, scripCode, exchange, price.ToString(), transactionType);
                }


            }

          if (blResult)
            {
                // Success Message
            }
            else
            {
                // Failure Message
            }

            // BindGrid
            if (Request.QueryString["processId"] != null)
            {
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
            }
            BindEquityTransactionGrid(ProcessId);
        }

        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void gvWERPTrans_Sort(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = null;
            try
            {
                sortExpression = e.SortExpression;
                ViewState["sortExpression"] = sortExpression;
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    hdnSort.Value = sortExpression + " DESC";
                    this.BindEquityTransactionGrid(ProcessId);
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    hdnSort.Value = sortExpression + " ASC";
                    this.BindEquityTransactionGrid(ProcessId);
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedWERPTransaction.ascx.cs:gvWERPTrans_Sort()");

                object[] objects = new object[1];
                objects[0] = sortExpression;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void btnReprocess_Click(object sender, EventArgs e)
        {
            bool blResult = false;
            uploadsCommonBo = new UploadCommonBo();

            int countTransactionsInserted = 0;
            int countRejectedRecords = 0;
            string error = "";
            int processIdReprocessAll = 0;

            // BindGrid
            if (Request.QueryString["processId"] != null)
            {
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());
                blResult = MFWERPTransactionWERPInsertion(ProcessId, out countTransactionsInserted, out countRejectedRecords);
            }
            else
            {
                #region removed coz its taking all the processids
                               //DataSet ds = uploadsCommonBo.GetEquityTransactionStagingProcessId(adviserVo.advisorId);
                #endregion

                string StagingID = string.Empty;

                foreach (GridDataItem gvr in this.gvWERPTrans.Items)
                {
                    if (((CheckBox)gvr.FindControl("chkBxWPTrans")).Checked == true)
                    {
                        StagingID += Convert.ToString(gvWERPTrans.MasterTableView.DataKeyValues[gvr.ItemIndex]["ProcessId"]) + "~";

                    }
                }

                string[] a = StagingID.Split('~');


                foreach (string word in a) 


                //foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (word != "")
                    {
                        //processIdReprocessAll = int.Parse(dr["ProcessId"].ToString());
                        processIdReprocessAll = int.Parse(word);
                        blResult = MFWERPTransactionWERPInsertion(processIdReprocessAll, out countTransactionsInserted, out countRejectedRecords);
                    }
                 }
            }
            if (blResult == false)
            {
                error = error + "Error when reprocessing for the processid:" + processIdReprocessAll + ";";
            }

            if (blResult==true)
            {
                            
                NeedSource();
                gvWERPTrans.MasterTableView.Rebind();
                msgReprocessComplete.Visible = true;
                msgDelete.Visible = false;          
            
               
            }
            else
            {
                // Failure Message
                //reprocessSucess.Visible = false;
                msgReprocessincomplete.Visible = true;
                //lblError.Text = "Reprocess Failure!";
            }

            BindEquityTransactionGrid(ProcessId);
        }

        private bool MFWERPTransactionWERPInsertion(int ProcessId, out int countTransactionsInserted, out int countRejectedRecords)
        {
            bool blResult = false;
            processlogVo = new UploadProcessLogVo();
            uploadsCommonBo = new UploadCommonBo();
            werpUploadBo = new WerpUploadsBo();

            countTransactionsInserted = 0;
            countRejectedRecords = 0;

            processlogVo = uploadsCommonBo.GetProcessLogInfo(ProcessId);

            // WERP Insertion
            // WERP Equity Transation Insertion
            WerpEQUploadsBo werpEQUploadsBo = new WerpEQUploadsBo();
            string packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadChecksOnEQTranStaging.dtsx");
            bool WERPEQSecondStagingCheckResult = werpEQUploadsBo.WERPEQProcessDataInSecondStagingTrans(ProcessId, packagePath, configPath, adviserVo.advisorId);

            if (WERPEQSecondStagingCheckResult)
            {
                packagePath = Server.MapPath("\\UploadPackages\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\EQTransactionUploadPackage\\UploadEQTranStagingToWerp.dtsx");
                bool WERPEQTranWerpResult = werpEQUploadsBo.WERPEQInsertTransDetails(ProcessId, packagePath, configPath); // EQ Trans XML File Type Id = 8);
                if (WERPEQTranWerpResult)
                {
                    processlogVo.IsInsertionToWERPComplete = 1;
                    processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetTransUploadCount(ProcessId, "WPEQ");
                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetTransUploadRejectCount(ProcessId, "WPEQ");
                    processlogVo.EndTime = DateTime.Now;
                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                }
            }
            return blResult;
        }

        protected void lnkViewInputRejects_OnClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewEQTransactionInputrejects','processId=" + ProcessId + "');", true);
        }


        protected void ddlRejectReason_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {      
            RadComboBox dropdown = o as RadComboBox;
            ViewState["RejectReasonCode"] = dropdown.SelectedValue;
            if (ViewState["RejectReasonCode"] != "")
            {
                GridColumn column = gvWERPTrans.MasterTableView.GetColumnSafe("RejectReasonCode");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvWERPTrans.CurrentPageIndex = 0;
                gvWERPTrans.MasterTableView.Rebind();
             
            }
            else
            {
                GridColumn column = gvWERPTrans.MasterTableView.GetColumnSafe("RejectReasonCode");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvWERPTrans.CurrentPageIndex = 0;
                gvWERPTrans.MasterTableView.Rebind();
              
            }

            //DropDownList ddlReject = GetRejectReasonDDL();

            //if (Request.QueryString["processId"] != null)
            //    ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            //if (ddlReject != null)
            //{
            //    if (ddlReject.SelectedIndex != 0)
            //    {   // Bind the Grid with Only Selected Values
            //        hdnRejectReasonFilter.Value = ddlReject.SelectedValue;
            //        BindEquityTransactionGrid(ProcessId);
            //    }
            //    else
            //    {   // Bind the Grid with Only All Values
            //        hdnRejectReasonFilter.Value = "";
            //        BindEquityTransactionGrid(ProcessId);
            //    }
            //}
        }

        private TextBox GetFolioTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPTrans.FindControl("txtFolioSearch") != null)
            {
                txt = (TextBox)gvWERPTrans.FindControl("txtFolioSearch");
            }
            return txt;
        }

        private DropDownList GetIsRejectedDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvWERPTrans.FindControl("ddlIsRejected") != null)
            {
                ddl = (DropDownList)gvWERPTrans.FindControl("ddlIsRejected");
            }
            return ddl;
        }

        private DropDownList GetRejectReasonDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvWERPTrans.FindControl("ddlRejectReason") != null)
            {
                ddl = (DropDownList)gvWERPTrans.FindControl("ddlRejectReason");
            }
            return ddl;
        }

        protected void btnGridSearch_Click(object sender, EventArgs e)
        {
            TextBox txtPanNumber = GetPanNumberTextBox();
            TextBox txtTradeAccountNumber = GetTradeAccountNumberTextBox();
            TextBox txtScrip = GetScripTextBox();
            TextBox txtExchange = GetExchangeTextBox();
            TextBox txtTransactionType = GetTransactionTypeTextBox();

            if (Request.QueryString["processId"] != null)
                ProcessId = Int32.Parse(Request.QueryString["processId"].ToString());

            if (txtPanNumber != null)
                hdnPanNumberFilter.Value = txtPanNumber.Text.Trim();
            if (txtTradeAccountNumber != null)
                hdnTradeAccountNumberFilter.Value = txtTradeAccountNumber.Text.Trim();
            if (txtScrip != null)
                hdnScripFilter.Value = txtScrip.Text.Trim();
            if (txtExchange != null)
                hdnExchangeFilter.Value = txtExchange.Text.Trim();

            if (txtTransactionType != null)
                hdnTransactionTypeFilter.Value = txtTransactionType.Text.Trim();

            BindEquityTransactionGrid(ProcessId);
        }

        private TextBox GetTransactionTypeTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPTrans.FindControl("txtTransactionTypeSearch") != null)
            {
                txt = (TextBox)gvWERPTrans.FindControl("txtTransactionTypeSearch");
            }
            return txt;
        }

        private TextBox GetExchangeTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPTrans.FindControl("txtExchangeSearch") != null)
            {
                txt = (TextBox)gvWERPTrans.FindControl("txtExchangeSearch");
            }
            return txt;
        }

        private TextBox GetScripTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPTrans.FindControl("txtScripCodeSearch") != null)
            {
                txt = (TextBox)gvWERPTrans.FindControl("txtScripCodeSearch");
            }
            return txt;
        }

        private TextBox GetTradeAccountNumberTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPTrans.FindControl("txtTradeAccountNumberSearch") != null)
            {
                txt = (TextBox)gvWERPTrans.FindControl("txtTradeAccountNumberSearch");
            }
            return txt;
        }

        private TextBox GetPanNumberTextBox()
        {
            TextBox txt = new TextBox();
            if ((TextBox)gvWERPTrans.FindControl("txtPanNumberSearch") != null)
            {
                txt = (TextBox)gvWERPTrans.FindControl("txtPanNumberSearch");
            }
            return txt;
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewUploadProcessLog','login');", true);
        }

        /// <summary>
        /// This method displays the transaction type drop down in each row  
        /// </summary>
        protected void gvWERPTrans_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            const int CONST_TRANSACTION_TYPE_COLUMN = 9;

            /// Dropdown in each row || Header dropdown(for filtering) ||  Footer dropdown (Multiple updates)
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Footer || e.Row.RowType == DataControlRowType.Header)
            {
                HiddenField hdnTransactionType = new HiddenField();
                DropDownList ddlTransactionType = (DropDownList)e.Row.Cells[CONST_TRANSACTION_TYPE_COLUMN].FindControl("ddlTransactionType");

                if (e.Row.Cells[CONST_TRANSACTION_TYPE_COLUMN].FindControl("hdnTransactionType") != null)
                    hdnTransactionType = (HiddenField)e.Row.Cells[CONST_TRANSACTION_TYPE_COLUMN].FindControl("hdnTransactionType");
                if (ddlTransactionType != null)
                {
                    ddlTransactionType.Items.Add(new ListItem("Select Transaction Type", ""));

                    ddlTransactionType.SelectedValue = "";


                    ///Add all the Transaction types.
                    if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Footer)
                    {
                        foreach (DataRow dr in dtTransactionTypes.Rows)
                        {
                            ddlTransactionType.Items.Add(new ListItem(dr["WETT_TransactionTYpeName"].ToString(), dr["WETT_TransactionCode"].ToString()));
                        }
                        if (!String.IsNullOrEmpty(hdnTransactionType.Value) && Convert.ToInt32(hdnTransactionType.Value) > 0)
                            ddlTransactionType.SelectedValue = hdnTransactionType.Value;
                    }
                    else if (e.Row.RowType == DataControlRowType.Header)  //Add transaction types for filtering.
                    {
                        foreach (DataRow dr in dtFilterTransactionTypes.Rows)
                        {
                            ddlTransactionType.Items.Add(new ListItem(dr["WETT_TransactionTYpeName"].ToString(), dr["WETT_TransactionCode"].ToString()));
                        }
                        if (!String.IsNullOrEmpty(hdnTransactionTypeFilter.Value) && Convert.ToInt32(hdnTransactionTypeFilter.Value) > 0)
                            ddlTransactionType.SelectedValue = hdnTransactionTypeFilter.Value;
                    }
                }
            }

        }



        protected void gvWERPTrans_PreRender(object sender, EventArgs e)
        {
            if (gvWERPTrans.MasterTableView.FilterExpression != string.Empty)
            {
                RefreshCombos();
            }
        }

        protected void RefreshCombos()
        {


            dsRejectedRecords = (DataSet)Cache["RejectedEquityDetails" + adviserVo.advisorId.ToString()];
            dtTransactionTypes = dsRejectedRecords.Tables[0];
            DataView view = new DataView(dtTransactionTypes);
            DataTable distinctValues = view.ToTable();
            DataRow[] rows = distinctValues.Select(gvWERPTrans.MasterTableView.FilterExpression.ToString());
            gvWERPTrans.MasterTableView.Rebind();
           
           
        }


        protected void gvWERPTrans_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && e.Item.ItemIndex == -1)
            {
                DataTable dtgvWERPEQ = new DataTable();
                GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                RadComboBox RadComboBoxRR = (RadComboBox)filterItem.FindControl("RadComboBoxRR");
                RadComboBox RadComboBoxTT = (RadComboBox)filterItem.FindControl("RadComboBoxTT");
                dsRejectedRecords = (DataSet)Cache["RejectedEquityDetails" + adviserVo.advisorId.ToString()];

                //Session["dt"] = dtgvWERPEQ;
                DataTable dtcustMIS = new DataTable();
                dtgvWERPEQ = dsRejectedRecords.Tables[0];
                dtcustMIS.Columns.Add("RejectReason");
                //dtcustMIS.Columns.Add("ProcessId");
                dtcustMIS.Columns.Add("TransactionType");
                dtcustMIS.Columns.Add("RejectReasonCode");
                dtcustMIS.Columns.Add("TransactionTypeCode");
                DataRow drcustMIS;
                foreach (DataRow dr in dtgvWERPEQ.Rows)
                {
                    drcustMIS = dtcustMIS.NewRow();
                    drcustMIS["RejectReason"] = dr["RejectReason"].ToString();
                    //drcustMIS["ProcessId"] = dr["ProcessId"].ToString();
                    drcustMIS["TransactionType"] = dr["TransactionType"].ToString();
                    drcustMIS["RejectReasonCode"] =int.Parse(dr["RejectReasonCode"].ToString());
                    drcustMIS["TransactionTypeCode"] = int.Parse(dr["TransactionTypeCode"].ToString());
                    dtcustMIS.Rows.Add(drcustMIS);
                }
                DataView view = new DataView(dtgvWERPEQ);
                DataTable distinctValues = view.ToTable(true, "RejectReason", "RejectReasonCode");
                    //"RejectReasonCode");
                RadComboBoxRR.DataSource = distinctValues;
                RadComboBoxRR.DataValueField = dtcustMIS.Columns["RejectReasonCode"].ToString();
                RadComboBoxRR.DataTextField = dtcustMIS.Columns["RejectReason"].ToString();
                RadComboBoxRR.DataBind();
                DataTable distinctTT = view.ToTable(true, "TransactionType", "TransactionTypeCode");
                RadComboBoxTT.DataSource = distinctTT;
                RadComboBoxTT.DataValueField = dtcustMIS.Columns["TransactionTypeCode"].ToString();
                RadComboBoxTT.DataTextField = dtcustMIS.Columns["TransactionType"].ToString();
                RadComboBoxTT.DataBind();

            }

        }

        protected void NeedSource()
        {
            string rcbType = string.Empty;
            string tttype = string.Empty;
            DataSet dsEquity = new DataSet();
            DataTable dtrr = new DataTable();
           // DataView dvEquityReject = new DataView();
            dsEquity = (DataSet)Cache["RejectedEquityDetails" + adviserVo.advisorId.ToString()];
            if (dsEquity != null)
            {
                dtrr = dsEquity.Tables[0];
                if (ViewState["RejectReasonCode"] != null)
                    rcbType = ViewState["RejectReasonCode"].ToString();
                if (ViewState["TransactionTypeCode"] != null)
                    tttype = ViewState["TransactionTypeCode"].ToString();
                if ((!string.IsNullOrEmpty(rcbType)) && (string.IsNullOrEmpty(tttype)))
                {
                    dvEquityReject = new DataView(dtrr, "RejectReasonCode = '" + rcbType + "'", "ProcessId,TransactionTypeCode,Exchange", DataViewRowState.CurrentRows);
                    gvWERPTrans.DataSource = dvEquityReject.ToTable();
                }
                else if ((string.IsNullOrEmpty(rcbType)) && (!string.IsNullOrEmpty(tttype)))
                {
                    dvEquityReject = new DataView(dtrr, "TransactionTypeCode= '" + tttype + "'", "RejectReasonCode,ProcessId,Exchange", DataViewRowState.CurrentRows);
                    gvWERPTrans.DataSource = dvEquityReject.ToTable();
                }
                else if ((!string.IsNullOrEmpty(rcbType)) && (!string.IsNullOrEmpty(tttype)))
                {
                    dvEquityReject = new DataView(dtrr, "RejectReasonCode = '" + rcbType + "'and TransactionTypeCode= '" + tttype + "'", "ProcessId,Exchange", DataViewRowState.CurrentRows);
                    gvWERPTrans.DataSource = dvEquityReject.ToTable();
                }            
                else
                {
                    gvWERPTrans.DataSource = dtrr;
                  //  btnExport.Visible = true;
                }
            }              
        
        
        }


        protected void gvWERPTrans_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
                    // int rcbtype;
            string rcbType = string.Empty;
            string pitype = string.Empty;
            string tttype = string.Empty;
            btnExport.Visible = true;
            DataSet dsEquity = new DataSet();
            DataTable dtrr = new DataTable();
            dsEquity = (DataSet)Cache["RejectedEquityDetails" + adviserVo.advisorId.ToString()];
            if (dsEquity!=null)
            {
                dtrr = dsEquity.Tables[0];
                if (ViewState["RejectReasonCode"] != null)
                    rcbType = ViewState["RejectReasonCode"].ToString();
                if (ViewState["TransactionTypeCode"] != null)
                   tttype = ViewState["TransactionTypeCode"].ToString();
                if ((!string.IsNullOrEmpty(rcbType))&&(string.IsNullOrEmpty(tttype)))
                {
                    dvEquityReject = new DataView(dtrr, "RejectReasonCode = '" + rcbType + "'","ProcessId,TransactionTypeCode,Exchange",DataViewRowState.CurrentRows);
                    gvWERPTrans.DataSource = dvEquityReject.ToTable();
                }
             else if ((string.IsNullOrEmpty(rcbType)) && (!string.IsNullOrEmpty(tttype)))
                {
                    dvEquityReject = new DataView(dtrr, "TransactionTypeCode= '" + tttype + "'", "RejectReasonCode,ProcessId,Exchange", DataViewRowState.CurrentRows);
                    gvWERPTrans.DataSource = dvEquityReject.ToTable();
                }
            else if ((!string.IsNullOrEmpty(rcbType))&& (!string.IsNullOrEmpty(tttype)))
                {
                    dvEquityReject = new DataView(dtrr, "RejectReasonCode = '" + rcbType + "'and TransactionTypeCode= '" + tttype + "'", "ProcessId,Exchange", DataViewRowState.CurrentRows);
                    gvWERPTrans.DataSource = dvEquityReject.ToTable();
                
                }
            else
                {
                    gvWERPTrans.DataSource = dtrr;
                    btnExport.Visible = true;                  
                }
            }
        }
         protected void rcbContinents1_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            if (ViewState["RejectReasonCode"] != null)
            {
                Combo.SelectedValue = ViewState["RejectReasonCode"].ToString();
            }

        }
       
        protected void rcbContinents3_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
           if (ViewState["TransactionTypeCode"] != null)
            {

                Combo.SelectedValue = ViewState["TransactionTypeCode"].ToString();
            }

        }
        protected void btnAddLob_Click(object sender, EventArgs e)
        { }



        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvWERPTrans.ExportSettings.OpenInNewWindow = true;
            gvWERPTrans.ExportSettings.IgnorePaging = true;
            gvWERPTrans.ExportSettings.HideStructureColumns = true;
            gvWERPTrans.ExportSettings.ExportOnlyData = true;
            gvWERPTrans.ExportSettings.FileName = "Rejected Equity Transaction Details";
            gvWERPTrans.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvWERPTrans.MasterTableView.ExportToExcel();
        }

    }



}
