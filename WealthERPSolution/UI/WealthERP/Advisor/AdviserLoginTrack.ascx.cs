using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using System.Configuration;
using WealthERP.Base;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using Telerik.Web.UI;

namespace WealthERP.Advisor
{
    public partial class AdviserLoginTrack : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo = null;
        RMVo rmVo = null;
        CustomerVo customerVo = new CustomerVo();
        AdvisorVo advisorVo = new AdvisorVo();
        DataSet dsBindLoginTrack = new DataSet();
        DataTable dtLoginTrack = new DataTable();
        DateTime fromDate = new DateTime();
        DateTime ToDate = new DateTime();
        int AdviserId;
        String userType;
        string currentUserRole = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userBo = new UserBo();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            RMVo customerRMVo = new RMVo();
            gvLoginTrack.ShowHeader = true;
            //gvLoginTrack.GridLines = GridLines.None;
            if (!IsPostBack)
            {
                gvLoginTrack.Visible = false;
                DateTime fromDate = DateTime.Now.AddDays(-1);
                txtFrom.SelectedDate = fromDate;
                txtTo.SelectedDate = DateTime.Now;
            }
            //Session["ButtonGo"] = null;
            //if (Session["ButtonGo"] != null)
            //    CallAllGridBinding();   
        }
     protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {         
        }
        private void SetParameter()
            {
                if (txtFrom.SelectedDate.ToString() != "")
                {
                    hdnFromDate.Value = txtFrom.SelectedDate.ToString();
                    ViewState["txtFromDate"] = txtFrom.SelectedDate.ToString();
                }
                else if (ViewState["txtFromDate"].ToString() != null)
                {
                    hdnFromDate.Value = DateTime.Parse(ViewState["txtFromDate"].ToString()).ToString();
                }
                else
                    hdnFromDate.Value = DateTime.MinValue.ToString();

                if (txtTo.SelectedDate.ToString() != "")
                {
                    hdnTodate.Value = txtTo.SelectedDate.ToString();
                    ViewState["txtToDate"] = txtTo.SelectedDate.ToString();
                }
                else if (ViewState["txtToDate"].ToString() != null)
                {
                    hdnTodate.Value = DateTime.Parse(ViewState["txtToDate"].ToString()).ToString();
                }
                else
                    hdnTodate.Value = DateTime.MinValue.ToString();
        }
        private void GetData()
        {
            //userType = ddlCategory.SelectedValue;
            //dsBindLoginTrack = userBo.GetLoginDetail(userType, advisorVo.advisorId, DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnTodate.Value));
        }
        protected void CallAllGridBinding()
        {
            SetParameter();
            BindgvLoginTrack();
            //if (dsBindLoginTrack.Tables.Count != 0)
            //{   tblMessage.Visible = false;
            //    ErrorMessage.Visible = false;
               
            //}
            //else
            //{
            //    tblMessage.Visible = true;
            //    ErrorMessage.Visible = true;
            //    ErrorMessage.InnerText = "No Records Found...!";
            //}      
        }
        private void BindgvLoginTrack()
        {
            try
            {
                userType = ddlCategory.SelectedValue;
                dsBindLoginTrack = userBo.GetLoginDetail(userType, advisorVo.advisorId, DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnTodate.Value));
                if (dsBindLoginTrack.Tables.Count != 0)
                {                  
                    if (Cache["gvLoginTrack + '" + advisorVo.advisorId + "'"] == null)
                    {
                        Cache.Insert("gvLoginTrack + '" + advisorVo.advisorId + "'", dsBindLoginTrack);
                    }
                    else
                    {
                        Cache.Remove("gvLoginTrack + '" + advisorVo.advisorId + "'");
                        Cache.Insert("gvLoginTrack + '" + advisorVo.advisorId + "'", dsBindLoginTrack);
                    }
                    gvLoginTrack.DataSource = dsBindLoginTrack;
                    gvLoginTrack.DataBind();
                    gvLoginTrack.Visible = true;
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

                FunctionInfo.Add("Method", "AdviserLoginTrack.ascx:BindgvLoginTrack()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }  
        protected void btnGo_Click(object sender, EventArgs e)
        {
            Session["ButtonGo"] = "buttonClicked";
            ViewState["StartDate"] = null;
            ViewState["EndDate"] = null;
            ViewState["adviserId"] = null;
            CallAllGridBinding();
         if (userType == "customer")
            {
                gvLoginTrack.Visible = true;
                tblMessage.Visible = false;
                ErrorMessage.Visible = false;
                        
            }
            if (userType == "advisor")
            {
                gvLoginTrack.Visible = true;
                tblMessage.Visible = false;
                ErrorMessage.Visible = false;
            }
            if (userType == "associates")
            {
                gvLoginTrack.Visible = true;
                tblMessage.Visible = false;
                ErrorMessage.Visible = false;

            }
        }
        protected void gvLoginTrack_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            gvLoginTrack.Visible = true;
            DataSet dsLoginT = new DataSet();
            dsLoginT = (DataSet)Cache["gvLoginTrack + '" + advisorVo.advisorId + "'"]; 
            gvLoginTrack.DataSource =dsLoginT ;
            //if (gvLoginTrack.DataSource != null)
            //    gvLoginTrack.Visible = true;
            //else
            //    gvLoginTrack.Visible = false;
        
        }
     public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvLoginTrack.ExportSettings.OpenInNewWindow = true;
            gvLoginTrack.ExportSettings.IgnorePaging = true;
            gvLoginTrack.ExportSettings.HideStructureColumns = true;
            gvLoginTrack.ExportSettings.ExportOnlyData = true;
            gvLoginTrack.ExportSettings.FileName = "AdviserLoginTrack";
            gvLoginTrack.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvLoginTrack.MasterTableView.ExportToExcel();
        }
      }
}
    