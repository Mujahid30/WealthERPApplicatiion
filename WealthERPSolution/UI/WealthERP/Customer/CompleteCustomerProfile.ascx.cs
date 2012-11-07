using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using VoCustomerProfiling;
using BoUser;
using VoUser;
using BoCommon;
using BoUploads;
using BoCustomerProfiling;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoAdvisorProfiling;
namespace WealthERP.Customer
{
    public partial class CompleteCustomerProfile : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        UserVo userVo = new UserVo();
        UserBo userBo = new UserBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        UserVo tempUserVo = new UserVo();
        AdvisorBo advisorBo = new AdvisorBo();
        string path;
        int customerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session["rmVo"];
            userVo = (UserVo)Session["UserVo"];

            if (!Page.IsPostBack)
            {
                ClearCustomerBasicProfileDeatils();
            }
        
        }

        protected void rbtnNonIndividual_CheckedChanged(object sender, EventArgs e)
        {
            ShowHideControlsBasedOnCustomerType(false);
            BindSubTypeDropDown("NIND");

        }

        protected void rbtnIndividual_CheckedChanged(object sender, EventArgs e)
        {
            ShowHideControlsBasedOnCustomerType(true);
            BindSubTypeDropDown("IND");

        }

        private void ShowHideControlsBasedOnCustomerType(bool isCustTypeIndivisual)
        {
            if (isCustTypeIndivisual)
            {
                trCustomerIndivisualOne.Visible = true;
                trCustomerIndivisualTwo.Visible = true;
                trCustomerNonIndivisualOne.Visible = false;

            }
            else
            {
                trCustomerIndivisualOne.Visible = false;
                trCustomerIndivisualTwo.Visible = false;
                trCustomerNonIndivisualOne.Visible = true;
 
            }

        }

        private void BindSubTypeDropDown(string type)
        {
            try
            {
                DataTable dtCustomerSubType = XMLBo.GetCustomerSubType(path, type);
                ddlCustomerSubType.DataSource = dtCustomerSubType;
                ddlCustomerSubType.DataTextField = "CustomerTypeName";
                ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
                ddlCustomerSubType.DataBind();
                ddlCustomerSubType.Items.Insert(0, new ListItem("Select", "Select"));
                               
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CompleteCustomerProfile.ascx:rbtnIndividual_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void BindBranchListDropDown()
        {
            UploadCommonBo uploadCommonBo = new UploadCommonBo();
            DataSet ds = uploadCommonBo.GetAdviserBranchList(advisorVo.advisorId, "adviser");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlAdviserBranchList.DataSource = ds.Tables[0];
                ddlAdviserBranchList.DataTextField = "AB_BranchName";
                ddlAdviserBranchList.DataValueField = "AB_BranchId";
                ddlAdviserBranchList.DataBind();
                ddlAdviserBranchList.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {
                ddlAdviserBranchList.Items.Insert(0, new ListItem("No Branches Available to Associate", "No Branches Available to Associate"));
                ddlAdviserBranchList_CompareValidator2.ValueToCompare = "No Branches Available to Associate";
                ddlAdviserBranchList_CompareValidator2.ErrorMessage = "Cannot Add Customer Without a Branch";
            }
 
        }

        private void BindRMListForBranch(int branchId, int branchHeadId)
        {
            DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlAdviseRMList.DataSource = ds.Tables[0];
                ddlAdviseRMList.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                ddlAdviseRMList.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
                ddlAdviseRMList.DataBind();
                ddlAdviseRMList.Items.Insert(0, new ListItem("Select", "Select"));
                CompareValidator2.ValueToCompare = "Select";
                CompareValidator2.ErrorMessage = "Please select a RM";
            }
            else
            {
                ddlAdviseRMList.Items.Clear();
                ddlAdviseRMList.Items.Remove("Select");
                ddlAdviseRMList.Items.Insert(0, new ListItem("No RM Available", "No RM Available"));
                CompareValidator2.ValueToCompare = "No RM Available";
                CompareValidator2.ErrorMessage = "Cannot Add Customer Without a RM";

            }
        }
        private void BindCustomerCategory()
        {
            DataSet dsCustomerCategory = advisorBo.GetAdviserCustomerCategory(advisorVo.advisorId);
            if (dsCustomerCategory.Tables[0].Rows.Count > 0)
            {
                ddlCustomerCategory.DataSource = dsCustomerCategory.Tables[0];
                ddlCustomerCategory.DataValueField = dsCustomerCategory.Tables[0].Columns["ACC_CustomerCategoryCode"].ToString();
                ddlCustomerCategory.DataTextField = dsCustomerCategory.Tables[0].Columns["ACC_customerCategoryName"].ToString();
                ddlCustomerCategory.DataBind();
                ddlCustomerCategory.Items.Insert(0, new ListItem("Select", "Select"));
                //CompareValidator2.ValueToCompare = "Select";
                //CompareValidator2.ErrorMessage = "Please select a RM";
            }
            else
            {
                ddlCustomerCategory.Items.Clear();
                ddlCustomerCategory.Items.Remove("Select");
                ddlCustomerCategory.Items.Insert(0, new ListItem("No Category Available", "No Category Available"));
                //CompareValidator2.ValueToCompare = "No RM Available";
                //CompareValidator2.ErrorMessage = "Cannot Add Customer Without a RM";

            }
          
        }


        protected void ddlAdviserBranchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAdviserBranchList.SelectedIndex == 0)
            {
                BindRMListForBranch(0, 0);
            }
            else
            {
                BindRMListForBranch(int.Parse(ddlAdviserBranchList.SelectedValue.ToString()), 0);

            }

        }

        protected void btnSubmitAddMore_Click(object sender, EventArgs e)
        {
            try
            {

                CustomerBasicProfileDataCollection();
                customerBo.CreateCustomerBasicProfileDetails(customerVo, userVo.UserId, "RGL", "MyPortfolio");
                ClearCustomerBasicProfileDeatils();
            
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CompleteCustomerProfile.ascx:btnSubmitAddMore_Click()");
                object[] objects = new object[3];
                objects[0] = userVo.UserId;
                objects[1] = customerVo;                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnSaveAddDeatils_Click(object sender, EventArgs e)
        {
            try
            {

                CustomerBasicProfileDataCollection();
                customerId=customerBo.CreateCustomerBasicProfileDetails(customerVo, userVo.UserId, "RGL", "MyPortfolio");
                EnableCustomerBasicProfileSection(false);
                Session["CurrentCustomerId"] = customerId;
                lnkCustomerBasicProfileEdit.Visible = true;
                lnkCustomerBasicProfileEdit.Text = "Edit";
                lnkCustomerBasicProfileEdit.Enabled = true;
                

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CompleteCustomerProfile.ascx:btnSubmitAddMore_Click()");
                object[] objects = new object[3];
                objects[0] = userVo.UserId;
                objects[1] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        protected void btnUpdateProfileBasic_Click(object sender, EventArgs e)
        {

        }

        protected void lnkCustomerBasicProfileEdit_Click(object sender, EventArgs e)
        {
            lnkCustomerBasicProfileEdit.Text = "View";
            EnableCustomerBasicProfileSection(true);
        }

        private void CustomerBasicProfileDataCollection()
        {
            customerVo.SubType = ddlCustomerSubType.SelectedValue;
            customerVo.BranchId = Convert.ToInt16(ddlAdviserBranchList.SelectedValue.ToString());
            customerVo.RmId = Convert.ToInt16(ddlAdviseRMList.SelectedValue.ToString());
            customerVo.Email = txtEmail.Text.Trim();
            customerVo.PANNum = txtPanNumber.Text.Trim();
            if (ddlCustomerCategory.SelectedIndex!=0)
            customerVo.CustomerClassificationID = Convert.ToInt16(ddlCustomerCategory.SelectedValue.ToString());
            if(!string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
               customerVo.CustCode = txtCustomerCode.Text.Trim();
            if (!string.IsNullOrEmpty(txtDateofProfiling.Text.Trim()))
                customerVo.ProfilingDate = Convert.ToDateTime(txtDateofProfiling.Text.Trim());
            customerVo.UserType = "Customer";
            if (chkdummypan.Checked == true)
                customerVo.DummyPAN = 1;
            
            if (rbtnIndividual.Checked == true)
            {
                customerVo.Type = "IND";                
                if (rbtnMale.Checked)
                    customerVo.Gender = "M";
                else
                    customerVo.Gender = "F";

                if (ddlSalutation.SelectedIndex != 0)
                    customerVo.Salutation = ddlSalutation.SelectedValue.ToString();

                customerVo.FirstName = txtFirstName.Text.Trim();
                if(!string.IsNullOrEmpty(txtMiddleName.Text.Trim()))
                    customerVo.MiddleName = txtMiddleName.Text.Trim();
                if (!string.IsNullOrEmpty(txtLastName.Text.Trim()))
                    customerVo.LastName = txtLastName.Text.Trim();

            }
            else
            {
                customerVo.Type = "NIND";
                if (!string.IsNullOrEmpty(txtCompanyName.Text.Trim()))
                customerVo.CompanyName = txtCompanyName.Text.Trim();
                if (!string.IsNullOrEmpty(txtCompanyWebsite.Text.Trim()))
                    customerVo.CompanyWebsite = txtCompanyWebsite.Text.Trim();

            }

             
        }

        private void ClearCustomerBasicProfileDeatils()
        {
           
            rbtnIndividual.Checked = true;
            BindSubTypeDropDown("IND");
            BindBranchListDropDown();
            BindCustomerCategory();
            rbtnMale.Checked = true;
            ddlSalutation.SelectedIndex = 0;
            txtFirstName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPanNumber.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlCustomerCategory.SelectedIndex = 0;
            txtCustomerCode.Text = string.Empty;
            txtDateofProfiling.Text = DateTime.Now.ToShortDateString();

            txtCompanyName.Text = string.Empty;
            txtCompanyWebsite.Text = string.Empty;
            lnkCustomerBasicProfileEdit.Visible = false;
            ShowHideControlsBasedOnCustomerType(true);

            
 
        }

        private void EnableCustomerBasicProfileSection(bool isEnable)
        {
            if (isEnable)
            {
                rbtnIndividual.Enabled = true;
                rbtnNonIndividual.Enabled = true;
                ddlCustomerSubType.Enabled = true;
                ddlAdviserBranchList.Enabled = true;
                ddlAdviseRMList.Enabled = true;
                txtEmail.Enabled = true;
                txtPanNumber.Enabled = true;
                ddlCustomerCategory.Enabled = true;
                txtCustomerCode.Enabled = true;
                txtDateofProfiling.Enabled = true;
                rbtnMale.Enabled = true;
                rbtnFemale.Enabled = true;
                ddlSalutation.Enabled = true;
                txtFirstName.Enabled = true;
                txtMiddleName.Enabled = true;
                txtLastName.Enabled = true;
                txtCompanyName.Enabled = true;
                txtCompanyWebsite.Enabled = true;

            }
            else
            {
                rbtnIndividual.Enabled = false;
                rbtnNonIndividual.Enabled = false;
                ddlCustomerSubType.Enabled = false;
                ddlAdviserBranchList.Enabled = false;
                ddlAdviseRMList.Enabled = false;
                txtEmail.Enabled = false;
                txtPanNumber.Enabled = false;
                ddlCustomerCategory.Enabled = false;
                txtCustomerCode.Enabled = false;
                txtDateofProfiling.Enabled = false;
                rbtnMale.Enabled = false;
                rbtnFemale.Enabled = false;
                ddlSalutation.Enabled = false;
                txtFirstName.Enabled = false;
                txtMiddleName.Enabled = false;
                txtLastName.Enabled = false;
                txtCompanyName.Enabled = false;
                txtCompanyWebsite.Enabled = false;
 
            }

        }
    }
}