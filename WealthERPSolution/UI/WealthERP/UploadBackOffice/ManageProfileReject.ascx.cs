using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
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
        DataSet dsRejectedRecords = new DataSet();
        DataTable dtReject1 = new DataTable();
        DataTable dtReject2 = new DataTable();
        int reqId;
        int transactionId;
        string rcbType = string.Empty;
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
                if (transactionId == 10)
                {
                    rgKycRejectlist.DataSource = dtReqReje;
                    rgKycRejectlist.DataBind();
                    rgKycRejectlist.Visible = true;
                    gvProfileIncreamenetReject.Visible = false;
                    gvSIPReject.Visible = false;
                    btnDelete.Visible = false;
                    btnReProcess.Visible = false;
                    LinkButton2.Visible = false;
                }
                if (transactionId == 13)
                {
                    gvProfileIncreamenetReject.Visible = false;
                    rgKycRejectlist.Visible = false;
                    gvSIPReject.Visible = true;
                    gvSIPReject.MasterTableView.GetColumn("ReqId").Visible = true;
                    gvSIPReject.MasterTableView.GetColumn("InvName").Visible = true;
                    gvSIPReject.MasterTableView.GetColumn("Pan").Visible = true;
                    gvSIPReject.MasterTableView.GetColumn("FolioNo").Visible = true;
                    gvSIPReject.MasterTableView.GetColumn("Scheme").Visible = true;
                    gvSIPReject.MasterTableView.GetColumn("AUTOTRNO").Visible = true;
                    gvSIPReject.MasterTableView.GetColumn("AUTTRNTYP").Visible = true;
                    gvSIPReject.MasterTableView.GetColumn("REGDATE").Visible = true;
                    gvSIPReject.MasterTableView.GetColumn("AUTOAMOUN").Visible = true;
                    gvSIPReject.MasterTableView.GetColumn("Product").Visible = true;
                  
                    
                    btnDelete.Visible = false;
                    Button1.Visible = true;
                }
                //else
                //{
                //    gvProfileIncreamenetReject.DataSource = dtReqReje;
                //    gvProfileIncreamenetReject.DataBind();

                //    gvProfileIncreamenetReject.Visible = true;
                //}
                if (transactionId == 3 || transactionId == 4)
                {
                    gvSIPReject.Visible = false;
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
                    gvSIPReject.Visible = false;
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
                else if (transactionId == 9 )
                {
                    gvSIPReject.Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("ProductCode").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("FolioNo").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrLine1").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrLine2").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrLine3").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrPinCode").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrState").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BranchAdrCountry").Visible = false;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("BrokerCode").Visible = true;
                    gvProfileIncreamenetReject.MasterTableView.GetColumn("SchemeName").Visible = true;
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
            string transactionType = string.Empty;
            string transactionNature = string.Empty;
            string transactionHead = string.Empty;
            string transactionDescription = string.Empty;
            string productCode = string.Empty;
            string accountNo = string.Empty;
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
                if (((TextBox)footerRow.FindControl("txtTransactionTypeFooter")).Text.Trim() == "")
                {
                    transactionType = ((TextBox)dr.FindControl("txtTransactionType")).Text;
                }
                else
                {
                    transactionType = ((TextBox)footerRow.FindControl("txtTransactionTypeFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtTransactionNatureFooter")).Text.Trim() == "")
                {
                    transactionNature = ((TextBox)dr.FindControl("txtTransactionNature")).Text;
                }
                else
                {
                    transactionNature = ((TextBox)footerRow.FindControl("txtTransactionNatureFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtTransactionHeadFooter")).Text.Trim() == "")
                {
                    transactionHead = ((TextBox)dr.FindControl("txtTransactionHead")).Text;
                }
                else
                {
                    transactionHead = ((TextBox)footerRow.FindControl("txtTransactionHeadFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtTransactionDescriptionFooter")).Text.Trim() == "")
                {
                    transactionDescription = ((TextBox)dr.FindControl("txtTransactionDescription")).Text;
                }
                else
                {
                    transactionDescription = ((TextBox)footerRow.FindControl("txtTransactionDescriptionFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtProductCodeFooter")).Text.Trim() == "")
                {
                    productCode = ((TextBox)dr.FindControl("txtProductCode")).Text;
                }
                else
                {
                    productCode = ((TextBox)footerRow.FindControl("txtProductCodeFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtAccountNoFooter")).Text.Trim() == "")
                {
                    accountNo = ((TextBox)dr.FindControl("txtAccountNo")).Text;
                }
                else
                {
                    accountNo = ((TextBox)footerRow.FindControl("txtAccountNoFooter")).Text;
                }
                CheckBox checkBox = (CheckBox)dr.FindControl("chkId");
                if (checkBox.Checked == true)
                {
                    int selectedRow = 0;
                    GridDataItem gdi;
                    gdi = (GridDataItem)checkBox.NamingContainer;
                    selectedRow = gdi.ItemIndex + 1;
                    Id = int.Parse((gvProfileIncreamenetReject.MasterTableView.DataKeyValues[selectedRow - 1]["ID"].ToString()));
                    tableNo = int.Parse((gvProfileIncreamenetReject.MasterTableView.DataKeyValues[selectedRow - 1]["TableNo"].ToString()));
                    blResult = uploadCommonBo.UpdateRequestRejects(clientCode, Id, tableNo, city, state, pincode, mobileno, occupation, accounttype, bankname, personalstatus, address1, address2, address3, country, officePhoneNo, officeExtensionNo, officeFaxNo, homePhoneNo, homeFaxNo, annualIncome, pan1, pan2, pan3, emailId, transactionType, transactionNature, transactionHead, transactionDescription, productCode, accountNo);

                }

            }

            if (Request.QueryString["ReqId"] != null)
            {
                DataTable dtRequests = new DataTable();
                DataSet dtProcessLogDetails = new DataSet();

                reqId = Int32.Parse(Request.QueryString["ReqId"].ToString());
                GetProfileIncreamentRejection(reqId);
                dtRequests = (DataTable)Cache["RequestReject" + userVo.UserId.ToString()];
                {
                    if (ViewState["RejectReason"] != null)
                        rcbType = ViewState["RejectReason"].ToString();
                    if (!string.IsNullOrEmpty(rcbType))
                    {
                        DataView dvStaffList = new DataView(dtRequests, "RejectedReasonDescription = '" + rcbType + "'", "", DataViewRowState.CurrentRows);
                        gvProfileIncreamenetReject.DataSource = dvStaffList.ToTable();
                        GridColumn column = gvProfileIncreamenetReject.MasterTableView.GetColumnSafe("RejectedReasonDescription");
                        column.CurrentFilterFunction = GridKnownFunction.Contains;
                        gvProfileIncreamenetReject.MasterTableView.Rebind();

                    }
                    else
                    {
                        GetProfileIncreamentRejection(reqId);
                    }
                }
            }
        }
        protected void btnSave1_Click(object sender, EventArgs e)
        {
            string pan = string.Empty;
            string transactionType = string.Empty;
            string productCode = string.Empty;
          
            bool blResult1 = false;
            int Id = 0;
            int tableNo = 0;
            uploadCommonBo = new UploadCommonBo();
            GridFooterItem footerRow = (GridFooterItem)gvSIPReject.MasterTableView.GetItems(GridItemType.Footer)[0];
            foreach (GridDataItem dr in gvSIPReject.Items)
            {
                if (((TextBox)footerRow.FindControl("txtPANNOFooter")).Text.Trim() == "")
                {
                    pan = ((TextBox)dr.FindControl("txtPANNO")).Text;
                }
                else
                {
                    pan = ((TextBox)footerRow.FindControl("txtPANNOFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtAUTTRNTYPFooter")).Text.Trim() == "")
                {
                    transactionType = ((TextBox)dr.FindControl("txtAUTTRNTYP")).Text;
                }
                else
                {
                    transactionType = ((TextBox)footerRow.FindControl("txtAUTTRNTYPFooter")).Text;
                }
                if (((TextBox)footerRow.FindControl("txtProductFooter")).Text.Trim() == "")
                {
                    productCode = ((TextBox)dr.FindControl("txtProduct")).Text;
                }
                else
                {
                    productCode = ((TextBox)footerRow.FindControl("txtProductFooter")).Text;
                }
               
                 CheckBox checkBox = (CheckBox)dr.FindControl("chkId");
                if (checkBox.Checked == true)
                {
                    int selectedRow = 0;
                    GridDataItem gdi;
                    gdi = (GridDataItem)checkBox.NamingContainer;
                    selectedRow = gdi.ItemIndex + 1;
                    Id = int.Parse((gvSIPReject.MasterTableView.DataKeyValues[selectedRow - 1]["InputId"].ToString()));
                    tableNo = int.Parse((gvSIPReject.MasterTableView.DataKeyValues[selectedRow - 1]["TableNo"].ToString()));
                    blResult1 = uploadCommonBo.UpdateSIPRequestRejects(pan,  Id,  tableNo, transactionType, productCode);

                }

            }

            if (Request.QueryString["ReqId"] != null)
            {
                DataTable dtRequests = new DataTable();
                DataSet dtProcessLogDetails = new DataSet();

                reqId = Int32.Parse(Request.QueryString["ReqId"].ToString());
                GetProfileIncreamentRejection(reqId);
                dtRequests = (DataTable)Cache["RequestReject" + userVo.UserId.ToString()];
                gvSIPReject.Rebind();
              
            }
            }
        
        protected void rgKycRejectlist_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            DataTable dtRequests = new DataTable();
            DataSet dtProcessLogDetails = new DataSet();
            //dtRequests = (DataTable)Cache[userVo.UserId.ToString() + "RequestReject"];
            dtRequests = (DataTable)Cache["RequestReject" + userVo.UserId.ToString()];
            if (dtRequests != null)
            {
                    rgKycRejectlist.DataSource = dtRequests;

            }
        }
        protected void gvProfileIncreamenetReject_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            DataTable dtRequests = new DataTable();
            DataSet dtProcessLogDetails = new DataSet();
            //dtRequests = (DataTable)Cache[userVo.UserId.ToString() + "RequestReject"];
            dtRequests = (DataTable)Cache["RequestReject" + userVo.UserId.ToString()];
            if (dtRequests != null)
            {

                if (ViewState["RejectReason"] != null)
                    rcbType = ViewState["RejectReason"].ToString();
                if (!string.IsNullOrEmpty(rcbType))
                {
                    DataView dvStaffList = new DataView(dtRequests, "RejectedReasonDescription = '" + rcbType + "'", "", DataViewRowState.CurrentRows);
                    gvProfileIncreamenetReject.DataSource = dvStaffList.ToTable();

                }
                else
                {
                    gvProfileIncreamenetReject.DataSource = dtRequests;
                }

            }
        }
        protected void gvSIPReject_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            DataTable dtRequests = new DataTable();
            DataSet dtProcessLogDetails = new DataSet();
            //dtRequests = (DataTable)Cache[userVo.UserId.ToString() + "RequestReject"];
            dtRequests = (DataTable)Cache["RequestReject" + userVo.UserId.ToString()];
            if (dtRequests != null)
            {

                if (ViewState["RejectReason"] != null)
                    rcbType = ViewState["RejectReason"].ToString();
                if (!string.IsNullOrEmpty(rcbType))
                {
                    DataView dvStaffList = new DataView(dtRequests, "RejectedReasonDescription = '" + rcbType + "'", "", DataViewRowState.CurrentRows);
                    gvSIPReject.DataSource = dvStaffList.ToTable();

                }
                else
                {
                    gvSIPReject.DataSource = dtRequests;
                }

            }
        }
       
        protected void gvProfileIncreamenetReject_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && e.Item.ItemIndex == -1)
            {
                GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                RadComboBox RadComboBoxIN = (RadComboBox)filterItem.FindControl("RadComboBoxRR");
                dtReject1 = (DataTable)Cache["RequestReject" + userVo.UserId.ToString()];
                Session["dt"] = dtReject1;
                DataTable dtcustMIS = new DataTable();
                dtcustMIS.Columns.Add("RejectedReasonDescription");
                DataRow drcustMIS;
                foreach (DataRow dr in dtReject1.Rows)
                {
                    drcustMIS = dtcustMIS.NewRow();
                    string str = dr["RejectedReasonDescription"].ToString();
                    drcustMIS["RejectedReasonDescription"] = dr["RejectedReasonDescription"].ToString();
                    dtcustMIS.Rows.Add(drcustMIS);
                }
                DataView view = new DataView(dtReject1);
                DataTable distinctValues = view.ToTable(true, "RejectedReasonDescription");
                DataColumn dc = new DataColumn("RejectedReasonDescriptionText");
                distinctValues.Columns.Add(dc);
                foreach (DataRow dr in distinctValues.Rows)
                {
                    dr["RejectedReasonDescriptionText"] = dr["RejectedReasonDescription"].ToString().Replace("<br />", "");
                }
                RadComboBoxIN.DataSource = distinctValues;
                RadComboBoxIN.DataValueField = distinctValues.Columns["RejectedReasonDescription"].ToString();
                RadComboBoxIN.DataTextField = distinctValues.Columns["RejectedReasonDescriptionText"].ToString();
                RadComboBoxIN.DataBind();
            }

            // }

        }

        protected void btnReProcess_Click(object sender, EventArgs e)
        {
            int reprocess;
            if (Request.QueryString["ReqId"] != null)
            {
                reqId = Int32.Parse(Request.QueryString["ReqId"].ToString());
                transactionId = Convert.ToInt32(Request.QueryString["transactionId"]);
                reprocess = uploadCommonBo.SetRequestParentreqId(reqId, userVo.UserId, transactionId);
                if (reprocess > 0)
                {
                    msgReprocessincomplete.Visible = true;
                    msgReprocessincomplete.InnerText = "Request already exists";
                }
                else
                {
                    msgReprocessComplete.Visible = true;
                    msgReprocessComplete.InnerText = "Reprocess Successfully Done";
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
        protected void btnDeleteSIPStatus_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (GridDataItem gvr in this.gvSIPReject.Items)
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

                RejectedSIPRequestDelete();
                //NeedSource();
                gvProfileIncreamenetReject.Visible = false;
                gvSIPReject.MasterTableView.Rebind();
                msgReprocessComplete.Visible = false;
                msgReprocessincomplete.Visible = false;
                 
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
        private void RejectedSIPRequestDelete()
        {
            int Id = 0;
            int tableNo = 0;
            foreach (GridDataItem gvr in this.gvSIPReject.Items)
            {
                CheckBox checkBox = (CheckBox)gvr.FindControl("chkId");
                if (checkBox.Checked)
                {
                    int selectedRow = 0;
                    GridDataItem gdi;
                    gdi = (GridDataItem)checkBox.NamingContainer;
                    selectedRow = gdi.ItemIndex + 1;
                    Id = int.Parse((gvSIPReject.MasterTableView.DataKeyValues[selectedRow - 1]["InputId"].ToString()));
                    tableNo = int.Parse((gvSIPReject.MasterTableView.DataKeyValues[selectedRow - 1]["TableNo"].ToString()));
                    uploadCommonBo.DeleteSIPRequestRejected(Id, tableNo);
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
            transactionId = Convert.ToInt32(Request.QueryString["transactionId"]);
            reqId = Convert.ToInt32(Request.QueryString["ReqId"]);
            if (transactionId == 10)
            {
                rgKycRejectlist.MasterTableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
                rgKycRejectlist.ExportSettings.OpenInNewWindow = true;
                rgKycRejectlist.ExportSettings.IgnorePaging = true;
                rgKycRejectlist.ExportSettings.HideStructureColumns = true;
                rgKycRejectlist.ExportSettings.ExportOnlyData = true;
                rgKycRejectlist.ExportSettings.FileName = reqId.ToString()+"_Kyc Upload Rejection List";
                rgKycRejectlist.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                rgKycRejectlist.MasterTableView.ExportToExcel();
            }
            if (transactionId == 13)
            {
                gvSIPReject.MasterTableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
                gvSIPReject.ExportSettings.OpenInNewWindow = true;
                gvSIPReject.ExportSettings.IgnorePaging = true;
                gvSIPReject.ExportSettings.HideStructureColumns = true;
                gvSIPReject.ExportSettings.ExportOnlyData = true;
                gvSIPReject.ExportSettings.FileName = reqId.ToString() + "_SIP Upload Rejection List";
                gvSIPReject.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gvSIPReject.MasterTableView.ExportToExcel();
            }
            else
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
        protected void rcbContinents1_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            ////persist the combo selected value  
            if (ViewState["RejectReason"] != null)
            {

                Combo.SelectedValue = ViewState["RejectReason"].ToString();
            }

        }
        protected void RadComboBoxRR_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

            RadComboBox dropdown = o as RadComboBox;
            ViewState["RejectReason"] = dropdown.SelectedValue.ToString();
            if (ViewState["RejectReason"] != null)
            {
                GridColumn column = gvProfileIncreamenetReject.MasterTableView.GetColumnSafe("RejectedReasonDescription");
                column.CurrentFilterFunction = GridKnownFunction.Contains;
                //column.CurrentFilterValue = dropdown.SelectedValue.ToString();
                gvProfileIncreamenetReject.MasterTableView.Rebind();

            }
            else
            {
                // gvWERPTrans.MasterTableView.FilterExpression = "0";
                GridColumn column = gvProfileIncreamenetReject.MasterTableView.GetColumnSafe("RejectedReasonDescription");
                column.CurrentFilterFunction = GridKnownFunction.Contains;
                gvProfileIncreamenetReject.MasterTableView.Rebind();


            }

        }
        protected void gvProfileIncreamenetReject_PreRender(object sender, EventArgs e)
        {
            if (gvProfileIncreamenetReject.MasterTableView.FilterExpression != string.Empty)
            {
                RefreshCombos();
            }
        }
        protected void RefreshCombos()
        {
            dtReject1 = (DataTable)Cache["RequestReject" + userVo.UserId.ToString()];
            DataView view = new DataView(dtReject1);
            DataTable distinctValues = view.ToTable();
            DataRow[] rows = distinctValues.Select(gvProfileIncreamenetReject.MasterTableView.FilterExpression.ToString());
            gvProfileIncreamenetReject.MasterTableView.Rebind();
        }
    }
}
