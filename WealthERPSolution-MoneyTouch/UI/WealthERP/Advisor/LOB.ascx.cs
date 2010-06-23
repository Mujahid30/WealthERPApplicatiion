using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using BoCommon;
using System.Web.UI.WebControls;
using System.Configuration;
using BoAdvisorProfiling;
using VoAdvisorProfiling;
using VoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using BoProductMaster;
using System.Data.SqlClient;
using System.Data.Sql;


using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
namespace WealthERP.Advisor
{
    public partial class LOB : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        ProductMFBo productMfBo = new ProductMFBo();
        UserVo userVo = new UserVo();
        string mf;
        string equityBrokerCash;
        string equityBrokerDerivative;
        string equitySubCash;
        string equitySubDerivative;
        string equityRemissaryCash;
        string equityRemissaryDerivative;
        string LOBId;
        string strPmsBrokerCash;
        string strPmsBrokerDerivative;
        string strPmsSubBrokerCash;
        string strPmsSubBrokerDerivative;
        string strPmsRemissaryCash;
        string strPmsRemissaryDerivative;
        string strCommBrokerCash;
        string strCommBrokerDerivative;
        string strCommSubBrokerCash;
        string strCommSubBrokerDerivative;
        string strCommRemissaryCash;
        string strCommRemissaryDerivative;
        string strInsuranceAgent;
        string strPostalSavingsAgent;
        string strRealEstateAgent;
        string strLiabilitiesAgent;
        string strFixedIncomeAgent;
        DataSet dsProductAmc;
        DataSet dsHolidays = new DataSet();
        SqlConnection conn;
        String connectionstring;


        //protected void FillHolidayDataset()
        //{

        //    DateTime lastDate = GetFirstDayOfNextMonth();
        //    dsHolidays = GetCurrentMonthData(DateTime.Today, lastDate);
        //    //DateTime nextDate;
        //    //if (dsHolidays != null)
        //    //{
        //    //    foreach (DataRow dr in dsHolidays.Tables[0].Rows)
        //    //    {
        //    //        nextDate = (DateTime)dr["WTD_Date"];
        //    //        if (nextDate == txtMFValidity_CalendarExtender.Day.Date)
        //    //        {
        //    //            e.Cell.BackColor = System.Drawing.Color.Pink;
        //    //        }
        //    //    }
        //    //}
        //}

        //protected DateTime GetFirstDayOfNextMonth()
        //{
        //    int monthNumber, yearNumber;
        //    if (txtMFValidity_CalendarExtender.SelectedDate.Value.Month == 12)
        //    {
        //        monthNumber = 1;
        //        yearNumber = txtMFValidity_CalendarExtender.SelectedDate.Value.Year + 1;
        //    }
        //    else
        //    {
        //        monthNumber = txtMFValidity_CalendarExtender.SelectedDate.Value.Month + 1;
        //        yearNumber = txtMFValidity_CalendarExtender.SelectedDate.Value.Year;
        //    }
        //    DateTime lastDate = new DateTime(yearNumber, monthNumber, 1);
        //    return lastDate;
        //}

        //protected DataSet GetCurrentMonthData(DateTime firstDate, DateTime lastDate)
        //{
        //    DataSet dsMonth = new DataSet();
        //    connectionstring = "Data Source=SERVER; Initial Catalog=WealthERPQA;" + " user id=sa; password=pcg123#;";
        //    conn = new SqlConnection(connectionstring);
        //    conn.Open();

        //    Database db = DatabaseFactory.CreateDatabase("wealtherp");
        //    string query = "SELECT WTD_Date FROM WerpTradeDate WHERE WTD_Date >= '" + firstDate.ToShortDateString() + "' AND WTD_Date < '" + lastDate.ToShortDateString() + "'";
        //    DbCommand getBranchIdCmd = db.GetSqlStringCommand(query);
        //    dsMonth = db.ExecuteDataSet(getBranchIdCmd);



        //    //SqlCommand cmd = new SqlCommand(query, conn);


        //    //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        //    //sqlDataAdapter.SelectCommand = new SqlCommand(query, conn);
        //    //sqlDataAdapter.Fill(dsMonth);

        //    return dsMonth;
        //}
        //protected override void OnPreRender(EventArgs e)
        //{
        //    base.OnPreRender(e);
        //    DateTime nextDate;
        //    if (dsHolidays != null)
        //    {
        //        foreach (DataRow dr in dsHolidays.Tables[0].Rows)
        //        {
        //            nextDate = (DateTime)dr["WTD_Date"];
        //            if (nextDate == e.Day.Date)
        //            {
        //                e.Cell.BackColor = System.Drawing.Color.Pink;
        //            }
        //        }
        //    }
        //}
        //protected void Calendar1_DayRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
        //{
        //    DateTime nextDate;
        //    if (dsHolidays != null)
        //    {
        //        foreach (DataRow dr in dsHolidays.Tables[0].Rows)
        //        {
        //            nextDate = (DateTime)dr["WTD_Date"];
        //            if (nextDate == e.Day.Date)
        //            {
        //                e.Cell.BackColor = System.Drawing.Color.Pink;
        //            }
        //        }
        //    }
        //}
        //protected void Calendar1_VisibleMonthChanged(object sender,
        //    MonthChangedEventArgs e)
        //{
        //    FillHolidayDataset();
        //}


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                advisorVo = (AdvisorVo)Session["advisorVo"];
                userVo = (UserVo)Session["iffUserVo"];
                LOBId = Session["LOBId"].ToString();
                cvMFExpiryDate.ValueToCompare = DateTime.Now.ToShortDateString();
                cvInsExpiryDate.ValueToCompare = DateTime.Now.ToShortDateString();
                cvPostExpiryDate.ValueToCompare = DateTime.Now.ToShortDateString();
                cvRealEstExpiryDate.ValueToCompare = DateTime.Now.ToShortDateString();
                cvFixIncExpiryDate.ValueToCompare = DateTime.Now.ToShortDateString();
                cvLiabExpiryDate.ValueToCompare = DateTime.Now.ToShortDateString();

                if (!IsPostBack)
                {
                    txtMFValidity_CalendarExtender.Animated = true;
                    //txtMFValidity_CalendarExtender.SelectedDate = DateTime.Today;

                    //FillHolidayDataset();

                    {
                        if (Session["mf1"] != null)
                        {
                            mf = Session["mf1"].ToString();
                        }
                        if (Session["equityBrokerCash1"] != null)
                        {
                            equityBrokerCash = Session["equityBrokerCash1"].ToString();
                        }
                        if (Session["equityBrokerDerivative1"] != null)
                        {
                            equityBrokerDerivative = Session["equityBrokerDerivative1"].ToString();
                        }
                        if (Session["equitySubBrokerCash1"] != null)
                        {
                            equitySubCash = Session["equitySubBrokerCash1"].ToString();
                        }
                        if (Session["equitySubBrokerDerivative1"] != null)
                        {
                            equitySubDerivative = Session["equitySubBrokerDerivative1"].ToString();
                        }
                        if (Session["equityRemissaryCash1"] != null)
                        {
                            equityRemissaryCash = Session["equityRemissaryCash1"].ToString();
                        }
                        if (Session["equityRemissaryDerivative1"] != null)
                        {
                            equityRemissaryDerivative = Session["equityRemissaryDerivative1"].ToString();
                        }
                        if (Session["pmsBrokerCash1"] != null)
                            strPmsBrokerCash = Session["pmsBrokerCash1"].ToString();
                        if (Session["pmsBrokerDerivative1"] != null)
                            strPmsBrokerDerivative = Session["pmsBrokerDerivative1"].ToString();
                        if (Session["pmsSubBrokerCash1"] != null)
                            strPmsSubBrokerCash = Session["pmsSubBrokerCash1"].ToString();
                        if (Session["pmsSubBrokerDerivative1"] != null)
                            strPmsSubBrokerDerivative = Session["pmsSubBrokerDerivative1"].ToString();
                        if (Session["pmsRemissaryCash1"] != null)
                            strPmsRemissaryCash = Session["pmsRemissaryCash1"].ToString();
                        if (Session["pmsRemissaryDerivative1"] != null)
                            strPmsRemissaryDerivative = Session["pmsRemissaryDerivative1"].ToString();
                        if (Session["commBrokerCash1"] != null)
                            strCommBrokerCash = Session["commBrokerCash1"].ToString();
                        if (Session["commBrokerDerivative1"] != null)
                            strCommBrokerDerivative = Session["commBrokerDerivative1"].ToString();
                        if (Session["commSubBrokerCash1"] != null)
                            strCommSubBrokerCash = Session["commSubBrokerCash1"].ToString();
                        if (Session["commSubBrokerDerivative1"] != null)
                            strCommSubBrokerDerivative = Session["commSubBrokerDerivative1"].ToString();
                        if (Session["commRemissaryCash1"] != null)
                            strCommRemissaryCash = Session["commRemissaryCash1"].ToString();
                        if (Session["commRemissaryDerivative1"] != null)
                            strCommRemissaryDerivative = Session["commRemissaryDerivative1"].ToString();
                        if (Session["insuranceAgent1"] != null)
                            strInsuranceAgent = Session["insuranceAgent1"].ToString();
                        if (Session["postalSavingsAgent1"] != null)
                            strPostalSavingsAgent = Session["postalSavingsAgent1"].ToString();
                        if (Session["realEstateAgent1"] != null)
                            strRealEstateAgent = Session["realEstateAgent1"].ToString();
                        if (Session["liabilitiesAgent1"] != null)
                            strLiabilitiesAgent = Session["liabilitiesAgent1"].ToString();
                        if (Session["fixedIncomeAgent1"] != null)
                            strFixedIncomeAgent = Session["fixedIncomeAgent1"].ToString();

                        //login.Visible = true;
                    }
                    divMFDetails.Visible = false;
                    BrokerCash.Visible = false;
                    BrokerDerivative.Visible = false;
                    EquitySubBrokerCash.Visible = false;
                    EquitySubBrokerDerivative.Visible = false;
                    RemissaryCash.Visible = false;
                    RemissaryDerivative.Visible = false;
                    btnLogin.Visible = false;
                    Insurance.Visible = false;
                    PostalSavings.Visible = false;
                    FixedIncome.Visible = false;
                    RealEstate.Visible = false;
                    Liabilities.Visible = false;
                    PMSBrokerCash.Visible = false;
                    PMSBrokerDerivative.Visible = false;
                    PMSSubBrokerCash.Visible = false;
                    PMSSubBrokerDerivative.Visible = false;
                    PMSRemissaryCash.Visible = false;
                    PMSRemissaryDerivative.Visible = false;
                    CommoditiesBrokerCash.Visible = false;
                    CommoditiesBrokerDerivatives.Visible = false;
                    CommoditiesSubBrokerCash.Visible = false;
                    CommoditiesSubBrokerDerivatives.Visible = false;
                    CommoditiesRemissaryCash.Visible = false;
                    CommoditiesRemissaryDerivatives.Visible = false;

                    if (mf == "mf")
                    {
                        divMFDetails.Visible = true;
                    }
                    if (equityBrokerCash == "equityBrokerCash")
                    {
                        LoadSubBrokerCode();
                        BrokerCash.Visible = true;
                        trBCNse.Visible = false;
                    }
                    if (equityBrokerDerivative == "equityBrokerDerivative")
                    {
                        LoadSubBrokerCode();
                        BrokerDerivative.Visible = true;
                        trBDNse.Visible = false;
                    }

                    if (equitySubCash == "equitySubBrokerCash")
                    {
                        LoadSubBrokerCode();
                        EquitySubBrokerCash.Visible = true;
                        trSCNse.Visible = false;
                    }
                    if (equitySubDerivative == "equitySubBrokerDerivaitve")
                    {
                        LoadSubBrokerCode();
                        EquitySubBrokerDerivative.Visible = true;
                        trSDNse.Visible = false;
                    }
                    if (equityRemissaryCash == "equityRemissaryCash")
                    {
                        LoadSubBrokerCode();
                        RemissaryCash.Visible = true;
                        trRCNse.Visible = false;
                    }
                    if (equityRemissaryDerivative == "equityRemissaryDerivative")
                    {
                        LoadSubBrokerCode();
                        RemissaryDerivative.Visible = true;
                        trRDNse.Visible = false;
                    }
                    //Mahesh's code
                    if (strInsuranceAgent == "insuranceAgent")
                        Insurance.Visible = true;
                    if (strLiabilitiesAgent == "liabilitiesAgent")
                        Liabilities.Visible = true;
                    if (strPostalSavingsAgent == "postalSavingsAgent")
                        PostalSavings.Visible = true;
                    if (strRealEstateAgent == "realEstateAgent")
                        RealEstate.Visible = true;
                    if (strFixedIncomeAgent == "fixedIncomeAgent")
                        FixedIncome.Visible = true;
                    if (strPmsBrokerCash == "pmsBrokerCash")
                    {
                        LoadSubBrokerCode();
                        PMSBrokerCash.Visible = true;
                        trPMSBCNse.Visible = false;
                    }
                    if (strPmsBrokerDerivative == "pmsBrokerDerivative")
                    {
                        LoadSubBrokerCode();
                        PMSBrokerDerivative.Visible = true;
                        trPMSBDNse.Visible = false;
                    }
                    if (strPmsSubBrokerCash == "pmsSubBrokerCash")
                    {
                        LoadSubBrokerCode();
                        PMSSubBrokerCash.Visible = true;
                        trPMSSBCNse.Visible = false;
                    }
                    if (strPmsSubBrokerDerivative == "pmsSubBrokerDerivative")
                    {
                        LoadSubBrokerCode();
                        PMSSubBrokerDerivative.Visible = true;
                        trPMSSBDNse.Visible = false;
                    }
                    if (strPmsRemissaryCash == "pmsRemissaryCash")
                    {
                        LoadSubBrokerCode();
                        PMSRemissaryCash.Visible = true;
                        trPMSRCNse.Visible = false;
                    }
                    if (strPmsRemissaryDerivative == "pmsRemissaryDerivative")
                    {
                        LoadSubBrokerCode();
                        PMSRemissaryDerivative.Visible = true;
                        trPMSRDNse.Visible = false;
                    }
                    if (strCommBrokerCash == "commBrokerCash")
                    {
                        LoadSubBrokerCode();
                        CommoditiesBrokerCash.Visible = true;
                        trCommBCNse.Visible = false;
                    }
                    if (strCommBrokerDerivative == "commBrokerDerivative")
                    {
                        LoadSubBrokerCode();
                        CommoditiesBrokerDerivatives.Visible = true;
                        trCommBDNse.Visible = false;
                    }
                    if (strCommSubBrokerCash == "commSubBrokerCash")
                    {
                        LoadSubBrokerCode();
                        CommoditiesSubBrokerCash.Visible = true;
                        trCommSBCNse.Visible = false;
                    }
                    if (strCommSubBrokerDerivative == "commSubBrokerDerivative")
                    {
                        LoadSubBrokerCode();
                        CommoditiesSubBrokerDerivatives.Visible = true;
                        trCommSBDNse.Visible = false;
                    }
                    if (strCommRemissaryCash == "commRemissaryCash")
                    {
                        LoadSubBrokerCode();
                        CommoditiesRemissaryCash.Visible = true;
                        trCommRCNse.Visible = false;
                    }
                    if (strCommRemissaryDerivative == "commRemissaryDerivative")
                    {
                        LoadSubBrokerCode();
                        CommoditiesRemissaryDerivatives.Visible = true;
                        trCommRDNse.Visible = false;
                    }
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

                FunctionInfo.Add("Method", "LOB.ascx:Page_Load()");


                object[] objects = new object[8];
                objects[0] = mf;
                objects[1] = equityBrokerCash;
                objects[2] = equityBrokerDerivative;
                objects[3] = equitySubCash;
                objects[4] = equitySubDerivative;
                objects[5] = equityRemissaryCash;
                objects[6] = equityRemissaryDerivative;
                objects[7] = LOBId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void LoadSubBrokerCode()
        {
            dsProductAmc = productMfBo.GetBrokerCodeForLOB();

            ddlEQCBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlEQCBrokerCode.DataTextField = "XB_BrokerName";
            ddlEQCBrokerCode.DataValueField = "XB_BrokerCode";
            ddlEQCBrokerCode.DataBind();
            ddlEQCBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlEQDBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlEQDBrokerCode.DataTextField = "XB_BrokerName";
            ddlEQDBrokerCode.DataValueField = "XB_BrokerCode";
            ddlEQDBrokerCode.DataBind();
            ddlEQDBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));


            ddlSubBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlSubBrokerCode.DataTextField = "XB_BrokerName";
            ddlSubBrokerCode.DataValueField = "XB_BrokerCode";
            ddlSubBrokerCode.DataBind();
            ddlSubBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlBrokerCode.DataTextField = "XB_BrokerName";
            ddlBrokerCode.DataValueField = "XB_BrokerCode";
            ddlBrokerCode.DataBind();
            ddlBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlEQRCBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlEQRCBrokerCode.DataTextField = "XB_BrokerName";
            ddlEQRCBrokerCode.DataValueField = "XB_BrokerCode";
            ddlEQRCBrokerCode.DataBind();
            ddlEQRCBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlEQRDBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlEQRDBrokerCode.DataTextField = "XB_BrokerName";
            ddlEQRDBrokerCode.DataValueField = "XB_BrokerCode";
            ddlEQRDBrokerCode.DataBind();
            ddlEQRDBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlPMSBrCashBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlPMSBrCashBrokerCode.DataTextField = "XB_BrokerName";
            ddlPMSBrCashBrokerCode.DataValueField = "XB_BrokerCode";
            ddlPMSBrCashBrokerCode.DataBind();
            ddlPMSBrCashBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));


            ddlPMSBrDerBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlPMSBrDerBrokerCode.DataTextField = "XB_BrokerName";
            ddlPMSBrDerBrokerCode.DataValueField = "XB_BrokerCode";
            ddlPMSBrDerBrokerCode.DataBind();
            ddlPMSBrDerBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));


            ddlPMSSubBrCashBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlPMSSubBrCashBrokerCode.DataTextField = "XB_BrokerName";
            ddlPMSSubBrCashBrokerCode.DataValueField = "XB_BrokerCode";
            ddlPMSSubBrCashBrokerCode.DataBind();
            ddlPMSSubBrCashBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));


            ddlPMSSubBrDerBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlPMSSubBrDerBrokerCode.DataTextField = "XB_BrokerName";
            ddlPMSSubBrDerBrokerCode.DataValueField = "XB_BrokerCode";
            ddlPMSSubBrDerBrokerCode.DataBind();
            ddlPMSSubBrDerBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlPMSRemCashBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlPMSRemCashBrokerCode.DataTextField = "XB_BrokerName";
            ddlPMSRemCashBrokerCode.DataValueField = "XB_BrokerCode";
            ddlPMSRemCashBrokerCode.DataBind();
            ddlPMSRemCashBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlPMSRemDerBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlPMSRemDerBrokerCode.DataTextField = "XB_BrokerName";
            ddlPMSRemDerBrokerCode.DataValueField = "XB_BrokerCode";
            ddlPMSRemDerBrokerCode.DataBind();
            ddlPMSRemDerBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlCommBrCashBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlCommBrCashBrokerCode.DataTextField = "XB_BrokerName";
            ddlCommBrCashBrokerCode.DataValueField = "XB_BrokerCode";
            ddlCommBrCashBrokerCode.DataBind();
            ddlCommBrCashBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlCommBrDerBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlCommBrDerBrokerCode.DataTextField = "XB_BrokerName";
            ddlCommBrDerBrokerCode.DataValueField = "XB_BrokerCode";
            ddlCommBrDerBrokerCode.DataBind();
            ddlCommBrDerBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlCommSubBrCashBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlCommSubBrCashBrokerCode.DataTextField = "XB_BrokerName";
            ddlCommSubBrCashBrokerCode.DataValueField = "XB_BrokerCode";
            ddlCommSubBrCashBrokerCode.DataBind();
            ddlCommSubBrCashBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlCommSubBrDerBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlCommSubBrDerBrokerCode.DataTextField = "XB_BrokerName";
            ddlCommSubBrDerBrokerCode.DataValueField = "XB_BrokerCode";
            ddlCommSubBrDerBrokerCode.DataBind();
            ddlCommSubBrDerBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlCommRemCashBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlCommRemCashBrokerCode.DataTextField = "XB_BrokerName";
            ddlCommRemCashBrokerCode.DataValueField = "XB_BrokerCode";
            ddlCommRemCashBrokerCode.DataBind();
            ddlCommRemCashBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));

            ddlCommRemDerBrokerCode.DataSource = dsProductAmc.Tables[0];
            ddlCommRemDerBrokerCode.DataTextField = "XB_BrokerName";
            ddlCommRemDerBrokerCode.DataValueField = "XB_BrokerCode";
            ddlCommRemDerBrokerCode.DataBind();
            ddlCommRemDerBrokerCode.Items.Insert(0, new ListItem("Select Broker Name", "Select Broker Name"));
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            // Calendar2.Visible = true;
        }

        protected void btnMFSubmit_Click(object sender, EventArgs e)
        {

            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();

            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;
            string segment = "";
            string assetClass = "MF";
            string category = "INT";
            try
            {


                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.IdentifierTypeCode = "ARN";
                advisorLOBVo.OrganizationName = txtMFOrgName.Text.ToString();
                advisorLOBVo.Identifier = txtMFARNCode.Text.ToString();
                advisorLOBVo.ValidityDate = DateTime.Parse(txtMFValidity.Text.ToString());
                advisorLOBVo.LicenseNumber = "";
                advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                btnMFSubmit.Enabled = false;
                Session.Remove("mf1");
                divMFDetails.Visible = false;
                PageRedirect();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnMFSubmit_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            // txtMFValidity.Text = Calendar2.SelectedDate.ToShortDateString();
        }

        protected void btnBrokerCash_Click(object sender, EventArgs e)
        {

            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "EQ";
            string category = "BKR";
            string segment = "CSH";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;
            string identifierType = ddlBrokerCashId.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                //  advisorLOBVo.LOBId = advisorBo.getId();
                // advisorLOBVo.OrganizationName = txtEquBrCashOrgName.Text.ToString();
                advisorLOBVo.OrganizationName = ddlEQCBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlEQCBrokerCode.SelectedItem.Value.ToString();

                DateTime mydate = new DateTime(DateTime.Today.Year, 1, 1);
                advisorLOBVo.ValidityDate = mydate;
                advisorLOBVo.LicenseNumber = " ";
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtBrokerCashBseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtBrokerCashNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtBrokerCashBseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                    txtBrokerCashBseNumber.Text = advisorLOBVo.Identifier.ToString();
                    //  advisorLOBVo.LOBId = advisorBo.getId();
                    advisorLOBVo.Identifier = txtBrokerCashNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                    txtBrokerCashNseNumber.Text = advisorLOBVo.Identifier.ToString();
                    txtBrokerCashBseNumber.Visible = true;
                    txtBrokerCashNseNumber.Visible = true;
                    lblBrokerCashBseNumber.Visible = true;
                    lblBrokerCashNseNumber.Visible = true;
                }
                btnBrokerCash.Enabled = false;
                Session.Remove("equityBrokerCash1");
                BrokerCash.Visible = false;
                PageRedirect();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnBrokerCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlBrokerCashId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBrokerCashId.SelectedItem.Value == "BSE")
            {
                trBCNse.Visible = false;
                trBCBse.Visible = true;

            }
            if (ddlBrokerCashId.SelectedItem.Value == "NSE")
            {
                trBCNse.Visible = true;
                trBCBse.Visible = false;

            }
            if (ddlBrokerCashId.SelectedItem.Value == "BOTH BSE and NSE")
            {
                trBCNse.Visible = true;
                trBCBse.Visible = true;

            }
        }

        protected void btnBrokerDeriv_Click(object sender, EventArgs e)
        {

            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;
            string assetClass = "EQ";
            string category = "BKR";
            string segment = "DER";
            string identifierType = ddlBrokerDeivIdentifier.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                //advisorLOBVo.LOBId = advisorBo.getId();

                //advisorLOBVo.OrganizationName = txtEquityDerivOrgName.Text.ToString();
                advisorLOBVo.OrganizationName = ddlEQDBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlEQDBrokerCode.SelectedItem.Value.ToString();
                DateTime mydate = new DateTime(DateTime.Today.Year, 1, 1);
                advisorLOBVo.ValidityDate = mydate;
                advisorLOBVo.LicenseNumber = txtBrokerDerivLicenseNumber.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtBrokerDerivBseNUmber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtBrokerDerivNseNUmber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtBrokerDerivBseNUmber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                    //   advisorLOBVo.LOBId = advisorBo.getId();
                    advisorLOBVo.Identifier = txtBrokerDerivNseNUmber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                    txtBrokerDerivBseNUmber.Visible = true;
                    txtBrokerDerivNseNUmber.Visible = true;
                    lblBrokerDerivBseNumber.Visible = true;
                    lblBrokerDerivNseNUmber.Visible = true;
                }
                btnBrokerDeriv.Enabled = false;
                Session.Remove("equityBrokerDerivative1");
                BrokerDerivative.Visible = false;
                PageRedirect();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnBrokerDeriv_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlBrokerDeivIdentifier_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlBrokerDeivIdentifier.SelectedItem.Value == "BSE")
            {
                trBDBse.Visible = true;
                trBDNse.Visible = false;
                            }
            if (ddlBrokerDeivIdentifier.SelectedItem.Value == "NSE")
            {
                trBDNse.Visible = true;
                trBDBse.Visible = false;
                            }
            if (ddlBrokerDeivIdentifier.SelectedItem.Value == "BOTH BSE and NSE")
            {
                trBDBse.Visible = true;
                trBDNse.Visible = true;
            }

        }

        protected void ddlSubCashIdentifier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubCashIdentifier.SelectedItem.Value == "BSE")
            {
                trSCNse.Visible = false;
                trSCBse.Visible = true;

            }
            if (ddlSubCashIdentifier.SelectedItem.Value == "NSE")
            {
                trSCBse.Visible = false;
                trSCNse.Visible = true;

            }
            if (ddlSubCashIdentifier.SelectedItem.Value == "BOTH BSE and NSE")
            {
                trSCNse.Visible = true;
                trSCBse.Visible = true;

            }
        }

        protected void btnSubCash_Click(object sender, EventArgs e)
        {

            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;
            string assetClass = "EQ";
            string category = "SBR";
            string segment = "CSH";
            string identifierType = ddlSubCashIdentifier.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                //  advisorLOBVo.LOBId = advisorBo.getId();
                //   advisorLOBVo.OrganizationName = txtSubCashOrgName.Text.ToString();
                advisorLOBVo.OrganizationName = ddlBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlBrokerCode.SelectedItem.Value.ToString();
                DateTime mydate = new DateTime(DateTime.Today.Year, 1, 1);
                advisorLOBVo.ValidityDate = mydate;
                advisorLOBVo.LicenseNumber = " ";
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtSubCashBseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtSubCashNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {

                    advisorLOBVo.Identifier = txtSubCashBseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                    //  advisorLOBVo.LOBId = advisorBo.getId();
                    advisorLOBVo.Identifier = txtSubCashNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                    txtSubCashBseNumber.Visible = true;
                    txtSubCashNseNumber.Visible = true;
                    lblSubCashBseNumber.Visible = true;
                    lblSubCashNseNumber.Visible = true;

                }
                btnSubCash.Enabled = false;
                Session.Remove("equitySubBrokerCash1");
                EquitySubBrokerCash.Visible = false;
                PageRedirect();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnSubCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlSubDerivIdentifier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubDerivIdentifier.SelectedItem.Value == "BSE")
            {
                trSDBse.Visible = true;
                trSDNse.Visible = false;

            }
            if (ddlSubDerivIdentifier.SelectedItem.Value == "NSE")
            {
                trSDBse.Visible = false;
                trSDNse.Visible = true;

            }
            if (ddlSubDerivIdentifier.SelectedItem.Value == "BOTH BSE and NSE")
            {
                trSDBse.Visible = true;
                trSDNse.Visible = true;

            }
        }

        protected void btnSubDeriv_Click(object sender, EventArgs e)
        {

            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;
            string assetClass = "EQ";
            string category = "SBR";
            string segment = "DER";
            string identifierType = ddlSubDerivIdentifier.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                // advisorLOBVo.LOBId = advisorBo.getId();
                //  advisorLOBVo.OrganizationName = txtSubDerivOrgName.Text.ToString();

                advisorLOBVo.BrokerCode = ddlSubBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.OrganizationName = ddlSubBrokerCode.SelectedItem.Text.ToString();
                DateTime mydate = new DateTime(DateTime.Today.Year, 1, 1);
                advisorLOBVo.ValidityDate = mydate;
                advisorLOBVo.LicenseNumber = txtSubDerivLicenseNumber.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtSubDerivBseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtSubDerivNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtSubDerivBseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                    // advisorLOBVo.LOBId = advisorBo.getId();
                    advisorLOBVo.Identifier = txtSubDerivNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                    txtSubDerivBseNumber.Visible = true;
                    txtSubDerivNseNumber.Visible = true;
                    lblSubDerivBseNumber.Visible = true;
                    lblSubDerivNseNumber.Visible = true;

                }
                btnSubDeriv.Enabled = false;
                Session.Remove("equitySubBrokerDerivative1");
                EquitySubBrokerDerivative.Visible = false;
                PageRedirect();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnSubDeriv_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlRemissCashIdentifier_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlRemissCashIdentifier.SelectedItem.Value == "BSE")
            {
                trRCBse.Visible = true;
                trRCNse.Visible = false;
            }
            if (ddlRemissCashIdentifier.SelectedItem.Value == "NSE")
            {
                trRCBse.Visible = false;
                trRCNse.Visible = true;
            }
            if (ddlRemissCashIdentifier.SelectedItem.Value == "BOTH BSE and NSE")
            {
                trRCBse.Visible = true;
                trRCNse.Visible = true;
            }
        }

        protected void btnRemissCash_Click(object sender, EventArgs e)
        {

            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;
            string assetClass = "EQ";
            string category = "REM";
            string segment = "CSH";
            string identifierType = ddlRemissCashIdentifier.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                // advisorLOBVo.LOBId = advisorBo.getId();

                advisorLOBVo.OrganizationName = ddlEQRCBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlEQRCBrokerCode.SelectedItem.Value.ToString();
                DateTime mydate = new DateTime(DateTime.Today.Year, 1, 1);
                advisorLOBVo.ValidityDate = mydate;
                advisorLOBVo.LicenseNumber = " ";
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtRemissCashBseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtRemissCashNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtRemissCashBseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                    //  advisorLOBVo.LOBId = advisorBo.getId();
                    advisorLOBVo.Identifier = txtRemissCashNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                    txtRemissCashBseNumber.Visible = true;
                    txtRemissCashNseNumber.Visible = true;
                    lblRemissCashBseNUmber.Visible = true;
                    lblRemissCashNseNumber.Visible = true;
                }
                btnRemissCash.Enabled = false;
                Session.Remove("equityRemissaryCash1");
                RemissaryCash.Visible = false;
                PageRedirect();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnRemissCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlRemissDerivIdentifer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRemissDerivIdentifer.SelectedItem.Value == "BSE")
            {
                trRDBse.Visible = true;
                trRDNse.Visible = false;


            }
            if (ddlRemissDerivIdentifer.SelectedItem.Value == "NSE")
            {
                trRDBse.Visible = false;
                trRDNse.Visible = true;


            }
            if (ddlRemissDerivIdentifer.SelectedItem.Value == "BOTH BSE and NSE")
            {
                trRDBse.Visible = true;
                trRDNse.Visible = true;

            }
        }

        protected void btnRemissDeriv_Click(object sender, EventArgs e)
        {

            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;
            string assetClass = "EQ";
            string category = "REM";
            string segment = "DER";
            string identifierType = ddlRemissDerivIdentifer.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                // advisorLOBVo.LOBId = advisorBo.getId();

                advisorLOBVo.OrganizationName = ddlEQRDBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlEQRDBrokerCode.SelectedItem.Value.ToString();
                DateTime mydate = new DateTime(DateTime.Today.Year, 1, 1);
                advisorLOBVo.ValidityDate = mydate;

                advisorLOBVo.LicenseNumber = txtRemissDerivLicenseNumber.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtRemissDerivBseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtRemissDerivNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtRemissDerivBseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                    advisorLOBVo.Identifier = txtRemissDerivNseNumber.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                    txtRemissDerivBseNumber.Visible = true;
                    txtRemissDerivNseNumber.Visible = true;
                    lblRemissDerivBseNumber.Visible = true;
                    lblRemissDerivNseUNumber.Visible = true;

                }
                btnRemissDeriv.Enabled = false;
                Session.Remove("equityRemissaryDerivative1");
                RemissaryDerivative.Visible = false;
                PageRedirect();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnRemissDeriv_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UserLoginMessage','none');", true);
        }

        protected void ddlPMSBrCashIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPMSBrCashIdType.SelectedItem.Value == "BSE")
            {
                trPMSBCBse.Visible = true;
                trPMSBCNse.Visible = false;
            }
            else
                if (ddlPMSBrCashIdType.SelectedItem.Value == "NSE")
                {
                    trPMSBCNse.Visible = true;
                    trPMSBCBse.Visible = false;
                }
                else
                {
                    trPMSBCBse.Visible = true;
                    trPMSBCNse.Visible = true;
                }
        }

        protected void ddlPMSBrDerIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPMSBrDerIdType.SelectedItem.Value == "BSE")
            {
                trPMSBDBse.Visible = true;
                trPMSBDNse.Visible = false;
            }
            else
                if (ddlPMSBrDerIdType.SelectedItem.Value == "NSE")
                {
                    trPMSBDNse.Visible = true;
                    trPMSBDBse.Visible = false;
                }
                else
                {
                    trPMSBDBse.Visible = true;
                    trPMSBDNse.Visible = true;
                }
        }

        protected void ddlPMSSubBrCashIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPMSSubBrCashIdType.SelectedItem.Value == "BSE")
            {
                trPMSSBCBse.Visible = true;
                trPMSSBCNse.Visible = false;
            }
            else
                if (ddlPMSSubBrCashIdType.SelectedItem.Value == "NSE")
                {
                    trPMSSBCNse.Visible = true;
                    trPMSSBCBse.Visible = false;
                }
                else
                {
                    trPMSSBCBse.Visible = true;
                    trPMSSBCNse.Visible = true;
                }
        }

        protected void ddlPMSSubBrDerIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPMSSubBrDerIdType.SelectedItem.Value == "BSE")
            {
                trPMSSBDBse.Visible = true;
                trPMSSBDNse.Visible = false;
            }
            else
                if (ddlPMSSubBrDerIdType.SelectedItem.Value == "NSE")
                {
                    trPMSSBDNse.Visible = true;
                    trPMSSBDBse.Visible = false;
                }
                else
                {
                    trPMSSBDBse.Visible = true;
                    trPMSSBDNse.Visible = true;
                }

        }

        protected void ddlPMSRemissCashIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPMSRemissCashIdType.SelectedItem.Value == "BSE")
            {
                trPMSRCBse.Visible = true;
                trPMSRCNse.Visible = false;
            }
            else
                if (ddlPMSRemissCashIdType.SelectedItem.Value == "NSE")
                {
                    trPMSRCNse.Visible = true;
                    trPMSRCBse.Visible = false;
                }
                else
                {
                    trPMSRCBse.Visible = true;
                    trPMSRCNse.Visible = true;
                }

        }

        protected void ddlPMSRemissDerIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPMSRemissDerIdType.SelectedItem.Value == "BSE")
            {
                trPMSRDBse.Visible = true;
                trPMSRDNse.Visible = false;
            }
            else
                if (ddlPMSRemissDerIdType.SelectedItem.Value == "NSE")
                {
                    trPMSRDNse.Visible = true;
                    trPMSRDBse.Visible = false;
                }
                else
                {
                    trPMSRDBse.Visible = true;
                    trPMSRDNse.Visible = true;
                }
        }

        protected void ddlCommBrCashIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCommBrCashIdType.SelectedItem.Value == "BSE")
            {
                trCommBCBse.Visible = true;
                trCommBCNse.Visible = false;
            }
            else
                if (ddlCommBrCashIdType.SelectedItem.Value == "NSE")
                {
                    trCommBCNse.Visible = true;
                    trCommBCBse.Visible = false;
                }
                else
                {
                    trCommBCBse.Visible = true;
                    trCommBCNse.Visible = true;
                }
        }

        protected void ddlCommBrDerIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCommBrDerIdType.SelectedItem.Value == "BSE")
            {
                trCommBDBse.Visible = true;
                trCommBDNse.Visible = false;
            }
            else
                if (ddlCommBrDerIdType.SelectedItem.Value == "NSE")
                {
                    trCommBDNse.Visible = true;
                    trCommBDBse.Visible = false;
                }
                else
                {
                    trCommBDBse.Visible = true;
                    trCommBDNse.Visible = true;
                }
        }

        protected void ddlCommSubBrCashIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCommSubBrCashIdType.SelectedItem.Value == "BSE")
            {
                trCommSBCBse.Visible = true;
                trCommSBCNse.Visible = false;
            }
            else
                if (ddlCommSubBrCashIdType.SelectedItem.Value == "NSE")
                {
                    trCommSBCNse.Visible = true;
                    trCommSBCBse.Visible = false;
                }
                else
                {
                    trCommSBCBse.Visible = true;
                    trCommSBCNse.Visible = true;
                }
        }

        protected void ddlCommSubBrDerIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCommSubBrDerIdType.SelectedItem.Value == "BSE")
            {
                trCommSBDBse.Visible = true;
                trCommSBDNse.Visible = false;
            }
            else
                if (ddlCommSubBrDerIdType.SelectedItem.Value == "NSE")
                {
                    trCommSBDNse.Visible = true;
                    trCommSBDBse.Visible = false;
                }
                else
                {
                    trCommSBDBse.Visible = true;
                    trCommSBDNse.Visible = true;
                }
        }

        protected void ddlCommRemissCashIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCommRemissCashIdType.SelectedItem.Value == "BSE")
            {
                trCommRCBse.Visible = true;
                trCommRCNse.Visible = false;
            }
            else
                if (ddlCommRemissCashIdType.SelectedItem.Value == "NSE")
                {
                    trCommRCNse.Visible = true;
                    trCommRCBse.Visible = false;
                }
                else
                {
                    trCommRCBse.Visible = true;
                    trCommRCNse.Visible = true;
                }
        }

        protected void ddlCommRemissIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCommRemissIdType.SelectedItem.Value == "BSE")
            {
                trCommRDBse.Visible = true;
                trCommRDNse.Visible = false;
            }
            else
                if (ddlCommRemissIdType.SelectedItem.Value == "NSE")
                {
                    trCommRDNse.Visible = true;
                    trCommRDBse.Visible = false;
                }
                else
                {
                    trCommRDBse.Visible = true;
                    trCommRDNse.Visible = true;
                }
        }

        private void PageRedirect()
        {
            if (Session["mf1"] == null && Session["equityBrokerCash1"] == null && Session["equityBrokerDerivative1"] == null && Session["equitySubBrokerCash1"] == null && Session["equitySubBrokerDerivative1"] == null && Session["equityRemissaryCash1"] == null && Session["equityRemissaryDerivative1"] == null && Session["pmsBrokerCash1"] == null &&
                Session["pmsBrokerDerivative1"] == null && Session["pmsSubBrokerCash1"] == null && Session["pmsSubBrokerDerivative1"] == null && Session["pmsRemissaryCash1"] == null && Session["pmsRemissaryDerivative1"] == null && Session["commBrokerCash1"] == null && Session["commBrokerDerivative1"] == null &&
                Session["commSubBrokerCash1"] == null && Session["commSubBrokerDerivative1"] == null && Session["commRemissaryCash1"] == null && Session["commRemissaryDerivative1"] == null && Session["insuranceAgent1"] == null && Session["liabilitiesAgent1"] == null && Session["postalSavingsAgent1"] == null &&
                Session["realEstateAgent1"] == null && Session["fixedIncomeAgent1"] == null)
            {
                if (Session["LOBId"].ToString() == "lob")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFFAdd','none');", true);
                }
                else if (Session["LOBId"].ToString() == "FromReg")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('UserLoginMessage','none');", true);
                }
            }
        }

        protected void btnPMSBrCash_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "PMS";
            string category = "BKR";
            string segment = "CSH";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlPMSBrCashIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                //  advisorLOBVo.LOBId = advisorBo.getId();
                advisorLOBVo.OrganizationName = ddlPMSBrCashBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlPMSBrCashBrokerCode.SelectedItem.Value.ToString();
                //DateTime mydate = new DateTime(DateTime.Today.Year, 1, 1);
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = String.Empty;
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtPMSBrCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtPMSBrCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtPMSBrCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                    advisorLOBVo.Identifier = txtPMSBrCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                btnPMSBrCash.Enabled = false;
                Session.Remove("pmsBrokerCash1");
                PMSBrokerCash.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnPMSBrCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnPMSBrDer_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "PMS";
            string category = "BKR";
            string segment = "DER";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;



            string identifierType = ddlPMSBrDerIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlPMSBrDerBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlPMSBrDerBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = txtPMSBrDerLicenseNum.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtPMSBrDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtPMSBrDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtPMSBrDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                    advisorLOBVo.Identifier = txtPMSBrDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                btnPMSBrDer.Enabled = false;
                Session.Remove("pmsBrokerDerivative1");
                PMSBrokerDerivative.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnPMSBrDer_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnPMSSubBrCash_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "PMS";
            string category = "SBR";
            string segment = "CSH";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;



            string identifierType = ddlPMSSubBrCashIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlPMSSubBrCashBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlPMSSubBrCashBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = String.Empty;
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtPMSSubBrCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtPMSSubBrCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtPMSSubBrCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                    advisorLOBVo.Identifier = txtPMSSubBrCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                btnPMSSubBrCash.Enabled = false;
                Session.Remove("pmsSubBrokerCash1");
                PMSSubBrokerCash.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnPMSSubBrCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnPMSSubBrDer_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "PMS";
            string category = "SBR";
            string segment = "DER";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlPMSSubBrDerIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlPMSSubBrDerBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlPMSSubBrDerBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = txtPMSSubBrDerLicenseNum.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtPMSSubBrDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtPMSSubBrDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtPMSSubBrDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                    advisorLOBVo.Identifier = txtPMSSubBrDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                btnPMSSubBrDer.Enabled = false;
                Session.Remove("pmsSubBrokerDerivative1");
                PMSSubBrokerDerivative.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnPMSSubBrDer_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnPMSRemissCash_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "PMS";
            string category = "REM";
            string segment = "CSH";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlPMSRemissCashIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlPMSRemCashBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlPMSRemCashBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = String.Empty;
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtPMSRemissCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtPMSRemissCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtPMSRemissCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                    advisorLOBVo.Identifier = txtPMSRemissCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                btnPMSRemissCash.Enabled = false;
                Session.Remove("pmsRemissaryCash1");
                PMSRemissaryCash.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnPMSRemissCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnPMSRemissDer_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "PMS";
            string category = "REM";
            string segment = "DER";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlPMSRemissDerIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlPMSRemDerBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlPMSRemDerBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = txtPMSRemissDerLicenseNum.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtPMSRemissDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtPMSRemissDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtPMSRemissDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                    advisorLOBVo.Identifier = txtPMSRemissDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                btnPMSRemissDer.Enabled = false;
                Session.Remove("pmsRemissaryDerivative1");
                PMSRemissaryDerivative.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnPMSRemissDer_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnCommBrCash_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "CM";
            string category = "BKR";
            string segment = "CSH";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlCommBrCashIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlCommBrCashBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlCommBrCashBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = String.Empty;
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtCommBrCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtCommBrCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtCommBrCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                    advisorLOBVo.Identifier = txtCommBrCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                btnCommBrCash.Enabled = false;
                Session.Remove("commBrokerCash1");
                CommoditiesBrokerCash.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnCommBrCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnCommBrDer_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "CM";
            string category = "BKR";
            string segment = "DER";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlCommBrDerIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlCommBrDerBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlCommBrDerBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = txtCommBrDerLicenseNum.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtCommBrDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtCommBrDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtCommBrDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                    advisorLOBVo.Identifier = txtCommBrDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                btnCommBrDer.Enabled = false;
                Session.Remove("commBrokerDerivative1");
                CommoditiesBrokerDerivatives.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnCommBrDer_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnCommSubBrCash_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "CM";
            string category = "SBR";
            string segment = "CSH";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlCommSubBrCashIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlCommSubBrCashBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlCommSubBrCashBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = String.Empty;
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtCommSubBrCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtCommSubBrCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtCommSubBrCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                    advisorLOBVo.Identifier = txtCommSubBrCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                btnCommSubBrCash.Enabled = false;
                Session.Remove("commSubBrokerCash1");
                CommoditiesSubBrokerCash.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnCommSubBrCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnCommSubBrDer_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "CM";
            string category = "SBR";
            string segment = "DER";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlCommSubBrDerIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                //advisorLOBVo.OrganizationName = txtCommSubBrDerBrokerName.Text.ToString();
                advisorLOBVo.OrganizationName = ddlCommSubBrDerBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlCommSubBrDerBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = txtCommSubBrDerLicenseNum.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtCommSubBrDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtCommSubBrDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtCommSubBrDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                    advisorLOBVo.Identifier = txtCommSubBrDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                btnCommSubBrDer.Enabled = false;
                Session.Remove("commSubBrokerDerivative1");
                CommoditiesSubBrokerDerivatives.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnCommSubBrDer_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnCommRemissCash_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "CM";
            string category = "REM";
            string segment = "CSH";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlCommRemissCashIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlCommRemCashBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlCommRemCashBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = String.Empty;
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtCommRemissCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtCommRemissCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtCommRemissCashBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                    advisorLOBVo.Identifier = txtCommRemissCashNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                btnCommRemissCash.Enabled = false;
                Session.Remove("commRemissaryCash1");
                CommoditiesRemissaryCash.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnCommRemissCash_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnCommRemissDer_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "CM";
            string category = "REM";
            string segment = "DER";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string identifierType = ddlCommRemissIdType.SelectedItem.Value.ToString();
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = ddlCommRemDerBrokerCode.SelectedItem.Text.ToString();
                advisorLOBVo.BrokerCode = ddlCommRemDerBrokerCode.SelectedItem.Value.ToString();
                advisorLOBVo.ValidityDate = DateTime.MinValue;
                advisorLOBVo.LicenseNumber = txtCommRemissDerLicenseNum.Text.ToString();
                if (identifierType == "BSE")
                {
                    advisorLOBVo.Identifier = txtCommRemissDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "NSE")
                {
                    advisorLOBVo.Identifier = txtCommRemissDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                if (identifierType == "BOTH BSE and NSE")
                {
                    advisorLOBVo.Identifier = txtCommRemissDerBSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "BSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                    advisorLOBVo.Identifier = txtCommRemissDerNSENum.Text.ToString();
                    advisorLOBVo.IdentifierTypeCode = "NSE";
                    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                }
                btnCommRemissDer.Enabled = false;
                Session.Remove("commRemissaryDerivative1");
                CommoditiesRemissaryDerivatives.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnCommRemissDer_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnIns_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "INS";
            string category = "AGN";
            string segment = "";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;


            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = txtInsOrgName.Text.ToString();
                advisorLOBVo.ValidityDate = DateTime.Parse(txtInsAgencyExpiry.Text.ToString());
                advisorLOBVo.AgentNum = txtInsAgentNum.Text.ToString();
                advisorLOBVo.Identifier = txtInsIRDANum.Text.ToString();
                if (txtInsTargetPolicies.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAccount = float.Parse(txtInsTargetPolicies.Text.ToString().Trim());
                if (txtInsTargetSumAssuredAmt.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAmount = double.Parse(txtInsTargetSumAssuredAmt.Text.ToString().Trim());
                if (txtInsTargetPremiumAmt.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetPremiumAmount = double.Parse(txtInsTargetPremiumAmt.Text.ToString().Trim());

                advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);


                //if (identifierType == "BSE")
                //{
                //    advisorLOBVo.Identifier = txtCommRemissDerBSENum.Text.ToString();
                //    advisorLOBVo.IdentifierTypeCode = "BSE";
                //    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                //}
                //if (identifierType == "NSE")
                //{
                //    advisorLOBVo.Identifier = txtCommRemissDerNSENum.Text.ToString();
                //    advisorLOBVo.IdentifierTypeCode = "NSE";
                //    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                //}
                //if (identifierType == "BOTH BSE and NSE")
                //{
                //    advisorLOBVo.Identifier = txtCommRemissDerBSENum.Text.ToString();
                //    advisorLOBVo.IdentifierTypeCode = "BNE";
                //    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                //    advisorLOBVo.Identifier = txtCommRemissDerNSENum.Text.ToString();
                //    advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);
                //}
                btnIns.Enabled = false;
                Session.Remove("insuranceAgent1");
                Insurance.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnIns_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnPostal_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "PS";
            string category = "AGN";
            string segment = "";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = txtPostalOrgName.Text.ToString();
                advisorLOBVo.ValidityDate = DateTime.Parse(txtPostalAgencyExpiry.Text.ToString());
                advisorLOBVo.AgentNum = txtPostalAgentNum.Text.ToString();
                if (txtPostalTargetAccount.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAccount = float.Parse(txtPostalTargetAccount.Text.ToString().Trim());
                if (txtPostalTargetAmt.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAmount = double.Parse(txtPostalTargetAmt.Text.ToString().Trim());

                advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                btnPostal.Enabled = false;
                Session.Remove("postalSavingsAgent1");
                PostalSavings.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnPostal_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnRealEst_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "RE";
            string category = "AGN";
            string segment = "";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = txtRealEstOrgName.Text.ToString();
                advisorLOBVo.ValidityDate = DateTime.Parse(txtRealEstAgencyExpiry.Text.ToString());
                advisorLOBVo.AgentNum = txtRealEstAgentNum.Text.ToString();
                if (txtRealEstTargetAccount.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAccount = float.Parse(txtRealEstTargetAccount.Text.ToString().Trim());
                if (txtRealEstTargetAmt.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAmount = double.Parse(txtRealEstTargetAmt.Text.ToString().Trim());

                advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                btnRealEst.Enabled = false;
                Session.Remove("realEstateAgent1");
                RealEstate.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnRealEst_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnFixInc_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "FI";
            string category = "AGN";
            string segment = "";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = txtFixIncOrgName.Text.ToString();
                advisorLOBVo.ValidityDate = DateTime.Parse(txtFixIncAgencyExpiry.Text.ToString());
                advisorLOBVo.AgentNum = txtFixIncAgentNum.Text.ToString();
                if (txtFixIncTargetAccount.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAccount = float.Parse(txtFixIncTargetAccount.Text.ToString().Trim());
                if (txtFixIncTargetAmt.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAmount = double.Parse(txtFixIncTargetAmt.Text.ToString().Trim());

                advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                btnFixInc.Enabled = false;
                Session.Remove("fixedIncomeAgent1");
                FixedIncome.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnFixInc_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnLiab_Click(object sender, EventArgs e)
        {
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorLOBVo advisorLOBVo = new AdvisorLOBVo();
            AdvisorLOBBo advisorLOBBo = new AdvisorLOBBo();
            string assetClass = "DSP";
            string category = "AGN";
            string segment = "";
            int advisorId = advisorVo.advisorId;
            int userId = userVo.UserId;

            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            try
            {
                if (chkLiabCarLoan.Checked)
                    advisorLOBVo.AgentType = "CLA";
                else if (chkLiabPerLoan.Checked)
                    advisorLOBVo.AgentType = "PLA";
                else
                    advisorLOBVo.AgentType = "CPLA";

                advisorLOBVo.LOBClassificationCode = XMLBo.GetLOBClassification(path, assetClass, category, segment);
                advisorLOBVo.OrganizationName = txtLiabOrgName.Text.ToString();
                advisorLOBVo.ValidityDate = DateTime.Parse(txtLiabAgencyExpiry.Text.ToString());
                advisorLOBVo.AgentNum = txtLiabAgentNum.Text.ToString();
                if (txtLiabTargetAccount.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAccount = float.Parse(txtLiabTargetAccount.Text.ToString().Trim());
                if (txtLiabTargetAmt.Text.ToString().Trim() != string.Empty)
                    advisorLOBVo.TargetAmount = double.Parse(txtLiabTargetAmt.Text.ToString().Trim());

                advisorLOBBo.CreateAdvisorLOB(advisorLOBVo, advisorId, userId);

                btnLiab.Enabled = false;
                Session.Remove("liabilitiesAgent1");
                Liabilities.Visible = false;
                PageRedirect();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewLOB','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "LOB.ascx:btnLiab_Click()");


                object[] objects = new object[9];
                objects[0] = assetClass;
                objects[1] = advisorBo;
                objects[2] = advisorLOBBo;
                objects[3] = advisorLOBVo;
                objects[4] = path;
                objects[5] = advisorId;
                objects[6] = userId;
                objects[7] = segment;
                objects[8] = category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void chkLiabCarLoan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLiabCarLoan.Checked)
            {
                chkLiabPerLoan.Enabled = false;
                chkLiabBothLoan.Enabled = false;
            }
            else
            {
                chkLiabPerLoan.Enabled = true;
                chkLiabBothLoan.Enabled = true;
            }
        }

        protected void chkLiabPerLoan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLiabPerLoan.Checked)
            {
                chkLiabCarLoan.Enabled = false;
                chkLiabBothLoan.Enabled = false;
            }
            else
            {
                chkLiabCarLoan.Enabled = true;
                chkLiabBothLoan.Enabled = true;
            }
        }

        protected void chkLiabBothLoan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLiabBothLoan.Checked)
            {
                chkLiabCarLoan.Enabled = false;
                chkLiabPerLoan.Enabled = false;
            }
            else
            {
                chkLiabCarLoan.Enabled = true;
                chkLiabPerLoan.Enabled = true;
            }
        }

        protected void btnAddARNVariations_Click(object sender, EventArgs e)
        {
            trMFAddVariation.Visible = true;
            btnAddARNVariations.Enabled = false;
        }

        protected void btnAddVariant_Click(object sender, EventArgs e)
        {
            //System.Text.StringBuilder sb =new System.Text.StringBuilder();
            //sb.Append(";" + txtMFARNVariation.Text.ToString()); 
            txtMFARNCode.Text += ";" + txtMFARNVariation.Text.ToString().Trim();
            txtMFARNVariation.Text = String.Empty;
            trMFAddVariation.Visible = false;
            btnAddARNVariations.Enabled = true;
        }
    }
}