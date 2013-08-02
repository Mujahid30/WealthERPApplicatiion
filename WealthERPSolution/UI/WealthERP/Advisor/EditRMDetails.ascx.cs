﻿using System;
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
using BoCommon;
using VoAdvisorProfiling;

namespace WealthERP.Advisor
{
    public partial class EditRMDetails : System.Web.UI.UserControl 
    {
        AdvisorBranchVo advisorBranchVo = null;
        AdvisorVo advisorVo = new AdvisorVo();
        UserBo userBo = new UserBo();
        int rmId;
        int rmIDRef;
        int userId;
        string Action;
        RMVo rmVo = new RMVo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        UserVo userVo = new UserVo();
        Hashtable htRMInfo = new Hashtable();
        List<AdvisorBranchVo> advisorBranchList = null;

        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        DataSet _commondatasetSource;
        DataSet _commondatasetdestination;
        UserVo uvo = new UserVo();
        int branchHead;
        protected void Page_PreInit(object sender, EventArgs e)
        {

            CheckRMBM.Attributes.Add("onClick", "CheckRMBMRole();");

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // addBranch.Attributes.Add("onclick", "return addbranches('availableBranch','associatedBranch')");
            //deleteBranch.Attributes.Add("onclick", "return deletebranches('associatedBranch','availableBranch')");
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            uvo = (UserVo)Session["userVo"];

            if (Session["CurrentrmVo"] != null)
            {
                rmVo = (RMVo)Session["CurrentrmVo"];
            }
            else
            {
                rmVo = (RMVo)Session["rmVo"];
            }

            if (!Page.IsPostBack)
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Verification", " CheckSubscription();", true);
                this.Action = Request.QueryString[0];
                GetPlanOpsStaffAddStatus(advisorVo.advisorId);
                if (Action == "Edit Profile")
                {
                    SetStaffDetails();
                    SetControlstate(Action);
                    lblHeader.Text = "Edit Staff Details";
                    lnkBtnBack.Visible = false;
                    lnkEdit.Visible = false;

                }
                else
                {
                    SetStaffDetails();
                    SetControlstate(Action);
                    lblHeader.Text = "View Staff Details";
                }
                htRMInfo = advisorStaffBo.CheckRMDependency(rmVo.RMId);
                hndRmCustomerCount.Value = htRMInfo["RMCustomerCount"].ToString();
                hndBMBranchHead.Value = htRMInfo["BMBranchHead"].ToString();
                hdnIsSubscripted.Value = advisorVo.IsISASubscribed.ToString();
                
            }
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];

            lblEmail.CssClass = "FieldName";
            lblISD.CssClass = "FieldName";
            //lblName.CssClass = "FieldName";
            //lblFirst.CssClass = "FieldName";
            //lblMiddle.CssClass = "FieldName";
            //lblLast.CssClass = "FieldName";
            lblStaffCode.CssClass = "FieldName";
            lblPhoneDirectNumber.CssClass = "FieldName";
            lblPhoneNumber.CssClass = "FieldName";
            lblSTD.CssClass = "FieldName";

        }
        //private void GetPlanOpsStaffAddStatus(int adviserId)
        //{
        //    DataSet dsPlanOpsStaffAddStatus = advisorStaffBo.GetPlanOpsStaffAddStatus(adviserId);
        //    if (dsPlanOpsStaffAddStatus.Tables[1].Rows[0]["WP_IsOpsEnabled"].ToString() == "1")
        //    {
        //        if (int.Parse(dsPlanOpsStaffAddStatus.Tables[0].Rows[0]["CountOps"].ToString()) == 1 && rmVo.RMRole == "Ops")
        //        {
        //            hdnIsOpsEnabled.Value = "1";
        //            chkOps.Visible = true;
        //            //chkOps.Checked = true;
        //        }
        //        else if (int.Parse(dsPlanOpsStaffAddStatus.Tables[0].Rows[0]["CountOps"].ToString()) == 0 && rmVo.RMRole != "Ops")
        //        {
        //            hdnIsOpsEnabled.Value = "1";
        //            chkOps.Visible = true;
        //        }
        //        else
        //        {
        //            hdnIsOpsEnabled.Value = "0";
        //            chkOps.Visible = false;
        //          // 
        //        }
        //    }
        //    else
        //    {
        //        chkOps.Visible = false;
        //        hdnIsOpsEnabled.Value = "0";
        //    }
        //}
        //protected void rbtnMainBranch_CheckedChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in gvBranchList.Rows)
        //    {
        //        ((RadioButton)row.FindControl("rbtnMainBranch")).Checked = false;
        //    }
        //    RadioButton rbtn = (RadioButton)sender;
        //    GridViewRow tempRow = (GridViewRow)rbtn.NamingContainer;
        //    ((RadioButton)tempRow.FindControl("rbtnMainBranch")).Checked = true;
        private void GetPlanOpsStaffAddStatus(int adviserId)
        {
            DataSet dsPlanOpsStaffAddStatus = advisorStaffBo.GetPlanOpsStaffAddStatus(adviserId);
            if (dsPlanOpsStaffAddStatus.Tables[0].Rows[0]["WP_IsOpsEnabled"].ToString() == "1" && int.Parse(dsPlanOpsStaffAddStatus.Tables[0].Rows[0]["AS_NoOfBranches"].ToString()) > 1)
            {
                chkOps.Visible = true;
                //lblOr.Visible = true;
                trCKMK.Visible = true;
                hdnIsOpsEnabled.Value = "1";

            }
            else
            {
                hdnIsOpsEnabled.Value = "0";
            }
        }


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
                Action = "Edit Profile";
                SetStaffDetails();
                SetControlstate(Action);
                lnkEdit.Visible = false;
                lnkBtnBack.Visible = false;
                lblHeader.Text = "Edit Staff Details";
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

        public bool chkAvailability()
        {
            bool result = false;
            string id;
            try
            {
                id = txtEmail.Text;
                result = userBo.ChkAvailability(id);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AddRM.ascx:chkAvailability()");


                object[] objects = new object[1];
                objects[0] = result;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public bool Validation()
        {
            bool result = true;
            try
            {
                if (!ChkMailId(txtEmail.Text.ToString()))
                {
                    result = false;
                    // lblEmail.CssClass = "Error";
                }

                //if (txtFirstName.Text.ToString() == "")
                //{
                //    //lblName.CssClass = "Error";
                //    lblFirst.CssClass = "Error";
                //    result = false;
                //}

                //if (txtPhDirectPhoneNumber.Text == "")
                //{
                //    lblPhoneDirectNumber.CssClass = "Error";
                //    result = false;
                //}

                //if (txtPhDirectSTD.Text == " ")
                //{
                //    lblSTD.CssClass = "Error";
                //    result = false;
                //}

                //if (txtPhDirectISD.Text == "")
                //{
                //    lblISD.CssClass = "Error";
                //    result = false;
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

                FunctionInfo.Add("Method", "EditRMDetails.ascx:Validation()");

                object[] objects = new object[1];
                objects[0] = result;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return result;
        }
        public void SetControlstate(String Action)
        {
            if (Action == "Edit Profile")
            {
                txtFirstName.Enabled = true;
                txtLastName.Enabled = true;
                txtMiddleName.Enabled = true;
                txtStaffCode.Enabled = true;
                txtEmail.Enabled = true;
                txtExtSTD.Enabled = true;
                txtFaxISD.Enabled = true;
                txtFaxNumber.Enabled = true;
                txtFaxSTD.Enabled = true;
                txtMobileNumber.Enabled = true;
                txtPhDirectISD.Enabled = true;
                txtPhDirectPhoneNumber.Enabled = true;
                txtPhDirectSTD.Enabled = true;
                txtPhExtISD.Enabled = true;
                txtPhExtPhoneNumber.Enabled = true;
                txtPhResiISD.Enabled = true;
                txtPhResiPhoneNumber.Enabled = true;
                txtResiSTD.Enabled = true;
                txtCTC.Enabled = true;
                ChklistRMBM.Enabled = true;
                chkExternalStaff.Enabled = true;
                chkOps.Enabled = true;
                chkExternalStaff.Enabled = true;
                ChklistRMBM.Enabled = true;
                availableBranch.Enabled = true;
                associatedBranch.Enabled = true;
                CheckListCKMK.Enabled = true;
            }
            else
            {
                txtFirstName.Enabled = false;
                txtLastName.Enabled = false;
                txtMiddleName.Enabled = false;
                txtStaffCode.Enabled = false;
                txtEmail.Enabled = false;
                txtExtSTD.Enabled = false;
                txtFaxISD.Enabled = false;
                txtFaxNumber.Enabled = false;
                txtFaxSTD.Enabled = false;
                txtMobileNumber.Enabled = false;
                txtPhDirectISD.Enabled = false;
                txtPhDirectPhoneNumber.Enabled = false;
                txtPhDirectSTD.Enabled = false;
                txtPhExtISD.Enabled = false;
                txtPhExtPhoneNumber.Enabled = false;
                txtPhResiISD.Enabled = false;
                txtPhResiPhoneNumber.Enabled = false;
                txtResiSTD.Enabled = false;
                txtCTC.Enabled = false;
                ChklistRMBM.Enabled = false;
                chkExternalStaff.Enabled = false;
                chkOps.Enabled = false;
                chkExternalStaff.Enabled = false;
                ChklistRMBM.Enabled = false;
                availableBranch.Enabled = false;
                associatedBranch.Enabled = false;
                CheckListCKMK.Enabled = false;
            }

        }
        //public void ViewRMDetail()
        //{

        //    try
        //    {
        //        RMVo rmVo = new RMVo();
        //        if (Session["CurrentrmVo"] != null)
        //        {
        //            rmVo = (RMVo)Session["CurrentrmVo"];
        //        }
        //        else
        //        {
        //            rmVo = (RMVo)Session["rmVo"];
        //        }
        //        txtFirstName.Text = rmVo.FirstName.ToString();
        //        txtFirstName.Enabled = false;
        //        txtLastName.Text = rmVo.LastName.ToString();
        //        txtLastName.Enabled = false;
        //        txtMiddleName.Text = rmVo.MiddleName.ToString();
        //        txtMiddleName.Enabled = false;
        //        if (!string.IsNullOrEmpty(rmVo.StaffCode))
        //            txtStaffCode.Text = rmVo.StaffCode.ToString();
        //        else
        //            txtStaffCode.Text = string.Empty;
        //        txtStaffCode.Enabled = false;
        //        txtEmail.Text = rmVo.Email.ToString();
        //        txtEmail.Enabled = false;
        //        txtExtSTD.Text = rmVo.OfficePhoneExtStd.ToString();
        //        txtExtSTD.Enabled = false;
        //        txtFaxISD.Text = rmVo.FaxIsd.ToString();
        //        txtFaxISD.Enabled = false;
        //        txtFaxNumber.Text = rmVo.Fax.ToString();
        //        txtFaxNumber.Enabled = false;
        //        txtFaxSTD.Text = rmVo.FaxStd.ToString();
        //        txtFaxSTD.Enabled = false;
        //        txtMobileNumber.Text = rmVo.Mobile.ToString();
        //        txtMobileNumber.Enabled = false;
        //        txtPhDirectISD.Text = rmVo.OfficePhoneDirectIsd.ToString();
        //        txtPhDirectISD.Enabled = false;
        //        txtPhDirectPhoneNumber.Text = rmVo.OfficePhoneDirectNumber.ToString();
        //        txtPhDirectPhoneNumber.Enabled = false;
        //        txtPhDirectSTD.Text = rmVo.OfficePhoneDirectStd.ToString();
        //        txtPhDirectSTD.Enabled = false;
        //        txtPhExtISD.Text = rmVo.OfficePhoneExtIsd.ToString();
        //        txtPhExtISD.Enabled = false;
        //        txtPhExtPhoneNumber.Text = rmVo.OfficePhoneExtNumber.ToString();
        //        txtPhExtPhoneNumber.Enabled = false;
        //        txtPhResiISD.Text = rmVo.ResPhoneIsd.ToString();
        //        txtPhResiISD.Enabled = false;
        //        txtPhResiPhoneNumber.Text = rmVo.ResPhoneNumber.ToString();
        //        txtPhResiPhoneNumber.Enabled = false;
        //        txtResiSTD.Text = rmVo.ResPhoneStd.ToString();
        //        txtResiSTD.Enabled = false;
        //        txtCTC.Text = rmVo.CTC.ToString();
        //        txtCTC.Enabled = false;
        //        string[] RoleListArray = rmVo.RMRoleList.Split(new char[] { ',' });
        //        foreach (string Role in RoleListArray)
        //        {
        //            if ((Role == "RM" || Role == "BM") || (Role == "Research"))
        //            {
        //                ChklistRMBM.Items.FindByText(Role).Selected = true;
        //                ChklistRMBM.Enabled = false;
        //            }

        //        }



        //        if (rmVo.IsExternal == 1)
        //            chkExternalStaff.Checked = true;
        //        else
        //            chkExternalStaff.Checked = false;
        //        chkExternalStaff.Enabled = false;
        //        BindBranchAssociation();
        //        if (rmVo.RMRole == "Ops")
        //        {
        //            chkOps.Checked = true;
        //            chkOps.Enabled = false;
        //            chkExternalStaff.Enabled = false;
        //            ChklistRMBM.Enabled = false;
        //            availableBranch.Enabled = false;
        //            associatedBranch.Enabled = false;
        //        }
        //        else
        //        {
        //            chkOps.Enabled = false;
        //            chkExternalStaff.Enabled = false;
        //            ChklistRMBM.Enabled = false;
        //            availableBranch.Enabled = false;
        //            associatedBranch.Enabled = false;
        //        }
        //        Session["rmId"] = rmVo.RMId;
        //        rmIDRef = rmVo.RMId;
        //        Session["userId"] = rmVo.UserId;

        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "ViewRMDetails.ascx:ViewRMDetail()");
        //        object[] objects = new object[1];
        //        objects[0] = rmVo;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }

        //}

        public bool ChkMailId(string email)
        {
            bool bResult = false;
            try
            {
                if (email == null)
                {
                    bResult = false;
                }
                int nFirstAT = email.IndexOf('@');
                int nLastAT = email.LastIndexOf('@');

                if ((nFirstAT > 0) && (nLastAT == nFirstAT) && (nFirstAT < (email.Length - 1)))
                {
                    bResult = true;
                }
                else
                {
                    bResult = false;
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

                FunctionInfo.Add("Method", "EditRMDetails.ascx:ChkMailId()");

                object[] objects = new object[1];
                objects[0] = email;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

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
                    //gvBranchList.DataSource = dt;

                    //gvBranchList.DataBind();
                    //gvBranchList.Visible = true;

                    // Show binded contents in List box
                    associatedBranch.DataSource = dtList;
                    associatedBranch.DataTextField = "Branch";
                    associatedBranch.DataValueField = "BranchId";
                    associatedBranch.DataBind();

                    if (chkExternalStaff.Checked == true)
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
                    if (chkExternalStaff.Checked == true)
                    {
                        setBranchList("Y");
                    }
                    else
                    {
                        setBranchList("N");
                    }
                    //gvBranchList.Visible = false;
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

                object[] objects = new object[2];
                objects[0] = rmVo;
                objects[1] = advisorVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
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
        public void SetStaffDetails()
        {
            RMVo rmVo = new RMVo();
            if (Session["CurrentrmVo"] != null)
            {
                rmVo = (RMVo)Session["CurrentrmVo"];
            }
            else
            {
                rmVo = (RMVo)Session["rmVo"];
            }
            txtFirstName.Text = rmVo.FirstName.ToString();
            txtLastName.Text = rmVo.LastName.ToString();
            txtMiddleName.Text = rmVo.MiddleName.ToString();
            if (!string.IsNullOrEmpty(rmVo.StaffCode))
                txtStaffCode.Text = rmVo.StaffCode.ToString();
            else
                txtStaffCode.Text = string.Empty;
            txtEmail.Text = rmVo.Email.ToString();
            txtExtSTD.Text = rmVo.OfficePhoneExtStd.ToString();
            txtFaxISD.Text = rmVo.FaxIsd.ToString();
            txtFaxNumber.Text = rmVo.Fax.ToString();
            txtFaxSTD.Text = rmVo.FaxStd.ToString();
            txtMobileNumber.Text = rmVo.Mobile.ToString();
            txtPhDirectISD.Text = rmVo.OfficePhoneDirectIsd.ToString();
            txtPhDirectPhoneNumber.Text = rmVo.OfficePhoneDirectNumber.ToString();
            txtPhDirectSTD.Text = rmVo.OfficePhoneDirectStd.ToString();
            txtPhExtISD.Text = rmVo.OfficePhoneExtIsd.ToString();
            txtPhExtPhoneNumber.Text = rmVo.OfficePhoneExtNumber.ToString();
            txtPhResiISD.Text = rmVo.ResPhoneIsd.ToString();
            txtPhResiPhoneNumber.Text = rmVo.ResPhoneNumber.ToString();
            txtResiSTD.Text = rmVo.ResPhoneStd.ToString();
            txtCTC.Text = rmVo.CTC.ToString();
            string[] RoleListArray = rmVo.RMRoleList.Split(new char[] { ',' });
            foreach (string Role in RoleListArray)
            {
                if ((Role == "RM" || Role == "BM") || (Role == "Research"))
                {
                    ChklistRMBM.Items.FindByText(Role).Selected = true;
                }

            }

            if (rmVo.IsExternal == 1)
                chkExternalStaff.Checked = true;
            else
                chkExternalStaff.Checked = false;

            BindBranchAssociation();
            if (rmVo.RMRole == "Ops")
            {
                chkOps.Checked = true;

                    //trCKMK.Visible = true;
                    bool Value;
                    foreach (ListItem Items in CheckListCKMK.Items)
                    {

                        Value = userBo.CheckCheckerMaker(rmVo.UserId, Int16.Parse(Items.Value.ToString()));
                        if (Value == true)
                        {

                            CheckListCKMK.Items.FindByText(Items.Text).Selected = true;
                        }
                        else
                        {

                        }
                    }
            }
            else
            {
                chkOps.Visible = false;
                trCKMK.Visible = false;

            }
            Session["rmId"] = rmVo.RMId;
            rmIDRef = rmVo.RMId;
            Session["userId"] = rmVo.UserId;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserBo userBo = new UserBo();
            UserVo userVo = new UserVo();
            UserVo userVo2 = new UserVo();
            bool branchDeletion = true;
            Random id = new Random();
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorBranchBo advisorBrBo = new AdvisorBranchBo();

            bool blUpdate = true;
            string association = "";
            try
            {
                if (Validation())
                {
                    userId = int.Parse(Session["userId"].ToString());
                    userVo = userBo.GetUserDetails(rmVo.UserId);
                    userVo.Email = txtEmail.Text.ToString();
                    userVo.FirstName = txtFirstName.Text.ToString();
                    userVo.LastName = txtLastName.Text.ToString();
                    userVo.MiddleName = txtMiddleName.Text.ToString();



                    rmVo.RMId = int.Parse(Session["rmId"].ToString());
                    rmVo.Email = txtEmail.Text.ToString();
                    if (txtFaxNumber.Text == string.Empty || txtFaxNumber.Text == "")
                        rmVo.Fax = 0;
                    else
                        rmVo.Fax = int.Parse(txtFaxNumber.Text.ToString());
                    if (txtFaxISD.Text == string.Empty || txtFaxISD.Text == "")
                        rmVo.FaxIsd = 0;
                    else
                        rmVo.FaxIsd = int.Parse(txtFaxISD.Text.ToString());
                    if (txtExtSTD.Text == string.Empty || txtExtSTD.Text == "")
                        rmVo.FaxStd = 0;
                    else
                        rmVo.FaxStd = int.Parse(txtExtSTD.Text.ToString());
                    rmVo.FirstName = txtFirstName.Text.ToString();
                    rmVo.LastName = txtLastName.Text.ToString();
                    rmVo.MiddleName = txtMiddleName.Text.ToString();
                    if (!string.IsNullOrEmpty(txtStaffCode.Text))
                        rmVo.StaffCode = txtStaffCode.Text.ToString();
                    else
                        rmVo.StaffCode = string.Empty;

                    if (txtMobileNumber.Text == string.Empty || txtMobileNumber.Text == "")
                        rmVo.Mobile = 0;
                    else
                        rmVo.Mobile = Convert.ToInt64(txtMobileNumber.Text.ToString());
                    if (txtPhDirectISD.Text == string.Empty || txtPhDirectISD.Text == "")
                        rmVo.OfficePhoneDirectIsd = 0;
                    else
                        rmVo.OfficePhoneDirectIsd = int.Parse(txtPhDirectISD.Text.ToString());
                    if (txtPhDirectPhoneNumber.Text == string.Empty || txtPhDirectPhoneNumber.Text == "")
                        rmVo.OfficePhoneDirectNumber = 0;
                    else
                        rmVo.OfficePhoneDirectNumber = int.Parse(txtPhDirectPhoneNumber.Text.ToString());
                    if (txtPhDirectSTD.Text == string.Empty || txtPhDirectSTD.Text == "")
                        rmVo.OfficePhoneDirectStd = 0;
                    else
                        rmVo.OfficePhoneDirectStd = int.Parse(txtPhDirectSTD.Text.ToString());
                    if (txtPhExtISD.Text == string.Empty || txtPhExtISD.Text == "")
                        rmVo.OfficePhoneExtIsd = 0;
                    else
                        rmVo.OfficePhoneExtIsd = int.Parse(txtPhExtISD.Text.ToString());
                    if (txtPhExtPhoneNumber.Text == string.Empty || txtPhExtPhoneNumber.Text == "")
                        rmVo.OfficePhoneExtNumber = 0;
                    else
                        rmVo.OfficePhoneExtNumber = int.Parse(txtPhExtPhoneNumber.Text.ToString());
                    if (txtExtSTD.Text == string.Empty || txtExtSTD.Text == "")
                        rmVo.OfficePhoneExtStd = 0;
                    else
                        rmVo.OfficePhoneExtStd = int.Parse(txtExtSTD.Text.ToString());
                    if (txtPhResiISD.Text == string.Empty || txtPhResiISD.Text == "")
                        rmVo.ResPhoneIsd = 0;
                    else
                        rmVo.ResPhoneIsd = int.Parse(txtPhResiISD.Text.ToString());
                    if (txtPhResiPhoneNumber.Text == string.Empty || txtPhResiPhoneNumber.Text == "")
                        rmVo.ResPhoneNumber = 0;
                    else
                        rmVo.ResPhoneNumber = int.Parse(txtPhResiPhoneNumber.Text.ToString());
                    if (txtResiSTD.Text == string.Empty || txtResiSTD.Text == "")
                        rmVo.ResPhoneStd = 0;
                    else
                        rmVo.ResPhoneStd = int.Parse(txtResiSTD.Text.ToString());
                    if (txtCTC.Text == string.Empty || txtCTC.Text == "")
                        rmVo.CTC = 0;
                    else
                        rmVo.CTC = Convert.ToDouble(txtCTC.Text.Trim());

                    if (chkExternalStaff.Checked)
                    {
                        // Check for Internal Associations, if they exist do not update.
                        if (advisorBrBo.CheckInternalBranchAssociations(rmVo.RMId))
                        {
                            association = "internal";
                            blUpdate = false;
                        }
                        else
                        {
                            rmVo.IsExternal = 1;
                        }
                    }
                    else
                    {
                        // Check for External Associations, if they exist do not update.
                        if (advisorBrBo.CheckExternalBranchAssociations(rmVo.RMId))
                        {
                            association = "external";
                            blUpdate = false;
                        }
                        else
                        {
                            rmVo.IsExternal = 0;
                        }
                    }

                    //Update the User details in the User Table
                    userBo.UpdateUser(userVo);
                    //Update the RM details in the AdvisorRM Table
                    advisorStaffBo.UpdateStaff(rmVo);





                    //*************Role Association Creation and deletion************************

                        bool RMBMResearchRole = false;
                        string[] RoleListArray = rmVo.RMRoleList.Split(new char[] { ',' });
                        foreach (string Role in RoleListArray)
                        {
                            // Create Role Association for Ops
                            if (chkOps.Checked == true)
                            {
                                userBo.CreateRoleAssociation(rmVo.UserId, 1004);
                            }
                            else if (chkOps.Checked == false)
                            {
                                userBo.DeleteRoleAssociation(rmVo.UserId, 1004);
                            }

                        }
                        foreach (ListItem Items in ChklistRMBM.Items)
                        {

                            if (Items.Text == "RM")
                            {
                                foreach (string Role in RoleListArray)
                                {
                                    if (Role == "RM")
                                    {
                                        RMBMResearchRole = true;
                                    }
                                }
                                // Create Role Association for RM
                                if (RMBMResearchRole == false && Items.Selected == true)
                                {
                                    userBo.CreateRoleAssociation(rmVo.UserId, Int16.Parse(Items.Value.ToString()));
                                }
                                else if (RMBMResearchRole == true && Items.Selected == false)
                                {

                                    userBo.DeleteRoleAssociation(rmVo.UserId, Int16.Parse(Items.Value.ToString()));

                                }
                                RMBMResearchRole = false;
                            }
                            else if (Items.Text == "BM")
                            {
                                foreach (string Role in RoleListArray)
                                {
                                    if (Role == "BM")
                                    {
                                        RMBMResearchRole = true;
                                    }
                                }
                                // Create Role Association for BM
                                if (RMBMResearchRole == false && Items.Selected == true)
                                {
                                    userBo.CreateRoleAssociation(rmVo.UserId, Int16.Parse(Items.Value.ToString()));
                                }
                                else if (RMBMResearchRole == true && Items.Selected == false)
                                {
                                    branchHead = advisorBranchBo.CheckBranchHead(rmVo.RMId, 0);
                                    if (branchHead == 0)
                                        userBo.DeleteRoleAssociation(rmVo.UserId, Int16.Parse(Items.Value.ToString()));
                                    else
                                    {
                                        ChklistRMBM.Items[1].Selected = true;
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "alert('BM Role can not be removed as this staff is a Branch Head');", true);
                                        return;
                                    }
                                }

                                RMBMResearchRole = false;

                            }
                            else if (Items.Text == "Research")
                            {
                                foreach (string Role in RoleListArray)
                                {
                                    if (Role == "Research")
                                    {
                                        RMBMResearchRole = true;
                                    }
                                    // Create Role Association for Research
                                    if (RMBMResearchRole == false && Items.Selected == true)
                                    {
                                        userBo.CreateRoleAssociation(rmVo.UserId, Int16.Parse(Items.Value.ToString()));
                                       
                                    }
                                    else if (RMBMResearchRole == true && Items.Selected == false)
                                    {
                                        userBo.DeleteRoleAssociation(rmVo.UserId, Int16.Parse(Items.Value.ToString()));
                                    }

                                    RMBMResearchRole = false;
                                }
                            }
                        }
                   // }


                    //*************Role Association Creation and deletion************************   



                    //**************Branch Association Creation and deletion*********************


                    //string hdnExistingString = hdnExistingBranches.Value.ToString();
                    string hdnSelectedString = hdnSelectedBranches.Value.ToString();
                    //string[] existingBranchesList = hdnExistingString.Split(',');
                    string[] selectedBranchesList = hdnSelectedString.Split(',');
                    DataTable dtSelectedBranch = new DataTable();
                    dtSelectedBranch.Columns.Add("Branch");
                    dtSelectedBranch.Columns.Add("BranchId");
                    DataTable dtAssociated = new DataTable();
                    List<int> deletedBRList = new List<int>();
                    List<int> addedBRList = new List<int>();
                    dtAssociated = (DataTable)Session["AssociatedBranch"];
                    Session.Remove("AssociatedBranch");
                    if (dtAssociated != null)
                    {
                        foreach (string str in selectedBranchesList)
                        {
                            if (str != "")
                            {

                                dtAssociated.DefaultView.Sort = "BranchId";
                                int i = dtAssociated.DefaultView.Find(str);
                                if (i == (0 - 1))
                                {
                                    addedBRList.Add(int.Parse(str));
                                }
                            }
                            DataRow dr = dtSelectedBranch.NewRow();
                            dr["Branch"] = str;
                            dr["BranchId"] = str;
                            dtSelectedBranch.Rows.Add(dr);
                        }



                        foreach (DataRow dr in dtAssociated.Rows)
                        {
                            dtSelectedBranch.DefaultView.Sort = "BranchId";
                            int j = dtSelectedBranch.DefaultView.Find(dr["BranchId"].ToString());
                            if (j == (0 - 1))
                            {
                                if (dr["BranchId"].ToString() != "")
                                    deletedBRList.Add(int.Parse(dr["BranchId"].ToString()));
                            }
                        }

                        for (int i = 0; i < addedBRList.Count; i++)
                        {
                            advisorBranchBo.CreateRMBranchAssociation(rmVo.RMId, addedBRList[i], advisorVo.advisorId, advisorVo.advisorId);

                        }
                        for (int i = 0; i < deletedBRList.Count; i++)
                        {
                            bool IsBranchDependency = false;
                            IsBranchDependency = advisorBranchBo.CheckRMBranchDependency(rmVo.RMId, deletedBRList[i]);
                            if (IsBranchDependency == true)
                            {
                                branchDeletion = false;
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Sorry... You need to delete your internal associations first');", true);

                            }
                            else
                                advisorBranchBo.DeleteBranchAssociation(rmVo.RMId, deletedBRList[i]);


                        }

                        if (branchDeletion == false)
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewRMDetails','none');", true);

                        }
                    }
                    else
                    {
                        foreach (string str in selectedBranchesList)
                        {
                            if (str != "")
                            {
                                advisorBranchBo.CreateRMBranchAssociation(rmVo.RMId, int.Parse(str), advisorVo.advisorId, advisorVo.advisorId);

                            }

                        }


                    }
                    //advisorBranchBo.DeleteRMBranchAssociation1(rmIDRef);

                    //**************Branch Association Creation and deletion*********************



                    if (blUpdate)
                    {



                        btnSave.Enabled = false;
                        if (Session["FromAdvisorView"] != null)
                        {
                            if (Session["FromAdvisorView"].ToString() == "FromAdvView")
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewRM','none');", true);
                            }
                            else
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewRMDetails','none');", true);
                            }
                            Session.Remove("FromAdvisorView");
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewRM','none');", true);
                        }

                    }
                    else
                    {
                        if (association == "internal")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Sorry... You need to delete your internal associations first');", true);
                        }
                        else if (association == "external")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Sorry... You need to delete your external associations first');", true);
                        }
                    }
                    if (uvo.UserId == userId)
                    {
                        userVo = userBo.GetUserDetails(uvo.UserId);
                        Session["UserVo"] = userVo;
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);
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

                FunctionInfo.Add("Method", "EditRMDetails.ascx:btnSave_Click()");

                object[] objects = new object[6];

                objects[0] = advisorVo;
                objects[1] = userVo;
                objects[2] = userBo;
                objects[3] = userId;
                objects[4] = rmId;
                objects[5] = userVo2;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);

        }

        private void DeleteRM()
        {
            int userId = 0; ;

            bool result = false;
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
                userId = int.Parse(Session["userId"].ToString());

                if (!string.IsNullOrEmpty(rmVo.BranchList.ToString()))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Sorry... You need to delete branch associations first');", true);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewRMDetails','none');", true);

                }
                else if (int.Parse(hndRmCustomerCount.Value.ToString()) > 0 || int.Parse(hndBMBranchHead.Value.ToString()) > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Sorry... You need to delete your internal associations first');", true);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewRMDetails','none');", true);

                }
                else if (string.IsNullOrEmpty(rmVo.BranchList.ToString().Trim()))
                {
                    result = advisorStaffBo.DeleteRM(rmVo.RMId, userId);
                }
                if (result)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewRM','none');", true);
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

                FunctionInfo.Add("Method", "EditRMDetails.ascx.cs:btnDelete_Click()");

                object[] objects = new object[2];
                objects[0] = rmVo;
                objects[1] = userId;
                objects[2] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewRM','none');", true);
        }

        protected void hiddenDelete_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                DeleteRM();
            }

        }

        //protected void btnSaveBranchAssociation_Click(object sender, EventArgs e)
        //{

        //    if (gvBranchList.Rows.Count != 0)
        //    {

        //        foreach (GridViewRow gvr in this.gvBranchList.Rows)
        //        {

        //              int  branchId = int.Parse(gvBranchList.DataKeys[gvr.RowIndex].Value.ToString());
        //                //if (((RadioButton)gvr.FindControl("rbtnMainBranch")).Checked == true)
        //                //{
        //                //    if (advisorBranchBo.UpdateAssociateBranch(rmVo.RMId, branchId, 1, userVo.UserId))
        //                //    {
        //                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert(' Association is done successfully');", true);
        //                //    }
        //                //    else
        //                //    {
        //                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Sorry.. Association is not done');", true);
        //                //    }
        //                //}
        //                //else
        //                //{
        //                    if (advisorBranchBo.UpdateAssociateBranch(rmVo.RMId, branchId, 0, userVo.UserId))
        //                    {
        //                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert(' Association is done successfully');", true);
        //                    }
        //                    else
        //                    {
        //                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Sorry.. Association is not done');", true);
        //                    }
        //                //}



        //            }

        //    }
        //}

        //protected void gvBranchList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //  DataTable   dt = new DataTable();
        //  //RadioButton rbtn;
        //  DataSet ds;
        //  ds = advisorBranchBo.GetRMBranchAssociation(rmVo.RMId, advisorVo.advisorId, "A");
        //    dt.Columns.Add("BranchId");
        //    dt.Columns.Add("Branch Name");
        //    dt.Columns.Add("Branch Code");
        //    //DataRowView drv = e.Row.DataItem as DataRowView;
        //    //if (e.Row.RowType == DataControlRowType.DataRow)
        //    //{
        //    //    if (ds.Tables[0].Rows[gvBranchList.Rows.Count]["ARB_IsMainBranch"].ToString() == "1")
        //    //    {

        //    //        rbtn = e.Row.FindControl("rbtnMainBranch") as RadioButton;
        //    //        rbtn.Checked = true;
        //    //    }
        //    //    else
        //    //    {
        //    //        rbtn = e.Row.FindControl("rbtnMainBranch") as RadioButton;
        //    //        rbtn.Checked = false;
        //    //    }
        //    //}
        //}

        //protected void btnDeleteBranchAssociation_Click(object sender, EventArgs e)
        //{
        //    DeleteBranchAssociation();
        //}

        //private void DeleteBranchAssociation()
        //{
        //    AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        //    bool result = false;
        //    int rmId = 0;
        //    int branchId = 0;
        //    int count = 0;
        //    DataSet ds;
        //    try
        //    {
        //        ds = advisorBranchBo.GetRMBranchAssociation(rmVo.RMId, advisorVo.advisorId, "A");
        //        foreach (GridViewRow dr in gvBranchList.Rows)
        //        {
        //            CheckBox checkBox = (CheckBox)dr.FindControl("chkId");
        //            if (checkBox.Checked)
        //            {
        //                if (ds.Tables[0].Rows[dr.RowIndex]["ARB_IsMainBranch"].ToString() == "1")
        //                {
        //                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Sorry.You can not delete the Main branch.');", true);
        //                }
        //                else
        //                {
        //                    rmId = Convert.ToInt32(gvBranchList.DataKeys[dr.RowIndex].Values["RMId"].ToString());
        //                    branchId = Convert.ToInt32(gvBranchList.DataKeys[dr.RowIndex].Values["BranchId"].ToString());

        //                    //result = advisorBranchBo.UpdateRMBranchAssociation(rmId, branchId);
        //                    if (result)
        //                    {
        //                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Deleted Successfully..');", true);

        //                    }
        //                    else
        //                    {
        //                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Sorry..');", true);
        //                    }
        //                    count = count + 1;
        //                }
        //            }
        //        }

        //        if (count == 0)
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Please select the branch..');", true);
        //        }
        //        BindBranchAssociation();
        //    }
        //    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewBranchAssociation','none');", true);
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "ViewBranchAssociation.ascx.cs:btnDeleteSelected_Click()");
        //        object[] objects = new object[3];
        //        objects[0] = rmId;
        //        objects[1] = branchId;
        //        objects[2] = result;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        protected void chkExternalStaff_CheckedChanged(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Verification", " CheckSubscription();", true);
        }

        public void ListBoxIteration(int selectedBranch)
        {

            Int16 m = 1;
            advisorBranchBo.UpdateRMBranchAssociation(rmIDRef, selectedBranch, userVo.UserId, m);

        }

        protected void HiddenField1_ValueChanged(object sender, EventArgs e)
        {

        }

        protected void ChklistRMBM_DataBound(object sender, EventArgs e)
        {
            CheckBoxList chklist = (CheckBoxList)sender;

            foreach (ListItem lstitem in chklist.Items)
            {
                lstitem.Attributes.Add("onclick", "CheckRMBMRole(this);");
            }

        }

    }
}
