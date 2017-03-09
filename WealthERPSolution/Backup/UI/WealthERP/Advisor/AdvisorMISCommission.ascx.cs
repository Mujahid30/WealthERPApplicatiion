using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoAdvisorProfiling;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoProductMaster;
using BoCommon;
using System.Configuration;
using Telerik.Web.UI;
using BoWerpAdmin;
using BoCommisionManagement;
using BoOnlineOrderManagement;

namespace WealthERP.Advisor
{
    public partial class AdvisorMISCommission : System.Web.UI.UserControl
    {
        AdvisorMISBo advisorMISBo = new AdvisorMISBo();
        ProductMFBo productMFBo = new ProductMFBo();
        string path = string.Empty;
        DataSet dsMISCommission = new DataSet();
        UserVo userVo = new UserVo();
        DateBo dtBo = new DateBo();
        DateTime dtTo = new DateTime();
        DateTime dtFrom = new DateTime();
        AdvisorVo advisorVo = new AdvisorVo();
        int amccode = 0;
        int Schemecode = 0;
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["userVo"];
            if (!Page.IsPostBack)
            {
                BindProduct();
                ddlMISType.SelectedIndex = 0;
                txtFromDate.SelectedDate = DateTime.Now;
                txtToDate.SelectedDate = DateTime.Now;


            }

        }


        public void ddlMISType_SelectedIndexChanged(object a, EventArgs b)
        {
            if (ddlMISType.SelectedValue == "AMC_Folio_Type_AllMIS")
            {
                trAMCSelection.Visible = true;
                BindAMC();
            }
            else if (ddlMISType.SelectedValue == "Category Wise")
            {
                trAMCSelection.Visible = false;

            }
            else if (ddlMISType.SelectedValue == "Zone_Cluster_Wise")
            {
                trAMCSelection.Visible = false;
            }
        }
        /// <summary>
        /// Binding Period Dropdown From Xml File
        /// </summary>        
        private void BindPeriodDropDown()
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            DataTable dtPeriod;
            dtPeriod = XMLBo.GetDatePeriod(path);
            ddlPeriod.DataSource = dtPeriod;
            ddlPeriod.DataTextField = "PeriodType";
            ddlPeriod.DataValueField = "PeriodCode";
            ddlPeriod.DataBind();
            ddlPeriod.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select a Period", "0"));
            ddlPeriod.Items.RemoveAt(15);
            ddlPeriod.SelectedIndex = 0;
        }
        private void SetParameter()
        {
            //if (ddlSelectMutualFund.SelectedIndex != 0)
            //{
            //    amccode = ddlSelectMutualFund.SelectedValue;
            //    //ddlSelectMutualFund
            //    //hdnAMC.Value = ddlAMC.SelectedValue;
            //    //ViewState["AMCDropDown"] = hdnAMC.Value;
            //}
            ////else if (ViewState["AMCDropDown"] != null)
            ////{
            ////    hdnAMC.Value = ViewState["AMCDropDown"].ToString();
            ////}
            //else
            //{
            //    hdnAMC.Value = "0";
            //}
            //if (ddlScheme.SelectedIndex != 0)
            //{
            //    hdnScheme.Value = ddlScheme.SelectedValue;
            //    ViewState["DropdownScheme"] = hdnScheme.Value;
            //}
            ////else if (ViewState["CategoryDropDown"] != null)
            ////{
            ////    hdnCategory.Value = ViewState["CategoryDropDown"].ToString();
            ////}
            //else
            //{
            //    hdnScheme.Value = "0";
            //}
        }

        protected void ddlNAVCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //BindNavSubCategory();
            LoadAllSchemeNAV();
            //gvMFRecord.DataSource = null;
            //gvMFRecord.DataBind();
        }
        private void LoadAllSchemeList(int amcCode)
        {
            //PriceBo priceBo = new PriceBo();
            //DataSet dsLoadAllScheme = new DataSet();
            //DataTable dtLoadAllScheme = new DataTable();
            ////if (ddlAmcCode.SelectedIndex != 0 && ddlFactCategory.SelectedIndex == 0)
            ////{
            ////    amcCode = int.Parse(ddlAmcCode.SelectedValue.ToString());
            ////    categoryCode = ddlFactCategory.SelectedValue;
            ////    dsLoadAllScheme = priceBo.GetSchemeListCategoryConcatenation(amcCode, categoryCode);
            ////    dtLoadAllScheme = dsLoadAllScheme.Tables[0];
            ////}
            ////if (ddlAmcCode.SelectedIndex != 0 && ddlFactCategory.SelectedIndex != 0)
            ////{
            ////    amcCode = int.Parse(ddlAmcCode.SelectedValue.ToString());
            ////    categoryCode = ddlFactCategory.SelectedValue;
            ////    dsLoadAllScheme = priceBo.GetSchemeListCategoryConcatenation(amcCode, categoryCode);
            ////    dtLoadAllScheme = dsLoadAllScheme.Tables[0];
            ////}
            //if (dtLoadAllScheme.Rows.Count > 0)
            //{
            //    ddlSchemeList.DataSource = dtLoadAllScheme;
            //    ddlSchemeList.DataTextField = dtLoadAllScheme.Columns["PASP_SchemePlanName"].ToString();
            //    ddlSchemeList.DataValueField = dtLoadAllScheme.Columns["PASP_SchemePlanCode"].ToString();
            //    ddlSchemeList.DataBind();
            //    ddlSchemeList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            //}
            //else
            //{
            //    ddlSchemeList.Items.Clear();
            //    ddlSchemeList.DataSource = null;
            //    ddlSchemeList.DataBind();
            //    ddlSchemeList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            //}

        }

        protected void ddlSelectMutualFund_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllSchemeNAV();
            //   tdscheme.Visible = true;
            //gvMFRecord.DataSource = null;
            //gvMFRecord.DataBind();
        }
        public void LoadAllSchemeNAV()
        {

            PriceBo priceBo = new PriceBo();
            DataSet dsLoadAllSchemeNAV;
            DataTable dtLoadAllSchemeNAV = new DataTable();
            int amcCode;
            string categoryCode;
            //if (ddlSelectMutualFund.SelectedIndex != 0 && ddlNAVCategory.SelectedIndex == 0)
            //{
            amcCode = int.Parse(ddlSelectMutualFund.SelectedValue.ToString());
            categoryCode = ddlNAVCategory.SelectedValue;
            dsLoadAllSchemeNAV = priceBo.GetSchemeListCategoryConcatenation(amcCode, "ALL");
            dtLoadAllSchemeNAV = dsLoadAllSchemeNAV.Tables[0];

            //    }
            // if (ddlSelectMutualFund.SelectedIndex != 0 && ddlNAVCategory.SelectedIndex != 0)
            //{
            //    amcCode = int.Parse(ddlSelectMutualFund.SelectedValue.ToString());
            //    categoryCode = ddlNAVCategory.SelectedValue;
            //    //subCategory = ddlNAVSubCategory.SelectedValue;
            //    dsLoadAllSchemeNAV = priceBo.GetSchemeListCategoryConcatenation(amcCode, categoryCode);
            //    dtLoadAllSchemeNAV = dsLoadAllSchemeNAV.Tables[0];
            //}
            if (dtLoadAllSchemeNAV.Rows.Count > 0)
            {
                ddlSelectSchemeNAV.DataSource = dtLoadAllSchemeNAV;
                ddlSelectSchemeNAV.DataTextField = dtLoadAllSchemeNAV.Columns["PASP_SchemePlanName"].ToString();
                ddlSelectSchemeNAV.DataValueField = dtLoadAllSchemeNAV.Columns["PASP_SchemePlanCode"].ToString();
                ddlSelectSchemeNAV.DataBind();
                ddlSelectSchemeNAV.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Scheme", "0"));
            }
            else
            {
                ddlSelectSchemeNAV.Items.Clear();
                ddlSelectSchemeNAV.DataSource = null;
                ddlSelectSchemeNAV.DataBind();
                ddlSelectSchemeNAV.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            //ddlSelectSchemeNAV.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));

        }

        public void BindCommissionMISGridCategoryWise()
        {
            DataTable dtMIS;
            //string misType = null;
            //ddlMISType.SelectedValue = misType;
            userVo = (UserVo)Session["userVo"];
            double sumTotal;
            if (hdnCurrentPage.Value.ToString() != "")
            {
            }
            dsMISCommission = advisorMISBo.GetMFMISCommission(advisorVo.advisorId, hdnMISType.Value.ToString(), DateTime.Parse(hdnFromDate.Value.ToString()), DateTime.Parse(hdnToDate.Value.ToString()), out sumTotal);
            if (dsMISCommission.Tables[0].Rows.Count > 0)
            {
                imgMISCommission.Visible = true;
                imgZoneClusterCommissionMIS.Visible = false;
                btnCommissionMIS.Visible = false;
                //trCommissionMIS.Visible = true;
                dtMIS = dsMISCommission.Tables[0];
                string misType = hdnMISType.Value.ToString();
                tblMessage.Visible = false;
                ErrorMessage.Visible = false;
                Label lblHeaderText = new Label();
                GridBoundColumn ghItem = gvCommissionMIS.MasterTableView.Columns.FindByUniqueName("MISType") as GridBoundColumn;
                GridBoundColumn ghItem1 = gvCommissionMIS.MasterTableView.Columns.FindByUniqueName("CustomerName") as GridBoundColumn;
                GridBoundColumn ghItem2 = gvCommissionMIS.MasterTableView.Columns.FindByUniqueName("RM_Name") as GridBoundColumn;
                GridBoundColumn ghItem3 = gvCommissionMIS.MasterTableView.Columns.FindByUniqueName("AB_BranchName") as GridBoundColumn;
                switch (misType)
                {
                    case "Folio Wise":
                        ghItem.HeaderText = "Folio Number";
                        ghItem.DataField = "folio";
                        ghItem1.Visible = true;
                        ghItem2.Visible = true;
                        ghItem3.Visible = true;
                        break;
                    case "AMC Wise":
                        ghItem.HeaderText = "AMC Name";
                        ghItem.DataField = "AMCCODE";
                        ghItem1.Visible = true;
                        ghItem2.Visible = true;
                        ghItem3.Visible = true;
                        break;
                    case "Transaction_Wise":
                        ghItem.HeaderText = "Transaction Classification Name";
                        ghItem.DataField = "TransactionType";
                        ghItem1.Visible = true;
                        ghItem2.Visible = true;
                        ghItem3.Visible = true;
                        break;
                    case "Category Wise":
                        ghItem.HeaderText = "Category";
                        ghItem.DataField = "categoryName";
                        ghItem1.Visible = false;
                        ghItem2.Visible = false;
                        ghItem3.Visible = false;
                        ghItem.FooterText = "Grand Total:";
                        ghItem.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
                        break;
                    default:
                        ghItem.HeaderText = "Folio Number";
                        ghItem.DataField = "";
                        ghItem1.Visible = true;
                        ghItem2.Visible = true;
                        ghItem3.Visible = true;
                        break;
                }



                gvCommissionMIS.DataSource = dtMIS;
                gvCommissionMIS.CurrentPageIndex = 0;
                gvCommissionMIS.DataBind();
                gvCommissionMIS.Visible = true;

                if (Cache["MIS" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("MIS" + advisorVo.advisorId, dtMIS);
                }
                else
                {
                    Cache.Remove("MIS" + advisorVo.advisorId);
                    Cache.Insert("MIS" + advisorVo.advisorId, dtMIS);
                }


            }
            else
            {
                gvCommissionMIS.Visible = false;
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
            }
        }


        private void BindAMC()
        {
            DataSet dsProductAmc;
            DataTable dtProductAMC;
            try
            {
                dsProductAmc = productMFBo.GetProductAmc();
                if (dsProductAmc.Tables[0].Rows.Count > 0)
                {
                    dtProductAMC = dsProductAmc.Tables[0];
                    ddlSelectMutualFund.DataSource = dtProductAMC;
                    ddlSelectMutualFund.DataTextField = dtProductAMC.Columns["PA_AMCName"].ToString();
                    ddlSelectMutualFund.DataValueField = dtProductAMC.Columns["PA_AMCCode"].ToString();
                    ddlSelectMutualFund.DataBind();
                }
                ddlSelectMutualFund.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
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

                object[] objects = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public void BindMISCommissionGrid()
        {
            //SetParameter();
            DataSet dsALLMISCommission = advisorMISBo.GetCommissionMIS(advisorVo.advisorId, hdnMISType.Value.ToString(), DateTime.Parse(hdnFromDate.Value.ToString()), DateTime.Parse(hdnToDate.Value.ToString()), Convert.ToInt32(ddlSelectMutualFund.SelectedValue), Convert.ToInt32(ddlSelectSchemeNAV.SelectedValue));
            if (dsALLMISCommission.Tables[0].Rows.Count > 0)
            {
                imgMISCommission.Visible = false;
                imgZoneClusterCommissionMIS.Visible = false;
                btnCommissionMIS.Visible = true;

                //trCommissionMIS.Visible = true;

                string misType = hdnMISType.Value.ToString();
                tblMessage.Visible = false;
                ErrorMessage.Visible = false;
                gvMISCommission.DataSource = dsALLMISCommission.Tables[0];
                gvMISCommission.CurrentPageIndex = 0;
                gvMISCommission.DataBind();
                gvMISCommission.Visible = true;
                this.gvMISCommission.GroupingSettings.RetainGroupFootersVisibility = true;
                divCommissionMIS.Visible = true;
                pnlCommissionMIS.Visible = true;
                if (Cache["AllMIS" + advisorVo.advisorId + userVo.UserId] == null)
                {
                    Cache.Insert("AllMIS" + advisorVo.advisorId + userVo.UserId, dsALLMISCommission.Tables[0]);
                }
                else
                {
                    Cache.Remove("AllMIS" + advisorVo.advisorId + userVo.UserId);
                    Cache.Insert("AllMIS" + advisorVo.advisorId + userVo.UserId, dsALLMISCommission.Tables[0]);
                }


            }
            else
            {
                pnlCommissionMIS.Visible = false;
                gvMISCommission.Visible = false;
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
            }
        }
        public void BindMISCommissionGridZoneCluster()
        {
            DataSet dsZoneClusterMISCommission = advisorMISBo.GetCommissionMISZoneClusterWise(advisorVo.advisorId, DateTime.Parse(hdnFromDate.Value.ToString()), DateTime.Parse(hdnToDate.Value.ToString()));
            if (dsZoneClusterMISCommission.Tables[0].Rows.Count > 0)
            {
                imgMISCommission.Visible = false;
                imgZoneClusterCommissionMIS.Visible = true;
                btnCommissionMIS.Visible = false;

                string misType = hdnMISType.Value.ToString();
                tblMessage.Visible = false;
                ErrorMessage.Visible = false;
                gvZoneClusterWiseCommissionMIS.DataSource = dsZoneClusterMISCommission.Tables[0];
                gvZoneClusterWiseCommissionMIS.CurrentPageIndex = 0;
                gvZoneClusterWiseCommissionMIS.DataBind();
                gvZoneClusterWiseCommissionMIS.Visible = true;
                this.gvZoneClusterWiseCommissionMIS.GroupingSettings.RetainGroupFootersVisibility = true;
                divZoneClusterWiseMIS.Visible = true;
                pnlZoneClusterWiseMIS.Visible = true;
                if (Cache["ClusterZoneMIS" + advisorVo.advisorId + userVo.UserId] == null)
                {
                    Cache.Insert("ClusterZoneMIS" + advisorVo.advisorId + userVo.UserId, dsZoneClusterMISCommission.Tables[0]);
                }
                else
                {
                    Cache.Remove("ClusterZoneMIS" + advisorVo.advisorId + userVo.UserId);
                    Cache.Insert("ClusterZoneMIS" + advisorVo.advisorId + userVo.UserId, dsZoneClusterMISCommission.Tables[0]);
                }


            }
            else
            {
                btnCommissionMIS.Visible = false;
                imgZoneClusterCommissionMIS.Visible = false;
                imgMISCommission.Visible = false;
                divZoneClusterWiseMIS.Visible = false;
                pnlZoneClusterWiseMIS.Visible = false;
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
            }
        }
        protected void gvZoneClusterWiseCommissionMIS_OnItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {

                //GridDataItem item = (GridDataItem)e.Item;
                //item["ZoneName"].BackColor = System.Drawing.Color.Red;

            }

        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            if (ddlMISType.SelectedValue == "0")
            {
                gvCommissionMIS.Columns[0].HeaderText = "Summary";
                gvNonMFMobilization.Columns[0].HeaderText = "Summary";
               // gvNonMFMobilization.Columns[0].Visible = false;
            }

            else if (ddlMISType.SelectedValue == "1")
            {
                gvCommissionMIS.Columns[0].HeaderText = "AMC";
            }
            else if (ddlMISType.SelectedValue == "2")
            {
                gvCommissionMIS.Columns[0].HeaderText = "Broker code ";
                gvNonMFMobilization.Columns[0].HeaderText = "Broker code";
            }
            else if (ddlMISType.SelectedValue == "3")
            {
                gvCommissionMIS.Columns[0].HeaderText = "Branch Wise";
                gvNonMFMobilization.Columns[0].HeaderText = "Branch Wise";
            }



            gvNonMFMobilization.Visible = false;
            gvCommissionMIS.Visible = false;
            btnCommissionMIS.Visible = true;
            DataTable dtGetProductMobilizedReport = new DataTable();
            dtGetProductMobilizedReport = advisorMISBo.GetProductMobilizedReport(advisorVo.advisorId, int.Parse(ddlMISType.SelectedValue), int.Parse(rcbMode.SelectedValue), ((!string.IsNullOrEmpty(rcbOnlineMode.SelectedValue)) ? int.Parse(rcbOnlineMode.SelectedValue) : 2), (!string.IsNullOrEmpty(rcbIssueName.SelectedValue)) ? int.Parse(rcbIssueName.SelectedValue) : 0, rcbProductType.SelectedValue, rcbProductType.SelectedValue == "FI" ? (!string.IsNullOrEmpty(RcbProductCategory.SelectedValue)) ? RcbProductCategory.SelectedValue : string.Empty : "FIFIIP", DateTime.Parse((txtFromDate.SelectedDate.Value).ToString()), DateTime.Parse((txtToDate.SelectedDate.Value).ToString()));
            if (rcbProductType.SelectedValue == "MF")
            {
                gvCommissionMIS.DataSource = dtGetProductMobilizedReport;
                gvCommissionMIS.DataBind();
                gvCommissionMIS.Visible = true;

                if (Cache["ProductMobilizedReportMF" + advisorVo.advisorId + userVo.UserId] != null)
                {
                    Cache.Remove("ProductMobilizedReportMF" + advisorVo.advisorId + userVo.UserId);
                }
                Cache.Insert("ProductMobilizedReportMF" + advisorVo.advisorId + userVo.UserId, dtGetProductMobilizedReport);
            }
            else
            {

                gvNonMFMobilization.DataSource = dtGetProductMobilizedReport;
                gvNonMFMobilization.DataBind();
                gvNonMFMobilization.Visible = true;

                if (Cache["ProductMobilizedReportNONMF" + advisorVo.advisorId + userVo.UserId] != null)
                {
                    Cache.Remove("ProductMobilizedReportNONMF" + advisorVo.advisorId + userVo.UserId);
                }
                Cache.Insert("ProductMobilizedReportNONMF" + advisorVo.advisorId + userVo.UserId, dtGetProductMobilizedReport);

            }
        }
        /// <summary>
        /// Get the From and To Date of reports
        /// </summary>
        private void CalculateDateRange(out DateTime fromDate, out DateTime toDate)
        {
            if (rbtnPickDate.Checked)
            {
                fromDate = DateTime.Parse((txtFromDate.SelectedDate.Value).ToString());
                toDate = DateTime.Parse((txtToDate.SelectedDate.Value).ToString());
            }
            else if (rbtnPickPeriod.Checked)
            {
                dtBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue.ToString(), out dtFrom, out dtTo);
                fromDate = dtFrom;
                toDate = dtTo;
            }
            else
            {
                fromDate = DateTime.MinValue;
                toDate = DateTime.MinValue;
            }
        }

        public void RadioButtonClick(object sender, EventArgs e)
        {
            if (rbtnPickPeriod.Checked)
            {
                lblPeriod.Visible = true;
                ddlPeriod.Visible = true;
                lblFromDate.Visible = false;
                txtFromDate.Visible = false;
                lblToDate.Visible = false;
                txtToDate.Visible = false;
                PickADateValidation.Visible = false;
                PickAPeriodValidation.Visible = true;
            }
            else if (rbtnPickDate.Checked)
            {
                lblPeriod.Visible = false;
                ddlPeriod.Visible = false;
                lblFromDate.Visible = true;
                txtFromDate.Visible = true;
                lblToDate.Visible = true;
                txtToDate.Visible = true;
                PickAPeriodValidation.Visible = false;
                PickADateValidation.Visible = true;
            }
        }
        protected void btnCategoryWise_OnClick(object sender, ImageClickEventArgs e)
        {
            gvCommissionMIS.ExportSettings.OpenInNewWindow = true;
            gvCommissionMIS.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvCommissionMIS.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvCommissionMIS.MasterTableView.ExportToExcel();
        }

        protected void btnCommissionMIS_OnClick(object sender, ImageClickEventArgs e)
        {
            if (rcbProductType.SelectedValue != "MF")
            {
                gvNonMFMobilization.ExportSettings.OpenInNewWindow = true;
                gvNonMFMobilization.ExportSettings.IgnorePaging = true;
                gvNonMFMobilization.ExportSettings.HideStructureColumns = true;
                gvNonMFMobilization.ExportSettings.ExportOnlyData = true;
                gvNonMFMobilization.ExportSettings.FileName = rcbProductType.Text + " " + txtFromDate.SelectedDate.Value.ToString("dd MMM yyyy") + "  " + txtToDate.SelectedDate.Value.ToString("dd MMM yyyy");
                gvNonMFMobilization.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gvNonMFMobilization.MasterTableView.ExportToExcel();
            }
            else
            {
                gvCommissionMIS.MasterTableView.ExportToExcel();
                gvCommissionMIS.ExportSettings.OpenInNewWindow = true;
                gvCommissionMIS.ExportSettings.IgnorePaging = true;
                gvCommissionMIS.ExportSettings.HideStructureColumns = true;
                gvCommissionMIS.ExportSettings.ExportOnlyData = true;
                gvCommissionMIS.ShowFooter = true;
                gvCommissionMIS.ExportSettings.FileName = rcbProductType.Text + " " + txtFromDate.SelectedDate.Value.ToString("dd MMM yyyy") + "  " + txtToDate.SelectedDate.Value.ToString("dd MMM yyyy");
                gvCommissionMIS.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gvCommissionMIS.MasterTableView.ExportToExcel();




            }
        }


        protected void btnZoneCLusterMISCommission_OnClick(object sender, ImageClickEventArgs e)
        {
            gvZoneClusterWiseCommissionMIS.ExportSettings.OpenInNewWindow = true;
            gvZoneClusterWiseCommissionMIS.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvZoneClusterWiseCommissionMIS.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvZoneClusterWiseCommissionMIS.MasterTableView.ExportToExcel();
        }


        public void gvCommissionMIS_OnNeedDataSource(object sender, EventArgs e)
        {
            gvCommissionMIS.Visible = true;
            DataTable dtMIS = new DataTable();
            dtMIS = (DataTable)Cache["ProductMobilizedReportMF" + advisorVo.advisorId + userVo.UserId];
            gvCommissionMIS.DataSource = dtMIS;
        }
        public void gvMISCommission_OnNeedDataSource(object sender, EventArgs e)
        {
            gvNonMFMobilization.Visible = true;
            DataTable dtFolioDetails = new DataTable();
            dtFolioDetails = (DataTable)Cache["ProductMobilizedReportNONMF" + advisorVo.advisorId + userVo.UserId];
            gvNonMFMobilization.DataSource = dtFolioDetails;

        }
        public void gvZoneClusterWiseCommissionMIS_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //gvZoneClusterWiseCommissionMIS.Visible = true;
            pnlZoneClusterWiseMIS.Visible = true;
            divZoneClusterWiseMIS.Visible = true;
            //tdZoneClusterCommissionMIS.Visible = true;
            DataTable dtMIS = new DataTable();
            dtMIS = (DataTable)Cache["ClusterZoneMIS" + advisorVo.advisorId + userVo.UserId];
            gvZoneClusterWiseCommissionMIS.DataSource = dtMIS;
            if (dtMIS != null)
            {
                gvZoneClusterWiseCommissionMIS.Visible = true;
            }
        }

        private void BindProduct()
        {
            CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();
            DataTable DtProductType = new DataTable();
            DtProductType = commisionReceivableBo.GetProductType().Tables[0];
            rcbProductType.DataSource = DtProductType;
            rcbProductType.DataValueField = DtProductType.Columns["PAG_AssetGroupCode"].ToString();
            rcbProductType.DataTextField = DtProductType.Columns["PAG_AssetGroupName"].ToString();
            rcbProductType.DataBind();
            rcbProductType.Items.Insert(0, new RadComboBoxItem("Select", "Select"));
            rcbProductType.SelectedIndex = 0;

        }
        protected void rcbProductType_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            tdIssueName.Visible = false;
            tdlblIssueName.Visible = false;
            if (rcbProductType.SelectedValue == "FI")
            {
                tdCategory.Visible = true;
                tdDdlCategory.Visible = true;
                BindBondCategories();
                ddlMISType.Items.FindItemByValue("1").Enabled = false;

            }
            else
            {
                tdCategory.Visible = false;
                tdDdlCategory.Visible = false;
                ddlMISType.Items.FindItemByValue("1").Enabled = true;
                if (rcbProductType.SelectedValue == "IP")
                {
                    tdIssueName.Visible = true;
                    tdlblIssueName.Visible = true;
                    BindIssueName();
                    ddlMISType.Items.FindItemByValue("1").Enabled = false;
                }

            }
        }
        protected void RcbProductCategory_OnSelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {


            if (RcbProductCategory.SelectedValue == "FICDCD" && rcbProductType.SelectedValue == "FI")
            {

                rcbMode.Items.FindItemByValue("1").Enabled = false;
                rcbMode.Items.FindItemByValue("2").Enabled = false;
                rcbMode.Items.FindItemByValue("0").Enabled = true;
                ddlMISType.Items.FindItemByValue("2").Enabled = true;
                ddlMISType.Items.FindItemByValue("3").Enabled = true;

            }
            else if (RcbProductCategory.SelectedValue == "FICGCG" && rcbProductType.SelectedValue == "FI")
            {
                rcbMode.Items.FindItemByValue("1").Enabled = false;
                rcbMode.Items.FindItemByValue("2").Enabled = false;
                rcbMode.Items.FindItemByValue("0").Enabled = true;
                ddlMISType.Items.FindItemByValue("2").Enabled = true;
                ddlMISType.Items.FindItemByValue("3").Enabled = true;
            }
            else if (RcbProductCategory.SelectedValue == "FISSGB" && rcbProductType.SelectedValue == "FI")
            {
                rcbMode.Items.FindItemByValue("0").Enabled = true;
                rcbMode.Items.FindItemByValue("2").Enabled = false;
                ddlMISType.Items.FindItemByValue("2").Enabled = false;
                ddlMISType.Items.FindItemByValue("3").Enabled = false;

            }
            else if (RcbProductCategory.SelectedValue == "FITFTF" && rcbProductType.SelectedValue == "FI")
            {
                rcbMode.Items.FindItemByValue("1").Enabled = true;
                rcbMode.Items.FindItemByValue("2").Enabled = true;
                rcbMode.Items.FindItemByValue("0").Enabled = true;
                ddlMISType.Items.FindItemByValue("2").Enabled = true;
                ddlMISType.Items.FindItemByValue("3").Enabled = true;

            }
            else if (RcbProductCategory.SelectedValue == "FISDSD" && rcbProductType.SelectedValue == "FI")
            {
                rcbMode.Items.FindItemByValue("1").Enabled = true;
                rcbMode.Items.FindItemByValue("2").Enabled = true;
                rcbMode.Items.FindItemByValue("0").Enabled = true;
                ddlMISType.Items.FindItemByValue("2").Enabled = true;
                ddlMISType.Items.FindItemByValue("3").Enabled = true;
            }
            tdIssueName.Visible = true;
            tdlblIssueName.Visible = true;
            BindIssueName();


        }
        protected void rcbMode_OnSelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            tdOnline.Visible = false;
            tdrcbOnlineMode.Visible = false;
            if (rcbMode.SelectedValue == "1")
            {
                tdOnline.Visible = true;
                tdrcbOnlineMode.Visible = true;
            }

        }
        protected void BindIssueName()
        {
            DataTable dtGetIssueName = new DataTable();
            dtGetIssueName = onlineNCDBackOfficeBo.GetNCDSGBIssueName(advisorVo.advisorId, rcbProductType.SelectedValue == "FI" ? RcbProductCategory.SelectedValue : "FIFIIP");
            rcbIssueName.DataSource = dtGetIssueName;
            rcbIssueName.DataValueField = dtGetIssueName.Columns["AIM_IssueId"].ToString();
            rcbIssueName.DataTextField = dtGetIssueName.Columns["AIM_IssueName"].ToString();
            rcbIssueName.DataBind();
            rcbIssueName.Items.Insert(0, new RadComboBoxItem("ALL", "0"));
            rcbIssueName.SelectedIndex = 0;

        }
        private void BindBondCategories()
        {
            OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
            DataTable dtCategory = new DataTable();
            dtCategory = onlineNCDBackOfficeBo.BindNcdCategory("SubInstrumentCat", RcbProductCategory.SelectedValue).Tables[0];
            if (dtCategory.Rows.Count > 0)
            {
                RcbProductCategory.DataSource = dtCategory;
                RcbProductCategory.DataValueField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                RcbProductCategory.DataTextField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                RcbProductCategory.DataBind();
            }
            RcbProductCategory.Items.Insert(0, new RadComboBoxItem("Select", "Select"));
            RcbProductCategory.SelectedIndex = 0;

        }

    }
}
