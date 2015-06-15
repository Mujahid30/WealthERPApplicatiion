﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoWerpAdmin;
using BoAdvisorProfiling;
using BOAssociates;
using BoCommisionManagement;
using VOAssociates;
using BoCommon;
using System.Globalization;
using System.Data;
using BoOnlineOrderManagement;
using WealthERP.Base;
using Telerik.Web.UI;

namespace WealthERP.Uploads
{
    public partial class CustomerUploadNew : System.Web.UI.UserControl
    {
        PriceBo priceBo = new PriceBo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        AssociatesBo associatesBo = new AssociatesBo();
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        string categoryCode = string.Empty;
        int amcCode = 0;
        string AgentCode = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                BindMutualFundDropDowns();
                BindNAVCategory();
                LoadAllSchemeList(0);
                BindProductDropdown();
                BindMonthsAndYear();

                int day = 1;
               // btnExportFilteredData.Visible = false;
                associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                if (associateuserheirarchyVo != null && associateuserheirarchyVo.AgentCode != null)
                {
                    AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                    ddlSelectMode.Items.FindByText("Both").Enabled = false;
                    ddlSelectMode.Items.FindByText("Online-Only").Enabled = false;
                }
            }

        }
        private void BindMonthsAndYear()
        {
            for (int i = 1; i <= 12; i++)
            {
                string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                ddlMnthQtr.Items.Add(new ListItem(monthName, i.ToString().PadLeft(2, '0')));
            }
            ddlMnthQtr.Items.Add(new ListItem("Quarter April-June", "13"));
            ddlMnthQtr.Items.Add(new ListItem("Quarter July-September", "14"));
            ddlMnthQtr.Items.Add(new ListItem("Quarter October-December", "15"));
            ddlMnthQtr.Items.Add(new ListItem("Quarter January-March", "16"));
            ddlMnthQtr.Items.Insert(0, new ListItem("Select", "0"));
            for (int i = DateTime.Now.Year; i >= 2008; i--)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

        }

        protected void ddlIssuer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssuer.SelectedIndex != 0)
            {
                int amcCode = int.Parse(ddlIssuer.SelectedValue);
                ddlCategory.SelectedIndex = 0;
                LoadAllSchemeList(amcCode);

            }
        }



        protected void ddlSelectMode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssueType.SelectedIndex == 0)
            {
                return;
            }
            else
            {
                ddlIssueName.Items.Clear();
                ddlIssueName.DataBind();
                BindMappedIssues(ddlIssueType.SelectedValue, ddlProduct.SelectedValue, int.Parse(ddlSelectMode.SelectedValue), (ddlProductCategory.SelectedValue == "") ? "FIFIIP" : ddlProductCategory.SelectedValue);

            }
        }
        protected void ddlSearchType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            td1.Visible = true;
            td2.Visible = true;
            if (ddlSearchType.SelectedValue != "Select" && ddlProduct.SelectedValue == "MF")
            {
                td1.Visible = false;
                td2.Visible = false;
            }
        }
        protected void ddlIssueType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssueType.SelectedIndex != 0)
            {
                ddlIssueName.Items.Clear();
                ddlIssueName.DataBind();
                BindMappedIssues(ddlIssueType.SelectedValue, ddlProduct.SelectedValue, 0, (ddlProductCategory.SelectedValue == "") ? "FIFIIP" : ddlProductCategory.SelectedValue);

            }
        }
        private void BindMappedIssues(string ModeOfIssue, string productType, int isOnlineIssue, string SubCategoryCode)
        {
            DataSet dsCommissionReceivable = commisionReceivableBo.GetIssuesStructureMapings(advisorVo.advisorId, "MappedIssue", ModeOfIssue, productType, isOnlineIssue, 0, SubCategoryCode);
            if (dsCommissionReceivable.Tables[0].Rows.Count > 0)
            {
                ddlIssueName.DataSource = dsCommissionReceivable.Tables[0];
                ddlIssueName.DataTextField = dsCommissionReceivable.Tables[0].Columns["AIM_IssueName"].ToString();
                ddlIssueName.DataValueField = dsCommissionReceivable.Tables[0].Columns["AIM_IssueId"].ToString();
                ddlIssueName.DataBind();
                ddlIssueName.Items.Insert(0, new ListItem("All", "0"));
            }

        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssuer.SelectedIndex != 0)
            {
                if (ddlCategory.SelectedIndex != 0)
                {
                    int amcCode = int.Parse(ddlIssuer.SelectedValue);
                    LoadAllSchemeList(amcCode);
                    //GdBind_Click(sender,e);
                }

            }

        }

        public void BindMutualFundDropDowns()
        {
            PriceBo priceBo = new PriceBo();
            DataTable dtGetMutualFundList = new DataTable();
            dtGetMutualFundList = priceBo.GetMutualFundList();
            ddlIssuer.DataSource = dtGetMutualFundList;
            ddlIssuer.DataTextField = dtGetMutualFundList.Columns["PA_AMCName"].ToString();
            ddlIssuer.DataValueField = dtGetMutualFundList.Columns["PA_AMCCode"].ToString();
            ddlIssuer.DataBind();
            ddlIssuer.Items.Insert(0, new ListItem("All", "0"));


        }
        private void ShowHideControlsBasedOnProduct(string asset)
        {
            tdCategory.Visible = false;
            tdDdlCategory.Visible = false;
            ddlProductCategory.Items.Clear();
            ddlProductCategory.DataBind();
            td2.Visible = true;
            td1.Visible = true;


            if (asset == "MF")
            {
                trSelectMutualFund.Visible = true;
                trNCDIPO.Visible = false;
                tdFrom.Visible = true;
                tdTolbl.Visible = true;
                cvddlIssueType.Enabled = false;
                Label1.Visible = true;
                ddlCommType.Visible = true;
                td2.Visible = false;
                td1.Visible = false;


            }
            else if (asset == "FI" || asset == "IP")
            {
                trSelectMutualFund.Visible = false;
                trNCDIPO.Visible = true;
                tdTolbl.Visible = true;
                ddlIssueName.Items.Clear();
                ddlIssueName.DataBind();
                cvddlIssueType.Enabled = true;
                Label1.Visible = false;
                ddlCommType.Visible = false;
                td2.Visible = false;
                td1.Visible = false;


            }
            if (asset == "FI")
            {
                BindBondCategories();
                tdCategory.Visible = true;
                tdDdlCategory.Visible = true;
            }

        }
        private void BindNAVCategory()
        {
            DataSet dsNavCategory;
            DataTable dtNavCategory;
            dsNavCategory = priceBo.GetNavOverAllCategoryList();
            dtNavCategory = dsNavCategory.Tables[0];
            if (dtNavCategory.Rows.Count > 0)
            {

                ddlCategory.DataSource = dtNavCategory;
                ddlCategory.DataValueField = dtNavCategory.Columns["Category_Code"].ToString();
                ddlCategory.DataTextField = dtNavCategory.Columns["Category_Name"].ToString();
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("All", "All"));

            }
        }
        protected void ddlProductCategory_OnSelectedIndexChanged(object Sender, EventArgs e)
        {
            if (ddlProductCategory.SelectedValue != "Select")
            {
                td1.Visible = false;
                td2.Visible = false;
                if (ddlProductCategory.SelectedValue != "FISDSD")
                {
                    tdFrom.Visible = true;
                    tdTolbl.Visible = true;
                    td1.Visible = true;

                    td2.Visible = true;
                }
                BindMappedIssues(ddlIssueType.SelectedValue, ddlProduct.SelectedValue, 0, (ddlProductCategory.SelectedValue == "") ? "FIFIIP" : ddlProductCategory.SelectedValue);
            }
        }
        private void LoadAllSchemeList(int amcCode)
        {
            DataSet dsLoadAllScheme = new DataSet();
            DataTable dtLoadAllScheme = new DataTable();
            if (ddlIssuer.SelectedIndex != 0)
            {
                amcCode = int.Parse(ddlIssuer.SelectedValue.ToString());
                categoryCode = ddlCategory.SelectedValue;
                //dtLoadAllScheme = priceBo.GetAllScehmeList(amcCode);
                dsLoadAllScheme = priceBo.GetSchemeListCategoryConcatenation(amcCode, categoryCode);
                dtLoadAllScheme = dsLoadAllScheme.Tables[0];
            }

            if (dtLoadAllScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtLoadAllScheme;
                ddlScheme.DataTextField = dtLoadAllScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataValueField = dtLoadAllScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlScheme.Items.Clear();
                ddlScheme.DataSource = null;
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("All", "0"));
            }

        }
        private void SetParameters()
        {
            if (string.IsNullOrEmpty(ddlMnthQtr.SelectedItem.Value.ToString()) != true)
                hdnFromDate.Value = ddlMnthQtr.SelectedItem.Value.ToString();
            if (string.IsNullOrEmpty(ddlYear.SelectedItem.Value.ToString()) != true)
                hdnToDate.Value = ddlYear.SelectedItem.Value.ToString();
            if (string.IsNullOrEmpty(ddlScheme.SelectedItem.Value.ToString()) != true)
                hdnschemeId.Value = ddlScheme.SelectedItem.Value.ToString();
            if (string.IsNullOrEmpty(ddlCategory.SelectedItem.Value.ToString()) != true)
                hdnCategory.Value = ddlCategory.SelectedItem.Value.ToString();
            if (ddlIssuer.SelectedValue.ToString() != "Select")
                hdnSBbrokercode.Value = ddlIssuer.SelectedItem.Value.ToString();
            else
                hdnSBbrokercode.Value = "0";
            if (string.IsNullOrEmpty(ddlIssueName.SelectedValue.ToString()) != true)
                hdnIssueId.Value = ddlIssueName.SelectedValue.ToString();
            else
                hdnIssueId.Value = "0";
            if (ddlProductCategory.SelectedValue.ToString() != "Select")
                hdnProductCategory.Value = ddlProductCategory.SelectedValue.ToString();
            else
                hdnProductCategory.Value = "0";

        }
     
        protected void ddlProduct_SelectedIndexChanged(object source, EventArgs e)
        {

            ddlCommType.Items[3].Enabled = false;

            if (ddlProduct.SelectedValue != "Select")
            {
                if (ddlProduct.SelectedValue == "MF")
                {

                    
                    ddlCommType.Items[3].Enabled = false;
                }
                else
                {
                    //ddlOrderStatus.Items[1].Enabled = false;
                    //ddlOrderStatus.Items[2].Enabled = true;

                }
                if (ddlProduct.SelectedValue == "FI")
                {
                    ddlCommType.Items[2].Enabled = false;
                }
                else
                {
                    ddlCommType.Items[2].Enabled = true;
                }
                ShowHideControlsBasedOnProduct(ddlProduct.SelectedValue);
            }

        }
        private void BindProductDropdown()
        {
            DataSet dsLookupData = commisionReceivableBo.GetProductType();
            //Populating the product dropdown
            ddlProduct.DataSource = dsLookupData.Tables[0];
            ddlProduct.DataValueField = dsLookupData.Tables[0].Columns["PAG_AssetGroupCode"].ToString();
            ddlProduct.DataTextField = dsLookupData.Tables[0].Columns["PAG_AssetGroupName"].ToString();
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new ListItem("Select", "Select"));
        }
        protected void GetCommisionTypes()
        {

            ddlSearchType.Items.Insert(0, new ListItem("Select", "Select"));
            ddlSearchType.Items.Insert(1, new ListItem("Brokerage", "0"));
        }
        private void BindBondCategories()
        {
            OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
            DataTable dtCategory = new DataTable();
            dtCategory = onlineNCDBackOfficeBo.BindNcdCategory("SubInstrumentCat", "").Tables[0];
            if (dtCategory.Rows.Count > 0)
            {
                ddlProductCategory.DataSource = dtCategory;
                ddlProductCategory.DataValueField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                ddlProductCategory.DataTextField = dtCategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                ddlProductCategory.DataBind();
            }
            ddlProductCategory.Items.Insert(0, new ListItem("Select", "Select"));

        }
        private void BindGrid()
        {
            DataSet ds = new DataSet();
            //ds.ReadXml(Server.MapPath(@"\Sample.xml"));

            ds = adviserMFMIS.GetWERPCommissionDetails(ddlProduct.SelectedValue, advisorVo.advisorId, Int32.Parse(ddlMnthQtr.SelectedValue), Int32.Parse(ddlYear.SelectedValue), "", Int32.Parse(ddlIssueName.SelectedValue), ddlProductCategory.SelectedValue);
            if (ds.Tables[0] != null)
            {


                //btnExportFilteredData.Visible = true;
                btnSave.Visible = true;
                gvbrokerageRecon.Visible = true;
                gvbrokerageRecon.DataSource = ds.Tables[0];
                DataTable dtGetAMCTransactionDeatails = new DataTable();
                gvbrokerageRecon.DataBind();
                if (Cache["gvbrokerageRecon" + userVo.UserId.ToString()] != null)
                {
                    Cache.Remove("gvbrokerageRecon" + userVo.UserId.ToString());

                }
                Cache.Insert("gvbrokerageRecon" + userVo.UserId.ToString(), ds.Tables[0]);


            }
        
        }

        protected void GdBind_Click(Object sender, EventArgs e)
        {


            BindGrid();
        }

        protected void gvbrokerageRecon_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Cache["gvbrokerageRecon" + userVo.UserId];
            if (dt != null)
            {
                gvbrokerageRecon.DataSource = dt;
                gvbrokerageRecon.Visible = true;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id = 0;
            int ActRec = 0;
            int ActPay = 0;
            bool blResult = false;
            foreach (GridDataItem dr in gvbrokerageRecon.Items)
            {
                CheckBox checkBox = (CheckBox)dr.FindControl("chkId");
                if (checkBox.Checked == true)
                {
                    if (((TextBox)dr.FindControl("txtActRecBrokerage")).Text.Trim() != "")
                    {
                        ActRec = Convert.ToInt32(((TextBox)dr.FindControl("txtActRecBrokerage")).Text);
                    }
                    if (((TextBox)dr.FindControl("txtActPaybrokerage")).Text.Trim() != "")
                    {
                        ActPay = Convert.ToInt32(((TextBox)dr.FindControl("txtActPaybrokerage")).Text);
                    }

                    int selectedRow = 0;
                    GridDataItem gdi;
                    gdi = (GridDataItem)checkBox.NamingContainer;
                    selectedRow = gdi.ItemIndex ;
                   id = int.Parse((gvbrokerageRecon.MasterTableView.DataKeyValues[selectedRow]["WCD_Id"].ToString()));
                    blResult = adviserMFMIS.UpdateActualPayAndRec(id,ActPay,ActRec);

                }
                
            }
            BindGrid();

        }
    }
}