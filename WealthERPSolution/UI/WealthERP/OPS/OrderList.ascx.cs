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

namespace WealthERP.OPS
{
    public partial class OrderList : System.Web.UI.UserControl
    {
        OrderBo orderbo = new OrderBo();
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
            userType = Session["UserType"].ToString().ToLower();

            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;           
            gvOrderList.Visible = false;

            if (!IsPostBack)
            {
                BindBranchDropDown();
                BindRMDropDown();
                if (userType == "bm")
                {
                    ddlBranch.SelectedValue = bmID.ToString();
                    ddlBranch.Enabled = false;
                }
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

        protected void gvOrderList_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
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
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + " ');", true);

                // Set the VO into the Session
                //insuranceVo = insuranceBo.GetInsuranceAssetLI(insuranceId, out dtAssociationId);
                //Session["dtAssociationId"] = dtAssociationId;
                //Session["insuranceVo"] = insuranceVo;
                //Session["customerAccountVo"] = customerAccountsBo.GetCustomerInsuranceAccount(insuranceVo.AccountId);

                if (ddlAction.SelectedItem.Value.ToString() == "Edit")
                {
                    action = "Edit";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + "&strAction=" + action + " ');", true);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','action=edit');", true);
                }
                if (ddlAction.SelectedItem.Value.ToString() == "View")
                {
                    action = "View";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + "&strAction=" + action + " ');", true);
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
            }
            else
            {
                txtIndividualCustomer.Enabled = false;
                hdnCustomerId.Value = null;
            }
        }
    }
}