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
using System.IO;
using VOAssociates;
 

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
        OneWayEncryption encryption = new OneWayEncryption();

        List<AdvisorBranchVo> advisorBranchList = null;
        List<int> rmIds;
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AssociatesVO associatesVo = new AssociatesVO();
        UserBo userBo = new UserBo();

        int rmId;
        int userId;
        string path = "";
        bool isOpsIsChecked = false;
        bool isPurelyResearchLogin = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["UserVo"];
            rmVo = (RMVo)Session["RMVo"];
            trAddStaffCode.Visible = false  ;

            //if (Session["associatesVo"] != null)
            //{
            //    associatesVo = (AssociatesVO)Session["associatesVo"];
            //    txtStaffCode.Text = associatesVo.AAC_AgentCode.ToString();
            //}
            if (!IsPostBack)
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Verification", " CheckSubscription();", true);
                lblEmailDuplicate.Visible = false;
                GetPlanOpsStaffAddStatus(advisorVo.advisorId);
                setBranchList("N");
                hdnIsSubscripted.Value = advisorVo.IsISASubscribed.ToString();
            }
            
        }
        /// <summary>
        /// To check Ops Planwise ops enable or not
        /// </summary>
        /// <param name="adviserId"></param>
        private void GetPlanOpsStaffAddStatus(int adviserId)
        {
            DataSet dsPlanOpsStaffAddStatus = advisorStaffBo.GetPlanOpsStaffAddStatus(adviserId);
            if (dsPlanOpsStaffAddStatus.Tables[0].Rows[0]["WP_IsOpsEnabled"].ToString() == "1" && int.Parse(dsPlanOpsStaffAddStatus.Tables[0].Rows[0]["AS_NoOfBranches"].ToString()) > 1)
            {
                chkOps.Visible = true;
                lblOr.Visible = true;
                trCKMK.Visible = true;
                hdnIsOpsEnabled.Value = "1";

            }
            else
            {
                hdnIsOpsEnabled.Value = "0";
            }
        }

        public void setBranchList1(string IsExternal)
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
                    lblEmailDuplicate.Visible = true;
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

                    //rmUserVo.UserType = ddlRMRole.SelectedItem.Text.ToString().Trim();
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
                    rmVo.OfficePhoneDirectIsd = txtPhDirectISD.Text.ToString();
                    rmVo.OfficePhoneDirectNumber = int.Parse(txtPhDirectPhoneNumber.Text.ToString());

                    if (txtPhExtISD.Text == "")
                    {
                        rmVo.OfficePhoneExtIsd = string.Empty;
                    }
                    else
                    {
                        rmVo.OfficePhoneExtIsd = txtPhExtISD.Text.ToString();
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
                        rmVo.OfficePhoneExtStd = string.Empty;
                    }
                    else
                    {
                        rmVo.OfficePhoneExtStd = txtExtSTD.Text.ToString();
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
                        rmVo.OfficePhoneDirectStd = string.Empty;
                    }
                    else
                    {
                        rmVo.OfficePhoneDirectStd = txtPhDirectSTD.Text.ToString();
                    }

                    //rmVo.RMRole = ddlRMRole.SelectedValue.ToString();

                    rmVo.AdviserId = advisorVo.advisorId;

                    if (chkExternalStaff.Checked)
                    {
                        rmVo.IsExternal = 1;
                        rmVo.CTC = Double.Parse(txtCTCMonthly.Text);
                    }
                    else
                        rmVo.IsExternal = 0;

                    rmIds = advisorStaffBo.CreateCompleteRM(rmUserVo, rmVo, userVo.UserId, isOpsIsChecked, isPurelyResearchLogin,null);

                    rmVo.UserId = rmIds[0];
                    Session["rmId"] = rmIds[1];
                    Session["rmUserVo"] = userBo.GetUserDetails(rmVo.UserId);
                    Session["userId"] = rmVo.UserId;

                    //if (rmVo.RMRole == "RM")
                    //{
                    //    // Create Association for RM
                    //    userBo.CreateRoleAssociation(rmVo.UserId, 1001);
                    //    // 1001 - RM
                    //    // 1000 - Adviser
                    //    // 1003 - Customer
                    //}
                    //else
                    //{
                    //    // Create Association if BM
                    //    userBo.CreateRoleAssociation(rmVo.UserId, 1002);
                    //    // 1002 - BM
                    //    if (chkRM.Checked)
                    //    {
                    //        // Create Association for RM
                    //        userBo.CreateRoleAssociation(rmVo.UserId, 1001);
                    //    }
                    //}
                    //if (chkRM.Checked)
                    //{
                    //    // Create Association for RM
                    //     userBo.CreateRoleAssociation(rmVo.UserId, 1001);

                    //}
                    //if (chkBM.Checked)
                    //{
                    //    // Create Association if BM
                    //   userBo.CreateRoleAssociation(rmVo.UserId, 1002);

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
        
             protected void BtnStaffCode_Click(object sender, EventArgs e)
             {
                 string StaffRole;
                 if (ChklistRMBM.Items[0].Selected == true)
                 {
                     StaffRole = ChklistRMBM.Items[0].Text;
                 }
                 else if (ChklistRMBM.Items[1].Selected == true)
                 {
                     StaffRole = ChklistRMBM.Items[1].Text;
                 }
                 else 
                 {
                     StaffRole = ChklistRMBM.Items[2].Text;
                 }

                 //string queryString = "?prevPage=AddRM&?StaffName=" + txtFirstName.Text + "&?StaffRole='" + StaffRole + "' ";
                 string queryString = "?prevPage=AddRM&StaffName=" + txtFirstName.Text + "&StaffRole=" + StaffRole + "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddBranchRMAgentAssociation','" + queryString + "');", true);

                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddBranchRMAgentAssociation", "loadcontrol('AddBranchRMAgentAssociation', '?GoalId=" + 5 + "&GoalAction=" + "pavani m" + "&'" + queryString + "'');", true);
                 // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "rightpane", "loadcontrol('AddBranchRMAgentAssociation','none');", true);

                 //AssociatesVO associatesVo = new AssociatesVO();

                 //associatesVo = (AssociatesVO)Session["associatesVo"];
                 //txtStaffCode.Text = associatesVo.AAC_AgentCode.ToString();  
                 //=Request.QueryString["Name"];
             }
             protected void BtnStaffCode1_Click(object sender, EventArgs e)
             {
                 

                 //string queryString = "?prevPage=AddRM&?StaffName=" + txtFirstName.Text + "&?StaffRole='" + StaffRole + "' ";
                 //string queryString = "?prevPage=AddRM&StaffName=" + txtFirstName.Text + "&StaffRole=" + StaffRole + "";
                 ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewRM');", true);

                 //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddBranchRMAgentAssociation", "loadcontrol('AddBranchRMAgentAssociation', '?GoalId=" + 5 + "&GoalAction=" + "pavani m" + "&'" + queryString + "'');", true);
                 // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "rightpane", "loadcontrol('AddBranchRMAgentAssociation','none');", true);

                 //AssociatesVO associatesVo = new AssociatesVO();

                 //associatesVo = (AssociatesVO)Session["associatesVo"];
                 //txtStaffCode.Text = associatesVo.AAC_AgentCode.ToString();  
                 //=Request.QueryString["Name"];
             }


        //     protected void HiddenField1_ValueChanged(object sender, EventArgs e)
        //{
        //    string str = HiddenField1.Value;

        //       }
        protected void btnNext_Click(object sender, EventArgs e)
        {
         
            try
            {
                
                if (chkOps.Checked != true)
                {
                    if ((ChklistRMBM.Items[2].Selected == true) && ((ChklistRMBM.Items[0].Selected != true) && (ChklistRMBM.Items[1].Selected != true)))
                    {
                        isPurelyResearchLogin = true;
                    }
                    CreateRM(isPurelyResearchLogin);
                   
                }
                else
                {
                    isOpsIsChecked = true;
                    CreateOps(isOpsIsChecked);
                   
                }
                //RadWindowManager1.RadConfirm("Server radconfirm: Are you sure?", "confirmCallBackFn", 330, 180, null, "Server RadConfirm", choices.SelectedValue);
               // Response.Write("<script language=javascript>var conf= confirm('Do u want to add Agent code')</script>");
               // BtnStaffCode1.Attributes.Add("onclick", "return Confirmbtn();");
                //BtnStaffCode1.Attributes.Add();
              //  HiddenField1_ValueChanged(this, null);
                // HiddenField1.Value = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "showmessage();", true);

             // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Staff Details added successfully.Select Either to add agent code or view Staf Deatils..');", true);
                
                //trAddStaffCode.Visible = true;
                //BtnStaffCode.Visible = true;
                //BtnviewStaffCode.Visible = true;
                //BtnStaffCode_Click(this, null);
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
            //BtnStaffCode.Visible = true;
            //BtnviewStaffCode.Visible = true;
        }
        protected void hiddenDelete_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            if (val == "1")
            {
                BtnStaffCode_Click(this, null);
            }
            else
            {
                BtnStaffCode1_Click(this, null);
            }

        }
        private void CreateOps(bool isOpsIsChecked)
        {
            if (Validation())
            {
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                RMVo rmVo = new RMVo();
                string FromPageToCheckOps = string.Empty;
                Random id = new Random();
                AdvisorBo advisorBo = new AdvisorBo();
                advisorVo = (AdvisorVo)Session["advisorVo"];
                int branchId;
                string password = id.Next(10000, 99999).ToString();
                string hassedPassword = string.Empty;
                string saltValue = string.Empty;

                rmUserVo.UserType = "Advisor";
                //rmUserVo.Password = Encryption.Encrypt(password);
                encryption.GetHashAndSaltString(password, out hassedPassword, out saltValue);
                rmUserVo.Password = hassedPassword;
                rmUserVo.PasswordSaltValue = saltValue;
                rmUserVo.OriginalPassword = password;
                rmUserVo.IsTempPassword = 1;

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
                rmVo.StaffCode =  txtStaffcode.Text.ToString();
                if (txtMobileNumber.Text.ToString() != "")
                    rmVo.Mobile = Convert.ToInt64(txtMobileNumber.Text.ToString());
                if (!string.IsNullOrEmpty(txtPhDirectISD.Text.ToString()))
                    rmVo.OfficePhoneDirectIsd = txtPhDirectISD.Text.ToString();
                if (!string.IsNullOrEmpty(txtPhDirectPhoneNumber.Text.ToString()))
                    rmVo.OfficePhoneDirectNumber = int.Parse(txtPhDirectPhoneNumber.Text.ToString());

                if (txtPhExtISD.Text == "")
                {
                    rmVo.OfficePhoneExtIsd = string.Empty;
                }
                else
                {
                    rmVo.OfficePhoneExtIsd = txtPhExtISD.Text.ToString();
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
                    rmVo.OfficePhoneExtStd = string.Empty;
                }
                else
                {
                    rmVo.OfficePhoneExtStd = txtExtSTD.Text.ToString();
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
                    rmVo.OfficePhoneDirectStd = string.Empty;
                }
                else
                {
                    rmVo.OfficePhoneDirectStd = txtPhDirectSTD.Text.ToString();
                }
                if (chkOps.Checked == true)
                {
                    rmVo.RMRole = "Ops";
                }

                rmVo.AdviserId = advisorVo.advisorId;

                if (txtCTCMonthly.Text != "")
                    rmVo.CTC = Double.Parse(txtCTCMonthly.Text);


                rmIds = advisorStaffBo.CreateCompleteRM(rmUserVo, rmVo, userVo.UserId, isOpsIsChecked, isPurelyResearchLogin,null);

                rmVo.UserId = rmIds[0];
                if ((CheckListCKMK.Items[0].Selected == true) || (CheckListCKMK.Items[1].Selected == true))
                    foreach (ListItem Items in CheckListCKMK.Items)
                    {
                        if (Items.Selected)
                        {
                            if (Items.Text == "Checker")
                            {
                                // Create Association for checker
                                userBo.CreateUserPermisionAssociation(rmVo.UserId, Int16.Parse(Items.Value.ToString()));

                            }
                            else if (Items.Text == "Maker")
                            {
                                // Create Association for Maker
                                userBo.CreateUserPermisionAssociation(rmVo.UserId, Int16.Parse(Items.Value.ToString()));
                            }

                        }
                    }
                userBo.CreateRoleAssociation(rmVo.UserId, 1004);
                Session["rmId"] = rmIds[1];
                Session["rmUserVo"] = userBo.GetUserDetails(rmVo.UserId);
                Session["userId"] = rmVo.UserId;
                if (Request.QueryString["PreviousPageName"] != null)
                    FromPageToCheckOps = Request.QueryString["PreviousPageName"].ToString();

                if ((chkMailSend.Checked == true) && ((advisorVo.IsOpsEnable == 1) || (FromPageToCheckOps != string.Empty)))
                {
                    SendMail();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Ops staff created successfully!');", true);
                }
                else if (advisorVo.IsOpsEnable == 0 && FromPageToCheckOps != "EnvironmentalSettings")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Ops role is not enabled for this adviser, therefore Ops staff will not be able to login...!');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Ops staff created successfully!');", true);
                }


                if (Request.QueryString["PreviousPageName"] != null)
                {
                    string PreviousPage = "AddRM";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserEnvironmentSettings','PreviousPageName=" + PreviousPage + "');", true);
                }
                else
                {
                    string hdnSelectedString = hdnSelectedBranches.Value.ToString();
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewRM','none');", true);
                }

            }
        }



        private void CreateRM(bool isPurelyResearchLogin)
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
                string hassedPassword = string.Empty;
                string saltValue = string.Empty;

                rmUserVo.UserType = "Advisor";
                encryption.GetHashAndSaltString(password, out hassedPassword, out saltValue);
                rmUserVo.Password = hassedPassword;
                rmUserVo.PasswordSaltValue = saltValue;
                rmUserVo.OriginalPassword = password;
                rmUserVo.IsTempPassword = 1;

                //rmUserVo.Password = Encryption.Encrypt(password);
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
                rmVo.StaffCode =  txtStaffcode.Text.ToString();
                if (txtMobileNumber.Text.ToString() != "")
                    rmVo.Mobile = Convert.ToInt64(txtMobileNumber.Text.ToString());
                if (!string.IsNullOrEmpty(txtPhDirectISD.Text.ToString()))
                    rmVo.OfficePhoneDirectIsd = txtPhDirectISD.Text.ToString();
                if (!string.IsNullOrEmpty(txtPhDirectPhoneNumber.Text.ToString()))
                    rmVo.OfficePhoneDirectNumber = int.Parse(txtPhDirectPhoneNumber.Text.ToString());

                if (txtPhExtISD.Text == "")
                {
                    rmVo.OfficePhoneExtIsd = string.Empty;
                }
                else
                {
                    rmVo.OfficePhoneExtIsd = txtPhExtISD.Text.ToString();
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
                    rmVo.OfficePhoneExtStd = string.Empty;
                }
                else
                {
                    rmVo.OfficePhoneExtStd = txtExtSTD.Text.ToString();
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
                    rmVo.OfficePhoneDirectStd = string.Empty;
                }
                else
                {
                    rmVo.OfficePhoneDirectStd = txtPhDirectSTD.Text.ToString();
                }
                if (ChklistRMBM.Items[0].Selected == true)
                {
                    rmVo.RMRole = "RM";
                }
                else if (ChklistRMBM.Items[1].Selected == true)
                {
                    rmVo.RMRole = "BM";
                }
                else if (ChklistRMBM.Items[2].Selected == true)
                {
                    rmVo.RMRole = "Research";
                }
                //rmVo.RMRole = ddlRMRole.SelectedValue.ToString();

                rmVo.AdviserId = advisorVo.advisorId;

                if (txtCTCMonthly.Text != "")
                    rmVo.CTC = Double.Parse(txtCTCMonthly.Text);

                if (chkExternalStaff.Checked)
                {
                    rmVo.IsExternal = 1;

                }
                else
                    rmVo.IsExternal = 0;

                rmIds = advisorStaffBo.CreateCompleteRM(rmUserVo, rmVo, userVo.UserId, isOpsIsChecked, isPurelyResearchLogin,null);

                rmVo.UserId = rmIds[0];
                Session["rmId"] = rmIds[1];
                Session["rmUserVo"] = userBo.GetUserDetails(rmVo.UserId);
                Session["userId"] = rmVo.UserId;
                if (chkMailSend.Checked == true)
                {
                    SendMail();
                }


                //if (rmVo.RMRole == "RM")
                //{
                //    // Create Association for RM
                //    userBo.CreateRoleAssociation(rmVo.UserId, 1001);
                //    // 1001 - RM
                //    // 1000 - Adviser
                //    // 1003 - Customer
                //}
                //else
                //{
                //    // Create Association if BM
                //    userBo.CreateRoleAssociation(rmVo.UserId, 1002);
                //    // 1002 - BM
                //    if (chkRM.Checked)
                //    {
                //        // Create Association for RM
                //        userBo.CreateRoleAssociation(rmVo.UserId, 1001);
                //    }
                //}

                foreach (ListItem Items in ChklistRMBM.Items)
                {
                    if (Items.Selected)
                    {
                        if (Items.Text == "RM")
                        {
                            // Create Association for RM
                            userBo.CreateRoleAssociation(rmVo.UserId, Int16.Parse(Items.Value.ToString()));

                        }
                        else if (Items.Text == "BM")
                        {
                            // Create Association for RM
                            userBo.CreateRoleAssociation(rmVo.UserId, Int16.Parse(Items.Value.ToString()));
                        }
                        else if (Items.Text == "Research")
                        {
                            // Create Association for Research
                            userBo.CreateRoleAssociation(rmVo.UserId, Int16.Parse(Items.Value.ToString()));
                        }

                    }

                }
                foreach (ListItem Items in CheckListCKMK.Items)
                {
                    if (Items.Selected)
                    {
                        if (Items.Text == "Checker")
                        {
                            // Create Association for RM
                            userBo.CreateUserPermisionAssociation(rmVo.UserId, Int16.Parse(Items.Value.ToString()));

                        }
                        else if (Items.Text == "Maker")
                        {
                            // Create Association for RM
                            userBo.CreateUserPermisionAssociation(rmVo.UserId, Int16.Parse(Items.Value.ToString()));
                        }

                    }
                }

                string hdnSelectedString = hdnSelectedBranches.Value.ToString();

                if (!string.IsNullOrEmpty(hdnSelectedString))
                {
                    string[] selectedBranchesList = hdnSelectedString.Split(',');

                    foreach (string str in selectedBranchesList)
                    {
                        if (str != "")
                        {
                            advisorBranchBo.CreateRMBranchAssociation(rmIds[1], int.Parse(str), advisorVo.advisorId, advisorVo.advisorId);

                        }
                    }

                }

             //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewRM','none');", true);

            }
        }

        //protected void ddlRMRole_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlRMRole.SelectedValue == "BM")
        //    {
        //        chkRM.Visible = true;
        //        chkExternalStaff.Visible = true;
        //    }
        //    else
        //    {
        //        chkRM.Visible = false;
        //        chkExternalStaff.Visible = true;
        //    }
        //}

        //protected void chkRM_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkRM.Checked)
        //    {
        //        chkExternalStaff.Visible = true;
        //    }
        //    else
        //    {
        //        chkExternalStaff.Visible = true;
        //    }
        //}

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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Verification", " CheckSubscription();", true);
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
            string logoPath = string.Empty;


            string userName = rmUserVo.FirstName + " " + rmUserVo.MiddleName + " " + rmUserVo.LastName;
            if (chkOps.Checked == true)
                email.GetAdviserRMAccountMail("Ops" + Session["userId"].ToString(), rmUserVo.OriginalPassword, userName);
            else if ((ChklistRMBM.Items[2].Selected == true) && ((ChklistRMBM.Items[0].Selected != true) && (ChklistRMBM.Items[1].Selected != true)))
                email.GetAdviserRMAccountMail("Research" + Session["userId"].ToString(), rmUserVo.OriginalPassword, userName);
            else
                email.GetAdviserRMAccountMail("rm" + Session["userId"].ToString(), rmUserVo.OriginalPassword, userName);



            email.Subject = email.Subject.Replace("WealthERP", advisorVo.OrganizationName);
            email.Subject = email.Subject.Replace("MoneyTouch", advisorVo.OrganizationName);

            email.Body = email.Body.Replace("[NAME]", userName);
            email.Body = email.Body.Replace("[ORGANIZATION]", advisorVo.OrganizationName);
            if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Wealtherp")
            {
                email.Body = email.Body.Replace("[WEBSITE]", !string.IsNullOrEmpty(advisorVo.DomainName.Trim()) ? advisorVo.Website.Trim() : "https://app.wealtherp.com/");
            }
            else if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Citrus")
            {
                email.Body = email.Body.Replace("[WEBSITE]", !string.IsNullOrEmpty(advisorVo.DomainName.Trim()) ? advisorVo.Website.Trim() : "https://www.citrusindiaonline.com/");
            }
            email.Body = email.Body.Replace("[CONTACTPERSON]", (!string.IsNullOrEmpty(advisorVo.ContactPersonFirstName.Trim()) ? advisorVo.ContactPersonFirstName.Trim() + " " : String.Empty) + (!string.IsNullOrEmpty(advisorVo.ContactPersonMiddleName) ? advisorVo.ContactPersonMiddleName.Trim() + " " : String.Empty) + (!string.IsNullOrEmpty(advisorVo.ContactPersonLastName) ? advisorVo.ContactPersonLastName.Trim() + " " : String.Empty));
            email.Body = email.Body.Replace("[DESIGNATION]", advisorVo.Designation);
            email.Body = email.Body.Replace("[PHONE]", advisorVo.Phone1Std.ToString() + "-" + advisorVo.Phone1Number.ToString());
            email.Body = email.Body.Replace("[EMAIL]", advisorVo.Email);
            email.Body = email.Body.Replace("[LOGO]", "<img src='cid:HDIImage' alt='Logo'>");

            System.Net.Mail.AlternateView htmlView;
            System.Net.Mail.AlternateView plainTextView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("Text view", null, "text/plain");
            //System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(hidBody.Value.Trim() + "<image src=cid:HDIImage>", null, "text/html");
            htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<html><body " + "style='font-family:Tahoma, Arial; font-size: 10pt;'><p>" + email.Body + "</p></body></html>", null, "text/html");
            //Add image to HTML version
            if (advisorVo != null)
                logoPath = "~/Images/" + advisorVo.LogoPath;
            if (!File.Exists(Server.MapPath(logoPath)))
                logoPath = "~/Images/spacer.png";
            //System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath("~/Images/") + @"\3DSYRW_4009.JPG", "image/jpeg");
            System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath(logoPath), "image/jpeg");
            imageResource.ContentId = "HDIImage";
            htmlView.LinkedResources.Add(imageResource);
            //Add two views to message.
            email.AlternateViews.Add(plainTextView);
            email.AlternateViews.Add(htmlView);
            email.To.Add(rmUserVo.Email);

            //AdviserStaffSMTPBo adviserStaffSMTPBo = new AdviserStaffSMTPBo();
            //AdviserStaffSMTPVo adviserStaffSMTPVo = adviserStaffSMTPBo.GetSMTPCredentials(advisorVo.advisorId);
            //if (adviserStaffSMTPVo.HostServer != null && adviserStaffSMTPVo.HostServer != string.Empty)
            //{
            //    emailer.isDefaultCredentials = !Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired);

            //    if (!String.IsNullOrEmpty(adviserStaffSMTPVo.Password))
            //        emailer.smtpPassword = Encryption.Decrypt(adviserStaffSMTPVo.Password);
            //    emailer.smtpPort = int.Parse(adviserStaffSMTPVo.Port);
            //    emailer.smtpServer = adviserStaffSMTPVo.HostServer;
            //    emailer.smtpUserName = adviserStaffSMTPVo.Email;

            //    if (Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired))
            //    {
            //        email.From = new MailAddress(emailer.smtpUserName, advisorVo.OrganizationName);
            //    }
            //}


            AdviserStaffSMTPBo adviserStaffSMTPBo = new AdviserStaffSMTPBo();
            AdviserStaffSMTPVo adviserStaffSMTPVo = adviserStaffSMTPBo.GetSMTPCredentials(((RMVo)Session["rmVo"]).RMId);
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
                    if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Wealtherp")
                    {
                        email.From = new MailAddress(emailer.smtpUserName, advisorVo.OrganizationName);
                    }
                    else if (ConfigurationSettings.AppSettings["HostName"].ToString() == "MoneyTouch")
                    {
                        email.From = new MailAddress(emailer.smtpUserName, advisorVo.OrganizationName);
                    }

                }
            }
            bool isMailSent = emailer.SendMail(email);
            return isMailSent;
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

