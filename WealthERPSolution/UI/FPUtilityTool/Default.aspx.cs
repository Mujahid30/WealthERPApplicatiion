using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VOFPUtilityUser;
using BoFPUtility;
using System.Collections;

namespace FPUtilityTool
{
    public partial class _Default : System.Web.UI.Page
    {
        FPUserBO fpUserBo = new FPUserBO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserVo"] != null)
                    Response.Redirect("Questionnaire.aspx");
            }
            if (this.IsPostBack)
            {
                TabName.Value = Request.Form[TabName.UniqueID];
            }
        }
        protected void btnsignInsubmit_Click(object sender, EventArgs e)
        {



        }
        protected void btnsignUpsubmit_Click(object sender, EventArgs e)
        {
            if (Session["UserVo"] != null)
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
                if (isValidUser)
                {
                    Session["UserVo"] = UserVo;
                    Response.Redirect("Questionnaire.aspx");
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
