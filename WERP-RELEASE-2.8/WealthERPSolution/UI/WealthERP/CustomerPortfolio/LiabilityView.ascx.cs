using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoUser;
using WealthERP.Base;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;
using BoCalculator;

namespace WealthERP.CustomerPortfolio
{
    public partial class LiabilityView : System.Web.UI.UserControl
    {
        List<LiabilitiesVo> liabilitiesListVo = null;
        LiabilitiesBo liabilitiesBo = new LiabilitiesBo();
        CustomerVo customerVo = null;
        Calculator calculator = new Calculator();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session[SessionContents.CustomerVo];

            if (!IsPostBack)
            {
                BindGridview();
            }
        }

        protected void BindGridview()
        {
            liabilitiesListVo = new List<LiabilitiesVo>();
            LiabilitiesVo liabilityVo = new LiabilitiesVo();
            liabilitiesListVo = liabilitiesBo.GetLiabilities(customerVo.CustomerId);
            DataTable dt = new DataTable();
            DataRow dr;
            Double loanOutStanding=0;
            DateTime nextInsDate=new DateTime();
            if (liabilitiesListVo != null)
            {
                lblMsg.Visible = false;
                dt.Columns.Add("LiabilityId");
                dt.Columns.Add("Loan Type");
                dt.Columns.Add("Lender");
                dt.Columns.Add("Amount");
                dt.Columns.Add("Rate of Interest");
                dt.Columns.Add("PaymentType");
                dt.Columns.Add("LumpsusmInstallment");
                dt.Columns.Add("LoanOutstanding");
                dt.Columns.Add("NextInstallmentDate");
                dt.Columns.Add("Frequency");
                for (int i = 0; i < liabilitiesListVo.Count; i++)
                {
                    dr = dt.NewRow();
                    liabilityVo = liabilitiesListVo[i];
                    dr[0] = liabilityVo.LiabilitiesId;
                    dr[1] = liabilityVo.LoanType.ToString();
                    dr[2] = liabilityVo.LoanPartner.ToString();
                    dr[3] = decimal.Parse(liabilityVo.LoanAmount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")); 
                    dr[4] = liabilityVo.RateOfInterest.ToString();
                    if (liabilityVo.PaymentOptionCode == 1)
                    {
                        dr[5] = "Lumpsum";
                        dr[6] = Math.Round(liabilityVo.LumpsumRepaymentAmount, 2).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        //loanOutStanding = calculator.GetLoanOutstanding(liabilityVo.CompoundFrequency, liabilityVo.LoanAmount, liabilityVo.InstallmentStartDate, liabilityVo.InstallmentEndDate, 1, liabilityVo.LumpsumRepaymentAmount, liabilityVo.NoOfInstallments);
                        //loanOutStanding = liabilityVo.OutstandingAmount;
                        //dr[7] = Math.Round(loanOutStanding, 2).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        dr[7] = liabilityVo.OutstandingAmount;
                        dr[8] = "-";
                        dr[9] = "-";
                    }
                    else if (liabilityVo.PaymentOptionCode == 2)
                    {
                        dr[5] = "Installment";
                        dr[6] = Math.Round(liabilityVo.EMIAmount, 2).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        //loanOutStanding = calculator.GetLoanOutstanding(liabilityVo.FrequencyCodeEMI, liabilityVo.LoanAmount, liabilityVo.InstallmentStartDate, liabilityVo.InstallmentEndDate, 2, liabilityVo.EMIAmount, liabilityVo.NoOfInstallments);
                        //loanOutStanding = liabilityVo.OutstandingAmount;
                        //dr[7] = Math.Round(loanOutStanding, 2).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        dr[7] = liabilityVo.OutstandingAmount;
                        nextInsDate = calculator.GetNextPremiumDate(liabilityVo.InstallmentStartDate, liabilityVo.InstallmentEndDate, liabilityVo.FrequencyCodeEMI);
                        if (nextInsDate != DateTime.MinValue)
                            dr[8] = nextInsDate.ToLongDateString();
                        else
                            dr[8] = "-";
                        switch (liabilityVo.FrequencyCodeEMI)
                        {

                            case "MN":
                                dr[9] = "Monthly";
                                break;
                            case "QT":
                                dr[9] = "Quarterly";
                                break;

                            case "HY":
                                dr[9] = "Half Yearly";
                                break;

                            case "YR":
                                dr[9] = "Yearly";
                                break;
                        }
                    }
                    else
                    {
                        dr[5] = "-";
                        dr[6] = "-";
                        //loanOutStanding = calculator.GetLoanOutstanding(liabilityVo.FrequencyCodeEMI, liabilityVo.LoanAmount, liabilityVo.InstallmentStartDate, liabilityVo.InstallmentEndDate, 2, liabilityVo.EMIAmount, liabilityVo.NoOfInstallments);
                        dr[7] = "-";
                        //nextInsDate = calculator.GetNextPremiumDate(liabilityVo.InstallmentStartDate, liabilityVo.InstallmentEndDate, liabilityVo.FrequencyCodeEMI);
                        dr[8] = "-";
                        dr[9] = "-";
                    }
                    dt.Rows.Add(dr);
                }
                gvLiabilities.DataSource = dt;
                gvLiabilities.DataBind();
            }

            else
            {
                lblMsg.Visible = true;
            }




        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string menu;
            int liabilityId = 0;
            LiabilitiesVo liabilityVo = new LiabilitiesVo();
            try
            {
                DropDownList MyDropDownList = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)MyDropDownList.NamingContainer;
                int selectedRow = gvr.RowIndex;
                liabilityId = int.Parse(gvLiabilities.DataKeys[selectedRow].Value.ToString());
                liabilityVo = liabilitiesBo.GetLiabilityDetails(liabilityId);
                
                menu = MyDropDownList.SelectedItem.Value.ToString();
              
                if (menu == "View")
                {
                    Session["menu"] = "View";
                    Session["liabilityVo"] = liabilityVo;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('LiabilitiesMaintenanceForm','none');", true);
                }
                if (menu == "Edit")
                {
                     Session["menu"]="Edit";
                     Session["liabilityVo"] = liabilityVo;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('LiabilitiesMaintenanceForm','none');", true);
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "LiabilityView.ascx:ddlMenu_SelectedIndexChanged()");
                object[] objects = new object[2];
                objects[1] = liabilityVo;
                objects[2] = liabilityId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvLiabilities_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLiabilities.PageIndex = e.NewPageIndex;
            BindGridview();
        }
    }
}