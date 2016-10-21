using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using WealthERP.Base;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;


using VoUser;
using BoCommon;

using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;

using BoOps;
using VOAssociates;


namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineNCDOrderMatchExceptionHandling : System.Web.UI.UserControl
    {

        UserVo userVo;
        AdvisorVo advisorVo;
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        OperationBo operationBo = new OperationBo();
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        OnlineNCDBackOfficeVo onlineNCDBackOfficeVo;
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        string categoryCode = "";
        string userType;
        string UserTitle;
        string AgentCode;
        string agentCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            int adviserId = advisorVo.advisorId;
            userType = Session[SessionContents.CurrentUserRole].ToString();
            associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
            {
                userType = "advisor";
                // userType = "admin";

            }
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
            {
                userType = "bm";

            }
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
            {
                userType = "rm";

            }

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
            {
                userType = "associates";
                if (UserTitle == "SubBroker")
                {
                    associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                    if (associateuserheirarchyVo.AgentCode != null)
                    {
                        AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                    }
                    else
                        AgentCode = "0";
                }
                else
                {
                    associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                    if (associateuserheirarchyVo.AgentCode != null)
                    {
                        AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                    }
                    else
                        AgentCode = "0";
                }
            }
            if (!IsPostBack)
            {
                // BindOrderStatus();
                BindBusinessChannel();
                BindCategory();
                if (userType == "associates")
                {
                    //ddlBChannnel.Items[2].Enabled = false;
                    //ddlBChannnel.Items[1].Enabled = false;
                    ddlType.Items[0].Enabled = false;

                }
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (ddlOrderStatus.SelectedValue == "OR")
            {
                pnlBtns.Visible = true;
            }
            else
            {
                pnlBtns.Visible = false;
            }
            btnNcdIpoExport.Visible = true;
            BindOrdersGrid();
        }

        protected void gvOfflineAllotment_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                gvOfflineAllotment.MasterTableView.GetColumn("COAD_CertificateNo").Visible = false;
                if (ddlSubCategory.SelectedValue == "FICGCG")
                {
                    gvOfflineAllotment.MasterTableView.GetColumn("COAD_CertificateNo").Visible = true;

                }

            }
        }
        //protected void cbOrderSelect_changed(object sender, EventArgs e)
        //{
        //    CheckBox cb = (CheckBox)sender;
        //    foreach (GridDataItem item in gvOrders.Items)
        //    {
        //        CheckBox chkI = item.FindControl("cbAutoMatch") as CheckBox;
        //        chkI.Checked = cb.Checked;
        //    }



        //}

        protected void btnNcdIpoExport_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlType.SelectedValue == "2")
            {
                if (ddlSubCategory.SelectedValue == "FISDSD")
                {

                    RadMultiSeries.ExportSettings.OpenInNewWindow = true;
                    RadMultiSeries.ExportSettings.IgnorePaging = true;
                    RadMultiSeries.ExportSettings.HideStructureColumns = true;
                    RadMultiSeries.ExportSettings.ExportOnlyData = true;
                    RadMultiSeries.ExportSettings.FileName = "NcdIpo Recon";
                    RadMultiSeries.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                    RadMultiSeries.MasterTableView.ExportToExcel();
                }
                else
                {
                    gvOfflineAllotment.ExportSettings.OpenInNewWindow = true;
                    gvOfflineAllotment.ExportSettings.IgnorePaging = true;
                    gvOfflineAllotment.ExportSettings.HideStructureColumns = true;
                    gvOfflineAllotment.ExportSettings.ExportOnlyData = true;
                    gvOfflineAllotment.ExportSettings.FileName = "NcdIpo Recon";
                    gvOfflineAllotment.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                    gvOfflineAllotment.MasterTableView.ExportToExcel();

                }
            }
            else
            {

                gvOrders.ExportSettings.OpenInNewWindow = true;
                gvOrders.ExportSettings.IgnorePaging = true;
                gvOrders.ExportSettings.HideStructureColumns = true;
                gvOrders.ExportSettings.ExportOnlyData = true;
                gvOrders.ExportSettings.FileName = "NcdIpo Recon";
                gvOrders.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gvOrders.MasterTableView.ExportToExcel();
            }
            

        }
        protected void btnManualMatchGo_Click(object sender, EventArgs e)
        {
            Button btnMatchGO = (Button)sender;
            GridEditableItem editedItem = btnMatchGO.NamingContainer as GridEditableItem;
            DropDownList ddlIssuer = (DropDownList)editedItem.FindControl("ddlIssuer");
            RadGrid gvUnmatchedAllotments = (RadGrid)editedItem.FindControl("gvUnmatchedAllotments");
            System.Web.UI.HtmlControls.HtmlTableRow trUnMatchedGrd = (System.Web.UI.HtmlControls.HtmlTableRow)editedItem.FindControl("trUnMatchedGrd");
            System.Web.UI.HtmlControls.HtmlTableRow trUnMatchedBtns = (System.Web.UI.HtmlControls.HtmlTableRow)editedItem.FindControl("trUnMatchedBtns");
            trUnMatchedGrd.Visible = true;
            trUnMatchedBtns.Visible = true;

            BindUnmatchedAllotmentsGrid(gvUnmatchedAllotments, Convert.ToInt32(ddlIssuer.SelectedValue));

        }

        protected void gvOrders_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //int issueId;
            //try
            //{
            //    if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode) && e.Item.ItemIndex != -1)
            //    {
            //        //  DropDownList ddlIssuer = (DropDownList)e.Item.FindControl("ddlIssuer");
            //        RadGrid gvUnmatchedAllotments = (RadGrid)e.Item.FindControl("gvUnmatchedAllotments");
            //        Button btnMatchGO = (Button)e.Item.FindControl("btnMatchGO");
            //        System.Web.UI.HtmlControls.HtmlTableRow trUnMatchedGrd = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trUnMatchedGrd");
            //        System.Web.UI.HtmlControls.HtmlTableRow trUnMatchedBtns = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trUnMatchedBtns");
            //        System.Web.UI.HtmlControls.HtmlTableRow trUnMatchedddl = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trUnMatchedddl");


            //        issueId = Convert.ToInt32(gvOrders.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIM_IssueId"].ToString());
            //        trUnMatchedGrd.Visible = true;
            //        trUnMatchedBtns.Visible = true;
            //        trUnMatchedddl.Visible = false;
            //        //     BindIssuer(ddlIssuer);
            //        BindUnmatchedAllotmentsGrid(gvUnmatchedAllotments, issueId);

            //    }
            //    if (e.Item is GridDataItem && e.Item.ItemIndex != -1)
            //    {
            //        string Status = Convert.ToString(gvOrders.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WOS_OrderStepCode"].ToString());
            //        // EditCommandColumn colmatch = (EditCommandColumn)e.Item["Match"];

            //        GridDataItem item = (GridDataItem)e.Item;
            //        LinkButton editButton = (LinkButton)item["Match"].Controls[0];

            //        if (Status == "")
            //        {
            //            editButton.Visible = false;
            //        }
            //        if (Status.Trim().ToUpper() == "OR")
            //        {
            //            editButton.Visible = true;
            //        }
            //        else
            //        {
            //            editButton.Visible = false;
            //        }



            //    }

            //}
            //catch (BaseApplicationException Ex)
            //{
            //    throw Ex;
            //}
            //catch (Exception Ex)
            //{
            //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
            //    NameValueCollection FunctionInfo = new NameValueCollection();
            //    FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:gvOrders_ItemDataBound()");
            //    object[] objects = new object[2];
            //    objects[1] = sender;
            //    objects[2] = e;
            //    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
            //    exBase.AdditionalInformation = FunctionInfo;
            //    ExceptionManager.Publish(exBase);
            //    throw exBase;
            //}
        }

        protected void RadMultiSeries_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtOrder = new DataTable();
            dtOrder = (DataTable)Cache[userVo.UserId.ToString() + "OrdersMatchMultiSeries"];// Cache["OrderMIS" + userVo.UserId];
            RadMultiSeries.DataSource = dtOrder;
        }
        protected void gvOrders_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtOrder = new DataTable();
            dtOrder = (DataTable)Cache[userVo.UserId.ToString() + "OrdersMatch"];// Cache["OrderMIS" + userVo.UserId];
            gvOrders.DataSource = dtOrder;
        }

        protected void gvOrders_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                RadGrid gvUnmatchedAllotments = (RadGrid)e.Item.FindControl("gvUnmatchedAllotments");
                int orderId = Convert.ToInt32(gvOrders.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CO_OrderId"].ToString());
                ManualMatch(gvUnmatchedAllotments, orderId);
                //BindOrdersGrid(Convert.ToInt32(ddlIssue.SelectedValue), categoryCode, ddlOrderStatus.SelectedValue, Convert.ToDateTime(txtFromDate.SelectedDate), Convert.ToDateTime(txtToDate.SelectedDate),Convert.ToInt32(ddlBChannnel.SelectedValue));
            }
            if (e.Item is GridDataItem)
            {
                GridDataItem gvr = (GridDataItem)e.Item;
                if (e.CommandName.ToString() != "Filter")
                {
                    if (e.CommandName.ToString() != "Sort")
                    {
                        if (e.CommandName.ToString() != "Page")
                        {
                            if (e.CommandName.ToString() != "ChangePageSize")
                            {

                                int selectedRow = gvr.ItemIndex + 1;
                                int orderid = int.Parse(gvr.GetDataKeyValue("CO_OrderId").ToString());
                                if (e.CommandName == "Select")
                                {
                                    Response.Redirect("ControlHost.aspx?pageid=OnlineAdviserNCDOrderBook&orderid=" + orderid + "", false);
                                }
                            }
                        }
                    }
                }
            }
        }


        private void ManualMatch(RadGrid gvUnmatchedAllotments, int orderId)
        {
            int i = 0;
            bool result = false;
            int isAlloted = 0;
            int isUpdated = 0;

            foreach (GridDataItem gvRow in gvUnmatchedAllotments.Items)
            {
                CheckBox chk = (CheckBox)gvRow.FindControl("cbUnMatched");
                if (chk.Checked)
                {
                    i++;
                }
            }
            if (i > 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select One record!');", true);
                //BindOrdersGrid(Convert.ToInt32(ddlIssue.SelectedValue), categoryCode, ddlOrderStatus.SelectedValue, Convert.ToDateTime(txtFromDate.SelectedDate), Convert.ToDateTime(txtToDate.SelectedDate));
                return;
            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a record!');", true);
                //BindOrdersGrid(Convert.ToInt32(ddlIssue.SelectedValue), categoryCode, ddlOrderStatus.SelectedValue, Convert.ToDateTime(txtFromDate.SelectedDate), Convert.ToDateTime(txtToDate.SelectedDate));
                return;
            }

            foreach (GridDataItem gdi in gvUnmatchedAllotments.Items)
            {
                if (((CheckBox)gdi.FindControl("cbUnMatched")).Checked == true)
                {
                    int allotmentId = Convert.ToInt32(gdi["AIA_Id"].Text);// Convert.ToInt32(gvUnmatchedAllotments.MasterTableView.DataKeyValues[selectedRow - 1]["AIA_Id"].ToString());
                    onlineNCDBackOfficeBo.UpdateNcdOrderMannualMatch(orderId, allotmentId, ref isAlloted, ref isUpdated);
                    if (isAlloted == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please fill Allotment Date.Not able to match');", true);
                        result = false;
                    }
                    else if (isUpdated == 1)
                    {
                        result = true;

                    }
                }
            }

            if (result == true)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Match is done');", true);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Not able to match');", true);
            //BindOrdersGrid(Convert.ToInt32(ddlIssue.SelectedValue), ddlProduct.SelectedValue, ddlOrderStatus.SelectedValue, Convert.ToDateTime(txtFromDate.SelectedDate), Convert.ToDateTime(txtToDate.SelectedDate));

        }

        protected void gvUnmatchedAllotments_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtUNmatchedAllotments = new DataTable();
            RadGrid gvUnmatchedAllotments = (RadGrid)sender;
            dtUNmatchedAllotments = (DataTable)Cache[userVo.UserId.ToString() + "UnAllotedOrders"];// Cache["OrderMIS" + userVo.UserId];
            gvUnmatchedAllotments.DataSource = dtUNmatchedAllotments;

        }

        protected void gvOfflineAllotment_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtOrdersMatch = new DataTable();
            RadGrid gvOfflineAllotment = (RadGrid)sender;
            dtOrdersMatch = (DataTable)Cache[userVo.UserId.ToString() + "HoldingOffline"];// Cache["OrderMIS" + userVo.UserId];
            gvOfflineAllotment.DataSource = dtOrdersMatch;

        }
        private void BindUnmatchedAllotmentsGrid(RadGrid gvUnmatchedAllotments, int issuerID)
        {
            try
            {
                DataTable dtUNmatchedAllotments = new DataTable();
                dtUNmatchedAllotments = onlineNCDBackOfficeBo.GetUnmatchedAllotments(advisorVo.advisorId, issuerID).Tables[0];
                gvUnmatchedAllotments.DataSource = dtUNmatchedAllotments;
                gvUnmatchedAllotments.DataBind();
                if (Cache[userVo.UserId.ToString() + "UnAllotedOrders"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "UnAllotedOrders");
                Cache.Insert(userVo.UserId.ToString() + "UnAllotedOrders", dtUNmatchedAllotments);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindSeriesCategoryGrid()");
                object[] objects = new object[1];
                //objects[1] = issuerId;
                //objects[2] = issueId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindOrdersGrid()
        {
            try
            {
                DataTable dtOrdersMatch = new DataTable();

                if (ddlType.SelectedValue == "2")
                {
                    pnlGrid.Visible = false;
                    dtOrdersMatch = onlineNCDBackOfficeBo.GetAdviserOrders(int.Parse(ddlIssue.SelectedValue), ddlProduct.SelectedValue, advisorVo.advisorId, 2, userType, AgentCode, ddlSubCategory.SelectedValue).Tables[0];
                    if (ddlType.SelectedValue == "2" && ddlSubCategory.SelectedValue == "FISDSD")
                    {
                        RadMultiSeries.DataSource = dtOrdersMatch;
                        RadMultiSeries.DataBind();
                        PnlMultiSeries.Visible = true;
                        pnlOfflineNCDIPO.Visible = false;
                        if (Cache[userVo.UserId.ToString() + "OrdersMatchMultiSeries"] != null)
                            Cache.Remove(userVo.UserId.ToString() + "OrdersMatchMultiSeries");
                        Cache.Insert(userVo.UserId.ToString() + "OrdersMatchMultiSeries", dtOrdersMatch);
                    }
                    else
                    {
                       
                        gvOfflineAllotment.DataSource = dtOrdersMatch;
                        gvOfflineAllotment.DataBind();
                        pnlOfflineNCDIPO.Visible = true;
                        PnlMultiSeries.Visible = false;
                        if (Cache[userVo.UserId.ToString() + "HoldingOffline"] != null)
                            Cache.Remove(userVo.UserId.ToString() + "HoldingOffline");
                        Cache.Insert(userVo.UserId.ToString() + "HoldingOffline", dtOrdersMatch);
                    }
                }
                else
                {
                      
                    dtOrdersMatch = onlineNCDBackOfficeBo.GetAdviserOrders(int.Parse(ddlIssue.SelectedValue), ddlProduct.SelectedValue, advisorVo.advisorId, 1, userType, AgentCode, ddlSubCategory.SelectedValue).Tables[0];
                    gvOrders.DataSource = dtOrdersMatch;
                    gvOrders.DataBind();
                    pnlGrid.Visible = true;
                    PnlMultiSeries.Visible = false;
                    pnlOfflineNCDIPO.Visible = false;
                    if (Cache[userVo.UserId.ToString() + "OrdersMatch"] != null)
                        Cache.Remove(userVo.UserId.ToString() + "OrdersMatch");
                    Cache.Insert(userVo.UserId.ToString() + "OrdersMatch", dtOrdersMatch);

                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }

        private void BindOrderStatus()
        {
            DataSet dsOrderStaus;
            DataTable dtOrderStatus;
            dsOrderStaus = operationBo.Get_Onl_NcdOrderStatus();
            dtOrderStatus = dsOrderStaus.Tables[0];
            if (dtOrderStatus.Rows.Count > 0)
            {
                ddlOrderStatus.DataSource = dtOrderStatus;
                ddlOrderStatus.DataValueField = dtOrderStatus.Columns["WOS_OrderStepCode"].ToString();
                ddlOrderStatus.DataTextField = dtOrderStatus.Columns["WOS_OrderStep"].ToString();
                ddlOrderStatus.DataBind();
            }
            ddlOrderStatus.Items.Insert(0, new ListItem("Select", "Select"));
        }



        private void BindBusinessChannel()
        {

            DataTable dtBUsinessChanel;
            dtBUsinessChanel = onlineNCDBackOfficeBo.GetBusinessChannel();

            if (dtBUsinessChanel.Rows.Count > 0)
            {
                ddlBChannnel.DataSource = dtBUsinessChanel;
                ddlBChannnel.DataValueField = dtBUsinessChanel.Columns["WCMV_LookupId"].ToString();
                ddlBChannnel.DataTextField = dtBUsinessChanel.Columns["WCMV_Name"].ToString();
                ddlBChannnel.DataBind();
            }
            ddlBChannnel.Items.Insert(0, new ListItem("Select", "Select"));
        }


        protected void ddlBChannnel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBChannnel.SelectedValue != "Select")
                BindIssue();

        }
        protected void ddlOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProduct.SelectedValue == "FI")
            {
                tdSubCategory.Visible = true;
                tdlblSubCategory.Visible = true;
            }
            else
            {
                tdSubCategory.Visible = false;
                tdlblSubCategory.Visible = false;
                BindIssue();
            }



        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "1")
            {
                ddlSubCategory.Items.FindByValue("FICDCD").Enabled = false;
                ddlSubCategory.Items.FindByValue("FICGCG").Enabled = false;
            }
            else
            {
                ddlSubCategory.Items.FindByValue("FICDCD").Enabled = true;
                ddlSubCategory.Items.FindByValue("FICGCG").Enabled = true;
            }

        }
        private void BindIssue()
        {
            try
            {
                ddlIssue.Items.Clear();
                DataSet dsIssuer = new DataSet();
                dsIssuer = onlineNCDBackOfficeBo.GetIssuerAllotmentIssues(advisorVo.advisorId, ddlProduct.SelectedValue,0, "PR", ddlSubCategory.SelectedValue);
                if (dsIssuer.Tables[0].Rows.Count > 0)
                {
                    ddlIssue.DataSource = dsIssuer;
                    ddlIssue.DataValueField = dsIssuer.Tables[0].Columns["AIM_IssueId"].ToString();
                    ddlIssue.DataTextField = dsIssuer.Tables[0].Columns["AIM_IssueName"].ToString();
                    ddlIssue.DataBind();
                }
                ddlIssue.Items.Insert(0, new ListItem("Select", "Select"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDOrderMatchExceptionHandling.ascx.cs:BindIssuer()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            //try
            //{
            //    DataSet dsIssuer = new DataSet();
            //    dsIssuer = onlineNCDBackOfficeBo.GetIssuer();
            //    if (dsIssuer.Tables[0].Rows.Count > 0)
            //    {
            //        ddlIssuer.DataSource = dsIssuer;
            //        ddlIssuer.DataValueField = dsIssuer.Tables[0].Columns["PI_issuerId"].ToString();
            //        ddlIssuer.DataTextField = dsIssuer.Tables[0].Columns["PI_IssuerName"].ToString();
            //        ddlIssuer.DataBind();
            //    }
            //    ddlIssuer.Items.Insert(0, new ListItem("Select", "Select"));

            //}
            //catch (BaseApplicationException Ex)
            //{
            //    throw Ex;
            //}
            //catch (Exception Ex)
            //{
            //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
            //    NameValueCollection FunctionInfo = new NameValueCollection();
            //    FunctionInfo.Add("Method", "OnlineNCDOrderMatchExceptionHandling.ascx.cs:BindIssuer()");
            //    object[] objects = new object[0];
            //    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
            //    exBase.AdditionalInformation = FunctionInfo;
            //    ExceptionManager.Publish(exBase);
            //    throw exBase;
            //}

        }
        protected void ddlSubCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubCategory.SelectedValue != "Select")
                BindIssue();
        }
        private void BindIssuerIssue()
        {
            try
            {
                //DataSet dsIssuer = new DataSet();
                //dsIssuer = onlineNCDBackOfficeBo.GetIssuerIssue(advisorVo.advisorId, ddlProduct.SelectedValue);
                //if (dsIssuer.Tables[0].Rows.Count > 0)
                //{
                //    ddlIssue.DataSource = dsIssuer;
                //    ddlIssue.DataValueField = dsIssuer.Tables[0].Columns["AIM_IssueId"].ToString();
                //    ddlIssue.DataTextField = dsIssuer.Tables[0].Columns["AIM_IssueName"].ToString();
                //    ddlIssue.DataBind();
                //}
                //ddlIssue.Items.Insert(0, new ListItem("Select", "Select"));
                //ddlIssue.Items.Insert(1, new ListItem("All", "0"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDOrderMatchExceptionHandling.ascx.cs:BindIssuer()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }


        private bool NCDAutoMatch()
        {
            int OrderId = 0;
            int isAlloted = 0;
            int isUpdated = 0;

            bool result = false;
            foreach (GridDataItem gdi in gvOrders.Items)
            {
                if (((CheckBox)gdi.FindControl("cbAutoMatch")).Checked == true)
                {
                    int selectedRow = gdi.ItemIndex + 1;
                    int applicationNo = Convert.ToInt32(gdi["CO_ApplicationNo"].Text);
                    string dpId = gdi["CEDA_DPId"].Text;
                    OrderId = Convert.ToInt32(gvOrders.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderId"].ToString());
                    onlineNCDBackOfficeBo.UpdateNcdAutoMatch(OrderId, applicationNo, dpId, ref  isAlloted, ref isUpdated);
                    if (isAlloted == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please fill Allotment Date.Not able to match');", true);
                        result = false;
                    }
                    else if (isUpdated == 1)
                    {
                        result = true;

                    }

                }
            }

            return result;
        }
        private void BindCategory()
        {

            DataTable dtBindCategory;
            dtBindCategory = onlineNCDBackOfficeBo.BindNcdCategory("SubInstrumentCat", "").Tables[0];

            if (dtBindCategory.Rows.Count > 0)
            {
                ddlSubCategory.DataSource = dtBindCategory;
                ddlSubCategory.DataValueField = dtBindCategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlSubCategory.DataTextField = dtBindCategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlSubCategory.DataBind();
            }
            ddlSubCategory.Items.Insert(0, new ListItem("Select", "Select"));
        }

        protected void btnAutoMatch_Click(object sender, EventArgs e)
        {
            int i = 0;
            bool result = false;

            foreach (GridDataItem gvRow in gvOrders.Items)
            {
                CheckBox chk = (CheckBox)gvRow.FindControl("cbAutoMatch");
                if (chk.Checked)
                {
                    i++;
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a record!');", true);
                //BindMISGridView();
                return;
            }

            result = NCDAutoMatch();

            if (result == true)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Match is done');", true);
                //BindOrdersGrid(Convert.ToInt32(ddlIssue.SelectedValue), ddlProduct.SelectedValue, ddlOrderStatus.SelectedValue, Convert.ToDateTime(txtFromDate.SelectedDate), Convert.ToDateTime(txtToDate.SelectedDate), Convert.ToInt32(ddlBChannnel.SelectedValue));
            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Not able to match');", true);


        }
        //protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlType.SelectedValue == "1")
        //    {

        //        ddlSubCategory.Items.FindByValue("FICDCD").Enabled = false;
        //        
        //    }
        //    else
        //    {
        //        ddlSubCategory.Items.FindByValue("FICDCD").Enabled = true;
        //    }



        //}


        //protected void lnkOrderId_Click(object sender, EventArgs e)
        //{
        //}

    }
}
