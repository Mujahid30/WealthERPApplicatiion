using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WealthERP.Base;
using BoCommon;
using System.Configuration;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoCustomerProfiling;
using VoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;


namespace WealthERP.CustomerAssetDetails
{
    public partial class CustomerGovtSaving : System.Web.UI.UserControl
    {
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        GovtSavingsVo govtSavingsVo = new GovtSavingsVo();
        GovtSavingsBo govtSavingsBo = new GovtSavingsBo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        List<InsuranceULIPVo> insuranceUlipList = new List<InsuranceULIPVo>();
        AssetBo assetBo = new AssetBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        InsuranceBo insuranceBo = new InsuranceBo();

        DataSet dsGovtsavingGrid;
        DataSet dsCustomerAssociates;
        DataSet dsAssetSubCategories;

        DataTable dtCustomerAssociatesRaw = new DataTable();
        DataTable dtCustomerAssociates = new DataTable();
        DataTable dtModeOfHolding;
        DataRow drCustomerAssociates;
        int accountId;
        string accountNo;
        string categoryType;
        string path;
        static int portfolioId;
        string group;
        int GovtSavingNPId;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            if (portfolioId == 0)
                portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
            if (!IsPostBack)
            {
                BindPortfolioDropDown();
                BindGovtSavingGrid();

            }
        }

        private void BindGovtSavingGrid()
        {
            DataTable dtGovtsavingGrid;
            dtGovtsavingGrid = govtSavingsBo.GetGovtSavingGridDetails(portfolioId);
            if (dtGovtsavingGrid != null)
            {
                gvGovtSaving.DataSource = dtGovtsavingGrid;
                gvGovtSaving.DataBind();
                if (Cache["gvGovtSaving" + userVo.UserId + customerVo.CustomerId] == null)
                {
                    Cache.Insert("gvGovtSaving" + userVo.UserId + customerVo.CustomerId, dtGovtsavingGrid);
                }
                else
                {
                    Cache.Remove("gvGovtSaving" + userVo.UserId + customerVo.CustomerId);
                    Cache.Insert("gvGovtSaving" + userVo.UserId + customerVo.CustomerId, dtGovtsavingGrid);
                }
                gvGovtSaving.Visible = true;
            }
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
        protected void gvGovtSaving_ItemDataBound(object sender, GridItemEventArgs e)
        {

            int AccountId;
            string strModeOfHoldingCode;
            string strDebtIssuerCode;
            string strInterestBasisCode;
            string strCompoundInterestFrequencyCode;
            string strAssetInstrumentCategoryCode;
            string strInterestPayableFrequencyCode;

            if ((e.Item is GridEditFormItem) && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;               
                HtmlGenericControl divAccout = (HtmlGenericControl)editedItem.FindControl("divAccout");
                HtmlGenericControl divGovtSavings = (HtmlGenericControl)editedItem.FindControl("divGovtSavings");
                ImageButton btnImgGovtSaving = (ImageButton)editedItem.FindControl("btnImgGovtSaving");
                ImageButton btnImgAddAccount = (ImageButton)editedItem.FindControl("btnImgAddAccount");

                if (e.Item.RowIndex == -1)
                {
                    if (hdnCondition.Value != "1")
                        divGovtSavings.Visible = false;
                }
                else
                {
                    divAccout.Visible = false;
                }
            }
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                GridEditFormItem gefi = (GridEditFormItem)e.Item;

                DropDownList ddlModeOfHolding = (DropDownList)gefi.FindControl("ddlModeOfHolding");
                dtModeOfHolding = XMLBo.GetModeOfHolding(path);
                ddlModeOfHolding.DataSource = dtModeOfHolding;
                ddlModeOfHolding.DataTextField = "ModeOfHolding";
                ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
                ddlModeOfHolding.DataBind();
                ddlModeOfHolding.Items.Insert(0, new ListItem("Select", "Select"));

                DropDownList ddlCategory = (DropDownList)gefi.FindControl("ddlCategory");
                group = "GS";
                DataSet ds = assetBo.GetAssetInstrumentCategory(group);
                ddlCategory.DataSource = ds.Tables[0];
                ddlCategory.DataTextField = "PAIC_AssetInstrumentCategoryName";
                ddlCategory.DataValueField = "PAIC_AssetInstrumentCategoryCode";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));

                DropDownList ddlgovtModeofHoldings = (DropDownList)gefi.FindControl("ddlgovtModeofHoldings");
                ddlgovtModeofHoldings.DataSource = dtModeOfHolding;
                ddlgovtModeofHoldings.DataTextField = "ModeOfHolding";
                ddlgovtModeofHoldings.DataValueField = "ModeOfHoldingCode";
                ddlgovtModeofHoldings.DataBind();
                ddlgovtModeofHoldings.Items.Insert(0, new ListItem("Select a Mode of Holding", "0"));

                DropDownList ddlDebtIssuerCode = (DropDownList)gefi.FindControl("ddlDebtIssuerCode");
                DataTable dtDebtIssuerCode = XMLBo.GetDebtIssuer(path);
                ddlDebtIssuerCode.DataSource = dtDebtIssuerCode;
                ddlDebtIssuerCode.DataTextField = dtDebtIssuerCode.Columns["DebtIssuerName"].ToString();
                ddlDebtIssuerCode.DataValueField = dtDebtIssuerCode.Columns["DebtIssuerCode"].ToString();
                ddlDebtIssuerCode.DataBind();
                ddlDebtIssuerCode.Items.Insert(0, new ListItem("Select an Asset Issuer", "0"));


                DropDownList ddlInterestBasis = (DropDownList)gefi.FindControl("ddlInterestBasis");
                DataTable dtInterestBasis = XMLBo.GetInterestBasis(path);
                ddlInterestBasis.DataSource = dtInterestBasis;
                ddlInterestBasis.DataTextField = dtInterestBasis.Columns["InterestBasisType"].ToString();
                ddlInterestBasis.DataValueField = dtInterestBasis.Columns["InterestBasisCode"].ToString();
                ddlInterestBasis.DataBind();
                ddlInterestBasis.Items.Insert(0, new ListItem("Select an Interest Basis", "0"));
                ddlInterestBasis.SelectedIndex = 0;

                DropDownList ddlSimpleInterestFC = (DropDownList)gefi.FindControl("ddlSimpleInterestFC");
                DataTable dtSimpleInterestFC = XMLBo.GetFrequency(path);
                ddlSimpleInterestFC.DataSource = dtSimpleInterestFC;
                ddlSimpleInterestFC.DataTextField = dtSimpleInterestFC.Columns["Frequency"].ToString();
                ddlSimpleInterestFC.DataValueField = dtSimpleInterestFC.Columns["FrequencyCode"].ToString();
                ddlSimpleInterestFC.DataBind();
                ddlSimpleInterestFC.Items.Insert(0, new ListItem("Select a Frequency", "NA"));

                DropDownList ddlCompoundInterestFC = (DropDownList)gefi.FindControl("ddlCompoundInterestFC");
                DataTable dtCompoundInterestFC = XMLBo.GetFrequency(path);
                ddlCompoundInterestFC.DataSource = dtCompoundInterestFC;
                ddlCompoundInterestFC.DataTextField = dtCompoundInterestFC.Columns["Frequency"].ToString();
                ddlCompoundInterestFC.DataValueField = dtCompoundInterestFC.Columns["FrequencyCode"].ToString();
                ddlCompoundInterestFC.DataBind();
                ddlCompoundInterestFC.Items.Insert(0, new ListItem("Select a Frequency", "0"));

                RadGrid gvNominees = (RadGrid)gefi.FindControl("gvNominees");

                #region loadnominnes

                DataTable dtCustomerAssociates = new DataTable();
                DataTable dtNewCustomerAssociate = new DataTable();
                DataRow drCustomerAssociates;


                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                dtCustomerAssociates = dsCustomerAssociates.Tables[0];

                dtNewCustomerAssociate.Columns.Add("MemberCustomerId");
                dtNewCustomerAssociate.Columns.Add("AssociationId");
                dtNewCustomerAssociate.Columns.Add("Name");
                dtNewCustomerAssociate.Columns.Add("Relationship");

                foreach (DataRow dr in dtCustomerAssociates.Rows)
                {

                    drCustomerAssociates = dtNewCustomerAssociate.NewRow();
                    drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                    drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                    drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                    drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                    dtNewCustomerAssociate.Rows.Add(drCustomerAssociates);
                }

                if (dtNewCustomerAssociate.Rows.Count > 0)
                {

                    gvNominees.DataSource = dtNewCustomerAssociate;
                    gvNominees.DataBind();
                    gvNominees.Visible = true;

                }
                else
                {

                }

                #endregion

            }
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
            }
            string strRelationshipCode = string.Empty;
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {

                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                RadGrid gvNominees = (RadGrid)gefi.FindControl("gvNominees");

                GovtSavingNPId = int.Parse(gvGovtSaving.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CGSNP_GovtSavingNPId"].ToString());
                AccountId = int.Parse(gvGovtSaving.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CGSA_AccountId"].ToString());
                strModeOfHoldingCode = gvGovtSaving.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XMOH_ModeOfHoldingCode"].ToString();
                strDebtIssuerCode = gvGovtSaving.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XDI_DebtIssuerCode"].ToString();
                strInterestBasisCode = gvGovtSaving.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XIB_InterestBasisCode"].ToString();
                strCompoundInterestFrequencyCode = gvGovtSaving.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XF_CompoundInterestFrequencyCode"].ToString();
                strAssetInstrumentCategoryCode = gvGovtSaving.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PAIC_AssetInstrumentCategoryCode"].ToString();
                strInterestPayableFrequencyCode = gvGovtSaving.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XF_InterestPayableFrequencyCode"].ToString();

                DropDownList ddlModeOfHolding = (DropDownList)gefi.FindControl("ddlModeOfHolding");
                dtModeOfHolding = XMLBo.GetModeOfHolding(path);
                ddlModeOfHolding.DataSource = dtModeOfHolding;
                ddlModeOfHolding.DataTextField = "ModeOfHolding";
                ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
                ddlModeOfHolding.DataBind();
                ddlModeOfHolding.SelectedValue = strModeOfHoldingCode;

                DropDownList ddlCategory = (DropDownList)gefi.FindControl("ddlCategory");
                group = "GS";
                DataSet ds = assetBo.GetAssetInstrumentCategory(group);
                ddlCategory.DataSource = ds.Tables[0];
                ddlCategory.DataTextField = "PAIC_AssetInstrumentCategoryName";
                ddlCategory.DataValueField = "PAIC_AssetInstrumentCategoryCode";
                ddlCategory.DataBind();
                ddlCategory.SelectedValue = strAssetInstrumentCategoryCode;


                DropDownList ddlgovtModeofHoldings = (DropDownList)gefi.FindControl("ddlgovtModeofHoldings");
                ddlgovtModeofHoldings.DataSource = dtModeOfHolding;
                ddlgovtModeofHoldings.DataTextField = "ModeOfHolding";
                ddlgovtModeofHoldings.DataValueField = "ModeOfHoldingCode";
                ddlgovtModeofHoldings.DataBind();
                ddlgovtModeofHoldings.SelectedValue = strModeOfHoldingCode;

                DropDownList ddlDebtIssuerCode = (DropDownList)gefi.FindControl("ddlDebtIssuerCode");
                DataTable dtDebtIssuerCode = XMLBo.GetDebtIssuer(path);
                ddlDebtIssuerCode.DataSource = dtDebtIssuerCode;
                ddlDebtIssuerCode.DataTextField = dtDebtIssuerCode.Columns["DebtIssuerName"].ToString();
                ddlDebtIssuerCode.DataValueField = dtDebtIssuerCode.Columns["DebtIssuerCode"].ToString();
                ddlDebtIssuerCode.DataBind();
                ddlDebtIssuerCode.SelectedValue = strDebtIssuerCode;


                DropDownList ddlInterestBasis = (DropDownList)gefi.FindControl("ddlInterestBasis");
                DataTable dtInterestBasis = XMLBo.GetInterestBasis(path);
                ddlInterestBasis.DataSource = dtInterestBasis;
                ddlInterestBasis.DataTextField = dtInterestBasis.Columns["InterestBasisType"].ToString();
                ddlInterestBasis.DataValueField = dtInterestBasis.Columns["InterestBasisCode"].ToString();
                ddlInterestBasis.DataBind();
                ddlInterestBasis.SelectedValue = strInterestBasisCode;

                DropDownList ddlSimpleInterestFC = (DropDownList)gefi.FindControl("ddlSimpleInterestFC");
                DataTable dtSimpleInterestFC = XMLBo.GetFrequency(path);
                ddlSimpleInterestFC.DataSource = dtSimpleInterestFC;
                ddlSimpleInterestFC.DataTextField = dtSimpleInterestFC.Columns["Frequency"].ToString();
                ddlSimpleInterestFC.DataValueField = dtSimpleInterestFC.Columns["FrequencyCode"].ToString();
                ddlSimpleInterestFC.DataBind();
                ddlSimpleInterestFC.SelectedValue = strInterestPayableFrequencyCode;

                DropDownList ddlCompoundInterestFC = (DropDownList)gefi.FindControl("ddlCompoundInterestFC");
                DataTable dtCompoundInterestFC = XMLBo.GetFrequency(path);
                ddlCompoundInterestFC.DataSource = dtCompoundInterestFC;
                ddlCompoundInterestFC.DataTextField = dtCompoundInterestFC.Columns["Frequency"].ToString();
                ddlCompoundInterestFC.DataValueField = dtCompoundInterestFC.Columns["FrequencyCode"].ToString();
                ddlCompoundInterestFC.DataBind();
                ddlCompoundInterestFC.SelectedValue = strCompoundInterestFrequencyCode;


                #region loadnominnes

                DataTable dtCustomerAssociates = new DataTable();
                DataTable dtNewCustomerAssociate = new DataTable();
                DataRow drCustomerAssociates;


                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                dtCustomerAssociates = dsCustomerAssociates.Tables[0];

                dtNewCustomerAssociate.Columns.Add("MemberCustomerId");
                dtNewCustomerAssociate.Columns.Add("AssociationId");
                dtNewCustomerAssociate.Columns.Add("Name");
                dtNewCustomerAssociate.Columns.Add("Relationship");

                foreach (DataRow dr in dtCustomerAssociates.Rows)
                {

                    drCustomerAssociates = dtNewCustomerAssociate.NewRow();
                    drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                    drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                    drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                    drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                    dtNewCustomerAssociate.Rows.Add(drCustomerAssociates);
                }

                if (dtNewCustomerAssociate.Rows.Count > 0)
                {

                    gvNominees.DataSource = dtNewCustomerAssociate;
                    gvNominees.DataBind();
                    gvNominees.Visible = true;

                }
                else
                {

                }

                #endregion

            }

        }


        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {

            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
        }
        protected void rbtnYes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton radio = (RadioButton)sender;
                GridEditableItem editedItem = radio.NamingContainer as GridEditableItem;
                RadioButton rbtnYes = editedItem.FindControl("rbtnYes") as RadioButton;
                DropDownList ddlModeOfHolding = editedItem.FindControl("ddlModeOfHolding") as DropDownList;
                RadGrid gvJointHoldersList = editedItem.FindControl("gvJointHoldersList") as RadGrid;
                if (rbtnYes.Checked)
                {
                    ddlModeOfHolding.Enabled = true;

                    dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                    dtCustomerAssociates.Rows.Clear();
                    dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];

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
                        gvJointHoldersList.DataSource = dtCustomerAssociates;
                        gvJointHoldersList.DataBind();
                        gvJointHoldersList.Visible = true;
                    }
                    else
                    {
                        gvJointHoldersList.Visible = false;
                    }
                    ddlModeOfHolding.SelectedIndex = 0;
                }
                else
                {
                    ddlModeOfHolding.SelectedValue = "SI";
                    ddlModeOfHolding.Enabled = false;
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

        protected void gvGovtSaving_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtGovtsavingGrid = new DataTable();
            if (Cache["gvGovtSaving" + userVo.UserId + customerVo.CustomerId] != null)
            {
                dtGovtsavingGrid = (DataTable)Cache["gvGovtSaving" + userVo.UserId + customerVo.CustomerId];
                gvGovtSaving.DataSource = dtGovtsavingGrid;
            }
        }

        protected void gvGovtSaving_ItemCommand(object source, GridCommandEventArgs e)
        {

            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                bool isUpdated = false;
                string relationCode = string.Empty;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                e.Canceled = true; 
                //gridEditableItem.OwnerTableView.IsItemInserted = false;

                GovtSavingNPId = int.Parse(gvGovtSaving.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CGSNP_GovtSavingNPId"].ToString());
                Button btnGovtSubmit = (Button)e.Item.FindControl("btnGovtSubmit");

                DropDownList ddlDebtIssuerCode = (DropDownList)e.Item.FindControl("ddlDebtIssuerCode");
                TextBox txtCurrentValue = (TextBox)e.Item.FindControl("txtCurrentValue");
                TextBox txtDepositDate = (TextBox)e.Item.FindControl("txtDepositDate");
                TextBox txtMaturityValue = (TextBox)e.Item.FindControl("txtMaturityValue");
                TextBox txtMaturityDate = (TextBox)e.Item.FindControl("txtMaturityDate");
                TextBox txtDepositAmount = (TextBox)e.Item.FindControl("txtDepositAmount");
                TextBox txtInterstRate = (TextBox)e.Item.FindControl("txtInterstRate");
                DropDownList ddlInterestBasis = (DropDownList)e.Item.FindControl("ddlInterestBasis");
                DropDownList ddlSimpleInterestFC = (DropDownList)e.Item.FindControl("ddlSimpleInterestFC");
                DropDownList ddlCompoundInterestFC = (DropDownList)e.Item.FindControl("ddlCompoundInterestFC");
                TextBox txtInterestAmtCredited = (TextBox)e.Item.FindControl("txtInterestAmtCredited");
                TextBox txtAssetParticulars = (TextBox)e.Item.FindControl("txtAssetParticulars");
                RadioButton rbtnAccumulated = (RadioButton)e.Item.FindControl("rbtnAccumulated");
                TextBox txtSubsqntDepositAmount = (TextBox)e.Item.FindControl("txtSubsqntDepositAmount");
                TextBox txtSubsqntDepositDate = (TextBox)e.Item.FindControl("txtSubsqntDepositDate");
                DropDownList ddlDepositFrequency = (DropDownList)e.Item.FindControl("ddlDepositFrequency");
                TextBox txtRemarks = (TextBox)e.Item.FindControl("txtRemarks");
                TextBox txtAccOpenDate = (TextBox)e.Item.FindControl("txtAccOpenDate");
                TextBox txtAccountWith = (TextBox)e.Item.FindControl("txtAccountWith");
                TextBox txtAccountNumber = (TextBox)e.Item.FindControl("txtAccountNumber");


                Button btnAcctInsert = (Button)e.Item.FindControl("btnAcctInsert");
                CustomerAccountsVo newAccountVo = new CustomerAccountsVo();
                if (btnGovtSubmit.Visible == true)
                {
                    customerAccountsVo = (CustomerAccountsVo)Session["customerAccountVo"];
                    Session["govtSavingsVo"] = govtSavingsBo.GetGovtSavingsDetails(GovtSavingNPId);
                    govtSavingsVo = (GovtSavingsVo)Session["govtSavingsVo"];
                    govtSavingsVo.PortfolioId = govtSavingsVo.GoveSavingsPortfolioId;
                    if (ddlDebtIssuerCode != null && ddlDebtIssuerCode.SelectedItem.Value != string.Empty)
                        govtSavingsVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();

                    govtSavingsVo.Name = txtAssetParticulars.Text;

                    if (txtCurrentValue != null && txtCurrentValue.Text != "")
                        govtSavingsVo.CurrentValue = float.Parse(txtCurrentValue.Text);



                    if (txtDepositDate != null && txtDepositDate.Text != "")
                        govtSavingsVo.PurchaseDate = DateTime.Parse(txtDepositDate.Text.ToString());

                    if (txtMaturityValue != null && txtMaturityValue.Text != "")
                        govtSavingsVo.MaturityValue = float.Parse(txtMaturityValue.Text);
                    if (txtMaturityDate != null && txtMaturityDate.Text != "")
                        govtSavingsVo.MaturityDate = DateTime.Parse(txtMaturityDate.Text.ToString());
                    if (txtDepositAmount != null && txtDepositAmount.Text != "")
                        govtSavingsVo.DepositAmt = float.Parse(txtDepositAmount.Text);
                    if (txtInterstRate != null && txtInterstRate.Text != "")
                        govtSavingsVo.InterestRate = float.Parse(txtInterstRate.Text);
                    if (ddlInterestBasis != null)
                        govtSavingsVo.InterestBasisCode = ddlInterestBasis.SelectedItem.Value.ToString();

                    if (govtSavingsVo.InterestBasisCode == "SI")
                    {
                        govtSavingsVo.InterestPayableFrequencyCode = ddlSimpleInterestFC.SelectedItem.Value.ToString();
                        govtSavingsVo.CompoundInterestFrequencyCode = "NA";
                    }
                    else if (govtSavingsVo.InterestBasisCode == "CI")
                    {
                        govtSavingsVo.InterestPayableFrequencyCode = "NA";
                        govtSavingsVo.CompoundInterestFrequencyCode = ddlCompoundInterestFC.SelectedItem.Value.ToString();
                    }
                    else
                    {
                        govtSavingsVo.InterestBasisCode = null;
                    }

                    if (rbtnAccumulated.Checked)
                    {
                        govtSavingsVo.IsInterestAccumalated = 1;
                        if (txtInterestAmtCredited != null && txtInterestAmtCredited.Text != string.Empty)
                            govtSavingsVo.InterestAmtAccumalated = float.Parse(txtInterestAmtCredited.Text);
                        govtSavingsVo.InterestAmtPaidOut = 0;
                    }
                    else
                    {
                        govtSavingsVo.IsInterestAccumalated = 0;
                        if (txtInterestAmtCredited != null && txtInterestAmtCredited.Text != string.Empty)
                            govtSavingsVo.InterestAmtPaidOut = float.Parse(txtInterestAmtCredited.Text);
                        govtSavingsVo.InterestAmtAccumalated = 0;
                    }

                    //Post Office Recurring Deposit Account- Subsequent deposit details
                    if (govtSavingsVo.AssetInstrumentCategoryCode != null && govtSavingsVo.AssetInstrumentCategoryCode.ToString().Trim() == "GSRD")
                    {
                        if (txtSubsqntDepositAmount != null && txtSubsqntDepositAmount.Text != string.Empty)
                            govtSavingsVo.SubsqntDepositAmount = float.Parse(txtSubsqntDepositAmount.Text.Trim());
                        if (txtSubsqntDepositDate != null && txtSubsqntDepositDate.Text != string.Empty)
                            govtSavingsVo.SubsqntDepositDate = Convert.ToDateTime(txtSubsqntDepositDate.Text);

                        if (ddlDepositFrequency != null && ddlDepositFrequency.SelectedValue != string.Empty)
                            govtSavingsVo.DepositFrequencyCode = ddlDepositFrequency.SelectedValue;
                    }

                    govtSavingsVo.Remarks = txtRemarks.Text;
                    govtSavingsBo.UpdateGovtSavingsNP(govtSavingsVo, userVo.UserId);
                }
                else if (btnAcctInsert.Visible == true)
                {

                }

            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                int i = 0;
                bool blResult = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;

                Button btnAcctInsert = (Button)e.Item.FindControl("btnAcctInsert");

                //gridEditableItem.OwnerTableView.IsItemInserted = false;

                if (hdnCondition.Value != "1")
                {
                    gvGovtSaving.MasterTableView.IsItemInserted = true;
                    e.Item.Edit = true;
                    e.Canceled = true;
                    hdnCondition.Value = "1";
                    
                }
                else
                {
                    gvGovtSaving.MasterTableView.IsItemInserted = false;
                    e.Item.Edit = false;
                    e.Canceled = false;
                    hdnCondition.Value = "0";
                }

                
                
                //Account details control defined........
                TextBox txtAccountNumber = (TextBox)e.Item.FindControl("txtAccountNumber");
                DropDownList ddlCategory = (DropDownList)e.Item.FindControl("ddlCategory");
                RadioButton rbtnNo = (RadioButton)e.Item.FindControl("rbtnNo");
                RadioButton rbtnYes = (RadioButton)e.Item.FindControl("rbtnYes");
                DropDownList ddlModeOfHolding = (DropDownList)e.Item.FindControl("ddlModeOfHolding");
                TextBox txtAccountSource = (TextBox)e.Item.FindControl("txtAccountSource");
                RadGrid gvJointHoldersList = (RadGrid)e.Item.FindControl("gvJointHoldersList");
                TextBox txtAccOpenDate = (TextBox)e.Item.FindControl("txtAccOpenDate");
                TextBox txtAccountId = (TextBox)e.Item.FindControl("txtAccountId");
                TextBox txtAccountWith = (TextBox)e.Item.FindControl("txtAccountWith");
                Label lblInstrumentCategory = (Label)e.Item.FindControl("lblInstrumentCategory");


                //Govt saving control defined........


                Button btnGovtSubmit = (Button)e.Item.FindControl("btnGovtSubmit");
                DropDownList ddlDebtIssuerCode = (DropDownList)e.Item.FindControl("ddlDebtIssuerCode");
                TextBox txtCurrentValue = (TextBox)e.Item.FindControl("txtCurrentValue");
                TextBox txtDepositDate = (TextBox)e.Item.FindControl("txtDepositDate");
                TextBox txtMaturityValue = (TextBox)e.Item.FindControl("txtMaturityValue");
                TextBox txtMaturityDate = (TextBox)e.Item.FindControl("txtMaturityDate");
                TextBox txtDepositAmount = (TextBox)e.Item.FindControl("txtDepositAmount");
                TextBox txtInterstRate = (TextBox)e.Item.FindControl("txtInterstRate");
                DropDownList ddlInterestBasis = (DropDownList)e.Item.FindControl("ddlInterestBasis");
                DropDownList ddlSimpleInterestFC = (DropDownList)e.Item.FindControl("ddlSimpleInterestFC");
                DropDownList ddlCompoundInterestFC = (DropDownList)e.Item.FindControl("ddlCompoundInterestFC");
                TextBox txtInterestAmtCredited = (TextBox)e.Item.FindControl("txtInterestAmtCredited");
                TextBox txtAssetParticulars = (TextBox)e.Item.FindControl("txtAssetParticulars");
                RadioButton rbtnAccumulated = (RadioButton)e.Item.FindControl("rbtnAccumulated");
                TextBox txtSubsqntDepositAmount = (TextBox)e.Item.FindControl("txtSubsqntDepositAmount");
                TextBox txtSubsqntDepositDate = (TextBox)e.Item.FindControl("txtSubsqntDepositDate");
                DropDownList ddlDepositFrequency = (DropDownList)e.Item.FindControl("ddlDepositFrequency");
                TextBox txtRemarks = (TextBox)e.Item.FindControl("txtRemarks");
                HtmlGenericControl divGovtSavings = (HtmlGenericControl)e.Item.FindControl("divGovtSavings");
                DropDownList ddlgovtModeofHoldings = (DropDownList)e.Item.FindControl("ddlgovtModeofHoldings");

                CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
                //divGovtSavings.Visible = true;
                HtmlGenericControl divAccout = (HtmlGenericControl)e.Item.FindControl("divAccout");
                

                if (btnAcctInsert.Visible == true)
                {
                    customerAccountsVo.AssetClass = "GS";
                    customerAccountsVo.AccountNum = txtAccountNumber.Text;
                    customerAccountsVo.AssetCategory = ddlCategory.SelectedItem.Value.ToString();
                    customerAccountsVo.CustomerId = customerVo.CustomerId;
                    customerAccountsVo.PortfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString()); ;
                    if (rbtnNo.Checked)
                    {
                        customerAccountsVo.IsJointHolding = 0;
                    }
                    if (rbtnYes.Checked)
                    {
                        customerAccountsVo.IsJointHolding = 1;
                    }
                    customerAccountsVo.ModeOfHolding = ddlModeOfHolding.SelectedItem.Value.ToString();
                    customerAccountsVo.AccountSource = txtAccountSource.Text;
                    accountId = customerAccountBo.CreateCustomerGovtSavingAccount(customerAccountsVo, userVo.UserId);
                    customerAccountsVo.AccountId = accountId;

                    customerAccountAssociationVo.AccountId = accountId;
                    customerAccountAssociationVo.CustomerId = customerVo.CustomerId;
                    foreach (GridDataItem gvRow in gvJointHoldersList.Items)
                    {

                        CheckBox chk = (CheckBox)gvRow.FindControl("cbRecons");
                        if (chk.Checked)
                        {
                            i++;
                        }

                    }
                    if (i!= 0)
                    {
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a record!');", true);
                    //}
                    //else
                    //{
                        foreach (GridDataItem gdi in gvJointHoldersList.Items)
                        {
                            if (((CheckBox)gdi.FindControl("cbRecons")).Checked == true)
                            {
                                int selectedRow = gdi.ItemIndex + 1;
                                customerAccountAssociationVo.AssociationId = Convert.ToInt32(gvJointHoldersList.MasterTableView.DataKeyValues[selectedRow - 1]["AssociationId"].ToString());
                                customerAccountAssociationVo.AssociationType = "Joint Holder";
                                customerAccountBo.CreateGovtSavingsAccountAssociation(customerAccountAssociationVo, userVo.UserId);

                            }
                        }
                    }

                    if (blResult)
                    {
                        Session["customerAccountVo"] = customerAccountsVo;

                    }
                    if (customerAccountsVo.AccountOpeningDate != DateTime.MinValue)
                        txtAccOpenDate.Text = customerAccountsVo.AccountOpeningDate.ToShortDateString();
                    txtAccountId.Text = customerAccountsVo.AccountNum.ToString();
                    if (customerAccountsVo.AccountSource != null)
                        txtAccountWith.Text = customerAccountsVo.AccountSource.ToString();
                    if (customerAccountsVo.AssetCategory != null)
                        lblInstrumentCategory.Text = customerAccountsVo.AssetCategory.ToString();
                    ddlModeOfHolding.SelectedValue = customerAccountsVo.ModeOfHolding.Trim();
                    ddlgovtModeofHoldings.SelectedValue = customerAccountsVo.ModeOfHolding.Trim();
                    Session["customerAccountVo"] = customerAccountsVo;
                    divGovtSavings.Visible = true;
                    btnAcctInsert.Visible = false;
                    btnGovtSubmit.Visible = true;
                }
                else if (btnGovtSubmit.Visible == true)
                {
                    customerAccountsVo = (CustomerAccountsVo)Session["customerAccountVo"];
                    customerAccountsVo.AccountId = customerAccountsVo.AccountId;//Convert.ToInt32(txtAccountId.Text);
                    if (txtAccOpenDate != null && txtAccOpenDate.Text != string.Empty)
                        customerAccountsVo.AccountOpeningDate = Convert.ToDateTime(txtAccOpenDate.Text);

                    customerAccountsVo.AccountSource = txtAccountWith.Text;
                    govtSavingsVo.PortfolioId = customerAccountsVo.PortfolioId;
                    govtSavingsVo.AccountId = customerAccountsVo.AccountId;
                    govtSavingsVo.AssetGroupCode = customerAccountsVo.AssetClass;
                    
                    govtSavingsVo.AssetInstrumentCategoryCode = customerAccountsVo.AssetCategory;

                    if (ddlDebtIssuerCode != null && ddlDebtIssuerCode.SelectedItem.Value != string.Empty)
                        govtSavingsVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();

                    govtSavingsVo.Name = txtAssetParticulars.Text;

                    if (txtCurrentValue != null && txtCurrentValue.Text != "")
                        govtSavingsVo.CurrentValue = float.Parse(txtCurrentValue.Text);



                    if (txtDepositDate != null && txtDepositDate.Text != "")
                        govtSavingsVo.PurchaseDate = DateTime.Parse(txtDepositDate.Text.ToString());

                    if (txtMaturityValue != null && txtMaturityValue.Text != "")
                        govtSavingsVo.MaturityValue = float.Parse(txtMaturityValue.Text);
                    if (txtMaturityDate != null && txtMaturityDate.Text != "")
                        govtSavingsVo.MaturityDate = DateTime.Parse(txtMaturityDate.Text.ToString());
                    if (txtDepositAmount != null && txtDepositAmount.Text != "")
                        govtSavingsVo.DepositAmt = float.Parse(txtDepositAmount.Text);
                    if (txtInterstRate != null && txtInterstRate.Text != "")
                        govtSavingsVo.InterestRate = float.Parse(txtInterstRate.Text);
                    if (ddlInterestBasis != null)
                        govtSavingsVo.InterestBasisCode = ddlInterestBasis.SelectedItem.Value.ToString();

                    if (govtSavingsVo.InterestBasisCode == "SI")
                    {
                        govtSavingsVo.InterestPayableFrequencyCode = ddlSimpleInterestFC.SelectedItem.Value.ToString();
                        govtSavingsVo.CompoundInterestFrequencyCode = "NA";
                    }
                    else if (govtSavingsVo.InterestBasisCode == "CI")
                    {
                        govtSavingsVo.InterestPayableFrequencyCode = "NA";
                        govtSavingsVo.CompoundInterestFrequencyCode = ddlCompoundInterestFC.SelectedItem.Value.ToString();
                    }
                    else
                    {
                        govtSavingsVo.InterestBasisCode = null;
                    }

                    if (rbtnAccumulated.Checked)
                    {
                        govtSavingsVo.IsInterestAccumalated = 1;
                        if (txtInterestAmtCredited != null && txtInterestAmtCredited.Text != string.Empty)
                            govtSavingsVo.InterestAmtAccumalated = float.Parse(txtInterestAmtCredited.Text);
                        govtSavingsVo.InterestAmtPaidOut = 0;
                    }
                    else
                    {
                        govtSavingsVo.IsInterestAccumalated = 0;
                        if (txtInterestAmtCredited != null && txtInterestAmtCredited.Text != string.Empty)
                            govtSavingsVo.InterestAmtPaidOut = float.Parse(txtInterestAmtCredited.Text);
                        govtSavingsVo.InterestAmtAccumalated = 0;
                    }

                    //Post Office Recurring Deposit Account- Subsequent deposit details
                    if (customerAccountsVo.AssetCategory != null && customerAccountsVo.AssetCategory.ToString().Trim() == "GSRD")
                    {
                        if (txtSubsqntDepositAmount != null && txtSubsqntDepositAmount.Text != string.Empty)
                            govtSavingsVo.SubsqntDepositAmount = float.Parse(txtSubsqntDepositAmount.Text.Trim());
                        if (txtSubsqntDepositDate != null && txtSubsqntDepositDate.Text != string.Empty)
                            govtSavingsVo.SubsqntDepositDate = Convert.ToDateTime(txtSubsqntDepositDate.Text);

                        if (ddlDepositFrequency != null && ddlDepositFrequency.SelectedValue != string.Empty)
                            govtSavingsVo.DepositFrequencyCode = ddlDepositFrequency.SelectedValue;
                    }

                    govtSavingsVo.Remarks = txtRemarks.Text;
                    bool bResult = govtSavingsBo.CreateGovtSavingsNP(govtSavingsVo, userVo.UserId);
                    //btnAcctInsert.Visible = true;
                    //btnGovtSubmit.Visible = false;
                    BindGovtSavingGrid();
                }
                divAccout.Visible = false;
                //gridEditableItem.OwnerTableView.IsItemInserted = false;
                divGovtSavings.Visible = true;

            }
            if (e.CommandName == "Delete")
            {
                GovtSavingNPId = int.Parse(gvGovtSaving.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CGSNP_GovtSavingNPId"].ToString());
                govtSavingsBo.DeleteGovtSavingsPortfolio(GovtSavingNPId);
                BindGovtSavingGrid();
            }
           // BindGovtSavingGrid();


        }
        protected void ddlInterestBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropdown = (DropDownList)sender;
            GridEditableItem editedItem = dropdown.NamingContainer as GridEditableItem;
            DropDownList ddlInterestBasis = editedItem.FindControl("ddlInterestBasis") as DropDownList;
            HtmlTableRow trCompoundInterest = editedItem.FindControl("trCompoundInterest") as HtmlTableRow;
            HtmlTableRow trSimpleInterest = editedItem.FindControl("trSimpleInterest") as HtmlTableRow;


            if (ddlInterestBasis.SelectedItem.Value.ToString().Trim() == "CI")
            {
                trCompoundInterest.Visible = true;
                trSimpleInterest.Visible = false;

            }
            else if (ddlInterestBasis.SelectedItem.Value.ToString().Trim() == "SI")   // If Simple Interest 
            {
                trCompoundInterest.Visible = false;
                trSimpleInterest.Visible = true;
            }
            else
            {
                trCompoundInterest.Visible = false;
                trSimpleInterest.Visible = false;
            }
        }
        protected void btnImgAddAccount_Click(object sender, EventArgs e)
        {
            ImageButton imgAccount = (ImageButton)sender;
            GridEditableItem editItem = (GridEditableItem)imgAccount.Parent.NamingContainer;
            //GridEditableItem editedItem = imgAccount.NamingContainer as GridEditableItem;
            //GridEditFormItem editedItem = (GridEditFormItem)e.Item;
            HtmlContainerControl divAccout = editItem.FindControl("divAccout") as HtmlContainerControl;
            //HtmlGenericControl divAccout = (HtmlGenericControl)editedItem.FindControl("divAccout");
            HtmlContainerControl divGovtSavings = editItem.FindControl("divGovtSavings") as HtmlContainerControl;
            ImageButton btnImgGovtSaving = editItem.FindControl("btnImgGovtSaving") as ImageButton;
            //HtmlGenericControl divGovtSavings = (HtmlGenericControl)editedItem.FindControl("divGovtSavings");
            //ImageButton btnImgGovtSaving = (ImageButton)editedItem.FindControl("btnImgGovtSaving");
            divAccout.Visible = true;
            divGovtSavings.Visible = false;
            btnImgGovtSaving.Enabled = true;

        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridEditableItem editItem = (GridEditableItem)btn.Parent.NamingContainer;
            HtmlGenericControl divAccout = editItem.FindControl("divAccout") as HtmlGenericControl;
            HtmlGenericControl divGovtSavings = editItem.FindControl("divGovtSavings") as HtmlGenericControl;
            ImageButton btnImgGovtSaving = editItem.FindControl("btnImgGovtSaving") as ImageButton;
            divAccout.Visible = false;
            divGovtSavings.Visible = true;
            btnImgGovtSaving.Enabled = true;

        }

        protected void btnImgGS_Click(object sender, ImageClickEventArgs e)
        {
            gvGovtSaving.ExportSettings.OpenInNewWindow = true;
            gvGovtSaving.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvGovtSaving.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvGovtSaving.MasterTableView.ExportToExcel();
        }
    }
}