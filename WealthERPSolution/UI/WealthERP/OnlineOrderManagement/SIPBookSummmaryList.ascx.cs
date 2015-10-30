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

namespace WealthERP.OnlineOrderManagement
{
    public partial class SIPBookSummmaryList : System.Web.UI.UserControl
    {

        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        AdvisorVo advisorVo;
        CustomerVo customerVO = new CustomerVo();
        MFOrderBo mforderBo = new MFOrderBo();
        MFOrderVo mforderVo = new MFOrderVo();
        OrderVo orderVo = new OrderVo();
        UserVo userVo;
        string userType;
        int customerId = 0;
        string systematicType;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVO = (CustomerVo)Session["customerVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();
            userVo = (UserVo)Session["UserVo"];
            customerId = customerVO.CustomerId;
            hdnsystamaticType.Value = "";
            //BindAmc();
            BindOrderStatus();
            RadInformation1.VisibleOnPageLoad = false;
            if (!Page.IsPostBack)
            {

                if (Request.QueryString["systematicType"] != null)
                {
                    hdnsystamaticType.Value = Request.QueryString["systematicType"].ToString();
                    BindAmc(hdnsystamaticType.Value);
                }
                if (Request.QueryString["systematicId"] != null)
                {
                    int systematicId = int.Parse(Request.QueryString["systematicId"].ToString());
                    int AmcCode = int.Parse(Request.QueryString["AmcCode"].ToString());
                    string orderStatus = Request.QueryString["OrderStatus"];
                    string systematictype = Request.QueryString["systematicType"].ToString();
                    hdnAmc.Value = AmcCode.ToString();
                    BindSIPSummaryBook();
                }
            }
        }

        /// <summary>
        /// get AMC
        /// </summary>
        protected void BindAmc(string systematicType)
        {
            ddlAMCCode.Items.Clear();
            DataSet ds = new DataSet();
            DataTable dtAmc = new DataTable();
            ds = OnlineMFOrderBo.GetSIPAmcDetails(customerId, hdnsystamaticType.Value);
            dtAmc = ds.Tables[0];
            if (dtAmc.Rows.Count > 0)
            {
                ddlAMCCode.DataSource = dtAmc;
                ddlAMCCode.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAMCCode.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAMCCode.DataBind();
                //BindFolioNumber(int.Parse(ddlAmc.SelectedValue));

            }
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

            dsSIPBookMIS = OnlineMFOrderBo.GetSIPSummaryBookMIS(customerId, int.Parse(hdnAmc.Value), hdnsystamaticType.Value);
            dtSIPBookMIS = dsSIPBookMIS.Tables[0];
            dtSIPBookMIS = createSIPOrderBook(dsSIPBookMIS);
            if (dtSIPBookMIS.Rows.Count > 0)
            {
                if (Cache["SIPSumList" + userVo.UserId] == null)
                {
                    Cache.Insert("SIPSumList" + userVo.UserId, dtSIPBookMIS);
                }
                else
                {
                    Cache.Remove("SIPSumList" + userVo.UserId);
                    Cache.Insert("SIPSumList" + userVo.UserId, dtSIPBookMIS);
                }
                gvSIPSummaryBookMIS.DataSource = dtSIPBookMIS;
                gvSIPSummaryBookMIS.DataBind();
                gvSIPSummaryBookMIS.Visible = true;
                //pnlSIPSumBook.Visible = true;
                btnExport.Visible = true;
                trNoRecords.Visible = false;
                divNoRecords.Visible = false;
                Div1.Visible = true;

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
            DataTable dtAAAcceptedCount = dsSIPOrderDetails.Tables[2];
            DataView dvSIPOrderDetails;
            DataView dvAAAcceptedCount;
            DataRow drSIPOrderBook;

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
                drSIPOrderBook["CMFSS_SystematicSetupId"] = drSIP["CMFSS_SystematicSetupId"];
                drSIPOrderBook["PA_AMCName"] = drSIP["PA_AMCName"].ToString();
                drSIPOrderBook["PASP_SchemePlanName"] = drSIP["PASP_SchemePlanName"].ToString();
                drSIPOrderBook["PAISC_AssetInstrumentSubCategoryName"] = drSIP["PAISC_AssetInstrumentSubCategoryName"].ToString();
                drSIPOrderBook["CMFSS_DividendOption"] = drSIP["CMFSS_DividendOption"];
                drSIPOrderBook["CMFSS_Amount"] = drSIP["CMFSS_Amount"];
                drSIPOrderBook["XF_Frequency"] = drSIP["XF_Frequency"];
                drSIPOrderBook["CMFSS_StartDate"] = DateTime.Parse(drSIP["CMFSS_StartDate"].ToString());
                drSIPOrderBook["CMFSS_EndDate"] = DateTime.Parse(drSIP["CMFSS_EndDate"].ToString());
                if (!string.IsNullOrEmpty(drSIP["CMFSS_NextSIPDueDate"].ToString()))
                {
                    drSIPOrderBook["CMFSS_NextSIPDueDate"] = DateTime.Parse(drSIP["CMFSS_NextSIPDueDate"].ToString());
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
                drSIPOrderBook["InProcessCount"] = inProcessCount;

                if (int.Parse(drSIP["CMFSS_IsSourceAA"].ToString()) == 1)
                {
                    //foreach (DataRow drAAAcceptedCount in dvAAAcceptedCount.ToTable().Rows)
                    //{

                    //if (int.Parse(drSIP["CMFSS_SystematicSetupId"].ToString()) == int.Parse(drAAAcceptedCount["CMFSS_SystematicSetupId"].ToString()))
                    if (dvAAAcceptedCount.ToTable().Rows.Count > 0)
                        acceptCount = Convert.ToInt16(dvAAAcceptedCount.ToTable().Rows[0]["Occurence"].ToString());
                    else
                        acceptCount = 0;
                    //int.Parse(drSIP["CMFSS_InstallmentAccepted"].ToString()) +
                    //}
                }


                if (drSIP["CMFSS_IsCanceled"].ToString() == "Cancelled" || Convert.ToDateTime(drSIP["CMFSS_EndDate"].ToString()) < DateTime.Now)
                {
                    otherCount = (Convert.ToInt16(drSIP["CMFSS_TotalInstallment"].ToString()) - (acceptCount + inProcessCount + executedCount + rejectedCount + sipDueCount));

                }
                else
                    otherCount = Convert.ToInt16(drSIP["CMFSS_InstallmentOther"].ToString());

                drSIPOrderBook["AcceptCount"] = acceptCount;
                drSIPOrderBook["CMFSS_InstallmentOther"] = otherCount;
                drSIPOrderBook["SystemRejectCount"] = systemRejectCount;
                drSIPOrderBook["RejectedCount"] = rejectedCount;
                drSIPOrderBook["ExecutedCount"] = executedCount;
                drSIPOrderBook["CMFA_AccountId"] = drSIP["CMFA_AccountId"];
                drSIPOrderBook["PASP_SchemePlanCode"] = drSIP["PASP_SchemePlanCode"];
                drSIPOrderBook["CMFSS_IsSourceAA"] = drSIP["CMFSS_IsSourceAA"];
                drSIPOrderBook["Unit"] = drSIP["Unit"];

                drSIPOrderBook["SchemeRating3Year"] = drSIP["PMFRD_Rating3Year"];
                drSIPOrderBook["SchemeRating5Year"] = drSIP["PMFRD_Rating5Year"];
                drSIPOrderBook["SchemeRating10Year"] = drSIP["PMFRD_Rating10Year"];

                drSIPOrderBook["SchemeReturn3Year"] = drSIP["PMFRD_Return3Year"];
                drSIPOrderBook["SchemeReturn5Year"] = drSIP["PMFRD_Return5Year"];
                drSIPOrderBook["SchemeReturn10Year"] = drSIP["PMFRD_Return10Year"];

                drSIPOrderBook["SchemeRisk3Year"] = drSIP["PMFRD_Risk3Year"];
                drSIPOrderBook["SchemeRisk5Year"] = drSIP["PMFRD_Risk5Year"];
                drSIPOrderBook["SchemeRisk10Year"] = drSIP["PMFRD_Risk10Year"];

                drSIPOrderBook["SchemeRatingOverall"] = drSIP["PMFRD_RatingOverall"];
                drSIPOrderBook["SchemeRatingSubscriptionExpiryDtae"] = drSIP["AVSD_ExpiryDtae"];
                if (DateTime.Parse(drSIP["PMFRD_RatingDate"].ToString()) != DateTime.Parse("01/01/1900 00:00:00"))
                    drSIPOrderBook["SchemeRatingDate"] = DateTime.Parse(drSIP["PMFRD_RatingDate"].ToString()).ToString("dd/MM/yyyy");
                dtFinalSIPOrderBook.Rows.Add(drSIPOrderBook);

            }
            return dtFinalSIPOrderBook;

        }

        protected DataTable CreateSIPBookDataTable()
        {
            DataTable dtSIPOrderBook = new DataTable();
            dtSIPOrderBook.Columns.Add("CMFSS_CreatedOn", typeof(DateTime));
            dtSIPOrderBook.Columns.Add("CMFSS_SystematicSetupId");
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
            dtSIPOrderBook.Columns.Add("CMFA_AccountId");
            dtSIPOrderBook.Columns.Add("PASP_SchemePlanCode");
            dtSIPOrderBook.Columns.Add("Unit", typeof(double));

            dtSIPOrderBook.Columns.Add("SchemeRating3Year");
            dtSIPOrderBook.Columns.Add("SchemeRating5Year");
            dtSIPOrderBook.Columns.Add("SchemeRating10Year");

            dtSIPOrderBook.Columns.Add("SchemeReturn3Year");
            dtSIPOrderBook.Columns.Add("SchemeReturn5Year");
            dtSIPOrderBook.Columns.Add("SchemeReturn10Year");

            dtSIPOrderBook.Columns.Add("SchemeRisk3Year");
            dtSIPOrderBook.Columns.Add("SchemeRisk5Year");
            dtSIPOrderBook.Columns.Add("SchemeRisk10Year");
            dtSIPOrderBook.Columns.Add("SchemeRatingOverall");
            dtSIPOrderBook.Columns.Add("SchemeRatingSubscriptionExpiryDtae");
            dtSIPOrderBook.Columns.Add("SchemeRatingDate");
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
            dtOrderBookMIS = (DataTable)Cache["SIPSumList" + userVo.UserId.ToString()];
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
            string orderStatus = string.Empty;
            if (e.Item is GridDataItem)
            {
                GridDataItem gvr = (GridDataItem)e.Item;
                RadGrid gvChildOrderBookDetails = (RadGrid)gvr.FindControl("gvChildOrderBookDetails");
                if (e.CommandName.ToString() != "Filter")
                {
                    if (e.CommandName.ToString() != "Sort")
                    {
                        if (e.CommandName.ToString() != "Page")
                        {
                            if (e.CommandName.ToString() != "ChangePageSize")
                            {
                                // GridDataItem gvr = (GridDataItem)e.Item;
                                int amccode = int.Parse(ddlAMCCode.SelectedValue);

                                int selectedRow = gvr.ItemIndex + 1;
                                int systematicId = int.Parse(gvr.GetDataKeyValue("CMFSS_SystematicSetupId").ToString());
                                int AccountId = int.Parse(gvr.GetDataKeyValue("CMFA_AccountId").ToString());
                                int schemeplanCode = int.Parse(gvr.GetDataKeyValue("PASP_SchemePlanCode").ToString());
                                int IsSourceAA = int.Parse(gvr.GetDataKeyValue("CMFSS_IsSourceAA").ToString());
                                int Amount = int.Parse(gvr.GetDataKeyValue("CMFSS_Amount").ToString());
                                DateTime SIPStartDate = Convert.ToDateTime(gvr.GetDataKeyValue("CMFSS_StartDate").ToString());
                                string systematictype = Request.QueryString["systematicType"].ToString();

                                if (e.CommandName == "Accepted")
                                {
                                    BindChildCridDetails(int.Parse(ddlAMCCode.SelectedValue), "PR", systematicId, gvChildOrderBookDetails);
                                    //if (Session["PageDefaultSetting"] != null)
                                    //{
                                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('CustomerTransactionBookList','?systematicId=" + systematicId + "&AccountId=" + AccountId + "&schemeplanCode=" + schemeplanCode + "&IsSourceAA=" + IsSourceAA + "&Amount=" + Amount + "&SIPStartDate=" + SIPStartDate + "');", true);
                                    //}
                                    //else
                                    //{
                                    //    Response.Redirect("ControlHost.aspx?pageid=CustomerTransactionBookList&systematicId=" + systematicId + "&AccountId=" + AccountId + "&schemeplanCode=" + schemeplanCode + "&IsSourceAA=" + IsSourceAA + "&Amount=" + Amount + "&SIPStartDate=" + SIPStartDate + "", false);
                                    //}

                                }
                                if (e.CommandName == "InProcess" | e.CommandName == "Rejected" | e.CommandName == "Executed")
                                {
                                    if (e.CommandName == "InProcess")
                                    {
                                        orderStatus = "AL";
                                    }
                                    else if (e.CommandName == "Rejected")
                                    {
                                        orderStatus = "RJ";
                                    }
                                    else if (e.CommandName == "Executed")
                                    {
                                        orderStatus = "IP";
                                    }
                                    BindChildCridDetails(int.Parse(ddlAMCCode.SelectedValue), orderStatus, systematicId, gvChildOrderBookDetails);

                                    //if (Session["PageDefaultSetting"] != null)
                                    //{
                                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('CustomerSIPBookList','?systematicId=" + systematicId + "&AmcCode=" + ddlAMCCode.SelectedValue + "&systematicType=" + systematictype + "&OrderStatus=" + orderStatus + "');", true);
                                    //}
                                    //else
                                    //{
                                    //    Response.Redirect("ControlHost.aspx?pageid=CustomerSIPBookList&systematicId=" + systematicId + "&AmcCode=" + ddlAMCCode.SelectedValue + "&systematicType=" + systematictype + "&OrderStatus=" + orderStatus + "", false);
                                    //    //Response.Redirect("ControlHost.aspx?pageid=CustomerSIPBookList&systematicId=" + systematicId + "&AccountId=" + AccountId + "&schemeplanCode=" + schemeplanCode + "&IsSourceAA=" + IsSourceAA + "&Amount=" + Amount + "&SIPStartDate=" + SIPStartDate + "", false);
                                    //    //Response.Redirect("ControlHost.aspx?pageid=CustomerSIPBookList&systematicId=" + systematicId + "&OrderStatus=" + orderStatus + "", false);
                                    //}

                                }
                            }
                        }
                    }
                }
            }
        }
        protected void btnDetails_OnClick(object sender, EventArgs e)

        {
           Button lnkOrderNo = (Button)sender;
            GridDataItem gdi;
            DataTable filldt = new DataTable();
            gdi = (GridDataItem)lnkOrderNo.NamingContainer;
            int selectedRow = gdi.ItemIndex + 1;
            RadGrid gvChildOrderBookDetails = (RadGrid)gdi.FindControl("gvChildOrderBookDetails");
            int systematicId = int.Parse((gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CMFSS_SystematicSetupId"].ToString()));
            string systematictype = Request.QueryString["systematicType"].ToString();
            BindChildCridDetails(int.Parse(ddlAMCCode.SelectedValue),null,systematicId,gvChildOrderBookDetails);
        }
        protected void BindChildCridDetails(int amcCode, string orderstatus, int systematicId, RadGrid gvChildOrderBookDetails)
        {
            DataSet dsSIPBookMIS = OnlineMFOrderBo.GetSIPBookMIS(customerId, amcCode, orderstatus, systematicId);
            if (gvChildOrderBookDetails.Visible == false)
            {
                gvChildOrderBookDetails.Visible = true;
                //btnDetails.Text = "-";
            }
            else if (gvChildOrderBookDetails.Visible == true)
            {
                gvChildOrderBookDetails.Visible = false;
                //buttonlink.Text = "+";
            }
            DataTable dtNCDOrderBook = (DataTable)Cache["SIPSumList" + userVo.UserId.ToString()];
            //DataRow[] rows = dtNCDOrderBook.Select("CMFSS_SystematicSetupId = " + systematicId + " ");
            //if (rows.Length > 0)
            //{
            //    filldt = rows.CopyToDataTable();
            //}
            gvChildOrderBookDetails.DataSource = dsSIPBookMIS;
            gvChildOrderBookDetails.DataBind();
            //gvChildOrderBookDetails.Visible = true;
            //if (Session["PageDefaultSetting"] != null)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('CustomerSIPBookList','?systematicId=" + systematicId + "&AmcCode=" + ddlAMCCode.SelectedValue + "&systematicType=" + systematictype + "&IsPageDefaultSetting=1" + "');", true);
            //}
            //else
            //{
            //    Response.Redirect("ControlHost.aspx?pageid=CustomerSIPBookList&systematicId=" + systematicId + "&AmcCode=" + ddlAMCCode.SelectedValue + "&systematicType=" + systematictype + "", false);
            //}
            //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerSIPBookList", "loadcontrol('CustomerSIPBookList');", true);
            // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerSIPBookList", "loadcontrol('CustomerSIPBookList','action=viewIsssueList&issueNo=" + issueNo + "&type=" + ddlType.SelectedValue + "&date=" + DateTime.Now + "&product=" + ddlProduct.SelectedValue + "');", true);

        }
        protected void gvSIPSummaryBookMIS_UpdateCommand(object source, GridCommandEventArgs e)
        {
            string strRemark = string.Empty;
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditableItem editItem = e.Item as GridEditableItem;
                TextBox txtRemark = (TextBox)e.Item.FindControl("txtRemark");
                strRemark = txtRemark.Text;
                //LinkButton buttonEdit = editItem["editColumn"].Controls[0] as LinkButton;
                Int32 systematicId = Convert.ToInt32(gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CMFSS_SystematicSetupId"].ToString());
                OnlineMFOrderBo.UpdateCnacleRegisterSIP(systematicId, 1, strRemark, userVo.UserId);
                BindSIPSummaryBook();
                //buttonEdit.Enabled = false;
            }

        }
        protected void gvSIPSummaryBookMIS_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                string isCancel = Convert.ToString(gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CMFSS_IsCanceled"]);
                int totalInstallment = Convert.ToInt32(gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CMFSS_TotalInstallment"].ToString());
                int currentInstallmentNumber = Convert.ToInt32(gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CMFSS_CurrentInstallmentNumber"].ToString());
                DateTime endDate = Convert.ToDateTime(gvSIPSummaryBookMIS.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CMFSS_EndDate"].ToString());
                //LinkButton buttonCancel = dataItem["editColumn"].Controls[0] as LinkButton;
                DateTime currentTime = DateTime.Now;
                LinkButton buttonCancel = (LinkButton)dataItem.FindControl("lbtnMarkAsReject");
                DateTime fixedTime = Convert.ToDateTime("08:35:00 AM");
                int compare = DateTime.Compare(currentTime, fixedTime);
                if (isCancel == "Cancelled" || totalInstallment == currentInstallmentNumber - 1 || endDate < DateTime.Now)
                {

                    buttonCancel.Enabled = false;
                }
                if (endDate == DateTime.Now)
                {
                    if (compare >= 0)
                    {
                        buttonCancel.Enabled = false;
                    }
                }
            }
            if (hdnsystamaticType.Value == "SWP")
            {
                //gvSIPSummaryBookMIS.MasterTableView.GetColumn("CMFSS_Amount").Visible = false;
                //gvSIPSummaryBookMIS.MasterTableView.GetColumn("Unit").Visible = true;
            }
            else
            {
                //gvSIPSummaryBookMIS.MasterTableView.GetColumn("CMFSS_Amount").Visible = true;
                //gvSIPSummaryBookMIS.MasterTableView.GetColumn("Unit").Visible = false;

            }
            if (e.Item is GridDataItem)
            {

                Label lblSchemeRating = (Label)e.Item.FindControl("lblSchemeRating");

                Label lblRating3Year = (Label)e.Item.FindControl("lblRating3Year");
                Label lblRating5Year = (Label)e.Item.FindControl("lblRating5Year");
                Label lblRating10Year = (Label)e.Item.FindControl("lblRating10Year");
                Label lblRatingAsOnPopUp = (Label)e.Item.FindControl("lblRatingAsOnPopUp");

                System.Web.UI.WebControls.Image imgSchemeRating = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgSchemeRating");

                System.Web.UI.WebControls.Image imgRating3Year = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRating3yr");
                System.Web.UI.WebControls.Image imgRating5Year = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRating5yr");
                System.Web.UI.WebControls.Image imgRating10Year = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRating10yr");
                System.Web.UI.WebControls.Image imgRatingOvelAll = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgRatingOvelAll");

                imgSchemeRating.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblSchemeRating.Text.Trim() + ".png";

                imgRating3Year.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblRating3Year.Text.Trim() + ".png";
                imgRating5Year.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblRating5Year.Text.Trim() + ".png";
                imgRating10Year.ImageUrl = @"../Images/MorningStarRating/RatingSmallIcon/" + lblRating10Year.Text.Trim() + ".png";

                imgRatingOvelAll.ImageUrl = @"../Images/MorningStarRating/RatingOverall/" + lblSchemeRating.Text.Trim() + ".png";
            }

        }
        protected void imgInformation_OnClick(object sender, EventArgs e)
        {
            RadInformation1.VisibleOnPageLoad = true;

        }

    }
}


