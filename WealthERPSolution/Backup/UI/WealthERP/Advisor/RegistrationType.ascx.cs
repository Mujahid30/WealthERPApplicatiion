using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class RegistrationType : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // SessionBo.CheckSession();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (rbtnAdviser.Checked)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdvisorRegistration','none');", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('DirectInvestorRegistration','none');", true);
            }
        }
    }
}
