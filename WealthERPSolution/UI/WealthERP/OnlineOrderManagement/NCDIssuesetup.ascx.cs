using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using WealthERP.Base;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;


using VoUser;
using BoCommon;

using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;


namespace WealthERP.OnlineOrderManagement
{
    public partial class NCDIssuesetup : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        OnlineNCDBackOfficeVo onlineNCDBackOfficeVo;
        string issuerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                BindIssuer();
                pnlSeries.Visible = false;
                Panel1.Visible = false;
                //pnlSeries.Visible = true;
                //Panel1.Visible = true;
                //BindSeriesGrid("DHFL",39);
                //BindEligibleInvestorsGrid("DHFL",39);

            }
        }
        protected void rgSeries_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtIssueDetail;
            dtIssueDetail = (DataTable)Cache[userVo.UserId.ToString() + "Series"];
            if (dtIssueDetail != null)
            {
                rgSeries.DataSource = dtIssueDetail;
            }

        }
        protected void rgSeriesCat_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid rgSeriesCategories1 = (RadGrid)sender; // Get reference to grid 
            DataTable dtCategory = new DataTable();
            dtCategory = onlineNCDBackOfficeBo.GetCategory(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text)).Tables[0];
            rgSeriesCategories1.DataSource = dtCategory;
        }
        protected void rgSeries_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == RadGrid.UpdateCommandName)
                {

                    TextBox txtSereiesName = (TextBox)e.Item.FindControl("txtSereiesName");
                    TextBox txtTenure = (TextBox)e.Item.FindControl("txtTenure");
                    DropDownList ddlTenure = (DropDownList)e.Item.FindControl("ddlTenure");
                    TextBox txtInterestFrequency = (TextBox)e.Item.FindControl("txtInterestFrequency");
                    CheckBox chkBuyAvailability = (CheckBox)e.Item.FindControl("chkBuyAvailability");
                    DropDownList ddlInterestType = (DropDownList)e.Item.FindControl("ddlInterestType");

                    int availblity;
                    if (chkBuyAvailability.Checked == true)
                    {
                        availblity = 1;
                    }
                    else
                    {
                        availblity = 0;

                    }
                    int seriesId = CreateSeries(Convert.ToInt32(txtIssueId.Text), txtSereiesName.Text, availblity, Convert.ToInt32(txtTenure.Text), Convert.ToInt32(txtInterestFrequency.Text), ddlInterestType.SelectedValue);
                    RadGrid rgSeriesCat = (RadGrid)e.Item.FindControl("rgSeriesCat");

                    foreach (GridDataItem gdi in rgSeriesCat.Items)
                    {
                        if (((CheckBox)gdi.FindControl("cbSeriesCat")).Checked == true)
                        {
                            int categoryId = Convert.ToInt32(gdi["AIIC_InvestorCatgeoryId"].Text);
                            TextBox txtInterestRate = ((TextBox)(gdi.FindControl("txtInterestRate")));
                            TextBox txtAnnualizedYield = ((TextBox)(gdi.FindControl("txtAnnualizedYield")));
                            CreateSeriesCategories(seriesId, categoryId, Convert.ToDouble(txtInterestRate.Text), Convert.ToDouble(txtAnnualizedYield.Text));
                        }
                    }

                }
                BindSeriesGrid(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:rgMaping_UpdateCommand()");
                object[] objects = new object[2];
                objects[1] = source;
                objects[2] = e;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private int CreateSeries(int issueId, string seriesName, int isBuyBackAvailable, int tenure, int interestFrequency,
          string interestType)
        {
            try
            {
                bool result;
                onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                onlineNCDBackOfficeVo.IssueId = issueId;
                onlineNCDBackOfficeVo.SeriesName = seriesName.Trim();
                onlineNCDBackOfficeVo.IsBuyBackAvailable = isBuyBackAvailable;
                onlineNCDBackOfficeVo.Tenure = tenure;
                onlineNCDBackOfficeVo.InterestFrequency = interestFrequency;
                onlineNCDBackOfficeVo.InterestType = interestType;

                return onlineNCDBackOfficeBo.CreateSeries(onlineNCDBackOfficeVo, userVo.UserId);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:CreateNewWerpName()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void rgSeries_ItemCommand(object source, GridCommandEventArgs e)
        {
            string description = string.Empty;
            string name = string.Empty;
            string insertType = string.Empty;

            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                TextBox txtNewWERPName = (TextBox)e.Item.FindControl("txtSereiesName");
                TextBox txtTenure = (TextBox)e.Item.FindControl("txtTenure");
                DropDownList ddlTenure = (DropDownList)e.Item.FindControl("ddlTenure");
                TextBox txtInterestFrequency = (TextBox)e.Item.FindControl("txtInterestFrequency");
                CheckBox chkBuyAvailability = (CheckBox)e.Item.FindControl("ddlTenure");
                DropDownList ddlInterestType = (DropDownList)e.Item.FindControl("ddlInterestType");

            }

            BindSeriesGrid(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));

        }

        protected void rgEligibleInvestorCategories_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == RadGrid.UpdateCommandName)
                {
                    int categoryId;
                //    int issueId = Convert.ToInt32(rgEligibleInvestorCategories.MasterTableView.DataKeyValues[e.Item.ItemIndex + 1]["AIM_IssueId"].ToString());


                    TextBox txtCategoryName = (TextBox)e.Item.FindControl("txtCategoryName");
                    TextBox txtCategoryDescription = (TextBox)e.Item.FindControl("txtCategoryDescription");
                    TextBox txtChequePayableTo = (TextBox)e.Item.FindControl("txtChequePayableTo");
                    TextBox txtMinBidAmount = (TextBox)e.Item.FindControl("txtMinBidAmount");
                    TextBox txtMaxBidAmount = (TextBox)e.Item.FindControl("txtMaxBidAmount");


                    categoryId = CreateCategory(Convert.ToInt32(txtIssueId.Text), txtCategoryName.Text, txtCategoryDescription.Text, txtChequePayableTo.Text, Convert.ToInt32(txtMinBidAmount.Text), Convert.ToInt32(txtMaxBidAmount.Text));


                    RadGrid rgSubCategories = (RadGrid)e.Item.FindControl("rgSubCategories");
                    foreach (GridDataItem gdi in rgSubCategories.Items)
                    {
                        if (((CheckBox)gdi.FindControl("cbSubCategories")).Checked == true)
                        {
                            int lookupId = Convert.ToInt32(gdi["WCMV_LookupId"].Text);
                            TextBox txtSubCategoryCode = ((TextBox)(gdi.FindControl("txtSubCategoryCode")));
                            TextBox txtMinInvestmentAmount = ((TextBox)(gdi.FindControl("txtMinInvestmentAmount")));
                            TextBox txtMaxInvestmentAmount = ((TextBox)(gdi.FindControl("txtMaxInvestmentAmount")));

                            CreateSubTypePerCategory(categoryId, lookupId, txtSubCategoryCode.Text, Convert.ToInt32(txtMinInvestmentAmount.Text), Convert.ToInt32(txtMaxInvestmentAmount.Text));

                        }
                    }

                    BindEligibleInvestorsGrid(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));
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
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:rgMaping_UpdateCommand()");
                object[] objects = new object[2];
                objects[1] = source;
                objects[2] = e;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private int CreateCategory(int issueId, string investorCatgeoryName, string investorCatgeoryDescription, string chequePayableTo,
            int mInBidAmount, int maxBidAmount)
        {

            try
            {
                bool result;
                onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                onlineNCDBackOfficeVo.IssueId = issueId;
                onlineNCDBackOfficeVo.CatgeoryName = investorCatgeoryName;
                onlineNCDBackOfficeVo.CatgeoryDescription = investorCatgeoryDescription;
                onlineNCDBackOfficeVo.ChequePayableTo = chequePayableTo;
                onlineNCDBackOfficeVo.MInBidAmount = mInBidAmount;
                onlineNCDBackOfficeVo.MaxBidAmount = maxBidAmount;

                return onlineNCDBackOfficeBo.CreateCategory(onlineNCDBackOfficeVo, userVo.UserId);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:CreateNewWerpName()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void CreateSubTypePerCategory(int catgeoryId, int lookUpId, string subTypeCode, int minInvestmentAmount, int maxInvestmentAmount)
        {

            try
            {
                bool result;
                onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                onlineNCDBackOfficeVo.CatgeoryId = catgeoryId;
                onlineNCDBackOfficeVo.LookUpId = lookUpId;
                onlineNCDBackOfficeVo.SubCatgeoryTypeCode = subTypeCode;
                onlineNCDBackOfficeVo.MinInvestmentAmount = minInvestmentAmount;
                onlineNCDBackOfficeVo.MaxInvestmentAmount = maxInvestmentAmount;


                result = onlineNCDBackOfficeBo.CreateSubTypePerCategory(onlineNCDBackOfficeVo, userVo.UserId);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:CreateNewWerpName()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void rgSeries_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    GridEditFormItem editform = (GridEditFormItem)e.Item;
                    RadGrid rgSeriesCat = (RadGrid)editform.FindControl("rgSeriesCat");
                    BindCategory(rgSeriesCat, ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));
                    
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
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:rgMaping_ItemDataBound()");
                object[] objects = new object[2];
                objects[1] = sender;
                objects[2] = e;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }


        protected void btnSetUpSubmit_Click(object sender, EventArgs e)
        {

           // issuerId = ddlIssuer.SelectedValue;
           txtIssueId.Text = CreateIssue().ToString();
            pnlSeries.Visible = true;
            Panel1.Visible = true;
            BindSeriesGrid(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));
            BindEligibleInvestorsGrid(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));
        }
        private int CreateIssue()
        {
            int issueId;
            try
            {
                onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                onlineNCDBackOfficeVo.AssetGroupCode = ddlProduct.SelectedValue;
                onlineNCDBackOfficeVo.AssetInstrumentCategoryCode = ddlCategory.SelectedValue;

                onlineNCDBackOfficeVo.IssueName = txtName.Text;
                onlineNCDBackOfficeVo.IssuerId = ddlIssuer.SelectedValue;

                onlineNCDBackOfficeVo.FromRange = Convert.ToInt32(txtFormRange.Text);
                onlineNCDBackOfficeVo.ToRange = Convert.ToInt32(txtToRange.Text);


                if (!string.IsNullOrEmpty(txtInitialCqNo.Text))
                {
                    onlineNCDBackOfficeVo.InitialChequeNo = txtInitialCqNo.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.InitialChequeNo = "";
                }

              
                onlineNCDBackOfficeVo.FaceValue = Convert.ToDouble(txtFaceValue.Text);

                onlineNCDBackOfficeVo.FloorPrice = Convert.ToDouble(txtPrice.Text);
                onlineNCDBackOfficeVo.ModeOfIssue = ddlModeofIssue.SelectedValue;

                if (!string.IsNullOrEmpty(txtRating.Text))
                {
                    onlineNCDBackOfficeVo.Rating = txtRating.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.Rating ="";
                }
                // onlineNCDBackOfficeVo.Rating = Convert.ToInt32(txtRating.Text);
                onlineNCDBackOfficeVo.ModeOfTrading = ddlModeOfTrading.SelectedValue;



                onlineNCDBackOfficeVo.OpenDate = txtOpenDate.SelectedDate.Value;
                onlineNCDBackOfficeVo.CloseDate = txtCloseDate.SelectedDate.Value;


                string time = txtOpenTime.SelectedDate.Value.ToShortTimeString().ToString();
                //string time = d.ToString("HH:mm:ss");

                onlineNCDBackOfficeVo.OpenTime = txtOpenTime.SelectedDate.Value.ToShortTimeString().ToString();
                onlineNCDBackOfficeVo.CloseTime = txtCloseTime.SelectedDate.Value.ToShortTimeString().ToString();


                if (!string.IsNullOrEmpty((txtRevisionDates.SelectedDate).ToString().Trim()))
                    onlineNCDBackOfficeVo.IssueRevis = DateTime.Parse(txtRevisionDates.SelectedDate.ToString());
                else
                    onlineNCDBackOfficeVo.IssueRevis = DateTime.MinValue;


                
                    onlineNCDBackOfficeVo.TradingLot = Convert.ToDecimal(txtTradingLot.Text);            
                    onlineNCDBackOfficeVo.BiddingLot = Convert.ToDecimal(txtBiddingLot.Text);
                



                onlineNCDBackOfficeVo.MinApplicationSize = Convert.ToInt32(txtMinAplicSize.Text);
                if (!string.IsNullOrEmpty(txtIsPrefix.Text))
                {
                    onlineNCDBackOfficeVo.IsPrefix = txtIsPrefix.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.IsPrefix = "";
                }

                 
                    onlineNCDBackOfficeVo.TradingInMultipleOf = Convert.ToInt32(txtTradingInMultipleOf.Text);
                
                if (!string.IsNullOrEmpty(ddlListedInExchange.SelectedValue ))
                {
                    onlineNCDBackOfficeVo.ListedInExchange = ddlListedInExchange.SelectedValue;
                }
                else
                {
                    onlineNCDBackOfficeVo.ListedInExchange = "";
                }


                if (!string.IsNullOrEmpty(ddlBankName.Text))
                {
                    onlineNCDBackOfficeVo.BankName = ddlBankName.SelectedValue;
                }
                else
                {
                    onlineNCDBackOfficeVo.BankName = "";
                }

                if (!string.IsNullOrEmpty(ddlBankBranch.Text))
                {
                    onlineNCDBackOfficeVo.BankBranch = ddlBankBranch.SelectedValue;
                }
                else
                {
                    onlineNCDBackOfficeVo.BankBranch = "";
                }



                if (chkIsActive.Checked == true)
                {
                    onlineNCDBackOfficeVo.IsActive = 1;
                }
                else
                {
                    onlineNCDBackOfficeVo.IsActive = 0;
                }
                if (!string.IsNullOrEmpty(txtPutCallOption.Text))
                {
                    onlineNCDBackOfficeVo.PutCallOption = txtPutCallOption.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.PutCallOption = "";
                }

                if (chkNomineeReQuired.Checked == true)
                {
                    onlineNCDBackOfficeVo.IsNominationRequired = 1;
                }
                else
                {
                    onlineNCDBackOfficeVo.IsNominationRequired = 0;
                }


                issueId = onlineNCDBackOfficeBo.CreateIssue(onlineNCDBackOfficeVo, userVo.UserId);
                if (issueId > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Issue added successfully.');", true);
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
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:CreateNewWerpName()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return issueId;
        }
        private void BankName()
        {

        }

        private void CreateSeriesCategories(int seriesId, int catgeoryId, double defaultInterestRate, double annualizedYieldUpto)
        {
            try
            {
                bool result;
                onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                onlineNCDBackOfficeVo.SeriesId = seriesId;
                onlineNCDBackOfficeVo.CatgeoryId = catgeoryId;               
                onlineNCDBackOfficeVo.DefaultInterestRate = defaultInterestRate;
                onlineNCDBackOfficeVo.AnnualizedYieldUpto = annualizedYieldUpto;

                result = onlineNCDBackOfficeBo.CreateSeriesCategory(onlineNCDBackOfficeVo, userVo.UserId);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:CreateNewWerpName()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void btnExpandAll_Click(object sender, EventArgs e)
        {
            int seriesId = 0;
            LinkButton buttonlink = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)buttonlink.NamingContainer;
            seriesId = Convert.ToInt32(rgSeries.MasterTableView.DataKeyValues[gdi.ItemIndex]["PFISD_SeriesId"].ToString());
            RadGrid rgSeriesCategories = (RadGrid)gdi.FindControl("rgSeriesCategories");
            Panel pnlchild = (Panel)gdi.FindControl("pnlchild");

            if (pnlchild.Visible == false)
            {
                pnlchild.Visible = true;
                buttonlink.Text = "-";
            }
            else if (pnlchild.Visible == true)
            {
                pnlchild.Visible = false;
                buttonlink.Text = "+";
            }
            BindSeriesCategoryGrid(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text), seriesId, rgSeriesCategories);
        }
        protected void rgSubCategories_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid rgSeriesCategories1 = (RadGrid)sender; // Get reference to grid 
            GridDataItem item = (GridDataItem)(rgSeriesCategories1.NamingContainer as GridEditFormItem).ParentItem;  // Get the mastertableview item 
            DataTable dtSubCategory = new DataTable();
            dtSubCategory = onlineNCDBackOfficeBo.GetSubCategory(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text)).Tables[0];
            rgSeriesCategories1.DataSource = dtSubCategory;
        }

        protected void rgSeriesCategories_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid rgSeriesCategories1 = (RadGrid)sender; // Get reference to grid 
            GridDataItem item = (GridDataItem)(rgSeriesCategories1.NamingContainer as GridEditFormItem).ParentItem;  // Get the mastertableview item 
            int seriesId = Convert.ToInt32(item["PFISD_SeriesId"].Text); // Get the value 
            DataTable dtSeriesCategories = new DataTable();
            dtSeriesCategories = onlineNCDBackOfficeBo.GetSeriesCategories(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text), seriesId).Tables[0];
            rgSeriesCategories1.DataSource = dtSeriesCategories;
        }

        private void BindSeriesCategoryGrid(string issuerid, int issueid, int seriesId, RadGrid rgSeriesCategories)
        {
            try
            {
                DataTable dtSeriesCategories = new DataTable();
                dtSeriesCategories = onlineNCDBackOfficeBo.GetSeriesCategories(issuerid, issueid, seriesId).Tables[0];
                rgSeriesCategories.DataSource = dtSeriesCategories;
                rgSeriesCategories.DataBind();
                if (Cache[userVo.UserId.ToString() + "SeriesCategories"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "SeriesCategories");
                Cache.Insert(userVo.UserId.ToString() + "SeriesCategories", dtSeriesCategories);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:BindSeriesCategoryGrid()");
                object[] objects = new object[1];
                objects[1] = issuerid;
                objects[2] = issueid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindSeriesGrid(string issuerid, int issueid)
        {
            try
            {
                DataTable dtSeries = new DataTable();
                dtSeries = onlineNCDBackOfficeBo.GetSeries(issuerid, issueid).Tables[0];
                rgSeries.DataSource = dtSeries;
                rgSeries.DataBind();
                if (Cache[userVo.UserId.ToString() + "Series"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "Series");
                Cache.Insert(userVo.UserId.ToString() + "Series", dtSeries);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:BindSeriesCategoryGrid()");
                object[] objects = new object[2];
                objects[1] = issuerid;
                objects[2] = issueid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindCategory(RadGrid rgCategory, string issuerId, int issueId)
        {
            try
            {
                DataTable dtCategory = new DataTable();
                dtCategory = onlineNCDBackOfficeBo.GetCategory(issuerId, issueId).Tables[0];
                rgCategory.DataSource = dtCategory;
                rgCategory.DataBind();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:BindCategory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void BindIssuer()
        {
            try
            {
                DataSet dsIssuer = new DataSet();
                dsIssuer = onlineNCDBackOfficeBo.GetIssuer();

                if (dsIssuer.Tables[0].Rows.Count > 0)
                {
                    ddlIssuer.DataSource = dsIssuer;
                    ddlIssuer.DataValueField = dsIssuer.Tables[0].Columns["PFIIM_IssuerId"].ToString();
                    ddlIssuer.DataTextField = dsIssuer.Tables[0].Columns["PFIIM_IssuerName"].ToString();
                    ddlIssuer.DataBind();
                }
                ddlIssuer.Items.Insert(0, new ListItem("Select", "Select"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:BindIssuer()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }


        private void BindEligibleInvestorsGrid(string issuerId, int issueId)
        {

            try
            {
                DataTable dtInvestorCategories = new DataTable();
                dtInvestorCategories = onlineNCDBackOfficeBo.GetEligibleInvestorsCategory(issuerId, issueId).Tables[0];
                //if (dtInvestorCategories.Rows.Count > 0)
                //{
                //    if (Cache["EligibleInvestorsCategories" + userVo.UserId] == null)
                //    {
                //        Cache.Insert("EligibleInvestorsCategories" + userVo.UserId , dtInvestorCategories);
                //    }
                //    else
                //    {
                //        Cache.Remove("EligibleInvestorsCategories" + userVo.UserId );
                //        Cache.Insert("EligibleInvestorsCategories" + userVo.UserId,  dtInvestorCategories);
                //    }
                    rgEligibleInvestorCategories.DataSource = dtInvestorCategories;
                    rgEligibleInvestorCategories.DataBind();

                    if (Cache[userVo.UserId.ToString() + "EligibleInvestors"] != null)
                        Cache.Remove(userVo.UserId.ToString() + "EligibleInvestors");
                    Cache.Insert(userVo.UserId.ToString() + "EligibleInvestors", dtInvestorCategories);
                //}
                //else {
                //    rgEligibleInvestorCategories.DataSource = dtInvestorCategories;
                //    rgEligibleInvestorCategories.DataBind();
                //    rgEligibleInvestorCategories.Visible = true;
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
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:BindEligibleInvestorsGrid()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindSubCategoriesGrid(RadGrid rgSubCategory, string issuerId, int issueId)
        {

            try
            {
                DataTable dtSubCategory = new DataTable();
                dtSubCategory = onlineNCDBackOfficeBo.GetSubCategory(issuerId, issueId).Tables[0];
                rgSubCategory.DataSource = dtSubCategory;
                rgSubCategory.DataBind();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:BindInvestorCategoriesGrid()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void rgEligibleInvestorCategories_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtEligibleInvestorCategories=new DataTable();
            dtEligibleInvestorCategories = (DataTable)Cache[userVo.UserId.ToString() + "EligibleInvestors"];
            //dtEligibleInvestorCategories = (DataTable)Cache["EligibleInvestorsCategories" + userVo.UserId];
            //dtEligibleInvestorCategories = (DataTable)Cache[userVo.UserId.ToString() + "EligibleInvestorsCategories"];
            if (dtEligibleInvestorCategories != null)
            {
                rgEligibleInvestorCategories.DataSource = dtEligibleInvestorCategories;
            }

        }

        protected void rgEligibleInvestorCategories_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    GridEditFormItem editform = (GridEditFormItem)e.Item;
                    TextBox txtIssueName = (TextBox)editform.FindControl("txtIssueName");
                    txtIssueName.Text = txtName.Text;
                    RadGrid rgSubCategories = (RadGrid)editform.FindControl("rgSubCategories");
                    BindSubCategoriesGrid(rgSubCategories,ddlIssuer.SelectedValue , Convert.ToInt32( txtIssueId.Text));
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
                FunctionInfo.Add("Method", "ManageLookups.ascx.cs:rgMaping_ItemDataBound()");
                object[] objects = new object[2];
                objects[1] = sender;
                objects[2] = e;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }
}