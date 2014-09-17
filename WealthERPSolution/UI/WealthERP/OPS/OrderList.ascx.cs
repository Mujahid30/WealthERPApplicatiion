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
using VOAssociates;
using BOAssociates;
using VoOps;
using System.Collections;

namespace WealthERP.OPS
{
    public partial class OrderList : System.Web.UI.UserControl
    {
        OrderBo orderbo = new OrderBo();
        MFOrderBo mforderBo = new MFOrderBo();
        MFOrderVo mforderVo = new MFOrderVo();
        OrderVo orderVo = new OrderVo();
        OperationBo operationBo = new OperationBo();
        RMVo rmVo = new RMVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AssociatesVO associatesVo = new AssociatesVO();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        FIOrderBo fiorderBo = new FIOrderBo();
        FIOrderVo fiorderVo = new FIOrderVo();
        AdvisorVo advisorVo;
        UserVo userVo;
        string userType;
        int customerId = 0;
        int AgentId = 0;
        int IsAssociate;
        int bmID;
        int rmId;
        string AgentCode;
        string UserTitle;
        string customerType = string.Empty;
        Hashtable mfParameters ;
        int btnConunt = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userType = Session[SessionContents.CurrentUserRole].ToString();
            associatesVo = (AssociatesVO)Session["associatesVo"];
            userVo = (UserVo)Session["userVo"];
           
            //else
            //{
            //    Session.Remove("mfParametersHT");
            //}
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
            {
                userType = "advisor";
                // userType = "admin";
                txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
            }
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
            {
                userType = "bm";
                txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
            }
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
            {
                userType = "rm";
                txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
            }
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
            {
                userType = "associates";
                if (UserTitle =="SubBroker")
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
                txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
            }
            rmVo = (RMVo)Session[SessionContents.RmVo];
            bmID = rmVo.RMId;
            rmId = rmVo.RMId;

            gvOrderList.Visible = false;
            trExportFilteredDupData.Visible = false;
            if (!IsPostBack)
            {
                DateTime fromDate = DateTime.Now.AddDays(-1);
                txtFromDate.SelectedDate = fromDate;
                txtToDate.SelectedDate = DateTime.Now;
                if (userType == "advisor" || userType == "rm")
                {
                    BindBranchDropDown();
                    AgentCode = "0";
                    BindRMDropDown();
                    trZCCS.Visible = false;
                    BindSubBrokerCode(userType);
                    //BindSubBrokerName();
                    if (userType == "rm")
                    {
                        trZCCS.Visible = false;
                        trRMbranch.Visible = true;
                        ddlBranch.Enabled = false;
                        ddlRM.SelectedValue = rmVo.RMId.ToString();
                        ddlRM.Enabled = false;
                    }
                }
                else if (userType == "rm")
                {
                    //ddlBranch.SelectedValue = rmVo.RMId.ToString();
                    // ddlRM.SelectedValue=rmVo.RMId.ToString();
                    //Action.Visible = false;
                    //ddlAction.Visible = false;
                    //  trBranchRM.Visible = false;
                }
                else if (userType == "bm")
                {
                    trZCCS.Visible = false;
                    AgentCode = "0";
                    trRMbranch.Visible = true;
                    BindBranchForBMDropDown();
                    BindRMforBranchDropdown(0, bmID);
                }

                //BindBranchDropDown();
                //BindRMDropDown();
                BindOrderStatus();
                if (userType == "bm")
                {
                    ddlBranch.SelectedValue = bmID.ToString();
                    //ddlBranch.Enabled = false;
                }
                else if (userType == "rm")
                {
                    ddlBranch.Enabled = false;
                    ddlRM.SelectedValue = rmVo.RMId.ToString();
                    ddlRM.Enabled = false;
                }

                lblselectCustomer.Visible = true;
                txtIndividualCustomer.Visible = true;
                if (userType == "associates")
                {
                    //BindSubBrokerName();
                    //BindSubBrokerCode();
                    BindBranchDropDown();
                    BindRMDropDown();
                    BindSubBrokerAgentCode(AgentCode);
                    //trBrokerCodeName.Visible = false;
                    AgentId = associatesVo.AAC_AdviserAgentId;
                    //ddlCustomerType.Visible = false;
                    //lblSelectTypeOfCustomer.Visible = false;
                    trRMbranch.Visible = false;
                    lblselectCustomer.Visible = true;
                    txtIndividualCustomer.Visible = true;
                    txtIndividualCustomer.Enabled = true;

                }


                //---------  Fixedincome

                gvFIOrderList.Visible = false;

            }
            if (Request.QueryString["Mfaction"] != null)
            {
                if (Session["mfParametersHT"] != null )
                {
                    btnConunt = 2;
                    SetMfParameters();
                    btnGo_Click(this, null);
                    //Session.Remove("mfParametersHT");
                    Request.QueryString["Mfaction"].Remove(0);

                    return;
                }
            }
        }
        private void SetMfParameters()
        {
               mfParameters = new Hashtable();

               mfParameters = (Hashtable)Session["mfParametersHT"];
                ddlBranch.SelectedIndex = Convert.ToInt32(mfParameters["BranchId"]);
                ddlRM.SelectedIndex = Convert.ToInt32(mfParameters["RMId"]);

                txtFromDate.SelectedDate = Convert.ToDateTime(mfParameters["Fromdate"]);
                txtToDate.SelectedDate = Convert.ToDateTime(mfParameters["Todate"]);
                BindOrderStatus();
                ddlOrderStatus.SelectedIndex = Convert.ToInt32(mfParameters["OrderStatus"]);
              
                
                txtIndividualCustomer.Text = mfParameters["Custname"].ToString();
                userType = mfParameters["userType"].ToString();
           
                hdnCustomerId.Value = mfParameters["CustId"].ToString();
                if (userType == "associates")
                {
                    AgentCode = mfParameters["AgentCode"].ToString();
                    BindSubBrokerAgentCode(AgentCode);
                }
                else
                {
                    BindSubBrokerCode(userType);
                    trZCCS.Visible = false;
                }

                //ddlBrokerCode.SelectedItem.Value = mfParameters["SubBrokerCodeAdv"].ToString();
                ddlBrokerCode.SelectedIndex = Convert.ToInt32(mfParameters["SubBrokerCode"]);
                //hdnAgentCode.Value = mfParameters["AgentCode"].ToString();
                //hdnAgentId.Value = mfParameters["AgentId"].ToString();
           
                    }
        private void BindBranchForBMDropDown()
        {
            try
            {
                DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(0, bmID, 0);
                if (ds != null)
                {
                    ddlBranch.DataSource = ds.Tables[1]; ;
                    ddlBranch.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindOrderStatus()
        {
            DataSet dsOrderStaus;
            DataTable dtOrderStatus;
            dsOrderStaus = operationBo.GetOrderStatus();
            dtOrderStatus = dsOrderStaus.Tables[0];
            if (dtOrderStatus.Rows.Count > 0)
            {
                ddlOrderStatus.DataSource = dtOrderStatus;
                ddlOrderStatus.DataValueField = dtOrderStatus.Columns["XS_StatusCode"].ToString();
                ddlOrderStatus.DataTextField = dtOrderStatus.Columns["XS_Status"].ToString();
                ddlOrderStatus.DataBind();
            }
            ddlOrderStatus.Items.Insert(0, "All");
        }
        protected void BindSubBrokerCode(string userType)
        {
            DataTable dtSubbrokerCode = new DataTable();
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
            {
                dtSubbrokerCode = orderbo.GetAllAgentListForOrder(advisorVo.advisorId, "admin");
              
            }
            else if (userType == "rm" || userType == "bm")
            {
                dtSubbrokerCode = orderbo.GetAllAgentListForOrder(advisorVo.advisorId, userType);
              
                //dtSubbrokerCode = orderbo.GetSubBrokerCode(advisorVo.advisorId, rmId, bmID, userType);
            }
            //else if (userType == "associates")
            //{
            //    dtSubbrokerCode = orderbo.GetSubBrokerAgentCode(AgentCode);
            //}
            if (dtSubbrokerCode.Rows.Count != 0)
            {
                ddlBrokerCode.DataSource = dtSubbrokerCode;
                ddlBrokerCode.DataValueField = dtSubbrokerCode.Columns["AgentId"].ToString();
                ddlBrokerCode.DataTextField = dtSubbrokerCode.Columns["AgentName"].ToString();
                ddlBrokerCode.DataBind();
            }
            ddlBrokerCode.Items.Insert(0, new ListItem("Select(SubBroker Code-Name-Type)All", "0"));
            //ddlBrokerCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }
        protected void BindSubBrokerAgentCode(string AgentCode)
        {
            DataTable dtSubbrokerCode = new DataTable();

            dtSubbrokerCode = orderbo.GetSubBrokerAgentCode(advisorVo.advisorId,AgentCode);

            if (dtSubbrokerCode.Rows.Count > 0)
            {
                ddlBrokerCode.DataSource = dtSubbrokerCode;
                ddlBrokerCode.DataValueField = dtSubbrokerCode.Columns["ACC_AgentId"].ToString();
                ddlBrokerCode.DataTextField = dtSubbrokerCode.Columns["AAC_AgentCode"].ToString();
                ddlBrokerCode.DataBind();
            }
             ddlBrokerCode.Items.Insert(0, new ListItem("All", "0"));

        }
        //protected void BindSubBrokerName()
        //{
        //    DataTable dtSubbrokerName;
        //    dtSubbrokerName = orderbo.GetSubBrokerName(advisorVo.advisorId, rmId, bmID, userType);         
        //    if (dtSubbrokerName.Rows.Count > 0)
        //    {
        //        ddlSubBrokerName.DataSource = dtSubbrokerName;
        //        ddlSubBrokerName.DataValueField = dtSubbrokerName.Columns["AA_ContactPersonName"].ToString();
        //        ddlSubBrokerName.DataTextField = dtSubbrokerName.Columns["AA_ContactPersonName"].ToString();
        //        ddlSubBrokerName.DataBind();
        //    }
        //    //ddlSubBrokerName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        //}

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
        protected void ddlZonal_SelectedIndexChanged(object sender, EventArgs e)
        { }
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
            Cache.Remove("FIOrderList" + advisorVo.advisorId);
            Session.Remove("mfParametersHT");
          //  SetParameters();
            if (hdnOrderType.Value == "FI")
            {
                
                BindGvFiOrderList();
            }
            else
            {
                pnlFiOrderList.Visible = false ;
                BindGvOrderList();
            }

        }
        protected void BindGvFiOrderList()
        {
            //  if (userType != "associates")
            // {
            //SetParameters();
            //SetParameterSubbroker();
            // }
            SetParameters();
            SetParameterSubbroker();
            DataTable dtOrder = new DataTable();
          //  dtOrder = fiorderBo.GetOrderList(advisorVo.advisorId, "", "", Convert.ToDateTime(hdnTodate.Value), Convert.ToDateTime(hdnFromdate.Value), "", hdnCustomerId.Value, "FI", userType, 0, "", "");

            dtOrder = fiorderBo.GetOrderList(advisorVo.advisorId, hdnRMId.Value, hdnBranchId.Value, Convert.ToDateTime(hdnTodate.Value), Convert.ToDateTime(hdnFromdate.Value), hdnOrderStatus.Value, hdnCustomerId.Value, hdnOrderType.Value, userType, int.Parse(hdnAgentId.Value), hdnSubBrokerCode.Value, hdnAgentCode.Value);
            if (dtOrder.Rows.Count > 0)
            {
                // trExportFilteredDupData.Visible = true;
                if (Cache["FIOrderList" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("FIOrderList" + advisorVo.advisorId, dtOrder);
                }
                else
                {
                    Cache.Remove("FIOrderList" + advisorVo.advisorId);
                    Cache.Insert("FIOrderList" + advisorVo.advisorId, dtOrder);
                }
                gvFIOrderList .DataSource = dtOrder;
                gvFIOrderList.DataBind();
                gvFIOrderList.Visible = true;
                pnlFiOrderList.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
                //gvOrderList.Visible = true;
                ErrorMessage.Visible = false;
               tblMessage.Visible = false;
                //pnlOrderList.Visible = true;
                //  btnExportFilteredDupData.Visible = true;
            }
            else
            {
                gvFIOrderList.Visible = false;
                //tblMessage.Visible = true;
                //ErrorMessage.Visible = true;
                //btnExportFilteredDupData.Visible = false;
                //ErrorMessage.InnerText = "No Records Found...!";
                //pnlOrderList.Visible = false;
            }
            
        }
        private void FillHashtable()
    {
        mfParameters = new Hashtable();
        mfParameters.Add("BranchId", ddlBranch.SelectedIndex);
        mfParameters.Add("RMId", ddlRM.SelectedIndex);
        mfParameters.Add("Fromdate", txtFromDate.SelectedDate);
        mfParameters.Add("Todate", txtToDate.SelectedDate);
        mfParameters.Add("OrderStatus", ddlOrderStatus.SelectedIndex);
        mfParameters.Add("SubBrokerCode", ddlBrokerCode.SelectedIndex);
            
        mfParameters.Add("SubBrokerCodeAdv", ddlBrokerCode.SelectedItem.Value);
        mfParameters.Add("userType", userType);

        mfParameters.Add("AgentCode", AgentCode);
        mfParameters.Add("Custname", txtIndividualCustomer.Text);

        mfParameters.Add("CustId", hdnCustomerId.Value);
        //mfParameters.Add("AgentId", hdnAgentId.Value);
        Session["mfParametersHT"] = mfParameters;
 
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

            if (txtFromDate.SelectedDate.ToString() != "")
                hdnFromdate.Value = DateTime.Parse(txtFromDate.SelectedDate.ToString()).ToString();
            else
                hdnFromdate.Value = DateTime.MinValue.ToString();

            if (txtToDate.SelectedDate.ToString() != "")
                hdnTodate.Value = DateTime.Parse(txtToDate.SelectedDate.ToString()).ToString();
            else
                hdnTodate.Value = DateTime.MinValue.ToString();

            if (ddlOrderStatus.SelectedIndex != 0)
            {
                hdnOrderStatus.Value = ddlOrderStatus.SelectedValue.ToString();
            }
            else
            {
               // hdnOrderStatus.Value = "OMIP";
                hdnOrderStatus.Value =string.Empty;
            }
        }
        private void SetParameterSubbroker()
        {
            if (userType == "advisor" || userType == "rm" || userType == "bm")
            {
                hdnAgentCode.Value = "0";
                hdnAgentId.Value = "0";
                if (ddlBrokerCode.SelectedIndex != 0)
                {
                    hdnSubBrokerCode.Value = ddlBrokerCode.SelectedItem.Value.ToString();
                    ViewState["SubBrokerCode"] = hdnSubBrokerCode.Value;
                }
                else
                {
                    hdnSubBrokerCode.Value = "0";
                }               
            }
            else if (userType == "associates")
            {
               
                if (ddlBrokerCode.SelectedIndex != 0)
                {
                    hdnAgentCode.Value = ddlBrokerCode.SelectedItem.ToString();
                    hdnAgentId.Value = ddlBrokerCode.SelectedItem.Value.ToString(); 
                    
                }
                else
                {
                    hdnAgentCode.Value = AgentCode;
                    hdnAgentId.Value ="0";
                    //AgentId = int.Parse(ddlBrokerCode.SelectedItem.Value.ToString());
                }
                //AgentCode = ddlBrokerCode.SelectedValue.ToString();

            }
            //{
            //    if (ddlBrokerCode.SelectedIndex != 0)
            //    {
            //        hdnAgentCode.Value = ddlBrokerCode.SelectedValue.ToString();
            //        ViewState["hdnAgentCode"] = hdnAgentCode.Value;
            //    }
            //    else
            //    {
            //        hdnAgentCode.Value = "0";
            //    }
            //}
            // if (ddlSubBrokerName.SelectedIndex!=0)
            // {
            //     hdnSubBrokerName.Value = ddlSubBrokerName.SelectedItem.ToString();
            //     ViewState["SubBrokerName"] = hdnSubBrokerName.Value;
            // }
            //else
            // {
            //     hdnSubBrokerName.Value = "0";
            // }
        }
        protected void BindGvOrderList()
        {
            //  if (userType != "associates")
            // {
            SetParameters();
            SetParameterSubbroker();
            // }
            DataTable dtOrder = new DataTable();
            dtOrder = orderbo.GetOrderList(advisorVo.advisorId, hdnRMId.Value, hdnBranchId.Value, Convert.ToDateTime(hdnTodate.Value), Convert.ToDateTime(hdnFromdate.Value), hdnOrderStatus.Value, hdnCustomerId.Value, hdnOrderType.Value, userType, int.Parse(hdnAgentId.Value), hdnSubBrokerCode.Value, hdnAgentCode.Value);
            if (dtOrder.Rows.Count > 0)
            {
                trExportFilteredDupData.Visible = true;
                if (Cache["OrderList" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("OrderList" + advisorVo.advisorId, dtOrder);
                }
                else
                {
                    Cache.Remove("OrderList" + advisorVo.advisorId);
                    Cache.Insert("OrderList" + advisorVo.advisorId, dtOrder);
                }
                gvOrderList.DataSource = dtOrder;
                gvOrderList.DataBind();
                gvOrderList.Visible = true;
                ErrorMessage.Visible = false;
                tblMessage.Visible = false;
                pnlOrderList.Visible = true;
               btnExportFilteredDupData.Visible = true;
            }
            else
            {
                gvOrderList.Visible = false;
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                btnExportFilteredDupData.Visible = false;
                ErrorMessage.InnerText = "No Records Found...!";
                pnlOrderList.Visible = false;
            }
            FillHashtable();
        }
        protected void btnExportFilteredDupData_OnClick(object sender, ImageClickEventArgs e)
        {
            //gvOrderList.ExportSettings.OpenInNewWindow = true;
            //gvOrderList.ExportSettings.IgnorePaging = true;
            //foreach (GridFilteringItem filter in gvOrderList.MasterTableView.GetItems(GridItemType.FilteringItem))
            //{
            //    filter.Visible = false;
            //}
            //gvOrderList.MasterTableView.ExportToCSV();

            gvOrderList.ExportSettings.OpenInNewWindow = true;
            gvOrderList.ExportSettings.IgnorePaging = true;
            gvOrderList.ExportSettings.HideStructureColumns = true;
            gvOrderList.ExportSettings.ExportOnlyData = true;
            //gvOrderList.ExportSettings.FileName = "Customer List";
            gvOrderList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvOrderList.MasterTableView.ExportToExcel();




        }

        protected void gvOrderList_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            trExportFilteredDupData.Visible = true;
            DataTable dtGIDetails = new DataTable();
            dtGIDetails = (DataTable)Cache["OrderList" + advisorVo.advisorId];
            gvOrderList.Visible = true;
            this.gvOrderList.DataSource = dtGIDetails;
        }
        protected void gvFIOrderList_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            trExportFilteredDupData.Visible = true;
            DataTable dtFIGIDetails = new DataTable();
            dtFIGIDetails = (DataTable)Cache["FIOrderList" + advisorVo.advisorId];
            gvFIOrderList.Visible = true;
            this.gvFIOrderList.DataSource = dtFIGIDetails;
        }

        protected void gvFIOrderList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (userVo.UserType == "Advisor") return;

            if ((e.Item is GridDataItem) == false) return;

            GridDataItem item = (GridDataItem)e.Item;
            DateTime orderDate = DateTime.Parse(gvFIOrderList.MasterTableView.DataKeyValues[item.ItemIndex]["CO_OrderDate"].ToString());

            //if (userVo.UserType == "Advisor" && orderDate.Date == DateTime.Now.Date) return;

            RadComboBox actions = (RadComboBox)item.FindControl("ddlMenu");
            RadComboBoxItem rbcItem = actions.Items.FindItemByValue("Edit", true);
            rbcItem.Visible = false;
        }

        protected void gvOrderList_ItemDataBound(object sender, GridItemEventArgs e) {
            if (userVo.UserType == "Advisor") return;

            if ((e.Item is GridDataItem) == false) return;
            
            GridDataItem item = (GridDataItem)e.Item;
            DateTime orderDate = DateTime.Parse(gvOrderList.MasterTableView.DataKeyValues[item.ItemIndex]["CO_OrderDate"].ToString());

            //if (userVo.UserType == "Advisor" && orderDate.Date == DateTime.Now.Date) return;

            RadComboBox actions = (RadComboBox)item.FindControl("ddlMenu");
            RadComboBoxItem rbcItem = actions.Items.FindItemByValue("Edit", true);
            rbcItem.Visible = false;
        }

        protected void gvFIOrderList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {

                GridDataItem item = (GridDataItem)e.Item;
                string orderId = item.GetDataKeyValue("CO_OrderId").ToString();
                string customerId = item.GetDataKeyValue("C_CustomerId").ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FixedIncomeOrderEntry", "loadcontrol('FixedIncomeOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + " ');", true);
            }
        }
        //protected void gvOrderList_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridDataItem)
        //    {
        //        GridDataItem item = (e.Item as GridDataItem);
        //        RadComboBox rcb = new RadComboBox();
        //        TemplateColumn tm = new TemplateColumn();
        //        RadComboBox lbl = new RadComboBox();
        //        if (userType == "bm")
        //        {

        //            rcb = (RadComboBox)e.Item.FindControl("ddlMenu");
        //            if (rcb != null)
        //            {
        //                rcb.Items.FindItemByValue("Edit").Remove();
        //            }
        //        }


        //    }
        //}
        //protected void gvOrderList_ItemCommand(object source, GridCommandEventArgs e)
        //{
        //    if (e.CommandName == "Redirect")
        //    {

        //        GridDataItem item = (GridDataItem)e.Item;
        //        string orderId = item.GetDataKeyValue("CO_OrderId").ToString();
        //        string customerId = item.GetDataKeyValue("C_CustomerId").ToString();
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + " ');", true);
        //    }
        //}
        //protected void gvOrderList_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridDataItem)
        //    {
        //        GridDataItem item = (e.Item as GridDataItem);
        //        RadComboBox rcb = new RadComboBox();
        //        TemplateColumn tm = new TemplateColumn();
        //        RadComboBox lbl = new RadComboBox();
        //        if (userType == "bm")
        //        {

        //            rcb = (RadComboBox)e.Item.FindControl("ddlMenu");
        //            if (rcb != null)
        //            {
        //                rcb.Items.FindItemByValue("Edit").Remove();
        //            }
        //        }


        //    }
        //}
       
        
        
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
            else if (ddlOrderType.SelectedValue == "FI")
            {
                hdnOrderType.Value = "FI";
            }
            

        }

        protected void ddlFIMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RadComboBox ddlAction = (RadComboBox)sender;
                GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;

                string action = "";
                string orderId = gvFIOrderList.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderId"].ToString();
                string customerId = gvFIOrderList.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString();
                string assetGroupCode = gvFIOrderList.MasterTableView.DataKeyValues[selectedRow - 1]["PAG_AssetGroupCode"].ToString();
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + " ');", true);

                // Set the VO into the Session
                //insuranceVo = insuranceBo.GetInsuranceAssetLI(insuranceId, out dtAssociationId);
                //Session["dtAssociationId"] = dtAssociationId;
                //Session["insuranceVo"] = insuranceVo;
                //Session["customerAccountVo"] = customerAccountsBo.GetCustomerInsuranceAccount(insuranceVo.AccountId);

                if (ddlAction.SelectedItem.Value.ToString() == "Edit")
                {
                    action = "Edit";
                    int fiOrderId = int.Parse(orderId);
                    if (assetGroupCode == "FI")
                        GetFIOrderDetails(fiOrderId);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ProductOrderMaster", "loadcontrol('ProductOrderMaster','fiaction=Edit');", true);
                    //if (assetGroupCode == "IN")
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + "&strAction=" + action + " ');", true);
                    //else if (assetGroupCode == "MF")
                    //{
                    //    int mfOrderId = int.Parse(orderId);
                    //    GetMFOrderDetails(mfOrderId);
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderEntry", "loadcontrol('MFOrderEntry','action=Edit');", true);
                    //}
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','action=edit');", true);
                }
                if (ddlAction.SelectedItem.Value.ToString() == "View")
                {
                    action = "View";
                    int fiOrderId = int.Parse(orderId);
                    if (assetGroupCode == "FI")                         
                           GetFIOrderDetails(fiOrderId);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ProductOrderMaster", "loadcontrol('ProductOrderMaster','fiaction=View');", true);

                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + "&strAction=" + action + " ');", true);

                    //else  if (assetGroupCode == "IN")
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + "&strAction=" + action + " ');", true);
                    //else if (assetGroupCode == "MF")
                    //{
                    //    int mfOrderId = int.Parse(orderId);
                    //    GetMFOrderDetails(mfOrderId);
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderEntry", "loadcontrol('MFOrderEntry','action=View');", true);
                    //}
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

        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnConunt = 1;
                RadComboBox ddlAction = (RadComboBox)sender;
                GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;

                string action = "";
                string orderId = gvOrderList.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderId"].ToString();
                string customerId = gvOrderList.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString();
                string assetGroupCode = gvOrderList.MasterTableView.DataKeyValues[selectedRow - 1]["PAG_AssetGroupCode"].ToString();


                if (assetGroupCode == "MF" && ddlAction.SelectedItem.Value!="0")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderEntry", "loadcontrol('MFOrderEntry','action=" + ddlAction.SelectedItem.Value.ToString() + "&orderId=" + orderId + "');", true);
                                   
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


        //protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        btnConunt = 1;
        //        RadComboBox ddlAction = (RadComboBox)sender;
        //        GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
        //        int selectedRow = gvr.ItemIndex + 1;

        //        string action = "";
        //        string orderId = gvOrderList.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderId"].ToString();
        //        string customerId = gvOrderList.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString();
        //        string assetGroupCode = gvOrderList.MasterTableView.DataKeyValues[selectedRow - 1]["PAG_AssetGroupCode"].ToString();
        //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + " ');", true);

        //        // Set the VO into the Session
        //        //insuranceVo = insuranceBo.GetInsuranceAssetLI(insuranceId, out dtAssociationId);
        //        //Session["dtAssociationId"] = dtAssociationId;
        //        //Session["insuranceVo"] = insuranceVo;
        //        //Session["customerAccountVo"] = customerAccountsBo.GetCustomerInsuranceAccount(insuranceVo.AccountId);

        //        if (ddlAction.SelectedItem.Value.ToString() == "Edit")
        //        {
        //            action = "Edit";
        //            if (assetGroupCode == "IN")
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + "&strAction=" + action + " ');", true);
        //            else if (assetGroupCode == "MF")
        //            {
        //                int mfOrderId = int.Parse(orderId);
        //                GetMFOrderDetails(mfOrderId);
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderEntry", "loadcontrol('MFOrderEntry','action=Edit');", true);
        //            }
        //            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','action=edit');", true);
        //        }
        //        if (ddlAction.SelectedItem.Value.ToString() == "View")
        //        {
        //            action = "View";
        //            if (assetGroupCode == "FI")
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ProductOrderMaster", "loadcontrol('ProductOrderMaster','action=View');", true);

        //            else if (assetGroupCode == "IN")
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + "&strAction=" + action + " ');", true);
        //            else if (assetGroupCode == "MF")
        //            {
        //                int mfOrderId = int.Parse(orderId);
        //                GetMFOrderDetails(mfOrderId);
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MFOrderEntry", "loadcontrol('MFOrderEntry','action=View');", true);
        //            }
        //            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','action=view');", true);
        //        }
        //        if (ddlAction.SelectedItem.Value.ToString() == "Delete")
        //        {
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);
        //        }
        //        FillHashtable();
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "ViewInsuranceDetails.ascx:ddlMenu_SelectedIndexChanged()");
        //        object[] objects = new object[1];
        //        //objects[0] = insuranceVo;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, null);/*, objects*/
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}
        private void GetFIOrderDetails(int orderId)
        {
            DataSet dsGetFIOrderDetails = fiorderBo.GetCustomerFIOrderDetails(orderId);
            if (dsGetFIOrderDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsGetFIOrderDetails.Tables[0].Rows)
                {
                   
                   
                    orderVo.OrderId = int.Parse(dr["CO_OrderId"].ToString());
                    orderVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                    orderVo.AgentCode =  dr["AAC_AgentCode"].ToString() ;
                    //fiorderVo.CustomerName = dr["Customer_Name"].ToString();
                    //fiorderVo.RMName = dr["RM_Name"].ToString();
                    //fiorderVo.BMName = dr["AB_BranchName"].ToString();
                    //fiorderVo.PanNo = dr["C_PANNum"].ToString();
                    //if (!string.IsNullOrEmpty(dr["PA_AMCCode"].ToString().Trim()))
                    //    fiorderVo.Amccode = int.Parse(dr["PA_AMCCode"].ToString());
                    //else
                    //    fiorderVo.Amccode = 0;
                    if (!string.IsNullOrEmpty(dr["PAIC_AssetInstrumentCategoryCode"].ToString().Trim()))
                        fiorderVo.AssetInstrumentCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();

                    fiorderVo.CODimage = dr["COD_image"].ToString();

                    fiorderVo.OrderNumber = Convert.ToInt32 (dr["CFIOD_DetailsId"]);

                    if (!string.IsNullOrEmpty(dr["PAIC_AssetInstrumentCategoryCode"].ToString().Trim()))
                        fiorderVo.AssetInstrumentCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["CFIOD_TransactionType"].ToString().Trim()))
                        fiorderVo.TransactionType =  dr["CFIOD_TransactionType"].ToString() ;

                    if (!string.IsNullOrEmpty(dr["CFIOD_ModeOfHolding"].ToString().Trim()))
                        fiorderVo.ModeOfHolding = dr["CFIOD_ModeOfHolding"].ToString();

                    if (!string.IsNullOrEmpty(dr["CFIOD_SchemeOption"].ToString().Trim()))
                        fiorderVo.Schemeoption = dr["CFIOD_SchemeOption"].ToString();

                    if (!string.IsNullOrEmpty(dr["CFIOD_DepositPayableTo"].ToString().Trim()))
                        fiorderVo.Depositpayableto = dr["CFIOD_DepositPayableTo"].ToString();

                    if (!string.IsNullOrEmpty(dr["CFIOD_Frequency"].ToString().Trim()))
                        fiorderVo.Frequency = dr["CFIOD_Frequency"].ToString();

                    if (!string.IsNullOrEmpty(dr["CFIOD_Privilidge"].ToString().Trim()))
                        fiorderVo.Privilidge = dr["CFIOD_Privilidge"].ToString();

                    if (!string.IsNullOrEmpty(dr["CFIOD_ExisitingDepositreceiptno"].ToString().Trim()))
                        fiorderVo.ExisitingDepositreceiptno = dr["CFIOD_ExisitingDepositreceiptno"].ToString();

                    if (!string.IsNullOrEmpty(dr["CFIOD_RenewalAmount"].ToString().Trim()))
                        fiorderVo.RenewalAmount =Convert.ToDouble ( dr["CFIOD_RenewalAmount"]) ;

                    if (!string.IsNullOrEmpty(dr["CFIOD_MaturityDate"].ToString().Trim()))
                        fiorderVo.MaturityDate = Convert.ToDateTime (dr["CFIOD_MaturityDate"]);//.ToString();

                    fiorderVo.OrderNumber = int.Parse(dr["CFIOD_OrderNO"].ToString());


                    if (!string.IsNullOrEmpty(dr["CFIOD_AmountPayable"].ToString().Trim()))
                        fiorderVo.AmountPayable = double.Parse(dr["CFIOD_AmountPayable"].ToString());
                    else
                        fiorderVo.AmountPayable = 0;

                    if (!string.IsNullOrEmpty(dr["CFIOD_MaturityAmount"].ToString().Trim()))
                        fiorderVo.MaturityAmount = double.Parse(dr["CFIOD_MaturityAmount"].ToString());
                    else
                        fiorderVo.MaturityAmount = 0;

                    //if (int.Parse(dr["CMFA_accountid"].ToString()) != 0)
                    //    fiorderVo.accountid = int.Parse(dr["CMFA_accountid"].ToString());
                    //else
                    //    fiorderVo.accountid = 0;
                   // fiorderVo.TransactionCode = dr["WMTT_TransactionClassificationCode"].ToString();
                    orderVo.OrderDate = DateTime.Parse(dr["CO_OrderDate"].ToString());
                  //  fiorderVo.IsImmediate = int.Parse(dr["CMFOD_IsImmediate"].ToString());
                    orderVo.ApplicationNumber = dr["CO_ApplicationNumber"].ToString();
                    if (!string.IsNullOrEmpty(dr["CO_ApplicationReceivedDate"].ToString()))
                    {
                        orderVo.ApplicationReceivedDate = DateTime.Parse(dr["CO_ApplicationReceivedDate"].ToString());
                    }
                    else
                        orderVo.ApplicationReceivedDate = DateTime.MinValue;
                //    fiorderVo.portfolioId = int.Parse(dr["CP_portfolioId"].ToString());
                    orderVo.PaymentMode = dr["XPM_PaymentModeCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["CO_ChequeNumber"].ToString()))
                        orderVo.ChequeNumber = dr["CO_ChequeNumber"].ToString();
                    else
                        orderVo.ChequeNumber = "";
                    if (!string.IsNullOrEmpty(dr["CO_PaymentDate"].ToString()))
                        orderVo.PaymentDate = DateTime.Parse(dr["CO_PaymentDate"].ToString());
                    else
                        orderVo.PaymentDate = DateTime.MinValue;

                    if (!string.IsNullOrEmpty(dr["AAC_AdviserAgentId"].ToString()))
                    {
                        orderVo.AgentId = Convert.ToInt32(dr["AAC_AdviserAgentId"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dr["AAC_AgentCode"].ToString()))
                    {
                        fiorderVo.AgentCode = dr["AAC_AgentCode"].ToString();
                    }

                    //if (!string.IsNullOrEmpty(dr["CMFOD_FutureTriggerCondition"].ToString()))
                    //    fiorderVo.FutureTriggerCondition = dr["CMFOD_FutureTriggerCondition"].ToString();
                    //else
                    //    fiorderVo.FutureTriggerCondition = "";
                    //if (!string.IsNullOrEmpty(dr["CMFOD_FutureExecutionDate"].ToString()))
                    //    fiorderVo.FutureExecutionDate = DateTime.Parse(dr["CMFOD_FutureExecutionDate"].ToString());
                    //else
                    //    fiorderVo.FutureExecutionDate = DateTime.MinValue;
                    //if (!string.IsNullOrEmpty(dr["PASP_SchemePlanSwitch"].ToString()))
                    //    fiorderVo.SchemePlanSwitch = int.Parse(dr["PASP_SchemePlanSwitch"].ToString());
                    //else
                    //    fiorderVo.SchemePlanSwitch = 0;
                    if (!string.IsNullOrEmpty(dr["CB_CustBankAccId"].ToString()))
                        orderVo.CustBankAccId = int.Parse(dr["CB_CustBankAccId"].ToString());
                    else
                        orderVo.CustBankAccId = 0;


                    if (!string.IsNullOrEmpty(dr["CFIOD_DepCustBankAccId"].ToString()))
                        fiorderVo.DepCustBankAccId = int.Parse(dr["CFIOD_DepCustBankAccId"].ToString());
                    else
                           fiorderVo.DepCustBankAccId = 0;

                 
                    //if (!string.IsNullOrEmpty(dr["CMFOD_BankName"].ToString()))
                    //    fiorderVo.BankName = dr["CMFOD_BankName"].ToString();
                    //else
                    //    fiorderVo.BankName = "";
                    //if (!string.IsNullOrEmpty(dr["CFIOD_BranchName"].ToString()))
                    //    fiorderVo.BankBranchName = dr["CFIOD_BranchName"].ToString();
                    //else
                    //    fiorderVo.BankBranchName = "";
                    //
                    //if (!string.IsNullOrEmpty(dr["CFIOD_DepCustBankAccId"].ToString()))
                    //    orderVo.d= int.Parse(dr["CFIOD_DepCustBankAccId"].ToString());
                    //else
                    //    orderVo.CustBankAccId = 0;

                    if (!string.IsNullOrEmpty(dr["PFISD_SeriesId"].ToString()))
                        fiorderVo.SeriesId = Convert.ToInt32 (dr["PFISD_SeriesId"].ToString());
                    else
                        fiorderVo.SeriesId = 0;

                    if (!string.IsNullOrEmpty(dr["PFISM_SchemeId"].ToString()))
                        fiorderVo.SchemeId = Convert.ToInt32(dr["PFISM_SchemeId"].ToString());
                    else
                        fiorderVo.SchemeId = 0;

                    if (!string.IsNullOrEmpty(dr["PFIIM_IssuerId"].ToString()))
                        fiorderVo.IssuerId = dr["PFIIM_IssuerId"].ToString() ;
                    else
                        fiorderVo.IssuerId ="0";
                     
                    //if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine2"].ToString()))
                    //    fiorderVo.AddrLine2 = dr["CMFOD_AddrLine2"].ToString();
                    //else
                    //    fiorderVo.AddrLine2 = "";
                    //if (!string.IsNullOrEmpty(dr["CMFOD_AddrLine3"].ToString()))
                    //    fiorderVo.AddrLine3 = dr["CMFOD_AddrLine3"].ToString();
                    //else
                    //    fiorderVo.AddrLine3 = "";
                    //if (!string.IsNullOrEmpty(dr["CMFOD_City"].ToString()))
                    //    fiorderVo.City = dr["CMFOD_City"].ToString();
                    //else
                    //    fiorderVo.City = "";
                    //if (!string.IsNullOrEmpty(dr["CMFOD_State"].ToString()))
                    //    fiorderVo.State = dr["CMFOD_State"].ToString();
                    //else
                    //    fiorderVo.State = "";
                    //if (!string.IsNullOrEmpty(dr["CMFOD_Country"].ToString()))
                    //    fiorderVo.Country = dr["CMFOD_Country"].ToString();
                    //else
                    //    fiorderVo.Country = "";
                    //if (!string.IsNullOrEmpty(dr["CMFOD_PinCode"].ToString()))
                    //    fiorderVo.Pincode = dr["CMFOD_PinCode"].ToString();
                    //else
                    //    fiorderVo.Pincode = "";
                    //if (!string.IsNullOrEmpty(dr["CMFOD_LivingScince"].ToString()))
                    //    fiorderVo.LivingSince = DateTime.Parse(dr["CMFOD_LivingScince"].ToString());
                    //else
                    //    fiorderVo.LivingSince = DateTime.MinValue;

                    //if (!string.IsNullOrEmpty(dr["XF_FrequencyCode"].ToString()))
                    //    fiorderVo.FrequencyCode = dr["XF_FrequencyCode"].ToString();
                    //else
                    //    fiorderVo.FrequencyCode = "";
                    //if (!string.IsNullOrEmpty(dr["CMFOD_StartDate"].ToString()))
                    //    fiorderVo.StartDate = DateTime.Parse(dr["CMFOD_StartDate"].ToString());
                    //else
                    //    fiorderVo.StartDate = DateTime.MinValue;
                    //if (!string.IsNullOrEmpty(dr["CMFOD_EndDate"].ToString()))
                    //    fiorderVo.EndDate = DateTime.Parse(dr["CMFOD_EndDate"].ToString());
                    //else
                    //    fiorderVo.EndDate = DateTime.MinValue;

                    //if (!string.IsNullOrEmpty(dr["CMFOD_Units"].ToString()))
                    //    fiorderVo.Units = double.Parse(dr["CMFOD_Units"].ToString());
                    //else
                    //    fiorderVo.Units = 0;

                    //if (!string.IsNullOrEmpty(dr["CMFOD_ARNNo"].ToString()))
                    //{
                    //    fiorderVo.ARNNo = Convert.ToString(dr["CMFOD_ARNNo"]);
                    //}

                }
                Session["orderVo"] = orderVo;
                Session["fiorderVo"] = fiorderVo;
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
                    mforderVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
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
                    mforderVo.FolioNumber = dr["CMFA_FolioNum"].ToString();
                    mforderVo.TransactionCode = dr["WMTT_TransactionClassificationCode"].ToString();
                    orderVo.OrderDate = DateTime.Parse(dr["CO_OrderDate"].ToString());
                    mforderVo.IsImmediate = int.Parse(dr["CMFOD_IsImmediate"].ToString());
                    orderVo.ApplicationNumber = dr["CO_ApplicationNumber"].ToString();
                    if (!string.IsNullOrEmpty(dr["CO_ApplicationReceivedDate"].ToString()))
                    {
                        orderVo.ApplicationReceivedDate = DateTime.Parse(dr["CO_ApplicationReceivedDate"].ToString());
                    }
                    else 
                    orderVo.ApplicationReceivedDate = DateTime.MinValue;
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

                    if (!string.IsNullOrEmpty(dr["AAC_AdviserAgentId"].ToString()))
                    {
                        orderVo.AgentId = Convert.ToInt32(dr["AAC_AdviserAgentId"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dr["AAC_AgentCode"].ToString()))
                    {
                        orderVo.AgentCode = dr["AAC_AgentCode"].ToString();
                    }

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

                    if (!string.IsNullOrEmpty(dr["CMFOD_ARNNo"].ToString()))
                    {
                        mforderVo.ARNNo = Convert.ToString(dr["CMFOD_ARNNo"]);
                    }

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
            //    txtIndividualCustomer.Text = string.Empty;
            //    txtIndividualCustomer.Enabled = true;
            //    hdnIndividualOrGroup.Value = ddlCustomerType.SelectedItem.Value;
            //    rquiredFieldValidatorIndivudialCustomer.Visible = true;
            //    if (ddlCustomerType.SelectedItem.Value == "0")
            //    {

            //        customerType = "GROUP";
            //        if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
            //        {
            //            txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
            //            txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
            //        }
            //        else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
            //        {
            //            if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
            //            {
            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";
            //            }
            //            else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
            //            {
            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllGroupCustomers";
            //            }
            //            else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
            //            {
            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
            //            }
            //            else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
            //            {
            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMGroupCustomers";
            //            }
            //        }

            //        else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
            //        {
            //            if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
            //            {
            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetBMParentCustomerNames";
            //            }
            //            if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
            //            {
            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllGroupCustomers";
            //            }
            //            if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
            //            {
            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
            //            }
            //            if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
            //            {
            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMGroupCustomers";
            //            }
            //        }
            //        lblselectCustomer.Visible = true;
            //        txtIndividualCustomer.Visible = true;
            //    }
            //    else if (ddlCustomerType.SelectedItem.Value == "1")
            //    {
            //        customerType = "IND";

            //        //rquiredFieldValidatorIndivudialCustomer.Visible = true;
            //        if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
            //        {
            //            txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
            //            txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";

            //        }
            //        else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
            //        {
            //            if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
            //            {
            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
            //            }
            //            else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
            //            {

            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllIndividualCustomers";
            //            }
            //            else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
            //            {
            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
            //            }
            //            else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
            //            {
            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMIndividualCustomers";
            //            }
            //        }
            //        else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
            //        {
            //            if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
            //            {
            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
            //            }
            //            else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
            //            {
            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllIndividualCustomers";
            //            }
            //            else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
            //            {
            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
            //            }
            //            else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
            //            {
            //                txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
            //                txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMIndividualCustomers";
            //            }
            //        }
            //        lblselectCustomer.Visible = true;
            //        txtIndividualCustomer.Visible = true;
            //    }
            //    else
            //    {
            //        txtIndividualCustomer.Enabled = false;
            //        hdnCustomerId.Value = null;
            //        lblselectCustomer.Visible = false;
            //        txtIndividualCustomer.Visible = false;
            //    }
            //}
        }
    }
}
