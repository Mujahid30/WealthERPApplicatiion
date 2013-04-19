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
using Telerik.Web.UI;

namespace WealthERP.CustomerPortfolio
{
    public partial class CustomerMFSystematicTransactionReport : System.Web.UI.UserControl
    {
        DropDownList ddlTransactionType;
        TextBox txtCustomerName;
        TextBox txtSchemeName;

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
            if (gvSystematicTransactions.Items != null)
            {                
                transType = hdnDdlTranTypeSelectedValue.Value;

                if (transType == "Select")
                    transType = "";
            }

            if (rbtnPickDate.Checked)
            {
                fromDate = DateTime.Parse(txtFromDate.Text.Trim());
                toDate = DateTime.Parse(txtToDate.Text.Trim());
            }
            else
            {
                dateBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue, out fromDate, out toDate);
            }

            int adviserId = ((AdvisorVo)Session["advisorVo"]).advisorId;
            mfSystematicTransactionReportVoList = customerTransactionBo.GetMFSystematicTransactionsReport(adviserId, fromDate, toDate, customerNameSearch, schemeNameSearch, transType, portfolioType, out transactionTypeList);
            ViewState["SystematicTransactionVoList"] = mfSystematicTransactionReportVoList;
            ViewState["trntypelist"] = transactionTypeList;

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
                imgexportButton.Visible = true;
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
                        && (mfSystematicTransactionReportVoList[i].OriginalTransactionType == "" || mfSystematicTransactionReportVoList[i].OriginalTransactionType == null))
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
                    else if (viewType == "NST" && (mfSystematicTransactionReportVoList[i].SystematicTransacionType == "" || mfSystematicTransactionReportVoList[i].SystematicTransacionType == null)
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
                    ViewState["SystematicTransactions"] = dtSystematicTransactionReport;
                    gvSystematicTransactions.CurrentPageIndex = 0;
                    gvSystematicTransactions.DataSource = dtSystematicTransactionReport;
                    gvSystematicTransactions.DataBind();

                   // ViewState["SystematicTransactions"] = dtSystematicTransactionReport;

                    gvSystematicTransactions.Visible = true;
                    pnlSystematicTransactions.Visible = true;
                    //BindGridSearchBoxes(transactionTypeList, transType, customerNameSearch, schemeNameSearch);
                }
                else
                {
                    gvSystematicTransactions.DataSource = dtSystematicTransactionReport;
                    gvSystematicTransactions.DataBind();
                    gvSystematicTransactions.Visible = true;
                    pnlSystematicTransactions.Visible = true;
                    trErrorMessage.Visible = false;
                }
            }
            else
            {
                imgexportButton.Visible = false;
                gvSystematicTransactions.DataSource = dtSystematicTransactionReport;
                //gvSystematicTransactions.DataBind();               
                gvSystematicTransactions.Visible = true;
                pnlSystematicTransactions.Visible = true;
                trErrorMessage.Visible = false;
            }
        }

        protected void gvSystematicTransactions_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                CheckBox checkBox = (CheckBox)e.Item.FindControl("chkSystematicTransaction");
            }
        }

        protected void gvSystematicTransactions_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
            if (e.Item is GridFilteringItem && e.Item.ItemIndex == -1)
            {                                       
                GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                RadComboBox ddlTranType = (RadComboBox)filterItem.FindControl("ddlTranType");
                DataTable dt = new DataTable();
                if (ViewState["SystematicTransactions"] != null)
                dt = (DataTable)ViewState["SystematicTransactions"];
                if (dt != null )
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtsystmatic = new DataTable();
                        DataRow drsystematic;
                        dtsystmatic.Columns.Add("SystematicType");
                        foreach (DataRow dr in dt.Rows)
                        {
                            drsystematic = dtsystmatic.NewRow();
                            drsystematic["SystematicType"] = dr["SystematicType"].ToString();
                            dtsystmatic.Rows.Add(drsystematic);
                        }
                        DataView view = new DataView(dt);
                        DataTable distinctValues = view.ToTable(true, "SystematicType");
                        ddlTranType.DataSource = distinctValues;
                        ddlTranType.DataValueField = dtsystmatic.Columns["SystematicType"].ToString();
                        ddlTranType.DataTextField = dtsystmatic.Columns["SystematicType"].ToString();
                        ddlTranType.DataBind();
                    }
                }
           // if (e.Item is GridHeaderItem)
            //{
              //  GridHeaderItem item = e.Item as GridHeaderItem;
               // ddlTransactionType = (RadComboBox)item.FindControl("ddlTranType");
                //txtCustomerName = (TextBox)item.FindControl("txtCustomerSearch");
                //txtSchemeName = (TextBox)item.FindControl("txtSchemeSearch");

               // BindTranTypeDDL(ddlTransactionType);
            }
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                CheckBox checkBox = (CheckBox)e.Item.FindControl("chkSystematicTransaction");
                if (e.Item.Cells[7].Text == "-" && e.Item.Cells[8].Text == "-")
                {
                    checkBox.Visible = true;
                }
            }
        }
        protected void gvSystematicTransactions_PreRender(object sender, EventArgs e)
        {
            if (gvSystematicTransactions.MasterTableView.FilterExpression != string.Empty)
            {
                RefreshCombos();
            }
        }

        protected void RefreshCombos()
        {
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["SystematicTransactions"];
            DataView view = new DataView(dt);
            DataTable distinctValues = view.ToTable();
            DataRow[] rows = distinctValues.Select(gvSystematicTransactions.MasterTableView.FilterExpression.ToString());
            gvSystematicTransactions.MasterTableView.Rebind();
        }
        protected void gvSystematicTransactions_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            DataView dvTType = new DataView();
            string ttype = string.Empty;
           dt = (DataTable)ViewState["SystematicTransactions"];
                if (dt != null)
                {  
                  if (ViewState["SystematicType"] != null)
                  ttype = ViewState["SystematicType"].ToString();
                     if ((!string.IsNullOrEmpty(ttype)))
               {
                   dvTType = new DataView(dt, "SystematicType = '" + ttype + "'", "CustomerName,Scheme,SystematicDate", DataViewRowState.CurrentRows);
                   gvSystematicTransactions.DataSource = dvTType.ToTable();
               
               }
                else
                gvSystematicTransactions.DataSource = dt;
            }
           
        }

        private void BindGridSearchBoxes(List<string> transTypeList, string transType, string customerSearch, string schemeSearch)
        {

            //if (transTypeList != null)
            //{
            //    for (int i = 0; i < transTypeList.Count; i++)
            //    {
            //        ddlTransactionType.Items.Add(new ListItem(transTypeList[i], transTypeList[i]));
            //    }
            //}

            //ddlTransactionType.Items.Add(new ListItem("Select", "Select"));

            //if (transType != "")
            //    ddlTransactionType.SelectedValue = transType;
            //else
            //    ddlTransactionType.SelectedValue = "Select";

            //if (txtCustomerName != null)
            //{
            //    txtCustomerName.Text = customerSearch;
            //}

            //if (txtSchemeName != null)
            //{
            //    txtSchemeName.Text = schemeSearch;
            //}

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
            foreach (GridDataItem ItemGrid in gvSystematicTransactions.MasterTableView.Items)
            {
                if (((CheckBox)ItemGrid.FindControl("chkSystematicTransaction")) != (CheckBox)sender)
                {
                    ((CheckBox)ItemGrid.FindControl("chkSystematicTransaction")).Checked = false;
                }
                else
                {
                    if (((CheckBox)ItemGrid.FindControl("chkSystematicTransaction")).Checked)
                    {
                        ((CheckBox)ItemGrid.FindControl("chkSystematicTransaction")).Checked = true;
                        btnRegister.Visible = true;
                    }
                    else
                    {
                        ((CheckBox)ItemGrid.FindControl("chkSystematicTransaction")).Checked = false;
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
            //else
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Select a Period');", true);

        }

        protected void btnSysTransactionSearch_Click(object sender, EventArgs e)
        {
            SetSystematicTransactions();
        }

        protected void ddlGroupPortfolioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSystematicTransactions();
        }

        protected void ddlTranType_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //string strTransactionType = string.Empty;
            RadComboBox dropdown = sender as RadComboBox;
            ViewState["SystematicType"] = dropdown.SelectedValue;
            if (ViewState["SystematicType"] != "")
            {
                GridColumn column = gvSystematicTransactions.MasterTableView.GetColumnSafe("SystematicType");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvSystematicTransactions.CurrentPageIndex = 0;
                gvSystematicTransactions.MasterTableView.Rebind();
            }
            else
            {
                GridColumn column = gvSystematicTransactions.MasterTableView.GetColumnSafe("SystematicType");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvSystematicTransactions.CurrentPageIndex = 0;
                gvSystematicTransactions.MasterTableView.Rebind();

            } 
            //GridHeaderItem headerItem = gvSystematicTransactions.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
            //DropDownList ddl = headerItem.FindControl("ddlTranType") as DropDownList;
            //if (ddl != null)
            //{
            //    hdnDdlTranTypeSelectedValue.Value = ddl.SelectedValue;
            //}            
            SetSystematicTransactions();
            hdnDdlTranTypeSelectedValue.Value = string.Empty;
        }

        protected void TranType_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            if (ViewState["SystematicType"] != null)
            {
                Combo.SelectedValue = ViewState["SystematicType"].ToString();
            }
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
            int rowId = 0;
            bool isChecked = false;
            SystematicSetupVo systematicSetupVo = new SystematicSetupVo();
            CustomerBo customerBo = new CustomerBo();
            if (ViewState["SystematicTransactionVoList"] != null)
            {
                mfSystematicTransactionReportVoList = (List<MFSystematicTransactionReportVo>)ViewState["SystematicTransactionVoList"];
            }
            foreach (GridDataItem item in gvSystematicTransactions.MasterTableView.Items)
            {
                if (((CheckBox)item.FindControl("chkSystematicTransaction")).Checked)
                {
                    rowId = int.Parse(item.GetDataKeyValue("RowId").ToString());
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
            ViewState.Remove("SystematicType");   
            SetSystematicTransactions();
        }

        private void BindTranTypeDDL(DropDownList ddl)
        {
            List<string> transTypeList = (List<string>)ViewState["trntypelist"];

            ddl.Items.Add(new ListItem("Select", "Select"));
            if (transTypeList != null)
            {
                for (int i = 0; i < transTypeList.Count; i++)
                {
                    ddl.Items.Add(new ListItem(transTypeList[i], transTypeList[i]));
                }
            }
            if (hdnDdlTranTypeSelectedValue.Value != "")
                ddl.SelectedValue = hdnDdlTranTypeSelectedValue.Value;
            else
                ddl.SelectedValue = "Select";
        }

        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvSystematicTransactions.ExportSettings.OpenInNewWindow = true;
            gvSystematicTransactions.ExportSettings.IgnorePaging = true;
            gvSystematicTransactions.ExportSettings.HideStructureColumns = true;
            gvSystematicTransactions.ExportSettings.ExportOnlyData = true;
            gvSystematicTransactions.ExportSettings.FileName = "CustomerMFSystematicReport";
            gvSystematicTransactions.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvSystematicTransactions.MasterTableView.ExportToExcel();

        }
    }
}