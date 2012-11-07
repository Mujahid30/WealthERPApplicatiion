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


namespace WealthERP.Customer
{
    public partial class EditCustomerIndividualProfile : System.Web.UI.UserControl
    {
        UserVo userVo = null;
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorStaffBo adviserStaffBo = new AdvisorStaffBo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerISAAccountsVo customerISAAccountsVo = new CustomerISAAccountsVo();
        string path;
        DataTable dtMaritalStatus = new DataTable();
        DataTable dtNationality = new DataTable();
        DataTable dtOccupation = new DataTable();
        DataTable dtQualification = new DataTable();
        DataTable dtState = new DataTable();
        DataTable dtCustomerSubType = new DataTable();
        DataSet dsCustomerAssociates = new DataSet();
       
        DataSet dsGetSlab = new DataSet();
        int years;

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
                RMVo customerRMVo = new RMVo();
                if (!IsPostBack)
                {
                    BindCustomerISAAccountGrid();
                    trNewISAAccountSection.Visible = false;
                    lblPanDuplicate.Visible = false;
                    btnGenerateISA.Visible = false;
                   
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

                    if(rbtnMale.Checked)
                    {
                        customerVo.Gender = "M";
                    }
                    else if(rbtnFemale.Checked)
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
                        if (customerVo.Type.ToUpper().ToString() == "IND")
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewCustomerIndividualProfile','none');", true);
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
                if (customerBo.PANNumberDuplicateCheck(adviserId, txtPanNumber.Text.ToString(),customerVo.CustomerId))
                {
                    result = false;
                    lblPanDuplicate.Visible = true;
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
                 Session["CustomerVo"]= customerVo;
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

        protected void btnImgAddCustomer_Click(object sender, ImageClickEventArgs e)
        {
            trNewISAAccountSection.Visible = true;
            LoadNominees();
            BindModeOfHolding();
            ddlModeOfHolding.SelectedValue = "SI";
            ddlModeOfHolding.Enabled = false;
            btnGenerateISA.Visible = true;
        }

        public void BindCustomerISAAccountGrid()
        {
            DataTable dtCustomerISAAccounts = new DataTable();
            dtCustomerISAAccounts = customerBo.GetCustomerISAAccounts(customerVo.CustomerId);
            gvISAAccountList.DataSource = dtCustomerISAAccounts;
            gvISAAccountList.DataBind();
        }

        protected void rbtnYes_CheckedChanged(object sender, EventArgs e)
        {
          
            ddlModeOfHolding.Enabled = true;
            ddlModeOfHolding.SelectedIndex = 0;
            tdJointHolders.Visible = true;
            JointHolderHeading.Visible = true;
        }

        protected void rbtnNo_CheckedChanged(object sender, EventArgs e)
        {
            ddlModeOfHolding.SelectedValue = "SI";
            ddlModeOfHolding.Enabled = false;
            tdJointHolders.Visible = false;
            JointHolderHeading.Visible = false;
            
        }


        private void BindModeOfHolding()
        {
            DataTable dtModeOfHolding = XMLBo.GetModeOfHolding(path);
            ddlModeOfHolding.DataSource = dtModeOfHolding;
            ddlModeOfHolding.DataTextField = "ModeOfHolding";
            ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
            ddlModeOfHolding.DataBind();
            ddlModeOfHolding.Items.Insert(0, new ListItem("Select Mode of Holding", "Select Mode of Holding"));
        }

        private void LoadNominees()
        {
            DataTable dtCustomerAssociates = new DataTable();
            DataTable dtNewCustomerAssociate = new DataTable();
            DataRow drCustomerAssociates;
            try
            {

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
                    tdJointHolders.Visible = true;
                    
                }
                else
                {
                    trAssociate.Visible = false;
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
                FunctionInfo.Add("Method", "CustomerMFAccountAdd.ascx:LoadNominees()");
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
          
            try
            {
                CollectDataForISAAccountSetup();
                customerISAAccountsVo.ISAAccountId = customerAccountBo.CreateCustomerISAAccount(customerISAAccountsVo, customerVo.CustomerId, userVo.UserId);
                
                foreach (GridViewRow gvr in this.gvJointHoldersList.Rows)
                {
                    
                        if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                        {  
                            customerISAAccountsVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[1].ToString());
                            customerISAAccountsVo.AssociationTypeCode = "JointHolder";
                            customerAccountBo.CreateISAAccountAssociation(customerISAAccountsVo, userVo.UserId);
                        }
                      
                    
                }
                             

                foreach (GridViewRow gvr in this.gvNominees.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId0")).Checked == true)
                    {
                        customerISAAccountsVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[1].ToString());
                        customerISAAccountsVo.AssociationTypeCode = "Nominee";
                        customerAccountBo.CreateISAAccountAssociation(customerISAAccountsVo, userVo.UserId);
                    }
                }

                trNewISAAccountSection.Visible = false;
                BindCustomerISAAccountGrid();
                btnGenerateISA.Visible = false;
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

        private void CollectDataForISAAccountSetup()
        {
            if (rbtnYes.Checked)
                customerISAAccountsVo.IsJointHolding = true;
            else
                customerISAAccountsVo.IsJointHolding = false;

            customerISAAccountsVo.ModeOfHolding = ddlModeOfHolding.SelectedValue.ToString();
            if (rbtnPOAYes.Checked)
                customerISAAccountsVo.IsOperatedByPOA = true;
            else
                customerISAAccountsVo.IsOperatedByPOA = false;
                          
           
        }

       
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
