using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;

namespace WealthERP.General
{
    public partial class ChangeTempPassword : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
        }

        protected void lnkChangePassword_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ChangePassword','none');", true);
            Session["ChangeTempPass"] = "Y";
        }
    }
}