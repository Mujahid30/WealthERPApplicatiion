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
        AdvisorVo advisorVo = new AdvisorVo();
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
                txtOrderTo.SelectedDate = DateTime.Now;
                txtOrderFrom.SelectedDate = DateTime.Now.AddMonths(-1);
                //  BindOrderStatus();
                //if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                //{
                //    AutoCompleteExtender2_txtOrderSubbrokerCode.ContextKey = advisorVo.advisorId.ToString();
                //    AutoCompleteExtender2_txtOrderSubbrokerCode.ServiceMethod = "GetAgentCodeAssociateDetails";

                //}

                if (Request.QueryString["FormAction"] != null)
                {
                    if (Request.QueryString["FormAction"].Trim() == "54ECOrder_Back")
                    {
                        string issueId;
                        string fromDt = "";
                        string todt = "";
                        string type = "";

                        issueId = Request.QueryString["issueId"].ToString();
                        fromDt = Request.QueryString["fromDt"].ToString();
                        todt = Request.QueryString["todt"].ToString();
                        type = Request.QueryString["type"].ToString();

                        ddlProduct.SelectedValue = "Bonds";
                        BindNcdCategory();
                        ddlCategory.SelectedValue = "FICGCG";
                        BindIssueName(ddlCategory.SelectedValue);
                        ddlIssueName.SelectedValue = issueId;
                        txtOrderFrom.SelectedDate = Convert.ToDateTime(fromDt); ;
                        txtOrderTo.SelectedDate = Convert.ToDateTime(todt);

                        BindOrderMissMatchDetails();
                    }
                }

            }

        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "4")
            {
                trReprocess.Visible = true;
            }

            HandlingLinkbuttons();
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

        protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
        {



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
        private void CreateBulkOrderFromAllotment()
        {
            int i = 0;
            int count = 0;
            string allotmentIds = string.Empty;
            foreach (GridDataItem gvRow in gvOrderRecon.Items)
            {


                CheckBox chk = (CheckBox)gvRow.FindControl("chkId");
                if (chk.Checked)
                {
                    i++;
                    allotmentIds = allotmentIds + "," + gvOrderRecon.MasterTableView.DataKeyValues[gvRow.ItemIndex]["COAD_Id"].ToString();

                }

            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptOrderRecon", "alert('Please select a record!');", true);

                return;
            }
            else
            {
                onlineNCDBackOfficeBo.CreateBulkOrderFromAllotment(allotmentIds, out count);
                if (count >= 1)
                {
                    ShowMessage(count + " Orders Created Successfully", "S");

                }

                else
                {
                    ShowMessage("Orders not Created.", "F");
                }
            }

        }


        protected void HandlingLinkbuttons()
        {
            gvOrderRecon.MasterTableView.GetColumn("action").Visible = false;
            gvOrderRecon.MasterTableView.GetColumn("AddOrder").Visible = false;
            gvOrderRecon.MasterTableView.GetColumn("OrderEdit").Visible = false;

            gvOrderRecon.MasterTableView.GetColumn("editColumn").Visible = false;
            gvOrderRecon.MasterTableView.GetColumn("MissmatchType").Visible = false;
            gvOrderRecon.MasterTableView.GetColumn("issueName").Visible = true;
            if (ddlType.SelectedValue == "4")
            {
                gvOrderRecon.MasterTableView.GetColumn("action").Visible = true;
                gvOrderRecon.MasterTableView.GetColumn("AddOrder").Visible = true;
                gvOrderRecon.MasterTableView.GetColumn("editColumn").Visible = false;
                gvOrderRecon.MasterTableView.GetColumn("MissmatchType").Visible = false;
                gvOrderRecon.MasterTableView.GetColumn("AddOrder").Visible = true;
            }
            else if (ddlType.SelectedValue == "5")
            {
                gvOrderRecon.MasterTableView.GetColumn("OrderEdit").Visible = false;
                gvOrderRecon.MasterTableView.GetColumn("editColumn").Visible = false;
                gvOrderRecon.MasterTableView.GetColumn("MissmatchType").Visible = true;
                gvOrderRecon.MasterTableView.GetColumn("action").Visible = true;
                gvOrderRecon.MasterTableView.GetColumn("AddOrder").Visible = false;

            }
            else if (ddlType.SelectedValue == "3")
            {

                gvOrderRecon.MasterTableView.GetColumn("issueName").Visible = false;
                gvOrderRecon.MasterTableView.GetColumn("OrderEdit").Visible = false;
                gvOrderRecon.MasterTableView.GetColumn("editColumn").Visible = true;
                gvOrderRecon.MasterTableView.GetColumn("AddOrder").Visible = false;

            }
            else if (ddlType.SelectedValue == "2")
            {

                gvOrderRecon.MasterTableView.GetColumn("OrderEdit").Visible = false;
                gvOrderRecon.MasterTableView.GetColumn("MissmatchType").Visible = false;
                gvOrderRecon.MasterTableView.GetColumn("editColumn").Visible = false;
                gvOrderRecon.MasterTableView.GetColumn("AddOrder").Visible = false;

            }

        }

        protected void BindOrderMissMatchDetails()
        {



            dBindOrderMissMatchDetails = onlineNCDBackOfficeBo.GetOrderMissMatchDetails(int.Parse(ddlIssueName.SelectedValue), "PR", (ddlProduct.SelectedValue != "IP") ? ddlCategory.SelectedValue : "IP", int.Parse(ddlOrderStatus.SelectedValue), Convert.ToDateTime(txtOrderFrom.SelectedDate), Convert.ToDateTime(txtOrderTo.SelectedDate), int.Parse(ddlType.SelectedValue));
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
            HandlingLinkbuttons();
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
           // rgMatch.DataSource = dtBindOrderMatchDetails;
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
            if (e.Item is GridFooterItem)
            {
                GridFooterItem FooterItem = (GridFooterItem)e.Item;

                AutoCompleteExtender AutoCompleteExtender2 = (AutoCompleteExtender)e.Item.FindControl("AutoCompleteExtender2");
              //  AutoCompleteExtender2.ContextKey = advisorVo.advisorId.ToString();
                AutoCompleteExtender AutoCompleteExtender2_txtOrderSubbrokerCode = (AutoCompleteExtender)e.Item.FindControl("AutoCompleteExtender2_txtOrderSubbrokerCode");
                //AutoCompleteExtender2_txtOrderSubbrokerCode.ContextKey = advisorVo.advisorId.ToString();


            }
            if (e.Item is GridDataItem)
            {

                //GridDataItem item = e.Item as GridDataItem;

                //TextBox txtApplicationNo = (TextBox)item.FindControl("txtApplicationNo");
                //if (ddlType.SelectedValue == "2")
                //    txtApplicationNo.ReadOnly = false;
                //else
                //    txtApplicationNo.ReadOnly = true;

                //   LinkButton lnkOrderEntry = (LinkButton)item.FindControl("lnkOrderEntry");

                // lnkMatch.Visible = false;
                // lnkOrderEntry.Visible = false;
                //// item["editColumn"].Visible = false;
                // //item["MissmatchType"].Visible = false;
                // gvOrderRecon.MasterTableView.GetColumn("editColumn").Visible = false;
                // gvOrderRecon.MasterTableView.GetColumn("MissmatchType").Visible = false;
                // if (ddlType.SelectedValue == "4")
                // {
                //     lnkMatch.Visible = true;
                //     gvOrderRecon.MasterTableView.GetColumn("editColumn").Visible = true;


                // }
                //else if (ddlType.SelectedValue == "3")
                //{

                //    lnkMatch.Visible = false;
                //    lnkOrderEntry.Visible = true;                 
                //    item["editColumn"].Visible = false;
                //    item["issueName"].Text = string.Empty;
                //    //item["MissmatchType"].Visible = false;


                //}
                //else if (ddlType.SelectedValue == "2")
                //{

                //    lnkMatch.Visible = false;
                //    lnkOrderEntry.Visible = true;
                //    item["editColumn"].Visible = true;
                //    //item["MissmatchType"].Visible = true;

                //}
                //else
                //{
                //    lnkMatch.Visible = false;
                //    lnkOrderEntry.Visible = false;
                //    item["editColumn"].Visible = false;

                //}

            }
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode) && e.Item.ItemIndex != -1)
            {
                GridEditFormItem editform = (GridEditFormItem)e.Item;
                TextBox txtApplicationNo = (TextBox)editform.FindControl("txtApplicationNo");
                if (ddlType.SelectedValue == "2")
                    txtApplicationNo.Enabled = false;
                else
                    txtApplicationNo.Enabled = true;
            }
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem item = e.Item as GridEditFormItem;
                AutoCompleteExtender AutoCompleteExtender2 = (AutoCompleteExtender)item.FindControl("AutoCompleteExtender2");
                AutoCompleteExtender2.ContextKey = advisorVo.advisorId.ToString();
                AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetails";



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
            int orderQty = 0;
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                int allotedOrderId = Convert.ToInt32(gvOrderRecon.MasterTableView.DataKeyValues[e.Item.ItemIndex]["COAD_Id"].ToString());
                if (!string.IsNullOrEmpty(gvOrderRecon.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CFIOD_Quantity"].ToString()))
                    orderQty = Convert.ToInt32(gvOrderRecon.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CFIOD_Quantity"].ToString());
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

            //BindOrderMissMatchDetails();
            //  BindOrderMatchDetails();

        }
        protected void BulkOrderGeneration_Click(object sender, EventArgs e)
        {
            CreateBulkOrderFromAllotment();
            BindOrderMissMatchDetails();

        }
        protected void btnApply_OnClick(object sender, EventArgs e)
        {
            int i = 0;
            Button btn = (Button)sender;

            GridFooterItem footerItem = (GridFooterItem)gvOrderRecon.MasterTableView.GetItems(GridItemType.Footer)[0];
            TextBox newSubBrokerCode = (TextBox)footerItem.FindControl("txtAllotedSubBrokerCode");
            TextBox newOrderSubBrokerCode = (TextBox)footerItem.FindControl("txtOrderSubbrokerCode");
            foreach (GridDataItem dataItem in gvOrderRecon.MasterTableView.Items)
            {
                if ((dataItem.FindControl("chkId") as CheckBox).Checked)
                {
                    i = i + 1;
                }
            }
            switch (btn.ID)
            {
                case "btnApply":

                    if (i == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Select Item To Apply SubBrokerCode !');", true);
                        return;
                    }
                    else
                    {
                        foreach (GridDataItem dataItem in gvOrderRecon.MasterTableView.Items)
                        {
                            if ((dataItem.FindControl("chkId") as CheckBox).Checked)
                            {
                                TextBox txtSubBrokerCode = dataItem.FindControl("txtAllotedSubBrokerCodetoAllotment") as TextBox;
                                txtSubBrokerCode.Text = newSubBrokerCode.Text;
                            }
                        }

                    }
                    break;
                case "btnOrderApply":

                    if (i == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Select Item To Apply Order SubBrokerCode !');", true);
                        return;
                    }
                    else
                    {
                        foreach (GridDataItem dataItem in gvOrderRecon.MasterTableView.Items)
                        {
                            if ((dataItem.FindControl("chkId") as CheckBox).Checked)
                            {
                                TextBox txtOrderSubBrokerCode = dataItem.FindControl("txtOrderAgentCode") as TextBox;
                                txtOrderSubBrokerCode.Text = newOrderSubBrokerCode.Text;
                            }
                        }

                    }
                    break;
            }

        }
        protected void btnReprocess_Click(object sender, EventArgs e)
        {

            BindOrderMatchDetails();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Reprocess done')", true);
            BindOrderMissMatchDetails();
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
            string issueId = ddlIssueName.SelectedValue;
            DateTime AllotmentDate = Convert.ToDateTime(gvOrderRecon.MasterTableView.DataKeyValues[rowindex]["AllotmentOROrderDate"].ToString());

            string fromDt = txtOrderFrom.SelectedDate.ToString();
            string todt = txtOrderTo.SelectedDate.ToString();
            string type = ddlType.SelectedValue;

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('FixedIncome54ECOrderEntry','FormAction=" + "NonMfRecon_OrderAdd" + "&SubBrokerCode=" + SubBrokerCode + "&PAN=" + PAN + "&ApplicationNumberAlloted=" + ApplicationNumberAlloted + "&Quantity=" + Quantity + "&issueId=" + issueId + "&AllotmentDate=" + AllotmentDate + "&fromDt=" + fromDt + "&todt=" + todt + "&type=" + type + "');", true);

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

        private void ShowMessage(string msg, string type)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
        }
        protected void btnUpdateAllDetails_Update(object sender, EventArgs e)
        {
            int i = 0;
            List<int> issuedetails;
            Button btn = (Button)sender;
            hdnissueId.Value = gvOrderRecon.MasterTableView.DataKeyValues[0]["AIM_IssueId"].ToString();
            issuedetails = onlineNCDBackOfficeBo.GetOrderRelatedDetails(int.Parse(hdnissueId.Value), ddlCategory.SelectedValue);
            foreach (GridDataItem dataItem in gvOrderRecon.MasterTableView.Items)
            {
                if ((dataItem.FindControl("chkId") as CheckBox).Checked)
                {
                    i = i + 1;
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Select to Update SubBrokerCode/order quentity/PAN !');", true);
                return;
            }
            else
            {
                DataTable dtSubBrokerCode = new DataTable();
                dtSubBrokerCode.Columns.Add("allotmentorderId", typeof(Int32));
                dtSubBrokerCode.Columns.Add("allotmentsubBrokerCode", typeof(String));
                dtSubBrokerCode.Columns.Add("allotmentorderQty", typeof(Int32));
                dtSubBrokerCode.Columns.Add("allotmentPAN", typeof(String));
                dtSubBrokerCode.Columns.Add("orderId", typeof(Int32));
                dtSubBrokerCode.Columns.Add("OrdersubBrokerCode", typeof(String));
                dtSubBrokerCode.Columns.Add("orderQty", typeof(Int32));
                dtSubBrokerCode.Columns.Add("CFIOD_DetailsId", typeof(String));
                DataRow drSubBrokerCode;
                foreach (GridDataItem radItem in gvOrderRecon.MasterTableView.Items)
                {
                    if ((radItem.FindControl("chkId") as CheckBox).Checked)
                    {
                        hdnFIorderId.Value = gvOrderRecon.MasterTableView.DataKeyValues[radItem.ItemIndex]["CFIOD_DetailsId"].ToString();
                        txtAgentId.Value = gvOrderRecon.MasterTableView.DataKeyValues[radItem.ItemIndex]["AAC_AdviserAgentId"].ToString();
                        string OrderPAN = gvOrderRecon.MasterTableView.DataKeyValues[radItem.ItemIndex]["C_PANNum"].ToString();
                        drSubBrokerCode = dtSubBrokerCode.NewRow();
                        int j = radItem.ItemIndex + 1;
                        drSubBrokerCode["allotmentorderId"] = int.Parse(gvOrderRecon.MasterTableView.DataKeyValues[radItem.ItemIndex]["COAD_Id"].ToString());
                        drSubBrokerCode["orderId"] = int.Parse(gvOrderRecon.MasterTableView.DataKeyValues[radItem.ItemIndex]["CO_OrderId"].ToString());
                        TextBox allotmentorderQty = radItem.FindControl("COAD_Quantity") as TextBox;
                        TextBox allotmentsubBrokerCode = radItem.FindControl("txtAllotedSubBrokerCodetoAllotment") as TextBox;
                        TextBox allotmentPAN = radItem.FindControl("txtAllotmentPAN") as TextBox;
                        TextBox orderQty = radItem.FindControl("txtOrderQuantity") as TextBox;
                        TextBox OrdersubBrokerCode = radItem.FindControl("txtOrderAgentCode") as TextBox;
                        string trimPAN = allotmentPAN.Text.Trim(' ');
                        drSubBrokerCode["CFIOD_DetailsId"] = hdnFIorderId.Value;
                        if (Convert.ToInt32(orderQty.Text) >= issuedetails[0] && Convert.ToInt32(orderQty.Text) <= issuedetails[1])
                        {
                            drSubBrokerCode["orderQty"] = orderQty.Text;
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Order Quentity should be between min and max.row no " + j + " data is not match  !!');", true);
                            return;
                        }
                        if ((allotmentorderQty.Text == orderQty.Text) && (allotmentsubBrokerCode.Text == OrdersubBrokerCode.Text) && (trimPAN.ToUpper() == OrderPAN.ToUpper()))
                        {
                            drSubBrokerCode["allotmentsubBrokerCode"] = allotmentsubBrokerCode.Text;
                            drSubBrokerCode["allotmentorderQty"] = allotmentorderQty.Text;
                            drSubBrokerCode["allotmentPAN"] = allotmentPAN.Text;
                            drSubBrokerCode["OrdersubBrokerCode"] = OrdersubBrokerCode.Text;
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Provide same as order details and allotment details.row no " + j + " data is not match');", true);
                            return;
                        }
                        dtSubBrokerCode.Rows.Add(drSubBrokerCode);

                    }
                   
                }
                bool result = onlineNCDBackOfficeBo.UpdateNewOrderAllotmentSubBrokerCodeDetails(dtSubBrokerCode, (ddlProduct.SelectedValue != "IP") ? ddlCategory.SelectedValue : "IP", userVo.UserId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Update Successfully !!');", true);
            }
        }
    }
}