using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
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
using BoAdvisorProfiling;
using BoOnlineOrderManagement;
using BoOps;

namespace WealthERP.Receivable
{
    public partial class ReceivableSetup : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        RMVo rmVo;
        AdvisorBo advisorBo = new AdvisorBo();
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        CommissionStructureMasterVo commissionStructureMasterVo;
        CommissionStructureRuleVo commissionStructureRuleVo = new CommissionStructureRuleVo();
        int issueid = 0;
        int categoryId = 0;
        string MappedruleId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session["rmVo"];
            int structureId = 0;
            radAplicationPopUp.VisibleOnPageLoad = false;
            if (!IsPostBack)
            {

                if (Request.QueryString["StructureId"] != null)
                {
                    structureId = Convert.ToInt32(Request.QueryString["StructureId"].ToString());
                    ddlProductType.SelectedValue = "MF";
                    hidCommissionStructureName.Value = structureId.ToString();
                    BindPayableGrid();
                    Table5.Visible = true;
                }
                GetProduct();


                if (structureId != 0)
                {
                    TogglePanel();
                    ddlProductType.SelectedValue = Request.QueryString["ProductType"].ToString();
                    BindAllDropdown();
                    LoadStructureDetails(structureId);
                    BindCommissionStructureRuleGrid(structureId);
                    trPayableMapping.Visible = true;
                    tbNcdIssueList.Visible = false;
                    if (ddlProductType.SelectedValue == "MF")
                    {
                        pnlAddSchemesButton.Visible = true;
                        Table2.Visible = true;
                        CreateMappedSchemeGrid();
                        pnlIssueList.Visible = false;

                    }
                    else if (ddlProductType.SelectedValue != "MF")
                    {

                        GetMapped_Unmapped_Issues("Mapped", "");
                        if (ddlProductType.SelectedValue == "IP")
                            GetUnamppedIssues("FIFIIP");
                        else
                            GetUnamppedIssues(ddlIssueType.SelectedValue);
                        Table4.Visible = true;
                        tbNcdIssueList.Visible = false;
                        pnlIssueList.Visible = true;
                        RadGridStructureRule.MasterTableView.GetColumn("ACSR_TransactionType").Visible = false;
                    }

                }
                else
                {
                    ControlStateNewStructureCreate();
                }
            }
        }

        protected void imgBuy_Click(object sender, ImageClickEventArgs e)
        {
            ShowAndHideVisible_FirstSection();
        }


        private void ShowAndHideVisible_FirstSection()
        {

            if (tb1.Visible == true)
            {
                tb1.Visible = false;

                ImageButton3.ImageUrl = "~/Images/toggle-expand-alt_blue.png";
                ImageButton3.ToolTip = "Expend";
            }
            else
            {
                tb1.Visible = true;
                ImageButton3.ImageUrl = "~/Images/toggle-collapse-alt_blue.png";
                ImageButton3.ToolTip = "Collapse";
            }

        }


        protected void imgBuyMapping_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlProductType.SelectedValue == "MF")
            {
                if (pnlAddSchemes.Visible == true)
                {
                    pnlAddSchemesButton.Visible = false;
                    pnlAddSchemes.Visible = false;
                    gvMappedSchemes.Visible = false;
                    pnlGrid.Visible = false;
                    ImageButton4.ToolTip = "Expend";
                    ImageButton4.ImageUrl = "~/Images/toggle-expand-alt_blue.png";
                }
                else
                {
                    pnlAddSchemes.Visible = true;
                    pnlAddSchemesButton.Visible = true;
                    ImageButton4.ImageUrl = "~/Images/toggle-collapse-alt_blue.png";
                    ImageButton4.ToolTip = "Collapse";
                    pnlGrid.Visible = true;
                    gvMappedSchemes.Visible = true;


                }

            }



        }
        protected void imgNcd_Click(object sender, ImageClickEventArgs e)
        {


            if (ddlProductType.SelectedValue != "MF")
            {

                if (tbNcdIssueList.Visible == true)
                {
                    tbNcdIssueList.Visible = false;
                    gvMappedIssueList.Visible = false;
                    pnlIssueList.Visible = false;
                    ImageButton5.ToolTip = "Expend";
                    ImageButton5.ImageUrl = "~/Images/toggle-expand-alt_blue.png";
                }
                else
                {
                    gvMappedIssueList.Visible = true;
                    tbNcdIssueList.Visible = true;
                    pnlIssueList.Visible = true;
                    ImageButton5.ImageUrl = "~/Images/toggle-collapse-alt_blue.png";
                    ImageButton5.ToolTip = "Collapse";
                }
            }
            else
            {

                if (tbNcdIssueList.Visible == true)
                {
                    tbNcdIssueList.Visible = false;
                    pnlIssueList.Visible = false;
                    ImageButton5.ToolTip = "Expend";
                    ImageButton5.ImageUrl = "~/Images/toggle-expand-alt_blue.png";
                }
                else
                {
                    tbNcdIssueList.Visible = true;
                    pnlIssueList.Visible = true;
                    ImageButton5.ImageUrl = "~/Images/toggle-collapse-alt_blue.png";
                    ImageButton5.ToolTip = "Collapse";
                }

            }
        }
        protected void imgBuy1_Click(object sender, ImageClickEventArgs e)
        {
            if (tblCommissionStructureRule1.Visible == true)
            {
                tblCommissionStructureRule1.Visible = false;
                imgBuy1.ImageUrl = "~/Images/toggle-expand-alt_blue.png";
                imgBuy1.ToolTip = "Expend";
            }
            else
            {
                tblCommissionStructureRule1.Visible = true;
                imgBuy1.ImageUrl = "~/Images/toggle-collapse-alt_blue.png";
                imgBuy1.ToolTip = "Collapse";
            }
        }


        protected void imgBuy3_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ddlProductType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindAllDropdown();
            ShowHideControlsBasedOnProduct(ddlProductType.SelectedValue);
            GetCategory(ddlProductType.SelectedValue);
            ShowHideRadGridStructureRulecolumns();

        }
        protected void chkListApplyTax_CheckChanged(object sender, EventArgs e)
        {
            CheckBoxList chkListApplyTax = (CheckBoxList)sender;
            GridEditFormItem gdi = (GridEditFormItem)chkListApplyTax.NamingContainer;
            CheckBoxList chkListApplyTax1 = (CheckBoxList)gdi.FindControl("chkListApplyTax");
            TextBox txtTaxValue = (TextBox)gdi.FindControl("txtTaxValue");
            TextBox txtTDS = (TextBox)gdi.FindControl("txtTDS");
            TextBox txtSBCValue = (TextBox)gdi.FindControl("txtSBCValue");
            TextBox txtKKCValue = (TextBox)gdi.FindControl("txtKKCValue");
            Label lblApplyTaxes = (Label)gdi.FindControl("lblApplyTaxes");
            lblApplyTaxes.Visible = false;
            if (chkListApplyTax.Items[0].Selected)
            {
                txtTaxValue.Visible = true;
                lblApplyTaxes.Visible = true;
            }
            else
            {
                txtTaxValue.Visible = false;

            }
            if (chkListApplyTax.Items[1].Selected)
            {
                txtTDS.Visible = true;
                lblApplyTaxes.Visible = true;
            }
            else
            {
                txtTDS.Visible = false;

            }
            if (chkListApplyTax.Items[2].Selected)
            {
                txtSBCValue.Visible = true;
                lblApplyTaxes.Visible = true;
            }
            else
            {
                txtSBCValue.Visible = false;

            }
            if (chkListApplyTax.Items[3].Selected)
            {
                txtKKCValue.Visible = true;
                lblApplyTaxes.Visible = true;
            }
            else
            {
                txtKKCValue.Visible = false;

            }
            if (chkListApplyTax.Items[0].Selected && chkListApplyTax.Items[1].Selected && chkListApplyTax.Items[2].Selected && chkListApplyTax.Items[3].Selected)
            {
                txtTaxValue.Visible = true;
                txtTDS.Visible = true;
                txtKKCValue.Visible = true;
                txtSBCValue.Visible = true;
                lblApplyTaxes.Visible = true;

            }



        }

        private void ShowHideRadGridStructureRulecolumns()
        {

            if (ddlProductType.SelectedValue == "MF")
            {

                RadGridStructureRule.MasterTableView.GetColumn("ACSR_TenureUnit").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("ACSM_AUMFrequency").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("ACSR_AUMMonth").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("ACSR_InvestmentAgeUnit").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("ACSR_TransactionType").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("ACSR_MinInvestmentAge").Visible = true;
                RadGridStructureRule.MasterTableView.GetColumn("ACSR_MaxInvestmentAge").Visible = true;

            }
            else
            {
                RadGridStructureRule.MasterTableView.GetColumn("ACSR_MinTenure").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("ACSR_MaxTenure").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("ACSR_TenureUnit").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("ACSR_MinInvestmentAge").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("ACSR_MaxInvestmentAge").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("ACSM_AUMFrequency").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("ACSR_AUMMonth").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("ACSR_InvestmentAgeUnit").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("ACSR_TransactionType").Visible = false;

            }

        }
        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubcategoryListBox(ddlCategory.SelectedValue);
        }
        protected void btnSubmitCommissionTypeBrokerage_Click(object sender, EventArgs e)
        {
            //RadWDCommissionTypeBrokerage.VisibleOnPageLoad = true;

        }

        private void CreateUpdateDeleteAplication(int fromRange, int toRange, int adviserId, int issueId, int formRangeId, string commandType)
        {

        }
        protected void llPurchase_Click(object sender, EventArgs e)
        {
            int rowindex1 = ((GridDataItem)((LinkButton)sender).NamingContainer).RowIndex;
            int rowindex = (rowindex1 / 2) - 1;
            LinkButton lbButton = (LinkButton)sender;
            GridDataItem item = (GridDataItem)lbButton.NamingContainer;

        }



        protected void llView_Click(object sender, EventArgs e)
        {
            int rowindex1 = ((GridDataItem)((LinkButton)sender).NamingContainer).RowIndex;
            int rowindex = (rowindex1 / 2) - 1;
            LinkButton lbButton = (LinkButton)sender;
            GridDataItem item = (GridDataItem)lbButton.NamingContainer;
            string ruleid = string.Empty;
            if (ruleid != null)
            {
                string structureId = hidCommissionStructureName.Value;
                string myscript = "window.open('PopUp.aspx?ID=" + structureId + "&ruleId=" + ruleid.TrimEnd(',') + "&Action=VIEW&pageID=PayableStructureToAgentCategoryMapping&', 'mywindow', 'width=1000,height=600,scrollbars=yes,location=no')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "<script>" + myscript + "</script>", false);
            }
        }

        protected void llViewUnMapping_Click(object sender, EventArgs e)
        {
            int rowindex1 = ((GridDataItem)((LinkButton)sender).NamingContainer).RowIndex;
            int rowindex = (rowindex1 / 2) - 1;
            LinkButton lbButton = (LinkButton)sender;
            GridDataItem item = (GridDataItem)lbButton.NamingContainer;

        }
        private void BindRuleDetGrid(RadGrid rgCommissionTypeCaliculation, int ruleId)
        {
            DataSet dsLookupData;
            dsLookupData = commisionReceivableBo.GetCommissionTypeBrokerage(ruleId);
            rgCommissionTypeCaliculation.DataSource = dsLookupData;
            rgCommissionTypeCaliculation.DataBind();
            btnIssueMap.Visible = true;
            if (Cache[userVo.UserId.ToString() + "RuleDet"] != null)
                Cache.Remove(userVo.UserId.ToString() + "RuleDet");
            Cache.Insert(userVo.UserId.ToString() + "RuleDet", dsLookupData.Tables[0]);

        }



        protected void rgCommissionTypeCaliculation_ItemCommand(object source, GridCommandEventArgs e)
        {
            int ruleId = 0;
            int ruledetId = 0;
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                RadGrid rgCommissionTypeCaliculation = (RadGrid)source;
                ruleId = Convert.ToInt32(HiddenField1.Value);
                TextBox txtBrokerageValue = (TextBox)e.Item.FindControl("txtBrokerageValue");
                DropDownList ddlCommissionype = (DropDownList)e.Item.FindControl("ddlCommissionype");
                DropDownList ddlmode = (DropDownList)e.Item.FindControl("ddlmode");
                DropDownList ddlBrokerageUnit = (DropDownList)e.Item.FindControl("ddlBrokerageUnit");
                TextBox txtRateName = (TextBox)e.Item.FindControl("txtRateName");
                DropDownList ddlBrokerCode = (DropDownList)e.Item.FindControl("ddlBrokerCode");
                commisionReceivableBo.CreateUpdateDeleteCommissionTypeBrokerage(ruleId, Convert.ToInt32(ddlCommissionype.SelectedValue), Convert.ToInt32(ddlmode.SelectedValue), ddlBrokerageUnit.SelectedValue, Convert.ToDecimal(txtBrokerageValue.Text), txtRateName.Text, "INSERT", 0, 0, ddlBrokerCode.SelectedValue);
                BindRuleDetGrid(rgCommissionTypeCaliculation, ruleId);
            }
            else if (e.CommandName == RadGrid.UpdateCommandName)
            {
                RadGrid rgCommissionTypeCaliculation = (RadGrid)source;
                ruleId = Convert.ToInt32(HiddenField1.Value);
                ruledetId = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CSRD_StructureRuleDetailsId"].ToString());

                TextBox txtBrokerageValue = (TextBox)e.Item.FindControl("txtBrokerageValue");
                DropDownList ddlCommissionype = (DropDownList)e.Item.FindControl("ddlCommissionype");
                DropDownList ddlmode = (DropDownList)e.Item.FindControl("ddlmode");
                DropDownList ddlBrokerageUnit = (DropDownList)e.Item.FindControl("ddlBrokerageUnit");
                TextBox txtRateName = (TextBox)e.Item.FindControl("txtRateName");
                DropDownList ddlBrokerCode = (DropDownList)e.Item.FindControl("ddlBrokerCode");

                commisionReceivableBo.CreateUpdateDeleteCommissionTypeBrokerage(ruleId, Convert.ToInt32(ddlCommissionype.SelectedValue), Convert.ToInt32(ddlmode.SelectedValue), ddlBrokerageUnit.SelectedValue, Convert.ToDecimal(txtBrokerageValue.Text), txtRateName.Text, "UPDATE", ruledetId, 0, ddlBrokerCode.SelectedValue);

                BindRuleDetGrid(rgCommissionTypeCaliculation, ruleId);
                BindCommissionStructureRuleGrid(Convert.ToInt32(hidCommissionStructureName.Value));
            }
            else if (e.CommandName == RadGrid.DeleteCommandName)
            {
            }

        }

        protected void rgCommissionTypeCaliculation_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                DropDownList ddlCommissionype = (DropDownList)e.Item.FindControl("ddlCommissionype");
                DropDownList ddlBrokerageUnit = (DropDownList)e.Item.FindControl("ddlBrokerageUnit");
                DropDownList ddlBrokerCode = (DropDownList)e.Item.FindControl("ddlBrokerCode");
                DropDownList ddlmode = (DropDownList)e.Item.FindControl("ddlmode");
                System.Web.UI.HtmlControls.HtmlTableCell tdlblBrokerCode = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdlblBrokerCode");
                System.Web.UI.HtmlControls.HtmlTableCell tdddlBrokerCode = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdddlBrokerCode");
                tdlblBrokerCode.Visible = false;
                tdddlBrokerCode.Visible = false;
                DataSet dscommissionTypes;
                dscommissionTypes = commisionReceivableBo.GetCommisionTypes();
                DataSet dsModeTypes;
                dsModeTypes = commisionReceivableBo.GetModeTypes();
                if (ddlProductType.SelectedValue != "MF")
                {
                    tdlblBrokerCode.Visible = true;
                    tdddlBrokerCode.Visible = true;
                    BindBrokerCode(ddlBrokerCode, int.Parse(gvMappedIssueList.MasterTableView.DataKeyValues[0]["AIM_IssueId"].ToString()));
                }
                ddlCommissionype.DataSource = dscommissionTypes.Tables[0];
                ddlCommissionype.DataValueField = "WCMV_LookupId";
                ddlCommissionype.DataTextField = "WCMV_Name";
                ddlCommissionype.DataBind();
                ddlCommissionype.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                ddlmode.DataSource = dsModeTypes.Tables[0];
                ddlmode.DataValueField = "WCMV_LookupId";
                ddlmode.DataTextField = "WCMV_Name";
                ddlmode.DataBind();
                ddlmode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));


                DataSet dsCommissionLookup;
                dsCommissionLookup = (DataSet)Session["CommissionLookUpData"];
                ddlBrokerageUnit.DataSource = dsCommissionLookup.Tables[3];
                ddlBrokerageUnit.DataValueField = dsCommissionLookup.Tables[3].Columns["WCU_UnitCode"].ToString();
                ddlBrokerageUnit.DataTextField = dsCommissionLookup.Tables[3].Columns["WCU_Unit"].ToString();
                ddlBrokerageUnit.DataBind();
                ddlBrokerageUnit.SelectedValue = "PER";
                ddlBrokerageUnit.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            }
            else if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode) && e.Item.ItemIndex != -1)
            {

                GridEditFormItem editFormItem = e.Item as GridEditFormItem;
                GridDataItem parentItem = editFormItem.ParentItem;
                int ruleID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CSRD_StructureRuleDetailsId"].ToString());
                DataSet dsCommissionTypesAndBrokerage;
                dsCommissionTypesAndBrokerage = commisionReceivableBo.GetCommissionTypeAndBrokerage(ruleID);
                DropDownList ddlCommissionype = (DropDownList)editFormItem.FindControl("ddlCommissionype");
                DropDownList ddlBrokerageUnit = (DropDownList)editFormItem.FindControl("ddlBrokerageUnit");
                TextBox txtBrokerageValue = editFormItem.FindControl("txtBrokerageValue") as TextBox;
                txtBrokerageValue.Text = parentItem["CSRD_BrokageValue"].Text;
                DropDownList ddlBrokerCode = (DropDownList)e.Item.FindControl("ddlBrokerCode");
                System.Web.UI.HtmlControls.HtmlTableCell tdlblBrokerCode = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdlblBrokerCode");
                System.Web.UI.HtmlControls.HtmlTableCell tdddlBrokerCode = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdddlBrokerCode");
                if (ddlProductType.SelectedValue != "MF")
                    BindBrokerCode(ddlBrokerCode, int.Parse(gvMappedIssueList.MasterTableView.DataKeyValues[0]["AIM_IssueId"].ToString()));
                string brokerCode = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["XB_BrokerIdentifier"].ToString();
                ddlBrokerCode.SelectedValue = brokerCode;
                DataSet dscommissionTypes;
                dscommissionTypes = commisionReceivableBo.GetCommisionTypes();
                ddlCommissionype.DataSource = dscommissionTypes.Tables[0];
                ddlCommissionype.DataValueField = "WCMV_LookupId";
                ddlCommissionype.DataTextField = "WCMV_Name";
                ddlCommissionype.DataBind();
                ddlCommissionype.SelectedValue = dsCommissionTypesAndBrokerage.Tables[0].Rows[0].ItemArray[4].ToString();
                DataSet dsCommissionLookup;
                dsCommissionLookup = (DataSet)Session["CommissionLookUpData"];
                ddlBrokerageUnit.DataSource = dsCommissionLookup.Tables[3];
                ddlBrokerageUnit.DataValueField = dsCommissionLookup.Tables[3].Columns["WCU_UnitCode"].ToString();
                ddlBrokerageUnit.DataTextField = dsCommissionLookup.Tables[3].Columns["WCU_Unit"].ToString();
                ddlBrokerageUnit.DataBind();
                ddlBrokerageUnit.SelectedValue = "PER";
                ddlBrokerageUnit.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                ddlBrokerageUnit.SelectedValue = dsCommissionTypesAndBrokerage.Tables[0].Rows[0].ItemArray[2].ToString();
            }

        }

        protected void rgCommissionTypeCaliculation_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid rgCommissionTypeCaliculation = (RadGrid)sender;

            DataSet dsLookupData;
            if (string.IsNullOrEmpty(HiddenField1.Value))
                HiddenField1.Value = "0";
            dsLookupData = commisionReceivableBo.GetCommissionTypeBrokerage(Convert.ToInt32(HiddenField1.Value));
            rgCommissionTypeCaliculation.DataSource = dsLookupData.Tables[0];



        }
        protected void BtnActivRangeClose_Click(object sender, EventArgs e)
        {
            //RadWDCommissionTypeBrokerage.VisibleOnPageLoad = false;

        }

        protected void ddlBrokerageUnit_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlBrokerageUnit = (DropDownList)sender;
            DropDownList ddlCommisionCalOn = new DropDownList();
        }


        private void ShowHideControlsBasedOnProduct(string asset)
        {
            ddlSubInstrCategory.Items.Clear();
            ddlSubInstrCategory.DataBind();
            if (asset == "MF")
            {
                trIssuer.Visible = true;
                lblCategory.Visible = false;
                ddlCategory.Visible = false;
                lblSubCategory.Visible = false;
                SpanCategory.Visible = false;
                tdlblCategory.Visible = false;
                tdddlCategory.Visible = false;
                GetMFCategory("MF");

            }
            else if (asset == "FI")
            {
                trIssuer.Visible = false;
                lblCategory.Visible = false;
                ddlCategory.Visible = false;
                lblSubCategory.Visible = false;
                SpanCategory.Visible = false;
                tdlblCategory.Visible = true;
                tdddlCategory.Visible = true;
                BindBondCategories();
                RadGridStructureRule.MasterTableView.GetColumn("ACSR_TransactionType").Visible = false;
            }
            else if (asset == "IP")
            {
                trIssuer.Visible = false;
                lblCategory.Visible = false;
                ddlCategory.Visible = false;
                lblSubCategory.Visible = false;
                SpanCategory.Visible = false;
                tdlblCategory.Visible = false;
                tdddlCategory.Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("ACSR_TransactionType").Visible = false;
            }


        }

        private void BindSubcategoryListBox(string categoryCode)
        {
            DataTable dtSubcategory = new DataTable();
            dtSubcategory = commonLookupBo.GetMFInstrumentSubCategory(categoryCode);
            rlbAssetSubCategory.DataSource = dtSubcategory;
            rlbAssetSubCategory.DataValueField = dtSubcategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
            rlbAssetSubCategory.DataTextField = dtSubcategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
            rlbAssetSubCategory.DataBind();

            foreach (RadListBoxItem item in rlbAssetSubCategory.Items)
            {
                item.Checked = true;

            }

        }


        protected void GetProduct()
        {
            DataSet dsproduct;
            dsproduct = commisionReceivableBo.GetProduct(advisorVo.advisorId);

            ddlProductType.DataSource = dsproduct.Tables[0];
            ddlProductType.DataValueField = dsproduct.Tables[0].Columns["PAG_AssetGroupCode"].ToString();
            ddlProductType.DataTextField = dsproduct.Tables[0].Columns["PAG_AssetGroupName"].ToString();
            ddlProductType.DataBind();

        }

        protected void GetCategory(string product)
        {
            DataSet dsLookupData;
            dsLookupData = commisionReceivableBo.GetCategories(ddlProductType.SelectedValue);

            ddlCategory.DataSource = dsLookupData.Tables[0];
            ddlCategory.DataValueField = dsLookupData.Tables[0].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory.DataTextField = dsLookupData.Tables[0].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory.DataBind();
        }
        protected void GetMFCategory(string product)
        {
            DataSet dsLookupData;
            dsLookupData = commisionReceivableBo.GetCategories(ddlProductType.SelectedValue);

            ddlMFCategory.DataSource = dsLookupData.Tables[0];
            ddlMFCategory.DataValueField = dsLookupData.Tables[0].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlMFCategory.DataTextField = dsLookupData.Tables[0].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlMFCategory.DataBind();
            ddlMFCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }
        protected void GetCommisionTypes()
        {

        }

        protected void BindAllDropdown()
        {
            DataSet dsLookupData;
            dsLookupData = commisionReceivableBo.GetLookupDataForReceivableSetUP(advisorVo.advisorId, ddlProductType.SelectedValue);

            ddlIssuer.DataSource = dsLookupData.Tables[6];
            ddlIssuer.DataValueField = dsLookupData.Tables[6].Columns["PA_AMCCode"].ToString();
            ddlIssuer.DataTextField = dsLookupData.Tables[6].Columns["PA_AMCName"].ToString();
            ddlIssuer.DataBind();
            ddlIssuer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            Session["CommissionLookUpData"] = dsLookupData;

        }

        protected void SetStructureMasterControlDefaultValues(string assetType)
        {
            if (assetType == "MF")
            {

                ddlProductType.SelectedValue = "MF";
                ddlCategory.SelectedValue = "MFDT";
                GetMFCategory("MF");

            }
            else if (assetType == "FI")
            {
                ddlProductType.SelectedValue = "FI";
                ddlCategory.SelectedValue = "FISD";
            }
            else if (assetType == "IP")
            {
                ddlProductType.SelectedValue = "IP";
                ddlCategory.SelectedValue = "FIIP";
            }

        }


        protected CommissionStructureMasterVo CollectStructureMastetrData()
        {
            commissionStructureMasterVo = new CommissionStructureMasterVo();
            StringBuilder strSubCategoryCode = new StringBuilder();
            try
            {

                commissionStructureMasterVo.AdviserId = advisorVo.advisorId;
                commissionStructureMasterVo.ProductType = ddlProductType.SelectedValue;
                commissionStructureMasterVo.AssetCategory = ddlCategory.SelectedValue;
                commissionStructureMasterVo.Issuer = ddlIssuer.SelectedValue;

                commissionStructureMasterVo.ValidityStartDate = Convert.ToDateTime(txtValidityFrom.Text);
                commissionStructureMasterVo.ValidityEndDate = Convert.ToDateTime(txtValidityTo.Text);
                commissionStructureMasterVo.CommissionStructureName = txtStructureName.Text.Trim();
                hdnRulestart.Value = txtValidityFrom.Text;
                hdnRuleEnd.Value = txtValidityTo.Text;
                commissionStructureMasterVo.IsClawBackApplicable = chkHasClawBackOption.Checked;
                commissionStructureMasterVo.IsNonMonetaryReward = chkMoneytaryReward.Checked;

                hdnProductId.Value = ddlProductType.SelectedValue;
                hdnStructValidFrom.Value = txtValidityFrom.Text;
                hdnStructValidTill.Value = txtValidityTo.Text;
                hdnIssuerId.Value = ddlIssuer.SelectedValue;
                hdnCategoryId.Value = ddlCategory.SelectedValue;

                commissionStructureMasterVo.StructureNote = txtNote.Text.Trim();


                if (ddlProductType.SelectedValue == "IP")
                {

                    strSubCategoryCode.Append("FIFIIP");
                    commissionStructureMasterVo.AssetSubCategory = strSubCategoryCode;
                    commissionStructureMasterVo.AssetCategory = "FIIP";
                    string subcategoryIds = commissionStructureMasterVo.AssetSubCategory.ToString();
                    subcategoryIds = subcategoryIds.Replace("~", ",");
                    hdnSubcategoryIds.Value = subcategoryIds;
                }
                else if (ddlProductType.SelectedValue == "FI")
                {
                    strSubCategoryCode.Append(ddlSubInstrCategory.SelectedValue);
                    commissionStructureMasterVo.AssetSubCategory = strSubCategoryCode;
                    commissionStructureMasterVo.AssetCategory = "FICD";
                    string subcategoryIds = commissionStructureMasterVo.AssetSubCategory.ToString();
                    subcategoryIds = subcategoryIds.Replace("~", ",");
                    hdnSubcategoryIds.Value = subcategoryIds;
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
                FunctionInfo.Add("Method", "ReceivableSetup.ascx.cs:CollectStructureMastetrData()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return commissionStructureMasterVo;

        }

        protected void btnStructureSubmit_Click(object sender, EventArgs e)
        {
            int commissionStructureId = 0;
            commissionStructureMasterVo = CollectStructureMastetrData();

            commisionReceivableBo.CreateCommissionStructureMastter(commissionStructureMasterVo, userVo.UserId, 0, out commissionStructureId);
            hidCommissionStructureName.Value = commissionStructureId.ToString();
            CommissionStructureControlsEnable(false);
            tblCommissionStructureRule.Visible = true;
            tblCommissionStructureRule1.Visible = true;

            trPayableMapping.Visible = true;
            if (ddlProductType.SelectedValue == "MF")
            {
                pnlAddSchemes.Visible = true;
                Table2.Visible = true;
                BindCommissionStructureRuleBlankRow();
            }
            else if (ddlProductType.SelectedValue == "FI")
            {

                GetMapped_Unmapped_Issues("Mapped", "");
                GetUnamppedIssues(ddlIssueType.SelectedValue);
                Table4.Visible = true;
                tbNcdIssueList.Visible = true;
                BindBondCategories();
            }
            else if (ddlProductType.SelectedValue == "IP")
            {

                GetMapped_Unmapped_Issues("Mapped", "");
                GetUnamppedIssues(ddlIssueType.SelectedValue);
                Table4.Visible = true;
                tbNcdIssueList.Visible = true;
            }

            ShowAndHideVisible_FirstSection();



        }
        private void BindBondCategories()
        {
            DataTable dtCategory = new DataTable();
            dtCategory = commisionReceivableBo.BindNcdCategory("SubInstrumentCat", "").Tables[0];
            if (dtCategory.Rows.Count > 0)
            {
                ddlSubInstrCategory.DataSource = dtCategory;
                ddlSubInstrCategory.DataValueField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString().Trim();
                ddlSubInstrCategory.DataTextField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString().Trim();
                ddlSubInstrCategory.DataBind();
                ddlSubInstrCategory.Items[2].Enabled = false;
            }
            ddlSubInstrCategory.Items.Insert(0, new ListItem("Select", "0"));

        }

        private void MapPingLinksBasedOnCpmmissionTypes(string lookUpId)
        {
            if (lookUpId == "16019")
            {
                btnMapToscheme.Text = "Map Scheme";
                btnMapToscheme.Visible = true;
                ButtonAgentCodeMapping.Visible = false;
            }
            else
            {
                btnMapToscheme.Text = "Map Issue";
                btnMapToscheme.Visible = true;
                ButtonAgentCodeMapping.Visible = true;
            }
        }

        protected void btnMapToscheme_Click(object sender, EventArgs e)
        {
            if (ddlProductType.SelectedValue == "MF")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('CommissionStructureToSchemeMapping','ID=" + hidCommissionStructureName.Value + "&Product=" + ddlProductType.SelectedValue + "');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('CommisionManagementStructureToIssueMapping','ID=" + hidCommissionStructureName.Value + "&Product=" + ddlProductType.SelectedValue + "');", true);

            }

        }
        protected void ButtonAgentCodeMapping_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('PayableStructureToAgentCategoryMapping','ID=" + hidCommissionStructureName.Value + "&Product=" + ddlProductType.SelectedValue + "');", true);

        }




        protected void btnStructureUpdate_Click(object sender, EventArgs e)
        {
            commissionStructureMasterVo = CollectStructureMastetrData();
            commissionStructureMasterVo.CommissionStructureId = Convert.ToInt32(hidCommissionStructureName.Value);
            commisionReceivableBo.UpdateCommissionStructureMastter(commissionStructureMasterVo, userVo.UserId);
            CommissionStructureControlsEnable(false);
        }

        protected void ddlCommissionApplicableLevel_Selectedindexchanged(object sender, EventArgs e)
        {
        }
        protected void ddlTransaction_Selectedindexchanged(object sender, EventArgs e)
        {
            DropDownList ddlTransaction = (DropDownList)sender;
            if (ddlTransaction.NamingContainer is Telerik.Web.UI.GridEditFormItem)
            {
                GridEditFormItem editform = (GridEditFormItem)ddlTransaction.NamingContainer; ;
                System.Web.UI.HtmlControls.HtmlTableRow trMinMaxTenure = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trMinMaxTenure");
                Label lblSIPFrequency = (Label)editform.FindControl("lblSIPFrequency");
                CheckBoxList chkListTtansactionType = (CheckBoxList)editform.FindControl("chkListTtansactionType");
                DropDownList ddlCommissionApplicableLevel = (DropDownList)editform.FindControl("ddlCommissionApplicableLevel");
                chkListTtansactionType.Visible = true;
                if (ddlTransaction.SelectedValue == "SIP")
                {
                    trMinMaxTenure.Visible = true;

                    foreach (ListItem chkItems in chkListTtansactionType.Items)
                    {
                        if (chkItems.Value == "SIP" || chkItems.Value == "STB")
                        {
                            chkItems.Enabled = true;
                            chkItems.Selected = true;
                        }
                        else
                        {
                            chkItems.Enabled = false;
                            chkItems.Selected = false;
                        }
                    }
                }
                else if (ddlTransaction.SelectedValue == "NonSIP")
                {
                    trMinMaxTenure.Visible = false;

                    foreach (ListItem chkItems in chkListTtansactionType.Items)
                    {
                        if (chkItems.Value == "SIP" || chkItems.Value == "STB")
                        {
                            chkItems.Enabled = false;
                            chkItems.Selected = false;
                        }
                        else
                        {
                            chkItems.Enabled = true;
                            chkItems.Selected = true;
                        }
                    }
                }
                else if (ddlTransaction.SelectedValue == "All")
                {
                    trMinMaxTenure.Visible = false;

                    foreach (ListItem chkItems in chkListTtansactionType.Items)
                    {
                        chkItems.Selected = true;
                        chkItems.Enabled = false;
                    }
                }
            }
            else if (ddlTransaction.NamingContainer is Telerik.Web.UI.GridEditFormInsertItem)
            {
                GridEditFormInsertItem editform = (GridEditFormInsertItem)ddlTransaction.NamingContainer; ;
                System.Web.UI.HtmlControls.HtmlTableRow trMinMaxTenure = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trMinMaxTenure");
                Label lblSIPFrequency = (Label)editform.FindControl("lblSIPFrequency");
                CheckBoxList chkListTtansactionType = (CheckBoxList)editform.FindControl("chkListTtansactionType");
                if (ddlTransaction.SelectedValue == "SIP")
                {
                    trMinMaxTenure.Visible = true;
                    foreach (ListItem chkItems in chkListTtansactionType.Items)
                    {
                        if (chkItems.Value == "SIP" || chkItems.Value == "STB")
                            chkItems.Enabled = true;
                        else
                        {
                            chkItems.Enabled = false;
                            chkItems.Selected = false;
                        }
                    }

                }
                else if (ddlTransaction.SelectedValue == "NonSIP")
                {
                    trMinMaxTenure.Visible = false;
                    foreach (ListItem chkItems in chkListTtansactionType.Items)
                    {

                        chkItems.Selected = true;
                        chkItems.Enabled = true;
                    }
                }
            }
        }



        protected void ddlCommisionCalOn_Selectedindexchanged(object sender, EventArgs e)
        {
            DropDownList ddlCommisionCalOn = (DropDownList)sender;
            System.Web.UI.HtmlControls.HtmlTableRow trMinMAxInvAmount = new System.Web.UI.HtmlControls.HtmlTableRow();
            System.Web.UI.HtmlControls.HtmlTableRow trMinAndMaxNumberOfApplication = new System.Web.UI.HtmlControls.HtmlTableRow();
            DropDownList ddlCommissionType = new DropDownList();
            System.Web.UI.HtmlControls.HtmlTableCell tdlblApplicationNo = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell tdApplicationNo = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableRow trincentive = new System.Web.UI.HtmlControls.HtmlTableRow();
            if (ddlCommisionCalOn.NamingContainer is Telerik.Web.UI.GridEditFormItem)
            {
                GridEditFormItem gdi;

                gdi = (GridEditFormItem)ddlCommisionCalOn.NamingContainer;
                trMinMAxInvAmount = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinMAxInvAmount");
                trMinAndMaxNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinAndMaxNumberOfApplication");
                ddlCommissionType = (DropDownList)gdi.FindControl("ddlCommissionType");
                tdlblApplicationNo = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlblApplicationNo");
                tdApplicationNo = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdApplicationNo");
                TextBox txtMinInvestmentAmount = (TextBox)gdi.FindControl("txtMinInvestmentAmount");
                TextBox txtMaxInvestmentAmount = (TextBox)gdi.FindControl("txtMaxInvestmentAmount");
                TextBox txtMinNumberOfApplication = (TextBox)gdi.FindControl("txtMinNumberOfApplication");
                TextBox txtMaxNumberOfApplication = (TextBox)gdi.FindControl("txtMaxNumberOfApplication");
                txtMinInvestmentAmount.Text = string.Empty;
                txtMaxInvestmentAmount.Text = string.Empty;
                txtMinNumberOfApplication.Text = string.Empty;
                txtMaxNumberOfApplication.Text = string.Empty;

            }
            else if (ddlCommisionCalOn.NamingContainer is Telerik.Web.UI.GridEditFormInsertItem)
            {
                GridEditFormInsertItem gdi;
                gdi = (GridEditFormInsertItem)ddlCommisionCalOn.NamingContainer;
                trMinMAxInvAmount = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinMAxInvAmount");
                trMinAndMaxNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinAndMaxNumberOfApplication");
                ddlCommissionType = (DropDownList)gdi.FindControl("ddlCommissionType");
                tdlblApplicationNo = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlblApplicationNo");
                tdApplicationNo = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdApplicationNo");
                TextBox txtMinInvestmentAmount = (TextBox)RadGridStructureRule.FindControl("txtMinInvestmentAmount");
                TextBox txtMaxInvestmentAmount = (TextBox)RadGridStructureRule.FindControl("txtMaxInvestmentAmount");
                TextBox txtMinNumberOfApplication = (TextBox)RadGridStructureRule.FindControl("txtMinNumberOfApplication");
                TextBox txtMaxNumberOfApplication = (TextBox)RadGridStructureRule.FindControl("txtMaxNumberOfApplication");
                txtMinInvestmentAmount.Text = string.Empty;
                txtMaxInvestmentAmount.Text = string.Empty;
                txtMinNumberOfApplication.Text = string.Empty;
                txtMaxNumberOfApplication.Text = string.Empty;

            }
            trMinMAxInvAmount.Visible = false;
            trMinAndMaxNumberOfApplication.Visible = false;
            tdlblApplicationNo.Visible = false;
            tdApplicationNo.Visible = false;
            trincentive.Visible = false;

            if (ddlCommisionCalOn.SelectedValue.ToString().ToUpper() == "APPC")
            {
                trMinAndMaxNumberOfApplication.Visible = true;
            }
            else if (ddlCommisionCalOn.SelectedValue.ToString().ToUpper() == "INAM")
            {
                trMinMAxInvAmount.Visible = true;
            }
            else if (ddlCommisionCalOn.SelectedValue.ToString().ToUpper() == "APPNO")
            {
                tdlblApplicationNo.Visible = true;
                tdApplicationNo.Visible = true;
            }

        }
        protected void ddlCommissionType_Selectedindexchanged(object sender, EventArgs e)
        {


            DropDownList ddlCommissionType = (DropDownList)sender;
            Label lblReceivableFrequency = new Label();
            DropDownList ddlReceivableFrequency = new DropDownList();
            System.Web.UI.HtmlControls.HtmlTableRow trTransactionTypeSipFreq = new System.Web.UI.HtmlControls.HtmlTableRow();
            System.Web.UI.HtmlControls.HtmlTableRow trMinMaxTenure = new System.Web.UI.HtmlControls.HtmlTableRow();
            System.Web.UI.HtmlControls.HtmlTableRow trMinMaxAge = new System.Web.UI.HtmlControls.HtmlTableRow();
            System.Web.UI.HtmlControls.HtmlTableCell tdlb1MinNumberOfApplication = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell tdtxtMinNumberOfApplication = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell tdlb1MaxNumberOfApplication = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell tdtxtMaxNumberOfApplication = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableRow trMinAndMaxNumberOfApplication = new System.Web.UI.HtmlControls.HtmlTableRow();
            System.Web.UI.HtmlControls.HtmlTableCell tdlb1SipFreq = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell tdddlSipFreq = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableRow trMinMAxInvAmount = new System.Web.UI.HtmlControls.HtmlTableRow();
            System.Web.UI.HtmlControls.HtmlTableCell tdApplicationNo = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell tdlblApplicationNo = new System.Web.UI.HtmlControls.HtmlTableCell();
            System.Web.UI.HtmlControls.HtmlTableCell tdlblClock = new System.Web.UI.HtmlControls.HtmlTableCell();


            DropDownList ddlIncentiveType = new DropDownList();
            Label lblTransactionType = new Label();
            CheckBoxList chkListTtansactionType = new CheckBoxList();
            Label lblMinNumberOfApplication = new Label();
            Label lblMaxNumberOfApplication = new Label();
            Label lblSIPFrequency = new Label();
            DropDownList ddlSIPFrequency = new DropDownList();
            DropDownList ddlTransaction = new DropDownList();
            DropDownList ddlCommisionCalOn = new DropDownList();
            TextBox txtMinNumberOfApplication = new TextBox();
            TextBox txtMaxNumberOfApplication = new TextBox();
            DropDownList ddlCommissionApplicableLevel = new DropDownList();
            System.Web.UI.HtmlControls.HtmlTableRow trincentive = new System.Web.UI.HtmlControls.HtmlTableRow();
            CheckBox check = new CheckBox();
            Label lbl = new Label();
            TextBox txtbox = new TextBox();
            TextBox txtMaxInvestmentAmount = new TextBox();
            TextBox txtMinInvestmentAmount = new TextBox();

            if (ddlCommissionType.NamingContainer is Telerik.Web.UI.GridEditFormItem)
            {
                GridEditFormItem gdi;

                gdi = (GridEditFormItem)ddlCommissionType.NamingContainer;
                lblTransactionType = (Label)gdi.FindControl("lblTransactionType");
                check = (CheckBox)gdi.FindControl("chkCloBack");
                tdlblClock = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlblClock");
                chkListTtansactionType = (CheckBoxList)gdi.FindControl("chkListTtansactionType");
                lblMinNumberOfApplication = (Label)gdi.FindControl("lblMinNumberOfApplication");
                lblMaxNumberOfApplication = (Label)gdi.FindControl("lblMaxNumberOfApplication");
                lblSIPFrequency = (Label)gdi.FindControl("lblSIPFrequency");
                txtMinNumberOfApplication = (TextBox)gdi.FindControl("txtMinNumberOfApplication");
                txtMaxNumberOfApplication = (TextBox)gdi.FindControl("txtMaxNumberOfApplication");
                ddlSIPFrequency = (DropDownList)gdi.FindControl("ddlSIPFrequency");
                ddlTransaction = (DropDownList)gdi.FindControl("ddlTransaction");
                ddlCommisionCalOn = (DropDownList)gdi.FindControl("ddlCommisionCalOn");
                ddlCommissionApplicableLevel = (DropDownList)gdi.FindControl("ddlCommissionApplicableLevel");
                lblReceivableFrequency = (Label)gdi.FindControl("lblReceivableFrequency");
                ddlReceivableFrequency = (DropDownList)gdi.FindControl("ddlReceivableFrequency");
                trTransactionTypeSipFreq = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trTransactionTypeSipFreq");
                trMinMaxTenure = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinMaxTenure");
                trMinMaxAge = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinMaxAge");
                tdlb1MinNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlb1MinNumberOfApplication");
                tdtxtMinNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdtxtMinNumberOfApplication");
                tdlb1MaxNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlb1MaxNumberOfApplication");
                tdtxtMaxNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdtxtMaxNumberOfApplication");
                trMinAndMaxNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinAndMaxNumberOfApplication");
                trMinMAxInvAmount = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinMAxInvAmount"); ;
                tdlb1SipFreq = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlb1SipFreq");
                tdddlSipFreq = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdddlSipFreq");
                tdlblApplicationNo = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlblApplicationNo");
                tdApplicationNo = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdApplicationNo");
                trincentive = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trincentive");
                ddlIncentiveType = (DropDownList)gdi.FindControl("ddlIncentiveType");
                ddlCommissionType = (DropDownList)gdi.FindControl("ddlCommissionType");
            }
            else if (ddlCommissionType.NamingContainer is Telerik.Web.UI.GridEditFormInsertItem)
            {
                GridEditFormInsertItem gdi;
                gdi = (GridEditFormInsertItem)ddlCommissionType.NamingContainer;
                lblTransactionType = (Label)gdi.FindControl("lblTransactionType");
                check = (CheckBox)gdi.FindControl("chkCloBack");
                tdlblClock = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlblClock");

                chkListTtansactionType = (CheckBoxList)gdi.FindControl("chkListTtansactionType");
                lblMinNumberOfApplication = (Label)gdi.FindControl("lblMinNumberOfApplication");
                lblMaxNumberOfApplication = (Label)gdi.FindControl("lblMaxNumberOfApplication");
                lblSIPFrequency = (Label)gdi.FindControl("lblSIPFrequency");
                txtMinNumberOfApplication = (TextBox)gdi.FindControl("txtMinNumberOfApplication");
                txtMaxNumberOfApplication = (TextBox)gdi.FindControl("txtMaxNumberOfApplication");
                ddlTransaction = (DropDownList)gdi.FindControl("ddlTransaction");
                ddlCommisionCalOn = (DropDownList)gdi.FindControl("ddlCommisionCalOn");
                ddlCommissionApplicableLevel = (DropDownList)gdi.FindControl("ddlCommissionApplicableLevel");
                ddlSIPFrequency = (DropDownList)gdi.FindControl("ddlSIPFrequency");
                lblReceivableFrequency = (Label)gdi.FindControl("lblReceivableFrequency");
                ddlReceivableFrequency = (DropDownList)gdi.FindControl("ddlReceivableFrequency");
                trTransactionTypeSipFreq = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trTransactionTypeSipFreq");
                trMinMaxTenure = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinMaxTenure");
                trMinMaxAge = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinMaxAge");
                tdlb1MinNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlb1MinNumberOfApplication");
                tdtxtMinNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdtxtMinNumberOfApplication");
                tdlb1MaxNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlb1MaxNumberOfApplication");
                tdtxtMaxNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdtxtMaxNumberOfApplication");
                trMinAndMaxNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinAndMaxNumberOfApplication");
                trMinMAxInvAmount = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinMAxInvAmount");
                tdlb1SipFreq = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlb1SipFreq");
                tdddlSipFreq = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdddlSipFreq");
                tdlblApplicationNo = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlblApplicationNo");
                tdApplicationNo = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdApplicationNo");
                trincentive = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trincentive");
                ddlIncentiveType = (DropDownList)gdi.FindControl("ddlIncentiveType");
                ddlCommissionType = (DropDownList)gdi.FindControl("ddlCommissionType");

            }
            ShowAndHideSTructureRuleControlsBasedOnProductAndCommisionType(lblReceivableFrequency, ddlReceivableFrequency, trTransactionTypeSipFreq, tdlb1SipFreq, tdddlSipFreq, trMinMaxTenure, trMinMaxAge, tdlb1MinNumberOfApplication, tdtxtMinNumberOfApplication, ddlProductType.SelectedValue, ddlCommissionType.SelectedValue
                 , lblMinNumberOfApplication, txtMinNumberOfApplication, lblSIPFrequency, ddlSIPFrequency, ddlTransaction, chkListTtansactionType, lblTransactionType, ddlCommisionCalOn, ddlCommissionApplicableLevel,
                 lblMaxNumberOfApplication, tdlb1MaxNumberOfApplication, tdtxtMaxNumberOfApplication, txtMaxNumberOfApplication, trMinAndMaxNumberOfApplication, trMinMAxInvAmount, tdlblApplicationNo, tdApplicationNo, trincentive, ddlIncentiveType, ddlCommissionType, check, tdlblClock);
        }

        protected void RadGridStructureRule_ItemDataBound(object sender, GridItemEventArgs e)
        {
            int seriseid = 0, categoryss = 0;
            ShowHideRadGridStructureRulecolumns();
            if (e.Item is GridDataItem)
            {
                DropDownList ddlBrokrageUnit = (DropDownList)e.Item.FindControl("ddlBrokrageUnit");
                Label lblTransactionType = (Label)e.Item.FindControl("lblTransactionType");
                TextBox txtReceivableValue = (TextBox)e.Item.FindControl("txtReceivableValue");
                TextBox txtMaxInvestmentAmount = (TextBox)e.Item.FindControl("txtMaxInvestmentAmount");
                TextBox txtMinInvestmentAmount = (TextBox)e.Item.FindControl("txtMinInvestmentAmount");

                TextBox txtPaybleValue = (TextBox)e.Item.FindControl("txtPaybleValue");
                TextBox txtServiceTex = (TextBox)e.Item.FindControl("txtServiceTex");
                TextBox txtTDSTex = (TextBox)e.Item.FindControl("txtTDSTex");
                Button btnupdate = (Button)e.Item.FindControl("btnupdate");
                CheckBox check = (CheckBox)e.Item.FindControl("ChkIsclowBack");
                TextBox txtbox = (TextBox)e.Item.FindControl("txtClawBackAge");
                string transtype = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WCT_CommissionTypeCode"].ToString();
                if (ddlProductType.SelectedValue == "MF")
                {
                    RadGridStructureRule.MasterTableView.GetColumn("ACSR_MinNumberOfApplications").Visible = false;
                    RadGridStructureRule.MasterTableView.GetColumn("ACSR_MaxNumberOfApplications").Visible = false;
                    RadGridStructureRule.MasterTableView.GetColumn("CO_ApplicationNo").Visible = false;
                    if (ViewState["gridEdit"] != null)
                    {
                        if (ViewState["gridEdit"].ToString() != "1")
                        {
                            RadGridStructureRule.MasterTableView.GetColumn("Edit1").Visible = false;
                            RadGridStructureRule.MasterTableView.GetColumn("Edit").Visible = true;
                            RadGridStructureRule.MasterTableView.GetColumn("Update").Visible = false;
                            RadGridStructureRule.MasterTableView.GetColumn("Delete").Visible = true;
                            txtReceivableValue.Enabled = false;
                            txtPaybleValue.Enabled = false;
                            txtServiceTex.Enabled = false;
                            txtTDSTex.Enabled = false;
                            btnupdate.Enabled = false;
                            ddlBrokrageUnit.Enabled = false;
                            check.Enabled = false;
                            txtbox.Enabled = false;
                            txtMaxInvestmentAmount.Enabled = false;
                            txtMinInvestmentAmount.Enabled = false;

                        }
                    }
                }
                if (ddlProductType.SelectedValue == "MF" && transtype != "UF")
                {
                    check.Visible = false;
                    txtbox.Visible = false;
                }
                if (ViewState["ruleid"] != null)
                {
                    foreach (var item in ViewState["ruleid"].ToString().TrimEnd(',').Split(','))
                    {
                        int ruleid = int.Parse(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_CommissionStructureRuleId"].ToString());

                        if (ruleid == int.Parse(item))
                        {
                            GridDataItem dataBoundItem = e.Item as GridDataItem;
                            dataBoundItem["ACSR_CommissionStructureRuleName"].BackColor = System.Drawing.Color.Gray;
                            dataBoundItem["ACSR_CommissionStructureRuleName"].Font.Bold = true;
                        }

                    }
                }
                string transactiontype = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_TransactionType"].ToString();
                if (transactiontype.Contains("ABY,BUY"))
                {
                    lblTransactionType.Text = "Normal";
                }
                string brokrageUnit = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WCU_UnitCode1"].ToString();
                ddlBrokrageUnit.SelectedValue = brokrageUnit;
            }
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlCommisionCalOn = (DropDownList)gefi.FindControl("ddlCommisionCalOn");
                System.Web.UI.HtmlControls.HtmlTableCell tdApplicationNo = (System.Web.UI.HtmlControls.HtmlTableCell)gefi.FindControl("tdApplicationNo");
                System.Web.UI.HtmlControls.HtmlTableCell tdlblApplicationNo = (System.Web.UI.HtmlControls.HtmlTableCell)gefi.FindControl("tdlblApplicationNo");
                System.Web.UI.HtmlControls.HtmlTableRow trMinMAxInvAmount = (System.Web.UI.HtmlControls.HtmlTableRow)gefi.FindControl("trMinMAxInvAmount");
                TextBox txtRuleValidityTo = (TextBox)e.Item.FindControl("txtRuleValidityTo");
                TextBox txtRuleValidityFrom = (TextBox)e.Item.FindControl("txtRuleValidityFrom");
                CheckBox chkCategory = (CheckBox)e.Item.FindControl("chkCategory");
                CheckBox chkSeries = (CheckBox)e.Item.FindControl("chkSeries");
                CheckBox chkMode = (CheckBox)e.Item.FindControl("chkMode");
                CheckBox check = (CheckBox)e.Item.FindControl("ChkIsclowBack");
                TextBox txtbox = (TextBox)e.Item.FindControl("txtClawBackAge");
                System.Web.UI.HtmlControls.HtmlTableCell tdddlSeries = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdddlSeries");
                System.Web.UI.HtmlControls.HtmlTableCell tdlblSerise = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdlblSerise");
                System.Web.UI.HtmlControls.HtmlTableCell tdlblMode = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdlblMode");
                System.Web.UI.HtmlControls.HtmlTableCell tdddlMode = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdddlMode");
                System.Web.UI.HtmlControls.HtmlTableRow trCity = (System.Web.UI.HtmlControls.HtmlTableRow)gefi.FindControl("trCity");
                System.Web.UI.HtmlControls.HtmlTableRow trDateValidation = (System.Web.UI.HtmlControls.HtmlTableRow)gefi.FindControl("trDateValidation");
                txtRuleValidityTo.Text = hdnRuleEnd.Value;
                txtRuleValidityFrom.Text = hdnRulestart.Value;
                ddlCommisionCalOn.SelectedValue = "INAM";
                trMinMAxInvAmount.Visible = true;
                if (ddlSubInstrCategory.SelectedValue == "FICGCG")
                {
                    chkCategory.Visible = false;
                    trDateValidation.Visible = false;

                }
                if (ddlSubInstrCategory.SelectedValue == "FISDSD")
                {
                    chkMode.Visible = false;
                    tdddlMode.Visible = false;
                    tdlblMode.Visible = false;
                    trDateValidation.Visible = false;

                }
                if (ddlProductType.SelectedValue == "IP")
                {
                    chkSeries.Visible = false;
                    tdddlSeries.Visible = false;
                    tdlblSerise.Visible = false;
                    chkCategory.Visible = true;
                    trDateValidation.Visible = false;
                }
                if (ddlProductType.SelectedValue == "MF")
                {
                    trCity.Visible = true;
                    RadGridStructureRule.MasterTableView.GetColumn("Edit").Visible = true;
                    RadGridStructureRule.MasterTableView.GetColumn("Edit1").Visible = false;
                    RadGridStructureRule.MasterTableView.GetColumn("Delete").Visible = false;

                }
            }
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode) && e.Item.ItemIndex > 0)
            {
                HiddenField1.Value = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_CommissionStructureRuleId"].ToString();
                RadGrid rgCommissionTypeCaliculation = (RadGrid)e.Item.FindControl("rgCommissionTypeCaliculation");
                System.Web.UI.HtmlControls.HtmlTableRow trRuleDetailSection = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trRuleDetailSection");
                rgCommissionTypeCaliculation.Visible = true;
                trRuleDetailSection.Visible = true;
                DataSet dsLookupData;
                if (string.IsNullOrEmpty(HiddenField1.Value))
                    HiddenField1.Value = "0";
                dsLookupData = commisionReceivableBo.GetCommissionTypeBrokerage(Convert.ToInt32(HiddenField1.Value));
                rgCommissionTypeCaliculation.DataSource = dsLookupData;
                rgCommissionTypeCaliculation.DataBind();

            }
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {

                if (e.Item.ItemIndex != -1)
                {
                    hdnIsSpecialIncentive.Value = "";
                    HiddenField1.Value = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_CommissionStructureRuleId"].ToString();
                    hdnIsSpecialIncentive.Value = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ASCR_WCMV_IncentiveType"].ToString();
                    RadGrid rgCommissionTypeCaliculation = (RadGrid)e.Item.FindControl("rgCommissionTypeCaliculation");
                    System.Web.UI.HtmlControls.HtmlTableRow trRuleDetailSection = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trRuleDetailSection");
                    hdnRuleName.Value = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_CommissionStructureRuleName"].ToString();
                    rgCommissionTypeCaliculation.Visible = true;
                    trRuleDetailSection.Visible = true;
                    DataSet dsLookupData;
                    if (string.IsNullOrEmpty(HiddenField1.Value))
                        HiddenField1.Value = "0";
                    dsLookupData = commisionReceivableBo.GetCommissionTypeBrokerage(Convert.ToInt32(HiddenField1.Value));
                    rgCommissionTypeCaliculation.DataSource = dsLookupData;
                    rgCommissionTypeCaliculation.DataBind();
                } //
                else
                {
                    if (string.IsNullOrEmpty(HiddenField1.Value))
                    {
                        HiddenField1.Value = "0";
                    }
                    RadGrid rgCommissionTypeCaliculation = (RadGrid)e.Item.FindControl("rgCommissionTypeCaliculation");
                    BindRuleDetGrid(rgCommissionTypeCaliculation, Convert.ToInt32(HiddenField1.Value));

                }
                if (ddlProductType.SelectedValue == "MF")
                {
                    ShowHideControlsForRulesBasedOnProduct(true, e);
                    RadGridStructureRule.MasterTableView.GetColumn("Edit").Visible = true;
                    RadGridStructureRule.MasterTableView.GetColumn("Delete").Visible = true;
                    RadGridStructureRule.MasterTableView.GetColumn("Edit1").Visible = false;
                }
                else
                    ShowHideControlsForRulesBasedOnProduct(false, e);

                DataSet dsCommissionLookup;
                dsCommissionLookup = (DataSet)Session["CommissionLookUpData"];
                GridEditFormItem editform = (GridEditFormItem)e.Item;
                DropDownList ddlCommissionType = (DropDownList)editform.FindControl("ddlCommissionType");
                DropDownList ddlInvestorType = (DropDownList)editform.FindControl("ddlInvestorType");
                Label lblMinNumberOfApplication = (Label)editform.FindControl("lblMinNumberOfApplication");
                Label lblMaxNumberOfApplication = (Label)editform.FindControl("lblMaxNumberOfApplication");
                Label lblSIPFrequency = (Label)editform.FindControl("lblSIPFrequency");
                Label lblTransactionType = (Label)editform.FindControl("lblTransactionType");
                CheckBoxList chkListTtansactionType = (CheckBoxList)editform.FindControl("chkListTtansactionType");
                TextBox txtMinNumberOfApplication = (TextBox)editform.FindControl("txtMinNumberOfApplication");
                TextBox txtMaxNumberOfApplication = (TextBox)editform.FindControl("txtMaxNumberOfApplication");
                TextBox TxtRuleName = (TextBox)editform.FindControl("TxtRuleName");
                DropDownList ddlTenureFrequency = (DropDownList)editform.FindControl("ddlTenureFrequency");
                DropDownList ddlInvestAgeTenure = (DropDownList)editform.FindControl("ddlInvestAgeTenure");
                DropDownList ddlBrokerageUnit = (DropDownList)editform.FindControl("ddlBrokerageUnit");
                DropDownList ddlCommisionCalOn = (DropDownList)editform.FindControl("ddlCommisionCalOn");
                DropDownList ddlSIPFrequency = (DropDownList)editform.FindControl("ddlSIPFrequency");
                DropDownList ddlAppCityGroup = (DropDownList)e.Item.FindControl("ddlAppCityGroup");
                DropDownList ddlTransaction = (DropDownList)e.Item.FindControl("ddlTransaction");
                DropDownList ddlReceivableFrequency = (DropDownList)e.Item.FindControl("ddlReceivableFrequency");
                DropDownList ddlCommissionApplicableLevel = (DropDownList)e.Item.FindControl("ddlCommissionApplicableLevel");
                CheckBoxList chkListApplyTax = (CheckBoxList)editform.FindControl("chkListApplyTax");
                TextBox txtTaxValue = (TextBox)editform.FindControl("txtTaxValue");
                TextBox txtTDS = (TextBox)editform.FindControl("txtTDS");
                DropDownList ddlIncentiveType = (DropDownList)editform.FindControl("ddlIncentiveType");
                Label lblReceivableFrequency = (Label)editform.FindControl("lblReceivableFrequency");
                System.Web.UI.HtmlControls.HtmlTableRow trTransactionTypeSipFreq = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trTransactionTypeSipFreq");
                System.Web.UI.HtmlControls.HtmlTableRow trMinMaxTenure = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trMinMaxTenure");
                System.Web.UI.HtmlControls.HtmlTableRow trMinMaxAge = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trMinMaxAge");
                System.Web.UI.HtmlControls.HtmlTableCell tdMinNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdlb1MinNumberOfApplication");
                System.Web.UI.HtmlControls.HtmlTableCell tdlb1MaxNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdlb1MaxNumberOfApplication");
                System.Web.UI.HtmlControls.HtmlTableCell tdlb1SipFreq = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdlb1SipFreq");
                System.Web.UI.HtmlControls.HtmlTableCell tdddlSipFreq = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdddlSipFreq");
                System.Web.UI.HtmlControls.HtmlTableCell tdtxtMinNumberOfApplication1 = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdtxtMinNumberOfApplication");
                System.Web.UI.HtmlControls.HtmlTableCell tdtxtMaxNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdtxtMaxNumberOfApplication");
                System.Web.UI.HtmlControls.HtmlTableRow trMinAndMaxNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trMinAndMaxNumberOfApplication");
                System.Web.UI.HtmlControls.HtmlTableRow trMinMAxInvAmount = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trMinMAxInvAmount");
                System.Web.UI.HtmlControls.HtmlTableCell tdlblApplicationNo = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdlblApplicationNo");
                System.Web.UI.HtmlControls.HtmlTableCell tdApplicationNo = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdApplicationNo");
                System.Web.UI.HtmlControls.HtmlTableRow trincentive = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trincentive");
                System.Web.UI.HtmlControls.HtmlTableCell tdddlSeries = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdddlSeries");
                System.Web.UI.HtmlControls.HtmlTableCell tdlblSerise = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdlblSerise");
                System.Web.UI.HtmlControls.HtmlTableCell tdlblMode = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdlblMode");
                System.Web.UI.HtmlControls.HtmlTableCell tdddlMode = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdddlMode");
                System.Web.UI.HtmlControls.HtmlTableCell tdlblCategory = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdlblCategory");
                System.Web.UI.HtmlControls.HtmlTableCell tdddlCategorys = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdddlCategorys");
                DropDownList ddlSeries = (DropDownList)editform.FindControl("ddlSeries");
                DropDownList ddlCategorys = (DropDownList)editform.FindControl("ddlCategorys");
                DropDownList ddlMode = (DropDownList)editform.FindControl("ddlMode");
                CheckBox chkCategory = (CheckBox)editform.FindControl("chkCategory");
                CheckBox chkSeries = (CheckBox)editform.FindControl("chkSeries");
                CheckBox chkMode = (CheckBox)editform.FindControl("chkMode");
                CheckBox chkEForm = (CheckBox)editform.FindControl("chkEForm");
                Label lblApplyTaxes = (Label)editform.FindControl("lblApplyTaxes");
                CheckBox chkCloBack = (CheckBox)editform.FindControl("chkCloBack");
                TextBox txtClawBackAge = (TextBox)e.Item.FindControl("txtAge");
                TextBox txtSBCValue = (TextBox)e.Item.FindControl("txtSBCValue");
                TextBox txtKKCValue = (TextBox)e.Item.FindControl("txtKKCValue");
                System.Web.UI.HtmlControls.HtmlTableRow trDateValidation = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trDateValidation");
                System.Web.UI.HtmlControls.HtmlTableRow trCity = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trCity");
                System.Web.UI.HtmlControls.HtmlTableCell tdlblClock = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdlblClock");

                if (ddlProductType.SelectedValue == "MF")
                {
                    trDateValidation.Visible = false;
                    trCity.Visible = true;
                    RadGridStructureRule.MasterTableView.GetColumn("Edit").Visible = true;
                    RadGridStructureRule.MasterTableView.GetColumn("Delete").Visible = true;
                    RadGridStructureRule.MasterTableView.GetColumn("Edit1").Visible = false;

                }
                if (ddlProductType.SelectedValue != "MF")
                {
                    BindSeries(ddlSeries, int.Parse(gvMappedIssueList.MasterTableView.DataKeyValues[0]["AIM_IssueId"].ToString()), 0);
                    BindCategory(ddlCategorys, int.Parse(gvMappedIssueList.MasterTableView.DataKeyValues[0]["AIM_IssueId"].ToString()));

                }

                if (dsCommissionLookup != null)
                {


                    ddlCommissionApplicableLevel.DataSource = dsCommissionLookup.Tables[1];
                    ddlCommissionApplicableLevel.DataValueField = dsCommissionLookup.Tables[1].Columns["WCAL_ApplicableLEvelCode"].ToString();
                    ddlCommissionApplicableLevel.DataTextField = dsCommissionLookup.Tables[1].Columns["WCAL_ApplicableLEvel"].ToString();
                    ddlCommissionApplicableLevel.DataBind();
                    ddlCommissionApplicableLevel.SelectedValue = "TR";

                    ddlAppCityGroup.DataSource = dsCommissionLookup.Tables[7];
                    ddlAppCityGroup.DataValueField = dsCommissionLookup.Tables[7].Columns["ACG_CityGroupID"].ToString();
                    ddlAppCityGroup.DataTextField = dsCommissionLookup.Tables[7].Columns["ACG_CityGroupName"].ToString();
                    ddlAppCityGroup.DataBind();
                    ddlAppCityGroup.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));


                    ddlReceivableFrequency.DataSource = dsCommissionLookup.Tables[5];
                    ddlReceivableFrequency.DataValueField = dsCommissionLookup.Tables[5].Columns["XF_FrequencyCode"].ToString();
                    ddlReceivableFrequency.DataTextField = dsCommissionLookup.Tables[5].Columns["XF_Frequency"].ToString();
                    ddlReceivableFrequency.DataBind();
                    ddlReceivableFrequency.SelectedValue = "MN";


                    ddlCommissionType.DataSource = dsCommissionLookup.Tables[2];
                    ddlCommissionType.DataValueField = dsCommissionLookup.Tables[2].Columns["WCT_CommissionTypeCode"].ToString();
                    ddlCommissionType.DataTextField = dsCommissionLookup.Tables[2].Columns["WCT_CommissionType"].ToString();
                    ddlCommissionType.DataBind();
                    ddlCommissionType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                    if (ddlSubInstrCategory.SelectedValue == "FICGCG")
                    {
                        if (ddlCommissionType.Items.FindByValue("TC") != null)
                        {
                            ddlCommissionType.Items.FindByValue("TC").Enabled = false;
                        }
                    }



                    ddlCommisionCalOn.DataSource = dsCommissionLookup.Tables[4];
                    ddlCommisionCalOn.DataValueField = dsCommissionLookup.Tables[4].Columns["WCCO_Calculatedoncode"].ToString();
                    ddlCommisionCalOn.DataTextField = dsCommissionLookup.Tables[4].Columns["WCCO_CalculatedOn"].ToString();
                    ddlCommisionCalOn.DataBind();
                    ddlCommisionCalOn.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                    ddlInvestorType.DataSource = dsCommissionLookup.Tables[9];
                    ddlInvestorType.DataValueField = dsCommissionLookup.Tables[9].Columns["XCC_CustomerCategoryCode"].ToString();
                    ddlInvestorType.DataTextField = dsCommissionLookup.Tables[9].Columns["XCC_CustomerCategory"].ToString();
                    ddlInvestorType.DataBind();

                    ddlSIPFrequency.DataSource = dsCommissionLookup.Tables[5];
                    ddlSIPFrequency.DataValueField = dsCommissionLookup.Tables[5].Columns["XF_FrequencyCode"].ToString();
                    ddlSIPFrequency.DataTextField = dsCommissionLookup.Tables[5].Columns["XF_Frequency"].ToString();
                    ddlSIPFrequency.DataBind();
                    ddlSIPFrequency.SelectedValue = "MN";

                    chkListTtansactionType.DataSource = dsCommissionLookup.Tables[10];
                    chkListTtansactionType.DataValueField = dsCommissionLookup.Tables[10].Columns["WMTT_TransactionClassificationCode"].ToString();
                    chkListTtansactionType.DataTextField = dsCommissionLookup.Tables[10].Columns["WMTT_TransactionClassificationName"].ToString();
                    chkListTtansactionType.DataBind();

                }

                if (e.Item.RowIndex != -1)
                {


                    string strCommissionType = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WCT_CommissionTypeCode"].ToString();
                    string strCustomerCategory = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XCT_CustomerTypeCode"].ToString();
                    string strTenureUnit = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_TenureUnit"].ToString();
                    string strInvestmentTransactionType = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_TransactionType"].ToString();
                    string strSIPFrequency = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_SIPFrequency"].ToString();
                    string strBrokargeUnit = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WCU_UnitCode"].ToString();
                    string strCalculatedOn = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WCCO_CalculatedOnCode"].ToString();
                    string strAUMFrequency = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSM_AUMFrequency"].ToString();

                    string strCityGroupID = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACG_CityGroupID"].ToString();
                    string strReceivableRuleFrequency = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_ReceivableRuleFrequency"].ToString();
                    string strApplicableLevelCode = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WCAL_ApplicableLevelCode"].ToString();
                    string strIsServiceTaxReduced = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_IsServiceTaxReduced"].ToString();
                    string strIsTDSReduced = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_IsTDSReduced"].ToString();
                    string strIsOtherTaxReduced = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSM_IsOtherTaxReduced"].ToString();
                    string IncentiveType = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ASCR_WCMV_IncentiveType"].ToString();
                    string incentiveAge = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_InvestmentAgeUnit"].ToString();
                    bool IsClaWback = false, IsServiceTaxInclusive = false, IsSBCApplicable = false, IsKKCApplicable = false;

                    if (!string.IsNullOrEmpty(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_IsClaWback"].ToString()))
                        IsClaWback = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_IsClaWback"].ToString() == "1" ? true : false;
                    string ClawBackAge = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_ClawBackAge"].ToString();
                    if (IsClaWback)
                        txtClawBackAge.Visible = true;
                    else
                        txtClawBackAge.Visible = false;
                    if (RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AID_IssueDetailId"].ToString() != string.Empty)
                    {
                        seriseid = int.Parse(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AID_IssueDetailId"].ToString());
                    }
                    if (RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString() != string.Empty)
                    {
                        categoryss = int.Parse(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString());
                    }
                    if (!string.IsNullOrEmpty(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_IsServiceTaxInclusive"].ToString()))
                        IsServiceTaxInclusive =Convert.ToBoolean(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_IsServiceTaxInclusive"].ToString());
                    if (!string.IsNullOrEmpty(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_IsSBCApplicable"].ToString()))
                        IsSBCApplicable = Convert.ToBoolean(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_IsSBCApplicable"].ToString());
                    if (!string.IsNullOrEmpty(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_IsKKCApplicable"].ToString()))
                        IsKKCApplicable = Convert.ToBoolean(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_IsKKCApplicable"].ToString());
                    string SBCValue = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_SBCValue"].ToString();
                    string KKCValue = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_KKCValue"].ToString();
                    string mode = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_Mode"].ToString();
                    ddlIncentiveType.SelectedValue = IncentiveType;
                    if (mode != "")
                    {
                        ddlMode.SelectedValue = mode;
                        chkMode.Checked = false;
                        tdlblMode.Visible = false;
                        tdddlMode.Visible = false;
                    }
                    if (seriseid != 0)
                    {
                        tdddlSeries.Visible = true;
                        chkSeries.Checked = true;
                        tdlblSerise.Visible = true;
                        ddlSeries.SelectedValue = seriseid.ToString();
                    }
                    if (categoryss != 0)
                    {
                        tdlblCategory.Visible = true;
                        tdddlCategorys.Visible = true;
                        chkCategory.Checked = true;
                        ddlCategorys.SelectedValue = categoryss.ToString();
                    }
                    ddlAppCityGroup.SelectedValue = strCityGroupID;
                    ddlReceivableFrequency.SelectedValue = strReceivableRuleFrequency;
                    ddlCommissionApplicableLevel.SelectedValue = "TR";
                    chkCloBack.Checked = IsClaWback;
                    txtClawBackAge.Text = ClawBackAge;
                    foreach (ListItem chkItems in chkListApplyTax.Items)
                    {

                        if (chkItems.Value == "ServiceTax" & strIsServiceTaxReduced == "1")
                        {
                            chkItems.Selected = true;
                            txtTaxValue.Visible = true;
                            lblApplyTaxes.Visible = true;
                        }
                        else if (chkItems.Value == "TDS" & strIsTDSReduced == "1")
                        {
                            chkItems.Selected = true;
                            txtTDS.Visible = true;
                            lblApplyTaxes.Visible = true;

                        }
                        else if (chkItems.Value == "SBC" & IsSBCApplicable == true)
                        {
                            chkItems.Selected = true;
                            txtSBCValue.Visible = true;
                            lblApplyTaxes.Visible = true;
                        }
                        else if (chkItems.Value == "KKC" & IsKKCApplicable==true)
                        {
                            chkItems.Selected = true;
                            txtKKCValue.Visible = true;
                            lblApplyTaxes.Visible = true;

                        }
                        else if (chkItems.Value == "TI" & IsServiceTaxInclusive==true)
                        {
                            chkItems.Selected = true;
                        }


                    }



                    ddlCommissionType.SelectedValue = strCommissionType;
                    ShowAndHideSTructureRuleControlsBasedOnProductAndCommisionType(lblReceivableFrequency, ddlReceivableFrequency, trTransactionTypeSipFreq, tdlb1SipFreq, tdddlSipFreq, trMinMaxTenure, trMinMaxAge, tdMinNumberOfApplication, tdtxtMinNumberOfApplication1, ddlProductType.SelectedValue, ddlCommissionType.SelectedValue
                        , lblMinNumberOfApplication, txtMinNumberOfApplication, lblSIPFrequency, ddlSIPFrequency, ddlTransaction, chkListTtansactionType, lblTransactionType, ddlCommisionCalOn, ddlCommissionApplicableLevel
                        , lblMaxNumberOfApplication, tdlb1MaxNumberOfApplication, tdtxtMaxNumberOfApplication, txtMaxNumberOfApplication, trMinAndMaxNumberOfApplication, trMinMAxInvAmount, tdlblApplicationNo, tdApplicationNo, trincentive, ddlIncentiveType, ddlCommissionType, chkCloBack, tdlblClock);


                    ddlInvestorType.SelectedValue = strCustomerCategory;
                    ddlTenureFrequency.SelectedValue = strTenureUnit;
                    ddlInvestAgeTenure.SelectedValue = incentiveAge;
                    ddlCommisionCalOn.SelectedValue = strCalculatedOn;
                    if (ddlCommisionCalOn.SelectedValue.ToString().ToUpper() == "APPC")
                    {
                        trMinAndMaxNumberOfApplication.Visible = true;
                        trMinMAxInvAmount.Visible = false;
                    }
                    else if (ddlCommisionCalOn.SelectedValue.ToString().ToUpper() == "INAM")
                    {
                        trMinMAxInvAmount.Visible = true;
                    }
                    else if (ddlCommisionCalOn.SelectedValue.ToString().ToUpper() == "APPNO")
                    {
                        tdlblApplicationNo.Visible = true;
                        tdApplicationNo.Visible = true;
                        trMinAndMaxNumberOfApplication.Visible = false;
                        trMinMAxInvAmount.Visible = false;
                    }
                    else if (ddlCommisionCalOn.SelectedValue.ToString().ToUpper() == "NA")
                    {
                        trMinMAxInvAmount.Visible = false;
                    }
                    chkListTtansactionType.Visible = true;

                    TxtRuleName.Text = hdnRuleName.Value;
                    if ((strCommissionType == "IN" || strCommissionType == "TC" || strCommissionType == "UF") && (strInvestmentTransactionType.Contains("SIP") || strInvestmentTransactionType.Contains("STB")) && !strInvestmentTransactionType.Contains("NFO"))
                    {
                        ddlTransaction.Visible = true;
                        ddlTransaction.SelectedValue = "SIP";
                        trMinMaxTenure.Visible = true;
                        foreach (ListItem chkItems in chkListTtansactionType.Items)
                        {
                            if (chkItems.Value == "SIP" || chkItems.Value == "STB")
                            {
                                chkItems.Enabled = true;
                                chkItems.Selected = true;
                            }
                            else
                            {
                                chkItems.Enabled = false;
                                chkItems.Selected = false;
                            }
                        }

                    }
                    else if ((strCommissionType == "IN" || strCommissionType == "TC" || strCommissionType == "UF") && (strInvestmentTransactionType.Contains("ABY") || strInvestmentTransactionType.Contains("BUY") || strInvestmentTransactionType.Contains("NFO") || strInvestmentTransactionType.Contains("SWB")) && !strInvestmentTransactionType.Contains("SIP"))
                    {
                        foreach (ListItem chkItems in chkListTtansactionType.Items)
                        {
                            if (chkItems.Value == "SIP" || chkItems.Value == "STB")
                            {
                                chkItems.Enabled = false;
                                chkItems.Selected = false;
                            }
                            else
                            {
                                chkItems.Enabled = true;
                                chkItems.Selected = true;
                            }
                        }
                        ddlTransaction.Visible = true;

                        ddlTransaction.SelectedValue = "NonSIP";
                        trMinMaxTenure.Visible = false;

                    }
                    else if ((strCommissionType == "IN" || strCommissionType == "TC" || strCommissionType == "UF") && (strInvestmentTransactionType.Contains("SIP") && strInvestmentTransactionType.Contains("BUY")))
                    {
                        foreach (ListItem chkItems in chkListTtansactionType.Items)
                        {

                            chkItems.Enabled = false;
                            chkItems.Selected = true;
                            //}
                        }
                        ddlTransaction.Visible = true;

                        ddlTransaction.SelectedValue = "All";
                        trMinMaxTenure.Visible = false;

                    }

                    ddlSIPFrequency.SelectedValue = strSIPFrequency;

                    if (Request.QueryString["StructureId"] != null)
                    {
                        if (ddlProductType.SelectedValue == "FI" || ddlProductType.SelectedValue == "IP")
                        {

                            tbNcdIssueList.Visible = false;
                            chkListTtansactionType.Visible = false;
                        }
                        else
                        {
                            chkListTtansactionType.Visible = true;
                        }
                    }

                }
                if (ddlProductType.SelectedValue == "FI" || ddlProductType.SelectedValue == "IP")
                {
                    chkListTtansactionType.Visible = false;
                }
                else
                {
                    chkListTtansactionType.Visible = true;
                }
                if (ddlSubInstrCategory.SelectedValue == "FICGCG")
                {
                    chkCategory.Visible = false;
                }

                if (ddlProductType.SelectedValue == "IP")
                {
                    chkSeries.Visible = false;
                    tdddlSeries.Visible = false;
                    tdlblSerise.Visible = false;
                    chkCategory.Visible = true;
                }
                if (ddlSubInstrCategory.SelectedValue == "FISDSD")
                {
                    chkMode.Visible = false;
                    tdddlMode.Visible = false;
                    tdlblMode.Visible = false;
                }
            }

        }

        protected void RadGridStructureRule_DeleteCommand(object source, GridCommandEventArgs e)
        {
            Int32 commissionStructureRuleId = Convert.ToInt32(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_CommissionStructureRuleId"].ToString());
            commisionReceivableBo.DeleteCommissionStructureRule(commissionStructureRuleId, false);
            BindCommissionStructureRuleGrid(Convert.ToInt32(hidCommissionStructureName.Value));
        }

        protected void BindCommissionStructureRuleGrid(int structureId)
        {

            DataSet dsStructureRules = commisionReceivableBo.GetAdviserCommissionStructureRules(advisorVo.advisorId, structureId);
            DataTable dtStructureRules = dsStructureRules.Tables[0];
            if (ddlProductType.SelectedValue == "MF" && dtStructureRules.Rows.Count <= 0)
            {
                dtStructureRules = null;
                dtStructureRules = CreateCommissionStructureRuleDataTable();
                dtStructureRules = Createtable(dtStructureRules);
                RadGridStructureRule.MasterTableView.GetColumn("Edit1").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("Delete").Visible = false;

            }
            else
            {

                RadGridStructureRule.MasterTableView.GetColumn("Update").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("Edit1").Visible = true;
                RadGridStructureRule.MasterTableView.GetColumn("Delete").Visible = true;

                ViewState["gridEdit"] = "0";
            }
            RadGridStructureRule.DataSource = dtStructureRules;
            RadGridStructureRule.DataBind();
            Cache.Insert(userVo.UserId.ToString() + "CommissionStructureRule", dtStructureRules);

        }

        protected void RadGridStructureRule_ItemInserted(object source, GridCommandEventArgs e)
        {
            try
            {

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ReceivableSetup.ascx.cs:CollectStructureMastetrData()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void RadGridStructureRule_UpdateCommand(object source, GridCommandEventArgs e)
        {
            bool isPageValid = true;
            CheckBoxList chkListTtansactionType = (CheckBoxList)e.Item.FindControl("chkListTtansactionType");
            CustomValidator CustomValidator4 = (CustomValidator)e.Item.FindControl("CustomValidator4");
            string chkItemType = string.Empty;
            foreach (ListItem chkItems in chkListTtansactionType.Items)
            {
                if (chkItems.Selected == true)
                {
                    chkItemType = commissionStructureRuleVo.TransactionType + chkItems.Value + ",";

                }
            }
            if (!CheckRuleDate(e))
            {
                return;
            }
            /*******************UI VALIDATION********************/

            if (isPageValid)
            {

                string sbRuleHash = commisionReceivableBo.GetHash(commissionStructureRuleVo);
                if (!string.IsNullOrEmpty(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_CommissionStructureRuleId"].ToString()))
                {
                    /*******************COLLECT DATA********************/
                    commissionStructureRuleVo = CollectDataForCommissionStructureRule(e);
                    commissionStructureRuleVo.CommissionStructureRuleId = Convert.ToInt32(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_CommissionStructureRuleId"].ToString());
                    commisionReceivableBo.UpdateCommissionStructureRule(commissionStructureRuleVo, userVo.UserId, sbRuleHash);
                }
                else
                {
                    RadGridStructureRule_InsertCommand(source, e);
                }
                BindCommissionStructureRuleGrid(Convert.ToInt32(hidCommissionStructureName.Value));

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('AUM For is Required !');", true);
                e.Canceled = true;
                return;
            }

        }

        protected void RadGridStructureRule_ItemCommand(object source, GridCommandEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                RadGridStructureRule.MasterTableView.GetColumn("Update").Visible = true;

            }
        }
        protected void RadGridStructureRule_OnCancelCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.CancelCommandName)
            {
                BindCommissionStructureRuleGrid(Convert.ToInt32(hidCommissionStructureName.Value));
            }
        }


        protected void RadGridStructureRule_InsertCommand(object source, GridCommandEventArgs e)
        {
            bool isPageValid = true;
            int ruleId = 0;
            try
            {
                RadGrid rgCommissionTypeCaliculation = (RadGrid)e.Item.FindControl("rgCommissionTypeCaliculation");
                /*******************COLLECT DATA********************/
                commissionStructureRuleVo = CollectDataForCommissionStructureRule(e);
                Button btnSubmitRule = (Button)e.Item.FindControl("btnSubmitRule");
                /*******************UI VALIDATION********************/
                //isPageValid = ValidatePage(commissionStructureRuleVo);

                if (!CheckRuleDate(e))
                {
                    return;
                }

                /*******************DUPLICATE CHECK********************/

                if (isPageValid)
                {
                    string sbRuleHash = commisionReceivableBo.GetHash(commissionStructureRuleVo);
                    if (commisionReceivableBo.hasRule(commissionStructureRuleVo, sbRuleHash))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Rule already exists');", true);
                        return;
                    }
                    int count = commisionReceivableBo.RuleIsCreated(int.Parse(hidCommissionStructureName.Value));
                    if (count == 0 && ddlProductType.SelectedValue == "MF")
                    {
                        ViewState["bindGrid"] = "1";
                        ViewState["ItemRemove"] = e.Item.ItemIndex;
                        CreateRuleAndRateForMFBONDIPO();
                    }
                    else
                    {
                        RadGridStructureRule.MasterTableView.GetColumn("Edit1").Visible = false;
                        RadGridStructureRule.MasterTableView.GetColumn("Delete").Visible = true;
                    }
                    ruleId = commisionReceivableBo.CreateCommissionStructureRule(commissionStructureRuleVo, userVo.UserId, sbRuleHash.ToString());
                    HiddenField1.Value = ruleId.ToString();
                    hdnIsSpecialIncentive.Value = commissionStructureRuleVo.CommissionSubType;
                    e.Canceled = true;
                    rgCommissionTypeCaliculation.Visible = true;
                    BindRuleDetGrid(rgCommissionTypeCaliculation, ruleId);
                    Table5.Visible = true;

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('AUM For is Required !');", true);
                    e.Canceled = true;
                    return;
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
                FunctionInfo.Add("Method", "ReceivableSetup.ascx.cs:RadGridStructureRule_InsertCommand()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private CommissionStructureRuleVo CollectDataForCommissionStructureRule(GridCommandEventArgs e)
        {
            try
            {

                Label lb1RuleName = (Label)e.Item.FindControl("lb1RuleName");
                TextBox TxtRuleName = (TextBox)e.Item.FindControl("TxtRuleName");

                DropDownList ddlCommissionType = (DropDownList)e.Item.FindControl("ddlCommissionType");
                DropDownList ddlInvestorType = (DropDownList)e.Item.FindControl("ddlInvestorType");

                TextBox txtMinInvestmentAmount = (TextBox)e.Item.FindControl("txtMinInvestmentAmount");
                TextBox txtMaxInvestmentAmount = (TextBox)e.Item.FindControl("txtMaxInvestmentAmount");

                TextBox txtMinTenure = (TextBox)e.Item.FindControl("txtMinTenure");
                TextBox txtMaxTenure = (TextBox)e.Item.FindControl("txtMaxTenure");
                DropDownList ddlTenureFrequency = (DropDownList)e.Item.FindControl("ddlTenureFrequency");

                TextBox txtMinInvestAge = (TextBox)e.Item.FindControl("txtMinInvestAge");
                TextBox txtMaxInvestAge = (TextBox)e.Item.FindControl("txtMaxInvestAge");
                DropDownList ddlInvestAgeTenure = (DropDownList)e.Item.FindControl("ddlInvestAgeTenure");

                CheckBoxList chkListTtansactionType = (CheckBoxList)e.Item.FindControl("chkListTtansactionType");
                DropDownList ddlSIPFrequency = (DropDownList)e.Item.FindControl("ddlSIPFrequency");


                TextBox txtBrokerageValue = (TextBox)e.Item.FindControl("txtBrokerageValue");
                DropDownList ddlBrokerageUnit = (DropDownList)e.Item.FindControl("ddlBrokerageUnit");

                DropDownList ddlCommisionCalOn = (DropDownList)e.Item.FindControl("ddlCommisionCalOn");

                TextBox txtMinNumberOfApplication = (TextBox)e.Item.FindControl("txtMinNumberOfApplication");
                TextBox txtMaxNumberOfApplication = (TextBox)e.Item.FindControl("txtMaxNumberOfApplication");
                TextBox txtStruRuleComment = (TextBox)e.Item.FindControl("txtStruRuleComment");

                DropDownList ddlAppCityGroup = (DropDownList)e.Item.FindControl("ddlAppCityGroup");
                DropDownList ddlReceivableFrequency = (DropDownList)e.Item.FindControl("ddlReceivableFrequency");
                DropDownList ddlCommissionApplicableLevel = (DropDownList)e.Item.FindControl("ddlCommissionApplicableLevel");
                CheckBoxList chkListApplyTax = (CheckBoxList)e.Item.FindControl("chkListApplyTax");
                TextBox txtTaxValue = (TextBox)e.Item.FindControl("txtTaxValue");
                TextBox txtTDS = (TextBox)e.Item.FindControl("txtTDS");
                TextBox txtApplicationNo = (TextBox)e.Item.FindControl("txtApplicationNo");
                TextBox txtRuleValidityFrom = (TextBox)e.Item.FindControl("txtRuleValidityFrom");
                TextBox txtRuleValidityTo = (TextBox)e.Item.FindControl("txtRuleValidityTo");
                DropDownList ddlIncentiveType = (DropDownList)e.Item.FindControl("ddlIncentiveType");
                CheckBox chkEForm = (CheckBox)e.Item.FindControl("chkEForm");
                DropDownList ddlCategorys = (DropDownList)e.Item.FindControl("ddlCategorys");
                DropDownList ddlSeries = (DropDownList)e.Item.FindControl("ddlSeries");
                DropDownList ddlMode = (DropDownList)e.Item.FindControl("ddlMode");
                CheckBox check = (CheckBox)e.Item.FindControl("chkCloBack");
                TextBox txtbox = (TextBox)e.Item.FindControl("txtAge");
                TextBox txtSBCValue = (TextBox)e.Item.FindControl("txtSBCValue");
                TextBox txtKKCValue = (TextBox)e.Item.FindControl("txtKKCValue");
                commissionStructureRuleVo.eForm = Convert.ToBoolean((chkEForm.Checked) ? true : false);
                if (ddlMode.SelectedValue != "Select")
                    commissionStructureRuleVo.mode = ddlMode.SelectedValue;
                if (!string.IsNullOrEmpty(ddlCategorys.SelectedValue))
                    commissionStructureRuleVo.Category = int.Parse(ddlCategorys.SelectedValue);
                if (!string.IsNullOrEmpty(ddlSeries.SelectedValue))
                    commissionStructureRuleVo.series = int.Parse(ddlSeries.SelectedValue);
                commissionStructureRuleVo.CommissionStructureId = Convert.ToInt32(hidCommissionStructureName.Value);

                commissionStructureRuleVo.CommissionStructureRuleName = TxtRuleName.Text;
                if (!string.IsNullOrEmpty(ddlCommissionType.SelectedValue))
                {
                    commissionStructureRuleVo.CommissionType = ddlCommissionType.SelectedValue;
                    switch (ddlCommissionType.SelectedValue)
                    {
                        case "IN": commissionStructureRuleVo.CommissionSubType = "N";
                            break;
                        case "IM": commissionStructureRuleVo.CommissionSubType = "MBl";
                            break;
                        case "IS": commissionStructureRuleVo.CommissionSubType = "SPl";
                            break;
                        case "IA": commissionStructureRuleVo.CommissionSubType = "N";
                            break;
                        case "UF": commissionStructureRuleVo.CommissionSubType = "0";
                            break;

                    }
                }
                commissionStructureRuleVo.CustomerType = ddlInvestorType.SelectedValue;

                commissionStructureRuleVo.AdviserCityGroupCode = ddlAppCityGroup.SelectedValue;
                commissionStructureRuleVo.ReceivableFrequency = ddlReceivableFrequency.SelectedValue;
                commissionStructureRuleVo.ApplicableLevelCode = "TR";
                commissionStructureRuleVo.applicationNo = txtApplicationNo.Text;
                commissionStructureRuleVo.RuleValidateFrom = Convert.ToDateTime(txtValidityFrom.Text);
                commissionStructureRuleVo.RuleValidateTo = Convert.ToDateTime(txtValidityTo.Text);

                if (chkListApplyTax.Items[0].Selected)
                {
                    commissionStructureRuleVo.IsServiceTaxReduced = true;
                    commissionStructureRuleVo.TaxValue = Convert.ToDecimal(txtTaxValue.Text.Trim());
                }
                if (chkListApplyTax.Items[1].Selected)
                {
                    commissionStructureRuleVo.IsTDSReduced = true;
                    commissionStructureRuleVo.TDSValue = Convert.ToDecimal(txtTDS.Text.Trim());
                }
                if (chkListApplyTax.Items[2].Selected)
                {
                    commissionStructureRuleVo.IsSBCApplicable = true;
                    commissionStructureRuleVo.SBCValue = Convert.ToDecimal(txtSBCValue.Text.Trim());
                }
                if (chkListApplyTax.Items[3].Selected)
                {
                    commissionStructureRuleVo.IsKKCApplicable = true;
                    commissionStructureRuleVo.KKCValue = Convert.ToDecimal(txtKKCValue.Text.Trim());
                }
                if (chkListApplyTax.Items[4].Selected)
                {
                    commissionStructureRuleVo.ServiceTaxInclusive = true;
                }
                if (!string.IsNullOrEmpty(txtMinInvestmentAmount.Text.Trim()))
                    commissionStructureRuleVo.MinInvestmentAmount = Convert.ToDecimal(txtMinInvestmentAmount.Text.Trim());
                if (!string.IsNullOrEmpty(txtMaxInvestmentAmount.Text.Trim()))
                    commissionStructureRuleVo.MaxInvestmentAmount = Convert.ToDecimal(txtMaxInvestmentAmount.Text.Trim());

                if (!string.IsNullOrEmpty(txtMinTenure.Text.Trim()))
                    commissionStructureRuleVo.TenureMin = Convert.ToInt32(txtMinTenure.Text.Trim());
                if (!string.IsNullOrEmpty(txtMaxTenure.Text.Trim()))
                    commissionStructureRuleVo.TenureMax = Convert.ToInt32(txtMaxTenure.Text.Trim());
                if (!string.IsNullOrEmpty(txtMinTenure.Text.Trim()) || !string.IsNullOrEmpty(txtMaxTenure.Text.Trim()))
                    commissionStructureRuleVo.TenureUnit = ddlTenureFrequency.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(txtMinInvestAge.Text.Trim()))
                    commissionStructureRuleVo.MinInvestmentAge = Convert.ToInt32(txtMinInvestAge.Text.Trim());
                if (!string.IsNullOrEmpty(txtMaxInvestAge.Text.Trim()))
                    commissionStructureRuleVo.MaxInvestmentAge = Convert.ToInt32(txtMaxInvestAge.Text.Trim());
                if (!string.IsNullOrEmpty(txtMinInvestAge.Text.Trim()) || !string.IsNullOrEmpty(txtMaxInvestAge.Text.Trim()))
                    commissionStructureRuleVo.InvestmentAgeUnit = ddlInvestAgeTenure.SelectedValue;

                foreach (ListItem chkItems in chkListTtansactionType.Items)
                {
                    if (chkItems.Selected == true)
                    {
                        commissionStructureRuleVo.TransactionType = commissionStructureRuleVo.TransactionType + chkItems.Value + ",";
                        if (chkItems.Value == "SIP")
                            commissionStructureRuleVo.SIPFrequency = ddlSIPFrequency.SelectedValue;
                    }
                }
                if (!string.IsNullOrEmpty(commissionStructureRuleVo.TransactionType))
                    commissionStructureRuleVo.TransactionType = commissionStructureRuleVo.TransactionType.Remove((commissionStructureRuleVo.TransactionType.Length - 1), 1);


                commissionStructureRuleVo.CalculatedOnCode = ddlCommisionCalOn.SelectedValue;

                if (!string.IsNullOrEmpty(txtMinNumberOfApplication.Text.Trim()))
                    commissionStructureRuleVo.MinNumberofApplications = Convert.ToInt16(txtMinNumberOfApplication.Text.Trim());
                if (!string.IsNullOrEmpty(txtMaxNumberOfApplication.Text.Trim()))
                    commissionStructureRuleVo.MaxNumberofApplications = Convert.ToInt32(txtMaxNumberOfApplication.Text.Trim());


                if (!string.IsNullOrEmpty(txtStruRuleComment.Text.Trim()))
                    commissionStructureRuleVo.StructureRuleComment = txtStruRuleComment.Text.Trim();
                commissionStructureRuleVo.AdviserId = rmVo.AdviserId;
                if (check.Checked)
                {
                    commissionStructureRuleVo.IsClawBack = 1;
                }
                else
                    commissionStructureRuleVo.IsClawBack = 0;

                commissionStructureRuleVo.ClawBackAge = (!string.IsNullOrEmpty(txtbox.Text)) ? int.Parse(txtbox.Text) : 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ReceivableSetup.ascx.cs:CollectDataForCommissionStructureRule()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return commissionStructureRuleVo;

        }

        protected void RadGridStructureRule_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtStructureRules = new DataTable();
            if (Cache[userVo.UserId.ToString() + "CommissionStructureRule"] != null)
            {
                dtStructureRules = (DataTable)Cache[userVo.UserId.ToString() + "CommissionStructureRule"];
                RadGridStructureRule.DataSource = dtStructureRules;
            }

        }

        private void ShowAndHideSTructureRuleControlsBasedOnProductAndCommisionType(Label lblReceivableFrequency, DropDownList ddlReceivableFrequency, System.Web.UI.HtmlControls.HtmlTableRow trTransactionTypeSipFreq, System.Web.UI.HtmlControls.HtmlTableCell tdlb1SipFreq, System.Web.UI.HtmlControls.HtmlTableCell tdddlSipFreq, System.Web.UI.HtmlControls.HtmlTableRow trMinMaxTenure, System.Web.UI.HtmlControls.HtmlTableRow trMinMaxAge, System.Web.UI.HtmlControls.HtmlTableCell tdMinNumberOfApplication, System.Web.UI.HtmlControls.HtmlTableCell tdtxtMinNumberOfApplication1, string product, string CommisionType
            , Label lblMinNumberOfApplication, TextBox txtMinNumberOfApplication, Label lblSIPFrequency, DropDownList ddlSIPFrequency, DropDownList ddlTransaction, CheckBoxList chkListTtansactionType, Label lblTransactionType, DropDownList ddlCommisionCalOn, DropDownList ddlCommissionApplicableLevel
            , Label lblMaxNumberOfApplication, System.Web.UI.HtmlControls.HtmlTableCell tdlb1MaxNumberOfApplication, System.Web.UI.HtmlControls.HtmlTableCell tdtxtMaxNumberOfApplication, TextBox txtMaxNumberOfApplication, System.Web.UI.HtmlControls.HtmlTableRow trMinAndMaxNumberOfApplication, System.Web.UI.HtmlControls.HtmlTableRow trMinMAxInvAmount,
            System.Web.UI.HtmlControls.HtmlTableCell tdlblApplicationNo, System.Web.UI.HtmlControls.HtmlTableCell tdApplicationNo, System.Web.UI.HtmlControls.HtmlTableRow trincentive, DropDownList ddlIncentiveType, DropDownList ddlCommissionType, CheckBox check, System.Web.UI.HtmlControls.HtmlTableCell tdlblClock)
        {
            bool enablement = false;
            lblSIPFrequency.Visible = enablement;
            ddlSIPFrequency.Visible = enablement;




            if (product == "FI" || product == "IP")
            {



                lblReceivableFrequency.Visible = enablement;
                ddlReceivableFrequency.Visible = enablement;
                trTransactionTypeSipFreq.Visible = enablement;
                trMinMaxTenure.Visible = enablement;
                tdMinNumberOfApplication.Visible = enablement;
                trMinMaxAge.Visible = enablement;

                trMinAndMaxNumberOfApplication.Visible = enablement;
                if (CommisionType == "UF")
                {
                    ddlCommissionApplicableLevel.SelectedValue = "TR";
                    ddlCommissionApplicableLevel.Enabled = false;
                    ddlCommisionCalOn.Items[1].Enabled = false;
                    ddlCommisionCalOn.Items[0].Enabled = false;
                    ddlCommisionCalOn.Items[2].Enabled = false;
                    trMinAndMaxNumberOfApplication.Visible = false;
                    ddlCommisionCalOn.Items[3].Enabled = false;
                    ddlCommisionCalOn.Items[4].Enabled = true;
                    ddlCommisionCalOn.Items[5].Enabled = false;
                    trMinMAxInvAmount.Visible = true;
                    trincentive.Visible = false;
                    tdApplicationNo.Visible = false;
                    tdlblApplicationNo.Visible = false;
                }
                else if (CommisionType == "TC")
                {
                    ddlCommissionApplicableLevel.Enabled = true;
                    tdMinNumberOfApplication.Visible = !enablement;
                    lblReceivableFrequency.Visible = !enablement;
                    ddlReceivableFrequency.Visible = !enablement;
                    trMinAndMaxNumberOfApplication.Visible = !enablement;
                    trincentive.Visible = false;
                    tdApplicationNo.Visible = false;
                    tdlblApplicationNo.Visible = false;
                }
                else if (CommisionType == "IN" || CommisionType == "IM" || CommisionType == "IA" || CommisionType == "IS")
                {
                    ddlCommissionApplicableLevel.SelectedValue = "TR";
                    ddlCommissionApplicableLevel.Enabled = false;
                    tdMinNumberOfApplication.Visible = !enablement;
                    trMinAndMaxNumberOfApplication.Visible = !enablement;
                    ddlCommisionCalOn.Items[5].Enabled = false;

                    ddlCommisionCalOn.Items[1].Enabled = false;
                    ddlCommisionCalOn.Items[2].Enabled = false;
                    ddlCommisionCalOn.Items[3].Enabled = false;
                    ddlCommisionCalOn.Items[4].Enabled = false;
                    trMinMAxInvAmount.Visible = false;

                    if (CommisionType == "IN")
                    {
                        ddlCommisionCalOn.Items[4].Enabled = true;
                        ddlCommisionCalOn.Items[5].Enabled = true;
                        trMinMAxInvAmount.Visible = true;


                    }
                    else if (CommisionType == "IM")
                    {
                        ddlCommisionCalOn.Items[4].Enabled = true;
                        ddlCommisionCalOn.Items[2].Enabled = true;
                        trMinMAxInvAmount.Visible = true;

                    }
                    else if (CommisionType == "IS")
                    {
                        ddlCommisionCalOn.Items[4].Enabled = true;
                        ddlCommisionCalOn.Items[3].Enabled = true;
                        trMinMAxInvAmount.Visible = true;

                    }
                    else if (CommisionType == "IA")
                    {
                        ddlCommisionCalOn.Items[5].Enabled = true;
                        trMinMAxInvAmount.Visible = false;

                    }
                    trMinAndMaxNumberOfApplication.Visible = false;
                    if (ddlCommisionCalOn.Items[3].Selected == true)
                    {
                        tdApplicationNo.Visible = true;
                        tdlblApplicationNo.Visible = true;
                    }
                }
            }
            else if (product == "MF")
            {


                trMinMaxTenure.Visible = enablement;
                trMinMaxAge.Visible = enablement;

                trMinAndMaxNumberOfApplication.Visible = enablement;
                lblReceivableFrequency.Visible = enablement;
                ddlReceivableFrequency.Visible = enablement;
                ddlCommisionCalOn.Enabled = true;
                if (CommisionType == "IN")
                {

                    trTransactionTypeSipFreq.Visible = !enablement;
                    trMinMaxTenure.Visible = !enablement;

                    trMinAndMaxNumberOfApplication.Visible = false;
                    chkListTtansactionType.Visible = !enablement;
                    lblTransactionType.Visible = !enablement;
                    ddlTransaction.Visible = !enablement;
                    ddlCommisionCalOn.Items[0].Enabled = false;
                    ddlCommisionCalOn.Items[1].Enabled = false;
                    ddlCommisionCalOn.Items[2].Enabled = false;
                    ddlCommisionCalOn.Items[3].Enabled = false;
                    ddlCommisionCalOn.Items[5].Enabled = false;
                    check.Visible = false;
                    check.Checked = false;
                    tdlblClock.Visible = true;
                    check.Visible = true;
                    check.Checked = true;
                    foreach (ListItem chkItems in chkListTtansactionType.Items)
                    {

                        chkItems.Enabled = true;
                    }
                }
                else if (CommisionType == "TC")
                {
                    ddlCommissionApplicableLevel.SelectedValue = "TR";
                    ddlCommissionApplicableLevel.Enabled = false;
                    trTransactionTypeSipFreq.Visible = !enablement;
                    trMinMaxAge.Visible = !enablement;
                    tdlb1SipFreq.Visible = enablement;
                    tdddlSipFreq.Visible = enablement;
                    lblReceivableFrequency.Visible = enablement;
                    ddlReceivableFrequency.Visible = enablement;
                    chkListTtansactionType.Visible = !enablement;
                    lblTransactionType.Visible = !enablement;
                    ddlCommisionCalOn.Items[0].Enabled = true;
                    //ddlCommisionCalOn.SelectedValue = "AGAM";
                    //ddlCommisionCalOn.Enabled = false;
                    ddlCommisionCalOn.Items[1].Enabled = true;
                    tdApplicationNo.Visible = false;
                    tdlblApplicationNo.Visible = false;
                    trMinMAxInvAmount.Visible = false;
                    tdlblClock.Visible = false;
                    check.Visible = false;
                    check.Checked = false;
                    foreach (ListItem chkItems in chkListTtansactionType.Items)
                    {

                        chkItems.Enabled = true;
                        chkItems.Selected = true;
                    }
                }
                else if (CommisionType == "UF")
                {
                    ddlCommisionCalOn.SelectedValue = "INAM";
                    ddlCommisionCalOn.Enabled = false;
                    trTransactionTypeSipFreq.Visible = !enablement;
                    chkListTtansactionType.Visible = !enablement;
                    lblTransactionType.Visible = !enablement;
                    trMinMAxInvAmount.Visible = true;
                    ddlCommissionApplicableLevel.Enabled = true;
                    check.Visible = true;
                    tdlblClock.Visible = true;

                    foreach (ListItem chkItems in chkListTtansactionType.Items)
                    {
                        chkItems.Selected = true;
                    }
                }

            }


        }


        private bool ValidateCommissionRule(CommissionStructureRuleVo commissionStructureRuleVo)
        {
            bool isValidRule = false;
            var duplicateCheck = new List<bool>();
            try
            {

                DataSet dsCommissionRule = commisionReceivableBo.GetStructureCommissionAllRules(commissionStructureRuleVo.CommissionStructureId, commissionStructureRuleVo.CommissionType);
                foreach (DataRow dr in dsCommissionRule.Tables[0].Rows)
                {
                    /******Check for Customer Type******/
                    if (dr["XCT_CustomerTypeCode"].ToString() == commissionStructureRuleVo.CustomerType)
                    {
                        duplicateCheck.Add(true);
                    }
                    else
                    {
                        duplicateCheck.Add(false);

                    }


                    /*********Check for Investment Amount*********/
                    if ((!string.IsNullOrEmpty(dr["ACSR_MinInvestmentAmount"].ToString()) && !string.IsNullOrEmpty(dr["ACSR_MinInvestmentAmount"].ToString()))
                        && (commissionStructureRuleVo.MinInvestmentAmount != 0 && commissionStructureRuleVo.MaxInvestmentAmount != 0))
                    {
                        if ((commissionStructureRuleVo.MinInvestmentAmount > Convert.ToDecimal(dr["ACSR_MinInvestmentAmount"].ToString())) && (commissionStructureRuleVo.MinInvestmentAmount < Convert.ToDecimal(dr["ACSR_MaxInvestmentAmount"].ToString()))
                            || (commissionStructureRuleVo.MaxInvestmentAmount > Convert.ToDecimal(dr["ACSR_MinInvestmentAmount"].ToString())) && (commissionStructureRuleVo.MaxInvestmentAmount < Convert.ToDecimal(dr["ACSR_MaxInvestmentAmount"].ToString())))
                        {
                            duplicateCheck.Add(true);
                        }
                        else
                        {
                            duplicateCheck.Add(false);

                        }
                    }

                    /**********Check for Tenure**********/
                    if ((commissionStructureRuleVo.TenureUnit == dr["ACSR_TenureUnit"].ToString())
                        && (!string.IsNullOrEmpty(dr["ACSR_MinInvestmentAmount"].ToString())
                        && !string.IsNullOrEmpty(dr["ACSR_MinInvestmentAmount"].ToString()))
                        && (commissionStructureRuleVo.TenureMin != 0 && commissionStructureRuleVo.TenureMax != 0))
                    {
                        if ((commissionStructureRuleVo.TenureMin > Convert.ToDecimal(dr["ACSR_MinTenure"].ToString())) && (commissionStructureRuleVo.TenureMin < Convert.ToDecimal(dr["ACSR_MaxTenure"].ToString()))
                            || (commissionStructureRuleVo.TenureMax > Convert.ToDecimal(dr["ACSR_MinTenure"].ToString())) && (commissionStructureRuleVo.TenureMax < Convert.ToDecimal(dr["ACSR_MaxTenure"].ToString())))
                        {
                            duplicateCheck.Add(true);
                        }
                        else
                        {
                            duplicateCheck.Add(false);

                        }

                    }
                    /******Check for Investment Age ******/

                    if ((!string.IsNullOrEmpty(dr["ACSR_MinInvestmentAge"].ToString()) && !string.IsNullOrEmpty(dr["ACSR_MinInvestmentAge"].ToString()))
                        && (commissionStructureRuleVo.MinInvestmentAge != 0 && commissionStructureRuleVo.MaxInvestmentAge != 0))
                    {
                        if ((commissionStructureRuleVo.MinInvestmentAge > Convert.ToDecimal(dr["ACSR_MinInvestmentAge"].ToString())) && (commissionStructureRuleVo.MinInvestmentAge < Convert.ToDecimal(dr["ACSR_MaxInvestmentAge"].ToString()))
                            || (commissionStructureRuleVo.MaxInvestmentAge > Convert.ToDecimal(dr["ACSR_MinInvestmentAge"].ToString())) && (commissionStructureRuleVo.MaxInvestmentAge < Convert.ToDecimal(dr["ACSR_MaxInvestmentAge"].ToString())))
                        {
                            duplicateCheck.Add(true);
                        }
                        else
                        {
                            duplicateCheck.Add(false);

                        }
                    }
                    /******Check for Transaction Type ******/
                    if (!string.IsNullOrEmpty(commissionStructureRuleVo.TransactionType) && !string.IsNullOrEmpty(dr["ACSR_TransactionType"].ToString()))
                    {
                        string[] arrayTTypeE = dr["ACSR_TransactionType"].ToString().Split(',');
                        string[] arrayTTypeN = commissionStructureRuleVo.TransactionType.ToString().Split(',');
                        if (arrayTTypeE.Count() == arrayTTypeN.Count())
                        {
                            var duplicateCheckTType = new List<bool>();

                            foreach (string str in arrayTTypeE)
                            {
                                if (!string.IsNullOrEmpty(str.Trim()))
                                {
                                    duplicateCheckTType.Add(str.Contains(commissionStructureRuleVo.TransactionType));
                                }
                            }

                            if (duplicateCheckTType.Count == duplicateCheckTType.Distinct().Count())
                            {
                                duplicateCheck.Add(true);
                            }
                            else
                            {
                                duplicateCheck.Add(false);
                            }
                        }
                        else
                        {
                            duplicateCheck.Add(false);
                        }

                    }

                    /******Check for Minimum Application Nos ******/
                    if (commissionStructureRuleVo.MinNumberofApplications != 0 && String.IsNullOrEmpty(dr["ACSR_MinNumberOfApplications"].ToString()))
                    {
                        if (commissionStructureRuleVo.MinNumberofApplications == Convert.ToInt32(dr["ACSR_MinNumberOfApplications"].ToString()))
                        {
                            duplicateCheck.Add(true);
                        }
                    }
                    else
                    {
                        duplicateCheck.Add(false);
                    }
                    if (commissionStructureRuleVo.MaxNumberofApplications != 0 && String.IsNullOrEmpty(dr["ACSR_MaxNumberOfApplications"].ToString()))
                    {
                        if (commissionStructureRuleVo.MaxNumberofApplications == Convert.ToInt32(dr["ACSR_MaxNumberOfApplications"].ToString()))
                        {
                            duplicateCheck.Add(true);
                        }
                    }
                    else
                    {
                        duplicateCheck.Add(false);
                    }
                    if (commissionStructureRuleVo.TaxValue != 0 && String.IsNullOrEmpty(dr["ACSR_ReducedValue"].ToString()))
                    {
                        if (commissionStructureRuleVo.TaxValue == Convert.ToDecimal(dr["ACSR_ReducedValue"].ToString()))
                        {
                            duplicateCheck.Add(true);
                        }
                    }
                    else
                    {
                        duplicateCheck.Add(false);
                    }
                    if (commissionStructureRuleVo.TDSValue != 0 && String.IsNullOrEmpty(dr["ACSR_ServiceTaxValue"].ToString()))
                    {
                        if (commissionStructureRuleVo.TDSValue == Convert.ToDecimal(dr["ACSR_ServiceTaxValue"].ToString()))
                        {
                            duplicateCheck.Add(true);
                        }
                    }
                    else
                    {
                        duplicateCheck.Add(false);
                    }

                    if (duplicateCheck.Count(b => b == false) >= 1)
                    {
                        isValidRule = true;
                    }
                    else
                    {
                        isValidRule = false;
                        break;
                    }

                }

                // Commission structure Rule Duplicates exist

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ReceivableSetup.ascx.cs:ValidateCommissionRule()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return isValidRule;
        }

        private void LoadStructureDetails(int structureId)
        {
            try
            {
                commissionStructureMasterVo = commisionReceivableBo.GetCommissionStructureMaster(structureId);
                BindSubcategoryListBox(commissionStructureMasterVo.AssetCategory);
                ddlProductType.SelectedValue = commissionStructureMasterVo.ProductType;
                ddlCategory.SelectedValue = commissionStructureMasterVo.AssetCategory;
                ShowHideControlsBasedOnProduct(ddlProductType.SelectedValue);
                ddlIssuer.SelectedValue = commissionStructureMasterVo.Issuer;
                hdnRulestart.Value = commissionStructureMasterVo.ValidityStartDate.ToShortDateString();
                hdnRuleEnd.Value = commissionStructureMasterVo.ValidityEndDate.ToShortDateString();
                txtValidityFrom.Text = commissionStructureMasterVo.ValidityStartDate.ToShortDateString();
                txtValidityTo.Text = commissionStructureMasterVo.ValidityEndDate.ToShortDateString();
                txtStructureName.Text = commissionStructureMasterVo.CommissionStructureName;
                chkHasClawBackOption.Checked = commissionStructureMasterVo.IsClawBackApplicable;
                chkMoneytaryReward.Checked = commissionStructureMasterVo.IsNonMonetaryReward;
                txtNote.Text = commissionStructureMasterVo.StructureNote;
                hidCommissionStructureName.Value = structureId.ToString();
                CommissionStructureControlsEnable(false);

                hdnProductId.Value = commissionStructureMasterVo.ProductType;
                hdnStructValidFrom.Value = commissionStructureMasterVo.ValidityStartDate.ToShortDateString();
                hdnStructValidTill.Value = commissionStructureMasterVo.ValidityEndDate.ToShortDateString();
                hdnIssuerId.Value = commissionStructureMasterVo.Issuer;
                hdnCategoryId.Value = commissionStructureMasterVo.AssetCategory;
                string subcategoryIds = commissionStructureMasterVo.AssetSubCategory.ToString();
                subcategoryIds = subcategoryIds.Replace("~", ",");
                hdnSubcategoryIds.Value = subcategoryIds;
                BindBondCategories();
                if (subcategoryIds != "0")
                    ddlSubInstrCategory.SelectedValue = commissionStructureMasterVo.AssetSubCategory.ToString();
                GetMapped_Unmapped_Issues("Mapped", "");
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ReceivableSetup.ascx.cs:ValidateCommissionRule()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }


        private void CommissionStructureControlsEnable(bool enable)
        {
            if (enable)
            {
                ddlProductType.Enabled = true;
                ddlCategory.Enabled = true;
                rlbAssetSubCategory.Enabled = true;
                ddlIssuer.Enabled = true;
                txtValidityFrom.Enabled = true;
                txtValidityTo.Enabled = true;
                txtStructureName.Enabled = true;
                chkHasClawBackOption.Enabled = true;
                chkMoneytaryReward.Enabled = true;
                txtNote.Enabled = true;
                btnStructureSubmit.Visible = false;
                btnStructureUpdate.Visible = true;
                Table5.Visible = true;
                lnkEditStructure.Visible = false;

            }
            else
            {
                ddlProductType.Enabled = false;
                ddlCategory.Enabled = false;
                rlbAssetSubCategory.Enabled = false;
                ddlIssuer.Enabled = true;
                txtValidityFrom.Enabled = false;
                txtValidityTo.Enabled = false;
                txtStructureName.Enabled = false;
                chkHasClawBackOption.Enabled = false;
                chkMoneytaryReward.Enabled = false;
                txtNote.Enabled = false;

                lnkEditStructure.Visible = true;
                lnkEditStructure.Visible = true;
                lnkEditStructure.ToolTip = "Edit commission structure section";
                lnkAddNewStructure.Visible = false;
                btnStructureSubmit.Visible = false;
                btnStructureUpdate.Visible = false;

            }

        }

        protected void lnkEditStructure_Click(object sender, EventArgs e)
        {
            if (lnkEditStructure.Text == "View")
            {
                LoadStructureDetails(Convert.ToInt32(hidCommissionStructureName.Value));
                CommissionStructureControlsEnable(false);
            }
            else if (lnkEditStructure.Text == "Edit")
                CommissionStructureControlsEnable(true);
        }

        protected void lnkAddNewStructure_Click(object sender, EventArgs e)
        {
            ControlStateNewStructureCreate();
        }

        private void ControlStateNewStructureCreate()
        {
            ddlCategory.SelectedIndex = 0;
            rlbAssetSubCategory.Items.Clear();
            ddlIssuer.SelectedIndex = 0;
            txtValidityFrom.Text = string.Empty;
            txtValidityTo.Text = string.Empty;
            txtStructureName.Text = string.Empty;
            chkHasClawBackOption.Checked = false;
            chkMoneytaryReward.Checked = false;
            txtNote.Text = string.Empty;
            CommissionStructureControlsEnable(true);
            btnStructureSubmit.Visible = true;
            btnStructureUpdate.Visible = false;
            lnkEditStructure.Visible = false;
            lnkAddNewStructure.Visible = false;


            if (Cache[userVo.UserId.ToString() + "CommissionStructureRule"] != null)
                Cache.Remove(userVo.UserId.ToString() + "CommissionStructureRule");
            if (Cache[userVo.UserId.ToString() + "RulePayableDet"] != null)
                Cache.Remove(userVo.UserId.ToString() + "RulePayableDet");

            BindCommissionStructureRuleBlankRow();
            tblCommissionStructureRule.Visible = false;
            tblCommissionStructureRule1.Visible = false;

            btnMapToscheme.Visible = false;

            ShowHideControlsBasedOnProduct("MF");
            SetStructureMasterControlDefaultValues("MF");
            BindAllDropdown();
            BindSubcategoryListBox(ddlCategory.SelectedValue);
        }

        private void BindCommissionStructureRuleBlankRow()
        {

            DataTable dtStructureRules = new DataTable();
            dtStructureRules = CreateCommissionStructureRuleDataTable();

            if (ddlProductType.SelectedValue == "MF")
            {
                ViewState["gridEdit"] = 1;
                dtStructureRules = Createtable(dtStructureRules);
                RadGridStructureRule.MasterTableView.GetColumn("Update").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("Edit").Visible = true;
                RadGridStructureRule.MasterTableView.GetColumn("Delete").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("Edit1").Visible = false;
            }
            else
            {
                RadGridStructureRule.MasterTableView.GetColumn("Edit1").Visible = false;
                RadGridStructureRule.MasterTableView.GetColumn("Delete").Visible = false;

            }
            RadGridStructureRule.DataSource = dtStructureRules;
            RadGridStructureRule.Rebind();
            Cache.Insert(userVo.UserId.ToString() + "CommissionStructureRule", dtStructureRules);
        }
        protected DataTable Createtable(DataTable dttable)
        {
            DataRow dr = dttable.NewRow();
            dr["SNO."] = 1;
            dr["WCT_CommissionType"] = "UF";
            dr["WCT_CommissionTypeCode"] = "UF";
            dr["XCT_CustomerTypeCode"] = "IND";
            dr["XCT_CustomerTypeName"] = "IND";
            dr["ACSR_MinInvestmentAge"] = "0";
            dr["ACSR_MaxInvestmentAge"] = "0";
            dr["ACSR_TransactionType"] = "Normal";
            dr["ACSR_ServiceTaxValue"] = "0";
            dr["ACSR_ReducedValue"] = "0";
            dr["Normal"] = "TRXN";
            dr["ACG_CityGroupName"] = "T15";
            dr["ACG_CityGroupID"] = "1009";
            dr["ACSR_MinInvestmentAmount"] = "0";
            dr["ACSR_MaxInvestmentAmount"] = "10000000";
            dr["PaybleValue"] = "0";
            dr["RecievableValue"] = "0";
            dr["CSRD_IsUpdate"] = 0;
            dr["ACSR_ClawBackAge"] = "0";
            dr["ACSR_IsClaWback"] = 0;
            dr["ACSR_IsServiceTaxInclusive"] = "false";
            dr["ACSR_IsSBCApplicable"] = "false";
            dr["ACSR_IsKKCApplicable"] = "false";

            dr["ACSR_CommissionStructureRuleName"] = txtStructureName.Text.Substring(0, 5) + " " + "T15" + " " + "UF" + " " + "Normal";
            dttable.Rows.Add(dr);
            dr = null;
            dr = dttable.NewRow();
            dr["SNO."] = 2;
            dr["WCT_CommissionType"] = "UF";
            dr["WCT_CommissionTypeCode"] = "UF";
            dr["XCT_CustomerTypeCode"] = "IND";
            dr["XCT_CustomerTypeName"] = "IND";
            dr["ACSR_MinInvestmentAge"] = "0";
            dr["ACSR_MaxInvestmentAge"] = "0";
            dr["ACSR_TransactionType"] = "Normal";
            dr["ACSR_ServiceTaxValue"] = "0";
            dr["ACSR_ReducedValue"] = "0";
            dr["Normal"] = "TRXN";
            dr["ACG_CityGroupName"] = "B15";
            dr["ACG_CityGroupID"] = "1010";
            dr["ACSR_MinInvestmentAmount"] = "0";
            dr["ACSR_MaxInvestmentAmount"] = "10000000";
            dr["PaybleValue"] = "0";
            dr["RecievableValue"] = "0";
            dr["CSRD_IsUpdate"] = 0;
            dr["ACSR_ClawBackAge"] = "0";
            dr["ACSR_IsClaWback"] = 0;
            dr["ACSR_CommissionStructureRuleName"] = txtStructureName.Text.Substring(0, 5) + " " + "B15" + " " + "UF" + " " + "Normal";
            dr["ACSR_IsServiceTaxInclusive"] = "false";
            dr["ACSR_IsSBCApplicable"] = "false";
            dr["ACSR_IsKKCApplicable"] = "false";

            dttable.Rows.Add(dr);
            dr = null;
            dr = dttable.NewRow();
            dr["SNO."] = 3;
            dr["WCT_CommissionType"] = "TC";
            dr["WCT_CommissionTypeCode"] = "TC";
            dr["XCT_CustomerTypeCode"] = "IND";
            dr["XCT_CustomerTypeName"] = "IND";
            dr["ACSR_MinInvestmentAmount"] = "0";
            dr["ACSR_MaxInvestmentAmount"] = "0";
            dr["ACSR_MinInvestmentAge"] = "0";
            dr["ACSR_MaxInvestmentAge"] = "365";
            dr["ACSR_TransactionType"] = "Normal";
            dr["ACSR_ServiceTaxValue"] = "0";
            dr["ACSR_ReducedValue"] = "0";
            dr["Normal"] = "TRXN";
            dr["ACG_CityGroupName"] = "T15";
            dr["ACG_CityGroupID"] = "1009";
            dr["PaybleValue"] = "0";
            dr["RecievableValue"] = "0";
            dr["ACSR_InvestmentAgeUnit"] = "Days";
            dr["CSRD_IsUpdate"] = 0;
            dr["ACSR_ClawBackAge"] = "0";
            dr["ACSR_IsClaWback"] = 0;
            dr["ACSR_CommissionStructureRuleName"] = txtStructureName.Text.Substring(0, 5) + " " + "T15 1st Year" + " " + "Trail" + " " + "Normal";
            dr["ACSR_IsServiceTaxInclusive"] = "false";
            dr["ACSR_IsSBCApplicable"] = "false";
            dr["ACSR_IsKKCApplicable"] = "false";

            dttable.Rows.Add(dr);
            dr = null;
            dr = dttable.NewRow();
            dr["SNO."] = 4;
            dr["WCT_CommissionType"] = "TC";
            dr["WCT_CommissionTypeCode"] = "TC";
            dr["XCT_CustomerTypeCode"] = "IND";
            dr["XCT_CustomerTypeName"] = "IND";
            dr["ACSR_MinInvestmentAmount"] = "0";
            dr["ACSR_MaxInvestmentAmount"] = "0";
            dr["ACSR_MinInvestmentAge"] = "0";
            dr["ACSR_MaxInvestmentAge"] = "365";
            dr["ACSR_TransactionType"] = "Normal";
            dr["ACSR_ServiceTaxValue"] = "0";
            dr["ACSR_ReducedValue"] = "0";
            dr["Normal"] = "TRXN";
            dr["ACG_CityGroupName"] = "B15";
            dr["ACG_CityGroupID"] = "1010";
            dr["PaybleValue"] = "0";
            dr["RecievableValue"] = "0";
            dr["ACSR_InvestmentAgeUnit"] = "Days";
            dr["CSRD_IsUpdate"] = 0;
            dr["ACSR_ClawBackAge"] = "0";
            dr["ACSR_IsClaWback"] = 0;
            dr["ACSR_CommissionStructureRuleName"] = txtStructureName.Text.Substring(0, 5) + " " + "B15 1st Year" + " " + "Trail" + " " + "Normal";
            dr["ACSR_IsServiceTaxInclusive"] = "false";
            dr["ACSR_IsSBCApplicable"] = "false";
            dr["ACSR_IsKKCApplicable"] = "false";

            dttable.Rows.Add(dr);
            dr = null;
            dr = dttable.NewRow();
            dr["SNO."] = 5;
            dr["WCT_CommissionType"] = "TC";
            dr["WCT_CommissionTypeCode"] = "TC";
            dr["XCT_CustomerTypeCode"] = "IND";
            dr["XCT_CustomerTypeName"] = "IND";
            dr["ACSR_MinInvestmentAmount"] = "0";
            dr["ACSR_MaxInvestmentAmount"] = "0";
            dr["ACSR_MinInvestmentAge"] = "366";
            dr["ACSR_MaxInvestmentAge"] = "730";
            dr["ACSR_TransactionType"] = "Normal";
            dr["ACSR_ServiceTaxValue"] = "0";
            dr["ACSR_ReducedValue"] = "0";
            dr["Normal"] = "TRXN";
            dr["ACG_CityGroupName"] = "T15";
            dr["ACG_CityGroupID"] = "1009";
            dr["PaybleValue"] = "0";
            dr["RecievableValue"] = "0";
            dr["ACSR_InvestmentAgeUnit"] = "Days";
            dr["CSRD_IsUpdate"] = 0;
            dr["ACSR_ClawBackAge"] = "0";
            dr["ACSR_IsClaWback"] = 0;
            dr["ACSR_CommissionStructureRuleName"] = txtStructureName.Text.Substring(0, 5) + " " + "T15 2nd Year" + " " + "Trail" + " " + "Normal";
            dr["ACSR_IsServiceTaxInclusive"] = "false";
            dr["ACSR_IsSBCApplicable"] = "false";
            dr["ACSR_IsKKCApplicable"] = "false";
            dttable.Rows.Add(dr);
            dr = null;
            dr = dttable.NewRow();
            dr["SNO."] = 6;
            dr["WCT_CommissionType"] = "TC";
            dr["WCT_CommissionTypeCode"] = "TC";
            dr["XCT_CustomerTypeCode"] = "IND";
            dr["XCC_CustomerCategory"] = "IND";
            dr["ACSR_MinInvestmentAmount"] = "0";
            dr["ACSR_MaxInvestmentAmount"] = "0";
            dr["ACSR_MinInvestmentAge"] = "366";
            dr["ACSR_MaxInvestmentAge"] = "730";
            dr["ACSR_TransactionType"] = "Normal";
            dr["ACSR_ServiceTaxValue"] = "0";
            dr["ACSR_ReducedValue"] = "0";
            dr["Normal"] = "TRXN";
            dr["ACG_CityGroupName"] = "B15";
            dr["ACG_CityGroupID"] = "1010";
            dr["PaybleValue"] = "0";
            dr["RecievableValue"] = "0";
            dr["ACSR_InvestmentAgeUnit"] = "Days";
            dr["CSRD_IsUpdate"] = 0;
            dr["ACSR_ClawBackAge"] = "0";
            dr["ACSR_IsClaWback"] = 0;
            dr["ACSR_CommissionStructureRuleName"] = txtStructureName.Text.Substring(0, 5) + " " + "B15 2nd Year" + " " + "Trail" + " " + "Normal";
            dr["ACSR_IsServiceTaxInclusive"] = "false";
            dr["ACSR_IsSBCApplicable"] = "false";
            dr["ACSR_IsKKCApplicable"] = "false";

            dttable.Rows.Add(dr);
            dr = null;
            dr = dttable.NewRow();
            dr["SNO."] = 7;
            dr["WCT_CommissionType"] = "TC";
            dr["WCT_CommissionTypeCode"] = "TC";
            dr["XCT_CustomerTypeCode"] = "IND";
            dr["XCT_CustomerTypeName"] = "IND";
            dr["ACSR_MinInvestmentAmount"] = "0";
            dr["ACSR_MaxInvestmentAmount"] = "0";
            dr["ACSR_MinInvestmentAge"] = "730";
            dr["ACSR_MaxInvestmentAge"] = "3650";
            dr["ACSR_TransactionType"] = "Normal";
            dr["ACSR_ServiceTaxValue"] = "0";
            dr["ACSR_ReducedValue"] = "0";
            dr["Normal"] = "TRXN";
            dr["ACG_CityGroupName"] = "T15";
            dr["ACG_CityGroupID"] = "1009";
            dr["PaybleValue"] = "0";
            dr["RecievableValue"] = "0";
            dr["ACSR_InvestmentAgeUnit"] = "Days";
            dr["CSRD_IsUpdate"] = 0;
            dr["ACSR_ClawBackAge"] = "0";
            dr["ACSR_IsClaWback"] = 0;
            dr["ACSR_CommissionStructureRuleName"] = txtStructureName.Text.Substring(0, 5) + " " + "T15 3rd Year" + " " + "Trail" + " " + "Normal";
            dr["ACSR_IsServiceTaxInclusive"] = "false";
            dr["ACSR_IsSBCApplicable"] = "false";
            dr["ACSR_IsKKCApplicable"] = "false";

            dttable.Rows.Add(dr);
            dr = null;
            dr = dttable.NewRow();
            dr["SNO."] = 8;
            dr["WCT_CommissionType"] = "TC";
            dr["WCT_CommissionTypeCode"] = "TC";
            dr["XCT_CustomerTypeCode"] = "IND";
            dr["XCT_CustomerTypeName"] = "IND";
            dr["ACSR_MinInvestmentAmount"] = "0";
            dr["ACSR_MaxInvestmentAmount"] = "0";
            dr["ACSR_MinInvestmentAge"] = "730";
            dr["ACSR_MaxInvestmentAge"] = "3650";
            dr["ACSR_TransactionType"] = "Normal";
            dr["ACSR_ServiceTaxValue"] = "0";
            dr["ACSR_ReducedValue"] = "0";
            dr["Normal"] = "TRXN";
            dr["ACG_CityGroupName"] = "B15";
            dr["ACG_CityGroupID"] = "1010";
            dr["PaybleValue"] = "0";
            dr["RecievableValue"] = "0";
            dr["ACSR_InvestmentAgeUnit"] = "Days";
            dr["CSRD_IsUpdate"] = 0;
            dr["ACSR_ClawBackAge"] = "0";
            dr["ACSR_IsClaWback"] = 0;
            dr["ACSR_CommissionStructureRuleName"] = txtStructureName.Text.Substring(0, 5) + " " + "B15 3rd Year" + " " + "Trail" + " " + "Normal";
            dr["ACSR_IsServiceTaxInclusive"] = "false";
            dr["ACSR_IsSBCApplicable"] = "false";
            dr["ACSR_IsKKCApplicable"] = "false";

            dttable.Rows.Add(dr);

            return dttable;
        }
        private DataTable CreateCommissionStructureRuleDataTable()
        {
            DataTable dtCommissionStructureRule = new DataTable();
            dtCommissionStructureRule.Columns.Add("SNO.");
            dtCommissionStructureRule.Columns.Add("WCT_CommissionType");
            dtCommissionStructureRule.Columns.Add("XCT_CustomerTypeName");
            dtCommissionStructureRule.Columns.Add("XCC_CustomerCategory");
            dtCommissionStructureRule.Columns.Add("ACSR_MinInvestmentAmount");
            dtCommissionStructureRule.Columns.Add("ACSR_MaxInvestmentAmount");
            dtCommissionStructureRule.Columns.Add("ACSR_MinTenure");
            dtCommissionStructureRule.Columns.Add("ACSR_MaxTenure");
            dtCommissionStructureRule.Columns.Add("ACSR_TenureUnit");
            dtCommissionStructureRule.Columns.Add("ACSR_MinInvestmentAge");
            dtCommissionStructureRule.Columns.Add("ACSR_MaxInvestmentAge");
            dtCommissionStructureRule.Columns.Add("ACSR_InvestmentAgeUnit");
            dtCommissionStructureRule.Columns.Add("ACSR_TransactionType");
            dtCommissionStructureRule.Columns.Add("ACSR_MinNumberOfApplications");
            dtCommissionStructureRule.Columns.Add("ACSR_MaxNumberOfApplications");
            dtCommissionStructureRule.Columns.Add("ACSR_ReducedValue");
            dtCommissionStructureRule.Columns.Add("ACSR_ServiceTaxValue");
            dtCommissionStructureRule.Columns.Add("ACSR_BrokerageValue");
            dtCommissionStructureRule.Columns.Add("WCU_Unit");
            dtCommissionStructureRule.Columns.Add("WCCO_CalculatedOn");
            dtCommissionStructureRule.Columns.Add("ACSM_AUMFrequency");
            dtCommissionStructureRule.Columns.Add("ACSR_AUMMonth");
            dtCommissionStructureRule.Columns.Add("ACG_CityGroupName");
            dtCommissionStructureRule.Columns.Add("ACSR_Comment");
            dtCommissionStructureRule.Columns.Add("ASCR_WCMV_IncentiveType");
            dtCommissionStructureRule.Columns.Add("CO_ApplicationNo");
            dtCommissionStructureRule.Columns.Add("ACSR_ValidilityStart", typeof(DateTime));
            dtCommissionStructureRule.Columns.Add("ACSR_ValidilityEnd", typeof(DateTime));
            //dtCommissionStructureRule.Columns.Add("WCMV_Name");
            dtCommissionStructureRule.Columns.Add("ACSR_EForm");
            dtCommissionStructureRule.Columns.Add("AID_IssueDetailId");
            dtCommissionStructureRule.Columns.Add("Normal");
            dtCommissionStructureRule.Columns.Add("ACSR_CommissionStructureRuleName");
            dtCommissionStructureRule.Columns.Add("ACSR_CommissionStructureRuleId");
            dtCommissionStructureRule.Columns.Add("WCT_CommissionTypeCode");
            dtCommissionStructureRule.Columns.Add("XCT_CustomerTypeCode");
            dtCommissionStructureRule.Columns.Add("WCU_UnitCode");
            dtCommissionStructureRule.Columns.Add("WCCO_CalculatedOnCode");
            dtCommissionStructureRule.Columns.Add("ACSR_SIPFrequency");
            dtCommissionStructureRule.Columns.Add("ACG_CityGroupID");
            dtCommissionStructureRule.Columns.Add("ACSR_ReceivableRuleFrequency");
            dtCommissionStructureRule.Columns.Add("WCAL_ApplicableLevelCode");
            dtCommissionStructureRule.Columns.Add("ACSR_IsServiceTaxReduced");
            dtCommissionStructureRule.Columns.Add("ACSR_IsTDSReduced");
            dtCommissionStructureRule.Columns.Add("ACSM_IsOtherTaxReduced");
            dtCommissionStructureRule.Columns.Add("PaybleValue");
            dtCommissionStructureRule.Columns.Add("ReceivableValue");
            dtCommissionStructureRule.Columns.Add("ACSR_Mode");
            dtCommissionStructureRule.Columns.Add("AIIC_InvestorCatgeoryId");
            dtCommissionStructureRule.Columns.Add("RecievableValue");
            dtCommissionStructureRule.Columns.Add("WCU_UnitCode1");
            dtCommissionStructureRule.Columns.Add("CSRD_IsUpdate");
            dtCommissionStructureRule.Columns.Add("ACSR_IsClaWback");
            dtCommissionStructureRule.Columns.Add("ACSR_ClawBackAge");
            dtCommissionStructureRule.Columns.Add("ACSR_IsServiceTaxInclusive");
            dtCommissionStructureRule.Columns.Add("ACSR_IsSBCApplicable");
            dtCommissionStructureRule.Columns.Add("ACSR_IsKKCApplicable");
            dtCommissionStructureRule.Columns.Add("ACSR_SBCValue");
            dtCommissionStructureRule.Columns.Add("ACSR_KKCValue");
            return dtCommissionStructureRule;
        }


        protected void lnkDeleteAllRule_Click(object sender, EventArgs e)
        {
            commisionReceivableBo.DeleteCommissionStructureRule(Convert.ToInt32(hidCommissionStructureName.Value), true);
            BindCommissionStructureRuleBlankRow();
        }

        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            RadGridStructureRule.ExportSettings.OpenInNewWindow = true;
            RadGridStructureRule.ExportSettings.IgnorePaging = true;
            RadGridStructureRule.ExportSettings.HideStructureColumns = true;
            RadGridStructureRule.ExportSettings.ExportOnlyData = true;
            RadGridStructureRule.ExportSettings.FileName = "CommissionStructureRule";
            RadGridStructureRule.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            RadGridStructureRule.MasterTableView.ExportToExcel();
        }

        private bool ValidatePage(CommissionStructureMasterVo commissionStructureMasterVo)
        {
            bool isValid = false;
            if (commissionStructureRuleVo.CalculatedOnCode == "AGAM" || commissionStructureRuleVo.CalculatedOnCode == "AGAM" || commissionStructureRuleVo.CalculatedOnCode == "AGAM")
            {
                if (commissionStructureRuleVo.AUMMonth == 0)
                {
                    isValid = false;
                }
                else
                    isValid = true;

            }
            else
            {
                isValid = true;
            }
            return isValid;
        }


        private void CreateMappedSchemeGrid()
        {
            try
            {
                DataSet dsMappedSchemes = new DataSet();
                dsMappedSchemes = commisionReceivableBo.GetMappedSchemes(int.Parse(hidCommissionStructureName.Value));
                gvMappedSchemes.DataSource = dsMappedSchemes.Tables[0];
                gvMappedSchemes.DataBind();
                Cache.Insert(userVo.UserId.ToString() + "MappedSchemes", dsMappedSchemes.Tables[0]);
                pnlGrid.Visible = true;
                ibtExportSummary.Visible = true;
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
            //lblMappedSchemes.Text = "Schemes available to be mapped";
        }

        private void BindAvailSchemesToList()
        {
            int sStructId = int.Parse(hidCommissionStructureName.Value);
            int sIssuerId = 0;
            string sProduct = string.Empty, sCategory = string.Empty, sSubcats = string.Empty;
            try
            {

                if (ddlProductType.SelectedValue == "MF")
                    sIssuerId = int.Parse(ddlIssuer.SelectedValue);
                else
                    sIssuerId = int.Parse(hdnIssuerId.Value);

                sProduct = hdnProductId.Value;
                sCategory = ddlMFCategory.SelectedValue;
                sSubcats = "m";

                DataSet dsAvailSchemes = commisionReceivableBo.GetAvailSchemes(advisorVo.advisorId, sStructId, sIssuerId, sProduct, sCategory, sSubcats, Convert.ToDateTime(txtValidityFrom.Text), Convert.ToDateTime(txtValidityTo.Text));
                rlbAvailSchemes.DataSource = dsAvailSchemes.Tables[0];
                rlbAvailSchemes.DataValueField = dsAvailSchemes.Tables[0].Columns["PASP_SchemePlanCode"].ToString();
                rlbAvailSchemes.DataTextField = dsAvailSchemes.Tables[0].Columns["PASP_SchemePlanName"].ToString();
                rlbAvailSchemes.DataBind();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionStructureToSchemeMapping.ascx.cs:BindAvailSchemesToList(advisorVo.advisorId, sStructId, sIssuerId, sProduct, sCategory, sSubcats, Convert.ToDateTime(txtValidityFrom.Text), Convert.ToDateTime(txtValidityTo.Text))");
                object[] objects = new object[5];
                objects[0] = sStructId;
                objects[1] = sIssuerId;
                objects[2] = sProduct;
                objects[3] = sCategory;
                objects[4] = sSubcats;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
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
        }

        private void SetFetchSchemesDatePickControls()
        {

        }

        protected void btnAddNewSchemes_Click(object sender, EventArgs e)
        {
            SetFetchSchemesDatePickControls();
            pnlAddSchemes.Visible = true;
        }

        protected void ListBoxSource_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
        }

        private void SetMappedSchemesDatePicker()
        {
            rdpMappedFrom.MinDate = DateTime.Parse(hdnStructValidFrom.Value.ToString());
            rdpMappedFrom.MaxDate = DateTime.Parse(hdnStructValidTill.Value.ToString()).AddDays(-1);
            rdpMappedFrom.SelectedDate = rdpMappedFrom.MinDate;

            rdpMappedTill.MinDate = DateTime.Parse(hdnStructValidFrom.Value.ToString()).AddDays(1);
            rdpMappedTill.MaxDate = DateTime.Parse(hdnStructValidTill.Value.ToString());
            rdpMappedTill.SelectedDate = rdpMappedTill.MaxDate;
        }

        protected void btn_GetAvailableSchemes_Click(object sender, EventArgs e)
        {
            tbSchemeMapping.Visible = true;
            tbSchemeMapped.Visible = true;
            rlbAvailSchemes.Items.Clear();
            rlbMappedSchemes.Items.Clear();
            BindAvailSchemesToList();
            BindMappedSchemesToList();
        }

        protected void rlbAvailSchemes_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
        }

        protected void rlbMappedSchemes_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
            //lblMappedSchemes.Text = "Mapped Schemes(" + rlbMappedSchemes.Items.Count.ToString() + ")";
        }

        private void MapSchemesToStructure()
        {
            if (rlbMappedSchemes.Items.Count < 1)
                return;
            List<int> schemeIds = new List<int>();

            bool mapOk = true;
            int structId = int.Parse(hidCommissionStructureName.Value);
            foreach (RadListBoxItem item in rlbMappedSchemes.Items)
            {
                int schemeId = int.Parse(item.Value);
                if (commisionReceivableBo.checkSchemeAssociationExists(schemeId, structId, Convert.ToDateTime(txtValidityFrom.Text), Convert.ToDateTime(txtValidityTo.Text), ddlMFCategory.SelectedValue))
                {
                    mapOk = false;
                    break;
                }
            }

            if (!mapOk)
            {
                showMapError();
                return;
            }
            foreach (RadListBoxItem item in rlbMappedSchemes.Items)
            {
                try
                {
                    commisionReceivableBo.MapSchemesToStructres(structId, int.Parse(item.Value), Convert.ToDateTime(txtValidityFrom.Text), Convert.ToDateTime(txtValidityTo.Text));
                }
                catch (BaseApplicationException Ex)
                {
                    throw Ex;
                }
                catch (Exception Ex)
                {
                    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                    NameValueCollection FunctionInfo = new NameValueCollection();
                    FunctionInfo.Add("Method", "ReceivableSetup.cs:MapSchemesToStructres(int structId, int schemeId, DateTime validFrom, DateTime validTill)");
                    object[] objects = new object[4];
                    objects[0] = structId;
                    objects[1] = item.Value;
                    objects[2] = Convert.ToDateTime(txtValidityFrom.Text);
                    objects[3] = Convert.ToDateTime(txtValidityTo.Text);
                    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                    exBase.AdditionalInformation = FunctionInfo;
                    ExceptionManager.Publish(exBase);
                    throw exBase;
                }
            }
        }

        private void showMapError()
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Scheme mapping could not be performed');", true);
        }

        protected void btnMapSchemes_Click(object sender, EventArgs e)
        {
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('ReceivableSetup','StructureId=" + this.hidCommissionStructureName.Value + "');", true);
        }

        protected void gvMappedSchemes_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = (GridEditableItem)e.Item;
            int setupId = int.Parse(item.GetDataKeyValue("ACSTSM_SetupId").ToString());
            DateTime oldDate = DateTime.Parse(item.SavedOldValues["ValidTill"].ToString());
            DateTime newDate = ((RadDatePicker)item["schemeValidTill"].Controls[0]).SelectedDate.Value;
            int retVal = commisionReceivableBo.updateStructureToSchemeMapping(setupId, newDate);
            if (retVal < 1) { return; }

            CreateMappedSchemeGrid();
        }

        protected void gvMappedSchemes_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = (GridEditableItem)e.Item;
            int setupId = int.Parse(item.GetDataKeyValue("ACSTSM_SetupId").ToString());
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
            gvMappedSchemes.ExportSettings.OpenInNewWindow = true;
            gvMappedSchemes.ExportSettings.IgnorePaging = true;
            gvMappedSchemes.ExportSettings.HideStructureColumns = true;
            gvMappedSchemes.ExportSettings.ExportOnlyData = true;
            gvMappedSchemes.ExportSettings.FileName = "MappedSchemes";
            gvMappedSchemes.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvMappedSchemes.MasterTableView.ExportToExcel();



        }

        private void ShowHideControlsForRulesBasedOnProduct(bool flag, GridItemEventArgs e)
        {
            GridEditFormItem editform = (GridEditFormItem)e.Item;
            Label lblInvestorType1 = (Label)editform.FindControl("lblInvestorType");
            Label lblAppCityGroup = (Label)editform.FindControl("lblAppCityGroup");
            Label lblReceivableFrequency = (Label)editform.FindControl("lblReceivableFrequency");
            Label lbltransaction = (Label)editform.FindControl("lbltransaction");
            DropDownList ddlTransaction = (DropDownList)editform.FindControl("ddlTransaction");
            DropDownList ddlReceivableFrequency = (DropDownList)editform.FindControl("ddlReceivableFrequency");
            DropDownList ddlAppCityGroup = (DropDownList)editform.FindControl("ddlAppCityGroup");
            DropDownList ddlInvestorType = (DropDownList)editform.FindControl("ddlInvestorType");
            System.Web.UI.HtmlControls.HtmlTableRow trTransactionTypeSipFreq = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trTransactionTypeSipFreq");
            System.Web.UI.HtmlControls.HtmlTableRow trMinMaxTenure = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trMinMaxTenure");
            System.Web.UI.HtmlControls.HtmlTableRow trMinMaxAge = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trMinMaxAge");

            System.Web.UI.HtmlControls.HtmlTableCell tdlblApplicationNo = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdlblApplicationNo");
            System.Web.UI.HtmlControls.HtmlTableCell tdApplicationNo = (System.Web.UI.HtmlControls.HtmlTableCell)editform.FindControl("tdApplicationNo");
            System.Web.UI.HtmlControls.HtmlTableCell tdddlSeries = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdddlSeries");
            System.Web.UI.HtmlControls.HtmlTableCell tdlblSerise = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdlblSerise");
            CheckBox chkCategory = (CheckBox)e.Item.FindControl("chkCategory");
            CheckBox chkSeries = (CheckBox)e.Item.FindControl("chkSeries");
            if (ddlProductType.SelectedValue == "MF")
            {
                tdddlSeries.Visible = false;
                tdlblSerise.Visible = false;
                chkCategory.Visible = false;
                chkSeries.Visible = false;
                tdlblApplicationNo.Visible = false;
                tdApplicationNo.Visible = false;
            }
            lblInvestorType1.Visible = flag;
            lblAppCityGroup.Visible = flag;
            lblReceivableFrequency.Visible = flag;
            ddlReceivableFrequency.Visible = flag;
            ddlAppCityGroup.Visible = flag;
            ddlInvestorType.Visible = flag;
            trTransactionTypeSipFreq.Visible = flag;

            trMinMaxTenure.Visible = flag;
            trMinMaxAge.Visible = flag;
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

        private void SetStructureDetails()
        {
            DataSet dsStructDet;
            try
            {
                dsStructDet = commisionReceivableBo.GetStructureDetails(advisorVo.advisorId, int.Parse(hidCommissionStructureName.Value));
                foreach (DataRow row in dsStructDet.Tables[0].Rows)
                {
                    hdnProductId.Value = row["PAG_AssetGroupCode"].ToString();
                    hdnStructValidFrom.Value = row["ACSM_ValidityStartDate"].ToString();
                    hdnStructValidTill.Value = row["ACSM_ValidityEndDate"].ToString();
                    hdnIssuerId.Value = row["PA_AMCCode"].ToString();
                    hdnCategoryId.Value = row["PAIC_AssetInstrumentCategoryCode"].ToString();
                    ddlMapping.SelectedValue = row["UserType"].ToString();
                    if (ddlMapping.SelectedValue == "Associate")
                    {
                        ddlType.SelectedValue = "UserCategory";
                    }
                    else
                    {
                        ddlType.SelectedValue = "Custom";

                    }
                    GetControlsBasedOnType(ddlType.SelectedValue);
                    if (!string.IsNullOrEmpty(row["AC_CategoryId"].ToString()))
                    {
                        ddlAdviserCategory.SelectedValue = row["AC_CategoryId"].ToString();
                    }
                }


                //Getting the list of subcategories
                dsStructDet = commisionReceivableBo.GetSubcategories(advisorVo.advisorId, int.Parse(hidCommissionStructureName.Value));
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


        public void GetMapped_Unmapped_Issues(string type, string issueType)
        {
            DataTable dtmappedIssues = new DataTable();
            ddlUnMappedIssues.Items.Clear();
            ddlUnMappedIssues.DataBind();
            string product = string.Empty;
            int structureId = 0;
            product = hdnProductId.Value.ToString();
            structureId = Convert.ToInt32(hidCommissionStructureName.Value);
            ddlSubInstrCategory.SelectedValue = hdnSubcategoryIds.Value;


            if (type == "Mapped")
            {
                dtmappedIssues = commisionReceivableBo.GetIssuesStructureMapings(advisorVo.advisorId, type, issueType, product, 0, structureId, (ddlSubInstrCategory.SelectedValue == "" || ddlSubInstrCategory.SelectedValue == "Select") ? "FIFIIP" : ddlSubInstrCategory.SelectedValue).Tables[0];

                gvMappedIssueList.DataSource = dtmappedIssues;
                gvMappedIssueList.DataBind();
                pnlIssueList.Visible = true;
                if (Cache[userVo.UserId.ToString() + "MappedIssueList"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "MappedIssueList");
                Cache.Insert(userVo.UserId.ToString() + "MappedIssueList", dtmappedIssues);
                if (Request.QueryString["StructureId"] != null)
                {
                    ddlUnMappedIssues.DataSource = dtmappedIssues;
                    ddlUnMappedIssues.DataTextField = "AIM_IssueName";
                    ddlUnMappedIssues.DataValueField = "AIM_IssueId";
                    ddlUnMappedIssues.DataBind();
                    ddlUnMappedIssues.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                }
            }
            else if (type == "UnMapped")
            {
                dtmappedIssues = commisionReceivableBo.GetIssuesStructureMapings(advisorVo.advisorId, type, issueType, product, 0, structureId, (ddlSubInstrCategory.SelectedValue == "" || ddlSubInstrCategory.SelectedValue == "Select") ? "FIFIIP" : ddlSubInstrCategory.SelectedValue).Tables[0];

                ddlUnMappedIssues.DataSource = dtmappedIssues;
                ddlUnMappedIssues.DataTextField = "AIM_IssueName";
                ddlUnMappedIssues.DataValueField = "AIM_IssueId";
                ddlUnMappedIssues.DataBind();
                ddlUnMappedIssues.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
        }


        private void GetUnamppedIssues(string issueType)
        {
            GetMapped_Unmapped_Issues("UnMapped", issueType);

        }

        protected void ddlIssueType_Selectedindexchanged(object sender, EventArgs e)
        {
            GetUnamppedIssues(ddlIssueType.SelectedValue);

        }
        protected void gvMappedIssueList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtIssues = (DataTable)Cache[userVo.UserId.ToString() + "MappedIssueList"];
            if (dtIssues != null) gvMappedIssueList.DataSource = dtIssues;

        }

        protected void gvMappedIssueList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                if (gvMappedIssueList.Items.Count > 0)
                {
                    int setupId = Convert.ToInt32(gvMappedIssueList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSTSM_SetupId"].ToString());
                    commisionReceivableBo.DeleteIssueMapping(setupId);
                    GetUnamppedIssues(ddlIssueType.SelectedValue);
                    GetMapped_Unmapped_Issues("Mapped", "");

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please remove Mapping then try.');", true);
                    GetMapped_Unmapped_Issues("Mapped", "");

                    return;
                }
            }
        }

        protected void btnMAP_Click(object sender, EventArgs e)
        {

            int mappingId;
            int resultMapping;
            if (string.IsNullOrEmpty(ddlUnMappedIssues.SelectedValue))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select Issue');", true);
                return;
            }

            resultMapping = commisionReceivableBo.IssueMappingDuplicateChecks(Convert.ToInt32(ddlUnMappedIssues.SelectedValue), Convert.ToDateTime(txtValidityFrom.Text), Convert.ToDateTime(txtValidityTo.Text), Convert.ToInt32(hidCommissionStructureName.Value));
            if (resultMapping > 0)
            {
                // tbNcdIssueList.Visible
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Issue already mapped to Other structure within validity period');", true);
                return;
            }

            commissionStructureRuleVo.CommissionStructureId = Convert.ToInt32(hidCommissionStructureName.Value);

            commissionStructureRuleVo.IssueId = Convert.ToInt32(ddlUnMappedIssues.SelectedValue);
            commisionReceivableBo.CreateIssuesStructureMapings(commissionStructureRuleVo, out mappingId);
            GetUnamppedIssues(ddlIssueType.SelectedValue);
            GetMapped_Unmapped_Issues("Mapped", "");

        }
        protected void rgPayableMapping_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {

            }
        }

        protected void Map_btnIssueMap(object sender, EventArgs e)
        {
            int associateid = 0;
            DefaultAssignments();
            RadListBoxSelectedAgentCodes.Items.Clear();
            BindPayableGridMapping(int.Parse(hidCommissionStructureName.Value));
            if (Request.QueryString["StructureId"] != null)
            {
                associateid = commisionReceivableBo.RuleAssociate(Request.QueryString["StructureId"]);
                if (associateid > 0)
                {

                    string confirmValue = "Yes";
                    if (confirmValue == "Yes")
                    {

                        radAplicationPopUp.VisibleOnPageLoad = true;
                    }
                }
                else
                {
                    radAplicationPopUp.VisibleOnPageLoad = true;
                }
            }
            else
            {
                if (gvPayaMapping.Items.Count > 0)
                {

                    string confirmValue = "Yes";// Request.Form["confirm_value"];
                    if (confirmValue == "Yes")
                    {
                        radAplicationPopUp.VisibleOnPageLoad = true;

                    }
                }
                else
                {
                    radAplicationPopUp.VisibleOnPageLoad = true;
                }
            }

        }
        protected void MapStructure()
        {
        }
        public bool CheckRuleDate(GridCommandEventArgs e)
        {
            TextBox txtRuleValidityFrom = (TextBox)e.Item.FindControl("txtRuleValidityFrom");
            TextBox txtRuleValidityTo = (TextBox)e.Item.FindControl("txtRuleValidityTo");
            bool result = true;
            DateTime start = Convert.ToDateTime(txtValidityFrom.Text);
            DateTime end = Convert.ToDateTime(txtValidityTo.Text);
            if (!(start >= Convert.ToDateTime(hdnStructValidFrom.Value) && end <= Convert.ToDateTime(hdnStructValidTill.Value)))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select rule start and rule end date as on structure date');", true);
                result = false;
            }
            return result;
        }
        protected void ddlInvestorType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridEditFormItem RadGridStructureRule = (GridEditFormItem)ddl.NamingContainer;
            DropDownList ddlIncentiveType = (DropDownList)RadGridStructureRule.FindControl("ddlIncentiveType");
            DropDownList ddlCommisionCalOn = (DropDownList)RadGridStructureRule.FindControl("ddlCommisionCalOn");
            TextBox txtMinInvestmentAmount = (TextBox)RadGridStructureRule.FindControl("txtMinInvestmentAmount");
            TextBox txtMaxInvestmentAmount = (TextBox)RadGridStructureRule.FindControl("txtMaxInvestmentAmount");
            TextBox txtMinNumberOfApplication = (TextBox)RadGridStructureRule.FindControl("txtMinNumberOfApplication");
            TextBox txtMaxNumberOfApplication = (TextBox)RadGridStructureRule.FindControl("txtMaxNumberOfApplication");
            txtMinInvestmentAmount.Text = string.Empty;
            txtMaxInvestmentAmount.Text = string.Empty;
            txtMinNumberOfApplication.Text = string.Empty;
            txtMaxNumberOfApplication.Text = string.Empty;
            ddlCommisionCalOn.Items[1].Enabled = false;
            ddlCommisionCalOn.Items[2].Enabled = false;
            ddlCommisionCalOn.Items[3].Enabled = false;
            ddlCommisionCalOn.Items[4].Enabled = false;
            if (ddlIncentiveType.SelectedValue == "N")
            {
                ddlCommisionCalOn.Items[3].Enabled = true;
                ddlCommisionCalOn.Items[4].Enabled = true;

            }
            else if (ddlIncentiveType.SelectedValue == "SPl")
            {
                //ddlCommisionCalOn.Items[1].Enabled = true;
                ddlCommisionCalOn.Items[3].Enabled = true;
                ddlCommisionCalOn.Items[2].Enabled = true;
            }
            else if (ddlIncentiveType.SelectedValue == "MBl")
            {
                ddlCommisionCalOn.Items[3].Enabled = true;
                ddlCommisionCalOn.Items[1].Enabled = true;
            }
        }
        protected void gvPayaMapping_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int agentId;
                int ruleDetailId = Convert.ToInt32(gvPayaMapping.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CSRD_StructureRuleDetailsId"].ToString());
                if (!string.IsNullOrEmpty(gvPayaMapping.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AAC_AdviserAgentId"].ToString()))
                    agentId = Convert.ToInt32(gvPayaMapping.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AAC_AdviserAgentId"].ToString());
                else
                    agentId = 0;
                string category = gvPayaMapping.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AC_Category"].ToString();

                int result = commisionReceivableBo.DeleteStaffAndAssociateMapping(ruleDetailId, agentId, category);
                BindPayableGrid();
                if (result > 0)
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Mapping deleted successfully');", true);
            }
        }
        protected void gvPayaMapping_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet dsPayable = new DataSet();
            if (Cache[userVo.UserId.ToString() + "CommissionPayable"] != null)
            {
                dsPayable = (DataSet)Cache[userVo.UserId.ToString() + "CommissionPayable"];
                gvPayaMapping.DataSource = dsPayable.Tables[0];
            }
        }
        private void BindPayableGrid()
        {

            string structureId = hidCommissionStructureName.Value;

            DataSet dsPayable = new DataSet();
            dsPayable = commisionReceivableBo.GetPayableMappings(int.Parse(structureId));
            if (dsPayable.Tables[0].Rows.Count > 0)
            {
                hdneligible.Value = "Eligible";
                hdnViewMode.Value = "ViewEdit";
                btnCancelSelectedRule.Visible = true;
            }
            else
            {
                hdneligible.Value = "";
                hdnViewMode.Value = "";
                btnCancelSelectedRule.Visible = true;

            }
            gvPayaMapping.DataSource = dsPayable;
            gvPayaMapping.DataBind();
            gvPayaMapping.Visible = true;
            ImageButton6.Visible = true;
            if (Cache[userVo.UserId.ToString() + "CommissionPayable"] != null)
                Cache.Remove(userVo.UserId.ToString() + "CommissionPayable");
            Cache.Insert(userVo.UserId.ToString() + "CommissionPayable", dsPayable);

        }
        protected void OnClick_imgMapping(object sender, ImageClickEventArgs e)
        {


        }
        protected void chkCategory_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkCategory = (CheckBox)sender;
            GridEditFormItem gdi = (GridEditFormItem)chkCategory.NamingContainer;
            DropDownList ddl = (DropDownList)gdi.FindControl("ddlCategorys");
            CheckBox chkSeries = (CheckBox)gdi.FindControl("chkSeries");
            System.Web.UI.HtmlControls.HtmlTableCell tdlblCategory = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlblCategory");
            System.Web.UI.HtmlControls.HtmlTableCell tdddlCategorys = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdddlCategorys");
            System.Web.UI.HtmlControls.HtmlTableCell tdlblSerise = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlblSerise");
            System.Web.UI.HtmlControls.HtmlTableCell tdddlSeries = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdddlSeries");
            if (chkCategory.Checked == true)
            {

                tdlblCategory.Visible = true;
                tdddlCategorys.Visible = true;

                BindCategory(ddl, int.Parse(gvMappedIssueList.MasterTableView.DataKeyValues[0]["AIM_IssueId"].ToString()));
                if (ddlProductType.SelectedValue != "IP")
                {
                    chkSeries.Checked = true;
                    chkSeries.Enabled = false;
                    tdlblSerise.Visible = true;
                    tdddlSeries.Visible = true;
                }
            }
            else
            {
                chkSeries.Checked = false;
                chkSeries.Enabled = true;
                tdlblCategory.Visible = false;
                tdddlCategorys.Visible = false;
                tdlblSerise.Visible = false;
                tdddlSeries.Visible = false;
            }

        }
        protected void chkSeries_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSeries = (CheckBox)sender;
            GridEditFormItem gdi = (GridEditFormItem)chkSeries.NamingContainer;
            DropDownList dd = (DropDownList)gdi.FindControl("ddlSeries");
            System.Web.UI.HtmlControls.HtmlTableCell tdlblSerise = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlblSerise");
            System.Web.UI.HtmlControls.HtmlTableCell tdddlSeries = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdddlSeries");
            if (chkSeries.Checked == true)
            {
                tdlblSerise.Visible = true;
                tdddlSeries.Visible = true;
                BindSeries(dd, int.Parse(gvMappedIssueList.MasterTableView.DataKeyValues[0]["AIM_IssueId"].ToString()), 0);
            }
            else
            {
                tdlblSerise.Visible = false;
                tdddlSeries.Visible = false;
            }

        }
        protected void chkMode_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkMode = (CheckBox)sender;
            GridEditFormItem gdi = (GridEditFormItem)chkMode.NamingContainer;
            System.Web.UI.HtmlControls.HtmlTableCell tdlblMode = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlblMode");
            System.Web.UI.HtmlControls.HtmlTableCell tdddlMode = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdddlMode");
            if (chkMode.Checked == true)
            {
                tdlblMode.Visible = false;
                tdddlMode.Visible = false;
            }
            else
            {
                tdlblMode.Visible = false;
                tdddlMode.Visible = false;
            }
        }

        protected void ddlCategorys_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList ddlCategorys = (DropDownList)sender;
            GridEditFormItem gdi = (GridEditFormItem)ddlCategorys.NamingContainer;
            DropDownList ddlCategorys1 = (DropDownList)gdi.FindControl("ddlCategorys");
            DropDownList ddlSeries = (DropDownList)gdi.FindControl("ddlSeries");
            BindSeries(ddlSeries, 0, int.Parse(ddlCategorys1.SelectedValue));
            //}
        }
        protected void ddlSeries_OnSelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void BindCategory(DropDownList ddlCategory, int issueId)
        {
            DataTable dtCategory = commisionReceivableBo.GetCategory(issueId);
            ddlCategory.DataSource = dtCategory;
            ddlCategory.DataTextField = "AIIC_InvestorCatgeoryName";
            ddlCategory.DataValueField = "AIIC_InvestorCatgeoryId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }
        protected void BindSeries(DropDownList ddlSeries, int issueId, int categoryId)
        {
            DataTable dtSeries = commisionReceivableBo.GetSeriese(issueId, categoryId);
            ddlSeries.DataSource = dtSeries;
            ddlSeries.DataTextField = "AID_IssueDetailName";
            ddlSeries.DataValueField = "AID_IssueDetailId";
            ddlSeries.DataBind();
            ddlSeries.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }
        protected void ddlSubInstrCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSubInstrCategory = (DropDownList)sender;
        }
        protected void BindBrokerCode(DropDownList ddlBrokerCode, int issueId)
        {
            FIOrderBo fiorderBo = new FIOrderBo();
            DataTable dtBindSubbroker = fiorderBo.GetSubBroker(issueId);

            ddlBrokerCode.DataSource = dtBindSubbroker;
            ddlBrokerCode.DataValueField = dtBindSubbroker.Columns["XB_BrokerIdentifier"].ToString();
            ddlBrokerCode.DataTextField = dtBindSubbroker.Columns["XB_BrokerShortName"].ToString();
            ddlBrokerCode.DataBind();
            ddlBrokerCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }
        protected void ddlCommissionype_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void ibtExport_OnClick(object sender, ImageClickEventArgs e)
        {
            //  gvIPOOrderBook.MasterTableView.DetailTables[0].HierarchyDefaultExpanded = true;
            gvPayaMapping.MasterTableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
            gvPayaMapping.ExportSettings.OpenInNewWindow = true;
            gvPayaMapping.ExportSettings.IgnorePaging = true;
            gvPayaMapping.ExportSettings.HideStructureColumns = true;
            gvPayaMapping.ExportSettings.ExportOnlyData = true;
            gvPayaMapping.ExportSettings.FileName = "Associate Payable List";
            gvPayaMapping.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvPayaMapping.MasterTableView.ExportToExcel();

        }
        protected void btnGo_Click(object sender, EventArgs e)
        {

            int mappingId = CreatePayableMapping();
            if (mappingId > 0)
            {
                RadListBoxSelectedAgentCodes.Items.Clear();
                BindPayableGrid();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Mapping Created SuccessFully');", true);
                radAplicationPopUp.VisibleOnPageLoad = false;
                RadListBoxSelectedAgentCodes.Items.Clear();
                //BindPayableGrid(int.Parse(hdnStructId.Value));
            }

        }
        private int CreatePayableMapping()
        {
            StringBuilder sbAgentid = new StringBuilder();
            StringBuilder sbRuleid = new StringBuilder();
            string ruleId = string.Empty;
            DataTable dtRuleMapping = new DataTable();

            try
            {

                foreach (GridDataItem gdi in rgPayableMapping.MasterTableView.Items)
                {

                    CheckBoxList chkListrate = (CheckBoxList)gdi.FindControl("chkListrate");
                    for (int i = 0; i < chkListrate.Items.Count; i++)
                    {
                        if (chkListrate.Items[i].Selected)
                        {
                            //Storing the selected values
                            ruleId = ruleId + "," + chkListrate.Items[i].Value;
                        }
                    }
                }
                if (ruleId != "")
                {
                    ruleId = ruleId.Trim(',');
                    ruleId = ruleId.TrimEnd(',');

                    dtRuleMapping.Columns.Add("agentId", typeof(string));
                    dtRuleMapping.Columns.Add("ruleids");
                    DataRow drRuleMapping;
                    int mappingId = 0;
                    string agentId = string.Empty;
                    string categoryId = string.Empty;
                    if (ddlType.SelectedValue == "Custom")
                    {
                        foreach (RadListBoxItem ListItem in this.RadListBoxSelectedAgentCodes.Items)
                        {

                            agentId = ListItem.Value;
                            sbAgentid.Append(ListItem.Value);
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

                    commisionReceivableBo.CreateAdviserPayableRuleToAgentCategoryMapping(Convert.ToInt32(hidCommissionStructureName.Value), ddlMapping.SelectedValue, categoryId, dtRuleMapping, ruleId.TrimEnd(','), int.Parse(ddlAMFIAvaliable.SelectedValue), out mappingId);
                    radAplicationPopUp.VisibleOnPageLoad = true;
                    return mappingId;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select rate(%)');", true);
                    radAplicationPopUp.VisibleOnPageLoad = true;
                    return 0;

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
                FunctionInfo.Add("Method", "CommissionStructureToSchemeMapping.ascx.cs:CreatePayableMapping()");
                object[] objects = new object[8];
                objects[0] = Convert.ToInt32(hidCommissionStructureName.Value);
                objects[1] = ddlMapping.SelectedValue;
                objects[2] = categoryId;
                objects[3] = dtRuleMapping;
                objects[4] = ruleId.TrimEnd(',');
                objects[5] = ddlAMFIAvaliable.SelectedValue;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
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
        private void BindPayableGridMapping(int structureId)
        {
            DataSet dsLookupData;
            dsLookupData = commisionReceivableBo.GetPayableCommissionTypeBrokerage(structureId);
            ViewState["dsrate"] = dsLookupData;
            rgPayableMapping.DataSource = dsLookupData;
            rgPayableMapping.DataBind();
            rgPayableMapping.Visible = true;
            if (Cache[userVo.UserId.ToString() + "RulePayableDet"] != null)
                Cache.Remove(userVo.UserId.ToString() + "RulePayableDet");
            Cache.Insert(userVo.UserId.ToString() + "RulePayableDet", dsLookupData.Tables[0]);
        }
        protected void rgPayableMapping_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            //{
            //}
            DataSet dsratelist = (DataSet)ViewState["dsrate"];
            if (e.Item is GridDataItem)
            {
                CheckBoxList chkListrate = e.Item.FindControl("chkListrate") as CheckBoxList;
                //RadioButtonList rbtnListRate = e.Item.FindControl("rbtnListRate") as RadioButtonList;
                int ruleId = int.Parse(rgPayableMapping.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_CommissionStructureRuleId"].ToString());

                DataView dv = dsratelist.Tables[1].DefaultView;
                dv.RowFilter = "ACSR_CommissionStructureRuleId = '" + ruleId.ToString() + "'";
                if (chkListrate != null)
                {

                    chkListrate.DataSource = dv;
                    chkListrate.DataValueField = "CSRD_StructureRuleDetailsId";
                    chkListrate.DataTextField = "CSRD_BrokageValue";
                    chkListrate.DataBind();
                }
            }
        }
        private void DefaultAssignments()
        {
            ddlMapping.SelectedValue = "Associate";
            ddlType.SelectedValue = "Custom";
            ddlAMFIAvaliable.SelectedValue = "0";
            GetControlsBasedOnType(ddlType.SelectedValue);
        }
        protected void ddlAMFIAvaliable_Selectedindexchanged(object sender, EventArgs e)
        {
            //BindAgentCodes();
            radAplicationPopUp.VisibleOnPageLoad = true;
        }
        protected void ddlType_Selectedindexchanged(object sender, EventArgs e)
        {
            radAplicationPopUp.VisibleOnPageLoad = true;
            GetControlsBasedOnType(ddlType.SelectedValue);
        }

        protected void ddlMapping_Selectedindexchanged(object sender, EventArgs e)
        {
            radAplicationPopUp.VisibleOnPageLoad = true;
            SelectionsBasedOnMappingFor();
            GetControlsBasedOnType(ddlType.SelectedValue);
        }

        private void SelectionsBasedOnMappingFor()
        {
            if (ddlMapping.SelectedValue == "Staff")
            {
                ddlType.SelectedValue = "Custom";
                ddlType.Enabled = false;
                lblAssetCategory.Visible = false;
                ddlAdviserCategory.Visible = false;
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
                lblAssetCategory.Visible = true;
                ddlAdviserCategory.Visible = true;


            }
        }


        private void GetControlsBasedOnType(string type)
        {
            if (type == "Custom")
            {
                trListControls.Visible = true;
                //trAssetCategory.Visible = false;
                lblAssetCategory.Visible = false;
                ddlAdviserCategory.Visible = false;
                //BindAgentCodes();
                ddlType.SelectedValue = "Custom";

            }
            else
            {
                trListControls.Visible = false;
                //trAssetCategory.Visible = true;
                lblAssetCategory.Visible = true;
                ddlAdviserCategory.Visible = true;
                BindClassification();
            }
        }
        protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
        {
            if (gvPayaMapping.Visible == true)
            {
                gvPayaMapping.Visible = false;
                ImageButton7.ImageUrl = "~/Images/toggle-expand-alt_blue.png";
                ImageButton7.ToolTip = "Expend";
                btnIssueMap.Visible = false;
            }
            else
            {
                gvPayaMapping.Visible = true;
                ImageButton7.ImageUrl = "~/Images/toggle-collapse-alt_blue.png";
                ImageButton7.ToolTip = "Collapse";
                btnIssueMap.Visible = true;
            }
        }
        protected void chkListrate_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            radAplicationPopUp.VisibleOnPageLoad = true;
            int count = 0;
            foreach (GridDataItem gdi in rgPayableMapping.MasterTableView.Items)
            {
                CheckBoxList chkListrate = (CheckBoxList)gdi.FindControl("chkListrate");
                for (int i = 0; i < chkListrate.Items.Count; i++)
                {
                    if (chkListrate.Items[i].Selected)
                    {
                        //Storing the selected values
                        chkListrate.Enabled = false;
                        btnCancelSelectedRule.Visible = true;
                        MappedruleId = MappedruleId + "," + chkListrate.Items[i].Value;
                        count++;
                    }
                    //if (hdneligible.Value != "")
                    //{
                    chkListrate.Enabled = false;

                    //}
                }
            }

            BindMAppedStaffList();
            BindStaffListAssociate();
        }
        protected void BindMAppedStaffList()
        {
            RadListBoxSelectedAgentCodes.Items.Clear();
            DataSet dsAdviserBranchList = new DataSet();
            dsAdviserBranchList = commisionReceivableBo.GetAdviserAgentMappedCodes(advisorVo.advisorId, ddlMapping.SelectedValue, int.Parse(ddlAMFIAvaliable.SelectedValue), MappedruleId.TrimStart(','));
            RadListBoxSelectedAgentCodes.DataSource = dsAdviserBranchList;
            RadListBoxSelectedAgentCodes.DataValueField = "AgentId";
            RadListBoxSelectedAgentCodes.DataTextField = "AgentCodeWithName";
            RadListBoxSelectedAgentCodes.DataBind();
        }
        protected void BindStaffListAssociate()
        {
            DataSet dsAdviserBranchList = new DataSet();
            dsAdviserBranchList = commisionReceivableBo.GetAdviserAgentCodes(advisorVo.advisorId, ddlMapping.SelectedValue, int.Parse(ddlAMFIAvaliable.SelectedValue), MappedruleId.TrimStart(','));
            LBAgentCodes.DataSource = dsAdviserBranchList;
            LBAgentCodes.DataValueField = "AgentId";
            LBAgentCodes.DataTextField = "AgentCodeWithName";
            LBAgentCodes.DataBind();
        }

        protected void btnCancelSelectedRule_OnClick(object sender, EventArgs e)
        {
            foreach (GridDataItem gdi in rgPayableMapping.MasterTableView.Items)
            {
                CheckBoxList chkListrate = (CheckBoxList)gdi.FindControl("chkListrate");
                chkListrate.Enabled = true;
                for (int i = 0; i < chkListrate.Items.Count; i++)
                {
                    chkListrate.Items[i].Selected = false;
                }
                radAplicationPopUp.VisibleOnPageLoad = true;
                RadListBoxSelectedAgentCodes.Items.Clear();
            }
        }
        protected void TogglePanel()
        {
            gvPayaMapping.Visible = false;
            ImageButton7.ImageUrl = "~/Images/toggle-expand-alt_blue.png";
            ImageButton7.ToolTip = "Expend";
            btnIssueMap.Visible = false;
            tb1.Visible = false;
            ImageButton3.ImageUrl = "~/Images/toggle-expand-alt_blue.png";
            ImageButton3.ToolTip = "Expend";
            pnlAddSchemesButton.Visible = false;
            pnlAddSchemes.Visible = false;
            gvMappedSchemes.Visible = false;
            //Table2.Visible = false;
            ImageButton4.ToolTip = "Expend";
            ImageButton4.ImageUrl = "~/Images/toggle-expand-alt_blue.png";
            tbNcdIssueList.Visible = false;
            pnlIssueList.Visible = false;
            ImageButton5.ToolTip = "Expend";
            ImageButton5.ImageUrl = "~/Images/toggle-expand-alt_blue.png";
            tblCommissionStructureRule1.Visible = false;
            imgBuy1.ImageUrl = "~/Images/toggle-expand-alt_blue.png";
            imgBuy1.ToolTip = "Expend";
            gvMappedIssueList.Visible = false;
        }
        protected void btnCreateRule_OnClick(object sender, EventArgs e)
        {
            CreateRuleAndRateForMFBONDIPO();
        }
        protected void CreateRuleAndRateForMFBONDIPO()
        {
            DataTable dtRulecreate = new DataTable();
            dtRulecreate.Columns.Add("RuleName");
            dtRulecreate.Columns.Add("ComissionType");
            dtRulecreate.Columns.Add("InVestorType");
            dtRulecreate.Columns.Add("CityGroup");
            dtRulecreate.Columns.Add("Levels");
            dtRulecreate.Columns.Add("TRXNType");
            dtRulecreate.Columns.Add("S_Tax");
            dtRulecreate.Columns.Add("TDS");
            dtRulecreate.Columns.Add("MinAmt", typeof(decimal), null);
            dtRulecreate.Columns.Add("MaxAmt", typeof(decimal));
            dtRulecreate.Columns.Add("MinAge");
            dtRulecreate.Columns.Add("MaxAge");
            dtRulecreate.Columns.Add("Rec", typeof(decimal));
            dtRulecreate.Columns.Add("Payable", typeof(decimal));
            dtRulecreate.Columns.Add("BrokerageUnit");
            dtRulecreate.Columns.Add("ValidationFrom", typeof(DateTime));
            dtRulecreate.Columns.Add("ValidationTo", typeof(DateTime));
            dtRulecreate.Columns.Add("ACSR_InvestmentAgeUnit");
            dtRulecreate.Columns.Add("IsClawBack");
            dtRulecreate.Columns.Add("ClowBackAge");
            DataRow drRulecreate = dtRulecreate.NewRow();
            foreach (GridDataItem radItem in RadGridStructureRule.MasterTableView.Items)
            {
                if (!string.IsNullOrEmpty(ViewState["ItemRemove"].ToString()) && int.Parse(ViewState["ItemRemove"].ToString()) != radItem.ItemIndex)
                {
                    TextBox txtServiceTex = (TextBox)radItem.FindControl("txtServiceTex");
                    TextBox txtTDSTex = (TextBox)radItem.FindControl("txtTDSTex");
                    TextBox txtReceivableValue = (TextBox)radItem.FindControl("txtReceivableValue");
                    TextBox txtPaybleValue = (TextBox)radItem.FindControl("txtPaybleValue");
                    TextBox txtMinInvestmentAmount = (TextBox)radItem.FindControl("txtMinInvestmentAmount");
                    TextBox txtMaxInvestmentAmount = (TextBox)radItem.FindControl("txtMaxInvestmentAmount");
                    DropDownList ddlBrokrageUnit = (DropDownList)radItem.FindControl("ddlBrokrageUnit");
                    CheckBox check = (CheckBox)radItem.FindControl("ChkIsclowBack");
                    TextBox txtbox = (TextBox)radItem.FindControl("txtClawBackAge");
                    string commtype = RadGridStructureRule.MasterTableView.DataKeyValues[radItem.ItemIndex]["WCT_CommissionTypeCode"].ToString();
                    string minage = RadGridStructureRule.MasterTableView.DataKeyValues[radItem.ItemIndex]["ACSR_MinInvestmentAge"].ToString();
                    string MaxAge = RadGridStructureRule.MasterTableView.DataKeyValues[radItem.ItemIndex]["ACSR_MaxInvestmentAge"].ToString();
                    drRulecreate = dtRulecreate.NewRow();
                    drRulecreate["RuleName"] = RadGridStructureRule.MasterTableView.DataKeyValues[radItem.ItemIndex]["ACSR_CommissionStructureRuleName"];
                    drRulecreate["ComissionType"] = commtype;
                    drRulecreate["InVestorType"] = "IND";
                    if (RadGridStructureRule.MasterTableView.DataKeyValues[radItem.ItemIndex]["ACSR_CommissionStructureRuleName"].ToString().Contains("T15"))
                        drRulecreate["CityGroup"] = "1009";
                    else
                        drRulecreate["CityGroup"] = "1010";
                    drRulecreate["TRXNType"] = "NonSIP";
                    drRulecreate["S_Tax"] = (!string.IsNullOrEmpty(txtServiceTex.Text)) ? Convert.ToDecimal(txtServiceTex.Text) : 0;
                    drRulecreate["TDS"] = (!string.IsNullOrEmpty(txtTDSTex.Text)) ? Convert.ToDecimal(txtTDSTex.Text) : 0;
                    drRulecreate["MinAmt"] = (!string.IsNullOrEmpty(txtMinInvestmentAmount.Text)) ? Convert.ToDecimal(txtMinInvestmentAmount.Text) : 1;//RadGridStructureRule.MasterTableView.DataKeyValues[radItem.ItemIndex]["ACSR_MinInvestmentAmount"];
                    drRulecreate["MaxAmt"] = (!string.IsNullOrEmpty(txtMaxInvestmentAmount.Text)) ? Convert.ToDecimal(txtMaxInvestmentAmount.Text) : 1;//RadGridStructureRule.MasterTableView.DataKeyValues[radItem.ItemIndex]["ACSR_MaxInvestmentAmount"];
                    if (minage == "N/A" || minage == "0")
                        drRulecreate["MinAge"] = "0";
                    else
                        drRulecreate["MinAge"] = minage;
                    if (MaxAge == "N/A")
                        drRulecreate["MaxAge"] = "0";
                    else
                        drRulecreate["MaxAge"] = MaxAge;

                    drRulecreate["Rec"] = (!string.IsNullOrEmpty(txtReceivableValue.Text)) ? Convert.ToDecimal(txtReceivableValue.Text) : 0;
                    drRulecreate["Payable"] = (!string.IsNullOrEmpty(txtPaybleValue.Text)) ? Convert.ToDecimal(txtPaybleValue.Text) : 0;
                    drRulecreate["BrokerageUnit"] = ddlBrokrageUnit.SelectedValue;
                    drRulecreate["ValidationFrom"] = Convert.ToDateTime(txtValidityFrom.Text);
                    drRulecreate["ValidationTo"] = Convert.ToDateTime(txtValidityTo.Text);
                    drRulecreate["ACSR_InvestmentAgeUnit"] = "Days";
                    drRulecreate["IsClawBack"] = (check.Checked) ? 1 : 0;
                    if (check.Checked)
                        drRulecreate["ClowBackAge"] = (!string.IsNullOrEmpty(txtbox.Text)) ? int.Parse(txtbox.Text) : 0;
                    else
                        drRulecreate["ClowBackAge"] = 0;
                    dtRulecreate.Rows.Add(drRulecreate);
                }


            }
            bool result = false;
            result = commisionReceivableBo.CreateNewRuleAndRate(dtRulecreate, int.Parse(hidCommissionStructureName.Value), userVo.UserId);
            if (ViewState["bindGrid"] != "1")
            {
                ViewState["gridEdit"] = 0;
                BindCommissionStructureRuleGrid(int.Parse(hidCommissionStructureName.Value));

            }
            if (result == true)
            {
                RadGridStructureRule.MasterTableView.GetColumn("Edit1").Visible = true;
                RadGridStructureRule.MasterTableView.GetColumn("Delete").Visible = true;


            }
        }

        protected void btnupdate_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridItem gr = (GridItem)btn.NamingContainer;
            int ruleid = int.Parse(RadGridStructureRule.MasterTableView.DataKeyValues[gr.ItemIndex]["ACSR_CommissionStructureRuleId"].ToString());
            TextBox txtServiceTex = (TextBox)gr.FindControl("txtServiceTex");
            TextBox txtTDSTex = (TextBox)gr.FindControl("txtTDSTex");
            TextBox txtReceivableValue = (TextBox)gr.FindControl("txtReceivableValue");
            TextBox txtPaybleValue = (TextBox)gr.FindControl("txtPaybleValue");
            TextBox txtMinInvestmentAmount = (TextBox)gr.FindControl("txtMinInvestmentAmount");
            TextBox txtMaxInvestmentAmount = (TextBox)gr.FindControl("txtMaxInvestmentAmount");
            DropDownList ddlBrokrageUnit = (DropDownList)gr.FindControl("ddlBrokrageUnit");
            CheckBox check = (CheckBox)gr.FindControl("ChkIsclowBack");
            TextBox txtbox = (TextBox)gr.FindControl("txtClawBackAge");
            bool result = false;

            ViewState["ruleid"] = ViewState["ruleid"] + ruleid.ToString() + ",";
            result = commisionReceivableBo.UpdateRuleRateandTax(ruleid, (txtServiceTex.Text == "") ? 0 : Convert.ToDecimal(txtServiceTex.Text),
                (txtTDSTex.Text == "") ? 0 : Convert.ToDecimal(txtTDSTex.Text), (txtReceivableValue.Text == "") ? 0 : Convert.ToDecimal(txtReceivableValue.Text), (txtPaybleValue.Text == "") ? 0 : Convert.ToDecimal(txtPaybleValue.Text), ddlBrokrageUnit.SelectedValue, userVo.UserId, (check.Checked) ? 1 : 0, (!string.IsNullOrEmpty(txtbox.Text)) ? int.Parse(txtbox.Text) : 0, (txtMaxInvestmentAmount.Text == "") ? 0 : Convert.ToDecimal(txtMaxInvestmentAmount.Text), (txtMinInvestmentAmount.Text == "") ? 0 : Convert.ToDecimal(txtMinInvestmentAmount.Text));
            if (result == true)
                BindCommissionStructureRuleGrid(int.Parse(hidCommissionStructureName.Value));

        }
        protected void btnruleEdit_OnClick(object sender, EventArgs e)
        {
            try
            {
                LinkButton imgbutton = (LinkButton)sender;
                GridDataItem gvr = (GridDataItem)imgbutton.NamingContainer;
                LinkButton lnkEdit = (LinkButton)gvr.FindControl("lnkEdit");
                RadGridStructureRule.MasterTableView.GetColumn("Update").Visible = true;
                TextBox txtReceivableValue = (TextBox)gvr.FindControl("txtReceivableValue");
                TextBox txtPaybleValue = (TextBox)gvr.FindControl("txtPaybleValue");
                TextBox txtServiceTex = (TextBox)gvr.FindControl("txtServiceTex");
                TextBox txtTDSTex = (TextBox)gvr.FindControl("txtTDSTex");
                Button btnupdate = (Button)gvr.FindControl("btnupdate");
                DropDownList ddlBrokrageUnit = (DropDownList)gvr.FindControl("ddlBrokrageUnit");
                CheckBox check = (CheckBox)gvr.FindControl("ChkIsclowBack");
                TextBox txtbox = (TextBox)gvr.FindControl("txtClawBackAge");
                TextBox txtMinInvestmentAmount = (TextBox)gvr.FindControl("txtMinInvestmentAmount");
                TextBox txtMaxInvestmentAmount = (TextBox)gvr.FindControl("txtMaxInvestmentAmount");
                RadGridStructureRule.MasterTableView.GetColumn("Delete").Visible = true;
                txtMinInvestmentAmount.Enabled = true;
                txtMaxInvestmentAmount.Enabled = true;
                txtReceivableValue.Enabled = true;
                txtPaybleValue.Enabled = true;
                txtServiceTex.Enabled = true;
                txtTDSTex.Enabled = true;
                btnupdate.Enabled = true;
                ddlBrokrageUnit.Enabled = true;
                lnkEdit.Visible = false;
                btnupdate.Visible = true;
                check.Enabled = true;
                txtbox.Enabled = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        protected void chkCloBack_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            CheckBox check = new CheckBox();
            Label lnkEdit = new Label();
            TextBox txtbox = new TextBox();
            RequiredFieldValidator RequiredFieldValidator15 = new RequiredFieldValidator();
            System.Web.UI.HtmlControls.HtmlTableCell tdlblClock = new System.Web.UI.HtmlControls.HtmlTableCell();

            if (chk.NamingContainer is Telerik.Web.UI.GridEditFormItem)
            {
                GridEditFormItem gvr = (GridEditFormItem)chk.NamingContainer;
                check = (CheckBox)gvr.FindControl("chkCloBack");
                lnkEdit = (Label)gvr.FindControl("lblClock");
                txtbox = (TextBox)gvr.FindControl("txtAge");
                tdlblClock = (System.Web.UI.HtmlControls.HtmlTableCell)gvr.FindControl("tdlblClock");
                RequiredFieldValidator15 = (RequiredFieldValidator)gvr.FindControl("RequiredFieldValidator15");
            }
            else if (chk.NamingContainer is Telerik.Web.UI.GridEditFormInsertItem)
            {
                GridEditFormInsertItem gvr = (GridEditFormInsertItem)chk.NamingContainer;
                check = (CheckBox)gvr.FindControl("chkCloBack");
                lnkEdit = (Label)gvr.FindControl("lblClock");
                txtbox = (TextBox)gvr.FindControl("txtAge");
                tdlblClock = (System.Web.UI.HtmlControls.HtmlTableCell)gvr.FindControl("tdlblClock");
                RequiredFieldValidator15 = (RequiredFieldValidator)gvr.FindControl("RequiredFieldValidator15");
            }

            if (chk.Checked == true)
            {
                check.Visible = true;
                lnkEdit.Visible = true;
                txtbox.Visible = true;
                tdlblClock.Visible = true;
                RequiredFieldValidator15.Visible = true;
            }
            else
            {
                tdlblClock.Visible = false;
                check.Visible = true;
                lnkEdit.Visible = false;
                txtbox.Visible = false;
                RequiredFieldValidator15.Visible = false;
                txtbox.Text = "0";
            }
        }
        protected void ChkIsclowBack_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridDataItem gvr = (GridDataItem)chk.NamingContainer;
            CheckBox check = (CheckBox)gvr.FindControl("ChkIsclowBack");
            TextBox txtbox = (TextBox)gvr.FindControl("txtClawBackAge");
            if (check.Checked)
            {
                txtbox.Enabled = true;
            }
        }
    }
}
