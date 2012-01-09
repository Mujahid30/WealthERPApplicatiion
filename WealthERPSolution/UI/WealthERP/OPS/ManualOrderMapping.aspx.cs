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
       string Ids = string.Empty;
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
                    Ids = Request.QueryString["result"];
                    BindMannualMatchGrid(Ids);
                }
                //if (Session["GridView"] != null)
                //    dtOrderRecon = (DataTable)Session["GridView"];
                //if (Request.QueryString["result"] != null)
                    //BindMannualMatchGrid(dtOrderRecon, ids);
                    
            } 
        }

        private void BindMannualMatchGrid(string Ids)
        {
            string orderIds = Ids;
            string OrderType;
            DataSet dsOrderMannualMatch;
            DataTable dtOrderMannualMatch;
            dsOrderMannualMatch = operationBo.GetOrderMannualMatch(orderIds);
            dtOrderMannualMatch = dsOrderMannualMatch.Tables[0];
            if (dtOrderMannualMatch.Rows.Count > 0)
            {
                gvMannualMatch.DataSource = dtOrderMannualMatch;
                gvMannualMatch.DataBind();
                //lblGetOrderNo.Text = dtOrderMannualMatch.Rows[0]["CMOT_OrderNumber"].ToString();
                //lblGetOrderDate.Text = dtOrderMannualMatch.Rows[0]["CMOT_OrderDate"].ToString();
                //lblGetOrderStatus.Text = dtOrderMannualMatch.Rows[0]["XS_Status"].ToString();
                if(dtOrderMannualMatch.Rows[0]["CMOT_IsImmediate"].ToString()=="1")
                    OrderType = "Immediate";
                else
                    OrderType = "Future";
                //lblGetOrderType.Text = OrderType;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow gvRow in gvMannualMatch.Rows)
            {
                RadioButton rbd = (RadioButton)gvRow.FindControl("rbtnMatch");
                if (rbd.Checked)
                {
                    count++;
                }
                if (count > 1)
                    rbd.Checked = false;

            }
            if (count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a record!');", true);
                BindMannualMatchGrid(Ids);
            }
            else
            {
                int OrderId = 0;
                int PortfolioId = 0;
                int SchemeCode = 0;
                int accountId = 0;
                DateTime orderDate = DateTime.MinValue;
                string TrxType = string.Empty;
                bool isUpdate = false;
                foreach (GridViewRow gvRow1 in gvMannualMatch.Rows)
                {
                    if (((RadioButton)gvRow1.FindControl("rbtnMatch")).Checked == true)
                    {
                        OrderId = Convert.ToInt32(gvMannualMatch.DataKeys[gvRow1.RowIndex].Values["CMOT_MFOrderId"].ToString());
                        //PortfolioId = Convert.ToInt32(gvMannualMatch.DataKeys[gvRow1.RowIndex].Values["CP_portfolioId"].ToString());
                        SchemeCode = Convert.ToInt32(gvMannualMatch.DataKeys[gvRow1.RowIndex].Values["PASP_SchemePlanCode"].ToString());
                        if (!string.IsNullOrEmpty(gvMannualMatch.DataKeys[gvRow1.RowIndex].Values["CMFA_AccountId"].ToString().Trim()))
                            accountId = Convert.ToInt32(gvMannualMatch.DataKeys[gvRow1.RowIndex].Values["CMFA_AccountId"].ToString());
                        else
                            accountId = 0;
                        TrxType = gvMannualMatch.DataKeys[gvRow1.RowIndex].Values["WMTT_TransactionClassificationCode"].ToString();
                        orderDate = Convert.ToDateTime(gvMannualMatch.DataKeys[gvRow1.RowIndex].Values["CMOT_OrderDate"].ToString());
                        isUpdate = operationBo.OrderMannualMatch(OrderId, accountId, SchemeCode, orderDate, TrxType);

                        if (isUpdate == true)
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Matched successfully');", true);
                        else
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Multiple records found.Not able to match mannually');", true);
                    }

                }
               
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
