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
        DataSet _dsGetSubscriptionDetails;
        AdvisorVo advisorVo = new AdvisorVo();
        AdviserSubscriptionBo _advisersubscriptionbo = new AdviserSubscriptionBo();
        AdviserSubscriptionVo _advisersubscriptionvo = new AdviserSubscriptionVo();


        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
          //  txtBalanceSize.Attributes.Add("disabled", "true");
            
           

            //if (Session["storageBalance"] != "" && Session["storageBalance"] != null)
            //{
            //    txtBalanceSize.Text =Convert.ToString(Session["storageBalance"]);

            //}
           
            if (!IsPostBack)
            {
                // BindPlanDropdown();
                if (advisorVo != null)
                {
                    SetAdviserFlavourSubscription();
                }
                //if (ddlPlan.SelectedValue != "3")
                //{
                //    chkModules.Enabled = false;
                //}
                //else
                //{
                //    chkModules.Enabled = true;
                //}
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
            if (advisorVo != null)
            {
                _dsGetSubscriptionDetails = _advisersubscriptionbo.GetAdviserSubscriptionPlanDetails(advisorVo.advisorId);
                Session["SubscriptionDetails"] = _dsGetSubscriptionDetails;

                if (_dsGetSubscriptionDetails != null && _dsGetSubscriptionDetails.Tables[0].Rows.Count > 0)
                {
                    txtComment.Text = _dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_Comments"].ToString();
                    if (!string.IsNullOrEmpty(_dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_SubscriptionEndDate"].ToString()))
                    {
                        dpEndDate.SelectedDate = DateTime.Parse(_dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_SubscriptionEndDate"].ToString());
                    }
                    double storageBalance = 0;
                    double storagePaidSize = 0;
                    double storageSize = 0;
                    storageBalance = double.Parse(_dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_StorageBalance"].ToString());
                    storagePaidSize = double.Parse(_dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_PaidStorage"].ToString());
                    storageSize = double.Parse(_dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_StorageSize"].ToString());

                    txtPaidSize.Text = Convert.ToString(storagePaidSize);
                    hdnStorageUsed.Value = Convert.ToString(storageSize - storageBalance);
                    txtUsedSpace.Text = Convert.ToString(storageSize - storageBalance);
                    txtDefaultStorage.Text = _dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_DefaultStorage"].ToString();

                    txtBalanceSize.Text = Math.Round(decimal.Parse(_dsGetSubscriptionDetails.Tables[0].Rows[0]["AS_StorageBalance"].ToString()), 2).ToString();
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
                    ddlFlavourCategory.SelectedValue = _dsGetSubscriptionDetails.Tables[0].Rows[0]["WFC_FlavourCategoryCode"].ToString();

                    if (_dsGetSubscriptionDetails.Tables[1] != null)
                    {
                        //for (int i = 0; i < _dsGetSubscriptionDetails.Tables[1].Rows.Count; i++)
                        //{
                        //    for (int j = 0; j < chkModules.Items.Count; j++)
                        //    {
                        //        if (chkModules.Items[j].Value == _dsGetSubscriptionDetails.Tables[1].Rows[i]["WF_FlavourId"].ToString())
                        //        {
                        //            chkModules.Items[j].Selected = true;
                        //        }
                        //        
                        //    }
                        //    //chkModules.SelectedItem.Value = _dsGetSubscriptionDetails.Tables[1].Rows[i]["WF_FlavourId"].ToString();
                        //}
                        int flavourId;
                        for (int i = 0; i < _dsGetSubscriptionDetails.Tables[1].Rows.Count; i++)
                        {
                            flavourId = Convert.ToInt32(_dsGetSubscriptionDetails.Tables[1].Rows[i]["WF_FlavourId"].ToString());
                            if (flavourId < 10)
                            {
                                chkModules.Items[flavourId - 1].Selected = true;
                            }
                            else
                            {
                                if (flavourId == 10)
                                {
                                    chkValueAdds.Items[0].Selected = true;
                                }
                                if (flavourId == 11)
                                {
                                    chkValueAdds.Items[1].Selected = true;
                                }
                                if (flavourId == 12)
                                {
                                    chkValueAdds.Items[2].Selected = true;
                                }
                            }
                        }

                        //for (int j = 0; j < chkModules.Items.Count; j++)
                        //{
                        //    if (chkModules.Items[j].Selected == false)
                        //    {
                        //        chkModules.Items[j].Enabled = false;
                        //    }
                        //}
                        if (ddlFlavourCategory.SelectedValue == "AD")
                        {
                            chkModules.Enabled = true;
                            chkModules.Items[0].Enabled = false;
                            chkModules.Items[1].Enabled = false;
                            chkModules.Items[2].Enabled = true;
                            chkModules.Items[3].Enabled = false;
                            chkModules.Items[4].Enabled = false;
                            chkModules.Items[5].Enabled = false;
                            //   chkModules.Items[6].Enabled = true;
                            chkModules.Items[7].Enabled = true;
                            chkModules.Items[8].Enabled = true;
                        }
                        else if (ddlFlavourCategory.SelectedValue == "DT")
                        {
                            chkModules.Enabled = true;
                            chkModules.Items[0].Enabled = true;
                            chkModules.Items[1].Enabled = true;
                            chkModules.Items[2].Enabled = false;
                            chkModules.Items[3].Enabled = true;
                            chkModules.Items[4].Enabled = true;
                            chkModules.Items[5].Enabled = true;
                            // chkModules.Items[6].Enabled = true;
                            chkModules.Items[7].Enabled = false;
                            chkModules.Items[8].Enabled = false;

                            //plan 1000
                            // SelectCheckList(2);
                        }
                        else if (ddlFlavourCategory.SelectedItem.Value == "AL")
                        {
                            chkModules.Enabled = true;
                            chkModules.Items[0].Enabled = true;
                            chkModules.Items[1].Enabled = true;
                            chkModules.Items[2].Enabled = true;
                            chkModules.Items[3].Enabled = true;
                            chkModules.Items[4].Enabled = true;
                            chkModules.Items[5].Enabled = true;
                            //   chkModules.Items[6].Enabled = true;
                            chkModules.Items[7].Enabled = true;
                            chkModules.Items[8].Enabled = true;
                        }
                        else
                        {
                            chkModules.Enabled = false;
                            chkModules.Items[0].Selected = false;
                            chkModules.Items[1].Selected = false;
                            chkModules.Items[2].Selected = false;
                            chkModules.Items[3].Selected = false;
                            chkModules.Items[4].Selected = false;
                            chkModules.Items[5].Selected = false;
                            //  chkModules.Items[6].Selected = false;
                            chkModules.Items[7].Selected = false;
                            chkModules.Items[8].Selected = false;
                        }
                    }
                }
                else
                {
                    txtDefaultStorage.Text = "10";
                    txtBalanceSize.Text = "10";
                }

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
                        if (ddlFlavourCategory.SelectedValue != "Select")
                        {
                            _advisersubscriptionvo.FlavourCategory = ddlFlavourCategory.SelectedValue;
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
                            if (uservo != null)
                            {
                                _subscriptionId = _advisersubscriptionbo.CreateAdviserSubscription(_advisersubscriptionvo, uservo.UserId);
                                string flavourIds = GetFlavourIds();
                                _advisersubscriptionbo.SetFlavoursToAdviser(flavourIds, advisorVo.advisorId);
                                SettingsSavedMessage.Visible = true;
                                SetAdviserFlavourSubscription();
                            }
                            SetAdviserFlavourSubscription();
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
        protected void ddlFlavourCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList _ddl = (DropDownList)sender;
            if (_ddl.SelectedItem.Value == "AD")
            {
                chkModules.Enabled = true;
                chkModules.Items[0].Enabled = false;
                chkModules.Items[1].Enabled = false;
                chkModules.Items[2].Enabled = true;
                chkModules.Items[3].Enabled = false;
                chkModules.Items[4].Enabled = false;
                chkModules.Items[5].Enabled = false;
               // chkModules.Items[6].Enabled = true;
                chkModules.Items[7].Enabled = true;
                chkModules.Items[8].Enabled = true;

               // chkModules.Items[0].Selected = false;
               // chkModules.Items[1].Selected = false;
               // chkModules.Items[2].Selected = true;
               // chkModules.Items[3].Selected = false;
               // chkModules.Items[4].Selected = false;
               // chkModules.Items[5].Selected = false;
               //// chkModules.Items[6].Selected = true;
               // chkModules.Items[7].Selected = true;
               // chkModules.Items[8].Selected = true;
                
                
                
                //Plan 500
                //SelectCheckList(1);
            }
            else if (_ddl.SelectedItem.Value == "DT")
            {
                chkModules.Enabled = true;
                chkModules.Items[0].Enabled = true;
                chkModules.Items[1].Enabled = true;
                chkModules.Items[2].Enabled = false;
                chkModules.Items[3].Enabled = true;
                chkModules.Items[4].Enabled = true;
                chkModules.Items[5].Enabled = true;
              //  chkModules.Items[6].Enabled = true;
                chkModules.Items[7].Enabled = false;
                chkModules.Items[8].Enabled = false;

                //chkModules.Items[0].Selected = true;              
                //chkModules.Items[1].Selected = true;
                //chkModules.Items[2].Selected = false;
                //chkModules.Items[3].Selected = true;
                //chkModules.Items[4].Selected = true;
                //chkModules.Items[5].Selected = true;
                ////chkModules.Items[6].Selected = true;
                //chkModules.Items[7].Selected = false;
                //chkModules.Items[8].Selected = false;
                //plan 1000
               // SelectCheckList(2);
            }
            else if (_ddl.SelectedItem.Value == "VA")
            {
                chkModules.Enabled = false;
                //chkModules.Items[0].Selected = false;
                //chkModules.Items[1].Selected = false;
                //chkModules.Items[2].Selected = false;
                //chkModules.Items[3].Selected = false;
                //chkModules.Items[4].Selected = false;
                //chkModules.Items[5].Selected = false;
                ////chkModules.Items[6].Selected = false;
                //chkModules.Items[7].Selected = false;
                //chkModules.Items[8].Selected = false;
                //Custom Plan
              //  SelectCheckList(3);

            }
            else if (_ddl.SelectedItem.Value == "Select")
            {
                chkModules.Enabled = false;
                //chkModules.Items[0].Selected = false;
                //chkModules.Items[1].Selected = false;
                //chkModules.Items[2].Selected = false;
                //chkModules.Items[3].Selected = false;
                //chkModules.Items[4].Selected = false;
                //chkModules.Items[5].Selected = false;
                ////chkModules.Items[6].Selected = false;
                //chkModules.Items[7].Selected = false;
                //chkModules.Items[8].Selected = false;
            }
            else if (_ddl.SelectedItem.Value == "AL")
            {
                chkModules.Enabled = true;
                chkModules.Items[0].Enabled = true;
                chkModules.Items[1].Enabled = true;
                chkModules.Items[2].Enabled = true;
                chkModules.Items[3].Enabled = true;
                chkModules.Items[4].Enabled = true;
                chkModules.Items[5].Enabled = true;
               // chkModules.Items[6].Enabled = true;
                chkModules.Items[7].Enabled = true;
                chkModules.Items[8].Enabled = true;

              //  chkModules.Items[0].Selected = false;
              //  chkModules.Items[1].Selected = false;
              //  chkModules.Items[2].Selected = false;
              //  chkModules.Items[3].Selected = false;
              //  chkModules.Items[4].Selected = false;
              //  chkModules.Items[5].Selected = false;
              ////  chkModules.Items[6].Selected = false;
              //  chkModules.Items[7].Selected = false;
              //  chkModules.Items[8].Selected = false;
            }

            DataSet dsFlavourDetails = new DataSet();

            dsFlavourDetails = (DataSet)Session["SubscriptionDetails"];

            int flavourId;

            chkModules.Items[0].Selected = false;
            chkModules.Items[1].Selected = false;
            chkModules.Items[2].Selected = false;
            chkModules.Items[3].Selected = false;
            chkModules.Items[4].Selected = false;
            chkModules.Items[5].Selected = false;
            //  chkModules.Items[6].Selected = false;
            chkModules.Items[7].Selected = false;
            chkModules.Items[8].Selected = false;

            chkValueAdds.Items[0].Selected = false;
            chkValueAdds.Items[1].Selected = false;
            chkValueAdds.Items[2].Selected = false;

            string flavourCategory="";
            if (dsFlavourDetails != null)
            {
                if (dsFlavourDetails.Tables[0].Rows.Count > 0)
                {
                    flavourCategory = dsFlavourDetails.Tables[0].Rows[0]["WFC_FlavourCategoryCode"].ToString();
                }
                if (dsFlavourDetails.Tables[1].Rows.Count > 0)
                {
                    if (flavourCategory == _ddl.SelectedItem.Value)
                    {
                        for (int i = 0; i < dsFlavourDetails.Tables[1].Rows.Count; i++)
                        {
                            flavourId = Convert.ToInt32(dsFlavourDetails.Tables[1].Rows[i]["WF_FlavourId"].ToString());
                            if (flavourId < 10)
                            {
                                chkModules.Items[flavourId - 1].Selected = true;
                            }
                            else
                            {
                                if (chkValueAdds.Items[0].Value == flavourId.ToString())
                                {
                                    chkValueAdds.Items[0].Selected = true;
                                }
                                if (chkValueAdds.Items[1].Value == flavourId.ToString())
                                {
                                    chkValueAdds.Items[1].Selected = true;
                                }
                                if (chkValueAdds.Items[2].Value == flavourId.ToString())
                                {
                                    chkValueAdds.Items[2].Selected = true;
                                }

                            }
                        }
                    }

                }

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