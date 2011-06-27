using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using VoCustomerProfiling;
using BoUser;
using BoCustomerProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;
using System.Configuration;
using System.Text;
namespace WealthERP.Advisor
{
    public partial class RMCustomerNonIndividualDasboard : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        CustomerVo customerVo;
        RMVo rmVo;
        CustomerBo customerBo = new CustomerBo(); 
        string path;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            customerVo = new CustomerVo();
            rmVo = new RMVo();

            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            try
            {
                customerVo = (CustomerVo)Session["CustomerVo"];
                rmVo = (RMVo)Session["RmVo"];

                StringBuilder sbAddress = new StringBuilder();

                lblName.Text = customerVo.LastName.ToString();
                lblPanNum.Text = customerVo.PANNum.ToString();
                lblPhone.Text = customerVo.ResISDCode.ToString() + " - " + customerVo.ResSTDCode.ToString() + " - " + customerVo.ResPhoneNum.ToString();

                sbAddress.Append(customerVo.Adr1Line1.ToString());
                sbAddress.Append("<br />");
                sbAddress.Append(customerVo.Adr1Line2.ToString());
                sbAddress.Append("<br />");
                sbAddress.Append(customerVo.Adr1Line3.ToString());
                sbAddress.Append("<br />");
                sbAddress.Append(customerVo.Adr1PinCode.ToString());
                sbAddress.Append("<br />");

                if (customerVo.Adr1State.ToString() != "")
                {
                    sbAddress.Append(XMLBo.GetStateName(path, customerVo.Adr1State.ToString()));
                    sbAddress.Append("<br />");
                }

                sbAddress.Append(customerVo.Adr1City.ToString());
                sbAddress.Append("<br />");
                sbAddress.Append(customerVo.Adr1Country.ToString());

                lblContactPerson.Text = customerVo.ContactFirstName.ToString() + " " + customerVo.ContactMiddleName.ToString() + " " + customerVo.ContactLastName.ToString();
                lblEmail.Text = customerVo.Email.ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RMCustomerNonIndividualDasboard.ascx:Page_Load()");
                object[] objects = new object[2];                
                objects[0] = rmVo;
                objects[1] = customerVo;
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
    }
}