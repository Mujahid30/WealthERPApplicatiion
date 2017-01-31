using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using VoUser;
using Telerik.Web.UI;
using BoCommon;
using System.Configuration;
using VOAssociates;
using BOAssociates;
using BoUploads;

namespace WealthERP.Associates
{
    public partial class ViewAdviserAssociateList : System.Web.UI.UserControl
    {
        List<AssociatesVO> associateVoList = null;
        AdvisorVo advisorVo = new AdvisorVo();
        AssociatesBo associatesBo = new AssociatesBo();
        AssociatesVO associatesVo = new AssociatesVO();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        UserVo userVo = new UserVo();
        RMVo rmVo = new RMVo();
        string userType;
        string currentUserRole;
        String Agentcode;
        int Id;

        protected void Page_Load(object sender, EventArgs e)
        {
            rmVo = (RMVo)Session[SessionContents.RmVo];
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            //currentUserRole = Session[SessionContents.CurrentUserRole].ToString().ToLower();
            associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
            imgViewAssoList.Visible = false;
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
                userType = "associates";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();
            if (!IsPostBack)
            {
                //associatesVo = (AssociatesVO)Session["associatesVo"];
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin"
                    || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops"
                    || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                {
                    Agentcode = string.Empty;
                }
                else

                    Agentcode = associateuserheirarchyVo.AgentCode;
                GetAdviserAssociateList();

            }
        }

        private void GetAdviserAssociateList()
        {
            DataSet dsGetAdviserAssociateList;
            DataTable dtGetAdviserAssociateList;
            if (userType == "advisor" || userType == "ops" || userType == "associates")
                Id = advisorVo.advisorId;
            else if (userType == "bm")
                Id = rmVo.RMId;
            dsGetAdviserAssociateList = associatesBo.GetAdviserAssociateList(Id, userType, Agentcode);
            if (dsGetAdviserAssociateList.Tables.Count > 0)
                dtGetAdviserAssociateList = dsGetAdviserAssociateList.Tables[0];
            else
                dtGetAdviserAssociateList = null;
            if (dtGetAdviserAssociateList == null)
            {
                gvAdviserAssociateList.DataSource = dtGetAdviserAssociateList;
                gvAdviserAssociateList.DataBind();
                imgViewAssoList.Visible = false;
            }
            else
            {
                gvAdviserAssociateList.DataSource = dtGetAdviserAssociateList;
                gvAdviserAssociateList.DataBind();
                pnlAdviserAssociateList.Visible = true;
                gvAdviserAssociateList.Visible = true;
                imgViewAssoList.Visible = true;
                if (Cache["gvAdviserAssociateList" + userVo.UserId + userType] == null)
                {
                    Cache.Insert("gvAdviserAssociateList" + userVo.UserId + userType, dtGetAdviserAssociateList);
                }
                else
                {
                    Cache.Remove("gvAdviserAssociateList" + userVo.UserId + userType);
                    Cache.Insert("gvAdviserAssociateList" + userVo.UserId + userType, dtGetAdviserAssociateList);
                }
            }
        }
        protected void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            ExcelToExport();
          

        }
        private void ExcelToExport()
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "AdviserAssociateList.xls"));
            Response.ContentType = "application/ms-excel";
            DataTable dt = new DataTable();
            dt = (DataTable)Cache["gvAdviserAssociateList" + userVo.UserId + userType];
            string str = string.Empty;
            foreach (DataColumn dtcol in dt.Columns)
            {
                Response.Write(str + dtcol.ColumnName);
                str = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in dt.Rows)
            {
                str = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Response.Write(str + Convert.ToString(dr[j]));
                    str = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        protected void gvAdviserAssociateList_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetAdviserAssociateList = new DataTable();
            dtGetAdviserAssociateList = (DataTable)Cache["gvAdviserAssociateList" + userVo.UserId + userType];
            gvAdviserAssociateList.DataSource = dtGetAdviserAssociateList;
            gvAdviserAssociateList.Visible = true;

            pnlAdviserAssociateList.Visible = true;
            imgViewAssoList.Visible = true;
        }
        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAction = (DropDownList)sender;
            GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            int selectedRow = gvr.ItemIndex + 1;

            string action = "";
            int assiciateId = int.Parse(gvAdviserAssociateList.MasterTableView.DataKeyValues[selectedRow - 1]["AA_AdviserAssociateId"].ToString());
            if (ddlAction.SelectedItem.Value.ToString() == "Edit")
            {
                action = "Edit";
                GetAssociateVoList(assiciateId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddAssociatesDetails", "loadcontrol('AddAssociatesDetails','action=Edit');", true);

            }
            if (ddlAction.SelectedItem.Value.ToString() == "View")
            {
                action = "View";
                GetAssociateVoList(assiciateId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddAssociatesDetails", "loadcontrol('AddAssociatesDetails','action=View');", true);

            }
        }

        private void GetAssociateVoList(int assiciateId)
        {
            associatesVo = associatesBo.GetAssociateVoList(assiciateId);
            Session["associatesVo"] = associatesVo;
        }

       

        protected void gvAdviserAssociateList_ItemDataBound(Object sender, GridItemEventArgs e)
        {
            if (advisorVo.advisorId != Convert.ToInt32(ConfigurationSettings.AppSettings["ONLINE_ADVISER"]))
            {
                gvAdviserAssociateList.MasterTableView.GetColumn("Welcome").Visible = false;
            }
            if (userVo.UserType == "Advisor") return;

            if ((e.Item is GridDataItem) == false) return;

            GridDataItem item = (GridDataItem)e.Item;
            //GridColumn column=(GridColumn)sender as GridColumn;
            DropDownList actions = (DropDownList)item.FindControl("ddlMenu");
            if (userType == "associates")
                gvAdviserAssociateList.Columns[0].Visible = false;
        
           
            //column.Visible = false;

            //RadComboBoxItem rbcItem = actions.Items.FindItemByValue("Edit", true);
            //rbcItem.Visible = false;
        }
        //protected void gvAdviserAssociateList_OnItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridDataItem)
        //    {
        //        GridDataItem dataItem = (GridDataItem)e.Item;
        //    LinkButton lbtnWelcomeletter = dataItem["editColumn"].Controls[0] as LinkButton;
        //    }
        //}
        protected void lbtnWelcomeletter_OnClick(object sender, EventArgs e)
        {
            GridDataItem grdrow = (GridDataItem)((LinkButton)sender).NamingContainer;
            if (string.IsNullOrEmpty(gvAdviserAssociateList.MasterTableView.DataKeyValues[grdrow.ItemIndex]["WelcomeNotePath"].ToString()))
            {
                
                string redirectPath = ConfigurationManager.AppSettings["WEL_COME_LETER_QUERY_STRING"].ToString();
                string associateid = gvAdviserAssociateList.MasterTableView.DataKeyValues[grdrow.ItemIndex]["AA_AdviserAssociateId"].ToString();
                Response.Redirect(redirectPath + associateid + "&associateList=1");
                //btnPreviewSend.PostBackUrl = "~/Reports/Display.aspx?welcomeNote=1&associateId=" + associateId.ToString();
            }
            else if (!string.IsNullOrEmpty(gvAdviserAssociateList.MasterTableView.DataKeyValues[grdrow.ItemIndex]["WelcomeNotePath"].ToString()))
            {
                string WelcomeNotePath = gvAdviserAssociateList.MasterTableView.DataKeyValues[grdrow.ItemIndex]["WelcomeNotePath"].ToString();
                string targetPath = ConfigurationManager.AppSettings["Welcome_Note_PATH"].ToString();
                Response.Redirect(targetPath + WelcomeNotePath);
            }
          

            
        }
        protected void gvAdviserAssociateList_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "childAgentcode")
            {
                GridEditableItem editItem = e.Item as GridEditableItem;
                LinkButton lnkprAmcB = (LinkButton)editItem.FindControl("LnkRQ");
                string SubBrokerCode = gvAdviserAssociateList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["SubBrokerCode"].ToString();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewAgentCode','AgentCode=" + SubBrokerCode + "');", true);
            }
        
        }


        protected void LnkRQ_Click(object sender, EventArgs e)
        {
           
        }
    }
}
