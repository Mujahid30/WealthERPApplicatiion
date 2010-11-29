using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WealthERP.Base;
using VoUser;

namespace WealthERP.FP
{
    public partial class ByPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {  
            AdvisorVo advisorvo=new AdvisorVo();
            if (advisorvo != null)
            {
                advisorvo = (AdvisorVo)Session["advisorVo"];
                Response.Redirect("\\Images\\" + advisorvo.LogoPath);
            }
            //Response.Redirect("\\Images\\logo.jpg");
        }
    }
}
