using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;

namespace WealthERP
{
    public partial class PopUp : System.Web.UI.Page
    {
        string path;
        string pageID;
        string customerImageProofPath;
        string customerProofExt;
        string customerProofFileName;
        string strAdvisorIdWithFielName;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = Session["Theme"].ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string linkId = "";
            string AddMFFolioLinkId = "";
            string AddMFOrderEntryLinkId = "";
            string userType = string.Empty;
            string rmId = string.Empty;
            int OnlineSchemeSetupSchemecode = 0;

            if (Request.QueryString["PageId"] != null)
                pageID = Request.QueryString["PageId"].ToString();


            if (Request.QueryString["OnlineSchemeSetupSchemecode"] != null && Request.QueryString["OnlineSchemeSetupSchemecode"] !="")
            {
                OnlineSchemeSetupSchemecode = Convert.ToInt32(Request.QueryString["OnlineSchemeSetupSchemecode"]);
                Session["OnlineSchemeSetupSchemecode"] = OnlineSchemeSetupSchemecode;
            }
            
            if (Request.QueryString["LinkId"] != null)
            {
                linkId = Request.QueryString["LinkId"].ToString();
                Session["LinkAction"] = linkId;
            }

            if (Request.QueryString["userType"] != null)
            {
                userType = Request.QueryString["userType"].ToString();
            }
            if (Request.QueryString["rmId"] != null)
            {
                rmId = Request.QueryString["rmId"].ToString();
            }
            

            if (Request.QueryString["AddMFFolioLinkId"] != null)
            {
                AddMFFolioLinkId = Request.QueryString["AddMFFolioLinkId"].ToString();
                Session["AddMFFolioLinkIdLinkAction"] = AddMFFolioLinkId;
            }

            if (Request.QueryString["AddMFOrderEntryLinkId"] != null)
            {
                AddMFFolioLinkId = Request.QueryString["AddMFOrderEntryLinkId"].ToString();
                Session["AddMFOrderEntryLinkIdLinkAction"] = AddMFOrderEntryLinkId;
            }
            //if (pageID.Contains("AddBranchRMAgentAssociation"))
            //{
            //    string userType = Request.QueryString["userType"].ToString();
            //    pageID = "AddBranchRMAgentAssociation";
            //}
           
            path = Getpagepath(pageID.Trim());           
            UserControl uc1 = new UserControl();
            uc1 = (UserControl)this.Page.LoadControl(path);


            if (pageID.Trim() == "CustomerProofView")
            {
                customerImageProofPath = Request.QueryString["ImagePath"].ToString();
                customerProofExt = Request.QueryString["strExt"].ToString();
                customerProofFileName = Request.QueryString["strFileName"].ToString();
                strAdvisorIdWithFielName = Request.QueryString["strAdvisorIdWithFielName"].ToString();
            }
            else if (pageID.Trim() == "AddBankAccount")
            {
                if (Request.QueryString["action"] != null)
                {
                    string bankId = Request.QueryString["bankId"].ToString();
                    string action = Request.QueryString["action"].ToString();
                    uc1.ID = "ctrl_" + pageID.Trim() + "-" + bankId + "-" + action;
                }

            }
            else if (pageID.Trim() == "AddBranchRMAgentAssociation")
            {
                uc1.ID = "ctrl_" + pageID.Trim() + "-" + userType + "-" + rmId;
            }

            if (pageID.Trim() == "PayableStructureToAgentCategoryMapping")
            {
                if (Request.QueryString["ruleId"] != null)
                {
                    string ruleId = Request.QueryString["ruleId"].ToString();
                    string ID = Request.QueryString["ID"].ToString();
                    uc1.ID = "ctrl_" + pageID.Trim() + "-" + ruleId + "-" + ID;
                }

            }

           

            if (!string.IsNullOrEmpty(customerImageProofPath))
            {
                uc1.ID = "ctrl_" + pageID.Trim() + "-" + customerImageProofPath + "-" + customerProofExt + "-" + customerProofFileName + "-" + strAdvisorIdWithFielName;
            }
            else
            {
                uc1.ID = "ctrl_" + pageID.Trim();
            }

            phLoadControl.Controls.Clear();
            phLoadControl.Controls.Add(uc1);
        }

        protected string Getpagepath(string pageID)
        {
            ResourceManager resourceMessages = new ResourceManager("WealthERP.ControlMapping", typeof(ControlHost).Assembly);
            return resourceMessages.GetString(pageID);
        }

    }

}

