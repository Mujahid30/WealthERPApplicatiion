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
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System.Xml;
using System.Text;
using iTextSharp.text.html.simpleparser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoCommon;
using System.Configuration;
using Telerik.Web.UI;
using VoUser;

namespace WealthERP.Advisor
{
    public partial class AdvisorMISCommission : System.Web.UI.UserControl
    {
        AdvisorMISBo advisorMISBo=new AdvisorMISBo();
        string path = string.Empty;
        DataSet dsMISCommission=new DataSet();
        UserVo userVo = new UserVo();
        DateBo dtBo = new DateBo();
        DateTime dtTo = new DateTime();
        DateTime dtFrom = new DateTime();
        AdvisorVo advisorVo = new AdvisorVo();

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!Page.IsPostBack)
            {
                BindPeriodDropDown();
                RadioButtonClick(sender, e);
            }
        }
        /// <summary>
        /// Binding Period Dropdown From Xml File
        /// </summary>        
        private void BindPeriodDropDown()
        {            
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            DataTable dtPeriod;
            dtPeriod = XMLBo.GetDatePeriod(path);
            ddlPeriod.DataSource = dtPeriod;
            ddlPeriod.DataTextField = "PeriodType";
            ddlPeriod.DataValueField = "PeriodCode";
            ddlPeriod.DataBind();
            ddlPeriod.Items.Insert(0, new RadComboBoxItem("Select a Period","0"));
            ddlPeriod.Items.Remove(15);
        }
        public void BindCommissionMISGrid()
        {
            DataTable dtMIS;
            //string misType = null;
            //ddlMISType.SelectedValue = misType;
            userVo=(UserVo)Session["userVo"];
            double sumTotal;
            if (hdnCurrentPage.Value.ToString() != "")
            {
            }
            dsMISCommission = advisorMISBo.GetMFMISCommission(advisorVo.advisorId, hdnMISType.Value.ToString(), DateTime.Parse(hdnFromDate.Value.ToString()), DateTime.Parse(hdnToDate.Value.ToString()), out sumTotal);            
            if (dsMISCommission.Tables[0].Rows.Count > 0)
            {
                dtMIS = dsMISCommission.Tables[0];
                string misType = hdnMISType.Value.ToString();                
                tblMessage.Visible = false;
                ErrorMessage.Visible = false;
                Label lblHeaderText = new Label();
                GridBoundColumn ghItem = gvCommissionMIS.MasterTableView.Columns.FindByUniqueName("MISType") as GridBoundColumn;                
                switch (misType)
                {
                    case "Folio Wise":
                        ghItem.HeaderText = "Folio Number";
                        break;
                    case "AMC Wise":
                        ghItem.HeaderText = "AMC Name";
                        break;
                    case "Transaction_Wise":
                        ghItem.HeaderText = "Transaction Classification Name";
                        break;
                    case "Category Wise":
                        ghItem.HeaderText = "Category";
                        break;
                    default:
                        ghItem.HeaderText = "Folio Number";                        
                        break;
                }
                
                gvCommissionMIS.DataSource = dtMIS;
                gvCommissionMIS.CurrentPageIndex = 0;
                gvCommissionMIS.DataBind();
                gvCommissionMIS.Visible = true;

                if (Cache["MIS"] == null)
                {
                    Cache.Insert("MIS", dtMIS);
                }
                else
                {
                    Cache.Remove("MIS");
                    Cache.Insert("MIS", dtMIS);
                }


            }
            else
            {
                gvCommissionMIS.Visible = false;                
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
            }           
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            hdnMISType.Value = ddlMISType.SelectedValue.ToString();
            CalculateDateRange(out dtFrom, out dtTo);
            hdnFromDate.Value = dtFrom.ToString();
            hdnToDate.Value = dtTo.ToString();
            hdnRecordCount.Value = "1";
            BindCommissionMISGrid();
        }
        /// <summary>
        /// Get the From and To Date of reports
        /// </summary>
        private void CalculateDateRange(out DateTime fromDate, out DateTime toDate)
        {
            if (rbtnPickDate.Checked)
            {
                fromDate = DateTime.Parse((txtFromDate.SelectedDate.Value).ToString());
                toDate = DateTime.Parse((txtToDate.SelectedDate.Value).ToString());
            }
            else if (rbtnPickPeriod.Checked)
            {
                dtBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue.ToString(), out dtFrom, out dtTo);
                fromDate = dtFrom;
                toDate = dtTo;
            }
            else
            {
                fromDate = DateTime.MinValue;
                toDate = DateTime.MinValue;
            }
        }
        
        public void RadioButtonClick(object sender, EventArgs e)
        {
            if (rbtnPickPeriod.Checked)
            {
                lblPeriod.Visible = true;
                ddlPeriod.Visible = true;
                lblFromDate.Visible = false;
                txtFromDate.Visible = false;
                lblToDate.Visible = false;
                txtToDate.Visible = false;
                PickADateValidation.Visible = false;
                PickAPeriodValidation.Visible = true;
            }
            else if (rbtnPickDate.Checked)
            {             
                lblPeriod.Visible = false;
                ddlPeriod.Visible = false;
                lblFromDate.Visible = true;
                txtFromDate.Visible = true;
                lblToDate.Visible = true;
                txtToDate.Visible = true;
                PickAPeriodValidation.Visible = false;
                PickADateValidation.Visible = true;
            }
        }

        public void gvCommissionMIS_OnNeedDataSource(object sender, EventArgs e)
        {

            DataTable dtMIS = new DataTable();
            dtMIS = (DataTable)Cache["MIS"];
            gvCommissionMIS.DataSource = dtMIS;
        }
    }
}