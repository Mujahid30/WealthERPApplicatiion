using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoUser;
using VoAdvisorProfiling;
using BoAdvisorProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class ViewBMProfile : System.Web.UI.UserControl
    {
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        UserVo userVo = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session["userVo"];

                ViewRMDetail();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ViewBMProfile.ascx.cs:Page_Load()");

                object[] objects = new object[1];
                objects[0] = userVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
         
        }
        public void ViewRMDetail()
        {
            RMVo Bmvo = new RMVo();
            try
            {
                Bmvo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);

                lblMail.Text = Bmvo.Email.ToString();
                lblFax.Text = Bmvo.FaxIsd.ToString() + "-" + Bmvo.FaxStd.ToString() + "-" + Bmvo.Fax.ToString();
                lblMobile.Text = Bmvo.Mobile.ToString();
                lblRMName.Text = Bmvo.FirstName.ToString() + " " + Bmvo.MiddleName.ToString() + "" + Bmvo.LastName.ToString();
                lblPhDirect.Text = Bmvo.OfficePhoneDirectIsd.ToString() + "-" + Bmvo.OfficePhoneDirectStd.ToString() + "-" + Bmvo.OfficePhoneDirectNumber.ToString();
                lblPhExt.Text = Bmvo.OfficePhoneExtIsd.ToString() + "-" + Bmvo.OfficePhoneExtStd.ToString() + "-" + Bmvo.OfficePhoneExtNumber.ToString();
                lblPhResi.Text = Bmvo.ResPhoneIsd.ToString() + "-" + Bmvo.ResPhoneStd.ToString() + "-" + Bmvo.ResPhoneNumber.ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ViewBMProfile.ascx.cs:ViewRMDetail()");

                object[] objects = new object[1];
                objects[0] = Bmvo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
    }
}