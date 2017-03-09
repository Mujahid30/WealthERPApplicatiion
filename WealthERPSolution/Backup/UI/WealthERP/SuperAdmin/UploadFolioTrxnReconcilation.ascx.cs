using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoSuperAdmin;
using VoUser;
using Telerik.Web.UI;

namespace WealthERP.SuperAdmin
{
    public partial class UploadFolioTrxnReconcilation : System.Web.UI.UserControl
    {
        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();
        UserVo userVo = new UserVo();
        int adviserId=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlTransaction.Visible = false;
            pnlFolioRecon.Visible = false;
            imgBtnTransaction.Visible = false;
            btnFolio.Visible = false;
            pnlCustomer.Visible = false;
            pnlsystematic.Visible = false;
            pnlTrail.Visible = false;
            imgbtnCustomer.Visible = false;
            txtFromDate.SelectedDate = DateTime.Now;
            txtToDate.SelectedDate = DateTime.Now;
            if (!IsPostBack)
            {
                BindAdviserDropDownList();
            }
        }

        private void BindAdviserDropDownList()
        {
            DataTable dtAdviserList = new DataTable();
            dtAdviserList = superAdminOpsBo.BindAdviserForUpload();

            if (dtAdviserList.Rows.Count > 0)
            {
                ddlAdviser.DataSource = dtAdviserList;
                ddlAdviser.DataTextField = "A_OrgName";
                ddlAdviser.DataValueField = "A_AdviserId";
                ddlAdviser.DataBind();
            }
            ddlAdviser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }
        /// <summary>
        ///  Exporting Uploded Folio details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFolio_Click(object sender, ImageClickEventArgs e)
        {
            gvFolioRecon.ExportSettings.OpenInNewWindow = true;
            gvFolioRecon.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvFolioRecon.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvFolioRecon.MasterTableView.ExportToExcel();
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (ddlAdviser.SelectedIndex != 0)
                adviserId = int.Parse(ddlAdviser.SelectedValue);
            BindFolioTransactionGrid();
        }
        /// <summary>
        ///  Get Folio and Transaction details of Uploaded data
        /// </summary>
        private void BindFolioTransactionGrid()
        {
            DataSet dsFoliotransaction;
            DataTable dt;
            dsFoliotransaction = superAdminOpsBo.UploadFolioTransactionReconcilation(adviserId,Convert.ToDateTime(txtFromDate.SelectedDate),Convert.ToDateTime(txtToDate.SelectedDate));
            if (ddlType.SelectedValue == "Folio")
            {
                pnlFolioRecon.Visible = true;
                btnFolio.Visible = true;
                pnlTransaction.Visible = false;
                pnlTrail.Visible = false;
                pnlsystematic.Visible = false;
                imgBtnTransaction.Visible = false;
                pnlCustomer.Visible = false;
                imgbtnCustomer.Visible = false;
                dt = dsFoliotransaction.Tables[0];
                if (dt != null)
                {
                    gvFolioRecon.DataSource = dt;
                    gvFolioRecon.DataBind();
                    gvFolioRecon.Visible = true;
                    if (Cache["gvFolioRecon" + userVo.UserId] == null)
                    {
                        Cache.Insert("gvFolioRecon" + userVo.UserId, dt);
                    }
                    else
                    {
                        Cache.Remove("gvFolioRecon" + userVo.UserId);
                        Cache.Insert("gvFolioRecon" + userVo.UserId, dt);
                    }
                }
                else
                {
                    gvFolioRecon.DataSource = null;
                    gvFolioRecon.DataBind();
                }
            }

            if (ddlType.SelectedValue == "Tranx")
            {
                pnlFolioRecon.Visible = false;
                btnFolio.Visible = false;
                pnlTransaction.Visible = true;
                pnlsystematic.Visible = true;
                pnlTrail.Visible = false;
                imgBtnTransaction.Visible = true;
                pnlCustomer.Visible = false;
                imgbtnCustomer.Visible = false;
                dt = dsFoliotransaction.Tables[1];
                if (dt != null)
                {
                    gvTransaction.DataSource = dt;
                    gvTransaction.DataBind();
                    gvTransaction.Visible = true;
                    if (Cache["gvTransaction" + userVo.UserId] == null)
                    {
                        Cache.Insert("gvTransaction" + userVo.UserId, dt);
                    }
                    else
                    {
                        Cache.Remove("gvTransaction" + userVo.UserId);
                        Cache.Insert("gvTransaction" + userVo.UserId, dt);
                    }
                }
                else
                {
                    gvTransaction.DataSource = null;
                    gvTransaction.DataBind();
                }
            }
            if (ddlType.SelectedValue == "Customer")
            {
                pnlFolioRecon.Visible = false;
                btnFolio.Visible = false;
                pnlTransaction.Visible = false;
                pnlTrail.Visible = false;
                pnlsystematic.Visible = false;
                imgBtnTransaction.Visible = false;
                pnlCustomer.Visible = true;
                imgbtnCustomer.Visible = true;
                dt = dsFoliotransaction.Tables[2];
                if (dt != null)
                {
                    gvCustomer.DataSource = dt;
                    gvCustomer.DataBind();
                    gvCustomer.Visible = true;
                    if (Cache["gvCustomer" + userVo.UserId] == null)
                    {
                        Cache.Insert("gvCustomer" + userVo.UserId, dt);
                    }
                    else
                    {
                        Cache.Remove("gvCustomer" + userVo.UserId);
                        Cache.Insert("gvCustomer" + userVo.UserId, dt);
                    }
                }
                else
                {
                    gvCustomer.DataSource = null;
                    gvCustomer.DataBind();
                }
            }
            if (ddlType.SelectedValue == "Systematic")
            {
                pnlFolioRecon.Visible = false;
                btnFolio.Visible = false;
                pnlTransaction.Visible = false;
                pnlTrail.Visible = false;
                imgBtnTransaction.Visible = false;
                pnlCustomer.Visible = false;
                pnlsystematic.Visible = true;
                imgbtnCustomer.Visible = true;
                dt = dsFoliotransaction.Tables[3];
                if (dt != null)
                {
                    gvSystematic.DataSource = dt;
                    gvSystematic.DataBind();
                    gvSystematic.Visible = true;
                    if (Cache["gvSystematic" + userVo.UserId] == null)
                    {
                        Cache.Insert("gvSystematic" + userVo.UserId, dt);
                    }
                    else
                    {
                        Cache.Remove("gvSystematic" + userVo.UserId);
                        Cache.Insert("gvSystematic" + userVo.UserId, dt);
                    }
                }
                else
                {
                    gvSystematic.DataSource = null;
                    gvSystematic.DataBind();
                }
            }
            if (ddlType.SelectedValue == "Trail")
            {
                pnlFolioRecon.Visible = false;
                btnFolio.Visible = false;
                pnlTransaction.Visible = false;
                pnlTrail.Visible = true;
                pnlsystematic.Visible = false;
                imgBtnTransaction.Visible = false;
                pnlCustomer.Visible = false;              
                imgbtnCustomer.Visible = true;
                dt = dsFoliotransaction.Tables[4];
                if (dt != null)
                {
                    gvTrail.DataSource = dt;
                    gvTrail.DataBind();
                    gvTrail.Visible = true;
                    if (Cache["gvTrail" + userVo.UserId] == null)
                    {
                        Cache.Insert("gvTrail" + userVo.UserId, dt);
                    }
                    else
                    {
                        Cache.Remove("gvTrail" + userVo.UserId);
                        Cache.Insert("gvTrail" + userVo.UserId, dt);
                    }
                }
                else
                {
                    gvTrail.DataSource = null;
                    gvTrail.DataBind();
                }
            }

        }
        /// <summary>
        /// Exporting Upploaded Transaction Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnTransaction_Click(object sender, ImageClickEventArgs e)
        {
            gvTransaction.ExportSettings.OpenInNewWindow = true;
            gvTransaction.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvTransaction.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvTransaction.MasterTableView.ExportToExcel();
        }
        protected void gvFolioRecon_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Cache["gvFolioRecon" + userVo.UserId];
            gvFolioRecon.DataSource = dt;
            pnlFolioRecon.Visible = true;
            gvFolioRecon.Visible = true;
            btnFolio.Visible = true;
        }
        protected void gvTransaction_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Cache["gvTransaction" + userVo.UserId];
            gvTransaction.DataSource = dt;
            pnlTransaction.Visible = true;
            gvTransaction.Visible = true;
            imgBtnTransaction.Visible = true;
        }
        protected void gvCustomer_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Cache["gvCustomer" + userVo.UserId];
            gvCustomer.DataSource = dt;
            pnlCustomer.Visible = true;
            gvCustomer.Visible = true;
            imgBtnTransaction.Visible = true;
        }

        protected void gvSystematic_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Cache["gvSystematic" + userVo.UserId];
            gvSystematic.DataSource = dt;
            pnlsystematic.Visible = true;
            gvSystematic.Visible = true;
            imgBtnTransaction.Visible = true;
        }
        protected void gvTrail_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Cache["gvTrail" + userVo.UserId];
            gvTrail.DataSource = dt;
            pnlTrail.Visible = true;
            gvTrail.Visible = true;
            imgBtnTransaction.Visible = true;
        }
        protected void imgbtnCustomer_Click(object sender, ImageClickEventArgs e)
        {
            gvCustomer.ExportSettings.OpenInNewWindow = true;
            gvCustomer.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvCustomer.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvCustomer.MasterTableView.ExportToExcel();
        }
    }

}