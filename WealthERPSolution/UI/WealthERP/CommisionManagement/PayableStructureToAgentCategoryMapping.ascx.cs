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
using BoAdvisorProfiling;

namespace WealthERP.CommisionManagement
{
    public partial class PayableStructureToAgentCategoryMapping : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        RMVo rmVo;
        bool isRedirect;
        AdvisorBo advisorBo = new AdvisorBo();
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session["rmVo"];
            isRedirect = false;
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    isRedirect = true;
                    hdnStructId.Value = Request.QueryString["ID"].Trim();
                    lbtStructureName.Visible = true;
                    SetStructureDetails();
                    //  CreateMappedSchemeGrid();
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
            //CreateMappedSchemeGrid();
            //pnlGrid.Visible = true;
        }


        protected void ddlType_Selectedindexchanged(object sender, EventArgs e)
        {
            GetControlsBasedOnType(ddlType.SelectedValue);
        }

        private void GetControlsBasedOnType(string type)
        {
            if (type == "Custom")
            {
                trListControls.Visible = true;
                ddlAdviserCategory.Visible = false;
                lblAssetCategory.Visible = false;
                BindAgentCodes();
            }
            else
            {
                trListControls.Visible = false;
                ddlAdviserCategory.Visible = true;
                lblAssetCategory.Visible = true;

                BindClassification();
            }
        }

        private void BindClassification()
        {
            DataSet classificationDs = new DataSet();

            classificationDs = advisorBo.GetAdviserCategory(advisorVo.advisorId);
            ddlAdviserCategory.DataSource = classificationDs;
            ddlAdviserCategory.DataValueField = classificationDs.Tables[0].Columns["AC_CategoryId"].ToString();
            ddlAdviserCategory.DataTextField = classificationDs.Tables[0].Columns["AC_CategoryName"].ToString();
            ddlAdviserCategory.DataBind();
            ddlAdviserCategory.Items.Insert(0, new ListItem("Select", "Select"));
        }

        private string convertSubcatListToCSV(List<RadListBoxItem> itemList)
        {
            string strSubcatsList = "";
            int nCount = itemList.Count, i = 0;
            foreach (RadListBoxItem item in itemList)
            {
                i++;
                strSubcatsList += item.Value;
                if (i < nCount) { strSubcatsList += ","; }
            }

            return strSubcatsList;
        }

        protected void lbtStructureName_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('ReceivableSetup','StructureId=" + this.hdnStructId.Value + "');", true);
        }

        private int CreatePayableMapping()
        {

            int mappingId = 0;
            string agentId = "";
            if (ddlType.SelectedValue == "Custom")
            {
                foreach (RadListBoxItem ListItem in this.RadListBoxSelectedAgentCodes.Items)
                {
                    agentId = agentId + ListItem.Value.ToString();
                }
            }

            commisionReceivableBo.CreatePayableAgentCodeMapping(Convert.ToInt32(hdnStructId.Value), ddlMapping.SelectedValue, ddlAdviserCategory.SelectedValue, agentId, out mappingId);
            return mappingId;

        }
        protected void btnGo_Click(object sender, EventArgs e)
        {

            int mappingId = CreatePayableMapping();
            if (mappingId > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Mapping Created SuccessFully');", true);

            }

        }
        private void BindAgentCodes()
        {
            DataSet dsAdviserBranchList = new DataSet();
            dsAdviserBranchList = commisionReceivableBo.GetAdviserAgentCodes(advisorVo.advisorId);
            LBAgentCodes.DataSource = dsAdviserBranchList;
            LBAgentCodes.DataValueField = "AgentId";
            LBAgentCodes.DataTextField = "AgentCodeWithName";
            LBAgentCodes.DataBind();

        }
        private void SetStructureDetails()
        {
            DataSet dsStructDet;
            try
            {
                dsStructDet = commisionReceivableBo.GetStructureDetails(advisorVo.advisorId, int.Parse(hdnStructId.Value));
                foreach (DataRow row in dsStructDet.Tables[0].Rows)
                {
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
                rlbAssetSubCategory.Items.Clear();
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
    }
}