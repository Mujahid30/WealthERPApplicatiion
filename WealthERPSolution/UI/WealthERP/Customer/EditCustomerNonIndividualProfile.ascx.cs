using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoUploads;
using VoCustomerProfiling;
using BoCustomerProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Configuration;
using BoCommon;
using System.Data;
using WealthERP.Base;
using AjaxControlToolkit;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;
namespace WealthERP.Customer
{
    public partial class EditCustomerNonIndividualProfile : System.Web.UI.UserControl
    {
        UserVo userVo = null;
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        string path = "";
        DataTable dtCustomerSubType = new DataTable();
        DataTable dtOccupation = new DataTable();
        DataTable dtNationality = new DataTable();
        DataTable dtState = new DataTable();
        DataTable dtCity = new DataTable();
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        AdvisorVo advisorVo = new AdvisorVo();
        string viewForm = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            customerVo = (CustomerVo)Session["CustomerVo"];
            userVo = (UserVo)Session["userVo"];
            try
            {
                SessionBo.CheckSession();
                if (Session["LinkAction"] != null && Session["LinkAction"].ToString().Trim() == "ViewEditProfile")
                {
                    Session["action"] = "View";
                    viewForm = Session["action"].ToString();
                    SetControlstate(viewForm);
                    rbtnIndividual.Visible = false;
                }
                else
                {
                    rbtnIndividual.Visible = true;
                }
                if (Session["LinkAction"] != null && Session["LinkAction"].ToString().Trim() == "AddEditProfile")
                {
                    
                    Session["action"] = "Edit";
                    rbtnIndividual.Visible = false;
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
                    BindDropDowns(path);
                    //Bind Adviser Branch List
                    BindListBranch(customerVo.RmId, "rm");
                    userVo = (UserVo)Session["userVo"];
                    if (customerVo.Type.ToUpper().ToString() == "IND")
                    {

                        rbtnIndividual.Checked = true;
                        trSalutation.Visible = true;
                    }
                    else
                    {
                        rbtnNonIndividual.Checked = true;
                        trSalutation.Visible = false;
                        BinSubtypeDropdown(1002);
                    }
                    if (!string.IsNullOrEmpty(ddlCustomerSubType.SelectedValue))
                    {
                        ddlCustomerSubType.SelectedValue = customerVo.TaxStatusCustomerSubTypeId.ToString();
                    }
                    if (customerVo != null)
                    {

                        if (customerVo.ProfilingDate == DateTime.MinValue)
                        {
                            txtDateofProfiling.Text = DateTime.Today.ToShortDateString();
                        }
                        else
                            txtDateofProfiling.Text = customerVo.ProfilingDate.ToShortDateString();
                        ddlAdviserBranchList.SelectedValue = customerVo.BranchId.ToString();
                        if (!string.IsNullOrEmpty(customerVo.ContactFirstName))
                        {
                            txtFirstName.Text = customerVo.ContactFirstName;
                        }
                        if (!string.IsNullOrEmpty(customerVo.ContactMiddleName))
                        {
                            txtMiddleName.Text = customerVo.ContactMiddleName;
                        }
                        if (!string.IsNullOrEmpty(customerVo.ContactLastName))
                        {
                            txtLastName.Text = customerVo.ContactLastName;
                        }
                        if (!string.IsNullOrEmpty(customerVo.CompanyWebsite))
                        {
                            txtCompanyWebsite.Text = customerVo.CompanyWebsite.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.RegistrationPlace))
                        {
                            txtRegistrationPlace.Text = customerVo.RegistrationPlace.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.RegistrationNum))
                        {
                            txtRocRegistration.Text = customerVo.RegistrationNum.ToString();
                        }
                        if (customerVo.RegistrationDate == DateTime.MinValue)
                        {
                            txtDateofRegistration.Text = "";
                        }
                        else
                            txtDateofRegistration.Text = customerVo.RegistrationDate.ToShortDateString();

                        if (customerVo.CommencementDate == DateTime.MinValue)
                        {
                            txtDateofCommencement.Text = "";
                        }
                        else
                            txtDateofCommencement.Text = customerVo.CommencementDate.ToShortDateString();
                        if (!string.IsNullOrEmpty(customerVo.CompanyName))
                        {
                            txtCompanyName.Text = customerVo.CompanyName;
                        }
                        if (!string.IsNullOrEmpty(customerVo.CustCode.ToString()))
                        {
                            txtCustomerCode.Text = customerVo.CustCode.ToString();
                        }
                        if (customerVo.PANNum == null)
                        {
                            customerVo.PANNum = null;
                        }
                        else
                        {
                            txtPanNumber.Text = customerVo.PANNum.ToString();
                        }
                        //txtRmName.Text = customerVo.RmId.ToString();
                        if (!string.IsNullOrEmpty(customerVo.Adr1Line1))
                        {
                            txtCorrAdrLine1.Text = customerVo.Adr1Line1.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.Adr1Line2))
                        {
                            txtCorrAdrLine2.Text = customerVo.Adr1Line2.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.Adr1Line3))
                        {
                            txtCorrAdrLine3.Text = customerVo.Adr1Line3.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.Adr1PinCode.ToString()))
                        {
                            txtCorrAdrPinCode.Text = customerVo.Adr1PinCode.ToString();
                        }
                        if (customerVo.Adr1State != "")
                        {
                            ddlCorrAdrState.SelectedValue = customerVo.Adr1State;
                        }
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
                        //    chkprospectn.Checked = true;
                        //}
                        //else
                        //{
                        //    chkprospectn.Checked = false;
                        //}
                        if (customerVo.ViaSMS == 1)
                        {
                            chksmsn.Checked = true;
                        }
                        else
                        {
                            chksmsn.Checked = false;
                        }
                        if (customerVo.AlertViaEmail == 1)
                        {
                            chkmailn.Checked = true;
                        }
                        else
                        {
                            chkmailn.Checked = false;
                        }
                        if (!string.IsNullOrEmpty(customerVo.Adr1Country))
                        {
                            ddlCorrAdrCountry.SelectedItem.Value = customerVo.Adr1Country.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.Adr2Line1))
                        {
                            txtPermAdrLine1.Text = customerVo.Adr2Line1.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.Adr2Line2))
                        {
                            txtPermAdrLine2.Text = customerVo.Adr2Line2.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.Adr2Line3))
                        {
                            txtPermAdrLine3.Text = customerVo.Adr2Line3.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.Adr2PinCode.ToString()))
                        {
                            txtPermAdrPinCode.Text = customerVo.Adr2PinCode.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.Adr2State))
                        {
                            if (customerVo.Adr2State.ToString() != "")
                            {
                                ddlPermAdrState.SelectedValue = customerVo.Adr2State;
                            }
                        }
                        if (!string.IsNullOrEmpty(customerVo.Adr2Country))
                        {
                            ddlPermAdrCountry.SelectedItem.Value = customerVo.Adr2Country.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.ResISDCode.ToString()))
                        {
                            txtPhoneNo1Isd.Text = customerVo.ResISDCode.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.ResSTDCode.ToString()))
                        {
                            txtPhoneNo1Std.Text = customerVo.ResSTDCode.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.ResPhoneNum.ToString()))
                        {
                            txtPhoneNo1.Text = customerVo.ResPhoneNum.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.OfcISDCode.ToString()))
                        {
                            txtPhoneNo2Isd.Text = customerVo.OfcISDCode.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.OfcSTDCode.ToString()))
                        {
                            txtPhoneNo2Std.Text = customerVo.OfcSTDCode.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.OfcPhoneNum.ToString()))
                        {
                            txtPhoneNo2.Text = customerVo.OfcPhoneNum.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.Fax.ToString()))
                        {
                            txtFax.Text = customerVo.Fax.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.ISDFax.ToString()))
                        {
                            txtFaxIsd.Text = customerVo.ISDFax.ToString();
                        }
                        if (!string.IsNullOrEmpty(customerVo.STDFax.ToString()))
                        {
                            txtFaxStd.Text = customerVo.STDFax.ToString();
                        }
                         if (customerVo.Email != null)
                        {
                            txtEmail.Text = customerVo.Email.ToString();
                        }
                        if (customerVo.AltEmail != null)
                        {
                            txtAltEmail.Text = customerVo.AltEmail.ToString();
                        }
                        if (customerVo.OccupationId != 0)
                            ddlOccupation.SelectedValue = customerVo.OccupationId.ToString();
                        if (customerVo.Nationality != null)
                            ddlNationality.SelectedValue = customerVo.Nationality.ToString();
                        txtAnnualIncome.Text = customerVo.AnnualIncome.ToString();
                        txtMinNo1.Text = customerVo.MinNo1;
                        txtMinNo2.Text = customerVo.MinNo2;
                        txtMinNo3.Text = customerVo.MinNo3;
                        txtESCNo.Text = customerVo.ESCNo;
                        txtUINNo.Text = customerVo.UINNo;
                        txtGuardianName.Text = customerVo.GuardianName;
                        txtGuardianRelation.Text = customerVo.GuardianRelation;
                        txtContactGuardianPANNum.Text = customerVo.ContactGuardianPANNum;
                        txtPOA.Text = customerVo.POA.ToString();
                        if (customerVo.GuardianDob == DateTime.MinValue)
                            txtGuardianDOB.SelectedDate = null;
                        else
                            txtGuardianDOB.SelectedDate = customerVo.GuardianDob;
                        txtGuardianMinNo.Text = customerVo.GuardianMinNo;
                        txtOtherBankName.Text = customerVo.OtherBankName;
                        txtTaxStatus.Text = customerVo.TaxStatus;
                        txtCategory.Text = customerVo.Category;
                        txtAdr1City.Text = customerVo.Adr1City;
                        txtAdr1State.Text = customerVo.Adr1State;
                        txtOtherCountry.Text = customerVo.OtherCountry;
                        txtMobile1.Text = customerVo.Mobile1.ToString();
                        txtMobile2.Text = customerVo.Mobile2.ToString();
                        if (customerVo.Dob == DateTime.MinValue)
                            txtDate.SelectedDate = null;
                        else
                            txtDate.SelectedDate = customerVo.Dob;
                        txtSubBroker.Text = customerVo.SubBroker;
                        txtMotherMaidenName.Text = customerVo.MothersMaidenName;
                        if (customerVo.Adr1City != null)
                            ddlCorrAdrCity.SelectedValue = customerVo.customerCity.ToString();
                        else

                            ddlCorrAdrCity.SelectedValue = "--Select---";
                        if (customerVo.PermanentCityId != null)
                            ddlPermAdrCity.SelectedValue = customerVo.PermanentCityId.ToString();
                        else
                            ddlPermAdrCity.SelectedValue = "--Select---";
                        RadTabStripCustomerProfile.TabIndex = 0;
                        CustomerProfileDetails.SelectedIndex = 0;
                        RadTabStripCustomerProfile.Tabs[0].Selected = true;
                        
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
                FunctionInfo.Add("Method", "EditCustomerNonIndividualProfile.ascx:Page_Load()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = userVo;
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
                btnEdit.Visible = false;
             }
            else if (action == "Edit")
            {
                btnEdit.Visible = true;
            }
        }
        private void BindDropDowns(string path)
        {
            AdvisorVo advisorVo = new AdvisorVo();
            try
            {
                dtState = commonLookupBo.GetWERPLookupMasterValueList(14000, 0);

                ddlCorrAdrState.DataSource = dtState;
                ddlCorrAdrState.DataTextField = "WCMV_Name";
                ddlCorrAdrState.DataValueField = "WCMV_LookupId";
                ddlCorrAdrState.DataBind();
                ddlCorrAdrState.Items.Insert(0, new ListItem("--SELECT--", "0"));

                ddlPermAdrState.DataSource = dtState;
                ddlPermAdrState.DataTextField = "WCMV_Name";
                ddlPermAdrState.DataValueField = "WCMV_LookupId";
                ddlPermAdrState.DataBind();
                ddlPermAdrState.Items.Insert(0, new ListItem("--SELECT--", "0"));

                dtOccupation = commonLookupBo.GetWERPLookupMasterValueList(3000, 0); 
                ddlOccupation.DataSource = dtOccupation;
                ddlOccupation.DataTextField = "WCMV_Name";
                ddlOccupation.DataValueField = "WCMV_LookupId";
                ddlOccupation.DataBind();
                ddlOccupation.Items.Insert(0, new ListItem("--SELECT--", "0"));

                dtNationality = XMLBo.GetNationality(path);
                ddlNationality.DataSource = dtNationality;
                ddlNationality.DataTextField = "Nationality";
                ddlNationality.DataValueField = "NationalityCode";
                ddlNationality.DataBind();
                ddlNationality.Items.Insert(0, new ListItem("Select a Nationality", "Select a Nationality"));
               
                dtCity = commonLookupBo.GetWERPLookupMasterValueList(8000, 0);

                ddlCorrAdrCity.DataSource = dtCity;
                ddlCorrAdrCity.DataTextField = "WCMV_Name";
                ddlCorrAdrCity.DataValueField = "WCMV_LookupId";
                ddlCorrAdrCity.DataBind();
                ddlCorrAdrCity.Items.Insert(0, new ListItem("--SELECT--", "0"));

                ddlPermAdrCity.DataSource = dtCity;
                ddlPermAdrCity.DataTextField = "WCMV_Name";
                ddlPermAdrCity.DataValueField = "WCMV_LookupId";
                ddlPermAdrCity.DataBind();
                ddlPermAdrCity.Items.Insert(0, new ListItem("--SELECT--", "0"));


                if (customerVo.Type == null || customerVo.Type.ToUpper().ToString() == "IND")
                {
                    dtCustomerSubType = XMLBo.GetCustomerSubType(path, "IND");

                }
                else
                {
                    dtCustomerSubType = XMLBo.GetCustomerSubType(path, "NIND");
                }
                //ddlCustomerSubType.DataSource = dtCustomerSubType;
                //ddlCustomerSubType.DataTextField = "CustomerTypeName";
                //ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
                //ddlCustomerSubType.DataBind();
                //ddlCustomerSubType.SelectedValue = customerVo.SubType;

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

        protected void RadTabStripCustomerProfile_onClick(object sender, EventArgs e)
        {
 
        }
        private void BinSubtypeDropdown(int parentId)
        {
            DataTable dt = new DataTable();
            // dtOccupation = commonLookupBo.GetWERPLookupMasterValueList(2000, 1002);
            dt = commonLookupBo.GetWERPLookupMasterValueList(2000, parentId);
            ddlCustomerSubType.Items.Clear();
            //  dtCustomerSubType = XMLBo.GetCustomerSubType(path, "NIND");
            // ddlCustomerSubType.DataSource = dtCustomerSubType;
            ddlCustomerSubType.DataSource = dt;
            ddlCustomerSubType.DataTextField = "WCMV_Name";
            ddlCustomerSubType.DataValueField = "WCMV_LookupId";
            ddlCustomerSubType.DataBind();
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbtnIndividual.Checked)
                {
                    customerVo.Type = "IND";

                }
                else
                {
                    customerVo.Type = "NIND";

                    customerVo.FirstName = txtCompanyName.Text;
                    customerVo.MiddleName = "";
                    customerVo.LastName = "";
                }
                customerVo.BranchId = int.Parse(ddlAdviserBranchList.SelectedValue.ToString());
                customerVo.TaxStatusCustomerSubTypeId = Int16.Parse(ddlCustomerSubType.SelectedItem.Value.ToString());
                customerVo.ContactFirstName = txtFirstName.Text.ToString();
                customerVo.ContactMiddleName = txtMiddleName.Text.ToString();
                customerVo.ContactLastName = txtLastName.Text.ToString();
                customerVo.CompanyName = txtCompanyName.Text.ToString();
                customerVo.CustCode = txtCustomerCode.Text.ToString();
                //customerVo.Salutation = ddlSalutation.SelectedItem.Value.ToString();

                //if (customerVo.Salutation == "Mr.")
                //{
                //    customerVo.Gender = "M";
                //}
                //else
                //{
                //    customerVo.Gender = "F";
                //}
                if (txtDateofRegistration.Text == "")
                {
                    customerVo.RegistrationDate = DateTime.MinValue;
                }
                else
                {
                    customerVo.RegistrationDate = DateTime.Parse(txtDateofRegistration.Text.ToString());
                }
                if (txtDateofCommencement.Text == "")
                {
                    customerVo.CommencementDate = DateTime.MinValue;
                }
                else
                {
                    customerVo.CommencementDate = DateTime.Parse(txtDateofCommencement.Text.ToString());
                }
                if (txtDateofProfiling.Text != "")
                {
                    customerVo.ProfilingDate = DateTime.Parse(txtDateofProfiling.Text.ToString());
                }
                else
                {
                    customerVo.ProfilingDate = DateTime.Today;
                }
                customerVo.RegistrationPlace = txtRegistrationPlace.Text.ToString();
                customerVo.CompanyWebsite = txtCompanyWebsite.Text.ToString();
                customerVo.PANNum = txtPanNumber.Text.ToString();
                customerVo.RegistrationNum = txtRocRegistration.Text.ToString();
                customerVo.Adr1Line1 = txtCorrAdrLine1.Text.ToString();
                customerVo.Adr1Line2 = txtCorrAdrLine2.Text.ToString();
                customerVo.Adr1Line3 = txtCorrAdrLine3.Text.ToString();
                if (txtCorrAdrPinCode.Text != "")
                {
                    customerVo.Adr1PinCode = int.Parse(txtCorrAdrPinCode.Text.ToString());
                }
                else
                {
                    customerVo.Adr1PinCode = 0;
                }
                if (ddlCorrAdrState.SelectedIndex != 0)
                {
                    customerVo.Adr1State = ddlCorrAdrState.SelectedItem.Value.ToString();
                }
                customerVo.Adr1Country = ddlCorrAdrCountry.SelectedItem.Value.ToString();
                customerVo.Adr2Line1 = txtPermAdrLine1.Text.ToString();
                customerVo.Adr2Line2 = txtPermAdrLine2.Text.ToString();
                customerVo.Adr2Line3 = txtPermAdrLine3.Text.ToString();
                if (chkdummypan.Checked)
                {
                    customerVo.DummyPAN = 1;
                }
                else
                {
                    customerVo.DummyPAN = 0;
                }
                //if (chkprospectn.Checked)
                //{
                //    customerVo.IsProspect = 1;
                //}
                //else
                //{
                //    customerVo.IsProspect = 0;
                //}
                if (chkmailn.Checked)
                {
                    customerVo.AlertViaEmail = 1;
                }
                else
                {
                    customerVo.AlertViaEmail = 0;
                }
                if (chksmsn.Checked)
                {
                    customerVo.ViaSMS = 1;
                }
                else
                {
                    customerVo.ViaSMS = 0;
                }
                if (txtPermAdrPinCode.Text != "")
                {
                    customerVo.Adr2PinCode = int.Parse(txtPermAdrPinCode.Text.ToString());
                }
                else
                {
                    customerVo.Adr2PinCode = 0;
                }
             
                if (ddlPermAdrState.SelectedIndex != 0)
                {
                    customerVo.Adr2State = ddlPermAdrState.SelectedItem.Value.ToString();
                }
                customerVo.Adr2Country = ddlPermAdrCountry.Text.ToString();
                if (txtPhoneNo1Isd.Text != "")
                {
                    customerVo.ResISDCode = Int64.Parse(txtPhoneNo1Isd.Text.ToString());
                }
                else
                {
                    customerVo.ResISDCode = 0;
                }
                if (txtPhoneNo1Std.Text != "")
                {
                    customerVo.ResSTDCode = Int64.Parse(txtPhoneNo1Std.Text.ToString());
                }
                else
                {
                    customerVo.ResSTDCode = 0;
                }
                if (txtPhoneNo1.Text != "")
                {
                    customerVo.ResPhoneNum = Int64.Parse(txtPhoneNo1.Text.ToString());
                }
                else
                {
                    customerVo.ResPhoneNum = 0;
                }
                if (txtPhoneNo2Isd.Text != "")
                {
                    customerVo.OfcISDCode = Int64.Parse(txtPhoneNo2Isd.Text.ToString());
                }
                else
                {
                    customerVo.OfcISDCode = 0;
                }
                if (txtPhoneNo2Std.Text != "")
                {
                    customerVo.OfcSTDCode = Int64.Parse(txtPhoneNo2Std.Text.ToString());
                }
                else
                {
                    customerVo.OfcSTDCode = 0;
                }
                if (txtPhoneNo2.Text != "")
                {
                    customerVo.OfcPhoneNum = Int64.Parse(txtPhoneNo2.Text.ToString());
                }
                else
                {
                    customerVo.OfcPhoneNum = 0;
                }
                if (txtFax.Text != "")
                {
                    customerVo.Fax = int.Parse(txtFax.Text.ToString());
                }
                else
                {
                    customerVo.Fax = 0;
                }
                if (txtFaxIsd.Text != "")
                {
                    customerVo.ISDFax = int.Parse(txtFaxIsd.Text.ToString());
                }
                else
                {
                    customerVo.ISDFax = 0;
                }
                if (txtFaxStd.Text != "")
                {
                    customerVo.STDFax = int.Parse(txtFaxStd.Text.ToString());
                }
                else
                {
                    customerVo.STDFax = 0;
                }
                if (txtEmail.Text != "")
                {
                    customerVo.Email = txtEmail.Text.ToString();
                }
                customerVo.AltEmail = txtAltEmail.Text;
                customerVo.OfcFax = 0;
                customerVo.OfcISDFax = 0;
                customerVo.OfcSTDFax = 0;
                customerVo.OfcFax = 0;
                customerVo.OfcAdrPinCode = 0;
                customerVo.Dob = DateTime.MinValue;
                customerVo.MaritalStatus = null;
                customerVo.Nationality = null;
                customerVo.Occupation = null;
                customerVo.Qualification = null;
                customerVo.OccupationId = int.Parse(ddlOccupation.SelectedItem.Value.ToString());
                customerVo.OtherCountry = txtOtherCountry.Text.ToString();
                if (ddlNationality.SelectedIndex == 0)
                    customerVo.Nationality = null;
                else
                    customerVo.Nationality = ddlNationality.SelectedItem.Value.ToString();
                if (txtAnnualIncome.Text != "")
                    customerVo.AnnualIncome = decimal.Parse(txtAnnualIncome.Text);
                if (txtMinNo1.Text != "")
                {
                    customerVo.MinNo1 = txtMinNo1.Text.ToString();
                }
                if (txtMinNo2.Text != "")
                {
                    customerVo.MinNo2 = txtMinNo2.Text.ToString();
                }
                if (txtMinNo3.Text != "")
                {
                    customerVo.MinNo3 = txtMinNo3.Text.ToString();
                }
                if (txtESCNo.Text != "")
                {
                    customerVo.ESCNo = txtESCNo.Text.ToString();
                }
                if (txtUINNo.Text != "")
                {
                    customerVo.UINNo = txtUINNo.Text.ToString();
                }
                if (txtTaxStatus.Text != "")
                {
                    customerVo.TaxStatus = txtTaxStatus.Text.ToString();
                }
                if (txtCategory.Text != "")
                {
                    customerVo.Category = txtCategory.Text.ToString();
                }
                if (txtGuardianName.Text != "")
                {
                    customerVo.GuardianName = txtGuardianName.Text.ToString();
                }
                if (txtGuardianRelation.Text != "")
                {
                    customerVo.GuardianRelation = txtGuardianRelation.Text.ToString();
                }
                if (txtContactGuardianPANNum.Text != "")
                {
                    customerVo.ContactGuardianPANNum = txtContactGuardianPANNum.Text.ToString();
                }
                if (txtGuardianMinNo.Text != "")
                {
                    customerVo.GuardianMinNo = txtGuardianMinNo.Text.ToString();
                }
                if (txtGuardianDOB.SelectedDate.ToString() == "")
                    customerVo.GuardianDob = DateTime.MinValue;
                else
                    customerVo.GuardianDob = DateTime.Parse(txtGuardianDOB.SelectedDate.ToString());
                if (txtPOA.Text != "")
                    customerVo.POA = int.Parse(txtPOA.Text);
                if (txtOtherBankName.Text != "")
                {
                    customerVo.OtherBankName = txtOtherBankName.Text.ToString();
                }
                if (txtMobile1.Text == "")
                    customerVo.Mobile1 = 0;
                else
                    customerVo.Mobile1 = long.Parse(txtMobile1.Text.ToString());

                if (txtMobile2.Text == "")
                    customerVo.Mobile2 = 0;
                else
                    customerVo.Mobile2 = long.Parse(txtMobile2.Text.ToString());
                if (txtSubBroker.Text != "")
                {
                    customerVo.SubBroker = txtSubBroker.Text.ToString();
                }
                else
                {
                    customerVo.SubBroker =null;
                }
               
                if (txtDate.SelectedDate.ToString() == "")
                    customerVo.Dob = DateTime.MinValue;
                else
                    customerVo.Dob = DateTime.Parse(txtDate.SelectedDate.ToString());
                customerVo.MothersMaidenName = txtMotherMaidenName.Text.ToString();
                customerVo.PermanentCityId = int.Parse(ddlPermAdrCity.SelectedValue);
                customerVo.CorrespondenceCityId = int.Parse(ddlCorrAdrCity.SelectedValue);
                if (customerBo.UpdateCustomer(customerVo, userVo.UserId))
                {
                    customerVo = customerBo.GetCustomer(customerVo.CustomerId);
                    Session["CustomerVo"] = customerVo;
                    //ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "Message", "alert('Profile updated Succesfully');", true);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseThePopUp", " CloseWindowsPopUp();", true);
                    if (customerVo.Type.ToUpper().ToString() == "NIND")
                    {
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewCustomerIndividualProfile','none');", true);
                        viewForm = "View";
                        SetControlstate(viewForm);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewCustomerIndividualProfile','none');", true);
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
                FunctionInfo.Add("Method", "EditCustomerNonIndividualProfile.ascx:btnEdit_Click()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void rbtnIndividual_CheckedChanged(object sender, EventArgs e)
        {
            BinSubtypeDropdown(1001);

            //ddlCustomerSubType.DataSource = null;
            //dtCustomerSubType = XMLBo.GetCustomerSubType(path, "IND");
            //ddlCustomerSubType.DataSource = dtCustomerSubType;
            //ddlCustomerSubType.DataTextField = "CustomerTypeName";
            //ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
            //ddlCustomerSubType.DataBind();
            if (customerVo != null)
            {
                customerVo.Type = "IND";
                Session[SessionContents.CustomerVo] = customerVo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PageLoadScript", "loadcontrol('EditCustomerIndividualProfile','none');", true);
            }
        }

        protected void rbtnNonIndividual_CheckedChanged(object sender, EventArgs e)
        {
            BinSubtypeDropdown(1002);

            //dtCustomerSubType = XMLBo.GetCustomerSubType(path, "NIND");
            //ddlCustomerSubType.DataSource = dtCustomerSubType;
            //ddlCustomerSubType.DataTextField = "CustomerTypeName";
            //ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
            //ddlCustomerSubType.DataBind();
            if (customerVo != null)
            {
                Session[SessionContents.CustomerVo] = customerVo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PageLoadScript", "loadcontrol('EditCustomerNonIndividualProfile','none');", true);
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

        protected void txtCorrAdrLine1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
