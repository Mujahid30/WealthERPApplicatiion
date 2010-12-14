using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoCustomerPortfolio;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data.Common;
using System.Data;
using BoCustomerPortfolio;
using VoUser;
using WealthERP.Base;
using System.Collections;

namespace WealthERP.Customer
{
    public partial class AddDematAccountDetails : System.Web.UI.UserControl
    {
        DematAccountVo demataccountvo = new DematAccountVo();
        BoCustomerPortfolio.BoDematAccount bodemataccount = new BoDematAccount();
        CustomerPortfolioVo customerportfoliovo = new CustomerPortfolioVo();
        CustomerVo customervo = new CustomerVo();
        int customerId = 0;
        int demataccountid = 0;
        //Used when Performing Adding Action
        DataTable dtCustomerAccociation;
        DataSet dsCustomerAssociation;
        DataSet dsModeOfHolding;
        RMVo rmvo = new RMVo();
        
        //Used when performing Viewing Action
        DataSet dsAvailableTrades;
        DataSet dsDematDetails;
        DataSet dsJointHoldersAndNominees;
        protected void Page_Load(object sender, EventArgs e)
        {
            rmvo =(RMVo) Session["rmvo"];
            
            
            customerportfoliovo = (CustomerPortfolioVo)Session["customerPortfolioVo"];
            
                if (Session["DematDetailsView"].ToString() == "View")
                {
                    lblTitle.Text = "View Demat Account";
                    # region View Section
                    txtBeneficiaryAcctNbr.Enabled = false;
                    rbtnYes.Enabled = false;
                    rbtnNo.Enabled = false;
                    txtDpClientId.Enabled = false;
                    txtDPId.Enabled = false;
                    txtDpName.Enabled = false;
                    ddlModeOfHolding.Enabled = false;
                    txtBeneficiaryAcctNbr.Enabled = false;
                    gvPickJointHolder.Enabled = false;
                    gvPickNominee.Enabled = false;
                    lstAvailableTrades.Enabled = false;
                    lstAssocaitedTrades.Enabled = false;
                    addBranch.Disabled = true;
                    deleteBranch.Disabled = true;
                    txtAccountOpeningDate.Enabled = false;
                    btnSubmit.Visible = false;
                    lbtnBackButton.Visible = true;
                    ViewEditMode(); 
                    
                    # endregion
                }


                else if (Session["DematDetailsView"].ToString() == "Edit")
                {
                    lblTitle.Text = "Edit Demat Account";
                    txtBeneficiaryAcctNbr.Enabled = true;
                    txtDpClientId.Enabled = true;
                    txtDPId.Enabled = true;
                    txtDpName.Enabled = true;
                    rbtnYes.Enabled = true;
                    rbtnNo.Enabled = true;
                    ddlModeOfHolding.Enabled = true;
                    txtBeneficiaryAcctNbr.Enabled = true;
                    gvPickJointHolder.Enabled = true;
                    gvPickNominee.Enabled = true;
                    lstAvailableTrades.Enabled = true;
                    lstAssocaitedTrades.Enabled = true;
                    addBranch.Disabled = false;
                    deleteBranch.Disabled = false;
                    txtAccountOpeningDate.Enabled = true;
                    btnSubmit.Visible = true;
                    lbtnBackButton.Visible = false;
                    ViewEditMode();
                   
                }
                else if (Session["DematDetailsView"].ToString() == "Add")
                {

                    lblTitle.Text = "Add Demat Account";
                    
                    customervo = (CustomerVo)Session["CustomerVo"];
                    try
                    {
                        dsModeOfHolding = new DataSet();
                        dtCustomerAccociation = new DataTable();
                        dsCustomerAssociation = new DataSet();
                        dsModeOfHolding = bodemataccount.GetXmlModeOfHolding();
                        dsCustomerAssociation = bodemataccount.GetCustomerAccociation(customervo);
                        //Mode of Holding Combobox populating
                        ddlModeOfHolding.DataSource = dsModeOfHolding;
                        ddlModeOfHolding.DataTextField = "XMOH_ModeOfHolding";
                        ddlModeOfHolding.DataValueField = "XMOH_ModeOfHoldingCode";
                        ddlModeOfHolding.DataBind();
                        ddlModeOfHolding.SelectedIndex = 7;
                        //This for IsJointlyHeld and Drop down loading based on that
                        //====================================
                        
                        
                        gvPickJointHolder.Visible = false;
                        lblPickJointHolder.Visible = false;
                        hrPickJointHolder.Visible = false;
                        //====================================
                        //Pick nominee Grid Populating
                        //==========================================================
                        if (dsCustomerAssociation.Tables[0].Rows.Count == 0)
                        {
                            lblPickNominee.Text = "You have no associates";                            

                        }
                        else
                        {
                            lblPickNominee.Text = "Pick Nominee";
                            gvPickNominee.DataSource = dsCustomerAssociation.Tables[0];
                            gvPickNominee.DataBind();
                        }
                        //==========================================================
                        //Pick Joint Holder Grid Populating
                        //==========================================================
                        if (dsCustomerAssociation.Tables[0].Rows.Count == 0)
                        {
                            lblPickJointHolder.Text = "You have no associates";

                        }
                        else
                        {
                            lblPickJointHolder.Text = "Pick JointHolder";
                            gvPickJointHolder.DataSource = dsCustomerAssociation;
                            gvPickJointHolder.DataBind();
                        }
                        //==========================================================
                        BindListBox();

                    }
                    catch (BaseApplicationException ex)
                    {
                        BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                        NameValueCollection FunctionInfo = new NameValueCollection();
                        FunctionInfo.Add("Method", "RMBranchAssocistion.ascx:setBranchList()");
                        object[] objects = new object[3];
                        //objects[0] = ;
                        //objects[1] = ;
                        //objects[2] = 
                        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                        exBase.AdditionalInformation = FunctionInfo;
                        ExceptionManager.Publish(exBase);
                        throw exBase;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }     
            
        }
        protected void ViewEditMode()
        {
            DateTime accountopeningdate;
            int listItemCount = 0;
            customervo = (CustomerVo)Session["CustomerVo"];
            customerId = customervo.CustomerId;
            demataccountid = int.Parse(Session["DematAccountId"].ToString());
            dsDematDetails = bodemataccount.GetDematAccountDetails(demataccountid);
            txtDpClientId.Text = dsDematDetails.Tables[0].Rows[0]["DpClientId"].ToString();
            txtDPId.Text = dsDematDetails.Tables[0].Rows[0]["DpId"].ToString();
            txtDpName.Text = dsDematDetails.Tables[0].Rows[0]["DpName"].ToString();
            // This is to populate Drop down box with details
            //=========================================================
            dsModeOfHolding = bodemataccount.GetXmlModeOfHolding();
            ddlModeOfHolding.DataSource = dsModeOfHolding;
            ddlModeOfHolding.DataTextField = "XMOH_ModeOfHolding";
            ddlModeOfHolding.DataValueField = "XMOH_ModeOfHoldingCode";
            ddlModeOfHolding.DataBind();
            //=========================================================
            
            txtBeneficiaryAcctNbr.Text = dsDematDetails.Tables[0].Rows[0]["BeneficiaryAccountNum"].ToString();
            if (dsDematDetails.Tables[0].Rows[0]["IsHeldJointly"].ToString() == "1")
            {
                rbtnYes.Checked = true;
                rbtnNo.Checked = false;
            }
            else
            {
                rbtnNo.Checked = true;
                rbtnYes.Checked = false ;
            }
            foreach(ListItem liModeOfHolding in ddlModeOfHolding.Items)
            {
                if (liModeOfHolding.Value == dsDematDetails.Tables[0].Rows[0]["ModeOfHolding"].ToString())
                {
                    break;
                }
                listItemCount++;
            }
            ddlModeOfHolding.SelectedIndex = listItemCount;
            if (!string.IsNullOrEmpty (dsDematDetails.Tables[0].Rows[0]["AccountOpeningDate"].ToString()))
            {
                accountopeningdate = (DateTime)dsDematDetails.Tables[0].Rows[0]["AccountOpeningDate"];
                txtAccountOpeningDate.Text = accountopeningdate.ToShortDateString();
                
            }
            else
            {
                txtAccountOpeningDate.Text = " ";
               
            }
          
            

            //Filling Associated Trades
            lstAssocaitedTrades.DataSource = dsDematDetails.Tables[1];
            lstAssocaitedTrades.DataTextField = "TradeAcountNum";
            lstAssocaitedTrades.DataValueField = "AccountId";
            lstAssocaitedTrades.DataBind();

            //Filling Available Trades
            dsAvailableTrades = bodemataccount.GetAvailableTrades(customerId, demataccountid);
            lstAvailableTrades.DataSource = dsAvailableTrades.Tables[0];
            lstAvailableTrades.DataTextField = "CETA_TradeAccountNum";
            lstAvailableTrades.DataValueField = "CETA_AccountId";
            lstAvailableTrades.DataBind();



            dsJointHoldersAndNominees = bodemataccount.GetJointHoldersAndNominees(demataccountid);
            dsCustomerAssociation = bodemataccount.GetCustomerAccociation(customervo);
            //Populating  Pick nominee
            if (dsCustomerAssociation.Tables[0].Rows.Count == 0)
            {
                lblPickNominee.Text = "You have no associates";

            }
            else
            {
                lblPickNominee.Text = "Pick Joint Holder";
                gvPickNominee.DataSource = dsCustomerAssociation.Tables[0];
                gvPickNominee.DataBind();
            }

            //Populating  Joint Holder nominee
            if (dsCustomerAssociation.Tables[0].Rows.Count == 0)
            {
                lblPickJointHolder.Text = "You have no associates";

            }
            else
            {
                lblPickJointHolder.Text = "Pick Nominee";
                gvPickJointHolder.DataSource = dsCustomerAssociation;
                gvPickJointHolder.DataBind();
            }



            for (int i = 0; i < gvPickJointHolder.Rows.Count; i++)
            {
                for (int j = 0; j < dsJointHoldersAndNominees.Tables[0].Rows.Count; j++)
                {
                    if (dsJointHoldersAndNominees.Tables[0].Rows[j]["AssociationType"].ToString() == "JH")
                    {

                        if (gvPickJointHolder.DataKeys[i].Value.ToString() == dsJointHoldersAndNominees.Tables[0].Rows[j]["AssociationId"].ToString())
                        {

                            ((CheckBox)gvPickJointHolder.Rows[i].FindControl("PJHCheckBox")).Checked = true;

                        }
                    }
                }
                for (int k = 0; k < dsJointHoldersAndNominees.Tables[0].Rows.Count; k++)
                {
                    if (dsJointHoldersAndNominees.Tables[0].Rows[k]["AssociationType"].ToString() == "N")
                    {
                        if (gvPickNominee.DataKeys[i].Value.ToString() == dsJointHoldersAndNominees.Tables[0].Rows[k]["AssociationId"].ToString())
                        {
                            ((CheckBox)gvPickNominee.Rows[i].FindControl("PNCheckBox")).Checked = true;
                        }
                    }
                }

            }
        
                
                    
            
        }
        protected void ddlModeOfHolding_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlModeOfHolding.SelectedIndex != 8)
            {
                hrPickJointHolder.Visible = true;
                gvPickJointHolder.Visible = true;
                lblPickJointHolder.Visible = true;
               
            }
            else
            {
                gvPickJointHolder.Visible = false;
                lblPickJointHolder.Visible = false;
                hrPickJointHolder.Visible = false;
                
                
            }
        }
        public void BindListBox()
        {
            CustomerVo customervo = (CustomerVo)Session["CustomerVo"];
            DataSet dsTradeAccount = bodemataccount.GetTradeAccountNumber(customervo);
            lstAvailableTrades.DataSource = dsTradeAccount.Tables[0];
            lstAvailableTrades.DataTextField = "CETA_TradeAccountNum";
            lstAvailableTrades.DataValueField = "CETA_AccountId";
            lstAvailableTrades.DataBind();

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {           
            int portfolioid=0;
            customervo = (CustomerVo)Session["CustomerVo"];
             int customerId = customervo.CustomerId;

             customerportfoliovo = (CustomerPortfolioVo)Session["customerPortfolioVo"];
            if (customerportfoliovo.PortfolioId.ToString() != "")
            {
                portfolioid = (int)customerportfoliovo.PortfolioId;
            }            
            //string associationIdJH = null;
            //string associationIdN = null;
            
            string accountopeningdate = txtAccountOpeningDate.Text;
            //string lstassociatedtradeaccount = null;
            ArrayList associationIdJH = new ArrayList();
            ArrayList associationIdN = new ArrayList();
            ArrayList lstassociatedtradeaccount = new ArrayList();
            
            if (Page.IsValid)
            {
                try
                {
                    if (Session["DematDetailsView"].ToString() == "Add")
                    {
                        if (!string.IsNullOrEmpty(txtAccountOpeningDate.Text.Trim()))
                            demataccountvo.AccountOpeningDate = DateTime.Parse(accountopeningdate);
                        else
                            demataccountvo.AccountOpeningDate = DateTime.MinValue;
                       
                        demataccountvo.BeneficiaryAccountNbr = txtBeneficiaryAcctNbr.Text;
                        demataccountvo.DpclientId = txtDpClientId.Text;
                        demataccountvo.DpId = txtDPId.Text;
                        demataccountvo.DpName = txtDpName.Text;
                        if (rbtnYes.Checked == true)
                            demataccountvo.IsHeldJointly = 1;
                        else
                            demataccountvo.IsHeldJointly = 0;
                        demataccountvo.ModeOfHolding = hdnSelectedModeOfHolding.Value;

                        if (gvPickJointHolder.Rows.Count != 0)
                        {
                            foreach (GridViewRow gvrJH in gvPickJointHolder.Rows)
                            {
                                if (((CheckBox)gvrJH.FindControl("PJHCheckBox")).Checked == true)
                                {
                                    associationIdJH.Add(gvPickJointHolder.DataKeys[gvrJH.RowIndex].Value.ToString());
                                }
                            }
                        }
                        if (gvPickNominee.Rows.Count != 0)
                        {
                            foreach (GridViewRow gvrN in gvPickNominee.Rows)
                            {
                                if (((CheckBox)gvrN.FindControl("PNCheckBox")).Checked == true)
                                {
                                    associationIdN.Add(gvPickNominee.DataKeys[gvrN.RowIndex].Value.ToString());
                                }
                            }
                        }
                        string[] associatedtradeitems = hdnSelectedBranches.Value.Split(',');
                        if (associatedtradeitems.Length!= 0)
                        {
                            foreach (string associatedtradeitem in associatedtradeitems)
                            {
                                if (associatedtradeitem != "")
                                {

                                    lstassociatedtradeaccount.Add(associatedtradeitem);
                                }
                            }
                        }


                        
                        bodemataccount.AddDematDetails(customerId, portfolioid, demataccountvo, rmvo, associationIdJH, associationIdN, lstassociatedtradeaccount);

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('DematAccountDetails','none');", true);
                    }
                    else if (Session["DematDetailsView"].ToString() == "Edit")
                    {
                        if (!string.IsNullOrEmpty(accountopeningdate.ToString()))
                        {
                            if (!string.IsNullOrEmpty(txtAccountOpeningDate.Text.Trim()))
                                demataccountvo.AccountOpeningDate = DateTime.Parse(accountopeningdate);
                            else
                                demataccountvo.AccountOpeningDate = DateTime.MinValue;
                            
                        }
                        else
                        {
                            demataccountvo.AccountOpeningDate = DateTime.MinValue;
                            
                        }
                        demataccountvo.BeneficiaryAccountNbr = txtBeneficiaryAcctNbr.Text;
                        demataccountvo.DpclientId = txtDpClientId.Text;
                        demataccountvo.DpId = txtDPId.Text;
                        demataccountvo.DpName = txtDpName.Text;
                        if (rbtnYes.Checked == true)
                            demataccountvo.IsHeldJointly = 1;
                        else
                            demataccountvo.IsHeldJointly = 0;
                        demataccountvo.ModeOfHolding = hdnSelectedModeOfHolding.Value;

                        if (gvPickJointHolder.Rows.Count != 0)
                        {
                            foreach (GridViewRow gvrJH in gvPickJointHolder.Rows)
                            {
                                if (((CheckBox)gvrJH.FindControl("PJHCheckBox")).Checked == true)
                                {
                                    associationIdJH.Add(gvPickJointHolder.DataKeys[gvrJH.RowIndex].Value.ToString());
                                }
                            }
                        }
                        if (gvPickNominee.Rows.Count != 0)
                        {
                            foreach (GridViewRow gvrN in gvPickNominee.Rows)
                            {
                                if (((CheckBox)gvrN.FindControl("PNCheckBox")).Checked == true)
                                {
                                    associationIdN.Add(gvPickNominee.DataKeys[gvrN.RowIndex].Value.ToString());
                                }
                            }
                        }
                        string[] associatedtradeitems = hdnSelectedBranches.Value.Split(',');
                        if (associatedtradeitems.Length != 0)
                        {
                            foreach (string associatedtradeitem in associatedtradeitems)
                            {
                                if (associatedtradeitem != "")
                                {

                                    lstassociatedtradeaccount.Add(associatedtradeitem);
                                }
                            }
                        }

                        bodemataccount.UpdateDematDetails(customerId, portfolioid,demataccountid,demataccountvo, rmvo, associationIdJH, associationIdN, lstassociatedtradeaccount);                        
                        ViewEditMode();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('DematAccountDetails','none');", true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }      

        protected void ddlModeOfHolding_PreRender(object sender, EventArgs e)
        {

        }

        protected void lbtnBackButton_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddDematAccountDetails','none');", true);
            Session["DematDetailsView"] = "Edit";
        }
            

       
    }
}