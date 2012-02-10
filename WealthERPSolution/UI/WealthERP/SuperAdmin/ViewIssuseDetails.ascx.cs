using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WealthERP.Base;
using BoCommon;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Text;
using VoSuperAdmin;
using BoSuperAdmin;
using DaoSuperAdmin;
using System.Globalization;
using System.Configuration;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using System.Web.UI.HtmlControls;
using VoUser;
using Telerik.Web.UI;

namespace WealthERP.SuperAdmin
{
    public partial class ViewIssuseDetails : System.Web.UI.UserControl
    {
        IssueTrackerVo superAdminCSIssueTrackerVo = new IssueTrackerVo();
        IssueTrackerBo superAdminOpsBo = new IssueTrackerBo();
        LinkButton lnkIssueCode;
        string strCsId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                csIssueGridViewBind();
                gvCSIssueTracker_Init(sender, e);
            }
           // btnSearch_Click(sender, e);
        }

        public void csIssueGridViewBind()
        { 
            DataSet ds = superAdminOpsBo.getCSIssueDetails();
            gvCSIssueTracker.DataSource = ds;
            gvCSIssueTracker.DataBind();
        }

        protected void lnkCSIssue_Click(object sender, EventArgs e)
        {
            string strActiveLevel = null;
            string strStatus = null;
            int selectedRow = 0;
            int csissueId = 0;
            lnkIssueCode = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkIssueCode.NamingContainer;
            selectedRow = gdi.ItemIndex + 1;
            csissueId = int.Parse((gvCSIssueTracker.MasterTableView.DataKeyValues[selectedRow - 1]["CSI_id"].ToString()));
            strActiveLevel = (gvCSIssueTracker.MasterTableView.DataKeyValues[selectedRow - 1]["XMLCSL_Name"].ToString());
            strStatus = (gvCSIssueTracker.MasterTableView.DataKeyValues[selectedRow - 1]["XMLCSS_Name"].ToString());
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('AddIssue','strCsId=" + csissueId + "&strActiveLevel=" + strActiveLevel + "&strStatus=" + strStatus +" ');", true);           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strSearch = null;
            strSearch = "%" + txtSearch.Text +"%";
            superAdminCSIssueTrackerVo.CSILA_Comments = strSearch.ToString();
            superAdminCSIssueTrackerVo.CSILA_RepliedBy = strSearch.ToString();
            superAdminCSIssueTrackerVo.CSILA_RepliedBy= strSearch.ToString();
            DataSet ds = superAdminOpsBo.GetSearchDetails(strSearch);            
            gvCSIssueTracker.DataSource = ds;
            gvCSIssueTracker.DataBind();
        }

        public void ConfigureExport()
        {
           // gvCSIssueTracker.ExportSettings.IgnorePaging = chkExportAll.Checked;
        }

        protected void gvCSIssueTracker_Init(object sender, System.EventArgs e)
        {
            GridFilterMenu menu = gvCSIssueTracker.FilterMenu;
            int i = 0;
            while (i < menu.Items.Count)
            {
                if (menu.Items[i].Text == "NoFilter" || menu.Items[i].Text == "Contains" || menu.Items[i].Text == "EqualTo" || menu.Items[i].Text == "GreaterThan" || menu.Items[i].Text == "LessThan")
                {
                    i++;
                }
                else
                {
                    menu.Items.RemoveAt(i);
                }
            }
        }

    }
}