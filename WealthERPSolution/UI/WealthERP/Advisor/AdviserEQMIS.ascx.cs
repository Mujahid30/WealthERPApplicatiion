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


        UserVo userVo = new UserVo();
        int bmID;
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();

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
            try
            {
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
                if (userType == "advisor")
                {
                    if (!IsPostBack)
                    {
                        BindBranchDropDown();
                        BindRMDropDown();
                        trRange.Visible = true;
                        trPeriod.Visible = false;
                    }
                    //dsGetLastTradeDate = customertransactionbo.GetLastTradeDate();
                    //DateTime dtLastTradeDate;
                }
                else if (userType == "bm")
                {
                    if (!IsPostBack)
                    {
                        BindBranchForBMDropDown();
                        BindRMforBranchDropdown(0, bmID, 1);
                        trRange.Visible = true;
                        trPeriod.Visible = false;
                    }
                }
                    dsGetLastTradeDate = customertransactionbo.GetLastTradeDate();
                    DateTime dtLastTradeDate;
                
                
                if (dsGetLastTradeDate.Tables[0].Rows.Count != 0)
                {
                    dtLastTradeDate=(DateTime)dsGetLastTradeDate.Tables[0].Rows[0]["WTD_Date"];

                    txtFromDate.Text = dtLastTradeDate.ToShortDateString();
                    txtToDate.Text = dtLastTradeDate.ToShortDateString();

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

        private void BindGrid(DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsEQMIS = new DataSet();
            DataTable dtAdviserEQMIS = new DataTable();
            
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
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, 0, 0, int.Parse(hdnbranchHeadId.Value.ToString()), 1);
                    }
                    else if (hdnall.Value == "3")
                    {
                        hdnbranchHeadId.Value = ddlBranchForEQ.SelectedValue;
                        hdnrmId.Value = ddlRMEQ.SelectedValue;
                        dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo, int.Parse(hdnrmId.Value.ToString()), 0, int.Parse(hdnbranchHeadId.Value.ToString()), 1);
                    }
                    dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo,0, 0, 0, 0);
                }
            }

            try
            {
                dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo,0,0,0,0);

                if (dsEQMIS.Tables[0].Rows.Count > 0)
                {
                    trMessage.Visible = false;
                    lblMessage.Visible = false;

                    dtAdviserEQMIS.Columns.Add("DeliveryBuy");
                    dtAdviserEQMIS.Columns.Add("DeliverySell");
                    dtAdviserEQMIS.Columns.Add("SpeculativeSell");
                    dtAdviserEQMIS.Columns.Add("SpeculativeBuy");

                    DataRow drAdvEQMIS;

                    for (int i = 0; i < dsEQMIS.Tables[0].Rows.Count; i++)
                    {
                        drAdvEQMIS = dtAdviserEQMIS.NewRow();

                        drAdvEQMIS[0] = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["DeliveryBuy"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drAdvEQMIS[1] = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["DeliverySell"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drAdvEQMIS[2] = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["SpeculativeSell"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drAdvEQMIS[3] = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["SpeculativeBuy"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                        dtAdviserEQMIS.Rows.Add(drAdvEQMIS);
                    }

                    gvEQMIS.DataSource = dtAdviserEQMIS;
                    gvEQMIS.DataBind();
                }
                else
                {
                    trMessage.Visible = true;
                    lblMessage.Visible = true;
                    gvEQMIS.Visible = false;
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
        }

        protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
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
            CultureInfo ci = new CultureInfo("en-GB");

            DateTime convertedFromDate = new DateTime();
            DateTime convertedToDate = new DateTime();

            convertedFromDate = Convert.ToDateTime(txtFromDate.Text.Trim(), ci);
            convertedToDate = Convert.ToDateTime(txtToDate.Text.Trim(), ci);

           

            /* For BM MIS */
            if (userType == "bm")
            {
                if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex == 0))
                {
                    hdnbranchId.Value = "0";
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnall.Value = "2";
                    //hdnXWise.Value = "1";
                    hdnrmId.Value = ddlRMEQ.SelectedValue;

                    //dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), hdnAMCSearchVal.Value.ToString(), out count, 0);
                    this.BindGrid(convertedFromDate, convertedToDate);
                }
            }
            else if (userType == "advisor")
            {
                hdnbranchId.Value = "0";
                hdnbranchHeadId.Value = advisorId.ToString();
                hdnall.Value = "2";
               // hdnXWise.Value = "1";
                hdnrmId.Value = ddlRMEQ.SelectedValue;

               // dsMISReport = adviserMISBo.GetMISForBM(int.Parse(hdnrmId.Value.ToString()), int.Parse(hdnbranchId.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnXWise.Value.ToString()), int.Parse(hdnAll.Value.ToString()), DateTime.Parse(hdnValuationDate.Value.ToString()), hdnAMCSearchVal.Value.ToString(), out count, 0);
                this.BindGrid(convertedFromDate, convertedToDate);
            }

            if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex == 0))
            {
                hdnbranchId.Value = "0";
                hdnbranchHeadId.Value = ddlBranchForEQ.SelectedValue;
                hdnall.Value = "2";
                hdnrmId.Value = ddlRMEQ.SelectedValue;

               
                this.BindGrid(convertedFromDate, convertedToDate);
            }
            else if ((ddlBranchForEQ.SelectedIndex == 0) && (ddlRMEQ.SelectedIndex != 0))
            {
                hdnbranchId.Value = "0";
                hdnbranchHeadId.Value = ddlBranchForEQ.SelectedValue;
                hdnall.Value = "3";
                hdnrmId.Value = ddlRMEQ.SelectedValue;

                
                this.BindGrid(convertedFromDate, convertedToDate);
            }
            else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex == 0))
            {
                hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                hdnbranchHeadId.Value = ddlBranchForEQ.SelectedValue;
                hdnall.Value = "1";
                hdnrmId.Value = ddlRMEQ.SelectedValue;

                this.BindGrid(convertedFromDate, convertedToDate);
            }
            else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex != 0))
            {
                hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                hdnbranchHeadId.Value = ddlBranchForEQ.SelectedValue;
                hdnall.Value = "0";
                hdnrmId.Value = ddlRMEQ.SelectedValue;

                this.BindGrid(convertedFromDate, convertedToDate);
            }

            /* ********** */
        }

        protected void ddlBranchForEQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranchForEQ.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID, 1);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranchForEQ.SelectedValue.ToString()), 0, 0);
            }
            
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

        }

        /* End For Binding the RM Dropdowns */

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
                ddlBranchForEQ.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
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

    }
}