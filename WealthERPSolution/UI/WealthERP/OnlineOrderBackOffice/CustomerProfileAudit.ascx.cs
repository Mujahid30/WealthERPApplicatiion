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
                txtSchemeName_AutoCompleteExtender.ServiceMethod = "GetSchemeName";


            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                txtSchemeName_AutoCompleteExtender.ServiceMethod = "GetSchemeName";


            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                txtSchemeName_AutoCompleteExtender.ServiceMethod = "GetSchemeName";

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
            else
            {
                GetSchemePlanAuditDetail();
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
            tblCustomerDemat.Visible = false;
            tblCustomerDematHeading.Visible = false;
            tblCustomerDematAssociates.Visible = false;
            tblCustomerDematAssociatesHeading.Visible = false;
            tableCustomerTransaction.Visible = false;
            tableTransaction.Visible = false;
            DataSet dsGetCustomerProfileAuditData = new DataSet();
            dsGetCustomerProfileAuditData = customerBo.GetCustomerProfileAuditDetails((!string.IsNullOrEmpty(hdnCustomerId.Value)) ? int.Parse(hdnCustomerId.Value) : 0, rdpFromModificationDate.SelectedDate.Value, rdpToDate.SelectedDate.Value, adviserVo.advisorId, ddlAuditType.SelectedValue);
            switch (ddlAuditType.SelectedValue.ToString())
            {
                case "CP": rdCustomerProfile.DataSource = dsGetCustomerProfileAuditData.Tables[0];
                    rdCustomerProfile.DataBind();
                    tblProfileHeading.Visible = true;
                    tblProfileData.Visible = true;
                    rdCustomerProfile.Visible = true;
                    if (Cache["CustomerProfile" + adviserVo.advisorId] != null) Cache.Remove("CustomerProfile" + adviserVo.advisorId);
                    Cache.Insert("CustomerProfile" + adviserVo.advisorId, dsGetCustomerProfileAuditData.Tables[0]);
                    break;
                case "CB": rdCustomerBank.DataSource = dsGetCustomerProfileAuditData.Tables[0];
                    rdCustomerBank.DataBind();
                    tblCustomerBank.Visible = true;
                    tblCustomerBankHeading.Visible = true;
                    rdCustomerBank.Visible = true;
                    if (Cache["CustomerBank" + adviserVo.advisorId] != null) Cache.Remove("CustomerBank" + adviserVo.advisorId);
                    Cache.Insert("CustomerBank" + adviserVo.advisorId, dsGetCustomerProfileAuditData.Tables[0]);
                    break;
                case "CD": rdCustomerDemat.DataSource = dsGetCustomerProfileAuditData.Tables[0];
                    rdCustomerDemat.DataBind();
                    tblCustomerDemat.Visible = true;
                    tblCustomerDematHeading.Visible = true;
                    rdCustomerDemat.Visible = true;
                    if (Cache["CustomerDemat" + adviserVo.advisorId] != null) Cache.Remove("CustomerDemat" + adviserVo.advisorId);
                    Cache.Insert("CustomerDemat" + adviserVo.advisorId, dsGetCustomerProfileAuditData.Tables[0]);
                    break;
                case "CDA": rdCustomerDematAssociates.DataSource = dsGetCustomerProfileAuditData.Tables[0];
                    rdCustomerDematAssociates.DataBind();
                    tblCustomerDematAssociates.Visible = true;
                    tblCustomerDematAssociatesHeading.Visible = true;
                    rdCustomerDematAssociates.Visible = true;
                    if (Cache["CustomerDematAssociate" + adviserVo.advisorId] != null) Cache.Remove("CustomerDematAssociate" + adviserVo.advisorId);
                    Cache.Insert("CustomerDematAssociate" + adviserVo.advisorId, dsGetCustomerProfileAuditData.Tables[0]);
                    break;
                case "CTA": rdTransaction.DataSource = dsGetCustomerProfileAuditData.Tables[0];
                    rdTransaction.DataBind();
                    tableTransaction.Visible = true;
                    tableCustomerTransaction.Visible = true;
                    rdTransaction.Visible = true;
                    if (Cache["CustomerTransaction" + adviserVo.advisorId] != null) Cache.Remove("CustomerTransaction" + adviserVo.advisorId);
                    Cache.Insert("CustomerTransaction" + adviserVo.advisorId, dsGetCustomerProfileAuditData.Tables[0]);
                    break;
                //if (ddlType.SelectedValue == "CustomerProfile")
                //{
                //    r
                //}
            }


        }
        protected void rdCustomerProfile_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtCustomerProfile = (DataTable)Cache["CustomerProfile" + adviserVo.advisorId];

            if (dtCustomerProfile != null) rdCustomerProfile.DataSource = dtCustomerProfile;
        }
        protected void rdCustomerBank_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtCustomerBank = (DataTable)Cache["CustomerBank" + adviserVo.advisorId];

            if (dtCustomerBank != null) rdCustomerBank.DataSource = dtCustomerBank;
        }
        protected void rdCustomerDemat_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtCustomerDemat = (DataTable)Cache["CustomerDemat" + adviserVo.advisorId];

            if (dtCustomerDemat != null) rdCustomerDemat.DataSource = dtCustomerDemat;
        }
        protected void rdCustomerDematAssociates_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtCustomerDematAssociates = (DataTable)Cache["CustomerDematAssociate" + adviserVo.advisorId];

            if (dtCustomerDematAssociates != null) rdCustomerDematAssociates.DataSource = dtCustomerDematAssociates;
        }
        protected void rdTransaction_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtCustomerTransaction = (DataTable)Cache["CustomerTransaction" + adviserVo.advisorId];

            if (dtCustomerTransaction != null) rdTransaction.DataSource = dtCustomerTransaction;
        }
        protected void rdSchemeAudit_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtrdSchemeAudit = (DataTable)Cache["SchemeAudit" + userVo.UserId];
            if (dtrdSchemeAudit != null) rdSchemeAudit.DataSource = dtrdSchemeAudit;
        }
        protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "CustomerProfile")
            {
                tdCustomer.Visible = true;
                tdTodate.Visible = true;
                tdFromDate.Visible = true;
                tdCustomerAuditList.Visible = true;
                tdSchemePlan.Visible = false;
                btnSubmit.Visible = true;
                tblSchemePlan.Visible = false;
                taSchemeAudit.Visible = false;
                rdSchemeAudit.Visible = false;

            }
            else if (ddlType.SelectedValue == "Schemeplan")
            {
                tdSchemePlan.Visible = true;
                tdTodate.Visible = true;
                tdFromDate.Visible = true;
                tdCustomer.Visible = false;
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
    }
}