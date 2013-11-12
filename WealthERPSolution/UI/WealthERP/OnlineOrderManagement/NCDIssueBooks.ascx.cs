using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using VoUser;
using WealthERP.Base;

namespace WealthERP.OnlineOrderManagement
{
    public partial class NCDIssueBooks : System.Web.UI.UserControl
    {
        UserVo userVo;
        CustomerVo customerVo = new CustomerVo();
        AdvisorVo advisorVo;
        int customerId;
        //string CustId = "7709";
        BoOnlineOrderManagement.OnlineBondOrderBo BoOnlineBondOrder = new BoOnlineOrderManagement.OnlineBondOrderBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            userVo = (UserVo)Session[SessionContents.UserVo];
            customerVo = (CustomerVo)Session["customerVo"];
            advisorVo = (AdvisorVo)Session["adviserVo"];

            if (!IsPostBack)
            {
                if (Request.QueryString["customerId"] != null)
                {
                    customerId = int.Parse(Request.QueryString["customerId"].ToString());
                    BindBBGV(customerId);
                }
                else
                {
                    //CustId = Session["CustId"].ToString();
                    BindBBGV(customerVo.CustomerId);
                }
            }
        }

        protected void BindBBGV(int customerId)
        {
            DataSet dsbondsBook = BoOnlineBondOrder.GetOrderBondBook(customerId);
            if (dsbondsBook.Tables[0].Rows.Count > 0)
            {
                gvBBList.DataSource = dsbondsBook;
                gvBBList.DataBind();
                ibtExportSummary.Visible = true;
                 //pnlGrid.Visible = true;
               // pnlGrid.Visible = true;
            }
            else
            {
                ibtExportSummary.Visible = false;
                gvBBList.DataSource = dsbondsBook;
                gvBBList.DataBind();
                //pnlGrid.Visible = true;
            }

            Cache.Insert(userVo.UserId.ToString() + "CommissionStructureRule", dsbondsBook.Tables[0]);
        }

        protected void ibtExportSummary_OnClick(object sender, ImageClickEventArgs e)
        {
            DataTable dtCommMgmt = new DataTable();
            dtCommMgmt = (DataTable)Cache[userVo.UserId.ToString() + "CommissionStructureRule"];
            if (dtCommMgmt == null)
                return;
            else if (dtCommMgmt.Rows.Count < 1)
                return;
            gvBBList.DataSource = dtCommMgmt;
            gvBBList.ExportSettings.OpenInNewWindow = true;
            gvBBList.ExportSettings.IgnorePaging = true;
            gvBBList.ExportSettings.HideStructureColumns = true;
            gvBBList.ExportSettings.ExportOnlyData = true;
            gvBBList.ExportSettings.FileName = "Details";
            gvBBList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvBBList.MasterTableView.ExportToExcel();
            //BindStructureRuleGrid();
        }

        protected void ddlMenu_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //string sActionName = ((DropDownList)sender).SelectedItem.Text;
            //string sStructId = ((DropDownList)sender).SelectedValue;

            RadComboBox ddlAction = (RadComboBox)sender;
            //GridDataItem item = (GridDataItem)ddlAction.NamingContainer;
            //int structureId = int.Parse(gvBBList.MasterTableView.DataKeyValues[item.ItemIndex]["StructureId"].ToString());
            //string prodType = this.ddProduct.SelectedValue;

            switch (ddlAction.SelectedValue)
            {
                case "Cancel":
                    BoOnlineBondOrder.cancelBondsBookOrder("");
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('ReceivableSetup','StructureId=1');", true);
                    break;
                default:
                    return;
            }
        }

        protected void gvBBList_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            gvBBList.CurrentPageIndex = e.NewPageIndex;
            int rowindex1 = ((GridDataItem)((DropDownList)sender).NamingContainer).RowIndex;

            int rowindex = (rowindex1 / 2) - 1;
            if (Request.QueryString["customerId"] != null)
            {
                customerId = int.Parse(Request.QueryString["customerId"].ToString());
                BindjointNominee(customerId);
            }
            else
            {
               customerId=customerVo.CustomerId;
               BindjointNominee(customerId);
            }



        }
        protected void BindjointNominee(int customerId)
        {
            DataSet dsjointNominee = BoOnlineBondOrder.GetNomineeJointHolder(customerId);

            if (dsjointNominee.Tables[0].Rows.Count > 0)
                ibtExportSummary.Visible = true;
            else
                ibtExportSummary.Visible = false;

            gvBBList.DataSource = dsjointNominee;
            gvBBList.DataBind();

            Cache.Insert(userVo.UserId.ToString() + "NomineeJointHolder", dsjointNominee.Tables[0]);
        }
        protected void btnExpandAll_Click(object sender, EventArgs e)
        {
            DataTable dtIssueDetail;
            string strIssuerId = null;
            LinkButton buttonlink = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)buttonlink.NamingContainer;
            strIssuerId = gvBBList.MasterTableView.DataKeyValues[gdi.ItemIndex]["PFIIM_IssuerId"].ToString();
            int orderId = int.Parse(gvBBList.MasterTableView.DataKeyValues[gdi.ItemIndex]["CO_OrderId"].ToString());
            RadGrid gvChildDetails = (RadGrid)gdi.FindControl("gvChildDetails");
            Panel PnlChild = (Panel)gdi.FindControl("pnlchild");
            if (PnlChild.Visible == false)
            {
                PnlChild.Visible = true;
                buttonlink.Text = "-";
            }
            else if (PnlChild.Visible == true)
            {
                PnlChild.Visible = false;
                buttonlink.Text = "+";
            }
            DataSet ds = BoOnlineBondOrder.GetOrderBondSubBook(customerVo.CustomerId,strIssuerId, orderId);
            dtIssueDetail = ds.Tables[0];
            gvChildDetails.DataSource = dtIssueDetail;
            gvChildDetails.DataBind();
        }
        protected void gvBBList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtIssueDetail;
            dtIssueDetail = (DataTable)Cache["NCDBookList" + advisorVo.advisorId.ToString()];
            if (dtIssueDetail != null)
            {
                gvBBList.DataSource = dtIssueDetail;
            }

        }
        protected void gvChildDetails_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid gvChildDetails = (RadGrid)sender; // Get reference to grid 
            GridDataItem item = (GridDataItem)(gvChildDetails.NamingContainer as GridEditFormItem).ParentItem;  // Get the mastertableview item 
            string strIssuerId = item["PFIIM_IssuerId"].Text; // Get the value 
            int orderId = int.Parse(item["CO_OrderId"].Text.ToString());
            DataSet ds = BoOnlineBondOrder.GetOrderBondSubBook(orderId, strIssuerId, customerVo.CustomerId);
            gvChildDetails.DataSource = ds.Tables[0];
        }
    }
}