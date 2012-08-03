using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoOps;
using VoUser;
using System.Data;
using VoOps;

namespace WealthERP.OPS
{
    public partial class CustomerOrderList : System.Web.UI.UserControl
    {
        OperationBo operationBo = new OperationBo();
        OperationVo operationVo = new OperationVo();
        MFOrderVo mforderVo = new MFOrderVo();
        MFOrderBo mforderBo = new MFOrderBo();
        OrderVo orderVo = new OrderVo();
        CustomerVo customerVo = null;
        int customerId;
        int status;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnApprove.Visible = false;
            if (!IsPostBack)
            {
                BindCustomerList();

            }
          
        }

        private void BindCustomerList()
        {
            DataSet dsCustomerApprovalList = new DataSet();
            DataTable dtCustomerApprovallist = new DataTable();
            status = 1;
            try
            {
                SessionBo.CheckSession();
                customerVo = (CustomerVo)Session["CustomerVo"];
                customerId = customerVo.CustomerId;
                status = int.Parse(ddlOrderType.SelectedValue);
                dsCustomerApprovalList = operationBo.GetCustomerApprovalList(customerId,status);
                dtCustomerApprovallist = dsCustomerApprovalList.Tables[0];
                if (dtCustomerApprovallist.Rows.Count > 0)
                {
                    gvOrderList.DataSource = dtCustomerApprovallist;
                    gvOrderList.DataBind();
                    gvOrderList.Visible = true;
                    ErrorMessage.Visible = false;
                    tblMessage.Visible = false;
                    //btnApprove.Visible = false;
                }
                else
                {
                    gvOrderList.Visible = false;
                    tblMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    ErrorMessage.InnerText = "No Records Found...!";
                    btnApprove.Visible = false;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            int i = 0;
            int gvOrderId = 0;
            bool result = false;
            foreach (GridViewRow gvRow in gvOrderList.Rows)
            {

                CheckBox chk = (CheckBox)gvRow.FindControl("cbRecons");
                if (chk.Checked)
                {
                    i++;
                }

            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a record!');", true);
                BindCustomerList();
            }
            else
            {
                foreach (GridViewRow gvRow in gvOrderList.Rows)
                {
                    if (((CheckBox)gvRow.FindControl("cbRecons")).Checked == true)
                    {
                        gvOrderId = Convert.ToInt32(gvOrderList.DataKeys[gvRow.RowIndex].Values["CO_OrderId"].ToString());
                        operationBo.UpdateCustomerApprovalList(gvOrderId);
                       
                    }
                }
                BindCustomerList();
            }
        }

        protected void gvOrderList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            DataSet dsGetMFOrderDetails;
           
           if (e.CommandName == "ViewOrderDetails")
            {
                try
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    //GridViewRow gvRow = ((GridViewRow)(((LinkButton)sender).Parent.Parent));
                    int orderId = Convert.ToInt32(gvOrderList.DataKeys[index].Value.ToString());
                    dsGetMFOrderDetails = mforderBo.GetCustomerMFOrderDetails(orderId);
                    if (dsGetMFOrderDetails.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsGetMFOrderDetails.Tables[0].Rows)
                        {
                            orderVo.OrderId = int.Parse(dr["CO_OrderId"].ToString());
                            orderVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                            mforderVo.CustomerName = dr["Customer_Name"].ToString();
                            mforderVo.RMName = dr["RM_Name"].ToString();
                            mforderVo.BMName = dr["AB_BranchName"].ToString();
                            mforderVo.PanNo = dr["C_PANNum"].ToString();
                            if (!string.IsNullOrEmpty(dr["PA_AMCCode"].ToString().Trim()))
                                mforderVo.Amccode = int.Parse(dr["PA_AMCCode"].ToString());
                            else
                                mforderVo.Amccode = 0;
                            if (!string.IsNullOrEmpty(dr["PAIC_AssetInstrumentCategoryCode"].ToString().Trim()))
                                mforderVo.category = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                            if (!string.IsNullOrEmpty(dr["PASP_SchemePlanCode"].ToString().Trim()))
                                mforderVo.SchemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                            mforderVo.OrderNumber = int.Parse(dr["CMFOD_OrderNumber"].ToString());
                            if (!string.IsNullOrEmpty(dr["CMFOD_Amount"].ToString().Trim()))
                                mforderVo.Amount = double.Parse(dr["CMFOD_Amount"].ToString());
                            else
                                mforderVo.Amount = 0;

                            if (int.Parse(dr["CMFA_accountid"].ToString()) != 0)
                                mforderVo.accountid = int.Parse(dr["CMFA_accountid"].ToString());
                            else
                                mforderVo.accountid = 0;
                            mforderVo.TransactionCode = dr["WMTT_TransactionClassificationCode"].ToString();
                            orderVo.OrderDate = DateTime.Parse(dr["CO_OrderDate"].ToString());
                            mforderVo.IsImmediate = int.Parse(dr["CMFOD_IsImmediate"].ToString());
                            orderVo.ApplicationNumber = dr["CO_ApplicationNumber"].ToString();
                            orderVo.ApplicationReceivedDate = DateTime.Parse(dr["CO_ApplicationReceivedDate"].ToString());
                            mforderVo.portfolioId = int.Parse(dr["CP_portfolioId"].ToString());
                            orderVo.PaymentMode = dr["XPM_PaymentModeCode"].ToString();
                            if (!string.IsNullOrEmpty(dr["CO_ChequeNumber"].ToString()))
                                orderVo.ChequeNumber = dr["CO_ChequeNumber"].ToString();
                            else
                                orderVo.ChequeNumber = "";
                            if (!string.IsNullOrEmpty(dr["CO_PaymentDate"].ToString()))
                                orderVo.PaymentDate = DateTime.Parse(dr["CO_PaymentDate"].ToString());
                            else
                                orderVo.PaymentDate = DateTime.MinValue;
                            if (!string.IsNullOrEmpty(dr["CMFOD_FutureTriggerCondition"].ToString()))
                                mforderVo.FutureTriggerCondition = dr["CMFOD_FutureTriggerCondition"].ToString();
                            else
                                mforderVo.FutureTriggerCondition = "";
                            if (!string.IsNullOrEmpty(dr["CMFOD_FutureExecutionDate"].ToString()))
                                mforderVo.FutureExecutionDate = DateTime.Parse(dr["CMFOD_FutureExecutionDate"].ToString());
                            else
                                mforderVo.FutureExecutionDate = DateTime.MinValue;
                            if (!string.IsNullOrEmpty(dr["PASP_SchemePlanSwitch"].ToString()))
                                mforderVo.SchemePlanSwitch = int.Parse(dr["PASP_SchemePlanSwitch"].ToString());
                            else
                                mforderVo.SchemePlanSwitch = 0;
                            if (!string.IsNullOrEmpty(dr["CB_CustBankAccId"].ToString()))
                                orderVo.CustBankAccId = int.Parse(dr["CB_CustBankAccId"].ToString());
                            else
                                orderVo.CustBankAccId = 0;
                            if (!string.IsNullOrEmpty(dr["CMFOD_BankName"].ToString()))
                                mforderVo.BankName = dr["CMFOD_BankName"].ToString();
                            else
                                mforderVo.BankName = "";
                            if (!string.IsNullOrEmpty(dr["CMFOD_BranchName"].ToString()))
                                mforderVo.BranchName = dr["CMFOD_BranchName"].ToString();
                            else
                                mforderVo.BranchName = "";
                            if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine1"].ToString()))
                                mforderVo.AddrLine1 = dr["CMFOD_AddrLine1"].ToString();
                            else
                                mforderVo.AddrLine1 = "";
                            if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine2"].ToString()))
                                mforderVo.AddrLine2 = dr["CMFOD_AddrLine2"].ToString();
                            else
                                mforderVo.AddrLine2 = "";
                            if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine3"].ToString()))
                                mforderVo.AddrLine3 = dr["CMFOD_AddrLine3"].ToString();
                            else
                                mforderVo.AddrLine3 = "";
                            if (!string.IsNullOrEmpty(dr["CMFOD_City"].ToString()))
                                mforderVo.City = dr["CMFOD_City"].ToString();
                            else
                                mforderVo.City = "";
                            if (!string.IsNullOrEmpty(dr["CMFOD_State"].ToString()))
                                mforderVo.State = dr["CMFOD_State"].ToString();
                            else
                                mforderVo.State = "";
                            if (!string.IsNullOrEmpty(dr["CMFOD_Country"].ToString()))
                                mforderVo.Country = dr["CMFOD_Country"].ToString();
                            else
                                mforderVo.Country = "";
                            if (!string.IsNullOrEmpty(dr["CMFOD_PinCode"].ToString()))
                                mforderVo.Pincode = dr["CMFOD_PinCode"].ToString();
                            else
                                mforderVo.Pincode = "";
                            if (!string.IsNullOrEmpty(dr["CMFOD_LivingScince"].ToString()))
                                mforderVo.LivingSince = DateTime.Parse(dr["CMFOD_LivingScince"].ToString());
                            else
                                mforderVo.LivingSince = DateTime.MinValue;

                            if (!string.IsNullOrEmpty(dr["XF_FrequencyCode"].ToString()))
                                mforderVo.FrequencyCode = dr["XF_FrequencyCode"].ToString();
                            else
                                mforderVo.FrequencyCode = "";
                            if (!string.IsNullOrEmpty(dr["CMFOD_StartDate"].ToString()))
                                mforderVo.StartDate = DateTime.Parse(dr["CMFOD_StartDate"].ToString());
                            else
                                mforderVo.StartDate = DateTime.MinValue;
                            if (!string.IsNullOrEmpty(dr["CMFOD_EndDate"].ToString()))
                                mforderVo.EndDate = DateTime.Parse(dr["CMFOD_EndDate"].ToString());
                            else
                                mforderVo.EndDate = DateTime.MinValue;

                            if (!string.IsNullOrEmpty(dr["CMFOD_Units"].ToString()))
                                mforderVo.Units = double.Parse(dr["CMFOD_Units"].ToString());
                            else
                                mforderVo.Units = 0;
                            
                        }
                        Session["orderVo"] = orderVo;
                        Session["mforderVo"]=mforderVo;
                    }
                    //****************Old Vo,Bo****************************************
                    //operationVo = operationBo.GetCustomerOrderTrackingDetails(orderId);
                    //Session["operationVo"] = operationVo;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderEntry", "loadcontrol('MFOrderEntry','action=View');", true);
                }
                catch (BaseApplicationException Ex)
                {
                    throw Ex;
                }
            
            }
               
        }
        protected void gvOrderList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TemplateField b = gvOrderList.Columns[0] as TemplateField;

                if (int.Parse(ddlOrderType.SelectedValue) == 1)
                    b.Visible = false;
                else
                    b.Visible = true;
                
            }
        }
        protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(ddlOrderType.SelectedValue) == 0)
            {
                BindCustomerList();
                btnApprove.Visible = true;
            }
            else
            {
                BindCustomerList();
                btnApprove.Visible = false;
            }
        }
    }
}