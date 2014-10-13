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
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
                userType = "associates";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();
            if (userType == "associates")
            {
                ddlFilter.Items[1].Enabled = false;
                ddlFilter.Items[3].Enabled = false;
            }
            associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops"
                || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
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
                pnlMember.Visible = false;
                pnlOrganization.Visible = false;
                rbtnPickDate.Checked = true;
                rbtnPickPeriod.Checked = false;
                divDateRange.Visible = true;
                divDatePeriod.Visible = false;
                trPnlOrganization.Visible = false;
                trMember.Visible = false;
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
            if (ddlFilter.SelectedValue == "S")
            {
                lblErrorFilter.Visible = true;
            }
            else
            {
                lblErrorFilter.Visible = false;
                SetParameters();
                BindMember();
                lblMFMISType.Text = "CUSTOMER/FOLIO";
                trPnlProduct.Visible = false;
                trPnlOrganization.Visible = false;
                trMember.Visible = true;
            }
        }

        private void BindMember()
        {
            int customerId = 0;
            int customerIdOld = 0;
            DataSet dsGetMemberDetailFromMFOrder = new DataSet();
            dsGetMemberDetailFromMFOrder = adviserMFMIS.GetMemberDetailFromMFOrder(Agentcode, userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), int.Parse(hdnAgentId.Value), int.Parse(ddlFilter.SelectedValue));
            DataTable dtGetMemberDetailFromMFOrder = new DataTable();
            dtGetMemberDetailFromMFOrder.Columns.Add("customerId");
            dtGetMemberDetailFromMFOrder.Columns.Add("OrderNo");
            dtGetMemberDetailFromMFOrder.Columns.Add("CustomerName");
            dtGetMemberDetailFromMFOrder.Columns.Add("SubBrokerCode");
            dtGetMemberDetailFromMFOrder.Columns.Add("SubBrokerName");
            dtGetMemberDetailFromMFOrder.Columns.Add("ChannelName");
            dtGetMemberDetailFromMFOrder.Columns.Add("Titles");
            dtGetMemberDetailFromMFOrder.Columns.Add("ZonalManagerName");
            dtGetMemberDetailFromMFOrder.Columns.Add("AreaManager");
            dtGetMemberDetailFromMFOrder.Columns.Add("ClusterManager");
            dtGetMemberDetailFromMFOrder.Columns.Add("ChannelMgr");
            dtGetMemberDetailFromMFOrder.Columns.Add("DeputyHead");
            dtGetMemberDetailFromMFOrder.Columns.Add("Folio");
            dtGetMemberDetailFromMFOrder.Columns.Add("IsOnline");
            dtGetMemberDetailFromMFOrder.Columns.Add("BUYCount", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("BUYAmount", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("SELCount", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("SELAmount", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("SIPCount", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("SIPAmount", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("STBCount", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("STBAmount", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("SWBCount", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("SWBAmount", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("SWPCount", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("SWPAmount", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("ABYCount", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("ABYAmount", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("GrossRedemption", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("GrossInvestment", typeof(double));
            dtGetMemberDetailFromMFOrder.Columns.Add("Net", typeof(double));

            //--------------------Default Value ------------------

            #region Data Table Default value

            dtGetMemberDetailFromMFOrder.Columns["GrossRedemption"].DefaultValue = 0;
            dtGetMemberDetailFromMFOrder.Columns["GrossInvestment"].DefaultValue = 0;

            dtGetMemberDetailFromMFOrder.Columns["Net"].DefaultValue = 0;

            dtGetMemberDetailFromMFOrder.Columns["BUYCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFOrder.Columns["BUYAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFOrder.Columns["SELCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFOrder.Columns["SELAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFOrder.Columns["SWPCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFOrder.Columns["SWPAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFOrder.Columns["ABYCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFOrder.Columns["ABYAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFOrder.Columns["SIPCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFOrder.Columns["SIPAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFOrder.Columns["STBCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFOrder.Columns["STBAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFOrder.Columns["SWBCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFOrder.Columns["SWBAmount"].DefaultValue = 0;

            #endregion



            DataRow drGetMemberDetailFromMFOrder;
            DataRow[] drOrderMemberWise;
            if (dsGetMemberDetailFromMFOrder.Tables[0] != null)
            {
                DataTable dtGetMemberOrderTransaction = dsGetMemberDetailFromMFOrder.Tables[0];

                foreach (DataRow drMemberOrderTransaction in dtGetMemberOrderTransaction.Rows)
                {

                    Int32.TryParse(drMemberOrderTransaction["C_CustomerId"].ToString(), out customerId);

                    if (customerId != customerIdOld)
                    { //go for another row to find new customer
                        customerIdOld = customerId;
                        drGetMemberDetailFromMFOrder = dtGetMemberDetailFromMFOrder.NewRow();
                        if (customerId != 0)
                        { // add row in manual datatable within this brace end
                            drOrderMemberWise = dtGetMemberOrderTransaction.Select("C_CustomerId=" + customerId.ToString());
                            drGetMemberDetailFromMFOrder["customerId"] = drMemberOrderTransaction["C_CustomerId"].ToString();
                            drGetMemberDetailFromMFOrder["OrderNo"] = drMemberOrderTransaction["CMFOD_OrderNumber"].ToString();
                            drGetMemberDetailFromMFOrder["CustomerName"] = drMemberOrderTransaction["CustomerName"].ToString();
                            drGetMemberDetailFromMFOrder["SubBrokerCode"] = drMemberOrderTransaction["SubBrokerCode"].ToString();
                            drGetMemberDetailFromMFOrder["SubBrokerName"] = drMemberOrderTransaction["AssociatesName"].ToString();
                            drGetMemberDetailFromMFOrder["ChannelName"] = drMemberOrderTransaction["ChannelName"].ToString();
                            drGetMemberDetailFromMFOrder["Titles"] = drMemberOrderTransaction["Titles"].ToString();
                            drGetMemberDetailFromMFOrder["ClusterManager"] = drMemberOrderTransaction["ClusterManager"].ToString();
                            drGetMemberDetailFromMFOrder["AreaManager"] = drMemberOrderTransaction["AreaManager"].ToString();
                            drGetMemberDetailFromMFOrder["ZonalManagerName"] = drMemberOrderTransaction["ZonalManagerName"].ToString();
                            drGetMemberDetailFromMFOrder["DeputyHead"] = drMemberOrderTransaction["DeputyHead"].ToString();
                            drGetMemberDetailFromMFOrder["IsOnline"] = drMemberOrderTransaction["IsOnline"].ToString();
                            drGetMemberDetailFromMFOrder["Folio"] = drMemberOrderTransaction["CMFA_FolioNum"].ToString();
                            if (drOrderMemberWise.Count() > 0)
                            {
                                foreach (DataRow dr in drOrderMemberWise)
                                {

                                    string transactiontype = dr["WMTT_TransactionClassificationCode"].ToString();
                                    switch (transactiontype)
                                    {
                                        case "BUY":
                                            {
                                                drGetMemberDetailFromMFOrder["BUYCount"] = dr["TrnsCount"].ToString();
                                                drGetMemberDetailFromMFOrder["BUYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetMemberDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetMemberDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetMemberDetailFromMFOrder["BUYAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SEL":
                                            {
                                                drGetMemberDetailFromMFOrder["SELCount"] = dr["TrnsCount"].ToString();
                                                drGetMemberDetailFromMFOrder["SELAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetMemberDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetMemberDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetMemberDetailFromMFOrder["SELAmount"].ToString());
                                                }
                                                break;
                                            }

                                        case "SIP":
                                            {
                                                drGetMemberDetailFromMFOrder["SIPCount"] = dr["TrnsCount"].ToString();
                                                drGetMemberDetailFromMFOrder["SIPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetMemberDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetMemberDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetMemberDetailFromMFOrder["SIPAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "STB":
                                            {
                                                drGetMemberDetailFromMFOrder["STBCount"] = dr["TrnsCount"].ToString();
                                                drGetMemberDetailFromMFOrder["STBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetMemberDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetMemberDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetMemberDetailFromMFOrder["STBAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SWB":
                                            {
                                                drGetMemberDetailFromMFOrder["SWBCount"] = dr["TrnsCount"].ToString();
                                                drGetMemberDetailFromMFOrder["SWBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetMemberDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetMemberDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetMemberDetailFromMFOrder["SWBAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SWP":
                                            {
                                                drGetMemberDetailFromMFOrder["SWPCount"] = dr["TrnsCount"].ToString();
                                                drGetMemberDetailFromMFOrder["SWPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetMemberDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetMemberDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetMemberDetailFromMFOrder["SWPAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "ABY":
                                            {
                                                drGetMemberDetailFromMFOrder["ABYCount"] = dr["TrnsCount"].ToString();
                                                drGetMemberDetailFromMFOrder["ABYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetMemberDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetMemberDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetMemberDetailFromMFOrder["ABYAmount"].ToString());
                                                }

                                                break;
                                            }
                                    }

                                }
                            }

                            drGetMemberDetailFromMFOrder["Net"] = double.Parse(drGetMemberDetailFromMFOrder["GrossInvestment"].ToString()) - double.Parse(drGetMemberDetailFromMFOrder["GrossRedemption"].ToString());


                            dtGetMemberDetailFromMFOrder.Rows.Add(drGetMemberDetailFromMFOrder);
                        }//*

                    }//**

                }//***
                gvMember.DataSource = dtGetMemberDetailFromMFOrder;
                gvMember.DataBind();
                pnlMember.Visible = true;
                gvMember.Visible = true;
                btnExpMember.Visible = true;
                btnExpOrganization.Visible = false;
                btnExpProduct.Visible = false;
                this.gvMember.GroupingSettings.RetainGroupFootersVisibility = true;
                if (Cache["gvMember" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("gvMember" + advisorVo.advisorId, dtGetMemberDetailFromMFOrder);
                }
                else
                {
                    Cache.Remove("gvMember" + advisorVo.advisorId);
                    Cache.Insert("gvMember" + advisorVo.advisorId, dtGetMemberDetailFromMFOrder);
                }
            }
        }

        protected void lnkBtnOrganization_Click(object sender, EventArgs e)
        {
            if (ddlFilter.SelectedValue == "S")
            {
                lblErrorFilter.Visible = true;
            }
            else
            {
                lblErrorFilter.Visible = false;
                SetParameters();
                BindOrganizationGrid();
                lblMFMISType.Text = "ORGANIZATION";
                trPnlProduct.Visible = false;
                trPnlOrganization.Visible = true;
                trMember.Visible = false;
            }
        }

        private void BindOrganizationGrid()
        {
            int customerId = 0;
            int customerIdOld = 0;
            DataSet dsGetOrganizationDetailFromMFOrder = new DataSet();
            dsGetOrganizationDetailFromMFOrder = adviserMFMIS.GetOrganizationDetailFromMFOrder(Agentcode, userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), int.Parse(hdnAgentId.Value), int.Parse(ddlFilter.SelectedValue));

            DataTable dtGetOrganizationDetailFromMFOrder = new DataTable();
            dtGetOrganizationDetailFromMFOrder.Columns.Add("customerId");
            dtGetOrganizationDetailFromMFOrder.Columns.Add("ZonalManagerName");
            dtGetOrganizationDetailFromMFOrder.Columns.Add("AreaManager");
            dtGetOrganizationDetailFromMFOrder.Columns.Add("ClusterManager");
            dtGetOrganizationDetailFromMFOrder.Columns.Add("ChannelMgr");
            dtGetOrganizationDetailFromMFOrder.Columns.Add("DeputyHead");
            dtGetOrganizationDetailFromMFOrder.Columns.Add("OrderNo");
            dtGetOrganizationDetailFromMFOrder.Columns.Add("CustomerName");
            dtGetOrganizationDetailFromMFOrder.Columns.Add("IsOnline");
            dtGetOrganizationDetailFromMFOrder.Columns.Add("BUYCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("BUYAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SELCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SELAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SIPCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SIPAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("STBCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("STBAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SWBCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SWBAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SWPCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SWPAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("ABYCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("ABYAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("GrossRedemption", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("GrossInvestment", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("Net", typeof(double));

            //--------------------Default Value ------------------

            #region Data Table Default value

            dtGetOrganizationDetailFromMFOrder.Columns["GrossRedemption"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["GrossInvestment"].DefaultValue = 0;

            dtGetOrganizationDetailFromMFOrder.Columns["Net"].DefaultValue = 0;

            dtGetOrganizationDetailFromMFOrder.Columns["BUYCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["BUYAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SELCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SELAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SWPCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SWPAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["ABYCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["ABYAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SIPCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SIPAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["STBCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["STBAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SWBCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SWBAmount"].DefaultValue = 0;

            #endregion



            DataRow drGetOrganizationDetailFromMFOrder;
            DataRow[] drOrderOrgWise;
            if (dsGetOrganizationDetailFromMFOrder.Tables[0] != null)
            {
                DataTable dtGetOrgOrderTransaction = dsGetOrganizationDetailFromMFOrder.Tables[0];

                foreach (DataRow drOrgOrderTransaction in dtGetOrgOrderTransaction.Rows)
                {

                    Int32.TryParse(drOrgOrderTransaction["C_CustomerId"].ToString(), out customerId);

                    if (customerId != customerIdOld)
                    { //go for another row to find new customer
                        customerIdOld = customerId;
                        drGetOrganizationDetailFromMFOrder = dtGetOrganizationDetailFromMFOrder.NewRow();
                        if (customerId != 0)
                        { // add row in manual datatable within this brace end
                            drOrderOrgWise = dtGetOrgOrderTransaction.Select("C_CustomerId=" + customerId.ToString());
                            drGetOrganizationDetailFromMFOrder["customerId"] = drOrgOrderTransaction["C_CustomerId"].ToString();
                            drGetOrganizationDetailFromMFOrder["ZonalManagerName"] = drOrgOrderTransaction["ZonalManagerName"].ToString();
                            drGetOrganizationDetailFromMFOrder["AreaManager"] = drOrgOrderTransaction["AreaManager"].ToString();
                            drGetOrganizationDetailFromMFOrder["ClusterManager"] = drOrgOrderTransaction["ClusterManager"].ToString();
                            drGetOrganizationDetailFromMFOrder["DeputyHead"] = drOrgOrderTransaction["DeputyHead"].ToString();


                            drGetOrganizationDetailFromMFOrder["ChannelMgr"] = drOrgOrderTransaction["ChannelMgr"].ToString();
                            drGetOrganizationDetailFromMFOrder["OrderNo"] = drOrgOrderTransaction["CMFOD_OrderNumber"].ToString();
                            drGetOrganizationDetailFromMFOrder["CustomerName"] = drOrgOrderTransaction["CustomerName"].ToString();
                            drGetOrganizationDetailFromMFOrder["IsOnline"] = drOrgOrderTransaction["IsOnline"].ToString();
                            if (drOrderOrgWise.Count() > 0)
                            {
                                foreach (DataRow dr in drOrderOrgWise)
                                {

                                    string transactiontype = dr["WMTT_TransactionClassificationCode"].ToString();
                                    switch (transactiontype)
                                    {
                                        case "BUY":
                                            {
                                                drGetOrganizationDetailFromMFOrder["BUYCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["BUYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["BUYAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SEL":
                                            {
                                                drGetOrganizationDetailFromMFOrder["SELCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["SELAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["SELAmount"].ToString());
                                                }
                                                break;
                                            }

                                        case "SIP":
                                            {
                                                drGetOrganizationDetailFromMFOrder["SIPCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["SIPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["SIPAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "STB":
                                            {
                                                drGetOrganizationDetailFromMFOrder["STBCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["STBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["STBAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SWB":
                                            {
                                                drGetOrganizationDetailFromMFOrder["SWBCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["SWBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["SWBAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SWP":
                                            {
                                                drGetOrganizationDetailFromMFOrder["SWPCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["SWPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["SWPAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "ABY":
                                            {
                                                drGetOrganizationDetailFromMFOrder["ABYCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["ABYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["ABYAmount"].ToString());
                                                }

                                                break;
                                            }
                                    }

                                }
                            }

                            drGetOrganizationDetailFromMFOrder["Net"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossInvestment"].ToString()) - double.Parse(drGetOrganizationDetailFromMFOrder["GrossRedemption"].ToString());


                            dtGetOrganizationDetailFromMFOrder.Rows.Add(drGetOrganizationDetailFromMFOrder);
                        }//*

                    }//**

                }//***

                gvOrganization.DataSource = dtGetOrganizationDetailFromMFOrder;
                gvOrganization.DataBind();
                pnlProduct.Visible = true;
                btnExpOrganization.Visible = true;
                btnExpProduct.Visible = false;
                btnExpMember.Visible = false;
                gvOrganization.Visible = true;
                pnlOrganization.Visible = true;
                this.gvOrganization.GroupingSettings.RetainGroupFootersVisibility = true;
                if (Cache["gvOrganization" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("gvOrganization" + advisorVo.advisorId, dtGetOrganizationDetailFromMFOrder);
                }
                else
                {
                    Cache.Remove("gvOrganization" + advisorVo.advisorId);
                    Cache.Insert("gvOrganization" + advisorVo.advisorId, dtGetOrganizationDetailFromMFOrder);
                }

            }
        }

        protected void lnkBtnProduct_Click(object sender, EventArgs e)
        {
            if (ddlFilter.SelectedValue == "S")
            {
                lblErrorFilter.Visible = true;
            }
            else
            {
                lblErrorFilter.Visible = false;
                SetParameters();
                BindProductGrid();
                lblMFMISType.Text = "PRODUCT";
                trPnlProduct.Visible = true;
                trPnlOrganization.Visible = false;
                trMember.Visible = false;
            }

        }

        private void BindProductGrid()
        {
            int SchemeCode = 0;
            int SchemeCodeOld = 0;
            DataSet dsGetProductDetailFromMFOrder = new DataSet();
            dsGetProductDetailFromMFOrder = adviserMFMIS.GetProductDetailFromMFOrder(Agentcode, userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), int.Parse(hdnAgentId.Value),int.Parse(ddlFilter.SelectedValue));

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
                btnExpOrganization.Visible = false;
                btnExpProduct.Visible = true;
                btnExpMember.Visible = false;
                gvProduct.Visible = true;
                pnlProduct.Visible = true;
                this.gvProduct.GroupingSettings.RetainGroupFootersVisibility = true;
                //if (Cache["gvProduct" + userVo.UserId] == null)
                //{
                //    Cache.Insert("gvProduct" + userVo.UserId, dtGetProductDetailFromMFOrder);
                //}
                //else
                //{
                //    Cache.Remove("gvProduct" + userVo.UserId);
                //    Cache.Insert("gvProduct" + userVo.UserId, dtGetProductDetailFromMFOrder);
                //}
                if (Cache["gvProduct" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("gvProduct" + advisorVo.advisorId, dtGetProductDetailFromMFOrder);
                }
                else
                {
                    Cache.Remove("gvProduct" + advisorVo.advisorId);
                    Cache.Insert("gvProduct" + advisorVo.advisorId, dtGetProductDetailFromMFOrder);
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
                hdnadviserId.Value = advisorVo.advisorId.ToString();
                hdnbranchHeadId.Value = bmID.ToString();
                hdnAll.Value = "0";
                hdnrmId.Value = "0";
                hdnAgentId.Value = "0";
            }
            else if (userType == "associates")
            {
                hdnAgentId.Value = associatesVo.AAC_AdviserAgentId.ToString();
                Agentcode = associateuserheirarchyVo.AgentCode;
                hdnadviserId.Value = advisorVo.advisorId.ToString();
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
        protected void btnExpProduct_Click(object sender, ImageClickEventArgs e)
        {
            gvProduct.ExportSettings.OpenInNewWindow = true;
            gvProduct.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvProduct.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvProduct.MasterTableView.ExportToExcel();
        }
        protected void btnExpOrganization_Click(object sender, ImageClickEventArgs e)
        {
            gvOrganization.ExportSettings.OpenInNewWindow = true;
            gvOrganization.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvOrganization.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvOrganization.MasterTableView.ExportToExcel();
        }
        protected void btnExpMember_Click(object sender, ImageClickEventArgs e)
        {
            gvMember.ExportSettings.OpenInNewWindow = true;
            gvMember.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvMember.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvMember.MasterTableView.ExportToExcel();
        }

        protected void gvProduct_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetProductDetailFromMFOrder = new DataTable();
            dtGetProductDetailFromMFOrder = (DataTable)Cache["gvProduct" + advisorVo.advisorId];
            gvProduct.Visible = true;
            this.gvProduct.DataSource = dtGetProductDetailFromMFOrder;
        }
        protected void gvOrganization_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetOrganizationDetailFromMFOrder = new DataTable();
            dtGetOrganizationDetailFromMFOrder = (DataTable)Cache["gvOrganization" + advisorVo.advisorId];
            gvOrganization.Visible = true;
            this.gvOrganization.DataSource = dtGetOrganizationDetailFromMFOrder;
        }
        protected void gvMember_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            DataTable dtGetMemberDetailFromMFOrder = new DataTable();
            dtGetMemberDetailFromMFOrder = (DataTable)Cache["gvMember" + advisorVo.advisorId];
            gvMember.Visible = true;
            this.gvMember.DataSource = dtGetMemberDetailFromMFOrder;

        }
    }
}