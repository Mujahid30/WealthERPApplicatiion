
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCustomerPortfolio;
using VoUser;
using BoCustomerProfiling;
using WealthERP.Base;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoAdvisorProfiling;
using BoUploads;
using Telerik.Web.UI;
using BoProductMaster;
using BoOps;
using BOAssociates;
using System.Configuration;
using VoOps;
using BoWerpAdmin;
using VoCustomerPortfolio;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using VOAssociates;
using BOAssociates;
using iTextSharp.text.pdf;
using System.IO;

namespace WealthERP.OPS
{
    public partial class ProductOrderDetailsMF : System.Web.UI.UserControl
    {
        string path,userType,   UserTitle;                                
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo;
        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
            if (!IsPostBack)
            {
                 AgentCode.Value = "0";
                associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
                {
                    userType = "associates";
                    if (UserTitle == "SubBroker")
                    {
                        associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                        if (associateuserheirarchyVo.AgentCode != null)
                        {
                            AgentCode.Value = associateuserheirarchyVo.AgentCode.ToString();
                        }
                        
                    }
                }

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindInsuranceGrid();

        }
        protected void btnExportFilteredData_OnClick(object sender, EventArgs e)
        {
            gvrInsurance.ExportSettings.OpenInNewWindow = true;
            gvrInsurance.ExportSettings.OpenInNewWindow = true;
            gvrInsurance.ExportSettings.IgnorePaging = true;
            gvrInsurance.ExportSettings.HideStructureColumns = true;
            gvrInsurance.ExportSettings.ExportOnlyData = true;
            gvrInsurance.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvrInsurance.MasterTableView.ExportToExcel();

        }
        private void BindInsuranceGrid()
        {

            InsuranceBo insuranceBo = new InsuranceBo();
            DataTable dtInsuranceOrderBook = insuranceBo.GetInsuranceOrders(ddlInsurance.SelectedValue, advisorVo.advisorId,AgentCode.Value.ToString());
            gvrInsurance.DataSource = dtInsuranceOrderBook;
            gvrInsurance.DataBind();
            pnlInsuranceBook.Visible = true;
            if (Cache["InsuranceOrderMIS" + userVo.UserId] != null)
            {
                Cache.Remove("InsuranceOrderMIS" + userVo.UserId);
            }
            Cache.Insert("InsuranceOrderMIS" + userVo.UserId, dtInsuranceOrderBook);

        }
        protected void gvrInsurance_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtOrderMIS = new DataTable();
            dtOrderMIS = (DataTable)Cache["InsuranceOrderMIS" + userVo.UserId];
            gvrInsurance.DataSource = dtOrderMIS;
            gvrInsurance.Visible = true;
        }
    }
}
