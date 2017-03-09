using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoCustomerProfiling;
using VoUser;
using BoCustomerProfiling;
using BoUser;
using System.Configuration;
using BoCommon;
using System.Collections.Specialized;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCustomerPortfolio;
using VoCustomerPortfolio;


namespace WealthERP.Customer
{
    public partial class FamilyDetailsSpouse : System.Web.UI.UserControl
    {
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerVo parentCustomerVo = new CustomerVo();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        CustomerFamilyVo customerFamilyVo = new CustomerFamilyVo();
        //UserVo tempUserVo = new UserVo();
        RMVo rmVo = new RMVo();
        UserVo userVo = new UserVo();
        UserBo userBo = new UserBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        UserVo newUserVo = new UserVo();
        DataTable dtStates = new DataTable();
        string path;
        DataTable dtCustomerSubType = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            this.Page.Culture = "en-GB";
            txtDob_CompareValidator.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            BindDropDowns();
        }
        protected void BindDropDowns()
        {
            dtStates = XMLBo.GetStates(path);
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
                    rmVo = (RMVo)Session["rmVo"];
                    userVo = (UserVo)Session["userVo"];
                    parentCustomerVo = (CustomerVo)Session["CustomerVo"];
                    customerVo.RmId = rmVo.RMId;
                    //customerVo.CustomerId = int.Parse(Session["CustomerId2"].ToString());
                    //customerVo.UserId = customerBo.GenerateId();
                    customerVo.FirstName = txtFirstName.Text.ToString();
                    customerVo.MiddleName = txtMiddleName.Text.ToString();
                    customerVo.LastName = txtLastName.Text.ToString();
                    customerVo.BranchId = parentCustomerVo.BranchId;
                    if (txtDob.Text.ToString() != "")
                    customerVo.Dob = DateTime.Parse(txtDob.Text.ToString());
                    customerVo.PANNum = txtPanNum.Text.ToString();
                    customerVo.OfcAdrLine1 = txtOfcAdrLine1.Text.ToString();
                    customerVo.OfcAdrLine2 = txtOfcAdrLine2.Text.ToString();
                    customerVo.OfcAdrLine3 = txtOfcAdrLine3.Text.ToString();
                    customerVo.OfcAdrPinCode = int.Parse(txtOfcAdrPinCode.Text.ToString());
                    customerVo.OfcAdrCity = txtOfcAdrCity.Text.ToString();
                    customerVo.OfcAdrState = ddlOfcAdrState.Text.ToString();
                    customerVo.OfcAdrCountry = ddlOfcAdrCountry.Text.ToString();
                    customerVo.CompanyName = txtOfcCompanyName.Text.ToString();
                    customerVo.ResISDCode = int.Parse(txtResPhoneNoIsd.Text.ToString());
                    customerVo.ResSTDCode = int.Parse(txtResPhoneNoStd.Text.ToString());
                    customerVo.ResPhoneNum = int.Parse(txtResPhoneNo.Text.ToString());
                    customerVo.OfcISDCode = int.Parse(txtOfcPhoneNoIsd.Text.ToString());
                    customerVo.OfcSTDCode = int.Parse(txtOfcPhoneNoStd.Text.ToString());
                    customerVo.OfcPhoneNum = int.Parse(txtOfcPhoneNo.Text.ToString());
                    customerVo.ISDFax = int.Parse(txtResFaxIsd.Text.ToString());
                    customerVo.STDFax = int.Parse(txtResFaxStd.Text.ToString());
                    customerVo.Fax = int.Parse(txtResFax.Text.ToString());
                    customerVo.OfcISDFax = int.Parse(txtOfcFaxIsd.Text.ToString());
                    customerVo.OfcSTDFax = int.Parse(txtOfcFaxStd.Text.ToString());
                    customerVo.OfcFax = int.Parse(txtOfcFax.Text.ToString());
                    customerVo.Mobile1 = long.Parse(txtMobile1.Text.ToString());
                    customerVo.Mobile2 = long.Parse(txtMobile2.Text.ToString());
                    customerVo.Email = txtEmail.Text.ToString();
                    customerVo.AltEmail = txtAltEmail.Text.ToString();
                    customerVo.ProfilingDate = DateTime.Today;
                    if (rbtnIndividual.Checked)
                    {
                        customerVo.Type = "IND";
                    }
                    else
                    {
                        customerVo.Type = "NIND";
                    }
                    customerVo.SubType = ddlCustomerSubType.SelectedItem.Value.ToString();
                   // customerPortfolioVo.CustomerId = customerFamilyVo.AssociateCustomerId;
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PMSIdentifier = "";
                    customerPortfolioVo.PortfolioName = "MyPortfolio";


                    newUserVo.FirstName = txtFirstName.Text.ToString();
                    newUserVo.MiddleName = txtMiddleName.Text.ToString();
                    newUserVo.LastName = txtLastName.Text.ToString();
                    newUserVo.UserType = "Customer";
                    newUserVo.Email = txtEmail.Text;


                    List<int> CustomerIds = new List<int>();
                    CustomerIds = customerBo.CreateCompleteCustomer(customerVo, newUserVo, customerPortfolioVo, userVo.UserId);
                    customerFamilyVo.AssociateCustomerId = CustomerIds[1];
                    //customerVo.UserId = customerBo.CreateCustomerUser(customerVo, userVo.UserId);
                    //tempUserVo = userBo.GetUser(customerVo.Email);
                    //customerVo.UserId = tempUserVo.UserId;
                    //customerFamilyVo.AssociateCustomerId = customerBo.CreateCustomer(customerVo, customerVo.RmId, userVo.UserId);
                    customerFamilyVo.Relationship = Session["relationship"].ToString();
                    customerFamilyBo.CreateCustomerFamily(customerFamilyVo, parentCustomerVo.CustomerId, userVo.UserId);

                    

                    //portfolioBo.CreateCustomerPortfolio(customerPortfolioVo, userVo.UserId);


                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCustomerFamily','none');", true);
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

                FunctionInfo.Add("Method", "FamilyDetailsSpouse.ascx:btnSubmit_Click()");


                object[] objects = new object[5];

                objects[0] = customerVo;
                objects[1] = parentCustomerVo;
                objects[2] = userVo;
                objects[3] = customerFamilyVo;
                objects[4] = rmVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void btnAddDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    rmVo = (RMVo)Session["rmVo"];
                    userVo = (UserVo)Session["userVo"];
                    parentCustomerVo = (CustomerVo)Session["CustomerVo"];
                    customerVo.RmId = rmVo.RMId;
                    //customerVo.CustomerId = int.Parse(Session["CustomerId2"].ToString());
                    //customerVo.UserId = customerBo.GenerateId();
                    customerVo.FirstName = txtFirstName.Text.ToString();
                    customerVo.MiddleName = txtMiddleName.Text.ToString();
                    customerVo.LastName = txtLastName.Text.ToString();
                    if (txtDob.Text.ToString() != "")
                        customerVo.Dob = DateTime.Parse(txtDob.Text.ToString());
                    customerVo.PANNum = txtPanNum.Text.ToString();
                    customerVo.OfcAdrLine1 = txtOfcAdrLine1.Text.ToString();
                    customerVo.OfcAdrLine2 = txtOfcAdrLine2.Text.ToString();
                    customerVo.OfcAdrLine3 = txtOfcAdrLine3.Text.ToString();
                    customerVo.OfcAdrPinCode = int.Parse(txtOfcAdrPinCode.Text.ToString());
                    customerVo.OfcAdrCity = txtOfcAdrCity.Text.ToString();
                    customerVo.OfcAdrState = ddlOfcAdrState.Text.ToString();
                    customerVo.OfcAdrCountry = ddlOfcAdrCountry.Text.ToString();
                    customerVo.CompanyName = txtOfcCompanyName.Text.ToString();
                    customerVo.ResISDCode = int.Parse(txtResPhoneNoIsd.Text.ToString());
                    customerVo.ResSTDCode = int.Parse(txtResPhoneNoStd.Text.ToString());
                    customerVo.ResPhoneNum = int.Parse(txtResPhoneNo.Text.ToString());
                    customerVo.OfcISDCode = int.Parse(txtOfcPhoneNoIsd.Text.ToString());
                    customerVo.OfcSTDCode = int.Parse(txtOfcPhoneNoStd.Text.ToString());
                    customerVo.OfcPhoneNum = int.Parse(txtOfcPhoneNo.Text.ToString());
                    customerVo.ISDFax = int.Parse(txtResFaxIsd.Text.ToString());
                    customerVo.STDFax = int.Parse(txtResFaxStd.Text.ToString());
                    customerVo.Fax = int.Parse(txtResFax.Text.ToString());
                    customerVo.OfcISDFax = int.Parse(txtOfcFaxIsd.Text.ToString());
                    customerVo.OfcSTDFax = int.Parse(txtOfcFaxStd.Text.ToString());
                    customerVo.OfcFax = int.Parse(txtOfcFax.Text.ToString());
                    customerVo.Mobile1 = long.Parse(txtMobile1.Text.ToString());
                    customerVo.Mobile2 = long.Parse(txtMobile2.Text.ToString());
                    customerVo.Email = txtEmail.Text.ToString();
                    customerVo.AltEmail = txtAltEmail.Text.ToString();
                    customerVo.ProfilingDate = DateTime.Today;
                    //customerVo.RBIApprovalDate = DateTime.Parse("1/1/99");
                    //customerVo.RegistrationDate = DateTime.Parse("1/1/99");
                    //customerVo.CommencementDate = DateTime.Parse("1/1/99");
                    if (rbtnIndividual.Checked)
                    {
                        customerVo.Type = "IND";
                    }
                    else
                    {
                        customerVo.Type = "NIND";
                    }
                    customerVo.SubType = ddlCustomerSubType.SelectedItem.Value.ToString();
                    //Session["CustomerId"] = customerVo.CustomerId;

                    // customerPortfolioVo.CustomerId = customerFamilyVo.AssociateCustomerId;
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PMSIdentifier = "";
                    customerPortfolioVo.PortfolioName = "MyPortfolio";


                    newUserVo.FirstName = txtFirstName.Text.ToString();
                    newUserVo.MiddleName = txtMiddleName.Text.ToString();
                    newUserVo.LastName = txtLastName.Text.ToString();
                    newUserVo.UserType = "Customer";
                    newUserVo.Email = txtEmail.Text;


                    List<int> CustomerIds = new List<int>();
                    CustomerIds = customerBo.CreateCompleteCustomer(customerVo, newUserVo, customerPortfolioVo, userVo.UserId);
                    customerFamilyVo.AssociateCustomerId = CustomerIds[1];
                    //customerVo.UserId = customerBo.CreateCustomerUser(customerVo, userVo.UserId);
                    //tempUserVo = userBo.GetUser(customerVo.Email);
                    //customerVo.UserId = tempUserVo.UserId;
                    //customerFamilyVo.AssociateCustomerId = customerBo.CreateCustomer(customerVo, customerVo.RmId, userVo.UserId);
                    customerFamilyVo.Relationship = Session["relationship"].ToString();
                    customerFamilyBo.CreateCustomerFamily(customerFamilyVo, parentCustomerVo.CustomerId, userVo.UserId);
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
                FunctionInfo.Add("Method", "FamilyDetailsSpouse.ascx:btnAddDetails_Click()");
                object[] objects = new object[3];
                objects[0] = customerVo;                
                objects[1] = userVo;                
                objects[2] = rmVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
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
                FunctionInfo.Add("Method", "FamilyDetailsSpouse.ascx:ChkMailId()");


                object[] objects = new object[1];
                objects[0] = email;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
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
                FunctionInfo.Add("Method", "FamilyDetailsSpouse.ascx:chkAvailability()");
                object[] objects = new object[1];
                objects[0] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

       

        public bool Validation()
        {  
            bool result = true;

            try
            {
                if (!ChkMailId(txtEmail.Text.ToString()))
                {
                    result = false;
                    lblEmail.CssClass = "Error";

                }
             
             
                if (txtLastName.Text.ToString() == "")
                {
                    lblName.CssClass = "Error";
                    result = false;

                }
                else
                {
                    lblName.CssClass = "FieldName";
                    result = true;
                }
              
                if (txtPanNum.Text.ToString() == "")
                {
                    lblPanNum.CssClass = "Error";
                    result = false;

                }
                else
                {
                    lblPanNum.CssClass = "FieldName";
                    result = true;
                }
                if (txtResPhoneNoStd.Text.ToString() == "")
                {
                    txtResPhoneNoStd.Text = "0";
                }
                if (txtResPhoneNo.Text.ToString() == "")
                {
                    txtResPhoneNo.Text = "0";
                }
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
            }

             catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FamilyDetailsSpouse.ascx:Validation()");
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
            try
            {
                dtCustomerSubType = XMLBo.GetCustomerSubType(path, "IND");
                ddlCustomerSubType.DataSource = dtCustomerSubType;
                ddlCustomerSubType.DataTextField = "CustomerTypeName";
                ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
                ddlCustomerSubType.DataBind();
                ddlCustomerSubType.Items.Insert(0, new ListItem("Select a Sub-Type", "Select a Sub-Type"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FamilyDetailsChild.ascx:rbtnIndividual_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void rbtnNonIndividual_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dtCustomerSubType = XMLBo.GetCustomerSubType(path, "NIND");
                ddlCustomerSubType.DataSource = dtCustomerSubType;
                ddlCustomerSubType.DataTextField = "CustomerTypeName";
                ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
                ddlCustomerSubType.DataBind();
                ddlCustomerSubType.Items.Insert(0, new ListItem("Select a Sub-Type", "Select a Sub-Type"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FamilyDetailsChild.ascx:rbtnNonIndividual_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
           


    }
}

