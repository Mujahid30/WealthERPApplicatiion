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

namespace WealthERP.Receivable
{
    public partial class ReceivableSetup : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        RMVo rmVo;
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        ReceivableStructureMasterVo receivableStructureMasterVo = new ReceivableStructureMasterVo();
        ReceivableStructureRuleVo receivableStructureRuleVo = new ReceivableStructureRuleVo(); 


        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session["rmVo"];
            if (!IsPostBack)
            {
                BindAllDropdown();
            }

        }

        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtSubcategory = new DataTable();
            dtSubcategory = commonLookupBo.GetMFInstrumentSubCategory(ddlCategory.SelectedValue.ToString());
            ddlSubCategory.DataSource = dtSubcategory;
            ddlSubCategory.DataValueField = dtSubcategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
            ddlSubCategory.DataTextField = dtSubcategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
            ddlSubCategory.DataBind();

        }

        protected void BindAllDropdown()
        {
            DataSet dsLookupData;
            dsLookupData = commisionReceivableBo.GetLookupDataForReceivableSetUP(advisorVo.advisorId);

            ddlCategory.DataSource = dsLookupData.Tables[0];
            ddlCategory.DataValueField = dsLookupData.Tables[0].Columns["PAG_AssetGroupCode"].ToString();
            ddlCategory.DataTextField = dsLookupData.Tables[0].Columns["PAG_AssetGroupName"].ToString();
            ddlCategory.DataBind();


            ddlCommissionApplicableLevel.DataSource = dsLookupData.Tables[1];
            ddlCommissionApplicableLevel.DataValueField = dsLookupData.Tables[1].Columns["WCAL_ApplicableLEvelCode"].ToString();
            ddlCommissionApplicableLevel.DataTextField = dsLookupData.Tables[1].Columns["WCAL_ApplicableLEvel"].ToString();
            ddlCommissionApplicableLevel.DataBind();

            ddlCommissionType.DataSource = dsLookupData.Tables[2];
            ddlCommissionType.DataValueField = dsLookupData.Tables[2].Columns["WCT_CommissionTypeCode"].ToString();
            ddlCommissionType.DataTextField = dsLookupData.Tables[2].Columns["WCT_CommissionType"].ToString();
            ddlCommissionType.DataBind();

            ddlUnit.DataSource = dsLookupData.Tables[3];
            ddlUnit.DataValueField = dsLookupData.Tables[3].Columns["WCU_UnitCode"].ToString();
            ddlUnit.DataTextField = dsLookupData.Tables[3].Columns["WCU_Unit"].ToString();
            ddlUnit.DataBind();

            ddlCommisionCalOn.DataSource = dsLookupData.Tables[4];
            ddlCommisionCalOn.DataValueField = dsLookupData.Tables[4].Columns["WCCO_Calculatedoncode"].ToString();
            ddlCommisionCalOn.DataTextField = dsLookupData.Tables[4].Columns["WCCO_CalculatedOn"].ToString();
            ddlCommisionCalOn.DataBind();           

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
 
        }

        protected ReceivableStructureMasterVo CollectStructureMastetrData()
        {
            try
            {
                receivableStructureMasterVo.AdviserId = advisorVo.advisorId;
                receivableStructureMasterVo.ApplicableLEvelCode = ddlCommissionApplicableLevel.SelectedValue;
                receivableStructureMasterVo.AssetGroupCode = ddlCategory.SelectedValue;
                receivableStructureMasterVo.CommissionStructureName = txtStructureName.Text.Trim();
                receivableStructureMasterVo.CommissionTypeCode = ddlCommissionType.SelectedValue;
                receivableStructureMasterVo.IsNonMonetaryReward = bool.Parse(chkMoneytaryReward.Checked.ToString());

                receivableStructureMasterVo.IsServicetaxReduced = bool.Parse(chkListApplyTax.Items[1].Selected.ToString());
                receivableStructureMasterVo.IsTDSReduced = bool.Parse(chkListApplyTax.Items[2].Selected.ToString());
                receivableStructureMasterVo.IsOtherTAxReduced = bool.Parse(chkListApplyTax.Items[2].Selected.ToString());
                //receivableStructureMasterVo.IsStructureFromIssuer = bool.Parse(chk.Checked.ToString());
                  //receivableStructureMasterVo.RecurringiSIPFrequency=ddl

                receivableStructureMasterVo.ReceivableFrequency = ddlReceivableFrequency.SelectedValue;
               

            
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
            return receivableStructureMasterVo;

        }

    }
}