using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VoUser;
using BoFPSuperlite;


namespace WealthERP.FP
{
    public partial class CustomerFPProjections : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        int customerAge = 0;
        CustomerFPAnalyticsBo customerFPAnalyticsBo = new CustomerFPAnalyticsBo();
        DataTable dtFutureSurplusEngine;
        protected void Page_Load(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];

            if (!Page.IsPostBack)
            {
                dtFutureSurplusEngine = customerFPAnalyticsBo.FutureSurplusEngine(customerVo.CustomerId);
                BindYearDropDowns();
                BindCustomerProjectedAssumption();
                
            }

        }

        private void BindYearDropDowns()
        {
            int lifeExpentancy=80;
            if (customerVo.Dob != DateTime.MinValue)
                customerAge = DateTime.Now.Year - customerVo.Dob.Year;

            int customerLife = lifeExpentancy - customerAge;
            int lifeLastYear = DateTime.Now.Year + customerLife;

            int currentYear = DateTime.Now.Year;

            for (; currentYear <= lifeLastYear; currentYear++)
            {
                ddlPickYear.Items.Add(currentYear.ToString());
                ddlFromYear.Items.Add(currentYear.ToString());
                ddlToYear.Items.Add(currentYear.ToString());
                
            }
            ddlPickYear.Items.Insert(0, new ListItem("Select", "Select"));
            ddlFromYear.Items.Insert(0, new ListItem("Select", "Select"));
            ddlToYear.Items.Insert(0, new ListItem("Select", "Select"));
        }
        private void BindCustomerProjectedAssumption()
        {
            DataSet dsCustomerProjectedAssetAllocation;
            DataTable dtCustomerProjectedAssetAllocation;
            dsCustomerProjectedAssetAllocation = customerFPAnalyticsBo.GetCustomerProjectedAssetAllocation(customerVo.CustomerId);
            dtCustomerProjectedAssetAllocation=CreateAssetAllocationTable(dsCustomerProjectedAssetAllocation.Tables[0]);
            gvAssetAllocation.DataSource = dtCustomerProjectedAssetAllocation;
            gvAssetAllocation.DataBind();

        }
        protected DataTable CreateAssetAllocationTable(DataTable dtAssetAllocation)
        {
            DataTable dtCustomerProjectedAssetAllocation = new DataTable();
            dtCustomerProjectedAssetAllocation.Columns.Add("Year");
            dtCustomerProjectedAssetAllocation.Columns.Add("Rec_Equity");
            dtCustomerProjectedAssetAllocation.Columns.Add("Rec_Debt");
            dtCustomerProjectedAssetAllocation.Columns.Add("Rec_Cash");
            dtCustomerProjectedAssetAllocation.Columns.Add("Rec_Alternate");

            dtCustomerProjectedAssetAllocation.Columns.Add("Agr_Equity");
            dtCustomerProjectedAssetAllocation.Columns.Add("Agr_Debt");
            dtCustomerProjectedAssetAllocation.Columns.Add("Agr_Cash");
            dtCustomerProjectedAssetAllocation.Columns.Add("Agr_Alternate");

            DataRow drCustomerProjectedAssetAllocation;
            int tempYear = 0;
            int assetClassificationCode = 0;
            DataRow[] drAssetallocationYearWise;
            foreach (DataRow drAssetAllocation in dtAssetAllocation.Rows)
            {
                tempYear = int.Parse(drAssetAllocation["CAA_Year"].ToString());
                //drFinalAssumption["Year"] = tempYear.ToString();
                drAssetallocationYearWise = dtAssetAllocation.Select("CAA_Year=" + tempYear.ToString());
                drCustomerProjectedAssetAllocation = dtCustomerProjectedAssetAllocation.NewRow();
               
                foreach (DataRow dr in drAssetallocationYearWise)
                {
                    assetClassificationCode = int.Parse(dr["WAC_AssetClassificationCode"].ToString());
                    switch (assetClassificationCode)
                    {
                        case 1:
                            {
                                drCustomerProjectedAssetAllocation["Rec_Equity"] = dr["CAA_RecommendedPercentage"].ToString();
                                drCustomerProjectedAssetAllocation["Agr_Equity"]=decimal.Parse(dr["CAA_RecommendedPercentage"].ToString())+decimal.Parse((dr["CAA_AgreedAdjustment"].ToString()));
                                break;
                            }
                        case 2:
                            {
                                drCustomerProjectedAssetAllocation["Rec_Debt"] = dr["CAA_RecommendedPercentage"].ToString();
                                drCustomerProjectedAssetAllocation["Agr_Debt"] = decimal.Parse(dr["CAA_RecommendedPercentage"].ToString()) + decimal.Parse(dr["CAA_AgreedAdjustment"].ToString());
                                break;

                            }
                        case 3:
                            {
                                drCustomerProjectedAssetAllocation["Rec_Cash"] = dr["CAA_RecommendedPercentage"].ToString();
                                drCustomerProjectedAssetAllocation["Agr_Cash"] = decimal.Parse(dr["CAA_RecommendedPercentage"].ToString()) + decimal.Parse((dr["CAA_AgreedAdjustment"].ToString()));
                                break;
                            }
                        case 4:
                            {
                                drCustomerProjectedAssetAllocation["Rec_Alternate"] = dr["CAA_RecommendedPercentage"].ToString();
                                drCustomerProjectedAssetAllocation["Agr_Alternate"] = decimal.Parse(dr["CAA_RecommendedPercentage"].ToString()) + decimal.Parse((dr["CAA_AgreedAdjustment"].ToString()));
                                break;
                            }

                    }

                    drCustomerProjectedAssetAllocation["Year"] = tempYear.ToString();
 
                }

                dtCustomerProjectedAssetAllocation.Rows.Add(drCustomerProjectedAssetAllocation);
            }



            return dtCustomerProjectedAssetAllocation;
 
        }

        protected void gvAssetAllocation_PreRender(object sender, EventArgs e)
        {
            gvAssetAllocation.UseAccessibleHeader = true;
            gvAssetAllocation.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

    }
}