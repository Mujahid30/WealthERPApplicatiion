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


namespace WealthERP.Advisor
{
    public partial class ViewRM : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo;
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        UserBo userBo = new UserBo();
        UserVo userVo = new UserVo();
        int rmId;
        int bmID;
        int userId;
        int branchId;
        string userType;
        int adviserID;

        int index;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        List<int> rmList = new List<int>();
        string currentRole = "";

        protected override void OnInit(EventArgs e)
        {
            try
            {

                ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
                mypager.EnableViewState = true;
                base.OnInit(e);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewRM.ascx.cs:OnInit()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            try
            {
                GetPageCount();
                this.BindGrid();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewRM.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void GetPageCount()
        {
            string upperlimit = "";
            int rowCount = 0;
            int ratio = 0;
            string lowerlimit = "";
            string PageRecords = "";
            try
            {
                if (hdnCount.Value != "")
                    rowCount = Convert.ToInt32(hdnCount.Value);
                if (rowCount > 0)
                {
                    ratio = rowCount / 10;
                    mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    lowerlimit = (((mypager.CurrentPage - 1) * 10)+1).ToString();
                    upperlimit = (mypager.CurrentPage * 10).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnCount.Value;
                    PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                    lblCurrentPage.Text = PageRecords;
                    hdnCurrentPage.Value = mypager.CurrentPage.ToString();
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
                FunctionInfo.Add("Method", "ViewRM.ascx.cs:GetPageCount()");
                object[] objects = new object[5];
                objects[0] = upperlimit;
                objects[1] = rowCount;
                objects[2] = ratio;
                objects[3] = lowerlimit;
                objects[4] = PageRecords;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void BindGrid()
        {
            try
            {

                advisorVo = (AdvisorVo)Session["advisorVo"];
                userVo = (UserVo)Session["UserVo"];
                if (hdnCurrentPage.Value.ToString() != "")
                {
                    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                    hdnCurrentPage.Value = "";
                }
                if (userType == "advisor")
                {
                    ShowRM();
                }
                if (Session["RM"] != null)
                {

                    ShowRM();
                    Session.Remove("RM");

                }
                else if(userType == "bm")
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
            SessionBo.CheckSession();

            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
                userType = "advisor";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            userVo = (UserVo)Session["userVo"];
            rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
            bmID = rmVo.RMId;
            adviserID = advisorVo.advisorId;



            if (userType == "bm")
            {
                lblChooseBr.Visible = true;
                ddlBMStaffList.Visible = true;
                if (!IsPostBack)
                {
                    BindBranchForBMDropDown();
                    hdnbranchID.Value = "0";
                    hdnbranchHeadId.Value = ddlBMStaffList.SelectedValue;
                    hdnall.Value = "1";

                    mypager.CurrentPage = 1;
                    this.BindGrid();
                }
            }
            if (userType == "advisor")
            {
                lblChooseBr.Visible = false;
                ddlBMStaffList.Visible = false;
                if (!IsPostBack)
                {
                    BindBranchDropDown();
                    hdnbranchID.Value = "0";
                    hdnbranchHeadId.Value = adviserID.ToString();
                    hdnall.Value = "1";

                    mypager.CurrentPage = 1;
                    this.BindGrid();
                }
            }


            string rm = "";
            try
            {
                if (!IsPostBack)
                {
                    

                    mypager.CurrentPage = 1;
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
                if (Session["RM"]!=null)
                    rm = Session["RM"].ToString();
                else
                    rm = string.Empty;
              
                if (rm.ToLower().Trim() == "find rm" || rm.ToLower().Trim() == "")
                    rm = "";

                if(userType == "advisor")


                    ds = advisorStaffBo.FindRM(rm, advisorVo.advisorId, mypager.CurrentPage, hdnSort.Value.ToString(), out count);

                lblTotalRows.Text = hdnCount.Value = count.ToString();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    dt = ds.Tables[0];
                    trMessage.Visible = false;
                    dtAdvisorStaff.Columns.Add("UserId");
                    dtAdvisorStaff.Columns.Add("WealthERP Id");
                    dtAdvisorStaff.Columns.Add("RMName");
                    //dtAdvisorStaff.Columns.Add("RM Main Branch");
                    dtAdvisorStaff.Columns.Add("StaffType");
                    dtAdvisorStaff.Columns.Add("StaffRole");
                    dtAdvisorStaff.Columns.Add("Email");
                    dtAdvisorStaff.Columns.Add("Mobile Number");
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
                        dtAdvisorStaff.Rows.Add(drAdvisorStaff);
                    }

                    gvRMList.DataSource = dtAdvisorStaff;
                    gvRMList.DataBind();
                    this.GetPageCount();
                }
                else
                {
                    gvRMList.DataSource = null;
                    gvRMList.DataBind();
                    DivPager.Visible = false;
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
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
            /* Coding For Staff under branches in BM */ 
            rmVo = new RMVo();
            int Count = 0;
            int currentPage = 1;
            List<RMVo> rmList = new List<RMVo>();
            List<string> roleList = new List<string>();
            string role = "BM";
            try
            {
                if (Session[SessionContents.CurrentUserRole] != null)
                    currentRole = Session[SessionContents.CurrentUserRole].ToString();
                if (currentRole == role)
                {
                    rmList = advisorStaffBo.GetBMRMList(int.Parse(hdnbranchID.Value.ToString()), int.Parse(hdnbranchHeadId.Value.ToString()), int.Parse(hdnall.Value.ToString()), currentPage, out Count);
                    if (rmList.Count != 0)
                    {
                        lblTotalRows.Text = hdnCount.Value = Count.ToString();
                        DataTable dtAdvisorStaff = new DataTable();
                        dtAdvisorStaff.Columns.Add("UserId");
                        dtAdvisorStaff.Columns.Add("WealthERP Id");
                        dtAdvisorStaff.Columns.Add("RMName");
                        dtAdvisorStaff.Columns.Add("StaffType");
                        dtAdvisorStaff.Columns.Add("Email");
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
                                drAdvisorStaff[3] = "External";
                            else
                                drAdvisorStaff[3] = "Internal";
                            drAdvisorStaff[4] = rmVo.Email.ToString();
                            drAdvisorStaff[5] = rmVo.Mobile.ToString();
                            dtAdvisorStaff.Rows.Add(drAdvisorStaff);
                        }
                        gvRMList.DataSource = dtAdvisorStaff;
                        gvRMList.DataBind();
                        this.GetPageCount();
                    }
                    else
                    {
                        gvRMList.DataSource = null;
                        gvRMList.DataBind();
                        DivPager.Visible = false;
                        lblCurrentPage.Visible = false;
                        lblTotalRows.Visible = false;
                        trMessage.Visible = true;
                    }
                }
                else
                {
                    gvRMList.DataSource = null;
                    gvRMList.DataBind();
                    DivPager.Visible = false;
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
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
                DropDownList MyDropDownList = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)MyDropDownList.NamingContainer;
                int selectedRow = gvr.RowIndex;
                userId = int.Parse(gvRMList.DataKeys[selectedRow].Value.ToString());
                Session["userId"] = userId;
                rmVo = advisorStaffBo.GetAdvisorStaff(userId);
                Session["rmVo"] = rmVo;
                menu = MyDropDownList.SelectedItem.Value.ToString();
                if (menu == "Edit profile")
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
                hdnbranchID.Value = "0";
                hdnbranchHeadId.Value = ddlBMStaffList.SelectedValue;
                hdnall.Value = "1";
                showRMList();
            }
            else
            {
                hdnbranchID.Value = ddlBMStaffList.SelectedValue;
                hdnbranchHeadId.Value = bmID.ToString();
                hdnall.Value = "0";
                showRMList();

            }
        }


        private void BindBranchDropDown()
        {
            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null)
                {
                    ddlBMStaffList.DataSource = ds;
                    ddlBMStaffList.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlBMStaffList.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlBMStaffList.DataBind();
                }
                ddlBMStaffList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        
        /* For Binding the Branch Dropdowns */

        private void BindBranchForBMDropDown()
        {

            try
            {

                DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(0, bmID, 0);
                if (ds != null)
                {
                    ddlBMStaffList.DataSource = ds.Tables[1]; ;
                    ddlBMStaffList.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                    ddlBMStaffList.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                    ddlBMStaffList.DataBind();
                }
                ddlBMStaffList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
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
        /* End For Binding the Branch Dropdowns */
    }
}
























        //protected void btn_Print_Click(object sender, EventArgs e)
        //{


        //    //PrepareGridViewForExport(gvRMList);

        //    //ExportGridView();
        //    //fn(gvRMList);


        //}



        //private void fn(GridView gvRMList)
        //{



        //    print.Controls.Remove(gvRMList);
        //    gvRMList.AllowPaging = false;
        //    print.Controls.Add(gvRMList);

        //    try
        //    {
        //        StringBuilder l_objStrBuilder = new StringBuilder();
        //        l_objStrBuilder.Append(print.InnerHtml);
        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.ContentType = "application/vnd.ms-excel";
        //        Response.AddHeader("Content-Disposition", "attachment;filename=WAYBILL.xls");
        //        Response.Write(l_objStrBuilder.ToString());
        //        Response.Flush();
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    //System.Web.HttpContext context = System.Web.HttpContext.Current;
        //    ////export to excel

        //    //context.Response.Clear();
        //    //context.Response.Buffer = true;
        //    //context.Response.ContentType = "application/vnd.ms-excel";
        //    //context.Response.Charset = "";
        //    //this.Page.EnableViewState = false;

        //    //System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        //    //System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

        //    //this.ClearControls(gvRMList);
        //    //gvRMList.RenderControl(oHtmlTextWriter);

        //    //context.Response.Write(oStringWriter.ToString());

        //    //context.Response.End();

        //}

        //private void ClearControls(Control control)
        //{
        //    for (int i = control.Controls.Count - 1; i >= 0; i--)
        //    {
        //        ClearControls(control.Controls[i]);
        //    }

        //    if (!(control is TableCell))
        //    {
        //        if (control.GetType().GetProperty("SelectedItem") != null)
        //        {
        //            LiteralControl literal = new LiteralControl();
        //            control.Parent.Controls.Add(literal);
        //            try
        //            {
        //                literal.Text = (string)control.GetType().GetProperty("SelectedItem").GetValue(control, null);
        //            }
        //            catch
        //            {

        //            }

        //            control.Parent.Controls.Remove(control);
        //        }

        //        else

        //            if (control.GetType().GetProperty("Text") != null)
        //            {
        //                LiteralControl literal = new LiteralControl();
        //                control.Parent.Controls.Add(literal);
        //                literal.Text = (string)control.GetType().GetProperty("Text").GetValue(control, null);
        //                control.Parent.Controls.Remove(control);
        //            }
        //    }
        //    return;
        //}



        //private void PrepareGridViewForExport(Control gv)
        //{

        //    this.Page.EnableViewState = false;

        ////    LinkButton lb = new LinkButton();
        //    Literal l = new Literal();
        //    string name = String.Empty;
        //    for (int i = 0; i < gv.Controls.Count; i++)
        //    {

        //        if (gv.Controls[i].GetType() == typeof(LinkButton))
        //        {

        //            l.Text = (gv.Controls[i] as LinkButton).Text;

        //            gv.Controls.Remove(gv.Controls[i]);

        //            gv.Controls.AddAt(i, l);

        //        }

        //        else if (gv.Controls[i].GetType() == typeof(DropDownList))
        //        {

        //            l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;

        //            gv.Controls.Remove(gv.Controls[i]);

        //            gv.Controls.AddAt(i, l);

        //        }

        //        else if (gv.Controls[i].GetType() == typeof(CheckBox))
        //        {

        //            l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";

        //            gv.Controls.Remove(gv.Controls[i]);

        //            gv.Controls.AddAt(i, l);

        //        }

        //        if (gv.Controls[i].HasControls())
        //        {

        //            PrepareGridViewForExport(gv.Controls[i]);

        //        }

        //    }

        //}
        //private void ExportGridView()
        //{

        //    //string attachment = "attachment; filename=Contacts.xls";

        //    //Response.ClearContent();

        //    //Response.AddHeader("content-disposition", attachment);

        //    //Response.ContentType = "application/ms-excel";

        //    //StringWriter sw = new StringWriter();

        //    //HtmlTextWriter htw = new HtmlTextWriter(sw);

        //    //GridView1.RenderControl(htw);

        //    //Response.Write(sw.ToString());

        //    //Response.End();
        //    this.Page.EnableViewState = false;

        //    string attachment = "attachment; filename=Contacts.xls";

        //    Response.ClearContent();

        //    Response.AddHeader("content-disposition", attachment);

        //    Response.ContentType = "application/ms-excel";

        //    StringWriter sw = new StringWriter();

        //    HtmlTextWriter htw = new HtmlTextWriter(sw);



        //    // Create a form to contain the grid

        //    HtmlForm frm = new HtmlForm();

        //    gvRMList.Parent.Controls.Add(frm);

        //    frm.Attributes["runat"] = "server";

        //    frm.Controls.Add(gvRMList);



        //    frm.Render(htw);

        //    //GridView1.RenderControl(htw);

        //    Response.Write(sw.ToString());

        //    Response.End();

        //}











        //protected void gvRMList_DataBound(object sender, EventArgs e)
        //{
        //    string rm = " ";
        //    try
        //    {
        //        if (Session["RM"] != null)
        //        {


        //            rm = Session["RM"].ToString();
        //            rmList = advisorStaffBo.FindRM(rm, advisorVo.advisorId,mypager.CurrentPage);
        //            if (rmList.Count != 0)
        //            {
        //                gvRMList.FooterRow.Cells[0].Text = "Total Records: " + rmList.Count.ToString();
        //                gvRMList.FooterRow.Cells[0].ColumnSpan = gvRMList.FooterRow.Cells.Count;
        //                for (int i = 1; i < gvRMList.FooterRow.Cells.Count; i++)
        //                {
        //                    gvRMList.FooterRow.Cells[i].Visible = false;
        //                }
        //            }
        //            Session.Remove("RM");

        //        }
        //        else
        //        {
        //            List<RMVo> rmList = new List<RMVo>();
        //            if (userVo.UserType == "Branch Man")
        //            {
        //                rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
        //                branchId = advisorBranchBo.GetBranchId(rmVo.RMId);
        //                rmList = advisorStaffBo.GetBMRMList(branchId);
        //                gvRMList.FooterRow.Cells[0].Text = "Total Records: " + rmList.Count.ToString();
        //                gvRMList.FooterRow.Cells[0].ColumnSpan = gvRMList.FooterRow.Cells.Count;
        //                for (int i = 1; i < gvRMList.FooterRow.Cells.Count; i++)
        //                {
        //                    gvRMList.FooterRow.Cells[i].Visible = false;
        //                }

        //            }
        //            else
        //            {
        //                advisorStaffBo = new AdvisorStaffBo();
        //                List<RMVo> advisorStaffList = null;
        //                advisorStaffList = advisorStaffBo.GetRMList(advisorVo.advisorId);
        //                gvRMList.FooterRow.Cells[0].Text = "Total Records: " + advisorStaffList.Count.ToString();
        //                gvRMList.FooterRow.Cells[0].ColumnSpan = gvRMList.FooterRow.Cells.Count;
        //                for (int i = 1; i < gvRMList.FooterRow.Cells.Count; i++)
        //                {
        //                    gvRMList.FooterRow.Cells[i].Visible = false;
        //                }
        //            }

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
        //        FunctionInfo.Add("Method", "ViewRM.ascx.cs:gvRMList_DataBound()");
        //        object[] objects = new object[4];
        //        objects[0] = rm;
        //        objects[1] = rmList;
        //        objects[2] = branchId;
        //        objects[3] = userId;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }



        //}




