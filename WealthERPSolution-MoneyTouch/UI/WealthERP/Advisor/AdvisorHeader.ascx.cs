using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class AdvisorHeader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            UserVo userVo=new UserVo();
            userVo=(UserVo)Session["UserVo"];
            lblUser.Text = userVo.FirstName.ToString() + " " + userVo.LastName.ToString();
        }
    }
}