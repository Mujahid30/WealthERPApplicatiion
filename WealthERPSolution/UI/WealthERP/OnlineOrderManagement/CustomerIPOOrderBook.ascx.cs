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
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using System.Data;

namespace WealthERP.OnlineOrderManagement
{
    public partial class CustomerIPOOrderBook : System.Web.UI.UserControl
    {
        OnlineIPOOrderBo onlineIPOOrderBo = new OnlineIPOOrderBo();
        AdvisorVo advisorVo;
        CustomerVo customerVo;
        UserVo userVo;

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["customerVo"];
            if (!Page.IsPostBack)
            {
                BindCustomerIssueIPOBook();
            }

        }

        private void BindCustomerIssueIPOBook()
        {

            DataTable dtCustomerIssueIPOBook = onlineIPOOrderBo.GetCustomerIPOIssueBook(customerVo.CustomerId);

            if (dtCustomerIssueIPOBook.Rows.Count > 0)
            {
                if (Cache["CustomerIPOIssueBook" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("CustomerIPOIssueBook" + userVo.UserId.ToString(), dtCustomerIssueIPOBook);
                }
                else
                {
                    Cache.Remove("CustomerIPOIssueBook" + userVo.UserId.ToString());
                    Cache.Insert("CustomerIPOIssueBook" + userVo.UserId.ToString(), dtCustomerIssueIPOBook);
                }
                //ibtExportSummary.Visible = false;
                RadGridIssueIPOBook.DataSource = dtCustomerIssueIPOBook;
                RadGridIssueIPOBook.DataBind();
            }
            else
            {
                //ibtExportSummary.Visible = false;
                RadGridIssueIPOBook.DataSource = dtCustomerIssueIPOBook;
                RadGridIssueIPOBook.DataBind();

            }
 
        }
    }
}