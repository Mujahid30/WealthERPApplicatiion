﻿using System;
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

namespace WealthERP.Receivable
{
    public partial class ReceivableSetup : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        RMVo rmVo;
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

                BindAllDropdown();
                if (structureId != 0)
                {
                    LoadStructureDetails(structureId);
                    BindCommissionStructureRuleGrid(structureId);
                }
                else
                {
                    ControlStateNewStructureCreate();
                }
            }

        }

        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubcategoryListBox(ddlCategory.SelectedValue);
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

        protected void BindAllDropdown()
        {
            DataSet dsLookupData;
            dsLookupData = commisionReceivableBo.GetLookupDataForReceivableSetUP(advisorVo.advisorId);

            ddlProductType.DataSource = dsLookupData.Tables[0];
            ddlProductType.DataValueField = dsLookupData.Tables[0].Columns["PAG_AssetGroupCode"].ToString();
            ddlProductType.DataTextField = dsLookupData.Tables[0].Columns["PAG_AssetGroupName"].ToString();
            ddlProductType.DataBind();

            ddlCommissionApplicableLevel.DataSource = dsLookupData.Tables[1];
            ddlCommissionApplicableLevel.DataValueField = dsLookupData.Tables[1].Columns["WCAL_ApplicableLEvelCode"].ToString();
            ddlCommissionApplicableLevel.DataTextField = dsLookupData.Tables[1].Columns["WCAL_ApplicableLEvel"].ToString();
            ddlCommissionApplicableLevel.DataBind();
            ddlCommissionApplicableLevel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            //ddlCommissionType.DataSource = dsLookupData.Tables[2];
            //ddlCommissionType.DataValueField = dsLookupData.Tables[2].Columns["WCT_CommissionTypeCode"].ToString();
            //ddlCommissionType.DataTextField = dsLookupData.Tables[2].Columns["WCT_CommissionType"].ToString();
            //ddlCommissionType.DataBind();

            //ddlBrokerageUnit.DataSource = dsLookupData.Tables[3];
            //ddlBrokerageUnit.DataValueField = dsLookupData.Tables[3].Columns["WCU_UnitCode"].ToString();
            //ddlBrokerageUnit.DataTextField = dsLookupData.Tables[3].Columns["WCU_Unit"].ToString();
            //ddlBrokerageUnit.DataBind();

            //ddlCommisionCalOn.DataSource = dsLookupData.Tables[4];
            //ddlCommisionCalOn.DataValueField = dsLookupData.Tables[4].Columns["WCCO_Calculatedoncode"].ToString();
            //ddlCommisionCalOn.DataTextField = dsLookupData.Tables[4].Columns["WCCO_CalculatedOn"].ToString();
            //ddlCommisionCalOn.DataBind();

            ddlReceivableFrequency.DataSource = dsLookupData.Tables[5];
            ddlReceivableFrequency.DataValueField = dsLookupData.Tables[5].Columns["XF_FrequencyCode"].ToString();
            ddlReceivableFrequency.DataTextField = dsLookupData.Tables[5].Columns["XF_Frequency"].ToString();
            ddlReceivableFrequency.DataBind();
            ddlReceivableFrequency.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            

            ddlIssuer.DataSource = dsLookupData.Tables[6];
            ddlIssuer.DataValueField = dsLookupData.Tables[6].Columns["PA_AMCCode"].ToString();
            ddlIssuer.DataTextField = dsLookupData.Tables[6].Columns["PA_AMCName"].ToString();
            ddlIssuer.DataBind();
            ddlIssuer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlAppCityGroup.DataSource = dsLookupData.Tables[7];
            ddlAppCityGroup.DataValueField = dsLookupData.Tables[7].Columns["ACG_CityGroupID"].ToString();
            ddlAppCityGroup.DataTextField = dsLookupData.Tables[7].Columns["ACG_CityGroupName"].ToString();
            ddlAppCityGroup.DataBind();
            ddlAppCityGroup.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            ddlCategory.DataSource = dsLookupData.Tables[8];
            ddlCategory.DataValueField = dsLookupData.Tables[8].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory.DataTextField = dsLookupData.Tables[8].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            


            //ddlInvestorType.DataSource = dsLookupData.Tables[9];
            //ddlInvestorType.DataValueField = dsLookupData.Tables[8].Columns["XCC_CustomerCategoryCode"].ToString();
            //ddlInvestorType.DataTextField = dsLookupData.Tables[8].Columns["XCC_CustomerCategory"].ToString();
            //ddlInvestorType.DataBind();
            Session["CommissionLookUpData"] = dsLookupData;

        }

        protected void SetStructureMasterControlDefaultValues(string assetType)
        {
            if (assetType == "MF")
            {
                ddlProductType.SelectedValue = "MF";
                ddlProductType.Enabled = false;

                ddlCategory.SelectedValue = "MFDT";
            }
            ddlReceivableFrequency.SelectedValue = "MN";
 
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
                commissionStructureMasterVo.ApplicableLevelCode = ddlCommissionApplicableLevel.SelectedValue;
                commissionStructureMasterVo.ValidityStartDate = Convert.ToDateTime(txtValidityFrom.Text);
                commissionStructureMasterVo.ValidityEndDate = Convert.ToDateTime(txtValidityTo.Text);
                commissionStructureMasterVo.CommissionStructureName = txtStructureName.Text.Trim();

                //receivableStructureMasterVo.CommissionTypeCode = ddlCommissionType.SelectedValue;
                commissionStructureMasterVo.IsClawBackApplicable = chkHasClawBackOption.Checked;
                commissionStructureMasterVo.IsNonMonetaryReward = chkMoneytaryReward.Checked;
                commissionStructureMasterVo.IsServiceTaxReduced = chkListApplyTax.Items[1].Selected;
                commissionStructureMasterVo.IsTDSReduced = chkListApplyTax.Items[2].Selected;
                commissionStructureMasterVo.IsOtherTaxReduced = chkListApplyTax.Items[2].Selected;

                //receivableStructureMasterVo.IsStructureFromIssuer = bool.Parse(chk.Checked.ToString());
                //receivableStructureMasterVo.RecurringiSIPFrequency=ddl

                commissionStructureMasterVo.ReceivableFrequency = ddlReceivableFrequency.SelectedValue;
                commissionStructureMasterVo.StructureNote = txtNote.Text.Trim();
                commissionStructureMasterVo.AdviserCityGroupCode = ddlAppCityGroup.SelectedValue;

                foreach (RadListBoxItem item in rlbAssetSubCategory.Items)
                {
                    if (item.Checked == true)
                    {
                        strSubCategoryCode.Append(item.Value);
                        strSubCategoryCode.Append("~");
                    }
                }

                if (!string.IsNullOrEmpty(strSubCategoryCode.ToString()))
                    strSubCategoryCode.Remove((strSubCategoryCode.Length - 1), 1);

                commissionStructureMasterVo.AssetSubCategory = strSubCategoryCode;

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
            if (!string.IsNullOrEmpty(commissionStructureMasterVo.AssetSubCategory.ToString()))
            {
                commisionReceivableBo.CreateCommissionStructureMastter(commissionStructureMasterVo, userVo.UserId, out commissionStructureId);
                hidCommissionStructureName.Value = commissionStructureId.ToString();
                CommissionStructureControlsEnable(false);
                tblCommissionStructureRule.Visible = true;
                tblCommissionStructureRule1.Visible = true;

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('At least one subcategory required!');", true);
                return;
            }
        }
        protected void btnMapToscheme_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('CommissionStructureToSchemeMapping','ID=" + hidCommissionStructureName.Value + "');", true);
        }
        

        protected void btnStructureUpdate_Click(object sender, EventArgs e)
        {
            commissionStructureMasterVo = CollectStructureMastetrData();
            if (!string.IsNullOrEmpty(commissionStructureMasterVo.AssetSubCategory.ToString()))
            {
                commissionStructureMasterVo.CommissionStructureId = Convert.ToInt32(hidCommissionStructureName.Value);
                commisionReceivableBo.UpdateCommissionStructureMastter(commissionStructureMasterVo, userVo.UserId);
                CommissionStructureControlsEnable(false);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('At least one subcategory required!');", true);
                return;
            }
        }

        protected void RadGridStructureRule_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                DataSet dsCommissionLookup;
                dsCommissionLookup = (DataSet)Session["CommissionLookUpData"];

                GridEditFormItem editform = (GridEditFormItem)e.Item;
                DropDownList ddlCommissionType = (DropDownList)editform.FindControl("ddlCommissionType");
                DropDownList ddlInvestorType = (DropDownList)editform.FindControl("ddlInvestorType");

                DropDownList ddlTenureFrequency = (DropDownList)editform.FindControl("ddlTenureFrequency");
                DropDownList ddlInvestAgeTenure = (DropDownList)editform.FindControl("ddlInvestAgeTenure");

                DropDownList ddlBrokerageUnit = (DropDownList)editform.FindControl("ddlBrokerageUnit");
                DropDownList ddlCommisionCalOn = (DropDownList)editform.FindControl("ddlCommisionCalOn");
                DropDownList ddlAUMFrequency = (DropDownList)editform.FindControl("ddlAUMFrequency");
                CheckBoxList chkListTtansactionType = (CheckBoxList)editform.FindControl("chkListTtansactionType");
                DropDownList ddlSIPFrequency = (DropDownList)editform.FindControl("ddlSIPFrequency");



                ddlCommissionType.DataSource = dsCommissionLookup.Tables[2];
                ddlCommissionType.DataValueField = dsCommissionLookup.Tables[2].Columns["WCT_CommissionTypeCode"].ToString();
                ddlCommissionType.DataTextField = dsCommissionLookup.Tables[2].Columns["WCT_CommissionType"].ToString();
                ddlCommissionType.DataBind();

                ddlBrokerageUnit.DataSource = dsCommissionLookup.Tables[3];
                ddlBrokerageUnit.DataValueField = dsCommissionLookup.Tables[3].Columns["WCU_UnitCode"].ToString();
                ddlBrokerageUnit.DataTextField = dsCommissionLookup.Tables[3].Columns["WCU_Unit"].ToString();
                ddlBrokerageUnit.DataBind();
                ddlBrokerageUnit.SelectedValue = "PER";

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

                    ddlCommissionType.SelectedValue = strCommissionType;
                    ddlInvestorType.SelectedValue = strCustomerCategory;
                    ddlTenureFrequency.SelectedValue = strTenureUnit;
                    ddlInvestAgeTenure.SelectedValue = "Months";
                    ddlBrokerageUnit.SelectedValue = strBrokargeUnit;
                    ddlCommisionCalOn.SelectedValue = strCalculatedOn;
                    ddlAUMFrequency.SelectedValue = strAUMFrequency;

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
            bool isPageValid = false;
            /*******************COLLECT DATA********************/
            commissionStructureRuleVo = CollectDataForCommissionStructureRule(e);

            /*******************UI VALIDATION********************/
            isPageValid = ValidatePage(commissionStructureRuleVo);
            if (isPageValid)
            {
                commissionStructureRuleVo.CommissionStructureRuleId = Convert.ToInt32(RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_CommissionStructureRuleId"].ToString());
                commisionReceivableBo.UpdateCommissionStructureRule(commissionStructureRuleVo, userVo.UserId);
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


        }

        protected void RadGridStructureRule_InsertCommand(object source, GridCommandEventArgs e)
        {
            bool isPageValid = false;
            try
            {
                /*******************COLLECT DATA********************/
                commissionStructureRuleVo = CollectDataForCommissionStructureRule(e);

                /*******************UI VALIDATION********************/
                isPageValid = ValidatePage(commissionStructureRuleVo);

                /*******************DUPLICATE CHECK********************/
                //bool isValidRule = true;

                if (isPageValid)
                {
                    commisionReceivableBo.CreateCommissionStructureRule(commissionStructureRuleVo, userVo.UserId);
                    BindCommissionStructureRuleGrid(Convert.ToInt32(hidCommissionStructureName.Value));
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
                TextBox txtAUMFor = (TextBox)e.Item.FindControl("txtAUMFor");
                DropDownList ddlAUMFrequency = (DropDownList)e.Item.FindControl("ddlAUMFrequency");

                TextBox txtMinNumberOfApplication = (TextBox)e.Item.FindControl("txtMinNumberOfApplication");
                TextBox txtStruRuleComment = (TextBox)e.Item.FindControl("txtStruRuleComment");

                commissionStructureRuleVo.CommissionStructureId = Convert.ToInt32(hidCommissionStructureName.Value);
                commissionStructureRuleVo.CommissionType = ddlCommissionType.SelectedValue;
                commissionStructureRuleVo.CustomerType = ddlInvestorType.SelectedValue;

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

                if (ddlInvestAgeTenure.SelectedValue == "Months")
                {
                    if (!string.IsNullOrEmpty(txtMinInvestAge.Text.Trim()))
                        commissionStructureRuleVo.MinInvestmentAge = Convert.ToInt32(txtMinInvestAge.Text.Trim());
                    if (!string.IsNullOrEmpty(txtMaxInvestAge.Text.Trim()))
                        commissionStructureRuleVo.MaxInvestmentAge = Convert.ToInt32(txtMaxInvestAge.Text.Trim());
                }
                else if (ddlInvestAgeTenure.SelectedValue == "Years")
                {
                    if (!string.IsNullOrEmpty(txtMinInvestAge.Text.Trim()))
                        commissionStructureRuleVo.MinInvestmentAge = Convert.ToInt32(txtMinInvestAge.Text.Trim()) * 12;
                    if (!string.IsNullOrEmpty(txtMaxInvestAge.Text.Trim()))
                        commissionStructureRuleVo.MaxInvestmentAge = Convert.ToInt32(txtMaxInvestAge.Text.Trim()) * 12;
                }

                if (!string.IsNullOrEmpty(txtMinInvestAge.Text.Trim()) || !string.IsNullOrEmpty(txtMaxInvestAge.Text.Trim()))
                commissionStructureRuleVo.InvestmentAgeUnit = "Months";

                foreach (ListItem chkItems in chkListTtansactionType.Items)
                {
                    if (chkItems.Selected == true)
                    {
                        commissionStructureRuleVo.TransactionType = commissionStructureRuleVo.TransactionType + chkItems.Value + ",";
                        if (chkItems.Value == "SIP")
                            commissionStructureRuleVo.SIPFrequency = ddlSIPFrequency.SelectedValue;
                    }
                }

                if (!string.IsNullOrEmpty(txtBrokerageValue.Text.Trim()))
                {
                    commissionStructureRuleVo.BrokerageValue = Convert.ToDecimal(txtBrokerageValue.Text.Trim());
                    commissionStructureRuleVo.BrokerageUnitCode = ddlBrokerageUnit.SelectedValue;
                }

                commissionStructureRuleVo.CalculatedOnCode = ddlCommisionCalOn.SelectedValue;
                if (ddlCommisionCalOn.SelectedValue == "AGAM" || ddlCommisionCalOn.SelectedValue == "AUM" || ddlCommisionCalOn.SelectedValue == "CLAM")
                {
                    if (!string.IsNullOrEmpty(txtAUMFor.Text.Trim()))
                        commissionStructureRuleVo.AUMMonth = Convert.ToDecimal(txtAUMFor.Text.Trim());
                    commissionStructureRuleVo.AUMFrequency = ddlAUMFrequency.SelectedValue;
                }

                if (!string.IsNullOrEmpty(txtMinNumberOfApplication.Text.Trim()))
                    commissionStructureRuleVo.MinNumberofApplications = Convert.ToInt16(txtMinNumberOfApplication.Text.Trim());

                if (!string.IsNullOrEmpty(txtStruRuleComment.Text.Trim()))
                    commissionStructureRuleVo.StructureRuleComment = txtStruRuleComment.Text.Trim();
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

                    if ((!string.IsNullOrEmpty(dr["ACSR_MinInvestmentAgeInMonth"].ToString()) && !string.IsNullOrEmpty(dr["ACSR_MinInvestmentAgeInMonth"].ToString()))
                        && (commissionStructureRuleVo.MinInvestmentAge != 0 && commissionStructureRuleVo.MaxInvestmentAge != 0))
                    {
                        if ((commissionStructureRuleVo.MinInvestmentAge > Convert.ToDecimal(dr["ACSR_MinInvestmentAgeInMonth"].ToString())) && (commissionStructureRuleVo.MinInvestmentAge < Convert.ToDecimal(dr["ACSR_MaxInvestmentAgeInMonth"].ToString()))
                            || (commissionStructureRuleVo.MaxInvestmentAge > Convert.ToDecimal(dr["ACSR_MinInvestmentAgeInMonth"].ToString())) && (commissionStructureRuleVo.MaxInvestmentAge < Convert.ToDecimal(dr["ACSR_MaxInvestmentAgeInMonth"].ToString())))
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
                ddlCategory.SelectedValue = commissionStructureMasterVo.AssetCategory;                
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
                ddlCommissionApplicableLevel.SelectedValue = commissionStructureMasterVo.ApplicableLevelCode;
                txtValidityFrom.Text = commissionStructureMasterVo.ValidityStartDate.ToShortDateString();
                txtValidityTo.Text = commissionStructureMasterVo.ValidityEndDate.ToShortDateString();
                txtStructureName.Text = commissionStructureMasterVo.CommissionStructureName;
                chkHasClawBackOption.Checked = commissionStructureMasterVo.IsClawBackApplicable;
                chkMoneytaryReward.Checked = commissionStructureMasterVo.IsNonMonetaryReward;
                chkListApplyTax.Items[0].Selected = commissionStructureMasterVo.IsServiceTaxReduced;
                chkListApplyTax.Items[1].Selected = commissionStructureMasterVo.IsTDSReduced;
                chkListApplyTax.Items[2].Selected = commissionStructureMasterVo.IsOtherTaxReduced;
                ddlAppCityGroup.SelectedValue = commissionStructureMasterVo.AdviserCityGroupCode;
                ddlReceivableFrequency.SelectedValue = commissionStructureMasterVo.ReceivableFrequency;
                txtNote.Text = commissionStructureMasterVo.StructureNote;
                hidCommissionStructureName.Value = structureId.ToString();
                CommissionStructureControlsEnable(false);
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
                ddlCategory.Enabled = true;
                rlbAssetSubCategory.Enabled = true;
                ddlIssuer.Enabled = true;
                ddlCommissionApplicableLevel.Enabled = true;
                txtValidityFrom.Enabled = true;
                txtValidityTo.Enabled = true;
                txtStructureName.Enabled = true;
                chkHasClawBackOption.Enabled = true;
                chkMoneytaryReward.Enabled = true;
                chkListApplyTax.Enabled = true;
                ddlAppCityGroup.Enabled = true;
                ddlReceivableFrequency.Enabled = true;
                txtNote.Enabled = true;

                lnkEditStructure.Text = "View";
                lnkEditStructure.ToolTip = "View commission structure section";
                btnStructureSubmit.Visible = false;
                btnStructureUpdate.Visible = true;
                btnMapToscheme.Visible = true;
            }
            else
            {
                ddlCategory.Enabled = false;
                rlbAssetSubCategory.Enabled = false;
                ddlIssuer.Enabled = false;
                ddlCommissionApplicableLevel.Enabled = false;
                txtValidityFrom.Enabled = false;
                txtValidityTo.Enabled = false;
                txtStructureName.Enabled = false;
                chkHasClawBackOption.Enabled = false;
                chkMoneytaryReward.Enabled = false;
                chkListApplyTax.Enabled = false;
                ddlAppCityGroup.Enabled = false;
                ddlReceivableFrequency.Enabled = false;
                txtNote.Enabled = false;

                lnkEditStructure.Visible = true;
                lnkEditStructure.Text = "Edit";
                lnkEditStructure.ToolTip = "Edit commission structure section";
                lnkAddNewStructure.Visible = true;
                btnStructureSubmit.Visible = false;
                btnStructureUpdate.Visible = false;
                btnMapToscheme.Visible = true;
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
            ddlCommissionApplicableLevel.SelectedIndex = 0;
            txtValidityFrom.Text = string.Empty;
            txtValidityTo.Text = string.Empty;
            txtStructureName.Text = string.Empty;
            chkHasClawBackOption.Checked = false;
            chkMoneytaryReward.Checked = false;
            foreach (ListItem item in chkListApplyTax.Items)
            {
                item.Selected = false;
            }
            ddlAppCityGroup.SelectedIndex = 0;
            ddlReceivableFrequency.SelectedIndex = 0;
            txtNote.Text = string.Empty;
            CommissionStructureControlsEnable(true);
            btnStructureSubmit.Visible = true;
            btnStructureUpdate.Visible = false;
            lnkEditStructure.Visible = false;
            lnkAddNewStructure.Visible = false;
            BindSubcategoryListBox(ddlCategory.SelectedValue);

            if (Cache[userVo.UserId.ToString() + "CommissionStructureRule"] != null)
                Cache.Remove(userVo.UserId.ToString() + "CommissionStructureRule");


            BindCommissionStructureRuleBlankRow();
            tblCommissionStructureRule.Visible = false;
            tblCommissionStructureRule1.Visible = false;

            btnMapToscheme.Visible = false;

            SetStructureMasterControlDefaultValues("MF");
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
            dtCommissionStructureRule.Columns.Add("ACSR_MinInvestmentAgeInMonth");
            dtCommissionStructureRule.Columns.Add("ACSR_MaxInvestmentAgeInMonth");
            dtCommissionStructureRule.Columns.Add("ACSR_TransactionType");
            dtCommissionStructureRule.Columns.Add("ACSR_MinNumberOfApplications");
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




    }

}
