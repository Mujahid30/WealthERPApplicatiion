using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using WealthERP.Base;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoOnlineOrderManagement;
using BoCommon;
using VoUser;
using VoOnlineOrderManagemnet;

namespace WealthERP.UploadBackOffice
{
    public partial class BulkRequestManagement : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        OnlineNCDBackOfficeVo onlineNCDBackOfficeVo;
        string issuerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];

            if (!IsPostBack)
            {
                //string type = "";
                //string product = "";
                //DateTime date = DateTime.MinValue;
                //if (Request.QueryString["action"] != null)
                //{

                //    type = Request.QueryString["type"].ToString();
                //    product = Request.QueryString["product"].ToString();
                //    date = Convert.ToDateTime(Request.QueryString["date"].ToString());
                //    ddlSelectType.SelectedValue = product;
                //    BindViewListGrid(date, product);

                //}
                multipageBulkOrderRequest.SelectedIndex = 0;
                ddlSelectIssue.Visible = false;
                trSelectIssueRow.Visible = false;

            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            int ReqId;
            string OBT = ddlSelectType.SelectedItem.Value;
            string IssueNo = ddlSelectIssue.SelectedItem.Value;
            WERPTaskRequestManagementBo werpTaskRequestManagementBo = new WERPTaskRequestManagementBo();
            werpTaskRequestManagementBo.CreateTaskRequestForBulk(6, userVo.UserId, out ReqId, advisorVo.advisorId, OBT, IssueNo);
            msgUploadComplete.Visible = true;
            if (ReqId > 0)
            {
                msgUploadComplete.InnerText = "Request Id-" + ReqId.ToString() + "-Generated SuccessFully";
            }
            else
            {
                msgUploadComplete.InnerText = "Not able to create Request,Try again";
            }
        }
        private void BindViewListGrid(DateTime date, string product)
        {
            try
            {
                DataTable dtIssueList = new DataTable();
                dtIssueList = onlineNCDBackOfficeBo.GetAdviserIssueListClosed(product, advisorVo.advisorId).Tables[0];
                ddlSelectIssue.DataSource = dtIssueList;
                ddlSelectIssue.DataTextField = "AIM_IssueName";
                ddlSelectIssue.DataValueField = "AIM_IssueId";
                ddlSelectIssue.DataBind();
                if (Cache[userVo.UserId.ToString() + "IssueList"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "IssueList");
                Cache.Insert(userVo.UserId.ToString() + "IssueList", dtIssueList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindViewListGrid()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        protected void Product_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSelectIssue.Visible = true;
            BindViewListGrid(DateTime.Now, ddlSelectType.SelectedValue);
            ddlSelectIssue.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            trSelectIssueRow.Visible = true;
        }
 
    }
}