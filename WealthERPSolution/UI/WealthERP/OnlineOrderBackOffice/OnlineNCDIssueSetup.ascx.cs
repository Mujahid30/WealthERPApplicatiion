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

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineNCDIssueSetup : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        OnlineNCDBackOfficeVo onlineNCDBackOfficeVo;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            int adviserId = advisorVo.advisorId;
            if (!IsPostBack)
            {
                BindIssuer();
                BindRTA();
                pnlSeries.Visible = false;
                pnlCategory.Visible = false;
                BindHours();
                BindMinutesAndSeconds();
                EnablityOfControlsonProductAndIssueTypeSelection("Select");
                if (Request.QueryString["action"] != null)
                {
                    int issueNo = Convert.ToInt32(Request.QueryString["issueNo"].ToString());
                    ViewIssueList(issueNo, advisorVo.advisorId);
                    VisblityAndEnablityOfScreen("View");
                }
                else
                {
                    VisblityAndEnablityOfScreen("New");
                }

            }

        }

        private void ViewIssueList(int issueNo, int adviserId)
        {
            try
            {
                DataTable dtSeries = new DataTable();
                dtSeries = onlineNCDBackOfficeBo.GetIssueDetails(issueNo, adviserId).Tables[0];

                foreach (DataRow dr in dtSeries.Rows)
                {
                    txtIssueId.Text = issueNo.ToString();

                    if (!string.IsNullOrEmpty(dr["PAIC_AssetInstrumentCategoryCode"].ToString()))
                    {
                        if (dr["PAIC_AssetInstrumentCategoryCode"].ToString() == "FIIP")
                        {
                            ddlProduct.SelectedValue = "IP";
                            if (!string.IsNullOrEmpty(dr["AIM_CapPrice"].ToString()))
                            {
                                ddlIssueType.SelectedValue = "BookBuilding";
                                txtCapPrice.Text = dr["AIM_CapPrice"].ToString();
                            }
                            else
                            {
                                txtCapPrice.Text = "";
                                ddlIssueType.SelectedValue = "FixedPrice";
                            }
                        }
                        else
                        {
                            if (dr["PAIC_AssetInstrumentCategoryCode"].ToString() == "FISD")
                            {
                                ddlCategory.SelectedValue = "NCD";
                                ddlProduct.SelectedValue = "NCD";
                            }
                            else if (dr["PAIC_AssetInstrumentCategoryCode"].ToString() == "FIIB")
                            {
                                ddlCategory.SelectedValue = "IB";
                                ddlProduct.SelectedValue = "NCD";

                            }
                            ddlIssueType.SelectedValue = "Select";
                        }
                    }
                    
                    //ddlProduct.SelectedValue =dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    EnablityOfControlsonProductAndIssueTypeSelection(ddlProduct.SelectedValue);
                    EnablityOfControlsonIssueTypeSelection(ddlIssueType.SelectedValue);

                   // ddlCategory.SelectedValue = "NCD";
                    txtName.Text = dr["AIM_IssueName"].ToString();
                    ddlIssuer.SelectedValue = dr["PI_issuerId"].ToString();
                    txtFormRange.Text = dr["AIFR_From"].ToString();
                    txtToRange.Text = dr["AIFR_To"].ToString();
                    txtInitialCqNo.Text = dr["AIM_InitialChequeNo"].ToString();
                    if (!string.IsNullOrEmpty(dr["AIM_ModeOfIssue"].ToString()))
                    {
                        ddlModeofIssue.SelectedValue = dr["AIM_ModeOfIssue"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["AIM_ModeOfTrading"].ToString()))
                    {
                        ddlModeOfTrading.SelectedValue = dr["AIM_ModeOfTrading"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_OpenDate"].ToString()))
                    {
                        txtOpenDate.SelectedDate = Convert.ToDateTime(dr["AIM_OpenDate"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_CloseDate"].ToString()))
                    {
                        txtCloseDate.SelectedDate = Convert.ToDateTime(dr["AIM_CloseDate"].ToString());
                    }


                    if (!string.IsNullOrEmpty(dr["AIM_AllotmentDate"].ToString()))
                    {
                        txtAllotmentDate.SelectedDate = Convert.ToDateTime(dr["AIM_AllotmentDate"].ToString());
                    }

                    

                    //string time                                                                                   txtOpenTimes.SelectedDate.Value.ToShortTimeString().ToString();
                    if (!string.IsNullOrEmpty(dr["AIM_OpenTime"].ToString()))
                    {
                        string opentime = dr["AIM_OpenTime"].ToString();
                        ddlOpenTimeHours.SelectedValue = opentime.Substring(0, 2);
                        ddlOpenTimeMinutes.SelectedValue = opentime.Substring(3, 2);
                        ddlOpenTimeSeconds.SelectedValue = opentime.Substring(6, 2);

                        //txtOpenTimes.Text = dr["AIM_OpenTime"].ToString(); ; //SelectedDate.Value.ToShortTimeString().ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["AIM_CloseTime"].ToString()))
                    {
                        string closetime = dr["AIM_OpenTime"].ToString();

                        ddlCloseTimeHours.SelectedValue = closetime.Substring(0, 2);
                        ddlCloseTimeMinutes.SelectedValue = closetime.Substring(3, 2);
                        ddlCloseTimeSeconds.SelectedValue = closetime.Substring(6, 2);

                        //txtCloseTimes.Text = dr["AIM_CloseTime"].ToString(); ; //SelectedDate.Value.ToShortTimeString().ToString();
                    }

                    txtRevisionDates.SelectedDate = DateTime.Now;

                    if (!string.IsNullOrEmpty(dr["AIM_TradingLot"].ToString()))
                    {
                        txtTradingLot.Text = dr["AIM_TradingLot"].ToString();
                    }
                    else
                    {
                        txtTradingLot.Text = "";
                    }

                    txtBiddingLot.Text = dr["AIM_BiddingLot"].ToString();
                    if (!string.IsNullOrEmpty(dr["AIM_IsPrefix"].ToString()))
                    {
                        txtIsPrefix.Text = dr["AIM_IsPrefix"].ToString();
                    }
                    else
                    {
                        txtIsPrefix.Text = "";
                    }

                   // ddlListedInExchange.SelectedValue = "";
                    ddlBankName.Text = "";
                    ddlBankBranch.Text = "";
                    if (!string.IsNullOrEmpty(dr["AIM_IsActive"].ToString()))
                    {
                        chkIsActive.Checked = true;
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_PutCallOption"].ToString()))
                    {
                        txtPutCallOption.Text = dr["AIM_PutCallOption"].ToString();
                    }
                    else
                    {
                        txtPutCallOption.Text = "";
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_IsNominationRequired"].ToString()))
                    {
                        chkNomineeReQuired.Checked = true;
                    }
                    if (!string.IsNullOrEmpty(dr["AIM_MinQty"].ToString()))
                    {
                        txtMinAplicSize.Text = dr["AIM_MinQty"].ToString();
                    }
                    else
                    {
                        txtMinAplicSize.Text = "";
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_TradingInMultipleOf"].ToString()))
                    {
                        txtTradingInMultipleOf.Text = dr["AIM_TradingInMultipleOf"].ToString();
                    }
                    else
                    {
                        txtTradingInMultipleOf.Text = "";
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_FloorPrice"].ToString()))
                    {
                        txtFloorPrice.Text = dr["AIM_FloorPrice"].ToString();
                    }
                    else
                    {
                        txtFloorPrice.Text = "";
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_FixedPrice"].ToString()))
                    {
                        txtFixedPrice.Text = dr["AIM_FixedPrice"].ToString();
                    }
                    else
                    {
                        txtFixedPrice.Text = "";
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_BookBuildingPercentage"].ToString()))
                    {
                        txtBookBuildingPer.Text = dr["AIM_BookBuildingPercentage"].ToString();
                    }
                    else
                    {
                        txtBookBuildingPer.Text = "";
                    }

                    
                    
                    if (!string.IsNullOrEmpty(dr["AIM_FaceValue"].ToString()))
                    {
                        txtFaceValue.Text = dr["AIM_FaceValue"].ToString();
                    }
                    else
                    {
                        txtFaceValue.Text = "";
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_Rating"].ToString()))
                    {
                        txtRating.Text = dr["AIM_Rating"].ToString();
                    }
                    else
                    {
                        txtRating.Text = "";
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_NoOfBidAllowed"].ToString()))
                    {
                        txtNoOfBids.Text = dr["AIM_NoOfBidAllowed"].ToString();
                    }
                    else
                    {
                        txtNoOfBids.Text = "";
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_IssueSizeQty"].ToString()))
                    {
                        txtIssueSizeQty.Text = dr["AIM_IssueSizeQty"].ToString();
                    }
                    else
                    {
                        txtIssueSizeQty.Text = "";
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_IssueSizeAmt"].ToString()))
                    {
                        txtIssueSizeAmt.Text = dr["AIM_IssueSizeAmt"].ToString();
                    }
                    else
                    {
                        txtIssueSizeAmt.Text = "";
                    }
                    if (!string.IsNullOrEmpty(dr["AIM_NSECode"].ToString()))
                    {
                        txtNSECode.Text = dr["AIM_NSECode"].ToString();
                    }
                    else
                    {
                        txtNSECode.Text = "";
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_BSECode"].ToString()))
                    {
                        txtBSECode.Text = dr["AIM_BSECode"].ToString();
                    }
                    else
                    {
                        txtBSECode.Text = "";

                    }


                    

                    SeriesAndCategoriesGridsVisiblity(Convert.ToInt32(ddlIssuer.SelectedValue), issueNo);
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindSeriesCategoryGrid()");
                object[] objects = new object[2];
                objects[1] = issueNo;
                objects[2] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void VisblityAndEnablityOfScreen(string Mode)
        {
            if (Mode == "New")
            {
                EnablityOfScreen(true, false, false, false);

            }
            else if (Mode == "Submited")
            {
                //After Submit
                EnablityOfScreen(false, true, true, false);
            }
            else if (Mode == "View")
            {
                EnablityOfScreen(false, false, true, true);
            }
            else if (Mode == "LnkEdit")
            {
                EnablityOfScreen(true, true, true, true);
            }
            else if (Mode == "AfterUpdate")
            {
                //After Update
                EnablityOfScreen(false, true, false, false);
            }
        }

        private void EnablityOfScreen(bool value, bool boolGridsEnablity, bool boolGridsVisblity, bool boolBtnsVisblity)
        {

            ddlProduct.Enabled = value;
            ddlCategory.Enabled = value;

            txtName.Enabled = value;
            ddlIssuer.Enabled = value;

            txtFormRange.Enabled = value;
            txtToRange.Enabled = value;

            txtInitialCqNo.Enabled = value;
            txtFaceValue.Enabled = value;

            txtPrice.Enabled = value;
            ddlModeofIssue.Enabled = value;

            txtRating.Enabled = value;
            ddlModeOfTrading.Enabled = value;

            txtOpenDate.Enabled = value;
            txtCloseDate.Enabled = value;

            ddlOpenTimeHours.Enabled = value;
            ddlOpenTimeMinutes.Enabled = value;
            ddlOpenTimeSeconds.Enabled = value;

            //txtCloseTimes.Enabled = value;
            ddlCloseTimeHours.Enabled = value;
            ddlCloseTimeMinutes.Enabled = value;
            ddlCloseTimeSeconds.Enabled = value;
            txtRevisionDates.Enabled = value;

            txtTradingLot.Enabled = value;
            txtBiddingLot.Enabled = value;



            txtTradingInMultipleOf.Enabled = value;
            //ddlListedInExchange.Enabled = value;

            ddlBankName.Enabled = value;
            ddlBankBranch.Enabled = value;

            txtPutCallOption.Enabled = value;

            txtMinAplicSize.Enabled = value;
            txtIsPrefix.Enabled = value;

            chkIsActive.Enabled = value;
            chkNomineeReQuired.Enabled = value;

            pnlSeries.Enabled = boolGridsEnablity;
            pnlCategory.Enabled = boolGridsEnablity;

            btnSetUpSubmit.Enabled = value;


            pnlSeries.Visible = boolGridsVisblity;
            pnlCategory.Visible = boolGridsVisblity;

            btnSetUpSubmit.Visible = !boolBtnsVisblity;
            btnUpdate.Visible = boolBtnsVisblity;
            lnkBtnEdit.Visible = boolBtnsVisblity;
            lnlBack.Visible = boolBtnsVisblity;
            lnkDelete.Visible = boolBtnsVisblity;


            if (ddlProduct.SelectedValue == "IP")
            {
                pnlSeries.Visible = false;
                //trMaxQty.Visible = false;
            }
            else if (ddlProduct.SelectedValue == "NCD")
            {
                pnlSeries.Visible = true;
                //trMaxQty.Visible = true;
            }


        }


        private int UpdateIssue()
        {
            int issueId;
            try
            {
                onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                onlineNCDBackOfficeVo.AssetGroupCode = ddlProduct.SelectedValue;
                if (ddlProduct.SelectedValue == "IPO")
                {
                    onlineNCDBackOfficeVo.AssetInstrumentCategoryCode = "IP";
                }
                else
                {
                    onlineNCDBackOfficeVo.AssetInstrumentCategoryCode = "NCD";// ddlCategory.SelectedValue;
                }

                onlineNCDBackOfficeVo.IssueName = txtName.Text;
                onlineNCDBackOfficeVo.IssuerId = Convert.ToInt32(ddlIssuer.SelectedValue);

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
                if (!string.IsNullOrEmpty(txtFloorPrice.Text))
                {
                    onlineNCDBackOfficeVo.FloorPrice = Convert.ToDouble(txtFloorPrice.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.FloorPrice = 0;
                }
                onlineNCDBackOfficeVo.ModeOfIssue = ddlModeofIssue.SelectedValue;

                if (!string.IsNullOrEmpty(txtRating.Text))
                {
                    onlineNCDBackOfficeVo.Rating = txtRating.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.Rating = "";
                }
                onlineNCDBackOfficeVo.ModeOfTrading = ddlModeOfTrading.SelectedValue;

                onlineNCDBackOfficeVo.OpenDate = txtOpenDate.SelectedDate.Value;
                onlineNCDBackOfficeVo.CloseDate = txtCloseDate.SelectedDate.Value;


                //string time = txtOpenTimes.SelectedDate.Value.ToShortTimeString().ToString();
                onlineNCDBackOfficeVo.OpenTime = Convert.ToDateTime(ddlOpenTimeHours.SelectedValue + ":" + ddlOpenTimeMinutes.SelectedValue + ":" + ddlOpenTimeSeconds.SelectedValue); //SelectedDate.Value.ToShortTimeString().ToString();
                onlineNCDBackOfficeVo.CloseTime = Convert.ToDateTime(ddlCloseTimeHours.SelectedValue + ":" + ddlCloseTimeMinutes.SelectedValue + ":" + ddlCloseTimeSeconds.SelectedValue);//SelectedDate.Value.ToShortTimeString().ToString();

                if (!string.IsNullOrEmpty((txtRevisionDates.SelectedDate).ToString().Trim()))
                    onlineNCDBackOfficeVo.IssueRevis = DateTime.Parse(txtRevisionDates.SelectedDate.ToString());
                else
                    onlineNCDBackOfficeVo.IssueRevis = DateTime.MinValue;

                onlineNCDBackOfficeVo.TradingLot = Convert.ToDecimal(txtTradingLot.Text);
                onlineNCDBackOfficeVo.BiddingLot = Convert.ToDecimal(txtBiddingLot.Text);

                onlineNCDBackOfficeVo.MinApplicationSize = Convert.ToInt32(txtMinAplicSize.Text);
                if (!string.IsNullOrEmpty(txtIsPrefix.Text))
                {
                    onlineNCDBackOfficeVo.IsPrefix = Convert.ToInt32(txtIsPrefix.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.IsPrefix = 0;
                }

                onlineNCDBackOfficeVo.TradingInMultipleOf = Convert.ToInt32(txtTradingInMultipleOf.Text);

                //if (!string.IsNullOrEmpty(ddlListedInExchange.SelectedValue))
                //{
                //    onlineNCDBackOfficeVo.ListedInExchange = ddlListedInExchange.SelectedValue;
                //}
                //else
                //{
                //    onlineNCDBackOfficeVo.ListedInExchange = "";
                //}


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

                if (!string.IsNullOrEmpty(txtBookBuildingPer.Text))
                {
                    onlineNCDBackOfficeVo.BookBuildingPercentage = Convert.ToInt32(txtBookBuildingPer.Text);
                    onlineNCDBackOfficeVo.IsBookBuilding = 1;
                }
                else
                {
                    onlineNCDBackOfficeVo.BookBuildingPercentage = 0;
                    onlineNCDBackOfficeVo.IsBookBuilding = 0;
                }
                if (!string.IsNullOrEmpty(txtCapPrice.Text))
                {
                    onlineNCDBackOfficeVo.CapPrice = Convert.ToDouble(txtCapPrice.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.CapPrice = 0;
                }

                if (!string.IsNullOrEmpty(txtFixedPrice.Text))
                {
                    onlineNCDBackOfficeVo.FixedPrice = Convert.ToInt32(txtFixedPrice.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.FixedPrice = 0;
                }
                if (!string.IsNullOrEmpty(txtSyndicateMemberCode.Text))
                {
                    onlineNCDBackOfficeVo.SyndicateMemberCode = txtSyndicateMemberCode.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.SyndicateMemberCode = "";
                }
                if (!string.IsNullOrEmpty(txtBrokerCode.Text))
                {
                    onlineNCDBackOfficeVo.BrokerCode = txtBrokerCode.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.BrokerCode = "";
                }

                if (!string.IsNullOrEmpty(txtNoOfBids.Text))
                {
                    onlineNCDBackOfficeVo.NoOfBidAllowed = Convert.ToInt32(txtNoOfBids.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.NoOfBidAllowed = 0;
                }

                if (!string.IsNullOrEmpty(ddlRegistrar.Text))
                {
                    onlineNCDBackOfficeVo.RtaSourceCode = ddlRegistrar.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.RtaSourceCode = "";
                }


                if (!string.IsNullOrEmpty(txtIssueSizeQty.Text))
                {
                    onlineNCDBackOfficeVo.IssueSizeQty = Convert.ToInt32(txtIssueSizeQty.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.IssueSizeQty = 0;
                }

                if (!string.IsNullOrEmpty(txtIssueSizeAmt.Text))
                {
                    onlineNCDBackOfficeVo.IssueSizeAmt = Convert.ToDecimal(txtIssueSizeAmt.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.IssueSizeAmt = 0;
                }



                if (!string.IsNullOrEmpty(txtNSECode.Text))
                {
                    onlineNCDBackOfficeVo.IsListedinNSE = 1;
                    onlineNCDBackOfficeVo.NSECode = txtNSECode.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.NSECode = "";
                }

                if (!string.IsNullOrEmpty(txtBSECode.Text))
                {
                    onlineNCDBackOfficeVo.IsListedinBSE = 1;
                    onlineNCDBackOfficeVo.BSECode = txtBSECode.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.BSECode = "";

                }








                if (!string.IsNullOrEmpty(txtMaxQty.Text))
                {
                    onlineNCDBackOfficeVo.MaxQty = Convert.ToInt32(txtMaxQty.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.MaxQty = 0;
                }


                issueId = onlineNCDBackOfficeBo.UpdateIssue(onlineNCDBackOfficeVo);
                if (issueId > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Issue Updated successfully.');", true);
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateNewWerpName()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return issueId;
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
            dtCategory = onlineNCDBackOfficeBo.GetCategory(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text)).Tables[0];
            rgSeriesCategories1.DataSource = dtCategory;
        }

        //protected void rgSeries_UpdateCommand(object source, GridCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName == RadGrid.UpdateCommandName)
        //        {
        //            int availblity;
        //            TextBox txtSereiesName = (TextBox)e.Item.FindControl("txtSereiesName");
        //            TextBox txtTenure = (TextBox)e.Item.FindControl("txtTenure");
        //            DropDownList ddlTenure = (DropDownList)e.Item.FindControl("ddlTenure");
        //            TextBox txtInterestFrequency = (TextBox)e.Item.FindControl("txtInterestFrequency");
        //            CheckBox chkBuyAvailability = (CheckBox)e.Item.FindControl("chkBuyAvailability");
        //            DropDownList ddlInterestType = (DropDownList)e.Item.FindControl("ddlInterestType");
        //            if (chkBuyAvailability.Checked == true)
        //            {
        //                availblity = 1;
        //            }
        //            else
        //            {
        //                availblity = 0;
        //            }
        //            RadGrid rgSeriesCat = (RadGrid)e.Item.FindControl("rgSeriesCat");

        //            foreach (GridDataItem gdi in rgSeriesCat.Items)
        //            {
        //                if (((CheckBox)gdi.FindControl("cbSeriesCat")).Checked == true)
        //                {
        //                    int categoryId = Convert.ToInt32(gdi["AIIC_InvestorCatgeoryId"].Text);
        //                    TextBox txtInterestRate = ((TextBox)(gdi.FindControl("txtInterestRate")));
        //                    TextBox txtAnnualizedYield = ((TextBox)(gdi.FindControl("txtAnnualizedYield")));
        //                }
        //            }
        //        }
        //        BindSeriesGrid(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:rgSeries_UpdateCommand()");
        //        object[] objects = new object[2];
        //        objects[1] = source;
        //        objects[2] = e;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        private int CreateUpdateDeleteSeries(int issueId, int seriesId, string seriesName, int isBuyBackAvailable, int tenure, string interestFrequency,
         string interestType, int SeriesSequence, string CommandType)
        {
            int result = 0;

            if (CommandType == "Insert")
            {
                onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                onlineNCDBackOfficeVo.IssueId = issueId;
                onlineNCDBackOfficeVo.SeriesName = seriesName.Trim();
                onlineNCDBackOfficeVo.IsBuyBackAvailable = isBuyBackAvailable;
                onlineNCDBackOfficeVo.Tenure = tenure;
                onlineNCDBackOfficeVo.InterestFrequency = interestFrequency;
                onlineNCDBackOfficeVo.InterestType = interestType;
                onlineNCDBackOfficeVo.SeriesSequence = SeriesSequence;
                result = onlineNCDBackOfficeBo.CreateSeries(onlineNCDBackOfficeVo, userVo.UserId);
            }
            else if (CommandType == "Update")
            {
                onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                onlineNCDBackOfficeVo.SeriesId = seriesId;
                onlineNCDBackOfficeVo.SeriesName = seriesName.Trim();
                onlineNCDBackOfficeVo.IsBuyBackAvailable = isBuyBackAvailable;
                onlineNCDBackOfficeVo.Tenure = tenure;
                onlineNCDBackOfficeVo.InterestFrequency = interestFrequency;
                onlineNCDBackOfficeVo.InterestType = interestType;
                onlineNCDBackOfficeVo.SeriesSequence = SeriesSequence;
                result = onlineNCDBackOfficeBo.UpdateSeries(onlineNCDBackOfficeVo, userVo.UserId);

            }
            else if (CommandType == "Delete")
            {
                onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                onlineNCDBackOfficeVo.IssueId = issueId;
                onlineNCDBackOfficeVo.SeriesName = seriesName.Trim();
                onlineNCDBackOfficeVo.IsBuyBackAvailable = isBuyBackAvailable;
                onlineNCDBackOfficeVo.Tenure = tenure;
                onlineNCDBackOfficeVo.InterestFrequency = interestFrequency;
                onlineNCDBackOfficeVo.InterestType = interestType;
            }
            return result;

        }

        //private int CreateSeries(int issueId, string seriesName, int isBuyBackAvailable, int tenure, string interestFrequency,
        //  string interestType)
        //{
        //    try
        //    {
        //        onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
        //        onlineNCDBackOfficeVo.IssueId = issueId;
        //        onlineNCDBackOfficeVo.SeriesName = seriesName.Trim();
        //        onlineNCDBackOfficeVo.IsBuyBackAvailable = isBuyBackAvailable;
        //        onlineNCDBackOfficeVo.Tenure = tenure;
        //        onlineNCDBackOfficeVo.InterestFrequency = interestFrequency;
        //        onlineNCDBackOfficeVo.InterestType = interestType;
        //        return onlineNCDBackOfficeBo.CreateSeries(onlineNCDBackOfficeVo, userVo.UserId);
        //    }

        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateSeries()");
        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        protected void rgSeries_ItemCommand(object source, GridCommandEventArgs e)
        {
            int count = 0;
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                int availblity;
                TextBox txtSereiesName = (TextBox)e.Item.FindControl("txtSereiesName");
                TextBox txtTenure = (TextBox)e.Item.FindControl("txtTenure");
                DropDownList ddlTenure = (DropDownList)e.Item.FindControl("ddlTenure");
                TextBox txtInterestFrequency = (TextBox)e.Item.FindControl("txtInterestFrequency");
                CheckBox chkBuyAvailability = (CheckBox)e.Item.FindControl("chkBuyAvailability");
                DropDownList ddlInterestType = (DropDownList)e.Item.FindControl("ddlInterestType");
                TextBox txtSequence = (TextBox)e.Item.FindControl("txtSequence");
                RadGrid rgSeriesCat = (RadGrid)e.Item.FindControl("rgSeriesCat");

                foreach (GridDataItem gdi in rgSeriesCat.Items)
                {
                    if (((CheckBox)gdi.FindControl("cbSeriesCat")).Checked == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Select One Category.');", true);
                    return;
                }

                if (chkBuyAvailability.Checked == true)
                {
                    availblity = 1;
                }
                else
                {
                    availblity = 0;
                }
                if (string.IsNullOrEmpty(txtTenure.Text))
                {
                    txtTenure.Text = 0.ToString();
                }


                int dupSeqNo = onlineNCDBackOfficeBo.ChekSeriesSequence(Convert.ToInt32(txtSequence.Text), Convert.ToInt32(txtIssueId.Text), advisorVo.advisorId);
                if (dupSeqNo != 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Seq No exist.');", true);
                    return;
                }

                int seriesId = CreateUpdateDeleteSeries(Convert.ToInt32(txtIssueId.Text), 0, txtSereiesName.Text, availblity, Convert.ToInt32(txtTenure.Text), txtInterestFrequency.Text, ddlInterestType.SelectedValue, Convert.ToInt32(txtSequence.Text), "Insert");

                foreach (GridDataItem gdi in rgSeriesCat.Items)
                {
                    if (((CheckBox)gdi.FindControl("cbSeriesCat")).Checked == true)
                    {
                        int categoryId = Convert.ToInt32(gdi["AIIC_InvestorCatgeoryId"].Text);
                        TextBox txtInterestRate = ((TextBox)(gdi.FindControl("txtInterestRate")));
                        TextBox txtAnnualizedYield = ((TextBox)(gdi.FindControl("txtAnnualizedYield")));

                        if (string.IsNullOrEmpty(txtInterestRate.Text))
                        {
                            txtInterestRate.Text = 0.ToString();
                        }

                        if (string.IsNullOrEmpty(txtAnnualizedYield.Text))
                        {
                            txtAnnualizedYield.Text = 0.ToString();
                        }
                        CreateUpdateDeleteSeriesCategories(seriesId, categoryId, Convert.ToDouble(txtInterestRate.Text), Convert.ToDouble(txtAnnualizedYield.Text), "Insert");
                    }
                }
                BindSeriesGrid(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));

            }
            else if (e.CommandName == RadGrid.UpdateCommandName)
            {
                int availblity;
                TextBox txtSereiesName = (TextBox)e.Item.FindControl("txtSereiesName");
                TextBox txtTenure = (TextBox)e.Item.FindControl("txtTenure");
                DropDownList ddlTenure = (DropDownList)e.Item.FindControl("ddlTenure");
                TextBox txtInterestFrequency = (TextBox)e.Item.FindControl("txtInterestFrequency");
                CheckBox chkBuyAvailability = (CheckBox)e.Item.FindControl("chkBuyAvailability");
                DropDownList ddlInterestType = (DropDownList)e.Item.FindControl("ddlInterestType");
                TextBox txtSequence = (TextBox)e.Item.FindControl("txtSequence");

                if (chkBuyAvailability.Checked == true)
                {
                    availblity = 1;
                }
                else
                {
                    availblity = 0;
                }
                if (string.IsNullOrEmpty(txtTenure.Text))
                {
                    txtTenure.Text = 0.ToString();
                }
                if (string.IsNullOrEmpty(txtSequence.Text))
                {
                    txtSequence.Text = 0.ToString();
                }
                int seriesId = Convert.ToInt32(rgSeries.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AID_IssueDetailId"].ToString());
                int InsseriesId = CreateUpdateDeleteSeries(Convert.ToInt32(txtIssueId.Text), seriesId, txtSereiesName.Text, availblity, Convert.ToInt32(txtTenure.Text), txtInterestFrequency.Text, ddlInterestType.SelectedValue, Convert.ToInt32(txtSequence.Text), "Update");
                RadGrid rgSeriesCat = (RadGrid)e.Item.FindControl("rgSeriesCat");
                foreach (GridDataItem gdi in rgSeriesCat.Items)
                {
                    if (((CheckBox)gdi.FindControl("cbSeriesCat")).Checked == true)
                    {
                        int categoryId = Convert.ToInt32(gdi["AIIC_InvestorCatgeoryId"].Text);
                        TextBox txtInterestRate = ((TextBox)(gdi.FindControl("txtInterestRate")));
                        TextBox txtAnnualizedYield = ((TextBox)(gdi.FindControl("txtAnnualizedYield")));
                        if (string.IsNullOrEmpty(txtInterestRate.Text))
                        {
                            txtInterestRate.Text = 0.ToString();
                        }

                        if (string.IsNullOrEmpty(txtAnnualizedYield.Text))
                        {
                            txtAnnualizedYield.Text = 0.ToString();
                        }
                        CreateUpdateDeleteSeriesCategories(seriesId, categoryId, Convert.ToDouble(txtInterestRate.Text), Convert.ToDouble(txtAnnualizedYield.Text), "Update");
                    }
                }
                BindSeriesGrid(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));
            }
            else if (e.CommandName == RadGrid.DeleteCommandName)
            {

            }

        }

        protected void rgSeries_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridNestedViewItem)
            {
                (e.Item.FindControl("rgSeriesCategories") as RadGrid).NeedDataSource += new GridNeedDataSourceEventHandler(rgSeriesCategories_OnNeedDataSource);
            }
        }

        protected void rgEligibleInvestorCategories_ItemCommand(object source, GridCommandEventArgs e)
        {
            string description = string.Empty;
            string discountType = string.Empty;
            int count = 0;
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                int categoryId;
                TextBox txtCategoryName = (TextBox)e.Item.FindControl("txtCategoryName");
                TextBox txtCategoryDescription = (TextBox)e.Item.FindControl("txtCategoryDescription");
                TextBox txtChequePayableTo = (TextBox)e.Item.FindControl("txtChequePayableTo");
                TextBox txtMinBidAmount = (TextBox)e.Item.FindControl("txtMinBidAmount");
                TextBox txtMaxBidAmount = (TextBox)e.Item.FindControl("txtMaxBidAmount");
                DropDownList ddlDiscountType = (DropDownList)e.Item.FindControl("ddlDiscountType");
                TextBox txtDiscountValue = (TextBox)e.Item.FindControl("txtDiscountValue");
                RadGrid rgSubCategories = (RadGrid)e.Item.FindControl("rgSubCategories");

                foreach (GridDataItem gdi in rgSubCategories.Items)
                {
                    if (((CheckBox)gdi.FindControl("cbSubCategories")).Checked == true)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Select One Category.');", true);
                    return;
                }

                if (string.IsNullOrEmpty(txtMinBidAmount.Text))
                {
                    txtMinBidAmount.Text = 0.ToString();
                }

                if (string.IsNullOrEmpty(txtMaxBidAmount.Text))
                {
                    txtMaxBidAmount.Text = 0.ToString();
                }

                if (ddlDiscountType.SelectedValue == "Per")
                {
                    discountType = "PE";
                }
                else if (ddlDiscountType.SelectedValue == "Amt")
                {
                    discountType = "AM";
                }
                if (txtDiscountValue.Text == "")
                {
                    txtDiscountValue.Text = 0.ToString();
                }

                categoryId = CreateUpdateDeleteCategory(Convert.ToInt32(txtIssueId.Text), 0, txtCategoryName.Text, txtCategoryDescription.Text, txtChequePayableTo.Text, Convert.ToInt32(txtMinBidAmount.Text), Convert.ToInt32(txtMaxBidAmount.Text), discountType, Convert.ToDecimal(txtDiscountValue.Text), "Insert");

                foreach (GridDataItem gdi in rgSubCategories.Items)
                {
                    if (((CheckBox)gdi.FindControl("cbSubCategories")).Checked == true)
                    {
                        int lookupId = Convert.ToInt32(gdi["WCMV_LookupId"].Text);
                        TextBox txtSubCategoryCode = ((TextBox)(gdi.FindControl("txtSubCategoryCode")));
                        TextBox txtMinInvestmentAmount = ((TextBox)(gdi.FindControl("txtMinInvestmentAmount")));
                        TextBox txtMaxInvestmentAmount = ((TextBox)(gdi.FindControl("txtMaxInvestmentAmount")));
                        if (string.IsNullOrEmpty(txtMinInvestmentAmount.Text))
                        {
                            txtMinInvestmentAmount.Text = 0.ToString();
                        }

                        if (string.IsNullOrEmpty(txtMaxInvestmentAmount.Text))
                        {
                            txtMaxInvestmentAmount.Text = 0.ToString();
                        }
                        CreateUpdateDeleteCategoryDetails(categoryId, lookupId, txtSubCategoryCode.Text, Convert.ToInt32(txtMinInvestmentAmount.Text), Convert.ToInt32(txtMaxInvestmentAmount.Text), "Insert");
                    }
                }
                BindEligibleInvestorsGrid(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));
            }
            else if (e.CommandName == RadGrid.UpdateCommandName)
            {
                int categoryId;
                int result;

                categoryId = Convert.ToInt32(rgEligibleInvestorCategories.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString());
                TextBox txtCategoryName = (TextBox)e.Item.FindControl("txtCategoryName");
                TextBox txtCategoryDescription = (TextBox)e.Item.FindControl("txtCategoryDescription");
                TextBox txtChequePayableTo = (TextBox)e.Item.FindControl("txtChequePayableTo");
                TextBox txtMinBidAmount = (TextBox)e.Item.FindControl("txtMinBidAmount");
                TextBox txtMaxBidAmount = (TextBox)e.Item.FindControl("txtMaxBidAmount");
                DropDownList ddlDiscountType = (DropDownList)e.Item.FindControl("ddlDiscountType");
                TextBox txtDiscountValue = (TextBox)e.Item.FindControl("txtDiscountValue");
                if (string.IsNullOrEmpty(txtMinBidAmount.Text))
                {
                    txtMinBidAmount.Text = 0.ToString();
                }

                if (string.IsNullOrEmpty(txtMaxBidAmount.Text))
                {
                    txtMaxBidAmount.Text = 0.ToString();
                }
                if (ddlDiscountType.SelectedValue == "Per")
                {
                    discountType = "PE";
                }
                else if (ddlDiscountType.SelectedValue == "Amt")
                {
                    discountType = "AM";
                }
                if (txtDiscountValue.Text == "")
                {
                    txtDiscountValue.Text = 0.ToString();
                }
                result = CreateUpdateDeleteCategory(0, categoryId, txtCategoryName.Text, txtCategoryDescription.Text, txtChequePayableTo.Text, Convert.ToInt32(txtMinBidAmount.Text), Convert.ToInt32(txtMaxBidAmount.Text), discountType, Convert.ToDecimal(txtDiscountValue.Text), "Update");
                RadGrid rgSubCategories = (RadGrid)e.Item.FindControl("rgSubCategories");
                foreach (GridDataItem gdi in rgSubCategories.Items)
                {
                    if (((CheckBox)gdi.FindControl("cbSubCategories")).Checked == true)
                    {
                        int lookupId = Convert.ToInt32(gdi["WCMV_LookupId"].Text);
                        TextBox txtSubCategoryCode = ((TextBox)(gdi.FindControl("txtSubCategoryCode")));
                        TextBox txtMinInvestmentAmount = ((TextBox)(gdi.FindControl("txtMinInvestmentAmount")));
                        TextBox txtMaxInvestmentAmount = ((TextBox)(gdi.FindControl("txtMaxInvestmentAmount")));
                        if (string.IsNullOrEmpty(txtMinInvestmentAmount.Text))
                        {
                            txtMinInvestmentAmount.Text = 0.ToString();
                        }

                        if (string.IsNullOrEmpty(txtMaxInvestmentAmount.Text))
                        {
                            txtMaxInvestmentAmount.Text = 0.ToString();
                        }
                        CreateUpdateDeleteCategoryDetails(categoryId, lookupId, txtSubCategoryCode.Text, Convert.ToInt32(txtMinInvestmentAmount.Text), Convert.ToInt32(txtMaxInvestmentAmount.Text), "Update");
                    }
                }

                BindEligibleInvestorsGrid(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));

            }
            else if (e.CommandName == RadGrid.DeleteCommandName)
            {
            }
        }

        //protected void rgEligibleInvestorCategories_UpdateCommand(object source, GridCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName == RadGrid.UpdateCommandName)
        //        {
        //            int categoryId;
        //            TextBox txtCategoryName = (TextBox)e.Item.FindControl("txtCategoryName");
        //            TextBox txtCategoryDescription = (TextBox)e.Item.FindControl("txtCategoryDescription");
        //            TextBox txtChequePayableTo = (TextBox)e.Item.FindControl("txtChequePayableTo");
        //            TextBox txtMinBidAmount = (TextBox)e.Item.FindControl("txtMinBidAmount");
        //            TextBox txtMaxBidAmount = (TextBox)e.Item.FindControl("txtMaxBidAmount");
        //            if (string.IsNullOrEmpty(txtMinBidAmount.Text))
        //            {
        //                txtMinBidAmount.Text = 0.ToString();
        //            }

        //            if (string.IsNullOrEmpty(txtMaxBidAmount.Text))
        //            {
        //                txtMaxBidAmount.Text = 0.ToString();
        //            }
        //            categoryId = CreateCategory(Convert.ToInt32(txtIssueId.Text), txtCategoryName.Text, txtCategoryDescription.Text, txtChequePayableTo.Text, Convert.ToInt32(txtMinBidAmount.Text), Convert.ToInt32(txtMaxBidAmount.Text));
        //            RadGrid rgSubCategories = (RadGrid)e.Item.FindControl("rgSubCategories");
        //            foreach (GridDataItem gdi in rgSubCategories.Items)
        //            {
        //                if (((CheckBox)gdi.FindControl("cbSubCategories")).Checked == true)
        //                {
        //                    int lookupId = Convert.ToInt32(gdi["WCMV_LookupId"].Text);
        //                    TextBox txtSubCategoryCode = ((TextBox)(gdi.FindControl("txtSubCategoryCode")));
        //                    TextBox txtMinInvestmentAmount = ((TextBox)(gdi.FindControl("txtMinInvestmentAmount")));
        //                    TextBox txtMaxInvestmentAmount = ((TextBox)(gdi.FindControl("txtMaxInvestmentAmount")));
        //                    if (string.IsNullOrEmpty(txtMinInvestmentAmount.Text))
        //                    {
        //                        txtMinInvestmentAmount.Text = 0.ToString();
        //                    }

        //                    if (string.IsNullOrEmpty(txtMaxInvestmentAmount.Text))
        //                    {
        //                        txtMaxInvestmentAmount.Text = 0.ToString();
        //                    }
        //                    CreateSubTypePerCategory(categoryId, lookupId, txtSubCategoryCode.Text, Convert.ToInt32(txtMinInvestmentAmount.Text), Convert.ToInt32(txtMaxInvestmentAmount.Text));
        //                }
        //            }
        //            BindEligibleInvestorsGrid(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));
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
        //        FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:rgEligibleInvestorCategories_UpdateCommand()");
        //        object[] objects = new object[2];
        //        objects[1] = source;
        //        objects[2] = e;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        private int CreateUpdateDeleteCategory(int issueId, int categoryId, string investorCatgeoryName, string investorCatgeoryDescription, string chequePayableTo,
           int mInBidAmount, int maxBidAmount, string discountType, decimal discountValue, string CommandType)
        {
            int result = 0;
            try
            {
                if (CommandType == "Insert")
                {
                    onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                    onlineNCDBackOfficeVo.IssueId = issueId;
                    onlineNCDBackOfficeVo.CatgeoryName = investorCatgeoryName;
                    onlineNCDBackOfficeVo.CatgeoryDescription = investorCatgeoryDescription;
                    onlineNCDBackOfficeVo.ChequePayableTo = chequePayableTo;
                    onlineNCDBackOfficeVo.MInBidAmount = mInBidAmount;
                    onlineNCDBackOfficeVo.MaxBidAmount = maxBidAmount;
                    onlineNCDBackOfficeVo.DiscuountType = discountType;
                    onlineNCDBackOfficeVo.DiscountValue = discountValue;

                    return onlineNCDBackOfficeBo.CreateCategory(onlineNCDBackOfficeVo, userVo.UserId);
                }
                else if (CommandType == "Update")
                {
                    onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                    //onlineNCDBackOfficeVo.issueId = issueId;
                    onlineNCDBackOfficeVo.CatgeoryId = categoryId;
                    onlineNCDBackOfficeVo.CatgeoryName = investorCatgeoryName;
                    onlineNCDBackOfficeVo.CatgeoryDescription = investorCatgeoryDescription;
                    onlineNCDBackOfficeVo.ChequePayableTo = chequePayableTo;
                    onlineNCDBackOfficeVo.MInBidAmount = mInBidAmount;
                    onlineNCDBackOfficeVo.MaxBidAmount = maxBidAmount;
                    onlineNCDBackOfficeVo.DiscuountType = discountType;
                    onlineNCDBackOfficeVo.DiscountValue = discountValue;
                    result = onlineNCDBackOfficeBo.UpdateCategory(onlineNCDBackOfficeVo, userVo.UserId);
                }
                else if (CommandType == "Delete")
                {
                    onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                    onlineNCDBackOfficeVo.IssueId = issueId;
                    onlineNCDBackOfficeVo.CatgeoryName = investorCatgeoryName;
                    onlineNCDBackOfficeVo.CatgeoryDescription = investorCatgeoryDescription;
                    onlineNCDBackOfficeVo.ChequePayableTo = chequePayableTo;
                    onlineNCDBackOfficeVo.MInBidAmount = mInBidAmount;
                    onlineNCDBackOfficeVo.MaxBidAmount = maxBidAmount;
                }
                return result;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateUpdateDeleteCategory()");
                object[] objects = new object[0];
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateNewWerpName()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void CreateUpdateDeleteCategoryDetails(int catgeoryId, int lookUpId, string subTypeCode, int minInvestmentAmount, int maxInvestmentAmount, string CommandType)
        {
            bool result = false;
            try
            {
                if (CommandType == "Insert")
                {
                    onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                    onlineNCDBackOfficeVo.CatgeoryId = catgeoryId;
                    onlineNCDBackOfficeVo.LookUpId = lookUpId;
                    onlineNCDBackOfficeVo.SubCatgeoryTypeCode = subTypeCode;
                    onlineNCDBackOfficeVo.MinInvestmentAmount = minInvestmentAmount;
                    onlineNCDBackOfficeVo.MaxInvestmentAmount = maxInvestmentAmount;
                    result = onlineNCDBackOfficeBo.CreateSubTypePerCategory(onlineNCDBackOfficeVo, userVo.UserId);
                }
                else if (CommandType == "Update")
                {
                    onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                    onlineNCDBackOfficeVo.CatgeoryId = catgeoryId;
                    onlineNCDBackOfficeVo.LookUpId = lookUpId;
                    onlineNCDBackOfficeVo.SubCatgeoryTypeCode = subTypeCode;
                    onlineNCDBackOfficeVo.MinInvestmentAmount = minInvestmentAmount;
                    onlineNCDBackOfficeVo.MaxInvestmentAmount = maxInvestmentAmount;
                    result = onlineNCDBackOfficeBo.UpdateCategoryDetails(onlineNCDBackOfficeVo, userVo.UserId);

                }
                else if (CommandType == "Delete")
                {
                    onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                    onlineNCDBackOfficeVo.CatgeoryId = catgeoryId;
                    onlineNCDBackOfficeVo.LookUpId = lookUpId;
                    onlineNCDBackOfficeVo.SubCatgeoryTypeCode = subTypeCode;
                    onlineNCDBackOfficeVo.MinInvestmentAmount = minInvestmentAmount;
                    onlineNCDBackOfficeVo.MaxInvestmentAmount = maxInvestmentAmount;
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateNewWerpName()");
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateNewWerpName()");
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
                if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
                {
                    GridEditFormItem editform = (GridEditFormItem)e.Item;
                    RadGrid rgSeriesCat = (RadGrid)editform.FindControl("rgSeriesCat");
                    BindCategory(rgSeriesCat, Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));

                }
                else if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    GridEditFormItem editform = (GridEditFormItem)e.Item;
                    int seriesId = Convert.ToInt32(rgSeries.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AID_IssueDetailId"].ToString());
                    TextBox txtSereiesName = (TextBox)e.Item.FindControl("txtSereiesName");
                    TextBox txtTenure = (TextBox)e.Item.FindControl("txtTenure");
                    TextBox txtInterestFrequency = (TextBox)e.Item.FindControl("txtInterestFrequency");
                    CheckBox chkBuyAvailability = (CheckBox)e.Item.FindControl("chkBuyAvailability");
                    TextBox txtSequence = (TextBox)e.Item.FindControl("txtSequence");

                    RadGrid rgSeriesCat = (RadGrid)editform.FindControl("rgSeriesCat");
                    BindCategory(rgSeriesCat, Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));
                    FillSeriesPopupControlsForUpdate(seriesId, txtSereiesName, txtTenure, txtInterestFrequency, chkBuyAvailability, txtSequence, rgSeriesCat);
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:rgSeries_ItemDataBound()");
                object[] objects = new object[2];
                objects[1] = sender;
                objects[2] = e;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void FillSeriesPopupControlsForUpdate(int seriesId, TextBox txtSereiesName, TextBox txtTenure,
                         TextBox txtInterestFrequency, CheckBox chkBuyAvailability, TextBox txtSequence, RadGrid rgSeriesCat)
        {
            int seriesCategoryId = 0;
            try
            {
                DataTable dtCategory = new DataTable();
                dtCategory = onlineNCDBackOfficeBo.GetSeriesInvestorTypeRule(seriesId).Tables[0];

                if (dtCategory.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCategory.Rows)
                    {
                        txtSereiesName.Text = dr["AID_IssueDetailName"].ToString();
                        txtTenure.Text = dr["AID_Tenure"].ToString();
                        txtInterestFrequency.Text = dr["AID_InterestFrequency"].ToString();
                        chkBuyAvailability.Checked = Convert.ToBoolean(dr["AID_BuyBackFacility"].ToString());
                        txtSequence.Text = dr["AID_Sequence"].ToString();
                        if (!string.IsNullOrEmpty(dr["AIIC_InvestorCatgeoryId"].ToString()))
                        {
                            seriesCategoryId = Convert.ToInt32(dr["AIIC_InvestorCatgeoryId"].ToString());
                        }
                        else
                        {
                            return;
                        }


                        foreach (GridDataItem gdi in rgSeriesCat.Items)
                        {
                            int grdcategoryId = Convert.ToInt32(gdi["AIIC_InvestorCatgeoryId"].Text);
                            if (seriesCategoryId == grdcategoryId)
                            {
                                CheckBox cbSeriesCat = (CheckBox)gdi.FindControl("cbSeriesCat");
                                TextBox txtInterestRate = ((TextBox)(gdi.FindControl("txtInterestRate")));
                                TextBox txtAnnualizedYield = ((TextBox)(gdi.FindControl("txtAnnualizedYield")));

                                cbSeriesCat.Checked = true;
                                txtInterestRate.Text = dr["AIDCSR_DefaultInterestRate"].ToString();
                                txtAnnualizedYield.Text = dr["AIDCSR_AnnualizedYieldUpto"].ToString();
                            }
                        }
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindCategory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void btnSetUpSubmit_Click(object sender, EventArgs e)
        {
            txtIssueId.Text = CreateIssue().ToString();
            SeriesAndCategoriesGridsVisiblity(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));
            VisblityAndEnablityOfScreen("Submited");


        }


        protected void btnProspect_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ManageRepository", "loadcontrol('ManageRepository','action=ManageRepository&issueId=1');", true);

        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int result = UpdateIssue();
            //VisblityAndEnablityOfScreen("AfterUpdate");
            SeriesAndCategoriesGridsVisiblity(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));
        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            VisblityAndEnablityOfScreen("LnkEdit");

        }

        protected void lnlBack_Click(object sender, EventArgs e)
        {
            string type = "";
            string date = "";
            string product = "";
            if (Request.QueryString["action"] != null)
            {
                type = Request.QueryString["type"].ToString();
                date = Request.QueryString["date"].ToString();
                product = Request.QueryString["product"].ToString();
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineNCDIssueList", "loadcontrol('OnlineNCDIssueList','action=viewIsssueList&type=" + type + "&date=" + date + "&product=" + product + "');", true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineNCDIssueList", "loadcontrol('OnlineNCDIssueList','action=viewIsssueList&type="+type + "&date="+date+"'');", true);
        }

        private void SeriesAndCategoriesGridsVisiblity(int issuerId, int issueId)
        {
            pnlSeries.Visible = true;
            pnlCategory.Visible = true;
            BindSeriesGrid(issuerId, issueId);
            BindEligibleInvestorsGrid(issuerId, issueId);
        }

        private int CreateIssue()
        {
            int issueId;
            try
            {
                onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                onlineNCDBackOfficeVo.AssetGroupCode = ddlProduct.SelectedValue;
                if (ddlProduct.SelectedValue == "IP")
                {
                    onlineNCDBackOfficeVo.AssetInstrumentCategoryCode = "FIIP";
                    onlineNCDBackOfficeVo.AssetInstrumentSubCategoryCode = "FIIP";
                }
                else
                {
                    if (ddlCategory.SelectedValue == "NCD")
                    {
                        onlineNCDBackOfficeVo.AssetInstrumentCategoryCode = "FISD";
                        onlineNCDBackOfficeVo.AssetInstrumentSubCategoryCode = "FINCD";
                    }
                    else if (ddlCategory.SelectedValue == "IB")
                    {
                        onlineNCDBackOfficeVo.AssetInstrumentCategoryCode = "FIIB";
                        onlineNCDBackOfficeVo.AssetInstrumentSubCategoryCode = "FIIB";
                    }

                }

                onlineNCDBackOfficeVo.IssueName = txtName.Text;
                onlineNCDBackOfficeVo.IssuerId = Convert.ToInt32(ddlIssuer.SelectedValue);

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
                if (!string.IsNullOrEmpty(txtFloorPrice.Text))
                {
                    onlineNCDBackOfficeVo.FloorPrice = Convert.ToDouble(txtFloorPrice.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.FloorPrice = 0;
                }
                onlineNCDBackOfficeVo.ModeOfIssue = ddlModeofIssue.SelectedValue;

                if (!string.IsNullOrEmpty(txtRating.Text))
                {
                    onlineNCDBackOfficeVo.Rating = txtRating.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.Rating = "";
                }
                onlineNCDBackOfficeVo.ModeOfTrading = ddlModeOfTrading.SelectedValue;

                onlineNCDBackOfficeVo.OpenDate = txtOpenDate.SelectedDate.Value;
                onlineNCDBackOfficeVo.CloseDate = txtCloseDate.SelectedDate.Value;

                if (ddlOpenTimeMinutes.SelectedValue == "MM")
                {
                    ddlOpenTimeMinutes.SelectedValue = "00";
                }
                if (ddlCloseTimeMinutes.SelectedValue == "MM")
                {
                    ddlCloseTimeMinutes.SelectedValue = "00";
                }
                if (ddlOpenTimeSeconds.SelectedValue == "SS")
                {
                    ddlOpenTimeSeconds.SelectedValue = "00";
                }
                if (ddlCloseTimeSeconds.SelectedValue == "SS")
                {
                    ddlCloseTimeSeconds.SelectedValue = "00";
                }
                //string time = txtOpenTimes.SelectedDate.Value.ToShortTimeString().ToString();
                onlineNCDBackOfficeVo.OpenTime = Convert.ToDateTime(ddlOpenTimeHours.SelectedValue + ":" + ddlOpenTimeMinutes.SelectedValue + ":" + ddlOpenTimeSeconds.SelectedValue); //SelectedDate.Value.ToShortTimeString().ToString();
                onlineNCDBackOfficeVo.CloseTime = Convert.ToDateTime(ddlCloseTimeHours.SelectedValue + ":" + ddlCloseTimeMinutes.SelectedValue + ":" + ddlCloseTimeSeconds.SelectedValue);//SelectedDate.Value.ToShortTimeString().ToString();

                if (!string.IsNullOrEmpty((txtRevisionDates.SelectedDate).ToString().Trim()))
                    onlineNCDBackOfficeVo.IssueRevis = DateTime.Parse(txtRevisionDates.SelectedDate.ToString());
                else
                    onlineNCDBackOfficeVo.IssueRevis = DateTime.MinValue;

                onlineNCDBackOfficeVo.TradingLot = Convert.ToDecimal(txtTradingLot.Text);
                onlineNCDBackOfficeVo.BiddingLot = Convert.ToDecimal(txtBiddingLot.Text);

                onlineNCDBackOfficeVo.MinApplicationSize = Convert.ToInt32(txtMinAplicSize.Text);
                if (!string.IsNullOrEmpty(txtIsPrefix.Text))
                {
                    onlineNCDBackOfficeVo.IsPrefix = Convert.ToInt32(txtIsPrefix.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.IsPrefix = 0;
                }

                onlineNCDBackOfficeVo.TradingInMultipleOf = Convert.ToInt32(txtTradingInMultipleOf.Text);

                //if (!string.IsNullOrEmpty(ddlListedInExchange.SelectedValue))
                //{
                //    onlineNCDBackOfficeVo.ListedInExchange = ddlListedInExchange.SelectedValue;
                //}
                //else
                //{
                //    onlineNCDBackOfficeVo.ListedInExchange = "";
                //}


                if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
                {
                    onlineNCDBackOfficeVo.BankName = ddlBankName.SelectedValue;
                }
                else
                {
                    onlineNCDBackOfficeVo.BankName = "";
                }

                if (!string.IsNullOrEmpty(ddlBankBranch.SelectedValue))
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
                //if (ddlListedInExchange.SelectedValue == "BSE")
                //{
                //    onlineNCDBackOfficeVo.BSECode = txtNcdBsnCode.Text;
                //    onlineNCDBackOfficeVo.BSECode = "";
                //}
                //else if (ddlListedInExchange.SelectedValue == "NSE")
                //{
                //    onlineNCDBackOfficeVo.NSECode = txtNcdBsnCode.Text;
                //    onlineNCDBackOfficeVo.NSECode = "";
                //}
                //else
                //{
                //    onlineNCDBackOfficeVo.NSECode = "";
                //    onlineNCDBackOfficeVo.BSECode = "";
                //}
                if (!string.IsNullOrEmpty(txtBookBuildingPer.Text))
                {
                    onlineNCDBackOfficeVo.BookBuildingPercentage = Convert.ToInt32(txtBookBuildingPer.Text);
                    onlineNCDBackOfficeVo.IsBookBuilding = 1;
                }
                else
                {
                    onlineNCDBackOfficeVo.BookBuildingPercentage = 0;
                    onlineNCDBackOfficeVo.IsBookBuilding = 0;
                }
                if (!string.IsNullOrEmpty(txtCapPrice.Text))
                {
                    onlineNCDBackOfficeVo.CapPrice = Convert.ToDouble(txtCapPrice.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.CapPrice = 0;
                }

                if (!string.IsNullOrEmpty(txtFixedPrice.Text))
                {
                    onlineNCDBackOfficeVo.FixedPrice = Convert.ToInt32(txtFixedPrice.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.FixedPrice = 0;
                }
                if (!string.IsNullOrEmpty(txtSyndicateMemberCode.Text))
                {
                    onlineNCDBackOfficeVo.SyndicateMemberCode = txtSyndicateMemberCode.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.SyndicateMemberCode = "";
                }
                if (!string.IsNullOrEmpty(txtBrokerCode.Text))
                {
                    onlineNCDBackOfficeVo.BrokerCode = txtBrokerCode.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.BrokerCode = "";
                }

                if (!string.IsNullOrEmpty(txtNoOfBids.Text))
                {
                    onlineNCDBackOfficeVo.NoOfBidAllowed = Convert.ToInt32(txtNoOfBids.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.NoOfBidAllowed = 0;
                }

                //if (!string.IsNullOrEmpty(txtRegistrar.Text))
                //{
                //    onlineNCDBackOfficeVo.RtaSourceCode = txtRegistrar.Text;
                //}
                //else
                //{
                //    onlineNCDBackOfficeVo.RtaSourceCode = "";
                //}

                if (!string.IsNullOrEmpty(txtMaxQty.Text))
                {
                    onlineNCDBackOfficeVo.MaxQty = Convert.ToInt32(txtMaxQty.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.MaxQty = 0;
                }


                if (!string.IsNullOrEmpty(ddlRegistrar.Text))
                {
                    onlineNCDBackOfficeVo.RtaSourceCode = ddlRegistrar.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.RtaSourceCode = "";
                }


                if (!string.IsNullOrEmpty(txtIssueSizeQty.Text))
                {
                    onlineNCDBackOfficeVo.IssueSizeQty = Convert.ToInt32(txtIssueSizeQty.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.IssueSizeQty = 0;
                }

                if (!string.IsNullOrEmpty(txtIssueSizeAmt.Text))
                {
                    onlineNCDBackOfficeVo.IssueSizeAmt = Convert.ToDecimal(txtIssueSizeAmt.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.IssueSizeAmt = 0;
                }


                if (!string.IsNullOrEmpty(txtNSECode.Text))
                {
                    onlineNCDBackOfficeVo.IsListedinNSE = 1;
                    onlineNCDBackOfficeVo.NSECode = txtNSECode.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.NSECode = "";
                }

                if (!string.IsNullOrEmpty(txtBSECode.Text))
                {
                    onlineNCDBackOfficeVo.IsListedinBSE = 1;
                    onlineNCDBackOfficeVo.BSECode = txtBSECode.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.BSECode = "";

                }


                issueId = onlineNCDBackOfficeBo.CreateIssue(onlineNCDBackOfficeVo, advisorVo.advisorId);
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateIssue()");
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

        private void CreateUpdateDeleteSeriesCategories(int seriesId, int catgeoryId, double defaultInterestRate, double annualizedYieldUpto, string commandType)
        {
            bool result;
            try
            {
                if (commandType == "Insert")
                {
                    onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                    onlineNCDBackOfficeVo.SeriesId = seriesId;
                    onlineNCDBackOfficeVo.CatgeoryId = catgeoryId;
                    onlineNCDBackOfficeVo.DefaultInterestRate = defaultInterestRate;
                    onlineNCDBackOfficeVo.AnnualizedYieldUpto = annualizedYieldUpto;
                    result = onlineNCDBackOfficeBo.CreateSeriesCategory(onlineNCDBackOfficeVo, userVo.UserId);
                }
                else if (commandType == "Update")
                {
                    onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                    onlineNCDBackOfficeVo.SeriesId = seriesId;
                    onlineNCDBackOfficeVo.CatgeoryId = catgeoryId;
                    onlineNCDBackOfficeVo.DefaultInterestRate = defaultInterestRate;
                    onlineNCDBackOfficeVo.AnnualizedYieldUpto = annualizedYieldUpto;
                    result = onlineNCDBackOfficeBo.UpdateSeriesCategory(onlineNCDBackOfficeVo, userVo.UserId);
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateNewWerpName()");
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
            seriesId = Convert.ToInt32(rgSeries.MasterTableView.DataKeyValues[gdi.ItemIndex]["AID_IssueDetailId"].ToString());
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
            BindSeriesCategoryGrid(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text), seriesId, rgSeriesCategories);
        }

        protected void btnCategoriesExpandAll_Click(object sender, EventArgs e)
        {
            int categoryId = 0;
            LinkButton buttonlink = (LinkButton)sender;
            GridDataItem gdi = (GridDataItem)buttonlink.NamingContainer;
            if (!string.IsNullOrEmpty(rgEligibleInvestorCategories.MasterTableView.DataKeyValues[gdi.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString()))
            {
                categoryId = int.Parse(rgEligibleInvestorCategories.MasterTableView.DataKeyValues[gdi.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString());
            }
            else
            {
                categoryId = 0;
            }
            RadGrid rgCategoriesDetails = (RadGrid)gdi.FindControl("rgCategoriesDetails");
            Panel pnlCategoriesDetailschild = (Panel)gdi.FindControl("pnlCategoriesDetailschild");

            if (pnlCategoriesDetailschild.Visible == false)
            {
                pnlCategoriesDetailschild.Visible = true;
                buttonlink.Text = "-";
            }
            else if (pnlCategoriesDetailschild.Visible == true)
            {
                pnlCategoriesDetailschild.Visible = false;
                buttonlink.Text = "+";
            }
            BindCategoriesDetailsGrid(categoryId, rgCategoriesDetails);
        }

        protected void rgSubCategories_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid rgSubCategories = (RadGrid)sender; // Get reference to grid 
            DataTable dtSubCategory = new DataTable();
            dtSubCategory = onlineNCDBackOfficeBo.GetSubCategory(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text)).Tables[0];
            rgSubCategories.DataSource = dtSubCategory;
        }

        protected void rgSeriesCategories_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid rgSeriesCategories1 = (RadGrid)sender; // Get reference to grid 
            GridDataItem nesteditem = (GridDataItem)rgSeriesCategories1.NamingContainer;
            int seriesId = Convert.ToInt32(rgSeries.MasterTableView.DataKeyValues[nesteditem.ItemIndex]["AID_IssueDetailId"].ToString());
            DataTable dtSeriesCategories = new DataTable();
            dtSeriesCategories = onlineNCDBackOfficeBo.GetSeriesCategories(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text), seriesId).Tables[0];
            rgSeriesCategories1.DataSource = dtSeriesCategories;
        }

        private void BindSeriesCategoryGrid(int issuerId, int issueId, int seriesId, RadGrid rgSeriesCategories)
        {
            try
            {
                DataTable dtSeriesCategories = new DataTable();
                dtSeriesCategories = onlineNCDBackOfficeBo.GetSeriesCategories(issuerId, issueId, seriesId).Tables[0];
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindSeriesCategoryGrid()");
                object[] objects = new object[1];
                objects[1] = issuerId;
                objects[2] = issueId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void rgCategoriesDetails_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid rgCategoriesDetails = (RadGrid)sender; // Get reference to grid 
            GridDataItem nesteditem = (GridDataItem)rgCategoriesDetails.NamingContainer;
            int catgeoryId = Convert.ToInt32(rgSeries.MasterTableView.DataKeyValues[nesteditem.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString());
            DataTable dtCategoriesDetails = new DataTable();
            dtCategoriesDetails = onlineNCDBackOfficeBo.GetSubTypePerCategoryDetails(catgeoryId).Tables[0];
            rgCategoriesDetails.DataSource = dtCategoriesDetails;
        }

        private void BindCategoriesDetailsGrid(int categoryId, RadGrid rgCategoriesDetails)
        {
            try
            {
                DataTable dtSeriesCategories = new DataTable();
                dtSeriesCategories = onlineNCDBackOfficeBo.GetCategoryDetails(categoryId).Tables[0];
                rgCategoriesDetails.DataSource = dtSeriesCategories;
                rgCategoriesDetails.DataBind();

                if (Cache[userVo.UserId.ToString() + "CategoriesDetails"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "CategoriesDetails");
                Cache.Insert(userVo.UserId.ToString() + "CategoriesDetails", dtSeriesCategories);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindSeriesCategoryGrid()");
                object[] objects = new object[1];
                objects[1] = categoryId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindSeriesGrid(int issuerId, int issueId)
        {
            try
            {
                DataTable dtSeries = new DataTable();
                dtSeries = onlineNCDBackOfficeBo.GetSeries(issuerId, issueId).Tables[0];
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindSeriesGrid()");
                object[] objects = new object[2];
                objects[1] = issuerId;
                objects[2] = issueId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindCategory(RadGrid rgCategory, int issuerId, int issueId)
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindCategory()");
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
                    ddlIssuer.DataValueField = dsIssuer.Tables[0].Columns["PI_issuerId"].ToString();
                    ddlIssuer.DataTextField = dsIssuer.Tables[0].Columns["PI_IssuerName"].ToString();
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindIssuer()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        //private void BindFilterIssuer()
        //{
        //    try
        //    {
        //        DataSet dsIssuer = new DataSet();
        //        dsIssuer = onlineNCDBackOfficeBo.GetIssuer();

        //        if (dsIssuer.Tables[0].Rows.Count > 0)
        //        {
        //            ddlFilterIssuer.DataSource = dsIssuer;
        //            ddlFilterIssuer.DataValueField = dsIssuer.Tables[0].Columns["PI_issuerId"].ToString();
        //            ddlFilterIssuer.DataTextField = dsIssuer.Tables[0].Columns["PFIIM_IssuerName"].ToString();
        //            ddlFilterIssuer.DataBind();
        //        }
        //        ddlFilterIssuer.Items.Insert(0, new ListItem("Select", "Select"));
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindIssuer()");
        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //}

        //private void BindFilterIssue(int  issuerId)
        //{
        //    try
        //    {
        //        DataSet dsIssuer = new DataSet();
        //        dsIssuer = onlineNCDBackOfficeBo.GetIssuerIssue(issuerId);

        //        if (dsIssuer.Tables[0].Rows.Count > 0)
        //        {
        //            ddlFilterIssue.DataSource = dsIssuer;
        //            ddlFilterIssue.DataValueField = dsIssuer.Tables[0].Columns["AIM_IssueId"].ToString();
        //            ddlFilterIssue.DataTextField = dsIssuer.Tables[0].Columns["AIM_IssueName"].ToString();
        //            ddlFilterIssue.DataBind();
        //        }
        //        ddlFilterIssue.Items.Insert(0, new ListItem("Select", "Select"));

        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindIssuer()");
        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //}

        private void BindEligibleInvestorsGrid(int issuerId, int issueId)
        {
            try
            {
                DataTable dtInvestorCategories = new DataTable();
                dtInvestorCategories = onlineNCDBackOfficeBo.GetEligibleInvestorsCategory(issuerId, issueId).Tables[0];
                rgEligibleInvestorCategories.DataSource = dtInvestorCategories;
                rgEligibleInvestorCategories.DataBind();

                if (Cache[userVo.UserId.ToString() + "EligibleInvestors"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "EligibleInvestors");
                Cache.Insert(userVo.UserId.ToString() + "EligibleInvestors", dtInvestorCategories);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindEligibleInvestorsGrid()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindSubCategoriesGrid(RadGrid rgSubCategory, int issuerId, int issueId)
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindSubCategoriesGrid()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindAllInvestorTypesForUpdatePopUpCategory(RadGrid rgSubCategory, int issuerId, int issueId)
        {
            try
            {
                DataTable dtSubCategory = new DataTable();
                dtSubCategory = onlineNCDBackOfficeBo.GetAllInvestorTypes(issuerId, issueId).Tables[0];
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindAllInvestorTypesForUpdatePopUpCategory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void rgEligibleInvestorCategories_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtEligibleInvestorCategories = new DataTable();
            dtEligibleInvestorCategories = (DataTable)Cache[userVo.UserId.ToString() + "EligibleInvestors"];

            if (dtEligibleInvestorCategories != null)
            {
                rgEligibleInvestorCategories.DataSource = dtEligibleInvestorCategories;
            }

        }

        protected void rgIssuer_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode) && e.Item.ItemIndex != -1)
            {
                GridEditFormItem editform = (GridEditFormItem)e.Item;
                string issuerId = rgIssuer.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PI_IssuerId"].ToString() ;

                TextBox txtIssuerName = (TextBox)editform.FindControl("txtIssuerName");
                TextBox txtIssuerCode = (TextBox)editform.FindControl("txtIssuerCode");
                DataTable dtIssuer = new DataTable();
                dtIssuer = onlineNCDBackOfficeBo.GetIssuer().Tables[0];
                DataTable tbl = (from DataRow dr in dtIssuer.Rows
                                 where dr["PI_IssuerId"].ToString() == issuerId
                                 select dr).CopyToDataTable();


                foreach (DataRow dr in tbl.Rows)
                {
                    txtIssuerName.Text = dr["PI_IssuerName"].ToString();
                    txtIssuerCode.Text = dr["PI_IssuerCode"].ToString();
                }
               
            }
            //if (e.CommandName == RadGrid.DeleteCommandName)
            //{

            //    OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
            //    GridDataItem dataItem = (GridDataItem)e.Item;
            //    int TradeBusinessId = Convert.ToInt32(gvTradeBusinessDate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WTBD_DayName"].ToString());
            //}
        }
        protected void rgEligibleInvestorCategories_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
                {
                    GridEditFormItem editform = (GridEditFormItem)e.Item;
                    TextBox txtIssueName = (TextBox)editform.FindControl("txtIssueName");
                    txtIssueName.Text = txtName.Text;

                    if (ddlProduct.SelectedValue == "IPO")
                    {
                        System.Web.UI.HtmlControls.HtmlTableRow trDiscountType = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trDiscountType");
                        System.Web.UI.HtmlControls.HtmlTableRow trDiscountValue = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trDiscountValue");

                        trDiscountType.Visible = true;
                        trDiscountValue.Visible = true;
                    }
                    else
                    {
                        System.Web.UI.HtmlControls.HtmlTableRow trDiscountType = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trDiscountType");
                        System.Web.UI.HtmlControls.HtmlTableRow trDiscountValue = (System.Web.UI.HtmlControls.HtmlTableRow)editform.FindControl("trDiscountValue");

                        trDiscountType.Visible = false;
                        trDiscountValue.Visible = false;
                    }
                    RadGrid rgSubCategories = (RadGrid)editform.FindControl("rgSubCategories");
                    BindSubCategoriesGrid(rgSubCategories, Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));
                }
                else if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode) && e.Item.ItemIndex != -1)
                {
                    int categoryId = Convert.ToInt32(rgEligibleInvestorCategories.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString());
                    TextBox txtIssueName = (TextBox)e.Item.FindControl("txtIssueName");
                    txtIssueName.Text = txtName.Text;
                    TextBox txtCategoryName = (TextBox)e.Item.FindControl("txtCategoryName");
                    TextBox txtCategoryDescription = (TextBox)e.Item.FindControl("txtCategoryDescription");
                    TextBox txtChequePayableTo = (TextBox)e.Item.FindControl("txtChequePayableTo");
                    TextBox txtMinBidAmount = (TextBox)e.Item.FindControl("txtMinBidAmount");
                    TextBox txtMaxBidAmount = (TextBox)e.Item.FindControl("txtMaxBidAmount");
                    RadGrid rgSubCategories = (RadGrid)e.Item.FindControl("rgSubCategories");
                    TextBox txtSubCategoryCode = (TextBox)e.Item.FindControl("txtSubCategoryCode");
                    TextBox txtMinInvestmentAmount = (TextBox)e.Item.FindControl("txtMinInvestmentAmount");
                    TextBox txtMaxInvestmentAmount = (TextBox)e.Item.FindControl("txtMaxInvestmentAmount");
                    CheckBox cbSubCategories = (CheckBox)e.Item.FindControl("cbSubCategories");
                    //
                    TextBox txtDiscountValue = (TextBox)e.Item.FindControl("txtDiscountValue");
                    DropDownList ddlDiscountType = (DropDownList)e.Item.FindControl("ddlDiscountType");

                    if (ddlProduct.SelectedValue == "IPO")
                    {
                        System.Web.UI.HtmlControls.HtmlTableRow trDiscountType = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trDiscountType");
                        System.Web.UI.HtmlControls.HtmlTableRow trDiscountValue = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trDiscountValue");

                        trDiscountType.Visible = true;
                        trDiscountValue.Visible = true;
                    }
                    else
                    {
                        System.Web.UI.HtmlControls.HtmlTableRow trDiscountType = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trDiscountType");
                        System.Web.UI.HtmlControls.HtmlTableRow trDiscountValue = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("trDiscountValue");

                        trDiscountType.Visible = false;
                        trDiscountValue.Visible = false;
                    }
                    //if (string.IsNullOrEmpty(ddlDiscountType.SelectedValue))
                    //{
                    //    ddlDiscountType = "";
                    //}

                    BindAllInvestorTypesForUpdatePopUpCategory(rgSubCategories, Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));
                    FillCategoryPopupControlsForUpdate(categoryId, txtCategoryName, txtCategoryDescription, txtChequePayableTo, txtMinBidAmount, txtMaxBidAmount, rgSubCategories, txtDiscountValue, ddlDiscountType);
                    //, txtSubCategoryCode, txtMinInvestmentAmount, txtMaxInvestmentAmount, cbSubCategories);
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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:rgEligibleInvestorCategories_ItemDataBound()");
                object[] objects = new object[2];
                objects[1] = sender;
                objects[2] = e;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void FillCategoryPopupControlsForUpdate(int categoryId, TextBox txtCategoryName, TextBox txtCategoryDescription,
                       TextBox txtChequePayableTo, TextBox txtMinBidAmount, TextBox txtMaxBidAmount, RadGrid rgSubCategories, TextBox txtDiscountValue, DropDownList ddlDiscountType)
        //, TextBox txtSubCategoryCode, TextBox txtMinInvestmentAmount, TextBox txtMaxInvestmentAmount, CheckBox cbSubCategories)
        {
            int lookupId = 0;
            try
            {
                DataTable dtCategory = new DataTable();
                dtCategory = onlineNCDBackOfficeBo.GetCategoryDetails(categoryId).Tables[0];
                if (dtCategory.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCategory.Rows)
                    {
                        txtCategoryName.Text = dr["AIIC_InvestorCatgeoryName"].ToString();
                        txtCategoryDescription.Text = dr["AIIC_InvestorCatgeoryDescription"].ToString();
                        txtChequePayableTo.Text = dr["AIIC_ChequePayableTo"].ToString();
                        txtMinBidAmount.Text = dr["AIIC_MInBidAmount"].ToString();
                        txtMaxBidAmount.Text = dr["AIIC_MaxBidAmount"].ToString();
                        if (!string.IsNullOrEmpty(dr["AIIC_PriceDiscountValue"].ToString()))
                        {
                            txtDiscountValue.Text = dr["AIIC_PriceDiscountValue"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["AIIC_PriceDiscountType"].ToString()))
                        {
                            if (dr["AIIC_PriceDiscountType"].ToString() == "PE")
                            {
                                ddlDiscountType.SelectedValue = "Per";
                            }
                            else
                            {
                                ddlDiscountType.SelectedValue = "Amt";
                            }
                        }

                        lookupId = Convert.ToInt32(dr["WCMV_LookupId"].ToString());

                        if (!string.IsNullOrEmpty(dr["WCMV_LookupId"].ToString()))
                        {
                            lookupId = Convert.ToInt32(dr["WCMV_LookupId"].ToString());
                        }
                        else
                        {
                            return;
                        }


                        foreach (GridDataItem gdi in rgSubCategories.Items)
                        {
                            int grdLookupId = Convert.ToInt32(gdi["WCMV_LookupId"].Text);
                            if (grdLookupId == lookupId)
                            {
                                TextBox txtSubCategoryCode = ((TextBox)(gdi.FindControl("txtSubCategoryCode")));
                                TextBox txtMinInvestmentAmount = ((TextBox)(gdi.FindControl("txtMinInvestmentAmount")));
                                TextBox txtMaxInvestmentAmount = ((TextBox)(gdi.FindControl("txtMaxInvestmentAmount")));
                                CheckBox cbSubCategories = (CheckBox)gdi.FindControl("cbSubCategories");
                                cbSubCategories.Checked = true;
                                txtSubCategoryCode.Text = dr["AIICST_InvestorSubTypeCode"].ToString();
                                txtMinInvestmentAmount.Text = dr["AIICST_MinInvestmentAmount"].ToString();
                                txtMaxInvestmentAmount.Text = dr["AIICST_MaxInvestmentAmount"].ToString();
                            }
                        }

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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindCategory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void BindHours()
        {
            List<string> hours = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("Hours");
            hours = commonLookupBo.GetHours();
            foreach (var array in hours)
            {
                dt.Rows.Add(array);
            }
            if (dt.Rows.Count > 0)
            {
                ddlOpenTimeHours.DataSource = dt;
                ddlOpenTimeHours.DataValueField = dt.Columns["Hours"].ToString();
                ddlOpenTimeHours.DataTextField = dt.Columns["Hours"].ToString();
                ddlOpenTimeHours.DataBind();

                ddlCloseTimeHours.DataSource = dt;
                ddlCloseTimeHours.DataValueField = dt.Columns["Hours"].ToString();
                ddlCloseTimeHours.DataTextField = dt.Columns["Hours"].ToString();
                ddlCloseTimeHours.DataBind();
            }
            ddlOpenTimeHours.Items.Insert(0, new ListItem("Hours", "HH"));
            ddlCloseTimeHours.Items.Insert(0, new ListItem("Hours", "HH"));
        }

        private void BindRTA()
        {
            DataTable dtRTA = new DataTable();
            dtRTA = onlineNCDBackOfficeBo.BindRta().Tables[0];
            if (dtRTA.Rows.Count > 0)
            {
                ddlRegistrar.DataSource = dtRTA;
                ddlRegistrar.DataValueField = dtRTA.Columns["XES_SourceCode"].ToString();
                ddlRegistrar.DataTextField = dtRTA.Columns["XES_SourceName"].ToString();
                ddlRegistrar.DataBind();
            }
        }
        private void BindBankNames()
        {
            DataTable dtBankNames = new DataTable();
            dtBankNames = commonLookupBo.GetWERPLookupMasterValueList(7000, 0);
            if (dtBankNames.Rows.Count > 0)
            {
                ddlBankName.DataSource = dtBankNames;
                ddlBankName.DataValueField = dtBankNames.Columns["WCMV_LookupId"].ToString();
                ddlBankName.DataTextField = dtBankNames.Columns["WCMV_Name"].ToString();
                ddlBankName.DataBind();
            }
        }
        //private void BindBankBranch(int BankId)
        //{
        //    DataTable dtBankNames = new DataTable();
        //    dtBankNames = commonLookupBo.GetBankBranch(BankId);
        //    if (dtBankNames.Rows.Count > 0)
        //    {
        //        ddlBankBranch.DataSource = dtBankNames;
        //        ddlBankBranch.DataValueField = dtBankNames.Columns["Hours"].ToString();
        //        ddlBankBranch.DataTextField = dtBankNames.Columns["Hours"].ToString();
        //        ddlBankBranch.DataBind();
        //    }
        //}
        private void BindMinutesAndSeconds()
        {
            List<string> Minutes = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("Minutes");
            Minutes = commonLookupBo.GetMinutes();
            foreach (var array in Minutes)
            {
                dt.Rows.Add(array);
            }
            if (dt.Rows.Count > 0)
            {
                ddlOpenTimeMinutes.DataSource = dt;
                ddlOpenTimeMinutes.DataValueField = dt.Columns["Minutes"].ToString();
                ddlOpenTimeMinutes.DataTextField = dt.Columns["Minutes"].ToString();
                ddlOpenTimeMinutes.DataBind();

                ddlOpenTimeSeconds.DataSource = dt;
                ddlOpenTimeSeconds.DataValueField = dt.Columns["Minutes"].ToString();
                ddlOpenTimeSeconds.DataTextField = dt.Columns["Minutes"].ToString();
                ddlOpenTimeSeconds.DataBind();

                ddlCloseTimeMinutes.DataSource = dt;
                ddlCloseTimeMinutes.DataValueField = dt.Columns["Minutes"].ToString();
                ddlCloseTimeMinutes.DataTextField = dt.Columns["Minutes"].ToString();
                ddlCloseTimeMinutes.DataBind();

                ddlCloseTimeSeconds.DataSource = dt;
                ddlCloseTimeSeconds.DataValueField = dt.Columns["Minutes"].ToString();
                ddlCloseTimeSeconds.DataTextField = dt.Columns["Minutes"].ToString();
                ddlCloseTimeSeconds.DataBind();

            }
            ddlOpenTimeMinutes.Items.Insert(0, new ListItem("Minutes", "MM"));
            ddlCloseTimeMinutes.Items.Insert(0, new ListItem("Minutes", "MM"));
            ddlOpenTimeSeconds.Items.Insert(0, new ListItem("Seconds", "SS"));
            ddlCloseTimeSeconds.Items.Insert(0, new ListItem("Seconds", "SS"));
            ddlOpenTimeMinutes.SelectedValue = "00";
            ddlCloseTimeMinutes.SelectedValue = "00";
            ddlOpenTimeSeconds.SelectedValue = "00";
            ddlCloseTimeSeconds.SelectedValue = "00";

        }

        private void ListedExchange(string code)
        {
            trExchangeCode.Visible = true;
            //if (ddlListedInExchange.SelectedValue == "NCD")
            //{
            //    lb1Code.Text = "NCd" + lb1Code.Text;

            //}
            //else if (ddlListedInExchange.SelectedValue == "BSN")
            //{
            //    lb1Code.Text = "BSN" + lb1Code.Text;
            //}
        }

        //protected void ddlListedInExchange_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //   // ListedExchange(ddlListedInExchange.SelectedValue);
        //}

        protected void ddlProduct_Selectedindexchanged(object sender, EventArgs e)
        {
            if (ddlProduct.SelectedValue == "Select")
            {
                return;
            }
            EnablityOfControlsonProductAndIssueTypeSelection(ddlProduct.SelectedValue);
        }

        protected void ddlIssueType_Selectedindexchanged(object sender, EventArgs e)
        {
            EnablityOfControlsonIssueTypeSelection(ddlIssueType.SelectedValue);
        }
        private void EnablityOfControlsonIssueTypeSelection(string issueType)
        {
            if (issueType == "Select")
            {
                return;
            }
            trFloorAndFixedPrices.Visible = true;
            //trBookBuildingAndCapprices.Visible = true;      
            //trSyndicateAndMemberCodes.Visible = true;
            trRegistrarAndNoofBidsAlloweds.Visible = true;
            if (issueType == "FixedPrice")
            {
                tdLbFixedPrice.Visible = true;
                tdtxtFixedPrice.Visible = true;

                trBookBuildingAndCapprices.Visible = false;
                tdLbFloorPrice.Visible = false;
                tdTxtFloorPrice.Visible = false;
            }
            else if (issueType == "BookBuilding")
            {
                trBookBuildingAndCapprices.Visible = true;
                tdLbFloorPrice.Visible = true;
                tdTxtFloorPrice.Visible = true;

                tdLbFixedPrice.Visible = false;
                tdtxtFixedPrice.Visible = false;
            }
        }

        private void EnablityOfControlsonProductAndIssueTypeSelection(string product)
        {

            //Ncd
            trNomineeReQuired.Visible = false;
            trIsActiveandPutCallOption.Visible = false;
            trRatingAndModeofTrading.Visible = false;
            trModeofIssue.Visible = false;
            trMaxQty.Visible = false;
            //Ipo
            trIssueTypes.Visible = false;
            trBookBuildingAndCapprices.Visible = false;
            trSyndicateAndMemberCodes.Visible = false;
            trRegistrarAndNoofBidsAlloweds.Visible = false;
            //both
            //trFloorAndFixedPrices.Visible = true;
            trExchangeCode.Visible = false;

            if (product == "NCD")
            {
                trNomineeReQuired.Visible = true;
                trIsActiveandPutCallOption.Visible = true;
                trRatingAndModeofTrading.Visible = true;
                trModeofIssue.Visible = true;
                trFloorAndFixedPrices.Visible = true;
                trMaxQty.Visible = true;

                tdlblCategory.Visible = true;
                tdddlCategory.Visible = true;
            }
            else if (product == "IP")
            {
                trIssueTypes.Visible = true;
                tdlblCategory.Visible = false;
                tdddlCategory.Visible = false;
                trMaxQty.Visible = false;
                trExchangeCode.Visible = true;
            }
        }

        private void CreateUpdateDeleteIssuer(int issuerId, string issuerCode, string issuerName, string commandType)
        {
            int i = onlineNCDBackOfficeBo.CreateUpdateDeleteIssuer(issuerId, issuerCode, issuerName, commandType);
            if (i > 0)
            {
                if (commandType == "INSERT")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Inserted Successfully.');", true);
                else if (commandType == "UPDATE")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Updated Successfully.');", true);
                else
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Deleted Successfully.');", true);
            }
        }

        protected void rgIssuer_ItemCommand(object source, GridCommandEventArgs e)
        {
            int issuerId;
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                TextBox txtIssuerCode = (TextBox)e.Item.FindControl("txtIssuerCode");
                TextBox txtIssuername = (TextBox)e.Item.FindControl("txtIssuername");

                CreateUpdateDeleteIssuer(0, txtIssuerCode.Text, txtIssuername.Text, "INSERT");
            }
            else if (e.CommandName == RadGrid.UpdateCommandName)
            {
                issuerId = Convert.ToInt32(rgIssuer.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PI_IssuerId"].ToString());
                TextBox txtIssuerCode = (TextBox)e.Item.FindControl("txtIssuerCode");
                TextBox txtIssuername = (TextBox)e.Item.FindControl("txtIssuername");

                CreateUpdateDeleteIssuer(issuerId, txtIssuerCode.Text, txtIssuername.Text, "UPDATE");

            }
            else if (e.CommandName == RadGrid.DeleteCommandName)
            {
                issuerId = Convert.ToInt32(rgIssuer.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PI_IssuerId"].ToString());
                CreateUpdateDeleteIssuer(issuerId, "", "", "DELETE");

            }
            BindIssuerGrid();
        }

        protected void btnIssuerPopUp_Click(object sender, ImageClickEventArgs e)
        {
            radwindowPopup.VisibleOnPageLoad = true;
            BindIssuerGrid();

        }
        protected void btnIssuerPopClose_Click(object sender, EventArgs e)
        {
            radwindowPopup.VisibleOnPageLoad = false;
            BindIssuer();
        }
        
        private void BindIssuerGrid()
        {
            try
            {
                DataTable dtIssuer = new DataTable();
                dtIssuer = onlineNCDBackOfficeBo.GetIssuer().Tables[0];
                rgIssuer.DataSource = dtIssuer;
                rgIssuer.DataBind();
                if (Cache[userVo.UserId.ToString() + "Issuer"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "Issuer");
                Cache.Insert(userVo.UserId.ToString() + "Issuer", dtIssuer);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindSeriesCategoryGrid()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void rgIssuer_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtIssuer = new DataTable();
            dtIssuer = (DataTable)Cache[userVo.UserId.ToString() + "Issuer"];
            if (dtIssuer != null)
            {
                rgIssuer.DataSource = dtIssuer;
            }

        }
        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    radwindowPopup.VisibleOnPageLoad = false;
        //}
    }
}