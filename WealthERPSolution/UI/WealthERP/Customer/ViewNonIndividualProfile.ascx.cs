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
                lblName.Text = customerVo.ContactFirstName+ " " + customerVo.ContactMiddleName + " " + customerVo.ContactLastName;
                lblCustomerCode.Text = customerVo.CustCode.ToString();
                lblPanNum.Text = customerVo.PANNum.ToString();
                lblCompanyName.Text = customerVo.FirstName;
                if (customerVo.BranchName != null)
                {
                    lblBranch.Text = customerVo.BranchName.ToString();
                }
                if (customerVo.RegistrationDate == DateTime.MinValue)
                {
                    lblRegistrationDate.Text = "";
                }
                else
                {
                    lblRegistrationDate.Text = customerVo.RegistrationDate.ToShortDateString();
                }
                if (customerVo.CommencementDate == DateTime.MinValue)
                {
                    lblCommencementDate.Text = "";
                }
                else
                {
                    lblCommencementDate.Text = customerVo.CommencementDate.ToShortDateString();
                }
                lblRegistrationNum.Text = customerVo.RegistrationNum.ToString();
                lblRegistrationPlace.Text = customerVo.RegistrationPlace.ToString();
                lblCompanyWebsite.Text = customerVo.CompanyWebsite.ToString();
                if (customerVo.DummyPAN == 1)
                {
                    chkdummypan.Checked = true;
                }
                else
                {
                    chkdummypan.Checked = false;
                }

                lblCustomerCode.Text = customerVo.CustCode.ToString();
                customerRMVo = adviserStaffBo.GetAdvisorStaffDetails(customerVo.RmId);
                if (customerRMVo.FirstName + " " + customerRMVo.MiddleName + " " + customerRMVo.LastName != null && (customerRMVo.FirstName + " " + customerRMVo.MiddleName + " " + customerRMVo.LastName).ToString() != "")
                    lblRM.Text = customerRMVo.FirstName + " " + customerRMVo.MiddleName + " " + customerRMVo.LastName;
                else
                    lblRM.Text = "";
                lblPanNum.Text = customerVo.PANNum.ToString();
                lblCorrLine1.Text = customerVo.Adr1Line1.ToString();
                lblCorrLine2.Text = customerVo.Adr1Line2.ToString();
                lblCorrLine3.Text = customerVo.Adr1Line3.ToString();
                lblCorrPinCode.Text = customerVo.Adr1PinCode.ToString();
                lblCorrCity.Text = customerVo.Adr1City.ToString();
                if (customerVo.Adr1State != "")
                {
                    lblCorrState.Text = XMLBo.GetStateName(path, customerVo.Adr1State);
                    
                }
                else
                {
                    lblCorrState.Text = "";
                }
                lblCorrCountry.Text = customerVo.Adr1Country.ToString();
                lblPermLine1.Text = customerVo.Adr2Line1.ToString();
                lblPermLine2.Text = customerVo.Adr2Line2.ToString();
                lblPermLine3.Text = customerVo.Adr2Line3.ToString();
                lblPermPinCode.Text = customerVo.Adr2PinCode.ToString();
                lblPermCity.Text = customerVo.Adr2City.ToString();
                if (customerVo.Adr2State.ToString() !=string.Empty)

                {
                    lblPermState.Text = XMLBo.GetStateName(path, customerVo.Adr2State);
                    
                }
                else
                {
                    lblPermState.Text = "";
                }
                lblPermCountry.Text = customerVo.Adr2Country.ToString();
                lblResPhone.Text = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
                lblOfcPhone.Text = customerVo.OfcISDCode.ToString() + "-" + customerVo.OfcSTDCode.ToString() + "-" + customerVo.OfcPhoneNum.ToString();
                lblResFax.Text = customerVo.Fax.ToString() + "-" + customerVo.ISDFax.ToString() + "-" + customerVo.STDFax.ToString();
                lblEmail.Text = customerVo.Email.ToString();
                lblAltEmail.Text = customerVo.AltEmail.ToString();
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
   

