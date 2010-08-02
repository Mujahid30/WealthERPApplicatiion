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
            //if (!IsPostBack)
            //{
            //    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
            //    string First = customerVo.FirstName.ToString();
            //    string Middle = customerVo.MiddleName.ToString();
            //    string Last = customerVo.LastName.ToString();

            //    if (Middle != "")
            //    {
            //        lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
            //    }
            //    else
            //    {
            //        lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.LastName.ToString();
            //    }

            //    lblEmailIdValue.Text = customerVo.Email.ToString();
            //}
            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
            if (!IsPostBack)
            {
                SuperAdminTreeView.CollapseAll();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('SuperAdminLeftPane');", true);
            }
        }

        protected void SuperAdminTreeView_SelectedNodeChanged(object sender, EventArgs e)
        {
            string strNodeValue = null;
            try
            {
                //if (SuperAdminTreeView.SelectedNode.Value == "SuperAdminHome")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UnderConstruction','login');", true);
                //}
                if (SuperAdminTreeView.SelectedNode.Value == "IFF")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFF','none');", true);
                }
                else if (SuperAdminTreeView.SelectedNode.Value == "IFFAdd")
                {
                    Session["IFFAdd"] = "Add";
                    Session.Remove("advisorVo");
                    Session.Remove("IDs");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFFAdd','none');", true);
                }
                else if (SuperAdminTreeView.SelectedNode.Value == "MessageBroadcast")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SuperAdminMessageBroadcast','login')", true);
                }
                //else if (SuperAdminTreeView.SelectedNode.Value == "MFMIS")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrolCustomer('UnderConstruction','none')", true);
                //}
                //else if (SuperAdminTreeView.SelectedNode.Value == "MF")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('UnderConstruction', 'none')", true);
                //}
                //else if (SuperAdminTreeView.SelectedNode.Value == "Equity")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('UnderConstruction', 'none')", true);
                //}
                //else if (SuperAdminTreeView.SelectedNode.Value == "Loan")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('UnderConstruction', 'none')", true);
                //}
                //else if (SuperAdminTreeView.SelectedNode.Value == "UserManagement")
                //{
                //    Session["UserManagement"] = "Advisor";
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMCustomerUserDetails','login');", true);
                //}
                else if (SuperAdminTreeView.SelectedNode.Value == "LoanScheme")
                {
                    Session["LoanSchemeView"] = "SuperAdmin";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrolCustomer('LoanSchemeView', 'none')", true);
                }
                else if (SuperAdminTreeView.SelectedNode.Value == "AddLoanScheme")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('LoanScheme', 'none')", true);
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