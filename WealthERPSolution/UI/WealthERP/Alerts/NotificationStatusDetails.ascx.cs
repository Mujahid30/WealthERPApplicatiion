using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoOnlineOrderManagement;
using System.Data;
using VoUser;
using BoCommon;
using WealthERP.Base;
using Telerik.Web.UI;

namespace WealthERP.Alerts
{
    public partial class NotificationStatusDetails : System.Web.UI.UserControl
    {
        OnlineOrderBackOfficeBo onlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            
            if(!IsPostBack)
            {
                DataSet ds = onlineOrderBackOfficeBo.BindNotificationSetup(adviserVo.advisorId);

                ddlAssetGroupName1.DataSource = ds.Tables[1];
                ddlAssetGroupName1.DataValueField = "Code";
                ddlAssetGroupName1.DataTextField = "Name";
                ddlAssetGroupName1.DataBind();
                ddlAssetGroupName1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                Panel1.Visible = false;
            }
        }
        protected void ddlAssetGroupName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = onlineOrderBackOfficeBo.GetNotificationTypes(ddlAssetGroupName1.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DropDownList1.DataSource = ds;
                DropDownList1.DataTextField = ds.Tables[0].Columns["CNT_NotificationType"].ToString();
                DropDownList1.DataValueField = ds.Tables[0].Columns["CNT_ID"].ToString();
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            }
            else
                DropDownList1.Items.Clear();
        }
        protected void NotificationType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = onlineOrderBackOfficeBo.GetNotificationHeader(Convert.ToInt32(DropDownList1.SelectedValue), adviserVo.advisorId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DropDownList2.DataSource = ds;
                DropDownList2.DataTextField = ds.Tables[0].Columns["CTNS_NotificationHeader"].ToString();
                DropDownList2.DataValueField = ds.Tables[0].Columns["CTNS_Id"].ToString();
                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            }
            else
                DropDownList2.Items.Clear();
        }
        protected void NotificationHeader_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = onlineOrderBackOfficeBo.GetNotificationCommChannel(Convert.ToInt32(DropDownList2.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                DropDownList3.DataSource = ds;
                DropDownList3.DataTextField = ds.Tables[0].Columns["Channel"].ToString();
                DropDownList3.DataValueField = ds.Tables[0].Columns["Channel"].ToString();
                DropDownList3.DataBind();
                DropDownList3.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            }
            else
                DropDownList3.Items.Clear();
        }
        protected void Submit_OnClick(object sender, EventArgs e)
        {
            //fromdate = DateTime.Parse(txtFromDate.SelectedDate.ToString());

            DataSet ds = onlineOrderBackOfficeBo.GetNotificationMessageDetails(Convert.ToInt32(DropDownList2.SelectedValue), DropDownList3.SelectedValue, Convert.ToDateTime(txtFromDate.SelectedDate),Convert.ToDateTime(txtToDate.SelectedDate));
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Cache["MessageDetails" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("MessageDetails" + userVo.UserId.ToString(), ds.Tables[0]);
                }
                else
                {
                    Cache.Remove("MessageDetails" + userVo.UserId.ToString());
                    Cache.Insert("MessageDetails" + userVo.UserId.ToString(), ds.Tables[0]);
                }
                Panel1.Visible = true;
                RadGrid3.DataSource = ds;
                RadGrid3.DataBind();
            }
            else
            {
                Panel1.Visible = false;
            }
            
        }
        protected void RadGrid3_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt;
            dt = (DataTable)Cache["MessageDetails" + userVo.UserId.ToString()];
            if (dt != null)
            {
                RadGrid3.DataSource = dt;
            }
        }
    }
}