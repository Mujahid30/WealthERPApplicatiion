using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoOps;
using Telerik.Web.UI;
using VoUser;
using BoCommon;
using BoUploads;
using WealthERP.Base;
using BoAdvisorProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoOps;

namespace WealthERP.OPS
{
    public partial class OrderList : System.Web.UI.UserControl
    {
        OrderBo orderbo = new OrderBo();
        MFOrderBo mforderBo = new MFOrderBo();
        MFOrderVo mforderVo = new MFOrderVo();
        OrderVo orderVo = new OrderVo();
        RMVo rmVo = new RMVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorVo advisorVo;
        string userType;
        int customerId = 0;
        string customerType = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();            
            advisorVo = (AdvisorVo)Session["advisorVo"];           
            userType = Session[SessionContents.CurrentUserRole].ToString();

            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;           
            gvOrderList.Visible = false;
            trExportFilteredDupData.Visible = false;
            if (!IsPostBack)
            {
                BindBranchDropDown();
                BindRMDropDown();
                if (userType == "bm")
                {
                    ddlBranch.SelectedValue = bmID.ToString();
                    ddlBranch.Enabled = false;
                }
                lblselectCustomer.Visible = false;
                txtIndividualCustomer.Visible = false;
            }
        }

        private void BindBranchDropDown()
        {
            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;

            UploadCommonBo uploadsCommonDao = new UploadCommonBo();
            DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
            if (ds != null)
            {
                ddlBranch.DataSource = ds;
                ddlBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                ddlBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                ddlBranch.DataBind();
            }
            ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bmID = rmVo.RMId;
            if (ddlBranch.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0);
            }
        }

        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        {
            DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
            if (ds != null)
            {
                ddlRM.DataSource = ds.Tables[0]; ;
                ddlRM.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                ddlRM.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
                ddlRM.DataBind();
            }
            ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }

        private void BindRMDropDown()
        {
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
            if (dt.Rows.Count > 0)
            {
                ddlRM.DataSource = dt;
                ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
                ddlRM.DataTextField = dt.Columns["RMName"].ToString();
                ddlRM.DataBind();
            }
            ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "2"));
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            Cache.Remove("OrderList" + advisorVo.advisorId);
            SetParameters();
            BindGvOrderList();
        }

        private void SetParameters()
        {
            if (ddlBranch.SelectedIndex != 0)
                hdnBranchId.Value = ddlBranch.SelectedValue;
            else
                hdnBranchId.Value = "";

            if (ddlRM.SelectedIndex != 0)
                hdnRMId.Value = ddlRM.SelectedValue;
            else
                hdnRMId.Value = "";

            if (txtFrom.Text != "")
                hdnFromdate.Value = DateTime.Parse(txtFrom.Text).ToString();
            else
                hdnFromdate.Value = DateTime.MinValue.ToString();

            if (txtTo.Text != "")
                hdnTodate.Value = DateTime.Parse(txtTo.Text).ToString();
            else
                hdnTodate.Value = DateTime.MinValue.ToString();

            if (ddlOrderStatus.SelectedIndex == 0)
                hdnOrderStatus.Value = "0";
            else
                hdnOrderStatus.Value = "1";
        }

        protected void BindGvOrderList()
        {
            DataTable dtOrder = new DataTable();
            dtOrder = orderbo.GetOrderList(advisorVo.advisorId, hdnRMId.Value, hdnBranchId.Value, Convert.ToDateTime(hdnTodate.Value), Convert.ToDateTime(hdnFromdate.Value), Convert.ToInt16(hdnOrderStatus.Value), hdnCustomerId.Value,hdnOrderType.Value);

            if (dtOrder.Rows.Count > 0)
            {
                trExportFilteredDupData.Visible = true;
                gvOrderList.DataSource = dtOrder;
                gvOrderList.DataBind();
                gvOrderList.Visible = true;

                if (Cache["OrderList" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("OrderList" + advisorVo.advisorId, dtOrder);
                }
                else
                {
                    Cache.Remove("OrderList" + advisorVo.advisorId);
                    Cache.Insert("OrderList" + advisorVo.advisorId, dtOrder);
                }

                ErrorMessage.Visible = false;
                tblMessage.Visible = false;
                pnlOrderList.Visible = true;
            }
            else
            {
                gvOrderList.Visible = false;
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
                pnlOrderList.Visible = false;
            }
        }
        protected void btnExportFilteredDupData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvOrderList.ExportSettings.OpenInNewWindow = true;
            gvOrderList.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvOrderList.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvOrderList.MasterTableView.ExportToCSV();
        }

        protected void gvOrderList_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            trExportFilteredDupData.Visible = true;
            DataTable dtGIDetails = new DataTable();
            dtGIDetails = (DataTable)Cache["OrderList" + advisorVo.advisorId];
            gvOrderList.Visible = true;
            this.gvOrderList.DataSource = dtGIDetails;
        }

        protected void gvOrderList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                
                GridDataItem item = (GridDataItem)e.Item;
                string orderId = item.GetDataKeyValue("CO_OrderId").ToString();
                string customerId = item.GetDataKeyValue("C_CustomerId").ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + " ');", true);
            }
        }

        protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOrderType.SelectedValue == "IN")
            {
                hdnOrderType.Value = "IN";
            }
            else if (ddlOrderType.SelectedValue == "MF")
            {
                hdnOrderType.Value = "MF";
            }
            else if (ddlOrderType.SelectedValue == "All")
            {
                hdnOrderType.Value = "All";
            }

        }
        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RadComboBox ddlAction = (RadComboBox)sender;
                GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;

                string action = "";
                string orderId = gvOrderList.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderId"].ToString();
                string customerId = gvOrderList.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString();
                string assetGroupCode = gvOrderList.MasterTableView.DataKeyValues[selectedRow - 1]["PAG_AssetGroupCode"].ToString();
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + " ');", true);

                // Set the VO into the Session
                //insuranceVo = insuranceBo.GetInsuranceAssetLI(insuranceId, out dtAssociationId);
                //Session["dtAssociationId"] = dtAssociationId;
                //Session["insuranceVo"] = insuranceVo;
                //Session["customerAccountVo"] = customerAccountsBo.GetCustomerInsuranceAccount(insuranceVo.AccountId);

                if (ddlAction.SelectedItem.Value.ToString() == "Edit")
                {
                    action = "Edit";
                    if(assetGroupCode =="IN")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + "&strAction=" + action + " ');", true);
                    else if (assetGroupCode == "MF")
                    {
                        int mfOrderId = int.Parse(orderId);
                        GetMFOrderDetails(mfOrderId);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderEntry", "loadcontrol('MFOrderEntry','action=Edit');", true);
                    }
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','action=edit');", true);
                }
                if (ddlAction.SelectedItem.Value.ToString() == "View")
                {
                    action = "View";
                    if (assetGroupCode == "IN")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + "&strAction=" + action + " ');", true);
                    else if (assetGroupCode == "MF")
                    {
                        int mfOrderId = int.Parse(orderId);
                        GetMFOrderDetails(mfOrderId);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderEntry", "loadcontrol('MFOrderEntry','action=View');", true);
                    }
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','action=view');", true);
                }
                if (ddlAction.SelectedItem.Value.ToString() == "Delete")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewInsuranceDetails.ascx:ddlMenu_SelectedIndexChanged()");
                object[] objects = new object[1];
                //objects[0] = insuranceVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void GetMFOrderDetails(int orderId)
        {
            DataSet dsGetMFOrderDetails = mforderBo.GetCustomerMFOrderDetails(orderId);
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
                Session["mforderVo"] = mforderVo;
            }
        }

        protected void hiddenassociation_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                bool DeleteAccount;
                //CustomerAccountsVo customeraccountvo = (CustomerAccountsVo)Session["customerAccountVo"];
                //int Account = customeraccountvo.AccountId;
                //DeleteAccount = customerAccountsBo.DeleteInsuranceAccount(Account);
                //orderbo.DeleteOrder(advisorVo.advisorId);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('OrderList','none');", true);
            }
        }
        protected void hdnCustomerId_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnCustomerId.Value.ToString().Trim()))
            {
                customerId = int.Parse(hdnCustomerId.Value);
               
                //customerVo = customerBo.GetCustomer(int.Parse(txtIndividualCustomer_autoCompleteExtender.ContextKey));
            }
        }
        protected void ddlCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIndividualCustomer.Text = string.Empty;
            txtIndividualCustomer.Enabled = true;
            hdnIndividualOrGroup.Value = ddlCustomerType.SelectedItem.Value;
            rquiredFieldValidatorIndivudialCustomer.Visible = true;
            if (ddlCustomerType.SelectedItem.Value == "0")
            {
               
                customerType = "GROUP";
                if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllGroupCustomers";
                    }
                    else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMGroupCustomers";
                    }
                }

                else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetBMParentCustomerNames";
                    }
                    if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllGroupCustomers";
                    }
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                    }
                    if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMGroupCustomers";
                    }
                }
                lblselectCustomer.Visible = true;
                txtIndividualCustomer.Visible = true;
            }
            else if (ddlCustomerType.SelectedItem.Value == "1")
            {
                customerType = "IND";

                //rquiredFieldValidatorIndivudialCustomer.Visible = true;
                if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";

                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {

                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllIndividualCustomers";
                    }
                    else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMIndividualCustomers";
                    }
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllIndividualCustomers";
                    }
                    else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMIndividualCustomers";
                    }
                }
                lblselectCustomer.Visible = true;
                txtIndividualCustomer.Visible = true;
            }
            else
            {
                txtIndividualCustomer.Enabled = false;
                hdnCustomerId.Value = null;
                lblselectCustomer.Visible = false;
                txtIndividualCustomer.Visible = false;
            }
        }
    }
}
