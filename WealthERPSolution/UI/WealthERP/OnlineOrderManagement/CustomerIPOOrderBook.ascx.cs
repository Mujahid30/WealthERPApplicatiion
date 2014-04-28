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
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using System.Data;

namespace WealthERP.OnlineOrderManagement
{
    public partial class CustomerIPOOrderBook : System.Web.UI.UserControl
    {
        BoOnlineOrderManagement.OnlineBondOrderBo BoOnlineBondOrder = new BoOnlineOrderManagement.OnlineBondOrderBo();
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        OnlineIPOOrderBo onlineIPOOrderBo = new OnlineIPOOrderBo();
        AdvisorVo advisorVo;
        CustomerVo customerVo;
        UserVo userVo;

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["customerVo"];
            if (!Page.IsPostBack)
            {
                BindCustomerIssueIPOBook();
            }

        }

        private void BindCustomerIssueIPOBook()
        {

            DataTable dtCustomerIssueIPOBook = onlineIPOOrderBo.GetCustomerIPOIssueBook(customerVo.CustomerId);

            if (dtCustomerIssueIPOBook.Rows.Count > 0)
            {
                if (Cache["CustomerIPOIssueBook" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("CustomerIPOIssueBook" + userVo.UserId.ToString(), dtCustomerIssueIPOBook);
                }
                else
                {
                    Cache.Remove("CustomerIPOIssueBook" + userVo.UserId.ToString());
                    Cache.Insert("CustomerIPOIssueBook" + userVo.UserId.ToString(), dtCustomerIssueIPOBook);
                }
                //ibtExportSummary.Visible = false;
                RadGridIssueIPOBook.DataSource = dtCustomerIssueIPOBook;
                RadGridIssueIPOBook.DataBind();
            }
            else
            {
                //ibtExportSummary.Visible = false;
                RadGridIssueIPOBook.DataSource = dtCustomerIssueIPOBook;
                RadGridIssueIPOBook.DataBind();

            }
 
        }
        protected void RadGridIssueIPOBook_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtCustomerIssueIPOBook;
            dtCustomerIssueIPOBook = (DataTable)Cache["CustomerIPOIssueBook" + userVo.UserId.ToString()];
            if (dtCustomerIssueIPOBook != null)
            {
                RadGridIssueIPOBook.DataSource = dtCustomerIssueIPOBook;
            }

        }
        public void RadGridIssueIPOBook_OnItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                string Iscancel = Convert.ToString(RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WOS_OrderStep"]);
                //LinkButton MarkAsReject = (LinkButton)dataItem.FindControl("MarkAsReject");
                LinkButton buttonEdit = dataItem["MarkAsReject"].Controls[0] as LinkButton;
                if (Iscancel == "CANCELLED" || Iscancel == "EXECUTED" || Iscancel == "ORDERED" || Iscancel == "ACCEPTED")
                {
                    buttonEdit.Enabled = false;
                }
            }
        }

        protected void RadGridIssueIPOBook_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            bool lbResult = false;
            string strRemark = string.Empty;
            //if (e.CommandName == RadGrid.UpdateCommandName)
            //{
            //    int OrderId = int.Parse(gvIPOOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
            //    int Amount = int.Parse(gvIPOOrderBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Amount"].ToString());
            //    //lbResult = BoOnlineBondOrder.cancelBondsBookOrder(OrderId, 2);
            //    if (lbResult == true)
            //    {
            //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Cancelled Successfully');", true);
            //    }

            //}
            if (e.CommandName == "Update")
            {
                GridEditableItem editItem = e.Item as GridEditableItem;
                TextBox txtRemark = (TextBox)e.Item.FindControl("txtRemark");
                strRemark = txtRemark.Text;
                LinkButton buttonEdit = editItem["MarkAsReject"].Controls[0] as LinkButton;
                //   Label extractStepCode = editItem["WES_Code"].Controls[1] as Label;
                Int32 orderId = Convert.ToInt32(RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
                string extractionStepCode = RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WES_Code"].ToString();
                if (extractionStepCode == string.Empty)
                {
                    string AcntId = RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["C_CustCode"].ToString();
                    double AmountPayable = Convert.ToDouble(RadGridIssueIPOBook.MasterTableView.DataKeyValues[e.Item.ItemIndex]["Amounttoinvest"].ToString());
                    lbResult = BoOnlineBondOrder.cancelBondsBookOrder(orderId, 2, txtRemark.Text);
                    BoOnlineBondOrder.DebitRMSUserAccountBalance(AcntId, AmountPayable, 0);
                    if (lbResult == true)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Cancelled Successfully');", true);
                    }
                    BindCustomerIssueIPOBook();

                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Order Cant be Cancelled as it is Extracted.');", true);

                }
            }
        }
    }
}