﻿using System;
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
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        ProductMFBo productMfBo = new ProductMFBo();
        OnlineMFOrderBo boOnlineOrder = new OnlineMFOrderBo();
        OnlineMFOrderVo onlineMFOrderVo = new OnlineMFOrderVo();

        string[] AllSipDates;


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
                AmcBind();
                //LoadNominees();
                BindCategory();
                BindModeOfHolding();
                //BindNomineeAndJointHolders();
            }
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
            BindFolioNumber(Convert.ToInt32(ddlAmc.SelectedValue));
            SchemeBind(Convert.ToInt32(ddlAmc.SelectedValue), ddlCategory.SelectedValue);
            DataTable dtAmc = commonLookupBo.GetProdAmc(int.Parse(ddlAmc.SelectedValue));
            lnkFactSheet.PostBackUrl = dtAmc.Rows[0]["PA_Url"].ToString();
            //BindNomineeAndJointHolders();
        }

        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
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
                    ddlCategory.Items.Insert(0, new ListItem("All", "ALL"));
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
        }

        protected void BindSIPUI()
        {

        }

        private void SaveOrderDetails()
        {
            onlineMFOrderVo.SchemePlanCode = int.Parse(ddlScheme.SelectedValue);
            onlineMFOrderVo.AccountId = string.IsNullOrEmpty(ddlFolio.SelectedValue) ? 0 : int.Parse(ddlFolio.SelectedValue);
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
            int retVal = commonLookupBo.IsRuleCorrect(float.Parse(txtAmount.Text), float.Parse(lblMinAmountrequiredDisplay.Text), float.Parse(txtAmount.Text), float.Parse(lblMutiplesThereAfterDisplay.Text), DateTime.Parse(lblCutOffTimeDisplay.Text));
            if (retVal != 0 && retVal != 1) {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Rules defined were incorrect');", true);
                return;
            }
            if (retVal == 1) {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "javascript:DeleteConfirmation();", true);
            }
            List<int> OrderIds = new List<int>();
            SaveOrderDetails();
            OrderIds = boOnlineOrder.CreateOrderMFSipDetails(onlineMFOrderVo, userVo.UserId);
        }
        protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSIPUIONSchemeSelection();
            BindStartDates();
            BindddlTotalInstallments();
            ShowHideControlsForDivAndGrowth();
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
            MFReportsDao MFReportsDao=new MFReportsDao(); 
            DataSet dsNomineeAndJointHolders;
            dsNomineeAndJointHolders = MFReportsDao.GetARNNoAndJointHoldings(customerVo.CustomerId, 0, ddlFolio.SelectedItem.ToString());
            StringBuilder strbNominee = new StringBuilder();
            StringBuilder strbJointHolder = new StringBuilder();

            foreach (DataRow dr in dsNomineeAndJointHolders.Tables[1].Rows)
            {
                strbJointHolder.Append(dr["JointHolderName"].ToString()+",");
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
            DateTime[] dtStartdates;
            dtStartdates = boOnlineOrder.GetSipStartDates(Convert.ToInt32(ddlScheme.SelectedValue), ddlFrequency.SelectedValue);

            foreach (DateTime d in dtStartdates)
            {
                ddlStartDate.Items.Add(new ListItem(d.ToString("dd-MMM-yyyy"), d.ToString("dd-MMM-yyyy")));
            }
        }

        protected void ddlFrequency_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindSIPDetailsONFrequencySelection(ddlScheme.SelectedValue, ddlFrequency.SelectedValue);
        }
        protected void BindSIPDetailsONFrequencySelection(string schemeId, string freq)
        {
            DataSet dsSipDetails = boOnlineOrder.GetSipDetails(int.Parse(schemeId), freq);
            if (dsSipDetails == null) return;
            DataRow dtSipDet = dsSipDetails.Tables[0].Rows[0];
            lblMinAmountrequiredDisplay.Text = dtSipDet["PASPSD_MinAmount"].ToString();
            txtMinAmtDisplay.Text = dtSipDet["PASPSD_MinAmount"].ToString();            
            lblMutiplesThereAfterDisplay.Text = dtSipDet["PASPSD_MultipleAmount"].ToString();
            lblCutOffTimeDisplay.Text = dtSipDet["PASPD_CutOffTime"].ToString();
            lblDividendOptionDisplay.Text = dtSipDet["PSLV_LookupValue"].ToString();
            lblUnitHeldDisplay.Text = "0.00";
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

            DataSet ds = commonLookupBo.GetLatestNav(int.Parse(ddlScheme.SelectedValue));
            try
            {
                latNav = float.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
 
            }
            lblNavDisplay.Text = FormatFloat(latNav);
        }

        protected void SetOptionsList()
        {

        }

        protected void BindSIPUIONSchemeSelection()
        {
            dtGetAllSIPDataForOrder = commonLookupBo.GetAllSIPDataForOrder(Convert.ToInt32(ddlScheme.SelectedValue));
            BindFrequency();
            BindAllControlsWithSIPData();
            SetLatestNav();
            BindFolioNumber(int.Parse(ddlAmc.SelectedValue));
            BindModeOfHolding();
            //LoadNominees();
            //ShowSipDates();
        }

        protected void BindFrequency()
        {
            ddlFrequency.Items.Clear();
            foreach (DataRow row in dtGetAllSIPDataForOrder.Rows)
            {
                if (row["PASP_SchemePlanCode"].ToString() == ddlScheme.SelectedValue.ToString())
                {
                    ddlFrequency.Items.Add(new ListItem(row["XF_Frequency"].ToString(), row["XF_FrequencyCode"].ToString()));
                }

            }
            //dtGetAllSIPDataForOrder.
            //if (dtGetAllSIPDataForOrder != null)
            //{
            //    ddlFrequency.DataSource = dtGetAllSIPDataForOrder;
            //    ddlFrequency.DataTextField = dtGetAllSIPDataForOrder.Columns["XF_Frequency"].ToString();
            //    ddlFrequency.DataValueField = dtGetAllSIPDataForOrder.Columns["XF_FrequencyCode"].ToString();
            //    ddlFrequency.DataBind();

            //}
        }

        private void BindModeOfHolding()
        {
        }


        public void BindAllControlsWithSIPData()
        {
            if (dtGetAllSIPDataForOrder.Rows.Count > 0)
            {
                txtMinAmtDisplay.Text = dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MinAmount"].ToString();
                lblMutiplesThereAfterDisplay.Text = dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MultipleAmount"].ToString();
                lblCutOffTimeDisplay.Text = dtGetAllSIPDataForOrder.Rows[0]["PASPD_CutOffTime"].ToString();
                BindFrequency();
                ddlFrequency.SelectedValue = dtGetAllSIPDataForOrder.Rows[0]["XF_FrequencyCode"].ToString();
                ShowSipDates(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_StatingDates"].ToString());
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
                dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(Convert.ToInt32(ddlAmc.SelectedValue), customerVo.CustomerId);

                if (dtgetfolioNo.Rows.Count > 0)
                {
                    ddlFolio.DataSource = dtgetfolioNo;
                    ddlFolio.DataTextField = dtgetfolioNo.Columns["CMFA_FolioNum"].ToString();
                    ddlFolio.DataValueField = dtgetfolioNo.Columns["CMFA_AccountId"].ToString();
                    ddlFolio.DataBind();
                }
                ddlFolio.Items.Insert(0, new ListItem("Select", "0"));
                ddlFolio.Items.Insert(1, new ListItem("New", "1"));
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
            int minDues = Convert.ToInt32(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MinDues"]);
            int maxDues = Convert.ToInt32(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MaxDues"]);
            StringBuilder strTotalInstallments = new StringBuilder();

            for (int i = minDues; i <= maxDues; i++)
            {
                strTotalInstallments.Append(i + "~");
            }
            string str=strTotalInstallments.ToString();

            string[] strSplit= str.Split('~');

            foreach (string s in strSplit)
            {
                if(string.IsNullOrEmpty(s.Trim())) continue;
                ddlTotalInstallments.Items.Add(new ListItem (s.ToString()));
            }
        }

        protected void ddlTotalInstallments_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dtEndDate;
            dtEndDate = boOnlineOrder.GetSipEndDate(Convert.ToDateTime(ddlStartDate.SelectedValue), ddlFrequency.SelectedValue, Convert.ToInt32(ddlTotalInstallments.SelectedValue));
            lblEndDateDisplay.Text = dtEndDate.ToString("dd-MMM-yyyy");
        }

        protected void ddlFolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFolio.SelectedItem.ToString() != "New")
            {
                BindNomineeAndJointHolders();
                GetControlDetails(Convert.ToInt32(ddlScheme.SelectedValue), ddlFolio.SelectedValue);
                trNominee.Visible = true;
                trJointHolder.Visible = true;
            }
            else
            {
                trNominee.Visible = false;
                trJointHolder.Visible = false;
            }
        }
    }
}
