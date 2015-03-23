using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoUser;
using BoUser;
using System.Web.UI;
using PCGMailLib;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Configuration;
using BoCommon;

namespace WealthERP.General
{
    public partial class ForgotPassword : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        UserBo userBo = new UserBo();
        Random r = new Random();
        PcgMailMessage email = new PcgMailMessage();
                    

        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionBo.CheckSession();
            lblError.Visible = false;
            lblMailSent.Visible = false;

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           // userVo = userBo.GetUser(txtLoginId.Text);
            userVo = userBo.GetUserReset(txtLoginId.Text,txtEmail.Text,TxtPan.Text);
            Random r = new Random();
            OneWayEncryption encryption;
            encryption = new OneWayEncryption();
            bool isSuccess = false;

            if (userVo != null)  // && userVo.Email == txtEmail.Text )
            {
                  
               string hassedPassword = string.Empty;
                string saltValue = string.Empty;
                string password = r.Next(20000, 100000).ToString();

                //userVo = userBo.GetUserDetails(userId);
              //  string userName = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;
                encryption.GetHashAndSaltString(password, out hassedPassword, out saltValue);
                userVo.Password = hassedPassword;
                userVo.PasswordSaltValue = saltValue;
                userVo.OriginalPassword = password;
                userVo.IsTempPassword = 1;
             //  isSuccess = userBo.UpdateUser(userVo);
               isSuccess = userBo.UpdateUserReset(userVo);
            }

            if (isSuccess)
            {
                tblMessage.Visible = true;
                SuccessMsg.Visible = true;
                ErrorMessage.Visible = false;
                SuccessMsg.InnerText = "Password has been reset successfully...";
                txtLoginId.Text = "";
                txtEmail.Text = "";
                TxtPan.Text = "";
              }
            else
            {
                tblMessage.Visible = true;
                SuccessMsg.Visible = false;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "An error occurred while reseting password.";

            }
            //{
            //    userVo.Password = r.Next(20000, 100000).ToString();
            //    userVo.IsTempPassword = 1;
            //    userVo.Password = Encryption.Encrypt(userVo.Password);
            //    userBo.UpdateUser(userVo);


            //    //Send email
            //    //
            //    //string EmailPath = Server.MapPath(ConfigurationManager.AppSettings["EmailPath"].ToString());
            //    //email.SendForgotPasswordMail("admin@wealtherp.net", userVo.Email, userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName, userVo.LoginId, userVo.Password, EmailPath);
            //    bool isMailSent = SendMail(userVo);
            //    //
            //    if (!isMailSent)
            //        lblMailSent.Text = "An error occurred while sending mail to your Email ID";

            //    lblMailSent.Visible = true;
            //    lblError.Visible = false;
            //    trEmail.Visible = false;
            //    trLogin.Visible = false;
            //    btnSubmit.Visible = false;
            //    lnkSignIn.Visible = true;
            //}
            //else if (userVo == null || userVo.Email != txtEmail.Text)
            //{
            //    lblError.Text = "Login ID and Email ID do not match";
            //    lblError.Visible = true;
            //}
            //else
            //{
            //    lblError.Visible = true;
            //    lblMailSent.Visible = false;
            //}


        }

        private bool SendMail(UserVo userVo)
        {
            Emailer emailer = new Emailer();
            EmailMessage email = new EmailMessage();
            email.To.Add(userVo.Email);
            string name = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;
            string password = Encryption.Decrypt(userVo.Password);
            email.GetForgotPasswordMail(userVo.LoginId, password, name);
            bool isMailSent = emailer.SendMail(email);
            return isMailSent;
        }

        protected void lnkSignIn_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('Userlogin','none');", true);
        }
    }
}