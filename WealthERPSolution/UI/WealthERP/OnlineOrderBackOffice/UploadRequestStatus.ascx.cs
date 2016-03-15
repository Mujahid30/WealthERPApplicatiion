using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using Telerik.Web.UI;
using System.Data;
using WealthERP.Base;
using System.Collections.Specialized;
using System.Collections;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;
using BoUploads;
using BoOfflineOrderManagement;
using BoOnlineOrderManagement;
using System.Text;
using VoOnlineOrderManagemnet;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class UploadRequestStatus : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        AdvisorVo advisorVo;
        UploadCommonBo uploadCommonBo = new UploadCommonBo();
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        StringBuilder columnNameError = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            txtReqDate.SelectedDate = DateTime.Now.Date;
            if (!IsPostBack)
            {
                GetTypes();
            }
        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            pnlRequest.Visible = true;
            btnexport.Visible = true;
            if (ddlType.SelectedValue != "11")
                GetRequests();
            else
                BindOrderReprocessDetails();

        }
        protected void BindOrderReprocessDetails()
        {
            try
            {
                DataTable dtOrderReject = uploadCommonBo.GetOrderRejectedData(Convert.ToDateTime(txtReqDate.SelectedDate), (ddlProduct.SelectedValue != "IP") ? ddlCategory.SelectedValue : "FIFIIP", int.Parse(ddlIsonline.SelectedValue));
                if (Cache[userVo.UserId.ToString() + "OrderReject"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "OrderReject");
                Cache.Insert(userVo.UserId.ToString() + "OrderReject", dtOrderReject);
                rgBondsGrid.Visible = false;
                rgRequests.Visible = false;
                radGridOrderDetails.Visible = true;
                radGridOrderDetails.DataSource = dtOrderReject;
                radGridOrderDetails.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void GetTypes()
        {
            try
            {
                DataTable dtType = new DataTable();
                dtType = uploadCommonBo.GetCMLType().Tables[0];
                ddlType.DataValueField = "WT_TaskId";
                ddlType.DataTextField = "WT_Task";
                ddlType.DataSource = dtType;
                ddlType.DataBind();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        private void GetRequests()
        {
            try
            {
                DataTable dtType = new DataTable();
                DataSet dsType = new DataSet();
                dsType = uploadCommonBo.GetCMLData(Convert.ToInt32(ddlType.SelectedValue), Convert.ToDateTime(txtReqDate.SelectedDate), advisorVo.advisorId, (ddlProduct.SelectedValue != "IP") ? ddlCategory.SelectedValue : "IP");
                if (dsType.Tables.Count == 0)
                    return;
                if (dsType != null)
                    dtType = dsType.Tables[0];
                if (ddlType.SelectedValue != "11")
                {
                    radGridOrderDetails.Visible = false;
                    rgRequests.Visible = true;
                    rgBondsGrid.Visible = false;
                    rgRequests.DataSource = dtType;
                    rgRequests.DataBind();
                }

                if (Cache[userVo.UserId.ToString() + "Requests"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "Requests");
                Cache.Insert(userVo.UserId.ToString() + "Requests", dtType);
                if (ddlType.SelectedValue == "8")
                {
                    rgRequests.MasterTableView.GetColumn("Cutomercreated").Visible = true;
                    rgRequests.MasterTableView.GetColumn("FolioCreated").Visible = true;
                    rgRequests.MasterTableView.GetColumn("TransactionCreated").Visible = false;
                    rgRequests.MasterTableView.GetColumn("RejectReseaon").Visible = true;
                    rgRequests.MasterTableView.GetColumn("IsOnl").Visible = true;
                    rgRequests.MasterTableView.GetColumn("RTA").Visible = true;
                    rgRequests.MasterTableView.GetColumn("StagingRejects").Visible = true;
                    rgRequests.MasterTableView.GetColumn("Staging").Visible = true;
                }
                else
                    if (ddlType.SelectedValue == "9")
                    {
                        rgRequests.MasterTableView.GetColumn("Cutomercreated").Visible = true;
                        rgRequests.MasterTableView.GetColumn("FolioCreated").Visible = true;
                        rgRequests.MasterTableView.GetColumn("TransactionCreated").Visible = true;
                        rgRequests.MasterTableView.GetColumn("RejectReseaon").Visible = true;
                        rgRequests.MasterTableView.GetColumn("IsOnl").Visible = true;
                        rgRequests.MasterTableView.GetColumn("RTA").Visible = true;
                        rgRequests.MasterTableView.GetColumn("StagingRejects").Visible = true;
                        rgRequests.MasterTableView.GetColumn("Staging").Visible = true;
                    }
                    else
                        if (ddlType.SelectedValue == "10")
                        {

                            rgRequests.MasterTableView.GetColumn("IsOnl").Visible = false;
                            rgRequests.MasterTableView.GetColumn("RTA").Visible = false;
                            rgRequests.MasterTableView.GetColumn("StagingRejects").Visible = false;
                            rgRequests.MasterTableView.GetColumn("Staging").Visible = false;
                            rgRequests.MasterTableView.GetColumn("RejectReseaon").Visible = false;
                            rgRequests.MasterTableView.GetColumn("Cutomercreated").Visible = false;
                            rgRequests.MasterTableView.GetColumn("FolioCreated").Visible = false;
                            rgRequests.MasterTableView.GetColumn("TransactionCreated").Visible = false;
                        }
                        else
                            if (ddlType.SelectedValue == "3" || ddlType.SelectedValue == "4")
                            {
                                rgRequests.MasterTableView.GetColumn("IsOnl").Visible = false;
                                rgRequests.MasterTableView.GetColumn("RTA").Visible = false;
                                rgRequests.MasterTableView.GetColumn("StagingRejects").Visible = true;
                                rgRequests.MasterTableView.GetColumn("Staging").Visible = true;
                                rgRequests.MasterTableView.GetColumn("RejectReseaon").Visible = false;
                                rgRequests.MasterTableView.GetColumn("Cutomercreated").Visible = false;
                                rgRequests.MasterTableView.GetColumn("FolioCreated").Visible = false;
                                rgRequests.MasterTableView.GetColumn("TransactionCreated").Visible = false;
                            }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            rgRequests.ExportSettings.OpenInNewWindow = true;
            rgRequests.ExportSettings.IgnorePaging = true;
            rgRequests.ExportSettings.HideStructureColumns = true;
            rgRequests.ExportSettings.ExportOnlyData = true;
            rgRequests.ExportSettings.FileName = "Upload Request Status";
            rgRequests.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rgRequests.MasterTableView.ExportToExcel();
        }
        protected void rgRequests_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem && e.Item.ItemIndex != -1)
            {
                if (ddlType.SelectedValue != "11")
                {
                    GridDataItem editform = (GridDataItem)e.Item;
                    int inputRejects = Convert.ToInt32(editform["InputRejects"].Text);
                    int stagingRejects = Convert.ToInt32(editform["StagingRejects"].Text);
                    int Staging = Convert.ToInt32(editform["Staging"].Text);
                    LinkButton lbDetails = (LinkButton)editform.FindControl("lbDetails");
                    if (inputRejects + stagingRejects + Staging > 0)
                    {
                        lbDetails.Visible = true;
                    }
                    else
                    {
                        lbDetails.Visible = false;
                    }
                }
            }
        }
        //protected void rgRequests_ItemCommand(object source, GridCommandEventArgs e)
        //{
        //    DataTable dt = (DataTable)Cache[userVo.UserId.ToString() + "Requests"];
        //    string ecommand = null;

        //    if (e.CommandName == RadGrid.UpdateCommandName)
        //    {
        //        ecommand = "UP";
        //        GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
        //        TextBox txtExHeader = (TextBox)e.Item.FindControl("txtExHeader");
        //        int requestId = Convert.ToInt32(rgRequests.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ReqId"].ToString());

        //        //uploadCommonBo.CreateUpdateExternalHeader();
        //            Response.Write(@"<script language='javascript'>alert('External Header " + txtExHeader.Text + "Updated successfully');</script>");


        //    }

        //}

        protected void radGridOrderDetails_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtRequests = new DataTable();
            dtRequests = (DataTable)Cache[userVo.UserId.ToString() + "OrderReject"];
            if (dtRequests != null)
            {
                radGridOrderDetails.DataSource = dtRequests;
            }
        }
        protected void rgRequests_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtRequests = new DataTable();
            dtRequests = (DataTable)Cache[userVo.UserId.ToString() + "Requests"];
            if (dtRequests != null)
            {
                rgRequests.DataSource = dtRequests;
            }
        }

        protected void rgBondsGrid_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtRequests = new DataTable();
            dtRequests = (DataTable)Cache[userVo.UserId.ToString() + "OrderRejectdtType"];
            if (dtRequests != null)
            {
                rgBondsGrid.DataSource = dtRequests;
            }
        }

        protected void rgRequestRejects_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid rgRequestRejects = (RadGrid)sender;
            DataTable dtRequestsWiseRejects = new DataTable();
            dtRequestsWiseRejects = (DataTable)Cache[userVo.UserId.ToString() + "RequestsWiseRejects"];
            if (dtRequestsWiseRejects != null)
            {
                rgRequestRejects.DataSource = dtRequestsWiseRejects;
            }
        }
        protected void lnkCustmCreated_Click(object sender, EventArgs e)
        {
            LinkButton lnkOrderNo = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkOrderNo.NamingContainer;
            int selectedRow = gdi.ItemIndex + 1;
            int reqId = int.Parse((rgRequests.MasterTableView.DataKeyValues[selectedRow - 1]["ReqId"].ToString()));
            string IsOnl = rgRequests.MasterTableView.DataKeyValues[selectedRow - 1]["IsOnl"].ToString();
            if (reqId != 0)
            {
                Response.Redirect("ControlHost.aspx?pageid=AdviserCustomer&reqId=" + reqId + "&IsOnl=" + IsOnl + "", false);
            }
        }
        protected void lnkFolioCreated_Click(object sender, EventArgs e)
        {
            LinkButton lnkOrderNo = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkOrderNo.NamingContainer;
            int selectedRow = gdi.ItemIndex + 1;
            int reqId = int.Parse((rgRequests.MasterTableView.DataKeyValues[selectedRow - 1]["ReqId"].ToString()));

            if (reqId != 0)
            {
                Response.Redirect("ControlHost.aspx?pageid=AdvisorCustomerAccounts&reqId=" + reqId + "", false);
            }
        }
        protected void lnkTransactionCreated_Click(object sender, EventArgs e)
        {
            LinkButton lnkOrderNo = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkOrderNo.NamingContainer;
            int selectedRow = gdi.ItemIndex + 1;
            int reqId = int.Parse((rgRequests.MasterTableView.DataKeyValues[selectedRow - 1]["ReqId"].ToString()));
            string IsOnl = rgRequests.MasterTableView.DataKeyValues[selectedRow - 1]["IsOnl"].ToString();
            if (reqId != 0)
            {
                if (IsOnl == "Offline")
                {
                    Response.Redirect("ControlHost.aspx?pageid=RMMultipleTransactionView&reqId=" + reqId + "", false);
                }
                else if (IsOnl == "Online")
                {
                    Response.Redirect("ControlHost.aspx?pageid=OnlineAdviserCustomerTransctionBook&reqId=" + reqId + "", false);
                }
            }
        }
        protected void btnCategoriesExpandAll_Click(object sender, EventArgs e)
        {
            int reqId = 0;
            int transactionId = 0;
            LinkButton buttonlink = (LinkButton)sender;
            GridDataItem gdi = (GridDataItem)buttonlink.NamingContainer;
            if (!string.IsNullOrEmpty(rgRequests.MasterTableView.DataKeyValues[gdi.ItemIndex]["ReqId"].ToString()))
            {
                reqId = int.Parse(rgRequests.MasterTableView.DataKeyValues[gdi.ItemIndex]["ReqId"].ToString());
                transactionId = Convert.ToInt32(ddlType.SelectedValue);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ManageProfileReject", "loadcontrol('ManageProfileReject','?ReqId=" + reqId + "&transactionId=" + transactionId + "');", true);
            }
        }

        protected void ddlProduct_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProduct.SelectedValue == "FI")
            {
                tdCategory.Visible = true;
                tdProductType.Visible = true;
                BindNcdCategory();
            }
            else
            {
                tdCategory.Visible = false;
                tdProductType.Visible = true;
            }
        }
        protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            tdProduct.Visible = false;
            tdCategory.Visible = false;
            if (ddlType.SelectedValue == "11")
            {
                tdProduct.Visible = true;
                tdCategory.Visible = true;
            }


        }

        private void BindNcdCategory()
        {
            DataTable dtCategory = new DataTable();
            OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
            dtCategory = onlineNCDBackOfficeBo.BindNcdCategory("SubInstrumentCat", "").Tables[0];
            if (dtCategory.Rows.Count > 0)
            {
                ddlCategory.DataSource = dtCategory;
                ddlCategory.DataValueField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlCategory.DataTextField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlCategory.DataBind();
            }
            ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));
        }

        protected void btnReprocess_OnClick(object sender, EventArgs e)
        {
            int i = 0,issueId=0;

            foreach (GridDataItem dataItem in rgBondsGrid.MasterTableView.Items)
            {
                issueId = int.Parse(rgBondsGrid.MasterTableView.DataKeyValues[dataItem.ItemIndex]["AIAPL_IssueId"].ToString());
                if ((dataItem.FindControl("chkId") as CheckBox).Checked)
                {
                    i = i + 1;
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Select to Update SubBrokerCode/order quentity/PAN !');", true);
                return;
            }
            else
            {
                DataTable dtSubBrokerCode = new DataTable();
                dtSubBrokerCode.Columns.Add("AppNbr");
                dtSubBrokerCode.Columns.Add("IssueName1");
                dtSubBrokerCode.Columns.Add("AlltAmount");
                dtSubBrokerCode.Columns.Add("L/Folio");
                dtSubBrokerCode.Columns.Add("PAN");
                dtSubBrokerCode.Columns.Add("InvestorName");
                dtSubBrokerCode.Columns.Add("ChequeNO");
                dtSubBrokerCode.Columns.Add("IssueName");
                dtSubBrokerCode.Columns.Add("BrokerCode");
                dtSubBrokerCode.Columns.Add("SubBrokerCode");
                dtSubBrokerCode.Columns.Add("RTARejectRemark");
                dtSubBrokerCode.Columns.Add("RTAOtherRemarks");
                dtSubBrokerCode.Columns.Add("ReceivableAmt");
                dtSubBrokerCode.Columns.Add("ReceivableRate");
                dtSubBrokerCode.Columns.Add("PayableRate");
                dtSubBrokerCode.Columns.Add("PayableAmt");
                dtSubBrokerCode.Columns.Add("SpecialRate");
                dtSubBrokerCode.Columns.Add("SpecialAmt");
                dtSubBrokerCode.Columns.Add("NetBrokerage");
                dtSubBrokerCode.Columns.Add("SvcTaxAMT");
                dtSubBrokerCode.Columns.Add("TDSAMT");
                dtSubBrokerCode.Columns.Add("GrossBrokerge");
                dtSubBrokerCode.Columns.Add("PaymentDate");
                dtSubBrokerCode.Columns.Add("AIM_IssueName");
                dtSubBrokerCode.Columns.Add("AIAPL_IssueId");

                
                
                DataRow drSubBrokerCode;
                foreach (GridDataItem radItem in rgBondsGrid.MasterTableView.Items)
                {
                    TextBox txtApplicationNo = (TextBox)radItem.FindControl("txtApplicationNo");
                    TextBox txtAlltQty = (TextBox)radItem.FindControl("txtAlltQty");
                    TextBox txtCertificate_No = (TextBox)radItem.FindControl("txtCertificate_No");
                    TextBox txtPangir = (TextBox)radItem.FindControl("txtPangir");

                    if ((radItem.FindControl("chkId") as CheckBox).Checked)
                    {
                        drSubBrokerCode = dtSubBrokerCode.NewRow();

                        drSubBrokerCode["AppNbr"] = txtApplicationNo.Text;
                        drSubBrokerCode["IssueName1"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIM_IssueName"];
                        drSubBrokerCode["AIAPL_IssueId"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAPL_IssueId"];
                        drSubBrokerCode["AlltAmount"] = txtAlltQty.Text;
                        drSubBrokerCode["L/Folio"] = txtCertificate_No.Text;
                        drSubBrokerCode["PAN"] = txtPangir.Text.ToString().TrimEnd(' ');
                        drSubBrokerCode["InvestorName"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_InvestorName"].ToString().TrimEnd(' ');
                        drSubBrokerCode["ChequeNO"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_RfndNo"];
                        drSubBrokerCode["IssueName"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_IssueCode"];
                        drSubBrokerCode["BrokerCode"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_BrokerCode"].ToString().TrimEnd(' ');
                        drSubBrokerCode["SubBrokerCode"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_SubBrokerCode"].ToString().TrimEnd(' ');
                        drSubBrokerCode["RTARejectRemark"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_Reason"];
                        drSubBrokerCode["RTAOtherRemarks"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_Remark_Aot"];
                        drSubBrokerCode["ReceivableAmt"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_Brk1_Rec"];
                        drSubBrokerCode["ReceivableRate"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_Brk1_Rec_Rate"];
                        drSubBrokerCode["PayableRate"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_Brk2_Rec_Rate"];
                        drSubBrokerCode["PayableAmt"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_Brk2_Rec"];
                        drSubBrokerCode["SpecialRate"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_Brk3_Rec_Rate"];
                        drSubBrokerCode["SpecialAmt"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_Brk3_Rec"];
                        drSubBrokerCode["NetBrokerage"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_Total_Brk_rec"];
                        drSubBrokerCode["SvcTaxAMT"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_SvcTaxAM"];
                        drSubBrokerCode["TDSAMT"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_Tds"];
                        drSubBrokerCode["GrossBrokerge"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_Total_Receivable"];
                        drSubBrokerCode["PaymentDate"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIAUL_AllotmentDate"];
                        drSubBrokerCode["AIM_IssueName"] = rgBondsGrid.MasterTableView.DataKeyValues[radItem.ItemIndex]["AIM_IssueName"];
                        dtSubBrokerCode.Rows.Add(drSubBrokerCode);
                    }
                }

                DataTable dtValidatedData = onlineNCDBackOfficeBo.ValidateUploadData(dtSubBrokerCode,46, "R1", ref columnNameError);
                ToggleUpload(dtValidatedData, issueId);
                DataTable dtUploadData = CheckHeadersGrid(dtValidatedData);

                rgBondsGrid.DataSource = dtUploadData;
                rgBondsGrid.Rebind(); btnReprocess.Visible = false;
                
            }
        }
        private void ToggleUpload(DataTable dtUpload,int  issueId)
        {
            bool bUpload = true;
            int r = 46;
            foreach (DataRow row in dtUpload.Rows)
            {
                if (string.IsNullOrEmpty(row["Remarks"].ToString().Trim())) continue;
                bUpload = false;
                ShowMessage("Please check the data in the file & re-process", "F");
                break;
            }

            if (r == 46 && bUpload==true)
            {
                UploadIssueAllotmentData(dtUpload, true, issueId);
            }
            else
            {
                ShowMessage("File data has been re-process", "S");
            }

        }
        public void UploadIssueAllotmentData(DataTable dtUploadData, bool IsAllotmentUpload,int issueId)
        {
            string isIssueAvailable = "";
            string result = "";
            int acceptedOrders = 0, rejectedOrders = 0, totalOrder = 0;
            OnlineNCDBackOfficeBo boNcdBackOff = new OnlineNCDBackOfficeBo();
            DataTable dtAllotmentUpload = new DataTable();
            dtUploadData = CheckHeaders(dtUploadData);
            if (IsAllotmentUpload)
            {
                dtAllotmentUpload = boNcdBackOff.UploadAllotmentFile(dtUploadData, 46, issueId, ref isIssueAvailable, advisorVo.advisorId, null, ref result, ddlProduct.SelectedValue, null, userVo.UserId, Convert.ToInt32(ddlType.SelectedValue), "FICGCG", ref totalOrder, ref rejectedOrders, ref acceptedOrders);
            }

            if (isIssueAvailable == "NotEligble")
            {
                ShowMessage("Uploaded file Issue and Selected issue Does not match", "F");
            }
            else if (result != string.Empty && result != "1")
            {
                ShowMessage(result, "W");
            }
            else
            {
                ShowMessage("data uploaded", "S");

            }


        }


        private void ShowMessage(string msg, string type)
        {
            tblMessagee.Visible = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wsedrftgyhjukloghjnnnghj", " showMsg('" + msg + "','" + type + "');", true);
        }
        private DataTable CheckHeadersGrid(DataTable dtUploadData)
        {
            OnlineNCDBackOfficeBo boNcdBackOff = new OnlineNCDBackOfficeBo();
            List<OnlineIssueHeader> updHeaders;
            updHeaders = boNcdBackOff.GetHeaderDetails(46, "R1");

            foreach (OnlineIssueHeader header in updHeaders)
            {
                if (header.IsUploadRelated == true)
                {
                    if (dtUploadData.Columns.Contains(header.HeaderName))
                    {
                        dtUploadData.Columns[header.HeaderName].ColumnName = header.ColumnName;
                    }
                }

            }

            if (dtUploadData.Columns.Contains("SN"))
            {
                dtUploadData.Columns.Remove(dtUploadData.Columns["SN"]);

            }
            if (!dtUploadData.Columns.Contains("AIM_IssueName"))
            {
                dtUploadData.Columns.Add("AIM_IssueName");

            }
            if (!dtUploadData.Columns.Contains("AIAPL_IssueId"))
            {
                dtUploadData.Columns.Add("AIAPL_IssueId");

            }
            dtUploadData.AcceptChanges();
            return dtUploadData;
        }
        private DataTable CheckHeaders(DataTable dtUploadData)
        {
            OnlineNCDBackOfficeBo boNcdBackOff = new OnlineNCDBackOfficeBo();
            List<OnlineIssueHeader> updHeaders;
            updHeaders = boNcdBackOff.GetHeaderDetails(46, "R1");
            
            foreach (OnlineIssueHeader header in updHeaders)
            {
                if (header.IsUploadRelated == true)
                {
                    if (dtUploadData.Columns.Contains(header.HeaderName))
                    {
                        dtUploadData.Columns[header.HeaderName].ColumnName = header.ColumnName;
                    }
                }
               
            }

            if (dtUploadData.Columns.Contains("SN"))
            {
                dtUploadData.Columns.Remove(dtUploadData.Columns["SN"]);

            }
            if (dtUploadData.Columns.Contains("IssueName1"))
            {
                dtUploadData.Columns.Remove(dtUploadData.Columns["IssueName1"]);

            }
            if (dtUploadData.Columns.Contains("Remarks"))
            {
                dtUploadData.Columns.Remove(dtUploadData.Columns["Remarks"]);

            }
            if (dtUploadData.Columns.Contains("AIM_IssueName"))
            {
                dtUploadData.Columns.Remove(dtUploadData.Columns["AIM_IssueName"]);

            }

            if (dtUploadData.Columns.Contains("AIAPL_IssueId"))
            {
                dtUploadData.Columns.Remove(dtUploadData.Columns["AIAPL_IssueId"]);

            }
            
            
            dtUploadData.AcceptChanges();

            return dtUploadData;

        }
        protected void txtPangir_OnClick(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridDataItem grd = (GridDataItem)lnk.NamingContainer;

            int processid = int.Parse(radGridOrderDetails.MasterTableView.DataKeyValues[grd.ItemIndex]["processid"].ToString());
            BindBondIPOProductrejectedData(processid);
        }
        protected void BindBondIPOProductrejectedData(int processId)
        {
            DataTable dtType = uploadCommonBo.GetCMLBONCDData(processId, Convert.ToDateTime(txtReqDate.SelectedDate), advisorVo.advisorId, (ddlProduct.SelectedValue != "IP") ? ddlCategory.SelectedValue : "IP",int.Parse(ddlIsonline.SelectedValue));
            if (Cache[userVo.UserId.ToString() + "OrderRejectdtType"] != null)
                Cache.Remove(userVo.UserId.ToString() + "OrderRejectdtType");
            Cache.Insert(userVo.UserId.ToString() + "OrderRejectdtType", dtType);
            rgRequests.Visible = false;
            radGridOrderDetails.Visible = false;
            rgBondsGrid.Visible = true;
            btnReprocess.Visible = true;
            rgBondsGrid.DataSource = dtType;
            rgBondsGrid.DataBind();

        }
    }
}