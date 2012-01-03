using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using System.Data;
using BoResearch;
using Telerik.Web.UI;

namespace WealthERP.Research
{
    public partial class AssetMapping : System.Web.UI.UserControl
    {
        AdvisorVo adviserVo = new AdvisorVo();
        AdviserFPConfigurationBo adviserFPConfigurationBo = new AdviserFPConfigurationBo();
        int expiryAge = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            adviserVo = (AdvisorVo)Session["AdvisorVo"];
            BindAssetClassificationMapping();
        }

        public void BindAssetClassificationMapping()
        {
            int i;
            DataSet dsAssetClassification;
            dsAssetClassification = adviserFPConfigurationBo.GetAssetClassificationMapping();
            DataTable dtAssetClassification = new DataTable();
            decimal equityPer = 0;
            decimal debtPer = 0;
            decimal cashPer = 0;
            decimal alternatePer = 0;

            dtAssetClassification.Columns.Add("Name");
            dtAssetClassification.Columns.Add("Equity");
            dtAssetClassification.Columns.Add("Debt");
            dtAssetClassification.Columns.Add("Cash");
            dtAssetClassification.Columns.Add("Alternate");    
      
            DataRow drAssumption;
            foreach (DataRow drAssetClassification in dsAssetClassification.Tables[0].Rows)
            {

                drAssumption = dtAssetClassification.NewRow();
                if (Convert.ToDecimal(drAssetClassification["PercentageAllocation"].ToString()) == 100)
                {
                    drAssumption["Name"] = drAssetClassification["Name"].ToString();
                    switch (Convert.ToInt32(drAssetClassification["WAC_AssetClassificationCode"].ToString()))
                    {
                        case 1:
                            {
                                drAssumption["Equity"] = drAssetClassification["PercentageAllocation"];
                                drAssumption["Debt"] = "-";
                                drAssumption["Cash"] = "-";
                                drAssumption["Alternate"] = "-";
                                break;
                            }
                        case 2:
                            {
                                drAssumption["Equity"] = "-";
                                drAssumption["Debt"] = drAssetClassification["PercentageAllocation"];
                                drAssumption["Cash"] = "-";
                                drAssumption["Alternate"] = "-";
                                break;
                            }
                        case 3:
                            {
                                drAssumption["Equity"] = "-";
                                drAssumption["Debt"] = "-";
                                drAssumption["Cash"] = drAssetClassification["PercentageAllocation"];
                                drAssumption["Alternate"] = "-";
                                break;
                            }
                        case 4:
                            {
                                drAssumption["Equity"] = "-";
                                drAssumption["Debt"] = "-";
                                drAssumption["Cash"] = "-";
                                drAssumption["Alternate"] = drAssetClassification["PercentageAllocation"];
                                break;
                            }
                    }
                    dtAssetClassification.Rows.Add(drAssumption);

                }
                else
                {
                    drAssumption["Name"] = drAssetClassification["Name"].ToString();
                    switch (Convert.ToInt32(drAssetClassification["WAC_AssetClassificationCode"].ToString()))
                    {
                        case 1:
                            {
                                equityPer = Convert.ToDecimal(drAssetClassification["PercentageAllocation"].ToString());
                                break;
                            }
                        case 2:
                            {
                                debtPer = Convert.ToDecimal(drAssetClassification["PercentageAllocation"].ToString());
                                break;
                            }
                        case 3:
                            {
                                cashPer = Convert.ToDecimal(drAssetClassification["PercentageAllocation"].ToString());
                                break;
                            }
                        case 4:
                            {
                                alternatePer = Convert.ToDecimal(drAssetClassification["PercentageAllocation"].ToString());
                                break;
                            }
                    }

                    if ((equityPer + debtPer + cashPer + alternatePer) == 100)
                    {
                        if (equityPer != 0)
                            drAssumption["Equity"] = equityPer;
                        else
                            drAssumption["Equity"] = "-";
                        if (debtPer!=0)
                           drAssumption["Debt"] = debtPer;
                        else
                            drAssumption["Debt"] = "-";
                        if (cashPer!=0)
                            drAssumption["Cash"] = cashPer;
                        else
                            drAssumption["Cash"] = "-";
                        if (alternatePer!=0)
                            drAssumption["Alternate"] = alternatePer;
                        else
                            drAssumption["Alternate"] = "-";

                        dtAssetClassification.Rows.Add(drAssumption);

                        equityPer = 0;
                        debtPer = 0;
                        cashPer = 0;
                        alternatePer = 0;
                    }
                }





                //i = dsAssetClassification.Tables[0].Rows.IndexOf(drAssetClassification);
                //drAssumption = dtAssetClassification.NewRow();
                //drAssumption["Name"] = drAssetClassification["Name"].ToString();
                //if ((decimal.Parse(dsAssetClassification.Tables[0].Rows[i]["PercentageAllocation"].ToString()) == 100) && (decimal.Parse(dsAssetClassification.Tables[0].Rows[i]["ClassificationCode"].ToString()) == 1))
                //{
                //    drAssumption["Equity"] = dsAssetClassification.Tables[0].Rows[i]["PercentageAllocation"].ToString();
                //    drAssumption["Debt"] = "-";
                //    drAssumption["Cash"] = "-";
                //    drAssumption["Alternate"] = "-";
                //}
                //else if ((decimal.Parse(dsAssetClassification.Tables[0].Rows[i]["PercentageAllocation"].ToString()) == 100) && (decimal.Parse(dsAssetClassification.Tables[0].Rows[i]["ClassificationCode"].ToString()) == 2))
                //{
                //    drAssumption["Equity"] = "-";
                //    drAssumption["Debt"] = dsAssetClassification.Tables[0].Rows[i]["PercentageAllocation"].ToString();
                //    drAssumption["Cash"] = "-";
                //    drAssumption["Alternate"] = "-";
                //}
                //else if ((decimal.Parse(dsAssetClassification.Tables[0].Rows[i]["PercentageAllocation"].ToString()) == 100) && (decimal.Parse(dsAssetClassification.Tables[0].Rows[i]["ClassificationCode"].ToString()) == 3))
                //{
                //    drAssumption["Equity"] = "-";
                //    drAssumption["Debt"] = "-";
                //    drAssumption["Cash"] = dsAssetClassification.Tables[0].Rows[i]["PercentageAllocation"].ToString();
                //    drAssumption["Alternate"] = "-";
                //}
                //else if ((decimal.Parse(dsAssetClassification.Tables[0].Rows[i]["PercentageAllocation"].ToString()) == 100) && (decimal.Parse(dsAssetClassification.Tables[0].Rows[i]["ClassificationCode"].ToString()) == 4))
                //{
                //    drAssumption["Equity"] = "-";
                //    drAssumption["Debt"] = "-";
                //    drAssumption["Cash"] = "-";
                //    drAssumption["Alternate"] = dsAssetClassification.Tables[0].Rows[i]["PercentageAllocation"].ToString();

                //}
                //dtAssetClassification.Rows.Add(drAssumption);
            }
        
            RadGrid1.DataSource = dtAssetClassification;
            RadGrid1.DataBind();
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            //decimal value = 0;
            //if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
            //{
            //    GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
            //    editColumn.Visible = false;
            //}
            //else if (e.CommandName == RadGrid.RebindGridCommandName && e.Item.OwnerTableView.IsItemInserted)
            //{
            //    e.Canceled = true;
            //}
            //else if (e.CommandName == RadGrid.UpdateCommandName)
            //{
            //    GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
            //    TextBox txt = (TextBox)e.Item.FindControl("txtAssumptionValue");
            //    value = decimal.Parse(txt.Text);
            //    string assumptionId = (RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WA_AssumptionId"].ToString());
            //    adviserFPConfigurationBo.UpdateAdviserAssumptions(adviserVo.advisorId, value, assumptionId);
            //    BindAssetClassificationMapping();
            //}
            //else
            //{
            //    GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
            //    if (!editColumn.Visible)
            //        editColumn.Visible = true;
            //}
        }        
    }
}