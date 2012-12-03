﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using VoUser;
using BoCommon;
using BoUploads;
using VoCustomerProfiling;
using BoCustomerProfiling;
using BoAdvisorProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using WealthERP.Base;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;
using BoCustomerProfiling;
using AjaxControlToolkit;


namespace WealthERP.Customer
{
    public partial class EditCustomerIndividualProfile : System.Web.UI.UserControl
    {
        UserVo userVo = null;
        RMVo rmVo = null;
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        AdvisorStaffBo adviserStaffBo = new AdvisorStaffBo();
        AdvisorVo advisorVo = new AdvisorVo();
        CustomerISAAccountsVo customerISAAccountsVo = new CustomerISAAccountsVo();
        string path;
        DataTable dtMaritalStatus = new DataTable();
        DataTable dtNationality = new DataTable();
        DataTable dtOccupation = new DataTable();
        DataTable dtQualification = new DataTable();
        DataTable dtState = new DataTable();
        DataTable dtCustomerSubType = new DataTable();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        DataSet dsCustomerAssociates = new DataSet();
        CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
        CustomerBankAccountVo customerBankAccountVo = new CustomerBankAccountVo();
        List<CustomerBankAccountVo> customerBankAccountList = null;
        string strExternalCodeToBeEdited;
        DataSet dsISADetails;
        int IsaAccountId;

        DataSet dsBankDetails;
        int bankId;

        DataSet dsGetSlab = new DataSet();
        int years;
        int customerId = 0;
        int associateId = 0;
        string relCode = string.Empty;
        string viewForm = string.Empty;
        int requestNo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            cvDepositDate1.ValueToCompare = DateTime.Now.ToShortDateString();
            txtLivingSince_CompareValidator.ValueToCompare = DateTime.Now.ToShortDateString();
            cvJobStartDate.ValueToCompare = DateTime.Now.ToShortDateString();
            //txtMarriageDate_CompareValidator.ValueToCompare = DateTime.Now.ToShortDateString();

            try
            {
                SessionBo.CheckSession();
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                advisorVo = (AdvisorVo)Session["advisorVo"];
                RMVo customerRMVo = new RMVo();
                rmVo = (RMVo)Session[SessionContents.RmVo];
                if (advisorVo.IsISASubscribed != true)
                {
                    RadTabStripCustomerProfile.Tabs[1].Visible = false;
                }

                if (Session["LinkAction"] != null && Session["LinkAction"].ToString().Trim() == "AddEditProfile")
                {
                    gvFamilyAssociate.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                    gvISAAccountList.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                    gvBankDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                    requestNo = customerAccountBo.GetRequestNo(customerVo.CustomerId);
                    hdnRequestId.Value = requestNo.ToString();
                }
                else
                {
                    if (Session["action"] != null)
                    {
                        viewForm = Session["action"].ToString();
                        SetControlstate(viewForm);
                    }
                }
                if (!IsPostBack)
                {
                    if (Request.QueryString["action"] != null)
                    {
                        viewForm = Request.QueryString["action"].ToString();
                        if (viewForm == "Edit")
                            SetControlstate("Edit");
                        else if (viewForm == "View")
                            SetControlstate("View");
                    }
                    
                    BindCustomerISAAccountGrid();
                    //trNewISAAccountSection.Visible = false;
                    lblPanDuplicate.Visible = false;
                   
                        //"None";
                    if (customerVo.SubType != "NRI")
                    {
                        txtRBIRefDate.Visible = false;
                        txtRBIRefNo.Visible = false;
                        lblRBIRefDate.Visible = false;
                        lblRBIRefNo.Visible = false;
                    }
                    if (customerVo.SubType == "MNR")
                    {
                        trGuardianName.Visible = true;
                    }
                    else
                    {
                        trGuardianName.Visible = false;
                    }

                    if ((customerVo.SubType == "RES") && (customerVo.Type == "IND"))
                    {
                        btnGetSlab.Visible = true;
                    }
                    else
                    {
                        btnGetSlab.Visible = false;
                    }

                    BindDropDowns();

                    //Bind Adviser Branch List

                    BindListBranch(customerVo.RmId, "rm");


                    if (customerVo.Type.ToUpper().ToString() == "IND")
                    {
                        rbtnIndividual.Checked = true;
                    }
                    else
                    {
                        rbtnNonIndividual.Checked = true;
                    }
                    if (customerVo.Gender.ToUpper().ToString() == "M")
                    {
                        rbtnMale.Checked = true;
                    }
                    else if (customerVo.Gender.ToUpper().ToString() == "F")
                    {
                        rbtnFemale.Checked = true;
                    }

                    ddlAdviserBranchList.SelectedValue = customerVo.BranchId.ToString();
                    customerRMVo = adviserStaffBo.GetAdvisorStaffDetails(customerVo.RmId);
                    if (customerRMVo.FirstName + " " + customerRMVo.MiddleName + " " + customerRMVo.LastName != null && (customerRMVo.FirstName + " " + customerRMVo.MiddleName + " " + customerRMVo.LastName).ToString() != "")
                        lblRM.Text = customerRMVo.FirstName + " " + customerRMVo.MiddleName + " " + customerRMVo.LastName;
                    else
                        lblRM.Text = "";
                    if (customerVo.MaritalStatus != null)
                        ddlMaritalStatus.SelectedValue = customerVo.MaritalStatus.ToString();
                    if (ddlMaritalStatus.SelectedValue == "MA")
                        txtMarriageDate.Enabled = true;
                    else
                        txtMarriageDate.Enabled = false;

                    if (customerVo.MarriageDate == DateTime.MinValue)
                        txtMarriageDate.SelectedDate = null;
                    else
                        txtMarriageDate.SelectedDate = customerVo.MarriageDate;
                    if (customerVo.Nationality != null)
                        ddlNationality.SelectedValue = customerVo.Nationality.ToString();
                    if (customerVo.Occupation != null)
                        ddlOccupation.SelectedValue = customerVo.Occupation.ToString();
                    if (customerVo.Qualification != null)
                        ddlQualification.SelectedValue = customerVo.Qualification.ToString();

                    if (customerVo.ProfilingDate == DateTime.MinValue)
                        txtProfilingDate.Text = "";
                    else
                        txtProfilingDate.Text = customerVo.ProfilingDate.ToShortDateString();

                    if (customerVo.Dob == DateTime.MinValue)
                        txtDate.SelectedDate = null;
                    else
                        txtDate.SelectedDate = customerVo.Dob;
                    if (!string.IsNullOrEmpty(customerVo.Salutation))
                        ddlSalutation.SelectedValue = customerVo.Salutation;
                    else
                        ddlSalutation.SelectedIndex = 0;
                    if (customerVo.DummyPAN == 1)
                    {
                        chkdummypan.Checked = true;
                    }
                    else
                    {
                        chkdummypan.Checked = false;
                    }
                    //if (customerVo.IsProspect == 1)
                    //{
                    //    chkprospect.Checked = true;
                    //}
                    //else
                    //{
                    //    chkprospect.Checked = false;
                    //}
                    if (customerVo.ViaSMS == 1)
                    {
                        chksms.Checked = true;
                    }
                    else
                    {
                        chksms.Checked = false;
                    }
                    if (customerVo.AlertViaEmail == 1)
                    {
                        chkmail.Checked = true;
                    }
                    else
                    {
                        chkmail.Checked = false;
                    }

                    txtGuardianFirstName.Text = customerVo.ContactFirstName;
                    txtGuardianLastName.Text = customerVo.ContactLastName;
                    txtGuardianMiddleName.Text = customerVo.ContactMiddleName;
                    txtFirstName.Text = customerVo.FirstName;
                    txtMiddleName.Text = customerVo.MiddleName;
                    txtLastName.Text = customerVo.LastName;
                    txtCustomerCode.Text = customerVo.CustCode;
                    txtPanNumber.Text = customerVo.PANNum;

                    txtCorrAdrLine1.Text = customerVo.Adr1Line1;
                    txtCorrAdrLine2.Text = customerVo.Adr1Line2;
                    txtCorrAdrLine3.Text = customerVo.Adr1Line3;
                    txtCorrAdrPinCode.Text = customerVo.Adr1PinCode.ToString();
                    txtCorrAdrCity.Text = customerVo.Adr1City;
                    ddlCorrAdrState.SelectedValue = customerVo.Adr1State;
                    txtCorrAdrCountry.Text = customerVo.Adr1Country;
                    txtPermAdrLine1.Text = customerVo.Adr2Line1;
                    txtPermAdrLine2.Text = customerVo.Adr2Line2;

                    txtPermAdrLine3.Text = customerVo.Adr2Line3;
                    txtPermAdrPinCode.Text = customerVo.Adr2PinCode.ToString();
                    txtPermAdrCity.Text = customerVo.Adr2City;
                    ddlPermAdrState.SelectedValue = customerVo.Adr2State;
                    txtPermAdrCountry.Text = customerVo.Adr2Country;
                    txtOfcCompanyName.Text = customerVo.CompanyName;
                    txtOfcAdrLine1.Text = customerVo.OfcAdrLine1;
                    txtOfcAdrLine2.Text = customerVo.OfcAdrLine2;
                    txtOfcAdrLine3.Text = customerVo.OfcAdrLine3;
                    txtOfcAdrPinCode.Text = customerVo.OfcAdrPinCode.ToString();
                    txtOfcAdrCity.Text = customerVo.OfcAdrCity;
                    ddlOfcAdrState.SelectedValue = customerVo.OfcAdrState;
                    txtOfcAdrCountry.Text = customerVo.OfcAdrCountry;
                    txtResPhoneNoIsd.Text = customerVo.ResISDCode.ToString();
                    txtResPhoneNoStd.Text = customerVo.ResSTDCode.ToString();
                    txtResPhoneNo.Text = customerVo.ResPhoneNum.ToString();
                    txtOfcPhoneNoIsd.Text = customerVo.OfcISDCode.ToString();
                    txtOfcPhoneNoStd.Text = customerVo.OfcSTDCode.ToString();
                    txtOfcPhoneNo.Text = customerVo.OfcPhoneNum.ToString();
                    txtOfcFaxIsd.Text = customerVo.OfcISDFax.ToString();
                    txtOfcFaxStd.Text = customerVo.OfcSTDFax.ToString();
                    txtOfcFax.Text = customerVo.OfcFax.ToString();
                    txtResFax.Text = customerVo.Fax.ToString();
                    txtResFaxIsd.Text = customerVo.ISDFax.ToString();
                    txtResFaxStd.Text = customerVo.STDFax.ToString();
                    txtMobile1.Text = customerVo.Mobile1.ToString();
                    txtMobile2.Text = customerVo.Mobile2.ToString();
                    txtEmail.Text = customerVo.Email;
                    txtAltEmail.Text = customerVo.AltEmail;
                    txtRBIRefNo.Text = customerVo.RBIRefNum;

                    txtSlab.Text = customerVo.TaxSlab.ToString();

                    if (customerVo.RBIApprovalDate == DateTime.MinValue)
                        txtRBIRefDate.Text = "";
                    else
                        txtRBIRefDate.Text = customerVo.RBIApprovalDate.ToShortDateString();

                    if (customerVo.ResidenceLivingDate == DateTime.MinValue)
                        txtLivingSince.Text = "";
                    else
                        txtLivingSince.Text = customerVo.ResidenceLivingDate.ToShortDateString();

                    if (customerVo.JobStartDate == DateTime.MinValue)
                        txtJobStartDate.Text = "";
                    else
                        txtJobStartDate.Text = customerVo.JobStartDate.ToShortDateString();

                    txtMotherMaidenName.Text = customerVo.MothersMaidenName;
                    //txtMember_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    //txtMember_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                    BindFamilyAssociationList(customerVo.CustomerId);
                    BindBankDetails(customerVo.CustomerId);
                    if (customerVo.MfKYC == 1)
                        chkKYC.Checked = true;
                    else
                        chkKYC.Checked = false;


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
                FunctionInfo.Add("Method", "EditCustomerIndividualProfile.ascx:btnSubmit_Click()");
                object[] objects = new object[3];
                objects[0] = customerVo;
                objects[1] = path;
                objects[2] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void SetControlstate(string action)
        {
            if (action == "View")
            {
                btnGetSlab.Enabled = false;
                btnEdit.Visible = false;
                gvFamilyAssociate.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                gvISAAccountList.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                gvBankDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                
            }
            else if (action == "Edit")
            {
                btnGetSlab.Enabled = true;
                btnEdit.Visible = true;
                gvFamilyAssociate.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                gvISAAccountList.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                gvBankDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            }
        }

        private void BindFamilyAssociationList(int CustomerId)
        {
            DataSet dsCustomerAssociates;
            DataTable dtCustomerAssociates;
            dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
            dtCustomerAssociates = dsCustomerAssociates.Tables[0];
            if (dtCustomerAssociates != null)
            {
                gvFamilyAssociate.DataSource = dtCustomerAssociates;
                gvFamilyAssociate.DataBind();
                if (Cache["gvFamilyAssociate" + userVo.UserId] == null)
                {
                    Cache.Insert("gvFamilyAssociate" + userVo.UserId, dtCustomerAssociates);
                }
                else
                {
                    Cache.Remove("gvFamilyAssociate" + userVo.UserId);
                    Cache.Insert("gvFamilyAssociate" + userVo.UserId, dtCustomerAssociates);
                }
                trFamilyAssociates.Visible = true;
                gvFamilyAssociate.Visible = true;
            }

        }

        //private void BindBranchDropDown()
        //{
        //    RMVo rmVo = new RMVo();
        //    rmVo = (RMVo)Session[SessionContents.RmVo];
        //    int bmID = rmVo.RMId;
        //    try
        //    {
        //        UploadCommonBo uploadsCommonDao = new UploadCommonBo();
        //        DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
        //        if (ds != null)
        //        {
        //            ddlMemberBranch.DataSource = ds;
        //            ddlMemberBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
        //            ddlMemberBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
        //            ddlMemberBranch.DataBind();
        //        }
        //        ddlMemberBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

        //        object[] objects = new object[4];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        private void BindDropDowns()
        {
            AdvisorVo advisorVo = new AdvisorVo();
            try
            {
                dtMaritalStatus = XMLBo.GetMaritalStatus(path);
                ddlMaritalStatus.DataSource = dtMaritalStatus;
                ddlMaritalStatus.DataTextField = "MaritalStatus";
                ddlMaritalStatus.DataValueField = "MaritalStatusCode";
                ddlMaritalStatus.DataBind();
                ddlMaritalStatus.Items.Insert(0, new ListItem("Select a Status", "Select a Status"));

                dtNationality = XMLBo.GetNationality(path);
                ddlNationality.DataSource = dtNationality;
                ddlNationality.DataTextField = "Nationality";
                ddlNationality.DataValueField = "NationalityCode";
                ddlNationality.DataBind();
                ddlNationality.Items.Insert(0, new ListItem("Select a Nationality", "Select a Nationality"));

                dtOccupation = XMLBo.GetOccupation(path);
                ddlOccupation.DataSource = dtOccupation;
                ddlOccupation.DataTextField = "Occupation";
                ddlOccupation.DataValueField = "OccupationCode";
                ddlOccupation.DataBind();
                ddlOccupation.Items.Insert(0, new ListItem("Select a Occupation", "Select a Occupation"));

                dtQualification = XMLBo.GetQualification(path);
                ddlQualification.DataSource = dtQualification;
                ddlQualification.DataTextField = "Qualification";
                ddlQualification.DataValueField = "QualificationCode";
                ddlQualification.DataBind();
                ddlQualification.Items.Insert(0, new ListItem("Select a Qualification", "Select a Qualification"));

                dtState = XMLBo.GetStates(path);

                ddlCorrAdrState.DataSource = dtState;
                ddlCorrAdrState.DataTextField = "StateName";
                ddlCorrAdrState.DataValueField = "StateCode";
                ddlCorrAdrState.DataBind();
                ddlCorrAdrState.Items.Insert(0, new ListItem("Select a State", "Select a State"));

                ddlPermAdrState.DataSource = dtState;
                ddlPermAdrState.DataTextField = "StateName";
                ddlPermAdrState.DataValueField = "StateCode";
                ddlPermAdrState.DataBind();
                ddlPermAdrState.Items.Insert(0, new ListItem("Select a State", "Select a State"));

                ddlOfcAdrState.DataSource = dtState;
                ddlOfcAdrState.DataTextField = "StateName";
                ddlOfcAdrState.DataValueField = "StateCode";
                ddlOfcAdrState.DataBind();
                ddlOfcAdrState.Items.Insert(0, new ListItem("Select a State", "Select a State"));

                if (customerVo.Type.ToUpper().ToString() == "IND")
                {
                    dtCustomerSubType = XMLBo.GetCustomerSubType(path, "IND");

                }
                else
                {
                    dtCustomerSubType = XMLBo.GetCustomerSubType(path, "NIND");
                }
                ddlCustomerSubType.DataSource = dtCustomerSubType;
                ddlCustomerSubType.DataTextField = "CustomerTypeName";
                ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
                ddlCustomerSubType.DataBind();
                ddlCustomerSubType.SelectedValue = customerVo.SubType;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EditCustomerIndividualProfile.ascx:BindDropDowns()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void BindListBranch(int Id, string userType)
        {
            UploadCommonBo uploadCommonBo = new UploadCommonBo();
            DataSet ds = uploadCommonBo.GetAdviserBranchList(Id, userType);

            ddlAdviserBranchList.DataSource = ds.Tables[0];
            ddlAdviserBranchList.DataTextField = "AB_BranchName";
            ddlAdviserBranchList.DataValueField = "AB_BranchId";
            ddlAdviserBranchList.DataBind();
            ddlAdviserBranchList.Items.Insert(0, new ListItem("Select a Branch", "Select a Branch"));
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            try
            {
                if (Validation())
                {
                    customerVo.BranchId = int.Parse(ddlAdviserBranchList.SelectedValue.ToString());
                    //customerVo.Salutation = ddlSalutation.SelectedValue;
                    if (ddlSalutation.SelectedIndex == 0)
                        customerVo.Salutation = "";
                    else
                        customerVo.Salutation = ddlSalutation.SelectedValue.ToString();

                    customerVo.FirstName = txtFirstName.Text;
                    customerVo.MiddleName = txtMiddleName.Text;
                    customerVo.LastName = txtLastName.Text;
                    if (rbtnIndividual.Checked)
                    {
                        customerVo.Type = "IND";
                    }
                    else
                        customerVo.Type = "NIND";

                    if (rbtnMale.Checked)
                    {
                        customerVo.Gender = "M";
                    }
                    else if (rbtnFemale.Checked)
                    {
                        customerVo.Gender = "F";
                    }


                    customerVo.SubType = ddlCustomerSubType.SelectedItem.Value.ToString();

                    if (customerVo.SubType == "MNR")
                    {
                        customerVo.ContactMiddleName = txtGuardianMiddleName.Text;
                        customerVo.ContactLastName = txtGuardianLastName.Text;
                        customerVo.ContactFirstName = txtGuardianFirstName.Text;
                    }

                    if (txtDate.SelectedDate.ToString() == "")
                        customerVo.Dob = DateTime.MinValue;
                    else
                        customerVo.Dob = DateTime.Parse(txtDate.SelectedDate.ToString());
                    if (txtProfilingDate.Text == "")
                        customerVo.ProfilingDate = DateTime.MinValue;
                    else
                        customerVo.ProfilingDate = DateTime.Parse(txtProfilingDate.Text);
                    customerVo.PANNum = txtPanNumber.Text;

                    customerVo.CustCode = txtCustomerCode.Text;



                    customerVo.Adr1Line1 = txtCorrAdrLine1.Text;
                    customerVo.Adr1Line2 = txtCorrAdrLine2.Text;
                    customerVo.Adr1Line3 = txtCorrAdrLine3.Text;
                    if (txtCorrAdrPinCode.Text == "")
                        customerVo.Adr1PinCode = 0;
                    else
                        customerVo.Adr1PinCode = int.Parse(txtCorrAdrPinCode.Text);
                    customerVo.Adr1City = txtCorrAdrCity.Text;
                    if (ddlCorrAdrState.SelectedIndex == 0)
                        customerVo.Adr1State = "";
                    else
                        customerVo.Adr1State = ddlCorrAdrState.SelectedValue.ToString();

                    customerVo.Adr1Country = txtCorrAdrCountry.Text.ToString();
                    customerVo.Adr2Line1 = txtPermAdrLine1.Text.ToString();
                    customerVo.Adr2Line2 = txtPermAdrLine2.Text.ToString();
                    customerVo.Adr2Line3 = txtPermAdrLine3.Text.ToString();
                    customerVo.Adr2City = txtPermAdrCity.Text.ToString();
                    if (txtPermAdrPinCode.Text == "")
                        customerVo.Adr2PinCode = 0;
                    else
                        customerVo.Adr2PinCode = int.Parse(txtPermAdrPinCode.Text.ToString());
                    if (ddlPermAdrState.SelectedIndex == 0)
                        customerVo.Adr2State = "";
                    else
                        customerVo.Adr2State = ddlPermAdrState.SelectedValue.ToString();

                    customerVo.Adr2Country = txtPermAdrCountry.Text.ToString();
                    customerVo.OfcAdrLine1 = txtOfcAdrLine1.Text.ToString();
                    customerVo.OfcAdrLine2 = txtOfcAdrLine2.Text.ToString();
                    customerVo.OfcAdrLine3 = txtOfcAdrLine3.Text.ToString();
                    if (txtOfcAdrPinCode.Text == "")
                        customerVo.OfcAdrPinCode = 0;
                    else
                        customerVo.OfcAdrPinCode = int.Parse(txtOfcAdrPinCode.Text.ToString());

                    customerVo.OfcAdrCity = txtOfcAdrCity.Text.ToString();
                    if (ddlOfcAdrState.SelectedIndex == 0)
                        customerVo.OfcAdrState = "";
                    else
                        customerVo.OfcAdrState = ddlOfcAdrState.SelectedValue.ToString();

                    customerVo.OfcAdrCountry = txtOfcAdrCountry.Text.ToString();

                    string formatstring = "";
                    if (!string.IsNullOrEmpty(customerVo.FirstName.Trim()))
                        formatstring = customerVo.FirstName.Trim();
                    //array[0] = customerVo.Adr1Line1.Trim();
                    if (!string.IsNullOrEmpty(customerVo.MiddleName.Trim()))
                    {
                        if (formatstring == "")
                        {
                            formatstring = customerVo.MiddleName.Trim();
                        }
                        else
                        {
                            formatstring = formatstring + " " + customerVo.MiddleName.Trim();
                            //array[1] = customerVo.Adr1Line2.Trim();
                        }
                    }
                    if (!string.IsNullOrEmpty(customerVo.LastName.Trim()))
                    {
                        if (formatstring == "")
                        {
                            formatstring = customerVo.LastName.Trim();
                        }
                        else
                        {
                            formatstring = formatstring + " " + customerVo.LastName.Trim();
                            //array[1] = customerVo.Adr1Line2.Trim();
                        }
                    }        //--String.IsNullOrEmpty(customerVo.FirstName) +" " + customerVo.MiddleName + " " + customerVo.LastName;
                    customerVo.CompanyName = formatstring;
                    if (txtResPhoneNoIsd.Text == "")
                        customerVo.ResISDCode = 0;
                    else
                        customerVo.ResISDCode = int.Parse(txtResPhoneNoIsd.Text.ToString());

                    if (txtResPhoneNoStd.Text == "")
                        customerVo.ResSTDCode = 0;
                    else
                        customerVo.ResSTDCode = int.Parse(txtResPhoneNoStd.Text.ToString());

                    if (txtResPhoneNo.Text == "")
                        customerVo.ResPhoneNum = 0;
                    else
                        customerVo.ResPhoneNum = int.Parse(txtResPhoneNo.Text.ToString());

                    if (txtOfcPhoneNoIsd.Text == "")
                        customerVo.OfcISDCode = 0;
                    else
                        customerVo.OfcISDCode = int.Parse(txtOfcPhoneNoIsd.Text.ToString());

                    if (txtOfcPhoneNoStd.Text == "")
                        customerVo.OfcSTDCode = 0;
                    else
                        customerVo.OfcSTDCode = int.Parse(txtOfcPhoneNoStd.Text.ToString());

                    if (txtOfcPhoneNo.Text == "")
                        customerVo.OfcPhoneNum = 0;
                    else
                        customerVo.OfcPhoneNum = int.Parse(txtOfcPhoneNo.Text.ToString());

                    if (txtResFaxIsd.Text == "")
                        customerVo.ISDFax = 0;
                    else
                        customerVo.ISDFax = int.Parse(txtResFaxIsd.Text.ToString());

                    if (txtResFaxStd.Text == "")
                        customerVo.STDFax = 0;
                    else
                        customerVo.STDFax = int.Parse(txtResFaxStd.Text.ToString());

                    if (txtResFax.Text == "")
                        customerVo.Fax = 0;
                    else
                        customerVo.Fax = int.Parse(txtResFax.Text.ToString());

                    if (txtOfcFaxIsd.Text == "")
                        customerVo.OfcISDFax = 0;
                    else
                        customerVo.OfcISDFax = int.Parse(txtOfcFaxIsd.Text.ToString());

                    if (txtOfcFaxStd.Text == "")
                        customerVo.OfcSTDFax = 0;
                    else
                        customerVo.OfcSTDFax = int.Parse(txtOfcFaxStd.Text.ToString());

                    if (txtOfcFax.Text == "")
                        customerVo.OfcFax = 0;
                    else
                        customerVo.OfcFax = int.Parse(txtOfcFax.Text.ToString());

                    if (txtMobile1.Text == "")
                        customerVo.Mobile1 = 0;
                    else
                        customerVo.Mobile1 = long.Parse(txtMobile1.Text.ToString());

                    if (txtMobile2.Text == "")
                        customerVo.Mobile2 = 0;
                    else
                        customerVo.Mobile2 = long.Parse(txtMobile2.Text.ToString());

                    if (txtSlab.Text != "")
                        customerVo.TaxSlab = int.Parse(txtSlab.Text);

                    customerVo.Email = txtEmail.Text.ToString();
                    customerVo.AltEmail = txtAltEmail.Text.ToString();

                    if (ddlOccupation.SelectedIndex == 0)
                        customerVo.Occupation = null;
                    else
                        customerVo.Occupation = ddlOccupation.SelectedItem.Value.ToString();

                    if (chkdummypan.Checked)
                    {
                        customerVo.DummyPAN = 1;
                    }
                    else
                    {
                        customerVo.DummyPAN = 0;
                    }
                    //if (chkprospect.Checked)
                    //{
                    //    customerVo.IsProspect = 1;
                    //}
                    //else
                    //{
                    //    customerVo.IsProspect = 0;
                    //}
                    if (chkmail.Checked)
                    {
                        customerVo.AlertViaEmail = 1;
                    }
                    else
                    {
                        customerVo.AlertViaEmail = 0;
                    }
                    if (chksms.Checked)
                    {
                        customerVo.ViaSMS = 1;
                    }
                    else
                    {
                        customerVo.ViaSMS = 0;
                    }


                    if (ddlQualification.SelectedIndex == 0)
                        customerVo.Qualification = null;
                    else
                        customerVo.Qualification = ddlQualification.SelectedItem.Value.ToString();

                    if (ddlNationality.SelectedIndex == 0)
                        customerVo.Nationality = null;
                    else
                        customerVo.Nationality = ddlNationality.SelectedItem.Value.ToString();


                    customerVo.RBIRefNum = txtRBIRefNo.Text.ToString();
                    if (txtRBIRefDate.Text == "")
                        //customerVo.RBIApprovalDate = DateTime.Parse("1/1/0001 12:00:00 AM");
                        customerVo.RBIApprovalDate = DateTime.MinValue;
                    else
                        customerVo.RBIApprovalDate = DateTime.Parse(txtRBIRefDate.Text.ToString());
                    if (ddlMaritalStatus.SelectedIndex == 0)
                        customerVo.MaritalStatus = null;
                    else
                        customerVo.MaritalStatus = ddlMaritalStatus.SelectedItem.Value.ToString();

                    if (txtMarriageDate.SelectedDate.ToString() != string.Empty && txtMarriageDate.SelectedDate.ToString() != "dd/mm/yyyy")
                    {
                        customerVo.MarriageDate = DateTime.Parse(txtMarriageDate.SelectedDate.ToString());
                    }
                    else
                        customerVo.MarriageDate = DateTime.MinValue;

                    if (txtLivingSince.Text == "")
                        customerVo.ResidenceLivingDate = DateTime.MinValue;
                    else
                        customerVo.ResidenceLivingDate = DateTime.Parse(txtLivingSince.Text.ToString());
                    if (txtJobStartDate.Text == "")
                        customerVo.JobStartDate = DateTime.MinValue;
                    else
                        customerVo.JobStartDate = DateTime.Parse(txtJobStartDate.Text.ToString());
                    customerVo.MothersMaidenName = txtMotherMaidenName.Text.ToString();
                    if (chkKYC.Checked)
                    {
                        customerVo.MfKYC = 1;
                    }
                    else
                    {
                        customerVo.MfKYC = 0;
                    }

                    if (chkCorrPerm.Checked)
                    {
                        customerVo.Adr2City = txtCorrAdrCity.Text;
                        customerVo.Adr2Country = txtCorrAdrCountry.Text;
                        customerVo.Adr2Line1 = txtCorrAdrLine1.Text;
                        customerVo.Adr2Line2 = txtCorrAdrLine2.Text;
                        customerVo.Adr2Line3 = txtCorrAdrLine3.Text;
                        customerVo.Adr2PinCode = int.Parse(txtCorrAdrPinCode.Text);
                        customerVo.Adr2State = ddlCorrAdrState.SelectedItem.Value.ToString();
                    }


                    if (customerBo.UpdateCustomer(customerVo))
                    {
                        customerVo = customerBo.GetCustomer(customerVo.CustomerId);
                        Session["CustomerVo"] = customerVo;
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Profile updated Succesfully');", true);
                        if (customerVo.Type.ToUpper().ToString() == "IND")
                        {
                            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewCustomerIndividualProfile','none');", true);
                            viewForm = "View";
                            SetControlstate(viewForm);
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewNonIndividualProfile','none');", true);
                        }
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
                FunctionInfo.Add("Method", "EditCustomerIndividualProfile.ascx:btnEdit_Click()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public bool Validation()
        {
            bool result = true;
            int adviserId = ((AdvisorVo)Session["advisorVo"]).advisorId;
            try
            {
                if (customerBo.PANNumberDuplicateCheck(adviserId, txtPanNumber.Text.ToString(), customerVo.CustomerId))
                {
                    result = false;
                    lblPanDuplicate.Visible = true;
                }
                else
                {
                    lblPanDuplicate.Visible = false;
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
                FunctionInfo.Add("Method", "CustomerType.ascx:Validation()");
                object[] objects = new object[1];
                objects[0] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        protected void rbtnIndividual_CheckedChanged(object sender, EventArgs e)
        {
            dtCustomerSubType = XMLBo.GetCustomerSubType(path, "IND");
            ddlCustomerSubType.DataSource = dtCustomerSubType;
            ddlCustomerSubType.DataTextField = "CustomerTypeName";
            ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
            ddlCustomerSubType.DataBind();
            //  ddlCustomerSubType.SelectedValue = customerVo.SubType;
            if (customerVo != null)
            {
                Session["CustomerVo"] = customerVo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PageLoadScript", "loadcontrol('EditCustomerIndividualProfile','none');", true);
            }
        }

        protected void rbtnNonIndividual_CheckedChanged(object sender, EventArgs e)
        {
            dtCustomerSubType = XMLBo.GetCustomerSubType(path, "NIND");
            ddlCustomerSubType.DataSource = dtCustomerSubType;
            ddlCustomerSubType.DataTextField = "CustomerTypeName";
            ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
            ddlCustomerSubType.DataBind();
            // ddlCustomerSubType.SelectedValue = customerVo.SubType;
            if (customerVo != null)
            {
                customerVo.Type = "NIND";
                Session["CustomerVo"] = customerVo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PageLoadScript", "loadcontrol('EditCustomerNonIndividualProfile','none');", true);
            }
        }

        protected void ddlCustomerSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnIndividual.Checked)
            {
                if (ddlCustomerSubType.SelectedItem.Value.ToString() == "MNR")
                {
                    trGuardianName.Visible = true;
                }
                else
                {
                    trGuardianName.Visible = false;
                }
            }
            if (ddlCustomerSubType.SelectedValue == "RES")
            {
                btnGetSlab.Visible = true;
            }
            else
            {
                btnGetSlab.Visible = false;
            }
        }

        protected void btnGetSlab_Click(object sender, EventArgs e)
        {
            //if ((((customerVo.Gender == "") && (customerVo.Dob == DateTime.MinValue)) && (txtDob.Text == "")) && ((rbtnMale.Checked == false) && (rbtnFemale.Checked == false)))
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select gender and date of birth for the customer to get the tax slab');", true);
            //}
            // if ((customerVo.Gender != "") || ((rbtnMale.Checked != false) || (rbtnFemale.Checked != false)))
            //{
            //    if ((customerVo.Gender == "M") || (rbtnMale.Checked == true))
            //        hdnGender.Value = "Male";
            //    else if ((customerVo.Gender == "F") || (rbtnFemale.Checked == true))
            //        hdnGender.Value = "Female";
            //}            
            //if (txtDob.Text != "")
            //{
            //    CalculateAge(DateTime.Parse(txtDob.Text));
            //    if ((years < 60) && (hdnGender.Value == ""))
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select gender because customer is not senior citizen');", true);
            //    }
            //    else
            //    {
            //        dsGetSlab = customerBo.GetCustomerTaxSlab(customerVo.CustomerId, years, hdnGender.Value);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select date of birth for the customer to get the tax slab');", true);
            //}

            //if (dsGetSlab.Tables.Count != 0)
            //{
            //    if (dsGetSlab.Tables[0].Columns[0].ToString() != "Income")
            //    {
            //        if (dsGetSlab.Tables[0].Rows[0]["WTSR_TaxPer"].ToString() != null)
            //        {
            //            txtSlab.Text = dsGetSlab.Tables[0].Rows[0]["WTSR_TaxPer"].ToString();

            //        }
            //    }
            //    else if ((dsGetSlab.Tables[0].Rows.Count == 0) || (dsGetSlab.Tables[0].Rows[0]["Income"].ToString() == "0.00"))
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please put Income details for the customer to get the tax slab');", true);
            //    }
            //    else if (dsGetSlab.Tables[0].Rows[0]["Income"].ToString() != null)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please provide the proper required customer information to get Tax slab..');", true);
            //    }
            //}

            //if ((rbtnFemale.Checked != true || rbtnMale.Checked != true) && string.IsNullOrEmpty(txtDob.Text.Trim()))
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select gender and date of birth for the customer to get the tax slab');", true);
            //}
            //else if (!string.IsNullOrEmpty(txtDob.Text.Trim()))
            //{
            //    CalculateAge(DateTime.Parse(txtDob.Text));

            //    if ((years < 60) && (customerVo.Gender == ""))
            //    {
            //      ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select gender because customer is not senior citizen');", true);
            //    }
            //    else
            //    {
            //        dsGetSlab = customerBo.GetCustomerTaxSlab(customerVo.CustomerId, years, customerVo.Gender=="M"?"Male":"Female");
            //        if (dsGetSlab.Tables.Count > 0)
            //        {
            //            if (dsGetSlab.Tables[0].Rows.Count>0)
            //            {
            //                if (dsGetSlab.Tables[0].Rows[0]["WTSR_TaxPer"].ToString() != null)
            //                {
            //                    txtSlab.Text = dsGetSlab.Tables[0].Rows[0]["WTSR_TaxPer"].ToString();

            //                }
            //            }

            //        }
            //        else
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please put Income details for the customer to get the tax slab');", true); 
            //        }

            //    }
            //}
            bool isGenderExist = false;
            if (!string.IsNullOrEmpty(txtDate.SelectedDate.ToString()))
                CalculateAge(DateTime.Parse(txtDate.SelectedDate.ToString()));

            if ((rbtnFemale.Checked == true || rbtnMale.Checked == true))
            {
                isGenderExist = true;
            }
            if ((!isGenderExist && years < 60) || (string.IsNullOrEmpty(txtDate.SelectedDate.ToString()) && years < 60))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select gender and date of birth for the customer to get the tax slab');", true);
            }
            else if (!string.IsNullOrEmpty(txtDate.SelectedDate.ToString()))
            {

                if ((years < 60) && (!isGenderExist))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select gender because customer is not senior citizen');", true);
                }
                else
                {
                    dsGetSlab = customerBo.GetCustomerTaxSlab(customerVo.CustomerId, years, rbtnMale.Checked == true ? "Male" : "Female");
                    if (dsGetSlab.Tables.Count > 0)
                    {
                        if (dsGetSlab.Tables[0].Rows.Count > 0)
                        {
                            if (dsGetSlab.Tables[0].Rows[0]["WTSR_TaxPer"].ToString() != null)
                            {
                                txtSlab.Text = dsGetSlab.Tables[0].Rows[0]["WTSR_TaxPer"].ToString();

                            }
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please put Income details for the customer to get the tax slab');", true);
                    }

                }
            }


        }
        public int CalculateAge(DateTime birthDate)
        {
            DateTime now = DateTime.Today;

            years = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                --years;

            return years;
        }

        protected void rbtnType_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            GridEditableItem editedItem = radio.NamingContainer as GridEditableItem;
            RadioButton rbtnExisting = editedItem.FindControl("rbtnExisting") as RadioButton;
            RadioButton rbtnNew = editedItem.FindControl("rbtnNew") as RadioButton;
            HtmlTableRow trExCustHeader = editedItem.FindControl("trExCustHeader") as HtmlTableRow;
            HtmlTableRow trExCustomerType = editedItem.FindControl("trExCustomerType") as HtmlTableRow;
            HtmlTableRow trNewCustHeader = editedItem.FindControl("trNewCustHeader") as HtmlTableRow;
            HtmlTableRow trNewCustomer = editedItem.FindControl("trNewCustomer") as HtmlTableRow;


            if (rbtnExisting.Checked == true)
            {
                trExCustHeader.Visible = true;
                trExCustomerType.Visible = true;
                trNewCustHeader.Visible = false;
                trNewCustomer.Visible = false;
            }
            else if (rbtnNew.Checked == true)
            {
                trExCustHeader.Visible = false;
                trExCustomerType.Visible = false;
                trNewCustHeader.Visible = true;
                trNewCustomer.Visible = true;
            }
        }

        protected void ddlMemberBranch_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList dropdown = (DropDownList)sender;
            GridEditableItem editedItem = dropdown.NamingContainer as GridEditableItem;
            DropDownList ddlMemberBranch = editedItem.FindControl("ddlMemberBranch") as DropDownList;
            AutoCompleteExtender txtMember_autoCompleteExtender = editedItem.FindControl("txtMember_autoCompleteExtender") as AutoCompleteExtender;


            if (ddlMemberBranch.SelectedIndex == 0)
            {
                txtMember_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                txtMember_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
            }
            else
            {
                //txtMember_autoCompleteExtender.ContextKey = ddlMemberBranch.SelectedValue.ToString();
                //txtMember_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                txtMember_autoCompleteExtender.ContextKey = ddlMemberBranch.SelectedValue;
                txtMember_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllIndividualCustomers";
            }


        }
        //private void BindRelationshipDropDown()
        //{
        //    DataTable dtRelationship = customerBo.GetMemberRelationShip();
        //    if (dtRelationship.Rows.Count > 0)
        //    {
        //        ddlRelation.DataSource = dtRelationship;
        //        ddlRelation.DataTextField = dtRelationship.Columns["XR_Relationship"].ToString();
        //        ddlRelation.DataValueField = dtRelationship.Columns["XR_RelationshipCode"].ToString();
        //        ddlRelation.DataBind();
        //        ddlRelation.Items.Insert(0, new ListItem("Select", "Select"));
        //        /*----------------------------------------------------------*/
        //        ddlNewRelationship.DataSource = dtRelationship;
        //        ddlNewRelationship.DataTextField = dtRelationship.Columns["XR_Relationship"].ToString();
        //        ddlNewRelationship.DataValueField = dtRelationship.Columns["XR_RelationshipCode"].ToString();
        //        ddlNewRelationship.DataBind();
        //        ddlNewRelationship.Items.Insert(0, new ListItem("Select", "Select"));
        //    }

        //}

        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            if (txtCustomerId.Value != string.Empty)
            {
                DataTable dt = customerBo.GetCustomerPanAddress(int.Parse(txtCustomerId.Value));
                DataRow dr = dt.Rows[0];
                hdnPannum.Value = dr["C_PANNum"].ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CustomerSearchPannum", "DisplayPanId('" + hdnPannum.Value + "');", true);                
               

            }
        }

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{

        //    if(customerVo !=null)
        //        customerId = customerVo.CustomerId;
        //    if(!string.IsNullOrEmpty(txtCustomerId.Value))
        //        associateId = int.Parse(txtCustomerId.Value);
        //    if(ddlRelation.SelectedIndex!=0)
        //        relCode= ddlRelation.SelectedItem.Value;
        //    customerFamilyBo.CustomerAssociateUpdate(customerId, associateId, relCode, userVo.UserId);
        //    BindFamilyAssociationList(customerId);

        //    txtMember.Text = "";
        //    lblGetPan.Text = "";
        //    ddlRelation.SelectedIndex =0;
        //}

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    CustomerVo customerNewVo= new CustomerVo();
        //    List<int> customerIds = null;
        //    UserVo tempUserVo = (UserVo)Session["userVo"];

        //    customerNewVo.RmId = customerVo.RmId;
        //    customerNewVo.BranchId = customerVo.BranchId;
        //    customerNewVo.Type = "IND";
        //    customerNewVo.FirstName = txtNewName.Text;
        //    customerNewVo.PANNum = txtNewPan.Text;
        //    customerVo.ProfilingDate = DateTime.Today;
        //    customerNewVo.UserId = customerVo.UserId;
        //    userVo.Email = txtEmail.Text.ToString();
        //    customerPortfolioVo.IsMainPortfolio = 1;
        //    customerPortfolioVo.PortfolioTypeCode = "RGL";
        //    customerPortfolioVo.PortfolioName = "MyPortfolio";
        //    customerVo.ViaSMS = 1;
        //    customerIds = customerBo.CreateCompleteCustomer(customerNewVo, userVo, customerPortfolioVo, tempUserVo.UserId);
        //    if (customerIds != null)
        //    {
        //        associateId = customerIds[1];
        //        CustomerFamilyVo familyVo = new CustomerFamilyVo();
        //        CustomerFamilyBo familyBo = new CustomerFamilyBo();
        //        familyVo.AssociateCustomerId = customerIds[1];
        //        familyVo.CustomerId = associateId;
        //        familyVo.Relationship = "SELF";
        //        familyBo.CreateCustomerFamily(familyVo, customerIds[1], userVo.UserId);
        //    }
        //    if (customerVo != null)
        //        customerId = customerVo.CustomerId;
        //    if (ddlNewRelationship.SelectedIndex != 0)
        //        relCode = ddlNewRelationship.SelectedItem.Value;
        //    customerFamilyBo.CustomerAssociateUpdate(customerId, associateId, relCode, userVo.UserId);
        //    BindFamilyAssociationList(customerId);

        //    txtNewName.Text = "";
        //    txtNewPan.Text = "";
        //    ddlNewRelationship.SelectedIndex = 0;
        //}

        //protected void btnImgAddExMember_Click(object sender, ImageClickEventArgs e)
        //{
        //    RadioButton radio = (RadioButton)sender;
        //    GridEditableItem editedItem = radio.NamingContainer as GridEditableItem;
        //    HtmlTableRow trExCustHeader = editedItem.FindControl("trExCustHeader") as HtmlTableRow;
        //    HtmlTableRow trExCustomerType = editedItem.FindControl("trExCustomerType") as HtmlTableRow;
        //    HtmlTableRow trNewCustHeader = editedItem.FindControl("trNewCustHeader") as HtmlTableRow;
        //    HtmlTableRow trNewCustomer = editedItem.FindControl("trNewCustomer") as HtmlTableRow;
        //    HtmlTableRow trCustomerTypeSelection = editedItem.FindControl("trCustomerTypeSelection") as HtmlTableRow;
        //    trCustomerTypeSelection.Visible = true;
        //    trExCustHeader.Visible = false;
        //    trExCustomerType.Visible = false;
        //    trNewCustHeader.Visible = false;
        //    trNewCustomer.Visible = false;
        //}

        #region unused
        
        //protected void btnImgAddCustomer_Click(object sender, ImageClickEventArgs e)
        //{

        //    pnlJointholders.Visible = false;
        //    //trNewISAAccountSection.Visible = true;

        //    JointHolderHeading.Visible = false;


        //    LoadNominees();
        //    BindModeOfHolding();
        //    ddlModeOfHolding.SelectedValue = "SI";
        //    ddlModeOfHolding.Enabled = false;
        //    btnGenerateISA.Visible = true;

        //    pnlJointholders.Visible = false;

        //}

        #endregion

        public void BindCustomerISAAccountGrid()
        {
            DataTable dtCustomerISAAccounts = new DataTable();
            dtCustomerISAAccounts = customerBo.GetCustomerISAAccounts(customerVo.CustomerId);
            if (hdnRequestId.Value != "")
            {
                DataRow[] drCustomerISAAccounts;
                drCustomerISAAccounts = dtCustomerISAAccounts.Select("AISAQ_RequestQueueid=" + hdnRequestId.Value);
                if (drCustomerISAAccounts.Count() > 0)
                {
                    gvISAAccountList.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                    
                }
                else
                {
                    gvISAAccountList.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                }
            }
            gvISAAccountList.DataSource = dtCustomerISAAccounts;
            gvISAAccountList.DataBind();

            if (Cache["gvISAAccountList" + userVo.UserId] == null)
            {
                Cache.Insert("gvISAAccountList" + userVo.UserId, dtCustomerISAAccounts);
            }
            else
            {
                Cache.Remove("gvISAAccountList" + userVo.UserId);
                Cache.Insert("gvISAAccountList" + userVo.UserId, dtCustomerISAAccounts);
            }

        }

        protected void rbtnYes_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton dropdown = (RadioButton)sender;

            GridEditableItem editedItem = dropdown.NamingContainer as GridEditableItem;
            DropDownList ddlModeOfHolding = editedItem.FindControl("ddlModeOfHolding") as DropDownList;

            Label JointHolderHeading = editedItem.FindControl("JointHolderHeading") as Label;
            Panel pnlJointholders = editedItem.FindControl("pnlJointholders") as Panel;

            ddlModeOfHolding.Enabled = true;
            ddlModeOfHolding.SelectedIndex = 0;
            //tdJointHolders.Visible = true;
            JointHolderHeading.Visible = true;
            pnlJointholders.Visible = true;

        }

        protected void rbtnNo_CheckedChanged(object sender, EventArgs e)
        {

            RadioButton dropdown = (RadioButton)sender;

            GridEditableItem editedItem = dropdown.NamingContainer as GridEditableItem;
            DropDownList ddlModeOfHolding = editedItem.FindControl("ddlModeOfHolding") as DropDownList;

            Label JointHolderHeading = editedItem.FindControl("JointHolderHeading") as Label;
            Panel pnlJointholders = editedItem.FindControl("pnlJointholders") as Panel;

            ddlModeOfHolding.SelectedValue = "SI";
            ddlModeOfHolding.Enabled = false;
            //tdJointHolders.Visible = false;
            JointHolderHeading.Visible = false;
            pnlJointholders.Visible = false;

        }


        //private void BindModeOfHolding()
        //{
        //    DataTable dtModeOfHolding = XMLBo.GetModeOfHolding(path);
        //    ddlModeOfHolding.DataSource = dtModeOfHolding;
        //    ddlModeOfHolding.DataTextField = "ModeOfHolding";
        //    ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
        //    ddlModeOfHolding.DataBind();
        //    ddlModeOfHolding.Items.Insert(0, new ListItem("Select Mode of Holding", "Select Mode of Holding"));
        //}

        //private void LoadNominees()
        //{
        //    DataTable dtCustomerAssociates = new DataTable();
        //    DataTable dtNewCustomerAssociate = new DataTable();
        //    DataRow drCustomerAssociates;
        //    try
        //    {

        //        dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
        //        dtCustomerAssociates = dsCustomerAssociates.Tables[0];

        //        dtNewCustomerAssociate.Columns.Add("MemberCustomerId");
        //        dtNewCustomerAssociate.Columns.Add("AssociationId");
        //        dtNewCustomerAssociate.Columns.Add("Name");
        //        dtNewCustomerAssociate.Columns.Add("Relationship");

        //        foreach (DataRow dr in dtCustomerAssociates.Rows)
        //        {

        //            drCustomerAssociates = dtNewCustomerAssociate.NewRow();
        //            drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
        //            drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
        //            drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
        //            drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
        //            dtNewCustomerAssociate.Rows.Add(drCustomerAssociates);
        //        }

        //        if (dtNewCustomerAssociate.Rows.Count > 0)
        //        {

        //            gvNominees.DataSource = dtNewCustomerAssociate;
        //            gvNominees.DataBind();

        //            gvNominees.Visible = true;
        //            tdNominees.Visible = true;

        //            gvJointHoldersList.DataSource = dtNewCustomerAssociate;
        //            gvJointHoldersList.DataBind();

        //            gvJointHoldersList.Visible = true;
        //            pnlJointholders.Visible = true;
        //            //tdJointHolders.Visible = true;

        //        }
        //        else
        //        {
        //            trAssociate.Visible = false;
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }

        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "CustomerMFAccountAdd.ascx:LoadNominees()");
        //        object[] objects = new object[1];
        //        objects[0] = customerVo;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //}

        protected void btnUpdateISA_Click(object sender, EventArgs e)
        {

            try
            {

                bool IsISAUpdated = false;
                Button Button = (Button)sender;


                GridEditableItem editedItem = Button.NamingContainer as GridEditableItem;



                RadioButton rbtnYes = editedItem.FindControl("rbtnYes") as RadioButton;
                RadioButton rbtnNo = editedItem.FindControl("rbtnNo") as RadioButton;
                RadioButton rbtnPOAYes = editedItem.FindControl("rbtnPOAYes") as RadioButton;
                DropDownList ddlModeOfHolding = editedItem.FindControl("ddlModeOfHolding") as DropDownList;
                Label JointHolderHeading = editedItem.FindControl("JointHolderHeading") as Label;
                Panel pnlJointholders = editedItem.FindControl("pnlJointholders") as Panel;
                Button btnGenerateISA = editedItem.FindControl("btnGenerateISA") as Button;

                BindCustomerISAAccountGrid();
                btnGenerateISA.Visible = false;
                rbtnNo.Checked = true;

                if (rbtnYes.Checked)
                    customerISAAccountsVo.IsJointHolding = true;
                else
                    customerISAAccountsVo.IsJointHolding = false;

                customerISAAccountsVo.ModeOfHolding = ddlModeOfHolding.SelectedValue.ToString();
                if (rbtnPOAYes.Checked)
                    customerISAAccountsVo.IsOperatedByPOA = true;
                else
                    customerISAAccountsVo.IsOperatedByPOA = false;
                int accountId=Convert.ToInt32(Session["IsaAccountId"]);
                customerISAAccountsVo.ISAAccountId = accountId;

                IsISAUpdated = customerAccountBo.UpdateCustomerISAAccount(customerISAAccountsVo);

                GridView gvJointHoldersList = editedItem.FindControl("gvJointHoldersList") as GridView;
                GridView gvNominees = editedItem.FindControl("gvNominees") as GridView;
                string associationIdsNominee = string.Empty;
                string associationIdsForJH = string.Empty;

                foreach (GridViewRow gvr in gvJointHoldersList.Rows)
                {

                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        customerISAAccountsVo.AssociationId = int.Parse(gvJointHoldersList.DataKeys[gvr.RowIndex].Values[1].ToString());
                        customerISAAccountsVo.AssociationTypeCode = "JointHolder";
                        //customerAccountBo.UpdateISAAccountAssociation(customerISAAccountsVo);

                        associationIdsNominee = associationIdsNominee + customerISAAccountsVo.AssociationId + "~";
                    }


                }
                if(associationIdsNominee!="")
                    customerAccountBo.UpdateISAAccountAssociation(customerISAAccountsVo, associationIdsNominee);

                foreach (GridViewRow gvr in gvNominees.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId0")).Checked == true)
                    {
                        customerISAAccountsVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[1].ToString());
                        customerISAAccountsVo.AssociationTypeCode = "Nominee";
                        //customerAccountBo.UpdateISAAccountAssociation(customerISAAccountsVo);

                        associationIdsForJH = associationIdsForJH + customerISAAccountsVo.AssociationId + "~";
                    }
                }

                if(associationIdsForJH!="")
                    customerAccountBo.UpdateISAAccountAssociation(customerISAAccountsVo, associationIdsForJH);

                JointHolderHeading.Visible = false;
                pnlJointholders.Visible = false;
                //trNewISAAccountSection.Visible = false;
                BindCustomerISAAccountGrid();
                btnGenerateISA.Visible = false;
                rbtnNo.Checked = true;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EditCustomerIndivisualProfile.ascx:btnGenerateISA_Click()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }


        protected void btnGenerateISA_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {


                Button Button = (Button)sender;

                GridEditableItem editedItem = Button.NamingContainer as GridEditableItem;
                RadioButton rbtnYes = editedItem.FindControl("rbtnYes") as RadioButton;
                RadioButton rbtnNo = editedItem.FindControl("rbtnNo") as RadioButton;
                RadioButton rbtnPOAYes = editedItem.FindControl("rbtnPOAYes") as RadioButton;
                DropDownList ddlModeOfHolding = editedItem.FindControl("ddlModeOfHolding") as DropDownList;
                Label JointHolderHeading = editedItem.FindControl("JointHolderHeading") as Label;
                Panel pnlJointholders = editedItem.FindControl("pnlJointholders") as Panel;
                Button btnGenerateISA = editedItem.FindControl("btnGenerateISA") as Button;

                BindCustomerISAAccountGrid();
                btnGenerateISA.Visible = false;
                rbtnNo.Checked = true;

                if (rbtnYes.Checked)
                    customerISAAccountsVo.IsJointHolding = true;
                else
                    customerISAAccountsVo.IsJointHolding = false;

                customerISAAccountsVo.ModeOfHolding = ddlModeOfHolding.SelectedValue.ToString();
                if (rbtnPOAYes.Checked)
                    customerISAAccountsVo.IsOperatedByPOA = true;
                else
                    customerISAAccountsVo.IsOperatedByPOA = false;

                GridView gvJointHoldersList = editedItem.FindControl("gvJointHoldersList") as GridView;
                GridView gvNominees = editedItem.FindControl("gvNominees") as GridView;
                //Check ISA Account Combination 
                
                customerISAAccountsVo.ISAAccountId = customerAccountBo.CreateCustomerISAAccount(customerISAAccountsVo, customerVo.CustomerId, userVo.UserId,int.Parse(hdnRequestId.Value));

                foreach (GridViewRow gvr in gvJointHoldersList.Rows)
                {

                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        customerISAAccountsVo.AssociationId = int.Parse(gvJointHoldersList.DataKeys[gvr.RowIndex].Values[1].ToString());
                        customerISAAccountsVo.AssociationTypeCode = "JointHolder";
                        customerAccountBo.CreateISAAccountAssociation(customerISAAccountsVo, userVo.UserId);
                    }


                }


                foreach (GridViewRow gvr in gvNominees.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId0")).Checked == true)
                    {
                        customerISAAccountsVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[1].ToString());
                        customerISAAccountsVo.AssociationTypeCode = "Nominee";
                        customerAccountBo.CreateISAAccountAssociation(customerISAAccountsVo, userVo.UserId);
                    }
                }

                JointHolderHeading.Visible = false;
                pnlJointholders.Visible = false;
                //trNewISAAccountSection.Visible = false;
                BindCustomerISAAccountGrid();
                btnGenerateISA.Visible = false;
                rbtnNo.Checked = true;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EditCustomerIndivisualProfile.ascx:btnGenerateISA_Click()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        //private void CollectDataForISAAccountSetup()
        //{


        //    if (rbtnYes.Checked)
        //        customerISAAccountsVo.IsJointHolding = true;
        //    else
        //        customerISAAccountsVo.IsJointHolding = false;

        //    customerISAAccountsVo.ModeOfHolding = ddlModeOfHolding.SelectedValue.ToString();
        //    if (rbtnPOAYes.Checked)
        //        customerISAAccountsVo.IsOperatedByPOA = true;
        //    else
        //        customerISAAccountsVo.IsOperatedByPOA = false;


        //}


        //Bank Details Functionality start
        public void BindBankDetails(int customerIdForGettingBankDetails)
        {
            try
            {
                SessionBo.CheckSession();
                customerVo = (CustomerVo)Session["CustomerVo"];
                customerIdForGettingBankDetails = customerVo.CustomerId;

                customerBankAccountList = customerBankAccountBo.GetCustomerBankAccounts(customerIdForGettingBankDetails);
                //if (customerBankAccountList.Count != 0)
                //{
                    DataTable dtCustomerBankAccounts = new DataTable();
                    dtCustomerBankAccounts.Columns.Add("CB_CustBankAccId");
                    dtCustomerBankAccounts.Columns.Add("CB_BankName");
                    dtCustomerBankAccounts.Columns.Add("CB_BranchName");
                    dtCustomerBankAccounts.Columns.Add("XBAT_BankAccountTypeCode");
                    dtCustomerBankAccounts.Columns.Add("XMOH_ModeOfHoldingCode");
                    dtCustomerBankAccounts.Columns.Add("CB_AccountNum");


                    dtCustomerBankAccounts.Columns.Add("CB_BranchAdrLine1");
                    dtCustomerBankAccounts.Columns.Add("CB_BranchAdrLine2");
                    dtCustomerBankAccounts.Columns.Add("CB_BranchAdrLine3");
                    dtCustomerBankAccounts.Columns.Add("CB_BranchAdrPinCode");
                    dtCustomerBankAccounts.Columns.Add("CB_BranchAdrCity");
                    dtCustomerBankAccounts.Columns.Add("CB_BranchAdrState");
                    dtCustomerBankAccounts.Columns.Add("CB_BranchAdrCountry");
                    dtCustomerBankAccounts.Columns.Add("CB_MICR");
                    dtCustomerBankAccounts.Columns.Add("CB_IFSC");

                    dtCustomerBankAccounts.Columns.Add("BankAccountTypeCode");
                    dtCustomerBankAccounts.Columns.Add("ModeOfHoldingCode");

                    DataRow drCustomerBankAccount;
                    for (int i = 0; i < customerBankAccountList.Count; i++)
                    {
                        drCustomerBankAccount = dtCustomerBankAccounts.NewRow();
                        customerBankAccountVo = new CustomerBankAccountVo();
                        customerBankAccountVo = customerBankAccountList[i];
                        drCustomerBankAccount[0] = customerBankAccountVo.CustBankAccId.ToString();
                        drCustomerBankAccount[1] = customerBankAccountVo.BankName.ToString();
                        drCustomerBankAccount[2] = customerBankAccountVo.BranchName.ToString();
                        drCustomerBankAccount[3] = customerBankAccountVo.AccountType.ToString();
                        drCustomerBankAccount[4] = customerBankAccountVo.ModeOfOperation.ToString();
                        drCustomerBankAccount[5] = customerBankAccountVo.BankAccountNum.ToString();

                        if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrLine1))
                            drCustomerBankAccount[6] = customerBankAccountVo.BranchAdrLine1.ToString();
                        if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrLine2))
                            drCustomerBankAccount[7] = customerBankAccountVo.BranchAdrLine2.ToString();
                        if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrLine3))
                            drCustomerBankAccount[8] = customerBankAccountVo.BranchAdrLine3.ToString();
                        if (customerBankAccountVo.BranchAdrPinCode != 0)
                            drCustomerBankAccount["CB_BranchAdrPinCode"] = customerBankAccountVo.BranchAdrPinCode.ToString();
                        if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrCity))
                            drCustomerBankAccount[10] = customerBankAccountVo.BranchAdrCity.ToString();

                        if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrState))
                            drCustomerBankAccount[11] = customerBankAccountVo.BranchAdrState.ToString();
                        if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrCountry))
                            drCustomerBankAccount[12] = customerBankAccountVo.BranchAdrCountry.ToString();
                        if (customerBankAccountVo.MICR != 0)
                            drCustomerBankAccount["CB_MICR"] = customerBankAccountVo.MICR.ToString();
                        if (!string.IsNullOrEmpty(customerBankAccountVo.IFSC))
                            drCustomerBankAccount[14] = customerBankAccountVo.IFSC.ToString();
                        drCustomerBankAccount[15] = customerBankAccountVo.AccountTypeCode.ToString();
                        drCustomerBankAccount[16] = customerBankAccountVo.ModeOfOperationCode.ToString();
                        dtCustomerBankAccounts.Rows.Add(drCustomerBankAccount);
                    }

                    if (Cache["gvDetailsForBank" + userVo.UserId] == null)
                    {
                        Cache.Insert("gvDetailsForBank" + userVo.UserId, dtCustomerBankAccounts);
                    }
                    else
                    {
                        Cache.Remove("gvDetailsForBank" + userVo.UserId);
                        Cache.Insert("gvDetailsForBank" + userVo.UserId, dtCustomerBankAccounts);
                    }
                    gvBankDetails.DataSource = dtCustomerBankAccounts;
                    gvBankDetails.DataBind();
                    gvBankDetails.Visible = true;
                //}
                //else
                //{
                //    gvBankDetails.DataSource = null;
                //    gvBankDetails.DataBind();
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
                FunctionInfo.Add("Method", "ViewBankDetails.ascx:Page_Load()");
                object[] objects = new object[5];
                objects[0] = customerVo;
                objects[2] = customerBankAccountVo;
                objects[3] = customerBankAccountList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void gvBankDetails_ItemCommand(object source, GridCommandEventArgs e)
        {
            int customerId = 0;
            string strExternalCode = string.Empty;
            string strExternalType = string.Empty;
            DateTime createdDate = new DateTime();
            DateTime editedDate = new DateTime();
            DateTime deletedDate = new DateTime();
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;

                DropDownList ddlAccountType = (DropDownList)e.Item.FindControl("ddlAccountType");
                TextBox txtAccountNumber = (TextBox)e.Item.FindControl("txtAccountNumber");
                DropDownList ddlModeOfOperation = (DropDownList)e.Item.FindControl("ddlModeOfOperation");
                TextBox txtBankName = (TextBox)e.Item.FindControl("txtBankName");
                TextBox txtBranchName = (TextBox)e.Item.FindControl("txtBranchName");
                TextBox txtBankAdrLine1 = (TextBox)e.Item.FindControl("txtBankAdrLine1");
                TextBox txtBankAdrLine2 = (TextBox)e.Item.FindControl("txtBankAdrLine2");
                TextBox txtBankAdrLine3 = (TextBox)e.Item.FindControl("txtBankAdrLine3");
                TextBox txtBankAdrCity = (TextBox)e.Item.FindControl("txtBankAdrCity");
                TextBox txtBankAdrPinCode = (TextBox)e.Item.FindControl("txtBankAdrPinCode");
                TextBox txtMicr = (TextBox)e.Item.FindControl("txtMicr");
                DropDownList ddlBankAdrState = (DropDownList)e.Item.FindControl("ddlBankAdrState");
                TextBox txtIfsc = (TextBox)e.Item.FindControl("txtIfsc");

                bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());
                customerVo = (CustomerVo)Session["customerVo"];
                customerId = customerVo.CustomerId;

                customerBankAccountVo.BankAccountNum = txtAccountNumber.Text.ToString();
                customerBankAccountVo.AccountType = ddlAccountType.SelectedItem.Value.ToString();
                customerBankAccountVo.ModeOfOperation = ddlModeOfOperation.SelectedItem.Value.ToString();
                customerBankAccountVo.BankName = txtBankName.Text.ToString();
                customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
                customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
                customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
                customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
                if (txtBankAdrPinCode.Text.ToString() != "")
                    customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
                else
                    customerBankAccountVo.BranchAdrPinCode = 0;
                customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
                if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
                    customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();

                //customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedItem.Value.ToString();
                customerBankAccountVo.CustBankAccId = bankId;
                customerBankAccountVo.BranchAdrCountry = "India";
                customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
                if (txtMicr.Text.ToString() != "")
                    customerBankAccountVo.MICR = int.Parse(txtMicr.Text.ToString());
                else
                    customerBankAccountVo.MICR = 0;
                customerBankAccountBo.UpdateCustomerBankAccount(customerBankAccountVo, customerId);


            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                CustomerBo customerBo = new CustomerBo();
                bool isInserted = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;

                DropDownList ddlAccountType = (DropDownList)e.Item.FindControl("ddlAccountType");
                TextBox txtAccountNumber = (TextBox)e.Item.FindControl("txtAccountNumber");
                DropDownList ddlModeOfOperation = (DropDownList)e.Item.FindControl("ddlModeOfOperation");
                TextBox txtBankName = (TextBox)e.Item.FindControl("txtBankName");
                TextBox txtBranchName = (TextBox)e.Item.FindControl("txtBranchName");
                TextBox txtBankAdrLine1 = (TextBox)e.Item.FindControl("txtBankAdrLine1");
                TextBox txtBankAdrLine2 = (TextBox)e.Item.FindControl("txtBankAdrLine2");
                TextBox txtBankAdrLine3 = (TextBox)e.Item.FindControl("txtBankAdrLine3");
                TextBox txtBankAdrCity = (TextBox)e.Item.FindControl("txtBankAdrCity");
                TextBox txtBankAdrPinCode = (TextBox)e.Item.FindControl("txtBankAdrPinCode");
                TextBox txtMicr = (TextBox)e.Item.FindControl("txtMicr");
                DropDownList ddlBankAdrState = (DropDownList)e.Item.FindControl("ddlBankAdrState");
                TextBox txtIfsc = (TextBox)e.Item.FindControl("txtIfsc");


                RMVo rmVo = new RMVo();
                int userId;
                rmVo = (RMVo)Session["RmVo"];
                userId = rmVo.UserId;
                string chk;

                if (Session["Check"] != null)
                {
                    chk = Session["Check"].ToString();
                }

                customerVo = (CustomerVo)Session["customerVo"];
                customerId = customerVo.CustomerId;
                customerBankAccountVo = new CustomerBankAccountVo();

                customerBankAccountVo.AccountType = ddlAccountType.SelectedValue.ToString();
                customerBankAccountVo.BankAccountNum = txtAccountNumber.Text.ToString();

                if (ddlModeOfOperation.SelectedValue.ToString() != "Select a Mode of Holding")
                    customerBankAccountVo.ModeOfOperation = ddlModeOfOperation.SelectedValue.ToString();
                customerBankAccountVo.BankName = txtBankName.Text.ToString();
                customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
                customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
                customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
                customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
                if (txtBankAdrPinCode.Text.ToString() != "")
                    customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
                customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
                if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
                    customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();
                customerBankAccountVo.BranchAdrCountry = "India";
                if (txtMicr.Text.ToString() != "")
                    customerBankAccountVo.MICR = long.Parse(txtMicr.Text.ToString());
                customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
                customerBankAccountVo.Balance = 0;
                //customerBankAccountVo.Balance = long.Parse(txtBalance.Text.ToString());

                customerBankAccountBo.CreateCustomerBankAccount(customerBankAccountVo, customerId, userId);


                txtAccountNumber.Text = "";
                txtBankAdrLine1.Text = "";
                txtBankAdrLine2.Text = "";
                txtBankAdrLine3.Text = "";
                txtBankAdrPinCode.Text = "";
                txtBankAdrCity.Text = "";
                txtBankName.Text = "";
                txtBranchName.Text = "";
                txtIfsc.Text = "";
                txtMicr.Text = "";
                ddlAccountType.SelectedIndex = 0;
                ddlModeOfOperation.SelectedIndex = 0;


                //isInserted = customerBo.InsertProductAMCSchemeMappingDetalis(customerId, strExternalCode, strExternalType, createdDate, editedDate, deletedDate);
            }

            if (e.CommandName == "Delete")
            {
                bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());
                customerBankAccountBo.DeleteCustomerBankAccount(bankId);
            }
            BindBankDetails(customerId);
        }

        protected void gvFamilyAssociate_ItemDataBound(object sender, GridItemEventArgs e)
        {
            customerBo = new CustomerBo();
            if ((e.Item is GridEditFormItem) && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DropDownList ddlMemberBranch = (DropDownList)editedItem.FindControl("ddlMemberBranch");
                HtmlTableRow trCustomerTypeSelection = (HtmlTableRow)editedItem.FindControl("trCustomerTypeSelection");
                TextBox txtMember = editedItem.FindControl("txtMember") as TextBox;


                if (e.Item.RowIndex == -1)
                {
                    txtMember.Enabled = true;
                    ddlMemberBranch.Enabled = true;
                    trCustomerTypeSelection.Visible = true;
                }
                else
                {
                    txtMember.Enabled = false;
                    ddlMemberBranch.Enabled = false;
                    trCustomerTypeSelection.Visible = false;
                }
               


            }
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DataTable dtRelationship = customerBo.GetMemberRelationShip();

                TextBox txtMember = (TextBox)item.FindControl("txtMember");
                Label lblGetPan = (Label)item.FindControl("lblGetPan");
                TextBox txtNewMemPan = (TextBox)e.Item.FindControl("txtNewPan");
                Session["lblGetPan"] = lblGetPan;

                DropDownList ddlRelation = (DropDownList)gefi.FindControl("ddlRelation");
                ddlRelation.DataSource = dtRelationship;
                ddlRelation.DataTextField = dtRelationship.Columns["XR_Relationship"].ToString();
                ddlRelation.DataValueField = dtRelationship.Columns["XR_RelationshipCode"].ToString();
                ddlRelation.DataBind();
                ddlRelation.Items.Insert(0, new ListItem("Select", "Select"));

                DropDownList ddlNewRelationship = (DropDownList)gefi.FindControl("ddlNewRelationship");
                ddlNewRelationship.DataSource = dtRelationship;
                ddlNewRelationship.DataTextField = dtRelationship.Columns["XR_Relationship"].ToString();
                ddlNewRelationship.DataValueField = dtRelationship.Columns["XR_RelationshipCode"].ToString();
                ddlNewRelationship.DataBind();
                ddlNewRelationship.Items.Insert(0, new ListItem("Select", "Select"));

                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                DropDownList ddlMemberBranch = (DropDownList)gefi.FindControl("ddlMemberBranch");
                if (ds != null)
                {
                    ddlMemberBranch.DataSource = ds;
                    ddlMemberBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlMemberBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlMemberBranch.DataBind();
                }
                ddlMemberBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));



            }
            if (e.Item is GridDataItem)
            {
                    GridDataItem dataItem = e.Item as GridDataItem;
                    LinkButton buttonEdit = dataItem["editColumn"].Controls[0] as LinkButton;
                    if (viewForm == "View")
                        buttonEdit.Visible = false;
                    else if (viewForm == "Edit")
                        buttonEdit.Visible = true;
            }
            string strRelationshipCode = string.Empty;
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {

                strRelationshipCode = gvFamilyAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XR_RelationshipCode"].ToString();
                string panNum = gvFamilyAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["C_PANNum"].ToString();
                int branchId = int.Parse(gvFamilyAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AB_BranchId"].ToString());

                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DataTable dtRelationship = customerBo.GetMemberRelationShip();

                TextBox txtMember = (TextBox)editedItem.FindControl("txtMember");
                Label lblGetPan = (Label)editedItem.FindControl("lblGetPan");
                TextBox txtNewMemPan = (TextBox)e.Item.FindControl("txtNewPan");
                lblGetPan.Text = panNum;
                txtNewMemPan.Text = panNum;

                DropDownList ddlRelation = (DropDownList)editedItem.FindControl("ddlRelation");
                ddlRelation.DataSource = dtRelationship;
                ddlRelation.DataTextField = dtRelationship.Columns["XR_Relationship"].ToString();
                ddlRelation.DataValueField = dtRelationship.Columns["XR_RelationshipCode"].ToString();
                ddlRelation.DataBind();
                ddlRelation.Items.Insert(0, new ListItem("Select", "Select"));
                ddlRelation.SelectedValue = strRelationshipCode;

                DropDownList ddlNewRelationship = (DropDownList)editedItem.FindControl("ddlNewRelationship");
                ddlNewRelationship.DataSource = dtRelationship;
                ddlNewRelationship.DataTextField = dtRelationship.Columns["XR_Relationship"].ToString();
                ddlNewRelationship.DataValueField = dtRelationship.Columns["XR_RelationshipCode"].ToString();
                ddlNewRelationship.DataBind();
                ddlNewRelationship.Items.Insert(0, new ListItem("Select", "Select"));
                ddlNewRelationship.SelectedValue = strRelationshipCode;

                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                DropDownList ddlMemberBranch = (DropDownList)editedItem.FindControl("ddlMemberBranch");
                ddlMemberBranch.DataSource = ds;
                ddlMemberBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                ddlMemberBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                ddlMemberBranch.DataBind();
                ddlMemberBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
                ddlMemberBranch.SelectedValue = branchId.ToString();

            }
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                RadioButton rbtnExisting = (RadioButton)editedItem.FindControl("rbtnExisting");
                rbtnExisting.Checked = true;

                HtmlTableRow trExCustHeader = editedItem.FindControl("trExCustHeader") as HtmlTableRow;
                HtmlTableRow trExCustomerType = editedItem.FindControl("trExCustomerType") as HtmlTableRow;
                HtmlTableRow trNewCustHeader = editedItem.FindControl("trNewCustHeader") as HtmlTableRow;
                HtmlTableRow trNewCustomer = editedItem.FindControl("trNewCustomer") as HtmlTableRow;
                AutoCompleteExtender txtMember_autoCompleteExtender = editedItem.FindControl("txtMember_autoCompleteExtender") as AutoCompleteExtender;

                txtMember_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                txtMember_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";

                trExCustHeader.Visible = true;
                trExCustomerType.Visible = true;
                trNewCustHeader.Visible = false;
                trNewCustomer.Visible = false;

            }

        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            DataSet dtGvSchemeDetails = new DataSet();
            dtGvSchemeDetails = (DataSet)Cache["gvSchemeDetailsForMappinginSuperAdmin"];
            gvBankDetails.DataSource = dtGvSchemeDetails;

            gvBankDetails.ExportSettings.OpenInNewWindow = true;
            gvBankDetails.ExportSettings.IgnorePaging = true;
            gvBankDetails.ExportSettings.HideStructureColumns = true;
            gvBankDetails.ExportSettings.ExportOnlyData = true;
            gvBankDetails.ExportSettings.FileName = "Scheme Mapping Details";
            gvBankDetails.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvBankDetails.MasterTableView.ExportToExcel();
        }

        protected void gvBankDetails_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGvBankDetails = new DataTable();
            if (Cache["gvDetailsForBank" + userVo.UserId] != null)
            {
                dtGvBankDetails = (DataTable)Cache["gvDetailsForBank" + userVo.UserId];
                gvBankDetails.DataSource = dtGvBankDetails;
            }
        }
        protected void gvFamilyAssociate_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtFamilyAssociate = new DataTable();
            if (Cache["gvFamilyAssociate" + userVo.UserId] != null)
            {
                dtFamilyAssociate = (DataTable)Cache["gvFamilyAssociate" + userVo.UserId];
                gvFamilyAssociate.DataSource = dtFamilyAssociate;
            }
        }

        protected void gvFamilyAssociate_ItemCommand(object source, GridCommandEventArgs e)
        {

            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                CustomerBo customerBo = new CustomerBo();
                bool isUpdated = false;
                string relationCode = string.Empty;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                int AssociationId = int.Parse(gvFamilyAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CA_AssociationId"].ToString());
                Button Button3 = (Button)e.Item.FindControl("Button3");
                Button Button1 = (Button)e.Item.FindControl("Button1");
                if (Button3.Visible == true)
                {
                    TextBox txtMember = (TextBox)e.Item.FindControl("txtMember");
                    Label lblGetPan = (Label)e.Item.FindControl("lblGetPan");
                    DropDownList ddlRelation = (DropDownList)e.Item.FindControl("ddlRelation");
                    txtMember.Enabled = false;
                    lblGetPan.Enabled = false;
                    relationCode = ddlRelation.SelectedValue;
                }
                else if (Button1.Visible == true)
                {
                    TextBox txtMember = (TextBox)e.Item.FindControl("txtNewName");
                    TextBox lblGetPan = (TextBox)e.Item.FindControl("txtNewPan");
                    DropDownList ddlNewRelationship = (DropDownList)e.Item.FindControl("ddlNewRelationship");
                    txtMember.Enabled = false;
                    lblGetPan.Enabled = false;
                    relationCode = ddlNewRelationship.SelectedValue;
                }

                isUpdated = customerBo.UpdateMemberRelation(AssociationId, relationCode);

            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                CustomerVo customerNewVo = new CustomerVo();
                List<int> customerIds = null;
                UserVo tempUserVo = new UserVo();

                Button Button3 = (Button)e.Item.FindControl("Button3");
                Button Button1 = (Button)e.Item.FindControl("Button1");

               

                if (Button1.Visible == true)
                {
                    customerNewVo.RmId = customerVo.RmId;
                    customerNewVo.BranchId = customerVo.BranchId;
                    customerNewVo.Type = "IND";
                    TextBox txtNewMemName = (TextBox)e.Item.FindControl("txtNewName");
                    TextBox txtNewMemPan = (TextBox)e.Item.FindControl("txtNewPan");
                    DropDownList ddlNewMemRel = (DropDownList)e.Item.FindControl("ddlNewRelationship");
                    customerNewVo.FirstName = txtNewMemName.Text;
                    customerNewVo.PANNum = txtNewMemPan.Text;
                    customerVo.ProfilingDate = DateTime.Today;
                    tempUserVo.FirstName = txtNewMemName.Text;
                    tempUserVo.Email = txtEmail.Text;
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PortfolioName = "MyPortfolio";
                    customerVo.ViaSMS = 1;
                    customerIds = customerBo.CreateCompleteCustomer(customerNewVo, tempUserVo, customerPortfolioVo, tempUserVo.UserId);
                    if (customerIds != null)
                    {
                        associateId = customerIds[1];
                        CustomerFamilyVo familyVo = new CustomerFamilyVo();
                        CustomerFamilyBo familyBo = new CustomerFamilyBo();
                        familyVo.AssociateCustomerId = customerIds[1];
                        familyVo.CustomerId = associateId;
                        familyVo.Relationship = "SELF";
                        familyBo.CreateCustomerFamily(familyVo, customerIds[1], userVo.UserId);
                    }
                    if (customerVo != null)
                        customerId = customerVo.CustomerId;
                    if (ddlNewMemRel.SelectedIndex != 0)
                        relCode = ddlNewMemRel.SelectedItem.Value;
                    customerFamilyBo.CustomerAssociateUpdate(customerId, associateId, relCode, userVo.UserId);
                }
                if (Button3.Visible == true)
                {
                    CustomerFamilyBo familyBo = new CustomerFamilyBo();
                    CustomerFamilyVo familyVo = new CustomerFamilyVo();
                    if (customerVo != null)
                        customerId = customerVo.CustomerId;
                    if (!string.IsNullOrEmpty(txtCustomerId.Value))
                        associateId = int.Parse(txtCustomerId.Value);
                    familyVo.AssociateCustomerId = associateId;
                    familyVo.CustomerId = associateId;
                    familyVo.Relationship = "SELF";
                    familyBo.CreateCustomerFamily(familyVo, associateId, userVo.UserId);
                    DropDownList ddlRelation = (DropDownList)e.Item.FindControl("ddlRelation");
                    if (ddlRelation.SelectedIndex != 0)
                        relCode = ddlRelation.SelectedItem.Value;
                    customerFamilyBo.CustomerAssociateUpdate(customerId, associateId, relCode, userVo.UserId);
                    BindFamilyAssociationList(customerId);
                }

            }

            BindFamilyAssociationList(customerVo.CustomerId);
        }

        protected void gvBankDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                DataTable dtAccType = new DataTable();
                DataTable dtModeOfOpn = new DataTable();
                DataTable dtBankState = new DataTable();

                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlAccountType = (DropDownList)gefi.FindControl("ddlAccountType");
                dtAccType = XMLBo.GetBankAccountTypes(path);
                ddlAccountType.DataSource = dtAccType;
                ddlAccountType.DataTextField = "BankAccountType";
                ddlAccountType.DataValueField = "BankAccountTypeCode";
                ddlAccountType.DataBind();
                ddlAccountType.Items.Insert(0, new ListItem("Select", "Select"));

                DropDownList ddlModeOfOperation = (DropDownList)gefi.FindControl("ddlModeOfOperation");
                dtModeOfOpn = XMLBo.GetModeOfHolding(path);
                ddlModeOfOperation.DataSource = dtModeOfOpn;
                ddlModeOfOperation.DataTextField = "ModeOfHolding";
                ddlModeOfOperation.DataValueField = "ModeOfHoldingCode";
                ddlModeOfOperation.DataBind();
                ddlModeOfOperation.Items.Insert(0, new ListItem("Select", "Select"));

                DropDownList ddlBankAdrState = (DropDownList)gefi.FindControl("ddlBankAdrState");
                dtBankState = XMLBo.GetStates(path);
                ddlBankAdrState.DataSource = dtBankState;
                ddlBankAdrState.DataTextField = "StateName";
                ddlBankAdrState.DataValueField = "StateCode";
                ddlBankAdrState.DataBind();
                ddlBankAdrState.Items.Insert(0, new ListItem("Select", "Select"));

            }
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                LinkButton buttonEdit = dataItem["editColumn"].Controls[0] as LinkButton;
                if (viewForm == "View")
                    buttonEdit.Visible = false;
                else if (viewForm == "Edit")
                    buttonEdit.Visible = true;
            }
            string strBankAdrState;
            string strModeOfOperation;
            string strAccountType;
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());
                strBankAdrState = gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_BranchAdrState"].ToString();
                strModeOfOperation = gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ModeOfHoldingCode"].ToString();
                strAccountType = gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["BankAccountTypeCode"].ToString();

                GridEditFormItem editedItem = (GridEditFormItem)e.Item;

                DataTable dtAccType = new DataTable();
                DataTable dtModeOfOpn = new DataTable();
                DataTable dtBankState = new DataTable();

                DropDownList ddlAccountType = (DropDownList)editedItem.FindControl("ddlAccountType");
                dtAccType = XMLBo.GetBankAccountTypes(path);
                ddlAccountType.DataSource = dtAccType;
                ddlAccountType.DataTextField = "BankAccountType";
                ddlAccountType.DataValueField = "BankAccountTypeCode";
                ddlAccountType.DataBind();
                ddlAccountType.SelectedValue = strAccountType;

                DropDownList ddlModeOfOperation = (DropDownList)editedItem.FindControl("ddlModeOfOperation");
                dtModeOfOpn = XMLBo.GetModeOfHolding(path);
                ddlModeOfOperation.DataSource = dtModeOfOpn;
                ddlModeOfOperation.DataTextField = "ModeOfHolding";
                ddlModeOfOperation.DataValueField = "ModeOfHoldingCode";
                ddlModeOfOperation.DataBind();
                ddlModeOfOperation.SelectedValue = strModeOfOperation;

                DropDownList ddlBankAdrState = (DropDownList)editedItem.FindControl("ddlBankAdrState");
                dtBankState = XMLBo.GetStates(path);
                ddlBankAdrState.DataSource = dtBankState;
                ddlBankAdrState.DataTextField = "StateName";
                ddlBankAdrState.DataValueField = "StateCode";
                ddlBankAdrState.DataBind();
                ddlBankAdrState.SelectedValue = strBankAdrState;



            }
        }
        //Bank Details Functionality End

        #region ISA Details Functionality Start

        #region unused     
        protected void gvISAAccountList_ItemCommand(object source, GridCommandEventArgs e)
        {
            int ISAAccounts = 0;
            //int customerId = 0;
            //string strExternalCode = string.Empty;
            //string strExternalType = string.Empty;
            //DateTime createdDate = new DateTime();
            //DateTime editedDate = new DateTime();
            //DateTime deletedDate = new DateTime();
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                //    GridEditableItem gridEditableItem = (GridEditableItem)e.Item;

                //    DropDownList ddlAccountType = (DropDownList)e.Item.FindControl("ddlAccountType");
                //    TextBox txtAccountNumber = (TextBox)e.Item.FindControl("txtAccountNumber");
                //    DropDownList ddlModeOfOperation = (DropDownList)e.Item.FindControl("ddlModeOfOperation");
                //    TextBox txtBankName = (TextBox)e.Item.FindControl("txtBankName");
                //    TextBox txtBranchName = (TextBox)e.Item.FindControl("txtBranchName");
                //    TextBox txtBankAdrLine1 = (TextBox)e.Item.FindControl("txtBankAdrLine1");
                //    TextBox txtBankAdrLine2 = (TextBox)e.Item.FindControl("txtBankAdrLine2");
                //    TextBox txtBankAdrLine3 = (TextBox)e.Item.FindControl("txtBankAdrLine3");
                //    TextBox txtBankAdrCity = (TextBox)e.Item.FindControl("txtBankAdrCity");
                //    TextBox txtBankAdrPinCode = (TextBox)e.Item.FindControl("txtBankAdrPinCode");
                //    TextBox txtMicr = (TextBox)e.Item.FindControl("txtMicr");
                //    DropDownList ddlBankAdrState = (DropDownList)e.Item.FindControl("ddlBankAdrState");
                //    TextBox txtIfsc = (TextBox)e.Item.FindControl("txtIfsc");

                //    bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());
                //    customerVo = (CustomerVo)Session["customerVo"];
                //    customerId = customerVo.CustomerId;

                //    customerBankAccountVo.BankAccountNum = txtAccountNumber.Text.ToString();
                //    customerBankAccountVo.AccountType = ddlAccountType.SelectedItem.Value.ToString();
                //    customerBankAccountVo.ModeOfOperation = ddlModeOfOperation.SelectedItem.Value.ToString();
                //    customerBankAccountVo.BankName = txtBankName.Text.ToString();
                //    customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
                //    customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
                //    customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
                //    customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
                //    if (txtBankAdrPinCode.Text.ToString() != "")
                //        customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
                //    else
                //        customerBankAccountVo.BranchAdrPinCode = 0;
                //    customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
                //    if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
                //        customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();

                //    //customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedItem.Value.ToString();
                //    customerBankAccountVo.CustBankAccId = bankId;
                //    customerBankAccountVo.BranchAdrCountry = "India";
                //    customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
                //    if (txtMicr.Text.ToString() != "")
                //        customerBankAccountVo.MICR = int.Parse(txtMicr.Text.ToString());
                //    else
                //        customerBankAccountVo.MICR = 0;
                //    customerBankAccountBo.UpdateCustomerBankAccount(customerBankAccountVo, customerId);


                //CollectDataForISAAccountSetup();
                //customerISAAccountsVo.ISAAccountId = customerAccountBo.CreateCustomerISAAccount(customerISAAccountsVo, customerVo.CustomerId, userVo.UserId);

                //foreach (GridViewRow gvr in this.gvJointHoldersList.Rows)
                //{

                //    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                //    {
                //        customerISAAccountsVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[1].ToString());
                //        customerISAAccountsVo.AssociationTypeCode = "JointHolder";
                //        customerAccountBo.CreateISAAccountAssociation(customerISAAccountsVo, userVo.UserId);
                //    }


                //}


                //foreach (GridViewRow gvr in this.gvNominees.Rows)
                //{
                //    if (((CheckBox)gvr.FindControl("chkId0")).Checked == true)
                //    {
                //        customerISAAccountsVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[1].ToString());
                //        customerISAAccountsVo.AssociationTypeCode = "Nominee";
                //        customerAccountBo.CreateISAAccountAssociation(customerISAAccountsVo, userVo.UserId);
                //    }
                //}

                //JointHolderHeading.Visible = false;
                //pnlJointholders.Visible = false;
                ////trNewISAAccountSection.Visible = false;
                //BindCustomerISAAccountGrid();
                //btnGenerateISA.Visible = false;
                //rbtnNo.Checked = true;


            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {

            }

            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                bool isDeleted = false;
                customerBo = new CustomerBo();
                GridDataItem dataItem = (GridDataItem)e.Item;
                TableCell strISAAccounts = dataItem["CISAA_accountid"];
                ISAAccounts = int.Parse(strISAAccounts.Text);
                isDeleted = customerAccountBo.DeleteISAAccount(ISAAccounts);
            }



            BindFamilyAssociationList(customerVo.CustomerId);
            BindCustomerISAAccountGrid();
        }
        #endregion




        protected void gvISAAccountList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGvBankDetails = new DataTable();
            if (Cache["gvISAAccountList" + userVo.UserId] != null)
            {
                dtGvBankDetails = (DataTable)Cache["gvISAAccountList" + userVo.UserId];
                gvISAAccountList.DataSource = dtGvBankDetails;
            }
        }




        protected void gvISAAccountList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                DataTable dtModeOfHolding = new DataTable();
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlModeOfHolding = (DropDownList)gefi.FindControl("ddlModeOfHolding");
                Button btnGenerateISA = (Button)gefi.FindControl("btnGenerateISA");
                Button Button1 = (Button)gefi.FindControl("Button1");
                btnGenerateISA.Visible = true;
                Button1.Visible = false;
                dtModeOfHolding = XMLBo.GetModeOfHolding(path);
                ddlModeOfHolding.DataSource = dtModeOfHolding;
                ddlModeOfHolding.DataTextField = "ModeOfHolding";
                ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
                ddlModeOfHolding.DataBind();
                ddlModeOfHolding.Items.Insert(0, new ListItem("Select Mode of Holding", "Select Mode of Holding"));

            }
            if (e.Item is GridDataItem)
            {
                // GridDataItem dataItem = e.Item as GridDataItem;
                 
                GridDataItem dataItem = e.Item as GridDataItem;              
                LinkButton buttonEdit = dataItem["editColumn"].Controls[0] as LinkButton;
                if(viewForm=="View")
                    buttonEdit.Visible = false;
                else if(viewForm=="Edit")
                    buttonEdit.Visible = true;
            }
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {

                dsISADetails = new DataSet();

                IsaAccountId = Convert.ToInt32(gvISAAccountList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CISAA_accountid"]);
                if (Session["IsaAccountId"] != null)
                {
                    Session["IsaAccountId"] = null;


                    Session["IsaAccountId"] = IsaAccountId;
                }
                else
                    Session["IsaAccountId"] = IsaAccountId;

                dsISADetails = customerAccountBo.GetISADetails(IsaAccountId);


                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlModeOfHolding = (DropDownList)gefi.FindControl("ddlModeOfHolding");
                RadioButton rbtnYes = (RadioButton)gefi.FindControl("rbtnYes");
                RadioButton rbtnNo = (RadioButton)gefi.FindControl("rbtnNo");
                RadioButton rbtnPOAYes = (RadioButton)gefi.FindControl("rbtnPOAYes");
                RadioButton rbtnPOANo = (RadioButton)gefi.FindControl("rbtnPOANo");
                GridView gvNominees = (GridView)gefi.FindControl("gvNominees");
                GridView gvJointHoldersList = (GridView)gefi.FindControl("gvJointHoldersList");
                Button btnGenerateISA = (Button)gefi.FindControl("btnGenerateISA");
                Button Button1 = (Button)gefi.FindControl("Button1");
                Panel pnlJointholders = (Panel)gefi.FindControl("pnlJointholders");
                HtmlTableCell tdNominees = (HtmlTableCell)gefi.FindControl("tdNominees");
                HtmlTableRow trAssociate = (HtmlTableRow)gefi.FindControl("trAssociate");
                Label JointHolderHeading = (Label)gefi.FindControl("JointHolderHeading");
                btnGenerateISA.Visible = false;
                Button1.Visible = true;
                DataTable dtModeOfHolding = XMLBo.GetModeOfHolding(path);
                ddlModeOfHolding.DataSource = dtModeOfHolding;
                ddlModeOfHolding.DataTextField = "ModeOfHolding";
                ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
                ddlModeOfHolding.DataBind();
                ddlModeOfHolding.Items.Insert(0, new ListItem("Select Mode of Holding", "Select Mode of Holding"));
                ddlModeOfHolding.SelectedValue = dsISADetails.Tables[0].Rows[0]["XMOH_ModeOfHoldingCode"].ToString();

                if (dsISADetails.Tables[0].Rows[0]["CISAA_Isjointlyheld"].ToString() == "1")
                {
                    rbtnYes.Checked = true;
                    rbtnNo.Checked = false;

                    JointHolderHeading.Visible = true;
                    pnlJointholders.Visible = true;
                }
                else
                {
                    rbtnYes.Checked = false;
                    rbtnNo.Checked = true;

                    JointHolderHeading.Visible = false;
                    pnlJointholders.Visible = false;
                }
                if (dsISADetails.Tables[0].Rows[0]["CISAA_IsPOAOperated"].ToString() == "1")
                {
                    rbtnPOAYes.Checked = true;
                    rbtnPOANo.Checked = false;
                }
                else
                {
                    rbtnPOAYes.Checked = false;
                    rbtnPOANo.Checked = true;
                }

                #region loadnominnes

                DataTable dtCustomerAssociates = new DataTable();
                DataTable dtNewCustomerAssociate = new DataTable();
                DataRow drCustomerAssociates;


                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                dtCustomerAssociates = dsCustomerAssociates.Tables[0];

                dtNewCustomerAssociate.Columns.Add("MemberCustomerId");
                dtNewCustomerAssociate.Columns.Add("AssociationId");
                dtNewCustomerAssociate.Columns.Add("Name");
                dtNewCustomerAssociate.Columns.Add("Relationship");

                foreach (DataRow dr in dtCustomerAssociates.Rows)
                {

                    drCustomerAssociates = dtNewCustomerAssociate.NewRow();
                    drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                    drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                    drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                    drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                    dtNewCustomerAssociate.Rows.Add(drCustomerAssociates);
                }

                if (dtNewCustomerAssociate.Rows.Count > 0)
                {

                    gvNominees.DataSource = dtNewCustomerAssociate;
                    gvNominees.DataBind();

                    gvNominees.Visible = true;
                    tdNominees.Visible = true;

                    gvJointHoldersList.DataSource = dtNewCustomerAssociate;
                    gvJointHoldersList.DataBind();

                    gvJointHoldersList.Visible = true;
                    pnlJointholders.Visible = true;

                }
                else
                {
                    trAssociate.Visible = false;
                }

                if (dsISADetails.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsISADetails.Tables[1].Rows)
                    {
                        for (int i = 0; i < gvNominees.Rows.Count; i++)
                        {
                            if (dr["CA_AssociationId"].ToString() == dtNewCustomerAssociate.Rows[i]["AssociationId"].ToString())
                            {
                                CheckBox cb = (CheckBox)gvNominees.Rows[i].Cells[0].FindControl("chkId0");
                                cb.Checked = true;
                            }
                        }
                    }
                }

                if (dsISADetails.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsISADetails.Tables[2].Rows)
                    {
                        for (int i = 0; i < gvJointHoldersList.Rows.Count; i++)
                        {
                            if (dr["CA_AssociationId"].ToString() == dtNewCustomerAssociate.Rows[i]["AssociationId"].ToString())
                            {
                                CheckBox cb = (CheckBox)gvJointHoldersList.Rows[i].Cells[0].FindControl("chkId");
                                cb.Checked = true;
                            }
                        }
                    }
                }


                #endregion

                #region selecting the selected for edit



                #endregion

            }


            if (e.Item is GridEditFormInsertItem)
            {

                GridEditFormInsertItem gridEditableItem = (GridEditFormInsertItem)e.Item;
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                Label JointHolderHeading = (Label)gefi.FindControl("JointHolderHeading");
                GridView gvNominees = (GridView)gefi.FindControl("gvJointHoldersList");
                GridView gvJointHoldersList = (GridView)gefi.FindControl("gvNominees");
                DropDownList ddlModeOfHolding = (DropDownList)gefi.FindControl("ddlModeOfHolding");
                Button btnGenerateISA = (Button)gefi.FindControl("btnGenerateISA");
                Panel pnlJointholders = (Panel)gefi.FindControl("pnlJointholders");
                HtmlTableCell tdNominees = (HtmlTableCell)gefi.FindControl("tdNominees");
                HtmlTableRow trAssociate = (HtmlTableRow)gefi.FindControl("trAssociate");
                JointHolderHeading.Visible = false;

                #region loadnominnes

                DataTable dtCustomerAssociates = new DataTable();
                DataTable dtNewCustomerAssociate = new DataTable();
                DataRow drCustomerAssociates;


                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                dtCustomerAssociates = dsCustomerAssociates.Tables[0];

                dtNewCustomerAssociate.Columns.Add("MemberCustomerId");
                dtNewCustomerAssociate.Columns.Add("AssociationId");
                dtNewCustomerAssociate.Columns.Add("Name");
                dtNewCustomerAssociate.Columns.Add("Relationship");

                foreach (DataRow dr in dtCustomerAssociates.Rows)
                {

                    drCustomerAssociates = dtNewCustomerAssociate.NewRow();
                    drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                    drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                    drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                    drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                    dtNewCustomerAssociate.Rows.Add(drCustomerAssociates);
                }

                if (dtNewCustomerAssociate.Rows.Count > 0)
                {

                    gvNominees.DataSource = dtNewCustomerAssociate;
                    gvNominees.DataBind();

                    gvNominees.Visible = true;
                    tdNominees.Visible = true;

                    gvJointHoldersList.DataSource = dtNewCustomerAssociate;
                    gvJointHoldersList.DataBind();

                    gvJointHoldersList.Visible = true;
                    pnlJointholders.Visible = true;

                }
                else
                {
                    trAssociate.Visible = false;
                }

                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                DataTable dtModeOfHolding = new DataTable();


                dtModeOfHolding = XMLBo.GetModeOfHolding(path);
                ddlModeOfHolding.DataSource = dtModeOfHolding;
                ddlModeOfHolding.DataTextField = "ModeOfHolding";
                ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
                ddlModeOfHolding.DataBind();
                ddlModeOfHolding.Items.Insert(0, new ListItem("Select Mode of Holding", "Select Mode of Holding"));

                ddlModeOfHolding.SelectedValue = "SI";
                ddlModeOfHolding.Enabled = false;
                btnGenerateISA.Visible = true;
                pnlJointholders.Visible = false;
                #endregion

            }
        }

        #endregion


        //ISA Details Functionality End


        //private void BindTaxSlabDropDown()
        //{
        //    try
        //    {
        //        if (dsGetSlab.Tables[0].Rows.Count > 0)
        //        {
        //            ddlTaxSlab.DataSource = dsGetSlab.Tables[0]; ;
        //            ddlTaxSlab.DataValueField = dsGetSlab.Tables[0].Columns["WTSR_TaxPer"].ToString();
        //            ddlTaxSlab.DataTextField = dsGetSlab.Tables[0].Columns["Income Range"].ToString();
        //            ddlTaxSlab.DataBind();
        //        }
        //        ddlTaxSlab.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "CustomerIndividualAdd.ascx:BindTaxSlabDropDown()");

        //        object[] objects = new object[4];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        //protected void ddlTaxSlab_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    hdnTaxSlabValue.Value = ddlTaxSlab.SelectedValue;
        //}
    }
}
