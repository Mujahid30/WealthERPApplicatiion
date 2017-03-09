using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using BoOnlineOrderManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Telerik.Web.UI;
using VoOnlineOrderManagemnet;
using VoUser;
using WealthERP.Base;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class TradeBusinessDate : System.Web.UI.UserControl
    {
        OnlineOrderBackOfficeBo OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        UserVo userVo;
        AdvisorVo advisorVo;
        int holiday;
        int weekend;
        String strdt;

        string datesToBeUpdated;
        AdvisorVo adviserVo = new AdvisorVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                BindTradebusinessdate();
              BindCurrentPage();
                GetTradeBusinessDates();
                BindYearDropdown();
                //btngo_Click();
                //gvTradeBusinessDate.PageIndex = 8;
            }
            createcalanders.Visible = false;
        }

        private void GetTradeBusinessDates()
        {
            //try
            //{
            //    DataTable getTradeBusinessDateDt = new DataTable();
            //    getTradeBusinessDateDt = OnlineOrderBackOfficeBo.GetTradeBusinessDates().Tables[0];
            //    gvTradeBusinessDate.DataSource = getTradeBusinessDateDt;
            //    gvTradeBusinessDate.DataBind();
            //    if (Cache[userVo.UserId.ToString() + "TradeBusinessDates"] != null)
            //        Cache.Remove(userVo.UserId.ToString() + "TradeBusinessDates");
            //    Cache.Insert(userVo.UserId.ToString() + "TradeBusinessDates", getTradeBusinessDateDt);
            //    //gvTradeBusinessDate.Rebind();
            //}
            //catch (BaseApplicationException Ex)
            //{
            //    throw Ex;
            //}
            //catch (Exception Ex)
            //{
            //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
            //    NameValueCollection FunctionInfo = new NameValueCollection();
            //    FunctionInfo.Add("Method", "TradeBusinessDate.ascx.cs:CreateTradeBusinessDate()");
            //    object[] objects = new object[1];
            //    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
            //    exBase.AdditionalInformation = FunctionInfo;
            //    ExceptionManager.Publish(exBase);
            //    throw exBase;
            //}
        }
        protected void gvTradeBusinessDateDetails_ItemCommand(object source, GridCommandEventArgs e)
        {
            DataTable getTradeBusinessDateDt = new DataTable();
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                OnlineOrderBackOfficeBo OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
                TradeBusinessDateVo TradeBusinessDateVo = new TradeBusinessDateVo();
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                int TradeBusinessId = Convert.ToInt32(gvTradeBusinessDate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WTBD_Id"].ToString());
                RadDatePicker date1 = (RadDatePicker)e.Item.FindControl("RadDatePicker1");
                //RadDatePicker dt2 = (RadDatePicker)e.Item.FindControl("RadDatePicker2");
               // RadDatePicker txtdate = (RadDatePicker)e.Item.FindControl("txtdate");
                TextBox txtHoliday = (TextBox)e.Item.FindControl("txtHolidaysName");
                TradeBusinessDateVo.HolidayName = txtHoliday.Text.ToString();
                TradeBusinessDateVo.TradeBusinessDate = date1.SelectedDate.Value;

                if (date1.SelectedDate != null)
                {
                    bool bln = OnlineOrderBackOfficeBo.updateTradeBusinessDate(TradeBusinessId, TradeBusinessDateVo.HolidayName,TradeBusinessDateVo.TradeBusinessDate);
                    //GetTradeBusinessDates();
                    BindTradebusinessdate();
                }


            }
            if (e.CommandName == RadGrid.RebindGridCommandName)
            {
                //GetTradeBusinessDates();

            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {

                OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
                GridDataItem dataItem = (GridDataItem)e.Item;
                int TradeBusinessId = Convert.ToInt32(gvTradeBusinessDate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WTBD_DayName"].ToString());
                //RadDatePicker dt1 = (RadDatePicker)e.Item.FindControl("RadDatePicker1");
                //RadDatePicker dt2 = (RadDatePicker)e.Item.FindControl("RadDatePicker2");
                //RadDatePicker txtdate = (RadDatePicker)e.Item.FindControl("txtdate");
                //RadDatePicker txtExecutionDate = (RadDatePicker)e.Item.FindControl("txtExecutionDate");
                //if (dt1.SelectedDate != null)
                //{
                deleteTradeBusinessDate(TradeBusinessId);
                //}
                ////GetTradeBusinessDates();
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

                createTradeBusinessDate(dt1.SelectedDate.ToString(), txtExecutionDate.SelectedDate.ToString(), holiday, weekend);

            }
            //GridCommandItem commandItem = (GridCommandItem)gvTradeBusinessDate.MasterTableView.GetItems(GridItemType.CommandItem)[0];
            //commandItem.FindControl("AddNewRecordButton").Parent.Visible = false; 
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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Calendar Created!!');", true);
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
        //private void updateTradeBusinessDate(int TradeBusinessId, string TradeBusinessDate, string TradeBusinessExecutionDate)
        //{
        //    try
        //    {
        //        bool result;
        //        TradeBusinessDateVo TradeBusinessDateVo = new TradeBusinessDateVo();
        //        TradeBusinessDateVo.TradeBusinessId = TradeBusinessId;
        //        TradeBusinessDateVo.TradeBusinessDate = DateTime.Parse(TradeBusinessDate);
        //        TradeBusinessDateVo.TradeBusinessExecutionDate = DateTime.Parse(TradeBusinessExecutionDate);
        //        result = OnlineOrderBackOfficeBo.updateTradeBusinessDate(TradeBusinessDateVo);

        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "TradeBusinessDate.ascx.cs:updateTradeBusinessDate()");
        //        object[] objects = new object[2];
        //        objects[0] = TradeBusinessId;
        //        objects[1] = TradeBusinessDate;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        private void deleteTradeBusinessDate(int TradeBusinessId)
        {
            try
            {
                bool result;
                TradeBusinessDateVo TradeBusinessDateVo = new TradeBusinessDateVo();
                TradeBusinessDateVo.TradeBusinessId = TradeBusinessId;
                result = OnlineOrderBackOfficeBo.deleteTradeBusinessDate(TradeBusinessId);

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

        //protected void gvTradeBusinessDate_SelectionChanged(object sender, Telerik.Web.UI.Calendar.SelectedDatesEventArgs e)
        //{

        //    gvTradeBusinessDate.CurrentPageIndex = gvTradeBusinessDate.NewPageIndex;

        //// Rebind the data to refresh the DataGrid control. 
        //gvTradeBusinessDate.DataSource = CreateDataSource();
        //gvTradeBusinessDate.DataBind();

        //}


        protected void btnTradeBusinessDate_Click(object sender, ImageClickEventArgs e)
        {
            gvTradeBusinessDate.ExportSettings.OpenInNewWindow = true;
            gvTradeBusinessDate.ExportSettings.ExportOnlyData = true;
            gvTradeBusinessDate.ExportSettings.IgnorePaging = true;
            gvTradeBusinessDate.ExportSettings.FileName = "Trade Business Date";
            gvTradeBusinessDate.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvTradeBusinessDate.MasterTableView.ExportToExcel();
        }
        protected void Btncreatecalander_OnClick(object sender, EventArgs e)
        {
            createcalanders.Visible = true;
        }
        protected void Btncreatecal_OnClick(object sender, EventArgs e)
        {

            try
            {

                if (ddlyear.SelectedValue == "0")

                    return;
                int count = OnlineOrderBackOfficeBo.YearCheck(Convert.ToInt32(ddlyear.SelectedValue));
                if (count > 0)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Calendar Already Exist !!');", true);
                }
                else
                {
                    int selYear = int.Parse(ddlyear.SelectedValue);
                    OnlineOrderBackOfficeBo.CreateCalendar(selYear);
                    createcalanders.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Calendar Created!!');", true);
                    //gvTradeBusinessDate.MasterTableView.Rebind();
                    //gvTradeBusinessDate.Rebind();
                }
                //GetTradeBusinessDates();
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
            ddlyear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Create New Business Calendar", "0"));
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
            int i = 0;

            foreach (GridDataItem gvr in gvTradeBusinessDate.Items)
            {
                if (((CheckBox)gvr.FindControl("CheckBox")).Checked == true)
                {
                    i = i + 1;
                }
            }

            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please Select Date!');", true);
            }
            //    TradeBusinessDateVo TradeBusinessDateVo = new TradeBusinessDateVo();
            //    markholiday();
            //    string[] strDates = strdt.Split();
            //    strdt = strDates[0];

            //    OnlineOrderBackOfficeBo.MakeTradeToHoliday(Convert.ToDateTime(strdt), datesToBeUpdated);
            else
            {
                radwindowPopup.VisibleOnPageLoad = true;
            }
        }

        public void setExecutionDate(DateTime strDate)
        {


        }

        public void markholiday()
        {
            string[] strDates;
            string strDate;

            foreach (GridDataItem gvr in gvTradeBusinessDate.Items)
            {
                if (((CheckBox)gvr.FindControl("CheckBox")).Checked == true)
                {
                    strdt = gvTradeBusinessDate.MasterTableView.DataKeyValues[gvr.ItemIndex]["WTBD_DayName"].ToString();
                    strDates = strdt.Split();
                    strDate = strDates[0];

                    datesToBeUpdated = strDate + "~" + datesToBeUpdated;
                }

            }
        }
        //setExecutionDate(strDate);
        protected void gvTradeBusinessDate_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            DateTime dt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (DateTime.Parse(item["WTBD_DayName"].Text) == dt)
                {
                    //item["WTBD_DayName"].Focus();
                    item["WTBD_DayName"].BackColor = Color.Red;
                    item["WTBD_DayName"].Font.Bold = true;
                    item["WTBD_Day"].BackColor = Color.Red;
                    item["WTBD_Day"].Font.Bold = true;
                    item["WTBD_DayName1"].BackColor = Color.Red;
                    item["WTBD_DayName1"].Font.Bold = true;
                    item["WTBD_ExecutionDay"].BackColor = Color.Red;
                    item["WTBD_ExecutionDay"].Font.Bold = true;
                    //item["WTBD_IsHoliday"].BackColor = Color.Red;
                    //item["WTBD_IsHoliday"].Font.Bold = true;
                    item["WTBD_HolidayName"].BackColor = Color.Red;
                    item["WTBD_HolidayName"].Font.Bold = true;
                    item["WTBD_IsWeekend"].BackColor = Color.Red;
                    item["WTBD_IsWeekend"].Font.Bold = true;
                    item["check"].BackColor = Color.Red;
                    item["check"].Font.Bold = true;
                }
            }


            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                string holidaysname = gvTradeBusinessDate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WTBD_HolidayName"].ToString();
                DateTime date=Convert.ToDateTime(gvTradeBusinessDate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WTBD_DayName"].ToString());
                LinkButton update = dataItem["UPdate"].Controls[0] as LinkButton;
                //LinkButton update = (LinkButton)dataItem.FindControl("UPdate");
                if (holidaysname != null && holidaysname != string.Empty)
                {
                    if (date >= DateTime.Now)
                    {
                        update.Visible = true;
                        ////GetTradeBusinessDates();
                    }
                    else
                    {
                        update.Visible = false;
                    }
                }
                else
                {
                    update.Visible = false;

                }

            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            radwindowPopup.VisibleOnPageLoad = false;
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            TradeBusinessDateVo TradeBusinessDateVo = new TradeBusinessDateVo();
            markholiday();
            string[] strDates = strdt.Split();
            strdt = strDates[0];
            if (!string.IsNullOrEmpty(Texcmt.Text))
            {
                TradeBusinessDateVo.HolidayName = Texcmt.Text;
            }
            OnlineOrderBackOfficeBo.MakeTradeToHoliday(Convert.ToDateTime(strdt), datesToBeUpdated, TradeBusinessDateVo);
            radwindowPopup.VisibleOnPageLoad = false;
            Texcmt.Text = String.Empty;
           ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Holiday created!!');", true);
           BindTradebusinessdate();
           
            //GetTradeBusinessDates();
        }
        protected void BindTradebusinessdate()
        {
            try
            {
                DataSet dsGetAllTradeBussiness = new DataSet();
                DataTable dtGetAllTradeBussiness = new DataTable();
                dsGetAllTradeBussiness = OnlineOrderBackOfficeBo.GetAllTradeBussiness(int.Parse(Ddlyears.SelectedItem.Value), int.Parse(Ddlholiday.SelectedItem.Value));
                dtGetAllTradeBussiness = dsGetAllTradeBussiness.Tables[0];
                if (dtGetAllTradeBussiness.Rows.Count > 0)
                {
                    if (Cache[userVo.UserId.ToString() + "TradeBusinessDates"] != null)
                        Cache.Remove(userVo.UserId.ToString() + "TradeBusinessDates");
                    Cache.Insert(userVo.UserId.ToString() + "TradeBusinessDates", dtGetAllTradeBussiness);

                    //gvTradeBusinessDate.DataSource = dtGetAllTradeBussiness;
                    //gvTradeBusinessDate.DataBind();
                    
                    //if (Cache[userVo.UserId.ToString() + "TradeBusinessDates"] != null)
                    //{
                    //    Cache.Remove("Tradebussiness" + adviserVo.advisorId);
                    //    Cache.Insert("Tradebussiness" + adviserVo.advisorId, dtGetAllTradeBussiness);
                    //    Cache.Insert("Tradebussiness" + adviserVo.advisorId, dtGetAllTradeBussiness);
                    //}
                    //else
                    //{
                       
                    //}


                    //  sai
                    //if (Cache["Tradebussiness" + adviserVo.advisorId] == null)
                    //{
                    //    Cache.Insert("Tradebussiness" + adviserVo.advisorId, dtGetAllTradeBussiness);
                    //}
                    //else
                    //{
                    //    Cache.Remove("Tradebussiness" + adviserVo.advisorId);
                    //    Cache.Insert("Tradebussiness" + adviserVo.advisorId, dtGetAllTradeBussiness);
                    //}


                    //int i = 0;

                    //string s = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                    //foreach (DataRow dr in dtGetAllTradeBussiness.Rows)
                    //{
                    //    if (dtGetAllTradeBussiness.Rows[i][0].ToString().ToLower().Trim() == s.ToLower().Trim())
                    //    {
                    //        var page = (i / 20);  //eg row 50 means page = 10
                    //        gvTradeBusinessDate.CurrentPageIndex = page;

                    //        break;
                    //    }
                    //    else
                    //    {
                    //        i++;
                    //        gvTradeBusinessDate.CurrentPageIndex = 0;
                    //    }
                    //}

                    gvTradeBusinessDate.DataSource = dtGetAllTradeBussiness;
                    gvTradeBusinessDate.DataBind();
                    //BindTradebusinessdate();
                }
                else
                {
                    gvTradeBusinessDate.DataSource = dtGetAllTradeBussiness;
                    gvTradeBusinessDate.DataBind();
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
                FunctionInfo.Add("Method", "OnlineSchemeMIS.ascx.cs:SetParameter()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void SetParameter()
        {
            try
            {
                if (Ddlyears.SelectedIndex >= 0)
                {
                    hdnyear.Value = Ddlyears.SelectedValue;
                    ViewState["year"] = hdnyear.Value;
                }

                else
                {
                    hdnyear.Value = "0";
                }

                if (Ddlholiday.SelectedIndex == 0 || Ddlholiday.SelectedIndex == 1 ||Ddlholiday.SelectedIndex==2|| Ddlholiday.SelectedIndex == 3)
                {
                    hdnholiday.Value = Ddlholiday.SelectedValue;
                    ViewState["holiday"] = hdnholiday.Value;
                }
                else
                {
                    hdnholiday.Value = "0";
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
                FunctionInfo.Add("Method", "OnlineSchemeMIS.ascx.cs:SetParameter()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        protected void btngo_Click(object sender, EventArgs e)
        {
            
            SetParameter();
            BindTradebusinessdate();
            BindCurrentPage();
        }
        private void BindCurrentPage()
        {
            int pageSize = 20;// gvTradeBusinessDate.PageSize;
            var page = (DateTime.Now.DayOfYear/ pageSize);  //eg row 50 means page = 3
            gvTradeBusinessDate.CurrentPageIndex = page;

            GridSortExpression expression = new GridSortExpression();
            expression.FieldName = "WTBD_DayName";
            expression.SortOrder = GridSortOrder.Ascending;
            gvTradeBusinessDate.MasterTableView.SortExpressions.AddSortExpression(expression);

            gvTradeBusinessDate.MasterTableView.Rebind();

        }
    }
}
