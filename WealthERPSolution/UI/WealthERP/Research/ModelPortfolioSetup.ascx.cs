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
        HtmlTableRow trInvestmentAmount;
        HtmlTableRow trTimeHorizon;


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
            dtVariant.Columns.Add("MinAUM");
            dtVariant.Columns.Add("MaxAUM");
            dtVariant.Columns.Add("XAMP_ROR");
            dtVariant.Columns.Add("XAMP_RiskPercentage");
            dtVariant.Columns.Add("XAMP_IsRiskModel");
            //dtVariant.Columns.Add("Risk");

            DataRow drVariant;
            foreach (DataRow dr in dsVariantDetails.Tables[0].Rows)
            {
                drVariant = dtVariant.NewRow();
                drVariant["XAMP_ModelPortfolioCode"] = dr["XAMP_ModelPortfolioCode"].ToString();
                drVariant["XAMP_ModelPortfolioName"] = dr["XAMP_ModelPortfolioName"].ToString();
                drVariant["XRC_RiskClass"] = dr["XRC_RiskClass"].ToString();
                drVariant["XRC_RiskClassCode"] = dr["XRC_RiskClassCode"].ToString();
               
                //dr["XAMP_MaxAUM"].ToString();
                drVariant["XAMP_MinAge"] = dr["XAMP_MinAge"].ToString();
                drVariant["XAMP_MaxAge"] = dr["XAMP_MaxAge"].ToString();
               
                drVariant["XAMP_Description"] = dr["XAMP_Description"].ToString();
                drVariant["XAMP_CreatedOn"] = DateTime.Parse(dr["XAMP_CreatedOn"].ToString()).ToShortDateString();
                drVariant["Allocation"] = dr["Allocation"].ToString();

                //drVariant["MinYear"] = Convert.ToInt32(dr["XAMP_MinTimeHorizon"].ToString()) / 12;
                //drVariant["MinMonth"] = Convert.ToInt32(dr["XAMP_MinTimeHorizon"].ToString()) % 12;
                //drVariant["MaxYear"] = Convert.ToInt32(dr["XAMP_MaxTimeHorizon"].ToString()) / 12;
                //drVariant["MaxMonth"] = Convert.ToInt32(dr["XAMP_MaxTimeHorizon"].ToString()) % 12;             

                drArrayAllocation = new DataRow[dsVariantDetails.Tables[2].Rows.Count];
                drArrayAllocation = dsVariantDetails.Tables[1].Select("XAMP_ModelPortfolioCode=" + dr["XAMP_ModelPortfolioCode"].ToString());

                drVariant["Equity"] = drArrayAllocation[0][4];
                drVariant["Debt"] = drArrayAllocation[1][4];
                drVariant["Cash"] = drArrayAllocation[2][4];
                drVariant["Alternate"] = drArrayAllocation[3][4];

                drVariant["XAMP_ROR"] = dr["XAMP_ROR"].ToString();
                drVariant["XAMP_RiskPercentage"] = dr["XAMP_RiskPercentage"].ToString();
                if (dr["XAMP_IsRiskModel"].ToString() == "1")
                {
                    drVariant["XAMP_IsRiskModel"] = "Risk Profile";
                    drVariant["XAMP_MinTimeHorizon"] = "N/A";
                    drVariant["XAMP_MaxTimeHorizon"] = "N/A";
                    drVariant["MinYear"] = "0";
                    drVariant["MinMonth"] = "0";
                    drVariant["MaxYear"] = "0";
                    drVariant["MaxMonth"] ="0";
                    drVariant["XAMP_MinAUM"] = "N/A";
                    drVariant["XAMP_MaxAUM"] = "N/A";
                    drVariant["MinAUM"] = "0";
                    drVariant["MaxAUM"] = "0";
                }
                else if (dr["XAMP_IsRiskModel"].ToString() == "0")
                {
                    drVariant["XAMP_IsRiskModel"] = "Goal Profile";
                    drVariant["XAMP_MinTimeHorizon"] = dr["XAMP_MinTimeHorizon"].ToString();
                    drVariant["XAMP_MaxTimeHorizon"] = dr["XAMP_MaxTimeHorizon"].ToString();
                    drVariant["MinYear"] = Convert.ToInt32(dr["XAMP_MinTimeHorizon"].ToString()) / 12;
                    drVariant["MinMonth"] = Convert.ToInt32(dr["XAMP_MinTimeHorizon"].ToString());
                    drVariant["MaxYear"] = Convert.ToInt32(dr["XAMP_MaxTimeHorizon"].ToString()) / 12;
                    drVariant["MaxMonth"] = Convert.ToInt32(dr["XAMP_MaxTimeHorizon"].ToString());
                    drVariant["XAMP_MinAUM"] = Math.Round(Decimal.Parse(dr["XAMP_MinAUM"].ToString()), 0).ToString();
                    drVariant["XAMP_MaxAUM"] = Math.Round(Decimal.Parse(dr["XAMP_MaxAUM"].ToString()), 0).ToString();
                    drVariant["MinAUM"] = Math.Round(Decimal.Parse(dr["XAMP_MinAUM"].ToString()), 0).ToString();
                    drVariant["MaxAUM"] = Math.Round(Decimal.Parse(dr["XAMP_MaxAUM"].ToString()), 0).ToString();
                  
                  
                
                }
                dtVariant.Rows.Add(drVariant);

            }
            RadGrid1.DataSource = dtVariant;
            RadGrid1.DataSourceID = String.Empty;
            RadGrid1.DataBind();
        }

        protected void RadGrid1_DataBound(object sender, EventArgs e)
        {
        }

        protected void ddlModelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Session.Remove(SessionContents.FPS_AddProspect_DataTable);
            DropDownList dropdown = (DropDownList)sender;
            string categoryCode = dropdown.SelectedValue;
            if (dropdown.SelectedValue == "RC")
            {
                trTimeHorizon.Visible = false;
                trInvestmentAmount.Visible = false;
            }
            else if (dropdown.SelectedValue == "GC")
            {
                trTimeHorizon.Visible = true;
                trInvestmentAmount.Visible = true;
            }
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
                bool chkFlag = false ;
                DataSet ds = adviserFPConfigurationBo.GetAdviserAssumptions(advisorVo.advisorId);

                DropDownList ddl = (DropDownList)e.Item.FindControl("ddlPickRiskClass");
                DropDownList ddlModelType = (DropDownList)e.Item.FindControl("ddlModelType");
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

                DataTable dtGetMaxminAge = adviserFPConfigurationBo.GetMaxMinAge(advisorVo.advisorId, ddl.SelectedValue, ddlModelType.SelectedValue);
             
                int VarMinMonth = 0;
                int VarMaxMonth = 0;
              
                if (txtboxMinTimeHorizonMonth.Text != "")
                {
                    VarMinMonth = Convert.ToInt32(txtboxMinTimeHorizonMonth.Text.Trim());
                }
              
                if (txtboxMaxTimeHorizonMonth.Text != "")
                {
                    VarMaxMonth = Convert.ToInt32(txtboxMaxTimeHorizonMonth.Text.Trim());
                }              

                minTimeHorizon = VarMinMonth;
                maxTimeHorizon = VarMaxMonth;

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
                int minAge = int.Parse(txtboxMinAge.Text);
                int maxAge = int.Parse(txtboxMaxAge.Text);
                int count = 0, countAge = 0, countTimeHorizon = 0, countInvestment = 0 ;             
                int countDataset = dtGetMaxminAge.Rows.Count;
                if (ddlModelType.SelectedValue == "RC")
                {
                    foreach (DataRow dr in dtGetMaxminAge.Rows)
                    {
                        if ((int.Parse(dr["MinAge"].ToString()) < minAge && minAge < int.Parse(dr["MaxAge"].ToString())) || (int.Parse(dr["MinAge"].ToString()) < maxAge && maxAge < int.Parse(dr["MaxAge"].ToString())))
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Model Can not be created  Criteria overlapping with existing model  Please try change the criteria');", true);
                            return;
                        }

                        else if (maxAge < int.Parse(dr["MinAge"].ToString()) || minAge > int.Parse(dr["MaxAge"].ToString()))
                        {
                            count++;
                        }
                        else if (minAge < int.Parse(dr["MinAge"].ToString()) && maxAge < int.Parse(dr["MinAge"].ToString()))
                        {
                            count++;
                        }
                    }
                }

                else  if (ddlModelType.SelectedValue == "GC")                
                {
                    foreach (DataRow dr in dtGetMaxminAge.Rows)
                    {
                        if (maxAge < int.Parse(dr["MinAge"].ToString()) || minAge > int.Parse(dr["MaxAge"].ToString()))
                        {
                            countAge++;
                        }
                        else if (minAge < int.Parse(dr["MinAge"].ToString()) && maxAge < int.Parse(dr["MinAge"].ToString()))
                        {
                            countAge++;
                        }

                        if (varMaxAUM < double.Parse(dr["XAMP_MinAUM"].ToString()) || varMinAUM > double.Parse(dr["XAMP_MaxAUM"].ToString()))
                        {
                            countInvestment++;
                        }
                        else if (varMinAUM < double.Parse(dr["XAMP_MinAUM"].ToString()) && varMaxAUM < double.Parse(dr["XAMP_MinAUM"].ToString()))
                        {
                            countInvestment++;
                        }

                        if (VarMaxMonth < int.Parse(dr["XAMP_MinTimeHorizon"].ToString()) || VarMinMonth > int.Parse(dr["XAMP_MaxTimeHorizon"].ToString()))
                        {
                            countTimeHorizon++;
                        }
                        else if (VarMinMonth < int.Parse(dr["XAMP_MinTimeHorizon"].ToString()) && VarMinMonth < int.Parse(dr["XAMP_MinTimeHorizon"].ToString()))
                        {
                            countTimeHorizon++;
                        }                       
                    }
                }
                if (ddlModelType.SelectedValue == "RC")
                {
                    if (count == countDataset)
                    {
                       chkFlag = true;
                    }

                   else
                    {
                       chkFlag = false;
                    }
                }
                else if (ddlModelType.SelectedValue == "GC")
                {
                    if (countAge == countDataset || countInvestment == countDataset || countTimeHorizon == countDataset)
                    {
                            chkFlag = true;
                    }
                    else
                         chkFlag = false;
                }

                    
           
                if(chkFlag == true)
                {
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
                       
                        allocation = Convert.ToDecimal((txtboxCashAllocation.Text.Trim())) + Convert.ToDecimal(txtboxAlternateAllocation.Text.Trim())
                                    + Convert.ToDecimal(txtboxDebtAllocation.Text.Trim()) + Convert.ToDecimal(txtboxEquityAllocation.Text.Trim());

                        if (allocation == 100)
                        {
                            
                            if (ddlModelType.SelectedValue == "RC")
                            {

                                modelPortfolioVo.MinAUM = 0;
                                modelPortfolioVo.MaxAUM = 0;
                                modelPortfolioVo.MinTimeHorizon = 0;
                                modelPortfolioVo.MaxTimeHorizon = 0;

                            }
                            else
                            {
                                modelPortfolioVo.MinAUM = varMinAUM;
                                modelPortfolioVo.MaxAUM = varMaxAUM;
                                modelPortfolioVo.MinTimeHorizon = minTimeHorizon;
                                modelPortfolioVo.MaxTimeHorizon = maxTimeHorizon;

                            }
                            modelPortfolioVo.PortfolioName = txtboxPortfolioName.Text;
                            modelPortfolioVo.RiskClassCode = ddl.SelectedValue;

                            modelPortfolioVo.MinAge = varMinAge;
                            modelPortfolioVo.MaxAge = varMaxAge;

                            modelPortfolioVo.VariantDescription = txtboxVariantdescription.Text;

                            modelPortfolioVo.AlternateAllocation = Convert.ToDecimal(txtboxAlternateAllocation.Text);
                            modelPortfolioVo.DebtAllocation = Convert.ToDecimal(txtboxDebtAllocation.Text);
                            modelPortfolioVo.EquityAllocation = Convert.ToDecimal(txtboxEquityAllocation.Text);
                            modelPortfolioVo.CashAllocation = Convert.ToDecimal(txtboxCashAllocation.Text);

                            modelPortfolioVo.ROR = RORPer;
                            modelPortfolioVo.RiskPercentage = riskPer;
                            modelPortfolioVo.IsRiskClass = ddlModelType.SelectedValue;
                            modelPortfolioBo.CreateVariantAssetPortfolio(modelPortfolioVo, advisorVo.advisorId, advisorVo.UserId);
                            bindRadGrid1();
                        }
                    }
    
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Model can not be created  Criteria overlapping with existing model  Please try change the criteria');", true);
                        return;
                    }
        

                }
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Total asset allocation should be 100%');", true);
                //}
       


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
                bool chkFlag = false;
                DataSet ds = adviserFPConfigurationBo.GetAdviserAssumptions(advisorVo.advisorId);
                GridEditFormItem item = (GridEditFormItem)e.Item;
                string riskCode = item.GetDataKeyValue("XRC_RiskClassCode").ToString();
                int code = Convert.ToInt32(item.GetDataKeyValue("XAMP_ModelPortfolioCode"));

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
                DropDownList ddlModelType = (DropDownList)e.Item.FindControl("ddlModelType");
                TextBox txtIsRiskClass = (TextBox)e.Item.FindControl("txtIsRiskClass");
                string isRiskClass ="";
                if (txtIsRiskClass.Text == "Risk Profile")
                {
                    isRiskClass = "RC";
                }
                else if (txtIsRiskClass.Text == "Goal Profile")
                {
                    isRiskClass = "GC";
                }
                allocation = Convert.ToDecimal((txtboxCashAllocation.Text.Trim())) + Convert.ToDecimal(txtboxAlternateAllocation.Text.Trim())
                               + Convert.ToDecimal(txtboxDebtAllocation.Text.Trim()) + Convert.ToDecimal(txtboxEquityAllocation.Text.Trim());

                if (allocation == 100)
                {
                DataTable dtGetMaxminAge = adviserFPConfigurationBo.GetMaxMinAgeModelPortFolio(advisorVo.advisorId, riskCode, code, isRiskClass);
                //int minAge = Convert.ToInt16(dtGetMaxminAge.Rows[0]["MinAge"].ToString());
                //int maxAge = Convert.ToInt16(dtGetMaxminAge.Rows[0]["MaxAge"].ToString());
                //int getminAge = int.Parse(txtboxMinAge.Text);
                //int getMaxAge = int.Parse(txtboxMaxAge.Text);

                int VarMinMonth = 0;
                int VarMaxMonth = 0;

                if (txtboxMinTimeHorizonMonth.Text != "")
                {
                    VarMinMonth = Convert.ToInt32(txtboxMinTimeHorizonMonth.Text.Trim());
                }

                if (txtboxMaxTimeHorizonMonth.Text != "")
                {
                    VarMaxMonth = Convert.ToInt32(txtboxMaxTimeHorizonMonth.Text.Trim());
                }

                minTimeHorizon = VarMinMonth;
                maxTimeHorizon = VarMaxMonth;

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
                int minAge = int.Parse(txtboxMinAge.Text);
                int maxAge = int.Parse(txtboxMaxAge.Text);
               
                int countDataset = dtGetMaxminAge.Rows.Count;
                int count = 0, countAge = 0, countTimeHorizon = 0, countInvestment = 0;   
                if (dtGetMaxminAge.Rows.Count > 0)
                {
                    if (isRiskClass == "RC")
                    {
                        foreach (DataRow dr in dtGetMaxminAge.Rows)
                        {
                            if ((int.Parse(dr["MinAge"].ToString()) < minAge && minAge < int.Parse(dr["MaxAge"].ToString())) || (int.Parse(dr["MinAge"].ToString()) < maxAge && maxAge < int.Parse(dr["MaxAge"].ToString())))
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Model Can not be created  Criteria overlapping with existing model  Please try change the criteria');", true);
                                return;
                            }

                            else if (maxAge < int.Parse(dr["MinAge"].ToString()) || minAge > int.Parse(dr["MaxAge"].ToString()))
                            {
                                count++;
                            }
                            else if (minAge < int.Parse(dr["MinAge"].ToString()) && maxAge < int.Parse(dr["MinAge"].ToString()))
                            {
                                count++;
                            }
                        }
                    }

                    else if (isRiskClass == "GC")
                    {
                        foreach (DataRow dr in dtGetMaxminAge.Rows)
                        {
                            if (maxAge < int.Parse(dr["MinAge"].ToString()) || minAge > int.Parse(dr["MaxAge"].ToString()))
                            {
                                countAge++;
                            }
                            else if (minAge < int.Parse(dr["MinAge"].ToString()) && maxAge < int.Parse(dr["MinAge"].ToString()))
                            {
                                countAge++;
                            }

                            if (varMaxAUM < double.Parse(dr["XAMP_MinAUM"].ToString()) || varMinAUM > double.Parse(dr["XAMP_MaxAUM"].ToString()))
                            {
                                countInvestment++;
                            }
                            else if (varMinAUM < double.Parse(dr["XAMP_MinAUM"].ToString()) && varMaxAUM < double.Parse(dr["XAMP_MinAUM"].ToString()))
                            {
                                countInvestment++;
                            }

                            if (VarMaxMonth < int.Parse(dr["XAMP_MinTimeHorizon"].ToString()) || VarMinMonth > int.Parse(dr["XAMP_MaxTimeHorizon"].ToString()))
                            {
                                countTimeHorizon++;
                            }
                            else if (VarMinMonth < int.Parse(dr["XAMP_MinTimeHorizon"].ToString()) && VarMinMonth < int.Parse(dr["XAMP_MinTimeHorizon"].ToString()))
                            {
                                countTimeHorizon++;
                            }
                        }
                    }
                }
                if (isRiskClass == "RC")
                {
                    if (count == countDataset)
                    {
                        chkFlag = true;
                    }

                    else
                    {
                        chkFlag = false;
                    }
                }
                else if (isRiskClass == "GC")
                {
                    if (countAge == countDataset || countInvestment == countDataset || countTimeHorizon == countDataset)
                    {
                        chkFlag = true;
                    }
                    else
                        chkFlag = false;
                }



                if (chkFlag == true)
                {
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





                    if (isRiskClass == "RC")
                        {

                            modelPortfolioVo.MinAUM = 0;
                            modelPortfolioVo.MaxAUM = 0;
                            modelPortfolioVo.MinTimeHorizon = 0;
                            modelPortfolioVo.MaxTimeHorizon = 0;

                        }
                        else
                        {
                            modelPortfolioVo.MinAUM = varMinAUM;
                            modelPortfolioVo.MaxAUM = varMaxAUM;
                            modelPortfolioVo.MinTimeHorizon = minTimeHorizon;
                            modelPortfolioVo.MaxTimeHorizon = maxTimeHorizon;

                        }
                        modelPortfolioVo.ModelPortfolioCode = code;
                        modelPortfolioVo.PortfolioName = txtboxPortfolioName.Text;
                        modelPortfolioVo.RiskClassCode = riskCode;

                        modelPortfolioVo.MinAge = varMinAge;
                        modelPortfolioVo.MaxAge = varMaxAge;

                        modelPortfolioVo.VariantDescription = txtboxVariantdescription.Text;

                        modelPortfolioVo.AlternateAllocation = Convert.ToDecimal(txtboxAlternateAllocation.Text);
                        modelPortfolioVo.DebtAllocation = Convert.ToDecimal(txtboxDebtAllocation.Text);
                        modelPortfolioVo.EquityAllocation = Convert.ToDecimal(txtboxEquityAllocation.Text);
                        modelPortfolioVo.CashAllocation = Convert.ToDecimal(txtboxCashAllocation.Text);

                        modelPortfolioVo.ROR = RORPer;
                        modelPortfolioVo.RiskPercentage = riskPer;

                        modelPortfolioBo.UpdateVariantAssetPortfolio(modelPortfolioVo, advisorVo.advisorId, advisorVo.UserId);
                        bindRadGrid1();
                    }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Model Can not be created  Criteria overlapping with existing model  Please try change the criteria');", true);
                    return;
                }
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('You dont have enough amount');", true);
                    //}
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Allocation is not 100%');", true);
                    return;
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
                hdnButtonText.Value = "Insert";
            }
            else if (e.CommandName == RadGrid.RebindGridCommandName && e.Item.OwnerTableView.IsItemInserted)
            {
                e.Canceled = true;
            }
            else if (e.CommandName == RadGrid.UpdateCommandName)
            {
            }
            else if (e.CommandName == "Edit")
            {
                hdnButtonText.Value = "Edit";
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
            //DataSet dsAdviserRiskClass = new DataSet();
            DataTable dtDefaultRiskClass = new DataTable();
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
                trInvestmentAmount = (HtmlTableRow)editform.FindControl("trInvestmentAmount");
                trTimeHorizon = (HtmlTableRow)editform.FindControl("trTimeHorizon");

                HtmlTableRow trIsRiskClassDdl = (HtmlTableRow)editform.FindControl("trIsRiskClass");
                HtmlTableRow trIsRiskClassText = (HtmlTableRow)editform.FindControl("trIsRiskClassText");
                //HtmlTableRow trAddNamePortfolio = (HtmlTableRow)editform.FindControl("trAddNamePortfolio");
                //HtmlTableRow trEditNamePortfolio = (HtmlTableRow)editform.FindControl("trEditNamePortfolio");

                if (e.Item.RowIndex == -1)
                {
                    trRiskClassDdl.Visible = true;
                    trRiskClassTxt.Visible = false;
                    trIsRiskClassText.Visible = false;
                    trIsRiskClassDdl.Visible = true;

                    dtDefaultRiskClass = modelPortfolioBo.GetDefaultAdviserRiskClasses(advisorVo.advisorId);
                    if (dtDefaultRiskClass.Rows.Count > 0)
                    {
                        ddlPickRiskClass.DataSource = dtDefaultRiskClass;
                        ddlPickRiskClass.DataValueField = dtDefaultRiskClass.Columns["XRC_RiskClassCode"].ToString();
                        ddlPickRiskClass.DataTextField = dtDefaultRiskClass.Columns["XRC_RiskClass"].ToString();
                        ddlPickRiskClass.DataBind();
                        ddlPickRiskClass.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Risk Class", "Select Risk Class"));
                    }
                }
                else
                {
                    TextBox txtIsRiskClass = (TextBox)editform.FindControl("txtIsRiskClass");
                    if (txtIsRiskClass.Text == "Risk Profile")
                    {
                        trTimeHorizon.Visible = false;
                        trInvestmentAmount.Visible = false;
                    }
                    else
                    {
                        trTimeHorizon.Visible = true;
                        trInvestmentAmount.Visible = true;

                    }
                    trRiskClassDdl.Visible = false;
                    trRiskClassTxt.Visible = true;

                    trIsRiskClassText.Visible = true;
                    trIsRiskClassDdl.Visible = false;

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