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
                gvBulkOrderStatusList.Visible = false;
                msgNoRecords.Visible = false;

            }
        }

        protected void btnGo1_Click(object sender, EventArgs e)
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
        protected void btnGo2_Click(object sender, EventArgs e)
        {
            try
            {
                msgNoRecords.Visible = false; 
                gvBulkOrderStatusList.Visible = false;
                string OBT = ddlIssueType.SelectedItem.Value;
                DateTime Fromdate = txtReqFromDate.SelectedDate.Value;
                DateTime Todate = txtReqToDate.SelectedDate.Value;
                //string Fromdate=frmdt.ToString("yyyy-mm-dd");
                //string Todate=todt.ToString("yyyy-mm-dd");
                WERPTaskRequestManagementBo werpTaskRequestManagementBo = new WERPTaskRequestManagementBo();
                
                dtBulkOrderStatusList = werpTaskRequestManagementBo.GetBulkOrderStatus(OBT, Fromdate, Todate);
                if (dtBulkOrderStatusList.Tables[0].Rows.Count > 0)
                {
                    gvBulkOrderStatusList.Visible = true;
                    gvBulkOrderStatusList.DataSource = dtBulkOrderStatusList.Tables[0];
                    gvBulkOrderStatusList.DataBind();
                }
                else
                {
                    msgNoRecords.Visible = true;
                    msgNoRecords.InnerText = "No records Found";
                }



            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
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
        protected void gvBulkOrderStatusList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                string OBT = ddlIssueType.SelectedItem.Value;
                DateTime Fromdate = txtReqFromDate.SelectedDate.Value;
                DateTime Todate = txtReqToDate.SelectedDate.Value;
                //string Fromdate = frmdt.ToString("yyyy-mm-dd");
                //string Todate = todt.ToString("yyyy-mm-dd");
                WERPTaskRequestManagementBo werpTaskRequestManagementBo = new WERPTaskRequestManagementBo();
                dtBulkOrderStatusList = werpTaskRequestManagementBo.GetBulkOrderStatus(OBT, Fromdate, Todate);
                gvBulkOrderStatusList.DataSource = dtBulkOrderStatusList;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }
        protected void gvBulkOrderStatusList_OnDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                //if (string.IsNullOrEmpty(dataItem["Test"].Text))
                if (dataItem["FileNamePath"].Text == "&nbsp;" || dataItem["FileNamePath"].Text == null)
                {
                    dataItem["Download"].Text = String.Empty;
                }
            }
        }
        protected void gvBulkOrderStatusList_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            
            if (e.CommandName == "Download")
            {
                GridDataItem ditem = (GridDataItem)e.Item;
                string filename = ditem["FileNamePath"].Text;
                //string path = MapPath("C:\\Users\\Jgeorge\\Downloads\\" + filename);
                if (filename != null || filename == "&nbsp;")
                 {
                     byte[] bts = System.IO.File.ReadAllBytes(filename);
                     Response.Clear();
                     Response.ClearHeaders();
                     Response.AddHeader("Content-Type", "Application/octet-stream");
                     Response.AddHeader("Content-Length", bts.Length.ToString());
                     Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                     Response.BinaryWrite(bts);
                     Response.Flush();
                     Response.End();
                 }
                 else { Response.Write(@"<script language='javascript'>alert('Cannot Download');</script>"); }
                
            }
        }

    }
}