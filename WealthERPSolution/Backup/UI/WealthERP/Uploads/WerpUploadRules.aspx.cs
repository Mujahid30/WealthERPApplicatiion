using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;

namespace WealthERP.Uploads
{
    public partial class WerpUploadRules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
        }
    }
}
