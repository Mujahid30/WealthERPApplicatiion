﻿using System;
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
using Telerik.Web.UI;
using BoProductMaster;
using BoOps;
using BOAssociates;
using System.Configuration;
using VoOps;
using BoWerpAdmin;
using VoCustomerPortfolio;
using VOAssociates;
using iTextSharp.text.pdf;
using System.IO;
using System.Globalization;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using System.Text;
using VoCustomerProfiling;
using BoFPSuperlite;

namespace WealthERP.FP
{
    public partial class CustomerCashFlow : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CustomerFPAnalyticsBo customerFPAnalyticsBo = new CustomerFPAnalyticsBo();
        DataSet dsGetProductTypes;
        int customercashrecomendationid;
        UserVo userVo = new UserVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session["userVo"];
            if (!IsPostBack)
            {
                BindProductType();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {



            customercashrecomendationid = customerFPAnalyticsBo.CreateCashFlowRecomendation(customerVo.CustomerId, userVo.UserId, Convert.ToInt32(ddlpaytyppe.SelectedValue), Convert.ToInt32(ddlptype.SelectedValue), Convert.ToDecimal(txtAmount.Text), Convert.ToDateTime(txtStartDate.SelectedDate), Convert.ToDateTime(txtEndDate.SelectedDate), Convert.ToDecimal(txtsumassure.Text), txtRemarks.Text, ddlfrequncy.SelectedValue);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerCashFlowView','none');", true);




        }









        private void BindProductType()
        {
            dsGetProductTypes = customerFPAnalyticsBo.GetProductTypes();
            ddlptype.DataSource = dsGetProductTypes.Tables[0];
            ddlptype.DataTextField = "CRPL_ProductName";
            ddlptype.DataValueField = "CRPL_ID";
            ddlptype.DataBind();
            ddlptype.Items.Insert(0, new ListItem("Select", "0"));


        }
 
    }
}