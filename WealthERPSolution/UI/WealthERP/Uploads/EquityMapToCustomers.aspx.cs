using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCustomerProfiling;
using System.Data;
using VoUser;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using BoUploads;
using BoCommon;

namespace WealthERP.Uploads
{
    public partial class EquityMapToCustomers : System.Web.UI.Page
    {
        
        AdvisorVo advisorVo = new AdvisorVo();
        int transactionStagingId;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Theme"] == null || Session["Theme"].ToString() == string.Empty)
            {
                Session["Theme"] = "PCG";
            }

            Page.Theme = Session["Theme"].ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            lblRefine.Visible = false;
            lblMessage.Text = "";
            transactionStagingId = Convert.ToInt32(Request.Params["id"]);
            
            if (advisorVo == null || advisorVo.advisorId < 1)
            {
                lblMessage.Visible = true;
                lblMessage.CssClass = "Error";
                lblMessage.Text = "your session has expired.Please close this window and login again.";
                btnSearch.Enabled = false;
                gvCustomers.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            if (advisorVo != null && advisorVo.advisorId > 0)
            {
                CustomerBo customerBo = new CustomerBo();
                DataSet dsCustomers = customerBo.SearchCustomers(advisorVo.advisorId, txtCustomerName.Text);
                gvCustomers.DataSource = dsCustomers;
                gvCustomers.DataBind();
                gvCustomers.Visible = true;
                if (dsCustomers.Tables[0].Rows.Count >= 100)
                    lblRefine.Visible = true;
                else
                    lblRefine.Visible = false;
            }
           
        }

        protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int userId = 0;
            int customerId = 0;
            customerId = Convert.ToInt32(e.CommandArgument);
            UserVo userVo = (UserVo)Session["userVo"];
            userId = userVo.UserId;
            CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
            CustomerAccountBo customerBo = new CustomerAccountBo();
            GridView gv = (GridView)sender;

            RejectedTransactionsBo rejectedTransactionsBo = new RejectedTransactionsBo();
            bool isSucess = rejectedTransactionsBo.MapEquityToCustomer(transactionStagingId, customerId, userId);
            if (isSucess)
            {
                gvCustomers.Visible = false;
                lblMessage.Visible = true;
                lblMessage.Text = "Customer is mapped";
                lblMessage.CssClass = "SuccessMsg";
                tblSearch.Visible = false;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "An error occurred while mapping.";
            }
        }
    }
}
