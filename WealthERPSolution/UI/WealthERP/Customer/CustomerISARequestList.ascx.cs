using System;
using System.Collections.Generic;
using System.Linq;
using BoUser;
using VoUser;
using WealthERP.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoUser;
using WealthERP.Base;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;
using BoCalculator;
using Telerik.Web.UI;


namespace WealthERP.Customer
{
    public partial class CustomerISARequestList : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        List<LiabilitiesVo> liabilitiesListVo = null;
        LiabilitiesBo liabilitiesBo = new LiabilitiesBo();
        CustomerVo customerVo = null;
        Calculator calculator = new Calculator();
        AdvisorVo advisorVo=new AdvisorVo();
        int advisorId;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userBo = new UserBo();
            userVo = (UserVo)Session[SessionContents.UserVo];
            if (!IsPostBack)
            {
                
                advisorId = advisorVo.advisorId;
            }
        }
        protected void gvISArequest_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            gvISArequest.Visible = true;
            DataTable dt = new DataTable();

            btnExportFilteredData.Visible = true;
            dt = (DataTable)Cache["dtLiabilities + '" + advisorVo.advisorId + "'"];
            gvISArequest.DataSource = dt;
        }
        protected void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {

            gvISArequest.ExportSettings.OpenInNewWindow = true;
            gvISArequest.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvISArequest.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvISArequest.MasterTableView.ExportToExcel();

        }
        protected void BindGridview()
        {
            liabilitiesListVo = new List<LiabilitiesVo>();
            LiabilitiesVo liabilityVo = new LiabilitiesVo();
            liabilitiesListVo = liabilitiesBo.GetISAQueueList(advisorVo.advisorId);
            DataTable dt = new DataTable();
            DataRow dr;
            Double loanOutStanding = 0;
            DateTime nextInsDate = new DateTime();

            if (liabilitiesListVo != null)
            {
                btnExportFilteredData.Visible = true;
                trErrorMessage.Visible = false;
                dt.Columns.Add("AISAQ_RequestQueueid");
                dt.Columns.Add("AISAQ_date");
                dt.Columns.Add("AISAQ_Status");
                dt.Columns.Add("AISAQ_Priority");
                dt.Columns.Add("CustomerName");
                dt.Columns.Add("WWFSM_StepCode");
                dt.Columns.Add("AISAQD_Status");
                dt.Columns.Add("BranchName");
                           
                for (int i = 0; i < liabilitiesListVo.Count; i++)
                {
                    dr = dt.NewRow();
                    liabilityVo = liabilitiesListVo[i];
                    dr[0] = liabilityVo.ISARequestId;
                    if (liabilityVo.RequestDate != DateTime.MinValue)
                        dr[1] = liabilityVo.RequestDate.ToShortDateString();
                    if (liabilityVo.Status != null)
                        dr[2] = liabilityVo.Status;
                    if (liabilityVo.Priority != null)
                        dr[3] = liabilityVo.Priority;
                    if (liabilityVo.CustomerName != null)
                        dr[4] = liabilityVo.CustomerName;
                    if (liabilityVo.StepCode != null)
                        dr[5] = liabilityVo.StepCode;
                    if (liabilityVo.Status != null)
                        dr[6] = liabilityVo.Status;
                    if (liabilityVo.BranchName != null)
                        dr[7] = liabilityVo.BranchName;
                    
                }
                gvISArequest.DataSource = dt;
                gvISArequest.DataBind();

                if (Cache["dtLiabilities + '" + advisorVo.advisorId + "'"] == null)
                {
                    Cache.Insert("dtLiabilities + '" + advisorVo.advisorId + "'", dt);
                }
                else
                {
                    Cache.Remove("dtLiabilities + '" + advisorVo.advisorId + "'");
                    Cache.Insert("dtLiabilities + '" + advisorVo.advisorId + "'", dt);
                }

            }

            else
            {
                trErrorMessage.Visible = true;
                gvISArequest.Visible = false;
                btnExportFilteredData.Visible = false;
            }
        }
       
    }
    }
