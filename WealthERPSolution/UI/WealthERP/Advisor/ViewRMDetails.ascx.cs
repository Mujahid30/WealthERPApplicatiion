using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoAdvisorProfiling;
using BoAdvisorProfiling;
using VoUser;
using BoUser;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class ViewRMDetails : System.Web.UI.UserControl
    {   
        AdvisorBranchVo advisorBranchVo = null;   
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        UserVo userVo = new UserVo();
        string rmId;
        string userId;
        RMVo rmVo = new RMVo();
        List<AdvisorBranchVo> advisorBranchList = null;
        DataSet _commondatasetdestination;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session["userVo"];

                if (!IsPostBack)
                {
                    advisorVo = (AdvisorVo)Session["advisorVo"];
                    ViewRMDetail();
                    BindBranchAssociation();

                    if (Session["FromAdvisorView"] != null)
                    {
                        if (Session["FromAdvisorView"].ToString() == "FromAdvView")
                        {
                            //.ToString() == "FromAdvView")
                            advisorVo = (AdvisorVo)Session["advisorVo"];
                            lnkEdit.Visible = true;
                            lnkBtnBack.Visible = true;
                            //Session.Remove("FromAdvisorView");
                        }
                        else
                        {
                            lnkEdit.Visible = false;
                            lnkBtnBack.Visible = false;
                        }
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
                FunctionInfo.Add("Method", "ViewRMDetails.ascx.cs:Page_Load()");
                object[] objects = new object[2];
                objects[0] = advisorVo;
                objects[1] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        //private void BindBranchAssociation()
        //{
        //    DataSet ds;
        //    DataTable dt;
        //    DataRow dr;
        //    try
        //    {
        //        if (rmVo.IsExternal == 1)
        //        {
        //            setBranchList("Y");
        //        }
        //        else
        //        {
        //            setBranchList("N");
        //        }
                

        //        if (Session["CurrentrmVo"] != null)
        //        {
        //            rmVo = (RMVo)Session["CurrentrmVo"];
        //        }
        //        else
        //        {
        //            rmVo = (RMVo)Session["rmVo"];
        //        }
                

        //        if (Session["advisorVo"] != null)
        //        {
        //            ds = advisorBranchBo.GetRMBranchAssociation(rmVo.RMId, advisorVo.advisorId, "A");
        //        }
        //        else
        //        {
        //            ds = advisorBranchBo.GetRMBranchAssociation(rmVo.RMId, 0, "A");
        //        }

        //        if (ds != null)
        //        {
        //            trBranchAssoication.Visible = true;
        //            dt = new DataTable();
        //            dt.Columns.Add("BranchId");
                   
        //            dt.Columns.Add("Branch Code");
        //            dt.Columns.Add("Branch Name");

        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {
        //                dr = dt.NewRow();
        //                dr["BranchId"] = ds.Tables[0].Rows[i]["AB_BranchId"].ToString();
        //                dr["Branch Code"] = ds.Tables[0].Rows[i]["AB_BranchCode"].ToString();
        //                dr["Branch Name"] = ds.Tables[0].Rows[i]["AB_BranchName"].ToString();
        //                dt.Rows.Add(dr);
        //            }
        //            associatedBranch.DataSource = dt;
        //            associatedBranch.DataTextField = "Branch Name";
        //            associatedBranch.DataValueField = "Branch Code";
        //            associatedBranch.DataBind();
        //            associatedBranch.Enabled = false;
        //            availableBranch.Enabled = false;
        //            //gvRMBranch.DataSource = dt;
        //            //gvRMBranch.DataBind();
        //            //gvRMBranch.Visible = true;
        //        }
        //        else
        //        {
        //            //trBranchAssoication.Visible = false;
        //            //gvRMBranch.DataSource = null;
        //            //gvRMBranch.DataBind();
        //            //gvRMBranch.Visible = false;
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

        //        FunctionInfo.Add("Method", "ViewRMDetails.ascx.cs:BindBranchAssociation()");

        //        object[] objects = new object[2];
        //        objects[0] = rmVo;
        //        objects[1] = advisorVo;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }


        //}

        private void BindBranchAssociation()
        {
            DataSet ds;
            //DataTable dt;
            DataTable dtList;
            //DataRow dr;
            DataRow drList;
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            try
            {
                if (Session["CurrentrmVo"] != null)
                {
                    rmVo = (RMVo)Session["CurrentrmVo"];
                }
                else
                {
                    rmVo = (RMVo)Session["rmVo"];
                }

                if (Session["advisorVo"] != null)
                {
                    ds = advisorBranchBo.GetRMBranchAssociation(rmVo.RMId, advisorVo.advisorId, "A");
                }
                else
                {
                    ds = advisorBranchBo.GetRMBranchAssociation(rmVo.RMId, 0, "A");
                }

                if (ds != null)
                {
                    _commondatasetdestination = ds;
                    //dt = new DataTable();
                    //dt.Columns.Add("BranchId");
                    //dt.Columns.Add("RMId");
                    //dt.Columns.Add("Branch Name");
                    //dt.Columns.Add("Branch Code");
                    dtList = new DataTable();
                    dtList.Columns.Add("Branch");
                    dtList.Columns.Add("BranchId");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //dr = dt.NewRow();
                        //dr["BranchId"] = ds.Tables[0].Rows[i]["AB_BranchId"].ToString();
                        //dr["RMId"] = ds.Tables[0].Rows[i]["AR_RMId"].ToString();
                        //dr["Branch Code"] = ds.Tables[0].Rows[i]["AB_BranchCode"].ToString();
                        //dr["Branch Name"] = ds.Tables[0].Rows[i]["AB_BranchName"].ToString();

                        drList = dtList.NewRow();
                        drList["Branch"] = ((ds.Tables[0].Rows[i]["AB_BranchName"].ToString()) + "," + (ds.Tables[0].Rows[i]["AB_BranchCode"].ToString()));
                        drList["BranchId"] = ds.Tables[0].Rows[i]["AB_BranchId"].ToString();
                        //dt.Rows.Add(dr);
                        dtList.Rows.Add(drList);
                        hdnExistingBranches.Value += drList["BranchId"].ToString() + ",";
                    }

                    Session["AssociatedBranch"] = dtList;
                    
                    // Show binded contents in List box
                    associatedBranch.DataSource = dtList;
                    associatedBranch.DataTextField = "Branch";
                    associatedBranch.DataValueField = "BranchId";
                    associatedBranch.DataBind();

                  

                    if (rmVo.IsExternal == 1)
                    {
                        setBranchList("Y");
                    }
                    else
                    {
                        setBranchList("N");
                    }

                }
                else
                {
                    if (rmVo.IsExternal == 1)
                    {
                        setBranchList("Y");
                    }
                    else
                    {
                        setBranchList("N");
                    }

                    //gvBranchList.Visible = false;
                }
                associatedBranch.Enabled = false;
                availableBranch.Enabled = false;


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

                object[] objects = new object[2];
                objects[0] = rmVo;
                objects[1] = advisorVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void ViewRMDetail()
        {
            try
            {
                if (Session["rmVo"] != null)
                {
                    if (Session["CurrentrmVo"] != null)
                    {
                        rmVo = (RMVo)Session["CurrentrmVo"];
                    }
                    else
                    {
                        rmVo = (RMVo)Session["rmVo"];
                    }
                    string[] RoleListArray = rmVo.RMRoleList.Split(new char[] { ',' });
                    foreach (string Role in RoleListArray)
                    {
                        if (Role == "RM" || Role == "BM")
                        {
                            ChklistRMBM.Items.FindByText(Role).Selected = true;
                        }
                    }
                    lblMail.Text = rmVo.Email.ToString();
                    lblFax.Text = rmVo.FaxIsd.ToString() + "-" + rmVo.FaxStd.ToString() + "-" + rmVo.Fax.ToString();
                    lblMobile.Text = rmVo.Mobile.ToString();
                    lblRMName.Text = rmVo.FirstName.ToString() + " " + rmVo.MiddleName.ToString() + " " + rmVo.LastName.ToString();
                    lblPhDirect.Text = rmVo.OfficePhoneDirectIsd.ToString() + "-" + rmVo.OfficePhoneDirectStd.ToString() + "-" + rmVo.OfficePhoneDirectNumber.ToString();
                    lblPhExt.Text = rmVo.OfficePhoneExtIsd.ToString() + "-" + rmVo.OfficePhoneExtStd.ToString() + "-" + rmVo.OfficePhoneExtNumber.ToString();
                    lblPhResi.Text = rmVo.ResPhoneIsd.ToString() + "-" + rmVo.ResPhoneStd.ToString() + "-" + rmVo.ResPhoneNumber.ToString();
                    lblCTCValue.Text = rmVo.CTC.ToString() + " per month";
                    if (rmVo.IsExternal == 1)
                    {
                        lblStaffTypeValue.Text = "External";
                    }
                    else
                    {
                        lblStaffTypeValue.Text = "Internal";
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
                FunctionInfo.Add("Method", "ViewRMDetails.ascx:ViewRMDetail()");
                object[] objects = new object[1];
                objects[0] = rmVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //   RMVo newrmVo = (RMVo)Session["rmVo"];
        //    int userId = int.Parse(Session["userId"].ToString());
        //    bool result = false;
        //    result = advisorStaffBo.DeleteRM(newrmVo.RMId, userId);
        //    if (result)
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewRM','none');", true);
        //    }
        //    else
        //    {

        //    }
        //}

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            RMVo newrmVo = null;

            try
            {
                if (Session["CurrentrmVo"] != null)
                {
                    newrmVo = (RMVo)Session["CurrentrmVo"];
                }
                else
                {
                    newrmVo = (RMVo)Session["rmVo"];
                }

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EditRMDetails','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewRMDetails.cs:lnkEdit_Click()");
                object[] objects = new object[1];
                objects[0] = newrmVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{
        //    AdvisorBranchBo advisorBranchBo = null;
        //    bool result = false;
        //    int rmId = 0;
        //    int branchId = 0;
        //    try
        //    {
        //        advisorBranchBo = new AdvisorBranchBo();

        //        foreach (GridViewRow dr in gvRMBranch.Rows)
        //        {
        //            CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
        //            if (checkBox.Checked)
        //            {

        //                //  int rmId = Convert.ToInt32(gvRMBranch.DataKeys[dr.RowIndex].Values["RMId"].ToString());
        //                rmVo = (RMVo)Session["rmVo"];
        //                rmId = rmVo.RMId;
        //                branchId = Convert.ToInt32(gvRMBranch.DataKeys[dr.RowIndex].Values["BranchId"].ToString());

        //                result = advisorBranchBo.UpdateRMBranchAssociation(rmId, branchId);
        //                if (result)
        //                {
        //                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Deleted Successfully..');", true);

        //                }
        //                else
        //                {
        //                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Sorry..');", true);
        //                }
        //            }
        //        }
        //        BindBranchAssociation();
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "ViewRMDetails.ascx.cs:btnDeleteSelected_Click()");

        //        object[] objects = new object[3];
        //        objects[0] = rmVo;
        //        objects[1] = rmId;
        //        objects[2] = branchId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //}

     protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            string url = string.Empty;
            url = Request.UrlReferrer.ToString();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewRM','none');", true);
        }
     protected void gvRMBranch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = new DataTable();
            RadioButton rbtn;
            DataSet ds;
            ds = advisorBranchBo.GetRMBranchAssociation(rmVo.RMId, advisorVo.advisorId, "A");
            dt.Columns.Add("BranchId");
            dt.Columns.Add("Branch Name");
            dt.Columns.Add("Branch Code");
            DataRowView drv = e.Row.DataItem as DataRowView;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (ds.Tables[0].Rows[gvRMBranch.Rows.Count]["ARB_IsMainBranch"].ToString() == "1")
                //{

                //    rbtn = e.Row.FindControl("rbtnMainBranch") as RadioButton;
                //    rbtn.Checked = true;
                //}
                //else
                //{
                    rbtn = e.Row.FindControl("rbtnMainBranch") as RadioButton;
                    //rbtn.Checked = false;
                //}
            }
        }

     public void setBranchList(string IsExternal)
        {
            UserVo rmUserVo = null;
            DataRow drAdvisorBranch;
            DataTable dtAdvisorBranch = new DataTable();
            bool tracker = false;
            try
            {
                rmUserVo = (UserVo)Session["rmUserVo"];
                if (IsExternal == "Y")
                {
                    advisorBranchList = advisorBranchBo.GetAdvisorBranches(advisorVo.advisorId, "Y");
                }
                else if (IsExternal == "N")
                {
                    advisorBranchList = advisorBranchBo.GetAdvisorBranches(advisorVo.advisorId, "N");
                }

                dtAdvisorBranch.Columns.Add("Branch");
                dtAdvisorBranch.Columns.Add("Branch Code");

                if (advisorBranchList != null)
                {
                    for (int i = 0; i < advisorBranchList.Count; i++)
                    {
                        drAdvisorBranch = dtAdvisorBranch.NewRow();
                        advisorBranchVo = new AdvisorBranchVo();
                        advisorBranchVo = advisorBranchList[i];

                        if (associatedBranch.Items.FindByValue(advisorBranchVo.BranchId.ToString()) == null)
                        {
                            //if (tracker)
                            //{
                            if (drAdvisorBranch["Branch"] != null && drAdvisorBranch["Branch Code"] != null)
                            {
                                drAdvisorBranch["Branch"] = advisorBranchVo.BranchName.ToString() + "," + advisorBranchVo.BranchId.ToString();
                                drAdvisorBranch["Branch Code"] = advisorBranchVo.BranchId.ToString();
                                dtAdvisorBranch.Rows.Add(drAdvisorBranch);
                            }

                        }



                    }
                }
                //_commondatasetSource.Tables.Add(dtAdvisorBranch);
                availableBranch.DataSource = dtAdvisorBranch;
                availableBranch.DataTextField = "Branch";
                availableBranch.DataValueField = "Branch Code";
                availableBranch.DataBind();



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
                objects[0] = rmUserVo;
                objects[1] = advisorBranchList;
                objects[2] = advisorBranchVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
    }

   
}