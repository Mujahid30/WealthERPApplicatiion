using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoUser;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using System.Data;
using WealthERP.Customer;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.Base;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Text;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoCommon;
using VoAdvisorProfiling;
using BoSuperAdmin;
using VOAssociates;
using BOAssociates;
using Telerik.Web.UI;
using System.Text.RegularExpressions;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class CustomerProfileAudit : System.Web.UI.UserControl{
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        int CustomerId;
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        AdvisorBo advisorBo = new AdvisorBo();
        static string user = "";
        string UserRole;
        static Dictionary<string, string> genDictParent;
        static Dictionary<string, string> genDictRM;
        static Dictionary<string, string> genDictReassignRM;
        int RowCount = 0;
        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        AssociatesVO associatesVo = new AssociatesVO();
        int rmId;
        int branchHeadId;
        AdvisorPreferenceVo advisorPrefernceVo = new AdvisorPreferenceVo();
        string customer = "";
        int ParentId;
        int adviserId;
        int AgentId;
        string AgentCode;
        AssociatesUserHeirarchyVo assocUsrHeirVo;
    
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            advisorPrefernceVo = (AdvisorPreferenceVo)Session["AdvisorPreferenceVo"];
            rmVo = (RMVo)Session["rmVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            associatesVo = (AssociatesVO)Session["associatesVo"];
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = adviserVo.advisorId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                

            }

        }
        protected void hdnCustomerId_ValueChanged(object sender, EventArgs e)
        {

            

        }
    }
}