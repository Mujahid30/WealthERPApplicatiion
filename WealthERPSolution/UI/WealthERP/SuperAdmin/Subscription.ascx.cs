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

namespace WealthERP.SuperAdmin
{
    public partial class Subscription : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AdvisorVo advisorVo = (AdvisorVo)Session["advisorVo"];

            AdviserSubscriptionBo _advisersubscriptionbo = new AdviserSubscriptionBo();
            AdviserSubscriptionVo _advisersubscriptionvo = new AdviserSubscriptionVo();
            DataSet _dsGetSubscriptionDetails;
            if (!IsPostBack)
            {
                BindPlanDropdown();
                if (advisorVo != null)
                {
                    _dsGetSubscriptionDetails = _advisersubscriptionbo.GetAdviserSubscriptionPlanDetails(advisorVo.advisorId);
                    if (_dsGetSubscriptionDetails != null && _dsGetSubscriptionDetails.Tables[0].Rows.Count>0)
                    {
                        txtComment.Text = _dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_Comments"].ToString();
                        if (!string.IsNullOrEmpty(_dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_SubscriptionEndDate"].ToString()))
                        {
                            dpEndDate.SelectedDate = DateTime.Parse(_dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_SubscriptionEndDate"].ToString());
                        }
                        txtNoOfBranches.Text = _dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_NoOfBranches"].ToString();
                        txtNoOfCustomerLogins.Text = _dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_NoOfCustomerWebLogins"].ToString();
                        txtNoOfStaffLogins.Text = _dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_NoOfStaffWebLogins"].ToString();
                        txtSMSBought.Text = _dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_SMSLicences"].ToString();
                        if (!string.IsNullOrEmpty(_dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_SubscriptionStartDate"].ToString()))
                        {
                            dpStartDate.SelectedDate = DateTime.Parse(_dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_SubscriptionStartDate"].ToString());
                        }
                        if (!string.IsNullOrEmpty(_dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_TrialEndDate"].ToString()))
                        {
                            dpTrialEndDate.SelectedDate = DateTime.Parse(_dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_TrialEndDate"].ToString());
                        }
                        if (!string.IsNullOrEmpty(_dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_TrialStartDate"].ToString()))
                        {
                            dpTrialStartDate.SelectedDate = DateTime.Parse(_dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_TrialStartDate"].ToString());
                        }
                        ddlPlan.SelectedValue = _dsGetSubscriptionDetails.Tables[0].Rows[0]["WP_PlanId"].ToString();
                        
                        if (_dsGetSubscriptionDetails.Tables[1] != null)
                        {
                            for (int i = 0; i < _dsGetSubscriptionDetails.Tables[1].Rows.Count; i++)
                            {
                                for (int j = 0; j < chkModules.Items.Count; j++)
                                {
                                    if (chkModules.Items[j].Value == _dsGetSubscriptionDetails.Tables[1].Rows[i]["WF_FlavourId"].ToString())
                                    {
                                        chkModules.Items[j].Selected = true;
                                    }
                                }
                                //chkModules.SelectedItem.Value = _dsGetSubscriptionDetails.Tables[1].Rows[i]["WF_FlavourId"].ToString();
                            }
                        }
                    }
                }               
                
            }
            if (ddlPlan.SelectedValue != "3")
            {
                chkModules.Enabled = false;
            }
            else
            {
                chkModules.Enabled = true;
            }
        }
        public void BindPlanDropdown()
        {
            DataSet _dsBindDropDown;
            AdviserSubscriptionBo _advisersubscriptionbo = new AdviserSubscriptionBo();
            _dsBindDropDown= _advisersubscriptionbo.GetWerpPlans();
            if (_dsBindDropDown != null && _dsBindDropDown.Tables[0].Rows.Count > 0)
            {
                ddlPlan.DataSource = _dsBindDropDown.Tables[0];
                ddlPlan.DataTextField = "WP_PlanName";
                ddlPlan.DataValueField = "WP_PlanId";                
                ddlPlan.DataBind();
                ddlPlan.Items.Insert(0, new ListItem("--Select--", "Select"));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdviserSubscriptionVo _advisersubscriptionvo = new AdviserSubscriptionVo();
            AdviserSubscriptionBo _advisersubscriptionbo = new AdviserSubscriptionBo();
            UserVo uservo=(UserVo)Session["UserVo"];
            AdvisorVo advisorVo = (AdvisorVo)Session["advisorVo"];
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
            try
            {
                if (Page.IsValid)
                {
                    if (advisorVo != null)
                    {
                        _advisersubscriptionvo.AdviserId = advisorVo.advisorId;
                        if (int.TryParse(ddlPlan.SelectedItem.Value, out _planid))
                        {
                            _advisersubscriptionvo.PlanId = _planid;
                        }
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
                        if (uservo != null)
                        {
                            _subscriptionId = _advisersubscriptionbo.CreateAdviserSubscription(_advisersubscriptionvo, uservo.UserId);
                            SettingsSavedMessage.Visible = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", @"alert('Something Went Wrong \n Record Status: Unsuccessful');", true);
            }
        }
        public void SelectCheckList(int _planid)
        {
            DataSet _dsSelectCheckList;
            AdviserSubscriptionBo _advisersubscriptionbo = new AdviserSubscriptionBo();
            _dsSelectCheckList = _advisersubscriptionbo.GetWerpPlanFlavours(_planid);
            for (int k = 0; k < chkModules.Items.Count; k++)
            {
                if (chkModules.Items[k].Selected == true)
                {
                    chkModules.Items[k].Selected = false;
                }
            }
            if (_dsSelectCheckList != null && _dsSelectCheckList.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < _dsSelectCheckList.Tables[0].Rows.Count; i++)
                {
                    chkModules.Items[int.Parse(_dsSelectCheckList.Tables[0].Rows[i]["WF_FlavourId"].ToString()) - 1].Selected = true;
                }
            }
            if (_planid != 3)
            {
                chkModules.Enabled = false;
            }
            else
            {
                chkModules.Enabled = true;
            }
        }
        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList _ddl = (DropDownList)sender;
            if (_ddl.SelectedItem.Value == "1")
            {
                //Plan 500
                SelectCheckList(1);
            }
            else if (_ddl.SelectedItem.Value == "2")
            {
                //plan 1000
                SelectCheckList(2);
            }
            else if (_ddl.SelectedItem.Value == "3")
            {
                //Custom Plan
                SelectCheckList(3);
            }
            else
            {
                chkModules.Enabled = false;
            }
        }
    }
}