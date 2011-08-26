using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoAdvisorProfiling;
using BoAdvisorProfiling;
using VoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class ViewBranches : System.Web.UI.UserControl
    {
        AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        List<AdvisorBranchVo> advisorBranchList = null;
        AdvisorVo advisorVo = new AdvisorVo();
        int branchId;
        int index;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        List<int> branchList;

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
                FunctionInfo.Add("Method", "ViewBranches.ascx.cs:OnInit()");
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
                    PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
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
                FunctionInfo.Add("Method", "ViewBranches.ascx.cs:GetPageCount()");
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

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            string branch = "";
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["DeleteBranchFlag"] != null)
                    {
                        msgRecordStatus.Visible = true;
                        msgRecordStatus.InnerText = "Record Deleted Successfully...!";
                    }
                    else
                    {
                        msgRecordStatus.Visible = false;
                    }
                    mypager.CurrentPage = 1;
                   
                    this.BindGrid();
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
                FunctionInfo.Add("Method", "ViewBranches.ascx.cs:Page_Load()");
                object[] objects = new object[2];
                objects[0] = branch;
                objects[1] = branchList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void LoadBranches(List<int> branchList)
        {

            AdvisorBranchVo advisorBranchVo;
            DataTable dtAdvisorBranch = new DataTable();
            DataRow drAdvisorBranch;
            try
            {
                if (branchList != null)
                {

                    dtAdvisorBranch.Columns.Add("Sl.No.");
                    dtAdvisorBranch.Columns.Add("BranchId");
                    dtAdvisorBranch.Columns.Add("BranchName");
                    dtAdvisorBranch.Columns.Add("BranchCode");
                    dtAdvisorBranch.Columns.Add("Email");
                    dtAdvisorBranch.Columns.Add("Phone");
                    dtAdvisorBranch.Columns.Add("BranchHead");
                    dtAdvisorBranch.Columns.Add("BranchType");

                    for (int i = 0; i < branchList.Count; i++)
                    {
                        drAdvisorBranch = dtAdvisorBranch.NewRow();
                        advisorBranchVo = new AdvisorBranchVo();
                        advisorBranchVo = advisorBranchBo.GetBranch(branchList[i]);
                        drAdvisorBranch[0] = (i + 1).ToString();
                        drAdvisorBranch[1] = advisorBranchVo.BranchId.ToString();
                        drAdvisorBranch[2] = advisorBranchVo.BranchName.ToString();
                        drAdvisorBranch[3] = advisorBranchVo.BranchCode.ToString();                        
                        drAdvisorBranch[4] = advisorBranchVo.Email.ToString();
                        drAdvisorBranch[5] = advisorBranchVo.Phone1Isd.ToString() + "-" + advisorBranchVo.Phone1Std.ToString() + "-" + advisorBranchVo.Phone1Number.ToString();
                        drAdvisorBranch[6] = advisorBranchVo.BranchHead.ToString();
                        drAdvisorBranch[7] = advisorBranchVo.BranchType.ToString();
                        dtAdvisorBranch.Rows.Add(drAdvisorBranch);
                    }



                    gvBranchList.DataSource = dtAdvisorBranch;
                    gvBranchList.DataBind();
                    gvBranchList.Visible = true;
                    lblCurrentPage.Visible = true;
                    this.GetPageCount();
                    
                }
                else
                {
                    gvBranchList.DataSource = null;
                    gvBranchList.DataBind();
                    lblMessage.Visible = true;
                    tblPager.Visible = false;
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
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
                FunctionInfo.Add("Method", "ViewBranches.ascx.cs:LoadBranches()");
                object[] objects = new object[1];
                objects[0] = branchList;
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
                if (hdnCurrentPage.Value.ToString() != "")
                {
                    mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                    hdnCurrentPage.Value = "";
                }

                advisorVo = (AdvisorVo)Session["advisorVo"];
                int Count;              
              

                if (Session["Branch"] != null)
                {
                    lblMessage.Visible = false;
                    string branch = Session["Branch"].ToString();
                    if (branch.ToLower().Trim() == "find branch" || branch.ToLower().Trim() == "")
                        branch = "";
                    branchList = advisorBranchBo.FindBranch(branch, advisorVo.advisorId,mypager.CurrentPage,hdnSort.Value, out Count);
                    lblTotalRows.Text = hdnCount.Value = Count.ToString();
                    LoadBranches(branchList);
                    Session.Remove("Branch");
                }
                else 
                {
                    advisorBranchList = advisorBranchBo.GetAdvisorBranches(advisorVo.advisorId, hdnSort.Value, mypager.CurrentPage, out Count);
                    lblTotalRows.Text = hdnCount.Value = Count.ToString();
                    //if (Count > 0)
                    //{
                    //    tblPager.Visible = true;
                    //    lblTotalRows.Text = hdnCount.Value = Count.ToString();
                    //}
                    if (advisorBranchList.Count == 0)
                    {
                        lblMessage.Visible = true;
                        tblPager.Visible = false;
                        lblCurrentPage.Visible = false;
                        lblTotalRows.Visible = false;
                    }
                    else
                    {
                        lblMessage.Visible = false;
                        DataTable dtAdvisorBranch2 = new DataTable();
                        dtAdvisorBranch2.Columns.Add("Sl.No.");
                        dtAdvisorBranch2.Columns.Add("BranchId");
                        dtAdvisorBranch2.Columns.Add("BranchName");
                        dtAdvisorBranch2.Columns.Add("BranchCode");
                        dtAdvisorBranch2.Columns.Add("Email");
                        dtAdvisorBranch2.Columns.Add("Phone");      
                        dtAdvisorBranch2.Columns.Add("BranchHead");
                        dtAdvisorBranch2.Columns.Add("BranchType");

                        DataRow drAdvisorBranch;
                        for (int i = 0; i < advisorBranchList.Count; i++)
                        {
                            drAdvisorBranch = dtAdvisorBranch2.NewRow();
                            advisorBranchVo = new AdvisorBranchVo();
                            advisorBranchVo = advisorBranchList[i];
                            drAdvisorBranch[0] = (i + 1).ToString();
                            drAdvisorBranch[1] = advisorBranchVo.BranchId.ToString();
                            drAdvisorBranch[2] = advisorBranchVo.BranchName.ToString();
                            drAdvisorBranch[3] = advisorBranchVo.BranchCode.ToString();
                            drAdvisorBranch[4] = advisorBranchVo.Email;
                            drAdvisorBranch[5] = advisorBranchVo.Phone1Isd.ToString() + "-" + advisorBranchVo.Phone1Std.ToString() + "-" + advisorBranchVo.Phone1Number.ToString();
                            drAdvisorBranch[6] = advisorBranchVo.BranchHead.ToString();
                            drAdvisorBranch[7] = advisorBranchVo.BranchType.ToString();
                            dtAdvisorBranch2.Rows.Add(drAdvisorBranch);
                        }

                        gvBranchList.DataSource = dtAdvisorBranch2;
                        gvBranchList.DataBind();
                        gvBranchList.Visible = true;
                    }
                }
                this.GetPageCount();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewBranches.ascx:Page_Load()");
                object[] objects = new object[4];
                objects[0] = advisorVo;
                objects[1] = advisorBranchVo;
                objects[2] = advisorBranchBo;
                objects[3] = advisorBranchList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvBranchlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                index = Convert.ToInt32(e.CommandArgument);

                branchId = int.Parse(gvBranchList.DataKeys[index].Value.ToString());
                Session["branchId"] = branchId;
                advisorBranchVo = advisorBranchBo.GetBranch(branchId);
                Session["advisorBranchVo"] = advisorBranchVo;
                Session["FromAdvisorView"] = "FromAdvView";
                if (e.CommandName == "Edit")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EditBranchDetails','none');", true);
                }
                if (e.CommandName == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewBranchDetails','none');", true);
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

                FunctionInfo.Add("Method", "ViewBranches.ascx:gvBranchlist_RowCommand()");


                object[] objects = new object[2];
                objects[0] = index;
                objects[1] = branchId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvBranchList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvBranchList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvBranchList_Sorting(object sender, GridViewSortEventArgs e)
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
                
        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string menu;
            try
            {
                DropDownList MyDropDownList = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)MyDropDownList.NamingContainer;
                int selectedRow = gvr.RowIndex;
                int branchId = int.Parse(gvBranchList.DataKeys[selectedRow].Value.ToString());
                advisorBranchVo = advisorBranchBo.GetBranch(branchId);
                Session["advisorBranchVo"] = advisorBranchVo;

                menu = MyDropDownList.SelectedItem.Value.ToString();
                Session["FromAdvisorView"] = "FromAdvView";
                if (menu == "Edit")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EditBranchDetails','none');", true);
                }
                if (menu == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewBranchDetails','none');", true);
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
                objects[0] = advisorBranchBo;
                objects[1] = advisorBranchVo;
                objects[2] = branchId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }
}
