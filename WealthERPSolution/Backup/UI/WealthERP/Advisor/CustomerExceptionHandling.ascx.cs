using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoCustomerProfiling;
using BoProductMaster;
using WealthERP.Base;
using VoUser;
using BoCommon;
using BoUploads;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Numeric;
using Telerik.Web.UI;

namespace WealthERP.Advisor
{
    public partial class CustomerExceptionHandling : System.Web.UI.UserControl
    {
        SystematicSetupVo systematicSetupVo;
        SystematicSetupBo systematicSetupBo = new SystematicSetupBo();
        AdvisorVo advisorVo = new AdvisorVo();
        DataSet dsSystematicMIS = new DataSet();
        DataTable dtSystematicTransactionType;
        DataTable dtAMC;
        DataTable dtCategory;
        DataTable dtScheme;
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        string strAmcCode = null;
        String userType;
        int advisorId = 0;
        int customerId = 0;
        double sumTotal;
        double totalAmount = 0;
        int rmId = 0;
        int isIndividualOrGroup = 0;
        string customerType = string.Empty;
        int bmID = 0;
        int all;
        bool IsfixedMapped;
        double monthlyTotalSipAmount = 0;
        RMVo rmVo = new RMVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        DataSet dsBindgvExceptionReport = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();
            // gvSystematicMIS.Visible = true;
            //ErrorMessage.Visible = false;
            if (!IsPostBack)
            {
                rquiredFieldValidatorIndivudialCustomer.Visible = false;
                btnExportFilteredData.Visible = false;
                gvExceptionReport.Visible = false;
                BindExceptionList();
                BindExceptionType();
                ddlSelectCutomer.Visible = false;
                lblSelectTypeOfCustomer.Visible = false;
                lblselectCustomer.Visible = false;
                txtIndividualCustomer.Visible = false;
                if (userType == "advisor")
                {
                    BindBranchDropDown();
                    BindRMDropDown();
                }
                else if (userType == "rm")
                {
                    trBranchRM.Visible = false;

                }
                if (userType == "bm")
                {
                    BindBranchForBMDropDown();
                    BindRMforBranchDropdown(0, bmID);
                }
            }
        }
        protected void ddlDisplay_SelectedIndexChanged(object sender, EventArgs e)
        { }
        protected void ddlExpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsfixedMapped = customerBo.GetFixedMapped(ddlExpList.SelectedValue);
            if (IsfixedMapped == true)
            {
                ddlDisplay.Enabled = true;
                ddlDisplay_SelectedIndexChanged(sender, e);
            }
            else
            {
                ddlDisplay.SelectedIndex = 0;
                ddlDisplay.Enabled = false;
                ddlDisplay_SelectedIndexChanged(sender, e);
            }

        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0);
            }
        }
        private void BindExceptionList()
        {

            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
            try
            {
                CustomerBo customerBo = new CustomerBo();
                DataSet ds = customerBo.GetExceptionList();
                if (ds != null)
                {
                    ddlExpList.DataSource = ds;
                    ddlExpList.DataValueField = ds.Tables[0].Columns["WDE_Code"].ToString();
                    ddlExpList.DataTextField = ds.Tables[0].Columns["WDE_Name"].ToString();
                    ddlExpList.DataBind();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindExceptionType()
        {
            bool isISA;
            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
            try
            {
                isISA = advisorVo.IsISASubscribed;
                CustomerBo customerBo = new CustomerBo();
                DataSet ds = customerBo.GetExceptionType(isISA);
                if (ds != null)
                {
                    ddlExpType.DataSource = ds;
                    ddlExpType.DataValueField = ds.Tables[0].Columns["WDEM_Code"].ToString();
                    ddlExpType.DataTextField = ds.Tables[0].Columns["WDEM_Name"].ToString();
                    ddlExpType.DataBind();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            Session["ButtonGo"] = "buttonClicked";
            ViewState["GroupHeadCustomers"] = null;
            ViewState["IndividualCustomers"] = null;
            ViewState["CustomerId"] = null;

            CallAllGridBindingFunctions();
        }
        protected void CallAllGridBindingFunctions()
        {


            SetParameter();
            Bindgrid();


        }
        protected void gvExceptionReport_ItemCommand(object source, GridCommandEventArgs e)
        {
            string strFolioNumber = string.Empty;
            string strProfileData = string.Empty;
            string ProData = string.Empty;
            string strFolioData = string.Empty;
            String Exptype = string.Empty;
            DateTime createdDate = new DateTime();
            DateTime editedDate = new DateTime();
            DateTime deletedDate = new DateTime();

            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                //strExternalCodeToBeEdited = Session["extCodeTobeEdited"].ToString();
                CustomerBo customerBo = new CustomerBo();
                bool isUpdated = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txtProfileData = (TextBox)e.Item.FindControl("txtProfileDataForEditForm");
                TextBox txtFolioData = (TextBox)e.Item.FindControl("txtFolioDataForEditForm");
                TextBox txtFolioNumber = (TextBox)e.Item.FindControl("txtFolioNumberForEditForm");
                TextBox txtCustomerId = (TextBox)e.Item.FindControl("txtCustomerIdForEditForm");
                //DropDownList txtExtType = (DropDownList)e.Item.FindControl("ddlExternalType");
                //TextBox txtSchemePlancode = (TextBox)e.Item.FindControl("txtSchemePlanCodeForEditForm");
                //strProfileData = txtProfileData.Text;
                strFolioData = txtFolioData.Text;
                strFolioNumber = txtFolioNumber.Text;
                ProData = txtProfileData.Text;
                customerId = int.Parse(txtCustomerId.Text);
               
                isUpdated = customerBo.EditData(ProData, strFolioData, strFolioNumber, customerId, hdnExplist.Value);

            }

            CallAllGridBindingFunctions();
        }
        protected void gvExceptionReport_ItemDataBound(object sender, GridItemEventArgs e)
        {
            customerBo = new CustomerBo();
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {

                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                TextBox txtFolioDataForEditForm = (TextBox)item.FindControl("txtFolioDataForEditForm");
                TextBox txtFolioNumberForEditForm = (TextBox)item.FindControl("txtFolioNumberForEditForm");
                txtFolioDataForEditForm.Text = txtFolioDataForEditForm.Text;
                txtFolioNumberForEditForm.Text = txtFolioNumberForEditForm.Text;

            }

            string panNum = string.Empty;
            string ISAChck = string.Empty;

            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                LinkButton buttonEdit = dataItem["editColumn"].Controls[0] as LinkButton;
                string ProfileDate = dataItem["ProfileFolio"].Text;
                panNum = dataItem["Exception"].Text;
                ISAChck = dataItem["CustomerName"].Text;
                if (ISAChck == "ISAF")
                {
                    buttonEdit.Visible = false;

                }
                ProfileDate = ProfileDate.ToUpper();
                if (ProfileDate == "PROFILE")
                {
                    buttonEdit.Visible = false;

                }
                int customerId = int.Parse(dataItem["CustomerId"].Text);

                
            }
            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem headeritem = e.Item as GridHeaderItem;
                string value = ddlExpList.SelectedItem.Text;
                headeritem["Exception"].Text = value;
                string NewValue = ddlExpType.SelectedItem.Value;
                if (NewValue == "ISAF")
                {
                    headeritem["ProfileFolio"].Text = "Folio Mode Of Holding";
                    headeritem["Exception"].Text = "ISA Mode Of Holding";
                    headeritem["Exceptionlist"].Text = "ISA No.";
                }
            }
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                //int customerId = int.Parse(gvExceptionReport.MasterTableView.DataKeyValues[e.Item.ItemIndex]["customerId"].ToString());

                //GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                //TextBox txtFolioDataForEditForm = (TextBox)editedItem.FindControl("txtFolioDataForEditForm");
                //TextBox txtFolioNumberForEditForm = (TextBox)editedItem.FindControl("txtFolioNumberForEditForm");
                //txtFolioDataForEditForm.Text = txtFolioDataForEditForm.Text;
                //txtFolioNumberForEditForm.Text = txtFolioNumberForEditForm.Text;


            }
        }
        protected void gvExceptionReport_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            gvExceptionReport.Visible = true;
            DataSet ds = new DataSet();

            btnExportFilteredData.Visible = true;
            ds = (DataSet)Cache["gvExceptionReport + '" + advisorVo.advisorId + "'"];
            gvExceptionReport.DataSource = ds;
        }
        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            DataSet dsgvExceptionReport = new DataSet();
            dsgvExceptionReport = (DataSet)Cache["gvExceptionReport"];
            gvExceptionReport.DataSource = dsgvExceptionReport;

            gvExceptionReport.ExportSettings.OpenInNewWindow = true;
            gvExceptionReport.ExportSettings.IgnorePaging = true;
            gvExceptionReport.ExportSettings.HideStructureColumns = true;
            gvExceptionReport.ExportSettings.ExportOnlyData = true;
            gvExceptionReport.ExportSettings.FileName = "Exception Details";
            gvExceptionReport.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvExceptionReport.MasterTableView.ExportToExcel();
        }
        private void Bindgrid()
        {
            if (ddlDisplay.SelectedItem.Text == "MisMatch Only")
            {
                hdnMismatch.Value = "1";
                dsBindgvExceptionReport = customerBo.GetExceptionReportMismatchDetails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnCustomerId.Value), int.Parse(hdnbranchheadId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnAll.Value), isIndividualOrGroup, hdnExplist.Value, hdnExptype.Value, int.Parse(hdnMismatch.Value));
            }
            else
            {
                hdnMismatch.Value = "0";
                dsBindgvExceptionReport = customerBo.GetExceptionReportDetails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnCustomerId.Value), int.Parse(hdnbranchheadId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnAll.Value), isIndividualOrGroup, hdnExplist.Value, hdnExptype.Value, int.Parse(hdnMismatch.Value));
            }

            if (dsBindgvExceptionReport.Tables.Count != 0)
            {
                gvExceptionReport.DataSource = dsBindgvExceptionReport;
                gvExceptionReport.DataBind();
                gvExceptionReport.Visible = true;
                btnExportFilteredData.Visible = true;
            }
            if (Cache["gvExceptionReport + '" + advisorVo.advisorId + "'"] == null)
            {
                Cache.Insert("gvExceptionReport + '" + advisorVo.advisorId + "'", dsBindgvExceptionReport);
            }
            else
            {
                Cache.Remove("gvExceptionReport + '" + advisorVo.advisorId + "'");
                Cache.Insert("gvExceptionReport + '" + advisorVo.advisorId + "'", dsBindgvExceptionReport);
            }
        }
        private void SetParameter()
        {
            if ((ddlSelectCustomer.SelectedItem.Value == "All Customer") && (userType == "advisor"))
            {
                hdnCustomerId.Value = "0";
                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnAll.Value = "0";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRM.SelectedValue; ;
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "3";
                }

            }
            else if (ddlSelectCustomer.SelectedItem.Value == "All Customer" && userType == "rm")
            {
                hdnrmId.Value = rmVo.RMId.ToString();
                hdnAll.Value = "0";

            }
            else if (ddlSelectCustomer.SelectedItem.Value == "All Customer" && userType == "bm")
            {
                hdnCustomerId.Value = "0";
                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {

                    hdnbranchheadId.Value = bmID.ToString();
                    hdnAll.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnbranchheadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnbranchheadId.Value = bmID.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRM.SelectedValue; ;
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnbranchheadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "3";
                }
            }


            if (ddlSelectCustomer.SelectedItem.Value == "Pick Customer" && userType == "advisor")
            {

                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnAll.Value = "4";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = "0";
                    hdnAll.Value = "5";

                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {

                    hdnbranchId.Value = "0";
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "6";

                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {

                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "7";
                }
            }
            else if (ddlSelectCustomer.SelectedItem.Value == "Pick Customer" && userType == "rm")
            {
                hdnAll.Value = "1";
            }


            else if (ddlSelectCustomer.SelectedItem.Value == "Pick Customer" && userType == "bm")
            {

                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnAll.Value = "4";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = "0";
                    hdnAll.Value = "5";

                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {

                    hdnbranchId.Value = "0";
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "6";

                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {

                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "7";
                }
            }
            if (ddlExpType.SelectedValue == "CPF")
            {
                if (ddlExpList.SelectedValue == "Pan")
                {
                    hdnExplist.Value = ddlExpList.SelectedValue;

                }
                if (ddlExpList.SelectedValue == "Email")
                {
                    hdnExplist.Value = ddlExpList.SelectedValue;
                }
                if (ddlExpList.SelectedValue == "Mob(r)")
                {
                    hdnExplist.Value = ddlExpList.SelectedValue;
                }
                if (ddlExpList.SelectedValue == "Dob")
                {
                    hdnExplist.Value = ddlExpList.SelectedValue;
                }
                if (ddlExpList.SelectedValue == "TS")
                {
                    hdnExplist.Value = ddlExpList.SelectedValue;
                }
                if (ddlExpList.SelectedValue == "Toh")
                {
                    hdnExplist.Value = ddlExpList.SelectedValue;
                }
                if (ddlExpList.SelectedValue == "Mob(o)")
                {
                    hdnExplist.Value = ddlExpList.SelectedValue;
                }
                hdnExptype.Value = ddlExpType.SelectedValue;
            }
            else if (ddlExpType.SelectedValue == "ISAF")
            {
                hdnExptype.Value = ddlExpType.SelectedValue;
            }
            if (hdnCustomerId.Value != "")
            {
                ViewState["CustomerId"] = hdnCustomerId.Value;
            }
            else if (ViewState["CustomerId"] != null)
            {
                hdnCustomerId.Value = ViewState["CustomerId"].ToString();
            }

            if (hdnIndividualOrGroup.Value == "Group Head")
            {
                isIndividualOrGroup = 1;
                ViewState["GroupHeadCustomers"] = isIndividualOrGroup.ToString();
            }
            else if (ViewState["GroupHeadCustomers"] != null)
            {
                isIndividualOrGroup = int.Parse(ViewState["GroupHeadCustomers"].ToString());
            }

            if (hdnIndividualOrGroup.Value == "Individual")
            {
                isIndividualOrGroup = 2;
                ViewState["IndividualCustomers"] = isIndividualOrGroup.ToString();
            }
            else if (ViewState["IndividualCustomers"] != null)
            {
                isIndividualOrGroup = int.Parse(ViewState["IndividualCustomers"].ToString());
            }

            if (hdnbranchheadId.Value == "")
                hdnbranchheadId.Value = "0";

            if (hdnbranchId.Value == "")
                hdnbranchId.Value = "0";

            if (hdnCustomerId.Value == "")
                hdnCustomerId.Value = "0";

            if (hdnadviserId.Value == "")
                hdnadviserId.Value = "0";

            if (hdnrmId.Value == "")
                hdnrmId.Value = "0";

        }
        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        {

            try
            {

                DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
                if (ds != null)
                {
                    ddlRM.DataSource = ds.Tables[0]; ;
                    ddlRM.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                    ddlRM.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserEQMIS.ascx:BindRMforBranchDropdown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindBranchForBMDropDown()
        {
            try
            {
                DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(0, bmID, 0);
                if (ds != null)
                {
                    ddlBranch.DataSource = ds.Tables[1]; ;
                    ddlBranch.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindBranchDropDown()
        {

            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null)
                {
                    ddlBranch.DataSource = ds;
                    ddlBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindRMDropDown()
        {
            try
            {
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
                if (dt.Rows.Count > 0)
                {
                    ddlRM.DataSource = dt;
                    ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
                    ddlRM.DataTextField = dt.Columns["RMName"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "2"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindRMDropDown()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void ddlExpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlExpType.SelectedItem.Value == "ISAF")
            {
                lblExceptionList.Visible = false;
                ddlExpList.Visible = false;
            }
            else
            {
                lblExceptionList.Visible = true;
                ddlExpList.Visible = true;
            }
        }

        protected void ddlSelectCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelectCustomer.SelectedItem.Value == "All Customer")
            {
                ddlSelectCutomer.Visible = false;
                lblSelectTypeOfCustomer.Visible = false;
                txtIndividualCustomer.Visible = false;
                lblselectCustomer.Visible = false;
                rquiredFieldValidatorIndivudialCustomer.Visible = false;

                ViewState["GroupHeadCustomers"] = null;
                ViewState["IndividualCustomers"] = null;
                //gvCalenderDetailView.Visible = false;
                //gvSystematicMIS.Visible = false;
                //tblMessage.Visible = true;
                //ErrorMessage.Visible = true;
                //ErrorMessage.InnerText = "No Records Found...!";
            }
            if (ddlSelectCustomer.SelectedItem.Value == "Pick Customer")
            {
                ddlSelectCutomer.Visible = true;
                lblSelectTypeOfCustomer.Visible = true;
                txtIndividualCustomer.Visible = true;
                txtIndividualCustomer.Text = string.Empty;
                lblselectCustomer.Visible = true;
                rquiredFieldValidatorIndivudialCustomer.Visible = true;
                ViewState["GroupHeadCustomers"] = null;
                ViewState["IndividualCustomers"] = null;
                ddlSelectCutomer.SelectedIndex = 0;
                //gvCalenderDetailView.Visible = false;
                //gvSystematicMIS.Visible = false;
                //tblMessage.Visible = true;
                //ErrorMessage.Visible = true;
                //ErrorMessage.InnerText = "No Records Found...!";
            }
        }
        protected void ddlSelectCutomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIndividualCustomer.Text = string.Empty;
            hdnIndividualOrGroup.Value = ddlSelectCutomer.SelectedItem.Value;
            if (ddlSelectCutomer.SelectedItem.Value == "Group Head")
            {
                customerType = "GROUP";
                if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllGroupCustomers";
                    }
                    else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMGroupCustomers";
                    }
                }

                else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetBMParentCustomerNames";
                    }
                    if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllGroupCustomers";
                    }
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                    }
                    if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMGroupCustomers";
                    }
                }
            }
            else if (ddlSelectCutomer.SelectedItem.Value == "Individual")
            {
                txtIndividualCustomer.Visible = true;
                customerType = "IND";

                rquiredFieldValidatorIndivudialCustomer.Visible = true;
                if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";

                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {

                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllIndividualCustomers";
                    }
                    else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMIndividualCustomers";
                    }
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllIndividualCustomers";
                    }
                    else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMIndividualCustomers";
                    }
                }
            }
        }
    }
}