using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VoUser;
using BoCommon;
using WealthERP.Base;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Telerik.Web.UI;
using BoResearch;
using VoResearch;
using BoCustomerRiskProfiling;
using System.Web.UI.HtmlControls;


namespace WealthERP.Research
{
    public partial class SchemeMappingToModelPortfolio : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        DataSet dsCategoryList = new DataSet();
        RiskProfileBo riskprofilebo = new RiskProfileBo();
        ModelPortfolioBo modelPortfolioBo = new ModelPortfolioBo();
        ModelPortfolioVo modelPortfolioVo = new ModelPortfolioVo();
        DropDownList dropdownAMC;
        DropDownList dropdownCategory;
        DropDownList dropdownSubCategory;
        DropDownList dropdownScheme;
        DropDownList dropdownArchive;
        HtmlTableRow trSubCategory;
        DataTable dtGetAMCList = new DataTable();
        DataTable dt = new DataTable();
        DataRow dr;


        double eqAllocation = 0;
        double dtAllocation = 0;

        string categoryCode;
        int amcCode = 0;
        string subCategory = "All";
        int AMPTBValueId = 0;
        int IsActiveFlag = 1;


        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            //bindRadGrid1();
            if (!IsPostBack)
            {
                BindSelectedMPDropDown();
                bindRadGrid1();
                bindHistryRadGrid();

                if (dt.Rows.Count == 0)
                {
                    dt.Columns.Add("PASP_SchemePlanName");
                    dt.Columns.Add("PASP_SchemePlanCode");
                    dt.Columns.Add("AMFMPD_AllocationPercentage");
                    dt.Columns.Add("AMFMPD_AddedOn");
                    dt.Columns.Add("AMFMPD_SchemeDescription");
                    dt.Columns.Add("AMFMPD_Id");
                    dt.Columns.Add("AMFMPD_RemovedOn");
                    dt.Columns.Add("XAR_ArchiveReason");
                    dt.Columns.Add("PA_AMCName");
                    dt.Columns.Add("PAIC_AssetInstrumentCategoryName");
                    dt.Columns.Add("PAISC_AssetInstrumentSubCategoryName");
                    dt.Columns.Add("AMFMPD_IsActive");
                    dt.Columns.Add("Flag");

                    Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                    //RadGrid1.DataSource = dt;
                    lblCategoryChart.Visible = false;
                    lblSubCategoryChart.Visible = false;
                }
                //tblPieChart.Visible = false;
            }

            //BindArchiveReasonDropDown();
        }

        public void bindRadGrid1()
        {
            DataTable dtRiskClass = new DataTable();
            DataTable dtClass = new DataTable();

            dtClass.Columns.Add("PASP_SchemePlanName");
            dtClass.Columns.Add("PASP_SchemePlanCode");
            dtClass.Columns.Add("AMFMPD_AllocationPercentage");
            dtClass.Columns.Add("AMFMPD_AddedOn");
            dtClass.Columns.Add("AMFMPD_SchemeDescription");
            dtClass.Columns.Add("AMFMPD_Id");
            dtClass.Columns.Add("AMFMPD_RemovedOn");
            dtClass.Columns.Add("XAR_ArchiveReason");
            dtClass.Columns.Add("PA_AMCName");
            dtClass.Columns.Add("PAIC_AssetInstrumentCategoryName");
            dtClass.Columns.Add("PAISC_AssetInstrumentSubCategoryName");
            dtClass.Columns.Add("AMFMPD_IsActive");
            dtClass.Columns.Add("Flag");
            if (ddlSelectedMP.SelectedValue != "0" && ddlSelectedMP.SelectedValue != "")
            {
                //modelPortfolioVo.ModelPortfolioCode = Convert.ToInt32(ddlSelectedMP.SelectedValue);
                dtRiskClass = modelPortfolioBo.GetAttachedSchemeDetails(Convert.ToInt32(ddlSelectedMP.SelectedValue), advisorVo.advisorId);
                
                if (dtRiskClass.Rows.Count > 0)
                {

                    DataRow drRiskClass;
                    foreach (DataRow dr in dtRiskClass.Rows)
                    {
                        drRiskClass = dtClass.NewRow();
                        drRiskClass["PASP_SchemePlanName"] = dr["PASP_SchemePlanName"].ToString();
                        drRiskClass["PASP_SchemePlanCode"] = dr["PASP_SchemePlanCode"].ToString();
                        drRiskClass["AMFMPD_AllocationPercentage"] = dr["AMFMPD_AllocationPercentage"].ToString();
                        drRiskClass["AMFMPD_AddedOn"] =DateTime.Parse(dr["AMFMPD_AddedOn"].ToString()).ToShortDateString();
                        drRiskClass["AMFMPD_Id"] = dr["AMFMPD_Id"].ToString();
                        drRiskClass["AMFMPD_SchemeDescription"] = dr["AMFMPD_SchemeDescription"].ToString();
                        drRiskClass["PA_AMCName"] = dr["PA_AMCName"].ToString();
                        drRiskClass["PAIC_AssetInstrumentCategoryName"] = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        drRiskClass["PAISC_AssetInstrumentSubCategoryName"] = dr["PAISC_AssetInstrumentSubCategoryName"].ToString();
                        drRiskClass["AMFMPD_IsActive"] = dr["AMFMPD_IsActive"].ToString();
                        drRiskClass["XAR_ArchiveReason"] = dr["XAR_ArchiveReason"].ToString();
                        drRiskClass["Flag"] = dr["Flag"].ToString();
                        dtClass.Rows.Add(drRiskClass);
                    }
                    Session[SessionContents.FPS_AddProspect_DataTable] = dtClass;
                    RadGrid1.DataSource = dtClass;
                    RadGrid1.DataSourceID = String.Empty;
                    RadGrid1.DataBind();
                }
                   
                else
                {
                    //Session.Remove(SessionContents.FPS_AddProspect_DataTable);
                    if (dtRiskClass.Rows.Count > 0)
                    {
                        dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                        RadGrid1.DataSource = dt;
                        RadGrid1.DataBind();
                    }
                    else
                    {
                        RadGrid1.DataSource = dtRiskClass;
                        RadGrid1.DataBind();
                    }
                    //tableGrid.Visible = false;
                }
                Session[SessionContents.FPS_AddProspect_DataTable] = dtClass;
                //if(dtClass.Rows.Count != 0)
                //Session[SessionContents.FPS_AddProspect_DataTable] = dtClass;
                tableGrid.Visible = true;
                tableNote.Visible = true;
            }
            else
            {
                tableGrid.Visible = false;
                tableNote.Visible = false;
                tblSelectddl.Visible = true;
            }
        }

        public void getAllocationPercentageFromModelPortFolio()
        {
            DataTable dt;
            dt = modelPortfolioBo.getAllocationPercentageFromModelPortFolio(Convert.ToInt32(ddlSelectedMP.SelectedValue));

            if (dt.Rows.Count > 0)
            {
                DataRow[] drArrayAllocation;
                drArrayAllocation = new DataRow[dt.Rows.Count];
                drArrayAllocation = dt.Select("XAMP_ModelPortfolioCode=" + Convert.ToInt32(ddlSelectedMP.SelectedValue));

                txtEquity.Text = drArrayAllocation[0][3].ToString();
                txtDebt.Text = drArrayAllocation[1][3].ToString();
                txtCash.Text = drArrayAllocation[2][3].ToString();
                txtAlternate.Text = drArrayAllocation[3][3].ToString();
            }
            if (txtEquitySelected.Text != "")
                txtEquitySelected.Text = Convert.ToInt32(eqAllocation).ToString();
            else
                txtEquitySelected.Text = "0";
            if (txtDebtSelected.Text != "")
                txtDebtSelected.Text = Convert.ToInt32(dtAllocation).ToString();
            else
                txtDebtSelected.Text = "0";

            txtCashSelected.Text = "0";
            txtAlternateSelected.Text = "0";

            txtEquityGap.Text = Convert.ToInt32(double.Parse(txtEquity.Text) - double.Parse(txtEquitySelected.Text)).ToString();
            txtDebtGap.Text = Convert.ToInt32(double.Parse(txtDebt.Text) - double.Parse(txtDebtSelected.Text)).ToString();
            txtCashGap.Text = Convert.ToInt32(double.Parse(txtCashSelected.Text) - double.Parse(txtCash.Text)).ToString();
            txtAlternateGap.Text = Convert.ToInt32(double.Parse(txtAlternate.Text) - double.Parse(txtAlternateSelected.Text)).ToString();
        }

        protected void RadGrid1_DataBound(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            //try
            //{  
            //    TextBox weightage = (TextBox)e.Item.FindControl("txtWeightage");
            //    TextBox description = (TextBox)e.Item.FindControl("txtSchemeDescription");
            //    modelPortfolioVo.Weightage = Convert.ToInt32(weightage.Text);
            //    modelPortfolioVo.SchemeDescription = description.Text;
            //    modelPortfolioVo.ModelPortfolioCode = Convert.ToInt32(ddlSelectedMP.SelectedValue);
            //    modelPortfolioVo.SchemeCode = Convert.ToInt32(dropdownScheme.SelectedValue);                
            //    modelPortfolioVo.ArchiveReason = 0;

            //    dt=(DataTable)Session[SessionContents.FPS_AddProspect_DataTable];

            //    dr = dt.NewRow();
            //    dr["PASP_SchemePlanName"] = dropdownScheme.SelectedItem;
            //    dr["PASP_SchemePlanCode"] = dropdownScheme.SelectedValue;
            //    dr["AMFMPD_AllocationPercentage"] = weightage.Text;
            //    dr["AMFMPD_SchemeDescription"] = description.Text;
            //    dr["AMFMPD_AddedOn"] = DateTime.Now.ToString();

            //    dt.Rows.Add(dr);

            //    //Session[SessionContents.FPS_AddProspect_DataTable] = dt;
            //    RadGrid1.DataSource = dt;               
            //    RadGrid1.Rebind();
            //}
            //catch (Exception ex)
            //{
            //    RadGrid1.Controls.Add(new LiteralControl("Unable to insert Scheme. Reason: " + ex.Message));
            //    e.Canceled = true;
            //} 
        }

        protected void Rebind()
        {
            dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
            RadGrid1.DataSource = dt;
        }
        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //try
            //{                
            //    int modelId = Convert.ToInt32(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AMFMPD_Id"].ToString());
            //    modelPortfolioBo.DeleteSchemeFromModelPortfolio(modelId, advisorVo.advisorId);
            //    RadGrid1.Rebind();
            //}
            //catch (Exception ex)
            //{
            //    RadGrid1.Controls.Add(new LiteralControl("Unable to insert Employee. Reason: " + ex.Message));
            //    e.Canceled = true;
            //} 
            try
            {                
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                dt.Rows[e.Item.ItemIndex].Delete();

                Rebind();
                bindAssetChart();
                //tblPieChart.Visible = false;
                //lblCategoryChart.Visible = false;
                lblSubCategoryChart.Visible = false;
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw ex;
            }
        }

        private void RowDeletionFunction()
        {
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "javascript:showmessage();", true);
        }

        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {            
            /*********************************This code is for updating the percentage & Description of attached scheme**************************************************/
            //try
            //{
            //    TextBox weightage = (TextBox)e.Item.FindControl("txtWeightage");
            //    TextBox description = (TextBox)e.Item.FindControl("txtSchemeDescription");

            //    dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
            //    DataRow[] drRow = dt.Select("AMFMPD_Id=" + Convert.ToInt32(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AMFMPD_Id"].ToString()));

            //    foreach (DataRow dr in drRow)
            //    {
            //        dr["AMFMPD_AllocationPercentage"] = weightage.Text;
            //        dr["AMFMPD_SchemeDescription"] = description.Text;
            //        dr["AMFMPD_AddedOn"] = DateTime.Now.ToString();
            //        dt.AcceptChanges();
            //    }
            //    RadGrid1.DataSource = dt;
            //    RadGrid1.Rebind();
            //}
            //catch (Exception ex)
            //{
            //    RadGrid1.Controls.Add(new LiteralControl("Unable to insert Scheme. Reason: " + ex.Message));
            //    e.Canceled = true;
            //}

            /*********************************This code End**************************************************/

            //try
            //{
            //    TextBox weightage = (TextBox)e.Item.FindControl("txtWeightage");
            //    TextBox description = (TextBox)e.Item.FindControl("txtSchemeDescription");
            //    modelPortfolioVo.Weightage = Convert.ToInt32(weightage.Text);
            //    modelPortfolioVo.SchemeDescription = description.Text;
            //    modelPortfolioVo.ModelPortfolioCode = Convert.ToInt32(ddlSelectedMP.SelectedValue);
            //    modelPortfolioVo.SchemeCode = Convert.ToInt32(dropdownScheme.SelectedValue);
            //    IsActiveFlag = 0;
            //    if (dropdownArchive.SelectedIndex != 0)
            //        modelPortfolioVo.ArchiveReason = Convert.ToInt32(dropdownArchive.SelectedValue);
            //    //modelPortfolioVo.ArchiveReason = 1;
            //    modelPortfolioBo.AttachSchemeToPortfolio(modelPortfolioVo, advisorVo.advisorId, IsActiveFlag);
            //    RadGrid1.Rebind();
            //}
            //catch (Exception ex)
            //{
            //    RadGrid1.Controls.Add(new LiteralControl("Unable to update Scheme. Reason: " + ex.Message));
            //    e.Canceled = true;
            //} 
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
            {
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                //bindRadGrid1();
                RadGrid1.DataSource = dt;
                RadGrid1.Rebind();
                bindAssetChart();
                bindAssetChartOnSubCategory();
            }
            else if (e.CommandName == RadGrid.RebindGridCommandName && e.Item.OwnerTableView.IsItemInserted)
            {
                e.Canceled = true;
            }
            else if (e.CommandName == RadGrid.EditCommandName)
            {
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                RadGrid1.DataSource = dt;
                RadGrid1.Rebind();

            }
            else if (e.CommandName == RadGrid.DeleteCommandName)
            {

            }
            else if (e.CommandName == "Update")
            {
                try
                {                    
                    modelPortfolioVo.ArchiveReason = Convert.ToInt32(dropdownArchive.SelectedValue);
                    //modelPortfolioBo.DeleteSchemeFromModelPortfolio(modelId, advisorVo.advisorId);
                    dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                    int modelId = Convert.ToInt32(dt.Rows[e.Item.ItemIndex]["AMFMPD_Id"].ToString());
                    int xarId = modelPortfolioVo.ArchiveReason;
                    //modelPortfolioBo.ArchiveSchemeFromModelPortfolio(modelId, xarId);

                    //dt.Rows[e.Item.ItemIndex].Delete();

                    DataRow[] drrow = dt.Select("AMFMPD_Id=" + modelId + "");
                    foreach (DataRow dr in drrow)
                    {
                        dr["AMFMPD_IsActive"] = 0;
                        dr["XAR_ArchiveReason"] = modelPortfolioVo.ArchiveReason;
                    }

                    dt.AcceptChanges();

                                       
                    RadGrid1.DataSource = dt;
                    RadGrid1.Rebind();
                    Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                    bindAssetChart();
                    //tblPieChart.Visible = false;
                    //lblCategoryChart.Visible = false;
                    lblSubCategoryChart.Visible = false;
                }
                catch (Exception ex)
                {
                    RadGrid1.Controls.Add(new LiteralControl("Unable to Archive. Reason: " + ex.Message));
                    e.Canceled = true;
                }
            }
            else if (e.CommandName == "Archive")
            {
                //try
                //{
                //    int modelId = Convert.ToInt32(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AMFMPD_Id"].ToString());
                //    modelPortfolioBo.DeleteSchemeFromModelPortfolio(modelId, advisorVo.advisorId);
                //    RadGrid1.Rebind();
                //}
                //catch (Exception ex)
                //{
                //    RadGrid1.Controls.Add(new LiteralControl("Unable to insert Employee. Reason: " + ex.Message));
                //    e.Canceled = true;
                //}
            }
            else if (e.CommandName == "PerformInsert")
            {
                try
                {
                    TextBox weightage = (TextBox)e.Item.FindControl("txtWeightage");
                    TextBox description = (TextBox)e.Item.FindControl("txtSchemeDescription");
                    modelPortfolioVo.Weightage = Convert.ToInt32(weightage.Text);
                    modelPortfolioVo.SchemeDescription = description.Text;
                    modelPortfolioVo.ModelPortfolioCode = Convert.ToInt32(ddlSelectedMP.SelectedValue);
                    modelPortfolioVo.SchemeCode = Convert.ToInt32(dropdownScheme.SelectedValue);
                    //modelPortfolioVo.ArchiveReason = 0;
                    dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                    dr = dt.NewRow();
                    dr["PASP_SchemePlanName"] = dropdownScheme.SelectedItem;
                    dr["PASP_SchemePlanCode"] = dropdownScheme.SelectedValue;
                    dr["AMFMPD_AllocationPercentage"] = weightage.Text;
                    dr["AMFMPD_SchemeDescription"] = description.Text;
                    dr["AMFMPD_AddedOn"] = DateTime.Now.ToString();

                    dr["PA_AMCName"] = dropdownAMC.SelectedItem;
                    dr["PAIC_AssetInstrumentCategoryName"] = dropdownCategory.SelectedItem;
                    dr["PAISC_AssetInstrumentSubCategoryName"] = dropdownSubCategory.SelectedItem;
                    dr["AMFMPD_IsActive"] = 1;
                    dr["XAR_ArchiveReason"] = string.Empty;
                    dr["Flag"] = "UI";
                    dt.Rows.Add(dr);

                    //Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                    RadGrid1.DataSource = dt;
                    RadGrid1.Rebind();
                    bindAssetChart();
                    lblSubCategoryChart.Visible = false;
                }
                catch (Exception ex)
                {
                    RadGrid1.Controls.Add(new LiteralControl("Unable to insert Scheme. Reason: " + ex.Message));
                    e.Canceled = true;
                }
            }
            else
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
                if (!editColumn.Visible)
                    editColumn.Visible = true;

                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];                
                RadGrid1.DataSource = dt;
                RadGrid1.Rebind();
                bindAssetChart();
                bindAssetChartOnSubCategory();
            }
        }

        protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                //the dropdown list will be the first control in the Controls collection of the corresponding cell
                GridEditableItem edititem = (GridEditableItem)e.Item;
                GridEditFormItem editform = (GridEditFormItem)e.Item;
                dropdownAMC = (DropDownList)edititem.FindControl("ddlAMC");
                dropdownCategory = (DropDownList)edititem.FindControl("ddlCategory");
                dropdownSubCategory = (DropDownList)edititem.FindControl("ddlSubCategory");
                dropdownScheme = (DropDownList)edititem.FindControl("ddlScheme");
                dropdownArchive = (DropDownList)edititem.FindControl("ddlArchive");
                trSubCategory = (HtmlTableRow)editform.FindControl("divSubCategory");
                //attach SelectedIndexChanged event for the combobox control
                dropdownAMC.SelectedIndexChanged += new EventHandler(ddlAMC_SelectedIndexChanged);
            }
        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "getControl();", true);
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {      
            DataTable dt = new DataTable();
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string schemeName = dataItem["PASP_SchemePlanName"].Text;

                LinkButton button = dataItem["EditCommandColumn"].Controls[0] as LinkButton;
                LinkButton buttonDelete = dataItem["column"].Controls[0] as LinkButton;
                //button.Attributes["onclick"] = "return confirm('Are you sure you want to Archive " +
                //schemeName + "?')";

               string isActive = dataItem["AMFMPD_IsActive"].Text;
               if (isActive == "0")
               {
                   dataItem["EditCommandColumn"].BackColor = System.Drawing.Color.LightGray;
                   dataItem["PASP_SchemePlanName"].BackColor = System.Drawing.Color.LightGray;
                   dataItem["AMFMPD_AllocationPercentage"].BackColor = System.Drawing.Color.LightGray;
                   dataItem["AMFMPD_AddedOn"].BackColor = System.Drawing.Color.LightGray;
                   dataItem["AMFMPD_SchemeDescription"].BackColor = System.Drawing.Color.LightGray;
                   dataItem["column"].BackColor = System.Drawing.Color.LightGray;
                   dataItem["EditCommandColumn"].Text = "Marked for Archive";
                   //button.Attributes["onclick"] = "return alert('You can not Archive this scheme')";
                   
               }
                 string flag = dataItem["Flag"].Text;
                 if (flag == "UI")
                 {

                     dataItem["EditCommandColumn"].Enabled = false;
                     button.Enabled = false;
                     button.Attributes["onclick"] = "return alert('You can not Archive this scheme')";
                     buttonDelete.Attributes["onclick"] = "return confirm('Are you sure you want to delete the Scheme?')";
                     //dataItem["EditCommandColumn"].BackColor = System.Drawing.Color.Green;
                 }
                 else
                 {
                     dataItem["column"].Enabled = false;
                     buttonDelete.Attributes["onclick"] = "return alert('You can not Delete this scheme')";
                     button.Attributes["onclick"] = "return confirm('Are you sure you want to Archive " +
                     schemeName + "?')";
                 }
                //dataItem["AMFMPD_IsActive"].BackColor = System.Drawing.Color.Red;

                //GridBoundColumn colDates =
                //RadGrid1.Columns.FindByUniqueName("AMFMPD_AddedOn") as GridBoundColumn;
                //colDates.DataFormatString = Convert.ToDateTime(dataItem["AMFMPD_AddedOn"].Text).ToString("MM/dd/yy");

            }
            if (e.Item is GridCommandItem)
            {
                GridCommandItem cmditm = (GridCommandItem)e.Item;
                //to hide AddNewRecord button
                //cmditm.FindControl("InitInsertButton").Visible = false;//hide the text
                //cmditm.FindControl("AddNewRecordButton").Visible = false;//hide the image

                //to hide Refresh button
                cmditm.FindControl("RefreshButton").Visible = false;//hide the text
                cmditm.FindControl("RebindGridButton").Visible = false;//hide the image
            }      
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem edititem = (GridEditableItem)e.Item;
                GridEditFormItem editform = (GridEditFormItem)e.Item;
                //dropdownAMC = (DropDownList)edititem.FindControl("ddlAMC");
                //dropdownCategory = (DropDownList)edititem.FindControl("ddlCategory");
                //dropdownSubCategory = (DropDownList)edititem.FindControl("ddlSubCategory");
                //dropdownScheme = (DropDownList)edititem.FindControl("ddlScheme");
                //trSubCategory = (HtmlTableRow)editform.FindControl("divSubCategory");
                //dropdownArchive = (DropDownList)edititem.FindControl("ddlArchive");

                TextBox weightage = (TextBox)e.Item.FindControl("txtWeightage");
                TextBox description = (TextBox)e.Item.FindControl("txtSchemeDescription");

                DropDownList ddl = (DropDownList)edititem.FindControl("ddlAMC"); //the field should point to the ListValueField of the dropdown editor
                DropDownList dropdownArchive = (DropDownList)edititem.FindControl("ddlArchive");

                HtmlTableRow trDdlPickAMC = (HtmlTableRow)editform.FindControl("trDdlPickAMC");
                HtmlTableRow trPickAMCtxt = (HtmlTableRow)editform.FindControl("trPickAMCtxt");

                HtmlTableRow trddlCategory = (HtmlTableRow)editform.FindControl("trddlCategory");
                HtmlTableRow trTxtCategory = (HtmlTableRow)editform.FindControl("trTxtCategory");

                HtmlTableRow divSubCategory = (HtmlTableRow)editform.FindControl("divSubCategory");
                HtmlTableRow trTxtSubCategory = (HtmlTableRow)editform.FindControl("trTxtSubCategory");

                HtmlTableRow trddlScheme = (HtmlTableRow)editform.FindControl("trddlScheme");
                HtmlTableRow trTxtScheme = (HtmlTableRow)editform.FindControl("trTxtScheme");

                HtmlTableRow trArchive = (HtmlTableRow)editform.FindControl("trArchive");

                if (e.Item.RowIndex == -1)
                {
                    trDdlPickAMC.Visible = true;
                    trddlCategory.Visible = true;
                    //divSubCategory.Visible = true;
                    //trSubCategory.Visible = true;
                    trddlScheme.Visible = true;
                    trPickAMCtxt.Visible = false;
                    trTxtCategory.Visible = false;
                    trTxtSubCategory.Visible = false;
                    trTxtScheme.Visible = false;
                    
                    trArchive.Visible = false;

                    dtGetAMCList = modelPortfolioBo.GetAMCList();
                    if (dtGetAMCList.Rows.Count != 0)
                    {
                        dropdownAMC.DataSource = dtGetAMCList;
                        dropdownAMC.DataTextField = dtGetAMCList.Columns["PA_AMCName"].ToString();
                        dropdownAMC.DataValueField = dtGetAMCList.Columns["PA_AMCCode"].ToString();
                        dropdownAMC.DataBind();
                        dropdownAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select AMC", "Select AMC Code"));
                        if (dropdownAMC.SelectedValue == "")
                        {
                            dropdownAMC.SelectedValue = ddl.SelectedValue;
                        }
                    }
                }
                else
                {
                    trDdlPickAMC.Visible = false;
                    trddlCategory.Visible = false;
                    //divSubCategory.Visible = false;
                    //trSubCategory.Visible = false;
                    trddlScheme.Visible = false;
                    trPickAMCtxt.Visible = true;
                    trTxtCategory.Visible = true;
                    trTxtSubCategory.Visible = true;
                    trTxtScheme.Visible = true;

                    weightage.Enabled = false;                    
                    trArchive.Visible = true;
                    description.Enabled = false;

                    DataTable dtGetArchiveReason = new DataTable();
                    dtGetArchiveReason = modelPortfolioBo.GetArchiveReason();
                    if (dtGetArchiveReason.Rows.Count != 0)
                    {
                        dropdownArchive.DataSource = dtGetArchiveReason;
                        dropdownArchive.DataTextField = dtGetArchiveReason.Columns["XAR_ArchiveReason"].ToString();
                        dropdownArchive.DataValueField = dtGetArchiveReason.Columns["XAR_Id"].ToString();
                        dropdownArchive.DataBind();
                        dropdownArchive.Items.Insert(0, new ListItem("Select Reason", "0"));
                    }
                }
            }
        }

        /****************************************************************************************************************************** */


        public void BindSelectedMPDropDown()
        {
            ModelPortfolioBo ModelPortfolioBo = new ModelPortfolioBo();
            DataTable dtModelPortfolioName = new DataTable();
            dtModelPortfolioName = ModelPortfolioBo.GetModelPortfolioName(advisorVo.advisorId);
            if (dtModelPortfolioName.Rows.Count > 0)
            {
                ddlSelectedMP.DataSource = dtModelPortfolioName;
                ddlSelectedMP.DataTextField = dtModelPortfolioName.Columns["XAMP_ModelPortfolioName"].ToString();
                ddlSelectedMP.DataValueField = dtModelPortfolioName.Columns["XAMP_ModelPortfolioCode"].ToString();
                ddlSelectedMP.DataBind();
                ddlSelectedMP.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ErrorMessage.Visible = true;
            }
        }

        public void LoadAllSchemeNAV()
        {

            if (dropdownAMC.SelectedIndex != 0)
            {
                ModelPortfolioBo ModelPortfolioBo = new ModelPortfolioBo();
                DataSet dsLoadAllScheme;
                DataTable dtLoadAllScheme = new DataTable();
                if (dropdownAMC.SelectedIndex != 0 && dropdownCategory.SelectedIndex == 0)
                {
                    amcCode = int.Parse(dropdownAMC.SelectedValue.ToString());
                    categoryCode = dropdownCategory.SelectedValue;
                    subCategory = "All";
                    dsLoadAllScheme = ModelPortfolioBo.GetSchemeListSubCategory(amcCode, categoryCode, subCategory);
                    dtLoadAllScheme = dsLoadAllScheme.Tables[0];
                }
                if (dropdownAMC.SelectedIndex != 0 && dropdownCategory.SelectedIndex != 0 && dropdownSubCategory.SelectedIndex == 0)
                {
                    amcCode = int.Parse(dropdownAMC.SelectedValue.ToString());
                    categoryCode = dropdownCategory.SelectedValue;
                    subCategory = "All";
                    dsLoadAllScheme = ModelPortfolioBo.GetSchemeListSubCategory(amcCode, categoryCode, subCategory);
                    dtLoadAllScheme = dsLoadAllScheme.Tables[0];
                }
                if (dropdownAMC.SelectedIndex != 0 && dropdownCategory.SelectedIndex != 0 && dropdownSubCategory.SelectedIndex != 0)
                {
                    amcCode = int.Parse(dropdownAMC.SelectedValue.ToString());
                    categoryCode = dropdownCategory.SelectedValue;
                    subCategory = dropdownSubCategory.SelectedValue;
                    dsLoadAllScheme = ModelPortfolioBo.GetSchemeListSubCategory(amcCode, categoryCode, subCategory);
                    dtLoadAllScheme = dsLoadAllScheme.Tables[0];
                }
                if (dtLoadAllScheme.Rows.Count > 0)
                {
                    dropdownScheme.DataSource = dtLoadAllScheme;
                    dropdownScheme.DataTextField = dtLoadAllScheme.Columns["PASP_SchemePlanName"].ToString();
                    dropdownScheme.DataValueField = dtLoadAllScheme.Columns["PASP_SchemePlanCode"].ToString();
                    dropdownScheme.DataBind();
                    dropdownScheme.Items.Insert(0, new ListItem("Select", "Select"));
                }
                else
                {
                    dropdownScheme.Items.Clear();
                    dropdownScheme.DataSource = null;
                    dropdownScheme.DataBind();
                    dropdownScheme.Items.Insert(0, new ListItem("Select", "Select"));
                }
            }
        }

        protected void ddlSelectedMP_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Session.Remove(SessionContents.FPS_AddProspect_DataTable);
            bindRadGrid1();
            bindHistryRadGrid();
            //Session.Remove(SessionContents.FPS_AddProspect_DataTable);
            bindAssetChart();
            bindAssetChartOnSubCategory();
            tblPieChart.Visible = true;
            getAllocationPercentageFromModelPortFolio();
            if (ddlSelectedMP.SelectedValue != "0")
            {
                tblAllocation.Visible = true;
                tblPieChart.Visible = true;
            }
            else
            {
                tblAllocation.Visible = false;
                tblPieChart.Visible = false;
            }
        }

        protected void ddlAMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllSchemeNAV();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropdown = (DropDownList)sender;
            string categoryCode = dropdownCategory.SelectedValue;
            string AMCCode = dropdownAMC.SelectedValue;
            //string SubcategoryCode = dropdownSubCategory.SelectedValue;

            dsCategoryList = modelPortfolioBo.BindddlMFSubCategory();
            if (categoryCode == "All")
            {
                trSubCategory.Visible = false;
                hdnSubCategory.Value = "";
            }
            if (categoryCode == "MFCO")
            {
                trSubCategory.Visible = true;
                //trSubCategory.Visible = true;
                dropdownSubCategory.DataSource = dsCategoryList.Tables[0];
                dropdownSubCategory.DataTextField = dsCategoryList.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                dropdownSubCategory.DataValueField = dsCategoryList.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                dropdownSubCategory.DataBind();
                dropdownSubCategory.Items.Insert(0, new ListItem("All", "All"));
            }
            if (categoryCode == "MFDT")
            {
                trSubCategory.Visible = true;
                dropdownSubCategory.DataSource = dsCategoryList.Tables[1];
                dropdownSubCategory.DataTextField = dsCategoryList.Tables[1].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                dropdownSubCategory.DataValueField = dsCategoryList.Tables[1].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                dropdownSubCategory.DataBind();
                dropdownSubCategory.Items.Insert(0, new ListItem("All", "All"));
            }
            if (categoryCode == "MFEQ")
            {
                trSubCategory.Visible = true;
                dropdownSubCategory.DataSource = dsCategoryList.Tables[2];
                dropdownSubCategory.DataTextField = dsCategoryList.Tables[2].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                dropdownSubCategory.DataValueField = dsCategoryList.Tables[2].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                dropdownSubCategory.DataBind();
                dropdownSubCategory.Items.Insert(0, new ListItem("All", "All"));
            }
            if (categoryCode == "MFHY")
            {
                trSubCategory.Visible = true;
                dropdownSubCategory.DataSource = dsCategoryList.Tables[3];
                dropdownSubCategory.DataTextField = dsCategoryList.Tables[3].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                dropdownSubCategory.DataValueField = dsCategoryList.Tables[3].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                dropdownSubCategory.DataBind();
                dropdownSubCategory.Items.Insert(0, new ListItem("All", "All"));
            }
         LoadAllSchemeNAV();
        }

        protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllSchemeNAV();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            decimal sum = 0;
            dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
            
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["AMFMPD_IsActive"].ToString() == "1")
                {
                    sum += Convert.ToDecimal(dr["AMFMPD_AllocationPercentage"].ToString());
                }
            }
            if (sum == 100)
            {
                //modelPortfolioBo.DeleteSchemeFromModelPortfolio(Convert.ToInt32(ddlSelectedMP.SelectedValue), advisorVo.advisorId);

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Flag"].ToString() == "D")
                    {
                    }
                    if (dr["Flag"].ToString() == "UI")
                    {
                        modelPortfolioVo.Weightage = Convert.ToDecimal(dr["AMFMPD_AllocationPercentage"]);
                        modelPortfolioVo.SchemeDescription = dr["AMFMPD_SchemeDescription"].ToString();
                        modelPortfolioVo.ModelPortfolioCode = Convert.ToInt32(ddlSelectedMP.SelectedValue);
                        modelPortfolioVo.SchemeCode = Convert.ToInt32(dr["PASP_SchemePlanCode"]);
                        IsActiveFlag = 1;
                        //modelPortfolioVo.ArchiveReason = 0;
                        modelPortfolioBo.AttachSchemeToPortfolio(modelPortfolioVo, advisorVo.advisorId, IsActiveFlag);
                    }
                    else if (dr["AMFMPD_IsActive"].ToString() == "0")
                    {
                        Telerik.Web.UI.GridDataItem item = (GridDataItem)RadGrid1.MasterTableView.Items[0];
                        int modelId = Convert.ToInt32(RadGrid1.MasterTableView.DataKeyValues[item.ItemIndex]["AMFMPD_Id"].ToString());
                        int xarId = int.Parse(dr["XAR_ArchiveReason"].ToString());
                            //Convert.ToInt32(dropdownArchive.SelectedIndex);
                            //modelPortfolioVo.ArchiveReason;
                        modelPortfolioBo.ArchiveSchemeFromModelPortfolio(modelId, xarId);
                    }
                }
                bindAssetChart();
                bindAssetChartOnSubCategory();
                bindRadGrid1();
                bindHistryRadGrid();
                tblPieChart.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Weightage must be 100');", true);
                tblPieChart.Visible = false;
            }           
            RadGrid1.Rebind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            bindRadGrid1();
            bindHistryRadGrid();
            //Session.Remove(SessionContents.FPS_AddProspect_DataTable);
            bindAssetChart();
            bindAssetChartOnSubCategory();
            tblPieChart.Visible = true;
            getAllocationPercentageFromModelPortFolio();
            if (ddlSelectedMP.SelectedValue != "0")
                tblAllocation.Visible = true;
            else
                tblAllocation.Visible = false;
        }

        protected void bindAssetChart()
        {
            Series AssetAllocation = new Series("Asset");
            try
            {
                ModelPortfolioBo modelPortfolioBo = new ModelPortfolioBo();
                modelPortfolioVo.ModelPortfolioCode = Convert.ToInt32(ddlSelectedMP.SelectedValue);
                DataTable dtChartAsset = new DataTable();
                DataRow[] drAllocation;
                //dtChartAsset = modelPortfolioBo.GetSchemeAssetAllocation(modelPortfolioVo, advisorVo.advisorId);
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                string schemePlanCode = "";
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (int.Parse(dr["AMFMPD_IsActive"].ToString()) == 1)
                        {
                            schemePlanCode = schemePlanCode + "~" + dr["PASP_SchemePlanCode"].ToString();
                        }

                    }
                    DataTable dtSchemeCategory = new DataTable();
                    dtSchemeCategory = modelPortfolioBo.GetSchemeAssetAllocation(schemePlanCode);
                    double allocationPercentage = 0;
                    double coAllocation = 0;
                    double hyAllocation = 0;
                    double percentageAllocation = 0;
                    string assetClassificationCode;
                    string category;
                    double totalAllocation;
                    foreach (DataRow dr in dtSchemeCategory.Rows)
                    {

                        int schemeId = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        percentageAllocation = double.Parse(dr["WACPISSCA_PercentageAllocation"].ToString());
                        assetClassificationCode = dr["WAC_AssetClassificationCode"].ToString();
                        category = dr["Category"].ToString();
                        drAllocation = dt.Select("PASP_SchemePlanCode='" + schemeId.ToString()+"'");
                        foreach (DataRow drAll in drAllocation)
                        {
                            allocationPercentage = double.Parse(drAll["AMFMPD_AllocationPercentage"].ToString());

                            if (category == "Debt")
                            {
                                dtAllocation = dtAllocation + allocationPercentage;
                            }

                            else if (category == "Equity")
                            {
                                eqAllocation = eqAllocation + allocationPercentage;
                            }

                            else if (category == "Hybrid")
                            {
                                if (assetClassificationCode == "Equity")
                                {
                                    eqAllocation = eqAllocation + (allocationPercentage * percentageAllocation) / 100;
                                }
                                if (assetClassificationCode == "Debt")
                                {
                                    dtAllocation = dtAllocation + (allocationPercentage * percentageAllocation) / 100;
                                }
                            }

                            if (category == "Commodity")
                            {
                                if (assetClassificationCode == "Equity")
                                {
                                    eqAllocation = eqAllocation + (allocationPercentage * percentageAllocation) / 100;
                                }
                                if (assetClassificationCode == "Debt")
                                {
                                    dtAllocation = dtAllocation + (allocationPercentage * percentageAllocation) / 100;
                                }
                            }


                            //drAllocation = dt.Select("PASP_SchemePlanCode=" + schemeId.ToString());
                            //foreach (DataRow drall in drAllocation)
                            //{
                            //    allocationPercentage = double.Parse(drall["AMFMPD_AllocationPercentage"].ToString());
                            //}

                            //if (dr["PAIC_AssetInstrumentCategoryCode"].ToString() == "MFDT")
                            //{
                            //    dtAllocation = dtAllocation + allocationPercentage;
                            //}
                            //if (dr["PAIC_AssetInstrumentCategoryCode"].ToString() == "MFEQ")
                            //{
                            //    eqAllocation = eqAllocation + allocationPercentage;
                            //}
                            //if (dr["PAIC_AssetInstrumentCategoryCode"].ToString() == "MFHY")
                            //{
                            //    hyAllocation = hyAllocation + allocationPercentage;
                            //}
                            //if (dr["PAIC_AssetInstrumentCategoryCode"].ToString() == "MFCO")
                            //{
                            //    coAllocation = coAllocation + allocationPercentage;
                            //}
                        }
                    }
                    totalAllocation = dtAllocation + eqAllocation;

                    dtAllocation = (dtAllocation / totalAllocation) * 100;
                    eqAllocation = (eqAllocation / totalAllocation) * 100;
                    dtChartAsset.Columns.Add("PAIC_AssetInstrumentCategoryName");
                    dtChartAsset.Columns.Add("AMFMPD_AllocationPercentage");
                    DataRow drRiskClass;
                    drRiskClass = dtChartAsset.NewRow();
                    drRiskClass["PAIC_AssetInstrumentCategoryName"] = "Equity";
                    drRiskClass["AMFMPD_AllocationPercentage"] = eqAllocation;
                    dtChartAsset.Rows.Add(drRiskClass);
                    drRiskClass = dtChartAsset.NewRow();
                    drRiskClass["PAIC_AssetInstrumentCategoryName"] = "Debt";
                    drRiskClass["AMFMPD_AllocationPercentage"] = dtAllocation;
                    dtChartAsset.Rows.Add(drRiskClass);

                }




        
                Legend legend = new Legend("ChartAssetAllocationLegend");
                legend.Enabled = true;

                if (dtChartAsset.Rows.Count > 0)
                {
                    // LoadChart 
                    AssetAllocation.ChartType = SeriesChartType.Pie;
                    ChartAsset.DataSource = dtChartAsset.DefaultView;
                    ChartAsset.Series.Clear();
                    ChartAsset.Series.Add(AssetAllocation);
                    ChartAsset.Series[0].XValueMember = "PAIC_AssetInstrumentCategoryName";
                    ChartAsset.Series[0].XValueType = ChartValueType.String;
                    ChartAsset.Series[0].YValueMembers = "AMFMPD_AllocationPercentage";

                    ChartAsset.Palette = ChartColorPalette.Pastel;
                    ChartAsset.PaletteCustomColors = new Color[]{Color.LimeGreen, Color.Yellow, Color.LightBlue, Color.Purple, Color.Goldenrod, Color.Blue, Color.BurlyWood,
                                                                          Color.Chocolate, Color.DeepPink, Color.Plum, Color.Violet, Color.Gainsboro, Color.Tomato, Color.Teal, Color.BlanchedAlmond, Color.Cornsilk};

                    ChartAsset.Legends.Add(legend);
                    //ChartAsset.Legends["ChartAssetAllocationLegend"].Title = "Asset Allocation";
                    //ChartAsset.Legends["ChartAssetAllocationLegend"].TitleAlignment = StringAlignment.Center;
                    //ChartAsset.Legends["ChartAssetAllocationLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
                    //ChartAsset.Legends["ChartAssetAllocationLegend"].BackColor = Color.FloralWhite;
                    //ChartAsset.Legends["ChartAssetAllocationLegend"].TitleSeparatorColor = Color.Black;

                    ChartArea chartArea1 = ChartAsset.ChartAreas[0];

                    LegendCellColumn colorColumn = new LegendCellColumn();
                    colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                    colorColumn.HeaderText = "Color";
                    colorColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartAsset.Legends["ChartAssetAllocationLegend"].CellColumns.Add(colorColumn);
                    chartArea1.BackColor = System.Drawing.Color.Transparent;
                    chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;

                    LegendCellColumn TypeColumn = new LegendCellColumn();
                    TypeColumn.Alignment = ContentAlignment.TopLeft;
                    TypeColumn.Text = "#VALX";
                    TypeColumn.HeaderText = "Type";
                    TypeColumn.Name = "TypeColumn";
                    TypeColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartAsset.Legends["ChartAssetAllocationLegend"].CellColumns.Add(TypeColumn);

                    LegendCellColumn percentColumn = new LegendCellColumn();
                    percentColumn.Alignment = ContentAlignment.MiddleLeft;
                    percentColumn.HeaderText = "%";
                    percentColumn.Text = "#PERCENT";
                    percentColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartAsset.Legends["ChartAssetAllocationLegend"].CellColumns.Add(percentColumn);

                    ChartAsset.Series[0]["PieLabelStyle"] = "Disabled";
                    ChartAsset.ChartAreas[0].Area3DStyle.Enable3D = true;
                    ChartAsset.ChartAreas[0].Area3DStyle.Perspective = 50;
                    ChartAsset.Series[0].ToolTip = "#VALX: #PERCENT";
                    ChartAsset.DataBind();

                    DataRow drAsset;
                    DataTable dtAssetForGrid = new DataTable();
                    dtAssetForGrid.Columns.Add("PAIC_AssetInstrumentCategoryName");
                    dtAssetForGrid.Columns.Add("AMFMPD_AllocationPercentage", System.Type.GetType("System.Decimal"));

                    dtAssetForGrid.Columns.Add("Percent", System.Type.GetType("System.Decimal"));
                    double WeightageSum = 0;
                    foreach (DataRow dr in dtChartAsset.Rows)
                    {
                        WeightageSum = WeightageSum + double.Parse(dr["AMFMPD_AllocationPercentage"].ToString());
                    }

                    foreach (DataRow dr in dtChartAsset.Rows)
                    {
                        drAsset = dtAssetForGrid.NewRow();
                        drAsset["PAIC_AssetInstrumentCategoryName"] = dr["PAIC_AssetInstrumentCategoryName"];
                        drAsset["AMFMPD_AllocationPercentage"] = dr["AMFMPD_AllocationPercentage"];
                        drAsset["Percent"] = Math.Round(((double.Parse(dr["AMFMPD_AllocationPercentage"].ToString()) / WeightageSum) * 100), 2);

                        dtAssetForGrid.Rows.Add(drAsset);
                    }

                    ChartAsset.DataSource = dtAssetForGrid;
                    ChartAsset.DataBind();
                    ChartAsset.Visible = true;
                    lblCategoryChart.Visible = true;
                    lblSubCategoryChart.Visible = true;
                }
                else
                {
                    ChartAsset.DataSource = null;
                    ChartAsset.Visible = false;
                    lblCategoryChart.Visible = false;
                    lblSubCategoryChart.Visible = false;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        protected void bindAssetChartOnSubCategory()
        {
            Series AssetAllocationSubCategory = new Series("Asset");
            try
            {
                ModelPortfolioBo modelPortfolioBo = new ModelPortfolioBo();
                modelPortfolioVo.ModelPortfolioCode = Convert.ToInt32(ddlSelectedMP.SelectedValue);
                DataTable dtChartAsset = new DataTable();
                dtChartAsset = modelPortfolioBo.SchemeAssetChartOnSubCategory(modelPortfolioVo, advisorVo.advisorId);

                Legend legend = new Legend("ChartAssetAllocationLegend");
                legend.Enabled = true;

                if (dtChartAsset.Rows.Count > 0)
                {
                    // LoadChart 
                    AssetAllocationSubCategory.ChartType = SeriesChartType.Pie;
                    Chart1.DataSource = dtChartAsset.DefaultView;
                    Chart1.Series.Clear();
                    Chart1.Series.Add(AssetAllocationSubCategory);
                    Chart1.Series[0].XValueMember = "PAISC_AssetInstrumentSubCategoryName";
                    Chart1.Series[0].XValueType = ChartValueType.String;
                    Chart1.Series[0].YValueMembers = "AMFMPD_AllocationPercentage";

                    Chart1.Palette = ChartColorPalette.Pastel;
                    Chart1.PaletteCustomColors = new Color[]{Color.LimeGreen, Color.Yellow, Color.LightBlue, Color.Purple, Color.Goldenrod, Color.Blue, Color.BurlyWood,
                                                                          Color.Chocolate, Color.DeepPink, Color.Plum, Color.Violet, Color.Gainsboro, Color.Tomato, Color.Teal, Color.BlanchedAlmond, Color.Cornsilk};

                    Chart1.Legends.Add(legend);
                    //Chart1.Legends["ChartAssetAllocationLegend"].Title = "Asset Allocation";
                    //Chart1.Legends["ChartAssetAllocationLegend"].TitleAlignment = StringAlignment.Center;
                    //Chart1.Legends["ChartAssetAllocationLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
                    //Chart1.Legends["ChartAssetAllocationLegend"].BackColor = Color.FloralWhite;
                    //Chart1.Legends["ChartAssetAllocationLegend"].TitleSeparatorColor = Color.Black;

                    ChartArea chartArea1 = Chart1.ChartAreas[0];

                    LegendCellColumn colorColumn = new LegendCellColumn();
                    colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                    colorColumn.HeaderText = "Color";
                    colorColumn.HeaderBackColor = Color.WhiteSmoke;
                    Chart1.Legends["ChartAssetAllocationLegend"].CellColumns.Add(colorColumn);
                    chartArea1.BackColor = System.Drawing.Color.Transparent;
                    chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;

                    LegendCellColumn TypeColumn = new LegendCellColumn();
                    TypeColumn.Alignment = ContentAlignment.TopLeft;
                    TypeColumn.Text = "#VALX";
                    TypeColumn.HeaderText = "Type";
                    TypeColumn.Name = "TypeColumn";
                    TypeColumn.HeaderBackColor = Color.WhiteSmoke;
                    Chart1.Legends["ChartAssetAllocationLegend"].CellColumns.Add(TypeColumn);

                    LegendCellColumn percentColumn = new LegendCellColumn();
                    percentColumn.Alignment = ContentAlignment.MiddleLeft;
                    percentColumn.HeaderText = "%";
                    percentColumn.Text = "#PERCENT";
                    percentColumn.HeaderBackColor = Color.WhiteSmoke;
                    Chart1.Legends["ChartAssetAllocationLegend"].CellColumns.Add(percentColumn);

                    Chart1.Series[0]["PieLabelStyle"] = "Disabled";
                    Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                    Chart1.ChartAreas[0].Area3DStyle.Perspective = 50;
                    Chart1.Series[0].ToolTip = "#VALX: #PERCENT";
                    Chart1.DataBind();

                    DataRow drAsset;
                    DataTable dtAssetForGrid = new DataTable();
                    dtAssetForGrid.Columns.Add("PAISC_AssetInstrumentSubCategoryName");
                    dtAssetForGrid.Columns.Add("AMFMPD_AllocationPercentage", System.Type.GetType("System.Decimal"));

                    dtAssetForGrid.Columns.Add("Percent", System.Type.GetType("System.Decimal"));
                    double WeightageSum = 0;
                    foreach (DataRow dr in dtChartAsset.Rows)
                    {
                        WeightageSum = WeightageSum + double.Parse(dr["AMFMPD_AllocationPercentage"].ToString());
                    }

                    foreach (DataRow dr in dtChartAsset.Rows)
                    {
                        drAsset = dtAssetForGrid.NewRow();
                        drAsset["PAISC_AssetInstrumentSubCategoryName"] = dr["PAISC_AssetInstrumentSubCategoryName"];
                        drAsset["AMFMPD_AllocationPercentage"] = dr["AMFMPD_AllocationPercentage"];
                        drAsset["Percent"] = Math.Round(((double.Parse(dr["AMFMPD_AllocationPercentage"].ToString()) / WeightageSum) * 100), 2);
                        dtAssetForGrid.Rows.Add(drAsset);
                    }

                    Chart1.DataSource = dtAssetForGrid;
                    Chart1.DataBind();
                    Chart1.Visible = true;
                    lblCategoryChart.Visible = true;
                    lblSubCategoryChart.Visible = true;
                }
                else
                {
                    Chart1.DataSource = null;
                    Chart1.Visible = false;
                    lblCategoryChart.Visible = false;
                    lblSubCategoryChart.Visible = false;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        //protected void btnArchive_Click(object sender, EventArgs e)
        //{
        //    Telerik.Web.UI.GridDataItem item = (GridDataItem)RadGrid1.MasterTableView.Items[0];

        //    foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
        //    {
        //        CheckBox chk = (dataItem.FindControl("chk") as CheckBox);
        //        if (chk.Checked)
        //        {
        //            int modelId = Convert.ToInt32(RadGrid1.MasterTableView.DataKeyValues[dataItem.ItemIndex]["AMFMPD_Id"].ToString());
        //            ModalPopupExtender1.Show();
        //        }
        //        else
        //        {


        //        }               
        //    }
        //}

        //public void BindArchiveReasonDropDown()
        //{
        //    ModelPortfolioBo ModelPortfolioBo = new ModelPortfolioBo();
        //    DataTable dtGetArchiveReason = new DataTable();
        //    dtGetArchiveReason = ModelPortfolioBo.GetArchiveReason();
        //    ddlArchive.DataSource = dtGetArchiveReason;
        //    ddlArchive.DataTextField = dtGetArchiveReason.Columns["XAR_ArchiveReason"].ToString();
        //    ddlArchive.DataValueField = dtGetArchiveReason.Columns["XAR_Id"].ToString();
        //    ddlArchive.DataBind();
        //    ddlArchive.Items.Insert(0, new ListItem("Select Reason", "0"));
        //}

        //protected void ddlArchive_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string archiveReason = ddlArchive.SelectedValue;
        //}

        public void bindHistryRadGrid()
        {
            DataTable dtRiskClass = new DataTable();
            DataTable dtClass = new DataTable();
            if (ddlSelectedMP.SelectedValue != "0" && ddlSelectedMP.SelectedValue != "")
            {
                //modelPortfolioVo.ModelPortfolioCode = Convert.ToInt32(ddlSelectedMP.SelectedValue);
                dtRiskClass = modelPortfolioBo.GetArchivedSchemeDetails(Convert.ToInt32(ddlSelectedMP.SelectedValue), advisorVo.advisorId);

                if (dtRiskClass.Rows.Count > 0)
                {
                    dtClass.Columns.Add("PASP_SchemePlanName");
                    dtClass.Columns.Add("AMFMPD_AddedOn");
                    dtClass.Columns.Add("AMFMPD_RemovedOn");
                    dtClass.Columns.Add("AMFMPD_AllocationPercentage");
                    dtClass.Columns.Add("AMFMPD_SchemeDescription");
                    dtClass.Columns.Add("XAR_ArchiveReason");

                    DataRow drRiskClass;
                    foreach (DataRow dr in dtRiskClass.Rows)
                    {
                        drRiskClass = dtClass.NewRow();
                        drRiskClass["PASP_SchemePlanName"] = dr["PASP_SchemePlanName"].ToString();
                        drRiskClass["AMFMPD_AllocationPercentage"] = dr["AMFMPD_AllocationPercentage"].ToString();
                        drRiskClass["AMFMPD_AddedOn"] = DateTime.Parse(dr["AMFMPD_AddedOn"].ToString()).ToShortDateString();
                        drRiskClass["AMFMPD_RemovedOn"] = DateTime.Parse(dr["AMFMPD_RemovedOn"].ToString()).ToShortDateString();
                        drRiskClass["AMFMPD_SchemeDescription"] = dr["AMFMPD_SchemeDescription"].ToString();
                        drRiskClass["XAR_ArchiveReason"] = dr["XAR_ArchiveReason"].ToString();
                        dtClass.Rows.Add(drRiskClass);
                    }                    
                    histryRadGrid.DataSource = dtClass;
                    histryRadGrid.DataSourceID = String.Empty;
                    histryRadGrid.DataBind();
                }
                else
                {
                    tableGrid.Visible = true;
                    tableNote.Visible = true;
                    tblSelectddl.Visible = true;
                }              
            }
            else
            {
                tableGrid.Visible = false;
                tableNote.Visible = false;
                tblSelectddl.Visible = true;
            }
        }
    }
}

        