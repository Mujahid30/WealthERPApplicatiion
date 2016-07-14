using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoFPSuperlite;
using VoUser;

namespace WealthERP.FP
{
    public partial class CustomerCashFlowView : System.Web.UI.UserControl
    {
        CustomerFPAnalyticsBo customerFPAnalyticsBo = new CustomerFPAnalyticsBo();
        CustomerVo customerVo = new CustomerVo();
        //CustomerVo customerVo;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindCustomerCashFlowDetailsGrid();
        }


        public void BindCustomerCashFlowDetailsGrid()
        {
            //DataSet ds = customerFPAnalyticsBo.GetCustomerCashFlowRecomendation(customerVo.customerId);
            //gvCashFlowDetails.DataSource = ds.Tables[0];
            //gvCashFlowDetails.DataBind();
        }
    }
}