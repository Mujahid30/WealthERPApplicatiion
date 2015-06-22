using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using WealthERP.Base;
using BoCommon;
using BoOnlineOrderManagement;
using System.Data;
using Telerik.Web.UI;
using AjaxControlToolkit;
namespace WealthERP.OffLineOrderManagement
{
    public partial class OnlineOfflineOrderRecon : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        AdvisorVo advisorVo;
        DataTable dBindOrderMissMatchDetails;
        protected void Page_Load(object sender, EventArgs e)
        {
       
            SessionBo.CheckSession();
            userBo = new UserBo();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            radOrderDetails.VisibleOnPageLoad = false;

            if (!IsPostBack)
            {
                //  BindOrderStatus();
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {

                    AutoCompleteExtender2_txtOrderSubbrokerCode.ContextKey = advisorVo.advisorId.ToString();
                    AutoCompleteExtender2_txtOrderSubbrokerCode.ServiceMethod = "GetAgentCodeAssociateDetails";

                }
                //if (Request.QueryString["product"] != null)
                //{
                //    ddlProduct.SelectedValue = Request.QueryString["product"].ToString();
                //    if (ddlProduct.SelectedValue == "Bonds")
                //        BindNcdCategory();
                //    ddlCategory.SelectedValue = Request.QueryString["category"].ToString();
                //    BindIssueName(ddlCategory.SelectedValue);
                //    ddlIssueName.SelectedValue = Request.QueryString["issueId"].ToString();
                //    ddlOrderStatus.SelectedValue = Request.QueryString["orderstatus"].ToString();
                //    ddlSearchType.SelectedValue = Request.QueryString["searchtype"].ToString();
                //    BindOrderMissMatchDetails();
                //}
            }

        }
        
        protected void btnGo_Click(object sender, EventArgs e)
        {
            BindOrderMissMatchDetails();
        }

        protected void BindIssueName(string category)
        {
            DataTable dtGetIssueName = new DataTable();

            dtGetIssueName = onlineNCDBackOfficeBo.GetIssueNamePRoductWise(category);
            ddlIssueName.DataSource = dtGetIssueName;
            ddlIssueName.DataValueField = dtGetIssueName.Columns["AIM_IssueId"].ToString();
            ddlIssueName.DataTextField = dtGetIssueName.Columns["AIM_IssueName"].ToString();
            ddlIssueName.DataBind();
            ddlIssueName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }
        protected void ddlProduct_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            tdCategorydropdown.Visible = false;
            tdcategory.Visible = false;
            if (ddlProduct.SelectedValue == "Bonds")
            {
                tdCategorydropdown.Visible = true;
                tdcategory.Visible = true;
                BindNcdCategory();
            }
            else
            {
                BindIssueName("FIFIIP");
            }
        }
        private void BindNcdCategory()
        {
            DataTable dtCategory = new DataTable();
            dtCategory = onlineNCDBackOfficeBo.BindNcdCategory("SubInstrumentCat", "").Tables[0];
            if (dtCategory.Rows.Count > 0)
            {
                ddlCategory.DataSource = dtCategory;
                ddlCategory.DataValueField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlCategory.DataTextField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlCategory.DataBind();
            }
            ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));
        }

        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlCategory.SelectedValue == "FICDCD")
            //{
            //    ddlSearchType.Items[1].Enabled = true;
            //    ddlSearchType.Items[0].Enabled = false;

            //}
            //else
            //{
            //    ddlSearchType.Items[1].Enabled = false;
            //    ddlSearchType.Items[0].Enabled = true;
            //}
            if (ddlCategory.SelectedValue != "Select")
                BindIssueName(ddlCategory.SelectedValue);

        }
        private void BindOrderStatus()
        {
            //OnlineMFOrderBo OnlineMFOrderBo = new OnlineMFOrderBo();
            //ddlOrderStatus.Items.Clear();
            //DataSet dsOrderStatus;
            //DataTable dtOrderStatus;
            //dsOrderStatus = OnlineMFOrderBo.GetOrderIssueStatus();
            //dtOrderStatus = dsOrderStatus.Tables[0];
            //if (dtOrderStatus.Rows.Count > 0)
            //{

            //    for (int i = dtOrderStatus.Rows.Count - 1; i >= 0; i--)
            //    {
            //        if (dtOrderStatus.Rows[i][1].ToString() == "INPROCESS" || dtOrderStatus.Rows[i][1].ToString() == "EXECUTED" || dtOrderStatus.Rows[i][1].ToString() == "NOT ALLOTED" || dtOrderStatus.Rows[i][1].ToString() == "REJECTED" || dtOrderStatus.Rows[i][1].ToString() == "ORDERED")
            //            dtOrderStatus.Rows[i].Delete();
            //    }
            //    dtOrderStatus.AcceptChanges();
            //    ddlOrderStatus.DataSource = dtOrderStatus;
            //    ddlOrderStatus.DataTextField = dtOrderStatus.Columns["WOS_OrderStep"].ToString();
            //    ddlOrderStatus.DataValueField = dtOrderStatus.Columns["WOS_OrderStepCode"].ToString();
            //    ddlOrderStatus.DataBind();
            //}
            //ddlOrderStatus.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void BindOrderMissMatchDetails()
        {
            dBindOrderMissMatchDetails = onlineNCDBackOfficeBo.GetOrderMissMatchDetails(int.Parse(ddlIssueName.SelectedValue), "PR", (ddlProduct.SelectedValue != "IP") ? ddlCategory.SelectedValue : "IP", int.Parse(ddlOrderStatus.SelectedValue), Convert.ToDateTime(txtOrderFrom.SelectedDate), Convert.ToDateTime(txtOrderTo.SelectedDate));
            if (Cache["OrderRecon" + userVo.UserId.ToString()] == null)
            {
                Cache.Insert("OrderRecon" + userVo.UserId.ToString(), dBindOrderMissMatchDetails);
            }
            else
            {
                Cache.Remove("OrderRecon" + userVo.UserId.ToString());
                Cache.Insert("OrderRecon" + userVo.UserId.ToString(), dBindOrderMissMatchDetails);
            }
            gvOrderRecon.DataSource = dBindOrderMissMatchDetails;
            gvOrderRecon.DataBind();
            pnlOrderRecon.Visible = true;
            ibtExportSummary.Visible = true;
        }

        protected void BindOrderMatchDetails()
        {
            DataTable dtBindOrderMatchDetails = new DataTable();
            dtBindOrderMatchDetails = onlineNCDBackOfficeBo.GetMatchDetails(int.Parse(ddlIssueName.SelectedValue));

        }

        protected void rgMatch_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtBindOrderMatchDetails = new DataTable();
            dtBindOrderMatchDetails = (DataTable)Cache["OrderMatch" + userVo.UserId.ToString()];
            rgMatch.DataSource = dtBindOrderMatchDetails;
        }

        protected void gvOrderRecon_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            string rcbType = string.Empty;

            radOrderDetails.VisibleOnPageLoad = false;
            DataTable dtrr = new DataTable();
            dtrr = (DataTable)Cache["OrderRecon" + userVo.UserId.ToString()];
            if (dtrr != null)
            {

                if (ViewState["MissmatchType"] != null)
                    rcbType = ViewState["MissmatchType"].ToString();
                if (!string.IsNullOrEmpty(rcbType))
                {
                    DataView dvStaffList = new DataView(dtrr, "MissmatchType = '" + rcbType + "'", "MissmatchType", DataViewRowState.CurrentRows);
                    gvOrderRecon.DataSource = dvStaffList.ToTable();

                }
                else
                {
                    gvOrderRecon.DataSource = dtrr;

                }
            }

        }
        protected void gvOrderRecon_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem item = (GridDataItem)e.Item;
            //    AjaxControlToolkit.AutoCompleteExtender a = (AjaxControlToolkit.AutoCompleteExtender)item.FindControl("AutoCompleteExtender2");
            //    AutoCompleteExtender AutoCompleteExtender2 = (AutoCompleteExtender)item.FindControl("AutoCompleteExtender2");
            //    //AutoCompleteExtender2.ContextKey = advisorVo.advisorId.ToString();
            //    //AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetails";

            //}
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem item = e.Item as GridEditFormItem;
                //TextBox txt_lect_in = item.FindControl("txt_lect_in") as TextBox;
                AutoCompleteExtender AutoCompleteExtender2 = (AutoCompleteExtender)item.FindControl("AutoCompleteExtender2");
                AutoCompleteExtender2.ContextKey = advisorVo.advisorId.ToString();
                AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetails";

                //Access your textbox heer
            }
            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem header = (GridHeaderItem)e.Item;

                if (ddlCategory.SelectedValue == "FICDCD")
                {
                    header["COAD_Quantity"].Text = "Alloted Amt.";
                    header["CFIOD_Quantity"].Text = "Order AMt.";
                }


            }

            if (e.Item is GridFilteringItem && e.Item.ItemIndex == -1)
            {
                GridFilteringItem filterItem = (GridFilteringItem)e.Item;

                RadComboBox RadComboBoxIN = (RadComboBox)filterItem.FindControl("RadComboBoxRR");
                dBindOrderMissMatchDetails = (DataTable)Cache["OrderRecon" + userVo.UserId.ToString()]; ;
                Session["dt"] = dBindOrderMissMatchDetails;
                DataTable dtcustMIS = new DataTable();
                dtcustMIS.Columns.Add("MissmatchType");
                DataRow drcustMIS;
                foreach (DataRow dr in dBindOrderMissMatchDetails.Rows)
                {
                    drcustMIS = dtcustMIS.NewRow();
                    drcustMIS["MissmatchType"] = dr["MissmatchType"].ToString();
                    dtcustMIS.Rows.Add(drcustMIS);
                }
                DataView view = new DataView(dBindOrderMissMatchDetails);
                DataTable distinctValues = view.ToTable(true, "MissmatchType");
                RadComboBoxIN.DataSource = distinctValues;
                RadComboBoxIN.DataValueField = dtcustMIS.Columns["MissmatchType"].ToString();
                RadComboBoxIN.DataTextField = dtcustMIS.Columns["MissmatchType"].ToString();
                RadComboBoxIN.DataBind();
            }
        }
        protected void gvOrderRecon_OnItemCommand(object source, GridCommandEventArgs e)
        {
            bool result = false;
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                int allotedOrderId = Convert.ToInt32(gvOrderRecon.MasterTableView.DataKeyValues[e.Item.ItemIndex]["COAD_Id"].ToString());
                int orderQty = Convert.ToInt32(gvOrderRecon.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CFIOD_Quantity"].ToString());
                string odrerSubbroker = gvOrderRecon.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AAC_AgentCode"].ToString();
                string orderPAN = gvOrderRecon.MasterTableView.DataKeyValues[e.Item.ItemIndex]["C_PANNum"].ToString();
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                TextBox txtAllotedQty = (TextBox)editedItem.FindControl("txtAllotedQty");
                TextBox txtAllotedSubBrokerCode = (TextBox)editedItem.FindControl("txtAllotedSubBrokerCode");
                TextBox txtPAN = (TextBox)editedItem.FindControl("txtPAN");

                if ((txtAllotedQty.Text == orderQty.ToString()) && (txtAllotedSubBrokerCode.Text == odrerSubbroker) && (txtPAN.Text == orderPAN))
                {
                    result = onlineNCDBackOfficeBo.UpdateAllotedMissMatchOrder(allotedOrderId, Convert.ToInt32(txtAllotedQty.Text), txtAllotedSubBrokerCode.Text, txtPAN.Text, (ddlProduct.SelectedValue != "IP") ? ddlCategory.SelectedValue : "IP");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please fill all the information same as ordered ')", true);

                }

            }
            BindOrderMissMatchDetails();
            //  BindOrderMatchDetails();

        }
        protected void OnClick_lnkOrderEntry(object sender, EventArgs e)
        {
            divorderQty.Visible = true;
            radOrderDetails.VisibleOnPageLoad = true; radOrderDetails.Visible = true;
            LinkButton lnkOrderEntry = (LinkButton)sender;
            GridDataItem grd = (GridDataItem)lnkOrderEntry.NamingContainer;
            txtOrderSubbrokerCode.Text = gvOrderRecon.MasterTableView.DataKeyValues[grd.ItemIndex]["AAC_AgentCode"].ToString();
            txtPAN.Text = gvOrderRecon.MasterTableView.DataKeyValues[grd.ItemIndex]["C_PANNum"].ToString();
            txtOrderQty.Text = gvOrderRecon.MasterTableView.DataKeyValues[grd.ItemIndex]["CFIOD_Quantity"].ToString();
            hdnissueId.Value = gvOrderRecon.MasterTableView.DataKeyValues[grd.ItemIndex]["AIM_IssueId"].ToString();
            hdnorderId.Value = gvOrderRecon.MasterTableView.DataKeyValues[grd.ItemIndex]["CO_OrderId"].ToString();
            hdnFIorderId.Value = gvOrderRecon.MasterTableView.DataKeyValues[grd.ItemIndex]["CFIOD_DetailsId"].ToString();
            txtAgentId.Value = gvOrderRecon.MasterTableView.DataKeyValues[grd.ItemIndex]["AAC_AdviserAgentId"].ToString();
            //if (ddlCategory.SelectedValue != "FICDCD")
            //    lblOrderQty.Text = "Order Amt.";
            if (ddlProduct.SelectedValue == "IP")
                txtOrderQty.Enabled = false;
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FixedIncome54ECOrderEntry", "loadcontrol( 'FixedIncome54ECOrderEntry','action=" + "Edit" + "&orderId=" + orderId + "&agentcode=" + agentcode + "&customeId=" + customeId + "&product=" + ddlProduct.SelectedValue +
            //    "&category=" + ddlCategory.SelectedValue + "&issueId=" + ddlIssueName.SelectedValue + "&orderstatus=" + ddlOrderStatus.SelectedValue + "&searchtype=" + ddlSearchType.SelectedValue + "');", true);
        }
        protected void lnkMatch_SelectedIndexChanged(object sender, EventArgs e)
        {

            int rowindex1 = ((GridDataItem)((LinkButton)sender).NamingContainer).RowIndex;
            int rowindex = (rowindex1 / 2) - 1;
            string PAN = gvOrderRecon.MasterTableView.DataKeyValues[rowindex]["COAD_PAN"].ToString();
            string SubBrokerCode = gvOrderRecon.MasterTableView.DataKeyValues[rowindex]["COAD_SubBrokerCode"].ToString();
            string ApplicationNumberAlloted = gvOrderRecon.MasterTableView.DataKeyValues[rowindex]["CO_ApplicationNumberAlloted"].ToString();
            string Quantity = gvOrderRecon.MasterTableView.DataKeyValues[rowindex]["COAD_Quantity"].ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('FixedIncome54ECOrderEntry','FormAction=" + "NonMfRecon_OrderAdd" + "&SubBrokerCode=" + SubBrokerCode + "&PAN=" + PAN + "&ApplicationNumberAlloted=" + ApplicationNumberAlloted + "&Quantity=" + Quantity + " ');", true);

        }



        protected void rcbContinents1_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            ////persist the combo selected value  

            if (ViewState["MissmatchType"] != null)
            {

                Combo.SelectedValue = ViewState["MissmatchType"].ToString();
            }

        }
        protected void RadComboBoxRR_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

            RadComboBox dropdown = o as RadComboBox;
            ViewState["MissmatchType"] = dropdown.SelectedValue.ToString();
            if (ViewState["MissmatchType"] != "")
            {
                GridColumn column = gvOrderRecon.MasterTableView.GetColumnSafe("MissmatchType");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvOrderRecon.MasterTableView.Rebind();

            }
            else
            {
                GridColumn column = gvOrderRecon.MasterTableView.GetColumnSafe("MissmatchType");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvOrderRecon.MasterTableView.Rebind();


            }

        }
        protected void gvOrderRecon_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && dBindOrderMissMatchDetails != null)
            {
                BoundColumn bc = new BoundColumn();
                RadComboBox rcb = new RadComboBox();
                RadComboBoxItem rcbi = new RadComboBoxItem("Select Reject Reason", "0");

                //rcb = (RadComboBox)e.Item.FindControl("rcbRejectReasonFilter");

                //rcb.DataSource = dBindOrderMissMatchDetails;
                //rcb.DataTextField = "MissmatchType";
                //rcb.DataValueField = "MissmatchType";

                //rcb.DataBind();


                //rcb.Items.Insert(0, rcbi);
            }
        }
        protected void gvOrderRecon_PreRender(object sender, EventArgs e)
        {
            //if (IsPostBack)
            //{
            //    radOrderDetails.VisibleOnPageLoad = false;
            //    if (!IsPostBack)
            //        radOrderDetails.VisibleOnPageLoad = true;
            //}

            //radOrderDetails.VisibleOnPageLoad = false;

            if (gvOrderRecon.MasterTableView.FilterExpression != string.Empty)
            {
                RefreshCombos();
            }
        }
        protected void RefreshCombos()
        {
            dBindOrderMissMatchDetails = (DataTable)Cache["OrderRecon" + userVo.UserId.ToString()];
            DataView view = new DataView(dBindOrderMissMatchDetails);
            DataTable distinctValues = view.ToTable();
            DataRow[] rows = distinctValues.Select(gvOrderRecon.MasterTableView.FilterExpression.ToString());
            gvOrderRecon.MasterTableView.Rebind();
        }
        protected void btnSubmit_Update(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            List<int> issuedetails = new List<int>();
            issuedetails = onlineNCDBackOfficeBo.GetOrderRelatedDetails(int.Parse(hdnissueId.Value), ddlCategory.SelectedValue);
            if (ddlCategory.SelectedValue == "FICDCD")
            {
                if (Convert.ToInt32(txtOrderQty.Text) >= issuedetails[0] && Convert.ToInt32(txtOrderQty.Text) <= issuedetails[1])
                {
                    onlineNCDBackOfficeBo.UpdateOrderMissMatchOrder(int.Parse(hdnFIorderId.Value), int.Parse(hdnorderId.Value), int.Parse(txtOrderQty.Text), txtOrderSubbrokerCode.Text, int.Parse(txtAgentId.Value), ddlCategory.SelectedValue);
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Amount should be between min and max')" + issuedetails[0] + issuedetails[0], true);

                }
            }
            if (ddlCategory.SelectedValue == "FICGCG")
            {
                if (Convert.ToInt32(txtOrderQty.Text) >= issuedetails[0] && Convert.ToInt32(txtOrderQty.Text) <= issuedetails[1])
                {
                    onlineNCDBackOfficeBo.UpdateOrderMissMatchOrder(int.Parse(hdnFIorderId.Value), int.Parse(hdnorderId.Value), int.Parse(txtOrderQty.Text), txtOrderSubbrokerCode.Text, int.Parse(txtAgentId.Value), ddlCategory.SelectedValue);
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please enter correct information')" + issuedetails[0] + issuedetails[0], true);
                }
            }
            if (ddlProduct.SelectedValue == "IP" || ddlCategory.SelectedValue == "FISDSD")
            {
                onlineNCDBackOfficeBo.UpdateOrderMissMatchOrder(int.Parse(hdnFIorderId.Value), int.Parse(hdnorderId.Value), (!string.IsNullOrEmpty(txtOrderQty.Text)) ? int.Parse(txtOrderQty.Text) : 0, txtOrderSubbrokerCode.Text, int.Parse(txtAgentId.Value), (ddlProduct.SelectedValue != "IP") ? ddlCategory.SelectedValue : "IP");
            }
            BindOrderMissMatchDetails();
        }
        public void ibtExport_OnClick(object sender, ImageClickEventArgs e)
        {
            gvOrderRecon.MasterTableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
            gvOrderRecon.ExportSettings.OpenInNewWindow = true;
            gvOrderRecon.ExportSettings.IgnorePaging = true;
            gvOrderRecon.ExportSettings.HideStructureColumns = true;
            gvOrderRecon.ExportSettings.ExportOnlyData = true;
            gvOrderRecon.ExportSettings.FileName = "Non MF Recon";
            gvOrderRecon.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvOrderRecon.MasterTableView.ExportToExcel();

        }

        
    }
}