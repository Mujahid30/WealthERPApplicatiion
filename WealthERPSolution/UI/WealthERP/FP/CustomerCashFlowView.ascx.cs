using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoFPSuperlite;
using VoUser;
using Telerik.Web.UI;

namespace WealthERP.FP
{
    public partial class CustomerCashFlowView : System.Web.UI.UserControl
    {
        CustomerFPAnalyticsBo customerFPAnalyticsBo = new CustomerFPAnalyticsBo();
        CustomerVo customerVo = new CustomerVo();
        int CFCustomerId = 0;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            if(!IsPostBack)
            BindCustomerCashFlowDetailsGrid();
        }


        public void BindCustomerCashFlowDetailsGrid()
        {
            DataSet ds = customerFPAnalyticsBo.BindCustomerCashFlowDetails(customerVo.CustomerId);
            Cache.Remove("CashFlowDetails" + customerVo.CustomerId.ToString());
            Cache.Insert("CashFlowDetails" + customerVo.CustomerId.ToString(), ds.Tables[0]);
            gvCashFlowDetails.DataSource = ds.Tables[0];
            gvCashFlowDetails.DataBind();
        }
        protected void gvCashFlowDetails_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtCashFlowDetails;
            dtCashFlowDetails = (DataTable)Cache["CashFlowDetails" + customerVo.CustomerId.ToString()];
            if (dtCashFlowDetails != null)
            {
                gvCashFlowDetails.DataSource = dtCashFlowDetails;
            }
        }
        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvCashFlowDetails.ExportSettings.OpenInNewWindow = true;
            gvCashFlowDetails.ExportSettings.IgnorePaging = true;
            gvCashFlowDetails.ExportSettings.HideStructureColumns = true;
            gvCashFlowDetails.ExportSettings.ExportOnlyData = true;
            gvCashFlowDetails.ExportSettings.FileName = "Gold Details";
            // gvrPensionAndGratuities.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvCashFlowDetails.MasterTableView.ExportToExcel();
        }
        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList ddlAction = (DropDownList)sender;
                GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;
                CFCustomerId = int.Parse(gvCashFlowDetails.MasterTableView.DataKeyValues[selectedRow - 1]["CCRL_ID"].ToString());
                if (ddlAction.SelectedValue.ToString() == "Edit")
                {
                    if (hdnIsCustomerLogin.Value == "Customer")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Permisssion denied for Manage Portfolio !!');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerCashFlow", "loadcontrol('CustomerCashFlow','action=" + ddlAction.SelectedItem.Value.ToString() + "&CFCustomerId=" + CFCustomerId +"');", true);
                }
                if (ddlAction.SelectedValue == "View")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerCashFlow", "loadcontrol('CustomerCashFlow','action=" + ddlAction.SelectedItem.Value.ToString() + "&CFCustomerId=" + CFCustomerId + "');", true);
                  
                }
                if (ddlAction.SelectedValue.ToString() == "Delete")
                {
                    if (hdnIsCustomerLogin.Value == "Customer" && hdnIsMainPortfolio.Value == "1")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Permisssion denied for Manage Portfolio !!');", true);
                    else
                    {
                        bool CheckCustomerCashFlow;
                        CheckCustomerCashFlow = customerFPAnalyticsBo.DeleteEquityTransaction(CFCustomerId);
                        if (CheckCustomerCashFlow == false)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "ShowAlertToDelete();", true);
                        }
                    }
                }
                BindCustomerCashFlowDetailsGrid();
            }
        }

    }

