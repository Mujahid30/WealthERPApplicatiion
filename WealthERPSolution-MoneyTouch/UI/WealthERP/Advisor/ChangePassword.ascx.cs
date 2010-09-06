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


namespace WealthERP.Advisor
{
    public partial class ChangePassword : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        UserBo userBo = new UserBo();
        AdvisorVo advisorVo = new AdvisorVo();
        int tempPass;
        //int changeTemp;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                if (Session["CustomerUser"] != null)
                {
                    userVo = (UserVo)Session["CustomerUser"];
                    tempPass = 1;
                }
                else
                {
                    userVo = (UserVo)Session["UserVo"];
                    tempPass = 0;
                }

                txtCurrentPassword.Text = "";
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ChangePassword.ascx:Page_Load()");
                object[] objects = new object[1];               
                objects[0] = userVo;               
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
              

                if (txtCurrentPassword.Text == Encryption.Decrypt(userVo.Password.ToString()))
                {
                    if (txtNewPassword.Text == txtConfirmPassword.Text)
                    {
                        userVo.Password = Encryption.Encrypt(txtConfirmPassword.Text.ToString());
                        userBo.ChangePassword(userVo.UserId, userVo.Password, tempPass);
                        
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('Your Password Changed Successfully..!');", true);
                        if(Session["ChangeTempPass"]!=null && Session["ChangeTempPass"].ToString()=="Y")
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('Userlogin','none');", true);
                            
                       // else
                            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('EditUserDetails','none');", true);

                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('Check the password');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('Current password is not correct..!');", true);
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
                FunctionInfo.Add("Method", "ChangePassword.ascx:btnSave_Click()");
                object[] objects = new object[2];
                objects[0] = advisorVo;
                objects[1] = userVo;               

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            
        }
    }
}
