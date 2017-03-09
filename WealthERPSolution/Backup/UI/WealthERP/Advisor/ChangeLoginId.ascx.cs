using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoAdvisorProfiling;
using BoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;


namespace WealthERP
{
    public partial class ChangeLoginId : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        UserBo userBo = new UserBo();
        AdvisorVo advisorVo = new AdvisorVo();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                SessionBo.CheckSession();

                if (Session["CustomerUser"] != null)
                    userVo = (UserVo)Session["CustomerUser"];
                else
                    userVo = (UserVo)Session["userVo"];

                //lblLoginId.Text = userVo.LoginId.ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ChangeLoginId.ascx:Page_Load()");
                object[] objects = new object[1];
                objects[0] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            

            try
            {
                if (IsValid())
                {
                    
                    if (userBo.ChkAvailability(txtNewLoginId.Text.Trim()))
                    {
                        userVo.LoginId = txtNewLoginId.Text.Trim();
                        userBo.UpdateUser(userVo);
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('Your Id has changed successfully..!');", true);
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('EditUserDetails','none');", true);
                    }
                    else //Some one else is using the login id.
                    {
                        lblStatusMessage.Text = "The Login Id is not available.";
                    }
                }
                else //The login Id entered by user is wrong
                {
                    lblStatusMessage.Text = "The current Login Id is wrong.";
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

                FunctionInfo.Add("Method", "ChangeLoginId.ascx:btnSave_Click()");


                object[] objects = new object[3];
                objects[0] = userBo;
                objects[1] = userVo;
                objects[3] = advisorVo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        private bool IsValid()
        {
            if (txCurrentLoginId.Text.Trim() == userVo.LoginId)
                return true;
            else
                return false;
        }


    }
}