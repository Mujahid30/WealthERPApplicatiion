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


        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session["rmVo"];
            int structureId = 0;
            if (!IsPostBack)
            {
                if (Request.QueryString["StructureId"] != null)
                    structureId = Convert.ToInt32(Request.QueryString["StructureId"].ToString());
                GetCommisionTypes();
                GetProduct();
                

                if (structureId != 0)
                {
                    BindAllDropdown();
                    LoadStructureDetails(structureId);
                    BindCommissionStructureRuleGrid(structureId);
                    trPayableMapping.Visible = true;
                    BindPayableGrid(structureId);
                    rgPayableMapping.Visible = true;
                    //pnlAddSchemes.Visible = true;
                    // Label12.Visible = true;
                    //Label13.Visible = true;
                    if (ddlProductType.SelectedValue == "MF")
                    {
                        pnlAddSchemesButton.Visible = true;
                        Table2.Visible = true;
                        CreateMappedSchemeGrid();
                    }
                    else if (ddlProductType.SelectedValue != "MF")
                    {

                        GetMapped_Unmapped_Issues("Mapped", "");
                        GetUnamppedIssues(ddlIssueType.SelectedValue);
                        Table4.Visible = true;
                        tbNcdIssueList.Visible = true;
                    }
                    //else if (ddlCommissionype.SelectedValue == "16020")
                    //{
                    //    Table3.Visible = true;
                    //    tblMapping.Visible = true;
                    //    btnPaybleMapping.Visible = true;
                    //    SetStructureDetails();
                    //    DefaultAssignments();
                    //}
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
            }
            else
            {
                tb1.Visible = true;

            }

        }


        protected void imgBuyMapping_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlProductType.SelectedValue == "MF")
            {
                if (pnlAddSchemes.Visible == true)
                {
                    pnlAddSchemes.Visible = false;
                    gvMappedSchemes.Visible = false;
                    //Table2.Visible = false;
                }
                else
                {
                    pnlAddSchemes.Visible = true;
                    if (gvMappedSchemes.Items.Count > 0)
                        gvMappedSchemes.Visible = true;
                    //Table2.Visible = true;


                }

            }



        }
        protected void imgNcd_Click(object sender, ImageClickEventArgs e)
        {


            if (ddlProductType.SelectedValue != "MF")
            {

                if (tbNcdIssueList.Visible == true)
                {
                    // Table4.Visible = false;
                    tbNcdIssueList.Visible = false;
                    pnlIssueList.Visible = false;
                }
                else
                {
                    // Table4.Visible = true;
                    tbNcdIssueList.Visible = true;
                    pnlIssueList.Visible = true;

                }


            }
        }
        protected void imgBuy1_Click(object sender, ImageClickEventArgs e)
        {
            if (tblCommissionStructureRule1.Visible == true)
            {
                tblCommissionStructureRule1.Visible = false;
            }
            else
            {
                tblCommissionStructureRule1.Visible = true;

            }
        }


        protected void imgBuy3_Click(object sender, ImageClickEventArgs e)
        {
            //if (Table1.Visible == true)
            //{
            //    Table1.Visible = false;
            //}
            //else
            //{
            //    Table1.Visible = true;

            //}
        }

        protected void ddlProductType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindAllDropdown();
            ShowHideControlsBasedOnProduct(ddlProductType.SelectedValue);
            GetCategory(ddlProductType.SelectedValue);
           

        }

        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubcategoryListBox(ddlCategory.SelectedValue);
        }
        protected void btnSubmitCommissionTypeBrokerage_Click(object sender, EventArgs e)
        {
            RadWDCommissionTypeBrokerage.VisibleOnPageLoad = true;

        }

        private void CreateUpdateDeleteAplication(int fromRange, int toRange, int adviserId, int issueId, int formRangeId, string commandType)
        {
            //string status = string.Empty;


            //i = onlineNCDBackOfficeBo.CreateUpdateDeleteAplicationNos(fromRange, toRange, adviserId, issueId, formRangeId, commandType, ref status);



        }
        protected void llPurchase_Click(object sender, EventArgs e)
        {
            int rowindex1 = ((GridDataItem)((LinkButton)sender).NamingContainer).RowIndex;
            int rowindex = (rowindex1 / 2) - 1;
            LinkButton lbButton = (LinkButton)sender;
            GridDataItem item = (GridDataItem)lbButton.NamingContainer;

            int ruleId = int.Parse(rgPayableMapping.MasterTableView.DataKeyValues[rowindex]["CSRD_StructureRuleDetailsId"].ToString());
            string structureId = hidCommissionStructureName.Value;
            string myscript = "window.open('PopUp.aspx?ID=" + structureId + "&ruleId=" + ruleId + "&pageID=PayableStructureToAgentCategoryMapping&', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')";


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "<script>" + myscript + "</script>", false);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('PayableStructureToAgentCategoryMapping','&ruleId=" + ruleId + "');", true);
            // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('PayableStructureToAgentCategoryMapping','ruleId=" + ruleId + "&ID=" + hidCommissionStructureName.Value + "&Product=" + ddlProductType.SelectedValue + "');", true);

        }



        protected void llView_Click(object sender, EventArgs e)
        {
            int rowindex1 = ((GridDataItem)((LinkButton)sender).NamingContainer).RowIndex;
            int rowindex = (rowindex1 / 2) - 1;
            LinkButton lbButton = (LinkButton)sender;
            GridDataItem item = (GridDataItem)lbButton.NamingContainer;

            int ruleId = int.Parse(rgPayableMapping.MasterTableView.DataKeyValues[rowindex]["CSRD_StructureRuleDetailsId"].ToString());
            string structureId = hidCommissionStructureName.Value;
            string myscript = "window.open('PopUp.aspx?ID=" + structureId + "&ruleId=" + ruleId + "&Action=VIEW&pageID=PayableStructureToAgentCategoryMapping&', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')";


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "<script>" + myscript + "</script>", false);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('PayableStructureToAgentCategoryMapping','&ruleId=" + ruleId + "');", true);
            // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('PayableStructureToAgentCategoryMapping','ruleId=" + ruleId + "&ID=" + hidCommissionStructureName.Value + "&Product=" + ddlProductType.SelectedValue + "');", true);

        }

        private void BindRuleDetGrid(RadGrid rgCommissionTypeCaliculation, int ruleId)
        {
            DataSet dsLookupData;
            dsLookupData = commisionReceivableBo.GetCommissionTypeBrokerage(ruleId);
            rgCommissionTypeCaliculation.DataSource = dsLookupData;
            rgCommissionTypeCaliculation.DataBind();

            if (Cache[userVo.UserId.ToString() + "RuleDet"] != null)
                Cache.Remove(userVo.UserId.ToString() + "RuleDet");
            Cache.Insert(userVo.UserId.ToString() + "RuleDet", dsLookupData.Tables[0]);

        }

        private void BindPayableGrid(int structureId)
        {
            DataSet dsLookupData;
            dsLookupData = commisionReceivableBo.GetPayableCommissionTypeBrokerage(structureId);
            rgPayableMapping.DataSource = dsLookupData;
            rgPayableMapping.DataBind();

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
        protected void rgCommissionTypeCaliculation_ItemCommand(object source, GridCommandEventArgs e)
        {
            int ruleId = 0;
            int ruledetId = 0;
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                RadGrid rgCommissionTypeCaliculation = (RadGrid)source;

                ruleId = Convert.ToInt32(HiddenField1.Value);
                //Convert.ToInt32(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_CommissionStructureRuleId"].ToString());

                TextBox txtBrokerageValue = (TextBox)e.Item.FindControl("txtBrokerageValue");
                DropDownList ddlCommissionype = (DropDownList)e.Item.FindControl("ddlCommissionype");
                DropDownList ddlBrokerageUnit = (DropDownList)e.Item.FindControl("ddlBrokerageUnit");


                commisionReceivableBo.CreateUpdateDeleteCommissionTypeBrokerage(ruleId, Convert.ToInt32(ddlCommissionype.SelectedValue), ddlBrokerageUnit.SelectedValue, Convert.ToDecimal(txtBrokerageValue.Text), "INSERT",0,0);

                BindRuleDetGrid(rgCommissionTypeCaliculation, ruleId);
                BindPayableGrid(Convert.ToInt32(hidCommissionStructureName.Value));
            }
            else if (e.CommandName == RadGrid.UpdateCommandName)
            {
                RadGrid rgCommissionTypeCaliculation = (RadGrid)source;
                ruleId  = Convert.ToInt32(HiddenField1.Value);
                ruledetId = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CSRD_StructureRuleDetailsId"].ToString());

                TextBox txtBrokerageValue = (TextBox)e.Item.FindControl("txtBrokerageValue");
                DropDownList ddlCommissionype = (DropDownList)e.Item.FindControl("ddlCommissionype");
                DropDownList ddlBrokerageUnit = (DropDownList)e.Item.FindControl("ddlBrokerageUnit");

                commisionReceivableBo.CreateUpdateDeleteCommissionTypeBrokerage(ruleId, Convert.ToInt32(ddlCommissionype.SelectedValue), ddlBrokerageUnit.SelectedValue, Convert.ToDecimal(txtBrokerageValue.Text), "UPDATE", ruledetId,0);

                BindRuleDetGrid(rgCommissionTypeCaliculation, ruleId);
                BindPayableGrid(Convert.ToInt32(hidCommissionStructureName.Value));

            }
            else if (e.CommandName == RadGrid.DeleteCommandName)
            {
                //if (rgCommissionTypeCaliculation.Items.Count > 1)
                //{
                //    formRangeId = Convert.ToInt32(rgCommissionTypeCaliculation.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIFR_Id"].ToString());
                //    CreateUpdateDeleteAplication(0, 0, 0, 0, formRangeId, "DELETE");
                //}
                //BindApplGrid();

            }

        }

        protected void rgCommissionTypeCaliculation_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                DropDownList ddlCommissionype = (DropDownList)e.Item.FindControl("ddlCommissionype");
                DropDownList ddlBrokerageUnit = (DropDownList)e.Item.FindControl("ddlBrokerageUnit");


                DataSet dscommissionTypes;
                dscommissionTypes = commisionReceivableBo.GetCommisionTypes();

                ddlCommissionype.DataSource = dscommissionTypes.Tables[0];
                ddlCommissionype.DataValueField = "WCMV_LookupId";
                ddlCommissionype.DataTextField = "WCMV_Name";
                ddlCommissionype.DataBind();
                ddlCommissionype.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

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
                //Commission Type Bind
                DataSet dscommissionTypes;
                dscommissionTypes = commisionReceivableBo.GetCommisionTypes();
                ddlCommissionype.DataSource = dscommissionTypes.Tables[0];
                ddlCommissionype.DataValueField = "WCMV_LookupId";
                ddlCommissionype.DataTextField = "WCMV_Name";
                ddlCommissionype.DataBind();
                ddlCommissionype.SelectedValue = dsCommissionTypesAndBrokerage.Tables[0].Rows[0].ItemArray[4].ToString();
                //Brokerage Bind
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

            //DataTable dtLookupData;
            //dtLookupData = (DataTable)Cache[userVo.UserId.ToString() + "RuleDet"];
            DataSet dsLookupData;
            if (string.IsNullOrEmpty(HiddenField1.Value))
                HiddenField1.Value = "0";
            dsLookupData = commisionReceivableBo.GetCommissionTypeBrokerage(Convert.ToInt32(HiddenField1.Value));
            rgCommissionTypeCaliculation.DataSource = dsLookupData.Tables[0];



        }
        protected void BtnActivRangeClose_Click(object sender, EventArgs e)
        {
            RadWDCommissionTypeBrokerage.VisibleOnPageLoad = false;

        }

        protected void ddlBrokerageUnit_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlBrokerageUnit = (DropDownList)sender;
            DropDownList ddlCommisionCalOn = new DropDownList();

            //if (ddlBrokerageUnit.NamingContainer is Telerik.Web.UI.GridEditFormItem)
            //{
            //    GridEditFormItem gdi;
            //    gdi = (GridEditFormItem)ddlBrokerageUnit.NamingContainer;
            //    ddlCommisionCalOn = (DropDownList)gdi.FindControl("ddlCommisionCalOn");
            //}
            //else if (ddlBrokerageUnit.NamingContainer is Telerik.Web.UI.GridEditFormInsertItem)
            //{
            //    GridEditFormInsertItem gdi;
            //    gdi = (GridEditFormInsertItem)ddlBrokerageUnit.NamingContainer;
            //    ddlCommisionCalOn = (DropDownList)gdi.FindControl("ddlCommisionCalOn");
            //    ddlCommisionCalOn.Enabled = false;
            //}

            //if (ddlBrokerageUnit.SelectedValue == "PER")
            //{
            //    ddlCommisionCalOn.SelectedValue = "AUM on the date";
            //}
            //else if (ddlBrokerageUnit.SelectedValue == "ADA")
            //{
            //    ddlCommisionCalOn.SelectedValue = "APPC";
            //}
            //else if (ddlBrokerageUnit.SelectedValue == "APU")
            //{
            //    ddlCommisionCalOn.SelectedValue = "INAM";

            //}
        }


        private void ShowHideControlsBasedOnProduct(string asset)
        {
            ddlSubInstrCategory.Items.Clear();
            ddlSubInstrCategory.DataBind();
            if (asset == "MF")
            {
                trIssuer.Visible = true;
                lblCategory.Visible = true;
                ddlCategory.Visible = true;
                lblSubCategory.Visible = true;
                SpanCategory.Visible = true;
                SpanSubCategory.Visible = true;
               
            }
            else if (asset == "FI")
            {
                trIssuer.Visible = false;
                lblCategory.Visible = false;
                ddlCategory.Visible = false;
                lblSubCategory.Visible = false;
                SpanCategory.Visible = false;
                SpanSubCategory.Visible = false;
                tdlblCategory.Visible = true;
                tdddlCategory.Visible = true;
                BindBondCategories();
            }
            else if (asset == "IP")
            {
                trIssuer.Visible = false;
                lblCategory.Visible = false;
                ddlCategory.Visible = false;
                lblSubCategory.Visible = false;
                SpanCategory.Visible = false;
                SpanSubCategory.Visible = false;
                tdlblCategory.Visible = false;
                tdddlCategory.Visible = false;
              
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
            ddlProductType.SelectedValue = "MF";

            GetCategory(ddlProductType.SelectedValue);

        }

        protected void GetCategory(string product)
        {
            DataSet dsLookupData;
            dsLookupData = commisionReceivableBo.GetCategories(ddlProductType.SelectedValue);

            ddlCategory.DataSource = dsLookupData.Tables[0];
            ddlCategory.DataValueField = dsLookupData.Tables[0].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory.DataTextField = dsLookupData.Tables[0].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory.DataBind();
            //ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));


        }

        protected void GetCommisionTypes()
        {
            //DataSet dscommissionTypes;
            //dscommissionTypes = commisionReceivableBo.GetCommisionTypes();

            //ddlCommissionype.DataSource = dscommissionTypes.Tables[0];
            //ddlCommissionype.DataValueField = "WCMV_LookupId";
            //ddlCommissionype.DataTextField = "WCMV_Name";
            //ddlCommissionype.DataBind();
            //ddlCommissionype.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
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


            //ddlCategory.DataSource = dsLookupData.Tables[8];
            //ddlCategory.DataValueField = dsLookupData.Tables[8].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            //ddlCategory.DataTextField = dsLookupData.Tables[8].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            //ddlCategory.DataBind();
            //ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));


            Session["CommissionLookUpData"] = dsLookupData;

        }

        protected void SetStructureMasterControlDefaultValues(string assetType)
        {
            if (assetType == "MF")
            {

                ddlProductType.SelectedValue = "MF";
                ddlCategory.SelectedValue = "MFDT";

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
        //protected void SetStructureRuleControlDefaultValues(string commType)
        //{

        //}

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

                //receivableStructureMasterVo.CommissionTypeCode = ddlCommissionType.SelectedValue;
                commissionStructureMasterVo.IsClawBackApplicable = chkHasClawBackOption.Checked;
                commissionStructureMasterVo.IsNonMonetaryReward = chkMoneytaryReward.Checked;

                //receivableStructureMasterVo.IsStructureFromIssuer = bool.Parse(chk.Checked.ToString());
                //receivableStructureMasterVo.RecurringiSIPFrequency=ddl
                hdnProductId.Value = ddlProductType.SelectedValue;
                hdnStructValidFrom.Value = txtValidityFrom.Text;
                hdnStructValidTill.Value = txtValidityTo.Text;
                hdnIssuerId.Value = ddlIssuer.SelectedValue;
                hdnCategoryId.Value = ddlCategory.SelectedValue;

                commissionStructureMasterVo.StructureNote = txtNote.Text.Trim();

                if (ddlProductType.SelectedValue == "MF")
                {
                    foreach (RadListBoxItem item in rlbAssetSubCategory.Items)
                    {
                        if (item.Checked == true)
                        {
                            strSubCategoryCode.Append(item.Value);
                            strSubCategoryCode.Append("~");
                        }
                    }

                    if (!string.IsNullOrEmpty(strSubCategoryCode.ToString()))
                        strSubCategoryCode = strSubCategoryCode.Remove((strSubCategoryCode.Length - 1), 1);

                    commissionStructureMasterVo.AssetSubCategory = strSubCategoryCode;

                }
                else if (ddlProductType.SelectedValue == "IP")
                {

                    strSubCategoryCode.Append("FIFIIP");
                    commissionStructureMasterVo.AssetSubCategory = strSubCategoryCode;
                }
                else if (ddlProductType.SelectedValue == "FI")
                {
                    strSubCategoryCode.Append(ddlSubInstrCategory.SelectedValue);
                    commissionStructureMasterVo.AssetSubCategory = strSubCategoryCode;
                }
                string subcategoryIds = commissionStructureMasterVo.AssetSubCategory.ToString();
                subcategoryIds = subcategoryIds.Replace("~", ",");
                hdnSubcategoryIds.Value = subcategoryIds;

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
            if (string.IsNullOrEmpty(commissionStructureMasterVo.AssetSubCategory.ToString()) && ddlProductType.SelectedValue == "MF")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('At least one subcategory required!');", true);
                return;
            }
            else
            {
                commisionReceivableBo.CreateCommissionStructureMastter(commissionStructureMasterVo, userVo.UserId, 0, out commissionStructureId);
                hidCommissionStructureName.Value = commissionStructureId.ToString();
                CommissionStructureControlsEnable(false);
                tblCommissionStructureRule.Visible = true;
                tblCommissionStructureRule1.Visible = true;
                // MapPingLinksBasedOnCpmmissionTypes(ddlCommissionype.SelectedValue);
                //  Label12.Visible = true;
                rgPayableMapping.Visible = true;
                // Label13.Visible = true;
                trPayableMapping.Visible = true;
                BindPayableGrid(Convert.ToInt32(hidCommissionStructureName.Value));
                if (ddlProductType.SelectedValue == "MF")
                {
                    pnlAddSchemes.Visible = true;
                    Table2.Visible = true;
                }
                else if (ddlProductType.SelectedValue != "MF")
                {

                    GetMapped_Unmapped_Issues("Mapped", "");
                    GetUnamppedIssues(ddlIssueType.SelectedValue);
                    Table4.Visible = true;
                    tbNcdIssueList.Visible = true;
                    BindBondCategories();
                }

                ShowAndHideVisible_FirstSection();
                //else if (ddlCommissionype.SelectedValue == "16020")
                //{
                //Table3.Visible = true;
                //// SetStructureDetails();
                //DefaultAssignments();
                //tblMapping.Visible = true;
                //btnPaybleMapping.Visible = true;
                //}
                //pnlAddSchemesButton.Visible = true;
            }


        }
        private void BindBondCategories()
        {
            OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
            DataTable dtCategory = new DataTable();
            dtCategory = onlineNCDBackOfficeBo.BindNcdCategory("SubInstrumentCat", "").Tables[0];
            if (dtCategory.Rows.Count > 0)
            {
                ddlSubInstrCategory.DataSource = dtCategory;
                ddlSubInstrCategory.DataValueField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlSubInstrCategory.DataTextField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlSubInstrCategory.DataBind();
            }
            ddlSubInstrCategory.Items.Insert(0, new ListItem("Select", "Select"));

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


            if (string.IsNullOrEmpty(commissionStructureMasterVo.AssetSubCategory.ToString()) && ddlProductType.SelectedValue == "MF")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('At least one subcategory required!');", true);
                return;
            }
            else
            {
                commissionStructureMasterVo.CommissionStructureId = Convert.ToInt32(hidCommissionStructureName.Value);
                commisionReceivableBo.UpdateCommissionStructureMastter(commissionStructureMasterVo, userVo.UserId);
                CommissionStructureControlsEnable(false);
                // MapPingLinksBasedOnCpmmissionTypes(ddlCommissionype.SelectedValue);
            }

        }

        protected void ddlCommissionApplicableLevel_Selectedindexchanged(object sender, EventArgs e)
        {


            //DropDownList ddlCommissionApplicableLevel = (DropDownList)sender;
            //DropDownList ddlCommissionType = new DropDownList();

            ////ShowAndHideSTructureRuleControlsBasedOnCommissionTypeAndLevel

            //Label lblReceivableFrequency = new Label();

            //System.Web.UI.HtmlControls.HtmlTableRow trTransactionTypeSipFreq = new System.Web.UI.HtmlControls.HtmlTableRow();
            //System.Web.UI.HtmlControls.HtmlTableRow trMinMaxTenure = new System.Web.UI.HtmlControls.HtmlTableRow(); ;
            //System.Web.UI.HtmlControls.HtmlTableRow trMinMaxAge = new System.Web.UI.HtmlControls.HtmlTableRow(); ;
            //System.Web.UI.HtmlControls.HtmlTableCell tdMinNumberOfApplication = new System.Web.UI.HtmlControls.HtmlTableCell(); ;

            //if (ddlCommissionApplicableLevel.NamingContainer is Telerik.Web.UI.GridEditFormItem)
            //{
            //    GridEditFormItem gdi;
            //    gdi = (GridEditFormItem)ddlCommissionApplicableLevel.NamingContainer;
            //    lblReceivableFrequency = (Label)gdi.FindControl("lblReceivableFrequency");
            //    ddlCommissionType = (DropDownList)gdi.FindControl("ddlCommissionType");
            //    trTransactionTypeSipFreq = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trTransactionTypeSipFreq");
            //    trMinMaxTenure = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinMaxTenure");
            //    trMinMaxAge = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinMaxAge");
            //    tdMinNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdMinNumberOfApplication");
            //}
            //else if (ddlCommissionApplicableLevel.NamingContainer is Telerik.Web.UI.GridEditFormInsertItem)
            //{
            //    GridEditFormInsertItem gdi;
            //    gdi = (GridEditFormInsertItem)ddlCommissionApplicableLevel.NamingContainer;
            //    lblReceivableFrequency = (Label)gdi.FindControl("lblReceivableFrequency");

            //    ddlCommissionType = (DropDownList)gdi.FindControl("ddlCommissionType");

            //    trTransactionTypeSipFreq = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trTransactionTypeSipFreq");
            //    trMinMaxTenure = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinMaxTenure");
            //    trMinMaxAge = (System.Web.UI.HtmlControls.HtmlTableRow)gdi.FindControl("trMinMaxAge");
            //    tdMinNumberOfApplication = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdMinNumberOfApplication");
            //}



            ////lblReceivableFrequency.Visible = !enablement;
            ////ddlReceivableFrequency.Visible = !enablement;
            ////trTransactionTypeSipFreq.Visible = !enablement;
            ////trMinMaxTenure.Visible = !enablement;
            ////trMinMaxAge.Visible = !enablement;
            ////tdMinNumberOfApplication.Visible = !enablement;

            //ShowAndHideSTructureRuleControlsBasedOnProductAndCommisionType(lblReceivableFrequency, ddlReceivableFrequency, trTransactionTypeSipFreq, trMinMaxTenure, trMinMaxAge, tdMinNumberOfApplication, ddlProductType.SelectedValue, ddlCommissionApplicableLevel.SelectedValue);
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
                    //ddlCommissionApplicableLevel.SelectedValue = "TR";
                    //ddlCommissionApplicableLevel.Enabled = false;
                    foreach (ListItem chkItems in chkListTtansactionType.Items)
                    {
                        //if (chkItems.Value == "SIP" || chkItems.Value == "STB")
                        //{
                        //    chkItems.Enabled = false;
                        //    chkItems.Selected = false;
                        //}
                        //else
                        //{
                        chkItems.Enabled = true;
                        chkItems.Selected = true;
                        //}
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
                        //if (chkItems.Value == "SIP" || chkItems.Value == "STB")
                        //{
                        //    chkItems.Enabled = false;
                        //    chkItems.Selected = false;
                        //}
                        //else
                        chkItems.Selected = true;
                        chkItems.Enabled = true;
                    }
                }
            }
        }

        protected void ddlCommissionType_Selectedindexchanged(object sender, EventArgs e)
        {


            DropDownList ddlCommissionType = (DropDownList)sender;
            Label lblReceivableFrequency = new Label();
            DropDownList ddlReceivableFrequency = new DropDownList();
            System.Web.UI.HtmlControls.HtmlTableRow trTransactionTypeSipFreq = new System.Web.UI.HtmlControls.HtmlTableRow();
            System.Web.UI.HtmlControls.HtmlTableRow trMinMaxTenure = new System.Web.UI.HtmlControls.HtmlTableRow(); ;
            System.Web.UI.HtmlControls.HtmlTableRow trMinMaxAge = new System.Web.UI.HtmlControls.HtmlTableRow(); ;
            System.Web.UI.HtmlControls.HtmlTableCell tdlb1MinNumberOfApplication = new System.Web.UI.HtmlControls.HtmlTableCell(); ;
            System.Web.UI.HtmlControls.HtmlTableCell tdtxtMinNumberOfApplication = new System.Web.UI.HtmlControls.HtmlTableCell(); ;
            System.Web.UI.HtmlControls.HtmlTableCell tdlb1MaxNumberOfApplication = new System.Web.UI.HtmlControls.HtmlTableCell(); ;
            System.Web.UI.HtmlControls.HtmlTableCell tdtxtMaxNumberOfApplication = new System.Web.UI.HtmlControls.HtmlTableCell(); ;
            System.Web.UI.HtmlControls.HtmlTableRow trMinAndMaxNumberOfApplication = new System.Web.UI.HtmlControls.HtmlTableRow(); ;
            System.Web.UI.HtmlControls.HtmlTableCell tdlb1SipFreq = new System.Web.UI.HtmlControls.HtmlTableCell(); ;
            System.Web.UI.HtmlControls.HtmlTableCell tdddlSipFreq = new System.Web.UI.HtmlControls.HtmlTableCell(); ;
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
           
            if (ddlCommissionType.NamingContainer is Telerik.Web.UI.GridEditFormItem)
            {
                GridEditFormItem gdi;
              
                gdi = (GridEditFormItem)ddlCommissionType.NamingContainer;
                lblTransactionType = (Label)gdi.FindControl("lblTransactionType");
              
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
                tdlb1SipFreq = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlb1SipFreq");
                tdddlSipFreq = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdddlSipFreq");

            }
            else if (ddlCommissionType.NamingContainer is Telerik.Web.UI.GridEditFormInsertItem)
            {
                GridEditFormInsertItem gdi;
                gdi = (GridEditFormInsertItem)ddlCommissionType.NamingContainer;
                lblTransactionType = (Label)gdi.FindControl("lblTransactionType");
            
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
                tdlb1SipFreq = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdlb1SipFreq");
                tdddlSipFreq = (System.Web.UI.HtmlControls.HtmlTableCell)gdi.FindControl("tdddlSipFreq");

            }



            //lblReceivableFrequency.Visible = !enablement;
            //ddlReceivableFrequency.Visible = !enablement;
            //trTransactionTypeSipFreq.Visible = !enablement;
            //trMinMaxTenure.Visible = !enablement;
            //trMinMaxAge.Visible = !enablement;
            //tdMinNumberOfApplication.Visible = !enablement;



            ShowAndHideSTructureRuleControlsBasedOnProductAndCommisionType(lblReceivableFrequency, ddlReceivableFrequency, trTransactionTypeSipFreq, tdlb1SipFreq, tdddlSipFreq, trMinMaxTenure, trMinMaxAge, tdlb1MinNumberOfApplication, tdtxtMinNumberOfApplication, ddlProductType.SelectedValue, ddlCommissionType.SelectedValue
                 , lblMinNumberOfApplication, txtMinNumberOfApplication, lblSIPFrequency, ddlSIPFrequency, ddlTransaction, chkListTtansactionType, lblTransactionType, ddlCommisionCalOn, ddlCommissionApplicableLevel,
                 lblMaxNumberOfApplication, tdlb1MaxNumberOfApplication, tdtxtMaxNumberOfApplication, txtMaxNumberOfApplication, trMinAndMaxNumberOfApplication);
        }


        protected void RadGridStructureRule_ItemDataBound(object sender, GridItemEventArgs e)
        {



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
               
                DropDownList ddlTenureFrequency = (DropDownList)editform.FindControl("ddlTenureFrequency");
                DropDownList ddlInvestAgeTenure = (DropDownList)editform.FindControl("ddlInvestAgeTenure");

                DropDownList ddlBrokerageUnit = (DropDownList)editform.FindControl("ddlBrokerageUnit");
                DropDownList ddlCommisionCalOn = (DropDownList)editform.FindControl("ddlCommisionCalOn");
                //DropDownList ddlAUMFrequency = (DropDownList)editform.FindControl("ddlAUMFrequency");

                DropDownList ddlSIPFrequency = (DropDownList)editform.FindControl("ddlSIPFrequency");
                DropDownList ddlAppCityGroup = (DropDownList)e.Item.FindControl("ddlAppCityGroup");
                DropDownList ddlTransaction = (DropDownList)e.Item.FindControl("ddlTransaction");
                DropDownList ddlReceivableFrequency = (DropDownList)e.Item.FindControl("ddlReceivableFrequency");
                DropDownList ddlCommissionApplicableLevel = (DropDownList)e.Item.FindControl("ddlCommissionApplicableLevel");
                CheckBoxList chkListApplyTax = (CheckBoxList)editform.FindControl("chkListApplyTax");
                TextBox txtTaxValue = (TextBox)editform.FindControl("txtTaxValue");
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
                if (dsCommissionLookup != null)
                {


                    ddlCommissionApplicableLevel.DataSource = dsCommissionLookup.Tables[1];
                    ddlCommissionApplicableLevel.DataValueField = dsCommissionLookup.Tables[1].Columns["WCAL_ApplicableLEvelCode"].ToString();
                    ddlCommissionApplicableLevel.DataTextField = dsCommissionLookup.Tables[1].Columns["WCAL_ApplicableLEvel"].ToString();
                    ddlCommissionApplicableLevel.DataBind();
                    ddlCommissionApplicableLevel.SelectedValue = "TR";
                    //ddlCommissionApplicableLevel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                    ddlAppCityGroup.DataSource = dsCommissionLookup.Tables[7];
                    ddlAppCityGroup.DataValueField = dsCommissionLookup.Tables[7].Columns["ACG_CityGroupID"].ToString();
                    ddlAppCityGroup.DataTextField = dsCommissionLookup.Tables[7].Columns["ACG_CityGroupName"].ToString();
                    ddlAppCityGroup.DataBind();
                    ddlAppCityGroup.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                    ddlReceivableFrequency.DataSource = dsCommissionLookup.Tables[5];
                    ddlReceivableFrequency.DataValueField = dsCommissionLookup.Tables[5].Columns["XF_FrequencyCode"].ToString();
                    ddlReceivableFrequency.DataTextField = dsCommissionLookup.Tables[5].Columns["XF_Frequency"].ToString();
                    ddlReceivableFrequency.DataBind();
                    //ddlReceivableFrequency.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                    ddlReceivableFrequency.SelectedValue = "MN";

                    ddlCommissionType.DataSource = dsCommissionLookup.Tables[2];
                    ddlCommissionType.DataValueField = dsCommissionLookup.Tables[2].Columns["WCT_CommissionTypeCode"].ToString();
                    ddlCommissionType.DataTextField = dsCommissionLookup.Tables[2].Columns["WCT_CommissionType"].ToString();
                    ddlCommissionType.DataBind();
                    ddlCommissionType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                    //ddlBrokerageUnit.DataSource = dsCommissionLookup.Tables[3];
                    //ddlBrokerageUnit.DataValueField = dsCommissionLookup.Tables[3].Columns["WCU_UnitCode"].ToString();
                    //ddlBrokerageUnit.DataTextField = dsCommissionLookup.Tables[3].Columns["WCU_Unit"].ToString();
                    //ddlBrokerageUnit.DataBind();
                    //ddlBrokerageUnit.SelectedValue = "PER";

                    ddlCommisionCalOn.DataSource = dsCommissionLookup.Tables[4];
                    ddlCommisionCalOn.DataValueField = dsCommissionLookup.Tables[4].Columns["WCCO_Calculatedoncode"].ToString();
                    ddlCommisionCalOn.DataTextField = dsCommissionLookup.Tables[4].Columns["WCCO_CalculatedOn"].ToString();
                    ddlCommisionCalOn.DataBind();

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
                    //string strInvestmentAgeUnit=RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_InvestmentAgeUnit"].ToString();
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



                    ddlAppCityGroup.SelectedValue = strCityGroupID;
                    ddlReceivableFrequency.SelectedValue = strReceivableRuleFrequency;
                    ddlCommissionApplicableLevel.SelectedValue = strApplicableLevelCode;

                    foreach (ListItem chkItems in chkListApplyTax.Items)
                    {
                        if (chkItems.Value == "ServiceTax" & strIsServiceTaxReduced == "1")
                            chkItems.Selected = true;
                        else if (chkItems.Value == "TDS" & strIsTDSReduced == "1")
                            chkItems.Selected = true;
                        else if (chkItems.Value == "Others" & strIsOtherTaxReduced == "1")
                            chkItems.Selected = true;
                    }



                    ddlCommissionType.SelectedValue = strCommissionType;
                    ShowAndHideSTructureRuleControlsBasedOnProductAndCommisionType( lblReceivableFrequency, ddlReceivableFrequency, trTransactionTypeSipFreq, tdlb1SipFreq, tdddlSipFreq, trMinMaxTenure, trMinMaxAge, tdMinNumberOfApplication, tdtxtMinNumberOfApplication1, ddlProductType.SelectedValue, ddlCommissionType.SelectedValue
                        , lblMinNumberOfApplication, txtMinNumberOfApplication, lblSIPFrequency, ddlSIPFrequency, ddlTransaction, chkListTtansactionType, lblTransactionType, ddlCommisionCalOn, ddlCommissionApplicableLevel
                        , lblMaxNumberOfApplication, tdlb1MaxNumberOfApplication, tdtxtMaxNumberOfApplication, txtMaxNumberOfApplication, trMinAndMaxNumberOfApplication);


                    ddlInvestorType.SelectedValue = strCustomerCategory;
                    ddlTenureFrequency.SelectedValue = strTenureUnit;
                    ddlInvestAgeTenure.SelectedValue = "Months";
                    // ddlBrokerageUnit.SelectedValue = strBrokargeUnit;
                    ddlCommisionCalOn.SelectedValue = strCalculatedOn;
                    chkListTtansactionType.Visible = true;
                    //ddlAUMFrequency.SelectedValue = strAUMFrequency;
                    if (strCommissionType == "IN" && (strInvestmentTransactionType.Contains("SIP") || strInvestmentTransactionType.Contains("STB")))
                    {
                        ddlTransaction.Visible = true;
                        ddlTransaction.SelectedValue = "SIP";
                        foreach (ListItem chkItems in chkListTtansactionType.Items)
                        {
                            if (chkItems.Value == "SIP" || chkItems.Value == "STB")
                                chkItems.Enabled = true;
                            else
                                chkItems.Enabled = false;
                        }

                    }
                    else if (strCommissionType == "IN")
                    {
                        foreach (ListItem chkItems in chkListTtansactionType.Items)
                        {
                            //if (chkItems.Value == "SIP" || chkItems.Value == "STB")
                            //    chkItems.Enabled = false;
                            //else
                            chkItems.Enabled = true;
                            chkItems.Selected = true;
                        }
                        ddlTransaction.Visible = true;

                        ddlTransaction.SelectedValue = "NonSIP";
                    }
                    foreach (ListItem chkItems in chkListTtansactionType.Items)
                    {
                        if (strInvestmentTransactionType.Contains(chkItems.Value))
                            chkItems.Selected = true;
                    }
                    ddlSIPFrequency.SelectedValue = strSIPFrequency;
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
            RadGridStructureRule.DataSource = dsStructureRules.Tables[0];
            RadGridStructureRule.DataBind();
            Cache.Insert(userVo.UserId.ToString() + "CommissionStructureRule", dsStructureRules);

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
            if (string.IsNullOrEmpty(chkItemType))
                CustomValidator4.Enabled = true;  
            //if (ddlProductType.SelectedValue == "MF")
            //{
            //    ShowHideControlsForRules(true, e);
            //}
            //else
            //    ShowHideControlsForRules(false, e);
            /*******************COLLECT DATA********************/
            commissionStructureRuleVo = CollectDataForCommissionStructureRule(e);

            /*******************UI VALIDATION********************/
            //isPageValid = ValidatePage(commissionStructureRuleVo);
            if (!string.IsNullOrEmpty(chkItemType))
            {
                if (isPageValid)
                {

                    string sbRuleHash = commisionReceivableBo.GetHash(commissionStructureRuleVo);
                    if (commisionReceivableBo.hasRule(commissionStructureRuleVo, sbRuleHash))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Rule already exists');", true);
                        return;
                    }
                    commissionStructureRuleVo.CommissionStructureRuleId = Convert.ToInt32(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_CommissionStructureRuleId"].ToString());
                    commisionReceivableBo.UpdateCommissionStructureRule(commissionStructureRuleVo, userVo.UserId, sbRuleHash);
                    BindCommissionStructureRuleGrid(Convert.ToInt32(hidCommissionStructureName.Value));

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('AUM For is Required !');", true);
                    e.Canceled = true;
                    return;
                }
            }
            else
            {
                CustomValidator4.Enabled = true;  
            }
        }

        protected void RadGridStructureRule_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                
            }
        }

        protected void rgPayableMapping_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                object obj = e.CommandArgument;
                string ID = obj.ToString();
            }
        }
        protected void RadGridStructureRule_InsertCommand(object source, GridCommandEventArgs e)
        {
            bool isPageValid = true;
            try
            {
                // GridEditFormItem editform = (GridEditFormItem)e.Item;

                /*******************COLLECT DATA********************/
                commissionStructureRuleVo = CollectDataForCommissionStructureRule(e);

                /*******************UI VALIDATION********************/
                //isPageValid = ValidatePage(commissionStructureRuleVo);
            
                /*******************DUPLICATE CHECK********************/
                //bool isValidRule = true;
               
                    if (isPageValid)
                    {
                        string sbRuleHash = commisionReceivableBo.GetHash(commissionStructureRuleVo);
                        if (commisionReceivableBo.hasRule(commissionStructureRuleVo, sbRuleHash))
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Rule already exists');", true);
                            return;
                        }

                        commisionReceivableBo.CreateCommissionStructureRule(commissionStructureRuleVo, userVo.UserId, sbRuleHash.ToString());
                        BindCommissionStructureRuleGrid(Convert.ToInt32(hidCommissionStructureName.Value));
                        //HiddenField1.Value = hidCommissionStructureName.Value;

                        //RadGrid rgCommissionTypeCaliculation = (RadGrid)e.Item.FindControl("rgCommissionTypeCaliculation");
                        //rgCommissionTypeCaliculation.Visible = true;
                        //DataSet dsLookupData;
                        //dsLookupData = commisionReceivableBo.GetCommissionTypeBrokerage();
                        //rgCommissionTypeCaliculation.DataSource = dsLookupData;
                        //rgCommissionTypeCaliculation.DataBind();
                        //e.Canceled = true;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('AUM For is Required !');", true);
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Duplicate commission structure rule');", true);
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
                //TextBox txtAUMFor = (TextBox)e.Item.FindControl("txtAUMFor");
                //DropDownList ddlAUMFrequency = (DropDownList)e.Item.FindControl("ddlAUMFrequency");

                TextBox txtMinNumberOfApplication = (TextBox)e.Item.FindControl("txtMinNumberOfApplication");
                TextBox txtMaxNumberOfApplication = (TextBox)e.Item.FindControl("txtMaxNumberOfApplication");
                TextBox txtStruRuleComment = (TextBox)e.Item.FindControl("txtStruRuleComment");

                DropDownList ddlAppCityGroup = (DropDownList)e.Item.FindControl("ddlAppCityGroup");
                DropDownList ddlReceivableFrequency = (DropDownList)e.Item.FindControl("ddlReceivableFrequency");
                DropDownList ddlCommissionApplicableLevel = (DropDownList)e.Item.FindControl("ddlCommissionApplicableLevel");
                CheckBoxList chkListApplyTax = (CheckBoxList)e.Item.FindControl("chkListApplyTax");
                TextBox txtTaxValue = (TextBox)e.Item.FindControl("txtTaxValue");

                commissionStructureRuleVo.CommissionStructureId = Convert.ToInt32(hidCommissionStructureName.Value);

                commissionStructureRuleVo.CommissionStructureRuleName = TxtRuleName.Text;

                commissionStructureRuleVo.CommissionType = ddlCommissionType.SelectedValue;
                commissionStructureRuleVo.CustomerType = ddlInvestorType.SelectedValue;

                commissionStructureRuleVo.AdviserCityGroupCode = ddlAppCityGroup.SelectedValue;
                commissionStructureRuleVo.ReceivableFrequency = ddlReceivableFrequency.SelectedValue;
                commissionStructureRuleVo.ApplicableLevelCode = ddlCommissionApplicableLevel.SelectedValue;


                if (chkListApplyTax.Items[0].Selected)
                    commissionStructureRuleVo.IsServiceTaxReduced = true;
                if (chkListApplyTax.Items[1].Selected)
                    commissionStructureRuleVo.IsTDSReduced = true;
                if (chkListApplyTax.Items[2].Selected)
                    commissionStructureRuleVo.IsOtherTaxReduced = true;



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

                //if (!string.IsNullOrEmpty(txtBrokerageValue.Text.Trim()))
                //{
                //    commissionStructureRuleVo.BrokerageValue = Convert.ToDecimal(txtBrokerageValue.Text.Trim());
                //    commissionStructureRuleVo.BrokerageUnitCode = ddlBrokerageUnit.SelectedValue;
                //}

                commissionStructureRuleVo.CalculatedOnCode = ddlCommisionCalOn.SelectedValue;
                //if (ddlCommisionCalOn.SelectedValue == "AGAM" || ddlCommisionCalOn.SelectedValue == "AUM" || ddlCommisionCalOn.SelectedValue == "CLAM")
                //{
                //    if (!string.IsNullOrEmpty(txtAUMFor.Text.Trim()))
                //        commissionStructureRuleVo.AUMMonth = Convert.ToDecimal(txtAUMFor.Text.Trim());
                //    //commissionStructureRuleVo.AUMFrequency = ddlAUMFrequency.SelectedValue;
                //}

                if (!string.IsNullOrEmpty(txtMinNumberOfApplication.Text.Trim()))
                    commissionStructureRuleVo.MinNumberofApplications = Convert.ToInt16(txtMinNumberOfApplication.Text.Trim());
                if (!string.IsNullOrEmpty(txtMaxNumberOfApplication.Text.Trim()))
                    commissionStructureRuleVo.MaxNumberofApplications = Convert.ToInt32(txtMaxNumberOfApplication.Text.Trim());

                if (!string.IsNullOrEmpty(txtTaxValue.Text.Trim()))
                    commissionStructureRuleVo.TaxValue = Convert.ToDecimal(txtTaxValue.Text.Trim());

                if (!string.IsNullOrEmpty(txtStruRuleComment.Text.Trim()))
                    commissionStructureRuleVo.StructureRuleComment = txtStruRuleComment.Text.Trim();
                commissionStructureRuleVo.AdviserId = rmVo.AdviserId;
                
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
            DataSet dsCommissionStructureRule = new DataSet();
            if (Cache[userVo.UserId.ToString() + "CommissionStructureRule"] != null)
            {
                dsCommissionStructureRule = (DataSet)Cache[userVo.UserId.ToString() + "CommissionStructureRule"];
                RadGridStructureRule.DataSource = dsCommissionStructureRule.Tables[0];
            }
        }

        private void ShowAndHideSTructureRuleControlsBasedOnProductAndCommisionType(Label lblReceivableFrequency, DropDownList ddlReceivableFrequency, System.Web.UI.HtmlControls.HtmlTableRow trTransactionTypeSipFreq, System.Web.UI.HtmlControls.HtmlTableCell tdlb1SipFreq, System.Web.UI.HtmlControls.HtmlTableCell tdddlSipFreq, System.Web.UI.HtmlControls.HtmlTableRow trMinMaxTenure, System.Web.UI.HtmlControls.HtmlTableRow trMinMaxAge, System.Web.UI.HtmlControls.HtmlTableCell tdMinNumberOfApplication, System.Web.UI.HtmlControls.HtmlTableCell tdtxtMinNumberOfApplication1, string product, string CommisionType
            , Label lblMinNumberOfApplication, TextBox txtMinNumberOfApplication, Label lblSIPFrequency, DropDownList ddlSIPFrequency, DropDownList ddlTransaction, CheckBoxList chkListTtansactionType, Label lblTransactionType, DropDownList ddlCommisionCalOn, DropDownList ddlCommissionApplicableLevel
            , Label lblMaxNumberOfApplication, System.Web.UI.HtmlControls.HtmlTableCell tdlb1MaxNumberOfApplication, System.Web.UI.HtmlControls.HtmlTableCell tdtxtMaxNumberOfApplication, TextBox txtMaxNumberOfApplication, System.Web.UI.HtmlControls.HtmlTableRow trMinAndMaxNumberOfApplication)
        {
            bool enablement = false;
            lblSIPFrequency.Visible = enablement;
            ddlSIPFrequency.Visible = enablement;
           
           
            
            //GridEditableItem editedItem = chkBuyAvailability.NamingContainer as GridEditableItem;
            //RadGrid rgSeriesCat = (RadGrid)editedItem.FindControl("rgSeriesCat");
            if (product == "FI" || product == "IP")
            {
               
                      
               
                lblReceivableFrequency.Visible = enablement;
                ddlReceivableFrequency.Visible = enablement;
                trTransactionTypeSipFreq.Visible = enablement;
                trMinMaxTenure.Visible = enablement;
                tdMinNumberOfApplication.Visible = enablement;
                trMinMaxAge.Visible = enablement;
                //lblMinNumberOfApplication.Visible = enablement;

                //tdtxtMinNumberOfApplication1.Visible = enablement;
                //lblMaxNumberOfApplication.Visible = enablement;

                //tdtxtMaxNumberOfApplication.Visible = enablement;
                trMinAndMaxNumberOfApplication.Visible = enablement;
                if (CommisionType == "UF")
                {
                    ddlCommissionApplicableLevel.SelectedValue = "TR";
                    ddlCommissionApplicableLevel.Enabled = false;

                    ddlCommisionCalOn.Items[0].Enabled = false;


                }
                else if (CommisionType == "TC")
                {
                    ddlCommissionApplicableLevel.Enabled = true;
                    tdMinNumberOfApplication.Visible = !enablement;
                    //lblMinNumberOfApplication.Visible = !enablement;
                    //tdtxtMinNumberOfApplication1.Visible = !enablement;
                    lblReceivableFrequency.Visible = !enablement;
                    ddlReceivableFrequency.Visible = !enablement;
                    //lblMaxNumberOfApplication.Visible = !enablement;
                    //tdtxtMaxNumberOfApplication.Visible = !enablement;
                    trMinAndMaxNumberOfApplication.Visible = !enablement;
                }
                else if (CommisionType == "IN")
                {
                    ddlCommissionApplicableLevel.SelectedValue = "AD";
                    ddlCommissionApplicableLevel.Enabled = false;
                    tdMinNumberOfApplication.Visible = !enablement;
                    //lblMinNumberOfApplication.Visible = !enablement;
                    //tdtxtMinNumberOfApplication1.Visible = !enablement;
                    //lblMaxNumberOfApplication.Visible = !enablement;
                    //tdtxtMaxNumberOfApplication.Visible = !enablement;
                    trMinAndMaxNumberOfApplication.Visible = !enablement;
                }
            }
            else if (product == "MF")
            {
                
                

                trMinMaxTenure.Visible = enablement;
                trMinMaxAge.Visible = enablement;
                //tdMinNumberOfApplication.Visible = enablement;
                //lblMinNumberOfApplication.Visible = enablement;
                //tdtxtMinNumberOfApplication1.Visible = enablement;
                trMinAndMaxNumberOfApplication.Visible = enablement;
                lblReceivableFrequency.Visible = enablement;
                ddlReceivableFrequency.Visible = enablement;
                ddlCommisionCalOn.Enabled = true;
                if (CommisionType == "IN")
                {
                    //ddlCommissionApplicableLevel.SelectedValue = "AD";
                    //ddlCommissionApplicableLevel.Enabled = false;
                    trTransactionTypeSipFreq.Visible = !enablement;
                    trMinMaxTenure.Visible = !enablement;
                    //tdMinNumberOfApplication.Visible = !enablement;
                    //lblMinNumberOfApplication.Visible = !enablement;
                    //lblMaxNumberOfApplication.Visible = !enablement;
                    //tdtxtMaxNumberOfApplication.Visible = !enablement;
                    //tdtxtMinNumberOfApplication1.Visible = !enablement;
                    //tdtxtMaxNumberOfApplication.Visible = !enablement;
                    trMinAndMaxNumberOfApplication.Visible = !enablement;
                    chkListTtansactionType.Visible = enablement;
                    lblTransactionType.Visible = !enablement;
                    ddlTransaction.Visible = !enablement;
                    ddlCommisionCalOn.Items[0].Enabled = false;
                    foreach (ListItem chkItems in chkListTtansactionType.Items)
                    {
                        //if (chkItems.Value == "SIP" || chkItems.Value == "STB")
                        //{
                        //    chkItems.Enabled = false;
                        //    chkItems.Selected = false;
                        //}
                        //else
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
                    lblReceivableFrequency.Visible = !enablement;
                    ddlReceivableFrequency.Visible = !enablement;
                    chkListTtansactionType.Visible = !enablement;
                    lblTransactionType.Visible = !enablement;
                    ddlTransaction.Visible = enablement;
                    ddlCommisionCalOn.Items[0].Enabled = true;
                    ddlCommisionCalOn.SelectedValue = "AGAM";
                    ddlCommisionCalOn.Enabled = false;
                    foreach (ListItem chkItems in chkListTtansactionType.Items)
                    {
                        ////if (chkItems.Value == "SIP" || chkItems.Value == "STB")
                        ////{
                        //    chkItems.Enabled = false;
                        //    chkItems.Selected = false;
                        //}
                        //else
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
                    ddlTransaction.Visible = enablement;
                    ddlCommissionApplicableLevel.Enabled = true;
                    foreach (ListItem chkItems in chkListTtansactionType.Items)
                    {
                        chkItems.Selected = true;
                    }
                }
                //else
                //{
                //    tdlb1SipFreq.Visible = !enablement;
                //    tdddlSipFreq.Visible = !enablement;
                //    lblReceivableFrequency.Visible = enablement;
                //    ddlReceivableFrequency.Visible = enablement;
                //    tdMinNumberOfApplication.Visible = enablement;
                //    tdtxtMinNumberOfApplication1.Visible = enablement;

                //}
            }
            //else
            //{
            //    lblReceivableFrequency.Visible = !enablement;
            //    ddlReceivableFrequency.Visible = !enablement;
            //    trTransactionTypeSipFreq.Visible = !enablement;
            //    trMinMaxTenure.Visible = !enablement;
            //    trMinMaxAge.Visible = !enablement;
            //    tdtxtMinNumberOfApplication1.Visible = !enablement;
            //    tdtxtMinNumberOfApplication1.Visible = !enablement;
            //}

        }

        //private void ShowAndHideSTructureRuleControlsBasedOnCommissionTypeAndLevel (Label lblReceivableFrequency, DropDownList ddlReceivableFrequency, System.Web.UI.HtmlControls.HtmlTableRow trTransactionTypeSipFreq, System.Web.UI.HtmlControls.HtmlTableRow trMinMaxTenure, System.Web.UI.HtmlControls.HtmlTableRow trMinMaxAge, System.Web.UI.HtmlControls.HtmlTableCell tdMinNumberOfApplication, string product, string CommisionType)
        //{
        //    bool enablement = false;

        //      if (product == "MF")
        //    {
        //        //if (CommisionType == "IN")
        //        //{
        //        //    tdMinNumberOfApplication.Visible = !enablement;
        //        //}
        //        if (CommisionType == "Trail Commission")
        //        {
        //            lblReceivableFrequency.Visible = !enablement;
        //            ddlReceivableFrequency.Visible = !enablement;
        //        }
        //        //else if (CommisionType == "upfront")
        //        //{
        //        //    trTransactionTypeSipFreq.Visible = !enablement;

        //        //}
        //        else
        //        {
        //            lblReceivableFrequency.Visible = enablement;
        //            ddlReceivableFrequency.Visible = enablement;
        //            tdMinNumberOfApplication.Visible = enablement;
        //        }
        //    }
        //    else
        //    {
        //        lblReceivableFrequency.Visible = !enablement;
        //        ddlReceivableFrequency.Visible = !enablement;
        //        trTransactionTypeSipFreq.Visible = !enablement;
        //        trMinMaxTenure.Visible = !enablement;
        //        trMinMaxAge.Visible = !enablement;
        //        tdMinNumberOfApplication.Visible = !enablement;

        //    }

        //}

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
                // ddlCommissionype.SelectedValue = commissionStructureMasterVo.CommissionLookUpId.ToString();
                ShowHideControlsBasedOnProduct(ddlProductType.SelectedValue);
                foreach (RadListBoxItem item in rlbAssetSubCategory.Items)
                {
                    item.Checked = false;
                }
                foreach (RadListBoxItem item in rlbAssetSubCategory.Items)
                {
                    if (commissionStructureMasterVo.AssetSubCategory.ToString().Contains(item.Value))
                    {
                        item.Checked = true;
                    }

                }
                ddlIssuer.SelectedValue = commissionStructureMasterVo.Issuer;
              
                txtValidityFrom.Text = commissionStructureMasterVo.ValidityStartDate.ToShortDateString();
                txtValidityTo.Text = commissionStructureMasterVo.ValidityEndDate.ToShortDateString();
                txtStructureName.Text = commissionStructureMasterVo.CommissionStructureName;
                chkHasClawBackOption.Checked = commissionStructureMasterVo.IsClawBackApplicable;
                chkMoneytaryReward.Checked = commissionStructureMasterVo.IsNonMonetaryReward;
                //chkListApplyTax.Items[0].Selected = commissionStructureMasterVo.IsServiceTaxReduced;
                //chkListApplyTax.Items[1].Selected = commissionStructureMasterVo.IsTDSReduced;
                //chkListApplyTax.Items[2].Selected = commissionStructureMasterVo.IsOtherTaxReduced;
                txtNote.Text = commissionStructureMasterVo.StructureNote;
                hidCommissionStructureName.Value = structureId.ToString();
                CommissionStructureControlsEnable(false);
                //sai  MapPingLinksBasedOnCpmmissionTypes(ddlCommissionype.SelectedValue);

                hdnProductId.Value = commissionStructureMasterVo.ProductType;
                hdnStructValidFrom.Value = commissionStructureMasterVo.ValidityStartDate.ToShortDateString();
                hdnStructValidTill.Value = commissionStructureMasterVo.ValidityEndDate.ToShortDateString();
                hdnIssuerId.Value = commissionStructureMasterVo.Issuer;
                hdnCategoryId.Value = commissionStructureMasterVo.AssetCategory;
                string subcategoryIds = commissionStructureMasterVo.AssetSubCategory.ToString();
                subcategoryIds = subcategoryIds.Replace("~", ",");
                hdnSubcategoryIds.Value = subcategoryIds;
                BindBondCategories();
                  ddlSubInstrCategory.SelectedValue = commissionStructureMasterVo.AssetSubCategory.ToString();
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
                //sai ddlCommissionype.Enabled = true;
                ddlProductType.Enabled = true;
                ddlCategory.Enabled = true;
                rlbAssetSubCategory.Enabled = true;
                ddlIssuer.Enabled = true;
                //ddlCommissionApplicableLevel.Enabled = true;
                txtValidityFrom.Enabled = true;
                txtValidityTo.Enabled = true;
                txtStructureName.Enabled = true;
                chkHasClawBackOption.Enabled = true;
                chkMoneytaryReward.Enabled = true;
                //chkListApplyTax.Enabled = true;
                //ddlAppCityGroup.Enabled = true;
                //ddlReceivableFrequency.Enabled = true;
                txtNote.Enabled = true;

                lnkEditStructure.Text = "View";
                lnkEditStructure.ToolTip = "View commission structure section";
                btnStructureSubmit.Visible = false;
                btnStructureUpdate.Visible = true;


            }
            else
            {
                //sai  ddlCommissionype.Enabled = false;
                ddlProductType.Enabled = false;
                ddlCategory.Enabled = false;
                rlbAssetSubCategory.Enabled = false;
                ddlIssuer.Enabled = false;
                //ddlCommissionApplicableLevel.Enabled = false;
                txtValidityFrom.Enabled = false;
                txtValidityTo.Enabled = false;
                txtStructureName.Enabled = false;
                chkHasClawBackOption.Enabled = false;
                chkMoneytaryReward.Enabled = false;
                //chkListApplyTax.Enabled = false;
                //ddlAppCityGroup.Enabled = false;
                //ddlReceivableFrequency.Enabled = false;
                txtNote.Enabled = false;

                lnkEditStructure.Visible = true;
                lnkEditStructure.Text = "Edit";
                lnkEditStructure.ToolTip = "Edit commission structure section";
                lnkAddNewStructure.Visible = true;
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
            //ddlCommissionApplicableLevel.SelectedIndex = 0;
            txtValidityFrom.Text = string.Empty;
            txtValidityTo.Text = string.Empty;
            txtStructureName.Text = string.Empty;
            chkHasClawBackOption.Checked = false;
            chkMoneytaryReward.Checked = false;
            //foreach (ListItem item in chkListApplyTax.Items)
            //{
            //    item.Selected = false;
            //}
            //ddlAppCityGroup.SelectedIndex = 0;
            //ddlReceivableFrequency.SelectedIndex = 0;
            txtNote.Text = string.Empty;
            CommissionStructureControlsEnable(true);
            btnStructureSubmit.Visible = true;
            btnStructureUpdate.Visible = false;
            lnkEditStructure.Visible = false;
            lnkAddNewStructure.Visible = false;


            if (Cache[userVo.UserId.ToString() + "CommissionStructureRule"] != null)
                Cache.Remove(userVo.UserId.ToString() + "CommissionStructureRule");


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
            DataSet dsStructureRules = new DataSet();
            dsStructureRules.Tables.Add(CreateCommissionStructureRuleDataTable());
            RadGridStructureRule.DataSource = dsStructureRules;
            RadGridStructureRule.Rebind();
            Cache.Insert(userVo.UserId.ToString() + "CommissionStructureRule", dsStructureRules);
        }

        private DataTable CreateCommissionStructureRuleDataTable()
        {
            DataTable dtCommissionStructureRule = new DataTable();
            dtCommissionStructureRule.Columns.Add("WCT_CommissionType");
            dtCommissionStructureRule.Columns.Add("XCT_CustomerTypeName");
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
            dtCommissionStructureRule.Columns.Add("ACSR_BrokerageValue");
            dtCommissionStructureRule.Columns.Add("WCU_Unit");
            dtCommissionStructureRule.Columns.Add("WCCO_CalculatedOn");
            dtCommissionStructureRule.Columns.Add("ACSM_AUMFrequency");
            dtCommissionStructureRule.Columns.Add("ACSR_AUMMonth");
            dtCommissionStructureRule.Columns.Add("ACG_CityGroupName");
            dtCommissionStructureRule.Columns.Add("ACSR_Comment");
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
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('AUM For is Required !');", true);
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


        //private void getAllStructures()
        //{
        //    DataSet dsAllStructs;
        //    try
        //    {
        //        dsAllStructs = commisionReceivableBo.GetAdviserCommissionStructureRules(advisorVo.advisorId);
        //        DataRow drStructs = dsAllStructs.Tables[0].NewRow();
        //        drStructs["ACSM_CommissionStructureId"] = 0;
        //        drStructs["ACSM_CommissionStructureName"] = "-SELECT-";
        //        dsAllStructs.Tables[0].Rows.InsertAt(drStructs, 0);
        //        ddlStructs.DataTextField = dsAllStructs.Tables[0].Columns["ACSM_CommissionStructureName"].ToString();
        //        ddlStructs.DataValueField = dsAllStructs.Tables[0].Columns["ACSM_CommissionStructureId"].ToString();
        //        ddlStructs.DataSource = dsAllStructs.Tables[0];
        //        ddlStructs.DataBind();
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "CommissionStructureToSchemeMapping.ascx.cs:getAllStructures()");
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        //protected void ddlStructs_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (int.Parse(ddlStructs.SelectedValue) == 0) { return; }

        //    hdnStructId.Value = this.ddlStructs.SelectedValue.ToString();
        //    SetStructureDetails();
        //    CreateMappedSchemeGrid();
        //    pnlGrid.Visible = true;
        //}

        //private string convertSubcatListToCSV(List<RadListBoxItem> itemList)
        //{
        //    string strSubcatsList = "";
        //    int nCount = itemList.Count, i = 0;
        //    foreach (RadListBoxItem item in itemList)
        //    {
        //        i++;
        //        strSubcatsList += item.Value;
        //        if (i < nCount) { strSubcatsList += ","; }
        //    }

        //    return strSubcatsList;
        //}

        //private void SetStructureDetails()
        //{
        //    DataSet dsStructDet;
        //    try
        //    {
        //        dsStructDet = commisionReceivableBo.GetStructureDetails(advisorVo.advisorId, int.Parse(hdnStructId.Value));
        //        foreach (DataRow row in dsStructDet.Tables[0].Rows)
        //        {
        //            hdnProductId.Value = row["PAG_AssetGroupCode"].ToString();
        //            hdnStructValidFrom.Value = row["ACSM_ValidityStartDate"].ToString();
        //            hdnStructValidTill.Value = row["ACSM_ValidityEndDate"].ToString();
        //            hdnIssuerId.Value = row["PA_AMCCode"].ToString();
        //            hdnCategoryId.Value = row["PAIC_AssetInstrumentCategoryCode"].ToString();

        //            lbtStructureName.Text = row["ACSM_CommissionStructureName"].ToString();
        //            lbtStructureName.ToolTip = row["ACSM_CommissionStructureName"].ToString();
        //            txtProductName.Text = row["PAG_AssetGroupName"].ToString();
        //            txtProductName.ToolTip = row["PAG_AssetGroupName"].ToString();
        //            txtCategory.Text = row["PAIC_AssetInstrumentCategoryName"].ToString();
        //            txtCategory.ToolTip = row["PAIC_AssetInstrumentCategoryName"].ToString();
        //            txtIssuerName.Text = row["PA_AMCName"].ToString();
        //            txtIssuerName.ToolTip = row["PA_AMCName"].ToString();
        //            txtValidFrom.Text = DateTime.Parse(hdnStructValidFrom.Value).ToShortDateString();
        //            txtValidTo.Text = DateTime.Parse(hdnStructValidTill.Value).ToShortDateString();
        //        }


        //        //Getting the list of subcategories
        //        dsStructDet = commisionReceivableBo.GetSubcategories(advisorVo.advisorId, int.Parse(hdnStructId.Value));
        //        DataTable dtSubcats = dsStructDet.Tables[0];

        //        foreach (DataRow row in dtSubcats.Rows)
        //        {
        //            if (row["PAISC_AssetInstrumentSubCategoryName"].ToString().Trim() == "")
        //                continue;
        //            rlbAssetSubCategory.Items.Add(new RadListBoxItem(row["PAISC_AssetInstrumentSubCategoryName"].ToString().Trim(), row["PAISC_AssetInstrumentSubCategoryCode"].ToString().Trim()));

        //        }
        //        hdnSubcategoryIds.Value = convertSubcatListToCSV(rlbAssetSubCategory.Items.ToList());
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "CommissionStructureToSchemeMapping.ascx.cs:SetStructureDetails()");
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

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
                int sStructId = int.Parse(hidCommissionStructureName.Value);
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

            //Perform validations
            this.Page.Validate("availSchemesPeriod");
            if (!this.Page.IsValid) { return; }
            tbSchemeMapping.Visible = true;
            tbSchemeMapped.Visible = true;
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
            int structId = int.Parse(hidCommissionStructureName.Value);
            foreach (RadListBoxItem item in rlbMappedSchemes.Items)
            {
                int schemeId = int.Parse(item.Value);
                if (commisionReceivableBo.checkSchemeAssociationExists(schemeId, structId, rdpMappedFrom.SelectedDate.Value, rdpMappedTill.SelectedDate.Value))
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
                commisionReceivableBo.MapSchemesToStructres(structId, int.Parse(item.Value), rdpMappedFrom.SelectedDate.Value, rdpMappedTill.SelectedDate.Value);
            }
        }

        private void showMapError()
        {
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('ReceivableSetup','StructureId=" + this.hidCommissionStructureName.Value + "');", true);
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
            gvMappedSchemes.ExportSettings.OpenInNewWindow = true;
            gvMappedSchemes.ExportSettings.IgnorePaging = true;
            gvMappedSchemes.ExportSettings.HideStructureColumns = true;
            gvMappedSchemes.ExportSettings.ExportOnlyData = true;
            gvMappedSchemes.ExportSettings.FileName = "MappedSchemes";
            gvMappedSchemes.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvMappedSchemes.MasterTableView.ExportToExcel();


            //DataTable dtMappedSchemes = new DataTable();
            //dtMappedSchemes = (DataTable)Cache[userVo.UserId.ToString() + "MappedSchemes"];
            //if (dtMappedSchemes == null)
            //    return;
            //if (dtMappedSchemes.Rows.Count < 1)
            //    return;
            //gvMappedSchemes.DataSource = dtMappedSchemes;
            //gvMappedSchemes.ExportSettings.OpenInNewWindow = true;
            //gvMappedSchemes.ExportSettings.IgnorePaging = true;
            //gvMappedSchemes.ExportSettings.HideStructureColumns = true;
            //gvMappedSchemes.ExportSettings.ExportOnlyData = true;
            //gvMappedSchemes.ExportSettings.FileName = "MappedSchemes";
            //gvMappedSchemes.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            //gvMappedSchemes.MasterTableView.ExportToExcel();
            //CreateMappedSchemeGrid();
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
            //System.Web.UI.HtmlControls.HtmlTableRow trTransaction = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trTransaction");
            System.Web.UI.HtmlControls.HtmlTableRow trMinMaxTenure = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trMinMaxTenure");
            System.Web.UI.HtmlControls.HtmlTableRow trMinMaxAge = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trMinMaxAge");

            lblInvestorType1.Visible = flag;
            lblAppCityGroup.Visible = flag;
            lblReceivableFrequency.Visible = flag;
            ddlReceivableFrequency.Visible = flag;
            ddlAppCityGroup.Visible = flag;
            ddlInvestorType.Visible = flag;
            trTransactionTypeSipFreq.Visible = flag;
            trMinMaxTenure.Visible = flag;
            trMinMaxAge.Visible = flag;
            //trTransaction.Visible = flag;







        }
        private void DefaultAssignments()
        {
            ddlMapping.SelectedValue = "Associate";
            ddlType.SelectedValue = "UserCategory";
            GetControlsBasedOnType(ddlType.SelectedValue);
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



        private int CreatePayableMapping()
        {

            int mappingId = 0;
            string agentId = "";
            string categoryId = string.Empty;
            if (ddlType.SelectedValue == "Custom")
            {
                foreach (RadListBoxItem ListItem in this.RadListBoxSelectedAgentCodes.Items)
                {
                    agentId = agentId + ListItem.Value.ToString() + ",";
                }
            }
            else
            {
                categoryId = ddlAdviserCategory.SelectedValue;
            }

            commisionReceivableBo.CreatePayableAgentCodeMapping(Convert.ToInt32(hidCommissionStructureName.Value), ddlMapping.SelectedValue, categoryId, agentId, out mappingId);
            return mappingId;

        }
        protected void btnPaybleMapping_Click(object sender, EventArgs e)
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
            dsAdviserBranchList = commisionReceivableBo.GetAdviserAgentCodes(advisorVo.advisorId,ddlMapping.SelectedValue);
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
            dtmappedIssues = commisionReceivableBo.GetIssuesStructureMapings(advisorVo.advisorId, type, issueType, product, 0, structureId, (ddlSubInstrCategory.SelectedValue == "" || ddlSubInstrCategory.SelectedValue == "Select") ? "FIFIIP" : ddlSubInstrCategory.SelectedValue).Tables[0];
            if (dtmappedIssues == null)
                return;
            if (dtmappedIssues.Rows.Count == 0)
                return;

            if (type == "Mapped")
            {
                gvMappedIssueList.DataSource = dtmappedIssues;
                gvMappedIssueList.DataBind();
                pnlIssueList.Visible = true;
                if (Cache[userVo.UserId.ToString() + "MappedIssueList"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "MappedIssueList");

                Cache.Insert(userVo.UserId.ToString() + "MappedIssueList", dtmappedIssues);
            }
            else if (type == "UnMapped")
            {
                
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
                int setupId = Convert.ToInt32(gvMappedIssueList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSTSM_SetupId"].ToString());
                commisionReceivableBo.DeleteIssueMapping(setupId);
                GetUnamppedIssues(ddlIssueType.SelectedValue);
                GetMapped_Unmapped_Issues("Mapped", "");
            }
        }

        protected void btnMAP_Click(object sender, EventArgs e)
        {
            int mappingId;
            if (string.IsNullOrEmpty(ddlUnMappedIssues.SelectedValue))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select Issue');", true);
                return;
            }

            commissionStructureRuleVo.CommissionStructureId = Convert.ToInt32(hidCommissionStructureName.Value);

            commissionStructureRuleVo.IssueId = Convert.ToInt32(ddlUnMappedIssues.SelectedValue);
            commisionReceivableBo.CreateIssuesStructureMapings(commissionStructureRuleVo, out mappingId);
            GetUnamppedIssues(ddlIssueType.SelectedValue);
            GetMapped_Unmapped_Issues("Mapped", "");

        }

    }

}
