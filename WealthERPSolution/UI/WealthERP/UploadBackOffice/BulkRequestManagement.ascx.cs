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
using System.Net;

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
              
                multipageBulkOrderRequest.SelectedIndex = 0;
                ddlSelectIssue.Visible = false;
                trSelectIssueRow.Visible = false;
                gvBulkOrderStatusList.Visible = true;
                msgNoRecords.Visible = false;

            }
        }

        protected void btnGo1_Click(object sender, EventArgs e)
        {
            int ReqId;
            string OBT = ddlSelectType.SelectedItem.Value;
            string IssueNo = string.Empty;
            if (ddlSelectType.SelectedValue != "AssoList")
            {
                IssueNo = ddlSelectIssue.SelectedItem.Value;
            }
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
              //  DataSet dtBulkOrderStatusList = new DataSet();

                int reqId = 0;
                string OBT = null;
                DateTime Fromdate = DateTime.MinValue;
                DateTime Todate = DateTime.MinValue;
                msgNoRecords.Visible = false;
                gvBulkOrderStatusList.Visible = true;
                if (txtRequestId.Text != "")
                {
                    reqId = Convert.ToInt32(txtRequestId.Text);
                }
                else
                {
                    reqId = 0;
                }

                OBT = ddlIssueType.SelectedItem.Value;

                if (txtReqFromDate.SelectedDate != null)
                {
                    Fromdate = txtReqFromDate.SelectedDate.Value;
                }
                else
                {
                    Fromdate = DateTime.MinValue;
                }
                if (txtReqToDate.SelectedDate != null)
                {
                    Todate = txtReqToDate.SelectedDate.Value;
                }
                else
                {
                    Todate = DateTime.MinValue;
                }



                DataSet dtBulkOrderStatusList;
                WERPTaskRequestManagementBo werpTaskRequestManagementBo = new WERPTaskRequestManagementBo();
                dtBulkOrderStatusList = werpTaskRequestManagementBo.GetBulkOrderStatus(reqId, OBT, Fromdate, Todate);
                if (dtBulkOrderStatusList.Tables[0].Rows.Count > 0)
                {
                    gvBulkOrderStatusList.Visible = true;
                    gvBulkOrderStatusList.DataSource = dtBulkOrderStatusList.Tables[0];
                    gvBulkOrderStatusList.DataBind();
                    if (Cache["gvBulkOrderStatusList" + userVo.UserId] == null)
                    {
                        Cache.Insert("gvBulkOrderStatusList" + userVo.UserId, dtBulkOrderStatusList);
                    }
                    else
                    {
                        Cache.Remove("gvBulkOrderStatusList" + userVo.UserId);
                        Cache.Insert("gvBulkOrderStatusList" + userVo.UserId, dtBulkOrderStatusList);
                    }

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
            if (ddlSelectType.SelectedValue != "AssoList")
            {
                ddlSelectIssue.Visible = true;
                BindViewListGrid(DateTime.Now, ddlSelectType.SelectedValue);
                ddlSelectIssue.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                trSelectIssueRow.Visible = true;
            }
        }
        protected void gvBulkOrderStatusList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                DataSet dtBulkOrderStatusList = new DataSet();
                dtBulkOrderStatusList = (DataSet)Cache["gvBulkOrderStatusList" + userVo.UserId.ToString()];
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

                string filenamepath = ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["FileNamePath"].ToString();
                string issuename = ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["IssueName"].ToString();
                string RequestId = ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["RequestId"].ToString();
                string RequestDateTime = ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["RequestDateTime"].ToString().Substring(0, ditem.OwnerTableView.DataKeyValues[ditem.ItemIndex]["RequestDateTime"].ToString().IndexOf(' '));
                string fileName = (RequestId + "_" + issuename + RequestDateTime + ".xlsx").Replace(" ", "").Trim();
                //string path = MapPath("C:\\Users\\Jgeorge\\Downloads\\" + filename);
                if (filenamepath != null || filenamepath == "&nbsp;")
                {
                    byte[] bts = System.IO.File.ReadAllBytes(filenamepath);
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.xlsx";
                    Response.AddHeader("Content-Length", bts.Length.ToString());
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.BinaryWrite(bts);
                    Response.Flush();
                    Response.End();
                }
                else { Response.Write(@"<script language='javascript'>alert('Cannot Download');</script>"); }

            }
        }
        protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "RI")
            {
                tdlblRequestId.Visible = true;
                tdtxtRequestId.Visible = true;
                tdlbltype.Visible = false;
                tdddlIssueType.Visible = false;
                tdlbFromdate.Visible = false;
                tdtxtReqFromDate.Visible = false;
                tdlblToDate.Visible = false;
                tdtxtReqToDate.Visible = false;
                tdbtnGo2.Visible = true;
                rfvType2.Visible = false;
                rfvFromDate.Visible = false;
                rfvToDate.Visible = false;
                ddlIssueType.SelectedValue = "0";
                txtReqFromDate.SelectedDate = null;
                txtReqToDate.SelectedDate = null;
                gvBulkOrderStatusList.Visible = true;
            }
            else if (ddlType.SelectedValue == "RT")
            {
                tdlbltype.Visible = true;
                tdddlIssueType.Visible = true;
                tdlblRequestId.Visible = false;
                tdtxtRequestId.Visible = false;
                tdlbFromdate.Visible = false;
                tdtxtReqFromDate.Visible = false;
                tdlblToDate.Visible = false;
                tdtxtReqToDate.Visible = false;
                tdbtnGo2.Visible = true;
                rfvRequestId.Visible = false;
                rfvFromDate.Visible = false;
                rfvToDate.Visible = false;
                txtRequestId.Text = "";
                txtReqFromDate.SelectedDate = null;
                txtReqToDate.SelectedDate = null;
                gvBulkOrderStatusList.Visible = true;
            }
            else if (ddlType.SelectedValue == "RD")
            {
                tdlbFromdate.Visible = true;
                tdtxtReqFromDate.Visible = true;
                tdlblToDate.Visible = true;
                tdtxtReqToDate.Visible = true;
                tdlbltype.Visible = false;
                tdddlIssueType.Visible = false;
                tdlblRequestId.Visible = false;
                tdtxtRequestId.Visible = false;
                tdbtnGo2.Visible = true;
                rfvRequestId.Visible = false;
                rfvType2.Visible = false;
                ddlIssueType.SelectedValue = "0";
                txtRequestId.Text = "";
                gvBulkOrderStatusList.Visible = true;
            }
        }

    }
}