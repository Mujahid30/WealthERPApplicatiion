using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using BoCustomerProfiling;
using System.Data;
using WealthERP.Customer;
using System.Collections.Specialized;
using System.Configuration;
using BoCommon;
namespace WealthERP.CustomerPortfolio
{
    public partial class ULIPPlanAdd : System.Web.UI.UserControl
    {
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerVo customerVo;
        AssetBo assetBo = new AssetBo();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session["customerVo"];
            string path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            string assetGroupCode = "Ins";
            string category = "ULIP";
            DataSet ds = customerAccountBo.GetCustomerInsuranceAccounts(customerVo.CustomerId, assetGroupCode);
            LoadInsuranceIssuerCode(path);
            lblAllocation.Visible = false;
            lblPurchasePrice.Visible = false;
            lblUnits.Visible = false;
            btnSubmit.Visible = false;
            if (Session["table"] != null)
            {

                Table tb = (Table)Session["table"];
                //Page.Controls.Add(tb);
                this.PlaceHolder1.Controls.Add(tb);
                lblUnits.Visible = true;
                lblPurchasePrice.Visible = true;
                lblAllocation.Visible = true;
                btnSubmit.Visible = true;

            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                //lblMessage.Text = "You Dont have an account for this category..";
                //lblMessage.Visible = true;

            }
            //else
            //{

            //    ddlAccountId.DataSource = ds.Tables[0];
            //    ddlAccountId.DataTextField = ds.Tables[0].Columns["CIA_AccountNum"].ToString();
            //    ddlAccountId.DataValueField = ds.Tables[0].Columns["CIA_AccountId"].ToString();
            //    ddlAccountId.DataBind();

            //}
           
        

        }
        public void LoadInsuranceIssuerCode(string path)
        {
            DataTable dt = assetBo.GetInsuranceIssuerCode(path);
            ddlInsuranceIssuerCode.DataSource = dt;
            ddlInsuranceIssuerCode.DataTextField = dt.Columns["Name"].ToString();
            ddlInsuranceIssuerCode.DataValueField = dt.Columns["Code"].ToString();
            ddlInsuranceIssuerCode.DataBind();

        }
        public void LoadUlipPlan()
        {
            DataSet ds = assetBo.GetULIPPlans(ddlInsuranceIssuerCode.SelectedItem.Value.ToString());
            ddlUlipPlans.DataSource = ds;
            ddlUlipPlans.DataTextField = ds.Tables[0].Columns["WUP_ULIPPlanName"].ToString();
            ddlUlipPlans.DataValueField = ds.Tables[0].Columns["WUP_ULIPPlanCode"].ToString();
            ddlUlipPlans.DataBind();            
        }

        protected void ddlInsuranceIssuerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUlipPlan();
        }

        protected void ddlUlipPlans_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUlipSubPlans();
        }
        public void LoadUlipSubPlans()
        {
          

            DataSet ds = assetBo.GetULIPSubPlans(int.Parse(ddlUlipPlans.SelectedItem.Value.ToString()));
            int count = ds.Tables[0].Rows.Count;
            LiteralControl literal = new LiteralControl();
            Table tb = new Table();
            TableCell tc;
          
                //Terminal.Controls.Add(tb);

                for (int i = 0; i < count; i++)
                {
                    TableRow tr = new TableRow();

                    tc = new TableCell();
                    Label lbl = new Label();
                    lbl.ID = "lblTerminalId" + (i).ToString();
                   // lbl.ID = ds.Tables[0].Rows[i][0].ToString();
                    lbl.CssClass = "FieldName";
                    lbl.Text = ds.Tables[0].Rows[i][1].ToString();                    
                    tc.Controls.Add(lbl);
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    TextBox txtBox1 = new TextBox();
                   txtBox1.ID = "txtAllocationId" + i.ToString();               
                   // txtBox1.ID = ds.Tables[0].Rows[i][0].ToString();                 
                    txtBox1.CssClass = "txtField";
                    tc.Controls.Add(txtBox1);
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    TextBox txtBox2 = new TextBox();
                    txtBox2.ID = "txtUnitsId" + i.ToString();               
                    //txtBox2.ID = ds.Tables[0].Rows[i][0].ToString();
                    txtBox2.CssClass = "txtField";
                    tc.Controls.Add(txtBox2);
                    tr.Cells.Add(tc);

                    tc = new TableCell();
                    TextBox txtBox3 = new TextBox();
                    txtBox3.ID = "txtPurchasePriceId" + i.ToString();               
                   // txtBox3.ID = ds.Tables[0].Rows[i][0].ToString();
                    txtBox3.CssClass = "txtField";
                    tc.Controls.Add(txtBox3);
                    tr.Cells.Add(tc);

                    tb.Rows.Add(tr);
                }
                PlaceHolder1.Controls.Add(tb);
                lblUnits.Visible = true;
                lblPurchasePrice.Visible = true;
                lblAllocation.Visible = true;
                btnSubmit.Visible = true;
                Session["table"] = tb;
          
            }

        protected void Button1_Click(object sender, EventArgs e)
        {

            List<float> subPlanList = null;
            List<InsuranceULIPVo> insuranceUlipList;
            InsuranceULIPVo insuranceUlipVo;
            DataSet ds = assetBo.GetULIPSubPlans(int.Parse(ddlUlipPlans.SelectedItem.Value.ToString()));
            int count = ds.Tables[0].Rows.Count;
            subPlanList = new List<float>();
            insuranceUlipList = new List<InsuranceULIPVo>();
            float tot = 0;
            int txt = 0;
            // Calcuating the total Asset Allocation value

            for (int i = 0; i < count; i++)
            {
                string temp = (((TextBox)PlaceHolder1.FindControl("txtAllocationId" + i.ToString())).Text.ToString());
                if (temp == "")
                {
                     txt = 0;

                }
                else
                {                             
                    txt = int.Parse(temp.ToString());
                }

               tot = tot +(float) txt;

               subPlanList.Add(txt);
              
            }


            // Check the total asset Allocation and assign Unit, Purchase price and Allocation percentage 

          

            if (tot == 100)
            {
                Label6.Text = "Hundred";
             
                for (int i = 0; i < count; i++)
                {
                    insuranceUlipVo = new InsuranceULIPVo();
                    string allocationPer = ((TextBox)PlaceHolder1.FindControl("txtAllocationId" + i.ToString())).Text.ToString();
                    string units = ((TextBox)PlaceHolder1.FindControl("txtUnitsId" + i.ToString())).Text.ToString();
                    string purchasePrice = ((TextBox)PlaceHolder1.FindControl("txtPurchasePriceId" + i.ToString())).Text.ToString();
                  
                    insuranceUlipVo.WUP_ULIPSubPlaCode = ds.Tables[0].Rows[i][0].ToString();
                    insuranceUlipVo.CIUP_ULIPPlanId = int.Parse(ddlUlipPlans.SelectedItem.Value.ToString());
                    
                    if (allocationPer == "" )
                    {

                        insuranceUlipVo.CIUP_AllocationPer = 0;
                        insuranceUlipVo.CIUP_PurchasePrice = 0;
                        insuranceUlipVo.CIUP_Unit = 0;

                    }
                    else
                    {
                        
                        insuranceUlipVo.CIUP_AllocationPer=float.Parse(allocationPer.ToString());
                        insuranceUlipVo.CIUP_PurchasePrice = float.Parse(purchasePrice.ToString());
                        insuranceUlipVo.CIUP_Unit = float.Parse(units.ToString());
                    }

                                   
                    insuranceUlipList.Add(insuranceUlipVo);
                }
                Session["ulipList"] = insuranceUlipList;
                Session["issuerCode"] = ddlInsuranceIssuerCode.SelectedItem.Value.ToString();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioInsuranceEntry','none');", true);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd','none');", true);

            }
            else
            {
                Label6.Text = "Not 100";
            }
                    

        }

        

    }
}