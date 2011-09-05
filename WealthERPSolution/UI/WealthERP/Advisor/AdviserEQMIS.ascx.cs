using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using VoUser;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using WealthERP.Base;
using BoCommon;
using System.Globalization;
using System.Collections.Specialized;
using BoCustomerPortfolio;
using BoUploads;

namespace WealthERP.Advisor
{
    public partial class AdviserEQMIS : System.Web.UI.UserControl
    {
        AdvisorMISBo adviserMIS = new AdvisorMISBo();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        string path;
        string userType; 
        int advisorId;
        int rmId = 0;
        DateTime LatestValuationdate = new DateTime();
        DataRow drAdvEQMIS;
        UserVo userVo = new UserVo();
        int bmID;
        int ScripCodeForCompany;
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        string BranchSelection = string.Empty;
        string RMSelection = string.Empty;


        CustomerTransactionBo customertransactionbo = new CustomerTransactionBo();
        DataSet dsGetLastTradeDate;
        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
                userType = "advisor";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            advisorId = advisorVo.advisorId;
            PortfolioBo portfoliobo = new PortfolioBo();
            string valuedate = Convert.ToString(portfoliobo.GetLatestValuationDate(advisorId, "EQ"));
            hdnValuationDate.Value = valuedate.ToString();
            if (hdnValuationDate.Value == string.Empty)
            {
                ValuationNotDoneErrorMsg.Visible = true;
                if (userType == "advisor")
                {
                    BindBranchDropDown();
                    BindRMDropDown();
                }
                else if (userType == "rm")
                {
                    trBranchRmDpRow.Visible = false;

                }
                else if (userType == "bm")
                {
                    BindBranchForBMDropDown();
                    BindRMforBranchDropdown(0, bmID, 1);
                    trComSecWiseOptions.Visible = true;
                }
            }
            else
            {
                ValuationNotDoneErrorMsg.Visible = false;
                try
                {
                    /* For UserType */
                    advisorVo = (AdvisorVo)Session["advisorVo"];
                    path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

                    if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
                        userType = "advisor";
                    else
                        userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

                    SessionBo.CheckSession();
                    userVo = (UserVo)Session["userVo"];
                    rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
                    bmID = rmVo.RMId;

                    if (Request.QueryString["ScripCode"] != null)
                    {
                        if (Request.QueryString["BranchSelection"] != "")
                        {
                            BranchSelection = Request.QueryString["BranchSelection"].ToString();
                            ddlBranchForEQ.SelectedValue = BranchSelection;
                        }
                        if (Request.QueryString["RMSelection"] != "")
                        {
                            RMSelection = Request.QueryString["RMSelection"].ToString();
                            ddlRMEQ.SelectedValue = RMSelection;
                        }
                        ScripCodeForCompany = int.Parse(Request.QueryString["ScripCode"].ToString());
                        ddlMISType.SelectedValue = "SectorWise";
                    }

                    if (!IsPostBack)
                    {
                        
                        if (valuedate != "")
                        {
                            LatestValuationdate = Convert.ToDateTime(portfoliobo.GetLatestValuationDate(advisorVo.advisorId, "EQ"));
                            hdnValuationDate.Value = LatestValuationdate.ToString();
                        }
                        if (LatestValuationdate != DateTime.MinValue)
                        {
                            txtEQDate.Text = LatestValuationdate.Date.ToShortDateString();

                        }
                        BindMISTypeDropDown();
                    }

                    dsGetLastTradeDate = customertransactionbo.GetLastTradeDate();
                    DateTime dtLastTradeDate;

                    if (valuedate != "")
                    {
                        LatestValuationdate = Convert.ToDateTime(portfoliobo.GetLatestValuationDate(advisorVo.advisorId, "EQ"));
                        hdnValuationDate.Value = LatestValuationdate.ToString();
                    }
                    if (LatestValuationdate != DateTime.MinValue)
                    {
                        txtEQDate.Text = LatestValuationdate.Date.ToShortDateString();
                    }


                    if (dsGetLastTradeDate.Tables[0].Rows.Count != 0)
                    {
                        dtLastTradeDate = (DateTime)dsGetLastTradeDate.Tables[0].Rows[0]["WTD_Date"];

                        txtFromDate.Text = dtLastTradeDate.ToShortDateString();
                        txtToDate.Text = dtLastTradeDate.ToShortDateString();

                    }

                    //to hide the dropdown selection for mis type for adviser and bm
                    if (!IsPostBack)
                    {
                        rbtnPickDate.Checked = true;
                        rbtnPickPeriod.Checked = false;
                        //trRange.Visible = true;
                        trPeriod.Visible = false;
                    }
                    //BindMISTypeDropDown();
                }
                catch (BaseApplicationException Ex)
                {
                    throw Ex;
                }
                catch (Exception Ex)
                {
                    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                    NameValueCollection FunctionInfo = new NameValueCollection();

                    FunctionInfo.Add("Method", "AddBranch.ascx:PageLoad()");

                    object[] objects = new object[2];
                    objects[0] = advisorVo;
                    objects[1] = path;
                    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                    exBase.AdditionalInformation = FunctionInfo;
                    ExceptionManager.Publish(exBase);
                    throw exBase;

                }
            }
        }

        private void BindMISTypeDropDown()
        {
            if (txtEQDate.Text == string.Empty)
            {
                if ((userType == "advisor") || (userType == "bm"))
                {
                    if (ddlMISType.SelectedValue == "TurnOverSummery")
                    {
                        ValuationNotDoneErrorMsg.Visible = true;
                        rbtnPickDate.Checked = true;
                        rbtnPickPeriod.Checked = false;
                        trPeriod.Visible = false;
                        trRange.Visible = true;
                        trRbtnPickDate.Visible = true;
                        tableComSecWiseOptions.Visible = false;
                        trBranchRmDpRow.Visible = true;
                    }
                    else if (ddlMISType.SelectedValue == "CompanyWise")
                    {
                        ValuationNotDoneErrorMsg.Visible = true;
                        trRange.Visible = false;
                        trRbtnPickDate.Visible = false;
                        trPeriod.Visible = false;
                        tableComSecWiseOptions.Visible = true;
                        trBranchRmDpRow.Visible = true;
                    }
                    else if (ddlMISType.SelectedValue == "SectorWise")
                    {
                        ValuationNotDoneErrorMsg.Visible = true;
                        trRange.Visible = false;
                        trRbtnPickDate.Visible = false;
                        trPeriod.Visible = false;
                        tableComSecWiseOptions.Visible = true;
                        trBranchRmDpRow.Visible = true;
                    }
                }
                else if (userType == "rm")
                {
                    if (ddlMISType.SelectedValue == "TurnOverSummery")
                    {
                        ValuationNotDoneErrorMsg.Visible = true;
                        rbtnPickDate.Checked = true;
                        rbtnPickPeriod.Checked = false;
                        trPeriod.Visible = false;
                        trRange.Visible = true;
                        trRbtnPickDate.Visible = true;
                        tableComSecWiseOptions.Visible = false;
                        trBranchRmDpRow.Visible = false;
                    }
                    else if (ddlMISType.SelectedValue == "CompanyWise")
                    {
                        ValuationNotDoneErrorMsg.Visible = true;
                        trRange.Visible = false;
                        trRbtnPickDate.Visible = false;
                        trPeriod.Visible = false;
                        tableComSecWiseOptions.Visible = true;
                        trBranchRmDpRow.Visible = false;
                    }
                    else if (ddlMISType.SelectedValue == "SectorWise")
                    {
                        ValuationNotDoneErrorMsg.Visible = true;
                        trRange.Visible = false;
                        trRbtnPickDate.Visible = false;
                        trPeriod.Visible = false;
                        tableComSecWiseOptions.Visible = true;
                        trBranchRmDpRow.Visible = false;
                    }

                }
                    
            }
                
            else
            {
            LatestValuationdate = Convert.ToDateTime(txtEQDate.Text);
            hdnValuationDate.Value = LatestValuationdate.ToString();
            DateTime Valuation_Date = Convert.ToDateTime(hdnValuationDate.Value.ToString());

                if (ddlMISType.SelectedValue == "TurnOverSummery")
                {
                    rbtnPickDate.Checked = true;
                    rbtnPickPeriod.Checked = false;
                    trPeriod.Visible = false;

                    trRange.Visible = true;
                    trRbtnPickDate.Visible = true;
                    //trPeriod.Visible = false;

                    tableComSecWiseOptions.Visible = false;
                    if ((!IsPostBack) && (userType == "advisor"))
                    {
                        BindBranchDropDown();
                        BindRMDropDown();
                    }
                    else if ((!IsPostBack) && (userType == "rm"))
                    {
                        trBranchRmDpRow.Visible = false;

                    }
                    else if ((!IsPostBack) && (userType == "bm"))
                    {
                        BindBranchForBMDropDown();
                        BindRMforBranchDropdown(0, bmID, 1);
                        trComSecWiseOptions.Visible = true;
                    }
                }

                else if (ddlMISType.SelectedValue == "CompanyWise")
                {
                    //tableTurnOverOptions.Visible = false;
                    trRange.Visible = false;
                    trRbtnPickDate.Visible = false;
                    trPeriod.Visible = false;
                    tableComSecWiseOptions.Visible = true;


                    hdnEQMISType.Value = "0";
                    //trRbtnPickDate.Visible = false;
                    //trRange.Visible = false;
                    //trPeriod.Visible = false;
                    trBranchRmDpRow.Visible = true;

                    if (userType == "advisor")
                    {
                        BindBranchDropDown();
                        BindRMDropDown();
                        GenerateEQMIS();
                    }
                    else if (userType == "rm")
                    {
                        trBranchRmDpRow.Visible = false;
                        GenerateEQMIS();

                    }
                    else if (userType == "bm")
                    {
                        BindBranchForBMDropDown();
                        BindRMforBranchDropdown(0, bmID, 1);
                        GenerateEQMIS();
                    }
                }
                else if (ddlMISType.SelectedValue == "SectorWise")
                {
                    //tableTurnOverOptions.Visible = false;
                    trRange.Visible = false;
                    trRbtnPickDate.Visible = false;
                    trPeriod.Visible = false;
                    tableComSecWiseOptions.Visible = true;

                    hdnEQMISType.Value = "1";
                    //trRbtnPickDate.Visible = false;
                    //trRange.Visible = false;
                    //trPeriod.Visible = false;
                    trBranchRmDpRow.Visible = true;

                    if (userType == "advisor")
                    {
                        BindBranchDropDown();
                        BindRMDropDown();
                        GenerateEQMIS();
                    }
                    else if (userType == "rm")
                    {
                        trBranchRmDpRow.Visible = false;
                        this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                        GenerateEQMIS();
                    }
                    else if (userType == "bm")
                    {
                        BindBranchForBMDropDown();
                        BindRMforBranchDropdown(0, bmID, 1);
                        GenerateEQMIS();
                    }
                }
            }
            
        }

        private void BindGrid(DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsEQMIS = new DataSet();
            DataTable dtAdviserEQMIS = new DataTable();
            gvEQMIS.DataKeyNames = null;
            
            int ID = 0;
           
            if (userType == "advisor")
                ID = advisorVo.advisorId;


            else if (userType == "rm")
            {
                rmVo = (RMVo)Session[SessionContents.RmVo];
                ID = rmVo.RMId;
            }
            else if (userType == "bm")
            {
                rmVo = (RMVo)Session[SessionContents.RmVo];
                ID = rmVo.RMId;
                if (userType == "bm")
                {
                    if (hdnall.Value == "0")
                    {
                        hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                        hdnrmId.Value = ddlRMEQ.SelectedValue;
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo,int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), 0, 0);
                    }
                    else if(hdnall.Value == "1")
                    {
                        hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo,0,int.Parse(hdnbranchId.Value.ToString()),0 , 1);
                    }
                    else if (hdnall.Value == "2")
                    {
                        hdnbranchHeadId.Value = ddlBranchForEQ.SelectedValue;
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, 0, 0, int.Parse(hdnbranchHeadId.Value.ToString()), 2);
                    }
                    else if (hdnall.Value == "3")
                    {
                        hdnbranchHeadId.Value = ddlBranchForEQ.SelectedValue;
                        hdnrmId.Value = ddlRMEQ.SelectedValue;
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, int.Parse(hdnrmId.Value.ToString()), 0, int.Parse(hdnbranchHeadId.Value.ToString()), 3);
                    }
                    //dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo,0, 0, 0, 0);
                }
            }

            try
            {
                if (userType == "advisor")
                {
                    if (hdnall.Value == "0")
                    {
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, 0, 0, int.Parse(hdnbranchHeadId.Value.ToString()), 0);
                    }
                    else if (hdnall.Value == "1")
                    {
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, 0, int.Parse(hdnbranchId.Value.ToString()), 0, 1);
                    }
                    else if (hdnall.Value == "2")
                    {
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, int.Parse(hdnrmId.Value.ToString()), 0, int.Parse(hdnbranchHeadId.Value.ToString()), 2);
                    }
                    else if (hdnall.Value == "3")
                    {
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), 0, 3);
                    }
                }
                if (userType == "rm")
                {
                    dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, 0, 0, 0, 0);
                }

                if (dsEQMIS.Tables[0].Rows.Count > 0)
                {
                    DataRow drAdvEQMIS;
                    drAdvEQMIS = dtAdviserEQMIS.NewRow();
                    
                    dtAdviserEQMIS.Columns.Add("CName_Industry_Delby");
                    dtAdviserEQMIS.Columns.Add("Industry_MValue_DelSell");
                    dtAdviserEQMIS.Columns.Add("MValue_Percentage_SpecSell");
                    dtAdviserEQMIS.Columns.Add("MValue_Blank_SpecBuy");

                    int flag = 0;

                    for (int i = 0; i < dsEQMIS.Tables[0].Rows.Count; i++)
                    {
                        NumberFormatInfo nfi0 = new CultureInfo("en-US", false).NumberFormat;
                        NumberFormatInfo nfi1 = new CultureInfo("en-US", false).NumberFormat;
                        NumberFormatInfo nfi2 = new CultureInfo("en-US", false).NumberFormat;
                        NumberFormatInfo nfi3 = new CultureInfo("en-US", false).NumberFormat;

                        nfi0.NumberDecimalSeparator = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["DeliveryBuy"].ToString()).ToString("N", nfi0));
                        nfi1.NumberDecimalSeparator = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["DeliverySell"].ToString()).ToString("N", nfi1));
                        nfi2.NumberDecimalSeparator = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["SpeculativeSell"].ToString()).ToString("N", nfi2));
                        nfi3.NumberDecimalSeparator = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["SpeculativeBuy"].ToString()).ToString("N", nfi3));

                        drAdvEQMIS[0] = nfi0.NumberDecimalSeparator;
                        drAdvEQMIS[1] = nfi1.NumberDecimalSeparator;
                        drAdvEQMIS[2] = nfi2.NumberDecimalSeparator;
                        drAdvEQMIS[3] = nfi3.NumberDecimalSeparator;

                        dtAdviserEQMIS.Rows.Add(drAdvEQMIS);

                        
                    }

                    if ((decimal.Parse(dsEQMIS.Tables[0].Rows[0]["DeliveryBuy"].ToString()) != 0) || (decimal.Parse(dsEQMIS.Tables[0].Rows[0]["DeliverySell"].ToString()) != 0) ||
                            (decimal.Parse(dsEQMIS.Tables[0].Rows[0]["SpeculativeSell"].ToString()) != 0) || (decimal.Parse(dsEQMIS.Tables[0].Rows[0]["SpeculativeBuy"].ToString()) != 0))
                    {
                        flag = 1;
                    }

                    if (flag == 0)
                    {
                        gvEQMIS.Visible = false;
                        ErrorMessage.Visible = true;
                    }
                    else
                    {
                        gvEQMIS.Visible = true;
                        ErrorMessage.Visible = false;
                        gvEQMIS.DataSource = dtAdviserEQMIS;
                        gvEQMIS.DataBind();
                        gvEQMIS.Columns[0].Visible = false;
                        gvEQMIS.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                        gvEQMIS.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                        //Assigning Text To Header template

                        Label Hlb1 = (Label)gvEQMIS.HeaderRow.FindControl("lblCustIndDelby");
                        Hlb1.Text = "Delivery Buy Value";

                        Label Hlb2 = (Label)gvEQMIS.HeaderRow.FindControl("lblIndMValueDelSell");
                        Hlb2.Text = "Delivery Sell Value";

                        Label Hlb3 = (Label)gvEQMIS.HeaderRow.FindControl("lblMValuePerCSpecSell");
                        Hlb3.Text = "Speculative Sell Value";

                        Label Hlb4 = (Label)gvEQMIS.HeaderRow.FindControl("lblMvalueBlankSpecbuy");
                        Hlb4.Text = "Speculative Buy Value";

                        Label TotalText = (Label)gvEQMIS.FooterRow.FindControl("lblTotalText");

                        TotalText.Text = string.Empty;

                        Label TotalMText = (Label)gvEQMIS.FooterRow.FindControl("lblSectorWiseTotalText");

                        TotalMText.Text = string.Empty;

                        Label TotalMarketValue = (Label)gvEQMIS.FooterRow.FindControl("lblFooterItemMValue");

                        TotalMarketValue.Text = string.Empty;

                        Label TotalValue = (Label)gvEQMIS.FooterRow.FindControl("lblFooterItemMValue");

                        TotalValue.Text = string.Empty;
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
                FunctionInfo.Add("Method", "AdviserEQMIS.ascx.cs:BindGrid()");
                object[] objects = new object[2];
                objects[0] = dtFrom;
                objects[1] = dtTo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindEQMISgrid(DateTime Valuationdate,int ScripCodes)
        {
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            DataSet dsCompSecEQMIS = new DataSet();
            DataTable dtCompSecEQMIS = new DataTable();
            gvEQMIS.DataKeyNames = new string[] { "ScripCodes" };
            
            int adviserId = 0;
            int rmId = 0;
           
            adviserId = advisorVo.advisorId;

            rmVo = (RMVo)Session[SessionContents.RmVo];
            rmId = rmVo.RMId;
           
            hdnPortfolioType.Value = ddlPortfolioGroup.SelectedValue;
            hdnbranchHeadId.Value = ddlBranchForEQ.SelectedValue;
            hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
            hdnrmId.Value = ddlRMEQ.SelectedValue;
            hdnScripCodes.Value = ScripCodes.ToString();


            DateTime Valuation_Date = Convert.ToDateTime(hdnValuationDate.Value.ToString());

            if (userType == "rm")
            {
                dsCompSecEQMIS = adviserMISBo.GetAllUsersEQMISForComSec(userType, Valuation_Date, adviserId, rmId, 0, 0, 0, int.Parse(hdnEQMISType.Value.ToString()), int.Parse(hdnPortfolioType.Value.ToString()),int.Parse(hdnScripCodes.Value.ToString()));
            }
            if (userType == "advisor")
            {
                if (hdnall.Value == "0")
                {
                    dsCompSecEQMIS = adviserMISBo.GetAllUsersEQMISForComSec(userType, Valuation_Date, adviserId, 0, 0, int.Parse(hdnbranchHeadId.Value.ToString()), 0, int.Parse(hdnEQMISType.Value.ToString()), int.Parse(hdnPortfolioType.Value.ToString()),int.Parse(hdnScripCodes.Value.ToString()));
                }
                else if (hdnall.Value == "1")
                {
                    dsCompSecEQMIS = adviserMISBo.GetAllUsersEQMISForComSec(userType, Valuation_Date, adviserId, 0, int.Parse(hdnbranchId.Value.ToString()), 0, 1, int.Parse(hdnEQMISType.Value.ToString()), int.Parse(hdnPortfolioType.Value.ToString()), int.Parse(hdnScripCodes.Value.ToString()));
                }
                else if (hdnall.Value == "2")
                {
                    dsCompSecEQMIS = adviserMISBo.GetAllUsersEQMISForComSec(userType, Valuation_Date, adviserId, int.Parse(hdnrmId.Value.ToString()), 0, int.Parse(hdnbranchHeadId.Value.ToString()), 2, int.Parse(hdnEQMISType.Value.ToString()), int.Parse(hdnPortfolioType.Value.ToString()), int.Parse(hdnScripCodes.Value.ToString()));
                }
                else if (hdnall.Value == "3")
                {
                    dsCompSecEQMIS = adviserMISBo.GetAllUsersEQMISForComSec(userType, Valuation_Date, adviserId, int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), 0, 3, int.Parse(hdnEQMISType.Value.ToString()), int.Parse(hdnPortfolioType.Value.ToString()), int.Parse(hdnScripCodes.Value.ToString()));
                }
            }
            if (userType == "bm")
            {
                if (hdnall.Value == "0")
                {
                    dsCompSecEQMIS = adviserMISBo.GetAllUsersEQMISForComSec(userType, Valuation_Date, adviserId, 0, 0, int.Parse(hdnbranchHeadId.Value.ToString()), 0, int.Parse(hdnEQMISType.Value.ToString()), int.Parse(hdnPortfolioType.Value.ToString()), int.Parse(hdnScripCodes.Value.ToString()));
                }
                else if (hdnall.Value == "1")
                {
                    dsCompSecEQMIS = adviserMISBo.GetAllUsersEQMISForComSec(userType, Valuation_Date, adviserId, 0, int.Parse(hdnbranchId.Value.ToString()), 0, 1, int.Parse(hdnEQMISType.Value.ToString()), int.Parse(hdnPortfolioType.Value.ToString()), int.Parse(hdnScripCodes.Value.ToString()));
                }
                else if (hdnall.Value == "2")
                {
                    dsCompSecEQMIS = adviserMISBo.GetAllUsersEQMISForComSec(userType, Valuation_Date, adviserId, int.Parse(hdnrmId.Value.ToString()), 0, int.Parse(hdnbranchHeadId.Value.ToString()), 2, int.Parse(hdnEQMISType.Value.ToString()), int.Parse(hdnPortfolioType.Value.ToString()), int.Parse(hdnScripCodes.Value.ToString()));
                }
                else if (hdnall.Value == "3")
                {
                    dsCompSecEQMIS = adviserMISBo.GetAllUsersEQMISForComSec(userType, Valuation_Date, adviserId, int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), 0, 3, int.Parse(hdnEQMISType.Value.ToString()), int.Parse(hdnPortfolioType.Value.ToString()), int.Parse(hdnScripCodes.Value.ToString()));
                }
            }

            if (dsCompSecEQMIS.Tables[0].Rows.Count == 0)
            {
                ErrorMessage.Visible = true;
                gvEQMIS.DataSource = dsCompSecEQMIS;
                gvEQMIS.DataBind();
            }
            else
            {
                

                ErrorMessage.Visible = false;

                dtCompSecEQMIS.Columns.Add("ScripCodes");
                dtCompSecEQMIS.Columns.Add("CName_Industry_Delby");
                dtCompSecEQMIS.Columns.Add("Industry_MValue_DelSell");
                dtCompSecEQMIS.Columns.Add("MValue_Percentage_SpecSell");
                dtCompSecEQMIS.Columns.Add("MValue_Blank_SpecBuy");
                

                DataRow drAdvEQMIS;

                for (int i = 0; i < dsCompSecEQMIS.Tables[0].Rows.Count; i++)
                {
                    drAdvEQMIS = dtCompSecEQMIS.NewRow();
                   
                    NumberFormatInfo nfi1 = new CultureInfo("en-US", false).NumberFormat;
                    NumberFormatInfo nfi2 = new CultureInfo("en-US", false).NumberFormat;

                    
                    drAdvEQMIS["ScripCodes"] = dsCompSecEQMIS.Tables[0].Rows[i]["ScripCode"].ToString();
                    if (hdnEQMISType.Value == "0")
                    {
                        drAdvEQMIS["CName_Industry_Delby"] = dsCompSecEQMIS.Tables[0].Rows[i]["CompanyName"].ToString();
                    }
                    else
                        drAdvEQMIS["CName_Industry_Delby"] = string.Empty;

                    drAdvEQMIS["Industry_MValue_DelSell"] = dsCompSecEQMIS.Tables[0].Rows[i]["Industry"].ToString();
                    nfi1.NumberDecimalSeparator = String.Format("{0:0,0}", decimal.Parse(dsCompSecEQMIS.Tables[0].Rows[i]["MarketValue"].ToString()).ToString("N", nfi1));
                    nfi2.NumberDecimalSeparator = String.Format("{0:0,0}", decimal.Parse(dsCompSecEQMIS.Tables[0].Rows[i]["NetAssetsPercentage"].ToString()).ToString("N", nfi2));

                    drAdvEQMIS["MValue_Percentage_SpecSell"] = nfi1.NumberDecimalSeparator;
                    drAdvEQMIS["MValue_Blank_SpecBuy"] = nfi2.NumberDecimalSeparator;

                    dtCompSecEQMIS.Rows.Add(drAdvEQMIS);
                }
                gvEQMIS.DataSource = dtCompSecEQMIS;
                gvEQMIS.DataBind();

                //Assigning Text To Header template
                if (hdnEQMISType.Value == "0")
                {
                    Label Hlb1 = (Label)gvEQMIS.HeaderRow.FindControl("lblCustIndDelby");
                    Hlb1.Text = "Company Name";
                    

                    Label Hlb2 = (Label)gvEQMIS.HeaderRow.FindControl("lblIndMValueDelSell");
                    Hlb2.Text = "Industry";

                    Label Hlb3 = (Label)gvEQMIS.HeaderRow.FindControl("lblMValuePerCSpecSell");
                    Hlb3.Text = "Market Value";

                    Label Hlb4 = (Label)gvEQMIS.HeaderRow.FindControl("lblMvalueBlankSpecbuy");
                    Hlb4.Text = "% to net Assets";

                    Label TotalText = (Label)gvEQMIS.FooterRow.FindControl("lblTotalText");

                    TotalText.Text = "Total";

                    Label TotalMarketValue = (Label)gvEQMIS.FooterRow.FindControl("lblFooterItemMValue");

                    TotalMarketValue.Text = decimal.Parse((dsCompSecEQMIS.Tables[2].Rows[0]["Total"].ToString()).ToString()).ToString();
                    TotalMarketValue.Style.Add("float", "right");

                    Label TotalPerValue = (Label)gvEQMIS.FooterRow.FindControl("lblFooterItemMValueBlankSpecBuy");

                    TotalPerValue.Text = "100%";
                    TotalPerValue.Style.Add("float", "right");


                    
                    //TotalValue.Text = drAdvEQMIS[dsCompSecEQMIS.Tables[0].Columns.Count - 1].ToString();

                }
                else if (hdnEQMISType.Value == "1")
                {
                    Label Hlb2 = (Label)gvEQMIS.HeaderRow.FindControl("lblIndMValueDelSell");
                    Hlb2.Text = "Industry";

                    Label Hlb3 = (Label)gvEQMIS.HeaderRow.FindControl("lblMValuePerCSpecSell");
                    Hlb3.Text = "Market Value";

                    Label Hlb4 = (Label)gvEQMIS.HeaderRow.FindControl("lblMvalueBlankSpecbuy");
                    Hlb4.Text = "% to net Assets";

                    Label TotalText = (Label)gvEQMIS.FooterRow.FindControl("lblSectorWiseTotalText");

                    TotalText.Text = "Total";

                    Label TotalValue = (Label)gvEQMIS.FooterRow.FindControl("lblFooterItemMValue");

                    TotalValue.Text = decimal.Parse((dsCompSecEQMIS.Tables[2].Rows[0]["Total"].ToString()).ToString()).ToString();
                    TotalValue.Style.Add("float", "right");

                    Label TotalPerValue = (Label)gvEQMIS.FooterRow.FindControl("lblFooterItemMValueBlankSpecBuy");

                    TotalPerValue.Text = "100%";
                    TotalPerValue.Style.Add("float", "right");
                    
                    gvEQMIS.Columns[0].Visible = false;
                    gvEQMIS.Columns[1].Visible = false;
                    //gvEQMIS.Columns[1].Visible = false;
                }

               

            }

        }

        /* End */ 


        protected void rbtnDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPickDate.Checked == true)
            {
                trRange.Visible = true;
                trPeriod.Visible = false;
            }
            else if (rbtnPickPeriod.Checked == true)
            {
                trRange.Visible = false;
                trPeriod.Visible = true;
                BindPeriodDropDown();
            }
        }

        private void BindPeriodDropDown()
        {
            DataTable dtPeriod;
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            dtPeriod = XMLBo.GetDatePeriod(path);

            ddlPeriod.DataSource = dtPeriod;
            ddlPeriod.DataTextField = "PeriodType";
            ddlPeriod.DataValueField = "PeriodCode";
            ddlPeriod.DataBind();
            ddlPeriod.Items.Insert(0, new ListItem("Select a Period", "Select a Period"));
            ddlPeriod.Items.RemoveAt(15);
        }

        protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateEQMIS();
        }

        public void GereratePickPeriod()
        {
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();
            DateBo dtBo = new DateBo();

            if (ddlPeriod.SelectedIndex != 0)
            {
                dtBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue, out dtFrom, out dtTo);
                this.BindGrid(dtFrom, dtTo);
            }
            else
            {

            }

        }


        protected void btnGo_Click(object sender, EventArgs e)
        {
            GenerateEQMIS();
        }
        public void GenerateEQMIS()
        {
            CultureInfo ci = new CultureInfo("en-GB");

            DateTime convertedFromDate = new DateTime();
            DateTime convertedToDate = new DateTime();

            LatestValuationdate = Convert.ToDateTime(txtEQDate.Text);
            hdnValuationDate.Value = LatestValuationdate.ToString();
            DateTime Valuation_Date = Convert.ToDateTime(hdnValuationDate.Value.ToString());
            if(txtFromDate.Text != "")
                convertedFromDate = Convert.ToDateTime(txtFromDate.Text.Trim(), ci);
            if(txtToDate.Text != "")
                convertedToDate = Convert.ToDateTime(txtToDate.Text.Trim(), ci);

            /* For Turn Over EQ */
            if ((userType == "advisor") && (ddlMISType.SelectedValue == "TurnOverSummery"))
            {
                if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex == 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "0";
                    hdnrmId.Value = "0";
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);
                }
                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex == 0))
                {

                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = "0";
                    hdnall.Value = "1";
                    hdnrmId.Value = "0";
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);

                }

                else if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "2";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);
                }

                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = "0";
                    hdnall.Value = "3";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);
                }
            }
            else if ((userType == "rm") && (ddlMISType.SelectedValue == "TurnOverSummery"))
            {
                rmId = rmVo.RMId;
                hdnbranchId.Value = "0";
                hdnbranchHeadId.Value = "0";
                hdnrmId.Value = rmId.ToString();
                hdnall.Value = "0";
                if (rbtnPickPeriod.Checked == true)
                    GereratePickPeriod();
                else
                    this.BindGrid(convertedFromDate, convertedToDate);
            }
            else if ((userType == "bm") && (ddlMISType.SelectedValue == "TurnOverSummery"))
            {
                if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex == 0))
                {

                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "2";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);
                }

                else if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "3";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);
                }

                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex == 0))
                {

                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "1";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);
                }

                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "0";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    if (rbtnPickPeriod.Checked == true)
                        GereratePickPeriod();
                    else
                        this.BindGrid(convertedFromDate, convertedToDate);
                }

            }
            /* ********** */

            /* For CompanyWise EQ */
            if ((userType == "advisor") && (ddlMISType.SelectedValue == "CompanyWise"))
            {
                if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex == 0))
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "0";
                    hdnrmId.Value = "0";
                    this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                }
                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex == 0))
                {
                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = "0";
                    hdnall.Value = "1";
                    hdnrmId.Value = "0";
                    this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                }
                else if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "2";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                }
                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = "0";
                    hdnall.Value = "3";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                }
            }
            else if ((userType == "bm") && (ddlMISType.SelectedValue == "CompanyWise"))
            {
                if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex == 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "0";
                    hdnrmId.Value = "0";
                    this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                }
                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex == 0))
                {

                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = "0";
                    hdnall.Value = "1";
                    hdnrmId.Value = "0";
                    this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                }

                else if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "2";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                }

                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = "0";
                    hdnall.Value = "3";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                }

            }
            else if ((userType == "rm") && (ddlMISType.SelectedValue == "CompanyWise"))
            {
                hdnbranchId.Value = "0";
                rmId = rmVo.RMId;
                hdnbranchHeadId.Value = "0";
                this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
            }
            /* ************* */

            /* For Sector Wise */
            if ((userType == "advisor") && (ddlMISType.SelectedValue == "SectorWise"))
            {
                if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex == 0))
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "0";
                    hdnrmId.Value = "0";
                    this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                }
                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex == 0))
                {
                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = "0";
                    hdnall.Value = "1";
                    hdnrmId.Value = "0";
                    this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                }
                else if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "2";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                }
                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = "0";
                    hdnall.Value = "3";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                }
            }
            else if ((userType == "bm") && (ddlMISType.SelectedValue == "SectorWise"))
            {
                if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex == 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "0";
                    hdnrmId.Value = "0";
                    this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                }
                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex == 0))
                {

                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = "0";
                    hdnall.Value = "1";
                    hdnrmId.Value = "0";
                    this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                }

                else if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "2";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
                }

                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex != 0))
                {
                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnbranchHeadId.Value = "0";
                    hdnall.Value = "3";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    this.BindGrid(convertedFromDate, convertedToDate);
                }

            }
            else if ((userType == "rm") && (ddlMISType.SelectedValue == "SectorWise"))
            {
                hdnbranchId.Value = "0";
                rmId = rmVo.RMId;
                hdnbranchHeadId.Value = "0";
                this.BindEQMISgrid(Valuation_Date, ScripCodeForCompany);
            }
        }

        protected void ddlBranchForEQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnBranchSelection.Value = ddlBranchForEQ.SelectedValue.ToString();
            if (ddlBranchForEQ.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID, 1);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranchForEQ.SelectedValue.ToString()), 0, 0);
            }
            GenerateEQMIS();
        }


        /* For Binding the Branch Dropdowns */

        private void BindBranchForBMDropDown()
        {

            try
            {

                DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(0, bmID, 0);
                if (ds != null)
                {
                    ddlBranchForEQ.DataSource = ds.Tables[1]; ;
                    ddlBranchForEQ.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                    ddlBranchForEQ.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                    ddlBranchForEQ.DataBind();
                }
                ddlBranchForEQ.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
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
                    ddlRMEQ.DataSource = ds.Tables[0]; ;
                    ddlRMEQ.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                    ddlRMEQ.DataTextField = ds.Tables[0].Columns["RM Name"].ToString();
                    ddlRMEQ.DataBind();
                }
                ddlRMEQ.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserEQMIS.ascx:BindRMforBranchDropdown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlRMEQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnRMSelection.Value = ddlRMEQ.SelectedValue.ToString();
            GenerateEQMIS();
        }

        /* ********** */

        /*for AdviserAssociateCategorySetup drop down */

        private void BindBranchDropDown()
        {
            
            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null)
                {
                    ddlBranchForEQ.DataSource = ds;
                    ddlBranchForEQ.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlBranchForEQ.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlBranchForEQ.DataBind();
                }
                ddlBranchForEQ.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", advisorVo.advisorId.ToString()));
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
                    ddlRMEQ.DataSource = dt;
                    ddlRMEQ.DataValueField = dt.Columns["AR_RMId"].ToString();
                    ddlRMEQ.DataTextField = dt.Columns["RMName"].ToString();
                    ddlRMEQ.DataBind();
                }
                ddlRMEQ.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "2"));
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

        protected void ddlMISType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindMISTypeDropDown();
            
        }

        protected void ddlPortfolioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateEQMIS();
        }

        protected void gvEQMIS_SelectedIndexChanged(object sender, EventArgs e)
        {
            int scripCode;
            scripCode = int.Parse(gvEQMIS.SelectedDataKey["ScripCodes"].ToString());
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserEQMIS','ScripCode=" + scripCode + "&BranchSelection=" + hdnBranchSelection.Value + "&RMSelection=" + hdnRMSelection.Value + "');", true);
        }

        protected void gvEQMIS_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

    }
}
