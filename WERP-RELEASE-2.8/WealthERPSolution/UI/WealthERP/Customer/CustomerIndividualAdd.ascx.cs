using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using VoUser;
using VoCustomerProfiling;
using BoCustomerProfiling;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoUser;
using BoCommon;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoAdvisorProfiling;

namespace WealthERP.Customer
{
    public partial class BasicIndividualProfile : System.Web.UI.UserControl
    {
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerBo customerBo = new CustomerBo();
        UserBo userBo = new UserBo();

        DataTable dtMaritalStatus = new DataTable();
        DataTable dtNationality = new DataTable();
        DataTable dtOccupation = new DataTable();
        DataTable dtQualification = new DataTable();
        DataTable dtStates = new DataTable();

        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        CustomerVo customerVo = new CustomerVo();
        UserVo tempUserVo = new UserVo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        int rmId;
        //int userId;
        string path;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["UserVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            txtLivingSince_CompareValidator.ValueToCompare = DateTime.Now.ToShortDateString();
            txtMarriageDate_CompareValidator.ValueToCompare = DateTime.Now.ToShortDateString();
            cvJobStartDate.ValueToCompare = DateTime.Now.ToShortDateString();

            try
            {
                this.Page.Culture = "en-GB";
                rmVo = (RMVo)Session["rmVo"];

                customerVo = (CustomerVo)Session["CustomerVo"];
                if (Request.QueryString["RmId"] != null)
                {
                    rmId = int.Parse(Request.QueryString["RmId"].ToString());

                    rmVo = advisorStaffBo.GetAdvisorStaffDetails(customerVo.RmId);
                }
                else
                {
                    rmId = rmVo.RMId;
                }
                if (!IsPostBack)
                {
                    if (customerVo.SubType != "NRI")
                    {
                        txtRBIApprovalDate.Visible = false;
                        txtRBIRefNo.Visible = false;
                        lblRBIApprovalDate.Visible = false;
                        lblRBIRefNum.Visible = false;
                        trGuardianName.Visible = true;
                    }
                    if (customerVo.SubType == "MNR")
                    {
                        trGuardianName.Visible = true;
                    }
                    else
                    {
                        trGuardianName.Visible = false;
                    }

                   // txtRmName.Text = rmVo.FirstName.ToString() + " " + rmVo.MiddleName.ToString() + " " + rmVo.LastName.ToString();
                    if(!string.IsNullOrEmpty(customerVo.Salutation))
                    ddlSalutation.SelectedValue = customerVo.Salutation;
                    txtFirstName.Text = customerVo.FirstName.ToString();
                    txtMiddleName.Text = customerVo.MiddleName.ToString();
                    txtLastName.Text = customerVo.LastName.ToString();
                    txtEmail.Text = customerVo.Email.ToString();
                    txtPanNumber.Text = customerVo.PANNum;
                    if (customerVo.DummyPAN == 1)
                        chkdummypan.Checked = true;
                    else
                        chkdummypan.Checked = false;

                    txtProfilingDate.Text = DateTime.Today.Date.ToShortDateString().ToString();
                    txtRMName.Text = rmVo.FirstName + " " + rmVo.MiddleName + " " + rmVo.LastName;
                    BindDropDowns(path);
                    
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
                FunctionInfo.Add("Method", "CustomerIndividualAdd.ascx:Page_Load()");
                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = customerVo;
                objects[2] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindDropDowns(string path)
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
            ddlOccupation.Items.Insert(0, new ListItem("Select an Occupation", "Select a Occupation"));

            dtQualification = XMLBo.GetQualification(path);
            ddlQualification.DataSource = dtQualification;
            ddlQualification.DataTextField = "Qualification";
            ddlQualification.DataValueField = "QualificationCode";
            ddlQualification.DataBind();
            ddlQualification.Items.Insert(0, new ListItem("Select a Qualification", "Select a Qualification"));

            dtStates = XMLBo.GetStates(path);
            ddlCorrAdrState.DataSource = dtStates;
            ddlCorrAdrState.DataTextField = "StateName";
            ddlCorrAdrState.DataValueField = "StateCode";
            ddlCorrAdrState.DataBind();
            ddlCorrAdrState.Items.Insert(0, new ListItem("Select a State", "Select a State"));

            ddlPermAdrState.DataSource = dtStates;
            ddlPermAdrState.DataTextField = "StateName";
            ddlPermAdrState.DataValueField = "StateCode";
            ddlPermAdrState.DataBind();
            ddlPermAdrState.Items.Insert(0, new ListItem("Select a State", "Select a State"));

            ddlOfcAdrState.DataSource = dtStates;
            ddlOfcAdrState.DataTextField = "StateName";
            ddlOfcAdrState.DataValueField = "StateCode";
            ddlOfcAdrState.DataBind();
            ddlOfcAdrState.Items.Insert(0, new ListItem("Select a State", "Select a State"));

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    userVo = (UserVo)Session["UserVo"];
                    rmId = rmVo.RMId;

                    customerVo.ProfilingDate = DateTime.Parse(txtProfilingDate.Text.ToString());
                    if (!string.IsNullOrEmpty(customerVo.Salutation))
                    customerVo.Salutation = ddlSalutation.SelectedValue.ToString();
                    
                    customerVo.FirstName = txtFirstName.Text.ToString().Trim();
                    customerVo.MiddleName = txtMiddleName.Text.ToString().Trim();
                    customerVo.LastName = txtLastName.Text.ToString().Trim();
                    customerVo.CustCode = txtCustomerCode.Text.ToString().Trim();
                    if (rbtnMale.Checked)
                    {
                        customerVo.Gender = "M";
                    }
                    if (rbtnFemale.Checked)
                    {
                        customerVo.Gender = "F";
                    }
                    if (chkdummypan.Checked)
                    {
                        customerVo.DummyPAN = 1;
                    }
                    else
                        customerVo.DummyPAN = 0;
                    if (chksms.Checked)
                    {
                        customerVo.ViaSMS = 1;
                    }
                    else
                        customerVo.ViaSMS = 0;
                    if (chkmail.Checked)
                    {
                        customerVo.AlertViaEmail = 1;
                    }
                    else
                        customerVo.AlertViaEmail = 0;

                    if (txtDob.Text.ToString() != "")
                        customerVo.Dob = DateTime.Parse(txtDob.Text.ToString());
                    else
                        customerVo.Dob = DateTime.MinValue;

                    customerVo.PANNum = txtPanNumber.Text.ToString().Trim();
                    customerVo.RmId = rmId;
                    if (chkCorrPerm.Checked)
                    {
                        customerVo.Adr2Line1 = txtCorrAdrLine1.Text.ToString().Trim();
                        customerVo.Adr2Line2 = txtCorrAdrLine2.Text.ToString().Trim();
                        customerVo.Adr2Line3 = txtCorrAdrLine3.Text.ToString().Trim();
                        customerVo.Adr2PinCode = (txtCorrAdrPinCode.Text.ToString().Trim() != "") ? Int32.Parse(txtCorrAdrPinCode.Text.ToString().Trim()) : 0;
                        customerVo.Adr2City = txtCorrAdrCity.Text.ToString().Trim();
                        customerVo.Adr2State = ddlCorrAdrState.SelectedValue.ToString().Trim();
                        customerVo.Adr2Country = ddlCorrAdrCountry.SelectedValue.ToString().Trim();
                    }
                    else
                    {
                        customerVo.Adr2Line1 = txtPermAdrLine1.Text.ToString().Trim();
                        customerVo.Adr2Line2 = txtPermAdrLine2.Text.ToString().Trim();
                        customerVo.Adr2Line3 = txtPermAdrLine3.Text.ToString().Trim();
                        customerVo.Adr2PinCode = (txtPermAdrPinCode.Text.ToString().Trim() != "") ? Int32.Parse(txtPermAdrPinCode.Text.ToString().Trim()) : 0;
                        customerVo.Adr2City = txtPermAdrCity.Text.ToString().Trim();
                        customerVo.Adr2State = ddlPermAdrState.SelectedValue.ToString().Trim();
                        customerVo.Adr2Country = ddlPermAdrCountry.SelectedValue.ToString().Trim();
                    }
                    customerVo.Adr1Line1 = txtCorrAdrLine1.Text.ToString().Trim();
                    customerVo.Adr1Line2 = txtCorrAdrLine2.Text.ToString().Trim();
                    customerVo.Adr1Line3 = txtCorrAdrLine3.Text.ToString().Trim();
                    customerVo.Adr1PinCode = (txtCorrAdrPinCode.Text.ToString().Trim() != "") ? Int32.Parse(txtCorrAdrPinCode.Text.ToString().Trim()) : 0;
                    customerVo.Adr1City = txtCorrAdrCity.Text.ToString().Trim();
                    customerVo.Adr1State = ddlCorrAdrState.SelectedValue.ToString().Trim();
                    customerVo.Adr1Country = ddlCorrAdrCountry.SelectedValue.ToString().Trim();
                    customerVo.OfcAdrLine1 = txtOfcAdrLine1.Text.ToString().Trim();
                    customerVo.OfcAdrLine2 = txtOfcAdrLine2.Text.ToString().Trim();
                    customerVo.OfcAdrLine3 = txtOfcAdrLine3.Text.ToString().Trim();
                    customerVo.OfcAdrPinCode = (txtOfcAdrPinCode.Text.ToString().Trim() != "") ? Int32.Parse(txtOfcAdrPinCode.Text.ToString().Trim()) : 0;
                    customerVo.OfcAdrCity = txtOfcAdrCity.Text.ToString().Trim();
                    customerVo.OfcAdrState = ddlOfcAdrState.SelectedValue.ToString().Trim();
                    customerVo.OfcAdrCountry = ddlOfcAdrCountry.SelectedValue.ToString().Trim();
                    customerVo.CompanyName = txtOfcCompanyName.Text.ToString().Trim();
                    customerVo.ResISDCode = (txtResPhoneNoIsd.Text.ToString().Trim() != "") ? Int32.Parse(txtResPhoneNoIsd.Text.ToString().Trim()) : 0;
                    customerVo.ResSTDCode = (txtResPhoneNoStd.Text.ToString().Trim() != "") ? Int32.Parse(txtResPhoneNoStd.Text.ToString().Trim()) : 0;
                    customerVo.ResPhoneNum = (txtResPhoneNo.Text.ToString().Trim() != "") ? Int32.Parse(txtResPhoneNo.Text.ToString().Trim()) : 0;
                    customerVo.OfcISDCode = (txtOfcPhoneNoIsd.Text.ToString().Trim() != "") ? Int32.Parse(txtOfcPhoneNoIsd.Text.ToString().Trim()) : 0;
                    customerVo.OfcSTDCode = (txtOfcPhoneNoStd.Text.ToString().Trim() != "") ? Int32.Parse(txtOfcPhoneNoStd.Text.ToString().Trim()) : 0;
                    customerVo.OfcPhoneNum = (txtOfcPhoneNo.Text.ToString().Trim() != "") ? Int32.Parse(txtOfcPhoneNo.Text.ToString().Trim()) : 0;
                    customerVo.ISDFax = (txtResFaxIsd.Text.ToString().Trim() != "") ? Int32.Parse(txtResFaxIsd.Text.ToString().Trim()) : 0;
                    customerVo.STDFax = (txtResFaxStd.Text.ToString().Trim() != "") ? Int32.Parse(txtResFaxStd.Text.ToString().Trim()) : 0;
                    customerVo.Fax = (txtResFax.Text.ToString().Trim() != "") ? Int32.Parse(txtResFax.Text.ToString().Trim()) : 0;
                    customerVo.OfcISDFax = (txtOfcFaxIsd.Text.ToString().Trim() != "") ? Int32.Parse(txtOfcFaxIsd.Text.ToString().Trim()) : 0;
                    customerVo.OfcSTDFax = (txtOfcFaxStd.Text.ToString().Trim() != "") ? Int32.Parse(txtOfcFaxStd.Text.ToString().Trim()) : 0;
                    customerVo.OfcFax = (txtOfcFax.Text.ToString().Trim() != "") ? Int32.Parse(txtOfcFax.Text.ToString().Trim()) : 0;
                    customerVo.Mobile1 = (txtMobile1.Text.ToString().Trim() != "") ? long.Parse(txtMobile1.Text.ToString().Trim()) : 0;
                    customerVo.Mobile2 = (txtMobile2.Text.ToString().Trim() != "") ? long.Parse(txtMobile2.Text.ToString().Trim()) : 0;
                    customerVo.Email = txtEmail.Text.ToString().Trim();
                    customerVo.AltEmail = txtAltEmail.Text.ToString().Trim();
                    customerVo.Occupation = ddlOccupation.SelectedValue.ToString().Trim();
                    customerVo.Qualification = ddlQualification.SelectedValue.ToString().Trim();
                    customerVo.Nationality = ddlNationality.SelectedValue.ToString().Trim();
                    customerVo.MaritalStatus = ddlMaritalStatus.SelectedValue.ToString().Trim();
                    if (txtMarriageDate.Text != string.Empty && txtMarriageDate.Text != "dd/mm/yyyy")
                    {
                        customerVo.MarriageDate = DateTime.Parse(txtMarriageDate.Text);
                    }
                    else
                        customerVo.MarriageDate = DateTime.MinValue;
                    customerVo.RBIRefNum = txtRBIRefNo.Text.ToString().Trim();
                    if (txtLivingSince.Text.ToString() != "")
                        customerVo.ResidenceLivingDate = DateTime.Parse(txtLivingSince.Text.ToString());
                    else
                        customerVo.ResidenceLivingDate = DateTime.MinValue;
                    if (txtJobStartDate.Text.ToString() != "")
                        customerVo.JobStartDate = DateTime.Parse(txtJobStartDate.Text.ToString());
                    else
                        customerVo.JobStartDate = DateTime.MinValue;
                    customerVo.MothersMaidenName = txtMotherMaidenName.Text.ToString();
                    if (customerVo.SubType == "NRI")
                    {
                        if (txtRBIApprovalDate.Text.ToString().Trim() != "")
                            customerVo.RBIApprovalDate = DateTime.Parse(txtRBIApprovalDate.Text.ToString().Trim());
                        else
                            customerVo.RBIApprovalDate = DateTime.MinValue;
                    }
                    if (customerVo.SubType == "MNR")
                    {
                        customerVo.ContactFirstName = txtGuardianFirstName.Text;
                        customerVo.ContactMiddleName = txtGuardianMiddleName.Text;
                        customerVo.ContactLastName = txtGuardianLastName.Text;
                    }
                    if (Session["customerIds"] != null)
                    {
                        List<int> customerIds = new List<int>();
                        customerIds = (List<int>)Session["CustomerIds"];
                        customerVo.CustomerId = customerIds[1];
                        customerBo.UpdateCustomer(customerVo);
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AdviserCustomer','none');", true);
                    }
                }
                else
                {
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
                FunctionInfo.Add("Method", "CustomerIndividualAdd.ascx:btnSubmit_Click()");
                object[] objects = new object[4];
                objects[0] = rmVo;
                objects[1] = rmId;
                objects[2] = customerVo;
                objects[3] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnAddBankDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    userVo = (UserVo)Session["UserVo"];
                    rmId = rmVo.RMId;

                    customerVo.ProfilingDate = DateTime.Parse(txtProfilingDate.Text.ToString());
                    if (!string.IsNullOrEmpty(customerVo.Salutation))
                    customerVo.Salutation = ddlSalutation.SelectedItem.Value.ToString();
                    customerVo.FirstName = txtFirstName.Text.ToString();
                    customerVo.MiddleName = txtMiddleName.Text.ToString();
                    customerVo.LastName = txtLastName.Text.ToString();
                    customerVo.CustCode = txtCustomerCode.Text.ToString();
                    if (rbtnMale.Checked)
                    {
                        customerVo.Gender = "M";
                    }
                    else if (rbtnFemale.Checked)
                    {
                        customerVo.Gender = "F";
                    }

                    if (txtDob.Text.ToString().Trim() != "")
                        customerVo.Dob = DateTime.Parse(txtDob.Text.ToString());
                    else
                        customerVo.Dob = DateTime.MinValue;
                    customerVo.PANNum = txtPanNumber.Text.ToString();
                    customerVo.RmId = rmId;

                    if (chkCorrPerm.Checked)
                    {
                        customerVo.Adr2Line1 = txtCorrAdrLine1.Text.ToString();
                        customerVo.Adr2Line2 = txtCorrAdrLine2.Text.ToString();
                        customerVo.Adr2Line3 = txtCorrAdrLine3.Text.ToString();
                        customerVo.Adr2PinCode = (txtCorrAdrPinCode.Text.ToString().Trim() != "") ? Int32.Parse(txtCorrAdrPinCode.Text.ToString().Trim()) : 0;
                        customerVo.Adr2City = txtCorrAdrCity.Text.ToString();
                        customerVo.Adr2State = ddlCorrAdrState.SelectedValue.ToString();
                        customerVo.Adr2Country = ddlCorrAdrCountry.SelectedValue.ToString();
                    }
                    else
                    {
                        customerVo.Adr2Line1 = txtPermAdrLine1.Text.ToString();
                        customerVo.Adr2Line2 = txtPermAdrLine2.Text.ToString();
                        customerVo.Adr2Line3 = txtPermAdrLine3.Text.ToString();
                        customerVo.Adr2PinCode = (txtPermAdrPinCode.Text.ToString().Trim() != "") ? Int32.Parse(txtPermAdrPinCode.Text.ToString().Trim()) : 0;
                        customerVo.Adr2City = txtPermAdrCity.Text.ToString();
                        customerVo.Adr2State = ddlPermAdrState.SelectedValue.ToString();
                        customerVo.Adr2Country = ddlPermAdrCountry.SelectedValue.ToString();
                    }
                    customerVo.Adr1Line1 = txtCorrAdrLine1.Text.ToString();
                    customerVo.Adr1Line2 = txtCorrAdrLine2.Text.ToString();
                    customerVo.Adr1Line3 = txtCorrAdrLine3.Text.ToString();
                    customerVo.Adr1PinCode = (txtCorrAdrPinCode.Text.ToString().Trim() != "") ? Int32.Parse(txtCorrAdrPinCode.Text.ToString().Trim()) : 0;
                    customerVo.Adr1City = txtCorrAdrCity.Text.ToString();
                    customerVo.Adr1State = ddlCorrAdrState.SelectedValue.ToString();
                    customerVo.Adr1Country = ddlCorrAdrCountry.SelectedItem.Value.ToString();
                    customerVo.OfcAdrLine1 = txtOfcAdrLine1.Text.ToString();
                    customerVo.OfcAdrLine2 = txtOfcAdrLine2.Text.ToString();
                    customerVo.OfcAdrLine3 = txtOfcAdrLine3.Text.ToString();
                    customerVo.OfcAdrPinCode = (txtOfcAdrPinCode.Text.ToString().Trim() != "") ? Int32.Parse(txtOfcAdrPinCode.Text.ToString().Trim()) : 0;
                    customerVo.OfcAdrCity = txtOfcAdrCity.Text.ToString();
                    customerVo.OfcAdrState = ddlOfcAdrState.SelectedValue.ToString();
                    customerVo.OfcAdrCountry = ddlOfcAdrCountry.SelectedValue.ToString();
                    customerVo.CompanyName = txtOfcCompanyName.Text.ToString();
                    customerVo.ResISDCode = (txtResPhoneNoIsd.Text.ToString().Trim() != "") ? Int32.Parse(txtResPhoneNoIsd.Text.ToString().Trim()) : 0;
                    customerVo.ResSTDCode = (txtResPhoneNoStd.Text.ToString().Trim() != "") ? Int32.Parse(txtResPhoneNoStd.Text.ToString().Trim()) : 0;
                    customerVo.ResPhoneNum = (txtResPhoneNo.Text.ToString().Trim() != "") ? Int32.Parse(txtResPhoneNo.Text.ToString().Trim()) : 0;
                    customerVo.OfcISDCode = (txtOfcPhoneNoIsd.Text.ToString().Trim() != "") ? Int32.Parse(txtOfcPhoneNoIsd.Text.ToString().Trim()) : 0;
                    customerVo.OfcSTDCode = (txtOfcPhoneNoStd.Text.ToString().Trim() != "") ? Int32.Parse(txtOfcPhoneNoStd.Text.ToString().Trim()) : 0;
                    customerVo.OfcPhoneNum = (txtOfcPhoneNo.Text.ToString().Trim() != "") ? Int32.Parse(txtOfcPhoneNo.Text.ToString().Trim()) : 0;
                    customerVo.ISDFax = (txtResFaxIsd.Text.ToString().Trim() != "") ? Int32.Parse(txtResFaxIsd.Text.ToString().Trim()) : 0;
                    customerVo.STDFax = (txtResFaxStd.Text.ToString().Trim() != "") ? Int32.Parse(txtResFaxStd.Text.ToString().Trim()) : 0;
                    customerVo.Fax = (txtResFax.Text.ToString().Trim() != "") ? Int32.Parse(txtResFax.Text.ToString().Trim()) : 0;
                    customerVo.OfcISDFax = (txtOfcFaxIsd.Text.ToString().Trim() != "") ? Int32.Parse(txtOfcFaxIsd.Text.ToString().Trim()) : 0;
                    customerVo.OfcSTDFax = (txtOfcFaxStd.Text.ToString().Trim() != "") ? Int32.Parse(txtOfcFaxStd.Text.ToString().Trim()) : 0;
                    customerVo.OfcFax = (txtOfcFax.Text.ToString().Trim() != "") ? Int32.Parse(txtOfcFax.Text.ToString().Trim()) : 0;
                    customerVo.Mobile1 = (txtMobile1.Text.ToString().Trim() != "") ? long.Parse(txtMobile1.Text.ToString().Trim()) : 0;
                    customerVo.Mobile2 = (txtMobile2.Text.ToString().Trim() != "") ? long.Parse(txtMobile2.Text.ToString().Trim()) : 0;
                    customerVo.Email = txtEmail.Text.ToString();
                    customerVo.AltEmail = txtAltEmail.Text.ToString();
                    customerVo.Occupation = ddlOccupation.SelectedItem.Value.ToString();
                    customerVo.Qualification = ddlQualification.SelectedItem.Value.ToString();
                    customerVo.Nationality = ddlNationality.SelectedItem.Value.ToString();
                    customerVo.MaritalStatus = ddlMaritalStatus.SelectedItem.Value.ToString();
                    customerVo.RBIRefNum = txtRBIRefNo.Text.ToString();
                    if (customerVo.SubType == "NRI")
                    {
                        if (txtRBIApprovalDate.Text.ToString().Trim() != "")
                            customerVo.RBIApprovalDate = DateTime.Parse(txtRBIApprovalDate.Text.ToString().Trim());
                        else
                            customerVo.RBIApprovalDate = DateTime.MinValue;
                    }

                    customerPortfolioVo.CustomerId = customerVo.CustomerId;
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";

                    if (Session["customerIds"] != null)
                    {
                        List<int> customerIds = new List<int>();
                        customerIds = (List<int>)Session["CustomerIds"];
                        customerVo.CustomerId = customerIds[1];
                        customerBo.UpdateCustomer(customerVo);
                    }
                    Session["Check"] = "CustomerAdd";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AddBankDetails','none');", true);
                }
                else
                {

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
                FunctionInfo.Add("Method", "CustomerIndividualAdd.ascx:btnAddBankDetails_Click()");
                object[] objects = new object[4];
                objects[0] = rmVo;
                objects[2] = customerVo;
                objects[3] = customerPortfolioVo;
                objects[4] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public bool chkAvailability()
        {
            bool result = false;
            string id;
            try
            {
                id = txtEmail.Text;
                result = userBo.ChkAvailability(id);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerIndividualAdd.ascx:chkAvailability()");
                object[] objects = new object[1];
                objects[0] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public bool IsInteger(string value)
        {
            foreach (char c in value.ToCharArray())
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        public bool Validation()
        {
            bool result = true;

            //if (!ChkMailId(txtEmail.Text.ToString()))
            //{
            //    result = false;
            //    lblEmail.CssClass = "Error";
            //}

            //if (txtLastName.Text.ToString() == "")
            //{
            //    lblName.CssClass = "Error";
            //    result = false;
            //}
            //else
            //{
            //    lblName.CssClass = "FieldName";
            //    result = true;
            //}

            if (txtPanNumber.Text.ToString() == "")
            {
                lblPanNum.CssClass = "Error";
                result = false;
            }
            else
            {
                lblPanNum.CssClass = "FieldName";
                result = true;
            }
            //if (txtCorrAdrLine1.Text.ToString() == "")
            //{
            //    lblAdrLine1.CssClass = "Error";
            //    result = false;
            //}
            //else
            //{
            //    lblAdrLine1.CssClass = "FieldName";
            //    result = true;
            //}

            if (txtOfcFax.Text.ToString() == "")
            {
                txtOfcFax.Text = "0";
            }
            if (txtOfcFaxIsd.Text.ToString() == "")
            {
                txtOfcFaxIsd.Text = "0";
            }
            if (txtOfcFaxStd.Text.ToString() == "")
            {
                txtOfcFaxStd.Text = "0";
            }
            if (txtResFax.Text.ToString() == "")
            {
                txtResFax.Text = "0";
            }
            if (txtResFaxIsd.Text.ToString() == "")
            {
                txtResFaxIsd.Text = "0";
            }
            if (txtResFaxStd.Text.ToString() == "")
            {
                txtResFaxStd.Text = "0";
            }
            if (txtOfcPhoneNo.Text.ToString() == "")
            {
                txtOfcPhoneNo.Text = "0";
            }
            if (txtOfcPhoneNoIsd.Text.ToString() == "")
            {
                txtOfcPhoneNoIsd.Text = "0";
            }
            if (txtOfcPhoneNoStd.Text.ToString() == "")
            {
                txtOfcPhoneNoStd.Text = "0";
            }
            if (txtPermAdrPinCode.Text == "")
            {
                txtPermAdrPinCode.Text = "0";
            }
            if (txtOfcAdrPinCode.Text.ToString() == "")
            {
                txtOfcAdrPinCode.Text = "0";
            }
            if (txtMobile1.Text.ToString() == "")
            {
                txtMobile1.Text = "0";
            }
            if (txtMobile2.Text.ToString() == "")
            {
                txtMobile2.Text = "0";
            }

            return result;
        }

        public bool ChkMailId(string email)
        {
            bool bResult = false;
            try
            {
                if (email == null)
                {
                    bResult = false;
                }
                int nFirstAT = email.IndexOf('@');
                int nLastAT = email.LastIndexOf('@');

                if ((nFirstAT > 0) && (nLastAT == nFirstAT) && (nFirstAT < (email.Length - 1)))
                {
                    bResult = true;
                }
                else
                {
                    bResult = false;
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

                FunctionInfo.Add("Method", "CustomerIndividualAdd.ascx:ChkMailId()");


                object[] objects = new object[1];
                objects[0] = email;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        protected void btnCorrNext_Click(object sender, EventArgs e)
        {
            string TabberScript = "<script language='javascript'>document.getElementById('divPermAdr').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);
        }

        protected void btnPermNext_Click(object sender, EventArgs e)
        {
            string TabberScript = "<script language='javascript'>document.getElementById('divOfcAdr').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);
        }

        protected void btnOfcNext_Click(object sender, EventArgs e)
        {
            string TabberScript = "<script language='javascript'>document.getElementById('divContactDetails').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);
        }

        protected void btnContactNext_Click(object sender, EventArgs e)
        {
            string TabberScript = "<script language='javascript'>document.getElementById('divAddInfo').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);
        }

    }








}
