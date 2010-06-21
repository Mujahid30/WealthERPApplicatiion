using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;
using VoUser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;

namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioGeneralInsuranceAccountAdd : System.Web.UI.UserControl
    {
        AssetBo assetBo = new AssetBo();
        DataSet dsCustomerAssociates;
        DataTable dtCustomerAssociatesRaw = new DataTable();
        DataTable dtCustomerAssociates = new DataTable();
        DataRow drCustomerAssociates;
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        GeneralInsuranceVo generalInsuranceVo = new GeneralInsuranceVo();
        InsuranceBo insuranceBo = new InsuranceBo();
        int CustomerGIPortfolioId;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            DataTable dt;

            if (!IsPostBack)
            {
                BindAssetGroupDropDown();
                LoadNominees();

                //Checking whether a General Insurance Portfolio exists or not
                dt = insuranceBo.ChkGenInsurancePortfolioExist(customerVo.CustomerId);
                if (dt.Rows.Count > 0)
                {
                    ViewState["CustomerGIPortfolioId"] = dt.Rows[0][0].ToString().Trim();
                }
                else
                    ViewState["CustomerGIPortfolioId"] = (insuranceBo.CreateCustomerGIPortfolio(customerVo.CustomerId, userVo.UserId)).ToString();
            }
        }

        private void BindAssetGroupDropDown()
        {
            try
            {
                DataSet ds = assetBo.GetAssetInstrumentCategory("GI");
                ddlAssetCategory.DataSource = ds;
                ddlAssetCategory.DataValueField = ds.Tables[0].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                ddlAssetCategory.DataTextField = ds.Tables[0].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                ddlAssetCategory.DataBind();
                ddlAssetCategory.SelectedValue = "GIRI";
                //ddlAssetCategory.Items.Insert(0, new ListItem("Select Asset Category", "Select Asset Category"));
                BindAssetSubGroupDropDown();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioGeneralInsuranceAccountAdd.ascx:BindAssetGroupDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlAssetCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (ddlAssetCategory.SelectedIndex != 0)
                //{
                DataSet ds = assetBo.GetAssetInstrumentSubCategory("GI", ddlAssetCategory.SelectedValue);
                ddlAssetSubCategory.DataSource = ds;
                ddlAssetSubCategory.DataValueField = ds.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlAssetSubCategory.DataTextField = ds.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlAssetSubCategory.DataBind();
                ddlAssetSubCategory.Items.Insert(0, new ListItem("Select Asset Sub-Category", "Select Asset Sub-Category"));
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioGeneralInsuranceAccountAdd.ascx:ddlAssetCategory_SelectedIndexChanged()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void LoadNominees()
        {
            try
            {
                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];

                dtCustomerAssociates.Columns.Add("MemberCustomerId");
                dtCustomerAssociates.Columns.Add("AssociationId");
                dtCustomerAssociates.Columns.Add("Name");
                dtCustomerAssociates.Columns.Add("Relationship");

                foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
                {
                    drCustomerAssociates = dtCustomerAssociates.NewRow();
                    drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                    drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                    drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                    drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                    dtCustomerAssociates.Rows.Add(drCustomerAssociates);
                }

                if (dtCustomerAssociates.Rows.Count > 0)
                {
                    gvNominees.DataSource = dtCustomerAssociates;
                    gvNominees.DataBind();
                    gvNominees.Visible = true;

                    trNominees.Visible = true;
                }
                else
                {
                    trNominees.Visible = false;
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
                FunctionInfo.Add("Method", "PortfolioGeneralInsuranceAccountAdd.ascx:LoadNominees()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int accountId=0;
            string qryString; 
            try
            {

                generalInsuranceVo.AssetInstrumentCategoryCode = ddlAssetCategory.SelectedValue;
                generalInsuranceVo.AssetInstrumentSubCategoryCode = ddlAssetSubCategory.SelectedValue;
                generalInsuranceVo.AssetGroupCode = "GI";
                generalInsuranceVo.PolicyNumber = txtPolicyNumber.Text;
                generalInsuranceVo.PortfolioId = int.Parse(ViewState["CustomerGIPortfolioId"].ToString());

                accountId = insuranceBo.CreateCustomerGIAccount(generalInsuranceVo, userVo.UserId);

                customerAccountAssociationVo.AccountId = accountId;
                customerAccountAssociationVo.CustomerId = customerVo.CustomerId;

                foreach (GridViewRow gvr in this.gvNominees.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {

                        customerAccountAssociationVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[1].ToString());
                        customerAccountAssociationVo.AssociationType = "Nominee";
                        insuranceBo.CreateGIAccountAssociation(customerAccountAssociationVo, userVo.UserId);
                    }
                }
                qryString = "?AccountId=" +accountId +"&FromPage=GIAccountAdd&AssCatCode=" + ddlAssetCategory.SelectedValue + "&AssCat=" + ddlAssetCategory.SelectedItem.Text + "&AssSubCatCode=" + ddlAssetSubCategory.SelectedValue +"&AssSubCat=" + ddlAssetSubCategory.SelectedItem.Text + "&PolicyNumber="+txtPolicyNumber.Text;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioGeneralInsuranceEntry','"+ qryString +"');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerMFAccountAdd.ascx:rbtnYes_CheckedChanged()");
                object[] objects = new object[5];
                objects[0] = customerAccountAssociationVo;
                objects[1] = generalInsuranceVo;
                objects[2] = userVo;
                objects[3] = customerVo;
                objects[4] = CustomerGIPortfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void BindAssetSubGroupDropDown()
        {
            try
            {
                DataSet ds = assetBo.GetAssetInstrumentSubCategory("GI", ddlAssetCategory.SelectedValue);
                ddlAssetSubCategory.DataSource = ds;
                ddlAssetSubCategory.DataValueField = ds.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlAssetSubCategory.DataTextField = ds.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlAssetSubCategory.DataBind();
                ddlAssetSubCategory.Items.Insert(0, new ListItem("Select Asset Sub-Category", "Select Asset Sub-Category"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioGeneralInsuranceAccountAdd.ascx:BindAssetSubGroupDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }
    }
}