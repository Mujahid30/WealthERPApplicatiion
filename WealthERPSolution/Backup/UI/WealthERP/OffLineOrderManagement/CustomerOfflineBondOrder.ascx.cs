﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoCommon;
using WealthERP.Base;
using System.Data;
using BoOnlineOrderManagement;
using BoOfflineOrderManagement;
using BoOps;
using Telerik.Web.UI;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCustomerPortfolio;
using VoCustomerPortfolio;

namespace WealthERP.OffLineOrderManagement
{
    public partial class CustomerOfflineBondOrder : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo;
        CustomerVo customerVO = new CustomerVo();
        UserVo userVo;
        OfflineBondOrderBo OfflineBondOrderBo = new OfflineBondOrderBo();

        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        CustomerVo customerVo = new CustomerVo();
        DataSet dsAssetCategories;
        DataSet dsCustomerAssociates;
        DataSet dsAssetSubCategories;
        DataTable dtCustomerAssociatesRaw = new DataTable();
        DataTable dtCustomerAssociates = new DataTable();
        DataRow drCustomerAssociates;
        DataTable dtModeOfHolding;
        string path;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
          
            //customerVO = (CustomerVo)Session["customerVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];

            userVo = (UserVo)Session[SessionContents.UserVo];








           
            if (!IsPostBack)
            {
                
                //Cache.Remove("BondOrderBookList" + userVo.UserId.ToString());
                BindNcdCategory();
               LoadNominees();
                LoadModeOfHolding(path);
               
                if (Request.QueryString["COADID"] != null)
                {
                    btnCreateAllotment.Visible = false;
                    BindControlData();
                    if (Request.QueryString["action"] == "View")
                    {
                        ControlVisiblity(false);
                        lnkEdit.Visible = true;
                       
                    }
                    else
                    {
                        ControlVisiblity(true);
                        btnUpdate.Visible = true;

                    }
                }
            }
        }

        protected bool ControlVisiblity(bool type)
        {
            ddlCategory.Enabled = type;
            ddlIssueCategory.Enabled = type;
            ddlIssue.Enabled = type;
            ddlSeries.Enabled = type;
            txtMaturityAmount.Enabled = type;
            txtQuentity.Enabled = type;
            textPrice.Enabled = type;
            RadMaturityDate.Enabled = type;
            txtOrderFrom.Enabled = type;
            txtOrderTo.Enabled = type;
            gvNominees.Enabled = type;
            gvJointHoldersList.Enabled = type;
            rbtnYes.Enabled = type;
            rbtnNo.Enabled = type;
            return type;
        }
        protected void BindControlData()
        {
            ddlCategory.SelectedValue = Request.QueryString["category"].ToString();
            if (ddlCategory.SelectedValue == "FICGCG")
            {
                tdIssuerCategory.Visible = false;
                tdIssueCategory.Visible = false;
            }
            else
            {
                tdIssuerCategory.Visible = true;
                tdIssueCategory.Visible = true;
            }
            BindIssue(ddlCategory.SelectedValue);
            DataSet ds = OfflineBondOrderBo.GetCustomerAllotedDetailData(int.Parse(Request.QueryString["COADID"]));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ddlCategory.SelectedValue = dr["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlIssue.SelectedValue = dr["AIM_IssueId"].ToString();
                BindIssueCategory(int.Parse(ddlIssue.SelectedValue));
                ddlIssueCategory.SelectedValue = dr["AIIC_InvestorCatgeoryId"].ToString();
                IssureSeries(int.Parse(ddlIssue.SelectedValue));
                ddlSeries.SelectedValue = dr["AID_IssueDetailId"].ToString();
                BindSeriesDetails();
                txtMaturityAmount.Text = dr["COAD_MaturityAmount"].ToString();
                txtQuentity.Text = dr["COAD_Quantity"].ToString();
                txtInterestRate.Text = dr["COAD_InterestAmount"].ToString();
                textPrice.Text = dr["COAD_Price"].ToString();
                if(dr["COAD_MaturityDate"].ToString()!="" && dr["COAD_MaturityDate"].ToString()!=null)
                RadMaturityDate.SelectedDate = Convert.ToDateTime(dr["COAD_MaturityDate"].ToString());
                if (dr["COAD_OrderDate"].ToString() != "" && dr["COAD_OrderDate"].ToString() != null)
                txtOrderFrom.SelectedDate = Convert.ToDateTime(dr["COAD_OrderDate"].ToString());
                if (dr["AIM_AllotmentDate"].ToString() != "" && dr["AIM_AllotmentDate"].ToString() != null)
                txtOrderTo.SelectedDate = Convert.ToDateTime(dr["AIM_AllotmentDate"].ToString());
                txtCurrentPrice.Text = dr["currentValue"].ToString();
            }
        }
        private void BindNcdCategory()
        {
            DataTable dtCategory = new DataTable();
            OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
            dtCategory = onlineNCDBackOfficeBo.BindNcdCategory("SubInstrumentCat", "").Tables[0];
            if (dtCategory.Rows.Count > 0)
            {
                ddlCategory.DataSource = dtCategory;
                ddlCategory.DataValueField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlCategory.DataTextField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlCategory.DataBind();
            }
            ddlCategory.Items.Insert(0, new ListItem("Select", "0"));

        }
        protected void ddlCategory_Selectedindexchanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue == "FICGCG")
            {
                tdIssuerCategory.Visible = false;
                tdIssueCategory.Visible = false;
            }
            else
            {
                tdIssuerCategory.Visible = true;
                tdIssueCategory.Visible = true;
            }
            if (ddlCategory.SelectedValue != "0")
            {
                BindIssue(ddlCategory.SelectedValue);
                //ddlCategory.Enabled = false;
            }
        }

        protected void BindIssue(string Category)
        {
            DataTable dt = OfflineBondOrderBo.GetFDIddueList(Category);
            ddlIssue.DataSource = dt;
            ddlIssue.DataValueField = "AIM_IssueId";
            ddlIssue.DataTextField = "AIM_IssueName";
            ddlIssue.DataBind();
            ddlIssue.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void ddlIssue_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssue.SelectedValue != "0")
            {
                IssureSeries(int.Parse(ddlIssue.SelectedValue));
                BindIssueCategory(int.Parse(ddlIssue.SelectedValue));
            }
        }
        protected void ddlSeries_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSeries.SelectedValue != "0")
            {
                BindSeriesDetails();
            }
        }

        protected void BindSeriesDetails()
        {
            BindFrequency();
            DataSet ds = OfflineBondOrderBo.GetIntrestFrequency(int.Parse(ddlSeries.SelectedValue));

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ddlFrequency.SelectedValue = dr["WCMV_Lookup_FreqId"].ToString();
            }

            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                txtInterestRate.Text = dr["AIDCSR_DefaultInterestRate"].ToString();
            }
        }

        private void IssureSeries(int IssueId)
        {
            FIOrderBo fiorderBo = new FIOrderBo();
            DataSet dsScheme = fiorderBo.GetFISeries(IssueId);
            if (dsScheme.Tables[0].Rows.Count > 0)
            {
                ddlSeries.DataSource = dsScheme;
                ddlSeries.DataValueField = dsScheme.Tables[0].Columns["PFISD_SeriesId"].ToString();
                ddlSeries.DataTextField = dsScheme.Tables[0].Columns["PFISD_SeriesName"].ToString();
                ddlSeries.DataBind();
                ddlSeries.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        protected void OnClick_Edit(object sender, EventArgs e)
        {
            ControlVisiblity(true);
            btnUpdate.Visible = true;
            lnkEdit.Visible = false;
        }
        protected void OnClick_lnkbuttonAddSeries(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineNCDIssueSetup", "loadcontrol('OnlineNCDIssueSetup');", true);
        }
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            int i = 0;
            DataTable dtBondOrder;
            if (Cache["BondOrderBookList" + userVo.UserId.ToString()] == null)
            {
                dtBondOrder = new DataTable();
                dtBondOrder.Columns.Add("SeriesId");
                dtBondOrder.Columns.Add("Quentity");
                dtBondOrder.Columns.Add("Price");
                dtBondOrder.Columns.Add("issuecategory");
                dtBondOrder.Columns.Add("MaturityDate", typeof(DateTime));
                dtBondOrder.Columns.Add("MaturityAmount");
                dtBondOrder.Columns.Add("Frequency");
                dtBondOrder.Columns.Add("FrequencyText");
                dtBondOrder.Columns.Add("InterestRate");
                DataRow dr = dtBondOrder.NewRow();
                dr["SeriesId"] = ddlSeries.SelectedValue;
                dr["Quentity"] = txtQuentity.Text;
                dr["Price"] = textPrice.Text;
                dr["issuecategory"] = ddlIssueCategory.SelectedValue;
                dr["MaturityDate"] = RadMaturityDate.SelectedDate;
                dr["MaturityAmount"] = (!string.IsNullOrEmpty(txtMaturityAmount.Text)) ? txtMaturityAmount.Text : "0";
                dr["Frequency"] = ddlFrequency.SelectedValue;
                dr["InterestRate"] = txtInterestRate.Text;
                dr["FrequencyText"] = ddlFrequency.SelectedItem.Text;
                dtBondOrder.Rows.Add(dr);


                if (this.gvJointHoldersList.Rows.Count > 0)
                {
                    foreach (GridViewRow gvr in this.gvJointHoldersList.Rows)
                    {
                        if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                        {
                            i++;
                            customerAccountAssociationVo.AssociationId = int.Parse(gvJointHoldersList.DataKeys[gvr.RowIndex].Values[1].ToString());
                            customerAccountAssociationVo.AssociationType = "Joint Holder";
                            customerAccountBo.CreateFixedIncomeAccountAssociation(customerAccountAssociationVo, userVo.UserId);
                        }

                    }
                }
                else
                {
                    i = -1;
                }
                foreach (GridViewRow gvr in this.gvNominees.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId0")).Checked == true)
                    {
                        i++;
                        customerAccountAssociationVo.AssociationId = int.Parse(gvNominees.DataKeys[gvr.RowIndex].Values[1].ToString());
                        customerAccountAssociationVo.AssociationType = "Nominee";
                        customerAccountBo.CreateFixedIncomeAccountAssociation(customerAccountAssociationVo, userVo.UserId);//change after making all classes
                    }
                }



            }
            else
            {
                string str = "SeriesId=" + ddlSeries.SelectedValue;
                dtBondOrder = (DataTable)Cache["BondOrderBookList" + userVo.UserId.ToString()];
                DataRow[] rowss = dtBondOrder.Select(str);
                if (rowss.Length >= 1)
                {
                    return;
                }
                DataRow dr = dtBondOrder.NewRow();
                dr["SeriesId"] = ddlSeries.SelectedValue;
                dr["Quentity"] = txtQuentity.Text;
                dr["Price"] = textPrice.Text;
                dr["issuecategory"] = ddlIssueCategory.SelectedValue;
                dr["MaturityDate"] = RadMaturityDate.SelectedDate;
                dr["MaturityAmount"] = (!string.IsNullOrEmpty(txtMaturityAmount.Text)) ? txtMaturityAmount.Text : "0";
                dr["Frequency"] = ddlFrequency.SelectedValue;
                dr["InterestRate"] = txtInterestRate.Text;
                dr["FrequencyText"] = ddlFrequency.SelectedItem.Text;
                dtBondOrder.Rows.Add(dr);
            }
            BindOrderList(dtBondOrder);
        }

        protected void BindOrderList(DataTable dtOrder)
        {
            Cache.Remove("BondOrderBookList" + userVo.UserId.ToString());
            Cache.Insert("BondOrderBookList" + userVo.UserId.ToString(), dtOrder);
            btnCreateAllotment.Visible = true;
            pnlGrid.Visible = true;
            gvBondOrderList.DataSource = dtOrder;
            gvBondOrderList.DataBind();

        }
        protected void gvBondOrderList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtOrder;
            dtOrder = (DataTable)Cache["BondOrderBookList" + userVo.UserId.ToString()];
            if (dtOrder != null)
            {
                gvBondOrderList.DataSource = dtOrder;
            }

        }
        private void BindIssueCategory(int issueId)
        {
            OfflineIPOOrderBo OfflineIPOOrderBo = new OfflineIPOOrderBo();
            DataTable dt = OfflineIPOOrderBo.GetIssueCategory(issueId);
            ddlIssueCategory.DataSource = dt;
            ddlIssueCategory.DataValueField = "AIIC_InvestorCatgeoryId";
            ddlIssueCategory.DataTextField = "AIIC_InvestorCatgeoryName";
            ddlIssueCategory.DataBind();
            //ddlIssueCategory.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }
        protected void OnClick_btnUpdate(object sender, EventArgs e)
        {
            OfflineBondOrderBo OfflineBondOrderBo = new OfflineBondOrderBo();
            OfflineBondOrderBo.UpdateAllotmentDetails(userVo.UserId, SaveDetails(),int.Parse(Request.QueryString["COADID"]));
            btnUpdate.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Bond order Updated');", true);
            ControlVisiblity(false);
        }
        protected DataTable SaveDetails()
        {
            DataTable dtOrder = new DataTable();
            dtOrder.Columns.Add("Category");
            dtOrder.Columns.Add("IssuerId");
            dtOrder.Columns.Add("IssuerCategory");
            dtOrder.Columns.Add("CustomerId");
            dtOrder.Columns.Add("SeriesId");
            dtOrder.Columns.Add("OrderQuentity");
            dtOrder.Columns.Add("Price");
            dtOrder.Columns.Add("OrderDate");
            dtOrder.Columns.Add("AllotmentDate", typeof(DateTime));
            dtOrder.Columns.Add("AIDR_Id");
            dtOrder.Columns.Add("MaturityDate", typeof(DateTime));
            dtOrder.Columns.Add("MaturityAmount");
            dtOrder.Columns.Add("Frequency");
            dtOrder.Columns.Add("InterestRate");

            DataRow dr;

            dr = dtOrder.NewRow();
            dr["Category"] = ddlCategory.SelectedValue;
            dr["IssuerId"] = ddlIssue.SelectedValue;
            dr["IssuerCategory"] = ddlIssueCategory.SelectedValue;
            dr["CustomerId"] = customerVO.CustomerId;
            dr["SeriesId"] = ddlSeries.SelectedValue; //gvBondOrderList.MasterTableView.DataKeyValues[row.ItemIndex]["SeriesId"].ToString();
            dr["OrderQuentity"] = txtQuentity.Text; //gvBondOrderList.MasterTableView.DataKeyValues[row.ItemIndex]["Quentity"].ToString();
            dr["Price"] = textPrice.Text;//gvBondOrderList.MasterTableView.DataKeyValues[row.ItemIndex]["Price"].ToString();
            if (ddlIssueCategory.SelectedValue != "0" && ddlIssueCategory.SelectedValue !="")
                dr["AIDR_Id"] = OfflineBondOrderBo.GetAdviserIssueDetailsId(int.Parse(ddlIssueCategory.SelectedValue));
            dr["OrderDate"] = (txtOrderFrom.SelectedDate!=null)?txtOrderFrom.SelectedDate.ToString(): string.Empty ;
            dr["MaturityDate"] =RadMaturityDate.SelectedDate;
            dr["MaturityAmount"] = (!string.IsNullOrEmpty(txtMaturityAmount.Text )) ? txtMaturityAmount.Text : 0.ToString();
            dr["Frequency"] = ddlFrequency.SelectedValue;
            dr["InterestRate"] = txtInterestRate.Text;
            dr["AllotmentDate"] = txtOrderTo.SelectedDate;
            dtOrder.Rows.Add(dr);
            return dtOrder;
        }
        protected void OnClick_btnCreateAllotment(object sender, EventArgs e)
        {
            OfflineBondOrderBo OfflineBondOrderBo = new OfflineBondOrderBo();
            OfflineBondOrderBo.CreateAllotmentDetails(userVo.UserId, SaveDetails());
            //Cache.Remove("BondOrderBookList" + userVo.UserId.ToString());
            ControlVisiblity(false);
            btnCreateAllotment.Visible = false;
            btnGo.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Bond order created');", true);
        }
        private void BindFrequency()
        {
            DataTable dtFrequency = new DataTable();
            OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
            dtFrequency = onlineNCDBackOfficeBo.GetFrequency();
            if (dtFrequency.Rows.Count > 0)
            {
                ddlFrequency.DataSource = dtFrequency;
                ddlFrequency.DataValueField = dtFrequency.Columns["WCMV_LookupId"].ToString();
                ddlFrequency.DataTextField = dtFrequency.Columns["WCMV_Name"].ToString();
                ddlFrequency.DataBind();
            }
        }

        protected void textPrice_OnTextChanged(object sender, EventArgs e)
        {
            if((!string.IsNullOrEmpty(txtQuentity.Text)) && (!string.IsNullOrEmpty(textPrice.Text)))
            {
                txtCurrentPrice.Text=(int.Parse(txtQuentity.Text)*Convert.ToDecimal(textPrice.Text)).ToString();
            }
        }




        public void LoadNominees()
        {

            try
            {
                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
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

                    trNoNominee.Visible = false;
                    trNominees.Visible = true;
                }
                else
                {
                    trNoNominee.Visible = true;
                    trNominees.Visible = false;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }


        protected void rbtnYes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
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
                        trNoJointHolders.Visible = false;
                        trJoinHolders.Visible = true;
                        trJointHolderGrid.Visible = true;
                        gvJointHoldersList.DataSource = dtCustomerAssociates;
                        gvJointHoldersList.DataBind();
                        gvJointHoldersList.Visible = true;
                    }
                    else
                    {
                        trNoJointHolders.Visible = true;
                        trJoinHolders.Visible = false;
                        trJointHolderGrid.Visible = false;
                    }
                    //ddlModeOfHolding.SelectedIndex = 0;
                }
                else
                {
                    ddlModeOfHolding.SelectedValue = "SI";
                    ddlModeOfHolding.Enabled = false;
                    trJoinHolders.Visible = false;
                    trJointHolderGrid.Visible = false;
                    trNoJointHolders.Visible = false;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }


        private void LoadModeOfHolding(string path)
        {
            try
            {
                //dtModeOfHolding = XMLBo.GetModeOfHolding(path);
                //ddlModeOfHolding.DataSource = dtModeOfHolding;
                //ddlModeOfHolding.DataTextField = "ModeOfHolding";
                //ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
                //ddlModeOfHolding.DataBind();
                //ddlModeOfHolding.Items.Insert(0, new ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }


           

        }



    }
}