using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using VoUser;
using VoAdvisorProfiling;
using BoUser;
using BoCommon;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Configuration;
using System.Net.Mail;
using PCGMailLib;

namespace WealthERP.Advisor
{
    public partial class AddRM : System.Web.UI.UserControl
    {
        AdvisorBranchVo advisorBranchVo = null;
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo tempUserVo = new UserVo();
        UserVo rmUserVo = new UserVo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();

        List<AdvisorBranchVo> advisorBranchList = null;
        List<int> rmIds;
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        UserBo userBo = new UserBo();
        
        int rmId;
        int userId;
        string path = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["UserVo"];
            
            if (!IsPostBack)
            {
                lblEmailDuplicate.Visible = false;
                chkRM.Visible = false;
                chkExternalStaff.Visible = false;
                setBranchList("N");
            }
        }

        public void setBranchList(string IsExternal)
        {
            UserVo rmUserVo = null;
            DataRow drAdvisorBranch;
            DataTable dtAdvisorBranch = new DataTable();

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
                
                if (advisorBranchList == null)
                {
                    lblError.Visible = true;
                    lblError.Text = "Add some Branches..";
                    gvBranchList.DataSource = null;
                    gvBranchList.DataBind();
                }
                else
                {
                    gvBranchList.Visible = true;
                    lblError.Visible = false;

                    dtAdvisorBranch.Columns.Add("Sl.No.");
                    dtAdvisorBranch.Columns.Add("BranchId");
                    dtAdvisorBranch.Columns.Add("Branch Name");
                    dtAdvisorBranch.Columns.Add("Branch Address");
                    dtAdvisorBranch.Columns.Add("Branch Phone");

                    for (int i = 0; i < advisorBranchList.Count; i++)
                    {
                        drAdvisorBranch = dtAdvisorBranch.NewRow();
                        advisorBranchVo = new AdvisorBranchVo();
                        advisorBranchVo = advisorBranchList[i];
                        drAdvisorBranch[0] = (i + 1).ToString();
                        drAdvisorBranch[1] = advisorBranchVo.BranchId.ToString();
                        drAdvisorBranch[2] = advisorBranchVo.BranchName.ToString();
                        if (advisorBranchVo.State != "" && advisorBranchVo.State != "Select a State")
                        {
                            drAdvisorBranch[3] = advisorBranchVo.AddressLine1.ToString() + "'" + advisorBranchVo.AddressLine2.ToString() + "'" + advisorBranchVo.AddressLine3.ToString() + "," + advisorBranchVo.City.ToString() + "'" + XMLBo.GetStateName(path, advisorBranchVo.State.ToString());
                        }
                        else
                        {
                            drAdvisorBranch[3] = advisorBranchVo.AddressLine1.ToString() + "'" + advisorBranchVo.AddressLine2.ToString() + "'" + advisorBranchVo.AddressLine3.ToString() + "," + advisorBranchVo.City.ToString() + "'";
                        }

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
                objects[0] = rmUserVo;
                objects[1] = advisorBranchList;
                objects[2] = advisorBranchVo;
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
            double res;
           
            try
            {
                //if (!ChkMailId(txtEmail.Text.ToString()))
                //{
                //    result = false;
                //    lblEmail.CssClass = "Error";
                //}
                //else
                //{
                    if (!chkAvailability())
                    {
                        result = false;
                        lblEmailDuplicate.Visible=true;
                    }
                    else
                    {
                        result = true;
                        lblEmail.CssClass = "FieldName";
                    }
              //  }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AddRM.ascx:Validation()");


                object[] objects = new object[1];
                objects[0] = result;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;

        }

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

                FunctionInfo.Add("Method", "AddRM.ascx:ChkMailId()");

                object[] objects = new object[1];
                objects[0] = email;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                    RMVo rmVo = new RMVo();
                    Random id = new Random();
                    AdvisorBo advisorBo = new AdvisorBo();

                    string password = id.Next(10000, 99999).ToString();

                    rmUserVo.UserType = ddlRMRole.SelectedItem.Text.ToString().Trim();
                    rmUserVo.Password = password;
                    rmUserVo.MiddleName = txtMiddleName.Text.ToString();
                    rmUserVo.LoginId = txtEmail.Text.ToString();
                    rmUserVo.LastName = txtLastName.Text.ToString();
                    rmUserVo.FirstName = txtFirstName.Text.ToString();
                    rmUserVo.Email = txtEmail.Text.ToString();
                    
                    rmVo.Email = txtEmail.Text.ToString();
                    if (txtFaxNumber.Text == "")
                    {
                        rmVo.Fax = 0;
                    }
                    else
                    {
                        rmVo.Fax = int.Parse(txtFaxNumber.Text.ToString());
                    }
                    if (txtFaxISD.Text == "")
                    {
                        rmVo.FaxIsd = 0;
                    }
                    else
                    {
                        rmVo.FaxIsd = int.Parse(txtFaxISD.Text.ToString());
                    }
                    if (txtFaxSTD.Text == "")
                    {
                        rmVo.FaxStd = 0;
                    }
                    else
                    {
                        rmVo.FaxStd = int.Parse(txtExtSTD.Text.ToString());
                    }

                    rmVo.FirstName = txtFirstName.Text.ToString();
                    rmVo.LastName = txtLastName.Text.ToString();
                    rmVo.MiddleName = txtMiddleName.Text.ToString();
                    rmVo.Mobile = Convert.ToInt64(txtMobileNumber.Text.ToString());
                    rmVo.OfficePhoneDirectIsd = int.Parse(txtPhDirectISD.Text.ToString());
                    rmVo.OfficePhoneDirectNumber = int.Parse(txtPhDirectPhoneNumber.Text.ToString());

                    if (txtPhExtISD.Text == "")
                    {
                        rmVo.OfficePhoneExtIsd = 0;
                    }
                    else
                    {
                        rmVo.OfficePhoneExtIsd = int.Parse(txtPhExtISD.Text.ToString());
                    }
                    if (txtPhExtPhoneNumber.Text == "")
                    {
                        rmVo.OfficePhoneExtNumber = 0;
                    }
                    else
                    {
                        rmVo.OfficePhoneExtNumber = int.Parse(txtPhExtPhoneNumber.Text.ToString());
                    }
                    if (txtExtSTD.Text == "")
                    {
                        rmVo.OfficePhoneExtStd = 0;
                    }
                    else
                    {
                        rmVo.OfficePhoneExtStd = int.Parse(txtExtSTD.Text.ToString());
                    }
                    if (txtPhResiISD.Text == "")
                    {
                        rmVo.ResPhoneIsd = 0;
                    }
                    else
                    {
                        rmVo.ResPhoneIsd = int.Parse(txtPhResiISD.Text.ToString());
                    }
                    if (txtPhResiPhoneNumber.Text == "")
                    {
                        rmVo.ResPhoneNumber = 0;
                    }
                    else
                    {
                        rmVo.ResPhoneNumber = int.Parse(txtPhResiPhoneNumber.Text.ToString());
                    }
                    if (txtResiSTD.Text == "")
                    {
                        rmVo.ResPhoneStd = 0;
                    }
                    else
                    {
                        rmVo.ResPhoneStd = int.Parse(txtResiSTD.Text.ToString());
                    }
                    if (txtPhDirectSTD.Text == "")
                    {
                        rmVo.OfficePhoneDirectStd = 0;
                    }
                    else
                    {
                        rmVo.OfficePhoneDirectStd = int.Parse(txtPhDirectSTD.Text.ToString());
                    }

                    rmVo.RMRole = ddlRMRole.SelectedValue.ToString();
                    
                    rmVo.AdviserId = advisorVo.advisorId;

                    if (chkExternalStaff.Checked)
                    {
                        rmVo.IsExternal = 1;
                        rmVo.CTC = Double.Parse(txtCTCMonthly.Text);
                    }
                    else
                        rmVo.IsExternal = 0;

                    rmIds = advisorStaffBo.CreateCompleteRM(rmUserVo, rmVo, userVo.UserId);

                    rmVo.UserId = rmIds[0];
                    Session["rmId"] = rmIds[1];
                    Session["rmUserVo"] = userBo.GetUserDetails(rmVo.UserId);
                    Session["userId"] = rmVo.UserId;

                    if (rmVo.RMRole == "RM")
                    {
                        // Create Association for RM
                        userBo.CreateRoleAssociation(rmVo.UserId, 1001);
                        // 1001 - RM
                        // 1000 - Adviser
                        // 1003 - Customer
                    }
                    else
                    {
                        // Create Association if BM
                        userBo.CreateRoleAssociation(rmVo.UserId, 1002);
                        // 1002 - BM
                        if (chkRM.Checked)
                        {
                            // Create Association for RM
                            userBo.CreateRoleAssociation(rmVo.UserId, 1001);
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
                FunctionInfo.Add("Method", "AddRM.ascx:btnSave_Click()");
                object[] objects = new object[5];
                objects[0] = advisorVo;
                objects[1] = userVo;
                objects[2] = rmUserVo;
                objects[3] = userId;
                objects[4] = rmVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnRMBranchAssociation_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlRMRole.SelectedValue.ToString() == "RM")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMBranchAssociation','none');", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('BMBranchAssociation','none');", true);
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
                FunctionInfo.Add("Method", "AddRM.ascx:btnSave_Click()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            //int i = 0;
            //int j = 0,temp=0;
            try
            {
                //if (gvBranchList.Rows.Count != 0)
                //{
                //    foreach (GridViewRow gvr in this.gvBranchList.Rows)
                //    {
                //        if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                //        {
                //            i = i + 1;
                //            j = gvr.RowIndex;
                //        }
                //    }
                //    if (i == 1)
                //    {
                //        ((RadioButton)gvBranchList.Rows[j].FindControl("rbtnMainBranch")).Checked = true;
                //        CreateRM();
                //    }
                //    else if (i > 1)
                //    {
                //        foreach (GridViewRow row in gvBranchList.Rows)
                //        {
                //            if (((RadioButton)row.FindControl("rbtnMainBranch")).Checked)
                //            {
                //                temp = temp + 1;
                //            }
                //        }
                //        if (temp == 0)
                //        {
                //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Please select the branch..!');", true);
                //        }
                //        else
                //        {
                            CreateRM();
                //        }
                //    }
                //    else if (i == 0)
                //    {
                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Please select the branch..!');", true);
                //    }

                //}
                //else
                //{
                //    CreateRM();
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
                FunctionInfo.Add("Method", "AddRM.ascx:btnSave_Click()");
                object[] objects = new object[5];
                objects[0] = advisorVo;
                objects[1] = userVo;
                objects[2] = rmVo;
                objects[3] = userId;
                objects[4] = rmUserVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void CreateRM()
        {
            if (Validation())
            {
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                RMVo rmVo = new RMVo();
                Random id = new Random();
                AdvisorBo advisorBo = new AdvisorBo();
                advisorVo = (AdvisorVo)Session["advisorVo"];
                int branchId;
                string password = id.Next(10000, 99999).ToString();

                rmUserVo.UserType = "Advisor";
                rmUserVo.Password = Encryption.Encrypt(password);
                rmUserVo.MiddleName = txtMiddleName.Text.ToString();
                //rmUserVo.LoginId = txtEmail.Text.ToString();
                rmUserVo.LastName = txtLastName.Text.ToString();
                rmUserVo.FirstName = txtFirstName.Text.ToString();
                rmUserVo.Email = txtEmail.Text.ToString();

                rmVo.Email = txtEmail.Text.ToString();
                if (txtFaxNumber.Text == "")
                {
                    rmVo.Fax = 0;
                }
                else
                {
                    rmVo.Fax = int.Parse(txtFaxNumber.Text.ToString());
                }
                if (txtFaxISD.Text == "")
                {
                    rmVo.FaxIsd = 0;
                }
                else
                {
                    rmVo.FaxIsd = int.Parse(txtFaxISD.Text.ToString());
                }
                if (txtFaxSTD.Text == "")
                {
                    rmVo.FaxStd = 0;
                }
                else
                {
                    rmVo.FaxStd = int.Parse(txtExtSTD.Text.ToString());
                }

                rmVo.FirstName = txtFirstName.Text.ToString();
                rmVo.LastName = txtLastName.Text.ToString();
                rmVo.MiddleName = txtMiddleName.Text.ToString();
                if(txtMobileNumber.Text.ToString()!="")
                    rmVo.Mobile = Convert.ToInt64(txtMobileNumber.Text.ToString());
                if(txtPhDirectISD.Text.ToString()!="")
                    rmVo.OfficePhoneDirectIsd = int.Parse(txtPhDirectISD.Text.ToString());
                if (txtPhDirectPhoneNumber.Text.ToString() != "")
                    rmVo.OfficePhoneDirectNumber = int.Parse(txtPhDirectPhoneNumber.Text.ToString());

                if (txtPhExtISD.Text == "")
                {
                    rmVo.OfficePhoneExtIsd = 0;
                }
                else
                {
                    rmVo.OfficePhoneExtIsd = int.Parse(txtPhExtISD.Text.ToString());
                }
                if (txtPhExtPhoneNumber.Text == "")
                {
                    rmVo.OfficePhoneExtNumber = 0;
                }
                else
                {
                    rmVo.OfficePhoneExtNumber = int.Parse(txtPhExtPhoneNumber.Text.ToString());
                }
                if (txtExtSTD.Text == "")
                {
                    rmVo.OfficePhoneExtStd = 0;
                }
                else
                {
                    rmVo.OfficePhoneExtStd = int.Parse(txtExtSTD.Text.ToString());
                }
                if (txtPhResiISD.Text == "")
                {
                    rmVo.ResPhoneIsd = 0;
                }
                else
                {
                    rmVo.ResPhoneIsd = int.Parse(txtPhResiISD.Text.ToString());
                }
                if (txtPhResiPhoneNumber.Text == "")
                {
                    rmVo.ResPhoneNumber = 0;
                }
                else
                {
                    rmVo.ResPhoneNumber = int.Parse(txtPhResiPhoneNumber.Text.ToString());
                }
                if (txtResiSTD.Text == "")
                {
                    rmVo.ResPhoneStd = 0;
                }
                else
                {
                    rmVo.ResPhoneStd = int.Parse(txtResiSTD.Text.ToString());
                }
                if (txtPhDirectSTD.Text == "")
                {
                    rmVo.OfficePhoneDirectStd = 0;
                }
                else
                {
                    rmVo.OfficePhoneDirectStd = int.Parse(txtPhDirectSTD.Text.ToString());
                }

                rmVo.RMRole = ddlRMRole.SelectedValue.ToString();

                rmVo.AdviserId = advisorVo.advisorId;

                if(txtCTCMonthly.Text != "")
                    rmVo.CTC = Double.Parse(txtCTCMonthly.Text);

                if (chkExternalStaff.Checked)
                {
                    rmVo.IsExternal = 1;
                    
                }
                else
                    rmVo.IsExternal = 0;

                rmIds = advisorStaffBo.CreateCompleteRM(rmUserVo, rmVo, userVo.UserId);
                
                rmVo.UserId = rmIds[0];
                Session["rmId"] = rmIds[1];
                Session["rmUserVo"] = userBo.GetUserDetails(rmVo.UserId);
                Session["userId"] = rmVo.UserId;

                SendMail();



                if (rmVo.RMRole == "RM")
                {
                    // Create Association for RM
                    userBo.CreateRoleAssociation(rmVo.UserId, 1001);
                    // 1001 - RM
                    // 1000 - Adviser
                    // 1003 - Customer
                }
                else
                {
                    // Create Association if BM
                    userBo.CreateRoleAssociation(rmVo.UserId, 1002);
                    // 1002 - BM
                    if (chkRM.Checked)
                    {
                        // Create Association for RM
                        userBo.CreateRoleAssociation(rmVo.UserId, 1001);
                    }
                }

                if (gvBranchList.Rows.Count!=0)
                {
                    foreach (GridViewRow gvr in this.gvBranchList.Rows)
                    {
                        if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                        {
                            branchId = int.Parse(gvBranchList.DataKeys[gvr.RowIndex].Value.ToString());
                            //if (((RadioButton)gvr.FindControl("rbtnMainBranch")).Checked == true)
                            //{
                            //    if (advisorBranchBo.AssociateBranch(rmIds[1], branchId, 1, userVo.UserId))
                            //    {
                            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert(' Association is done successfully');", true);
                            //    }
                            //    else
                            //    {
                            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Sorry.. Association is not done');", true);
                            //    }
                            //}
                            //else
                            //{
                                if (!advisorBranchBo.AssociateBranch(rmIds[1], branchId, 0, userVo.UserId))
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Sorry.. Association is not done');", true);
                                }
                                
                            //}
                        }
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewRM','none');", true);

            }
        }

        protected void ddlRMRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRMRole.SelectedValue == "BM")
            {
                chkRM.Visible = true;
                chkExternalStaff.Visible = true;
            }
            else
            {
                chkRM.Visible = false;
                chkExternalStaff.Visible = true;
            }
        }

        protected void chkRM_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRM.Checked)
            {
                chkExternalStaff.Visible = true;
            }
            else
            {
                chkExternalStaff.Visible = true;
            }
        }

        protected void rbtnMainBranch_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvBranchList.Rows)
            {
                ((RadioButton)row.FindControl("rbtnMainBranch")).Checked = false;
            }
            RadioButton rbtn = (RadioButton)sender;
            GridViewRow tempRow = (GridViewRow)rbtn.NamingContainer;
            if (((CheckBox)tempRow.FindControl("chkId")).Checked == true)
            {
                ((RadioButton)tempRow.FindControl("rbtnMainBranch")).Checked = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Pageloadscript", "alert('Please select Main branch..!');", true);
            }
        }

        protected void chkExternalStaff_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExternalStaff.Checked)
            {
                setBranchList("Y");
            }
            else
            {
                setBranchList("N");
            }
        }

        protected void txtCTCMonthly_TextChanged(object sender, EventArgs e)
        {
            txtCTCYearly.Text = (Double.Parse(txtCTCMonthly.Text) * 12).ToString();
        }

        private bool SendMail()
        {
            Emailer emailer = new Emailer();
            EmailMessage email = new EmailMessage();

            string userName = rmUserVo.FirstName + " " + rmUserVo.MiddleName + " " + rmUserVo.LastName;
            email.GetAdviserRMAccountMail("rm" + Session["userId"].ToString(), Encryption.Decrypt(rmUserVo.Password), userName);
            email.To.Add(rmUserVo.Email);

            AdviserStaffSMTPBo adviserStaffSMTPBo = new AdviserStaffSMTPBo();
            AdviserStaffSMTPVo adviserStaffSMTPVo = adviserStaffSMTPBo.GetSMTPCredentials(advisorVo.advisorId);
            if (adviserStaffSMTPVo.HostServer != null && adviserStaffSMTPVo.HostServer != string.Empty)
            {
                emailer.isDefaultCredentials = !Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired);

                if (!String.IsNullOrEmpty(adviserStaffSMTPVo.Password))
                    emailer.smtpPassword = Encryption.Decrypt(adviserStaffSMTPVo.Password);
                emailer.smtpPort = int.Parse(adviserStaffSMTPVo.Port);
                emailer.smtpServer = adviserStaffSMTPVo.HostServer;
                emailer.smtpUserName = adviserStaffSMTPVo.Email;

                if (Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired))
                {
                    email.From = new MailAddress(emailer.smtpUserName, "WealthERP");
                }
            }
            bool isMailSent = emailer.SendMail(email);
            return isMailSent;
        }
    }
}
