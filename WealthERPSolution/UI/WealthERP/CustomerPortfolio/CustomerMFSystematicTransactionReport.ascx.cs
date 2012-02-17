using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using BoCommon;
using VoUser;
using WealthERP.Base;

namespace WealthERP.CustomerPortfolio
{
    public partial class CustomerMFSystematicTransactionReport : System.Web.UI.UserControl
    {
        string path = string.Empty;
        double systematicTotalAmount = 0;
        double originalAmountTotal = 0;
        private List<MFSystematicTransactionReportVo> mfSystematicTransactionReportVoList = new List<MFSystematicTransactionReportVo>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                BindPeriodDropDown();
                int Day = 1;
                txtFromDate.Text = Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
                txtToDate.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
                SetSystematicTransactions();
            }
        }
        private void BindPeriodDropDown()
        {
            DataTable dtPeriod;
            dtPeriod = XMLBo.GetDatePeriod(path);
            ddlPeriod.DataSource = dtPeriod;
            ddlPeriod.DataTextField = "PeriodType";
            ddlPeriod.DataValueField = "PeriodCode";
            ddlPeriod.DataBind();
            ddlPeriod.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select a Period", "Select a Period"));
        }
        private void SetSystematicTransactions()
        {
            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
            mfSystematicTransactionReportVoList = new List<MFSystematicTransactionReportVo>();
            List<string> transactionTypeList = new List<string>();
            DateTime fromDate = new DateTime();
            DateTime toDate = new DateTime();
            DateBo dateBo = new DateBo();
            string customerNameSearch = "";
            string schemeNameSearch = "";
            string transType = "";
            string portfolioType = "";
            string viewType = "";
            systematicTotalAmount = 0;
            originalAmountTotal = 0;
            portfolioType = ddlGroupPortfolioGroup.SelectedValue.ToString();
            viewType = ddlViewType.SelectedValue.ToString();
            if (gvSystematicTransactions.HeaderRow != null)
            {
                customerNameSearch = ((TextBox)gvSystematicTransactions.HeaderRow.FindControl("txtCustomerSearch")).Text;
                schemeNameSearch = ((TextBox)gvSystematicTransactions.HeaderRow.FindControl("txtSchemeSearch")).Text;
                transType = ((DropDownList)gvSystematicTransactions.HeaderRow.FindControl("ddlTranType")).SelectedValue.ToString();
                if (transType == "Select")
                    transType = "";
            }

            //if ((TextBox)gvSystematicTransactions.HeaderRow.FindControl("txtSchemeSearch") != null)
            //{
                
            //}

            //if ((DropDownList)gvSystematicTransactions.HeaderRow.FindControl("ddlTranType") != null)
            //{
                
            //}

            if (rbtnPickDate.Checked)
            {
                fromDate = DateTime.Parse(txtFromDate.Text.Trim());
                toDate = DateTime.Parse(txtToDate.Text.Trim());
            }
            else
            {
                dateBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue, out fromDate,out toDate);
            }

            int adviserId = ((AdvisorVo)Session["advisorVo"]).advisorId;
            mfSystematicTransactionReportVoList = customerTransactionBo.GetMFSystematicTransactionsReport(adviserId, fromDate, toDate,customerNameSearch,schemeNameSearch,transType,portfolioType,out transactionTypeList);
            ViewState["SystematicTransactionVoList"] = mfSystematicTransactionReportVoList;
            DataTable dtSystematicTransactionReport = new DataTable();
            DataRow drSystematicTransactionReport;

            dtSystematicTransactionReport.Columns.Add("CustomerName");
            dtSystematicTransactionReport.Columns.Add("Folio");
            dtSystematicTransactionReport.Columns.Add("Scheme");
            dtSystematicTransactionReport.Columns.Add("SystematicType");
            dtSystematicTransactionReport.Columns.Add("SystematicAmount");
            dtSystematicTransactionReport.Columns.Add("SystematicDate");
            dtSystematicTransactionReport.Columns.Add("ActualAmount");
            dtSystematicTransactionReport.Columns.Add("ActualDate");
            dtSystematicTransactionReport.Columns.Add("RowId");

            if (mfSystematicTransactionReportVoList != null && mfSystematicTransactionReportVoList.Count != 0)
            {
                for (int i = 0; i < mfSystematicTransactionReportVoList.Count; i++)
                {
                    if (viewType == "ALL")
                    {
                        drSystematicTransactionReport = dtSystematicTransactionReport.NewRow();
                        drSystematicTransactionReport[0] = mfSystematicTransactionReportVoList[i].CustomerName;
                        drSystematicTransactionReport[1] = mfSystematicTransactionReportVoList[i].FolioNum;
                        drSystematicTransactionReport[2] = mfSystematicTransactionReportVoList[i].SchemePlanName;
                        if (mfSystematicTransactionReportVoList[i].SystematicTransacionType != null && mfSystematicTransactionReportVoList[i].SystematicTransacionType != "")
                            drSystematicTransactionReport[3] = mfSystematicTransactionReportVoList[i].SystematicTransacionType;
                        else
                            drSystematicTransactionReport[3] = mfSystematicTransactionReportVoList[i].OriginalTransactionType;
                        if (mfSystematicTransactionReportVoList[i].SystematicAmount != 0)
                            drSystematicTransactionReport[4] = mfSystematicTransactionReportVoList[i].SystematicAmount.ToString("f2");
                        else
                            drSystematicTransactionReport[4] = "-";
                        if (mfSystematicTransactionReportVoList[i].SystematicTransactionDate != null && mfSystematicTransactionReportVoList[i].SystematicTransactionDate != DateTime.MinValue)
                            drSystematicTransactionReport[5] = mfSystematicTransactionReportVoList[i].SystematicTransactionDate.ToShortDateString();
                        else
                            drSystematicTransactionReport[5] = "-";
                        if (mfSystematicTransactionReportVoList[i].OriginalTransactionAmount != 0)
                            drSystematicTransactionReport[6] = mfSystematicTransactionReportVoList[i].OriginalTransactionAmount.ToString("f2");
                        else
                            drSystematicTransactionReport[6] = "-";
                        if (mfSystematicTransactionReportVoList[i].OriginalTransactionDate != null && mfSystematicTransactionReportVoList[i].OriginalTransactionDate != DateTime.MinValue)
                            drSystematicTransactionReport[7] = mfSystematicTransactionReportVoList[i].OriginalTransactionDate.ToShortDateString();
                        else
                            drSystematicTransactionReport[7] = "-";
                        drSystematicTransactionReport[8] = i;
                        dtSystematicTransactionReport.Rows.Add(drSystematicTransactionReport);
                        systematicTotalAmount = systematicTotalAmount + mfSystematicTransactionReportVoList[i].SystematicAmount;
                        originalAmountTotal = originalAmountTotal + mfSystematicTransactionReportVoList[i].OriginalTransactionAmount;
                    }
                    else if (viewType == "NAT" && (mfSystematicTransactionReportVoList[i].OriginalTransactionDate == DateTime.MinValue
                        || mfSystematicTransactionReportVoList[i].OriginalTransactionDate == null)
                        && (mfSystematicTransactionReportVoList[i].OriginalTransactionType == ""||mfSystematicTransactionReportVoList[i].OriginalTransactionType ==null))
                    {
                        drSystematicTransactionReport = dtSystematicTransactionReport.NewRow();
                        drSystematicTransactionReport[0] = mfSystematicTransactionReportVoList[i].CustomerName;
                        drSystematicTransactionReport[1] = mfSystematicTransactionReportVoList[i].FolioNum;
                        drSystematicTransactionReport[2] = mfSystematicTransactionReportVoList[i].SchemePlanName;
                        if (mfSystematicTransactionReportVoList[i].SystematicTransacionType != null && mfSystematicTransactionReportVoList[i].SystematicTransacionType != "")
                            drSystematicTransactionReport[3] = mfSystematicTransactionReportVoList[i].SystematicTransacionType;
                        else
                            drSystematicTransactionReport[3] = mfSystematicTransactionReportVoList[i].OriginalTransactionType;
                        if (mfSystematicTransactionReportVoList[i].SystematicAmount != 0)
                            drSystematicTransactionReport[4] = mfSystematicTransactionReportVoList[i].SystematicAmount.ToString("f2");
                        else
                            drSystematicTransactionReport[4] = "-";
                        if (mfSystematicTransactionReportVoList[i].SystematicTransactionDate != null && mfSystematicTransactionReportVoList[i].SystematicTransactionDate != DateTime.MinValue)
                            drSystematicTransactionReport[5] = mfSystematicTransactionReportVoList[i].SystematicTransactionDate.ToShortDateString();
                        else
                            drSystematicTransactionReport[5] = "-";
                        if (mfSystematicTransactionReportVoList[i].OriginalTransactionAmount != 0)
                            drSystematicTransactionReport[6] = mfSystematicTransactionReportVoList[i].OriginalTransactionAmount.ToString("f2");
                        else
                            drSystematicTransactionReport[6] = "-";
                        if (mfSystematicTransactionReportVoList[i].OriginalTransactionDate != null && mfSystematicTransactionReportVoList[i].OriginalTransactionDate != DateTime.MinValue)
                            drSystematicTransactionReport[7] = mfSystematicTransactionReportVoList[i].OriginalTransactionDate.ToShortDateString();
                        else
                            drSystematicTransactionReport[7] = "-";
                        drSystematicTransactionReport[8] = i;
                        dtSystematicTransactionReport.Rows.Add(drSystematicTransactionReport);
                        systematicTotalAmount = systematicTotalAmount + mfSystematicTransactionReportVoList[i].SystematicAmount;
                        originalAmountTotal = originalAmountTotal + mfSystematicTransactionReportVoList[i].OriginalTransactionAmount;

                    }
                    else if (viewType == "NST" && (mfSystematicTransactionReportVoList[i].SystematicTransacionType == ""||mfSystematicTransactionReportVoList[i].SystematicTransacionType ==null)
                        && mfSystematicTransactionReportVoList[i].SystematicAmount == 0)
                    {
                        drSystematicTransactionReport = dtSystematicTransactionReport.NewRow();
                        drSystematicTransactionReport[0] = mfSystematicTransactionReportVoList[i].CustomerName;
                        drSystematicTransactionReport[1] = mfSystematicTransactionReportVoList[i].FolioNum;
                        drSystematicTransactionReport[2] = mfSystematicTransactionReportVoList[i].SchemePlanName;
                        if (mfSystematicTransactionReportVoList[i].SystematicTransacionType != null && mfSystematicTransactionReportVoList[i].SystematicTransacionType != "")
                            drSystematicTransactionReport[3] = mfSystematicTransactionReportVoList[i].SystematicTransacionType;
                        else
                            drSystematicTransactionReport[3] = mfSystematicTransactionReportVoList[i].OriginalTransactionType;
                        if (mfSystematicTransactionReportVoList[i].SystematicAmount != 0)
                            drSystematicTransactionReport[4] = mfSystematicTransactionReportVoList[i].SystematicAmount.ToString("f2");
                        else
                            drSystematicTransactionReport[4] = "-";
                        if (mfSystematicTransactionReportVoList[i].SystematicTransactionDate != null && mfSystematicTransactionReportVoList[i].SystematicTransactionDate != DateTime.MinValue)
                            drSystematicTransactionReport[5] = mfSystematicTransactionReportVoList[i].SystematicTransactionDate.ToShortDateString();
                        else
                            drSystematicTransactionReport[5] = "-";
                        if (mfSystematicTransactionReportVoList[i].OriginalTransactionAmount != 0)
                            drSystematicTransactionReport[6] = mfSystematicTransactionReportVoList[i].OriginalTransactionAmount.ToString("f2");
                        else
                            drSystematicTransactionReport[6] = "-";
                        if (mfSystematicTransactionReportVoList[i].OriginalTransactionDate != null && mfSystematicTransactionReportVoList[i].OriginalTransactionDate != DateTime.MinValue)
                            drSystematicTransactionReport[7] = mfSystematicTransactionReportVoList[i].OriginalTransactionDate.ToShortDateString();
                        else
                            drSystematicTransactionReport[7] = "-";
                        drSystematicTransactionReport[8] = i;
                        dtSystematicTransactionReport.Rows.Add(drSystematicTransactionReport);
                        systematicTotalAmount = systematicTotalAmount + mfSystematicTransactionReportVoList[i].SystematicAmount;
                        originalAmountTotal = originalAmountTotal + mfSystematicTransactionReportVoList[i].OriginalTransactionAmount;

                    }

                }
                if (dtSystematicTransactionReport.Rows.Count != 0)
                {
                    gvSystematicTransactions.DataSource = dtSystematicTransactionReport;
                    gvSystematicTransactions.DataBind();
                    
                    gvSystematicTransactions.Visible = true;
                    pnlSystematicTransactions.Visible = true;
                    BindGridSearchBoxes(transactionTypeList, transType, customerNameSearch, schemeNameSearch);
                }
                else
                {
                     drSystematicTransactionReport = dtSystematicTransactionReport.NewRow();
                     drSystematicTransactionReport[0]="";
                     drSystematicTransactionReport[1]="";
                     drSystematicTransactionReport[2]="";
                     drSystematicTransactionReport[3]="";
                     drSystematicTransactionReport[4]="";
                     drSystematicTransactionReport[5]="";
                     drSystematicTransactionReport[6] = "";
                     drSystematicTransactionReport[7] = "";
                     drSystematicTransactionReport[8] = "";
                     dtSystematicTransactionReport.Rows.Add(drSystematicTransactionReport);

                     gvSystematicTransactions.DataSource = dtSystematicTransactionReport;
                     gvSystematicTransactions.DataBind();
                     gvSystematicTransactions.Visible = true;
                     pnlSystematicTransactions.Visible = true;
                     BindGridSearchBoxes(transactionTypeList, transType, customerNameSearch, schemeNameSearch);
                    trErrorMessage.Visible = true;
                }
            }
            else
            {
                drSystematicTransactionReport = dtSystematicTransactionReport.NewRow();
                drSystematicTransactionReport[0] = "";
                drSystematicTransactionReport[1] = "";
                drSystematicTransactionReport[2] = "";
                drSystematicTransactionReport[3] = "";
                drSystematicTransactionReport[4] = "";
                drSystematicTransactionReport[5] = "";
                drSystematicTransactionReport[6] = "";
                drSystematicTransactionReport[7] = "";
                dtSystematicTransactionReport.Rows.Add(drSystematicTransactionReport);

                gvSystematicTransactions.DataSource = dtSystematicTransactionReport;
                gvSystematicTransactions.DataBind();
                gvSystematicTransactions.Visible = true;
                pnlSystematicTransactions.Visible = true;
                BindGridSearchBoxes(transactionTypeList, transType, customerNameSearch, schemeNameSearch);
                trErrorMessage.Visible = true;
            }
        }
        private void BindGridSearchBoxes(List<string> transTypeList, string transType, string customerSearch, string schemeSearch)
        {
            if (transTypeList != null)
            {
                for(int i =0;i<transTypeList.Count;i++)
                {
                    ((DropDownList)gvSystematicTransactions.HeaderRow.FindControl("ddlTranType")).Items.Add(new ListItem(transTypeList[i], transTypeList[i]));
                }
            }
            ((DropDownList)gvSystematicTransactions.HeaderRow.FindControl("ddlTranType")).Items.Add(new ListItem("Select", "Select"));
            if (transType != "")
                ((DropDownList)gvSystematicTransactions.HeaderRow.FindControl("ddlTranType")).SelectedValue = transType;
            else
                ((DropDownList)gvSystematicTransactions.HeaderRow.FindControl("ddlTranType")).SelectedValue = "Select";

            if ((TextBox)gvSystematicTransactions.HeaderRow.FindControl("txtCustomerSearch") != null)
            {
               ((TextBox)gvSystematicTransactions.HeaderRow.FindControl("txtCustomerSearch")).Text=customerSearch;
            }

            if ((TextBox)gvSystematicTransactions.HeaderRow.FindControl("txtSchemeSearch") != null)
            {
                ((TextBox)gvSystematicTransactions.HeaderRow.FindControl("txtSchemeSearch")).Text = schemeSearch;
            }

        }
        protected void rbtnPickDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPickDate.Checked)
            {
                trRange.Visible = true;
                trPeriod.Visible = false;
            }
            else
            {
                trRange.Visible = false;
                trPeriod.Visible = true;
            }
        }
        protected void chkSystematicTransaction_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvSystematicTransactions.Rows)
            {
                if (((CheckBox)row.FindControl("chkSystematicTransaction")) != (CheckBox)sender)
                {
                    ((CheckBox)row.FindControl("chkSystematicTransaction")).Checked = false;
                }
                else
                {
                    if(((CheckBox)row.FindControl("chkSystematicTransaction")).Checked)
                    {
                        ((CheckBox)row.FindControl("chkSystematicTransaction")).Checked = true;
                        btnRegister.Visible = true;
                    }
                    else
                    {
                        ((CheckBox)row.FindControl("chkSystematicTransaction")).Checked = false;
                        btnRegister.Visible = false;
                    }
                }
            }
        }
        protected void rbtnPickPeriod_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPickPeriod.Checked)
            {
                trRange.Visible = false;
                trPeriod.Visible = true;
                
            }
            else
            {
                trRange.Visible = true;
                trPeriod.Visible = false;
            }
        }

        protected void ddlViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnPickPeriod.Checked && ddlPeriod.SelectedValue != "Select a Period")
                SetSystematicTransactions();
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Select a Period');", true);

        }

        protected void btnSysTransactionSearch_Click(object sender, EventArgs e)
        {
            SetSystematicTransactions();
        }

        protected void ddlGroupPortfolioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSystematicTransactions();
        }
        protected void ddlTranType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSystematicTransactions();
        }

        protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSystematicTransactions();
        }
        protected void gvSystematicTransactions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total ";
                e.Row.Cells[5].Text = decimal.Parse(systematicTotalAmount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[7].Text = decimal.Parse(originalAmountTotal.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text == "-" && e.Row.Cells[6].Text == "-")
                {
                    ((CheckBox)e.Row.FindControl("chkSystematicTransaction")).Visible = true;
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            int rowId=0;
            bool isChecked = false;
            SystematicSetupVo systematicSetupVo = new SystematicSetupVo();
            CustomerBo customerBo = new CustomerBo();
            if (ViewState["SystematicTransactionVoList"] != null)
            {
                mfSystematicTransactionReportVoList = (List<MFSystematicTransactionReportVo>)ViewState["SystematicTransactionVoList"];
            }
            foreach (GridViewRow row in gvSystematicTransactions.Rows)
            {
                if (((CheckBox)row.FindControl("chkSystematicTransaction")).Checked)
                {
                    rowId = int.Parse(gvSystematicTransactions.DataKeys[row.RowIndex].Value.ToString());
                    isChecked = true;
                    break;
                }
            }
            if (isChecked)
            {
                systematicSetupVo = new SystematicSetupVo();
                systematicSetupVo.AccountId = mfSystematicTransactionReportVoList[rowId].AccountId;
                systematicSetupVo.Amount = mfSystematicTransactionReportVoList[rowId].OriginalTransactionAmount;
                systematicSetupVo.Folio = mfSystematicTransactionReportVoList[rowId].FolioNum;
                systematicSetupVo.SchemePlanCode = mfSystematicTransactionReportVoList[rowId].SchemePlanCode;
                systematicSetupVo.SchemePlan = mfSystematicTransactionReportVoList[rowId].SchemePlanName;
                systematicSetupVo.Portfolio = mfSystematicTransactionReportVoList[rowId].PortfolioId.ToString();
                systematicSetupVo.SystematicTypeCode = mfSystematicTransactionReportVoList[rowId].OriginalTransactionType;
                Session["customerVo"] = customerBo.GetCustomer(mfSystematicTransactionReportVoList[rowId].CustomerId);
                Session["systematicSetupVo"] = systematicSetupVo;
                Session["SourcePage"] = "ReconReport";
                Session[SessionContents.PortfolioId] = mfSystematicTransactionReportVoList[rowId].PortfolioId;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioSystematicEntry','action=entry');", true);

            }
            else
            {
            }
        }

        protected void btnGen_Click(object sender, EventArgs e)
        {
            SetSystematicTransactions();
        }
    }
}