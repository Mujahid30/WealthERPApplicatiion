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
using System.Net;
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
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        int adviserId = 0;
        string path;
        string viewAction;
        int associateId;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session["userVo"];
            //viewAction = (string)Session["action"];
            if (Session["associatesVo"] != null)
            {
                associatesVo = (AssociatesVO)Session["associatesVo"];
            }

            if (!IsPostBack)
            {
                lblPanDuplicate.Visible = false;
                lblPanlength.Visible = false;
                AssociatesDetails.SelectedIndex = 0;
                //if (Request.QueryString["action"] != null)
                //{
                //    SetControls(associatesVo);
                //}
                BindRegistration();
                BindAccountType();
                BindBankName();
                BindAssociatetype();
                BindState();
                BindQualification();
                BindClassification();
                BindRelationship();
                //BindAssetCategory();
                BindMaritalStatus();
                BindHierarchyTitleDropList();
                //BindCity(0);
                BinddepartDropList();
                BindDepartmentRole();
                BindSubTypeDropDown("IND");
                RadTab radTABChildCodes = RadTabStripAssociatesDetails.Tabs.FindTabByValue("Child_Codes");
                btnPreviewSend.OnClientClick = "window.document.forms[0].target='_blank'; setTimeout(function(){window.document.forms[0].target='';}, 500);";
                if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
                {
                    BtnSave.Visible = false;
                    if (Request.QueryString["action"].Trim() == "View")
                    {
                        //if (viewAction == "View")
                        //{
                        BindRegistration();
                        SetEnableDisable(0);
                        associatesVo = (AssociatesVO)Session["associatesVo"];
                        if (associatesVo != null)
                            SetEditViewControls(associatesVo);
                        head.InnerText = "View Associate";
                        ddlTitleList.Enabled = false;
                        ddlBranch.Enabled = false;
                        ddlRM.Enabled = false;
                        lnkBtnEdit.Visible = true;
                        lnlBack.Visible = true;
                        lnkContactDetails.Visible = true;
                        lnkCrossPondingAddress.Visible = true;
                        lnkOtherInformation.Visible = true;
                        lnkBankDetails.Visible = true;
                        lnkNominee.Visible = true;
                        lnkCategory.Visible = true;
                        lnkBusinessDetails.Visible = true;
                        radTABChildCodes.Visible = true;
                        BindChildCodeLabel(associatesVo.AAC_AdviserAgentId);
                        associateId = associatesVo.AdviserAssociateId;
                        if (!string.IsNullOrEmpty(associatesVo.WelcomeNotePath) && advisorVo.advisorId == Convert.ToInt32(ConfigurationSettings.AppSettings["ONLINE_ADVISER"]))
                            lbtnPreviewSend.Visible = true;


                    }
                    else if (Request.QueryString["action"].Trim() == "Edit")
                    //if (viewAction == "Edit" || viewAction == "EditFromRequestPage")
                    {
                        associatesVo = (AssociatesVO)Session["associatesVo"];
                        if (associatesVo != null)
                            SetEditViewControls(associatesVo);
                        head.InnerText = "Edit Associate";
                        associateId = associatesVo.AdviserAssociateId;
                        ddlTitleList.Enabled = false;
                        ddlBranch.Enabled = true;
                        ddlRM.Enabled = false;
                        lnkBtnEdit.Visible = false;
                        lnlBack.Visible = true;
                        //btnSubmit.Visible = true;
                        btnPreviewSend.Visible = true;
                        radTABChildCodes.Visible = true;
                        lbkbtnAddChildCodes.Enabled = true;
                        if (!string.IsNullOrEmpty(associatesVo.WelcomeNotePath) && advisorVo.advisorId == Convert.ToInt32(ConfigurationSettings.AppSettings["ONLINE_ADVISER"]))
                            lbtnPreviewSend.Visible = true;
                        btnContactDetailsUpdate.Visible = true;
                        btnOtherInformationUpdate.Visible = true;
                        btnbtnCrossPondenceUpdate.Visible = true;
                        btnBankDetailsUpdate.Visible = true;
                        btnBusinessDetailsUpdate.Visible = true;
                        btnNomineeUpdate.Visible = true;
                        btnCategoryUpdate.Visible = true;
                        btnAssociateUpdate.Visible = true;
                        BindChildCodeLabel(associatesVo.AAC_AdviserAgentId);
                        BindRegistration();
                    }
                }
            }
            if (userVo.UserType != "Advisor") { lnkBtnEdit.Visible = false; }
        }
        private void BindChildCodeLabel(int PagentId)
        {
            lblChildCodeListView.Text = "";
            DataTable dtChildCodeList;
            dtChildCodeList = associatesBo.GetAgentChildCodeList(PagentId);
            //dtChildCodeList = dsChildCodeList.Tables[0];
            if (dtChildCodeList.Rows.Count > 0)
            {
                int times = 1;
                foreach (DataRow row in dtChildCodeList.Rows)
                {
                    if (times == dtChildCodeList.Rows.Count)
                    {
                        lblChildCodeListView.Text = lblChildCodeListView.Text + row["AAC_AgentCode"].ToString();
                    }
                    else
                    {
                        lblChildCodeListView.Text = lblChildCodeListView.Text + row["AAC_AgentCode"].ToString() + ",";
                    }
                    times++;
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


        private void SetEnableDisable(int flag)
        {
            ddlTitleList.Enabled = false;
            ddlRM.Enabled = false;
            if (flag == 0)
            {

                // txtBranch.Enabled = false;
                // txtRM.Enabled = false;
                txtAMFINo.Enabled = false;
                txtStartDate.Enabled = false;
                txtEndDate.Enabled = false;
                txtAssociateExpDate.Enabled = false;
                //txtResPhoneNoStd.Enabled = false;
                txtResPhoneNo.Enabled = false;
                //txtResFaxStd.Enabled = false;
                txtResFax.Enabled = false;
                //txtOfcPhoneNoStd.Enabled = false;
                txtOfcPhoneNo.Enabled = false;
                //txtOfcFaxStd.Enabled = false;
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
                txtCorState.Enabled = false;
                txtPermState.Enabled = false;
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
                rdpUpdateDate.Enabled = false;
                txtMicr.Enabled = false;
                txtIfsc.Enabled = false;
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


                chkModules.Enabled = false;
                txtEUIN.Enabled = false;
                ddlAssociateSubType.Enabled = false;
                rbtnIndividual.Enabled = false;
                rbtnNonIndividual.Enabled = false;
                txtAdviserAgentCode.Enabled = false;
                txtAssociateName.Enabled = false;
                chkbldepart.Enabled = false;
                chkAddressChk.Enabled = false;
                txtBankBranchName.Enabled = false;
                txtPan.Enabled = false;
                chkIsActive.Enabled = false;
                chkIsDummy.Enabled = false;
                chkKYD.Enabled = false;
                chkFormB.Enabled = false;
                txtRemarks.Enabled = false;
                txtBankEmail.Enabled = false;
                txtBankMobile.Enabled = false;
                rdpUpdateDate.Enabled = false;
            }
            else
            {

                rdpUpdateDate.Enabled = true;
                txtAMFINo.Enabled = true;
                txtStartDate.Enabled = true;
                txtEndDate.Enabled = true;
                txtAssociateExpDate.Enabled = true;
                //txtResPhoneNoStd.Enabled = true;
                txtResPhoneNo.Enabled = true;
                //txtResFaxStd.Enabled = true;
                txtResFax.Enabled = true;
                //txtOfcPhoneNoStd.Enabled = true;
                txtOfcPhoneNo.Enabled = true;
                //txtOfcFaxStd.Enabled = true;
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
                txtCorState.Enabled = true;
                txtPermState.Enabled = true;
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
                //ddlCategory.Enabled = true;
                //txtRegNo.Enabled = true;
                //txtRegExpDate.Enabled = true;


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


                chkModules.Enabled = true;
                txtEUIN.Enabled = true;
                ddlAssociateSubType.Enabled = true;
                rbtnIndividual.Enabled = true;
                rbtnNonIndividual.Enabled = true;
                txtAdviserAgentCode.Enabled = true;
                txtAssociateName.Enabled = true;
                chkbldepart.Enabled = true;
                chkAddressChk.Enabled = true;
                txtBankBranchName.Enabled = true;
                txtPan.Enabled = true;
                chkIsActive.Enabled = true;
                chkIsDummy.Enabled = true;
                chkKYD.Enabled = true;
                chkFormB.Enabled = true;
                txtRemarks.Enabled = true;
                txtBankEmail.Enabled = true;
                txtBankMobile.Enabled = true;
                rdpUpdateDate.Enabled = true;
            }

        }

        private void SetEditViewControls(AssociatesVO associatesVo)
        {
            BindHierarchyTitleDropList();
            BindStaffBranchDropList(associatesVo.RMId);
            BindStaffDropList(associatesVo.adviserhirerchi);
            BinddepartDropList();
            if (associatesVo.Departmrntid != 0)
            {
                BindAdviserrole(associatesVo.Departmrntid);
                string[] RoleIds = associatesVo.Roleid.Split(',');
                for (int i = 0; i < RoleIds.Length; i++)
                {
                    foreach (RadListBoxItem li in chkbldepart.Items)
                    {
                        if (RoleIds[i] == li.Value.ToString())
                        {
                            li.Checked = true;
                        }
                    }
                }
            }
            if (associatesVo.IsActive == 1)
                chkIsActive.Checked = true;
            else
                chkIsActive.Checked = false;
            chkFormB.Checked = associatesVo.FormBRecvd;
            chkKYD.Checked = associatesVo.KYDStatus;
            if (associatesVo.IsDummy == 1)
                chkIsDummy.Checked = true;
            else
                chkIsDummy.Checked = false;
            chkKYD.Checked = associatesVo.KYDStatus;
            chkFormB.Checked = associatesVo.FormBRecvd;
            if (associatesVo.ARNDate != null && associatesVo.ARNDate != DateTime.MinValue)
                rdpARNDate.SelectedDate = associatesVo.ARNDate;

            if (associatesVo.BMName != null)
                ddlBranch.SelectedValue = associatesVo.BranchId.ToString();
            if (associatesVo.Departmrntid != 0)
            {
                ddlDepart.SelectedValue = associatesVo.Departmrntid.ToString();
                PnlDepartRole.Visible = true;
            }
            if (associatesVo.RMNAme != null)
                ddlRM.SelectedValue = associatesVo.RMId.ToString();
            if (associatesVo.adviserhirerchi != 0)
                ddlTitleList.SelectedValue = associatesVo.adviserhirerchi.ToString();
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
            //if (associatesVo.ResPhoneNo != null)
            //    txtResPhoneNoStd.Text = associatesVo.ResPhoneNo.ToString();
            if (associatesVo.ResPhoneNo != null)
                txtResPhoneNo.Text = associatesVo.ResPhoneNo.ToString();
            //if (associatesVo.ResFaxStd != null)
            //    txtResFaxStd.Text = associatesVo.ResFaxStd.ToString();
            if (associatesVo.ResFaxNumber != null)
                txtResFax.Text = associatesVo.ResFaxNumber.ToString();
            //if (associatesVo.OfcSTDCode != null)
            //    txtOfcPhoneNoStd.Text = associatesVo.OfcSTDCode.ToString();
            if (associatesVo.OfficePhoneNo != null)
                txtOfcPhoneNo.Text = associatesVo.OfficePhoneNo.ToString();
            //if (associatesVo.OfcFaxSTD != null)
            //    txtOfcFaxStd.Text = associatesVo.OfcFaxSTD.ToString();
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
            {
                ddlCorState.SelectedValue = associatesVo.CorrAdrState;
                txtCorState.Text = associatesVo.CorrAdrState;
            }
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
            if (associatesVo.PerAdrCity != null)
                txtPermAdrCity.Text = associatesVo.PerAdrCity;
            if (associatesVo.PerAdrPinCode != null)
                txtPermAdrPinCode.Text = associatesVo.PerAdrPinCode.ToString();
            //BindState();

            if (!string.IsNullOrEmpty(associatesVo.PerAdrState))
            {
                ddlPermAdrState.SelectedValue = associatesVo.PerAdrState;
                txtPermState.Text = associatesVo.PerAdrState;
            }
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
            txtRemarks.Text = associatesVo.Remarks;
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
            //if (!string.isnullorempty(associatesvo.branchadrcity))
            //    ddlbankadrcity.selectedvalue = associatesvo.branchadrcity;
            if (!string.IsNullOrEmpty(associatesVo.BranchAdrState))
                ddlBankAdrState.Text = associatesVo.BranchAdrState;

            if (associatesVo.MICR != null)
                txtMicr.Text = associatesVo.MICR.ToString();
            if (associatesVo.IFSC != null)
                txtIfsc.Text = associatesVo.IFSC;

            //if (associatesVo.ExpiryDate != DateTime.MinValue)
            //    txtRegExpDate.SelectedDate = associatesVo.ExpiryDate;
            txtBankMobile.Text = associatesVo.BankMobile.ToString();
            txtBankEmail.Text = associatesVo.BankEmail;
            if (associatesVo.BankUpdatedDate != DateTime.MinValue)
                rdpUpdateDate.SelectedDate = associatesVo.BankUpdatedDate;

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
            if (associatesVo.NomineeDOB != DateTime.MinValue)
                rdpNomDOB.SelectedDate = associatesVo.NomineeDOB;
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

                }


            }



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
            //txtBranch.Text = associatesVo.BMName;
            //txtRM.Text = associatesVo.RMNAme;
            txtAssociateName.Text = associatesVo.ContactPersonName;
            txtEmail.Text = associatesVo.Email;

        }

        private void Updatedepartment()
        {
            bool result = false;
            int departmentid = 0;
            string roleIds = string.Empty;
            int userid = associatesVo.UserId;
            if (ddlDepart.SelectedValue != "0")
                departmentid = int.Parse(ddlDepart.SelectedValue);
            foreach (RadListBoxItem items in chkbldepart.Items)
            {
                if (items.Checked == true)
                    roleIds = roleIds + items.Value.ToString() + ",";
            }
            if (!string.IsNullOrEmpty(roleIds))
            {
                associatesVo.Roleid = roleIds.Remove(roleIds.Length - 1);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Select Privilege Roles!!');", true);
                return;
            }
            roleIds = roleIds.Remove(roleIds.Length - 1);
            result = associatesBo.UpdateUserrole(userid, roleIds);
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
        protected void lnkBtnChildCodes_Click(object sender, EventArgs e)
        {
            int associationId = 0;
            string AAC_AgentCode = "";
            string Flag = "";
            if (associatesVo.AdviserAssociateId != 0)
            {
                associationId = associatesVo.AdviserAssociateId;
                AAC_AgentCode = associatesVo.AAC_AgentCode;
                Flag = "notnull";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddBranchRMAgentAssociation','?AssociationId=" + associationId + "&AgentCode=" + AAC_AgentCode + "&Flag=" + Flag + "');", true);
            }
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            controlEnable(1);
            //if (Session["associatesVo"] != null)
            //{
            //    associatesVo = (AssociatesVO)Session["associatesVo"];
            //    if (associatesVo != null)
            //        SetEnableDisable(1);
            //    lnkBtnEdit.Visible = false;
            //    lbkbtnAddChildCodes.Enabled = true;
            //    BindChildCodeLabel(associatesVo.AAC_AdviserAgentId);
            //}
            //else
            //{
            //    btnSubmit.Visible = true;
            //}
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
                txtPermState.Text = txtCorState.Text;
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
        protected void UpdateAssociate()
        {
            int associateid = 0, agentcode = 0, userId = 0;

            if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"].Trim() == "Edit" || Request.QueryString["action"].Trim() == "View")
                {
                    agentcode = associatesVo.AAC_AdviserAgentId;
                    associateid = associatesVo.AdviserAssociateId;
                    userId = associatesVo.UserId;
                }
            }
            else
            {
                agentcode = int.Parse(Session["AdviserAgentId"].ToString());
                associateid = int.Parse(Session["AdviserAssociateIds"].ToString());
                userId = int.Parse(Session["UserIds"].ToString());
            }
            associatesVo.AAC_AgentCode = txtAdviserAgentCode.Text.ToString();
            associatesVo.ContactPersonName = txtAssociateName.Text;
            if (!string.IsNullOrEmpty(ddlBranch.SelectedValue))
                associatesVo.BranchId = Convert.ToInt32(ddlBranch.SelectedValue);
            associatesVo.RMId = Convert.ToInt32(ddlRM.SelectedValue);
            associatesVo.PanNo = txtPan.Text;
            associatesBo.UpdateAssociate(associatesVo, userId, associateid, agentcode);

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string roleIds = string.Empty;
            UserVo associateUserVo = new UserVo();
            Random id = new Random();
            string password = id.Next(10000, 99999).ToString();

            associateUserVo.Password = password;
            associateUserVo.LoginId = txtAdviserAgentCode.Text.ToString();
            associateUserVo.FirstName = txtAssociateName.Text.ToString();
            associateUserVo.Email = txtEmail.Text;
            associateUserVo.UserType = "Associates";
            associatesVo.AAC_AgentCode = txtAdviserAgentCode.Text;
            associatesVo.ContactPersonName = txtAssociateName.Text;
            if (!string.IsNullOrEmpty(ddlBranch.SelectedValue))
                associatesVo.BranchId = Convert.ToInt32(ddlBranch.SelectedValue);
            if (!string.IsNullOrEmpty(ddlBranch.SelectedValue))
                associatesVo.BMName = ddlBranch.SelectedItem.Text;
            associatesVo.RMId = Convert.ToInt32(ddlRM.SelectedValue);
            associatesVo.RMNAme = ddlRM.SelectedItem.Text;
            associatesVo.UserRoleId = 1009;
            associatesVo.PanNo = txtPan.Text;
            if (!string.IsNullOrEmpty(txtMobile1.Text))
                associatesVo.Mobile = long.Parse(txtMobile1.Text);
            else
                associatesVo.Mobile = 0;
            associatesVo.RequestDate = DateTime.Now;
            associatesVo.AAC_UserType = "Associates";
            Session["TempAssociatesVo"] = associatesVo;
            Session["AssociateUserVo"] = associateUserVo;

            if (chkIsActive.Checked)
            {
                associatesVo.IsActive = 1;
            }
            else
            {
                associatesVo.IsActive = 0;
            }

            if (chkIsDummy.Checked)
            {
                associatesVo.IsDummy = 1;
            }
            else
            {
                associatesVo.IsDummy = 0;
            }

            associatesVo.AssociationExpairyDate = Convert.ToDateTime(txtAssociateExpDate.SelectedDate);
            if (txtAMFINo.Text != null)
                associatesVo.AMFIregistrationNo = txtAMFINo.Text;
            else
                associatesVo.AMFIregistrationNo = "";
            if (!string.IsNullOrEmpty(txtEUIN.Text))
                associatesVo.EUIN = txtEUIN.Text;
            if (rbtnIndividual.Checked == true)
                associatesVo.AssociateType = "IND";
            if (rbtnNonIndividual.Checked == true)
                associatesVo.AssociateType = "NIND";
            if (ddlAssociateSubType.SelectedIndex != 0)
                associatesVo.AssociateSubType = ddlAssociateSubType.SelectedValue;
            associatesVo.KYDStatus = chkKYD.Checked;
            associatesVo.FormBRecvd = chkFormB.Checked;
            associatesVo.ARNDate = Convert.ToDateTime(rdpARNDate.SelectedDate);
            //if (txtStartDate.SelectedDate == null)
            //    associatesVo.StartDate = DateTime.MinValue;
            //else
            associatesVo.StartDate = Convert.ToDateTime(txtStartDate.SelectedDate);
            //if (txtEndDate.SelectedDate == null)
            //    associatesVo.EndDate = DateTime.MinValue;
            //else
            associatesVo.EndDate = Convert.ToDateTime(txtEndDate.SelectedDate);
            foreach (RadListBoxItem items in chkbldepart.Items)
            {
                if (items.Checked == true)
                    roleIds = roleIds + items.Value.ToString() + ",";
            }
            if (!string.IsNullOrEmpty(roleIds))
            {
                associatesVo.Roleid = roleIds.Remove(roleIds.Length - 1);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Select Privilege Roles!!');", true);
                return;
            }

            if (!Validation(txtAdviserAgentCode.Text))
            {
                return;
            }
            if (panValidation(txtPan.Text, associatesVo.AdviserAssociateId))
            {
                lblPanlength.Visible = false;
                lblPanDuplicate.Visible = true;
                return;
            }
            else
            {
                lblPanlength.Visible = false;
                lblPanDuplicate.Visible = false;
            }
            if (txtPan.Text.Length != 10)
            {
                lblPanDuplicate.Visible = false;
                lblPanlength.Visible = true;
                return;
            }
            else
            {
                lblPanDuplicate.Visible = false;
                lblPanlength.Visible = false;
            }
            associatesIds = associatesBo.CreateCompleteAssociates(associateUserVo, associatesVo, userVo.UserId);
            associatesVo.UserId = associatesIds[0];
            associatesVo.AdviserAssociateId = associatesIds[1];
            associateId = associatesVo.AdviserAssociateId;
            Session["AdviserAssociateIds"] = associatesIds[1];
            Session["UserIds"] = associatesIds[0];
            Session["AdviserAgentId"] = associatesIds[2];
            //------------------------ To create User role Association-----------------------
            userBo.CreateRoleAssociation(associatesVo.UserId, 1009);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Associates Added successfully!!');", true);
            controlEnable(0);
            //BindRegistration();
            btnBusinessDetails.Visible = true;
            btnCategory.Visible = true;
            btnNominee.Visible = true;
            btnBankDetails.Visible = true;
            OtherInformation.Visible = true;
            btnCrossPondence.Visible = true;
            btnContactDetails.Visible = true;
            if (advisorVo.advisorId == Convert.ToInt32(ConfigurationSettings.AppSettings["ONLINE_ADVISER"]))
            {
                btnPreviewSend.Visible = true;
            }
            //btnPreviewSend.Visible = false;
            //btnPreviewSend.PostBackUrl = "~/Reports/Display.aspx?&welcomeNote=1&associateId=" + associateId.ToString();
            //btnPreviewSend.OnClientClick = "window.document.forms[0].target='_blank'; setTimeout(function(){window.document.forms[0].target='';}, 500);";
            Session["associatesVo"] = null;

        }
        private void ShowMessageMainAssociateupdate(string msg)
        {
            MainAssociateUpdt.Visible = true;
            MainAssociateUpdt.InnerText = msg;
        }
        protected void btnAssociateUpdate_OnClick(object sender, EventArgs e)
        {
            int associateid = 0, agentcode = 0, userId = 0;
            if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"].Trim() == "Edit" || Request.QueryString["action"].Trim() == "View")
                {
                    agentcode = associatesVo.AAC_AdviserAgentId;
                    associateid = associatesVo.AdviserAssociateId;
                    userId = associatesVo.UserId;
                }
            }
            else
            {
                agentcode = int.Parse(Session["AdviserAgentId"].ToString());
                associateid = int.Parse(Session["AdviserAssociateIds"].ToString());
                userId = int.Parse(Session["UserIds"].ToString());
            }

            string PANNo = associatesBo.GetPANNo(associateid);
            string Agentcode1 = associatesBo.GetAgentCode(agentcode, 0);
            if (Agentcode1 != txtAdviserAgentCode.Text)
            {
                if (!Validation(txtAdviserAgentCode.Text))
                {
                    return;
                }
            }
            if (PANNo != txtPan.Text)
            {
                if (panValidation(txtPan.Text, associatesVo.AdviserAssociateId))
                {
                    lblPanlength.Visible = false;
                    lblPanDuplicate.Visible = true;
                    return;
                }
                if (txtPan.Text.Length != 10)
                {
                    lblPanDuplicate.Visible = false;
                    lblPanlength.Visible = true;
                    return;
                }
            }
            Updatedepartment();
            associatesVo.AAC_AgentCode = txtAdviserAgentCode.Text;
            associatesVo.ContactPersonName = txtAssociateName.Text;
            associatesVo.PanNo = txtPan.Text;
            if (rdpARNDate.SelectedDate != null)
                associatesVo.ARNDate = Convert.ToDateTime(rdpARNDate.SelectedDate);
            if (!string.IsNullOrEmpty(txtMobile1.Text))
                associatesVo.Mobile = long.Parse(txtMobile1.Text);
            else
                associatesVo.Mobile = 0;
            associatesVo.RequestDate = DateTime.Now;
            if (chkIsActive.Checked)
            {
                associatesVo.IsActive = 1;
            }
            else
            {
                associatesVo.IsActive = 0;
            }

            if (chkIsDummy.Checked)
            {
                associatesVo.IsDummy = 1;
            }
            else
            {
                associatesVo.IsDummy = 0;
            }

            if (txtAssociateExpDate.SelectedDate == null)
                associatesVo.AssociationExpairyDate = DateTime.MinValue;
            else
                associatesVo.AssociationExpairyDate = Convert.ToDateTime(txtAssociateExpDate.SelectedDate);
            if (txtAMFINo.Text != null)
                associatesVo.AMFIregistrationNo = txtAMFINo.Text;
            else
                associatesVo.AMFIregistrationNo = "";
            if (!string.IsNullOrEmpty(txtEUIN.Text))
                associatesVo.EUIN = txtEUIN.Text;
            if (rbtnIndividual.Checked == true)
                associatesVo.AssociateType = "IND";
            if (rbtnNonIndividual.Checked == true)
                associatesVo.AssociateType = "NIND";
            if (ddlAssociateSubType.SelectedIndex != 0)
                associatesVo.AssociateSubType = ddlAssociateSubType.SelectedValue;
            if (txtStartDate.SelectedDate == null)
                associatesVo.StartDate = DateTime.MinValue;
            else
                associatesVo.StartDate = Convert.ToDateTime(txtStartDate.SelectedDate);
            if (txtEndDate.SelectedDate == null)
                associatesVo.EndDate = DateTime.MinValue;
            else
                associatesVo.EndDate = Convert.ToDateTime(txtEndDate.SelectedDate);
            if (!string.IsNullOrEmpty(ddlBranch.SelectedValue))
                associatesVo.BranchId = Convert.ToInt32(ddlBranch.SelectedValue);
            associatesVo.KYDStatus = chkKYD.Checked;
            associatesVo.FormBRecvd = chkFormB.Checked;
            associatesBo.UpdateAssociateDetails(associatesVo, userId, associateid, agentcode);
            controlEnable(0);
            btnAssociateUpdate.Visible = false;
            ShowMessageMainAssociateupdate("Associate details updated successfully.");
        }
        protected void UpdateContact(string value)
        {
            bool result = false;
            int associateid = 0, agentcode = 0, userId = 0;
            if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"].Trim() == "Edit" || Request.QueryString["action"].Trim() == "View")
                {
                    agentcode = associatesVo.AAC_AdviserAgentId;
                    associateid = associatesVo.AdviserAssociateId;
                    userId = associatesVo.UserId;
                }
            }
            else
            {
                agentcode = int.Parse(Session["AdviserAgentId"].ToString());
                associateid = int.Parse(Session["AdviserAssociateIds"].ToString());
                userId = int.Parse(Session["UserIds"].ToString());
            }

            //if (!string.IsNullOrEmpty(txtResPhoneNoStd.Text))
            //    associatesVo.ResSTDCode = int.Parse(txtResPhoneNoStd.Text);
            //else
            //    associatesVo.ResSTDCode = 0;
            if (!string.IsNullOrEmpty(txtResPhoneNo.Text))
                associatesVo.ResPhoneNo = Convert.ToInt64(txtResPhoneNo.Text);
            else
                associatesVo.ResPhoneNo = 0;
            //if (!string.IsNullOrEmpty(txtOfcPhoneNoStd.Text))
            //    associatesVo.OfcSTDCode = int.Parse(txtOfcPhoneNoStd.Text);
            //else
            //    associatesVo.OfcSTDCode = 0;
            if (!string.IsNullOrEmpty(txtOfcPhoneNo.Text))
                associatesVo.OfficePhoneNo = Convert.ToInt64(txtOfcPhoneNo.Text);
            else
                associatesVo.OfficePhoneNo = 0;
            //if (!string.IsNullOrEmpty(txtResFaxStd.Text))
            //    associatesVo.ResFaxStd = int.Parse(txtResFaxStd.Text);
            //else
            //    associatesVo.ResFaxStd = 0;

            if (!string.IsNullOrEmpty(txtResFax.Text))
                associatesVo.ResFaxNumber = Convert.ToInt64(txtResFax.Text);
            else
                associatesVo.ResFaxNumber = 0;
            //if (!string.IsNullOrEmpty(txtOfcFaxStd.Text))
            //    associatesVo.OfcFaxSTD = int.Parse(txtOfcFaxStd.Text);
            //else
            //    associatesVo.OfcFaxSTD = 0;
            if (!string.IsNullOrEmpty(txtOfcFax.Text))
                associatesVo.OfcFaxNumber = Convert.ToInt64(txtOfcFax.Text);
            else
                associatesVo.OfcFaxNumber = 0;
            if (!string.IsNullOrEmpty(txtMobile1.Text))
                associatesVo.Mobile = long.Parse(txtMobile1.Text);
            else
                associatesVo.Mobile = 0;
            if (!string.IsNullOrEmpty(txtEmail.Text))
                associatesVo.Email = txtEmail.Text;
            else
                associatesVo.Email = "";

            // ---------------------------------------CORRESPONDING ADDRESS-------------------------------------------

            if (!string.IsNullOrEmpty(txtCorLine1.Text))
                associatesVo.CorrAdrLine1 = txtCorLine1.Text;
            else
                associatesVo.CorrAdrLine1 = "";
            if (!string.IsNullOrEmpty(txtCorLine2.Text))
                associatesVo.CorrAdrLine2 = txtCorLine2.Text;
            else
                associatesVo.CorrAdrLine2 = "";
            if (!string.IsNullOrEmpty(txtCorLine3.Text))
                associatesVo.CorrAdrLine3 = txtCorLine3.Text;
            else
                associatesVo.CorrAdrLine3 = "";
            if (!string.IsNullOrEmpty(txtCorCity.Text))
                associatesVo.CorrAdrCity = txtCorCity.Text;
            else
                associatesVo.CorrAdrCity = "";
            if (!string.IsNullOrEmpty(txtCorPin.Text))
                associatesVo.CorrAdrPinCode = int.Parse(txtCorPin.Text);
            else
                associatesVo.CorrAdrPinCode = 0;
            //if (ddlCorState.SelectedIndex != 0)
            //    associatesVo.CorrAdrState = ddlCorState.SelectedValue;
            //else
            //    associatesVo.CorrAdrState = "";
            associatesVo.CorrAdrState = txtCorState.Text;
            if (!string.IsNullOrEmpty(txtCorCountry.Text))
                associatesVo.CorrAdrCountry = txtCorCountry.Text;
            else
                associatesVo.CorrAdrCountry = "";
            //---------------------------------------PERMANENT ADDRESS-------------------------------------------
            if (!string.IsNullOrEmpty(txtPermAdrLine1.Text))
                associatesVo.PerAdrLine1 = txtPermAdrLine1.Text;
            else
                associatesVo.PerAdrLine1 = "";
            if (!string.IsNullOrEmpty(txtPermAdrLine2.Text))
                associatesVo.PerAdrLine2 = txtPermAdrLine2.Text;
            else
                associatesVo.PerAdrLine2 = "";
            if (!string.IsNullOrEmpty(txtPermAdrLine3.Text))
                associatesVo.PerAdrLine3 = txtPermAdrLine3.Text;
            else
                associatesVo.PerAdrLine3 = "";
            if (!string.IsNullOrEmpty(txtPermAdrCity.Text))
                associatesVo.PerAdrCity = txtCorCity.Text;
            else
                associatesVo.PerAdrCity = "";

            if (!string.IsNullOrEmpty(txtPermAdrPinCode.Text))
                associatesVo.PerAdrPinCode = int.Parse(txtPermAdrPinCode.Text);
            else
                associatesVo.PerAdrPinCode = 0;
            //if (ddlPermAdrState.SelectedIndex != 0)
            //    associatesVo.PerAdrState = ddlPermAdrState.SelectedValue;
            //else
            //    associatesVo.PerAdrState = "";
            associatesVo.PerAdrState = txtCorState.Text;
            if (!string.IsNullOrEmpty(txtPermAdrCountry.Text))
                associatesVo.PerAdrCountry = txtPermAdrCountry.Text;
            else
                associatesVo.PerAdrCountry = "";


            //---------------------------------------OTHER INFO---------------------------------------------
            if (ddlMaritalStatus.SelectedIndex != 0)
                associatesVo.MaritalStatusCode = ddlMaritalStatus.SelectedValue;
            else
                associatesVo.MaritalStatusCode = "0";
            if (ddlQualification.SelectedIndex != 0)
                associatesVo.QualificationCode = ddlQualification.SelectedValue;
            else
                associatesVo.QualificationCode = "";
            associatesVo.Gender = ddlGender.SelectedValue;
            if (txtDOB.SelectedDate != DateTime.MinValue)
                associatesVo.DOB = Convert.ToDateTime(txtDOB.SelectedDate);
            associatesVo.Remarks = txtRemarks.Text;
            //---------------------------------------BANK DETAILS-------------------------------------------

            if (ddlBankName.SelectedIndex != 0)
                associatesVo.BankCode = ddlBankName.SelectedValue;
            else
                associatesVo.BankCode = "";
            if (ddlAccountType.SelectedIndex != 0)
                associatesVo.BankAccountTypeCode = ddlAccountType.SelectedValue;
            else
                associatesVo.BankAccountTypeCode = "";
            if (!string.IsNullOrEmpty(txtAccountNumber.Text))
                associatesVo.AccountNum = txtAccountNumber.Text;
            else
                associatesVo.AccountNum = "";
            if (!string.IsNullOrEmpty(txtBankBranchName.Text))
                associatesVo.BranchName = txtBankBranchName.Text;
            else
                associatesVo.BranchName = "";
            if (!string.IsNullOrEmpty(txtBankAdrLine1.Text))
                associatesVo.BranchAdrLine1 = txtBankAdrLine1.Text;
            else
                associatesVo.BranchAdrLine1 = "";
            if (!string.IsNullOrEmpty(txtBankAdrLine2.Text))
                associatesVo.BranchAdrLine2 = txtBankAdrLine2.Text;
            else
                associatesVo.BranchAdrLine2 = "";
            if (!string.IsNullOrEmpty(txtBankAdrLine3.Text))
                associatesVo.BranchAdrLine3 = txtBankAdrLine3.Text;
            else
                associatesVo.BranchAdrLine3 = "";
            if (!string.IsNullOrEmpty(txtBankAdrCity.Text))
                associatesVo.BranchAdrCity = txtBankAdrCity.Text;
            else
                associatesVo.BranchAdrCity = "";
            if (ddlBankAdrState.SelectedIndex != 0)
                associatesVo.BranchAdrState = ddlBankAdrState.SelectedValue;
            else
                associatesVo.BranchAdrState = "";
            if (!string.IsNullOrEmpty(txtMicr.Text))
                associatesVo.MICR = txtMicr.Text;
            else
                associatesVo.MICR = "";
            if (!string.IsNullOrEmpty(txtIfsc.Text))
                associatesVo.IFSC = txtIfsc.Text;
            else
                associatesVo.IFSC = "";
            if (rdpUpdateDate.SelectedDate != DateTime.MinValue)
                associatesVo.BankUpdatedDate = Convert.ToDateTime(rdpUpdateDate.SelectedDate);
            associatesVo.BankEmail = txtBankEmail.Text;
            if (!String.IsNullOrEmpty(txtBankMobile.Text))
                associatesVo.BankMobile = Convert.ToInt64(txtBankMobile.Text);
            //---------------------------------------NOMINEE-------------------------------------------

            if (!string.IsNullOrEmpty(txtNomineeName.Text))
                associatesVo.NomineeName = txtNomineeName.Text;
            else
                associatesVo.NomineeName = "";
            if (ddlNomineeRel.SelectedIndex != 0)
                associatesVo.RelationshipCode = ddlNomineeRel.SelectedValue;
            else
                associatesVo.RelationshipCode = "";
            if (!string.IsNullOrEmpty(txtNomineeAdress.Text))
                associatesVo.NomineeAddres = txtNomineeAdress.Text;
            else
                associatesVo.NomineeAddres = "";
            if (!string.IsNullOrEmpty(txtNomineePhone.Text))
                associatesVo.NomineeTelNo = Convert.ToInt32(txtNomineePhone.Text);
            else
                associatesVo.NomineeTelNo = 0;
            if (!string.IsNullOrEmpty(txtGurdiannName.Text))
                associatesVo.GuardianName = txtGurdiannName.Text;
            else
                associatesVo.GuardianName = "";
            if (!string.IsNullOrEmpty(txtGuardianAdress.Text))
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
            if (rdpNomDOB.SelectedDate != DateTime.MinValue)
                associatesVo.NomineeDOB = Convert.ToDateTime(rdpNomDOB.SelectedDate);
            //---------------------------------------Category-------------------------------------------

            if (ddlAdviserCategory.SelectedIndex != 0)
                associatesVo.categoryId = int.Parse(ddlAdviserCategory.SelectedValue);
            else
                associatesVo.categoryId = 0;


            //---------------------------------------Business Details-----------------------------------

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
            result = associatesBo.UpdateAdviserAssociates(associatesVo, associateid, userId, value);

        }
        private void ShowMessage(string msg)
        {
            msgRecordStatus.Visible = true;
            msgRecordStatus.InnerText = msg;
        }
        protected void lnkContactDetails_OnClick(object sender, EventArgs e)
        {
            btnContactDetailsUpdate.Visible = true;
            msgRecordStatus.Visible = false;
            //txtResPhoneNoStd.Enabled = true;
            txtResPhoneNo.Enabled = true;
            //txtResFaxStd.Enabled = true;
            txtResFax.Enabled = true;
            //txtOfcPhoneNoStd.Enabled = true;
            txtOfcPhoneNo.Enabled = true;
            //txtOfcFaxStd.Enabled = true;
            txtOfcFax.Enabled = true;
            txtMobile1.Enabled = true;
            txtEmail.Enabled = true;
            lblMobMandatory.Visible = false;
            lblEmailMandatory.Visible = false;

        }
        protected void btnContactDetails_OnClick(object sender, EventArgs e)
        {
            int associateId = 0;
            if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"].Trim() == "Edit" || Request.QueryString["action"].Trim() == "View")
                {
                    associateId = associatesVo.AdviserAssociateId;
                }
            }
            else
            {
                associateId = int.Parse(Session["AdviserAssociateIds"].ToString());

            }
            if (!String.IsNullOrEmpty(txtMobile1.Text) && associatesBo.AssociateFieldValidation(txtMobile1.Text, "Mobile", advisorVo.advisorId, associateId))
            {
                lblMobMandatory.Visible = true;
                return;
            }
            else
            {
                lblMobMandatory.Visible = false;
            }
            if (!String.IsNullOrEmpty(txtEmail.Text) && associatesBo.AssociateFieldValidation(txtEmail.Text, "EMail", advisorVo.advisorId, associateId))
            {
                lblEmailMandatory.Visible = true;
                return;
            }
            else
            {
                lblEmailMandatory.Visible = false;
            }
            UpdateContact("CD");
            lnkContactDetails.Visible = true;
            btnContactDetails.Visible = false;
            ShowMessage("Contact Details Submited Successfully");
        }
        protected void btnContactDetailsUpdate_OnClick(object sender, EventArgs e)
        {
            int associateId = 0;
            if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"].Trim() == "Edit" || Request.QueryString["action"].Trim() == "View")
                {
                    associateId = associatesVo.AdviserAssociateId;
                }
            }
            else
            {
                associateId = int.Parse(Session["AdviserAssociateIds"].ToString());

            }
            if (!String.IsNullOrEmpty(txtMobile1.Text) && associatesBo.AssociateFieldValidation(txtMobile1.Text, "Mobile", advisorVo.advisorId, associateId))
            {
                lblMobMandatory.Visible = true;
                return;
            }
            else
            {
                lblMobMandatory.Visible = false;
            }
            if (!String.IsNullOrEmpty(txtEmail.Text) && associatesBo.AssociateFieldValidation(txtEmail.Text, "EMail", advisorVo.advisorId, associateId))
            {
                lblEmailMandatory.Visible = true;
                return;
            }
            else
            {
                lblEmailMandatory.Visible = false;
            }
            UpdateContact("CD");
            btnContactDetailsUpdate.Visible = false;
            ShowMessage("Contact Details Updated Successfully");
            lnkContactDetails.Visible = true;
        }
        private void ShowMessagecross(string msg)
        {
            tblCrosspondance.Visible = true;
            dvCrosspondance.InnerText = msg;
        }
        protected void lnkCrossPondingAddress_OnClick(object sender, EventArgs e)
        {
            btnbtnCrossPondenceUpdate.Visible = true;
            tblCrosspondance.Visible = false;
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
            txtCorState.Enabled = true;
            txtPermState.Enabled = true;
            txtPermAdrCountry.Enabled = true;
            chkAddressChk.Enabled = true;
        }
        protected void btnCrossPondence_OnClick(object sender, EventArgs e)
        {
            UpdateContact("CA");
            lnkCrossPondingAddress.Visible = true;
            btnCrossPondence.Visible = false;
            ShowMessagecross("correspondence Details Submited Successfully");
        }
        protected void btnbtnCrossPondenceUpdate_OnClick(object sender, EventArgs e)
        {
            UpdateContact("CA");
            btnbtnCrossPondenceUpdate.Visible = false;
            ShowMessagecross("correspondence Details Updated Successfully");
            lnkCrossPondingAddress.Visible = true;

        }
        private void ShowMessageOI(string msg)
        {
            tblOther.Visible = true;
            dvOther.InnerText = msg;
        }
        protected void lnkOtherInformation_OnClick(object sender, EventArgs e)
        {
            btnOtherInformationUpdate.Visible = true;
            tblOther.Visible = false;
            ddlMaritalStatus.Enabled = true;
            ddlQualification.Enabled = true;
            ddlGender.Enabled = true;
            txtDOB.Enabled = true;
            txtRemarks.Enabled = true;

        }
        protected void OtherInformation_OnClick(object sender, EventArgs e)
        {
            UpdateContact("OI");
            lnkOtherInformation.Visible = true;
            OtherInformation.Visible = false;
            ShowMessageOI("Other Information Submited Successfully");
        }
        protected void btnOtherInformationUpdate_OnClick(object sender, EventArgs e)
        {
            UpdateContact("OI");
            btnOtherInformationUpdate.Visible = false;
            ShowMessageOI("Other Information Updated Successfully");
            lnkOtherInformation.Visible = true;
        }
        private void ShowMessageBD(string msg)
        {
            tblBankDetails.Visible = true;
            dvBankDetails.InnerText = msg;
        }
        protected void lnkBankDetails_OnClick(object sender, EventArgs e)
        {
            btnBankDetailsUpdate.Visible = true;
            tblBankDetails.Visible = false;

            ddlBankName.Enabled = true;
            ddlAccountType.Enabled = true;
            txtAccountNumber.Enabled = true;
            txtBankAdrLine1.Enabled = true;
            txtBankAdrLine2.Enabled = true;
            txtBankAdrLine3.Enabled = true;
            txtBankAdrCity.Enabled = true;
            ddlBankAdrState.Enabled = true;
            txtBankAdrPinCode.Enabled = true;
            txtBankBranchName.Enabled = true;
            txtMicr.Enabled = true;
            txtIfsc.Enabled = true;
            chkAddressChk.Enabled = true;
            txtBankEmail.Enabled = true;
            txtBankMobile.Enabled = true;
            rdpUpdateDate.Enabled = true;
        }
        protected void btnBankDetails_OnClick(object sender, EventArgs e)
        {
            UpdateContact("BD");
            lnkBankDetails.Visible = true;
            btnBankDetails.Visible = false;
            ShowMessageBD("Bank Details Submited Successfully");
        }
        protected void btnBankDetailsUpdate_OnClick(object sender, EventArgs e)
        {
            UpdateContact("BD");
            btnBankDetailsUpdate.Visible = false;
            ShowMessageBD("Bank Details Updated Successfully");
            lnkBankDetails.Visible = true;
        }

        private void ShowMessageNominee(string msg)
        {
            tblNominee.Visible = true;
            dvNominee.InnerText = msg;
        }
        protected void lnkNominee_OnClick(object sender, EventArgs e)
        {
            btnNomineeUpdate.Visible = true;
            tblNominee.Visible = false;

            txtNomineeName.Enabled = true;
            ddlNomineeRel.Enabled = true;
            txtNomineeAdress.Enabled = true;
            txtNomineePhone.Enabled = true;
            txtGurdiannName.Enabled = true;
            ddlGuardianRel.Enabled = true;
            txtGuardianAdress.Enabled = true;
            txtGurdianPhone.Enabled = true;
            rdpNomDOB.Enabled = true;
        }
        protected void btnNominee_OnClick(object sender, EventArgs e)
        {
            UpdateContact("AN");
            lnkNominee.Visible = true;
            btnNominee.Visible = false;
            ShowMessageNominee("Nominee Details Submited Successfully");

        }
        protected void btnNomineeUpdate_OnClick(object sender, EventArgs e)
        {
            UpdateContact("AN");
            btnNomineeUpdate.Visible = false;
            ShowMessageNominee("Nominee Updated Successfully");
            lnkNominee.Visible = true;
        }
        protected void lnkCategory_OnClick(object sender, EventArgs e)
        {
            btnCategoryUpdate.Visible = true;
            tblCategory.Visible = false;
            ddlAdviserCategory.Enabled = true;
        }
        private void ShowMessageCategory(string msg)
        {
            tblCategory.Visible = true;
            dvCategory.InnerText = msg;
        }
        protected void btnCategory_OnClick(object sender, EventArgs e)
        {
            UpdateContact("CY");
            lnkCategory.Visible = true;
            btnCategory.Visible = false;
            ShowMessageCategory("Category Submited Successfully");

        }
        protected void btnCategoryUpdate_OnClick(object sender, EventArgs e)
        {
            UpdateContact("CY");
            btnCategoryUpdate.Visible = false;
            ShowMessageCategory("Category Updated Successfully");
            lnkCategory.Visible = true;
        }
        private void ShowMessageBusinessDetails(string msg)
        {
            tblBusinessDetails.Visible = true;
            dvBusinessDetails.InnerText = msg;
        }
        protected void lnkBusinessDetails_OnClick(object sender, EventArgs e)
        {
            btnBusinessDetailsUpdate.Visible = true;
            tblBusinessDetails.Visible = false;
            txtNoBranches.Enabled = true;
            txtNoofSales.Enabled = true;
            txtNoofSubBrokers.Enabled = true;
            txtNoofClients.Enabled = true;
        }
        protected void btnBusinessDetails_OnClick(object sender, EventArgs e)
        {
            UpdateContact("ABD");
            lnkBusinessDetails.Visible = true;
            btnBusinessDetails.Visible = false;
            ShowMessageBusinessDetails("Business Details Submited Successfully");
        }
        protected void btnBusinessDetailsUpdate_OnClick(object sender, EventArgs e)
        {
            UpdateContact("ABD");
            btnBusinessDetailsUpdate.Visible = false;
            ShowMessageBusinessDetails("Business Details Updated Successfully");
            lnkBusinessDetails.Visible = true;
        }

        protected void controlEnable(int value)
        {
            if (value == 0)
            {

                lnkBtnEdit.Visible = true;
                txtAdviserAgentCode.Enabled = false;
                txtAssociateName.Enabled = false;
                ddlBranch.Enabled = false;
                ddlRM.Enabled = false;
                txtPan.Enabled = false;
                chkIsActive.Enabled = false;
                chkIsDummy.Enabled = false;
                txtAssociateExpDate.Enabled = false;
                txtAMFINo.Enabled = false;
                txtEUIN.Enabled = false;
                rbtnIndividual.Enabled = false;
                rbtnNonIndividual.Enabled = false;
                ddlAssociateSubType.Enabled = false;
                txtStartDate.Enabled = false;
                txtEndDate.Enabled = false;
                chkbldepart.Enabled = false;
                BtnSave.Visible = false;
                ddlTitleList.Enabled = false;
                rdpARNDate.Enabled = false;
                chkFormB.Enabled = false;
                chkKYD.Enabled = false;
            }
            else
            {
                txtAdviserAgentCode.Enabled = true;
                txtAssociateName.Enabled = true;
                txtPan.Enabled = true;
                chkIsActive.Enabled = true;
                chkIsDummy.Enabled = true;
                txtAssociateExpDate.Enabled = true;
                txtAMFINo.Enabled = true;
                txtEUIN.Enabled = true;
                rbtnIndividual.Enabled = true;
                rbtnNonIndividual.Enabled = true;
                ddlAssociateSubType.Enabled = true;
                txtStartDate.Enabled = true;
                txtEndDate.Enabled = true;
                chkbldepart.Enabled = true;
                btnAssociateUpdate.Visible = true;
                lbkbtnAddChildCodes.Enabled = true;
                ddlBranch.Enabled = true;
                lnkBtnEdit.Visible = false;

            }
        }
        protected void BindRegistration()
        {
            try
            {
                int associateid = 0;
                if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
                {
                    if (Request.QueryString["action"].Trim() == "Edit" || Request.QueryString["action"].Trim() == "View")
                    {
                        associateid = associatesVo.AdviserAssociateId;

                    }
                }
                else
                {
                    if (Session["AdviserAssociateIds"] != null)
                        associateid = int.Parse(Session["AdviserAssociateIds"].ToString());
                }
                DataTable dtRegistration = associatesBo.GetAssetsRegistration(associateid);

                if (dtRegistration.Rows.Count > 0)
                {
                    if (Cache["Registration" + userVo.UserId] == null)
                    {
                        Cache.Insert("Registration" + userVo.UserId, dtRegistration);
                    }
                    else
                    {
                        Cache.Remove("Registration" + userVo.UserId);
                        Cache.Insert("Registration" + userVo.UserId, dtRegistration);
                    }
                    gvRegistration.DataSource = dtRegistration;
                    gvRegistration.DataBind();
                    gvRegistration.Visible = true;
                }
                else
                {
                    Cache.Remove("Registration" + userVo.UserId);
                    Cache.Insert("Registration" + userVo.UserId, dtRegistration);
                    gvRegistration.DataSource = dtRegistration;
                    gvRegistration.DataBind();
                    gvRegistration.Visible = true;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        protected void gvRegistration_OnItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlCategory = (DropDownList)gefi.FindControl("ddlCategory");
                BindAssetsgroup(ddlCategory);
            }
        }
        protected void gvRegistration_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtRegistration = new DataTable();
            dtRegistration = (DataTable)Cache["Registration" + userVo.UserId];
            if (dtRegistration != null)
            {
                gvRegistration.DataSource = dtRegistration;
            }

        }
        protected void gvRegistration_OnItemCommand(object source, GridCommandEventArgs e)
        {
            int associateid = 0;
            if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"].Trim() == "Edit" || Request.QueryString["action"].Trim() == "View")
                {
                    associateid = associatesVo.AdviserAssociateId;

                }
            }
            else
            {
                if (Session["AdviserAssociateIds"] != null)
                    associateid = int.Parse(Session["AdviserAssociateIds"].ToString());
            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                DropDownList ddlCategory = (DropDownList)e.Item.FindControl("ddlCategory");
                RadDatePicker txtRegExpDate = (RadDatePicker)e.Item.FindControl("txtRegExpDate");
                TextBox txtRegNumber = (TextBox)e.Item.FindControl("txtRegNumber");
                associatesBo.AssociateRegistration(associateid, Convert.ToDateTime(txtRegExpDate.SelectedDate), int.Parse(txtRegNumber.Text), ddlCategory.SelectedValue);
                BindRegistration();
            }

        }
        protected void BindAssetsgroup(DropDownList ddlCategory)
        {
            DataTable dt = associatesBo.AssetsGroup();
            ddlCategory.DataSource = dt;
            ddlCategory.DataValueField = dt.Columns["PAG_AssetGroupCode"].ToString();
            ddlCategory.DataTextField = dt.Columns["PAG_AssetGroupName"].ToString();
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        public bool panValidation(string Pan, int AdviserAssociateId)
        {

            bool result = false;
            try
            {
                if (associatesBo.CheckPanNumberDuplicatesForAssociates(Pan, AdviserAssociateId, advisorVo.advisorId))
                {
                    result = true;

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
                FunctionInfo.Add("Method", "AddAssociatesDetails.ascx:panValidation()");
                object[] objects = new object[3];
                objects[0] = Pan;
                objects[1] = AdviserAssociateId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }
        private bool Validation(string agentCode)
        {
            bool result = true;
            int adviserId = advisorVo.advisorId;
            try
            {
                if (agentCode != string.Empty)
                {
                    if (associatesBo.CodeduplicateCheck(adviserId, agentCode))
                    {

                        result = false;
                        //lblPanDuplicate.Visible = true;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('SubBroker Code already exists !!');", true);
                    }
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
                FunctionInfo.Add("Method", "AddAssociatesDetails.ascx.ascx:Validation()");
                object[] objects = new object[1];
                objects[0] = agentCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
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
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"].Trim() == "Edit" || Request.QueryString["action"].Trim() == "View")
                {
                    return;
                }
            }
            if (ddlBranch.SelectedIndex == 0) return;
            else
            {
                string sampleAssociateCode = associatesBo.GetSampleAssociateCode(advisorVo.advisorId, Convert.ToInt32(ddlBranch.SelectedValue),"BranchType");
                txtAdviserAgentCode.Text = sampleAssociateCode;
            }
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

        private void BinddepartDropList()
        {
            DataSet dsDepartmentlist = new DataSet();
            dsDepartmentlist = associatesBo.GetDepartment(advisorVo.advisorId);
            ddlDepart.DataSource = dsDepartmentlist.Tables[0];
            ddlDepart.DataTextField = dsDepartmentlist.Tables[0].Columns["AD_DepartmentName"].ToString();
            ddlDepart.DataValueField = dsDepartmentlist.Tables[0].Columns["AD_DepartmentId"].ToString();
            ddlDepart.DataBind();
            //ddlDepart.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }
        protected void ddlDepart_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void BindDepartmentRole()
        {
            PnlDepartRole.Visible = false;
            if (ddlDepart.SelectedValue.ToString() != "0")
            {
                BindAdviserrole(int.Parse(ddlDepart.SelectedValue));
                PnlDepartRole.Visible = true;
            }
        }
        private void BindAdviserrole(int departmentId)
        {

            DataTable dtBindAdvisor = new DataTable();
            dtBindAdvisor = advisorStaffBo.GetUserRoleDepartmentWise(departmentId, advisorVo.advisorId);
            chkbldepart.DataSource = dtBindAdvisor;
            chkbldepart.DataTextField = dtBindAdvisor.Columns["AR_Role"].ToString();
            chkbldepart.DataValueField = dtBindAdvisor.Columns["AR_RoleId"].ToString();
            chkbldepart.DataBind();

        }

        protected void btnPreviewSend_Click(object sender, EventArgs e)
        {
            int associateid = 0;
            if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"].Trim() == "Edit" || Request.QueryString["action"].Trim() == "View")
                {
                    associateid = associatesVo.AdviserAssociateId;
                }
            }
            else
            {
                associateid = int.Parse(Session["AdviserAssociateIds"].ToString());
            }

            if (!string.IsNullOrEmpty(associatesVo.WelcomeNotePath))
            {
                string targetPath = ConfigurationManager.AppSettings["Welcome_Note_PATH"].ToString();
                Response.Redirect(targetPath + associatesVo.WelcomeNotePath);
            }

        }
        protected void lbtnPreviewSend_Click(object sender, EventArgs e)
        {
            int associateid = 0;
            if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"].Trim() == "Edit" || Request.QueryString["action"].Trim() == "View")
                {
                    associateid = associatesVo.AdviserAssociateId;
                }
            }
            else
            {
                associateid = int.Parse(Session["AdviserAssociateIds"].ToString());
            }
            string redirectPath = ConfigurationManager.AppSettings["WEL_COME_LETER_QUERY_STRING"].ToString();
            Response.Redirect(redirectPath + associateid);
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"].Trim() == "Edit" || Request.QueryString["action"].Trim() == "View")
                {
                    return;
                }
            }
            if (ddlType.SelectedIndex == 0) return;
            else
            {
                string sampleAssociateCode = associatesBo.GetSampleAssociateCode(advisorVo.advisorId, Convert.ToInt32(ddlType.SelectedValue),"ProductType");
                txtAdviserAgentCode.Text = sampleAssociateCode;
            }
        }

        private void BindAssociatetype()
        {
            DataTable dtAssociatetype = new DataTable();
            dtAssociatetype = associatesBo.GetAssociatetype();
            ddlType.DataSource = dtAssociatetype;
            ddlType.DataValueField = dtAssociatetype.Columns["AMPL_ID"].ToString();
            ddlType.DataTextField = dtAssociatetype.Columns["AMPL_ProductName"].ToString();
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("Select", "Select"));
        }

        protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSource.SelectedValue.ToString() != "1")
            {

                ddlType.Visible = true;
                Label19.Visible = true;
            }
            else
            {
                ddlType.Visible = false;
                Label19.Visible = false;
            }

        }
    }
}