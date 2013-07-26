using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoSuperAdmin;
using VoSuperAdmin;
using System.Data;
using VoUser;
using PCGMailLib;
using BoCommon;

namespace WealthERP.SuperAdmin
{
    public partial class Subscription : System.Web.UI.UserControl
    {
        DataSet _dsGetSubscriptionDetails;
        DataSet dsGetPlanSubscriptionFlavourDetails;
        AdvisorVo advisorVo = new AdvisorVo();
        AdviserSubscriptionBo _advisersubscriptionbo = new AdviserSubscriptionBo();
        AdviserSubscriptionVo _advisersubscriptionvo = new AdviserSubscriptionVo();
        UserVo IFAuserVo = new UserVo();
        OneWayEncryption encryption = new OneWayEncryption();
        Random r = new Random();
        int adviserId=0;

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["IFAadvisorVo"];
          //  txtBalanceSize.Attributes.Add("disabled", "true");

            IFAuserVo = (UserVo)Session["iffUserVo"];

            //if (Session["storageBalance"] != "" && Session["storageBalance"] != null)
            //{
            //    txtBalanceSize.Text =Convert.ToString(Session["storageBalance"]);

            //}

            if (IFAuserVo.IsTempPassword == 1)
            {
                chkMailSend.Enabled = true;
                chkMailSend.Checked = true;
            }
            else
            {
                chkMailSend.Enabled = false;
                chkMailSend.Checked = false;
            }
            if (!IsPostBack)
            {
                // BindPlanDropdown();
                if(advisorVo !=null)
                    adviserId=advisorVo.advisorId;
                dsGetPlanSubscriptionFlavourDetails = _advisersubscriptionbo.GetAdviserSubscriptionPlanFlavourDetails(adviserId);
                Session["PlanSubscriptionFlavourDetails"] = dsGetPlanSubscriptionFlavourDetails;
                BindWerpPlan();
                if (advisorVo != null)
                {
                    SetAdviserFlavourSubscription();
                }
                
            }
        }
        /// <summary>
        /// Bind Plan table with New Data
        /// </summary>
        private void BindWerpPlan()
        {
            if (Session["PlanSubscriptionFlavourDetails"] != null )
            {
                dsGetPlanSubscriptionFlavourDetails =(DataSet) Session["PlanSubscriptionFlavourDetails"];
                if (dsGetPlanSubscriptionFlavourDetails.Tables[0].Rows.Count > 0)
                {
                    ddlPlan.DataSource = dsGetPlanSubscriptionFlavourDetails.Tables[0];
                    ddlPlan.DataValueField = dsGetPlanSubscriptionFlavourDetails.Tables[0].Columns["WP_PlanId"].ToString();
                    ddlPlan.DataTextField = dsGetPlanSubscriptionFlavourDetails.Tables[0].Columns["WP_PlanName"].ToString();
                    ddlPlan.DataBind();
                    ddlPlan.Items.Insert(0, new ListItem("Select", "Select"));
                }
            }
        }
        //public void BindPlanDropdown()
        //{
        //    DataSet _dsBindDropDown;
        //    AdviserSubscriptionBo _advisersubscriptionbo = new AdviserSubscriptionBo();
        //    _dsBindDropDown= _advisersubscriptionbo.GetWerpPlans();
        //    if (_dsBindDropDown != null && _dsBindDropDown.Tables[0].Rows.Count > 0)
        //    {
        //        ddlPlan.DataSource = _dsBindDropDown.Tables[0];
        //        ddlPlan.DataTextField = "WP_PlanName";
        //        ddlPlan.DataValueField = "WP_PlanId";                
        //        ddlPlan.DataBind();
        //        ddlPlan.Items.Insert(0, new ListItem("--Select--", "Select"));
        //    }
        //}

        public void SetAdviserFlavourSubscription()
        {
            DataTable dtPlan = new DataTable(); ;
            DataRow[] drPlan;
            int isNoBranchs = 0; int isNooStaffs = 0; int customerLogins = 0;
            int noOfStaffWebLogins = 0; int noOfBranches = 0; int storageSizeBalance = 0;

            //----------------------------------------New Logic------------------------------------------------
            DataTable dtPlanSubscription = new DataTable();
            if (Session["PlanSubscriptionFlavourDetails"] != null)
            {
                dsGetPlanSubscriptionFlavourDetails = (DataSet)Session["PlanSubscriptionFlavourDetails"];
                dtPlanSubscription = dsGetPlanSubscriptionFlavourDetails.Tables[1];
                if (dtPlanSubscription.Rows.Count > 0)
                {
                    txtComment.Text = dtPlanSubscription.Rows[0]["AS_Comments"].ToString();
                    if (!string.IsNullOrEmpty(dtPlanSubscription.Rows[0]["AS_SubscriptionEndDate"].ToString()))
                    {
                        dpEndDate.SelectedDate = DateTime.Parse(dtPlanSubscription.Rows[0]["AS_SubscriptionEndDate"].ToString());
                    }
                    double storageBalance = 0;
                    double storagePaidSize = 0;
                    double storageSize = 0;
                    storageBalance = double.Parse(dtPlanSubscription.Rows[0]["AS_StorageBalance"].ToString());
                    storagePaidSize = double.Parse(dtPlanSubscription.Rows[0]["AS_PaidStorage"].ToString());
                    storageSize = double.Parse(dtPlanSubscription.Rows[0]["AS_StorageSize"].ToString());

                    txtPaidSize.Text = Convert.ToString(storagePaidSize);
                    hdnStorageUsed.Value = Convert.ToString(storageSize - storageBalance);
                    txtUsedSpace.Text = Convert.ToString(storageSize - storageBalance);
                    txtDefaultStorage.Text = dtPlanSubscription.Rows[0]["StaorageSpace"].ToString();
                    txtBalanceSize.Text = Math.Round(decimal.Parse(dtPlanSubscription.Rows[0]["AS_StorageBalance"].ToString()), 2).ToString();
                    //if (int.Parse(dtPlanSubscription.Rows[0]["WP_IsMultiBranchPlan"].ToString()) == 1)
                    //    txtNoOfBranches.Text = dtPlanSubscription.Rows[0]["NoOfBranches"].ToString();
                    //else
                    //{
                    //    txtNoOfBranches.Text = "0";
                    //    lblNoOfBranches.Visible = false;
                    //    txtNoOfBranches.Visible = false;
                    //}
                    if (!string.IsNullOrEmpty(dtPlanSubscription.Rows[0]["NoOfBranches"].ToString()))
                        txtNoOfBranches.Text = dtPlanSubscription.Rows[0]["NoOfBranches"].ToString();
                    txtNoOfCustomerLogins.Text = dtPlanSubscription.Rows[0]["customerWebLogins"].ToString();
                    if (!string.IsNullOrEmpty(dtPlanSubscription.Rows[0]["StaffWebLogins"].ToString()))
                        txtNoOfStaffLogins.Text = dtPlanSubscription.Rows[0]["StaffWebLogins"].ToString();
                    //if (int.Parse(dtPlanSubscription.Rows[0]["WP_IsOtherStaffEnabled"].ToString()) == 1)
                    //    txtNoOfStaffLogins.Text = dtPlanSubscription.Rows[0]["StaffWebLogins"].ToString();
                    //else
                    //{
                    //    txtNoOfStaffLogins.Text = dtPlanSubscription.Rows[0]["StaffWebLogins"].ToString();
                    //    lblNoOfStaffLogins.Visible = false;
                    //    txtNoOfStaffLogins.Visible = false;
                    //}
                   
                    txtSMSBought.Text = dtPlanSubscription.Rows[0]["AS_SMSLicences"].ToString();
                    if (!string.IsNullOrEmpty(dtPlanSubscription.Rows[0]["AS_SubscriptionStartDate"].ToString()))
                    {
                        dpStartDate.SelectedDate = DateTime.Parse(dtPlanSubscription.Rows[0]["AS_SubscriptionStartDate"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dtPlanSubscription.Rows[0]["AS_TrialEndDate"].ToString()))
                    {
                        dpTrialEndDate.SelectedDate = DateTime.Parse(dtPlanSubscription.Rows[0]["AS_TrialEndDate"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dtPlanSubscription.Rows[0]["AS_TrialStartDate"].ToString()))
                    {
                        dpTrialStartDate.SelectedDate = DateTime.Parse(dtPlanSubscription.Rows[0]["AS_TrialStartDate"].ToString());
                    }
                    ddlPlan.SelectedValue = dtPlanSubscription.Rows[0]["WP_PlanId"].ToString();

                    if (ddlPlan.SelectedValue == "1")
                    {
                        chkModules.Items[0].Selected = true;
                        chkModules.Items[1].Selected = true;
                        chkModules.Items[3].Selected = true;
                        chkModules.Items[5].Selected = true;
                        chkModules.Items[4].Selected = true;
                    }
                    else if (ddlPlan.SelectedValue == "2")
                    {
                        chkModules.Items[0].Selected = true;
                        chkModules.Items[1].Selected = true;
                        chkModules.Items[3].Selected = true;
                        chkModules.Items[5].Selected = true;
                        chkModules.Items[8].Selected = true;
                        chkModules.Items[4].Selected = true;
                        chkModules.Items[11].Selected = true;
                        chkModules.Items[2].Selected = true;
                        chkModules.Items[7].Selected = true;
                        chkModules.Items[10].Selected = true;
                        chkModules.Items[13].Selected = true;
                        chkValueAdds.Items[0].Selected = true;
                    }
                    else if (ddlPlan.SelectedValue == "3")
                    {
                        chkModules.Items[0].Selected = true;
                        chkModules.Items[1].Selected = true;
                        chkModules.Items[3].Selected = true;
                        chkModules.Items[5].Selected = true;
                        chkModules.Items[8].Selected = true;
                        chkModules.Items[4].Selected = true;
                        chkModules.Items[11].Selected = true;
                        chkModules.Items[2].Selected = true;
                        chkModules.Items[7].Selected = true;
                        chkModules.Items[10].Selected = true;
                        chkModules.Items[13].Selected = true;
                        chkValueAdds.Items[0].Selected = true;
                    }
                    else if (ddlPlan.SelectedValue == "4")
                    {
                        chkModules.Items[0].Selected = true;
                        chkModules.Items[1].Selected = true;
                        chkModules.Items[3].Selected = true;
                        chkModules.Items[5].Selected = true;
                        chkModules.Items[8].Selected = true;
                        chkModules.Items[4].Selected = true;
                        chkModules.Items[11].Selected = true;
                        chkModules.Items[2].Selected = true;
                        chkModules.Items[7].Selected = true;
                        chkModules.Items[10].Selected = true;
                        chkModules.Items[13].Selected = true;
                        chkModules.Items[12].Selected = true;
                        chkModules.Items[14].Selected = true;
                        chkModules.Items[15].Selected = true;
                        chkValueAdds.Items[0].Selected = true;
                    }
                    else if (ddlPlan.SelectedValue == "5")
                    {
                        chkModules.Items[0].Selected = true;
                        chkModules.Items[1].Selected = true;
                        chkModules.Items[2].Selected = true;
                        chkModules.Items[3].Selected = true;
                        chkModules.Items[4].Selected = true;
                        chkModules.Items[5].Selected = true;
                        chkModules.Items[7].Selected = true;
                        chkModules.Items[8].Selected = true;
                        chkModules.Items[9].Selected = true;
                        chkModules.Items[10].Selected = true;
                        chkModules.Items[11].Selected = true;
                        chkModules.Items[12].Selected = true;
                        chkModules.Items[13].Selected = true;
                        chkModules.Items[14].Selected = true;
                        chkModules.Items[15].Selected = true;
                        chkValueAdds.Items[0].Selected = true;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdviserSubscriptionVo _advisersubscriptionvo = new AdviserSubscriptionVo();
            AdviserSubscriptionBo _advisersubscriptionbo = new AdviserSubscriptionBo();
            //UserVo uservo = (UserVo)Session["iffUserVo"];          
          
            //_advisersubscriptionvo.AdviserId
            int _planid=0;
            DateTime _endDate;
            DateTime _startDate;
            DateTime _trialStartDate;
            DateTime _trialEndDate;
            int _noofbranches;
            int _noofstafflogins;
            int _noofcustomerlogins;
            int _smsBought;
            int _smsRemaining;
            int _smsSentTillDate;
            int _subscriptionId;
            float _vaultBalanceSize;
            float _vaultPaidSize;
            float _vaultDefaultSize;

           // txtBalanceSize.Attributes.Add("readonly", "readonly");
           // txtBalanceSize.Text = Server.HtmlEncode(Request.Form[txtBalanceSize.UniqueID]);

           // txtBalanceSize.Attributes.Add("disabled", "false");
            try
            {
                if (Page.IsValid)
                {
                    if (advisorVo != null)
                    {
                        _advisersubscriptionvo.AdviserId = advisorVo.advisorId;
                        //if (int.TryParse(ddlPlan.SelectedItem.Value, out _planid))
                        //{
                        //    _advisersubscriptionvo.PlanId = _planid;
                        //}
                        //-----------old code
                        //if (ddlFlavourCategory.SelectedValue != "Select")
                        //{
                        //    _advisersubscriptionvo.FlavourCategory = ddlFlavourCategory.SelectedValue;
                        //}
                        if (ddlPlan.SelectedIndex != 0)
                            _advisersubscriptionvo.PlanId = int.Parse(ddlPlan.SelectedValue);
                        if (!string.IsNullOrEmpty(txtComment.Text))
                        {
                            _advisersubscriptionvo.Comments = txtComment.Text;
                        }

                        if (DateTime.TryParse(dpEndDate.SelectedDate.ToString(), out _endDate))
                        {
                            _advisersubscriptionvo.EndDate = _endDate;
                        }
                        if (DateTime.TryParse(dpStartDate.SelectedDate.ToString(), out _startDate))
                        {
                            _advisersubscriptionvo.StartDate = _startDate;
                        }
                        if (DateTime.TryParse(dpTrialStartDate.SelectedDate.ToString(), out _trialStartDate))
                        {
                            _advisersubscriptionvo.TrialStartDate = _trialStartDate;
                        }
                        if (DateTime.TryParse(dpTrialEndDate.SelectedDate.ToString(), out _trialEndDate))
                        {
                            _advisersubscriptionvo.TrialEndDate = _trialEndDate;
                        }
                        if (int.TryParse(txtNoOfBranches.Text, out _noofbranches))
                        {
                            _advisersubscriptionvo.NoOfBranches = _noofbranches;
                        }
                        if (int.TryParse(txtNoOfStaffLogins.Text, out _noofstafflogins))
                        {
                            _advisersubscriptionvo.NoOfStaffLogins = _noofstafflogins;
                        }
                        if (int.TryParse(txtNoOfCustomerLogins.Text, out _noofcustomerlogins))
                        {
                            _advisersubscriptionvo.NoOfCustomerLogins = _noofcustomerlogins;
                        }

                        if (float.TryParse(txtPaidSize.Text, out _vaultPaidSize))
                        {
                            _advisersubscriptionvo.StorageSize = _vaultPaidSize;
                        }
                        //if (float.TryParse(txtBalanceSize.Text, out _vaultBalanceSize))
                        //{
                        //    _advisersubscriptionvo.StorageBalance = _vaultBalanceSize;
                        //}
                        if (float.TryParse(txtDefaultStorage.Text, out _vaultDefaultSize))
                        {
                            _advisersubscriptionvo.StorageDefaultSize = _vaultDefaultSize;
                        }

                        if (hdnStorageUsed.Value == null || hdnStorageUsed.Value == "")
                        {
                            hdnStorageUsed.Value = "0";

                        }
                        _vaultBalanceSize = _vaultPaidSize + _vaultDefaultSize - float.Parse(hdnStorageUsed.Value);

                        if (_vaultBalanceSize >= 0)
                        {
                            _advisersubscriptionvo.StorageBalance = _vaultBalanceSize;

                            if (int.TryParse(txtSMSBought.Text, out _smsBought))
                            {
                                _advisersubscriptionvo.SmsBought = _smsBought;
                            }

                            for (int i = 0; i < chkModules.Items.Count; i++)
                            {
                                ListItem _limodules = (ListItem)chkModules.Items[i];
                                if (_limodules.Selected == true)
                                {
                                    _advisersubscriptionvo.CustomPlanSelection += _limodules.Value + ",";
                                }
                            }
                            if (IFAuserVo != null)
                            {
                                _subscriptionId = _advisersubscriptionbo.CreateAdviserSubscription(_advisersubscriptionvo, IFAuserVo.UserId);
                                string flavourIds = GetFlavourIds();
                                _advisersubscriptionbo.SetFlavoursToAdviser(flavourIds, advisorVo.advisorId);
                                SettingsSavedMessage.Visible = true;
                                //SetAdviserFlavourSubscription();
                                if (IFAuserVo.IsTempPassword == 1)
                                {
                                    if (chkMailSend.Checked == true)
                                    {
                                        string hassedPassword = string.Empty;
                                        string saltValue = string.Empty;
                                        string password = r.Next(20000, 100000).ToString();
                                        encryption.GetHashAndSaltString(password, out hassedPassword, out saltValue);
                                        IFAuserVo.Password = hassedPassword;
                                        IFAuserVo.PasswordSaltValue = saltValue;
                                        IFAuserVo.OriginalPassword = password;
                                        _advisersubscriptionbo.UpdateUserPasswordInDatabase(hassedPassword, saltValue, IFAuserVo.UserId);
                                        SendMail(IFAuserVo);
                                    }
                                }
                                if (Cache["AdminLeftTreeNode" + advisorVo.advisorId.ToString()] != null)
                                {
                                    Cache.Remove("AdminLeftTreeNode" + advisorVo.advisorId.ToString());
                                }
                            }
                            //SetAdviserFlavourSubscription();
                        }

                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('You don't have enough amount');", true);
                        //    return;
                        //}
                    }
                }

               // txtBalanceSize.Attributes.Add("disabled", "true");

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", @"alert('Something Went Wrong \n Record Status: Unsuccessful');", true);
            }
        }


        private bool SendMail(UserVo userVo)
        {
            Emailer emailer = new Emailer();
            EmailMessage email = new EmailMessage();
            //userVo = (UserVo)Session["iffUserVo"];
            bool isMailSent = false;
            try
            {
                email.To.Add(userVo.Email);
                string name = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;
                email.GetAdviserRegistrationMail(userVo.LoginId, userVo.OriginalPassword, name);
                isMailSent = emailer.SendMail(email);

                //Send a notification mail to Wealth ERP team.
                EmailMessage notificationMail = new EmailMessage();
                notificationMail.GetAdviserRegistrationMailNotification(advisorVo.OrganizationName, advisorVo.City, advisorVo.MobileNumber, userVo.LoginId, userVo.Email, name);
                //notificationMail.GetAdviserRegistrationMail(userVo.LoginId, userVo.Password, name);

                emailer.SendMail(notificationMail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isMailSent;
        }

        private string GetFlavourIds()
        {
            string flavourIds="";
            int i =0;
            for (i = 0; i < chkModules.Items.Count; i++)
            {
                if(chkModules.Items[i].Selected == true)
                    {
                        flavourIds += chkModules.Items[i].Value + "~";
                    }
            }
            for (i = 0; i < chkValueAdds.Items.Count; i++)
            {
                if (chkValueAdds.Items[i].Selected == true)
                {
                    flavourIds += chkValueAdds.Items[i].Value + "~";    
                }
            }
          return flavourIds;
        }
        //public void SelectCheckList(int _planid)
        //{
        //    DataSet _dsSelectCheckList;
        //    AdviserSubscriptionBo _advisersubscriptionbo = new AdviserSubscriptionBo();
        //    _dsSelectCheckList = _advisersubscriptionbo.GetWerpPlanFlavours(_planid);
        //    DataSet _tempSubscriptionDetails = (DataSet)Session["SubscriptionDetails"];
        //    for (int k = 0; k < chkModules.Items.Count; k++)
        //    {
        //        if (chkModules.Items[k].Selected == true)
        //        {
        //            chkModules.Items[k].Selected = false;
        //        }
        //    }
        //    if (_planid != 3)
        //    {
        //        if (_dsSelectCheckList != null && _dsSelectCheckList.Tables[0].Rows.Count > 0)
        //        {
        //            for (int i = 0; i < _dsSelectCheckList.Tables[0].Rows.Count; i++)
        //            {
        //                for (int j = 0; j < chkModules.Items.Count; j++)
        //                {
        //                    if (chkModules.Items[j].Value == _dsSelectCheckList.Tables[0].Rows[i]["WF_FlavourId"].ToString())
        //                    {
        //                        chkModules.Items[j].Selected = true;
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    else if (_planid == 3)
        //    {
        //        if (_tempSubscriptionDetails != null && _tempSubscriptionDetails.Tables[1].Rows.Count > 0)
        //        {
        //            for (int i = 0; i < _tempSubscriptionDetails.Tables[1].Rows.Count; i++)
        //            {
        //                for (int j = 0; j < chkModules.Items.Count; j++)
        //                {
        //                    if (chkModules.Items[j].Value == _tempSubscriptionDetails.Tables[1].Rows[i]["WF_FlavourId"].ToString())
        //                    {
        //                        chkModules.Items[j].Selected = true;
        //                    }
        //                }
        //                //chkModules.SelectedItem.Value = _dsGetSubscriptionDetails.Tables[1].Rows[i]["WF_FlavourId"].ToString();
        //            }
        //        }
        //    }
          
        //    if (_planid != 3)
        //    {
        //        chkModules.Enabled = false;
        //    }
        //    else
        //    {
        //        chkModules.Enabled = true;
        //    }
        //}

        //----------------------- old code--------------------------
        //protected void ddlFlavourCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList _ddl = (DropDownList)sender;
        //    if (_ddl.SelectedItem.Value == "AD")
        //    {
        //        chkModules.Enabled = true;
        //        chkModules.Items[0].Enabled = false;
        //        chkModules.Items[1].Enabled = false;
        //        chkModules.Items[2].Enabled = true;
        //        chkModules.Items[3].Enabled = false;
        //        chkModules.Items[4].Enabled = false;
        //        chkModules.Items[5].Enabled = false;
        //       // chkModules.Items[6].Enabled = true;
        //        chkModules.Items[7].Enabled = true;
        //        chkModules.Items[8].Enabled = true;

        //       // chkModules.Items[0].Selected = false;
        //       // chkModules.Items[1].Selected = false;
        //       // chkModules.Items[2].Selected = true;
        //       // chkModules.Items[3].Selected = false;
        //       // chkModules.Items[4].Selected = false;
        //       // chkModules.Items[5].Selected = false;
        //       //// chkModules.Items[6].Selected = true;
        //       // chkModules.Items[7].Selected = true;
        //       // chkModules.Items[8].Selected = true;
                
                
                
        //        //Plan 500
        //        //SelectCheckList(1);
        //    }
        //    else if (_ddl.SelectedItem.Value == "DT")
        //    {
        //        chkModules.Enabled = true;
        //        chkModules.Items[0].Enabled = true;
        //        chkModules.Items[1].Enabled = true;
        //        chkModules.Items[2].Enabled = false;
        //        chkModules.Items[3].Enabled = true;
        //        chkModules.Items[4].Enabled = true;
        //        chkModules.Items[5].Enabled = true;
        //      //  chkModules.Items[6].Enabled = true;
        //        chkModules.Items[7].Enabled = false;
        //        chkModules.Items[8].Enabled = false;

        //        //chkModules.Items[0].Selected = true;              
        //        //chkModules.Items[1].Selected = true;
        //        //chkModules.Items[2].Selected = false;
        //        //chkModules.Items[3].Selected = true;
        //        //chkModules.Items[4].Selected = true;
        //        //chkModules.Items[5].Selected = true;
        //        ////chkModules.Items[6].Selected = true;
        //        //chkModules.Items[7].Selected = false;
        //        //chkModules.Items[8].Selected = false;
        //        //plan 1000
        //       // SelectCheckList(2);
        //    }
        //    else if (_ddl.SelectedItem.Value == "VA")
        //    {
        //        chkModules.Enabled = false;
        //        //chkModules.Items[0].Selected = false;
        //        //chkModules.Items[1].Selected = false;
        //        //chkModules.Items[2].Selected = false;
        //        //chkModules.Items[3].Selected = false;
        //        //chkModules.Items[4].Selected = false;
        //        //chkModules.Items[5].Selected = false;
        //        ////chkModules.Items[6].Selected = false;
        //        //chkModules.Items[7].Selected = false;
        //        //chkModules.Items[8].Selected = false;
        //        //Custom Plan
        //      //  SelectCheckList(3);

        //    }
        //    else if (_ddl.SelectedItem.Value == "Select")
        //    {
        //        chkModules.Enabled = false;
        //        //chkModules.Items[0].Selected = false;
        //        //chkModules.Items[1].Selected = false;
        //        //chkModules.Items[2].Selected = false;
        //        //chkModules.Items[3].Selected = false;
        //        //chkModules.Items[4].Selected = false;
        //        //chkModules.Items[5].Selected = false;
        //        ////chkModules.Items[6].Selected = false;
        //        //chkModules.Items[7].Selected = false;
        //        //chkModules.Items[8].Selected = false;
        //    }
        //    else if (_ddl.SelectedItem.Value == "AL")
        //    {
        //        chkModules.Enabled = true;
        //        chkModules.Items[0].Enabled = true;
        //        chkModules.Items[1].Enabled = true;
        //        chkModules.Items[2].Enabled = true;
        //        chkModules.Items[3].Enabled = true;
        //        chkModules.Items[4].Enabled = true;
        //        chkModules.Items[5].Enabled = true;
        //       // chkModules.Items[6].Enabled = true;
        //        chkModules.Items[7].Enabled = true;
        //        chkModules.Items[8].Enabled = true;

        //      //  chkModules.Items[0].Selected = false;
        //      //  chkModules.Items[1].Selected = false;
        //      //  chkModules.Items[2].Selected = false;
        //      //  chkModules.Items[3].Selected = false;
        //      //  chkModules.Items[4].Selected = false;
        //      //  chkModules.Items[5].Selected = false;
        //      ////  chkModules.Items[6].Selected = false;
        //      //  chkModules.Items[7].Selected = false;
        //      //  chkModules.Items[8].Selected = false;
        //    }

        //    DataSet dsFlavourDetails = new DataSet();

        //    dsFlavourDetails = (DataSet)Session["SubscriptionDetails"];

        //    int flavourId;

        //    chkModules.Items[0].Selected = false;
        //    chkModules.Items[1].Selected = false;
        //    chkModules.Items[2].Selected = false;
        //    chkModules.Items[3].Selected = false;
        //    chkModules.Items[4].Selected = false;
        //    chkModules.Items[5].Selected = false;
        //    //  chkModules.Items[6].Selected = false;
        //    chkModules.Items[7].Selected = false;
        //    chkModules.Items[8].Selected = false;

        //    chkValueAdds.Items[0].Selected = false;
        //    chkValueAdds.Items[1].Selected = false;
        //    chkValueAdds.Items[2].Selected = false;

        //    string flavourCategory="";
        //    if (dsFlavourDetails != null)
        //    {
        //        if (dsFlavourDetails.Tables[0].Rows.Count > 0)
        //        {
        //            flavourCategory = dsFlavourDetails.Tables[0].Rows[0]["WFC_FlavourCategoryCode"].ToString();
        //        }
        //        if (dsFlavourDetails.Tables[1].Rows.Count > 0)
        //        {
        //            if (flavourCategory == _ddl.SelectedItem.Value)
        //            {
        //                for (int i = 0; i < dsFlavourDetails.Tables[1].Rows.Count; i++)
        //                {
        //                    flavourId = Convert.ToInt32(dsFlavourDetails.Tables[1].Rows[i]["WF_FlavourId"].ToString());
        //                    if (flavourId < 10)
        //                    {
        //                        chkModules.Items[flavourId - 1].Selected = true;
        //                    }
        //                    else
        //                    {
        //                        if (chkValueAdds.Items[0].Value == flavourId.ToString())
        //                        {
        //                            chkValueAdds.Items[0].Selected = true;
        //                        }
        //                        if (chkValueAdds.Items[1].Value == flavourId.ToString())
        //                        {
        //                            chkValueAdds.Items[1].Selected = true;
        //                        }
        //                        if (chkValueAdds.Items[2].Value == flavourId.ToString())
        //                        {
        //                            chkValueAdds.Items[2].Selected = true;
        //                        }

        //                    }
        //                }
        //            }

        //        }

        //    }
        //}

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            if (Cache["AdminLeftTreeNode" + advisorVo.advisorId.ToString()] != null)
            {
                Cache.Remove("AdminLeftTreeNode" + advisorVo.advisorId.ToString());
            }
        }

        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtPlan = new DataTable(); ;
            DataRow[] drPlan;
            int isNoBranchs=0; int isNooStaffs=0; int customerLogins=0;
            int noOfStaffWebLogins = 0; int noOfBranches = 0; 
            int storageSize = 0;

            if (Session["PlanSubscriptionFlavourDetails"] != null)
            {
                dsGetPlanSubscriptionFlavourDetails = (DataSet)Session["PlanSubscriptionFlavourDetails"];
                dtPlan = dsGetPlanSubscriptionFlavourDetails.Tables[0];
                drPlan = dtPlan.Select("WP_PlanId=" + ddlPlan.SelectedValue);
                if (drPlan.Count() > 0)
                {
                    foreach (DataRow dr in drPlan)
                    {
                        isNoBranchs = int.Parse(dr["WP_IsMultiBranchPlan"].ToString());
                        isNooStaffs = int.Parse(dr["WP_IsOtherStaffEnabled"].ToString());
                        if (!string.IsNullOrEmpty(dr["WP_NoOfCustomerWebLogins"].ToString().Trim()))
                            customerLogins = int.Parse(dr["WP_NoOfCustomerWebLogins"].ToString());
                        noOfStaffWebLogins = int.Parse(dr["WP_NoOfStaffWebLogins"].ToString());
                        if (!string.IsNullOrEmpty(dr["WP_NoOfBranches"].ToString().Trim()))
                            noOfBranches = int.Parse(dr["WP_NoOfBranches"].ToString());
                        storageSize = int.Parse(dr["WP_Proofstaoragespace"].ToString());
                    }
                    txtNoOfBranches.Text = noOfBranches.ToString();
                    txtNoOfStaffLogins.Text = noOfStaffWebLogins.ToString();
                    //if (isNoBranchs == 1)
                    //{
                    //    lblNoOfBranches.Visible = true;
                    //    txtNoOfBranches.Visible = true;
                    //    txtNoOfBranches.Text = noOfBranches.ToString();
                    //}
                    //else
                    //{
                    //    lblNoOfBranches.Visible = false;
                    //    txtNoOfBranches.Visible = false;
                    //}
                    //if (isNooStaffs == 1)
                    //{
                    //    txtNoOfStaffLogins.Text = noOfStaffWebLogins.ToString();
                    //    lblNoOfStaffLogins.Visible = true;
                    //    txtNoOfStaffLogins.Visible = true;
                    //}
                    //else
                    //{
                    //    lblNoOfStaffLogins.Visible = false;
                    //    txtNoOfStaffLogins.Visible = false;
                    //}
                    txtNoOfCustomerLogins.Text = customerLogins.ToString();
                    txtDefaultStorage.Text = storageSize.ToString();
                }
              
            }
            if (ddlPlan.SelectedValue == "1")
            {
                chkModules.Items[0].Selected= true;
                chkModules.Items[1].Selected = true;
                chkModules.Items[3].Selected = true;
                chkModules.Items[5].Selected = true;
                chkModules.Items[4].Selected = true;

                chkModules.Items[2].Selected = false;
                chkModules.Items[7].Selected = false;
                chkModules.Items[8].Selected = false;
                chkModules.Items[9].Selected = false;
                chkModules.Items[10].Selected = false;
                chkModules.Items[11].Selected = false;
                chkModules.Items[12].Selected = false;
                chkModules.Items[13].Selected = false;
                chkModules.Items[14].Selected = false;
                chkValueAdds.Items[0].Selected=true;

             
                
            }
            else if (ddlPlan.SelectedValue == "2")
            {
                chkModules.Items[0].Selected = true;
                chkModules.Items[1].Selected = true;
                chkModules.Items[3].Selected = true;
                chkModules.Items[5].Selected = true;
                chkModules.Items[8].Selected = true;
                chkModules.Items[4].Selected = true;
                chkModules.Items[11].Selected = true;
                chkModules.Items[2].Selected = true;
                chkModules.Items[7].Selected = true;
                chkModules.Items[10].Selected = true;
                chkModules.Items[13].Selected = true;

                chkModules.Items[9].Selected = false;
                chkModules.Items[12].Selected = false;
                chkModules.Items[14].Selected = false;
                chkValueAdds.Items[0].Selected = true;
            }
            else if (ddlPlan.SelectedValue == "3")
            {
                chkModules.Items[0].Selected = true;
                chkModules.Items[1].Selected = true;
                chkModules.Items[3].Selected = true;
                chkModules.Items[5].Selected = true;
                chkModules.Items[8].Selected = true;
                chkModules.Items[4].Selected = true;
                chkModules.Items[11].Selected = true;
                chkModules.Items[2].Selected = true;
                chkModules.Items[7].Selected = true;
                chkModules.Items[10].Selected = true;
                chkModules.Items[13].Selected = true;

                chkModules.Items[9].Selected = false;
                chkModules.Items[12].Selected = false;
                chkModules.Items[14].Selected = false;
                chkValueAdds.Items[0].Selected = true;
            }
            else if (ddlPlan.SelectedValue == "4")
            {
                chkModules.Items[0].Selected = true;
                chkModules.Items[1].Selected = true;
                chkModules.Items[3].Selected = true;
                chkModules.Items[5].Selected = true;
                chkModules.Items[8].Selected = true;
                chkModules.Items[4].Selected = true;
                chkModules.Items[11].Selected = true;
                chkModules.Items[2].Selected = true;
                chkModules.Items[7].Selected = true;
                chkModules.Items[10].Selected = true;
                chkModules.Items[13].Selected = true;
                chkModules.Items[12].Selected = true;
                chkModules.Items[14].Selected = true;
                chkModules.Items[15].Selected = true;

                chkModules.Items[9].Selected = false;
                chkModules.Items[12].Selected = false;
                chkModules.Items[14].Selected = false;
                chkValueAdds.Items[0].Selected = true;
            }
            else if (ddlPlan.SelectedValue == "5" )
            {
                chkModules.Items[0].Selected = true;
                chkModules.Items[1].Selected = true;
                chkModules.Items[2].Selected = true;
                chkModules.Items[3].Selected = true;
                chkModules.Items[4].Selected = true;
                chkModules.Items[5].Selected = true;
                chkModules.Items[7].Selected = true;
                chkModules.Items[8].Selected = true;
                chkModules.Items[9].Selected = true;
                chkModules.Items[10].Selected = true;
                chkModules.Items[11].Selected = true;
                chkModules.Items[12].Selected = true;
                chkModules.Items[13].Selected = true;
                chkModules.Items[14].Selected = true;
                chkModules.Items[15].Selected = true;
                chkValueAdds.Items[0].Selected = true;
            }
        }
        //[System.Web.Services.WebMethod(EnableSession = true)]
        //public static void AjaxSetBalanceStorage(string storageBalance)
        //{
        //    try
        //    {
        //        HttpContext.Current.Session["storageBalance"] = storageBalance;
        //        //HttpContext.Current.Session["Sessionkey"] = key;
        //        //HttpContext.Current.Session["Sessionvalue"] = value;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
    }
}