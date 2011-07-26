using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoCustomerProfiling;
using BoProductMaster;
using WealthERP.Base;
using VoUser;
using BoCommon;
using BoUploads;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Numeric;
using Telerik.Web.UI;


namespace WealthERP.Advisor
{
    public partial class AdviserRMMFSystematicMIS : System.Web.UI.UserControl
    {
        SystematicSetupVo systematicSetupVo;
        SystematicSetupBo systematicSetupBo = new SystematicSetupBo();
        AdvisorVo advisorVo = new AdvisorVo();
        DataSet dsSystematicMIS = new DataSet();
        DataTable dtSystematicTransactionType;
        DataTable dtAMC;
        DataTable dtCategory;
        DataTable dtScheme;
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        string strAmcCode = null;
        String userType;
        int advisorId=0;
        int customerId=0;
        double sumTotal;
        double totalAmount = 0;
        int rmId = 0;
        int bmID=0;
        RMVo rmVo = new RMVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();

        DataSet dsBindGvSystematicMIS = new DataSet();
        DataTable dtSystematicMIS1 = new DataTable();
        DataTable dtSystematicMIS2 = new DataTable();
        DataTable dtSystematicMIS3 = new DataTable();
        DataTable dtSystematicMIS4 = new DataTable();
        
        DateTime startDate = new DateTime();
        DateTime endDate = new DateTime();
        string frequency = "";
        int systematicDate = 0;
        int monthCode = 0;

        int all;
        string customerType = string.Empty;

        private decimal totalSIPAmount = 0;
        private decimal totalSWPAmount = 0;
        private int totalNoOfSIP = 0;
        private int totalNoOfFreshSIP = 0;
        private int totalNoOfSWP = 0;
        private int totalNoOfFreshSWP = 0;

       
        string path;    
        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            advisorId = advisorVo.advisorId;
            int RMId = rmVo.RMId;
            customerId = customerVo.CustomerId;
            rmId = rmVo.RMId;
            bmID=rmVo.RMId;
            

            hdnRecordCount.Value = "1";
            //GetPageCount();
          
            if (!IsPostBack)
            {
                ddlSelectCutomer.Visible = false;
                lblSelectTypeOfCustomer.Visible = false;
                txtIndividualCustomer.Visible = false;
                //trPager.Visible = false;
                dsSystematicMIS = systematicSetupBo.GetAllDropdownBinding(strAmcCode);
                BindDropDowns(path);
                BindAMCDropDown(dsSystematicMIS.Tables[0]);
                SchemeDropdown(dsSystematicMIS.Tables[1]);
                CategoryDropdown(dsSystematicMIS.Tables[2]);
                Session["ButtonGo"] = null;
                rquiredFieldValidatorIndivudialCustomer.Visible = false;
                txtIndividualCustomer.Visible = false;

                if (userType == "advisor")
                {
                    BindBranchDropDown();
                    BindRMDropDown();
                }
                else if (userType == "rm")
                {
                    trBranchRM.Visible = false;

                }
                if (userType == "bm")
                {
                    BindBranchForBMDropDown();
                    BindRMforBranchDropdown(0, bmID);
                }
             }
            if (Session["ButtonGo"] != null)
                CallAllGridBindingFunctions();
    
                  
        }
        /// <summary>
        /// Bind All the Dropdowns 
        /// </summary>
        /// <param name="path"></param>
        private void BindDropDowns(string path)
        {
            userVo = (UserVo)Session["userVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            //Binding  Systematic Type
            dtSystematicTransactionType = XMLBo.GetSystematicTransactionType(path);
            ddlSystematicType.DataSource = dtSystematicTransactionType;
            ddlSystematicType.DataTextField = "SystemationTypeCode";
            ddlSystematicType.DataValueField = "SystemationTypeCode";
            ddlSystematicType.DataBind();
            ddlSystematicType.Items.Insert(0, "All");
            ddlSystematicType.Items.Remove("STP");
        }
          

        /* Binding AMC DropDown */
        private void BindAMCDropDown(DataTable dtAMC)
        {
            
            try
            {
                if (dtAMC != null)
                {                   
                    ddlAMC.DataSource = dtAMC;
                    ddlAMC.DataValueField = dtAMC.Columns["PA_AMCCode"].ToString();
                    ddlAMC.DataTextField = dtAMC.Columns["PA_AMCName"].ToString();
                    ddlAMC.DataBind();
                }
                ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        /* Binding Scheme Drop Down */
        private void SchemeDropdown(DataTable dtScheme)
        {
            try
            {

                if (dtScheme != null)
                {
                    ddlScheme.DataSource = dtScheme;
                    ddlScheme.DataValueField = dtScheme.Columns["PA_AMCCode"].ToString();
                    ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                    ddlScheme.DataBind();
                }
                ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "Select"));

            }
            catch(BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        /* Binding  Category Dropdown*/
        private void CategoryDropdown(DataTable dtCategory)
        {
            try
            {

                if (dtCategory != null)
                {
                    //dtCategory = dsSystematicMIS.Tables[2];
                    ddlCategory.DataSource = dtCategory;
                    ddlCategory.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                    ddlCategory.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                    ddlCategory.DataBind();
                }
                ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        /* Binding Branch DropDown*/
        private void BindBranchDropDown()
        {
            
            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null)
                {
                    ddlBranch.DataSource = ds;
                    ddlBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindRMDropDown()
        {
            try
            {
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
                if (dt.Rows.Count > 0)
                {
                    ddlRM.DataSource = dt;
                    ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
                    ddlRM.DataTextField = dt.Columns["RMName"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "2"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindRMDropDown()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlAMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMC.SelectedIndex != 0)
            {
                strAmcCode = ddlAMC.SelectedValue.ToString();
                dsSystematicMIS = systematicSetupBo.GetAllDropdownBinding(strAmcCode);
                SchemeDropdown(dsSystematicMIS.Tables[1]);
            }
            else
            {
                ddlAMC.SelectedIndex = 0;
            }
        }

        protected void rdoAllCustomer_CheckedChanged(object sender, EventArgs e)
        {
            ddlSelectCutomer.Visible = false;
            lblSelectTypeOfCustomer.Visible = false;
            txtIndividualCustomer.Visible = false;
            rquiredFieldValidatorIndivudialCustomer.Visible = false;
        }

        protected void rdoPickCustomer_CheckedChanged(object sender, EventArgs e)
        {
            ddlSelectCutomer.Visible = true;
            lblSelectTypeOfCustomer.Visible = true;
            txtIndividualCustomer.Visible = true;
            rquiredFieldValidatorIndivudialCustomer.Visible = true;
        }
        /* Customer search for Group ang Individual*/
      
        private void BindBranchForBMDropDown()
        {
            try
            {
                DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(0, bmID, 0);
                if (ds != null)
                {
                    ddlBranch.DataSource = ds.Tables[1]; ;
                    ddlBranch.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            Session["ButtonGo"] = "buttonClicked";
           
            CallAllGridBindingFunctions();
        }
        protected void CallAllGridBindingFunctions()
        {
            SetParameter();
            GetDataFromDB();
            if (dsBindGvSystematicMIS.Tables.Count != 0)
            {
                tblMessage.Visible = false;
                ErrorMessage.Visible = false;
                BindgvSystematicMIS();
                BindgvCalenderDetailView();
                BindreptCalenderSummaryView();
            }
            else
            {
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "No Records Found...!";
            }
        }

        private void GetDataFromDB()
        {
            dsBindGvSystematicMIS = systematicSetupBo.GetAllSystematicMISData(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnCustomerId.Value), int.Parse(hdnbranchheadId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnAll.Value), hdnCategory.Value, hdnSystematicType.Value, hdnamcCode.Value, hdnschemeCade.Value, hdnstartdate.Value, hdnendDate.Value, DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnTodate.Value));
        }

        private void SetParameter()
        {
            if ((rdoAllCustomer.Checked == true) && (userType == "advisor"))
            {
                hdnCustomerId.Value = "0";
                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnAll.Value = "0";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex==0))
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if(ddlBranch.SelectedIndex==0 && ddlRM.SelectedIndex!=0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRM.SelectedValue; ;
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value="3";
                }

            }
            else if (rdoAllCustomer.Checked == true && userType == "rm")
            {
                hdnrmId.Value = rmVo.RMId.ToString();
                hdnAll.Value = "0";

            }
            else if (rdoAllCustomer.Checked == true && userType == "bm")
            {
                hdnCustomerId.Value = "0";
                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {

                    hdnbranchheadId.Value = bmID.ToString();
                    hdnAll.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnbranchheadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnbranchheadId.Value = bmID.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRM.SelectedValue; ;
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnbranchheadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "3";
                }
            }
                    

            if (rdoPickCustomer.Checked == true && userType == "advisor")
            {

                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnAll.Value = "4";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                else  if (ddlBranch.SelectedIndex !=0 && ddlRM.SelectedIndex==0)
                {
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = "0";
                    hdnAll.Value = "5";

                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "6";

                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "7";
                }
            }
             else if (rdoPickCustomer.Checked == true && userType == "rm")
            {
               hdnAll.Value = "1";
            }


            else if (rdoPickCustomer.Checked == true && userType == "bm")
            {

                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnAll.Value = "4";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = "0";
                    hdnAll.Value = "5";

                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {

                    hdnbranchId.Value = "0";
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "6";

                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {

                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "7";
                }
            }
            if (ddlDateFilter.SelectedIndex == 0)
                hdnstartdate.Value = ddlDateFilter.SelectedValue;
            else
                hdnendDate.Value = "";

            if (ddlAMC.SelectedIndex != 0)
                hdnamcCode.Value = ddlAMC.SelectedValue;
            else
                hdnamcCode.Value = "";


            if (ddlScheme.SelectedIndex != 0)
                hdnschemeCade.Value = ddlScheme.SelectedValue;
            else
                hdnschemeCade.Value = "";


            if (ddlCategory.SelectedIndex != 0)
                hdnCategory.Value = ddlCategory.SelectedValue;
            else
                hdnCategory.Value = "";


            if (ddlSystematicType.SelectedIndex != 0)
                hdnSystematicType.Value = ddlSystematicType.SelectedValue;
            else
                hdnSystematicType.Value = "";


            if (txtFrom.Text != "")
            {
                hdnFromDate.Value = DateTime.Parse(txtFrom.Text).ToString();
                ViewState["txtFromDate"] = DateTime.Parse(txtFrom.Text).ToString();
            }
            else if (ViewState["txtFromDate"].ToString() != null)
            {
                hdnFromDate.Value = DateTime.Parse(ViewState["txtFromDate"].ToString()).ToString();
            }
            else
                hdnFromDate.Value = DateTime.MinValue.ToString();



            if (txtTo.Text != "")
            {
                hdnTodate.Value = DateTime.Parse(txtTo.Text).ToString();
                ViewState["txtToDate"] = DateTime.Parse(txtTo.Text).ToString();
            }
            else if (ViewState["txtToDate"].ToString() != null)
            {
                hdnTodate.Value = DateTime.Parse(ViewState["txtToDate"].ToString()).ToString();
            }
            else
                hdnTodate.Value = DateTime.MinValue.ToString();


            if (hdnbranchheadId.Value == "")
                hdnbranchheadId.Value = "0";

            if (hdnbranchId.Value == "")
                hdnbranchId.Value = "0";

            if (hdnCustomerId.Value == "")
                hdnCustomerId.Value = "0";

            if (hdnadviserId.Value == "")
                hdnadviserId.Value = "0";

            if (hdnrmId.Value == "")
                hdnrmId.Value = "0";  


  
        }

        private  void BindgvCalenderDetailView()
        {
          
            try
            {
                DataTable dtCalenderDetail = new DataTable();
                dtCalenderDetail.Columns.Add("CustomerName");
                dtCalenderDetail.Columns.Add("Type");
                dtCalenderDetail.Columns.Add("Scheme");
                dtCalenderDetail.Columns.Add("Frequency");
                dtCalenderDetail.Columns.Add("NextSystematicDate");
                dtCalenderDetail.Columns.Add("Amount",typeof(Decimal));

                DataRow drCalenderDetail;


                foreach (DataRow dr in dtSystematicMIS2.Rows)
                {

                    drCalenderDetail = dtCalenderDetail.NewRow();
                    drCalenderDetail["CustomerName"] = dr["CustomerName"].ToString();
                    drCalenderDetail["Type"] = dr["TypeCode"].ToString();
                    drCalenderDetail["Scheme"] = dr["SchemeName"].ToString();
                    drCalenderDetail["Frequency"] = dr["Frequency"].ToString();
                    startDate = Convert.ToDateTime(dr["StartDate"].ToString());
                    endDate = Convert.ToDateTime(dr["EndDate"].ToString());
                    frequency = dr["Frequency"].ToString();
                    systematicDate = Convert.ToInt32(dr["SystematicDate"].ToString());
                    DateTime nextSystematicDate = GetNextSystematicDate(startDate, endDate, frequency, systematicDate);
                    drCalenderDetail["NextSystematicDate"] = nextSystematicDate.ToShortDateString();
                    drCalenderDetail["Amount"] = decimal.Parse(dr["Amount"].ToString());

                    dtCalenderDetail.Rows.Add(drCalenderDetail);
                 
                   }
                gvCalenderDetailView.DataSource = dtCalenderDetail;
                gvCalenderDetailView.DataBind();

                if (dtCalenderDetail.Rows.Count > 0)
                {
                    gvCalenderDetailView.Visible = true;
                    tblMessage.Visible = false;
                    ErrorMessage.Visible = false;
                  }
                else
                {
                    gvCalenderDetailView.Visible = false;
                    tblMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    ErrorMessage.InnerText = "No Records Found...!";
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

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindgvCalenderDetailView()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void gvCalenderDetailView_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            DateTime nextSystematicDate = GetNextSystematicDate(startDate, endDate, frequency, systematicDate);
            Label lblSysNxt = (Label)e.Item.FindControl("lblNextSystematicDate");
            //lblSysNxt.Text = dtCalenderDetail.ToShortDateString();
        }

        private DateTime GetNextSystematicDate(DateTime startDate, DateTime endDate, string frequency, int systematicDate)
        {

            DateTime nextSystematicDate = new DateTime();
            nextSystematicDate = new DateTime(startDate.Year, startDate.Month, 1);
            switch (frequency)
            {
               case "Daily":
                    nextSystematicDate = nextSystematicDate.AddDays(1);
                    break;
                case "FortNightly":
                    nextSystematicDate = nextSystematicDate.AddDays(15);
                    break;
                case "Weekly":
                    nextSystematicDate = nextSystematicDate.AddDays(7);
                    break;
                case  "Monthly":
                    nextSystematicDate = nextSystematicDate.AddMonths(1);
                    break;
                case "Quarterly":
                    nextSystematicDate = nextSystematicDate.AddMonths(4);
                    break;
                case "HalfYearly":
                    nextSystematicDate = nextSystematicDate.AddMonths(6);
                    break;
                case "Yearly":
                    nextSystematicDate = nextSystematicDate.AddYears(1);
                    break;
            }

           
            nextSystematicDate = nextSystematicDate.AddDays(systematicDate);
            return nextSystematicDate;

           }


        private void BindgvSystematicMIS()
        {
            try
            {

             
               
                
                dtSystematicMIS1 = dsBindGvSystematicMIS.Tables[0];
                dtSystematicMIS2 = dsBindGvSystematicMIS.Tables[1];
                dtSystematicMIS3 = dsBindGvSystematicMIS.Tables[2];
               // dtSystematicMIS4 = dsBindGvSystematicMIS.Tables[3];

                DataTable dtSystematicDetails = new DataTable();
                dtSystematicDetails.Columns.Add("CustomerName");
                dtSystematicDetails.Columns.Add("SystematicTransactionType");
                dtSystematicDetails.Columns.Add("AMCname");
                dtSystematicDetails.Columns.Add("SchemePlaneName");
                dtSystematicDetails.Columns.Add("FolioNumber");
                dtSystematicDetails.Columns.Add("StartDate");
                dtSystematicDetails.Columns.Add("EndDate");
                dtSystematicDetails.Columns.Add("Frequency");
                dtSystematicDetails.Columns.Add("Amount",typeof(Decimal));

                DataRow drSystematicDetails;
                foreach (DataRow dr in dtSystematicMIS1.Rows)
                {
                    drSystematicDetails =  dtSystematicDetails.NewRow();
                    drSystematicDetails["CustomerName"] = dr["CustomerName"].ToString();
                    drSystematicDetails["SystematicTransactionType"] = dr["TypeCode"].ToString();
                    drSystematicDetails["AMCname"] = dr["AMCName"].ToString();
                    drSystematicDetails["SchemePlaneName"] = dr["SchemeName"].ToString();
                    drSystematicDetails["FolioNumber"] = dr["FolioNumber"].ToString();
                    drSystematicDetails["StartDate"] = DateTime.Parse(dr["StartDate"].ToString()).ToShortDateString();
                    drSystematicDetails["EndDate"] = DateTime.Parse(dr["EndDate"].ToString()).ToShortDateString();
                    drSystematicDetails["Frequency"] = dr["Frequency"].ToString();
                    drSystematicDetails["Amount"] = decimal.Parse(dr["Amount"].ToString());

                    dtSystematicDetails.Rows.Add(drSystematicDetails);
                }
                gvSystematicMIS.DataSource = dtSystematicDetails;
                gvSystematicMIS.DataBind();

                if (dtSystematicDetails.Rows.Count > 0)
                {
                   
                    
                    gvSystematicMIS.Visible = true;
                    tblMessage.Visible = false;
                    ErrorMessage.Visible = false;
                    //trPager.Visible = true;
                 }
              
                else
                {
                    gvSystematicMIS.Visible = false;
                    tblMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    ErrorMessage.InnerText = "No Records Found...!";
                    //trPager.Visible = false;
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

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindgvSystematicMIS()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        {

            try
            {

                DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
                if (ds != null)
                {
                    ddlRM.DataSource = ds.Tables[0]; ;
                    ddlRM.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                    ddlRM.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserEQMIS.ascx:BindRMforBranchDropdown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }



        private void BindreptCalenderSummaryView()
        {
            totalSIPAmount = 0;
            totalSWPAmount = 0;
            totalNoOfSWP = 0;
            totalNoOfSIP = 0;
            totalNoOfFreshSIP = 0;
            totalNoOfFreshSWP = 0;
            try
            {
                DataTable dtCalenderSymmary = new DataTable();
                dtCalenderSymmary.Columns.Add("Year");
                dtCalenderSymmary.Columns.Add("Month");
                dtCalenderSymmary.Columns.Add("FinalMonth");
                dtCalenderSymmary.Columns.Add("SIPAmount", typeof(Decimal));
                dtCalenderSymmary.Columns.Add("NoOfSIP", typeof(Int16));
                dtCalenderSymmary.Columns.Add("NoOfFreshSIP", typeof(Int16));
                dtCalenderSymmary.Columns.Add("SWPDate");
                dtCalenderSymmary.Columns.Add("SWPAmount", typeof(Decimal));
                dtCalenderSymmary.Columns.Add("NoOfSWP", typeof(Int16));
                dtCalenderSymmary.Columns.Add("NoOfFreshSWP", typeof(Int16));
                DataRow drCalenderSummary;


                foreach (DataRow dr in dtSystematicMIS3.Rows)
                {
                    drCalenderSummary = dtCalenderSymmary.NewRow();
                    if (dr["TypeCode"].ToString() == "SIP")
                    {
                        drCalenderSummary["Year"] = DateTime.Parse(dr["SIPDate"].ToString()).Year;
                        drCalenderSummary["Month"] = DateTime.Parse(dr["SIPDate"].ToString()).Month;
                        monthCode = Convert.ToInt32(drCalenderSummary["Month"]);
                        String month = GetMonth(monthCode);
                        drCalenderSummary["FinalMonth"] = month;
                        drCalenderSummary["SIPAmount"] = Decimal.Parse(dr["SIPAmount"].ToString());
                        totalSIPAmount = totalSIPAmount + Decimal.Parse(dr["SIPAmount"].ToString());
                        drCalenderSummary["NoOfSIP"] = int.Parse(dr["NoOfSIP"].ToString());
                        totalNoOfSIP = totalNoOfSIP + int.Parse(dr["NoOfSIP"].ToString());

                        if (!string.IsNullOrEmpty(dr["FreshSIP"].ToString()))
                        {
                            drCalenderSummary["NoOfFreshSIP"] = int.Parse(dr["FreshSIP"].ToString());
                            totalNoOfFreshSIP = totalNoOfFreshSIP + int.Parse(dr["FreshSIP"].ToString());
                        }
                        else
                            drCalenderSummary["NoOfFreshSIP"] = 0;

                        drCalenderSummary["SWPAmount"] = 0;
                        drCalenderSummary["NoOfSWP"] = 0;
                        drCalenderSummary["NoOfFreshSWP"] = 0;
                    }
                    else if (dr["TypeCode"].ToString() == "SWP")
                    {
                        drCalenderSummary["Year"] = DateTime.Parse(dr["SIPDate"].ToString()).Year;
                        drCalenderSummary["Month"] = DateTime.Parse(dr["SIPDate"].ToString()).Month;
                        monthCode = Convert.ToInt32(drCalenderSummary["Month"]);
                        String month = GetMonth(monthCode);
                        drCalenderSummary["FinalMonth"] = month;
                        drCalenderSummary["SIPAmount"] = 0;
                        drCalenderSummary["NoOfSIP"] = 0;
                        drCalenderSummary["NoOfFreshSIP"] = 0;
                        drCalenderSummary["SWPAmount"] = Decimal.Parse(dr["SIPAmount"].ToString());
                        totalSWPAmount = totalSWPAmount + Decimal.Parse(dr["SIPAmount"].ToString());
                        drCalenderSummary["NoOfSWP"] = int.Parse(dr["NoOfSIP"].ToString());
                        totalNoOfSWP = totalNoOfSWP + int.Parse(dr["NoOfSIP"].ToString());

                        if (!string.IsNullOrEmpty(dr["FreshSIP"].ToString()))
                        {
                            drCalenderSummary["NoOfFreshSWP"] = int.Parse(dr["FreshSIP"].ToString());
                            totalNoOfFreshSWP = totalNoOfFreshSWP + int.Parse(dr["FreshSIP"].ToString());
                        }
                        else
                            drCalenderSummary["NoOfFreshSWP"] = 0;


                    }
                    dtCalenderSymmary.Rows.Add(drCalenderSummary);

                }

                //dtSystematicMIS3.Merge(dtSystematicMIS4);
                GridGroupByExpression expression1 = GridGroupByExpression.Parse("Year [year] Group By Year");
                //this.CustomizeExpression(expression1);
                this.reptCalenderSummaryView.MasterTableView.GroupByExpressions.Add(expression1);
                reptCalenderSummaryView.DataSource = dtCalenderSymmary;
                reptCalenderSummaryView.DataBind();
                if (dtCalenderSymmary.Rows.Count > 0)
                {
                    reptCalenderSummaryView.Visible = true;
                    tblMessage.Visible = false;
                    ErrorMessage.Visible = false;
                    //trPager.Visible = true;
                }

                else
                {
                    reptCalenderSummaryView.Visible = false;
                    tblMessage.Visible = true;
                    ErrorMessage.Visible = true;
                    ErrorMessage.InnerText = "No Records Found...!";
                    //trPager.Visible = false;
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

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindgvSystematicMIS()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private String GetMonth(int monthCode)
        {
            String finalMonth = "";
            switch (monthCode)
            {
                case 1:
                    finalMonth = "January";
                    break;
                case 2:
                    finalMonth = "February";
                    break;
                case 3:
                    finalMonth = "March";
                    break;
                case 4:
                    finalMonth = "April";
                    break;
                case 5:
                    finalMonth = "May";
                    break;
                case 6:
                    finalMonth = "June";
                    break;
                case 7:
                    finalMonth = "July";
                    break;
                case 8:
                    finalMonth = "August";
                    break;
                case 9:
                    finalMonth = "September";
                    break;
                case 10:
                    finalMonth = "October";
                    break;
                case 11:
                    finalMonth = "November";
                    break;
                case 12:
                    finalMonth = "December";
                    break;

            }
            return finalMonth;
        }

        protected void reptCalenderSummaryView_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFooterItem)
            {
                e.Item.Cells[3].Text = "Total :";

                e.Item.Cells[5].Text = double.Parse(totalSIPAmount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Item.Cells[5].Attributes.Add("align", "Right");

                e.Item.Cells[6].Text = int.Parse(totalNoOfSIP.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Item.Cells[6].Attributes.Add("align", "Right");

                e.Item.Cells[7].Text = int.Parse(totalNoOfFreshSIP.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Item.Cells[7].Attributes.Add("align", "Right");

                e.Item.Cells[8].Text = double.Parse(totalSWPAmount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Item.Cells[8].Attributes.Add("align", "Right");

                e.Item.Cells[9].Text = int.Parse(totalNoOfSWP.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Item.Cells[9].Attributes.Add("align", "Right");

                e.Item.Cells[10].Text = int.Parse(totalNoOfFreshSWP.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Item.Cells[10].Attributes.Add("align", "Right");

            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0);
            }
        }

        protected void ddlSelectCutomer_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlSelectCutomer.SelectedItem.Value == "Group Head")
            {
                customerType = "GROUP";
                if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllGroupCustomers";
                    }
                    else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMGroupCustomers";
                    }
                }

                else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetBMParentCustomerNames";
                    }
                    if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllGroupCustomers";
                    }
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                    }
                    if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMGroupCustomers";
                    }
                }
            }
            else if (ddlSelectCutomer.SelectedItem.Value == "Individual")
            {
                txtIndividualCustomer.Visible = true;
                customerType = "IND";

                //rquiredFieldValidatorIndivudialCustomer.Visible = true;
                if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";

                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {

                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllIndividualCustomers";
                    }
                    else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMIndividualCustomers";
                    }
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                {
                    if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllIndividualCustomers";
                    }
                    else if ((ddlBranch.SelectedIndex == 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                    }
                    else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex != 0))
                    {
                        txtIndividualCustomer_autoCompleteExtender.ContextKey = ddlBranch.SelectedValue + '~' + ddlRM.SelectedValue;
                        txtIndividualCustomer_autoCompleteExtender.ServiceMethod = "GetAllBranchAndRMIndividualCustomers";
                    }
                }
            }
        }

       

 
    }
}