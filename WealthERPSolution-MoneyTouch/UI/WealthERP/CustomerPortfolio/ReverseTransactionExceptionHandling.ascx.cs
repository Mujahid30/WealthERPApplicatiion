using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using VoUser;

namespace WealthERP.CustomerPortfolio
{
    public partial class ReverseTransactionExceptionHandling : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCustomer_autoCompleteExtender.ContextKey = ((AdvisorVo)Session["advisorVo"]).advisorId.ToString();
                ShowTransactions();
            }
        }

        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            ShowCustomerPortfolios();
            ShowTransactions();
        }
        private void ShowCustomerPortfolios()
        {
            int customerId = int.Parse(txtCustomerId.Value.ToString());
            PortfolioBo portfolioBo = new PortfolioBo();
            List<CustomerPortfolioVo> customerPortfolioVoList = new List<CustomerPortfolioVo>();
            tdPortfolios.Visible = true;
            customerPortfolioVoList = portfolioBo.GetCustomerPortfolios(customerId);
            ddlPortfolios.Items.Clear();
            if (customerPortfolioVoList != null)
            {
                for (int i = 0; i < customerPortfolioVoList.Count; i++)
                {
                    ddlPortfolios.Items.Add(new ListItem(customerPortfolioVoList[i].PortfolioName, customerPortfolioVoList[i].PortfolioId.ToString()));
                    if (customerPortfolioVoList[i].IsMainPortfolio == 1)
                    {
                        ddlPortfolios.Items[i].Selected = true;
                    }
                }
            }
            else
            {
                ddlPortfolios.Items.Add(new ListItem("No Portfolios Available","NA"));               

            }


        }
        private void ShowTransactions()
        {
            gvRejectedTransactions.DataSource = null;
            gvRejectedTransactions.DataBind();
            gvOriginalTransactions.DataSource = null;
            gvOriginalTransactions.DataBind();
            pnlRejectedTransactions.Visible = false;
            pnlOriginalTransactions.Visible = false;
            lblRejectedTransactions.Visible = false;
            lblOriginalTransactions.Visible = false;
            tdOriginalTransactions.Visible = false;
            tdRejectedTransactions.Visible = false;
            btnMap.Visible = false;
            if (ddlPortfolios.SelectedValue.ToString() != "NA")
            {
                int adviserId = 0;
                if(Session["advisorVo"]!=null)
                    adviserId=((AdvisorVo)Session["advisorVo"]).advisorId;
                int portfolioId = 0;
                if (txtCustomerId.Value.ToString() != "" && ddlPortfolios.SelectedValue.ToString()!="NA")
                    portfolioId=int.Parse(ddlPortfolios.SelectedValue.ToString());
                List<MFTransactionVo> mfRejectTransactionVoList = new List<MFTransactionVo>();
                
                CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
                DataTable dtRejectedTransactions = new DataTable();
                
                DataRow drRejectedTransaction;
                
                

                dtRejectedTransactions.Columns.Add("MFTransId");
                dtRejectedTransactions.Columns.Add("CustomerName");
                dtRejectedTransactions.Columns.Add("Scheme");
                dtRejectedTransactions.Columns.Add("Folio");
                dtRejectedTransactions.Columns.Add("Date");
                dtRejectedTransactions.Columns.Add("Units");
                dtRejectedTransactions.Columns.Add("Price");
                dtRejectedTransactions.Columns.Add("Amount");
                dtRejectedTransactions.Columns.Add("TransactioType");
                

                
                    mfRejectTransactionVoList = customerTransactionBo.GetMFRejectTransactions(portfolioId,adviserId);

               
                if (mfRejectTransactionVoList != null)
                {
                    for (int i = 0; i < mfRejectTransactionVoList.Count; i++)
                    {
                        drRejectedTransaction = dtRejectedTransactions.NewRow();
                        drRejectedTransaction[0] = mfRejectTransactionVoList[i].TransactionId;
                        drRejectedTransaction[1] = mfRejectTransactionVoList[i].CustomerName;
                        drRejectedTransaction[2] = mfRejectTransactionVoList[i].SchemePlan;
                        drRejectedTransaction[3] = mfRejectTransactionVoList[i].Folio;
                        drRejectedTransaction[4] = mfRejectTransactionVoList[i].TransactionDate.ToShortDateString();
                        drRejectedTransaction[5] = mfRejectTransactionVoList[i].Units.ToString("f2");
                        drRejectedTransaction[6] = mfRejectTransactionVoList[i].Price.ToString("f2");
                        drRejectedTransaction[7] = mfRejectTransactionVoList[i].Amount.ToString("f2");
                        drRejectedTransaction[8] = mfRejectTransactionVoList[i].TransactionType;
                        dtRejectedTransactions.Rows.Add(drRejectedTransaction);

                    }
                    gvRejectedTransactions.DataSource = dtRejectedTransactions;
                    
                    gvRejectedTransactions.DataBind();
                    //((RadioButton)gvRejectedTransactions.Rows[0].Cells[0].FindControl("rdRejectId")).Checked = true;
                    gvRejectedTransactions.Visible = true;
                    lblRejectedTransactions.Visible = true;
                    pnlRejectedTransactions.Visible = true;
                    tdRejectedTransactions.Visible = true;
                    lblRejectedTransactions.Text = "Select a Rejected Transaction To Map";
                }
                else
                {
                    lblRejectedTransactions.Visible = true;
                    lblRejectedTransactions.Text = "No Rejection Transactions Available";
                    pnlRejectedTransactions.Visible = false;
                    tdRejectedTransactions.Visible = false;
                   
                }
                
            }
            else
            {
              
                gvRejectedTransactions.Visible = false;
                lblRejectedTransactions.Visible = false;
                tdRejectedTransactions.Visible = false;
                pnlRejectedTransactions.Visible = false;
                
                
            }
            
        }
        

        protected void ddlPortfolios_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTransactions();
        }
        private void ShowOriginalTransactions(int MFTransId)
        {
            bool isAvailable = true;
            if (ddlPortfolios.SelectedValue.ToString() != "NA")
            {
                //int portfolioId = int.Parse(ddlPortfolios.SelectedValue.ToString());
                
                List<MFTransactionVo> mfOriginalTransactionVoList = new List<MFTransactionVo>();
                CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
                
                DataTable dtOriginalTransactions = new DataTable();
                
                DataRow drOriginalTransaction;



                dtOriginalTransactions.Columns.Add("MFTransId");
                dtOriginalTransactions.Columns.Add("Scheme");
                dtOriginalTransactions.Columns.Add("Folio");
                dtOriginalTransactions.Columns.Add("Date");
                dtOriginalTransactions.Columns.Add("Units");
                dtOriginalTransactions.Columns.Add("Price");
                dtOriginalTransactions.Columns.Add("Amount");
                dtOriginalTransactions.Columns.Add("TransactioType");

              
                mfOriginalTransactionVoList = customerTransactionBo.GetMFOriginalTransactions(MFTransId);
                
                if (mfOriginalTransactionVoList != null)
                {

                    for (int i = 0; i < mfOriginalTransactionVoList.Count; i++)
                    {
                        drOriginalTransaction = dtOriginalTransactions.NewRow();
                        drOriginalTransaction[0] = mfOriginalTransactionVoList[i].TransactionId;
                        drOriginalTransaction[1] = mfOriginalTransactionVoList[i].SchemePlan;
                        drOriginalTransaction[2] = mfOriginalTransactionVoList[i].Folio;
                        drOriginalTransaction[3] = mfOriginalTransactionVoList[i].TransactionDate.ToShortDateString();
                        drOriginalTransaction[4] = mfOriginalTransactionVoList[i].Units.ToString("f2");
                        drOriginalTransaction[5] = mfOriginalTransactionVoList[i].Price.ToString("f2");
                        drOriginalTransaction[6] = mfOriginalTransactionVoList[i].Amount.ToString("f2");
                        drOriginalTransaction[7] = mfOriginalTransactionVoList[i].TransactionType;
                        dtOriginalTransactions.Rows.Add(drOriginalTransaction);

                    }
                    gvOriginalTransactions.DataSource = dtOriginalTransactions;

                    gvOriginalTransactions.DataBind();
                    ((RadioButton)gvOriginalTransactions.Rows[0].Cells[0].FindControl("rdOriginalId")).Checked = true;
                    gvOriginalTransactions.Visible = true;
                    lblOriginalTransactions.Visible = true;
                    pnlOriginalTransactions.Visible = true;
                    lblOriginalTransactions.Text = "Similar Original Transactions. Select Matching Transaction and Click on the button 'Map Transaction below'";
                    tdOriginalTransactions.Visible = true;
                }
                else
                {
                    lblOriginalTransactions.Visible = true;
                    lblOriginalTransactions.Text = "No Similar Original Transactions Available";
                    pnlOriginalTransactions.Visible = false;
                    tdOriginalTransactions.Visible = false;
                    isAvailable = false;
                }
            }
            else
            {
                gvOriginalTransactions.Visible = false;
                
                
                lblOriginalTransactions.Visible = false;
               
                pnlOriginalTransactions.Visible = false;
                tdOriginalTransactions.Visible = false;
                isAvailable = false;
            }
            if (isAvailable)
            {
                btnMap.Visible = true;
            }
            else
            {
                btnMap.Visible = false;
            }
        }
        protected void gvRejectedTransactions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void rdRejectId_CheckedChanged(object sender, EventArgs e)
        {
            int MfTransId=0;
            foreach (GridViewRow row in gvRejectedTransactions.Rows)
            {
                if (((RadioButton)row.FindControl("rdRejectId")) != (RadioButton)sender)
                {
                    ((RadioButton)row.FindControl("rdRejectId")).Checked = false;
                }
                else
                {
                    ((RadioButton)row.FindControl("rdRejectId")).Checked = true;
                    MfTransId = int.Parse(gvRejectedTransactions.DataKeys[row.RowIndex].Value.ToString());
                }
            }
            ShowOriginalTransactions(MfTransId);
            
        }
        protected void rdOriginalId_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvOriginalTransactions.Rows)
            {
                if (((RadioButton)row.FindControl("rdOriginalId")) != (RadioButton)sender)
                    ((RadioButton)row.FindControl("rdOriginalId")).Checked = false;
                else
                    ((RadioButton)row.FindControl("rdOriginalId")).Checked = true;
            }

        }

        protected void btnMap_Click(object sender, EventArgs e)
        {
            int MfTransId = 0;
            int OriginalTransId = 0;
            bool bResult=false;
            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
            foreach (GridViewRow row in gvRejectedTransactions.Rows)
            {
                if (((RadioButton)row.FindControl("rdRejectId")).Checked)
                {
                    MfTransId=int.Parse(gvRejectedTransactions.DataKeys[row.RowIndex].Value.ToString());
                    break;
                }
            }
            foreach (GridViewRow row in gvOriginalTransactions.Rows)
            {
                if (((RadioButton)row.FindControl("rdOriginalId")).Checked)
                {
                    OriginalTransId = int.Parse(gvOriginalTransactions.DataKeys[row.RowIndex].Value.ToString());
                    break;
                }
            }
            bResult = customerTransactionBo.UpdateRejectedTransactionStatus(MfTransId, OriginalTransId);

            if (bResult)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Transactions Updated Succesfully');", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Transactions Not Updated');", true);

            }
            ShowTransactions();
        }

        protected void rbtnAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAll.Checked)
            {
                txtCustomerId.Value = "";
                ddlPortfolios.Items.Clear();
                tblCustomerSearch.Visible=false;
                ShowTransactions();
            }
            else
            {
                tblCustomerSearch.Visible = true;
            }
        }

    }
}