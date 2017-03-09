using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using VoUser;
using VoCustomerProfiling;
using BoCustomerProfiling;
using BoAdvisorProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.Base;
using BoCommon;

namespace WealthERP.Customer
{
    public partial class ViewCustomerIndividualProfile : System.Web.UI.UserControl
    {
        UserVo userVo = null;
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorStaffBo adviserStaffBo = new AdvisorStaffBo();
        string path;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                SessionBo.CheckSession();
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                RMVo customerRMVo = new RMVo();
                if (customerVo.SubType == "MNR")
                {
                    trGuardianName.Visible = true;
                }
                else
                {
                    trGuardianName.Visible = false;
                }
                if (userVo.UserType.Trim() == "Adviser" || userVo.UserType.Trim() == "RM" || userVo.UserType.Trim() == "Branch Man" || userVo.UserType.Trim() == "Advisor")
                {
                    trDelete.Visible = true;
                }
                else
                {
                    trDelete.Visible = false;
                }

                if (customerVo.ProfilingDate.Year == 1800 || customerVo.ProfilingDate==DateTime.MinValue)
                {
                    lblProfilingDate.Text = "";
                }
                else
                {
                    lblProfilingDate.Text = customerVo.ProfilingDate.ToShortDateString();
                }
                if (customerVo.Dob.Year == 1800 || customerVo.Dob==DateTime.MinValue)
                {
                    lblDob.Text = "";
                }
                else
                {
                    lblDob.Text = customerVo.Dob.ToShortDateString();
                }
                
                //hdnassociationcount.Value = customerBo.GetAssociationCount("C", customerVo.CustomerId).ToString();
                lblGuardianName.Text = customerVo.ContactFirstName + " " + customerVo.ContactMiddleName + " " + customerVo.ContactLastName;
                lblName.Text = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                lblCustCode.Text = customerVo.CustCode;
                //lblGender.Text = customerVo.Gender;
                if(customerVo.Gender.ToUpper().ToString()=="M")
                {
                    lblGender.Text = "Male";
                }
                else if(customerVo.Gender.ToUpper().ToString() == "F")
                {
                    lblGender.Text = "Female";
                }

                lblPanNum.Text = customerVo.PANNum;
                lblCorrLine1.Text = customerVo.Adr1Line1;
                lblCorrLine2.Text = customerVo.Adr1Line2;
                lblCorrLine3.Text = customerVo.Adr1Line3;
                lblCorrPinCode.Text = customerVo.Adr1PinCode.ToString();
                lblCorrCity.Text = customerVo.Adr1City;
                if (customerVo.BranchName != null && customerVo.BranchName != "")
                    lblBranch.Text = customerVo.BranchName;
                else
                    lblBranch.Text = "";
                customerRMVo = adviserStaffBo.GetAdvisorStaffDetails(customerVo.RmId);
                if (customerRMVo.FirstName + " " + customerRMVo.MiddleName + " " + customerRMVo.LastName != null && (customerRMVo.FirstName + " " + customerRMVo.MiddleName + " " + customerRMVo.LastName).ToString() != "")
                    lblRM.Text = customerRMVo.FirstName + " " + customerRMVo.MiddleName + " " + customerRMVo.LastName;
                else
                    lblRM.Text = "";

                if (customerVo.JobStartDate.Year == 1800 || customerVo.JobStartDate == DateTime.MinValue)
                {
                    lblJobStart.Text = "";
                }
                else
                {
                    lblJobStart.Text = customerVo.JobStartDate.ToShortDateString();
                }

                if (customerVo.ResidenceLivingDate.Year == 1800 || customerVo.ResidenceLivingDate == DateTime.MinValue)
                {
                    lblLiving.Text = "";
                }
                else
                {
                    lblLiving.Text = customerVo.ResidenceLivingDate.ToShortDateString();
                }

                if (customerVo.Adr1State == "")
                {
                    lblCorrState.Text = "";
                }
                else
                lblCorrState.Text = XMLBo.GetStateName(path, customerVo.Adr1State);

                lblMotherMaiden.Text = customerVo.MothersMaidenName;
                lblCorrCountry.Text = customerVo.Adr1Country;
                lblPermLine1.Text = customerVo.Adr2Line1;
                lblPermLine2.Text = customerVo.Adr2Line2;
                lblPermLine3.Text = customerVo.Adr2Line3;
                lblPermPinCode.Text = customerVo.Adr2PinCode.ToString();
                lblPermCity.Text = customerVo.Adr2City;
                if (customerVo.Adr2State == "")
                {
                    lblPermState.Text = "";
                }
                else
                lblPermState.Text = XMLBo.GetStateName(path,customerVo.Adr2State);

                lblPermCountry.Text = customerVo.Adr2Country;
                lblCompanyName.Text = customerVo.CompanyName;
                lblOfcLine1.Text = customerVo.OfcAdrLine1;
                lblOfcLine2.Text = customerVo.OfcAdrLine2;
                lblOfcLine3.Text = customerVo.OfcAdrLine3;
                lblOfcPinCode.Text = customerVo.OfcAdrPinCode.ToString();
                lblOfcCity.Text = customerVo.OfcAdrCity;

                if (customerVo.OfcAdrState == "")
                {
                    lblOfcState.Text = "";
                }
                else
                lblOfcState.Text = XMLBo.GetStateName(path, customerVo.OfcAdrState);
                lblOfcCountry.Text = customerVo.OfcAdrCountry;
                lblResPhone.Text = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
                lblOfcPhone.Text = customerVo.OfcISDCode.ToString() + "-" + customerVo.OfcSTDCode.ToString() + "-" + customerVo.OfcPhoneNum.ToString();
                lblOfcFax.Text = customerVo.OfcISDFax.ToString() + "-" + customerVo.OfcSTDFax.ToString() + "-" + customerVo.OfcFax.ToString();
                lblResFax.Text = customerVo.ISDFax.ToString() + "-" + customerVo.STDFax.ToString() + "-" + customerVo.Fax.ToString();
                lblMobile1.Text = customerVo.Mobile1.ToString();
                lblMobile2.Text = customerVo.Mobile2.ToString();
                lblEmail.Text = customerVo.Email;
                lblAltEmail.Text = customerVo.AltEmail;
                txtSlab.Text = customerVo.TaxSlab.ToString();
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

                if (customerVo.Occupation != null)
                    lblOccupation.Text = XMLBo.GetOccupationName(path, customerVo.Occupation);
                else
                    lblOccupation.Text = "";
                if (customerVo.MaritalStatus != null)
                    lblMaritalStatus.Text = XMLBo.GetMaritalStatusName(path, customerVo.MaritalStatus);
                else
                    lblMaritalStatus.Text = "";
                if (customerVo.MarriageDate.Year == 1800 || customerVo.MarriageDate == DateTime.MinValue)
                    lblMarriageDate.Text = "";
                else
                    lblMarriageDate.Text = customerVo.MarriageDate.ToShortDateString();
                if (customerVo.Qualification != null)
                    lblQualification.Text = XMLBo.GetQualificationName(path, customerVo.Qualification);
                else
                    lblQualification.Text = "";
                if (customerVo.Nationality != null)
                    lblNationality.Text = XMLBo.GetNationalityName(path, customerVo.Nationality);
                else
                    lblNationality.Text = "";
                lblRBIRefNo.Text = customerVo.RBIRefNum;
                if (customerVo.RBIApprovalDate.Year == 1800 || customerVo.RBIApprovalDate==DateTime.MinValue)
                    
                {
                    lblRBIRefDate.Text = "";
                }
                else
                {
                    lblRBIRefDate.Text = customerVo.RBIApprovalDate.ToShortDateString();
                }
                if (customerVo.Nationality != null)
                {
                    lblNationality.Text = XMLBo.GetNationalityName(path, customerVo.Nationality);
                }
                else
                    lblNationality.Text = "";
                lblType.Text = XMLBo.GetCustomerTypeName(path, customerVo.Type);
                lblSubType.Text = XMLBo.GetCustomerSubTypeName(path, customerVo.SubType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewCustomerIndividualProfile.ascx:Page_Load()");
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);   
        }

        protected void hiddenassociation_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                hdnassociationcount.Value = customerBo.GetAssociationCount("C", customerVo.CustomerId).ToString();
                string asc = Convert.ToString(hdnassociationcount.Value);

                if (asc == "0")
                
                    DeleteCustomerProfile();
                
            
            else
            
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showassocation();", true);
            }
        }

        protected void hiddenassociationfound_Click(object sender, EventArgs e)
        {
            string aso = Convert.ToString(hdnassociation.Value);
            if (aso == "1")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomer','none');", true);
            }

            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomer','none');", true);
            }
        }



        private void DeleteCustomerProfile()
        {
            try
            {
                customerVo = (CustomerVo)Session["CustomerVo"];
                userVo = (UserVo)Session[SessionContents.UserVo];


                if (customerBo.DeleteCustomer(customerVo.CustomerId,"D"))
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
                //objects[1] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        //protected void hiddenDelete_Click(object sender, EventArgs e)
        //{
        //    string val = Convert.ToString(hdnMsgValue.Value);
        //    if (val == "1")
        //    {
        //        DeleteCustomerProfile();
        //    }
           
        //}

        //protected void hiddenassociation_Click(object sender, EventArgs e)
        //{
        //    string val = Convert.ToString(hdnMsgValue.Value);
        //    if (val == "1")
        //    {
        //        DeleteCustomerProfile();
        //    }

        //}
    }
}
