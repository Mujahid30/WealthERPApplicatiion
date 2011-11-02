using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCustomerPortfolio;
using VoUser;
using BoCustomerProfiling;
using WealthERP.Base;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoAdvisorProfiling;
using BoUploads;

namespace WealthERP.FP
{
    public partial class CustomerFPGoalDashBoard : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            BindCustomerDashboardGridView();
        }


        private void BindCustomerDashboardGridView()
        {

            DataTable dtBindGridView = new DataTable();

            dtBindGridView.Columns.Add("Goal");
            dtBindGridView.Columns.Add("Goal Amount");
            dtBindGridView.Columns.Add("Target year");
            dtBindGridView.Columns.Add("Fund Gap");
            dtBindGridView.Columns.Add("GoalFlag");


            DataRow drdtBindGridView;
            drdtBindGridView = dtBindGridView.NewRow();
            drdtBindGridView[0] = "Buy Home";
            drdtBindGridView[1] = "94,91,493";
            drdtBindGridView[2] = "2022";
            drdtBindGridView[3] = "8,00,000";
            drdtBindGridView[4] = "1";
            dtBindGridView.Rows.Add(drdtBindGridView);

            drdtBindGridView = dtBindGridView.NewRow();
            drdtBindGridView[0] = "Child Education";
            drdtBindGridView[1] = "13,38,226";
            drdtBindGridView[2] = "2016";
            drdtBindGridView[3] = "9,00,000";
            drdtBindGridView[4] = "1";
            dtBindGridView.Rows.Add(drdtBindGridView);

            drdtBindGridView = dtBindGridView.NewRow();
            drdtBindGridView[0] = "Child Marriage";
            drdtBindGridView[1] = "30,07,261";
            drdtBindGridView[2] = "2018";
            drdtBindGridView[3] = "11,00,000";
            drdtBindGridView[4] = "0";
            dtBindGridView.Rows.Add(drdtBindGridView);

            drdtBindGridView = dtBindGridView.NewRow();
            drdtBindGridView[0] = "Other";
            drdtBindGridView[1] = "5,95,508";
            drdtBindGridView[2] = "	2014";
            drdtBindGridView[3] = "2,00,000";
            drdtBindGridView[4] = "1";
            dtBindGridView.Rows.Add(drdtBindGridView);

            drdtBindGridView = dtBindGridView.NewRow();
            drdtBindGridView[0] = "Retirement";
            drdtBindGridView[1] = "54,95,553";
            drdtBindGridView[2] = "	2014";
            drdtBindGridView[3] = "28,00,000";
            drdtBindGridView[4] = "1";
            dtBindGridView.Rows.Add(drdtBindGridView);



            gVCustomerDashboard.DataSource = dtBindGridView;
            gVCustomerDashboard.DataBind();
            gVCustomerDashboard.Columns[4].Visible = false;


        }

        protected void gVCustomerDashboard_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int lblImageTxt = 0;
                System.Web.UI.WebControls.Image imgActionIndicator = e.Row.FindControl("imgActionIndicator") as System.Web.UI.WebControls.Image;
                Label lblImage = e.Row.FindControl("lblImage") as Label;
                lblImageTxt = int.Parse(lblImage.Text.Trim());
                if (lblImageTxt == 1)
                {
                    imgActionIndicator.ImageUrl = "~/Images/GreenUpArrow.png";
                }
                else
                {
                    imgActionIndicator.ImageUrl = "~/Images/RedDownArrow.png";
                }
            }
        }
    }
}