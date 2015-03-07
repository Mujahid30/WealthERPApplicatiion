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
                    DefaultAssignments();
                    //  CreateMappedSchemeGrid();
                    BindPayableGrid(int.Parse(hdnStructId.Value));
                    ddlType.SelectedValue = "Custom";
                }
                else
                {
                    Cache.Remove(userVo.UserId.ToString() + "MappedSchemes");
                    isRedirect = false;
                    getAllStructures();
                    ddlStructs.Visible = true;
                }
                if (Request.QueryString["Action"] != null)
                {
                    if (Request.QueryString["Action"] == "VIEW")
                    {
                        trMappings.Visible = false;
                        trListControls.Visible = false;
                        btnGo.Visible = false;

                    }
                    else
                    {
                        trMappings.Visible = true;
                        trListControls.Visible = false;
                        btnGo.Visible = true;

                    }

                }
                //BindPayableGrid();
                SelectionsBasedOnMappingFor();


            }
        }


      

     

        private void DefaultAssignments()
        {
            ddlMapping.SelectedValue = "Associate";
            ddlType.SelectedValue = "Custom";
            GetControlsBasedOnType(ddlType.SelectedValue);
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

        protected void ddlMapping_Selectedindexchanged(object sender, EventArgs e)
        {
            SelectionsBasedOnMappingFor();
            GetControlsBasedOnType(ddlType.SelectedValue);
        }


        private void SelectionsBasedOnMappingFor()
        {
            if (ddlMapping.SelectedValue == "Staff")
            {
                ddlType.SelectedValue = "Custom";
                ddlType.Enabled = false;
                rfvddlAdviserCategory.Visible = false;
                RadListBoxSelectedAgentCodes.Items.Clear();
            }
            else
            {

                if (ddlType.SelectedValue == "Custom")
                {
                    RadListBoxSelectedAgentCodes.Items.Clear();
                }
                ddlType.SelectedValue = "Custom";
                ddlType.Enabled = true;



            }
        }


        private void GetControlsBasedOnType(string type)
        {
            if (type == "Custom")
            {
                trListControls.Visible = true;
                trAssetCategory.Visible = false;
                BindAgentCodes();
                ddlType.SelectedValue = "Custom";
            }
            else
            {
                trListControls.Visible = false;
                trAssetCategory.Visible = true;
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
            string ruleId = string.Empty;
            //string[] ruleid=new string[];
            //int ruleId = 0;
            //if (Request.QueryString["ruleId"] != null)
            //{
            //    ruleId = Request.QueryString["ruleId"].ToString();
            //    //ruleid=ruleId.Split(',');
            //}
            foreach (GridDataItem gdi in rgPayableMapping.MasterTableView.Items)
            {
                RadioButtonList rbtnListRate = (RadioButtonList)gdi.FindControl("rbtnListRate");
                if (rbtnListRate.SelectedItem != null)
                    ruleId += rbtnListRate.SelectedValue + ",";
               
            }
            if (ruleId != "")
            {
                ruleId = ruleId.TrimEnd(',');
                DataTable dtRuleMapping = new DataTable();
                dtRuleMapping.Columns.Add("agentId",typeof(string));
                dtRuleMapping.Columns.Add("ruleids");
                //dtRuleMapping.Columns.Add("categoryId", typeof(Int32));
                DataRow drRuleMapping;
                int mappingId = 0;
                string agentId = "";
                string categoryId = string.Empty;
                if (ddlType.SelectedValue == "Custom")
                {
                    foreach (RadListBoxItem ListItem in this.RadListBoxSelectedAgentCodes.Items)
                    {

                        agentId = ListItem.Value;
                        foreach (object rule in ruleId.Split(','))
                        {
                            drRuleMapping = dtRuleMapping.NewRow();
                            drRuleMapping["agentId"] = agentId;
                            drRuleMapping["ruleids"] = rule;
                            dtRuleMapping.Rows.Add(drRuleMapping);
                        }
                    }

                }
                else
                {
                    categoryId = ddlAdviserCategory.SelectedValue;
                }

                commisionReceivableBo.CreateAdviserPayableRuleToAgentCategoryMapping(Convert.ToInt32(hdnStructId.Value), ddlMapping.SelectedValue, categoryId, dtRuleMapping, ruleId.TrimEnd(','), out mappingId);
                return mappingId;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select rate(%)');", true);
                return 0;
            }
        }
        protected void btnGo_Click(object sender, EventArgs e)
        {

            int mappingId = CreatePayableMapping();
            if (mappingId > 0)
            {
                //BindPayableGrid();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Mapping Created SuccessFully');", true);
                BindPayableGrid(int.Parse(hdnStructId.Value));

            }

        }
        private void BindAgentCodes()
        {
            DataSet dsAdviserBranchList = new DataSet();
            dsAdviserBranchList = commisionReceivableBo.GetAdviserAgentCodes(advisorVo.advisorId, ddlMapping.SelectedValue);
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
                    ddlMapping.SelectedValue = row["UserType"].ToString();
                    if (ddlMapping.SelectedValue == "Associate")
                    {
                        ddlType.SelectedValue = "Custom";
                    }
                    else
                    {
                        ddlType.SelectedValue = "Custom";

                    }
                    GetControlsBasedOnType(ddlType.SelectedValue);
                    ddlAdviserCategory.SelectedValue = row["AC_CategoryId"].ToString();
                    ddlType.SelectedValue = "Custom";
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
        private void BindPayableGrid(int structureId)
        {
            DataSet dsLookupData;
            dsLookupData = commisionReceivableBo.GetPayableCommissionTypeBrokerage(structureId);
            ViewState["dsrate"] = dsLookupData;
            rgPayableMapping.DataSource = dsLookupData;
            rgPayableMapping.DataBind();
            //rgchecklist.DataSource = dsLookupData;
            //rgchecklist.DataBind();
            //btnIssueMap.Visible = true;
            //Table5.Visible = true;

            if (Cache[userVo.UserId.ToString() + "RulePayableDet"] != null)
                Cache.Remove(userVo.UserId.ToString() + "RulePayableDet");
            Cache.Insert(userVo.UserId.ToString() + "RulePayableDet", dsLookupData.Tables[0]);
        }
        protected void rgPayableMapping_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid rgCommissionTypeCaliculation = (RadGrid)sender;

            DataTable dtLookupData;
            dtLookupData = (DataTable)Cache[userVo.UserId.ToString() + "RulePayableDet"];
            if (dtLookupData != null)
            {
                rgPayableMapping.DataSource = dtLookupData;
            }

        }
        protected void rgPayableMapping_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            //{
            //}
            DataSet dsratelist = (DataSet)ViewState["dsrate"];
            if (e.Item is GridDataItem)
            {
                RadioButtonList rbtnListRate = e.Item.FindControl("rbtnListRate") as RadioButtonList;
                int ruleId = int.Parse(rgPayableMapping.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_CommissionStructureRuleId"].ToString());

                DataView dv = dsratelist.Tables[1].DefaultView;
                dv.RowFilter = "ACSR_CommissionStructureRuleId = '" + ruleId.ToString() + "'";
                if (rbtnListRate != null)
                {
                    rbtnListRate.DataSource = dv;
                    rbtnListRate.DataValueField = "CSRD_StructureRuleDetailsId";
                    rbtnListRate.DataTextField = "CSRD_BrokageValue";
                    rbtnListRate.DataBind();
                    //rbtnListRate.Items[0].Selected = true;

                    if (Request.QueryString["StructureId"] != null)
                    {
                        int ruleids = int.Parse(Request.QueryString["StructureId"].ToString());
                        DataTable dt = commisionReceivableBo.GetMappedStructure(ruleids);
                        foreach (DataRow dr in dt.Rows)
                        {
                            foreach (ListItem obj1 in rbtnListRate.Items)
                            {
                                if (dr["CSRD_StructureRuleDetailsId"].ToString() == obj1.Value)
                                {
                                    obj1.Selected = true;
                                    hdneligible.Value = "Eligible";
                                }
                            }
                        }
                    }
                }
            }
        }
        protected void ListBoxSource_Transferred(object source, Telerik.Web.UI.RadListBoxTransferredEventArgs e)
        {

            LBAgentCodes.Items.Sort();

        }
    }
}