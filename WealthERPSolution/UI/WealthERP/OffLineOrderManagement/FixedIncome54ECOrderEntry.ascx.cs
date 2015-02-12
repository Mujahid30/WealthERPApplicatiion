using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCustomerPortfolio;
using VoUser;
using BoCustomerProfiling;
using WealthERP.Base;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoAdvisorProfiling;
using BoUploads;
using Telerik.Web.UI;
using BoProductMaster;
using BoOps;
using BOAssociates;
using System.Configuration;
using VoOps;
using BoWerpAdmin;
using VoCustomerPortfolio;
using VoCustomerProfiling;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using VOAssociates;
using BOAssociates;
using iTextSharp.text.pdf;
using System.IO;
using System.Resources;
using System.Web.UI.HtmlControls;
using System.Transactions;
using VoCommon;
using BoOfflineOrderManagement;

namespace WealthERP.OffLineOrderManagement
{
    public partial class FixedIncome54ECOrderEntry : System.Web.UI.UserControl
    {

        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorVo advisorVo;
        UserVo tempUserVo = new UserVo();
        OperationBo operationBo = new OperationBo();
        MFOrderBo mfOrderBo = new MFOrderBo();
        ProductMFBo productMFBo = new ProductMFBo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        AssetBo assetBo = new AssetBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        OrderBo orderbo = new OrderBo();
        MFOrderVo mforderVo = new MFOrderVo();
        OrderVo orderVo = new OrderVo();
        FIOrderBo fiorderBo = new FIOrderBo();
        FIOrderVo fiorderVo = new FIOrderVo();
        RMVo rmVo = new RMVo();
        AssociatesBo associatesBo = new AssociatesBo();
        AssociatesVO associatesVo = new AssociatesVO();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        OfflineBondOrderBo OfflineBondOrderBo = new OfflineBondOrderBo();
        RepositoryVo repoVo;

        BoDematAccount boDematAccount = new BoDematAccount();

        UserVo userVo;
        PriceBo priceBo = new PriceBo();
        string path;
        DataTable dtBankName = new DataTable();
        DataTable dtFrequency;
        DataTable ISAList;
        int customerId;
        int amcCode;
        string categoryCode;
        int portfolioId;
        int schemePlanCode;
        int Aflag = 0;
        int Sflag = 0;
        int orderId;
        int orderNumber = 0;
        string ViewForm = string.Empty;
        string updatedStatus = "";
        string updatedReason = "";
        bool result = false;
        string userType = string.Empty;
        string mail = string.Empty;
        string AgentCode;
        DataTable AgentId = new DataTable();
        DataTable Agentname;
        String TransType;
        string fileExtension = string.Empty;

        string strRepositoryPath = string.Empty;


        string imgPath = "";
        string TargetPath = "";
        string imageUploadPath = "";
        CustomerProofUploadsVO CPUVo = new CustomerProofUploadsVO();

        System.Drawing.Image thumbnail_image = null;
        System.Drawing.Image original_image = null;
        System.Drawing.Bitmap final_image = null;
        System.Drawing.Graphics graphic = null;
        MemoryStream ms = null;

        DataSet dsCustomerProof = new DataSet();

        int custBankAccId;

        string strGuid = string.Empty;
        RepositoryBo repoBo;
        float fStorageBalance;
        float fMaxStorage;
        string linkAction = "";
        DataRow drCustomerAssociates;
        DataSet dsCustomerAssociates;
        DataTable dtCustomerAssociates = new DataTable();
        DataTable dtCustomerAssociatesRaw = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            radCustomApp.Visible = false;
            associatesVo = (AssociatesVO)Session["associatesVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];

            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

            strRepositoryPath = string.Empty;
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            GetUserType();
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                AutoCompleteExtender1.ContextKey = advisorVo.advisorId.ToString();
                AutoCompleteExtender1.ServiceMethod = "GetAdviserCustomerPan";
                AutoCompleteExtender2.ContextKey = advisorVo.advisorId.ToString();
                AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetails";

            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";

            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                AutoCompleteExtender1.ContextKey = advisorVo.advisorId.ToString();
                AutoCompleteExtender1.ServiceMethod = "GetAdviserCustomerPan";
                txtAssociateSearch.Text = associateuserheirarchyVo.AgentCode;
                GetAgentName(associateuserheirarchyVo.AdviserAgentId);
                txtAgentId.Value = associateuserheirarchyVo.AdviserAgentId.ToString();
                AutoCompleteExtender2.ContextKey = associateuserheirarchyVo.AgentCode + "/" + advisorVo.advisorId.ToString();
                AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetailsForAssociates";

            }



            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(ViewForm))
                    RadDateControlBusinessDateValidation(ref txtPaymentInstDate, DateTime.Now);
                FICategory();
                //  FIScheme(advisorVo.advisorId, "0");                
                lblPanDuplicate.Visible = false;
                rbtnIndividual.Checked = true;
                trIndividualName.Visible = false;
                trNonIndividualName.Visible = false;
                //radCustomApp.VisibleOnPageLoad = false;
                BindSubTypeDropDown(1001);
                BindBanks(0);
                if (Request.QueryString["action"] != null)
                {
                    if (Request.QueryString["action"].Trim() == "Edit")
                    {
                        orderId = int.Parse(Request.QueryString["orderId"].ToString());
                        txtCustomerId.Value = Request.QueryString["customeId"].ToString();
                        if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
                            GetUserType();
                        else
                            trOfficeUse.Visible = true;
                        lblAssociate.Visible = true;
                        lblAssociatetext.Text = Request.QueryString["associatename"].ToString();
                        txtAssociateSearch.Text = Request.QueryString["agentcode"].ToString();
                        GetcustomerDetails();
                        View54ECOrderDetails(orderId);
                        lnkBtnEdit();
                        trOrder.Visible = true;
                        tbUploadDocument.Visible = true;
                        BindDocument(orderId);
                        btnUpdate.Visible = true;
                        lnkBtnFIEdit.Visible = false;
                        SetFICOntrolsEnablity(true);

                    }

                    else
                    {
                        orderId = int.Parse(Request.QueryString["orderId"].ToString());
                        txtCustomerId.Value = Request.QueryString["customeId"].ToString();
                        lblAssociate.Visible = true;
                        if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
                            GetUserType();
                        else
                            trOfficeUse.Visible = true;
                        lblAssociatetext.Text = Request.QueryString["associatename"].ToString();
                        txtAssociateSearch.Text = Request.QueryString["agentcode"].ToString();
                        GetcustomerDetails();
                        trOrder.Visible = true;
                        BindDocument(orderId);
                        View54ECOrderDetails(orderId);
                        lnkBtnEdit();
                        tbUploadDocument.Visible = true;
                        lnkBtnFIEdit.Visible = true;
                        btnUpdate.Visible = false;
                        SetFICOntrolsEnablity(false);

                    }

                }

            }
            repoBo = new RepositoryBo();

            fStorageBalance = repoBo.GetAdviserStorageValues(advisorVo.advisorId, out fMaxStorage);


        }
        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
        }
        protected void txtAgentId_ValueChanged1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAgentId.Value.ToString().Trim()))
            {
                GetAgentName(int.Parse(txtAgentId.Value));
            }
        }


        protected void txtCustomerId_ValueChanged1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {
                Session["customerid"] = txtCustomerId.Value.ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
                customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
                Session["customerVo"] = customerVo;
                lblGetBranch.Text = customerVo.BranchName;
                lblgetPan.Text = customerVo.PANNum;
                customerId = int.Parse(txtCustomerId.Value);
                if (ddlsearch.SelectedItem.Value == "2")
                    lblgetcust.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;
                OnTaxStatus();
                BindBanks(customerId);

                CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
                customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(int.Parse(txtCustomerId.Value));

                hdnPortfolioId.Value = customerPortfolioVo.PortfolioId.ToString();

                Table3.Visible = true;
                Panel1.Visible = true;
            }
        }

        private void BindBanks(int customerId)
        {
            DataSet dsBankName = mfOrderBo.GetCustomerBanks(customerId);
            if (dsBankName.Tables[0].Rows.Count > 0)
            {
                ddlBankName.DataSource = dsBankName;
                ddlBankName.DataValueField = dsBankName.Tables[0].Columns["WCMV_LookupId"].ToString();
                ddlBankName.DataTextField = dsBankName.Tables[0].Columns["WCMV_Name"].ToString();
                ddlBankName.DataBind();
                ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {
                ddlBankName.Items.Clear();
                ddlBankName.DataSource = null;
                ddlBankName.DataBind();
                ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }

        private void OnTaxStatus()
        {

            if (!string.IsNullOrEmpty(txtCustomerId.Value))
            {

                DataSet dtDpProofTypes = new DataSet();
                dtDpProofTypes = fiorderBo.GetTaxStatus1(Convert.ToInt32(txtCustomerId.Value));

                if (dtDpProofTypes.Tables[0].Rows.Count > 0)
                {
                    ddlTax.DataSource = dtDpProofTypes.Tables[0];
                    ddlTax.DataValueField = dtDpProofTypes.Tables[0].Columns["WCMV_Name"].ToString();
                    ddlTax.DataTextField = dtDpProofTypes.Tables[0].Columns["WCMV_Name"].ToString();
                    ddlTax.DataBind();
                }
                //ddlTax.Items.Insert(0, new ListItem("Select", "Select"));

                //ddlTax.Text = fiorderBo.GetTaxStatus(Convert.ToInt32(txtCustomerId.Value));
            }
        }

        public void clearPancustomerDetails()
        {
            lblgetPan.Text = "";
            txtCustomerName.Text = "";
            txtPansearch.Text = "";
            lblgetcust.Text = "";
        }

        protected void ddlsearch_Selectedindexchanged(object sender, EventArgs e)
        {

            if (ddlsearch.SelectedItem.Text == "Customer")
            {
                clearPancustomerDetails();
                trCust.Visible = true;
                trpan.Visible = false;
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";

                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                }

            }
            else if (ddlsearch.SelectedItem.Text == "PAN")
            {
                clearPancustomerDetails();
                trCust.Visible = false;
                trpan.Visible = true;
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    AutoCompleteExtender1.ContextKey = advisorVo.advisorId.ToString();
                    AutoCompleteExtender1.ServiceMethod = "GetAdviserCustomerPan";

                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                }

            }
        }

        private void GetAgentName(int agentId)
        {
            // Admin after selecting agent code and sales login default 
            lblAssociate.Visible = true;
            lblAssociateReport.Visible = true;
            Agentname = customerBo.GetSubBrokerName(agentId);
            if (Agentname.Rows.Count > 0)
            {
                lblAssociatetext.Text = Agentname.Rows[0][0].ToString();
                lblAssociateReportTo.Text = Agentname.Rows[0][4].ToString();
                if (!string.IsNullOrEmpty(Agentname.Rows[0][3].ToString()))
                {
                    //  lb1EUIN.Text = Agentname.Rows[0][3].ToString();
                }
                else
                {
                    // lb1EUIN.Text = string.Empty;
                }
            }
        }

        public void GetUserType()
        {
            string usertype = string.Empty;
            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!string.IsNullOrEmpty(Session[SessionContents.RmVo].ToString()))
                rmVo = (RMVo)Session[SessionContents.RmVo];
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
            {
                userType = "associates";
                associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                if (associateuserheirarchyVo.AgentCode != null)
                {
                    AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                    if (Request.QueryString["action"] != null)
                    {
                        usertype = fiorderBo.GetUserType(advisorVo.advisorId, associateuserheirarchyVo.AdviserAgentId);
                        if (usertype == "RM" || usertype == "BM")
                        {
                            trOfficeUse.Visible = true;
                        }
                        else
                            trOfficeUse.Visible = false;
                    }
                }
                else
                    AgentCode = "0";
            }
        }


        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ddlSeries.DataSource = dt;

            if (ddlCategory.SelectedIndex != 0)
            {

                FIScheme(advisorVo.advisorId, ddlCategory.SelectedValue);
            }

            // FIIssuer(advisorVo.advisorId);
            if (ddlCategory.SelectedValue == "FICG")
            {
                trSchemeOpFreq.Visible = false;
                trDepPaypriv.Visible = false;
                Label8.Text = "Capital Amount";
            }
            else
            {
                //for fd 
                // trSchemeOpFreq.Visible = true;
                // trDepPaypriv.Visible = true;
                Label8.Text = "FD Amount";
            }
            if (ddlCategory.SelectedValue == "FICGCG")
            {
                tdLabel11.Visible = false;
                tdddlTranstype.Visible = false;
            }
            else
            {
                tdLabel11.Visible = true;
                tdddlTranstype.Visible = true;
            }
        }


        private void FIIssuer(int AdviserId)
        {
            DataSet dsIssuer = fiorderBo.GetFIIssuer(AdviserId, ddlCategory.SelectedValue);
            if (dsIssuer.Tables[0].Rows.Count > 0)
            {
                ddlIssuer.DataSource = dsIssuer;
                ddlIssuer.DataValueField = dsIssuer.Tables[0].Columns["PFIIM_IssuerId"].ToString();
                ddlIssuer.DataTextField = dsIssuer.Tables[0].Columns["PFIIM_IssuerName"].ToString();
                ddlIssuer.DataBind();
                ddlIssuer.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {
                ddlIssuer.Items.Clear();
                ddlIssuer.DataSource = null;
                ddlIssuer.DataBind();
                ddlIssuer.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }

        private void FISeries(int SeriesID)
        {
            DataSet dsScheme = fiorderBo.GetFISeries(SeriesID);
            if (dsScheme.Tables[0].Rows.Count > 0)
            {
                ddlSeries.DataSource = dsScheme;
                ddlSeries.DataValueField = dsScheme.Tables[0].Columns["PFISD_SeriesId"].ToString();
                ddlSeries.DataTextField = dsScheme.Tables[0].Columns["PFISD_SeriesName"].ToString();
                ddlSeries.DataBind();
                //ddlSeries.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {
                ddlSeries.Items.Clear();
                ddlSeries.DataSource = null;
                ddlSeries.DataBind();
                ddlSeries.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }

        protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        {

            DDLSchemeSelection();

        }

        private void DDLSchemeSelection()
        {
            if (ddlScheme.SelectedValue != "Select")
                FISeries(Convert.ToInt32(ddlScheme.SelectedValue));


            int minTenure = 0;
            int maxTenure = 0;
            if (ddlSeries.SelectedValue != "Select")
            {
                if (ddlSeries.SelectedValue != "")
                {
                    FISeriesDetails(Convert.ToInt32(ddlSeries.SelectedValue));


                    if (ddlSeries.SelectedValue != "0")
                        fiorderBo.GetTenure(Convert.ToInt32(ddlSeries.SelectedValue), out minTenure, out maxTenure);


                }
            }
            OnPayAmtTextchanged(this, null);
        }

        protected void ddlTranstype_SelectedIndexChanged(object sender, EventArgs e)
        {
            TransactionTypeChanges(ddlTranstype.SelectedValue);

        }

        private void TransactionTypeChanges(string transactionType)
        {
            if (transactionType == "Select")
            {
                trDepRen.Visible = false;
                //trPayAmt.Visible = false;
                txtPayAmt.Visible = false;
                // Label18.Visible = false;

            }
            else if (transactionType == "Renewal")
            {
                trDepRen.Visible = true;
                ////trPayAmt.Visible = false;
                txtPayAmt.Visible = false;
                // Label18.Visible = false;
            }
            else if (transactionType == "New")
            {
                txtPayAmt.Visible = true;
                // Label18.Visible = true;
                //trPayAmt.Visible = true;
                trDepRen.Visible = false;
            }
        }


        protected void ddlProofType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void BindddlProof(int proofTypeSelectedValue)
        {
            DataTable dtDpProofsForTypes = new DataTable();
            dtDpProofsForTypes = customerBo.GetCustomerProofsForTypes(proofTypeSelectedValue);
        }

        private void FICategory()
        {
            DataSet dsBankName = fiorderBo.GetFICategory();
            if (dsBankName.Tables[0].Rows.Count > 0)
            {

                ddlCategory.DataSource = dsBankName;
                ddlCategory.DataValueField = dsBankName.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlCategory.DataTextField = dsBankName.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));

            }
            else
            {
                ddlCategory.Items.Clear();
                ddlCategory.DataSource = null;
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }

        private void BindARNNo(int adviserId)
        {
            DataSet dsArnNo = mfOrderBo.GetARNNo(adviserId);
            if (dsArnNo.Tables[0].Rows.Count > 0)
            {
                ddlARNNo.DataSource = dsArnNo;
                ddlARNNo.DataValueField = dsArnNo.Tables[0].Columns["Identifier"].ToString();
                ddlARNNo.DataTextField = dsArnNo.Tables[0].Columns["Identifier"].ToString();
                ddlARNNo.DataBind();
            }
            ddlARNNo.Items.Insert(0, new ListItem("Select", "Select"));
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            bool result = false; ;
            var hdMax = (object)Cache["HiddenMaxid" + userVo.UserId.ToString()];
            var hdMin = (object)Cache["HiddenMinid" + userVo.UserId.ToString()];
            if ((Convert.ToDouble(txtQty.Text) <= Convert.ToDouble(hdMax)))
            {
                result = true;
            }
            if (result == true)
            {
                if ((Convert.ToDouble(txtQty.Text) >= Convert.ToDouble(hdMin)))
                {
                    result = true;

                }
                else
                {
                    result = false;
                }
            }

            if (string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please select Customer Name.');", true);
                return;
            }

            if (result == true)
            {
                hdnButtonAction.Value = "Update";
                //lnkBtnFIEdit.Visible = true;
                msgRecordStatus.Visible = true;
                GetFICOntrolsValues();
                btnUpdate.Visible = false;
                lnkBtnFIEdit.Visible = true;
                //BtnFileupload.Visible = true;

                SetFICOntrolsEnablity(false);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Order cannot be processed.Please enter quantity less than or equal to maximum quantity allowed for this issue')", true);
                return;
            }

        }


        protected void BtnFileupload_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(lblGetOrderNo.Text))
            {
                if (Convert.ToInt32(lblGetOrderNo.Text) > 0)
                {

                    //AddClick();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Create Order Details Then Try again')", true);
                return;
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = false;
            bool result = false;
            var hdMax = (object)Cache["HiddenMaxid" + userVo.UserId.ToString()];
            var hdMin = (object)Cache["HiddenMinid" + userVo.UserId.ToString()];
            if ((Convert.ToDouble(txtQty.Text) <= Convert.ToDouble(hdMax)))
            {
                result = true;
            }
            if (result == true)
            {
                if ((Convert.ToDouble(txtQty.Text) >= Convert.ToDouble(hdMin)))
                {
                    result = true;

                }
                else
                {
                    result = false;
                }
            }
            if (!String.IsNullOrEmpty(txtApplicationNumber.Text))
            {
                if (CheckApllicationNo() > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Application number already used.');", true);
                    btnSubmit.Enabled = true;

                    return;
                }
            }

            if (string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please select Customer Name.');", true);
                btnSubmit.Enabled = true;

                return;
            }
            if (string.IsNullOrEmpty(txtAgentId.Value.ToString().Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please select Agent Name.');", true);
                btnSubmit.Enabled = true;

                return;
            }
            if (result == true)
            {
                hdnButtonAction.Value = "Submit";
                orderVo.CustomerId = int.Parse(txtCustomerId.Value);
                GetFICOntrolsValues();
                SetFICOntrolsEnablity(false);
                btnSubmit.Visible = false;
                btnAddMore.Visible = false;
                lnkBtnFIEdit.Visible = true;
                btnSubmit.Enabled = true;
                tbUploadDocument.Visible = true;
                gvUploadDocument.Visible = true;
                //BtnFileupload.Visible = true;
                // btnUpdate.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Order cannot be processed.Please enter quantity less than or equal to maximum quantity allowed for this issue')", true);
                btnSubmit.Enabled = true;
                return;
            }


        }

        private void SetFICOntrolsEnablity(bool Val)
        {
            ddlsearch.Enabled = Val;
            ddlPaymentMode.Enabled = Val;

            txtPaymentNumber.Enabled = Val;
            txtPaymentInstDate.Enabled = Val;
            ddlBankName.Enabled = Val;
            txtAssociateSearch.Enabled = Val;
            ddlDepoBank.Enabled = Val;
            txtCustomerName.Enabled = Val;
            ddlBankName.Enabled = Val;


            ddlCategory.Enabled = Val;
            ddlIssuer.Enabled = Val;
            ddlScheme.Enabled = Val;
            ddlSeries.Enabled = Val;
            ddlTranstype.Enabled = Val;
            txtSeries.Enabled = Val;
            ddlSchemeOption.Enabled = Val;
            ddlFrequency.Enabled = Val;
            if (rbtnAuthentication.Checked || rbtnReject.Checked)
            {
                rbtnAuthentication.Enabled = false;
                rbtnReject.Enabled = false;
            }
            else
            {
                rbtnAuthentication.Enabled = Val;
                rbtnReject.Enabled = Val;
            }
            //if (rbtnReject.Checked)
            //{
            //    rbtnReject.Enabled = false;
            //    rbtnAuthentication.Enabled = false;
            //}
            //else
            //{
            //    rbtnReject.Enabled = Val;
            //}
            //ddlModeofHOlding.Enabled = Val;

            lblGetOrderNo.Enabled = Val;
            //txtOrderDate.Enabled = Val;
            txtApplicationNumber.Enabled = Val;
            txtApplicationDate.Enabled = Val;
            txtExistDepositreceiptno.Enabled = Val;
            txtRenAmt.Enabled = Val;
            txtMaturDate.Enabled = Val;
            txtMatAmt.Enabled = Val;
            txtPayAmt.Enabled = Val;
            txtQty.Enabled = Val;
            TxtPurAmt.Enabled = Val;
            txtMatAmt.Enabled = Val;
            txtPayAmt.Enabled = Val;

        }

        protected void lnkBtnFIEdit_Click(object sender, EventArgs e)
        {
            lnkBtnFIEdit.Visible = false;
            lnkBtnEdit();
            msgRecordStatus.Visible = false;

        }

        protected void lnkBtnEdit()
        {
            //lnkBtnFIEdit.Visible = true;
            btnUpdate.Visible = true;
            ViewForm = "Edit";
            SetFICOntrolsEnablity(true);
            btnUpdate.Visible = true;
            btnSubmit.Visible = false;
        }
        protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaymentMode(ddlPaymentMode.SelectedValue);
        }

        private void PaymentMode(string type)
        {
            if (type == "CQ" || type == "DF")
            {
                trPINo.Visible = true;
                txtPaymentInstDate.MaxDate = txtOrderDate.MaxDate;
                RadDateControlBusinessDateValidation(ref txtPaymentInstDate, DateTime.Now);
            }
            else
            {
                trPINo.Visible = false;
            }
        }
        private void GetFICOntrolsValues()
        {
            int i = 0;


            orderVo.CustomerId = int.Parse(txtCustomerId.Value);

            orderVo.AssetGroup = "FI";


            //if (!String.IsNullOrEmpty(txtAssociateSearch.Text))
            //    AgentId = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
            //if (AgentId.Rows.Count > 0)
            //{
            //    fiorderVo.AgentId = int.Parse(AgentId.Rows[0][1].ToString());
            //}
            //else
            //    fiorderVo.AgentId = 0;



            fiorderVo.AgentId = int.Parse(txtAgentId.Value);

            if (!string.IsNullOrEmpty(lblGetOrderNo.Text))
                orderVo.OrderNumber = Convert.ToInt32(lblGetOrderNo.Text);
            if (ChkSeniorcitizens.Checked == true)
            {
                fiorderVo.Privilidge = "Seniorcitizens";
            }
            else if (ChkWidow.Checked == true)
            {
                fiorderVo.Privilidge = "Widow";
            }
            else if (ChkArmedForcePersonnel.Checked == true)
            {
                fiorderVo.Privilidge = "ArmedForcePersonnel";
            }
            else if (CHKExistingrelationship.Checked == true)
            {
                fiorderVo.Privilidge = "Existingrelationship";
            }

            if (ChkFirstholder.Checked == true)
            {
                fiorderVo.Depositpayableto = "Firstholder";
            }
            else if (ChkEORS.Checked == true)
            {
                fiorderVo.Depositpayableto = "Either or survivor";
            }

            orderVo.OrderDate = Convert.ToDateTime(txtOrderDate.SelectedDate);
            orderVo.ApplicationReceivedDate = Convert.ToDateTime(txtApplicationDate.FocusedDate);
            orderVo.ApplicationNumber = txtApplicationNumber.Text;
            fiorderVo.AssetInstrumentCategory = ddlCategory.SelectedValue;

            fiorderVo.SeriesDetails = txtSeries.Text;
            fiorderVo.TransactionType = ddlTranstype.SelectedValue;
            fiorderVo.AssetInstrumentCategoryCode = ddlCategory.SelectedValue;
            fiorderVo.IssuerId = ddlIssuer.SelectedValue;
            fiorderVo.ExisitingDepositreceiptno = txtExistDepositreceiptno.Text;
            if (!string.IsNullOrEmpty(txtRenAmt.Text))
                fiorderVo.RenewalAmount = Convert.ToDouble(txtRenAmt.Text);
            else
                fiorderVo.RenewalAmount = 0;

            fiorderVo.MaturityDate = Convert.ToDateTime(txtMaturDate.FocusedDate);

            if (!string.IsNullOrEmpty(txtMatAmt.Text))
                fiorderVo.MaturityAmount = Convert.ToDouble(txtQty.Text);
            else
                fiorderVo.MaturityAmount = 0;

            fiorderVo.SchemeId = Convert.ToInt32(ddlScheme.SelectedValue);
            fiorderVo.SeriesId = Convert.ToInt32(ddlSeries.SelectedValue);
            if (!string.IsNullOrEmpty(TxtPurAmt.Text))
                fiorderVo.AmountPayable = Convert.ToDouble(TxtPurAmt.Text);
            else
                fiorderVo.AmountPayable = 0;

            fiorderVo.ModeOfHolding = "0"; //ddlModeofHOlding.SelectedValue;
            fiorderVo.Schemeoption = ddlSchemeOption.SelectedValue;
            if (!string.IsNullOrEmpty(txtExistDepositreceiptno.Text))
                fiorderVo.ExisitingDepositreceiptno = txtExistDepositreceiptno.Text;
            else
                fiorderVo.ExisitingDepositreceiptno = "";

            fiorderVo.Frequency = ddlFrequency.SelectedValue;
            fiorderVo.Qty = Convert.ToDouble(txtQty.Text);

            //if (ddlPaymentMode.SelectedIndex != 0)
            orderVo.PaymentMode = ddlPaymentMode.SelectedValue;
            //else
            //    orderVo.PaymentMode = "ES";

            if (!string.IsNullOrEmpty(txtPaymentNumber.Text.ToString().Trim()))
                orderVo.ChequeNumber = txtPaymentNumber.Text;
            else
                orderVo.ChequeNumber = "";
            if (!string.IsNullOrEmpty(txtPaymentInstDate.SelectedDate.ToString().Trim()))
                orderVo.PaymentDate = DateTime.Parse(txtPaymentInstDate.SelectedDate.ToString());
            else
                orderVo.PaymentDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
            {
                if (ddlBankName.SelectedValue != "Select")
                    orderVo.CustBankAccId = int.Parse(ddlBankName.SelectedValue);
                else
                    orderVo.CustBankAccId = 0;
            }
            else
                orderVo.CustBankAccId = 0;


            if (!string.IsNullOrEmpty(txtBranchName.Text))
                orderVo.BankBranchName = txtBranchName.Text;
            else
                orderVo.BankBranchName = "";


            int dematAccountId = 0;
            foreach (GridDataItem gvr in gvDematDetailsTeleR.MasterTableView.Items)
            {
                if (((CheckBox)gvr.FindControl("chkDematId")).Checked == true)
                {
                    dematAccountId = int.Parse(gvDematDetailsTeleR.MasterTableView.DataKeyValues[gvr.ItemIndex]["CEDA_DematAccountId"].ToString());
                    fiorderVo.DematAccountId = dematAccountId;

                    break;
                }

            }

            List<int> OrderIds = new List<int>();
            if (hdnButtonAction.Value == "Submit")
            {

                OrderIds = fiorderBo.CreateOrderFIDetails(orderVo, fiorderVo, userVo.UserId, "Submit");
                orderId = int.Parse(OrderIds[0].ToString());
                lblGetOrderNo.Text = orderId.ToString();
                lblOrderNumber.Text = "Order No.";
                ViewState["orderno"] = orderId;

                ShowMessage("Order placed successfully, Order reference no. is " + lblGetOrderNo.Text);
                //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Your order added successfully.');", true);
                tbUploadDocument.Visible = true;
                BindDocument(orderId);

            }
            else
            {
                //if (chkAuthentication.Checked == true)
                //    fiorderVo.authenticId = 1;
                bool lbResult = false;
                orderId = orderVo.OrderNumber;

                if (rbtnAuthentication.Checked == true || rbtnReject.Checked == true)
                {
                    lbResult = OfflineBondOrderBo.CancelBondsFDBookOrder(orderId, txtRejectReseaon.Text, userVo.UserId, (rbtnReject.Checked) ? false : true);
                }

                OrderIds = fiorderBo.CreateOrderFIDetails(orderVo, fiorderVo, userVo.UserId, "Update");
                orderId = fiorderVo.OrderNumber;
                BindDocument(orderId);
                tbUploadDocument.Visible = true;
                lblOrderNumber.Text = "Order No.";

                ShowMessage("Order updated successfully, Order reference no. is " + lblGetOrderNo.Text);

                //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Your order updated successfully.');", true);
                tbUploadDocument.Visible = true;
            }


            Session["CO_OrderId"] = orderId;
            orderVo.OrderId = orderId;
            int rowNodt = 0;
            DataTable dtJntHld = new DataTable();
            DataTable dtNominee = new DataTable();
            dtJntHld.Columns.Add("AssociateId");
            dtJntHld.Columns.Add("AssociateType");
            if (gvAssociate.MasterTableView.Items.Count > 0)
            {
                foreach (GridDataItem gvr in gvAssociate.Items)
                {

                    dtJntHld.Rows.Add();
                    dtJntHld.Rows[rowNodt]["AssociateId"] = int.Parse(gvAssociate.MasterTableView.DataKeyValues[gvr.ItemIndex]["CDAA_Id"].ToString());
                    dtJntHld.Rows[rowNodt]["AssociateType"] = gvAssociate.MasterTableView.DataKeyValues[gvr.ItemIndex]["AssociateType"].ToString();
                    rowNodt++;




                }
            }

            if (dtJntHld.Rows.Count > 0)
            {
                OfflineBondOrderBo offlineBondBo = new OfflineBondOrderBo();

                offlineBondBo.CreateOfflineCustomerOrderAssociation(dtJntHld, userVo.UserId, orderId);
            }


        }

        private int CheckApllicationNo()
        {
            int isUniqueApplicationNo = 0;

            if (!string.IsNullOrEmpty(ddlScheme.SelectedValue))
            {

                isUniqueApplicationNo = fiorderBo.CheckApplicationNo(Convert.ToInt32(ddlScheme.SelectedValue), txtApplicationNumber.Text, ddlCategory.SelectedValue);
            }
            return isUniqueApplicationNo;
        }

        private Int64 GetFaceValue()
        {
            Int64 faceValue = 0;
            if (!string.IsNullOrEmpty(ddlScheme.SelectedValue))
            {

                faceValue = fiorderBo.GetFaceValue(Convert.ToInt32(ddlScheme.SelectedValue));
            }
            return faceValue;
        }

        protected void OnAmtchanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtPurAmt.Text))
            {
                if (Convert.ToInt32(TxtPurAmt.Text) <= (Convert.ToInt32(lblMaxQuentity.Text) * GetFaceValue()))
                {
                    txtQty.Text = string.Empty;
                    txtQty.Text = (Convert.ToInt32(TxtPurAmt.Text) / GetFaceValue()).ToString();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Purchase Amount Should be less than  Multiple of Face Value and Max.Qty!');", true);
                    txtQty.Text = string.Empty;
                    TxtPurAmt.Text = string.Empty;
                }
            }
        }

        protected void OnQtychanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQty.Text))
            {
                if ((Convert.ToInt32(txtQty.Text) <= Convert.ToInt32(lblMaxQuentity.Text)) && (Convert.ToInt32(txtQty.Text) >= Convert.ToInt32(lblMinQuentity.Text)))
                {
                    TxtPurAmt.Text = string.Empty;
                    TxtPurAmt.Text = (Convert.ToInt32(txtQty.Text) * GetFaceValue()).ToString();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Quantity Should be less than Max.Qty And greater than Min.Qty!');", true);
                    TxtPurAmt.Text = string.Empty;
                }
            }

        }

        private void AddClick(RadUpload radUploadProof, DropDownList ddlProofType)
        {
            bool blResult = false;
            bool blZeroBalance = false;
            bool blFileSizeExceeded = false;
            //  AddFile(out blZeroBalance, out blFileSizeExceeded);

            if (fStorageBalance > 0)
                blResult = AddFile(out blZeroBalance, out blFileSizeExceeded, radUploadProof, ddlProofType);
            else
                blZeroBalance = true;


            if (blZeroBalance)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('You do not have enough space. You have only " + fStorageBalance + " MB left in your account!');", true);
            else
            {
                if (blResult)
                {
                    // ResetControls();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Repository Item added successfully!')", true);

                }
                else
                {
                    if (blFileSizeExceeded)
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Sorry your file size exceeds the allowable 2 MB limit!');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Error adding repository item!');", true);
                }
            }
        }

        private bool AddFile(out bool blZeroBalance, out bool blFileSizeExceeded, RadUpload radUploadProof, DropDownList ddlProofType)
        {
            // If the upload type is file
            // We need to see if the adviser has a folder in Repository folder
            // Case 1: If not, then encode the adviser id and create a folder with the encoded id
            // then create a folder for the repository category within the encoded folder
            // then store the encoded adviserID + GUID + file name
            // Case 2: If folder exists, check if the category folder exists. 
            // If not then, create a folder with the category code and store the file as done above.
            // If yes, then just store the file as done above.
            // Once this is done, store the info in the DB with the file path.
            strRepositoryPath = string.Empty;
            strRepositoryPath = Server.MapPath(strRepositoryPath) + "\\advisor_" + advisorVo.advisorId + "\\54EC_OrderDocuments";
            //File.Delete(filePath);
            AdvisorBo advBo = new AdvisorBo();
            repoBo = new RepositoryBo();
            bool blResult = false;
            blZeroBalance = false;
            blFileSizeExceeded = false;

            try
            {
                // Reading File Upload Control
                if (radUploadProof.UploadedFiles.Count != 0)
                {
                    // Put this part under a transaction scope
                    //using (TransactionScope scope1 = new TransactionScope())
                    //{
                    UploadedFile file = radUploadProof.UploadedFiles[0];
                    float fileSize = float.Parse(file.ContentLength.ToString()) / 1048576; // Converting bytes to MB

                    // If space is there to upload file
                    if (fStorageBalance >= fileSize)
                    {
                        if (fileSize <= 10)   // If upload file size is less than 10 MB then upload
                        {
                            // Check if directory for advisor exists, and if not then create a new directoty
                            if (!Directory.Exists(strRepositoryPath))
                            {
                                Directory.CreateDirectory(strRepositoryPath);
                            }
                            strGuid = Guid.NewGuid().ToString();
                            string newFileName = SaveFileIntoServer(file, strGuid, strRepositoryPath);
                            repoVo = new RepositoryVo();
                            repoVo.AdviserId = advisorVo.advisorId;
                            repoVo.CategoryCode = ddlProofType.SelectedValue;
                            repoVo.Description = newFileName;

                            blResult = repoBo.AddOrderDocument(repoVo, Convert.ToInt32(lblGetOrderNo.Text));

                            if (blResult)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Document Uploaded Successfully')", true);

                                // Once the adding of repository is a success, then update the balance storage in advisor subscription table
                                fStorageBalance = UpdateAdvisorStorageBalance(fileSize, 0, fStorageBalance);
                            }
                        }
                        else
                        {
                            blFileSizeExceeded = true;
                        }
                    }
                    else
                    {
                        blZeroBalance = true;
                    }

                    // scope1.Complete();   // Commit the transaction scope if no errors
                    //}
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please select a file to upload!');", true);
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return blResult;
        }

        private float UpdateAdvisorStorageBalance(float fileSize, float oldFileSize, float fStorageBalance)
        {
            AdvisorBo advBo = new AdvisorBo();
            fStorageBalance -= (fileSize - oldFileSize);
            advBo.UpdateAdviserStorageBalance(advisorVo.advisorId, fStorageBalance);
            advisorVo.SubscriptionVo.StorageBalance = fStorageBalance;
            Session[SessionContents.AdvisorVo] = advisorVo;
            return fStorageBalance;
        }

        private string SaveFileIntoServer(UploadedFile file, string strGuid, string strPath)
        {
            fileExtension = file.GetExtension();
            Random ranNo = new Random();
            int ranDomNo = ranNo.Next();
            string filename = file.FileName.Substring(0, file.FileName.IndexOf('.'));
            //string strRenameFilename = file.GetName();
            //strRenameFilename = strRenameFilename.Replace(' ', '_');
            //string newFileName = advisorVo.advisorId + "_" + strGuid + "_" + strRenameFilename;
            string newFileName = ranDomNo+"_"+ filename + fileExtension; ;
            // Save adviser repository file in the path

            file.SaveAs(strPath + "\\" + newFileName);
            return newFileName;
        }

        protected void ddlSchemeOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlFrequency.SelectedIndex = 3;

            //if (ddlSchemeOption.SelectedValue == "NonCummulative")
            //{
            //    ddlFrequency.Enabled = true;

            //}
            //else
            //{
            //    ddlFrequency.Enabled = false;

            //}
            //OnPayAmtTextchanged(this, null);
        }

        protected void ddlSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            int minTenure = 0;
            int maxTenure = 0;

            if (ddlSeries.SelectedIndex > 0)
            {
                FISeriesDetails(Convert.ToInt32(ddlSeries.SelectedValue));


                if (ddlSeries.SelectedValue != "0")
                    fiorderBo.GetTenure(Convert.ToInt32(ddlSeries.SelectedValue), out minTenure, out maxTenure);

                //hdnMintenure.Value = minTenure.ToString();
                //hdnMaxtenure.Value = maxTenure.ToString();
            }
            OnPayAmtTextchanged(this, null);
        }

        private void FISeriesDetails(int SeriesID)
        {
            DataSet dsScheme = fiorderBo.GetFISeriesDetailssDetails(SeriesID);
            DataTable dtSeriesDetails = dsScheme.Tables[0];

            if (dtSeriesDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSeriesDetails.Rows)
                {
                    lblTenureRate.Text = dr["PFISD_Tenure"].ToString();
                    //hdnSeriesDetails.Value = dr["SeriesDetails"].ToString();
                    hdnMinQty.Value = dr["MinQty"].ToString();
                    hdnMaxQty.Value = dr["MaxQty"].ToString();
                    lblCoupenRate.Text = dr["CouponRate"].ToString();
                    lblMinQuentity.Text = dr["MinQty"].ToString();
                    lblMaxQuentity.Text = dr["MaxQty"].ToString();
                    lblFaceValue.Text = dr["AID_SeriesFaceValue"].ToString();
                    if (Cache["HiddenMaxid" + userVo.UserId.ToString()] == null)
                    {
                        Cache.Insert("HiddenMaxid" + userVo.UserId.ToString(), hdnMaxQty.Value);
                    }
                    else
                    {
                        Cache.Remove("HiddenMaxid" + userVo.UserId.ToString());
                        Cache.Insert("HiddenMaxid" + userVo.UserId.ToString(), hdnMaxQty.Value);
                    }
                    if (Cache["HiddenMinid" + userVo.UserId.ToString()] == null)
                    {
                        Cache.Insert("HiddenMinid" + userVo.UserId.ToString(), hdnMinQty.Value);
                    }
                    else
                    {
                        Cache.Remove("HiddenMinid" + userVo.UserId.ToString());
                        Cache.Insert("HiddenMinid" + userVo.UserId.ToString(), hdnMinQty.Value);
                    }
                    ////CouponType = dr["PFISD_CouponType"].ToString();
                    ////txtSeries.Text = "Tenure-" + Tenure + "/" + "InterestRate-" + hdnDefaulteInteresRate.Value + "/" + "InterestType-" + CouponType;
                    //Label12.Text = hdnSeriesDetails.Value;

                }



            }


        }

        protected void ddlIssuer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssuer.SelectedIndex != 0)
                FIScheme(advisorVo.advisorId, ddlIssuer.SelectedValue);
        }

        protected void OnPayAmtTextchanged(object sender, EventArgs e)
        {



        }



        protected void lnkBtnDemat_onClick(object sender, EventArgs e)
        {

            rwDematDetails.VisibleOnPageLoad = true;

            GetDematAccountDetails(Convert.ToInt32(txtCustomerId.Value));

        }

        protected void btnAddDemat_Click(object sender, EventArgs e)
        {
            int dematAccountId = 0;
            foreach (GridDataItem gvr in gvDematDetailsTeleR.MasterTableView.Items)
            {
                if (((CheckBox)gvr.FindControl("chkDematId")).Checked == true)
                {
                    dematAccountId = int.Parse(gvDematDetailsTeleR.MasterTableView.DataKeyValues[gvr.ItemIndex]["CEDA_DematAccountId"].ToString());
                    txtDematid.Text = gvDematDetailsTeleR.MasterTableView.DataKeyValues[gvr.ItemIndex]["CEDA_DPClientId"].ToString();
                    rwDematDetails.VisibleOnPageLoad = false;

                    break;
                }
            }
            BindgvFamilyAssociate(dematAccountId);

        }


        protected void btnClose_Click(object sender, EventArgs e)
        {
            rwDematDetails.VisibleOnPageLoad = false;

        }

        private void BindgvFamilyAssociate(int demataccountid)
        {
            gvAssociate.Visible = true;
            DataSet dsAssociate = boDematAccount.GetCustomerDematAccountAssociates(demataccountid);
            gvAssociate.DataSource = dsAssociate;
            gvAssociate.DataBind();

            if (gvAssociate.Items.Count > 0)
            {
                pnlJointHolderNominee.Visible = true;
            }
            else
            {
                pnlJointHolderNominee.Visible = false;

            }

            if (Cache["gvAssociate" + userVo.UserId] == null)
            {
                Cache.Insert("gvAssociate" + userVo.UserId, dsAssociate);
            }
            else
            {
                Cache.Remove("gvAssociate" + userVo.UserId);
                Cache.Insert("gvAssociate" + userVo.UserId, dsAssociate);
            }
        }

        private void GetDematAccountDetails(int customerId)
        {
            try
            {
                DataSet dsDematDetails = boDematAccount.GetDematAccountHolderDetails(customerId);
                gvDematDetailsTeleR.Visible = true;
                gvDematDetailsTeleR.DataSource = dsDematDetails.Tables[0];
                gvDematDetailsTeleR.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FIScheme(int AdviserId, string category)
        {
            DataSet dsScheme = new DataSet();
            ddlScheme.DataSource = dsScheme;
            Label12.Text = string.Empty;
            dsScheme = fiorderBo.GetFIScheme(AdviserId, category);
            if (dsScheme.Tables[0].Rows.Count > 0)
            {
                ddlScheme.DataSource = dsScheme;
                ddlScheme.DataValueField = dsScheme.Tables[0].Columns["PFISM_SchemeId"].ToString();
                ddlScheme.DataTextField = dsScheme.Tables[0].Columns["PFISM_SchemeName"].ToString();
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {
                ddlScheme.Items.Clear();
                ddlScheme.DataSource = null;
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }

        //private void SaveOrderDetails()
        //{
        //    orderVo.CustomerId = int.Parse(txtCustomerId.Value);
        //    if (  DdlLoad.SelectedIndex  == 1)
        //    {
        //        orderVo.AssetGroup = "MF";
        //        GetMFCOntrols();
        //    }
        //    else if (  DdlLoad.SelectedIndex  == 2)
        //    {
        //        orderVo.AssetGroup = "FI";
        //    }
        //    GetFICOntrolsValues();

        //    if (!string.IsNullOrEmpty((txtAmount.Text).ToString().Trim()))
        //        mforderVo.Amount = double.Parse(txtAmount.Text);
        //    else
        //        mforderVo.Amount = 0;

        //    if (TransType == "Sel" || TransType == "STB" || TransType == "SWP" || TransType == "SWB")
        //    {
        //        if (rbtAmount.Checked == true)
        //        {
        //            if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
        //                mforderVo.Amount = double.Parse(txtNewAmount.Text);
        //            else
        //                mforderVo.Amount = 0;
        //        }
        //        if (rbtUnit.Checked == true)
        //        {
        //            if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
        //                mforderVo.Units = double.Parse(txtNewAmount.Text);
        //            else
        //                mforderVo.Units = 0;
        //        }

        //    }
        //    orderVo.PaymentMode = ddlPaymentMode.SelectedValue;

        //    if (!string.IsNullOrEmpty(txtPaymentNumber.Text.ToString().Trim()))
        //        orderVo.ChequeNumber = txtPaymentNumber.Text;
        //    else
        //        orderVo.ChequeNumber = "";
        //    if (!string.IsNullOrEmpty(txtPaymentInstDate.SelectedDate.ToString().Trim()))
        //        orderVo.PaymentDate = DateTime.Parse(txtPaymentInstDate.SelectedDate.ToString());
        //    else
        //        orderVo.PaymentDate = DateTime.MinValue;
        //    if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
        //    {
        //        if (ddlBankName.SelectedValue != "Select")
        //            orderVo.CustBankAccId = int.Parse(ddlBankName.SelectedValue);
        //        else
        //            orderVo.CustBankAccId = 0;
        //    }
        //    else
        //        orderVo.CustBankAccId = 0;
        //    if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
        //    {
        //        if (ddlBankName.SelectedValue != "Select")
        //            mforderVo.BankName = ddlBankName.SelectedItem.Text;
        //        else
        //            mforderVo.BankName = "";
        //    }
        //    else
        //        mforderVo.BankName = "";
        //    if (!string.IsNullOrEmpty(txtBranchName.Text.ToString().Trim()))
        //        mforderVo.BranchName = txtBranchName.Text;
        //    else
        //        mforderVo.BranchName = "";
        //    if (ddlSchemeSwitch.SelectedValue != "")
        //    {
        //        if (ddlSchemeSwitch.SelectedIndex != 0)
        //            mforderVo.SchemePlanSwitch = int.Parse(ddlSchemeSwitch.SelectedValue);
        //    }
        //    if (!string.IsNullOrEmpty(txtCorrAdrLine1.Text.ToString().Trim()))
        //        mforderVo.AddrLine1 = txtCorrAdrLine1.Text;
        //    else
        //        mforderVo.AddrLine1 = "";
        //    if (txtCorrAdrLine2.Text != "" || txtCorrAdrLine2.Text != null)
        //        mforderVo.AddrLine2 = txtCorrAdrLine2.Text;
        //    else
        //        mforderVo.AddrLine2 = "";
        //    if (!string.IsNullOrEmpty(txtCorrAdrLine3.Text.ToString().Trim()))
        //        mforderVo.AddrLine3 = txtCorrAdrLine3.Text;
        //    else
        //        mforderVo.AddrLine3 = "";
        //    if (!string.IsNullOrEmpty(txtLivingSince.SelectedDate.ToString().Trim()))
        //        mforderVo.LivingSince = DateTime.Parse(txtLivingSince.SelectedDate.ToString());
        //    else
        //        mforderVo.LivingSince = DateTime.MinValue;
        //    if (!string.IsNullOrEmpty(txtCorrAdrCity.Text.ToString().Trim()))
        //        mforderVo.City = txtCorrAdrCity.Text;
        //    else
        //        mforderVo.City = "";
        //    if (ddlCorrAdrState.SelectedIndex > 0)  //!= 0)
        //        mforderVo.State = ddlCorrAdrState.SelectedItem.Text;
        //    else
        //        mforderVo.State = "";
        //    if (!string.IsNullOrEmpty(txtCorrAdrPinCode.Text.ToString().Trim()))
        //        mforderVo.Pincode = txtCorrAdrPinCode.Text;
        //    else
        //        mforderVo.Pincode = "";
        //    mforderVo.Country = ddlCorrAdrCountry.SelectedValue;
        //    if (TransType == "SIP")
        //    {
        //        if (!string.IsNullOrEmpty((ddlFrequencySIP.SelectedValue).ToString().Trim()))
        //            mforderVo.FrequencyCode = ddlFrequencySIP.SelectedValue;
        //        if (!string.IsNullOrEmpty((txtstartDateSIP.SelectedDate).ToString().Trim()))
        //            mforderVo.StartDate = DateTime.Parse(txtstartDateSIP.SelectedDate.ToString());
        //        else
        //            mforderVo.StartDate = DateTime.MinValue;
        //        if (!string.IsNullOrEmpty((txtendDateSIP.SelectedDate).ToString().Trim()))
        //            mforderVo.EndDate = DateTime.Parse(txtendDateSIP.SelectedDate.ToString());
        //        else
        //            mforderVo.EndDate = DateTime.MinValue;
        //    }
        //    else if (TransType == "STB" || TransType == "SWP")
        //    {
        //        if (!string.IsNullOrEmpty((ddlFrequencySTP.SelectedValue).ToString().Trim()))
        //            mforderVo.FrequencyCode = ddlFrequencySTP.SelectedValue;
        //        if (!string.IsNullOrEmpty((txtstartDateSTP.SelectedDate).ToString().Trim()))
        //            mforderVo.StartDate = DateTime.Parse(txtstartDateSTP.SelectedDate.ToString());
        //        else
        //            mforderVo.StartDate = DateTime.MinValue;
        //        if (!string.IsNullOrEmpty((txtendDateSTP.SelectedDate).ToString().Trim()))
        //            mforderVo.EndDate = DateTime.Parse(txtendDateSTP.SelectedDate.ToString());
        //        else
        //            mforderVo.EndDate = DateTime.MinValue;
        //    }
        //    if (ddlARNNo.SelectedIndex != 0)
        //        mforderVo.ARNNo = ddlARNNo.SelectedItem.Text;
        //    if (!String.IsNullOrEmpty(txtAssociateSearch.Text))
        //        AgentId = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
        //    if (AgentId.Rows.Count > 0)
        //    {
        //        mforderVo.AgentId = int.Parse(AgentId.Rows[0][1].ToString());
        //    }
        //    else
        //        mforderVo.AgentId = 0;
        //    //Convert.ToInt32(ddlAssociate.SelectedValue);

        //    Session["orderVo"] = orderVo;
        //    Session["mforderVo"] = mforderVo;

        //}
        //protected void lnkBtnEdit_Click(object sender, EventArgs e)
        //{

        //}

        ////private void SaveOrderDetails()
        ////{
        ////    orderVo.CustomerId = int.Parse(txtCustomerId.Value);
        ////    orderVo.AssetGroup = "MF";
        ////    mforderVo.CustomerName = txtCustomerName.Text;
        ////    mforderVo.BMName = lblGetBranch.Text;
        ////    mforderVo.RMName = lblGetRM.Text;
        ////    mforderVo.PanNo = lblgetPan.Text;

        ////    mforderVo.TransactionCode = TransType;
        ////    if (!string.IsNullOrEmpty(txtReceivedDate.SelectedDate.ToString().Trim()))
        ////    {
        ////        orderVo.ApplicationReceivedDate = DateTime.Parse(txtReceivedDate.SelectedDate.ToString());
        ////    }
        ////    else
        ////        orderVo.ApplicationReceivedDate = DateTime.MinValue;
        ////    orderVo.ApplicationNumber = txtApplicationNumber.Text;
        ////    if (ddlAMCList.SelectedIndex != 0)
        ////        mforderVo.Amccode = int.Parse(ddlAMCList.SelectedValue);
        ////    else
        ////        mforderVo.Amccode = 0;
        ////    mforderVo.category = ddlCategory.SelectedValue;
        ////    if (//sai      ddlAmcSchemeList.SelectedIndex != 0)
        ////        mforderVo.SchemePlanCode = int.Parse(//sai      ddlAmcSchemeList.SelectedValue);
        ////    else
        ////        mforderVo.SchemePlanCode = 0;
        ////    mforderVo.portfolioId = int.Parse(ddlPortfolio.SelectedValue);
        ////    if (ddlFolioNumber.SelectedIndex != -1)
        ////        mforderVo.accountid = int.Parse(ddlFolioNumber.SelectedValue);
        ////    else
        ////        mforderVo.accountid = 0;
        ////    orderVo.OrderDate = Convert.ToDateTime(//sai  txtOrderDate.SelectedDate);
        ////    mforderVo.OrderNumber = int.Parse(//sai  lblGetOrderNo.Text);
        ////    //orderVo.OrderStatusCode = ddlOrderStatus.SelectedValue;
        ////    //orderVo.ReasonCode = ddlOrderPendingReason.SelectedValue;
        ////    if (rbtnImmediate.Checked == true)
        ////        mforderVo.IsImmediate = 1;
        ////    else
        ////        mforderVo.IsImmediate = 0;
        ////    if (!string.IsNullOrEmpty((//sai txtFutureDate.SelectedDate).ToString().Trim()))
        ////        mforderVo.FutureExecutionDate = DateTime.Parse(//sai txtFutureDate.SelectedDate.ToString());
        ////    else
        ////        mforderVo.FutureExecutionDate = DateTime.MinValue;
        ////    if (!string.IsNullOrEmpty((//sai txtFutureTrigger.Text).ToString().Trim()))
        ////        mforderVo.FutureTriggerCondition = //sai txtFutureTrigger.Text;
        ////    else
        ////        mforderVo.FutureTriggerCondition = "";
        ////    if (!string.IsNullOrEmpty((txtAmount.Text).ToString().Trim()))
        ////        mforderVo.Amount = double.Parse(txtAmount.Text);
        ////    else
        ////        mforderVo.Amount = 0;
        ////    //if (!string.IsNullOrEmpty((lblGetAvailableUnits.Text).ToString().Trim()))
        ////    //    mforderVo.Units = double.Parse(lblGetAvailableUnits.Text);
        ////    //else
        ////    //    mforderVo.Units = 0;
        ////    if (TransType == "Sel" || TransType == "STB" || TransType == "SWP" || TransType == "SWB")
        ////    {
        ////        if (rbtAmount.Checked == true)
        ////        {
        ////            if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
        ////                mforderVo.Amount = double.Parse(txtNewAmount.Text);
        ////            else
        ////                mforderVo.Amount = 0;
        ////        }
        ////        if (rbtUnit.Checked == true)
        ////        {
        ////            if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
        ////                mforderVo.Units = double.Parse(txtNewAmount.Text);
        ////            else
        ////                mforderVo.Units = 0;
        ////        }

        ////    }
        ////    orderVo.PaymentMode = ddlPaymentMode.SelectedValue;
        ////    if (!string.IsNullOrEmpty(txtPaymentNumber.Text.ToString().Trim()))
        ////        orderVo.ChequeNumber = txtPaymentNumber.Text;
        ////    else
        ////        orderVo.ChequeNumber = "";
        ////    if (!string.IsNullOrEmpty(txtPaymentInstDate.SelectedDate.ToString().Trim()))
        ////        orderVo.PaymentDate = DateTime.Parse(txtPaymentInstDate.SelectedDate.ToString());
        ////    else
        ////        orderVo.PaymentDate = DateTime.MinValue;
        ////    if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
        ////    {
        ////        if (ddlBankName.SelectedValue != "Select")
        ////            orderVo.CustBankAccId = int.Parse(ddlBankName.SelectedValue);
        ////        else
        ////            orderVo.CustBankAccId = 0;
        ////    }
        ////    else
        ////        orderVo.CustBankAccId = 0;
        ////    if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
        ////    {
        ////        if (ddlBankName.SelectedValue != "Select")
        ////            mforderVo.BankName = ddlBankName.SelectedItem.Text;
        ////        else
        ////            mforderVo.BankName = "";
        ////    }
        ////    else
        ////        mforderVo.BankName = "";
        ////    if (!string.IsNullOrEmpty(txtBranchName.Text.ToString().Trim()))
        ////        mforderVo.BranchName = txtBranchName.Text;
        ////    else
        ////        mforderVo.BranchName = "";
        ////    if (ddlSchemeSwitch.SelectedValue != "")
        ////    {
        ////        if (ddlSchemeSwitch.SelectedIndex != 0)
        ////            mforderVo.SchemePlanSwitch = int.Parse(ddlSchemeSwitch.SelectedValue);
        ////    }
        ////    if (!string.IsNullOrEmpty(txtCorrAdrLine1.Text.ToString().Trim()))
        ////        mforderVo.AddrLine1 = txtCorrAdrLine1.Text;
        ////    else
        ////        mforderVo.AddrLine1 = "";
        ////    if (txtCorrAdrLine2.Text != "" || txtCorrAdrLine2.Text != null)
        ////        mforderVo.AddrLine2 = txtCorrAdrLine2.Text;
        ////    else
        ////        mforderVo.AddrLine2 = "";
        ////    if (!string.IsNullOrEmpty(txtCorrAdrLine3.Text.ToString().Trim()))
        ////        mforderVo.AddrLine3 = txtCorrAdrLine3.Text;
        ////    else
        ////        mforderVo.AddrLine3 = "";
        ////    if (!string.IsNullOrEmpty(txtLivingSince.SelectedDate.ToString().Trim()))
        ////        mforderVo.LivingSince = DateTime.Parse(txtLivingSince.SelectedDate.ToString());
        ////    else
        ////        mforderVo.LivingSince = DateTime.MinValue;
        ////    if (!string.IsNullOrEmpty(txtCorrAdrCity.Text.ToString().Trim()))
        ////        mforderVo.City = txtCorrAdrCity.Text;
        ////    else
        ////        mforderVo.City = "";
        ////    if (ddlCorrAdrState.SelectedIndex != 0)
        ////        mforderVo.State = ddlCorrAdrState.SelectedItem.Text;
        ////    else
        ////        mforderVo.State = "";
        ////    if (!string.IsNullOrEmpty(txtCorrAdrPinCode.Text.ToString().Trim()))
        ////        mforderVo.Pincode = txtCorrAdrPinCode.Text;
        ////    else
        ////        mforderVo.Pincode = "";
        ////    mforderVo.Country = ddlCorrAdrCountry.SelectedValue;
        ////    if (TransType == "SIP")
        ////    {
        ////        if (!string.IsNullOrEmpty((ddlFrequencySIP.SelectedValue).ToString().Trim()))
        ////            mforderVo.FrequencyCode = ddlFrequencySIP.SelectedValue;
        ////        if (!string.IsNullOrEmpty((txtstartDateSIP.SelectedDate).ToString().Trim()))
        ////            mforderVo.StartDate = DateTime.Parse(txtstartDateSIP.SelectedDate.ToString());
        ////        else
        ////            mforderVo.StartDate = DateTime.MinValue;
        ////        if (!string.IsNullOrEmpty((txtendDateSIP.SelectedDate).ToString().Trim()))
        ////            mforderVo.EndDate = DateTime.Parse(txtendDateSIP.SelectedDate.ToString());
        ////        else
        ////            mforderVo.EndDate = DateTime.MinValue;
        ////    }
        ////    else if (TransType == "STB" || TransType == "SWP")
        ////    {
        ////        if (!string.IsNullOrEmpty((ddlFrequencySTP.SelectedValue).ToString().Trim()))
        ////            mforderVo.FrequencyCode = ddlFrequencySTP.SelectedValue;
        ////        if (!string.IsNullOrEmpty((txtstartDateSTP.SelectedDate).ToString().Trim()))
        ////            mforderVo.StartDate = DateTime.Parse(txtstartDateSTP.SelectedDate.ToString());
        ////        else
        ////            mforderVo.StartDate = DateTime.MinValue;
        ////        if (!string.IsNullOrEmpty((txtendDateSTP.SelectedDate).ToString().Trim()))
        ////            mforderVo.EndDate = DateTime.Parse(txtendDateSTP.SelectedDate.ToString());
        ////        else
        ////            mforderVo.EndDate = DateTime.MinValue;
        ////    }
        ////    if (ddlARNNo.SelectedIndex != 0)
        ////        mforderVo.ARNNo = ddlARNNo.SelectedItem.Text;
        ////    if (!String.IsNullOrEmpty(txtAssociateSearch.Text))
        ////        AgentId = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
        ////    if (AgentId.Rows.Count > 0)
        ////    {
        ////        mforderVo.AgentId = int.Parse(AgentId.Rows[0][1].ToString());
        ////    }
        ////    else
        ////        mforderVo.AgentId = 0;
        ////    //Convert.ToInt32(ddlAssociate.SelectedValue);

        ////    Session["orderVo"] = orderVo;
        ////    Session["mforderVo"] = mforderVo;

        ////}

        //protected void rbtnImmediate_CheckedChanged(object sender, EventArgs e)
        //{

        //}

        //protected void btnAddMore_Click(object sender, EventArgs e)
        //{
        //    btnSubmit_Click(this, null);
        //    // DdlLoad.SelectedIndex  = 2;
        //    //DdlLoad_Selectedindexchanged(this, null);

        //}

        //private DataTable GetUploadedImagePaths(int ProofUploadId)
        //{
        //    customerVo.CustomerId = Convert.ToInt32(txtCustomerId.Value);
        //    DataTable dtImages = new DataTable();
        //    try
        //    {
        //        dtImages = customerBo.GetCustomerUploadedProofs(customerVo.CustomerId, ProofUploadId);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "CustomerDao.cs:GetUploadedImagePaths()");
        //        object[] objects = new object[1];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }

        //    return dtImages;
        //}
        //private void LoadImages()
        //{
        //    //string Temppath = "";
        //    DataTable dtImages = new DataTable();
        //    DataTable dtBindImages = new DataTable();
        //    DataTable dtProofPurposes = new DataTable();
        //    dtBindImages.Columns.Add("ProofUploadId");
        //    dtBindImages.Columns.Add("ProofType");
        //    dtBindImages.Columns.Add("ProofName");
        //    dtBindImages.Columns.Add("ProofCopyType");
        //    dtBindImages.Columns.Add("ProofImage");
        //    dtBindImages.Columns.Add("ProofExtensions");
        //    dtBindImages.Columns.Add("ProofFileName");

        //    DataRow drBindImages = null;
        //    dtImages = GetUploadedImagePaths(0);
        //    System.Web.UI.WebControls.Image imageProof = new System.Web.UI.WebControls.Image();
        //    System.Web.UI.WebControls.HyperLink hypPdf = new HyperLink();
        //    string fileExt = "";

        //    int i = 0;
        //    string sourceDir = "";


        //}

        //protected void lnkBtnFIEdit_Click(object sender, EventArgs e)
        //{
        //    btnUpdate.Visible = true;
        //    ViewForm = "Edit";
        //    SetFICOntrolsEnablity(true);
        //    ShowTransactionType(1);
        //    UserControl FIControls = (UserControl)this.FindControl("FixedIncomeOrder");
        //    Type typUserControl = null;
        //    typUserControl = FIControls.GetType();

        //    System.Reflection.MethodInfo mthdUserControl = null;
        //    mthdUserControl = typUserControl.GetMethod("SetControlsEnablity");
        //    //object[] arrParameter = new object[1];
        //    //arrParameter[0] = true;
        //    //object[] arguments = new object[] { true };
        //    mthdUserControl.Invoke(FIControls, null);


        //}

        //public void SetEditViewMode(bool Bool)
        //{

        //    if (Bool)
        //    {


        //        txtCustomerName.Enabled = false;
        //        btnImgAddCustomer.Enabled = false;


        //        txtAmount.Enabled = false;
        //        ddlPaymentMode.Enabled = false;
        //        txtPaymentInstDate.Enabled = false;
        //        txtPaymentNumber.Enabled = false;
        //        rbtAmount.Enabled = false;
        //        rbtUnit.Enabled = false;
        //        txtNewAmount.Enabled = false;
        //        ddlBankName.Enabled = false;
        //        txtBranchName.Enabled = false;
        //        ddlFrequencySIP.Enabled = false;
        //        txtstartDateSIP.Enabled = false;
        //        txtendDateSIP.Enabled = false;

        //        ddlSchemeSwitch.Enabled = false;
        //        ddlFrequencySTP.Enabled = false;
        //        txtstartDateSTP.Enabled = false;
        //        txtendDateSTP.Enabled = false;

        //        txtCorrAdrLine1.Enabled = false;
        //        txtCorrAdrLine2.Enabled = false;
        //        txtCorrAdrLine3.Enabled = false;
        //        txtLivingSince.Enabled = false;
        //        txtCorrAdrCity.Enabled = false;
        //        ddlCorrAdrState.Enabled = false;
        //        txtCorrAdrPinCode.Enabled = false;
        //        ddlCorrAdrCountry.Enabled = false;
        //        ddlARNNo.Enabled = false;

        //        //sai btnSubmit.Enabled = false;
        //        //btnAddMore.Visible = false;
        //        //ddlAssociate.Enabled = false;

        //    }
        //    else
        //    {
        //        //txtOrederNumber.Enabled = true;
        //        ////sai  txtOrderDate.Enabled = true;
        //        //ddlBranch.Enabled = true;
        //        //ddlRM.Enabled = true;
        //        txtCustomerName.Enabled = false;
        //        btnImgAddCustomer.Enabled = false;
        //        //  ddltransType.Enabled = true;
        //        //ddlPortfolio.Enabled = true;
        //        //  ddlFolioNumber.Enabled = true;
        //        //btnAddFolio.Enabled = true;
        //        //sai             ddlAMCList..Enabled = false;
        //        //sai      ddlAmcSchemeList.Enabled = true;
        //        //ddlCategory.Enabled = true;
        //        //txtReceivedDate.Enabled = true;
        //        //txtApplicationNumber.Enabled = true;
        //        //rbtnImmediate.Enabled = true;
        //        //rbtnFuture.Enabled = true;
        //        ////sai txtFutureDate.Enabled = true;
        //        ////sai txtFutureTrigger.Enabled = true;
        //        //ddlOrderStatus.Enabled = true;
        //        //ddlOrderPendingReason.Enabled = true;
        //        ////sai  txtOrderDate.Enabled = false;

        //        txtAmount.Enabled = true;
        //        ddlPaymentMode.Enabled = true;
        //        txtPaymentInstDate.Enabled = true;
        //        txtPaymentNumber.Enabled = true;
        //        rbtAmount.Enabled = true;
        //        rbtUnit.Enabled = true;
        //        txtNewAmount.Enabled = true;
        //        ddlBankName.Enabled = true;
        //        txtBranchName.Enabled = true;
        //        ddlFrequencySIP.Enabled = true;
        //        txtstartDateSIP.Enabled = true;
        //        txtendDateSIP.Enabled = true;

        //        ddlSchemeSwitch.Enabled = true;
        //        ddlFrequencySTP.Enabled = true;
        //        txtstartDateSTP.Enabled = true;
        //        txtendDateSTP.Enabled = true;

        //        txtCorrAdrLine1.Enabled = true;
        //        txtCorrAdrLine2.Enabled = true;
        //        txtCorrAdrLine3.Enabled = true;
        //        txtLivingSince.Enabled = true;
        //        txtCorrAdrCity.Enabled = true;
        //        ddlCorrAdrState.Enabled = true;
        //        txtCorrAdrPinCode.Enabled = true;
        //        ddlCorrAdrCountry.Enabled = true;
        //        ddlARNNo.Enabled = true;

        //        ////sai btnSubmit.Enabled = true;
        //        //btnAddMore.Visible = false;
        //        //ddlAssociate.Enabled = true;
        //    }


        //}

        ////protected void lnkBtnFIEdit_Click(object sender, EventArgs e)
        ////{
        ////    SetEditViewMode(false);
        ////    ViewForm = "Edit";
        ////   // //sai lnkDelete.Visible = true;
        ////    if (mforderVo != null && orderVo != null)
        ////    {
        ////        mforderVo = (MFOrderVo)Session["mforderVo"];
        ////        orderVo = (OrderVo)Session["orderVo"];
        ////    }
        ////    if (mforderVo != null && orderVo != null)
        ////    {
        ////        if (ViewForm == "Edit")
        ////        {
        ////            SetControls("Edit", mforderVo, orderVo);
        ////          //  //sai  lnlFIBack.Visible = true;
        ////        }
        ////    }
        ////    else
        ////    {
        ////        SetControls("Entry", mforderVo, orderVo);
        ////    }
        ////    ////sai btnSubmit.Visible = false;
        ////    ////sai rgvOrderSteps.Enabled = true;
        ////    //btnAddMore.Visible = false;
        ////    ////sai btnUpdate.Visible = true;
        ////    ////sai lnkBtnFIEdit.Visible = false;
        ////    //btnreport.Visible = true;
        ////    //btnpdfReport.Visible = true;
        ////}

        //protected void lnlFIBack_Click(object sender, EventArgs e)
        //{
        //    if (Request.QueryString["FromPage"] != null)
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerOrderList','none');", true);
        //    }
        //    else if (Request.QueryString["fiaction"] != null)
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('OrderList','none');", true);
        //    }
        //}

        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    hdnButtonAction.Value = "Update";
        //    GetFICOntrolsValues();
        //    //List<int> OrderIds = new List<int>();
        //    //UpdateMFOrderDetails();
        //    //mfOrderBo.UpdateCustomerMFOrderDetails(orderVo, mforderVo, userVo.UserId);
        //    SetEditViewMode(true);
        //    imgBtnRefereshBank.Enabled = false;
        //    btnUpdate.Visible = false;
        //    //  btnViewInPDFNew.Visible = false;
        //    //   btnViewInDOCNew.Visible = false;
        //}

        //private void UpdateMFOrderDetails()
        //{

        //    //operationVo.CustomerId = int.Parse(txtCustomerId.Value);
        //    //mforderVo.CustomerName = txtCustomerName.Text;
        //    //if (orderVo.CustomerId != 0)
        //    //    hdnCustomerId.Value = orderVo.CustomerId.ToString();
        //    //mforderVo.BMName = lblGetBranch.Text;
        //    //mforderVo.RMName = lblGetRM.Text;
        //    //mforderVo.PanNo = lblgetPan.Text;
        //    //mforderVo.TransactionCode = TransType;
        //    //if (!string.IsNullOrEmpty(txtReceivedDate.SelectedDate.ToString()))
        //    //    orderVo.ApplicationReceivedDate = DateTime.Parse(txtReceivedDate.SelectedDate.ToString());
        //    //else
        //    //    orderVo.ApplicationReceivedDate = DateTime.MinValue;
        //    //orderVo.ApplicationNumber = txtApplicationNumber.Text;
        //    //if (ddlAMCList.SelectedIndex != -1)
        //    //    mforderVo.Amccode = int.Parse(ddlAMCList.SelectedValue);
        //    //else
        //    //    mforderVo.Amccode = 0;
        //    //mforderVo.category = ddlCategory.SelectedValue;
        //    //if (//sai      ddlAmcSchemeList.SelectedIndex != -1)
        //    //    mforderVo.SchemePlanCode = int.Parse(//sai      ddlAmcSchemeList.SelectedValue);
        //    //else
        //    //    mforderVo.SchemePlanCode = 0;
        //    //// mforderVo.portfolioId = int.Parse(ddlPortfolio.SelectedValue);
        //    //if (ddlFolioNumber.SelectedIndex != -1)
        //    //    mforderVo.accountid = int.Parse(ddlFolioNumber.SelectedValue);
        //    //else
        //    //    mforderVo.accountid = 0;
        //    //orderVo.OrderDate = Convert.ToDateTime(//sai  txtOrderDate.SelectedDate);
        //    //mforderVo.OrderNumber = int.Parse(//sai  lblGetOrderNo.Text);
        //    ////orderVo.OrderStatusCode = ddlOrderStatus.SelectedValue;
        //    ////orderVo.ReasonCode = ddlOrderPendingReason.SelectedValue;
        //    //if (rbtnImmediate.Checked == true)
        //    //    mforderVo.IsImmediate = 1;
        //    //else
        //    //    mforderVo.IsImmediate = 0;
        //    //if (!string.IsNullOrEmpty(//sai txtFutureDate.SelectedDate.ToString().Trim()))
        //    //    mforderVo.FutureExecutionDate = DateTime.Parse(//sai txtFutureDate.SelectedDate.ToString());
        //    //else
        //    //    mforderVo.FutureExecutionDate = DateTime.MinValue;
        //    //if (!string.IsNullOrEmpty((//sai txtFutureTrigger.Text).ToString().Trim()))
        //    //    mforderVo.FutureTriggerCondition = //sai txtFutureTrigger.Text;
        //    //else
        //    //    mforderVo.FutureTriggerCondition = "";
        //    //if (!string.IsNullOrEmpty((txtAmount.Text).ToString().Trim()))
        //    //    mforderVo.Amount = double.Parse(txtAmount.Text);
        //    //else
        //    //    mforderVo.Amount = 0;

        //    //if (TransType == "Sel" || TransType == "STP" || TransType == "SWP" || TransType == "SWB")
        //    //{
        //    //    if (rbtAmount.Checked == true)
        //    //    {
        //    //        if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
        //    //            mforderVo.Amount = double.Parse(txtNewAmount.Text);
        //    //        else
        //    //            mforderVo.Amount = 0;
        //    //    }
        //    //    if (rbtUnit.Checked == true)
        //    //    {
        //    //        if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
        //    //            mforderVo.Units = double.Parse(txtNewAmount.Text);
        //    //        else
        //    //            mforderVo.Units = 0;
        //    //    }
        //    //}
        //    //if (TransType == "SIP")
        //    //{
        //    //    if (!string.IsNullOrEmpty((ddlFrequencySIP.SelectedValue).ToString().Trim()))
        //    //        mforderVo.FrequencyCode = ddlFrequencySIP.SelectedValue;
        //    //    if (!string.IsNullOrEmpty((txtstartDateSIP.SelectedDate).ToString().Trim()))
        //    //        mforderVo.StartDate = DateTime.Parse(txtstartDateSIP.SelectedDate.ToString());
        //    //    else
        //    //        mforderVo.StartDate = DateTime.MinValue;
        //    //    if (!string.IsNullOrEmpty((txtendDateSIP.SelectedDate).ToString().Trim()))
        //    //        mforderVo.EndDate = DateTime.Parse(txtendDateSIP.SelectedDate.ToString());
        //    //    else
        //    //        mforderVo.EndDate = DateTime.MinValue;
        //    //}
        //    //else if (TransType == "STB" || TransType == "SWP")
        //    //{
        //    //    if (!string.IsNullOrEmpty((ddlFrequencySTP.SelectedValue).ToString().Trim()))
        //    //        mforderVo.FrequencyCode = ddlFrequencySTP.SelectedValue;

        //    //    if (!string.IsNullOrEmpty((txtstartDateSTP.SelectedDate).ToString().Trim()))
        //    //        mforderVo.StartDate = DateTime.Parse(txtstartDateSTP.SelectedDate.ToString());
        //    //    else
        //    //        mforderVo.StartDate = DateTime.MinValue;
        //    //    if (!string.IsNullOrEmpty((txtendDateSTP.SelectedDate).ToString().Trim()))
        //    //        mforderVo.EndDate = DateTime.Parse(txtendDateSTP.SelectedDate.ToString());
        //    //    else
        //    //        mforderVo.EndDate = DateTime.MinValue;
        //    //}

        //    //if (ddlPaymentMode.SelectedValue == "ES")
        //    //    orderVo.PaymentMode = "ES";
        //    //else if (ddlPaymentMode.SelectedValue == "DF")
        //    //    orderVo.PaymentMode = "DF";
        //    //else if (ddlPaymentMode.SelectedValue == "CQ")
        //    //    orderVo.PaymentMode = "CQ";
        //    //if (!string.IsNullOrEmpty(txtPaymentNumber.Text.ToString().Trim()))
        //    //    orderVo.ChequeNumber = txtPaymentNumber.Text;
        //    //else
        //    //    orderVo.ChequeNumber = "";
        //    //if (txtPaymentInstDate.SelectedDate != null)
        //    //    orderVo.PaymentDate = DateTime.Parse(txtPaymentInstDate.SelectedDate.ToString());
        //    //else
        //    //    orderVo.PaymentDate = DateTime.MinValue;
        //    //if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
        //    //{
        //    //    if (ddlBankName.SelectedValue != "Select")
        //    //        orderVo.CustBankAccId = int.Parse(ddlBankName.SelectedValue);
        //    //    else
        //    //        orderVo.CustBankAccId = 0;
        //    //}
        //    //else
        //    //    orderVo.CustBankAccId = 0;
        //    //if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
        //    //{
        //    //    if (ddlBankName.SelectedValue != "Select")
        //    //        mforderVo.BankName = ddlBankName.SelectedItem.Text;
        //    //    else
        //    //        mforderVo.BankName = "";
        //    //}
        //    //else
        //    //    mforderVo.BankName = "";
        //    //if (!string.IsNullOrEmpty(txtBranchName.Text.ToString().Trim()))
        //    //    mforderVo.BranchName = txtBranchName.Text;
        //    //else
        //    //    mforderVo.BranchName = "";
        //    //if (ddlSchemeSwitch.SelectedValue != "")
        //    //{
        //    //    if (ddlSchemeSwitch.SelectedIndex != 0)
        //    //        mforderVo.SchemePlanSwitch = int.Parse(ddlSchemeSwitch.SelectedValue);
        //    //}
        //    //if (!string.IsNullOrEmpty(txtCorrAdrLine1.Text.ToString().Trim()))
        //    //    mforderVo.AddrLine1 = txtCorrAdrLine1.Text;
        //    //else
        //    //    mforderVo.AddrLine1 = "";
        //    //if (!string.IsNullOrEmpty(txtCorrAdrLine2.Text.ToString().Trim()))
        //    //    mforderVo.AddrLine2 = txtCorrAdrLine2.Text;
        //    //else
        //    //    mforderVo.AddrLine2 = "";
        //    //if (!string.IsNullOrEmpty(txtCorrAdrLine3.Text.ToString().Trim()))
        //    //    mforderVo.AddrLine3 = txtCorrAdrLine3.Text;
        //    //else
        //    //    mforderVo.AddrLine3 = "";
        //    //if (txtLivingSince.SelectedDate.ToString() != "dd/mm/yyyy")
        //    //    mforderVo.LivingSince = DateTime.MinValue;
        //    //else
        //    //    mforderVo.LivingSince = DateTime.MinValue;
        //    //if (!string.IsNullOrEmpty(txtCorrAdrCity.Text.ToString().Trim()))
        //    //    mforderVo.City = txtCorrAdrCity.Text;
        //    //else
        //    //    mforderVo.City = "";
        //    //if (ddlCorrAdrState.SelectedIndex != 0)
        //    //    mforderVo.State = ddlCorrAdrState.SelectedItem.Text;
        //    //else
        //    //    mforderVo.State = "";
        //    //if (!string.IsNullOrEmpty(txtCorrAdrPinCode.Text.ToString().Trim()))
        //    //    mforderVo.Pincode = txtCorrAdrPinCode.Text;
        //    //else
        //    //    mforderVo.Pincode = "";
        //    //mforderVo.Country = ddlCorrAdrCountry.SelectedValue;
        //    //if (ddlARNNo.SelectedIndex != 0)
        //    //    mforderVo.ARNNo = ddlARNNo.SelectedItem.Text;
        //    //if (!string.IsNullOrEmpty(txtAssociateSearch.Text))
        //    //{

        //    //    AgentId = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
        //    //    if (AgentId.Rows.Count > 0)
        //    //    {
        //    //        orderVo.AgentId = int.Parse(AgentId.Rows[0][1].ToString());
        //    //    }
        //    //    else
        //    //        orderVo.AgentId = 0;
        //    //}
        //    //if (ddlAssociate.SelectedIndex != 0)
        //    //    orderVo.AgentId = Convert.ToInt32(ddlAssociate.SelectedValue);
        //}

        //protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int BankAccountId = 0;
        //    DataTable dtgetBankBranch;
        //    if (ddlBankName.SelectedIndex != 0)
        //    {
        //        BankAccountId = int.Parse(ddlBankName.SelectedValue);
        //        dtgetBankBranch = mfOrderBo.GetBankBranch(BankAccountId);
        //        if (dtgetBankBranch.Rows.Count > 0)
        //        {
        //            DataRow dr = dtgetBankBranch.Rows[0];
        //            txtBranchName.Text = dr["CB_BranchName"].ToString();
        //        }
        //        hdnBankName.Value = ddlBankName.SelectedItem.Text;
        //    }
        //}
        //protected void ddlDepoBank_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int BankAccountId = 0;
        //    DataTable dtgetBankBranch;
        //    if (ddlBankName.SelectedIndex != 0)
        //    {
        //        BankAccountId = int.Parse(ddlDepoBank.SelectedValue);
        //        dtgetBankBranch = mfOrderBo.GetBankBranch(BankAccountId);
        //        if (dtgetBankBranch.Rows.Count > 0)
        //        {
        //            DataRow dr = dtgetBankBranch.Rows[0];
        //            txtDepositedBranch.Text = dr["CB_BranchName"].ToString();
        //        }
        //        //   hdnBankName.Value = ddlBankName.SelectedItem.Text;
        //    }
        //}
        //protected void lnkDelete_Click(object sender, EventArgs e)
        //{
        //    if (fiorderVo != null && orderVo != null)
        //    {
        //        orderId = orderVo.OrderId;

        //        if (orderId != 0)
        //        {
        //            fiorderBo.DeleteFIOrder(orderId);
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Your order has been deleted.');", true);
        //            ClearAllFields();

        //            lblGetRM.Text = "";
        //            lblGetBranch.Text = "";
        //            lblgetPan.Text = "";
        //            ////sai  txtOrderDate.SelectedDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
        //            ////sai  lblGetOrderNo.Text = ((mfOrderBo.GetOrderNumber()) + 1).ToString();
        //            //txtApplicationNumber.Enabled = true;
        //            ////sai lnkBtnFIEdit.Visible = false;
        //            ////sai  lnlFIBack.Visible = false;
        //            ////sai lnkDelete.Visible = false;
        //            ////sai btnUpdate.Visible = false;
        //            ////sai btnSubmit.Visible = true;
        //            //btnAddMore.Visible = true;
        //            ////sai rgvOrderSteps.Visible = false;
        //            //SetEditViewMode(false);
        //            //btnImgAddCustomer.Enabled = true;
        //            //btnImgAddCustomer.Visible = true;
        //            //txtCustomerName.Enabled = true;
        //            //txtCustomerName.Text = "";

        //        }

        //    }

        //}

        //protected void btnreport_Click(object sender, EventArgs e)
        //{
        //    mail = "0";
        //    DisplayTransactionSlip();

        //}

        //private void DisplayTransactionSlip()
        //{
        //    string schemeSwitch = ""; string bankName = ""; string arnno = "";
        //    if (!string.IsNullOrEmpty(hdnCustomerId.Value.ToString().Trim()))
        //        customerId = int.Parse(hdnCustomerId.Value);
        //    if (!string.IsNullOrEmpty(hdnPortfolioId.Value.ToString().Trim()))
        //        portfolioId = int.Parse(hdnPortfolioId.Value);
        //    if (ddlSchemeSwitch.SelectedIndex != -1 && ddlSchemeSwitch.SelectedIndex != 0)
        //        schemeSwitch = ddlSchemeSwitch.SelectedItem.Text;
        //    if (ddlBankName.SelectedIndex != -1 && ddlBankName.SelectedIndex != 0)
        //        bankName = ddlBankName.SelectedItem.Text;
        //    if (ddlARNNo.SelectedIndex != 0)
        //        arnno = ddlARNNo.SelectedItem.Text;

        //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Display", "loadcontrol('Display','action=Order');", true);
        //    //Response.Write("<script type='text/javascript'>detailedresults= window.open('Display.aspx?PageId=Display&result1=" + var1 + "&result2=" + var2 + "', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no');</script>");
        //    Response.Write("<script type='text/javascript'>detailedresults=window.open('Reports/Display.aspx?Page=MFOrder&CustomerId=" + customerId + "&AmcCode=" + hdnAmcCode.Value +
        //        "&AccoutId=" + hdnAccountId.Value + "&SchemeCode=" + hdnSchemeName.Value + "&Type=" + hdnType.Value + "&Portfolio=" + portfolioId +
        //        "&BankName=" + bankName + "&BranchName=" + txtBranchName.Text + "&Amount=" + txtAmount.Text + "&ChequeNo=" + txtPaymentNumber.Text + "&ChequeDate=" + txtPaymentInstDate.SelectedDate +
        //        "&StartDateSIP=" + txtstartDateSIP.SelectedDate + "&StartDateSTP=" + txtstartDateSTP.SelectedDate + "&NewAmount=" + txtNewAmount.Text +
        //        "&EndDateSIP=" + txtendDateSIP.SelectedDate + "&EndDateSTP=" + txtendDateSTP.SelectedDate + "&SchemeSwitch=" + schemeSwitch +
        //        "&RbtnUnits=" + rbtUnit.Checked + "&RbtnAmounts=" + rbtAmount.Checked + "&ArnNo=" + arnno + "&mail=" + mail +
        //        "','mywindow', 'width=1000,height=450,scrollbars=yes,location=center');</script>");
        //}

        //protected void btnpdfReport_Click(object sender, EventArgs e)
        //{
        //    mail = "2";
        //    DisplayTransactionSlip();
        //}
        ////CustomerVo customerVo = new CustomerVo();
        ////CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        ////CustomerBo customerBo = new CustomerBo();
        ////AdvisorVo advisorVo;
        ////UserVo userVo;
        ////RMVo rmVo = new RMVo();
        ////protected void Page_Load(object sender, EventArgs e)
        ////{
        ////   // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " GetCustomerId();", true);

        ////    userVo = (UserVo)Session[SessionContents.UserVo];
        ////    if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
        ////        advisorVo = (AdvisorVo)Session["advisorVo"];
        ////    if (!string.IsNullOrEmpty(Session[SessionContents.RmVo].ToString()))
        ////        rmVo = (RMVo)Session[SessionContents.RmVo];
        ////    if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
        ////    {
        ////        txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
        ////        txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
        ////        AutoCompleteExtender1.ContextKey = advisorVo.advisorId.ToString();
        ////        AutoCompleteExtender1.ServiceMethod = "GetAdviserCustomerPan";
        ////        //AutoCompleteExtender2.ContextKey = advisorVo.advisorId.ToString();
        ////        //AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetails";
        ////    }
        ////    else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
        ////    {
        ////        txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
        ////        txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";

        ////    }
        ////    else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
        ////    {
        ////        txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
        ////        txtCustomerName_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
        ////    }
        ////    else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
        ////    {
        ////        txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
        ////        txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
        ////        AutoCompleteExtender1.ContextKey = advisorVo.advisorId.ToString();
        ////        AutoCompleteExtender1.ServiceMethod = "GetAdviserCustomerPan";
        ////        //AutoCompleteExtender2.ContextKey = associateuserheirarchyVo.AgentCode;
        ////        //AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetailsForAssociates";

        ////    }
        ////   // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " GetCustomerId();", true);
        ////}
        ////protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////}
        ////protected void ddlIssuer_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////}
        ////protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////}

        ////protected void ddlSeries_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////}

        ////protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        ////{

        ////}
        ////protected void //sai  txtOrderDate_DateChanged(object sender, EventArgs e)
        ////{
        ////}

        ////protected void txtApplicationDt_DateChanged(object sender, EventArgs e)
        ////{
        ////}
        ////protected void txtCustomerId_ValueChanged1(object sender, EventArgs e)
        ////{
        ////    if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
        ////    {
        ////        //trJointHoldersList.Visible = false;
        ////        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
        ////        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
        ////        ////sai             ddlAMCList..Enabled = true;
        ////        customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
        ////        Session["customerVo"] = customerVo;
        ////        txtBranchName.Text = customerVo.BranchName;
        ////        lblRM.Text = customerVo.RMName;
        ////        lblgetPan.Text = customerVo.PANNum;
        ////        //hdnCustomerId.Value = txtCustomerId.Value;
        ////        //customerId = int.Parse(txtCustomerId.Value);
        ////        // if (ddlsearch.SelectedItem.Value == "2")
        ////        //     lblgetcust.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;

        ////        //= rmVo.FirstName + ' ' + rmVo.MiddleName + ' ' + rmVo.LastName;
        ////        // BindBank(customerId);
        ////        //BindPortfolioDropdown(customerId);
        ////        // ddltransType.SelectedIndex = 0;
        ////        //  BindISAList();
        ////    }
        ////}

        ////  protected void rgvOrderSteps_ItemDataBound(object sender, GridItemEventArgs e)
        ////{
        ////    DataTable dt = (DataTable)Session["OrderDetails"];
        ////    if (e.Item is GridDataItem)
        ////    {
        ////        GridDataItem dataItem = e.Item as GridDataItem;
        ////        //((Literal)dataItem["DropDownColumnStatus"].Controls[0]).Text = dataItem.GetDataKeyValue("WOS_OrderStepCode").ToString();
        ////        //((Literal)dataItem["DropDownColumnStatusReason"].Controls[0]).Text = dataItem.GetDataKeyValue("WOS_OrderStepCode").ToString();

        ////        TemplateColumn tm = new TemplateColumn();
        ////        Label lblStatusCode = new Label();
        ////        Label lblOrderStep = new Label();
        ////        LinkButton editButton = dataItem["EditCommandColumn"].Controls[0] as LinkButton;
        ////        Label lblOrderStatus = new Label();
        ////        Label lblOrderStatusReason = new Label();
        ////        lblStatusCode = (Label)e.Item.FindControl("lblStatusCode");
        ////        lblOrderStep = (Label)e.Item.FindControl("lblOrderStepCode");
        ////        lblOrderStatus = (Label)e.Item.FindControl("lblOrderStatus");
        ////        lblOrderStatusReason = (Label)e.Item.FindControl("lblOrderStatusReason");
        ////        Label lblCMFOS_Date = (Label)e.Item.FindControl("lblCMFOS_Date");
        ////        if (lblOrderStep.Text.Trim() == "IP")
        ////        {
        ////            if (lblStatusCode.Text == "OMIP")
        ////            {
        ////                editButton.Text = "Mark as Pending";
        ////                result = mfOrderBo.MFOrderAutoMatch(orderVo.OrderId, mforderVo.SchemePlanCode, mforderVo.accountid, mforderVo.TransactionCode, orderVo.CustomerId, mforderVo.Amount, orderVo.OrderDate);
        ////                if (result == true)
        ////                {
        ////                    editButton.Text = "";
        ////                    lblOrderStatusReason.Text = "";
        ////                }

        ////            }

        ////            else if (lblStatusCode.Text == "OMPD")
        ////            {
        ////                editButton.Text = "Mark as InProcess";
        ////            }

        ////        }
        ////        else if (lblOrderStep.Text.Trim() == "PR")
        ////        {
        ////            if (result == true)
        ////            {
        ////                lblOrderStatus.Text = "Executed";
        ////                lblOrderStatusReason.Text = "Order Confirmed";
        ////            }
        ////            else
        ////            {
        ////                lblOrderStatus.Text = "";
        ////                lblOrderStatusReason.Text = "";
        ////                lblCMFOS_Date.Text = "";
        ////            }
        ////            editButton.Text = "";
        ////        }
        ////        else
        ////        {
        ////            lblOrderStatusReason.Text = "";
        ////            editButton.Text = "";
        ////        }

        ////        //string editColumn = dataItem["COS_IsEditable"].Text;
        ////        //if (editColumn == "1")
        ////        //{
        ////        //    editButton.Enabled = true;
        ////        //}
        ////        //else
        ////        //{
        ////        //    editButton.Enabled = false;
        ////        //}
        ////    }
        ////}
        //public void BindOrderStepsGrid()
        //{
        //    DataSet dsOrderSteps = new DataSet();
        //    DataTable dtOrderDetails;
        //    pnlOrderSteps.Visible = true;
        //    //SetControls(false);
        //    dsOrderSteps = orderbo.GetOrderStepsDetails(orderId);
        //    dtOrderDetails = dsOrderSteps.Tables[0];
        //    //if (dtOrderDetails.Rows.Count == 0)
        //    //{
        //    //    lblPickNominee.Text = "You have not placed any Order";
        //    //}
        //    //else
        //    //{
        //    //lblPickNominee.Text = "Order Steps";
        //    rgvOrderSteps.DataSource = dtOrderDetails;
        //    rgvOrderSteps.DataBind();
        //    Session["OrderDetails"] = dtOrderDetails;
        //    //}
        //}
        //protected void rgvOrderSteps_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    this.rgvOrderSteps.DataSource = (DataTable)Session["OrderDetails"];
        //}

        //protected void rgvOrderSteps_ItemCommand(object source, GridCommandEventArgs e)
        //{
        //    bool bResult = false;
        //    if (e.CommandName == "Update")
        //    {
        //        GridEditableItem edititem = e.Item as GridEditableItem;
        //        GridEditFormItem editform = (GridEditFormItem)e.Item;


        //        RadComboBox rcStatus = edititem.FindControl("ddlCustomerOrderStatus") as RadComboBox;
        //        RadComboBox rcPendingReason = edititem.FindControl("ddlCustomerOrderStatusReason") as RadComboBox;

        //        DataTable dt = new DataTable();
        //        dt = (DataTable)Session["OrderDetails"];

        //        string orderStepCode = dt.Rows[e.Item.ItemIndex]["WOS_OrderStepCode"].ToString().Trim();
        //        orderId = int.Parse(dt.Rows[e.Item.ItemIndex]["CO_OrderId"].ToString().Trim());
        //        string updatedStatus = rcStatus.SelectedValue;
        //        string updatedReason = rcPendingReason.SelectedValue;


        //        bResult = orderbo.UpdateOrderStep(updatedStatus, updatedReason, orderId, orderStepCode);
        //        if (bResult == true)
        //        {
        //            rgvOrderSteps.Controls.Add(new LiteralControl("<strong>Successfully Updated</strong>"));
        //        }
        //        else
        //        {
        //            rgvOrderSteps.Controls.Add(new LiteralControl("<strong>Unable to update value</strong>"));
        //            e.Canceled = true;
        //        }
        //        BindOrderStepsGrid();
        //    }
        //}

        //protected void rcStatus_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    RadComboBox rcStatus = (RadComboBox)o;
        //    GridEditableItem editedItem = rcStatus.NamingContainer as GridEditableItem;
        //    RadComboBox rcPendingReason = editedItem.FindControl("rcbPendingReason") as RadComboBox;
        //    string statusOrderCode = rcStatus.SelectedValue;
        //    BindRadComboBoxPendingReason(rcPendingReason, statusOrderCode);
        //}

        //// View and edit

        //private void SetParameters()
        //{
        //    //if (ddlBranch.SelectedIndex != 0)
        //    //    hdnBranchId.Value = ddlBranch.SelectedValue;
        //    //else
        //    //    hdnBranchId.Value = "";

        //    //if (ddlRM.SelectedIndex != 0)
        //    //    hdnRMId.Value = ddlRM.SelectedValue;
        //    //else
        //    //    hdnRMId.Value = "";

        //    //if (txtFromDate.SelectedDate.ToString() != "")
        //    //    hdnFromdate.Value = DateTime.Parse(txtFromDate.SelectedDate.ToString()).ToString();
        //    //else
        //    //    hdnFromdate.Value = DateTime.MinValue.ToString();

        //    //if (txtToDate.SelectedDate.ToString() != "")
        //    //    hdnTodate.Value = DateTime.Parse(txtToDate.SelectedDate.ToString()).ToString();
        //    //else
        //    //    hdnTodate.Value = DateTime.MinValue.ToString();

        //    //if (ddlOrderStatus.SelectedIndex != 0)
        //    //{
        //    //    hdnOrderStatus.Value = ddlOrderStatus.SelectedValue.ToString();
        //    //}
        //    //else
        //    //{
        //    //    hdnOrderStatus.Value = "OMIP";
        //    //}
        //}
        ////private void SetParameterSubbroker()
        ////{
        ////if (userType == "advisor" || userType == "rm" || userType == "bm")
        ////{
        ////    hdnAgentCode.Value = "0";
        ////    hdnAgentId.Value = "0";
        ////    if (ddlBrokerCode.SelectedIndex != 0)
        ////    {
        ////        hdnSubBrokerCode.Value = ddlBrokerCode.SelectedItem.Value.ToString();
        ////        ViewState["SubBrokerCode"] = hdnSubBrokerCode.Value;
        ////    }
        ////    else
        ////    {
        ////        hdnSubBrokerCode.Value = "0";
        ////    }
        ////}
        ////else if (userType == "associates")
        ////{

        ////    if (ddlBrokerCode.SelectedIndex != 0)
        ////    {
        ////        hdnAgentCode.Value = ddlBrokerCode.SelectedItem.ToString();
        ////        hdnAgentId.Value = ddlBrokerCode.SelectedItem.Value.ToString();

        ////    }
        ////    else
        ////    {
        ////        hdnAgentCode.Value = AgentCode;
        ////        hdnAgentId.Value = "0";
        ////        //AgentId = int.Parse(ddlBrokerCode.SelectedItem.Value.ToString());
        ////    }
        ////AgentCode = ddlBrokerCode.SelectedValue.ToString();

        ////    }

        ////}
        //protected void BindGvOrderList()
        //{
        //    //  if (userType != "associates")
        //    // {
        //    //SetParameters();
        //    //SetParameterSubbroker();
        //    // }
        //    DataTable dtOrder = new DataTable();
        //    dtOrder = fiorderBo.GetOrderList(advisorVo.advisorId, "", "", Convert.ToDateTime(hdnTodate.Value), Convert.ToDateTime(hdnFromdate.Value), "", hdnCustomerId.Value, "FI", userType, 0, "", "");

        //    //dtOrder = fiorderBo.GetOrderList(advisorVo.advisorId, "", "", Convert.ToDateTime(hdnTodate.Value), Convert.ToDateTime(hdnFromdate.Value), hdnOrderStatus.Value, hdnCustomerId.Value, hdnOrderType.Value, userType, int.Parse(hdnAgentId.Value), hdnSubBrokerCode.Value, hdnAgentCode.Value);
        //    if (dtOrder.Rows.Count > 0)
        //    {
        //        // trExportFilteredDupData.Visible = true;
        //        if (Cache["OrderList" + advisorVo.advisorId] == null)
        //        {
        //            Cache.Insert("OrderList" + advisorVo.advisorId, dtOrder);
        //        }
        //        else
        //        {
        //            Cache.Remove("OrderList" + advisorVo.advisorId);
        //            Cache.Insert("OrderList" + advisorVo.advisorId, dtOrder);
        //        }
        //        gvOrderList.DataSource = dtOrder;
        //        gvOrderList.DataBind();
        //        gvOrderList.Visible = true;
        //        //ErrorMessage.Visible = false;
        //        //tblMessage.Visible = false;
        //        //pnlOrderList.Visible = true;
        //        //  btnExportFilteredDupData.Visible = true;
        //    }
        //    else
        //    {
        //        gvOrderList.Visible = false;
        //        //tblMessage.Visible = true;
        //        //ErrorMessage.Visible = true;
        //        //btnExportFilteredDupData.Visible = false;
        //        //ErrorMessage.InnerText = "No Records Found...!";
        //        //pnlOrderList.Visible = false;
        //    }
        //}



        //protected void gvOrderList_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        //{
        //    //  trExportFilteredDupData.Visible = true;
        //    DataTable dtGIDetails = new DataTable();
        //    dtGIDetails = (DataTable)Cache["OrderList" + advisorVo.advisorId];
        //    gvOrderList.Visible = true;
        //    this.gvOrderList.DataSource = dtGIDetails;
        //}

        //protected void gvOrderList_ItemCommand(object source, GridCommandEventArgs e)
        //{
        //    if (e.CommandName == "Redirect")
        //    {

        //        GridDataItem item = (GridDataItem)e.Item;
        //        string orderId = item.GetDataKeyValue("CO_OrderId").ToString();
        //        string customerId = item.GetDataKeyValue("C_CustomerId").ToString();
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + " ');", true);
        //    }
        //}
        //protected void gvOrderList_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridDataItem)
        //    {
        //        GridDataItem item = (e.Item as GridDataItem);
        //        RadComboBox rcb = new RadComboBox();
        //        TemplateColumn tm = new TemplateColumn();
        //        RadComboBox lbl = new RadComboBox();
        //        if (userType == "bm")
        //        {

        //            rcb = (RadComboBox)e.Item.FindControl("ddlMenu");
        //            if (rcb != null)
        //            {
        //                rcb.Items.FindItemByValue("Edit").Remove();
        //            }
        //        }


        //    }
        //}

        //private void OnTaxStatus()
        //{

        //    if (!string.IsNullOrEmpty(txtCustomerId.Value))
        //    {

        //        txtTax.Text = fiorderBo.GetTaxStatus(Convert.ToInt32(txtCustomerId.Value));
        //    }

        //}



        //public void SetControlsEnablity()
        //{
        //    ddlModeofHOldingFI.Enabled = true;
        //    txtOrderDate.Enabled = true;
        //    txtApplicationDate.Enabled = true;
        //    lblGetOrderNo.Enabled = true;
        //    txtApplicationNumber.Enabled = true;
        //    txtSeries.Enabled = true;
        //    ddlTranstype.Enabled = true;
        //    ddlCategory.Enabled = true;
        //    ddlIssuer.Enabled = true;
        //    txtExistDepositreceiptno.Enabled = true;
        //    txtRenAmt.Enabled = true;
        //    txtMaturDate.Enabled = true;
        //    txtMatAmt.Enabled = true;
        //    ddlScheme.Enabled = true;
        //    ddlSeries.Enabled = true;
        //    txtPayAmt.Enabled = true;
        //    ddlSchemeOption.Enabled = true;
        //    txtExistDepositreceiptno.Enabled = true;
        //    ddlFrequency.Enabled = true;
        //}

        //private void SetControls(bool Val)
        //{
        //    ddlModeofHOldingFI.Enabled = Val;
        //    txtOrderDate.Enabled = Val;
        //    txtApplicationDate.Enabled = Val;
        //    lblGetOrderNo.Enabled = Val;
        //    txtApplicationNumber.Enabled = Val;
        //    txtSeries.Enabled = Val;
        //    ddlTranstype.Enabled = Val;
        //    ddlCategory.Enabled = Val;
        //    ddlIssuer.Enabled = Val;
        //    txtExistDepositreceiptno.Enabled = Val;
        //    txtRenAmt.Enabled = Val;
        //    txtMaturDate.Enabled = Val;
        //    txtMatAmt.Enabled = Val;
        //    ddlScheme.Enabled = Val;
        //    ddlSeries.Enabled = Val;
        //    txtPayAmt.Enabled = Val;
        //    ddlSchemeOption.Enabled = Val;
        //    txtExistDepositreceiptno.Enabled = Val;
        //    ddlFrequency.Enabled = Val;

        //    OrderVo orderVo = new OrderVo();
        //    if (Session["orderVo"] != null && Session["fiorderVo"] != null)
        //    {
        //        orderVo = (OrderVo)Session["orderVo"];
        //        fiorderVo = (FIOrderVo)Session["fiorderVo"];
        //    }
        //    lblGetOrderNo.Text = fiorderVo.OrderNumber.ToString();
        //    ddlCategory.SelectedValue = fiorderVo.AssetInstrumentCategoryCode;
        //    ddlCategory_SelectedIndexChanged(this, null);

        //    ddlIssuer.SelectedValue = fiorderVo.IssuerId;
        //    ddlIssuer_SelectedIndexChanged(this, null);

        //    ddlScheme.SelectedValue = fiorderVo.SchemeId.ToString();
        //    ddlScheme_SelectedIndexChanged(this, null);



        //    ddlSeries.SelectedValue = fiorderVo.SeriesId.ToString();
        //    ddlSeries_SelectedIndexChanged(this, null);



        //    //   lblGetOrderNo.Text = orderVo.OrderNumber.ToString();//= Convert.ToInt32();
        //    if (fiorderVo.Privilidge == "Seniorcitizens")
        //    {
        //        ChkSeniorcitizens.Checked = true;

        //    }
        //    else if (fiorderVo.Privilidge == "Widow")
        //    {
        //        ChkWidow.Checked = true;
        //    }
        //    else if (fiorderVo.Privilidge == "ArmedForcePersonnel")
        //    {
        //        ChkArmedForcePersonnel.Checked = true;
        //    }
        //    else if (fiorderVo.Privilidge == "Existingrelationship")
        //    {
        //        CHKExistingrelationship.Checked = true;

        //    }

        //    if (fiorderVo.Depositpayableto == "Firstholder")
        //    {
        //        ChkFirstholder.Checked = true;
        //    }
        //    else if (fiorderVo.Depositpayableto == "Either or survivor")
        //    {
        //        ChkEORS.Checked = true;

        //    }

        //    if (Convert.ToDateTime(orderVo.OrderDate) != DateTime.MinValue)
        //        txtOrderDate.SelectedDate = orderVo.OrderDate;
        //    //else
        //    //    txtOrderDate.SelectedDate = DateTime.MinValue;

        //    //= Convert.ToDateTime();
        //    if (Convert.ToDateTime(orderVo.ApplicationReceivedDate) != DateTime.MinValue)
        //        txtApplicationDate.SelectedDate = orderVo.OrderDate;
        //    //else
        //    //    txtApplicationDate.SelectedDate = DateTime.MinValue;


        //    //   txtApplicationDate.FocusedDate =  orderVo.ApplicationReceivedDate; //= Convert.ToDateTime();
        //    txtApplicationNumber.Text = orderVo.ApplicationNumber;//=;

        //    //  txtSeries.Text = fiorderVo.SeriesDetails;
        //    ddlTranstype.SelectedValue = fiorderVo.TransactionType;
        //    ddlTranstype_SelectedIndexChanged(this, null);

        //    ddlModeofHOldingFI.SelectedValue = fiorderVo.ModeOfHolding;

        //    txtExistDepositreceiptno.Text = fiorderVo.ExisitingDepositreceiptno;

        //    if (!string.IsNullOrEmpty(fiorderVo.RenewalAmount.ToString()))
        //        txtRenAmt.Text = fiorderVo.RenewalAmount.ToString();
        //    else
        //        txtRenAmt.Text = "0";


        //    if (Convert.ToDateTime(fiorderVo.MaturityDate) != DateTime.MinValue)
        //        txtMaturDate.SelectedDate = fiorderVo.MaturityDate;
        //    //else
        //    //    txtMaturDate.SelectedDate = DateTime.MinValue;

        //    // txtMaturDate.FocusedDate =fiorderVo.MaturityDate ;

        //    // if (!string.IsNullOrEmpty(fiorderVo.MaturityAmount))
        //    txtMatAmt.Text = fiorderVo.MaturityAmount.ToString();
        //    //  else
        //    //    txtMatAmt.Text = " 0";



        //    //if (!string.IsNullOrEmpty(txtPayAmt.Text))
        //    txtPayAmt.Text = Convert.ToDouble(fiorderVo.AmountPayable).ToString();
        //    //  else
        //    //  txtPayAmt.Text = "0";

        //    //ddlModeofHOlding.SelectedValue = "0";// fiorderVo.ModeOfHolding;

        //    ddlSchemeOption.SelectedValue = fiorderVo.Schemeoption;
        //    if (!string.IsNullOrEmpty(fiorderVo.ExisitingDepositreceiptno))
        //        txtExistDepositreceiptno.Text = fiorderVo.ExisitingDepositreceiptno;
        //    else
        //        txtExistDepositreceiptno.Text = "";

        //    ddlFrequency.SelectedValue = fiorderVo.Frequency;
        //    ddlFrequency_SelectedIndexChanged(this, null);
        //    // fiorderVo.Privilidge = "";


        //    //if (rbtnNo.Checked)
        //    //{
        //    //    customerAccountsVo.IsJointHolding = 0;
        //    //}
        //    //if (rbtnYes.Checked)
        //    //{
        //    //    customerAccountsVo.IsJointHolding = 1;
        //    //}




        //}

        //protected void txtMaturDate_DateChanged(object sender, EventArgs e)
        //{
        //    //int minTenure = 0;
        //    //int maxTenure = 0;
        //    //if (ddlSeries.SelectedValue != "0")
        //    //    fiorderBo.GetTenure(Convert.ToInt32(ddlSeries.SelectedValue), out minTenure, out maxTenure);
        //    if (hdnMintenure.Value != hdnMaxtenure.Value)
        //    {
        //        // txtMaturDate.Enabled = false;
        //        DateTime dt = txtOrderDate.SelectedDate.Value;
        //        dt = dt.AddMonths(Convert.ToInt32(hdnMaxtenure.Value));
        //        if (txtMaturDate.SelectedDate.Value > dt.Date)
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('You cant select more than.' );" + dt.Date, true);
        //            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FIxedIncomeOrderEntry", "alert('You cant select more than'  + dt.Date);", true);
        //            // txtMaturDate.SelectedDate = "01 / 01 / 0001";
        //        }

        //    }
        //    DifferenceBetDates();

        //}
        //private void DifferenceBetDates()
        //{
        //    if (txtMaturDate.SelectedDate.Value != null & txtOrderDate.SelectedDate.Value != null)
        //    {
        //        DateTime d2 = txtMaturDate.SelectedDate.Value;
        //        DateTime d1 = txtOrderDate.SelectedDate.Value;
        //        int x = 12 * (d2.Year - d1.Year) + (d1.Month - d2.Month);
        //        hdnMaxtenure.Value = x.ToString();
        //    }

        //}


        ////public void AddcLick(string[] args)
        ////{
        ////    bool blResult = false;
        ////    bool blZeroBalance = false;
        ////    bool blFileSizeExceeded = false;
        ////    hdnOrderId.Value = args.ToString() ;

        ////    if (fStorageBalance > 0)
        ////        blResult = UploadFile(out blZeroBalance, out blFileSizeExceeded);
        ////    else
        ////        blZeroBalance = false;



        ////    if (blZeroBalance)
        ////        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FIxedIncomeOrderEntry", "alert('You do not have enough space. You have only " + fStorageBalance + " MB left in your account!');", true);
        ////    else
        ////    {
        ////        if (blResult)
        ////        {

        ////            // ResetControls();
        ////            //  if (string.IsNullOrEmpty(linkAction))
        ////            // {

        ////            // }
        ////            //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "alert('Document uploaded Successfully!');", true);
        ////            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseThePopUp", " CloseWindowsPopUp();", true);
        ////        }
        ////        else
        ////        {
        ////            //if (blFileSizeExceeded)
        ////            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "alert('Sorry your Document file size exceeds the allowable 2 MB limit!');", true);
        ////            //else
        ////            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "alert('Error in uploading Document!');", true);
        ////        }
        ////    }

        ////}
        //protected void btnUploadImg_Click(object sender, EventArgs e)
        //{
        //    // bool blResult = false;
        //    // bool blZeroBalance = false;
        //    // bool blFileSizeExceeded = false;
        //    // fStorageBalance = repoBo.GetAdviserStorageValues(advisorVo.advisorId, out fMaxStorage);
        //    //if (fStorageBalance > 0)
        //    //     blResult = UploadFile(out blZeroBalance, out blFileSizeExceeded);
        //    //else
        //    //    blZeroBalance = false ;



        //    // if (blZeroBalance)
        //    //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FIxedIncomeOrderEntry", "alert('You do not have enough space. You have only " + fStorageBalance + " MB left in your account!');", true);
        //    // else
        //    // {
        //    //     if (blResult)
        //    //     {

        //    //         // ResetControls();
        //    //         //  if (string.IsNullOrEmpty(linkAction))
        //    //         // {

        //    //         // }
        //    //    //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "alert('Document uploaded Successfully!');", true);
        //    //         // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CloseThePopUp", " CloseWindowsPopUp();", true);
        //    //     }
        //    //     else
        //    //     {
        //    //         //if (blFileSizeExceeded)
        //    //         //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "alert('Sorry your Document file size exceeds the allowable 2 MB limit!');", true);
        //    //         //else
        //    //         //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "alert('Error in uploading Document!');", true);
        //    //     }
        //    // }


        //}
        //public void LoadNominees()
        //{
        //    //customerVo.CustomerId = customerid;
        //    //try
        //    //{
        //    //    dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
        //    //    dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];

        //    //    dtCustomerAssociates.Columns.Add("MemberCustomerId");
        //    //    dtCustomerAssociates.Columns.Add("AssociationId");
        //    //    dtCustomerAssociates.Columns.Add("Name");
        //    //    dtCustomerAssociates.Columns.Add("Relationship");

        //    //    foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
        //    //    {

        //    //        drCustomerAssociates = dtCustomerAssociates.NewRow();
        //    //        drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
        //    //        drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
        //    //        drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
        //    //        drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
        //    //        dtCustomerAssociates.Rows.Add(drCustomerAssociates);
        //    //    }

        //    //    if (dtCustomerAssociates.Rows.Count > 0)
        //    //    {
        //    //        gvNominees.DataSource = dtCustomerAssociates;
        //    //        gvNominees.DataBind();
        //    //        gvNominees.Visible = true;

        //    //        trNoNominee.Visible = false;
        //    //        //trNomineeCaption.Visible = true;
        //    //        trNominees.Visible = true;
        //    //    }
        //    //    else
        //    //    {
        //    //        trNoNominee.Visible = true;
        //    //        //trNomineeCaption.Visible = false;
        //    //        trNominees.Visible = false;
        //    //    }
        //    //}
        //    //catch (BaseApplicationException Ex)
        //    //{
        //    //    throw Ex;
        //    //}

        //    //catch (Exception Ex)
        //    //{
        //    //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //    //    NameValueCollection FunctionInfo = new NameValueCollection();
        //    //    FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:LoadNominees()");
        //    //    object[] objects = new object[1];
        //    //    objects[0] = customerVo;
        //    //    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //    //    exBase.AdditionalInformation = FunctionInfo;
        //    //    ExceptionManager.Publish(exBase);
        //    //    throw exBase;

        //    //}
        //}

        //private void TaxStatus()
        //{


        //}
        //private double SimpleInterest(double principal, double interestRate, double years)
        //{
        //    //A = P(1 + rt)

        //    //    double body = 1 + (interestRate / timesPerYear);
        //    double i = Convert.ToDouble(years / 12);
        //    double a = principal * (1 + interestRate * i); //* System.Math.Pow(body, exponent);
        //    return a;

        //}
        //protected void OnPayAmtTextchanged(object sender, EventArgs e)
        //{
        //    double Maturityvalue;
        //    if (hdnMaxtenure.Value == "")
        //        return;
        //    int SchemePeriod = Convert.ToInt32(hdnMaxtenure.Value);
        //    if (ddlSchemeOption.SelectedValue == "NonCummulative")
        //    {


        //        if (hdnFrequency.Value == "0" | hdnFrequency.Value == "")
        //        {
        //            txtPayAmt.Text = "";
        //            txtMatAmt.Text = "";
        //            Label11.Visible = false;
        //            return;
        //        }
        //        else
        //        {
        //            Label11.Visible = true;
        //        }

        //        if (!string.IsNullOrEmpty(txtPayAmt.Text) & txtPayAmt.Visible == true & Label18.Visible == true)
        //        {
        //            Maturityvalue = CompoundInterest(Convert.ToDouble(txtPayAmt.Text), Convert.ToDouble(hdnDefaulteInteresRate.Value) / 100, Convert.ToInt32(hdnFrequency.Value), SchemePeriod);
        //            Maturityvalue = Math.Round(Maturityvalue, 3);
        //            //Convert.ToDouble(Math.Round (SchemePeriod / 12,5))
        //            txtMatAmt.Text = Maturityvalue.ToString(); //
        //            Label11.Text = ddlFrequency.SelectedValue + "-Earned Interest" + (Convert.ToDouble(txtPayAmt.Text) - Maturityvalue).ToString();
        //        }
        //        else if (!string.IsNullOrEmpty(txtRenAmt.Text) & txtPayAmt.Visible == false & Label18.Visible == false)
        //        {
        //            Maturityvalue = CompoundInterest(Convert.ToDouble(txtRenAmt.Text), Convert.ToDouble(hdnDefaulteInteresRate.Value) / 100, Convert.ToInt32(hdnFrequency.Value), SchemePeriod);
        //            Maturityvalue = Math.Round(Maturityvalue, 3);

        //            txtMatAmt.Text = Maturityvalue.ToString();
        //            Label11.Text = ddlFrequency.SelectedValue + "-Earned Interest" + (Convert.ToDouble(txtRenAmt.Text) - Maturityvalue).ToString();

        //            // Convert.ToString(Convert.ToDouble(txtRenAmt.Text) + (Convert.ToDouble(txtRenAmt.Text) * Convert.ToDouble(hdnDefaulteInteresRate.Value) / 100));
        //        }
        //    }
        //    if (ddlSchemeOption.SelectedValue == "Cummulative")
        //    {
        //        double i = Convert.ToDouble(SchemePeriod / 12);
        //        if (!string.IsNullOrEmpty(txtPayAmt.Text) & txtPayAmt.Visible == true & Label18.Visible == true)
        //        {
        //            //  Maturityvalue = Convert.ToString(Convert.ToDouble(txtPayAmt.Text) + (Convert.ToDouble(txtPayAmt.Text) * (Convert.ToDouble(hdnDefaulteInteresRate.Value) / 100) * (i)));
        //            Maturityvalue = SimpleInterest(Convert.ToDouble(txtPayAmt.Text), (Convert.ToDouble(hdnDefaulteInteresRate.Value) / 100), SchemePeriod);

        //            Maturityvalue = Math.Round(Maturityvalue, 3);
        //            txtMatAmt.Text = Maturityvalue.ToString();
        //            Label11.Text = ddlFrequency.SelectedValue + "-Earned Interest" + (Convert.ToDouble(txtPayAmt.Text) - Maturityvalue).ToString();

        //        }
        //        else if (!string.IsNullOrEmpty(txtRenAmt.Text) & txtPayAmt.Visible == false & Label18.Visible == false)
        //        {
        //            Maturityvalue = SimpleInterest(Convert.ToDouble(txtRenAmt.Text), (Convert.ToDouble(hdnDefaulteInteresRate.Value) / 100), SchemePeriod);

        //            Maturityvalue = Math.Round(Maturityvalue, 3);
        //            txtMatAmt.Text = Maturityvalue.ToString();
        //            Label11.Text = ddlFrequency.SelectedValue + "-Earned Interest" + (Convert.ToDouble(txtRenAmt.Text) - Maturityvalue).ToString();

        //            // txtRenAmt.Text=  Convert.ToString(Convert.ToDouble(txtRenAmt.Text) + (Convert.ToDouble(txtRenAmt.Text) * Convert.ToDouble(hdnDefaulteInteresRate.Value) / 100));

        //        }
        //    }


        //}
        //private int SchemePlan()
        //{
        //    string a = ddlSeries.SelectedItem.Text;


        //    //String[] SchemePeriod = s.Split('-');


        //    //string a = SchemePeriod[1];
        //    string b = string.Empty;
        //    int val = 0;
        //    for (int i = 0; i < a.Length; i++)
        //    {
        //        if (Char.IsDigit(a[i]))
        //            b += a[i];
        //    }
        //    if (b.Length > 0)
        //        val = int.Parse(b);




        //    // int a = Convert.ToInt32(SchemePeriod[1]);

        //    //  int b = Convert.ToInt32(SchemePeriod[1]);

        //    //int c=  SchemePeriod[1].Substring(0, 2);


        //    //  int c;
        //    //  if (b != 0)
        //    //      // for 18-24
        //    //      c = a - b;
        //    //  else
        //    //      c = a;

        //    return val;


        //}
        ////private double CompoundInterest(double principal, double interestRate, int timesPerYear, double years)
        ////{
        ////    //timesPerYear = 2; // Half yearly 2 // Monthly 12 //yearly 
        ////    // (1 + r/n)
        ////    double body = 1 + (interestRate / timesPerYear);
        ////    // body = Math.Round(body, 4);
        ////    double i = Convert.ToDouble(years / 12);

        ////    // nt
        ////    double exponent = timesPerYear * i;
        ////    double a = principal * System.Math.Pow(body, exponent);
        ////    // P(1 + r/n)^nt
        ////    return a;
        ////}
        ////private bool UploadFile(out bool blZeroBalance, out bool blFileSizeExceeded)
        ////{
        ////    // We need to see if the adviser has a folder in Vault imgPath retrieved from the web.config
        ////    // Case 1: If not, then encode the adviser id and create a folder with the encoded id
        ////    // then create a folder for the repository category within the encoded folder
        ////    // then store the encoded advisor_adviserID + customerID + GUID + file name
        ////    // Case 2: If folder exists, check if the category folder exists. 
        ////    // If not then, create a folder with the category code and store the file as done above.
        ////    // If yes, then just store the file as done above.
        ////    // Once this is done, store the info in the DB with the file imgPath.

        ////    string Temppath = System.Configuration.ConfigurationManager.AppSettings["UploadCustomerProofImages"];
        ////    string UploadedFileName = String.Empty;
        ////    bool blResult = false;
        ////    blZeroBalance = false;
        ////    blFileSizeExceeded = false;
        ////    string extension = String.Empty;
        ////    float fileSize = 0;

        ////    try
        ////    {
        ////        customerVo.CustomerId = customerid;
        ////        customerVo = (CustomerVo)Session[SessionContents.CustomerVo];

        ////        // Uploading of file mandatory during button submit
        ////        if (radUploadProof.UploadedFiles.Count != 0)
        ////        {
        ////            // Put this part under a transaction scope
        ////            using (TransactionScope scope1 = new TransactionScope())
        ////            {
        ////                UploadedFile file = radUploadProof.UploadedFiles[0];
        ////                fileSize = float.Parse(file.ContentLength.ToString()) / 1048576; // Converting bytes to MB
        ////                extension = file.GetExtension();

        ////                // If space is there to upload file
        ////                //saiif (fStorageBalance >= fileSize)
        ////                //sai{
        ////                    if (fileSize <= 2)   // If upload file size is less than 2 MB then upload
        ////                    {
        ////                        // Check if directory for advisor exists, and if not then create a new directoty
        ////                        if (Directory.Exists(Temppath))
        ////                        {
        ////                            imgPath = Temppath + "\\advisor_" + rmVo.AdviserId + "\\";
        ////                            if (!System.IO.Directory.Exists(imgPath))
        ////                            {
        ////                                System.IO.Directory.CreateDirectory(imgPath);
        ////                            }
        ////                        }
        ////                        else
        ////                        {
        ////                            System.IO.Directory.CreateDirectory(Temppath);
        ////                            imgPath = Temppath + "\\advisor_" + rmVo.AdviserId + "\\";
        ////                            if (!System.IO.Directory.Exists(imgPath))
        ////                            {
        ////                                System.IO.Directory.CreateDirectory(imgPath);
        ////                            }
        ////                            //imgPath = Server.MapPath("TempCustomerProof") + "\\advisor_" + rmVo.AdviserId + "\\";
        ////                        }

        ////                        strGuid = Guid.NewGuid().ToString();
        ////                        string newFileName = SaveFileIntoServer(file, strGuid, imgPath, customerVo.CustomerId);

        ////                        // Update DB with details
        ////                        CPUVo.CustomerId = customerVo.CustomerId;
        ////                        CPUVo.ProofTypeCode = Convert.ToInt32(ddlProofType.SelectedValue);
        ////                        CPUVo.ProofCode = Convert.ToInt32(ddlProof.SelectedValue);
        ////                        //CPUVo.ProofCopyTypeCode = ddlProofCopyType.SelectedValue;
        ////                        CPUVo.ProofImage = imgPath + "\\" + newFileName;

        ////                            blResult = CreateDBReferrenceForProofUploads(CPUVo);

        ////                        if (blResult)
        ////                        {
        ////                            // Once the adding of Document is a success, then update the balance storage in advisor subscription table
        ////                         //   fStorageBalance = UpdateAdvisorStorageBalance(fileSize, 0, fStorageBalance);
        ////                            LoadImages();
        ////                        }
        ////                    }
        ////                    else
        ////                    {
        ////                        blFileSizeExceeded = true;
        ////                    }
        ////                //}
        ////                //else
        ////                //{
        ////                //    blZeroBalance = true;
        ////                //}

        ////                scope1.Complete();   // Commit the transaction scope if no errors
        ////            }
        ////        }
        ////        else
        ////        {
        ////            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a document file to upload!');", true);
        ////        }
        ////    }
        ////    catch (BaseApplicationException Ex)
        ////    {
        ////        throw Ex;
        ////    }
        ////    catch (Exception Ex)
        ////    {
        ////        //object[] objects = new object[1];
        ////        //objects[0] = CPUVo;
        ////        //PageException(objects, Ex, "ViewCustomerProofs.ascx:AddClick()");
        ////    }
        ////    return blResult;

        ////    #region Old Code

        ////    //    FileIOPermission fp = new FileIOPermission(FileIOPermissionAccess.AllAccess, imgPath);
        ////    //    PermissionSet ps = new PermissionSet(PermissionState.None);
        ////    //    ps.AddPermission(fp);
        ////    //    DirectoryInfo[] DI = new DirectoryInfo(imgPath).GetDirectories("*.*", SearchOption.AllDirectories);
        ////    //    FileInfo[] FI = new DirectoryInfo(imgPath).GetFiles("*.*", SearchOption.AllDirectories);

        ////    //    foreach (FileInfo F1 in FI)
        ////    //    {
        ////    //        DirSize += F1.Length;
        ////    //    }
        ////    //    //Converting in Mega bytes
        ////    //    DirSize = DirSize / 1048576;

        ////    //    #region Update Code 1

        ////    //    if (Session["ImagePath"] != null)
        ////    //    {
        ////    //        // If Uploaded File Exists
        ////    //        FileInfo fi = new FileInfo(Session["ImagePath"].ToString());
        ////    //        float alreadyUploadedFileSize = fi.Length;
        ////    //        alreadyUploadedFileSize = alreadyUploadedFileSize / 1048576;
        ////    //        DirSize = DirSize - alreadyUploadedFileSize;
        ////    //    }

        ////    //    #endregion

        ////    //    if ((fileSize < adviserVo.VaultSize) && (DirSize < adviserVo.VaultSize))
        ////    //    {
        ////    //        foreach (UploadedFile f in radUploadProof.UploadedFiles)
        ////    //        {
        ////    //            int l = (int)f.InputStream.Length;
        ////    //            byte[] bytes = new byte[l];
        ////    //            f.InputStream.Read(bytes, 0, l);

        ////    //            imageUploadPath = customerVo.CustomerId + "_" + guid + "_" + f.GetName();
        ////    //            if (btnSubmit.Text == "Submit")
        ////    //            {
        ////    //                // Submit part
        ////    //                if (extension != ".pdf")
        ////    //                {
        ////    //                    UploadImage(imgPath, f, imageUploadPath);
        ////    //                }
        ////    //                else
        ////    //                {
        ////    //                    f.SaveAs(imgPath + "\\" + imageUploadPath);
        ////    //                }
        ////    //            }

        ////    //            #region Update Code 2

        ////    //            else
        ////    //            {
        ////    //                if (extension != ".pdf")
        ////    //                {
        ////    //                    File.Delete(imgPath + UploadedFileName);
        ////    //                    DataTable dtGetPerticularProofs = new DataTable();
        ////    //                    if (Session["ProofID"] != null)
        ////    //                    {
        ////    //                        int ProofIdToDelete = Convert.ToInt32(Session["ProofID"].ToString());
        ////    //                        dtGetPerticularProofs = GetUploadedImagePaths(ProofIdToDelete);
        ////    //                        string imageAttachmentPath = dtGetPerticularProofs.Rows[0]["CPU_Image"].ToString();
        ////    //                        if (customerBo.DeleteCustomerUploadedProofs(customerVo.CustomerId, ProofIdToDelete))
        ////    //                        {
        ////    //                            File.Delete(imageAttachmentPath);
        ////    //                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewCustomerProofs", "loadcontrol('ViewCustomerProofs','login');", true);
        ////    //                        }
        ////    //                    }
        ////    //                }
        ////    //                f.SaveAs(imgPath + "\\" + imageUploadPath);
        ////    //            }

        ////    //            #endregion

        ////    //        }
        ////    //    }
        ////    //    else
        ////    //    {
        ////    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Sorry your Document attachment size exceeds the allowable limit..!');", true);
        ////    //    }
        ////    //}

        ////    //CPUVo.CustomerId = customerVo.CustomerId;
        ////    //CPUVo.ProofTypeCode = Convert.ToInt32(ddlProofType.SelectedValue);
        ////    //CPUVo.ProofCode = Convert.ToInt32(ddlProof.SelectedValue);
        ////    //CPUVo.ProofCopyTypeCode = ddlProofCopyType.SelectedValue;
        ////    //if (imageUploadPath == "")
        ////    //    CPUVo.ProofImage = imgPath + "\\" + UploadedFileName;
        ////    //else
        ////    //    CPUVo.ProofImage = imgPath + "\\" + imageUploadPath;
        ////    //CreateDBReferrenceForProofUploads(CPUVo);

        ////    //LoadImages();
        ////    //Session["ImagePath"] = null;

        ////    #endregion
        ////}
        ////private bool CreateDBReferrenceForProofUploads(CustomerProofUploadsVO CPUVo)
        ////{
        ////    string createOrUpdate = "";
        ////    int proofUploadID = 0;
        ////    bool bStatus = false;

        ////    //if (btnSubmit.Text.Trim().Equals("Submit"))
        ////    //{
        ////        createOrUpdate = "Submit";
        ////        if (CPUVo != null &   hdnOrderId.Value != "0")
        ////        {
        ////             customerBo.CreateCustomerOrderDocument(CPUVo, Convert.ToInt32(hdnOrderId.Value));
        ////        }
        ////    //}
        ////    //else if (btnSubmit.Text.Trim().Equals(Constants.Update.ToString()))
        ////    //{
        ////    //    createOrUpdate = Constants.Update.ToString();
        ////    //    proofUploadID = Convert.ToInt32(Session["ProofID"].ToString());
        ////    //    if (CPUVo != null)
        ////    //    {
        ////    //        bStatus = customerBo.CreateCustomersProofUploads(CPUVo, proofUploadID, createOrUpdate);
        ////    //    }
        ////    //}

        ////    return bStatus;
        ////}

        ////private void UploadImage(string imgPath, UploadedFile f, string imageUploadPath)
        ////{
        ////    TargetPath = imgPath;

        ////    UploadedFile jpeg_image_upload = f;

        ////    // Retrieve the uploaded image
        ////    original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
        ////    original_image.Save(TargetPath + imageUploadPath);//"O_" + 

        ////    int width = original_image.Width;
        ////    int height = original_image.Height;
        ////    //int new_width, new_height;

        ////    //int target_width = 140;
        ////    //int target_height = 100;

        ////    //CreateThumbnail(original_image, ref final_image, ref graphic, ref ms, jpeg_image_upload, width, height, target_width, target_height, "", true, false, out new_width, out new_height, imageUploadPath); // , out thumbnail_id

        ////    //File.Delete(TargetPath + "O_" + System.IO.imgPath.GetFileName(jpeg_image_upload.FileName));
        ////}
        ////private string SaveFileIntoServer(UploadedFile file, string strGuid, string strPath, int intCustId)
        ////{
        ////    string fileExtension = String.Empty;
        ////    fileExtension = file.GetExtension();
        ////    string strRenameFilename = file.GetName();
        ////    strRenameFilename = strRenameFilename.Replace(' ', '_');
        ////    string newFileName = intCustId + "_" + strGuid + "_" + strRenameFilename;

        ////    // Save Document file in the imgPath
        ////    if (fileExtension != ".pdf")
        ////        UploadImage(strPath, file, newFileName);
        ////    else
        ////        file.SaveAs(strPath + "\\" + newFileName);

        ////    return newFileName;
        ////}
        ////private void saveJpeg(string p, System.Drawing.Bitmap final_image, int p_3)
        ////{
        ////    // Encoder parameter for image quality
        ////    EncoderParameter qualityParam =
        ////       new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)p_3);

        ////    // Jpeg image codec
        ////    ImageCodecInfo jpegCodec =
        ////       this.getEncoderInfo("image/png");

        ////    if (jpegCodec == null)
        ////        return;

        ////    EncoderParameters encoderParams = new EncoderParameters(1);
        ////    encoderParams.Param[0] = qualityParam;

        ////    final_image.Save(p, jpegCodec, encoderParams);
        ////}
        ////private DataTable GetUploadedImagePaths(int ProofUploadId)
        ////{
        ////    customerVo.CustomerId = customerid;
        ////    DataTable dtImages = new DataTable();
        ////    try
        ////    {
        ////        dtImages = customerBo.GetCustomerUploadedProofs(customerVo.CustomerId, ProofUploadId);
        ////    }
        ////    catch (BaseApplicationException Ex)
        ////    {
        ////        throw Ex;
        ////    }
        ////    catch (Exception Ex)
        ////    {
        ////        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        ////        NameValueCollection FunctionInfo = new NameValueCollection();

        ////        FunctionInfo.Add("Method", "CustomerDao.cs:GetUploadedImagePaths()");
        ////        object[] objects = new object[1];

        ////        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        ////        exBase.AdditionalInformation = FunctionInfo;
        ////        ExceptionManager.Publish(exBase);
        ////        throw exBase;

        ////    }

        ////    return dtImages;
        ////}
        ////private void LoadImages()
        ////{
        ////    //string Temppath = "";
        ////    DataTable dtImages = new DataTable();
        ////    DataTable dtBindImages = new DataTable();
        ////    DataTable dtProofPurposes = new DataTable();
        ////    dtBindImages.Columns.Add("ProofUploadId");
        ////    dtBindImages.Columns.Add("ProofType");
        ////    dtBindImages.Columns.Add("ProofName");
        ////    dtBindImages.Columns.Add("ProofCopyType");
        ////    dtBindImages.Columns.Add("ProofImage");
        ////    dtBindImages.Columns.Add("ProofExtensions");
        ////    dtBindImages.Columns.Add("ProofFileName");

        ////    DataRow drBindImages = null;
        ////    dtImages = GetUploadedImagePaths(0);
        ////    System.Web.UI.WebControls.Image imageProof = new System.Web.UI.WebControls.Image();
        ////    System.Web.UI.WebControls.HyperLink hypPdf = new HyperLink();
        ////    string fileExt = "";

        ////    int i = 0;
        ////    //int fileCount = Directory.GetFiles(imgPath, "*.*", SearchOption.AllDirectories).Length;
        ////    string sourceDir = "";

        ////    //if (dtImages.Rows.Count > 0)
        ////    //{
        ////    //    foreach (DataRow drUploadImage in dtImages.Rows)
        ////    //    {
        ////    //        drBindImages = dtBindImages.NewRow();
        ////    //        drBindImages["ProofUploadId"] = drUploadImage["CPU_ProofUploadId"];
        ////    //        drBindImages["ProofType"] = drUploadImage["XPRT_ProofType"];
        ////    //        drBindImages["ProofName"] = drUploadImage["XP_ProofName"];
        ////    //        drBindImages["ProofCopyType"] = drUploadImage["XPCT_ProofCopyType"];
        ////    //        drBindImages["ProofImage"] = drUploadImage["CPU_Image"];
        ////    //        drBindImages["ProofExtensions"] = imgPath.GetExtension(drUploadImage["CPU_Image"].ToString());
        ////    //        drBindImages["ProofFileName"] = imgPath.GetFileName(drUploadImage["CPU_Image"].ToString());

        ////    //        dtBindImages.Rows.Add(drBindImages);
        ////    //    }

        ////    //    #region ??? Code

        ////    //    if (dtBindImages.Rows.Count > 0)
        ////    //    {
        ////    //        foreach (DataRow dr in dtBindImages.Rows)
        ////    //        {
        ////    //            string fileTempPath = dr["ProofImage"].ToString();

        ////    //            string extension = imgPath.GetExtension(fileTempPath);
        ////    //            string fileName = imgPath.GetFileName(fileTempPath);
        ////    //            Session["FileExtension"] = extension;
        ////    //        }
        ////    //    }


        ////    //  #endregion

        ////    //    repProofImages.DataSource = dtBindImages;
        ////    //    repProofImages.DataBind();

        ////    //    if (Session["Button"] != "")
        ////    //    {
        ////    //        if (Session["Button"] == "Submit")
        ////    //        {
        ////    //            if (Session["LinkAction"] != null)
        ////    //            {
        ////    //                radPOCProof.SelectedIndex = 0;
        ////    //                multiPageView.SelectedIndex = 0;
        ////    //                //this.Close();
        ////    //            }
        ////    //            else
        ////    //            {
        ////    //                radPOCProof.SelectedIndex = 1;
        ////    //                multiPageView.SelectedIndex = 1;
        ////    //            }
        ////    //        }
        ////    //        else if (Session["Button"] == "Submit & Add More")
        ////    //        {
        ////    //            radPOCProof.TabIndex = 0;
        ////    //            multiPageView.TabIndex = 0;

        ////    //            BindProofTypeDP();
        ////    //            ddlProofType.SelectedIndex = 0;
        ////    //            BindProofCopy();
        ////    //            ddlProofCopyType.SelectedIndex = 0;
        ////    //            ddlProof.SelectedIndex = 0;
        ////    //            btnDelete.Visible = false;
        ////    //        }
        ////    //    }
        ////    //}
        ////    //else
        ////    //{
        ////    //    repProofImages.DataSource = null;
        ////    //    ChangeTelerikRadTab(0);
        ////    //}
        ////}
        //////private void UploadImage(string imgPath, UploadedFile f, string imageUploadPath)
        //////{
        //////    TargetPath = imgPath;

        //////    UploadedFile jpeg_image_upload = f;

        //////    // Retrieve the uploaded image
        //////    original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
        //////    original_image.Save(TargetPath + imageUploadPath);//"O_" + 

        //////    int width = original_image.Width;
        //////    int height = original_image.Height;
        //////    //int new_width, new_height;

        //////    //int target_width = 140;
        //////    //int target_height = 100;

        //////    //CreateThumbnail(original_image, ref final_image, ref graphic, ref ms, jpeg_image_upload, width, height, target_width, target_height, "", true, false, out new_width, out new_height, imageUploadPath); // , out thumbnail_id

        //////    //File.Delete(TargetPath + "O_" + System.IO.imgPath.GetFileName(jpeg_image_upload.FileName));
        //////}
        //////private bool UploadFile(out bool blZeroBalance, out bool blFileSizeExceeded)
        //////{
        //////    // We need to see if the adviser has a folder in Vault imgPath retrieved from the web.config
        //////    // Case 1: If not, then encode the adviser id and create a folder with the encoded id
        //////    // then create a folder for the repository category within the encoded folder
        //////    // then store the encoded advisor_adviserID + customerID + GUID + file name
        //////    // Case 2: If folder exists, check if the category folder exists. 
        //////    // If not then, create a folder with the category code and store the file as done above.
        //////    // If yes, then just store the file as done above.
        //////    // Once this is done, store the info in the DB with the file imgPath.

        //////    string Temppath = System.Configuration.ConfigurationManager.AppSettings["UploadCustomerProofImages"];
        //////    string UploadedFileName = String.Empty;
        //////    bool blResult = false;
        //////    blZeroBalance = false;
        //////    blFileSizeExceeded = false;
        //////    string extension = String.Empty;
        //////    float fileSize = 0;

        //////    try
        //////    {
        //////        customerVo = (CustomerVo)Session[SessionContents.CustomerVo];

        //////        // Uploading of file mandatory during button submit
        //////        if (radUploadProof.UploadedFiles.Count != 0)
        //////        {
        //////            // Put this part under a transaction scope
        //////            using (TransactionScope scope1 = new TransactionScope())
        //////            {
        //////                UploadedFile file = radUploadProof.UploadedFiles[0];
        //////                fileSize = float.Parse(file.ContentLength.ToString()) / 1048576; // Converting bytes to MB
        //////                extension = file.GetExtension();

        //////                // If space is there to upload file
        //////                if (fStorageBalance >= fileSize)
        //////                {
        //////                    if (fileSize <= 2)   // If upload file size is less than 2 MB then upload
        //////                    {
        //////                        // Check if directory for advisor exists, and if not then create a new directoty
        //////                        if (Directory.Exists(Temppath))
        //////                        {
        //////                            imgPath = Temppath + "\\advisor_" + rmVo.AdviserId + "\\";
        //////                            if (!System.IO.Directory.Exists(imgPath))
        //////                            {
        //////                                System.IO.Directory.CreateDirectory(imgPath);
        //////                            }
        //////                        }
        //////                        else
        //////                        {
        //////                            System.IO.Directory.CreateDirectory(Temppath);
        //////                            imgPath = Temppath + "\\advisor_" + rmVo.AdviserId + "\\";
        //////                            if (!System.IO.Directory.Exists(imgPath))
        //////                            {
        //////                                System.IO.Directory.CreateDirectory(imgPath);
        //////                            }
        //////                            //imgPath = Server.MapPath("TempCustomerProof") + "\\advisor_" + rmVo.AdviserId + "\\";
        //////                        }

        //////                        strGuid = Guid.NewGuid().ToString();
        //////                        string newFileName = SaveFileIntoServer(file, strGuid, imgPath, customerVo.CustomerId);

        //////                        // Update DB with details
        //////                        CPUVo.CustomerId = customerVo.CustomerId;
        //////                        CPUVo.ProofTypeCode = Convert.ToInt32(ddlProofType.SelectedValue);
        //////                        CPUVo.ProofCode = Convert.ToInt32(ddlProof.SelectedValue);
        //////                       // CPUVo.ProofCopyTypeCode = ddlProofCopyType.SelectedValue;
        //////                        CPUVo.ProofImage = imgPath + "\\" + newFileName;

        //////                        blResult = CreateDBReferrenceForProofUploads(CPUVo);

        //////                        if (blResult)
        //////                        {
        //////                            // Once the adding of Document is a success, then update the balance storage in advisor subscription table
        //////                          //sai  fStorageBalance = UpdateAdvisorStorageBalance(fileSize, 0, fStorageBalance);
        //////                            LoadImages();
        //////                        }
        //////                    }
        //////                    else
        //////                    {
        //////                        blFileSizeExceeded = true;
        //////                    }
        //////                }
        //////                else
        //////                {
        //////                    blZeroBalance = true;
        //////                }

        //////                scope1.Complete();   // Commit the transaction scope if no errors
        //////            }
        //////        }
        //////        else
        //////        {
        //////            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a document file to upload!');", true);
        //////        }
        //////    }
        //////    catch (BaseApplicationException Ex)
        //////    {
        //////        throw Ex;
        //////    }
        //////    catch (Exception Ex)
        //////    {
        //////        object[] objects = new object[1];
        //////        //objects[0] = CPUVo;
        //////        //PageException(objects, Ex, "ViewCustomerProofs.ascx:AddClick()");
        //////    }
        //////    return blResult;


        //////}

        //private void GetCustomerAssociates(int customerid)
        //{
        //    //DataSet dsAssociates = fiorderBo.GetCustomerAssociates(customerid);
        //    //gvAssociation.DataSource = dsAssociates.Tables[0];
        //    //gvAssociation.DataBind();
        //}
        ////protected void rgvOrderSteps_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        ////{
        ////    //DataSet dsAssociates = fiorderBo.GetCustomerAssociates(customerid);
        ////    //this.rgvCustGrid.DataSource = dsAssociates.Tables[0];
        //////}
        //private void FICategory()
        //{
        //    DataSet dsBankName = fiorderBo.GetFICategory();




        //    if (dsBankName.Tables[0].Rows.Count > 0)
        //    {

        //        ddlCategory.DataSource = dsBankName;
        //        ddlCategory.DataValueField = dsBankName.Tables[0].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
        //        ddlCategory.DataTextField = dsBankName.Tables[0].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
        //        ddlCategory.DataBind();

        //       // ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));

        //    }
        //    else
        //    {
        //        ddlCategory.Items.Clear();
        //        ddlCategory.DataSource = null;
        //        ddlCategory.DataBind();
        //        ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));
        //    }
        //}


        //private void GetFIModeOfHolding()
        //{
        //    DataSet dsDepoBank = fiorderBo.GetFIModeOfHolding();


        //    if (dsDepoBank.Tables[0].Rows.Count > 0)
        //    {

        //        ddlModeofHOldingFI.DataSource = dsDepoBank.Tables[0];
        //        ddlModeofHOldingFI.DataValueField = dsDepoBank.Tables[0].Columns["XMOH_ModeOfHoldingCode"].ToString();
        //        ddlModeofHOldingFI.DataTextField = dsDepoBank.Tables[0].Columns["XMOH_ModeOfHolding"].ToString();
        //        ddlModeofHOldingFI.DataBind();

        //        ddlModeofHOldingFI.Items.Insert(0, new ListItem("Select", "Select"));

        //    }
        //    else
        //    {
        //        ddlModeofHOldingFI.Items.Clear();
        //        ddlModeofHOldingFI.DataSource = null;
        //        ddlModeofHOldingFI.DataBind();
        //        ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));
        //    }
        //}

        //private void FIIssuer(int AdviserId)
        //{
        //    DataSet dsIssuer = fiorderBo.GetFIIssuer(AdviserId, ddlCategory.SelectedValue);
        //    if (dsIssuer.Tables[0].Rows.Count > 0)
        //    {
        //        ddlIssuer.DataSource = dsIssuer;
        //        ddlIssuer.DataValueField = dsIssuer.Tables[0].Columns["PFIIM_IssuerId"].ToString();
        //        ddlIssuer.DataTextField = dsIssuer.Tables[0].Columns["PFIIM_IssuerName"].ToString();
        //        ddlIssuer.DataBind();
        //        ddlIssuer.Items.Insert(0, new ListItem("Select", "Select"));
        //    }
        //    else
        //    {
        //        ddlIssuer.Items.Clear();
        //        ddlIssuer.DataSource = null;
        //        ddlIssuer.DataBind();
        //        ddlIssuer.Items.Insert(0, new ListItem("Select", "Select"));
        //    }
        //}
        //protected void rbtnYes_CheckedChanged(object sender, EventArgs e)
        //{
        //    //customerVo.CustomerId = customerid;

        //    //try
        //    //{
        //    //    if (rbtnYes.Checked)
        //    //    {
        //    //        ddlModeofHOldingFI.Enabled = true;

        //    //        dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
        //    //        dtCustomerAssociates.Rows.Clear();
        //    //        dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];

        //    //        dtCustomerAssociates.Columns.Clear();
        //    //        dtCustomerAssociates.Columns.Add("MemberCustomerId");
        //    //        dtCustomerAssociates.Columns.Add("AssociationId");
        //    //        dtCustomerAssociates.Columns.Add("Name");
        //    //        dtCustomerAssociates.Columns.Add("Relationship");

        //    //        foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
        //    //        {
        //    //            drCustomerAssociates = dtCustomerAssociates.NewRow();
        //    //            drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
        //    //            drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
        //    //            drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
        //    //            drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
        //    //            dtCustomerAssociates.Rows.Add(drCustomerAssociates);
        //    //        }

        //    //        if (dtCustomerAssociates.Rows.Count > 0)
        //    //        {
        //    //            trNoJointHolders.Visible = false;
        //    //            trJoinHolders.Visible = true;
        //    //            trJointHolderGrid.Visible = true;
        //    //            gvJointHoldersList.DataSource = dtCustomerAssociates;
        //    //            gvJointHoldersList.DataBind();
        //    //            gvJointHoldersList.Visible = true;
        //    //        }
        //    //        else
        //    //        {
        //    //            trNoJointHolders.Visible = true;
        //    //            trJoinHolders.Visible = false;
        //    //            trJointHolderGrid.Visible = false;
        //    //        }
        //    //        ddlModeofHOldingFI.SelectedIndex = 0;
        //    //    }
        //    //    else
        //    //    {
        //    //        ddlModeofHOldingFI.SelectedValue = "SI";
        //    //        ddlModeofHOldingFI.Enabled = false;
        //    //        trJoinHolders.Visible = false;
        //    //        trJointHolderGrid.Visible = false;
        //    //        trNoJointHolders.Visible = false;
        //    //    }
        //    //}
        //    //catch (BaseApplicationException Ex)
        //    //{
        //    //    throw Ex;
        //    //}

        //    //catch (Exception Ex)
        //    //{
        //    //    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //    //    NameValueCollection FunctionInfo = new NameValueCollection();
        //    //    FunctionInfo.Add("Method", "CustomerAccountAdd.ascx:rbtnYes_CheckedChanged()");
        //    //    object[] objects = new object[1];
        //    //    objects[0] = customerVo;
        //    //    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //    //    exBase.AdditionalInformation = FunctionInfo;
        //    //    ExceptionManager.Publish(exBase);
        //    //    throw exBase;

        //    //}
        //}
        //private void FIScheme(int AdviserId, string IssuerID)
        //{
        //    DataSet dsScheme = fiorderBo.GetFIScheme(AdviserId, IssuerID);
        //    if (dsScheme.Tables[0].Rows.Count > 0)
        //    {
        //        ddlScheme.DataSource = dsScheme;
        //        ddlScheme.DataValueField = dsScheme.Tables[0].Columns["PFISM_SchemeId"].ToString();
        //        ddlScheme.DataTextField = dsScheme.Tables[0].Columns["PFISM_SchemeName"].ToString();
        //        ddlScheme.DataBind();
        //        ddlScheme.Items.Insert(0, new ListItem("Select", "Select"));
        //    }
        //    else
        //    {
        //        ddlScheme.Items.Clear();
        //        ddlScheme.DataSource = null;
        //        ddlScheme.DataBind();
        //        ddlScheme.Items.Insert(0, new ListItem("Select", "Select"));
        //    }
        //}
        ////FISeriesDetails
        //private void FISeriesDetails(int SeriesID)
        //{
        //    DataSet dsScheme = fiorderBo.GetFISeriesDetailssDetails(SeriesID);
        //    DataTable dtSeriesDetails = dsScheme.Tables[0];
        //    string Tenure;
        //    string CouponType;

        //    if (dtSeriesDetails.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dtSeriesDetails.Rows)
        //        {
        //            Tenure = dr["PFISD_Tenure"].ToString();
        //            hdnDefaulteInteresRate.Value = dr["PFISD_defaultInterestRate"].ToString();
        //            CouponType = dr["PFISD_CouponType"].ToString();
        //            txtSeries.Text = "Tenure-" + Tenure + "/" + "InterestRate-" + hdnDefaulteInteresRate.Value + "/" + "InterestType-" + CouponType;
        //            Label10.Text = txtSeries.Text;
        //        }



        //    }


        //}
        //private void FISeries(int SeriesID)
        //{
        //    DataSet dsScheme = fiorderBo.GetFISeries(SeriesID);
        //    if (dsScheme.Tables[0].Rows.Count > 0)
        //    {
        //        ddlSeries.DataSource = dsScheme;
        //        ddlSeries.DataValueField = dsScheme.Tables[0].Columns["PFISD_SeriesId"].ToString();
        //        ddlSeries.DataTextField = dsScheme.Tables[0].Columns["PFISD_SeriesName"].ToString();
        //        ddlSeries.DataBind();
        //        ddlSeries.Items.Insert(0, new ListItem("Select", "Select"));
        //    }
        //    else
        //    {
        //        ddlSeries.Items.Clear();
        //        ddlSeries.DataSource = null;
        //        ddlSeries.DataBind();
        //        ddlSeries.Items.Insert(0, new ListItem("Select", "Select"));
        //    }
        //}
        ////private void BindBank(int customerId)
        ////{
        //    //DataSet dsBankName = mfOrderBo.GetCustomerBank(customerId);
        //    //if (dsBankName.Tables[0].Rows.Count > 0)
        //    //{
        //    //    ddlBankName.DataSource = dsBankName;
        //    //    ddlBankName.DataValueField = dsBankName.Tables[0].Columns["CB_CustBankAccId"].ToString();
        //    //    ddlBankName.DataTextField = dsBankName.Tables[0].Columns["WERPBM_BankName"].ToString();
        //    //    ddlBankName.DataBind();
        //    //    ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));
        //    //}
        //    //else
        //    //{
        //    //    ddlBankName.Items.Clear();
        //    //    ddlBankName.DataSource = null;
        //    //    ddlBankName.DataBind();
        //    //    ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));
        //    //}
        ////}

        //protected void ddlSchemeOption_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlFrequency.SelectedIndex = 3;

        //    if (ddlSchemeOption.SelectedValue == "NonCummulative")
        //    {
        //        ddlFrequency.Enabled = true;

        //    }
        //    else
        //    {
        //        ddlFrequency.Enabled = false;

        //    }
        //    OnPayAmtTextchanged(this, null);
        //}

        //protected void ddlFrequency_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // Half yearly 2 // Monthly 12 //yearly 
        //    int Val = 0;
        //    if (ddlFrequency.SelectedValue == "Monthly")
        //    {
        //        Val = 12;

        //    }
        //    else if (ddlFrequency.SelectedValue == "Quarterly")
        //    {
        //        Val = 4;
        //    }
        //    else if (ddlFrequency.SelectedValue == "Yearly")
        //    {
        //        Val = 1;
        //    }
        //    else if (ddlFrequency.SelectedValue == "Hfyearly")
        //    {
        //        Val = 2;

        //    }
        //    hdnFrequency.Value = Val.ToString();
        //    OnPayAmtTextchanged(this, null);
        //}

        //protected void ddlTranstype_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlTranstype.SelectedValue == "Select")
        //    {
        //        trDepRen.Visible = false;
        //        //trPayAmt.Visible = false;
        //        txtPayAmt.Visible = false;
        //        Label18.Visible = false;

        //    }
        //    else if (ddlTranstype.SelectedValue == "Renewal")
        //    {
        //        trDepRen.Visible = true;
        //        ////trPayAmt.Visible = false;
        //        txtPayAmt.Visible = false;
        //        Label18.Visible = false;
        //    }
        //    else if (ddlTranstype.SelectedValue == "New")
        //    {
        //        txtPayAmt.Visible = true;
        //        Label18.Visible = true;
        //        //trPayAmt.Visible = true;
        //        trDepRen.Visible = false;
        //    }

        //}
        //protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCategory.SelectedIndex != 0)
        //        FIIssuer(advisorVo.advisorId);
        //    if (ddlCategory.SelectedValue == "FICG")
        //    {
        //        trSchemeOpFreq.Visible = false;
        //        trDepPaypriv.Visible = false;
        //        Label8.Text = "Capital Amount";
        //    }
        //    else
        //    {
        //        trSchemeOpFreq.Visible = true;
        //        trDepPaypriv.Visible = true;
        //        Label8.Text = "FD Amount";
        //    }
        //}

        //protected void SetEnabilityControls()
        //{

        //}

        //protected void ddlIssuer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlIssuer.SelectedIndex != 0)
        //        FIScheme(advisorVo.advisorId, ddlIssuer.SelectedValue);
        //}
        //protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlScheme.SelectedIndex >= 0)
        //        FISeries(Convert.ToInt32(ddlScheme.SelectedValue));
        //}

        //protected void ddlSeries_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int minTenure = 0;
        //    int maxTenure = 0;

        //    if (ddlSeries.SelectedIndex > 0)
        //    {
        //        FISeriesDetails(Convert.ToInt32(ddlSeries.SelectedValue));


        //        if (ddlSeries.SelectedValue != "0")
        //            fiorderBo.GetTenure(Convert.ToInt32(ddlSeries.SelectedValue), out minTenure, out maxTenure);

        //        hdnMintenure.Value = minTenure.ToString();
        //        hdnMaxtenure.Value = maxTenure.ToString();
        //    }
        //    OnPayAmtTextchanged(this, null);
        //}
        //protected void txtApplicationDt_DateChanged(object sender, EventArgs e)
        //{
        //}
        //protected void txtOrderDate_DateChanged(object sender, EventArgs e)
        //{


        //    if (hdnMintenure.Value == hdnMaxtenure.Value)
        //    {
        //        txtMaturDate.Enabled = false;
        //        DateTime dt = txtOrderDate.SelectedDate.Value;
        //        dt = dt.AddMonths(Convert.ToInt32(hdnMaxtenure.Value));
        //        txtMaturDate.SelectedDate = dt.Date;
        //    }
        //    else
        //    {


        //        txtMaturDate.Enabled = true;
        //    }
        //}

        ////protected void ddlProofType_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    //if (ddlProofType.SelectedIndex != 0)
        ////    //    BindddlProof(Convert.ToInt32(ddlProofType.SelectedValue));
        ////}

        ////protected void BindddlProof(int proofTypeSelectedValue)
        ////{
        ////    //DataTable dtDpProofsForTypes = new DataTable();
        ////    //dtDpProofsForTypes = customerBo.GetCustomerProofsForTypes(proofTypeSelectedValue);

        ////    //ddlProof.Items.Clear();
        ////    //ddlProof.SelectedValue = null;
        ////    //if (dtDpProofsForTypes.Rows.Count > 0)
        ////    //{
        ////    //    ddlProof.DataSource = dtDpProofsForTypes;
        ////    //    ddlProof.DataValueField = dtDpProofsForTypes.Columns["XP_ProofCode"].ToString();
        ////    //    ddlProof.DataTextField = dtDpProofsForTypes.Columns["XP_ProofName"].ToString();
        ////    //    ddlProof.DataBind();
        ////    //}
        ////    //ddlProof.Items.Insert(0, new ListItem("Select", "Select"));
        ////}

        private void BindProofTypeDP(DropDownList ddlProofType)
        {
            DataTable dtDpProofTypes = new DataTable();
            dtDpProofTypes = customerBo.GetCustomerProofTypes();

            //if (dtDpProofTypes.Rows.Count > 0)
            //{
            ddlProofType.DataSource = dtDpProofTypes;
            ddlProofType.DataValueField = dtDpProofTypes.Columns["XPRT_ProofTypeCode"].ToString();
            ddlProofType.DataTextField = dtDpProofTypes.Columns["XPRT_ProofType"].ToString();
            ddlProofType.DataBind();
            //}
            //ddlProofType.Items.Insert(0, new ListItem("Select", "Select"));
        }

        ////protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        ////{

        ////}
        ////protected void txtOrderDate_DateChanged(object sender, EventArgs e)
        ////{


        ////    if (hdnMintenure.Value == hdnMaxtenure.Value)
        ////    {
        ////        txtMaturDate.Enabled = false;
        ////        DateTime dt = txtOrderDate.SelectedDate.Value;
        ////        dt = dt.AddMonths(Convert.ToInt32(hdnMaxtenure.Value));
        ////        txtMaturDate.SelectedDate = dt.Date;
        ////    }
        ////    else
        ////    {


        ////        txtMaturDate.Enabled = true;
        ////    }
        ////}

        ////protected void txtApplicationDt_DateChanged(object sender, EventArgs e)
        ////{
        ////}
        ////protected void txtCustomerId_ValueChanged1(object sender, EventArgs e)
        ////{
        ////    if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
        ////    {
        ////        //trJointHoldersList.Visible = false;
        ////        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
        ////        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
        ////        // ddlAMCList.Enabled = true;
        ////        customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
        ////        Session["customerVo"] = customerVo;
        ////        //txtBranch.Text = customerVo.BranchName;
        ////        //txtRM.Text = customerVo.RMName;
        ////        //txtPanSearch.Text = customerVo.PANNum;
        ////        //hdnCustomerId.Value = txtCustomerId.Value;
        ////        //customerId = int.Parse(txtCustomerId.Value);
        ////        // if (ddlsearch.SelectedItem.Value == "2")
        ////        //     lblgetcust.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;

        ////        //= rmVo.FirstName + ' ' + rmVo.MiddleName + ' ' + rmVo.LastName;
        ////        // BindBank(customerId);
        ////        //BindPortfolioDropdown(customerId);
        ////        // ddltransType.SelectedIndex = 0;
        ////        //  BindISAList();
        ////    }
        ////}

        protected void View54ECOrderDetails(int orderid)
        {
            DataTable dtView54ECOrderDetails = fiorderBo.Get54ECOrderDetails(orderid);
            foreach (DataRow dr in dtView54ECOrderDetails.Rows)
            {
                if (!string.IsNullOrEmpty(dr["PAIC_AssetInstrumentCategoryCode"].ToString()))
                {
                    ddlCategory.SelectedValue = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                }
                FIScheme(advisorVo.advisorId, ddlCategory.SelectedValue);
                ddlScheme.SelectedValue = dr["AIM_IssueId"].ToString();
                DDLSchemeSelection();
                ddlTranstype.SelectedValue = dr["CFIOD_TransactionType"].ToString();
                TransactionTypeChanges(ddlTranstype.SelectedValue);

                txtApplicationNumber.Text = dr["CO_ApplicationNo"].ToString();
                if (!string.IsNullOrEmpty(dr["CO_ApplicationReceivedDate"].ToString()))
                {
                    txtApplicationDate.SelectedDate = Convert.ToDateTime(dr["CO_ApplicationReceivedDate"].ToString());
                }
                if (!string.IsNullOrEmpty(dr["CFIOD_Quantity"].ToString()))
                {
                    txtQty.Text = dr["CFIOD_Quantity"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["CFIOD_ExisitingDepositreceiptno"].ToString()))
                {
                    txtExistDepositreceiptno.Text = dr["CFIOD_ExisitingDepositreceiptno"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["CFIOD_RenewalAmount"].ToString()))
                {
                    txtRenAmt.Text = dr["CFIOD_RenewalAmount"].ToString();
                }
                TxtPurAmt.Text = dr["CFIOD_AmountPayable"].ToString();
                if (!string.IsNullOrEmpty(dr["XPM_PaymentModeCode"].ToString()))
                {
                    ddlPaymentMode.SelectedValue = dr["XPM_PaymentModeCode"].ToString();
                    PaymentMode(ddlPaymentMode.SelectedValue);
                }

                if (!string.IsNullOrEmpty(dr["CO_ChequeNumber"].ToString()))
                {
                    txtPaymentNumber.Text = dr["CO_ChequeNumber"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["CO_PaymentDate"].ToString()))
                {
                    txtPaymentInstDate.MinDate = Convert.ToDateTime(dr["CO_PaymentDate"].ToString());
                    txtPaymentInstDate.SelectedDate = Convert.ToDateTime(dr["CO_PaymentDate"].ToString());
                }
                if (!string.IsNullOrEmpty(dr["CO_OrderDate"].ToString()))
                {
                    txtOrderDate.SelectedDate = Convert.ToDateTime(dr["CO_OrderDate"].ToString());
                }

                if (!string.IsNullOrEmpty(dr["CEDA_DPClientId"].ToString()))
                {
                    txtDematid.Text = dr["CEDA_DPClientId"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["CEDA_DematAccountId"].ToString()))
                    BindgvFamilyAssociate(int.Parse(dr["CEDA_DematAccountId"].ToString()));
                if (!string.IsNullOrEmpty(dr["CB_CustBankAccId"].ToString()))
                {
                    ddlBankName.SelectedValue = dr["CB_CustBankAccId"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["CO_BankBranchName"].ToString()))
                {
                    txtBranchName.Text = dr["CO_BankBranchName"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["CO_ApplicationNumber"].ToString()))
                {
                    txtApplicationNumber.Text = dr["CO_ApplicationNumber"].ToString();

                }
                if (!string.IsNullOrEmpty(dr["AAC_AgentId"].ToString()))
                {
                    txtAgentId.Value = dr["AAC_AgentId"].ToString(); ;
                }
                if (!string.IsNullOrEmpty(dr["Co_OrderId"].ToString()))
                {
                    //Order No.
                    lblOrderNumber.Text = "Order No.";
                    lblGetOrderNo.Text = dr["Co_OrderId"].ToString();
                }
                if (dr["CO_IsAuthenticated"].ToString() != "True")
                {
                    rbtnAuthentication.Checked = false;

                }
                else
                {
                    rbtnAuthentication.Checked = true;
                    lblAuthenticatedBy.Text = dr["U_FirstName"].ToString();
                }
            }

        }

        protected void rbtnAuthentication_OnCheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAuthentication.Checked)
                lblAuthenticatedBy.Text = userVo.FirstName + ' ' + userVo.MiddleName + ' ' + userVo.LastName;
            else
                lblAuthenticatedBy.Text = "";
            tdlblReject.Visible = false;
            tdtxtReject.Visible = false;
            txtRejectReseaon.Text = "";
        }
        protected void rbtnReject_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAuthentication.Checked)
                lblAuthenticatedBy.Text = userVo.FirstName + ' ' + userVo.MiddleName + ' ' + userVo.LastName;
            else
                lblAuthenticatedBy.Text = "";
            tdlblReject.Visible = true;
            tdtxtReject.Visible = true;
        }
        protected void GetcustomerDetails()
        {
            CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(int.Parse(txtCustomerId.Value));
            customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
            Session["customerVo"] = customerVo;
            lblGetBranch.Text = customerVo.BranchName;
            lblgetPan.Text = customerVo.PANNum;
            ddlsearch.SelectedValue = "1";
            trCust.Visible = true;
            txtCustomerName.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;
            customerId = int.Parse(txtCustomerId.Value);
            //if (ddlsearch.SelectedItem.Value == "2")
            //    lblgetcust.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;
            OnTaxStatus();
            //ddlTax.SelectedValue = int.Parse(customerVo.TaxStatusCustomerSubTypeId.ToString());
            GetDematAccountDetails(int.Parse(txtCustomerId.Value));
            Panel1.Visible = true;
            hdnPortfolioId.Value = customerPortfolioVo.PortfolioId.ToString();
        }
        protected void BindDocument(int orderid)
        {
            if (Request.QueryString["action"] != null)
                orderid = int.Parse(Request.QueryString["orderId"].ToString());
            DataTable dtBindDocument = fiorderBo.GetDocumentDetails(orderid);
            if (dtBindDocument.Rows.Count > 0)
            {
                if (Cache["DocumentUpload" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("DocumentUpload" + advisorVo.advisorId, dtBindDocument);
                }
                else
                {
                    Cache.Remove("DocumentUpload" + advisorVo.advisorId);
                    Cache.Insert("DocumentUpload" + advisorVo.advisorId, dtBindDocument);
                }
                gvUploadDocument.DataSource = dtBindDocument;
                gvUploadDocument.DataBind();
            }
            else
            {
                Cache.Remove("DocumentUpload" + advisorVo.advisorId);
                Cache.Insert("DocumentUpload" + advisorVo.advisorId, dtBindDocument);
                gvUploadDocument.DataSource = dtBindDocument;
                gvUploadDocument.DataBind();
            }

        }
        protected void gvUploadDocument_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlProofType = (DropDownList)gefi.FindControl("ddlProofType");
                RadUpload radUploadProof = (RadUpload)gefi.FindControl("radUploadProof");
                BindProofTypeDP(ddlProofType);
                //BindCustomerSIPRule(ddlAlert);
            }
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                //GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                //DropDownList ddlProofType = (DropDownList)editedItem.FindControl("ddlProofType");
                //RadUpload radUploadProof = (RadUpload)editedItem.FindControl("radUploadProof");

                //BindProofTypeDP(ddlProofType);
                //string filename = gvUploadDocument.MasterTableView.DataKeyValues[e.Item.ItemIndex]["COD_image"].ToString();
                ////radUploadProof.t
                //if (ddlProofType.Items.Count > 0)
                //    ddlProofType.SelectedItem.Text = gvUploadDocument.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XPRT_ProofType"].ToString();
            }
        }
        protected void gvUploadDocument_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtBindDocument = new DataTable();
            dtBindDocument = (DataTable)Cache["DocumentUpload" + advisorVo.advisorId];
            if (dtBindDocument != null)
            {
                gvUploadDocument.DataSource = dtBindDocument;
            }
        }
        protected void gvUploadDocument_OnItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                DropDownList ddlProofType = (DropDownList)gridEditableItem.FindControl("ddlProofType");
                RadUpload radUploadProof = (RadUpload)gridEditableItem.FindControl("radUploadProof");
                AddClick(radUploadProof, ddlProofType);
            }
            if (e.CommandName == "download_file" & e.Item is GridDataItem && e.Item.ItemIndex != -1)
            {
                string filename = gvUploadDocument.MasterTableView.DataKeyValues[e.Item.ItemIndex]["COD_image"].ToString();
                if (filename == string.Empty)
                    return;
                string path = Server.MapPath(strRepositoryPath) + "\\advisor_" + advisorVo.advisorId + "\\54EC_OrderDocuments" + "\\" + filename;
                byte[] bts = System.IO.File.ReadAllBytes(path);
                Response.Clear();
                Response.ClearHeaders();
                Response.AddHeader("Content-Type", "Application/octet-stream");
                Response.AddHeader("Content-Length", bts.Length.ToString());
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.BinaryWrite(bts);
                Response.Flush();
                Response.End();
            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                int documentsID = int.Parse(gvUploadDocument.MasterTableView.DataKeyValues[e.Item.ItemIndex]["COD_DocumentId"].ToString());
                fiorderBo.DeleteDocuments(documentsID);
            }
            if (ViewState["orderno"] != null)
                BindDocument(int.Parse(ViewState["orderno"].ToString()));
            else
                BindDocument(orderId);

        }
        private void RadDateControlBusinessDateValidation(ref RadDatePicker rdtp, DateTime dtDate)
        {
            DataTable dtTradaDate = mfOrderBo.GetTradeDateList(dtDate);

            DateTime dtMinDate = Convert.ToDateTime(dtTradaDate.Compute("min(WTD_Date)", string.Empty));
            DateTime dtMaxDate = Convert.ToDateTime(dtTradaDate.Compute("max(WTD_Date)", string.Empty));

            rdtp.MinDate = dtMinDate;
            rdtp.MaxDate = dtMaxDate;
            DateTime dtTempIncrement;

            while (dtMinDate < dtMaxDate)
            {
                dtTempIncrement = dtMinDate.AddDays(1);

                DataRow[] foundRows = dtTradaDate.Select(String.Format("WTD_Date='{0}'", dtTempIncrement.ToString("O")));
                //dtTradaDate.Select("CONVERT(VARCHAR,WTD_Date,103)='" + dtTempIncrement.ToShortDateString() + "'");
                if (foundRows.Count() == 0)
                {
                    RadCalendarDay holiday = new RadCalendarDay();
                    holiday.Date = dtTempIncrement.Date;
                    holiday.IsSelectable = false;
                    holiday.IsDisabled = true;
                    rdtp.Calendar.SpecialDays.Add(holiday);
                }

                dtMinDate = dtTempIncrement;
            }

        }
        protected void txtPaymentInstDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {

            if (!string.IsNullOrEmpty(txtPaymentInstDate.SelectedDate.ToString()))
            {
                RadDateControlBusinessDateValidation(ref txtOrderDate, DateTime.Parse(txtPaymentInstDate.SelectedDate.ToString()));
            }
        }
        protected void openpopupAddCustomer_Click(object sender, EventArgs e)
        {
            radCustomApp.VisibleOnPageLoad = true;
            radCustomApp.Visible = true;
            BindRMforBranchDropdown(0, 0);
            BindListBranch();
            lblPanDuplicate.Visible = false;
            rbtnIndividual.Checked = true;
            trNonIndividualName.Visible = false;
            txtPanNumber.Text = "";
            txtFirstName.Text = "";
            txtPanNumber.Text = txtPansearch.Text;
            txtMobileNumber.Text = "";
            txtEmail.Text = "";
            //txtMiddleName.Text="";
            //txtLastName.Text="";
            ddlSalutation.SelectedIndex = 0;
            txtCompanyName.Text = "";
            rbtnIndividual.Checked = true;
            rbtnNonIndividual.Checked = false;
            trIndividualName.Visible = true;
            //trSalutation.Visible = true;
            trNonIndividualName.Visible = false;
            BindSubTypeDropDown(1001);
            ddlCustomerSubType.SelectedValue = "0";
        }
        protected void rbtnIndividual_CheckedChanged(object sender, EventArgs e)
        {
            BindSubTypeDropDown(1001);
            BindRMforBranchDropdown(0, 0);
            BindListBranch();
            radCustomApp.Visible = true;
            //trSalutation.Visible = true;
        }

        private void BindSubTypeDropDown(int lookupParentId)
        {
            CommonLookupBo commonLookupBo = new CommonLookupBo();
            DataTable dtCustomerTaxSubTypeLookup = new DataTable();
            try
            {
                dtCustomerTaxSubTypeLookup = commonLookupBo.GetWERPLookupMasterValueList(2000, lookupParentId);
                ddlCustomerSubType.DataSource = dtCustomerTaxSubTypeLookup;
                ddlCustomerSubType.DataTextField = "WCMV_Name";
                ddlCustomerSubType.DataValueField = "WCMV_LookupId";
                ddlCustomerSubType.DataBind();
                ddlCustomerSubType.Items.Insert(0, new ListItem("--SELECT--", "0"));
                trIndividualName.Visible = true;
                trNonIndividualName.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerType.ascx:rbtnIndividual_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void rbtnNonIndividual_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BindSubTypeDropDown(1002);
                BindListBranch();
                BindRMforBranchDropdown(0, 0);
                radCustomApp.Visible = true;
                trIndividualName.Visible = false;
                trNonIndividualName.Visible = true;
                //trSalutation.Visible = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerType.ascx:rbtnNonIndividual_CheckedChanged()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public bool Validation1()
        {
            bool result = true;
            int adviserId = ((AdvisorVo)Session["advisorVo"]).advisorId;
            try
            {
                if (customerBo.PANNumberDuplicateCheck(adviserId, txtPanNumber.Text.ToString(), customerVo.CustomerId))
                {
                    result = false;
                    lblPanDuplicate.Visible = true;
                    radCustomApp.Visible = true;
                }
                else
                {
                    lblPanDuplicate.Visible = false;
                }
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
        protected void btnCustomerSubmit_Click(object sender, EventArgs e)
        {
            List<int> customerIds = null;
            try
            {

                Nullable<DateTime> dt = new DateTime();
                customerIds = new List<int>();
                lblPanDuplicate.Visible = false;
                if (Validation1())
                {
                    lblPanDuplicate.Visible = false;
                    userVo = new UserVo();
                    if (rbtnIndividual.Checked)
                    {
                        rmVo = (RMVo)Session["rmVo"];
                        tempUserVo = (UserVo)Session["userVo"];
                        customerVo.RmId = rmVo.RMId;
                        if (customerVo.RmId == rmVo.RMId)
                        {
                            customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue.ToString());
                        }
                        else
                        {
                            customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue);
                        }
                        customerVo.Type = "IND";

                        customerVo.TaxStatusCustomerSubTypeId = Int16.Parse(ddlCustomerSubType.SelectedValue.ToString());
                        //customerVo.IsRealInvestor = chkRealInvestor.Checked;
                        customerVo.FirstName = txtFirstName.Text.ToString();
                        //customerVo.MiddleName = txtMiddleName.Text.ToString();
                        //customerVo.LastName = txtLastName.Text.ToString();
                        //if (ddlSalutation.SelectedIndex == 0)
                        //{
                        //    customerVo.Salutation = "";
                        //}
                        //else
                        //{
                        //    customerVo.Salutation = ddlSalutation.SelectedValue.ToString();
                        //}

                        userVo.FirstName = txtFirstName.Text.ToString();
                        //userVo.MiddleName = txtMiddleName.Text.ToString();
                        //userVo.LastName = txtLastName.Text.ToString();
                    }
                    else if (rbtnNonIndividual.Checked)
                    {
                        rmVo = (RMVo)Session["rmVo"];
                        tempUserVo = (UserVo)Session["userVo"];
                        customerVo.RmId = rmVo.RMId;
                        customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue.ToString());
                        if (customerVo.RmId == rmVo.RMId)
                        {
                            customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue.ToString());
                        }
                        else
                        {
                            customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue);
                        }
                        customerVo.Type = "NIND";

                        customerVo.TaxStatusCustomerSubTypeId = Int16.Parse(ddlCustomerSubType.SelectedValue.ToString());
                        //customerVo.IsRealInvestor = chkRealInvestor.Checked;
                        customerVo.CompanyName = txtCompanyName.Text.ToString();
                        customerVo.FirstName = txtCompanyName.Text.ToString();
                        userVo.FirstName = txtCompanyName.Text.ToString();
                    }
                    if (customerVo.BranchId == rmVo.BranchId)
                    {
                        customerVo.BranchId = int.Parse(ddlAdviserBranchList.SelectedValue);
                    }
                    else
                    {
                        customerVo.BranchId = int.Parse(ddlAdviserBranchList.SelectedValue);
                    }
                    customerVo.PANNum = txtPanNumber.Text.ToString();

                    customerVo.Dob = DateTime.MinValue;
                    customerVo.RBIApprovalDate = DateTime.MinValue;
                    customerVo.CommencementDate = DateTime.MinValue;
                    customerVo.RegistrationDate = DateTime.MinValue;
                    customerVo.Adr1State = null;
                    customerVo.Adr2State = null;
                    customerVo.ProfilingDate = DateTime.Today;
                    customerVo.UserId = userVo.UserId;
                    //userVo.Email = txtEmail.Text.ToString();
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PortfolioName = "MyPortfolio";
                    customerVo.ViaSMS = 1;
                    customerIds = customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, tempUserVo.UserId);
                    Session["Customer"] = "Customer";
                    int customerid = customerIds[1];
                    txtCustomerId.Value = customerid.ToString();
                    GetcustomerDetails();
                    ViewState["customerID"] = customerIds[1];

                    if (customerIds != null)
                    {

                        CustomerFamilyVo familyVo = new CustomerFamilyVo();
                        CustomerFamilyBo familyBo = new CustomerFamilyBo();
                        familyVo.AssociateCustomerId = customerIds[1];
                        familyVo.CustomerId = customerIds[1];
                        familyVo.Relationship = "SELF";
                        familyBo.CreateCustomerFamily(familyVo, customerIds[1], userVo.UserId);
                        //trCustomerAdd.Visible = false;
                        //DataTable dtcustomer = OfflineIPOOrderBo.GetAddedCustomer(customerid);
                        //foreach (DataRow dr in dtcustomer.Rows)
                        //{
                        //    lblgetPan.Text = dr["C_PANNum"].ToString();
                        //    txtCustomerName.Text = dr["CustomerName"].ToString();
                        //    lblGetBranch.Text = dr["AB_BranchName"].ToString();
                        //}

                    }

                    Response.Write(@"<script language='javascript'>alert('Customer inserted successfully');</script>");
                    //Response.Write("<script>window.close();</" + "script>");
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
                FunctionInfo.Add("Method", "CustomerType.ascx:btnSubmit_Click()");
                object[] objects = new object[5];
                objects[0] = customerIds;
                objects[1] = customerVo;
                objects[2] = rmVo;
                objects[3] = userVo;
                objects[4] = customerPortfolioVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        {


            if (advisorVo.A_AgentCodeBased != 1)
            {
                DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlAdviseRMList.DataSource = ds.Tables[0];
                    ddlAdviseRMList.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                    ddlAdviseRMList.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
                    ddlAdviseRMList.DataBind();

                }
                else
                {
                    if (!IsPostBack)
                    {
                        ddlAdviseRMList.Items.Insert(0, new ListItem("Select", "Select"));
                        CompareValidator2.ValueToCompare = "Select";
                        CompareValidator2.ErrorMessage = "Please select a RM";

                    }
                    else
                    {
                        if (rbtnNonIndividual.Checked == true)
                        {
                            if ((IsPostBack) && (ddlAdviserBranchList.SelectedIndex == 0))
                            {
                                ddlAdviseRMList.Items.Clear();
                                ddlAdviseRMList.Items.Insert(0, new ListItem("Select", "Select"));
                                CompareValidator2.ValueToCompare = "Select";
                                CompareValidator2.ErrorMessage = "Please select a RM";
                            }
                            else
                            {
                                ddlAdviseRMList.Items.Clear();
                                ddlAdviseRMList.Items.Remove("Select");
                                ddlAdviseRMList.Items.Insert(0, new ListItem("No RM Available", "No RM Available"));
                                CompareValidator2.ValueToCompare = "No RM Available";
                                CompareValidator2.ErrorMessage = "Cannot Add Customer Without a RM";


                            }
                        }
                        else
                        {
                            if ((IsPostBack) && (ddlAdviserBranchList.SelectedIndex == 0))
                            {
                                ddlAdviseRMList.Items.Clear();
                                ddlAdviseRMList.Items.Insert(0, new ListItem("Select", "Select"));
                                CompareValidator2.ValueToCompare = "Select";
                                CompareValidator2.ErrorMessage = "Please select a RM";
                            }
                            else
                            {
                                ddlAdviseRMList.Items.Clear();
                                ddlAdviseRMList.Items.Remove("Select");
                                ddlAdviseRMList.Items.Insert(0, new ListItem("No RM Available", "No RM Available"));
                                CompareValidator2.ValueToCompare = "No RM Available";
                                CompareValidator2.ErrorMessage = "Cannot Add Customer Without a RM";
                            }
                        }
                    }
                }
            }
            else
            {
                DataSet Rmds = new DataSet();

                Rmds = advisorBranchBo.GetRMRoleForAgentBased(advisorVo.advisorId);

                if (Rmds.Tables[0].Rows.Count > 0)
                {
                    ddlAdviseRMList.DataSource = Rmds;
                    ddlAdviseRMList.DataValueField = Rmds.Tables[0].Columns["RmID"].ToString();
                    ddlAdviseRMList.DataTextField = Rmds.Tables[0].Columns["RMName"].ToString();
                    ddlAdviseRMList.DataBind();
                    ddlAdviseRMList.Enabled = false;
                }
                else
                {
                    ddlAdviseRMList.Enabled = false;
                }
            }

        }
        private void BindListBranch()
        {
            if (advisorVo.A_AgentCodeBased != 1)
            {
                UploadCommonBo uploadCommonBo = new UploadCommonBo();
                DataSet ds = uploadCommonBo.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlAdviserBranchList.DataSource = ds.Tables[0];
                    ddlAdviserBranchList.DataTextField = "AB_BranchName";
                    ddlAdviserBranchList.DataValueField = "AB_BranchId";
                    //ddlAdviserBranchList.SelectedValue = "1339";
                    ddlAdviserBranchList.DataBind();
                    ddlAdviserBranchList.Items.Insert(0, new ListItem("Select", "Select"));
                }
                else
                {
                    ddlAdviserBranchList.Items.Insert(0, new ListItem("No Branches Available to Associate", "No Branches Available to Associate"));
                    //ddlAdviserBranchList_CompareValidator2.ValueToCompare = "No Branches Available to Associate";
                    //ddlAdviserBranchList_CompareValidator2.ErrorMessage = "Cannot Add Customer Without a Branch";
                }
            }
            else
            {
                DataSet BMDs = new DataSet();
                DataTable BMList = new DataTable();
                //BMList.Columns.Add("AB_BranchId");
                //BMList.Columns.Add("AB_BranchName");
                BMDs = advisorBranchBo.GetBMRoleForAgentBased(advisorVo.advisorId);

                if (BMDs.Tables[0].Rows.Count > 0)
                {
                    ddlAdviserBranchList.DataSource = BMDs;
                    ddlAdviserBranchList.DataValueField = BMDs.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlAdviserBranchList.DataTextField = BMDs.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlAdviserBranchList.DataBind();
                    ddlAdviserBranchList.Enabled = false;
                }
                else
                {
                    ddlAdviseRMList.Enabled = false;
                }

            }
        }

    }
}
