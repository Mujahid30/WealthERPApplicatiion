using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using VoUser;
using BoUser;
using BoCommon;
using WealthERP.Base;
using BoOnlineOrderManagement;
using System.Data;
using Telerik.Web.UI;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class AMCManage : System.Web.UI.UserControl
    {
        OnlineOrderBackOfficeBo onlineorderbackofficeBO = new OnlineOrderBackOfficeBo();
        UserBo userBo = new UserBo();
        UserVo userVo = new UserVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userBo = new UserBo();
            userVo = (UserVo)Session[SessionContents.UserVo];

            BindAMCGrid();
            if (!IsPostBack)
            {

            }

        }

        public void BindAMCGrid()
        {
            DataSet dsAMCGrid;
            dsAMCGrid = onlineorderbackofficeBO.GetAMCList();
            if (Cache["gvEqTrxnMis" + userVo.UserId.ToString()] == null)
            {
                Cache.Insert("gvEqTrxnMis" + userVo.UserId.ToString(), dsAMCGrid.Tables[0]);
            }
            else
            {
                Cache.Remove("gvEqTrxnMis" + userVo.UserId.ToString());
                Cache.Insert("gvEqTrxnMis" + userVo.UserId.ToString(), dsAMCGrid.Tables[0]);
            }
            gvAMCManage.DataSource = dsAMCGrid.Tables[0];
            gvAMCManage.DataBind();
            imgexportButton.Visible = true;

        }

        protected void gvAMCManage_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet dsEqTrnxnMis = (DataSet)Cache["gvEqTrxnMis" + userVo.UserId.ToString()];

            if (dsEqTrnxnMis != null)
            {
                gvAMCManage.DataSource = dsEqTrnxnMis;
            }

        }

        protected void gvAMCManage_OnItemCommand(object source, GridCommandEventArgs e)
        {
            int result = 0;
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                TextBox txtAMCName = (TextBox)e.Item.FindControl("txtAMCName");
                CheckBox chkIsOnline = (CheckBox)e.Item.FindControl("chkIsOnline");
                TextBox txtAmcCode = (TextBox)e.Item.FindControl("txtAmcCode");
                result = onlineorderbackofficeBO.GetAMCCode(txtAmcCode.Text);
                if (result == 0)
                    onlineorderbackofficeBO.CreateAMC(txtAMCName.Text, chkIsOnline.Checked ? 1 : 0, userVo.UserId, txtAmcCode.Text);
                else
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Enter Unique Alphabets');", true);

            }
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                TextBox txtAMCName = (TextBox)e.Item.FindControl("txtAMCName");
                CheckBox chkIsOnline = (CheckBox)e.Item.FindControl("chkIsOnline");
                TextBox txtAmcCode = (TextBox)e.Item.FindControl("txtAmcCode");
                int amcCode = int.Parse(gvAMCManage.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PA_AMCCode"].ToString());
                string amccodes = onlineorderbackofficeBO.CheckAMCCode(amcCode);
                if (txtAmcCode.Text != amccodes)
                {
                    result = onlineorderbackofficeBO.GetAMCCode(txtAmcCode.Text);
                    if (result > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Enter Unique Alphabets');", true);
                    }
                    else
                    onlineorderbackofficeBO.UpdateAMC(txtAMCName.Text, chkIsOnline.Checked ? 1 : 0, userVo.UserId, amcCode, txtAmcCode.Text);

                }
                else
                onlineorderbackofficeBO.UpdateAMC(txtAMCName.Text, chkIsOnline.Checked ? 1 : 0, userVo.UserId, amcCode, txtAmcCode.Text);

            }

            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                int amcCode = int.Parse(gvAMCManage.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PA_AMCCode"].ToString());
                onlineorderbackofficeBO.deleteAMC(amcCode);
            }

            BindAMCGrid();


        }
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {


            gvAMCManage.ExportSettings.OpenInNewWindow = true;
            gvAMCManage.ExportSettings.IgnorePaging = true;
            gvAMCManage.ExportSettings.HideStructureColumns = true;
            gvAMCManage.ExportSettings.ExportOnlyData = true;
            gvAMCManage.ExportSettings.FileName = "AMC List";
            gvAMCManage.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvAMCManage.MasterTableView.ExportToExcel();

        }
    }
}