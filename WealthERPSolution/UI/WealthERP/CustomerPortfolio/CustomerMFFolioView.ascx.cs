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
using BoCustomerProfiling;
using BoAdvisorProfiling;
using Telerik.Web.UI;

namespace WealthERP.CustomerPortfolio
{
    public partial class CustomerMFFolioView : System.Web.UI.UserControl
    {
        static int portfolioId;
        RMVo rmVo = new RMVo();
        DataSet dsCustomerPortfolioList = new DataSet();
        CustomerVo customerVo = new CustomerVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        List<CustomerAccountsVo> FolioList = new List<CustomerAccountsVo>();
        CustomerTransactionBo CustomerTransactionBo = new CustomerTransactionBo();
        CustomerAccountsVo FolioVo = new CustomerAccountsVo();
        int FolioId = 0;
       
        //protected override void OnInit(EventArgs e)
        //{
        //    //((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
        //    //mypager.EnableViewState = true;
        //    //base.OnInit(e);
        //}

        //public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        //{
        //    //GetPageCount();
          
        //    this.BindFolioGridView();
        //}

        //private void GetPageCount()
        //{
        //    //string upperlimit;
        //    //string lowerlimit;
        //    //int rowCount = 0;
        //    //if (hdnRecordCount.Value != "")
        //    //    rowCount = Convert.ToInt32(hdnRecordCount.Value);
        //    //if (rowCount > 0)
        //    //{

        //    //    int ratio = rowCount / 30;
        //    //    mypager.PageCount = rowCount % 30 == 0 ? ratio : ratio + 1;
        //    //    mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
        //    //    lowerlimit = (((mypager.CurrentPage - 1) * 30) + 1).ToString();
        //    //    upperlimit = (mypager.CurrentPage * 30).ToString();
        //    //    if (mypager.CurrentPage == mypager.PageCount)
        //    //        upperlimit = hdnRecordCount.Value;
        //    //    string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
        //    //    lblCurrentPage.Text = PageRecords;

        //    //    hdnCurrentPage.Value = mypager.CurrentPage.ToString();
        //    //}
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session["CustomerVo"];
            
            if (!IsPostBack)
            {

                Cache.Remove("FolioDetails" + customerVo.CustomerId.ToString());

                this.Page.Culture = "en-GB";
                portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                BindPortfolioDropDown();
                this.BindFolioGridView();
                trFolioStatus.Visible = false;
            }            
            
        }

        private void BindFolioGridView()
        {
            int Count;
            try
            {
                FolioList = CustomerTransactionBo.GetCustomerMFFolios(portfolioId, customerVo.CustomerId);

                // lblTotalRows.Text = hdnRecordCount.Value = count.ToString();

                DataTable dtMFFolio = new DataTable();

                dtMFFolio.Columns.Add("FolioId");
                dtMFFolio.Columns.Add("ADUL_ProcessId");
                dtMFFolio.Columns.Add("Folio No");
                dtMFFolio.Columns.Add("AMC Name");
                dtMFFolio.Columns.Add("Name");// original costumer name from folio uploads
                dtMFFolio.Columns.Add("Mode Of Holding");
                dtMFFolio.Columns.Add("A/C Opening Date", typeof(DateTime));
                dtMFFolio.Columns.Add("CMFA_IsOnline",typeof(string));


                DataRow drMFFolio;

                if (FolioList == null)
                {
                    imgBtnrgHoldings.Visible = false;
                    trSelectAction.Visible = false;
                    //trErrorMsg.Visible = true;
                    //lblCurrentPage.Visible = false;
                    //lblTotalRows.Visible = false;
                    //DivPager.Visible = false;
                    gvMFFolio.DataSource = dtMFFolio;
                    gvMFFolio.DataBind();                    
                    //btnTransferFolio.Visible = false;
                    //btnMoveFolio.Visible = false;
                }
                else
                {
                    imgBtnrgHoldings.Visible = true;
                    if (Session[SessionContents.CurrentUserRole].ToString() == "Customer")
                    {
                        trSelectAction.Visible = false;
                    }
                    else
                    {
                        trSelectAction.Visible = true;
                    }
                    //trErrorMsg.Visible = false;
                    //lblTotalRows.Visible = true;
                    //lblCurrentPage.Visible = true;
                    //DivPager.Visible = true;
                   

                    for (int i = 0; i < FolioList.Count; i++)
                    {
                        drMFFolio = dtMFFolio.NewRow();
                        FolioVo = new CustomerAccountsVo();
                        FolioVo = FolioList[i];
                        drMFFolio[0] = FolioVo.AccountId.ToString();
                        if (FolioVo.ProcessId == 0)
                        {
                            drMFFolio[1] = "N/A";
                        }
                        else
                            drMFFolio[1] = FolioVo.ProcessId.ToString();

                        drMFFolio[2] = FolioVo.AccountNum.ToString();
                        if(FolioVo.AMCName!=null)
                        drMFFolio[3] = FolioVo.AMCName.ToString();
                        if(FolioVo.ModeOfHolding =="Jointly")
                        {

                        }
                        drMFFolio[4] = FolioVo.Name.ToString();
                        if(FolioVo.ModeOfHolding!=null)
                        drMFFolio[5] = FolioVo.ModeOfHolding.ToString();
                        if (FolioVo.AccountOpeningDate != DateTime.MinValue)
                            drMFFolio[6] = FolioVo.AccountOpeningDate.ToShortDateString();
                        if (FolioVo.IsOnline != null)
                        {
                            if (FolioVo.IsOnline==1)    
                                drMFFolio["CMFA_IsOnline"] = "Yes";
                            else if (FolioVo.IsOnline==0)
                                drMFFolio["CMFA_IsOnline"] = "No";
                        }
                        //else
                        //    drMFFolio[5] = String.Empty;
                        dtMFFolio.Rows.Add(drMFFolio);
                    }
                    gvMFFolio.DataSource = dtMFFolio;
                    
                    gvMFFolio.DataBind();

                    if (Cache["FolioDetails" + customerVo.CustomerId.ToString()] == null)
                    {
                        Cache.Insert("FolioDetails" + customerVo.CustomerId.ToString(), dtMFFolio);
                    }
                    else
                    {
                        Cache.Remove("FolioDetails" + customerVo.CustomerId.ToString());
                        Cache.Insert("FolioDetails" + customerVo.CustomerId.ToString(), dtMFFolio);
                    }
                }



                //lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
                //if (Count > 0)
                ////    DivPager.Style.Add("display", "visible");
                //else
                //{
                //    DivPager.Style.Add("display", "none");
                //    lblCurrentPage.Text = "";
                //    lblCurrentPage.Text = "";
                //}

                //this.GetPageCount();
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

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            DataTable dtFolioDetails = new DataTable();
            dtFolioDetails = (DataTable)Cache["FolioDetails" + customerVo.CustomerId.ToString()];
            gvMFFolio.DataSource = dtFolioDetails;
            gvMFFolio.ExportSettings.OpenInNewWindow = true;
            gvMFFolio.ExportSettings.IgnorePaging = true;
            gvMFFolio.ExportSettings.HideStructureColumns = true;
            gvMFFolio.ExportSettings.ExportOnlyData = true;
            gvMFFolio.ExportSettings.FileName = "Folio Details";
            gvMFFolio.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvMFFolio.MasterTableView.ExportToExcel();
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
        }
        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {

            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            BindFolioGridView();

        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlAction = (DropDownList)sender;
                GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
                int selectedRow = gvr.ItemIndex + 1;;
                FolioId = Convert.ToInt32(gvMFFolio.MasterTableView.DataKeyValues[selectedRow - 1]["FolioId"].ToString());
                Session["FolioId"] = FolioId;
                Session["FolioVo"] = CustomerTransactionBo.GetCustomerMFFolioDetails(FolioId);
                if (ddlAction.SelectedValue.ToString() == "Edit")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerMFAccountAdd','?action=Edit');", true);
                }
                if (ddlAction.SelectedValue.ToString() == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerMFAccountAdd','?action=View');", true);
                }
                if (ddlAction.SelectedValue.ToString() == "Delete")
                {
                   // bool CheckMFFolioNoAssociationWithTransactions;
                    //CheckMFFolioNoAssociationWithTransactions = CustomerTransactionBo.CheckMFFOlioAssociatedWithTransactions(FolioId);

                    //if (CheckMFFolioNoAssociationWithTransactions == true)
                    //{
                      //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Folio can not be deleted as some Transactions are Associated with this Folio Number.');", true);
                   // }
                    //else if (CheckMFFolioNoAssociationWithTransactions == false)
                    //{
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "ShowAlertToDelete();", true);
                   // }

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

        //protected void btnTransferFolio_Click(object sender, EventArgs e)
        //{
        //    tblTransferFolio.Visible = true;
        //    //btnTransferFolio.Visible = false;
        //    rmVo = (RMVo)Session[SessionContents.RmVo];
        //    txtCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();

        //}

        protected void btnMoveFolio_Click(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            tblMoveFolio.Visible = true;
            bindDropdownPickPortfolio(int.Parse(customerVo.CustomerId.ToString()));
            
        }

        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            CustomerBo customerBo = new CustomerBo();
            if (txtCustomerId.Value != string.Empty)
            {
                DataTable dt = customerBo.GetCustomerPanAddress(int.Parse(txtCustomerId.Value));
                DataRow dr = dt.Rows[0];

                txtPanParent.Text = dr["C_PANNum"].ToString();
                txtAddress.Text = dr["C_Adr1Line1"].ToString();

                trCustomerDetails.Visible = true;
                trCustomerAddress.Visible = true;
                bindFolioDropDown(int.Parse(txtCustomerId.Value));

            }

        }

        protected void bindFolioDropDown(int customerId)
        {
            
            DataSet folioDs;
            CustomerBo customerBo = new CustomerBo();
            folioDs = new DataSet();
            folioDs = portfolioBo.GetCustomerPortfolio(customerId);
            //folioDs = customerBo.GetCustomerPortfolioList(customerId);
            ddlAdvisorBranchList.DataSource = folioDs;
            ddlAdvisorBranchList.DataValueField = folioDs.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlAdvisorBranchList.DataBind();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            PortfolioBo portfolioBo = new PortfolioBo();
            int isBankAssociatedWithOtherTransactions=0;
            String statusMsg = string.Empty;
            int totalFoliosMoved = 0;
            CustomerPortfolioVo portfolioVo = new CustomerPortfolioVo();
            if (txtCustomerId.Value != string.Empty)
            {
                string portfolio = ddlAdvisorBranchList.SelectedValue;
                portfolioVo = portfolioBo.GetCustomerDefaultPortfolio1((Convert.ToInt32(txtCustomerId.Value)), portfolio);
                if (portfolioVo.PortfolioId < 1)
                {
                    lblMessage.Text = "No default Portfolio found for the selected customer.";
                    //trErrorMsg.Visible = true;
                    tblTransferFolio.Visible = false;
                    tblMoveFolio.Visible = false;
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Please select a customer.";
                //trErrorMsg.Visible = true;
                tblTransferFolio.Visible = false;
                tblMoveFolio.Visible = false;
                return;
            }


            foreach (GridDataItem dr in gvMFFolio.Items)
            {

                CheckBox checkBox = (CheckBox)dr.FindControl("chkBox");
                if (checkBox.Checked)
                {

                    int MFAccountId = Convert.ToInt32(gvMFFolio.MasterTableView.Items[dr.ItemIndex].GetDataKeyValue("FolioId").ToString());
                    bool isUpdated = PortfolioBo.TransferFolio(MFAccountId, portfolioVo.PortfolioId, isBankAssociatedWithOtherTransactions);
                    if (isUpdated)
                        totalFoliosMoved++;
                    else
                    {
                        //statusMsgFailure.InnerText = statusMsg;
                        //statusMsg += "Error occurred while moving folio with mutual fund accountId " + MFAccountId;
                        //trFailure.Visible = true;
                        //statusMsgSuccess.Visible = false;
                        //statusMsgFailure.Visible = true;

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Cannot transfer this folio the bank is associate with some other transactions');", true);



                    }
                    //lblstatusMsgFailure.Visible = true;
                }
            }
            if (totalFoliosMoved > 0)
            {
                
                statusMsg += "Total Folios moved = " + totalFoliosMoved;
                statusMsgSuccess.InnerText = statusMsg;
                trFailure.Visible = true;
                statusMsgSuccess.Visible = true;
                statusMsgFailure.Visible = false;

                //    lblstatusMsgSuccess.Text += "<br/>Total Folios moved = " + totalFoliosMoved + ".<br/>";
                //lblstatusMsgSuccess.Visible = true;
                //lblMessage.Text = statusMsg;
                //lblMessage.Visible = true;
                //lblTransferMsg.Text = statusMsg;
                //divMessage.Attributes.Add("class", "yellow-box");
                //if (trTransferMsg.Visible == false)

                
                //    trTransferMsg.Visible = true;
            }
            BindFolioGridView();
        }

        protected void btnFolioAssociation_Click(object sender, EventArgs e)
        {
            string HiddenVal = hdnStatusValue.Value;
            if (Session["FolioId"] != "")
                FolioId = int.Parse(Session["FolioId"].ToString());

            if (HiddenVal == "1")
            {
                bool isDeleteMFFolioId;
                isDeleteMFFolioId = CustomerTransactionBo.DeleteMFFolio(FolioId);
                if (isDeleteMFFolioId)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerMFFolioView','none');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", @"alert('Folio can not be deleted as some Transactions are Associated with this Folio Number.');", true);
                }
            }

        }

        private void bindDropdownPickPortfolio(int customerId)
        {
            PortfolioBo portfolioBo = new PortfolioBo();
            try
            {
                if (txtCustomerId.Value != null)
                {
                    dsCustomerPortfolioList = portfolioBo.GetCustomerPortfolio(customerId);
                    ddlPickPortfolio.DataSource = dsCustomerPortfolioList;
                    ddlPickPortfolio.DataValueField = dsCustomerPortfolioList.Tables[0].Columns["CP_PortfolioId"].ToString();
                    ddlPickPortfolio.DataTextField = dsCustomerPortfolioList.Tables[0].Columns["CP_PortfolioName"].ToString();
                    ddlPickPortfolio.DataBind();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        private CheckBox GetchkBox()
        {
            CheckBox chkBoxRow = new CheckBox();
            if (gvMFFolio.TemplateControl != null)
            {
                if ((CheckBox)gvMFFolio.TemplateControl.FindControl("chkBox") != null)
                {
                    chkBoxRow = (CheckBox)gvMFFolio.TemplateControl.FindControl("chkBox");
                }
            }
            else
                chkBoxRow = null;

            return chkBoxRow;
        }

        protected void btnSubmitMoveFolio_Click(object sender, EventArgs e)
        {
            try
            {
                customerVo = (CustomerVo)Session["customerVo"];
                CustomerPortfolioVo portfolioVo = new CustomerPortfolioVo();
                CheckBox chkBox = new CheckBox();
                chkBox = GetchkBox();
                int accountID;              
                int customerId = customerVo.CustomerId;
                int TofolioId = Convert.ToInt32(ddlPickPortfolio.SelectedValue);
                bool PortFolioUpdate;
                string statusMsg=string.Empty;
                
                foreach (GridDataItem dr in gvMFFolio.Items)
                {
                    int selectedRow=0;
                    //int rowIndex = dr.RowIndex;
                    //DataKey dKey = gvMFFolio.DataKeys[rowIndex];
                  
                    if (((CheckBox)dr.FindControl("chkBox")).Checked == true)
                    {
                        AdvisorBranchBo adviserBranchBo = new AdvisorBranchBo();
                        accountID = Convert.ToInt32(gvMFFolio.MasterTableView.DataKeyValues[selectedRow]["FolioId"].ToString());
                        //string folioNo = gvMFFolio.MasterTableView.DataKeyValues[selectedRow]["FolioId"].ToString();
                        string folioNo = gvMFFolio.MasterTableView.Items[dr.ItemIndex].GetDataKeyValue("Folio No").ToString();

                        PortFolioUpdate = adviserBranchBo.FolioMoveToPortfolio(customerId, folioNo, TofolioId, accountID);
                        if (PortFolioUpdate == true)
                        {
                            
                            statusMsg = "Portfolio Moved to Another Portfolio";
                            statusMsgSuccess.InnerText = statusMsg;
                            trFailure.Visible = true;
                            statusMsgSuccess.Visible = true;
                            statusMsgFailure.Visible = false;

                        }
                        else
                        {
                            statusMsgFailure.InnerText = statusMsg;
                            statusMsg = "Portfolio Has not been moved";
                            trFailure.Visible = true;
                            statusMsgSuccess.Visible = false;
                            statusMsgFailure.Visible = true;
                        }

                    }                   
                }
                this.BindFolioGridView();                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        protected void ddlAction_SelectedIndexChanged1(object sender, EventArgs e)
        {        
            if(trTransferMsg.Visible==true)
            {
                trTransferMsg.Visible = false;
            }

            if (ddlAction.SelectedValue == "MFtoAP")
            {
                customerVo = (CustomerVo)Session["customerVo"];
                tblMoveFolio.Visible = true;
                tblTransferFolio.Visible = false;
                bindDropdownPickPortfolio(int.Parse(customerVo.CustomerId.ToString()));
                
                txtCustomer.Text = string.Empty;
                ddlAdvisorBranchList.Items.Clear();
                txtPanParent.Text = string.Empty;
                txtAddress.Text = string.Empty;
               
                //tblTransferFolio.InnerText = string.Empty;
                //lblTransferMsg.Text = string.Empty;
                
            }
            else if(ddlAction.SelectedValue == "TF")
            {
                tblTransferFolio.Visible = true;
                tblMoveFolio.Visible = false;
                
                rmVo = (RMVo)Session[SessionContents.RmVo];
                txtCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();                
            }
            else if (ddlAction.SelectedValue == "0")
            {
                tblTransferFolio.Visible = false;
                tblMoveFolio.Visible = false;
                txtCustomer.Text = string.Empty;
                ddlAdvisorBranchList.Items.Clear();
                txtPanParent.Text = string.Empty;
                txtAddress.Text = string.Empty;
               //tblTransferFolio.Rows.RemoveAt=;
                //lblTransferMsg.Text = string.Empty;
                
            }
        }

        protected void gvMFFolio_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                RadComboBox ddl = item.FindControl("ddlAction") as RadComboBox;
                if (Session[SessionContents.CurrentUserRole].ToString() == "Customer")
                {
                    if (ddl != null)
                    {
                        //ddl.Items.FindByValue("Edit").
                        //ddl.Items.RemoveAt(2);
                        ddl.Items.Remove(ddl.Items.FindItemByValue("Edit"));
                        ddl.Items.Remove(ddl.Items.FindItemByValue("Delete"));

                    }
                }
            }
        }

        protected void gvMFFolio_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtProcessLogDetails = new DataTable();
             dtProcessLogDetails = (DataTable)Cache["FolioDetails" + customerVo.CustomerId.ToString()];
            gvMFFolio.DataSource = dtProcessLogDetails;            
        }
        //protected void gvMFFolio_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        DropDownList ddl = e.Row.FindControl("ddlAction") as DropDownList;
        //        if (Session[SessionContents.CurrentUserRole].ToString() == "Customer")
        //        {
        //            if (ddl != null)
        //            {
        //                //ddl.Items.FindByValue("Edit").
        //                //ddl.Items.RemoveAt(2);
        //                ddl.Items.Remove(ddl.Items.FindByValue("Edit"));
        //                ddl.Items.Remove(ddl.Items.FindByValue("Delete"));
                       
        //            }
        //        }
        //     }
        //}
    }
}
