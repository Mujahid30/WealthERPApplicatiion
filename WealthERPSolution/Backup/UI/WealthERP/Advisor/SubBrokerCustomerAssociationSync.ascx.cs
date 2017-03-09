using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using WealthERP.Base;
using BoCommon; 
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
 using System.Text;
using System.Data;
using BOAssociates;

namespace WealthERP.Advisor
{
    public partial class SubBrokerCustomerAssociationSync : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        AssociatesBo AssBo = new AssociatesBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];         



        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
     int Result = AssBo.SynchronizeCustomerAssociation(advisorVo.advisorId, ddSource.SelectedValue, userVo.UserId);

     ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Associations added successfully');", true);
         
        }

       

    }
}