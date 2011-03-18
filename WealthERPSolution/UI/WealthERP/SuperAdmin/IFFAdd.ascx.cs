using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using BoCommon;
using BoAdvisorProfiling;
using VoUser;
using VoAdvisorProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using PCGMailLib;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BoCustomerPortfolio;
using DaoUser;

namespace WealthERP.SuperAdmin
{
    public partial class IFFAdd : System.Web.UI.UserControl
    {
        UserBo userBo = new UserBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBo advisorBo = new AdvisorBo();
        Random r = new Random();
        UserVo tempUserVo = new UserVo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        UserVo user;
        int rm;
        int bm;
        int advisor;
        int advisorId;
        int result = 0;
        List<int> Ids = new List<int>();
        int index;
        string LOBId;
        AdvisorVo advisorVo;
        AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
        // List<AdvisorLOBVo> advisorLOBList = null;
        DataSet advisorLOBList = new DataSet();
        AdvisorLOBVo advisorLOBVo;
        DataSet advisorLOBListCheck = new DataSet();
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        private bool IsAddUpdate = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnSubmit.Attributes.Add("onclick", "Validation('" + ddlStatus.ClientID + "');");
            SessionBo.CheckSession();
            if (!IsPostBack)
            {
                BindCategory();
                BindStatus();

            }
            if (Session["IFFAdd"] != null)
            {
                if (Session["IFFAdd"].ToString() == "Edit")
                {
                    try
                    {
                        hidStatus.Value = "1";
                        btnAddLOB.Visible = true;
                        btnSendLoginId.Visible = true;
                        btnSubscription.Visible = true;
                        lblIFFAdd.Text = "Edit IFF";
                        btnSubmit.Text = "Update";
                        Deactivation.Visible = false;
                        lblDeactivation.Visible = false;
                        advisorVo = (AdvisorVo)Session["advisorVo"];
                        userVo = (UserVo)Session["iffUserVo"];
                        //advisorVo = advisorBo.GetAdvisor(advisorVo.advisorId);
                        txtDeactivationDate.Text = advisorVo.DeactivationDate.ToShortDateString();
                        txtAddressLine1.Text = advisorVo.AddressLine1;
                        txtAddressLine2.Text = advisorVo.AddressLine2;
                        txtAddressLine3.Text = advisorVo.AddressLine3;
                        txtCity.Text = advisorVo.City;
                        txtContactPerson.Text = advisorVo.ContactPersonFirstName;
                        txtCountry.Text = advisorVo.Country;
                        txtEmailId.Text = advisorVo.Email;
                        txtLoginId.Text = userVo.LoginId;
                        txtMobileNo.Text = advisorVo.MobileNumber.ToString();
                        txtNameofIFF.Text = advisorVo.OrganizationName;
                        txtPinCode.Text = advisorVo.PinCode.ToString();
                        txtTelephoneNumber.Text = advisorVo.Phone1Number.ToString();
                        btnSendLoginId.Text = "Reset/Send Login Id";
                        if (advisorVo.IsActive == 1)
                        {
                            ddlStatus.SelectedIndex = 0;
                            Activation.Visible = true;
                            lblActivation.Visible = true;
                            //lblActivation.Style.Add("display", "block");

                        }
                        else
                        {
                            ddlStatus.SelectedIndex = 1;
                            Activation.Visible = false;
                            lblActivation.Visible = false;
                            Deactivation.Visible = true;
                            lblDeactivation.Visible = true;

                        }

                        if (advisorVo.Category != null)
                        {

                            for (int i = 0; i < ddlCategory.Items.Count; i++)
                            {
                                if (ddlCategory.Items[i].Text == advisorVo.Category)
                                {
                                    ddlCategory.Items[i].Selected = true;
                                }
                                else
                                    ddlCategory.Items[i].Selected = false;
                            }

                        }
                        else
                        {
                            ddlCategory.SelectedIndex = 0;
                        }

                        DataSet dsadvisorLOB = advisorLOBBo.GetAdvisorLOBs(advisorVo.advisorId, null, null);

                        showLOBList(advisorVo.advisorId);
                        if (dsadvisorLOB != null)
                        {
                            for (int i = 0; i < dsadvisorLOB.Tables[0].Rows.Count; i++)
                            {
                                if (dsadvisorLOB.Tables[0].Rows[i]["AL_IsDependent"].ToString() == "1")
                                {
                                    GridValidationForIsDependent(i);
                                }
                            }
                        }
                        txtActivationDate.Text = advisorVo.ActivationDate.ToShortDateString();
                        txtActivationHidden.Text = advisorVo.ActivationDate.ToShortDateString();
                        dateCompareValidator.EnableClientScript = false;
                        dateCompareValidator.ControlToCompare = "txtActivationHidden";
                        dateCompareValidator.ControlToValidate = "txtDeactivationDate";
                        dateCompareValidator.Type = ValidationDataType.Date;
                        dateCompareValidator.Operator = ValidationCompareOperator.GreaterThan;
                        dateCompareValidator.ErrorMessage = "Deactivation Date should not be less than Activation Date";
                        if (advisorVo.IsActive == 1)
                        {
                            if (txtDeactivationDate.Text == "01/01/1900")
                            {
                                calExeDeactivationDate.SelectedDate = DateTime.Now;
                            }

                        }
                        else
                        {
                            if (txtActivationDate.Text == "01/01/1900")
                            {
                                calExeActivationDate.SelectedDate = DateTime.Now;
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else if (Session["IFFAdd"].ToString() == "Add")
                {
                    hidStatus.Value = "0";
                    Deactivation.Visible = false;
                    lblDeactivation.Visible = false;
                    txtDeactivationDate.Text = "";
                    txtAddressLine1.Text = "";
                    txtAddressLine2.Text = "";
                    txtAddressLine3.Text = "";
                    txtCity.Text = "";
                    txtContactPerson.Text = "";
                    txtCountry.Text = "";
                    txtEmailId.Text = "";
                    txtLoginId.Text = "";
                    txtMobileNo.Text = "";
                    txtNameofIFF.Text = "";
                    txtPinCode.Text = "";
                    Deactivation.Visible = false;
                    lblDeactivation.Visible = false;
                    txtTelephoneNumber.Text = "";

                    //advisorVo = (AdvisorVo)Session["LOBAdvisorVo"];
                    if (Session["IDs"] != null)
                    {
                        btnAddLOB.Visible = true;
                        btnSendLoginId.Visible = true;
                        btnSubscription.Visible = true;
                        btnSubmit.Text = "Update";
                        IsAddUpdate = true;
                        ddlStatus.Items[1].Enabled = true;
                        advisorVo = (AdvisorVo)Session["advisorVo"];
                        DataRepopulating();
                    }
                    else
                    {
                        btnAddLOB.Visible = false;
                        btnSendLoginId.Visible = false;
                        btnSubscription.Visible = false;
                        ddlStatus.Items[1].Enabled = false;
                        if (!IsPostBack)
                        {
                            calExeActivationDate.SelectedDate = DateTime.Now;
                            calExeDeactivationDate.SelectedDate = DateTime.Now;
                        }
                    }
                    if (advisorVo != null)
                    {
                        showLOBList(advisorVo.advisorId);

                    }
                    else
                    {
                        gvLOBList.DataSource = null;
                    }
                }
            }


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
                if (btnSubmit.Text == "Update")
                {
                    try
                    {
                        if (Validation())
                        {
                            advisor = 1000;
                            rm = 1001;
                            advisorVo = (AdvisorVo)Session["advisorVo"];

                            if (!IsAddUpdate)
                            {

                                userVo = (UserVo)Session["iffUserVo"];
                                advisorVo = advisorBo.GetAdvisorUser(userVo.UserId);
                                rmVo = (RMVo)Session["rmVo"];
                            }
                            else
                            {
                                Ids = (List<int>)Session["IDs"];
                                userVo.UserId = Ids[0];
                            }
                            DataPopulating();
                            advisorBo.UpdateCompleteAdviser(userVo, advisorVo, rmVo);

                            showLOBList(advisorVo.advisorId);
                            // This Delete Role Association Update Role . Dont worry about this name. If you have any doubt please refer "Stored Procedure"
                            userBo.DeleteRoleAssociation(advisorVo.UserId, advisor);
                            userBo.DeleteRoleAssociation(advisorVo.UserId, rm);
                            try
                            {
                                GridValidationForIsDependent();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFF','none');", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    if (Validation())
                    {
                        advisorVo = new AdvisorVo();
                        DataPopulating();
                        Ids = advisorBo.RegisterAdviser(userVo, advisorVo, rmVo);
                        Session["IDs"] = Ids;
                        // Session["advisorVo"] = advisorVo;
                        showLOBList(Ids[1]);

                        if (Ids != null)
                        {
                            advisorVo = advisorBo.GetAdvisor(Ids[1]);
                            Session["advisorVo"] = advisorVo;

                            CreateMainBranch();
                            advisor = 1000;
                            rm = 1001;
                            bm = 1002;
                            userBo.CreateRoleAssociation(Ids[0], advisor);
                            userBo.CreateRoleAssociation(Ids[0], rm);
                            userBo.CreateRoleAssociation(Ids[0], bm);
                            GridValidationForIsDependent();
                        }
                        try
                        {
                            CreationSuccessMessage.Visible = true;
                            Session["iffUserVo"] = userVo;
                            btnAddLOB.Visible = true;
                            btnSendLoginId.Visible = true;
                            btnSubscription.Visible = true;
                            lblMsg.Visible = true;
                            btnSubmit.Text = "Update";
                            advisorVo = (AdvisorVo)Session["advisorVo"]; DataRepopulating();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
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
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        /// <summary>
        /// Used to send mail for registered users
        /// </summary>
        /// <param name="userVo"></param>
        /// <returns></returns>
        /// 
        public void showLOBList(int advisorId)
        {
            string path = "";
            string classificationCode = "";
            try
            {
                //advisorVo = (AdvisorVo)Session["LOBAdvisorVo"];
                if (!IsPostBack)
                {

                    path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

                    advisorLOBList = advisorLOBBo.GetAdvisorLOBs(advisorId, null, null);

                    if (advisorLOBList.Tables[0].Rows.Count > 0)
                    {
                        lblMsg.Visible = false;

                        DataTable dtAdvisorLOB = new DataTable();
                        dtAdvisorLOB.Columns.Add("SI.No");
                        dtAdvisorLOB.Columns.Add("LOBId");
                        dtAdvisorLOB.Columns.Add("Broker Name");
                        dtAdvisorLOB.Columns.Add("Business Type");
                        dtAdvisorLOB.Columns.Add("Identifier");
                        dtAdvisorLOB.Columns.Add("Identifier Type");

                        DataRow drAdvisorLOB;

                        for (int i = 0; i < advisorLOBList.Tables[0].Rows.Count; i++)
                        {
                            classificationCode = advisorLOBList.Tables[0].Rows[i]["XALC_LOBClassificationCode"].ToString();
                            drAdvisorLOB = dtAdvisorLOB.NewRow();

                            drAdvisorLOB[0] = (i + 1).ToString();
                            drAdvisorLOB[1] = advisorLOBList.Tables[0].Rows[i]["AL_LOBId"].ToString();
                            drAdvisorLOB[2] = advisorLOBList.Tables[0].Rows[i]["AL_OrgName"].ToString();
                            drAdvisorLOB[3] = XMLBo.GetLOBType(path, advisorLOBList.Tables[0].Rows[i]["XALC_LOBClassificationCode"].ToString());
                            if (classificationCode == "LDSA" || classificationCode == "LFIA" || classificationCode == "LIAG" || classificationCode == "LPAG" || classificationCode == "LREA")
                            {
                                drAdvisorLOB[4] = advisorLOBList.Tables[0].Rows[i]["AL_AgentNo"].ToString();
                                drAdvisorLOB[5] = "Agent No./Agency Code";
                            }
                            else
                            {
                                drAdvisorLOB[4] = advisorLOBList.Tables[0].Rows[i]["AL_Identifier"].ToString();
                                drAdvisorLOB[5] = advisorLOBList.Tables[0].Rows[i]["XALIT_IdentifierTypeCode"].ToString();
                            }
                            dtAdvisorLOB.Rows.Add(drAdvisorLOB);
                        }
                        advisorLOBListCheck = advisorLOBList;
                        if (dtAdvisorLOB.Rows.Count > 10)
                            gvLOBList.ShowFooter = true;
                        else
                            gvLOBList.FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                        gvLOBList.ShowFooter = true;
                        gvLOBList.DataSource = dtAdvisorLOB;
                        gvLOBList.DataBind();

                    }
                    else
                    {
                        lblMsg.Visible = true;
                        //lblMsg.Text = "LOB List is Empty..";
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

                FunctionInfo.Add("Method", "ViewLOB.ascx:showLOBList()");


                object[] objects = new object[4];
                objects[0] = advisorId;
                objects[1] = path;
                objects[2] = advisorLOBVo;
                objects[3] = advisorLOBList;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        public void GridValidationForIsDependent()
        {
            foreach (GridViewRow gvr in gvLOBList.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkIsDependent")).Checked == true)
                {
                    advisorLOBBo.UpdateAdvisorLOB(int.Parse(gvLOBList.DataKeys[gvr.RowIndex].Value.ToString()), 1);
                }
                else
                {
                    advisorLOBBo.UpdateAdvisorLOB(int.Parse(gvLOBList.DataKeys[gvr.RowIndex].Value.ToString()), 0);

                }

            }
        }
        public void GridValidationForIsDependent(int i)
        {
            GridViewRow gvr = gvLOBList.Rows[i];
            if (((CheckBox)gvr.FindControl("chkIsDependent")).Checked != true)
            {
                ((CheckBox)gvr.FindControl("chkIsDependent")).Checked = true;
            }


        }
        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void gvLOBList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvLOBList.PageIndex = e.NewPageIndex;
            gvLOBList.DataBind();
        }

        protected void lnkEditLOB_OnClick(object sender, EventArgs e)
        {
            int selectedRow = 0;
            int LOBId = 0;
            Session["LOBAdvisorVo"] = advisorVo;
            LinkButton lnkbtn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)lnkbtn.NamingContainer;
            selectedRow = gvr.RowIndex;
            LOBId = int.Parse(gvLOBList.DataKeys[selectedRow].Value.ToString());
            Session["LOBId"] = LOBId;
            Session["LOBGridAction"] = "LOBEdit";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EditLOB','none');", true);

        }


        private bool SendMail(UserVo userVo)
        {
            Emailer emailer = new Emailer();
            EmailMessage email = new EmailMessage();
            userVo = (UserVo)Session["iffUserVo"];
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
                throw ex;
            }
            return isMailSent;
        }
        /// <summary>
        /// Used to validate
        /// </summary>
        /// <returns></returns>
        private bool Validation()
        {
            if (Page.IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedItem.Value == "A")
            {
                Deactivation.Visible = false;
                lblDeactivation.Visible = false;
                Activation.Visible = true;
                lblActivation.Visible = true;

            }
            else
            {
                Activation.Visible = false;
                lblActivation.Visible = false;
                Deactivation.Visible = true;
                lblDeactivation.Visible = true;
            }

        }
        /// <summary>
        /// used to populate all data from UI to VO
        /// </summary>
        ///

        public void DataRepopulating()
        {
            txtEmailId.Text = Session["IFFEmailId"].ToString();
            txtContactPerson.Text = Session["IFFContactPerson"].ToString();
            txtLoginId.Text = Session["IFFLoginId"].ToString();
            txtMobileNo.Text = Session["IFFMobileNo"].ToString();
            txtCity.Text = Session["IFFCity"].ToString();
            txtAddressLine1.Text = Session["IFFAddressLine1"].ToString();
            txtAddressLine2.Text = Session["IFFAddressLine2"].ToString();
            txtAddressLine3.Text = Session["IFFAddressLine3"].ToString();
            txtPinCode.Text = Session["IFFPinCode"].ToString();
            txtCountry.Text = Session["IFFCountry"].ToString();
            txtTelephoneNumber.Text = Session["IFFTelephoneNumber"].ToString();
            txtNameofIFF.Text = Session["IFFNameofIFF"].ToString();
            txtActivationDate.Text = Session["IFFActivationDate"].ToString();
            calExeActivationDate.SelectedDate = DateTime.Parse(Session["IFFActivationDate"].ToString());
            txtDeactivationDate.Text = Session["IFFDeactivationDate"].ToString();
            calExeDeactivationDate.SelectedDate = DateTime.Parse(Session["IFFDeactivationDate"].ToString());
            if (Session["IFFCategory"] != null)
            {

                for (int i = 0; i < ddlCategory.Items.Count; i++)
                {
                    if (ddlCategory.Items[i].Value == Session["IFFCategory"].ToString())
                    {
                        ddlCategory.Items[i].Selected = true;
                    }
                    else
                        ddlCategory.Items[i].Selected = false;
                }

            }
            if (Session["IFFIsActive"] != null)
            {

                if (Session["IFFIsActive"].ToString() == "1")
                {
                    ddlStatus.SelectedIndex = 0;
                }
                else
                {
                    ddlStatus.SelectedIndex = 1;
                }

            }
        }
        public void DataPopulating()
        {
            userVo.Email = txtEmailId.Text.ToString();
            Session["IFFEmailId"] = txtEmailId.Text.ToString();
            userVo.FirstName = txtContactPerson.Text.ToString();
            Session["IFFContactPerson"] = txtContactPerson.Text.ToString();
            userVo.Password = Encryption.Encrypt(r.Next(20000, 100000).ToString());
            userVo.UserType = "Advisor";
            userVo.LoginId = txtLoginId.Text.ToString();
            Session["IFFLoginId"] = txtLoginId.Text.ToString();
            rmVo.Email = txtEmailId.Text;
            rmVo.FirstName = txtContactPerson.Text.ToString();
            rmVo.LoginId = txtLoginId.Text.ToString();
            rmVo.Mobile = Convert.ToInt64(txtMobileNo.Text.Trim());
            Session["IFFMobileNo"] = txtMobileNo.Text.Trim();
            rmVo.UserId = userVo.UserId;
            rmVo.RMRole = "RM";
            rmVo.IsExternal = 0;

            advisorVo.City = txtCity.Text.Trim();
            Session["IFFCity"] = txtCity.Text.Trim();
            advisorVo.AddressLine1 = txtAddressLine1.Text;
            Session["IFFAddressLine1"] = txtAddressLine1.Text;
            advisorVo.AddressLine2 = txtAddressLine2.Text;
            Session["IFFAddressLine2"] = txtAddressLine2.Text;
            advisorVo.AddressLine3 = txtAddressLine3.Text;
            Session["IFFAddressLine3"] = txtAddressLine3.Text;
            advisorVo.PinCode = int.Parse(txtPinCode.Text);
            Session["IFFPinCode"] = txtPinCode.Text;
            advisorVo.Country = txtCountry.Text;
            Session["IFFCountry"] = txtCountry.Text;
            advisorVo.LoginId = txtLoginId.Text;
            if (txtTelephoneNumber.Text != "")
            {
                advisorVo.Phone1Number = int.Parse(txtTelephoneNumber.Text);
            }
            Session["IFFTelephoneNumber"] = txtTelephoneNumber.Text;
            advisorVo.ContactPersonFirstName = txtContactPerson.Text.ToString();
            advisorVo.MobileNumber = Convert.ToInt64(txtMobileNo.Text.Trim());
            advisorVo.MultiBranch = 1;
            advisorVo.OrganizationName = txtNameofIFF.Text.Trim();
            Session["IFFNameofIFF"] = txtNameofIFF.Text.Trim();
            advisorVo.Email = userVo.Email;
            //advisorVo.Email = userVo.Email;
            advisorVo.Category = ddlCategory.SelectedValue.ToString();
            Session["IFFCategory"] = ddlCategory.SelectedValue.ToString();
            advisorVo.Status = ddlStatus.SelectedValue.ToString();
            Session["IFFStatus"] = ddlStatus.SelectedValue.ToString();
            if (ddlStatus.SelectedItem.Value == "A")
            {
                advisorVo.ActivationDate = DateTime.Parse(txtActivationDate.Text);
                Session["IFFActivationDate"] = txtActivationDate.Text;
                string deactivationDate = "1900-01-01 01:01:01.001";
                advisorVo.DeactivationDate = DateTime.Parse(deactivationDate);
                Session["IFFDeactivationDate"] = deactivationDate;
                advisorVo.IsActive = 1;
                Session["IFFIsActive"] = "1";
            }
            else
            {
                advisorVo.DeactivationDate = DateTime.Parse(txtDeactivationDate.Text);
                Session["IFFDeactivationDate"] = txtDeactivationDate.Text;
                advisorVo.IsActive = 0;
                Session["IFFIsActive"] = "0";
            }

        }
        protected void ValidatingGridForIsDefault()
        {

        }
        private void CreateMainBranch()
        {
            AdvisorBranchVo branchVo = new AdvisorBranchVo();
            AdvisorBranchBo adviserBranchBo = new AdvisorBranchBo();
            List<int> adviserIds = new List<int>();
            adviserIds = (List<int>)Session["IDs"];
            branchVo.BranchHeadId = adviserIds[2];
            branchVo.BranchName = txtNameofIFF.Text;
            branchVo.City = txtCity.Text;
            branchVo.Email = txtEmailId.Text;
            branchVo.BranchTypeCode = 1;

            branchVo.MobileNumber = Convert.ToInt64(txtMobileNo.Text);

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


        protected void BindCategory()
        {
            ddlCategory.DataSource = advisorBo.GetXMLAdvisorCategory();
            ddlCategory.DataTextField = "AdviserCategory";
            ddlCategory.DataValueField = "AdviserCategoryCode";
            ddlCategory.DataBind();
            ListItem li = new ListItem();
            li.Text = "Select";
            li.Value = "Select";
            ddlCategory.Items.Insert(0, li);
        }
        protected void BindStatus()
        {
            ddlStatus.Items.Clear();
            ListItem liActive = new ListItem("Active", "A");
            ListItem liInActive = new ListItem("DeActive", "IA");
            ddlStatus.Items.Add(liActive);
            ddlStatus.Items.Add(liInActive);
        }

        protected void lbtnIFFEdit_Click(object sender, EventArgs e)
        {

        }

        protected void btnSendLoginId_Click(object sender, EventArgs e)
        {
            bool isSuccess = false;

            try
            {
                if (btnSendLoginId.Text == "Reset/Send Login Id")
                {
                    userVo = (UserVo)Session["iffUserVo"];
                    if (userVo != null)
                    {
                        userVo.Password = r.Next(20000, 100000).ToString();
                        userVo.IsTempPassword = 1;
                        userVo.Password = Encryption.Encrypt(userVo.Password);
                        isSuccess = userBo.UpdateUser(userVo);
                    }
                    if (isSuccess)
                    {
                        MailSentSuccessMessage.Visible = true;
                        bool isEmailSent = SendMail(userVo);
                        Session["RegistrationMailSent"] = isEmailSent;
                        loginIdSendMsg.Text = "Password has been reset and Sent successfully...";
                    }
                    else
                    {
                        MailSentSuccessMessage.Visible = true;
                        loginIdSendMsg.Text = "An error occurred while reseting password.";

                    }
                }
                else
                {
                    Ids = (List<int>)Session["IDs"];
                    userVo = userBo.GetUserDetails(Ids[0]);
                    Session["iffUserVo"] = userVo;
                    Session["RegistrationMailSent"] = null;
                    bool isEmailSent = SendMail(userVo);
                    Session["RegistrationMailSent"] = isEmailSent;
                    MailSentSuccessMessage.Visible = true;
                    loginIdSendMsg.Text = "Login ID Sent Successfully....";
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        protected void btnAddLOB_Click(object sender, EventArgs e)
        {
            try
            {
                advisorVo = (AdvisorVo)Session["advisorvo"];

                if (Validation())
                {
                    if (btnSubmit.Text == "Update")
                    {
                        AddLOB(advisorVo.UserId, advisorVo);
                    }
                    else
                    {
                        Ids = (List<int>)Session["IDs"];
                        AddLOB(Ids[0], advisorVo);
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
        protected void btnSubscription_Click(object sender, EventArgs e)
        {
            try
            {
                
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mainframe", "loadcontrol('Subscription','none');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void AddLOB(int userid, AdvisorVo advisorVo)
        {
            try
            {

                //Session["advisorVo"] = advisorVo;
                userVo = (UserVo)Session["iffUserVo"];

                Session["ADDLOBadvisorVo"] = advisorBo.GetAdvisorUser(userid);
                Session["rmVo"] = advisorStaffBo.GetAdvisorStaff(userid);
                //Session["IFFAddSubmit"] = "1";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddLOB','none');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}