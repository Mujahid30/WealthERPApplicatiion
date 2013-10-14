using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoCustomerProfiling;

namespace WealthERP.OnlineOrder
{
    public partial class MFOnlineOrder : System.Web.UI.Page
    {
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Theme"] != null)
            {
                Page.Theme = Session["Theme"].ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                customerVo = customerBo.GetCustomer(7709);
                Session["CustomerVo"] = customerVo;
            }

        }

        protected void lblSignOut_Click(object sender, EventArgs e)
        {
            string currentURL = string.Empty;
            if (Request.ServerVariables["HTTPS"].ToString() == "")
            {
                currentURL = Request.ServerVariables["SERVER_PROTOCOL"].ToString().ToLower().Substring(0, 4).ToString() + "://" + Request.ServerVariables["SERVER_NAME"].ToString() + ":" + Request.ServerVariables["SERVER_PORT"].ToString() + Request.ServerVariables["SCRIPT_NAME"].ToString();
            }
            if (currentURL.Contains("localhost"))
            {
                Session.Abandon();
                Response.Redirect(currentURL);
            }
            else
            {

                Session.Abandon();
                Response.Redirect("http://sspr.sbicapstestlab.com/AGLogout");

            }
        }
    }
}
