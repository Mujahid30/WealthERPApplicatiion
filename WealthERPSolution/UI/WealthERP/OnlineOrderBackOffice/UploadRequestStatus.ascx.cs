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

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class UploadRequestStatus : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        AdvisorVo advisorVo;
        UploadCommonBo uploadCommonBo = new UploadCommonBo();
        

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
            GetRequests();
            
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
                dsType = uploadCommonBo.GetCMLData(Convert.ToInt32(ddlType.SelectedValue), Convert.ToDateTime(txtReqDate.SelectedDate), advisorVo.advisorId);
                if (dsType.Tables.Count == 0)
                    return;
                if (dsType != null)
                    dtType = dsType.Tables[0];
                rgRequests.DataSource = dtType;
                rgRequests.DataBind();
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
                            if (ddlType.SelectedValue == "11")
                            {
                                //rgRequests.MasterTableView.GetColumn("Cutomercreated").Visible = true;
                                //rgRequests.MasterTableView.GetColumn("FolioCreated").Visible = true;
                                //rgRequests.MasterTableView.GetColumn("TransactionCreated").Visible = true;
                                //rgRequests.MasterTableView.GetColumn("RejectReseaon").Visible = true;
                                //rgRequests.MasterTableView.GetColumn("IsOnl").Visible = true;
                                //rgRequests.MasterTableView.GetColumn("RTA").Visible = true;
                                //rgRequests.MasterTableView.GetColumn("StagingRejects").Visible = true;
                                //rgRequests.MasterTableView.GetColumn("Staging").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_ProcessId").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_Status").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIM_IssueName").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_ApplicationNumber").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_Shares").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_Certificate_No").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_Pangir").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_InvestorName").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_RfndNo").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_IssueCode").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_BrokerCode").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_Reason").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_Remark_Aot").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_Brk1_Rec").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_Brk1_Rec_Rate").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_Brk2_Rec_Rate").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_Brk2_Rec").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_Brk3_Rec_Rate").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_Total_Brk_rec").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_SvcTaxAM").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_Tds").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_Total_Receivable").Visible = true;
                                rgRequests.MasterTableView.GetColumn("AIAUL_AllotmentDate").Visible = true;
                                rgRequests.MasterTableView.GetColumn("ReqId").Visible = false;
                                rgRequests.MasterTableView.GetColumn("ReqDate").Visible = false;
                                rgRequests.MasterTableView.GetColumn("filename").Visible = false;
                                rgRequests.MasterTableView.GetColumn("Status").Visible = false;
                                rgRequests.MasterTableView.GetColumn("IsOnl").Visible = false;
                                rgRequests.MasterTableView.GetColumn("RTA").Visible = false;
                                rgRequests.MasterTableView.GetColumn("TotalNoOfRecords").Visible = false;
                                rgRequests.MasterTableView.GetColumn("InputRejects").Visible = false;
                                rgRequests.MasterTableView.GetColumn("StagingRejects").Visible = false;
                                rgRequests.MasterTableView.GetColumn("Staging").Visible = false;
                                rgRequests.MasterTableView.GetColumn("Success").Visible = false;


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
        protected void rgRequests_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtRequests = new DataTable();
            dtRequests = (DataTable)Cache[userVo.UserId.ToString() + "Requests"];
            if (dtRequests != null)
            {
                rgRequests.DataSource = dtRequests;
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
            if (reqId!=0)
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
    }
}