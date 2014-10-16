using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using WealthERP.Base;
using BoCommon;
using BoCommisionManagement;
using VoCommisionManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using System.Data;

namespace WealthERP.CommisionManagement
{
    public partial class SchemeStructureRuleAssociation : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        RMVo rmVo;
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session["rmVo"];

            //ibtExportSummary.Visible = false;
            if (!Page.IsPostBack)
            {
                GetProduct();

                BindSchemeStructureRuleGrid(advisorVo.advisorId);

            }

        }

        protected void GetProduct()
        {
            DataSet dsproduct;
            dsproduct = commisionReceivableBo.GetProduct(advisorVo.advisorId);

            ddlProductType.DataSource = dsproduct.Tables[0];
            ddlProductType.DataValueField = dsproduct.Tables[0].Columns["PAG_AssetGroupCode"].ToString();
            ddlProductType.DataTextField = dsproduct.Tables[0].Columns["PAG_AssetGroupName"].ToString();
            ddlProductType.DataBind();
            ddlProductType.SelectedValue = "MF";
            BindSchemeStructureRuleGrid(advisorVo.advisorId);
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            BindSchemeStructureRuleGrid(advisorVo.advisorId);
        }

        private void BindSchemeStructureRuleGrid(int adviserId)
        {
            DataSet dsSchemeStructureRule = commisionReceivableBo.GetCommissionSchemeStructureRuleList(adviserId, ddlProductType.SelectedValue);
            RadGridSchemeRule.Visible = false;
            RadGridIssueStructureRule.Visible = false;

            if (ddlProductType.SelectedValue == "MF")
            {
                RadGridSchemeRule.Visible = true;
                RadGridSchemeRule.DataSource = dsSchemeStructureRule.Tables[0];
                RadGridSchemeRule.DataBind();
            }
            else
            {
                RadGridIssueStructureRule.Visible = true;
                RadGridIssueStructureRule.DataSource = dsSchemeStructureRule.Tables[0];
                RadGridIssueStructureRule.DataBind();
            }
            Cache.Insert(userVo.UserId.ToString() + "SchemeStructureRule", dsSchemeStructureRule);
            if (dsSchemeStructureRule.Tables[0].Rows.Count > 0)
            {
                ibtExport.Visible = true;
            }
        }

        protected void RadGridStructureRule_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet dsSchemeStructureRule = new DataSet();
            if (Cache[userVo.UserId.ToString() + "SchemeStructureRule"] != null)
            {
                dsSchemeStructureRule = (DataSet)Cache[userVo.UserId.ToString() + "SchemeStructureRule"];
                RadGridSchemeRule.DataSource = dsSchemeStructureRule.Tables[0];
            }
        }

        protected void RadGridIssueStructureRule_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet dsSchemeStructureRule = new DataSet();
            if (Cache[userVo.UserId.ToString() + "SchemeStructureRule"] != null)
            {
                dsSchemeStructureRule = (DataSet)Cache[userVo.UserId.ToString() + "SchemeStructureRule"];
                RadGridIssueStructureRule.DataSource = dsSchemeStructureRule.Tables[0];
            }
        }

        protected void ibtExport_OnClick(object sender, ImageClickEventArgs e)
        {
            RadGridSchemeRule.ExportSettings.OpenInNewWindow = true;
            RadGridSchemeRule.ExportSettings.IgnorePaging = true;
            RadGridSchemeRule.ExportSettings.HideStructureColumns = true;
            RadGridSchemeRule.ExportSettings.ExportOnlyData = true;
            RadGridSchemeRule.ExportSettings.FileName = "ViewSchemeRule";
            RadGridSchemeRule.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            RadGridSchemeRule.MasterTableView.ExportToExcel();

        }

    }
}