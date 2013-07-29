using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoWerpAdmin;

namespace WealthERP.CommisionManagement
{
    public partial class ViewSchemeStructureAssociation : System.Web.UI.UserControl
    {
        PriceBo priceBo = new PriceBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = priceBo.GetStructureoverScheme();
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
        }

    }
}