using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using WealthERP.Base;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;


using VoUser;
using BoCommon;

using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineNCDIssueList : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        OnlineNCDBackOfficeVo onlineNCDBackOfficeVo;
        string issuerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];

            if (!IsPostBack)
            {
                string type = "";
                string product = "";
                DateTime date = DateTime.MinValue;
                if (Request.QueryString["action"] != null)
                {

                    type = Request.QueryString["type"].ToString();
                    product = Request.QueryString["product"].ToString();
                    date = Convert.ToDateTime(Request.QueryString["date"].ToString());
                    string category = Request.QueryString["category"].ToString();
                    ddlType.SelectedValue = type;
                    if (ddlType.SelectedValue == "Bonds")
                    {
                        tdCategorydropdown.Visible = true;
                        tdcategory.Visible = true;
                    }
                    txtDate.SelectedDate = DateTime.Now;
                    ddlProduct.SelectedValue = category;
                    BindNcdCategory();
                    ddlCategory.SelectedValue = product;
                    pnlIssueList.Visible = true;
                    BindViewListGrid(GetType(type), date, category);
                }

            }
        }
        private int GetType(string ddlSelection)
        {
            int type = 0;
            if (ddlSelection == "Curent")
            {
                type = 1;
            }
            else if (ddlSelection == "Closed")
            {
                type = 2;
            }
            else
            {
                type = 3;
            }
            return type;
        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            int type = GetType(ddlType.SelectedValue);
            BindViewListGrid(type, DateTime.Now, ddlProduct.SelectedValue);
            pnlIssueList.Visible = true;
            btnNcdIpoExport.Visible = true;
        }
        private void BindViewListGrid(int type, DateTime date, string product)
        {
            try
            {
                if (ddlProduct.SelectedValue == "Bonds")
                    product = ddlCategory.SelectedValue;
                else
                    product = "FIFIIP";
                DataTable dtIssueList = new DataTable();
                dtIssueList = onlineNCDBackOfficeBo.GetAdviserIssueList(DateTime.Now, type, product, advisorVo.advisorId).Tables[0];
                gvIssueList.DataSource = dtIssueList;
                gvIssueList.DataBind();
                if (Cache[userVo.UserId.ToString() + "IssueList"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "IssueList");
                Cache.Insert(userVo.UserId.ToString() + "IssueList", dtIssueList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindViewListGrid()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        //btnNcdIpoExport_Click
        protected void btnNcdIpoExport_Click(object sender, ImageClickEventArgs e)
        {
            gvIssueList.ExportSettings.OpenInNewWindow = true;
            gvIssueList.ExportSettings.IgnorePaging = true;
            gvIssueList.ExportSettings.HideStructureColumns = true;
            gvIssueList.ExportSettings.ExportOnlyData = true;
            gvIssueList.ExportSettings.FileName = "Issue List";
            gvIssueList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvIssueList.MasterTableView.ExportToExcel();

        }
        protected void gvIssueList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtEligibleInvestorCategories = new DataTable();
            dtEligibleInvestorCategories = (DataTable)Cache[userVo.UserId.ToString() + "IssueList"];

            if (dtEligibleInvestorCategories != null)
            {
                gvIssueList.DataSource = dtEligibleInvestorCategories;
            }

        }
        protected void lnkIssueNo_Click(object sender, EventArgs e)
        {
            LinkButton lnkOrderNo = (LinkButton)sender;
            GridDataItem gdi;
            string producttype = string.Empty;
            gdi = (GridDataItem)lnkOrderNo.NamingContainer;
            int selectedRow = gdi.ItemIndex + 1;
            int issueNo = int.Parse((gvIssueList.MasterTableView.DataKeyValues[selectedRow - 1]["AIM_IssueId"].ToString()));
             if (ddlProduct.SelectedValue == "Bonds")
                    producttype = ddlCategory.SelectedValue;
                else
                 producttype = "FIFIIP";
             ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineNCDIssueSetup", "loadcontrol('OnlineNCDIssueSetup','action=viewIsssueList&issueNo=" + issueNo + "&type=" + ddlType.SelectedValue + "&date=" + DateTime.Now + "&product=" + producttype + "&category=" + ddlProduct.SelectedValue + "');", true);

        }
        private void BindNcdCategory()
        {
            DataTable dtCategory = new DataTable();
            dtCategory = onlineNCDBackOfficeBo.BindNcdCategory("SubInstrumentCat", "").Tables[0];
            if (dtCategory.Rows.Count > 0)
            {
                ddlCategory.DataSource = dtCategory;
                ddlCategory.DataValueField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlCategory.DataTextField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlCategory.DataBind();
            }
            ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));
        }
        protected void ddlProduct_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            tdCategorydropdown.Visible = false;
            tdcategory.Visible = false;
            if (ddlProduct.SelectedValue == "Bonds")
            {
                tdCategorydropdown.Visible = true;
                tdcategory.Visible = true;
                BindNcdCategory();
            }
        }
    }
}