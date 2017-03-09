using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoUser;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using System.Data;
using WealthERP.Customer;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.Base;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Text;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoCommon;
using VoAdvisorProfiling;
using BoSuperAdmin;
using VOAssociates;
using BOAssociates;
using Telerik.Web.UI;

namespace WealthERP.Advisor
{
    public partial class AssociateCustomerList : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
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
        SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        AssociatesVO associatesVo = new AssociatesVO();
        int rmId;
        int branchHeadId;
        AdvisorPreferenceVo advisorPrefernceVo = new AdvisorPreferenceVo();
        int adviserId;
        string agentCode;
        AssociatesUserHeirarchyVo assocUsrHeirVo;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            advisorPrefernceVo = (AdvisorPreferenceVo)Session["AdvisorPreferenceVo"];
            //CreationSuccessMessage.Visible = false;
            rmVo = (RMVo)Session["rmVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            associatesVo = (AssociatesVO)Session["associatesVo"];

            string userType = userVo.UserType.ToLower();
            string currUserRole = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            if (userType == "SuperAdmin") {
                this.UserRole = "advisor";
                adviserId = 1000;
            }
            else {
                
                switch (currUserRole) {
                    case "admin":
                    case "ops":
                    case "research":
                        this.UserRole = "advisor";
                        break;                    
                    case "associates":
                        this.UserRole = currUserRole;
                        break;
                }
                rmId = rmVo.RMId;
                branchHeadId = rmVo.RMId;
                adviserId = adviserVo.advisorId;
            }
            if (currUserRole == "associates") {
                assocUsrHeirVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                agentCode = assocUsrHeirVo.AgentCode;
            }
            if (!IsPostBack) {
                BindCustomerGrid();
                gvAssocCustList.Visible = true;
                if (userType == "SuperAdmin") {
                    gvAssocCustList.Visible = false;
                }
            }
        }

        /// <summary>
        ///  Binding Customer List at diffrent user role
        /// </summary>

        protected void BindCustomerGrid()
        {
            AdvisorBo adviserBo = new AdvisorBo();
            DataSet dsCustList;
            RMVo customerRMVo = new RMVo();

            try {
                dsCustList = adviserBo.GetAssociateCustomerList(adviserId, UserRole, agentCode);
                gvAssocCustList.DataSource = dsCustList.Tables[0];
                gvAssocCustList.DataBind();
                Cache.Insert(userVo.UserId.ToString() + "assocCustList", dsCustList.Tables[0]);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateCustomerList.ascx.cs:BindCustomerGrid()");
                object[] objects = new object[4];
                objects[0] = user;
                objects[1] = rmId;
                objects[2] = objects;
                //customerVo[3] = dsCustList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvAssocCustList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtAssocCust = new DataTable();
            if (Cache[userVo.UserId.ToString() + "assocCustList"] != null)
            {
                dtAssocCust = (DataTable)Cache[userVo.UserId.ToString() + "assocCustList"];
                gvAssocCustList.DataSource = dtAssocCust;
            }
        }

        /// <summary>
        /// this is use to export customer list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvAssocCustList.ExportSettings.OpenInNewWindow = true;
            gvAssocCustList.ExportSettings.IgnorePaging = true;
            gvAssocCustList.ExportSettings.HideStructureColumns = true;
            gvAssocCustList.ExportSettings.ExportOnlyData = true;
            gvAssocCustList.ExportSettings.FileName = "AssociateCustomerList";
            gvAssocCustList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvAssocCustList.MasterTableView.ExportToExcel();
        }

        protected void ddlAction_OnSelectedIndexChanged(object sender, EventArgs e)
        { 
        }

        protected void gvAssocCustList_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            gvAssocCustList.CurrentPageIndex = e.NewPageIndex;
        }
    }
}
