using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using VoUser;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;
using System.Configuration;
using BoOps;
using System.Web.Script.Services;

namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioGeneralInsuranceEntry : System.Web.UI.UserControl
    {
        DataSet dsCustomerAssociates;
        DataTable dtCustomerAssociatesRaw = new DataTable();
        DataTable dtCustomerAssociates = new DataTable();
        DataRow drCustomerAssociates;
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        InsuranceBo insuranceBo = new InsuranceBo();
        GeneralInsuranceVo generalInsuranceVo = new GeneralInsuranceVo();
        string assCatCode, assCat;
        string assSubCatCode, assSubCat;
        string policyNumber;
        string action = null;
        int acntId;
        int accountId;
        int insuranceId;
        int GINetPositionId;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            customerVo = (CustomerVo)Session["customerVo"];
            DataTable dt;

            if (Request.QueryString["FromPage"] == "GIAccountAdd")
            {
                int.TryParse(Request.QueryString["AccountId"].ToString(), out acntId);
                ViewState["accountId"] = accountId = acntId;

                ViewState["assCatCode"] = assCatCode = Request.QueryString["AssCatCode"].ToString();
                ViewState["assCat"] = assCat = Request.QueryString["AssCat"].ToString();
                ViewState["assSubCatCode"] = assSubCatCode = Request.QueryString["AssSubCatCode"].ToString();
                ViewState["assSubCat"] = assSubCat = Request.QueryString["AssSubCat"].ToString();
                ViewState["policyNumber"] = policyNumber = Request.QueryString["PolicyNumber"].ToString();

                txtAssetCategory.Text = ViewState["assCat"].ToString();
                txtAssetSubCategory.Text = ViewState["assSubCat"].ToString();
                txtPolicyNumber.Text = ViewState["policyNumber"].ToString();

                trAssetGroup.Visible = false;
                lnkBtnBack.Visible = false;
                lnkBtnEdit.Visible = false;
                LoadNominees();
                DisplayAssetCategory();


            }
            cv2_txtPolicyCommencementDate.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            //CompareValidator77.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");

            if (Request.QueryString["FromPage"] == "ViewGeneralInsuranceDetails")
            {
                Session["insuranceId"] = insuranceId;
                if (Session["insuranceId"] != null)
                {
                    insuranceId = (int)Session["insuranceId"];
                }

                int.TryParse(Request.QueryString["InsuranceId"].ToString(), out insuranceId);
                // ViewState["insuranceId"] = insuranceId;
                ddlPeriodSelection.Visible = false;
                txtPolicyTerm.Visible = false;
            }

            if (!IsPostBack)
            {
                BindPolicyIssuerDropDown();
                BindFrequencyDropDown();
                BindPolicyTypeDropDown();

                if (Request.QueryString["FromPage"] == "ViewGeneralInsuranceDetails")
                {
                    action = Request.QueryString["action"].ToString();
                    Session["action"] = action;
                    Session["insuranceId"] = insuranceId;
                }

                if (Session["insuranceId"] != null && Session["action"] != null)
                {
                    insuranceId = int.Parse(Session["insuranceId"].ToString());
                    action = Session["action"].ToString();
                }
            }

            if (action == "View")
            {
              // 
                btnAddScheme.Enabled = false;
                hideControlsForViewAndEdit();
                Session["insuranceId"] = insuranceId;
                if (Session["insuranceId"] != null)
                {
                    insuranceId = (int)Session["insuranceId"];
                }

                SetControls();
                btnAssetShow.Visible = false;
                btnSubmit.Visible = false;
                EnableDisableControls(false);
            }
            else if (action == "Edit")
            {
                btnAddScheme.Enabled = true;
                hideControlsForViewAndEdit();
                btnSubmit.Text = "Update";
                btnAssetShow.Visible = true;
                lnkBtnEdit.Visible = false;
                SetControls();
            }

            if (assSubCatCode == "GIRIHM" || assSubCatCode == "GIRIPA")
            {
                showHealthInsuranceFields();
            }
            else
                hideHealthInsuranceFields();

        }

        protected void hideControlsForViewAndEdit()
        {
            lblPolicyTerm.Visible = false;
            //rdbPolicyTermDays.Visible = false;
            //rdbPolicyTermMonth.Visible = false;
            //lblDays.Visible = false;
           // txtDays.Visible = false;
           // lblMonths.Visible = false;
           // txtMonths.Visible = false;

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //double SumAssured, premiumAmount;
            double premiumAmount;
            double nomineeShare;
            long TPAContactNumber;
            bool bresult;
            try
            {
                customerVo = (CustomerVo)Session["customerVo"];
                userVo = (UserVo)Session["userVo"];

                generalInsuranceVo.AccountId = int.Parse(ViewState["accountId"].ToString());
                generalInsuranceVo.InsIssuerCode = ddlPolicyIssuer.SelectedValue;
                generalInsuranceVo.InsIssuerName = ddlPolicyIssuer.SelectedItem.Text;
                generalInsuranceVo.PolicyParticular = txtPolicyParticular.Text;
                if (txtPolicyCommencementDate.SelectedDate != DateTime.MinValue)
                    generalInsuranceVo.OriginalStartDate = Convert.ToDateTime(txtPolicyCommencementDate.SelectedDate);
                if (rdoGroupPolicyYes.Checked)
                    generalInsuranceVo.IsFamilyPolicy = 1;
                generalInsuranceVo.PolicyTypeCode = ddlTypeOfPolicy.SelectedValue;
                generalInsuranceVo.PolicyType = ddlTypeOfPolicy.SelectedItem.Text;
                //double.TryParse(hdnSumAssured.Value, out SumAssured);
                //generalInsuranceVo.SumAssured = SumAssured;
                generalInsuranceVo.SumAssured = double.Parse(txtSumAssured1.Text);
                generalInsuranceVo.TPAName = txtTPA.Text;
                long.TryParse(txtTPAContactNumber.Text, out TPAContactNumber);
                generalInsuranceVo.TPAContactNumber = TPAContactNumber;
                if (rdoHealthYes.Checked)
                    generalInsuranceVo.IsEligibleFreeHealth = 1;
                if (txtCheckUpDate.IsEmpty == false)
                    generalInsuranceVo.CheckupDate = Convert.ToDateTime(txtCheckUpDate.SelectedDate);
                if (txtProposalDate.IsEmpty == false)
                    generalInsuranceVo.ProposalDate = Convert.ToDateTime(txtProposalDate.SelectedDate);
                generalInsuranceVo.ProposalNumber = txtProposalNumber.Text; ;
                //if (txtPolicyValidityStartDate.IsEmpty == false)
                //    generalInsuranceVo.PolicyValidityStartDate = Convert.ToDateTime(txtPolicyValidityStartDate.SelectedDate);
                //if (txtPolicyValidityEndDate.IsEmpty == false)
                //    generalInsuranceVo.PolicyValidityEndDate = Convert.ToDateTime(txtPolicyValidityEndDate.SelectedDate);
                if (txtMaturityDate.IsEmpty == false)
                    generalInsuranceVo.PolicyValidityEndDate = Convert.ToDateTime(txtMaturityDate.SelectedDate);

                double.TryParse(txtPremiumAmount.Text, out premiumAmount);
                generalInsuranceVo.PremiumAmount = premiumAmount;
                generalInsuranceVo.FrequencyCode = ddlPremiumCycle.SelectedValue;
                generalInsuranceVo.Frequency = ddlPremiumCycle.SelectedItem.Text;
                generalInsuranceVo.Remarks = txtRemarks.Text;
                if (chkIsPolicyByEmployer.Checked)
                    generalInsuranceVo.IsProvidedByEmployer = 1;

                if (btnSubmit.Text == "Submit")
                    generalInsuranceVo.GINetPositionId = insuranceBo.CreateGINetPosition(generalInsuranceVo, userVo.UserId);
                else
                {
                    generalInsuranceVo.GINetPositionId = int.Parse(ViewState["insuranceId"].ToString());
                    insuranceBo.UpdateGINetPosition(generalInsuranceVo, userVo.UserId);
                }

                //Delete Account and Asset Association to re-Enter if Update
                if (btnSubmit.Text == "Update")
                {
                    bresult = insuranceBo.DeleteGIAccountAssociation(int.Parse(ViewState["accountId"].ToString()));
                    bresult = insuranceBo.DeleteGIAssetAssociation(int.Parse(ViewState["insuranceId"].ToString()));
                }

                //Creating the Nominees
                if (rdoGroupPolicyYes.Checked)
                {
                    customerAccountAssociationVo.AccountId = int.Parse(ViewState["accountId"].ToString());
                    customerAccountAssociationVo.CustomerId = customerVo.CustomerId;

                    foreach (GridViewRow gvr in this.gvNominees.Rows)
                    {
                        if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                        {
                            if (rdoGroupPolicyYes.Checked && ddlTypeOfPolicy.SelectedValue == "PTIND")
                            {
                                TextBox txtBox = (TextBox)gvr.FindControl("txtSumAssured");
                                double.TryParse(txtBox.Text.ToString(), out nomineeShare);
                                customerAccountAssociationVo.NomineeShare = Convert.ToInt32(nomineeShare);
                            }
                            customerAccountAssociationVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[0].ToString());
                            customerAccountAssociationVo.AssociationType = "Policy";
                            insuranceBo.CreateGIAccountAssociation(customerAccountAssociationVo, userVo.UserId);
                        }
                    }
                }


                //generalInsuranceVo.GINetPositionId = GINetPositionId;

                //Associating the Assets to the GI Account

                if (chkProperty.Checked)
                {
                    generalInsuranceVo.AssetGroup = "PR";
                    generalInsuranceVo.AssetTable = "CustomerPropertyNetPosition";

                    foreach (String key in Request.Params.AllKeys)
                    {
                        if (key != null)
                        {
                            if (key.Contains("chkProperty_"))
                            {
                                generalInsuranceVo.AssetId = int.Parse(key.Substring(key.IndexOf("chkProperty_") + 12));
                                insuranceBo.CreateGIAssetAssociation(generalInsuranceVo, userVo.UserId);
                            }
                        }

                    }
                }
                if (chkCollectibles.Checked)
                {
                    generalInsuranceVo.AssetGroup = "CL";
                    generalInsuranceVo.AssetTable = "CustomerCollectiblesNetPosition";

                    foreach (String key in Request.Params.AllKeys)
                    {
                        if (key != null)
                        {
                            if (key.Contains("chkCollectibles_"))
                            {
                                generalInsuranceVo.AssetId = int.Parse(key.Substring(key.IndexOf("chkCollectibles_") + 16));
                                insuranceBo.CreateGIAssetAssociation(generalInsuranceVo, userVo.UserId);
                            }
                        }

                    }
                }
                if (chkGold.Checked)
                {
                    generalInsuranceVo.AssetGroup = "GD";
                    generalInsuranceVo.AssetTable = "CustomerGoldNetPosition";

                    foreach (String key in Request.Params.AllKeys)
                    {
                        if (key != null)
                        {
                            if (key.Contains("chkGold_"))
                            {
                                generalInsuranceVo.AssetId = int.Parse(key.Substring(key.IndexOf("chkGold_") + 8));
                                insuranceBo.CreateGIAssetAssociation(generalInsuranceVo, userVo.UserId);
                            }
                        }
                    }
                }
                if (chkPersonal.Checked)
                {
                    generalInsuranceVo.AssetGroup = "PI";
                    generalInsuranceVo.AssetTable = "CustomerPersonalNetPosition";

                    foreach (String key in Request.Params.AllKeys)
                    {
                        if (key != null)
                        {
                            if (key.Contains("chkPersonal_"))
                            {
                                generalInsuranceVo.AssetId = int.Parse(key.Substring(key.IndexOf("chkPersonal_") + 12));
                                insuranceBo.CreateGIAssetAssociation(generalInsuranceVo, userVo.UserId);
                            }
                        }
                    }
                }
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewGeneralInsuranceDetails','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioInsuranceEntry.ascx:btnSubmit_Click()");
                object[] objects = new object[4];
                objects[0] = customerVo;
                objects[1] = userVo;
                objects[2] = customerAccountAssociationVo;
                objects[3] = generalInsuranceVo;
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
                dsCustomerAssociates = customerAccountBo.GetCustomerFamilyDetail(customerVo.CustomerId);
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
                FunctionInfo.Add("Method", "PortfolioGeneralInsuranceEntry.ascx:LoadNominees()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void BindPolicyIssuerDropDown()
        {
            try
            {

                DataSet ds = insuranceBo.GetGIIssuerList();
                if (ds != null)
                {
                    ddlPolicyIssuer.DataSource = ds;
                    ddlPolicyIssuer.DataValueField = ds.Tables[0].Columns["XGII_GIIssuerCode"].ToString();
                    ddlPolicyIssuer.DataTextField = ds.Tables[0].Columns["XGII_GeneralinsuranceCompany"].ToString();
                    ddlPolicyIssuer.DataBind();
                }
                ddlPolicyIssuer.Items.Insert(0, new ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioGeneralInsuranceEntry.ascx:BindPolicyIssuerDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindFrequencyDropDown()
        {
            AssetBo assetBo = new AssetBo();
            try
            {
                string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
                DataTable dt = assetBo.GetFrequencyCode(path);
                if (dt.Rows.Count > 0)
                {
                    ddlPremiumCycle.DataSource = dt;
                    ddlPremiumCycle.DataValueField = dt.Columns["FrequencyCode"].ToString();
                    ddlPremiumCycle.DataTextField = dt.Columns["Frequency"].ToString();
                    ddlPremiumCycle.DataBind();
                }
                ddlPremiumCycle.Items.Insert(0, new ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioGeneralInsuranceEntry.ascx:BindFrequencyDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindPolicyTypeDropDown()
        {
            try
            {

                DataTable dt = insuranceBo.GetGIPolicyType();
                if (dt != null)
                {
                    ddlTypeOfPolicy.DataSource = dt;
                    ddlTypeOfPolicy.DataValueField = dt.Columns["XGIPT_PolicyTypeCode"].ToString();
                    ddlTypeOfPolicy.DataTextField = dt.Columns["XGIPT_PolicyType"].ToString();
                    ddlTypeOfPolicy.DataBind();
                }
                //ddlTypeOfPolicy.Items.Insert(0, new ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioGeneralInsuranceEntry.ascx:BindPolicyIssuerDropDown()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnAssetShow_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            int i = 0;
            int j = 0;
            string perAssSubCatCode = null;
            try
            {
                if (chkProperty.Checked)
                {
                    dt = insuranceBo.GetCustomerPropertyList(customerVo.CustomerId);
                    if (dt.Rows.Count > 0)
                    {
                        phProperty.Controls.Clear();
                        Table tb = new Table();
                        TableRow tr = new TableRow();
                        TableCell tc;
                        //My code
                        foreach (DataRow dr in dt.Rows)
                        {
                            //Cells in table
                            if (dr != null)
                            {
                                tc = new TableCell();
                                CheckBox chk = new CheckBox();
                                chk.ID = "chkProperty_" + dt.Rows[i][0].ToString();
                                chk.Text = dt.Rows[i][1].ToString();
                                chk.CssClass = "cmbField";
                                tc.Controls.Add(chk);
                                tc.ColumnSpan = 1;
                                tr.Cells.Add(tc);
                                j = j + 1;
                                i = i + 1;
                            }

                            if (j == 7)
                            {
                                tb.Rows.Add(tr);
                                j = 0;
                                tr = new TableRow();
                            }
                        }
                        tb.Rows.Add(tr);
                        phProperty.Controls.Add(tb);
                        pnlPickAssetProperty.Visible = true;
                    }
                }
                if (chkCollectibles.Checked)
                {
                    i = 0;
                    j = 0;
                    dt = insuranceBo.GetCustomerCollectiblesList(customerVo.CustomerId);
                    if (dt.Rows.Count > 0)
                    {
                        phCollectibles.Controls.Clear();
                        Table tb = new Table();
                        TableRow tr = new TableRow();
                        TableCell tc;
                        //My code
                        foreach (DataRow dr in dt.Rows)
                        {
                            //Cells in table
                            if (dr != null)
                            {
                                tc = new TableCell();
                                CheckBox chk = new CheckBox();
                                chk.ID = "chkCollectibles_" + dt.Rows[i][0].ToString();
                                chk.Text = dt.Rows[i][1].ToString();
                                chk.CssClass = "cmbField";
                                tc.Controls.Add(chk);
                                tc.ColumnSpan = 1;
                                tr.Cells.Add(tc);
                                j = j + 1;
                                i = i + 1;
                            }

                            if (j == 7)
                            {
                                tb.Rows.Add(tr);
                                j = 0;
                                tr = new TableRow();
                            }
                        }
                        tb.Rows.Add(tr);
                        phCollectibles.Controls.Add(tb);
                        pnlPickAssetCollectibles.Visible = true;
                    }
                }
                if (chkGold.Checked)
                {
                    i = 0;
                    j = 0;
                    dt = insuranceBo.GetCustomerGoldList(customerVo.CustomerId);
                    if (dt.Rows.Count > 0)
                    {
                        phGold.Controls.Clear();
                        Table tb = new Table();
                        TableRow tr = new TableRow();
                        TableCell tc;
                        //My code
                        foreach (DataRow dr in dt.Rows)
                        {
                            //Cells in table
                            if (dt.Rows[i] != null)
                            {
                                tc = new TableCell();
                                CheckBox chk = new CheckBox();
                                chk.ID = "chkGold_" + dt.Rows[i][0].ToString();
                                chk.Text = dt.Rows[i][1].ToString();
                                chk.CssClass = "cmbField";
                                tc.Controls.Add(chk);
                                tc.ColumnSpan = 1;
                                tr.Cells.Add(tc);
                                j = j + 1;
                                i = i + 1;
                            }

                            if (j == 7)
                            {
                                tb.Rows.Add(tr);
                                j = 0;
                                tr = new TableRow();
                            }
                        }
                        tb.Rows.Add(tr);
                        phGold.Controls.Add(tb);
                        pnlPickAssetGold.Visible = true;
                    }
                }
                if (chkPersonal.Checked)
                {
                    i = 0;
                    j = 0;
                    if (ViewState["assSubCatCode"] == "GIBCVE" || ViewState["assSubCatCode"] == "GIBCAT" || ViewState["assSubCatCode"] == "GIRIAT" || ViewState["assSubCatCode"] == "GIRIMV")
                        perAssSubCatCode = "PIAL";

                    dt = insuranceBo.GetCustomerPersonalItemsList(customerVo.CustomerId, perAssSubCatCode);
                    if (dt.Rows.Count > 0)
                    {
                        phPersonal.Controls.Clear();
                        Table tb = new Table();
                        TableRow tr = new TableRow();
                        TableCell tc;
                        //My code
                        foreach (DataRow dr in dt.Rows)
                        {
                            //Cells in table
                            if (dr != null)
                            {
                                tc = new TableCell();
                                CheckBox chk = new CheckBox();
                                chk.ID = "chkPersonal_" + dt.Rows[i][0].ToString();
                                chk.Text = dt.Rows[i][1].ToString();
                                chk.CssClass = "cmbField";
                                tc.Controls.Add(chk);
                                tc.ColumnSpan = 1;
                                tr.Cells.Add(tc);
                                j = j + 1;
                                i = i + 1;
                            }

                            if (j == 7)
                            {
                                tb.Rows.Add(tr);
                                j = 0;
                                tr = new TableRow();
                            }
                        }
                        tb.Rows.Add(tr);
                        phPersonal.Controls.Add(tb);
                        pnlPickAssetPersonal.Visible = true;
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
                FunctionInfo.Add("Method", "PortfolioGeneralInsuranceEntry.ascx:btnAssetShow_Click()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        private void SetControls()
        {

            Session["insuranceId"] = insuranceId;
            generalInsuranceVo = new GeneralInsuranceVo();
            generalInsuranceVo = insuranceBo.GetGINetPositionDetails(insuranceId);
            BindAssetParticular(generalInsuranceVo.InsIssuerCode);
            assCatCode = generalInsuranceVo.AssetInstrumentCategoryCode;
            ViewState["assCatCode"] = assCatCode;
            assSubCatCode = generalInsuranceVo.AssetInstrumentSubCategoryCode;
            ViewState["assSubCatCode"] = assSubCatCode;
            accountId = int.Parse(generalInsuranceVo.AccountId.ToString());
            //ViewState["accountId"] = accountId;

            txtAssetCategory.Text = generalInsuranceVo.AssetInstrumentCategoryName;
            txtAssetSubCategory.Text = generalInsuranceVo.AssetInstrumentSubCategoryName;
            txtPolicyNumber.Text = generalInsuranceVo.PolicyNumber;
            txtPolicyParticular.SelectedValue = generalInsuranceVo.PolicyParticular;
            ddlPolicyIssuer.SelectedValue = generalInsuranceVo.InsIssuerCode;
            if (generalInsuranceVo.PolicyValidityEndDate != DateTime.MinValue)
                txtMaturityDate.SelectedDate = generalInsuranceVo.PolicyValidityEndDate;
            if (generalInsuranceVo.OriginalStartDate != DateTime.MinValue)
                txtPolicyCommencementDate.SelectedDate = generalInsuranceVo.OriginalStartDate;
            if (generalInsuranceVo.IsProvidedByEmployer == 1)
                chkIsPolicyByEmployer.Checked = true;
            if (generalInsuranceVo.IsFamilyPolicy == 1)
            {
                rdoGroupPolicyYes.Checked = true;
                hdnGroupPolicy.Value = "1";
            }
            else
            {
                rdoGroupPolicyNo.Checked = true;
                hdnGroupPolicy.Value = "0";
            }
            ddlTypeOfPolicy.SelectedValue = generalInsuranceVo.PolicyTypeCode;
            hdnPolicyType.Value = generalInsuranceVo.PolicyTypeCode;
            txtSumAssured1.Text = generalInsuranceVo.SumAssured.ToString();
            hdnSumAssured.Value = generalInsuranceVo.SumAssured.ToString();
            txtTPA.Text = generalInsuranceVo.TPAName;
            if (generalInsuranceVo.TPAContactNumber != 0)
                txtTPAContactNumber.Text = generalInsuranceVo.TPAContactNumber.ToString();
            if (generalInsuranceVo.IsEligibleFreeHealth == 1)
            {
                rdoHealthYes.Checked = true;
                hdnFreeHealth.Value = "1";
            }
            else
            {
                rdoHealthNo.Checked = true;
                hdnFreeHealth.Value = "0";
            }
            if (generalInsuranceVo.CheckupDate != DateTime.MinValue)
                txtCheckUpDate.SelectedDate = generalInsuranceVo.CheckupDate;
            if (generalInsuranceVo.ProposalDate != DateTime.MinValue)
                txtProposalDate.SelectedDate = generalInsuranceVo.ProposalDate;
            txtProposalNumber.Text = generalInsuranceVo.ProposalNumber;
            //if (generalInsuranceVo.PolicyValidityStartDate != DateTime.MinValue)
            //    txtPolicyValidityStartDate.SelectedDate = generalInsuranceVo.PolicyValidityStartDate;
            //if (generalInsuranceVo.PolicyValidityEndDate != DateTime.MinValue)
            //    txtPolicyValidityEndDate.SelectedDate = generalInsuranceVo.PolicyValidityEndDate;
            ddlPremiumCycle.SelectedValue = generalInsuranceVo.FrequencyCode;
            if (generalInsuranceVo.PremiumAmount != 0)
                txtPremiumAmount.Text = generalInsuranceVo.PremiumAmount.ToString();
            txtRemarks.Text = generalInsuranceVo.Remarks;

            //Account Associates Details
            LoadNominees();
            DataTable dtAccountAssc = insuranceBo.GetGIAccountAssociation(accountId);
            foreach (DataRow dr in dtAccountAssc.Rows)
            {
                foreach (GridViewRow gvr in this.gvNominees.Rows)
                {
                    if (gvNominees.DataKeys[gvr.RowIndex].Values[0].ToString() == dr["CA_AssociationId"].ToString())
                    {
                        CheckBox chkBox = (CheckBox)gvr.FindControl("chkId");
                        chkBox.Checked = true;
                        TextBox txtBox = (TextBox)gvr.FindControl("txtSumAssured");
                        txtBox.Text = dr["CGIAA_Sum Assured"].ToString();
                    }
                }
            }
            DisplayAssetCategory();
            //Asset Association Details
            DataTable dtAssetAssc = insuranceBo.GetGIAssetAssociation(insuranceId);
            foreach (DataRow dr in dtAssetAssc.Rows)
            {
                if (dr["CGIAA_AssetGroup"].ToString() == "PR")
                    chkProperty.Checked = true;
                else if (dr["CGIAA_AssetGroup"].ToString() == "PI")
                    chkPersonal.Checked = true;
                else if (dr["CGIAA_AssetGroup"].ToString() == "CL")
                    chkCollectibles.Checked = true;
                else if (dr["CGIAA_AssetGroup"].ToString() == "GD")
                    chkGold.Checked = true;
            }
            AssetShow(dtAssetAssc);
        }

        private void EnableDisableControls(bool trueOrFalse)
        {
            txtAssetCategory.Enabled = trueOrFalse;
            txtAssetSubCategory.Enabled = trueOrFalse;
            txtCheckUpDate.Enabled = trueOrFalse;
            txtPolicyCommencementDate.Enabled = trueOrFalse;
            txtPolicyNumber.Enabled = trueOrFalse;
            txtPolicyParticular.Enabled = trueOrFalse;
            txtMaturityDate.Enabled = trueOrFalse;
            //txtPolicyValidityEndDate.Enabled = trueOrFalse;
            //txtPolicyValidityStartDate.Enabled = trueOrFalse;
            txtPremiumAmount.Enabled = trueOrFalse;
            txtProposalDate.Enabled = trueOrFalse;
            txtProposalNumber.Enabled = trueOrFalse;
            txtRemarks.Enabled = trueOrFalse;
            txtSumAssured1.Enabled = trueOrFalse;
            txtTPA.Enabled = trueOrFalse;
            txtTPAContactNumber.Enabled = trueOrFalse;
            gvNominees.Enabled = trueOrFalse;
            rdoGroupPolicyNo.Enabled = trueOrFalse;
            rdoGroupPolicyYes.Enabled = trueOrFalse;
            rdoHealthNo.Enabled = trueOrFalse;
            rdoHealthYes.Enabled = trueOrFalse;
            chkCollectibles.Enabled = trueOrFalse;
            chkGold.Enabled = trueOrFalse;
            chkIsPolicyByEmployer.Enabled = trueOrFalse;
            chkPersonal.Enabled = trueOrFalse;
            chkProperty.Enabled = trueOrFalse;
            ddlPolicyIssuer.Enabled = trueOrFalse;
            ddlPremiumCycle.Enabled = trueOrFalse;
            ddlTypeOfPolicy.Enabled = trueOrFalse;
        }

        private void DisplayAssetCategory()
        {
            //if (Request.QueryString["FromPage"] == "GIAccountAdd")
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), " ChangeGroupPolicy", "ChangeGroupPolicy('No')", true);

            if (ViewState["assSubCatCode"].ToString() == "GIBCSB" || ViewState["assSubCatCode"].ToString() == "GIBCFR" || ViewState["assSubCatCode"].ToString() == "GIBCVE" || ViewState["assSubCatCode"].ToString() == "GIBCAT" || ViewState["assSubCatCode"].ToString() == "GIBCOT" || ViewState["assSubCatCode"].ToString() == "GIRIAT" || ViewState["assSubCatCode"].ToString() == "GIRIMV" || ViewState["assSubCatCode"].ToString() == "GIRISK" || ViewState["assSubCatCode"].ToString() == "GIRIFR" || ViewState["assSubCatCode"].ToString() == "GIRIBH" || ViewState["assSubCatCode"].ToString() == "GIRIOT")
            {
                trAssetGroupHeader.Visible = true;
                trAssetGroup.Visible = true;
                spnchkPersonal.Visible = true;
            }
            if (ViewState["assSubCatCode"].ToString() == "GIBCOC" || ViewState["assSubCatCode"].ToString() == "GIBCSB" || ViewState["assSubCatCode"].ToString() == "GIBCFR" || ViewState["assSubCatCode"].ToString() == "GIRIHO" || ViewState["assSubCatCode"].ToString() == "GIRISK" || ViewState["assSubCatCode"].ToString() == "GIRISK" || ViewState["assSubCatCode"].ToString() == "GIRIFR" || ViewState["assSubCatCode"].ToString() == "GIRIBH")
            {
                trAssetGroupHeader.Visible = true;
                trAssetGroup.Visible = true;
                spnchkProperty.Visible = true;
            }
            if (ViewState["assSubCatCode"].ToString() == "GIBCFR" || ViewState["assSubCatCode"].ToString() == "GIBCOT" || ViewState["assSubCatCode"].ToString() == "GIRIBH" || ViewState["assSubCatCode"].ToString() == "GIRIOT")
            {
                trAssetGroup.Visible = true;
                spnchkCollectibles.Visible = true;
            }
            if (ViewState["assSubCatCode"].ToString() == "GIRIBH" || ViewState["assSubCatCode"].ToString() == "GIRIOT")
            {
                trAssetGroup.Visible = true;
                spnchkGold.Visible = true;
            }
        }

        private void AssetShow(DataTable dtAssetAssc)
        {
            DataTable dt = new DataTable();
            int i = 0;
            int j = 0;
            string perAssSubCatCode = null;
            try
            {
                if (chkProperty.Checked)
                {
                    dt = insuranceBo.GetCustomerPropertyList(customerVo.CustomerId);
                    if (dt.Rows.Count > 0)
                    {
                        phProperty.Controls.Clear();
                        Table tb = new Table();
                        TableRow tr = new TableRow();
                        TableCell tc;
                        //My code
                        foreach (DataRow dr in dt.Rows)
                        {
                            //Cells in table
                            if (dr != null)
                            {

                                tc = new TableCell();
                                CheckBox chk = new CheckBox();
                                chk.ID = "chkProperty_" + dt.Rows[i][0].ToString();
                                chk.Text = dt.Rows[i][1].ToString();
                                chk.CssClass = "cmbField";
                                foreach (DataRow dr1 in dtAssetAssc.Rows)
                                {
                                    if (dr1["CGIAA_AssetId"].ToString() == dt.Rows[i][0].ToString() && dr1["CGIAA_AssetGroup"].ToString() == "PR")
                                        chk.Checked = true;
                                }
                                if (action == "View")
                                    chk.Enabled = false;
                                tc.Controls.Add(chk);
                                tc.ColumnSpan = 1;
                                tr.Cells.Add(tc);
                                j = j + 1;
                                i = i + 1;
                            }

                            if (j == 7)
                            {
                                tb.Rows.Add(tr);
                                j = 0;
                                tr = new TableRow();
                            }
                        }
                        tb.Rows.Add(tr);
                        phProperty.Controls.Add(tb);
                        pnlPickAssetProperty.Visible = true;
                    }
                }
                if (chkCollectibles.Checked)
                {
                    i = 0;
                    j = 0;
                    dt = insuranceBo.GetCustomerCollectiblesList(customerVo.CustomerId);
                    if (dt.Rows.Count > 0)
                    {
                        phCollectibles.Controls.Clear();
                        Table tb = new Table();
                        TableRow tr = new TableRow();
                        TableCell tc;
                        //My code
                        foreach (DataRow dr in dt.Rows)
                        {
                            //Cells in table
                            if (dr != null)
                            {
                                tc = new TableCell();
                                CheckBox chk = new CheckBox();
                                chk.ID = "chkCollectibles_" + dt.Rows[i][0].ToString();
                                chk.Text = dt.Rows[i][1].ToString();
                                chk.CssClass = "cmbField";
                                foreach (DataRow dr1 in dtAssetAssc.Rows)
                                {
                                    if (dr1["CGIAA_AssetId"].ToString() == dt.Rows[i][0].ToString() && dr1["CGIAA_AssetGroup"].ToString() == "CL")
                                        chk.Checked = true;
                                }
                                if (action == "View")
                                    chk.Enabled = false;
                                tc.Controls.Add(chk);
                                tc.ColumnSpan = 1;
                                tr.Cells.Add(tc);
                                j = j + 1;
                                i = i + 1;
                            }

                            if (j == 7)
                            {
                                tb.Rows.Add(tr);
                                j = 0;
                                tr = new TableRow();
                            }
                        }
                        tb.Rows.Add(tr);
                        phCollectibles.Controls.Add(tb);
                        pnlPickAssetCollectibles.Visible = true;
                    }
                }
                if (chkGold.Checked)
                {
                    i = 0;
                    j = 0;
                    dt = insuranceBo.GetCustomerGoldList(customerVo.CustomerId);
                    if (dt.Rows.Count > 0)
                    {
                        phGold.Controls.Clear();
                        Table tb = new Table();
                        TableRow tr = new TableRow();
                        TableCell tc;
                        //My code
                        foreach (DataRow dr in dt.Rows)
                        {
                            //Cells in table
                            if (dt.Rows[i] != null)
                            {
                                tc = new TableCell();
                                CheckBox chk = new CheckBox();
                                chk.ID = "chkGold_" + dt.Rows[i][0].ToString();
                                chk.Text = dt.Rows[i][1].ToString();
                                chk.CssClass = "cmbField";
                                foreach (DataRow dr1 in dtAssetAssc.Rows)
                                {
                                    if (dr1["CGIAA_AssetId"].ToString() == dt.Rows[i][0].ToString() && dr1["CGIAA_AssetGroup"].ToString() == "GD")
                                        chk.Checked = true;
                                }
                                if (action == "View")
                                    chk.Enabled = false;
                                tc.Controls.Add(chk);
                                tc.ColumnSpan = 1;
                                tr.Cells.Add(tc);
                                j = j + 1;
                                i = i + 1;
                            }

                            if (j == 7)
                            {
                                tb.Rows.Add(tr);
                                j = 0;
                                tr = new TableRow();
                            }
                        }
                        tb.Rows.Add(tr);
                        phGold.Controls.Add(tb);
                        pnlPickAssetGold.Visible = true;
                    }
                }
                if (chkPersonal.Checked)
                {
                    i = 0;
                    j = 0;
                    if (assSubCatCode == "GIBCVE" || assSubCatCode == "GIBCAT" || assSubCatCode == "GIRIAT" || assSubCatCode == "GIRIMV")
                        perAssSubCatCode = "PIAL";

                    dt = insuranceBo.GetCustomerPersonalItemsList(customerVo.CustomerId, perAssSubCatCode);
                    if (dt.Rows.Count > 0)
                    {
                        phPersonal.Controls.Clear();
                        Table tb = new Table();
                        TableRow tr = new TableRow();
                        TableCell tc;
                        //My code
                        foreach (DataRow dr in dt.Rows)
                        {
                            //Cells in table
                            if (dr != null)
                            {
                                tc = new TableCell();
                                CheckBox chk = new CheckBox();
                                chk.ID = "chkPersonal_" + dt.Rows[i][0].ToString();
                                chk.Text = dt.Rows[i][1].ToString();
                                chk.CssClass = "cmbField";
                                foreach (DataRow dr1 in dtAssetAssc.Rows)
                                {
                                    if (dr1["CGIAA_AssetId"].ToString() == dt.Rows[i][0].ToString() && dr1["CGIAA_AssetGroup"].ToString() == "PI")
                                        chk.Checked = true;
                                }
                                if (action == "View")
                                    chk.Enabled = false;
                                tc.Controls.Add(chk);
                                tc.ColumnSpan = 1;
                                tr.Cells.Add(tc);
                                j = j + 1;
                                i = i + 1;
                            }

                            if (j == 7)
                            {
                                tb.Rows.Add(tr);
                                j = 0;
                                tr = new TableRow();
                            }
                        }
                        tb.Rows.Add(tr);
                        phPersonal.Controls.Add(tb);
                        pnlPickAssetPersonal.Visible = true;
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
                FunctionInfo.Add("Method", "PortfolioGeneralInsuranceEntry.ascx:AssetShow()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        //private void CheckAssets(DataTable dt)
        //{
        //    try
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            if(dr["CGIAA_AssetGroup"].ToString() == "PR")
        //            {
        //                foreach (Control ctrl in phProperty.Controls)
        //                {
        //                    if (ctrl.GetType().Name == "CheckBox")
        //                    {
        //                        CheckBox chk = ((CheckBox)(ctrl));
        //                        if (chk.ID.Contains("chkProperty_"))
        //                        {
        //                            if (chk.ID.Contains(dr["CGIAA_AssetId"].ToString()))
        //                                chk.Checked = true;
        //                        }
        //                    }
        //                }
        //            }
        //            else if (dr["CGIAA_AssetGroup"].ToString() == "GD")
        //            {
        //                foreach (Control ctrl in phGold.Controls)
        //                {
        //                    if (ctrl.GetType().Name == "CheckBox")
        //                    {
        //                        CheckBox chk = ((CheckBox)(ctrl));
        //                        if (chk.ID.Contains("chkGold_"))
        //                        {
        //                            if (chk.ID.Contains(dr["CGIAA_AssetId"].ToString()))
        //                                chk.Checked = true;
        //                        }
        //                    }
        //                }
        //            }
        //            else if (dr["CGIAA_AssetGroup"].ToString() == "CL")
        //            {
        //                foreach (Control ctrl in phCollectibles.Controls)
        //                {
        //                    if (ctrl.GetType().Name == "CheckBox")
        //                    {
        //                        CheckBox chk = ((CheckBox)(ctrl));
        //                        if (chk.ID.Contains("chkCollectibles_"))
        //                        {
        //                            if (chk.ID.Contains(dr["CGIAA_AssetId"].ToString()))
        //                                chk.Checked = true;
        //                        }
        //                    }
        //                }
        //            }
        //            else if (dr["CGIAA_AssetGroup"].ToString() == "PI")
        //            {
        //                foreach (Control ctrl in phPersonal.Controls)
        //                {
        //                    if (ctrl.GetType().Name == "CheckBox")
        //                    {
        //                        CheckBox chk = ((CheckBox)(ctrl));
        //                        if (chk.ID.Contains("chkPersonal_"))
        //                        {
        //                            if (chk.ID.Contains(dr["CGIAA_AssetId"].ToString()))
        //                                chk.Checked = true;
        //                        }
        //                    }
        //                }
        //            }
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
        //        FunctionInfo.Add("Method", "PortfolioGeneralInsuranceEntry.ascx:CheckAssets()");
        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }

        //}

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewGeneralInsuranceDetails','login');", true);
            Session["action"] = null;
            Session["insuranceId"] = null;
        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            EnableDisableControls(true);
            btnSubmit.Text = "Update";
            btnSubmit.Visible = true;
            btnAssetShow.Visible = true;
            lnkBtnEdit.Visible = false;
            txtAssetCategory.Enabled = false;
            txtAssetSubCategory.Enabled = false;
            txtPolicyNumber.Enabled = false;
            DataTable dtAssetAssc = insuranceBo.GetGIAssetAssociation(insuranceId);
            action = "Edit";
            AssetShow(dtAssetAssc);
        }

        public void BindAssetParticular(string issuerCode)
        {
            AssetBo assetBo = new AssetBo();
            DataSet ds = assetBo.GetGIPlans(issuerCode);
            DataTable dtSchemePlan = ds.Tables[0];
            txtPolicyParticular.Items.Clear();
            if (dtSchemePlan.Rows.Count > 0)
            {
                txtPolicyParticular.DataSource = dtSchemePlan;
                txtPolicyParticular.DataValueField = dtSchemePlan.Columns["PGISP_SchemePlanCode"].ToString();
                txtPolicyParticular.DataTextField = dtSchemePlan.Columns["PGISP_SchemePlanName"].ToString();
                txtPolicyParticular.DataBind();
                txtPolicyParticular.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {

                txtPolicyParticular.Items.Insert(0, new ListItem("Select", "Select"));
            }

        }

        protected void btnInsertNewScheme_Click(object sender, EventArgs e)
        {
            OrderBo orderbo = new OrderBo();
            if (txtAsset.Text.Trim() != "")
            {
                if (ddlPolicyIssuer.SelectedIndex != 0)
                {
                    orderbo.InsertIntoProductGIInsuranceScheme(assSubCatCode, ddlPolicyIssuer.SelectedValue, txtAsset.Text.Trim());
                    BindAssetParticular(ddlPolicyIssuer.SelectedValue);
                }
            }
          
        }

        public void ddlPolicyIssuer_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            lblIssuarCode.Text = ddlPolicyIssuer.SelectedItem.Text;
            BindAssetParticular(ddlPolicyIssuer.SelectedValue);
        }

        public void showHealthInsuranceFields()
        {
            lblWthHealthCheckUp.Visible = true;
            rdoHealthNo.Visible = true;
            rdoHealthYes.Visible = true;
            lblCheckUpDate.Visible = true;
            txtCheckUpDate.Visible = true;
            lblProposalDate.Visible = true;
            txtProposalDate.Visible = true;
            lblProposalNumber.Visible = true;
            txtProposalNumber.Visible = true;
        }

        public void hideHealthInsuranceFields()
        {
            lblWthHealthCheckUp.Visible = false;
            rdoHealthNo.Visible = false;
            rdoHealthYes.Visible = false;
            lblCheckUpDate.Visible = false;
            txtCheckUpDate.Visible = false;
            lblProposalDate.Visible = false;
            txtProposalDate.Visible = false;
            lblProposalNumber.Visible = false;
            txtProposalNumber.Visible = false;
        }

        public DateTime calculateMaturityDate(int period, DateTime startDate)
        {
            DateTime endDate = new DateTime();
            startDate = DateTime.Parse(txtPolicyCommencementDate.SelectedDate.ToString());

            //if (rdbPolicyTermDays.Checked == true)
            //{
              //  period = int.Parse(txtDays.Text);
                endDate = startDate.AddDays(period);
            //}
            //else if (rdbPolicyTermMonth.Checked == true)
            //{
             //   period = int.Parse(txtMonths.Text);
                endDate = startDate.AddMonths(period);
            //}


            return endDate;
        }

        protected void ddlPeriodSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMaturityDate.SelectedDate = CalcEndDate(int.Parse(txtPolicyTerm.Text), DateTime.Parse(txtPolicyCommencementDate.SelectedDate.ToString()));
        }

        private DateTime CalcEndDate(int period, DateTime startDate)
        {
            DateTime endDate = new DateTime();
            if (ddlPeriodSelection.SelectedItem.Value == "DA")
            {
                endDate = startDate.AddDays(period);
            }
            else if (ddlPeriodSelection.SelectedItem.Value == "MN")
            {
                endDate = startDate.AddMonths(period);
            }
            else if (ddlPeriodSelection.SelectedItem.Value == "YR")
            {
                endDate = startDate.AddYears(period);
            }

            return endDate;
        }

    }
}