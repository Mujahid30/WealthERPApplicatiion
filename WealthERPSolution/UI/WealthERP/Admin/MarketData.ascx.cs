using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WealthERP.Admin
{
    public partial class MarketData : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void lnkEquityData_Click(object sender, EventArgs e)
        {
            string AssetId = "Equity";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdminPriceList", "loadcontrol('AdminPriceList','?AssetId=" + AssetId + "');", true);
           // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdminPriceList','login')", true);
        }

        protected void lnkMFData_Click(object sender, EventArgs e)
        {
            string AssetId = "MF";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AdminPriceList", "loadcontrol('AdminPriceList','?AssetId=" + AssetId + "');", true);
           // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdminPriceList','login')", true);
        }
    }
}