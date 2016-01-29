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
using System.Collections;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using BoWerpAdmin;
using System.Data;
using BoProductMaster;
using System.Xml;
using System.Net;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
using System.Web.Script.Serialization;
using Telerik.Web.UI;
using VoCustomerProfiling;
using BoCustomerProfiling;
namespace WealthERP.OnlineOrderManagement
{
    public partial class OnlineCustomerOrderandTransactionBook : System.Web.UI.UserControl
    {
        UserVo userVo;
        OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
        CustomerVo customerVO;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            customerVO = (CustomerVo)Session["customerVo"];
            if (!IsPostBack)
            {
                BindAmc();
                BindScheme();
            }
        }
        protected void BindAmc()
        {
            DataSet ds = new DataSet();
            DataTable dtAmc = new DataTable();
            ds = OnlineMFOrderBo.GetOrderAmcDetails(customerVO.CustomerId);
            dtAmc = ds.Tables[0];

            if (dtAmc.Rows.Count > 0)
            {
                ddlAMC.DataSource = dtAmc;
                ddlAMC.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAMC.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAMC.DataBind();
            }
            ddlAMC.Items.Insert(0, new ListItem("All", "0"));

        }
        protected void BindScheme()
        {
            OnlineMFSchemeDetailsBo onlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
            DataSet ds;
            if (ddlAMC.SelectedValue != "")
            {
                ds = onlineMFSchemeDetailsBo.GetSIPCustomeSchemePlan(customerVO.CustomerId, int.Parse(ddlAMC.SelectedValue));
                ddlScheme.DataSource = ds.Tables[0];
                ddlScheme.DataValueField = ds.Tables[0].Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = ds.Tables[0].Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        protected void btnViewOrder_Click(object sender, EventArgs e)
        {

            switch (ddlAction.SelectedValue)
            {
                case "SIP":
                    BindOrderTransactionBook(customerVO.CustomerId, int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), "SIP", int.Parse(ddlExchange.SelectedValue));
                    break;
                case "BUY":
                    BindOrderTransactionBook(customerVO.CustomerId, int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), "BUY", int.Parse(ddlExchange.SelectedValue));
                    break;
                case "ABY":
                    BindOrderTransactionBook(customerVO.CustomerId, int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), "ABY", int.Parse(ddlExchange.SelectedValue));

                    break;
                case "SEL":
                    BindOrderTransactionBook(customerVO.CustomerId, int.Parse(ddlAMC.SelectedValue), int.Parse(ddlScheme.SelectedValue), "SEL", int.Parse(ddlExchange.SelectedValue));
                    break;
            }
        }


        protected void BindOrderTransactionBook(int customerId, int amcCode, int schemeCode, string OrderType, int exchangeType)
        {
            DataSet ds = OnlineMFOrderBo.GetCustomerOrderBookTransaction(customerId, amcCode, schemeCode, OrderType, exchangeType);
            DataTable dtOrderTransactionBook = ds.Tables[0];
            if (dtOrderTransactionBook.Rows.Count >= 0)
            {
                if (Cache["OrderTransactionBook" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("OrderTransactionBook" + userVo.UserId.ToString(), dtOrderTransactionBook);
                }
                else
                {
                    Cache.Remove("OrderTransactionBook" + userVo.UserId.ToString());
                    Cache.Insert("OrderTransactionBook" + userVo.UserId.ToString(), dtOrderTransactionBook);
                }

                gvOrderBook.DataSource = ds.Tables[0];
                gvOrderBook.Rebind();
                Div1.Visible = true;
            }
        }
        protected void gvOrderBook_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtOrderTransactionBook = (DataTable)Cache["OrderTransactionBook" + userVo.UserId.ToString()];
            if (dtOrderTransactionBook != null)
            {
                gvOrderBook.DataSource = dtOrderTransactionBook;
            }
        }
        protected void ddlAMC_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMC.SelectedValue != "0")
            {
                BindScheme();
            }
        }
        protected void btnDetails_OnClick(object sender, EventArgs e)
        {
            int count = gvOrderBook.MasterTableView.Items.Count;
            DataTable dtIssueDetail;
            Button btnDetails = (Button)sender;
            GridDataItem gdi;
            DataTable filldt = new DataTable();
            gdi = (GridDataItem)btnDetails.NamingContainer;
            int orderId = int.Parse(gvOrderBook.MasterTableView.DataKeyValues[gdi.ItemIndex]["CO_OrderId"].ToString());
            RadGrid gvChildDetails = (RadGrid)gdi.FindControl("gvChildDetails");
            //Panel PnlChild = (Panel)gdi.FindControl("pnlchild");
            if (gvChildDetails.Visible == false)
            {
                gvChildDetails.Visible = true;
                //btnDetails.Text = "-";
            }
            else if (gvChildDetails.Visible == true)
            {
                gvChildDetails.Visible = false;
                //buttonlink.Text = "+";
            }
            DataTable dtNCDOrderBook = (DataTable)Cache["OrderTransactionBook" + userVo.UserId.ToString()];
            DataRow[] rows = dtNCDOrderBook.Select("CO_OrderId = " + orderId + " ");
            if (rows.Length > 0)
            {
                filldt = rows.CopyToDataTable();
            }

            gvChildDetails.DataSource = filldt;
            gvChildDetails.DataBind();
        }
    }
}