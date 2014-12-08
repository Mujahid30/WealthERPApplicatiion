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
using BOAssociates;

namespace WealthERP.BusinessMIS
{
    public partial class MFTransactionTurnOverMIS : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        RMVo rmVo = new RMVo();
        AssociatesVO associatesVo = new AssociatesVO();
        AssociatesBo associatesBo = new AssociatesBo();
        AdvisorMISBo adviserMISBo = new AdvisorMISBo();
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

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session["userVo"];
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
                Agentcode = associateuserheirarchyVo.AgentCode;
                //trSelectType.Visible = false;
            }
            advisorId = advisorVo.advisorId;
            int RMId = rmVo.RMId;

            rmId = rmVo.RMId;
            bmID = rmVo.RMId;
            if (!IsPostBack)
            {
                rbtnPickDate.Checked = true;
                rbtnPickPeriod.Checked = false;
                divDateRange.Visible = true;
                divDatePeriod.Visible = false;
                txtFromDate.SelectedDate = DateTime.Now;
                txtToDate.SelectedDate = DateTime.Now;
                trPnlProduct.Visible = false;
                trPnlOrganization.Visible = false;
                trMember.Visible = false;
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
        protected void imgProduct_Click(object sender, ImageClickEventArgs e)
        {
            gvProduct.ExportSettings.OpenInNewWindow = true;
            gvProduct.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvProduct.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvProduct.MasterTableView.ExportToExcel();
        }
        protected void imgOrganization_Click(object sender, ImageClickEventArgs e)
        {
            gvOrganization.ExportSettings.OpenInNewWindow = true;
            gvOrganization.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvOrganization.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvOrganization.MasterTableView.ExportToExcel();
        }
        protected void imgMember_Click(object sender, ImageClickEventArgs e)
        {
            gvMember.ExportSettings.OpenInNewWindow = true;
            gvMember.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvMember.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvMember.MasterTableView.ExportToExcel();
        }

        private void BindProductGrid()
        {
            int SchemeCode = 0;
            int SchemeCodeOld = 0;
            DataSet dsGetProductDetailFromMFOrder = new DataSet();
            dsGetProductDetailFromMFOrder = associatesBo.GetProductDetailsFromMFTransaction(hdnsubBrokerCode.Value, userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), int.Parse(hdnAll.Value), int.Parse(ddlFilter.SelectedValue));

            DataTable dtGetProductDetailFromMFOrder = new DataTable();
            dtGetProductDetailFromMFOrder.Columns.Add("AMC");
            dtGetProductDetailFromMFOrder.Columns.Add("SchemeCode");
            dtGetProductDetailFromMFOrder.Columns.Add("Scheme");
            dtGetProductDetailFromMFOrder.Columns.Add("Category");
            dtGetProductDetailFromMFOrder.Columns.Add("SubCategory");
            dtGetProductDetailFromMFOrder.Columns.Add("IsOnline");
            dtGetProductDetailFromMFOrder.Columns.Add("BUYCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("BUYAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SELCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SELAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("DVRCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("DVRAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("DVPCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("DVPAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SIPCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SIPAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("BCICount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("BCIAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("BCOCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("BCOAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("STBCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("STBAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("STSCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("STSAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SWBCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SWBAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SWPCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SWPAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SWSCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SWSAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("PRJCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("PRJAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("ABYCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("ABYAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("BIRCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("BIRAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("BNSCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("BNSAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("CNICount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("CNIAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("CNOCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("CNOAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("DSICount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("DSIAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("DSOCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("DSOAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("HLDCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("HLDAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("NFOCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("NFOAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("RRJCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("RRJAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SRJCount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("SRJAmount", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("GrossRedemption", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("GrossInvestment", typeof(double));
            dtGetProductDetailFromMFOrder.Columns.Add("Net", typeof(double));

            //--------------------Default Value ------------------

            #region Data Table Default value

            dtGetProductDetailFromMFOrder.Columns["GrossRedemption"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["GrossInvestment"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["Net"].DefaultValue = 0;

            dtGetProductDetailFromMFOrder.Columns["ABYCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["ABYAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["BIRCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["BIRAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["BNSCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["BNSAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["CNICount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["CNIAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["CNOCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["CNOAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["DSICount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["DSIAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["DSOCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["DSOAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["HLDCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["HLDAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["NFOCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["NFOAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["RRJCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["RRJAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SRJCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SRJAmount"].DefaultValue = 0;

            dtGetProductDetailFromMFOrder.Columns["DVRCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["DVRAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["DVPCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["DVPAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SIPCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SIPAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["BCICount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["BCIAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["BCOCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["BCOAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["STBCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["STBAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["STSCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["STSAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SWBCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SWBAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SWPCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SWPAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SWSCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["SWSAmount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["PRJCount"].DefaultValue = 0;
            dtGetProductDetailFromMFOrder.Columns["PRJAmount"].DefaultValue = 0;

            #endregion



            DataRow drGetProductDetailFromMFOrder;
            DataRow[] drTransactionSchemeWise;
            if (dsGetProductDetailFromMFOrder.Tables[0] != null)
            {
                DataTable dtGetProductTransaction = dsGetProductDetailFromMFOrder.Tables[0];

                foreach (DataRow drAMCTransaction in dtGetProductTransaction.Rows)
                {

                    Int32.TryParse(drAMCTransaction["PASP_SchemePlanCode"].ToString(), out SchemeCode);

                    if (SchemeCode != SchemeCodeOld)
                    { //go for another row to find new customer
                        SchemeCodeOld = SchemeCode;
                        drGetProductDetailFromMFOrder = dtGetProductDetailFromMFOrder.NewRow();
                        if (SchemeCode != 0)
                        { // add row in manual datatable within this brace end
                            drTransactionSchemeWise = dtGetProductTransaction.Select("PASP_SchemePlanCode=" + SchemeCode.ToString());
                            drGetProductDetailFromMFOrder["AMC"] = drAMCTransaction["PA_AMCName"].ToString();
                            drGetProductDetailFromMFOrder["SchemeCode"] = drAMCTransaction["PASP_SchemePlanCode"].ToString();
                            drGetProductDetailFromMFOrder["Scheme"] = drAMCTransaction["PASP_SchemePlanName"].ToString();

                            drGetProductDetailFromMFOrder["Category"] = drAMCTransaction["PAIC_AssetInstrumentCategoryName"].ToString();
                            drGetProductDetailFromMFOrder["SubCategory"] = drAMCTransaction["PAISC_AssetInstrumentSubCategoryName"].ToString();
                            drGetProductDetailFromMFOrder["IsOnline"] = drAMCTransaction["IsOnline"].ToString();
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
                                        case "DVR":
                                            {
                                                drGetProductDetailFromMFOrder["DVRCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["DVRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetProductDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["DVRAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "DVP":
                                            {
                                                drGetProductDetailFromMFOrder["DVPCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["DVPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
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
                                        case "BCI":
                                            {
                                                drGetProductDetailFromMFOrder["BCICount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["BCIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetProductDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["BCIAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "BCO":
                                            {
                                                drGetProductDetailFromMFOrder["BCOCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["BCOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetProductDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["BCOAmount"].ToString());
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
                                        case "STS":
                                            {
                                                drGetProductDetailFromMFOrder["STSCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["STSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetProductDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["STSAmount"].ToString());
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
                                        case "SWS":
                                            {
                                                drGetProductDetailFromMFOrder["SWSCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["SWSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetProductDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["SWSAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "PRJ":
                                            {
                                                drGetProductDetailFromMFOrder["PRJCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["PRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        #region newly added
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
                                        case "BIR":
                                            {
                                                drGetProductDetailFromMFOrder["BIRCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["BIRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetProductDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["BIRAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "BNS":
                                            {
                                                drGetProductDetailFromMFOrder["BNSCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["BNSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetProductDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["BNSAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "CNI":
                                            {
                                                drGetProductDetailFromMFOrder["CNICount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["CNIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetProductDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["CNIAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "CNO":
                                            {
                                                drGetProductDetailFromMFOrder["CNOCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["CNOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetProductDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["CNOAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "DSI":
                                            {
                                                drGetProductDetailFromMFOrder["DSICount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["DSIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetProductDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["DSIAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "DSO":
                                            {
                                                drGetProductDetailFromMFOrder["DSOCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["DSOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetProductDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["DSOAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "HLD":
                                            {
                                                drGetProductDetailFromMFOrder["HLDCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["HLDAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetProductDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["HLDAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "NFO":
                                            {
                                                drGetProductDetailFromMFOrder["NFOCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["NFOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetProductDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["NFOAmount"].ToString());
                                                }

                                                break;
                                            }


                                        case "RRJ":
                                            {
                                                drGetProductDetailFromMFOrder["RRJCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["RRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetProductDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["RRJAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "SRJ":
                                            {
                                                drGetProductDetailFromMFOrder["SRJCount"] = dr["TrnsCount"].ToString();
                                                drGetProductDetailFromMFOrder["SRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetProductDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetProductDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetProductDetailFromMFOrder["SRJAmount"].ToString());
                                                }

                                                break;
                                            }
                                        #endregion
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
                gvProduct.Visible = true;
                pnlProduct.Visible = true;
                imgProduct.Visible = true;
                imgOrganization.Visible = false;
                imgMember.Visible = false;
                this.gvProduct.GroupingSettings.RetainGroupFootersVisibility = true;
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
                //if (ddlSelectType.SelectedIndex != 0)
                //{
                //    hdnsubBrokerCode.Value = ddlSelectType.SelectedItem.Text;
                //    hdnAll.Value = "0";
                //}
                //else
                //{
                //    hdnsubBrokerCode.Value = " ";
                //    hdnAll.Value = "1";
                //}
                hdnAll.Value = "1";
                hdnsubBrokerCode.Value = null;
                hdnbranchId.Value = "0";
                hdnrmId.Value = "0";
            }
            else if (userType == "rm")
            {
                hdnrmId.Value = rmVo.RMId.ToString();
                //if (ddlSelectType.SelectedIndex != 0)
                //{
                //    hdnsubBrokerCode.Value = ddlSelectType.SelectedItem.Text;
                //    hdnAll.Value = "0";
                //}
                //else
                //{
                //    hdnsubBrokerCode.Value = "0";
                //    hdnAll.Value = "1";
                //}
                hdnAll.Value = "1";
                hdnsubBrokerCode.Value = null;
                hdnAgentId.Value = "0";
            }
            else if (userType == "bm")
            {
                hdnbranchHeadId.Value = bmID.ToString();
                //if (ddlSelectType.SelectedIndex != 0)
                //{
                //    hdnsubBrokerCode.Value = ddlSelectType.SelectedItem.Text;
                //    hdnAll.Value = "0";
                //}
                //else
                //{
                //    hdnsubBrokerCode.Value = "0";
                //    hdnAll.Value = "1";
                //}
                hdnAll.Value = "1";
                hdnsubBrokerCode.Value = null;
                hdnrmId.Value = "0";
                hdnAgentId.Value = "0";
            }
            else if (userType == "associates")
            {
                hdnAgentId.Value = associatesVo.AAC_AdviserAgentId.ToString();
                hdnsubBrokerCode.Value = associateuserheirarchyVo.AgentCode;
                hdnadviserId.Value = advisorVo.advisorId.ToString();
                hdnAll.Value = "0";
                hdnbranchHeadId.Value = "0";
                hdnbranchId.Value = "0";
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

        //protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlUserType.SelectedIndex != 0)
        //    {
        //        if (ddlUserType.SelectedValue == "BM")
        //        {
        //            BindBranchSubBrokerCode();
        //        }
        //        else if (ddlUserType.SelectedValue == "RM")
        //        {
        //            BindSalesSubBrokerCode();
        //        }
        //        else if (ddlUserType.SelectedValue == "Associates")
        //        {
        //            BindAssociatesSubBrokerCode();
        //        }
        //    }
        //}

        //private void BindAssociatesSubBrokerCode()
        //{
        //    DataSet ds;
        //    DataTable dt;
        //    dt = associatesBo.GetAssociatesSubBrokerCodeList(advisorVo.advisorId);
        //    if (dt.Rows.Count > 0)
        //    {
        //        ddlSelectType.DataSource = dt;
        //        ddlSelectType.DataValueField = dt.Columns["AAC_AdviserAgentId"].ToString();
        //        ddlSelectType.DataTextField = dt.Columns["AAC_AgentCode"].ToString();
        //        ddlSelectType.DataBind();
        //    }
        //    ddlSelectType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
        //}

        //private void BindSalesSubBrokerCode()
        //{
        //    DataSet ds;
        //    DataTable dt;
        //    dt = associatesBo.GetSalesSubBrokerCodeList(advisorVo.advisorId);
        //    if (dt.Rows.Count > 0)
        //    {
        //        ddlSelectType.DataSource = dt;
        //        ddlSelectType.DataValueField = dt.Columns["AAC_AdviserAgentId"].ToString();
        //        ddlSelectType.DataTextField = dt.Columns["AAC_AgentCode"].ToString();
        //        ddlSelectType.DataBind();
        //    }
        //    ddlSelectType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
        //}

        //private void BindBranchSubBrokerCode()
        //{
        //    DataSet ds;
        //    DataTable dt;
        //    dt = associatesBo.GetBranchSubBrokerCodeList(advisorVo.advisorId);
        //    if (dt.Rows.Count > 0)
        //    {
        //        ddlSelectType.DataSource = dt;
        //        ddlSelectType.DataValueField = dt.Columns["AAC_AdviserAgentId"].ToString();
        //        ddlSelectType.DataTextField = dt.Columns["AAC_AgentCode"].ToString();
        //        ddlSelectType.DataBind();
        //    }
        //    ddlSelectType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
        //}
        protected void gvProduct_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetProductDetailFromMFOrder = new DataTable();
            dtGetProductDetailFromMFOrder = (DataTable)Cache["gvProduct" + advisorVo.advisorId];
            gvProduct.Visible = true;
            this.gvProduct.DataSource = dtGetProductDetailFromMFOrder;
        }
        private void BindOrganizationGrid()
        {
            int customerId = 0;
            int customerIdOld = 0;
            DataSet dsGetOrganizationDetailFromMFOrder = new DataSet();
            dsGetOrganizationDetailFromMFOrder = associatesBo.GetOrganizationDetailFromTransaction(hdnsubBrokerCode.Value, userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), int.Parse(hdnAll.Value));

            DataTable dtGetOrganizationDetailFromMFOrder = new DataTable();
            dtGetOrganizationDetailFromMFOrder.Columns.Add("customerId");
            dtGetOrganizationDetailFromMFOrder.Columns.Add("ZonalManagerName");
            dtGetOrganizationDetailFromMFOrder.Columns.Add("AreaManager");
            dtGetOrganizationDetailFromMFOrder.Columns.Add("CircleManager");
            dtGetOrganizationDetailFromMFOrder.Columns.Add("ChannelMgr");
            dtGetOrganizationDetailFromMFOrder.Columns.Add("CustomerName");
            dtGetOrganizationDetailFromMFOrder.Columns.Add("BUYCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("BUYAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SELCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SELAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("DVRCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("DVRAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("DVPCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("DVPAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SIPCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SIPAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("BCICount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("BCIAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("BCOCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("BCOAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("STBCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("STBAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("STSCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("STSAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SWBCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SWBAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SWPCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SWPAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SWSCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SWSAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("PRJCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("PRJAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("ABYCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("ABYAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("BIRCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("BIRAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("BNSCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("BNSAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("CNICount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("CNIAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("CNOCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("CNOAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("DSICount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("DSIAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("DSOCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("DSOAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("HLDCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("HLDAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("NFOCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("NFOAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("RRJCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("RRJAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SRJCount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("SRJAmount", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("GrossRedemption", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("GrossInvestment", typeof(double));
            dtGetOrganizationDetailFromMFOrder.Columns.Add("Net", typeof(double));

            //--------------------Default Value ------------------

            #region Data Table Default value

            dtGetOrganizationDetailFromMFOrder.Columns["GrossRedemption"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["GrossInvestment"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["Net"].DefaultValue = 0;

            dtGetOrganizationDetailFromMFOrder.Columns["ABYCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["ABYAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["BIRCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["BIRAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["BNSCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["BNSAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["CNICount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["CNIAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["CNOCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["CNOAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["DSICount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["DSIAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["DSOCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["DSOAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["HLDCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["HLDAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["NFOCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["NFOAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["RRJCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["RRJAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SRJCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SRJAmount"].DefaultValue = 0;

            dtGetOrganizationDetailFromMFOrder.Columns["DVRCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["DVRAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["DVPCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["DVPAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SIPCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SIPAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["BCICount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["BCIAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["BCOCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["BCOAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["STBCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["STBAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["STSCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["STSAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SWBCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SWBAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SWPCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SWPAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SWSCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["SWSAmount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["PRJCount"].DefaultValue = 0;
            dtGetOrganizationDetailFromMFOrder.Columns["PRJAmount"].DefaultValue = 0;

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
                            //drOrderOrgWise = dtGetOrgOrderTransaction.Select("AgenId=" + agentId.ToString());
                            //drGetOrganizationDetailFromMFOrder["AgenId"] = drOrgOrderTransaction["AgenId"].ToString();
                            drGetOrganizationDetailFromMFOrder["ZonalManagerName"] = drOrgOrderTransaction["ZonalManagerName"].ToString();
                            drGetOrganizationDetailFromMFOrder["AreaManager"] = drOrgOrderTransaction["AreaManager"].ToString();
                            drGetOrganizationDetailFromMFOrder["CircleManager"] = drOrgOrderTransaction["CircleManager"].ToString();

                            drGetOrganizationDetailFromMFOrder["ChannelMgr"] = drOrgOrderTransaction["ChannelMgr"].ToString();
                            drGetOrganizationDetailFromMFOrder["CustomerName"] = drOrgOrderTransaction["CustomerName"].ToString();
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
                                        case "DVR":
                                            {
                                                drGetOrganizationDetailFromMFOrder["DVRCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["DVRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["DVRAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "DVP":
                                            {
                                                drGetOrganizationDetailFromMFOrder["DVPCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["DVPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
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
                                        case "BCI":
                                            {
                                                drGetOrganizationDetailFromMFOrder["BCICount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["BCIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["BCIAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "BCO":
                                            {
                                                drGetOrganizationDetailFromMFOrder["BCOCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["BCOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["BCOAmount"].ToString());
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
                                        case "STS":
                                            {
                                                drGetOrganizationDetailFromMFOrder["STSCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["STSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["STSAmount"].ToString());
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
                                        case "SWS":
                                            {
                                                drGetOrganizationDetailFromMFOrder["SWSCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["SWSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["SWSAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "PRJ":
                                            {
                                                drGetOrganizationDetailFromMFOrder["PRJCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["PRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        #region newly added
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
                                        case "BIR":
                                            {
                                                drGetOrganizationDetailFromMFOrder["BIRCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["BIRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["BIRAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "BNS":
                                            {
                                                drGetOrganizationDetailFromMFOrder["BNSCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["BNSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["BNSAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "CNI":
                                            {
                                                drGetOrganizationDetailFromMFOrder["CNICount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["CNIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["CNIAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "CNO":
                                            {
                                                drGetOrganizationDetailFromMFOrder["CNOCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["CNOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["CNOAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "DSI":
                                            {
                                                drGetOrganizationDetailFromMFOrder["DSICount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["DSIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["DSIAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "DSO":
                                            {
                                                drGetOrganizationDetailFromMFOrder["DSOCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["DSOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["DSOAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "HLD":
                                            {
                                                drGetOrganizationDetailFromMFOrder["HLDCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["HLDAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["HLDAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "NFO":
                                            {
                                                drGetOrganizationDetailFromMFOrder["NFOCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["NFOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["NFOAmount"].ToString());
                                                }

                                                break;
                                            }


                                        case "RRJ":
                                            {
                                                drGetOrganizationDetailFromMFOrder["RRJCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["RRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["RRJAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "SRJ":
                                            {
                                                drGetOrganizationDetailFromMFOrder["SRJCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromMFOrder["SRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromMFOrder["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromMFOrder["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromMFOrder["SRJAmount"].ToString());
                                                }

                                                break;
                                            }
                                        #endregion
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
                //btnExpOrganization.Visible = true;
                //btnExpProduct.Visible = false;
                //btnExpMember.Visible = false;
                gvOrganization.Visible = true;
                pnlOrganization.Visible = true;
                imgProduct.Visible = false;
                imgOrganization.Visible = true;
                imgMember.Visible = false;
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

        protected void lnkBtnOrganization_Click(object sender, EventArgs e)
        {
            SetParameters();
            BindOrganizationGrid();
            lblMFMISType.Text = "ORGANIZATION";
            trPnlProduct.Visible = false;
            trPnlOrganization.Visible = true;
            trMember.Visible = false;
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
            int accountId = 0;
            int accountIdOld = 0;
            DataSet dsGetMemberDetailFromMFTrnx = new DataSet();
            dsGetMemberDetailFromMFTrnx = associatesBo.GetMemberDetailFromTransaction(hdnsubBrokerCode.Value, userType, int.Parse(hdnadviserId.Value), int.Parse(hdnbranchHeadId.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), int.Parse(ddlFilter.SelectedValue));

            DataTable dtGetMemberDetailFromMFTrnx = new DataTable();
            dtGetMemberDetailFromMFTrnx.Columns.Add("AccountId");
            dtGetMemberDetailFromMFTrnx.Columns.Add("CustomerName");
            dtGetMemberDetailFromMFTrnx.Columns.Add("SubBrokerCode");
            dtGetMemberDetailFromMFTrnx.Columns.Add("AssociatesName");
            dtGetMemberDetailFromMFTrnx.Columns.Add("Folio");
            dtGetMemberDetailFromMFTrnx.Columns.Add("IsOnline");
            dtGetMemberDetailFromMFTrnx.Columns.Add("ChannelName");
            dtGetMemberDetailFromMFTrnx.Columns.Add("Titles");
            dtGetMemberDetailFromMFTrnx.Columns.Add("ClusterManager");
            dtGetMemberDetailFromMFTrnx.Columns.Add("AreaManager");
            dtGetMemberDetailFromMFTrnx.Columns.Add("ZonalManagerName");
            dtGetMemberDetailFromMFTrnx.Columns.Add("DeputyHead");
            dtGetMemberDetailFromMFTrnx.Columns.Add("BUYCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("BUYAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("SELCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("SELAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("DVRCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("DVRAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("DVPCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("DVPAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("SIPCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("SIPAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("BCICount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("BCIAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("BCOCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("BCOAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("STBCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("STBAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("STSCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("STSAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("SWBCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("SWBAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("SWPCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("SWPAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("SWSCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("SWSAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("PRJCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("PRJAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("ABYCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("ABYAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("BIRCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("BIRAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("BNSCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("BNSAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("CNICount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("CNIAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("CNOCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("CNOAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("DSICount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("DSIAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("DSOCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("DSOAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("HLDCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("HLDAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("NFOCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("NFOAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("RRJCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("RRJAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("SRJCount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("SRJAmount", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("GrossRedemption", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("GrossInvestment", typeof(double));
            dtGetMemberDetailFromMFTrnx.Columns.Add("Net", typeof(double));

            //--------------------Default Value ------------------

            #region Data Table Default value
            dtGetMemberDetailFromMFTrnx.Columns["SELCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["SELAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["BUYCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["BUYAmount"].DefaultValue = 0;
            
            
            dtGetMemberDetailFromMFTrnx.Columns["GrossRedemption"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["GrossInvestment"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["Net"].DefaultValue = 0;

            dtGetMemberDetailFromMFTrnx.Columns["ABYCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["ABYAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["BIRCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["BIRAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["BNSCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["BNSAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["CNICount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["CNIAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["CNOCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["CNOAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["DSICount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["DSIAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["DSOCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["DSOAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["HLDCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["HLDAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["NFOCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["NFOAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["RRJCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["RRJAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["SRJCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["SRJAmount"].DefaultValue = 0;

            dtGetMemberDetailFromMFTrnx.Columns["DVRCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["DVRAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["DVPCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["DVPAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["SIPCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["SIPAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["BCICount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["BCIAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["BCOCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["BCOAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["STBCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["STBAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["STSCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["STSAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["SWBCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["SWBAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["SWPCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["SWPAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["SWSCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["SWSAmount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["PRJCount"].DefaultValue = 0;
            dtGetMemberDetailFromMFTrnx.Columns["PRJAmount"].DefaultValue = 0;

            #endregion



            DataRow drGetOrganizationDetailFromTrnx;
            DataRow[] drOrderOrgWise;
            if (dsGetMemberDetailFromMFTrnx.Tables[0] != null)
            {
                DataTable dtGetOrgOrderTransaction = dsGetMemberDetailFromMFTrnx.Tables[0];

                foreach (DataRow drOrgOrderTransaction in dtGetOrgOrderTransaction.Rows)
                {

                    Int32.TryParse(drOrgOrderTransaction["CMFA_AccountId"].ToString(), out accountId);

                    if (accountId != accountIdOld)
                    { //go for another row to find new customer
                        accountIdOld = accountId;
                        drGetOrganizationDetailFromTrnx = dtGetMemberDetailFromMFTrnx.NewRow();
                        if (accountId != 0)
                        { // add row in manual datatable within this brace end
                            drOrderOrgWise = dtGetOrgOrderTransaction.Select("CMFA_AccountId=" + accountId.ToString());
                            drGetOrganizationDetailFromTrnx["AccountId"] = drOrgOrderTransaction["CMFA_AccountId"].ToString();
                            drGetOrganizationDetailFromTrnx["CustomerName"] = drOrgOrderTransaction["CustomerName"].ToString();
                            drGetOrganizationDetailFromTrnx["SubBrokerCode"] = drOrgOrderTransaction["CMFT_SubBrokerCode"].ToString();
                            drGetOrganizationDetailFromTrnx["AssociatesName"] = drOrgOrderTransaction["AssociatesName"].ToString();
                            drGetOrganizationDetailFromTrnx["ChannelName"] = drOrgOrderTransaction["ChannelName"].ToString();
                            drGetOrganizationDetailFromTrnx["Titles"] = drOrgOrderTransaction["Titles"].ToString();
                            drGetOrganizationDetailFromTrnx["ClusterManager"] = drOrgOrderTransaction["ClusterManager"].ToString();
                            drGetOrganizationDetailFromTrnx["AreaManager"] = drOrgOrderTransaction["AreaManager"].ToString();
                            drGetOrganizationDetailFromTrnx["ZonalManagerName"] = drOrgOrderTransaction["ZonalManagerName"].ToString();
                            drGetOrganizationDetailFromTrnx["DeputyHead"] = drOrgOrderTransaction["DeputyHead"].ToString();
                            drGetOrganizationDetailFromTrnx["Folio"] = drOrgOrderTransaction["CMFA_FolioNum"].ToString();
                            drGetOrganizationDetailFromTrnx["IsOnline"] = drOrgOrderTransaction["IsOnline"].ToString();
                            if (drOrderOrgWise.Count() > 0)
                            {
                                foreach (DataRow dr in drOrderOrgWise)
                                {

                                    string transactiontype = dr["WMTT_TransactionClassificationCode"].ToString();
                                    switch (transactiontype)
                                    {
                                        case "BUY":
                                            {
                                                drGetOrganizationDetailFromTrnx["BUYCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["BUYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["BUYAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SEL":
                                            {
                                                drGetOrganizationDetailFromTrnx["SELCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["SELAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["SELAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "DVR":
                                            {
                                                drGetOrganizationDetailFromTrnx["DVRCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["DVRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["DVRAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "DVP":
                                            {
                                                drGetOrganizationDetailFromTrnx["DVPCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["DVPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        case "SIP":
                                            {
                                                drGetOrganizationDetailFromTrnx["SIPCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["SIPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["SIPAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "BCI":
                                            {
                                                drGetOrganizationDetailFromTrnx["BCICount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["BCIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["BCIAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "BCO":
                                            {
                                                drGetOrganizationDetailFromTrnx["BCOCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["BCOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["BCOAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "STB":
                                            {
                                                drGetOrganizationDetailFromTrnx["STBCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["STBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["STBAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "STS":
                                            {
                                                drGetOrganizationDetailFromTrnx["STSCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["STSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["STSAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SWB":
                                            {
                                                drGetOrganizationDetailFromTrnx["SWBCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["SWBAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["SWBAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SWP":
                                            {
                                                drGetOrganizationDetailFromTrnx["SWPCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["SWPAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["SWPAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "SWS":
                                            {
                                                drGetOrganizationDetailFromTrnx["SWSCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["SWSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["SWSAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "PRJ":
                                            {
                                                drGetOrganizationDetailFromTrnx["PRJCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["PRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                break;
                                            }
                                        #region newly added
                                        case "ABY":
                                            {
                                                drGetOrganizationDetailFromTrnx["ABYCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["ABYAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["ABYAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "BIR":
                                            {
                                                drGetOrganizationDetailFromTrnx["BIRCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["BIRAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["BIRAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "BNS":
                                            {
                                                drGetOrganizationDetailFromTrnx["BNSCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["BNSAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["BNSAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "CNI":
                                            {
                                                drGetOrganizationDetailFromTrnx["CNICount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["CNIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["CNIAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "CNO":
                                            {
                                                drGetOrganizationDetailFromTrnx["CNOCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["CNOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["CNOAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "DSI":
                                            {
                                                drGetOrganizationDetailFromTrnx["DSICount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["DSIAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["DSIAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "DSO":
                                            {
                                                drGetOrganizationDetailFromTrnx["DSOCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["DSOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);
                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["DSOAmount"].ToString());
                                                }
                                                break;
                                            }
                                        case "HLD":
                                            {
                                                drGetOrganizationDetailFromTrnx["HLDCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["HLDAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["HLDAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "NFO":
                                            {
                                                drGetOrganizationDetailFromTrnx["NFOCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["NFOAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["NFOAmount"].ToString());
                                                }

                                                break;
                                            }


                                        case "RRJ":
                                            {
                                                drGetOrganizationDetailFromTrnx["RRJCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["RRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossInvestment"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossInvestment"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossInvestment"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["RRJAmount"].ToString());
                                                }

                                                break;
                                            }
                                        case "SRJ":
                                            {
                                                drGetOrganizationDetailFromTrnx["SRJCount"] = dr["TrnsCount"].ToString();
                                                drGetOrganizationDetailFromTrnx["SRJAmount"] = Math.Round(double.Parse(dr["TrnsAmount"].ToString()), 2);

                                                if (dr["WMTT_GrossRedemption"].ToString() == "1")
                                                {
                                                    drGetOrganizationDetailFromTrnx["GrossRedemption"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossRedemption"].ToString()) + double.Parse(drGetOrganizationDetailFromTrnx["SRJAmount"].ToString());
                                                }

                                                break;
                                            }
                                        #endregion
                                    }

                                }
                            }

                            drGetOrganizationDetailFromTrnx["Net"] = double.Parse(drGetOrganizationDetailFromTrnx["GrossInvestment"].ToString()) - double.Parse(drGetOrganizationDetailFromTrnx["GrossRedemption"].ToString());


                            dtGetMemberDetailFromMFTrnx.Rows.Add(drGetOrganizationDetailFromTrnx);
                        }//*

                    }//**

                }//***
                gvMember.DataSource = dtGetMemberDetailFromMFTrnx;
                gvMember.DataBind();
                pnlMember.Visible = true;
                gvMember.Visible = true;
                pnlOrganization.Visible = false;
                pnlProduct.Visible = false;
                imgProduct.Visible = false;
                imgOrganization.Visible = false;
                imgMember.Visible = true;
                this.gvMember.GroupingSettings.RetainGroupFootersVisibility = true;
                if (Cache["gvMember" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("gvMember" + advisorVo.advisorId, dtGetMemberDetailFromMFTrnx);
                }
                else
                {
                    Cache.Remove("gvMember" + advisorVo.advisorId);
                    Cache.Insert("gvMember" + advisorVo.advisorId, dtGetMemberDetailFromMFTrnx);
                }

            }
        }
        protected void gvMember_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                if (ddlFilter.SelectedValue == "1")
                {
                    gvMember.MasterTableView.GetColumn("SubBrokerCode").Visible = false;
                    gvMember.MasterTableView.GetColumn("AssociatesName").Visible = false;
                    gvMember.MasterTableView.GetColumn("Folio").Visible = false;
                    gvMember.MasterTableView.GetColumn("ChannelName").Visible = false;
                    gvMember.MasterTableView.GetColumn("Titles").Visible = false;
                    gvMember.MasterTableView.GetColumn("ClusterManager").Visible = false;
                    gvMember.MasterTableView.GetColumn("AreaManager").Visible = false;
                    gvMember.MasterTableView.GetColumn("ZonalManagerName").Visible = false;
                    gvMember.MasterTableView.GetColumn("DeputyHead").Visible = false;
                }
            }
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

            DataTable dtGetMemberDetailFromMFTrnx = new DataTable();
            dtGetMemberDetailFromMFTrnx = (DataTable)Cache["gvMember" + advisorVo.advisorId];
            gvMember.Visible = true;
            this.gvMember.DataSource = dtGetMemberDetailFromMFTrnx;

        }
    }
}