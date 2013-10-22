﻿using System;
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
        //string Ids = string.Empty;
        int Ids;
        int scheme;
        int accountId;
        int customerId;
        int schemeSwitch;
        int onlineMFOrderFlag = 0;
        string type;
        double amount;
        DateTime orderDate;
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
            if (Request.QueryString["result"] != null)
            {
                Ids = Convert.ToInt32(Request.QueryString["result"]);
                //BindMannualMatchGrid(Ids);
            }
            if (Request.QueryString["SchemeCode"] != null)
            {
                scheme = Convert.ToInt32(Request.QueryString["SchemeCode"]);
            }
            if (Request.QueryString["AccountId"] != null)
            {
                accountId = Convert.ToInt32(Request.QueryString["AccountId"]);
            }
            if (Request.QueryString["Type"] != null)
            {
                type = Request.QueryString["Type"];
            }
            if (Request.QueryString["Amount"] != null)
            {
                amount = Convert.ToDouble(Request.QueryString["Amount"]);
            }
            if (Request.QueryString["OrderDate"] != null)
            {
                orderDate = Convert.ToDateTime(Request.QueryString["OrderDate"]);
            }
            if (Request.QueryString["Customerid"] != null)
            {
                customerId = Convert.ToInt32(Request.QueryString["Customerid"]);
            }
            if (Request.QueryString["SchemeSwitch"] != null)
            {
                schemeSwitch = Convert.ToInt32(Request.QueryString["SchemeSwitch"]);
            }
            if (Request.QueryString["OnlineStatus"] != null)
            {
                onlineMFOrderFlag = Convert.ToInt32(Request.QueryString["OnlineStatus"]);
            }


            if (!IsPostBack)
            {
                if (onlineMFOrderFlag == 1)
                {
                    Bind_Onl_MannualMatchGrid(scheme, accountId, type, amount, orderDate, customerId, schemeSwitch);
                }
                else
                {
                    BindMannualMatchGrid(scheme, accountId, type, amount, orderDate, customerId, schemeSwitch);
                }

            }
        }
        private void Bind_Onl_MannualMatchGrid(int scheme, int accountId, string type, double amount, DateTime orderDate, int customerId, int schemeSwitch)
        {
            //string orderIds = Ids;
            string OrderType;
            DataSet dsOrderMannualMatch;
            DataTable dtOrderMannualMatch;
            dsOrderMannualMatch = operationBo.Get_Onl_OrderMannualMatch(scheme, accountId, type, amount, orderDate, customerId, schemeSwitch);
            dtOrderMannualMatch = dsOrderMannualMatch.Tables[0];
            if (dtOrderMannualMatch.Rows.Count > 0)
            {
                gvMannualMatch.DataSource = dtOrderMannualMatch;
                gvMannualMatch.DataBind();
                gvMannualMatch.Visible = true;                 
                btnSubmit.Visible = true;
                ErrorMessage.Visible = false;
                tblMessage.Visible = false;                
                imgBubble.Visible = true;
            }
            else
            {
                gvMannualMatch.Visible = false;
                btnSubmit.Visible = false;
                imgBubble.Visible = false;
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
            }
        }
        private void BindMannualMatchGrid(int scheme, int accountId, string type, double amount, DateTime orderDate, int customerId, int schemeSwitch)
        {
            //string orderIds = Ids;
            string OrderType;
            DataSet dsOrderMannualMatch;
            DataTable dtOrderMannualMatch;
            dsOrderMannualMatch = operationBo.GetOrderMannualMatch(scheme, accountId, type, amount, orderDate, customerId, schemeSwitch);
            dtOrderMannualMatch = dsOrderMannualMatch.Tables[0];
            if (dtOrderMannualMatch.Rows.Count > 0)
            {
                gvMannualMatch.DataSource = dtOrderMannualMatch;
                gvMannualMatch.DataBind();
                gvMannualMatch.Visible = true;
                //lblGetOrderNo.Text = dtOrderMannualMatch.Rows[0]["CMOT_OrderNumber"].ToString();
                //lblGetOrderDate.Text = dtOrderMannualMatch.Rows[0]["CMOT_OrderDate"].ToString();
                //lblGetOrderStatus.Text = dtOrderMannualMatch.Rows[0]["XS_Status"].ToString();
                //if(dtOrderMannualMatch.Rows[0]["CMOT_IsImmediate"].ToString()=="1")
                //    OrderType = "Immediate";
                //else
                //    OrderType = "Future";
                //lblGetOrderType.Text = OrderType;
                btnSubmit.Visible = true;
                ErrorMessage.Visible = false;
                tblMessage.Visible = false;
                //hlClose.Visible = true;
                imgBubble.Visible = true;
            }
            else
            {
                gvMannualMatch.Visible = false;
                btnSubmit.Visible = false;
                //hlClose.Visible = false;
                imgBubble.Visible = false;
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow gvRow in gvMannualMatch.Rows)
            {
                RadioButton RdBnItem = (RadioButton)gvRow.FindControl("rbtnMatch");
                if (RdBnItem.Checked)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a record!');", true);
                BindMannualMatchGrid(scheme, accountId, type, amount, orderDate, customerId, schemeSwitch);
            }
            else
            {
                int transId = 0;
                int SchemeCode = 0;
                int accountId = 0;
                double amount = 0.0;
                string TrxType = string.Empty;
                bool isUpdate = false;
                foreach (GridViewRow gvRow1 in gvMannualMatch.Rows)
                {
                    if (((RadioButton)gvRow1.FindControl("rbtnMatch")).Checked == true)
                    {
                        transId = Convert.ToInt32(gvMannualMatch.DataKeys[gvRow1.RowIndex].Values["CMFT_MFTransId"].ToString());
                        SchemeCode = Convert.ToInt32(gvMannualMatch.DataKeys[gvRow1.RowIndex].Values["PASP_SchemePlanCode"].ToString());
                        //if (!string.IsNullOrEmpty(gvMannualMatch.DataKeys[gvRow1.RowIndex].Values["CMFA_AccountId"].ToString().Trim()))
                        //    accountId = Convert.ToInt32(gvMannualMatch.DataKeys[gvRow1.RowIndex].Values["CMFA_AccountId"].ToString());
                        //else
                        //    accountId = 0;
                        TrxType = gvMannualMatch.DataKeys[gvRow1.RowIndex].Values["WMTT_TransactionClassificationCode"].ToString();
                        amount = Convert.ToDouble(gvMannualMatch.DataKeys[gvRow1.RowIndex].Values["CMFT_Amount"].ToString());

                        if (onlineMFOrderFlag == 1)
                        {
                            isUpdate = operationBo.Order_Onl_MannualMatch(Ids, transId, SchemeCode, amount, TrxType);

                        }
                        else
                        {
                            isUpdate = operationBo.OrderMannualMatch(Ids, transId, SchemeCode, amount, TrxType);
                        }

                        if (isUpdate == true)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Matched successfully');", true);
                            return;
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Not able to match manually');", true);
                        }
                    }

                }

            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('OrderMIS','none');", true);

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
