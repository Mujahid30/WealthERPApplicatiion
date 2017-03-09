using System;
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
    public partial class CustomerEQAccountRateView : System.Web.UI.UserControl
    {
        static int portfolioId;
        CustomerVo customerVo = new CustomerVo();
        UserVo userVo = new UserVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        List<CustomerAccountsVo> FolioList = new List<CustomerAccountsVo>();
        CustomerAccountBo customeraccountBo = new CustomerAccountBo();
        CustomerAccountsVo FolioVo = new CustomerAccountsVo();
        int FolioId = 0;
        int EQCebId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            if (!IsPostBack)
            {

                portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                BindRateGrid();

            }

        }

        public void BindRateGrid()
        {
            try
            {
                DataTable dtEQAccRate = new DataTable();


                FolioList = customeraccountBo.GetEquityRates(portfolioId);
                if (FolioList == null)
                {
                    lblMessage.Visible = true;
                    gvEQAccRate.DataSource = null;
                    gvEQAccRate.DataBind();

                }
                else
                {
                    lblMessage.Visible = false;

                    dtEQAccRate.Columns.Add("CebId");
                    dtEQAccRate.Columns.Add("AccountId");
                    dtEQAccRate.Columns.Add("Trade No");
                    dtEQAccRate.Columns.Add("Transaction Mode");
                    dtEQAccRate.Columns.Add("Transaction Type");
                    dtEQAccRate.Columns.Add("Rate");
                    dtEQAccRate.Columns.Add("Sebi Turn Over Fee");
                    dtEQAccRate.Columns.Add("Transaction Charges");
                    dtEQAccRate.Columns.Add("Stamp Charges");
                    dtEQAccRate.Columns.Add("STT");
                    dtEQAccRate.Columns.Add("Service Tax");
                    dtEQAccRate.Columns.Add("Start Date");
                    dtEQAccRate.Columns.Add("End Date");
                    DataRow drEQAcc;

                    for (int i = 0; i < FolioList.Count; i++)
                    {
                        drEQAcc = dtEQAccRate.NewRow();
                        FolioVo = new CustomerAccountsVo();
                        FolioVo = FolioList[i];
                        drEQAcc["CebId"] = FolioVo.CebId;
                        drEQAcc["AccountId"] = FolioVo.AccountId;
                        drEQAcc["Trade No"] = FolioVo.TradeNum;
                        if (FolioVo.TransactionMode == 1)
                        {
                            drEQAcc["Transaction Mode"] = "Speculative";
                        }
                        else if (FolioVo.TransactionMode == 0)
                        {
                            drEQAcc["Transaction Mode"] = "Delivery";
                        }
                        if (FolioVo.Type == "B")
                        {
                            drEQAcc["Transaction Type"] = "Buy";
                        }
                        else if (FolioVo.Type == "S")
                        {
                            drEQAcc["Transaction Type"] = "Sell";
                        }
                        drEQAcc["Rate"] = FolioVo.Rate;



                        drEQAcc["Sebi Turn Over Fee"] = FolioVo.SebiTurnOverFee.ToString();
                        drEQAcc["Transaction Charges"] = FolioVo.TransactionCharges.ToString();
                        drEQAcc["Stamp Charges"] = FolioVo.StampCharges.ToString();
                        drEQAcc["STT"] = FolioVo.Stt.ToString();
                        drEQAcc["Service Tax"] = FolioVo.ServiceTax.ToString();

                        if (FolioVo.StartDate != DateTime.MinValue)
                            drEQAcc["Start Date"] = FolioVo.StartDate.ToShortDateString();
                        else
                            drEQAcc["Start Date"] = string.Empty;

                        if (FolioVo.EndDate != DateTime.MinValue)
                            drEQAcc["End Date"] = FolioVo.EndDate.ToShortDateString();
                        else
                            drEQAcc["End Date"] = string.Empty;
                        dtEQAccRate.Rows.Add(drEQAcc);
                    }
                    gvEQAccRate.DataSource = dtEQAccRate;
                    gvEQAccRate.DataBind();
                }
                if (Cache["EQAccountDetails" + portfolioId] == null)
                {
                    Cache.Insert("EQAccountDetails" + portfolioId, dtEQAccRate);
                }
                else
                {
                    Cache.Remove("EQAccountDetails" + portfolioId);
                    Cache.Insert("EQAccountDetails" + portfolioId, dtEQAccRate);
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

        protected void gvEQAcc_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtProcessLogDetails = new DataTable();
            dtProcessLogDetails = (DataTable)Cache["EQAccountDetails" + portfolioId];
            gvEQAccRate.DataSource = dtProcessLogDetails;
        }
        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                RadComboBox ddlAction = (RadComboBox)sender;
                GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;

                EQCebId = int.Parse(gvEQAccRate.MasterTableView.DataKeyValues[selectedRow - 1]["CebId"].ToString());
                Session["CebId"] = EQCebId;
                Session["EQAccountVoRow"] = customeraccountBo.GetEquityRate(EQCebId);
                if (ddlAction.SelectedValue.ToString() == "Edit")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountRateAdd','action=Edit');", true);
                }
                if (ddlAction.SelectedValue.ToString() == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountRateAdd','action=View');", true);
                }
                if (ddlAction.SelectedValue.ToString() == "Delete")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "ShowAlertToDelete();", true);
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
            if (Session["CebId"] != "")
                EQCebId = int.Parse(Session["CebId"].ToString());
            if (HiddenVal == "1")
            {
                bool DeleteTradeAccAssociationCheck;
                DeleteTradeAccAssociationCheck = customeraccountBo.DeleteEqRate(EQCebId);

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountRateView','none');", true);
            }

        }

    }
}