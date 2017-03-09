using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class NewDPAccountEntry : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
        }

        protected void ButbtnAddTran_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EquityManualSingleTransaction','login');", true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}