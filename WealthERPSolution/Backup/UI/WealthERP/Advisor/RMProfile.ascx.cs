using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WealthERP.Advisor;
using VoUser;
using BoUser;
using BoAdvisorProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;

namespace WealthERP
{
    public partial class RMProfile : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        
        RMVo rmVo = new RMVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            rmVo = (RMVo)Session["rmVo"];
            
           
            showRM();
           
        }
        public void showRM()
        {
            try
            {
                txtEmail.Text = rmVo.Email.ToString();
                txtFirstName.Text = rmVo.FirstName.ToString();
                txtLastName.Text = rmVo.LastName.ToString();
                txtMiddleName.Text = rmVo.MiddleName.ToString();
                txtMobileNumber.Text = rmVo.Mobile.ToString();
                txtPhDirectISD.Text = rmVo.OfficePhoneDirectIsd.ToString();
                txtPhDirectISD0.Text = rmVo.OfficePhoneExtIsd.ToString();
                txtPhDirectISD1.Text = rmVo.ResPhoneIsd.ToString();
                txtPhDirectISD2.Text = rmVo.FaxIsd.ToString();
                txtPhDirectPhoneNumber.Text = rmVo.OfficePhoneDirectNumber.ToString();
                txtPhDirectPhoneNumber0.Text = rmVo.OfficePhoneExtNumber.ToString();
                txtPhDirectPhoneNumber1.Text = rmVo.ResPhoneNumber.ToString();
                txtPhDirectPhoneNumber2.Text = rmVo.Fax.ToString();
                txtPhDirectSTD.Text = rmVo.OfficePhoneDirectStd.ToString();
                txtPhDirectSTD0.Text = rmVo.OfficePhoneExtStd.ToString();
                txtPhDirectSTD1.Text = rmVo.ResPhoneStd.ToString();
                txtPhDirectSTD2.Text = rmVo.FaxStd.ToString();

                txtEmail.Enabled = false;
                txtFirstName.Enabled = false;
                txtLastName.Enabled = false;
                txtMiddleName.Enabled = false;
                txtMobileNumber.Enabled = false;
                txtPhDirectISD.Enabled = false;
                txtPhDirectISD0.Enabled = false;
                txtPhDirectISD1.Enabled = false;
                txtPhDirectISD2.Enabled = false;
                txtPhDirectPhoneNumber.Enabled = false;
                txtPhDirectPhoneNumber0.Enabled = false;
                txtPhDirectPhoneNumber1.Enabled = false;
                txtPhDirectPhoneNumber2.Enabled = false;
                txtPhDirectSTD.Enabled = false;
                txtPhDirectSTD0.Enabled = false;
                txtPhDirectSTD1.Enabled = false;
                txtPhDirectSTD2.Enabled = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMProfile.ascx:showRM()");


                object[] objects = new object[2];
                objects[0] = userVo;
                objects[1] = rmVo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
       
    }
}