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
using BoCustomerPortfolio;

namespace WealthERP.BusinessMIS
{
    public partial class EquityReturns : System.Web.UI.UserControl
    {
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        RMVo rmVo = new RMVo();
        UserVo userVo = new UserVo();
        Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();

        int advisorId = 0;
        String userType;
        int rmId = 0;
        int bmID = 0;
        int all = 0;
        int branchId = 0;
        int branchHeadId = 0;
        string strValuationDate;

        public enum Constants
        {
            EQ = 0,    
            EQDate = 2
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session["userVo"];
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            if (Session[SessionContents.ValuationDate] == null)
                GetLatestValuationDate();
            genDict = (Dictionary<string, DateTime>)Session[SessionContents.ValuationDate];
            strValuationDate = genDict[Constants.EQDate.ToString()].ToShortDateString();
            lblPickDate.Text = DateTime.Parse(genDict[Constants.EQDate.ToString()].ToString()).ToShortDateString();

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
            if (!IsPostBack)
            {
                SetParameters();
                BindEQReturnsGrid();
            }
        }
        private void GetLatestValuationDate()
        {
            DateTime EQValuationDate = new DateTime();
            PortfolioBo portfolioBo = null;
            genDict = new Dictionary<string, DateTime>();
            AdvisorVo advisorVo = new AdvisorVo();
            int adviserId = 0;
            try
            {
                portfolioBo = new PortfolioBo();
                advisorVo = (AdvisorVo)Session["advisorVo"];
                adviserId = advisorVo.advisorId;


                if (portfolioBo.GetLatestValuationDate(adviserId, "EQ") != null)
                {
                    EQValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserId, "EQ").ToString());
                }
                genDict.Add("EQDate", EQValuationDate);
                Session["ValuationDate"] = genDict;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioDashboard.ascx.cs:GetLatestValuationDate()");
                object[] objects = new object[2];
                objects[0] = EQValuationDate;
                objects[1] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void BindEQReturnsGrid()
        {
            DataTable dtEQReturns;
            dtEQReturns = adviserMFMIS.GetEQReturnsDetails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), strValuationDate);
            if (dtEQReturns != null)
            {
                gvEQReturns.DataSource = dtEQReturns;
                gvEQReturns.DataBind();
                pnlEQReturns.Visible = true;
                gvEQReturns.Visible = true;
                imgEQReturns.Visible = true;
                if (Cache["gvEQReturns" + userVo.UserId + userType] == null)
                {
                    Cache.Insert("gvEQReturns" + userVo.UserId + userType, dtEQReturns);
                }
                else
                {
                    Cache.Remove("gvEQReturns" + userVo.UserId + userType);
                    Cache.Insert("gvEQReturns" + userVo.UserId + userType, dtEQReturns);
                }
            }
        }
        private void SetParameters()
        {
            if (userType == "advisor")
            {
                hdnadviserId.Value = advisorVo.advisorId.ToString();
                hdnAll.Value = "0";
                hdnbranchId.Value = "0";
                hdnrmId.Value = "0";

            }
            else if (userType == "rm")
            {
                hdnrmId.Value = rmVo.RMId.ToString();
                hdnAll.Value = "0";

            }
            else if (userType == "bm")
            {
                hdnbranchHeadId.Value = bmID.ToString();
                hdnAll.Value = "0";
                hdnrmId.Value = "0";
            }
            if (hdnbranchHeadId.Value == "")
                hdnbranchHeadId.Value = "0";

            if (hdnbranchId.Value == "")
                hdnbranchId.Value = "0";

            if (hdnadviserId.Value == "")
                hdnadviserId.Value = "0";

            if (hdnrmId.Value == "")
                hdnrmId.Value = "0";
        }
        protected void gvEQReturns_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dtMfReturns = new DataTable();
            dtMfReturns = (DataTable)Cache["gvEQReturns" + userVo.UserId + userType];
            gvEQReturns.DataSource = dtMfReturns;
            gvEQReturns.Visible = true;
        }

        protected void imgEQReturns_Click(object sender, ImageClickEventArgs e)
        {
            gvEQReturns.ExportSettings.OpenInNewWindow = true;
            gvEQReturns.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvEQReturns.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvEQReturns.MasterTableView.ExportToExcel();
        }
    }
}