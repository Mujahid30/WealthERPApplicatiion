using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using WealthERP.Base;

using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;


namespace WealthERP.SuperAdmin
{
    public partial class SuperAdminLeftPane : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            Session["LoanSchemeView"] = "SuperAdmin";
            customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
            if (!IsPostBack)
            {
                RadPanelBar1.CollapseAllItems();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('SuperAdminLeftTopMenu');", true);
            }
        }




        protected void RadPanelBar1_ItemClick(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
        {
            string strNodeValue = null;
            try
            {

                if (e.Item.Value == "IFF")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFF','none');", true);
                }
                else if (e.Item.Value == "IFFAdd")
                {
                    Session["IFFAdd"] = "Add";
                    Session.Remove("advisorVo");
                    Session.Remove("IDs");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFFAdd','none');", true);
                }
                else if (e.Item.Value == "IFFUserManagement")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFFUserManagement','login')", true);
                }
                else if (e.Item.Value == "MessageBroadcast")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SuperAdminMessageBroadcast','login')", true);
                }

                else if (e.Item.Value == "LoanScheme")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('LoanSchemeView', 'none')", true);
                }
                else if (e.Item.Value == "AddLoanScheme")
                {
                    Session.Remove("LoanSchemeId");
                    Session.Remove("LoanSchemeViewStatus");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('LoanScheme', 'none')", true);
                }
                else if (e.Item.Value == "Configuration")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('SuperAdminConfiguration', 'none')", true);
                }
                else if (e.Item.Value == "Valuation_Monitor")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('DailyValuationMonitor', 'none')", true);
                }
                else if (e.Item.Value == "Manual_Valuation")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ManualValuation', 'none')", true);
                }                    
                else if (e.Item.Value == "Gold_Price_Monito")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PriceListMonitor', 'none')", true);
                }
                else if (e.Item.Value == "GoalFunding_Sync")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('SuperAdminGoalSIPFundingSync', 'none')", true);
                }
                else if (e.Item.Value == "IssueTracker")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewIssuseDetails', 'none')", true);
                }
                else if (e.Item.Value == "AddNewIssue")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddIssue', 'none')", true);
                }
                else if (e.Item.Value == "MsgCompose")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('MessageCompose', 'none')", true);
                }
                else if (e.Item.Value == "MsgInbox")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('MessageInbox', 'none')", true);
                }
                else if (e.Item.Value == "MsgOutbox")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('MessageOutbox', 'none')", true);
                }
                else if (e.Item.Value == "Uploads")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AdviserUploadsSA', 'none')", true);
                }
                else if (e.Item.Value == "Uploads_History")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewUploadHistorySA', 'none')", true);
                }
                else if (e.Item.Value == "View_Profile_Exceptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RejectedWERPProfileSA', 'none')", true);
                }
                else if (e.Item.Value == "View_Transactions_Exceptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RejectedMFTransactionStagingSA', 'none')", true);
                }
                else if (e.Item.Value == "View_MFFolio_Exceptions")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RejectedMFFolioStagingSA', 'none')", true);
                }
                else if (e.Item.Value == "View_EQ_TRADE_Account")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RejectedTradeAccountStagingSA', 'none')", true);
                }

                else if (e.Item.Value == "View_EQ_Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RejectedEquityTransactionStagingSA', 'none')", true);
                }

                else if (e.Item.Value == "View_Systematic_Transaction")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RejectedSystematicTransactionStagingSA', 'none')", true);
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

                FunctionInfo.Add("Method", "RMCustomerIndividualLeftPane.ascx.cs:SuperAdminTreeView_SelectedNodeChanged()");

                object[] objects = new object[1];

                objects[0] = strNodeValue;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }        
    }
}