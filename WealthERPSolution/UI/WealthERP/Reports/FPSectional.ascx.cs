using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using BoFPSuperlite;
using VoUser;
using BoCustomerProfiling;
using System.Data;
using System.Text;
using WealthERP.Base;

namespace WealthERP.Reports
{
    public partial class FPSectional : System.Web.UI.UserControl
    {
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo adviserVo = new AdvisorVo();
        CustomerVo customerVo = new CustomerVo();

        int customerId = 0;
        bool CustomerLogin = false;
        bool strFromCustomerDashBoard = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            msgRecordStatus.Visible = false;
            customerVo = (CustomerVo)Session["customerVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            

            if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
            {
                //hidBMLogin.Value = "False";
                txtCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                //txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                //txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
            {
                //hidBMLogin.Value = "False";
                txtCustomer_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                //txtParentCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                txtCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                //txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";

            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
            {
                //hidBMLogin.Value = "true";
                txtCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                //txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtCustomer_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                //txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetBMParentCustomerNames";

            }
            if (Session["IsCustomerDrillDown"] != null)
            {
                if (customerVo != null)
                    customerId = customerVo.CustomerId;
                tblCustomer.Visible = false;
            }
            else
            {
                tblCustomer.Visible = true;
                if (hdnCustomerId.Value != "0" && hdnCustomerId.Value != "")
                    customerId = int.Parse(hdnCustomerId.Value);
            }
            
            
                if (!IsPostBack)
                {

                    //if (Session[SessionContents.CurrentUserRole].ToString() == "RM" ||
                    //Session[SessionContents.CurrentUserRole].ToString() == "Admin" ||
                    //Session[SessionContents.CurrentUserRole].ToString() == "BM")
                    //{
                    //    customerVo = null;
                    //}

                    if (Session["IsCustomerDrillDown"] != null)
                    {
                        if (customerVo != null)
                            customerId = customerVo.CustomerId;

                        if (Session["IsCustomerDrillDown"].ToString() == "Yes")
                        {
                            tblCustomer.Visible = false;
                            trIndCustomer.Visible = false;
                            btnViewReport.Visible = true;
                            btnViewInPDF.Visible = true;
                            btnViewInDOC.Visible = true;
                        }

                    }
                    else
                    {

                        tblCustomer.Visible = true;
                        trIndCustomer.Visible = true;
                        btnViewReport.Visible = false;
                        btnViewInPDF.Visible = false;
                        btnViewInDOC.Visible = false;

                    }
                }
            }
        
       
       

    
      

      
        protected void hdnCustomerId_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnCustomerId.Value.ToString().Trim()))
            {
                customerVo = customerBo.GetCustomer(int.Parse(hdnCustomerId.Value));
                Session["customerVo"] = customerVo;
                customerId = int.Parse(hdnCustomerId.Value);


                tdReportButtons.Visible = true;
                btnViewReport.Visible = true;
                btnViewInPDF.Visible = true;
                btnViewInDOC.Visible = true;
            }
                 
        }

        //protected void ddlBtnSelect_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlBtnSelect.SelectedIndex != -1)
        //    {
        //        if (ddlBtnSelect.SelectedValue == "ViewReport")
        //        {
        //            btnViewReport.Visible = true;
        //            btnViewInPDF.Visible = false;
        //            btnViewInDOC.Visible = false;
        //        }
        //        if (ddlBtnSelect.SelectedValue == "ViewInPdf")
        //        {
        //            btnViewReport.Visible = false;
        //            btnViewInPDF.Visible = true;
        //            btnViewInDOC.Visible = false;
        //        }
        //        if (ddlBtnSelect.SelectedValue == "ViewInDoc")
        //        {
        //            btnViewReport.Visible = false;
        //            btnViewInPDF.Visible = false;
        //            btnViewInDOC.Visible = true;
        //        }
        //    }
        //}

        //protected void btnAddPara_Click(object sender, EventArgs e)
        //{
        //    PlaceHolder PlaceHolder1 = new PlaceHolder();
        //    int txtCount = 5;

        //    txtCount = int.Parse(ddlParaList.SelectedValue);

        //    //if (ViewState["txtPrevieusID"] != null)
        //    //{
        //    //    txtId = int.Parse(ViewState["txtPrevieusID"].ToString())+1;
        //    //}

        //    for (int txtId = 1; txtId <= txtCount; txtId++)
        //    {
        //        TextBox txtDynamic = new TextBox();
        //        Label lblDynamic = new Label();
        //        lblDynamic.ID = "lblParagraph" + txtId.ToString();
        //        lblDynamic.Text = "Paragraph " + txtId.ToString();
        //        lblDynamic.CssClass = "Field";
        //        txtDynamic.CssClass = "txtField";
        //        txtDynamic.Style["Width"] = "700px";
        //        txtDynamic.Style["Height"] = "80px";
        //        PlaceHolder1.Controls.Add(lblDynamic);
        //        txtDynamic.ID = "txtRmRecommendation" + txtId.ToString();
        //        txtDynamic.TextMode = TextBoxMode.MultiLine;
        //        PlaceHolder1.Controls.Add(txtDynamic);
        //        PlaceHolder1.Controls.Add(new LiteralControl("<br /><br />"));
        //    }
        //    pnlCustomizedtext.Controls.Add(PlaceHolder1);

        //}
        //protected void btnSubmitPara_OnClick(object sender, EventArgs e)
        //{
        //    StringBuilder strRMRecommendationText = new StringBuilder();
        //    strRMRecommendationText.Append("<html><head><title></title></head><body><table>");
        //    string strTR = "<tr><td><p align=" + "justify" + ">&nbsp;</p></td></tr>";
        //    string strParaText = string.Empty;
        //    for (int i = 1; i <= int.Parse(ddlParaList.SelectedValue); i++)
        //    {
        //        TextBox txtRmRecommendation =(TextBox) this.FindControl("txtRmRecommendation" + i.ToString());
        //        if (txtRmRecommendation != null)
        //        {
        //            strParaText = txtRmRecommendation.Text;
 
        //        }
        //        if (!string.IsNullOrEmpty(strParaText))
        //        { 
        //             strRMRecommendationText.Append(strTR.Replace("&nbsp;",strParaText));

        //        }

             
        //    }

        //}

       

    }
}
