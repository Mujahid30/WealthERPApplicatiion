using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using System.Configuration;
using System.Data;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using System.Web.UI.HtmlControls;
using VoUser;


namespace WealthERP.Loans
{
    public partial class AdviserLoanScheme : System.Web.UI.UserControl
    {
        int CONST_INTERESTE_RATE_COLUMN_ID = 6;
        public enum Mode
        {
            View = 0,
            Add = 1,
            Edit = 2
        }
        int schemeId = 0;
        SchemeDetailsVo schemeDetailsVo = new SchemeDetailsVo();
        Mode mode = Mode.Add;
        UserVo userVo = null;
        AdvisorVo adviserVo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            //Response.Write(ddlLoanType.SelectedValue +ddlLoanType.SelectedItem.Text);
            //Response.Write(ddlBorrowerType.SelectedValue + ddlBorrowerType.Text);
            userVo = (UserVo)Session["UserVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            if (Session["LoanSchemeId"] != null && Session["LoanSchemeViewStatus"].ToString() == "Edit")
            {
                schemeId = int.Parse(Session["LoanSchemeId"].ToString());
                lblSchemeId.Text = "Scheme ID :";
                lblSchemeIdVal.Text = schemeId.ToString();
                mode = Mode.Edit;
            }
            else if (Session["LoanSchemeId"] != null && Session["LoanSchemeViewStatus"].ToString() == "View")
            {
                schemeId = int.Parse(Session["LoanSchemeId"].ToString());
                lblSchemeId.Text = "Scheme ID :";
                lblSchemeIdVal.Text = schemeId.ToString();
                mode = Mode.View;
            }
            else
            {
                lblSchemeId.Visible = false;
                lblSchemeIdVal.Visible = false;
            }

            if (!IsPostBack)
            {

                //currentUser = userVo.UserId;

                BindLoanPartner();
                BindLoanType();


            }

            BindInterestRates();
            SetVisibilty(mode);

            BindDocuments();
        }

        private void SetVisibilty(Mode mode)
        {
            SetControlVisibility();
            if (mode == Mode.Edit)
            {
                lnkEdit.Visible = false;
                btnSubmit.Visible = true;
                if (!IsPostBack)
                    BindSchemeDetails();
                btnSubmit.Text = "Update Scheme";
            }
            else if (mode == Mode.Add)
            {
                btnSubmit.Visible = true;
                btnSubmit.Text = "Create Scheme";
                lnkEdit.Visible = false;
            }
            else if (mode == Mode.View)
            {
                lnkEdit.Visible = true;
                BindSchemeDetails();
                btnSubmit.Visible = false;
                gvAdviserInterestRates.Enabled = false;

            }

        }

        private void BindInterestRates()
        {
            if (mode == Mode.Edit || mode == Mode.View)
            {
                if (!Page.IsPostBack)
                {
                    DataSet ds = LiabilitiesBo.GetInterestRateBySchemeId(schemeId);
                    gvAdviserInterestRates.DataSource = ds;
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        gvAdviserInterestRates.DataBind();
                        ViewState["CurrentTable"] = ds.Tables[0];
                    }
                    else
                        SetDummyRow();
                }
                else
                {
                    if ((DataTable)ViewState["CurrentTable"] != null)
                    {
                        gvAdviserInterestRates.DataSource = (DataTable)ViewState["CurrentTable"];
                        gvAdviserInterestRates.DataBind();
                    }
                }
            }
            else if (mode == Mode.Add)
            {
                if (!Page.IsPostBack)
                {
                    SetDummyRow();
                    //gvAdviserInterestRates.FooterRow.Visible = true;
                }
            }
            DisplayFooter();
        }

        private DataTable GetGridviewTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ALSIR_InterestCategory", typeof(string)));
            dt.Columns.Add(new DataColumn("ALSIR_MinimumFinance", typeof(string)));
            dt.Columns.Add(new DataColumn("ALSIR_MaximumFinance", typeof(string)));
            dt.Columns.Add(new DataColumn("ALSIR_MinimumPeriod", typeof(string)));
            dt.Columns.Add(new DataColumn("ALSIR_MaximumPeriod", typeof(string)));
            dt.Columns.Add(new DataColumn("ALSIR_DifferentialInterestRate", typeof(string)));
            dt.Columns.Add(new DataColumn("ALSIR_MaximumFinancePer", typeof(string)));
            dt.Columns.Add(new DataColumn("ALSIR_ProcessingCharges", typeof(string)));
            dt.Columns.Add(new DataColumn("ALSIR_PreClosingCharges", typeof(string)));
            dt.Columns.Add(new DataColumn("ALSIR_LoanSchemeInterestRateId", typeof(string)));
            return dt;
        }

        private void SetDummyRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = GetGridviewTable();
            dr = dt.NewRow();
            dr["ALSIR_LoanSchemeInterestRateId"] = "-1";
            dt.Rows.Add(dr);
            //Store the DataTable in ViewState

            gvAdviserInterestRates.DataSource = dt;
            gvAdviserInterestRates.DataBind();
            gvAdviserInterestRates.Rows[0].Visible = false;


        }

        private void DisplayFooter()
        {
            Button btnAdd = new Button();
            btnAdd.Text = "Add Record";
            btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            //btnAdd.CausesValidation = false;
            if (gvAdviserInterestRates.FooterRow != null)
            {
                gvAdviserInterestRates.FooterRow.Cells[0].Controls.Add(btnAdd);
                gvAdviserInterestRates.FooterRow.Visible = true;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = null;//GetGridviewTable();
            if (ViewState["CurrentTable"] != null)
                dt = (DataTable)ViewState["CurrentTable"];
            else
                dt = GetGridviewTable();

            DataRow dr = dt.NewRow();

            TableCellCollection footerRow = gvAdviserInterestRates.FooterRow.Cells;
            dr["ALSIR_InterestCategory"] = ((TextBox)footerRow[1].FindControl("txtCategory")).Text;
            dr["ALSIR_MinimumFinance"] = TryParseDouble(((TextBox)footerRow[2].FindControl("txtMinFinance")).Text);
            dr["ALSIR_MaximumFinance"] = TryParseDouble(((TextBox)footerRow[3].FindControl("txtMaxFinance")).Text);
            dr["ALSIR_MinimumPeriod"] = TryParseInt(((TextBox)footerRow[4].FindControl("txtMinimumPeriod")).Text);
            dr["ALSIR_MaximumPeriod"] = TryParseInt(((TextBox)footerRow[5].FindControl("txtMaximumPeriod")).Text);
            dr["ALSIR_DifferentialInterestRate"] = TryParseFloat(((TextBox)footerRow[6].FindControl("txtDifferentialInterestRate")).Text);
            dr["ALSIR_MaximumFinancePer"] = TryParseFloat(((TextBox)footerRow[7].FindControl("txtMaximumFinancePer")).Text);
            dr["ALSIR_ProcessingCharges"] = TryParseFloat(((TextBox)footerRow[8].FindControl("txtProcessingCharges")).Text);
            dr["ALSIR_PreClosingCharges"] = TryParseFloat(((TextBox)footerRow[9].FindControl("txtPreClosingCharges")).Text);
            dr["ALSIR_LoanSchemeInterestRateId"] = "99999" + dt.Rows.Count + 1;
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            gvAdviserInterestRates.DataSource = dt;
            gvAdviserInterestRates.DataBind();
            DisplayFooter();
        }

        private void BindDocuments()
        {
            if (!IsPostBack && mode == Mode.Add)
                return;

            int customerType = 0;
            int loanTypeCode = 0;
            tblDocuments.Visible = true;

            if (mode == Mode.Add || mode == Mode.Edit)
            {
                if (ddlLoanType.SelectedValue != string.Empty)
                    loanTypeCode = int.Parse(ddlLoanType.SelectedValue);
                if (ddlBorrowerType.SelectedValue != string.Empty)
                    customerType = int.Parse(ddlBorrowerType.SelectedValue);

            }
            else
            {
                customerType = int.Parse(schemeDetailsVo.CustomerCategory.ToString());
                loanTypeCode = int.Parse(schemeDetailsVo.LoanType.ToString());
            }
            if (customerType == 0 || loanTypeCode == 0)
                return;
            List<SchemeProof> schemeProofs = LiabilitiesBo.GetDocumentsForBorrower(schemeId, customerType, loanTypeCode);
            tblDocuments.Rows.Clear();

            foreach (SchemeProof schemeProof in schemeProofs)
            {
                TableRow tr = new TableRow();
                TableCell tcProofType = new TableCell();
                tcProofType.Text = "<b>" + schemeProof.proofTypeName + "</b>";
                tcProofType.CssClass = "FieldName";
                TableCell tcProof = new TableCell();
                //CheckBoxList chkBoxList = new CheckBoxList();
                //chkBoxList.ID = "chkDocuments";

                foreach (Proof proof in schemeProof.proofs)
                {
                    CheckBox chk = new CheckBox();

                    //chkBoxList.Items.Add(new ListItem(proof.proofName, proof.proofCode));
                    chk.ID = "chkLoan-" + proof.proofCode;
                    chk.Text = proof.proofName;
                    //chk.ToolTip = schemeProof.proofTypeCode + schemeProof.proofTypeName;
                    if (proof.isAdded)
                        chk.Checked = true;
                    LiteralControl newline = new LiteralControl("<br/>");
                    if (mode == Mode.View)
                        chk.Enabled = false;
                    tcProof.Controls.Add(chk);
                    tcProof.Controls.Add(newline);
                }

                tr.Cells.Add(tcProofType);
                tr.Cells.Add(tcProof);

                tblDocuments.Rows.Add(tr);
            }

        }

        private void BindSchemeDetails()
        {


            schemeDetailsVo = LiabilitiesBo.GetSchemeDetails(schemeId);
            //if (mode == Mode.Edit || mode == Mode.Add || mode == Mode.View)
            //{

            txtSchemeName.Text = schemeDetailsVo.LoanSchemeName;

            txtMinLoanAmount.Text = schemeDetailsVo.MinimunLoanAmount.ToString("#0.00");

            txtMaxLoanAmount.Text = schemeDetailsVo.MaximumLoanAmount.ToString("#0.00");
            txtMinLoanPeriod.Text = schemeDetailsVo.MinimumLoanPeriod.ToString();
            txtMaxLoanPeriod.Text = schemeDetailsVo.MaximumLoanPeriod.ToString();
            txtPrimeLendingRate.Text = schemeDetailsVo.PLR.ToString("#0.00");
            txtMargin.Text = schemeDetailsVo.MarginMaintained.ToString("#0.00");

            txtMinAge.Text = schemeDetailsVo.MinimumAge.ToString();
            txtMaxAge.Text = schemeDetailsVo.MaximumAge.ToString();
            txtMinSalary.Text = schemeDetailsVo.MinimumSalary.ToString("#0.00");
            txtMinimumProfitAmount.Text = schemeDetailsVo.MinimumProfitAmount.ToString("#0.00");
            txtMinimumProfitPeriod.Text = schemeDetailsVo.MinimumProfitPeriod.ToString();
            txtRemarks.Text = schemeDetailsVo.Remark;


            ddlLoanPartner.SelectedValue = schemeDetailsVo.LoanPartner.ToString();
            ddlLoanType.SelectedValue = schemeDetailsVo.LoanType.ToString();

            BindBorrowerType(int.Parse(ddlLoanType.SelectedValue));
            ddlBorrowerType.SelectedValue = schemeDetailsVo.CustomerCategory.ToString();

            if (schemeDetailsVo.IsFloatingRateInterest)
            {
                rdoFloatingRate.SelectedValue = "1";
                gvAdviserInterestRates.HeaderRow.Cells[CONST_INTERESTE_RATE_COLUMN_ID].Text = "Diff. Interest Rate";
            }
            else
            {
                rdoFloatingRate.SelectedValue = "0";
                gvAdviserInterestRates.HeaderRow.Cells[CONST_INTERESTE_RATE_COLUMN_ID].Text = "Interest Rate";
            }
            //}
            if (mode == Mode.View)
            {



                txtSchemeName.Enabled = false;
                txtMinLoanAmount.Enabled = false;
                txtMaxLoanAmount.Enabled = false;
                txtMinLoanPeriod.Enabled = false;
                txtMaxLoanPeriod.Enabled = false;
                txtPrimeLendingRate.Enabled = false;
                txtMargin.Enabled = false;

                txtMinAge.Enabled = false;
                txtMaxAge.Enabled = false;
                txtMinSalary.Enabled = false;
                txtMinimumProfitAmount.Enabled = false;
                txtMinimumProfitPeriod.Enabled = false;
                txtRemarks.Enabled = false;


                ddlLoanPartner.Enabled = false;
                ddlLoanType.Enabled = false;

                ddlBorrowerType.Enabled = false;
                rdoFloatingRate.Enabled = false;

                //lblSchemeName.Text = schemeDetailsVo.LoanSchemeName;
                //lblMinLoanAmount.Text = schemeDetailsVo.MinimunLoanAmount.ToString();
                //lblMaxLoanAmount.Text = schemeDetailsVo.MaximumLoanAmount.ToString();
                //lblMinLoanPeriod.Text = schemeDetailsVo.MinimumLoanPeriod.ToString();
                //lblMaxLoanPeriod.Text = schemeDetailsVo.MaximumLoanPeriod.ToString();
                //lblPrimeLendingRate.Text = schemeDetailsVo.PLR.ToString();
                //lblMargin.Text = schemeDetailsVo.MarginMaintained.ToString();

                //lblMinAge.Text = schemeDetailsVo.MinimumAge.ToString();
                //lblMaxAge.Text = schemeDetailsVo.MaximumAge.ToString();
                //lblMinSalary.Text = schemeDetailsVo.MinimumSalary.ToString();
                //lblMinimumProfitAmount.Text = schemeDetailsVo.MinimumProfitAmount.ToString();
                //lblMinimumProfitPeriod.Text = schemeDetailsVo.MinimumProfitPeriod.ToString();
                //lblRemarks.Text = schemeDetailsVo.Remark;

                //lblBorrowerType.Text = GetBorrowerTypeName(schemeDetailsVo.CustomerCategory.ToString());
                //lblLoanPartner.Text = GetLoanPartnerName(schemeDetailsVo.LoanPartner.ToString());
                //lblLoanType.Text = GetLoanTypeName(schemeDetailsVo.LoanType.ToString());
                //if (schemeDetailsVo.IsFloatingRateInterest)
                //    lblFloatingRate.Text = "Yes";
                //else
                //    lblFloatingRate.Text = "No";
            }

            int bType = int.Parse(schemeDetailsVo.CustomerCategory.ToString());
            if (bType == 1 || bType == 2 || bType == 3) //|| bType == 5
            {
                tblIndividual1.Visible = true;
                tblNonIndividual1.Visible = false;
            }
            else
            {
                tblIndividual1.Visible = false;
                tblNonIndividual1.Visible = true;

            }
        }

        private void SetControlVisibility()
        {
            if (mode == Mode.View)
            {
                //txtSchemeName.Visible = false;
                //txtMinLoanAmount.Visible = false;
                //txtMaxLoanAmount.Visible = false;
                //txtMinLoanPeriod.Visible = false;
                //txtMaxLoanPeriod.Visible = false;
                //txtPrimeLendingRate.Visible = false;
                //txtMargin.Visible = false;

                //txtMinAge.Visible = false;
                //txtMaxAge.Visible = false;
                //txtMinSalary.Visible = false;
                //txtMinimumProfitAmount.Visible = false;
                //txtMinimumProfitPeriod.Visible = false;
                //txtRemarks.Visible = false;

                //ddlBorrowerType.Visible = false;
                //ddlLoanPartner.Visible = false;
                //ddlLoanType.Visible = false;
                //rdoFloatingRate.Visible = false;

                //lblSchemeName.Visible = true;
                //lblMinLoanAmount.Visible = true;
                //lblMaxLoanAmount.Visible = true;
                //lblMinLoanPeriod.Visible = true;
                //lblMaxLoanPeriod.Visible = true;
                //lblPrimeLendingRate.Visible = true;
                //lblMargin.Visible = true;

                //lblMinAge.Visible = true;
                //lblMaxAge.Visible = true;
                //lblMinSalary.Visible = true;
                //lblMinimumProfitAmount.Visible = true;
                //lblMinimumProfitPeriod.Visible = true;
                //lblRemarks.Visible = true;

                //lblBorrowerType.Visible = true;
                //lblLoanPartner.Visible = true;
                //lblLoanType.Visible = true;
                //lblFloatingRate.Visible = true;

            }

        }

        private void BindLoanPartner()
        {
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            DataTable dt = XMLBo.GetLoanPartner(path);
            ddlLoanPartner.DataSource = dt;
            ddlLoanPartner.DataValueField = dt.Columns["XLP_LoanPartnerCode"].ToString();
            ddlLoanPartner.DataTextField = dt.Columns["XLP_LoanPartner"].ToString();
            ddlLoanPartner.DataBind();
            ddlLoanPartner.Items.Insert(0, new ListItem("Select", "0"));
        }

        private string GetLoanPartnerName(string partnerId)
        {
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            DataTable dt = XMLBo.GetLoanPartner(path);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["XLP_LoanPartnerCode"].ToString() == partnerId)
                {
                    return dr["XLP_LoanPartner"].ToString();
                }
            }
            return string.Empty;

        }

        private string GetBorrowerTypeName(string partnerId)
        {
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            DataTable dt = XMLBo.GetCustomerCategory(path);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["XCC_CustomerCategoryCode"].ToString() == partnerId)
                {
                    return dr["XCC_CustomerCategory"].ToString();
                }
            }
            return string.Empty;

        }
        private string GetLoanTypeName(string loanTypeCode)
        {
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            DataTable dt = XMLBo.GetLoanType(path);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["XLT_LoanTypeCode"].ToString() == loanTypeCode)
                {
                    return dr["XLT_LoanType"].ToString();
                }
            }
            return string.Empty;

        }

        private void BindBorrowerType(int loanType)
        {
            //string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            //DataTable dt = XMLBo.GetCustomerCategory(path);
            DataTable dt = LiabilitiesBo.GetAllCustomerTypes(loanType);
            ddlBorrowerType.DataSource = dt;
            ddlBorrowerType.DataSource = dt;
            ddlBorrowerType.DataValueField = dt.Columns["XCC_CustomerCategoryCode"].ToString();
            ddlBorrowerType.DataTextField = dt.Columns["XCC_CustomerCategory"].ToString();
            ddlBorrowerType.DataBind();
            ddlBorrowerType.Items.Insert(0, new ListItem("Select", "0"));
        }

        private void BindLoanType()
        {
            // string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            DataTable dt = LiabilitiesBo.GetAllLoanTypes();      //XMLBo.GetLoanType(path);
            ddlLoanType.DataSource = dt;
            ddlLoanType.DataValueField = dt.Columns["XLT_LoanTypeCode"].ToString();
            ddlLoanType.DataTextField = dt.Columns["XLT_LoanType"].ToString();
            ddlLoanType.DataBind();
            ddlLoanType.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void gvAdviserInterestRates_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAdviserInterestRates.EditIndex = e.NewEditIndex;
            gvAdviserInterestRates.DataSource = (DataTable)ViewState["CurrentTable"];
            gvAdviserInterestRates.DataBind();
            //BindInterestRates();
        }

        protected void gvAdviserInterestRates_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];

            string editKey = gvAdviserInterestRates.DataKeys[e.RowIndex].Value.ToString();
            //   DataTable dtTemp = dt.Clone();

            for (int i = 0; i < gvAdviserInterestRates.Rows.Count; i++)
            {
                //DataRow dr = dtTemp.NewRow();
                if (dt.Rows[i]["ALSIR_LoanSchemeInterestRateId"].ToString() == editKey)
                {
                    //dtTemp.Rows.Add(dt.Rows[i]);

                    //dt.Rows[i].
                    //DataRow dr = dt.NewRow();

                    TableCellCollection row = gvAdviserInterestRates.Rows[i].Cells;

                    //dr["ALSIR_LoanSchemeInterestRateId"] = gvAdviserInterestRates.FindControl(""); // dt.Rows[i]["ALSIR_LoanSchemeInterestRateId"];
                    dt.Rows[i]["ALSIR_InterestCategory"] = ((TextBox)row[1].FindControl("txtCategory")).Text;
                    dt.Rows[i]["ALSIR_MinimumFinance"] = ((TextBox)row[2].FindControl("txtMinFinance")).Text;
                    dt.Rows[i]["ALSIR_MaximumFinance"] = ((TextBox)row[3].FindControl("txtMaxFinance")).Text;
                    dt.Rows[i]["ALSIR_MinimumPeriod"] = ((TextBox)row[4].FindControl("txtMinimumPeriod")).Text;
                    dt.Rows[i]["ALSIR_MaximumPeriod"] = ((TextBox)row[5].FindControl("txtMaximumPeriod")).Text;
                    dt.Rows[i]["ALSIR_DifferentialInterestRate"] = ((TextBox)row[6].FindControl("txtDifferentialInterestRate")).Text;
                    dt.Rows[i]["ALSIR_MaximumFinancePer"] = ((TextBox)row[7].FindControl("txtMaximumFinancePer")).Text;
                    dt.Rows[i]["ALSIR_ProcessingCharges"] = ((TextBox)row[8].FindControl("txtProcessingCharges")).Text;
                    dt.Rows[i]["ALSIR_PreClosingCharges"] = ((TextBox)row[9].FindControl("txtPreClosingCharges")).Text;
                    dt.Rows[i]["ALSIR_LoanSchemeInterestRateId"] = editKey;
                    //dt.Rows.Add(dr);
                }
            }
            ViewState["CurrentTable"] = dt;
            gvAdviserInterestRates.EditIndex = -1;
            gvAdviserInterestRates.DataSource = (DataTable)ViewState["CurrentTable"];
            gvAdviserInterestRates.DataBind();
            DisplayFooter();


        }
        private void SaveInterestRates()
        {
            SchemeInterestRateVo interestRateVo = new SchemeInterestRateVo();
            bool result = false;
            foreach (GridViewRow gvr in gvAdviserInterestRates.Rows)
            {

                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    DataControlRowState dcrs = gvr.RowState;
                    if (gvr.RowState == (DataControlRowState.Alternate | DataControlRowState.Edit) || gvr.RowState == DataControlRowState.Edit)
                    {
                        result = true;
                        break;
                    }

                }
            }
            if (!result)
            {
                UpdationIncomplete.Visible = false;
                for (int i = 0; i < gvAdviserInterestRates.Rows.Count; i++)
                {
                    if (gvAdviserInterestRates.Rows[i].RowType == DataControlRowType.DataRow && gvAdviserInterestRates.DataKeys[i].Value != "-1")
                    {
                        GridViewRow row = gvAdviserInterestRates.Rows[i];

                        interestRateVo.InterestCategory = ((Label)row.FindControl("lblCategory")).Text.Trim();
                        interestRateVo.MinimumFinance = TryParseFloat(((Label)row.FindControl("lblMinFinance")).Text.Trim());
                        interestRateVo.MaximumFinance = TryParseFloat(((Label)row.FindControl("lblMaxFinance")).Text.Trim());
                        interestRateVo.MinimumPeriod = TryParseInt(((Label)row.FindControl("lblMinimumPeriod")).Text.Trim());
                        interestRateVo.MaximumPeriod = TryParseInt(((Label)row.FindControl("lblMaximumPeriod")).Text.Trim());

                        interestRateVo.DifferentialInterestRate = TryParseFloat(((Label)row.FindControl("lblDifferentialInterestRate")).Text.Trim());
                        interestRateVo.PreClosingCharges = TryParseFloat(((Label)row.FindControl("lblPreClosingCharges")).Text.Trim());
                        interestRateVo.ProcessingCharges = TryParseFloat(((Label)row.FindControl("lblProcessingCharges")).Text.Trim());
                        interestRateVo.MaximumFinancePer = TryParseFloat(((Label)row.FindControl("lblMaximumFinancePer")).Text.Trim());
                        //if(mode == Mode.Add)
                        //    // bool isUpdated = LiabilitiesBo.UpdateInterestRate(interestRateVo);
                        //else

                        interestRateVo.LoanSchemeId = schemeId;

                        bool isUpdated = false;
                        if (gvAdviserInterestRates.DataKeys[i] != null && int.Parse(gvAdviserInterestRates.DataKeys[i].Value.ToString()) < 99999)
                        {
                            interestRateVo.CreatedBy = userVo.UserId;
                            interestRateVo.LoanSchemeInterestRateId = int.Parse(gvAdviserInterestRates.DataKeys[i].Value.ToString());
                            isUpdated = LiabilitiesBo.UpdateInterestRate(interestRateVo);

                        }
                        else
                        {
                            interestRateVo.ModifiedBy = userVo.UserId;
                            isUpdated = LiabilitiesBo.AddInterestRate(interestRateVo);
                        }
                        //schemeId = int.Parse(gvAdviserLoanSchemeView.DataKeys[selectedRow].Values["SchemeId"].ToString());

                        //gvAdviserInterestRates.EditIndex = -1;

                        // BindInterestRates();
                    }
                }
            }
            else
            {
                UpdationIncomplete.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            schemeDetailsVo.LoanSchemeName = txtSchemeName.Text;
            schemeDetailsVo.MinimunLoanAmount = TryParseDouble(txtMinLoanAmount.Text);
            schemeDetailsVo.MaximumLoanAmount = TryParseDouble(txtMaxLoanAmount.Text);
            schemeDetailsVo.MinimumLoanPeriod = TryParseInt(txtMinLoanPeriod.Text);
            schemeDetailsVo.MaximumLoanPeriod = TryParseInt(txtMaxLoanPeriod.Text);
            schemeDetailsVo.PLR = TryParseFloat(txtPrimeLendingRate.Text);
            schemeDetailsVo.MarginMaintained = TryParseInt(txtMargin.Text);

            //schemeDetailsVo.AdviserId = adviserVo.advisorId;
            //if (txtMinAge != null && txtMinAge.Text != string.Empty)
            schemeDetailsVo.MinimumAge = TryParseInt(txtMinAge.Text);
            //if (txtMaxAge != null && txtMaxAge.Text != string.Empty)
            schemeDetailsVo.MaximumAge = TryParseInt(txtMaxAge.Text);
            //if (txtMinSalary != null && txtMinSalary.Text != string.Empty)
            schemeDetailsVo.MinimumSalary = TryParseFloat(txtMinSalary.Text);
            //if (txtMinimumProfitAmount != null && txtMinimumProfitAmount.Text != string.Empty)
            schemeDetailsVo.MinimumProfitAmount = TryParseFloat(txtMinimumProfitAmount.Text);
            //if (txtMinimumProfitPeriod != null && txtMinimumProfitAmount.Text != string.Empty)
            schemeDetailsVo.MinimumProfitPeriod = TryParseInt(txtMinimumProfitPeriod.Text);
            schemeDetailsVo.Remark = txtRemarks.Text;

            schemeDetailsVo.CustomerCategory = TryParseInt(ddlBorrowerType.SelectedValue);
            schemeDetailsVo.LoanPartner = TryParseInt(ddlLoanPartner.SelectedValue);
            schemeDetailsVo.LoanType = TryParseInt(ddlLoanType.SelectedValue);
            if (rdoFloatingRate.SelectedValue == "1")
                schemeDetailsVo.IsFloatingRateInterest = true;
            else
                schemeDetailsVo.IsFloatingRateInterest = false;
            if (mode == Mode.Add)
            {
                schemeDetailsVo.CreatedBy = userVo.UserId;
                schemeId = LiabilitiesBo.CreateScheme(schemeDetailsVo);
                SaveInterestRates();
                bool isProofsUpdated = AddProofs();
            }
            else
            {
                schemeDetailsVo.LoanSchemeId = schemeId;
                schemeDetailsVo.ModifiedBy = userVo.UserId;
                Boolean isUpdated = LiabilitiesBo.UpdateScheme(schemeDetailsVo);
                SaveInterestRates();
                bool isProofsUpdated = AddProofs();
            }
            string url = "?mode=view";
            Session["LoanSchemeViewStatus"] = "View";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanSchemeView','" + url + "');", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscript", "loadcontrol('LoanSchemeView','');", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscript", "loadcontrol('SessionExpired','');", true);

        }

        private bool AddProofs()
        {
            LiabilitiesBo.DeleteProofs(schemeId);
            foreach (String key in Request.Params.AllKeys)
            {
                if (key.Contains("chkLoan-"))
                {
                    int proofCode = int.Parse(key.Substring(key.IndexOf("chkLoan-") + 8));
                    LiabilitiesBo.AddProofs(schemeId, proofCode, userVo.UserId);
                }

            }
            return false;
        }

        protected void ddlBorrowerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bType = int.Parse(ddlBorrowerType.SelectedValue);

            if (bType == 1 || bType == 2 || bType == 3) //|| bType == 5
            {
                tblIndividual1.Visible = true;
                tblNonIndividual1.Visible = false;
            }
            else
            {
                if (tblIndividual1 != null)
                {
                    tblIndividual1.Visible = false;
                    tblNonIndividual1.Visible = true;
                }

            }
            BindDocuments();
        }


        protected void gvAdviserInterestRates_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAdviserInterestRates.EditIndex = -1;
            gvAdviserInterestRates.DataSource = (DataTable)ViewState["CurrentTable"];
            gvAdviserInterestRates.DataBind();
            DisplayFooter();
            //BindInterestRates();

        }

        protected void lnkViewAll_Click(object sender, EventArgs e)
        {
            string url = "?mode=view";
            Session["LoanSchemeViewStatus"] = "View";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanSchemeView','" + url + "');", true);
        }

        //void gvAdviserInterestRates_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{

        //    GridViewRow row = gvAdviserInterestRates.Rows[e.RowIndex];
        //    SchemeInterestRateVo interestRateVo = new SchemeInterestRateVo();
        //    if (row != null)
        //    {
        //        //interestRateVo.InterestCategory = row.FindControl(
        //        //SchemeInterestRateVo
        //        //TextBox t = row.FindControl("TextBox1") as TextBox;
        //        //if (t != null)
        //        //{
        //        //    Response.Write("The Text Entered is" + t.Text);
        //        //}


        //    }

        #region HelperFuncations
        private float TryParseFloat(Object obj)
        {
            float floatVal = 0f;
            if (obj != null)
                float.TryParse(obj.ToString(), out floatVal);
            else
                return 0;
            return floatVal;
        }
        private int TryParseInt(Object obj)
        {
            int intVal = 0;
            if (obj != null && obj.ToString() != string.Empty)
                int.TryParse(obj.ToString(), out intVal);
            else
                return 0;
            return intVal;
        }
        private double TryParseDouble(Object obj)
        {
            double dblVal = 0.0;
            if (obj != null && obj.ToString() != string.Empty)
                double.TryParse(obj.ToString(), out dblVal);
            else
                return 0;
            return dblVal;
        }
        #endregion

        protected void ddlLoanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            //DataTable dt = XMLBo.GetCustomerCategory(path);
            DataTable dt = LiabilitiesBo.GetAllCustomerTypes(int.Parse(ddlLoanType.SelectedValue));
            ddlBorrowerType.DataSource = dt;
            ddlBorrowerType.DataValueField = dt.Columns["XCC_CustomerCategoryCode"].ToString();
            ddlBorrowerType.DataTextField = dt.Columns["XCC_CustomerCategory"].ToString();
            ddlBorrowerType.DataBind();
            ddlBorrowerType.Items.Insert(0, new ListItem("Select", "0"));
            tblDocuments.Visible = false;
        }

        protected void rdoFloatingRate_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rdoFloatingRate.SelectedValue == "0")
                gvAdviserInterestRates.HeaderRow.Cells[CONST_INTERESTE_RATE_COLUMN_ID].Text = "Interest Rate";
            else
                gvAdviserInterestRates.HeaderRow.Cells[CONST_INTERESTE_RATE_COLUMN_ID].Text = "Diff. Interest Rate";
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            if (Session["LoanSchemeView"]!=null && Session["LoanSchemeView"].ToString() == "SuperAdmin")
            {
                string url = "?schemeId=" + schemeId + "&mode=Edit";
                Session["LoanSchemeViewStatus"] = "Edit";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanScheme','" + url + "');", true);
            }
        }

    }

}