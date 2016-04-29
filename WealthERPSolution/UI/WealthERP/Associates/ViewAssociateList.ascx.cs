using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using WealthERP.Base;
using BoCommon;
using System.Collections;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using BoWerpAdmin;
using System.Data;
using BoProductMaster;
using BoCustomerProfiling;
using BoAdvisorProfiling;
using BoSuperAdmin;
using VoValuation;
using BoCustomerPortfolio;
using VOAssociates;
using VoAdvisorProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text.RegularExpressions;
using BOAssociates;
using System.Configuration;

namespace WealthERP.Associates
{
    public partial class ViewAssociateList : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        AssociatesBo associatesBo = new AssociatesBo();
        AdvisorVo advisorVo = new AdvisorVo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        int CustomerId;
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        AdvisorBo advisorBo = new AdvisorBo();
        static string user = "";
        string UserRole;
        static Dictionary<string, string> genDictParent;
        static Dictionary<string, string> genDictRM;
        static Dictionary<string, string> genDictReassignRM;
        int RowCount = 0;
        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        AssociatesVO associatesVo = new AssociatesVO();
        int rmId;
        int branchHeadId;
        AdvisorPreferenceVo advisorPrefernceVo = new AdvisorPreferenceVo();
        string customer = "";
        int ParentId;
        string userType;
        string currentUserRole;
        String Agentcode;
        int Id;
        int adviserId;
        int AgentId;
        string AgentCode;
        int rbtnReg;
        AssociatesUserHeirarchyVo assocUsrHeirVo;
        string custCode = null;
        string pan = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            {
                SessionBo.CheckSession();
                advisorPrefernceVo = (AdvisorPreferenceVo)Session["AdvisorPreferenceVo"];
                rmVo = (RMVo)Session["rmVo"];
                adviserVo = (AdvisorVo)Session["advisorVo"];
                associatesVo = (AssociatesVO)Session["associatesVo"];



            }
        }
        protected void click_Go(object sender, EventArgs e)
        {
            GetAdviserAssociateList();

        }

        protected void ddlCOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearControls();
            if (ddlCOption.SelectedIndex == 0)
            {
                tdtxtCustomerName.Visible = false;
                return;
            }
            else
            {
                txtCustomerName_autoCompleteExtender.ContextKey = ddlCOption.SelectedValue + "/" + adviserVo.advisorId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAssociateAllCustomerName";

                tdtxtCustomerName.Visible = true;
            }


        }
        protected void clearControls()
        {

            txtCustomerName.Text = "";
        }


        protected void hdnCustomerId_ValueChanged(object sender, EventArgs e)
        {

        }


        protected void gvAdviserAssociateList_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetAdviserAssociateList = new DataTable();
            dtGetAdviserAssociateList = (DataTable)Cache["gvAdviserAssociateList" + userVo.UserId + userType];
            gvAdviserAssociateList.DataSource = dtGetAdviserAssociateList;
            gvAdviserAssociateList.Visible = true;
            //  pnlAdviserAssociateList.visible = true;
            // imgViewAssoList.Visible = true;
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


        private void GetAdviserAssociateList()
        {
            DataSet dsGetAdviserAssociateList;
            DataTable dtGetAdviserAssociateList;
            Id = advisorVo.advisorId;

            dsGetAdviserAssociateList = associatesBo.GetAdviserIndividualAssociateList(int.Parse(txtCustomerId.Value), adviserVo.advisorId);
            if (dsGetAdviserAssociateList.Tables.Count > 0)
                dtGetAdviserAssociateList = dsGetAdviserAssociateList.Tables[0];
            else
                dtGetAdviserAssociateList = null;
            if (dtGetAdviserAssociateList == null)
            {
                gvAdviserAssociateList.DataSource = dtGetAdviserAssociateList;
                gvAdviserAssociateList.DataBind();
                // imgViewAssoList.Visible = false;
            }
            else
            {
                gvAdviserAssociateList.DataSource = dtGetAdviserAssociateList;
                gvAdviserAssociateList.DataBind();
                //  pnlAdviserAssociateList.Visible = true;
                gvAdviserAssociateList.Visible = true;
                // imgViewAssoList.Visible = true;
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

    }
}