using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using WealthERP.Base;
using BoCommon;

namespace WealthERP.General
{
    public partial class SetTheme : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userBo = new UserBo();
            userVo = (UserVo)Session[SessionContents.UserVo];
            if (Session["Theme"] != null)
            {
                ddlTheme.SelectedValue = Session["Theme"].ToString();
            }
            
        }

        protected void ddlTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTheme.SelectedIndex != 0)
            {
                Session["Theme"] = ddlTheme.SelectedValue;
                userBo.UpdateUserTheme(userVo.UserId, ddlTheme.SelectedValue);
            }
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "window.parent.location.href = window.parent.location.href;", true);
        }
    }
}