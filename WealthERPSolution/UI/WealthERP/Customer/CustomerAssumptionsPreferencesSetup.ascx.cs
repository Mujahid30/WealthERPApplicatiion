using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VoCustomerProfiling;
using BoCustomerProfiling;
using VoUser;
using BoUser;
using PCGMailLib;
using System.Net.Mail;
using System.Configuration;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WealthERP.Base;


namespace WealthERP.Customer
{
    public partial class CustomerAssumptionsPreferencesSetup : System.Web.UI.UserControl
    {
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();
        RMVo rmVo = new RMVo();        
        AdvisorVo adviserVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        int expiryAge = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            adviserVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            rmVo = (RMVo)Session["rmVo"];
            userVo = (UserVo)Session["UserVo"];
            this.Page.Culture = "en-US";
            SessionBo.CheckSession();
            btnEdit.Enabled = true;
            //lblProjectedAssumptions.Visible = true;
            //gvProjectedAssumption.Visible = true;
            msgRecordStatus.Visible = false;
            //hfAge.Value = customerVo.Dob.Year.ToString();
            //trStaticGrid.Visible = false;
            
            //expiryAge = customerBo.ExpiryAgeOfAdviser(adviserVo.advisorId, customerVo.CustomerId);

            //if (ddlPickAssumtion.SelectedIndex == 0)
            //{
            //    txtAssumptionValue.Enabled = false;
            //}
            //else
            //    txtAssumptionValue.Enabled = true;


            //if (!IsPostBack)
            //{
            //    //rbtnYearly.Checked = true;
                //if (customerVo.Dob != DateTime.MinValue)
                //{
                    
                //    DateTime now = DateTime.Today;
                //    int todayAgeInYear = now.Year;
                //    DateTime dob = customerVo.Dob;
                //    int age = todayAgeInYear - dob.Year;
                //    if (age > expiryAge)
                //    {
                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Your Age is Exceeding Expiry Age');", true);
                //        btnEdit.Enabled = false;
                //        lblProjectedAssumptions.Visible = false;
                //        gvProjectedAssumption.Visible = false;
                //    }
                //    else
                //    {
                //        BindYearDropDowns();
                //        SetDefaultVisibilty();
                //        BindDropDownassumption("P");                        
                //    }
                //}
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Update Your Date Of Birth Before Proceed');", true);
                //    btnEdit.Enabled = false;
                //    lblProjectedAssumptions.Visible = false;
                //    gvProjectedAssumption.Visible = false;

                //}
            // }
            //if (!IsPostBack)
            //{
            //    BindAllCustomerAssumptions();
            //}
            SetDefaultPlanRetirementValueForCustomer();


            if (ViewState["ActionAssumptionPage"] == null)
               {
                   ViewState["ActionAssumptionPage"] = "View";
               }

             if (ViewState["ActionAssumptionPage"].ToString() == "View")
             {
                 SetEditViewMode(false);
             }
             else if (ViewState["ActionAssumptionPage"].ToString() == "Edit")
             {
                 SetEditViewMode(true);
             }
        }
       
            
        
       

        //protected void rbtnyear_OnCheckChanged(object sender, EventArgs e)
        //{
        //    tdLblYear.Visible=true;
        //    tdtxtYear.Visible=true;
        //    tdlblRangeYear.Visible=false;
        //    tdlblRangeFrom.Visible=false;
        //    tdlblRangeTo.Visible = false;



        //}
        //protected void rbtnRangeYear_OnCheckChanged(object sender, EventArgs e)
        //{
        //    tdLblYear.Visible = false;
        //    tdtxtYear.Visible = false;
        //    tdlblRangeYear.Visible = true;
        //    tdlblRangeFrom.Visible = true;
        //    tdlblRangeTo.Visible = true;


        //}
        //public void BindDropDownassumption(string flag)
        //{
        //    DataTable dtBindDropDownassumption = new DataTable();
        //    dtBindDropDownassumption = customerBo.BindDropDownassumption(flag);
        //    ddlPickAssumtion.DataSource = dtBindDropDownassumption;
        //    ddlPickAssumtion.DataTextField = dtBindDropDownassumption.Columns["WA_AssumptionName"].ToString();
        //    ddlPickAssumtion.DataValueField = dtBindDropDownassumption.Columns["WA_AssumptionId"].ToString();
        //    ddlPickAssumtion.DataBind();
        //    ddlPickAssumtion.Items.Insert(0,new ListItem("-Select-", "-Select-"));

        //}
        //protected void ddlPickAssumtion_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    msgRecordStatus.Visible = false;
        //    //rdbYearWise.Checked = true;
        //    //rdbYearRangeWise.Checked = false;
        //    trRangeYear.Visible = false;
        //    trPickYear.Visible = false;
        //    //tdLblYear.Visible = true;
        //    //tdtxtYear.Visible = true;
        //    //tdlblRangeYear.Visible = false;
        //    //tdlblRangeFrom.Visible = false;
        //    //tdlblRangeTo.Visible = false;
        //    if (ddlPickAssumtion.SelectedValue == "LE" || ddlPickAssumtion.SelectedValue == "RA")
        //    {
        //        trRangeYear.Visible = false;
        //        trPickYear.Visible = false;
        //        trRbtnYear.Visible = false;
        //    }
        //    else
        //    {
        //        //trRbtnYearDetail.Visible = true;
        //        //trYearRangeDetail.Visible = true;
        //    }
        //    if (ddlPickAssumtion.SelectedIndex != 0)
        //        txtAssumptionValue.Enabled = true;
        //        BindYearDropDowns();
            

            
        //}

        //public void SetDefaultVisibilty()
        //{
        //    //tdLblYear.Visible = true;
        //    //tdtxtYear.Visible = true;
        //    //tdlblRangeYear.Visible = true;
        //    //tdlblRangeFrom.Visible = true;
        //    //tdlblRangeTo.Visible = true;
        //    //tdlblRangeYear.Visible = false;
        //    //tdlblRangeFrom.Visible = false;
        //    //tdlblRangeTo.Visible = false;

        //    msgRecordStatus.Visible = false;

        //}
        //protected void btnSubmit_OnClick(object sender, EventArgs e)
        //{
        //    int userId = 0;
        //    int customerId = 0;
        //    decimal assumptionValue = 0;
        //    if (ddlPickAssumtion.SelectedValue == "LE" || ddlPickAssumtion.SelectedValue == "RA")
        //    {
        //        userId = userVo.UserId;
        //        customerId = customerVo.CustomerId;
        //        assumptionValue = decimal.Parse(txtAssumptionValue.Text.ToString());
        //        customerBo.InsertCustomerStaticDetalis(userId, customerId, assumptionValue, ddlPickAssumtion.SelectedValue.ToString().Trim());

        //        if (ddlPickAssumtion.SelectedValue == "LE")
        //        {
        //            txtLifeExpectancy.Text = txtAssumptionValue.Text;
                    
        //        }
        //        else if (ddlPickAssumtion.SelectedValue == "RA")
        //        {
        //            txtRetirementAge.Text = txtAssumptionValue.Text;

        //        }

        //        txtAssumptionValue.Text = "";
        //        msgRecordStatus.Visible = true;
        //    }
        //    else
        //    {
        //        if (ddlPickPeriod.SelectedValue == "SY")
        //        {
        //            trPickYear.Visible = true;
        //            trRangeYear.Visible = false;
        //            int yearSelected = 0;
        //            yearSelected = int.Parse(ddlPickYear.SelectedItem.Text.ToString());
        //            userId = userVo.UserId;
        //            customerId = customerVo.CustomerId;
        //            assumptionValue = decimal.Parse(txtAssumptionValue.Text.ToString());
        //            customerBo.UpdateCustomerProjectedDetalis(userId, customerId, assumptionValue, ddlPickAssumtion.SelectedValue.ToString().Trim(),yearSelected,0,0);
                    

        //        }
        //        else if (ddlPickPeriod.SelectedValue == "RY")
        //        {
        //            trRangeYear.Visible = true;
        //            trPickYear.Visible = false;
        //            int rangeFromYear = 0;
        //            int rangeToYear = 0;
        //            userId = userVo.UserId;
        //            customerId = customerVo.CustomerId;
        //            rangeFromYear = int.Parse(ddlFromYear.SelectedItem.Text.ToString());
        //            rangeToYear = int.Parse(ddlToYear.SelectedItem.Text.ToString());
        //            assumptionValue = decimal.Parse(txtAssumptionValue.Text.ToString());
        //            customerBo.UpdateCustomerProjectedDetalis(userId, customerId, assumptionValue, ddlPickAssumtion.SelectedValue.ToString().Trim(), 0, rangeFromYear, rangeToYear);
        //        }                
        //        txtAssumptionValue.Text = "";
        //        msgRecordStatus.Visible = true;
        //        //rbtnyear.Checked = true;
        //        //rbtnRangeYear.Checked = false;
        //        //tdlblRangeFrom.Visible = false;
        //        //tdlblRangeTo.Visible = false;
        //        //tdlblRangeYear.Visible = false;
        //        //tdLblYear.Visible = true;
        //        //tdtxtYear.Visible = true;
        //        ddlFromYear.SelectedIndex = 0;
        //        ddlToYear.SelectedIndex = 0;
        //        ddlPickYear.SelectedIndex = 0;
               
              
        //    }
        //    BindAllCustomerAssumptions();  
        //}

        //public void BindYearDropDowns()
        //{
        //        int customerAge=0;
        //        int remainingAge = 0;
        //        int todayAgeInYear = 0;
        //        int nextPeriod = 0;
                             
        //        DateTime now=DateTime.Today;
        //        todayAgeInYear = now.Year;
        //        DateTime dob=customerVo.Dob;
             
                
        //          customerAge = now.Year - dob.Year;
        //          remainingAge = expiryAge - customerAge;
                
        //        nextPeriod = todayAgeInYear + remainingAge;
        //        for (; todayAgeInYear <= nextPeriod; todayAgeInYear++)
        //        {
        //            ddlPickYear.Items.Add(todayAgeInYear.ToString());
        //            ddlFromYear.Items.Add(todayAgeInYear.ToString());
        //            ddlToYear.Items.Add(todayAgeInYear.ToString());
        //        }
        //}

        //public void BindAllCustomerAssumptions()
        //{
        //    DataSet dsBindAllCustomerAssumptions;
            
        //    DataTable dtProjectedAssumption = new DataTable();
        //    dsBindAllCustomerAssumptions = customerBo.GetAllCustomersAssumptions(customerVo.CustomerId);
        //    dtProjectedAssumption = AssumptionTableCreation(dsBindAllCustomerAssumptions.Tables[1]);
            
        //    gvProjectedAssumption.DataSource = dtProjectedAssumption;
        //    gvProjectedAssumption.DataBind();

        //    txtLifeExpectancy.Text =  (Math.Round(decimal.Parse(dsBindAllCustomerAssumptions.Tables[0].Rows[0]["CSA_Value"].ToString()),0)).ToString();
        //        //Math.Round(double.Parse(dr["CG_CostToday"].ToString()), 2);

        //    txtRetirementAge.Text = (Math.Round(decimal.Parse(dsBindAllCustomerAssumptions.Tables[0].Rows[0]["CSA_Value"].ToString()), 0)).ToString();
        //            //dsBindAllCustomerAssumptions.Tables[0].Rows[1]["CSA_Value"].ToString();
        
        //  }



        //protected void gvProjectedAssumption_OnPageIndexChanging(Object sender, GridViewPageEventArgs e)
        //{
        //    gvProjectedAssumption.PageIndex = e.NewPageIndex;
        //    BindAllCustomerAssumptions();
        //}

        //private void GetPageCount()
        //{
        //    string upperlimit;
        //    int rowCount = Convert.ToInt32(hdnProjectedCount.Value);
        //    int ratio = rowCount / 10;
        //    mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
        //    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
        //    string lowerlimit = (((mypager.CurrentPage - 1) * 10) + 1).ToString();
        //    upperlimit = (mypager.CurrentPage * 10).ToString();
        //    if (mypager.CurrentPage == mypager.PageCount)
        //        upperlimit = hdnProjectedCount.Value;
        //    string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
        //    lblProjectedAssumptionTotalRows.Text = PageRecords;
        //    hdnCurrentPage.Value = mypager.CurrentPage.ToString();

        //}



        protected void btnGoalFundingPreference_OnClick(object sender, EventArgs e)
        {
            UpdatePlanPreferences();
            //msgRecordStatus.Visible = true;
        }

        private void IsSpouseExist()
        {
            
            //bool spouseRelationExist = false;
            //bool spouseDobExist = false;
            //bool spouseAssumptionExist = false;
            //customerBo.CheckSpouseRelationship(customerVo.CustomerId, out spouseRelationExist, out spouseDobExist, out spouseAssumptionExist);
            //if (!spouseRelationExist)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('There is no SpouseAssociation!!');", true);
            //}
            ////else if(!spouseDobExist)
            ////{
            ////    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Spouse does not have Date of Birth!!');", true);
            ////}
            //else if (!spouseAssumptionExist)

            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Spouse does not have Assuption!!');", true);
            //else
            //{
                //customerBo.InsertPlanPreferences(customerVo.CustomerId, 6,3);
                //msgRecordStatus.Visible = true;
            //}
           
          
             
           
        }

        protected void btnCalculationBasis_OnClick(object sender, EventArgs e)
        {
            UpdateCalculationBasis();
            //msgRecordStatus.Visible = true;
        }


        //public DataTable AssumptionTableCreation(DataTable dtAssumption)
        //{
        //    DataTable dtFinalAssumption = new DataTable();
        //    dtFinalAssumption.Columns.Add("Year",System.Type.GetType("System.Int32"));
        //    dtFinalAssumption.Columns.Add("Inflation");
        //    dtFinalAssumption.Columns.Add("Equity");
        //    dtFinalAssumption.Columns.Add("Debt");
        //    dtFinalAssumption.Columns.Add("Cash");
        //    dtFinalAssumption.Columns.Add("Alternate");
        //    dtFinalAssumption.Columns.Add("IncomeGrowth");
        //    dtFinalAssumption.Columns.Add("ExpenseGrowth");
        //    dtFinalAssumption.Columns.Add("DiscountRate");
        //    dtFinalAssumption.Columns.Add("PostRetirement");
        //    dtFinalAssumption.Columns.Add("ReturnNewInvestments");
        //    DataRow drFinalAssumption;
        //    int tempYear=0;
        //    string tempAssumption="";
        //    DataRow[] assumptionYearWise;
        //    foreach (DataRow drAssumption in dtAssumption.Rows)
        //    {               
                
        //        if (tempYear != int.Parse(drAssumption["CPA_Year"].ToString()))
        //        {                   
        //            tempYear = int.Parse(drAssumption["CPA_Year"].ToString());
        //            //drFinalAssumption["Year"] = tempYear.ToString();
        //            assumptionYearWise = dtAssumption.Select("CPA_Year="+tempYear.ToString());
        //            drFinalAssumption = dtFinalAssumption.NewRow();
        //            foreach (DataRow dr in assumptionYearWise)
        //            {
        //                tempAssumption = dr["WA_AssumptionId"].ToString();
        //                switch (tempAssumption)
        //                {
        //                    case "AR":
        //                        drFinalAssumption["Alternate"] = double.Parse(dr["CPA_Value"].ToString());
        //                        break;
        //                    case "CR":
        //                        drFinalAssumption["Cash"] = double.Parse(dr["CPA_Value"].ToString());
        //                        break;
        //                    case "DISR":
        //                        drFinalAssumption["DiscountRate"] = double.Parse(dr["CPA_Value"].ToString());
        //                        break;
        //                    case "DR":
        //                        drFinalAssumption["Debt"] = double.Parse(dr["CPA_Value"].ToString());
        //                        break;
        //                    case "EG":
        //                        drFinalAssumption["ExpenseGrowth"] = double.Parse(dr["CPA_Value"].ToString());
        //                        break;
        //                    case "ER":
        //                        drFinalAssumption["Equity"] = double.Parse(dr["CPA_Value"].ToString());
        //                        break;
        //                    case "IG":
        //                        drFinalAssumption["IncomeGrowth"] = double.Parse(dr["CPA_Value"].ToString());
        //                        break;
        //                    case "IR":
        //                        drFinalAssumption["Inflation"] = double.Parse(dr["CPA_Value"].ToString());
        //                        break;
        //                    case "PRT":
        //                        drFinalAssumption["PostRetirement"] = double.Parse(dr["CPA_Value"].ToString());
        //                        break;
        //                    case "RNI":
        //                        drFinalAssumption["ReturnNewInvestments"] = double.Parse(dr["CPA_Value"].ToString());
        //                        break;
        //                }

        //                drFinalAssumption["Year"] = tempYear.ToString();
        //            }
        //            dtFinalAssumption.Rows.Add(drFinalAssumption);
        //        }
        //     }

        //    return dtFinalAssumption;
        //}

        protected void aplToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Value == "Edit")
            {
                ViewState["ActionAssumptionPage"] = "Edit";
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerAssumptionsPreferencesSetup','login');", true);
                SetEditViewMode(true);
            }
        }
        private void SetEditViewMode(bool Bool)
        {
            if (Bool)
            {
                rbtnNo.Enabled = true;
                rbtnYes.Enabled = true;
                //rdbYearRangeWise.Enabled = true;
                rbtnMapping.Enabled = true;
                rbtnConsolidateEntry.Enabled = true;
               // ddlPickPeriod.Enabled = true;                
                btnCalculationBasis.Enabled = true;
                btnGoalFundingPreference.Enabled = true;
              //  btnSubmit.Enabled = true;
                //txtAssumptionValue.Enabled = true;
                //ddlPickAssumtion.Enabled = true;
               // ddlPickYear.Enabled = true;
                //ddlFromYear.Enabled = true;
              //  ddlToYear.Enabled = true;
                
           }
            else
            {
                rbtnNo.Enabled = false;
                rbtnYes.Enabled = false;
                //rdbYearRangeWise.Enabled = false;
                rbtnMapping.Enabled = false;
                rbtnConsolidateEntry.Enabled = false;
              //  ddlPickPeriod.Enabled = false;
                btnCalculationBasis.Enabled = false;
                btnGoalFundingPreference.Enabled = false;
              //  btnSubmit.Enabled = false;
              // txtAssumptionValue.Enabled = false;
               // ddlPickAssumtion.Enabled = false;
              //  ddlPickYear.Enabled = false;
               // ddlFromYear.Enabled = false;
               // ddlToYear.Enabled = false;
                
                
            }

        }
        public void SetDefaultPlanRetirementValueForCustomer()
        {
            int customerId = 0;
            customerId = customerVo.CustomerId;
            int calculationIdRowI=0;
            int calculationIdRowII = 0;
            int calculationBasisIdI=0;
            int calculationBasisIdII = 0;

            DataSet dsSetDefaultPlanRetirementValueForCustomer;
            dsSetDefaultPlanRetirementValueForCustomer=customerBo.SetDefaultPlanRetirementValueForCustomer(customerId);

            if (dsSetDefaultPlanRetirementValueForCustomer.Tables[0].Rows.Count > 0)
            {

                calculationBasisIdI = int.Parse(dsSetDefaultPlanRetirementValueForCustomer.Tables[0].Rows[0]["WFPCB_CalculationBasisId"].ToString());
                calculationIdRowI = int.Parse(dsSetDefaultPlanRetirementValueForCustomer.Tables[0].Rows[0]["WFPCB_CalculationId"].ToString());

                calculationBasisIdII = int.Parse(dsSetDefaultPlanRetirementValueForCustomer.Tables[0].Rows[1]["WFPCB_CalculationBasisId"].ToString());
                calculationIdRowII = int.Parse(dsSetDefaultPlanRetirementValueForCustomer.Tables[0].Rows[1]["WFPCB_CalculationId"].ToString());
                if (calculationIdRowI == 3)
                {
                    if (calculationBasisIdI == 5)
                    {
                        rbtnMapping.Checked = true;
                        rbtnConsolidateEntry.Checked = false;

                    }
                    else
                    {
                        rbtnMapping.Checked = false;
                        rbtnConsolidateEntry.Checked = true;
                    }
                }
                if (calculationIdRowII == 4)
                {
                    if (calculationBasisIdII == 7)
                    {
                        rbtnYes.Checked = true;
                        rbtnNo.Checked = false;
                    }
                    else
                    {
                        rbtnYes.Checked = false;
                        rbtnNo.Checked = true;
                    }
                }
            
            }

            else
            {
                customerBo.InsertPlanPreferences(customerVo.CustomerId, 5, 3);
                customerBo.InsertPlanPreferences(customerVo.CustomerId, 7,4);
                SetDefaultPlanRetirementValueForCustomer();
            }

        }


        public void UpdatePlanPreferences()
        {
            if (rbtnMapping.Checked)
            {
                customerBo.InsertPlanPreferences(customerVo.CustomerId, 5, 3);
                msgRecordStatus.Visible = true;
            }
            if (rbtnConsolidateEntry.Checked)
            {
                customerBo.InsertPlanPreferences(customerVo.CustomerId, 6, 3);
                msgRecordStatus.Visible = true;
                //IsSpouseExist();
            }

        }
        public void UpdateCalculationBasis()
        {
            if (rbtnYes.Checked)
            {
                customerBo.InsertPlanPreferences(customerVo.CustomerId, 7, 4);
                msgRecordStatus.Visible = true;
            }
            if (rbtnNo.Checked)
            {
                customerBo.InsertPlanPreferences(customerVo.CustomerId, 8, 4);
                msgRecordStatus.Visible = true;
            }
        }

        // Commented for adding RadioButtons for Yearly and Static LinkButtons..
        // commented by Vinayak Patil..

        //protected void lnkBtnYearly_Click(object sender, EventArgs e)
        //{
        //    string flag = "P";            
        //    trStaticGrid.Visible = false;
        //    trGridAssumption.Visible = true;
        //    trRbtnYear.Visible = true;
        //    trPickYear.Visible = true;
        //    BindDropDownassumption(flag);            
        //}

        //protected void lnkBtnStatic_Click(object sender, EventArgs e)
        //{
        //    string flag = "S";
        //    trRbtnYear.Visible = false;
        //    trPickYear.Visible = false;
        //    BindDropDownassumption(flag);
        //    trGridAssumption.Visible = false;
        //    trStaticGrid.Visible = true;

        //}

        //protected void rbtnYearly_CheckedChanged(object sender, EventArgs e)
        //{
        //    string flag = "P";
        //    trStaticGrid.Visible = false;
        //    trGridAssumption.Visible = true;
        //    trRbtnYear.Visible = true;
        //    trPickYear.Visible = true;
        //    BindDropDownassumption(flag);
        //}

        //protected void rbtnStatic_CheckedChanged(object sender, EventArgs e)
        //{
        //    string flag = "S";
        //    trRbtnYear.Visible = false;
        //    trPickYear.Visible = false;
        //    BindDropDownassumption(flag);
        //    trGridAssumption.Visible = false;
        //    trStaticGrid.Visible = true;
        //}

        //protected void ddlPickPeriod_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlPickPeriod.SelectedValue == "SY")
        //    {
        //        trPickYear.Visible = true;
        //        trRangeYear.Visible = false;
        //    }
        //    else if (ddlPickPeriod.SelectedValue == "RY")
        //    {
        //        trPickYear.Visible = false;
        //        trRangeYear.Visible = true;
 
        //    }
        //}

        
        //protected void txtAssumptionValue_OnTextChanged(object sender, EventArgs e)
        //{
        //    if (ddlPickAssumtion.SelectedValue == "LE")
        //    {
        //        if (int.Parse(txtAssumptionValue.Text) < decimal.Parse(txtRetirementAge.Text))
        //        {
        //      ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Life Expectancy Should Be More Than Retirement Age');", true);
   
        //        }
                
        //    }
        //    else if (ddlPickAssumtion.SelectedValue == "RA")
        //    {
        //        if (int.Parse(txtAssumptionValue.Text) > decimal.Parse(txtLifeExpectancy.Text))
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Life Expectancy Should Be More Than Retirement Age');", true);
        //    }

        //}

    }
}