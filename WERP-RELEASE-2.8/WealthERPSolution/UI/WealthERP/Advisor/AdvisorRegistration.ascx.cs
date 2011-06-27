using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using VoAdvisorProfiling;
using VoUser;
using BoAdvisorProfiling;
using BoUser;
using WealthERP.General;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using BoCommon;
using System.Data;
using PCGMailLib;
using System.Net.Mail;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace WealthERP.Advisor
{

    public partial class AdvisorRegistration : System.Web.UI.UserControl
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

        System.Drawing.Image thumbnail_image = null;
        System.Drawing.Image original_image = null;
        System.Drawing.Bitmap final_image = null;
        System.Drawing.Graphics graphic = null;
        MemoryStream ms = null;

        int rm;
        int bm;
        int advisor;
        int advisorId;
        int result = 0;

        string path;
        string UploadImagePath;
        List<int> Ids = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

                if (!IsPostBack)
                {
                    LoadBusinessCode();
                    chkDerivative.Enabled = false;
                    chkCash.Enabled = false;
                    chkBroker.Enabled = false;
                    chkSubbroker.Enabled = false;
                    chkRemissary.Enabled = false;
                    chkIntermediary.Enabled = false;
                    lblEmailError.Visible = false;
                    LoadStates();
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
                FunctionInfo.Add("Method", "AdvisorRegistration.ascx:Page_Load()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void LoadBusinessCode()
        {
            DataTable dt = XMLBo.GetBusinessType(path);
            ddlBusinessType.DataSource = dt;
            ddlBusinessType.DataValueField = dt.Columns["BusinessTypeCode"].ToString();
            ddlBusinessType.DataTextField = dt.Columns["BusinessType"].ToString();
            ddlBusinessType.DataBind();
            ddlBusinessType.Items.Insert(0, new ListItem("Select a Business Type", "Select a Business Type"));
        }

        private void LoadStates()
        {

            DataTable dtStates = XMLBo.GetStates(path);
            ddlState.DataSource = dtStates;
            ddlState.DataValueField = "StateCode";
            ddlState.DataTextField = "StateName";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("Select a State", "Select a State"));
        }

        public bool chkAvailability()
        {
            bool result = false;
            string id = ""; ;
            try
            {
                id = txtEmail.Text;
                result = userBo.ChkAvailability(id);
            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorRegistration.ascx:chkAvailability()");
                object[] objects = new object[2];
                objects[0] = result;
                objects[1] = id;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;

        }

        public bool IsInteger(string value)
        {

            foreach (char c in value.ToCharArray())
            {

                if (!Char.IsDigit(c))
                {

                    return false;

                }

            }

            return true;

        }

        public bool Validation()
        {
            bool result = false;

            try
            {
                if (!ChkMailId(txtEmail.Text.ToString()))
                {
                    result = false;
                    lblEmailError.Visible = true;
                                  }


                else
                {
                    if (!chkAvailability())
                    {
                        result = false;
                        lblEmailError.Visible = true;
                    }


                    else
                    {
                        result = true;
                        lblEmailError.Visible = false;
                        lblEmail.CssClass = "FieldName";
                    }
                }

                return result;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorRegistration.ascx:PageLoad()");
                object[] objects = new object[1];
                objects[0] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }



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
                FunctionInfo.Add("Method", "AdvisorRegistration.ascx:PageLoad()");
                object[] objects = new object[1];
                objects[1] = email;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        protected void chkMFEQ_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMf.Checked == false && chkEquity.Checked == false)
            {
                chkDerivative.Enabled = false;
                chkCash.Enabled = false;
                chkBroker.Enabled = false;
                chkSubbroker.Enabled = false;
                chkRemissary.Enabled = false;
                chkIntermediary.Enabled = false;
            }
            else if (chkMf.Checked == false && chkEquity.Checked == true)
            {
                chkDerivative.Enabled = true;
                chkCash.Enabled = true;
                chkBroker.Enabled = true;
                chkSubbroker.Enabled = true;
                chkRemissary.Enabled = true;
                chkIntermediary.Enabled = false;
            }
            else if (chkMf.Checked == true && chkEquity.Checked == false)
            {
                chkDerivative.Enabled = false;
                chkCash.Enabled = false;
                chkBroker.Enabled = false;
                chkSubbroker.Enabled = false;
                chkRemissary.Enabled = false;
                chkIntermediary.Enabled = true;
            }
            else if (chkMf.Checked == true && chkEquity.Checked == true)
            {
                chkDerivative.Enabled = true;
                chkCash.Enabled = true;
                chkBroker.Enabled = true;
                chkSubbroker.Enabled = true;
                chkRemissary.Enabled = true;
                chkIntermediary.Enabled = true;
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string bType = "";
            string path = "";
            try
            {
                if (Validation())
                {
                    user = (UserVo)Session["UserVo"];
                    bType = ddlBusinessType.SelectedValue.ToString();
                    path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString(); string multiBranch = " ";
                    if (rbtnNo.Checked)
                        multiBranch = "no";
                    if (rbtnYes.Checked)
                        multiBranch = "yes";

                    
                    userVo.Email = txtEmail.Text.ToString();
                    if (TextBox1.Text == "")
                    {
                        advisorVo.Website = null;
                        
                    }
                    else
                    {
                        
                        advisorVo.Website = TextBox1.Text.ToString();
                    }
                    userVo.FirstName = txtFirstName.Text.ToString();
                    userVo.MiddleName = txtMiddleName.Text.ToString();
                    userVo.LastName = txtLastName.Text.ToString();
                    userVo.Password = Encryption.Encrypt(r.Next(20000, 100000).ToString());
                    userVo.UserType = "Advisor";
                    userVo.Email = txtEmail.Text.ToString();
                    userVo.LoginId = txtEmail.Text.ToString();


                    advisorVo.AddressLine1 = txtAddressLine1.Text.ToString();
                    advisorVo.AddressLine2 = txtAddressLine2.Text.ToString();
                    advisorVo.AddressLine3 = txtAddressLine3.Text.ToString();

                    if (ddlBusinessType.SelectedIndex != 0)
                        advisorVo.BusinessCode = ddlBusinessType.SelectedValue.ToString();
                    else
                        advisorVo.BusinessCode = null;
                    advisorVo.City = txtCity.Text.Trim();
                    advisorVo.ContactPersonFirstName = txtFirstName.Text.ToString();
                    advisorVo.ContactPersonLastName = txtLastName.Text.ToString();
                    advisorVo.ContactPersonMiddleName = txtMiddleName.Text.ToString();
                    advisorVo.Country = ddlCountry.SelectedItem.Value.ToString();

                    if (txtFaxNumber.Text == "")
                    {
                        advisorVo.Fax = 0;
                        rmVo.Fax = 0;
                    }
                    else
                    {
                        advisorVo.Fax = int.Parse(txtFaxNumber.Text.ToString());
                        rmVo.Fax = int.Parse(txtFaxNumber.Text);
                    }
                    if (txtFaxISD.Text == "")
                    {
                        advisorVo.FaxIsd = 0;
                        rmVo.FaxIsd = 0;
                    }
                    else
                    {
                        advisorVo.FaxIsd = int.Parse(txtFaxISD.Text.ToString());
                        rmVo.FaxIsd = int.Parse(txtFaxISD.Text);
                    }
                    if (txtFaxSTD.Text == "")
                    {
                        advisorVo.FaxStd = 0;
                        rmVo.FaxStd = 0;
                    }
                    else
                    {
                        advisorVo.FaxStd = int.Parse(txtFaxSTD.Text.ToString());
                        rmVo.FaxStd = int.Parse(txtFaxSTD.Text);
                    }
                    if (txtMobileNumber.Text == "")
                    {
                        advisorVo.MobileNumber = 0;
                        rmVo.Mobile = 0;
                    }
                    else
                    {
                        advisorVo.MobileNumber = Convert.ToInt64(txtMobileNumber.Text.ToString());
                        rmVo.Mobile = Convert.ToInt64(txtMobileNumber.Text.ToString());
                    }
                    if (txtISD1.Text == "")
                    {
                        advisorVo.Phone1Isd = 0;
                        rmVo.OfficePhoneDirectIsd = 0;
                    }
                    else
                    {
                        advisorVo.Phone1Isd = int.Parse(txtISD1.Text.ToString());
                        rmVo.OfficePhoneDirectIsd = int.Parse(txtISD1.Text);
                    }

                    if (txtISD2.Text == "")
                    {
                        advisorVo.Phone2Isd = 0;
                        rmVo.OfficePhoneExtIsd = 0;
                    }
                    else
                    {
                        advisorVo.Phone2Isd = int.Parse(txtISD2.Text.ToString());
                        rmVo.OfficePhoneExtIsd = int.Parse(txtISD2.Text);
                    }

                    if (txtPhoneNumber1.Text == "")
                    {
                        advisorVo.Phone1Number = 0;
                        rmVo.OfficePhoneDirectNumber = 0;
                    }
                    else
                    {
                        advisorVo.Phone1Number = int.Parse(txtPhoneNumber1.Text.ToString());
                        rmVo.OfficePhoneDirectNumber = int.Parse(txtPhoneNumber1.Text);
                    }
                    if (txtPhoneNumber2.Text == "")
                    {
                        advisorVo.Phone2Number = 0;
                        rmVo.OfficePhoneExtNumber = 0;
                    }
                    else
                    {
                        advisorVo.Phone2Number = int.Parse(txtPhoneNumber2.Text.ToString());

                        rmVo.OfficePhoneExtNumber = int.Parse(txtPhoneNumber2.Text);
                    }
                    if (txtSTD1.Text == "")
                    {
                        advisorVo.Phone1Std = 0;
                        rmVo.OfficePhoneDirectStd = 0;
                    }
                    else
                    {
                        advisorVo.Phone1Std = int.Parse(txtSTD1.Text);
                        rmVo.OfficePhoneDirectStd = int.Parse(txtSTD1.Text);
                    }
                    if (txtSTD2.Text == "")
                    {
                        advisorVo.Phone2Std = 0;
                        rmVo.OfficePhoneExtStd = 0;
                    }
                    else
                    {
                        advisorVo.Phone2Std = int.Parse(txtSTD2.Text);
                        rmVo.OfficePhoneExtStd = int.Parse(txtSTD2.Text);
                    }

                    if (multiBranch.ToString().Trim() == "yes")
                    {
                        advisorVo.MultiBranch = 1;
                    }
                    else
                    {
                        advisorVo.MultiBranch = 0;
                    }
                    if (RadioButton2.Checked)
                    {
                        advisorVo.Associates = 0;
                    }
                    else
                    {
                        advisorVo.Associates = 1;
                    }
                    advisorVo.OrganizationName = txtOrganizationName.Text.ToString().Trim();
                    path = Server.MapPath("Images") + "\\";
                    if (AdvlogoPath.HasFile)
                    {
                        string[] fileName = AdvlogoPath.FileName.Split('.');
                        advisorVo.LogoPath = advisorVo.OrganizationName + "_" + fileName[0] + ".jpg";
                        //advisorBranchVo.LogoPath = advisorVo.advisorId + "_" + txtBranchCode.Text.ToString() + ".jpg";
                        HttpPostedFile myFile = AdvlogoPath.PostedFile;
                        UploadImage(path, myFile, advisorVo.LogoPath);
                        //FileUpload.SaveAs(Server.MapPath("Images") + "\\" + advisorVo.advisorId + "_" + txtBranchCode.Text.ToString() + ".jpg");
                    }

                    if (txtPinCode.Text == "")
                        advisorVo.PinCode = 0;
                    else
                        advisorVo.PinCode = int.Parse(txtPinCode.Text.ToString());
                    if (ddlState.SelectedIndex != 0)
                    {
                        advisorVo.State = ddlState.SelectedItem.Value.ToString();
                    }
                    else
                    {
                        advisorVo.State = null;
                    }
                    advisorVo.Email = userVo.Email;

                    rmVo.Email = txtEmail.Text;
                    rmVo.FirstName = txtFirstName.Text.ToString();
                    rmVo.LastName = txtLastName.Text.ToString();
                    rmVo.LoginId = txtEmail.Text.ToString();
                    rmVo.MiddleName = txtMiddleName.Text;
                    rmVo.UserId = userVo.UserId;
                    rmVo.RMRole = "RM";
                    rmVo.IsExternal = 0;



                    Ids = advisorBo.CreateCompleteAdviser(userVo, advisorVo, rmVo);
                    Session["IDs"] = Ids;

                    if (Ids!=null)
                    {
                        CreateMainBranch();
                    }

                    //string EmailPath = Server.MapPath(ConfigurationManager.AppSettings["EmailPath"].ToString());

                    //PcgMailMessage email = new PcgMailMessage();
                    //email.SendRegistrationMail("admin@wealtherp.net", userVo.Email, userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName, userVo.LoginId, userVo.Password, EmailPath);
                    Session["RegistrationMailSent"] = null;
                    bool isEmailSent = SendMail(userVo);
                    Session["RegistrationMailSent"] = isEmailSent;

                    if (Ids != null)
                    {
                        advisorVo = advisorBo.GetAdvisor(Ids[1]);
                        Session["advisorVo"] = advisorVo;
                        advisor = 1000;
                        rm = 1001;

                        userBo.CreateRoleAssociation(Ids[0], advisor);
                        // userBo.CreateRoleAssociation(userVo.UserId, rm);
                        if (chkRoleBM.Checked)
                        {
                            bm = 1002;
                            userBo.CreateRoleAssociation(Ids[0], bm);
                            rm = 1001;
                            userBo.CreateRoleAssociation(Ids[0], rm);
                        }
                        if (!chkRoleBM.Checked)
                        {
                            rm = 1001;
                            userBo.CreateRoleAssociation(Ids[0], rm);
                        }
                    }



                    Session["advisorId"] = advisorVo.advisorId.ToString();
                    Session["orgName"] = advisorVo.OrganizationName.ToString();
                    Session["UserVo"] = userVo;
                    Session["multiBranch"] = multiBranch.ToString();
                    Session["mf1"] = null;
                    Session["equityBrokerCash1"] = null;
                    Session["equityBrokerDerivative1"] = null;
                    Session["equitySubBrokerCash1"] = null;
                    Session["equitySubBrokerDerivative1"] = null;
                    Session["equityRemissaryCash1"] = null;
                    Session["equityRemissaryDerivative1"] = null;
                    Session["LOBId"] = "FromReg";

                    if (!chkMf.Checked && !chkEquity.Checked)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('UserLoginMessage','none');", true);
                    }
                    else
                    {
                        if (chkMf.Checked == true)
                        {
                            Session["mf1"] = "mf";
                            if (chkIntermediary.Checked == true)
                            {
                                Session["mf1"] = "mf";
                            }
                        }
                        if (chkEquity.Checked == true)
                        {
                            if (chkBroker.Checked == true)
                            {
                                if (chkCash.Checked == true)
                                {
                                    Session["equityBrokerCash1"] = "equityBrokerCash";
                                }
                                if (chkDerivative.Checked == true)
                                {
                                    Session["equityBrokerDerivative1"] = "equityBrokerDerivative";
                                }
                            }
                            if (chkSubbroker.Checked == true)
                            {
                                if (chkCash.Checked == true)
                                {
                                    Session["equitySubBrokerCash1"] = "equitySubBrokerCash";
                                }
                                if (chkDerivative.Checked == true)
                                {
                                    Session["equitySubBrokerDerivative1"] = "equitySubBrokerDerivaitve";
                                }
                            }
                            if (chkRemissary.Checked == true)
                            {
                                if (chkCash.Checked == true)
                                {
                                    Session["equityRemissaryCash1"] = "equityRemissaryCash";
                                }
                                if (chkDerivative.Checked == true)
                                {
                                    Session["equityRemissaryDerivative1"] = "equityRemissaryDerivative";
                                }
                            }


                        }
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LOB','none');", true);
                    }
                }
                //else
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Sorry.. Error in Account creation..!');", true);
                //}
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
                objects[3] = result;
                objects[4] = advisorId;
                objects[5] = path;
                objects[6] = bType;
                objects[7] = advisor;
                objects[8] = rm;
                objects[9] = user;
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
                notificationMail.GetAdviserRegistrationMailNotification(advisorVo.OrganizationName, advisorVo.City, advisorVo.MobileNumber, userVo.LoginId, userVo.Email, name);
                //notificationMail.GetAdviserRegistrationMail(userVo.LoginId, userVo.Password, name);

                emailer.SendMail(notificationMail);
            }
            catch (Exception ex)
            {

            }
            return isMailSent;
        }

        private void CreateMainBranch()
        {
            AdvisorBranchVo branchVo = new AdvisorBranchVo();
            AdvisorBranchBo adviserBranchBo = new AdvisorBranchBo();
            List<int> adviserIds = new List<int>();
            adviserIds = (List<int>)Session["IDs"];
            branchVo.AddressLine1 = txtAddressLine1.Text;
            branchVo.BranchHeadId = adviserIds[2];
            branchVo.BranchName = txtOrganizationName.Text;
            branchVo.City = txtCity.Text;
            branchVo.Country = ddlCountry.SelectedItem.Value.ToString();
            branchVo.Email = txtEmail.Text;
            branchVo.BranchTypeCode = 1;
            if (txtPinCode.Text == "")
                branchVo.PinCode = 0;
            else
                branchVo.PinCode = int.Parse(txtPinCode.Text.ToString());
            if (ddlState.SelectedIndex != 0)
                branchVo.State = ddlState.SelectedItem.Value.ToString();
            else
                branchVo.State = null;
            if (txtFaxNumber.Text == "")
                branchVo.Fax = 0;
            else
                branchVo.Fax = int.Parse(txtFaxNumber.Text.ToString());
            if (txtFaxISD.Text == "")
                branchVo.FaxIsd = 0;
            else
                branchVo.FaxIsd = int.Parse(txtFaxISD.Text.ToString());
            if (txtFaxSTD.Text == "")
                branchVo.FaxStd = 0;
            else
                branchVo.FaxStd = int.Parse(txtFaxSTD.Text.ToString());
            if (txtMobileNumber.Text == "")
                branchVo.MobileNumber = 0;
            else
                branchVo.MobileNumber = Convert.ToInt64(txtMobileNumber.Text.ToString());
            if (txtISD1.Text == "")
                branchVo.Phone1Isd = 0;
            else
                branchVo.Phone1Isd = int.Parse(txtISD1.Text.ToString());
            if (txtISD2.Text == "")
                branchVo.Phone2Isd = 0;
            else
                branchVo.Phone2Isd = int.Parse(txtISD2.Text.ToString());
            if (txtPhoneNumber1.Text == "")
                branchVo.Phone1Number = 0;
            else
                branchVo.Phone1Number = int.Parse(txtPhoneNumber1.Text.ToString());
            if (txtPhoneNumber2.Text == "")
                branchVo.Phone2Number = 0;
            else
                branchVo.Phone2Number = int.Parse(txtPhoneNumber2.Text.ToString());
            if (txtSTD1.Text == "")
                branchVo.Phone1Std = 0;
            else
                branchVo.Phone1Std = int.Parse(txtSTD1.Text);
            if (txtSTD2.Text == "")
                branchVo.Phone2Std = 0;
            else
                branchVo.Phone2Std = int.Parse(txtSTD2.Text);
            branchVo.IsHeadBranch = 1;
            branchVo.BranchId = adviserBranchBo.CreateAdvisorBranch(branchVo, adviserIds[1], adviserIds[0]);
            adviserBranchBo.AssociateBranch(adviserIds[2], branchVo.BranchId, 1, adviserIds[0]);


        }

        protected void ResizeFromStream(string path, Stream Buffer)
        {
            int intNewWidth = 0;
            int intNewHeight = 0;
            System.Drawing.Image imgInput = null;
            ImageFormat fmtImageFormat = null;
            Bitmap bmpResized = null;
            try
            {
                imgInput = System.Drawing.Image.FromStream(Buffer);
                fmtImageFormat = imgInput.RawFormat;
                intNewWidth = 70;
                intNewHeight = 78;
                bmpResized = new Bitmap(imgInput, intNewWidth, intNewHeight);
                // bmpResized = new Bitmap(imgInput);
                bmpResized.Save(path, fmtImageFormat);
                logo.Src = path;
                imgInput.Dispose();
                bmpResized.Dispose();
                Buffer.Close();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorRegistration.ascx:ResizeFromStream()");
                object[] objects = new object[5];
                objects[0] = imgInput;
                objects[1] = fmtImageFormat;
                objects[2] = bmpResized;
                objects[3] = intNewHeight;
                objects[4] = intNewWidth;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }





        }

        private void UploadImage(string ImagePath, HttpPostedFile postedFile, string fileName) // Here ImagePath is actually the Image Name
        {
            try
            {
                UploadImagePath = ImagePath;

                // Get the data
                HttpPostedFile jpeg_image_upload = postedFile;

                // Retrieve the uploaded image
                original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
                original_image.Save(UploadImagePath + "O_" + System.IO.Path.GetFileName(jpeg_image_upload.FileName));

                // Calculate the new width and height
                int width = original_image.Width;
                int height = original_image.Height;
                int new_width, new_height;
                //string thumbnail_id;

                int target_width = 70;
                int target_height = 78;
                CreateThumbnail(original_image, ref final_image, ref graphic, ref ms, jpeg_image_upload, width, height, target_width, target_height, "", true, false, out new_width, out new_height, fileName); // , out thumbnail_id

                File.Delete(UploadImagePath + "O_" + System.IO.Path.GetFileName(jpeg_image_upload.FileName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Clean up
                if (final_image != null) final_image.Dispose();
                if (graphic != null) graphic.Dispose();
                if (original_image != null) original_image.Dispose();
                if (thumbnail_image != null) thumbnail_image.Dispose();
                if (ms != null) ms.Close();
            }
        }

        private void CreateThumbnail(System.Drawing.Image original_image, ref System.Drawing.Bitmap final_image, ref System.Drawing.Graphics graphic, ref MemoryStream ms, HttpPostedFile jpeg_image_upload, int width, int height, int target_width, int target_height, string prefix, bool thumb, bool watermark, out int new_width, out int new_height, string fileName) // , out string thumbnail_id
        {
            int indexOfExtension = System.IO.Path.GetFileName(jpeg_image_upload.FileName).LastIndexOf(".");
            string imageFileName = System.IO.Path.GetFileName(jpeg_image_upload.FileName).Substring(0, indexOfExtension);

            float image_ratio = (float)width / (float)height;

            if (width > height)
            {
                if (image_ratio > 1.5)
                {   // If Image width is lot greater than height, then do following
                    if (width > 100 && width < 400)
                    {
                        target_width = width;
                    }
                    else if (width > 400)
                    {
                        target_width = 400;
                    }
                }
            }

            float target_ratio = (float)target_width / (float)target_height;

            if (target_ratio > image_ratio)
            {
                new_height = target_height;
                new_width = (int)Math.Floor(image_ratio * (float)target_height);
            }
            else
            {
                new_height = (int)Math.Floor((float)target_width / image_ratio);
                new_width = target_width;
            }

            new_width = new_width > target_width ? target_width : new_width;
            new_height = new_height > target_height ? target_height : new_height;

            //final_image = new System.Drawing.Bitmap(target_width, target_height);
            final_image = new System.Drawing.Bitmap(new_width, new_height);
            graphic = System.Drawing.Graphics.FromImage(final_image);
            graphic.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), new System.Drawing.Rectangle(0, 0, new_width, new_height));
            //int paste_x = (target_width - new_width) / 2;
            //int paste_y = (target_height - new_height) / 2;
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; /* new way */
            //graphic.DrawImage(original_image, paste_x, paste_y, new_width, new_height);
            graphic.DrawImage(original_image, 0, 0, new_width, new_height);

            ms = new MemoryStream();
            final_image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            saveJpeg(UploadImagePath + fileName, final_image, 100); //jpeg_image_upload.FileName
            ms.Dispose();
        }

        private void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam =
               new EncoderParameter(Encoder.Quality, (long)quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec =
               this.getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }


    }
}
