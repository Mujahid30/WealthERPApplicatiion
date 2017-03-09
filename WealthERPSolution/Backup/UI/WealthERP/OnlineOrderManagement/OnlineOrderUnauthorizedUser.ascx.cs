using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WealthERP.OnlineOrderManagement
{
    public partial class OnlineOrderUnauthorizedUser : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wsedrftgyhjukloghjnnnghj", " showMsg('Owing to heavy  load, our systems are experiencing a technical problem that is preventing you from transacting in mutual funds. Kindly contact our call center or send us a mail to contact@sbicapsec.com ','I');", true);
        }
    }
}