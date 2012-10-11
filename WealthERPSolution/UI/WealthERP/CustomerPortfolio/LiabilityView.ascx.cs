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
using Telerik.Web.UI;

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
            btnExportFilteredData.Visible = false;
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
            Double loanOutStanding = 0;
            DateTime nextInsDate = new DateTime();

            if (liabilitiesListVo != null)
            {
                btnExportFilteredData.Visible = true;
                trErrorMessage.Visible = false;
                dt.Columns.Add("LiabilityId");
                dt.Columns.Add("Loan Type");
                dt.Columns.Add("Lender");
                dt.Columns.Add("Amount", typeof(double));
                dt.Columns.Add("Rate of Interest");
                dt.Columns.Add("PaymentType");
                dt.Columns.Add("LumpsusmInstallment",typeof(double));
                dt.Columns.Add("LoanOutstanding", typeof(double));
                dt.Columns.Add("NextInstallmentDate",typeof(DateTime));
                dt.Columns.Add("Frequency");
                dt.Columns.Add("CL_AssetParticular");
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
                        //if(nextInsDate!=DateTime.MinValue)
                        //dr[8] = " ";
                        dr[9] = " ";
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
                            dr[8] = nextInsDate.ToShortDateString();
                        //else
                        //    dr[8] = " ";
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
                        dr[5] = " ";
                        dr[6] = " ";
                        //loanOutStanding = calculator.GetLoanOutstanding(liabilityVo.FrequencyCodeEMI, liabilityVo.LoanAmount, liabilityVo.InstallmentStartDate, liabilityVo.InstallmentEndDate, 2, liabilityVo.EMIAmount, liabilityVo.NoOfInstallments);
                        dr[7] = " ";
                        //nextInsDate = calculator.GetNextPremiumDate(liabilityVo.InstallmentStartDate, liabilityVo.InstallmentEndDate, liabilityVo.FrequencyCodeEMI);
                        dr[8] = " ";
                        dr[9] = " ";
                    }
                    dr[10] = liabilityVo.AssetParticular;
                    dt.Rows.Add(dr);
                }
                gvLiabilities.DataSource = dt;
                gvLiabilities.DataBind();

                if (Cache["dtLiabilities + '"+customerVo.CustomerId+"'"] == null)
                {
                    Cache.Insert("dtLiabilities + '" + customerVo.CustomerId + "'", dt);
                }
                else
                {
                    Cache.Remove("dtLiabilities + '" + customerVo.CustomerId + "'");
                    Cache.Insert("dtLiabilities + '" + customerVo.CustomerId + "'", dt);
                }

            }

            else
            {
                trErrorMessage.Visible = true;
                gvLiabilities.Visible = false;
                btnExportFilteredData.Visible = false;
            }
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string menu;
            int liabilityId = 0;
            LiabilitiesVo liabilityVo = new LiabilitiesVo();
            try
            {
                RadComboBox MyDropDownList = (RadComboBox)sender;
                GridDataItem gvr = (GridDataItem)MyDropDownList.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;
                liabilityId = int.Parse(gvLiabilities.MasterTableView.DataKeyValues[selectedRow - 1]["LiabilityId"].ToString());
                liabilityVo = liabilitiesBo.GetLiabilityDetails(liabilityId);

                hdndeleteId.Value = liabilityId.ToString();
                menu = MyDropDownList.SelectedItem.Value.ToString();

                if (menu == "View")
                {
                    Session["menu"] = "View";
                    Session["liabilityVo"] = liabilityVo;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('LiabilitiesMaintenanceForm','none');", true);
                }
                if (menu == "Edit")
                {
                    Session["menu"] = "Edit";
                    Session["liabilityVo"] = liabilityVo;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('LiabilitiesMaintenanceForm','none');", true);
                }
                if (menu == "Delete")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }

        //protected void gvLiabilities_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvLiabilities.PageIndex = e.NewPageIndex;
        //    BindGridview();
        //}

        protected void hiddenassociation_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                liabilitiesBo.DeleteLiabilityPortfolio(int.Parse(hdndeleteId.Value));
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('LiabilityView','login');", true);
                msgRecordStatus.Visible = true;
            }
        }
        protected void gvLiabilities_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            gvLiabilities.Visible = true;
            DataTable dt = new DataTable();

            btnExportFilteredData.Visible = true;
            dt = (DataTable)Cache["dtLiabilities + '" + customerVo.CustomerId + "'"];
            gvLiabilities.DataSource = dt;
        }
        protected void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {

            gvLiabilities.ExportSettings.OpenInNewWindow = true;
            gvLiabilities.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvLiabilities.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvLiabilities.MasterTableView.ExportToExcel();

        }
    }
}
