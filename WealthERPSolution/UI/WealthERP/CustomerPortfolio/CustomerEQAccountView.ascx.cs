﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using WealthERP.Base;
using VoUser;
using System.Data;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;

namespace WealthERP.CustomerPortfolio
{
    public partial class CustomerEQAccountView : System.Web.UI.UserControl
    {
        static int portfolioId;
        CustomerVo customerVo = new CustomerVo();
        UserVo userVo = new UserVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        List<CustomerAccountsVo> FolioList = new List<CustomerAccountsVo>();
        CustomerTransactionBo CustomerTransactionBo = new CustomerTransactionBo();
        CustomerAccountsVo FolioVo = new CustomerAccountsVo();
        int FolioId = 0;
        int EQAccountId = 0;
        Dictionary<int, int> genDictPortfolioDetails = new Dictionary<int, int>();
        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            if (!IsPostBack)
            {
                this.Page.Culture = "en-GB";
                portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                BindPortfolioDropDown();
                this.BindFolioGridView();
            }

        }
        private void BindFolioGridView()
        {
            try
            {
                DataTable dtEQAcc = new DataTable();
                customerVo = (CustomerVo)Session["CustomerVo"];

                FolioList = CustomerTransactionBo.GetCustomerEQAccount(portfolioId);

                // lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                if (FolioList == null)
                {
                    lblMessage.Visible = true;
                    //lblCurrentPage.Visible = false;
                    //lblTotalRows.Visible = false;
                    //DivPager.Visible = false;
                    gvEQAcc.DataSource = null;
                    gvEQAcc.DataBind();
                }
                else
                {
                    lblMessage.Visible = false;
                    //lblTotalRows.Visible = true;
                    //lblCurrentPage.Visible = true;
                    //DivPager.Visible = true;
                  

                    dtEQAcc.Columns.Add("AccountId");
                    dtEQAcc.Columns.Add("Broker Name");
                    dtEQAcc.Columns.Add("Trade No");
                    dtEQAcc.Columns.Add("Broker Del Percent");
                    dtEQAcc.Columns.Add("Broker Spec Percent");
                    dtEQAcc.Columns.Add("Other Charges");
                    dtEQAcc.Columns.Add("A/C Opening Date");


                    DataRow drEQAcc;

                    for (int i = 0; i < FolioList.Count; i++)
                    {
                        drEQAcc = dtEQAcc.NewRow();
                        FolioVo = new CustomerAccountsVo();
                        FolioVo = FolioList[i];
                        drEQAcc["AccountId"] = FolioVo.AccountId.ToString();
                        drEQAcc["Broker Name"] = FolioVo.BrokerName.ToString();
                        drEQAcc["Trade No"] = FolioVo.TradeNum.ToString();
                        drEQAcc["Broker Del Percent"] = FolioVo.BrokerageDeliveryPercentage.ToString();
                        drEQAcc["Broker Spec Percent"] = FolioVo.BrokerageSpeculativePercentage.ToString();
                        drEQAcc["Other Charges"] = FolioVo.OtherCharges.ToString();
                        if (FolioVo.AccountOpeningDate != DateTime.MinValue)
                            drEQAcc["A/C Opening Date"] = FolioVo.AccountOpeningDate.ToShortDateString();
                        else
                            drEQAcc["A/C Opening Date"] = string.Empty;
                        dtEQAcc.Rows.Add(drEQAcc);
                    }
                    gvEQAcc.DataSource = dtEQAcc;
                    gvEQAcc.DataBind();

                }

                if (Cache["EQAccountDetails" + portfolioId] == null)
                {
                    Cache.Insert("EQAccountDetails" + portfolioId, dtEQAcc);
                }
                else
                {
                    Cache.Remove("EQAccountDetails" + portfolioId);
                    Cache.Insert("EQAccountDetails" + portfolioId, dtEQAcc);
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
                FunctionInfo.Add("Method", "CustomerMFFolioView.ascx:BindFolioGridView()");
                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        private void BindPortfolioDropDown()
        {
            customerVo = (CustomerVo)Session["customerVo"];
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();

            ddlPortfolio.SelectedValue = portfolioId.ToString();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                genDictPortfolioDetails.Add(int.Parse(dr["CP_PortfolioId"].ToString()), int.Parse(dr["CP_IsMainPortfolio"].ToString()));
            }

            ddlPortfolio.SelectedValue = portfolioId.ToString();


            var keyValuePair = genDictPortfolioDetails.FirstOrDefault(x => x.Key == portfolioId);

            hdnIsMainPortfolio.Value = keyValuePair.Value.ToString();
            Session["genDictPortfolioDetails"] = genDictPortfolioDetails;
            hdnIsCustomerLogin.Value = userVo.UserType;
        }
        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            BindFolioGridView();

            if (Session["genDictPortfolioDetails"] != null)
            {
                genDictPortfolioDetails = (Dictionary<int, int>)Session["genDictPortfolioDetails"];
            }
            var keyValuePair = genDictPortfolioDetails.FirstOrDefault(x => x.Key == portfolioId);
            //int value = keyValuePair.Value;

            hdnIsMainPortfolio.Value = keyValuePair.Value.ToString();
            hdnIsCustomerLogin.Value = userVo.UserType;
        }
        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //DropDownList ddlAction = (DropDownList)sender;
                //GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
                //int selectedRow = gvr.RowIndex;

                RadComboBox ddlAction = (RadComboBox)sender;
                GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;

                EQAccountId = int.Parse(gvEQAcc.MasterTableView.DataKeyValues[selectedRow - 1]["AccountId"].ToString());
                Session["AccountId"] = EQAccountId;
                Session["EQAccountVoRow"] = CustomerTransactionBo.GetCustomerEQAccountDetails(EQAccountId, portfolioId);
                if (ddlAction.SelectedValue.ToString() == "Edit")
                {
                     if (hdnIsCustomerLogin.Value == "Customer" && hdnIsMainPortfolio.Value == "1")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Permisssion denied for Manage Portfolio !!');", true);                     
                     else
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountAdd','action=Edit');", true);
                }
                if (ddlAction.SelectedValue.ToString() == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountAdd','action=View');", true);
                }
                if (ddlAction.SelectedValue.ToString() == "Delete")
                {
                    if (hdnIsCustomerLogin.Value == "Customer" && hdnIsMainPortfolio.Value == "1")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Permisssion denied for Manage Portfolio !!');", true);
                    else
                    {
                        bool CheckTradeAccAssociationWithTransactions;
                        CheckTradeAccAssociationWithTransactions = CustomerTransactionBo.CheckEQTradeAccNoAssociatedWithTransactions(EQAccountId);

                        if (CheckTradeAccAssociationWithTransactions == true)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Trade Account can not be deleted as some Transactions are Associated with this Trade Account Number.');", true);
                        }
                        else if (CheckTradeAccAssociationWithTransactions == false)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "ShowAlertToDelete();", true);
                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerMFFolioView.ascx:ddlAction_OnSelectedIndexChange()");
                object[] objects = new object[1];
                objects[0] = FolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnTradeNoAssociation_Click(object sender, EventArgs e)
        {
            string HiddenVal = hdnStatusValue.Value;
            if(Session["AccountId"] != "")
                EQAccountId = int.Parse(Session["AccountId"].ToString());
            if (HiddenVal == "1")
            {
                bool DeleteTradeAccAssociationCheck;
                DeleteTradeAccAssociationCheck = CustomerTransactionBo.DeleteTradeAccount(EQAccountId);

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountView','none');", true);
            }

        }


        protected void gvEQAcc_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtProcessLogDetails = new DataTable();
            dtProcessLogDetails = (DataTable)Cache["EQAccountDetails" + portfolioId];
            gvEQAcc.DataSource = dtProcessLogDetails;
        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvEQAcc.ExportSettings.OpenInNewWindow = true;
            gvEQAcc.ExportSettings.IgnorePaging = true;
            gvEQAcc.ExportSettings.HideStructureColumns = true;
            gvEQAcc.ExportSettings.ExportOnlyData = true;
            gvEQAcc.ExportSettings.FileName = "EQ Trade Account Details";
            gvEQAcc.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvEQAcc.MasterTableView.ExportToExcel();
        }
    }
}