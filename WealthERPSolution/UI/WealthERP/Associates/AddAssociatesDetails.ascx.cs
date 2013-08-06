using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using BoAdvisorProfiling;
using VoUser;
using BoCustomerGoalProfiling;
using Telerik.Web.UI;
using BoCommon;
using System.Configuration;
using VOAssociates;
using BOAssociates;
using BoCustomerProfiling;

namespace WealthERP.Associates
{
    public partial class AddAssociatesDetails : System.Web.UI.UserControl
    {
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        AssociatesVO associatesVo = new AssociatesVO();
        AssociatesBo associatesBo = new AssociatesBo();
        CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
        AdvisorBo advisorBo = new AdvisorBo();
        CustomerBo customerBo = new CustomerBo();

        int adviserId = 0;
        string path;
        string viewAction;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session["userVo"];
            viewAction = (string)Session["action"];
            if (Session["associatesVo"] != null)
            {
                associatesVo = (AssociatesVO)Session["associatesVo"];
            }
            if (!IsPostBack)
            {
                AssociatesDetails.SelectedIndex = 0;
                //if (Request.QueryString["action"] != null)
                //{
                //    SetControls(associatesVo);
                //}
                BindAccountType();
                BindBankName();
                BindState();
                BindQualification();
                BindClassification();
                BindRelationship();
                BindAssetCategory();
                BindMaritalStatus();
                if (viewAction == "View")
                {
                    SetEnableDisable();
                    associatesVo = (AssociatesVO)Session["associatesVo"];
                    if(associatesVo!=null)
                        SetEditViewControls(associatesVo);
                }
                if (viewAction == "Edit" || viewAction == "EditFromRequestPage")
                {
                    //SetControls(associatesVo);
                    associatesVo = (AssociatesVO)Session["associatesVo"];
                    if (associatesVo != null)
                        SetEditViewControls(associatesVo);
                }
               
            }
        }

        private void BindMaritalStatus()
        {
            DataTable dtMaritalStatus = XMLBo.GetMaritalStatus(path);
            ddlMaritalStatus.DataSource = dtMaritalStatus;
            ddlMaritalStatus.DataTextField = "MaritalStatus";
            ddlMaritalStatus.DataValueField = "MaritalStatusCode";
            ddlMaritalStatus.DataBind();
            ddlMaritalStatus.Items.Insert(0, new ListItem("Select", "Selec"));
        }

        private void SetEnableDisable()
        {
            txtBranch.Enabled = false;
            btnSubmit.Visible = false;
            txtRM.Enabled = false;
            txtAMFINo.Enabled = false;
            txtStartDate.Enabled = false;
            txtEndDate.Enabled = false;
            txtAssociateExpDate.Enabled = false;
            txtResPhoneNoStd.Enabled = false;
            txtResPhoneNo.Enabled = false;
            txtResFaxStd.Enabled = false;
            txtResFax.Enabled = false;
            txtOfcPhoneNoStd.Enabled = false;
            txtOfcPhoneNo.Enabled = false;
            txtOfcFaxStd.Enabled = false;
            txtOfcFax.Enabled = false;
            txtMobile1.Enabled = false;
            txtEmail.Enabled = false;
            txtCorLine1.Enabled = false;
            txtCorLine2.Enabled = false;
            txtCorLine3.Enabled = false;
            txtCorCity.Enabled = false;
            txtCorPin.Enabled = false;
            ddlCorState.Enabled = false;
            txtCorCountry.Enabled = false;

            txtPermAdrLine1.Enabled = false;
            txtPermAdrLine2.Enabled = false;
            txtPermAdrLine3.Enabled = false;
            txtPermAdrCity.Enabled = false;
            txtPermAdrPinCode.Enabled = false;
            ddlPermAdrState.Enabled = false;
            txtPermAdrCountry.Enabled = false;

            ddlMaritalStatus.Enabled = false;
            ddlQualification.Enabled = false;
            ddlGender.Enabled = false;
            txtDOB.Enabled = false;

            ddlBankName.Enabled = false;
            ddlAccountType.Enabled = false;
            txtAccountNumber.Enabled = false;
            txtBankAdrLine1.Enabled = false;
            txtBankAdrLine2.Enabled = false;
            txtBankAdrLine3.Enabled = false;
            txtBankAdrCity.Enabled = false;
            ddlBankAdrState.Enabled = false;
            txtBankAdrPinCode.Enabled = false;

            txtMicr.Enabled = false;
            txtIfsc.Enabled = false;
            ddlCategory.Enabled = false;
            txtRegNo.Enabled = false;
            txtRegExpDate.Enabled = false;


            txtNomineeName.Enabled = false;
            ddlNomineeRel.Enabled = false;
            txtNomineeAdress.Enabled = false;
            txtNomineePhone.Enabled = false;
            txtGurdiannName.Enabled = false;
            ddlGuardianRel.Enabled = false;
            txtGuardianAdress.Enabled = false;
            txtGurdianPhone.Enabled = false;

            ddlAdviserCategory.Enabled = false;

            txtNoBranches.Enabled = false;
            txtNoofSales.Enabled = false;
            txtNoofSubBrokers.Enabled = false;
            txtNoofClients.Enabled = false;

            chkAssociates.Enabled = false;
            chkMf.Enabled = false;
            chlIpo.Enabled = false;
            chkfd.Enabled = false;

        }

        private void SetEditViewControls(AssociatesVO associatesVo)
        {
            if (associatesVo.BMName != null)
                txtBranch.Text = associatesVo.BMName;
            if (associatesVo.RMNAme!= null)
                txtRM.Text = associatesVo.RMNAme;
            if (associatesVo.ContactPersonName != null)
                txtAssociateName.Text = associatesVo.ContactPersonName;
            if (associatesVo.AMFIregistrationNo != null)
                txtAMFINo.Text = associatesVo.AMFIregistrationNo;
            if (associatesVo.StartDate != null && associatesVo.StartDate != DateTime.MinValue)
                txtStartDate.SelectedDate = associatesVo.StartDate;
            if (associatesVo.EndDate != null && associatesVo.EndDate != DateTime.MinValue)
                txtEndDate.SelectedDate = associatesVo.EndDate;
            if (associatesVo.AssociationExpairyDate != null && associatesVo.AssociationExpairyDate != DateTime.MinValue)
                txtAssociateExpDate.SelectedDate = associatesVo.AssociationExpairyDate;
            if (associatesVo.ResPhoneNo != null)
                txtResPhoneNoStd.Text = associatesVo.ResPhoneNo.ToString();
            if (associatesVo.ResPhoneNo != null)
                txtResPhoneNo.Text = associatesVo.ResPhoneNo.ToString();
            if (associatesVo.ResFaxStd != null)
                txtResFaxStd.Text = associatesVo.ResFaxStd.ToString();
            if (associatesVo.ResFaxNumber != null)
                txtResFax.Text = associatesVo.ResFaxNumber.ToString();
            if (associatesVo.OfcSTDCode != null)
                txtOfcPhoneNoStd.Text = associatesVo.OfcSTDCode.ToString();
            if (associatesVo.OfficePhoneNo != null)
                txtOfcPhoneNo.Text = associatesVo.OfficePhoneNo.ToString();
            if (associatesVo.OfcFaxSTD != null)
                txtOfcFaxStd.Text = associatesVo.OfcFaxSTD.ToString();
            if (associatesVo.OfcFaxNumber != null)
                txtOfcFax.Text = associatesVo.OfcFaxNumber.ToString();
            if (associatesVo.Mobile != null)
                txtMobile1.Text = associatesVo.Mobile.ToString();
            if (associatesVo.Email != null)
                txtEmail.Text = associatesVo.Email;
            if (associatesVo.CorrAdrLine1 != null)
                txtCorLine1.Text = associatesVo.CorrAdrLine1;
            if (associatesVo.CorrAdrLine2 != null)
                txtCorLine2.Text = associatesVo.CorrAdrLine2;
            if (associatesVo.CorrAdrLine3 != null)
                txtCorLine3.Text = associatesVo.CorrAdrLine3;
            if (associatesVo.CorrAdrCity != null)
                txtCorCity.Text = associatesVo.CorrAdrCity;
            if (associatesVo.CorrAdrPinCode != null)
                txtCorPin.Text = associatesVo.CorrAdrPinCode.ToString();
            if (!String.IsNullOrEmpty(associatesVo.CorrAdrState))
                ddlCorState.SelectedValue = associatesVo.CorrAdrState;
            if (associatesVo.CorrAdrCountry != null)
                txtCorCountry.Text = associatesVo.CorrAdrCountry;

            if (associatesVo.PerAdrLine1 != null)
                txtPermAdrLine1.Text = associatesVo.PerAdrLine1;
            if (associatesVo.PerAdrLine2 != null)
                txtPermAdrLine2.Text = associatesVo.PerAdrLine2;
            if (associatesVo.PerAdrLine3 != null)
                txtPermAdrLine3.Text = associatesVo.PerAdrLine3;
            if (associatesVo.PerAdrCity != null)
                txtPermAdrCity.Text = associatesVo.PerAdrCity;
            if (associatesVo.PerAdrPinCode != null)
                txtPermAdrPinCode.Text = associatesVo.PerAdrPinCode.ToString();
            if (associatesVo.PerAdrState != null)
                ddlPermAdrState.SelectedValue = associatesVo.PerAdrState;
            if (associatesVo.PerAdrCountry != null)
                txtPermAdrCountry.Text = associatesVo.PerAdrCountry;

            if (associatesVo.MaritalStatusCode != null)
                ddlMaritalStatus.SelectedValue = associatesVo.MaritalStatusCode;
            if (associatesVo.QualificationCode != null)
                ddlQualification.SelectedValue = associatesVo.QualificationCode;
            if(associatesVo.Gender!=null)
                ddlGender.SelectedValue = associatesVo.Gender;
            if (associatesVo.DOB != DateTime.MinValue)
                txtDOB.SelectedDate = associatesVo.DOB;

            if (associatesVo.BankCode != null)
                ddlBankName.SelectedValue = associatesVo.BankCode;
            if (associatesVo.BankAccountTypeCode != null)
                ddlAccountType.SelectedValue = associatesVo.BankAccountTypeCode;
            if (associatesVo.AccountNum != null)
                txtAccountNumber.Text = associatesVo.AccountNum;
            if (associatesVo.BranchAdrLine1 != null)
                txtBankAdrLine1.Text = associatesVo.BranchAdrLine1;
            if (associatesVo.BranchAdrLine2 != null)
                txtBankAdrLine2.Text = associatesVo.BranchAdrLine2;
            if (associatesVo.BranchAdrLine3 != null)
                txtBankAdrLine3.Text = associatesVo.BranchAdrLine3;
            if (associatesVo.BranchAdrCity != null)
                txtBankAdrCity.Text = associatesVo.BranchAdrCity;
            if (associatesVo.BranchAdrState != null)
                ddlBankAdrState.Text = associatesVo.BranchAdrState;

            if (associatesVo.MICR != null)
                txtMicr.Text = associatesVo.MICR.ToString();
            if (associatesVo.IFSC != null)
                txtIfsc.Text = associatesVo.IFSC;
            if (associatesVo.assetGroupCode != null)
                ddlCategory.SelectedValue = associatesVo.assetGroupCode;
            if (associatesVo.Registrationumber != null)
                txtRegNo.Text = associatesVo.Registrationumber;
            if (associatesVo.ExpiryDate!= DateTime.MinValue)
                txtRegExpDate.SelectedDate = associatesVo.ExpiryDate;

            if (associatesVo.NomineeName != null)
                txtNomineeName.Text = associatesVo.NomineeName;
            if (associatesVo.RelationshipCode != null)
                ddlNomineeRel.SelectedValue = associatesVo.RelationshipCode;
            if (associatesVo.NomineeAddres != null)
                txtNomineeAdress.Text = associatesVo.NomineeAddres;
            if (associatesVo.NomineeTelNo != null)
                txtNomineePhone.Text = associatesVo.NomineeTelNo.ToString();
            if (associatesVo.GuardianName != null)
                txtGurdiannName.Text = associatesVo.GuardianName;
            if (associatesVo.GuardianRelationship != null)
                ddlGuardianRel.SelectedValue = associatesVo.GuardianRelationship;
            if (associatesVo.GuardianAddress != null)
                txtGuardianAdress.Text = associatesVo.GuardianAddress;
            if (associatesVo.GuardianTelNo != null)
                txtGurdianPhone.Text = associatesVo.GuardianTelNo.ToString();

            if (associatesVo.AdviserCategory != null)
                ddlAdviserCategory.SelectedValue = associatesVo.AdviserCategory;

            if (associatesVo.NoOfBranches != null)
                txtNoBranches.Text = associatesVo.NoOfBranches.ToString();
            if (associatesVo.NoOfSalesEmployees != null)
                txtNoofSales.Text = associatesVo.NoOfSalesEmployees.ToString();
            if (associatesVo.NoOfSubBrokers != null)
                txtNoofSubBrokers.Text = associatesVo.NoOfSubBrokers.ToString();
            if (associatesVo.NoOfClients != null)
                txtNoofClients.Text = associatesVo.NoOfClients.ToString();

            if (associatesVo.AAC_AgentCode != null)
                txtAdviserAgentCode.Text = associatesVo.AAC_AgentCode.Trim();

            //chkAssociates.Enabled = false;
            //chkMf.Enabled = false;
            //chlIpo.Enabled = false;
            //chkfd.Enabled = false;
        }

        private void BindAssetCategory()
        {
            DataTable dtAssetCategory = associatesBo.GetProductAssetGroup();
            if (dtAssetCategory.Rows.Count > 0)
            {
                ddlCategory.DataSource = dtAssetCategory;
                ddlCategory.DataValueField = dtAssetCategory.Columns["PAG_AssetGroupCode"].ToString();
                ddlCategory.DataTextField = dtAssetCategory.Columns["PAG_AssetGroupName"].ToString();
                ddlCategory.DataBind();
            }
            ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));
        }

        private void BindRelationship()
        {
            DataTable dtRelationship = customerBo.GetMemberRelationShip();
            //----------------------------------------Nominee Relation---------------------------
            ddlNomineeRel.DataSource = dtRelationship;
            ddlNomineeRel.DataTextField = dtRelationship.Columns["XR_Relationship"].ToString();
            ddlNomineeRel.DataValueField = dtRelationship.Columns["XR_RelationshipCode"].ToString();
            ddlNomineeRel.DataBind();
            ddlNomineeRel.Items.Insert(0, new ListItem("Select", "Select"));
            //----------------------------------------Gurdian Relation---------------------------
            ddlGuardianRel.DataSource = dtRelationship;
            ddlGuardianRel.DataTextField = dtRelationship.Columns["XR_Relationship"].ToString();
            ddlGuardianRel.DataValueField = dtRelationship.Columns["XR_RelationshipCode"].ToString();
            ddlGuardianRel.DataBind();
            ddlGuardianRel.Items.Insert(0, new ListItem("Select", "Select"));
        }

        private void BindClassification()
        {
            DataSet classificationDs = new DataSet();
            adviserId = advisorVo.advisorId;
            classificationDs = advisorBo.GetAdviserCustomerCategory(adviserId);
            ddlAdviserCategory.DataSource = classificationDs;
            ddlAdviserCategory.DataValueField = classificationDs.Tables[0].Columns["ACC_CustomerCategoryCode"].ToString();
            ddlAdviserCategory.DataTextField = classificationDs.Tables[0].Columns["ACC_customerCategoryName"].ToString();
            ddlAdviserCategory.DataBind();
            ddlAdviserCategory.Items.Insert(0, new ListItem("Select", "Select"));
        }
        private void BindQualification()
        {
            DataTable dtQualification;
            dtQualification = XMLBo.GetQualification(path);
            ddlQualification.DataSource = dtQualification;
            ddlQualification.DataTextField = "Qualification";
            ddlQualification.DataValueField = "QualificationCode";
            ddlQualification.DataBind();
            ddlQualification.Items.Insert(0, new ListItem("Select a Qualification", "Select a Qualification"));
        }
  

        private void BindAccountType()
        {
            DataTable dtAccType = new DataTable();
            dtAccType = customerBankAccountBo.AssetBankaccountType();
            ddlAccountType.DataSource = dtAccType;
            ddlAccountType.DataValueField = dtAccType.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddlAccountType.DataTextField = dtAccType.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddlAccountType.DataBind();
            ddlAccountType.Items.Insert(0, new ListItem("Select", "Select"));
        }

        private void BindState()
        {
            DataTable dtBankState = new DataTable();
            dtBankState = XMLBo.GetStates(path);
            ddlBankAdrState.DataSource = dtBankState;
            ddlBankAdrState.DataTextField = "StateName";
            ddlBankAdrState.DataValueField = "StateCode";
            ddlBankAdrState.DataBind();
            ddlBankAdrState.Items.Insert(0, new ListItem("Select", "Select"));
            //-------------------------------------------------------------------------------------
            ddlCorState.DataSource = dtBankState;
            ddlCorState.DataTextField = "StateName";
            ddlCorState.DataValueField = "StateCode";
            ddlCorState.DataBind();
            ddlCorState.Items.Insert(0, new ListItem("Select", "Select"));
            //-------------------------------------------------------------------------------------
            ddlPermAdrState.DataSource = dtBankState;
            ddlPermAdrState.DataTextField = "StateName";
            ddlPermAdrState.DataValueField = "StateCode";
            ddlPermAdrState.DataBind();
            ddlPermAdrState.Items.Insert(0, new ListItem("Select", "Select"));
        }

        private void BindBankName()
        {
            DataTable dtBankName = new DataTable();
            dtBankName = customerBankAccountBo.GetALLBankName();
            ddlBankName.DataSource = dtBankName;
            ddlBankName.DataValueField = dtBankName.Columns["WERPBM_BankCode"].ToString();
            ddlBankName.DataTextField = dtBankName.Columns["WERPBM_BankName"].ToString();
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));
        }

        private void SetControls(AssociatesVO associatesVo)
        {
            txtBranch.Text = associatesVo.BMName;
            txtRM.Text = associatesVo.RMNAme;
            txtAssociateName.Text = associatesVo.ContactPersonName;
            txtEmail.Text = associatesVo.Email;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int associationId = 0;
            bool result = false;

            if (associatesVo.AdviserAssociateId != 0)
                associationId = associatesVo.AdviserAssociateId;

            associatesVo.AdviserAssociateId = associationId;
            associatesVo.AAC_AdviserAgentId = associatesVo.AAC_AdviserAgentId;
            associatesVo.ContactPersonName = txtAssociateName.Text;

            //------------------------------CONTACT DETAILS--------------
            if (txtResPhoneNoIsd.Text != null)
                associatesVo.ResISDCode = int.Parse(txtResPhoneNoIsd.Text);
            else
                associatesVo.ResISDCode = 0;
            if (!string.IsNullOrEmpty(txtResPhoneNo.Text))
                associatesVo.ResPhoneNo = int.Parse(txtResPhoneNo.Text);
            else
                associatesVo.ResPhoneNo = 0;

            if (!string.IsNullOrEmpty(txtResFaxStd.Text))
                associatesVo.ResFaxStd = int.Parse(txtResFaxStd.Text);
            else
                associatesVo.ResFaxStd = 0;
            if (!string.IsNullOrEmpty(txtResFax.Text))
                associatesVo.ResFaxNumber = int.Parse(txtResFax.Text);
            else
                associatesVo.ResFaxNumber = 0;
            if (!string.IsNullOrEmpty(txtOfcFaxStd.Text))
                associatesVo.OfcFaxNumber = int.Parse(txtOfcFaxStd.Text);
            else
                associatesVo.OfcFaxNumber = 0;
            if (!string.IsNullOrEmpty(txtMobile1.Text))
                associatesVo.Mobile = long.Parse(txtMobile1.Text);
            else
                associatesVo.Mobile = 0;
            if (txtEmail.Text != null)
                associatesVo.Email = txtEmail.Text;
            else
                associatesVo.Email = "";
            #region ---------------------------------------CORRESPONDING ADDRESS-------------------------------------------

            if (txtCorLine1.Text != null)
                associatesVo.CorrAdrLine1 = txtCorLine1.Text;
            else
                associatesVo.CorrAdrLine1 = "";
            if (txtCorLine2.Text != null)
                associatesVo.CorrAdrLine2 = txtCorLine2.Text;
            else
                associatesVo.CorrAdrLine2 = "";
            if (txtCorLine3.Text != null)
                associatesVo.CorrAdrLine3 = txtCorLine3.Text;
            else
                associatesVo.CorrAdrLine3 = "";
            if (txtCorCity.Text != null)
                associatesVo.CorrAdrCity = txtCorCity.Text;
            else
                associatesVo.CorrAdrCity = "";
            if (!string.IsNullOrEmpty(txtCorPin.Text))
                associatesVo.CorrAdrPinCode = int.Parse(txtCorPin.Text);
            else
                associatesVo.CorrAdrPinCode = 0;
            if (ddlCorState.SelectedIndex != 0)
                associatesVo.CorrAdrState = ddlCorState.SelectedValue;
            else
                associatesVo.CorrAdrState = "";
            if (txtCorCountry.Text != null)
                associatesVo.CorrAdrCountry = txtCorCountry.Text;
            else
                associatesVo.CorrAdrCountry = "";

            #endregion
            #region //---------------------------------------PERMANENT ADDRESS-------------------------------------------


            if (txtPermAdrLine1.Text != null)
                associatesVo.PerAdrLine1 = txtPermAdrLine1.Text;
            else
                associatesVo.PerAdrLine1 = "";
            if (txtPermAdrLine2.Text != null)
                associatesVo.PerAdrLine2 = txtPermAdrLine2.Text;
            else
                associatesVo.PerAdrLine2 = "";
            if (txtPermAdrLine3.Text != null)
                associatesVo.PerAdrLine3 = txtPermAdrLine3.Text;
            else
                associatesVo.PerAdrLine3 = "";
            if (txtPermAdrCity.Text != null)
                associatesVo.PerAdrCity = txtCorCity.Text;
            else
                associatesVo.PerAdrCity = "";
            if (!string.IsNullOrEmpty(txtPermAdrPinCode.Text))
                associatesVo.PerAdrPinCode = int.Parse(txtPermAdrPinCode.Text);
            else
                associatesVo.PerAdrPinCode = 0;
            if (ddlPermAdrState.SelectedIndex != 0)
                associatesVo.PerAdrState = ddlPermAdrState.SelectedValue;
            else
                associatesVo.PerAdrState = "";
            if (txtPermAdrCountry.Text != null)
                associatesVo.PerAdrCountry = txtPermAdrCountry.Text;
            else
                associatesVo.PerAdrCountry = "";
            #endregion
            //---------------------------------------OTHER INFO---------------------------------------------

            if (ddlMaritalStatus.SelectedIndex != 0)
                associatesVo.MaritalStatusCode = ddlMaritalStatus.SelectedValue;
            else
                associatesVo.MaritalStatusCode = "";
            if (ddlQualification.SelectedIndex != 0)
                associatesVo.QualificationCode = ddlQualification.SelectedValue;
            else
                associatesVo.QualificationCode = "";
            associatesVo.Gender = ddlGender.SelectedValue;
            if (txtDOB.SelectedDate != DateTime.MinValue)
                associatesVo.DOB = Convert.ToDateTime(txtDOB.SelectedDate);

            //---------------------------------------BANK DETAILS-------------------------------------------

            if (ddlBankName.SelectedIndex != 0)
                associatesVo.BankCode = ddlBankName.SelectedValue;
            else
                associatesVo.BankCode = "";
            if (ddlAccountType.SelectedIndex != 0)
                associatesVo.BankAccountTypeCode = ddlAccountType.SelectedValue;
            else
                associatesVo.BankAccountTypeCode = "";
            if (txtAccountNumber.Text != null)
                associatesVo.AccountNum = txtAccountNumber.Text;
            else
                associatesVo.AccountNum = "";
            if (txtBankBranchName.Text != null)
                associatesVo.BranchName = txtBankBranchName.Text;
            else
                associatesVo.BranchName = "";
            if (txtBankAdrLine1.Text != null)
                associatesVo.BranchAdrLine1 = txtBankAdrLine1.Text;
            else
                associatesVo.BranchAdrLine1 = "";
            if (txtBankAdrLine2.Text != null)
                associatesVo.BranchAdrLine2 = txtBankAdrLine2.Text;
            else
                associatesVo.BranchAdrLine2 = "";
            if (txtBankAdrLine3.Text != null)
                associatesVo.BranchAdrLine3 = txtBankAdrLine3.Text;
            else
                associatesVo.BranchAdrLine3 = "";
            if (txtBankAdrCity.Text != null)
                associatesVo.BranchAdrCity = txtBankAdrCity.Text;
            else
                associatesVo.BranchAdrCity = "";
            if (ddlBankAdrState.SelectedIndex != 0)
                associatesVo.BranchAdrState = ddlBankAdrState.SelectedValue;
            else
                associatesVo.BranchAdrState = "";
            if (!string.IsNullOrEmpty(txtMicr.Text))
                associatesVo.MICR = int.Parse(txtMicr.Text);
            else
                associatesVo.MICR = 0;
            if (txtIfsc.Text != null)
                associatesVo.IFSC = txtIfsc.Text;
            else
                associatesVo.IFSC = "";
            //---------------------------------------Registration-------------------------------------------

            if (txtRegNo.Text != null)
                associatesVo.Registrationumber = txtRegNo.Text;
            else
                associatesVo.Registrationumber = "";
            if (ddlCategory.SelectedIndex != 0)
                associatesVo.assetGroupCode = ddlCategory.SelectedValue;
            else
                associatesVo.assetGroupCode = "";
            if (txtRegExpDate.SelectedDate != DateTime.MinValue)
                associatesVo.ExpiryDate = Convert.ToDateTime(txtRegExpDate.SelectedDate);
            //---------------------------------------NOMINEE-------------------------------------------

            if (txtNomineeName.Text != null)
                associatesVo.NomineeName = txtNomineeName.Text;
            else
                associatesVo.NomineeName = "";
            if (ddlNomineeRel.SelectedIndex != 0)
                associatesVo.RelationshipCode = ddlNomineeRel.SelectedValue;
            else
                associatesVo.RelationshipCode = "";
            if (txtNomineeAdress.Text != null)
                associatesVo.NomineeAddres = txtNomineeAdress.Text;
            else
                associatesVo.NomineeAddres = "";
            if (!string.IsNullOrEmpty(txtNomineePhone.Text))
                associatesVo.NomineeTelNo = int.Parse(txtNomineePhone.Text);
            else
                associatesVo.NomineeTelNo = 0;
            if (txtGurdiannName.Text != null)
                associatesVo.GuardianName = txtGurdiannName.Text;
            else
                associatesVo.GuardianName = "";
            if (txtGuardianAdress.Text != null)
                associatesVo.GuardianAddress = txtGuardianAdress.Text;
            else
                associatesVo.GuardianAddress = "";
            if (!string.IsNullOrEmpty(txtGurdianPhone.Text))
                associatesVo.GuardianTelNo = int.Parse(txtGurdianPhone.Text);
            else
                associatesVo.GuardianTelNo = 0;
            if (ddlGuardianRel.SelectedIndex != 0)
                associatesVo.GuardianRelationship = ddlGuardianRel.SelectedValue;
            else
                associatesVo.GuardianRelationship = "";
            //---------------------------------------NOMINEE-------------------------------------------

            if (ddlAdviserCategory.SelectedIndex != 0)
                associatesVo.AdviserCategory = ddlAdviserCategory.SelectedValue;
            else
                associatesVo.AdviserCategory = "";
            //---------------------------------------Business Details-----------------------------------

            if (txtStartDate.SelectedDate == null)
                associatesVo.StartDate = DateTime.MinValue;
            else
                associatesVo.StartDate = Convert.ToDateTime(txtStartDate.SelectedDate);
            if (txtEndDate.SelectedDate == null)
                associatesVo.EndDate = DateTime.MinValue;
            else
                associatesVo.EndDate = Convert.ToDateTime(txtEndDate.SelectedDate);
            if (txtAssociateExpDate.SelectedDate == null)
                associatesVo.AssociationExpairyDate = DateTime.MinValue;
            else
                associatesVo.AssociationExpairyDate = Convert.ToDateTime(txtAssociateExpDate.SelectedDate);
            if (txtAMFINo.Text != null)
                associatesVo.AMFIregistrationNo = txtAMFINo.Text;
            else
                associatesVo.AMFIregistrationNo = "";
            if (!string.IsNullOrEmpty(txtNoBranches.Text))
                associatesVo.NoOfBranches = int.Parse(txtNoBranches.Text);
            else
                associatesVo.NoOfBranches = 0;
            if (!string.IsNullOrEmpty(txtNoofSales.Text))
                associatesVo.NoOfSalesEmployees = int.Parse(txtNoofSales.Text);
            else
                associatesVo.NoOfSalesEmployees = 0;
            if (!string.IsNullOrEmpty(txtNoofSubBrokers.Text))
                associatesVo.NoOfSubBrokers = int.Parse(txtNoofSubBrokers.Text);
            else
                associatesVo.NoOfSubBrokers = 0;
            if (!string.IsNullOrEmpty(txtNoofClients.Text))
                associatesVo.NoOfClients = int.Parse(txtNoofClients.Text);
            else
                associatesVo.NoOfClients = 0;



            result = associatesBo.UpdateAdviserAssociates(associatesVo);
            Session["associatesVo"] = associatesVo;
            if (result == true)
            {
                if (viewAction == "View" || viewAction == "Edit")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewAdviserAssociateList');", true);

                                       //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddBranchRMAgentAssociation','?AssociationId=" + associationId + "');", true);
                }
                else if (viewAction == "EditFromRequestPage")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddAssociates','?AssociationId=" + associationId + "&pageName=" + "AddAssociates" + "');", true);
                }
            }
        }
    }
}