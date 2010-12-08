using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoAdvisorProfiling;
using VoUser;
using BoAdvisorProfiling;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class ViewBranchAssociation : System.Web.UI.UserControl
    {
        static Dictionary<string, string> genDictRM;
        static Dictionary<string, string> genDictBranch;
        AdvisorVo advisorVo;

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
                FunctionInfo.Add("Method", "ViewRM.ascx.cs:OnInit()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void GetPageCount()
        {
            string upperlimit = null;
            int rowCount = 0;
            int ratio = 0;
            string lowerlimit = null;
            string PageRecords = null;
            try
            {
                if (hdnRecordCount.Value.ToString() != "")
                    rowCount = Convert.ToInt32(hdnRecordCount.Value);
                if (rowCount > 0)
                {
                    ratio = rowCount / 10;
                    mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    if (((mypager.CurrentPage - 1) * 10) != 0)
                        lowerlimit = ((mypager.CurrentPage - 1) * 10).ToString();
                    else
                        lowerlimit = "1";
                    upperlimit = (mypager.CurrentPage * 10).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnRecordCount.Value;
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

                FunctionInfo.Add("Method", "RMCustomer.ascx.cs:GetPageCount()");

                object[] objects = new object[5];
                objects[0] = upperlimit;
                objects[0] = rowCount;
                objects[0] = ratio;
                objects[0] = lowerlimit;
                objects[0] = PageRecords;
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

                this.LoadBranchAssociation(mypager.CurrentPage);

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
                object[] objects = new object[1];
                objects[0] = mypager.CurrentPage;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["advisorVo"] != null)
                advisorVo = (AdvisorVo)Session["advisorVo"];
            SessionBo.CheckSession();
            if(!IsPostBack)
                LoadBranchAssociation(mypager.CurrentPage);
        }

        private void LoadBranchAssociation(int currentPage)
        {
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            UserVo userVo = null;
            DataSet ds = null;
            try
            {

                int Count;
                ds = advisorBranchBo.GetBranchAssociation(advisorVo.advisorId, currentPage, out Count, hdnBranchFilter.Value, hdnRMFilter.Value, hdnSort.Value, out genDictBranch, out genDictRM);

                if (Count > 0)
                {
                    lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
                    tblPager.Visible = true;
                }

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblMsg.Visible = false;

                        DataTable dt = new DataTable();
                        dt.Columns.Add("BranchId");
                        dt.Columns.Add("RMId");
                        dt.Columns.Add("Branch Name");
                        dt.Columns.Add("RM Name");

                        DataRow dr;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dr = dt.NewRow();
                            dr["BranchId"] = ds.Tables[0].Rows[i]["branchId"].ToString();
                            dr["RMId"] = ds.Tables[0].Rows[i]["RMId"].ToString();
                            dr["Branch Name"] = ds.Tables[0].Rows[i]["BranchName"].ToString();
                            dr["RM Name"] = ds.Tables[0].Rows[i]["RMName"].ToString();
                            dt.Rows.Add(dr);
                        }
                        gvBranchList.DataSource = dt;
                        gvBranchList.DataBind();

                        if (genDictBranch.Count > 0)
                        {
                            DropDownList ddlBranch = GetBranchDDL();
                            if (ddlBranch != null)
                            {
                                ddlBranch.DataSource = genDictBranch;
                                ddlBranch.DataTextField = "Value";
                                ddlBranch.DataValueField = "Key";
                                ddlBranch.DataBind();
                                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                            }
                            if (hdnBranchFilter.Value != "")
                            {
                                ddlBranch.SelectedValue = hdnBranchFilter.Value.ToString();
                            }
                        }

                        if (genDictRM.Count > 0)
                        {
                            DropDownList ddlRM = GetRMDDL();
                            if (ddlRM != null)
                            {
                                ddlRM.DataSource = genDictRM;
                                ddlRM.DataTextField = "Value";
                                ddlRM.DataValueField = "Key";
                                ddlRM.DataBind();
                                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                            }
                            if (hdnRMFilter.Value != "")
                            {
                                ddlRM.SelectedValue = hdnRMFilter.Value.ToString();
                            }
                        }

                        gvBranchList.Visible = true;
                        this.GetPageCount();
                    }
                }
                else
                {
                    gvBranchList.DataSource = null;
                    gvBranchList.DataBind();

                    lblMsg.Visible = true;
                    lblMsg.Text = "Branch Association is not done..! ";
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
                FunctionInfo.Add("Method", "ViewBranchAssociation.ascx.cs:LoadBranchAssociation()");
                object[] objects = new object[2];
                objects[0] = ds;
                objects[1] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private DropDownList GetBranchDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvBranchList.HeaderRow != null)
            {
                if ((DropDownList)gvBranchList.HeaderRow.FindControl("ddlBranchName") != null)
                {
                    ddl = (DropDownList)gvBranchList.HeaderRow.FindControl("ddlBranchName");
                }
            }
            else
                ddl = null;

            return ddl;
        }

        private DropDownList GetRMDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvBranchList.HeaderRow != null)
            {
                if ((DropDownList)gvBranchList.HeaderRow.FindControl("ddlRMName") != null)
                {
                    ddl = (DropDownList)gvBranchList.HeaderRow.FindControl("ddlRMName");
                }
            }
            else
                ddl = null;

            return ddl;
        }

        protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlBranch = GetBranchDDL();

            if (ddlBranch != null)
            {
                if (ddlBranch.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnBranchFilter.Value = ddlBranch.SelectedValue;
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnBranchFilter.Value = "";
                }


                this.LoadBranchAssociation(mypager.CurrentPage);

            }
        }

        protected void ddlRMName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlRM = GetRMDDL();

            if (ddlRM != null)
            {
                if (ddlRM.SelectedIndex != 0)
                {   // Bind the Grid with Only Selected Values
                    hdnRMFilter.Value = ddlRM.SelectedValue;
                }
                else
                {   // Bind the Grid with Only All Values
                    hdnRMFilter.Value = "";
                }


                this.LoadBranchAssociation(mypager.CurrentPage);

            }
        }

        //protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string menu;
        //    int rmId, branchId;

        //        DropDownList MyDropDownList = (DropDownList)sender;
        //        GridViewRow gvr = (GridViewRow)MyDropDownList.NamingContainer;
        //        int selectedRow = gvr.RowIndex;
        //        rmId = int.Parse(gvBranchList.DataKeys[selectedRow].Values["RMId"].ToString());
        //        branchId = int.Parse(gvBranchList.DataKeys[selectedRow].Values["BranchId"].ToString());
        //        List<int> AssociationList = new List<int>();
        //        AssociationList.Add(rmId);
        //        AssociationList.Add(branchId);
        //        Session["associationList"] = AssociationList;                
        //        menu = MyDropDownList.SelectedItem.Value.ToString();
        //        if (menu == "View Association")
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewAssociation','none');", true);
        //        }



        //}

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                foreach (GridViewRow dr in gvBranchList.Rows)
                {

                    if (((CheckBox)dr.FindControl("chkBx")).Checked == true)
                    {
                        i = i + 1;
                    }
                }
                if (i == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select the Associations to be removed!');", true);
                }
                else
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
                FunctionInfo.Add("Method", "ViewBranchDetails.ascx.cs:btnDeleteSelected_Click()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        protected void hiddenDelete_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                DeleteBranchAssociation();
            }
            else
            {
                ClearCheckBox();
            }
        }

        protected void ClearCheckBox()
        {
            foreach (GridViewRow dr in gvBranchList.Rows)
            {
                CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
                checkBox.Checked = false;
            }
        }

        private void DeleteBranchAssociation()
        {
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            bool result = false;
            int rmId = 0;
            int branchId = 0;
            int count = 0;
            string asscNotDeleted =string.Empty;
 
            try
            {
                foreach (GridViewRow dr in gvBranchList.Rows)
                {
                    CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
                    rmId = Convert.ToInt32(gvBranchList.DataKeys[dr.RowIndex].Values["RMId"].ToString());
                    branchId = Convert.ToInt32(gvBranchList.DataKeys[dr.RowIndex].Values["BranchId"].ToString());
                    Label lblRMName= (Label) dr.FindControl("lblRMName");
                    Label lblBranchName = (Label) dr.FindControl("lblBranchName");
                    
    
                    if (checkBox.Checked)
                    {
                        count = advisorBranchBo.CheckBranchHead(rmId, branchId);
                        if (count > 0)
                        {
                            asscNotDeleted = asscNotDeleted + lblBranchName.Text + " - " + lblRMName.Text + "\\n";
                        }
                        else
                        {
                            rmId = Convert.ToInt32(gvBranchList.DataKeys[dr.RowIndex].Values["RMId"].ToString());
                            branchId = Convert.ToInt32(gvBranchList.DataKeys[dr.RowIndex].Values["BranchId"].ToString());
                            result= advisorBranchBo.DeleteBranchAssociation(rmId, branchId);
                        }
                    }
                }
                if (asscNotDeleted.Length == 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Removed Successfully..');", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('" + asscNotDeleted + "\\n \\n Above associations cannot be deleted as the RM is the Branch head of that Branch');", true);
                }

               
                mypager.CurrentPage = int.Parse(hdnCurrentPage.Value.ToString());

                LoadBranchAssociation(mypager.CurrentPage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewBranchAssociation.ascx.cs:btnDeleteSelected_Click()");
                object[] objects = new object[3];
                objects[0] = rmId;
                objects[1] = branchId;
                objects[2] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }
}