using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using VoAdvisorProfiling;
using VoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using BoCommon;
using System.Configuration;

namespace WealthERP.Advisor
{
    public partial class ViewBranchDetails : System.Web.UI.UserControl
    {
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
        RMVo rmVo = new RMVo();
        UserVo userVo = new UserVo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        string path;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                advisorBranchVo = (AdvisorBranchVo)Session["advisorBranchVo"];
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                if (advisorBranchVo.BranchId != 0)
                {
                    SetFields(1);
                    setBranchDetail();
                    LoadTerminalId();
                    BindCommnGridView();
                }
                else
                {
                    SetFields(0);
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
                FunctionInfo.Add("Method", "ViewBranchDetails.ascx.cs:Page_Load()");
                object[] objects = new object[3];
                objects[0] = advisorBranchVo;
                objects[1] = userVo;
                objects[2] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void SetFields(int Flag)
        {
            if (Flag == 1)
            {
                lblBranchCode.Visible = true;
                lblBranchName.Visible = true;
                lblCity.Visible = true;
                lblCountry.Visible = true;
                lblEmail.Visible = true;
                lblFax.Visible = true;
                lblHead.Visible = true;
                lblHeadName.Visible = true;
                lblLine1.Visible = true;
                lblLineone.Visible = true;
                lblLineThree.Visible = true;
                lblLinetwo.Visible = true;
                lblMail.Visible = true;
                lblPhone1.Visible = true;
                lblPhone2.Visible = true;
                lblPhoneNumber.Visible = true;
                lblPin.Visible = true;
                lblPinCode.Visible = true;
                lblState.Visible = true;
                LinkButton1.Visible = true;
                lblBCity.Visible = true;
                lblBCountry.Visible = true;
                lblBFax.Visible = true;
                lblBPH2.Visible = true;
                Label2.Visible = true;
                lblBState.Visible = true;
                Label5.Visible = true;
                lblBCode.Visible = true;
                lblBName.Visible = true;
                lblLine3.Visible = true;
                lblLine2.Visible = true;
                lblLineThree.Visible = true;
                lblLinetwo.Visible = true;
                lblBranchHeadMobile.Visible = true;
                lblBranchHeadMobileNumber.Visible = true;
            }
            else
            {


                lblBranchCode.Visible = true;
                lblBranchCode.Text = "You dont have a Branch..!";
                lblBranchCode.CssClass = "FieldName";
                lblBranchName.Visible = false;
                lblCity.Visible = false;
                lblCountry.Visible = false;
                lblEmail.Visible = false;
                lblFax.Visible = false;
                lblHead.Visible = false;
                lblHeadName.Visible = false;
                lblLine1.Visible = false;
                lblLineone.Visible = false;
                lblLineThree.Visible = false;
                lblLinetwo.Visible = false;
                lblMail.Visible = false;
                lblPhone1.Visible = false;
                lblPhone2.Visible = false;
                lblPhoneNumber.Visible = false;
                lblPin.Visible = false;
                lblPinCode.Visible = false;
                lblState.Visible = false;
                LinkButton1.Visible = false;
                lblBCity.Visible = false;
                lblBCountry.Visible = false;
                lblBFax.Visible = false;
                lblBPH2.Visible = false;
                Label2.Visible = false;
                lblBState.Visible = false;
                Label5.Visible = false;
                lblBCode.Visible = false;
                lblBName.Visible = false;
                lblLine2.Visible = false;
                lblLine3.Visible = false;
                lblLineThree.Visible = false;
                lblLinetwo.Visible = false;
                lblBranchHeadMobileNumber.Visible = false;
                lblBranchHeadMobile.Visible = false;
            }
            if (Session["FromAdvisorView"] != null)
            {
                if (Session["FromAdvisorView"].ToString() == "FromAdvView")
                {
                    lnkBtnBack.Visible = true;
                }
                else
                {
                    lnkBtnBack.Visible = false;
                }
                //Session.Remove("FromAdvisorView");
            }
            else
            {
                lnkBtnBack.Visible = false;
            }
        }

        private void LoadTerminalId()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                advisorBranchVo = (AdvisorBranchVo)Session["advisorBranchVo"];

                if ((ds = advisorBranchBo.GetBranchTerminals(advisorBranchVo.BranchId)) != null)
                {

                    dt.Columns.Add("Terminal Id");
                    DataRow dr;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        dr["Terminal Id"] = ds.Tables[0].Rows[i]["AT_TerminalId"].ToString();
                        dt.Rows.Add(dr);
                    }
                    gvTerminalList.DataSource = dt;
                    gvTerminalList.DataBind();
                    gvTerminalList.Visible = true;
                }
                else
                {
                    gvTerminalList.DataSource = null;
                    gvTerminalList.Visible = false;
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
                FunctionInfo.Add("Method", "ViewBranchDetails.ascx.cs:LoadTerminalId()");
                object[] objects = new object[2];
                objects[0] = advisorBranchVo;
                objects[1] = ds;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void setBranchDetail()
        {
            try
            {
                RMVo temp = new RMVo();


                if (advisorBranchVo.BranchId != 0)
                {
                    lblBranchCode.Text = advisorBranchVo.BranchCode.ToString();
                    lblBranchName.Text = advisorBranchVo.BranchName.ToString();
                    lblBranchType.Text = advisorBranchVo.BranchType;
                    lblAssociateCategory.Text = advisorBranchVo.AssociateCategory;
                    lblCity.Text = advisorBranchVo.City.ToString();
                    lblCountry.Text = advisorBranchVo.Country.ToString();
                    lblMail.Text = advisorBranchVo.Email.ToString();
                    lblFax.Text = advisorBranchVo.FaxIsd.ToString() + "-" + advisorBranchVo.FaxStd.ToString() + "-" + advisorBranchVo.Fax.ToString();
                    temp = advisorStaffBo.GetAdvisorStaff(advisorStaffBo.GetUserId(advisorBranchVo.BranchHeadId));
                    lblHead.Text = temp.FirstName + " " + temp.MiddleName + " " + temp.LastName;
                    lblLineone.Text = advisorBranchVo.AddressLine1.ToString();
                    lblLinetwo.Text = advisorBranchVo.AddressLine2.ToString();
                    lblLineThree.Text = advisorBranchVo.AddressLine3.ToString();
                    lblPhone1.Text = advisorBranchVo.Phone1Isd.ToString() + "-" + advisorBranchVo.Phone1Std.ToString() + "-" + advisorBranchVo.Phone1Number.ToString();
                    lblPhone2.Text = advisorBranchVo.Phone2Isd.ToString() + "-" + advisorBranchVo.Phone2Std.ToString() + "-" + advisorBranchVo.Phone2Number.ToString();
                    lblPin.Text = advisorBranchVo.PinCode.ToString();
                    if (advisorBranchVo.State == "")
                    {
                        lblState.Text = "";
                    }
                    else
                        lblState.Text = XMLBo.GetStateName(path, advisorBranchVo.State.ToString());
                    lblBranchHeadMobile.Text = temp.Mobile.ToString();
                    if (advisorBranchVo.BranchType != "Associate")
                    {
                        CommSharingStructureHdr.Visible = false;
                        trAssocCategory.Visible = false;
                    }
                    else
                    {
                        CommSharingStructureHdr.Visible = true;
                        trAssocCategory.Visible = true;
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
                FunctionInfo.Add("Method", "ViewBranchDetails.ascx:setBranchDetail()");
                object[] objects = new object[1];
                objects[0] = advisorBranchVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                foreach (GridViewRow dr in gvTerminalList.Rows)
                {

                    if (((CheckBox)dr.FindControl("chkBx")).Checked == true)
                    {
                        i = i + 1;
                    }
                }
                if (i == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select terminal Id..!');", true);
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool res = false;
            try
            {
                advisorBranchVo = (AdvisorBranchVo)Session["advisorBranchVo"];

                res = advisorBranchBo.DeleteBranch(advisorBranchVo.BranchId);
                if (res)
                {

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewBranches','none');", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Sorry... Branch is not deleted...');", true);
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
                FunctionInfo.Add("Method", "ViewBranchDetails.ascx.cs:btnDelete_Click()");
                object[] objects = new object[2];
                objects[0] = res;
                objects[1] = advisorBranchVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                advisorBranchVo = (AdvisorBranchVo)Session["advisorBranchVo"];
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EditBranchDetails','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewBranchDetails.ascx.cs:LinkButton1_Click()");
                object[] objects = new object[1];

                objects[0] = advisorBranchVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewBranches','none');", true);
        }

        protected void hiddenDelete_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                DeleteTerminalId();
            }
            else
            {
                ClearCheckBox();
            }
        }

        private void DeleteTerminalId()
        {
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            bool result = false;
            int Id = 0;
            try
            {
                foreach (GridViewRow dr in gvTerminalList.Rows)
                {
                    CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
                    if (checkBox.Checked)
                    {

                        Id = Convert.ToInt32(gvTerminalList.DataKeys[dr.RowIndex].Values["TerminalId"].ToString());
                        result = advisorBranchBo.DeleteBranchTerminal(Id);
                        if (result)
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Deleted Successfully..');", true);

                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Sorry..');", true);
                        }
                    }
                }
                LoadTerminalId();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewBranchDetails.ascx.cs:DeleteTerminalId()");
                object[] objects = new object[2];
                objects[0] = Id;
                objects[1] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ClearCheckBox()
        {
            foreach (GridViewRow dr in gvTerminalList.Rows)
            {
                CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
                checkBox.Checked = false;
            }
        }

        private void BindCommnGridView()
        {
            DataTable dt = new DataTable();
            try
            {
                if ((dt = advisorBranchBo.GetBranchAssociateCommission(advisorBranchVo.BranchId)) != null)
                {
                    gvCommStructure.DataSource = dt;
                    gvCommStructure.DataBind();
                    gvCommStructure.Visible = true;
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
                FunctionInfo.Add("Method", "ViewBranchDetails.ascx.cs:BindCommnGridView()");
                object[] objects = new object[2];
                objects[0] = advisorBranchVo;
                objects[1] = dt;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }
}