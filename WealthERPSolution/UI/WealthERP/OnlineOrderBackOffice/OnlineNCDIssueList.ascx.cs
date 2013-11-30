﻿using System;
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


using VoUser;
using BoCommon;

using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineNCDIssueList : System.Web.UI.UserControl
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
            int adviserId = advisorVo.advisorId;
            if (!IsPostBack)
            {
                string type = "";
                string product = "";
                DateTime date = DateTime.MinValue;
                if (Request.QueryString["action"] != null)
                {
                   
                    type = Request.QueryString["type"].ToString();
                    product =Request.QueryString["product"].ToString() ;
                    date = Convert.ToDateTime(Request.QueryString["date"].ToString());
                    ddlType.SelectedValue = type;
                    txtDate.SelectedDate = date;
                    pnlIssueList.Visible = true;
                    BindViewListGrid(GetType(type), date, product);
                }

            }
        }
        private int GetType(string ddlSelection)
        {
            int type = 0;
            if (ddlSelection == "Curent")
            {
                type = 1;
            }
            else if (ddlSelection == "Closed")
            {
                type = 2;
            }
            else
            {
                type = 3;
            }
            return type;
        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            int type = GetType(ddlType.SelectedValue);
            BindViewListGrid(type, txtDate.SelectedDate.Value, ddlProduct.SelectedValue);
            pnlIssueList.Visible = true;
        }
        private void BindViewListGrid(int type, DateTime date,string product)
        {
            try
            {
                DataTable dtIssueList = new DataTable();
                dtIssueList = onlineNCDBackOfficeBo.GetAdviserIssueList(date, type, product).Tables[0];
                gvIssueList.DataSource = dtIssueList;
                gvIssueList.DataBind();
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
        protected void gvIssueList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtEligibleInvestorCategories = new DataTable();
            dtEligibleInvestorCategories = (DataTable)Cache[userVo.UserId.ToString() + "IssueList"];

            if (dtEligibleInvestorCategories != null)
            {
                gvIssueList.DataSource = dtEligibleInvestorCategories;
            }

        }
        protected void lnkIssueNo_Click(object sender, EventArgs e)
        {
            LinkButton lnkOrderNo = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkOrderNo.NamingContainer;
            int selectedRow = gdi.ItemIndex + 1;
            int issueNo = int.Parse((gvIssueList.MasterTableView.DataKeyValues[selectedRow - 1]["AIM_IssueId"].ToString()));
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineNCDIssueSetup", "loadcontrol('OnlineNCDIssueSetup','action=viewIsssueList&issueNo=" + issueNo + "&type=" + ddlType.SelectedValue + "&date=" + txtDate.SelectedDate.Value + "&type=" + ddlProduct.SelectedValue + "');", true);

        }
    }
}