﻿using System;
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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('SuperAdminLeftPane');", true);
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