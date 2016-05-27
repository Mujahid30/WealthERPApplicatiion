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
                FPUserVo UserVo = new FPUserVo();
                int adviserId = Convert.ToInt32(ConfigurationManager.AppSettings["ONLINE_ADVISER"]);
                if (fpUserBo.CheckInvestorExists(adviserId, txtpan2.Text, txtclientCode.Text))
                {
                    lblClient.Visible = false;
                    UserVo = fpUserBo.CreateAndGetFPUtilityUserDetails(fpUserVo, txtclientCode.Text, true);
                    isValidUser = ValidateSingleSessionPerUser(UserVo.UserId.ToString());
                    if (isValidUser && UserVo.UserId!=0)
                    {
                        Session["FPUserVo"] = UserVo;
                        Response.Redirect("Questionnaire.aspx");
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
                FPUserVo UserVo = new FPUserVo();
                UserVo = fpUserBo.CreateAndGetFPUtilityUserDetails(fpUserVo, "", false);
                isValidUser = ValidateSingleSessionPerUser(UserVo.UserId.ToString());
                if (isValidUser && UserVo.UserId != 0)
                {
                    Session["FPUserVo"] = UserVo;
                    Response.Redirect("Questionnaire.aspx");
                }
                else
                    lbllogedIn1.Visible = true;
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
