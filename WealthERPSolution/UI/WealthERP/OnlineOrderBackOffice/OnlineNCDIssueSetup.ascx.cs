﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using WealthERP.Base;
using System.Collections.Specialized;
using System.Collections;
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
        IDictionary<string, string> SubCategories;
        Hashtable ht = new Hashtable();
        int count = 0;
        int size = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            int adviserId = advisorVo.advisorId;
            txtOpenDate.SelectedDate = DateTime.Now;

            if (!IsPostBack)
            {
                if (Cache[userVo.UserId.ToString() + "SubCat"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "SubCat");
                DefaultBindings();
                IsPrefixEnablement(false);
            }
            //if (Request.QueryString["IssueId"] != "" && Request.QueryString["IssueId"] != null)
            //{
            //    string product = Request.QueryString["product"].ToString();
            //    int IssueId = int.Parse(Request.QueryString["IssueId"].ToString());
            //    //IssueId=int.Parse(txtIssueId.Text.ToString());
            //    ViewIssueList(IssueId, advisorVo.advisorId, product);
            //    VisblityAndEnablityOfScreen("View");
            //}



        }
        private void DefaultBindings()
        {

            txtOpenDate.SelectedDate = DateTime.Now;
            BindIssuer();
            BindRTA();
            pnlSeries.Visible = false;
            pnlCategory.Visible = false;
            BindHours();
            BindMinutesAndSeconds();
            BindNcdCategory();
            BindBankName();
            BindBranch();
            if (Request.QueryString["action"] != null || Request.QueryString["ProspectUsaction"] != null)
            {
                int issueNo = Convert.ToInt32(Request.QueryString["issueNo"].ToString());
                string product = Request.QueryString["product"].ToString();
                //if (product == null)
                //    product = "NCD";
                ViewIssueList(issueNo, advisorVo.advisorId, product);
                VisblityAndEnablityOfScreen("View");
                // EnablityOfControlsonProductAndIssueTypeSelection("Select");
            }
            else
            {
                VisblityAndEnablityOfScreen("New");
                EnablityOfControlsonProductAndIssueTypeSelection("Select");

            }
        }
        protected void rgSubCategories_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {


            RadGrid rgSubCategories = (RadGrid)source;
            size = rgSubCategories.PageSize;
            int index = e.NewPageIndex + 1;

            size = size * index;

            //foreach (GridDataItem gdi in rgSubCategories.Items)
            //{
            //    if (((CheckBox)gdi.FindControl("cbSubCategories")).Checked == true)
            //    {

            //        int lookupId = Convert.ToInt32(gdi["WCMV_LookupId"].Text);

            //    }

            //}




        }

        private void RememberSelected()
        {
            //ArrayList selectedItems = new ArrayList();
            //int index = -1;
            //bool result = false;
            //GridDataItem parent=rgEligibleInvestorCategories.Items[0];
            //RadGrid rgSubCategories =( RadGrid)(rgEligibleInvestorCategories.Items.FindControl("rgSubCategories"));
            ////RadGrid rgSubCategories = (RadGrid)(parent.Items[0].FindControl(("cbSubCategories")));

            //foreach (GridDataItem gdi in rgSubCategories.Items)
            //{
            //    if (((CheckBox)gdi.FindControl("cbSubCategories")).Checked == true)
            //    {
            //        if (ViewState["Selected"] != null)
            //            selectedItems = (ArrayList)ViewState["Selected"];
            //        if (result)
            //        {
            //            if (!selectedItems.Contains(index))
            //                selectedItems.Add(index);
            //        }
            //        else
            //            selectedItems.Remove(index);
            //    }

            //}



            //if (selectedItems != null && selectedItems.Count > 0)
            //    ViewState["Selected"] = selectedItems;
        }

        private void ViewIssueList(int issueNo, int adviserId, string product)
        {
            try
            {
                DataTable dtSeries = new DataTable();
                dtSeries = onlineNCDBackOfficeBo.GetIssueDetails(issueNo, adviserId).Tables[0];

                foreach (DataRow dr in dtSeries.Rows)
                {
                    txtIssueId.Text = issueNo.ToString();


                    if (product == "NCD")
                    {
                        ddlSubInstrCategory.SelectedValue = dr["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                        BindInstCate(ddlSubInstrCategory.SelectedValue);
                        ddlInstrCat.SelectedValue = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        ddlProduct.SelectedValue = "NCD";
                        // ddlInstrCat.SelectedValue=
                    }
                    else if (product == "IP")
                    {
                        ddlProduct.SelectedValue = "IP";
                        if (Convert.ToInt32(dr["AIM_IsBookBuilding"].ToString()) == 1)
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


                    //if (!string.IsNullOrEmpty(dr["PAIC_AssetInstrumentCategoryCode"].ToString()))
                    //{
                    //    if (dr["PAIC_AssetInstrumentCategoryCode"].ToString() == "FIIP")
                    //    {
                    //        ddlProduct.SelectedValue = "IP";
                    //        if (!string.IsNullOrEmpty(dr["AIM_CapPrice"].ToString()))
                    //        {
                    //            ddlIssueType.SelectedValue = "BookBuilding";
                    //            txtCapPrice.Text = dr["AIM_CapPrice"].ToString();
                    //        }
                    //        else
                    //        {
                    //            txtCapPrice.Text = "";
                    //            ddlIssueType.SelectedValue = "FixedPrice";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (dr["PAIC_AssetInstrumentCategoryCode"].ToString() == "FISD")
                    //        {
                    //            ddlSubInstrCategory.SelectedValue = "FISD";
                    //            ddlProduct.SelectedValue = "NCD";
                    //        }
                    //        else if (dr["PAIC_AssetInstrumentCategoryCode"].ToString() == "FIIB")
                    //        {
                    //            ddlSubInstrCategory.SelectedValue = "FIIB";
                    //            ddlProduct.SelectedValue = "NCD";

                    //        }
                    //        ddlIssueType.SelectedValue = "Select";
                    //    }
                    //}

                    //ddlProduct.SelectedValue =dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    EnablityOfControlsonProductAndIssueTypeSelection(ddlProduct.SelectedValue);
                    EnablityOfControlsonIssueTypeSelection(ddlIssueType.SelectedValue);

                    // ddlSubInstrCategory.SelectedValue = "NCD";
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
                        string closetime = dr["AIM_CloseTime"].ToString();

                        ddlCloseTimeHours.SelectedValue = closetime.Substring(0, 2);
                        ddlCloseTimeMinutes.SelectedValue = closetime.Substring(3, 2);
                        ddlCloseTimeSeconds.SelectedValue = closetime.Substring(6, 2);

                        //txtCloseTimes.Text = dr["AIM_CloseTime"].ToString(); ; //SelectedDate.Value.ToShortTimeString().ToString();
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_CutOffTime"].ToString()))
                    {
                        string opentime = dr["AIM_CutOffTime"].ToString();
                        ddlCutOffTimeHours.SelectedValue = opentime.Substring(0, 2);
                        ddlCutOffTimeMinutes.SelectedValue = opentime.Substring(3, 2);
                        ddlCutOffTimeSeconds.SelectedValue = opentime.Substring(6, 2);

                        //txtOpenTimes.Text = dr["AIM_OpenTime"].ToString(); ; //SelectedDate.Value.ToShortTimeString().ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["AIM_IssueRevisionDate"].ToString()))
                    {
                        txtRevisionDates.SelectedDate = Convert.ToDateTime(dr["AIM_IssueRevisionDate"].ToString());
                    }
                    // txtRevisionDates.SelectedDate = DateTime.Now;

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
                    //ddlBankName.Text = "";
                    //ddlBankBranch.Text = "";
                    if (!string.IsNullOrEmpty(dr["AIM_IsActive"].ToString()))
                    {
                        chkIsActive.Checked = bool.Parse(dr["AIM_IsActive"].ToString());
                    }
                    else
                    {
                        chkIsActive.Checked = false;
                    }
                    if (!string.IsNullOrEmpty(dr["AIM_TradeableAtExchange"].ToString()))
                    {
                        chkTradebleExchange.Checked = true;
                    }
                    else
                    {
                        chkTradebleExchange.Checked = false;
                    }
                    if (!string.IsNullOrEmpty(dr["AIM_PutCallOption"].ToString()))
                    {
                        if (dr["AIM_PutCallOption"].ToString() == "N")
                        {
                            chkPutCallOption.Checked = false;
                        }
                        else
                        {
                            chkPutCallOption.Checked = true;
                        }
                    }
                    else
                    {
                        chkPutCallOption.Checked = false;
                    }


                    if (int.Parse(dr["AIM_IsNominationRequired"].ToString()) != 0)
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
                    if (!string.IsNullOrEmpty(dr["AIM_BankName"].ToString()))
                    {
                        ddlBankName.SelectedValue = dr["AIM_BankName"].ToString();
                    }
                    else
                    {
                        ddlBankName.SelectedValue = "";
                    }
                    if (!string.IsNullOrEmpty(dr["AIM_BankBranch"].ToString()))
                    {
                        txtBankBranch.Text = dr["AIM_BankBranch"].ToString();
                    }
                    else
                    {
                        txtBankBranch.Text = "";
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
                    if (!string.IsNullOrEmpty(dr["AIM_MaxQty"].ToString()))
                    {
                        txtMaxQty.Text = dr["AIM_MaxQty"].ToString();
                    }
                    else
                    {
                        txtMaxQty.Text = "";

                    }
                    if (!string.IsNullOrEmpty(dr["AIM_SubBrokerCode"].ToString()))
                    {
                        txtSubBrokerCode.Text = dr["AIM_SubBrokerCode"].ToString();
                    }
                    else
                    {
                        txtSubBrokerCode.Text = "";

                    }
                    if (!string.IsNullOrEmpty(dr["PI_IssuerId"].ToString()))
                    {
                        ddlIssuer.SelectedValue = dr["PI_IssuerId"].ToString();
                    }
                    else
                    {
                        ddlIssuer.SelectedValue = "Select";
                    }





                    if (!string.IsNullOrEmpty(dr["AIM_RegistrarAddress"].ToString()))
                    {
                        txtRegistrarAddress.Text = dr["AIM_RegistrarAddress"].ToString();
                    }
                    else
                    {
                        txtRegistrarAddress.Text = "";
                    }


                    if (!string.IsNullOrEmpty(dr["AIM_RegistrarTelNo"].ToString()))
                    {
                        txtRegistrarTelNO.Text = dr["AIM_RegistrarTelNo"].ToString();
                    }
                    else
                    {
                        txtRegistrarTelNO.Text = "";
                    }


                    if (!string.IsNullOrEmpty(dr["AIM_RegistrarFaxNo"].ToString()))
                    {
                        txtRegistrarFaxNo.Text = dr["AIM_RegistrarFaxNo"].ToString();
                    }
                    else
                    {
                        txtRegistrarFaxNo.Text = "";
                    }


                    if (!string.IsNullOrEmpty(dr["AIM_RegistrarGrievenceEmail"].ToString()))
                    {
                        txtInvestorGrievenceEmail.Text = dr["AIM_RegistrarGrievenceEmail"].ToString();
                    }
                    else
                    {
                        txtInvestorGrievenceEmail.Text = "";
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_RegistrarWebsite"].ToString()))
                    {
                        txtWebsite.Text = dr["AIM_RegistrarWebsite"].ToString();
                    }
                    else
                    {
                        txtWebsite.Text = "";
                    }
                    if (!string.IsNullOrEmpty(dr["AIM_ISINNumber"].ToString()))
                    {
                        txtISINNo.Text = dr["AIM_ISINNumber"].ToString();
                    }
                    else
                    {
                        txtISINNo.Text = "";
                    }

                    if (!string.IsNullOrEmpty(dr["AIM_RegistrarContactPerson"].ToString()))
                    {
                        txtContactPerson.Text = dr["AIM_RegistrarContactPerson"].ToString();
                    }
                    else
                    {
                        txtContactPerson.Text = "";
                    }



                    if (ddlIssuer.SelectedValue == "Select")
                        return;
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
        public void BindBankName()
        {
            ddlBankName.Items.Clear();
            DataTable dtBankName = new DataTable();
            dtBankName = commonLookupBo.GetWERPLookupMasterValueList(7000, 0); ;
            ddlBankName.DataSource = dtBankName;
            ddlBankName.DataValueField = dtBankName.Columns["WCMV_LookupId"].ToString();
            ddlBankName.DataTextField = dtBankName.Columns["WCMV_Name"].ToString();
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }

        public void BindSubCateDDL(DropDownList ddlSubCat)
        {
            ddlSubCat.Items.Clear();
            DataTable dtBankName = new DataTable();
            dtBankName = commonLookupBo.GetWERPLookupMasterValueList(2000, 0); ;
            ddlSubCat.DataSource = dtBankName;
            ddlSubCat.DataValueField = dtBankName.Columns["WCMV_LookupId"].ToString();
            ddlSubCat.DataTextField = dtBankName.Columns["WCMV_Name"].ToString();
            ddlSubCat.DataBind();
            ddlSubCat.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }
        //protected void ddlBankName_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlBankName.SelectedIndex != 0)
        //        bankid = ddlBankName.SelectedValue;
        //    categoryCode = Convert.ToString(ViewState["Category"]);
        //    BindSubSubCategory(categoryCode, subcategoryCode);
        //}

        public void BindBranch()
        {
            DataTable dt = new DataTable();
            if (ddlBankName.SelectedValue != "Select")
            {
                dt = onlineNCDBackOfficeBo.BankBranchName(int.Parse(ddlBankName.SelectedValue));
                ddlBankBranch.DataSource = dt;
                ddlBankBranch.DataValueField = dt.Columns["CB_CustBankAccId"].ToString();
                ddlBankBranch.DataValueField = dt.Columns["CB_BranchName"].ToString();
                ddlBankBranch.DataBind();
                ddlBankBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
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
                chkIsActive.Enabled = true;

            }
            else if (Mode == "View")
            {
                EnablityOfScreen(false, true, true, true);
                ImageActivRange.Visible = true;
            }
            else if (Mode == "LnkEdit")
            {
                string type = "";
                if (Request.QueryString["action"] != null)
                {
                    type = Request.QueryString["type"].ToString();
                    if (type == "Closed")
                    {
                        EnablityOfScreen(false, false, false, true);
                        txtAllotmentDate.Enabled = true;
                        txtRevisionDates.Enabled = true;
                    }
                    else
                        EnablityOfScreen(true, true, true, true);
                }

            }
            else if (Mode == "AfterUpdate")
            {
                //After Update
                EnablityOfScreen(false, true, false, false);
            }
        }

        private void EnablityOfScreen(bool value, bool boolGridsEnablity, bool boolGridsVisblity, bool boolBtnsVisblity)
        {
            txtBankBranch.Enabled = value;
           
            txtTradingInMultipleOf.Enabled = value;

            ddlProduct.Enabled = value;
            ddlSubInstrCategory.Enabled = value;

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
            txtAllotmentDate.Enabled = value;


            ddlCutOffTimeHours.Enabled = value;
            ddlCutOffTimeMinutes.Enabled = value;
            ddlCutOffTimeSeconds.Enabled = value;

            txtTradingLot.Enabled = value;
            txtBiddingLot.Enabled = value;
            txtBSECode.Enabled = value;
            txtNSECode.Enabled = value;
            txtSubBrokerCode.Enabled = value;


            //txtTradingInMultipleOf.Enabled = value;
            //ddlListedInExchange.Enabled = value;

            ddlBankName.Enabled = value;
            ddlBankBranch.Enabled = value;

            chkPutCallOption.Enabled = value;

            txtMinAplicSize.Enabled = value;
            txtIsPrefix.Enabled = value;

            chkIsActive.Enabled = value;
            chkNomineeReQuired.Enabled = value;
            chkTradebleExchange.Enabled = value;
            //pnlSeries.Enabled = boolGridsEnablity;
            //pnlCategory.Enabled = boolGridsEnablity;
            rgSeries.Enabled = boolGridsEnablity;
            rgEligibleInvestorCategories.Enabled = boolGridsEnablity;

            btnSetUpSubmit.Enabled = value;


            pnlSeries.Visible = boolGridsVisblity;
            pnlCategory.Visible = boolGridsVisblity;

            btnSetUpSubmit.Visible = !boolBtnsVisblity;
            btnUpdate.Visible = boolBtnsVisblity;
            lnkBtnEdit.Visible = boolBtnsVisblity;
            lnlBack.Visible = boolBtnsVisblity;
            //lnkDelete.Visible = boolBtnsVisblity;


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
            int issueId = 0;
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
                    //if (ddlSubInstrCategory.SelectedValue == "FISD")
                    //{
                    onlineNCDBackOfficeVo.AssetInstrumentCategoryCode = ddlInstrCat.SelectedValue;
                    onlineNCDBackOfficeVo.AssetInstrumentSubCategoryCode = ddlSubInstrCategory.SelectedValue;
                    //}
                    //else if (ddlSubInstrCategory.SelectedValue == "FIIB")
                    //{
                    //    onlineNCDBackOfficeVo.AssetInstrumentCategoryCode = "FIIB";
                    //    onlineNCDBackOfficeVo.AssetInstrumentSubCategoryCode = "FIIB";
                    //}
                }

                onlineNCDBackOfficeVo.IssueName = txtName.Text;
                onlineNCDBackOfficeVo.IssuerId = Convert.ToInt32(ddlIssuer.SelectedValue);

                onlineNCDBackOfficeVo.FromRange = Convert.ToInt64(txtFormRange.Text);
                onlineNCDBackOfficeVo.ToRange = Convert.ToInt64(txtToRange.Text);

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
                if (ddlCutOffTimeMinutes.SelectedValue == "MM")
                {
                    ddlCutOffTimeMinutes.SelectedValue = "00";
                }
                if (ddlOpenTimeSeconds.SelectedValue == "SS")
                {
                    ddlOpenTimeSeconds.SelectedValue = "00";
                }
                if (ddlCloseTimeSeconds.SelectedValue == "SS")
                {
                    ddlCloseTimeSeconds.SelectedValue = "00";
                }
                if (ddlCutOffTimeSeconds.SelectedValue == "SS")
                {
                    ddlCutOffTimeSeconds.SelectedValue = "00";
                }
                //string time = txtOpenTimes.SelectedDate.Value.ToShortTimeString().ToString();
                onlineNCDBackOfficeVo.OpenTime = Convert.ToDateTime(ddlOpenTimeHours.SelectedValue + ":" + ddlOpenTimeMinutes.SelectedValue + ":" + ddlOpenTimeSeconds.SelectedValue); //SelectedDate.Value.ToShortTimeString().ToString();
                onlineNCDBackOfficeVo.CloseTime = Convert.ToDateTime(ddlCloseTimeHours.SelectedValue + ":" + ddlCloseTimeMinutes.SelectedValue + ":" + ddlCloseTimeSeconds.SelectedValue);//SelectedDate.Value.ToShortTimeString().ToString();
                onlineNCDBackOfficeVo.CutOffTime = Convert.ToDateTime(ddlCutOffTimeHours.SelectedValue + ":" + ddlCutOffTimeMinutes.SelectedValue + ":" + ddlCutOffTimeSeconds.SelectedValue);//SelectedDate.Value.ToShortTimeString().ToString();


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
                if (!string.IsNullOrEmpty(txtBankBranch.Text))
                {
                    onlineNCDBackOfficeVo.BankBranch = txtBankBranch.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.BankBranch = "";
                }

                //if (!string.IsNullOrEmpty(ddlBankBranch.SelectedValue))
                //{
                //    onlineNCDBackOfficeVo.BankBranch = ddlBankBranch.SelectedValue;
                //}
                //else
                //{
                //    onlineNCDBackOfficeVo.BankBranch = "";
                //}

                if (chkIsActive.Checked == true)
                {
                    onlineNCDBackOfficeVo.IsActive = 1;
                }
                else
                {
                    onlineNCDBackOfficeVo.IsActive = 0;
                }
                if (chkPutCallOption.Checked == true)
                {
                    onlineNCDBackOfficeVo.PutCallOption = "Y";
                }
                else
                {
                    onlineNCDBackOfficeVo.PutCallOption = "N";
                }
                if (chkTradebleExchange.Checked == true)
                {
                    onlineNCDBackOfficeVo.TradableExchange = 1;
                }
                else
                {
                    onlineNCDBackOfficeVo.TradableExchange = 0;
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
                if (!string.IsNullOrEmpty((txtAllotmentDate.SelectedDate).ToString().Trim()))
                    onlineNCDBackOfficeVo.AllotmentDate = DateTime.Parse(txtAllotmentDate.SelectedDate.ToString());
                else
                    onlineNCDBackOfficeVo.AllotmentDate = DateTime.MinValue;

                if (ddlIssueType.SelectedValue == "BookBuilding")
                {
                    onlineNCDBackOfficeVo.BookBuildingPercentage = Convert.ToInt32(txtBookBuildingPer.Text);
                    onlineNCDBackOfficeVo.IsBookBuilding = 1;
                }
                else if (ddlIssueType.SelectedValue == "FixedPrice")
                {
                    onlineNCDBackOfficeVo.BookBuildingPercentage = 0;
                    onlineNCDBackOfficeVo.IsBookBuilding = 0;
                }
                //if (!string.IsNullOrEmpty(txtBookBuildingPer.Text))
                //{
                //    onlineNCDBackOfficeVo.BookBuildingPercentage = Convert.ToInt32(txtBookBuildingPer.Text);
                //    onlineNCDBackOfficeVo.IsBookBuilding = 1;
                //}
                //else
                //{
                //    onlineNCDBackOfficeVo.BookBuildingPercentage = 0;
                //    onlineNCDBackOfficeVo.IsBookBuilding = 0;
                //}
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
                if (!string.IsNullOrEmpty(txtSubBrokerCode.Text))
                {
                    onlineNCDBackOfficeVo.Subbrokercode = txtSubBrokerCode.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.Subbrokercode = "";
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
                if (!string.IsNullOrEmpty(txtRegistrarAddress.Text))
                {

                    onlineNCDBackOfficeVo.RegistrarAddress = txtRegistrarAddress.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.RegistrarAddress = "";
                }

                if (!string.IsNullOrEmpty(txtRegistrarTelNO.Text))
                {

                    onlineNCDBackOfficeVo.RegistrarTelNo = txtRegistrarTelNO.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.RegistrarTelNo = "";
                }

                if (!string.IsNullOrEmpty(txtRegistrarFaxNo.Text))
                {

                    onlineNCDBackOfficeVo.RegistrarFaxNo = txtRegistrarFaxNo.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.RegistrarFaxNo = "";
                }

                if (!string.IsNullOrEmpty(txtInvestorGrievenceEmail.Text))
                {

                    onlineNCDBackOfficeVo.RegistrarGrievenceEmail = txtInvestorGrievenceEmail.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.RegistrarGrievenceEmail = "";
                }

                if (!string.IsNullOrEmpty(txtWebsite.Text))
                {

                    onlineNCDBackOfficeVo.RegistrarWebsite = txtWebsite.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.RegistrarWebsite = "";
                }

                if (!string.IsNullOrEmpty(txtISINNo.Text))
                {

                    onlineNCDBackOfficeVo.ISINNumber = txtISINNo.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.ISINNumber = "";
                }

                if (!string.IsNullOrEmpty(txtContactPerson.Text))
                {

                    onlineNCDBackOfficeVo.RegistrarContactPerson = txtContactPerson.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.RegistrarContactPerson = "";
                }

                if (!string.IsNullOrEmpty(txtSBIRegistationNo.Text))
                {

                    onlineNCDBackOfficeVo.SBIRegistationNo = txtSBIRegistationNo.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.SBIRegistationNo = "";
                }

                
                onlineNCDBackOfficeVo.IssueId = Convert.ToInt32(txtIssueId.Text);
                if (NSCEBSCEcode())
                {
                    issueId = onlineNCDBackOfficeBo.UpdateIssue(onlineNCDBackOfficeVo);
                    if (issueId > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Issue Updated successfully.');", true);
                        VisblityAndEnablityOfScreen("AfterUpdate");
                        btnSetUpSubmit.Visible = false;
                        lnkBtnEdit.Visible = true;
                        lnlBack.Visible = true;

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

        private int CreateUpdateDeleteSeries(int issueId, int seriesId, string seriesName, int isBuyBackAvailable, int redemptionApplicable, int lockInApplicable, int tenure, string interestFrequency,
         string interestType, int SeriesSequence, string tenureUnits,string CommandType)
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
                onlineNCDBackOfficeVo.RedemptionApplicable = redemptionApplicable;
                onlineNCDBackOfficeVo.LockInApplicable = lockInApplicable;
                onlineNCDBackOfficeVo.TenureUnits = tenureUnits;

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
                onlineNCDBackOfficeVo.RedemptionApplicable = redemptionApplicable;
                onlineNCDBackOfficeVo.LockInApplicable = lockInApplicable;
                onlineNCDBackOfficeVo.TenureUnits = tenureUnits;

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
        private void ShowMessage(string msg)
        {

            //tblMessage.Visible = true;
            //msgRecordStatus.InnerText = msg;
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mykey", "hide();", true);
        }
        protected void rgSeries_ItemCommand(object source, GridCommandEventArgs e)
        {
            int count = 0;
            int categoryGridcount = rgEligibleInvestorCategories.Items.Count;
            string attachedCatId = string.Empty;
            if (categoryGridcount == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please Fill Categories.');", true);
                // return;
                e.Canceled = true;
            }

            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                int availblity, redemptionavaliable, lockinperiodavaliable;
                TextBox txtSereiesName = (TextBox)e.Item.FindControl("txtSereiesName");
                TextBox txtTenure = (TextBox)e.Item.FindControl("txtTenure");
                DropDownList ddlTenure = (DropDownList)e.Item.FindControl("ddlTenure");
                DropDownList ddltInterestFrequency = (DropDownList)e.Item.FindControl("ddlInterestFrequency");
                CheckBox chkBuyAvailability = (CheckBox)e.Item.FindControl("chkBuyAvailability");
                DropDownList ddlInterestType = (DropDownList)e.Item.FindControl("ddlInterestType");
                TextBox txtSequence = (TextBox)e.Item.FindControl("txtSequence");
                RadGrid rgSeriesCat = (RadGrid)e.Item.FindControl("rgSeriesCat");
                CheckBox chkredemptiondate = (CheckBox)e.Item.FindControl("chkredemptiondate");
                CheckBox chkLockinperiod = (CheckBox)e.Item.FindControl("chkLockinperiod");
                DropDownList ddlTenureUnits = (DropDownList)e.Item.FindControl("ddlTenureUnits");

                

                count = onlineNCDBackOfficeBo.CheckIssueSeriesName(txtSereiesName.Text, Convert.ToInt32(txtIssueId.Text));
                if (count > 0)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Series Name Already Exist');", true);
                    e.Canceled = true;
                    return;
                }

                int isSeqExist = onlineNCDBackOfficeBo.ChekSeriesSequence(Convert.ToInt32(txtSequence.Text), Convert.ToInt32(txtIssueId.Text), advisorVo.advisorId, 0);
                if (isSeqExist > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Sequence Exist');", true);
                    e.Canceled = true;
                    return;
                }
                foreach (GridDataItem gdi in rgSeriesCat.Items)
                {
                    if (((CheckBox)gdi.FindControl("cbSeriesCat")).Checked == true)
                    {
                        int categoryId = Convert.ToInt32(gdi["AIIC_InvestorCatgeoryId"].Text);
                        attachedCatId += categoryId + ",";
                        count++;
                    }
                }
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Select One Category.');", true);
                    e.Canceled = true;
                    return;
                }

                onlineNCDBackOfficeBo.IsSameSubTypeCatAttchedtoSeries(attachedCatId, Convert.ToInt32(txtIssueId.Text), ref attachedCatId);
                if (attachedCatId != String.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert( '" + attachedCatId + "');", true);
                    e.Canceled = true;
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
                if (chkredemptiondate.Checked == true)
                {
                    redemptionavaliable = 1;
                }
                else
                {
                    redemptionavaliable = 0;
                }
                if (chkLockinperiod.Checked == true)
                {
                    lockinperiodavaliable = 1;
                }
                else
                {
                    lockinperiodavaliable = 0;
                }
                if (string.IsNullOrEmpty(txtTenure.Text))
                {
                    txtTenure.Text = 0.ToString();
                }
                //if (string.IsNullOrEmpty(ddltInterestFrequency.Text))
                //{
                //    txtTenure.Text = 0.ToString();
                //}
                //{
                //if (string.IsNullOrEmpty(txtSereiesName.Text))
                //    return;




                int seriesId = CreateUpdateDeleteSeries(Convert.ToInt32(txtIssueId.Text), 0, txtSereiesName.Text, availblity, redemptionavaliable, lockinperiodavaliable, Convert.ToInt32(txtTenure.Text), ddltInterestFrequency.SelectedValue, ddlInterestType.SelectedValue, Convert.ToInt32(txtSequence.Text),ddlTenureUnits.SelectedValue, "Insert");

                foreach (GridDataItem gdi in rgSeriesCat.Items)
                {
                    if (((CheckBox)gdi.FindControl("cbSeriesCat")).Checked == true)
                    {
                        int categoryId = Convert.ToInt32(gdi["AIIC_InvestorCatgeoryId"].Text);
                        TextBox txtInterestRate = ((TextBox)(gdi.FindControl("txtInterestRate")));
                        TextBox txtAnnualizedYield = ((TextBox)(gdi.FindControl("txtAnnualizedYield")));
                        TextBox txtRenCpnRate = (TextBox)gdi.FindControl("txtRenCpnRate");
                        TextBox txtYieldAtCall = (TextBox)gdi.FindControl("txtYieldAtCall");
                        TextBox txtYieldAtBuyBack = (TextBox)gdi.FindControl("txtYieldAtBuyBack");
                        TextBox txtRedemptionDate = (TextBox)gdi.FindControl("txtRedemptionDate");
                        TextBox txtRedemptionAmount = (TextBox)gdi.FindControl("txtRedemptionAmount");
                        TextBox txtLockInPeriod = (TextBox)gdi.FindControl("txtLockInPeriod");

                        if (string.IsNullOrEmpty(txtInterestRate.Text))
                        {
                            txtInterestRate.Text = 0.ToString();
                        }

                        if (string.IsNullOrEmpty(txtAnnualizedYield.Text))
                        {
                            txtAnnualizedYield.Text = 0.ToString();
                        }
                        if (string.IsNullOrEmpty(txtRenCpnRate.Text))
                        {
                            txtRenCpnRate.Text = 0.ToString();
                        }

                        if (string.IsNullOrEmpty(txtYieldAtCall.Text))
                        {
                            txtYieldAtCall.Text = 0.ToString();
                        }

                        if (string.IsNullOrEmpty(txtYieldAtBuyBack.Text))
                        {
                            txtYieldAtBuyBack.Text = 0.ToString();
                        }
                        if (string.IsNullOrEmpty(txtRedemptionDate.Text))
                        {
                            txtRedemptionDate.Text = 0.ToString();
                        }
                        if (string.IsNullOrEmpty(txtRedemptionAmount.Text))
                        {
                            txtRedemptionAmount.Text = 0.ToString();
                        }
                        if (string.IsNullOrEmpty(txtLockInPeriod.Text))
                        {
                            txtLockInPeriod.Text = 0.ToString();
                        }

                         
                        CreateUpdateDeleteSeriesCategories(seriesId, categoryId, Convert.ToDouble(txtInterestRate.Text), Convert.ToDouble(txtAnnualizedYield.Text), Convert.ToDouble(txtRenCpnRate.Text),
                            Convert.ToDouble(txtYieldAtCall.Text), Convert.ToDouble(txtYieldAtBuyBack.Text), txtRedemptionDate.Text, Convert.ToDouble(txtRedemptionAmount.Text), int.Parse(txtLockInPeriod.Text), "Insert");
                    }
                }
                BindSeriesGrid(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));

            }
            else if (e.CommandName == RadGrid.UpdateCommandName)
            {
                int availblity, redemptionavaliable, lockinperiodavaliable;
                TextBox txtSereiesName = (TextBox)e.Item.FindControl("txtSereiesName");
                TextBox txtTenure = (TextBox)e.Item.FindControl("txtTenure");
                DropDownList ddlTenure = (DropDownList)e.Item.FindControl("ddlTenure");
                DropDownList ddlInterestFrequency = (DropDownList)e.Item.FindControl("ddlInterestFrequency");
                CheckBox chkBuyAvailability = (CheckBox)e.Item.FindControl("chkBuyAvailability");
                DropDownList ddlInterestType = (DropDownList)e.Item.FindControl("ddlInterestType");
                TextBox txtSequence = (TextBox)e.Item.FindControl("txtSequence");
                CheckBox chkredemptiondate = (CheckBox)e.Item.FindControl("chkredemptiondate");
                CheckBox chkLockinperiod = (CheckBox)e.Item.FindControl("chkLockinperiod");
                RadGrid rgSeriesCat = (RadGrid)e.Item.FindControl("rgSeriesCat");
                DropDownList ddlTenureUnits = (DropDownList)e.Item.FindControl("ddlTenureUnits");

                int seriesId = Convert.ToInt32(rgSeries.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AID_IssueDetailId"].ToString());

                int isSeqExist = onlineNCDBackOfficeBo.ChekSeriesSequence(Convert.ToInt32(txtSequence.Text), Convert.ToInt32(txtIssueId.Text), advisorVo.advisorId, seriesId);
                if (isSeqExist > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Sequence Exist');", true);
                    e.Canceled = true;
                    return;
                }
                foreach (GridDataItem gdi in rgSeriesCat.Items)
                {
                    if (((CheckBox)gdi.FindControl("cbSeriesCat")).Checked == true)
                    {
                        int categoryId = Convert.ToInt32(gdi["AIIC_InvestorCatgeoryId"].Text);
                        attachedCatId += categoryId + ",";
                        count++;
                    }
                }
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Select One Category.');", true);
                    e.Canceled = true;
                    return;
                }

                onlineNCDBackOfficeBo.IsSameSubTypeCatAttchedtoSeries(attachedCatId, Convert.ToInt32(txtIssueId.Text), ref attachedCatId);
                if (attachedCatId != String.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert( '" + attachedCatId + "');", true);
                    e.Canceled = true;
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
                if (chkredemptiondate.Checked == true)
                {
                    redemptionavaliable = 1;
                }
                else
                {
                    redemptionavaliable = 0;
                }
                if (chkLockinperiod.Checked == true)
                {
                    lockinperiodavaliable = 1;
                }
                else
                {
                    lockinperiodavaliable = 0;
                }
                if (string.IsNullOrEmpty(txtTenure.Text))
                {
                    txtTenure.Text = 0.ToString();
                }
                //if (string.IsNullOrEmpty(txtSequence.Text))
                //{
                //    txtSequence.Text = 0.ToString();
                //}

                int InsseriesId = CreateUpdateDeleteSeries(Convert.ToInt32(txtIssueId.Text), seriesId, txtSereiesName.Text, availblity, redemptionavaliable, lockinperiodavaliable, Convert.ToInt32(txtTenure.Text), ddlInterestFrequency.SelectedValue, ddlInterestType.SelectedValue, Convert.ToInt32(txtSequence.Text), ddlTenureUnits.SelectedValue, "Update");
                foreach (GridDataItem gdi in rgSeriesCat.Items)
                {
                    if (((CheckBox)gdi.FindControl("cbSeriesCat")).Checked == true)
                    {
                        int categoryId = Convert.ToInt32(gdi["AIIC_InvestorCatgeoryId"].Text);
                        TextBox txtInterestRate = ((TextBox)(gdi.FindControl("txtInterestRate")));
                        TextBox txtAnnualizedYield = ((TextBox)(gdi.FindControl("txtAnnualizedYield")));
                        TextBox txtRenCpnRate = (TextBox)gdi.FindControl("txtRenCpnRate");
                        TextBox txtYieldAtCall = (TextBox)gdi.FindControl("txtYieldAtCall");
                        TextBox txtYieldAtBuyBack = (TextBox)gdi.FindControl("txtYieldAtBuyBack");
                        TextBox txtRedemptionDate = (TextBox)gdi.FindControl("txtRedemptionDate");
                        TextBox txtRedemptionAmount = (TextBox)gdi.FindControl("txtRedemptionAmount");
                        TextBox txtLockInPeriod = (TextBox)gdi.FindControl("txtLockInPeriod");
                        if (string.IsNullOrEmpty(txtInterestRate.Text))
                        {
                            txtInterestRate.Text = 0.ToString();
                        }

                        if (string.IsNullOrEmpty(txtAnnualizedYield.Text))
                        {
                            txtAnnualizedYield.Text = 0.ToString();
                        }
                        if (string.IsNullOrEmpty(txtRenCpnRate.Text))
                        {
                            txtRenCpnRate.Text = 0.ToString();
                        }

                        if (string.IsNullOrEmpty(txtYieldAtCall.Text))
                        {
                            txtYieldAtCall.Text = 0.ToString();
                        }
                        if (string.IsNullOrEmpty(txtYieldAtBuyBack.Text))
                        {
                            txtYieldAtBuyBack.Text = 0.ToString();
                        }

                        if (string.IsNullOrEmpty(txtRedemptionDate.Text))
                        {
                            txtRedemptionDate.Text = 0.ToString();
                        }
                        if (string.IsNullOrEmpty(txtRedemptionAmount.Text))
                        {
                            txtRedemptionAmount.Text = 0.ToString();
                        }
                        if (string.IsNullOrEmpty(txtLockInPeriod.Text))
                        {
                            txtLockInPeriod.Text = 0.ToString();
                        }
                        CreateUpdateDeleteSeriesCategories(seriesId, categoryId, Convert.ToDouble(txtInterestRate.Text), Convert.ToDouble(txtAnnualizedYield.Text), Convert.ToDouble(txtRenCpnRate.Text), Convert.ToDouble(txtYieldAtCall.Text), Convert.ToDouble(txtYieldAtBuyBack.Text), txtRedemptionDate.Text, Convert.ToDouble(txtRedemptionAmount.Text), int.Parse(txtLockInPeriod.Text), "Update");
                    }
                }
                BindSeriesGrid(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));
            }
            else if (e.CommandName == RadGrid.DeleteCommandName)
            {
                int seriesId = Convert.ToInt32(rgSeries.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AID_IssueDetailId"].ToString());
            }

        }

        protected void rgSeries_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridNestedViewItem)
            {
                (e.Item.FindControl("rgSeriesCategories") as RadGrid).NeedDataSource += new GridNeedDataSourceEventHandler(rgSeriesCategories_OnNeedDataSource);
            }
        }

        protected void btnAddMore_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DataTable dtSubCategory = new DataTable();


            if ((btn).NamingContainer is GridEditFormInsertItem)
            {
                GridEditFormInsertItem gdi = (GridEditFormInsertItem)(btn).NamingContainer;
                RadGrid rgSubCategories = (RadGrid)gdi.FindControl("rgSubCategories");
                int cnt = rgSubCategories.Items.Count;


                DataTable dtRecords = new DataTable();
                //  RadGrid rgSubCategories = (RadGrid)gdi.FindControl("rgSubCategories");



                //if((e.Item is GridEditFormInsertItem ) || (e.Item is GridEditFormItem))
                //{
                foreach (GridColumn col in rgSubCategories.Columns)
                {
                    DataColumn colString = new DataColumn(col.UniqueName);
                    dtRecords.Columns.Add(colString);

                }
                //    DataColumn colString = new DataColumn(col.UniqueName);
                //    dtRecords.Columns.Add(colString);

                //}
                foreach (GridDataItem row in rgSubCategories.Items) // loops through each rows in RadGrid
                {
                    //  row.SetVisibleChildren(true);
                    TextBox txtSubCategoryCode = null;
                    DropDownList ddlSubCategory = null;
                    TextBox txtMinInvestmentAmount = null;
                    TextBox txtMaxInvestmentAmount = null;
                    TextBox txtSubCategoryId = null;
                    DataRow dr = dtRecords.NewRow();
                    foreach (GridColumn col in rgSubCategories.Columns) //loops through each column in RadGrid
                    {
                        txtSubCategoryCode = (TextBox)(row[col.UniqueName].FindControl("txtSubCategoryCode"));
                        ddlSubCategory = (DropDownList)(row[col.UniqueName].FindControl("ddlSubCategory"));
                        txtMinInvestmentAmount = (TextBox)(row[col.UniqueName].FindControl("txtMinInvestmentAmount"));
                        txtMaxInvestmentAmount = (TextBox)(row[col.UniqueName].FindControl("txtMaxInvestmentAmount"));
                        txtSubCategoryId = (TextBox)(row[col.UniqueName].FindControl("txtSubCategoryId"));
                        if (col.UniqueName == "SubCategoryCode")
                        {
                            dr[col.UniqueName] = txtSubCategoryCode.Text;
                        }
                        else if (col.UniqueName == "CustSubCategory")
                        {
                            dr[col.UniqueName] = ddlSubCategory.Text;
                        }
                        else if (col.UniqueName == "MinInvestmentAmt")
                        {
                            dr[col.UniqueName] = txtMinInvestmentAmount.Text;
                        }
                        else if (col.UniqueName == "MaxInvestmentAmt")
                        {
                            dr[col.UniqueName] = txtMaxInvestmentAmount.Text;
                        }
                        else if (col.UniqueName == "txtSubCategoryId")
                        {
                            dr[col.UniqueName] = txtSubCategoryId.Text;
                        }
                        // 

                    }
                    if (txtSubCategoryCode != null && ddlSubCategory != null && txtMinInvestmentAmount != null && txtMaxInvestmentAmount != null)
                    {
                        if (txtSubCategoryCode.Text != string.Empty && ddlSubCategory.SelectedValue != "Select")
                            dtRecords.Rows.Add(dr);
                    }

                }
                dtRecords.Rows.Add();
                dtRecords.AcceptChanges();



                if (Cache[userVo.UserId.ToString() + "SubCat"] != null)
                {
                    Cache.Remove(userVo.UserId.ToString() + "SubCat");
                    Cache.Insert(userVo.UserId.ToString() + "SubCat", dtRecords);
                }
                else
                {
                    Cache.Insert(userVo.UserId.ToString() + "SubCat", dtRecords);

                }

                rgSubCategories.DataSource = dtRecords;
                rgSubCategories.DataBind();
            }
            else if ((btn).NamingContainer is GridEditFormItem)
            {
                GridEditFormItem gdi = (GridEditFormItem)(btn).NamingContainer;


                RadGrid rgSubCategories = (RadGrid)gdi.FindControl("rgSubCategories");
                int cnt = rgSubCategories.Items.Count;


                DataTable dtRecords = new DataTable();
                //  RadGrid rgSubCategories = (RadGrid)gdi.FindControl("rgSubCategories");



                //if((e.Item is GridEditFormInsertItem ) || (e.Item is GridEditFormItem))
                //{
                foreach (GridColumn col in rgSubCategories.Columns)
                {
                    DataColumn colString = new DataColumn(col.UniqueName);
                    dtRecords.Columns.Add(colString);

                }
                //    DataColumn colString = new DataColumn(col.UniqueName);
                //    dtRecords.Columns.Add(colString);

                //}
                foreach (GridDataItem row in rgSubCategories.Items) // loops through each rows in RadGrid
                {
                    //  row.SetVisibleChildren(true);
                    TextBox txtSubCategoryCode = null;
                    DropDownList ddlSubCategory = null;
                    TextBox txtMinInvestmentAmount = null;
                    TextBox txtMaxInvestmentAmount = null;
                    TextBox txtSubCategoryId = null;

                    DataRow dr = dtRecords.NewRow();
                    foreach (GridColumn col in rgSubCategories.Columns) //loops through each column in RadGrid
                    {
                        txtSubCategoryCode = (TextBox)(row[col.UniqueName].FindControl("txtSubCategoryCode"));
                        ddlSubCategory = (DropDownList)(row[col.UniqueName].FindControl("ddlSubCategory"));
                        txtMinInvestmentAmount = (TextBox)(row[col.UniqueName].FindControl("txtMinInvestmentAmount"));
                        txtMaxInvestmentAmount = (TextBox)(row[col.UniqueName].FindControl("txtMaxInvestmentAmount"));
                        txtSubCategoryId = (TextBox)(row[col.UniqueName].FindControl("txtSubCategoryId"));

                        if (col.UniqueName == "SubCategoryCode")
                        {
                            dr[col.UniqueName] = txtSubCategoryCode.Text;
                        }
                        else if (col.UniqueName == "CustSubCategory")
                        {
                            dr[col.UniqueName] = ddlSubCategory.Text;
                        }
                        else if (col.UniqueName == "MinInvestmentAmt")
                        {
                            dr[col.UniqueName] = txtMinInvestmentAmount.Text;
                        }
                        else if (col.UniqueName == "MaxInvestmentAmt")
                        {
                            dr[col.UniqueName] = txtMaxInvestmentAmount.Text;
                        }
                        else if (col.UniqueName == "SubCategoryId")
                        {
                            dr[col.UniqueName] = txtSubCategoryId.Text;
                        }

                    }
                    if (txtSubCategoryCode != null && ddlSubCategory != null && txtMinInvestmentAmount != null && txtMaxInvestmentAmount != null)
                    {
                        if (txtSubCategoryCode.Text != string.Empty && ddlSubCategory.SelectedValue != "Select")
                            dtRecords.Rows.Add(dr);
                    }

                }
                dtRecords.Rows.Add();
                dtRecords.AcceptChanges();



                if (Cache[userVo.UserId.ToString() + "SubCat"] != null)
                {
                    Cache.Remove(userVo.UserId.ToString() + "SubCat");
                    Cache.Insert(userVo.UserId.ToString() + "SubCat", dtRecords);
                }
                else
                {
                    Cache.Insert(userVo.UserId.ToString() + "SubCat", dtRecords);

                }

                rgSubCategories.DataSource = dtRecords;
                rgSubCategories.DataBind();

            }


        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DataTable dtSubCategory = new DataTable();


            GridEditFormInsertItem gdi = (GridEditFormInsertItem)(btn).NamingContainer;

            RadGrid rgSubCategories = (RadGrid)gdi.FindControl("rgSubCategories");

            foreach (GridDataItem gvRow in rgSubCategories.Items)
            {
                CheckBox chk = (CheckBox)gvRow.FindControl("cbRemoveSubCategories");
                if (chk.Checked)
                {

                    int rowindex1 = gvRow.RowIndex;// ((GridDataItem)((CheckBox)sender).NamingContainer).RowIndex;
                    int rowindex = (rowindex1 / 2) - 1;

                    foreach (GridColumn col in rgSubCategories.Columns)
                    {
                        gvRow[col].Controls.RemoveAt(0);
                        //DataColumn colString = new DataColumn(col.UniqueName);
                        //dtRecords.Columns.Add(colString);

                    }
                    //  gvRow.Controls.RemoveAt(rowindex);
                    //int controlCnt = gvRow.items.Count;
                    //for (int i = 0; i <= controlCnt; i++)
                    //{
                    // gvRow.Controls.Remove(gvRow.Controls[rowindex-1]);
                    // gvRow.Controls.RemoveAt(rowindex);
                    // gvRow.Controls.RemoveAt(2);

                    // }


                }

            }

            //RadGrid rgSubCategories = (RadGrid)gdi.FindControl("rgSubCategories");
            //int cnt = rgSubCategories.Items.Count;


            //dtSubCategory = onlineNCDBackOfficeBo.GetSubCategory(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text), cnt + 1).Tables[0];
            //if (Cache[userVo.UserId.ToString() + "SubCat"] != null)
            //    Cache.Remove(userVo.UserId.ToString() + "SubCat");
            //Cache.Insert(userVo.UserId.ToString() + "SubCat", dtSubCategory);




        }


        protected void rgEligibleInvestorCategories_ItemInserted(object source, GridInsertedEventArgs e)
        {
            e.KeepInInsertMode = true;
        }
        protected void rgEligibleInvestorCategories_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            e.KeepInEditMode = true;
        }

        protected void rgSubCategories_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                int rowindex1 = (e.Item.ItemIndex);



                RadGrid rgSubCategories = (RadGrid)source;
                // rgSubCategories.Controls.RemoveAt(rowindex1);

                DataTable dtRecords = new DataTable();
                if (Cache[userVo.UserId.ToString() + "SubCat"] != null)
                {
                    // dtRecords = (DataTable)Cache[userVo.UserId.ToString() + "SubCat"];

                    //dtRecords.Rows.RemoveAt(rowindex1);
                    // dtRecords.AcceptChanges();

                    //   rgSubCategories.DataSource = dtRecords;
                    //   rgSubCategories.DataBind();


                }

                //foreach (GridColumn col in rgSubCategories.Columns)
                //{
                //    DataColumn colString = new DataColumn(col.UniqueName);
                //    dtRecords.Columns.Add(colString);

                //}
                //foreach (GridDataItem row in rgSubCategories.Items) // loops through each rows in RadGrid
                //{
                //    //  row.SetVisibleChildren(true);
                //    DataRow dr = dtRecords.NewRow();
                //    foreach (GridColumn col in rgSubCategories.Columns) //loops through each column in RadGrid
                //    {
                //        if (col.UniqueName == "SubCategoryCode")
                //        {
                //            TextBox txtSubCategoryCode = (TextBox)(row[col.UniqueName].FindControl("txtSubCategoryCode"));
                //            dr[col.UniqueName] = txtSubCategoryCode.Text;
                //        }
                //        else if (col.UniqueName == "CustSubCategory")
                //        {
                //            DropDownList ddlSubCategory = (DropDownList)(row[col.UniqueName].FindControl("ddlSubCategory"));
                //            dr[col.UniqueName] = ddlSubCategory.Text;
                //        }
                //        else if (col.UniqueName == "MinInvestmentAmt")
                //        {
                //            TextBox txtMinInvestmentAmount = (TextBox)(row[col.UniqueName].FindControl("txtMinInvestmentAmount"));
                //            dr[col.UniqueName] = txtMinInvestmentAmount.Text;
                //        }
                //        else if (col.UniqueName == "MaxInvestmentAmt")
                //        {
                //            TextBox txtMinInvestmentAmount = (TextBox)(row[col.UniqueName].FindControl("txtMinInvestmentAmount"));
                //            dr[col.UniqueName] = txtMinInvestmentAmount.Text;
                //        }

                //    }
                //    dtRecords.Rows.Add(dr);
                //}

                //dtRecords.Rows.RemoveAt(rowindex1);
                //dtRecords.AcceptChanges();


                //if (Cache[userVo.UserId.ToString() + "SubCat"] != null)
                //{
                //    Cache.Remove(userVo.UserId.ToString() + "SubCat");
                //    Cache.Insert(userVo.UserId.ToString() + "SubCat", dtRecords);
                //}
                //else
                //{
                //    Cache.Insert(userVo.UserId.ToString() + "SubCat", dtRecords);

                //}
                //rgSubCategories.DataSource = dtRecords;
                //rgSubCategories.Rebind();

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
                int isExist = onlineNCDBackOfficeBo.IsValidBidRange(Convert.ToInt32(txtIssueId.Text), Convert.ToDouble(txtMinBidAmount.Text), Convert.ToDouble(txtMaxBidAmount.Text));
                if (isExist > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('This Bid Range Exist');", true);
                    e.Canceled = true;
                    return;
                }
                foreach (GridDataItem gdi in rgSubCategories.Items)
                {
                    if (((TextBox)gdi.FindControl("txtSubCategoryCode")).Text != string.Empty && ((DropDownList)gdi.FindControl("ddlSubCategory")).SelectedValue != "Select")
                    {
                        count++;
                    }

                }
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Pls Add one Subcategory');", true);
                    e.Canceled = true;
                    return;
                }

                categoryId = CreateUpdateDeleteCategory(Convert.ToInt32(txtIssueId.Text), 0, txtCategoryName.Text, txtCategoryDescription.Text, txtChequePayableTo.Text, Convert.ToInt64(txtMinBidAmount.Text), Convert.ToInt64(txtMaxBidAmount.Text), discountType, Convert.ToDecimal(txtDiscountValue.Text), "Insert");

                foreach (GridDataItem gdi in rgSubCategories.Items)
                {
                    if (((TextBox)gdi.FindControl("txtSubCategoryCode")).Text != string.Empty && ((DropDownList)gdi.FindControl("ddlSubCategory")).SelectedValue != "Select")
                    {
                        //int lookupId = Convert.ToInt32(gdi["WCMV_LookupId"].Text);
                        DropDownList ddlSubCategory = (DropDownList)gdi.FindControl("ddlSubCategory");
                        int lookupId = Convert.ToInt32(ddlSubCategory.SelectedValue);
                        TextBox txtSubCategoryCode = ((TextBox)(gdi.FindControl("txtSubCategoryCode")));
                        TextBox txtMinInvestmentAmount = ((TextBox)(gdi.FindControl("txtMinInvestmentAmount")));
                        TextBox txtMaxInvestmentAmount = ((TextBox)(gdi.FindControl("txtMaxInvestmentAmount")));
                        TextBox txtSubCategoryId = ((TextBox)(gdi.FindControl("txtSubCategoryId")));



                        if (string.IsNullOrEmpty(txtMinInvestmentAmount.Text))
                        {
                            txtMinInvestmentAmount.Text = 0.ToString();
                        }

                        if (string.IsNullOrEmpty(txtMaxInvestmentAmount.Text))
                        {
                            txtMaxInvestmentAmount.Text = 0.ToString();
                        }
                        //  
                        if (txtSubCategoryId.Text == string.Empty)
                            CreateUpdateDeleteCategoryDetails(categoryId, 0, lookupId, txtSubCategoryCode.Text, Convert.ToInt64(txtMinInvestmentAmount.Text), Convert.ToInt64(txtMaxInvestmentAmount.Text), "Insert");
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
                result = CreateUpdateDeleteCategory(0, categoryId, txtCategoryName.Text, txtCategoryDescription.Text, txtChequePayableTo.Text, Convert.ToInt64(txtMinBidAmount.Text), Convert.ToInt64(txtMaxBidAmount.Text), discountType, Convert.ToDecimal(txtDiscountValue.Text), "Update");
                RadGrid rgSubCategories = (RadGrid)e.Item.FindControl("rgSubCategories");


                foreach (GridDataItem gdi in rgSubCategories.Items)
                {
                    //if (((CheckBox)gdi.FindControl("cbSubCategories")).Checked == true)
                    if (((TextBox)gdi.FindControl("txtSubCategoryCode")).Text != string.Empty && ((DropDownList)gdi.FindControl("ddlSubCategory")).SelectedValue != "Select")
                    {
                        //TextBox txtSubCategoryId = ((TextBox)(gdi.FindControl("txtSubCategoryId")));
                        TextBox txtSubCategoryCode = ((TextBox)(gdi.FindControl("txtSubCategoryCode")));
                        DropDownList ddlSubCategory = (DropDownList)gdi.FindControl("ddlSubCategory");
                        int lookupId = Convert.ToInt32(ddlSubCategory.SelectedValue);
                        TextBox txtSubCategoryId = ((TextBox)(gdi.FindControl("txtSubCategoryId")));
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
                        if (txtSubCategoryId.Text == string.Empty)
                            CreateUpdateDeleteCategoryDetails(categoryId, 0, lookupId, txtSubCategoryCode.Text, Convert.ToInt32(txtMinInvestmentAmount.Text), Convert.ToInt32(txtMaxInvestmentAmount.Text), "Insert");

                        else if (Convert.ToInt32(txtSubCategoryId.Text) > 0)
                            CreateUpdateDeleteCategoryDetails(categoryId, Convert.ToInt32(txtSubCategoryId.Text), lookupId, txtSubCategoryCode.Text, Convert.ToInt64(txtMinInvestmentAmount.Text), Convert.ToInt64(txtMaxInvestmentAmount.Text), "Update");



                    }
                }

                BindEligibleInvestorsGrid(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));


            }
            else if (e.CommandName == RadGrid.DeleteCommandName)
            {
                int categoryId = Convert.ToInt32(rgEligibleInvestorCategories.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString());
                int result = CreateUpdateDeleteCategory(0, categoryId, "", "", "", 0, 0, "", 0, "Delete");


            }


        }

        //protected void rgSubCategories_UpdateCommand(object source, GridCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName == "btnAddMore")
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
           long mInBidAmount, long maxBidAmount, string discountType, decimal discountValue, string CommandType)
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
                    onlineNCDBackOfficeVo.MInBidAmount = Convert.ToInt64(mInBidAmount.ToString());
                    onlineNCDBackOfficeVo.MaxBidAmount = Convert.ToInt64(maxBidAmount.ToString());
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
                    onlineNCDBackOfficeVo.MInBidAmount = Convert.ToInt64(mInBidAmount.ToString());
                    onlineNCDBackOfficeVo.MaxBidAmount = Convert.ToInt64(maxBidAmount.ToString());
                    onlineNCDBackOfficeVo.DiscuountType = discountType;
                    onlineNCDBackOfficeVo.DiscountValue = discountValue;
                    result = onlineNCDBackOfficeBo.UpdateCategory(onlineNCDBackOfficeVo, userVo.UserId);
                }
                else if (CommandType == "Delete")
                {
                    onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                    onlineNCDBackOfficeVo.IssueId = issueId;
                    onlineNCDBackOfficeVo.CatgeoryId = categoryId;
                    onlineNCDBackOfficeVo.CatgeoryName = investorCatgeoryName;
                    onlineNCDBackOfficeVo.CatgeoryDescription = investorCatgeoryDescription;
                    onlineNCDBackOfficeVo.ChequePayableTo = chequePayableTo;
                    onlineNCDBackOfficeVo.MInBidAmount = Convert.ToInt64(mInBidAmount.ToString());
                    onlineNCDBackOfficeVo.MaxBidAmount = Convert.ToInt64(maxBidAmount.ToString());
                    onlineNCDBackOfficeBo.DeleteIssueinvestor(categoryId);

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
            long mInBidAmount, long maxBidAmount)
        {
            try
            {
                onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                onlineNCDBackOfficeVo.IssueId = issueId;
                onlineNCDBackOfficeVo.CatgeoryName = investorCatgeoryName;
                onlineNCDBackOfficeVo.CatgeoryDescription = investorCatgeoryDescription;
                onlineNCDBackOfficeVo.ChequePayableTo = chequePayableTo;
                onlineNCDBackOfficeVo.MInBidAmount = int.Parse(mInBidAmount.ToString());
                onlineNCDBackOfficeVo.MaxBidAmount = int.Parse(maxBidAmount.ToString());
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

        private void CreateUpdateDeleteCategoryDetails(int catgeoryId, int subCatId, int lookUpId, string subTypeCode, long minInvestmentAmount, long maxInvestmentAmount, string CommandType)
        {
            bool result = false;
            try
            {
                if (CommandType == "Insert")
                {
                    onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                    onlineNCDBackOfficeVo.CatgeoryId = catgeoryId;
                    onlineNCDBackOfficeVo.SubCatgeoryId = subCatId;
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
                    onlineNCDBackOfficeVo.SubCatgeoryId = subCatId;
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


        protected void rgSeriesCategories_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem && e.Item.ItemIndex != -1)
            {
                //string Status = Convert.ToString(gvOrders.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WOS_OrderStepCode"].ToString());
                // EditCommandColumn colmatch = (EditCommandColumn)e.Item["Match"];

                GridDataItem item = (GridDataItem)e.Item;
                TextBox txtYieldAtCall = (TextBox)item["txtYieldAtCall"].Controls[0];

                // TextBox txtYieldAtCall = ((TextBox)(gdi.FindControl("txtYieldAtCall")));

                if (chkPutCallOption.Checked == true)
                {
                    txtYieldAtCall.Visible = true;
                }
                else
                {
                    txtYieldAtCall.Visible = false;

                }

            }
        }

        protected void rgSubCategories_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                DropDownList ddlSubCategory = (DropDownList)(item.FindControl("ddlSubCategory"));
                BindSubCateDDL(ddlSubCategory);


                DataTable dtRecords = new DataTable();
                dtRecords = (DataTable)Cache[userVo.UserId.ToString() + "SubCat"];
                RadGrid rgSubCategories = (RadGrid)(sender);
                if (dtRecords == null)
                    return;
                if (dtRecords.Rows.Count > 0)
                {
                    //if (dtRecords.Rows.Count != rgSubCategories.Items.Count)
                    //{
                    //    rgSubCategories.DataSource = dtRecords;
                    //    rgSubCategories.DataBind();
                    //}
                    int i = 0;
                    foreach (GridDataItem row in rgSubCategories.Items) // loops through each rows in RadGrid
                    {

                        DataRow dr = dtRecords.Rows[i];
                        foreach (GridColumn col in rgSubCategories.Columns) //loops through each column in RadGrid
                        {
                            if (col.UniqueName == "SubCategoryCode")
                            {
                                TextBox txtSubCategoryCode = (TextBox)(row[col.UniqueName].FindControl("txtSubCategoryCode"));
                                txtSubCategoryCode.Text = dr["SubCategoryCode"].ToString();
                            }
                            else if (col.UniqueName == "CustSubCategory")
                            {
                                DropDownList ddlSubCategory1 = (DropDownList)(row[col.UniqueName].FindControl("ddlSubCategory"));
                                ddlSubCategory1.SelectedValue = dr["CustSubCategory"].ToString();
                            }
                            else if (col.UniqueName == "MinInvestmentAmt")
                            {
                                TextBox txtMinInvestmentAmt = (TextBox)(row[col.UniqueName].FindControl("txtMinInvestmentAmount"));
                                txtMinInvestmentAmt.Text = dr["MinInvestmentAmt"].ToString();
                            }
                            else if (col.UniqueName == "MaxInvestmentAmt")
                            {
                                TextBox txtMinInvestmentAmt = (TextBox)(row[col.UniqueName].FindControl("txtMaxInvestmentAmount"));
                                txtMinInvestmentAmt.Text = dr["MaxInvestmentAmt"].ToString();
                            }
                            else if (col.UniqueName == "MaxInvestmentAmt")
                            {
                                TextBox txtMinInvestmentAmt = (TextBox)(row[col.UniqueName].FindControl("txtMaxInvestmentAmount"));
                                txtMinInvestmentAmt.Text = dr["MaxInvestmentAmt"].ToString();
                            }
                            else if (col.UniqueName == "SubCategoryId")
                            {
                                TextBox txtSubCategoryId = (TextBox)(row[col.UniqueName].FindControl("txtSubCategoryId"));
                                txtSubCategoryId.Text = dr["SubCategoryId"].ToString();
                            }


                        }
                        i = i + 1;

                    }

                }
            }

        }


        protected void rgSeriesCat_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem && e.Item.ItemIndex != -1)
            {


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
                    TextBox txtSequence = (TextBox)e.Item.FindControl("txtSequence");
                    DropDownList ddlInterestFrequency = (DropDownList)e.Item.FindControl("ddlInterestFrequency");
                    CheckBox chkBuyAvailability = (CheckBox)e.Item.FindControl("chkBuyAvailability");

                    foreach (GridColumn column in rgSeriesCat.Columns)
                    {
                        if (column.UniqueName == "YieldAtCall" && chkPutCallOption.Checked == false)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;

                        }

                    }
                    //int SeqNo = onlineNCDBackOfficeBo.GetSeriesSequence(Convert.ToInt32(txtIssueId.Text), advisorVo.advisorId);
                    //txtSequence.Text = SeqNo.ToString();
                    BindFrequency(ddlInterestFrequency);

                }
                else if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    GridEditFormItem editform = (GridEditFormItem)e.Item;
                    int seriesId = Convert.ToInt32(rgSeries.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AID_IssueDetailId"].ToString());
                    TextBox txtSereiesName = (TextBox)e.Item.FindControl("txtSereiesName");
                    TextBox txtTenure = (TextBox)e.Item.FindControl("txtTenure");
                    DropDownList ddlInterestFrequency = (DropDownList)e.Item.FindControl("ddlInterestFrequency");
                    CheckBox chkBuyAvailability = (CheckBox)e.Item.FindControl("chkBuyAvailability");
                    CheckBox chkredemptiondate = (CheckBox)e.Item.FindControl("chkredemptiondate");
                    CheckBox chkLockinperiod = (CheckBox)e.Item.FindControl("chkLockinperiod");
                    TextBox txtSequence = (TextBox)e.Item.FindControl("txtSequence");
                    DropDownList ddlTenureUnits = (DropDownList)e.Item.FindControl("ddlTenureUnits");


                    DropDownList ddlInterestType = (DropDownList)e.Item.FindControl("ddlInterestType");

                    RadGrid rgSeriesCat = (RadGrid)editform.FindControl("rgSeriesCat");

                    foreach (GridColumn column in rgSeriesCat.Columns)
                    {
                        if (column.UniqueName == "YieldAtCall" && chkPutCallOption.Checked == false)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    BindFrequency(ddlInterestFrequency);
                    BindCategory(rgSeriesCat, Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));
                    FillSeriesPopupControlsForUpdate(seriesId, txtSereiesName, txtTenure, ddlInterestFrequency, chkBuyAvailability, chkredemptiondate, chkLockinperiod, txtSequence, ddlInterestType,  ddlTenureUnits, rgSeriesCat);
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
                         DropDownList ddlInterestFrequency, CheckBox chkBuyAvailability, CheckBox chkredemptiondate, CheckBox chkLockinperiod, TextBox txtSequence, DropDownList ddlInterestType,DropDownList ddlTenureCycle, RadGrid rgSeriesCat)
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
                        ddlInterestFrequency.SelectedValue = dr["WCMV_LookupId"].ToString();
                        chkBuyAvailability.Checked = Convert.ToBoolean(dr["AID_BuyBackFacility"].ToString());
                        if (dr["AID_RedemptionApplicable"].ToString() != string.Empty)
                            chkredemptiondate.Checked = Convert.ToBoolean(dr["AID_RedemptionApplicable"]);
                        if (dr["AID_LockinPeriodApplicable"].ToString() != string.Empty)
                            chkLockinperiod.Checked = Convert.ToBoolean(dr["AID_LockinPeriodApplicable"].ToString());

                        if (dr["AID_TenureCycle"].ToString() != string.Empty)
                            ddlTenureCycle.SelectedValue =  dr["AID_TenureCycle"].ToString() ;
                        txtSequence.Text = dr["AID_Sequence"].ToString();
                        ddlInterestType.SelectedValue = dr["AID_InterestType"].ToString();
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
                            TextBox txtYieldAtBuyBack = (TextBox)gdi.FindControl("txtYieldAtBuyBack");
                            TextBox txtRedemptionDate = (TextBox)gdi.FindControl("txtRedemptionDate");
                            TextBox txtRedemptionAmount = (TextBox)gdi.FindControl("txtRedemptionAmount");
                            TextBox txtLockInPeriod = (TextBox)gdi.FindControl("txtLockInPeriod");

                            txtYieldAtBuyBack.Visible = chkBuyAvailability.Checked;
                            txtRedemptionDate.Visible = chkredemptiondate.Checked;
                            txtRedemptionAmount.Visible = chkredemptiondate.Checked;
                            txtLockInPeriod.Visible = chkLockinperiod.Checked;

                        }
                        foreach (GridDataItem gdi in rgSeriesCat.Items)
                        {
                            int grdcategoryId = Convert.ToInt32(gdi["AIIC_InvestorCatgeoryId"].Text);
                            rgSeriesCat.MasterTableView.GetColumn("AIIC_InvestorCatgeoryId").Visible = false;
                        

                            if (seriesCategoryId == grdcategoryId)
                            {
                                CheckBox cbSeriesCat = (CheckBox)gdi.FindControl("cbSeriesCat");
                                TextBox txtInterestRate = ((TextBox)(gdi.FindControl("txtInterestRate")));
                                TextBox txtAnnualizedYield = ((TextBox)(gdi.FindControl("txtAnnualizedYield")));
                                TextBox txtYieldAtCall = ((TextBox)(gdi.FindControl("txtYieldAtCall")));
                                TextBox txtRenCpnRate = ((TextBox)(gdi.FindControl("txtRenCpnRate")));
                                TextBox txtYieldAtBuyBack = (TextBox)gdi.FindControl("txtYieldAtBuyBack");
                                TextBox txtRedemptionDate = (TextBox)gdi.FindControl("txtRedemptionDate");
                                TextBox txtRedemptionAmount = (TextBox)gdi.FindControl("txtRedemptionAmount");
                                TextBox txtLockInPeriod = (TextBox)gdi.FindControl("txtLockInPeriod");
                                cbSeriesCat.Checked = true;
                                txtInterestRate.Text = dr["AIDCSR_DefaultInterestRate"].ToString();
                                txtAnnualizedYield.Text = dr["AIDCSR_AnnualizedYieldUpto"].ToString();
                                txtYieldAtCall.Text = dr["AIDCSR_YieldAtCall"].ToString();
                                txtRenCpnRate.Text = dr["AIDCSR_RenewCouponRate"].ToString();
                                txtYieldAtBuyBack.Text = dr["AIDCSR_YieldAtBuyBack"].ToString();
                                //if (chkBuyAvailability.Checked == true)
                                //{
                                //    txtRedemptionDate.Visible = true;
                                //    txtRedemptionAmount.Visible = true;
                                //}
                                //if (chkLockinperiod.Checked == true)
                                //{
                                //    txtLockInPeriod.Visible = true;
                                //}
                                //if (chkBuyAvailability.Checked == true)
                                //{
                                //    txtYieldAtBuyBack.Visible = true;
                                //}
                                txtRedemptionDate.Text = dr["AIDCSR_RedemptionDate"].ToString();
                                txtRedemptionAmount.Text = dr["AIDCSR_RedemptionAmount"].ToString();
                                txtLockInPeriod.Text = dr["AIDCSR_LockInPeriod"].ToString();
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

            //    if (txtNSECode.Text == "" && txtBSECode.Text == "")
            //    {
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CheckTextBoxes()", true);
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please Fill Atlist One Code NSE or BSE');", true);
            //        return;
            //    }
            //if (txtNSECode.Text != null && txtBSECode.Text != null)
            //{
            //     ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('You Can Enter Only One Code NSE or BSE');", true);
            //     return;
            //}
            //if(txtNSECode.Text != null || txtBSECode.Text != null)
            //{
            if (!string.IsNullOrEmpty(txtIssueId.Text))
            {
                if (chkIsActive.Checked == true)
                {
                    int categoryGridcount, serisecount;
                    categoryGridcount = rgEligibleInvestorCategories.Items.Count;
                    serisecount = rgSeries.Items.Count;

                    if (categoryGridcount > 0 || serisecount > 0)
                    {
                        UpdateOnlineEnblement(int.Parse(txtIssueId.Text));
                        chkIsActive.Checked = true;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Online Enablement done successfully.');", true);
                    }
                }

            }

            if (string.IsNullOrEmpty(txtIssueId.Text))
            {
                //if (txtBSECode.Text == string.Empty && txtNSECode.Text == string.Empty)
                //{
                //    rfvtxtBSECode.Enabled = true;
                //    rfvtxtNSECode.Enabled = true;
                //}
                //else
                //{
                if (NSCEBSCEcode())
                {
                    txtIssueId.Text = CreateIssue().ToString();
                    //if (Convert.ToInt32(txtIssueId.Text)>0)
                    //{               
                    SeriesAndCategoriesGridsVisiblity(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));
                    VisblityAndEnablityOfScreen("Submited");
                    btnSetUpSubmit.Enabled = true;
                }

            }

        }
        protected void btnProspect_Click(object sender, EventArgs e)
        {
            if (txtIssueId.Text != string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ManageRepository", "loadcontrol('ManageRepository','NCDProspect=NCDProspect&issueId=" + txtIssueId.Text + "');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Add Issue.');", true);

            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int result = UpdateIssue();
            SeriesAndCategoriesGridsVisiblity(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text));

        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            string type = "";
            if (Request.QueryString["action"] != null)
            {
                type = Request.QueryString["type"].ToString();
                if (type == "Closed")
                {
                    VisblityAndEnablityOfScreen("LnkEdit");
                    txtAllotmentDate.Enabled = true;
                    txtRevisionDates.Enabled = true;
                }

                else

                    VisblityAndEnablityOfScreen("LnkEdit");
                lnkBtnEdit.Visible = false;
            }
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
        }

        private void SeriesAndCategoriesGridsVisiblity(int issuerId, int issueId)
        {

            if (ddlProduct.SelectedValue == "IP")
            {
                pnlCategory.Visible = true;
                BindEligibleInvestorsGrid(issuerId, issueId);
            }
            else
            {

                pnlSeries.Visible = true;
                pnlCategory.Visible = true;
                BindEligibleInvestorsGrid(issuerId, issueId);
                BindSeriesGrid(issuerId, issueId);

            }
        }

        private int CreateIssue()
        {
            int issueId = 0;
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
                    onlineNCDBackOfficeVo.AssetInstrumentCategoryCode = ddlInstrCat.SelectedValue;
                    onlineNCDBackOfficeVo.AssetInstrumentSubCategoryCode = ddlSubInstrCategory.SelectedValue;
                }

                onlineNCDBackOfficeVo.IssueName = txtName.Text;
                onlineNCDBackOfficeVo.IssuerId = Convert.ToInt32(ddlIssuer.SelectedValue);

                onlineNCDBackOfficeVo.FromRange = Convert.ToInt64(txtFormRange.Text);
                onlineNCDBackOfficeVo.ToRange = Convert.ToInt64(txtToRange.Text);

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


                if (ddlCutOffTimeMinutes.SelectedValue == "MM")
                {
                    ddlCutOffTimeMinutes.SelectedValue = "00";
                }

                if (ddlCutOffTimeSeconds.SelectedValue == "SS")
                {
                    ddlCutOffTimeSeconds.SelectedValue = "00";
                }

                onlineNCDBackOfficeVo.CutOffTime = Convert.ToDateTime(ddlCutOffTimeHours.SelectedValue + ":" + ddlCutOffTimeMinutes.SelectedValue + ":" + ddlCutOffTimeSeconds.SelectedValue);//SelectedDate.Value.ToShortTimeString().ToString();

                onlineNCDBackOfficeVo.OpenTime = Convert.ToDateTime(ddlOpenTimeHours.SelectedValue + ":" + ddlOpenTimeMinutes.SelectedValue + ":" + ddlOpenTimeSeconds.SelectedValue); //SelectedDate.Value.ToShortTimeString().ToString();
                onlineNCDBackOfficeVo.CloseTime = Convert.ToDateTime(ddlCloseTimeHours.SelectedValue + ":" + ddlCloseTimeMinutes.SelectedValue + ":" + ddlCloseTimeSeconds.SelectedValue);//SelectedDate.Value.ToShortTimeString().ToString();

                if (!string.IsNullOrEmpty((txtRevisionDates.SelectedDate).ToString().Trim()))
                    onlineNCDBackOfficeVo.IssueRevis = DateTime.Parse(txtRevisionDates.SelectedDate.ToString());
                else
                    onlineNCDBackOfficeVo.IssueRevis = DateTime.MinValue;
                if (!string.IsNullOrEmpty((txtAllotmentDate.SelectedDate).ToString().Trim()))
                    onlineNCDBackOfficeVo.AllotmentDate = DateTime.Parse(txtAllotmentDate.SelectedDate.ToString());
                else
                    onlineNCDBackOfficeVo.AllotmentDate = DateTime.MinValue;
                if (!String.IsNullOrEmpty(txtTradingLot.Text))
                {
                    onlineNCDBackOfficeVo.TradingLot = Convert.ToDecimal(txtTradingLot.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.TradingLot = 0;
                }

                if (!string.IsNullOrEmpty(txtBiddingLot.Text))
                {
                    onlineNCDBackOfficeVo.BiddingLot = Convert.ToDecimal(txtBiddingLot.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.BiddingLot = 0;
                }
                if (!string.IsNullOrEmpty(txtMinAplicSize.Text))
                {
                    onlineNCDBackOfficeVo.MinApplicationSize = Convert.ToInt32(txtMinAplicSize.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.MinApplicationSize = 0;
                }
                if (!string.IsNullOrEmpty(txtIsPrefix.Text))
                {
                    onlineNCDBackOfficeVo.IsPrefix = Convert.ToInt32(txtIsPrefix.Text);
                }
                else
                {
                    onlineNCDBackOfficeVo.IsPrefix = 0;
                }

                onlineNCDBackOfficeVo.TradingInMultipleOf = Convert.ToInt32(txtTradingInMultipleOf.Text);


                if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
                {
                    onlineNCDBackOfficeVo.BankName = ddlBankName.SelectedValue;
                }
                else
                {
                    onlineNCDBackOfficeVo.BankName = "";
                }



                if (!string.IsNullOrEmpty(txtBankBranch.Text))
                {
                    onlineNCDBackOfficeVo.BankBranch = txtBankBranch.Text;
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
                if (chkPutCallOption.Checked == true)
                {
                    onlineNCDBackOfficeVo.PutCallOption = "y";
                }
                else
                {
                    onlineNCDBackOfficeVo.PutCallOption = "N";
                }
                if (chkTradebleExchange.Checked == true)
                {
                    onlineNCDBackOfficeVo.TradableExchange = 1;
                }
                else
                {
                    onlineNCDBackOfficeVo.TradableExchange = 0;
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

                if (ddlIssueType.SelectedValue == "BookBuilding")
                {
                    onlineNCDBackOfficeVo.BookBuildingPercentage = Convert.ToInt32(txtBookBuildingPer.Text);
                    onlineNCDBackOfficeVo.IsBookBuilding = 1;
                }
                else if (ddlIssueType.SelectedValue == "FixedPrice")
                {
                    onlineNCDBackOfficeVo.BookBuildingPercentage = 0;
                    onlineNCDBackOfficeVo.IsBookBuilding = 0;
                }

                //if (!string.IsNullOrEmpty(txtBookBuildingPer.Text))
                //{
                //    onlineNCDBackOfficeVo.BookBuildingPercentage = Convert.ToInt32(txtBookBuildingPer.Text);
                //    onlineNCDBackOfficeVo.IsBookBuilding = 1;
                //}
                //else
                //{
                //    onlineNCDBackOfficeVo.BookBuildingPercentage = 0;
                //    onlineNCDBackOfficeVo.IsBookBuilding = 0;
                //}
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
                if (!string.IsNullOrEmpty(txtSubBrokerCode.Text))
                {
                    onlineNCDBackOfficeVo.Subbrokercode = txtSubBrokerCode.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.Subbrokercode = "";
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

                if (!string.IsNullOrEmpty(ddlRegistrar.SelectedValue))
                {
                    onlineNCDBackOfficeVo.RtaSourceCode = ddlRegistrar.SelectedValue.ToString();
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

                if (!string.IsNullOrEmpty(txtBSECode.Text))
                {
                    onlineNCDBackOfficeVo.IsListedinBSE = 1;
                    onlineNCDBackOfficeVo.BSECode = txtBSECode.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.BSECode = "";
                }

                if (!string.IsNullOrEmpty(txtRegistrarAddress.Text))
                {

                    onlineNCDBackOfficeVo.RegistrarAddress = txtRegistrarAddress.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.RegistrarAddress = "";
                }

                if (!string.IsNullOrEmpty(txtRegistrarTelNO.Text))
                {

                    onlineNCDBackOfficeVo.RegistrarTelNo = txtRegistrarTelNO.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.RegistrarTelNo = "";
                }

                if (!string.IsNullOrEmpty(txtRegistrarFaxNo.Text))
                {

                    onlineNCDBackOfficeVo.RegistrarFaxNo = txtRegistrarFaxNo.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.RegistrarFaxNo = "";
                }

                if (!string.IsNullOrEmpty(txtInvestorGrievenceEmail.Text))
                {

                    onlineNCDBackOfficeVo.RegistrarGrievenceEmail = txtRegistrarFaxNo.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.RegistrarGrievenceEmail = "";
                }

                if (!string.IsNullOrEmpty(txtWebsite.Text))
                {

                    onlineNCDBackOfficeVo.RegistrarWebsite = txtWebsite.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.RegistrarWebsite = "";
                }

                if (!string.IsNullOrEmpty(txtISINNo.Text))
                {

                    onlineNCDBackOfficeVo.ISINNumber = txtISINNo.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.ISINNumber = "";
                }

                if (!string.IsNullOrEmpty(txtContactPerson.Text))
                {

                    onlineNCDBackOfficeVo.RegistrarContactPerson = txtWebsite.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.RegistrarContactPerson = "";
                }

                if (!string.IsNullOrEmpty(txtSBIRegistationNo.Text))
                {

                    onlineNCDBackOfficeVo.SBIRegistationNo = txtSBIRegistationNo.Text;
                }
                else
                {
                    onlineNCDBackOfficeVo.SBIRegistationNo = "";
                }


                if (NSCEBSCEcode())
                {
                    issueId = onlineNCDBackOfficeBo.CreateIssue(onlineNCDBackOfficeVo, advisorVo.advisorId);
                    if (issueId > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Issue added successfully.');", true);

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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:CreateIssue()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return issueId;
        }
        public bool NSCEBSCEcode()
        {
            bool isBool = true;
            int isBseExist = 0;
            int isNseExist = 0;
            int issuCheck;
            if (string.IsNullOrEmpty(txtIssueId.Text))
                issuCheck = 0;
            else
                issuCheck = Convert.ToInt32(txtIssueId.Text);

            if (string.IsNullOrEmpty(txtNSECode.Text) && string.IsNullOrEmpty(txtBSECode.Text))
                isBool = false;

            //if (txtNSECode.Text == txtBSECode.Text)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Enter Different NSE or BSE Code.');", true);
            //    isBool = false;
            //}
            onlineNCDBackOfficeBo.NSEandBSEcodeCheck(issuCheck, advisorVo.advisorId, txtNSECode.Text, txtBSECode.Text, ref  isBseExist, ref  isNseExist);
            if (isBseExist > 0 && txtBSECode.Text != string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('BSE Code Exist.');", true);
                isBool = false;
            }
            else if (isNseExist > 0 && txtNSECode.Text != string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('NSE Code Exist.');", true);
                isBool = false;
            }
            return isBool;
        }
        public void NseBsetextvalidation()
        {
            //if (txtBSECode.Text == null || txtNSECode.Text == null)
            //{

            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please Fill Atlist One Code NSE or BSE');", true);  
            //}
        }
        private void BankName()
        {

        }

        private void CreateUpdateDeleteSeriesCategories(int seriesId, int catgeoryId, double defaultInterestRate, double annualizedYieldUpto, double renCpnRate, double yieldAtCall, double yieldAtBuyBack, string redemptiondate, double redemptionAmount, int txtLockInPeriod, string commandType)
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
                    onlineNCDBackOfficeVo.RenCpnRate = renCpnRate;
                    onlineNCDBackOfficeVo.YieldAtCall = yieldAtCall;
                    onlineNCDBackOfficeVo.YieldAtBuyBack = yieldAtBuyBack;
                    onlineNCDBackOfficeVo.RedemptionDate = redemptiondate;
                    onlineNCDBackOfficeVo.RedemptionAmount = redemptionAmount;
                    onlineNCDBackOfficeVo.LockInPeriodapplicable = txtLockInPeriod;
                    onlineNCDBackOfficeVo.IssueId=Convert.ToInt32(txtIssueId.Text );
                    result = onlineNCDBackOfficeBo.CreateSeriesCategory(onlineNCDBackOfficeVo, userVo.UserId);
                }
                else if (commandType == "Update")
                {
                    onlineNCDBackOfficeVo = new OnlineNCDBackOfficeVo();
                    onlineNCDBackOfficeVo.SeriesId = seriesId;
                    onlineNCDBackOfficeVo.CatgeoryId = catgeoryId;
                    onlineNCDBackOfficeVo.DefaultInterestRate = defaultInterestRate;
                    onlineNCDBackOfficeVo.AnnualizedYieldUpto = annualizedYieldUpto;
                    onlineNCDBackOfficeVo.RenCpnRate = renCpnRate;
                    onlineNCDBackOfficeVo.YieldAtCall = yieldAtCall;
                    onlineNCDBackOfficeVo.YieldAtBuyBack = yieldAtBuyBack;
                    onlineNCDBackOfficeVo.RedemptionDate = redemptiondate;
                    onlineNCDBackOfficeVo.RedemptionAmount = redemptionAmount;
                    onlineNCDBackOfficeVo.LockInPeriodapplicable = txtLockInPeriod;
                    onlineNCDBackOfficeVo.IssueId = Convert.ToInt32(txtIssueId.Text);
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
            if (ddlIssuer.SelectedValue == "Select")
                return;
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

        //public static class SessionManager
        //{

        //        get
        //        {
        //            if (HttpContext.Current.Session["GetUserPermission"] == null)
        //            {
        //                //if session is null than set it to default value
        //                //here i set it 
        //                List<Entity.Permission> lstPermission = new List<Entity.Permission>();
        //                HttpContext.Current.Session["GetUserPermission"] = lstPermission;
        //            }
        //            return (List<Entity.Permission>)HttpContext.Current.Session["GetUserPermission"];
        //        }
        //        set
        //        {
        //            HttpContext.Current.Session["GetUserPermission"] = value;
        //        }

        //}
        protected void rgSubCategories_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid rgSubCategories = (RadGrid)sender; // Get reference to grid 
            DataTable dtSubCategory = new DataTable();
            if ((Cache[userVo.UserId.ToString() + "SubCat"] != null))
            {
                dtSubCategory = (DataTable)Cache[userVo.UserId.ToString() + "SubCat"];
                rgSubCategories.DataSource = dtSubCategory;
            }
            else
            {
                dtSubCategory = onlineNCDBackOfficeBo.GetSubCategory(Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text), 0).Tables[0];
                rgSubCategories.DataSource = dtSubCategory;
            }
        }



        private void SelectedLookUpIds(int lookupId)
        {


            ht.Add(lookupId, lookupId);

            ViewState["HtlookupId"] = ht;

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

        protected void rgSeriesCategories_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                RadGrid rgCategoriesDetails = (RadGrid)sender;
                int Serisesubcategoryid = int.Parse(rgCategoriesDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIDCSR_Id"].ToString());
                onlineNCDBackOfficeBo.DeleteSubcategory(Serisesubcategoryid);
            }

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
            RadGrid rgCategoriesDetails1 = (RadGrid)sender; // Get reference to grid 
            GridDataItem nesteditem = (GridDataItem)rgCategoriesDetails1.NamingContainer;
            //RadGrid rgCategoriesDetails2 = (RadGrid)rgCategoriesDetails1.NamingContainer; // Get reference to grid 

            int catgeoryId = 0;

            if (nesteditem.ItemIndex > 0)
            {
                catgeoryId = Convert.ToInt32(rgEligibleInvestorCategories.MasterTableView.DataKeyValues[nesteditem.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString());
            }
            DataTable dtCategoriesDetails = new DataTable();
            dtCategoriesDetails = onlineNCDBackOfficeBo.GetSubTypePerCategoryDetails(catgeoryId).Tables[0];
            rgCategoriesDetails1.DataSource = dtCategoriesDetails;
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


        private void GetAplRanges()
        {
            try
            {
                DataTable dtAplRanges = new DataTable();
                DataTable newDtAplRanges = new DataTable();


                if (Cache[userVo.UserId.ToString() + "Aplication"] != null)
                {
                    dtAplRanges = (DataTable)Cache[userVo.UserId.ToString() + "Aplication"];
                    if (dtAplRanges != null)
                    {
                        if (dtAplRanges.Rows.Count > 0)
                        {
                            newDtAplRanges = (from DataRow dr in dtAplRanges.Rows
                                              where dr["AIFR_IsActive"].ToString() == 1.ToString() || dr["AIFR_IsActive"].ToString() == 0.ToString()
                                              select dr).CopyToDataTable();

                            foreach (DataRow dr in newDtAplRanges.Rows)
                            {
                                txtFormRange.Text = dr["AIFR_From"].ToString();
                                txtToRange.Text = dr["AIFR_To"].ToString();
                            }
                        }
                    }
                    else
                    {

                        txtFormRange.Text = string.Empty;
                        txtToRange.Text = string.Empty;

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
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:GetAplRanges()");
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
                dtSubCategory = onlineNCDBackOfficeBo.GetSubCategory(issuerId, issueId, 0).Tables[0];
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

        private void BindAllInvestorTypesForUpdatePopUpCategory(RadGrid rgSubCategory, int issuerId, int issueId, int categoryId)
        {
            try
            {
                DataTable dtSubCategory = new DataTable();
                dtSubCategory = onlineNCDBackOfficeBo.GetAllInvestorTypes(issuerId, issueId, categoryId).Tables[0];
                rgSubCategory.DataSource = dtSubCategory;
                rgSubCategory.DataBind();

                foreach (GridDataItem gdi in rgSubCategory.Items)
                {
                    DropDownList ddlSubCategory = (DropDownList)gdi.FindControl("ddlSubCategory");
                    if (ddlSubCategory.SelectedValue == "Select")
                        BindSubCateDDL(ddlSubCategory);
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
                string issuerId = rgIssuer.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PI_IssuerId"].ToString();

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



            if (e.Item is GridPagerItem)
            {
                GridPagerItem pager = (GridPagerItem)e.Item;
                ((e.Item as GridPagerItem).FindControl("PageSizeComboBox") as RadComboBox).Enabled = false;
            }
            //if (e.CommandName == RadGrid.DeleteCommandName)
            //{

            //    OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
            //    GridDataItem dataItem = (GridDataItem)e.Item;
            //    int TradeBusinessId = Convert.ToInt32(gvTradeBusinessDate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WTBD_DayName"].ToString());
            //}
        }
        protected void rgAplication_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode) && e.Item.ItemIndex != -1)
            {
                GridEditFormItem editform = (GridEditFormItem)e.Item;
                int formrangeId = Convert.ToInt32(rgAplication.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIFR_Id"].ToString());

                TextBox txtFrom = (TextBox)editform.FindControl("txtFrom");
                TextBox txtTo = (TextBox)editform.FindControl("txtTo");
                DataTable dtIssuer = new DataTable();
                dtIssuer = onlineNCDBackOfficeBo.GetActiveRange(advisorVo.advisorId, Convert.ToInt32(txtIssueId.Text)).Tables[0];
                DataTable tbl = (from DataRow dr in dtIssuer.Rows
                                 where dr["AIFR_Id"].ToString() == formrangeId.ToString()
                                 select dr).CopyToDataTable();

                foreach (DataRow dr in tbl.Rows)
                {
                    txtFrom.Text = dr["AIFR_From"].ToString();
                    txtTo.Text = dr["AIFR_To"].ToString();
                }
            }

        }

        //protected void rgSubCategories_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //     }
        protected void rgEligibleInvestorCategories_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
                {
                    GridEditFormItem editform = (GridEditFormItem)e.Item;
                    TextBox txtIssueName = (TextBox)editform.FindControl("txtIssueName");
                    txtIssueName.Text = txtName.Text;
                    e.Item.OwnerTableView.EnableViewState = true;
                    if (ddlProduct.SelectedValue == "IP")
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

                    foreach (GridDataItem gdi in rgSubCategories.Items)
                    {
                        DropDownList ddlSubCategory = (DropDownList)gdi.FindControl("ddlSubCategory");
                        BindSubCateDDL(ddlSubCategory);
                    }


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
                    Button btnAddMore = (Button)e.Item.FindControl("btnAddMore");
                    btnAddMore.Visible = true;

                    if (ddlProduct.SelectedValue == "IP")
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

                    BindAllInvestorTypesForUpdatePopUpCategory(rgSubCategories, Convert.ToInt32(ddlIssuer.SelectedValue), Convert.ToInt32(txtIssueId.Text), categoryId);
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

                        //lookupId = Convert.ToInt32(dr["WCMV_LookupId"].ToString());

                        if (!string.IsNullOrEmpty(dr["WCMV_LookupId"].ToString()))
                        {
                            lookupId = Convert.ToInt32(dr["WCMV_LookupId"].ToString());
                        }
                        else
                        {
                            return;
                        }




                    }
                    //for (int i = 0; i <= dtCategory.Rows.Count - 1; i++)
                    //{
                    int i = 0;
                    foreach (DataRow dr in dtCategory.Rows)
                    {

                        // GridDataItem gdi = rgSubCategories.Items[i];
                        DropDownList ddlSubCategory = (DropDownList)rgSubCategories.Items[i].FindControl("ddlSubCategory");
                        BindSubCateDDL(ddlSubCategory);
                        //lookupId.ToString();
                        TextBox txtSubCategoryId = ((TextBox)(rgSubCategories.Items[i].FindControl("txtSubCategoryId")));
                        TextBox txtSubCategoryCode = ((TextBox)(rgSubCategories.Items[i].FindControl("txtSubCategoryCode")));
                        TextBox txtMinInvestmentAmount = ((TextBox)(rgSubCategories.Items[i].FindControl("txtMinInvestmentAmount")));
                        TextBox txtMaxInvestmentAmount = ((TextBox)(rgSubCategories.Items[i].FindControl("txtMaxInvestmentAmount")));
                        CheckBox cbSubCategories = (CheckBox)rgSubCategories.Items[i].FindControl("cbSubCategories");
                        cbSubCategories.Checked = true;

                        if (dr["WCMV_LookupId"].ToString() != string.Empty)
                            ddlSubCategory.SelectedValue = dr["WCMV_LookupId"].ToString();
                        txtSubCategoryCode.Text = dr["AIICST_InvestorSubTypeCode"].ToString();
                        txtMinInvestmentAmount.Text = dr["AIICST_MinInvestmentAmount"].ToString();
                        txtMaxInvestmentAmount.Text = dr["AIICST_MaxInvestmentAmount"].ToString();
                        txtSubCategoryId.Text = dr["AIICST_Id"].ToString();
                        i = i + 1;
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


                ddlCutOffTimeHours.DataSource = dt;
                ddlCutOffTimeHours.DataValueField = dt.Columns["Hours"].ToString();
                ddlCutOffTimeHours.DataTextField = dt.Columns["Hours"].ToString();
                ddlCutOffTimeHours.DataBind();

            }
            ddlOpenTimeHours.Items.Insert(0, new ListItem("Hours", "HH"));
            ddlCloseTimeHours.Items.Insert(0, new ListItem("Hours", "HH"));
            ddlCutOffTimeHours.Items.Insert(0, new ListItem("Hours", "HH"));

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

        //private void BindBankNames()
        //{
        //    DataTable dtBankNames = new DataTable();
        //    dtBankNames = commonLookupBo.GetWERPLookupMasterValueList(7000, 0);
        //    if (dtBankNames.Rows.Count > 0)
        //    {
        //        ddlBankName.DataSource = dtBankNames;
        //        ddlBankName.DataValueField = dtBankNames.Columns["WCMV_LookupId"].ToString();
        //        ddlBankName.DataTextField = dtBankNames.Columns["WCMV_Name"].ToString();
        //        ddlBankName.DataBind();
        //    }
        //}

        private void BindNcdCategory()
        {
            DataTable dtCategory = new DataTable();
            dtCategory = onlineNCDBackOfficeBo.BindNcdCategory("SubInstrumentCat", "").Tables[0];
            if (dtCategory.Rows.Count > 0)
            {
                ddlSubInstrCategory.DataSource = dtCategory;
                ddlSubInstrCategory.DataValueField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlSubInstrCategory.DataTextField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlSubInstrCategory.DataBind();
            }
            ddlSubInstrCategory.Items.Insert(0, new ListItem("Select", "Select"));

        }
        protected void ddlSubInstrCategory_Selectedindexchanged(object sender, EventArgs e)
        {
            if (ddlSubInstrCategory.SelectedValue == "Select")
            {
                return;
            }
            BindInstCate(ddlSubInstrCategory.SelectedValue);
        }
        private void BindInstCate(string subInstCat)
        {
            DataTable dtCategory = new DataTable();
            dtCategory = onlineNCDBackOfficeBo.BindNcdCategory("InstrumentCat", subInstCat).Tables[0];
            if (dtCategory.Rows.Count > 0)
            {
                ddlInstrCat.DataSource = dtCategory;
                ddlInstrCat.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                ddlInstrCat.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                ddlInstrCat.DataBind();
            }
        }

        private void BindFrequency(DropDownList ddlFrequency)
        {
            DataTable dtFrequency = new DataTable();
            dtFrequency = onlineNCDBackOfficeBo.GetFrequency();


            if (dtFrequency.Rows.Count > 0)
            {
                ddlFrequency.DataSource = dtFrequency;
                ddlFrequency.DataValueField = dtFrequency.Columns["WCMV_LookupId"].ToString();
                ddlFrequency.DataTextField = dtFrequency.Columns["WCMV_Name"].ToString();
                ddlFrequency.DataBind();
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



                ddlCutOffTimeMinutes.DataSource = dt;
                ddlCutOffTimeMinutes.DataValueField = dt.Columns["Minutes"].ToString();
                ddlCutOffTimeMinutes.DataTextField = dt.Columns["Minutes"].ToString();
                ddlCutOffTimeMinutes.DataBind();

                ddlCutOffTimeSeconds.DataSource = dt;
                ddlCutOffTimeSeconds.DataValueField = dt.Columns["Minutes"].ToString();
                ddlCutOffTimeSeconds.DataTextField = dt.Columns["Minutes"].ToString();
                ddlCutOffTimeSeconds.DataBind();



            }
            ddlOpenTimeMinutes.Items.Insert(0, new ListItem("Minutes", "MM"));
            ddlCloseTimeMinutes.Items.Insert(0, new ListItem("Minutes", "MM"));
            ddlOpenTimeSeconds.Items.Insert(0, new ListItem("Seconds", "SS"));
            ddlCloseTimeSeconds.Items.Insert(0, new ListItem("Seconds", "SS"));
            ddlCutOffTimeMinutes.Items.Insert(0, new ListItem("Minutes", "MM"));
            ddlCutOffTimeSeconds.Items.Insert(0, new ListItem("Seconds", "SS"));

            ddlOpenTimeMinutes.SelectedValue = "00";
            ddlCloseTimeMinutes.SelectedValue = "00";
            ddlOpenTimeSeconds.SelectedValue = "00";
            ddlCloseTimeSeconds.SelectedValue = "00";
            ddlCutOffTimeMinutes.SelectedValue = "00";
            ddlCutOffTimeSeconds.SelectedValue = "00";

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

        protected void ddlSubCategory_Selectedindexchanged(object sender, EventArgs e)
        {
            //DropDownList ddlSubCat = (DropDownList)sender;
            //int lookupid =Convert.ToInt32( ddlSubCat.SelectedValue);
            //int rowindex1 = ((GridDataItem)((DropDownList)sender).NamingContainer).RowIndex;
            //int rowindex = (rowindex1 / 2) - 1;
            int lookupid = 0;
            int gdlookupid = 0;
            DropDownList ddlSubCat = (DropDownList)sender;
            if (ddlSubCat.SelectedValue == "Select")
                lookupid = 0;
            else
                lookupid = Convert.ToInt32(ddlSubCat.SelectedValue);

            int rowindex1 = ((GridDataItem)((DropDownList)sender).NamingContainer).RowIndex;
            int rowindex = (rowindex1 / 2) - 1;



            GridTableView rg = (GridTableView)((GridDataItem)((DropDownList)sender).NamingContainer).NamingContainer;
            RadGrid rg1 = (RadGrid)rg.OwnerGrid;


            for (int i = 0; i < rg1.Items.Count; i++)
            {
                if (i == rowindex)
                {
                    continue;
                }
                else
                {
                    DropDownList ddlSubCategory = (DropDownList)rg1.Items[i].FindControl("ddlSubCategory");
                    if (ddlSubCategory.SelectedValue == "Select")
                        gdlookupid = 0;
                    else
                        gdlookupid = Convert.ToInt32(ddlSubCategory.SelectedValue);
                    if (gdlookupid == lookupid)
                    {
                        ((DropDownList)rg1.Items[rowindex].FindControl("ddlSubCategory")).SelectedValue = "Select";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Sub category exist.');", true);
                    }
                }

            }






            //if (rg1.Items.Count == 1)
            //    return ;

            //foreach (GridDataItem gdi in rg1.Items)
            //{

            //    //for (int i = 0; i <= rg1.Items.Count; i++)
            //    //{

            //    //}
            //   DropDownList ddlSubCategory = (DropDownList)gdi.FindControl("ddlSubCategory");
            //    if(ddlSubCategory.SelectedValue=="Select")
            //        gdlookupid=0;
            //    else
            //        gdlookupid=Convert.ToInt32(ddlSubCategory.SelectedValue);
            //    if (gdlookupid == lookupid)
            //    {
            //        ddlSubCategory.SelectedValue = "Select";
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Sub category exist.');", true);
            //    }

            //}
            //  RadGrid rg = ((DropDownList)sender).Controls[0] as RadGrid  ;
            //GridEditableItem editedItem = ddlSubCat.NamingContainer as GridEditableItem;
            //   RadGrid rg =(RadGrid) rg.FindControl("rgSubCategories");

            //  RadGrid rgSeriesCat = (RadGrid)editedItem.FindControl("rgSeriesCat");

            //GridEditableItem item = (GridEditableItem)ddlSubCat.NamingContainer;
            // RadGrid rg = (RadGrid)ddlSubCat.Parent;
            //(item.FindControl("rgSubCategories"));
            // BindSubCateDDL(ddlSubCat);

        }
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
            trModeofIssue.Visible = false;
            trMaxQty.Visible = true;
            txtMaxQty.Visible = false;
            Label15.Visible = false;
            trIssueqtySize.Visible = false;
            trTradinglotBidding.Visible = false;
            txtIsPrefix.Visible = true;
            //Ipo
            trIssueTypes.Visible = false;
            trBookBuildingAndCapprices.Visible = false;
            trSyndicateAndMemberCodes.Visible = false;
            trRegistrarAndNoofBidsAlloweds.Visible = false;
            trIsActiveandPutCallOption.Visible = true;

            trRegistrarAddressAndTelNo.Visible = false;
            trRegistrarFaxNoAndInvestorGrievenceEmail.Visible = false;
            trWebsiteAndContactPerson.Visible = false;
         
            //both
            //trFloorAndFixedPrices.Visible = true;
            trExchangeCode.Visible = false;
            trRatingAndModeofTrading.Visible = true;
            trSBIRegistationNoAndISINNumber.Visible = true;
            

            if (product == "NCD")
            {
                trNomineeReQuired.Visible = true;

                trRatingAndModeofTrading.Visible = true;
                trModeofIssue.Visible = true;
                trFloorAndFixedPrices.Visible = false;
                txtMaxQty.Visible = false;
                Label15.Visible = false;
                //chkIsActive.Enabled = false;
                trExchangeCode.Visible = true;
                tdlblCategory.Visible = true;
                tdddlCategory.Visible = true;
                txtIsPrefix.Visible = true;
                tdlb1MinQty.Visible = false;
                tdltxtMinQty.Visible = false;
                tdlb1ModeofTrading.Visible = true;
                tdtxtModeofTrading.Visible = true;
                tdlb1SBIRegistationNo.Visible = false ;
                tdtxtSBIRegistationNo.Visible = false;
                lb1Rating.Text = "Rating:";
            }
            else if (product == "IP")
            {
                trIssueTypes.Visible = true;
                tdlblCategory.Visible = false;
                tdddlCategory.Visible = false;
                trMaxQty.Visible = true;
                txtMaxQty.Visible = true;
                Label15.Visible = true;
                trExchangeCode.Visible = true;
                trTradinglotBidding.Visible = true;
                trIssueqtySize.Visible = true;
                tdlb1MinQty.Visible = true;
                tdltxtMinQty.Visible = true;
                trRegistrarAddressAndTelNo.Visible = true;
                trRegistrarFaxNoAndInvestorGrievenceEmail.Visible = true;
                trWebsiteAndContactPerson.Visible = true;
                trSBIRegistationNoAndISINNumber.Visible = true;
                //chkIsActive.Visible = true;
                //chkIsActive.Enabled = false;
                tdlb1ModeofTrading.Visible = false;
                tdtxtModeofTrading.Visible = false;
                tdlb1SBIRegistationNo.Visible = true;
                tdtxtSBIRegistationNo.Visible = true;
                lb1Rating.Text = "Grading:";
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

        private void CreateUpdateDeleteSyndicateMaster(int syndicateId, string syndicateCode, string syndicateName, string commandType)
        {
            int i = onlineNCDBackOfficeBo.CreateUpdateDeleteSyndicateMaster(syndicateId, syndicateCode, syndicateName, commandType);
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
        private void lookupids(ref Dictionary<string, string> RefOrders)
        {

            SubCategories = (Dictionary<string, string>)ViewState["SelectedCheckboxesForSubCategories"];
            RefOrders.Add("lookupId1", SubCategories["lookupId"].ToString());
        }

        private void CreateUpdateDeleteAplication(int fromRange, int toRange, int adviserId, int issueId, int formRangeId, string commandType)
        {
            string status = string.Empty;
            int i = onlineNCDBackOfficeBo.GetValidateFrom(fromRange, adviserId, issueId, formRangeId, ref status);
            if (status == "Not_Validated")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Pls Use From Range Greater Than Previous Active To Range.');", true);
                return;
            }
            else if (status == "Dont Fill")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('You Cannot Create New Range.');", true);
                return;
            }

            i = onlineNCDBackOfficeBo.CreateUpdateDeleteAplicationNos(fromRange, toRange, adviserId, issueId, formRangeId, commandType, ref status);
            if (i > 0)
            {
                if (commandType == "INSERT" & status == "Refill")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Inserted Successfully.');", true);
                else if (commandType == "INSERT" & status == "Filled")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Cant insert,Already Being used.');", true);
                else if (commandType == "INSERT" & status == "Not Used")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Active Range Exist.');", true);

                else if (commandType == "UPDATE" & status == "DontUpdate")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Cant Update.');", true);
                else if (commandType == "UPDATE" & status == "To Range Updated")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('from Range You cant Update.To Range Updated Successfully.');", true);
                else if (commandType == "UPDATE")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Updated Successfully.');", true);
                else if (commandType == "DELETE" & status == "Filled")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Cant delete,Already Being used.');", true);
                else if (commandType == "DELETE")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Deleted Successfully.');", true);

                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Deleted Successfully.');", true);
            }
        }

        protected void rgSyndicate_ItemCommand(object source, GridCommandEventArgs e)
        {
            //int SyndicateId;
            //if (e.CommandName == RadGrid.PerformInsertCommandName)
            //{
            //    TextBox txtSyndicateCode = (TextBox)e.Item.FindControl("txtSyndicateCode");
            //    TextBox txtSyndicatename = (TextBox)e.Item.FindControl("txtSyndicatename");

            //    CreateUpdateDeleteSyndicateMaster(0, txtSyndicateCode.Text, txtSyndicatename.Text, "INSERT");
            //}
            //else if (e.CommandName == RadGrid.UpdateCommandName)
            //{
            //    SyndicateId = Convert.ToInt32(rgSyndicate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PI_SyndicateId"].ToString());
            //    TextBox txtSyndicateCode = (TextBox)e.Item.FindControl("txtSyndicateCode");
            //    TextBox txtSyndicatename = (TextBox)e.Item.FindControl("txtSyndicatename");

            //    CreateUpdateDeleteSyndicateMaster(SyndicateId, txtSyndicateCode.Text, txtSyndicatename.Text, "UPDATE");

            //}
            //else if (e.CommandName == RadGrid.DeleteCommandName)
            //{
            //    SyndicateId = Convert.ToInt32(rgSyndicate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PI_SyndicateId"].ToString());
            //    CreateUpdateDeleteSyndicateMaster(SyndicateId, "", "", "DELETE");

            //}
            //BindSyndicateGrid();
        }

        private void BindApplGrid()
        {
            try
            {
                if (txtIssueId.Text == string.Empty)
                    txtIssueId.Text = "0";

                DataTable dtGetActiveRange = new DataTable();
                dtGetActiveRange = onlineNCDBackOfficeBo.GetActiveRange(advisorVo.advisorId, Convert.ToInt32(txtIssueId.Text)).Tables[0];
                rgAplication.DataSource = dtGetActiveRange;

                if (Cache[userVo.UserId.ToString() + "Aplication"] != null)
                    Cache.Remove(userVo.UserId.ToString() + "Aplication");
                Cache.Insert(userVo.UserId.ToString() + "Aplication", dtGetActiveRange);

                rgAplication.DataBind();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "NCDIssuesetup.ascx.cs:BindApplGrid()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
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
            BindIssuer();

        }
        //rgAplication_ItemCommand

        protected void rgAplication_ItemCommand(object source, GridCommandEventArgs e)
        {
            int formRangeId = 0;
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                TextBox txtFrom = (TextBox)e.Item.FindControl("txtFrom");
                TextBox txtTo = (TextBox)e.Item.FindControl("txtTo");

                CreateUpdateDeleteAplication(Convert.ToInt32(txtFrom.Text), Convert.ToInt32(txtTo.Text), advisorVo.advisorId, Convert.ToInt32(txtIssueId.Text), 0, "INSERT");
                BindApplGrid();
            }
            else if (e.CommandName == RadGrid.UpdateCommandName)
            {
                formRangeId = Convert.ToInt32(rgAplication.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIFR_Id"].ToString());
                TextBox txtFrom = (TextBox)e.Item.FindControl("txtFrom");
                TextBox txtTo = (TextBox)e.Item.FindControl("txtTo");

                CreateUpdateDeleteAplication(Convert.ToInt32(txtFrom.Text), Convert.ToInt32(txtTo.Text), advisorVo.advisorId, Convert.ToInt32(txtIssueId.Text), formRangeId, "UPDATE");
                BindApplGrid();

            }
            else if (e.CommandName == RadGrid.DeleteCommandName)
            {
                if (rgAplication.Items.Count > 1)
                {
                    formRangeId = Convert.ToInt32(rgAplication.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIFR_Id"].ToString());
                    CreateUpdateDeleteAplication(0, 0, 0, 0, formRangeId, "DELETE");
                }
                BindApplGrid();

            }

        }
        protected void rgCategoriesDetails_ItemCommand(object sender, GridCommandEventArgs e)
        {
            int categoryId = 0;
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                RadGrid rgCategoriesDetails = (RadGrid)sender;
                int Investorsubcategoryid = int.Parse(rgCategoriesDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AIICST_Id"].ToString());
                onlineNCDBackOfficeBo.Deleteinvestmentcategory(Investorsubcategoryid);
            }
            //    GridDataItem gdi = (GridDataItem)sender;
            //    if (!string.IsNullOrEmpty(rgEligibleInvestorCategories.MasterTableView.DataKeyValues[gdi.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString()))
            //    {
            //        categoryId = int.Parse(rgEligibleInvestorCategories.MasterTableView.DataKeyValues[gdi.ItemIndex]["AIIC_InvestorCatgeoryId"].ToString());
            //    }
            //    else
            //    {
            //        categoryId = 0;
            //    }
            //    //RadGrid rgCategoriesDetails1 = (RadGrid)gdi.FindControl("rgCategoriesDetails");
            //    //if (e.CommandName == RadGrid.RebindGridCommandName)
            //    //{
            //    //    rgCategoriesDetails.Rebind();
            //    //}
            //BindCategoriesDetailsGrid(categoryId, RadGrid rgCategoriesDetails);
        }
        protected void btnIssuerPopUp_Click(object sender, ImageClickEventArgs e)
        {
            radIssuerPopUp.VisibleOnPageLoad = true;
            // BindIssuer();
            BindIssuerGrid();
        }

        protected void btnIssuerPopClose_Click(object sender, EventArgs e)
        {
            radIssuerPopUp.VisibleOnPageLoad = false;
            //BindIssuer();

            BindIssuerGrid();
        }

        protected void btnImageActivRange_Click(object sender, ImageClickEventArgs e)
        {

            radAplicationPopUp.VisibleOnPageLoad = true;
            BindApplGrid();
        }

        protected void BtnActivRangeClose_Click(object sender, EventArgs e)
        {
            radAplicationPopUp.VisibleOnPageLoad = false;
            GetAplRanges();

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

        private void BindSyndicateGrid()
        {
            try
            {
                //DataTable dtIssuer = new DataTable();
                //dtIssuer = onlineNCDBackOfficeBo.GetIssuer().Tables[0];
                //rgIssuer.DataSource = dtIssuer;
                //rgIssuer.DataBind();
                //if (Cache[userVo.UserId.ToString() + "Issuer"] != null)
                //    Cache.Remove(userVo.UserId.ToString() + "Issuer");
                //Cache.Insert(userVo.UserId.ToString() + "Issuer", dtIssuer);
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

        protected void rgAplication_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtAplication = new DataTable();
            dtAplication = (DataTable)Cache[userVo.UserId.ToString() + "Aplication"];
            if (dtAplication != null)
            {
                rgAplication.DataSource = dtAplication;
            }

        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    radIssuerPopUp.VisibleOnPageLoad = false;
        //}



        protected void cbSeriesCat_changed(object sender, EventArgs e)
        {
            //  CheckBox cbSeriesCat = (CheckBox)sender;
            ////  GridDataItem editedItem = (GridDataItem)cbSeriesCat.NamingContainer;
            //  GridEditableItem editedItem = cbSeriesCat.NamingContainer as GridEditableItem;
            // RadGrid rgSeriesCat = (RadGrid)editedItem.FindControl("rgSeriesCat");
            //  foreach (GridDataItem gdi in rgSeriesCat.Items)
            //  {
            //      if (cbSeriesCat.Checked == true)
            //      {
            //          RequiredFieldValidator req = (RequiredFieldValidator)gdi.FindControl("RequiredFieldValidator26");
            //          req.Visible = true;
            //      }
            //  }

        }

        protected void chkOnlineEnablement_changed(object sender, EventArgs e)
        {
            int categoryGridcount, serisecount;
            categoryGridcount = rgEligibleInvestorCategories.Items.Count;
            serisecount = rgSeries.Items.Count;

            if ((categoryGridcount == 0 || serisecount == 0) && ddlProduct.SelectedValue == "NCD")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please Fill All The series.');", true);
                chkIsActive.Checked = false;
                // 
            }
            else if (ddlProduct.SelectedValue == "IP" && categoryGridcount == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please Fill All The series.');", true);
                chkIsActive.Checked = false;
            }

        }
        protected void chkIsPrefix_changed(object sender, EventArgs e)
        {
            IsPrefixEnablement(chkIsPrefix.Checked);
        }

        private void IsPrefixEnablement(bool isPrefix)
        {
            chkIsPrefix.Checked = isPrefix;
            txtIsPrefix.Visible = isPrefix;
        }

        protected void cbRemoveSubCategories_changed(object sender, EventArgs e)
        {
            //int i = 0;
            //CheckBox cb = (CheckBox)sender;
            //GridDataItem editedItem = cb.NamingContainer as GridDataItem;
            //RadGrid rgSubCategories = (RadGrid)editedItem.FindControl("rgSubCategories");

            //foreach (GridDataItem gvRow in rgSubCategories.Items)
            //{
            //    CheckBox chk = (CheckBox)gvRow.FindControl("cbSubCategories");
            //    if (chk.Checked)
            //    {
            //        i++;
            //        int rowindex1 = ((GridDataItem)((CheckBox)sender).NamingContainer).RowIndex;
            //        int rowindex = (rowindex1 / 2) - 1;
            //        LinkButton lbButton = (LinkButton)sender;
            //        GridDataItem item = (GridDataItem)lbButton.NamingContainer;
            //        gvRow.Controls.RemoveAt(rowindex1);
            //    }

            //}

        }
        protected void cbSubCategories_changed(object sender, EventArgs e)
        {


            //int i = 0;
            //CheckBox cb = (CheckBox)sender;


            //GridEditableItem editedItem = cb.NamingContainer as GridEditableItem;
            //RadGrid rgSubCategories = (RadGrid)editedItem.FindControl("rgEligibleInvestorCategories");


            //foreach (GridDataItem gvRow in rgSubCategories.Items)
            //{
            //    CheckBox chk = (CheckBox)gvRow.FindControl("cbSubCategories");
            //    if (chk.Checked)
            //    {
            //        i++;
            //    }

            //}
            //if (i == 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please Fill Category section.');", true);

            //}


        }
        protected void chkRedemptiondate_changed(object sender, EventArgs e)
        {
            CheckBox chkredemptiondate = (CheckBox)sender;
            GridEditableItem editedItem = chkredemptiondate.NamingContainer as GridEditableItem;
            RadGrid rgSeriesCat = (RadGrid)editedItem.FindControl("rgSeriesCat");
            foreach (GridDataItem gdi in rgSeriesCat.Items)
            {
                if (chkredemptiondate.Checked == true)
                {
                    TextBox txtRedemptionDate = (TextBox)gdi.FindControl("txtRedemptionDate");
                    TextBox txtRedemptionAmount = (TextBox)gdi.FindControl("txtRedemptionAmount");
                    txtRedemptionDate.Visible = true;
                    txtRedemptionAmount.Visible = true;
                }
                else
                {
                    if (chkredemptiondate.Checked == false)
                    {
                        TextBox txtRedemptionDate = (TextBox)gdi.FindControl("txtRedemptionDate");
                        TextBox txtRedemptionAmount = (TextBox)gdi.FindControl("txtRedemptionAmount");

                        //if (txtRedemptionDate != null && txtRedemptionAmount !=null)
                        txtRedemptionDate.Visible = false;
                        txtRedemptionAmount.Visible = false;
                    }
                }

            }
        }
        protected void chkLockinperiod_changed(object sender, EventArgs e)
        {
            CheckBox chkLockinperiod = (CheckBox)sender;
            GridEditableItem editedItem = chkLockinperiod.NamingContainer as GridEditableItem;
            RadGrid rgSeriesCat = (RadGrid)editedItem.FindControl("rgSeriesCat");
            foreach (GridDataItem gdi in rgSeriesCat.Items)
            {
                if (chkLockinperiod.Checked == true)
                {
                    TextBox txtLockInPeriod = (TextBox)gdi.FindControl("txtLockInPeriod");

                    txtLockInPeriod.Visible = true;
                }
                else
                {
                    if (chkLockinperiod.Checked == false)
                    {
                        TextBox txtLockInPeriod = (TextBox)gdi.FindControl("txtLockInPeriod");

                        if (txtLockInPeriod != null)
                            txtLockInPeriod.Visible = false;

                    }
                }

            }
        }


        protected void chkBuyAvailability_changed(object sender, EventArgs e)
        {
            CheckBox chkBuyAvailability = (CheckBox)sender;
            GridEditableItem editedItem = chkBuyAvailability.NamingContainer as GridEditableItem;
            RadGrid rgSeriesCat = (RadGrid)editedItem.FindControl("rgSeriesCat");
            //foreach (GridColumn column in rgSeriesCat.Columns)
            //{
            //    if (column.UniqueName == "YieldAtBuyBack")
            //    {
            //        column.Visible = false;
            //    }
            //    else
            //    {
            //        column.Visible = true;

            //    }
            //}
            foreach (GridDataItem gdi in rgSeriesCat.Items)
            {
                if (chkBuyAvailability.Checked == true)
                {
                    TextBox txtYieldAtBuyBack = (TextBox)gdi.FindControl("txtYieldAtBuyBack");
                    txtYieldAtBuyBack.Visible = true;
                }
                else
                {
                    if (chkBuyAvailability.Checked == false)
                    {
                        //gdi["YieldAtBuyBack"].Text = "";
                        TextBox txtYieldAtBuyBack = (TextBox)gdi.FindControl("txtYieldAtBuyBack");
                        if (txtYieldAtBuyBack != null)
                            txtYieldAtBuyBack.Visible = false;
                    }
                }

            }




            //foreach (GridDataItem gdi in rgSeries.Items)
            //{
            //    if (((CheckBox)gdi.FindControl("chkBuyAvailability")).Checked == true)
            //    {
            //        TextBox txtYieldAtBuyBack = (TextBox)gdi.FindControl("txtYieldAtBuyBack");
            //        txtYieldAtBuyBack.Visible = true;
            //    }
            //    else
            //    {
            //        TextBox txtYieldAtBuyBack = (TextBox)gdi.FindControl("txtYieldAtBuyBack");
            //        if (txtYieldAtBuyBack != null)
            //            txtYieldAtBuyBack.Visible = false;

            //    }
            //}

        }

        private void UpdateOnlineEnblement(int issueId)
        {
            onlineNCDBackOfficeBo.UpdateOnlineEnablement(issueId);

        }
    }
}
