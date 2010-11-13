using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WealthERP.Base;

namespace WealthERP.FP
{
    public partial class ByPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            //Response.Redirect(Session[SessionContents.LogoPath].ToString());
            Response.Redirect("\\Images\\logo.jpg");
        }
    }
}
