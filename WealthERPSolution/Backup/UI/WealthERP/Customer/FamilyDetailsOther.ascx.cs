using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoCustomerProfiling;
using VoUser;
using BoUser;
using BoCommon;
using BoCustomerProfiling;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoCustomerPortfolio;
using BoCustomerPortfolio;

namespace WealthERP.Customer
{
    public partial class FamilyDetailsOther : System.Web.UI.UserControl
    {
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerVo parentCustomerVo = new CustomerVo();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        CustomerFamilyVo customerFamilyVo = new CustomerFamilyVo();
        UserVo userVo = new UserVo();
        //UserVo tempUserVo = new UserVo();
        RMVo rmVo = new RMVo();
        UserBo userBo=new UserBo();
        UserVo newUserVo = new UserVo();
        DataTable dtStates = new DataTable();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        string path;
        DataTable dtCustomerSubType = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            this.Page.Culture = "en-GB";
            txtDob_CompareValidator.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            BindDropdowns();

        }

        protected void BindDropdowns()
        {
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
        }

        protected void btnSubmit_Click1(object sender, EventArgs e)
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
                    newUserVo.FirstName = txtFirstName.Text.ToString();
                    newUserVo.MiddleName = txtMiddleName.Text.ToString();
                    newUserVo.LastName = txtLastName.Text.ToString();
                    newUserVo.UserType = "Customer";
                    newUserVo.Email = txtEmail.Text;

                    if(txtDob.Text.ToString()!="")
                    customerVo.Dob = DateTime.Parse(txtDob.Text.ToString());
                    customerVo.PANNum = txtPanNum.Text.ToString();
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
                    customerVo.Adr1Line1 = txtCorrAdrLine1.Text.ToString();
                    customerVo.Adr1Line2 = txtCorrAdrLine2.Text.ToString();
                    customerVo.Adr1Line3 = txtCorrAdrLine3.Text.ToString();
                    customerVo.Adr1PinCode = int.Parse(txtCorrAdrPinCode.Text.ToString());
                    customerVo.Adr1City = txtCorrAdrCity.Text.ToString();
                    customerVo.Adr1State = ddlCorrAdrState.SelectedItem.Value.ToString();
                    customerVo.Adr1Country = ddlCorrAdrCountry.SelectedItem.Value.ToString();
                    customerVo.ResISDCode = int.Parse(txtResPhoneNoIsd.Text.ToString());
                    customerVo.ResSTDCode = int.Parse(txtResPhoneNoStd.Text.ToString());
                    customerVo.ResPhoneNum = Int32.Parse(txtResPhoneNo.Text.ToString());
                    customerVo.OfcISDCode = int.Parse(txtOfcPhoneNoIsd.Text.ToString());
                    customerVo.OfcSTDCode = int.Parse(txtOfcPhoneNoStd.Text.ToString());
                    customerVo.OfcPhoneNum = Int32.Parse(txtOfcPhoneNo.Text.ToString());
                    customerVo.ISDFax = int.Parse(txtResFaxIsd.Text.ToString());
                    customerVo.STDFax = int.Parse(txtResFaxStd.Text.ToString());
                    customerVo.Fax = Int32.Parse(txtResFax.Text.ToString());
                    customerVo.OfcISDFax = int.Parse(txtOfcFaxIsd.Text.ToString());
                    customerVo.OfcSTDFax = int.Parse(txtOfcFaxStd.Text.ToString());
                    customerVo.OfcFax = Int32.Parse(txtOfcFax.Text.ToString());
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

                    //customerPortfolioVo.CustomerId = customerFamilyVo.AssociateCustomerId;
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PMSIdentifier = "";


                    customerPortfolioVo.PortfolioName = "MyPortfolio";

                    List<int> CustomerIds = new List<int>();
                    CustomerIds = customerBo.CreateCompleteCustomer(customerVo, newUserVo, customerPortfolioVo, userVo.UserId);
                    customerFamilyVo.AssociateCustomerId = CustomerIds[1];

                   
                    customerFamilyVo.Relationship = Session["relationship"].ToString();
                    customerFamilyBo.CreateCustomerFamily(customerFamilyVo, parentCustomerVo.CustomerId, userVo.UserId);

                   

                   // portfolioBo.CreateCustomerPortfolio(customerPortfolioVo, userVo.UserId);


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
                FunctionInfo.Add("Method", "FamilyDetailsOther.ascx:btnSubmit_Click()");
                object[] objects = new object[5];
                objects[0] = customerVo;                
                objects[1] = userVo;                
                objects[2] = rmVo;
                objects[3] = customerFamilyVo;
                objects[4] = parentCustomerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }

        protected void btnBankDetails_Click(object sender, EventArgs e)
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

                    newUserVo.FirstName = txtFirstName.Text.ToString();
                    newUserVo.MiddleName = txtMiddleName.Text.ToString();
                    newUserVo.LastName = txtLastName.Text.ToString();
                    newUserVo.UserType = "Customer";
                    newUserVo.Email = txtEmail.Text;

                    if (txtDob.Text.ToString() != "")
                        customerVo.Dob = DateTime.Parse(txtDob.Text.ToString());
                    customerVo.PANNum = txtPanNum.Text.ToString();
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
                    customerVo.Adr1Line1 = txtCorrAdrLine1.Text.ToString();
                    customerVo.Adr1Line2 = txtCorrAdrLine2.Text.ToString();
                    customerVo.Adr1Line3 = txtCorrAdrLine3.Text.ToString();
                    customerVo.Adr1PinCode = int.Parse(txtCorrAdrPinCode.Text.ToString());
                    customerVo.Adr1City = txtCorrAdrCity.Text.ToString();
                    customerVo.Adr1State = ddlCorrAdrState.SelectedItem.Value.ToString();
                    customerVo.Adr1Country = ddlCorrAdrCountry.SelectedItem.Value.ToString();
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

                    //customerPortfolioVo.CustomerId = customerFamilyVo.AssociateCustomerId;
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PMSIdentifier = "";


                    customerPortfolioVo.PortfolioName = "Default";

                    List<int> CustomerIds = new List<int>();
                    CustomerIds = customerBo.CreateCompleteCustomer(customerVo, newUserVo, customerPortfolioVo, userVo.UserId);
                    customerFamilyVo.AssociateCustomerId = CustomerIds[1];


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
                FunctionInfo.Add("Method", "FamilyDetailsOther.ascx:btnBankDetails_Click()");
                object[] objects = new object[5];
                objects[0] = customerVo;
                objects[1] = customerPortfolioVo;
                objects[2] = userVo;
                objects[3] = customerFamilyVo;
                objects[4] = rmVo;

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
                FunctionInfo.Add("Method", "FamilyDetailsOther.ascx:ChkMailId()");
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
                FunctionInfo.Add("Method", "FamilyDetailsOther.ascx:chkAvailability()");
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
                if (txtDob.Text.ToString() == "")
                {
                    lblDob.CssClass = "Error";
                    result = false;

                }
                else
                {
                    lblDob.CssClass = "FieldName";
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
               
              
                if (txtCorrAdrPinCode.Text.ToString() == "")
                {
                    txtCorrAdrPinCode.Text = "0";
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
                if (txtPermAdrPinCode.Text == "")
                {
                    txtPermAdrPinCode.Text = "0";
                }
                if (txtMobile2.Text == "")
                {
                    txtMobile2.Text = "0";
                }
                if (txtMobile1.Text == "")
                {
                    txtMobile1.Text = "0";
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
                FunctionInfo.Add("Method", "FamilyDetailsOther.ascx:Validation()");
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
