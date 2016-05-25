using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCustomerRiskProfiling;
using BoCommon;
using BoUser;
using VoUser;
using Telerik.Web.UI;

namespace WealthERP.FP
{
    public partial class ViewFPUtilityUserDetails : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            BindGrid();
        }


        protected void gvLeadList_OnNeedDataSource(Object sender, GridItemEventArgs e)
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["gvLeadList"];
            gvLeadList.DataSource = ds;
        }
        protected void BindGrid()
        {
            RiskProfileBo bo = new RiskProfileBo();
            DataSet ds = new DataSet();
            ds = bo.GetFPUtilityUserDetailsList();
            gvLeadList.DataSource = ds;
            gvLeadList.DataBind();
            gvLeadList.Visible = true;
            if (Cache["gvLeadList"] == null)
            {
                Cache.Insert("gvLeadList", ds);
            }
            else
            {
                Cache.Remove("gvLeadList");
                Cache.Insert("gvLeadList", ds);
            }
        }
    }
}