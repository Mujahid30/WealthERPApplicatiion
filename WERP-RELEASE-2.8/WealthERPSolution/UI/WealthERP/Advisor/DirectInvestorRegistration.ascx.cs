using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using System.Configuration;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoUser;
using VoCustomerPortfolio;
using BoCustomerProfiling;
using BoUser;

namespace WealthERP
{
    public partial class DirectInvestorRegistration : System.Web.UI.UserControl
    {
        string path;
        DataTable dtCustomerSubType = new DataTable();
        DataTable dtStates = new DataTable();
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        CustomerBo customerBo = new CustomerBo();
        UserBo userBo = new UserBo();
        Random r = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            if (!IsPostBack)
            {
                BindState();
            }



        }

        protected void BindState()
        {
            dtStates = XMLBo.GetStates(path);
            ddlState.DataSource = dtStates;
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "StateCode";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("Select a State", "Select a State"));
        }

        protected void rbtnIndividual_CheckedChanged(object sender, EventArgs e)
        {
            dtCustomerSubType = XMLBo.GetCustomerSubType(path, "IND");
            ddlCustomerSubType.DataSource = dtCustomerSubType;
            ddlCustomerSubType.DataTextField = "CustomerTypeName";
            ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
            ddlCustomerSubType.DataBind();
            ddlCustomerSubType.Items.Insert(0, new ListItem("Select a Sub-Type", "Select a Sub-Type"));

            trCompanyName.Visible = false;
            trIndividualName.Visible = true;
        }

        protected void rbtnNonIndividual_CheckedChanged(object sender, EventArgs e)
        {
            dtCustomerSubType = XMLBo.GetCustomerSubType(path, "NIND");
            ddlCustomerSubType.DataSource = dtCustomerSubType;
            ddlCustomerSubType.DataTextField = "CustomerTypeName";
            ddlCustomerSubType.DataValueField = "CustomerSubTypeCode";
            ddlCustomerSubType.DataBind();
            ddlCustomerSubType.Items.Insert(0, new ListItem("Select a Sub-Type", "Select a Sub-Type"));

            trCompanyName.Visible = true;
            trIndividualName.Visible = false;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            List<int> customerIds = null;


            try
            {
                if (Validation())
                {
                    userVo = new UserVo();
                    if (rbtnIndividual.Checked)
                    {

                        customerVo.RmId = 100;
                        customerVo.Type = "IND";
                        customerVo.FirstName = txtFirstName.Text.ToString();
                        customerVo.MiddleName = txtMiddleName.Text.ToString();
                        customerVo.LastName = txtLastName.Text.ToString();

                        userVo.FirstName = txtFirstName.Text.ToString();
                        userVo.MiddleName = txtMiddleName.Text.ToString();
                        userVo.LastName = txtLastName.Text.ToString();
                    }
                    if (rbtnNonIndividual.Checked)
                    {

                        customerVo.RmId = 100;
                        customerVo.Type = "NIND";
                        customerVo.CompanyName = txtCompanyName.Text.ToString();
                        customerVo.LastName = txtCompanyName.Text.ToString();
                        userVo.LastName = txtCompanyName.Text.ToString();
                    }

                    userVo.LoginId = txtEmail.Text.ToString();
                    userVo.Email = txtEmail.Text.ToString();
                    userVo.Password = r.Next(20000, 100000).ToString();
                    

                    customerVo.SubType = ddlCustomerSubType.SelectedItem.Value;
                    customerVo.Email = txtEmail.Text.ToString();
                    customerVo.Dob = DateTime.MinValue;
                    customerVo.RBIApprovalDate = DateTime.MinValue;
                    customerVo.CommencementDate = DateTime.MinValue;
                    customerVo.RegistrationDate = DateTime.MinValue;
                    if (ddlState.SelectedIndex != 0)
                    {
                        customerVo.Adr1State = ddlState.SelectedItem.Value.ToString();
                    }
                    else
                    {
                        customerVo.Adr1State = null;
                    }
                    customerVo.Adr2State = null;

                    customerVo.Adr1City = txtCity.Text.ToString();
                    customerVo.Adr1Country = ddlCountry.SelectedItem.Value.ToString();
                    customerVo.Adr1Line1 = txtAddressLine1.Text.ToString();
                    customerVo.Adr1Line2 = txtAddressLine2.Text.ToString();
                    customerVo.Adr1Line3 = txtAddressLine3.Text.ToString();
                    if (txtPinCode.Text != "")
                    {
                        customerVo.Adr1PinCode = int.Parse(txtPinCode.Text.ToString());
                    }
                    else
                    {
                        customerVo.Adr1PinCode = 0;
                    }
                    customerVo.PANNum = txtPanNumber.Text.ToString();

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
                    customerVo.ProfilingDate = DateTime.MinValue;
                    customerVo.Email = txtEmail.Text.ToString();
                    customerVo.AltEmail = txtAltEmail.Text.ToString();
                    //userVo.UserId = customerBo.GenerateId();
                    customerVo.UserId = userVo.UserId;

                    userVo.Email = txtEmail.Text.ToString();
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PortfolioName = "MyPortfolio";
                    customerIds = customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, customerVo.RmId);
                    UserVo newUser = userBo.GetUserDetails(customerIds[0]);
                    Session["userVo"] = newUser;

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UserLoginMessage','none');", true);
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
                FunctionInfo.Add("Method", "DirectInvestorRegistration.ascx:btnSubmit_Click()");
                object[] objects = new object[4];
                objects[0] = customerIds;
                objects[1] = customerVo;
                objects[2] = userVo;
                objects[3] = customerPortfolioVo;
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

                FunctionInfo.Add("Method", "CustomerType.ascx:ChkMailId()");


                object[] objects = new object[1];
                objects[0] = email;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
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


                if (txtFirstName.Text.ToString() == "" || txtLastName.Text.ToString() == "")
                {
                    lblName.CssClass = "Error";
                    result = false;
                }
                else
                {
                    lblName.CssClass = "FieldName";
                    result = true;
                }
                if (txtCompanyName.Text.ToString() == "")
                {
                    lblCompanyName.CssClass = "Error";
                    result = false;
                }
                else
                {
                    lblCompanyName.CssClass = "FieldName";
                    result = true;
                }

                if (ddlCustomerSubType.SelectedItem.Value.ToString() == "")
                {
                    lblCustomerSubType.CssClass = "Error";
                    result = false;
                }
                else
                {
                    lblCustomerSubType.CssClass = "FieldName";
                    result = true;
                }
                if ((rbtnIndividual.Checked == false) && (rbtnNonIndividual.Checked == false))
                {
                    lblCustomerType.CssClass = "Error";
                    result = false;
                }
                else
                {
                    lblCustomerType.CssClass = "FieldName";
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


    }
}