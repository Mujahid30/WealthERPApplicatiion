using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WealthERP.Base;
using BoCustomerProfiling;
using VoUser;
using System.Data;
using BoFPSuperlite;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoAdvisorProfiling;
using BoUploads;

namespace WealthERP.FP
{
    public partial class ProspectList : System.Web.UI.UserControl
    {
        CustomerBo customerbo = new CustomerBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        UserVo userVo = new UserVo();
        DataSet dsGetAllProspectCustomersForRM = new DataSet();
        int rmId = 0;
        CustomerProspectBo customerProspectBo = new CustomerProspectBo();
        double sum = 0;
        string clientID;
        int bmID = 0;
        int advisorId = 0;
        string userType = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            rmVo = (RMVo)Session[SessionContents.RmVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["userVo"];
            advisorId = advisorVo.advisorId;
            int RMId = rmVo.RMId;
            bmID = rmVo.RMId;
            rmId = rmVo.RMId;

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            if (!IsPostBack)
            {

                if (userType == "advisor")
                {
                    btnGo.Visible = true;
                    tblAdviserBM.Visible = true;
                    BindBranchDropDown();
                    BindRMDropDown();
                    gvCustomerProspectlist.Visible = false;
                }
                else if (userType == "rm")
                {
                    btnGo.Visible = false;
                    tblAdviserBM.Visible = false;
                    gvCustomerProspectlist.Visible = true;
                    BindCustomerProspectGrid();
                }
                if (userType == "bm")
                {
                    btnGo.Visible = true;
                    tblAdviserBM.Visible = true;
                    BindBranchForBMDropDown();
                    BindRMforBranchDropdown(0, bmID);
                    gvCustomerProspectlist.Visible = false;
                }
            }


            //SqlDataSource1.SelectParameters["AR_RMId"].DefaultValue = rmVo.RMId.ToString();
        }

        private void BindCustomerProspectGrid()
        {
            SetParameters();
            DataTable dtcustomerProspect = new DataTable();
            dsGetAllProspectCustomersForRM = customerProspectBo.GetAllProspectCustomersForRM(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchheadId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnAll.Value));
            if ((dsGetAllProspectCustomersForRM.Tables[0].Rows.Count > 0) && (!string.IsNullOrEmpty(dsGetAllProspectCustomersForRM.ToString())))
            {
                gvCustomerProspectlist.Visible = true;

                dtcustomerProspect.Columns.Add("C_CustomerId");
                dtcustomerProspect.Columns.Add("Name");
                dtcustomerProspect.Columns.Add("IsProspect");
                dtcustomerProspect.Columns.Add("C_Email");
                dtcustomerProspect.Columns.Add("C_Mobile1");
                dtcustomerProspect.Columns.Add("Address");
                dtcustomerProspect.Columns.Add("Asset", typeof(double));
                dtcustomerProspect.Columns.Add("Liabilities", typeof(double));
                dtcustomerProspect.Columns.Add("Networth", typeof(double));
                DataRow drCustomerProspect;

                for (int i = 0; i < dsGetAllProspectCustomersForRM.Tables[0].Rows.Count; i++)
                {

                    drCustomerProspect = dtcustomerProspect.NewRow();
                    drCustomerProspect[0] = dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["C_CustomerId"].ToString();
                    drCustomerProspect[1] = dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Name"].ToString();

                    if (dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["IsProspect"].ToString() != "")
                        drCustomerProspect[2] = dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["IsProspect"].ToString();

                    drCustomerProspect[3] = dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["C_Email"].ToString();
                    drCustomerProspect[4] = dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["C_Mobile1"].ToString();
                    if (dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Address"].ToString() != ",,,,,,")
                        drCustomerProspect[5] = dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Address"].ToString();
                    else
                        drCustomerProspect[5] = " ";

                    if (dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Asset"].ToString() != "")
                        drCustomerProspect[6] = dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Asset"].ToString();
                   
                    if (dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Liabilities"].ToString() != "")
                        drCustomerProspect[7] = dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Liabilities"].ToString();
                    
                    if (dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Networth"].ToString() != "")
                        drCustomerProspect[8] = dsGetAllProspectCustomersForRM.Tables[0].Rows[i]["Networth"].ToString();
                  
                    dtcustomerProspect.Rows.Add(drCustomerProspect);
                }
                gvCustomerProspectlist.DataSource = dtcustomerProspect;
                gvCustomerProspectlist.DataBind();
                lblErrorMsg.Visible = false;
                if (Cache["NetworthMIS" + userVo.UserId] == null)
                {
                    Cache.Insert("NetworthMIS" + userVo.UserId, dtcustomerProspect);
                }
                else
                {
                    Cache.Remove("NetworthMIS" + userVo.UserId);
                    Cache.Insert("NetworthMIS" + userVo.UserId, dtcustomerProspect);
                }
                btnNetworthMIS.Visible = true;
            }
            else
            {
                gvCustomerProspectlist.Visible = false;
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = "No records found";
                btnNetworthMIS.Visible = false;
            }
        }

        private void SetParameters()
        {
            if ((userType == "advisor"))
            {
                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnAll.Value = "0";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRM.SelectedValue; ;
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "3";
                }

            }
            else if (userType == "rm")
            {
                hdnrmId.Value = rmVo.RMId.ToString();
                hdnAll.Value = "0";

            }
            else if (userType == "bm")
            {
                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {

                    hdnbranchheadId.Value = bmID.ToString();
                    hdnAll.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnbranchheadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnbranchheadId.Value = bmID.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRM.SelectedValue; ;
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnbranchheadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "3";
                }
            }

            if (hdnbranchheadId.Value == "")
                hdnbranchheadId.Value = "0";

            if (hdnbranchId.Value == "")
                hdnbranchId.Value = "0";

            if (hdnadviserId.Value == "")
                hdnadviserId.Value = "0";

            if (hdnrmId.Value == "")
                hdnrmId.Value = "0";
        }

        protected void lnkbtnGvProspectListName_Click(object sender, EventArgs e)
        {
            int customerId = 0;
            int selectedRow = 0;
            GridDataItem gdi;
            CustomerVo customervo = new CustomerVo();
            CustomerBo customerBo = new CustomerBo();


            //LinkButton lnkbtn = (LinkButton)gvCustomerProspectlist.FindControl("lnkbtnGvProspectListName");
            LinkButton lnkbtn = (LinkButton)sender;
            gdi = (GridDataItem)lnkbtn.NamingContainer;
            selectedRow = gdi.ItemIndex + 1;
            customerId = int.Parse((gvCustomerProspectlist.MasterTableView.DataKeyValues[selectedRow - 1]["C_CustomerId"].ToString()));
            customervo = customerBo.GetCustomer(customerId);
            Session["CustomerVo"] = customervo;

            Session["IsDashboard"] = "FP";
            if (customerId != 0)
            {
                Session[SessionContents.FPS_ProspectList_CustomerId] = customerId;
            }
            Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
            Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "View";
            Session[SessionContents.FPS_AddProspectListActionStatus] = "FPDashBoard";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerIndLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerFPDashBoard','login');", true);
        }

        private void BindBranchDropDown()
        {

            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
            try
            {
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
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProscpectList.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindRMDropDown()
        {
            try
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
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProscpectList.ascx:BindRMDropDown()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
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

                FunctionInfo.Add("Method", "ProscpectList.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
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

            try
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
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProscpectList.ascx:BindRMforBranchDropdown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            BindCustomerProspectGrid();
        }

        protected void gvCustomerProspectlist_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtcustomerProspect = new DataTable();
            dtcustomerProspect = (DataTable)Cache["NetworthMIS" + userVo.UserId];
            gvCustomerProspectlist.DataSource = dtcustomerProspect;
        }

        protected void btnNetworthMIS_Click(object sender, ImageClickEventArgs e)
        {
            gvCustomerProspectlist.ExportSettings.OpenInNewWindow = true;
            gvCustomerProspectlist.ExportSettings.IgnorePaging = true;
            gvCustomerProspectlist.ExportSettings.HideStructureColumns = true;
            gvCustomerProspectlist.ExportSettings.ExportOnlyData = true;
            gvCustomerProspectlist.ExportSettings.FileName = "Customer Networth MIS Details";
            gvCustomerProspectlist.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvCustomerProspectlist.MasterTableView.ExportToExcel();
        }



        //protected void gvCustomerProspectlist_ItemDataBound(object sender, GridItemEventArgs e)
        //{


        //    if (e.Item is GridDataItem)
        //    {
        //        GridDataItem dataItem = (GridDataItem)e.Item;
        //        Label lblAsset = (Label)(dataItem["Asset"].FindControl("lblAsset"));
        //        if (lblAsset.Text != "")
        //            sum += double.Parse(lblAsset.Text);
        //        else
        //            sum += 0;
        //    }
        //    else if (e.Item is GridFooterItem)
        //    {
        //        GridFooterItem footer = (GridFooterItem)e.Item;
        //        Label lblFooter = (Label)(footer["Asset"].FindControl("lblTotalAssets"));
        //        lblFooter.Text = sum.ToString();
        //        clientID = (lblFooter).ClientID;
        //    }
        //}

        //protected void gvCustomerProspectlist_PreRender(object sender, EventArgs e)
        //{
        //    foreach (GridDataItem dataItem in gvCustomerProspectlist.MasterTableView.Items)
        //    {
        //        (dataItem["Asset"].FindControl("lblAsset") as Label).Attributes.Add("onblur", "update('" + clientID + "'" + "," + "'" + (dataItem["Asset"].FindControl("lblAsset") as Label).ClientID + "')");
        //        (dataItem["Asset"].FindControl("lblAsset") as Label).Attributes.Add("onfocus", "getInitialValue('" + (dataItem["Asset"].FindControl("lblAsset") as Label).ClientID + "')");
        //    }
        //}  

        //protected void ddlProspectList_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    int customerId = 0;
        //    int selectedRow = 0;
        //    GridDataItem gdi;
        //    CustomerVo customervo = new CustomerVo();
        //    CustomerBo customerBo = new CustomerBo();
        //    try
        //    {
        //        RadComboBox rcb = (RadComboBox)sender;
        //        gdi = (GridDataItem)rcb.NamingContainer;
        //        selectedRow = gdi.ItemIndex;
        //        customerId = int.Parse((gvCustomerProspectlist.MasterTableView.DataKeyValues[selectedRow]["C_CustomerId"].ToString()));
        //        customervo = customerBo.GetCustomer(customerId);

        //        Session[SessionContents.CustomerVo] = customervo;


        //        if (customerId != 0)
        //        {
        //            Session[SessionContents.FPS_ProspectList_CustomerId] = customerId;
        //        }                
        //        if (e.Value == "ViewProfile")
        //        {
        //            Session[SessionContents.FPS_AddProspectListActionStatus] = "View";
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AddProspectList','login');", true);
        //        }
        //        if (e.Value == "FinancialPlanning")
        //        {
        //            Session[SessionContents.FPS_TreeView_Status] = "FinanceProfile";
        //            Session[SessionContents.FPS_CustomerPospect_ActionStatus] = "View";
        //            Session[SessionContents.FPS_AddProspectListActionStatus] = "FPDashBoard";
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerIndLeftPane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerFPDashBoard','login');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;

        //    }
        //}


    }
}
