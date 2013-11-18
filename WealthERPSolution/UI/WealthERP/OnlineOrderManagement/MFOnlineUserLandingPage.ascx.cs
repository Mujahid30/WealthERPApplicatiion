using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using BoCommon;
using VoUser;
using WealthERP.Base;
using System.Web.UI.HtmlControls;
using BoOnlineOrderManagement;
using Telerik.Web.UI;

namespace WealthERP.OnlineOrderManagement
{
    public partial class MFOnlineUserLandingPage : System.Web.UI.UserControl
    {
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        int schemeplancode = 0;
        CustomerVo customerVo;
        UserVo userVo;
        protected void Page_Load(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session["userVo"];

            if (!IsPostBack)
            {
                AmcBind();
            }
        }     
       
        protected void AmcBind()
        {
            ddlAmc.Items.Clear();
            DataTable dtAmc = new DataTable();
            dtAmc = commonLookupBo.GetProdAmc();
            if (dtAmc.Rows.Count > 0)
            {
                ddlAmc.DataSource = dtAmc;
                ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAmc.DataBind();
                ddlAmc.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        public void ddlAmc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CategoryBind();
            SchemeBind(int.Parse(ddlAmc.SelectedValue), null);

        }
        public void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAmc.SelectedIndex != -1 && ddlCategory.SelectedIndex != -1)
            {
                int amcCode = int.Parse(ddlAmc.SelectedValue);
                string category = ddlCategory.SelectedValue.ToString();
                SchemeBind(amcCode, category);
            }

        }
        protected void CategoryBind()
        {
            ddlCategory.Items.Clear();
            DataSet dsCategory = new DataSet();
            dsCategory = commonLookupBo.GetAllCategoryList();
            if (dsCategory.Tables[0].Rows.Count > 0)
            {
                ddlCategory.DataSource = dsCategory.Tables[0];
                ddlCategory.DataValueField = dsCategory.Tables[0].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                ddlCategory.DataTextField = dsCategory.Tables[0].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        protected void SchemeBind(int amccode, string category)
        {
            ddlScheme.Items.Clear();
            DataTable dtScheme = new DataTable();
            dtScheme = commonLookupBo.GetAmcSchemeList(amccode, category, 0);
            if (dtScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtScheme;
                ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        private void SetParameter()
        {
            if (ddlScheme.SelectedIndex != 0)
            {
                hdnScheme.Value = ddlScheme.SelectedValue;
                ViewState["hdnScheme"] = hdnScheme.Value;
            }
            else
            {
                hdnScheme.Value = "0";
            }

        }
        protected void BindMFSchemeLanding()
        {
            DataTable dtGetMFSchemeDetailsForLanding;
            dtGetMFSchemeDetailsForLanding = OnlineMFOrderBo.GetMFSchemeDetailsForLanding(int.Parse(hdnScheme.Value));
            if (dtGetMFSchemeDetailsForLanding.Rows.Count > 0)
            {
                if (Cache["GetMFSchemeDetailsForLanding" + userVo.UserId] == null)
                {
                    Cache.Insert("GetMFSchemeDetailsForLanding" + userVo.UserId, dtGetMFSchemeDetailsForLanding);
                }
                else
                {
                    Cache.Remove("GetMFSchemeDetailsForLanding" + userVo.UserId);
                    Cache.Insert("GetMFSchemeDetailsForLanding" + userVo.UserId, dtGetMFSchemeDetailsForLanding);
                }
                gvMFSchemeLanding.DataSource = dtGetMFSchemeDetailsForLanding;
                gvMFSchemeLanding.DataBind();
                pnlMFSchemeLanding.Visible = true;
                btnExport.Visible = true;
            }
            else
            {
                gvMFSchemeLanding.DataSource = dtGetMFSchemeDetailsForLanding;
                gvMFSchemeLanding.DataBind();
                pnlMFSchemeLanding.Visible = true;
                btnExport.Visible = false;
            }

        }
        protected void btnschemlanding_Click(object sender, EventArgs e)
        {
            SetParameter();
            BindMFSchemeLanding();

        }
        protected void gvMFSchemeLanding_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            DataTable dtGetMFSchemeDetailsForLanding = new DataTable();
            dtGetMFSchemeDetailsForLanding = (DataTable)Cache["GetMFSchemeDetailsForLanding" + userVo.UserId.ToString()];
            if (dtGetMFSchemeDetailsForLanding != null)
            {
                gvMFSchemeLanding.DataSource = dtGetMFSchemeDetailsForLanding;
                gvMFSchemeLanding.Visible = true;
            }

        }
        protected void btnExportFilteredData_OnClick(object sender, EventArgs e)
        {
            gvMFSchemeLanding.ExportSettings.OpenInNewWindow = true;
            gvMFSchemeLanding.ExportSettings.IgnorePaging = true;
            gvMFSchemeLanding.ExportSettings.HideStructureColumns = true;
            gvMFSchemeLanding.ExportSettings.ExportOnlyData = true;
            gvMFSchemeLanding.ExportSettings.FileName = "Landing Page Details";
            gvMFSchemeLanding.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvMFSchemeLanding.MasterTableView.ExportToExcel();
        }
    }
}