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
    public partial class RejectedSystematicTransactionStaging : System.Web.UI.UserControl
    {
        KarvyUploadsVo karvyUploadsVo = new KarvyUploadsVo();

        CustomerVo customerVo = new CustomerVo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        MFTransactionVo mfTransactionVo = new MFTransactionVo();
        UserVo userVo = new UserVo();
        UserVo rmUserVo = new UserVo();
        UserVo tempUserVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        UploadProcessLogVo processlogVo = new UploadProcessLogVo();
        RMVo rmVo = new RMVo();
        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();
        int adviserId;
        int rmId;

        int filetypeId;
        string ValidationProgress = "";
        // int processId;
        int rejectReasonCode;
        DateTime fromDate;
        DateTime toDate;
        DataView dvSIPReject;
        CustomerBo customerBo = new CustomerBo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        UploadCommonBo uploadsCommonBo = new UploadCommonBo();
        UploadValidationBo uploadsvalidationBo = new UploadValidationBo();
        UserBo userBo = new UserBo();
        int processId;
        string configPath;
        DataSet dsRejectedSIP = new DataSet();
        protected override void OnInit(EventArgs e)
        {
            //((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            //mypager.EnableViewState = true;
            //base.OnInit(e);
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            processId = 0;
            if (Request.QueryString["processId"] != null)
                processId = Int32.Parse(Request.QueryString["processId"].ToString());



            // GetPageCount();
            // this.BindRejectedSIPGrid(processId);
        }

        //private void GetPageCount()
        //{
        //    string upperlimit;
        //    string lowerlimit;
        //    int rowCount = 0;
        //    if (hdnRecordCount.Value != "")
        //        rowCount = Convert.ToInt32(hdnRecordCount.Value);
        //    if (rowCount > 0)
        //    {
        //        int ratio = rowCount / 10;
        //        mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
        //        mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
        //        lowerlimit = (((mypager.CurrentPage - 1) * 10) + 1).ToString();
        //        upperlimit = (mypager.CurrentPage * 10).ToString();
        //        if (mypager.CurrentPage == mypager.PageCount)
        //            upperlimit = hdnRecordCount.Value;
        //        string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
        //        lblCurrentPage.Text = PageRecords;

        //        hdnCurrentPage.Value = mypager.CurrentPage.ToString();
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {

            customerVo = (CustomerVo)Session["customerVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            processId = 0;
            adviserId = adviserVo.advisorId;
            configPath = Server.MapPath(ConfigurationManager.AppSettings["SSISConfigPath"].ToString());
            if (Request.QueryString["processId"] != null)
            {
                processId = Int32.Parse(Request.QueryString["processId"].ToString());
                LinkInputRejects.Visible = true;
            }
            //if (Request.QueryString["filetypeid"] != null)
            // {
            //     filetypeId = Int32.Parse(Request.QueryString["filetypeid"].ToString());

           // }
            else
            {
                LinkInputRejects.Visible = false;
            }

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
                    Session["adviserId_Upload"] = adviserId;
                }
                else
                {
                    adviserId = 1000;
                }
            }
            else
            {
                Session["adviserId_Upload"] = adviserId;
                if (adviserVo.advisorId != 1000)
                {
                    adviserId = adviserVo.advisorId;
                }
                if (rmVo != null)
                    rmId = rmVo.RMId;
        }

            if (!Page.IsPostBack)
            {
                DataSet dsSIP = new DataSet();
                if (Cache["RejectedSIPDetails" + adviserId] != null)
                    Cache["RejectedSIPDetails" + adviserId] = dsSIP;

                DateTime fromDate = DateTime.Now.AddMonths(-1);
                txtFromSIP.SelectedDate = fromDate.Date;
                txtToSIP.SelectedDate = DateTime.Now;


                if (adviserId != 1000)
                {
                    trAdviserSelection.Visible = false;
                    BindddlRejectReason();
                    if (processId != 0)
                    {
                        divConditional.Visible = false;
                        BindRejectedSIPGrid(processId);
                    }
                    else
                    {
                        divConditional.Visible = true;
                    }

                    // BindRejectedSIPGrid(processId);
                }
                else
                {
                    trAdviserSelection.Visible = true;
                    BindAdviserDropDownList();
                    if (processId != 0)
                    {
                        divConditional.Visible = false;
                        BindRejectedSIPGrid(processId);
                    }
                    else
                    {

                        BindddlRejectReason();
                        divConditional.Visible = true;
                        //tdBtnViewRejetcs.Visible = false;
                        //tdTxtToDate.Visible = false;
                        //tdlblToDate.Visible = false;
                        //tdTxtFromDate.Visible = false;
                        //tdlblFromDate.Visible = false;
                        //tdlblRejectReason.Visible = false;
                        //tdDDLRejectReason.Visible = false;
                        //lblEmptyMsg.Visible = false;
                        gvSIPReject.Visible = false;
                        //Panel3.Visible = false;
                        btnExport.Visible = false;

                    }
                }
            }
            // btnExport.Visible = false;
            Msgerror.Visible = false;
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

        protected void ddlAdviser_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Panel3.Visible = false;
            DivAction.Visible = false;
        }

        public void BindddlRejectReason()
        {
            Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
            processlogVo = new UploadProcessLogVo();
            uploadsCommonBo = new UploadCommonBo();
            DataSet ds = uploadsCommonBo.GetRejectReasonSIPList(1);
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
        private void BindRejectedSIPGrid(int processId)
        {
            if (processId == null || processId == 0)
            {
                if (txtFromSIP.SelectedDate != null)
                    fromDate = DateTime.Parse(txtFromSIP.SelectedDate.ToString());
                if (txtToSIP.SelectedDate != null)
                    toDate = DateTime.Parse(txtToSIP.SelectedDate.ToString());
                rejectReasonCode = int.Parse(ddlRejectReason.SelectedValue);
            }
            DataSet dsRejectedSIP = new DataSet();
            Dictionary<string, string> genDictIsRejected = new Dictionary<string, string>();
            Dictionary<string, string> genDictRejectReason = new Dictionary<string, string>();
            if (adviserId != 1000)
                dsRejectedSIP = uploadsCommonBo.GetRejectedSIPRecords(adviserId, processId, fromDate, toDate, rejectReasonCode);
            else
            {
                if (Request.QueryString["processId"] != null)
                    dsRejectedSIP = uploadsCommonBo.GetRejectedSIPRecords(Convert.ToInt32(adviserId), processId, fromDate, toDate, rejectReasonCode);
                else
                    dsRejectedSIP = uploadsCommonBo.GetRejectedSIPRecords(Convert.ToInt32(ddlAdviser.SelectedValue), processId, fromDate, toDate, rejectReasonCode);
            }

            if (dsRejectedSIP.Tables[0].Rows.Count > 0)
            {   // If Records found, then bind them to grid
                //trMessage.Visible = false;
                trReprocess.Visible = true;
                DivAction.Visible = true;
                if (Cache["RejectedSIPDetails" + adviserId.ToString()] == null)
                {
                    Cache.Insert("RejectedSIPDetails" + adviserId.ToString(), dsRejectedSIP);
                }
                else
                {
                    Cache.Remove("RejectedSIPDetails" + adviserId.ToString());
                    Cache.Insert("RejectedSIPDetails" + adviserId.ToString(), dsRejectedSIP);
                }
                gvSIPReject.CurrentPageIndex = 0;
                gvSIPReject.DataSource = dsRejectedSIP.Tables[0];
                gvSIPReject.DataBind();
                btnExport.Visible = true;
                gvSIPReject.Visible = true;
                Msgerror.Visible = false;
                Panel3.Visible = true;

            }
            else
            {
                gvSIPReject.CurrentPageIndex = 0;
                hdnRecordCount.Value = "0";
                gvSIPReject.DataSource = null;
                gvSIPReject.DataBind();
                gvSIPReject.Visible = false;
                Panel3.Visible = false;
                Msgerror.Visible = true;
                btnExport.Visible = false;
                DivAction.Visible = false;
                trReprocess.Visible = false;
            }
        }

        protected void gvSIPReject_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && e.Item.ItemIndex == -1)
            {
                DataTable dtgvWERPSIP = new DataTable();
                GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                RadComboBox RadComboBoxRR = (RadComboBox)filterItem.FindControl("RadComboBoxRR");
                RadComboBox RadComboBoxTT = (RadComboBox)filterItem.FindControl("RadComboBoxTT");

                dsRejectedSIP = (DataSet)Cache["RejectedSIPDetails" + adviserId.ToString()];
                DataTable dtSIP = new DataTable();
                dtgvWERPSIP = dsRejectedSIP.Tables[0];
                dtSIP.Columns.Add("WRR_RejectReasonDescription");
                dtSIP.Columns.Add("CMFSCS_SystematicCode");
                dtSIP.Columns.Add("WRR_RejectReasonCode");
                dtSIP.Columns.Add("TransactionTypeCode");
                DataRow drSIP;
                foreach (DataRow dr in dtgvWERPSIP.Rows)
                {
                    drSIP = dtSIP.NewRow();
                    drSIP["WRR_RejectReasonDescription"] = dr["WRR_RejectReasonDescription"].ToString();
                    drSIP["CMFSCS_SystematicCode"] = dr["CMFSCS_SystematicCode"].ToString();
                    drSIP["WRR_RejectReasonCode"] = int.Parse(dr["WRR_RejectReasonCode"].ToString());
                    dtSIP.Rows.Add(drSIP);
                }
                DataView view = new DataView(dtgvWERPSIP);
                DataTable distinctValues = view.ToTable(true, "WRR_RejectReasonDescription", "WRR_RejectReasonCode");
                RadComboBoxRR.DataSource = distinctValues;
                RadComboBoxRR.DataValueField = dtSIP.Columns["WRR_RejectReasonCode"].ToString();
                RadComboBoxRR.DataTextField = dtSIP.Columns["WRR_RejectReasonDescription"].ToString();
                RadComboBoxRR.DataBind();
                DataTable distinctTT = view.ToTable(true, "CMFSCS_SystematicCode");
                RadComboBoxTT.DataSource = distinctTT;
                RadComboBoxTT.DataValueField = dtSIP.Columns["CMFSCS_SystematicCode"].ToString();
                RadComboBoxTT.DataTextField = dtSIP.Columns["CMFSCS_SystematicCode"].ToString();
                RadComboBoxTT.DataBind();
            }
        }
        protected void NeedSource()
        {
            string rcbType = string.Empty;
            string tttype = string.Empty;
            // btnExport.Visible = true;
            DataSet dsSIP = new DataSet();
            DataTable dtrr = new DataTable();
            dsSIP = (DataSet)Cache["RejectedSIPDetails" + adviserId.ToString()];
            if (dsSIP != null)
            {
                dtrr = dsSIP.Tables[0];
                if (ViewState["WRR_RejectReasonCode"] != null)
                    rcbType = ViewState["WRR_RejectReasonCode"].ToString();
                if (ViewState["CMFSCS_SystematicCode"] != null)
                    tttype = ViewState["CMFSCS_SystematicCode"].ToString();
                if ((!string.IsNullOrEmpty(rcbType)) && (string.IsNullOrEmpty(tttype)))
                {
                    DataView dvStaffList = new DataView(dtrr, "WRR_RejectReasonCode = '" + rcbType + "'", "WUPL_ProcessId,CMFSCS_SystematicCode,ADUL_FileName,CMFSCS_InvName,CMFSCS_FolioNum,CMFSCS_SchemeName", DataViewRowState.CurrentRows);
                    gvSIPReject.DataSource = dvStaffList.ToTable();
                }
                else if ((string.IsNullOrEmpty(rcbType)) && (!string.IsNullOrEmpty(tttype)))
                {
                    DataView dvStaffList = new DataView(dtrr, "CMFSCS_SystematicCode= '" + tttype + "'", "WRR_RejectReasonCode,WUPL_ProcessId,CMFSCS_SystematicCode,ADUL_FileName,CMFSCS_InvName,CMFSCS_FolioNum,CMFSCS_SchemeName", DataViewRowState.CurrentRows);
                    gvSIPReject.DataSource = dvStaffList.ToTable();
                }
                else if ((!string.IsNullOrEmpty(rcbType)) && (!string.IsNullOrEmpty(tttype)))
                {
                    DataView dvStaffList = new DataView(dtrr, "WRR_RejectReasonCode = '" + rcbType + "'and CMFSCS_SystematicCode= '" + tttype + "'", "WUPL_ProcessId,CMFSCS_SystematicCode,ADUL_FileName,CMFSCS_InvName,CMFSCS_FolioNum,CMFSCS_SchemeName", DataViewRowState.CurrentRows);
                    gvSIPReject.DataSource = dvStaffList.ToTable();

                }
                else
                {
                    gvSIPReject.DataSource = dtrr;
                    //btnExport.Visible = true;
                }
            }
        }

        protected void gvSIPReject_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string rcbType = string.Empty;
            string tttype = string.Empty;
            btnExport.Visible = true;
            DataSet dsSIP = new DataSet();
            DataTable dtrr = new DataTable();
            dsSIP = (DataSet)Cache["RejectedSIPDetails" + adviserId.ToString()];
            if (dsSIP != null)
            {
                dtrr = dsSIP.Tables[0];
                if (ViewState["WRR_RejectReasonCode"] != null)
                    rcbType = ViewState["WRR_RejectReasonCode"].ToString();
                if (ViewState["CMFSCS_SystematicCode"] != null)
                    tttype = ViewState["CMFSCS_SystematicCode"].ToString();
                if ((!string.IsNullOrEmpty(rcbType)) && (string.IsNullOrEmpty(tttype)))
                {
                    DataView dvStaffList = new DataView(dtrr, "WRR_RejectReasonCode = '" + rcbType + "'", "WUPL_ProcessId,CMFSCS_SystematicCode,ADUL_FileName,CMFSCS_InvName,CMFSCS_FolioNum,CMFSCS_SchemeName", DataViewRowState.CurrentRows);
                    gvSIPReject.DataSource = dvStaffList.ToTable();
                }
                else if ((string.IsNullOrEmpty(rcbType)) && (!string.IsNullOrEmpty(tttype)))
                {
                    DataView dvStaffList = new DataView(dtrr, "CMFSCS_SystematicCode= '" + tttype + "'", "WRR_RejectReasonCode,WUPL_ProcessId,CMFSCS_SystematicCode,ADUL_FileName,CMFSCS_InvName,CMFSCS_FolioNum,CMFSCS_SchemeName", DataViewRowState.CurrentRows);
                    gvSIPReject.DataSource = dvStaffList.ToTable();
                }
                else if ((!string.IsNullOrEmpty(rcbType)) && (!string.IsNullOrEmpty(tttype)))
                {
                    DataView dvStaffList = new DataView(dtrr, "WRR_RejectReasonCode = '" + rcbType + "'and CMFSCS_SystematicCode= '" + tttype + "'", "WUPL_ProcessId,CMFSCS_SystematicCode,ADUL_FileName,CMFSCS_InvName,CMFSCS_FolioNum,CMFSCS_SchemeName", DataViewRowState.CurrentRows);
                    gvSIPReject.DataSource = dvStaffList.ToTable();

                }
                else
                {
                    gvSIPReject.DataSource = dtrr;
                    btnExport.Visible = true;
                }
            }
        }


        protected void lnkBtnBackToUploadLog_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RejectedSystematicTransactionStaging','login');", true);
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RejectedSystematicTransactionStaging','login');", true);
        }


        protected void ddlProcessId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList ddlProcessId = GetProcessIdDDL();

            //if (ddlProcessId != null)
            //{
            //    if (ddlProcessId.SelectedIndex != 0)
            //    {   // Bind the Grid with Only Selected Values
            //        hdnProcessIdFilter.Value = ddlProcessId.SelectedValue;
            //        processId = int.Parse(hdnProcessIdFilter.Value);
            //        BindRejectedSIPGrid(processId);
            //    }
            //    else
            //    {   // Bind the Grid with Only All Values
            //        hdnProcessIdFilter.Value = "0";
            //        BindRejectedSIPGrid(processId);
            //    }
            //}
        }

        protected void ddlInvName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList ddlInvName = GetInvNameDDL();

            //if (Request.QueryString["processId"] != null)
            //    processId = Int32.Parse(Request.QueryString["processId"].ToString());



            //if (ddlInvName != null)
            //{
            //    if (ddlInvName.SelectedIndex != 0)
            //    {   // Bind the Grid with Only Selected Values
            //        hdnInvNameFilter.Value = ddlInvName.SelectedValue;
            //        BindRejectedSIPGrid(processId);
            //    }
            //    else
            //    {   // Bind the Grid with Only All Values
            //        hdnInvNameFilter.Value = "";
            //        BindRejectedSIPGrid(processId);
            //    }
            //}
        }

        protected void ddlRejectReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadComboBox dropdown = sender as RadComboBox;
            ViewState["WRR_RejectReasonCode"] = dropdown.SelectedValue;
            if (ViewState["WRR_RejectReasonCode"] != "")
            {
                GridColumn column = gvSIPReject.MasterTableView.GetColumnSafe("WRR_RejectReasonCode");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvSIPReject.CurrentPageIndex = 0;
                gvSIPReject.MasterTableView.Rebind();

            }
            else
            {
                GridColumn column = gvSIPReject.MasterTableView.GetColumnSafe("WRR_RejectReasonCode");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvSIPReject.CurrentPageIndex = 0;
                gvSIPReject.MasterTableView.Rebind();

            }
            //DropDownList ddlReject = GetRejectReasonDDL();

            //if (Request.QueryString["processId"] != null)
            //    processId = Int32.Parse(Request.QueryString["processId"].ToString());



            //if (ddlReject != null)
            //{
            //    if (ddlReject.SelectedIndex != 0)
            //    {   // Bind the Grid with Only Selected Values
            //        hdnRejectReasonFilter.Value = ddlReject.SelectedValue;
            //        BindRejectedSIPGrid(processId);
            //    }
            //    else
            //    {   // Bind the Grid with Only All Values
            //        hdnRejectReasonFilter.Value = "";
            //        BindRejectedSIPGrid(processId);
            //    }
            //}
        }


        protected void ddlFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList ddlFileName = GetFileNameDDL();

            //if (Request.QueryString["processId"] != null)
            //    processId = Int32.Parse(Request.QueryString["processId"].ToString());



            //if (ddlFileName != null)
            //{
            //    if (ddlFileName.SelectedIndex != 0)
            //    {   // Bind the Grid with Only Selected Values
            //        hdnFileNameFilter.Value = ddlFileName.SelectedValue;
            //        BindRejectedSIPGrid(processId);
            //    }
            //    else
            //    {   // Bind the Grid with Only All Values
            //        hdnFileNameFilter.Value = "";
            //        BindRejectedSIPGrid(processId);
            //    }
            //}
        }

        protected void ddlFolioNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList ddlFolioNumber = GetFolioNumberDDL();

            //if (Request.QueryString["processId"] != null)
            //    processId = Int32.Parse(Request.QueryString["processId"].ToString());



            //if (ddlFolioNumber != null)
            //{
            //    if (ddlFolioNumber.SelectedIndex != 0)
            //    {   // Bind the Grid with Only Selected Values
            //        hdnFolioFilter.Value = ddlFolioNumber.SelectedValue;
            //        BindRejectedSIPGrid(processId);
            //    }
            //    else
            //    {   // Bind the Grid with Only All Values
            //        hdnFolioFilter.Value = "";
            //        BindRejectedSIPGrid(processId);
            //    }
            //}
        }

        protected void ddlSchemeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList ddlSchemeName = GetSchemeNameDDL();

            //if (Request.QueryString["processId"] != null)
            //    processId = Int32.Parse(Request.QueryString["processId"].ToString());


            //if (ddlSchemeName != null)
            //{
            //    if (ddlSchemeName.SelectedIndex != 0)
            //    {   // Bind the Grid with Only Selected Values
            //        hdnSchemeNameFilter.Value = ddlSchemeName.SelectedValue;
            //        BindRejectedSIPGrid(processId);
            //    }
            //    else
            //    {   // Bind the Grid with Only All Values
            //        hdnSchemeNameFilter.Value = "";
            //        BindRejectedSIPGrid(processId);
            //    }
            //}
        }
        private DropDownList GetRejectReasonDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvSIPReject.FindControl("ddlRejectReason") != null)
            {
                ddl = (DropDownList)gvSIPReject.FindControl("ddlRejectReason");
            }
            return ddl;
        }


        private void BindProcessId(DataTable dtProcessId)
        {
            Dictionary<string, string> genDictPanNum = new Dictionary<string, string>();
            if (dtProcessId.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtProcessId.Rows)
                {
                    genDictPanNum.Add(dr[0].ToString(), dr[0].ToString());
                }

                DropDownList ddlProcessId = GetProcessIdDDL();
                if (ddlProcessId != null)
                {
                    ddlProcessId.DataSource = genDictPanNum;
                    ddlProcessId.DataTextField = "Key";
                    ddlProcessId.DataValueField = "Value";
                    ddlProcessId.DataBind();
                    ddlProcessId.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnProcessIdFilter.Value != "")
                {
                    ddlProcessId.SelectedValue = hdnProcessIdFilter.Value.ToString().Trim();
                }
            }
        }

        private DropDownList GetProcessIdDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvSIPReject.FindControl("ddlProcessId") != null)
            {
                ddl = (DropDownList)gvSIPReject.FindControl("ddlProcessId");
            }
            return ddl;
        }

        private void BindFolioNumber(DataTable dtFolioNumber)
        {
            Dictionary<string, string> genDictFolioNumber = new Dictionary<string, string>();
            if (dtFolioNumber.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtFolioNumber.Rows)
                {
                    genDictFolioNumber.Add(dr[0].ToString(), dr[0].ToString());
                }

                DropDownList ddlFolioNumber = GetFolioNumberDDL();
                if (ddlFolioNumber != null)
                {
                    ddlFolioNumber.DataSource = genDictFolioNumber;
                    ddlFolioNumber.DataTextField = "Key";
                    ddlFolioNumber.DataValueField = "Value";
                    ddlFolioNumber.DataBind();
                    ddlFolioNumber.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnFolioFilter.Value != "")
                {
                    ddlFolioNumber.SelectedValue = hdnFolioFilter.Value.ToString();
                }
            }
        }

        //Methods with respect to Transaction Type Filter
        protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadComboBox dropdown = sender as RadComboBox;

            if (Request.QueryString["processId"] != null)
                processId = Int32.Parse(Request.QueryString["processId"].ToString());

            ViewState["CMFSCS_SystematicCode"] = dropdown.SelectedValue;
            if (ViewState["CMFSCS_SystematicCode"] != "")
            {
                GridColumn column = gvSIPReject.MasterTableView.GetColumnSafe("CMFSCS_SystematicCode");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvSIPReject.CurrentPageIndex = 0;
                gvSIPReject.MasterTableView.Rebind();

            }
            else
            {
                GridColumn column = gvSIPReject.MasterTableView.GetColumnSafe("CMFSCS_SystematicCode");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvSIPReject.CurrentPageIndex = 0;
                gvSIPReject.MasterTableView.Rebind();

            }
            //DropDownList ddlTransactionType = GetTransactionTypeDDL();

            //if (Request.QueryString["processId"] != null)
            //    processId = Int32.Parse(Request.QueryString["processId"].ToString());



            //if (ddlTransactionType != null)
            //{
            //    if (ddlTransactionType.SelectedIndex != 0)
            //    {   // Bind the Grid with Only Selected Values
            //        hdnTransactionTypeFilter.Value = ddlTransactionType.SelectedValue;
            //        BindRejectedSIPGrid(processId);
            //    }
            //    else
            //    {   // Bind the Grid with Only All Values
            //        hdnTransactionTypeFilter.Value = "";
            //        BindRejectedSIPGrid(processId);
            //    }
            //}
        }


        private DropDownList GetFolioNumberDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvSIPReject.FindControl("ddlFolioNumber") != null)
            {
                ddl = (DropDownList)gvSIPReject.FindControl("ddlFolioNumber");
            }
            return ddl;
        }

        private void BindInvName(DataTable dtInvName)
        {
            Dictionary<string, string> genDictInvName = new Dictionary<string, string>();
            if (dtInvName.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtInvName.Rows)
                {
                    genDictInvName.Add(dr[0].ToString(), dr[0].ToString());
                }

                DropDownList ddlInvName = GetInvNameDDL();
                if (ddlInvName != null)
                {
                    ddlInvName.DataSource = genDictInvName;
                    ddlInvName.DataTextField = "Key";
                    ddlInvName.DataValueField = "Value";
                    ddlInvName.DataBind();
                    ddlInvName.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnInvNameFilter.Value != "")
                {
                    ddlInvName.SelectedValue = hdnInvNameFilter.Value.ToString();
                }
            }
        }

        private DropDownList GetInvNameDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvSIPReject.FindControl("ddlInvName") != null)
            {
                ddl = (DropDownList)gvSIPReject.FindControl("ddlInvName");
            }
            return ddl;
        }


        private void BindSchemeName(DataTable dtSchemeName)
        {
            Dictionary<string, string> genDictSchemeName = new Dictionary<string, string>();
            if (dtSchemeName.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtSchemeName.Rows)
                {
                    //genDictSchemeName.Add(dr[0].ToString(), dr[1].ToString());
                }

                DropDownList ddlSchemeName = GetSchemeNameDDL();
                if (ddlSchemeName != null)
                {
                    ddlSchemeName.DataSource = genDictSchemeName;
                    ddlSchemeName.DataTextField = "Key";
                    ddlSchemeName.DataValueField = "Value";
                    ddlSchemeName.DataBind();
                    ddlSchemeName.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnSchemeNameFilter.Value != "")
                {
                    ddlSchemeName.SelectedValue = hdnSchemeNameFilter.Value.ToString();
                }
            }
        }


        private DropDownList GetSchemeNameDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvSIPReject.FindControl("ddlSchemeName") != null)
            {
                ddl = (DropDownList)gvSIPReject.FindControl("ddlSchemeName");
            }
            return ddl;
        }


        private void BindTransactionType(DataTable dtTransactionType)
        {
            Dictionary<string, string> genDictTransactionType = new Dictionary<string, string>();
            if (dtTransactionType.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtTransactionType.Rows)
                {
                    genDictTransactionType.Add(dr[0].ToString(), dr[0].ToString());
                }

                DropDownList ddlTransationType = GetTransactionTypeDDL();
                if (ddlTransationType != null)
                {
                    ddlTransationType.DataSource = genDictTransactionType;
                    ddlTransationType.DataTextField = "Key";
                    ddlTransationType.DataValueField = "Value";
                    ddlTransationType.DataBind();
                    ddlTransationType.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnTransactionTypeFilter.Value != "")
                {
                    ddlTransationType.SelectedValue = hdnTransactionTypeFilter.Value.ToString();
                }
            }
        }

        private DropDownList GetTransactionTypeDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvSIPReject.FindControl("ddlTransactionType") != null)
            {
                ddl = (DropDownList)gvSIPReject.FindControl("ddlTransactionType");
            }
            return ddl;
        }

        private void BindFileName(DataTable dtFileName)
        {
            Dictionary<string, string> genDictFileName = new Dictionary<string, string>();
            if (dtFileName.Rows.Count > 0)
            {
                // Get the Reject Reason Codes Available into Generic Dictionary
                foreach (DataRow dr in dtFileName.Rows)
                {
                    genDictFileName.Add(dr[0].ToString(), dr[0].ToString());
                }

                DropDownList ddlFileName = GetFileNameDDL();
                if (ddlFileName != null)
                {
                    ddlFileName.DataSource = genDictFileName;
                    ddlFileName.DataTextField = "Key";
                    ddlFileName.DataValueField = "Value";
                    ddlFileName.DataBind();
                    ddlFileName.Items.Insert(0, new ListItem("Select", "Select"));
                }

                if (hdnFileNameFilter.Value != "")
                {
                    ddlFileName.SelectedValue = hdnFileNameFilter.Value.ToString();
                }
            }
        }

        private DropDownList GetFileNameDDL()
        {
            DropDownList ddl = new DropDownList();
            if ((DropDownList)gvSIPReject.FindControl("ddlFileName") != null)
            {
                ddl = (DropDownList)gvSIPReject.FindControl("ddlFileName");
            }
            return ddl;
        }

        protected void btnReprocess_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(5000);
            bool blResult = false;
            uploadsCommonBo = new UploadCommonBo();
            UploadProcessLogVo processlogVo = new UploadProcessLogVo();
            string error = "";
            int processIdReprocessAll = 0;


            if (Request.QueryString["processId"] != null)
            {
                processId = Int32.Parse(Request.QueryString["processId"].ToString());
              //  processlogVo = uploadsCommonBo.GetProcessLogInfo(processId);

                blResult = MFWERPSIPTransactionInsertion(processId);

            }

            else
            {
                #region removed coz it was selecting all the processids
                //DataSet ds = uploadsCommonBo.GetSIPUploadRejectDistinctProcessIdForAdviser(adviserVo.advisorId);
                #endregion

                string StagingID = string.Empty;

                foreach (GridDataItem gvr in this.gvSIPReject.Items)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        StagingID += Convert.ToString(gvSIPReject.MasterTableView.DataKeyValues[gvr.ItemIndex]["WUPL_ProcessId"]) + "~";

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
                       // processlogVo = uploadsCommonBo.GetProcessLogInfo(processIdReprocessAll);

                        blResult = MFWERPSIPTransactionInsertion(processIdReprocessAll);

                        if (blResult == false)
                        {
                            error = error + "Error when reprocessing for the processid:" + processIdReprocessAll + ";";
                        }
                    }
                }

            }
            if (blResult == true)
            // if (error == "")
            {
                BindRejectedSIPGrid(processId);
                NeedSource();
                gvSIPReject.MasterTableView.Rebind();
                msgReprocessComplete.Visible = true;
                msgDelete.Visible = false;

            }
            else
            {
                // Failure Message

                msgDelete.Visible = false;
                //trErrorMessage.Visible = true;
                msgReprocessincomplete.Visible = true;
                //lblError.Text = "ErrorStatus:" + error;
            }


        }


        private bool MFWERPSIPTransactionInsertion(int UploadProcessId)
        {
            bool blResult = false;
            processlogVo = new UploadProcessLogVo();
            uploadsCommonBo = new UploadCommonBo();
            CamsUploadsBo camsUploadsBo = new CamsUploadsBo();
            bool camsSIPCommonStagingChk = false;
            bool camsSIPCommonStagingToWERP = false;
            bool updateProcessLog = false;
            string packagePath;
            string fileType="CA";


            processlogVo = uploadsCommonBo.GetProcessLogInfo(UploadProcessId);
            if (processlogVo.FileTypeId == 20)
            {
                fileType = "CA";
            }
            else if (processlogVo.FileTypeId == 26)
            {
                fileType = "WPT";
            }
            else if (processlogVo.FileTypeId == 27)
            {
                fileType = "KA";
            }
            packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingCheck.dtsx");

            camsSIPCommonStagingChk = camsUploadsBo.CamsSIPCommonStagingChk(UploadProcessId, packagePath, configPath, fileType);
            processlogVo.NoOfTransactionInserted = uploadsCommonBo.GetUploadSystematicInsertCount(UploadProcessId, fileType);

            updateProcessLog = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
            if (camsSIPCommonStagingChk)
            {
                packagePath = Server.MapPath("\\UploadPackages\\CAMSSystematicUploadPackageNew\\CAMSSystematicUploadPackageNew\\UploadSIPCommonStagingToWERP.dtsx");
                camsSIPCommonStagingToWERP = camsUploadsBo.CamsSIPCommonStagingToWERP(UploadProcessId, packagePath, configPath);

                if (camsSIPCommonStagingToWERP)
                {
                    processlogVo.IsInsertionToWERPComplete = 1;
                    processlogVo.EndTime = DateTime.Now;
                    processlogVo.NoOfRejectedRecords = uploadsCommonBo.GetUploadSystematicRejectCount(UploadProcessId, fileType);
                    blResult = uploadsCommonBo.UpdateUploadProcessLog(processlogVo);
                }
            }
            return blResult;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (GridDataItem gvr in gvSIPReject.Items)
            {
                if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
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
                CustomerSIPTransactionDelete();
                NeedSource();
                gvSIPReject.MasterTableView.Rebind();
                msgReprocessComplete.Visible = false;
                msgReprocessincomplete.Visible = false;
                msgDelete.Visible = true;

            }

        }


        private void CustomerSIPTransactionDelete()
        {
            // int i = 0;

            foreach (GridDataItem gvr in this.gvSIPReject.Items)
            {
                if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                {
                    int StagingID = int.Parse(gvSIPReject.MasterTableView.DataKeyValues[gvr.ItemIndex]["CMFSCS_ID"].ToString());
                    //i = i + 1;
                    uploadsCommonBo.DeleteMFSIPTransactionStaging(StagingID);
                    //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RejectedSystematicTransactionStaging','login');", true);

                }

            }
            BindRejectedSIPGrid(processId);
        }

        protected void LinkInputRejects_Click(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RejectedSystematicTransactionInputRejects','processId=" + processId + "');", true);

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RejectedSystematicTransactionInputRejects", "loadcontrol('RejectedSystematicTransactionInputRejects','processId=" + processId + " ');", true);

        }

        protected void gvSIPReject_PreRender(object sender, EventArgs e)
        {
            if (gvSIPReject.MasterTableView.FilterExpression != string.Empty)
            {
                RefreshCombos();
            }
        }

        protected void RefreshCombos()
        {
            dsRejectedSIP = (DataSet)Cache["RejectedSIPDetails" + adviserId.ToString()];
            DataTable dtRejectedSIP = new DataTable();
            dtRejectedSIP = dsRejectedSIP.Tables[0];
            DataView view = new DataView(dtRejectedSIP);
            DataTable distinctValues = view.ToTable();
            DataRow[] rows = distinctValues.Select(gvSIPReject.MasterTableView.FilterExpression.ToString());
            gvSIPReject.MasterTableView.Rebind();
        }
        protected void btnViewSIP_Click(object sender, EventArgs e)
        {
            if (adviserId == 1000)
            {
                if (ddlAdviser.SelectedIndex == 0)
                {
                    Response.Write("<script>alert('Please select an adviser');</script>");
                }

                else
                {

                    // if (!string.IsNullOrEmpty(txtFromSIP.SelectedDate.ToString()))
                    if (txtFromSIP.SelectedDate != null)
                        fromDate = DateTime.Parse(txtFromSIP.SelectedDate.ToString());
                    if (txtToSIP.SelectedDate != null)
                        toDate = DateTime.Parse(txtToSIP.SelectedDate.ToString());

                    BindRejectedSIPGrid(processId);
                    msgDelete.Visible = false;
                    msgReprocessComplete.Visible = false;
                    msgReprocessincomplete.Visible = false;
                    ViewState.Remove("WRR_RejectReasonCode");
                    ViewState.Remove("CMFSCS_SystematicCode");
                }
            }

            else
            {

                // if (!string.IsNullOrEmpty(txtFromSIP.SelectedDate.ToString()))
                if (txtFromSIP.SelectedDate != null)
                    fromDate = DateTime.Parse(txtFromSIP.SelectedDate.ToString());
                if (txtToSIP.SelectedDate != null)
                    toDate = DateTime.Parse(txtToSIP.SelectedDate.ToString());

                BindRejectedSIPGrid(processId);
                msgDelete.Visible = false;
                msgReprocessComplete.Visible = false;
                msgReprocessincomplete.Visible = false;
                ViewState.Remove("WRR_RejectReasonCode");
                ViewState.Remove("CMFSCS_SystematicCode");
            }
        }
        protected void rcbContinents1_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            if (ViewState["WRR_RejectReasonCode"] != null)
            {
                Combo.SelectedValue = ViewState["WRR_RejectReasonCode"].ToString();
            }
        }
        protected void rcbContinents2_PreRender(object sender, EventArgs e)
        { }
        protected void rcbContinents3_PreRender(object sender, EventArgs e)
        {
        }
        protected void rcbContinents4_PreRender(object sender, EventArgs e)
        { }
        protected void rcbContinents5_PreRender(object sender, EventArgs e)
        { }
        protected void rcbContinents6_PreRender(object sender, EventArgs e)
        { }
        protected void rcbContinents7_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            if (ViewState["CMFSCS_SystematicCode"] != null)
            {
                Combo.SelectedValue = ViewState["CMFSCS_SystematicCode"].ToString();
            }
        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvSIPReject.ExportSettings.OpenInNewWindow = true;
            gvSIPReject.ExportSettings.IgnorePaging = true;
            gvSIPReject.ExportSettings.HideStructureColumns = true;
            gvSIPReject.ExportSettings.ExportOnlyData = true;
            gvSIPReject.ExportSettings.FileName = "Rejected SIP Details";
            gvSIPReject.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvSIPReject.MasterTableView.ExportToExcel();
        }
    }
}
