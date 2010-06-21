using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoAdvisorProfiling;
using BoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;
using System.Configuration;

namespace WealthERP.Advisor
{
    public partial class AdvisorProfile : System.Web.UI.UserControl
    {

        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorBo advisorBo = new AdvisorBo();
        UserVo userVo = new UserVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session["userVo"];
                advisorVo = advisorBo.GetAdvisorUser(userVo.UserId);
                string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                if (advisorVo.BusinessCode == "" || advisorVo.BusinessCode == null)
                {
                    lblBusinessType.Text = "";
                }
                else
                    lblBusinessType.Text = XMLBo.GetBusinessTypeName(path, advisorVo.BusinessCode.Trim().ToString());
                lblCity.Text = advisorVo.City.ToString();
                if (advisorVo.ContactPersonMiddleName == null)
                    advisorVo.ContactPersonMiddleName = string.Empty;

                if (advisorVo.ContactPersonLastName == null)
                    advisorVo.ContactPersonLastName = string.Empty;

                lblContactPerson.Text = advisorVo.ContactPersonFirstName.ToString() + " " + advisorVo.ContactPersonMiddleName.ToString() + " " + advisorVo.ContactPersonLastName.ToString();
                if (advisorVo.Country != null && advisorVo.Country != string.Empty)
                    lblCountry.Text = advisorVo.Country.ToString();

                lblMail.Text = advisorVo.Email.ToString();

                //if (advisorVo.Website ==null || advisorVo.Website == strin )
                //{
                //    lblwsite.Text = "";
                //}
                //else
                if (advisorVo.Website != null)
                    lblwsite.Text = advisorVo.Website.ToString();

                lblMobile.Text = advisorVo.MobileNumber.ToString();

                lblOrgName.Text = advisorVo.OrganizationName.ToString();

                if (advisorVo.Phone1Isd > 1 && advisorVo.Phone1Std > 1 && advisorVo.Phone1Number > 1)
                    lblPhNumber1.Text = advisorVo.Phone1Isd.ToString() + "-" + advisorVo.Phone1Std.ToString() + "-" + advisorVo.Phone1Number.ToString();
                if (advisorVo.Phone2Isd > 1 && advisorVo.Phone2Std > 1 && advisorVo.Phone2Number > 1)
                    lblPhNumber2.Text = advisorVo.Phone2Isd.ToString() + "-" + advisorVo.Phone2Std.ToString() + "-" + advisorVo.Phone2Number.ToString();

                if (advisorVo.PinCode > 0)
                    lblPin.Text = advisorVo.PinCode.ToString();

                //if (advisorVo.State == "" || advisorVo.State == null)
                //{
                //    lblstate.Text = "";
                //}
                //else
                if (advisorVo.State != null && advisorVo.State != string.Empty)
                    lblstate.Text = XMLBo.GetStateName(path, advisorVo.State.ToString());

                if (advisorVo.AddressLine1 != null)
                    lblLine_1.Text = advisorVo.AddressLine1.ToString();
                if (advisorVo.AddressLine2 != null)
                    lblLine_2.Text = advisorVo.AddressLine2.ToString();
                if (advisorVo.AddressLine3 != null)
                    lblLine_3.Text = advisorVo.AddressLine3.ToString();
                if (advisorVo.FaxIsd != 0 && advisorVo.FaxStd != 0 && advisorVo.Fax != 0)
                    lblFax.Text = advisorVo.FaxIsd.ToString() + "-" + advisorVo.FaxStd.ToString() + "-" + advisorVo.Fax.ToString();

                if (advisorVo.MultiBranch == 0)
                {
                    lblMultiBranch.Text = "No";
                }
                else { lblMultiBranch.Text = "Yes"; }

                if (advisorVo.Associates == 0)
                {
                    lblmtype.Text = "No";
                }
                else { lblmtype.Text = "Yes"; }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorProfile.ascx:PageLoad()");


                object[] objects = new object[3];
                objects[0] = advisorVo;
                objects[1] = userVo;
                objects[2] = advisorBo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }

    }
}