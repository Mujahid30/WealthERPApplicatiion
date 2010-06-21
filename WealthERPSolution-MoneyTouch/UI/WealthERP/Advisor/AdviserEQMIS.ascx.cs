using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using VoUser;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using WealthERP.Base;
using BoCommon;
using System.Globalization;
using System.Collections.Specialized;
using BoCustomerPortfolio;

namespace WealthERP.Advisor
{
    public partial class AdviserEQMIS : System.Web.UI.UserControl
    {
        AdvisorMISBo adviserMIS = new AdvisorMISBo();
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        string path;
        string userType;
        int advisorId;
        CustomerTransactionBo customertransactionbo = new CustomerTransactionBo();
        DataSet dsGetLastTradeDate;
        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                advisorVo = (AdvisorVo)Session["advisorVo"];
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                userType = Session["UserType"].ToString().ToLower();
                if (!IsPostBack)
                {
                    trRange.Visible = true;
                    trPeriod.Visible = false;
                }
                dsGetLastTradeDate = customertransactionbo.GetLastTradeDate();
                DateTime dtLastTradeDate;
                
                if (dsGetLastTradeDate.Tables[0].Rows.Count != 0)
                {
                    dtLastTradeDate=(DateTime)dsGetLastTradeDate.Tables[0].Rows[0]["WTD_Date"];

                    txtFromDate.Text = dtLastTradeDate.ToShortDateString();
                    txtToDate.Text = dtLastTradeDate.ToShortDateString();

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

                FunctionInfo.Add("Method", "AddBranch.ascx:PageLoad()");

                object[] objects = new object[2];
                objects[0] = advisorVo;
                objects[1] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void BindGrid(DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsEQMIS = new DataSet();
            DataTable dtAdviserEQMIS = new DataTable();
            
            int ID = 0;
           
            if (userType == "adviser")
                ID = advisorVo.advisorId;
            else if (userType == "rm")
            {
                rmVo = (RMVo)Session[SessionContents.RmVo];
                ID = rmVo.RMId;
            }
            else if (userType == "bm")
            {
                ID = 0;
            }

            try
            {
                dsEQMIS = adviserMIS.GetEQMIS(userType, ID, dtFrom, dtTo);

                if (dsEQMIS.Tables[0].Rows.Count > 0)
                {
                    trMessage.Visible = false;
                    lblMessage.Visible = false;

                    dtAdviserEQMIS.Columns.Add("DeliveryBuy");
                    dtAdviserEQMIS.Columns.Add("DeliverySell");
                    dtAdviserEQMIS.Columns.Add("SpeculativeSell");
                    dtAdviserEQMIS.Columns.Add("SpeculativeBuy");

                    DataRow drAdvEQMIS;

                    for (int i = 0; i < dsEQMIS.Tables[0].Rows.Count; i++)
                    {
                        drAdvEQMIS = dtAdviserEQMIS.NewRow();

                        drAdvEQMIS[0] = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["DeliveryBuy"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drAdvEQMIS[1] = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["DeliverySell"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drAdvEQMIS[2] = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["SpeculativeSell"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                        drAdvEQMIS[3] = String.Format("{0:n2}", decimal.Parse(dsEQMIS.Tables[0].Rows[i]["SpeculativeBuy"].ToString()).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                        dtAdviserEQMIS.Rows.Add(drAdvEQMIS);
                    }

                    gvEQMIS.DataSource = dtAdviserEQMIS;
                    gvEQMIS.DataBind();
                }
                else
                {
                    trMessage.Visible = true;
                    lblMessage.Visible = true;
                    gvEQMIS.Visible = false;
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
                FunctionInfo.Add("Method", "AdviserEQMIS.ascx.cs:BindGrid()");
                object[] objects = new object[2];
                objects[0] = dtFrom;
                objects[1] = dtTo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void rbtnDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPickDate.Checked == true)
            {
                trRange.Visible = true;
                trPeriod.Visible = false;
            }
            else if (rbtnPickPeriod.Checked == true)
            {
                trRange.Visible = false;
                trPeriod.Visible = true;
                BindPeriodDropDown();
            }
        }

        private void BindPeriodDropDown()
        {
            DataTable dtPeriod;
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            dtPeriod = XMLBo.GetDatePeriod(path);

            ddlPeriod.DataSource = dtPeriod;
            ddlPeriod.DataTextField = "PeriodType";
            ddlPeriod.DataValueField = "PeriodCode";
            ddlPeriod.DataBind();
            ddlPeriod.Items.Insert(0, new ListItem("Select a Period", "Select a Period"));
        }

        protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();
            DateBo dtBo = new DateBo();

            if (ddlPeriod.SelectedIndex != 0)
            {
                dtBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue, out dtFrom, out dtTo);
                this.BindGrid(dtFrom, dtTo);
            }
            else
            {

            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            CultureInfo ci = new CultureInfo("en-GB");

            DateTime convertedFromDate = new DateTime();
            DateTime convertedToDate = new DateTime();

            convertedFromDate = Convert.ToDateTime(txtFromDate.Text.Trim(), ci);
            convertedToDate = Convert.ToDateTime(txtToDate.Text.Trim(), ci);

            this.BindGrid(convertedFromDate, convertedToDate);
        }

    }
}