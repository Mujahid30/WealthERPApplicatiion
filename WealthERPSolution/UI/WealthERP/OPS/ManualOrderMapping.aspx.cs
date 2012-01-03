using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoOps;

namespace WealthERP.OPS
{
    public partial class ManualOrderMapping : System.Web.UI.Page
    {
        OperationBo operationBo = new OperationBo();
       string path = string.Empty;
        string id = string.Empty;
        DataTable dtOrderRecon;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Theme"] != null)
            {
                Page.Theme = Session["Theme"].ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["result"] != null)
                {
                    id = Request.QueryString["result"];
                    BindMannualMatchGrid(id);
                }
                //if (Session["GridView"] != null)
                //    dtOrderRecon = (DataTable)Session["GridView"];
                //if (Request.QueryString["result"] != null)
                    //BindMannualMatchGrid(dtOrderRecon, ids);
                    
            } 
        }

        private void BindMannualMatchGrid(string id)
        {
            string orderId = id;
            string OrderType;
            DataSet dsOrderMannualMatch;
            DataTable dtOrderMannualMatch;
            dsOrderMannualMatch = operationBo.GetOrderMannualMatch(orderId);
            dtOrderMannualMatch = dsOrderMannualMatch.Tables[0];
            if (dtOrderMannualMatch.Rows.Count > 0)
            {
                gvMannualMatch.DataSource = dtOrderMannualMatch;
                gvMannualMatch.DataBind();
                lblGetOrderNo.Text = dtOrderMannualMatch.Rows[0]["CMOT_OrderNumber"].ToString();
                lblGetOrderDate.Text = dtOrderMannualMatch.Rows[0]["CMOT_OrderDate"].ToString();
                lblGetOrderStatus.Text = dtOrderMannualMatch.Rows[0]["XS_Status"].ToString();
                if(dtOrderMannualMatch.Rows[0]["CMOT_IsImmediate"].ToString()=="1")
                    OrderType = "Immediate";
                else
                    OrderType = "Future";
                lblGetOrderType.Text = OrderType;
            }
        }

        //private void BindMannualMatchGrid(DataTable dtMannualMatch, string strMannualMatchIds)
        //{
        //    string[] strIds = strMannualMatchIds.Split('~');
        //    DataRow[] drRow;
        //    DataTable dtSelectedForMannualMatch = dtMannualMatch.Clone();
        //    foreach (string str in strIds)
        //    {
        //        if (!string.IsNullOrEmpty(str.Trim()))
        //        {
        //            drRow = dtMannualMatch.Select("CMOT_MFOrderId=" + str);
        //            //if (drRow.Count() > 0)
        //            dtSelectedForMannualMatch.ImportRow(drRow[0]);

        //        }
        //    }

        //    gvMannualMatch.DataSource = dtSelectedForMannualMatch;
        //    gvMannualMatch.DataBind();

        //    lblGetOrderNo.Text = dtSelectedForMannualMatch.Rows[0]["OrderNumber"].ToString();
        //    lblGetOrderDate.Text = dtSelectedForMannualMatch.Rows[0]["Orderdate"].ToString();
        //    lblGetOrderStatus.Text = dtSelectedForMannualMatch.Rows[0]["OrderStatus"].ToString();
        //    lblGetOrderType.Text = dtSelectedForMannualMatch.Rows[0]["OrderType"].ToString();


        //}

    }
    
}
