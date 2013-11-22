using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Text;

using VoUser;
using WealthERP.Base;
using Telerik.Web.UI;
using VoOnlineOrderManagemnet;

using BoCommon;
using BoOnlineOrderManagement;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class TradeBusinessDate : System.Web.UI.UserControl
    {
        OnlineOrderBackOfficeBo OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        UserVo userVo;
        AdvisorVo advisorVo;
        int holiday;
        int weekend;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];

            if (!IsPostBack)
            {
                GetTradeBusinessDates();
                BindYearDropdown();
            }
        }

        private void GetTradeBusinessDates()
        {
            try
            {
                DataTable getTradeBusinessDateDt = new DataTable();
                getTradeBusinessDateDt = OnlineOrderBackOfficeBo.GetTradeBusinessDates().Tables[0];
                gvTradeBusinessDate.DataSource = getTradeBusinessDateDt;
                gvTradeBusinessDate.DataBind();
                if (Cache[userVo.UserId.ToString() + "TradeBusinessDates"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "TradeBusinessDates");
                Cache.Insert(userVo.UserId.ToString() + "TradeBusinessDates", getTradeBusinessDateDt);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "TradeBusinessDate.ascx.cs:CreateTradeBusinessDate()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void gvTradeBusinessDateDetails_ItemCommand(object source, GridCommandEventArgs e)
        {
            DataTable getTradeBusinessDateDt = new DataTable();
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                OnlineOrderBackOfficeBo OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                int TradeBusinessId = Convert.ToInt32(gvTradeBusinessDate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WTBD_id"].ToString());
                RadDatePicker dt1 = (RadDatePicker)e.Item.FindControl("RadDatePicker1");
                RadDatePicker txtExecutionDate = (RadDatePicker)e.Item.FindControl("txtExecutionDate");
                if (dt1.SelectedDate != null)
                {
                    updateTradeBusinessDate(TradeBusinessId, dt1.SelectedDate.ToString(), txtExecutionDate.SelectedDate.ToString());
                }
                GetTradeBusinessDates(); 
            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
               
                OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
                GridDataItem dataItem = (GridDataItem)e.Item;
                int TradeBusinessId = Convert.ToInt32(gvTradeBusinessDate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WTBD_id"].ToString());
                deleteTradeBusinessDate(TradeBusinessId);
            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
               
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                RadDatePicker dt1 = (RadDatePicker)e.Item.FindControl("RadDatePicker1");
                RadDatePicker txtExecutionDate = (RadDatePicker)e.Item.FindControl("txtExecutionDate");
                RadioButton rbtnIsHoliday = (RadioButton)e.Item.FindControl("rbtnIsHoliday");
                RadioButton rbtnIsWeekened = (RadioButton)e.Item.FindControl("rbtnIsWeekened");

                if (rbtnIsHoliday.Checked == true)
                  holiday = 1;
                else
                  holiday = 0;
                
                if (rbtnIsWeekened.Checked == true)
                   weekend = 1;
                else
                  weekend = 0;
                
                createTradeBusinessDate(dt1.SelectedDate.ToString(), txtExecutionDate.SelectedDate.ToString(),holiday,weekend );    
                
            }
        }

        private void createTradeBusinessDate(string TradeBusinessDate, string TradeBusinessExecutionDate, int holiday, int weekend)
        {
            try
            {
                bool result;
                TradeBusinessDateVo TradeBusinessDateVo = new TradeBusinessDateVo();
               TradeBusinessDateVo.TradeBusinessDate = DateTime.Parse(TradeBusinessDate);
               TradeBusinessDateVo.TradeBusinessExecutionDate = DateTime.Parse(TradeBusinessExecutionDate);
               TradeBusinessDateVo.IsTradeBusinessDateHoliday = holiday;
                TradeBusinessDateVo.IsTradeBusinessDateWeekend = weekend;
                result = OnlineOrderBackOfficeBo.CreateTradeBusinessDate(TradeBusinessDateVo);
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "TradeBusinessDate.ascx.cs:CreateTradeBusinessDate()");
                object[] objects = new object[4];
                objects[0] = TradeBusinessDate;
                objects[1] = TradeBusinessExecutionDate;
                objects[2] = holiday;
                objects[3] = weekend;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void updateTradeBusinessDate(int TradeBusinessId, string TradeBusinessDate, string TradeBusinessExecutionDate)
        {
            try
            {
                bool result;
                TradeBusinessDateVo TradeBusinessDateVo = new TradeBusinessDateVo();
                TradeBusinessDateVo.TradeBusinessId = TradeBusinessId;
                TradeBusinessDateVo.TradeBusinessDate = DateTime.Parse(TradeBusinessDate);
                TradeBusinessDateVo.TradeBusinessExecutionDate = DateTime.Parse(TradeBusinessExecutionDate);
                result = OnlineOrderBackOfficeBo.updateTradeBusinessDate(TradeBusinessDateVo);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "TradeBusinessDate.ascx.cs:updateTradeBusinessDate()");
                object[] objects = new object[2];
                objects[0] = TradeBusinessId;
                objects[1] = TradeBusinessDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void deleteTradeBusinessDate(int TradeBusinessId)
        {
            try
            {
                bool result;
                TradeBusinessDateVo TradeBusinessDateVo = new TradeBusinessDateVo();
                TradeBusinessDateVo.TradeBusinessId = TradeBusinessId;
                result = OnlineOrderBackOfficeBo.deleteTradeBusinessDate(TradeBusinessDateVo);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "TradeBusinessDate.ascx.cs:deleteTradeBusinessDate()");
                object[] objects = new object[1];
                objects[0] = TradeBusinessId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void gvTradeBusinessDate_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dsTradeBusinessDate = new DataTable();
            if (Cache[userVo.UserId.ToString() + "TradeBusinessDates"] != null)
            {
                dsTradeBusinessDate = (DataTable)Cache[userVo.UserId.ToString() + "TradeBusinessDates"];
                gvTradeBusinessDate.DataSource = dsTradeBusinessDate;
            }
        }

        protected void btnTradeBusinessDate_Click(object sender, ImageClickEventArgs e)
        {
            gvTradeBusinessDate.ExportSettings.OpenInNewWindow = true;
            gvTradeBusinessDate.ExportSettings.ExportOnlyData = true;
            gvTradeBusinessDate.ExportSettings.IgnorePaging = true;
            gvTradeBusinessDate.ExportSettings.FileName = "TradeBusinessDate";
            gvTradeBusinessDate.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvTradeBusinessDate.MasterTableView.ExportToExcel();
        }

        protected void Btncreatecal_OnClick(object sender, EventArgs e)
        {

            try
            {
                if (ddlyear.SelectedValue == "0") 
                    return;

                int selYear = int.Parse(ddlyear.SelectedValue);
                OnlineOrderBackOfficeBo.CreateCalendar(selYear);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "TradeBusinessDate.ascx.cs:ddlType_OnSelectedIndexChanged()");
                object[] objects = new object[2];
                objects[0] = sender;
                objects[1] = e;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        void BindYearDropdown()
        {
            ddlyear.Items.Clear();
            ddlyear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
            //ddlyear.Items.Add("--SELECT--");
            int currYear = DateTime.Now.Year;
            for (int i = currYear; i < currYear + 5; i++)
                ddlyear.Items.Add(i.ToString());
        }

        protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void Btnmarkholiday_Onclick(object sender, EventArgs e)
        {

        }
    }
}
