using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VOFPUtilityUser;
using BoFPUtility;
using System.Collections;
using System.Configuration;
using System.Globalization;

namespace FPUtilityTool
{
    public partial class _Default : System.Web.UI.Page
    {
        FPUserBO fpUserBo = new FPUserBO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["FPUserVo"] != null)
                    Response.Redirect("Questionnaire.aspx");
            }
            if (this.IsPostBack)
            {
                TabName.Value = Request.Form[TabName.UniqueID];
            }
        }
        protected void btnsignInsubmit_Click(object sender, EventArgs e)
        {
            if (Session["FPUserVo"] != null)
            {
                Response.Redirect("Questionnaire.aspx");
            }
            else
            {
                bool isValidUser = false;
                FPUserVo fpUserVo = new FPUserVo();
                fpUserVo.MobileNo = 0;
                fpUserVo.UserName = "";
                fpUserVo.Pan = txtpan2.Text;
                fpUserVo.EMail = "";
                fpUserVo.DOB = DateTime.MinValue;
                FPUserVo UserVo = new FPUserVo();
                int adviserId = Convert.ToInt32(ConfigurationManager.AppSettings["ONLINE_ADVISER"]);
                if (fpUserBo.CheckInvestorExists(adviserId, txtpan2.Text, txtclientCode.Text))
                {
                    lblClient.Visible = false;
                    UserVo = fpUserBo.CreateAndGetFPUtilityUserDetails(fpUserVo, txtclientCode.Text, true);
                    isValidUser = ValidateSingleSessionPerUser(UserVo.UserId.ToString());
                    if (isValidUser && UserVo.UserId != 0 && string.IsNullOrEmpty(UserVo.RiskClassCode))
                    {
                        Session["FPUserVo"] = UserVo;
                        Response.Redirect("Questionnaire.aspx");
                    }
                    else if (isValidUser && UserVo.UserId != 0 && !string.IsNullOrEmpty(UserVo.RiskClassCode))
                    {
                        Session["FPUserVo"] = UserVo;
                        Response.Redirect("Result.aspx");
                    }
                    else
                        lbllogedIn2.Visible = true;
                }
                else
                {
                    lblClient.Visible = true;
                }
            }


        }
       
        protected void btnsignUpsubmit_Click(object sender, EventArgs e)
        {
            if (Session["FPUserVo"] != null)
            {
                Response.Redirect("Questionnaire.aspx");
            }
            else
            {
                bool isValidUser = false;
                FPUserVo fpUserVo = new FPUserVo();
                fpUserVo.MobileNo = Convert.ToInt64(txtMobNo.Text);
                fpUserVo.UserName = txtName.Text;
                fpUserVo.Pan = txtPan1.Text;
                fpUserVo.EMail = txtEmail.Text;
                fpUserVo.DOB = DateTime.ParseExact(txtDob.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                FPUserVo UserVo = new FPUserVo();
                UserVo = fpUserBo.CreateAndGetFPUtilityUserDetails(fpUserVo, "", false);
                isValidUser = ValidateSingleSessionPerUser(UserVo.UserId.ToString());
                int adviserId = Convert.ToInt32(ConfigurationManager.AppSettings["ONLINE_ADVISER"]);
                if (!fpUserBo.CheckInvestorExists(adviserId, txtPan1.Text, txtclientCode.Text))
                {
                    Label1.Visible = false;
                    if (isValidUser && UserVo.UserId != 0 && string.IsNullOrEmpty(UserVo.RiskClassCode))
                    {
                        Session["FPUserVo"] = UserVo;
                        Response.Redirect("Questionnaire.aspx");
                    }
                    else if (isValidUser && UserVo.UserId != 0 && !string.IsNullOrEmpty(UserVo.RiskClassCode))
                    {
                        Session["FPUserVo"] = UserVo;
                        Response.Redirect("Result.aspx");
                    }
                    else
                        lbllogedIn1.Visible = true;
                }
                else
                {
                    Label1.Visible = true;
                }
            }

        }
        private bool ValidateSingleSessionPerUser(string userId)
        {
            bool isUserLogedIn = false;
            Hashtable currentLoginUserList = new Hashtable();

            if (Application["LoginUserList"] != null)
            {
                currentLoginUserList = (Hashtable)Application["LoginUserList"];
            }

            isUserLogedIn = currentLoginUserList.ContainsValue(userId);
            if (!isUserLogedIn)
            {
                currentLoginUserList.Add(Session.SessionID.ToString(), userId);
                Application.Lock();
                Application["LoginUserList"] = currentLoginUserList;
                Application.UnLock();
                isUserLogedIn = true;
            }
            else
            {
                isUserLogedIn = false;
            }

            return isUserLogedIn;

        }
    }
}
