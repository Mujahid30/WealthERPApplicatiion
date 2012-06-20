using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoOps;
using Telerik.Web.UI;
using VoUser;
using BoCommon;
using BoUploads;
using WealthERP.Base;
using BoAdvisorProfiling;

namespace WealthERP.OPS
{
    public partial class OrderList : System.Web.UI.UserControl
    {
        OrderBo orderbo = new OrderBo();
        RMVo rmVo = new RMVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorVo advisorVo;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];

            //rmVo = (RMVo)Session[SessionContents.RmVo];
            //int bmID = rmVo.RMId;
            gvOrderList.Visible = false;

            if (!IsPostBack)
            {
                BindBranchDropDown();
                BindRMDropDown();
            }
        }

        private void BindBranchDropDown()
        {
            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;

            UploadCommonBo uploadsCommonDao = new UploadCommonBo();
            DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
            if (ds != null)
            {
                ddlBranch.DataSource = ds;
                ddlBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                ddlBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                ddlBranch.DataBind();
            }
            ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bmID = rmVo.RMId;
            if (ddlBranch.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0);
            }
        }

        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        {
            DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
            if (ds != null)
            {
                ddlRM.DataSource = ds.Tables[0]; ;
                ddlRM.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                ddlRM.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
                ddlRM.DataBind();
            }
            ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }

        private void BindRMDropDown()
        {
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
            if (dt.Rows.Count > 0)
            {
                ddlRM.DataSource = dt;
                ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
                ddlRM.DataTextField = dt.Columns["RMName"].ToString();
                ddlRM.DataBind();
            }
            ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "2"));
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {            
            SetParameters();
            BindGvOrderList();
        }

        private void SetParameters()
        {
            if (ddlBranch.SelectedIndex != 0)
                hdnBranchId.Value = ddlBranch.SelectedValue;
            else
                hdnBranchId.Value = "";

            if (ddlRM.SelectedIndex != 0)
                hdnRMId.Value = ddlRM.SelectedValue;
            else
                hdnRMId.Value = "";

            if (txtFrom.Text != "")
                hdnFromdate.Value = DateTime.Parse(txtFrom.Text).ToString();
            else
                hdnFromdate.Value = DateTime.MinValue.ToString();

            if (txtTo.Text != "")
                hdnTodate.Value = DateTime.Parse(txtTo.Text).ToString();
            else
                hdnTodate.Value = DateTime.MinValue.ToString();
        }

        protected void BindGvOrderList()
        {
            DataTable dtOrder = new DataTable();
            dtOrder = orderbo.GetOrderList(advisorVo.advisorId);

            if (dtOrder.Rows.Count > 0)
            {
                gvOrderList.DataSource = dtOrder;
                gvOrderList.DataBind();
                gvOrderList.Visible = true;

                if (Cache["OrderList"] == null)
                {
                    Cache.Insert("OrderList", dtOrder);
                }
                else
                {
                    Cache.Remove("OrderList");
                    Cache.Insert("OrderList", dtOrder);
                }

                ErrorMessage.Visible = false;
                tblMessage.Visible = false;
            }
            else
            {
                gvOrderList.Visible = false;
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
            }
        }

        protected void gvOrderList_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGIDetails = new DataTable();
            dtGIDetails = (DataTable)Cache["OrderList"];
            gvOrderList.DataSource = dtGIDetails;
        }

        protected void gvOrderList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                GridDataItem item = (GridDataItem)e.Item;
                string value = item.GetDataKeyValue("CO_OrderId").ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('LifeInsuranceOrderEntry','strOrderId=" + value + " ');", true);
            }
        }
    }
}