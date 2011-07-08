using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VoUser;
using BoFPSuperlite;
using BoCustomerProfiling;
using Telerik.Web.UI;


namespace WealthERP.FP
{
    public partial class CustomerFPProjections : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        int customerAge = 0;
        CustomerFPAnalyticsBo customerFPAnalyticsBo = new CustomerFPAnalyticsBo();
        DataSet dsFPAnalyticsEngine;
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo adviserVo = new AdvisorVo();
        DataTable dtFPProjectionAssetAllocation = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
         
           {
            rdbYearWise.Attributes.Add("onClick", "javascript:ShowHideGaolType(value);");
            rdbYearRangeWise.Attributes.Add("onClick", "javascript:ShowHideGaolType(value);");
            rbtnFSPickYear.Attributes.Add("onClick", "javascript:ShowHideGaolTypeFS(value);");
            rbtnFSRangeYear.Attributes.Add("onClick", "javascript:ShowHideGaolTypeFS(value);");
            customerVo = (CustomerVo)Session["customerVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            ddlFromYear.SelectedIndex = 0;
            ddlToYear.SelectedIndex = 0;
            ddlRangeYearFSFROM.SelectedIndex = 0;
            ddlRangeYearFSTO.SelectedIndex = 0;

            msgRecordStatus.Visible = false;
            if (!Page.IsPostBack)
            {
                Session["PickAssetClassDPSelected"] = null;
                dsFPAnalyticsEngine = customerFPAnalyticsBo.FutureSurplusEngine(customerVo.CustomerId);
                if (dsFPAnalyticsEngine.Tables.Count > 0)
                {
                    BindYearDropDowns();
                    BindDropdownsRebalancing();
                }
                else
                {
                    SetEditViewMode(true);
                    btnEdit.Enabled = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Set Up Finance Profile and Assumption ');", true);
     
                }
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "pageloadscript", @"ShowHideGaolType();", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "pageloadscript", @"ShowHideGaolTypeFS();", true);

            }
              BindCustomerProjectedAssumption();
                BindFPFutureSavingGrid();

                if (Session["PickAssetClassDPSelected"] != null)
                {
                    CallRebalancing();
                }
                if (ViewState["ActionEditViewMode"] == null)
                {
                    ViewState["ActionEditViewMode"] = "View";
                }

                if (ViewState["ActionEditViewMode"].ToString() == "View")
                {
                    SetEditViewMode(true);
                }
                else if (ViewState["ActionEditViewMode"].ToString() == "Edit")
                {
                    SetEditViewMode(false);
                }
            }
          
    
                     
        

        private void BindYearDropDowns()
        {
            int lifeExpentancy = 0;
            lifeExpentancy = customerBo.ExpiryAgeOfAdviser(adviserVo.advisorId, customerVo.CustomerId);

            if (customerVo.Dob != DateTime.MinValue)
                customerAge = DateTime.Now.Year - customerVo.Dob.Year;

            int customerLife = lifeExpentancy - customerAge;
            int lifeLastYear = DateTime.Now.Year + customerLife;

            int currentYear = DateTime.Now.Year;

            for (; currentYear <= lifeLastYear; currentYear++)
            {
                ddlPickYear.Items.Add(currentYear.ToString());
                ddlFromYear.Items.Add(currentYear.ToString());
                ddlToYear.Items.Add(currentYear.ToString());

                ddlPickYearFS.Items.Add(currentYear.ToString());
                ddlRangeYearFSFROM.Items.Add(currentYear.ToString());
                ddlRangeYearFSTO.Items.Add(currentYear.ToString());


            }
            //ddlPickYear.Items.Insert(0, new ListItem("Select", "Select"));
            //ddlFromYear.Items.Insert(0, new ListItem("Select", "Select"));
            //ddlToYear.Items.Insert(0, new ListItem("Select", "Select"));
        }
        private void BindDropdownsRebalancing()
        {
            DataTable dtBindDropdownsRebalancing = new DataTable();
            dtBindDropdownsRebalancing = customerFPAnalyticsBo.BindDropdownsRebalancing(adviserVo.advisorId);
            ddlRebalancing.DataSource = dtBindDropdownsRebalancing;
            ddlRebalancing.DataTextField = dtBindDropdownsRebalancing.Columns["WAC_AssetClassification"].ToString();
            ddlRebalancing.DataValueField = dtBindDropdownsRebalancing.Columns["WAC_AssetClassificationCode"].ToString();
            ddlRebalancing.DataBind();
            ddlRebalancing.Items.Insert(0,new ListItem("-Select-", "-Select-"));

        }
        protected void ddlPickYearFS_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindFPFutureSavingGrid();
        
        }


        protected void aplToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Value == "Edit")
            {
                ViewState["ActionEditViewMode"] = "Edit";
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerAssumptionsPreferencesSetup','login');", true);
                SetEditViewMode(false);
            }
        }
        public void SetEditViewMode(bool Bool)
        {
            if (Bool)
            {
                rdbYearWise.Enabled = false;
                rdbYearRangeWise.Enabled = false;
                rbtnFSRangeYear.Enabled = false;
                rbtnFSPickYear.Enabled = false;
                txtAlternate.Enabled = false;
                txtAlternateFS.Enabled = false;
                txtCash.Enabled = false;
                txtCashFS.Enabled = false;
                txtDebt.Enabled = false;
                txtDebtFS.Enabled = false;
                txtEquityFS.Enabled = false;
                txtEquity.Enabled = false;
               
                btnSubmitAggredAllocation.Enabled = false;
                btnSubmitFpFs.Enabled = false;

            }
            else
            {
                rdbYearWise.Enabled = true;
                rdbYearRangeWise.Enabled = true;
                rbtnFSRangeYear.Enabled = true;
                rbtnFSPickYear.Enabled = true;
                txtAlternate.Enabled = true;
                txtAlternateFS.Enabled = true;
                txtCash.Enabled = true;
                txtCashFS.Enabled = true;
                txtDebt.Enabled = true;
                txtDebtFS.Enabled = true;
                txtEquityFS.Enabled = true;
                txtEquity.Enabled = true;
             
                btnSubmitAggredAllocation.Enabled = true;
                btnSubmitFpFs.Enabled = true;
            }



        }

        private void BindFPFutureSavingGrid()
        {
            DataSet dsFPFutureSavingGrid = new DataSet();
            DataTable dtFPFutureSavingGrid = new DataTable();
            dsFPFutureSavingGrid = customerFPAnalyticsBo.FutureSurplusEngine(customerVo.CustomerId);
            if (dsFPFutureSavingGrid.Tables.Count > 0)
            {
                dtFPFutureSavingGrid = CreateFutureSavingProjection(dsFPFutureSavingGrid.Tables[0]);
                gdvwFutureSavings.DataSource = dtFPFutureSavingGrid;
                gdvwFutureSavings.DataBind();
                SetDefaultFutureSaving(dtFPFutureSavingGrid);
            }
            else
            {
                SetEditViewMode(true);
                btnEdit.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Set Up Finance Profile and Assumption ');", true);
     
            }

        }
        public void SetDefaultFutureSaving(DataTable dtSetDefaultFutureSaving)
        {
            DataRow[] drSetDefaultFutureSaving;
            int year = int.Parse(ddlPickYearFS.SelectedItem.ToString());
            drSetDefaultFutureSaving = dtSetDefaultFutureSaving.Select("Year=" + year.ToString());
            txtEquityFS.Text = Math.Round(decimal.Parse(drSetDefaultFutureSaving[0]["Equity_Allocation_per"].ToString()),2).ToString();
            txtDebtFS.Text = Math.Round(decimal.Parse(drSetDefaultFutureSaving[0]["Debt_Allocation_per"].ToString()),2).ToString();
            txtCashFS.Text = Math.Round(decimal.Parse(drSetDefaultFutureSaving[0]["Cash_Allocation_per"].ToString()),2).ToString();
            txtAlternateFS.Text = Math.Round(decimal.Parse(drSetDefaultFutureSaving[0]["Alternate_Allocation_per"].ToString()),2).ToString();

        }
        protected void ddlPickYear_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindCustomerProjectedAssumption();
        }
      protected void ddlRebalancing_OnSelectIndexChanged(object sender, EventArgs e)
      {
          Session["PickAssetClassDPSelected"] = ddlRebalancing.SelectedItem.ToString();
          if(ddlRebalancing.SelectedItem.ToString() != "-Select-")
            CallRebalancing();
      }
      public void SetYearWiseDetailsInAllAssetAllocation(DataTable dtassetAllocation)
      {
          DataRow[] drSetDefaultAssetAllocation;
          int year = int.Parse(ddlPickYear.SelectedItem.ToString());
          drSetDefaultAssetAllocation = dtassetAllocation.Select("Year=" + year.ToString());
          if (drSetDefaultAssetAllocation.Length > 0)
          {
              txtEquity.Text = Math.Round(decimal.Parse(drSetDefaultAssetAllocation[0]["Agr_Equity"].ToString()), 2).ToString();
              txtDebt.Text = Math.Round(decimal.Parse(drSetDefaultAssetAllocation[0]["Agr_Debt"].ToString()), 2).ToString();
              txtCash.Text = Math.Round(decimal.Parse(drSetDefaultAssetAllocation[0]["Agr_Cash"].ToString()), 2).ToString();

              if (drSetDefaultAssetAllocation[0]["Agr_Alternate"].ToString() == "")
              {

              }
              else
                  txtAlternate.Text = Math.Round(decimal.Parse(drSetDefaultAssetAllocation[0]["Agr_Alternate"].ToString()), 2).ToString();
          }
      }
      //protected void gvAssetAllocation_OnRowCreated(object sender, gri e)
      //{
      //    if (e.Row.RowType == DataControlRowType.Header)
      //    {
      //        //Build custom header.
      //        GridView oGridView = (GridView)sender;
      //        GridViewRow oGridViewRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
      //        TableCell oTableCell = new TableCell();

      //        //Add Department
      //        oTableCell.Text = "Recommended Allocation";
      //        oTableCell.ColumnSpan = 4;
      //        oGridViewRow.Cells.Add(oTableCell);

      //        //Add Employee
      //        oTableCell = new TableCell();
      //        oTableCell.Text = "Agreed Allocation";
      //        oTableCell.ColumnSpan = 4;
      //        oGridViewRow.Cells.Add(oTableCell);

      //        oGridView.Controls[0].Controls.AddAt(0, oGridViewRow);
      //    }
      //}

      protected void CallRebalancing()
      {
          string assetClass = ddlRebalancing.SelectedItem.ToString();
          DataSet dsRebalancing = new DataSet();
          DataTable dtRebalancing = new DataTable();
          dsRebalancing = customerFPAnalyticsBo.FutureSurplusEngine(customerVo.CustomerId);
          if (dsRebalancing.Tables.Count > 0)
          {
              dtRebalancing = BindRebalancingGrid(dsRebalancing.Tables[1], assetClass);
              rdRebalancing.DataSource = dtRebalancing;
              rdRebalancing.DataBind();
          }
      }
      private DataTable BindRebalancingGrid(DataTable dtRebalancing,string assetClass)
      {
          DataTable dtRebalancingGrid = new DataTable();
            dtRebalancingGrid.Columns.Add("Year");
            dtRebalancingGrid.Columns.Add("Money_Available");
            dtRebalancingGrid.Columns.Add("Existing_Asset_Allocation");
            dtRebalancingGrid.Columns.Add("Gap_from_Agreed_Allocation");
            dtRebalancingGrid.Columns.Add("Money_Rebalanced");
            dtRebalancingGrid.Columns.Add("Money_withdrawn");
            dtRebalancingGrid.Columns.Add("Money_After_rebalancing");
            dtRebalancingGrid.Columns.Add("Money_After_rebalancing_returns");
            dtRebalancingGrid.Columns.Add("Money_flowing");
            dtRebalancingGrid.Columns.Add("Money_flowing_returns");
            dtRebalancingGrid.Columns.Add("Balance_Money");

            DataRow drRebalancingProjection;

            if (assetClass == "Equity")
            {
                foreach (DataRow drEuity in dtRebalancing.Rows)
                {
                    drRebalancingProjection = dtRebalancingGrid.NewRow();
                    if (drEuity["AssetClass"].ToString() == "Equity")
                    {
                        drRebalancingProjection["Year"] = drEuity["Year"].ToString();

                        drRebalancingProjection["Money_Available"] = String.Format("{0:n2}", decimal.Parse(drEuity["PreviousYearClosingBalance"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Existing_Asset_Allocation"] = drEuity["CurrentAssetAllocationPercent"].ToString();
                        drRebalancingProjection["Gap_from_Agreed_Allocation"] = drEuity["GapFrpmAgrredPercent"].ToString();
                        drRebalancingProjection["Money_Rebalanced"] = String.Format("{0:n2}", decimal.Parse(drEuity["MoneyToBeRebalanced"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_withdrawn"] = String.Format("{0:n2}", decimal.Parse(drEuity["GoalMoneyWithdrawn"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_After_rebalancing"] = String.Format("{0:n2}", decimal.Parse(drEuity["MoneyAvailableAfterRW"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_After_rebalancing_returns"] = String.Format("{0:n2}", decimal.Parse(drEuity["MoneyAvailableAfterRWReturn"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_flowing"] = String.Format("{0:n2}", decimal.Parse(drEuity["AmountBeforeReturns"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_flowing_returns"] = String.Format("{0:n2}", decimal.Parse(drEuity["AmountAfterReturns"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Balance_Money"] = String.Format("{0:n2}", decimal.Parse(drEuity["BalanceMoney"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                        dtRebalancingGrid.Rows.Add(drRebalancingProjection);
                    }

                }
            }

            if (assetClass == "Debt")
            {
                foreach (DataRow drDebt in dtRebalancing.Rows)
                {
                    drRebalancingProjection = dtRebalancingGrid.NewRow();
                    if (drDebt["AssetClass"].ToString() == "Debt")
                    {
                        drRebalancingProjection["Year"] = drDebt["Year"].ToString();
                        drRebalancingProjection["Money_Available"] = String.Format("{0:n2}", decimal.Parse(drDebt["PreviousYearClosingBalance"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Existing_Asset_Allocation"] = drDebt["CurrentAssetAllocationPercent"].ToString();
                        drRebalancingProjection["Gap_from_Agreed_Allocation"] = drDebt["GapFrpmAgrredPercent"].ToString();
                        drRebalancingProjection["Money_Rebalanced"] = String.Format("{0:n2}", decimal.Parse(drDebt["MoneyToBeRebalanced"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_withdrawn"] = String.Format("{0:n2}", decimal.Parse(drDebt["GoalMoneyWithdrawn"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_After_rebalancing"] = String.Format("{0:n2}", decimal.Parse(drDebt["MoneyAvailableAfterRW"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_After_rebalancing_returns"] = String.Format("{0:n2}", decimal.Parse(drDebt["MoneyAvailableAfterRWReturn"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_flowing"] = String.Format("{0:n2}", decimal.Parse(drDebt["AmountBeforeReturns"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_flowing_returns"] = String.Format("{0:n2}", decimal.Parse(drDebt["AmountAfterReturns"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Balance_Money"] = String.Format("{0:n2}", decimal.Parse(drDebt["BalanceMoney"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));


                        dtRebalancingGrid.Rows.Add(drRebalancingProjection);
                    }

                }
            }

            if (assetClass == "Cash")
            {
                foreach (DataRow drCash in dtRebalancing.Rows)
                {
                    drRebalancingProjection = dtRebalancingGrid.NewRow();
                    if (drCash["AssetClass"].ToString() == "Cash")
                    {
                        drRebalancingProjection["Year"] = drCash["Year"].ToString();
                        drRebalancingProjection["Money_Available"] = String.Format("{0:n2}", decimal.Parse(drCash["PreviousYearClosingBalance"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Existing_Asset_Allocation"] = drCash["CurrentAssetAllocationPercent"].ToString();
                        drRebalancingProjection["Gap_from_Agreed_Allocation"] = drCash["GapFrpmAgrredPercent"].ToString();
                        drRebalancingProjection["Money_Rebalanced"] = String.Format("{0:n2}", decimal.Parse(drCash["MoneyToBeRebalanced"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_withdrawn"] = String.Format("{0:n2}", decimal.Parse(drCash["GoalMoneyWithdrawn"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_After_rebalancing"] = String.Format("{0:n2}", decimal.Parse(drCash["MoneyAvailableAfterRW"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_After_rebalancing_returns"] = String.Format("{0:n2}", decimal.Parse(drCash["MoneyAvailableAfterRWReturn"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_flowing"] = String.Format("{0:n2}", decimal.Parse(drCash["AmountBeforeReturns"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_flowing_returns"] = String.Format("{0:n2}", decimal.Parse(drCash["AmountAfterReturns"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Balance_Money"] = String.Format("{0:n2}", decimal.Parse(drCash["BalanceMoney"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                        dtRebalancingGrid.Rows.Add(drRebalancingProjection);
                    }

                }
            }

            if (assetClass == "Alternate")
            {
                foreach (DataRow drAlternate in dtRebalancing.Rows)
                {
                    drRebalancingProjection = dtRebalancingGrid.NewRow();
                    if (drAlternate["AssetClass"].ToString() == "Alternate")
                    {
                        drRebalancingProjection["Year"] = drAlternate["Year"].ToString();
                        drRebalancingProjection["Money_Available"] = String.Format("{0:n2}", decimal.Parse(drAlternate["PreviousYearClosingBalance"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Existing_Asset_Allocation"] = drAlternate["CurrentAssetAllocationPercent"].ToString();
                        drRebalancingProjection["Gap_from_Agreed_Allocation"] = drAlternate["GapFrpmAgrredPercent"].ToString();
                        drRebalancingProjection["Money_Rebalanced"] = String.Format("{0:n2}", decimal.Parse(drAlternate["MoneyToBeRebalanced"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_withdrawn"] = String.Format("{0:n2}", decimal.Parse(drAlternate["GoalMoneyWithdrawn"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_After_rebalancing"] = String.Format("{0:n2}", decimal.Parse(drAlternate["MoneyAvailableAfterRW"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_After_rebalancing_returns"] = String.Format("{0:n2}", decimal.Parse(drAlternate["MoneyAvailableAfterRWReturn"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_flowing"] = String.Format("{0:n2}", decimal.Parse(drAlternate["AmountBeforeReturns"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Money_flowing_returns"] = String.Format("{0:n2}", decimal.Parse(drAlternate["AmountAfterReturns"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drRebalancingProjection["Balance_Money"] = String.Format("{0:n2}", decimal.Parse(drAlternate["BalanceMoney"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                        dtRebalancingGrid.Rows.Add(drRebalancingProjection);
                    }

                }
            }


            return dtRebalancingGrid;

        }
        private void BindCustomerProjectedAssumption()
        {
            DataSet dsCustomerProjectedAssetAllocation;
            DataTable dtCustomerProjectedAssetAllocation;
            dsCustomerProjectedAssetAllocation = customerFPAnalyticsBo.GetCustomerProjectedAssetAllocation(customerVo.CustomerId);
            DataRow[] drCheckForAlternate;
            int year =0;
            if (dsCustomerProjectedAssetAllocation.Tables[0].Rows.Count > 0)
            {
                year = int.Parse(dsCustomerProjectedAssetAllocation.Tables[0].Rows[0][5].ToString());
                drCheckForAlternate = dsCustomerProjectedAssetAllocation.Tables[0].Select("CAA_Year=" + year.ToString());
                if (drCheckForAlternate.Count() == 3)
                {
                    txtAlternate.Visible = false;
                    txtAlternateFS.Visible = false;
                    txtAlternateRV.Visible = false;
                    gdvwFutureSavings.Columns[13].Visible = false;
                    gdvwFutureSavings.Columns[14].Visible = false;
                    gdvwFutureSavings.Columns[15].Visible = false;
                    gvAssetAllocation.Columns[7].Visible = false;
                    gvAssetAllocation.Columns[8].Visible = false;
                    lblAlternate.Visible = false;
                    lblAlternateFS.Visible = false;


                }
                hdnAlternate.Value = drCheckForAlternate.Count().ToString();
                dtCustomerProjectedAssetAllocation = CreateAssetAllocationTable(dsCustomerProjectedAssetAllocation.Tables[0]);
                gvAssetAllocation.DataSource = dtCustomerProjectedAssetAllocation;
                gvAssetAllocation.DataBind();
                SetYearWiseDetailsInAllAssetAllocation(dtCustomerProjectedAssetAllocation);
            }
            else
            {
                SetEditViewMode(true);
                btnEdit.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Set Up Finance Profile and Assumption ');", true);
            }
        }
        protected DataTable CreateFutureSavingProjection(DataTable dtFutureSaving)
        {
            DataTable dtFutureSavingProjection = new DataTable();
            dtFutureSavingProjection.Columns.Add("Year");
            dtFutureSavingProjection.Columns.Add("IncomeGrowthRate");
            dtFutureSavingProjection.Columns.Add("ExpenseGrowthRate");
            dtFutureSavingProjection.Columns.Add("Avialable_Surplus");
            dtFutureSavingProjection.Columns.Add("Equity_Allocation_per");
            dtFutureSavingProjection.Columns.Add("Equity_Allocation");
            dtFutureSavingProjection.Columns.Add("Equity_FutureValue");
            dtFutureSavingProjection.Columns.Add("Debt_Allocation_per");
            dtFutureSavingProjection.Columns.Add("Debt_Allocation");
            dtFutureSavingProjection.Columns.Add("Debt_FutureValue");
            dtFutureSavingProjection.Columns.Add("Cash_Allocation_per");
            dtFutureSavingProjection.Columns.Add("Cash_Allocation");
            dtFutureSavingProjection.Columns.Add("Cash_FutureValue");
            dtFutureSavingProjection.Columns.Add("Alternate_Allocation_per");
            dtFutureSavingProjection.Columns.Add("Alternate_Allocation");
            dtFutureSavingProjection.Columns.Add("Alternate_FutureValue");

            dtFutureSavingProjection.Columns.Add("Amount_Returns");

            DataRow drFutureSavingProjection;



            int tempYear = 0;

            //int assetClassificationCode = 0;
            //DataRow[] drFutureSavingProjectionYearWise;
            foreach (DataRow drFutureSaving in dtFutureSaving.Rows)
            {
                //if (tempYear != int.Parse(drFutureSaving["Year"].ToString()))
                //{
                //    tempYear = int.Parse(drFutureSaving["Year"].ToString());
                //    //drFinalAssumption["Year"] = tempYear.ToString();
                //    drFutureSavingProjectionYearWise = dtFutureSaving.Select("Year=" + tempYear.ToString());
                drFutureSavingProjection = dtFutureSavingProjection.NewRow();

                //    foreach (DataRow dr in drFutureSavingProjectionYearWise)
                //    {
                drFutureSavingProjection["Year"] = drFutureSaving["Year"].ToString();
                drFutureSavingProjection["IncomeGrowthRate"] = String.Format("{0:n2}",decimal.Parse(drFutureSaving["IncomeGrowth"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                drFutureSavingProjection["ExpenseGrowthRate"] = String.Format("{0:n2}", decimal.Parse(drFutureSaving["ExpenseGrowth"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                drFutureSavingProjection["Avialable_Surplus"] = String.Format("{0:n2}", decimal.Parse(drFutureSaving["AvailableSurplus"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                drFutureSavingProjection["Equity_Allocation_per"] = drFutureSaving["FutureSavingEquityPercent"].ToString();
                drFutureSavingProjection["Equity_Allocation"] = String.Format("{0:n2}", decimal.Parse(drFutureSaving["EquityAmount"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                drFutureSavingProjection["Equity_FutureValue"] = String.Format("{0:n2}", decimal.Parse(drFutureSaving["EquityFutureValue"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                drFutureSavingProjection["Debt_Allocation_per"] = drFutureSaving["FutureSavingDebtPercent"].ToString();
                drFutureSavingProjection["Debt_Allocation"] = String.Format("{0:n2}", decimal.Parse(drFutureSaving["DebtAmount"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                drFutureSavingProjection["Debt_FutureValue"] = String.Format("{0:n2}", decimal.Parse(drFutureSaving["DebtFutureValue"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                drFutureSavingProjection["Cash_Allocation_per"] = drFutureSaving["FutureSavingCashPercent"].ToString();
                drFutureSavingProjection["Cash_Allocation"] = String.Format("{0:n2}", decimal.Parse(drFutureSaving["CashAmount"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                drFutureSavingProjection["Cash_FutureValue"] = String.Format("{0:n2}", decimal.Parse(drFutureSaving["CashFutureValue"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                drFutureSavingProjection["Alternate_Allocation_per"] = drFutureSaving["FutureSavingAlternatePercent"].ToString();
                drFutureSavingProjection["Alternate_Allocation"] = String.Format("{0:n2}", decimal.Parse(drFutureSaving["alternateAmount"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                drFutureSavingProjection["Alternate_FutureValue"] = String.Format("{0:n2}", decimal.Parse(drFutureSaving["AlternateFutureValue"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                drFutureSavingProjection["Amount_Returns"] = drFutureSaving["TotalAssetFutureValues"].ToString();
                dtFutureSavingProjection.Rows.Add(drFutureSavingProjection);
            }

            return dtFutureSavingProjection;
        }
        protected DataTable CreateAssetAllocationTable(DataTable dtAssetAllocation)
        {
            DataTable dtCustomerProjectedAssetAllocation = new DataTable();
            dtCustomerProjectedAssetAllocation.Columns.Add("Year");
            dtCustomerProjectedAssetAllocation.Columns.Add("Rec_Equity");
            dtCustomerProjectedAssetAllocation.Columns.Add("Rec_Debt");
            dtCustomerProjectedAssetAllocation.Columns.Add("Rec_Cash");
            dtCustomerProjectedAssetAllocation.Columns.Add("Rec_Alternate");

            dtCustomerProjectedAssetAllocation.Columns.Add("Agr_Equity");
            dtCustomerProjectedAssetAllocation.Columns.Add("Agr_Debt");
            dtCustomerProjectedAssetAllocation.Columns.Add("Agr_Cash");
            dtCustomerProjectedAssetAllocation.Columns.Add("Agr_Alternate");

            DataRow drCustomerProjectedAssetAllocation;
            int tempYear = 0;
            int assetClassificationCode = 0;
            DataRow[] drAssetallocationYearWise;
            foreach (DataRow drAssetAllocation in dtAssetAllocation.Rows)
            {
                if (tempYear != int.Parse(drAssetAllocation["CAA_Year"].ToString()))
                {
                    tempYear = int.Parse(drAssetAllocation["CAA_Year"].ToString());
                    //drFinalAssumption["Year"] = tempYear.ToString();
                    drAssetallocationYearWise = dtAssetAllocation.Select("CAA_Year=" + tempYear.ToString());
                    drCustomerProjectedAssetAllocation = dtCustomerProjectedAssetAllocation.NewRow();

                    foreach (DataRow dr in drAssetallocationYearWise)
                    {
                        assetClassificationCode = int.Parse(dr["WAC_AssetClassificationCode"].ToString());
                        switch (assetClassificationCode)
                        {
                            case 1:
                                {
                                    drCustomerProjectedAssetAllocation["Rec_Equity"] = dr["CAA_RecommendedPercentage"].ToString();
                                    drCustomerProjectedAssetAllocation["Agr_Equity"] = Math.Round(decimal.Parse(dr["CAA_RecommendedPercentage"].ToString()),0) +Math.Round(decimal.Parse(dr["CAA_AgreedAdjustment"].ToString()),0);
                                    break;
                                }
                            case 2:
                                {
                                    drCustomerProjectedAssetAllocation["Rec_Debt"] = dr["CAA_RecommendedPercentage"].ToString();
                                    drCustomerProjectedAssetAllocation["Agr_Debt"] = Math.Round(decimal.Parse(dr["CAA_RecommendedPercentage"].ToString()),0) +Math.Round(decimal.Parse(dr["CAA_AgreedAdjustment"].ToString()),0);
                                    break;

                                }
                            case 3:
                                {
                                    drCustomerProjectedAssetAllocation["Rec_Cash"] = dr["CAA_RecommendedPercentage"].ToString();
                                    drCustomerProjectedAssetAllocation["Agr_Cash"] =Math.Round( decimal.Parse(dr["CAA_RecommendedPercentage"].ToString()),0) + Math.Round(decimal.Parse(dr["CAA_AgreedAdjustment"].ToString()),0);
                                    break;
                                }
                            case 4:
                                {
                                    drCustomerProjectedAssetAllocation["Rec_Alternate"] = dr["CAA_RecommendedPercentage"].ToString();
                                    drCustomerProjectedAssetAllocation["Agr_Alternate"] =Math.Round( decimal.Parse(dr["CAA_RecommendedPercentage"].ToString()),0) + decimal.Parse((dr["CAA_AgreedAdjustment"].ToString()),0);
                                    break;
                                }

                        }

                        drCustomerProjectedAssetAllocation["Year"] = tempYear.ToString();


                    }

                    dtCustomerProjectedAssetAllocation.Rows.Add(drCustomerProjectedAssetAllocation);
                }
            }


            dtFPProjectionAssetAllocation = dtCustomerProjectedAssetAllocation;
            return dtCustomerProjectedAssetAllocation;

        }
        protected void btnSubmitAggredAllocation_OnClick(object sender, EventArgs e)
        {
            decimal equityAssetAllocation = 0;
            decimal equityAgreedAssetAllocation = 0;

            decimal debtAssetAllocation = 0;
            decimal debtAgreedAssetAllocation = 0;

            decimal cashAssetAllocation = 0;
            decimal cashAgreedAssetAllocation = 0;

            decimal alternateAssetAllocation = 0;
            decimal alternateAgreedAssetAllocation = 0;
            int tempYear = 0;

            if (txtEquity.Text.ToString() != "")
                equityAssetAllocation = decimal.Parse(txtEquity.Text.ToString());

            if (txtDebt.Text.ToString() != "")
                debtAssetAllocation = decimal.Parse(txtDebt.Text.ToString());

            if (txtCash.Text.ToString() != "")
                cashAssetAllocation = decimal.Parse(txtCash.Text.ToString());

            if (txtAlternate.Text.ToString() != "")
                alternateAssetAllocation = decimal.Parse(txtAlternate.Text.ToString());

            tempYear = int.Parse(ddlPickYear.SelectedValue.ToString());

            foreach (DataRow dr in dtFPProjectionAssetAllocation.Rows)
            {
                if (tempYear == int.Parse(dr["Year"].ToString()))
                {
                    if (equityAssetAllocation != 0)
                    {
                        if (dr["Rec_Equity"].ToString() == "")
                        {
                            alternateAgreedAssetAllocation = alternateAssetAllocation - 0;
                        }
                        else
                            equityAgreedAssetAllocation = equityAssetAllocation - decimal.Parse(dr["Rec_Equity"].ToString());
                    }
                    if (debtAssetAllocation != 0)
                    {
                        if (dr["Rec_Debt"].ToString() == "")
                        {
                            alternateAgreedAssetAllocation = alternateAssetAllocation - 0;
                        }
                        else
                            debtAgreedAssetAllocation = debtAssetAllocation - decimal.Parse(dr["Rec_Debt"].ToString());
                    }
                    if (cashAssetAllocation != 0)
                    {
                        if (dr["Rec_Cash"].ToString() == "")
                        {
                            alternateAgreedAssetAllocation = alternateAssetAllocation - 0;
                        }
                        else
                            cashAgreedAssetAllocation = cashAssetAllocation - decimal.Parse(dr["Rec_Cash"].ToString());
                    }
                    if (alternateAssetAllocation != 0)
                    {
                        if (dr["Rec_Alternate"].ToString() == "")
                        {
                            alternateAgreedAssetAllocation = alternateAssetAllocation - 0;
                        }
                        else
                            alternateAgreedAssetAllocation = alternateAssetAllocation - decimal.Parse(dr["Rec_Alternate"].ToString());
                    }
                    break;
                }
            }
            int rangeFromYear = int.Parse(ddlFromYear.SelectedItem.Text.ToString());
            int rangeToYear = int.Parse(ddlToYear.SelectedItem.Text.ToString());
            if (rdbYearWise.Checked==true)
            customerFPAnalyticsBo.UpdateFPProjectionAssetAllocation(customerVo.CustomerId,0,0, tempYear, equityAgreedAssetAllocation, debtAgreedAssetAllocation, cashAgreedAssetAllocation, alternateAgreedAssetAllocation);
            if(rdbYearRangeWise.Checked==true)
                customerFPAnalyticsBo.UpdateFPProjectionAssetAllocation(customerVo.CustomerId,rangeFromYear, rangeToYear,0, equityAgreedAssetAllocation, debtAgreedAssetAllocation, cashAgreedAssetAllocation, alternateAgreedAssetAllocation);      
            BindCustomerProjectedAssumption();
            msgRecordStatus.Visible = true;
            SetEditViewMode(true);

        }

        protected void btnSubmitFpFs_OnClick(object sender, EventArgs e)
        {
            decimal equityFutureAllocation = 0;
            decimal debtFutureAllocation = 0;
            decimal cashFutureAllocation = 0;
            decimal alternateFutureAllocation = 0;

            if (txtEquityFS.Text.ToString() != "")
                equityFutureAllocation = int.Parse(txtEquityFS.Text.ToString());

            if (txtDebtFS.Text.ToString() != "")
                debtFutureAllocation = int.Parse(txtDebtFS.Text.ToString());

            if (txtCashFS.Text.ToString() != "")
                cashFutureAllocation = int.Parse(txtCashFS.Text.ToString());

            if (txtAlternateFS.Text.ToString() != "")
                alternateFutureAllocation = int.Parse(txtAlternateFS.Text.ToString());

            int tempYear = int.Parse(ddlPickYearFS.SelectedValue.ToString());
            int rangeFromYearFutureSaving=int.Parse(ddlRangeYearFSFROM.SelectedItem.ToString());
            int rangeToYearFutureSaving = int.Parse(ddlRangeYearFSTO.SelectedItem.ToString());
            if(rbtnFSPickYear.Checked==true)
                customerFPAnalyticsBo.UpdateFutureSavingProjection(customerVo.CustomerId, adviserVo.advisorId, equityFutureAllocation, debtFutureAllocation, cashFutureAllocation, alternateFutureAllocation, tempYear,0,0);
           if(rbtnFSRangeYear.Checked==true)
               customerFPAnalyticsBo.UpdateFutureSavingProjection(customerVo.CustomerId, adviserVo.advisorId, equityFutureAllocation, debtFutureAllocation, cashFutureAllocation, alternateFutureAllocation, 0, rangeFromYearFutureSaving, rangeToYearFutureSaving);
            BindFPFutureSavingGrid();
            msgRecordStatus.Visible = true;
            SetEditViewMode(true);
        }

        //protected void gvAssetAllocation_PreRender(object sender, EventArgs e)
        //{
        //    gvAssetAllocation.UseAccessibleHeader = true;
        //    gvAssetAllocation.HeaderRow.TableSection = TableRowSection.TableHeader;
        //}

    }
}
