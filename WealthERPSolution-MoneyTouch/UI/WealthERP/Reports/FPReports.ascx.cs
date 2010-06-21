using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoCustomerProfiling;
using BoCommon;
using WealthERP.Base;
using System.Data;

namespace WealthERP.Reports
{
    public partial class FPReports : System.Web.UI.UserControl
    {

        RMVo rmVo = new RMVo();
        AdvisorVo advisorVo = new AdvisorVo();
        CustomerBo customerBo = new CustomerBo();
        //CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        CustomerVo customerVo = new CustomerVo();
        UserVo userVo = new UserVo();
        //string path = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            rmVo = (RMVo)Session[SessionContents.RmVo];
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            
            txtCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
            if ((Session["FP_UserID"] != null) && (Session["FP_UserName"] != null))
            {
                txtCustomer.Text = Session["FP_UserName"].ToString();
                txtCustomerId.Value = Session["FP_UserID"].ToString();
            }
        }

        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            CustomerBo customerBo = new CustomerBo();
            if (txtCustomerId.Value != string.Empty)
            {
                customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
                Session["CusVo"] = customerVo;

                DataTable dt = customerBo.GetCustomerPanAddress(int.Parse(txtCustomerId.Value));
                DataRow dr = dt.Rows[0];

                txtPanParent.Text = dr["C_PANNum"].ToString();
                trCustomerDetails.Visible = true;
               
            }
           

        }
    }
}