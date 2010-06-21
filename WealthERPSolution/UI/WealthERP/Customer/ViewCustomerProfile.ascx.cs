using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using VoCustomerProfiling;
using BoCustomerProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;
using WealthERP.Base;

namespace WealthERP.Customer
{
    public partial class ViewCustomerProfile : System.Web.UI.UserControl
    {
        UserVo userVo = null;
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                txtProfilingDate.Text = customerVo.ProfilingDate.ToString();
                txtDob.Text = customerVo.Dob.ToShortDateString().ToString();
                txtFirstName.Text = customerVo.ContactFirstName;
                txtMiddleName.Text = customerVo.ContactMiddleName;
                txtLastName.Text = customerVo.ContactLastName;
                txtCustomerCode.Text = customerVo.CustCode.ToString();
                txtPanNumber.Text = customerVo.PANNum.ToString();
                //txtRmName.Text = customerVo.RmId.ToString();
                txtCorrAdrLine1.Text = customerVo.Adr1Line1.ToString();
                txtCorrAdrLine2.Text = customerVo.Adr1Line2.ToString();
                txtCorrAdrLine3.Text = customerVo.Adr1Line3.ToString();
                txtCorrAdrPinCode.Text = customerVo.Adr1PinCode.ToString();
                txtCorrAdrCity.Text = customerVo.Adr1City.ToString();
                txtCorrAdrState.Text = customerVo.Adr1State.ToString();
                txtCorrAdrCountry.Text = customerVo.Adr1Country.ToString();
                txtPermAdrLine1.Text = customerVo.Adr2Line1.ToString();
                txtPermAdrLine2.Text = customerVo.Adr2Line2.ToString();
                txtPermAdrLine3.Text = customerVo.Adr2Line3.ToString();
                txtPermAdrPinCode.Text = customerVo.Adr2PinCode.ToString();
                txtPermAdrCity.Text = customerVo.Adr2City.ToString();
                txtPermAdrState.Text = customerVo.Adr2State.ToString();
                txtPermAdrCountry.Text = customerVo.Adr2Country.ToString();
                txtOfcAdrLine1.Text = customerVo.OfcAdrLine1.ToString();
                txtOfcAdrLine2.Text = customerVo.OfcAdrLine2.ToString();
                txtOfcAdrLine3.Text = customerVo.OfcAdrLine3.ToString();
                txtOfcAdrPinCode.Text = customerVo.OfcAdrPinCode.ToString();
                txtOfcAdrCity.Text = customerVo.OfcAdrCity.ToString();
                txtOfcAdrState.Text = customerVo.OfcAdrState.ToString();
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
                txtAltEmail.Text = customerVo.Email.ToString();
                txtOccupation.Text = customerVo.Occupation.ToString();
                txtMaritalStatus.Text = customerVo.MaritalStatus.ToString();
                txtQualification.Text = customerVo.Qualification.ToString();
                txtRBIRefNo.Text = customerVo.RBIRefNum.ToString();
                txtRBIRefDate.Text = customerVo.RBIApprovalDate.ToShortDateString().ToString();
                txtNationality.Text = customerVo.Nationality.ToString();
                lblSubType.Text = customerVo.SubType;
                lblType.Text = customerVo.Type;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewCustomerProfile.ascx:Page_Load()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = userVo;                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                customerVo.ContactFirstName = txtFirstName.Text.ToString();
                customerVo.ContactMiddleName = txtMiddleName.Text.ToString();
                customerVo.ContactLastName = txtLastName.Text.ToString();
                customerVo.Adr1Line1 = txtCorrAdrLine1.Text.ToString();
                customerVo.Adr1Line2 = txtCorrAdrLine2.Text.ToString();
                customerVo.Adr1Line3 = txtCorrAdrLine3.Text.ToString();
                customerVo.Adr1PinCode = int.Parse(txtCorrAdrPinCode.Text.ToString());
                customerVo.Adr1City = txtCorrAdrCity.Text.ToString();
                customerVo.Adr1State = txtCorrAdrState.Text.ToString();
                customerVo.Adr1Country = txtCorrAdrCountry.Text.ToString();
                customerVo.Adr2Line1 = txtPermAdrLine1.Text.ToString();
                customerVo.Adr2Line2 = txtPermAdrLine2.Text.ToString();
                customerVo.Adr2Line3 = txtPermAdrLine3.Text.ToString();
                customerVo.Adr2City = txtPermAdrCity.Text.ToString();
                customerVo.Adr2PinCode = int.Parse(txtPermAdrPinCode.Text.ToString());
                customerVo.Adr2State = txtPermAdrState.Text.ToString();
                customerVo.Adr2Country = txtPermAdrCountry.Text.ToString();
                customerVo.OfcAdrLine1 = txtOfcAdrLine1.Text.ToString();
                customerVo.OfcAdrLine2 = txtOfcAdrLine2.Text.ToString();
                customerVo.OfcAdrLine3 = txtOfcAdrLine3.Text.ToString();
                customerVo.OfcAdrPinCode = int.Parse(txtOfcAdrPinCode.Text.ToString());
                customerVo.OfcAdrCity = txtOfcAdrCity.Text.ToString();
                customerVo.OfcAdrState = txtOfcAdrState.Text.ToString();
                customerVo.OfcAdrCountry = txtOfcAdrCountry.Text.ToString();
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
                customerVo.Occupation = txtOccupation.Text.ToString();
                customerVo.Qualification = txtQualification.Text.ToString();
                customerVo.Nationality = txtNationality.Text.ToString();
                customerVo.RBIRefNum = txtRBIRefNo.Text.ToString();
                customerVo.RBIApprovalDate = DateTime.Parse(txtRBIRefDate.Text.ToString());
                customerVo.MaritalStatus = txtMaritalStatus.Text.ToString();

                customerBo.UpdateCustomer(customerVo);

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
                txtCorrAdrState.Text = customerVo.Adr1State.ToString();
                txtCorrAdrCountry.Text = customerVo.Adr1Country.ToString();
                txtPermAdrLine1.Text = customerVo.Adr2Line1.ToString();
                txtPermAdrLine2.Text = customerVo.Adr2Line2.ToString();
                txtPermAdrLine3.Text = customerVo.Adr2Line3.ToString();
                txtPermAdrPinCode.Text = customerVo.Adr2PinCode.ToString();
                txtPermAdrCity.Text = customerVo.Adr2City.ToString();
                txtPermAdrState.Text = customerVo.Adr2State.ToString();
                txtPermAdrCountry.Text = customerVo.Adr2Country.ToString();
                txtOfcAdrLine1.Text = customerVo.OfcAdrLine1.ToString();
                txtOfcAdrLine2.Text = customerVo.OfcAdrLine2.ToString();
                txtOfcAdrLine3.Text = customerVo.OfcAdrLine3.ToString();
                txtOfcAdrPinCode.Text = customerVo.OfcAdrPinCode.ToString();
                txtOfcAdrCity.Text = customerVo.OfcAdrCity.ToString();
                txtOfcAdrState.Text = customerVo.OfcAdrState.ToString();
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
                txtAltEmail.Text = customerVo.Email.ToString();
                txtOccupation.Text = customerVo.Occupation.ToString();
                txtMaritalStatus.Text = customerVo.MaritalStatus.ToString();
                txtQualification.Text = customerVo.Qualification.ToString();
                txtRBIRefNo.Text = customerVo.RBIRefNum.ToString();
                txtRBIRefDate.Text = customerVo.RBIApprovalDate.ToShortDateString().ToString();
                txtNationality.Text = customerVo.Nationality.ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewCustomerProfile.ascx:btnEdit_Click()");
                object[] objects = new object[1];
                objects[0] = customerVo;                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
        }

        protected void hiddenDelete_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                DeleteCustomerProfile();
            }
        }

        private void DeleteCustomerProfile()
        {
            try
            {
                customerVo = (CustomerVo)Session["CustomerVo"];
                userVo = (UserVo)Session[SessionContents.UserVo];

                if (customerBo.DeleteCustomer(customerVo.CustomerId, userVo.UserId))
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomer','none');", true);
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
                FunctionInfo.Add("Method", "ViewCustomerIndividualProfile.ascx:btnDelete_Click()");
                object[] objects = new object[3];
                objects[0] = customerVo;
                objects[1] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

    }
}