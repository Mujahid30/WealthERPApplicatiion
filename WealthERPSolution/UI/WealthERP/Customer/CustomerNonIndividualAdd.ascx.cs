using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoCustomerProfiling;
using VoUser;
using BoUser;
using BoCustomerProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Configuration;
using System.Data;
using BoCommon;
using VoCustomerPortfolio;
using BoAdvisorProfiling;

namespace WealthERP.Customer
{
    public partial class BasicNonIndividualProfile : System.Web.UI.UserControl
    {
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        UserBo userBo = new UserBo();
        int rmId;
        string path;
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        CustomerFamilyVo customerFamilyVo = new CustomerFamilyVo();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            try

            {
                
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
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
                txtDateofProfiling.Text = DateTime.Today.ToShortDateString();
                if (Session["Current_Link"].ToString() == "AdvisorLeftPane")
                {
                    // customerVo = (CustomerVo)Session["CustomerVo"];
                    txtRmName.Text = rmVo.FirstName.ToString() + " " + rmVo.MiddleName.ToString() + " " + rmVo.LastName.ToString();
                    txtEmail.Text = customerVo.Email.ToString();
                    txtPanNumber.Text = customerVo.PANNum.ToString();
                    txtCompanyName.Text = customerVo.CompanyName;
                    if (customerVo.DummyPAN == 1)
                        chkdummypan.Checked = true;
                    else
                        chkdummypan.Checked = false;
                }
                else if (Session["Current_Link"].ToString() == "RMCustomerNonIndividualLeftPane")
                {
                    //customerVo = (CustomerVo)Session["CustomerVo"];
                    txtRmName.Text = rmVo.FirstName.ToString() + " " + rmVo.MiddleName.ToString() + " " + rmVo.LastName.ToString();
                    txtEmail.Text = "";
                    txtCompanyName.Text = "";
                    txtDateofProfiling.Enabled = true;
                    txtDateofProfiling.Text = DateTime.Today.ToShortDateString();
                    if (customerVo.DummyPAN == 1)
                        chkdummypan.Checked = true;
                    else
                        chkdummypan.Checked = false;
                }
               
                
                if (!IsPostBack)
                {
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
                FunctionInfo.Add("Method", "CustomerNonIndividualAdd.ascx:Page_Load()");
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
            DataTable dt = null;
            try
            {

                // Bind States Drop Down
                dt = XMLBo.GetStates(path);

                ddlCorrAdrState.DataSource = dt;
                ddlCorrAdrState.DataTextField = "StateName";
                ddlCorrAdrState.DataValueField = "StateCode";
                ddlCorrAdrState.DataBind();
                ddlCorrAdrState.Items.Insert(0, new ListItem("Select a State", "Select a State"));

                ddlPermAdrState.DataSource = dt;
                ddlPermAdrState.DataTextField = "StateName";
                ddlPermAdrState.DataValueField = "StateCode";
                ddlPermAdrState.DataBind();
                ddlPermAdrState.Items.Insert(0, new ListItem("Select a State", "Select a State"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerNonIndividualAdd.ascx:BindDropDowns()");
                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = dt;                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    rmId = rmVo.RMId;
                    if (Session["Current_Link"].ToString() == "AdvisorLeftPane")
                    {

                        userVo = (UserVo)Session["UserVo"];
                       

                        txtDateofProfiling.Text = DateTime.Today.Date.ToString();
                        if (chkdummypan.Checked)
                        {
                            customerVo.DummyPAN = 1;
                        }
                        else
                            customerVo.DummyPAN = 0;
                        if (chksmsn.Checked)
                        {
                            customerVo.ViaSMS = 1;
                        }
                        else
                            customerVo.ViaSMS = 0;
                        if (chkmailn.Checked)
                        {
                            customerVo.AlertViaEmail = 1;
                        }
                        else
                            customerVo.AlertViaEmail = 0;

                        customerVo.ContactFirstName = txtFirstName.Text.ToString();
                        customerVo.ContactMiddleName = txtMiddleName.Text.ToString();
                        customerVo.ContactLastName = txtLastName.Text.ToString();
                        customerVo.CompanyName = txtCompanyName.Text.ToString();
                        customerVo.FirstName = txtCompanyName.Text.ToString(); 
                        customerVo.CustCode = txtCustomerCode.Text.ToString();
                        //customerVo.Salutation = ddlSalutation.SelectedItem.Value.ToString();
                        if (txtDateofRegistration.Text != "")
                        {
                            customerVo.RegistrationDate = DateTime.Parse(txtDateofRegistration.Text.ToString());
                        }
                        else
                        {
                            customerVo.RegistrationDate = DateTime.MinValue;
                        }
                        if (txtDateofCommencement.Text != "")
                        {
                            customerVo.CommencementDate = DateTime.Parse(txtDateofCommencement.Text.ToString());
                        }
                        else
                        {
                            customerVo.CommencementDate = DateTime.MinValue;
                        }
                        if (txtDateofProfiling.Text != "")
                        {
                            customerVo.ProfilingDate = DateTime.Parse(txtDateofProfiling.Text.ToString());
                        }
                        else
                        {
                            customerVo.ProfilingDate = DateTime.MinValue;
                        }
                        customerVo.RegistrationPlace = txtRegistrationPlace.Text.ToString();
                        customerVo.CompanyWebsite = txtCompanyWebsite.Text.ToString();
                        customerVo.RmId = rmId;
                        customerVo.PANNum = txtPanNumber.Text.ToString();
                        customerVo.RegistrationNum = txtRocRegistration.Text.ToString();
                        customerVo.Adr1Line1 = txtCorrAdrLine1.Text.ToString();
                        customerVo.Adr1Line2 = txtCorrAdrLine2.Text.ToString();
                        customerVo.Adr1Line3 = txtCorrAdrLine3.Text.ToString();
                        customerVo.Adr1PinCode = int.Parse(txtCorrAdrPinCode.Text.ToString());
                        customerVo.Adr1City = txtCorrAdrCity.Text.ToString();
                        customerVo.Adr1State = ddlCorrAdrState.SelectedItem.Value.ToString();
                        customerVo.Adr1Country = ddlCorrAdrCountry.SelectedItem.Value.ToString();
                        if (chkCorrPerm.Checked)
                        {
                            customerVo.Adr2Line1 = txtCorrAdrLine1.Text.ToString();
                            customerVo.Adr2Line2 = txtCorrAdrLine2.Text.ToString();
                            customerVo.Adr2Line3 = txtCorrAdrLine3.Text.ToString();
                            customerVo.Adr2PinCode = int.Parse(txtCorrAdrPinCode.Text.ToString());
                            customerVo.Adr2City = txtCorrAdrCity.Text.ToString();
                            customerVo.Adr2State = ddlCorrAdrState.Text.ToString();
                            customerVo.Adr2Country = ddlCorrAdrCountry.Text.ToString();
                        }
                        else
                        {
                            customerVo.Adr2Line1 = txtPermAdrLine1.Text.ToString();
                            customerVo.Adr2Line2 = txtPermAdrLine2.Text.ToString();
                            customerVo.Adr2Line3 = txtPermAdrLine3.Text.ToString();
                            customerVo.Adr2PinCode = int.Parse(txtPermAdrPinCode.Text.ToString());
                            customerVo.Adr2City = txtPermAdrCity.Text.ToString();
                            customerVo.Adr2State = ddlPermAdrState.Text.ToString();
                            customerVo.Adr2Country = ddlPermAdrCountry.Text.ToString();
                        }
                        customerVo.ResISDCode = int.Parse(txtPhoneNo1Isd.Text.ToString());
                        customerVo.ResSTDCode = int.Parse(txtPhoneNo1Std.Text.ToString());
                        customerVo.ResPhoneNum = int.Parse(txtPhoneNo1.Text.ToString());
                        customerVo.OfcISDCode = int.Parse(txtPhoneNo2Isd.Text.ToString());
                        customerVo.OfcSTDCode = int.Parse(txtPhoneNo2Std.Text.ToString());
                        customerVo.OfcPhoneNum = int.Parse(txtPhoneNo2.Text.ToString());
                        customerVo.Fax = int.Parse(txtFax.Text.ToString());
                        customerVo.ISDFax = int.Parse(txtFaxIsd.Text.ToString());
                        customerVo.STDFax = int.Parse(txtFaxStd.Text.ToString());
                        customerVo.Email = txtEmail.Text.ToString();
                        customerVo.MaritalStatus = null;
                        Session["Customer"] = "Customer";
                        //  customerBo.CreateCustomer(customerVo, customerVo.RmId,userVo.UserId);
                        if (Session["customerIds"] != null)
                        {
                            List<int> customerIds = new List<int>();
                            //int customerId = customerIds[1];
                            customerIds = (List<int>)Session["CustomerIds"];
                            customerVo.CustomerId = customerIds[1];
                            customerBo.UpdateCustomer(customerVo);
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AdviserCustomer','none');", true);
                            // Session.Remove("CustomerIds");
                        }



                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AdviserCustomer','none');", true);
                    }

                    else if (Session["Current_Link"].ToString() == "RMCustomerNonIndividualLeftPane")
                    {
                        CustomerVo newCustomerVo = new CustomerVo();
                        UserVo newUserVo = new UserVo();

                        newUserVo.FirstName = txtFirstName.Text.ToString();
                        newUserVo.MiddleName = txtMiddleName.Text.ToString();
                        newUserVo.LastName = txtLastName.Text.ToString();
                        newUserVo.Email = txtEmail.Text.ToString();
                        newUserVo.UserType = "Customer";

                        customerVo.FirstName = txtFirstName.Text.ToString();
                        customerVo.MiddleName = txtMiddleName.Text.ToString();
                        customerVo.LastName = txtLastName.Text.ToString();

                        if (txtEmail.Text == "")
                        {
                            customerVo.Email = txtFirstName.Text.ToString() + "@mail.com";
                        }
                        else
                        {
                            customerVo.Email = txtEmail.Text.ToString();
                        }
                        newCustomerVo.Type = "NIND";

                        txtDateofProfiling.Text = DateTime.Today.Date.ToString();

                        if (txtDateofRegistration.Text != "")
                        {
                            customerVo.RegistrationDate = DateTime.Parse(txtDateofRegistration.Text.ToString());
                        }
                        else
                        {
                            customerVo.RegistrationDate = DateTime.MinValue;
                        }
                        if (txtDateofCommencement.Text != "")
                        {
                            customerVo.CommencementDate = DateTime.Parse(txtDateofCommencement.Text.ToString());
                        }
                        else
                        {
                            customerVo.CommencementDate = DateTime.MinValue;
                        }
                        if (txtDateofProfiling.Text != "")
                        {
                            customerVo.ProfilingDate = DateTime.Parse(txtDateofProfiling.Text.ToString());
                        }
                        else
                        {
                            customerVo.ProfilingDate = DateTime.MinValue;
                        }

                        newCustomerVo.FirstName = txtFirstName.Text.ToString();
                        newCustomerVo.MiddleName = txtMiddleName.Text.ToString();
                        newCustomerVo.LastName = txtLastName.Text.ToString();
                        newCustomerVo.CompanyName = txtCompanyName.Text.ToString();
                        newCustomerVo.CustCode = txtCustomerCode.Text.ToString();
                        newCustomerVo.Salutation = ddlSalutation.SelectedItem.Value.ToString();
                        
                        newCustomerVo.RegistrationPlace = txtRegistrationPlace.Text.ToString();
                        newCustomerVo.CompanyWebsite = txtCompanyWebsite.Text.ToString();
                        newCustomerVo.RmId = rmId;
                        newCustomerVo.PANNum = txtPanNumber.Text.ToString();
                        newCustomerVo.RegistrationNum = txtRocRegistration.Text.ToString();
                        newCustomerVo.Adr1Line1 = txtCorrAdrLine1.Text.ToString();
                        newCustomerVo.Adr1Line2 = txtCorrAdrLine2.Text.ToString();
                        newCustomerVo.Adr1Line3 = txtCorrAdrLine3.Text.ToString();
                        newCustomerVo.Adr1PinCode = int.Parse(txtCorrAdrPinCode.Text.ToString());
                        newCustomerVo.Adr1City = txtCorrAdrCity.Text.ToString();
                        if (ddlCorrAdrState.SelectedIndex != 0)
                        {
                            newCustomerVo.Adr1State = ddlCorrAdrState.SelectedItem.Value.ToString();
                        }
                        else
                        {
                            newCustomerVo.Adr1State = null;
                        }
                        newCustomerVo.Adr1Country = ddlCorrAdrCountry.SelectedItem.Value.ToString();
                        if (chkCorrPerm.Checked)
                        {
                            newCustomerVo.Adr2Line1 = txtCorrAdrLine1.Text.ToString();
                            newCustomerVo.Adr2Line2 = txtCorrAdrLine2.Text.ToString();
                            newCustomerVo.Adr2Line3 = txtCorrAdrLine3.Text.ToString();
                            newCustomerVo.Adr2PinCode = int.Parse(txtCorrAdrPinCode.Text.ToString());
                            newCustomerVo.Adr2City = txtCorrAdrCity.Text.ToString();
                            if (ddlCorrAdrState.SelectedIndex != 0)
                            {
                                newCustomerVo.Adr2State = ddlCorrAdrState.Text.ToString();
                            }
                            else
                                newCustomerVo.Adr2State = null;
                            newCustomerVo.Adr2Country = ddlCorrAdrCountry.Text.ToString();
                        }
                        else
                        {
                            newCustomerVo.Adr2Line1 = txtPermAdrLine1.Text.ToString();
                            newCustomerVo.Adr2Line2 = txtPermAdrLine2.Text.ToString();
                            newCustomerVo.Adr2Line3 = txtPermAdrLine3.Text.ToString();
                            newCustomerVo.Adr2PinCode = int.Parse(txtPermAdrPinCode.Text.ToString());
                            newCustomerVo.Adr2City = txtPermAdrCity.Text.ToString();
                            if (ddlPermAdrState.SelectedIndex != 0)
                            {
                                newCustomerVo.Adr2State = ddlPermAdrState.Text.ToString();
                            }
                            else
                                newCustomerVo.Adr2State = null;
                            newCustomerVo.Adr2Country = ddlPermAdrCountry.Text.ToString();
                        }
                        newCustomerVo.ResISDCode = int.Parse(txtPhoneNo1Isd.Text.ToString());
                        newCustomerVo.ResSTDCode = int.Parse(txtPhoneNo1Std.Text.ToString());
                        newCustomerVo.ResPhoneNum = int.Parse(txtPhoneNo1.Text.ToString());
                        newCustomerVo.OfcISDCode = int.Parse(txtPhoneNo2Isd.Text.ToString());
                        newCustomerVo.OfcSTDCode = int.Parse(txtPhoneNo2Std.Text.ToString());
                        newCustomerVo.OfcPhoneNum = int.Parse(txtPhoneNo2.Text.ToString());
                        newCustomerVo.Fax = int.Parse(txtFax.Text.ToString());
                        newCustomerVo.ISDFax = int.Parse(txtFaxIsd.Text.ToString());
                        newCustomerVo.STDFax = int.Parse(txtFaxStd.Text.ToString());
                        newCustomerVo.Email = txtEmail.Text.ToString();
                        newCustomerVo.MaritalStatus = null;


                        customerPortfolioVo.CustomerId = customerFamilyVo.AssociateCustomerId;
                        customerPortfolioVo.IsMainPortfolio = 1;
                        customerPortfolioVo.PortfolioTypeCode = "RGL";
                        customerPortfolioVo.PMSIdentifier = "";
                        customerPortfolioVo.PortfolioName = "Default";



                        List<int> CustomerIds = new List<int>();
                        CustomerIds = customerBo.CreateCompleteCustomer(newCustomerVo, newUserVo, customerPortfolioVo, userVo.UserId);
                        customerFamilyVo.AssociateCustomerId = CustomerIds[1];
                        customerFamilyVo.Relationship = Session["relationship"].ToString();


                        customerFamilyBo.CreateCustomerFamily(customerFamilyVo, customerVo.CustomerId, userVo.UserId);
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomer','none');", true);
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
                FunctionInfo.Add("Method", "CustomerNonIndividualAdd.ascx:btnSubmit_Click()");
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
                    rmVo = (RMVo)Session["rmVo"];
                    customerVo = (CustomerVo)Session["CustomerVo"];
                    userVo = (UserVo)Session["UserVo"];
                    rmId = rmVo.RMId;


                    customerVo.FirstName = txtFirstName.Text.ToString();
                    customerVo.MiddleName = txtMiddleName.Text.ToString();
                    customerVo.LastName = txtLastName.Text.ToString();
                    customerVo.CompanyName = txtCompanyName.Text.ToString();
                    customerVo.FirstName = txtCompanyName.Text.ToString();
                    customerVo.CustCode = txtCustomerCode.Text.ToString();
                    customerVo.Salutation = ddlSalutation.SelectedItem.Value.ToString();
                    if (txtDateofRegistration.Text != string.Empty)
                        customerVo.RegistrationDate = DateTime.Parse(txtDateofRegistration.Text.ToString());
                    else
                        customerVo.RegistrationDate = DateTime.MinValue;
                    if (txtDateofCommencement.Text != string.Empty)
                        customerVo.CommencementDate = DateTime.Parse(txtDateofCommencement.Text.ToString());
                    else
                        customerVo.CommencementDate = DateTime.MinValue;
                    if (txtDateofProfiling.Text != string.Empty)
                        customerVo.ProfilingDate = DateTime.Parse(txtDateofProfiling.Text.ToString());
                    else
                        customerVo.ProfilingDate = DateTime.MinValue;

                    customerVo.RegistrationPlace = txtRegistrationPlace.Text.ToString();
                    customerVo.CompanyWebsite = txtCompanyWebsite.Text.ToString();
                    customerVo.RmId = rmId;
                    customerVo.PANNum = txtPanNumber.Text.ToString();
                    customerVo.RegistrationNum = txtRocRegistration.Text.ToString();
                    customerVo.Adr1Line1 = txtCorrAdrLine1.Text.ToString();
                    customerVo.Adr1Line2 = txtCorrAdrLine2.Text.ToString();
                    customerVo.Adr1Line3 = txtCorrAdrLine3.Text.ToString();
                    customerVo.Adr1PinCode = int.Parse(txtCorrAdrPinCode.Text.ToString());
                    customerVo.Adr1City = txtCorrAdrCity.Text.ToString();
                    customerVo.Adr1State = ddlCorrAdrState.SelectedItem.Value.ToString();
                    customerVo.Adr1Country = ddlCorrAdrCountry.SelectedItem.Value.ToString();
                    if (chkCorrPerm.Checked)
                    {
                        customerVo.Adr2Line1 = txtCorrAdrLine1.Text.ToString();
                        customerVo.Adr2Line2 = txtCorrAdrLine2.Text.ToString();
                        customerVo.Adr2Line3 = txtCorrAdrLine3.Text.ToString();
                        customerVo.Adr2PinCode = int.Parse(txtCorrAdrPinCode.Text.ToString());
                        customerVo.Adr2City = txtCorrAdrCity.Text.ToString();
                        customerVo.Adr2State = ddlCorrAdrState.Text.ToString();
                        customerVo.Adr2Country = ddlCorrAdrCountry.Text.ToString();
                    }
                    else
                    {
                        customerVo.Adr2Line1 = txtPermAdrLine1.Text.ToString();
                        customerVo.Adr2Line2 = txtPermAdrLine2.Text.ToString();
                        customerVo.Adr2Line3 = txtPermAdrLine3.Text.ToString();
                        customerVo.Adr2PinCode = int.Parse(txtPermAdrPinCode.Text.ToString());
                        customerVo.Adr2City = txtPermAdrCity.Text.ToString();
                        customerVo.Adr2State = ddlPermAdrState.Text.ToString();
                        customerVo.Adr2Country = ddlPermAdrCountry.Text.ToString();
                    }
                    customerVo.ResISDCode = int.Parse(txtPhoneNo1Isd.Text.ToString());
                    customerVo.ResSTDCode = int.Parse(txtPhoneNo1Std.Text.ToString());
                    customerVo.ResPhoneNum = int.Parse(txtPhoneNo1.Text.ToString());
                    customerVo.OfcISDCode = int.Parse(txtPhoneNo2Isd.Text.ToString());
                    customerVo.OfcSTDCode = int.Parse(txtPhoneNo2Std.Text.ToString());
                    customerVo.OfcPhoneNum = int.Parse(txtPhoneNo2.Text.ToString());
                    customerVo.Fax = int.Parse(txtFax.Text.ToString());
                    customerVo.ISDFax = int.Parse(txtFaxIsd.Text.ToString());
                    customerVo.STDFax = int.Parse(txtFaxStd.Text.ToString());
                    customerVo.Email = txtEmail.Text.ToString();
                    customerVo.MaritalStatus = null;
                    customerVo.Nationality = null;
                    customerVo.Occupation = null;
                    customerVo.Qualification = null;
                    customerVo.MarriageDate = DateTime.Parse("1990-01-01 00:00:00.000");
                    //customerBo.CreateCustomer(customerVo, customerVo.RmId,userVo.UserId,0);
                    customerBo.UpdateCustomer(customerVo);

                    Session["Check"] = "CustomerAdd";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AddBankDetails','none');", true);
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
                FunctionInfo.Add("Method", "CustomerNonIndividualAdd.ascx:btnAddBankDetails_Click()");
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
                FunctionInfo.Add("Method", "CustomerNonIndividualAdd.ascx:chkAvailability()");
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

            //if (txtDateofProfiling.Text.ToString() == "")
            //{
            //    lblProfilingDate.CssClass = "Error";
            //    result = false;
            //}
            //else
            //{
            //    lblProfilingDate.CssClass = "FieldName";
            //    result = true;
            //}


            if (txtPanNumber.Text.ToString() == "")
            {
                lblPANNum.CssClass = "Error";
                result = false;
            }
            else
            {
                lblPANNum.CssClass = "FieldName";
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
            //if (txtCorrAdrCity.Text.ToString() == "")
            //{
            //    lblAdrCity.CssClass = "Error";
            //    result = false;

            //}
            //else
            //{
            //    lblAdrCity.CssClass = "FieldName";
            //    result = true;
            //}
            //if (txtCorrAdrPinCode.Text.ToString() == "" || IsInteger(txtCorrAdrPinCode.Text.ToString()))
            //{
            //    lblAdrPinCode.CssClass = "Error";
            //    result = false;

            //}
            //else
            //{
            //    lblAdrPinCode.CssClass = "FieldName";
            //    result = true;
            //}

            //if (txtPhoneNo1.Text.ToString() == "" || txtPhoneNo1.Text.ToString() == "" || txtPhoneNo1.Text.ToString() == "")
            //{
            //    lblPhone1.CssClass = "Error";
            //    result = false;

            //}
            //else
            //{
            //    lblPhone1.CssClass = "FieldName";
            //    result = true;
            //}

            if (txtPhoneNo1Isd.Text.ToString() == "")
            {
                txtPhoneNo1Isd.Text = "0";
            }
            if (txtPhoneNo1Std.Text.ToString() == "")
            {
                txtPhoneNo1Std.Text = "0";
            }
            if (txtPhoneNo1.Text.ToString() == "")
            {
                txtPhoneNo1.Text = "0";
            }
            if (txtPhoneNo2Isd.Text.ToString() == "")
            {
                txtPhoneNo2Isd.Text = "0";
            }
            if (txtPhoneNo2Std.Text.ToString() == "")
            {
                txtPhoneNo2Std.Text = "0";
            }
            if (txtPhoneNo2.Text.ToString() == "")
            {
                txtPhoneNo2.Text = "0";
            }
            if (txtFax.Text.ToString() == "")
            {
                txtFax.Text = "0";
            }
            if (txtFaxIsd.Text.ToString() == "")
            {
                txtFaxIsd.Text = "0";
            }
            if (txtFaxStd.Text.ToString() == "")
            {
                txtFaxStd.Text = "0";
            }
            if (txtCorrAdrPinCode.Text.ToString() == "")
            {
                txtCorrAdrPinCode.Text = "0";
            }
            if (txtPermAdrPinCode.Text.ToString() == "")
            {
                txtPermAdrPinCode.Text = "0";
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

                FunctionInfo.Add("Method", "CustomerNonIndividualAdd.ascx:ChkMailId()");


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
            string TabberScript = "<script language='javascript'>document.getElementById('divContactDetails').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);
        }

    }
}

