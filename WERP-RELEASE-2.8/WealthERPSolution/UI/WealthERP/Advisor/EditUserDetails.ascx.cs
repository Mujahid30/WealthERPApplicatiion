using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoAdvisorProfiling;
using BoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class EditUserDetails : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo = new AdvisorVo();
       
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                SessionBo.CheckSession();
                if (Session["CustomerUser"] != null)
                {
                    userVo = (UserVo)Session["CustomerUser"];
                    lblId.Text = userVo.LoginId.ToString();
                    lblUser.Text = userVo.FirstName.ToString() + userVo.LastName.ToString();
                }
                else
                {
                    userVo = (UserVo)Session["UserVo"];
                    lblId.Text = userVo.LoginId.ToString();
                    lblUser.Text = userVo.FirstName.ToString() + userVo.LastName.ToString();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EditUserDetails.ascx:Page_Load()");
                object[] objects = new object[2];
                objects[0] = advisorVo;
                objects[1] = userVo;
              
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void lnlChgPassword_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ChangePassword','none');", true);
        }

        protected void lnkChgLoginId_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ChangeLoginId','none');", true);

        }
    }
}