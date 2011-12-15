using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VoCustomerProfiling;
using BoCustomerProfiling;
using VoUser;
using BoUser;
using PCGMailLib;
using System.Net.Mail;
using System.Configuration;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WealthERP.Base;



namespace WealthERP.Customer
{
    public partial class CustomerAssumptionsPreferencesSetup : System.Web.UI.UserControl
    {
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();
        RMVo rmVo = new RMVo();        
        AdvisorVo adviserVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        int expiryAge = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["CustomerVo"];
            adviserVo = (AdvisorVo)Session["AdvisorVo"];
            //DataTable dtAssumption = new DataTable();
            //dtAssumption.Columns.Add("WA_AssumptionName");
            //dtAssumption.Columns.Add("CPA_Value");
            //DataRow drAssumption;
            //drAssumption = dtAssumption.NewRow();
            //drAssumption["WA_AssumptionName"] = "Life Expiracy";
            //drAssumption["CPA_Value"] = "65";
            //dtAssumption.Rows.Add(drAssumption);
            //RadGrid1.DataSource = dtAssumption;
            //RadGrid1.DataBind();
            BindAllCustomerAssumptions();
            
            //DataTable dtBindDropDownassumption = new DataTable();
            //dtBindDropDownassumption = customerBo.BindDropDownassumption(flag);
            //ddlPickAssumptions.DataSource = dtBindDropDownassumption;
            //ddlPickAssumptions.DataTextField = dtBindDropDownassumption.Columns["WA_AssumptionName"].ToString();
            //ddlPickAssumptions.DataValueField = dtBindDropDownassumption.Columns["WA_AssumptionId"].ToString();
            //ddlPickAssumptions.DataBind();
            //ddlPickAssumptions.Items.Insert(0, new ListItem("-Select-", "-Select-"));
            SetDefaultPlanRetirementValueForCustomer();

        }




        public void BindAllCustomerAssumptions()
        {
            DataSet dsBindAllCustomerAssumptions;

            DataTable dtProjectedAssumption = new DataTable();
            dsBindAllCustomerAssumptions = customerBo.GetAllCustomersAssumptions(customerVo.CustomerId,adviserVo.advisorId);
            //dtProjectedAssumption = AssumptionTableCreation(dsBindAllCustomerAssumptions.Tables[1]);
            DataTable dtAssumption = new DataTable();
            dtAssumption.Columns.Add("WA_AssumptionName");
            dtAssumption.Columns.Add("CPA_Value");
            dtAssumption.Columns.Add("WA_AssumptionId");
            DataRow drAssumption;
            foreach (DataRow drStaticAssumption in dsBindAllCustomerAssumptions.Tables[0].Rows)
            {
                drAssumption = dtAssumption.NewRow();
                drAssumption["WA_AssumptionName"] = drStaticAssumption["WA_AssumptionName"].ToString();
                drAssumption["CPA_Value"] = drStaticAssumption["CSA_Value"].ToString();
                drAssumption["WA_AssumptionId"] = drStaticAssumption["WA_AssumptionId"].ToString();
                dtAssumption.Rows.Add(drAssumption);
            }

            DataRow[] drassumptionLE;
            drassumptionLE = dtAssumption.Select("WA_AssumptionId='" + "LE" + "'");
            DataRow[] drassumptionRA;
            drassumptionRA = dtAssumption.Select("WA_AssumptionId='" + "RA" + "'");
            //hdnLEValue.Value = dtAssumption.Rows[0][""].ToString();
            RadGrid1.DataSource = dtAssumption;
            RadGrid1.DataBind();



        }

        protected void RadGrid1_ItemUpdated(object source, Telerik.Web.UI.GridUpdatedEventArgs e)
        {

        }

        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {

        }

        //protected void RadGrid1_ItemDeleted(object source, GridDeletedEventArgs e)
        //{

        //}

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            string editorText = "unknown";
            decimal value = 0;
            if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
                editColumn.Visible = false;
            }
            else if (e.CommandName == RadGrid.RebindGridCommandName && e.Item.OwnerTableView.IsItemInserted)
            {
                e.Canceled = true;
            }
            else if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
                TextBox txt = (TextBox)e.Item.FindControl("txtAssumptionValue");
                value = decimal.Parse(txt.Text);
                string assumptionValue = (RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WA_AssumptionId"].ToString());
              //string a =  (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["WA_AssumptionId"]).ToString();
                int userId = 0;
                int customerId = 0;
               
                if (assumptionValue == "LE" || assumptionValue == "RA")
                {
                    userId = userVo.UserId;
                    customerId = customerVo.CustomerId;

                    customerBo.InsertCustomerStaticDetalis(userId, customerId, value, assumptionValue);
                                    
               }
                else
                {
                    userId = userVo.UserId;
                    customerId = customerVo.CustomerId;
                    customerBo.UpdateCustomerProjectedDetalis(userId, customerId, value, assumptionValue);

                }
                BindAllCustomerAssumptions();
     
            }
            else
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
                if (!editColumn.Visible)
                    editColumn.Visible = true;
            }
        }


        //protected void RadGrid1_ItemCreated(object sender, Telerik.WebControls.GridItemEventArgs e)
        //{
        //    if (e.Item is GridCommandItem)
        //    {
        //        e.Item.FindControl("InitInsertButton").Visible = false;
        //    }
        //} 

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                e.Item.FindControl("InitInsertButton").Parent.Visible = false;
            }
        }
        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    RadGrid1.EditIndexes.Add(0);
            //    RadGrid1.Rebind();
            //}
        }

        protected void btnGoalFundingPreference_OnClick(object sender, EventArgs e)
        {
            UpdatePlanPreferences();
            //msgRecordStatus.Visible = true;
        }

        public void UpdatePlanPreferences()
        {
            if (rbtnMapping.Checked)
            {
                customerBo.InsertPlanPreferences(customerVo.CustomerId, 5, 3);
               
            }
            if (rbtnConsolidateEntry.Checked)
            {
                customerBo.InsertPlanPreferences(customerVo.CustomerId, 6, 3);
              
                //IsSpouseExist();
            }

        }

        protected void btnCalculationBasis_OnClick(object sender, EventArgs e)
        {
            UpdateCalculationBasis();
            //msgRecordStatus.Visible = true;
        }

        public void UpdateCalculationBasis()
        {
            if (rbtnYes.Checked)
            {
                customerBo.InsertPlanPreferences(customerVo.CustomerId, 7, 4);
               
            }
            if (rbtnNo.Checked)
            {
                customerBo.InsertPlanPreferences(customerVo.CustomerId, 8, 4);
                
            }
        }

        public void SetDefaultPlanRetirementValueForCustomer()
        {
            int customerId = 0;
            customerId = customerVo.CustomerId;
            int calculationIdRowI = 0;
            int calculationIdRowII = 0;
            int calculationBasisIdI = 0;
            int calculationBasisIdII = 0;
            int calculationBasisIdIII = 0;
            int calculationIdRowIII = 0;

            DataSet dsSetDefaultPlanRetirementValueForCustomer;
            dsSetDefaultPlanRetirementValueForCustomer = customerBo.SetDefaultPlanRetirementValueForCustomer(customerId);

            if (dsSetDefaultPlanRetirementValueForCustomer.Tables[0].Rows.Count > 0)
            {

                calculationBasisIdI = int.Parse(dsSetDefaultPlanRetirementValueForCustomer.Tables[0].Rows[0]["WFPCB_CalculationBasisId"].ToString());
                calculationIdRowI = int.Parse(dsSetDefaultPlanRetirementValueForCustomer.Tables[0].Rows[0]["WFPCB_CalculationId"].ToString());

                calculationBasisIdII = int.Parse(dsSetDefaultPlanRetirementValueForCustomer.Tables[0].Rows[1]["WFPCB_CalculationBasisId"].ToString());
                calculationIdRowII = int.Parse(dsSetDefaultPlanRetirementValueForCustomer.Tables[0].Rows[1]["WFPCB_CalculationId"].ToString());

                calculationBasisIdIII = int.Parse(dsSetDefaultPlanRetirementValueForCustomer.Tables[0].Rows[2]["WFPCB_CalculationBasisId"].ToString());
                calculationIdRowIII = int.Parse(dsSetDefaultPlanRetirementValueForCustomer.Tables[0].Rows[2]["WFPCB_CalculationId"].ToString());
                
                
                if (calculationIdRowI == 3)
                {
                    if (calculationBasisIdI == 5)
                    {
                        rbtnMapping.Checked = true;
                        rbtnConsolidateEntry.Checked = false;

                    }
                    else
                    {
                        rbtnMapping.Checked = false;
                        rbtnConsolidateEntry.Checked = true;
                    }
                }
                if (calculationIdRowII == 4)
                {
                    if (calculationBasisIdII == 7)
                    {
                        rbtnYes.Checked = true;
                        rbtnNo.Checked = false;
                    }
                    else
                    {
                        rbtnYes.Checked = false;
                        rbtnNo.Checked = true;
                    }
                }
                if (calculationIdRowIII == 1)
                {
                    if (calculationBasisIdIII == 1)
                    {
                        rbtnNoCorpus.Checked = true;
                       rbtnCorpus.Checked = false;

                    }
                    else
                    {
                        rbtnNoCorpus.Checked = false;
                        rbtnCorpus.Checked = true;
                    }
                }

            }

            else
            {
                customerBo.InsertPlanPreferences(customerVo.CustomerId, 5, 3);
                customerBo.InsertPlanPreferences(customerVo.CustomerId, 7, 4);
                customerBo.InsertPlanPreferences(customerVo.CustomerId, 1, 1);
                SetDefaultPlanRetirementValueForCustomer();
            }

        }

        protected void btnRetirementCalculationBasis_OnClick(object sender, EventArgs e)
        {
            UpdateRetirementCalculationBasis();
            //msgRecordStatus.Visible = true;
        }
        public void UpdateRetirementCalculationBasis()
        {
            if (rbtnNoCorpus.Checked)
                customerBo.InsertPlanPreferences(customerVo.CustomerId, 1, 1);
            if (rbtnCorpus.Checked)
                customerBo.InsertPlanPreferences(customerVo.CustomerId, 2, 1);

        }

    }
}