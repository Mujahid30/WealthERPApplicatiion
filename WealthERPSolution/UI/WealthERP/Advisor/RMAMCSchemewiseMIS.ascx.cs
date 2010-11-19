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
    public partial class RMAMCSchemewiseMIS : System.Web.UI.UserControl
    {
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorMISBo adviserMISBo = new AdvisorMISBo();
        RMVo rmVo = new RMVo();
        string userType;
        int advisorId;
        int userId;
        int rmid;
        int amcCode;
        bool GridViewCultureFlag = true;
        bool AllPageExport = false;
        DateTime LatestValuationdate = new DateTime();
        static double totalAmount = 0;
        DataSet dsMISReport;
        int AdviserID;
        static double totalUnits = 0;
        int count;
        int Count;


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
            DateTime valdate = Convert.ToDateTime(ViewState["Valuationdate"].ToString());
            this.bindgrid(valdate,amcCode);
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

                int ratio = rowCount / 30;
                mypager.PageCount = rowCount % 30 == 0 ? ratio : ratio + 1;
                mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                lowerlimit = (((mypager.CurrentPage - 1) * 30) + 1).ToString();
                upperlimit = (mypager.CurrentPage * 30).ToString();
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
            this.Page.Culture = "en-GB";
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userType = Session["UserType"].ToString().ToLower();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            rmid = rmVo.RMId;
            trMessage.Visible = false;
            amcCode = 0;

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
                    hdnrmId.Value = "0";
                    hdnXWise.Value = "1";
                    dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), amcCode, 0, 1, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), string.Empty, string.Empty, hdnCategoryFilter.Value.ToString(), out count, 0);
                }
                if (Request.QueryString["amcCode"] != null)
                {
                    amcCode = int.Parse(Request.QueryString["amcCode"].ToString());
                    LatestValuationdate = DateTime.Parse(Request.QueryString["latestValuationdate"].ToString());
                }
                else
                {
                    valuedate = Convert.ToString(portfoliobo.GetLatestValuationDate(advisorVo.advisorId, "MF"));
                    hdnValuationDate.Value = valuedate.ToString();
                    if (valuedate != "")
                    {
                        LatestValuationdate = Convert.ToDateTime(valuedate);
                        //Valuation date storing in Hiddenfield For all page Export
                        ValuationDate.Value = LatestValuationdate.ToString();
                    }
                }
                if (LatestValuationdate != DateTime.MinValue)
                {
                   
                    txtDate.Text = LatestValuationdate.Date.ToShortDateString();

                    //txtDate.Text = LatestValuationdate.Date.ToShortDateString();
                    bindgrid(LatestValuationdate,amcCode);
                }
                else
                {
                    lblMessage.Text = "No valuation done";
                    trMessage.Visible = true;
                }
                hdnDownloadPageType.Value = "single";
               
            }

            if (hdnDownloadPageType.Value != "" || hdnDownloadPageType.Value != string.Empty)
            {
                hdnDownloadPageType.Value = "single";

            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            GenerateMIS();
        }

        public void GenerateMIS()
        {
            int CurrentPage;
            LatestValuationdate = Convert.ToDateTime(txtDate.Text);
            ValuationDate.Value = LatestValuationdate.ToString();
            TextBox AMCSearchval = new TextBox();
            TextBox SchemeSearchval = new TextBox();
            if (gvMFMIS.HeaderRow != null)
            {
                AMCSearchval = (TextBox)gvMFMIS.HeaderRow.FindControl("txtAMCSearch"); //GetAMCTextBox();
                SchemeSearchval = (TextBox)gvMFMIS.HeaderRow.FindControl("txtSchemeSearch");//GetSchemeTextBox();
            }

            hdnSchemeSearchVal.Value = SchemeSearchval.Text;
            hdnAMCSearchVal.Value = AMCSearchval.Text;

            if (hdnCurrentPage.Value == string.Empty || hdnCurrentPage.Value == "")
            {
                CurrentPage = 1;
            }
            else
            {
                CurrentPage = int.Parse(hdnCurrentPage.Value);

            }

            /* For BM MIS */
            if (userType == "adviser")
            {
                hdnbranchId.Value = "0";
                hdnbranchHeadId.Value = AdviserID.ToString();
                hdnAll.Value = "2";
                hdnXWise.Value = "1";
                hdnrmId.Value = ddlRM.SelectedValue;

                dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), amcCode, 0, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), string.Empty, string.Empty, hdnCategoryFilter.Value.ToString(), out count, 0);
                bindgrid(LatestValuationdate, amcCode);
            }

            else if (userType == "bm")
            {
                if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnAll.Value = "2";
                    hdnXWise.Value = "1";
                    hdnrmId.Value = ddlRM.SelectedValue;

                    dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), amcCode, 0, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), string.Empty, string.Empty, hdnCategoryFilter.Value.ToString(), out count, 0);
                    bindgrid(LatestValuationdate, amcCode);
                }
                else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnAll.Value = "3";
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnXWise.Value = "1";

                    dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), amcCode, 0, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), string.Empty, string.Empty, hdnCategoryFilter.Value.ToString(), out count, 0);
                    bindgrid(LatestValuationdate, amcCode);
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnAll.Value = "1";
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnXWise.Value = "1";

                    dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), amcCode, 0, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), string.Empty, string.Empty, hdnCategoryFilter.Value.ToString(), out count, 0);
                    bindgrid(LatestValuationdate, amcCode);
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                {
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnAll.Value = "0";
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnXWise.Value = "1";

                    dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), amcCode, 0, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), string.Empty, string.Empty, hdnCategoryFilter.Value.ToString(), out count, 0);
                    bindgrid(LatestValuationdate, amcCode);
                }
            }
        }


        //protected void gvMFMIS_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        e.Row.Cells[5].Text = "Total ";
        //        if (GridViewCultureFlag == true)
        //            e.Row.Cells[7].Text = decimal.Parse(totalAmount.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
        //        else
        //        {
        //            e.Row.Cells[7].Text = decimal.Parse(totalAmount.ToString()).ToString();
        //        }
        //        e.Row.Cells[7].Attributes.Add("align", "Right");
        //        if (GridViewCultureFlag == true)
        //            e.Row.Cells[6].Text = decimal.Parse(totalUnits.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
        //        else
        //        {
        //            e.Row.Cells[6].Text = decimal.Parse(totalUnits.ToString()).ToString();
        //        }
        //        e.Row.Cells[6].Attributes.Add("align", "Right");


        //    }
        //    //if (e.Row.RowType == DataControlRowType.Header)
        //    //{
        //    //    DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlCategory");
        //    //    if (hdnCategoryFilter.Value!=null)
        //    //    ddlStatus.SelectedValue = hdnCategoryFilter.Value;
        //    //}
        //}


        private void bindgrid(DateTime Valuationdate , int amcCode)
        {
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            DataSet dsMISReport = null;
            //double totalAum = 0;
            //double totalUnits = 0;
           
            int CurrentPage;
            int rmId,branchId;
            int.TryParse(ddlBranch.SelectedValue, out branchId);
            int.TryParse(ddlRM.SelectedValue, out rmId);
           // DateTime Valuation_Date = Convert.ToDateTime(txtDate.Text);
            DateTime Valuation_Date = Convert.ToDateTime(hdnValuationDate.Value.ToString());
            if (hdnCurrentPage.Value == string.Empty || hdnCurrentPage.Value == "")
            {
                CurrentPage = 1;
            }
            else
            {
                CurrentPage = int.Parse(hdnCurrentPage.Value);

            }
                        
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
                    dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), amcCode, 0, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), string.Empty, string.Empty, hdnCategoryFilter.Value.ToString(), out Count, int.Parse(hdnRecordCount.Value.ToString()));
                }
                else
                {
                    dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), amcCode, 0, CurrentPage, hdnAMCSearchVal.Value.ToString(), hdnSchemeSearchVal.Value.ToString(), string.Empty, string.Empty, hdnCategoryFilter.Value.ToString(), out Count, 0);
                }
            }
            else if (userType == "adviser")
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


            if (dsMISReport.Tables[0].Rows.Count == 0)
            {
                trMessage.Visible = true;
                gvMFMIS.DataSource = dsMISReport;
                gvMFMIS.DataBind();
            }
            else
            {

                    DataTable dtMISReport = new DataTable();

                    dtMISReport.Columns.Add("SchemePlanCode");
                    dtMISReport.Columns.Add("AMC");
                    dtMISReport.Columns.Add("Scheme");
                    dtMISReport.Columns.Add("Category");
                    dtMISReport.Columns.Add("AUM");
                    dtMISReport.Columns.Add("Units");
                    dtMISReport.Columns.Add("MarketPrice");
                    dtMISReport.Columns.Add("Percentage");

                    DataRow drMISReport;

                    for (int i = 0; i < dsMISReport.Tables[0].Rows.Count; i++)
                    {
                        drMISReport = dtMISReport.NewRow();

                        drMISReport[0] = dsMISReport.Tables[0].Rows[i][1].ToString();
                        drMISReport[1] = dsMISReport.Tables[0].Rows[i][2].ToString();
                        drMISReport[2] = dsMISReport.Tables[0].Rows[i][3].ToString();
                        drMISReport[3] = dsMISReport.Tables[0].Rows[i][4].ToString();
                        //drMISReport[4] = dsMISReport.Tables[0].Rows[i][5].ToString();
                        //drMISReport[5] = dsMISReport.Tables[0].Rows[i][6].ToString();
                        //drMISReport[6] = dsMISReport.Tables[0].Rows[i][7].ToString();
                        //drMISReport[7] = dsMISReport.Tables[0].Rows[i][8].ToString();

                        if (GridViewCultureFlag == true)
                        {
                            decimal temp = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][5].ToString()), 2);
                            drMISReport[4] = temp.ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            drMISReport[5] = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][6].ToString()), 4).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            //drMISReport[6] = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][7].ToString()), 2).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        }
                        else
                        {
                            drMISReport[4] = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][5].ToString()), 2).ToString();                             
                            drMISReport[5] =System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][6].ToString()),4).ToString();
                            //drMISReport[6] = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][7].ToString()), 2).ToString();
                                
                        }

                        drMISReport[6] = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][7].ToString()), 2).ToString();
                        drMISReport[7] = System.Math.Round(decimal.Parse(dsMISReport.Tables[0].Rows[i][8].ToString()), 2).ToString();
                            

                        dtMISReport.Rows.Add(drMISReport);

                    }
                          
                                
                                    
                  gvMFMIS.DataSource = dtMISReport;
                  gvMFMIS.DataBind();
                 

                  double no_of_units = 0;
                  double aum = 0;
                  foreach (DataRow dr in dsMISReport.Tables[0].Rows)
                    {
       
                        no_of_units = Convert.ToDouble(dr["Units"].ToString());
                        aum = Convert.ToDouble(dr["AUM"].ToString());
                        totalUnits = totalUnits + no_of_units;
                        totalAmount = totalAmount + aum;

                    }
                  if (GridViewCultureFlag == true)
                  {
                      gvMFMIS.FooterRow.Cells[5].Text = totalUnits.ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                      gvMFMIS.FooterRow.Cells[6].Text = totalAmount.ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
 
                  }
                  else
                  {
                      gvMFMIS.FooterRow.Cells[5].Text = totalUnits.ToString();
                      gvMFMIS.FooterRow.Cells[6].Text = totalAmount.ToString();

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
           
            ViewState["Valuationdate"] = Valuationdate.ToString();
            TextBox txtamc = new TextBox();
            TextBox txtscheme = new TextBox();

            if (gvMFMIS.HeaderRow != null)
            {
                 txtamc = (TextBox)gvMFMIS.HeaderRow.FindControl("txtAMCSearch");
                 txtscheme = (TextBox)gvMFMIS.HeaderRow.FindControl("txtSchemeSearch");
            }
            txtamc.Text = hdnAMCSearchVal.Value.ToString();      
            txtscheme.Text = hdnSchemeSearchVal.Value.ToString();

            if (dsMISReport.Tables[2].Rows.Count > 0)
            {
                DropDownList ddlcategorylist = GetCategoryDDL();
                if (ddlcategorylist != null)
                {
                    ddlcategorylist.DataSource = dsMISReport.Tables[2];
                  
                    ddlcategorylist.DataTextField = dsMISReport.Tables[2].Columns[0].ToString();
                    ddlcategorylist.DataValueField = dsMISReport.Tables[2].Columns[0].ToString();
                    ddlcategorylist.DataBind();
                    ddlcategorylist.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                    if (hdnCategoryFilter.Value.ToString() != "")
                    {
                        ddlcategorylist.SelectedValue = hdnCategoryFilter.Value.ToString();
                    }
                }
            }

            this.GetPageCount();

            if (GridViewCultureFlag == false)
            {
                GridViewCultureFlag = true;
            }

            totalUnits = 0;
            totalAmount = 0;

        }

        protected void gvMFMIS_SelectedIndexChanged(object sender, EventArgs e)
        {
            int schemeplanid;
            schemeplanid = int.Parse(gvMFMIS.SelectedDataKey["SchemePlanCode"].ToString());
            LatestValuationdate = Convert.ToDateTime(txtDate.Text).Date;
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMCustomerAMCSchemewiseMIS','?schemeplanid=" + schemeplanid + "&latestValuationdate=" + LatestValuationdate.ToShortDateString() + "');", true);
            Response.Redirect("ControlHost.aspx?pageid=RMCustomerAMCSchemewiseMIS&schemeplanid=" + schemeplanid+"&latestValuationdate="+LatestValuationdate.ToShortDateString() +"", false);
            
        }

        protected void ddlMISType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMISType.SelectedItem.Value.ToString() == "AMCWiseAUM")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMAMCwiseMIS','login');", true);
            }
            if (ddlMISType.SelectedItem.Value.ToString() == "AMCSchemeWiseAUM")
            {
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

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCategory = GetCategoryDDL();
            LatestValuationdate = Convert.ToDateTime(ViewState["Valuationdate"].ToString());
            if (ddlCategory != null)
            {
                if (ddlCategory.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnCategoryFilter.Value = ddlCategory.SelectedValue;
                    bindgrid(LatestValuationdate,amcCode);
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnCategoryFilter.Value = "";
                    bindgrid(LatestValuationdate,amcCode);
                }
            }
        }

        private DropDownList GetCategoryDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvMFMIS.HeaderRow != null)
            {
                if ((DropDownList)gvMFMIS.HeaderRow.FindControl("ddlCategory") != null)
                {
                    ddl = (DropDownList)gvMFMIS.HeaderRow.FindControl("ddlCategory");
                }
            }
            else
                ddl = null;

            return ddl;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            TextBox AMCSearchval =  (TextBox)gvMFMIS.HeaderRow.FindControl("txtAMCSearch"); 
            TextBox SchemeSearchval = (TextBox)gvMFMIS.HeaderRow.FindControl("txtSchemeSearch");

            hdnSchemeSearchVal.Value = SchemeSearchval.Text;
            hdnAMCSearchVal.Value = AMCSearchval.Text;
            LatestValuationdate = Convert.ToDateTime(ViewState["Valuationdate"].ToString());

            bindgrid(LatestValuationdate,amcCode);

        }

        protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
        {
            //gvMFMIS.Columns[0].Visible = false;
            //AllPageExport = true;
            //bindgrid(LatestValuationdate, amcCode);
            ////PrepareGridViewForExport(gvMFMIS);

            //ExportGridView("Excel");

            //LatestValuationdate = Convert.ToDateTime(ViewState["Valuationdate"].ToString());

            //bindgrid(LatestValuationdate, amcCode);



            ModalPopupExtender1.TargetControlID = "imgBtnExport";
            ModalPopupExtender1.Show();





        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {

            //gvMFTransactions.Columns[0].Visible = false;

            //gvMFTransactions.HeaderRow.Visible = true;
            gvMFMIS.Columns[0].Visible = false;
            gvMFMIS.HeaderRow.Visible = true;
            
            if (hdnDownloadPageType.Value.ToString() == "single")
            {
                GridViewCultureFlag = false;
                bindgrid(LatestValuationdate, amcCode);
                PrepareGridViewForExport(gvMFMIS);
                GridViewCultureFlag = true;
            }
            else
            {
                GridViewCultureFlag = false;
                AllPageExport = true;
                bindgrid(LatestValuationdate, amcCode);
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
            LatestValuationdate = Convert.ToDateTime(ValuationDate.Value.ToString());
            HtmlForm frm = new HtmlForm();
            frm.Controls.Clear();
            frm.Attributes["runat"] = "server";
            if (Filetype == "Excel")
            {

                gvMFMIS.Columns.Remove(this.gvMFMIS.Columns[0]);
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
                Response.Output.Write("AMC/Schemewise AUM</b>");
                Response.Output.Write("</td></tr><tr><td>");
                Response.Output.Write("As on Date : ");
                Response.Output.Write("</td><td><b>");
                System.DateTime tDate1 = LatestValuationdate;
                Response.Output.Write(tDate1.ToShortDateString());
                Response.Output.Write("</b></td></tr>");
                Response.Output.Write("</tbody></table>");


                PrepareGridViewForExport(gvMFMIS);
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
                DataSet ds =  uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId,"adviser");
                if (ds != null)
                {
                    ddlBranch.DataSource = ds;
                    ddlBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new  System.Web.UI.WebControls.ListItem("All", "All"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

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

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindRMDropDown()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        //private void BindRMDropDown()
        //{
        //    try
        //    {
        //        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        //        DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddlRM.DataSource = dt;
        //            ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
        //            ddlRM.DataTextField = dt.Columns["RMName"].ToString();
        //            ddlRM.DataBind();
        //        }
        //        ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindRMDropDown()");

        //        object[] objects = new object[0];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {

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

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        /* End For Binding the RM Dropdowns */

    }
}