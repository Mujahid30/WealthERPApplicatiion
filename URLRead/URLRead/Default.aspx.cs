using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace URLRead
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String TUrl = Request.Url.ToString();
            int i = TUrl.IndexOf("http://");
            if (i > -1)
            {
                Response.Redirect("https://www.wealtherp.com");
            }


        }
    }
}
