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
        int AdvisorRMId=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ////path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            //rmVo = (RMVo)Session[SessionContents.RmVo];
            //advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            
            //txtCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
            //if ((Session["FP_UserID"] != null) && (Session["FP_UserName"] != null))
            //{
            //    txtCustomer.Text = Session["FP_UserName"].ToString();
            //    txtCustomerId.Value = Session["FP_UserID"].ToString();
            //}

            SessionBo.CheckSession();
            if (!IsPostBack)
            {
                if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    rmVo = (RMVo)Session[SessionContents.RmVo];
                    AdvisorRMId = rmVo.RMId;
                    txtCustomer_autoCompleteExtender.ServiceMethod = "GetCustomerName";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                {
                    advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                    AdvisorRMId = advisorVo.advisorId;
                    txtCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                }


                if ((Session["FP_UserID"] == null) && (Session["FP_UserName"] == null))
                {
                    SessionBo.CheckSession();
                    //rmVo = (RMVo)Session[SessionContents.RmVo];
                    //txtPickCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtCustomer_autoCompleteExtender.ContextKey = AdvisorRMId.ToString();
                    
                }
                else
                {

                    txtCustomer.Text = Session["FP_UserName"].ToString();
                    txtCustomerId.Value = Session["FP_UserID"].ToString();
                    customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
                    Session["CusVo"] = customerVo;

                    DataTable dt = customerBo.GetCustomerPanAddress(int.Parse(txtCustomerId.Value));
                    DataRow dr = dt.Rows[0];

                    txtAddress.Text = customerVo.Adr1City;
                    txtPanParent.Text = dr["C_PANNum"].ToString();
                    trCustomerDetails1.Visible = true;
                    trCustomerDetails2.Visible = true;
                    SessionBo.CheckSession();
                    txtCustomer_autoCompleteExtender.ContextKey = AdvisorRMId.ToString();
                }
            }

        }

        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            CustomerBo customerBo = new CustomerBo();
            if (txtCustomerId.Value != string.Empty)
            {
                //Maintaining customer session throughout the financial planning Module //Pramoda//
                Session["FP_UserID"] = txtCustomerId.Value;
                Session["FP_UserName"] = txtCustomer.Text;

                customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
                Session["CusVo"] = customerVo;

                DataTable dt = customerBo.GetCustomerPanAddress(int.Parse(txtCustomerId.Value));
                DataRow dr = dt.Rows[0];
                txtAddress.Text = customerVo.Adr1City; ;
                txtPanParent.Text = dr["C_PANNum"].ToString();
                trCustomerDetails1.Visible = true;
                trCustomerDetails2.Visible = true;
            }
           

        }
    }
}