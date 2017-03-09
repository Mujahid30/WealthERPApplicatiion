using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoAdvisorProfiling;
using BoAdvisorProfiling;
using VoUser;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class RMBranchAssociation : System.Web.UI.UserControl
    {
        AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        RMVo rmVo = new RMVo();
        int branchId;
        List<AdvisorBranchVo> advisorBranchList = null;
        List<AdvisorBranchVo> branchList = null;
        int index;
        string identify;
        int userId;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                //identify = Session["id"].ToString();
                advisorVo = (AdvisorVo)Session["advisorVo"];
                userVo = (UserVo)Session["UserVo"];
                rmVo = (RMVo)Session["rmVo"];

                if (!IsPostBack)
                {
                    //if (identify == "1")
                    //{
                    //    Label2.Visible = false;
                    //    DropDownList1.Visible = false;
                    //    rmVo = advisorStaffBo.GetAdvisorStaff(int.Parse(Session["userId"].ToString()));
                    //    setBranchList();
                    //}
                    //if (identify == "")
                    //{
                    Label2.Visible = true;
                    DropDownList1.Visible = true;
                    DropDownList1.Items.Clear();
                    trBranch.Visible = false;
                    trAssociatedBranch.Visible = false;
                    lblError.Visible = false;
                    lblError1.Visible = false;
                    showRM();
                    //}
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

                FunctionInfo.Add("Method", "RMBranchAssocistion.ascx:Page_Load()");

                object[] objects = new object[7];

                objects[0] = rmVo;
                objects[1] = advisorVo;
                objects[2] = advisorBranchVo;

                objects[3] = index;
                objects[4] = branchId;
                objects[5] = identify;
                objects[6] = userVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void showBranches()
        {
            DataSet ds;
            DataTable dt;
            DataRow dr;
            int rmId = 0;
            try
            {
                ShowAssociatedBranches();

                //advisorBranchList = advisorBranchBo.GetAdvisorBranches(advisorVo.advisorId);

                // rmId = int.Parse(Session["rmId1"].ToString());
                rmId = int.Parse(DropDownList1.SelectedItem.Value.ToString());

                if (advisorBranchBo.GetRMBranchAssociation(rmId, advisorVo.advisorId, "N") != null)
                {
                    ds = advisorBranchBo.GetRMBranchAssociation(rmId, advisorVo.advisorId, "N");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblError.Visible = false;
                        trBranch.Visible = true;
                        dt = new DataTable();
                        dt.Columns.Add("BranchId");
                        dt.Columns.Add("Branch Name");
                        dt.Columns.Add("Branch Code");

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dr = dt.NewRow();
                            dr["BranchId"] = ds.Tables[0].Rows[i]["AB_BranchId"].ToString();
                            dr["Branch Code"] = ds.Tables[0].Rows[i]["AB_BranchCode"].ToString();
                            dr["Branch Name"] = ds.Tables[0].Rows[i]["AB_BranchName"].ToString();
                            dt.Rows.Add(dr);
                        }
                        gvBranchList.DataSource = dt;
                        gvBranchList.DataBind();
                        gvBranchList.Visible = true;
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "No Records Found";
                    trBranch.Visible = true;
                    gvBranchList.DataSource = null;
                    gvBranchList.DataBind();
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

                FunctionInfo.Add("Method", "RMBranchAssociation.ascx.cs:showBranches()");

                object[] objects = new object[2];
                objects[0] = advisorBranchList;
                objects[1] = advisorBranchVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void ShowAssociatedBranches()
        {
            DataSet ds;
            DataTable dt;
            DataRow dr;
            int rmId = 0;
            try
            {
                // rmId = int.Parse(Session["rmId1"].ToString());
                rmId = int.Parse(DropDownList1.SelectedItem.Value.ToString());
                if ((ds = advisorBranchBo.GetRMBranchAssociation(rmId, advisorVo.advisorId, "A")) != null)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        trAssociatedBranch.Visible = true;
                        lblError1.Visible = false;

                        dt = new DataTable();
                        dt.Columns.Add("BranchId");
                        dt.Columns.Add("Branch Name");
                        dt.Columns.Add("Branch Code");


                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dr = dt.NewRow();
                            dr["BranchId"] = ds.Tables[0].Rows[i]["AB_BranchId"].ToString();
                            dr["Branch Code"] = ds.Tables[0].Rows[i]["AB_BranchCode"].ToString();
                            dr["Branch Name"] = ds.Tables[0].Rows[i]["AB_BranchName"].ToString();
                            dt.Rows.Add(dr);
                        }

                        gvRMBranch.DataSource = dt;
                        gvRMBranch.DataBind();
                        gvRMBranch.Visible = true;
                    }
                }
                else
                {
                    trAssociatedBranch.Visible = true;
                    lblError1.Visible = true;
                    lblError1.Text = "No Records Found";
                    gvRMBranch.DataSource = null;
                    gvRMBranch.DataBind();
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

                FunctionInfo.Add("Method", "ViewRMDetails.ascx.cs:BindBranchAssociation()");

                object[] objects = new object[1];
                objects[0] = rmVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void showRM()
        {
            List<RMVo> rmList = null;
            RMVo rmVo = null;
            try
            {
                rmList = advisorStaffBo.GetRMList(advisorVo.advisorId);
                DropDownList1.Items.Clear();
                DataTable dt = new DataTable();

                DataRow dr;
                for (int i = 0; i < rmList.Count; i++)
                {
                    dr = dt.NewRow();
                    rmVo = new RMVo();
                    ListItem rmListItem = new ListItem();
                    rmVo = rmList[i];
                    rmListItem.Text = rmVo.FirstName.ToString() + rmVo.LastName.ToString();
                    rmListItem.Value = rmVo.RMId.ToString();
                    DropDownList1.Items.Add(rmListItem);

                    dt.Rows.Add(dr);
                }
                DropDownList1.Items.Insert(0, "Select RM");
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMBranchAssocistion.ascx:showRM()");

                object[] objects = new object[2];
                objects[0] = rmList;
                objects[1] = rmVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void setBranchList()
        {

            UserVo rmUserVo = (UserVo)Session["rmUserVo"];
            try
            {
                advisorBranchList = advisorBranchBo.GetAdvisorBranches(advisorVo.advisorId, "");
                if (advisorBranchList.Count == 0)
                {
                    btnAssociate.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "Add some Branches..";
                }
                else
                {

                    gvBranchList.Visible = true;
                    btnAssociate.Visible = true;

                    lblError.Visible = false;
                    trBranch.Visible = false;
                    DataTable dtAdvisorBranch = new DataTable();
                    dtAdvisorBranch.Columns.Add("Sl.No.");
                    dtAdvisorBranch.Columns.Add("BranchId");
                    dtAdvisorBranch.Columns.Add("Branch Name");
                    dtAdvisorBranch.Columns.Add("Branch Address");
                    dtAdvisorBranch.Columns.Add("Branch Phone");

                    DataRow drAdvisorBranch;
                    for (int i = 0; i < advisorBranchList.Count; i++)
                    {
                        drAdvisorBranch = dtAdvisorBranch.NewRow();
                        advisorBranchVo = new AdvisorBranchVo();
                        advisorBranchVo = advisorBranchList[i];
                        drAdvisorBranch[0] = (i + 1).ToString();
                        drAdvisorBranch[1] = advisorBranchVo.BranchId.ToString();
                        drAdvisorBranch[2] = advisorBranchVo.BranchName.ToString();
                        drAdvisorBranch[3] = advisorBranchVo.AddressLine1.ToString() + "'" + advisorBranchVo.AddressLine2.ToString() + "'" + advisorBranchVo.AddressLine3.ToString() + "," + advisorBranchVo.City.ToString() + "'" + advisorBranchVo.State.ToString();
                        drAdvisorBranch[4] = advisorBranchVo.Phone1Isd + "-" + advisorBranchVo.Phone1Std + "-" + advisorBranchVo.Phone1Number;
                        dtAdvisorBranch.Rows.Add(drAdvisorBranch);

                    }
                    gvBranchList.DataSource = dtAdvisorBranch;
                    gvBranchList.DataBind();
                    gvBranchList.Visible = true;


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

                FunctionInfo.Add("Method", "RMBranchAssocistion.ascx:setBranchList()");


                object[] objects = new object[3];
                objects[0] = advisorBranchBo;
                objects[1] = advisorBranchList;
                objects[2] = advisorBranchVo;



                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnAssociate_Click(object sender, EventArgs e)
        {
            try
            {
                identify = Session["id"].ToString();
                userId = userVo.UserId;
                //if (identify == "1")
                //{
                //    rmVo = advisorStaffBo.GetAdvisorStaff(int.Parse(Session["userId"].ToString()));
                //  //  int rmId = int.Parse(Session["rmId"].ToString());
                //    int rmId = rmVo.RMId;
                //    foreach (GridViewRow gvr in this.gvBranchList.Rows)
                //    {
                //        if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                //        {
                //            branchId = int.Parse(gvBranchList.DataKeys[gvr.RowIndex].Value.ToString());
                //            if (advisorBranchBo.AssociateBranch(rmId, branchId,0, userId))
                //            {
                //               ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert(' Association is done successfully');", true);
                //            }
                //            else
                //            {
                //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Sorry..  Association is not done');", true);
                //            }
                //          //  advisorBranchBo.AssociateBranch(rmId, branchId, userId);
                //        }
                //    }
                //    //Session["id"] = "";
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Association done Successfully...!');", true);
                //}
                //else
                //{

                int rmId = int.Parse(DropDownList1.SelectedItem.Value.ToString());
                foreach (GridViewRow gvr in this.gvBranchList.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("chkId");
                    if ((chk).Checked == true)
                    {
                        branchId = int.Parse(gvBranchList.DataKeys[gvr.RowIndex].Value.ToString());
                        if (advisorBranchBo.AssociateBranch(rmId, branchId, 0, userId))
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert(' Association is done successfully');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Sorry.. Association is not done');", true);
                        }
                    }
                }
                //Session["id"] = "";
                showBranches();
                //  Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Association done Successfully...!');", true);
                //}
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewBranchAssociation','none');", true);
                btnAssociate.Enabled = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMBranchAssocistion.ascx:btnAssociate_Click()");

                object[] objects = new object[3];
                objects[0] = identify;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedIndex != 0)
            {
                gvRMBranch.Visible = true;
                Session["rmId1"] = DropDownList1.SelectedValue.ToString();
                Label2.Text = "RM Name:";
                showBranches();
                //Session["id"] = "";
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

        //protected void gvBranchListBM_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    string sortExpression = e.SortExpression;
        //    ViewState["sortExpression"] = sortExpression;
        //    if (GridViewSortDirection == SortDirection.Ascending)
        //    {
        //        GridViewSortDirection = SortDirection.Descending;
        //        sortGridViewBM(sortExpression,  DESCENDING);
        //    }
        //    else
        //    {
        //        GridViewSortDirection = SortDirection.Ascending;
        //        sortGridViewBM(sortExpression, ASCENDING);
        //    }

        //}

        //private void sortGridViewBM(string sortExpression, string direction)
        //{

        //    advisorBranchList = advisorBranchBo.GetAdvisorBranches(advisorVo.AdvisorId.ToString());
        //    gvBranchList.Visible = false;
        //    gvBranchListBM.Visible = true;
        //    btnAssociate.Visible = false;
        //    btnAssociateBM.Visible = true;
        //    lblError.Visible = false;
        //    lblBranchList.Visible = true;
        //    DataTable dtAdvisorBranch = new DataTable();
        //    dtAdvisorBranch.Columns.Add("Sl.No.");
        //    dtAdvisorBranch.Columns.Add("BranchId");
        //    dtAdvisorBranch.Columns.Add("Branch Name");
        //    dtAdvisorBranch.Columns.Add("Branch Address");
        //    dtAdvisorBranch.Columns.Add("Branch Phone");

        //    DataRow drAdvisorBranch;
        //    for (int i = 0; i < advisorBranchList.Count; i++)
        //    {
        //        drAdvisorBranch = dtAdvisorBranch.NewRow();
        //        advisorBranchVo = new AdvisorBranchVo();
        //        advisorBranchVo = advisorBranchList[i];
        //        drAdvisorBranch[0] = (i + 1).ToString();
        //        drAdvisorBranch[1] = advisorBranchVo.BranchId.ToString();
        //        drAdvisorBranch[2] = advisorBranchVo.BranchName.ToString();
        //        drAdvisorBranch[3] = advisorBranchVo.AddressLine1.ToString() + "'" + advisorBranchVo.AddressLine2.ToString() + "'" + advisorBranchVo.AddressLine3.ToString() + "," + advisorBranchVo.City.ToString() + "'" + advisorBranchVo.State.ToString();
        //        drAdvisorBranch[4] = advisorBranchVo.Phone1Isd + "-" + advisorBranchVo.Phone1Std + "-" + advisorBranchVo.Phone1Number;
        //        dtAdvisorBranch.Rows.Add(drAdvisorBranch);

        //    }
        //    DataView dv = new DataView(dtAdvisorBranch);
        //    dv.Sort = sortExpression + direction;
        //    gvBranchListBM.DataSource = dv;
        //    gvBranchListBM.DataBind();
        //    gvBranchListBM.Visible = true;

        //}

        protected void gvBranchList_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = "";
            try
            {
                sortExpression = e.SortExpression;
                ViewState["sortExpression"] = sortExpression;
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    GridViewSortDirection = SortDirection.Descending;
                    sortGridViewRM(sortExpression, DESCENDING);
                }
                else
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    sortGridViewRM(sortExpression, ASCENDING);
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

                FunctionInfo.Add("Method", "RMBranchAssociation.ascx.cs:gvBranchList_Sorting()");

                object[] objects = new object[1];
                objects[0] = sortExpression;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void sortGridViewRM(string sortExpression, string direction)
        {
            DataTable dtAdvisorBranch = new DataTable();
            try
            {
                gvBranchList.Visible = true;
                btnAssociate.Visible = true;
                lblError.Visible = false;
                trBranch.Visible = true;
                dtAdvisorBranch.Columns.Add("Sl.No.");
                dtAdvisorBranch.Columns.Add("BranchId");
                dtAdvisorBranch.Columns.Add("Branch Name");
                dtAdvisorBranch.Columns.Add("Branch Address");
                dtAdvisorBranch.Columns.Add("Branch Phone");
                DataRow drAdvisorBranch;
                for (int i = 0; i < advisorBranchList.Count; i++)
                {
                    drAdvisorBranch = dtAdvisorBranch.NewRow();
                    advisorBranchVo = new AdvisorBranchVo();
                    advisorBranchVo = advisorBranchList[i];
                    drAdvisorBranch[0] = (i + 1).ToString();
                    drAdvisorBranch[1] = advisorBranchVo.BranchId.ToString();
                    drAdvisorBranch[2] = advisorBranchVo.BranchName.ToString();
                    drAdvisorBranch[3] = advisorBranchVo.AddressLine1.ToString() + "'" + advisorBranchVo.AddressLine2.ToString() + "'" + advisorBranchVo.AddressLine3.ToString() + "," + advisorBranchVo.City.ToString() + "'" + advisorBranchVo.State.ToString();
                    drAdvisorBranch[4] = advisorBranchVo.Phone1Isd + "-" + advisorBranchVo.Phone1Std + "-" + advisorBranchVo.Phone1Number;
                    dtAdvisorBranch.Rows.Add(drAdvisorBranch);

                }
                DataView dv = new DataView(dtAdvisorBranch);
                dv.Sort = sortExpression + direction;
                gvBranchList.DataSource = dv;
                gvBranchList.DataBind();
                gvBranchList.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMBranchAssociation.ascx.cs:sortGridViewRM()");

                object[] objects = new object[2];
                objects[0] = advisorBranchList;
                objects[1] = advisorBranchVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
    }
}