using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCommon;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using VoUser;
using BoCustomerPortfolio;
using System.Configuration;
using BoProductMaster;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using DaoReports;


namespace WealthERP.OnlineOrderManagement
{
    public partial class MFOrderSIPTransType : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        int CustomerId;
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();

        DataSet dsCustomerAssociates;
        DataTable dtCustomerAssociatesRaw;
        DataTable dtCustomerAssociates;
        DataRow drCustomerAssociates;
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        string path;
        DataTable dtgetfolioNo;
        DataTable dtModeOfHolding;
        DataTable dtGetAllSIPDataForOrder;
        DataTable dtGetAllSIPDataForOrderEdit;
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        ProductMFBo productMfBo = new ProductMFBo();
        OnlineMFOrderBo boOnlineOrder = new OnlineMFOrderBo();
        OnlineMFOrderVo onlineMFOrderVo = new OnlineMFOrderVo();
        string strAction;
        string[] AllSipDates;
        int orderIdForEdit;
        int customerIdforEdit;
        List<OnlineMFOrderVo> SIPDataForOrderEditList = new List<OnlineMFOrderVo>();
        DataTable dtFrequency;

        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            rmVo = (RMVo)Session["rmVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];




            if (!IsPostBack)
            {
                if (Request.QueryString["strAction"] != null && Request.QueryString["orderId"] != null && Request.QueryString["customerId"] != null)
                {
                    strAction = Request.QueryString["strAction"].ToString();
                    orderIdForEdit = Convert.ToInt32(Request.QueryString["orderId"].ToString());
                    customerIdforEdit = Convert.ToInt32(Request.QueryString["customerId"].ToString());
                }


                AmcBind();
                //LoadNominees();
                BindCategory();
                BindModeOfHolding();
                //BindNomineeAndJointHolders();

                if (strAction == "Edit")
                {
                    DataViewOnEdit();
                    btnSubmit.Text = "Modify";
                    onlineMFOrderVo.Action = "Edit";
                }
                else
                    btnSubmit.Text = "Submit";
            }
        }

        protected void DataViewOnEdit()
        {

            BindSIPDetailsForEdit();
            ddlAmc.Enabled = false;
            ddlCategory.Enabled = false;
            ddlScheme.Enabled = false;
            ddlFolio.Enabled = false;


        }

        protected void BindSIPDetailsForEdit()
        {
            SIPDataForOrderEditList = new List<OnlineMFOrderVo>();
            SIPDataForOrderEditList = commonLookupBo.GetAllSIPDataForOrderEdit(orderIdForEdit, customerIdforEdit);
            onlineMFOrderVo = (OnlineMFOrderVo)SIPDataForOrderEditList[0];

            BindEachControlForEditWithVO();



        }


        protected void BindEachControlForEditWithVO()
        {
            BindStartDates();
            BindFrequency();
            SetLatestNav();
            BindFolioNumber(Convert.ToInt32(onlineMFOrderVo.AssetGroup));

            SchemeBind(Convert.ToInt32(onlineMFOrderVo.AssetGroup), "ALL");
            ddlAmc.SelectedValue = onlineMFOrderVo.AssetGroup;
            if (!string.IsNullOrEmpty(onlineMFOrderVo.Category))
                ddlCategory.SelectedValue = onlineMFOrderVo.Category;
            ddlFolio.SelectedValue = onlineMFOrderVo.Folio;
            ddlScheme.SelectedValue = onlineMFOrderVo.SchemePlanCode.ToString();
            ddlFrequency.SelectedValue = onlineMFOrderVo.FrequencyCode;
            ddlDividendFreq.SelectedValue = onlineMFOrderVo.FrequencyCode;
            if (!string.IsNullOrEmpty(onlineMFOrderVo.DivOption))
                ddlDividendOption.SelectedValue = onlineMFOrderVo.DivOption;
            txtAmount.Text = onlineMFOrderVo.Amount.ToString();
            onlineMFOrderVo.StartDate.ToString("dd-MMM-yyyy");
            ddlStartDate.SelectedValue = onlineMFOrderVo.StartDate.ToString();
            lblEndDateDisplay.Text = onlineMFOrderVo.EndDate.ToString();
            BindddlTotalInstallments();
            ddlTotalInstallments.SelectedValue = onlineMFOrderVo.TotalInstallments.ToString();//for the time being take it as reedemed units

        }

        protected void ddlStartDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset dependent controls
            ddlTotalInstallments.SelectedIndex = 0;
        }

        protected void AmcBind()
        {
            DataTable dtAmc = new DataTable();
            dtAmc = commonLookupBo.GetProdAmc();
            if (dtAmc.Rows.Count > 0)
            {
                ddlAmc.DataSource = dtAmc;
                ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAmc.DataBind();
            }
            ddlAmc.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }
        public void ddlAmc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset dependent controls
            ddlFolio.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;

            if (ddlAmc.SelectedIndex == 0) return;

            BindFolioNumber(Convert.ToInt32(ddlAmc.SelectedValue));
            SchemeBind(Convert.ToInt32(ddlAmc.SelectedValue), ddlCategory.SelectedValue);
            DataTable dtAmc = commonLookupBo.GetProdAmc(int.Parse(ddlAmc.SelectedValue));
            lnkFactSheet.PostBackUrl = dtAmc.Rows[0]["PA_Url"].ToString();
            //BindNomineeAndJointHolders();
        }

        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset dependent controls
            ddlScheme.SelectedIndex = 0;

            if (ddlCategory.SelectedIndex == 0) return;

            SchemeBind(Convert.ToInt32(ddlAmc.SelectedValue), ddlCategory.SelectedValue);
        }

        private void BindCategory()
        {
            try
            {
                DataSet dsCategory = new DataSet();
                dsCategory = commonLookupBo.GetAllCategoryList();

                if (dsCategory.Tables[0].Rows.Count > 0)
                {
                    ddlCategory.DataSource = dsCategory.Tables[0];
                    ddlCategory.DataValueField = dsCategory.Tables[0].Columns["Category_Code"].ToString();
                    ddlCategory.DataTextField = dsCategory.Tables[0].Columns["Category_Name"].ToString();
                    ddlCategory.DataBind();
                }
                ddlCategory.Items.Insert(0, new ListItem("All", "ALL"));
                ddlCategory.Items.Insert(0, new ListItem("--SELECT--", "--SELECT--"));
                ddlCategory.SelectedIndex = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MFManualSingleTran.ascx:BindBranchDropDown()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void SchemeBind(int amccode, string category)
        {
            if (amccode <= 0) return;
            string paramCat = category.ToLower() == "all" ? null : category;

            DataTable dtScheme = new DataTable();

            dtScheme = commonLookupBo.GetAmcSipSchemeList(amccode, paramCat);
            ddlScheme.Items.Clear();
            if (dtScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtScheme;
                ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
            }
            ddlScheme.Items.Insert(0, new ListItem("--SELECT--", "0"));
            ddlScheme.SelectedIndex = 0;
        }

        protected void BindSIPUI()
        {
        }

        private void SaveOrderDetails()
        {
            onlineMFOrderVo.SchemePlanCode = int.Parse(ddlScheme.SelectedValue);
            onlineMFOrderVo.AccountId = Convert.ToInt32(ddlFolio.SelectedValue);
            onlineMFOrderVo.SystematicTypeCode = "SIP";
            onlineMFOrderVo.SystematicDate = 0;
            onlineMFOrderVo.Amount = int.Parse(txtAmount.Text);
            onlineMFOrderVo.SourceCode = "";
            onlineMFOrderVo.FrequencyCode = ddlFrequency.SelectedValue;
            onlineMFOrderVo.CustomerId = customerVo.CustomerId;
            onlineMFOrderVo.StartDate = DateTime.Parse(ddlStartDate.SelectedValue);
            onlineMFOrderVo.EndDate = DateTime.Parse(lblEndDateDisplay.Text);
            onlineMFOrderVo.SystematicDates = "";
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Submit")
            {
                onlineMFOrderVo.Action = "Insert";
                int retVal = commonLookupBo.IsRuleCorrect(float.Parse(txtAmount.Text), float.Parse(lblMinAmountrequiredDisplay.Text), float.Parse(txtAmount.Text), float.Parse(lblMutiplesThereAfterDisplay.Text), DateTime.Parse(lblCutOffTimeDisplay.Text));
                if (retVal != 0 && retVal != 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Rules defined were incorrect');", true);
                    return;
                }
                if (retVal == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "javascript:DeleteConfirmation();", true);
                }
                List<int> OrderIds = new List<int>();
                SaveOrderDetails();
                OrderIds = boOnlineOrder.CreateOrderMFSipDetails(onlineMFOrderVo, userVo.UserId);

                divOrderCompletionDetails.InnerHtml = "Order Submitted Successfully";
                divOrderCompletionDetails.Visible = true;

            }
            else
            {
                onlineMFOrderVo.Action = "Update";
                int retVal = commonLookupBo.IsRuleCorrect(float.Parse(txtAmount.Text), float.Parse(lblMinAmountrequiredDisplay.Text), float.Parse(txtAmount.Text), float.Parse(lblMutiplesThereAfterDisplay.Text), DateTime.Parse(lblCutOffTimeDisplay.Text));
                if (retVal != 0 && retVal != 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Rules defined were incorrect');", true);
                    return;
                }
                if (retVal == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "javascript:DeleteConfirmation();", true);
                }
                List<int> OrderIds = new List<int>();
                SaveOrderDetails();
                OrderIds = boOnlineOrder.CreateOrderMFSipDetails(onlineMFOrderVo, userVo.UserId);

                divOrderCompletionDetails.InnerHtml = "Order Updated Successfully";
                divOrderCompletionDetails.Visible = true;

            }

            FreezControlsAfterSubmit();
        }
        protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset dependent controls
            ddlFrequency.SelectedIndex = 0;

            if (ddlScheme.SelectedIndex == 0) return;

            BindSipUiOnSchemeSelection();
            BindStartDates();
            BindddlTotalInstallments();
            ShowHideControlsForDivAndGrowth();
            BindSIPDetailsONFrequencySelection(ddlScheme.SelectedValue, ddlFrequency.SelectedValue);
        }

        protected void ShowHideControlsForDivAndGrowth()
        {
            DataView dvFilterDivNGrowth = new DataView(dtGetAllSIPDataForOrder, "PASP_SchemePlanCode='" + ddlScheme.SelectedValue + "'", "PSLV_LookupValueCodeForSchemeOption", DataViewRowState.CurrentRows);

            dtGetAllSIPDataForOrder = dvFilterDivNGrowth.ToTable();
            if (dtGetAllSIPDataForOrder.Rows[0]["PSLV_LookupValueCodeForSchemeOption"].ToString() == "DV")
            {
                trDividendType.Visible = true;
                trDividendFrequency.Visible = true;
                trDividendOption.Visible = true;
            }
            else
            {
                trDividendType.Visible = false;
                trDividendFrequency.Visible = false;
                trDividendOption.Visible = false;
            }
        }

        protected void BindNomineeAndJointHolders()
        {
            MFReportsDao MFReportsDao = new MFReportsDao();
            DataSet dsNomineeAndJointHolders;
            dsNomineeAndJointHolders = MFReportsDao.GetARNNoAndJointHoldings(customerVo.CustomerId, 0, ddlFolio.SelectedItem.ToString());
            StringBuilder strbNominee = new StringBuilder();
            StringBuilder strbJointHolder = new StringBuilder();

            foreach (DataRow dr in dsNomineeAndJointHolders.Tables[1].Rows)
            {
                strbJointHolder.Append(dr["JointHolderName"].ToString() + ",");
                strbNominee.Append(dr["JointHolderName"].ToString() + ",");
            }

            lblNomineeDisplay.Text = strbNominee.ToString();
            lblHolderDisplay.Text = strbJointHolder.ToString();
        }
        protected void GetControlDetails(int scheme, string folio)
        {
            DataSet ds = new DataSet();

            ds = boOnlineOrder.GetControlDetails(scheme, ddlFolio.SelectedValue);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > -1)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["PSLV_LookupValue"].ToString()))
                    {
                        lblDividendType.Text = dr["PSLV_LookupValue"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["MinAmt"].ToString()))
                    {
                        txtMinAmtDisplay.Text = dr["MinAmt"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["MultiAmt"].ToString()))
                    {
                        lblMutiplesThereAfterDisplay.Text = dr["MultiAmt"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["CutOffTime"].ToString()))
                    {
                        lblCutOffTimeDisplay.Text = dr["CutOffTime"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dr["divFrequency"].ToString()))
                    {
                        lblFrequency.Text = dr["divFrequency"].ToString();
                    }
                }
            }
        }

        protected void BindStartDates()
        {
            ddlStartDate.Items.Clear();
            DateTime[] dtStartdates;
            if (strAction != "Edit")
            {

                dtStartdates = boOnlineOrder.GetSipStartDates(Convert.ToInt32(ddlScheme.SelectedValue), ddlFrequency.SelectedValue);
            }
            else
            {
                dtStartdates = boOnlineOrder.GetSipStartDates(Convert.ToInt32(onlineMFOrderVo.SchemePlanCode), onlineMFOrderVo.FrequencyCode);
            }

            foreach (DateTime d in dtStartdates)
            {
                ddlStartDate.Items.Add(new ListItem(d.ToString("dd-MMM-yyyy"), d.ToString("dd-MMM-yyyy")));
            }
        }

        protected void ddlFrequency_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset dpendent controls
            ddlStartDate.SelectedIndex = 0;
            ddlTotalInstallments.SelectedIndex = 0;

            if (ddlFrequency.SelectedIndex == 0) return;

            BindSIPDetailsONFrequencySelection(ddlScheme.SelectedValue, ddlFrequency.SelectedValue);

        }
        protected void BindSIPDetailsONFrequencySelection(string schemeId, string freq)
        {
            DataSet dsSipDetails = boOnlineOrder.GetSipDetails(int.Parse(schemeId), freq);


            if (dsSipDetails == null || dsSipDetails.Tables[0].Rows.Count == 0) return;
            DataRow dtSipDet = dsSipDetails.Tables[0].Rows[0];
            lblMinAmountrequiredDisplay.Text = Math.Round(Convert.ToDecimal(dtSipDet["PASPSD_MinAmount"].ToString()), 2).ToString();
            txtMinAmtDisplay.Text = Math.Round(Convert.ToDecimal(dtSipDet["PASPSD_MinAmount"].ToString()), 2).ToString();
            lblMutiplesThereAfterDisplay.Text = Math.Round(Convert.ToDecimal(dtSipDet["PASPSD_MultipleAmount"].ToString()), 2).ToString();
            lblCutOffTimeDisplay.Text = dtSipDet["PASPD_CutOffTime"].ToString();
            lblUnitHeldDisplay.Text = "0.00";
            ViewState["ExitLoad"] = dtSipDet["PASPD_ExitLoadRemark"].ToString();

        }
        protected void hidFolioNumber_ValueChanged(object sender, EventArgs e)
        {
        }

        protected string FormatFloat(float num)
        {
            string strFloat = "0.00";
            try
            {
                strFloat = num.ToString("0.00");
            }
            catch (Exception ex)
            {

            }
            return strFloat;
        }

        protected void SetLatestNav()
        {
            float latNav = 0;

            if (onlineMFOrderVo.Action != "Edit")
            {
                if ((!string.IsNullOrEmpty(ddlScheme.SelectedValue)) || ddlScheme.SelectedValue == "0")
                {
                    DataSet ds = commonLookupBo.GetLatestNav(int.Parse(ddlScheme.SelectedValue));

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        latNav = float.Parse(ds.Tables[0].Rows[0][1].ToString());
                        string strDateForNAV = Convert.ToDateTime(ds.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy");
                        lblNavDisplay.Text = latNav + " " + "As On " + strDateForNAV;
                    }


                }
            }
            else
            {

                DataSet ds = commonLookupBo.GetLatestNav(int.Parse(onlineMFOrderVo.SchemePlanCode.ToString()));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    latNav = float.Parse(ds.Tables[0].Rows[0][1].ToString());
                    string strDateForNAV = Convert.ToDateTime(ds.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy");
                    lblNavDisplay.Text = latNav + " " + "As On " + strDateForNAV;
                }


            }
            //lblNavDisplay.Text = FormatFloat(latNav);// +' ' + dtGetAllSIPDataForOrder.Rows[0]["PSP_Date"].ToString();
        }

        protected void SetOptionsList()
        {

        }

        protected void BindSipUiOnSchemeSelection()
        {
            dtGetAllSIPDataForOrder = commonLookupBo.GetAllSIPDataForOrder(Convert.ToInt32(ddlScheme.SelectedValue));

            SetLatestNav();
            BindFrequency();
            BindAllControlsWithSIPData();
            BindFolioNumber(int.Parse(ddlAmc.SelectedValue));
            BindModeOfHolding();
        }

        protected void BindFrequency()
        {
            ddlFrequency.Items.Clear();
            if (dtGetAllSIPDataForOrder == null) return;

            foreach (DataRow row in dtGetAllSIPDataForOrder.Rows) {
                if (row["PASP_SchemePlanCode"].ToString() == ddlScheme.SelectedValue.ToString()) {
                    ddlFrequency.Items.Add(new ListItem(row["XF_Frequency"].ToString(), row["XF_FrequencyCode"].ToString()));
                }
            }
            ddlFrequency.Items.Insert(0, new ListItem("--SELECT--"));
            ddlFrequency.SelectedIndex = 0;
        }

        private void BindModeOfHolding() {
        }


        public void BindAllControlsWithSIPData()
        {
            if (dtGetAllSIPDataForOrder.Rows.Count > 0)
            {
                txtMinAmtDisplay.Text = Math.Round(Convert.ToDecimal(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MinAmount"].ToString()), 2).ToString();
                lblMutiplesThereAfterDisplay.Text = Math.Round(Convert.ToDecimal(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MultipleAmount"].ToString()), 2).ToString();
                lblCutOffTimeDisplay.Text = dtGetAllSIPDataForOrder.Rows[0]["PASPD_CutOffTime"].ToString();
                BindFrequency();
                ddlFrequency.SelectedValue = dtGetAllSIPDataForOrder.Rows[0]["XF_FrequencyCode"].ToString();
                ShowSipDates(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_StatingDates"].ToString());
                lblEndDateDisplay.Text = string.Empty;
            }

        }


        protected void ShowSipDates(string DelimitedDateVals)
        {
            //string DelimitedDateVals = dtGetAllSIPDataForOrder.Row
            AllSipDates = DelimitedDateVals.Split(';');

            //if (tdSipDates == null) return;
            int i = 0;
            foreach (string date in AllSipDates)
            {
                if (string.IsNullOrEmpty(date)) continue;
                CheckBox chk = new CheckBox();
                chk.ID = "chk_Sip_" + i;
                chk.Text = date;
                chk.Visible = true;
                i++;
            }
        }

        private void BindFolioNumber(int amcCode)
        {
            ddlFolio.Items.Clear();
            DataTable dtScheme = new DataTable();
            DataTable dtgetfolioNo;
            try
            {
                if (strAction != "Edit")
                    dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(Convert.ToInt32(ddlAmc.SelectedValue), customerVo.CustomerId);
                else
                    dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(Convert.ToInt32(onlineMFOrderVo.AssetGroup), customerVo.CustomerId);

                if (dtgetfolioNo.Rows.Count > 0)
                {
                    ddlFolio.DataSource = dtgetfolioNo;
                    ddlFolio.DataTextField = dtgetfolioNo.Columns["CMFA_FolioNum"].ToString();
                    ddlFolio.DataValueField = dtgetfolioNo.Columns["CMFA_AccountId"].ToString();
                    ddlFolio.DataBind();
                }
                ddlFolio.Items.Insert(0, new ListItem("--SELECT--", "--SELECT--"));
                ddlFolio.Items.Insert(1, new ListItem("New", "1"));
                //ddlFolio.SelectedIndex = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }

        protected void LoadNominees()
        {
            try
            {
                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];

                dtCustomerAssociatesRaw.Columns.Add("MemberCustomerId");
                //dtCustomerAssociates.Columns.Add("AssociationId");
                dtCustomerAssociatesRaw.Columns.Add("Name");
                //dtCustomerAssociates.Columns.Add("Relationship");


                foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
                {
                    drCustomerAssociates = dtCustomerAssociates.NewRow();
                    drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                    //drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                    drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                    //drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                    dtCustomerAssociates.Rows.Add(drCustomerAssociates);
                }

                if (dtCustomerAssociates != null)
                {

                }
                //else
                //{
                //    trNominees.Visible = false;
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
                FunctionInfo.Add("Method", "PortfolioGeneralInsuranceAccountAdd.ascx:LoadNominees()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void lnkFactSheet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lnkFactSheet.PostBackUrl))
                Response.Write(@"<script language='javascript'>alert('The URL is not valid');</script>");
        }

        protected void BindddlTotalInstallments()
        {
            ddlTotalInstallments.Items.Clear();
            int minDues;
            int maxDues;
            if (strAction != "Edit")
            {
                minDues = Convert.ToInt32(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MinDues"]);
                maxDues = Convert.ToInt32(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MaxDues"]);

            }
            else
            {
                minDues = Convert.ToInt32(onlineMFOrderVo.MinDues);
                maxDues = Convert.ToInt32(onlineMFOrderVo.MaxDues);

            }
            StringBuilder strTotalInstallments = new StringBuilder();

            for (int i = minDues; i <= maxDues; i++)
            {
                strTotalInstallments.Append(i + "~");
            }
            string str = strTotalInstallments.ToString();

            string[] strSplit = str.Split('~');

            foreach (string s in strSplit)
            {
                if (string.IsNullOrEmpty(s.Trim())) continue;
                ddlTotalInstallments.Items.Add(new ListItem(s.ToString()));
            }
            ddlTotalInstallments.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }

        protected void ddlTotalInstallments_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dtEndDate;
            dtEndDate = boOnlineOrder.GetSipEndDate(Convert.ToDateTime(ddlStartDate.SelectedValue), ddlFrequency.SelectedValue, Convert.ToInt32(ddlTotalInstallments.SelectedValue));
            lblEndDateDisplay.Text = dtEndDate.ToString("dd-MMM-yyyy");
        }

        protected void FreezControlsAfterSubmit()
        {
            ddlAmc.Enabled = false;
            ddlCategory.Enabled = false;
            ddlScheme.Enabled = false;
            ddlFolio.Enabled = false;
            ddlFrequency.Enabled = false;
            ddlStartDate.Enabled = false;
            txtAmount.Enabled = false;
            btnSubmit.Enabled = false;
            ddlTotalInstallments.Enabled = false;

        }

        protected void ddlFolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFolio.SelectedIndex < 1) return;
            if (ddlFolio.SelectedItem.ToString() != "New")
            {
                BindNomineeAndJointHolders();
                GetControlDetails(Convert.ToInt32(ddlScheme.SelectedValue), ddlFolio.SelectedValue);
                trNominee.Visible = true;
                trJointHolder.Visible = true;
                ddlFrequency.SelectedIndex = 0;
            }
            else
            {
                trNominee.Visible = false;
                trJointHolder.Visible = false;
            }
        }

        protected void lnkExitLoad_Click(object sender, EventArgs e)
        {
            lblExitLoad.Text = ViewState["ExitLoad"].ToString();
        }
    }
}
