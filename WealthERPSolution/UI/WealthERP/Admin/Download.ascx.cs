using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using VoWerpAdmin;
using BoWerpAdmin;
using BoMarketDataPull;
using System.Configuration;
using WealthERP.Base;
using VoUser;


namespace WealthERP.Admin
{
    public partial class Download : System.Web.UI.UserControl
    {
        
        //string con = ConfigurationManager.ConnectionStrings["marketdb"].ToString();
        GetDownloadData objDownload = new GetDownloadData();
        UserVo userVo = new UserVo();

            
        
      

        protected void ddlAssetGroup_OnSelectedChange(object sender, EventArgs e)
        {
            if (ddlAssetGroup.SelectedValue =="Equity")
            {
                ddlSource.Items.Add(new ListItem("BSE","BSE"));
                ddlSource.Items.Add(new ListItem("NSE","NSE"));
            }
            else if (ddlAssetGroup.SelectedValue == "MF")
            {
                ddlSource.Items.Add(new ListItem("AMFI", "AMFI"));
                btnDownload.Text = "Download History";
                btnDownload.CssClass = "PCGLongButton";
                btnDownloadCurrent.Visible = true;
            }
        }

        protected void OnClick_Download(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            userVo = (UserVo)Session["userVo"];

            if (txtFromDate.Text == "" || txtToDate.Text == "")
            {
                errDateNull.Visible = true;
                errDateNull.Text = "'From Date' or 'To Date' cannot be null";
            }
            else
            {
                if (ddlAssetGroup.SelectedValue == Contants.Source.Equity.ToString() && ddlSource.SelectedValue == Contants.NSE)
                {

                    dt = objDownload.downloadNSEEquityData(userVo.UserId, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));

                }
                if (ddlAssetGroup.SelectedValue == Contants.Source.Equity.ToString() && ddlSource.SelectedValue == Contants.BSE)
                {
                    dt = objDownload.downloadBSEEquityData(userVo.UserId, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));

                }
                if (ddlAssetGroup.SelectedValue == Contants.Source.MF.ToString() && ddlSource.SelectedValue == Contants.AMFI)
                {
                    dt = objDownload.downloadAmfiHistoricalData(userVo.UserId, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
                }
                gvResult.DataSource = dt;
                gvResult.DataBind();
            }
        }

        protected void btnDownloadCurrent_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            userVo = (UserVo)Session["userVo"];

            dt = objDownload.downloadAmfiCurrentData(userVo.UserId);
            gvResult.DataSource = dt;
            gvResult.DataBind();

        }
    }
}