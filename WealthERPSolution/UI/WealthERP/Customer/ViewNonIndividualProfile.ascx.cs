using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoUser;
using BoCustomerProfiling;
using BoAdvisorProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.Base;
using BoCommon;
using System.Configuration;
namespace WealthERP.Customer
{
    public partial class ViewNonIndividualProfile : System.Web.UI.UserControl
    {
        UserVo userVo = null;
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        RMVo customerRMVo = new RMVo();
        AdvisorStaffBo adviserStaffBo = new AdvisorStaffBo();
        string path = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];



                if (userVo.UserType.Trim() == "Adviser" || userVo.UserType.Trim() == "RM" || userVo.UserType.Trim() == "Branch Man" || userVo.UserType.Trim() == "Advisor")
                {
                    trDelete.Visible = true;
                }
                else
                {
                    trDelete.Visible = false;
                }
                if (customerVo.ProfilingDate == DateTime.MinValue)
                {
                    lblProfilingDate.Text = "";
                }
                else
                {
                    lblProfilingDate.Text = customerVo.ProfilingDate.ToShortDateString();
                }

                lblType.Text = XMLBo.GetCustomerTypeName(path, customerVo.Type);
                lblSubType.Text = XMLBo.GetCustomerSubTypeName(path, customerVo.SubType);
                lblName.Text = customerVo.ContactFirstName + " " + customerVo.ContactMiddleName + " " + customerVo.ContactLastName;
                lblCustomerCode.Text = customerVo.CustCode.ToString();
                if (customerVo.PANNum != null)
                {
                    lblPanNum.Text = customerVo.PANNum.ToString();
                }
                else
                {
                    lblPanNum.Text = null;
                }
                if (customerVo.CompanyName != null)
                {
                    lblCompanyName.Text = customerVo.CompanyName;
                }
                else
                {
                    lblCompanyName.Text = null;
                }
                if (customerVo.BranchName != null)
                {
                    lblBranch.Text = customerVo.BranchName.ToString();
                }
                else
                {
                    lblBranch.Text = null;
                }
                if (customerVo.RegistrationDate == DateTime.MinValue)
                {
                    lblRegistrationDate.Text = null;
                }
                else
                {
                    lblRegistrationDate.Text = customerVo.RegistrationDate.ToShortDateString();
                }
                if (customerVo.CommencementDate == DateTime.MinValue)
                {
                    lblCommencementDate.Text = null;
                }
                else
                {
                    lblCommencementDate.Text = customerVo.CommencementDate.ToShortDateString();
                }
                if (customerVo.RegistrationNum != null)
                {
                    lblRegistrationNum.Text = customerVo.RegistrationNum.ToString();
                }
                else
                {
                    lblRegistrationNum.Text = null;
                }
                if (customerVo.RegistrationPlace != null)
                {
                    lblRegistrationPlace.Text = customerVo.RegistrationPlace.ToString();
                }
                else
                {
                    lblRegistrationPlace.Text = null;
                }
                if (customerVo.CompanyWebsite != null)
                {
                    lblCompanyWebsite.Text = customerVo.CompanyWebsite.ToString();
                }
                else
                {
                    lblCompanyWebsite.Text = null;
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

                lblCustomerCode.Text = customerVo.CustCode.ToString();
                customerRMVo = adviserStaffBo.GetAdvisorStaffDetails(customerVo.RmId);
                if (customerRMVo.FirstName + " " + customerRMVo.MiddleName + " " + customerRMVo.LastName != null && (customerRMVo.FirstName + " " + customerRMVo.MiddleName + " " + customerRMVo.LastName).ToString() != "")
                    lblRM.Text = customerRMVo.FirstName + " " + customerRMVo.MiddleName + " " + customerRMVo.LastName;
                else
                    lblRM.Text = "";
                if (customerVo.PANNum != null)
                {
                    lblPanNum.Text = customerVo.PANNum.ToString();
                }
                else
                {
                    lblPanNum.Text = null;
                }
                if (customerVo.Adr1Line1 != null)
                {
                    lblCorrLine1.Text = customerVo.Adr1Line1.ToString();
                }
                else
                {
                    lblCorrLine1.Text = null;
                }
                if (customerVo.Adr1Line2 != null)
                {
                    lblCorrLine2.Text = customerVo.Adr1Line2.ToString();
                }
                else
                {
                    lblCorrLine2.Text = null;
                }
                if (customerVo.Adr1Line3 != null)
                {
                    lblCorrLine3.Text = customerVo.Adr1Line3.ToString();
                }
                else
                {
                    lblCorrLine3.Text = null;
                }
                if (customerVo.Adr1PinCode != null)
                {
                    lblCorrPinCode.Text = customerVo.Adr1PinCode.ToString();
                }
                else
                {
                    lblCorrPinCode.Text = null;
                }
                if (customerVo.Adr1City != null)
                {
                    lblCorrCity.Text = customerVo.Adr1City.ToString();
                }
                else
                {
                    lblCorrCity.Text = null;
                }
                if (customerVo.Adr1State != "")
                {
                    lblCorrState.Text = customerVo.Adr1State.ToString();

                }
                else
                {
                    lblCorrState.Text = null;
                }
                if (customerVo.Adr1Country != "")
                {
                    lblCorrCountry.Text = customerVo.Adr1Country.ToString();
                }
                else
                {
                    lblCorrCountry.Text = null;
                }
                if (customerVo.Adr2Line1 != null)
                {
                    lblPermLine1.Text = customerVo.Adr2Line1.ToString();
                }
                else
                {
                    lblPermLine1.Text = null;
                }
                if (customerVo.Adr2Line2 != null)
                {
                    lblPermLine2.Text = customerVo.Adr2Line2.ToString();
                }
                else
                {
                    lblPermLine2.Text = null;
                }
                if (customerVo.Adr2Line3 != null)
                {
                    lblPermLine3.Text = customerVo.Adr2Line3.ToString();
                }
                else
                {
                    lblPermLine3.Text = null;
                }
                if (customerVo.Adr2PinCode != null)
                {
                    lblPermPinCode.Text = customerVo.Adr2PinCode.ToString();
                }
                else
                {
                    lblPermPinCode.Text = null;
                }
                if (customerVo.Adr2City == null)
                {
                    lblPermCity.Text = null;
                }
                else
                {
                    lblPermCity.Text = customerVo.Adr2City.ToString();
                }
                if (customerVo.Adr2State != null)
                {
                    lblPermState.Text = customerVo.Adr2State.ToString();

                }
                else
                {
                    lblPermState.Text = null;
                }
                if (customerVo.Adr2Country != null)
                {
                    lblPermCountry.Text = customerVo.Adr2Country.ToString();
                }
                else
                {
                    lblPermCountry.Text = null;
                }
                lblResPhone.Text = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
                lblOfcPhone.Text = customerVo.OfcISDCode.ToString() + "-" + customerVo.OfcSTDCode.ToString() + "-" + customerVo.OfcPhoneNum.ToString();
                lblResFax.Text = customerVo.Fax.ToString() + "-" + customerVo.ISDFax.ToString() + "-" + customerVo.STDFax.ToString();
                if (customerVo.Email != null)
                {
                    lblEmail.Text = customerVo.Email.ToString();
                }
                else
                {
                    lblEmail.Text = null;
                }
                if (customerVo.AltEmail != null)
                {
                    lblAltEmail.Text = customerVo.AltEmail.ToString();
                }
                else
                {
                    lblAltEmail.Text = null;
                }
                lblType.Text = XMLBo.GetCustomerTypeName(path, customerVo.Type);
                lblSubType.Text = XMLBo.GetCustomerSubTypeName(path, customerVo.SubType);
                if (customerVo.Occupation != null)
                {
                    lblOccupation.Text = customerVo.OccupationId.ToString();
                }

                else
                {
                    lblOccupation.Text = null;
                }
                if (customerVo.AnnualIncome != null)
                {
                    lblAnnualIncome.Text = customerVo.AnnualIncome.ToString();
                }
                else
                {
                    lblAnnualIncome.Text = null;
                }
                if (customerVo.Nationality != null)
                {
                    lblNationality.Text = customerVo.Nationality.ToString();
                }
                else
                {
                    lblNationality.Text = null;
                }
                if (customerVo.MinNo1 != null)
                {
                    lblMinNo1.Text = customerVo.MinNo1.ToString();
                }
                else
                {
                    lblMinNo1.Text = null;
                }
                if (customerVo.MinNo2 != null)
                {
                    lblMinNo2.Text = customerVo.MinNo2.ToString();
                }
                else
                {
                    lblMinNo2.Text = null;
                }
                if (customerVo.MinNo3 != null)
                {
                    lblMinNo3.Text = customerVo.MinNo3.ToString();
                }
                {
                    lblMinNo3.Text = null;
                }
                if (customerVo.ESCNo != null)
                {
                    lblESCNo.Text = customerVo.ESCNo.ToString();
                }
                else
                {
                   lblESCNo.Text = null;
                }
                if (customerVo.UINNo != null)
                {
                    lblUINNo.Text = customerVo.UINNo.ToString();
                }
                else
                {
                    lblUINNo.Text = null;
                }
                if (customerVo.POA != null)
                {
                    lblPOA.Text = customerVo.POA.ToString();
                }
                else
                {
                    lblPOA.Text = null;
                }
                if (customerVo.GuardianName != null)
                {
                    lblGuardianName.Text = customerVo.GuardianName.ToString();
                }
                else
                {
                    lblGuardianName.Text = null;
                }
                if (customerVo.GuardianRelation != null)
                {
                    lblGuardianRelation.Text = customerVo.GuardianRelation.ToString();
                }
                else
                {
                    lblGuardianRelation.Text = null;
                }
                if (customerVo.GuardPANNum != null)
                {
                    lblGuardianPANNum.Text = customerVo.ContactGuardianPANNum.ToString();
                }
                else
                {
                    lblGuardianPANNum.Text = null;
                }
                if (customerVo.GuardianMinNo != null)
                {
                    lblGuardianMinNo.Text = customerVo.GuardianMinNo.ToString();
                }
                else
                {
                    lblGuardianMinNo.Text = null;
                }
                if (customerVo.GuardianDob == DateTime.MinValue)
                {
                    lblGuardianDateOfBirth.Text =null;
                }
                else
                {
                    lblGuardianDateOfBirth.Text = customerVo.GuardianDob.ToShortDateString();
                }
                if (customerVo.OtherBankName != null)
                {
                    lblOtherBankName.Text = customerVo.OtherBankName.ToString();
                }
                else
                {
                    lblOtherBankName.Text = null;
                }
                if (customerVo.TaxStatus != null)
                {
                    lblTaxStatus.Text = customerVo.TaxStatus.ToString();
                }
                else
                {
                    lblTaxStatus.Text = null;
                }
                if (customerVo.Category != null)
                {
                    lblCategory.Text = customerVo.Category.ToString();
                }
                else
                {
                    lblCategory.Text = null;
                }
                if (customerVo.Adr1City != null)
                {
                    lblOtherCity.Text = customerVo.Adr1City.ToString();
                }
                else
                {
                    lblOtherCity.Text = null;
                }
                if (customerVo.Adr1State != null)
                {
                    lblOtherState.Text = customerVo.Adr1State.ToString();
                }
                else
                {
                    lblOtherState.Text = null;
                }
                if (customerVo.OtherCountry != null)
                {
                    lblOtherCountry.Text = customerVo.OtherCountry.ToString();
                }
                else
                {
                    lblOtherCountry.Text = null;
                }
                if (customerVo.Mobile1 != null)
                {
                    lblMobile1.Text = customerVo.Mobile1.ToString();
                }
                else
                {
                    lblMobile1.Text = null;
                }
                if (customerVo.Mobile2 != null)
                {
                    lblMobile2.Text = customerVo.Mobile2.ToString();
                }
                else
                {
                    lblMobile2.Text = null;
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
                FunctionInfo.Add("Method", "ViewNonIndividualProfile.ascx:Page_Load()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[2] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                customerVo = (CustomerVo)Session["CustomerVo"];
                userVo = (UserVo)Session[SessionContents.UserVo];
                hdnassociationcount.Value = customerBo.GetAssociationCount("C", customerVo.CustomerId).ToString();
                string asc = Convert.ToString(hdnassociationcount.Value);

                if (asc == "0")
                {

                    if (customerBo.DeleteCustomer(customerVo.CustomerId, "D"))
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomer','none');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showassocation();", true);
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
                FunctionInfo.Add("Method", "ViewNonIndividualProfile.ascx:btnDelete_Click()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[2] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void hiddenassociation_Click(object sender, EventArgs e)
        {

            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomer','none');", true);

            }

        }




    }
}


