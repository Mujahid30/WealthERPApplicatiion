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

namespace WealthERP.CustomerPortfolio
{
    public partial class CustomerMFFolioView : System.Web.UI.UserControl
    {
        static int portfolioId;
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        List<CustomerAccountsVo> FolioList = new List<CustomerAccountsVo>();
        CustomerTransactionBo CustomerTransactionBo = new CustomerTransactionBo();
        CustomerAccountsVo FolioVo = new CustomerAccountsVo();
        int FolioId = 0;
        protected override void OnInit(EventArgs e)
        {
            ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
            mypager.EnableViewState = true;
            base.OnInit(e);
        }

        public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        {
            GetPageCount();
          
            this.BindFolioGridView();
        }

        private void GetPageCount()
        {
            string upperlimit;
            string lowerlimit;
            int rowCount = 0;
            if (hdnRecordCount.Value != "")
                rowCount = Convert.ToInt32(hdnRecordCount.Value);
            if (rowCount > 0)
            {

                int ratio = rowCount / 30;
                mypager.PageCount = rowCount % 30 == 0 ? ratio : ratio + 1;
                mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
                lowerlimit = (((mypager.CurrentPage - 1) * 30) + 1).ToString();
                upperlimit = (mypager.CurrentPage * 30).ToString();
                if (mypager.CurrentPage == mypager.PageCount)
                    upperlimit = hdnRecordCount.Value;
                string PageRecords = string.Format("{0}- {1} of ", lowerlimit, upperlimit);
                lblCurrentPage.Text = PageRecords;

                hdnCurrentPage.Value = mypager.CurrentPage.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            
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
            int Count;
            try
            {
                customerVo = (CustomerVo)Session["CustomerVo"];

                FolioList = CustomerTransactionBo.GetCustomerMFFolios(portfolioId, customerVo.CustomerId, mypager.CurrentPage, out Count);

                // lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                if (FolioList == null)
                {
                    lblMessage.Visible = true;
                    lblCurrentPage.Visible = false;
                    lblTotalRows.Visible = false;
                    DivPager.Visible = false;
                    gvMFFolio.DataSource = null;
                    gvMFFolio.DataBind();
                    btnTransferFolio.Visible = false;
                }
                else
                {
                    lblMessage.Visible = false;
                    lblTotalRows.Visible = true;
                    lblCurrentPage.Visible = true;
                    DivPager.Visible = true;
                    DataTable dtMFFolio = new DataTable();

                    dtMFFolio.Columns.Add("FolioId");
                    dtMFFolio.Columns.Add("Folio No");
                    dtMFFolio.Columns.Add("AMC Name");
                    dtMFFolio.Columns.Add("Mode Of Holding");
                    dtMFFolio.Columns.Add("A/C Opening Date");


                    DataRow drMFFolio;

                    for (int i = 0; i < FolioList.Count; i++)
                    {
                        drMFFolio = dtMFFolio.NewRow();
                        FolioVo = new CustomerAccountsVo();
                        FolioVo = FolioList[i];
                        drMFFolio[0] = FolioVo.AccountId.ToString();
                        drMFFolio[1] = FolioVo.AccountNum.ToString();
                        drMFFolio[2] = FolioVo.AMCName.ToString();
                        drMFFolio[3] = FolioVo.ModeOfHolding.ToString();
                        if (FolioVo.AccountOpeningDate != DateTime.MinValue)
                            drMFFolio[4] = FolioVo.AccountOpeningDate.ToShortDateString();
                        else
                            drMFFolio[4] = string.Empty;
                        dtMFFolio.Rows.Add(drMFFolio);
                    }
                    gvMFFolio.DataSource = dtMFFolio;
                    gvMFFolio.DataBind();

                }



                lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();
                if (Count > 0)
                    DivPager.Style.Add("display", "visible");
                else
                {
                    DivPager.Style.Add("display", "none");
                    lblCurrentPage.Text = "";
                    lblCurrentPage.Text = "";
                }

                this.GetPageCount();
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
                GridViewRow gvr = (GridViewRow)ddlAction.NamingContainer;
                int selectedRow = gvr.RowIndex;
                FolioId = int.Parse(gvMFFolio.DataKeys[selectedRow].Value.ToString());
                Session["FolioVo"] = CustomerTransactionBo.GetCustomerMFFolioDetails(FolioId);
                if (ddlAction.SelectedValue.ToString() == "Edit")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerMFAccountAdd','?action=Edit');", true);
                }
                if (ddlAction.SelectedValue.ToString() == "View")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerMFAccountAdd','?action=View');", true);
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

        protected void btnTransferFolio_Click(object sender, EventArgs e)
        {
            tblTransferFolio.Visible = true;
            btnTransferFolio.Visible = false;
            rmVo = (RMVo)Session[SessionContents.RmVo];
            txtCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();

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
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            PortfolioBo portfolioBo = new PortfolioBo();

            String statusMsg = string.Empty;
            int totalFoliosMoved = 0;
            CustomerPortfolioVo portfolioVo = new CustomerPortfolioVo();
            if (txtCustomerId.Value != string.Empty)
            {
                portfolioVo = portfolioBo.GetCustomerDefaultPortfolio(Convert.ToInt32(txtCustomerId.Value));
                if (portfolioVo.PortfolioId < 1)
                {
                    lblMessage.Text = "No default Portfolio found for the selected customer.";
                    lblMessage.Visible = true;
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Please select a customer.";
                lblMessage.Visible = true;
                return;
            }


            foreach (GridViewRow dr in gvMFFolio.Rows)
            {

                CheckBox checkBox = (CheckBox)dr.FindControl("chkBox");
                if (checkBox.Checked)
                {

                    int MFAccountId = Convert.ToInt32(gvMFFolio.DataKeys[dr.RowIndex].Values[0]);
                    bool isUpdated = PortfolioBo.TransferFolio(MFAccountId, portfolioVo.PortfolioId);
                    if (isUpdated)
                        totalFoliosMoved++;
                    else
                        statusMsg += "<br/>Error occurred while moving folio with mutual fund accountId " + MFAccountId + ".<br/>";
                }
            }
            if (totalFoliosMoved > 0)
                statusMsg += "<b style='color:Green'>Total Folios moved = " + totalFoliosMoved +"</b>";
            //lblMessage.Text = statusMsg;
            //lblMessage.Visible = true;
            lblTransferMsg.Text = statusMsg;
            divMessage.Attributes.Add("class", "yellow-box");
            BindFolioGridView();
        }
    }
}