using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using VoUser;
using BoCustomerProfiling;
using System.Configuration;
using WealthERP.Base;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using VoReports;
using System.Web.UI;

namespace WealthERP.Reports
{
    public partial class OfflineForm : System.Web.UI.Page
    {
        RMVo rmVo = new RMVo();
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo = null;
        string path = string.Empty;
        FPOfflineFormVo fpofflineForm = new FPOfflineFormVo();
        CustomerVo customerVo = new CustomerVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            if (Request.Form["ctrl_EquityReports$btnViewInPDF"] != "View Report")
            {
                rmVo = (RMVo)Session[SessionContents.RmVo];
            }
        }
    }
}
