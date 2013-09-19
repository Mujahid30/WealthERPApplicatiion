using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using WealthERP.Base;
using BoCommon;
using BoCommisionManagement;
using VoCommisionManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using System.Data;

namespace WealthERP.CommisionManagement
{
    public partial class CommissionStructureToSchemeMapping : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        RMVo rmVo;
        bool isRedirect;
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session["rmVo"];
            isRedirect = false;

            if (!IsPostBack) {
                if (Request.QueryString["ID"] != null)
                {
                    isRedirect = true;
                    hdnStructId.Value = Request.QueryString["ID"].Trim();
                    lbtStructureName.Visible = true;
                    SetStructureDetails();
                    CreateMappedSchemeGrid();
                }
                else
                {
                    Cache.Remove(userVo.UserId.ToString() + "MappedSchemes");
                    isRedirect = false;
                    getAllStructures();
                    ddlStructs.Visible = true;
                }
            }
        }

        private void getAllStructures() 
        {
            DataSet dsAllStructs;
            try
            {
                dsAllStructs = commisionReceivableBo.GetAdviserCommissionStructureRules(advisorVo.advisorId);
                DataRow drStructs = dsAllStructs.Tables[0].NewRow();
                drStructs["ACSM_CommissionStructureId"] = 0;
                drStructs["ACSM_CommissionStructureName"] = "-SELECT-";
                dsAllStructs.Tables[0].Rows.InsertAt(drStructs, 0);
                ddlStructs.DataTextField = dsAllStructs.Tables[0].Columns["ACSM_CommissionStructureName"].ToString();
                ddlStructs.DataValueField = dsAllStructs.Tables[0].Columns["ACSM_CommissionStructureId"].ToString();
                ddlStructs.DataSource = dsAllStructs.Tables[0];
                ddlStructs.DataBind();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionStructureToSchemeMapping.ascx.cs:getAllStructures()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlStructs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(ddlStructs.SelectedValue) == 0) { return; }

            hdnStructId.Value = this.ddlStructs.SelectedValue.ToString();
            SetStructureDetails();
            CreateMappedSchemeGrid();
            pnlGrid.Visible = true;
        }

        private string convertSubcatListToCSV(List<RadListBoxItem> itemList)
        {
            string strSubcatsList = "";
            int nCount = itemList.Count, i = 0;
            foreach (RadListBoxItem item in itemList) {
                i++;
                strSubcatsList += item.Value;
                if (i < nCount) { strSubcatsList += ","; }
            }

            return strSubcatsList;
        }

        private void SetStructureDetails() {
            DataSet dsStructDet;
            try
            {
                dsStructDet = commisionReceivableBo.GetStructureDetails(advisorVo.advisorId, int.Parse(hdnStructId.Value));
                foreach (DataRow row in dsStructDet.Tables[0].Rows) {
                    hdnProductId.Value = row["PAG_AssetGroupCode"].ToString();
                    hdnStructValidFrom.Value = row["ACSM_ValidityStartDate"].ToString();
                    hdnStructValidTill.Value = row["ACSM_ValidityEndDate"].ToString();
                    hdnIssuerId.Value = row["PA_AMCCode"].ToString();
                    hdnCategoryId.Value = row["PAIC_AssetInstrumentCategoryCode"].ToString();

                    lbtStructureName.Text = row["ACSM_CommissionStructureName"].ToString();
                    lbtStructureName.ToolTip = row["ACSM_CommissionStructureName"].ToString();
                    txtProductName.Text = row["PAG_AssetGroupName"].ToString();
                    txtProductName.ToolTip = row["PAG_AssetGroupName"].ToString();
                    txtCategory.Text = row["PAIC_AssetInstrumentCategoryName"].ToString();
                    txtCategory.ToolTip = row["PAIC_AssetInstrumentCategoryName"].ToString();
                    txtIssuerName.Text = row["PA_AMCName"].ToString();
                    txtIssuerName.ToolTip = row["PA_AMCName"].ToString();
                    txtValidFrom.Text = DateTime.Parse(hdnStructValidFrom.Value).ToShortDateString();
                    txtValidTo.Text = DateTime.Parse(hdnStructValidTill.Value).ToShortDateString();
                }
                

                //Getting the list of subcategories
                dsStructDet = commisionReceivableBo.GetSubcategories(advisorVo.advisorId, int.Parse(hdnStructId.Value));
                DataTable dtSubcats = dsStructDet.Tables[0];

                foreach (DataRow row in dtSubcats.Rows) 
                {
                    if (row["PAISC_AssetInstrumentSubCategoryName"].ToString().Trim() == "")
                        continue;
                    rlbAssetSubCategory.Items.Add(new RadListBoxItem(row["PAISC_AssetInstrumentSubCategoryName"].ToString().Trim(), row["PAISC_AssetInstrumentSubCategoryCode"].ToString().Trim()));

                }
                hdnSubcategoryIds.Value = convertSubcatListToCSV(rlbAssetSubCategory.Items.ToList());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionStructureToSchemeMapping.ascx.cs:SetStructureDetails()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void CreateMappedSchemeGrid()
        {
            try 
            {
                DataSet dsMappedSchemes = new DataSet();
                dsMappedSchemes = commisionReceivableBo.GetMappedSchemes(int.Parse(hdnStructId.Value));
                gvMappedSchemes.DataSource = dsMappedSchemes.Tables[0];
                gvMappedSchemes.DataBind();
                Cache.Insert(userVo.UserId.ToString() + "MappedSchemes", dsMappedSchemes.Tables[0]);
                pnlGrid.Visible = true;
                pnlAddSchemesButton.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionStructureToSchemeMapping.ascx.cs:void CreateMappedSchemeGrid()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }            
        }

        private void BindMappedSchemesToList()
        {
            lblMappedSchemes.Text = "Mapped Schemes(" + rlbMappedSchemes.Items.Count.ToString() + ")";
        }

        private void BindAvailSchemesToList()
        {
            try
            {
                int sStructId = int.Parse(hdnStructId.Value);
                int sIssuerId = int.Parse(hdnIssuerId.Value);
                string sProduct = hdnProductId.Value;
                string sCategory = hdnCategoryId.Value;
                string sSubcats = hdnSubcategoryIds.Value;

                DateTime validFrom = rdpPeriodStart.SelectedDate.Value;
                DateTime validTill = rdpPeriodEnd.SelectedDate.Value;

                DataSet dsAvailSchemes = commisionReceivableBo.GetAvailSchemes(advisorVo.advisorId, sStructId, sIssuerId, sProduct, sCategory, sSubcats, validFrom, validTill);
                rlbAvailSchemes.DataSource = dsAvailSchemes.Tables[0];
                rlbAvailSchemes.DataValueField = dsAvailSchemes.Tables[0].Columns["PASP_SchemePlanCode"].ToString();
                rlbAvailSchemes.DataTextField = dsAvailSchemes.Tables[0].Columns["PASP_SchemePlanName"].ToString();
                rlbAvailSchemes.DataBind();

                lblAvailableSchemes.Text = "Available Schemes(" + rlbAvailSchemes.Items.Count.ToString() + ")";
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionStructureToSchemeMapping.ascx.cs:BindAvailSchemesToList()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            CreateMappedSchemeGrid();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }

        protected void gvMappedSchemes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dtMappedSchemes = new DataTable();
            if (Cache[userVo.UserId.ToString() + "MappedSchemes"] != null)
            {
                dtMappedSchemes = (DataTable)Cache[userVo.UserId.ToString() + "MappedSchemes"];
                gvMappedSchemes.DataSource = dtMappedSchemes;
            }
        }

        protected void gvMappedSchemes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            gvMappedSchemes.CurrentPageIndex = e.NewPageIndex;
        }

        protected void gvMappedSchemes_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            //gvMappedSchemes.PageSize = e.NewPageSize;
        }

        private void SetFetchSchemesDatePickControls() 
        {
            rdpPeriodStart.MinDate = DateTime.Parse(hdnStructValidFrom.Value.ToString());
            rdpPeriodStart.MaxDate = DateTime.Parse(hdnStructValidTill.Value.ToString()).AddDays(-1);
            rdpPeriodStart.SelectedDate = rdpPeriodStart.MinDate;
            
            rdpPeriodEnd.MinDate = DateTime.Parse(hdnStructValidFrom.Value.ToString()).AddDays(1);
            rdpPeriodEnd.MaxDate = DateTime.Parse(hdnStructValidTill.Value.ToString());
            rdpPeriodEnd.SelectedDate = rdpPeriodEnd.MaxDate;
        }

        protected void btnAddNewSchemes_Click(object sender, EventArgs e)
        {
            SetFetchSchemesDatePickControls();
            pnlAddSchemes.Visible = true;
        }

        protected void ListBoxSource_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
        }

        private void SetMappedSchemesDatePicker() {
            rdpMappedFrom.MinDate = DateTime.Parse(hdnStructValidFrom.Value.ToString());
            rdpMappedFrom.MaxDate = DateTime.Parse(hdnStructValidTill.Value.ToString()).AddDays(-1);
            rdpMappedFrom.SelectedDate = rdpMappedFrom.MinDate;

            rdpMappedTill.MinDate = DateTime.Parse(hdnStructValidFrom.Value.ToString()).AddDays(1);
            rdpMappedTill.MaxDate = DateTime.Parse(hdnStructValidTill.Value.ToString());
            rdpMappedTill.SelectedDate = rdpMappedTill.MaxDate;
        }

        protected void btn_GetAvailableSchemes_Click(object sender, EventArgs e)
        {

            //Perform validations
            this.Page.Validate("availSchemesPeriod");
            if (!this.Page.IsValid) { return; }

            lblMapError.Text = "";
            rlbAvailSchemes.Items.Clear();
            rlbMappedSchemes.Items.Clear();
            BindAvailSchemesToList();
            BindMappedSchemesToList();
            SetMappedSchemesDatePicker();
        }

        protected void rlbAvailSchemes_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
            lblAvailableSchemes.Text = "Available Schemes(" + rlbAvailSchemes.Items.Count.ToString() + ")";
        }

        protected void rlbMappedSchemes_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
            lblMappedSchemes.Text = "Mapped Schemes(" + rlbMappedSchemes.Items.Count.ToString() + ")";
        }

        private void MapSchemesToStructure() 
        {
            if (rlbMappedSchemes.Items.Count < 1)
                return;
            List<int> schemeIds = new List<int>();

            bool mapOk = true;
            int structId = int.Parse(hdnStructId.Value);
            foreach (RadListBoxItem item in rlbMappedSchemes.Items) {
                int schemeId = int.Parse(item.Value);
                if (commisionReceivableBo.checkSchemeAssociationExists(schemeId, structId, rdpMappedFrom.SelectedDate.Value, rdpMappedTill.SelectedDate.Value)) {
                    mapOk = false;
                    break;
                }
            }

            if (!mapOk) {
                showMapError();
                return;
            }
            foreach (RadListBoxItem item in rlbMappedSchemes.Items) {
                commisionReceivableBo.MapSchemesToStructres(structId, int.Parse(item.Value), rdpMappedFrom.SelectedDate.Value, rdpMappedTill.SelectedDate.Value);
            }
        }

        private void showMapError() {
            //lblMapError.Text = "Scheme mapping cannot be performed";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Scheme mapping could not be performed');", true);
        }

        protected void btnMapSchemes_Click(object sender, EventArgs e)
        {
            //Validation
            this.Page.Validate("mappingPeriod");
            if (!this.Page.IsValid) { return; }

            MapSchemesToStructure();
            CreateMappedSchemeGrid();
            rlbAvailSchemes.Items.Clear();
            BindAvailSchemesToList();
            rlbMappedSchemes.Items.Clear();
            BindMappedSchemesToList();
        }

        protected void lbtStructureName_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('ReceivableSetup','StructureId=" + this.hdnStructId.Value + "');", true);
        }

        protected void gvMappedSchemes_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = (GridEditableItem)e.Item;
            int setupId = int.Parse(item.GetDataKeyValue("ACSTSM_SetupId").ToString());
            DateTime oldDate = DateTime.Parse(item.SavedOldValues["ValidTill"].ToString());
            DateTime newDate = ((RadDatePicker)item["schemeValidTill"].Controls[0]).SelectedDate.Value;

            //check whether it is not associated
            int retVal = commisionReceivableBo.updateStructureToSchemeMapping(setupId, newDate);
            if (retVal < 1) { return; }

            CreateMappedSchemeGrid();
        }

        protected void gvMappedSchemes_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = (GridEditableItem)e.Item;
            int setupId = int.Parse(item.GetDataKeyValue("ACSTSM_SetupId").ToString());

            //check whether it is not associated
            commisionReceivableBo.deleteStructureToSchemeMapping(setupId);

            CreateMappedSchemeGrid();
        }

        protected void gvMappedSchemes_OnItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                RequiredFieldValidator rfvRequired = new RequiredFieldValidator();
                rfvRequired.ControlToValidate = ((RadDatePicker)item["schemeValidTill"].Controls[0]).ID;
                rfvRequired.ErrorMessage = "Please select a valid date";
                rfvRequired.Display = ValidatorDisplay.Dynamic;

                CompareValidator cmvCompare = new CompareValidator();
                cmvCompare.ControlToCompare = ((RadDatePicker)item["schemeValidFrom"].Controls[0]).ID;
                cmvCompare.ControlToValidate = ((RadDatePicker)item["schemeValidTill"].Controls[0]).ID;
                cmvCompare.ErrorMessage = "Please select a valid date";
                cmvCompare.Operator = ValidationCompareOperator.GreaterThan;
                cmvCompare.Display = ValidatorDisplay.Dynamic;

                //Custom validator: checks whether Scheme association exits)
                CustomValidator cusValidator = new CustomValidator();
                cusValidator.ControlToValidate = ((RadDatePicker)item["schemeValidTill"].Controls[0]).ID;
                cusValidator.ErrorMessage = "This scheme association not permitted";
                cusValidator.Display = ValidatorDisplay.Dynamic;
                cusValidator.ServerValidate += new ServerValidateEventHandler(cusValidator_ServerValidate);

                item["schemeValidTill"].Controls.Add(rfvRequired);
                item["schemeValidTill"].Controls.Add(cmvCompare);
                item["schemeValidTill"].Controls.Add(cusValidator);
            }
        }

        void cusValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            GridEditableItem item = (GridEditableItem)((CustomValidator)source).NamingContainer;
            int setupId = int.Parse(item.GetDataKeyValue("ACSTSM_SetupId").ToString());
            DateTime validFrom = ((RadDatePicker)item["schemeValidFrom"].Controls[0]).SelectedDate.Value;
            DateTime validTill = ((RadDatePicker)item["schemeValidTill"].Controls[0]).SelectedDate.Value;

            args.IsValid = true;
            if (commisionReceivableBo.checkSchemeAssociationExists(setupId, validFrom, validTill)) { args.IsValid = false; }
        }

        protected void ibtExportSummary_OnClick(object sender, ImageClickEventArgs e)
        {
            DataTable dtMappedSchemes = new DataTable();
            dtMappedSchemes = (DataTable)Cache[userVo.UserId.ToString() + "MappedSchemes"];
            if (dtMappedSchemes == null)
                return;
            if (dtMappedSchemes.Rows.Count < 1)
                return;
            gvMappedSchemes.DataSource = dtMappedSchemes;
            gvMappedSchemes.ExportSettings.OpenInNewWindow = true;
            gvMappedSchemes.ExportSettings.IgnorePaging = true;
            gvMappedSchemes.ExportSettings.HideStructureColumns = true;
            gvMappedSchemes.ExportSettings.ExportOnlyData = true;
            gvMappedSchemes.ExportSettings.FileName = "MappedSchemes";
            gvMappedSchemes.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvMappedSchemes.MasterTableView.ExportToExcel();
            CreateMappedSchemeGrid();
        }
    }
}
