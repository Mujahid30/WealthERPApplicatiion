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
using VoCustomerProfiling;
using BoSuperAdmin;
using System.Configuration;

namespace WealthERP.SuperAdmin
{
    public partial class DailyValuationMonitor : System.Web.UI.UserControl
    {
       string path = string.Empty;
       DateBo dtBo = new DateBo();
       DateTime dtTo = new DateTime();
       DateTime dtFrom = new DateTime();
       SuperAdminOpsBo superAdminOpsBo = new SuperAdminOpsBo();
       int count;

       protected override void OnInit(EventArgs e)
       {
           try
           {
               ((Pager)mypagerDuplicate).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
               mypagerDuplicate.EnableViewState = true;
               base.OnInit(e);

               ((Pager)mypagerAUM).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
               mypagerAUM.EnableViewState = true;
               base.OnInit(e);

               ((Pager)pgrReject).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
               pgrReject.EnableViewState = true;
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
               FunctionInfo.Add("Method", "UnderConstruction.ascx.cs:OnInit()");
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
               GetPageCountDuplicate();
               GetPageCountAUM();
               if (ddlAction.SelectedValue == "DuplicateMis")
               {
                  this.BindDuplicateGrid();
               }
               else if (ddlAction.SelectedValue == "AumMis")
               {
                   this.BindAUMGrid();
               }
               else if (ddlAction.SelectedValue == "mfRejects")
               {
                   this.BindMFRejectedGrid();
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
               FunctionInfo.Add("Method", "UnderConstruction.ascx.cs:HandlePagerEvent()");
               object[] objects = new object[2];
               objects[0] = mypagerDuplicate.CurrentPage;
               FunctionInfo = exBase.AddObject(FunctionInfo, objects);
               exBase.AdditionalInformation = FunctionInfo;
               ExceptionManager.Publish(exBase);
               throw exBase;
           }
       }

       protected void Page_Load(object sender, EventArgs e)
        {
            btnGo.Attributes.Add("onclick", "setTimeout(\"UpdateImg('Image1','/Images/Wait.gif');\",50);");
            if (!Page.IsPostBack)
            {
                trRange.Visible = true;
                if (rbtnPickDate.Checked == true)
                {
                    trRange.Visible = true;
                    txtFromDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
                    txtToDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
                    trPeriod.Visible = false;
                }
                btnDelete.Visible = false;
                lblCurrentPage.Visible = false;
                lblTotalRows.Visible = false;
                lblPage.Visible = false;
                lblTotalPage.Visible=false;
                trpagerDuplicate.Visible = false;
                trmypagerAUM.Visible = false;
                trPagerReject.Visible = false;
                lblRejectCount.Visible = false;
                lblRejectTotal.Visible = false;
                hidDateType.Value = "DATE_RANGE";
                pnlReject.Visible = false;
                BindPeriodDropDown();
            }
        }

        /// <summary>
        /// Bind the Period Drop down.
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
           ddlPeriod.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
           ddlPeriod.Items.RemoveAt(15);
       }
       protected void rbtnDate_CheckedChanged(object sender, EventArgs e)
       {
           if (rbtnPickDate.Checked == true)
           {
               trRange.Visible = true;
               txtFromDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
               txtToDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
               trPeriod.Visible = false;
           }
           else if (rbtnPickPeriod.Checked == true)
           {
               trRange.Visible = false;
               trPeriod.Visible = true;
               BindPeriodDropDown();
           }
       }

       protected void btnGo_Click(object sender, EventArgs e)
       {
           CalculateDateRange(out dtFrom, out dtTo);
           hdnFromDate.Value = dtFrom.ToString();
           hdnToDate.Value = dtTo.ToString();
           if (ddlAction.SelectedValue == "DuplicateMis")
           {
               BindDuplicateGrid();
               
           }
           else if (ddlAction.SelectedValue == "AumMis")
           {
               BindAUMGrid();
               btnDelete.Visible = false;
           }
           else if (ddlAction.SelectedValue == "mfRejects")
           {
               BindMFRejectedGrid();
               btnDelete.Visible = false;
           }
       }
        /// <summary>
        ///  To Get All adviser's total AUM
        /// </summary>
       private void BindAUMGrid()
       {
           Dictionary<string, string> genOrganizationData = new Dictionary<string, string>();
           DataSet dsAumMis= new DataSet();
           DataTable dtAumMis;

           dsAumMis = superAdminOpsBo.GetAllAdviserAUM(DateTime.Parse(hdnFromDate.Value.ToString()), DateTime.Parse(hdnToDate.Value.ToString()), mypagerAUM.CurrentPage, out count, hdnAdviserNameAUMFilter.Value);
           dtAumMis = dsAumMis.Tables[0];
           if (dtAumMis.Rows.Count > 0)
           {
               lblTotalPage.Text = hdnRecordCount.Value = count.ToString();
               gvAumMis.DataSource = dtAumMis;
               gvAumMis.DataBind();
               gvAumMis.Visible = true;
               gvDuplicateCheck.Visible = false;
               lblCurrentPage.Visible = false;
               lblTotalRows.Visible = false;
               lblPage.Visible = true;
               lblTotalPage.Visible = true;
               trpagerDuplicate.Visible = false;
               trmypagerAUM.Visible = true;
               btnDelete.Visible = false;
               gvMFRejectedDetails.Visible = false;
               foreach (DataRow dr in dsAumMis.Tables[1].Rows)
               {
                   genOrganizationData.Add(dr[0].ToString(), dr[0].ToString());
               }
               DropDownList ddlAdviserNameDate = GetOrganization();
               if (ddlAdviserNameDate != null)
               {
                   ddlAdviserNameDate.DataSource = genOrganizationData;
                   ddlAdviserNameDate.DataTextField = "Key";
                   ddlAdviserNameDate.DataValueField = "Value";
                   ddlAdviserNameDate.DataBind();
                   ddlAdviserNameDate.Items.Insert(0, new ListItem("Select", "Select"));
               }
               if (hdnAdviserNameAUMFilter.Value != "")
               {
                   ddlAdviserNameDate.SelectedValue = hdnAdviserNameAUMFilter.Value;
               }
               this.GetPageCountAUM();
               trPagerReject.Visible = false;
               lblRejectCount.Visible = false;
               lblRejectTotal.Visible = false;
               pnlReject.Visible = false;
           }
           else
           {
               tblMessage.Visible = true;
               ErrorMessage.Visible = true;
               ErrorMessage.InnerText = "No Records Found...!";
               gvDuplicateCheck.Visible = false;
               gvAumMis.Visible = false;
               lblCurrentPage.Visible = false;
               lblTotalRows.Visible = false;
               lblPage.Visible = false;
               lblTotalPage.Visible = false;
               trpagerDuplicate.Visible = false;
               trmypagerAUM.Visible = false;
               btnDelete.Visible = false;
               gvMFRejectedDetails.Visible = false;
               trPagerReject.Visible = false;
               lblRejectCount.Visible = false;
               lblRejectTotal.Visible = false;
               pnlReject.Visible = false;
           }
       }

       private DropDownList GetOrganization()
       {
           DropDownList ddl = new DropDownList();
           if ((DropDownList)gvAumMis.HeaderRow.FindControl("ddlAdviserNameDate") != null)
           {
               ddl = (DropDownList)gvAumMis.HeaderRow.FindControl("ddlAdviserNameDate");
           }
           return ddl;
       }
       /// <summary>
       /// check the duplicate records
       /// </summary>
 
       private void BindDuplicateGrid()
       {
           Dictionary<string, string> genAdviserDdl = new Dictionary<string, string>();
           DataSet dsduplicatecheck;
           DataTable dtDuplicateCheck;
           dsduplicatecheck = superAdminOpsBo.GetAllAdviserDuplicateRecords(DateTime.Parse(hdnFromDate.Value.ToString()), DateTime.Parse(hdnToDate.Value.ToString()), mypagerDuplicate.CurrentPage, out  count, hdnAdviserIdDupli.Value, hdnOrgNameDupli.Value,hdnFolioiNoDupli.Value, hdnSchemeDupli.Value);
           dtDuplicateCheck = dsduplicatecheck.Tables[0];
           if (dtDuplicateCheck.Rows.Count > 0)
           {
               lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
               gvDuplicateCheck.DataSource = dtDuplicateCheck;
               gvDuplicateCheck.DataBind();
               gvDuplicateCheck.Visible = true;
               gvAumMis.Visible = false;
               tblMessage.Visible = false;
               ErrorMessage.Visible = false;
               lblCurrentPage.Visible = true;
              

               foreach (DataRow dr in dsduplicatecheck.Tables[1].Rows)
               {
                   genAdviserDdl.Add(dr[0].ToString(), dr[0].ToString());
               }
               DropDownList ddlOrganization = GetAdviserDdl();
               if (ddlOrganization != null)
               {
                   ddlOrganization.DataSource = genAdviserDdl;
                   ddlOrganization.DataTextField = "Key";
                   ddlOrganization.DataValueField = "Value";
                   ddlOrganization.DataBind();
                   ddlOrganization.Items.Insert(0, new ListItem("Select", "Select"));
               }
               if (hdnOrgNameDupli.Value != "")
               {
                   ddlOrganization.SelectedValue = hdnOrgNameDupli.Value;
               }
               BindFolioNumber(dsduplicatecheck.Tables[2]);
               BindSchemeName(dsduplicatecheck.Tables[3]);
               BindAdviserIdDuplicate(dsduplicatecheck.Tables[4]);
               this.GetPageCountDuplicate();
               lblTotalRows.Visible = true;
               lblPage.Visible = false;
               lblTotalPage.Visible = false;
               trmypagerAUM.Visible = false;
               trpagerDuplicate.Visible = true;
               btnDelete.Visible = true;
               gvMFRejectedDetails.Visible = false;
               trPagerReject.Visible = false;
               lblRejectCount.Visible = false;
               lblRejectTotal.Visible = false;
               pnlReject.Visible = false;
           }
           else
           {
               gvDuplicateCheck.Visible = false;
               gvAumMis.Visible = false;
               gvMFRejectedDetails.Visible = false;
               tblMessage.Visible = true;
               ErrorMessage.Visible = true;
               ErrorMessage.InnerText = "No Records Found...!";
               lblCurrentPage.Visible = false;
               lblTotalRows.Visible = false;
               lblPage.Visible = false;
               lblTotalPage.Visible = false;
               trmypagerAUM.Visible = false;
               trpagerDuplicate.Visible = false;
               btnDelete.Visible = false;
               trPagerReject.Visible = false;
               lblRejectCount.Visible = false;
               lblRejectTotal.Visible = false;
               pnlReject.Visible = false;
           }
       }

       private void BindAdviserIdDuplicate(DataTable dtAdviserDupli)
       {
           Dictionary<string, string> genAdviserIdDupli = new Dictionary<string, string>();
           if (dtAdviserDupli.Rows.Count > 0)
           {
               // Get the Reject Reason Codes Available into Generic Dictionary
               foreach (DataRow dr in dtAdviserDupli.Rows)
               {
                   genAdviserIdDupli.Add(dr[0].ToString(), dr[0].ToString());
               }

               DropDownList ddlSchemeName = GetAdviserIdddlDupli();
               if (ddlSchemeName != null)
               {
                   ddlSchemeName.DataSource = genAdviserIdDupli;
                   ddlSchemeName.DataTextField = "Key";
                   ddlSchemeName.DataValueField = "Value";
                   ddlSchemeName.DataBind();
                   ddlSchemeName.Items.Insert(0, new ListItem("Select", "Select"));
               }

               if (hdnAdviserIdDupli.Value != "")
               {
                   ddlSchemeName.SelectedValue = hdnAdviserIdDupli.Value.ToString();
               }
           }
       }

       private DropDownList GetAdviserIdddlDupli()
       {
           DropDownList ddl = new DropDownList();
           if ((DropDownList)gvDuplicateCheck.HeaderRow.FindControl("ddlAdviserId") != null)
           {
               ddl = (DropDownList)gvDuplicateCheck.HeaderRow.FindControl("ddlAdviserId");
           }
           return ddl;
       }
       private DropDownList GetAdviserDdl()
       {
           DropDownList ddl = new DropDownList();
           if ((DropDownList)gvDuplicateCheck.HeaderRow.FindControl("ddlOrganization") != null)
           {
               ddl = (DropDownList)gvDuplicateCheck.HeaderRow.FindControl("ddlOrganization");
           }
           return ddl;
       }


       /// <summary>
       /// Get the From and To Date of reports
       /// </summary>
       private void CalculateDateRange(out DateTime fromDate, out DateTime toDate)
       {
           if (rbtnPickDate.Checked == true)
           {
               fromDate = DateTime.Parse(txtFromDate.Text);
               toDate = DateTime.Parse(txtToDate.Text);
           }
           else if (rbtnPickPeriod.Checked == true)
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
       //protected void gvDuplicateCheck_RowDataBound(object sender, GridViewRowEventArgs e)
       //{
       //    if (e.Row.RowType == DataControlRowType.DataRow)
       //    {
       //        Label lblDuplicate = e.Row.FindControl("lblDuplicate") as Label;
       //        string duplicate = null;
       //        duplicate = lblDuplicate.Text;
       //        if (duplicate == "1")
       //            lblDuplicate.Text = "No";
       //        else
       //            lblDuplicate.Text = "Yes";
       //    }
       //}
       private void GetPageCountDuplicate()
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
                   ratio = rowCount / 10;
                   mypagerDuplicate.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                   mypagerDuplicate.Set_Page(mypagerDuplicate.CurrentPage, mypagerDuplicate.PageCount);
                   if (((mypagerDuplicate.CurrentPage - 1) * 10) != 0)
                       lowerlimit = (((mypagerDuplicate.CurrentPage - 1) * 10) + 1).ToString();
                   else
                       lowerlimit = "1";
                   upperlimit = (mypagerDuplicate.CurrentPage * 10).ToString();
                   if (mypagerDuplicate.CurrentPage == mypagerDuplicate.PageCount)
                       upperlimit = hdnRecordCount.Value;
                   PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
                   lblCurrentPage.Text = PageRecords;
                   lblPage.Text = PageRecords;
                   hdnCurrentPage.Value = mypagerDuplicate.CurrentPage.ToString();
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
       private void BindFolioNumber(DataTable dtFolioNumber)
       {
           Dictionary<string, string> genDictFolioNumber = new Dictionary<string, string>();
           if (dtFolioNumber.Rows.Count > 0)
           {
               foreach (DataRow dr in dtFolioNumber.Rows)
               {
                   genDictFolioNumber.Add(dr[0].ToString(), dr[0].ToString());
               }

               DropDownList ddlFolioNumber = GetFolioNumberDupli();
               if (ddlFolioNumber != null)
               {
                   ddlFolioNumber.DataSource = genDictFolioNumber;
                   ddlFolioNumber.DataTextField = "Key";
                   ddlFolioNumber.DataValueField = "Value";
                   ddlFolioNumber.DataBind();
                   ddlFolioNumber.Items.Insert(0, new ListItem("Select", "Select"));
               }

               if (hdnFolioiNoDupli.Value != "")
               {
                   ddlFolioNumber.SelectedValue = hdnFolioiNoDupli.Value.ToString();
               }
           }
       }
       private DropDownList GetFolioNumberDupli()
       {
           DropDownList ddl = new DropDownList();
           if ((DropDownList)gvDuplicateCheck.HeaderRow.FindControl("ddlFolioNo") != null)
           {
               ddl = (DropDownList)gvDuplicateCheck.HeaderRow.FindControl("ddlFolioNo");
           }
           return ddl;
       }
       private void BindSchemeName(DataTable dtSchemeName)
       {
           Dictionary<string, string> genDictSchemeName = new Dictionary<string, string>();
           if (dtSchemeName.Rows.Count > 0)
           {
               // Get the Reject Reason Codes Available into Generic Dictionary
               foreach (DataRow dr in dtSchemeName.Rows)
               {
                   genDictSchemeName.Add(dr[0].ToString(), dr[0].ToString());
               }

               DropDownList ddlSchemeName = GetSchemeNameDDL();
               if (ddlSchemeName != null)
               {
                   ddlSchemeName.DataSource = genDictSchemeName;
                   ddlSchemeName.DataTextField = "Key";
                   ddlSchemeName.DataValueField = "Value";
                   ddlSchemeName.DataBind();
                   ddlSchemeName.Items.Insert(0, new ListItem("Select", "Select"));
               }

               if (hdnSchemeDupli.Value != "")
               {
                   ddlSchemeName.SelectedValue = hdnSchemeDupli.Value.ToString();
               }
           }
       }
       private DropDownList GetSchemeNameDDL()
       {
           DropDownList ddl = new DropDownList();
           if ((DropDownList)gvDuplicateCheck.HeaderRow.FindControl("ddlScheme") != null)
           {
               ddl = (DropDownList)gvDuplicateCheck.HeaderRow.FindControl("ddlScheme");
           }
           return ddl;
       }

       protected void ddlAdviserNameDate_SelectedIndexChanged(object sender, EventArgs e)
       {
           DropDownList ddlAdviserName = GetOrganization();

           if (ddlAdviserName != null)
           {
               if (ddlAdviserName.SelectedIndex != 0)
               {   // Bind the Grid with Only Selected Values
                   hdnAdviserNameAUMFilter.Value = ddlAdviserName.SelectedItem.Text;
                   BindAUMGrid();
                   // ddlAdviserName.SelectedItem.Text = hdnAdviserNameAUMFilter.Value;
               }
               else
               {   // Bind the Grid with Only All Values
                   hdnAdviserNameAUMFilter.Value = "";
                   BindAUMGrid();
               }
           }
       }
       private void GetPageCountAUM()
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
                   ratio = rowCount / 10;
                   mypagerAUM.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                   mypagerAUM.Set_Page(mypagerAUM.CurrentPage, mypagerAUM.PageCount);
                   if (((mypagerDuplicate.CurrentPage - 1) * 10) != 0)
                       lowerlimit = (((mypagerAUM.CurrentPage - 1) * 10) + 1).ToString();
                   else
                       lowerlimit = "1";
                   upperlimit = (mypagerAUM.CurrentPage * 10).ToString();
                   if (mypagerAUM.CurrentPage == mypagerAUM.PageCount)
                       upperlimit = hdnRecordCount.Value;
                   PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
                   lblCurrentPage.Text = PageRecords;
                   lblPage.Text = PageRecords;
                   hdnCurrentPage.Value = mypagerAUM.CurrentPage.ToString();
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
        /// <summary>
        /// Bind reject reason grid
        /// </summary>
       private void BindMFRejectedGrid()
       {
           Dictionary<string, string> genDictRejectReason = new Dictionary<string, string>();

           Dictionary<string, string> genDictProcessId = new Dictionary<string, string>();

           Dictionary<string, string> genDictAdviserId = new Dictionary<string, string>();

           DataSet dsRejectedRecords = new DataSet();
           dsRejectedRecords = superAdminOpsBo.GetMfrejectedDetails(DateTime.Parse(hdnFromDate.Value.ToString()), DateTime.Parse(hdnToDate.Value.ToString()), pgrReject.CurrentPage, out  count, hdnRejectReasonFilter.Value, hdnAdviserIdFilter.Value, hdnProcessIdFilter.Value);
           if (dsRejectedRecords.Tables[0].Rows.Count > 0)
           {
               lblRejectTotal.Text = hdnRecordCount.Value = count.ToString();
               gvMFRejectedDetails.DataSource = dsRejectedRecords.Tables[0];
               gvMFRejectedDetails.DataBind();
               foreach (DataRow dr in dsRejectedRecords.Tables[1].Rows)
               {
                   genDictRejectReason.Add(dr["WRR_RejectReasonDescription"].ToString(), dr[1].ToString());
               }
               DropDownList ddlRejectReason = GetRejectReasonDDL();
               if (ddlRejectReason != null)
               {
                   ddlRejectReason.DataSource = genDictRejectReason;
                   ddlRejectReason.DataTextField = "Key";
                   ddlRejectReason.DataValueField = "Value";
                   ddlRejectReason.DataBind();
                   ddlRejectReason.Items.Insert(0, new ListItem("Select", "Select"));
               }
               foreach (DataRow dr in dsRejectedRecords.Tables[2].Rows)
               {
                   genDictProcessId.Add(dr[0].ToString(), dr[0].ToString());
               }
               DropDownList ddlProcessid = GetProcessIdDDL();
               if (ddlRejectReason != null)
               {
                   ddlProcessid.DataSource = genDictProcessId;
                   ddlProcessid.DataTextField = "Key";
                   ddlProcessid.DataValueField = "Value";
                   ddlProcessid.DataBind();
                   ddlProcessid.Items.Insert(0, new ListItem("Select", "Select"));
               }
               foreach (DataRow dr in dsRejectedRecords.Tables[3].Rows)
               {
                   genDictAdviserId.Add(dr["A_AdviserId"].ToString(), dr[1].ToString());
               }
               DropDownList ddlAdviserId = GetAdviserIdDDL();
               if (ddlAdviserId != null)
               {
                   ddlAdviserId.DataSource = genDictAdviserId;
                   ddlAdviserId.DataTextField = "Key";
                   ddlAdviserId.DataValueField = "Value";
                   ddlAdviserId.DataBind();
                   ddlAdviserId.Items.Insert(0, new ListItem("Select", "Select"));
               }
               gvMFRejectedDetails.Visible = true;
               gvDuplicateCheck.Visible = false;
               this.GetPageCountReject();
               gvAumMis.Visible = false;
               tblMessage.Visible = false;
               ErrorMessage.Visible = false;
               lblCurrentPage.Visible = false;
               lblTotalRows.Visible = false;
               lblPage.Visible = false;
               lblTotalPage.Visible = false;
               trmypagerAUM.Visible = false;
               trpagerDuplicate.Visible = false;
               trPagerReject.Visible = true;
               lblRejectCount.Visible = true;
               lblRejectTotal.Visible = true;
               pnlReject.Visible = true;

               if (hdnRejectReasonFilter.Value != "")
               {
                   ddlRejectReason.SelectedValue = hdnRejectReasonFilter.Value;
               }
               if (hdnAdviserIdFilter.Value != "")
               {
                   ddlAdviserId.SelectedValue = hdnAdviserIdFilter.Value;
               }

               if (hdnProcessIdFilter.Value != "")
               {
                   ddlProcessid.SelectedValue = hdnProcessIdFilter.Value;
               }
           }
           else
           {
               gvMFRejectedDetails.Visible = false;
               gvDuplicateCheck.Visible = false;
               gvAumMis.Visible = false;
               tblMessage.Visible = true;
               ErrorMessage.Visible = true;
               ErrorMessage.InnerText = "No Records Found...!";
               lblCurrentPage.Visible = false;
               lblTotalRows.Visible = false;
               lblPage.Visible = false;
               lblTotalPage.Visible = false;
               trmypagerAUM.Visible = false;
               trpagerDuplicate.Visible = false;
               btnDelete.Visible = false;
               trPagerReject.Visible = false;
               lblRejectCount.Visible = false;
               lblRejectTotal.Visible = false;
               pnlReject.Visible = false;
           }

          
          
       }
       private DropDownList GetRejectReasonDDL()
       {
           DropDownList ddl = new DropDownList();
           if ((DropDownList)gvMFRejectedDetails.HeaderRow.FindControl("ddlRejectReason") != null)
           {
               ddl = (DropDownList)gvMFRejectedDetails.HeaderRow.FindControl("ddlRejectReason");
           }
           return ddl;
       }
       private DropDownList GetProcessIdDDL()
       {
           DropDownList ddl = new DropDownList();
           if ((DropDownList)gvMFRejectedDetails.HeaderRow.FindControl("ddlProcessId") != null)
           {
               ddl = (DropDownList)gvMFRejectedDetails.HeaderRow.FindControl("ddlProcessId");
           }
           return ddl;
       }

       private DropDownList GetAdviserIdDDL()
       {
           DropDownList ddl = new DropDownList();
           if ((DropDownList)gvMFRejectedDetails.HeaderRow.FindControl("ddlAdviserId") != null)
           {
               ddl = (DropDownList)gvMFRejectedDetails.HeaderRow.FindControl("ddlAdviserId");
           }
           return ddl;
       }
       protected void ddlRejectReason_SelectedIndexChanged(object sender, EventArgs e)
       {
           DropDownList ddlReject = GetRejectReasonDDL();
           if (ddlReject != null)
           {
               if (ddlReject.SelectedIndex != 0)
               {   // Bind the Grid with Only Selected Values
                   hdnRejectReasonFilter.Value = ddlReject.SelectedValue;
                   BindMFRejectedGrid();
               }
               else
               {   // Bind the Grid with Only All Values
                   hdnRejectReasonFilter.Value = "";
                   BindMFRejectedGrid();
               }
           }
       }
       protected void ddlProcessId_SelectedIndexChanged(object sender, EventArgs e)
       {
           DropDownList ddlProcessId = GetProcessIdDDL();
           if (ddlProcessId != null)
           {
               if (ddlProcessId.SelectedIndex != 0)
               {   // Bind the Grid with Only Selected Values
                   hdnProcessIdFilter.Value = ddlProcessId.SelectedValue;
                   BindMFRejectedGrid();
               }
               else
               {   // Bind the Grid with Only All Values
                   hdnProcessIdFilter.Value = "";
                   BindMFRejectedGrid();
               }
           }
       }
       protected void ddlAdviserIdDuplicate_SelectedIndexChanged(object sender, EventArgs e)
       {
           DropDownList ddladviserIdDupli = GetAdviserIdddlDupli();
           if (ddladviserIdDupli != null)
           {
               if (ddladviserIdDupli.SelectedIndex != 0)
               {
                   hdnAdviserIdDupli.Value = ddladviserIdDupli.SelectedValue;
                   BindDuplicateGrid();
               }
               else
               {
                   hdnAdviserIdDupli.Value = "";
                   BindDuplicateGrid();
               }
           }
       }

       protected void ddlAdviserId_SelectedIndexChanged(object sender, EventArgs e)
       {
           DropDownList ddlAdviserId = GetAdviserIdDDL();
           if (ddlAdviserId != null)
           {
               if (ddlAdviserId.SelectedIndex != 0)
               {   // Bind the Grid with Only Selected Values
                   hdnAdviserIdFilter.Value = ddlAdviserId.SelectedValue;
                   BindMFRejectedGrid();
               }
               else
               {   // Bind the Grid with Only All Values
                   hdnAdviserIdFilter.Value = "";
                   BindMFRejectedGrid();
               }
           }
       }

       protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
       {
           DropDownList ddlOrgDupli = GetAdviserDdl();
           if (ddlOrgDupli != null)
           {
               if (ddlOrgDupli.SelectedIndex != 0)
               {
                   hdnOrgNameDupli.Value = ddlOrgDupli.SelectedValue;
                   BindDuplicateGrid();
               }
               else
               {
                   hdnOrgNameDupli.Value = "";
                   BindDuplicateGrid();
               }
           }
       }
       protected void ddlFolioNo_SelectedIndexChanged(object sender, EventArgs e)
       {
           DropDownList ddlFolioNoDupli = GetFolioNumberDupli();
           if (ddlFolioNoDupli != null)
           {
               if (ddlFolioNoDupli.SelectedIndex != 0)
               {
                   hdnFolioiNoDupli.Value = ddlFolioNoDupli.SelectedValue;
                   BindDuplicateGrid();
               }
               else
               {
                   hdnFolioiNoDupli.Value = "";
                   BindDuplicateGrid();
               }
           }
       }
       protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
       {
           DropDownList ddlSchemeNameDupli = GetSchemeNameDDL();
           if (ddlSchemeNameDupli != null)
           {
               if (ddlSchemeNameDupli.SelectedIndex != 0)
               {
                   hdnSchemeDupli.Value = ddlSchemeNameDupli.SelectedValue;
                   BindDuplicateGrid();
               }
               else
               {
                   hdnSchemeDupli.Value = "";
                   BindDuplicateGrid();
               }
           }
       }
       protected void btnDelete_Click(object sender, EventArgs e)
       {
          
           int i=0;
          
           foreach (GridViewRow gvRow in gvDuplicateCheck.Rows)
           {

               CheckBox chk = (CheckBox)gvRow.FindControl("chkDelete");
               if (chk.Checked)
               {
                   i++;
               }
               
           }
           if (i == 0)
           {
               ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a record to delete!');", true);
           }
           else
           {
               DuplicateDelete();
               ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Record has been deleted successfully.');", true);
           }
       }
       private void DuplicateDelete()
       {
           int gvAdviserId = 0;
           int gvAccountId = 0;
           double gvNetHolding = 0;
           int gvSchemeCode = 0;
           DateTime gvValuationDate = new DateTime();
           foreach (GridViewRow gvRow in gvDuplicateCheck.Rows)
           {

               if (((CheckBox)gvRow.FindControl("chkDelete")).Checked == true)
               {
                   gvAdviserId = Convert.ToInt32(gvDuplicateCheck.DataKeys[gvRow.RowIndex].Values["A_AdviserId"].ToString());
                   gvAccountId = Convert.ToInt32(gvDuplicateCheck.DataKeys[gvRow.RowIndex].Values["CMFA_AccountId"].ToString());
                   gvNetHolding = double.Parse(gvDuplicateCheck.DataKeys[gvRow.RowIndex].Values["CMFNP_NetHoldings"].ToString());
                   gvSchemeCode = Convert.ToInt32(gvDuplicateCheck.DataKeys[gvRow.RowIndex].Values["PASP_SchemePlanCode"].ToString());
                   gvValuationDate = DateTime.Parse(gvDuplicateCheck.DataKeys[gvRow.RowIndex].Values["CMFNP_ValuationDate"].ToString());

                   superAdminOpsBo.DeleteDuplicateRecord(gvAdviserId, gvAccountId, gvNetHolding, gvSchemeCode, gvValuationDate);

                   BindDuplicateGrid();
               }
           }
       }
       private void GetPageCountReject()
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
                   ratio = rowCount / 10;
                   pgrReject.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
                   pgrReject.Set_Page(pgrReject.CurrentPage, pgrReject.PageCount);
                   if (((pgrReject.CurrentPage - 1) * 10) != 0)
                       lowerlimit = (((pgrReject.CurrentPage - 1) * 10) + 1).ToString();
                   else
                       lowerlimit = "1";
                   upperlimit = (pgrReject.CurrentPage * 10).ToString();
                   if (pgrReject.CurrentPage == pgrReject.PageCount)
                       upperlimit = hdnRecordCount.Value;
                   PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
                   lblCurrentPage.Text = PageRecords;
                   lblRejectCount.Text = PageRecords;
                   hdnCurrentPage.Value = pgrReject.CurrentPage.ToString();
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

       protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
       {
           if (IsPostBack)
           {
               tblMessage.Visible = false;
           }
       }
    }
}