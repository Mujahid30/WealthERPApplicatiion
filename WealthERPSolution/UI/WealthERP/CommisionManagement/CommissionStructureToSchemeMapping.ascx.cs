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
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session["rmVo"];

            if (!IsPostBack) {
                //pnlNewSchemes.Visible = false;
                pnlGrid.Visible = false;
                
                //BindStructDD();
                if (Request.QueryString["ID"] != null)
                {
                    hdnStructId.Value = Request.QueryString["ID"].Trim();
                    SetStructureDetails();
                    CreateMappedSchemeGrid();
                    SetDatePickControls();
                }                
            }
        }

        private string convertSubcatListToCSV(List<RadListBoxItem> itemList)
        {
            string strSubcatsList = "";
            int nCount = itemList.Count, i = 0;
            foreach (RadListBoxItem item in itemList) {
                i++;
                strSubcatsList += item.Value;
                if (i < nCount) {
                    strSubcatsList += ",";
                }
            }

            return strSubcatsList;
        }

        private void SetStructureDetails() {
            DataSet dsStructDet;
            try
            {
                dsStructDet = commisionReceivableBo.GetStructureDetails(advisorVo.advisorId, int.Parse(hdnStructId.Value));
                foreach (DataRow row in dsStructDet.Tables[0].Rows) {
                    txtStructureName.Text = row["ACSM_CommissionStructureName"].ToString();
                    txtStructureName.ToolTip = row["ACSM_CommissionStructureName"].ToString();
                    txtProductName.Text = row["PAG_AssetGroupName"].ToString();
                    txtProductName.ToolTip = row["PAG_AssetGroupName"].ToString();
                    txtCategory.Text = row["PAIC_AssetInstrumentCategoryName"].ToString();
                    txtCategory.ToolTip = row["PAIC_AssetInstrumentCategoryName"].ToString();
                    txtIssuerName.Text = row["PA_AMCName"].ToString();
                    txtIssuerName.ToolTip = row["PA_AMCName"].ToString();
                    hdnProductId.Value = row["PAG_AssetGroupCode"].ToString();
                    hdnStructValidFrom.Value = row["ACSM_ValidityStartDate"].ToString();
                    hdnStructValidTill.Value = row["ACSM_ValidityEndDate"].ToString();
                    hdnIssuerId.Value = row["PA_AMCCode"].ToString();
                    hdnCategoryId.Value = row["PAIC_AssetInstrumentCategoryCode"].ToString();
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
            DataSet dsMappedSchemes = new DataSet();
            dsMappedSchemes = commisionReceivableBo.GetMappedSchemes(int.Parse(hdnStructId.Value));
            gvMappedSchemes.DataSource = dsMappedSchemes.Tables[0];
            gvMappedSchemes.DataBind();
            Cache.Insert(userVo.UserId.ToString() + "MappedSchemes", dsMappedSchemes);
            pnlGrid.Visible = true;
        }

        private void BindMappedSchemesToList()
        {
            //DataSet dsMappedSchemes = new DataSet();
            //dsMappedSchemes = commisionReceivableBo.GetMappedSchemes(int.Parse(hdnStructId.Value), advisorVo.advisorId);
            //rlbAvailSchemes.DataSource = dsMappedSchemes.Tables[0];
            //rlbAvailSchemes.DataValueField = dsMappedSchemes.Tables[0].Columns["SchemePlanCode"].ToString();
            //rlbAvailSchemes.DataTextField = dsMappedSchemes.Tables[0].Columns["Name"].ToString();
            //rlbAvailSchemes.DataBind();
            //Cache.Insert(userVo.UserId.ToString() + "MappedSchemes", dsMappedSchemes);
            //pnlGrid.Visible = true;
        }

        private void BindAvailSchemesToList()
        {
            int sStructId = int.Parse(hdnStructId.Value);
            int sIssuerId = int.Parse(hdnIssuerId.Value);
            string sProduct = hdnProductId.Value;
            string sCategory = hdnCategoryId.Value;
            string sSubcats = hdnSubcategoryIds.Value;
            
            DateTime validFrom = rclPeriodStart.SelectedDate.Value;
            DateTime validTill = rclPeriodEnd.SelectedDate.Value;

            DataSet dsAvailSchemes = commisionReceivableBo.GetAvailSchemes(sStructId, sIssuerId, sProduct, sCategory, sSubcats, validFrom, validTill);
            rlbAvailSchemes.DataSource = dsAvailSchemes.Tables[0];
            rlbAvailSchemes.DataValueField = dsAvailSchemes.Tables[0].Columns["PASP_SchemePlanCode"].ToString();
            rlbAvailSchemes.DataTextField = dsAvailSchemes.Tables[0].Columns["PASP_SchemePlanName"].ToString();
            rlbAvailSchemes.DataBind();

            //Debug code
            lblAvailableSchemes.Text = "Available Schemes(" + rlbAvailSchemes.Items.Count.ToString() + ")";
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
            DataSet dsMappedSchemes = new DataSet();
            if (Cache[userVo.UserId.ToString() + "MappedSchemes"] != null)
            {
                dsMappedSchemes = (DataSet)Cache[userVo.UserId.ToString() + "MappedSchemes"];
                gvMappedSchemes.DataSource = dsMappedSchemes.Tables[0];
            }
        }

        protected void gvMappedSchemes_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            gvMappedSchemes.CurrentPageIndex = e.NewPageIndex;
            //CreateMappedSchemeGrid();
        }

        private void SetDatePickControls() 
        {
            rclPeriodStart.FocusedDate = DateTime.Parse(hdnStructValidFrom.Value.ToString());
            rclPeriodStart.MinDate =  DateTime.Parse(hdnStructValidFrom.Value.ToString());
            rclPeriodStart.SelectedDate = DateTime.Parse(hdnStructValidFrom.Value.ToString());
            rclPeriodStart.MaxDate = DateTime.Parse(hdnStructValidTill.Value.ToString()).AddDays(-1);
            rclPeriodEnd.FocusedDate = DateTime.Parse(hdnStructValidFrom.Value.ToString());
            rclPeriodEnd.MinDate = DateTime.Parse(hdnStructValidFrom.Value.ToString()).AddDays(1);
            rclPeriodEnd.MaxDate = DateTime.Parse(hdnStructValidTill.Value.ToString());
            rclPeriodEnd.SelectedDate = DateTime.Parse(hdnStructValidTill.Value.ToString());
        }

        protected void lnkAddNewSchemes_Click(object sender, EventArgs e)
        {
        }

        protected void ListBoxSource_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {

        }

        private void SetMappedSchemeDatePicker() {
            rdpMappedFrom.FocusedDate = DateTime.Parse(hdnStructValidFrom.Value.ToString());
            rdpMappedFrom.MinDate = DateTime.Parse(hdnStructValidFrom.Value.ToString());
            rdpMappedFrom.SelectedDate = DateTime.Parse(hdnStructValidFrom.Value.ToString());
            rdpMappedFrom.MaxDate = DateTime.Parse(hdnStructValidTill.Value.ToString()).AddDays(-1);
            rdpMappedTill.FocusedDate = DateTime.Parse(hdnStructValidFrom.Value.ToString());
            rdpMappedTill.MinDate = DateTime.Parse(hdnStructValidFrom.Value.ToString()).AddDays(1);
            rdpMappedTill.MaxDate = DateTime.Parse(hdnStructValidTill.Value.ToString());
            rdpMappedTill.SelectedDate = DateTime.Parse(hdnStructValidTill.Value.ToString());
        }

        protected void btn_GetAvailableSchemes_Click(object sender, EventArgs e)
        {
            rlbAvailSchemes.Items.Clear();
            rlbMappedSchemes.Items.Clear();
            BindAvailSchemesToList();
            BindMappedSchemesToList();
            SetMappedSchemeDatePicker();
        }

        protected void rlbAvailSchemes_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
            
        }

        protected void rlbMappedSchemes_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {

        }

        private void MapSchemesToStructure() 
        {
            if (rlbMappedSchemes.Items.Count < 1)
                return;
            List<int> schemeIds = new List<int>();
            foreach (RadListBoxItem item in rlbMappedSchemes.Items) {
                //schemeIds.Add(int.Parse(item.Value));
                int structId = int.Parse(hdnStructId.Value);
                commisionReceivableBo.MapSchemesToStructres(structId, int.Parse(item.Value), rdpMappedFrom.SelectedDate.Value, rdpMappedTill.SelectedDate.Value);
            }
        }

        protected void btnMapSchemes_Click(object sender, EventArgs e)
        {
            MapSchemesToStructure();
            CreateMappedSchemeGrid();
            rlbAvailSchemes.Items.Clear();
            BindAvailSchemesToList();
            rlbMappedSchemes.Items.Clear();
            BindMappedSchemesToList();
        }
    }
}