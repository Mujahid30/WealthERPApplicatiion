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
using VoCustomerPortfolio;
using BoCommon;
using BoUploads;
using VoCustomerProfiling;
using BoCustomerProfiling;
using BoAdvisorProfiling;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;

namespace WealthERP.Customer
{
    public partial class AddDematAccountDetails : System.Web.UI.UserControl
    {
        DematAccountVo demataccountvo = new DematAccountVo();
        BoCustomerPortfolio.BoDematAccount bodemataccount = new BoDematAccount();
        CustomerPortfolioVo customerportfoliovo = new CustomerPortfolioVo();
        CustomerVo customervo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
      
        int customerId = 0;
        int demataccountid = 0;
        //Used when Performing Adding Action
        DataTable dtCustomerAccociation;
        DataSet dsCustomerAssociation;
        DataSet dsModeOfHolding;
        RMVo rmvo = new RMVo();
        UserVo userVo = new UserVo();
        BoDematAccount boDematAccount = new BoDematAccount();
        //Used when performing Viewing Action
        DataSet dsAvailableTrades;
        DataSet dsDematDetails;
        DataSet dsJointHoldersAndNominees;
        protected void Page_Load(object sender, EventArgs e)
        {
            int customerId;
            int portfolioid;
            rmvo = (RMVo)Session["rmvo"];
            BindDepositoryType();
            customerportfoliovo = (CustomerPortfolioVo)Session["customerPortfolioVo"];
            if (Request.QueryString["CustomerId"] != null)
            {
                lblisactive.Visible = false;
                chk_isactive.Visible = false;
                    customerId = int.Parse(Request.QueryString["CustomerId"].ToString());
                    
                    dsModeOfHolding = new DataSet();
                    dtCustomerAccociation = new DataTable();
                    dsCustomerAssociation = new DataSet();
                    dsModeOfHolding = bodemataccount.GetXmlModeOfHolding();
                    dsCustomerAssociation = bodemataccount.GetCustomerAccociation(customerId);
                    //Mode of Holding Combobox populating
                    ddlModeOfHolding.DataSource = dsModeOfHolding;
                    ddlModeOfHolding.DataTextField = "XMOH_ModeOfHolding";
                    ddlModeOfHolding.DataValueField = "XMOH_ModeOfHoldingCode";
                    ddlModeOfHolding.DataBind();
                    ddlModeOfHolding.SelectedIndex = 8;
                    chk_isactive.Enabled = false;
                    lbtnBack2Button.Visible = false;
                    Session["DematDetailsView"] = "Add";
                    lblTitle.Text = "Add Demat Account";
            }

            else if (Session["DematDetailsView"].ToString() == "View")
            {
                lblTitle.Text = "View Demat Account";
                # region View Section
                ddlDepositoryName.Enabled = false;
                rbtnYes.Enabled = false;
                rbtnNo.Enabled = false;
                txtDpClientId.Enabled = false;
                txtDPId.Enabled = false;
                txtDpName.Enabled = false;
                ddlModeOfHolding.Enabled = false;
                ddlDepositoryName.Enabled = false;

                txtAccountOpeningDate.Enabled = false;
                btnSubmit.Visible = false;
                lbtnBackButton.Visible = true;
                chk_isactive.Enabled = false;
                gvAssociate.Visible = true;
                trAssociatePanel.Visible = true;
                gvAssociate.Enabled = false;
                ViewEditMode();

                # endregion
            }


            else if (Session["DematDetailsView"].ToString() == "Edit")
            {
                lblTitle.Text = "Demat Account";
                ddlDepositoryName.Enabled = true;
                txtDpClientId.Enabled = true;

                txtDpName.Enabled = true;
                rbtnYes.Enabled = true;
                rbtnNo.Enabled = true;
                ddlModeOfHolding.Enabled = true;
                ddlDepositoryName.Enabled = true;

                txtAccountOpeningDate.Enabled = true;
                btnSubmit.Visible = true;
                lbtnBackButton.Visible = false;
                chk_isactive.Enabled = true;
                gvAssociate.Visible = true;
                trAssociatePanel.Visible = true;
                gvAssociate.Enabled = true;
                ViewEditMode();

            }
            else if (Session["DematDetailsView"].ToString() == "Add")
            {
                
                lblTitle.Text = "Add Demat Account";
                
                    customervo = (CustomerVo)Session["CustomerVo"];
                    customerId = customervo.CustomerId;

                try
                {
                    dsModeOfHolding = new DataSet();
                    dtCustomerAccociation = new DataTable();
                    dsCustomerAssociation = new DataSet();
                    dsModeOfHolding = bodemataccount.GetXmlModeOfHolding();
                    dsCustomerAssociation = bodemataccount.GetCustomerAccociation(customerId);
                    //Mode of Holding Combobox populating
                    ddlModeOfHolding.DataSource = dsModeOfHolding;
                    ddlModeOfHolding.DataTextField = "XMOH_ModeOfHolding";
                    ddlModeOfHolding.DataValueField = "XMOH_ModeOfHoldingCode";
                    ddlModeOfHolding.DataBind();
                    ddlModeOfHolding.SelectedIndex = 8;


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
            //// This is to populate Drop down box with details
            ////=========================================================
            dsModeOfHolding = bodemataccount.GetXmlModeOfHolding();
            ddlModeOfHolding.DataSource = dsModeOfHolding;
            ddlModeOfHolding.DataTextField = "XMOH_ModeOfHolding";
            ddlModeOfHolding.DataValueField = "XMOH_ModeOfHoldingCode";
            ddlModeOfHolding.DataBind();
            ////=========================================================
            if (!string.IsNullOrEmpty(dsDematDetails.Tables[0].Rows[0]["DepositoryName"].ToString()))
            {
                ddlDepositoryName.SelectedValue = dsDematDetails.Tables[0].Rows[0]["DepositoryName"].ToString().Trim();
               if (Session["DematDetailsView"].ToString() == "Edit")
                {
                   if (ddlDepositoryName.SelectedItem.Text == "NSDL")
                   {
                        txtDPId.Enabled = true;
                    }
                    else if (ddlDepositoryName.SelectedItem.Text == "CDSL")
                    {
                        txtDPId.Enabled = false;
                    }
               }
            }
            if (dsDematDetails.Tables[0].Rows[0]["IsActive"].ToString() == "1")
           {
                chk_isactive.Checked = true;
            }
            else
            {
                chk_isactive.Checked = false;
            }


            if (dsDematDetails.Tables[0].Rows[0]["ModeOfHolding"].ToString() != "SI")
            {
                rbtnYes.Checked = true;
              
                
                rbtnNo.Checked = false;
            }
            else
            {
                rbtnNo.Checked = true;
                rbtnYes.Checked = false;
               
              
            }
            foreach (ListItem liModeOfHolding in ddlModeOfHolding.Items)
            {
                if (liModeOfHolding.Value == dsDematDetails.Tables[0].Rows[0]["ModeOfHolding"].ToString())
                {
                    break;
                }
                listItemCount++;
            }
            ddlModeOfHolding.SelectedIndex = listItemCount;
            if (!string.IsNullOrEmpty(dsDematDetails.Tables[0].Rows[0]["AccountOpeningDate"].ToString()))
            {
                accountopeningdate = (DateTime)dsDematDetails.Tables[0].Rows[0]["AccountOpeningDate"];
                txtAccountOpeningDate.SelectedDate = DateTime.Parse(accountopeningdate.ToString());

            }
            else
            {
                txtAccountOpeningDate.SelectedDate =null;

            }



            



            
            BindgvFamilyAssociate(demataccountid); 




        }
        protected void BindDepositoryType()
        {
            DataTable DsDepositoryNames = new DataTable();
            DsDepositoryNames = bodemataccount.GetDepositoryName();
            ddlDepositoryName.DataSource = DsDepositoryNames;
            if (DsDepositoryNames.Rows.Count > 0)
            {
                ddlDepositoryName.DataTextField = "WCMV_Code";
                ddlDepositoryName.DataValueField = "WCMV_Code";
                ddlDepositoryName.DataBind();
            }
            ddlDepositoryName.Items.Insert(0, new ListItem("Select", "Select"));

        }
        protected void ddlModeOfHolding_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlModeOfHolding.SelectedIndex != 4)
            {
                //hrPickJointHolder.Visible = true;
                //gvPickJointHolder.Visible = true;
                //lblPickJointHolder.Visible = true;

            }
            else
            {
                //gvPickJointHolder.Visible = false;
                //lblPickJointHolder.Visible = false;
                //hrPickJointHolder.Visible = false;
                //ddlModeOfHolding.SelectedIndex = 8;

            }
        }
       
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int result = 0;
            int portfolioid = 0;
            customervo = (CustomerVo)Session["CustomerVo"];
            int customerId = customervo.CustomerId;
            if (Request.QueryString["CustomerId"] != null)
            {
                CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
                PortfolioBo portfolioBo = new PortfolioBo();
                customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(int.Parse(Request.QueryString["CustomerId"]));
                portfolioid = customerPortfolioVo.PortfolioId; 
            }
            else
            {
                customerportfoliovo = (CustomerPortfolioVo)Session["customerPortfolioVo"];
                if (customerportfoliovo.PortfolioId.ToString() != "")
                {
                    portfolioid = (int)customerportfoliovo.PortfolioId;
                }
            }
            //string associationIdJH = null;
            //string associationIdN = null;
            string accountopeningdate = txtAccountOpeningDate.SelectedDate.ToString();
            //string lstassociatedtradeaccount = null;
            ArrayList associationIdJH = new ArrayList();
            ArrayList associationIdN = new ArrayList();
            //ArrayList lstassociatedtradeaccount = new ArrayList();

            //if (Page.IsValid)
            //{
                
                try
                {
                    if (Session["DematDetailsView"].ToString() == "Add")
                    {
                        if (!string.IsNullOrEmpty(accountopeningdate.Trim()))
                            demataccountvo.AccountOpeningDate = DateTime.Parse(accountopeningdate);
                        //demataccountvo.BeneficiaryAccountNbr = txtBeneficiaryAcctNbr.Text;
                        demataccountvo.DpclientId = txtDpClientId.Text;
                            //.Text;
                        demataccountvo.DpId = txtDPId.Text;
                        demataccountvo.DpName = txtDpName.Text;
                        demataccountvo.DepositoryName = ddlDepositoryName.SelectedValue;
                        if (rbtnYes.Checked == true)
                            demataccountvo.IsHeldJointly = 1;
                        else
                            demataccountvo.IsHeldJointly = 0;
                        if (chk_isactive.Checked == true)
                            demataccountvo.IsActive = 1;
                        else
                            demataccountvo.IsActive = 0;
                        demataccountvo.ModeOfHolding = ddlModeOfHolding.SelectedValue.ToString();

                       result= bodemataccount.AddDematDetails(customerId, portfolioid, demataccountvo, rmvo);
                       if (result!=0)
                       {
                           ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('DematAccountDetails','none');", true);
                          Response.Write("<script>alert('DematDeatails has been successfully added');</script>");
                           Response.Write("<script>window.close();</" + "script>");
                          
                       }
                    }
                    else if (Session["DematDetailsView"].ToString() == "Edit")
                    {
                        if (!string.IsNullOrEmpty(accountopeningdate.ToString().Trim()))
                        {
                            demataccountvo.AccountOpeningDate = DateTime.Parse(accountopeningdate);

                        }
                        else
                        {
                            demataccountvo.AccountOpeningDate = DateTime.MinValue;

                        }
                        //  demataccountvo.BeneficiaryAccountNbr = txtBeneficiaryAcctNbr.Text;
                        demataccountvo.DpclientId = txtDpClientId.Text;
                        demataccountvo.DpId = txtDPId.Text;
                        demataccountvo.DpName = txtDpName.Text;
                        demataccountvo.DepositoryName = ddlDepositoryName.SelectedValue;
                        if (rbtnYes.Checked == true)
                            demataccountvo.IsHeldJointly = 1;
                        else
                            demataccountvo.IsHeldJointly = 0;

                        if (chk_isactive.Checked == true)
                            demataccountvo.IsActive = 1;
                        else
                            demataccountvo.IsActive = 0;
                        demataccountvo.ModeOfHolding = ddlModeOfHolding.SelectedValue.ToString();

                      

                        bodemataccount.UpdateDematDetails(customerId, portfolioid, demataccountid, demataccountvo,rmvo);
                        ViewEditMode();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('DematAccountDetails','none');", true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        //}

        
        protected void lbtnBackButton_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('AddDematAccountDetails','none');", true);
            Session["DematDetailsView"] = "Edit";
        }

        protected void lbtnBack2Button_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('DematAccountDetails','none');", true);
        }
        protected void ddlDepositoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Session["DematDetailsView"].ToString() != "ADD"))
            {
                if (ddlDepositoryName.SelectedItem.Text == "NSDL")
                {
                    txtDPId.Enabled = true;
                    txtDPId.MaxLength = 8;
                    txtDpClientId.MaxLength = 8;
                    //txtDpClientId.Text = "";
                }
                else if (ddlDepositoryName.SelectedItem.Text == "CDSL")
                {
                    txtDPId.Enabled = false;
                    txtDpClientId.MaxLength = 16;
                    txtDPId.Text = "";
                    //txtDpClientId.Text = "";
                }
            }
        }
        protected void rbtnNo_CheckChanged(object sender, EventArgs e)
        {
            ddlModeOfHolding.SelectedIndex = 8;
            ddlModeOfHolding.Enabled = false;
            
        }
        protected void RadioButton_CheckChanged(object sender, EventArgs e)
        {
            if (rbtnYes.Checked && !(rbtnNo.Checked))
            {
                ddlModeOfHolding.SelectedIndex = 4;
                
                ddlModeOfHolding.Enabled = true;
                
               
            }
         
        }

        //------------------------------------------------
        private void BindgvFamilyAssociate(int demataccountid)
        {
            gvAssociate.Visible = true;
            DataSet dsAssociate = boDematAccount.GetCustomerDematAccountAssociates(demataccountid);
            gvAssociate.DataSource = dsAssociate;
            gvAssociate.DataBind();
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
        protected void gvFamilyAssociate_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dsAssociate = new DataSet();
            if (Cache["gvAssociate" + userVo.UserId] != null)
            {
                dsAssociate = (DataSet)Cache["gvAssociate" + userVo.UserId];
                gvAssociate.DataSource = dsAssociate;





            }
        }
        protected void gvFamilyAssociate_ItemCommand(object source, GridCommandEventArgs e)
        {
            int dematAccountNo = int.Parse(Session["DematAccountId"].ToString());
            if (e.CommandName == RadGrid.UpdateCommandName)
            {

                int result = 0;
                int iskyc = 0;
                DateTime date;
                string relationCode = string.Empty;
                string associatetype = string.Empty;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                gridEditableItem.OwnerTableView.IsItemInserted = false;
                int associationId = int.Parse(gvAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CDAA_Id"].ToString());
                dematAccountNo = int.Parse(gvAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CEDA_DematAccountId"].ToString());
                CheckBox chkycinside1 = (CheckBox)e.Item.FindControl("chkKYC");
                if (chkycinside1.Checked)
                    iskyc = 1;
                TextBox txtPan = (TextBox)e.Item.FindControl("txtNewPan");
                TextBox txtName = (TextBox)e.Item.FindControl("txtNewName");
                DropDownList ddlGender = (DropDownList)e.Item.FindControl("ddlGender");
                RadDatePicker rdDate = (RadDatePicker)e.Item.FindControl("txtDOB");
                if (rdDate.SelectedDate.ToString() != "")
                {
                    date = DateTime.Parse(rdDate.SelectedDate.ToString());
                }
                else
                    date =DateTime.MinValue;
                DropDownList ddlelationshipName = (DropDownList)e.Item.FindControl("ddlNewRelationship");
                DropDownList ddlAssociate = (DropDownList)e.Item.FindControl("ddlAssociate");
                relationCode = ddlelationshipName.SelectedValue;
                associatetype = ddlAssociate.SelectedValue.Trim();
                Button button1 = (Button)e.Item.FindControl("Button1");

                result = boDematAccount.UpdateCustomerDematAccountAssociates(associationId, demataccountid, associatetype, txtName.Text, txtPan.Text, ddlGender.SelectedValue, date, iskyc, ddlelationshipName.SelectedValue, userVo.UserId);

                if (result == 2)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('U Can Have Atmost Two Nominee/JointHolder');", true);

                }
            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                bool test = true;
                int result = 0;
                DropDownList ddlAssociate = (DropDownList)e.Item.FindControl("ddlAssociate");
                string associatetype = string.Empty;
                associatetype = ddlAssociate.SelectedValue;
                TextBox ttPan = (TextBox)e.Item.FindControl("txtNewPan");
                if (associatetype == "JH1" && ttPan.Text.Length <= 0)
                {
                    test = false;
                }


                int iskyc = 0;
                CheckBox chkycinside1 = (CheckBox)e.Item.FindControl("chkKYC");
                if (chkycinside1.Checked)
                    iskyc = 1;
                string relationCode = string.Empty;
                //string associatetype = string.Empty;
                DateTime date;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                //int associationId = int.Parse(gvAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CDAA_Id"].ToString());
                //TextBox ttPan = (TextBox)e.Item.FindControl("txtNewPan");
                TextBox txtName = (TextBox)e.Item.FindControl("txtNewName");
                DropDownList ddlGender = (DropDownList)e.Item.FindControl("ddlGender");


                RadDatePicker rdDate = (RadDatePicker)e.Item.FindControl("txtDOB");
                if (rdDate.SelectedDate.ToString() != "")
                {
                    date = DateTime.Parse(rdDate.SelectedDate.ToString());
                }
                else
                    date = DateTime.MinValue;


                DropDownList ddlelationshipName = (DropDownList)e.Item.FindControl("ddlNewRelationship");
                //DropDownList ddlAssociate = (DropDownList)e.Item.FindControl("ddlAssociate");
                relationCode = ddlelationshipName.SelectedValue;
                //associatetype = ddlAssociate.SelectedValue;
                Button button1 = (Button)e.Item.FindControl("Button1");
                gridEditableItem.OwnerTableView.IsItemInserted = false;
                if (test)
                {

                    result = boDematAccount.AddCustomerDematAccountAssociates(23, dematAccountNo, associatetype, txtName.Text, ttPan.Text, ddlGender.SelectedValue, date, iskyc, relationCode, userVo.UserId);
                    if (result == 2)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('U Can Have Atmost Two Nominee/JointHolder');", true);

                    }
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Pan No cant be Empty');", true);
        

            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                bool isDeleted = false;
                GridDataItem dataItem = (GridDataItem)e.Item;
                //TableCell strCategoryCodeForDelete = dataItem["CA_AssociationId"];
                int associateId = int.Parse(gvAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CDAA_Id"].ToString());
                isDeleted = boDematAccount.DeleteCustomerDematAccountAssociates(associateId);
                if (isDeleted)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Record has been de-associated successfully !!');", true);
                }

            }

            BindgvFamilyAssociate(demataccountid);
        }
        protected void gvFamilyAssociate_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {


                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DataTable dtRelationship = customerBo.GetMemberRelationShip();
                TextBox txtMember = (TextBox)item.FindControl("txtMember");
                Label lblGetPan = (Label)item.FindControl("lblGetPan");
                Label lblspan = (Label)item.FindControl("lblspan");
                TextBox txtpan = (TextBox)item.FindControl("txtPan");
                TextBox txtNewMemPan = (TextBox)e.Item.FindControl("txtNewPan");
                RadDatePicker rdDate = (RadDatePicker)e.Item.FindControl("txtDOB");

                CheckBox chkyc = (CheckBox)e.Item.FindControl("chkKYC");
                //Label lblspan = (Label)item.FindControl("lblspan");
                DropDownList ddlRelation = (DropDownList)e.Item.FindControl("ddlNewRelationship");
                ddlRelation.DataSource = dtRelationship;
                ddlRelation.DataTextField = dtRelationship.Columns["XR_Relationship"].ToString();
                ddlRelation.DataValueField = dtRelationship.Columns["XR_RelationshipCode"].ToString();
                ddlRelation.DataBind();
                ddlRelation.Items.Insert(0, new ListItem("Select", "Select"));
            }
            if (e.Item is GridDataItem)
            {
                //GridDataItem dataItem = e.Item as GridDataItem;
                //LinkButton buttonEdit = dataItem["editColumn"].Controls[0] as LinkButton;
                //if (viewForm == "View")
                //    buttonEdit.Visible = false;
                //else if (viewForm == "Edit")
                //    buttonEdit.Visible = true;
            }
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {

                string strRelationshipCode = gvAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CDAA_Name"].ToString();
                string panNum = gvAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CDAA_PanNum"].ToString();
                string gender = gvAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["SexShortName"].ToString();
                string relationshipName = gvAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XR_RelationshipCode"].ToString();
                string date = gvAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CDAA_DOB"].ToString();
                string AssociateTypeNo = gvAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CDAA_AssociateTypeNo"].ToString();
               
                 DateTime  txtDate=DateTime.Now;
                if (date != "")
                
                  txtDate = Convert.ToDateTime(gvAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CDAA_DOB"].ToString());
                
                
                int iskyc = int.Parse(gvAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CDAA_IsKYC"].ToString());
                string associateType = gvAssociate.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CDAA_AssociateType"].ToString();
                DropDownList ddlGender = (DropDownList)e.Item.FindControl("ddlGender");
                if (gender == "M" || gender == "F")
                    ddlGender.SelectedValue = gender;
                else
                    ddlGender.SelectedValue = "S";
                CheckBox chkyc = (CheckBox)e.Item.FindControl("chkKYC");
                if (iskyc == 1)
                    chkyc.Checked = true;
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;

                DataTable dtRelationship = customerBo.GetMemberRelationShip();
                TextBox txtMember = (TextBox)editedItem.FindControl("txtMember");
                Label lblGetPan = (Label)editedItem.FindControl("lblGetPan");
                TextBox txtNewMemPan = (TextBox)e.Item.FindControl("txtNewPan");
                TextBox txtPan = (TextBox)editedItem.FindControl("txtPan");

                RadDatePicker rdDate = (RadDatePicker)editedItem.FindControl("txtDOB");
                if(date!="")
                rdDate.SelectedDate = DateTime.Parse(txtDate.ToShortDateString());
                DropDownList ddlrelation = (DropDownList)editedItem.FindControl("ddlNewRelationship");
                DropDownList ddlassociateType = (DropDownList)editedItem.FindControl("ddlAssociate");
                ddlassociateType.SelectedValue = associateType.Trim() + AssociateTypeNo;

                ddlrelation.DataSource = dtRelationship;
                ddlrelation.DataTextField = dtRelationship.Columns["XR_Relationship"].ToString();
                ddlrelation.DataValueField = dtRelationship.Columns["XR_RelationshipCode"].ToString();
                ddlrelation.DataBind();
                ddlrelation.Items.Insert(0, new ListItem("Select", "Select"));
                ddlrelation.SelectedValue = relationshipName;


            }


        }

       

    }
}
