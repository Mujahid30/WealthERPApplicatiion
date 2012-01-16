using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using WealthERP.Base;
using VoUser;
using BoCustomerRiskProfiling;
using Telerik.Web.UI;
using BoResearch;
using VoResearch;
using System.Web.UI.HtmlControls;

namespace WealthERP.Research
{
    public partial class ModelPortfolioSetup : System.Web.UI.UserControl
    {        
        AdvisorVo advisorVo = new AdvisorVo();
        ModelPortfolioBo modelPortfolioBo = new ModelPortfolioBo();
        ModelPortfolioVo modelPortfolioVo = new ModelPortfolioVo();
        AdviserFPConfigurationBo adviserFPConfigurationBo = new AdviserFPConfigurationBo();
        RiskProfileBo riskprofilebo = new RiskProfileBo();        

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            bindRadGrid1();            
        }        

        public void bindRadGrid1()
        {
            DataTable dtVariant = new DataTable();
            DataSet dsVariantDetails = new DataSet();
            DataTable dtModel = new DataTable();
            ModelPortfolioBo modelPortfolioBo = new ModelPortfolioBo();
            DataRow[] drArrayAllocation;

            dsVariantDetails = modelPortfolioBo.GetVariantAssetPortfolioDetails(advisorVo.advisorId);
            
            dtVariant.Columns.Add("XAMP_ModelPortfolioCode");
            dtVariant.Columns.Add("XAMP_ModelPortfolioName");
            dtVariant.Columns.Add("XRC_RiskClass");
            dtVariant.Columns.Add("XRC_RiskClassCode");
            dtVariant.Columns.Add("XAMP_MinAUM");
            dtVariant.Columns.Add("XAMP_MaxAUM");
            dtVariant.Columns.Add("XAMP_MinAge");
            dtVariant.Columns.Add("XAMP_MaxAge");
            dtVariant.Columns.Add("XAMP_MinTimeHorizon");
            dtVariant.Columns.Add("XAMP_MaxTimeHorizon");
            dtVariant.Columns.Add("XAMP_Description");
            dtVariant.Columns.Add("XAMP_CreatedOn");
            dtVariant.Columns.Add("Allocation");

            dtVariant.Columns.Add("Cash");
            dtVariant.Columns.Add("Debt");
            dtVariant.Columns.Add("Equity");
            dtVariant.Columns.Add("Alternate");

            dtVariant.Columns.Add("MinYear");
            dtVariant.Columns.Add("MinMonth");
            dtVariant.Columns.Add("MaxYear");
            dtVariant.Columns.Add("MaxMonth");

            dtVariant.Columns.Add("XAMP_ROR");
            dtVariant.Columns.Add("XAMP_RiskPercentage");
            //dtVariant.Columns.Add("Risk");

            DataRow drVariant;
            foreach (DataRow dr in dsVariantDetails.Tables[0].Rows)
            {   
                drVariant = dtVariant.NewRow();
                drVariant["XAMP_ModelPortfolioCode"] = dr["XAMP_ModelPortfolioCode"].ToString();
                drVariant["XAMP_ModelPortfolioName"] = dr["XAMP_ModelPortfolioName"].ToString();
                drVariant["XRC_RiskClass"] = dr["XRC_RiskClass"].ToString();
                drVariant["XRC_RiskClassCode"] = dr["XRC_RiskClassCode"].ToString();
                drVariant["XAMP_MinAUM"] = dr["XAMP_MinAUM"].ToString();
                drVariant["XAMP_MaxAUM"] = dr["XAMP_MaxAUM"].ToString();
                drVariant["XAMP_MinAge"] = dr["XAMP_MinAge"].ToString();
                drVariant["XAMP_MaxAge"] = dr["XAMP_MaxAge"].ToString();
                drVariant["XAMP_MinTimeHorizon"] = dr["XAMP_MinTimeHorizon"].ToString();
                drVariant["XAMP_MaxTimeHorizon"] = dr["XAMP_MaxTimeHorizon"].ToString();
                drVariant["XAMP_Description"] = dr["XAMP_Description"].ToString();
                drVariant["XAMP_CreatedOn"] = dr["XAMP_CreatedOn"].ToString();
                drVariant["Allocation"] = dr["Allocation"].ToString();

                drVariant["MinYear"] = Convert.ToInt32(dr["XAMP_MinTimeHorizon"].ToString()) / 12;
                drVariant["MinMonth"] = Convert.ToInt32(dr["XAMP_MinTimeHorizon"].ToString()) % 12;
                drVariant["MaxYear"] = Convert.ToInt32(dr["XAMP_MaxTimeHorizon"].ToString()) / 12;
                drVariant["MaxMonth"] = Convert.ToInt32(dr["XAMP_MaxTimeHorizon"].ToString()) % 12;

                drArrayAllocation = new DataRow[dsVariantDetails.Tables[2].Rows.Count];
                drArrayAllocation = dsVariantDetails.Tables[1].Select("XAMP_ModelPortfolioCode="+dr["XAMP_ModelPortfolioCode"].ToString());

                drVariant["Equity"] = drArrayAllocation[0][4];
                drVariant["Debt"] = drArrayAllocation[1][4];
                drVariant["Cash"] = drArrayAllocation[2][4];
                drVariant["Alternate"] = drArrayAllocation[3][4];

                drVariant["XAMP_ROR"] = dr["XAMP_ROR"].ToString();
                drVariant["XAMP_RiskPercentage"] = dr["XAMP_RiskPercentage"].ToString();
                //drVariant["Risk"] = dr["Risk"].ToString();
                dtVariant.Rows.Add(drVariant);
            }           
                RadGrid1.DataSource = dtVariant;
                RadGrid1.DataSourceID = String.Empty;
                RadGrid1.DataBind();               
        }

        protected void RadGrid1_DataBound(object sender, EventArgs e)
        {
        }

        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                decimal RORPer = 0;
                int minTimeHorizon = 0;
                int maxTimeHorizon = 0;
                decimal allocation = 0;
                decimal riskPer = 0;
                DataSet ds = adviserFPConfigurationBo.GetAdviserAssumptions(advisorVo.advisorId);

                DropDownList ddl = (DropDownList)e.Item.FindControl("ddlPickRiskClass");
                TextBox txtboxPortfolioName = (TextBox)e.Item.FindControl("txtNamePortfolio");
                TextBox txtboxMinAUM = (TextBox)e.Item.FindControl("txtMinAUM");
                TextBox txtboxMaxAUM = (TextBox)e.Item.FindControl("txtMaxAUM");
                TextBox txtboxMinAge = (TextBox)e.Item.FindControl("txtMinAge");
                TextBox txtboxMaxAge = (TextBox)e.Item.FindControl("txtMaxAge");
                TextBox txtboxMinTimeHorizonYear = (TextBox)e.Item.FindControl("txtMinTimeHorizonYear");
                TextBox txtboxMaxTimeHorizonYear = (TextBox)e.Item.FindControl("txtMaxTimeHorizonYear");
                TextBox txtboxMinTimeHorizonMonth = (TextBox)e.Item.FindControl("txtMinTimeHorizonMonth");
                TextBox txtboxMaxTimeHorizonMonth = (TextBox)e.Item.FindControl("txtMaxTimeHorizonMonth");
                TextBox txtboxVariantdescription = (TextBox)e.Item.FindControl("txtDescription");

                TextBox txtboxCashAllocation = (TextBox)e.Item.FindControl("txtCash");
                TextBox txtboxAlternateAllocation = (TextBox)e.Item.FindControl("txtAlternate");
                TextBox txtboxDebtAllocation = (TextBox)e.Item.FindControl("txtDebt");
                TextBox txtboxEquityAllocation = (TextBox)e.Item.FindControl("txtEquity");


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    switch (dr["WA_AssumptionId"].ToString())
                    {
                        case "AR":
                            {
                                if (!string.IsNullOrEmpty(txtboxAlternateAllocation.Text.Trim()))
                                    RORPer = (RORPer + (Convert.ToDecimal(txtboxAlternateAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString())))/100);
                                break;
                            }
                        case "CR":
                            {
                                if (!string.IsNullOrEmpty(txtboxCashAllocation.Text.Trim()))
                                    RORPer = (RORPer + (Convert.ToDecimal(txtboxCashAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString()))) / 100);
                                break;
                            }
                        case "DR":
                            {
                                if (!string.IsNullOrEmpty(txtboxDebtAllocation.Text.Trim()))
                                    RORPer = (RORPer + (Convert.ToDecimal(txtboxDebtAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString()))) / 100);
                                break;
                            }
                        case "ER":
                            {
                                if (!string.IsNullOrEmpty(txtboxEquityAllocation.Text.Trim()))
                                    RORPer = (RORPer + (Convert.ToDecimal(txtboxEquityAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString()))) / 100);
                                break;
                            }


                        case "RIA":
                            {
                                if (!string.IsNullOrEmpty(txtboxAlternateAllocation.Text.Trim()))
                                    riskPer = (riskPer + (Convert.ToDecimal(txtboxAlternateAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString()))) / 100);
                                break;
                            }
                        case "RIC":
                            {
                                if (!string.IsNullOrEmpty(txtboxCashAllocation.Text.Trim()))
                                    riskPer = (riskPer + (Convert.ToDecimal(txtboxCashAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString()))) / 100);
                                break;
                            }
                        case "RID":
                            {
                                if (!string.IsNullOrEmpty(txtboxDebtAllocation.Text.Trim()))
                                    riskPer = (riskPer + (Convert.ToDecimal(txtboxDebtAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString()))) / 100);
                                break;
                            }
                        case "RIE":
                            {
                                if (!string.IsNullOrEmpty(txtboxEquityAllocation.Text.Trim()))
                                    riskPer = (riskPer + (Convert.ToDecimal(txtboxEquityAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString()))) / 100);
                                break;
                            }
                    }
                }
                int VarMinYear = 0;
                int VarMinMonth = 0;
                int VarMaxMonth = 0;
                int VarMaxYear = 0;
                if (txtboxMinTimeHorizonYear.Text != "")
                {
                    VarMinYear = Convert.ToInt32(txtboxMinTimeHorizonYear.Text.Trim());
                }
                if (txtboxMinTimeHorizonMonth.Text != "")
                {
                    VarMinMonth = Convert.ToInt32(txtboxMinTimeHorizonMonth.Text.Trim());
                }
                if (txtboxMaxTimeHorizonYear.Text != "")
                {
                    VarMaxYear = Convert.ToInt32(txtboxMaxTimeHorizonYear.Text.Trim());
                }
                if (txtboxMaxTimeHorizonMonth.Text != "")
                {
                    VarMaxMonth = Convert.ToInt32(txtboxMaxTimeHorizonMonth.Text.Trim());
                }

                minTimeHorizon = (VarMinYear * 12) + VarMinMonth;
                maxTimeHorizon = (VarMaxYear * 12) + VarMaxMonth;

                double varMinAUM = 0;
                double varMaxAUM = 0;
                int varMinAge = 0;
                int varMaxAge = 0;

                if (txtboxMinAUM.Text != "")
                {
                    varMinAUM = Convert.ToDouble(txtboxMinAUM.Text.Trim());
                }
                if (txtboxMaxAUM.Text != "")
                {
                    varMaxAUM = Convert.ToDouble(txtboxMaxAUM.Text.Trim());
                }
                if (txtboxMinAge.Text != "")
                {
                    varMinAge = Convert.ToInt32(txtboxMinAge.Text.Trim());
                }
                if (txtboxMaxAge.Text != "")
                {
                    varMaxAge = Convert.ToInt32(txtboxMaxAge.Text.Trim());
                }

                allocation = Convert.ToDecimal((txtboxCashAllocation.Text.Trim())) + Convert.ToDecimal(txtboxAlternateAllocation.Text.Trim())
                            + Convert.ToDecimal(txtboxDebtAllocation.Text.Trim()) + Convert.ToDecimal(txtboxEquityAllocation.Text.Trim());

                if (allocation == 100)
                {
                    //dtGlobal = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable];
                    //drGlobal = dtGlobal.NewRow();
                    //drGlobal["XAMP_ModelPortfolioName"] = txtboxPortfolioName.Text;
                    //drGlobal["XRC_RiskClass"] = ddl.SelectedItem;
                    //drGlobal["XRC_RiskClassCode"] = ddl.SelectedValue;
                    //drGlobal["XAMP_MinAUM"] = txtboxMinAUM.Text;
                    //drGlobal["XAMP_MaxAUM"] = txtboxMaxAUM.Text;
                    //drGlobal["XAMP_MinAge"] = txtboxMinAge.Text;
                    //drGlobal["XAMP_MaxAge"] = txtboxMaxAge.Text;
                    //drGlobal["XAMP_MinTimeHorizon"] = minTimeHorizon;
                    //drGlobal["XAMP_MaxTimeHorizon"] = maxTimeHorizon;
                    //drGlobal["XAMP_Description"] = txtboxVariantdescription.Text;                    

                    //drGlobal["Alternate"] = txtboxAlternateAllocation.Text;
                    //drGlobal["Debt"] = txtboxDebtAllocation.Text;
                    //drGlobal["Equity"] = txtboxEquityAllocation.Text;
                    //drGlobal["Cash"] = txtboxCashAllocation.Text;

                    //drGlobal["XAMP_ROR"] = RORPer;
                    //drGlobal["XAMP_RiskPercentage"] = riskPer;
                    //drGlobal["Allocation"] = allocation;

                    //dtGlobal.Rows.Add(drGlobal);
                    ////Session[SessionContents.FPS_AddProspect_DataTable] = dtGlobal;
                    //RadGrid1.DataSource = dtGlobal;
                    // bindRadGrid1();

                    modelPortfolioVo.PortfolioName = txtboxPortfolioName.Text;
                    modelPortfolioVo.RiskClassCode = ddl.SelectedValue;
                    modelPortfolioVo.MinAUM = varMinAUM;
                    modelPortfolioVo.MaxAUM = varMaxAUM;
                    modelPortfolioVo.MinAge = varMinAge;
                    modelPortfolioVo.MaxAge = varMaxAge;
                    modelPortfolioVo.MinTimeHorizon = minTimeHorizon;
                    modelPortfolioVo.MaxTimeHorizon = maxTimeHorizon;
                    modelPortfolioVo.VariantDescription = txtboxVariantdescription.Text;

                    modelPortfolioVo.AlternateAllocation = Convert.ToDecimal(txtboxAlternateAllocation.Text);
                    modelPortfolioVo.DebtAllocation = Convert.ToDecimal(txtboxDebtAllocation.Text);
                    modelPortfolioVo.EquityAllocation = Convert.ToDecimal(txtboxEquityAllocation.Text);
                    modelPortfolioVo.CashAllocation = Convert.ToDecimal(txtboxCashAllocation.Text);

                    modelPortfolioVo.ROR = RORPer;
                    modelPortfolioVo.RiskPercentage = riskPer;

                    modelPortfolioBo.CreateVariantAssetPortfolio(modelPortfolioVo, advisorVo.advisorId, advisorVo.UserId);
                    bindRadGrid1();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You dont have enough amount');", true);
                }
            }
            
            catch (Exception ex)
            {
                RadGrid1.Controls.Add(new LiteralControl("Unable to insert Scheme. Reason: " + ex.Message));
                e.Canceled = true;
            }
        }
        
        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                int modelPortfolioCode = Convert.ToInt32(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XAMP_ModelPortfolioCode"].ToString());
                modelPortfolioBo.DeleteVariantAssetPortfolio(modelPortfolioCode);
                bindRadGrid1();
            }
            catch (Exception ex)
            {
                RadGrid1.Controls.Add(new LiteralControl("Unable to Delete the Record. Reason: " + ex.Message));
                e.Canceled = true;
            } 
        }       

        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                decimal RORPer = 0;
                int minTimeHorizon = 0;
                int maxTimeHorizon = 0;
                decimal allocation = 0;
                decimal riskPer = 0;
                DataSet ds = adviserFPConfigurationBo.GetAdviserAssumptions(advisorVo.advisorId);

                DropDownList ddl = (DropDownList)e.Item.FindControl("ddlPickRiskClass");
                TextBox txtboxPortfolioName = (TextBox)e.Item.FindControl("txtNamePortfolio");
                TextBox txtboxMinAUM = (TextBox)e.Item.FindControl("txtMinAUM");
                TextBox txtboxMaxAUM = (TextBox)e.Item.FindControl("txtMaxAUM");
                TextBox txtboxMinAge = (TextBox)e.Item.FindControl("txtMinAge");
                TextBox txtboxMaxAge = (TextBox)e.Item.FindControl("txtMaxAge");
                TextBox txtboxMinTimeHorizonYear = (TextBox)e.Item.FindControl("txtMinTimeHorizonYear");
                TextBox txtboxMaxTimeHorizonYear = (TextBox)e.Item.FindControl("txtMaxTimeHorizonYear");
                TextBox txtboxMinTimeHorizonMonth = (TextBox)e.Item.FindControl("txtMinTimeHorizonMonth");
                TextBox txtboxMaxTimeHorizonMonth = (TextBox)e.Item.FindControl("txtMaxTimeHorizonMonth");
                TextBox txtboxVariantdescription = (TextBox)e.Item.FindControl("txtDescription");

                TextBox txtboxCashAllocation = (TextBox)e.Item.FindControl("txtCash");
                TextBox txtboxAlternateAllocation = (TextBox)e.Item.FindControl("txtAlternate");
                TextBox txtboxDebtAllocation = (TextBox)e.Item.FindControl("txtDebt");
                TextBox txtboxEquityAllocation = (TextBox)e.Item.FindControl("txtEquity");


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    switch (dr["WA_AssumptionId"].ToString())
                    {
                        case "AR":
                            {
                                if (!string.IsNullOrEmpty(txtboxAlternateAllocation.Text.Trim()))
                                    RORPer = (RORPer + (Convert.ToDecimal(txtboxAlternateAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString()))) / 100);
                                break;
                            }
                        case "CR":
                            {
                                if (!string.IsNullOrEmpty(txtboxCashAllocation.Text.Trim()))
                                    RORPer = (RORPer + (Convert.ToDecimal(txtboxCashAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString()))) / 100);
                                break;
                            }
                        case "DR":
                            {
                                if (!string.IsNullOrEmpty(txtboxDebtAllocation.Text.Trim()))
                                    RORPer = (RORPer + (Convert.ToDecimal(txtboxDebtAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString()))) / 100);
                                break;
                            }
                        case "ER":
                            {
                                if (!string.IsNullOrEmpty(txtboxEquityAllocation.Text.Trim()))
                                    RORPer = (RORPer + (Convert.ToDecimal(txtboxEquityAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString()))) / 100);
                                break;
                            }


                        case "RIA":
                            {
                                if (!string.IsNullOrEmpty(txtboxAlternateAllocation.Text.Trim()))
                                    riskPer = (riskPer + (Convert.ToDecimal(txtboxAlternateAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString()))) / 100);
                                break;
                            }
                        case "RIC":
                            {
                                if (!string.IsNullOrEmpty(txtboxCashAllocation.Text.Trim()))
                                    riskPer = (riskPer + (Convert.ToDecimal(txtboxCashAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString()))) / 100);
                                break;
                            }
                        case "RID":
                            {
                                if (!string.IsNullOrEmpty(txtboxDebtAllocation.Text.Trim()))
                                    riskPer = (riskPer + (Convert.ToDecimal(txtboxDebtAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString()))) / 100);
                                break;
                            }
                        case "RIE":
                            {
                                if (!string.IsNullOrEmpty(txtboxEquityAllocation.Text.Trim()))
                                    riskPer = (riskPer + (Convert.ToDecimal(txtboxEquityAllocation.Text.Trim()) * Convert.ToDecimal((dr["AA_Value"].ToString()))) / 100);
                                break;
                            }
                    }
                }
                int VarMinYear = 0;
                int VarMinMonth = 0;
                int VarMaxMonth = 0;
                int VarMaxYear = 0;
                if (txtboxMinTimeHorizonYear.Text != "")
                {
                    VarMinYear = Convert.ToInt32(txtboxMinTimeHorizonYear.Text.Trim());
                }
                if (txtboxMinTimeHorizonMonth.Text != "")
                {
                    VarMinMonth = Convert.ToInt32(txtboxMinTimeHorizonMonth.Text.Trim());
                }
                if (txtboxMaxTimeHorizonYear.Text != "")
                {
                    VarMaxYear = Convert.ToInt32(txtboxMaxTimeHorizonYear.Text.Trim());
                }
                if (txtboxMaxTimeHorizonMonth.Text != "")
                {
                    VarMaxMonth = Convert.ToInt32(txtboxMaxTimeHorizonMonth.Text.Trim());
                }

                minTimeHorizon = (VarMinYear * 12) + VarMinMonth;
                maxTimeHorizon = (VarMaxYear * 12) + VarMaxMonth;

                //minTimeHorizon = (Convert.ToInt32(txtboxMinTimeHorizonYear.Text.Trim()) * 12) + Convert.ToInt32(txtboxMinTimeHorizonMonth.Text.Trim());
                //maxTimeHorizon = (Convert.ToInt32(txtboxMaxTimeHorizonYear.Text.Trim()) * 12) + Convert.ToInt32(txtboxMaxTimeHorizonMonth.Text.Trim());

                allocation = Convert.ToDecimal((txtboxCashAllocation.Text.Trim())) + Convert.ToDecimal(txtboxAlternateAllocation.Text.Trim())
                            + Convert.ToDecimal(txtboxDebtAllocation.Text.Trim()) + Convert.ToDecimal(txtboxEquityAllocation.Text.Trim());

                double varMinAUM = 0;
                double varMaxAUM = 0;
                int varMinAge = 0;
                int varMaxAge = 0;

                if (txtboxMinAUM.Text != "")
                {
                    varMinAUM = Convert.ToDouble(txtboxMinAUM.Text.Trim());
                }
                if (txtboxMaxAUM.Text != "")
                {
                    varMaxAUM = Convert.ToDouble(txtboxMaxAUM.Text.Trim());
                }
                if (txtboxMinAge.Text != "")
                {
                    varMinAge = Convert.ToInt32(txtboxMinAge.Text.Trim());
                }
                if (txtboxMaxAge.Text != "")
                {
                    varMaxAge = Convert.ToInt32(txtboxMaxAge.Text.Trim());
                }


                if (allocation == 100)
                {
                    GridEditFormItem item = (GridEditFormItem)e.Item; 
                    string riskCode = item.GetDataKeyValue("XRC_RiskClassCode").ToString();
                    int code = Convert.ToInt32(item.GetDataKeyValue("XAMP_ModelPortfolioCode"));

                    modelPortfolioVo.ModelPortfolioCode = code;
                    modelPortfolioVo.PortfolioName = txtboxPortfolioName.Text;
                    modelPortfolioVo.RiskClassCode = riskCode;
                    modelPortfolioVo.MinAUM = varMinAUM;
                    modelPortfolioVo.MaxAUM = varMaxAUM;
                    modelPortfolioVo.MinAge = varMinAge;
                    modelPortfolioVo.MaxAge = varMaxAge;
                    modelPortfolioVo.MinTimeHorizon = minTimeHorizon;
                    modelPortfolioVo.MaxTimeHorizon = maxTimeHorizon;
                    modelPortfolioVo.VariantDescription = txtboxVariantdescription.Text;

                    modelPortfolioVo.AlternateAllocation = Convert.ToDecimal(txtboxAlternateAllocation.Text);
                    modelPortfolioVo.DebtAllocation = Convert.ToDecimal(txtboxDebtAllocation.Text);
                    modelPortfolioVo.EquityAllocation = Convert.ToDecimal(txtboxEquityAllocation.Text);
                    modelPortfolioVo.CashAllocation = Convert.ToDecimal(txtboxCashAllocation.Text);

                    modelPortfolioVo.ROR = RORPer;
                    modelPortfolioVo.RiskPercentage = riskPer;

                    modelPortfolioBo.CreateVariantAssetPortfolio(modelPortfolioVo, advisorVo.advisorId, advisorVo.UserId);
                    bindRadGrid1();                    
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You dont have enough amount');", true);
                }
            }
            catch (Exception ex)
            {
                RadGrid1.Controls.Add(new LiteralControl("Unable to Update Variant. Reason: " + ex.Message));
                e.Canceled = true;
            }
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
            {                
            }
            else if (e.CommandName == RadGrid.RebindGridCommandName && e.Item.OwnerTableView.IsItemInserted)
            {
                e.Canceled = true;
            }
            else if (e.CommandName == RadGrid.UpdateCommandName)
            {                
            }            
            else
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
                if (!editColumn.Visible)
                    editColumn.Visible = true;                
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            DataSet dsAdviserRiskClass = new DataSet();
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
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string modelPortfolio = dataItem["XAMP_ModelPortfolioName"].Text;

                LinkButton button = dataItem["DeleteColumn"].Controls[0] as LinkButton;
                button.Attributes["onclick"] = "return confirm('Are you sure you want to delete " +
                modelPortfolio + "?')";
            }

            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                int adviserId = advisorVo.advisorId;
                GridEditFormItem editform = (GridEditFormItem)e.Item;
                DropDownList ddlPickRiskClass = (DropDownList)editform.FindControl("ddlPickRiskClass");
                TextBox txtNamePortfolio = (TextBox)editform.FindControl("txtNamePortfolio");
                TextBox txtPortfolioName = (TextBox)editform.FindControl("txtPortfolioName");
                HtmlTableRow trRiskClassDdl = (HtmlTableRow)editform.FindControl("trRiskClassDdl");
                HtmlTableRow trRiskClassTxt = (HtmlTableRow)editform.FindControl("trRiskClassTxt");
                HtmlTableRow trAddNamePortfolio = (HtmlTableRow)editform.FindControl("trAddNamePortfolio");
                //HtmlTableRow trEditNamePortfolio = (HtmlTableRow)editform.FindControl("trEditNamePortfolio");
                
                if (e.Item.RowIndex == -1)
                {
                    trRiskClassDdl.Visible = true;
                    trRiskClassTxt.Visible = false;
                    //trAddNamePortfolio.Visible = true;
                    //trEditNamePortfolio.Visible = false;

                    dsAdviserRiskClass = riskprofilebo.GetAdviserRiskClasses(adviserId);
                    if (dsAdviserRiskClass.Tables[0].Rows.Count > 0)
                    {
                        ddlPickRiskClass.DataSource = dsAdviserRiskClass.Tables[0];
                        ddlPickRiskClass.DataValueField = dsAdviserRiskClass.Tables[0].Columns["XRC_RiskClassCode"].ToString();
                        ddlPickRiskClass.DataTextField = dsAdviserRiskClass.Tables[0].Columns["XRC_RiskClass"].ToString();
                        ddlPickRiskClass.DataBind();
                        ddlPickRiskClass.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Risk Class", "Select Risk Class"));
                    }                    
                }
                else
                {
                    trRiskClassDdl.Visible = false;
                    trRiskClassTxt.Visible = true;
                    txtNamePortfolio.Enabled = false;
                    //txtPortfolioName.Enabled = false;
                    //trAddNamePortfolio.Visible = true;
                    //trEditNamePortfolio.Visible = true;
                }
            }
        }

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    //dtGlobal = (DataTable)Session[SessionContents.FPS_AddProspect_DataTable]; 
           
        //    foreach (DataRow dr in dtGlobal.Rows)
        //    {
        //        modelPortfolioVo.PortfolioName = dr["XAMP_ModelPortfolioName"].ToString();
        //        modelPortfolioVo.RiskClassCode = dr["XRC_RiskClassCode"].ToString();
        //        modelPortfolioVo.MinAUM =  Convert.ToDouble(dr["XAMP_MinAUM"]);
        //        modelPortfolioVo.MaxAUM = Convert.ToDouble(dr["XAMP_MaxAUM"]);
        //        modelPortfolioVo.MinAge =  Convert.ToInt32(dr["XAMP_MinAge"]);
        //        modelPortfolioVo.MaxAge = Convert.ToInt32(dr["XAMP_MaxAge"]);
        //        modelPortfolioVo.MinTimeHorizon = Convert.ToInt32(dr["XAMP_MinTimeHorizon"]);
        //        modelPortfolioVo.MaxTimeHorizon = Convert.ToInt32(dr["XAMP_MaxTimeHorizon"]);
        //        modelPortfolioVo.VariantDescription = dr["XAMP_Description"].ToString();

        //        modelPortfolioVo.AlternateAllocation = Convert.ToDecimal(dr["Alternate"]);
        //        modelPortfolioVo.DebtAllocation = Convert.ToDecimal(dr["Debt"]);
        //        modelPortfolioVo.EquityAllocation = Convert.ToDecimal(dr["Equity"]);
        //        modelPortfolioVo.CashAllocation = Convert.ToDecimal(dr["Cash"]);

        //        modelPortfolioVo.ROR = Convert.ToDecimal(dr["XAMP_ROR"]);
        //        modelPortfolioVo.RiskPercentage = Convert.ToDecimal(dr["XAMP_RiskPercentage"]);
                
        //        modelPortfolioBo.CreateVariantAssetPortfolio(modelPortfolioVo, advisorVo.advisorId, advisorVo.UserId);
        //    }            
        //}
    }
}