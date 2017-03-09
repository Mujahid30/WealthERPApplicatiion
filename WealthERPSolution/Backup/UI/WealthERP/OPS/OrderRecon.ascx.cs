using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using VoUser;
using BoAdvisorProfiling;
using BoCommon;
using WealthERP.Base;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Configuration;
using BoOps;

namespace WealthERP.OPS
{
    public partial class OrderRecon : System.Web.UI.UserControl
    {
        OperationBo operationBo = new OperationBo();
        DateBo dtBo = new DateBo();
        DateTime dtTo = new DateTime();
        DateTime dtFrom = new DateTime();
        string path = string.Empty;
        string ids = string.Empty;
        DataTable dtOrderRecon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                btnSubmit.Visible = false;
                BindPeriodDropDown();
                if (Request.QueryString["result"] != null)
                    ids = Request.QueryString["result"];
                if (Session["GridView"] != null)
                    dtOrderRecon = (DataTable)Session["GridView"];
                if (Request.QueryString["result"] != null)
                    BindReconGrid(dtOrderRecon, ids);
                trRange.Visible = true;
                if (rbtnPickDate.Checked == true)
                {
                    trRange.Visible = true;
                    txtFromDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
                    txtToDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
                    trPeriod.Visible = false;
                }
                BindOrderStatus();
                tblMessage.Visible = false;
            }
 

        }
        protected void rbtnDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPickDate.Checked == true)
            {
                trRange.Visible = true;
                txtFromDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
                txtToDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
                trPeriod.Visible = false;
            }
            else if (rbtnPickPeriod.Checked == true)
            {
                trRange.Visible = false;
                trPeriod.Visible = true;
                BindPeriodDropDown();
            }
        }
        private void BindOrderStatus()
        {
            DataSet dsOrderStaus;
            DataTable dtOrderStatus;
            dsOrderStaus = operationBo.GetOrderStatus();
            dtOrderStatus = dsOrderStaus.Tables[0];
            if (dtOrderStatus.Rows.Count > 0)
            {
                ddlOrderStatus.DataSource = dtOrderStatus;
                ddlOrderStatus.DataValueField = dtOrderStatus.Columns["XS_StatusCode"].ToString();
                ddlOrderStatus.DataTextField = dtOrderStatus.Columns["XS_Status"].ToString();
                ddlOrderStatus.DataBind();
            }
            ddlOrderStatus.Items.Insert(0, new ListItem("Select", "Select"));
        }
        private void BindPeriodDropDown()
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            DataTable dtPeriod;
            dtPeriod = XMLBo.GetDatePeriod(path);

            ddlPeriod.DataSource = dtPeriod;
            ddlPeriod.DataTextField = "PeriodType";
            ddlPeriod.DataValueField = "PeriodCode";
            ddlPeriod.DataBind();
            ddlPeriod.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select a Period", "Select a Period"));
            ddlPeriod.Items.RemoveAt(15);
        }

        private void BindReconGrid(DataTable dtRecon, string strReconIds)
        {
            string[] strIds = strReconIds.Split('~');
            DataRow[] drRow;
            DataTable dtSelectedForRecon = dtRecon.Clone();
            bool isPending = false;
            foreach (string str in strIds)
            {
                if (!string.IsNullOrEmpty(str.Trim()))
                {
                    drRow = dtRecon.Select("CMOT_MFOrderId=" + str);
                    if (drRow.Count() > 0)
                        dtSelectedForRecon.ImportRow(drRow[0]);

                    if (drRow[0][15].ToString() == "Pending")
                    {
                        isPending = true;
                    }

                }
            }

            gvOrderRecon.DataSource = dtSelectedForRecon;
            gvOrderRecon.DataBind();
            if (isPending)
            {
                gvOrderRecon.Columns[6].Visible = true;
                gvOrderRecon.Columns[7].Visible = true;
            }
            else
            {
                gvOrderRecon.Columns[6].Visible = false;
                gvOrderRecon.Columns[7].Visible = false;

            }

            //            DataTable above24 = dt.Clone();

            ////get only the rows you want
            //DataRow[] results = dt.Select(”age > 24″);

            ////populate new destination table
            //foreach (DataRow dr in results)
            //above24.ImportRow(dr);

        }

        protected void btnGo_Click(object sender, EventArgs e)
       {
              BindOrderRecon();
              btnSubmit.Visible = true;
        //    if (Session["GridView"] != null)
        //    {
        //        DataTable dtForRecon;
        //        if (Session["GridView"] != null)
        //            dtOrderRecon = (DataTable)Session["GridView"];
        //        if (dtOrderRecon.Rows.Count > 0)
        //        {
        //            dtForRecon = dtOrderRecon.Clone();
        //            DataRow[] drRecon = dtOrderRecon.Select("OrderStatus='" + ddlOrderStatus.SelectedValue.ToString() + "'");
        //            if (drRecon.Count() > 0)
        //            {
        //                foreach (DataRow dr in drRecon)
        //                {
        //                    dtForRecon.ImportRow(dr);

        //                }

        //            }
        //            gvOrderRecon.DataSource = dtForRecon;
        //            gvOrderRecon.DataBind();

        //        }
        //        if (ddlOrderStatus.SelectedValue == "Pending" || ddlOrderStatus.SelectedValue == "Rejected")
        //        {
        //            gvOrderRecon.Columns[6].Visible = true;
        //            gvOrderRecon.Columns[7].Visible = true;

        //        }
        //        else
        //        {
        //            gvOrderRecon.Columns[6].Visible = false;
        //            gvOrderRecon.Columns[7].Visible = false;

        //        }



        //    }
        }

        private void BindOrderRecon()
        {
            DataSet dsMFOrderRecon;
            DataTable dtMFOrderRecon;
            CalculateDateRange(out dtFrom, out dtTo);
            hdnFromDate.Value = dtFrom.ToString();
            hdnToDate.Value = dtTo.ToString();
            if (ddlOrderStatus.SelectedIndex != 0)
                hdnOrderStatus.Value = ddlOrderStatus.SelectedValue;
            else
                hdnOrderStatus.Value = "";
            if (ddlOrderType.SelectedIndex != 0)
                hdnOrderType.Value = "N";
            else
                hdnOrderType.Value = "Y";
            dsMFOrderRecon = operationBo.GetMFOrderRecon(DateTime.Parse(hdnFromDate.Value.ToString()), DateTime.Parse(hdnToDate.Value.ToString()), hdnOrderStatus.Value, hdnOrderType.Value);
            dtMFOrderRecon = dsMFOrderRecon.Tables[0];
            if (dtMFOrderRecon.Rows.Count > 0)
            {
                gvOrderRecon.Visible = true;
                gvOrderRecon.DataSource = dtMFOrderRecon;
                gvOrderRecon.DataBind();

                if (ddlOrderStatus.SelectedValue == "GPPD" || ddlOrderStatus.SelectedValue == "GPRJ")
                {
                    gvOrderRecon.Columns[6].Visible = true;
                    gvOrderRecon.Columns[7].Visible = true;

                }
                else
                {
                    gvOrderRecon.Columns[6].Visible = false;
                    gvOrderRecon.Columns[7].Visible = false;

                }
                tblMessage.Visible = false;
            }
            else
            {
               
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
                gvOrderRecon.Visible = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string ids = GetSelectedIdString();
            Response.Write("<script type='text/javascript'>detailedresults=window.open('OPS/ManualOrderMapping.aspx?result=" + ids + "','mywindow', 'width=1000,height=450,scrollbars=yes,location=center');</script>");
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OrderManagement", "loadcontrol('AdviserLoanCommsnStrucWithLoanPartner','?result=" + ids + "');", true);
        }
        private string GetSelectedIdString()
        {
            string gvId = "";

            //'Navigate through each row in the GridView for checkbox items
            foreach (GridViewRow gvRow in gvOrderRecon.Rows)
            {
                RadioButton RdBnItem = (RadioButton)gvRow.FindControl("rbtnMatch");
                if (RdBnItem.Checked)
                {
                    gvId = Convert.ToString(gvOrderRecon.DataKeys[gvRow.RowIndex].Value);
                }
            }


            return gvId;

        }
        private void CalculateDateRange(out DateTime fromDate, out DateTime toDate)
        {
            if (rbtnPickDate.Checked==true)
            {
                fromDate = DateTime.Parse(txtFromDate.Text);
                toDate = DateTime.Parse(txtToDate.Text);
            }
            else if (rbtnPickPeriod.Checked == true)
            {
                dtBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue.ToString(), out dtFrom, out dtTo);
                fromDate = dtFrom;
                toDate = dtTo;
            }
            else
            {
                fromDate = DateTime.MinValue;
                toDate = DateTime.MinValue;
            }

        }

        protected void gvOrderRecon_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblordertype = e.Row.FindControl("lblOrderType") as Label;
                string ordertype = null;
                ordertype = lblordertype.Text;
                if (ordertype == "1")
                    lblordertype.Text = "Immediate";
                else
                    lblordertype.Text = "Future";
            }
        }


    }
}