using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using BoCommon;
using VoUser;
using WealthERP.Base;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using VoCustomerProfiling;
using BoCustomerProfiling;
using BoOnlineOrderManagement;
using VoOps;
using BoOps;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineAdviserSIPSummaryBooks : System.Web.UI.UserControl
    {
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        OnlineOrderMISBo OnlineOrderMISBo = new OnlineOrderMISBo();
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        AdvisorVo advisorVo;
        CustomerVo customerVO = new CustomerVo();
        MFOrderBo mforderBo = new MFOrderBo();
        MFOrderVo mforderVo = new MFOrderVo();
        OrderVo orderVo = new OrderVo();
        UserVo userVo;
        string userType;
        int customerId = 0;
        DateTime fromDate;
        DateTime toDate;
        int searchType = 0;
        int statusType = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            customerVO = (CustomerVo)Session["customerVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();
            //customerId = customerVO.CustomerId;
            BindAmc();
            BindOrderStatus();
            if (!Page.IsPostBack)
            {
                BindAmc();
                BindOrderStatus();
                fromDate = DateTime.Now;
                txtFrom.SelectedDate = fromDate.Date;
                txtTo.SelectedDate = DateTime.Now;
                tdStatusType.Visible = false;
                ddlSIP.Visible = false;
                Label4.Visible = false;
                if (Request.QueryString["SIPBook"] != null)
                {
                    string r = Request.QueryString["SIPBook"].ToString();
                }
            }
        }
        protected void ddlSearchtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSearchtype.SelectedValue == "5")
            {
                tdStatusType.Visible = true;
                ddlStatus.Visible = true;
                tdlblFromDate.Visible = false;
                tdTxtFromDate.Visible = false;
                tdlblToDate.Visible = false;
                tdTxtToDate.Visible = false;
               
            }
            else
            {
                tdStatusType.Visible = false;
                ddlStatus.Visible = false;
                tdlblFromDate.Visible = true;
                tdTxtFromDate.Visible = true;
                tdlblToDate.Visible = true;
                tdTxtToDate.Visible = true;
            }
        }
        protected void ddlMode_OnSelectedIndexChanged(object Sender, EventArgs e)
        {
            if (ddlMode.SelectedValue == "0")
            {
                ddlSIP.Visible = false;
                Label4.Visible = false;
            }
            else
            {
                ddlSIP.Visible = true;
                Label4.Visible = true;
            }
        }

        /// <summary>
        /// get AMC
        /// </summary>
        protected void BindAmc()
        {
            ddlAMCCode.Items.Clear();
            if (ddlAMCCode.SelectedIndex == 0) return;

            DataTable dtAmc = commonLookupBo.GetProdAmc(0, true);
            if (dtAmc == null) return;

            if (dtAmc.Rows.Count > 0)
            {
                ddlAMCCode.DataSource = dtAmc;
                ddlAMCCode.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAMCCode.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAMCCode.DataBind();
            }
            //DataSet ds = new DataSet();
            //DataTable dtAmc = new DataTable();
            //ds = OnlineMFOrderBo.GetSIPAmcDetails(customerId);
            //dtAmc = ds.Tables[0];
            //if (dtAmc.Rows.Count > 0)
            //{
            //    ddlAMCCode.DataSource = dtAmc;
            //    ddlAMCCode.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
            //    ddlAMCCode.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
            //    ddlAMCCode.DataBind();
            //    //BindFolioNumber(int.Parse(ddlAmc.SelectedValue));
            //}
            ddlAMCCode.Items.Insert(0, new ListItem("All", "0"));
        }

        /// <summary>
        /// Get Bind Orderstatus
        /// </summary>
        private void BindOrderStatus()
        {
            //ddlOrderstatus.Items.Clear();
            //DataSet dsOrderStatus;
            //DataTable dtOrderStatus;
            //dsOrderStatus = OnlineMFOrderBo.GetOrderStatus();
            //dtOrderStatus = dsOrderStatus.Tables[0];
            //if (dtOrderStatus.Rows.Count > 0)
            //{
            //    ddlOrderstatus.DataSource = dtOrderStatus;
            //    ddlOrderstatus.DataTextField = dtOrderStatus.Columns["WOS_OrderStep"].ToString();
            //    ddlOrderstatus.DataValueField = dtOrderStatus.Columns["WOS_OrderStepCode"].ToString();
            //    ddlOrderstatus.DataBind();
            //}
            //ddlOrderstatus.Items.Insert(0, new ListItem("All", "0"));
        }


        protected void btnViewOrder_Click(object sender, EventArgs e)
        {

            SetParameter();
            BindSIPSummaryBook();

        }

        /// <summary>
        /// Get Folio Account for Customer
        /// </summary>
        private void BindFolioAccount()
        {
            //    ddlAccount.Items.Clear();
            //    DataSet dsFolioAccount;
            //    DataTable dtFolioAccount;
            //    dsFolioAccount = OnlineMFOrderBo.GetFolioAccount(customerId);
            //    dtFolioAccount = dsFolioAccount.Tables[0];
            //    if (dtFolioAccount.Rows.Count > 0)
            //    {
            //        ddlAccount.DataSource = dsFolioAccount.Tables[0];
            //        ddlAccount.DataTextField = dtFolioAccount.Columns["CMFA_FolioNum"].ToString();
            //        ddlAccount.DataValueField = dtFolioAccount.Columns["CMFA_AccountId"].ToString();
            //        ddlAccount.DataBind();
            //    }
            //    ddlAccount.Items.Insert(0, new ListItem("All", "0"));
        }
        /// <summary>
        /// Get Order Book MIS
        /// </summary>
        protected void BindSIPSummaryBook()
        {
            DataSet dsSIPBookMIS = new DataSet();
            DataTable dtSIPBookMIS = new DataTable();

            searchType = Convert.ToInt32(ddlSearchtype.SelectedValue.ToString());
            if (searchType == 5)
            {
                statusType = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
            }
            if (txtFrom.SelectedDate != null)
                fromDate = DateTime.Parse(txtFrom.SelectedDate.ToString());
            if (txtTo.SelectedDate != null)
                toDate = DateTime.Parse(txtTo.SelectedDate.ToString());
            dsSIPBookMIS = OnlineOrderMISBo.GetSIPSummaryBookMIS(advisorVo.advisorId, int.Parse(hdnAmc.Value), fromDate, toDate, searchType, statusType, ddlSystematicType.SelectedValue);
            dtSIPBookMIS = dsSIPBookMIS.Tables[0];
            dtSIPBookMIS = createSIPOrderBook(dsSIPBookMIS);
            if (dtSIPBookMIS.Rows.Count > 0)
            {
                if (Cache["SIPSumList" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("SIPSumList" + advisorVo.advisorId, dtSIPBookMIS);
                }
                else
                {
                    Cache.Remove("SIPSumList" + advisorVo.advisorId);
                    Cache.Insert("SIPSumList" + advisorVo.advisorId, dtSIPBookMIS);
                }
                gvSIPSummaryBookMIS.DataSource = dtSIPBookMIS;
                gvSIPSummaryBookMIS.DataBind();
                gvSIPSummaryBookMIS.Visible = true;
                pnlSIPSumBook.Visible = true;
                btnExport.Visible = true;
                trNoRecords.Visible = false;
                divNoRecords.Visible = false;

            }
            else
            {
                gvSIPSummaryBookMIS.DataSource = dtSIPBookMIS;
                gvSIPSummaryBookMIS.DataBind();
                pnlSIPSumBook.Visible = true;
                trNoRecords.Visible = true;
                divNoRecords.Visible = true;
                btnExport.Visible = false;
            }
        }

        protected DataTable createSIPOrderBook(DataSet dsSIPOrderDetails)
        {
            DataTable dtFinalSIPOrderBook = new DataTable();
            dtFinalSIPOrderBook = CreateSIPBookDataTable();
            DataTable dtSIPDetails = dsSIPOrderDetails.Tables[0];
            DataTable dtOrderDetails = dsSIPOrderDetails.Tables[1];
            DataView dvSIPOrderDetails;
            DataRow drSIPOrderBook;
            DataTable dtAAAcceptedCount = dsSIPOrderDetails.Tables[2];
            DataView dvAAAcceptedCount;
            foreach (DataRow drSIP in dtSIPDetails.Rows)
            {

                drSIPOrderBook = dtFinalSIPOrderBook.NewRow();

                int sipDueCount = 0, inProcessCount = 0, acceptCount = 0, systemRejectCount = 0, rejectedCount = 0, executedCount = 0, otherCount = 0;
                //if (drSIP["CMFSS_SystematicSetupId"].ToString() == "8593")
                //{

                //}

                dvSIPOrderDetails = new DataView(dtOrderDetails, "CMFSS_SystematicSetupId=" + drSIP["CMFSS_SystematicSetupId"].ToString(), "CMFSS_SystematicSetupId", DataViewRowState.CurrentRows);
                if (drSIP["CMFSS_IsCanceled"].ToString() != "Cancelled" && Convert.ToInt16(drSIP["CMFSS_TotalInstallment"].ToString()) >= Convert.ToInt16(drSIP["CMFSS_CurrentInstallmentNumber"].ToString())
                    && Convert.ToDateTime(drSIP["CMFSS_EndDate"].ToString()) >= DateTime.Now)
                {
                    sipDueCount = (Convert.ToInt16(drSIP["CMFSS_TotalInstallment"].ToString())
                          - ((Convert.ToInt16(drSIP["CMFSS_CurrentInstallmentNumber"].ToString())) - 1));
                    //- dvSIPOrderDetails.ToTable().Rows.Count
                }
                else
                {
                    sipDueCount = 0;
                }
                //else
                //{
                //    sipDueCount = (Convert.ToInt16(drSIP["CMFSS_TotalInstallment"].ToString()) - dvSIPOrderDetails.ToTable().Rows.Count);
                //}
                //int.Parse(drSIP["CMFSS_InstallmentAccepted"].ToString())
                dvAAAcceptedCount = new DataView(dtAAAcceptedCount, "CMFSS_SystematicSetupId=" + drSIP["CMFSS_SystematicSetupId"].ToString(), "CMFSS_SystematicSetupId", DataViewRowState.CurrentRows);
                foreach (DataRow drOrder in dvSIPOrderDetails.ToTable().Rows)
                {
                    switch (drOrder["WOS_OrderStepCode"].ToString().TrimEnd())
                    {
                        case "AL":
                            inProcessCount = inProcessCount + 1;
                            break;
                        case "IP":
                            executedCount = executedCount + 1;
                            break;
                        case "RJ":
                            rejectedCount = rejectedCount + 1;
                            break;
                        case "PR":
                            acceptCount = acceptCount + 1;
                            break;
                        case "SJ":
                            systemRejectCount = systemRejectCount + 1;
                            break;
                        default:
                            break;
                    }
                }
                drSIPOrderBook["CMFSS_CreatedOn"] = DateTime.Parse(drSIP["CMFSS_CreatedOn"].ToString());
                drSIPOrderBook["CMFSS_ModifiedOn"] = DateTime.Parse(drSIP["CMFSS_ModifiedOn"].ToString());
                drSIPOrderBook["CMFSS_CreatedBy"] = drSIP["CMFSS_CreatedBy"];
                drSIPOrderBook["CMFSS_ModifiedBy"] = drSIP["CMFSS_ModifiedBy"];

                drSIPOrderBook["CMFSS_SystematicSetupId"] = drSIP["CMFSS_SystematicSetupId"];
                drSIPOrderBook["PA_AMCName"] = drSIP["PA_AMCName"].ToString();
                drSIPOrderBook["C_FirstName"] = drSIP["C_FirstName"].ToString();
                drSIPOrderBook["C_PANNum"] = drSIP["C_PANNum"].ToString();
                drSIPOrderBook["CustCode"] = drSIP["C_custcode"].ToString();

                drSIPOrderBook["PASP_SchemePlanName"] = drSIP["PASP_SchemePlanName"].ToString();
                drSIPOrderBook["PAISC_AssetInstrumentSubCategoryName"] = drSIP["PAISC_AssetInstrumentSubCategoryName"].ToString();
                drSIPOrderBook["CMFSS_DividendOption"] = drSIP["CMFSS_DividendOption"];
                drSIPOrderBook["CMFSS_Amount"] = drSIP["CMFSS_Amount"];
                drSIPOrderBook["XF_Frequency"] = drSIP["XF_Frequency"];
                drSIPOrderBook["CMFSS_StartDate"] = DateTime.Parse(drSIP["CMFSS_StartDate"].ToString());
                drSIPOrderBook["CMFSS_EndDate"] = DateTime.Parse(drSIP["CMFSS_EndDate"].ToString());
                if (!string.IsNullOrEmpty(drSIP["CMFSS_NextSIPDueDate"].ToString()))
                {
                    drSIPOrderBook["CMFSS_NextSIPDueDate"] = DateTime.Parse(drSIP["CMFSS_NextSIPDueDate"].ToString()).ToShortDateString();

                }
                else
                {
                    drSIPOrderBook["CMFSS_NextSIPDueDate"] = "";
                }
                drSIPOrderBook["CMFSS_TotalInstallment"] = drSIP["CMFSS_TotalInstallment"];
                drSIPOrderBook["CMFSS_CurrentInstallmentNumber"] = drSIP["CMFSS_CurrentInstallmentNumber"];
                drSIPOrderBook["CMFA_FolioNum"] = drSIP["CMFA_FolioNum"];
                drSIPOrderBook["Channel"] = drSIP["Channel"];
                drSIPOrderBook["CMFSS_IsCanceled"] = drSIP["CMFSS_IsCanceled"];
                drSIPOrderBook["CMFSS_Remark"] = drSIP["CMFSS_Remark"];
                drSIPOrderBook["SIPDueCount"] = sipDueCount;
                //if (int.Parse(drSIP["CMFSS_IsSourceAA"].ToString()) == 1)
                //{
                //    inProcessCount = (Convert.ToInt16(drSIP["CMFSS_TotalInstallment"].ToString())
                //          - ((Convert.ToInt16(drSIP["CMFSS_InstallmentOther"].ToString())) - 1)) - dvSIPOrderDetails.ToTable().Rows.Count;
                //}
                //else
                //{
                //    inProcessCount = (Convert.ToInt16(drSIP["CMFSS_TotalInstallment"].ToString()) - dvSIPOrderDetails.ToTable().Rows.Count);
                //}
                drSIPOrderBook["InProcessCount"] = inProcessCount;
                drSIPOrderBook["CMFSS_InstallmentOther"] = drSIP["CMFSS_InstallmentOther"];
                if (int.Parse(drSIP["CMFSS_IsSourceAA"].ToString()) == 1)
                {
                    drSIPOrderBook["AcceptCount"] = int.Parse(drSIP["CMFSS_InstallmentAccepted"].ToString()) + acceptCount;
                }
                else
                {
                    drSIPOrderBook["AcceptCount"] = acceptCount;
                }
                drSIPOrderBook["PASP_SchemePlanCode"] = drSIP["PASP_SchemePlanCode"];
                drSIPOrderBook["CMFA_AccountId"] = drSIP["CMFA_AccountId"];
                drSIPOrderBook["SystemRejectCount"] = systemRejectCount;
                drSIPOrderBook["RejectedCount"] = rejectedCount;
                drSIPOrderBook["ExecutedCount"] = executedCount;
                drSIPOrderBook["CMFSS_IsSourceAA"] = drSIP["CMFSS_IsSourceAA"];
                drSIPOrderBook["C_CustomerId"] = drSIP["C_CustomerId"];
                drSIPOrderBook["U_UMId"] = drSIP["U_UMId"];
                drSIPOrderBook["CMFSS_StartDate"] = DateTime.Parse(drSIP["CMFSS_StartDate"].ToString());
                if (!string.IsNullOrEmpty(drSIP["CMFSS_RegistrationDate"].ToString()))
                {
                    drSIPOrderBook["CMFSS_RegistrationDate"] = DateTime.Parse(drSIP["CMFSS_RegistrationDate"].ToString()).ToShortDateString();

                }
                drSIPOrderBook["CMFSS_CancelBy"] = drSIP["CMFSS_CancelBy"];
                if (!string.IsNullOrEmpty(drSIP["CMFSS_CancelDate"].ToString()))
                {
                    drSIPOrderBook["CMFSS_CancelDate"] = DateTime.Parse(drSIP["CMFSS_CancelDate"].ToString());
                }
                //else
                //{
                //    drSIPOrderBook["CMFSS_CancelDate"] = null;
                //}
                drSIPOrderBook["C_Mobile1"] = drSIP["C_Mobile1"].ToString();
                drSIPOrderBook["C_Email"] = drSIP["C_Email"];
                drSIPOrderBook["Unit"] = drSIP["Unit"];
                dtFinalSIPOrderBook.Rows.Add(drSIPOrderBook);
            }

            return dtFinalSIPOrderBook;

        }

        protected DataTable CreateSIPBookDataTable()
        {
            DataTable dtSIPOrderBook = new DataTable();
            dtSIPOrderBook.Columns.Add("CMFSS_CreatedOn", typeof(DateTime));
            dtSIPOrderBook.Columns.Add("CMFSS_SystematicSetupId");
            dtSIPOrderBook.Columns.Add("C_FirstName");
            dtSIPOrderBook.Columns.Add("C_PANNum");
            dtSIPOrderBook.Columns.Add("PA_AMCName");
            dtSIPOrderBook.Columns.Add("PASP_SchemePlanName");
            dtSIPOrderBook.Columns.Add("PAISC_AssetInstrumentSubCategoryName");
            dtSIPOrderBook.Columns.Add("CMFSS_DividendOption");
            dtSIPOrderBook.Columns.Add("CMFSS_Amount", typeof(double));
            dtSIPOrderBook.Columns.Add("XF_Frequency");
            dtSIPOrderBook.Columns.Add("CMFSS_StartDate", typeof(DateTime));
            dtSIPOrderBook.Columns.Add("CMFSS_EndDate", typeof(DateTime));
            dtSIPOrderBook.Columns.Add("CMFSS_NextSIPDueDate");
            dtSIPOrderBook.Columns.Add("CMFSS_TotalInstallment");
            dtSIPOrderBook.Columns.Add("CMFSS_CurrentInstallmentNumber");
            dtSIPOrderBook.Columns.Add("CMFA_FolioNum");
            dtSIPOrderBook.Columns.Add("Channel");
            dtSIPOrderBook.Columns.Add("CMFSS_IsCanceled");
            dtSIPOrderBook.Columns.Add("CMFSS_Remark");
            dtSIPOrderBook.Columns.Add("SIPDueCount");
            dtSIPOrderBook.Columns.Add("InProcessCount");
            dtSIPOrderBook.Columns.Add("AcceptCount");
            dtSIPOrderBook.Columns.Add("SystemRejectCount");
            dtSIPOrderBook.Columns.Add("RejectedCount");
            dtSIPOrderBook.Columns.Add("CMFSS_InstallmentOther");
            dtSIPOrderBook.Columns.Add("CMFSS_IsSourceAA");
            dtSIPOrderBook.Columns.Add("CMFSS_InstallmentAccepted");
            dtSIPOrderBook.Columns.Add("ExecutedCount");
            dtSIPOrderBook.Columns.Add("PASP_SchemePlanCode");
            dtSIPOrderBook.Columns.Add("CMFA_AccountId");
            dtSIPOrderBook.Columns.Add("C_CustomerId");
            dtSIPOrderBook.Columns.Add("CMFSS_CreatedBy");
            dtSIPOrderBook.Columns.Add("CMFSS_ModifiedBy");
            dtSIPOrderBook.Columns.Add("CMFSS_ModifiedOn");
            dtSIPOrderBook.Columns.Add("U_UMId");
            dtSIPOrderBook.Columns.Add("CMFSS_RegistrationDate", typeof(DateTime));
            dtSIPOrderBook.Columns.Add("CMFSS_CancelDate", typeof(DateTime));
            dtSIPOrderBook.Columns.Add("CMFSS_CancelBy");
            dtSIPOrderBook.Columns.Add("CustCode");
            dtSIPOrderBook.Columns.Add("C_Mobile1");
            dtSIPOrderBook.Columns.Add("C_Email");
            dtSIPOrderBook.Columns.Add("Unit", typeof(double));
            return dtSIPOrderBook;

        }



        private void SetParameter()
        {
            //if (ddlOrderstatus.SelectedIndex != 0)
            //{
            //    hdnOrderStatus.Value = ddlOrderstatus.SelectedValue;
            //    ViewState["OrderstatusDropDown"] = hdnOrderStatus.Value;
            //}
            //else
            //{
            //    hdnOrderStatus.Value = "0";
            //}
            if (ddlAMCCode.SelectedIndex != 0)
            {
                hdnAmc.Value = ddlAMCCode.SelectedValue;
                ViewState["AMCDropDown"] = hdnAmc.Value;
            }
            else
            {
                hdnAmc.Value = "0";
            }
        }
        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            //RadComboBox ddlAction = (RadComboBox)sender;
            //GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            //int selectedRow = gvr.ItemIndex + 1;
            //string action = "";
            //string orderId = gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderId"].ToString();
            //string customerId = gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString();
            //string assetGroupCode = gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["PAG_AssetGroupCode"].ToString();
            //if (ddlAction.SelectedItem.Value.ToString() == "Edit")
            //{
            //    action = "Edit";
            //    if (assetGroupCode == "MF")
            //    {
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFOrderSIPTransType','strAction=" + ddlAction.SelectedItem.Value.ToString() + "&orderId=" + orderId + "&customerId=" + customerId + "');", true);
            //    }
        }
        protected void gvSIPSummaryBookMIS_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            gvSIPSummaryBookMIS.Visible = true;
            DataTable dtOrderBookMIS = new DataTable();
            dtOrderBookMIS = (DataTable)Cache["SIPSumList" + advisorVo.advisorId.ToString()];
            if (dtOrderBookMIS != null)
            {
                gvSIPSummaryBookMIS.DataSource = dtOrderBookMIS;
                gvSIPSummaryBookMIS.Visible = true;
            }

        }
        protected void btnExportFilteredData_OnClick(object sender, EventArgs e)
        {
            gvSIPSummaryBookMIS.ExportSettings.OpenInNewWindow = true;
            gvSIPSummaryBookMIS.ExportSettings.IgnorePaging = true;
            gvSIPSummaryBookMIS.ExportSettings.HideStructureColumns = true;
            gvSIPSummaryBookMIS.ExportSettings.ExportOnlyData = true;
            gvSIPSummaryBookMIS.ExportSettings.FileName = "SIP Summary Book Details";
            gvSIPSummaryBookMIS.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvSIPSummaryBookMIS.MasterTableView.ExportToExcel();
        }
        protected void gvSIPSummaryBookMIS_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gvr = (GridDataItem)e.Item;
                if (e.CommandName.ToString() != "Filter")
                {
                    if (e.CommandName.ToString() != "Sort")
                    {
                        if (e.CommandName.ToString() != "Page")
                        {
                            if (e.CommandName.ToString() != "ChangePageSize")
                            {

                                int selectedRow = gvr.ItemIndex + 1;
                                int systematicId = int.Parse(gvr.GetDataKeyValue("CMFSS_SystematicSetupId").ToString());
                                int AccountId = int.Parse(gvr.GetDataKeyValue("CMFA_AccountId").ToString());
                                int schemeplanCode = int.Parse(gvr.GetDataKeyValue("PASP_SchemePlanCode").ToString());
                                int IsSourceAA = int.Parse(gvr.GetDataKeyValue("CMFSS_IsSourceAA").ToString());
                                int customerId = int.Parse(gvr.GetDataKeyValue("C_CustomerId").ToString());
                                //int accept = int.Parse(gvr.GetDataKeyValue("AcceptCount").ToString());
                                if (e.CommandName == "Select")
                                {
                                    //  Response.Redirect("ControlHost.aspx?pageid=OnlineAdviserCustomerTransactionBook&systematicId=" + systematicId + "", false);
                                    Response.Redirect("ControlHost.aspx?pageid=OnlineAdviserCustomerSIPOrderBook&systematicId=" + systematicId + "&AccountId=" + AccountId + "&schemeplanCode=" + schemeplanCode + "&IsSourceAA=" + IsSourceAA + "&customerId=" + customerId + "", false);
                                }

                                // if (e.CommandName == "Accepted")
                                //{
                                //    Response.Redirect("ControlHost.aspx?pageid=OnlineAdviserCustomerTransctionBook&systematicId=" + systematicId + "&AccountId=" + AccountId + "&schemeplanCode=" + schemeplanCode + "&IsSourceAA=" + IsSourceAA + "&customerId=" + customerId + "", false);
                                //}
                                if (e.CommandName == "Accepted")
                                {
                                    Response.Redirect("ControlHost.aspx?pageid=OnlineAdviserCustomerSIPOrderBook&systematicId=" + systematicId + "&OrderStatus=PR", false);
                                }
                                else if (e.CommandName == "In Process")
                                {
                                    Response.Redirect("ControlHost.aspx?pageid=OnlineAdviserCustomerSIPOrderBook&systematicId=" + systematicId + "&OrderStatus=AL", false);
                                }
                                else if (e.CommandName == "Rejected")
                                {
                                    Response.Redirect("ControlHost.aspx?pageid=OnlineAdviserCustomerSIPOrderBook&systematicId=" + systematicId + "&OrderStatus=RJ", false);

                                }
                                else if (e.CommandName == "Executed")
                                {
                                    Response.Redirect("ControlHost.aspx?pageid=OnlineAdviserCustomerSIPOrderBook&systematicId=" + systematicId + "&OrderStatus=IP", false);

                                    //else if (e.CommandName == "In Process")
                                    //{
                                    //    Response.Redirect("ControlHost.aspx?pageid=OnlineAdviserCustomerTransactionBook&systematicId=" + systematicId + "&OrderStatus=AL", false);"&OrderStatus=PR", false);
                                    //}
                                    //else if (e.CommandName == "Rejected")
                                    //{
                                    //    Response.Redirect("ControlHost.aspx?pageid=OnlineAdviserCustomerTransactionBook&systematicId=" + systematicId + "&OrderStatus=RJ", false);

                                    //}
                                    //else if (e.CommandName == "Executed")
                                    //{
                                    //    Response.Redirect("ControlHost.aspx?pageid=OnlineAdviserCustomerTransactionBook&systematicId=" + systematicId + "&OrderStatus=IP", false);
                                    //}  
                                }
                            }
                        }
                    }
                }
            }
        }
        protected void gvSIPSummaryBookMIS_UpdateCommand(object source, GridCommandEventArgs e)
        {
            string strRemark = string.Empty;
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditableItem editItem = e.Item as GridEditableItem;
                TextBox txtRemark = (TextBox)e.Item.FindControl("txtRemark");
                strRemark = txtRemark.Text;
                LinkButton buttonEdit = editItem["editColumn"].Controls[0] as LinkButton;
                Int32 systematicId = Convert.ToInt32(gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CMFSS_SystematicSetupId"].ToString());
                //sr
                //Int32 accept = Convert.ToInt32(gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AcceptCount"].ToString());
                OnlineMFOrderBo.UpdateCnacleRegisterSIP(systematicId, 1, strRemark, userVo.UserId);
                SetParameter();
                BindSIPSummaryBook();
                buttonEdit.Enabled = false;

            }
        }

        protected void gvSIPSummaryBookMIS_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                string Iscancel = Convert.ToString(gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CMFSS_IsCanceled"]);
                int totalInstallment = Convert.ToInt32(gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CMFSS_TotalInstallment"].ToString());
                int currentInstallmentNumber = Convert.ToInt32(gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CMFSS_CurrentInstallmentNumber"].ToString());
                DateTime endDate = Convert.ToDateTime(gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CMFSS_EndDate"].ToString());
                LinkButton buttonEdit = dataItem["editColumn"].Controls[0] as LinkButton;
                DateTime currentTime = DateTime.Now;
                DateTime fixedTime = Convert.ToDateTime("08:35:00 AM");
                int compare = DateTime.Compare(currentTime, fixedTime);
                //DropDownList ddlSystematicType = (DropDownList)e.Item.FindControl("ddlSystematicType");

                if (Iscancel == "Cancelled" || totalInstallment == currentInstallmentNumber || endDate < DateTime.Now)
                {

                    buttonEdit.Enabled = false;
                }
                if (endDate == DateTime.Now)
                {
                    if (compare >= 0)
                    {
                        buttonEdit.Enabled = false;
                    }
                }

                if (ddlSystematicType.SelectedValue == "SWP")
                {
                    gvSIPSummaryBookMIS.MasterTableView.GetColumn("CMFSS_Amount").Visible = false;
                    gvSIPSummaryBookMIS.MasterTableView.GetColumn("Unit").Visible = true;
                }
                else
                {
                    gvSIPSummaryBookMIS.MasterTableView.GetColumn("CMFSS_Amount").Visible = true;
                    gvSIPSummaryBookMIS.MasterTableView.GetColumn("Unit").Visible = false;

                }
            }
        }

    }
}