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
            if (!IsPostBack)
            {
                BindAllDropdown();
                BindStructureRuleGrid();
            }

        }

        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtSubcategory = new DataTable();
            dtSubcategory = commonLookupBo.GetMFInstrumentSubCategory(ddlCategory.SelectedValue.ToString());
            rlbAssetSubCategory.DataSource = dtSubcategory;
            rlbAssetSubCategory.DataValueField = dtSubcategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
            rlbAssetSubCategory.DataTextField = dtSubcategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
            rlbAssetSubCategory.DataBind();

        }

        protected void BindAllDropdown()
        {
            DataSet dsLookupData;
            dsLookupData = commisionReceivableBo.GetLookupDataForReceivableSetUP(advisorVo.advisorId);

            ddlProductType.DataSource = dsLookupData.Tables[0];
            ddlProductType.DataValueField = dsLookupData.Tables[0].Columns["PAG_AssetGroupCode"].ToString();
            ddlProductType.DataTextField = dsLookupData.Tables[0].Columns["PAG_AssetGroupName"].ToString();
            ddlProductType.DataBind();
            ddlProductType.SelectedValue = "MF";
            ddlProductType.Enabled = false;

            ddlCommissionApplicableLevel.DataSource = dsLookupData.Tables[1];
            ddlCommissionApplicableLevel.DataValueField = dsLookupData.Tables[1].Columns["WCAL_ApplicableLEvelCode"].ToString();
            ddlCommissionApplicableLevel.DataTextField = dsLookupData.Tables[1].Columns["WCAL_ApplicableLEvel"].ToString();
            ddlCommissionApplicableLevel.DataBind();

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

            ddlIssuer.DataSource = dsLookupData.Tables[6];
            ddlIssuer.DataValueField = dsLookupData.Tables[6].Columns["PA_AMCCode"].ToString();
            ddlIssuer.DataTextField = dsLookupData.Tables[6].Columns["PA_AMCName"].ToString();
            ddlIssuer.DataBind();

            ddlAppCityGroup.DataSource = dsLookupData.Tables[7];
            ddlAppCityGroup.DataValueField = dsLookupData.Tables[7].Columns["ACG_CityGroupID"].ToString();
            ddlAppCityGroup.DataTextField = dsLookupData.Tables[7].Columns["ACG_CityGroupName"].ToString();
            ddlAppCityGroup.DataBind();


            ddlCategory.DataSource = dsLookupData.Tables[8];
            ddlCategory.DataValueField = dsLookupData.Tables[8].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlCategory.DataTextField = dsLookupData.Tables[8].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlCategory.DataBind();


            //ddlInvestorType.DataSource = dsLookupData.Tables[9];
            //ddlInvestorType.DataValueField = dsLookupData.Tables[8].Columns["XCC_CustomerCategoryCode"].ToString();
            //ddlInvestorType.DataTextField = dsLookupData.Tables[8].Columns["XCC_CustomerCategory"].ToString();
            //ddlInvestorType.DataBind();
            Session["CommissionLookUpData"] = dsLookupData;
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
                commissionStructureMasterVo.ApplicableLevelCode = ddlCommissionApplicableLevel.SelectedValue;
                commissionStructureMasterVo.ValidityStartDate = Convert.ToDateTime(txtValidityFrom.Text);
                commissionStructureMasterVo.ValidityEndDate = Convert.ToDateTime(txtValidityTo.Text);
                commissionStructureMasterVo.CommissionStructureName = txtStructureName.Text.Trim();

                //receivableStructureMasterVo.CommissionTypeCode = ddlCommissionType.SelectedValue;
                commissionStructureMasterVo.IsClawBackApplicable = bool.Parse(chkHasClawBackOption.Checked.ToString());
                commissionStructureMasterVo.IsNonMonetaryReward = bool.Parse(chkMoneytaryReward.Checked.ToString());
                commissionStructureMasterVo.IsServiceTaxReduced = bool.Parse(chkListApplyTax.Items[1].Selected.ToString());
                commissionStructureMasterVo.IsTDSReduced = bool.Parse(chkListApplyTax.Items[2].Selected.ToString());
                commissionStructureMasterVo.IsOtherTaxReduced = bool.Parse(chkListApplyTax.Items[2].Selected.ToString());

                //receivableStructureMasterVo.IsStructureFromIssuer = bool.Parse(chk.Checked.ToString());
                //receivableStructureMasterVo.RecurringiSIPFrequency=ddl

                commissionStructureMasterVo.ReceivableFrequency = ddlReceivableFrequency.SelectedValue;
                commissionStructureMasterVo.StructureNote = txtNote.Text.Trim();


                foreach (RadListBoxItem item in rlbAssetSubCategory.Items)
                {
                    if (item.Checked == true)
                    {
                        strSubCategoryCode.Append(item.Value);
                        strSubCategoryCode.Append("~");
                    }
                }
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
            commisionReceivableBo.CreateCommissionStructureMastter(commissionStructureMasterVo, userVo.UserId, out commissionStructureId);
            hidCommissionStructureName.Value = commissionStructureId.ToString();
        }

        protected void BindStructureRuleGrid()
        {
            DataSet dsStructureRules = commisionReceivableBo.GetAdviserCommissionStructureRules(advisorVo.advisorId);
            RadGridStructureRule.DataSource = dsStructureRules.Tables[0];
            RadGridStructureRule.DataBind();
            Cache.Insert(userVo.UserId.ToString() + "CommissionStructureRule", dsStructureRules);

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



                ddlCommissionType.DataSource = dsCommissionLookup.Tables[2];
                ddlCommissionType.DataValueField = dsCommissionLookup.Tables[2].Columns["WCT_CommissionTypeCode"].ToString();
                ddlCommissionType.DataTextField = dsCommissionLookup.Tables[2].Columns["WCT_CommissionType"].ToString();
                ddlCommissionType.DataBind();

                ddlBrokerageUnit.DataSource = dsCommissionLookup.Tables[3];
                ddlBrokerageUnit.DataValueField = dsCommissionLookup.Tables[3].Columns["WCU_UnitCode"].ToString();
                ddlBrokerageUnit.DataTextField = dsCommissionLookup.Tables[3].Columns["WCU_Unit"].ToString();
                ddlBrokerageUnit.DataBind();

                ddlCommisionCalOn.DataSource = dsCommissionLookup.Tables[4];
                ddlCommisionCalOn.DataValueField = dsCommissionLookup.Tables[4].Columns["WCCO_Calculatedoncode"].ToString();
                ddlCommisionCalOn.DataTextField = dsCommissionLookup.Tables[4].Columns["WCCO_CalculatedOn"].ToString();
                ddlCommisionCalOn.DataBind();

                ddlInvestorType.DataSource = dsCommissionLookup.Tables[9];
                ddlInvestorType.DataValueField = dsCommissionLookup.Tables[9].Columns["XCC_CustomerCategoryCode"].ToString();
                ddlInvestorType.DataTextField = dsCommissionLookup.Tables[9].Columns["XCC_CustomerCategory"].ToString();
                ddlInvestorType.DataBind();


                if (e.Item.RowIndex != -1)
                {
                    string strCommissionType = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WCT_CommissionType"].ToString();
                    string strCustomerCategory = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XCT_CustomerTypeCode"].ToString();
                    string strTenureUnit = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_TenureUnit"].ToString();
                    //string strInvestmentAgeUnit=RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_InvestmentAgeUnit"].ToString();
                    string strInvestmentTransactionType = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACSR_TransactionType"].ToString();
                    string strBrokargeUnit = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WCU_Unit"].ToString();
                    string strCalculatedOn = RadGridStructureRule.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WCCO_CalculatedOn"].ToString();
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
                }

            }


        }
        protected void RadGridStructureRule_DeleteCommand(object source, GridCommandEventArgs e)
        {

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
        protected void RadGridStructureRule_ItemUpdated(object source, Telerik.Web.UI.GridUpdatedEventArgs e)
        {

        }
        protected void RadGridStructureRule_ItemCommand(object source, GridCommandEventArgs e)
        {


        }

        protected void RadGridStructureRule_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                DropDownList ddlCommissionType = (DropDownList)e.Item.FindControl("ddlCommissionType");
                DropDownList ddlInvestorType = (DropDownList)e.Item.FindControl("ddlInvestorType");

                TextBox txtMinInvestmentAmount = (TextBox)e.Item.FindControl("txtMinInvestmentAmount");
                TextBox txtMaxInvestmentAmount = (TextBox)e.Item.FindControl("txtMaxInvestmentAmount");

                TextBox txtMinTenure = (TextBox)e.Item.FindControl("txtMinTenure");
                TextBox txtMaxTenure = (TextBox)e.Item.FindControl("txtMinTenure");
                DropDownList ddlTenureFrequency = (DropDownList)e.Item.FindControl("ddlTenureFrequency");

                TextBox txtMinInvestAge = (TextBox)e.Item.FindControl("txtMinInvestAge");
                TextBox txtMaxInvestAge = (TextBox)e.Item.FindControl("txtMaxInvestAge");
                DropDownList ddlInvestAgeTenure = (DropDownList)e.Item.FindControl("ddlInvestAgeTenure");

                CheckBoxList chkListTtansactionType = (CheckBoxList)e.Item.FindControl("chkListTtansactionType");
                TextBox txtMinNumberOfApplication = (TextBox)e.Item.FindControl("txtMinNumberOfApplication");

                TextBox txtBrokerageValue = (TextBox)e.Item.FindControl("txtBrokerageValue");
                DropDownList ddlBrokerageUnit = (DropDownList)e.Item.FindControl("ddlBrokerageUnit");

                DropDownList ddlCommisionCalOn = (DropDownList)e.Item.FindControl("ddlCommisionCalOn");
                TextBox txtAUMFor = (TextBox)e.Item.FindControl("txtAUMFor");
                DropDownList ddlAUMFrequency = (DropDownList)e.Item.FindControl("ddlAUMFrequency");

                commissionStructureRuleVo.CommissionStructureId = 5;
                commissionStructureRuleVo.CommissionType = ddlCommissionType.SelectedValue;
                commissionStructureRuleVo.CustomerType = ddlInvestorType.SelectedValue;

                if (!string.IsNullOrEmpty(txtMinInvestmentAmount.Text.Trim()))
                    commissionStructureRuleVo.MinInvestmentAmount = Convert.ToDecimal(txtMinInvestmentAmount.Text.Trim());
                if (!string.IsNullOrEmpty(txtMinInvestmentAmount.Text.Trim()))
                    commissionStructureRuleVo.MaxInvestmentAmount = Convert.ToDecimal(txtMinInvestmentAmount.Text.Trim());

                if (!string.IsNullOrEmpty(txtMinTenure.Text.Trim()))
                    commissionStructureRuleVo.TenureMin = Convert.ToInt32(txtMinTenure.Text.Trim());
                if (!string.IsNullOrEmpty(txtMaxTenure.Text.Trim()))
                    commissionStructureRuleVo.TenureMax = Convert.ToInt32(txtMaxTenure.Text.Trim());
                if (!string.IsNullOrEmpty(txtMinTenure.Text.Trim()) && !string.IsNullOrEmpty(txtMaxTenure.Text.Trim()))
                    commissionStructureRuleVo.TenureUnit = ddlTenureFrequency.SelectedValue.ToString();

                if (ddlInvestAgeTenure.SelectedValue == "Month")
                {
                    if (!string.IsNullOrEmpty(txtMinInvestAge.Text.Trim()))
                        commissionStructureRuleVo.MinInvestmentAge = Convert.ToInt32(txtMinInvestAge.Text.Trim());
                    if (!string.IsNullOrEmpty(txtMaxInvestAge.Text.Trim()))
                        commissionStructureRuleVo.MaxInvestmentAge = Convert.ToInt32(txtMaxInvestAge.Text.Trim());
                }
                else if (ddlInvestAgeTenure.SelectedValue == "Year")
                {
                    if (!string.IsNullOrEmpty(txtMinInvestAge.Text.Trim()))
                        commissionStructureRuleVo.MinInvestmentAge = Convert.ToInt32(txtMinInvestAge.Text.Trim()) * 12;
                    if (!string.IsNullOrEmpty(txtMaxInvestAge.Text.Trim()))
                        commissionStructureRuleVo.MaxInvestmentAge = Convert.ToInt32(txtMaxInvestAge.Text.Trim()) * 12;
                }

                if (!string.IsNullOrEmpty(txtMinInvestAge.Text.Trim()) && !string.IsNullOrEmpty(txtMaxInvestAge.Text.Trim()))
                    commissionStructureRuleVo.InvestmentAgeUnit = ddlInvestAgeTenure.SelectedValue.ToString();

                foreach (ListItem chkItems in chkListTtansactionType.Items)
                {
                    if (chkItems.Selected == true)
                    {
                        commissionStructureRuleVo.TransactionType = commissionStructureRuleVo.TransactionType + chkItems.Value + ",";

                    }
                }
                if (!string.IsNullOrEmpty(txtMinNumberOfApplication.Text.Trim()))
                    commissionStructureRuleVo.MinNumberofApplications = Convert.ToInt16(txtMinNumberOfApplication.Text.Trim());

                if (!string.IsNullOrEmpty(txtBrokerageValue.Text.Trim()))
                {
                    commissionStructureRuleVo.BrokerageValue = Convert.ToDecimal(txtBrokerageValue.Text.Trim());
                    commissionStructureRuleVo.BrokerageUnitCode = ddlBrokerageUnit.SelectedValue;
                }

                commissionStructureRuleVo.CalculatedOnCode = ddlCommisionCalOn.SelectedValue;
                if (ddlCommisionCalOn.SelectedValue == "AGAM" || ddlCommisionCalOn.SelectedValue == "AUM" || ddlCommisionCalOn.SelectedValue == "CLAM")
                {
                    commissionStructureRuleVo.AUMMonth = Convert.ToDecimal(txtAUMFor.Text.Trim());
                    commissionStructureRuleVo.AUMFrequency = ddlAUMFrequency.SelectedValue;
                }
                /*******************DUPLICATE CHECK********************/
                bool isValidRule = ValidateCommissionRule(commissionStructureRuleVo);
                if (isValidRule)
                {
                    commisionReceivableBo.CreateCommissionStructureRule(commissionStructureRuleVo, userVo.UserId);
                    BindStructureRuleGrid();
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


                /******Check for Investment Amount******/
                if ((!string.IsNullOrEmpty(dr["ACSR_MinInvestmentAmount"].ToString()) && !string.IsNullOrEmpty(dr["ACSR_MinInvestmentAmount"].ToString()))
                    && (commissionStructureRuleVo.MinInvestmentAmount != 0 && commissionStructureRuleVo.MaxInvestmentAmount!=0))
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

                /******Check for Tenure******/
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
                        foreach (string str in arrayTTypeE)
                        {
                            if (!string.IsNullOrEmpty(str.Trim()))
                            {
                                str.Contains(commissionStructureRuleVo.TransactionType);
                            }
                        }
                    }else
                    {
                        duplicateCheck.Add(false);
                    }

                    }
                }


                if (duplicateCheck.Count != duplicateCheck.Distinct().Count())
                {
                    // Duplicates exist

                }

             return isValidRule;
            }


           
        }

    }
