using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using VoAdvisorProfiling;
using BoUser;
using BoAdvisorProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class RMEditProfile : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session["userVo"];
                rmVo = (RMVo)Session["rmVo"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMEditProfile.ascx.cs:Page_Load()");

                object[] objects = new object[2];
                objects[0] = userVo;
                objects[1] = rmVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
          

        }


        public void showRM()
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

        }
        
        public bool Validation()
        {
            bool result = true;
            try
            {
                
                if (!ChkMailId(txtEmail.Text.ToString()))
                {
                    result = false;
                    lblEmail.ForeColor = System.Drawing.Color.Red;
                }

                if (txtFirstName.Text.ToString() == "")
                {
                    lblRMName.ForeColor = System.Drawing.Color.Red;
                    result = false;

                }

                if (txtPhDirectPhoneNumber.Text == "")
                {
                    lblPhoneDirect.ForeColor = System.Drawing.Color.Red;
                    result = false;
                }
                if (txtPhDirectISD.Text == " ")
                {
                    lblSTD.ForeColor = System.Drawing.Color.Red;

                    result = false;
                }
                if (txtPhDirectISD.Text == "")
                {
                    lblISD.ForeColor = System.Drawing.Color.Red;
                    result = false;
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

                FunctionInfo.Add("Method", "RMEditProfile.ascx.cs:Validation()");

                object[] objects = new object[1];
                objects[0] = result;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;

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

                FunctionInfo.Add("Method", "RMEditProfile.ascx.cs:ChkMailId()");

                object[] objects = new object[2];
                objects[0] = email;
                objects[1] = bResult;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
          
            return bResult;
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            RMVo rmvo = new RMVo();
            UserBo userBo = new UserBo();
            try
            {
                if (Validation())
                {
                  
                    rmvo.RMId = rmVo.RMId;
                    

                    userVo.Email = txtEmail.Text.ToString();
                    userVo.FirstName = txtFirstName.Text.ToString();
                    userVo.LastName = txtLastName.Text.ToString();
                    userVo.MiddleName = txtMiddleName.Text.ToString();
                    userBo.UpdateUser(userVo);

                    rmvo.Email = txtEmail.Text.ToString();
                    rmvo.Fax = int.Parse(txtPhDirectPhoneNumber2.Text.ToString());
                    rmvo.OfficePhoneDirectNumber = int.Parse(txtPhDirectPhoneNumber.Text.ToString());
                    rmvo.OfficePhoneExtNumber = int.Parse(txtPhDirectPhoneNumber0.Text.ToString());
                    rmvo.ResPhoneNumber = int.Parse(txtPhDirectPhoneNumber1.Text.ToString());
                    rmvo.FaxIsd = int.Parse(txtPhDirectISD2.Text.ToString());
                    rmvo.OfficePhoneDirectIsd = int.Parse(txtPhDirectISD.Text.ToString());
                    rmvo.OfficePhoneExtIsd = int.Parse(txtPhDirectISD0.Text.ToString());
                    rmvo.ResPhoneIsd = int.Parse(txtPhDirectISD1.Text.ToString());
                    rmvo.FaxStd = int.Parse(txtPhDirectSTD2.Text.ToString());
                    rmvo.OfficePhoneDirectStd = int.Parse(txtPhDirectSTD.Text.ToString());
                    rmvo.OfficePhoneExtStd = int.Parse(txtPhDirectSTD0.Text.ToString());
                    rmvo.ResPhoneStd = int.Parse(txtPhDirectSTD1.Text.ToString());
                    rmvo.FirstName = userVo.FirstName.ToString();
                    rmvo.LastName = userVo.MiddleName.ToString();
                    rmvo.MiddleName = userVo.LastName.ToString();
                    rmvo.Mobile = int.Parse(txtMobileNumber.Text.ToString());

                    advisorStaffBo.UpdateStaff(rmvo);
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
                FunctionInfo.Add("Method", "RMEditProfile.ascx.cs:btnSaveChanges_Click()");
                object[] objects = new object[2];
                objects[0] = rmVo;
                objects[1] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }
}
