using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using WealthERP.Base;
using Telerik.Web.UI;
using System.Data;
using System.Collections.Specialized;
using System.Collections;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;
using BoUploads;
namespace WealthERP.UploadBackOffice
{
    public partial class ManageProfileReject : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        AdvisorVo advisorVo;
        UploadCommonBo uploadCommonBo = new UploadCommonBo();
        int reqId;
        int transactionId;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userBo = new UserBo();
            userVo = (UserVo)Session[SessionContents.UserVo];
            if (!IsPostBack)
            {
                msgReprocessComplete.Visible = false;
                reqId = Convert.ToInt32(Request.QueryString["ReqId"]);
                transactionId = Convert.ToInt32(Request.QueryString["transactionId"]);
                if (reqId != null)
                {
                    GetProfileIncreamentRejection(reqId);
                }
            }
        }

        private void GetProfileIncreamentRejection(int reqId)
        {
            try
            {

                DataTable dtReqReje = new DataTable();
                DataSet dsRej = new DataSet();
                dsRej = uploadCommonBo.RequestWiseRejects(reqId);
                dtReqReje = dsRej.Tables[0];
                if (Cache["RequestReject" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("RequestReject" + userVo.UserId.ToString(), dtReqReje);
                }
                else
                {
                    Cache.Remove("RequestReject" + userVo.UserId.ToString());
                    Cache.Insert("RequestReject" + userVo.UserId.ToString(), dtReqReje);
                }
                gvProfileIncreamenetReject.DataSource = dtReqReje;
                gvProfileIncreamenetReject.DataBind();
                gvProfileIncreamenetReject.Visible = true;
                if (transactionId == 3 || transactionId == 4)
                {
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("ProductCode").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("FolioNo").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrLine1").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrLine2").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrLine3").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrPinCode").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrState").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrCountry").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BrokerCode").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("SchemeName").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionNum").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionDate").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("Price").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("Units").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("Amount").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("STT").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BrokeragePer").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn(" BrokerageAmount").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("UserTransactionNo").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionType").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionNature").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionHead").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionDescription").Visible = false;

                }
                else if (transactionId == 8)
                {
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("ProductCode").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("FolioNo").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrLine1").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrLine2").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrLine3").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrPinCode").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrState").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrCountry").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BrokerCode").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("SchemeName").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionNum").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionDate").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("Price").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("Units").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("Amount").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("STT").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BrokeragePer").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn(" BrokerageAmount").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("UserTransactionNo").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionType").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionNature").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionHead").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionDescription").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("ClientCode").Visible = false;
                }
                else if (transactionId == 9)
                {
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("ProductCode").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("FolioNo").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrLine1").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrLine2").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrLine3").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrPinCode").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrState").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrCountry").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BrokerCode").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("SchemeName").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionNum").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionDate").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("Price").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("Units").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("Amount").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("STT").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BrokeragePer").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn(" BrokerageAmount").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("UserTransactionNo").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionType").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionNature").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionHead").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("TransactionDescription").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("ClientCode").Visible = false;
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string city = string.Empty;
            string state = string.Empty;
            string pincode = string.Empty;
            string mobileno = string.Empty;
            string occupation = string.Empty;
            string accounttype = string.Empty;
            string bankname = string.Empty;
            string personalstatus = string.Empty;
            bool blResult = false;
            int Id = 0;
            int tableNo = 0;
            string clientCode = string.Empty;
            string address1 = string.Empty;
            string address2 = string.Empty;
            string address3 = string.Empty;
            string country = string.Empty;
            string officePhoneNo = string.Empty;
            string officeExtensionNo = string.Empty;
            string officeFaxNo = string.Empty;
            string homePhoneNo = string.Empty;
            string homeFaxNo = string.Empty;
            string annualIncome = string.Empty;
            string pan1 = string.Empty;
            string pan2 = string.Empty;
            string pan3 = string.Empty;
            string emailId = string.Empty;
            //string dob1 = string.Empty;
            //string dob2 = string.Empty;
            //string dob3 = string.Empty;
            //string guardianDOB = string.Empty;
            uploadCommonBo = new UploadCommonBo();
            GridFooterItem footerRow = (GridFooterItem)gvProfileIncreamenetReject.MasterTableView.GetItems(GridItemType.Footer)[0];
            foreach (GridDataItem dr in gvProfileIncreamenetReject.Items)
            {
                if (((TextBox)footerRow.FindControl("txtClientCodeFooter")).Text.Trim() == "")
                {
                    clientCode = ((TextBox)dr.FindControl("txtClientCode")).Text;
                }
                else
                {
                    clientCode = ((TextBox)footerRow.FindControl("txtClientCodeFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtCityFooter")).Text.Trim() == "")
                {
                    city = ((TextBox)dr.FindControl("txtCity")).Text;
                }
                else
                {
                    city = ((TextBox)footerRow.FindControl("txtCityFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtStateFooter")).Text.Trim() == "")
                {
                    state = ((TextBox)dr.FindControl("txtState")).Text;
                }
                else
                {
                    state = ((TextBox)footerRow.FindControl("txtStateFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtPinCodeFooter")).Text.Trim() == "")
                {
                    pincode = ((TextBox)dr.FindControl("txtPinCode")).Text;
                }
                else
                {
                    pincode = ((TextBox)footerRow.FindControl("txtPinCodeFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtMobileNoFooter")).Text.Trim() == "")
                {
                    mobileno = ((TextBox)dr.FindControl("txtMobileNo")).Text;
                }
                else
                {
                    mobileno = ((TextBox)footerRow.FindControl("txtMobileNoFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtOccupationFooter")).Text.Trim() == "")
                {
                    occupation = ((TextBox)dr.FindControl("txtOccupation")).Text;
                }
                else
                {
                    occupation = ((TextBox)footerRow.FindControl("txtOccupationFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtAccountTypeFooter")).Text.Trim() == "")
                {
                    accounttype = ((TextBox)dr.FindControl("txtAccountType")).Text;
                }
                else
                {
                    accounttype = ((TextBox)footerRow.FindControl("txtAccountTypeFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtBankNameFooter")).Text.Trim() == "")
                {
                    bankname = ((TextBox)dr.FindControl("txtBankName")).Text;
                }
                else
                {
                    bankname = ((TextBox)footerRow.FindControl("txtBankNameFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtPersonalStatusFooter")).Text.Trim() == "")
                {
                    personalstatus = ((TextBox)dr.FindControl("txtPersonalStatus")).Text;
                }
                else
                {
                    personalstatus = ((TextBox)footerRow.FindControl("txtPersonalStatusFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtAddress1Footer")).Text.Trim() == "")
                {
                    address1 = ((TextBox)dr.FindControl("txtAddress1")).Text;
                }
                else
                {
                    address1 = ((TextBox)footerRow.FindControl("txtAddress1Footer")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtAddress2Footer")).Text.Trim() == "")
                {
                    address2 = ((TextBox)dr.FindControl("txtAddress2")).Text;
                }
                else
                {
                    address2 = ((TextBox)footerRow.FindControl("txtAddress2Footer")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtAddress3Footer")).Text.Trim() == "")
                {
                    address3 = ((TextBox)dr.FindControl("txtAddress3")).Text;
                }
                else
                {
                    address3 = ((TextBox)footerRow.FindControl("txtAddress3Footer")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtCountryFooter")).Text.Trim() == "")
                {
                    country = ((TextBox)dr.FindControl("txtCountry")).Text;
                }
                else
                {
                    country = ((TextBox)footerRow.FindControl("txtCountryFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtOfficePhoneNoFooter")).Text.Trim() == "")
                {
                    officePhoneNo = ((TextBox)dr.FindControl("txtOfficePhoneNo")).Text;
                }
                else
                {
                    officePhoneNo = ((TextBox)footerRow.FindControl("txtOfficePhoneNoFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtOfficeExtensionNoFooter")).Text.Trim() == "")
                {
                    officeExtensionNo = ((TextBox)dr.FindControl("txtOfficeExtensionNo")).Text;
                }
                else
                {
                    officeExtensionNo = ((TextBox)footerRow.FindControl("txtOfficeExtensionNoFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtOfficeFaxNoFooter")).Text.Trim() == "")
                {
                    officeFaxNo = ((TextBox)dr.FindControl("txtOfficeFaxNo")).Text;
                }
                else
                {
                    officeFaxNo = ((TextBox)footerRow.FindControl("txtOfficeFaxNoFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtHomePhoneNoFooter")).Text.Trim() == "")
                {
                    homePhoneNo = ((TextBox)dr.FindControl("txtHomePhoneNo")).Text;
                }
                else
                {
                    homePhoneNo = ((TextBox)footerRow.FindControl("txtHomePhoneNoFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtHomeFaxNoFooter")).Text.Trim() == "")
                {
                    homeFaxNo = ((TextBox)dr.FindControl("txtHomeFaxNo")).Text;
                }
                else
                {
                    homeFaxNo = ((TextBox)footerRow.FindControl("txtHomeFaxNoFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtAnnualIncomeFooter")).Text.Trim() == "")
                {
                    annualIncome = ((TextBox)dr.FindControl("txtAnnualIncome")).Text;
                }
                else
                {
                    annualIncome = ((TextBox)footerRow.FindControl("txtAnnualIncomeFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtPANNO1Footer")).Text.Trim() == "")
                {
                    pan1 = ((TextBox)dr.FindControl("txtPANNO1")).Text;
                }
                else
                {
                    pan1 = ((TextBox)footerRow.FindControl("txtPANNO1Footer")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtPANNO2Footer")).Text.Trim() == "")
                {
                    pan2 = ((TextBox)dr.FindControl("txtPANNO2")).Text;
                }
                else
                {
                    pan2 = ((TextBox)footerRow.FindControl("txtPANNO2Footer")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtPANNO3Footer")).Text.Trim() == "")
                {
                    pan3 = ((TextBox)dr.FindControl("txtPANNO3")).Text;
                }
                else
                {
                    pan3 = ((TextBox)footerRow.FindControl("txtPANNO3Footer")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtEmailIdFooter")).Text.Trim() == "")
                {
                    emailId = ((TextBox)dr.FindControl("txtEmailId")).Text;
                }
                else
                {
                    emailId = ((TextBox)footerRow.FindControl("txtEmailIdFooter")).Text;
                }
                //if (((TextBox)footerRow.FindControl("txtDOB1Footer")).Text.Trim() == "")
                //{
                //    dob1 = ((TextBox)dr.FindControl("txtDOB1")).Text;
                //}
                //else
                //{
                //    dob1 = ((TextBox)footerRow.FindControl("txtDOB1Footer")).Text;
                //}
                //if (((TextBox)footerRow.FindControl("txtDOB2Footer")).Text.Trim() == "")
                //{
                //    dob2 = ((TextBox)dr.FindControl("txtDOB2")).Text;
                //}
                //else
                //{
                //    dob2 = ((TextBox)footerRow.FindControl("txtDOB2Footer")).Text;
                //}
                // if (((TextBox)footerRow.FindControl("txtDOB3Footer")).Text.Trim() == "")
                //{
                //    dob3 = ((TextBox)dr.FindControl("txtDOB3")).Text;
                //}
                //else
                //{
                //    dob3 = ((TextBox)footerRow.FindControl("txtDOB3Footer")).Text;
                //}
                // if (((TextBox)footerRow.FindControl("txtGuardianDOBFooter")).Text.Trim() == "")
                //{
                //    guardianDOB = ((TextBox)dr.FindControl("txtGuardianDOB")).Text;
                //}
                //else
                //{
                //    guardianDOB = ((TextBox)footerRow.FindControl("txtGuardianDOBFooter")).Text;
                //}
                CheckBox checkBox = (CheckBox)dr.FindControl("chkId");
                if (checkBox.Checked == true)
                {
                    int selectedRow = 0;
                    GridDataItem gdi;
                    gdi = (GridDataItem)checkBox.NamingContainer;
                    selectedRow = gdi.ItemIndex + 1;
                    Id = int.Parse((gvProfileIncreamenetReject.MasterTableView.DataKeyValues[selectedRow - 1]["ID"].ToString()));
                    tableNo = int.Parse((gvProfileIncreamenetReject.MasterTableView.DataKeyValues[selectedRow - 1]["TableNo"].ToString()));
                    blResult = uploadCommonBo.UpdateRequestRejects(clientCode, Id, tableNo, city, state, pincode, mobileno, occupation, accounttype, bankname, personalstatus, address1, address2, address3, country, officePhoneNo, officeExtensionNo, officeFaxNo, homePhoneNo, homeFaxNo, annualIncome, pan1, pan2, pan3, emailId);

                }

            }

            if (Request.QueryString["ReqId"] != null)
                reqId = Int32.Parse(Request.QueryString["ReqId"].ToString());
            GetProfileIncreamentRejection(reqId);
        }
        protected void gvProfileIncreamenetReject_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtRequests = new DataTable();
            DataSet dtProcessLogDetails = new DataSet();
            //dtRequests = (DataTable)Cache[userVo.UserId.ToString() + "RequestReject"];
            dtRequests = (DataTable)Cache["RequestReject" + userVo.UserId.ToString()];
            if (dtRequests != null)
            {
                gvProfileIncreamenetReject.DataSource = dtRequests;
            }

        }
        protected void btnReProcess_Click(object sender, EventArgs e)
        {
            int reprocess;
            if (Request.QueryString["ReqId"] != null)
            {
                reqId = Int32.Parse(Request.QueryString["ReqId"].ToString());
                reprocess = uploadCommonBo.SetRequestParentreqId(reqId, userVo.UserId);
                if (reprocess > 0)
                {
                    msgReprocessincomplete.Visible = true;
                    msgReprocessincomplete.InnerText = "Request already exists";
                }
                else
                {
                    msgReprocessComplete.Visible = true;
                    msgReprocessComplete.InnerText = "ReProcess SuccessFully Done";
                }
            }

        }
        protected void btnDeleteStatus_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (GridDataItem gvr in this.gvProfileIncreamenetReject.Items)
            {
                if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
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

                RejectedRequestDelete();
                //NeedSource();
                gvProfileIncreamenetReject.MasterTableView.Rebind();
                msgReprocessComplete.Visible = false;
                msgReprocessincomplete.Visible = false;
                //    rejectedRecordsBo = new RejectedRecordsBo();
                //    rejectedRecordsBo.DeleteMFTransactionStaging(StagingID);
                //    if (hdnProcessIdFilter.Value != "")
                //    {
                //        ProcessId = int.Parse(hdnProcessIdFilter.Value);
                //    }
                //    BindEquityTransactionGrid(ProcessId);
            }
        }
        private void RejectedRequestDelete()
        {
            int Id = 0;
            int tableNo = 0;
            foreach (GridDataItem gvr in this.gvProfileIncreamenetReject.Items)
            {
                CheckBox checkBox = (CheckBox)gvr.FindControl("chkId");
                if (checkBox.Checked)
                {
                    int selectedRow = 0;
                    GridDataItem gdi;
                    gdi = (GridDataItem)checkBox.NamingContainer;
                    selectedRow = gdi.ItemIndex + 1;
                    Id = int.Parse((gvProfileIncreamenetReject.MasterTableView.DataKeyValues[selectedRow - 1]["ID"].ToString()));
                    tableNo = int.Parse((gvProfileIncreamenetReject.MasterTableView.DataKeyValues[selectedRow - 1]["TableNo"].ToString()));
                    uploadCommonBo.DeleteRequestRejected(Id, tableNo);
                }

            }
            if (Request.QueryString["ReqId"] != null)
                reqId = Int32.Parse(Request.QueryString["ReqId"].ToString());
            GetProfileIncreamentRejection(reqId);
        }
        protected void btnDataTranslationMapping_Click(object sender, EventArgs e)
        {
            Response.Redirect("ControlHost.aspx?pageid=ManageLookups", false);

        }
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            //  gvIPOOrderBook.MasterTableView.DetailTables[0].HierarchyDefaultExpanded = true;
            gvProfileIncreamenetReject.MasterTableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
            gvProfileIncreamenetReject.ExportSettings.OpenInNewWindow = true;
            gvProfileIncreamenetReject.ExportSettings.IgnorePaging = true;
            gvProfileIncreamenetReject.ExportSettings.HideStructureColumns = true;
            gvProfileIncreamenetReject.ExportSettings.ExportOnlyData = true;
            gvProfileIncreamenetReject.ExportSettings.FileName = "Upload Rejection";
            gvProfileIncreamenetReject.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvProfileIncreamenetReject.MasterTableView.ExportToExcel();

        }
    }
}
