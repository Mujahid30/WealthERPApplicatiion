using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using BoAdvisorProfiling;
using VoUser;
using BoUploads;
using BoCustomerGoalProfiling;
using Telerik.Web.UI;
using BoCommon;
using System.Configuration;

namespace WealthERP.BusinessMIS
{
    public partial class EquityTurnover : System.Web.UI.UserControl
    {
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        RMVo rmVo = new RMVo();
        UserVo userVo = new UserVo();
        DateTime dtTo = new DateTime();
        DateBo dtBo = new DateBo();
        DateTime dtFrom = new DateTime();

        int advisorId = 0;
        String userType;
        int rmId = 0;
        int bmID = 0;
        string path = string.Empty;
        DateTime LatestValuationdate;
        DataSet dsEQMIS = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session["userVo"];
            AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            advisorId = advisorVo.advisorId;
            int RMId = rmVo.RMId;
            rmId = rmVo.RMId;
            bmID = rmVo.RMId;

            imgOrgGridExport.Visible = false;
            imgProductGridExport.Visible = false;
            pnlEQTurnover.Visible = false;
            pnlOrgLevel.Visible = false;
            gvEQTurnover.Visible = false;
            gvOrgLevel.Visible = false;


            if (!IsPostBack)
            {
                trEQProductLevel.Visible = false;
                trOrgLevel.Visible = false;
                rbtnPickDate.Checked = true;
                rbtnPickPeriod.Checked = false;
                divDateRange.Visible = true;
                divDatePeriod.Visible = false;
                
                if (userType == "advisor")
                {
                    BindBranchDropDown();
                    BindRMDropDown();
                }
                else if (userType == "rm")
                {
                    trBranchRmDpRow.Visible = false;
                }
                if (userType == "bm")
                {
                    BindBranchForBMDropDown();
                    BindRMforBranchDropdown(0, bmID);
                }
                LatestValuationdate = adviserMFMIS.GetLatestValuationDateFromHistory(advisorId, "EQ");
                txtFromDate.SelectedDate = DateTime.Parse(LatestValuationdate.ToShortDateString());
                txtToDate.SelectedDate = DateTime.Parse(LatestValuationdate.ToShortDateString());
            }
        }
        private void BindBranchDropDown()
        {

            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
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

                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        {

            try
            {

                DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
                if (ds != null)
                {
                    ddlRMEQ.DataSource = ds.Tables[0]; ;
                    ddlRMEQ.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                    ddlRMEQ.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
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

        protected void rbtnDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPickDate.Checked == true)
            {
                divDateRange.Visible = true;
                divDatePeriod.Visible = false;
            }
            else if (rbtnPickPeriod.Checked == true)
            {
                divDateRange.Visible = false;
                divDatePeriod.Visible = true;
                BindPeriodDropDown();
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
            ddlPeriod.Items.RemoveAt(15);
        }
        protected void ddlBranchForEQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranchForEQ.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranchForEQ.SelectedValue.ToString()), 0);
            }

        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            SetParameters();
            if(ddlAction.SelectedValue=="Product")
            {
                trOrgLevel.Visible = false;
                trEQProductLevel.Visible = true;
                lblMFMISType.Text = "Company/Sector/Exchange";
                BindProductLevelGrid();
            }
            else if (ddlAction.SelectedValue == "Organization")
            {
                trOrgLevel.Visible = true;
                trEQProductLevel.Visible = false;
                lblMFMISType.Text = "Branch/RM/Customer";
                BindOrganisationLevelGrid();
            }
        }

        private void BindOrganisationLevelGrid()
        {
            int branchId = 0;
            int branchIdOld = 0;
            DataTable dtOrgLevel = new DataTable();
            dsEQMIS = adviserMFMIS.GetEQMIS(userType, int.Parse(hdnadviserId.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value));
            dtOrgLevel = dsEQMIS.Tables[1];
            if (dtOrgLevel == null)
            {
                pnlOrgLevel.Visible = true;
                gvOrgLevel.Visible = true;
                gvOrgLevel.DataSource = dtOrgLevel;
                gvOrgLevel.DataBind();
            }
            else
            {
                DataTable dtEQOrgNew = new DataTable();
                dtEQOrgNew.Columns.Add("AB_BranchId");
                dtEQOrgNew.Columns.Add("AB_BranchName");
                dtEQOrgNew.Columns.Add("AR_FirstName");
                dtEQOrgNew.Columns.Add("Customer");
                dtEQOrgNew.Columns.Add("DeliveryBuy", typeof(Double));
                dtEQOrgNew.Columns.Add("DeliveryBuyCount", typeof(int));
                dtEQOrgNew.Columns.Add("DeliverySell", typeof(Double));
                dtEQOrgNew.Columns.Add("DeliverySellCount", typeof(int));
                dtEQOrgNew.Columns.Add("SpeculativeBuy", typeof(Double));
                dtEQOrgNew.Columns.Add("SpeculativeBuyCount", typeof(int));
                dtEQOrgNew.Columns.Add("SpeculativeSell", typeof(Double));
                dtEQOrgNew.Columns.Add("SpeculativeSellCount", typeof(int));

                #region Data Table Default value

                dtEQOrgNew.Columns["DeliveryBuy"].DefaultValue = 0;
                dtEQOrgNew.Columns["DeliveryBuyCount"].DefaultValue = 0;
                dtEQOrgNew.Columns["DeliverySell"].DefaultValue = 0;
                dtEQOrgNew.Columns["DeliverySellCount"].DefaultValue = 0;

                dtEQOrgNew.Columns["SpeculativeBuy"].DefaultValue = 0;
                dtEQOrgNew.Columns["SpeculativeBuyCount"].DefaultValue = 0;
                dtEQOrgNew.Columns["SpeculativeSell"].DefaultValue = 0;
                dtEQOrgNew.Columns["SpeculativeSellCount"].DefaultValue = 0;

                #endregion Data Table Default value

                DataRow drGetEQOrgNew;
                DataRow[] drEQOrgWise;
                if (dtEQOrgNew != null)
                {
                    DataTable dtGetEQOrg = dsEQMIS.Tables[1];

                    foreach (DataRow drEQgOrTransaction in dtGetEQOrg.Rows)
                    {

                        Int32.TryParse(drEQgOrTransaction["AB_BranchId"].ToString(), out branchId);
                        if (branchId != branchIdOld)
                        { //go for another row to find new customer
                            branchIdOld = branchId;
                            drGetEQOrgNew = dtEQOrgNew.NewRow();
                            if (branchId != 0)
                            { // add row in manual datatable within this brace end
                                drEQOrgWise = dtGetEQOrg.Select("AB_BranchId=" + branchId.ToString());
                                drGetEQOrgNew["AB_BranchId"] = drEQgOrTransaction["AB_BranchId"].ToString();
                                drGetEQOrgNew["AB_BranchName"] = drEQgOrTransaction["AB_BranchName"].ToString();
                                drGetEQOrgNew["AR_FirstName"] = drEQgOrTransaction["AR_FirstName"].ToString();
                                drGetEQOrgNew["Customer"] = drEQgOrTransaction["Customer"].ToString();
                                if (drEQOrgWise.Count() > 0)
                                {
                                    foreach (DataRow dr in drEQOrgWise)
                                    {

                                        string transactiontype = dr["CET_BuySell"].ToString();
                                        switch (transactiontype)
                                        {
                                            case "B":
                                                {
                                                    if (int.Parse(dr["CET_IsSpeculative"].ToString()) == 0)
                                                    {
                                                        drGetEQOrgNew["DeliveryBuy"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                        drGetEQOrgNew["DeliveryBuyCount"] = dr["TrnsCount"].ToString();
                                                    }
                                                    else
                                                    {
                                                        drGetEQOrgNew["SpeculativeBuy"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                        drGetEQOrgNew["SpeculativeBuyCount"] = dr["TrnsCount"].ToString();
                                                    }
                                                    break;
                                                }
                                            case "S":
                                                {
                                                    if (int.Parse(dr["CET_IsSpeculative"].ToString()) == 0)
                                                    {
                                                        drGetEQOrgNew["DeliverySell"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                        drGetEQOrgNew["DeliverySellCount"] = dr["TrnsCount"].ToString();
                                                    }
                                                    else
                                                    {
                                                        drGetEQOrgNew["SpeculativeSell"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                        drGetEQOrgNew["SpeculativeSellCount"] = dr["TrnsCount"].ToString();
                                                    }
                                                    break;
                                                }

                                        }

                                    }
                                }
                                dtEQOrgNew.Rows.Add(drGetEQOrgNew);
                            }//*

                        }//**

                    }//***
                    pnlOrgLevel.Visible = true;
                    gvOrgLevel.Visible = true;
                    gvOrgLevel.DataSource = dtEQOrgNew;
                    gvOrgLevel.DataBind();
                    imgOrgGridExport.Visible = true;
                    this.gvOrgLevel.GroupingSettings.RetainGroupFootersVisibility = true;
                    if (Cache["gvOrgLevel" + userVo.UserId + userType] == null)
                    {
                        Cache.Insert("gvOrgLevel" + userVo.UserId + userType, dtEQOrgNew);
                    }
                    else
                    {
                        Cache.Remove("gvOrgLevel" + userVo.UserId + userType);
                        Cache.Insert("gvOrgLevel" + userVo.UserId + userType, dtEQOrgNew);
                    }
                }
            }
        }
        private void BindProductLevelGrid()
        {
            int amcCode = 0;
            int amcCodeOld = 0;
            DataTable dtAdviserEQMIS = new DataTable();
            dsEQMIS = adviserMFMIS.GetEQMIS(userType, int.Parse(hdnadviserId.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value));
            dtAdviserEQMIS = dsEQMIS.Tables[0];

            if (dtAdviserEQMIS == null)
            {
                gvEQTurnover.DataSource = dtAdviserEQMIS;
                gvEQTurnover.DataBind();
                pnlEQTurnover.Visible = true;
                gvEQTurnover.Visible = true;
            }
            else
            {
                DataTable dtEQProductNew = new DataTable();
                dtEQProductNew.Columns.Add("PEM_ScripCode");
                dtEQProductNew.Columns.Add("PEM_CompanyName");
                dtEQProductNew.Columns.Add("PGSC_SectorCategoryName");
                dtEQProductNew.Columns.Add("Exchange");
                dtEQProductNew.Columns.Add("DeliveryBuy", typeof(Double));
                dtEQProductNew.Columns.Add("DeliveryBuyCount", typeof(int));
                dtEQProductNew.Columns.Add("DeliverySell", typeof(Double));
                dtEQProductNew.Columns.Add("DeliverySellCount", typeof(int));
                dtEQProductNew.Columns.Add("SpeculativeBuy", typeof(Double));
                dtEQProductNew.Columns.Add("SpeculativeBuyCount", typeof(int));
                dtEQProductNew.Columns.Add("SpeculativeSell", typeof(Double));
                dtEQProductNew.Columns.Add("SpeculativeSellCount", typeof(int));

                #region Data Table Default value

                dtEQProductNew.Columns["DeliveryBuy"].DefaultValue = 0;
                dtEQProductNew.Columns["DeliveryBuyCount"].DefaultValue = 0;
                dtEQProductNew.Columns["DeliverySell"].DefaultValue = 0;
                dtEQProductNew.Columns["DeliverySellCount"].DefaultValue = 0;

                dtEQProductNew.Columns["SpeculativeBuy"].DefaultValue = 0;
                dtEQProductNew.Columns["SpeculativeBuyCount"].DefaultValue = 0;
                dtEQProductNew.Columns["SpeculativeSell"].DefaultValue = 0;
                dtEQProductNew.Columns["SpeculativeSellCount"].DefaultValue = 0;

                #endregion Data Table Default value

                DataRow drGetEQProductNew;
                DataRow[] drEQProductWise;
                if (dtAdviserEQMIS != null)
                {
                    DataTable dtGetEQProduct = dsEQMIS.Tables[0];

                    foreach (DataRow drEQProdTransaction in dtGetEQProduct.Rows)
                    {

                        Int32.TryParse(drEQProdTransaction["PEM_ScripCode"].ToString(), out amcCode);
                        if (amcCode != amcCodeOld)
                        { //go for another row to find new customer
                            amcCodeOld = amcCode;
                            drGetEQProductNew = dtEQProductNew.NewRow();
                            if (amcCode != 0)
                            { // add row in manual datatable within this brace end
                                drEQProductWise = dtGetEQProduct.Select("PEM_ScripCode=" + amcCode.ToString());
                                drGetEQProductNew["PEM_ScripCode"] = drEQProdTransaction["PEM_ScripCode"].ToString();
                                drGetEQProductNew["PEM_CompanyName"] = drEQProdTransaction["PEM_CompanyName"].ToString();
                                drGetEQProductNew["PGSC_SectorCategoryName"] = drEQProdTransaction["PGSC_SectorCategoryName"].ToString();
                                drGetEQProductNew["Exchange"] = drEQProdTransaction["XE_ExchangeCode"].ToString();
                                if (drEQProductWise.Count() > 0)
                                {
                                    foreach (DataRow dr in drEQProductWise)
                                    {

                                        string transactiontype = dr["CET_BuySell"].ToString();
                                        switch (transactiontype)
                                        {
                                            case "B":
                                                {
                                                    if (int.Parse(dr["CET_IsSpeculative"].ToString()) == 0)
                                                    {
                                                        drGetEQProductNew["DeliveryBuy"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                        drGetEQProductNew["DeliveryBuyCount"] = dr["TrnsCount"].ToString();
                                                    }
                                                    else
                                                    {
                                                        drGetEQProductNew["SpeculativeBuy"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                        drGetEQProductNew["SpeculativeBuyCount"] = dr["TrnsCount"].ToString();
                                                    }
                                                    break;
                                                }
                                            case "S":
                                                {
                                                    if (int.Parse(dr["CET_IsSpeculative"].ToString()) == 0)
                                                    {
                                                        drGetEQProductNew["DeliverySell"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                        drGetEQProductNew["DeliverySellCount"] = dr["TrnsCount"].ToString();
                                                    }
                                                    else
                                                    {
                                                        drGetEQProductNew["SpeculativeSell"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                        drGetEQProductNew["SpeculativeSellCount"] = dr["TrnsCount"].ToString();
                                                    }
                                                    break;
                                                }

                                        }

                                    }
                                }
                                dtEQProductNew.Rows.Add(drGetEQProductNew);
                            }//*

                        }//**

                    }//***


                    pnlEQTurnover.Visible = true;
                    gvEQTurnover.Visible = true;
                    gvEQTurnover.DataSource = dtEQProductNew;
                    gvEQTurnover.DataBind();
                    imgProductGridExport.Visible = true;
                    this.gvEQTurnover.GroupingSettings.RetainGroupFootersVisibility = true;
                    if (Cache["gvEQTurnover" + userVo.UserId + userType] == null)
                    {
                        Cache.Insert("gvEQTurnover" + userVo.UserId + userType, dtEQProductNew);
                    }
                    else
                    {
                        Cache.Remove("gvEQTurnover" + userVo.UserId + userType);
                        Cache.Insert("gvEQTurnover" + userVo.UserId + userType, dtEQProductNew);
                    }
                }

            }
        }

        private void SetParameters()
        {
            if (userType == "advisor")
            {
                if (ddlBranchForEQ.SelectedIndex == 0 && ddlRMEQ.SelectedIndex == 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnAll.Value = "0";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex == 0))
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranchForEQ.SelectedIndex == 0 && ddlRMEQ.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRMEQ.SelectedValue; ;
                }
                else if (ddlBranchForEQ.SelectedIndex != 0 && ddlRMEQ.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    hdnAll.Value = "3";
                }

            }
            else if (userType == "rm")
            {
                hdnrmId.Value = rmVo.RMId.ToString();
                hdnAll.Value = "0";

            }
            else if (userType == "bm")
            {
                if (ddlBranchForEQ.SelectedIndex == 0 && ddlRMEQ.SelectedIndex == 0)
                {

                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnAll.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if ((ddlBranchForEQ.SelectedIndex != 0) && (ddlRMEQ.SelectedIndex == 0))
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranchForEQ.SelectedIndex == 0 && ddlRMEQ.SelectedIndex != 0)
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRMEQ.SelectedValue; ;
                }
                else if (ddlBranchForEQ.SelectedIndex != 0 && ddlRMEQ.SelectedIndex != 0)
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranchForEQ.SelectedValue;
                    hdnrmId.Value = ddlRMEQ.SelectedValue;
                    hdnAll.Value = "3";
                }
            }
            if (hdnbranchHeadId.Value == "")
                hdnbranchHeadId.Value = "0";

            if (hdnbranchId.Value == "")
                hdnbranchId.Value = "0";

            if (hdnadviserId.Value == "")
                hdnadviserId.Value = "0";

            if (hdnrmId.Value == "")
                hdnrmId.Value = "0";

            if (rbtnPickDate.Checked)
            {
                hdnFromDate.Value = txtFromDate.SelectedDate.ToString();
                hdnToDate.Value = txtToDate.SelectedDate.ToString();
            }
            else
            {
                if (ddlPeriod.SelectedIndex != 0)
                {
                    dtBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue, out dtFrom, out dtTo);
                    hdnFromDate.Value = dtFrom.ToShortDateString();
                    hdnToDate.Value = dtTo.ToShortDateString();
                }
            }

        }

        protected void imgProductGridExport_Click(object sender, ImageClickEventArgs e)
        {
            gvEQTurnover.ExportSettings.OpenInNewWindow = true;
            gvEQTurnover.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvEQTurnover.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvEQTurnover.MasterTableView.ExportToExcel();
        }

        protected void imgOrgGridExport_Click(object sender, ImageClickEventArgs e)
        {
            gvOrgLevel.ExportSettings.OpenInNewWindow = true;
            gvOrgLevel.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvEQTurnover.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvOrgLevel.MasterTableView.ExportToExcel();
        }
        protected void gvEQTurnover_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtEQProductNew = new DataTable();
            dtEQProductNew = (DataTable)Cache["gvEQTurnover" + userVo.UserId + userType];
            gvEQTurnover.DataSource = dtEQProductNew;
            gvEQTurnover.Visible = true;
            imgOrgGridExport.Visible = false;
            imgProductGridExport.Visible = true;
            pnlEQTurnover.Visible = true;
            gvEQTurnover.Visible = true;
        }
        protected void gvOrgLevel_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtOrgLevel = new DataTable();
            dtOrgLevel = (DataTable)Cache["gvOrgLevel" + userVo.UserId + userType];
            gvOrgLevel.DataSource = dtOrgLevel;
            gvOrgLevel.Visible = true;
            imgOrgGridExport.Visible = true;
            imgProductGridExport.Visible = false;
            pnlOrgLevel.Visible = true;
            gvOrgLevel.Visible = true;
        }

     }
}