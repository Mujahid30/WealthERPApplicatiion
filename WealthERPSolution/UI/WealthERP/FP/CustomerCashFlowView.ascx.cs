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
            BindCustomerCashFlowDetailsGrid();
        }


        public void BindCustomerCashFlowDetailsGrid()
        {
            DataSet ds = customerFPAnalyticsBo.BindCustomerCashFlowDetails(customerVo.CustomerId);
            gvCashFlowDetails.DataSource = ds.Tables[0];
            gvCashFlowDetails.DataBind();
        }
        protected void gvCashFlowDetails_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //DataTable dtProcessLogDetails = new DataTable();
            //dtProcessLogDetails = (DataTable)Cache["EQAccountDetails" + portfolioId];
            //gvEQAcc.DataSource = dtProcessLogDetails;
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
           


                RadComboBox ddlAction = (RadComboBox)sender;
                GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;

                CFCustomerId = int.Parse(gvCashFlowDetails.MasterTableView.DataKeyValues[selectedRow - 1]["CCRL_ID"].ToString());
                Session["CustomerId"] = CFCustomerId;
                Session["EQAccountVoRow"] = customerFPAnalyticsBo.BindCustomerCashFlowDetails(customerVo.CustomerId);
                if (ddlAction.SelectedValue.ToString() == "Edit")
                {
                    if (hdnIsCustomerLogin.Value == "Customer" && hdnIsMainPortfolio.Value == "1")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Permisssion denied for Manage Portfolio !!');", true);
                    else
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerCashFlow','action=Edit');", true);
                }
                if (ddlAction.SelectedValue.ToString() == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerCashFlow','action=View');", true);
                }
                //if (ddlAction.SelectedValue.ToString() == "Delete")
                //{
                //    if (hdnIsCustomerLogin.Value == "Customer" && hdnIsMainPortfolio.Value == "1")
                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Permisssion denied for Manage Portfolio !!');", true);
                //    else
                //    {
                //        bool CheckTradeAccAssociationWithTransactions;
                //        CheckTradeAccAssociationWithTransactions = customerFPAnalyticsBo.CheckEQTradeAccNoAssociatedWithTransactions(CFCustomerId);

                //        if (CheckTradeAccAssociationWithTransactions == true)
                //        {
                //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Trade Account can not be deleted as some Transactions are Associated with this Trade Account Number.');", true);
                //        }
                //        else if (CheckTradeAccAssociationWithTransactions == false)
                //        {
                //            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "ShowAlertToDelete();", true);
                //        }
                //    }

                //}
            }


        }

    }

