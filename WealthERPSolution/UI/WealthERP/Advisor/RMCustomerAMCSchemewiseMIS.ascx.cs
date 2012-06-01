using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Configuration;
using BoCustomerPortfolio;
using BoAdvisorProfiling;
using VoUser;
using BoUser;
using WealthERP.Base;
using BoCommon;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using BoUploads;

namespace WealthERP.Advisor
{
    public partial class RMCustomerAMCSchemewiseMIS : System.Web.UI.UserControl
    {
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        string userType;
        int advisorId;
        int userId;
        int customerId=0;
        int schemeplanid;
        DateTime ValuationDate;
        DateTime LatestValuationdate = new DateTime();
        bool AllPageExport = false;
        bool GridViewCultureFlag = true;
        AdvisorMISBo adviserMISBo = new AdvisorMISBo();
        DataSet dsMISReport;
        int Count;
        int AdviserID;
        int rmId, branchId;
        int CurrentPage;
        DateTime Valuationdate;
        int rmid;

        string BranchSelection = string.Empty;
        string RMSelection = string.Empty;

        UserVo userVo = new UserVo();
        int bmID;
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();

        protected override void OnInit(EventArgs e)
        {
            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            GetPageCount();
            int schemeplancode = int.Parse(ViewState["schemeplanid"].ToString());
            DateTime valdate = Convert.ToDateTime(ViewState["Valuationdate"].ToString());
            this.bindgrid(valdate, schemeplanid);
        }

        private void GetPageCount()
        {
            string upperlimit;
            string lowerlimit;
            int rowCount = 0;
            if (hdnRecordCount.Value != "")
                rowCount = Convert.ToInt32(hdnRecordCount.Value);
            if (rowCount > 0)
            {

                int ratio = rowCount / 10;
                mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                lowerlimit = (((mypager.CurrentPage - 1) * 10) + 1).ToString();
                upperlimit = (mypager.CurrentPage * 10).ToString();
                if (mypager.CurrentPage == mypager.PageCount)
                    upperlimit = hdnRecordCount.Value;
                string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                lblCurrentPage.Text = PageRecords;

                hdnCurrentPage.Value = mypager.CurrentPage.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userType = Session["UserType"].ToString().ToLower();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            rmid = rmVo.RMId;
            trMessage.Visible = false;
            schemeplanid = 0;
            //trModalPopup.Visible = false;
            //ValuationDate = DateTime.Now.Date;

            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
            bmID = rmVo.RMId;
            AdviserID = advisorVo.advisorId;


            if (!IsPostBack)
            {
                PortfolioBo portfoliobo = new PortfolioBo();
                string valuedate = Convert.ToString(portfoliobo.GetLatestValuationDate(advisorVo.advisorId, "MF"));
                hdnValuationDate.Value = valuedate.ToString();
                if (hdnValuationDate.Value == string.Empty)
                {
                    ValuationNotDoneErrorMsg.Visible = true;
                    if (userType == "adviser")
                    {
                        BindBranchDropDown();
                        BindRMDropDown();
                    }
                    else if (userType == "rm")
                    {
                        spnBranch.Visible = false;
                        spnRM.Visible = false;
                    }
                    else if (userType == "bm")
                    {
                        BindBranchForBMDropDown();
                        BindRMforBranchDropdown(0, bmID, 1);
                    }
                }
                else
                {
                    ValuationNotDoneErrorMsg.Visible = false;

                    if (userType == "rm")
                    {
                        spnBranch.Visible = false;
                        spnRM.Visible = false;
                    }
                    if (userType == "adviser")
                    {
                        BindBranchDropDown();
                        BindRMDropDown();
                    }
                    if (userType == "bm")
                    {
                        if (!IsPostBack)
                        {
                            BindBranchForBMDropDown();
                            BindRMforBranchDropdown(0, bmID, 1);
                        }
                        hdnbranchId.Value = "0";
                        hdnbranchHeadId.Value = ddlBranch.SelectedValue;
                        hdnAll.Value = "2";
                        hdnXWise.Value = "2";
                        hdnrmId.Value = "0";
                    }
                }

                if (Request.QueryString["schemeplanid"] != null)
                {
                    if (Request.QueryString["BranchSelection"] != "")
                    {
                        BranchSelection = Request.QueryString["BranchSelection"].ToString();
                        ddlBranch.SelectedValue = BranchSelection;
                    }
                    if (Request.QueryString["RMSelection"] != "")
                    {
                        RMSelection = Request.QueryString["RMSelection"].ToString();
                        ddlRM.SelectedValue = RMSelection;
                    }

                    schemeplanid = int.Parse(Request.QueryString["schemeplanid"].ToString());
                    LatestValuationdate = DateTime.Parse(Request.QueryString["latestValuationdate"].ToString());
                }
                else
                {
                    valuedate = Convert.ToString(portfoliobo.GetLatestValuationDate(advisorVo.advisorId, "MF"));
                    if (valuedate != "")
                    {
                        LatestValuationdate = Convert.ToDateTime(portfoliobo.GetLatestValuationDate(advisorVo.advisorId, "MF"));
                        //Valuation date storing in Hiddenfield For all page Export
                        hdnValuationDate.Value = LatestValuationdate.ToString();
                    }
                }
                if (LatestValuationdate != DateTime.MinValue)
                {
                    txtDate.Text = LatestValuationdate.Date.ToShortDateString();
                    if (Request.QueryString["strCustomreId"] != null)
                    {
                        GetQueryString();
                    }
                    bindgrid(LatestValuationdate, schemeplanid);

                }
                //else
                //{
                //    trMessage.Visible = true;
                //    lblMessage.Text = "No valuation done";

                //}
            }

           
             
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            GenerateMIS();
        }
        public void GenerateMIS()
        {
            LatestValuationdate = Convert.ToDateTime(txtDate.Text);
            hdnValuationDate.Value = LatestValuationdate.ToString();
            TextBox AMCSearchval = new TextBox();
            TextBox SchemeSearchval = new TextBox();
            TextBox CustomerName = new TextBox();
            TextBox FolioNum = new TextBox();

            if (gvMFMIS.HeaderRow != null)
            {
                AMCSearchval = (TextBox)gvMFMIS.HeaderRow.FindControl("txtAMCSearch");
                SchemeSearchval = (TextBox)gvMFMIS.HeaderRow.FindControl("txtSchemeSearch");
                CustomerName = (TextBox)gvMFMIS.HeaderRow.FindControl("txtCustomerSearch");
                FolioNum = (TextBox)gvMFMIS.HeaderRow.FindControl("txtFolioSearch");
            }

            hdnSchemeSearchVal.Value = SchemeSearchval.Text;
            hdnAMCSearchVal.Value = AMCSearchval.Text;
            hdnCustomerNameVal.Value = CustomerName.Text;
            hdnFolioNumVal.Value = FolioNum.Text;

            if (hdnCurrentPage.Value == string.Empty || hdnCurrentPage.Value == "")
            {
                CurrentPage = 1;
            }
            else
            {
                CurrentPage = int.Parse(hdnCurrentPage.Value);

            }

            /* For BM Branch wise MIS */
            if (userType == "adviser")
            {
                hdnbranchId.Value = "0";
                hdnbranchHeadId.Value = AdviserID.ToString();
                hdnAll.Value = "2";
                bindgrid(LatestValuationdate, schemeplanid);
            }
            else if (userType == "bm")
            {
                if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnAll.Value = "2";
                    hdnXWise.Value = "2";
                    hdnrmId.Value = ddlRM.SelectedValue;

                    dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), 0, schemeplanid, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), hdnCustomerNameVal.Value.ToString(), hdnFolioNumVal.Value.ToString(), string.Empty, out Count, 0);
                    bindgrid(LatestValuationdate, schemeplanid);
                }
                else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnAll.Value = "3";
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnXWise.Value = "2";

                    dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), 0, schemeplanid, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), hdnCustomerNameVal.Value.ToString(), hdnFolioNumVal.Value.ToString(), string.Empty, out Count, 0);
                    bindgrid(LatestValuationdate, schemeplanid);
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnAll.Value = "1";
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnXWise.Value = "2";

                    dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), 0, schemeplanid, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), hdnCustomerNameVal.Value.ToString(), hdnFolioNumVal.Value.ToString(), string.Empty, out Count, 0);
                    bindgrid(LatestValuationdate, schemeplanid);
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                {
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnAll.Value = "0";
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnXWise.Value = "2";

                    dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), 0, schemeplanid, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), hdnCustomerNameVal.Value.ToString(), hdnFolioNumVal.Value.ToString(), string.Empty, out Count, 0);
                    bindgrid(LatestValuationdate, schemeplanid);
                }
            }
        }

        private void bindgrid(DateTime Valuationdate, int schemeplanid)
        {
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            DataSet dsMISReport = null;
            double totalAum = 0;
            double totalUnits = 0;
            int CurrentPage;
           
            int.TryParse(ddlBranch.SelectedValue, out branchId);
            int.TryParse(ddlRM.SelectedValue, out rmId);
            if (hdnCurrentPage.Value == string.Empty || hdnCurrentPage.Value == "")
            {
                CurrentPage = 1;
            }
            else
            {
                CurrentPage = int.Parse(hdnCurrentPage.Value);

            }


            //For paging
            //if (hdnCurrentPage.Value.ToString() != "")
            //{
            //    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
            //    hdnCurrentPage.Value = "";
            //}


            /*
             if(userType == "rm")
            {
               if (AllPageExport == true)
               {
                   dsMISReport = adviserMISBo.GetAMCSchemewiseMISForRM(rmid, Valuation_Date, amcCode, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), hdnCategoryFilter.Value.ToString(), out Count, int.Parse(hdnRecordCount.Value.ToString()));

               }
               else
               {
                   dsMISReport = adviserMISBo.GetAMCSchemewiseMISForRM(rmid, Valuation_Date, amcCode, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), hdnCategoryFilter.Value.ToString(), out Count, 0);
               }
            }

            else if (userType == "bm")
            {
                if (AllPageExport == true)
                {
                    dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), hdnAMCSearchVal.Value.ToString(), out Count, int.Parse(hdnRecordCount.Value.ToString()));
                }
                else
                {
                    dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), hdnAMCSearchVal.Value.ToString(), out Count, 0);
                }
            }
            else if (userType == "advisor")
            {
                if (AllPageExport == true)
                {
                    dsMISReport = adviserMISBo.GetAMCSchemewiseMISForAdviser(advisorVo.advisorId, branchId, rmId, Valuation_Date, amcCode, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), hdnCategoryFilter.Value.ToString(), out Count, int.Parse(hdnRecordCount.Value.ToString()));
                    
                }
                else
                {
                    dsMISReport = adviserMISBo.GetAMCSchemewiseMISForAdviser(advisorVo.advisorId, branchId, rmId, Valuation_Date, amcCode, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), hdnCategoryFilter.Value.ToString(), out Count, 0);
                }

            } 
             
             
             */


            if (userType == "rm")
            {
                if (AllPageExport == true)
                {
                    dsMISReport = adviserMISBo.GetCustomerAMCSchemewiseMISForRM(rmid, Valuationdate, schemeplanid, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), hdnCustomerNameVal.Value.ToString(), hdnFolioNumVal.Value.ToString(), out Count, int.Parse(hdnRecordCount.Value.ToString()));
                }
                else
                {
                    dsMISReport = adviserMISBo.GetCustomerAMCSchemewiseMISForRM(rmid, Valuationdate, schemeplanid, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), hdnCustomerNameVal.Value.ToString(), hdnFolioNumVal.Value.ToString(), out Count, 0);
                }
            }
            else if (userType == "bm")
            {
                if (AllPageExport == true)
                {
                    dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), 0, schemeplanid, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), hdnCustomerNameVal.Value.ToString(), hdnFolioNumVal.Value.ToString(), string.Empty, out Count, int.Parse(hdnRecordCount.Value.ToString()));
                }
                else
                {
                    dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), 0, schemeplanid, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), hdnCustomerNameVal.Value.ToString(), hdnFolioNumVal.Value.ToString(), string.Empty, out Count, 0);
                }
            }

            else if (userType == "adviser")
            {
                if (AllPageExport == true)
                {
                    dsMISReport = adviserMISBo.GetCustomerAMCSchemewiseMISForAdviser(advisorVo.advisorId, branchId, rmId, Valuationdate, schemeplanid, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), hdnCustomerNameVal.Value.ToString(), hdnFolioNumVal.Value.ToString(), out Count, int.Parse(hdnRecordCount.Value.ToString()));
                }
                else
                {
                    dsMISReport = adviserMISBo.GetCustomerAMCSchemewiseMISForAdviser(advisorVo.advisorId, branchId, rmId, Valuationdate, schemeplanid, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), hdnCustomerNameVal.Value.ToString(), hdnFolioNumVal.Value.ToString(), out Count, 0);

                }
            }

            if (dsMISReport.Tables[0].Rows.Count == 0)
            {
                trMessage.Visible = true;
                gvMFMIS.DataSource = dsMISReport;
                gvMFMIS.DataBind();
            }
            else
            {

                double no_of_units = 0;
                double aum = 0;
                //for (int i = 0; i < dsMISReport.Tables[0].Rows.Count; i++)
                //{
                //    dsMISReport.Tables[0].Rows[i]["MarketPrice"] = decimal.Parse(dsMISReport.Tables[0].Rows[i]["MarketPrice"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                //    dsMISReport.Tables[0].Rows[i]["AUM"] = decimal.Parse(dsMISReport.Tables[0].Rows[i]["AUM"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                //    dsMISReport.Tables[0].Rows[i]["Percentage"] = decimal.Parse(dsMISReport.Tables[0].Rows[i]["Percentage"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                //}
                foreach (DataRow dr in dsMISReport.Tables[0].Rows)
                {
                    no_of_units = Convert.ToDouble(dr["Units"].ToString());
                    aum = Convert.ToDouble(dr["AUM"].ToString());
                    totalUnits = totalUnits + no_of_units;
                    totalAum = totalAum + aum;
                }


                DataTable dtMISReport = new DataTable();

                dtMISReport.Columns.Add("CustomerName");
                dtMISReport.Columns.Add("FolioNum");
                dtMISReport.Columns.Add("AMC");
                dtMISReport.Columns.Add("Scheme");
                dtMISReport.Columns.Add("Category");
                dtMISReport.Columns.Add("MarketPrice");
                dtMISReport.Columns.Add("Units");
                dtMISReport.Columns.Add("AUM");
                dtMISReport.Columns.Add("Percentage");
                dtMISReport.Columns.Add("C_CustomerId");


                DataRow drMISReport;

                for (int i = 0; i < dsMISReport.Tables[0].Rows.Count; i++)
                {
                    drMISReport = dtMISReport.NewRow();

                    drMISReport[0] = dsMISReport.Tables[0].Rows[i][1].ToString();
                    drMISReport[1] = dsMISReport.Tables[0].Rows[i][2].ToString();
                    drMISReport[2] = dsMISReport.Tables[0].Rows[i][4].ToString();
                    drMISReport[3] = dsMISReport.Tables[0].Rows[i][5].ToString();
                    drMISReport[4] = dsMISReport.Tables[0].Rows[i][6].ToString();
                    drMISReport[5] = dsMISReport.Tables[0].Rows[i][9].ToString();
                    drMISReport[9] = dsMISReport.Tables[0].Rows[i][11].ToString();



                    if (GridViewCultureFlag == true)
                    {
                        decimal temp = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][8].ToString()), 2);
                        drMISReport[6] = temp.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        decimal tempAum = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][7].ToString()), 2);
                        drMISReport[7] = tempAum.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        //drMISReport[7] = tempAum.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        //drMISReport[6] = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][7].ToString()), 2).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                    }
                    else
                    {
                        drMISReport[6] = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][8].ToString()), 2).ToString();
                        decimal tempAum = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][7].ToString()), 2);
                        drMISReport[7] = tempAum.ToString();
                        //drMISReport[6] = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][7].ToString()), 2).ToString();

                    }

                    drMISReport[8] = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][10].ToString()), 2).ToString();



                    dtMISReport.Rows.Add(drMISReport);

                }
                string Customer_Name = "";

                if (customerId != 0)
                {
                    string expression = "C_CustomerId = " + customerId;
                  
                    dtMISReport.DefaultView.RowFilter = expression;
                    dtMISReport = dtMISReport.DefaultView.ToTable();
                    if (dtMISReport.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtMISReport.Rows)
                        {
                            Customer_Name = dr["CustomerName"].ToString();
                            hdnCustomerNameVal.Value = Customer_Name;
                            break;
                        }
                    }

                    totalUnits = 0;
                    totalAum = 0;
                    foreach (DataRow dtr in dtMISReport.Rows)
                    {
                        double tempAUM = double.Parse(dtr["AUM"].ToString());
                        double tempUnits = double.Parse(dtr["Units"].ToString());
                        totalAum = tempAUM + totalAum;
                        totalUnits = tempUnits + totalUnits;

                    }
                    //resultTotalAum = dtMISReport.Compute("Sum(AUM)", "");
                    //resultTotalUnits = dtMISReport.Compute("Sum(Units)","");                    
                }

                
                gvMFMIS.DataSource = dtMISReport;
                gvMFMIS.DataBind();
                if (gvMFMIS.HeaderRow != null)
                {
                    TextBox CustomerName = new TextBox();

                    CustomerName = (TextBox)gvMFMIS.HeaderRow.FindControl("txtCustomerSearch");

                    CustomerName.Text = Customer_Name;
                }




                if (GridViewCultureFlag == true)
                {
                    gvMFMIS.FooterRow.Cells[8].Text = System.Math.Round(decimal.Parse(totalAum.ToString()), 2).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                    gvMFMIS.FooterRow.Cells[7].Text = totalUnits.ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                }
                else
                {
                    decimal tempTotalAum = System.Math.Round(decimal.Parse(totalAum.ToString()), 2);
                    gvMFMIS.FooterRow.Cells[8].Text = tempTotalAum.ToString();

                    gvMFMIS.FooterRow.Cells[7].Text = totalUnits.ToString();

                }

            }

            lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
            if (Count > 0)
                DivPager.Style.Add("display", "visible");
            else
            {
                DivPager.Style.Add("display", "none");
                lblCurrentPage.Text = "";
                lblCurrentPage.Text = "";
            }
            ViewState["schemeplanid"] = schemeplanid.ToString();
            ViewState["Valuationdate"] = Valuationdate.ToString();

            TextBox txtamc = new TextBox();
            TextBox txtscheme = new TextBox();
            TextBox txtCustomer = new TextBox();
            TextBox txtFolio = new TextBox();

            if (gvMFMIS.HeaderRow != null)
            {
                txtamc = (TextBox)gvMFMIS.HeaderRow.FindControl("txtAMCSearch");
                txtscheme = (TextBox)gvMFMIS.HeaderRow.FindControl("txtSchemeSearch");
                txtCustomer = (TextBox)gvMFMIS.HeaderRow.FindControl("txtCustomerSearch");
                txtFolio = (TextBox)gvMFMIS.HeaderRow.FindControl("txtFolioSearch");
            }

            txtamc.Text = hdnAMCSearchVal.Value.ToString();
            txtscheme.Text = hdnSchemeSearchVal.Value.ToString();
            txtCustomer.Text = hdnCustomerNameVal.Value.ToString();
            txtFolio.Text = hdnFolioNumVal.Value.ToString();

            this.GetPageCount();


        }

        protected void gvMFMIS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string folio;
            folio = gvMFMIS.SelectedDataKey[0].ToString();
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMMultipleTransactionView','?folionum=" + folio + "');", true);
            Response.Redirect("ControlHost.aspx?pageid=RMMultipleTransactionView&folionum=" + folio + "", false);
        }

        protected void ddlMISType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMISType.SelectedItem.Value.ToString() == "AMCWiseAUM")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMAMCwiseMIS','login');", true);
            }

            if (ddlMISType.SelectedItem.Value.ToString() == "AMCSchemeWiseAUM")
            {
                Session["PassAMCCode"] = null;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMAMCSchemewiseMIS','login');", true);
            }
            else if (ddlMISType.SelectedItem.Value.ToString() == "FolioWiseAUM")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMCustomerAMCSchemewiseMIS','login');", true);
            }
            else if (ddlMISType.SelectedItem.Value.ToString() == "TurnOverSummary")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserMFMIS','login');", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            TextBox AMCSearchval = (TextBox)gvMFMIS.HeaderRow.FindControl("txtAMCSearch");
            TextBox SchemeSearchval = (TextBox)gvMFMIS.HeaderRow.FindControl("txtSchemeSearch");
            TextBox CustomerName = (TextBox)gvMFMIS.HeaderRow.FindControl("txtCustomerSearch");
            TextBox FolioNum = (TextBox)gvMFMIS.HeaderRow.FindControl("txtFolioSearch");

            hdnSchemeSearchVal.Value = SchemeSearchval.Text;
            hdnAMCSearchVal.Value = AMCSearchval.Text;
            hdnCustomerNameVal.Value = CustomerName.Text;
            hdnFolioNumVal.Value = FolioNum.Text;

            LatestValuationdate = Convert.ToDateTime(ViewState["Valuationdate"].ToString());
            schemeplanid = int.Parse(ViewState["schemeplanid"].ToString());
            bindgrid(LatestValuationdate, schemeplanid);

        }
        protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
        {
            //gvMFMIS.Columns[0].Visible = false;

            //PrepareGridViewForExport(gvMFMIS);
            //ExportGridView("Excel");

            //LatestValuationdate = Convert.ToDateTime(ViewState["Valuationdate"].ToString());
            //schemeplanid = int.Parse(ViewState["schemeplanid"].ToString());
            //bindgrid(LatestValuationdate, schemeplanid);
            //trModalPopup.Visible = true;
            //trExportPopup.Visible = true;
            ModalPopupExtender1.TargetControlID = "imgBtnExport";
            ModalPopupExtender1.Show();
            //trExportPopup.Visible = false;
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {

            //gvMFTransactions.Columns[0].Visible = false;

            //gvMFTransactions.HeaderRow.Visible = true;
            gvMFMIS.Columns[0].Visible = false;
            gvMFMIS.HeaderRow.Visible = true;
            schemeplanid = int.Parse(ViewState["schemeplanid"].ToString());
            DateTime Valuation_Date = Convert.ToDateTime(hdnValuationDate.Value.ToString());

            if (hdnDownloadPageType.Value.ToString() == "single")
            {
                GridViewCultureFlag = false;
                bindgrid(Valuation_Date, schemeplanid);
                PrepareGridViewForExport(gvMFMIS);
                GridViewCultureFlag = true;
            }
            else
            {
                GridViewCultureFlag = false;
                AllPageExport = true;
                bindgrid(Valuation_Date, schemeplanid);
                PrepareGridViewForExport(gvMFMIS);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "AferExportAll('ctrl_RMMultipleTransactionView_btnPrintGrid');", true);
                GridViewCultureFlag = true;
                AllPageExport = false;
            }

            ExportGridView("Excel");
            gvMFMIS.Columns[0].Visible = true;
            //ExportGridView(hdnDownloadFormat.Value.ToString());
            //
            //BindGrid(mypager.CurrentPage, convertedFromDate, convertedToDate);
            //gvMFTransactions.Columns[0].Visible = true;

        }

        private void ExportGridView(string Filetype)
        {
            LatestValuationdate = Convert.ToDateTime(ViewState["Valuationdate"].ToString());
            HtmlForm frm = new HtmlForm();
            frm.Controls.Clear();
            frm.Attributes["runat"] = "server";
            if (Filetype == "Excel")
            {

                string temp = rmVo.FirstName + rmVo.LastName + "'s_MF_MIS.xls";
                string attachment = "attachment; filename=" + temp;
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);


                //Add details for the header

                Response.Output.Write("<table border=\"0\"><tbody><tr><td>");
                Response.Output.Write("Report  : ");
                Response.Output.Write("</td>");
                Response.Output.Write("<td><b>");
                Response.Output.Write("Folio/AMC Schemewise MIS</b>");
                Response.Output.Write("</td></tr><tr><td>");
                Response.Output.Write("As on Date : ");
                Response.Output.Write("</td><td><b>");
                System.DateTime tDate1 = LatestValuationdate;
                Response.Output.Write(tDate1.ToShortDateString());
                Response.Output.Write("</b></td></tr>");
                Response.Output.Write("</tbody></table>");




                if (gvMFMIS.HeaderRow != null)
                {
                    PrepareControlForExport(gvMFMIS.HeaderRow);
                }
                foreach (GridViewRow row in gvMFMIS.Rows)
                {
                    PrepareControlForExport(row);
                }
                if (gvMFMIS.FooterRow != null)
                {
                    PrepareControlForExport(gvMFMIS.FooterRow);
                }

                gvMFMIS.Parent.Controls.Add(frm);
                frm.Controls.Add(gvMFMIS);
                frm.RenderControl(htw);
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();


            }
        }

        private void PrepareGridViewForExport(Control gv)
        {

            LinkButton lb = new LinkButton();
            Literal l = new Literal();
            string name = String.Empty;
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(TextBox))
                {
                    l.Text = (gv.Controls[i] as TextBox).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }
            }

        }
        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedValue.ToString()));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }

        private void BindBranchDropDown()
        {
            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null)
                {
                    ddlBranch.DataSource = ds;
                    ddlBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMCustomerAMCSchemewiseMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindRMDropDown()
        {
            try
            {
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
                if (dt.Rows.Count > 0)
                {
                    ddlRM.DataSource = dt;
                    ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
                    ddlRM.DataTextField = dt.Columns["RMName"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMCustomerAMCSchemewiseMIS.ascx:BindRMDropDown()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* For BM Branch wise MIS */

            if (ddlBranch.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID, 1);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0, 0);
            }
            GenerateMIS();
        }

        protected void ddlRM_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateMIS();
        }

        /* For Binding the Branch Dropdowns */

        private void BindBranchForBMDropDown()
        {
            try
            {
                DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(0, bmID, 0);
                if (ds != null)
                {
                    ddlBranch.DataSource = ds.Tables[1]; ;
                    ddlBranch.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserMFMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        /* End For Binding the Branch Dropdowns */



        /* For Binding the RM Dropdowns */

        private void BindRMforBranchDropdown(int branchId, int branchHeadId, int all)
        {

            try
            {

                DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(branchId, branchHeadId, all);
                if (ds != null)
                {
                    ddlRM.DataSource = ds.Tables[0]; ;
                    ddlRM.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                    ddlRM.DataTextField = ds.Tables[0].Columns["RM Name"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserMFMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void GetQueryString()
        {            
            customerId = Int32.Parse(Request.QueryString["strCustomreId"].ToString());


        }


        /* End For Binding the RM Dropdowns */
    }
}


            /* ********************** */



            //try
            //{
            //    DataTable dt = new DataTable();
            //    AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            //    ddlRM.Items.Clear();
            //    if (ddlBranch.SelectedValue != "All")
            //        dt = advisorStaffBo.GetBranchRMList(int.Parse(ddlBranch.SelectedValue));
            //    if (dt.Rows.Count > 0)
            //    {
            //        ddlRM.DataSource = dt;
            //        ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
            //        ddlRM.DataTextField = dt.Columns["RMName"].ToString();
            //        ddlRM.DataBind();
            //    }
            //    ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
            //}
            //catch (BaseApplicationException Ex)
            //{
            //    throw Ex;
            //}
            //catch (Exception Ex)
            //{
            //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
            //    NameValueCollection FunctionInfo = new NameValueCollection();

            //    FunctionInfo.Add("Method", "RMCustomerAMCSchemewiseMIS.ascx:BindBranchDropDown()");

            //    object[] objects = new object[4];

            //    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
            //    exBase.AdditionalInformation = FunctionInfo;
            //    ExceptionManager.Publish(exBase);
            //    throw exBase;
            //}
        
        //protected void btnOk_Click(object sender, ImageClickEventArgs e)
        //{
        //    trModalPopup.Visible = false;
        //    trExportPopup.Visible = false;
            
        //}
        //protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        //{
        //    trModalPopup.Visible = false;
        //    trExportPopup.Visible = false;

        //}