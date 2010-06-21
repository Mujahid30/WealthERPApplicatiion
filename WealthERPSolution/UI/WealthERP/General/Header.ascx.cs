using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WealthERP;
using WealthERP.Advisor;
using BoCommon;

namespace WealthERP.General
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionBo.CheckSession();
        }

        protected void LinkButtonSignIn_Click(object sender, EventArgs e)
        {
             Session["LastLoaded"] = "General//UserLogin.ascx";
             Session["LastLoadedID"]="ctrl_UserLogin";


             UserLogin Userlogin = new UserLogin();
             Userlogin = (UserLogin)this.Page.LoadControl("General//UserLogin.ascx");

             Userlogin.ID = "ctrl_UserLogin";

             UpdatePanel pmain = new UpdatePanel();
             pmain = (UpdatePanel)this.Parent.FindControl("MainupdatePanel");

             PlaceHolder ph = (PlaceHolder)this.Parent.FindControl("PlaceHolder1");
             ph.Controls.Clear();
             ph.Controls.Add(Userlogin);

            
            
            pmain.Update();
            
          
            
        }

        protected void LinkButtonSignIn_Click1(object sender, EventArgs e)
        {

        }

        

        
    }
}