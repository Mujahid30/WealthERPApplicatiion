using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoCommon;
using System.Configuration;
using BoAdvisorProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;

namespace WealthERP.Advisor
{
    public partial class AdviserEnvironmentSettings : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        AdvisorBo advisorBo = new AdvisorBo();
        AdvisorVo advisorVo = new AdvisorVo();
        AdviserIPVo adviserIPVo = new AdviserIPVo();
        DataSet dsGetAllAdviserIPFromIPPool = new DataSet();
        string PreviousPage = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string FromPageToCheckOps = string.Empty;
            try
            {
                SessionBo.CheckSession();
                string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();

                userVo = (UserVo)Session["UserVo"];
                if (Session["advisorVo"] != null)
                {
                    advisorVo = (AdvisorVo)Session["advisorVo"];
                }
                else
                {
                    advisorVo = advisorBo.GetAdvisorUser(userVo.UserId);
                }

                if (!IsPostBack)
                {
                    if (Request.QueryString["PreviousPageName"] != null)
                        FromPageToCheckOps = Request.QueryString["PreviousPageName"].ToString();
                    // To Check Whether Adviser already Enabled IP Security or not..
                    if (advisorVo.IsIPEnable == 1)
                        chkIsIPEnable.Checked = true;
                    else
                        chkIsIPEnable.Checked = false;

                    // To Check Whether Adviser already Enabled Ops Login or not..
                    if ((advisorVo.IsOpsEnable == 1) || (FromPageToCheckOps != string.Empty))
                        chkIsOpsEnable.Checked = true;
                    else
                        chkIsOpsEnable.Checked = false;
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

                FunctionInfo.Add("Method", "EditAdvisorProfile.ascx:Page_Load()");

                object[] objects = new object[4];

                objects[0] = advisorBo;
                objects[1] = advisorVo;
                objects[2] = userVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string IPAddress = string.Empty;
            int createdById = 0;
            bool RecordStatus = false;

            advisorVo.UserId = userVo.UserId;

            // To Check IP CheckBox Checked or not..
            if (chkIsIPEnable.Checked == true)
                advisorVo.IsIPEnable = 1;
            else
                advisorVo.IsIPEnable = 0;

            

            // To Check Ops CheckBox Checked or not..
            if (chkIsOpsEnable.Checked == true)
                advisorVo.IsOpsEnable = 1;
            else
                advisorVo.IsOpsEnable = 0;



            advisorBo.UpdateAdvisorUser(advisorVo);

            Session["advisorVo"] = (AdvisorVo)advisorVo;

            if (advisorVo.IsIPEnable == 1)
            {
                int i = 0;
                IPAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                dsGetAllAdviserIPFromIPPool = advisorBo.GetAdviserIPPoolsInformation(advisorVo.advisorId);
                if (dsGetAllAdviserIPFromIPPool.Tables.Count != 0)
                {
                    foreach (DataRow dr in dsGetAllAdviserIPFromIPPool.Tables[0].Rows)
                    {
                        if (IPAddress == dr["AIPP_IP"].ToString())
                        {
                            i = 1;
                        }
                    }
                }
                if (i != 1)
                {
                    if (IPAddress != "")
                        adviserIPVo.AdviserIPs = IPAddress;
                    adviserIPVo.AdviserIPComments = "Adviser's Default IP";
                    adviserIPVo.advisorId = advisorVo.advisorId;
                    createdById = userVo.UserId;

                    RecordStatus = advisorBo.CreateAdviserIPPools(adviserIPVo, createdById);
                }
            }

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserEnvironmentSettings','none');", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "adviserpaneleftttttt", "loadlinks('AdvisorLeftPane','login');", true);
        }

        protected void hiddenbtnOpsEnable_Click(object sender, EventArgs e)
        {
            //int rmId = 0;
            //DataSet dsGetAllOpsStaffs = new DataSet();
            //dsGetAllOpsStaffs = advisorBo.GetAllOpsStaffsForAdviser(advisorVo.advisorId, "Ops");

            //if (dsGetAllOpsStaffs.Tables.Count > 0)
            //{
            //    if (dsGetAllOpsStaffs.Tables[0].Rows.Count > 0)
            //    {
            //        foreach (DataRow dr in dsGetAllOpsStaffs.Tables[0].Rows)
            //        {
            //            rmId = int.Parse(dr["AR_RMId"].ToString());
            //            advisorBo.UpdateOpsStaffLoginStatus(rmId, 1);
            //        }
            //    }
            //}

            if (hdnEnableOpsMsg.Value == "1")
            {
                PreviousPage = "EnvironmentalSettings";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AddRM','PreviousPageName=" + PreviousPage + "');", true);

            }
        }

        protected void hiddenbtnOpsDisable_Click(object sender, EventArgs e)
        {
            //int rmId = 0;
            //DataSet dsGetAllOpsStaffs = new DataSet();
            //dsGetAllOpsStaffs = advisorBo.GetAllOpsStaffsForAdviser(advisorVo.advisorId, "Ops");

            //if (dsGetAllOpsStaffs.Tables.Count > 0)
            //{
            //    if (dsGetAllOpsStaffs.Tables[0].Rows.Count > 0)
            //    {
            //        foreach (DataRow dr in dsGetAllOpsStaffs.Tables[0].Rows)
            //        {
            //            rmId = int.Parse(dr["AR_RMId"].ToString());
            //            advisorBo.UpdateOpsStaffLoginStatus(rmId, 0);
            //        }
            //    }
            //}
        }
    }
}