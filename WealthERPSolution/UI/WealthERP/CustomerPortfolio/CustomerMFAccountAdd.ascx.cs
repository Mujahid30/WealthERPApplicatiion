using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoCustomerProfiling;
using BoProductMaster;
using BoCommon;
using WealthERP.Base;
using VoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Web.Services;
using BoOps;
using Telerik.Web.UI;
using BoCustomerProfiling;
using BOAssociates;

namespace WealthERP.CustomerPortfolio
{
    public partial class CustomerMFAccountAdd : System.Web.UI.UserControl
    {
        CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        CustomerBo customerBo = new CustomerBo();
        ProductMFBo productMfBo = new ProductMFBo();
        AssetBo assetBo = new AssetBo();
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        DataTable dtModeOfHolding;
        DataSet dsCustomerAssociates;
        DataSet dsProductAmc;
        DataSet dsCustomerTypes;
        DataTable dtCustomerAssociatesRaw = new DataTable();
        DataTable dtCustomerAssociates = new DataTable();
        DataRow drCustomerAssociates;
        int accountId;
        DataSet dsbankDetails;
        MFOrderBo mforderBo = new MFOrderBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        static int portfolioId;
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        string path;
        string action;
        int fundGoalId = 0;
        DataSet dsAccountType = new DataSet();
        DataTable dtModeofOperation = new DataTable();
        DataTable dtStates = new DataTable();
        Dictionary<int, int> genDictPortfolioDetails = new Dictionary<int, int>();
        AdvisorVo advisorVo = new AdvisorVo();
        AssociatesBo associatesBo = new AssociatesBo();


        [WebMethod]
        public void CheckTradeNoMFAvailability(string TradeAccNo, string BrokerCode, int PortfolioId)
        {
            //CustomerAccountDao checkAccDao = new CustomerAccountDao();
            //return checkAccDao.CheckTradeNoAvailability(TradeAccNo, BrokerCode, PortfolioId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            try
            {
                radwindowForJointHolder.VisibleOnPageLoad = false;
                radwindowForNominee.VisibleOnPageLoad = false;
                radwindowForGuardian.VisibleOnPageLoad = false;


                SessionBo.CheckSession();
                userVo = (UserVo)Session["userVo"];
                if (!IsPostBack)
                {
                   
                    if (Request.QueryString["Folioaction"] != null)
                    {
                        if (Request.QueryString["Folioaction"].Trim() == "viewFolioDts")
                        {
                            int folioId = Convert.ToInt32(Request.QueryString["FolioId"].ToString());
                            CustomerTransactionBo CustomerTransactionBo = new CustomerTransactionBo();
                           CustomerBankAccountBo customerBankAccountBo =new CustomerBankAccountBo(); 
                            Session["viewFolioDts"] = CustomerTransactionBo.GetCustomerMFFolioDetails(folioId);
                            ViewState["FolioId"] = folioId.ToString();
                      
                      
                            customerAccountsVo = (CustomerAccountsVo)Session["viewFolioDts"];
                            
                            ViewFolioDetails(customerAccountsVo);
                            string folioName = customerBankAccountBo.Getfolioname(folioId);
                            if (folioName != string.Empty)
                            {
                                ddlPortfolio.Items.Add(folioName);
                                ddlPortfolio.SelectedValue = folioName;
                            }
                            lnkEdit.Visible = false;
                            imgAddNominee.Visible = false;
                            imgAddJointHolder.Visible = false;
                            imgAddGuardian.Visible = false;

                        }
                    }
                 
                }

                if (Session["CustomerVo"] != null)
                {
                    customerVo = (CustomerVo)Session["CustomerVo"];
                    hdnCustomerName.Value = customerVo.FirstName + "" + customerVo.MiddleName + "" + customerVo.LastName;

                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    trJointHolders.Visible = false;
                    trJointHoldersGrid.Visible = false;
                    if (Request.QueryString["GoalId"] != null)
                    {
                        fundGoalId = int.Parse(Request.QueryString["GoalId"].ToString());
                        portfolioId = int.Parse(Request.QueryString["PortFolioIdgoal"].ToString());
                        Session[SessionContents.PortfolioId] = portfolioId;
                    }
                    if (!IsPostBack)
                    {
                        BindDropDowns(path);
                        //BindAssociateCode();
                        if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
                        {
                            if (Request.QueryString["action"].Trim() == "Edit")
                            {

                                EditFolioDetails();
                                imgAddNominee.Visible = true;
                                imgAddJointHolder.Visible = true;
                                imgAddGuardian.Visible = true;

                            }
                            else if (Request.QueryString["action"].Trim() == "View")
                            {
                                customerAccountsVo = (CustomerAccountsVo)Session["FolioVo"];
                                ViewFolioDetails(customerAccountsVo);
                                lnkEdit.Visible = true;
                                imgAddNominee.Visible = false;
                                imgAddJointHolder.Visible = false;
                                imgAddGuardian.Visible = false;
                            }
                        }
                        else
                        {
                            SetCustomerNameInInvestorTextBox();
                            BindCustomerBankList();
                            //trNominee2Header.Visible = false;
                            //trJoint2Header.Visible = false;
                            ddlModeOfHolding.Enabled = false;
                            ddlModeOfHolding.SelectedValue = "SI";
                            lnkEdit.Visible = false;
                            rbtnNo.Checked = true;
                            BindModeOfHolding();
                            BindAMC();
                            //LoadNominees();
                            btnSubmit.Visible = true;
                            btnUpdate.Visible = false;
                        }
                        if (Request.QueryString["PortFolioId"] != null)
                        {
                            portfolioId = int.Parse(Request.QueryString["PortFolioId"].ToString());
                            Session[SessionContents.PortfolioId] = portfolioId;
                        }
                        //pra..
                        BindPortfolioDropDown();
                        ddlPortfolio.SelectedValue = portfolioId.ToString();
                        BindCustomerSubType();
                        BindALLBankListForCustomer();
                    }

                    //BindPortfolioDropDown();
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
                FunctionInfo.Add("Method", "CustomerMFAccountAdd.ascx:Page_Load()");
                object[] objects = new object[4];
                objects[0] = path;
                objects[1] = userVo;
                objects[2] = customerVo;
                objects[3] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void BindAssociateCode()
        {
            if (!string.IsNullOrEmpty(txtAssociateCode.Text))
            {
                customerAccountsVo.AssociateCode = txtAssociateCode.Text;
            }
            else
                customerAccountsVo.AssociateCode = string.Empty;
            //if (AgentId.Rows.Count > 0)
            //{
            //    customerAccountsVo.AdviserAgentId = int.Parse(AgentId.Rows[0][1].ToString());

            //}
            //else
            //    customerAccountsVo.AdviserAgentId = 0;
            //DataSet ds = associatesBo.BindAssociateCodeList(advisorVo.advisorId);
            //if (ds != null)
            //{
            //    ddlAssociateCode.DataSource = ds;
            //    ddlAssociateCode.DataValueField = ds.Tables[0].Columns["AAC_AdviserAgentId"].ToString();
            //    ddlAssociateCode.DataTextField = ds.Tables[0].Columns["AAC_AgentCode"].ToString();
            //    ddlAssociateCode.DataBind();
            //}
            //ddlAssociateCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }

        protected void SetCustomerNameInInvestorTextBox()
        {
            if (customerVo != null)
                txtInvestorName.Text = customerVo.FirstName + "" + customerVo.MiddleName + "" + customerVo.LastName;
        }
        private void ViewFolioDetails(CustomerAccountsVo CAVO)
        {
           
            BindDropDowns(path);
         //  portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
           

            divBankDetails.Visible = true;
            customerAccountsVo = CAVO; //(CustomerAccountsVo)Session["FolioVo"];
            if (customerAccountsVo.AccountOpeningDate != DateTime.MinValue)
                txtAccountDate.SelectedDate = customerAccountsVo.AccountOpeningDate;

            txtFolioNumber.Text = customerAccountsVo.AccountNum.ToString();
            BindAMC();
            //ddlProductAmc.SelectedValue = customerAccountsVo.AMCCode.ToString();
            txtAssociateCode.Text = customerAccountsVo.AssociateCode.ToString();
            BindCustomerBankList();
            if (customerAccountsVo.AMCCode != 0)
            {
                ddlProductAmc.SelectedValue = customerAccountsVo.AMCCode.ToString();
            }
            else
            {
                ddlProductAmc.SelectedValue = "0";
            }
            if (!string.IsNullOrEmpty(customerAccountsVo.BankName))
                ddlBankList.SelectedItem.Text = customerAccountsVo.BankName.ToString();
            txtInvestorName.Text = customerAccountsVo.Name;
            if (customerAccountsVo.IsJointHolding == 1)
                rbtnYes.Checked = true;
            else
                rbtnNo.Checked = true;
            if (customerAccountsVo.IsOnline == 0)
                rbtnlIs_online.SelectedValue = "0";

            else
                rbtnlIs_online.SelectedValue = "1";
            BindModeOfHolding();
            //if (customerAccountsVo.ModeOfHoldingCode != "")
            //{
            //    ddlModeOfHolding.SelectedValue = customerAccountsVo.ModeOfHoldingCode.Trim();
            //}
            //else
            //{
            //    ddlModeOfHolding.SelectedIndex = 0;

            //}
            //ddlModeOfHolding.SelectedValue = "SI";
            ddlModeOfHolding.SelectedValue = customerAccountsVo.ModeOfHoldingCode.Trim();
            txtPAddress1.Text = customerAccountsVo.CAddress1;
            txtPAddress2.Text = customerAccountsVo.CAddress2;
            txtPAddress3.Text = customerAccountsVo.CAddress3;
            txtPCity.Text = customerAccountsVo.CCity;
            txtPPinCode.Text = customerAccountsVo.CPinCode.ToString();
            txtCustJName1.Text = customerAccountsVo.JointName1;
            txtCustJName2.Text = customerAccountsVo.JointName2;
            txtCustPhNoOff.Text = customerAccountsVo.CPhoneOffice.ToString();
            txtCustPhNoRes.Text = customerAccountsVo.CPhoneRes.ToString();
            txtCustEmail.Text = customerAccountsVo.CEmail;
            txtBLine1.Text = customerAccountsVo.CMGCXP_BankAddress1;
            txtBLine2.Text = customerAccountsVo.CMGCXP_BankAddress2;
            txtBLine3.Text = customerAccountsVo.CMGCXP_BankAddress3;
            txtCity.Text = customerAccountsVo.CMGCXP_BankCity;
            txtPanNo.Text = customerAccountsVo.PanNumber;
            //txtTaxStatus.Text = customerAccountsVo.TaxStaus;
            txtBrokerCode.Text = customerAccountsVo.BrokerCode;

            BindDropDowns(path);
            ddlAccType.SelectedValue = customerAccountsVo.AccountType;
            txtAccNo.Text = customerAccountsVo.BankAccountNum;
            ddlModeOfOpn.Text = customerAccountsVo.ModeOfOperation;
            txtBankName.Text = customerAccountsVo.BankName;
            txtBranchName.Text = customerAccountsVo.BranchName;
            txtBLine1.Text = customerAccountsVo.BranchAdrLine1;
            txtBLine2.Text = customerAccountsVo.BranchAdrLine2;
            txtBLine3.Text = customerAccountsVo.BranchAdrLine3;
            txtCity.Text = customerAccountsVo.BranchAdrCity;
            txtPinCode.Text = Convert.ToInt32(customerAccountsVo.BranchAdrPinCode).ToString();
            txtMicr.Text = Convert.ToInt32(customerAccountsVo.MICR).ToString();
            ddlBState.Text = customerAccountsVo.BranchAdrState;
            ddlBCountry.Text = ddlBCountry.SelectedValue;
            txtIfsc.Text = customerAccountsVo.IFSC;
            txtExternalFileBankName.Text = customerAccountsVo.BankNameInExtFile;
            BindALLBankListForCustomer();
            if (!string.IsNullOrEmpty(customerAccountsVo.BankName))
            {
                ddlALLBankList.SelectedItem.Text = customerAccountsVo.BankName.ToString();
                ddlALLBankList.SelectedValue = customerAccountsVo.MCmgcxpBankCode;
            }
            else
            {
                ddlALLBankList.SelectedValue = "TBC";
            }
            BindCustomerSubType();

            if (customerAccountsVo.XCT_CustomerTypeCode != "0")
                ddlCustomerType.SelectedValue = customerAccountsVo.XCT_CustomerTypeCode;
            if (customerAccountsVo.XCST_CustomerSubTypeCode != "0")
                ddlCustomerSubType.SelectedValue = customerAccountsVo.XCST_CustomerSubTypeCode;

            if (customerAccountsVo.CDOB != DateTime.MinValue)
                rdpDOB.SelectedDate = customerAccountsVo.CDOB;

            ViewState["ModeOfHolding"] = ddlModeOfHolding.SelectedValue;
            BindAssociates(customerAccountsVo);
            gvNominee2.Enabled = false;
            gvJoint2.Enabled = false;
            SetVisiblity(0);
            txtInvestorName.Enabled = false;
            //trJoint2Header.Visible = false;
            ddlBankList.Enabled = false;
            chkUseProfileName.Visible = false;
            imgBtnAddBank.Visible = false;
            imgBtnRefereshBank.Visible = false;
            ddlAccType.Enabled = false;
            ddlBCountry.Enabled = false;
            ddlBState.Enabled = false;
            ddlCustomerType.Enabled = false;
            ddlCustomerSubType.Enabled = false;
            ddlModeOfOpn.Enabled = false;
            ddlALLBankList.Enabled = false;
            rdpDOB.Enabled = false;
           
        }

        private void BindAssociates(CustomerAccountsVo AccountVo)
        {
            DataTable dtJoinHolder = new DataTable();
            DataTable dtJoinHolderGV = new DataTable();
            DataTable dtGuardian = new DataTable();
            DataTable dtNominees = new DataTable();
            DataTable dtNomineesGV = new DataTable();

            try
            {
                dsCustomerAssociates = customerTransactionBo.GetMFFolioAccountAssociates(AccountVo.AccountId, customerVo.CustomerId);
                dtJoinHolder = dsCustomerAssociates.Tables[2];
                dtNominees = dsCustomerAssociates.Tables[1];
                dtGuardian = dsCustomerAssociates.Tables[0];

                if (AccountVo.IsJointHolding == 1)
                {
                    trAddJointHolder.Visible = true;
                    gvJoint2.Visible = true;
                    if (dtJoinHolder.Rows.Count > 0 && dtJoinHolder != null)
                    {
                        ViewState["JointHold"] = dtJoinHolder;
                        gvJoint2.DataSource = dtJoinHolder;
                        gvJoint2.DataBind();
                        gvJoint2.Visible = true;
                    }
                    else
                    {
                    }
                }

                if (dtNominees.Rows.Count > 0 && dtJoinHolder != null)
                {
                    ViewState["Nominees"] = dtNominees;
                    gvNominee2.DataSource = dtNominees;
                    gvNominee2.DataBind();
                    gvNominee2.Visible = true;
                }
                if (dtGuardian.Rows.Count > 0 && dtJoinHolder != null)
                {
                    ViewState["Guardian"] = dtGuardian;
                    gvGuardian2.DataSource = dtGuardian;
                    gvGuardian2.DataBind();
                    gvGuardian2.Visible = true;
                }
                else
                {
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
                FunctionInfo.Add("Method", "CustomerMFAccountAdd.ascx:BindAssociates()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void EditFolioDetails()
        {

            customerAccountsVo = (CustomerAccountsVo)Session["FolioVo"];
            if (customerAccountsVo.AccountOpeningDate.ToShortDateString() != "01/01/1901" && customerAccountsVo.AccountOpeningDate != null && customerAccountsVo.AccountOpeningDate != DateTime.MinValue)
                txtAccountDate.SelectedDate = customerAccountsVo.AccountOpeningDate;
            else
                txtAccountDate.SelectedDate = null;
            txtFolioNumber.Text = customerAccountsVo.AccountNum.ToString();
            BindAMC();


            ddlProductAmc.SelectedValue = customerAccountsVo.AMCCode.ToString();
            // BindCustomerBankList();
            //if (customerAccountsVo.BankId != 0)
            //{
            //    ddlBankList.SelectedValue = customerAccountsVo.BankId.ToString();
            //}
            //else
            //{
            //    ddlBankList.SelectedValue = "0";
            //}

            if (!string.IsNullOrEmpty(customerAccountsVo.BankName))
                ddlBankList.SelectedItem.Text = customerAccountsVo.BankName.ToString();
            txtInvestorName.Text = customerAccountsVo.Name;
            if (customerAccountsVo.IsJointHolding == 1)
            {
                rbtnYes.Checked = true;
                //trJoint2Header.Visible = true;

            }
            else
            {
                rbtnNo.Checked = true;
                //trJoint2Header.Visible = false;
            }
            if (customerAccountsVo.IsOnline == 0)
            {
                rbtnlIs_online.SelectedValue = "0";
            }

            else
            {
                rbtnlIs_online.SelectedValue = "1";
            }
            BindModeOfHolding();
            if (customerAccountsVo.ModeOfHoldingCode != "" && customerAccountsVo.ModeOfHoldingCode != null)
            {
                ddlModeOfHolding.Enabled = true;
                ddlModeOfHolding.SelectedValue = customerAccountsVo.ModeOfHoldingCode.Trim();
            }
            else
                ddlModeOfHolding.SelectedIndex = 0;
            if (rbtnNo.Checked == true)
                ddlModeOfHolding.Enabled = false;
            else
                ddlModeOfHolding.Enabled = true;

            txtAssociateCode.Text = customerAccountsVo.AssociateCode;
            txtPAddress1.Text = customerAccountsVo.CAddress1;
            txtPAddress2.Text = customerAccountsVo.CAddress2;
            txtPAddress3.Text = customerAccountsVo.CAddress3;
            txtPCity.Text = customerAccountsVo.CCity;
            txtPPinCode.Text = customerAccountsVo.CPinCode.ToString();
            txtCustJName1.Text = customerAccountsVo.JointName1;
            txtCustJName2.Text = customerAccountsVo.JointName2;
            txtCustPhNoOff.Text = customerAccountsVo.CPhoneOffice.ToString();
            txtCustPhNoRes.Text = customerAccountsVo.CPhoneRes.ToString();
            txtCustEmail.Text = customerAccountsVo.CEmail;
            txtBLine1.Text = customerAccountsVo.CMGCXP_BankAddress1;
            txtBLine2.Text = customerAccountsVo.CMGCXP_BankAddress2;
            txtBLine3.Text = customerAccountsVo.CMGCXP_BankAddress3;
            txtCity.Text = customerAccountsVo.CMGCXP_BankCity;
            txtPanNo.Text = customerAccountsVo.PanNumber;
            //txtTaxStatus.Text = customerAccountsVo.TaxStaus;
            txtBrokerCode.Text = customerAccountsVo.BrokerCode;

            BindDropDowns(path);
            if (!string.IsNullOrEmpty(customerAccountsVo.AccountType))
                ddlAccType.SelectedValue = customerAccountsVo.AccountType;
            txtAccNo.Text = customerAccountsVo.BankAccountNum;
            if (!string.IsNullOrEmpty(customerAccountsVo.ModeOfOperation))
                ddlModeOfOpn.Text = customerAccountsVo.ModeOfOperation;
            txtBankName.Text = customerAccountsVo.BankName;
            txtBranchName.Text = customerAccountsVo.BranchName;
            txtBLine1.Text = customerAccountsVo.BranchAdrLine1;
            txtBLine2.Text = customerAccountsVo.BranchAdrLine2;
            txtBLine3.Text = customerAccountsVo.BranchAdrLine3;
            txtCity.Text = customerAccountsVo.BranchAdrCity;


            txtPinCode.Text = Convert.ToInt32(customerAccountsVo.BranchAdrPinCode).ToString();
            txtMicr.Text = Convert.ToInt32(customerAccountsVo.MICR).ToString();
            ddlBState.Text = customerAccountsVo.BranchAdrState;
            ddlBCountry.Text = ddlBCountry.SelectedValue;
            txtIfsc.Text = customerAccountsVo.IFSC;
            txtExternalFileBankName.Text = customerAccountsVo.BankNameInExtFile;


            BindCustomerSubType();
            if (customerAccountsVo.XCT_CustomerTypeCode != "0")
                ddlCustomerType.SelectedValue = customerAccountsVo.XCT_CustomerTypeCode;
            if (customerAccountsVo.XCST_CustomerSubTypeCode != "0")
                ddlCustomerSubType.SelectedValue = customerAccountsVo.XCST_CustomerSubTypeCode;

            if (customerAccountsVo.CDOB != DateTime.MinValue)
                rdpDOB.SelectedDate = customerAccountsVo.CDOB;

            BindALLBankListForCustomer();
            if (!string.IsNullOrEmpty(customerAccountsVo.BankName))
            {
                ddlALLBankList.SelectedItem.Text = customerAccountsVo.BankName.ToString();
                ddlALLBankList.SelectedValue = customerAccountsVo.MCmgcxpBankCode;
            }
            else
            {
                ddlALLBankList.SelectedValue = "TBC";
            }


            ViewState["ModeOfHolding"] = ddlModeOfHolding.SelectedValue;
            BindAssociates(customerAccountsVo);
            Session["CustomerAccountVo"] = customerAccountsVo;
            gvJoint2.Enabled = true;
            gvNominee2.Enabled = true;
            SetVisiblity(1);
            txtInvestorName.Enabled = true;
            ddlBankList.Enabled = true;
            chkUseProfileName.Visible = true;
            imgBtnAddBank.Visible = true;
            imgBtnRefereshBank.Visible = true;
            ddlCustomerType.Enabled = true;
            ddlCustomerSubType.Enabled = true;
            ddlModeOfOpn.Enabled = true;
            ddlALLBankList.Enabled = true;
            rdpDOB.Enabled = true;
            ddlAccType.Enabled = true;
            ddlBState.Enabled = true;
            ddlBCountry.Enabled = true;
        }

        private void SetVisiblity(int p)
        {
            if (p == 0)
            {
                txtAccountDate.Enabled = false;
                txtFolioNumber.Enabled = false;
                ddlModeOfHolding.Enabled = false;
                ddlPortfolio.Enabled = false;
                ddlProductAmc.Enabled = false;
                rbtnNo.Enabled = false;
                rbtnYes.Enabled = false;
                btnSubmit.Visible = false;
                btnUpdate.Visible = false;
                if (Session[SessionContents.CurrentUserRole].ToString() == "Customer")
                {
                    lnkEdit.Visible = false;
                }
                //else
                //    lnkEdit.Visible = false;

                txtAccNo.Enabled = false;
                txtAccountDate.Enabled = false;
                txtBankName.Enabled = false;
                txtBLine1.Enabled = false;
                txtBLine2.Enabled = false;
                txtBLine3.Enabled = false;
                txtBranchName.Enabled = false;
                txtBrokerCode.Enabled = false;
                txtCity.Enabled = false;
                txtCustEmail.Enabled = false;
                txtCustJName1.Enabled = false;
                txtCustJName2.Enabled = false;
                txtCustPhNoOff.Enabled = false;
                txtCustPhNoRes.Enabled = false;
                txtFolioNumber.Enabled = false;
                txtIfsc.Enabled = true;
                txtInvestorName.Enabled = false;
                txtMicr.Enabled = false;
                //txtModeOfOpn.Enabled = false;
                txtPAddress1.Enabled = false;
                txtPAddress2.Enabled = false;
                txtPAddress3.Enabled = false;
                txtPanNo.Enabled = false;
                txtPCity.Enabled = false;
                txtPinCode.Enabled = false;
                //txtPPhone.Enabled = false;
                txtPPinCode.Enabled = false;
                txtTaxStatus.Enabled = false;

            }
            else
            {
                txtAccountDate.Enabled = true;
                txtFolioNumber.Enabled = true;
                //ddlModeOfHolding.Enabled = true;

                txtAccNo.Enabled = true;
                txtAccountDate.Enabled = true;
                txtBankName.Enabled = true;
                txtBLine1.Enabled = true;
                txtBLine2.Enabled = true;
                txtBLine3.Enabled = true;
                txtBranchName.Enabled = true;
                txtBrokerCode.Enabled = true;
                txtCity.Enabled = true;
                txtCustEmail.Enabled = true;
                txtCustJName1.Enabled = true;
                txtCustJName2.Enabled = true;
                txtCustPhNoOff.Enabled = true;
                txtCustPhNoRes.Enabled = true;
                txtFolioNumber.Enabled = true;
                txtIfsc.Enabled = true;
                txtInvestorName.Enabled = true;
                txtMicr.Enabled = true;
                //txtModeOfOpn.Enabled = true;
                txtPAddress1.Enabled = true;
                txtPAddress2.Enabled = true;
                txtPAddress3.Enabled = true;
                txtPanNo.Enabled = true;
                txtPCity.Enabled = true;
                txtPinCode.Enabled = true;
                //txtPPhone.Enabled = true;
                txtPPinCode.Enabled = true;
                txtTaxStatus.Enabled = true;

                ddlPortfolio.Enabled = true;
                ddlProductAmc.Enabled = true;
                rbtnNo.Enabled = true;
                rbtnYes.Enabled = true;
                btnUpdate.Visible = true;
                btnSubmit.Visible = false;
                lnkEdit.Visible = false;
                gvNominee2.Enabled = true;
            }
        }

        private void BindAMC()
        {
            dsProductAmc = productMfBo.GetProductAmc();
            ddlProductAmc.DataSource = dsProductAmc.Tables[0];
            ddlProductAmc.DataTextField = "PA_AMCName";
            ddlProductAmc.DataValueField = "PA_AMCCode";
            ddlProductAmc.DataBind();
            ddlProductAmc.Items.Insert(0, new ListItem("Select", "0"));
        }


        private void BindCustomerSubType()
        {
            dsCustomerTypes = productMfBo.GetCustomerTypes();
            ddlCustomerSubType.DataSource = dsCustomerTypes.Tables[1];
            ddlCustomerSubType.DataTextField = "XCST_CustomersubTypeName";
            ddlCustomerSubType.DataValueField = "XCST_CustomerSubTypeCode";
            ddlCustomerSubType.DataBind();
            ddlCustomerSubType.Items.Insert(0, new ListItem("Select", "0"));

            ddlCustomerType.DataSource = dsCustomerTypes.Tables[0];
            ddlCustomerType.DataTextField = "XCT_CustomerTypeName";
            ddlCustomerType.DataValueField = "XCT_CustomerTypeCode";
            ddlCustomerType.DataBind();
            ddlCustomerType.Items.Insert(0, new ListItem("Select", "0"));
        }


        private void BindModeOfHolding()
        {
            dtModeOfHolding = XMLBo.GetModeOfHolding(path);
            ddlModeOfHolding.DataSource = dtModeOfHolding;
            ddlModeOfHolding.DataTextField = "ModeOfHolding";
            ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
            ddlModeOfHolding.DataBind();
            ddlModeOfHolding.Items.Insert(0, new ListItem("Select", "0"));
        }

        private void BindPortfolioDropDown()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();
            //ddlPortfolio.Items.Insert(0, "Select the Portfolio");

            //ddlPortfolio.SelectedValue = portfolioId.ToString();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                genDictPortfolioDetails.Add(int.Parse(dr["CP_PortfolioId"].ToString()), int.Parse(dr["CP_IsMainPortfolio"].ToString()));
            }

            var keyValuePair = genDictPortfolioDetails.FirstOrDefault(x => x.Key == portfolioId);

            hdnIsMainPortfolio.Value = keyValuePair.Value.ToString();
            Session["genDictPortfolioDetails"] = genDictPortfolioDetails;
            hdnIsCustomerLogin.Value = userVo.UserType;

        }

        private void BindALLBankListForCustomer()
        {

            DataTable ds = customerBankAccountBo.GetALLBankName();
            ddlALLBankList.DataSource = ds;
            ddlALLBankList.DataValueField = ds.Columns["WERPBM_BankCode"].ToString();
            ddlALLBankList.DataTextField = ds.Columns["WERPBM_BankName"].ToString();
            ddlALLBankList.DataBind();
            ddlALLBankList.Items.Insert(0, new ListItem("Select", "0"));
        }

        private void BindCustomerBankList()
        {
            DataSet ds = mforderBo.GetCustomerBank(customerVo.CustomerId);
            ddlBankList.DataSource = ds;
            ddlBankList.DataValueField = ds.Tables[0].Columns["CB_CustBankAccId"].ToString();
            ddlBankList.DataTextField = ds.Tables[0].Columns["WERPBM_BankName"].ToString();
            ddlBankList.DataBind();
            ddlBankList.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;

            if (Session["genDictPortfolioDetails"] != null)
            {
                genDictPortfolioDetails = (Dictionary<int, int>)Session["genDictPortfolioDetails"];
            }
            var keyValuePair = genDictPortfolioDetails.FirstOrDefault(x => x.Key == portfolioId);
            //int value = keyValuePair.Value;           
            hdnIsMainPortfolio.Value = keyValuePair.Value.ToString();
            hdnIsCustomerLogin.Value = userVo.UserType;
        }

        private void LoadGuardians()
        {
            try
            {
                dsCustomerAssociates = customerAccountBo.GetCustomerGuardians(customerVo.CustomerId);
                dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];

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
                    gvGuardian.DataSource = dtCustomerAssociates;
                    gvGuardian.DataBind();
                    gvGuardian.Visible = true;
                    Session["Guardian"] = dtCustomerAssociates;
                }
                else
                {
                    btnAddMinor.Visible = false;
                    divForGuardian.Visible = true;
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
                FunctionInfo.Add("Method", "CustomerMFAccountAdd.ascx:LoadNominees()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }


        private void LoadNominees()
        {
            try
            {
                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];

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
                    gvNominees.DataSource = dtCustomerAssociates;
                    gvNominees.DataBind();
                    gvNominees.Visible = true;
                    Session["Nominee"] = dtCustomerAssociates;
                    //trJoint2Header.Visible = true;
                    //trJoint2HeaderGrid.Visible = true;
                }
                else
                {
                    //trJoint2Header.Visible = false;
                    //trJoint2HeaderGrid.Visible = true;
                    btnAddNominee.Visible = false;
                    DivForNominee.Visible = true;
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
                FunctionInfo.Add("Method", "CustomerMFAccountAdd.ascx:LoadNominees()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btn_amccheck(object sender, EventArgs e)
        {
            if (ddlProductAmc.SelectedIndex == 0)
            {
                txtFolioNumber.Text = "";
                txtFolioNumber.Enabled = false;
            }
            else
            {
                txtFolioNumber.Text = "";
                txtFolioNumber.Enabled = true;

            }

        }

        protected void rbtnNoOnline_CheckedChanged(object sender, EventArgs e)
        { }
        protected void rbtnYesOnline_CheckedChanged1(object sender, EventArgs e)
        { }
        protected void rbtnYes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (rbtnYes.Checked)
                {

                    //if ((object)(Request.QueryString["action"]) != null && Request.QueryString["action"] != "")
                    //{
                    //    trJoint2.Visible = true;
                    //    if (Request.QueryString["action"].Trim() == "Edit")
                    //    {
                    //        EditFolioDetails();
                    //    }
                    //    else if (Request.QueryString["action"].Trim() == "View")
                    //    {
                    //        ViewFolioDetails();
                    //    }
                    //}
                    //else
                    //{

                    ddlModeOfHolding.Enabled = true;
                    ddlModeOfHolding.SelectedIndex = 0;
                    dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                    dtCustomerAssociates.Rows.Clear();
                    dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];
                    dtCustomerAssociates.Columns.Add("MemberCustomerId");
                    dtCustomerAssociates.Columns.Add("AssociationId");
                    dtCustomerAssociates.Columns.Add("Name");
                    dtCustomerAssociates.Columns.Add("Relationship");

                    DataRow drCustomerAssociates;
                    foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
                    {

                        drCustomerAssociates = dtCustomerAssociates.NewRow();
                        drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                        drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                        drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                        drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                        dtCustomerAssociates.Rows.Add(drCustomerAssociates);
                    }

                    if (dtCustomerAssociatesRaw.Rows.Count > 0)
                    {
                        //trJointHolders.Visible = true;
                        //trJointHoldersGrid.Visible = true;
                        gvJointHoldersList.DataSource = dtCustomerAssociates;
                        gvJointHoldersList.DataBind();
                        Session["JointHolder"] = dtCustomerAssociates;
                    }
                    else
                    {
                        //trJointHolders.Visible = false;
                        //trJointHoldersGrid.Visible = false;
                        btnAddJointHolder.Visible = false;
                        DivForJH.Visible = true;
                    }

                }
                else
                {
                    ddlModeOfHolding.SelectedValue = "SI";
                    ddlModeOfHolding.Enabled = false;
                    //trJointHolders.Visible = false;
                    //trJointHoldersGrid.Visible = false;
                    trJoint2.Visible = false;
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
                FunctionInfo.Add("Method", "CustomerMFAccountAdd.ascx:rbtnYes_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlModeOfHolding.SelectedValue == "0")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please select a mode of holding !!');", true);
                }
                else
                {
                    int BankId = 0;
                    Int32.TryParse(ddlBankList.SelectedValue, out BankId);
                    customerAccountsVo.AccountNum = txtFolioNumber.Text;
                    customerAccountsVo.AssetClass = "MF";
                    customerAccountsVo.CustomerId = customerVo.CustomerId;
                    customerAccountsVo.PortfolioId = portfolioId;
                    customerAccountsVo.BankId = BankId;
                    customerAccountsVo.Name = txtInvestorName.Text;

                    //newly added fields for profile
                    if (!string.IsNullOrEmpty(txtPAddress1.Text))
                        customerAccountsVo.CAddress1 = txtPAddress1.Text;
                    if (!string.IsNullOrEmpty(txtPAddress2.Text))
                        customerAccountsVo.CAddress2 = txtPAddress2.Text;
                    if (!string.IsNullOrEmpty(txtPAddress3.Text))
                        customerAccountsVo.CAddress3 = txtPAddress3.Text;
                    if (!string.IsNullOrEmpty(txtPCity.Text))
                        customerAccountsVo.CCity = txtPCity.Text;
                    if (txtPPinCode.Text != "")
                        customerAccountsVo.CPinCode = int.Parse(txtPPinCode.Text);
                    if (!string.IsNullOrEmpty(txtCustJName1.Text))
                        customerAccountsVo.JointName1 = txtCustJName1.Text;
                    if (!string.IsNullOrEmpty(txtCustJName2.Text))
                        customerAccountsVo.JointName2 = txtCustJName2.Text;
                    if (txtCustPhNoOff.Text != "")
                        customerAccountsVo.CPhoneOffice = Convert.ToInt64(txtCustPhNoOff.Text);
                    if (txtCustPhNoRes.Text != "")
                        customerAccountsVo.CPhoneRes = Convert.ToInt64(txtCustPhNoRes.Text);
                    if (!string.IsNullOrEmpty(txtCustEmail.Text))
                        customerAccountsVo.CEmail = txtCustEmail.Text;
                    if (rdpDOB.SelectedDate != null)
                        customerAccountsVo.CDOB = DateTime.Parse(rdpDOB.SelectedDate.ToString());
                    if (!string.IsNullOrEmpty(txtBLine1.Text))
                        customerAccountsVo.CMGCXP_BankAddress1 = txtBLine1.Text;
                    if (!string.IsNullOrEmpty(txtBLine2.Text))
                        customerAccountsVo.CMGCXP_BankAddress2 = txtBLine2.Text;
                    if (!string.IsNullOrEmpty(txtBLine3.Text))
                        customerAccountsVo.CMGCXP_BankAddress3 = txtBLine3.Text;
                    if (!string.IsNullOrEmpty(txtCity.Text))
                        customerAccountsVo.CMGCXP_BankCity = txtCity.Text;
                    if (!string.IsNullOrEmpty(txtPanNo.Text))
                        customerAccountsVo.PanNumber = txtPanNo.Text;
                    //if (!string.IsNullOrEmpty(txtTaxStatus.Text))
                    //customerAccountsVo.TaxStaus = txtTaxStatus.Text;
                    if (!string.IsNullOrEmpty(txtBrokerCode.Text))
                        customerAccountsVo.BrokerCode = txtBrokerCode.Text;
                    //CustomerBankAccountVo CustomerBankAccountVo = new CustomerBankAccountVo();
                    //added fields for bank details
                    if (ddlBankList.SelectedValue != "Select Bank")
                        customerAccountsVo.BankId = int.Parse(ddlBankList.SelectedValue);
                    if (ddlAccType.SelectedIndex != -1)
                        customerAccountsVo.AccountType = ddlAccType.SelectedValue.ToString();
                    if (!string.IsNullOrEmpty(txtAccNo.Text))
                        customerAccountsVo.BankAccountNum = txtAccNo.Text;
                    if (ddlModeOfOpn.SelectedIndex != -1)
                        customerAccountsVo.ModeOfOperation = ddlModeOfOpn.SelectedValue.ToString();

                    if (!string.IsNullOrEmpty(txtBranchName.Text))
                        customerAccountsVo.BranchName = txtBranchName.Text;
                    if (!string.IsNullOrEmpty(txtBLine1.Text))
                        customerAccountsVo.BranchAdrLine1 = txtBLine1.Text;
                    if (!string.IsNullOrEmpty(txtBLine2.Text))
                        customerAccountsVo.BranchAdrLine2 = txtBLine2.Text;
                    if (!string.IsNullOrEmpty(txtBLine3.Text))
                        customerAccountsVo.BranchAdrLine3 = txtBLine3.Text;
                    if (!string.IsNullOrEmpty(txtCity.Text))
                        customerAccountsVo.BranchAdrCity = txtCity.Text;
                    if (ddlBState.SelectedIndex != -1)
                        customerAccountsVo.BranchAdrState = ddlBState.SelectedValue;
                    if (txtPinCode.Text != "")
                        customerAccountsVo.BranchAdrPinCode = int.Parse(txtPinCode.Text);
                    if (txtMicr.Text != "")
                        customerAccountsVo.MICR = txtMicr.Text;

                    if (txtIfsc.Text != "")
                        customerAccountsVo.IFSC = txtIfsc.Text;
                    if (ddlBCountry.SelectedValue != "0")
                        customerAccountsVo.BranchAdrCountry = ddlBCountry.SelectedValue;
                    customerAccountsVo.XCT_CustomerTypeCode = ddlCustomerType.SelectedValue;
                    customerAccountsVo.XCST_CustomerSubTypeCode = ddlCustomerSubType.SelectedValue;
                    customerAccountsVo.BankName = ddlALLBankList.SelectedValue;
                    if (rbtnNo.Checked)
                        customerAccountsVo.IsJointHolding = 0;
                    else
                        customerAccountsVo.IsJointHolding = 1;
                    if (rbtnlIs_online.SelectedValue == "0")
                        customerAccountsVo.IsOnline = 0;
                    else
                        customerAccountsVo.IsOnline = 1;
                    if (ddlModeOfHolding.SelectedValue != "0")
                        customerAccountsVo.ModeOfHolding = ddlModeOfHolding.SelectedItem.Value.ToString();
                    if (txtAccountDate.SelectedDate.ToString() != "")
                        customerAccountsVo.AccountOpeningDate = DateTime.Parse(txtAccountDate.SelectedDate.ToString());

                    customerAccountsVo.AMCCode = int.Parse(ddlProductAmc.SelectedItem.Value.ToString());
                    BindAssociateCode();
                    //if (ddlAssociateCode.SelectedIndex != 0)
                    //{
                    //    customerAccountsVo.AdviserAgentId = int.Parse(ddlAssociateCode.SelectedValue);
                    //    customerAccountsVo.AssociateCode = ddlAssociateCode.SelectedItem.Text;
                    //}
                    //else
                    //{
                    //    customerAccountsVo.AdviserAgentId = 0;
                    //}
                    accountId = customerAccountBo.CreateCustomerMFAccount(customerAccountsVo, userVo.UserId);
                    if (accountId == 1)
                    {
                        txtFolioNumber.Text = string.Empty;
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Folio Number Already Exists');", true);
                        return;
                    }

                    customerAccountsVo.AccountId = accountId;
                    customerAccountAssociationVo.AccountId = accountId;
                    customerAccountAssociationVo.CustomerId = customerVo.CustomerId;
                    foreach (GridDataItem gvr in this.gvNominee2.Items)
                    {
                        int AssociationId = int.Parse(gvr.Cells[3].Text);
                        customerAccountAssociationVo.AssociationId = AssociationId;
                        customerAccountAssociationVo.AssociationType = "Nominee";
                        customerAccountBo.CreateMFAccountAssociation(customerAccountAssociationVo, userVo.UserId);

                    }
                    foreach (GridDataItem gvr in this.gvGuardian2.Items)
                    {
                        int AssociationId = int.Parse(gvr.Cells[3].Text);
                        customerAccountAssociationVo.AssociationId = AssociationId;
                        customerAccountAssociationVo.AssociationType = "Guardian";
                        customerAccountBo.CreateMFAccountAssociation(customerAccountAssociationVo, userVo.UserId);

                    }
                    if (rbtnYes.Checked)
                    {
                        foreach (GridDataItem gvr in this.gvJoint2.Items)
                        {
                            if (gvr is GridDataItem)
                            {
                                int AssociationId = int.Parse(gvr.Cells[3].Text);
                                customerAccountAssociationVo.AssociationId = AssociationId;
                                customerAccountAssociationVo.AssociationType = "Joint Holder";
                                customerAccountBo.CreateMFAccountAssociation(customerAccountAssociationVo, userVo.UserId);

                            }
                        }
                    }

                    Session[SessionContents.CustomerMFAccount] = customerAccountsVo;
                    Session[SessionContents.PortfolioId] = ddlPortfolio.SelectedValue.ToString();

                    if (Request.QueryString["FromPage"] == "MFManualSingleTran")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('MFManualSingleTran','?prevPage=CustomerMFAccountAdd');", true);
                    }
                    else if (Request.QueryString["PortFolioId"] != null)
                    {
                        //Response.Redirect("ControlHost.aspx?pageid=PortfolioSystematicEntry&Folionumber=" + customerAccountsVo.AccountNum + "&FromPage=" + "CustomerMFAccountAdd" + "&action=" + "edit", false);
                        //Response.Redirect("ControlHost.aspx?pageid=PortfolioSystematicEntry&Folionumber=" + customerAccountsVo.AccountNum + "", false);
                        //string action = "Edit";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioSystematicEntry&Folionumber", "loadcontrol('PortfolioSystematicEntry','?FolioNumber=" + accountId + "&FromPage=" + "CustomerMFAccountAdd" + "');", true);
                    }
                    else if (Request.QueryString["GoalId"] != null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortfolioSystematicEntry&Folionumber", "loadcontrol('PortfolioSystematicEntry','?FolioNumber=" + accountId + "&FromPage=" + "CustomerMFAccountAdd" + "&GoalId=" + fundGoalId + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "loadcontrol('CustomerMFFolioView');", true);
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
                FunctionInfo.Add("Method", "CustomerMFAccountAdd.ascx:rbtnYes_CheckedChanged()");
                object[] objects = new object[5];
                objects[0] = customerAccountAssociationVo;
                objects[1] = customerAccountsVo;
                objects[2] = userVo;
                objects[3] = customerVo;
                objects[4] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlModeOfHolding.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please select a mode of holding !!');", true);
            }
            else
            {
                string TradeAccNo;
                string BrokerCode;
                int PortfolioId;
                CustomerAccountsVo newAccountVo = new CustomerAccountsVo();
                CustomerAccountAssociationVo AccountAssociationVo = new CustomerAccountAssociationVo();
                customerAccountsVo = (CustomerAccountsVo)Session["FolioVo"];
                string oldaccount;
                oldaccount = customerAccountsVo.AccountNum;
                newAccountVo.AccountNum = txtFolioNumber.Text;
                int BankId = 0;
                Int32.TryParse(ddlBankList.SelectedValue, out BankId);
                newAccountVo.BankId = BankId;
                newAccountVo.Name = txtInvestorName.Text;
                newAccountVo.AssociateCode = txtAssociateCode.Text;
                if (oldaccount == txtFolioNumber.Text)
                {
                    newAccountVo.AssetClass = "MF";
                    if (rbtnNo.Checked)
                        newAccountVo.IsJointHolding = 0;
                    else
                        newAccountVo.IsJointHolding = 1;
                    if (ddlModeOfHolding.SelectedValue != "0")
                        newAccountVo.ModeOfHoldingCode = ddlModeOfHolding.SelectedItem.Value.ToString();
                    if (txtAccountDate.SelectedDate.ToString() != "")
                        newAccountVo.AccountOpeningDate = DateTime.Parse(txtAccountDate.SelectedDate.ToString());
                    newAccountVo.PortfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
                    newAccountVo.AMCCode = int.Parse(ddlProductAmc.SelectedItem.Value.ToString());
                    newAccountVo.AccountId = customerAccountsVo.AccountId;


                    //Newly added 
                    //newly added fields for profile
                    if (!string.IsNullOrEmpty(txtPAddress1.Text))
                        newAccountVo.CAddress1 = txtPAddress1.Text;
                    if (!string.IsNullOrEmpty(txtPAddress2.Text))
                        newAccountVo.CAddress2 = txtPAddress2.Text;
                    if (!string.IsNullOrEmpty(txtPAddress3.Text))
                        newAccountVo.CAddress3 = txtPAddress3.Text;
                    if (!string.IsNullOrEmpty(txtPCity.Text))
                        newAccountVo.CCity = txtPCity.Text;
                    if (txtPPinCode.Text != "")
                        newAccountVo.CPinCode = int.Parse(txtPPinCode.Text);
                    if (!string.IsNullOrEmpty(txtCustJName1.Text))
                        newAccountVo.JointName1 = txtCustJName1.Text;
                    if (!string.IsNullOrEmpty(txtCustJName2.Text))
                        newAccountVo.JointName2 = txtCustJName2.Text;
                    if (txtCustPhNoOff.Text != "")
                        newAccountVo.CPhoneOffice = Convert.ToInt64(txtCustPhNoOff.Text);
                    if (txtCustPhNoRes.Text != "")
                        newAccountVo.CPhoneRes = Convert.ToInt64(txtCustPhNoRes.Text);
                    if (!string.IsNullOrEmpty(txtCustEmail.Text))
                        newAccountVo.CEmail = txtCustEmail.Text;
                    if (rdpDOB.SelectedDate != null)
                        newAccountVo.CDOB = DateTime.Parse(rdpDOB.SelectedDate.ToString());
                    if (!string.IsNullOrEmpty(txtBLine1.Text))
                        newAccountVo.CMGCXP_BankAddress1 = txtBLine1.Text;
                    if (!string.IsNullOrEmpty(txtBLine2.Text))
                        newAccountVo.CMGCXP_BankAddress2 = txtBLine2.Text;
                    if (!string.IsNullOrEmpty(txtBLine3.Text))
                        newAccountVo.CMGCXP_BankAddress3 = txtBLine3.Text;
                    if (!string.IsNullOrEmpty(txtCity.Text))
                        newAccountVo.CMGCXP_BankCity = txtCity.Text;
                    if (!string.IsNullOrEmpty(txtPanNo.Text))
                        newAccountVo.PanNumber = txtPanNo.Text;
                    if (!string.IsNullOrEmpty(txtBrokerCode.Text))
                        newAccountVo.BrokerCode = txtBrokerCode.Text;
                    //added fields for bank details
                    if (ddlBankList.SelectedValue != "")
                    {
                        if (ddlBankList.SelectedValue != "Select Bank")
                            newAccountVo.BankId = int.Parse(ddlBankList.SelectedValue);
                    }
                    if (ddlAccType.SelectedIndex != -1)
                        newAccountVo.AccountType = ddlAccType.SelectedValue.ToString();
                    if (!string.IsNullOrEmpty(txtAccNo.Text))
                        newAccountVo.BankAccountNum = txtAccNo.Text;
                    if (ddlModeOfOpn.SelectedIndex != -1)
                        newAccountVo.ModeOfOperation = ddlModeOfOpn.SelectedValue.ToString();
                    if (!string.IsNullOrEmpty(ddlALLBankList.SelectedValue))
                        newAccountVo.BankName = ddlALLBankList.SelectedValue;
                    if (!string.IsNullOrEmpty(txtBranchName.Text))
                        newAccountVo.BranchName = txtBranchName.Text;
                    if (!string.IsNullOrEmpty(txtBLine1.Text))
                        newAccountVo.BranchAdrLine1 = txtBLine1.Text;
                    if (!string.IsNullOrEmpty(txtBLine2.Text))
                        newAccountVo.BranchAdrLine2 = txtBLine2.Text;
                    if (!string.IsNullOrEmpty(txtBLine3.Text))
                        newAccountVo.BranchAdrLine3 = txtBLine3.Text;
                    if (!string.IsNullOrEmpty(txtCity.Text))
                        newAccountVo.BranchAdrCity = txtCity.Text;
                    if (ddlBState.SelectedIndex != -1)
                        newAccountVo.BranchAdrState = ddlBState.SelectedValue;
                    if (txtPinCode.Text != "")
                        newAccountVo.BranchAdrPinCode = int.Parse(txtPinCode.Text);
                    if (txtMicr.Text != "")
                        newAccountVo.MICR = txtMicr.Text;
                    if (txtIfsc.Text != "")
                        newAccountVo.IFSC = txtIfsc.Text;
                    if (ddlBCountry.SelectedValue != "0")
                        newAccountVo.BranchAdrCountry = ddlBCountry.SelectedValue;
                    newAccountVo.XCT_CustomerTypeCode = ddlCustomerType.SelectedValue;
                    newAccountVo.XCST_CustomerSubTypeCode = ddlCustomerSubType.SelectedValue;

                    if (rbtnNo.Checked)
                        newAccountVo.IsJointHolding = 0;
                    else
                        newAccountVo.IsJointHolding = 1;
                    if (rbtnlIs_online.SelectedValue == "0")
                    {
                        newAccountVo.IsOnline = 0;
                    }
                    else
                    {
                        newAccountVo.IsOnline = 1;
                    }
                    if (ddlModeOfHolding.SelectedValue != "Select Mode of Holding")
                        newAccountVo.ModeOfHolding = ddlModeOfHolding.SelectedItem.Value.ToString();
                    if (txtAccountDate.SelectedDate.ToString() != "")
                        newAccountVo.AccountOpeningDate = DateTime.Parse(txtAccountDate.SelectedDate.ToString());
                    newAccountVo.AMCCode = int.Parse(ddlProductAmc.SelectedItem.Value.ToString());
                    newAccountVo.CustomerId = customerVo.CustomerId;
                    if (ddlAccType.SelectedIndex != -1)
                        customerAccountsVo.AccountType = ddlAccType.SelectedValue.ToString();
                    if (ddlALLBankList.SelectedIndex != -1)
                        customerAccountsVo.MCmgcxpBankCode = ddlALLBankList.SelectedValue.ToString();

                    //End

                    if (customerTransactionBo.UpdateCustomerMFFolioDetails(newAccountVo, userVo.UserId))
                    {
                        customerTransactionBo.DeleteMFFolioAccountAssociates(newAccountVo.AccountId);
                        AccountAssociationVo.AccountId = newAccountVo.AccountId;
                        AccountAssociationVo.CustomerId = customerVo.CustomerId;
                        RadGrid gvNominee2 = (RadGrid)this.FindControl("gvNominee2");
                        RadGrid gvGuardian2 = (RadGrid)this.FindControl("gvGuardian2");
                        RadGrid gvJoint2 = (RadGrid)this.FindControl("gvJoint2");


                        foreach (GridDataItem gvr in this.gvNominee2.Items)
                        {
                            if (gvr is GridDataItem)
                            {
                                int AssociationId = int.Parse(gvr.Cells[3].Text);
                                AccountAssociationVo.AssociationId = AssociationId;
                                AccountAssociationVo.AssociationType = "Nominee";
                                customerAccountBo.CreateMFAccountAssociation(AccountAssociationVo, userVo.UserId);
                            }

                        }
                        foreach (GridDataItem gvr in this.gvGuardian2.Items)
                        {
                            int AssociationId = int.Parse(gvr.Cells[3].Text);
                            AccountAssociationVo.AssociationId = AssociationId;
                            AccountAssociationVo.AssociationType = "Guardian";
                            customerAccountBo.CreateMFAccountAssociation(AccountAssociationVo, userVo.UserId);

                        }
                        if (rbtnYes.Checked)
                        {
                            foreach (GridDataItem gvr in this.gvJoint2.Items)
                            {
                                int AssociationId = int.Parse(gvr.Cells[3].Text);
                                AccountAssociationVo.AssociationId = AssociationId;
                                AccountAssociationVo.AssociationType = "Joint Holder";
                                customerAccountBo.CreateMFAccountAssociation(AccountAssociationVo, userVo.UserId);

                            }

                        }
                    }

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerMFFolioView','none');", true);
                }

                else
                {
                    TradeAccNo = txtFolioNumber.Text;
                    BrokerCode = customerAccountsVo.AMCCode.ToString();
                    PortfolioId = customerAccountsVo.AccountId;


                    if (ControlHost.CheckTradeNoAvailabilityAccount(TradeAccNo, BrokerCode, PortfolioId))
                    {

                        newAccountVo.AssetClass = "MF";
                        if (rbtnNo.Checked)
                            newAccountVo.IsJointHolding = 0;
                        else
                            newAccountVo.IsJointHolding = 1;
                        if (ddlModeOfHolding.SelectedValue != "Select Mode of Holding")
                            newAccountVo.ModeOfHoldingCode = ddlModeOfHolding.SelectedItem.Value.ToString();
                        if (txtAccountDate.SelectedDate.ToString() != "")
                            newAccountVo.AccountOpeningDate = DateTime.Parse(txtAccountDate.SelectedDate.ToString());
                        newAccountVo.PortfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
                        newAccountVo.AMCCode = int.Parse(ddlProductAmc.SelectedItem.Value.ToString());
                        newAccountVo.AccountId = customerAccountsVo.AccountId;
                        //newly added
                        //newly added fields for profile
                        if (!string.IsNullOrEmpty(txtPAddress1.Text))
                            newAccountVo.CAddress1 = txtPAddress1.Text;
                        if (!string.IsNullOrEmpty(txtPAddress2.Text))
                            newAccountVo.CAddress2 = txtPAddress2.Text;
                        if (!string.IsNullOrEmpty(txtPAddress3.Text))
                            newAccountVo.CAddress3 = txtPAddress3.Text;
                        if (!string.IsNullOrEmpty(txtPCity.Text))
                            newAccountVo.CCity = txtPCity.Text;
                        if (txtPPinCode.Text != "")
                            newAccountVo.CPinCode = int.Parse(txtPPinCode.Text);
                        if (!string.IsNullOrEmpty(txtCustJName1.Text))
                            newAccountVo.JointName1 = txtCustJName1.Text;
                        if (!string.IsNullOrEmpty(txtCustJName2.Text))
                            newAccountVo.JointName2 = txtCustJName2.Text;
                        if (txtCustPhNoOff.Text != "")
                            newAccountVo.CPhoneOffice = int.Parse(txtCustPhNoOff.Text);
                        if (txtCustPhNoRes.Text != "")
                            newAccountVo.CPhoneRes = int.Parse(txtCustPhNoRes.Text);
                        if (!string.IsNullOrEmpty(txtCustEmail.Text))
                            newAccountVo.CEmail = txtCustEmail.Text;
                        if (rdpDOB.SelectedDate != null)
                            newAccountVo.CDOB = DateTime.Parse(rdpDOB.SelectedDate.ToString());
                        if (!string.IsNullOrEmpty(txtBLine1.Text))
                            newAccountVo.CMGCXP_BankAddress1 = txtBLine1.Text;
                        if (!string.IsNullOrEmpty(txtBLine2.Text))
                            newAccountVo.CMGCXP_BankAddress2 = txtBLine2.Text;
                        if (!string.IsNullOrEmpty(txtBLine3.Text))
                            newAccountVo.CMGCXP_BankAddress3 = txtBLine3.Text;
                        if (!string.IsNullOrEmpty(txtCity.Text))
                            newAccountVo.CMGCXP_BankCity = txtCity.Text;
                        if (!string.IsNullOrEmpty(txtPanNo.Text))
                            newAccountVo.PanNumber = txtPanNo.Text;
                        if (!string.IsNullOrEmpty(txtBrokerCode.Text))
                            newAccountVo.BrokerCode = txtBrokerCode.Text;
                        //added fields for bank details
                        if (!string.IsNullOrEmpty(ddlBankList.SelectedValue))
                            newAccountVo.BankId = int.Parse(ddlBankList.SelectedValue);
                        if (ddlAccType.SelectedIndex != -1)
                            newAccountVo.AccountType = ddlAccType.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(txtAccNo.Text))
                            newAccountVo.BankAccountNum = txtAccNo.Text;
                        if (ddlModeOfOpn.SelectedIndex != -1)
                            newAccountVo.ModeOfOperation = ddlModeOfOpn.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(txtBankName.Text))
                            newAccountVo.BankName = txtBankName.Text;
                        if (!string.IsNullOrEmpty(txtBranchName.Text))
                            newAccountVo.BranchName = txtBranchName.Text;
                        if (!string.IsNullOrEmpty(txtBLine1.Text))
                            newAccountVo.BranchAdrLine1 = txtBLine1.Text;
                        if (!string.IsNullOrEmpty(txtBLine2.Text))
                            newAccountVo.BranchAdrLine2 = txtBLine2.Text;
                        if (!string.IsNullOrEmpty(txtBLine3.Text))
                            newAccountVo.BranchAdrLine3 = txtBLine3.Text;
                        if (!string.IsNullOrEmpty(txtCity.Text))
                            newAccountVo.BranchAdrCity = txtCity.Text;
                        if (ddlBState.SelectedIndex != -1)
                            newAccountVo.BranchAdrState = ddlBState.SelectedValue;
                        if (txtPinCode.Text != "")
                            newAccountVo.BranchAdrPinCode = int.Parse(txtPinCode.Text);
                        if (txtMicr.Text != "")
                            newAccountVo.MICR = txtMicr.Text;
                        if (txtIfsc.Text != "")
                            newAccountVo.IFSC = txtIfsc.Text;
                        if (ddlBCountry.SelectedValue != "0")
                            newAccountVo.BranchAdrCountry = ddlBCountry.SelectedValue;
                        newAccountVo.XCT_CustomerTypeCode = ddlCustomerType.SelectedValue;
                        newAccountVo.XCST_CustomerSubTypeCode = ddlCustomerSubType.SelectedValue;

                        if (rbtnNo.Checked)
                            newAccountVo.IsJointHolding = 0;
                        else
                            newAccountVo.IsJointHolding = 1;
                        if (ddlModeOfHolding.SelectedValue != "Select Mode of Holding")
                            newAccountVo.ModeOfHolding = ddlModeOfHolding.SelectedItem.Value.ToString();
                        if (txtAccountDate.SelectedDate.ToString() != "")
                            newAccountVo.AccountOpeningDate = DateTime.Parse(txtAccountDate.SelectedDate.ToString());
                        newAccountVo.AMCCode = int.Parse(ddlProductAmc.SelectedItem.Value.ToString());
                        //end



                        if (customerTransactionBo.UpdateCustomerMFFolioDetails(newAccountVo, userVo.UserId))
                        {
                            customerTransactionBo.DeleteMFFolioAccountAssociates(newAccountVo.AccountId);
                            AccountAssociationVo.AccountId = newAccountVo.AccountId;
                            AccountAssociationVo.CustomerId = customerVo.CustomerId;

                            foreach (GridDataItem gvr in this.gvNominee2.Items)
                            {
                                int AssociationId = int.Parse(gvr.Cells[3].Text);
                                AccountAssociationVo.AssociationId = AssociationId;
                                AccountAssociationVo.AssociationType = "Nominee";
                                customerAccountBo.CreateMFAccountAssociation(AccountAssociationVo, userVo.UserId);

                            }
                            foreach (GridDataItem gvr in this.gvGuardian2.Items)
                            {
                                int AssociationId = int.Parse(gvr.Cells[3].Text);
                                AccountAssociationVo.AssociationId = AssociationId;
                                AccountAssociationVo.AssociationType = "Guardian";
                                customerAccountBo.CreateMFAccountAssociation(AccountAssociationVo, userVo.UserId);

                            }
                            if (rbtnYes.Checked)
                            {
                                foreach (GridDataItem gvr in this.gvJoint2.Items)
                                {
                                    int AssociationId = int.Parse(gvr.Cells[3].Text);
                                    AccountAssociationVo.AssociationId = AssociationId;
                                    AccountAssociationVo.AssociationType = "Joint Holder";
                                    customerAccountBo.CreateMFAccountAssociation(AccountAssociationVo, userVo.UserId);
                                }

                            }
                        }

                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerMFFolioView','none');", true);
                    }



                    else
                    {

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Folio Already Exists');", true);

                    }

                }

            }
        }


        protected void gvJoint2_RowDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                DataTable dt = new DataTable();
                if ((DataTable)ViewState["JointHold"] != null)
                    dt = (DataTable)ViewState["JointHold"];
                CheckBox chkBox = e.Item.FindControl("chkId") as CheckBox;

                int selectedRow = e.Item.ItemIndex + 1;
                foreach (DataRow dr in dt.Rows)
                {
                    if (gvJoint2.MasterTableView.DataKeyValues[selectedRow - 1]["AssociateId"].ToString() == dr["AssociateId"].ToString() && dr["Stat"].ToString() == "yes")
                        chkBox.Checked = true;

                }
            }
        }

        protected void gvNominee2_RowDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    DataTable dt = new DataTable();
            //    if ((DataTable)ViewState["Nominees"] != null)
            //        dt = (DataTable)ViewState["Nominees"];

            //    CheckBox chkBox = e.Item.FindControl("chkId") as CheckBox;

            //    int selectedRow = e.Item.ItemIndex + 1;
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        if (gvNominee2.MasterTableView.DataKeyValues[selectedRow - 1]["AssociateId"].ToString() == dr["AssociateId"].ToString() && dr["Stat"].ToString() == "yes")
            //            chkBox.Checked = true;

            //    }

            //}
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            EditFolioDetails();
            btnUpdate.Visible = true;

            imgAddNominee.Visible = true;
            imgAddJointHolder.Visible = true;
            imgAddGuardian.Visible = true;
        }

        protected void rbtnYes_CheckedChanged1(object sender, EventArgs e)
        {
            #region unused


            //trAddJointHolder.Visible = true;
            //CustomerAccountsVo AccountVo = new CustomerAccountsVo();
            //if (Session["CustomerAccountVo"] != null)
            //{
            //    AccountVo = (CustomerAccountsVo)Session["CustomerAccountVo"];
            //}
            ddlModeOfHolding.Enabled = true;
            ddlModeOfHolding.SelectedIndex = 0;

            //    //when addding MF Folio Account 
            //    dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
            //    dtCustomerAssociates.Rows.Clear();
            //    dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];
            //    dtCustomerAssociatesRaw.Columns.Add("MemberCustomerId");
            //    dtCustomerAssociatesRaw.Columns.Add("AssociationId");
            //    dtCustomerAssociatesRaw.Columns.Add("Name");
            //    dtCustomerAssociatesRaw.Columns.Add("Relationship");

            //    DataRow drCustomerAssociates;
            //    foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
            //    {

            //        drCustomerAssociates = dtCustomerAssociates.NewRow();
            //        drCustomerAssociates[0] = dr["MemberCustomerId"].ToString();
            //        drCustomerAssociates[1] = dr["AssociationId"].ToString();
            //        drCustomerAssociates[2] = dr["Name"].ToString();
            //        drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
            //        dtCustomerAssociates.Rows.Add(drCustomerAssociates);
            //    }

            //    if (dtCustomerAssociatesRaw.Rows.Count > 0)
            //    {
            //        trJointHolders.Visible = true;
            //        trJointHoldersGrid.Visible = true;
            //        gvJointHoldersList.DataSource = dtCustomerAssociates;
            //        gvJointHoldersList.DataBind();
            //    }
            //    else
            //    {
            //        trJointHolders.Visible = false;
            //        trJointHoldersGrid.Visible = false;
            //    }

            #endregion
            trAddJointHolder.Visible = true;
            gvJoint2.Visible = true;
        }

        //protected void foliobutton_click(object sender, EventArgs e)
        //{

        //}

        protected void rbtnNo_CheckedChanged(object sender, EventArgs e)
        {
            trAddJointHolder.Visible = false;
            if (ddlModeOfHolding.SelectedIndex != 0)
            {
                ViewState["ModeOfHolding"] = ddlModeOfHolding.SelectedValue;
            }
            ddlModeOfHolding.SelectedValue = "SI";
            ddlModeOfHolding.Enabled = false;
            trJointHolders.Visible = false;
            trJointHoldersGrid.Visible = false;

            gvJoint2.Visible = false;
            trJoint2.Visible = false;


        }
        [WebMethod]
        public void CheckTradeNoAvailability(string TradeAccNo, string BrokerCode, int PortfolioId)
        {
            //CustomerAccountDao checkAccDao = new CustomerAccountDao();
            //return checkAccDao.CheckTradeNoAvailability(TradeAccNo, BrokerCode, PortfolioId);
        }

        protected void imgBtnRefereshBank_OnClick(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            BindCustomerBankList();
        }

        protected void ddlBankList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBankList.SelectedIndex != 0)
            {
                int customerId = customerVo.CustomerId;
                int bankId = int.Parse(ddlBankList.SelectedValue);
                dsbankDetails = new DataSet();
                dsbankDetails = customerPortfolioBo.getBankDetailsForCustomer(customerId, bankId);
                divBankDetails.Visible = true;
                FillBankDetails();
            }
            else
            {
                divBankDetails.Visible = false;
            }
        }

        public void FillBankDetails()
        {
            BindDropDowns(path);

            txtAccNo.Text = dsbankDetails.Tables[0].Rows[0]["CB_AccountNum"].ToString();
            txtBankName.Text = dsbankDetails.Tables[0].Rows[0]["WERPBM_BankName"].ToString();
            txtBranchName.Text = dsbankDetails.Tables[0].Rows[0]["CB_BranchName"].ToString();
            txtBLine1.Text = dsbankDetails.Tables[0].Rows[0]["CB_BranchAdrLine1"].ToString();
            txtBLine2.Text = dsbankDetails.Tables[0].Rows[0]["CB_BranchAdrLine2"].ToString();
            txtBLine3.Text = dsbankDetails.Tables[0].Rows[0]["CB_BranchAdrLine3"].ToString();
            if (dsbankDetails.Tables[0].Rows[0]["CB_BranchAdrState"].ToString() != "Select" && dsbankDetails.Tables[0].Rows[0]["CB_BranchAdrState"].ToString().Equals("") != true)

                ddlBState.SelectedValue = dsbankDetails.Tables[0].Rows[0]["CB_BranchAdrState"].ToString();

            if (dsbankDetails.Tables[0].Rows[0]["XMOH_ModeOfHoldingCode"].ToString().Trim() != "")
                ddlModeOfOpn.SelectedValue = dsbankDetails.Tables[0].Rows[0]["XMOH_ModeOfHoldingCode"].ToString().Trim();
            ddlAccType.SelectedValue = dsbankDetails.Tables[0].Rows[0]["PAIC_AssetInstrumentCategoryCode"].ToString().Trim();
            ddlALLBankList.SelectedValue = dsbankDetails.Tables[0].Rows[0]["WERPBM_BankCode"].ToString();
            txtPinCode.Text = dsbankDetails.Tables[0].Rows[0]["CB_BranchAdrPinCode"].ToString();
            txtMicr.Text = dsbankDetails.Tables[0].Rows[0]["CB_MICR"].ToString();
            txtIfsc.Text = dsbankDetails.Tables[0].Rows[0]["CB_IFSC"].ToString();
            txtCity.Text = dsbankDetails.Tables[0].Rows[0]["CB_BranchAdrCity"].ToString();
        }

        public void BindDropDowns(string path)
        {
            try
            {
                //dsAccountType = customerAccountBo.GetAccountType();
                //    //XMLBo.GetBankAccountTypes(path);
                //ddlAccType.DataSource = dsAccountType;
                //ddlAccType.DataTextField = "XBAT_BankAccountTye";
                //ddlAccType.DataValueField = "XBAT_BankAccountTypeCode";
                //ddlAccType.DataBind();
                //ddlAccType.Items.Insert(0, new ListItem("Select", "0"));
                DataTable dt = new DataTable();

                dt = customerBankAccountBo.AssetBankaccountType();
                ddlAccType.DataSource = dt;
                ddlAccType.DataValueField = dt.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                ddlAccType.DataTextField = dt.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                ddlAccType.DataBind();
                ddlAccType.Items.Insert(0, new ListItem("Select", "Select"));


                dtModeofOperation = XMLBo.GetModeOfHolding(path);
                ddlModeOfOpn.DataSource = dtModeofOperation;
                ddlModeOfOpn.DataTextField = "ModeOfHolding";
                ddlModeOfOpn.DataValueField = "ModeOfHoldingCode";
                ddlModeOfOpn.DataBind();
                ddlModeOfOpn.Items.Insert(0, new ListItem("Select", "0"));

                dtStates = XMLBo.GetStates(path);
                ddlBState.DataSource = dtStates;
                ddlBState.DataTextField = "StateName";
                ddlBState.DataValueField = "StateCode";
                ddlBState.DataBind();
                ddlBState.Items.Insert(0, new ListItem("Select", "0"));
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AddBankDetails.ascx:BindDropDowns()");
                object[] objects = new object[4];
                objects[0] = path;
                objects[1] = dsAccountType;
                objects[2] = dtModeofOperation;
                objects[3] = dtStates;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void imgAddNominee_Click(object sender, EventArgs e)
        {
            LoadNominees();
            radwindowForNominee.VisibleOnPageLoad = true;
        }
        protected void imgAddGuardian_Click(object sender, EventArgs e)
        {
            radwindowForGuardian.VisibleOnPageLoad = true;
            LoadGuardians();
        }

        protected void imgAddBankForTBC_Click(object sender, EventArgs e)
        {
            bool isUpdated = false;
            isUpdated = customerAccountBo.UpdateBankDetails(customerVo.CustomerId, ddlALLBankList.SelectedValue.ToString(), int.Parse(ddlProductAmc.SelectedValue), txtFolioNumber.Text);
        }

        protected void imgAddJointHolder_Click(object sender, EventArgs e)
        {

            dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
            dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];

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
                gvJointHoldersList.DataSource = dtCustomerAssociates;
                gvJointHoldersList.DataBind();
                gvJointHoldersList.Visible = true;

                Session["JointHolder"] = dtCustomerAssociates;
                //trJoint2Header.Visible = true;
                //trJoint2HeaderGrid.Visible = true;
            }
            else
            {
                //trJoint2Header.Visible = false;
                //trJoint2HeaderGrid.Visible = true;
                btnAddJointHolder.Visible = false;
                DivForJH.Visible = true;
            }


            radwindowForJointHolder.VisibleOnPageLoad = true;
        }

        protected void btnAddGuardian_Click(object sender, EventArgs e)
        {
            CheckBox chkbox = new CheckBox();
            hdnAssociationIdForGuardian.Value = "";
            DataTable dtBindTableWithSelectedGuardian = new DataTable();
            DataTable dtNominee = new DataTable();
            if (dtNominee != null)
                dtNominee = null;
            dtNominee = (DataTable)Session["Guardian"];
            string strNomineeAssnId = string.Empty;
            customerAccountsVo.AccountId = accountId;
            customerAccountAssociationVo.AccountId = accountId;
            customerAccountAssociationVo.CustomerId = customerVo.CustomerId;
            foreach (GridDataItem gvr in this.gvGuardian.Items)
            {
                chkbox = (CheckBox)gvr.FindControl("chkId0"); // accessing the CheckBox control
                if (chkbox.Checked == true)
                {
                    hdnAssociationIdForGuardian.Value = gvGuardian.MasterTableView.DataKeyValues[gvr.ItemIndex]["AssociationId"].ToString();
                    strNomineeAssnId = strNomineeAssnId + hdnAssociationIdForGuardian.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(strNomineeAssnId))
            {
                strNomineeAssnId = strNomineeAssnId.TrimEnd(',');
                string expression;
                expression = "AssociationId in" + "(" + strNomineeAssnId + ")";
                DataRow[] foundRows;
                foundRows = dtNominee.Select(expression);
                dtBindTableWithSelectedGuardian.Rows.Clear();
                dtBindTableWithSelectedGuardian.Columns.Add("MemberCustomerId");
                dtBindTableWithSelectedGuardian.Columns.Add("AssociationId");
                dtBindTableWithSelectedGuardian.Columns.Add("Name");
                dtBindTableWithSelectedGuardian.Columns.Add("XR_Relationship");
                foreach (DataRow dr in foundRows)
                {
                    dr.BeginEdit();
                    dtBindTableWithSelectedGuardian.Rows.Add(dr.ItemArray);
                    dtBindTableWithSelectedGuardian.AcceptChanges();
                }

                gvGuardian2.DataSource = dtBindTableWithSelectedGuardian;
                gvGuardian2.DataBind();
                gvGuardian2.Visible = true;
            }
        }

        protected void btnAddNominee_Click(object sender, EventArgs e)
        {
            CheckBox chkbox = new CheckBox();
            hdnAssociationIdForNominee.Value = "";
            DataTable dtBindTableWithSelectedNominee = new DataTable();
            DataTable dtNominee = new DataTable();
            if (dtNominee != null)
                dtNominee = null;
            dtNominee = (DataTable)Session["Nominee"];
            string strNomineeAssnId = string.Empty;
            customerAccountsVo.AccountId = accountId;
            customerAccountAssociationVo.AccountId = accountId;
            customerAccountAssociationVo.CustomerId = customerVo.CustomerId;
            foreach (GridDataItem gvr in this.gvNominees.Items)
            {
                chkbox = (CheckBox)gvr.FindControl("chkId0"); // accessing the CheckBox control
                if (chkbox.Checked == true)
                {
                    hdnAssociationIdForNominee.Value = gvNominees.MasterTableView.DataKeyValues[gvr.ItemIndex]["AssociationId"].ToString();
                    strNomineeAssnId = strNomineeAssnId + hdnAssociationIdForNominee.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(strNomineeAssnId))
            {
                strNomineeAssnId = strNomineeAssnId.TrimEnd(',');
                string expression;
                expression = "AssociationId in" + "(" + strNomineeAssnId + ")";
                DataRow[] foundRows;
                foundRows = dtNominee.Select(expression);
                dtBindTableWithSelectedNominee.Rows.Clear();
                dtBindTableWithSelectedNominee.Columns.Add("MemberCustomerId");
                dtBindTableWithSelectedNominee.Columns.Add("AssociationId");
                dtBindTableWithSelectedNominee.Columns.Add("Name");
                dtBindTableWithSelectedNominee.Columns.Add("XR_Relationship");
                foreach (DataRow dr in foundRows)
                {
                    dr.BeginEdit();
                    dtBindTableWithSelectedNominee.Rows.Add(dr.ItemArray);
                    dtBindTableWithSelectedNominee.AcceptChanges();
                }

                gvNominee2.DataSource = dtBindTableWithSelectedNominee;
                gvNominee2.DataBind();
                gvNominee2.Visible = true;
            }
        }

        protected void btnAddJointHolder_Click(object sender, EventArgs e)
        {
            CheckBox chkbox = new CheckBox();
            hdnAssociationIdForJointHolder.Value = "";
            DataTable dtBindTableWithSelectedJointHolder = new DataTable();
            DataTable dtJointHolder = new DataTable();
            if (dtJointHolder != null)
                dtJointHolder = null;
            dtJointHolder = (DataTable)Session["JointHolder"];
            string strJointHolderAssnId = string.Empty;
            customerAccountsVo.AccountId = accountId;
            customerAccountAssociationVo.AccountId = accountId;
            customerAccountAssociationVo.CustomerId = customerVo.CustomerId;

            foreach (GridDataItem gvr in this.gvJointHoldersList.Items)
            {
                chkbox = (CheckBox)gvr.FindControl("chkId"); // accessing the CheckBox control
                if (chkbox.Checked == true)
                {
                    hdnAssociationIdForJointHolder.Value = gvJointHoldersList.MasterTableView.DataKeyValues[gvr.ItemIndex]["AssociationId"].ToString();
                    strJointHolderAssnId = strJointHolderAssnId + hdnAssociationIdForJointHolder.Value + ",";
                }
            }

            if (!string.IsNullOrEmpty(strJointHolderAssnId))
            {
                strJointHolderAssnId = strJointHolderAssnId.TrimEnd(',');
                string expression;
                expression = "AssociationId in" + "(" + strJointHolderAssnId + ")";
                DataRow[] foundRows;
                foundRows = dtJointHolder.Select(expression);
                dtBindTableWithSelectedJointHolder.Rows.Clear();
                dtBindTableWithSelectedJointHolder.Columns.Add("MemberCustomerId");
                dtBindTableWithSelectedJointHolder.Columns.Add("AssociationId");
                dtBindTableWithSelectedJointHolder.Columns.Add("Name");
                dtBindTableWithSelectedJointHolder.Columns.Add("XR_Relationship");
                foreach (DataRow dr in foundRows)
                {
                    dr.BeginEdit();
                    dtBindTableWithSelectedJointHolder.Rows.Add(dr.ItemArray);
                    dtBindTableWithSelectedJointHolder.AcceptChanges();
                }

                gvJoint2.DataSource = dtBindTableWithSelectedJointHolder;
                gvJoint2.DataBind();
                gvJoint2.Visible = true;
            }
        }
    }
}
