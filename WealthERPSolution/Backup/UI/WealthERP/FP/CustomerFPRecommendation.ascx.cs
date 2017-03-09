using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using VoUser;
using BoCustomerProfiling;
using System.Text;
using WealthERP.Base;

namespace WealthERP.FP
{
    public partial class CustomerFPRecommendation : System.Web.UI.UserControl
    {
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo adviserVo = new AdvisorVo();
        CustomerVo customerVo = new CustomerVo();

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();         
            customerVo = (CustomerVo)Session["customerVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];

            if (!IsPostBack)
            {
                getCustomerRMRecommendationText();
                setRecommendationControlReadOnly(true);
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
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

        private void getCustomerRMRecommendationText()
        {
            string strRMRecommendationHTML;
            string[] strRMRecTR;
            string[] stringSeparators = new string[] { "<p align=\"justify\">" };
            string strParagraph = "";
            int index = 0;

            txtParagraph1.Text = string.Empty;
            txtParagraph2.Text = string.Empty;
            txtParagraph3.Text = string.Empty;
            txtParagraph4.Text = string.Empty;
            txtParagraph5.Text = string.Empty;

            //if (Session["customerVo"] != null)
            //    customerVo = (CustomerVo)Session["customerVo"];
            //if (customerVo != null)
            //    customerId = customerVo.CustomerId;
            //else
            //{
            //    if (!String.IsNullOrEmpty(hdnCustomerId.Value))
            //        customerId = Convert.ToInt32(hdnCustomerId.Value);
            //}
            strRMRecommendationHTML = customerBo.GetRMRecommendationForCustomer(customerVo.CustomerId);
            strRMRecTR = strRMRecommendationHTML.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            if (strRMRecTR.Count() > 0)
            {
                foreach (string str in strRMRecTR)
                {
                    if (str.Contains("</p></td></tr><tr><td>"))
                    {
                        index = str.IndexOf("</p></td></tr><tr><td>");
                        strParagraph = str.Remove(index, 22);
                    }
                    else if (str.Contains("</p></td></tr></table></body></html>"))
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
    }
}