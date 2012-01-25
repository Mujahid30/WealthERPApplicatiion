using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WealthERP.Base;
using VoUser;
using System.Data;
using BoResearch;
using Telerik.Web.UI;

namespace WealthERP.Research
{
    public partial class ResearchDashboard : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
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

            dsVariantDetails = modelPortfolioBo.GetVariantAssetPortfolioDetails(advisorVo.advisorId);

            dtVariant.Columns.Add("XAMP_ModelPortfolioCode");
            dtVariant.Columns.Add("XAMP_ModelPortfolioName");
            dtVariant.Columns.Add("XAMP_ROR");
            dtVariant.Columns.Add("XAMP_RiskPercentage");           

            DataRow drVariant;
            foreach (DataRow dr in dsVariantDetails.Tables[0].Rows)
            {
                drVariant = dtVariant.NewRow();
                drVariant["XAMP_ModelPortfolioCode"] = dr["XAMP_ModelPortfolioCode"].ToString();
                drVariant["XAMP_ModelPortfolioName"] = dr["XAMP_ModelPortfolioName"].ToString();
                drVariant["XAMP_ROR"] = dr["XAMP_ROR"].ToString();
                drVariant["XAMP_RiskPercentage"] = dr["XAMP_RiskPercentage"].ToString();                
                dtVariant.Rows.Add(drVariant);
            }
            RadGrid1.DataSource = dtVariant;
            RadGrid1.DataSourceID = String.Empty;
            RadGrid1.DataBind();
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                HyperLink link = (HyperLink)dataItem["XAMP_ModelPortfolioName"].Controls[0];
                ((e.Item as GridDataItem)["XAMP_ModelPortfolioName"].Controls[0] as HyperLink).Attributes["onclick"] = "loadcontrol('ModelPortfolioSetup', 'none')";                
            }
        }
    }
}