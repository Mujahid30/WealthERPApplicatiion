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

namespace WealthERP.Reports
{
    public partial class FPSectional : System.Web.UI.UserControl
    {
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo adviserVo = new AdvisorVo();
        CustomerVo customerVo = new CustomerVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            msgRecordStatus.Visible = false;
            customerVo = (CustomerVo)Session["customerVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                SetDefalutView();
                DefaultFPReportsAssumtion();
                btnSubmit.Enabled = false;
                getCustomerRMRecommendationText();
                setRecommendationControlReadOnly(true);
            }
        }
        public void DefaultFPReportsAssumtion()
        {
            DataSet dsDefaultFPReportsAssumtion = new DataSet();
            dsDefaultFPReportsAssumtion = customerBo.DefaultFPReportsAssumtion(customerVo.CustomerId);
            if (dsDefaultFPReportsAssumtion.Tables[0].Rows.Count > 0)
            {
                txtInflation.Text = dsDefaultFPReportsAssumtion.Tables[0].Rows[0][0].ToString();
                txtInvestmentReturn.Text = dsDefaultFPReportsAssumtion.Tables[0].Rows[1][0].ToString();
                txtDR.Text = dsDefaultFPReportsAssumtion.Tables[0].Rows[2][0].ToString();
            }

        }
        public void SetDefalutView()
        {
            txtDR.Enabled = false;
            txtInflation.Enabled = false;
            txtInvestmentReturn.Enabled = false;
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            decimal assumptionInflation = 0;
            decimal assumptionInvestment = 0;
            decimal assumptionDr = 0;
            if (txtInflation.Text != "")
            {
                assumptionInflation = decimal.Parse((txtInflation.Text).ToString());
            }
            if (txtInvestmentReturn.Text != "")
            {
                assumptionInvestment = decimal.Parse((txtInvestmentReturn.Text).ToString());
            }

            if (txtDR.Text != "")
            {
                assumptionDr = decimal.Parse((txtDR.Text).ToString());
            }
            customerBo.CustomerFPReportsAssumption(customerVo.CustomerId, assumptionInflation, assumptionInvestment, assumptionDr);
            msgRecordStatus.Visible = true;
            SetDefalutView();
            btnSubmit.Enabled = false;


        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            txtDR.Enabled = true;
            txtInflation.Enabled = true;
            txtInvestmentReturn.Enabled = true;
            btnSubmit.Enabled = true;
        }

        protected void txtInvestmentReturn_TextChanged(object sender, EventArgs e)
        {
            double investmentRT = 0;
            double inflationRate = 0;
            double discountRate = 0;
           if(!string.IsNullOrEmpty(txtInvestmentReturn.Text.Trim()))
               investmentRT= double.Parse(txtInvestmentReturn.Text);
           if (!string.IsNullOrEmpty(txtInflation.Text.Trim()))
               inflationRate = double.Parse(txtInflation.Text);

           discountRate = (((100 + investmentRT) / (100 + inflationRate ))-1)*100;
           txtDR.Text= Math.Round(discountRate,2).ToString();


        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if(Session["customerVo"]!=null)
             customerVo = (CustomerVo)Session["customerVo"];
            StringBuilder strRMRecommendationText = new StringBuilder();
            strRMRecommendationText.Append("<html><head><title></title></head><body><table>");
            string strTR = "<tr><td><p align=\"justify\">&nbsp;</p></td></tr>";
            string strParaText = string.Empty;
            if (!string.IsNullOrEmpty(txtParagraph1.Text.Trim()))
            {
                strRMRecommendationText.Append(strTR.Replace("&nbsp;", txtParagraph1.Text));
                strRMRecommendationText.Append(strTR);
            }

            if (!string.IsNullOrEmpty(txtParagraph2.Text.Trim()))
            {
                strRMRecommendationText.Append(strTR.Replace("&nbsp;", txtParagraph2.Text));
                strRMRecommendationText.Append(strTR);
            }

            if (!string.IsNullOrEmpty(txtParagraph3.Text.Trim()))
            {
                strRMRecommendationText.Append(strTR.Replace("&nbsp;", txtParagraph3.Text));
                strRMRecommendationText.Append(strTR);
            }

            if (!string.IsNullOrEmpty(txtParagraph4.Text.Trim()))
            {
                strRMRecommendationText.Append(strTR.Replace("&nbsp;", txtParagraph4.Text));
                strRMRecommendationText.Append(strTR);
            }

            if (!string.IsNullOrEmpty(txtParagraph5.Text.Trim()))
            {
                strRMRecommendationText.Append(strTR.Replace("&nbsp;", txtParagraph5.Text));
                strRMRecommendationText.Append(strTR);
            }

            strRMRecommendationText.Append("</table></body></html>");

            customerBo.AddRMRecommendationForCustomer(customerVo.CustomerId, Convert.ToString(strRMRecommendationText));
            getCustomerRMRecommendationText();
            setRecommendationControlReadOnly(true);

        }

        protected void btnEditRMRec_Click(object sender, EventArgs e)
        {
            setRecommendationControlReadOnly(false);
        }

        
        private void setRecommendationControlReadOnly(bool flag)
        {
            if (flag == true)
            {
                txtParagraph1.ReadOnly = true;
                txtParagraph2.ReadOnly = true;
                txtParagraph3.ReadOnly = true;
                txtParagraph4.ReadOnly = true;
                txtParagraph5.ReadOnly = true;
            }
            else
            {
                txtParagraph1.ReadOnly = false;
                txtParagraph2.ReadOnly = false;
                txtParagraph3.ReadOnly = false;
                txtParagraph4.ReadOnly = false;
                txtParagraph5.ReadOnly = false;
 
            }
        }

        private void getCustomerRMRecommendationText()
        {
            string strRMRecommendationHTML;
            string[] strRMRecTR;
            string[] stringSeparators = new string[] { "<p align=\"justify\">" };
            string strParagraph="";
            int index=0;

            txtParagraph1.Text = string.Empty;
            txtParagraph2.Text = string.Empty;
            txtParagraph3.Text = string.Empty;
            txtParagraph4.Text = string.Empty;
            txtParagraph5.Text = string.Empty;

            if (Session["customerVo"] != null)
                customerVo = (CustomerVo)Session["customerVo"];
            strRMRecommendationHTML = customerBo.GetRMRecommendationForCustomer(customerVo.CustomerId);           
            strRMRecTR = strRMRecommendationHTML.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            if (strRMRecTR.Count() > 0)
            {
                foreach (string str in strRMRecTR)
                {
                    if (str.Contains("</p></td></tr><tr><td>"))
                    {
                      index=str.IndexOf("</p></td></tr><tr><td>");
                      strParagraph = str.Remove(index, 22);
                    }
                    else if(str.Contains("</p></td></tr></table></body></html>"))
                    {
                        index = str.IndexOf("</p></td></tr></table></body></html>");
                        strParagraph = str.Remove(index, 36);
                    }
                    if (string.IsNullOrEmpty(txtParagraph1.Text) && !string.IsNullOrEmpty(strParagraph) && strParagraph.Trim() != "&nbsp;")
                    {
                        txtParagraph1.Text = strParagraph;
                    }
                    else if (string.IsNullOrEmpty(txtParagraph2.Text) && !string.IsNullOrEmpty(strParagraph) && strParagraph.Trim() != "&nbsp;")
                    {
                        txtParagraph2.Text = strParagraph;
                    }
                    else if (string.IsNullOrEmpty(txtParagraph3.Text) && !string.IsNullOrEmpty(strParagraph) && strParagraph.Trim() != "&nbsp;")
                    {
                        txtParagraph3.Text = strParagraph;
                    }
                    else if (string.IsNullOrEmpty(txtParagraph4.Text) && !string.IsNullOrEmpty(strParagraph) && strParagraph.Trim() != "&nbsp;")
                    {
                        txtParagraph4.Text = strParagraph;
                    }
                    else if (string.IsNullOrEmpty(txtParagraph5.Text) && !string.IsNullOrEmpty(strParagraph) && strParagraph.Trim() != "&nbsp;")
                    {
                        txtParagraph5.Text = strParagraph;
                    }
                }
                
 
            }
 
        }

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