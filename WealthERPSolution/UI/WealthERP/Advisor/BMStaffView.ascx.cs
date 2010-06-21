using System;
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

namespace WealthERP.Advisor
{
    public partial class BMStaffView : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo;
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        UserBo userBo = new UserBo();
        UserVo userVo = new UserVo();
        int rmId;
        int userId;
        int branchId;
        int index;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        List<int> rmList = new List<int>();

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
                FunctionInfo.Add("Method", "BMStaffView.ascx.cs:OnInit()");
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
                FunctionInfo.Add("Method", "BMStaffView.ascx.cs:HandlePagerEvent()");
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
                    ratio = rowCount / 15;
                    mypager.PageCount = rowCount % 15 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    lowerlimit = ((mypager.CurrentPage - 1) * 15).ToString();
                    upperlimit = (mypager.CurrentPage * 15).ToString();
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
                if (Session["BM"] != null)
                {
                    ShowBM();
                    Session.Remove("BM");
                }
                else
                {
                    ShowStaffList();
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
                FunctionInfo.Add("Method", "BMStaffView.ascx.cs:BindGrid()");
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
                FunctionInfo.Add("Method", "BMStaffView.ascx.cs:Page_Load()");
                object[] objects = new object[1];
                objects[0] = rmVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void ShowBM()
        {
            string bm = "";
            DataTable dtAdvisorStaff = new DataTable();
            DataRow drAdvisorStaff;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            int count = 0;
            try
            {
                bm = Session["BM"].ToString();

                if (hdnCurrentPage.Value.ToString() != "")
                {
                    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                    hdnCurrentPage.Value = "";
                }

                //ds = advisorStaffBo.FindBM(bm, advisorVo.advisorId, mypager.CurrentPage, hdnSort.Value.ToString(), out count);

                lblTotalRows.Text = hdnCount.Value = count.ToString();
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    lblMessage.Visible = false;
                    dtAdvisorStaff.Columns.Add("UserId");
                    dtAdvisorStaff.Columns.Add("StaffName");
                    dtAdvisorStaff.Columns.Add("Email");
                    dtAdvisorStaff.Columns.Add("MobileNo");
                    dtAdvisorStaff.Columns.Add("BranchName");
                    DataRow dr;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        drAdvisorStaff = dtAdvisorStaff.NewRow();
                        dr = dt.Rows[i];
                        rmVo = new RMVo();

                        drAdvisorStaff[0] = dr["u_userId"].ToString();
                        drAdvisorStaff[1] = dr["ar_firstname"].ToString() + " " + dr["ar_middlename"].ToString() + " " + dr["ar_lastname"].ToString();
                        drAdvisorStaff[2] = dr["AR_Email"].ToString();
                        drAdvisorStaff[3] = dr["AR_OfficePhoneDirectISD"].ToString() + "-" + dr["AR_OfficePhoneDirectSTD"].ToString() + "-" + dr["AR_OfficePhoneDirect"].ToString();
                        dtAdvisorStaff.Rows.Add(drAdvisorStaff);
                    }

                    gvBMStaffList.DataSource = dtAdvisorStaff;
                    gvBMStaffList.DataBind();
                    this.GetPageCount();
                }
                else
                {
                    gvBMStaffList.DataSource = null;
                    gvBMStaffList.DataBind();
                    DivPager.Visible = false;
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
                    lblMessage.Visible = true;
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
                FunctionInfo.Add("Method", "BMStaffView.ascx.cs:ShowRM()");
                object[] objects = new object[3];
                objects[0] = advisorVo;
                objects[1] = bm;
                objects[2] = rmList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void ShowStaffList()
        {
            int Count;
            DataSet dsRMList = new DataSet();

            try
            {
                if (userVo.UserType == "Branch Man")
                {
                    if (hdnCurrentPage.Value.ToString() != "")
                    {
                        mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                        hdnCurrentPage.Value = "";
                    }

                    dsRMList = advisorStaffBo.GetBMStaff(rmVo.RMId, mypager.CurrentPage, hdnSort.Value, out Count);
                    lblTotalRows.Text = hdnCount.Value = Count.ToString();

                    if (dsRMList != null)
                    {
                        DataTable dtBMStaff = new DataTable();
                        dtBMStaff.Columns.Add("UserId");
                        dtBMStaff.Columns.Add("StaffName");
                        dtBMStaff.Columns.Add("Email");
                        dtBMStaff.Columns.Add("MobileNo");
                        dtBMStaff.Columns.Add("BranchName");
                        DataRow drBMStaff;

                        for (int i = 0; i < dsRMList.Tables[0].Rows.Count ; i++)
                        {
                            drBMStaff = dtBMStaff.NewRow();
                            drBMStaff[0] = dsRMList.Tables[0].Rows[i]["U_UserId"].ToString();
                            drBMStaff[1] = dsRMList.Tables[0].Rows[i]["AR_FirstName"].ToString() + " " + dsRMList.Tables[0].Rows[i]["AR_MiddleName"].ToString() + " " + dsRMList.Tables[0].Rows[i]["AR_LastName"].ToString();
                            drBMStaff[2] = dsRMList.Tables[0].Rows[i]["AR_Email"].ToString();
                            drBMStaff[3] = dsRMList.Tables[0].Rows[i]["AR_Mobile"].ToString();
                            drBMStaff[4] = dsRMList.Tables[0].Rows[i]["AB_BranchCode"].ToString();
                            dtBMStaff.Rows.Add(drBMStaff);
                        }
                        gvBMStaffList.DataSource = dtBMStaff;
                        gvBMStaffList.DataBind();
                        this.GetPageCount();
                    }
                    else
                    {
                        lblMessage.Visible = false;
                    }
                }
                //else
                //{
                //    lblMessage.Visible = false;
                //    advisorStaffBo = new AdvisorStaffBo();
                //    List<RMVo> advisorStaffList = null;
                //    advisorStaffList = advisorStaffBo.GetRMList(advisorVo.advisorId, mypager.CurrentPage, hdnSort.Value, out Count);
                //    if (advisorStaffList != null)
                //    {
                //        lblTotalRows.Text = hdnCount.Value = Count.ToString();
                //        DataTable dtAdvisorStaff = new DataTable();
                //        dtAdvisorStaff.Columns.Add("UserId");
                //        dtAdvisorStaff.Columns.Add("StaffName");
                //        dtAdvisorStaff.Columns.Add("Email");
                //        dtAdvisorStaff.Columns.Add("MobileNo");
                //        DataRow drAdvisorStaff;

                //        for (int i = 0; i < advisorStaffList.Count; i++)
                //        {
                //            drAdvisorStaff = dtAdvisorStaff.NewRow();
                //            rmVo = new RMVo();
                //            rmVo = advisorStaffList[i];
                //            drAdvisorStaff[0] = rmVo.UserId.ToString();
                //            drAdvisorStaff[1] = rmVo.FirstName.ToString() + " " + rmVo.MiddleName.ToString() + " " + rmVo.LastName.ToString();
                //            drAdvisorStaff[2] = rmVo.Email.ToString();
                //            drAdvisorStaff[3] = rmVo.OfficePhoneDirectIsd.ToString() + "-" + rmVo.OfficePhoneDirectStd.ToString() + "-" + rmVo.OfficePhoneDirectNumber.ToString();
                //            dtAdvisorStaff.Rows.Add(drAdvisorStaff);
                //        }
                //        gvBMStaffList.DataSource = dtAdvisorStaff;
                //        gvBMStaffList.DataBind();

                //        this.GetPageCount();
                //    }
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
                FunctionInfo.Add("Method", "BMStaffView.ascx:showRMList()");
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
                userId = int.Parse(gvBMStaffList.DataKeys[selectedRow].Value.ToString());
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
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BMStaffView.ascx:ddlMenu_SelectedIndexChanged()");
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

        private void sortGridViewStaff(string sortExpression, string direction)
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
                dtAdvisorStaff.Columns.Add("BranchName");

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
                gvBMStaffList.DataSource = dv;
                gvBMStaffList.DataBind();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BMStaffView.ascx.cs:sortGridViewRM()");
                object[] objects = new object[2];
                objects[1] = rmVo;
                objects[2] = advisorStaffList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

    }
}