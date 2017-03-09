using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoCommon;
using WealthERP.Base;
using System.Data;
using BoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoCustomerPortfolio;

namespace WealthERP.OfflineOrderBackOffice
{

    public partial class InsuranceIssueSetup : System.Web.UI.UserControl
    {
        AssetBo assetBo = new AssetBo();
        AdvisorVo advisorVo;
        CustomerVo customerVO = new CustomerVo();
        UserVo userVo;
        InsuranceIssueVO InsuranceIssuevo = new InsuranceIssueVO();
        InsuranceBo InsuranceBo = new InsuranceBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            if (!IsPostBack)
            {
                if (Request.QueryString["action"] != null)
                {
                    if (Request.QueryString["action"].Trim() == "Edit")
                    {
                        ViewIssueList(int.Parse(Request.QueryString["IssueID"]));
                        btnUpdate.Visible = true;
                        btnGo.Visible = false;
                        ControlEnable(true);
                    }
                    else
                    {
                        ViewIssueList(int.Parse(Request.QueryString["IssueID"]));
                        btnUpdate.Visible = false;
                        btnGo.Visible = false;
                        ControlEnable(false);
                    }

                }
            }

        }
        protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindInsuranceIssuer(ddlType.SelectedValue);
            LoadCategory(ddlType.SelectedValue);
            if (ddlType.SelectedValue == "IN")
            {
                lblReq.Visible = false;
                ddlSubCategory.Visible = false;
                RequiredFieldValidator2.Visible = false;
                lblSubCategory.Visible = false;
            }
            else
            {
                lblReq.Visible = true;
                ddlSubCategory.Visible = true;
                RequiredFieldValidator2.Visible = true;
                lblSubCategory.Visible = true;
            }
        }

        public void LoadCategory(string productType)
        {
            try
            {
                DataSet ds = assetBo.GetAssetInstrumentCategory(productType);
                ddlCategory.DataSource = ds.Tables[0];
                ddlCategory.DataTextField = "PAIC_AssetInstrumentCategoryName";
                ddlCategory.DataValueField = "PAIC_AssetInstrumentCategoryCode";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            loadSubCategory(ddlType.SelectedValue, ddlCategory.SelectedValue);
        }
        private void loadSubCategory(string productType, string InsturmentCategory)
        {
            try
            {

                DataSet ds = assetBo.GetAssetInstrumentSubCategory(productType, InsturmentCategory);
                ddlSubCategory.DataSource = ds;
                ddlSubCategory.DataValueField = ds.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlSubCategory.DataTextField = ds.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlSubCategory.DataBind();
                ddlSubCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        private void BindInsuranceIssuer(string insuranceType)
        {
            try
            {
                DataSet ds = InsuranceBo.GetGILIIssuerList(insuranceType);
                if (ds != null)
                {
                    ddlIssuer.DataSource = ds;
                    ddlIssuer.DataValueField = ds.Tables[0].Columns["XGII_GIIssuerCode"].ToString();
                    ddlIssuer.DataTextField = ds.Tables[0].Columns["XGII_GeneralinsuranceCompany"].ToString();
                    ddlIssuer.DataBind();
                }
                ddlIssuer.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        protected void Submit_OnClick(object sender, EventArgs e)
        {
            bool bresult = false;
            try
            {
                SaveIssueDetails();
                bresult = InsuranceBo.CreateInsurenceIssue(InsuranceIssuevo, userVo.UserId);
                btnGo.Visible = false;
                ControlEnable(false);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Issue Created');", true);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        protected void SaveIssueDetails()
        {
            InsuranceIssuevo.insuranceType = ddlType.SelectedValue;
            InsuranceIssuevo.issureCode = ddlIssuer.SelectedValue;
            InsuranceIssuevo.category = ddlCategory.SelectedValue;
            InsuranceIssuevo.subCategory = ddlSubCategory.SelectedValue;
            InsuranceIssuevo.policyName = (!string.IsNullOrEmpty(txtPolicyName.Text)) ? txtPolicyName.Text : "";
            InsuranceIssuevo.remarks = (!string.IsNullOrEmpty(txtRemarks.Text)) ? txtRemarks.Text : "";
            InsuranceIssuevo.launchDate = Convert.ToDateTime(txtOrderFrom.SelectedDate);
            InsuranceIssuevo.closeDate = Convert.ToDateTime(txtOrderTo.SelectedDate);
            InsuranceIssuevo.active = (chkActive.Checked) ? 1 : 0;
        }
        protected void ViewIssueList(int issueId)
        {
            InsuranceIssueVO InsuranceIssueVO = new InsuranceIssueVO();
            try
            {
                InsuranceIssueVO = InsuranceBo.ViewEditInsuraceIssue(issueId);
                BindInsuranceIssuer(InsuranceIssueVO.insuranceType);
                LoadCategory(InsuranceIssueVO.insuranceType);
                loadSubCategory(InsuranceIssueVO.insuranceType, InsuranceIssueVO.category);
                ddlType.SelectedValue = InsuranceIssueVO.insuranceType;
                ddlIssuer.SelectedValue = InsuranceIssueVO.issureCode;
                txtPolicyName.Text = InsuranceIssueVO.policyName;
                ddlCategory.SelectedValue = InsuranceIssueVO.category;
                ddlSubCategory.SelectedValue = InsuranceIssueVO.subCategory;
                txtOrderFrom.SelectedDate = InsuranceIssueVO.launchDate;
                txtOrderTo.SelectedDate = InsuranceIssueVO.closeDate;
                txtRemarks.Text = InsuranceIssueVO.remarks;
                if (InsuranceIssueVO.insuranceType == "LI")
                {
                    lblReq.Visible = false;
                    ddlSubCategory.Visible = false;
                    RequiredFieldValidator2.Visible = false;
                    lblSubCategory.Visible = false;
                }
                else
                {
                    lblReq.Visible = true;
                    ddlSubCategory.Visible = true;
                    RequiredFieldValidator2.Visible = true;
                    lblSubCategory.Visible = true;
                }
                if (InsuranceIssueVO.active == 1)
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public bool ControlEnable(bool type)
        {

            ddlType.Enabled = type;
            ddlIssuer.Enabled = type;
            txtPolicyName.Enabled = type;
            ddlCategory.Enabled = type;
            ddlSubCategory.Enabled = type;
            txtOrderFrom.Enabled = type;
            txtOrderTo.Enabled = type;
            txtRemarks.Enabled = type;
            chkActive.Enabled = type;
            return type;
        }
        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
             bool bresult = false;
            try
            {
                SaveIssueDetails();
                bresult = InsuranceBo.UpdateInsuraceIssue(InsuranceIssuevo, int.Parse(Request.QueryString["IssueID"]), userVo.UserId);
                btnUpdate.Visible = false;
                ControlEnable(false);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Issue Updated Successfully');", true);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
    }
}