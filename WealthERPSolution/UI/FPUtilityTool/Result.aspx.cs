using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoFPUtility;
using System.Data;
using VOFPUtilityUser;
using System.Configuration;

namespace FPUtilityTool
{
    public partial class Result : System.Web.UI.Page
    {
        FPUserBO fpUserBo = new FPUserBO();
        FPUserVo userVo = new FPUserVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.UrlReferrer == null)
                    Response.Redirect("Questionnaire.aspx");
            }
            FPUserBO.CheckSession();
            userVo = (FPUserVo)Session["UserVo"];
            int adviserId = Convert.ToInt32(ConfigurationManager.AppSettings["ONLINE_ADVISER"]);
            lblUserName.Text = " "+userVo.UserName;
            DataSet dsRiskClass = fpUserBo.GetRiskClass(userVo.UserId, adviserId);
            if (dsRiskClass.Tables[0].Rows.Count > 0)
            {
                lblRiskClass.Text = dsRiskClass.Tables[0].Rows[0]["XRC_RiskClass"].ToString();
                lblRiskText.Text = dsRiskClass.Tables[0].Rows[0]["ARC_RiskText"].ToString();
            }
        }
        protected void btnLogOut_OnClick(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
}
