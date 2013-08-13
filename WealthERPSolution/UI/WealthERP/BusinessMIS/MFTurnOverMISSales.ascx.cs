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
using BoCommon;
using System.Configuration;
using VoUser;
using BoAdvisorProfiling;
using VOAssociates;
using Telerik.Web.UI;


namespace WealthERP.BusinessMIS
{
    public partial class MFTurnOverMISSales : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        RMVo rmVo = new RMVo();
        AssociatesVO associatesVo = new AssociatesVO();
        UserVo userVo = new UserVo();
        DateTime dtTo = new DateTime();
        DateBo dtBo = new DateBo();
        DateTime dtFrom = new DateTime();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        string Agentcode;
        string path = string.Empty;
        int advisorId = 0;
        String userType;
        int rmId = 0;
        int bmID = 0;
        int all = 0;
        int AgentId = 0;
        int branchId = 0;
        int branchHeadId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session["userVo"];
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            associatesVo = (AssociatesVO)Session["associatesVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
                userType = "associates";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();
            associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
            {
                Agentcode = string.Empty;
            }
            else

                Agentcode = associateuserheirarchyVo.AgentCode;

            advisorId = advisorVo.advisorId;
            int RMId = rmVo.RMId;

            rmId = rmVo.RMId;
            bmID = rmVo.RMId;
            if (!IsPostBack)
            {
                pnlProduct.Visible = false;
                rbtnPickDate.Checked = true;
                rbtnPickPeriod.Checked = false;
                divDateRange.Visible = true;
                divDatePeriod.Visible = false;
                txtFromDate.SelectedDate = DateTime.Now;
                txtToDate.SelectedDate = DateTime.Now;
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

        protected void lnkBtnSubBrokerCustomer_Click(object sender, EventArgs e)
        {

        }

        protected void lnkBtnOrganization_Click(object sender, EventArgs e)
        {

        }

        protected void lnkBtnProduct_Click(object sender, EventArgs e)
        {
            SetParameters();
            BindProductGrid();
        }

        private void BindProductGrid()
        {
            int SchemeCode = 0;
            int SchemeCodeOld = 0;
            DataSet dsGetProductDetailFromMFOrder = new DataSet();
            dsGetProductDetailFromMFOrder = adviserMFMIS.GetProductDetailFromMFOrder(Agentcode, userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), int.Parse(hdnAgentId.Value));

            DataTable dtGetProductDetailFromMFOrder = new DataTable();
            dtGetProductDetailFromMFOrder.Columns.Add("AMC");
            dtGetProductDetailFromMFOrder.Columns.Add("SchemeCode");
            dtGetProductDetailFromMFOrder.Columns.Add("Scheme");
            dtGetProductDetailFromMFOrder.Columns.Add("Category");
            dtGetProductDetailFromMFOrder.Columns.Add("SubCategory");
            dtGetProductDetailFromMFOrder.Columns.Add("BUYCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("BUYAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SELCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SELAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SIPCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SIPAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("STBCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("STBAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SWBCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SWBAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SWPCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SWPAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("ABYCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("ABYAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("GrossRedemption", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("GrossInvestment", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("Net", typeof(double));

            //--------------------Default Value ------------------

            #region Data Table Default value

            dtGetProductDetailFromMFOrder.Columns["GrossRedemption"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["GrossInvestment"].DefaultValue = 0;

            dtGetProductDetailFromMFOrder.Columns["Net"].DefaultValue = 0;

            dtGetProductDetailFromMFOrder.Columns["BUYCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["BUYAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SELCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SELAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SWPCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SWPAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["ABYCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["ABYAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SIPCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SIPAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["STBCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["STBAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SWBCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SWBAmount"].DefaultValue = 0;

            #endregion



            DataRow drGetProductDetailFromMFOrder;
            DataRow[] drTransactionSchemeWise;
            if (dsGetProductDetailFromMFOrder.Tables[0] != null)
            {
                DataTable dtGetSchemeTransaction = dsGetProductDetailFromMFOrder.Tables[0];

                foreach (DataRow drAMCTransaction in dtGetSchemeTransaction.Rows)
                {

                    Int32.TryParse(drAMCTransaction["PASP_SchemePlanCode"].ToString(), out SchemeCode);

                    if (SchemeCode != SchemeCodeOld)
                    { //go for another row to find new customer
                        SchemeCodeOld = SchemeCode;
                        drGetProductDetailFromMFOrder = dtGetProductDetailFromMFOrder.NewRow();
                        if (SchemeCode != 0)
                        { // add row in manual datatable within this brace end
                            drTransactionSchemeWise = dtGetSchemeTransaction.Select("PASP_SchemePlanCode=" + SchemeCode.ToString());
                            drGetProductDetailFromMFOrder["AMC"] = drAMCTransaction["PA_AMCName"].ToString();
                            drGetProductDetailFromMFOrder["SchemeCode"] = drAMCTransaction["PASP_SchemePlanCode"].ToString();
                            drGetProductDetailFromMFOrder["Scheme"] = drAMCTransaction["PASP_SchemePlanName"].ToString();

                            drGetProductDetailFromMFOrder["Category"] = drAMCTransaction["PAIC_AssetInstrumentCategoryName"].ToString();
                            drGetProductDetailFromMFOrder["SubCategory"] = drAMCTransaction["PAISC_AssetInstrumentSubCategoryName"].ToString();
                            if (drTransactionSchemeWise.Count() > 0)
                            {
                                foreach (DataRow dr in drTransactionSchemeWise)
                                {

                                    string transactiontype = dr["WMTT_TransactionClassificationCode"].ToString();
                                    switch (transactiontype)
                                    {
                                        case "BUY":
                                            {
                                                drGetProductDetailFromMFOrder["BUYCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["BUYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetProductDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["BUYAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SEL":
                                            {
                                                drGetProductDetailFromMFOrder["SELCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["SELAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetProductDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["SELAmount"].ToString());
                                                }
                                                break;
                                            }
                                     
                                        case "SIP":
                                            {
                                                drGetProductDetailFromMFOrder["SIPCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["SIPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetProductDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["SIPAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "STB":
                                            {
                                                drGetProductDetailFromMFOrder["STBCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["STBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetProductDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["STBAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SWB":
                                            {
                                                drGetProductDetailFromMFOrder["SWBCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["SWBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetProductDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["SWBAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SWP":
                                            {
                                                drGetProductDetailFromMFOrder["SWPCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["SWPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetProductDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["SWPAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "ABY":
                                            {
                                                drGetProductDetailFromMFOrder["ABYCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["ABYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetProductDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["ABYAmount"].ToString());
                                                }

                                                break;
                                            }
                                    }

                                }
                            }

                            drGetProductDetailFromMFOrder["Net"] = double.Parse(drGetProductDetailFromMFOrder["GrossInvestment"].ToString()) - double.Parse(drGetProductDetailFromMFOrder["GrossRedemption"].ToString());


                            dtGetProductDetailFromMFOrder.Rows.Add(drGetProductDetailFromMFOrder);
                        }//*

                    }//**

                }//***
                gvProduct.DataSource = dtGetProductDetailFromMFOrder;
                gvProduct.DataBind();
                pnlProduct.Visible = true;
                //btnProduct.Visible = true;
                gvProduct.Visible = true;
                pnlProduct.Visible = true;
                this.gvProduct.GroupingSettings.RetainGroupFootersVisibility = true;
                if (Cache["gvProduct" + userVo.UserId] == null)
                {
                    Cache.Insert("gvProduct" + userVo.UserId, dtGetProductDetailFromMFOrder);
                }
                else
                {
                    Cache.Remove("gvProduct" + userVo.UserId);
                    Cache.Insert("gvProduct" + userVo.UserId, dtGetProductDetailFromMFOrder);
                }

            }
        }
        private void SetParameters()
        {
            if (userType == "advisor")
            {
                hdnadviserId.Value = advisorVo.advisorId.ToString();
                hdnAll.Value = "0";
                hdnAgentId.Value = "0";
                hdnbranchId.Value = "0";
                hdnrmId.Value = "0";
            }
            else if (userType == "rm")
            {
                hdnrmId.Value = rmVo.RMId.ToString();
                hdnAgentId.Value = "0";
                hdnAll.Value = "0";

            }
            else if (userType == "bm")
            {
                hdnbranchHeadId.Value = bmID.ToString();
                hdnAll.Value = "0";
                hdnrmId.Value = "0";
                hdnAgentId.Value = "0";
            }
            else if (userType == "associates")
            {
                hdnAgentId.Value = associatesVo.AAC_AdviserAgentId.ToString();
                hdnadviserId.Value = "0";
                hdnbranchHeadId.Value = "0";
                hdnbranchId.Value = "0";
                hdnrmId.Value = "0";
                hdnAll.Value = "0";

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
        protected void btnProduct_Click(object sender, ImageClickEventArgs e)
        {
            gvProduct.ExportSettings.OpenInNewWindow = true;
            gvProduct.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvProduct.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvProduct.MasterTableView.ExportToExcel();
        }

        protected void gvProduct_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetProductDetailFromMFOrder = new DataTable();
            dtGetProductDetailFromMFOrder = (DataTable)Cache["gvProduct" + userVo.UserId.ToString()];
            gvProduct.DataSource = dtGetProductDetailFromMFOrder;
            gvProduct.Visible = true;
        }

    }
}