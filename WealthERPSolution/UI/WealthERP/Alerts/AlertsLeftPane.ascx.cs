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

namespace WealthERP.Alerts
{
    public partial class AlertsLeftPane : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            string First = null;
            string Middle = null;
            string Last = null;

            try
            {
                if (!IsPostBack)
                {
                    customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                    First = customerVo.FirstName.ToString();
                    Middle = customerVo.MiddleName.ToString();
                    Last = customerVo.LastName.ToString();

                    if (customerVo.RelationShip == "SELF")
                    {
                        TreeView1.Nodes.AddAt(1, new TreeNode("Group Dashboard"));
                    }

                    if (Middle != "")
                    {
                        lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();
                    }
                    else
                    {
                        lblNameValue.Text = customerVo.FirstName.ToString() + " " + customerVo.LastName.ToString();
                    }

                    lblEmailIdValue.Text = customerVo.Email.ToString();
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

                FunctionInfo.Add("Method", "RMCustomerIndividualLeftPane.ascx.cs:Page_Load()");

                object[] objects = new object[4];

                objects[0] = customerVo;
                objects[1] = First;
                objects[2] = Middle;
                objects[3] = Last;



                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
          
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
              if (TreeView1.SelectedNode.Value == "Advisor Home")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "pageloadscript", "loadcontrol('IFAAdminMainDashboard','login');", true);
            }
              if (TreeView1.SelectedNode.Value == "RM Home")
              {
                  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMDashBoard','none');", true);
              }
              else if (TreeView1.SelectedNode.Value == "Group Dashboard")
              {
                  Session["IsDashboard"] = "true";
                  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AdvisorRMCustGroupDashboard','none');", true);
              }
              else if (TreeView1.SelectedNode.Value == "Customer Dashboard")
              {
                  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdvisorRMCustIndiDashboard','none');", true);
              }
              else if (TreeView1.SelectedNode.Value == "Profile Dashboard")
              {
                  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMCustomerIndividualDashboard','none');", true);
              }
              else if (TreeView1.SelectedNode.Value == "Portfolio Dashboard")
              {
                  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioDashboard','none');", true);
              }
              else if (TreeView1.SelectedNode.Value == "Alert Dashboard")
              {
                  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMAlertNotifications','none');", true);
              }
              else if (TreeView1.SelectedNode.Value == "View Notifications")
              {
                  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMAlertNotifications','none');", true);
              }
              else if (TreeView1.SelectedNode.Value == "MF Alerts")
              {
                  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerMFAlert','none');", true);
              }
              else if (TreeView1.SelectedNode.Value == "FI Alerts")
              {
                  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerFIAlerts','none');", true);
              }
              else if (TreeView1.SelectedNode.Value == "Insurance Alerts")
              {
                  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerInsuranceAlerts','none');", true);
              }
              else if (TreeView1.SelectedNode.Value == "Equity Alerts")
              {
                  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerEQAlerts','none');", true);
              }
              else if (TreeView1.SelectedNode.Value == "Liabilities Dashboard")
              {
                  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('LiabilityView', 'none')", true);
              }
        }
    }
}