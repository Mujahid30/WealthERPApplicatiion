using System;
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

namespace WealthERP.Customer
{
    public partial class EditCustomerIndividualProfile : System.Web.UI.UserControl
    {
        UserVo userVo = null;
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorStaffBo adviserStaffBo = new AdvisorStaffBo();
        string path;
        DataTable dtMaritalStatus = new DataTable();
        DataTable dtNationality = new DataTable();
        DataTable dtOccupation = new DataTable();
        DataTable dtQualification = new DataTable();
        DataTable dtState = new DataTable();
        DataTable dtCustomerSubType = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            cvDepositDate1.ValueToCompare = DateTime.Now.ToShortDateString();
            txtLivingSince_CompareValidator.ValueToCompare = DateTime.Now.ToShortDateString();
            cvJobStartDate.ValueToCompare = DateTime.Now.ToShortDateString();
            txtMarriageDate_CompareValidator.ValueToCompare = DateTime.Now.ToShortDateString();

            try
            {
                SessionBo.CheckSession();
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                RMVo customerRMVo = new RMVo();
                if (!IsPostBack)
                {
                    lblPanDuplicate.Visible = false;
                   
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
                        txtMarriageDate.Text = "";
                    else
                        txtMarriageDate.Text = customerVo.MarriageDate.ToShortDateString();
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
                        txtDob.Text = "";
                    else
                        txtDob.Text = customerVo.Dob.ToShortDateString();
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
                    if (customerVo.IsProspect == 1)
                    {
                        chkprospect.Checked = true;
                    }
                    else
                    {
                        chkprospect.Checked = false;
                    }
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
                    txtFirstName.Text = customerVo.FirstName.ToString();
                    txtMiddleName.Text = customerVo.MiddleName.ToString();
                    txtLastName.Text = customerVo.LastName.ToString();
                    txtCustomerCode.Text = customerVo.CustCode.ToString();
                    txtPanNumber.Text = customerVo.PANNum.ToString();

                    txtCorrAdrLine1.Text = customerVo.Adr1Line1.ToString();
                    txtCorrAdrLine2.Text = customerVo.Adr1Line2.ToString();
                    txtCorrAdrLine3.Text = customerVo.Adr1Line3.ToString();
                    txtCorrAdrPinCode.Text = customerVo.Adr1PinCode.ToString();
                    txtCorrAdrCity.Text = customerVo.Adr1City.ToString();
                    ddlCorrAdrState.SelectedValue = customerVo.Adr1State.ToString();
                    txtCorrAdrCountry.Text = customerVo.Adr1Country.ToString();
                    txtPermAdrLine1.Text = customerVo.Adr2Line1.ToString();
                    txtPermAdrLine2.Text = customerVo.Adr2Line2.ToString();
                    txtPermAdrLine3.Text = customerVo.Adr2Line3.ToString();
                    txtPermAdrPinCode.Text = customerVo.Adr2PinCode.ToString();
                    txtPermAdrCity.Text = customerVo.Adr2City.ToString();
                    ddlPermAdrState.SelectedValue = customerVo.Adr2State.ToString();
                    txtPermAdrCountry.Text = customerVo.Adr2Country.ToString();
                    txtOfcCompanyName.Text = customerVo.CompanyName.ToString();
                    txtOfcAdrLine1.Text = customerVo.OfcAdrLine1.ToString();
                    txtOfcAdrLine2.Text = customerVo.OfcAdrLine2.ToString();
                    txtOfcAdrLine3.Text = customerVo.OfcAdrLine3.ToString();
                    txtOfcAdrPinCode.Text = customerVo.OfcAdrPinCode.ToString();
                    txtOfcAdrCity.Text = customerVo.OfcAdrCity.ToString();
                    ddlOfcAdrState.SelectedValue = customerVo.OfcAdrState.ToString();
                    txtOfcAdrCountry.Text = customerVo.OfcAdrCountry.ToString();
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
                    txtEmail.Text = customerVo.Email.ToString();
                    txtAltEmail.Text = customerVo.AltEmail.ToString();
                    txtRBIRefNo.Text = customerVo.RBIRefNum.ToString();
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

                    txtMotherMaidenName.Text = customerVo.MothersMaidenName.ToString();
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
                    customerVo.Salutation = ddlSalutation.SelectedValue;
                    customerVo.FirstName = txtFirstName.Text.ToString();
                    customerVo.MiddleName = txtMiddleName.Text.ToString();
                    customerVo.LastName = txtLastName.Text.ToString();
                    if (rbtnIndividual.Checked)
                    {
                        customerVo.Type = "IND";
                    }
                    else
                        customerVo.Type = "NIND";
                    customerVo.SubType = ddlCustomerSubType.SelectedItem.Value.ToString();

                    if (customerVo.SubType == "MNR")
                    {
                        customerVo.ContactMiddleName = txtGuardianMiddleName.Text;
                        customerVo.ContactLastName = txtGuardianLastName.Text;
                        customerVo.ContactFirstName = txtGuardianFirstName.Text;
                    }

                    if (txtDob.Text == "")
                        customerVo.Dob = DateTime.MinValue;
                    else
                        customerVo.Dob = DateTime.Parse(txtDob.Text);
                    if (txtProfilingDate.Text == "")
                        customerVo.ProfilingDate = DateTime.MinValue;
                    else
                        customerVo.ProfilingDate = DateTime.Parse(txtProfilingDate.Text);
                    customerVo.PANNum = txtPanNumber.Text;

                    customerVo.CustCode = txtCustomerCode.Text;



                    customerVo.Adr1Line1 = txtCorrAdrLine1.Text.ToString();
                    customerVo.Adr1Line2 = txtCorrAdrLine2.Text.ToString();
                    customerVo.Adr1Line3 = txtCorrAdrLine3.Text.ToString();
                    if (txtCorrAdrPinCode.Text == "")
                        customerVo.Adr1PinCode = 0;
                    else
                        customerVo.Adr1PinCode = int.Parse(txtCorrAdrPinCode.Text.ToString());
                    customerVo.Adr1City = txtCorrAdrCity.Text.ToString();
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
                    customerVo.CompanyName = txtOfcCompanyName.Text.ToString();
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
                    if (chkprospect.Checked)
                    {
                        customerVo.IsProspect = 1;
                    }
                    else
                    {
                        customerVo.IsProspect = 0;
                    }
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

                    if (txtMarriageDate.Text != string.Empty && txtMarriageDate.Text != "dd/mm/yyyy")
                    {
                        customerVo.MarriageDate = DateTime.Parse(txtMarriageDate.Text);
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
        }
    }
}
