using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using BoWerpAdmin;
using Telerik.Web.UI;
using BoUser;
using VoUser;
using BoCommisionManagement;
using BoCommon;
using VoCommisionManagement;
using WealthERP.Base;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;



namespace WealthERP.CommisionManagement
{
    public partial class ViewSchemeStructureAssociation : System.Web.UI.UserControl
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

            if (!IsPostBack)
            {
                BindStructureSchemeGrid();
            }
        }

        private void BindStructureSchemeGrid()
        {
            try
            {
                DataSet dsStructToScheme = commisionReceivableBo.GetStructureScheme(advisorVo.advisorId);
                if (dsStructToScheme.Tables[0].Rows.Count > 0)
                {
                    ibtExportSummary.Visible = true;
                    rgvSchemeToStruct.DataSource = dsStructToScheme.Tables[0];
                    rgvSchemeToStruct.DataBind();
                    Cache.Insert(userVo.UserId.ToString() + "AssocSchemes", dsStructToScheme.Tables[0]);
                }
                else
                {
                    ibtExportSummary.Visible = false;
                    rgvSchemeToStruct.DataSource = null;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewSchemeStructureAssociation.ascx:BindStructureSchemeGrid()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void rgvSchemeToStruct_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dtAssocSchemes = new DataTable();
            if (Cache[userVo.UserId.ToString() + "AssocSchemes"] != null)
            {
                dtAssocSchemes = (DataTable)Cache[userVo.UserId.ToString() + "AssocSchemes"];
                rgvSchemeToStruct.DataSource = dtAssocSchemes;
            }
        }

        protected void ibtExportSummary_OnClick(object sender, ImageClickEventArgs e)
        {
            rgvSchemeToStruct.ExportSettings.OpenInNewWindow = true;
            rgvSchemeToStruct.ExportSettings.IgnorePaging = true;
            rgvSchemeToStruct.ExportSettings.HideStructureColumns = true;
            rgvSchemeToStruct.ExportSettings.ExportOnlyData = true;
            rgvSchemeToStruct.ExportSettings.FileName = "ViewSchemeStructureAssociation";
            rgvSchemeToStruct.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rgvSchemeToStruct.MasterTableView.ExportToExcel();
        }

        protected void rgvSchemeToStruct_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;
                LinkButton lnkStruct = item.FindControl("lbtStruct") as LinkButton;
                string structName = ((DataRowView)e.Item.DataItem).Row["ACSM_CommissionStructureName"].ToString();
                lnkStruct.Text = structName;
                lnkStruct.ToolTip = structName;
            }
        }

        protected void lbtStruct_Click(object sender, EventArgs e)
        {
            LinkButton lnkStruct = (LinkButton)sender;
            GridDataItem item = (GridDataItem)lnkStruct.NamingContainer;
            int structureId = int.Parse(rgvSchemeToStruct.MasterTableView.DataKeyValues[item.ItemIndex]["StructureId"].ToString());

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('ReceivableSetup','StructureId=" + structureId + "');", true);
        }
    }
}