using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCommon;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;

namespace WealthERP.OnlineOrderManagement
{
    public partial class MFOrderRdemptionTransType : System.Web.UI.UserControl
    {

        CommonLookupBo commonLookupBo = new CommonLookupBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            AmcBind();
            


        }
        protected void AmcBind()
        {
            DataTable dtAmc = new DataTable();
            dtAmc = commonLookupBo.GetProdAmc();
            if (dtAmc.Rows.Count > 0)
            {
                ddlAmc.DataSource = dtAmc;
                ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAmc.DataBind();

            }
        }
        protected void ddlAmc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CategoryBind(ddlAmc.SelectedValue);
            SchemeBind(ddlAmc.SelectedValue, ddlCategory.SelectedValue);
        }
        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void CategoryBind(string amccode)
        {
            DataTable dtCategory = new DataTable();
            dtCategory = commonLookupBo.GetCategoryList(amccode, null);
            if (dtCategory.Rows.Count > 0)
            {
                ddlCategory.DataSource = dtCategory;
                ddlCategory.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                ddlCategory.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                ddlCategory.DataBind();

            }
        }

        protected void SchemeBind(string amccode, string category)
        {
            DataTable dtScheme = new DataTable();
            dtScheme = commonLookupBo.GetAmcSchemeList(amccode, category);
            if (dtScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtScheme;
                ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlCategory.DataBind();
            }
        }
    }
}