using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoUser;

namespace WealthERP
{
    public partial class RegistrationConfirmation : System.Web.UI.Page
    {
        UserVo userVo = new UserVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               // userVo = (UserVo)Session["userVo"];
                // lblPass.Text =Encryption.Decrypt(userVo.Password.ToString());
                //lblUser.Text = userVo.LoginId.ToString();
                if (Session["RegistrationMailSent"] != null && !Convert.ToBoolean(Session["RegistrationMailSent"]))
                {

                    //lblRegistrationEmail.Text = "There was an error while sending registration email.";

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

                FunctionInfo.Add("Method", "UserLoginMessage.ascx.cs:Page_Load()");

                object[] objects = new object[1];
                objects[0] = userVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void lnkUserLogin_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('Userlogin','none');", true);
        }

        
    }
}
