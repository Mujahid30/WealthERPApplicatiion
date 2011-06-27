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
        protected override void OnInit(EventArgs e)
        {
            try
            {
                ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
                mypager.EnableViewState = true;
                base.OnInit(e);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:OnInit()");
                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            try
            {
                GetPageCount();
                this.BindCommissionMISGrig();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:HandlePagerEvent()");
                object[] objects = new object[2];
                objects[0] = mypager.CurrentPage;                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //rbtnPickDate.Attributes.Add("onClick", "javascript:DisplayDates(value);");
            //rbtnPickPeriod.Attributes.Add("onClick", "javascript:DisplayDates(value);");
            if (!Page.IsPostBack)
            {
                hidDateType.Value = "DATE_RANGE";
                BindPeriodDropDown();
            }

            trPager.Visible = false;
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
            ddlPeriod.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select a Period", "Select a Period"));
        }
        public void BindCommissionMISGrig()
        {
            DataTable dtMIS;
            userVo=(UserVo)Session["userVo"];
            int count;
           double sumTotal;
            if (hdnCurrentPage.Value.ToString() != "")
            {
                mypager.CurrentPage = Int32.Parse(hdnCurrentPage.Value.ToString());
                hdnCurrentPage.Value = "";
            }
            dsMISCommission = advisorMISBo.GetMFMISCommission(userVo.UserId, hdnMISType.Value.ToString(), DateTime.Parse(hdnFromDate.Value.ToString()), DateTime.Parse(hdnToDate.Value.ToString()), mypager.CurrentPage, out count, out sumTotal);
            lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
            if (dsMISCommission.Tables[0].Rows.Count > 0)
            {
                dtMIS = dsMISCommission.Tables[0];
                gvCommissionMIS.DataSource = dtMIS;
                gvCommissionMIS.DataBind();
                gvCommissionMIS.Visible = true;
                lblCurrentPage.Visible = true;
                lblTotalRows.Visible = true;
                tblMessage.Visible = false;
                ErrorMessage.Visible = false;
                trPager.Visible = true;

                Label lblHeaderText = new Label();
                lblHeaderText = (Label)gvCommissionMIS.HeaderRow.FindControl("lblHeaderText");
                string misType = hdnMISType.Value.ToString();
                switch (misType)
                {
                    case "Folio Wise":
                        lblHeaderText.Text = "Folio Number";
                        break;
                    case "AMC Wise":
                        lblHeaderText.Text = "AMC Name";
                        break;
                    case "Transaction_Wise":
                        lblHeaderText.Text = "Transaction Classification Name";
                        break;
                    case "Category Wise":
                        lblHeaderText.Text = "Category";
                        break;

                    default:
                        lblHeaderText.Text = "Folio Number";
                        break;
                }

                Label lblTotalText = (Label)gvCommissionMIS.FooterRow.FindControl("lblTotalValue");
                lblTotalText.Text = lblTotalText.Text + " " + decimal.Parse(sumTotal.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")).ToString(); 
                this.GetPageCount();
            }
            else
            {
                gvCommissionMIS.Visible = false;
                lblCurrentPage.Visible = false;
                lblTotalRows.Visible = false;
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
                trPager.Visible = false;
             
            }
           
        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            hdnMISType.Value = ddlMISType.SelectedValue.ToString();
            CalculateDateRange(out dtFrom, out dtTo);
            hdnFromDate.Value = dtFrom.ToString();
            hdnToDate.Value = dtTo.ToString();
            hdnRecordCount.Value = "1";
            GetPageCount();
            BindCommissionMISGrig();
        }
        /// <summary>
        /// Get the From and To Date of reports
        /// </summary>
        private void CalculateDateRange(out DateTime fromDate, out DateTime toDate)
        {
            if (hidDateType.Value.ToString() == "DATE_RANGE")
            {
                fromDate =DateTime.Parse(txtFromDate.Text);
                toDate = DateTime.Parse(txtToDate.Text);
            }
            else if (hidDateType.Value.ToString() == "PERIOD")
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

        private void GetPageCount()
        {
            string upperlimit = null;
            int rowCount = 0;
            int ratio = 0;
            string lowerlimit = null;
            string PageRecords = null;
            try
            {
                if (hdnRecordCount.Value.ToString() != "")
                    rowCount = Convert.ToInt32(hdnRecordCount.Value);
                if (rowCount > 0)
                {
                    ratio = rowCount /15;
                    mypager.PageCount = rowCount % 15 == 0 ? ratio : ratio + 1;
                    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                    if (((mypager.CurrentPage - 1) * 15) != 0)
                        lowerlimit = (((mypager.CurrentPage - 1) * 15) + 1).ToString();
                    else
                        lowerlimit = "1";
                    upperlimit = (mypager.CurrentPage * 15).ToString();
                    if (mypager.CurrentPage == mypager.PageCount)
                        upperlimit = hdnRecordCount.Value;
                    PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
                    lblCurrentPage.Text = PageRecords;
                    hdnCurrentPage.Value = mypager.CurrentPage.ToString();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserCustomer.ascx.cs:GetPageCount()");

                object[] objects = new object[5];
                objects[0] = upperlimit;
                objects[1] = rowCount;
                objects[2] = ratio;
                objects[3] = lowerlimit;
                objects[4] = PageRecords;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

     
    }
}