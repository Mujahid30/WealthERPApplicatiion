using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioMFEntry : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
        } 

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (rbtnManual.Checked && rbtnSingle.Checked)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFManualSingleTran','login');", true);
            }
            if (rbtnManual.Checked && rbtnMultiple.Checked)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFManualMultipleTran','login');", true);
            }
        }
    }
}