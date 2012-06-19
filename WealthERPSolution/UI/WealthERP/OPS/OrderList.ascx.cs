using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoOps;
using Telerik.Web.UI;

namespace WealthERP.OPS
{
    public partial class OrderList : System.Web.UI.UserControl
    {
        OrderBo orderbo = new OrderBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGvOrderList();
            }
        }

        protected void BindGvOrderList()
        {
            int CustomerId = 40168;
            DataTable dtOrder = new DataTable();
            dtOrder = orderbo.GetCustomerOrderList(CustomerId);
            gvOrderList.DataSource = dtOrder;
            gvOrderList.DataBind();

            if (Cache["OrderList"] == null)
            {
                Cache.Insert("OrderList", dtOrder);
            }
            else
            {
                Cache.Remove("OrderList");
                Cache.Insert("OrderList", dtOrder);
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