using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoCustomerProfiling;
using BoCommon;
using BoReports;
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
        WERPReports CommonReport = new WERPReports();
        //string path = string.Empty;
        //int AdvisorRMId=0;
        //string customerAddress = null;
        string fullState = null;
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
            //if (Session["customerVo"]!=null)
            //customerVo = (CustomerVo)Session["customerVo"];
            if (!IsPostBack)
            {
                //if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                //{
                //    rmVo = (RMVo)Session[SessionContents.RmVo];
                //    AdvisorRMId = rmVo.RMId;
                //    txtCustomer_autoCompleteExtender.ServiceMethod = "GetCustomerName";
                //}
                //else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                //{
                //    advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                //    AdvisorRMId = advisorVo.advisorId;
                //    txtCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                //}


                //if ((Session["FP_UserID"] == null) && (Session["FP_UserName"] == null))
                //{
                //    SessionBo.CheckSession();
                //    //rmVo = (RMVo)Session[SessionContents.RmVo];
                //    //txtPickCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                //    txtCustomer_autoCompleteExtender.ContextKey = AdvisorRMId.ToString();
                    
                //}
                //else
                //{

                //    if (Session[SessionContents.FPS_ProspectList_CustomerId] != null)
                //    {
                //        Session["FP_UserID"] = Session[SessionContents.FPS_ProspectList_CustomerId];
                //    }

                //    txtCustomer.Text = Session["FP_UserName"].ToString();
                //    txtCustomerId.Value = Session["FP_UserID"].ToString();
                //    customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
                //    Session["CusVo"] = customerVo;

                //    //DataTable dt = customerBo.GetCustomerPanAddress();
                //    //DataRow dr = dt.Rows[0];


                //    showAddress(int.Parse(txtCustomerId.Value));
                //    //lblAddress1.Text = customerVo.Adr1Line1 + "," + customerVo.Adr1Line2 + "," + customerVo.Adr1Line3;
                //    //lblAddress2.Text = customerVo.Adr1City + "," + customerVo.Adr2City;
                //    //lblAddress3.Text = fullState + "-" + customerVo.Adr2PinCode;

                //    //txtPanParent.Text = dr["C_PANNum"].ToString();
                //    //trCustomerDetails1.Visible = true;
                //    //trCustomerDetails2.Visible = true;
                //    //trCustomerDetails3.Visible = true;
                //    //trCustomerDetails4.Visible = true;
                   
                //    txtCustomer_autoCompleteExtender.ContextKey = AdvisorRMId.ToString();
                //}

                //Trigger();
            }

        }

        //protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        //{
        //    CustomerBo customerBo = new CustomerBo();
        //    if (txtCustomerId.Value != string.Empty)
        //    {
        //        //Maintaining customer session throughout the financial planning Module //Pramoda//
        //        Session["FP_UserID"] = txtCustomerId.Value;
        //        Session["FP_UserName"] = txtCustomer.Text;

        //        customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
        //        Session["CusVo"] = customerVo;
        //        showAddress(int.Parse(txtCustomerId.Value));
                             
               
        //    }
           

        //}
        //protected void Trigger()
        //{


        //    if (Session[SessionContents.FPS_ProspectList_CustomerId].ToString() != string.Empty && Session[SessionContents.FPS_ProspectList_CustomerId] != null)
        //    {
        //        //ParentcustomerID = int.Parse(txtCustomerId.Value);

        //        Session["FP_UserID"] = Session[SessionContents.FPS_ProspectList_CustomerId].ToString();
        //        //Session["FP_UserName"] = txtPickCustomer.Text;
        //        showAddress(int.Parse(Session["FP_UserID"].ToString()));

        //    }

         

        //}

        //public void showAddress(int customerId)
        //{
        //    SessionBo.CheckSession();
        //    DataTable dt = customerBo.GetCustomerPanAddress(customerId);
        //    DataRow dr = dt.Rows[0];
        //    string path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["xmllookuppath"].ToString());

        //    if (!string.IsNullOrEmpty(customerVo.Adr1State))
        //        fullState = CommonReport.GetState(path, customerVo.Adr1State);

        //    lblAddress1.Text = customerVo.Adr1Line1 + " " + customerVo.Adr1Line2 + " " + customerVo.Adr1Line3;
        //    lblAddress2.Text = customerVo.Adr1City + " " + customerVo.Adr2PinCode;
        //    lblAddress3.Text = fullState;
        //    txtPanParent.Text = dr["C_PANNum"].ToString();
        //    trCustomerDetails1.Visible = true;
        //    trCustomerDetails2.Visible = true;
        //    trCustomerDetails3.Visible = true;
        //    trCustomerDetails4.Visible = true;

        //}
    }
}