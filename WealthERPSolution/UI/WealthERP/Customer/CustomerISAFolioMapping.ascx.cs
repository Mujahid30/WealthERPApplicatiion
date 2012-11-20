﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using VoUser;
using BoCommon;
using BoUploads;
using VoCustomerProfiling;
using BoCustomerProfiling;
using BoAdvisorProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using WealthERP.Base;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;




namespace WealthERP.Customer
{
    public partial class CustomerISAFolioMapping : System.Web.UI.UserControl
    {
        UserVo userVo = null;
        AdvisorVo advisorVo = new AdvisorVo();
        CustomerBo customerBo = new CustomerBo();
        RMVo rmVo = new RMVo();
        DataSet dsCustomerAssociates = new DataSet();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
         

        protected void Page_Load(object sender, EventArgs e)
       {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session["rmVo"];
            if (!string.IsNullOrEmpty(Session[SessionContents.RmVo].ToString()))
                rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
            if (!IsPostBack)
            {
                BindBranchDropDown();
                trPan.Visible = false;
                trISAList.Visible = false;
                trHoldings.Visible = false;
                gvAvailableFolio.Visible = false;
                gvAttachedFolio.Visible = false;
                btnGo.Visible = false;
                //txtMember_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                //txtMember_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                txtMember_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtMember_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                
            }

            
        }

        private void BindISAList()
        {
            DataTable dtGetISAList;
            if(!string.IsNullOrEmpty(txtCustomerId.Value))
            {
                dtGetISAList = customerAccountBo.GetISAListForFolioMapping(int.Parse(txtCustomerId.Value));
                if (dtGetISAList.Rows.Count > 0)
                {
                    ddlCustomerISAAccount.DataSource = dtGetISAList;
                    ddlCustomerISAAccount.DataValueField = dtGetISAList.Columns["CISAA_accountid"].ToString();
                    ddlCustomerISAAccount.DataTextField = dtGetISAList.Columns["CISAA_AccountNumber"].ToString();
                    ddlCustomerISAAccount.DataBind();

                }
                ddlCustomerISAAccount.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }

        }

        private void BindBranchDropDown()
        {
           
            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null)
                {
                    ddlMemberBranch.DataSource = ds;
                    ddlMemberBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlMemberBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlMemberBranch.DataBind();
                }
                ddlMemberBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void ddlMemberBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMemberBranch.SelectedIndex == 0)
            {
                //txtMember_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                //txtMember_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                txtMember_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtMember_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
            }
            else
            {
                txtMember_autoCompleteExtender.ContextKey = ddlMemberBranch.SelectedValue.ToString();
                txtMember_autoCompleteExtender.ServiceMethod = "GetPerticularBranchsAllIndividualCustomers";
            }


        }

        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            if (txtCustomerId.Value != string.Empty)
            {
                DataTable dt = customerBo.GetCustomerPanAddress(int.Parse(txtCustomerId.Value));
                DataRow dr = dt.Rows[0];
                trPan.Visible = true;
                trISAList.Visible = true;
                trHoldings.Visible = true;
                lblGetPan.Text = dr["C_PANNum"].ToString();
                BindISAList();
                LoadNominees();

            }
        }

        protected void ddlCustomerISAAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (ddlCustomerISAAccount.SelectedIndex != 0)
            {
                btnGo.Visible = true;
                DataTable dt = customerBo.GetISAHoldings(int.Parse(ddlCustomerISAAccount.SelectedValue));
                DataRow dr = dt.Rows[0];
                lblISAHoldingTypeValue.Text = dr["XMOH_ModeOfHolding"].ToString();
                BindAvailableFolioGrid(int.Parse(txtCustomerId.Value), int.Parse(ddlCustomerISAAccount.SelectedValue));
                BindAttachedFolioGrid(int.Parse(ddlCustomerISAAccount.SelectedValue));
            }
        }

        private void BindAttachedFolioGrid(int AccountId)
        {
            DataTable dtGetAttachedFolioGrid;
            dtGetAttachedFolioGrid = customerAccountBo.GetBindAttachedFolioGrid(AccountId);
            gvAttachedFolio.DataSource = dtGetAttachedFolioGrid;
            gvAttachedFolio.DataBind();
            gvAttachedFolio.Visible = true;
        }

        private void BindAvailableFolioGrid(int CustomerId, int AccountId)
        {
            DataTable dtGetAvailableFolioList;
            dtGetAvailableFolioList = customerAccountBo.GetAvailableFolioList(CustomerId,AccountId);
            gvAvailableFolio.DataSource = dtGetAvailableFolioList;
            gvAvailableFolio.DataBind();
            gvAvailableFolio.Visible = true;
        }
        private void LoadNominees()
        {
            DataTable dtCustomerAssociates = new DataTable();
            DataTable dtNewCustomerAssociate = new DataTable();
            DataRow drCustomerAssociates;
            if (txtCustomerId.Value != string.Empty)
            {
                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(int.Parse(txtCustomerId.Value));
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
                    tdNominees.Visible = true;

                    gvJointHoldersList.DataSource = dtNewCustomerAssociate;
                    gvJointHoldersList.DataBind();

                    gvJointHoldersList.Visible = true;
                    pnlJointholders.Visible = true;
                    //tdJointHolders.Visible = true;

                }
                else
                {
                    trAssociate.Visible = false;
                }
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            int i = 0;
            int isaAccountId = 0;
            if (ddlCustomerISAAccount.SelectedIndex != 0)
                isaAccountId =int.Parse(ddlCustomerISAAccount.SelectedValue);
            int CMFA_AccountId = 0;
            int AMCCode = 0;
            int IsJointlyHeld = 0;
            int PortfolioId = 0;
            string ModeOfHoldingCode = "";
            bool result = false;
            foreach (GridDataItem gvRow in gvAvailableFolio.Items)
            {

                CheckBox chk = (CheckBox)gvRow.FindControl("cbRecons");
                if (chk.Checked)
                {
                    i++;
                }

            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a record!');", true);
                BindAvailableFolioGrid(int.Parse(txtCustomerId.Value), int.Parse(ddlCustomerISAAccount.SelectedValue));
            }
            else
            {
                foreach (GridDataItem gdi in gvAvailableFolio.Items)
                {
                    if (((CheckBox)gdi.FindControl("cbRecons")).Checked == true)
                    {
                        int selectedRow = gdi.ItemIndex + 1;
                        CMFA_AccountId = Convert.ToInt32(gvAvailableFolio.MasterTableView.DataKeyValues[selectedRow - 1]["CMFA_AccountId"].ToString());
                        AMCCode = Convert.ToInt32(gvAvailableFolio.MasterTableView.DataKeyValues[selectedRow - 1]["PA_AMCCode"].ToString());
                        PortfolioId = Convert.ToInt32(gvAvailableFolio.MasterTableView.DataKeyValues[selectedRow - 1]["CP_PortfolioId"].ToString());
                        ModeOfHoldingCode = gvAvailableFolio.MasterTableView.DataKeyValues[selectedRow - 1]["XMOH_ModeOfHoldingCode"].ToString();
                        IsJointlyHeld =Convert.ToInt16(gvAvailableFolio.MasterTableView.DataKeyValues[selectedRow - 1]["CMFA_IsJointlyHeld"].ToString());
                        result = customerAccountBo.UpdateMFAccount(CMFA_AccountId,isaAccountId, AMCCode, PortfolioId, ModeOfHoldingCode, IsJointlyHeld);
                        
                    }
                }
                BindAttachedFolioGrid(isaAccountId);
            }
        }


    }
}