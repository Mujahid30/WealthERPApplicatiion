using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using System.Data;
using WealthERP.Base;
using BoCommon;
using BoCommisionManagement;
using VoCommisionManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using BoAdvisorProfiling;
namespace WealthERP.CommisionManagement
{
    public partial class PayableStructureView : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        RMVo rmVo;
        AdvisorBo advisorBo = new AdvisorBo();
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session["rmVo"];
            if (!IsPostBack)
            {
                BindCategoryddl();
            }

        }
        protected void BindCategoryddl()
        {

            DataSet dscategoryDetails = new DataSet();
            dscategoryDetails = advisorBo.gvAdvisorCategoryBind(advisorVo.advisorId, 1000);
            ddlCategory.DataSource = dscategoryDetails.Tables[0];
            ddlCategory.DataTextField = dscategoryDetails.Tables[0].Columns["AC_CategoryName"].ToString();
            ddlCategory.DataValueField = dscategoryDetails.Tables[0].Columns["AC_CategoryID"].ToString();
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Custom", "Custom"));
            ddlCategory.Items.Insert(0, new ListItem("Select","0"));
            

        }
        protected void BindgvStructureView(int isCategory, int categoryId)
        {
            DataSet dscategoryDetails = new DataSet();
            dscategoryDetails = commisionReceivableBo.PaybleStructureViewWithAssociateDetails(advisorVo.advisorId, isCategory, categoryId);
            if (dscategoryDetails != null)
            {

                gvStructureView.DataSource = dscategoryDetails.Tables[0];
                gvStructureView.DataBind();
                Cache.Insert(userVo.UserId.ToString() + "StructureView", dscategoryDetails);
                tblCommissionStructureRuleView.Visible = true;

            }


        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            int isCategory;
            int categoryId;
            if (ddlCategory.SelectedValue == "Custom")
            {
                isCategory = 0;
                categoryId = 0;
            }
            else
            {
                isCategory = 1;
                categoryId = Convert.ToInt32(ddlCategory.SelectedValue);
            }
            BindgvStructureView(isCategory, categoryId);

        }

        protected void RadGridStructureRule_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet dsCommissionStructureRule = new DataSet();
            if (Cache[userVo.UserId.ToString() + "StructureView"] != null)
            {
                dsCommissionStructureRule = (DataSet)Cache[userVo.UserId.ToString() + "StructureView"];
                gvStructureView.DataSource = dsCommissionStructureRule.Tables[0];
            }
        }
    }
}