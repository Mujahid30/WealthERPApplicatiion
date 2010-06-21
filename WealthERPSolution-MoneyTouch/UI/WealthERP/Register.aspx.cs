using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using BoAdvisorProfiling;
using VoUser;
using VoAdvisorProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using PCGMailLib;
using System.Web.Services;
using DaoUser;

namespace WealthERP
{
    public partial class Register : System.Web.UI.Page
    {
        UserBo userBo = new UserBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBo advisorBo = new AdvisorBo();

        Random r = new Random(); 

        AdvisorVo advisorVo = new AdvisorVo();
        UserVo tempUserVo = new UserVo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        UserVo user;

        int rm;
        //int bm;
        int advisor;
        int advisorId;
        int result = 0;

        List<int> Ids = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static bool CheckLoginIdAvailability(string loginId)
        {
            UserDao userDao = new UserDao();

            return userDao.ChkAvailability(loginId);
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
             
            try
            {
                if (Validation())
                {
                    user = (UserVo)Session["UserVo"];

                    userVo.Email = txtEmail.Text.ToString();
                    userVo.FirstName = txtContactPerson.Text.ToString();
                    userVo.Password = Encryption.Encrypt(r.Next(20000, 100000).ToString());
                    userVo.UserType = "Advisor";
                    userVo.Email = txtEmail.Text.ToString();
                    userVo.LoginId = txtLogin.Text.ToString();

                    advisorVo.City = txtCity.Text.Trim();
                    advisorVo.ContactPersonFirstName = txtContactPerson.Text.ToString();

                    advisorVo.MobileNumber = Convert.ToInt64(txtMobile.Text.Trim());
                    
                    advisorVo.MultiBranch = 1;

                    advisorVo.OrganizationName = txtOrganization.Text.Trim();

                    advisorVo.Email = userVo.Email;

                    rmVo.Email = txtEmail.Text;
                    rmVo.FirstName = txtContactPerson.Text.ToString();
                    rmVo.LoginId = txtLogin.Text.ToString();
                    rmVo.UserId = userVo.UserId;
                    rmVo.RMRole = "RM";
                    rmVo.IsExternal = 0;


                    Ids = advisorBo.RegisterAdviser(userVo, advisorVo, rmVo);
                    Session["IDs"] = Ids;


                    if (Ids != null)
                    {
                        //advisorVo = advisorBo.GetAdvisor(Ids[1]);
                        //Session["advisorVo"] = advisorVo;

                        CreateMainBranch();
                        advisor = 1000;
                        rm = 1001;

                        userBo.CreateRoleAssociation(Ids[0], advisor);
                        userBo.CreateRoleAssociation(Ids[0], rm);
                    }
                    Session["RegistrationMailSent"] = null;
                    bool isEmailSent = SendMail(userVo);
                    Session["RegistrationMailSent"] = isEmailSent;
                    try
                    {
                        Response.Redirect("~/RegistrationConfirmation.aspx", true);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
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
                FunctionInfo.Add("Method", "AdvisorRegistration.ascx:btnNext_Click()");
                object[] objects = new object[10];
                objects[0] = advisorVo;
                objects[1] = userVo;
                objects[2] = rmVo;
                //objects[3] = result;
                //objects[4] = advisorId;
                //objects[5] = path;
                //objects[6] = bType;
                //objects[7] = advisor;
                //objects[8] = rm;
                //objects[9] = user;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private bool SendMail(UserVo userVo)
        {
            Emailer emailer = new Emailer();
            EmailMessage email = new EmailMessage();
            bool isMailSent = false;
            try
            {
                email.To.Add(userVo.Email);
                string name = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;
                email.GetAdviserRegistrationMail(userVo.LoginId, Encryption.Decrypt(userVo.Password), name);
                isMailSent = emailer.SendMail(email);

                //Send a notification mail to Wealth ERP team.
                EmailMessage notificationMail = new EmailMessage();
                notificationMail.GetAdviserRegistrationMailNotification(advisorVo.OrganizationName,advisorVo.City,advisorVo.MobileNumber,userVo.LoginId, userVo.Email, name);
                //notificationMail.GetAdviserRegistrationMail(userVo.LoginId, userVo.Password, name);

                emailer.SendMail(notificationMail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isMailSent;
        }

        private bool Validation()
        {
            return true;
            //throw new NotImplementedException();
        }
        private void CreateMainBranch()
        {
            AdvisorBranchVo branchVo = new AdvisorBranchVo();
            AdvisorBranchBo adviserBranchBo = new AdvisorBranchBo();
            List<int> adviserIds = new List<int>();
            adviserIds = (List<int>)Session["IDs"];
            branchVo.BranchHeadId = adviserIds[2];
            branchVo.BranchName = txtOrganization.Text;
            branchVo.City = txtCity.Text;
            branchVo.Email = txtEmail.Text;
            branchVo.BranchTypeCode = 1;
            
            branchVo.MobileNumber = Convert.ToInt64(txtMobile.Text);
            
            branchVo.AddressLine1 = string.Empty;
            branchVo.AddressLine2 = string.Empty;
            branchVo.AddressLine3 = string.Empty;
            branchVo.Country = string.Empty;
            branchVo.BranchTypeCode = 1;
            branchVo.PinCode = 0;
            branchVo.State = null;
            branchVo.Fax = 0;
            branchVo.FaxIsd = 0;
            branchVo.FaxStd = 0;
            branchVo.Phone1Isd = 0;
            branchVo.Phone2Isd = 0;
            branchVo.Phone1Number = 0;
            branchVo.Phone2Number = 0;
            branchVo.Phone1Std = 0;
            branchVo.Phone2Std = 0;

            branchVo.IsHeadBranch = 1;
            branchVo.BranchId = adviserBranchBo.CreateAdvisorBranch(branchVo, adviserIds[1], adviserIds[0]);
            adviserBranchBo.AssociateBranch(adviserIds[2], branchVo.BranchId, 1, adviserIds[0]);

        }
    }
}
