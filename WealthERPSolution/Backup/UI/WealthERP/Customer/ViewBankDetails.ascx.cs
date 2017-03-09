using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCustomerProfiling;
using VoCustomerProfiling;
using VoUser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using System.Configuration;
using System.Data;
using WealthERP.Base;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;


namespace WealthERP.Customer
{
    public partial class ViewBankDetails : System.Web.UI.UserControl
    {
        DataSet dsCustomerBankAccountDetails = new DataSet();
        UserVo userVo = null;
        RMVo rmVo = null;
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo advisorVo = new AdvisorVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
        CustomerBankAccountVo customerBankAccountVo = new CustomerBankAccountVo();
        CustomerAccountsVo customeraccountVo = new CustomerAccountsVo();
        List<CustomerBankAccountVo> customerBankAccountList = null;
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        List<CustomerAccountsVo> TransactionList = new List<CustomerAccountsVo>();
        // CustomerVo customerVo = null;
        //int custBankAccId;
        // int customerId = 0;
        DataSet dsCustomerAssociates;
        DataTable dtCustomerAssociates;
        DataTable dtCustomerAssociatesRaw;
        DataRow drCustomerAssociates;
        int bankId;
        static int portfolioId;
        int custBankAccId;
        string path;
        DataRow dr;
        DataTable dt;
        DataSet dsBankDetails;
        string currentUserRole = string.Empty;
        string viewForm = string.Empty;

        //string customerId = session["customerId"].ToString();


        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            userVo = (UserVo)Session["userVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            RMVo customerRMVo = new RMVo();
            if (!IsPostBack)
            {

                //  ddlModeofOperation.Enabled = false;        
                if (currentUserRole == "admin")
                {
                    BindBankDetails(customerVo.CustomerId);
                }
                else
                {
                    BindBankDetails(customerVo.CustomerId);

                }

                if (Session["AddMFFolioLinkIdLinkAction"] != null)
                {
                    gvBankDetails.MasterTableView.IsItemInserted = true;
                    gvBankDetails.Rebind();
                }

                //BindBankDetails(customerVo.CustomerId);
            }
            gvCashSavingTransaction.Visible = false;
        }

        private void BindTransactionGrid()
        {
        //    DataTable dtBankAccId = new DataTable();
        //    dtBankAccId = (DataTable)Session["BankAccId"];      
        //    custBankAccId = Convert.ToInt32(ViewState["BankId"]);           
        //    TransactionList = customerAccountBo.GetCustomerBankTransaction(custBankAccId);
        //    DataTable dtTransaction = new DataTable();
        //    dtTransaction.Columns.Add("CCST_TransactionId");
        //    dtTransaction.Columns.Add("CCST_ExternalTransactionId");
        //    dtTransaction.Columns.Add("CCST_Transactiondate");
        //    dtTransaction.Columns.Add("CCST_Desc");
        //    dtTransaction.Columns.Add("CCST_ChequeNo");
        //    dtTransaction.Columns.Add("CCST_IsWithdrwal");// original costumer name from folio uploads
        //    dtTransaction.Columns.Add("CCST_Amount");
        //    // dtTransaction.Columns.Add("CCST_AvailableBalance");
        //    dtTransaction.Columns.Add("CB_HoldingAmount");

        //    DataRow drTransaction;
        //    for (int i = 0; i < TransactionList.Count; i++)
        //    {
        //        drTransaction = dtTransaction.NewRow();
        //        customeraccountVo = new CustomerAccountsVo();
        //        customeraccountVo = TransactionList[i];
        //        drTransaction["CCST_TransactionId"] = customeraccountVo.TransactionId.ToString();
        //        drTransaction["CCST_ExternalTransactionId"] = customeraccountVo.ExternalTransactionId.ToString();
        //        drTransaction["CCST_Transactiondate"] = customeraccountVo.Transactiondate.ToString();
        //        drTransaction["CCST_Desc"] = customeraccountVo.CCST_Desc.ToString().Trim();
        //        drTransaction["CCST_ChequeNo"] = customeraccountVo.ChequeNo.ToString().Trim();
        //        if (customeraccountVo.IsWithdrwal == 0)
        //        {
        //            drTransaction["CCST_IsWithdrwal"] = "CR";

        //        }
        //        else
        //        {
        //            drTransaction["CCST_IsWithdrwal"] = "DR";
        //        }
        //        //drTransaction["CCST_IsWithdrwal"] = customeraccountVo.IsWithdrwal.ToString();
        //        drTransaction["CCST_Amount"] = customeraccountVo.Amount.ToString();
        //        dtTransaction.Rows.Add(drTransaction);

        //    }
        //    if (TransactionList.Count > 0)
        //    {
        //        gvCashSavingTransaction.DataSource = dtTransaction;
        //        gvCashSavingTransaction.DataBind();
        //        //btnTransferFolio.Visible = false;
        //        //btnMoveFolio.Visible = false;
        //        gvCashSavingTransaction.Visible = true;
              
        //    }
        //    else
        //    {
        //        gvCashSavingTransaction.DataSource = dtTransaction;
        //        gvCashSavingTransaction.DataBind();
        //        gvCashSavingTransaction.Visible = true;

        //    }
        }
        public void BindBankDetails(int customerIdForGettingBankDetails)
        {
            try
            {
                //SessionBo.CheckSession();
                customerVo = (CustomerVo)Session["CustomerVo"];
                customerIdForGettingBankDetails = customerVo.CustomerId;
                dsCustomerBankAccountDetails = customerBankAccountBo.GetCustomerIndividualBankDetails(customerIdForGettingBankDetails);
                //DataTable dtCustomerBankAccounts = new DataTable();
                //dtCustomerBankAccounts.Columns.Add("CB_CustBankAccId");
                //dtCustomerBankAccounts.Columns.Add("WERPBM_BankCode");
                //dtCustomerBankAccounts.Columns.Add("CB_BranchName");
                //dtCustomerBankAccounts.Columns.Add("PAIC_AssetInstrumentCategoryCode");
                //dtCustomerBankAccounts.Columns.Add("XMOH_ModeOfHoldingCode");
                //dtCustomerBankAccounts.Columns.Add("CB_AccountNum");
                //dtCustomerBankAccounts.Columns.Add("CB_BranchAdrLine1");
                //dtCustomerBankAccounts.Columns.Add("CB_BranchAdrLine2");
                //dtCustomerBankAccounts.Columns.Add("CB_BranchAdrLine3");
                //dtCustomerBankAccounts.Columns.Add("CB_BranchAdrPinCode");
                //dtCustomerBankAccounts.Columns.Add("CB_BranchAdrCity");
                //dtCustomerBankAccounts.Columns.Add("CB_BranchAdrState");
                //dtCustomerBankAccounts.Columns.Add("CB_BranchAdrCountry");
                //dtCustomerBankAccounts.Columns.Add("CB_MICR");
                //dtCustomerBankAccounts.Columns.Add("CB_IFSC");
                //dtCustomerBankAccounts.Columns.Add("PAIC_AssetInstrumentCategoryName");
                //dtCustomerBankAccounts.Columns.Add("XMOH_ModeOfHolding");
                //dtCustomerBankAccounts.Columns.Add("WERPBMBankName");

                //DataRow drCustomerBankAccount;
                //for (int i = 0; i < customerBankAccountList.Count; i++)
                //{
                //    drCustomerBankAccount = dtCustomerBankAccounts.NewRow();
                //    customerBankAccountVo = new CustomerBankAccountVo();
                //    customerBankAccountVo = customerBankAccountList[i];
                //    drCustomerBankAccount[0] = customerBankAccountVo.CustBankAccId.ToString();
                //    if (!string.IsNullOrEmpty(customerBankAccountVo.BankName))
                //        drCustomerBankAccount[1] = customerBankAccountVo.BankName.ToString();
                //    drCustomerBankAccount[2] = customerBankAccountVo.BranchName.ToString();
                //    drCustomerBankAccount[3] = customerBankAccountVo.AccountTypeCode.ToString().Trim();
                //    drCustomerBankAccount[4] = customerBankAccountVo.ModeOfOperationCode.ToString().Trim();
                //    drCustomerBankAccount[5] = customerBankAccountVo.BankAccountNum.ToString();
                //    if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrLine1))
                //        drCustomerBankAccount[6] = customerBankAccountVo.BranchAdrLine1.ToString();
                //    if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrLine2))
                //        drCustomerBankAccount[7] = customerBankAccountVo.BranchAdrLine2.ToString();
                //    if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrLine3))
                //        drCustomerBankAccount[8] = customerBankAccountVo.BranchAdrLine3.ToString();
                //    if (customerBankAccountVo.BranchAdrPinCode != 0)
                //        drCustomerBankAccount["CB_BranchAdrPinCode"] = customerBankAccountVo.BranchAdrPinCode.ToString();
                //    if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrCity))
                //        drCustomerBankAccount[10] = customerBankAccountVo.BranchAdrCity.ToString();
                //    if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrState))
                //        drCustomerBankAccount[11] = customerBankAccountVo.BranchAdrState.ToString();
                //    if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrCountry))
                //        drCustomerBankAccount[12] = customerBankAccountVo.BranchAdrCountry.ToString();

                //    if (customerBankAccountVo.MICR != 0)
                //        drCustomerBankAccount["CB_MICR"] = customerBankAccountVo.MICR.ToString();
                //    if (!string.IsNullOrEmpty(customerBankAccountVo.IFSC))
                //        drCustomerBankAccount[14] = customerBankAccountVo.IFSC.ToString();
                //    drCustomerBankAccount[15] = customerBankAccountVo.AccountType.ToString();
                //    drCustomerBankAccount[16] = customerBankAccountVo.ModeOfOperation.ToString();
                //    if (!string.IsNullOrEmpty(customerBankAccountVo.WERPBMBankName))
                //        drCustomerBankAccount[17] = customerBankAccountVo.WERPBMBankName.ToString();
                //    dtCustomerBankAccounts.Rows.Add(drCustomerBankAccount);
                //}
                if (dsCustomerBankAccountDetails.Tables[0].Rows.Count > 0)
                {
                    if (Cache["gvDetailsForBank" + userVo.UserId + customerVo.CustomerId] == null)
                    {
                        Cache.Insert("gvDetailsForBank" + userVo.UserId + customerVo.CustomerId, dsCustomerBankAccountDetails.Tables[0]);
                    }
                    else
                    {
                        Cache.Remove("gvDetailsForBank" + userVo.UserId + customerVo.CustomerId);
                        Cache.Insert("gvDetailsForBank" + userVo.UserId + customerVo.CustomerId, dsCustomerBankAccountDetails.Tables[0]);
                    }

                    gvBankDetails.DataSource = dsCustomerBankAccountDetails.Tables[0];
                    gvBankDetails.DataBind();
                    gvBankDetails.Visible = true;
                    DivAction.Visible = true;
                    imgBtnrgHoldings.Visible = true;
                }
                else
                {
                    gvBankDetails.DataSource = dsCustomerBankAccountDetails.Tables[0];
                    gvBankDetails.DataBind();
                    gvBankDetails.Visible = true;
                    DivAction.Visible = false;
                    imgBtnrgHoldings.Visible = false;
                    // BindDDLBankDetails();
                    Session["BankAccId"] = dsCustomerBankAccountDetails.Tables[0];
                    //}
                    //else
                    //{
                    //    gvBankDetails.DataSource = null;
                    //    gvBankDetails.DataBind();
                }    //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewBankDetails.ascx:Page_Load()");
                object[] objects = new object[5];
                objects[0] = customerVo;
                objects[2] = customerBankAccountVo;
                objects[3] = customerBankAccountList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void BindDDLBankDetails()
        {
            ddlAccountDetails.DataSource = dsCustomerBankAccountDetails.Tables[1];
            ddlAccountDetails.DataTextField = "details";
            ddlAccountDetails.DataValueField = "CB_CustBankAccId";
            ddlAccountDetails.DataBind();
            ddlAccountDetails.Items.Insert(0, "Select");


        }

        public void ddlAccountDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbtnholding.Checked = false;
            rbtntransaction.Checked = false;

            if (ddlAccountDetails.SelectedIndex != 0)
                trHoldingAndTrnx.Visible = true;
            else
                trHoldingAndTrnx.Visible = false;

            if (ViewState["BankId"] != null)
                ViewState.Remove("BankId");
            ViewState["BankId"] = ddlAccountDetails.SelectedValue;
        }

        protected void gvBankDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                DataTable dtAccType = new DataTable();
                DataTable dtModeOfOpn = new DataTable();
                DataTable dtBankState = new DataTable();
                DataTable dtBankName = new DataTable();

                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlPortfolio = (DropDownList)gefi.FindControl("ddlPortfolioId");
                DropDownList ddlAccountType = (DropDownList)gefi.FindControl("ddlAccountType");
                dtAccType = customerBankAccountBo.AssetBankaccountType();
                ddlAccountType.DataSource = dtAccType;
                ddlAccountType.DataValueField = dtAccType.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                ddlAccountType.DataTextField = dtAccType.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                ddlAccountType.DataBind();
                ddlAccountType.Items.Insert(0, new ListItem("Select", "Select"));

                DropDownList ddlModeofOperation = (DropDownList)gefi.FindControl("ddlModeofOperation");
                RadioButton rbtnY = (RadioButton)gefi.FindControl("rbtnYes");
                RadioButton rbtnN = (RadioButton)gefi.FindControl("rbtnNo");
                dtModeOfOpn = customerBankAccountBo.XMLModeOfHolding();
                ddlModeofOperation.DataSource = dtModeOfOpn;
                ddlModeofOperation.DataValueField = dtModeOfOpn.Columns["XMOH_ModeOfHoldingCode"].ToString();
                ddlModeofOperation.DataTextField = dtModeOfOpn.Columns["XMOH_ModeOfHolding"].ToString();
                ddlModeofOperation.DataBind();
                ddlModeofOperation.SelectedValue = "SI";

                //   ddlModeofOperation.Items.Insert(0, new ListItem("Select", "Select"));


                DropDownList ddlBankName = (DropDownList)gefi.FindControl("ddlBankName");
                dtBankName = customerBankAccountBo.GetALLBankName();
                ddlBankName.DataSource = dtBankName;
                ddlBankName.DataValueField = dtBankName.Columns["WCMV_BankName"].ToString();
                ddlBankName.DataTextField = dtBankName.Columns["WCMV_LookupId_BankId"].ToString();
                ddlBankName.DataBind();
                ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));

                DropDownList ddlBankAdrState = (DropDownList)gefi.FindControl("ddlBankAdrState");
                dtBankState = XMLBo.GetStates(path);
                ddlBankAdrState.DataSource = dtBankState;
                ddlBankAdrState.DataTextField = "StateName";
                ddlBankAdrState.DataValueField = "StateCode";
                ddlBankAdrState.DataBind();
                ddlBankAdrState.Items.Insert(0, new ListItem("Select", "Select"));


                DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
                ddlPortfolio.DataSource = ds;
                ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
                ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
                ddlPortfolio.DataBind();

                HtmlTableRow trheadingCaption = (HtmlTableRow)gefi.FindControl("trheadingCaption");
                trheadingCaption.Visible = true;
             
                #region fornomineebinding
                BindNominees();
                RadGrid gv = (RadGrid)item.FindControl("gvNominees");
                Label lblNoNominee = (Label)item.FindControl("lblNoNominee");
                // TableRow trNoNominee = (TableRow)item.FindControl("trNoNominee");  
                if (dtCustomerAssociates.Rows.Count > 0)
                {
                    gv.DataSource = dtCustomerAssociates;
                    gv.DataBind();
                    gv.Visible = true;
                    lblNoNominee.Visible = false;
                }
                else
                {
                    gv.Visible = false;
                    lblNoNominee.Visible = true;
                }

                #endregion

                #region bindjoint

                RadGrid gvrlist = (RadGrid)gefi.FindControl("gvJointHolders");
                RadioButton rtbu = (RadioButton)e.Item.FindControl("rbtnYes");
                RadioButton rbtno = (RadioButton)e.Item.FindControl("rbtnNo");
                if (rtbu.Checked==true)
                {
                    gvrlist.Visible = true;
                }
               if(rbtno.Checked==true)
                {
                    gvrlist.Visible = false;
                }
                #endregion
            }
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editForm = (GridEditFormItem)e.Item;
                Button updatebutton = (Button)editForm.FindControl("Button1");
                if (currentUserRole == "customer")
                {
                    updatebutton.Enabled = false;
                    imgBtnrgHoldings.Visible = false;
                    // updatebutton.Attributes["onclick"] = "return alert('You cannot Update this Bank Detail')";
                }
            }
            if (e.Item is GridCommandItem)
            {
                Button Addnew = e.Item.FindControl("AddNewRecordButton") as Button;
                LinkButton AddBank = e.Item.FindControl("InitInsertButton") as LinkButton;
                if (currentUserRole == "customer")
                {
                    Addnew.Visible = false;
                    AddBank.Visible = false;

                    //AddBank.Attributes["onclick"] = "return alert('You cannot Add New Bank Detail')";
                }

            }
            string strBankAdrState;
            string strModeOfOperation;
            string strAccountType;
            string strBankName;
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());
                strBankAdrState = gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_BranchAdrState"].ToString();
                strModeOfOperation = gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XMOH_ModeOfHoldingCode"].ToString();
                strAccountType = gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PAIC_AssetInstrumentCategoryCode"].ToString();
                strBankName = gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WCMV_LookupId_BankId"].ToString();

                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DataTable dtAccType = new DataTable();
                DataTable dtModeOfOpn = new DataTable();
                DataTable dtBankState = new DataTable();
                DataTable dtBankName = new DataTable();

                DropDownList ddlAccountType = (DropDownList)editedItem.FindControl("ddlAccountType");
                dtAccType = customerBankAccountBo.AssetBankaccountType();
                ddlAccountType.DataSource = dtAccType;
                ddlAccountType.DataValueField = dtAccType.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                ddlAccountType.DataTextField = dtAccType.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                ddlAccountType.DataBind();
                ddlAccountType.SelectedValue = strAccountType;

                DropDownList ddlModeofOperation = (DropDownList)editedItem.FindControl("ddlModeofOperation");
                dtModeOfOpn = customerBankAccountBo.XMLModeOfHolding();
                ddlModeofOperation.DataSource = dtModeOfOpn;
                ddlModeofOperation.DataValueField = dtModeOfOpn.Columns["XMOH_ModeOfHoldingCode"].ToString();
                ddlModeofOperation.DataTextField = dtModeOfOpn.Columns["XMOH_ModeOfHolding"].ToString();
                ddlModeofOperation.DataBind();
                ddlModeofOperation.SelectedValue = strModeOfOperation;

                DropDownList ddlBankName = (DropDownList)editedItem.FindControl("ddlBankName");
                dtBankName = customerBankAccountBo.GetALLBankName();
                ddlBankName.DataSource = dtBankName;
                ddlBankName.DataValueField = dtBankName.Columns["WCMV_LookupId_BankId"].ToString();
                ddlBankName.DataTextField = dtBankName.Columns["WCMV_BankName"].ToString();
                ddlBankName.DataBind();
                ddlBankName.SelectedValue = strBankName;

                DropDownList ddlBankAdrState = (DropDownList)editedItem.FindControl("ddlBankAdrState");
                dtBankState = XMLBo.GetStates(path);
                ddlBankAdrState.DataSource = dtBankState;
                ddlBankAdrState.DataTextField = "StateName";
                ddlBankAdrState.DataValueField = "StateCode";
                ddlBankAdrState.DataBind();
                ddlBankAdrState.SelectedValue = strBankAdrState;

                DropDownList ddlPortfolio = (DropDownList)editedItem.FindControl("ddlPortfolioId");
                DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
                ddlPortfolio.DataSource = ds;
                ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
                ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
                ddlPortfolio.DataBind();


                HtmlTableRow trheadingCaption = (HtmlTableRow)editedItem.FindControl("trheadingCaption");
                trheadingCaption.Visible = false;

                #region fornomineebinding
                BindNominees();
                RadGrid gv = (RadGrid)e.Item.FindControl("gvNominees");
                Label lblNoNominee = (Label)e.Item.FindControl("lblNoNominee");
                // TableRow trNoNominee = (TableRow)item.FindControl("trNoNominee");  
                if (dtCustomerAssociates.Rows.Count > 0)
                {
                    gv.DataSource = dtCustomerAssociates;
                    gv.DataBind();
                    gv.Visible = false;
                    lblNoNominee.Visible = false;
                }
                else
                {
                    lblNoNominee.Visible = true;
                }

                #endregion
                #region bindjoint
                RadGrid gvrlist = (RadGrid)e.Item.FindControl("gvJointHolders");
                RadioButton rtbu = (RadioButton)e.Item.FindControl("rbtnYes");
                RadioButton rbtno = (RadioButton)e.Item.FindControl("rbtnNo");
                // dtCustomerAssociates = (DataTable)Cache["gvjoinholder" + customerVo.CustomerId];
                if (rtbu.Checked==true)
                {
                    // gvrlist.DataSource = dtCustomerAssociates;
                    gvrlist.Visible = false;
                    //  gvrlist.DataBind();
                }
                if (rbtno.Checked == true)               
                {
                    gvrlist.Visible = false;
                }
                #endregion

            }

            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                LinkButton buttonDelete = dataItem["deleteColumn"].Controls[0] as LinkButton;
                if (currentUserRole == "customer")
                {
                    buttonDelete.Enabled = false;
                    buttonDelete.Attributes["onclick"] = "return alert('You cannot delete this Bank Detail')";
                }
                else
                {
                    buttonDelete.Enabled = true;
                }
            }
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                LinkButton button = dataItem["ButtonColumn1"].Controls[0] as LinkButton;
                LinkButton buttonBalance = dataItem["ButtonColumn"].Controls[0] as LinkButton;
                TableCell cell = (TableCell)dataItem["CB_IsFromTransaction"];
                string flag=cell.Text;
                if (flag == "&nbsp;")
                    flag = null;
               if (flag=="0")
               {

                   dataItem["ButtonColumn1"].Enabled = false;
                   button.Enabled = false;
                   button.Attributes["onclick"] = "return alert('Already have Holding Amount')";                

               }
               else if (flag == "1")
               {
                   dataItem["ButtonColumn"].Enabled = false;
                   buttonBalance.Enabled = false;
                   buttonBalance.Attributes["onclick"] = "return alert('Already have Transaction')";
               }
               else
               {
                   dataItem["ButtonColumn1"].Enabled = true;
                   button.Enabled = true;
                   dataItem["ButtonColumn"].Enabled = true;
                   buttonBalance.Enabled = true;
               }

            }


        }
        protected void gvBankDetails_ItemCommand(object source, GridCommandEventArgs e)
        {
            int i = 0;
            int customerId = 0;
            int accountId = 0;
            string strExternalCode = string.Empty;
            string strExternalType = string.Empty;
            DateTime createdDate = new DateTime();
            DateTime editedDate = new DateTime();
            DateTime deletedDate = new DateTime();
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;

                DropDownList ddlAccountType = (DropDownList)e.Item.FindControl("ddlAccountType");
                TextBox txtAccountNumber = (TextBox)e.Item.FindControl("txtAccountNumber");
                DropDownList ddlModeofOperation = (DropDownList)e.Item.FindControl("ddlModeOfOperation");
                DropDownList ddlBankName = (DropDownList)e.Item.FindControl("ddlBankName");
                TextBox txtBranchName = (TextBox)e.Item.FindControl("txtBranchName");
                TextBox txtBankAdrLine1 = (TextBox)e.Item.FindControl("txtBankAdrLine1");
                TextBox txtBankAdrLine2 = (TextBox)e.Item.FindControl("txtBankAdrLine2");
                TextBox txtBankAdrLine3 = (TextBox)e.Item.FindControl("txtBankAdrLine3");
                TextBox txtBankAdrCity = (TextBox)e.Item.FindControl("txtBankAdrCity");
                TextBox txtBankAdrPinCode = (TextBox)e.Item.FindControl("txtBankAdrPinCode");
                TextBox txtMicr = (TextBox)e.Item.FindControl("txtMicr");
                DropDownList ddlBankAdrState = (DropDownList)e.Item.FindControl("ddlBankAdrState");
                TextBox txtIfsc = (TextBox)e.Item.FindControl("txtIfsc");
                RadioButton rtb = (RadioButton)e.Item.FindControl("rbtnYes");
                RadioButton rtbn = (RadioButton)e.Item.FindControl("rbtnNo");
                //trNomineeCaption.Visible = false;
                bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());
                customerVo = (CustomerVo)Session["customerVo"];
                customerId = customerVo.CustomerId;
                customerBankAccountVo.BankAccountNum = txtAccountNumber.Text.ToString();
                customerBankAccountVo.AccountType = ddlAccountType.SelectedItem.Value.ToString();
                if (rtbn.Checked)
                {
                    customerBankAccountVo.IsJointHolding = 0;
                }
                if (rtb.Checked)
                {
                    customerBankAccountVo.IsJointHolding = 1;
                }
                customerBankAccountVo.ModeOfOperation = ddlModeofOperation.SelectedItem.Value.ToString();
                customerBankAccountVo.BankName = ddlBankName.SelectedItem.Value.ToString();
                customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
                customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
                customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
                customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
                if (txtBankAdrPinCode.Text.ToString() != "")
                    customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
                else
                    customerBankAccountVo.BranchAdrPinCode = 0;
                customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
                if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
                    customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();

                customerBankAccountVo.CustBankAccId = bankId;
                customerBankAccountVo.BranchAdrCountry = "India";
                customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
                customerBankAccountVo.IFSC = txtMicr.Text.ToString();
                //if (txtMicr.Text.ToString() != "")
                //    customerBankAccountVo.MICR = int.Parse(txtMicr.Text.ToString());
                //else
                //    customerBankAccountVo.MICR = 0;
                customerBankAccountBo.UpdateCustomerBankAccount(customerBankAccountVo, customerId);

            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                CustomerBo customerBo = new CustomerBo();
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                DropDownList ddlPortfolio = (DropDownList)e.Item.FindControl("ddlPortfolioId");
                DropDownList ddlAccountType = (DropDownList)e.Item.FindControl("ddlAccountType");
                TextBox txtAccountNumber = (TextBox)e.Item.FindControl("txtAccountNumber");
                DropDownList ddlModeofOperation = (DropDownList)e.Item.FindControl("ddlModeOfOperation");
                DropDownList ddlBankName = (DropDownList)e.Item.FindControl("ddlBankName");
                TextBox txtBranchName = (TextBox)e.Item.FindControl("txtBranchName");
                TextBox txtBankAdrLine1 = (TextBox)e.Item.FindControl("txtBankAdrLine1");
                TextBox txtBankAdrLine2 = (TextBox)e.Item.FindControl("txtBankAdrLine2");
                TextBox txtBankAdrLine3 = (TextBox)e.Item.FindControl("txtBankAdrLine3");
                TextBox txtBankAdrCity = (TextBox)e.Item.FindControl("txtBankAdrCity");
                TextBox txtBankAdrPinCode = (TextBox)e.Item.FindControl("txtBankAdrPinCode");
                TextBox txtMicr = (TextBox)e.Item.FindControl("txtMicr");
                DropDownList ddlBankAdrState = (DropDownList)e.Item.FindControl("ddlBankAdrState");
                TextBox txtIfsc = (TextBox)e.Item.FindControl("txtIfsc");
                RadioButton rtb = (RadioButton)e.Item.FindControl("rbtnYes");
                RadioButton rtbn = (RadioButton)e.Item.FindControl("rbtnNo");
                RadGrid gvjointholder = (RadGrid)gridEditableItem.FindControl("gvJointHolders");
                Label lblError = (Label)e.Item.FindControl("lblError");
                TableRow trError = (TableRow)e.Item.FindControl("trError");


                RMVo rmVo = new RMVo();
                int userId;
                rmVo = (RMVo)Session["RmVo"];
                userId = rmVo.UserId;
                string chk;

                if (Session["Check"] != null)
                {
                    chk = Session["Check"].ToString();
                }
                customerVo = (CustomerVo)Session["customerVo"];
                customerId = customerVo.CustomerId;
                customerBankAccountVo = new CustomerBankAccountVo();
                customerBankAccountVo.PortfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString()); ;
                customerBankAccountVo.AccountType = ddlAccountType.SelectedValue.ToString();
                customerBankAccountVo.BankAccountNum = txtAccountNumber.Text.ToString();
                if (ddlModeofOperation.SelectedValue.ToString() != "Select a Mode of Holding")
                    customerBankAccountVo.ModeOfOperation = ddlModeofOperation.SelectedValue.ToString();
                customerBankAccountVo.BankName = ddlBankName.SelectedValue.ToString();
                customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
                customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
                customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
                customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
                if (txtBankAdrPinCode.Text.ToString() != "")
                    customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
                customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
                if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
                    customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();
                customerBankAccountVo.BranchAdrCountry = "India";
                if (txtMicr.Text.ToString() != "")
                    customerBankAccountVo.MICR = txtMicr.Text.ToString();
                customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
                customerBankAccountVo.Balance = 0;
                //customerBankAccountBo.CreateCustomerBankAccount(customerBankAccountVo, customerId, userId);
                if (rtbn.Checked)
                {
                    customerBankAccountVo.IsJointHolding = 0;
                }
                if (rtb.Checked)
                {
                    customerBankAccountVo.IsJointHolding = 1;
                }
                accountId = customerBankAccountBo.CreateCustomerBankAccount(customerBankAccountVo, customerId, userId);
                customerAccountAssociationVo.AccountId = accountId;

                if (gvjointholder.Items.Count > 0)
                {
                    foreach (GridDataItem item in gvjointholder.Items)
                    {
                        CheckBox chkId = (CheckBox)item.FindControl("chkId");
                        if (chkId.Checked)
                        {
                            i++;
                            customerAccountAssociationVo.AssociationId = int.Parse(gvjointholder.MasterTableView.DataKeyValues[item.ItemIndex]["AssociationId"].ToString());
                            customerAccountAssociationVo.AssociationType = "Joint Holder";
                            customerAccountBo.CreatecustomerBankAccountAssociation(customerAccountAssociationVo, userVo.UserId);
                        }
                    }
                }
                else
                {
                    i = -1;
                }
                if (rtb.Checked)
                {
                    if (i == 0)
                    {
                        //trError.Visible = true;
                        lblError.Visible = true;
                        lblError.Text = "Please select a Joint Holder";
                        // blResult = false;
                    }
                    else
                    {
                        // trError.Visible = false;
                        lblError.Visible = false;

                    }
                }
                txtAccountNumber.Text = "";
                txtBankAdrLine1.Text = "";
                txtBankAdrLine2.Text = "";
                txtBankAdrLine3.Text = "";
                txtBankAdrPinCode.Text = "";
                txtBankAdrCity.Text = "";
                ddlBankName.SelectedIndex = 0;
                txtBranchName.Text = "";
                txtIfsc.Text = "";
                txtMicr.Text = "";
                ddlAccountType.SelectedIndex = 0;
                ddlModeofOperation.SelectedIndex = 0;
               
            }

            //bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());

            if (e.CommandName == "Delete")
            {
                bool isdeleted = false;
                bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());
                isdeleted = customerBankAccountBo.DeleteCustomerBankAccount(bankId);
                if (isdeleted == false)
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Cannot delete the bank is associate Folio/Trade Account');", true);
            }
            if (e.CommandName == "viewTransaction")
            {
                bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());            
                string name = "viewTransaction";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalSetUPPage", "loadcontrol('AddBankDetails','?name=" + name + "&bankId=" + bankId + "');", true);             
            }
            if (e.CommandName == "Editbalance")
            {
                bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());             
                string accountNum = (gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_AccountNum"].ToString());
                double amount = double.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_HoldingAmount"].ToString());
                string bankname = (gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WCMV_BankName"].ToString());
                string name = "Editbalance";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddBankDetails", "loadcontrol('AddBankDetails','?name=" + name + "&accountNum=" + accountNum + "&amount=" + amount + "&bankname=" + bankname + "&bankId=" + bankId + "');", true);

            }
            if (e.CommandName == "Edit")
            {
                bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CB_CustBankAccId"].ToString());
                string  strModeOfOperation = gvBankDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XMOH_ModeOfHoldingCode"].ToString();
                string name = "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "AddBankAccount", "loadcontrol('AddBankAccount','?action=" + "View" + "&bankId=" + bankId + "&strModeOfOperation=" + strModeOfOperation + "');", true);
            }
            BindBankDetails(customerId);
        }
        protected void gvBankDetails_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGvBankDetails = new DataTable();
            if (Cache["gvDetailsForBank" + userVo.UserId + customerVo.CustomerId] != null)
            {
                dtGvBankDetails = (DataTable)Cache["gvDetailsForBank" + userVo.UserId + customerVo.CustomerId];
                gvBankDetails.DataSource = dtGvBankDetails;
            }
        }
        //protected void gvCustomerBankAccounts_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //   // Session["CustBankAccId"] = gvBankDetails.SelectedDataKey.Value.ToString();
        //   // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCustomerAllBankDetails','none');", true);


        // }
        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlPortfolio = (DropDownList)sender;
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (GridDataItem gvr in gvBankDetails.Items)
            {
                if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                {
                    i = i + 1;
                }
                
            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select record to delete!');", true);
            }

            else
            {
                CustomerBankDelete();
            }
        }
        private void CustomerBankDelete()
        {
            bool isdeleted = false;
            customerVo = (CustomerVo)Session["customerVo"];
            int customerId = customerVo.CustomerId;
            foreach (GridDataItem gvr in this.gvBankDetails.Items)
            {
                if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                {
                    int bankId = int.Parse(gvBankDetails.MasterTableView.DataKeyValues[gvr.ItemIndex]["CB_CustBankAccId"].ToString());
                    isdeleted= customerBankAccountBo.DeleteCustomerBankAccount(bankId);
                    if (isdeleted == false)
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Cannot delete the bank is associate with Folio/Trade Account');", true);
                }
            }
            BindBankDetails(customerId); ;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int accountId = Convert.ToInt32(ViewState["BankId"]);

            int customerId = 0;
            customerVo = (CustomerVo)Session["customerVo"];
            customerId = customerVo.CustomerId;
            customeraccountVo = new CustomerAccountsVo();
            customeraccountVo.Amount = double.Parse(txtholdingAmt.Text.ToString());
            // customerAccountBo.InsertholdingAmountCustomerBank(customeraccountVo);

        }
        protected void Holding_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnholding.Checked == true)
            {

                trCSTransactionCaption.Visible = true;
                DivTransaction.Visible = true;
                gvCashSavingTransaction.Visible = true;
                BindTransactionGrid();
                trholdingamount.Visible = true;
                DivTransaction.Visible = false;
            }
            if (rbtntransaction.Checked == true)
            {
                DivTransaction.Visible = true;
                BindTransactionGrid();
                gvCashSavingTransaction.Visible = true;
                trholdingamount.Visible = false;
            }
        }
        protected void rbtnYes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton rbtnYes = (RadioButton)sender;
                GridEditFormItem editItem = (GridEditFormItem)rbtnYes.NamingContainer;
                DropDownList ddlModeofOperation = (DropDownList)editItem.FindControl("ddlModeofOperation");
                RadGrid gvJointHolder = (RadGrid)editItem.FindControl("gvJointHolders");
                RadioButton rbtnNo = (RadioButton)editItem.FindControl("rbtnNo");
                if (rbtnYes.Checked == true)
                {

                    ddlModeofOperation.Enabled = true;
                    dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                    //dtCustomerAssociates.Rows.Clear();
                    dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];
                    dtCustomerAssociates = new DataTable();
                    dtCustomerAssociates.Columns.Clear();
                    dtCustomerAssociates.Columns.Add("MemberCustomerId");
                    dtCustomerAssociates.Columns.Add("AssociationId");
                    dtCustomerAssociates.Columns.Add("Name");
                    dtCustomerAssociates.Columns.Add("Relationship");
                    foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
                    {
                        drCustomerAssociates = dtCustomerAssociates.NewRow();
                        drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                        drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                        drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                        drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                        dtCustomerAssociates.Rows.Add(drCustomerAssociates);
                    }
                    if (dtCustomerAssociates.Rows.Count > 0)
                    {
                        if (Cache["gvjoinholder" + customerVo.CustomerId] == null)
                        {
                            Cache.Insert("gvjoinholder" + customerVo.CustomerId, dtCustomerAssociates);
                        }
                        else
                        {
                            Cache.Remove("gvjoinholder" + customerVo.CustomerId);
                            Cache.Insert("gvjoinholder" + customerVo.CustomerId, dtCustomerAssociates);
                        }
                        gvJointHolder.DataSource = dtCustomerAssociates;
                        gvJointHolder.DataBind();                       
                    }
                    else
                    {
                        gvJointHolder.Visible = false;                       
                    }
                    ddlModeofOperation.SelectedIndex = 0;
                }
                if (rbtnNo.Checked == true)
                {
                    ddlModeofOperation.SelectedValue = "SI";
                    ddlModeofOperation.Enabled = false;
                    gvJointHolder.Visible = false;
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
                FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:rbtnYes_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }


        /// <summary>
        /// Bind Nominee
        /// </summary>
        public void BindNominees()
        {
            try
            {
                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];
                dtCustomerAssociates = new DataTable();
                dtCustomerAssociates.Columns.Add("MemberCustomerId");
                dtCustomerAssociates.Columns.Add("AssociationId");
                dtCustomerAssociates.Columns.Add("Name");
                dtCustomerAssociates.Columns.Add("Relationship");

                foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
                {

                    drCustomerAssociates = dtCustomerAssociates.NewRow();
                    drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                    drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                    drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                    drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                    dtCustomerAssociates.Rows.Add(drCustomerAssociates);
                }

                //if (dtCustomerAssociates.Rows.Count > 0)
                //{
                //    gvNominees.DataSource = dtCustomerAssociates;
                //    gvNominees.DataBind();
                //    gvNominees.Visible = true;

                //    trNoNominee.Visible = false;
                //    //trNomineeCaption.Visible = true;
                //    trNominees.Visible = true;
                //}
                //else
                //{
                //    trNoNominee.Visible = true;
                //    //trNomineeCaption.Visible = false;
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
                FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:LoadNominees()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvBankDetails_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gvBankDetails.MasterTableView.IsItemInserted = false;
                gvBankDetails.Rebind();
                //imgBtnrgHoldings.Visible = true;
            }
        }
        protected void gvCashSavingTransaction_ItemCommand(object source, GridCommandEventArgs e)
        {
            int i = 0;
            int customerId = 0;
            int accountId = 0;
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;

                RadDatePicker dpTransactionDate = (RadDatePicker)e.Item.FindControl("dpTransactionDate");
                TextBox txtDescripton = (TextBox)e.Item.FindControl("txtDescripton");
                TextBox txtChequeNo = (TextBox)e.Item.FindControl("txtChequeNo");
                TextBox txtAmount = (TextBox)e.Item.FindControl("txtAmount");
                TextBox txtExternalTransactionId = (TextBox)e.Item.FindControl("txtExternalTransactionId");
                customeraccountVo = new CustomerAccountsVo();
                // bankId  =Convert.ToInt32(ViewState["BankId"]);
                accountId = int.Parse(gvCashSavingTransaction.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CCST_TransactionId"].ToString());
                customerAccountBo.UpdateCustomerBankTransaction(customeraccountVo, accountId);

            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                string strBankAccountID = ViewState["BankId"].ToString();
                RadDatePicker dpTransactionDate = (RadDatePicker)e.Item.FindControl("dpTransactionDate");
                TextBox txtDescripton = (TextBox)e.Item.FindControl("txtDescripton");
                TextBox txtChequeNo = (TextBox)e.Item.FindControl("txtChequeNo");
                TextBox txtAmount = (TextBox)e.Item.FindControl("txtAmount");
                TextBox txtExternalTransactionId = (TextBox)e.Item.FindControl("txtExternalTransactionId");
                DateTime date = Convert.ToDateTime(dpTransactionDate.SelectedDate);
                RadioButton rbtnY = (RadioButton)e.Item.FindControl("rbtnYes");
                RadioButton rbtnN = (RadioButton)e.Item.FindControl("rbtnNo");
                RMVo rmVo = new RMVo();
                int userId;
                rmVo = (RMVo)Session["RmVo"];
                userId = rmVo.UserId;
                customerId = customerVo.CustomerId;
                customeraccountVo = new CustomerAccountsVo();
                customeraccountVo.ExternalTransactionId = txtExternalTransactionId.Text.ToString();
                customeraccountVo.Transactiondate = date;
                customeraccountVo.CCST_Desc = txtDescripton.Text.ToString();
                customeraccountVo.ChequeNo = txtChequeNo.Text.ToString();
                customeraccountVo.Amount = double.Parse(txtAmount.Text.ToString());
                customeraccountVo.AccountId = Convert.ToInt32(strBankAccountID);
                if (rbtnN.Checked)
                {
                    customeraccountVo.IsWithdrwal = 0;
                }
                if (rbtnY.Checked)
                {
                    customeraccountVo.IsWithdrwal = 1;
                }

                customerAccountBo.CreatecustomerBankTransaction(customeraccountVo, userId);


                //isInserted = customerBo.InsertProductAMCSchemeMappingDetalis(customerId, strExternalCode, strExternalType, createdDate, editedDate, deletedDate);
            }

            if (e.CommandName == "Delete")
            {
                bool isdeleted = false;
                accountId = int.Parse(gvCashSavingTransaction.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CCST_TransactionId"].ToString());
                isdeleted = customerAccountBo.DeleteCustomerBankTransaction(accountId);
                if (isdeleted == false)
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Cannot delete the bank is associate with a folio/trade account');", true);
            }
            BindTransactionGrid();
        }


        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvBankDetails.ExportSettings.OpenInNewWindow = true;
            gvBankDetails.ExportSettings.IgnorePaging = true;
            gvBankDetails.ExportSettings.HideStructureColumns = true;
            gvBankDetails.ExportSettings.ExportOnlyData = true;
            gvBankDetails.ExportSettings.FileName = "BankDetails";
            gvBankDetails.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvBankDetails.MasterTableView.ExportToExcel();
        }
    }
}


