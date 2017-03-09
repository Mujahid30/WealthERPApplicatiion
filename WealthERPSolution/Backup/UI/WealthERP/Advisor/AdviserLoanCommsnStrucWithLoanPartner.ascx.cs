using System;
using System.Web.UI.WebControls;
using VoUser;
using BoAdvisorProfiling;
using BoCommon;
using System.Configuration;
using System.Data;
using VoAdvisorProfiling;
using System.Collections.Generic;


namespace WealthERP.Advisor
{
    public partial class AdviserLoanCommsnStrucWithLoanPartner : System.Web.UI.UserControl
    {

        string path = "";
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        AdvisorBo advisorBo = new AdvisorBo();
        List<AdviserLoanCommsnStrucWithLoanPartnerVo> AdviserLoanCommsnStrucWithLoanPartnerVoList = new List<AdviserLoanCommsnStrucWithLoanPartnerVo>();
        int dsCount;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            this.Page.Culture = "en-GB";
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            userVo = (UserVo)Session["userVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];

            if (!IsPostBack)
            {
                Bindgrid();
            }
        }

        protected void gvLnCommsnStrucLnPtr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlAddLoanPartner;
                DropDownList ddlAddLoanType;
                DropDownList ddlSchemeName;

                ddlAddLoanPartner = e.Row.FindControl("ddlAddLoanPartner") as DropDownList;
                if (ddlAddLoanPartner != null)
                {
                    ddlAddLoanPartner.DataSource = XMLBo.GetLoanPartner(path);
                    ddlAddLoanPartner.DataTextField = "XLP_LoanPartner";
                    ddlAddLoanPartner.DataValueField = "XLP_LoanPartnerCode";
                    ddlAddLoanPartner.DataBind();
                    ddlAddLoanPartner.Items.Insert(0, "Select Loan Partner");
                }

                ddlAddLoanType = e.Row.FindControl("ddlAddLoanType") as DropDownList;
                if (ddlAddLoanType != null)
                {
                    ddlAddLoanType.DataSource = XMLBo.GetLoanType(path);
                    ddlAddLoanType.DataTextField = "XLT_LoanType";
                    ddlAddLoanType.DataValueField = "XLT_LoanTypeCode";
                    ddlAddLoanType.DataBind();
                    ddlAddLoanType.Items.Insert(0, "Select Loan Type");
                }

                ddlSchemeName = e.Row.FindControl("ddlAddSchemeName") as DropDownList;
                ddlSchemeName.Items.Insert(0, "Select Scheme");

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (dsCount != 0)
                {
                    DropDownList ddlLoanPartner;
                    DropDownList ddlLoanType;
                    DropDownList ddlSchemeName;

                    AdviserLoanCommsnStrucWithLoanPartnerVo adviserLoanCommsnStrucWithLoanPartnervo = new AdviserLoanCommsnStrucWithLoanPartnerVo();
                    adviserLoanCommsnStrucWithLoanPartnervo = AdviserLoanCommsnStrucWithLoanPartnerVoList[gvLnCommsnStrucLnPtr.Rows.Count];

                    ddlLoanPartner = e.Row.FindControl("ddlLoanPartner") as DropDownList;
                    if (ddlLoanPartner != null)
                    {
                        ddlLoanPartner.DataSource = XMLBo.GetLoanPartner(path);
                        ddlLoanPartner.DataTextField = "XLP_LoanPartner";
                        ddlLoanPartner.DataValueField = "XLP_LoanPartnerCode";
                        ddlLoanPartner.DataBind();
                        ddlLoanPartner.SelectedValue = adviserLoanCommsnStrucWithLoanPartnervo.LoanPartnerCode.ToString();
                    }

                    ddlLoanType = e.Row.FindControl("ddlLoanType") as DropDownList;
                    if (ddlLoanType != null)
                    {
                        ddlLoanType.DataSource = XMLBo.GetLoanType(path);
                        ddlLoanType.DataTextField = "XLT_LoanType";
                        ddlLoanType.DataValueField = "XLT_LoanTypeCode";
                        ddlLoanType.DataBind();
                        ddlLoanType.SelectedValue = adviserLoanCommsnStrucWithLoanPartnervo.LoanTypeCode.ToString();
                    }

                    AdviserLoanCommsnStrucWithLoanPartnerBo AdviserLoanCommsnStrucWithLoanPartner = new AdviserLoanCommsnStrucWithLoanPartnerBo();
                    DataSet ds1 = AdviserLoanCommsnStrucWithLoanPartner.GetAdviserLoanSchemeNameForLnPtnrLnType(int.Parse(ddlLoanType.SelectedValue), int.Parse(ddlLoanPartner.SelectedValue), adviserVo.advisorId);
                    ddlSchemeName = e.Row.FindControl("ddlSchemeName") as DropDownList;
                    if (ddlSchemeName != null)
                    {
                        
                        ddlSchemeName.DataSource = ds1;
                        ddlSchemeName.DataTextField = "LoanSchemeName";
                        ddlSchemeName.DataValueField = "LoanSchemeId";
                        ddlSchemeName.DataBind();
                        ddlSchemeName.SelectedValue = adviserLoanCommsnStrucWithLoanPartnervo.LoanSchemeId.ToString();
                    }
                }
            }
           
        }

        protected void ddlAddLoanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAddLoanPartner;
            DropDownList ddlAddLoanType;
            DropDownList ddlSchemeName;

            ddlAddLoanPartner = gvLnCommsnStrucLnPtr.FooterRow.FindControl("ddlAddLoanPartner") as DropDownList;
            ddlAddLoanType = gvLnCommsnStrucLnPtr.FooterRow.FindControl("ddlAddLoanType") as DropDownList;

            int Loantype = 0;
            int LoanPartner = 0;
            if (ddlAddLoanType.SelectedItem.Value.ToString() != "Select Loan Type")
            {
                Loantype = Convert.ToInt32(ddlAddLoanType.SelectedItem.Value.ToString());
            }
            if (ddlAddLoanPartner.SelectedItem.Value.ToString() != "Select Loan Partner")
            {
                LoanPartner = Convert.ToInt32(ddlAddLoanPartner.SelectedItem.Value.ToString());
            }

            AdviserLoanCommsnStrucWithLoanPartnerBo AdviserLoanCommsnStrucWithLoanPartner = new AdviserLoanCommsnStrucWithLoanPartnerBo();
            DataSet ds = AdviserLoanCommsnStrucWithLoanPartner.GetAdviserLoanSchemeNameForLnPtnrLnType(Loantype, LoanPartner, adviserVo.advisorId);

            ddlSchemeName = gvLnCommsnStrucLnPtr.FooterRow.FindControl("ddlAddSchemeName") as DropDownList;
            ddlSchemeName.DataSource = ds;
            ddlSchemeName.DataTextField = "LoanSchemeName";
            ddlSchemeName.DataValueField = "LoanSchemeId";
            ddlSchemeName.DataBind();
            ddlSchemeName.Items.Insert(0, "Select Scheme");
        }

        private void Bindgrid()
        {
            DataSet ds;
            AdviserLoanCommsnStrucWithLoanPartnerBo AdviserLoanCommsnStrucWithLoanPartner = new AdviserLoanCommsnStrucWithLoanPartnerBo();

            ds = AdviserLoanCommsnStrucWithLoanPartner.GetAdvisorLoanPartnerCommissionForAdviser(adviserVo.advisorId);
            if (ds.Tables[0].Rows.Count == 0)
            {
                dsCount = 0;
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt = GetGridviewTable();
                dr = dt.NewRow();
                dt.Rows.Add(dr);

                gvLnCommsnStrucLnPtr.DataSource = dt;
                gvLnCommsnStrucLnPtr.DataBind();
                gvLnCommsnStrucLnPtr.Rows[0].Visible = false;
            }
            
            else
            {
                dsCount = ds.Tables[0].Rows.Count;
                lblNoOfRows.Text = dsCount.ToString();
                gvLnCommsnStrucLnPtr.DataSource = ds;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    AdviserLoanCommsnStrucWithLoanPartnerVo adviserLoanCommsnStrucWithLoanPartnerVo = new AdviserLoanCommsnStrucWithLoanPartnerVo();
                    adviserLoanCommsnStrucWithLoanPartnerVo.LoanPartnerCode = int.Parse(dr["LoanPartnerCode"].ToString());
                    adviserLoanCommsnStrucWithLoanPartnerVo.LoanTypeCode = int.Parse(dr["LoanTypeCode"].ToString());
                    adviserLoanCommsnStrucWithLoanPartnerVo.LoanSchemeId = int.Parse(dr["LoanSchemeId"].ToString());
                    adviserLoanCommsnStrucWithLoanPartnerVo.CommissionFee = float.Parse(dr["CommissionFee"].ToString());
                    adviserLoanCommsnStrucWithLoanPartnerVo.SlabUpperLimit = float.Parse(dr["SlabUpperLimit"].ToString());
                    adviserLoanCommsnStrucWithLoanPartnerVo.SlabLowerLimit = float.Parse(dr["SlabLowerLimit"].ToString());
                    adviserLoanCommsnStrucWithLoanPartnerVo.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                    adviserLoanCommsnStrucWithLoanPartnerVo.EndDate = DateTime.Parse(dr["EndDate"].ToString());

                    AdviserLoanCommsnStrucWithLoanPartnerVoList.Add(adviserLoanCommsnStrucWithLoanPartnerVo);
                }
                gvLnCommsnStrucLnPtr.DataBind();
            }
            
            if (ds.Tables[0].Rows.Count == 0)
            {
                lblNoOfRows.Text = "0";
            }
        }

        private DataTable GetGridviewTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("ALPC_Id", typeof(string)));
            dt.Columns.Add(new DataColumn("LoanPartner", typeof(string)));
            dt.Columns.Add(new DataColumn("LoanType", typeof(string)));
            dt.Columns.Add(new DataColumn("SchemeName", typeof(string)));
            dt.Columns.Add(new DataColumn("CommissionFee", typeof(string)));
            dt.Columns.Add(new DataColumn("SlabLowerLimit", typeof(string)));
            dt.Columns.Add(new DataColumn("SlabUpperLimit", typeof(string)));
            dt.Columns.Add(new DataColumn("StartDate", typeof(string)));
            dt.Columns.Add(new DataColumn("EndDate", typeof(string)));
            
            return dt;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                dsCount = int.Parse(lblNoOfRows.Text);
            }
            if (trInsertError.Visible == true)
                trInsertError.Visible = false;

            if (trUpdateError.Visible == true)
                trUpdateError.Visible = false;

            if (trValueInserted.Visible == true)
                trValueInserted.Visible = false;

            if (trValuesUpdated.Visible == true)
                trValuesUpdated.Visible = false;

            bool UpdateResult = true;
            AdviserLoanCommsnStrucWithLoanPartnerVo AdviserLoanCommsnStrucWithLoanPartner = new AdviserLoanCommsnStrucWithLoanPartnerVo();
            AdviserLoanCommsnStrucWithLoanPartnerBo adviserLoanCommsnStrucWithLoanPartnerbo = new AdviserLoanCommsnStrucWithLoanPartnerBo();

            if (dsCount > 0)
            {
                foreach (GridViewRow gvRow in gvLnCommsnStrucLnPtr.Rows)
                {
                    TextBox txtCommissionFee = gvRow.FindControl("txtCommissionFee") as TextBox;
                    TextBox txtSlabLowerLimit = gvRow.FindControl("txtSlabLowerLimit") as TextBox;
                    TextBox txtSlabUpperLimit = gvRow.FindControl("txtSlabUpperLimit") as TextBox;
                    TextBox txtStartDate = gvRow.FindControl("txtStartDate") as TextBox;
                    TextBox txtEndDate = gvRow.FindControl("txtEndDate") as TextBox;
                    DropDownList ddlLoanPartner = gvRow.FindControl("ddlLoanPartner") as DropDownList;
                    DropDownList ddlLoanType = gvRow.FindControl("ddlLoanType") as DropDownList;
                    DropDownList ddlSchemeName = gvRow.FindControl("ddlSchemeName") as DropDownList;

                    try
                    {
                        AdviserLoanCommsnStrucWithLoanPartner.AdviserId = adviserVo.advisorId;
                        AdviserLoanCommsnStrucWithLoanPartner.Id = int.Parse(gvLnCommsnStrucLnPtr.DataKeys[gvRow.RowIndex].Value.ToString());
                        AdviserLoanCommsnStrucWithLoanPartner.LoanPartnerCode = int.Parse(ddlLoanPartner.SelectedItem.Value.ToString());
                        AdviserLoanCommsnStrucWithLoanPartner.LoanTypeCode = int.Parse(ddlLoanType.SelectedItem.Value.ToString());

                        if (ddlSchemeName.SelectedItem.Value != "Select Scheme")
                            AdviserLoanCommsnStrucWithLoanPartner.LoanSchemeId = int.Parse(ddlSchemeName.SelectedItem.Value.ToString());

                        AdviserLoanCommsnStrucWithLoanPartner.CommissionFee = float.Parse(txtCommissionFee.Text);
                        AdviserLoanCommsnStrucWithLoanPartner.SlabUpperLimit = float.Parse(txtSlabUpperLimit.Text);
                        AdviserLoanCommsnStrucWithLoanPartner.SlabLowerLimit = float.Parse(txtSlabLowerLimit.Text);
                        AdviserLoanCommsnStrucWithLoanPartner.StartDate = DateTime.Parse(txtStartDate.Text);
                        AdviserLoanCommsnStrucWithLoanPartner.EndDate = DateTime.Parse(txtEndDate.Text);
                        AdviserLoanCommsnStrucWithLoanPartner.ModifiedBy = userVo.UserId;
                        AdviserLoanCommsnStrucWithLoanPartner.ModifiedOn = DateTime.Now;

                        if (ddlSchemeName.SelectedItem.Value != "Select Scheme" && txtCommissionFee.Text.Trim() != ""
                            && txtSlabLowerLimit.Text.Trim() != "" && txtSlabUpperLimit.Text.Trim() != ""
                                && txtEndDate.Text.Trim() != "" && txtStartDate.Text.Trim() != "")
                        {
                            bool result = adviserLoanCommsnStrucWithLoanPartnerbo.UpdateAdviserLoanSchemeNameForLnPtnrLnType(AdviserLoanCommsnStrucWithLoanPartner);
                        }
                        else
                        {
                            trUpdateError.Visible = true;
                            gvRow.BackColor = System.Drawing.Color.Red;
                            gvRow.CssClass = "selected";
                            UpdateResult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        trUpdateError.Visible = true;
                        gvRow.BackColor = System.Drawing.Color.Red;
                        gvRow.CssClass = "selected";
                        UpdateResult = false;
                    }
                }
            }


            bool insertresult= false;
            if (UpdateResult == true)
            {
                if (dsCount != 0)
                {
                    trValuesUpdated.Visible = true;
                }
                insertresult = InsertNew();
                Bindgrid();
            }
            if (insertresult == true && UpdateResult == true)
            {
                Bindgrid();
                trValueInserted.Visible = true;
            }
            

        }

        private bool InsertNew()
        {
            bool result = false;

            TextBox txtAddCommissionFee = gvLnCommsnStrucLnPtr.FooterRow.FindControl("txtAddCommissionFee") as TextBox;
            TextBox txtAddSlabLowerLimit = gvLnCommsnStrucLnPtr.FooterRow.FindControl("txtAddSlabLowerLimit") as TextBox;
            TextBox txtAddSlabUpperLimit = gvLnCommsnStrucLnPtr.FooterRow.FindControl("txtAddSlabUpperLimit") as TextBox;
            TextBox txtAddStartDate = gvLnCommsnStrucLnPtr.FooterRow.FindControl("txtAddStartDate") as TextBox;
            TextBox txtAddEndDate = gvLnCommsnStrucLnPtr.FooterRow.FindControl("txtAddEndDate") as TextBox;
            DropDownList ddlAddLoanPartner = gvLnCommsnStrucLnPtr.FooterRow.FindControl("ddlAddLoanPartner") as DropDownList;
            DropDownList ddlAddLoanType = gvLnCommsnStrucLnPtr.FooterRow.FindControl("ddlAddLoanType") as DropDownList;
            DropDownList ddlAddSchemeName = gvLnCommsnStrucLnPtr.FooterRow.FindControl("ddlAddSchemeName") as DropDownList;

            AdviserLoanCommsnStrucWithLoanPartnerVo AdviserLoanCommsnStrucWithLoanPartner = new AdviserLoanCommsnStrucWithLoanPartnerVo();
            AdviserLoanCommsnStrucWithLoanPartnerBo adviserLoanCommsnStrucWithLoanPartnerbo = new AdviserLoanCommsnStrucWithLoanPartnerBo();

            if (ddlAddLoanPartner.SelectedItem.Value != "Select Loan Partner" && ddlAddLoanType.SelectedItem.Value != "Select Loan Type"
                && ddlAddSchemeName.SelectedItem.Value != "Select Scheme" && txtAddCommissionFee.Text.Trim() != ""
                && txtAddSlabLowerLimit.Text.Trim() != "" && txtAddSlabUpperLimit.Text.Trim() != ""
                && txtAddEndDate.Text.Trim() != "" && txtAddStartDate.Text.Trim() != "")
            {
                try
                {
                    AdviserLoanCommsnStrucWithLoanPartner.AdviserId = adviserVo.advisorId;
                    AdviserLoanCommsnStrucWithLoanPartner.LoanPartnerCode = int.Parse(ddlAddLoanPartner.SelectedItem.Value.ToString());
                    AdviserLoanCommsnStrucWithLoanPartner.LoanTypeCode = int.Parse(ddlAddLoanType.SelectedItem.Value.ToString());
                    AdviserLoanCommsnStrucWithLoanPartner.LoanSchemeId = int.Parse(ddlAddSchemeName.SelectedItem.Value.ToString());
                    AdviserLoanCommsnStrucWithLoanPartner.CommissionFee = float.Parse(txtAddCommissionFee.Text);
                    AdviserLoanCommsnStrucWithLoanPartner.SlabUpperLimit = float.Parse(txtAddSlabUpperLimit.Text);
                    AdviserLoanCommsnStrucWithLoanPartner.SlabLowerLimit = float.Parse(txtAddSlabLowerLimit.Text);
                    AdviserLoanCommsnStrucWithLoanPartner.StartDate = DateTime.Parse(txtAddStartDate.Text);
                    AdviserLoanCommsnStrucWithLoanPartner.EndDate = DateTime.Parse(txtAddEndDate.Text);
                    AdviserLoanCommsnStrucWithLoanPartner.CreatedBy = userVo.UserId;
                    AdviserLoanCommsnStrucWithLoanPartner.CreatedOn = DateTime.Now;
                    AdviserLoanCommsnStrucWithLoanPartner.ModifiedBy = userVo.UserId;
                    AdviserLoanCommsnStrucWithLoanPartner.ModifiedOn = DateTime.Now;
                    result = adviserLoanCommsnStrucWithLoanPartnerbo.InsertAdviserLoanSchemeNameForLnPtnrLnType(AdviserLoanCommsnStrucWithLoanPartner);
                    trValueInserted.Visible = true;
                    result = true;
                }
                catch(Exception ex)
                {
                    trInsertError.Visible = true;
                    gvLnCommsnStrucLnPtr.FooterRow.BackColor = System.Drawing.Color.Red;
                    result = false;
                }

                
            }
            else if (ddlAddLoanPartner.SelectedItem.Value == "Select Loan Partner" && ddlAddLoanType.SelectedItem.Value == "Select Loan Type"
                && ddlAddSchemeName.SelectedItem.Value == "Select Scheme" && txtAddCommissionFee.Text.Trim() == ""
                && txtAddSlabLowerLimit.Text.Trim() == "" && txtAddSlabUpperLimit.Text.Trim() == ""
                && txtAddEndDate.Text.Trim() == "" && txtAddStartDate.Text.Trim() == "")
            {


                if (dsCount == 0)
                {
                    result = false;
                    trInsertError.Visible = true;
                }
            }
            else
            {
                if (gvLnCommsnStrucLnPtr.Rows.Count == 0)
                {
                    if (ddlAddLoanPartner.SelectedItem.Value == "Select Loan Partner" && ddlAddLoanType.SelectedItem.Value == "Select Loan Type"
                        && ddlAddSchemeName.SelectedItem.Value == "Select Scheme" && txtAddCommissionFee.Text.Trim() == ""
                        && txtAddSlabLowerLimit.Text.Trim() == "" && txtAddSlabUpperLimit.Text.Trim() == ""
                        && txtAddEndDate.Text.Trim() == "" && txtAddStartDate.Text.Trim() == "")
                    trInsertError.Visible = true;
                    gvLnCommsnStrucLnPtr.FooterRow.BackColor = System.Drawing.Color.Red;

                    result = false;
                }
                trValuesUpdated.Visible = false;
                trInsertError.Visible = true;
                gvLnCommsnStrucLnPtr.FooterRow.BackColor = System.Drawing.Color.Red;
                gvLnCommsnStrucLnPtr.CssClass = "selected";
            }
            


            return result;
        }

        protected void gvLnCommsnStrucLnPtr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLnCommsnStrucLnPtr.PageIndex = e.NewPageIndex;
            Bindgrid();
        }
        
        protected void ddlLoanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow gvr = (GridViewRow)ddl.Parent.Parent;
            DropDownList ddlLoanPartner;
            DropDownList ddlLoanType;
            DropDownList ddlSchemeName;

            ddlLoanPartner = gvr.FindControl("ddlLoanPartner") as DropDownList;
            ddlLoanType = gvr.FindControl("ddlLoanType") as DropDownList;

            int Loantype = Convert.ToInt32(ddlLoanType.SelectedItem.Value.ToString());
            int LoanPartner = Convert.ToInt32(ddlLoanPartner.SelectedItem.Value.ToString());

            AdviserLoanCommsnStrucWithLoanPartnerBo AdviserLoanCommsnStrucWithLoanPartner = new AdviserLoanCommsnStrucWithLoanPartnerBo();
            DataSet ds = AdviserLoanCommsnStrucWithLoanPartner.GetAdviserLoanSchemeNameForLnPtnrLnType(Loantype, LoanPartner, adviserVo.advisorId);

            ddlSchemeName = gvr.FindControl("ddlSchemeName") as DropDownList;

            ddlSchemeName.DataSource = ds;
            ddlSchemeName.DataBind();
            ddlSchemeName.Items.Insert(0, "Select Scheme");


        }

        protected void gvRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int advLnPrtnrId = 0;
            AdviserLoanCommsnStrucWithLoanPartnerBo adviserLoanCommsnStrucWithLoanPartnerbo = new AdviserLoanCommsnStrucWithLoanPartnerBo();

            advLnPrtnrId = Convert.ToInt32(gvLnCommsnStrucLnPtr.DataKeys[e.RowIndex].Value);

            bool result = adviserLoanCommsnStrucWithLoanPartnerbo.DeleteAdvisorLoanPartnerCommission(advLnPrtnrId);

            if (result)
            {
                Bindgrid();
            }
        }
    }
}