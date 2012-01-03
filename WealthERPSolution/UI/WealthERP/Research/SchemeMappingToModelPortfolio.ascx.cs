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

                dt = new DataTable();
                dt.Columns.Add("PASP_SchemePlanName");
                dt.Columns.Add("PASP_SchemePlanCode");
                dt.Columns.Add("AMFMPD_AllocationPercentage");
                dt.Columns.Add("AMFMPD_AddedOn");
                dt.Columns.Add("AMFMPD_SchemeDescription");
                dt.Columns.Add("AMFMPD_Id");
                dt.Columns.Add("AMFMPD_RemovedOn");
                dt.Columns.Add("XAR_ArchiveReason");

                Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                RadGrid1.DataSource = dt;
                bindRadGrid1();
            } 
            
        }
     
        public void bindRadGrid1()
        {
            DataTable dtRiskClass = new DataTable();          
            DataTable dtClass = new DataTable();
            if (ddlSelectedMP.SelectedValue != "0")
                modelPortfolioVo.ModelPortfolioCode = Convert.ToInt32(ddlSelectedMP.SelectedValue);
            
            dtRiskClass = modelPortfolioBo.GetAttachedSchemeDetails(modelPortfolioVo, advisorVo.advisorId);

            dtClass.Columns.Add("PASP_SchemePlanName");
            dtClass.Columns.Add("PASP_SchemePlanCode");
            dtClass.Columns.Add("AMFMPD_AllocationPercentage");
            dtClass.Columns.Add("AMFMPD_AddedOn");
            dtClass.Columns.Add("AMFMPD_SchemeDescription");
            dtClass.Columns.Add("AMFMPD_Id");
            dtClass.Columns.Add("AMFMPD_RemovedOn");
            dtClass.Columns.Add("XAR_ArchiveReason");

            DataRow drRiskClass;
            foreach (DataRow dr in dtRiskClass.Rows)
            {
                drRiskClass = dtClass.NewRow();
                drRiskClass["PASP_SchemePlanName"] = dr["PASP_SchemePlanName"].ToString();
                drRiskClass["PASP_SchemePlanCode"] = dr["PASP_SchemePlanCode"].ToString();
                drRiskClass["AMFMPD_AllocationPercentage"] = dr["AMFMPD_AllocationPercentage"].ToString();
                drRiskClass["AMFMPD_AddedOn"] = dr["AMFMPD_AddedOn"].ToString();
                drRiskClass["AMFMPD_Id"] = dr["AMFMPD_Id"].ToString();
                drRiskClass["AMFMPD_SchemeDescription"] = dr["AMFMPD_SchemeDescription"].ToString();
                dtClass.Rows.Add(drRiskClass);
            }
            if (dtRiskClass.Rows.Count > 0)
            {
                Session[SessionContents.FPS_AddProspect_DataTable] = dtClass;

                RadGrid1.DataSource = dtClass;

                RadGrid1.DataSourceID = String.Empty;
                RadGrid1.DataBind();
            }
            else
            {
                RadGrid1.DataSource = dtClass;
                RadGrid1.DataBind();
            }
        }
        
        protected void RadGrid1_DataBound(object sender, EventArgs e)
        {
            
        }

        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {  
                TextBox weightage = (TextBox)e.Item.FindControl("txtWeightage");
                TextBox description = (TextBox)e.Item.FindControl("txtSchemeDescription");
                modelPortfolioVo.Weightage = Convert.ToInt32(weightage.Text);
                modelPortfolioVo.SchemeDescription = description.Text;
                modelPortfolioVo.ModelPortfolioCode = Convert.ToInt32(ddlSelectedMP.SelectedValue);
                modelPortfolioVo.SchemeCode = Convert.ToInt32(dropdownScheme.SelectedValue);
                
                modelPortfolioVo.ArchiveReason = 0;
                               
                dt=(DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                
                dr = dt.NewRow();
                dr["PASP_SchemePlanName"] = dropdownScheme.SelectedItem;
                dr["PASP_SchemePlanCode"] = dropdownScheme.SelectedValue;
                dr["AMFMPD_AllocationPercentage"] = weightage.Text;
                dr["AMFMPD_SchemeDescription"] = description.Text;
                dr["AMFMPD_AddedOn"] = DateTime.Now.ToString();
                
                dt.Rows.Add(dr);
                
                //Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                RadGrid1.DataSource = dt;               
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                RadGrid1.Controls.Add(new LiteralControl("Unable to insert Scheme. Reason: " + ex.Message));
                e.Canceled = true;
            } 
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
                //GridEditableItem editedItem = e.Item as GridEditableItem;
                //GridEditManager editMan = editedItem.EditManager;
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                dt.Rows[e.Item.ItemIndex].Delete();
                Rebind();
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
            try
            {
                TextBox weightage = (TextBox)e.Item.FindControl("txtWeightage");
                TextBox description = (TextBox)e.Item.FindControl("txtSchemeDescription");
                modelPortfolioVo.Weightage = Convert.ToInt32(weightage.Text);
                modelPortfolioVo.SchemeDescription = description.Text;
                modelPortfolioVo.ModelPortfolioCode = Convert.ToInt32(ddlSelectedMP.SelectedValue);
                modelPortfolioVo.SchemeCode = Convert.ToInt32(dropdownScheme.SelectedValue);                
                modelPortfolioVo.ArchiveReason = 0;
          
                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                dr = dt.NewRow();
                dr["PASP_SchemePlanName"] = dropdownScheme.SelectedItem;
                dr["PASP_SchemePlanCode"] = dropdownScheme.SelectedValue;
                dr["AMFMPD_AllocationPercentage"] = weightage.Text;
                dr["AMFMPD_SchemeDescription"] = description.Text;
                dr["AMFMPD_AddedOn"] = DateTime.Now.ToString();

                dt.Rows.Add(dr);
                //Session[SessionContents.FPS_AddProspect_DataTable] = dt;
                RadGrid1.DataSource = dt;
                RadGrid1.Rebind();
            }
            catch (Exception ex)
            {
                RadGrid1.Controls.Add(new LiteralControl("Unable to insert Scheme. Reason: " + ex.Message));
                e.Canceled = true;
            } 
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
                RadGrid1.DataSource = dt;
                RadGrid1.Rebind();
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
            else
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
                if (!editColumn.Visible)
                    editColumn.Visible = true;

                dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                RadGrid1.DataSource = dt;
                RadGrid1.Rebind();
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
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            DataTable dt = new DataTable();            
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                //GridEditableItem edititem = (GridEditableItem)e.Item;
                //GridEditFormItem editform = (GridEditFormItem)e.Item;
                ////dropdownAMC = (DropDownList)edititem.FindControl("ddlAMC");
                //dropdownCategory = (DropDownList)edititem.FindControl("ddlCategory");
                //dropdownSubCategory = (DropDownList)edititem.FindControl("ddlSubCategory");
                //dropdownScheme = (DropDownList)edititem.FindControl("ddlScheme");
                //trSubCategory = (HtmlTableRow)editform.FindControl("divSubCategory");
                //dropdownArchive = (DropDownList)edititem.FindControl("ddlArchive");

                if (dtGetAMCList.Rows.Count == 0)
                {
                    dtGetAMCList = modelPortfolioBo.GetAMCList();
                    if (dtGetAMCList.Rows.Count != 0)
                    {
                        dropdownAMC.DataSource = dtGetAMCList;
                        dropdownAMC.DataTextField = dtGetAMCList.Columns["PA_AMCName"].ToString();
                        dropdownAMC.DataValueField = dtGetAMCList.Columns["PA_AMCCode"].ToString();
                        dropdownAMC.DataBind();
                    }
                    dropdownAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select AMC", "Select AMC Code"));
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
                    subCategory = dropdownSubCategory.SelectedValue;
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
                    dropdownScheme.Items.Insert(0, new ListItem("All Scheme", "0"));
                }
            }
            else
            {
            }
        }

        protected void ddlSelectedMP_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindRadGrid1();
        }

        protected void ddlAMC_SelectedIndexChanged(object sender, EventArgs e)
        {            
            //dtGetAMCList = modelPortfolioBo.GetAMCList();
            //if (dtGetAMCList.Rows.Count != 0)
            //{
            //    dropdownAMC.DataSource = dtGetAMCList;
            //    dropdownAMC.DataTextField = dtGetAMCList.Columns["PA_AMCName"].ToString();
            //    dropdownAMC.DataValueField = dtGetAMCList.Columns["PA_AMCCode"].ToString();
            //    dropdownAMC.DataBind();
            //}
            //dropdownAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select AMC", "Select AMC Code"));
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
                //dropdownSubCategory.Items.Insert(0, new ListItem("Select", "All"));   
            }
            if (categoryCode == "MFDT")
            {
                trSubCategory.Visible = true;
                dropdownSubCategory.DataSource = dsCategoryList.Tables[1];
                dropdownSubCategory.DataTextField = dsCategoryList.Tables[1].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                dropdownSubCategory.DataValueField = dsCategoryList.Tables[1].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                dropdownSubCategory.DataBind();
                ////dropdownSubCategory.Items.Insert(0, new ListItem("Select", "All"));
            }
            if (categoryCode == "MFEQ")
            {
                trSubCategory.Visible = true;
                dropdownSubCategory.DataSource = dsCategoryList.Tables[2];
                dropdownSubCategory.DataTextField = dsCategoryList.Tables[2].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                dropdownSubCategory.DataValueField = dsCategoryList.Tables[2].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                dropdownSubCategory.DataBind();
                ////dropdownSubCategory.Items.Insert(0, new ListItem("Select", "All"));
            }
            if (categoryCode == "MFHY")
            {
                trSubCategory.Visible = true;
                dropdownSubCategory.DataSource = dsCategoryList.Tables[3];
                dropdownSubCategory.DataTextField = dsCategoryList.Tables[3].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                dropdownSubCategory.DataValueField = dsCategoryList.Tables[3].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                dropdownSubCategory.DataBind();
                ////dropdownSubCategory.Items.Insert(0, new ListItem("Select", "All"));
            }
            LoadAllSchemeNAV();
        }

        protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadAllSchemeNAV();
            DropDownList dropdown1 = (DropDownList)sender;
            string a = dropdown1.SelectedValue;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            decimal sum = 0;
            //DataTable dtDetails;
            //DataRow drDetails;
            dt = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
            foreach (DataRow dr in dt.Rows)
            {
                sum += Convert.ToDecimal(dr["AMFMPD_AllocationPercentage"].ToString());
            }
            if (sum == 100)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    modelPortfolioVo.Weightage = Convert.ToDecimal(dr["AMFMPD_AllocationPercentage"]);
                    modelPortfolioVo.SchemeDescription = dr["AMFMPD_SchemeDescription"].ToString();
                    modelPortfolioVo.ModelPortfolioCode = Convert.ToInt32(ddlSelectedMP.SelectedValue);
                    modelPortfolioVo.SchemeCode = Convert.ToInt32(dr["PASP_SchemePlanCode"]);
                    IsActiveFlag = 1;
                    modelPortfolioVo.ArchiveReason = 0;
                    modelPortfolioBo.AttachSchemeToPortfolio(modelPortfolioVo, advisorVo.advisorId, IsActiveFlag);
                }
            }
            else
            {
               ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You dont have enough amount');", true);
            }

        }
    }
}      

        