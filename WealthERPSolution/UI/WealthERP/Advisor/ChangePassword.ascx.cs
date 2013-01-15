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
using VoAdvisorProfiling;


namespace WealthERP.Advisor
{
    public partial class ChangePassword : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        UserBo userBo = new UserBo();
        AdvisorVo advisorVo = new AdvisorVo();
        int tempPass;
        OneWayEncryption encryption = new OneWayEncryption();
        AdvisorPreferenceVo advisorPreferenceVo = new AdvisorPreferenceVo();
        //int changeTemp;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                advisorPreferenceVo = (AdvisorPreferenceVo)Session["AdvisorPreferenceVo"];
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
                hdnuname.Value = userVo.LoginId;
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
            bool isValidPwd=false;
            string password = string.Empty;
            string pwdSaltValue = string.Empty;
            try
            {

                if (txtNewPassword.Text.Trim() != userVo.LoginId)
                {
                    if (!string.IsNullOrEmpty(userVo.PasswordSaltValue))
                    {
                        isValidPwd = encryption.VerifyHashString(txtCurrentPassword.Text.Trim(), userVo.Password.Trim(), userVo.PasswordSaltValue.Trim());
                    }
                    else
                    {
                        if (txtCurrentPassword.Text.Trim() == Encryption.Decrypt(userVo.Password.ToString()))
                            isValidPwd = true;
                    }
                    if (isValidPwd)
                    {
                        if (txtNewPassword.Text.Trim() == txtConfirmPassword.Text.Trim())
                        {
                            //userVo.Password = txtConfirmPassword.Text.ToString();
                            if (!string.IsNullOrEmpty(userVo.PasswordSaltValue))
                            {
                                 encryption.GetHashAndSaltString(txtConfirmPassword.Text.Trim(), out password, out pwdSaltValue);
                                 userVo.Password=password;
                                 userVo.PasswordSaltValue=pwdSaltValue;
                            }
                            else
                            {
                                 password=Encryption.Encrypt(txtConfirmPassword.Text.Trim());
                                 userVo.Password=password;
                                 
                            }
                            userBo.ChangePassword(userVo.UserId, password,pwdSaltValue, tempPass);

                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('Your Password Changed Successfully..!');", true);
                            if (Session["ChangeTempPass"] != null && Session["ChangeTempPass"].ToString() == "Y")
                            {
                                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('Userlogin','none');", true);
                               
                                string currentURL = string.Empty;
                                if (Request.ServerVariables["HTTPS"].ToString() == "")
                                {
                                    currentURL = Request.ServerVariables["SERVER_PROTOCOL"].ToString().ToLower().Substring(0, 4).ToString() + "://" + Request.ServerVariables["SERVER_NAME"].ToString() + ":" + Request.ServerVariables["SERVER_PORT"].ToString() + Request.ServerVariables["SCRIPT_NAME"].ToString();
                                }
                                if (currentURL.Contains("localhost"))
                                {
                                    Session.Abandon();
                                    //Response.Redirect(currentURL);
                                    currentURL = currentURL.Replace("ControlHost", "Default");
                                    Page.ClientScript.RegisterStartupScript(this.GetType(),
                                              "pageloadscript", "window.parent.location.href = '" + currentURL + "'", true);
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(advisorPreferenceVo.LoginWidgetLogOutPageURL))
                                    {
                                        Session.Abandon();
                                        //Response.Redirect(advisorPreferenceVo.LoginWidgetLogOutPageURL);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(),
                                           "pageloadscript", "window.parent.location.href = '" + advisorPreferenceVo.LoginWidgetLogOutPageURL + "'", true);
                                    }
                                    else
                                    {
                                        Session.Abandon();
                                        //Response.Redirect("https://app.wealtherp.com/");
                                        Page.ClientScript.RegisterStartupScript(this.GetType(),
                                            "pageloadscript", "window.parent.location.href = 'https://app.wealtherp.com/'", true);
                                    }
                                }

                            }
                            // else
                            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('EditUserDetails','none');", true);

                        }
                        //else
                        //{
                        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('Check the password');", true);
                        //}
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('Current password is not correct..!');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('Password can not be same to User Name !');", true);
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
