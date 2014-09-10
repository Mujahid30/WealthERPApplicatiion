using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoWerpAdmin;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using BoAdvisorProfiling;
using VoUser;
using BoUploads;
using BoCustomerGoalProfiling;
using Telerik.Web.UI;
using BoCommon;
using BoAdvisorProfiling;
using System.Configuration;
using BOAssociates;

namespace WealthERP.BusinessMIS
{
    public partial class CommissionReceivableRecon : System.Web.UI.UserControl
    {
        PriceBo priceBo = new PriceBo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        AssociatesBo associatesBo = new AssociatesBo();


        string categoryCode = string.Empty;
        int amcCode = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                BindMutualFundDropDowns();
                BindNAVCategory();
                LoadAllSchemeList(0);
                int day = 1;
                gvCommissionReceiveRecon.Visible = false;
                txtFrom.SelectedDate = DateTime.Parse(day.ToString() + '/' + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString());
                txtTo.SelectedDate = DateTime.Now;
                divBtnActionSection.Visible = true;
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
        private void SetParameters()
        {
            //if (userVo.UserType=="advisor")
            //{
            if (string.IsNullOrEmpty(txtFrom.SelectedDate.ToString()) != true)
                hdnFromDate.Value = txtFrom.SelectedDate.ToString();
            if (string.IsNullOrEmpty(txtTo.SelectedDate.ToString()) != true)
                hdnToDate.Value = txtTo.SelectedDate.ToString();
            if (string.IsNullOrEmpty(ddlScheme.SelectedItem.Value.ToString()) != true)
                hdnschemeId.Value = ddlScheme.SelectedItem.Value.ToString();
            if (string.IsNullOrEmpty(ddlCategory.SelectedItem.Value.ToString()) != true)
                hdnCategory.Value = ddlCategory.SelectedItem.Value.ToString();
            if (string.IsNullOrEmpty(ddlReconStatus.SelectedItem.Value.ToString()) != true)
                hdnrecon.Value = ddlReconStatus.SelectedItem.Value.ToString();
            if (string.IsNullOrEmpty(ddlIssuer.SelectedItem.Value.ToString()) != true)
                hdnSBbrokercode.Value = ddlIssuer.SelectedItem.Value.ToString();
            

            //}


        }
        protected void gvWERPTrans_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            { }

        }
        protected void GdBind_Click(Object sender, EventArgs e)
        {
            SetParameters();
            DataSet ds = new DataSet();
            //ds.ReadXml(Server.MapPath(@"\Sample.xml"));

            ds = adviserMFMIS.GetCommissionReceivableRecon(advisorVo.advisorId, int.Parse(hdnschemeId.Value), DateTime.Parse(hdnFromDate.Value), DateTime.Parse(hdnToDate.Value), hdnCategory.Value, hdnrecon.Value, ddlCommType.SelectedValue, int.Parse(hdnSBbrokercode.Value));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null)
                {
                    gvCommissionReceiveRecon.Visible = true;
                    gvCommissionReceiveRecon.DataSource = ds.Tables[0];
                    DataTable dtGetAMCTransactionDeatails = new DataTable();
                    gvCommissionReceiveRecon.DataBind();
                    if (Cache["gvCommissionReceiveRecon" + userVo.UserId.ToString()] == null)
                    {
                        Cache.Insert("gvCommissionReceiveRecon" + userVo.UserId.ToString(), ds.Tables[0]);
                    }
                    else
                    {
                        Cache.Remove("gvCommissionReceiveRecon" + userVo.UserId.ToString());
                        Cache.Insert("gvCommissionReceiveRecon" + userVo.UserId.ToString(), ds.Tables[0]);
                    }
                }
                else
                {
                    gvCommissionReceiveRecon.Visible = false;

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
        protected void btnUpload_click(object sender, EventArgs e)
        {
            int transid;
            foreach (GridDataItem dr in gvCommissionReceiveRecon.Items)
            {
                if (dr["ReconStatus"].Text == "closed")
                {
                    msgReconClosed.Visible = true;
                }
                else
                {
                    CheckBox checkBox = (CheckBox)dr.FindControl("chkId");
                    if (checkBox.Checked)
                    {
                        int selectedRow = 0;
                        GridDataItem gdi;
                        gdi = (GridDataItem)checkBox.NamingContainer;
                        selectedRow = gdi.ItemIndex + 1;
                        transid = int.Parse((gvCommissionReceiveRecon.MasterTableView.DataKeyValues[selectedRow - 1]["CMFT_MFTransId"].ToString()));
                        bool result = adviserMFMIS.MarkReconStatus(transid);
                        msgReconComplete.Visible = true;
                        dr["ReconStatus"].Text = "closed";
                    }
                }
            }
        }
        protected void btnSubmit_click(object sender, EventArgs e)
        {
            bool blResult = false;
            int transid;
            double expectedamount;
            double adjustedValue;
            string adjustValue = string.Empty;
            foreach (GridDataItem dr in gvCommissionReceiveRecon.Items)
            {
                if (dr["ReconStatus"].Text == "closed")
                {
                    msgReconClosed.Visible = true;
                }
                else
                {
                    CheckBox checkBox = (CheckBox)dr.FindControl("chkId");
                    if (checkBox.Checked)
                    {
                        int selectedRow = 0;
                        GridDataItem gdi;
                        gdi = (GridDataItem)checkBox.NamingContainer;
                        selectedRow = gdi.ItemIndex + 1;
                        transid = int.Parse((gvCommissionReceiveRecon.MasterTableView.DataKeyValues[selectedRow - 1]["CMFT_MFTransId"].ToString()));
                        expectedamount = double.Parse(dr["expectedamount"].Text);
                        GridFooterItem footerRow = (GridFooterItem)gvCommissionReceiveRecon.MasterTableView.GetItems(GridItemType.Footer)[0];
                        if (((TextBox)footerRow.FindControl("txtAdjustAmountMultiple")).Text.Trim() == "")
                        {
                            adjustValue = ((TextBox)dr.FindControl("txtAdjustAmount")).Text;

                        }
                        else
                        {
                            adjustValue = ((TextBox)footerRow.FindControl("txtAdjustAmountMultiple")).Text;

                        }
                        blResult = adviserMFMIS.SaveReceivableReconChanges(transid, double.Parse(adjustValue), expectedamount);

                    }
                }


            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string adjustValue = string.Empty;

            string UpdatedExpectedAmt = string.Empty;


            GridFooterItem footerRow = (GridFooterItem)gvCommissionReceiveRecon.MasterTableView.GetItems(GridItemType.Footer)[0];
            foreach (GridDataItem dr in gvCommissionReceiveRecon.Items)
            {
                if (dr["ReconStatus"].Text == "closed")
                {
                    msgReconClosed.Visible = true;
                }
                else
                {
                    if (((TextBox)footerRow.FindControl("txtAdjustAmountMultiple")).Text.Trim() == "")
                    {
                        adjustValue = ((TextBox)dr.FindControl("txtAdjustAmount")).Text;

                    }
                    else
                    {
                        adjustValue = ((TextBox)footerRow.FindControl("txtAdjustAmountMultiple")).Text;

                    }
                    CheckBox checkBox = (CheckBox)dr.FindControl("chkId");
                    if (checkBox.Checked)
                    {
                        if (!(string.IsNullOrEmpty(dr["expectedamount"].Text)) & !string.IsNullOrEmpty(adjustValue))
                        {
                            UpdatedExpectedAmt = (double.Parse(dr["expectedamount"].Text) + double.Parse(adjustValue)).ToString();
                            dr["UpdatedExpectedAmount"].Text = UpdatedExpectedAmt;
                        }
                        else if (!(string.IsNullOrEmpty(dr["expectedamount"].Text)))
                        {
                            UpdatedExpectedAmt = (double.Parse(dr["expectedamount"].Text)).ToString();
                            dr["UpdatedExpectedAmount"].Text = UpdatedExpectedAmt;

                        }
                        else if (!string.IsNullOrEmpty(adjustValue))
                        {
                            UpdatedExpectedAmt = (double.Parse(adjustValue)).ToString();
                            dr["UpdatedExpectedAmount"].Text = UpdatedExpectedAmt;
                        }
                    }


                }

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

        protected void gvCommissionReceiveRecon_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            string rcbType = string.Empty;
            dt = (DataTable)Cache["gvCommissionReceiveRecon" + userVo.UserId];
            if (ViewState["CommissionReceiveRecon"] != null)
                rcbType = ViewState["CommissionReceiveRecon"].ToString();
            if (!string.IsNullOrEmpty(rcbType))
            {
                DataView dvStaffList = new DataView(dt, "CommissionReceiveRecon = '" + rcbType + "'", "schemeplanname,transactiondate,amount,transactiontype,Age,currentvalue,expectedamount,calculatedDate,receivedamount,diff,ACSR_CommissionStructureRuleId,CMFT_MFTransId,CMFT_ReceivableExpectedAmount,CMFT_ReceivedCommissionAdjustment", DataViewRowState.CurrentRows);
                // DataView dvStaffList = dtMIS.DefaultView;
                gvCommissionReceiveRecon.DataSource = dvStaffList.ToTable();
                gvCommissionReceiveRecon.Visible = true;

            }
            else
            {
                gvCommissionReceiveRecon.DataSource = dt;
                gvCommissionReceiveRecon.Visible = true;

            }



        }
    }
}