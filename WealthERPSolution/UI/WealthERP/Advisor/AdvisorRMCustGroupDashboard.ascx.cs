using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using VoCustomerProfiling;
using VoUser;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using BoUser;
using VoAlerts;
using BoAlerts;
using System.Data;
using System.Collections.Specialized;
using WealthERP.Base;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class AdvisorRMCustGroupDashboard : System.Web.UI.UserControl
    {
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        CustomerFamilyVo customerFamilyVo;
        List<CustomerFamilyVo> customerFamilyList = null;
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = null;
        CustomerVo customerMemberVo = null;
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        AlertsBo alertsBo = new AlertsBo();
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo = new UserVo();
        AssetBo assetBo = new AssetBo();
        InsuranceBo insuranceBo = new InsuranceBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        DataSet dsCustomerAssetMaturityDates = new DataSet();
        DataSet dsAssetAggrCurrentValues = new DataSet();
        DataSet dsGrpAssetNetHoldings = new DataSet();
        DataSet dsCustomerAlerts = new DataSet();
        DataSet dsInsuranceDetails = new DataSet();
        DataTable dtGrpAssetNetHoldings = new DataTable();
        DataRow drMaturityDates;
        DataRow drNetHoldings;
        DataRow drCustomerAlerts;
        DataRow drLifeInsurance;
        DataRow drGeneralInsurance;
        int customerId;
        int portfolioId;
        int memberCustomerId;
        int userId;
        string metatablePrimaryKey;
        double sum = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session[SessionContents.UserVo];
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                rmVo = (RMVo)Session[SessionContents.RmVo];
                userId = userVo.UserId;
                customerId = customerVo.CustomerId;
                customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerId);
                portfolioId = customerPortfolioVo.PortfolioId;
                Session[SessionContents.PortfolioId] = portfolioId;
                lblMessage.Visible = false;
                //trlblerrormsg.Visible = false;
                lblMaturityMsg.Visible = false;
                lblLifeInsurance.Visible = false;
                lblGeneralInsurance.Visible = false;
                BindCustomerFamilyGrid();
                BindAssetInvestments();
                BindAssetCurrentValChart();
                BindGroupInsuranceDetails();
                BindCustomerAssetMaturityDates();
                BindCustomerAlerts();
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:Page_Load()");


                object[] objects = new object[10];

                objects[0] = customerVo;
                objects[1] = userVo;
                objects[2] = customerMemberVo;
                objects[3] = rmVo;
                objects[4] = customerFamilyVo;
                objects[5] = customerFamilyList;
                objects[6] = customerPortfolioVo;
                objects[7] = userId;
                objects[8] = customerId;
                objects[9] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }

        //Function to populate the Customer Family Member details in the Grid
        public void BindCustomerFamilyGrid()
        {
            DataTable dtCustomerFamily = new DataTable();
            DataRow drCustomerFamily;
            try
            {
                customerFamilyList = customerFamilyBo.GetCustomerFamily(customerId);
                if (customerFamilyList == null)
                {
                    lblMessage.Visible = true;
                    //trlblerrormsg.Visible = true;
                    lblFamilyMembersNum.Text = "0";
                }
                else
                {
                    lblMessage.Visible = false;
                    //trlblerrormsg.Visible = false;
                    dtCustomerFamily.Columns.Add("CustomerId");
                    dtCustomerFamily.Columns.Add("Member Name");
                    dtCustomerFamily.Columns.Add("Relationship");
                    lblFamilyMembersNum.Text = customerFamilyList.Count.ToString();


                    for (int i = 0; i < customerFamilyList.Count; i++)
                    {
                        drCustomerFamily = dtCustomerFamily.NewRow();

                        customerFamilyVo = customerFamilyList[i];
                        memberCustomerId = customerFamilyVo.AssociateCustomerId;
                        customerMemberVo = customerBo.GetCustomer(memberCustomerId);
                        drCustomerFamily[0] = customerFamilyVo.AssociateCustomerId;
                        drCustomerFamily[1] = customerMemberVo.FirstName.ToString() + " " + customerMemberVo.MiddleName.ToString() + " " + customerMemberVo.LastName.ToString();
                        drCustomerFamily[2] = customerFamilyVo.Relationship.ToString();

                        dtCustomerFamily.Rows.Add(drCustomerFamily);

                    }

                    gvCustomerFamily.DataSource = dtCustomerFamily;
                    gvCustomerFamily.DataBind();
                    gvCustomerFamily.Visible = true;
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

                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:BindCustomerFamilyGrid()");


                object[] objects = new object[4];

                objects[0] = customerVo;
                objects[2] = customerMemberVo;
                objects[4] = customerFamilyVo;
                objects[5] = customerFamilyList;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        //function to populate the maturity dates in the grid
        public void BindCustomerAssetMaturityDates()
        {
            DataTable dtMaturityDates = new DataTable();
            try
            {
                dsCustomerAssetMaturityDates = assetBo.GetGrpAssetMaturityDates(customerId);
                if (dsCustomerAssetMaturityDates.Tables[0].Rows.Count == 0)
                {
                    lblMaturityMsg.Visible = true;
                }
                else
                {
                    dtMaturityDates.Columns.Add("Customer Name");
                    dtMaturityDates.Columns.Add("Asset Group");
                    dtMaturityDates.Columns.Add("Asset Particulars");
                    dtMaturityDates.Columns.Add("Maturity Date");
                    dtMaturityDates.Columns.Add("CustomerId");

                    foreach (DataRow dr in dsCustomerAssetMaturityDates.Tables[0].Rows)
                    {
                        drMaturityDates = dtMaturityDates.NewRow();

                        drMaturityDates[0] = dr["CustomerName"].ToString();
                        drMaturityDates[1] = dr["AssetGroup"].ToString();
                        if (dr["AssetParticulars"].ToString() != "")
                            drMaturityDates[2] = dr["AssetParticulars"].ToString();
                        else
                            drMaturityDates[2] = "N/A";
                        if (dr["MaturityDate"].ToString() != "")
                            drMaturityDates[3] = (DateTime.Parse(dr["MaturityDate"].ToString())).ToShortDateString();
                        else
                            drMaturityDates[3] = "N/A";
                        drMaturityDates[4] = dr["CustomerId"].ToString();

                        dtMaturityDates.Rows.Add(drMaturityDates);
                    }
                    gvMaturitySchedule.DataSource = dtMaturityDates;
                    gvMaturitySchedule.DataBind();
                    gvMaturitySchedule.Visible = true;
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

                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:BindCustomerAssetMaturityDates()");


                object[] objects = new object[2];

                objects[0] = portfolioId;
                objects[1] = dsCustomerAssetMaturityDates;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        //function to populate the Life Insurance and General Insurance Grids  
        public void BindGroupInsuranceDetails()
        {
            DataTable dtLifeInsDetails = new DataTable();
            DataTable dtGenInsDetails = new DataTable();
            try
            {
                //Binding the Life Insurance Gid
                dsInsuranceDetails = insuranceBo.GetGrpInsuranceDetails(customerId);
                if (dsInsuranceDetails.Tables[0].Rows.Count == 0)
                {
                    lblLifeInsurance.Visible = true;
                }
                else
                {
                    dtLifeInsDetails.Columns.Add("CustomerName");
                    dtLifeInsDetails.Columns.Add("Policy");
                    dtLifeInsDetails.Columns.Add("InsuranceType");
                    dtLifeInsDetails.Columns.Add("SumAssured");
                    dtLifeInsDetails.Columns.Add("PremiumAmount");
                    dtLifeInsDetails.Columns.Add("PremiumFrequency");
                    dtLifeInsDetails.Columns.Add("CustomerId");

                    foreach (DataRow dr in dsInsuranceDetails.Tables[0].Rows)
                    {
                        drLifeInsurance = dtLifeInsDetails.NewRow();

                        drLifeInsurance[0] = dr["CustomerName"].ToString();
                        drLifeInsurance[1] = dr["Policy"].ToString();
                        drLifeInsurance[2] = dr["InsuranceType"].ToString();
                        drLifeInsurance[3] = String.Format("{0:n2}", decimal.Parse(dr["SumAssured"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drLifeInsurance[4] = String.Format("{0:n2}", decimal.Parse(dr["PremiumAmount"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))); 
                        drLifeInsurance[5] = dr["PremiumFrequency"].ToString();
                        drLifeInsurance[6] = dr["CustomerId"].ToString();

                        dtLifeInsDetails.Rows.Add(drLifeInsurance);
                    }
                    gvLifeInsurance.DataSource = dtLifeInsDetails;
                    gvLifeInsurance.DataBind();
                    gvLifeInsurance.Visible = true;

                    //Binding the General Insurance Gid
                    if (dsInsuranceDetails.Tables[1].Rows.Count == 0)
                    {
                        lblGeneralInsurance.Visible = true;
                    }
                    else
                    {
                        dtGenInsDetails.Columns.Add("CustomerName");
                        dtGenInsDetails.Columns.Add("PolicyIssuer");
                        dtGenInsDetails.Columns.Add("InsuranceType");
                        dtGenInsDetails.Columns.Add("SumAssured");
                        dtGenInsDetails.Columns.Add("PremiumAmount");
                        dtGenInsDetails.Columns.Add("CustomerId");

                        foreach (DataRow dr in dsInsuranceDetails.Tables[1].Rows)
                        {
                            drGeneralInsurance = dtGenInsDetails.NewRow();

                            drGeneralInsurance[0] = dr["CustomerName"].ToString();
                            drGeneralInsurance[1] = dr["PolicyIssuer"].ToString();
                            drGeneralInsurance[2] = dr["InsuranceType"].ToString();
                            drGeneralInsurance[3] = String.Format("{0:n2}", decimal.Parse(dr["SumAssured"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            drGeneralInsurance[4] = String.Format("{0:n2}", decimal.Parse(dr["PremiumAmount"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            drGeneralInsurance[5] = dr["CustomerId"].ToString();

                            dtGenInsDetails.Rows.Add(drGeneralInsurance);
                        }
                        gvGeneralInsurance.DataSource = dtGenInsDetails;
                        gvGeneralInsurance.DataBind();
                        gvGeneralInsurance.Visible = true;
                    }
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

                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:BindGroupInsuranceDetails()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        /// <summary>
        ///function to populate the Asset Class wise Investments in the grid 
        /// </summary>
        public void BindAssetInvestments()
        {

            int tempCustId = 0;
            int i=0;
            try
            {
                dsGrpAssetNetHoldings = assetBo.GetGrpAssetNetHoldings(customerId);
                
                //liabilityValue = assetBo.GetCustomerPortfolioLiability(portfolioId);
                if (dsGrpAssetNetHoldings.Tables[0].Rows.Count == 0)
                {
                    lblAssetDetailsMsg.Visible = true;
                }
                else
                {
                    dsGrpAssetNetHoldings.Tables[0].DefaultView.Sort = "CustomerName ASC";
                    
                    lblAssetDetailsMsg.Visible = false;
                    DataTable dt = dsGrpAssetNetHoldings.Tables[0].DefaultView.Table;

                    dtGrpAssetNetHoldings.Columns.Add("Customer_Name");
                    dtGrpAssetNetHoldings.Columns.Add("Equity");
                    dtGrpAssetNetHoldings.Columns.Add("Mutual_Fund");
                    dtGrpAssetNetHoldings.Columns.Add("Fixed_Income");
                    dtGrpAssetNetHoldings.Columns.Add("Government_Savings");
                    dtGrpAssetNetHoldings.Columns.Add("Property");
                    dtGrpAssetNetHoldings.Columns.Add("Pension_and_Gratuity");
                    dtGrpAssetNetHoldings.Columns.Add("Personal_Assets");
                    dtGrpAssetNetHoldings.Columns.Add("Gold_Assets");
                    dtGrpAssetNetHoldings.Columns.Add("Collectibles");
                    dtGrpAssetNetHoldings.Columns.Add("Cash_and_Savings");
                    dtGrpAssetNetHoldings.Columns.Add("Assets_Total");
                    dtGrpAssetNetHoldings.Columns.Add("Liabilities_Total");
                    dtGrpAssetNetHoldings.Columns.Add("Net_Worth");
                    dtGrpAssetNetHoldings.Columns.Add("CustomerId");

                    dtGrpAssetNetHoldings.Columns["Equity"].DefaultValue = "0";
                    dtGrpAssetNetHoldings.Columns["Mutual_Fund"].DefaultValue = "0";
                    dtGrpAssetNetHoldings.Columns["Fixed_Income"].DefaultValue = "0";
                    dtGrpAssetNetHoldings.Columns["Government_Savings"].DefaultValue = "0";
                    dtGrpAssetNetHoldings.Columns["Property"].DefaultValue = "0";
                    dtGrpAssetNetHoldings.Columns["Pension_and_Gratuity"].DefaultValue = "0";
                    dtGrpAssetNetHoldings.Columns["Personal_Assets"].DefaultValue = "0";
                    dtGrpAssetNetHoldings.Columns["Gold_Assets"].DefaultValue = "0";
                    dtGrpAssetNetHoldings.Columns["Collectibles"].DefaultValue = "0";
                    dtGrpAssetNetHoldings.Columns["Cash_and_Savings"].DefaultValue = "0";
                    dtGrpAssetNetHoldings.Columns["Assets_Total"].DefaultValue = "0";
                    dtGrpAssetNetHoldings.Columns["Liabilities_Total"].DefaultValue = "0";
                    dtGrpAssetNetHoldings.Columns["Net_Worth"].DefaultValue = "0";


                    foreach (DataRow dr in dsGrpAssetNetHoldings.Tables[0].DefaultView.Table.Rows)
                    {
                        i++;
                        if (int.Parse(dr["CustomerId"].ToString()) != tempCustId)
                        {
                            if (tempCustId != 0)
                            {
                                drNetHoldings[11] = String.Format("{0:n2}",(double.Parse(drNetHoldings[1].ToString()) + double.Parse(drNetHoldings[2].ToString()) +
                                    double.Parse(drNetHoldings[3].ToString()) + double.Parse(drNetHoldings[4].ToString()) + double.Parse(drNetHoldings[5].ToString()) +
                                    double.Parse(drNetHoldings[6].ToString()) + double.Parse(drNetHoldings[7].ToString()) + double.Parse(drNetHoldings[8].ToString()) +
                                    double.Parse(drNetHoldings[9].ToString()) + double.Parse(drNetHoldings[10].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                                drNetHoldings[13] = String.Format("{0:n2}", (double.Parse(drNetHoldings[11].ToString()) - double.Parse(drNetHoldings[12].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))); 
                                dtGrpAssetNetHoldings.Rows.Add(drNetHoldings);
                            }
                            tempCustId = int.Parse(dr["CustomerId"].ToString());
                            drNetHoldings = dtGrpAssetNetHoldings.NewRow();
                            drNetHoldings[0] = dr["CustomerName"].ToString();
                            drNetHoldings[14] = dr["CustomerId"].ToString();
                            if (dr["AssetType"].ToString() == "Equity")
                                drNetHoldings[1] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Mutual Fund")
                                drNetHoldings[2] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Fixed Income")
                                drNetHoldings[3] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Government Savings")
                                drNetHoldings[4] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Property")
                                drNetHoldings[5] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Pension and Gratuities")
                                drNetHoldings[6] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Personal Assets")
                                drNetHoldings[7] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Gold Assets")
                                drNetHoldings[8] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Collectibles")
                                drNetHoldings[9] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Cash and Savings")
                                drNetHoldings[10] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Liabilities")
                                drNetHoldings[12] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            if (i == dsGrpAssetNetHoldings.Tables[0].Rows.Count)
                            {
                                drNetHoldings[11] = double.Parse(drNetHoldings[1].ToString()) + double.Parse(drNetHoldings[2].ToString()) +
                                    double.Parse(drNetHoldings[3].ToString()) + double.Parse(drNetHoldings[4].ToString()) + double.Parse(drNetHoldings[5].ToString()) +
                                    double.Parse(drNetHoldings[6].ToString()) + double.Parse(drNetHoldings[7].ToString()) + double.Parse(drNetHoldings[8].ToString()) +
                                    double.Parse(drNetHoldings[9].ToString()) + double.Parse(drNetHoldings[10].ToString());
                                drNetHoldings[13] = double.Parse(drNetHoldings[11].ToString()) - double.Parse(drNetHoldings[12].ToString());
                                dtGrpAssetNetHoldings.Rows.Add(drNetHoldings);
                            }
                        }
                        else
                        {
                            if (dr["AssetType"].ToString() == "Equity")
                                drNetHoldings[1] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Mutual Fund")
                                drNetHoldings[2] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Fixed Income")
                                drNetHoldings[3] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Government Savings")
                                drNetHoldings[4] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Property")
                                drNetHoldings[5] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Pension and Gratuities")
                                drNetHoldings[6] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Personal Assets")
                                drNetHoldings[7] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Gold Assets")
                                drNetHoldings[8] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Collectibles")
                                drNetHoldings[9] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Cash and Savings")
                                drNetHoldings[10] = String.Format("{0:n2}",double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            else if (dr["AssetType"].ToString() == "Liabilities")
                                drNetHoldings[12] = String.Format("{0:n2}", double.Parse(dr["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                            if (i == dsGrpAssetNetHoldings.Tables[0].Rows.Count)
                            {
                                drNetHoldings[11] = double.Parse(drNetHoldings[1].ToString()) + double.Parse(drNetHoldings[2].ToString()) +
                                    double.Parse(drNetHoldings[3].ToString()) + double.Parse(drNetHoldings[4].ToString()) + double.Parse(drNetHoldings[5].ToString()) +
                                    double.Parse(drNetHoldings[6].ToString()) + double.Parse(drNetHoldings[7].ToString()) + double.Parse(drNetHoldings[8].ToString()) +
                                    double.Parse(drNetHoldings[9].ToString()) + double.Parse(drNetHoldings[10].ToString());
                                drNetHoldings[13] = double.Parse(drNetHoldings[11].ToString()) - double.Parse(drNetHoldings[12].ToString());
                                dtGrpAssetNetHoldings.Rows.Add(drNetHoldings);
                            }
                        }
                    }

                    drNetHoldings = dtGrpAssetNetHoldings.NewRow();  //DataRow which holds the total of all columns to show in the footer
                    drNetHoldings[0] = "Total :";
                    foreach (DataRow dr in dtGrpAssetNetHoldings.Rows)
                    {
                        drNetHoldings[1] = String.Format("{0:n2}", (double.Parse(drNetHoldings[1].ToString()) + double.Parse(dr[1].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drNetHoldings[2] = String.Format("{0:n2}", (double.Parse(drNetHoldings[2].ToString()) + double.Parse(dr[2].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drNetHoldings[3] = String.Format("{0:n2}", (double.Parse(drNetHoldings[3].ToString()) + double.Parse(dr[3].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drNetHoldings[4] = String.Format("{0:n2}", (double.Parse(drNetHoldings[4].ToString()) + double.Parse(dr[4].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drNetHoldings[5] = String.Format("{0:n2}", (double.Parse(drNetHoldings[5].ToString()) + double.Parse(dr[5].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drNetHoldings[6] = String.Format("{0:n2}", (double.Parse(drNetHoldings[6].ToString()) + double.Parse(dr[6].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drNetHoldings[7] = String.Format("{0:n2}", (double.Parse(drNetHoldings[7].ToString()) + double.Parse(dr[7].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drNetHoldings[8] = String.Format("{0:n2}", (double.Parse(drNetHoldings[8].ToString()) + double.Parse(dr[8].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drNetHoldings[9] = String.Format("{0:n2}", (double.Parse(drNetHoldings[9].ToString()) + double.Parse(dr[9].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drNetHoldings[10] = String.Format("{0:n2}", (double.Parse(drNetHoldings[10].ToString()) + double.Parse(dr[10].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drNetHoldings[11] = String.Format("{0:n2}", (double.Parse(drNetHoldings[11].ToString()) + double.Parse(dr[11].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drNetHoldings[12] = String.Format("{0:n2}", (double.Parse(drNetHoldings[12].ToString()) + double.Parse(dr[12].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drNetHoldings[13] = String.Format("{0:n2}", (double.Parse(drNetHoldings[13].ToString()) + double.Parse(dr[13].ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    }
                    drNetHoldings[14] = "0";

                    gvAssetAggrCurrentValue.DataSource = dtGrpAssetNetHoldings;
                    gvAssetAggrCurrentValue.DataBind();
                    gvAssetAggrCurrentValue.Visible = true;
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
                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:BindAssetInvestments()");
                object[] objects = new object[2];
                objects[0] = portfolioId;
                objects[1] = dsGrpAssetNetHoldings;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        /// <summary>
        /// function to assign the data to the chart
        /// </summary>
        public void BindAssetCurrentValChart()
        {
            Series seriesAssets = null;
            Legend legend = null;
            int i = 0;
            int cnt = 0;
            string[] XValues = null;
            double[] YValues = null;
            try
            {

                // Total Assets Chart
                seriesAssets = new Series("Assets");
                legend = new Legend("AssetsLegend");
                legend.Enabled = true;
                XValues = new string[10];
                YValues = new double[10];

                seriesAssets.ChartType = SeriesChartType.Pie;

                cnt = dtGrpAssetNetHoldings.Rows.Count - 1;
                DataRow dr = dtGrpAssetNetHoldings.NewRow();
                if (dtGrpAssetNetHoldings.Rows.Count > 0)
                {
                    for (int j = 0; j < dtGrpAssetNetHoldings.Rows.Count; j++)
                    {
                        if (j == 0)
                        {
                            dr = dtGrpAssetNetHoldings.Rows[j];
                        }
                        else
                        {
                            dr[1] = (double.Parse(dr[1].ToString()) + double.Parse(dtGrpAssetNetHoldings.Rows[j][1].ToString())).ToString();
                            dr[2] = (double.Parse(dr[2].ToString()) + double.Parse(dtGrpAssetNetHoldings.Rows[j][2].ToString())).ToString();
                            dr[3] = (double.Parse(dr[3].ToString()) + double.Parse(dtGrpAssetNetHoldings.Rows[j][3].ToString())).ToString();
                            dr[4] = (double.Parse(dr[4].ToString()) + double.Parse(dtGrpAssetNetHoldings.Rows[j][4].ToString())).ToString();
                            dr[5] = (double.Parse(dr[5].ToString()) + double.Parse(dtGrpAssetNetHoldings.Rows[j][5].ToString())).ToString();
                            dr[6] = (double.Parse(dr[6].ToString()) + double.Parse(dtGrpAssetNetHoldings.Rows[j][6].ToString())).ToString();
                            dr[7] = (double.Parse(dr[7].ToString()) + double.Parse(dtGrpAssetNetHoldings.Rows[j][7].ToString())).ToString();
                            dr[8] = (double.Parse(dr[8].ToString()) + double.Parse(dtGrpAssetNetHoldings.Rows[j][8].ToString())).ToString();
                            dr[9] = (double.Parse(dr[9].ToString()) + double.Parse(dtGrpAssetNetHoldings.Rows[j][9].ToString())).ToString();
                            dr[10] = (double.Parse(dr[10].ToString()) + double.Parse(dtGrpAssetNetHoldings.Rows[j][10].ToString())).ToString();


                        }
                    }
                    for (i = 1; i <= 10; i++)
                    {
                        XValues[i - 1] = dtGrpAssetNetHoldings.Columns[i].ColumnName;
                        YValues[i - 1] = double.Parse(dr[i].ToString());
                    }
                }

                seriesAssets.Points.DataBindXY(XValues, YValues);
                //Chart1.DataSource = dsAssetAggrCurrentValues.Tables[0].DefaultView;
                Chart1.Series.Clear();
                Chart1.Series.Add(seriesAssets);

                Chart1.Palette = ChartColorPalette.Pastel;
                Chart1.PaletteCustomColors = new Color[] { Color.LimeGreen, Color.Yellow, Color.LightBlue, Color.Purple, Color.Goldenrod, Color.Blue, Color.BurlyWood,
                                                                          Color.Chocolate, Color.DeepPink, Color.Plum, Color.Violet, Color.Gainsboro, Color.Tomato, Color.Teal};
                //Chart1.Series[0].XValueMember = "AssetType";
                //Chart1.Series[0].YValueMembers = "AggrCurrentValue";
                Chart1.Legends.Add(legend);

                Chart1.Series[0]["CollectedSliceExploded"] = "true";
                Chart1.Legends["AssetsLegend"].Title = "Assets";
                Chart1.Legends["AssetsLegend"].TitleAlignment = StringAlignment.Center;
                Chart1.Legends["AssetsLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
                Chart1.Legends["AssetsLegend"].TitleSeparatorColor = Color.Black;
                //Chart1.Legends["AssetsLegend"].BackColor = Color.Transparent;
                Chart1.Series[0].IsValueShownAsLabel = false;
                Chart1.Series[0]["PieLabelStyle"] = "Disabled";
                Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                Chart1.ChartAreas[0].Area3DStyle.Perspective = 50;
                Chart1.Series[0].ToolTip = "#VALX: #PERCENT";
                //Chart1.Series[0].Label = "#PERCENT";
                //Chart1.Series[0]["CollectedLegendText"] = "Other";
                Chart1.BackColor = Color.Transparent;
                Chart1.ChartAreas[0].BackColor = Color.Transparent;

                LegendCellColumn colorColumn = new LegendCellColumn();
                colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                colorColumn.HeaderText = "Color";
                colorColumn.HeaderBackColor = Color.WhiteSmoke;
                Chart1.Legends["AssetsLegend"].CellColumns.Add(colorColumn);

                LegendCellColumn asset = new LegendCellColumn();
                asset.ColumnType = LegendCellColumnType.Text;
                asset.HeaderText = "Asset";
                asset.Alignment = ContentAlignment.TopLeft;
                asset.Text = "#VALX";
                asset.HeaderBackColor = Color.WhiteSmoke;
                Chart1.Legends["AssetsLegend"].CellColumns.Add(asset);


                LegendCellColumn assetPercent = new LegendCellColumn();
                assetPercent.Alignment = ContentAlignment.MiddleLeft;
                assetPercent.Text = "#PERCENT";
                assetPercent.HeaderText = "% Of Assets";
                assetPercent.Name = "PerformanceColumn";
                assetPercent.HeaderBackColor = Color.WhiteSmoke;
                Chart1.Legends["AssetsLegend"].CellColumns.Add(assetPercent);


                foreach (DataPoint point in Chart1.Series[0].Points)
                {
                    point["Exploded"] = "true";
                }

                Chart1.DataBind();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:BindAssetCurrentValChart()");
                object[] objects = new object[4];
                objects[0] = portfolioId;
                objects[1] = dsAssetAggrCurrentValues;
                objects[2] = XValues;
                objects[3] = YValues;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        /// <summary>
        /// function to populate the Customer Alerts grid 
        /// </summary>
        public void BindCustomerAlerts()
        {
            try
            {
                dsCustomerAlerts = alertsBo.GetCustomerGrpDashboardAlerts(customerId);
                if (dsCustomerAlerts.Tables[0].Rows.Count == 0)
                {
                    lblAlertsMessage.Visible = true;
                }
                else
                {
                    lblAlertsMessage.Visible = false;
                    DataTable dtCustomerAlerts = new DataTable();
                    dtCustomerAlerts.Columns.Add("CustomerName");
                    dtCustomerAlerts.Columns.Add("Details");
                    dtCustomerAlerts.Columns.Add("EventMessage");
                    dtCustomerAlerts.Columns.Add("PopulatedDate");
                    dtCustomerAlerts.Columns.Add("CustomerId");

                    foreach (DataRow dr in dsCustomerAlerts.Tables[0].Rows)
                    {
                        drCustomerAlerts = dtCustomerAlerts.NewRow();

                        drCustomerAlerts[0] = dr["CustomerName"].ToString();
                        drCustomerAlerts[1] = dr["EventCode"].ToString() + " : " + dr["Name"].ToString();
                        drCustomerAlerts[2] = dr["EventMessage"].ToString();
                        drCustomerAlerts[3] = DateTime.Parse(dr["PopulatedDate"].ToString()).ToShortDateString();
                        drCustomerAlerts[4] = dr["CustomerId"].ToString();
                        dtCustomerAlerts.Rows.Add(drCustomerAlerts);

                    }
                    gvCustomerAlerts.DataSource = dtCustomerAlerts;
                    gvCustomerAlerts.DataBind();
                    gvCustomerAlerts.Visible = true;
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

                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:BindCustomerAlerts()");


                object[] objects = new object[2];

                objects[0] = customerId;
                objects[1] = dsCustomerAlerts;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        /// <summary>
        /// Function to get the Alert scheme name
        /// </summary>
        /// <param name="alertType"></param>
        /// <param name="SchemeID"></param>
        /// <returns></returns>
        protected string GetSchemeName(string alertType, int SchemeID)
        {
            string schemeName = "";

            DataSet dsmetatableDetails = null;
            DataSet dsSchemeName = null;
            string tableName = "";
            string description = "";


            try
            {
                if (alertType == "Property")
                {
                    metatablePrimaryKey = "CPNP_PropertyNPId";
                }
                else if (alertType == "SIP" || alertType == "SWP" || alertType == "STP")
                {
                    metatablePrimaryKey = "PASP_SchemePlanCode";
                }
                else if (alertType == "Personal")
                {
                    metatablePrimaryKey = "CPNP_PersonalNPId";
                }
                dsmetatableDetails = alertsBo.GetMetatableDetails(metatablePrimaryKey);
                tableName = dsmetatableDetails.Tables[0].Rows[0][2].ToString();
                description = dsmetatableDetails.Tables[0].Rows[0][1].ToString();

                dsSchemeName = alertsBo.GetSchemeDescription(description, tableName, metatablePrimaryKey, SchemeID);

                if (dsSchemeName.Tables[0].Rows.Count > 0)
                {
                    schemeName = dsSchemeName.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    schemeName = "N/A";
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
                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:GetSchemeName()");
                object[] objects = new object[9];
                objects[0] = customerId;
                objects[1] = dsCustomerAlerts;
                objects[2] = alertType;
                objects[3] = SchemeID;
                objects[4] = dsmetatableDetails;
                objects[5] = dsSchemeName;
                objects[6] = tableName;
                objects[7] = description;
                objects[8] = schemeName;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return schemeName;
        }

        /// <summary>
        /// Goes to the Customer Dashboard when we click on the Member name on the Customer Family Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkCustomerNameFamilyGrid_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
            int rowIndex = gvRow.RowIndex;
            DataKey dk = gvCustomerFamily.DataKeys[rowIndex];
            int customerId = Convert.ToInt32(dk.Value);

            customerVo = customerBo.GetCustomer(customerId);
            Session["CustomerVo"] = customerVo;
            Session["IsDashboard"] = "CustDashboard";

            if (Session["S_CurrentUserRole"] == "Customer")
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrolCustomer('AdvisorRMCustIndiDashboard','none');", true);
            else
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AdvisorRMCustIndiDashboard','none');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
        }

        /// <summary>
        /// Goes to the Customer Dashboard when we click on the Member name on the Maturity Schedule Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkCustomerNameMaturityDatesGrid_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
            int rowIndex = gvRow.RowIndex;
            DataKey dk = gvMaturitySchedule.DataKeys[rowIndex];
            int customerId = Convert.ToInt32(dk.Value);

            customerVo = customerBo.GetCustomer(customerId);
            Session["CustomerVo"] = customerVo;

            if (Session["S_CurrentUserRole"] == "Customer")
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrolCustomer('AdvisorRMCustIndiDashboard','none');", true);
            else
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('AdvisorRMCustIndiDashboard','none');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
        }

        /// <summary>
        /// Goes to the Portfolio Dashboard when we click on the Member name on the Assets Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkCustomerNameAssetsGrid_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
            int rowIndex = gvRow.RowIndex;
            DataKey dk = gvAssetAggrCurrentValue.DataKeys[rowIndex];
            int customerId = Convert.ToInt32(dk.Value);

            customerVo = customerBo.GetCustomer(customerId);
            Session["CustomerVo"] = customerVo;

            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerId);
            Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;

            if (Session["S_CurrentUserRole"] == "Customer")
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrolCustomer('PortfolioDashboard','none');", true);
            else
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioDashboard','none');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
        }

        /// <summary>
        /// Goes to the Alerts Dashboard when we click on the Member name on the Alerts Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkCustomerNameAlertsGrid_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
            int rowIndex = gvRow.RowIndex;
            DataKey dk = gvCustomerAlerts.DataKeys[rowIndex];
            int customerId = Convert.ToInt32(dk.Value);

            customerVo = customerBo.GetCustomer(customerId);
            Session["CustomerVo"] = customerVo;

            if (Session["S_CurrentUserRole"] == "Customer")
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrolCustomer('RMAlertNotifications','none');", true);
            else
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMAlertNotifications','none');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
        }

        /// <summary>
        /// Goes to the Life Insurance Dashboard when we click on the Member name on the Life Insurance Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkCustomerNameLifeInsuranceGrid_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
            int rowIndex = gvRow.RowIndex;
            DataKey dk = gvLifeInsurance.DataKeys[rowIndex];
            int customerId = Convert.ToInt32(dk.Value);

            customerVo = customerBo.GetCustomer(customerId);
            Session["CustomerVo"] = customerVo;

            if (Session["S_CurrentUserRole"] == "Customer")
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrolCustomer('ViewInsuranceDetails','none');", true);
            else
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewInsuranceDetails','none');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
        }

        /// <summary>
        /// Goes to the General Insurance Dashboard when we click on the Member name on the General Insurance Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkCustomerNameGeneralInsuranceGrid_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
            int rowIndex = gvRow.RowIndex;
            DataKey dk = gvGeneralInsurance.DataKeys[rowIndex];
            int customerId = Convert.ToInt32(dk.Value);

            customerVo = customerBo.GetCustomer(customerId);
            Session["CustomerVo"] = customerVo;

            if (Session["S_CurrentUserRole"] == "Customer")
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrolCustomer('ViewGeneralInsuranceDetails','none');", true);
            else
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewGeneralInsuranceDetails','none');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RMCustomerLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
        }

        protected void gvAssetAggrCurrentValue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = drNetHoldings[0].ToString();
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].Text = drNetHoldings[1].ToString();
                e.Row.Cells[2].Text = drNetHoldings[2].ToString();
                e.Row.Cells[3].Text = drNetHoldings[3].ToString();
                e.Row.Cells[4].Text = drNetHoldings[4].ToString();
                e.Row.Cells[5].Text = drNetHoldings[5].ToString();
                e.Row.Cells[6].Text = drNetHoldings[6].ToString();
                e.Row.Cells[7].Text = drNetHoldings[7].ToString();
                e.Row.Cells[8].Text = drNetHoldings[8].ToString();
                e.Row.Cells[9].Text = drNetHoldings[9].ToString();
                e.Row.Cells[10].Text = drNetHoldings[10].ToString();
                e.Row.Cells[11].Text = drNetHoldings[11].ToString();
                e.Row.Cells[12].Text = drNetHoldings[12].ToString();
                e.Row.Cells[13].Text = drNetHoldings[13].ToString();
            }
        }
    }
}
