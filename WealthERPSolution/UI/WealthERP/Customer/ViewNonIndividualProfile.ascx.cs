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
                lblPanNum.Text = customerVo.PANNum.ToString();
                lblCompanyName.Text = customerVo.CompanyName;
                if (customerVo.BranchName != null)
                {
                    lblBranch.Text = customerVo.BranchName.ToString();
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
                lblPanNum.Text = customerVo.PANNum.ToString();
                if (customerVo.Adr1Line1 != null)
                {
                    lblCorrLine1.Text = customerVo.Adr1Line1.ToString();
                }
                if (customerVo.Adr1Line2 != null)
                {
                    lblCorrLine2.Text = customerVo.Adr1Line2.ToString();
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
                    lblCorrState.Text =null;
                }
                if (customerVo.Adr1Country != "")
                {
                    lblCorrCountry.Text = customerVo.Adr1Country.ToString();
                }
                if (customerVo.Adr2Line1 != null)
                {
                    lblPermLine1.Text = customerVo.Adr2Line1.ToString();
                }
                if (customerVo.Adr2Line2 != null)
                {
                    lblPermLine2.Text = customerVo.Adr2Line2.ToString();
                }
                if (customerVo.Adr2Line3 != null)
                {
                    lblPermLine3.Text = customerVo.Adr2Line3.ToString();
                }
                if (customerVo.Adr2PinCode != null)
                {
                    lblPermPinCode.Text = customerVo.Adr2PinCode.ToString();
                }
                if (customerVo.Adr2City != "")
                {
                    lblPermCity.Text = customerVo.Adr2City.ToString();
                }
                if (customerVo.Adr2State != null)
                {
                    lblPermState.Text = customerVo.Adr2State.ToString();

                }
                else
                {
                    lblPermState.Text = "";
                }
                if (customerVo.Adr2Country != null)
                {
                    lblPermCountry.Text = customerVo.Adr2Country.ToString();
                }
                lblResPhone.Text = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
                lblOfcPhone.Text = customerVo.OfcISDCode.ToString() + "-" + customerVo.OfcSTDCode.ToString() + "-" + customerVo.OfcPhoneNum.ToString();
                lblResFax.Text = customerVo.Fax.ToString() + "-" + customerVo.ISDFax.ToString() + "-" + customerVo.STDFax.ToString();
                if (customerVo.Email != null)
                {
                    lblEmail.Text = customerVo.Email.ToString();
                }
                if (customerVo.AltEmail != null)
                {
                    lblAltEmail.Text = customerVo.AltEmail.ToString();
                }
                lblType.Text = XMLBo.GetCustomerTypeName(path, customerVo.Type);
                lblSubType.Text = XMLBo.GetCustomerSubTypeName(path, customerVo.SubType);
                if (customerVo.Occupation != null)
                {
                    lblOccupation.Text = customerVo.OccupationId.ToString();
                }
                if (customerVo.AnnualIncome != null)
                {
                    lblAnnualIncome.Text = customerVo.AnnualIncome.ToString();
                }
                if (customerVo.Nationality != null)
                {
                    lblNationality.Text = customerVo.Nationality.ToString();
                }
                if (customerVo.MinNo1 != null)
                {
                    lblMinNo1.Text = customerVo.MinNo1.ToString();
                }
                if (customerVo.MinNo2 != null)
                {
                    lblMinNo2.Text = customerVo.MinNo2.ToString();
                }
                if (customerVo.MinNo3 != null)
                {
                    lblMinNo3.Text = customerVo.MinNo3.ToString();
                }
                if (customerVo.ESCNo != null)
                {
                    lblESCNo.Text = customerVo.ESCNo.ToString();
                }
                if (customerVo.UINNo != null)
                {
                    lblUINNo.Text = customerVo.UINNo.ToString();
                }
                if (customerVo.POA != null)
                {
                    lblPOA.Text = customerVo.POA.ToString();
                }
                if (customerVo.GuardianName != null)
                {
                    lblGuardianName.Text = customerVo.GuardianName.ToString();
                }
                if (customerVo.GuardianRelation != null)
                {
                    lblGuardianRelation.Text = customerVo.GuardianRelation.ToString();
                }
                if (customerVo.GuardPANNum != null)
                {
                    lblGuardianPANNum.Text = customerVo.ContactGuardianPANNum.ToString();
                }
                if (customerVo.GuardianMinNo != null)
                {
                    lblGuardianMinNo.Text = customerVo.GuardianMinNo.ToString();
                }
                if (customerVo.GuardianDob == DateTime.MinValue)
                {
                    lblGuardianDateOfBirth.Text = "";
                }
                else
                {
                    lblGuardianDateOfBirth.Text = customerVo.GuardianDob.ToShortDateString();
                }
                if (customerVo.OtherBankName != null)
                {
                    lblOtherBankName.Text = customerVo.OtherBankName.ToString();
                }
                if (customerVo.TaxStatus != null)
                {
                    lblTaxStatus.Text = customerVo.TaxStatus.ToString();
                }
                if (customerVo.Category != null)
                {
                    lblCategory.Text = customerVo.Category.ToString();
                }
                if (customerVo.Adr1City != null)
                {
                    lblOtherCity.Text = customerVo.Adr1City.ToString();
                }
                if (customerVo.Adr1State != null)
                {
                    lblOtherState.Text = customerVo.Adr1State.ToString();
                }
                if (customerVo.OtherCountry != null)
                {
                    lblOtherCountry.Text = customerVo.OtherCountry.ToString();
                }
                if (customerVo.Mobile1 != null)
                {
                    lblMobile1.Text = customerVo.Mobile1.ToString();
                }
                if (customerVo.Mobile2 != null)
                {
                    lblMobile2.Text = customerVo.Mobile2.ToString();
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


