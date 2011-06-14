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
            if (!Page.IsPostBack)
            {
                Session["PickAssetClassDPSelected"] = null;
                dsFPAnalyticsEngine = customerFPAnalyticsBo.FutureSurplusEngine(customerVo.CustomerId);
                BindYearDropDowns();
                BindDropdownsRebalancing();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "pageloadscript", @"ShowHideGaolType();", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "pageloadscript", @"ShowHideGaolTypeFS();", true);

            }
            BindCustomerProjectedAssumption();
            BindFPFutureSavingGrid();

            if (Session["PickAssetClassDPSelected"] != null)
            {
                CallRebalancing();
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
            ddlRebalancing.Items.Insert(0, new ListItem("-Select-", "-Select-"));

        }
        private void BindFPFutureSavingGrid()
        {
            DataSet dsFPFutureSavingGrid = new DataSet();
            DataTable dtFPFutureSavingGrid = new DataTable();
            dsFPFutureSavingGrid = customerFPAnalyticsBo.FutureSurplusEngine(customerVo.CustomerId);
            dtFPFutureSavingGrid = CreateFutureSavingProjection(dsFPFutureSavingGrid.Tables[0]);
            gdvwFutureSavings.DataSource = dtFPFutureSavingGrid;
            gdvwFutureSavings.DataBind();

        }


        protected void ddlRebalancing_OnSelectIndexChanged(object sender, EventArgs e)
        {
            Session["PickAssetClassDPSelected"] = ddlRebalancing.SelectedItem.ToString();
            if (ddlRebalancing.SelectedItem.ToString() != "-Select-")
                CallRebalancing();
        }

        protected void CallRebalancing()
        {
            string assetClass = ddlRebalancing.SelectedItem.ToString();
            DataSet dsRebalancing = new DataSet();
            DataTable dtRebalancing = new DataTable();
            dsRebalancing = customerFPAnalyticsBo.FutureSurplusEngine(customerVo.CustomerId);
            dtRebalancing = BindRebalancingGrid(dsRebalancing.Tables[1], assetClass);
            rdRebalancing.DataSource = dtRebalancing;
            rdRebalancing.DataBind();
        }
        private DataTable BindRebalancingGrid(DataTable dtRebalancing, string assetClass)
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

                        drRebalancingProjection["Money_Available"] = drEuity["PreviousYearClosingBalance"].ToString();
                        drRebalancingProjection["Existing_Asset_Allocation"] = drEuity["CurrentAssetAllocationPercent"].ToString();
                        drRebalancingProjection["Gap_from_Agreed_Allocation"] = drEuity["GapFrpmAgrredPercent"].ToString();
                        drRebalancingProjection["Money_Rebalanced"] = drEuity["MoneyToBeRebalanced"].ToString();
                        drRebalancingProjection["Money_withdrawn"] = drEuity["GoalMoneyWithdrawn"].ToString();
                        drRebalancingProjection["Money_After_rebalancing"] = drEuity["MoneyAvailableAfterRW"].ToString();
                        drRebalancingProjection["Money_After_rebalancing_returns"] = drEuity["MoneyAvailableAfterRWReturn"].ToString();
                        drRebalancingProjection["Money_flowing"] = drEuity["AmountBeforeReturns"].ToString();
                        drRebalancingProjection["Money_flowing_returns"] = drEuity["AmountAfterReturns"].ToString();
                        drRebalancingProjection["Balance_Money"] = drEuity["BalanceMoney"].ToString();

                        dtRebalancingGrid.Rows.Add(drRebalancingProjection);
                    }

                }
            }

            if (assetClass == "Debt")
            {
                foreach (DataRow drEuity in dtRebalancing.Rows)
                {
                    drRebalancingProjection = dtRebalancingGrid.NewRow();
                    if (drEuity["AssetClass"].ToString() == "Debt")
                    {
                        drRebalancingProjection["Year"] = drEuity["Year"].ToString();

                        drRebalancingProjection["Money_Available"] = drEuity["PreviousYearClosingBalance"].ToString();
                        drRebalancingProjection["Existing_Asset_Allocation"] = drEuity["CurrentAssetAllocationPercent"].ToString();
                        drRebalancingProjection["Gap_from_Agreed_Allocation"] = drEuity["GapFrpmAgrredPercent"].ToString();
                        drRebalancingProjection["Money_Rebalanced"] = drEuity["MoneyToBeRebalanced"].ToString();
                        drRebalancingProjection["Money_withdrawn"] = drEuity["GoalMoneyWithdrawn"].ToString();
                        drRebalancingProjection["Money_After_rebalancing"] = drEuity["MoneyAvailableAfterRW"].ToString();
                        drRebalancingProjection["Money_After_rebalancing_returns"] = drEuity["MoneyAvailableAfterRWReturn"].ToString();
                        drRebalancingProjection["Money_flowing"] = drEuity["AmountBeforeReturns"].ToString();
                        drRebalancingProjection["Money_flowing_returns"] = drEuity["AmountAfterReturns"].ToString();
                        drRebalancingProjection["Balance_Money"] = drEuity["BalanceMoney"].ToString();

                        dtRebalancingGrid.Rows.Add(drRebalancingProjection);
                    }

                }
            }

            if (assetClass == "Cash")
            {
                foreach (DataRow drEuity in dtRebalancing.Rows)
                {
                    drRebalancingProjection = dtRebalancingGrid.NewRow();
                    if (drEuity["AssetClass"].ToString() == "Cash")
                    {
                        drRebalancingProjection["Year"] = drEuity["Year"].ToString();

                        drRebalancingProjection["Money_Available"] = drEuity["PreviousYearClosingBalance"].ToString();
                        drRebalancingProjection["Existing_Asset_Allocation"] = drEuity["CurrentAssetAllocationPercent"].ToString();
                        drRebalancingProjection["Gap_from_Agreed_Allocation"] = drEuity["GapFrpmAgrredPercent"].ToString();
                        drRebalancingProjection["Money_Rebalanced"] = drEuity["MoneyToBeRebalanced"].ToString();
                        drRebalancingProjection["Money_withdrawn"] = drEuity["GoalMoneyWithdrawn"].ToString();
                        drRebalancingProjection["Money_After_rebalancing"] = drEuity["MoneyAvailableAfterRW"].ToString();
                        drRebalancingProjection["Money_After_rebalancing_returns"] = drEuity["MoneyAvailableAfterRWReturn"].ToString();
                        drRebalancingProjection["Money_flowing"] = drEuity["AmountBeforeReturns"].ToString();
                        drRebalancingProjection["Money_flowing_returns"] = drEuity["AmountAfterReturns"].ToString();
                        drRebalancingProjection["Balance_Money"] = drEuity["BalanceMoney"].ToString();

                        dtRebalancingGrid.Rows.Add(drRebalancingProjection);
                    }

                }
            }

            if (assetClass == "Alternate")
            {
                foreach (DataRow drEuity in dtRebalancing.Rows)
                {
                    drRebalancingProjection = dtRebalancingGrid.NewRow();
                    if (drEuity["AssetClass"].ToString() == "Alternate")
                    {
                        drRebalancingProjection["Year"] = drEuity["Year"].ToString();

                        drRebalancingProjection["Money_Available"] = drEuity["PreviousYearClosingBalance"].ToString();
                        drRebalancingProjection["Existing_Asset_Allocation"] = drEuity["CurrentAssetAllocationPercent"].ToString();
                        drRebalancingProjection["Gap_from_Agreed_Allocation"] = drEuity["GapFrpmAgrredPercent"].ToString();
                        drRebalancingProjection["Money_Rebalanced"] = drEuity["MoneyToBeRebalanced"].ToString();
                        drRebalancingProjection["Money_withdrawn"] = drEuity["GoalMoneyWithdrawn"].ToString();
                        drRebalancingProjection["Money_After_rebalancing"] = drEuity["MoneyAvailableAfterRW"].ToString();
                        drRebalancingProjection["Money_After_rebalancing_returns"] = drEuity["MoneyAvailableAfterRWReturn"].ToString();
                        drRebalancingProjection["Money_flowing"] = drEuity["AmountBeforeReturns"].ToString();
                        drRebalancingProjection["Money_flowing_returns"] = drEuity["AmountAfterReturns"].ToString();
                        drRebalancingProjection["Balance_Money"] = drEuity["BalanceMoney"].ToString();

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
            dtCustomerProjectedAssetAllocation = CreateAssetAllocationTable(dsCustomerProjectedAssetAllocation.Tables[0]);
            gvAssetAllocation.DataSource = dtCustomerProjectedAssetAllocation;
            gvAssetAllocation.DataBind();

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
                drFutureSavingProjection["IncomeGrowthRate"] = drFutureSaving["IncomeGrowth"].ToString();
                drFutureSavingProjection["ExpenseGrowthRate"] = drFutureSaving["ExpenseGrowth"].ToString();
                drFutureSavingProjection["Avialable_Surplus"] = drFutureSaving["AvailableSurplus"].ToString();
                drFutureSavingProjection["Equity_Allocation_per"] = drFutureSaving["FutureSavingEquityPercent"].ToString();
                drFutureSavingProjection["Equity_Allocation"] = drFutureSaving["EquityAmount"].ToString();
                drFutureSavingProjection["Equity_FutureValue"] = drFutureSaving["EquityFutureValue"].ToString();
                drFutureSavingProjection["Debt_Allocation_per"] = drFutureSaving["FutureSavingDebtPercent"].ToString();
                drFutureSavingProjection["Debt_Allocation"] = drFutureSaving["DebtAmount"].ToString();
                drFutureSavingProjection["Debt_FutureValue"] = drFutureSaving["DebtFutureValue"].ToString();
                drFutureSavingProjection["Cash_Allocation_per"] = drFutureSaving["FutureSavingCashPercent"].ToString();
                drFutureSavingProjection["Cash_Allocation"] = drFutureSaving["CashAmount"].ToString();
                drFutureSavingProjection["Cash_FutureValue"] = drFutureSaving["CashFutureValue"].ToString();
                drFutureSavingProjection["Alternate_Allocation_per"] = drFutureSaving["FutureSavingAlternatePercent"].ToString();
                drFutureSavingProjection["Alternate_Allocation"] = drFutureSaving["alternateAmount"].ToString();
                drFutureSavingProjection["Alternate_FutureValue"] = drFutureSaving["AlternateFutureValue"].ToString();

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
                                    drCustomerProjectedAssetAllocation["Agr_Equity"] = decimal.Parse(dr["CAA_RecommendedPercentage"].ToString()) + decimal.Parse((dr["CAA_AgreedAdjustment"].ToString()));
                                    break;
                                }
                            case 2:
                                {
                                    drCustomerProjectedAssetAllocation["Rec_Debt"] = dr["CAA_RecommendedPercentage"].ToString();
                                    drCustomerProjectedAssetAllocation["Agr_Debt"] = decimal.Parse(dr["CAA_RecommendedPercentage"].ToString()) + decimal.Parse(dr["CAA_AgreedAdjustment"].ToString());
                                    break;

                                }
                            case 3:
                                {
                                    drCustomerProjectedAssetAllocation["Rec_Cash"] = dr["CAA_RecommendedPercentage"].ToString();
                                    drCustomerProjectedAssetAllocation["Agr_Cash"] = decimal.Parse(dr["CAA_RecommendedPercentage"].ToString()) + decimal.Parse((dr["CAA_AgreedAdjustment"].ToString()));
                                    break;
                                }
                            case 4:
                                {
                                    drCustomerProjectedAssetAllocation["Rec_Alternate"] = dr["CAA_RecommendedPercentage"].ToString();
                                    drCustomerProjectedAssetAllocation["Agr_Alternate"] = decimal.Parse(dr["CAA_RecommendedPercentage"].ToString()) + decimal.Parse((dr["CAA_AgreedAdjustment"].ToString()));
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
            customerFPAnalyticsBo.UpdateFPProjectionAssetAllocation(customerVo.CustomerId, tempYear, equityAgreedAssetAllocation, debtAgreedAssetAllocation, cashAgreedAssetAllocation, alternateAgreedAssetAllocation);
            BindCustomerProjectedAssumption();


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
            customerFPAnalyticsBo.UpdateFutureSavingProjection(customerVo.CustomerId, adviserVo.advisorId, equityFutureAllocation, debtFutureAllocation, cashFutureAllocation, alternateFutureAllocation, tempYear);
            BindFPFutureSavingGrid();
        }

        //protected void gvAssetAllocation_PreRender(object sender, EventArgs e)
        //{
        //    gvAssetAllocation.UseAccessibleHeader = true;
        //    gvAssetAllocation.HeaderRow.TableSection = TableRowSection.TableHeader;
        //}

    }
}