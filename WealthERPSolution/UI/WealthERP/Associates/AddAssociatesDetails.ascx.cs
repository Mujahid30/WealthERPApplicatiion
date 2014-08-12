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
using BoUser;

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
        List<int> associatesIds;
        UserBo userBo = new UserBo();

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
                BindHierarchyTitleDropList();
                //BindCity(0);
                BindSubTypeDropDown("IND");
                if (viewAction == "View")
                {
                    SetEnableDisable(0);
                    associatesVo = (AssociatesVO)Session["associatesVo"];
                    if (associatesVo != null)
                        SetEditViewControls(associatesVo);
                    head.InnerText = "View Associates";
                    lnkBtnEdit.Visible = true;
                    lnlBack.Visible = true;
                }
                if (viewAction == "Edit" || viewAction == "EditFromRequestPage")
                {
                    associatesVo = (AssociatesVO)Session["associatesVo"];
                    if (associatesVo != null)
                        SetEditViewControls(associatesVo);
                    head.InnerText = "Edit Associates";
                    lnkBtnEdit.Visible = false;
                    lnlBack.Visible = true;
                }

            }
            if (userVo.UserType != "Advisor") { lnkBtnEdit.Visible = false; }
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

        private void SetEnableDisable(int flag)
        {
            if (flag == 0)
            {
               // txtBranch.Enabled = false;
                btnSubmit.Visible = false;
               // txtRM.Enabled = false;
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

                //chkAssociates.Enabled = false;
                //chkMf.Enabled = false;
                //chlIpo.Enabled = false;
                //chkfd.Enabled = false;
                chkModules.Enabled = false;
                txtEUIN.Enabled = false;
                ddlAssociateSubType.Enabled = false;
                rbtnIndividual.Enabled = false;
                rbtnNonIndividual.Enabled = false;
            }
            else
            {
                txtBranch.Enabled = false;
                btnSubmit.Visible = true;
                txtRM.Enabled = false;
                txtAMFINo.Enabled = true;
                txtStartDate.Enabled = true;
                txtEndDate.Enabled = true;
                txtAssociateExpDate.Enabled = true;
                txtResPhoneNoStd.Enabled = true;
                txtResPhoneNo.Enabled = true;
                txtResFaxStd.Enabled = true;
                txtResFax.Enabled = true;
                txtOfcPhoneNoStd.Enabled = true;
                txtOfcPhoneNo.Enabled = true;
                txtOfcFaxStd.Enabled = true;
                txtOfcFax.Enabled = true;
                txtMobile1.Enabled = true;
                txtEmail.Enabled = true;
                txtCorLine1.Enabled = true;
                txtCorLine2.Enabled = true;
                txtCorLine3.Enabled = true;
                txtCorCity.Enabled = true;
                txtCorPin.Enabled = true;
                ddlCorState.Enabled = true;
                txtCorCountry.Enabled = true;

                txtPermAdrLine1.Enabled = true;
                txtPermAdrLine2.Enabled = true;
                txtPermAdrLine3.Enabled = true;
                txtPermAdrCity.Enabled = true;
                txtPermAdrPinCode.Enabled = true;
                ddlPermAdrState.Enabled = true;
                txtPermAdrCountry.Enabled = true;

                ddlMaritalStatus.Enabled = true;
                ddlQualification.Enabled = true;
                ddlGender.Enabled = true;
                txtDOB.Enabled = true;

                ddlBankName.Enabled = true;
                ddlAccountType.Enabled = true;
                txtAccountNumber.Enabled = true;
                txtBankAdrLine1.Enabled = true;
                txtBankAdrLine2.Enabled = true;
                txtBankAdrLine3.Enabled = true;
                txtBankAdrCity.Enabled = true;
                ddlBankAdrState.Enabled = true;
                txtBankAdrPinCode.Enabled = true;

                txtMicr.Enabled = true;
                txtIfsc.Enabled = true;
                ddlCategory.Enabled = true;
                txtRegNo.Enabled = true;
                txtRegExpDate.Enabled = true;


                txtNomineeName.Enabled = true;
                ddlNomineeRel.Enabled = true;
                txtNomineeAdress.Enabled = true;
                txtNomineePhone.Enabled = true;
                txtGurdiannName.Enabled = true;
                ddlGuardianRel.Enabled = true;
                txtGuardianAdress.Enabled = true;
                txtGurdianPhone.Enabled = true;

                ddlAdviserCategory.Enabled = true;

                txtNoBranches.Enabled = true;
                txtNoofSales.Enabled = true;
                txtNoofSubBrokers.Enabled = true;
                txtNoofClients.Enabled = true;

                //chkAssociates.Enabled = false;
                //chkMf.Enabled = false;
                //chlIpo.Enabled = false;
                //chkfd.Enabled = false;
                chkModules.Enabled = true;
                txtEUIN.Enabled = true;
                ddlAssociateSubType.Enabled = true;
                rbtnIndividual.Enabled = true;
                rbtnNonIndividual.Enabled = true;
            }

        }

        private void SetEditViewControls(AssociatesVO associatesVo)
        {
            //if (associatesVo.BMName != null)
            //    txtBranch.Text = associatesVo.BMName;
            //if (associatesVo.RMNAme != null)
            //    txtRM.Text = associatesVo.RMNAme;
            BindStaffBranchDropList(associatesVo.BranchId);
            BindStaffDropList(11);
            if (associatesVo.BMName != null)
                ddlBranch.SelectedValue = associatesVo.BMName;
            if (associatesVo.RMNAme != null)
                ddlRM.SelectedValue = associatesVo.RMNAme;
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
            //if (!string.IsNullOrEmpty(associatesVo.CorrAdrCity))
            //    ddlCorCity.SelectedValue = associatesVo.CorrAdrCity;
            if (associatesVo.CorrAdrPinCode != null)
                txtCorPin.Text = associatesVo.CorrAdrPinCode.ToString();
            if (!String.IsNullOrEmpty(associatesVo.CorrAdrState))
                ddlCorState.SelectedValue = associatesVo.CorrAdrState;
            if (associatesVo.CorrAdrCountry != null)
                txtCorCountry.Text = associatesVo.CorrAdrCountry;
            if (associatesVo.PanNo != null)
                txtPan.Text = associatesVo.PanNo;

            if (associatesVo.PerAdrLine1 != null)
                txtPermAdrLine1.Text = associatesVo.PerAdrLine1;
            if (associatesVo.PerAdrLine2 != null)
                txtPermAdrLine2.Text = associatesVo.PerAdrLine2;
            if (associatesVo.PerAdrLine3 != null)
                txtPermAdrLine3.Text = associatesVo.PerAdrLine3;
            //if (associatesVo.PerAdrCity != null)
            //    txtPermAdrCity.Text = associatesVo.PerAdrCity;
            if (associatesVo.PerAdrPinCode != null)
                txtPermAdrPinCode.Text = associatesVo.PerAdrPinCode.ToString();
            //BindState();
            if (!string.IsNullOrEmpty(associatesVo.PerAdrState))
                ddlPermAdrState.SelectedValue = associatesVo.PerAdrState;
            if (associatesVo.PerAdrCountry != null)
                txtPermAdrCountry.Text = associatesVo.PerAdrCountry;

            if (associatesVo.MaritalStatusCode != null)
                ddlMaritalStatus.SelectedValue = associatesVo.MaritalStatusCode;
            if (associatesVo.QualificationCode != null)
                ddlQualification.SelectedValue = associatesVo.QualificationCode;
            if (associatesVo.Gender != null)
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
            //if (!string.IsNullOrEmpty(associatesVo.BranchAdrCity))
            //    ddlBankAdrCity.SelectedValue = associatesVo.BranchAdrCity;
            if (!string.IsNullOrEmpty(associatesVo.BranchAdrState))
                ddlBankAdrState.Text = associatesVo.BranchAdrState;

            if (associatesVo.MICR != null)
                txtMicr.Text = associatesVo.MICR.ToString();
            if (associatesVo.IFSC != null)
                txtIfsc.Text = associatesVo.IFSC;
            if (associatesVo.assetGroupCode != null)
                ddlCategory.SelectedValue = associatesVo.assetGroupCode;
            if (associatesVo.Registrationumber != null)
                txtRegNo.Text = associatesVo.Registrationumber;
            if (associatesVo.ExpiryDate != DateTime.MinValue)
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
            if (!string.IsNullOrEmpty(associatesVo.GuardianRelationship))
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
            if (associatesVo.EUIN != null)
                txtEUIN.Text = associatesVo.EUIN;
            if (associatesVo.AssociateType == "IND")
            {
                rbtnIndividual.Checked = true;
                BindSubTypeDropDown("IND");
            }
            if (associatesVo.AssociateType == "NIND")
            {
                rbtnNonIndividual.Checked = true;
                BindSubTypeDropDown("NIND");
            }
            if (associatesVo.AssociateSubType != null)
                ddlAssociateSubType.SelectedValue = associatesVo.AssociateSubType;
            if (associatesVo.assetGroupCode != null)
            {
                string assetlist = associatesVo.assetGroupCode;
                string[] words = assetlist.Split(',');
                for (int i = 0; i < chkModules.Items.Count; i++)
                {
                    foreach (string word in words)
                    {
                        if (chkModules.Items[i].Value == word)
                            chkModules.Items[i].Selected = true;
                    }
                    //if (chkModules.Items[i].Selected == true)
                    //{
                    //    //assetGroupCodes += chkModules.Items[i].Value + "~";
                    //}
                }


            }

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
            //dtBankState = associatesBo.GetStateList();
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

        //private void BindCity(int flag)
        //{
        //    string stateId = string.Empty;
        //    DataTable dtBindCity;
        //    if (ddlBankAdrState.SelectedIndex != 0)
        //        stateId = ddlBankAdrState.SelectedValue;
        //    else if (ddlCorState.SelectedIndex != 0)
        //        stateId = ddlCorState.SelectedValue;
        //    else if (ddlPermAdrState.SelectedIndex != 0)
        //        stateId = ddlPermAdrState.SelectedValue;
        //    dtBindCity = associatesBo.GetCityList(stateId, flag);
        //    ddlBankAdrCity.DataSource = dtBindCity;
        //    ddlBankAdrCity.DataTextField = "cityname";
        //    ddlBankAdrCity.DataValueField = "cityid";
        //    ddlBankAdrCity.DataBind();
        //    ddlBankAdrCity.Items.Insert(0, new ListItem("Select", "Select"));
        //    //-------------------------------------------------------------------------------------

        //    ddlCorCity.DataSource = dtBindCity;
        //    ddlCorCity.DataTextField = "cityname";
        //    ddlCorCity.DataValueField = "cityid";
        //    ddlCorCity.DataBind();
        //    ddlCorCity.Items.Insert(0, new ListItem("Select", "Select"));
        //    //-------------------------------------------------------------------------------------
        //    ddlPermAdrCity.DataSource = dtBindCity;
        //    ddlPermAdrCity.DataTextField = "cityname";
        //    ddlPermAdrCity.DataValueField = "cityid";
        //    ddlPermAdrCity.DataBind();
        //    ddlPermAdrCity.Items.Insert(0, new ListItem("Select", "Select"));
        //}

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

        protected void Update_Click(object sender, EventArgs e)
        {
            int associationId = 0;
            bool result = false;
            string assetGroupCodes;
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
            //if (ddlCorCity.SelectedIndex != 0)
            //    associatesVo.CorrAdrCity = ddlCorCity.SelectedValue;

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
            //if (ddlPermAdrCity.SelectedIndex != 0)
            //    associatesVo.PerAdrCity = ddlPermAdrCity.SelectedValue;
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
            //if (ddlBankAdrCity.SelectedIndex != 0)
            //    associatesVo.BranchAdrCity = ddlBankAdrCity.SelectedValue;
            if (ddlBankAdrState.SelectedIndex != 0)
                associatesVo.BranchAdrState = ddlBankAdrState.SelectedValue;
            else
                associatesVo.BranchAdrState = "";
            if (txtMicr.Text != null)
                associatesVo.MICR = txtMicr.Text;
            else
                associatesVo.MICR = "";
            if (txtIfsc.Text != null)
                associatesVo.IFSC = txtIfsc.Text;
            else
                associatesVo.IFSC = "";
            //---------------------------------------Registration-------------------------------------------

            if (txtRegNo.Text != null)
                associatesVo.Registrationumber = txtRegNo.Text;
            else
                associatesVo.Registrationumber = "";
            //if (ddlCategory.SelectedIndex != 0)
            //    associatesVo.assetGroupCode = ddlCategory.SelectedValue;
            //else
            //    associatesVo.assetGroupCode = "";

            assetGroupCodes = GetAssetGroup();
            if (assetGroupCodes != null)
                associatesVo.assetGroupCode = assetGroupCodes;

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
            //---------------------------------------Category-------------------------------------------

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
            if (!string.IsNullOrEmpty(txtEUIN.Text))
                associatesVo.EUIN = txtEUIN.Text;
            if (rbtnIndividual.Checked == true)
                associatesVo.AssociateType = "IND";
            if (rbtnNonIndividual.Checked == true)
                associatesVo.AssociateType = "NIND";
            if (ddlAssociateSubType.SelectedIndex != 0)
                associatesVo.AssociateSubType = ddlAssociateSubType.SelectedValue;




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

        private string GetAssetGroup()
        {
            string assetGroupCodes = "";
            int i = 0;
            for (i = 0; i < chkModules.Items.Count; i++)
            {
                if (chkModules.Items[i].Selected == true)
                {
                    assetGroupCodes += chkModules.Items[i].Value + "~";
                }
            }
            return assetGroupCodes;
        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            if (Session["associatesVo"] != null)
            {
                associatesVo = (AssociatesVO)Session["associatesVo"];
                if (associatesVo != null)
                    SetEnableDisable(1);
                lnkBtnEdit.Visible = false;
            }
        }

        protected void lnlBack_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewAdviserAssociateList');", true);
        }

        protected void chkAddressChk_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddressChk.Checked == true)
            {
                txtPermAdrLine1.Text = txtCorLine1.Text;
                txtPermAdrLine2.Text = txtCorLine2.Text;
                txtPermAdrLine3.Text = txtCorLine3.Text;
                ddlPermAdrState.SelectedValue = ddlCorState.SelectedValue;
                txtPermAdrCity.Text = txtCorCity.Text;
                //if (ddlCorCity.SelectedIndex != 0)
                //{
                //    BindCity(1);
                //    ddlPermAdrState.SelectedValue = ddlCorCity.SelectedValue;
                //}
                txtPermAdrPinCode.Text = txtCorPin.Text;

                txtPermAdrCountry.Text = txtCorCountry.Text;

            }
        }

        //protected void ddlBankAdrState_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlBankAdrState.SelectedIndex != 0)
        //        BindCity(1);
        //}

        //protected void ddlPermAdrState_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlPermAdrState.SelectedIndex != 0)
        //        BindCity(1);
        //}

        //protected void ddlCorState_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCorState.SelectedIndex != 0)
        //        BindCity(1);
        //}
        protected void rbtnIndividual_CheckedChanged(object sender, EventArgs e)
        {
            BindSubTypeDropDown("IND");
        }
        protected void rbtnNonIndividual_CheckedChanged(object sender, EventArgs e)
        {
            BindSubTypeDropDown("NIND");
        }

        private void BindSubTypeDropDown(string type)
        {
            DataTable dt;
            dt = XMLBo.GetCustomerSubType(path, type);
            ddlAssociateSubType.DataSource = dt;
            ddlAssociateSubType.DataTextField = "CustomerTypeName";
            ddlAssociateSubType.DataValueField = "CustomerSubTypeCode";
            ddlAssociateSubType.DataBind();
            ddlAssociateSubType.Items.Insert(0, new ListItem("Select", "Select"));
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            UserVo associateUserVo = new UserVo();
            Random id = new Random();
            string password = id.Next(10000, 99999).ToString();

            associateUserVo.Password = password;
            associateUserVo.LoginId = txtEmail.Text.ToString();
            associateUserVo.FirstName = txtAssociateName.Text.ToString();
            associateUserVo.Email = txtEmail.Text.ToString();
            associateUserVo.UserType = "Associates";

            associatesVo.ContactPersonName = txtAssociateName.Text;
            associatesVo.BranchId = 1339;
            associatesVo.BMName = ddlBranch.SelectedValue;
            associatesVo.RMId = 4682;
            associatesVo.RMNAme = ddlRM.SelectedValue;
            associatesVo.UserRoleId = 1009;
            associatesVo.Email = txtEmail.Text;
            associatesVo.PanNo = txtPan.Text;
            if (!string.IsNullOrEmpty(txtMobile1.Text))
                associatesVo.Mobile = long.Parse(txtMobile1.Text);
            else
                associatesVo.Mobile = 0;
            associatesVo.RequestDate = DateTime.Now;
            associatesVo.AAC_UserType = "Associates";
            Session["TempAssociatesVo"] = associatesVo;
            Session["AssociateUserVo"] = associateUserVo;
            associatesIds = associatesBo.CreateCompleteAssociates(associateUserVo, associatesVo, userVo.UserId);
            associatesVo.UserId = associatesIds[0];
            associatesVo.AdviserAssociateId = associatesIds[1];
            //   txtGenerateReqstNum.Text = associatesVo.AdviserAssociateId.ToString();
            //Session["userId"] = associatesVo.UserId;
            //Session["associatesId"] = associatesVo.AdviserAssociateId;
            Session["AdviserAgentId"] = associatesVo.AAC_AdviserAgentId;
            //------------------------ To create User role Association-----------------------
            userBo.CreateRoleAssociation(associatesVo.UserId, 1009);

            //    if (associatesIds.Count > 0)
            //    {
            //        HideAndShowBasedOnRole(associatesIds[1]);
            //    }

            //    AssignHeaderInfo();
            //    SetAccsessMode();
            //    //txtRequestNumber.Text = associatesVo.AdviserAssociateId.ToString();
            //    divStep1SuccMsg.Visible = true;
            //}
            //else
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Pan Number Already Exists');", true);
            //}
        }
        private void BindHierarchyTitleDropList()
        {
            DataTable dtAdviserHierachyTitleList = new DataTable();
            dtAdviserHierachyTitleList = associatesBo.GetAdviserHierarchyTitleList(advisorVo.advisorId);
            ddlTitleList.DataSource = dtAdviserHierachyTitleList;
            ddlTitleList.DataValueField = dtAdviserHierachyTitleList.Columns["AH_Id"].ToString();
            ddlTitleList.DataTextField = dtAdviserHierachyTitleList.Columns["AH_HierarchyName"].ToString();
            ddlTitleList.DataBind();
            ddlTitleList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }
        protected void ddlTitleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTitleList.SelectedIndex != -1)
            {
                BindStaffDropList(Convert.ToInt32(ddlTitleList.SelectedValue));
            }

        }
        private void BindStaffBranchDropList(int staffId)
        {
            DataTable dtAdviserStaffBranchList = new DataTable();
            dtAdviserStaffBranchList = associatesBo.GetAdviserStaffBranchList(staffId);
            ddlBranch.DataSource = dtAdviserStaffBranchList;
            ddlBranch.DataValueField = dtAdviserStaffBranchList.Columns["AB_BranchId"].ToString();
            ddlBranch.DataTextField = dtAdviserStaffBranchList.Columns["AB_BranchName"].ToString();
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }
        protected void ddlRM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRM.SelectedIndex > 0)
            {
                BindStaffBranchDropList(Convert.ToInt32(ddlRM.SelectedValue));
            }

        }

        private void BindStaffDropList(int hierarchyId)
        {

            DataSet ds = associatesBo.GetAdviserHierarchyStaffList(hierarchyId);
            if (ds != null)
            {
                ddlRM.DataSource = ds.Tables[0]; ;
                ddlRM.DataValueField = ds.Tables[0].Columns["AR_RMId"].ToString();
                ddlRM.DataTextField = ds.Tables[0].Columns["AR_RMName"].ToString();
                ddlRM.DataBind();
            }
            ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }


    }
}