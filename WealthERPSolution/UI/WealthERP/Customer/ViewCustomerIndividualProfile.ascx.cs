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
        
        string path;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                SessionBo.CheckSession();
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
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
                    lblProfilingDate.Text = customerVo.ProfilingDate.ToShortDateString().ToString();
                }
                if (customerVo.Dob.Year == 1800 || customerVo.Dob==DateTime.MinValue)
                {
                    lblDob.Text = "";
                }
                else
                {
                    lblDob.Text = customerVo.Dob.ToShortDateString().ToString();
                }
                
                //hdnassociationcount.Value = customerBo.GetAssociationCount("C", customerVo.CustomerId).ToString();
                lblGuardianName.Text = customerVo.ContactFirstName + " " + customerVo.ContactMiddleName + " " + customerVo.ContactLastName;
                lblName.Text = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                lblCustCode.Text = customerVo.CustCode.ToString();
                lblPanNum.Text = customerVo.PANNum.ToString();
                lblCorrLine1.Text = customerVo.Adr1Line1.ToString();
                lblCorrLine2.Text = customerVo.Adr1Line2.ToString();
                lblCorrLine3.Text = customerVo.Adr1Line3.ToString();
                lblCorrPinCode.Text = customerVo.Adr1PinCode.ToString();
                lblCorrCity.Text = customerVo.Adr1City.ToString();
                if (customerVo.BranchName != null && customerVo.BranchName.ToString() != "")
                    lblBranch.Text = customerVo.BranchName.ToString();
                else
                    lblBranch.Text = "";

                if (userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName != null && (userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName).ToString() != "")
                    lblRM.Text = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;
                else
                    lblRM.Text = "";

                if (customerVo.JobStartDate.Year == 1800 || customerVo.JobStartDate == DateTime.MinValue)
                {
                    lblJobStart.Text = "";
                }
                else
                {
                    lblJobStart.Text = customerVo.JobStartDate.ToShortDateString().ToString();
                }

                if (customerVo.ResidenceLivingDate.Year == 1800 || customerVo.ResidenceLivingDate == DateTime.MinValue)
                {
                    lblLiving.Text = "";
                }
                else
                {
                    lblLiving.Text = customerVo.ResidenceLivingDate.ToShortDateString().ToString();
                }

                if (customerVo.Adr1State == "")
                {
                    lblCorrState.Text = "";
                }
                else
                lblCorrState.Text = XMLBo.GetStateName(path, customerVo.Adr1State.ToString());

                lblMotherMaiden.Text = customerVo.MothersMaidenName.ToString();
                lblCorrCountry.Text = customerVo.Adr1Country.ToString();
                lblPermLine1.Text = customerVo.Adr2Line1.ToString();
                lblPermLine2.Text = customerVo.Adr2Line2.ToString();
                lblPermLine3.Text = customerVo.Adr2Line3.ToString();
                lblPermPinCode.Text = customerVo.Adr2PinCode.ToString();
                lblPermCity.Text = customerVo.Adr2City.ToString();
                if (customerVo.Adr2State == "")
                {
                    lblPermState.Text = "";
                }
                else
                lblPermState.Text = XMLBo.GetStateName(path,customerVo.Adr2State.ToString());

                lblPermCountry.Text = customerVo.Adr2Country.ToString();
                lblCompanyName.Text = customerVo.CompanyName.ToString();
                lblOfcLine1.Text = customerVo.OfcAdrLine1.ToString();
                lblOfcLine2.Text = customerVo.OfcAdrLine2.ToString();
                lblOfcLine3.Text = customerVo.OfcAdrLine3.ToString();
                lblOfcPinCode.Text = customerVo.OfcAdrPinCode.ToString();
                lblOfcCity.Text = customerVo.OfcAdrCity.ToString();

                if (customerVo.OfcAdrState == "")
                {
                    lblOfcState.Text = "";
                }
                else
                lblOfcState.Text = XMLBo.GetStateName(path, customerVo.OfcAdrState.ToString());
                lblOfcCountry.Text = customerVo.OfcAdrCountry.ToString();
                lblResPhone.Text = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
                lblOfcPhone.Text = customerVo.OfcISDCode.ToString() + "-" + customerVo.OfcSTDCode.ToString() + "-" + customerVo.OfcPhoneNum.ToString();
                lblOfcFax.Text = customerVo.OfcISDFax.ToString() + "-" + customerVo.OfcSTDFax.ToString() + "-" + customerVo.OfcFax.ToString();
                lblResFax.Text = customerVo.ISDFax.ToString() + "-" + customerVo.STDFax.ToString() + "-" + customerVo.Fax.ToString();
                lblMobile1.Text = customerVo.Mobile1.ToString();
                lblMobile2.Text = customerVo.Mobile2.ToString();
                lblEmail.Text = customerVo.Email.ToString();
                lblAltEmail.Text = customerVo.AltEmail.ToString();
                if (customerVo.Occupation != null)
                    lblOccupation.Text = XMLBo.GetOccupationName(path, customerVo.Occupation.ToString());
                else
                    lblOccupation.Text = "";
                if (customerVo.MaritalStatus != null)
                    lblMaritalStatus.Text = XMLBo.GetMaritalStatusName(path, customerVo.MaritalStatus.ToString());
                else
                    lblMaritalStatus.Text = "";
                if (customerVo.MarriageDate.Year == 1800 || customerVo.MarriageDate == DateTime.MinValue)
                    lblMarriageDate.Text = "";
                else
                    lblMarriageDate.Text = customerVo.MarriageDate.ToShortDateString();
                if (customerVo.Qualification != null)
                    lblQualification.Text = XMLBo.GetQualificationName(path, customerVo.Qualification.ToString());
                else
                    lblQualification.Text = "";
                if (customerVo.Nationality != null)
                    lblNationality.Text = XMLBo.GetNationalityName(path, customerVo.Nationality.ToString());
                else
                    lblNationality.Text = "";
                lblRBIRefNo.Text = customerVo.RBIRefNum.ToString();
                if (customerVo.RBIApprovalDate.Year == 1800 || customerVo.RBIApprovalDate==DateTime.MinValue)
                    
                {
                    lblRBIRefDate.Text = "";
                }
                else
                {
                    lblRBIRefDate.Text = customerVo.RBIApprovalDate.ToShortDateString().ToString();
                }
                if (customerVo.Nationality != null)
                {
                    lblNationality.Text = XMLBo.GetNationalityName(path, customerVo.Nationality.ToString());
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


                if (customerBo.DeleteCustomer(customerVo.CustomerId, userVo.UserId,"D"))
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