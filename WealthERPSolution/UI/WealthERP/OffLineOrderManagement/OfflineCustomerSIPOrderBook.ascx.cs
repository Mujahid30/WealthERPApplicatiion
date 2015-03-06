using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using VoUser;
using BoCommon;
using BoWerpAdmin;
using BoOfflineOrderManagement;
using WealthERP.Base;
using VOAssociates;
using VoOps;
using BoOps;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoProductMaster;
using BoOnlineOrderManagement;


namespace WealthERP.OffLineOrderManagement
{
    public partial class OfflineCustomerSIPOrderBook : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        DateTime fromDate;
        DateTime toDate;
        string userType;
        string UserTitle;
        string AgentCode;
        string agentCode;
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        OfflineMFOrderBackOfficeBo OfflineMFOrderBackOfficeBo = new OfflineMFOrderBackOfficeBo();
        ProductMFBo productMFBo = new ProductMFBo();
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();
            associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
            {
                userType = "advisor";
                // userType = "admin";

            }
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
            {
                userType = "bm";

            }
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
            {
                userType = "rm";

            }

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
            {
                userType = "associates";
                if (UserTitle == "SubBroker")
                {
                    associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                    if (associateuserheirarchyVo.AgentCode != null)
                    {
                        AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                    }
                    else
                        AgentCode = "0";
                }
                else
                {
                    associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                    if (associateuserheirarchyVo.AgentCode != null)
                    {
                        AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                    }
                    else
                        AgentCode = "0";
                }
            }
            if (!IsPostBack)
            {
                fromDate = DateTime.Now.AddDays(-1);
                txtOrderFrom.SelectedDate = fromDate.Date;
                txtOrderTo.SelectedDate = DateTime.Now;
                BindOrderStatus();
                BindAMC();
            }

        }
        protected void btnViewOrder_Click(object sender, EventArgs e)
        {
            BindSIPOfflineBook();
        }
        protected void BindOrderStatus()
        {
            try
            {
                DataTable dtBindOrderStatus = OfflineMFOrderBackOfficeBo.GetStatusCode();
                ddlStatus.DataSource = dtBindOrderStatus;
                ddlStatus.DataTextField = dtBindOrderStatus.Columns["XS_Status"].ToString();
                ddlStatus.DataValueField = dtBindOrderStatus.Columns["XS_StatusCode"].ToString();
                ddlStatus.DataBind();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        private void BindAMC()
        {
            DataSet dsProductAmc;
            DataTable dtProductAMC;
            try
            {
                dsProductAmc = productMFBo.GetProductAmcList();
                if (dsProductAmc.Tables[0].Rows.Count > 0)
                {
                    dtProductAMC = dsProductAmc.Tables[0];
                    ddlAMC.DataSource = dtProductAMC;
                    ddlAMC.DataTextField = dtProductAMC.Columns["PA_AMCName"].ToString();
                    ddlAMC.DataValueField = dtProductAMC.Columns["PA_AMCCode"].ToString();
                    ddlAMC.DataBind();
                }
                ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        protected void BindSIPOfflineBook()
        {
            DataTable dtSIPBookMIS = new DataTable();
            DataSet dsBindSIPOfflineBook = OfflineMFOrderBackOfficeBo.GetOfflineSIPSummaryBookMIS(int.Parse(ddlAMC.SelectedValue), Convert.ToDateTime(txtOrderFrom.SelectedDate), Convert.ToDateTime(txtOrderTo.SelectedDate), advisorVo.advisorId);
            dtSIPBookMIS = dsBindSIPOfflineBook.Tables[0];
            dtSIPBookMIS = createSIPOrderBook(dsBindSIPOfflineBook);
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
                //btnExport.Visible = true;
                //trNoRecords.Visible = false;
                //divNoRecords.Visible = false;

            }
            else
            {
                gvSIPSummaryBookMIS.DataSource = dtSIPBookMIS;
                gvSIPSummaryBookMIS.DataBind();
                pnlSIPSumBook.Visible = true;
                //trNoRecords.Visible = true;
                //divNoRecords.Visible = true;
                //btnExport.Visible = false;
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

            foreach (DataRow drSIP in dtSIPDetails.Rows)
            {
                drSIPOrderBook = dtFinalSIPOrderBook.NewRow();

                int sipDueCount = 0, inProcessCount = 0, executedcount = 0, acceptCount = 0, systemRejectCount = 0, rejectedCount = 0;

                dvSIPOrderDetails = new DataView(dtOrderDetails, "CMFSS_SystematicSetupId=" + drSIP["CMFSS_SystematicSetupId"].ToString(), "CMFSS_SystematicSetupId", DataViewRowState.CurrentRows);

                if (int.Parse(drSIP["CMFSS_IsSourceAA"].ToString()) == 1)
                {
                    sipDueCount = (Convert.ToInt16(drSIP["CMFSS_TotalInstallment"].ToString())
                          - ((Convert.ToInt16(drSIP["CMFSS_CurrentInstallmentNumber"].ToString())) - 1)) - dvSIPOrderDetails.ToTable().Rows.Count;

                }
                else
                {
                    sipDueCount = (Convert.ToInt16(drSIP["CMFSS_TotalInstallment"].ToString()) - dvSIPOrderDetails.ToTable().Rows.Count);
                }


                foreach (DataRow drOrder in dvSIPOrderDetails.ToTable().Rows)
                {
                    switch (drOrder["WOS_OrderStepCode"].ToString().TrimEnd())
                    {
                        case "AL":
                            inProcessCount = inProcessCount + 1;
                            break;
                        case "IP":
                            executedcount = executedcount + 1;
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
               // drSIPOrderBook["CMFSS_StartDate"] = DateTime.Parse(drSIP["CMFSS_StartDate"].ToString());
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
                if (int.Parse(drSIP["CMFSS_IsSourceAA"].ToString()) == 1)
                {
                    inProcessCount = (Convert.ToInt16(drSIP["CMFSS_TotalInstallment"].ToString())
                          - ((Convert.ToInt16(drSIP["CMFSS_InstallmentOther"].ToString())) - 1)) - dvSIPOrderDetails.ToTable().Rows.Count;
                }
                else
                {
                    inProcessCount = (Convert.ToInt16(drSIP["CMFSS_TotalInstallment"].ToString()) - dvSIPOrderDetails.ToTable().Rows.Count);
                }
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
                drSIPOrderBook["ExecutedCount"] = executedcount;
                drSIPOrderBook["CMFSS_IsSourceAA"] = drSIP["CMFSS_IsSourceAA"];
                drSIPOrderBook["C_CustomerId"] = drSIP["C_CustomerId"];
                drSIPOrderBook["U_UMId"] = drSIP["U_UMId"];
                if (!string.IsNullOrEmpty(drSIP["CMFSS_RegistrationDate"].ToString()))
                {
                    drSIPOrderBook["CMFSS_RegistrationDate"] = DateTime.Parse(drSIP["CMFSS_RegistrationDate"].ToString()) ;

                }
                if (!string.IsNullOrEmpty(drSIP["CMFSS_StartDate"].ToString()))
                {
                    drSIPOrderBook["CMFSS_StartDate"] = DateTime.Parse(drSIP["CMFSS_StartDate"].ToString());
                  
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
                drSIPOrderBook["ZonalManagerName"] = drSIP["ZonalManagerName"].ToString();
                drSIPOrderBook["AreaManager"] = drSIP["AreaManager"];
                drSIPOrderBook["AssociatesName"] = drSIP["AssociatesName"].ToString();
                drSIPOrderBook["ChannelName"] = drSIP["ChannelName"];
                drSIPOrderBook["Titles"] = drSIP["Titles"].ToString();
                drSIPOrderBook["ClusterManager"] = drSIP["ClusterManager"];
                drSIPOrderBook["ReportingManagerName"] = drSIP["ReportingManagerName"].ToString();
                drSIPOrderBook["UserType"] = drSIP["UserType"];
                drSIPOrderBook["DeputyHead"] = drSIP["DeputyHead"];
                drSIPOrderBook["WMTT_TransactionClassificationCode"] = drSIP["WMTT_TransactionClassificationCode"];
                drSIPOrderBook["WMTT_TransactionClassificationName"] = drSIP["WMTT_TransactionClassificationName"];
                //drSIPOrderBook["Unit"] = drSIP["Unit"];
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
            dtSIPOrderBook.Columns.Add("ZonalManagerName");
            dtSIPOrderBook.Columns.Add("AreaManager");
            dtSIPOrderBook.Columns.Add("AssociatesName");
            dtSIPOrderBook.Columns.Add("ChannelName");
            dtSIPOrderBook.Columns.Add("Titles");
            dtSIPOrderBook.Columns.Add("ClusterManager");
            dtSIPOrderBook.Columns.Add("ReportingManagerName");
            dtSIPOrderBook.Columns.Add("UserType");
            dtSIPOrderBook.Columns.Add("DeputyHead");
            dtSIPOrderBook.Columns.Add("WMTT_TransactionClassificationCode");
            dtSIPOrderBook.Columns.Add("WMTT_TransactionClassificationName");
            dtSIPOrderBook.Columns.Add("Unit", typeof(double));
            return dtSIPOrderBook;

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
                OnlineMFOrderBo.UpdateCnacleRegisterSIP(systematicId, 1, strRemark, userVo.UserId);
                BindSIPOfflineBook();
                buttonEdit.Enabled = false;
            }
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
                                string transCode=gvr.GetDataKeyValue("WMTT_TransactionClassificationCode").ToString();
                             
                                if (e.CommandName == "Select")
                                {
                                    Response.Redirect("ControlHost.aspx?pageid=OrderList&systematicId=" + systematicId + "&AccountId=" + AccountId + "&schemeplanCode=" + schemeplanCode + "&IsSourceAA=" + IsSourceAA + "&customerId=" + customerId + "", false);
                                }
                                if (e.CommandName == "Accepted")
                                {
                                    Response.Redirect("ControlHost.aspx?pageid=OrderList&systematicId=" + systematicId + "&transCode=" + transCode + "&FromDate=" + txtOrderFrom.SelectedDate + "&OrderStatus=PR", false);
                                }
                                else if (e.CommandName == "In Process")
                                {
                                    Response.Redirect("ControlHost.aspx?pageid=OrderList&systematicId=" + systematicId + "&transCode=" + transCode + "&FromDate=" + txtOrderFrom.SelectedDate + "&OrderStatus=AL", false);
                                }
                                else if (e.CommandName == "Rejected")
                                {
                                    Response.Redirect("ControlHost.aspx?pageid=OrderList&systematicId=" + systematicId + "&transCode=" + transCode + "&FromDate=" + txtOrderFrom.SelectedDate + "&OrderStatus=RJ", false);

                                }
                                else if (e.CommandName == "Executed")
                                {
                                    Response.Redirect("ControlHost.aspx?pageid=OrderList&systematicId=" + systematicId + "&transCode=" + transCode + "&FromDate=" + txtOrderFrom.SelectedDate + "&OrderStatus=IP", false);

                                }
                            }
                        }
                    }
                }
            }
        }

    }
}