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

namespace WealthERP.OPS
{
    public partial class OrderList : System.Web.UI.UserControl
    {
        OrderBo orderbo = new OrderBo();
        AdvisorVo advisorVo;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];

            if (!IsPostBack)
            {
                BindGvOrderList();
            }
        }

        protected void BindGvOrderList()
        {
            DataTable dtOrder = new DataTable();
            dtOrder = orderbo.GetOrderList(advisorVo.advisorId);
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