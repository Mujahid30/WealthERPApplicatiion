using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using VoUser;
using BoUser;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Drawing.Printing;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Text;
using BoCommon;
using WealthERP.Base;
using BoUploads;
using Telerik.Web.UI;


namespace WealthERP.Advisor
{
    public partial class ViewRM : System.Web.UI.UserControl
    {
        DataTable dt;
        public DropDownList ddl=new DropDownList();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo;
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        UserBo userBo = new UserBo();
        UserVo userVo = new UserVo();
        int rmId;
        int bmID;
        int userId;
        string userType;
        int adviserID;
        int bmIdOrHeadID;
        int branchId;
        int all;

        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        List<int> rmList = new List<int>();
        string currentRole = "";

        #region removed the pager

        //protected override void OnInit(EventArgs e)
        //{
        //    try
        //    {
        //        ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
        //        mypager.EnableViewState = true;
        //        base.OnInit(e);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "ViewRM.ascx.cs:OnInit()");
        //        object[] objects = new object[0];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

       
        //public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        GetPageCount();
        //        this.BindGrid();
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "ViewRM.ascx.cs:HandlePagerEvent()");
        //        object[] objects = new object[0];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

       
        
        //private void GetPageCount()
        //{
        //    string upperlimit = "";
        //    int rowCount = 0;
        //    int ratio = 0;
        //    string lowerlimit = "";
        //    string PageRecords = "";
        //    try
        //    {
        //        if (hdnCount.Value != "")
        //            rowCount = Convert.ToInt32(hdnCount.Value);
        //        if (rowCount > 0)
        //        {
        //            ratio = rowCount / 15;
        //            mypager.PageCount = rowCount % 15 == 0 ? ratio : ratio + 1;
        //            mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
        //            lowerlimit = (((mypager.CurrentPage - 1) * 15) + 1).ToString();
        //            upperlimit = (mypager.CurrentPage * 15).ToString();
        //            if (mypager.CurrentPage == mypager.PageCount)
        //                upperlimit = hdnCount.Value;
        //            PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
        //            lblCurrentPage.Text = PageRecords;
        //            hdnCurrentPage.Value = mypager.CurrentPage.ToString();
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "ViewRM.ascx.cs:GetPageCount()");
        //        object[] objects = new object[5];
        //        objects[0] = upperlimit;
        //        objects[1] = rowCount;
        //        objects[2] = ratio;
        //        objects[3] = lowerlimit;
        //        objects[4] = PageRecords;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        #endregion

        protected void BindGrid()
        {
            try
            {

                advisorVo = (AdvisorVo)Session["advisorVo"];
                userVo = (UserVo)Session["UserVo"];
                if (hdnCurrentPage.Value.ToString() != "")
                {
                    //mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                    hdnCurrentPage.Value = "";
                }
                if (Session["RM"] != null)
                {

                    ShowRM();
                    Session.Remove("RM");

                }
                else
                {
                    showRMList();
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
                FunctionInfo.Add("Method", "ViewRMD.ascx.cs:BindGrid()");
                object[] objects = new object[2];
                objects[0] = advisorVo;
                objects[1] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string rm = "";
            try
            {
                if (!IsPostBack)
                {

                    //mypager.CurrentPage = 1;
                    this.BindGrid();
                }
                //if (Session["RM"] != null)
                //{
                //    ShowRM();
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewRM.ascx.cs:Page_Load()");
                object[] objects = new object[1];
                objects[0] = rmVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void ShowRM()
        {
            string rm = "";
            DataTable dtAdvisorStaff = new DataTable();
            DataRow drAdvisorStaff;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            int count = 0;

            try
            {
                rm = Session["RM"].ToString();
                if (rm.ToLower().Trim() == "find rm" || rm.ToLower().Trim() == "")
                    rm = "";

                ds = advisorStaffBo.FindRM(rm, advisorVo.advisorId, hdnSort.Value.ToString(), out count);

                //lblTotalRows.Text = hdnCount.Value = count.ToString();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    trBMBranchs.Visible = false;
                    trMessage.Visible = false;

                    dt = ds.Tables[0];
                    trMessage.Visible = false;
                    dtAdvisorStaff.Columns.Add("UserId");
                    dtAdvisorStaff.Columns.Add("WealthERP Id");
                    dtAdvisorStaff.Columns.Add("RMName");
                    dtAdvisorStaff.Columns.Add("StaffCode");
                    //dtAdvisorStaff.Columns.Add("RM Main Branch");
                    dtAdvisorStaff.Columns.Add("StaffType");
                    dtAdvisorStaff.Columns.Add("StaffRole");
                    dtAdvisorStaff.Columns.Add("Email");
                    dtAdvisorStaff.Columns.Add("Mobile Number");
                    dtAdvisorStaff.Columns.Add("BranchList");
                    
                    //dtAdvisorStaff.Columns.Add("Branch Name");
                    DataRow dr;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        drAdvisorStaff = dtAdvisorStaff.NewRow();
                        dr = dt.Rows[i];
                        rmVo = new RMVo();

                        drAdvisorStaff[0] = dr["u_userId"].ToString();
                        drAdvisorStaff[1] = dr["ar_rmid"].ToString();
                        drAdvisorStaff[2] = dr["ar_firstname"].ToString() + " " + dr["ar_middlename"].ToString() + " " + dr["ar_lastname"].ToString();
                        if (dr["AR_IsExternalStaff"].ToString() == "1")
                            drAdvisorStaff[3] = "External";
                        else
                            drAdvisorStaff[3] = "Internal";
                        drAdvisorStaff[4] = dr["AR_JobFunction"].ToString();
                        drAdvisorStaff[5] = dr["AR_Email"].ToString();
                        drAdvisorStaff[6] = dr["AR_Mobile"].ToString();
                        drAdvisorStaff[7] = dr["BranchList"].ToString();
                        dtAdvisorStaff.Rows.Add(drAdvisorStaff);
                    }

                    gvRMList.DataSource = dtAdvisorStaff;
                    gvRMList.DataBind();
                    //this.GetPageCount();
                }
                else
                {
                    trBMBranchs.Visible = false;
                    gvRMList.DataSource = null;
                    gvRMList.DataBind();
                    //DivPager.Visible = false;
                    //lblCurrentPage.Visible = false;
                    //lblTotalRows.Visible = false;
                    trMessage.Visible = true;
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
                FunctionInfo.Add("Method", "ViewRM.ascx.cs:ShowRM()");
                object[] objects = new object[3];
                objects[0] = advisorVo;
                objects[1] = rm;
                objects[2] = rmList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void showRMList()
        {
            rmVo = new RMVo();
            int Count = 0;
            List<RMVo> rmList = new List<RMVo>();
            List<string> roleList = new List<string>();
            //roleList = userBo.GetUserRoles(userVo.UserId);
            string role = "BM";
            rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
            try
            {
                //if (roleList.Contains("BM") && Session["CurrentUserRole"] == "BM")
                if (role == Session[SessionContents.CurrentUserRole].ToString())
                {
                    trBMBranchs.Visible = true;
                    if (!Page.IsPostBack)
                    {
                        branchId = 0;
                        all = 1;
                        bmIdOrHeadID = rmVo.RMId;
                        BindBMDropDown(bmIdOrHeadID);
                    }
                    rmList = advisorStaffBo.GetBMRMList(branchId, bmIdOrHeadID, all, out Count);
                    if (rmList!=null && rmList.Count != 0)
                    {
                        //lblTotalRows.Text = hdnCount.Value = Count.ToString();
                        DataTable dtAdvisorStaff = new DataTable();
                        dtAdvisorStaff.Columns.Add("UserId");
                        dtAdvisorStaff.Columns.Add("WealthERP Id");
                        dtAdvisorStaff.Columns.Add("RMName");
                        dtAdvisorStaff.Columns.Add("StaffCode");
                        dtAdvisorStaff.Columns.Add("StaffType");
                        dtAdvisorStaff.Columns.Add("StaffRole");
                        dtAdvisorStaff.Columns.Add("Email");
                        dtAdvisorStaff.Columns.Add("BranchList");
                        dtAdvisorStaff.Columns.Add("Mobile Number");

                        DataRow drAdvisorStaff;
                        for (int i = 0; i < rmList.Count; i++)
                        {
                            drAdvisorStaff = dtAdvisorStaff.NewRow();
                            rmVo = new RMVo();
                            rmVo = rmList[i];
                            drAdvisorStaff[0] = rmVo.UserId.ToString();
                            drAdvisorStaff[1] = rmVo.RMId.ToString();
                            drAdvisorStaff[2] = rmVo.FirstName.ToString() + " " + rmVo.MiddleName.ToString() + " " + rmVo.LastName.ToString();
                            if (rmVo.IsExternal == 1)
                                drAdvisorStaff[4] = "External";
                            else
                                drAdvisorStaff[4] = "Internal";
                            if (!string.IsNullOrEmpty(rmVo.StaffCode))
                                drAdvisorStaff[3] = rmVo.StaffCode;
                            else
                                drAdvisorStaff[3] = "";
                            drAdvisorStaff[5] = string.Empty;
                            drAdvisorStaff[6] = rmVo.Email.ToString();
                            drAdvisorStaff[8] = rmVo.Mobile.ToString();
                            drAdvisorStaff[7] = string.Empty;
                            dtAdvisorStaff.Rows.Add(drAdvisorStaff);
                        }
                        gvRMList.DataSource = dtAdvisorStaff;
                        gvRMList.Columns[5].Visible = false;
                        gvRMList.Columns[4].Visible = false;
                        gvRMList.DataBind();

                        //this.GetPageCount();
                    }
                    else
                    {
                        gvRMList.DataSource = null;
                        gvRMList.DataBind();
                        //DivPager.Visible = false;
                        //lblCurrentPage.Visible = false;
                        //lblTotalRows.Visible = false;
                        trMessage.Visible = true;
                    }
                }
                else
                {
                    trBMBranchs.Visible = false;
                    trMessage.Visible = false;
                    advisorStaffBo = new AdvisorStaffBo();
                    List<RMVo> advisorStaffList = null;
                    //advisorStaffList = advisorStaffBo.GetRMList(advisorVo.advisorId, mypager.CurrentPage, hdnSort.Value, out Count, string.Empty);
                    advisorStaffList = advisorStaffBo.GetRMList(advisorVo.advisorId, hdnSort.Value, string.Empty);

                    if (advisorStaffList != null)
                    {
                        //lblTotalRows.Text = hdnCount.Value = Count.ToString();
                        DataTable dtAdvisorStaff = new DataTable();

                        dtAdvisorStaff.Columns.Add("UserId");
                        dtAdvisorStaff.Columns.Add("WealthERP Id");
                        dtAdvisorStaff.Columns.Add("RMName");
                        dtAdvisorStaff.Columns.Add("StaffCode");
                        dtAdvisorStaff.Columns.Add("StaffType");
                        dtAdvisorStaff.Columns.Add("StaffRole");
                        dtAdvisorStaff.Columns.Add("Email");
                        dtAdvisorStaff.Columns.Add("Mobile Number");
                        dtAdvisorStaff.Columns.Add("BranchList");
                        DataRow drAdvisorStaff;

                        for (int i = 0; i < advisorStaffList.Count; i++)
                        {
                            drAdvisorStaff = dtAdvisorStaff.NewRow();
                            rmVo = new RMVo();
                            rmVo = advisorStaffList[i];
                            drAdvisorStaff[0] = rmVo.UserId.ToString();
                            drAdvisorStaff[1] = rmVo.RMId.ToString();
                            drAdvisorStaff[2] = rmVo.FirstName.ToString() + " " + rmVo.MiddleName.ToString() + " " + rmVo.LastName.ToString();
                            //drAdvisorStaff[3] = rmVo.MainBranch.ToString();
                            if (!string.IsNullOrEmpty(rmVo.StaffCode))
                                drAdvisorStaff[3] = rmVo.StaffCode;
                            else
                                drAdvisorStaff[3] = "";
                            if (rmVo.IsExternal == 1)
                                drAdvisorStaff[4] = "External";
                            else
                                drAdvisorStaff[4] = "Internal";
                            drAdvisorStaff[5] = rmVo.RMRole.ToString();
                            drAdvisorStaff[6] = rmVo.Email.ToString();
                            drAdvisorStaff[7] = rmVo.Mobile.ToString();
                            drAdvisorStaff[8] = rmVo.BranchList.ToString();
                            dtAdvisorStaff.Rows.Add(drAdvisorStaff);
                        }
                        gvRMList.DataSource = dtAdvisorStaff;
                        gvRMList.DataBind();



                        if (Cache["RMList"] == null)
                        {
                            Cache.Insert("RMList", dtAdvisorStaff);
                        }
                        else
                        {
                            Cache.Remove("RMList");
                            Cache.Insert("RMList", dtAdvisorStaff);
                        }

                        
                        //this.GetPageCount();
                    }
                    else
                    {
                        gvRMList.DataSource = null;
                        gvRMList.DataBind();
                        //DivPager.Visible = false;
                        //lblCurrentPage.Visible = false;
                        //lblTotalRows.Visible = false;
                        trMessage.Visible = true;
                    }
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

                FunctionInfo.Add("Method", "ViewRM.ascx:showRMList()");


                object[] objects = new object[4];
                objects[0] = advisorVo;
                objects[1] = rmVo;
                objects[2] = rmId;
                objects[3] = userId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

       

        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string menu;
            try
            {
                RadComboBox MyDropDownList = (RadComboBox)sender; 
                //DropDownList MyDropDownList = (DropDownList)sender;                
                GridDataItem gvr = (GridDataItem)MyDropDownList.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;
                //int selectedRow = gvr.RowIndex;
                //userId = int.Parse(gvRMList.DataKeys[selectedRow].Value.ToString());
                userId = int.Parse(gvRMList.MasterTableView.DataKeyValues[selectedRow-1]["UserId"].ToString());

              
                Session["userId"] = userId;
                rmVo = advisorStaffBo.GetAdvisorStaff(userId);
                Session["CurrentrmVo"] = rmVo;
                menu = MyDropDownList.SelectedItem.Value.ToString();
                //Session["S_CurrentUserRole"] = "RM";
                if (menu == "Edit Profile")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EditRMDetails','none');", true);
                }
                if (menu == "View profile")
                {
                    Session["FromAdvisorView"] = "FromAdvView";

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewRMDetails','none');", true);
                }
                if (menu == "RM Dashboard")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMDashBoard','none');", true);
                }
                if (menu == "User Profile")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('GenerateLoginPassword','?GenLoginPassword_UserId=" + userId + "');", true);
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

                FunctionInfo.Add("Method", "ViewRM.ascx:ddlMenu_SelectedIndexChanged()");
                object[] objects = new object[3];
                objects[0] = advisorStaffBo;
                objects[1] = rmVo;
                objects[2] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }

        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void gvRMList_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                hdnSort.Value = sortExpression + " DESC";
                this.BindGrid();

            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                hdnSort.Value = sortExpression + " ASC";
                this.BindGrid();
            }
        }
        private void sortGridViewRM(string sortExpression, string direction)
        {
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            List<RMVo> advisorStaffList = null;
            RMVo rmVo = null;
            DataTable dtAdvisorStaff = new DataTable();
            DataRow drAdvisorStaff;
            try
            {

                advisorStaffList = advisorStaffBo.GetRMList(advisorVo.advisorId);

                dtAdvisorStaff.Columns.Add("Sl.No.");
                dtAdvisorStaff.Columns.Add("UserId");
                dtAdvisorStaff.Columns.Add("RM Name");
                dtAdvisorStaff.Columns.Add("Email");

                for (int i = 0; i < advisorStaffList.Count; i++)
                {
                    drAdvisorStaff = dtAdvisorStaff.NewRow();
                    rmVo = new RMVo();

                    rmVo = advisorStaffList[i];
                    drAdvisorStaff[0] = (i + 1).ToString();
                    drAdvisorStaff[1] = rmVo.UserId.ToString();
                    drAdvisorStaff[2] = rmVo.FirstName.ToString() + rmVo.MiddleName.ToString() + rmVo.LastName.ToString();
                    drAdvisorStaff[3] = rmVo.Email.ToString();
                    dtAdvisorStaff.Rows.Add(drAdvisorStaff);
                }
                DataView dv = new DataView(dtAdvisorStaff);
                dv.Sort = sortExpression + direction;
                //dv.Sort = (string)ViewState["sortExpression"];
                gvRMList.DataSource = dv;
                gvRMList.DataBind();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewRM.ascx.cs:sortGridViewRM()");
                object[] objects = new object[2];
                objects[1] = rmVo;
                objects[2] = advisorStaffList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlBMStaffList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBMStaffList.SelectedIndex == 0)
            {


                branchId = 0;
                bmIdOrHeadID = int.Parse(ddlBMStaffList.SelectedValue.ToString());
                all = 1;
                showRMList();
                //hdnbranchID.Value = "0";
                //hdnbranchHeadId.Value = ddlBMStaffList.SelectedValue;
                //hdnall.Value = "1";
                //showRMList();
            }
            else
            {
                branchId = int.Parse(ddlBMStaffList.SelectedValue.ToString());
                bmIdOrHeadID = 0;
                all = 0;
                showRMList();
                //hdnbranchID.Value = ddlBMStaffList.SelectedValue;
                //hdnbranchHeadId.Value = bmID.ToString();
                //hdnall.Value = "0";
                //showRMList();

            }
        }

        /* For Binding the Branch Dropdowns */

        private void BindBMDropDown(int bmId)
        {

            try
            {

                DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(0, bmId, 1);
                if (ds != null)
                {
                    ddlBMStaffList.DataSource = ds.Tables[1]; ;
                    ddlBMStaffList.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                    ddlBMStaffList.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                    ddlBMStaffList.DataBind();
                }
                ddlBMStaffList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmIdOrHeadID.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserMFMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvRMList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvRMList_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtMIS = new DataTable();
            dtMIS = (DataTable)Cache["RMList"];
            gvRMList.DataSource = dtMIS;
        }

        protected void ddlNameFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }

      
            /* End For Binding the Branch Dropdowns */
    }
}