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

namespace WealthERP.CustomerPortfolio
{
    public partial class CustomerMFAccountAdd : System.Web.UI.UserControl
    {
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        ProductMFBo productMfBo = new ProductMFBo();
        AssetBo assetBo = new AssetBo();
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        DataTable dtModeOfHolding;
        DataSet dsCustomerAssociates;
        DataSet dsProductAmc;
        DataTable dtCustomerAssociatesRaw = new DataTable();
        DataTable dtCustomerAssociates = new DataTable();
        DataRow drCustomerAssociates;
        int accountId;
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        static int portfolioId;
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        string path;
        string action;


        [WebMethod]
        public void CheckTradeNoMFAvailability(string TradeAccNo, string BrokerCode, int PortfolioId)
        {
            //CustomerAccountDao checkAccDao = new CustomerAccountDao();
            //return checkAccDao.CheckTradeNoAvailability(TradeAccNo, BrokerCode, PortfolioId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];

                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                trJointHolders.Visible = false;
                trJointHoldersGrid.Visible = false;


                if (!IsPostBack)
                {
                    if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
                    {
                        if (Request.QueryString["action"].Trim() == "Edit")
                        {

                            EditFolioDetails();
                           
                        }
                        else if (Request.QueryString["action"].Trim() == "View")
                        {
                            ViewFolioDetails();
                           

                        }

                    }
                    else
                    {

                        trNominee2Header.Visible = false;
                        trJoint2Header.Visible = false;
                        ddlModeOfHolding.Enabled = false;
                        ddlModeOfHolding.SelectedValue = "SI";
                        lnkEdit.Visible = false;                        
                        rbtnNo.Checked = true;
                        BindModeOfHolding();
                        BindAMC();
                        LoadNominees();
                        btnSubmit.Visible = true;
                        btnUpdate.Visible = false;
                    }

                    //pra..
                    BindPortfolioDropDown();

                }

                //BindPortfolioDropDown();

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

        private void ViewFolioDetails()
        {
            customerAccountsVo = (CustomerAccountsVo)Session["FolioVo"];
            if(customerAccountsVo.AccountOpeningDate != DateTime.MinValue)
                txtAccountDate.Text = customerAccountsVo.AccountOpeningDate.ToShortDateString();

            txtFolioNumber.Text = customerAccountsVo.AccountNum.ToString();
            BindAMC();
            ddlProductAmc.SelectedValue = customerAccountsVo.AMCCode.ToString();
            if (customerAccountsVo.IsJointHolding == 1)
                rbtnYes.Checked = true;
            else
                rbtnNo.Checked = true;
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
            ViewState["ModeOfHolding"] = ddlModeOfHolding.SelectedValue;
            BindAssociates(customerAccountsVo);
            gvNominee2.Enabled = false;
            gvJoint2.Enabled = false;
            SetVisiblity(0);
            trJoint2Header.Visible = false;
        }

        private void BindAssociates(CustomerAccountsVo AccountVo)
        {
            DataTable dtJoinHolder = new DataTable();
            DataTable dtJoinHolderGV = new DataTable();
            DataTable dtNominees = new DataTable();
            DataTable dtNomineesGV = new DataTable();
            DataRow drJoinHolder;
            DataRow drNominees;
            try
            {
                dsCustomerAssociates = customerTransactionBo.GetMFFolioAccountAssociates(AccountVo.AccountId, customerVo.CustomerId);
                dtJoinHolder = dsCustomerAssociates.Tables[0];
                dtNominees = dsCustomerAssociates.Tables[1];
                
                if (AccountVo.IsJointHolding == 1)
                {
                    if (dtJoinHolder.Rows.Count > 0 && dtJoinHolder != null)
                    {
                        dtJoinHolderGV.Columns.Add("AssociateId");
                        dtJoinHolderGV.Columns.Add("Name");
                        dtJoinHolderGV.Columns.Add("Relationship");
                        dtJoinHolderGV.Columns.Add("Stat");


                        foreach (DataRow dr in dtJoinHolder.Rows)
                        {
                            drJoinHolder = dtJoinHolderGV.NewRow();
                            drJoinHolder[0] = dr["CA_AssociationId"].ToString();
                            drJoinHolder[1] = dr["NAME"].ToString();
                            drJoinHolder[2] = dr["XR_Relationship"].ToString();
                            drJoinHolder[3] = dr["Stat"].ToString();

                            dtJoinHolderGV.Rows.Add(drJoinHolder);

                        }
                        gvJoint2.DataSource = dtJoinHolderGV;
                        ViewState["JointHold"] = dtJoinHolderGV;
                        gvJoint2.DataBind();
                        gvJoint2.Visible = true;
                        //trJointHolders.Visible = true;
                        //trJointHoldersGrid.Visible = true;
                        gvJoint2.Columns[3].Visible = false;
                        trJoint2Header.Visible = true;
                    }
                    else
                    {
                        trJoint2Header.Visible = false;
                    }
                }


                if (dtNominees.Rows.Count > 0 && dtJoinHolder != null)
                {
                    dtNomineesGV.Columns.Add("AssociateId");
                    dtNomineesGV.Columns.Add("Name");
                    dtNomineesGV.Columns.Add("Relationship");
                    dtNomineesGV.Columns.Add("Stat");



                    foreach (DataRow dr in dtNominees.Rows)
                    {
                        drNominees = dtNomineesGV.NewRow();
                        drNominees[0] = dr["CA_AssociationId"].ToString();
                        drNominees[1] = dr["NAME"].ToString();
                        drNominees[2] = dr["XR_Relationship"].ToString();
                        drNominees[3] = dr["Stat"].ToString();

                        dtNomineesGV.Rows.Add(drNominees);

                    }
                    ViewState["Nominees"] = dtNomineesGV;
                    gvNominee2.DataSource = dtNomineesGV;
                    gvNominee2.DataBind();

                    gvNominee2.Columns[3].Visible = false;
                    gvNominee2.Visible = true;
                    trNominee2Header.Visible = true;

                }
                else
                {
                    trNominee2Header.Visible = false;
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
                txtAccountDate.Text = customerAccountsVo.AccountOpeningDate.ToShortDateString();
            else
                txtAccountDate.Text = "";
            txtFolioNumber.Text = customerAccountsVo.AccountNum.ToString();
            BindAMC();
            ddlProductAmc.SelectedValue = customerAccountsVo.AMCCode.ToString();
            if (customerAccountsVo.IsJointHolding == 1)
            {
                rbtnYes.Checked = true;
                trJoint2Header.Visible = true;

            }
            else
            {
                rbtnNo.Checked = true;
                trJoint2Header.Visible = false;
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

            ViewState["ModeOfHolding"] = ddlModeOfHolding.SelectedValue;
            BindAssociates(customerAccountsVo);
            Session["CustomerAccountVo"] = customerAccountsVo;
            gvJoint2.Enabled = true;
            gvNominee2.Enabled = true;
            SetVisiblity(1);
           
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
                lnkEdit.Visible = true;
            }
            else
            {
                txtAccountDate.Enabled = true;
                txtFolioNumber.Enabled = true;
                //ddlModeOfHolding.Enabled = true;
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
            ddlProductAmc.Items.Insert(0, new ListItem("Select an AMC Code", "Select an AMC Code"));
        }

        private void BindModeOfHolding()
        {
            dtModeOfHolding = XMLBo.GetModeOfHolding(path);
            ddlModeOfHolding.DataSource = dtModeOfHolding;
            ddlModeOfHolding.DataTextField = "ModeOfHolding";
            ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
            ddlModeOfHolding.DataBind();
            ddlModeOfHolding.Items.Insert(0, new ListItem("Select Mode of Holding", "Select Mode of Holding"));
        }

        private void BindPortfolioDropDown()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();
            //ddlPortfolio.Items.Insert(0, "Select the Portfolio");

            ddlPortfolio.SelectedValue = portfolioId.ToString();
        }

        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;

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

                    trNominees.Visible = true;
                    trNomineesGrid.Visible = true;
                }
                else
                {
                    trNominees.Visible = false;
                    trNomineesGrid.Visible = true;
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

        protected void rbtnYes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (rbtnYes.Checked)
                {

                    if ((object)(Request.QueryString["action"]) != null && Request.QueryString["action"] != "")
                    {
                        trJoint2.Visible = true;
                        if (Request.QueryString["action"].Trim() == "Edit")
                        {
                            EditFolioDetails();
                        }
                        else if (Request.QueryString["action"].Trim() == "View")
                        {
                            ViewFolioDetails();
                        }
                    }
                    else
                    {

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
                        }
                        else
                        {
                            //trJointHolders.Visible = false;
                            //trJointHoldersGrid.Visible = false;
                        }
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
                customerAccountsVo.AccountNum = txtFolioNumber.Text;
                customerAccountsVo.AssetClass = "MF";
                customerAccountsVo.CustomerId = customerVo.CustomerId;
                customerAccountsVo.PortfolioId = portfolioId;
                if (rbtnNo.Checked)
                    customerAccountsVo.IsJointHolding = 0;
                else
                    customerAccountsVo.IsJointHolding = 1;
                if(ddlModeOfHolding.SelectedValue != "Select Mode of Holding") 
                    customerAccountsVo.ModeOfHolding = ddlModeOfHolding.SelectedItem.Value.ToString();
                if(txtAccountDate.Text != "")
                    customerAccountsVo.AccountOpeningDate = DateTime.Parse(txtAccountDate.Text.Trim());
                customerAccountsVo.AMCCode = int.Parse(ddlProductAmc.SelectedItem.Value.ToString());
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
                foreach (GridViewRow gvr in this.gvNominees.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId0")).Checked == true)
                    {

                         customerAccountAssociationVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[1].ToString());
                        customerAccountAssociationVo.AssociationType = "Nominee";
                        customerAccountBo.CreateMFAccountAssociation(customerAccountAssociationVo, userVo.UserId);
                    }
                }
                if (rbtnYes.Checked)
                {
                    foreach (GridViewRow gvr in this.gvJointHoldersList.Rows)
                    {
                        if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                        {
                            customerAccountAssociationVo.AssociationId = int.Parse(gvJointHoldersList.DataKeys[gvr.RowIndex].Values[0].ToString());
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
                else if (Request.QueryString["FromPage"] == "PortfolioSystematicEntry")
                {
                    Response.Redirect("ControlHost.aspx?pageid=PortfolioSystematicEntry&Folionumber=" + customerAccountsVo.AccountNum + "&FromPage=" + "CustomerMFAccountAdd" + "&action=" + "edit", false);
                    //Response.Redirect("ControlHost.aspx?pageid=PortfolioSystematicEntry&Folionumber=" + customerAccountsVo.AccountNum + "", false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "loadcontrol('CustomerMFFolioView');", true);
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
            string TradeAccNo;
            string BrokerCode;
            int PortfolioId;
            CustomerAccountsVo newAccountVo = new CustomerAccountsVo();
            CustomerAccountAssociationVo AccountAssociationVo = new CustomerAccountAssociationVo();
            customerAccountsVo = (CustomerAccountsVo)Session["FolioVo"];
            string oldaccount;
            oldaccount = customerAccountsVo.AccountNum;
            newAccountVo.AccountNum = txtFolioNumber.Text;
            if (oldaccount == txtFolioNumber.Text)
            {

                newAccountVo.AssetClass = "MF";
                if (rbtnNo.Checked)
                    newAccountVo.IsJointHolding = 0;
                else
                    newAccountVo.IsJointHolding = 1;
                if (ddlModeOfHolding.SelectedValue != "Select Mode of Holding")
                    newAccountVo.ModeOfHoldingCode = ddlModeOfHolding.SelectedItem.Value.ToString();
                if (txtAccountDate.Text != "")
                    newAccountVo.AccountOpeningDate = DateTime.Parse(txtAccountDate.Text.Trim());
                newAccountVo.PortfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
                newAccountVo.AMCCode = int.Parse(ddlProductAmc.SelectedItem.Value.ToString());
                newAccountVo.AccountId = customerAccountsVo.AccountId;
                if (customerTransactionBo.UpdateCustomerMFFolioDetails(newAccountVo, userVo.UserId))
                {
                    customerTransactionBo.DeleteMFFolioAccountAssociates(newAccountVo.AccountId);
                    AccountAssociationVo.AccountId = newAccountVo.AccountId;
                    AccountAssociationVo.CustomerId = customerVo.CustomerId;

                    foreach (GridViewRow gvr in this.gvNominee2.Rows)
                    {
                        if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                        {

                            AccountAssociationVo.AssociationId = int.Parse(gvNominee2.DataKeys[gvr.RowIndex].Value.ToString());
                            AccountAssociationVo.AssociationType = "Nominee";
                            customerAccountBo.CreateMFAccountAssociation(AccountAssociationVo, userVo.UserId);
                        }
                    }
                    if (rbtnYes.Checked)
                    {
                        foreach (GridViewRow gvr in this.gvJoint2.Rows)
                        {
                            if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                            {

                                AccountAssociationVo.AssociationId = int.Parse(gvJoint2.DataKeys[gvr.RowIndex].Value.ToString());
                                AccountAssociationVo.AssociationType = "Joint Holder";
                                customerAccountBo.CreateMFAccountAssociation(AccountAssociationVo, userVo.UserId);
                            }
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
                    if (txtAccountDate.Text != "")
                        newAccountVo.AccountOpeningDate = DateTime.Parse(txtAccountDate.Text.Trim());
                    newAccountVo.PortfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
                    newAccountVo.AMCCode = int.Parse(ddlProductAmc.SelectedItem.Value.ToString());
                    newAccountVo.AccountId = customerAccountsVo.AccountId;
                    if (customerTransactionBo.UpdateCustomerMFFolioDetails(newAccountVo, userVo.UserId))
                    {
                        customerTransactionBo.DeleteMFFolioAccountAssociates(newAccountVo.AccountId);
                        AccountAssociationVo.AccountId = newAccountVo.AccountId;
                        AccountAssociationVo.CustomerId = customerVo.CustomerId;

                        foreach (GridViewRow gvr in this.gvNominee2.Rows)
                        {
                            if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                            {

                                AccountAssociationVo.AssociationId = int.Parse(gvNominee2.DataKeys[gvr.RowIndex].Value.ToString());
                                AccountAssociationVo.AssociationType = "Nominee";
                                customerAccountBo.CreateMFAccountAssociation(AccountAssociationVo, userVo.UserId);
                            }
                        }
                        if (rbtnYes.Checked)
                        {
                            foreach (GridViewRow gvr in this.gvJoint2.Rows)
                            {
                                if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                                {

                                    AccountAssociationVo.AssociationId = int.Parse(gvJoint2.DataKeys[gvr.RowIndex].Value.ToString());
                                    AccountAssociationVo.AssociationType = "Joint Holder";
                                    customerAccountBo.CreateMFAccountAssociation(AccountAssociationVo, userVo.UserId);
                                }
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


        protected void gvJoint2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = new DataTable();
                if ((DataTable)ViewState["JointHold"] != null)
                    dt = (DataTable)ViewState["JointHold"];
                CheckBox chkBox = e.Row.FindControl("chkId") as CheckBox;
                foreach (DataRow dr in dt.Rows)
                {
                    if (gvJoint2.DataKeys[e.Row.RowIndex][0].ToString() == dr["AssociateId"].ToString() && dr["Stat"].ToString() == "yes")
                        chkBox.Checked = true;

                }
                //if (e.Row.Cells[3].Text == "yes")
                //    chkBox.Checked = true;
                //else
                //{
                //    chkBox.Checked = false;
                //}
            }
        }

        protected void gvNominee2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = new DataTable();
                if ((DataTable)ViewState["Nominees"] != null)
                    dt = (DataTable)ViewState["Nominees"];
                CheckBox chkBox = e.Row.FindControl("chkId") as CheckBox;
                foreach (DataRow dr in dt.Rows)
                {
                    if (gvNominee2.DataKeys[e.Row.RowIndex][0].ToString() == dr["AssociateId"].ToString() && dr["Stat"].ToString()=="yes")
                        chkBox.Checked = true;
                    
                }

            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {            
            EditFolioDetails();
            btnUpdate.Visible = true;
        }

        protected void rbtnYes_CheckedChanged1(object sender, EventArgs e)
        {
            CustomerAccountsVo AccountVo = new CustomerAccountsVo();
            if (Session["CustomerAccountVo"] != null)
            {                
                AccountVo = (CustomerAccountsVo)Session["CustomerAccountVo"];
            }
            ddlModeOfHolding.Enabled = true;
            ddlModeOfHolding.SelectedIndex = 0;
            if (txtFolioNumber.Text != "" && btnUpdate.Visible == true)
            {
                if (!string.IsNullOrEmpty(ViewState["ModeOfHolding"].ToString()))
                ddlModeOfHolding.SelectedValue = ViewState["ModeOfHolding"].ToString();
                
                DataTable dtJoinHolder = new DataTable();
                DataTable dtJoinHolderGV = new DataTable();
                DataRow drJoinHolder;
                dsCustomerAssociates = customerTransactionBo.GetMFFolioAccountAssociates(AccountVo.AccountId, customerVo.CustomerId);
                dtJoinHolder = dsCustomerAssociates.Tables[0];
                if (dtJoinHolder.Rows.Count > 0 && dtJoinHolder != null)
                {
                    dtJoinHolderGV.Columns.Add("AssociateId");
                    dtJoinHolderGV.Columns.Add("Name");
                    dtJoinHolderGV.Columns.Add("Relationship");
                    dtJoinHolderGV.Columns.Add("Stat");

                    foreach (DataRow dr in dtJoinHolder.Rows)
                    {
                        drJoinHolder = dtJoinHolderGV.NewRow();
                        drJoinHolder[0] = dr["CA_AssociationId"].ToString();
                        drJoinHolder[1] = dr["NAME"].ToString();
                        drJoinHolder[2] = dr["XR_Relationship"].ToString();
                        drJoinHolder[3] = dr["Stat"].ToString();

                        dtJoinHolderGV.Rows.Add(drJoinHolder);

                    }

                    gvJoint2.DataSource = dtJoinHolderGV;
                    ViewState["JointHold"] = dtJoinHolderGV;
                    gvJoint2.DataBind();
                    trJoint2Header.Visible = true;
                    trJoint2.Visible = true;
                    if (gvJoint2.Visible == false)
                    {
                        gvJoint2.Visible = true;
                    }

                    //trJointHolders.Visible = true;
                    gvJoint2.Columns[3].Visible = false;
                    trJoint2Header.Visible = true;
                }
                else
                {
                    trJoint2Header.Visible = false;
                }
               
            }
            else
            {
                //when addding MF Folio Account 
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
                    trJointHolders.Visible = true;
                    trJointHoldersGrid.Visible = true;
                    gvJointHoldersList.DataSource = dtCustomerAssociates;
                    gvJointHoldersList.DataBind();
                }
                else
                {
                    trJointHolders.Visible = false;
                    trJointHoldersGrid.Visible = false;
                }
            }
        }

        //protected void foliobutton_click(object sender, EventArgs e)
        //{

        //}

        protected void rbtnNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlModeOfHolding.SelectedIndex != 0)
            {
                ViewState["ModeOfHolding"] = ddlModeOfHolding.SelectedValue;
            }
            ddlModeOfHolding.SelectedValue = "SI";
            ddlModeOfHolding.Enabled = false;
            trJointHolders.Visible = false;
            trJointHoldersGrid.Visible = false;
            trJoint2Header.Visible = false;
            trJoint2.Visible = false;


        }
        [WebMethod]
        public void CheckTradeNoAvailability(string TradeAccNo, string BrokerCode, int PortfolioId)
        {
            //CustomerAccountDao checkAccDao = new CustomerAccountDao();
           //return checkAccDao.CheckTradeNoAvailability(TradeAccNo, BrokerCode, PortfolioId);
        }
       

    }
}