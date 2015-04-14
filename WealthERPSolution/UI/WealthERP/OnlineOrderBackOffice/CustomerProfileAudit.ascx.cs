using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoUser;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using System.Data;
using WealthERP.Customer;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.Base;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Text;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoCommon;
using VoAdvisorProfiling;
using BoSuperAdmin;
using VOAssociates;
using BOAssociates;
using Telerik.Web.UI;
using System.Text.RegularExpressions;
using BoOnlineOrderManagement;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class CustomerProfileAudit : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        AssociatesVO associatesVo = new AssociatesVO();
        AdvisorPreferenceVo advisorPrefernceVo = new AdvisorPreferenceVo();
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            advisorPrefernceVo = (AdvisorPreferenceVo)Session["AdvisorPreferenceVo"];
            rmVo = (RMVo)Session["rmVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            associatesVo = (AssociatesVO)Session["associatesVo"];
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";

                txtSchemeName_AutoCompleteExtender.ServiceMethod = "GetSchemeList";
                txtStaffName_AutoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtStaffName_AutoCompleteExtender.ServiceMethod = "GetStaffName";
                txtPansearch_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtPansearch_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerPan";
                txtClientCode_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtClientCode_autoCompleteExtender.ServiceMethod = "GetCustCode";
                txtAssociateName_AutoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtAssociateName_AutoCompleteExtender.ServiceMethod = "GetAssociateName";
                txtSubBrokerCode_AutoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtSubBrokerCode_AutoCompleteExtender.ServiceMethod = "GetAgentCodeDetails";
                txtSystematicID_AutoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtSystematicID_AutoCompleteExtender.ServiceMethod = "GetSystematicId";
                //txtNcdIssueSetup_AutoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                if (ddlProduct.SelectedValue == "Bonds")
                {
                     txtNcdIssueSetup_AutoCompleteExtender.ContextKey = ddlCategory.SelectedValue + "/" + adviserVo.advisorId.ToString();
                }
                else if (ddlProduct.SelectedValue == "IP")
                {
                     txtNcdIssueSetup_AutoCompleteExtender.ContextKey = "FIFIIP" + "/" + adviserVo.advisorId.ToString();
                }
                txtNcdIssueSetup_AutoCompleteExtender.ServiceMethod = "GetNcdIssueName";
             
            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                txtSchemeName_AutoCompleteExtender.ServiceMethod = "GetSchemeList";
                txtStaffName_AutoCompleteExtender.ServiceMethod = "GetStaffName";
                txtAssociateName_AutoCompleteExtender.ServiceMethod = "GetAssociateName";
                txtSubBrokerCode_AutoCompleteExtender.ServiceMethod = "GetAgentCodeDetails";
                txtSystematicID_AutoCompleteExtender.ServiceMethod = "GetSystematicId";
            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                txtSchemeName_AutoCompleteExtender.ServiceMethod = "GetSchemeList";
                txtStaffName_AutoCompleteExtender.ServiceMethod = "GetStaffName";
                txtAssociateName_AutoCompleteExtender.ServiceMethod = "GetAssociateName";
                txtSubBrokerCode_AutoCompleteExtender.ServiceMethod = "GetAgentCodeDetails";
                txtSystematicID_AutoCompleteExtender.ServiceMethod = "GetSystematicId";
            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                txtPansearch_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtPansearch_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerPan";
                txtClientCode_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtClientCode_autoCompleteExtender.ServiceMethod = "GetCustCode";
                txtAssociateName_AutoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtAssociateName_AutoCompleteExtender.ServiceMethod = "GetAssociateName";
                txtSubBrokerCode_AutoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtSubBrokerCode_AutoCompleteExtender.ServiceMethod = "GetAgentCodeDetails";
                txtSystematicID_AutoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtSystematicID_AutoCompleteExtender.ServiceMethod = "GetSystematicId";
                txtNcdIssueSetup_AutoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtNcdIssueSetup_AutoCompleteExtender.ServiceMethod = "GetNcdIssueName";

            }

        }
        protected void hdnCustomerId_ValueChanged(object sender, EventArgs e)
        {



        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "CustomerProfile")
            {
                GetCustomerProfileAduditDetails();
            }
            else if (ddlType.SelectedValue == "Schemeplan")
            {
                GetSchemePlanAuditDetail();
            }
            else if (ddlType.SelectedValue == "NCDIssueSetup")
            {
                GetNcdIssueSetUp();
            }
            else if (ddlType.SelectedValue == "StaffDetails")
            {
                GetStaffAuditDetail();
            }
            else if (ddlType.SelectedValue == "AssociateDetails")
            {
                GetAssociateAuditDetail();
            }
            else
            {
                GetSystematicAuditDetails();
            }

        }
        protected void GetSystematicAuditDetails()
        {
            DataTable dtGetSystematicAuditDetails = new DataTable();
            DataSet dsGetSystematicAuditDetails = new DataSet();
            dsGetSystematicAuditDetails = customerBo.GetSystematicAuditDetails((!string.IsNullOrEmpty(hdnSystematicId.Value)) ? int.Parse(hdnSystematicId.Value) : 0, rdpFromModificationDate.SelectedDate.Value, rdpToDate.SelectedDate.Value, adviserVo.advisorId);
            dtGetSystematicAuditDetails = dsGetSystematicAuditDetails.Tables[0];
            if (dtGetSystematicAuditDetails.Rows.Count > 0)
            {
                if (Cache["SystematicAudit" + userVo.UserId] == null)
                {
                    Cache.Insert("SystematicAudit" + userVo.UserId, dtGetSystematicAuditDetails);
                }
                else
                {
                    Cache.Remove("SystematicAudit" + userVo.UserId);
                    Cache.Insert("SystematicAudit" + userVo.UserId, dtGetSystematicAuditDetails);
                }
                rdSystematicAudit.DataSource = dtGetSystematicAuditDetails;
                rdSystematicAudit.DataBind();
                //taSchemeAudit.Visible = true;
                tbSystematicId.Visible = true;
                rdSystematicAudit.Visible = true;
                tblSystematicId.Visible = true;
            }
            else
            {
                rdSystematicAudit.DataSource = dtGetSystematicAuditDetails;
                rdSystematicAudit.DataBind();
                //taSchemeAudit.Visible = true;
                tbSystematicId.Visible = true;
                rdSystematicAudit.Visible = true;
                tblSystematicId.Visible = true;
            }
        }
        protected void GetAssociateAuditDetail()
        {
            DataTable dtGetAssociateAuditDetail = new DataTable();
            DataSet dsGetAssociateAuditDetail = new DataSet();
            dsGetAssociateAuditDetail = customerBo.GetAssociateAuditDetail((!string.IsNullOrEmpty(hdnAssociateId.Value)) ? int.Parse(hdnAssociateId.Value) : 0, rdpFromModificationDate.SelectedDate.Value, rdpToDate.SelectedDate.Value, adviserVo.advisorId);
            dtGetAssociateAuditDetail = dsGetAssociateAuditDetail.Tables[0];
            if (dtGetAssociateAuditDetail.Rows.Count > 0)
            {
                if (Cache["AssociateAudit" + userVo.UserId] == null)
                {
                    Cache.Insert("AssociateAudit" + userVo.UserId, dtGetAssociateAuditDetail);
                }
                else
                {
                    Cache.Remove("AssociateAudit" + userVo.UserId);
                    Cache.Insert("AssociateAudit" + userVo.UserId, dtGetAssociateAuditDetail);
                }
                rdAssociateAudit.DataSource = dtGetAssociateAuditDetail;
                rdAssociateAudit.DataBind();
                //taSchemeAudit.Visible = true;
                tbAssociateAudit.Visible = true;
                rdAssociateAudit.Visible = true;
                tblAssociateAudit.Visible = true;
            }
            else
            {
                rdAssociateAudit.DataSource = dtGetAssociateAuditDetail;
                rdAssociateAudit.DataBind();
                //taSchemeAudit.Visible = true;
                tbAssociateAudit.Visible = true;
                rdAssociateAudit.Visible = true;
                tblAssociateAudit.Visible = true;
            }
        }
        protected void GetStaffAuditDetail()
        {
            DataTable dtGetStaffAuditDetail = new DataTable();
            DataSet dsGetStaffAuditDetail = new DataSet();
            dsGetStaffAuditDetail = customerBo.GetStaffAuditDetail((!string.IsNullOrEmpty(hdnStaffId.Value)) ? int.Parse(hdnStaffId.Value) : 0, rdpFromModificationDate.SelectedDate.Value, rdpToDate.SelectedDate.Value, adviserVo.advisorId);
            dtGetStaffAuditDetail = dsGetStaffAuditDetail.Tables[0];
            if (dtGetStaffAuditDetail.Rows.Count > 0)
            {
                if (Cache["StaffAudit" + userVo.UserId] == null)
                {
                    Cache.Insert("StaffAudit" + userVo.UserId, dtGetStaffAuditDetail);
                }
                else
                {
                    Cache.Remove("StaffAudit" + userVo.UserId);
                    Cache.Insert("StaffAudit" + userVo.UserId, dtGetStaffAuditDetail);
                }
                rdStaffAudit.DataSource = dtGetStaffAuditDetail;
                rdStaffAudit.DataBind();
                //taSchemeAudit.Visible = true;
                tbStaffAudit.Visible = true;
                rdStaffAudit.Visible = true;
                tblStaffAudit.Visible = true;
            }
            else
            {
                rdStaffAudit.DataSource = dtGetStaffAuditDetail;
                rdStaffAudit.DataBind();
                //taSchemeAudit.Visible = true;
                tbStaffAudit.Visible = true;
                rdStaffAudit.Visible = true;
                tblStaffAudit.Visible = true;
            }
        }
        protected void GetSchemePlanAuditDetail()
        {
            DataTable dtGetSchemePlanAuditDetail = new DataTable();
            DataSet dsGetCustomerProfileAuditData = new DataSet();
            dsGetCustomerProfileAuditData = customerBo.GetSchemePlanAuditDetails(int.Parse(hdnschemePlanId.Value), rdpFromModificationDate.SelectedDate.Value, rdpToDate.SelectedDate.Value);
            dtGetSchemePlanAuditDetail = dsGetCustomerProfileAuditData.Tables[0];
            if (dtGetSchemePlanAuditDetail.Rows.Count > 0)
            {
                if (Cache["SchemeAudit" + userVo.UserId] == null)
                {
                    Cache.Insert("SchemeAudit" + userVo.UserId, dtGetSchemePlanAuditDetail);
                }
                else
                {
                    Cache.Remove("SchemeAudit" + userVo.UserId);
                    Cache.Insert("SchemeAudit" + userVo.UserId, dtGetSchemePlanAuditDetail);
                }
                rdSchemeAudit.DataSource = dtGetSchemePlanAuditDetail;
                rdSchemeAudit.DataBind();
                taSchemeAudit.Visible = true;
                tblSchemePlan.Visible = true;
                rdSchemeAudit.Visible = true;
            }
            else
            {
                rdSchemeAudit.DataSource = dtGetSchemePlanAuditDetail;
                rdSchemeAudit.DataBind();
                taSchemeAudit.Visible = true;
                tblSchemePlan.Visible = true;
                rdSchemeAudit.Visible = true;
            }
        }
        protected void GetCustomerProfileAduditDetails()
        {
            tblProfileHeading.Visible = false;
            tblProfileData.Visible = false;
            tblCustomerBank.Visible = false;
            tblCustomerBankHeading.Visible = false;
            //tblCustomerDemat.Visible = false;
            pnlCustomerDemat.Visible = false;
            tblCustomerDematHeading.Visible = false;
            tblCustomerDematAssociates.Visible = false;
            tblCustomerDematAssociatesHeading.Visible = false;
            tableCustomerTransaction.Visible = false;
            tableTransaction.Visible = false;
            pnlCustomerProfile.Visible = false;
            DataSet dsGetCustomerProfileAuditData = new DataSet();
            dsGetCustomerProfileAuditData = customerBo.GetCustomerProfileAuditDetails((!string.IsNullOrEmpty(hdnCustomerId.Value)) ? int.Parse(hdnCustomerId.Value) : 0, rdpFromModificationDate.SelectedDate.Value, rdpToDate.SelectedDate.Value, adviserVo.advisorId, ddlAuditType.SelectedValue);
            switch (ddlAuditType.SelectedValue.ToString())
            {
                case "CP": rdCustomerProfile.DataSource = dsGetCustomerProfileAuditData.Tables[0];
                    rdCustomerProfile.DataBind();
                    tblProfileHeading.Visible = true;
                    tblProfileData.Visible = true;
                    rdCustomerProfile.Visible = true;
                    if (Cache["CustomerProfile" + userVo.UserId] != null) Cache.Remove("CustomerProfile" + userVo.UserId);
                    Cache.Insert("CustomerProfile" + userVo.UserId, dsGetCustomerProfileAuditData.Tables[0]);
                    break;
                case "CB": rdCustomerBank.DataSource = dsGetCustomerProfileAuditData.Tables[0];
                    rdCustomerBank.DataBind();
                    tblCustomerBank.Visible = true;
                    tblCustomerBankHeading.Visible = true;
                    rdCustomerBank.Visible = true;
                    if (Cache["CustomerBank" + userVo.UserId] != null) Cache.Remove("CustomerBank" + userVo.UserId);
                    Cache.Insert("CustomerBank" + userVo.UserId, dsGetCustomerProfileAuditData.Tables[0]);
                    break;
                case "CD": rdCustomerDemat.DataSource = dsGetCustomerProfileAuditData.Tables[0];
                    rdCustomerDemat.DataBind();
                    //tblCustomerDemat.Visible = true;
                    pnlCustomerDemat.Visible = true;
                    tblCustomerDematHeading.Visible = true;
                    rdCustomerDemat.Visible = true;
                    if (Cache["CustomerDemat" + userVo.UserId] != null) Cache.Remove("CustomerDemat" + userVo.UserId);
                    Cache.Insert("CustomerDemat" + userVo.UserId, dsGetCustomerProfileAuditData.Tables[0]);
                    break;
                case "CDA": rdCustomerDematAssociates.DataSource = dsGetCustomerProfileAuditData.Tables[0];
                    rdCustomerDematAssociates.DataBind();
                    tblCustomerDematAssociates.Visible = true;
                    tblCustomerDematAssociatesHeading.Visible = true;
                    rdCustomerDematAssociates.Visible = true;
                    if (Cache["CustomerDematAssociate" + adviserVo.advisorId] != null) Cache.Remove("CustomerDematAssociate" + userVo.UserId);
                    Cache.Insert("CustomerDematAssociate" + userVo.UserId, dsGetCustomerProfileAuditData.Tables[0]);
                    break;
                case "CTA": rdTransaction.DataSource = dsGetCustomerProfileAuditData.Tables[0];
                    rdTransaction.DataBind();
                    tableTransaction.Visible = true;
                    tableCustomerTransaction.Visible = true;
                    rdTransaction.Visible = true;
                    if (Cache["CustomerTransaction" + userVo.UserId] != null) Cache.Remove("CustomerTransaction" + userVo.UserId);
                    Cache.Insert("CustomerTransaction" + userVo.UserId, dsGetCustomerProfileAuditData.Tables[0]);
                    break;
                //if (ddlType.SelectedValue == "CustomerProfile")
                //{
                //    r
                //}
            }
        }
        protected void GetNcdIssueSetUp()
        {
            tblNcdIssueId.Visible = false;
            tbNcdIssueId.Visible = false;
            tblNcdCategory.Visible = false;
            tbNcdCategoryAudit.Visible = false;
            tblNcdIssueSeries.Visible = false;
            tbNcdIssueSeries.Visible = false;
            tblNcdSubCategory.Visible = false;
            tbNcdSubCategory.Visible = false;
            tblProfileData.Visible = false;
            DataSet dsGetNcdIssueSetupData = new DataSet();
            dsGetNcdIssueSetupData = customerBo.GetNcdIssueSetUp((!string.IsNullOrEmpty(hdnIssueId.Value)) ? int.Parse(hdnIssueId.Value) : 0, rdpFromModificationDate.SelectedDate.Value, rdpToDate.SelectedDate.Value, adviserVo.advisorId, ddlNcdIssueSetup.SelectedValue, ddlCategory.SelectedValue, ddlProduct.SelectedValue);
            switch (ddlNcdIssueSetup.SelectedValue.ToString())
            {
                case "NIS": rdNcdIssueAudit.DataSource = dsGetNcdIssueSetupData.Tables[0];
                    rdNcdIssueAudit.DataBind();
                    tblNcdIssueId.Visible = true;
                    tbNcdIssueId.Visible = true;
                    rdNcdIssueAudit.Visible = true;
                    if (Cache["NcdIssueAudit" + userVo.UserId] != null) Cache.Remove("NcdIssueAudit" + userVo.UserId);
                    Cache.Insert("NcdIssueAudit" + userVo.UserId, dsGetNcdIssueSetupData.Tables[0]);
                    break;
                case "IC": rdNcdCategoryAudit.DataSource = dsGetNcdIssueSetupData.Tables[0];
                    rdNcdCategoryAudit.DataBind();
                    tblNcdCategory.Visible = true;
                    tbNcdCategoryAudit.Visible = true;
                    rdNcdCategoryAudit.Visible = true;
                    rdNcdSubCategoryAudit.DataSource = dsGetNcdIssueSetupData.Tables[1];
                    rdNcdSubCategoryAudit.DataBind();
                    tblNcdSubCategory.Visible = true;
                    tbNcdSubCategory.Visible = true;
                    rdNcdSubCategoryAudit.Visible = true;
                    if (Cache["NcdCategoryAudit" + userVo.UserId] != null) Cache.Remove("NcdCategoryAudit" + userVo.UserId);
                    Cache.Insert("NcdCategoryAudit" + userVo.UserId, dsGetNcdIssueSetupData.Tables[0]);
                    if (Cache["NcdSubCategoryAudit" + userVo.UserId] != null) Cache.Remove("NcdSubCategoryAudit" + userVo.UserId);
                    Cache.Insert("NcdSubCategoryAudit" + userVo.UserId, dsGetNcdIssueSetupData.Tables[1]);
                    break;
                case "NS": rdNcdIssueSeries.DataSource = dsGetNcdIssueSetupData.Tables[0];
                    rdNcdIssueSeries.DataBind();
                    tblNcdIssueSeries.Visible = true;
                    tbNcdIssueSeries.Visible = true;
                    rdNcdIssueSeries.Visible = true;
                    if (Cache["NcdIssueSeries" + userVo.UserId] != null) Cache.Remove("NcdIssueSeries" + userVo.UserId);
                    Cache.Insert("NcdIssueSeries" + userVo.UserId, dsGetNcdIssueSetupData.Tables[0]);
                    break;

            }


        }
        protected void rdCustomerProfile_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtCustomerProfile = (DataTable)Cache["CustomerProfile" + userVo.UserId];

            if (dtCustomerProfile != null) rdCustomerProfile.DataSource = dtCustomerProfile;
        }
        protected void rdCustomerBank_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtCustomerBank = (DataTable)Cache["CustomerBank" + userVo.UserId];

            if (dtCustomerBank != null) rdCustomerBank.DataSource = dtCustomerBank;
        }
        protected void rdCustomerDemat_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtCustomerDemat = (DataTable)Cache["CustomerDemat" + userVo.UserId];

            if (dtCustomerDemat != null) rdCustomerDemat.DataSource = dtCustomerDemat;
        }
        protected void rdCustomerDematAssociates_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtCustomerDematAssociates = (DataTable)Cache["CustomerDematAssociate" + userVo.UserId];

            if (dtCustomerDematAssociates != null) rdCustomerDematAssociates.DataSource = dtCustomerDematAssociates;
        }
        protected void rdTransaction_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtCustomerTransaction = (DataTable)Cache["CustomerTransaction" + userVo.UserId];

            if (dtCustomerTransaction != null) rdTransaction.DataSource = dtCustomerTransaction;
        }
        protected void rdSchemeAudit_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtrdSchemeAudit = (DataTable)Cache["SchemeAudit" + userVo.UserId];
            if (dtrdSchemeAudit != null) rdSchemeAudit.DataSource = dtrdSchemeAudit;
        }
        protected void rdStaffAudit_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtrdStaffAudit = (DataTable)Cache["StaffAudit" + userVo.UserId];
            if (dtrdStaffAudit != null) rdStaffAudit.DataSource = dtrdStaffAudit;
        }
        protected void rdAssociateAudit_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtrdAssociateAudit = (DataTable)Cache["AssociateAudit" + userVo.UserId];
            if (dtrdAssociateAudit != null) rdAssociateAudit.DataSource = dtrdAssociateAudit;
        }
        protected void rdSystematicAudit_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtrdSystematicAudit = (DataTable)Cache["SystematicAudit" + userVo.UserId];
            if (dtrdSystematicAudit != null) rdSystematicAudit.DataSource = dtrdSystematicAudit;
        }
        protected void rdNcdIssueAudit_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtrdNcdIssueAudit = (DataTable)Cache["NcdIssueAudit" + userVo.UserId];
            if (dtrdNcdIssueAudit != null) rdNcdIssueAudit.DataSource = dtrdNcdIssueAudit;
        }
        protected void rdNcdCategoryAudit_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtrdNcdCategoryAudit = (DataTable)Cache["NcdCategoryAudit" + userVo.UserId];
            if (dtrdNcdCategoryAudit != null) rdNcdCategoryAudit.DataSource = dtrdNcdCategoryAudit;
        }
        protected void rdNcdSubCategoryAudit_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtrdNcdSubCategoryAudit = (DataTable)Cache["NcdSubCategoryAudit" + userVo.UserId];
            if (dtrdNcdSubCategoryAudit != null) rdNcdSubCategoryAudit.DataSource = dtrdNcdSubCategoryAudit;
        }
        protected void rdNcdIssueSeries_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtrdNcdIssueSeries = (DataTable)Cache["NcdIssueSeries" + userVo.UserId];
            if (dtrdNcdIssueSeries != null) rdNcdIssueSeries.DataSource = dtrdNcdIssueSeries;
        }
        protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "CustomerProfile")
            {
                trCustomer.Visible = true;
                tdTodate.Visible = true;
                tdFromDate.Visible = true;
                tdTodate1.Visible = true;
                tdFromDate1.Visible = true;
                tdCustomerAuditList.Visible = true;
                trSchemePlan.Visible = false;
                btnSubmit.Visible = true;
                tblSchemePlan.Visible = false;
                taSchemeAudit.Visible = false;
                rdSchemeAudit.Visible = false;
                trStaff.Visible = false;
                tbStaffAudit.Visible = false;
                rdStaffAudit.Visible = false;
                tblStaffAudit.Visible = false;
                trAssociates.Visible = false;
                tblAssociateAudit.Visible = false;
                tbAssociateAudit.Visible = false;
                rdAssociateAudit.Visible = false;
                trSystematicId.Visible = false;
                tblSystematicId.Visible = false;
                tbSystematicId.Visible = false;
                trNcdIssueSetup.Visible = false;
                tblNcdIssueId.Visible = false;
                tbNcdIssueId.Visible = false;
                tblNcdCategory.Visible = false;
                tbNcdCategoryAudit.Visible = false;
                tblNcdIssueSeries.Visible = false;
                tbNcdIssueSeries.Visible = false;
                tblNcdSubCategory.Visible = false;
                tbNcdSubCategory.Visible = false;
                trProduct.Visible = false;
                lb1IssuerCode.Visible = false;
                ddlCategory.Visible = false;
                trNcdIssueSetup.Visible = false;
                tdtxtNcdIssueSetup.Visible = false;
                ddlNcdOption.SelectedValue = "0";
                ddlProduct.SelectedValue = "0";
                
            }
            else if (ddlType.SelectedValue == "Schemeplan")
            {
                trSchemePlan.Visible = true;
                tdTodate.Visible = true;
                tdFromDate.Visible = true;
                tdTodate1.Visible = true;
                tdFromDate1.Visible = true;
                trCustomer.Visible = false;
                //tdCustomer1.Visible = false;
                tdCustomerAuditList.Visible = false;
                btnSubmit.Visible = true;
                tblProfileHeading.Visible = false;
                rdCustomerProfile.Visible = false;
                tblCustomerBankHeading.Visible = false;
                tblCustomerBank.Visible = false;
                rdCustomerBank.Visible = false;
                tblCustomerDematHeading.Visible = false;
                rdCustomerDemat.Visible = false;
                tblCustomerDematAssociatesHeading.Visible = false;
                tblCustomerDematAssociates.Visible = false;
                rdCustomerDematAssociates.Visible = false;
                tableCustomerTransaction.Visible = false;
                tableTransaction.Visible = false;
                rdTransaction.Visible = false;
                trStaff.Visible = false;
                tbStaffAudit.Visible = false;
                rdStaffAudit.Visible = false;
                tblStaffAudit.Visible = false;
                trAssociates.Visible = false;
                tblAssociateAudit.Visible = false;
                tbAssociateAudit.Visible = false;
                rdAssociateAudit.Visible = false;
                tblSchemePlan.Visible = false;
                trSystematicId.Visible = false;
                tblSystematicId.Visible = false;
                tbSystematicId.Visible = false;
                trNcdIssueSetup.Visible = false;
                tblNcdIssueId.Visible = false;
                tbNcdIssueId.Visible = false;
                tblNcdCategory.Visible = false;
                tbNcdCategoryAudit.Visible = false;
                tblNcdIssueSeries.Visible = false;
                tbNcdIssueSeries.Visible = false;
                tblNcdSubCategory.Visible = false;
                tbNcdSubCategory.Visible = false;
                trProduct.Visible = false;
                lb1IssuerCode.Visible = false;
                ddlCategory.Visible = false;
                tdtxtNcdIssueSetup.Visible = false;
                ddlProduct.SelectedValue = "0";
            }
            else if (ddlType.SelectedValue == "StaffDetails")
            {
                trStaff.Visible = true;
                tdTodate.Visible = true;
                tdFromDate.Visible = true;
                tdTodate1.Visible = true;
                tdFromDate1.Visible = true;
                tbStaffAudit.Visible = false;
                trCustomer.Visible = false;
                //tdCustomer1.Visible = false;
                tdCustomerAuditList.Visible = false;
                btnSubmit.Visible = true;
                tblProfileHeading.Visible = false;
                rdCustomerProfile.Visible = false;
                tblCustomerBankHeading.Visible = false;
                tblCustomerBank.Visible = false;
                rdCustomerBank.Visible = false;
                tblCustomerDematHeading.Visible = false;
                rdCustomerDemat.Visible = false;
                tblCustomerDematAssociatesHeading.Visible = false;
                tblCustomerDematAssociates.Visible = false;
                rdCustomerDematAssociates.Visible = false;
                tableCustomerTransaction.Visible = false;
                tableTransaction.Visible = false;
                rdTransaction.Visible = false;
                trSchemePlan.Visible = false;
                trAssociates.Visible = false;
                tblAssociateAudit.Visible = false;
                tbAssociateAudit.Visible = false;
                rdAssociateAudit.Visible = false;
                tblSchemePlan.Visible = false;
                taSchemeAudit.Visible = false;
                trSystematicId.Visible = false;
                tblSystematicId.Visible = false;
                tbSystematicId.Visible = false;
                trNcdIssueSetup.Visible = false;
                tblNcdIssueId.Visible = false;
                tbNcdIssueId.Visible = false;
                tblNcdCategory.Visible = false;
                tbNcdCategoryAudit.Visible = false;
                tblNcdIssueSeries.Visible = false;
                tbNcdIssueSeries.Visible = false;
                tblNcdSubCategory.Visible = false;
                tbNcdSubCategory.Visible = false;
                trProduct.Visible = false;
                lb1IssuerCode.Visible = false;
                ddlCategory.Visible = false;
                tdtxtNcdIssueSetup.Visible = false;
            }
            else if (ddlType.SelectedValue == "AssociateDetails")
            {
                trAssociates.Visible = true;
                tdTodate.Visible = true;
                tdFromDate.Visible = true;
                tdTodate1.Visible = true;
                tdFromDate1.Visible = true;
                tbAssociateAudit.Visible = true;
                tbStaffAudit.Visible = false;
                trStaff.Visible = false;
                trCustomer.Visible = false;
                //tdCustomer1.Visible = false;
                tdCustomerAuditList.Visible = false;
                btnSubmit.Visible = true;
                tblProfileHeading.Visible = false;
                rdCustomerProfile.Visible = false;
                tblCustomerBankHeading.Visible = false;
                tblCustomerBank.Visible = false;
                rdCustomerBank.Visible = false;
                tblCustomerDematHeading.Visible = false;
                rdCustomerDemat.Visible = false;
                tblCustomerDematAssociatesHeading.Visible = false;
                tblCustomerDematAssociates.Visible = false;
                rdCustomerDematAssociates.Visible = false;
                tableCustomerTransaction.Visible = false;
                tableTransaction.Visible = false;
                rdTransaction.Visible = false;
                trSchemePlan.Visible = false;
                tblStaffAudit.Visible = false;
                tblAssociateAudit.Visible = false;
                tbAssociateAudit.Visible = false;
                trSystematicId.Visible = false;
                tblSystematicId.Visible = false;
                tbSystematicId.Visible = false;
                trNcdIssueSetup.Visible = false;
                tblNcdIssueId.Visible = false;
                tbNcdIssueId.Visible = false;
                tblNcdCategory.Visible = false;
                tbNcdCategoryAudit.Visible = false;
                tblNcdIssueSeries.Visible = false;
                tbNcdIssueSeries.Visible = false;
                tblNcdSubCategory.Visible = false;
                tbNcdSubCategory.Visible = false;
                tblSchemePlan.Visible = false;
                taSchemeAudit.Visible = false;
                trProduct.Visible = false;
                lb1IssuerCode.Visible = false;
                ddlCategory.Visible = false;
                tdtxtNcdIssueSetup.Visible = false;

            }
            else if (ddlType.SelectedValue == "NCDIssueSetup")
            {
                
                trNcdIssueSetup.Visible = false;
                tdTodate.Visible = false;
                tdFromDate.Visible = false;
                tdTodate1.Visible = false;
                tdFromDate1.Visible = false;
                tblNcdIssueId.Visible = false;
                tbNcdIssueId.Visible = false;
                tblNcdCategory.Visible = false;
                tbNcdCategoryAudit.Visible = false;
                tblNcdIssueSeries.Visible = false;
                tbNcdIssueSeries.Visible = false;
                tblNcdSubCategory.Visible = false;
                tbNcdSubCategory.Visible = false;
                trSystematicId.Visible = false;
                tblSystematicId.Visible = false;
                tbSystematicId.Visible = false;
                trAssociates.Visible = false;
                tbAssociateAudit.Visible = false;
                tbStaffAudit.Visible = false;
                trStaff.Visible = false;
                trCustomer.Visible = false;
                //tdCustomer1.Visible = false;
                tdCustomerAuditList.Visible = false;
                btnSubmit.Visible = true;
                tblProfileHeading.Visible = false;
                rdCustomerProfile.Visible = false;
                tblCustomerBankHeading.Visible = false;
                tblCustomerBank.Visible = false;
                rdCustomerBank.Visible = false;
                tblCustomerDematHeading.Visible = false;
                rdCustomerDemat.Visible = false;
                tblCustomerDematAssociatesHeading.Visible = false;
                tblCustomerDematAssociates.Visible = false;
                rdCustomerDematAssociates.Visible = false;
                tableCustomerTransaction.Visible = false;
                tableTransaction.Visible = false;
                rdTransaction.Visible = false;
                trSchemePlan.Visible = false;
                tblStaffAudit.Visible = false;
                tblAssociateAudit.Visible = false;
                tbAssociateAudit.Visible = false;
                tblSchemePlan.Visible = false;
                taSchemeAudit.Visible = false;
                trProduct.Visible = true;
                lb1IssuerCode.Visible = false;
                ddlCategory.Visible = false;
                btnSubmit.Visible = true;
                ddlProduct.SelectedValue = "0";
                trNcdIssueSetup.Visible = false;
                tdtxtNcdIssueSetup.Visible = false;
                ddlProduct.SelectedValue = "0";
                
             
            }
            else
            {
                trSystematicId.Visible = false;
                trAssociates.Visible = true;
                tdTodate.Visible = true;
                tdFromDate.Visible = true;
                tdTodate1.Visible = true;
                tdFromDate1.Visible = true;
                trAssociates.Visible = false;
                tbAssociateAudit.Visible = false;
                tbStaffAudit.Visible = false;
                trStaff.Visible = false;
                trCustomer.Visible = false;
                //tdCustomer1.Visible = false;
                tdCustomerAuditList.Visible = false;
                btnSubmit.Visible = true;
                tblProfileHeading.Visible = false;
                rdCustomerProfile.Visible = false;
                tblCustomerBankHeading.Visible = false;
                tblCustomerBank.Visible = false;
                rdCustomerBank.Visible = false;
                tblCustomerDematHeading.Visible = false;
                rdCustomerDemat.Visible = false;
                tblCustomerDematAssociatesHeading.Visible = false;
                tblCustomerDematAssociates.Visible = false;
                rdCustomerDematAssociates.Visible = false;
                tableCustomerTransaction.Visible = false;
                tableTransaction.Visible = false;
                rdTransaction.Visible = false;
                trSchemePlan.Visible = false;
                tblStaffAudit.Visible = false;
                tblAssociateAudit.Visible = false;
                tbAssociateAudit.Visible = false;
                trNcdIssueSetup.Visible = false;
                tblNcdIssueId.Visible = false;
                tbNcdIssueId.Visible = false;
                tblNcdCategory.Visible = false;
                tbNcdCategoryAudit.Visible = false;
                tblNcdIssueSeries.Visible = false;
                tbNcdIssueSeries.Visible = false;
                tblNcdSubCategory.Visible = false;
                tbNcdSubCategory.Visible = false;
                tblSchemePlan.Visible = false;
                taSchemeAudit.Visible = false;
                trProduct.Visible = false;
                lb1IssuerCode.Visible = false;
                ddlCategory.Visible = false;
            }
        }
        protected void clearControls1()
        {
            txtClientCode.Text = "";
            txtCustomerName.Text = "";
            txtPansearch.Text = "";
            tblProfileHeading.Visible = false;
            rdCustomerProfile.Visible = false;
            rdCustomerProfile.DataSource = null;
            rdCustomerProfile.CurrentPageIndex = 0;
            tblCustomerBankHeading.Visible = false;
            tblCustomerBank.Visible = false;
            rdCustomerBank.Visible = false;
            rdCustomerBank.DataSource = null;
            rdCustomerBank.CurrentPageIndex = 0;
            tblCustomerDematHeading.Visible = false;
            rdCustomerDemat.Visible = false;
            rdCustomerDemat.DataSource = null;
            rdCustomerDemat.CurrentPageIndex = 0;
            tblCustomerDematAssociatesHeading.Visible = false;
            tblCustomerDematAssociates.Visible = false;
            rdCustomerDematAssociates.Visible = false;
            rdCustomerDematAssociates.DataSource = null;
            rdCustomerDematAssociates.CurrentPageIndex = 0;
            tableCustomerTransaction.Visible = false;
            tableTransaction.Visible = false;
            rdTransaction.Visible = false;
            rdTransaction.DataSource = null;
            rdTransaction.CurrentPageIndex = 0;
        }

        protected void ddlCOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearControls1();
            if (ddlCOption.SelectedValue == "Name")
            {
                tdtxtCustomerName.Visible = true;
                tdtxtClientCode.Visible = false;
                tdtxtPansearch.Visible = false;
            }
            else if (ddlCOption.SelectedValue == "Panno")
            {
                tdtxtPansearch.Visible = true;
                tdtxtCustomerName.Visible = false;
                tdtxtClientCode.Visible = false;

            }
            else if (ddlCOption.SelectedValue == "Clientcode")
            {
                tdtxtClientCode.Visible = true;
                tdtxtPansearch.Visible = false;
                tdtxtCustomerName.Visible = false;
            }
            else
            {
                tdtxtClientCode.Visible = false;
                tdtxtPansearch.Visible = false;
                tdtxtCustomerName.Visible = false;

            }
        }
        protected void clearControls2()
        {
            txtAssociateName.Text = "";
            txtSubbrokerCode.Text = "";
            tblStaffAudit.Visible = false;
            rdStaffAudit.Visible = false;
            rdStaffAudit.DataSource = null;
            rdStaffAudit.CurrentPageIndex = 0;
        }
        protected void ddlAssociateOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearControls2();
            if (ddlAssociateOption.SelectedValue == "Name")
            {
                tdtxtAssociateName.Visible = true;
                tdtxtSubBrokerCode.Visible = false;
            }

            else if (ddlAssociateOption.SelectedValue == "SubBrokerCode")
            {
                tdtxtSubBrokerCode.Visible = true;
                tdtxtAssociateName.Visible = false;
            }
            else
            {
                tdtxtSubBrokerCode.Visible = false;
                tdtxtAssociateName.Visible = false;

            }
        }
        protected void ddlNcdOption_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlNcdOption.SelectedValue == "Name")
            {
                tdtxtNcdIssueSetup.Visible = true;

            }
            else
            {
                if (ddlNcdOption.SelectedValue == "Select")
                {
                    tdtxtNcdIssueSetup.Visible = false;
                }
            }
        }
        protected void btnExportFilteredData_OnClick1(object sender, EventArgs e)
        {
            rdCustomerProfile.ExportSettings.OpenInNewWindow = true;
            rdCustomerProfile.ExportSettings.IgnorePaging = true;
            rdCustomerProfile.ExportSettings.HideStructureColumns = true;
            rdCustomerProfile.ExportSettings.ExportOnlyData = true;
            rdCustomerProfile.ExportSettings.FileName = "Profile Audit Details";
            rdCustomerProfile.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rdCustomerProfile.MasterTableView.ExportToExcel();
        }
        protected void btnExportFilteredData_OnClick2(object sender, EventArgs e)
        {
            rdCustomerBank.ExportSettings.OpenInNewWindow = true;
            rdCustomerBank.ExportSettings.IgnorePaging = true;
            rdCustomerBank.ExportSettings.HideStructureColumns = true;
            rdCustomerBank.ExportSettings.ExportOnlyData = true;
            rdCustomerBank.ExportSettings.FileName = "Bank Audit Details";
            rdCustomerBank.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rdCustomerBank.MasterTableView.ExportToExcel();
        }
        protected void btnExportFilteredData_OnClick3(object sender, EventArgs e)
        {
            rdCustomerDemat.ExportSettings.OpenInNewWindow = true;
            rdCustomerDemat.ExportSettings.IgnorePaging = true;
            rdCustomerDemat.ExportSettings.HideStructureColumns = true;
            rdCustomerDemat.ExportSettings.ExportOnlyData = true;
            rdCustomerDemat.ExportSettings.FileName = "Demat Audit Details";
            rdCustomerDemat.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rdCustomerDemat.MasterTableView.ExportToExcel();
        }
        protected void btnExportFilteredData_OnClick4(object sender, EventArgs e)
        {
            rdCustomerDematAssociates.ExportSettings.OpenInNewWindow = true;
            rdCustomerDematAssociates.ExportSettings.IgnorePaging = true;
            rdCustomerDematAssociates.ExportSettings.HideStructureColumns = true;
            rdCustomerDematAssociates.ExportSettings.ExportOnlyData = true;
            rdCustomerDematAssociates.ExportSettings.FileName = "Demat Association Audit Details";
            rdCustomerDematAssociates.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rdCustomerDematAssociates.MasterTableView.ExportToExcel();
        }
        protected void btnExportFilteredData_OnClick5(object sender, EventArgs e)
        {
            rdTransaction.ExportSettings.OpenInNewWindow = true;
            rdTransaction.ExportSettings.IgnorePaging = true;
            rdTransaction.ExportSettings.HideStructureColumns = true;
            rdTransaction.ExportSettings.ExportOnlyData = true;
            rdTransaction.ExportSettings.FileName = "Customer Transaction Audit";
            rdTransaction.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rdTransaction.MasterTableView.ExportToExcel();
        }
        protected void btnExportFilteredData_OnClick6(object sender, EventArgs e)
        {
            rdSchemeAudit.ExportSettings.OpenInNewWindow = true;
            rdSchemeAudit.ExportSettings.IgnorePaging = true;
            rdSchemeAudit.ExportSettings.HideStructureColumns = true;
            rdSchemeAudit.ExportSettings.ExportOnlyData = true;
            rdSchemeAudit.ExportSettings.FileName = "SchemePlan Audit";
            rdSchemeAudit.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rdSchemeAudit.MasterTableView.ExportToExcel();
        }
        protected void btnExportFilteredData_OnClick7(object sender, EventArgs e)
        {
            rdStaffAudit.ExportSettings.OpenInNewWindow = true;
            rdStaffAudit.ExportSettings.IgnorePaging = true;
            rdStaffAudit.ExportSettings.HideStructureColumns = true;
            rdStaffAudit.ExportSettings.ExportOnlyData = true;
            rdStaffAudit.ExportSettings.FileName = "Staff Details Audit";
            rdStaffAudit.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rdStaffAudit.MasterTableView.ExportToExcel();
        }
        protected void btnExportFilteredData_OnClick8(object sender, EventArgs e)
        {
            rdAssociateAudit.ExportSettings.OpenInNewWindow = true;
            rdAssociateAudit.ExportSettings.IgnorePaging = true;
            rdAssociateAudit.ExportSettings.HideStructureColumns = true;
            rdAssociateAudit.ExportSettings.ExportOnlyData = true;
            rdAssociateAudit.ExportSettings.FileName = "Associate Details Audit";
            rdAssociateAudit.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rdAssociateAudit.MasterTableView.ExportToExcel();
        }
        protected void btnExportFilteredData_OnClick9(object sender, EventArgs e)
        {
            rdSystematicAudit.ExportSettings.OpenInNewWindow = true;
            rdSystematicAudit.ExportSettings.IgnorePaging = true;
            rdSystematicAudit.ExportSettings.HideStructureColumns = true;
            rdSystematicAudit.ExportSettings.ExportOnlyData = true;
            rdSystematicAudit.ExportSettings.FileName = "Systematic Audit Details";
            rdSystematicAudit.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rdSystematicAudit.MasterTableView.ExportToExcel();
        }
        protected void btnExportFilteredData_OnClick10(object sender, EventArgs e)
        {
            rdNcdIssueAudit.ExportSettings.OpenInNewWindow = true;
            rdNcdIssueAudit.ExportSettings.IgnorePaging = true;
            rdNcdIssueAudit.ExportSettings.HideStructureColumns = true;
            rdNcdIssueAudit.ExportSettings.ExportOnlyData = true;
            rdNcdIssueAudit.ExportSettings.FileName = "Issue SetUp Audit Details";
            rdNcdIssueAudit.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rdNcdIssueAudit.MasterTableView.ExportToExcel();
        }
        protected void btnExportFilteredData_OnClick11(object sender, EventArgs e)
        {
            rdNcdCategoryAudit.ExportSettings.OpenInNewWindow = true;
            rdNcdCategoryAudit.ExportSettings.IgnorePaging = true;
            rdNcdCategoryAudit.ExportSettings.HideStructureColumns = true;
            rdNcdCategoryAudit.ExportSettings.ExportOnlyData = true;
            rdNcdCategoryAudit.ExportSettings.FileName = "Issue Category Audit Details";
            rdNcdCategoryAudit.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rdNcdCategoryAudit.MasterTableView.ExportToExcel();
        }
        protected void btnExportFilteredData_OnClick12(object sender, EventArgs e)
        {
            rdNcdSubCategoryAudit.ExportSettings.OpenInNewWindow = true;
            rdNcdSubCategoryAudit.ExportSettings.IgnorePaging = true;
            rdNcdSubCategoryAudit.ExportSettings.HideStructureColumns = true;
            rdNcdSubCategoryAudit.ExportSettings.ExportOnlyData = true;
            rdNcdSubCategoryAudit.ExportSettings.FileName = "Issue SubCategory Audit Details";
            rdNcdSubCategoryAudit.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rdNcdSubCategoryAudit.MasterTableView.ExportToExcel();
        }
        protected void btnExportFilteredData_OnClick14(object sender, EventArgs e)
        {
            rdNcdIssueSeries.ExportSettings.OpenInNewWindow = true;
            rdNcdIssueSeries.ExportSettings.IgnorePaging = true;
            rdNcdIssueSeries.ExportSettings.HideStructureColumns = true;
            rdNcdIssueSeries.ExportSettings.ExportOnlyData = true;
            rdNcdIssueSeries.ExportSettings.FileName = "Issue Series Audit Details";
            rdNcdIssueSeries.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rdNcdIssueSeries.MasterTableView.ExportToExcel();
        }
        protected void ddlProduct_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProduct.SelectedValue == "Bonds")
            {
                lb1IssuerCode.Visible = true;
                ddlCategory.Visible = true;
                tdTodate.Visible = false;
                tdFromDate.Visible = false;
                tdTodate1.Visible = false;
                tdFromDate1.Visible = false;
                btnSubmit.Visible = true;
                tblNcdIssueId.Visible = false;
                tbNcdIssueId.Visible = false;
                tblNcdCategory.Visible = false;
                tbNcdCategoryAudit.Visible = false;
                tblNcdIssueSeries.Visible = false;
                tbNcdIssueSeries.Visible = false;
                trNcdIssueSetup.Visible = false;
                txtNcdIssueSetup.Text = "";
                rdpFromModificationDate.SelectedDate = DateTime.Now;
                rdpToDate.SelectedDate = DateTime.Now;
                ddlNcdIssueSetup.SelectedValue = "0";
                ddlNcdOption.SelectedValue = "0";
                
              
            }
                else
                {
                    if (ddlProduct.SelectedValue == "IP")
                    lb1IssuerCode.Visible = false;
                    ddlCategory.Visible = false;
                    tdTodate.Visible = true;
                    tdFromDate.Visible = true;
                    tdTodate1.Visible = true;
                    tdFromDate1.Visible = true;
                    btnSubmit.Visible = true;
                    tblNcdIssueId.Visible = false;
                    tbNcdIssueId.Visible = false;
                    tblNcdCategory.Visible = false;
                    tbNcdCategoryAudit.Visible = false;
                    tblNcdIssueSeries.Visible = false;
                    tbNcdIssueSeries.Visible = false;
                    trNcdIssueSetup.Visible = true;
                    txtNcdIssueSetup.Text = "";
                    ddlNcdOption.SelectedValue = "0";
                    ddlNcdIssueSetup.SelectedValue = "0";
                  
                }

                BindNcdCategory();
            }
        
        private void BindNcdCategory()
        {
            DataTable dtCategory = new DataTable();
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

        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue == "FICDCD" || ddlCategory.SelectedValue=="FICGCG" || ddlCategory.SelectedValue=="FINPNP" || ddlCategory.SelectedValue=="FISDSD")
            {
                trNcdIssueSetup.Visible = true;
                tdTodate.Visible = true;
                tdFromDate.Visible = true;
                tdTodate1.Visible = true;
                tdFromDate1.Visible = true;
                
            }
            else
            {
                trNcdIssueSetup.Visible = false;
                tdTodate.Visible = false;
                tdFromDate.Visible = false;
                tdTodate1.Visible = false;
                tdFromDate1.Visible = false;
            }
        }



    }
}