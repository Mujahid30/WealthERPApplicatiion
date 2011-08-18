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

namespace WealthERP.OPS
{
    public partial class OrderRecon : System.Web.UI.UserControl
    {
        string path = string.Empty;
        string ids = string.Empty;
        DataTable dtOrderRecon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hidDateType.Value = "DATE_RANGE";
                BindPeriodDropDown();
                if (Request.QueryString["result"] != null)
                    ids = Request.QueryString["result"];
                if (Session["GridView"] != null)
                    dtOrderRecon = (DataTable)Session["GridView"];
                if (Request.QueryString["result"] != null)
                    BindReconGrid(dtOrderRecon, ids);
            }

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
                    drRow = dtRecon.Select("Id=" + str);
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
            if (Session["GridView"] != null)
            {
                DataTable dtForRecon;
                if (Session["GridView"] != null)
                    dtOrderRecon = (DataTable)Session["GridView"];
                if (dtOrderRecon.Rows.Count > 0)
                {
                    dtForRecon = dtOrderRecon.Clone();
                    DataRow[] drRecon = dtOrderRecon.Select("OrderStatus='" + ddlOrderStatus.SelectedValue.ToString() + "'");
                    if (drRecon.Count() > 0)
                    {
                        foreach (DataRow dr in drRecon)
                        {
                            dtForRecon.ImportRow(dr);

                        }

                    }
                    gvOrderRecon.DataSource = dtForRecon;
                    gvOrderRecon.DataBind();

                }
                if (ddlOrderStatus.SelectedValue == "Pending" || ddlOrderStatus.SelectedValue == "Rejected")
                {
                    gvOrderRecon.Columns[6].Visible = true;
                    gvOrderRecon.Columns[7].Visible = true;

                }
                else
                {
                    gvOrderRecon.Columns[6].Visible = false;
                    gvOrderRecon.Columns[7].Visible = false;

                }



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
            string gvIds = "";

            //'Navigate through each row in the GridView for checkbox items
            foreach (GridViewRow gvRow in gvOrderRecon.Rows)
            {
                RadioButton RdBnItem = (RadioButton)gvRow.FindControl("rbtnMatch");
                if (RdBnItem.Checked)
                {
                    gvIds += Convert.ToString(gvOrderRecon.DataKeys[gvRow.RowIndex].Value) + "~";
                }
            }


            return gvIds;

        }


    }
}