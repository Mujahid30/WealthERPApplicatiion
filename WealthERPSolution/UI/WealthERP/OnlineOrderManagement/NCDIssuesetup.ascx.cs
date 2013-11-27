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
        //UserVo userVo;
        //AdvisorVo advisorVo;
        //OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        //OnlineNCDBackOfficeVo onlineNCDBackOfficeVo;
        //string issuerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionBo.CheckSession();
            //userVo = (UserVo)Session[SessionContents.UserVo];
            //advisorVo = (AdvisorVo)Session["advisorVo"];
            //int adviserId = advisorVo.advisorId;
            //if (!IsPostBack)
            //{
            //    BindIssuer();
            //    pnlSeries.Visible = false;
            //    pnlCategory.Visible = false;

            //    if (Request.QueryString["action"] != null)
            //    {
            //        int issueNo = Convert.ToInt32(Request.QueryString["issueNo"].ToString());
            //        ViewIssueList(issueNo, advisorVo.advisorId);
            //        btnUpdate.Visible = true;
            //        btnSetUpSubmit.Visible = false;
            //    }
            //    else
            //    {
            //        btnUpdate.Visible = false ;
            //        btnSetUpSubmit.Visible = true;
            //    }
            //}
        }

       // private void ViewIssueList(int issueNo, int adviserId)
       // {
       //     try
       //     {
       //         DataTable dtSeries = new DataTable();
       //         dtSeries = onlineNCDBackOfficeBo.GetIssueDetails(issueNo, adviserId).Tables[0];

       //         foreach (DataRow dr in dtSeries.Rows)
       //         {
       //             txtIssueId.Text = issueNo.ToString();
       //             ddlProduct.SelectedValue = "Bonds"; dr["PFISD_SeriesName"].ToString(); ;
       //             ddlCategory.SelectedValue = "NCD";
       //             txtName.Text = dr["AIM_IssueName"].ToString(); ;
       //             ddlIssuer.SelectedValue = dr["PFIIM_IssuerId"].ToString();
       //             txtFormRange.Text = dr["AIFR_From"].ToString();
       //             txtToRange.Text = dr["AIFR_To"].ToString();
       //             txtInitialCqNo.Text =  dr["AIM_InitialChequeNo"].ToString();
       //             if (!string.IsNullOrEmpty(dr["AIM_ModeOfIssue"].ToString()))
       //             {
       //                 ddlModeofIssue.SelectedValue = dr["AIM_ModeOfIssue"].ToString();
       //             }                   
       //             if (!string.IsNullOrEmpty(dr["AIM_ModeOfTrading"].ToString()))
       //             {
       //                 ddlModeOfTrading.SelectedValue = dr["AIM_ModeOfTrading"].ToString();
       //             }

       //             if (!string.IsNullOrEmpty(dr["AIM_OpenDate"].ToString()))
       //             {
       //                 txtOpenDate.SelectedDate =Convert.ToDateTime( dr["AIM_OpenDate"].ToString());
       //             }

       //             if (!string.IsNullOrEmpty(dr["AIM_CloseDate"].ToString()))
       //             {
       //                 txtCloseDate.SelectedDate = Convert.ToDateTime(dr["AIM_CloseDate"].ToString());
       //             }


       //             //string time                                                                                   txtOpenTimes.SelectedDate.Value.ToShortTimeString().ToString();
       //             if (!string.IsNullOrEmpty(dr["AIM_OpenTime"].ToString()))
       //             {
       //                 txtOpenTimes.Text = dr["AIM_OpenTime"].ToString(); ; //SelectedDate.Value.ToShortTimeString().ToString();
       //             }
       //             if (!string.IsNullOrEmpty(dr["AIM_CloseTime"].ToString()))
       //             {
       //                 txtCloseTimes.Text = dr["AIM_CloseTime"].ToString(); ; //SelectedDate.Value.ToShortTimeString().ToString();
       //             }

       //             txtRevisionDates.SelectedDate = DateTime.Now;

       //             if (!string.IsNullOrEmpty(dr["TradingLot"].ToString()))
       //             {
       //                 txtTradingLot.Text = dr["TradingLot"].ToString();
       //             }
       //             else
       //             {
       //                 txtTradingLot.Text = "";
       //             }

       //             txtBiddingLot.Text = dr["BiddingLot"].ToString();
       //             if (!string.IsNullOrEmpty(dr["IsPrefix"].ToString()))
       //             {
       //                 txtIsPrefix.Text = dr["IsPrefix"].ToString();
       //             }
       //             else
       //             {
       //                 txtIsPrefix.Text = "";
       //             }
       //             ddlListedInExchange.SelectedValue = "";
       //             ddlBankName.Text = "";
       //             ddlBankBranch.Text = "";
       //             if (!string.IsNullOrEmpty(dr["AIM_IsNominationRequired"].ToString()))
       //             {
       //                 chkIsActive.Checked = true;
       //             }

       //             if (!string.IsNullOrEmpty(dr["AIM_PutCallOption"].ToString()))
       //             {
       //                 txtPutCallOption.Text = dr["AIM_PutCallOption"].ToString();
       //             }
       //             else
       //             {
       //                 txtPutCallOption.Text = "";
       //             }

       //             if (!string.IsNullOrEmpty(dr["AIM_IsNominationRequired"].ToString()))
       //             {
       //                 chkNomineeReQuired.Checked = true ;                    
       //                                     }
       //             if (!string.IsNullOrEmpty(dr["AIM_MinApplicationSize"].ToString()))
       //             {
       //                 txtMinAplicSize.Text = dr["AIM_MinApplicationSize"].ToString();
       //             }
       //             else
       //             {
       //                 txtMinAplicSize.Text = "";
       //             }

       //             if (!string.IsNullOrEmpty(dr["AIM_TradingInMultipleOf"].ToString()))
       //              {
       //                  txtTradingInMultipleOf.Text = dr["AIM_TradingInMultipleOf"].ToString();
       //              }
       //              else
       //              {
       //                  txtTradingInMultipleOf.Text = "";
       //              }

       //             if (!string.IsNullOrEmpty(dr["FloorPrice"].ToString()))
       //              {
       //                  txtPrice.Text = dr["FloorPrice"].ToString();
       //              }
       //              else
       //              {
       //                  txtPrice.Text = "";
       //              }

       //             if (!string.IsNullOrEmpty(dr["FloorPrice"].ToString()))
       //             {
       //                 txtPrice.Text = dr["FloorPrice"].ToString();
       //             }
       //             else
       //             {
       //                 txtPrice.Text = "";
       //             }

       //             if (!string.IsNullOrEmpty(dr["AIM_FaceValue"].ToString()))
       //             {
       //                 txtFaceValue.Text = dr["AIM_FaceValue"].ToString();
       //             }
       //             else
       //             {
       //                 txtFaceValue.Text = "";
       //             }

       //             if (!string.IsNullOrEmpty(dr["AIM_FaceValue"].ToString()))
       //             {
       //                 txtFaceValue.Text = dr["AIM_FaceValue"].ToString();
       //             }
       //             else
       //             {
       //                 txtFaceValue.Text = "";
       //             }

       //             if (!string.IsNullOrEmpty(dr["AIM_FaceValue"].ToString()))
       //             {
       //                 txtFaceValue.Text = dr["AIM_FaceValue"].ToString();
       //             }
       //             else
       //             {
       //                 txtFaceValue.Text = "";
       //             }

       //             if (!string.IsNullOrEmpty(dr["AIM_Rating"].ToString()))
       //             {
       //                 txtRating.Text = dr["AIM_Rating"].ToString();
       //             }
       //             else
       //             {
       //                 txtRating.Text = "";
       //             }

       //             SeriesAndCategoriesGridsVisiblity(ddlIssuer.SelectedValue, issueNo);
       //         }
       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindSeriesCategoryGrid()");
       //         object[] objects = new object[2];
       //         objects[1] = issueNo;
       //         objects[2] = adviserId;
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }

       // }

       // private int UpdateIssue()
       // {
       //     int issueId;
       //     try
       //     {
       //         onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //         onlineNCDBackOfficeVo.IssueId = Convert.ToInt32(txtIssueId.Text);
       //         onlineNCDBackOfficeVo.AssetGroupCode = ddlProduct.SelectedValue;
       //         onlineNCDBackOfficeVo.AssetInstrumentCategoryCode = ddlCategory.SelectedValue;

       //         onlineNCDBackOfficeVo.IssueName = txtName.Text;
       //         onlineNCDBackOfficeVo.IssuerId = ddlIssuer.SelectedValue;

       //         onlineNCDBackOfficeVo.FromRange = Convert.ToInt32(txtFormRange.Text);
       //         onlineNCDBackOfficeVo.ToRange = Convert.ToInt32(txtToRange.Text);

       //         if (!string.IsNullOrEmpty(txtInitialCqNo.Text))
       //         {
       //             onlineNCDBackOfficeVo.InitialChequeNo = txtInitialCqNo.Text;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.InitialChequeNo = "";
       //         }

       //         onlineNCDBackOfficeVo.FaceValue = Convert.ToDouble(txtFaceValue.Text);

       //         onlineNCDBackOfficeVo.FloorPrice = Convert.ToDouble(txtPrice.Text);
       //         onlineNCDBackOfficeVo.ModeOfIssue = ddlModeofIssue.SelectedValue;

       //         if (!string.IsNullOrEmpty(txtRating.Text))
       //         {
       //             onlineNCDBackOfficeVo.Rating = txtRating.Text;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.Rating = "";
       //         }
       //         onlineNCDBackOfficeVo.ModeOfTrading = ddlModeOfTrading.SelectedValue;

       //         onlineNCDBackOfficeVo.OpenDate = txtOpenDate.SelectedDate.Value;
       //         onlineNCDBackOfficeVo.CloseDate = txtCloseDate.SelectedDate.Value;


       //         //string time = txtOpenTimes.SelectedDate.Value.ToShortTimeString().ToString();
       //         //onlineNCDBackOfficeVo.OpenTime = txtOpenTimes.Text; //SelectedDate.Value.ToShortTimeString().ToString();
       //         //onlineNCDBackOfficeVo.CloseTime = txtCloseTimes.Text; //SelectedDate.Value.ToShortTimeString().ToString();

       //         if (!string.IsNullOrEmpty((txtRevisionDates.SelectedDate).ToString().Trim()))
       //             onlineNCDBackOfficeVo.IssueRevis = DateTime.Parse(txtRevisionDates.SelectedDate.ToString());
       //         else
       //             onlineNCDBackOfficeVo.IssueRevis = DateTime.MinValue;

       //         onlineNCDBackOfficeVo.TradingLot = Convert.ToDecimal(txtTradingLot.Text);
       //         onlineNCDBackOfficeVo.BiddingLot = Convert.ToDecimal(txtBiddingLot.Text);

       //         onlineNCDBackOfficeVo.MinApplicationSize = Convert.ToInt32(txtMinAplicSize.Text);
       //         if (!string.IsNullOrEmpty(txtIsPrefix.Text))
       //         {
       //             onlineNCDBackOfficeVo.IsPrefix = Convert.ToInt32(txtIsPrefix.Text);
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.IsPrefix = 0;
       //         }

       //         onlineNCDBackOfficeVo.TradingInMultipleOf = Convert.ToInt32(txtTradingInMultipleOf.Text);

       //         if (!string.IsNullOrEmpty(ddlListedInExchange.SelectedValue))
       //         {
       //             onlineNCDBackOfficeVo.ListedInExchange = ddlListedInExchange.SelectedValue;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.ListedInExchange = "";
       //         }


       //         if (!string.IsNullOrEmpty(ddlBankName.Text))
       //         {
       //             onlineNCDBackOfficeVo.BankName = ddlBankName.SelectedValue;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.BankName = "";
       //         }

       //         if (!string.IsNullOrEmpty(ddlBankBranch.Text))
       //         {
       //             onlineNCDBackOfficeVo.BankBranch = ddlBankBranch.SelectedValue;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.BankBranch = "";
       //         }

       //         if (chkIsActive.Checked == true)
       //         {
       //             onlineNCDBackOfficeVo.IsActive = 1;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.IsActive = 0;
       //         }
       //         if (!string.IsNullOrEmpty(txtPutCallOption.Text))
       //         {
       //             onlineNCDBackOfficeVo.PutCallOption = txtPutCallOption.Text;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.PutCallOption = "";
       //         }

       //         if (chkNomineeReQuired.Checked == true)
       //         {
       //             onlineNCDBackOfficeVo.IsNominationRequired = 1;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.IsNominationRequired = 0;
       //         }
       //         issueId = onlineNCDBackOfficeBo.UpdateIssue(onlineNCDBackOfficeVo);
       //         if (issueId > 0)
       //         {
       //             ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Issue Updated successfully.');", true);
       //         }

       //     }

       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateNewWerpName()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       //     return issueId;
       // }


       // protected void rgSeries_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
       // {
       //     DataTable dtIssueDetail;
       //     dtIssueDetail = (DataTable)Cache[userVo.UserId.ToString() + "Series"];
       //     if (dtIssueDetail != null)
       //     {
       //         rgSeries.DataSource = dtIssueDetail;
       //     }
       // }

       // protected void rgSeriesCat_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
       // {
       //     RadGrid rgSeriesCategories1 = (RadGrid)sender; // Get reference to grid 
       //     DataTable dtCategory = new DataTable();
       //     dtCategory = onlineNCDBackOfficeBo.GetCategory(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text)).Tables[0];
       //     rgSeriesCategories1.DataSource = dtCategory;
       // }

       // protected void rgSeries_UpdateCommand(object source, GridCommandEventArgs e)
       // {
       //     try
       //     {
       //         if (e.CommandName == RadGrid.UpdateCommandName)
       //         {
       //             int availblity;
       //             TextBox txtSereiesName = (TextBox)e.Item.FindControl("txtSereiesName");
       //             TextBox txtTenure = (TextBox)e.Item.FindControl("txtTenure");
       //             DropDownList ddlTenure = (DropDownList)e.Item.FindControl("ddlTenure");
       //             TextBox txtInterestFrequency = (TextBox)e.Item.FindControl("txtInterestFrequency");
       //             CheckBox chkBuyAvailability = (CheckBox)e.Item.FindControl("chkBuyAvailability");
       //             DropDownList ddlInterestType = (DropDownList)e.Item.FindControl("ddlInterestType");
       //             if (chkBuyAvailability.Checked == true)
       //             {
       //                 availblity = 1;
       //             }
       //             else
       //             {
       //                 availblity = 0;
       //             }
       //             RadGrid rgSeriesCat = (RadGrid)e.Item.FindControl("rgSeriesCat");

       //             foreach (GridDataItem gdi in rgSeriesCat.Items)
       //             {
       //                 if (((CheckBox)gdi.FindControl("cbSeriesCat")).Checked == true)
       //                 {
       //                     int categoryId = Convert.ToInt32(gdi["AIIC_InvestorCatgeoryId"].Text);
       //                     TextBox txtInterestRate = ((TextBox)(gdi.FindControl("txtInterestRate")));
       //                     TextBox txtAnnualizedYield = ((TextBox)(gdi.FindControl("txtAnnualizedYield")));
       //                 }
       //             }
       //         }
       //         BindSeriesGrid(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));
       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:rgSeries_UpdateCommand()");
       //         object[] objects = new object[2];
       //         objects[1] = source;
       //         objects[2] = e;
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }

       // private int CreateUpdateDeleteSeries(int issueId, int seriesId, string seriesName, int isBuyBackAvailable, int tenure, string interestFrequency,
       //  string interestType, string CommandType)
       // {
       //     int result = 0;

       //     if (CommandType == "Insert")
       //     {
       //         onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //         onlineNCDBackOfficeVo.IssueId = issueId;
       //         onlineNCDBackOfficeVo.SeriesName = seriesName.Trim();
       //         onlineNCDBackOfficeVo.IsBuyBackAvailable = isBuyBackAvailable;
       //         onlineNCDBackOfficeVo.Tenure = tenure;
       //         onlineNCDBackOfficeVo.InterestFrequency = interestFrequency;
       //         onlineNCDBackOfficeVo.InterestType = interestType;
       //         result = onlineNCDBackOfficeBo.CreateSeries(onlineNCDBackOfficeVo, userVo.UserId);
       //     }
       //     else if (CommandType == "Update")
       //     {
       //         onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //         onlineNCDBackOfficeVo.SeriesId = seriesId;
       //         onlineNCDBackOfficeVo.SeriesName = seriesName.Trim();
       //         onlineNCDBackOfficeVo.IsBuyBackAvailable = isBuyBackAvailable;
       //         onlineNCDBackOfficeVo.Tenure = tenure;
       //         onlineNCDBackOfficeVo.InterestFrequency = interestFrequency;
       //         onlineNCDBackOfficeVo.InterestType = interestType;
       //         result = onlineNCDBackOfficeBo.UpdateSeries(onlineNCDBackOfficeVo, userVo.UserId);

       //     }
       //     else if (CommandType == "Delete")
       //     {
       //         onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //         onlineNCDBackOfficeVo.IssueId = issueId;
       //         onlineNCDBackOfficeVo.SeriesName = seriesName.Trim();
       //         onlineNCDBackOfficeVo.IsBuyBackAvailable = isBuyBackAvailable;
       //         onlineNCDBackOfficeVo.Tenure = tenure;
       //         onlineNCDBackOfficeVo.InterestFrequency = interestFrequency;
       //         onlineNCDBackOfficeVo.InterestType = interestType;
       //     }
       //     return result;

       // }

       // private int CreateSeries(int issueId, string seriesName, int isBuyBackAvailable, int tenure, string interestFrequency,
       //   string interestType)
       // {
       //     try
       //     {
       //         onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //         onlineNCDBackOfficeVo.IssueId = issueId;
       //         onlineNCDBackOfficeVo.SeriesName = seriesName.Trim();
       //         onlineNCDBackOfficeVo.IsBuyBackAvailable = isBuyBackAvailable;
       //         onlineNCDBackOfficeVo.Tenure = tenure;
       //         onlineNCDBackOfficeVo.InterestFrequency = interestFrequency;
       //         onlineNCDBackOfficeVo.InterestType = interestType;
       //         return onlineNCDBackOfficeBo.CreateSeries(onlineNCDBackOfficeVo, userVo.UserId);
       //     }

       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateSeries()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }

       // protected void rgSeries_ItemCommand(object source, GridCommandEventArgs e)
       // {
       //     if (e.CommandName == RadGrid.PerformInsertCommandName)
       //     {
       //         int availblity;
       //         TextBox txtSereiesName = (TextBox)e.Item.FindControl("txtSereiesName");
       //         TextBox txtTenure = (TextBox)e.Item.FindControl("txtTenure");
       //         DropDownList ddlTenure = (DropDownList)e.Item.FindControl("ddlTenure");
       //         TextBox txtInterestFrequency = (TextBox)e.Item.FindControl("txtInterestFrequency");
       //         CheckBox chkBuyAvailability = (CheckBox)e.Item.FindControl("chkBuyAvailability");
       //         DropDownList ddlInterestType = (DropDownList)e.Item.FindControl("ddlInterestType");
       //         if (chkBuyAvailability.Checked == true)
       //         {
       //             availblity = 1;
       //         }
       //         else
       //         {
       //             availblity = 0;
       //         }
       //         int seriesId = CreateUpdateDeleteSeries(Convert.ToInt32(txtIssueId.Text), 0, txtSereiesName.Text, availblity, Convert.ToInt32(txtTenure.Text), txtInterestFrequency.Text, ddlInterestType.SelectedValue, "Insert");
       //         RadGrid rgSeriesCat = (RadGrid)e.Item.FindControl("rgSeriesCat");

       //         foreach (GridDataItem gdi in rgSeriesCat.Items)
       //         {
       //             if (((CheckBox)gdi.FindControl("cbSeriesCat")).Checked == true)
       //             {
       //                 int categoryId = Convert.ToInt32(gdi["AIIC_InvestorCatgeoryId"].Text);
       //                 TextBox txtInterestRate = ((TextBox)(gdi.FindControl("txtInterestRate")));
       //                 TextBox txtAnnualizedYield = ((TextBox)(gdi.FindControl("txtAnnualizedYield")));
       //                 CreateUpdateDeleteSeriesCategories(seriesId, categoryId, Convert.ToDouble(txtInterestRate.Text), Convert.ToDouble(txtAnnualizedYield.Text), "Insert");
       //             }
       //         }
       //         BindSeriesGrid(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));

       //     }
       //     else if (e.CommandName == RadGrid.UpdateCommandName)
       //     {
       //         int availblity;
       //         TextBox txtSereiesName = (TextBox)e.Item.FindControl("txtSereiesName");
       //         TextBox txtTenure = (TextBox)e.Item.FindControl("txtTenure");
       //         DropDownList ddlTenure = (DropDownList)e.Item.FindControl("ddlTenure");
       //         TextBox txtInterestFrequency = (TextBox)e.Item.FindControl("txtInterestFrequency");
       //         CheckBox chkBuyAvailability = (CheckBox)e.Item.FindControl("chkBuyAvailability");
       //         DropDownList ddlInterestType = (DropDownList)e.Item.FindControl("ddlInterestType");
       //         if (chkBuyAvailability.Checked == true)
       //         {
       //             availblity = 1;
       //         }
       //         else
       //         {
       //             availblity = 0;
       //         }
       //         int seriesId = Convert.ToInt32(rgSeries.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PFISD_SeriesId"].ToString());
       //         int InsseriesId = CreateUpdateDeleteSeries(Convert.ToInt32(txtIssueId.Text), seriesId, txtSereiesName.Text, availblity, Convert.ToInt32(txtTenure.Text), txtInterestFrequency.Text, ddlInterestType.SelectedValue, "Update");
       //         RadGrid rgSeriesCat = (RadGrid)e.Item.FindControl("rgSeriesCat");
       //         foreach (GridDataItem gdi in rgSeriesCat.Items)
       //         {
       //             if (((CheckBox)gdi.FindControl("cbSeriesCat")).Checked == true)
       //             {
       //                 int categoryId = Convert.ToInt32(gdi["AIIC_InvestorCatgeoryId"].Text);
       //                 TextBox txtInterestRate = ((TextBox)(gdi.FindControl("txtInterestRate")));
       //                 TextBox txtAnnualizedYield = ((TextBox)(gdi.FindControl("txtAnnualizedYield")));
       //                 CreateUpdateDeleteSeriesCategories(seriesId, categoryId, Convert.ToDouble(txtInterestRate.Text), Convert.ToDouble(txtAnnualizedYield.Text), "Update");
       //             }
       //         }
       //         BindSeriesGrid(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));
       //     }
       //     else if (e.CommandName == RadGrid.DeleteCommandName)
       //     {

       //     }

       // }

       // protected void rgSeries_ItemCreated(object sender, GridItemEventArgs e)
       // {
       //     if (e.Item is GridNestedViewItem)
       //     {
       //         (e.Item.FindControl("rgSeriesCategories") as RadGrid).NeedDataSource += new GridNeedDataSourceEventHandler(rgSeriesCategories_OnNeedDataSource);
       //     }
       // }


       // protected void rgEligibleInvestorCategories_ItemCommand(object source, GridCommandEventArgs e)
       // {
       //     string description = string.Empty;
       //     if (e.CommandName == RadGrid.PerformInsertCommandName)
       //     {
       //         int categoryId;
       //         TextBox txtCategoryName = (TextBox)e.Item.FindControl("txtCategoryName");
       //         TextBox txtCategoryDescription = (TextBox)e.Item.FindControl("txtCategoryDescription");
       //         TextBox txtChequePayableTo = (TextBox)e.Item.FindControl("txtChequePayableTo");
       //         TextBox txtMinBidAmount = (TextBox)e.Item.FindControl("txtMinBidAmount");
       //         TextBox txtMaxBidAmount = (TextBox)e.Item.FindControl("txtMaxBidAmount");
       //         categoryId = CreateUpdateDeleteCategory(Convert.ToInt32(txtIssueId.Text), 0, txtCategoryName.Text, txtCategoryDescription.Text, txtChequePayableTo.Text, Convert.ToInt32(txtMinBidAmount.Text), Convert.ToInt32(txtMaxBidAmount.Text), "Insert");
       //         RadGrid rgSubCategories = (RadGrid)e.Item.FindControl("rgSubCategories");
       //         foreach (GridDataItem gdi in rgSubCategories.Items)
       //         {
       //             if (((CheckBox)gdi.FindControl("cbSubCategories")).Checked == true)
       //             {
       //                 int lookupId = Convert.ToInt32(gdi["WCMV_LookupId"].Text);
       //                 TextBox txtSubCategoryCode = ((TextBox)(gdi.FindControl("txtSubCategoryCode")));
       //                 TextBox txtMinInvestmentAmount = ((TextBox)(gdi.FindControl("txtMinInvestmentAmount")));
       //                 TextBox txtMaxInvestmentAmount = ((TextBox)(gdi.FindControl("txtMaxInvestmentAmount")));
       //                 CreateUpdateDeleteCategoryDetails(categoryId, lookupId, txtSubCategoryCode.Text, Convert.ToInt32(txtMinInvestmentAmount.Text), Convert.ToInt32(txtMaxInvestmentAmount.Text), "Insert");
       //             }
       //         }
       //         BindEligibleInvestorsGrid(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));
       //     }
       //     else if (e.CommandName == RadGrid.UpdateCommandName)
       //     {
       //         int categoryId;
       //         int result;

       //         categoryId = Convert.ToInt32(rgEligibleInvestorCategories.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString());
       //         TextBox txtCategoryName = (TextBox)e.Item.FindControl("txtCategoryName");
       //         TextBox txtCategoryDescription = (TextBox)e.Item.FindControl("txtCategoryDescription");
       //         TextBox txtChequePayableTo = (TextBox)e.Item.FindControl("txtChequePayableTo");
       //         TextBox txtMinBidAmount = (TextBox)e.Item.FindControl("txtMinBidAmount");
       //         TextBox txtMaxBidAmount = (TextBox)e.Item.FindControl("txtMaxBidAmount");
       //         result = CreateUpdateDeleteCategory(0, categoryId, txtCategoryName.Text, txtCategoryDescription.Text, txtChequePayableTo.Text, Convert.ToInt32(txtMinBidAmount.Text), Convert.ToInt32(txtMaxBidAmount.Text), "Update");
       //         RadGrid rgSubCategories = (RadGrid)e.Item.FindControl("rgSubCategories");
       //         foreach (GridDataItem gdi in rgSubCategories.Items)
       //         {
       //             if (((CheckBox)gdi.FindControl("cbSubCategories")).Checked == true)
       //             {
       //                 int lookupId = Convert.ToInt32(gdi["WCMV_LookupId"].Text);
       //                 TextBox txtSubCategoryCode = ((TextBox)(gdi.FindControl("txtSubCategoryCode")));
       //                 TextBox txtMinInvestmentAmount = ((TextBox)(gdi.FindControl("txtMinInvestmentAmount")));
       //                 TextBox txtMaxInvestmentAmount = ((TextBox)(gdi.FindControl("txtMaxInvestmentAmount")));
       //                 CreateUpdateDeleteCategoryDetails(categoryId, lookupId, txtSubCategoryCode.Text, Convert.ToInt32(txtMinInvestmentAmount.Text), Convert.ToInt32(txtMaxInvestmentAmount.Text), "Update");
       //             }
       //         }

       //         BindEligibleInvestorsGrid(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));

       //     }
       //     else if (e.CommandName == RadGrid.DeleteCommandName)
       //     {
       //     }
       // }

       // protected void rgEligibleInvestorCategories_UpdateCommand(object source, GridCommandEventArgs e)
       // {
       //     try
       //     {
       //         if (e.CommandName == RadGrid.UpdateCommandName)
       //         {
       //             int categoryId;
       //             TextBox txtCategoryName = (TextBox)e.Item.FindControl("txtCategoryName");
       //             TextBox txtCategoryDescription = (TextBox)e.Item.FindControl("txtCategoryDescription");
       //             TextBox txtChequePayableTo = (TextBox)e.Item.FindControl("txtChequePayableTo");
       //             TextBox txtMinBidAmount = (TextBox)e.Item.FindControl("txtMinBidAmount");
       //             TextBox txtMaxBidAmount = (TextBox)e.Item.FindControl("txtMaxBidAmount");
       //             categoryId = CreateCategory(Convert.ToInt32(txtIssueId.Text), txtCategoryName.Text, txtCategoryDescription.Text, txtChequePayableTo.Text, Convert.ToInt32(txtMinBidAmount.Text), Convert.ToInt32(txtMaxBidAmount.Text));
       //             RadGrid rgSubCategories = (RadGrid)e.Item.FindControl("rgSubCategories");
       //             foreach (GridDataItem gdi in rgSubCategories.Items)
       //             {
       //                 if (((CheckBox)gdi.FindControl("cbSubCategories")).Checked == true)
       //                 {
       //                     int lookupId = Convert.ToInt32(gdi["WCMV_LookupId"].Text);
       //                     TextBox txtSubCategoryCode = ((TextBox)(gdi.FindControl("txtSubCategoryCode")));
       //                     TextBox txtMinInvestmentAmount = ((TextBox)(gdi.FindControl("txtMinInvestmentAmount")));
       //                     TextBox txtMaxInvestmentAmount = ((TextBox)(gdi.FindControl("txtMaxInvestmentAmount")));
       //                     CreateSubTypePerCategory(categoryId, lookupId, txtSubCategoryCode.Text, Convert.ToInt32(txtMinInvestmentAmount.Text), Convert.ToInt32(txtMaxInvestmentAmount.Text));
       //                 }
       //             }
       //             BindEligibleInvestorsGrid(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));
       //         }
       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:rgEligibleInvestorCategories_UpdateCommand()");
       //         object[] objects = new object[2];
       //         objects[1] = source;
       //         objects[2] = e;
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }

       // private int CreateUpdateDeleteCategory(int issueId, int categoryId, string investorCatgeoryName, string investorCatgeoryDescription, string chequePayableTo,
       //    int mInBidAmount, int maxBidAmount, string CommandType)
       // {
       //     int result = 0;
       //     try
       //     {
       //         if (CommandType == "Insert")
       //         {
       //             onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //             onlineNCDBackOfficeVo.IssueId = issueId;
       //             onlineNCDBackOfficeVo.CatgeoryName = investorCatgeoryName;
       //             onlineNCDBackOfficeVo.CatgeoryDescription = investorCatgeoryDescription;
       //             onlineNCDBackOfficeVo.ChequePayableTo = chequePayableTo;
       //             onlineNCDBackOfficeVo.MInBidAmount = mInBidAmount;
       //             onlineNCDBackOfficeVo.MaxBidAmount = maxBidAmount;
       //             return onlineNCDBackOfficeBo.CreateCategory(onlineNCDBackOfficeVo, userVo.UserId);
       //         }
       //         else if (CommandType == "Update")
       //         {
       //             onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //             //onlineNCDBackOfficeVo.IssueId = issueId;
       //             onlineNCDBackOfficeVo.CatgeoryId = categoryId;
       //             onlineNCDBackOfficeVo.CatgeoryName = investorCatgeoryName;
       //             onlineNCDBackOfficeVo.CatgeoryDescription = investorCatgeoryDescription;
       //             onlineNCDBackOfficeVo.ChequePayableTo = chequePayableTo;
       //             onlineNCDBackOfficeVo.MInBidAmount = mInBidAmount;
       //             onlineNCDBackOfficeVo.MaxBidAmount = maxBidAmount;
       //             result = onlineNCDBackOfficeBo.UpdateCategory(onlineNCDBackOfficeVo, userVo.UserId);
       //         }
       //         else if (CommandType == "Delete")
       //         {
       //             onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //             onlineNCDBackOfficeVo.IssueId = issueId;
       //             onlineNCDBackOfficeVo.CatgeoryName = investorCatgeoryName;
       //             onlineNCDBackOfficeVo.CatgeoryDescription = investorCatgeoryDescription;
       //             onlineNCDBackOfficeVo.ChequePayableTo = chequePayableTo;
       //             onlineNCDBackOfficeVo.MInBidAmount = mInBidAmount;
       //             onlineNCDBackOfficeVo.MaxBidAmount = maxBidAmount;
       //         }
       //         return result;
       //     }

       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateUpdateDeleteCategory()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }


       // private int CreateCategory(int issueId, string investorCatgeoryName, string investorCatgeoryDescription, string chequePayableTo,
       //     int mInBidAmount, int maxBidAmount)
       // {
       //     try
       //     {
       //         onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //         onlineNCDBackOfficeVo.IssueId = issueId;
       //         onlineNCDBackOfficeVo.CatgeoryName = investorCatgeoryName;
       //         onlineNCDBackOfficeVo.CatgeoryDescription = investorCatgeoryDescription;
       //         onlineNCDBackOfficeVo.ChequePayableTo = chequePayableTo;
       //         onlineNCDBackOfficeVo.MInBidAmount = mInBidAmount;
       //         onlineNCDBackOfficeVo.MaxBidAmount = maxBidAmount;
       //         return onlineNCDBackOfficeBo.CreateCategory(onlineNCDBackOfficeVo, userVo.UserId);
       //     }

       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateNewWerpName()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }

       // private void CreateUpdateDeleteCategoryDetails(int catgeoryId, int lookUpId, string subTypeCode, int minInvestmentAmount, int maxInvestmentAmount, string CommandType)
       // {
       //     bool result = false;
       //     try
       //     {
       //         if (CommandType == "Insert")
       //         {
       //             onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //             onlineNCDBackOfficeVo.CatgeoryId = catgeoryId;
       //             onlineNCDBackOfficeVo.LookUpId = lookUpId;
       //             onlineNCDBackOfficeVo.SubCatgeoryTypeCode = subTypeCode;
       //             onlineNCDBackOfficeVo.MinInvestmentAmount = minInvestmentAmount;
       //             onlineNCDBackOfficeVo.MaxInvestmentAmount = maxInvestmentAmount;
       //             result = onlineNCDBackOfficeBo.CreateSubTypePerCategory(onlineNCDBackOfficeVo, userVo.UserId);
       //         }
       //         else if (CommandType == "Update")
       //         {
       //             onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //             onlineNCDBackOfficeVo.CatgeoryId = catgeoryId;
       //             onlineNCDBackOfficeVo.LookUpId = lookUpId;
       //             onlineNCDBackOfficeVo.SubCatgeoryTypeCode = subTypeCode;
       //             onlineNCDBackOfficeVo.MinInvestmentAmount = minInvestmentAmount;
       //             onlineNCDBackOfficeVo.MaxInvestmentAmount = maxInvestmentAmount;
       //             result = onlineNCDBackOfficeBo.UpdateCategoryDetails(onlineNCDBackOfficeVo, userVo.UserId);

       //         }
       //         else if (CommandType == "Delete")
       //         {
       //             onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //             onlineNCDBackOfficeVo.CatgeoryId = catgeoryId;
       //             onlineNCDBackOfficeVo.LookUpId = lookUpId;
       //             onlineNCDBackOfficeVo.SubCatgeoryTypeCode = subTypeCode;
       //             onlineNCDBackOfficeVo.MinInvestmentAmount = minInvestmentAmount;
       //             onlineNCDBackOfficeVo.MaxInvestmentAmount = maxInvestmentAmount;
       //         }

       //     }

       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateNewWerpName()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }

       // private void CreateSubTypePerCategory(int catgeoryId, int lookUpId, string subTypeCode, int minInvestmentAmount, int maxInvestmentAmount)
       // {
       //     try
       //     {
       //         bool result;
       //         onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //         onlineNCDBackOfficeVo.CatgeoryId = catgeoryId;
       //         onlineNCDBackOfficeVo.LookUpId = lookUpId;
       //         onlineNCDBackOfficeVo.SubCatgeoryTypeCode = subTypeCode;
       //         onlineNCDBackOfficeVo.MinInvestmentAmount = minInvestmentAmount;
       //         onlineNCDBackOfficeVo.MaxInvestmentAmount = maxInvestmentAmount;
       //         result = onlineNCDBackOfficeBo.CreateSubTypePerCategory(onlineNCDBackOfficeVo, userVo.UserId);
       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateNewWerpName()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }

       // protected void rgSeries_ItemDataBound(object sender, GridItemEventArgs e)
       // {
       //     try
       //     {
       //         if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
       //         {
       //             GridEditFormItem editform = (GridEditFormItem)e.Item;
       //             RadGrid rgSeriesCat = (RadGrid)editform.FindControl("rgSeriesCat");
       //             BindCategory(rgSeriesCat, ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));

       //         }
       //         else if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
       //         {
       //             GridEditFormItem editform = (GridEditFormItem)e.Item;
       //             int seriesId = Convert.ToInt32(rgSeries.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PFISD_SeriesId"].ToString());
       //             TextBox txtSereiesName = (TextBox)e.Item.FindControl("txtSereiesName");
       //             TextBox txtTenure = (TextBox)e.Item.FindControl("txtTenure");
       //             TextBox txtInterestFrequency = (TextBox)e.Item.FindControl("txtInterestFrequency");
       //             CheckBox chkBuyAvailability = (CheckBox)e.Item.FindControl("chkBuyAvailability");
       //             RadGrid rgSeriesCat = (RadGrid)editform.FindControl("rgSeriesCat");
       //             BindCategory(rgSeriesCat, ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));
       //             FillSeriesPopupControlsForUpdate(seriesId, txtSereiesName, txtTenure, txtInterestFrequency, chkBuyAvailability, rgSeriesCat);
       //         }
       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:rgSeries_ItemDataBound()");
       //         object[] objects = new object[2];
       //         objects[1] = sender;
       //         objects[2] = e;
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }

       // private void FillSeriesPopupControlsForUpdate(int seriesId, TextBox txtSereiesName, TextBox txtTenure,
       //                  TextBox txtInterestFrequency, CheckBox chkBuyAvailability, RadGrid rgSeriesCat)
       // {
       //     try
       //     {
       //         DataTable dtCategory = new DataTable();
       //         dtCategory = onlineNCDBackOfficeBo.GetSeriesInvestorTypeRule(seriesId).Tables[0];

       //         if (dtCategory.Rows.Count > 0)
       //         {
       //             foreach (DataRow dr in dtCategory.Rows)
       //             {
       //                 txtSereiesName.Text = dr["PFISD_SeriesName"].ToString();
       //                 txtTenure.Text = dr["PFISD_Tenure"].ToString();
       //                 txtInterestFrequency.Text = dr["PFISD_InterestFrequency"].ToString();
       //                 chkBuyAvailability.Checked = Convert.ToBoolean(dr["PFISD_BuyBackFacility"].ToString());
       //                 int seriesCategoryId = Convert.ToInt32(dr["AIIC_InvestorCatgeoryId"]);

       //                 foreach (GridDataItem gdi in rgSeriesCat.Items)
       //                 {
       //                     int grdcategoryId = Convert.ToInt32(gdi["AIIC_InvestorCatgeoryId"].Text);
       //                     if (seriesCategoryId == grdcategoryId)
       //                     {
       //                         CheckBox cbSeriesCat = (CheckBox)gdi.FindControl("cbSeriesCat");
       //                         TextBox txtInterestRate = ((TextBox)(gdi.FindControl("txtInterestRate")));
       //                         TextBox txtAnnualizedYield = ((TextBox)(gdi.FindControl("txtAnnualizedYield")));

       //                         cbSeriesCat.Checked = true;
       //                         txtInterestRate.Text = dr["PFISD_DefaultInterestRate"].ToString();
       //                         txtAnnualizedYield.Text = dr["PFISD_AnnualizedYieldUpto"].ToString();
       //                     }
       //                 }
       //             }



       //         }
       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindCategory()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }

       // }

       // protected void btnSetUpSubmit_Click(object sender, EventArgs e)
       // {
       //     txtIssueId.Text = CreateIssue().ToString();
       //    // EnablityOfScreen(false, true);
       //     SeriesAndCategoriesGridsVisiblity(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));
       // }

       // protected void btnUpdate_Click(object sender, EventArgs e)
       // {
       //     int result = UpdateIssue();
       ////     EnablityOfScreen(false, true);         
       //     SeriesAndCategoriesGridsVisiblity(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));
       // }
        
       // private void SeriesAndCategoriesGridsVisiblity(string issuerId,int issueId)
       // {
       //     pnlSeries.Visible = true;
       //     pnlCategory.Visible = true;
       //     BindSeriesGrid(issuerId, issueId);
       //     BindEligibleInvestorsGrid(issuerId, issueId);
       // }

       // private int CreateIssue()
       // {
       //     int issueId;
       //     try
       //     {
       //         onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //         onlineNCDBackOfficeVo.AssetGroupCode = ddlProduct.SelectedValue;
       //         onlineNCDBackOfficeVo.AssetInstrumentCategoryCode = ddlCategory.SelectedValue;

       //         onlineNCDBackOfficeVo.IssueName = txtName.Text;
       //         onlineNCDBackOfficeVo.IssuerId = ddlIssuer.SelectedValue;

       //         onlineNCDBackOfficeVo.FromRange = Convert.ToInt32(txtFormRange.Text);
       //         onlineNCDBackOfficeVo.ToRange = Convert.ToInt32(txtToRange.Text);

       //         if (!string.IsNullOrEmpty(txtInitialCqNo.Text))
       //         {
       //             onlineNCDBackOfficeVo.InitialChequeNo = txtInitialCqNo.Text;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.InitialChequeNo = "";
       //         }

       //         onlineNCDBackOfficeVo.FaceValue = Convert.ToDouble(txtFaceValue.Text);

       //         onlineNCDBackOfficeVo.FloorPrice = Convert.ToDouble(txtPrice.Text);
       //         onlineNCDBackOfficeVo.ModeOfIssue = ddlModeofIssue.SelectedValue;

       //         if (!string.IsNullOrEmpty(txtRating.Text))
       //         {
       //             onlineNCDBackOfficeVo.Rating = txtRating.Text;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.Rating = "";
       //         }
       //         onlineNCDBackOfficeVo.ModeOfTrading = ddlModeOfTrading.SelectedValue;

       //         onlineNCDBackOfficeVo.OpenDate = txtOpenDate.SelectedDate.Value;
       //         onlineNCDBackOfficeVo.CloseDate = txtCloseDate.SelectedDate.Value;


       //         //string time = txtOpenTimes.SelectedDate.Value.ToShortTimeString().ToString();
       //         //onlineNCDBackOfficeVo.OpenTime = txtOpenTimes.Text; //SelectedDate.Value.ToShortTimeString().ToString();
       //         //onlineNCDBackOfficeVo.CloseTime = txtCloseTimes.Text; //SelectedDate.Value.ToShortTimeString().ToString();

       //         if (!string.IsNullOrEmpty((txtRevisionDates.SelectedDate).ToString().Trim()))
       //             onlineNCDBackOfficeVo.IssueRevis = DateTime.Parse(txtRevisionDates.SelectedDate.ToString());
       //         else
       //             onlineNCDBackOfficeVo.IssueRevis = DateTime.MinValue;

       //         onlineNCDBackOfficeVo.TradingLot = Convert.ToDecimal(txtTradingLot.Text);
       //         onlineNCDBackOfficeVo.BiddingLot = Convert.ToDecimal(txtBiddingLot.Text);

       //         onlineNCDBackOfficeVo.MinApplicationSize = Convert.ToInt32(txtMinAplicSize.Text);
       //         if (!string.IsNullOrEmpty(txtIsPrefix.Text))
       //         {
       //             onlineNCDBackOfficeVo.IsPrefix =Convert.ToInt32( txtIsPrefix.Text);
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.IsPrefix = 0;
       //         }

       //         onlineNCDBackOfficeVo.TradingInMultipleOf = Convert.ToInt32(txtTradingInMultipleOf.Text);

       //         if (!string.IsNullOrEmpty(ddlListedInExchange.SelectedValue))
       //         {
       //             onlineNCDBackOfficeVo.ListedInExchange = ddlListedInExchange.SelectedValue;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.ListedInExchange = "";
       //         }


       //         if (!string.IsNullOrEmpty(ddlBankName.Text))
       //         {
       //             onlineNCDBackOfficeVo.BankName = ddlBankName.SelectedValue;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.BankName = "";
       //         }

       //         if (!string.IsNullOrEmpty(ddlBankBranch.Text))
       //         {
       //             onlineNCDBackOfficeVo.BankBranch = ddlBankBranch.SelectedValue;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.BankBranch = "";
       //         }

       //         if (chkIsActive.Checked == true)
       //         {
       //             onlineNCDBackOfficeVo.IsActive = 1;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.IsActive = 0;
       //         }
       //         if (!string.IsNullOrEmpty(txtPutCallOption.Text))
       //         {
       //             onlineNCDBackOfficeVo.PutCallOption = txtPutCallOption.Text;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.PutCallOption = "";
       //         }

       //         if (chkNomineeReQuired.Checked == true)
       //         {
       //             onlineNCDBackOfficeVo.IsNominationRequired = 1;
       //         }
       //         else
       //         {
       //             onlineNCDBackOfficeVo.IsNominationRequired = 0;
       //         }
       //         issueId = onlineNCDBackOfficeBo.CreateIssue(onlineNCDBackOfficeVo, userVo.UserId);
       //         if (issueId > 0)
       //         {
       //             ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Issue added successfully.');", true);
       //         }

       //     }

       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateNewWerpName()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       //     return issueId;
       // }

       // private void BankName()
       // {

       // }

       // private void CreateUpdateDeleteSeriesCategories(int seriesId, int catgeoryId, double defaultInterestRate, double annualizedYieldUpto, string commandType)
       // {
       //     bool result;
       //     try
       //     {
       //         if (commandType == "Insert")
       //         {
       //             onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //             onlineNCDBackOfficeVo.SeriesId = seriesId;
       //             onlineNCDBackOfficeVo.CatgeoryId = catgeoryId;
       //             onlineNCDBackOfficeVo.DefaultInterestRate = defaultInterestRate;
       //             onlineNCDBackOfficeVo.AnnualizedYieldUpto = annualizedYieldUpto;
       //             result = onlineNCDBackOfficeBo.CreateSeriesCategory(onlineNCDBackOfficeVo, userVo.UserId);
       //         }
       //         else if (commandType == "Update")
       //         {
       //             onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
       //             onlineNCDBackOfficeVo.SeriesId = seriesId;
       //             onlineNCDBackOfficeVo.CatgeoryId = catgeoryId;
       //             onlineNCDBackOfficeVo.DefaultInterestRate = defaultInterestRate;
       //             onlineNCDBackOfficeVo.AnnualizedYieldUpto = annualizedYieldUpto;
       //             result = onlineNCDBackOfficeBo.UpdateSeriesCategory(onlineNCDBackOfficeVo, userVo.UserId);
       //         }

       //     }

       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateNewWerpName()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }

       // protected void btnExpandAll_Click(object sender, EventArgs e)
       // {
       //     int seriesId = 0;
       //     LinkButton buttonlink = (LinkButton)sender;
       //     GridDataItem gdi;
       //     gdi = (GridDataItem)buttonlink.NamingContainer;
       //     seriesId = Convert.ToInt32(rgSeries.MasterTableView.DataKeyValues[gdi.ItemIndex]["PFISD_SeriesId"].ToString());
       //     RadGrid rgSeriesCategories = (RadGrid)gdi.FindControl("rgSeriesCategories");
       //     Panel pnlchild = (Panel)gdi.FindControl("pnlchild");

       //     if (pnlchild.Visible == false)
       //     {
       //         pnlchild.Visible = true;
       //         buttonlink.Text = "-";
       //     }
       //     else if (pnlchild.Visible == true)
       //     {
       //         pnlchild.Visible = false;
       //         buttonlink.Text = "+";
       //     }
       //     BindSeriesCategoryGrid(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text), seriesId, rgSeriesCategories);
       // }


       // protected void btnCategoriesExpandAll_Click(object sender, EventArgs e)
       // {
       //     int categoryId = 0;
       //     LinkButton buttonlink = (LinkButton)sender;
       //     GridDataItem gdi;
       //     gdi = (GridDataItem)buttonlink.NamingContainer;
       //     categoryId = Convert.ToInt32(rgEligibleInvestorCategories.MasterTableView.DataKeyValues[gdi.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString());
       //     RadGrid rgCategoriesDetails = (RadGrid)gdi.FindControl("rgCategoriesDetails");
       //     Panel pnlCategoriesDetailschild = (Panel)gdi.FindControl("pnlCategoriesDetailschild");

       //     if (pnlCategoriesDetailschild.Visible == false)
       //     {
       //         pnlCategoriesDetailschild.Visible = true;
       //         buttonlink.Text = "-";
       //     }
       //     else if (pnlCategoriesDetailschild.Visible == true)
       //     {
       //         pnlCategoriesDetailschild.Visible = false;
       //         buttonlink.Text = "+";
       //     }
       //     BindCategoriesDetailsGrid(categoryId, rgCategoriesDetails);
       // }

       // protected void rgSubCategories_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
       // {
       //     RadGrid rgSubCategories = (RadGrid)sender; // Get reference to grid 
       //     DataTable dtSubCategory = new DataTable();
       //     dtSubCategory = onlineNCDBackOfficeBo.GetSubCategory(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text)).Tables[0];
       //     rgSubCategories.DataSource = dtSubCategory;
       // }

       // protected void rgSeriesCategories_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
       // {
       //     RadGrid rgSeriesCategories1 = (RadGrid)sender; // Get reference to grid 
       //     GridDataItem nesteditem = (GridDataItem)rgSeriesCategories1.NamingContainer;
       //     int seriesId = Convert.ToInt32(rgSeries.MasterTableView.DataKeyValues[nesteditem.ItemIndex]["PFISD_SeriesId"].ToString());
       //     DataTable dtSeriesCategories = new DataTable();
       //     dtSeriesCategories = onlineNCDBackOfficeBo.GetSeriesCategories(ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text), seriesId).Tables[0];
       //     rgSeriesCategories1.DataSource = dtSeriesCategories;
       // }

       // private void BindSeriesCategoryGrid(string issuerid, int issueid, int seriesId, RadGrid rgSeriesCategories)
       // {
       //     try
       //     {
       //         DataTable dtSeriesCategories = new DataTable();
       //         dtSeriesCategories = onlineNCDBackOfficeBo.GetSeriesCategories(issuerid, issueid, seriesId).Tables[0];
       //         rgSeriesCategories.DataSource = dtSeriesCategories;
       //         rgSeriesCategories.DataBind();
       //         if (Cache[userVo.UserId.ToString() + "SeriesCategories"] != null)
       //             Cache.Remove(userVo.UserId.ToString() + "SeriesCategories");
       //         Cache.Insert(userVo.UserId.ToString() + "SeriesCategories", dtSeriesCategories);
       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindSeriesCategoryGrid()");
       //         object[] objects = new object[1];
       //         objects[1] = issuerid;
       //         objects[2] = issueid;
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }

       // protected void rgCategoriesDetails_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
       // {
       //     RadGrid rgCategoriesDetails = (RadGrid)sender; // Get reference to grid 
       //     GridDataItem nesteditem = (GridDataItem)rgCategoriesDetails.NamingContainer;
       //     int catgeoryId = Convert.ToInt32(rgSeries.MasterTableView.DataKeyValues[nesteditem.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString());
       //     DataTable dtCategoriesDetails = new DataTable();
       //     dtCategoriesDetails = onlineNCDBackOfficeBo.GetSubTypePerCategoryDetails(catgeoryId).Tables[0];
       //     rgCategoriesDetails.DataSource = dtCategoriesDetails;
       // }

       // private void BindCategoriesDetailsGrid(int categoryId, RadGrid rgCategoriesDetails)
       // {
       //     try
       //     {
       //         DataTable dtSeriesCategories = new DataTable();
       //         dtSeriesCategories = onlineNCDBackOfficeBo.GetCategoryDetails(categoryId).Tables[0];
       //         rgCategoriesDetails.DataSource = dtSeriesCategories;
       //         rgCategoriesDetails.DataBind();

       //         if (Cache[userVo.UserId.ToString() + "CategoriesDetails"] != null)
       //             Cache.Remove(userVo.UserId.ToString() + "CategoriesDetails");
       //         Cache.Insert(userVo.UserId.ToString() + "CategoriesDetails", dtSeriesCategories);
       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindSeriesCategoryGrid()");
       //         object[] objects = new object[1];
       //         objects[1] = categoryId;
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }

       // private void BindSeriesGrid(string issuerid, int issueid)
       // {
       //     try
       //     {
       //         DataTable dtSeries = new DataTable();
       //         dtSeries = onlineNCDBackOfficeBo.GetSeries(issuerid, issueid).Tables[0];
       //         rgSeries.DataSource = dtSeries;
       //         rgSeries.DataBind();
       //         if (Cache[userVo.UserId.ToString() + "Series"] != null)
       //             Cache.Remove(userVo.UserId.ToString() + "Series");
       //         Cache.Insert(userVo.UserId.ToString() + "Series", dtSeries);
       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindSeriesCategoryGrid()");
       //         object[] objects = new object[2];
       //         objects[1] = issuerid;
       //         objects[2] = issueid;
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }

       // private void BindCategory(RadGrid rgCategory, string issuerId, int issueId)
       // {
       //     try
       //     {
       //         DataTable dtCategory = new DataTable();
       //         dtCategory = onlineNCDBackOfficeBo.GetCategory(issuerId, issueId).Tables[0];
       //         rgCategory.DataSource = dtCategory;
       //         rgCategory.DataBind();
       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindCategory()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }

       // }

       // private void BindIssuer()
       // {
       //     try
       //     {
       //         DataSet dsIssuer = new DataSet();
       //         dsIssuer = onlineNCDBackOfficeBo.GetIssuer();
       //         if (dsIssuer.Tables[0].Rows.Count > 0)
       //         {
       //             ddlIssuer.DataSource = dsIssuer;
       //             ddlIssuer.DataValueField = dsIssuer.Tables[0].Columns["PFIIM_IssuerId"].ToString();
       //             ddlIssuer.DataTextField = dsIssuer.Tables[0].Columns["PFIIM_IssuerName"].ToString();
       //             ddlIssuer.DataBind();
       //         }
       //         ddlIssuer.Items.Insert(0, new ListItem("Select", "Select"));

       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindIssuer()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }

       // }

       // //private void BindFilterIssuer()
       // //{
       // //    try
       // //    {
       // //        DataSet dsIssuer = new DataSet();
       // //        dsIssuer = onlineNCDBackOfficeBo.GetIssuer();

       // //        if (dsIssuer.Tables[0].Rows.Count > 0)
       // //        {
       // //            ddlFilterIssuer.DataSource = dsIssuer;
       // //            ddlFilterIssuer.DataValueField = dsIssuer.Tables[0].Columns["PFIIM_IssuerId"].ToString();
       // //            ddlFilterIssuer.DataTextField = dsIssuer.Tables[0].Columns["PFIIM_IssuerName"].ToString();
       // //            ddlFilterIssuer.DataBind();
       // //        }
       // //        ddlFilterIssuer.Items.Insert(0, new ListItem("Select", "Select"));
       // //    }
       // //    catch (BaseApplicationException Ex)
       // //    {
       // //        throw Ex;
       // //    }
       // //    catch (Exception Ex)
       // //    {
       // //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       // //        NameValueCollection FunctionInfo = new NameValueCollection();
       // //        FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindIssuer()");
       // //        object[] objects = new object[0];
       // //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       // //        exBase.AdditionalInformation = FunctionInfo;
       // //        ExceptionManager.Publish(exBase);
       // //        throw exBase;
       // //    }

       // //}

       // private void BindFilterIssue(string issuerId)
       // {
       //     try
       //     {
       //         DataSet dsIssuer = new DataSet();
       //         dsIssuer = onlineNCDBackOfficeBo.GetIssuerIssue(issuerId);

       //         if (dsIssuer.Tables[0].Rows.Count > 0)
       //         {
       //             ddlFilterIssue.DataSource = dsIssuer;
       //             ddlFilterIssue.DataValueField = dsIssuer.Tables[0].Columns["AIM_IssueId"].ToString();
       //             ddlFilterIssue.DataTextField = dsIssuer.Tables[0].Columns["AIM_IssueName"].ToString();
       //             ddlFilterIssue.DataBind();
       //         }
       //         ddlFilterIssue.Items.Insert(0, new ListItem("Select", "Select"));

       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindIssuer()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }

       // }

       // private void BindEligibleInvestorsGrid(string issuerId, int issueId)
       // {
       //     try
       //     {
       //         DataTable dtInvestorCategories = new DataTable();
       //         dtInvestorCategories = onlineNCDBackOfficeBo.GetEligibleInvestorsCategory(issuerId, issueId).Tables[0];
       //         rgEligibleInvestorCategories.DataSource = dtInvestorCategories;
       //         rgEligibleInvestorCategories.DataBind();

       //         if (Cache[userVo.UserId.ToString() + "EligibleInvestors"] != null)
       //             Cache.Remove(userVo.UserId.ToString() + "EligibleInvestors");
       //         Cache.Insert(userVo.UserId.ToString() + "EligibleInvestors", dtInvestorCategories);

       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindEligibleInvestorsGrid()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }

       // private void BindSubCategoriesGrid(RadGrid rgSubCategory, string issuerId, int issueId)
       // {
       //     try
       //     {
       //         DataTable dtSubCategory = new DataTable();
       //         dtSubCategory = onlineNCDBackOfficeBo.GetSubCategory(issuerId, issueId).Tables[0];
       //         rgSubCategory.DataSource = dtSubCategory;
       //         rgSubCategory.DataBind();
       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindSubCategoriesGrid()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }

       // private void BindAllInvestorTypesForUpdatePopUpCategory(RadGrid rgSubCategory, string issuerId, int issueId)
       // {
       //     try
       //     {
       //         DataTable dtSubCategory = new DataTable();
       //         dtSubCategory = onlineNCDBackOfficeBo.GetAllInvestorTypes(issuerId, issueId).Tables[0];
       //         rgSubCategory.DataSource = dtSubCategory;
       //         rgSubCategory.DataBind();
       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindAllInvestorTypesForUpdatePopUpCategory()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }

       // protected void rgEligibleInvestorCategories_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
       // {
       //     DataTable dtEligibleInvestorCategories = new DataTable();
       //     dtEligibleInvestorCategories = (DataTable)Cache[userVo.UserId.ToString() + "EligibleInvestors"];

       //     if (dtEligibleInvestorCategories != null)
       //     {
       //         rgEligibleInvestorCategories.DataSource = dtEligibleInvestorCategories;
       //     }

       // }

       // protected void rgEligibleInvestorCategories_ItemDataBound(object sender, GridItemEventArgs e)
       // {
       //     try
       //     {

       //         if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
       //         {
       //             GridEditFormItem editform = (GridEditFormItem)e.Item;
       //             TextBox txtIssueName = (TextBox)editform.FindControl("txtIssueName");

       //             txtIssueName.Text = txtName.Text;
       //             RadGrid rgSubCategories = (RadGrid)editform.FindControl("rgSubCategories");
       //             BindSubCategoriesGrid(rgSubCategories, ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));
       //         }
       //         else if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode) && e.Item.ItemIndex != -1)
       //         {


       //             int categoryId = Convert.ToInt32(rgEligibleInvestorCategories.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString());



       //             TextBox txtCategoryName = (TextBox)e.Item.FindControl("txtCategoryName");
       //             TextBox txtCategoryDescription = (TextBox)e.Item.FindControl("txtCategoryDescription");
       //             TextBox txtChequePayableTo = (TextBox)e.Item.FindControl("txtChequePayableTo");
       //             TextBox txtMinBidAmount = (TextBox)e.Item.FindControl("txtMinBidAmount");
       //             TextBox txtMaxBidAmount = (TextBox)e.Item.FindControl("txtMaxBidAmount");
       //             RadGrid rgSubCategories = (RadGrid)e.Item.FindControl("rgSubCategories");
       //             TextBox txtSubCategoryCode = (TextBox)e.Item.FindControl("txtSubCategoryCode");
       //             TextBox txtMinInvestmentAmount = (TextBox)e.Item.FindControl("txtMinInvestmentAmount");
       //             TextBox txtMaxInvestmentAmount = (TextBox)e.Item.FindControl("txtMaxInvestmentAmount");
       //             CheckBox cbSubCategories = (CheckBox)e.Item.FindControl("cbSubCategories");



       //             BindAllInvestorTypesForUpdatePopUpCategory(rgSubCategories, ddlIssuer.SelectedValue, Convert.ToInt32(txtIssueId.Text));

       //             FillCategoryPopupControlsForUpdate(categoryId, txtCategoryName, txtCategoryDescription, txtChequePayableTo, txtMinBidAmount, txtMaxBidAmount, rgSubCategories);
       //             //, txtSubCategoryCode, txtMinInvestmentAmount, txtMaxInvestmentAmount, cbSubCategories);

       //         }



       //     }
       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:rgMaping_ItemDataBound()");
       //         object[] objects = new object[2];
       //         objects[1] = sender;
       //         objects[2] = e;
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }
       // }

       // private void FillCategoryPopupControlsForUpdate(int categoryId, TextBox txtCategoryName, TextBox txtCategoryDescription,
       //                TextBox txtChequePayableTo, TextBox txtMinBidAmount, TextBox txtMaxBidAmount, RadGrid rgSubCategories)
       // //, TextBox txtSubCategoryCode, TextBox txtMinInvestmentAmount, TextBox txtMaxInvestmentAmount, CheckBox cbSubCategories)
       // {

       //     try
       //     {
       //         DataTable dtCategory = new DataTable();
       //         dtCategory = onlineNCDBackOfficeBo.GetCategoryDetails(categoryId).Tables[0];

       //         if (dtCategory.Rows.Count > 0)
       //         {
       //             foreach (DataRow dr in dtCategory.Rows)
       //             {

       //                 txtCategoryName.Text = dr["AIIC_InvestorCatgeoryName"].ToString();
       //                 txtCategoryDescription.Text = dr["AIIC_InvestorCatgeoryDescription"].ToString();
       //                 txtChequePayableTo.Text = dr["AIIC_ChequePayableTo"].ToString();
       //                 txtMinBidAmount.Text = dr["AIIC_MInBidAmount"].ToString();
       //                 txtMaxBidAmount.Text = dr["AIIC_MaxBidAmount"].ToString();



       //                 int lookupId = Convert.ToInt32(dr["WCMV_LookupId"].ToString());


       //                 foreach (GridDataItem gdi in rgSubCategories.Items)
       //                 {
       //                     int grdLookupId = Convert.ToInt32(gdi["WCMV_LookupId"].Text);


       //                     if (grdLookupId == lookupId)
       //                     {
       //                         TextBox txtSubCategoryCode = ((TextBox)(gdi.FindControl("txtSubCategoryCode")));
       //                         TextBox txtMinInvestmentAmount = ((TextBox)(gdi.FindControl("txtMinInvestmentAmount")));
       //                         TextBox txtMaxInvestmentAmount = ((TextBox)(gdi.FindControl("txtMaxInvestmentAmount")));
       //                         CheckBox cbSubCategories = (CheckBox)gdi.FindControl("cbSubCategories");



       //                         cbSubCategories.Checked = true;
       //                         txtSubCategoryCode.Text = dr["AIIC_InvestorSubTypeCode"].ToString();
       //                         txtMinInvestmentAmount.Text = dr["AIIC_MinInvestmentAmount"].ToString();
       //                         txtMaxInvestmentAmount.Text = dr["AIIC_MaxInvestmentAmount"].ToString();
       //                     }
       //                 }


       //             }
       //         }
       //     }





       //     catch (BaseApplicationException Ex)
       //     {
       //         throw Ex;
       //     }
       //     catch (Exception Ex)
       //     {
       //         BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
       //         NameValueCollection FunctionInfo = new NameValueCollection();
       //         FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindCategory()");
       //         object[] objects = new object[0];
       //         FunctionInfo = exBase.AddObject(FunctionInfo, objects);
       //         exBase.AdditionalInformation = FunctionInfo;
       //         ExceptionManager.Publish(exBase);
       //         throw exBase;
       //     }




       // }


       // protected void lnkIssueNo_Click(object sender, EventArgs e)
       // {
       //     LinkButton lnkOrderNo = (LinkButton)sender;
       //     GridDataItem gdi;
       //     gdi = (GridDataItem)lnkOrderNo.NamingContainer;
       //     int selectedRow = gdi.ItemIndex + 1;
       //     int issueNo = int.Parse((gvIssueList.MasterTableView.DataKeyValues[selectedRow - 1]["AIM_IssueId"].ToString()));

       //     ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NCDIssuesetup", "loadcontrol('NCDIssuesetup','action=viewIsssueList & issueNo=" + issueNo + "');", true);

       // }




    }



}