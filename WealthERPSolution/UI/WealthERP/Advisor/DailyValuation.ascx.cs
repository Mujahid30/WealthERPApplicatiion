using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCustomerPortfolio;
using VoUser;
using BoAdvisorProfiling;
using VoCustomerPortfolio;
using System.Text;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;
using Telerik.Web.UI;

namespace WealthERP.Advisor
{
    public partial class DailyValuation : System.Web.UI.UserControl
    {
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        AdvisorVo advisorVo;
        AdvisorBo advisorBo = new AdvisorBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        UserVo userVo = null;
        CustomerVo customerVo = new CustomerVo();
        List<CustomerVo> customerList = null;
        static string assetGroup = "";
        //CustomerPortfolioVo customerPortfolioVo = null;
        //List<EQPortfolioVo> eqPortfolioList = null;
        //List<MFPortfolioVo> mfPortfolioList = null;
        //AdviserDailyLOGVo adviserDaliyLOGVo = null;
        DateTime dt = new DateTime();
        static DataSet dsAdviserValuationDate = new DataSet();
        static DateTime EQValuationDate = new DateTime();
        static DateTime MFValuationDate = new DateTime();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session["userVo"];

            if (!IsPostBack)
            {
                trMf.Visible = false;
                trEquity.Visible = false;
                trHeader.Visible = false;              
                trFPSync.Visible = false;
                btnFPSync.Visible = false;        
                trNote.Visible = false;
                trValuation.Visible = false;
                trSubmitButton.Visible = false;

            }


        }


        //protected void rbtnEquity_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rbtnEquity.Checked)
        //    {
        //        trMf.Visible = false;
        //        trEquity.Visible = true;

        //        trValuation.Visible = true;
        //        trSubmitButton.Visible = true;

        //        trNote.Visible = true;
        //        trHeader.Visible = true;
        //        ddTradeYear.Items.Clear();
        //        ddTradeMonth.Items.Clear();
        //        PopulateEQTradeYear();
        //        PopulateEQTradeMonth();
        //    }


        //    GetTradeDate();
        //}

        private void PopulateEQTradeYear()
        {
            ddTradeYear.Visible = true;

            DataSet ds = customerPortfolioBo.PopulateEQTradeYear();
            ddTradeYear.DataSource = ds;
            ddTradeYear.DataTextField = ds.Tables[0].Columns["TradeYear"].ToString();
            ddTradeYear.DataValueField = ds.Tables[0].Columns["TradeYear"].ToString();
            ddTradeYear.DataBind();
            ddTradeYear.SelectedValue = DateTime.Now.Year.ToString();
            //ddTradeYear.SelectedIndex = ds.Tables[0].Rows.Count - 1;
            //ddTradeYear.Items.Insert(0, "Select Trade Year");
        }


        //protected void rbtnMF_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rbtnMF.Checked)
        //    {
        //        trMf.Visible = true;

        //        trValuation.Visible = true;
        //        trSubmitButton.Visible = true;

        //        trNote.Visible = true;
        //        trHeader.Visible = true;
        //        trEquity.Visible = false;
        //        PopulateMFTradeDate();
        //        PopulateMFTradeMonth();
        //        assetGroup = "MF";
        //    }
        //    GetTradeDate();


        //}

        private void PopulateMFTradeDate()
        {
            trEquity.Visible = false;
            trMf.Visible = true;
            ddTradeMFYear.Visible = true;

            DataSet ds = customerPortfolioBo.PopulateEQTradeYear();
            ddTradeMFYear.DataSource = ds;
            ddTradeMFYear.DataTextField = ds.Tables[0].Columns["TradeYear"].ToString();
            ddTradeMFYear.DataValueField = ds.Tables[0].Columns["TradeYear"].ToString();
            ddTradeMFYear.DataBind();
            ddTradeMFYear.SelectedValue = DateTime.Now.Year.ToString();
            // ddTradeMFYear.SelectedIndex = ds.Tables[0].Rows.Count - 1;


        }



        private int CreateAdviserEODLog(string p, DateTime dt)
        {
            int LogId = 0;

            AdviserDailyLOGVo adviserDaliyLOGVo = new AdviserDailyLOGVo();
            try
            {
                adviserDaliyLOGVo.AdviserId = advisorVo.advisorId;
                adviserDaliyLOGVo.CreatedBy = userVo.UserId;
                adviserDaliyLOGVo.StartTime = DateTime.Now;
                adviserDaliyLOGVo.ProcessDate = dt;
                adviserDaliyLOGVo.AssetGroup = p;
                LogId = customerPortfolioBo.CreateAdviserEODLog(adviserDaliyLOGVo);
            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DailyValuation.ascx.cs:CreateAdviserEODLog()");
                object[] objects = new object[2];
                objects[0] = p;
                objects[1] = dt;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return LogId;
        }

        protected void UpdateAdviserEODLog(string group, int IsComplete, int LogId)
        {
            AdviserDailyLOGVo adviserDaliyLOGVo = new AdviserDailyLOGVo();
            try
            {
                adviserDaliyLOGVo.IsEquityCleanUpComplete = 0;
                adviserDaliyLOGVo.IsValuationComplete = IsComplete;
                adviserDaliyLOGVo.ModifiedBy = userVo.UserId;
                adviserDaliyLOGVo.EODLogId = LogId;
                adviserDaliyLOGVo.AssetGroup = group;
                adviserDaliyLOGVo.EndTime = DateTime.Now;
                customerPortfolioBo.UpdateAdviserEODLog(adviserDaliyLOGVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DailyValuation.ascx.cs:UpdateAdviserEODLog()");
                object[] objects = new object[3];
                objects[0] = group;
                objects[1] = IsComplete;
                objects[2] = LogId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }



        protected void ddTradeYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateEQTradeMonth();
        }

        protected void PopulateEQTradeMonth()
        {
            try
            {
                //if (ddTradeYear.SelectedIndex != 0)
                //{

                ddTradeMonth.Visible = true;

                DataSet ds = customerPortfolioBo.PopulateEQTradeMonth(int.Parse(ddTradeYear.SelectedItem.Value.ToString()));
                ddTradeMonth.DataSource = ds;
                ddTradeMonth.DataTextField = ds.Tables[0].Columns["TradeMonth"].ToString();
                ddTradeMonth.DataValueField = ds.Tables[0].Columns["WTD_Month"].ToString();
                ddTradeMonth.DataBind();
                // ddTradeMonth.Items.Insert(0, "Select Trade Month");
                ddTradeMonth.SelectedValue = DateTime.Now.Month.ToString();
                //ddTradeMonth.SelectedIndex = ds.Tables[0].Rows.Count - 1;
                //}
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select the trade Year..!');", true);
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DailyValuation.ascx.cs:PopulateEQTradeMonth()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddTradeMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTradeDate();
            //PopulateEQTradeDay();
        }

        //private void PopulateEQTradeDay()
        //{
        //    try
        //    {
        //        if (ddTradeYear.SelectedIndex != 0 && ddTradeMonth.SelectedIndex != 0)
        //        {

        //            ddTradeDay.Visible = true;
        //            DataSet ds = customerPortfolioBo.PopulateEQTradeDay(int.Parse(ddTradeYear.SelectedItem.Value.ToString()), int.Parse(ddTradeMonth.SelectedItem.Value.ToString()), advisorVo.advisorId, "EQ");
        //            ddTradeDay.DataSource = ds;
        //            ddTradeDay.DataTextField = ds.Tables[0].Columns["TradeDay"].ToString();
        //            ddTradeDay.DataValueField = ds.Tables[0].Columns["TradeDay"].ToString();
        //            ddTradeDay.DataBind();
        //            ddTradeDay.Items.Insert(0, "Select Trade Day");
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select the trade Month..!');", true);
        //        }
        //    }

        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "DailyValuation.ascx.cs:PopulateEQTradeDay()");
        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        protected void GetTradeDate()
        {
            DataTable dtValuation = new DataTable();
            DataRow dr;
            // int Count = 0;
            try
            {
                GetLatestValuationDate();
                if (ddlValuationTypes.SelectedValue == "EQ")
                {
                    assetGroup = "EQ";
                    hdnMsgValue.Value = "1";
                    dsAdviserValuationDate = customerPortfolioBo.GetAdviserValuationDate(advisorVo.advisorId, assetGroup, int.Parse(ddTradeMonth.SelectedValue.ToString()), int.Parse(ddTradeYear.SelectedItem.Value.ToString()));
                }
                if (ddlValuationTypes.SelectedValue == "MF")
                {
                    assetGroup = "MF";
                    hdnMsgValue.Value = "1";
                    dsAdviserValuationDate = customerPortfolioBo.GetAdviserValuationDate(advisorVo.advisorId, assetGroup, int.Parse(ddTradeMFMonth.SelectedValue.ToString()), int.Parse(ddTradeMFYear.SelectedItem.Value.ToString()));
                }


                //lblTotalRows.Text = hdnCount.Value = Count.ToString();

                dtValuation.Columns.Add("Valuation Date");
                dtValuation.Columns.Add("Valuation Status");
                for (int i = 0; i < dsAdviserValuationDate.Tables[0].Rows.Count; i++)
                {
                    dr = dtValuation.NewRow();
                    dr[0] = DateTime.Parse(dsAdviserValuationDate.Tables[0].Rows[i][0].ToString()).ToShortDateString();
                    dr[1] = dsAdviserValuationDate.Tables[0].Rows[i]["STAT"];

                    dtValuation.Rows.Add(dr);


                }
                gvValuationDate.DataSource = dtValuation;
                gvValuationDate.DataBind();
                gvValuationDate.Visible = true;

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DailyValuation.ascx.cs:GetTradeDate()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        private void GetLatestValuationDate()
        {

            PortfolioBo portfolioBo = null;
            try
            {
                portfolioBo = new PortfolioBo();
                if (portfolioBo.GetLatestValuationDate(advisorVo.advisorId, "EQ") != null)
                {
                    EQValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(advisorVo.advisorId, "EQ").ToString());
                }
                if (portfolioBo.GetLatestValuationDate(advisorVo.advisorId, "MF") != null)
                {
                    MFValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(advisorVo.advisorId, "MF").ToString());
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
                FunctionInfo.Add("Method", "PortfolioDashboard.ascx.cs:GetLatestValuationDate()");
                object[] objects = new object[2];
                objects[0] = EQValuationDate;
                objects[1] = MFValuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public void UpdateLOG(string value, string assetgroup)
        {
            int cnt = 0;
            int LogId = 0;
            int notNullcnt = 0;
            List<int> customerList = new List<int>();
            List<CustomerPortfolioVo> customerPortfolioList = new List<CustomerPortfolioVo>();
            List<EQPortfolioVo> eqPortfolioList = null;
            List<MFPortfolioVo> mfPortfolioList = null;
            string status = string.Empty;
            try
            {

                if (value == "1" && assetgroup == "EQ")
                {


                    if (ddlValuationTypes.SelectedValue == "EQ")
                    {

                        if (ddTradeMonth.SelectedValue != "" && ddTradeYear.SelectedValue != "")
                        {
                            for (int i = 0; i < dsAdviserValuationDate.Tables[0].Rows.Count; i++)
                            {
                                GridViewRow gvr = gvValuationDate.Rows[i];
                                if (gvValuationDate.Rows[i].RowType == DataControlRowType.DataRow)
                                {
                                    CheckBox checkBox = (CheckBox)gvr.FindControl("chkBx");
                                    status = gvr.Cells[2].Text.ToString();
                                    if (checkBox.Checked)
                                    {
                                        dt = DateTime.Parse(dsAdviserValuationDate.Tables[0].Rows[i][0].ToString());
                                        if (status == "Pending. Changes Found" || status == "Completed")
                                        {
                                            customerPortfolioBo.DeleteAdviserEODLog(advisorVo.advisorId, "EQ", dt, 0);
                                        }



                                        if (DateTime.Compare(dt, DateTime.Today) <= 0)
                                        {

                                            customerList = customerPortfolioBo.GetAdviserCustomerList_EQ(advisorVo.advisorId);

                                            if (customerList != null)
                                            {
                                                notNullcnt = notNullcnt + 1;
                                                LogId = CreateAdviserEODLog("EQ", dt);
                                                if (LogId != 0)
                                                {
                                                    cnt = 0;
                                                    for (int j = 0; j < customerList.Count; j++)
                                                    {
                                                        customerPortfolioList = portfolioBo.GetCustomerPortfolios(customerList[j]);
                                                        customerPortfolioBo.DeleteEquityNetPosition(customerList[j], dt);
                                                        if (customerPortfolioList != null)
                                                        {
                                                            for (int k = 0; k < customerPortfolioList.Count; k++)
                                                            {
                                                                eqPortfolioList = customerPortfolioBo.GetCustomerEquityPortfolio(customerList[j], customerPortfolioList[k].PortfolioId, dt, string.Empty, string.Empty);
                                                                if (eqPortfolioList != null)
                                                                {

                                                                    customerPortfolioBo.AddEquityNetPosition(eqPortfolioList, userVo.UserId);

                                                                }
                                                            }
                                                        }

                                                        cnt = cnt + 1;

                                                    }
                                                }

                                                if (cnt == customerList.Count)
                                                {
                                                    UpdateAdviserEODLog("EQ", 1, LogId);
                                                    if (Cache[advisorVo.advisorId.ToString()] != null && dt == DateTime.Today)
                                                    {
                                                        Cache.Remove(advisorVo.advisorId.ToString());
                                                    }
                                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Equity Valuation done...!');", true);

                                                }
                                                else
                                                {
                                                    UpdateAdviserEODLog("EQ", 0, LogId);
                                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Equity Valuation not done...!');", true);
                                                }

                                                //  
                                            }
                                            else
                                                break;
                                        }

                                    }

                                }
                                else
                                {
                                    continue;
                                }
                            }
                            if (notNullcnt == 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('No Customers With Equity Transactions....');", true);

                            }
                        }

                    }



                }
                else if (value == "1" && assetgroup == "MF")
                {

                    if (ddTradeMFMonth.SelectedValue != "" && ddTradeMFYear.SelectedValue != "")
                    {
                        for (int i = 0; i < dsAdviserValuationDate.Tables[0].Rows.Count; i++)
                        {
                            GridViewRow gvr = gvValuationDate.Rows[i];
                            if (gvValuationDate.Rows[i].RowType == DataControlRowType.DataRow)
                            {
                                CheckBox checkBox = (CheckBox)gvr.FindControl("chkBx");
                                status = gvr.Cells[2].Text.ToString();
                                if (checkBox.Checked)
                                {
                                    dt = DateTime.Parse(dsAdviserValuationDate.Tables[0].Rows[i][0].ToString());
                                    if (status == "Pending. Changes Found" || status == "Completed")
                                    {
                                        customerPortfolioBo.DeleteAdviserEODLog(advisorVo.advisorId, "MF", dt, 0);
                                    }

                                    if (DateTime.Compare(dt, DateTime.Today) <= 0)
                                    {
                                        customerList = customerPortfolioBo.GetAdviserCustomerList_MF(advisorVo.advisorId);
                                        if (customerList != null)
                                        {

                                            notNullcnt = notNullcnt + 1;
                                            LogId = CreateAdviserEODLog("MF", dt);
                                            if (LogId != 0)
                                            {
                                                cnt = 0;
                                                for (int j = 0; j < customerList.Count; j++)
                                                {
                                                    customerPortfolioList = portfolioBo.GetCustomerPortfolios(customerList[j]);
                                                    customerPortfolioBo.DeleteMutualFundNetPosition(customerList[j], dt);
                                                    if (customerPortfolioList != null)
                                                    {
                                                        for (int k = 0; k < customerPortfolioList.Count; k++)
                                                        {
                                                            mfPortfolioList = customerPortfolioBo.GetCustomerMFPortfolio(customerList[j], customerPortfolioList[k].PortfolioId, dt, "", "", "");


                                                            if (mfPortfolioList != null)
                                                            {

                                                                customerPortfolioBo.AddMutualFundNetPosition(mfPortfolioList, userVo.UserId);

                                                            }
                                                        }
                                                    }


                                                    cnt = cnt + 1;

                                                }
                                            }
                                            if (cnt == customerList.Count)
                                            {
                                                UpdateAdviserEODLog("MF", 1, LogId);
                                                if (Cache[advisorVo.advisorId.ToString()] != null && dt == DateTime.Today)
                                                {
                                                    Cache.Remove(advisorVo.advisorId.ToString());
                                                }
                                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('MF Valuation done...!');", true);
                                            }
                                            else
                                            {
                                                UpdateAdviserEODLog("MF", 0, LogId);
                                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('MF Valuation not done...!');", true);
                                            }

                                            //  GetTradeDate();

                                        }

                                    }
                                }
                                else
                                {
                                    continue;
                                }

                            }
                        }
                        if (notNullcnt == 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('No Customers With MF Transactions....');", true);

                        }



                    }

                }
                GetTradeDate();
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DailyValuation.ascx.cs:UpdateLOG()");
                object[] objects = new object[2];
                objects[0] = value;
                objects[1] = assetgroup;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        //protected void ddTradeDay_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    lblTradeDate.Text = ddTradeDay.SelectedItem.Value.ToString();
        //    // GetTradeDate();
        //}

        protected void ddTradeMFYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateMFTradeMonth();
        }

        private void PopulateMFTradeMonth()
        {
            try
            {
                //if (ddTradeMFYear.SelectedIndex != 0)
                //{
                ddTradeMFMonth.Visible = true;

                DataSet ds = customerPortfolioBo.PopulateEQTradeMonth(int.Parse(ddTradeMFYear.SelectedItem.Value.ToString()));
                ddTradeMFMonth.DataSource = ds;
                ddTradeMFMonth.DataTextField = ds.Tables[0].Columns["TradeMonth"].ToString();
                ddTradeMFMonth.DataValueField = ds.Tables[0].Columns["WTD_Month"].ToString();
                ddTradeMFMonth.DataBind();
                ddTradeMFMonth.SelectedValue = DateTime.Now.Month.ToString();
                //ddTradeMFMonth.SelectedIndex = ds.Tables[0].Rows.Count - 1;
                //}
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select the trade Year..!');", true);
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DailyValuation.ascx.cs:PopulateMFTradeMonth()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddTradeMFMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTradeDate();
        }

        //private void PopulateMFTradeDay()
        //{
        //    try
        //    {
        //        if (ddTradeMFYear.SelectedIndex != 0 && ddTradeMFMonth.SelectedIndex != 0)
        //        {
        //            ddTradeMFDay.Visible = true;
        //            DataSet ds = customerPortfolioBo.PopulateEQTradeDay(int.Parse(ddTradeMFYear.SelectedItem.Value.ToString()), int.Parse(ddTradeMFMonth.SelectedItem.Value.ToString()), advisorVo.advisorId, "MF");
        //            ddTradeMFDay.DataSource = ds;
        //            ddTradeMFDay.DataTextField = ds.Tables[0].Columns["TradeDay"].ToString();
        //            ddTradeMFDay.DataValueField = ds.Tables[0].Columns["TradeDay"].ToString();
        //            ddTradeMFDay.DataBind();
        //            ddTradeMFDay.Items.Insert(0, "Select Trade Day");
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select the trade Month..!');", true);
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "DailyValuation.ascx.cs:PopulateMFTradeDay()");
        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //}

        protected void ddTradeMFDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblMFTradeDate.Text = ddTradeMFDay.SelectedItem.Value.ToString();
            //  GetTradeDate();
        }

        protected void hiddenUpdateNetPosition_Click(object sender, EventArgs e)
        {
            string val = Convert.ToString(hdnMsgValue.Value);
            UpdateLOG(val, assetGroup);

        }


        protected void gvValuationDate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkBox = e.Row.FindControl("chkBx") as CheckBox;
                if (e.Row.Cells[2].Text == "Pending" || e.Row.Cells[2].Text == "Pending. Changes Found")
                    chkBox.Checked = true;
                else
                {
                    chkBox.Checked = false;
                }
                if (assetGroup == "MF")
                {
                    if (e.Row.Cells[1].Text == MFValuationDate.ToShortDateString())
                    {
                        // e.Row.CssClass = "HighLightRowStyle";
                    }
                }
                else
                {
                    if (e.Row.Cells[1].Text == EQValuationDate.ToShortDateString())
                    {
                        // e.Row.CssClass = "HighLightRowStyle";
                    }
                }
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {



            string val = Convert.ToString(hdnMsgValue.Value);
            UpdateLOG(val, assetGroup);
        }

        protected void ddlValuationTypes_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlValuationTypes.SelectedValue == "Select")
            {
                trMf.Visible = false;
                trEquity.Visible = false;
                trHeader.Visible = false;
                trFPSync.Visible = false;
                btnFPSync.Visible = false;
                trNote.Visible = false;
                trValuation.Visible = false;
                trSubmitButton.Visible = false;
            }
            else if (ddlValuationTypes.SelectedValue == "EQ")
            {
                trMf.Visible = false;
                trEquity.Visible = true;
                btnFPSync.Visible = false;
                trValuation.Visible = true;
                trSubmitButton.Visible = true;
                trFPSync.Visible = false;
                trNote.Visible = true;
                trHeader.Visible = true;
                ddTradeYear.Items.Clear();
                ddTradeMonth.Items.Clear();
                PopulateEQTradeYear();
                PopulateEQTradeMonth();
                GetTradeDate();
            }
            else if (ddlValuationTypes.SelectedValue == "MF")
            {
                trMf.Visible = true;
                trFPSync.Visible = false;
                btnFPSync.Visible = false;
                trValuation.Visible = true;
                trSubmitButton.Visible = true;

                trNote.Visible = true;
                trHeader.Visible = true;
                trEquity.Visible = false;
                PopulateMFTradeDate();
                PopulateMFTradeMonth();
                assetGroup = "MF";
                GetTradeDate();
               
            }
            else if (ddlValuationTypes.SelectedValue == "FP")
            {
                trMf.Visible = false;
                trValuation.Visible = false;
                trSubmitButton.Visible = false;
                trFPSync.Visible = true;
                btnFPSync.Visible = true;
                trNote.Visible = true;
                trHeader.Visible = false;
                trEquity.Visible = false;
                BindFPCustomerData();
                
            }
        }

        protected void BindFPCustomerData()
        {
            List<CustomerVo> customerList = new List<CustomerVo>();
            customerList = advisorBo.GetAdviserAllCustomerList(advisorVo.advisorId);


            if (customerList != null)
            {
                DataTable dtRMCustomer = new DataTable();
                dtRMCustomer.Columns.Add("CustomerId");
                dtRMCustomer.Columns.Add("UserId");
                dtRMCustomer.Columns.Add("RMId");
                dtRMCustomer.Columns.Add("Parent");
                dtRMCustomer.Columns.Add("Cust_Comp_Name");
                dtRMCustomer.Columns.Add("PAN Number");
                dtRMCustomer.Columns.Add("Mobile Number");
                dtRMCustomer.Columns.Add("Phone Number");
                dtRMCustomer.Columns.Add("Email");
                dtRMCustomer.Columns.Add("Address");
                dtRMCustomer.Columns.Add("Area");
                dtRMCustomer.Columns.Add("City");
                dtRMCustomer.Columns.Add("Pincode");
                dtRMCustomer.Columns.Add("Assigned RM");
                dtRMCustomer.Columns.Add("IsActive");
                dtRMCustomer.Columns.Add("IsProspect");
                dtRMCustomer.Columns.Add("IsFPClient");

                DataRow drRMCustomer;

                for (int i = 0; i < customerList.Count; i++)
                {
                    drRMCustomer = dtRMCustomer.NewRow();
                    customerVo = new CustomerVo();
                    customerVo = customerList[i];
                    drRMCustomer[0] = customerVo.CustomerId.ToString();
                    drRMCustomer[1] = customerVo.UserId.ToString();
                    drRMCustomer[2] = customerVo.RmId.ToString();

                    if (customerVo.ParentCustomer != null)
                    {
                        drRMCustomer[3] = customerVo.ParentCustomer.ToString();
                    }
                    drRMCustomer[4] = customerVo.FirstName.ToString() + " " + customerVo.MiddleName.ToString() + " " + customerVo.LastName.ToString();

                    if (customerVo.PANNum != null)
                        drRMCustomer[5] = customerVo.PANNum.ToString();
                    else
                        drRMCustomer[5] = "";
                    drRMCustomer[6] = customerVo.Mobile1.ToString();
                    drRMCustomer[7] = customerVo.ResISDCode.ToString() + "-" + customerVo.ResSTDCode.ToString() + "-" + customerVo.ResPhoneNum.ToString();
                    drRMCustomer[8] = customerVo.Email.ToString();
                    if (customerVo.Adr1City == null)
                        customerVo.Adr1City = "";
                    if (customerVo.Adr1Line1 == null)
                        customerVo.Adr1Line1 = "";
                    if (customerVo.Adr1Line2 == null)
                        customerVo.Adr1Line2 = "";
                    if (customerVo.Adr1Line3 == null)
                        customerVo.Adr1Line3 = "";
                    if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() == "")
                    {
                        drRMCustomer[9] = "-";
                    }
                    else if (customerVo.Adr1Line1.ToString() == "" && customerVo.Adr1Line2.ToString() != "")
                    {
                        drRMCustomer[9] = customerVo.Adr1Line2.ToString();
                    }
                    else if (customerVo.Adr1Line1.ToString() != "" && customerVo.Adr1Line2.ToString() == "")
                    {
                        drRMCustomer[9] = customerVo.Adr1Line1.ToString();
                    }
                    else
                        drRMCustomer[9] = customerVo.Adr1Line1.ToString() + "," + customerVo.Adr1Line2.ToString();
                    drRMCustomer[10] = customerVo.Adr1Line3.ToString();
                    drRMCustomer[11] = customerVo.Adr1City.ToString();
                    drRMCustomer[12] = customerVo.Adr1PinCode.ToString();
                    if (customerVo.AssignedRM != null)
                        drRMCustomer[13] = customerVo.AssignedRM.ToString();
                    else
                        drRMCustomer[13] = "-";
                    if (customerVo.IsActive == 1)
                    {
                        drRMCustomer[14] = "Active";
                    }
                    else
                    {
                        drRMCustomer[14] = "In Active";

                    }
                    if (customerVo.IsProspect == 1)
                    {
                        drRMCustomer[15] = "Yes";
                    }
                    else
                    {
                        drRMCustomer[15] = "No";
                    }
                    if (customerVo.IsFPClient == 1)
                    {
                        drRMCustomer[16] = "Yes";
                    }
                    else
                    {
                        drRMCustomer[16] = "No";
                    }
                    dtRMCustomer.Rows.Add(drRMCustomer);
                }
                gvCustomerlist.DataSource = dtRMCustomer;
                gvCustomerlist.DataBind();

                if (Cache["FP"] == null)
                {
                    Cache.Insert("FP", dtRMCustomer);
                }
                else
                {
                    Cache.Remove("FP");
                    Cache.Insert("FP", dtRMCustomer);
                }
            }
        }


        protected void gvCustomerlist_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //if (Cache["FP"] == null)
            //{
            //    BindFPCustomerData();
            //}
            DataTable dtCustomerList = new DataTable();
            dtCustomerList = (DataTable)Cache["FP"];
            gvCustomerlist.DataSource = dtCustomerList;
            //gvCustomerlist.DataBind();
        }    

        protected void FPSync_OnClick(object sender, EventArgs e)
        {
            string customerIds = "";
            bool result = true;
            foreach (GridDataItem item in gvCustomerlist.MasterTableView.Items)
            {
                CheckBox chkbx = (CheckBox)item["View"].FindControl("CheckBox1");
                if (chkbx.Checked == true)
                {
                    customerIds = customerIds + Convert.ToString(item.GetDataKeyValue("CustomerId").ToString()) + ',';                    
                }
            }
               result = advisorBo.UpdateAdviserFPBatch(customerIds,advisorVo.advisorId);
               ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Financial Valuation Done...');", true);

        }
    }
}
