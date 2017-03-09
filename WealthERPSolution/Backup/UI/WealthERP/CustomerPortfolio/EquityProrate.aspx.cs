using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class EquityProrate : System.Web.UI.Page
    {
        float total;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Session["serviceTax"] = txtServiceTax.Text;
            Session["STT"] = txtSTT.Text;
            Session["AdditionalCharge"] = txtAdditionalCharge.Text;
            total=float.Parse(txtServiceTax.Text) + float.Parse(txtSTT.Text) + float.Parse(txtAdditionalCharge.Text);
            total=(float)decimal.Round((decimal)total,4);
            Session["total"]=total;
            Response.Write("<script language=javascript> window.close();</script>");
            Session["flag"] ="2";
        }
    }
}
