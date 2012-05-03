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
            if (Request.QueryString["PageId"] != null)
                pageID = Request.QueryString["PageId"].ToString();
            if (pageID == "CustomerProofView")
            {
                customerImageProofPath = Request.QueryString["ImagePath"].ToString();
                customerProofExt = Request.QueryString["strExt"].ToString();
                customerProofFileName = Request.QueryString["strFileName"].ToString();
                strAdvisorIdWithFielName = Request.QueryString["strAdvisorIdWithFielName"].ToString();
            }
           
            path = Getpagepath(pageID);           
            UserControl uc1 = new UserControl();
            uc1 = (UserControl)this.Page.LoadControl(path);

            if (!string.IsNullOrEmpty(customerImageProofPath))
            {
                uc1.ID = "ctrl_" + pageID + "-" + customerImageProofPath + "-" + customerProofExt + "-" + customerProofFileName + "-" + strAdvisorIdWithFielName;
            }
            else
            {
                uc1.ID = "ctrl_" + pageID;
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

