using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCommon;
using System.Data.Common;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using BoOnlineOrderManagement;
using System.Configuration;
using VoUser;
using VoOnlineOrderManagemnet;

namespace WealthERP.OnlineOrderManagement
{
    public partial class MFOrderRdemptionTransType : System.Web.UI.UserControl
    {
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        OnlineMFOrderBo onlineMforderBo = new OnlineMFOrderBo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        CustomerVo customerVo = new CustomerVo();
        OnlineMFOrderVo onlinemforderVo = new OnlineMFOrderVo();
        UserVo userVo;
        string path;
        DataSet dsCustomerAssociates = new DataSet();
        DataTable dtCustomerAssociatesRaw = new DataTable();
        DataTable dtCustomerAssociates = new DataTable();
        DataRow drCustomerAssociates;
        int accountId;
        int OrderId;


        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session["userVo"];
            if (!IsPostBack)
            {
                AmcBind();
            }


        }
        protected void AmcBind()
        {
            DataTable dtAmc = new DataTable();
            dtAmc = commonLookupBo.GetProdAmc();
            if (dtAmc.Rows.Count > 0)
            {
                ddlAmc.DataSource = dtAmc;
                ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAmc.DataBind();
                ddlAmc.Items.Insert(0, new ListItem("Select", "0"));


            }
        }
        private void BindModeOfHolding()
        {
            DataTable dtModeOfHolding;
            dtModeOfHolding = XMLBo.GetModeOfHolding(path);
            ddlMoh.DataSource = dtModeOfHolding;
            ddlMoh.DataTextField = "ModeOfHolding";
            ddlMoh.DataValueField = "ModeOfHoldingCode";
            ddlMoh.DataBind();
            ddlMoh.Items.Insert(0, new ListItem("Select", "0"));
        }
        public void ddlAmc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CategoryBind();
            SchemeBind(int.Parse(ddlAmc.SelectedValue), null);
            BindFolioNumber(int.Parse(ddlAmc.SelectedValue));

        }

        public void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAmc.SelectedIndex != -1 && ddlCategory.SelectedIndex != -1)
            {
                int amcCode = int.Parse(ddlAmc.SelectedValue);
                string category = ddlCategory.SelectedValue.ToString();
                SchemeBind(amcCode, category);
            }

        }

        protected void ddlScheme_onSelectedChanged(object sender, EventArgs e)
        {
            if (ddlScheme.SelectedIndex != -1)
            {
                ResetControlDetails();
                GetControlDetails(int.Parse(ddlScheme.SelectedValue),ddlFolio.SelectedValue.ToString());
                SetControlDetails();
            }
        }

        protected void ResetControlDetails()
        {
            lblDividendType.Text = "";
            lblMintxt.Text = "";
            lblMulti.Text = "";
            lbltime.Text = "";
            lbldftext.Text = "";
            txtAmt.Text = "";
        }
        protected void GetControlDetails(int scheme,string folio)
        {
            DataSet ds = new DataSet();
            DataTable dt = ds.Tables[0];
            ds = onlineMforderBo.GetControlDetails(scheme, folio);
            if (dt.Rows.Count > -1)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["PSLV_LookupValue"].ToString()))
                    {
                        lblDividendType.Text = dr["PSLV_LookupValue"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["MinAmt"].ToString()))
                    {
                        lblMintxt.Text = dr["MinAmt"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["MultiAmt"].ToString()))
                    {
                        lblMulti.Text = dr["MultiAmt"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["CutOffTime"].ToString()))
                    {
                        lbltime.Text = dr["CutOffTime"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dr["divFrequency"].ToString()))
                    {
                        lbldftext.Text = dr["divFrequency"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["MinRedeemAmt"].ToString()))
                    {
                        txtMinAmt.Text = dr["MinRedeemAmt"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["MinRedeemUnit"].ToString()))
                    {
                        txtMinUnit.Text = dr["MinRedeemUnit"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["MultiRedeemAmt"].ToString()))
                    {
                        txtMultiAmt.Text = dr["MultiRedeemAmt"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["MultiRedeemUnit"].ToString()))
                    {
                        txtMultiUnit.Text = dr["MultiRedeemUnit"].ToString();
                    }
                }
            }
            if (ds.Tables[1].Rows.Count > -1)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    if (!string.IsNullOrEmpty(dr["CMFNP_NetHoldings"].ToString()))
                    {
                        lblCurrentUnitstxt.Text = dr["CMFNP_NetHoldings"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["CMFNP_CurrentValue"].ToString()))
                    {
                        lblCurrentValuetxt.Text = dr["CMFNP_CurrentValue"].ToString();
                    }

                }
            }


        }
        protected void SetControlDetails()
        {
            lbltime.Visible = true;
            lblDividendType.Visible = true;
            lblMulti.Visible = true;
            lblMintxt.Visible = true;
            lblDivType.Visible = true;

        }
        protected void CategoryBind()
        {
            DataSet dsCategory = new DataSet();
            dsCategory = commonLookupBo.GetAllCategoryList();

            if (dsCategory.Tables[0].Rows.Count > 0)
            {
                ddlCategory.DataSource = dsCategory.Tables[0];
                ddlCategory.DataValueField = dsCategory.Tables[0].Columns["Category_Code"].ToString();
                ddlCategory.DataTextField = dsCategory.Tables[0].Columns["Category_Name"].ToString();
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("All", "0"));
            }
        }

        protected void SchemeBind(int amccode, string category)
        {
            DataTable dtScheme = new DataTable();
            dtScheme = commonLookupBo.GetAmcSchemeList(amccode, category);
            if (dtScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtScheme;
                ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
            }
        }
        private void GetNetpositionValues(int folio,int scheme)
        { 
        
        }
        private void BindFolioNumber(int amcCode)
        {
            DataTable dtScheme = new DataTable();
            DataTable dtgetfolioNo;
            try
            {
                dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(Convert.ToInt32(ddlAmc.SelectedValue), customerVo.CustomerId);

                if (dtgetfolioNo.Rows.Count > 0)
                {
                    ddlFolio.DataSource = dtgetfolioNo;
                    ddlFolio.DataTextField = dtgetfolioNo.Columns["CMFA_FolioNum"].ToString();
                    ddlFolio.DataValueField = dtgetfolioNo.Columns["CMFA_AccountId"].ToString();
                    ddlFolio.DataBind();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }
        protected void OnClick_Submit(object sender, EventArgs e)
        {
            List<int> OrderIds = new List<int>();

            onlinemforderVo.SchemePlanCode = Int32.Parse(ddlScheme.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(txtAmt.Text.ToString()))
            {
                onlinemforderVo.Amount = double.Parse(txtAmt.Text.ToString());
            }
            else
                onlinemforderVo.Amount = 0.0;
            onlinemforderVo.FolioNumber = ddlFolio.SelectedValue;
            onlinemforderVo.DividendType = ddlDivType.SelectedValue;
            onlinemforderVo.TransactionType = "Sel";
            OrderIds = onlineMforderBo.CreateCustomerOnlineMFOrderDetails(onlinemforderVo, userVo.UserId, customerVo.CustomerId);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Your order added successfully.');", true);
            OrderId = int.Parse(OrderIds[0].ToString());
        }
        protected void imgAddNominee_Click(object sender, EventArgs e)
        {
            LoadNominees();
            radwindowForNominee.VisibleOnPageLoad = true;
        }
        private void LoadNominees()
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
                    Session["Nominee"] = dtCustomerAssociates;
                    //trJoint2Header.Visible = true;
                    //trJoint2HeaderGrid.Visible = true;
                }
                else
                {
                    //trJoint2Header.Visible = false;
                    //trJoint2HeaderGrid.Visible = true;
                    btnAddNominee.Visible = false;
                    DivForNominee.Visible = true;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerMFAccountAdd.ascx:LoadNominees()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        protected void btnAddNominee_Click(object sender, EventArgs e)
        {
            CheckBox chkbox = new CheckBox();
            hdnAssociationIdForNominee.Value = "";
            DataTable dtBindTableWithSelectedNominee = new DataTable();
            DataTable dtNominee = new DataTable();
            if (dtNominee != null)
                dtNominee = null;
            dtNominee = (DataTable)Session["Nominee"];
            string strNomineeAssnId = string.Empty;
            customerAccountsVo.AccountId = accountId;
            customerAccountAssociationVo.AccountId = accountId;
            customerAccountAssociationVo.CustomerId = customerVo.CustomerId;
            foreach (GridDataItem gvr in this.gvNominees.Items)
            {
                chkbox = (CheckBox)gvr.FindControl("chkId0"); // accessing the CheckBox control
                if (chkbox.Checked == true)
                {
                    hdnAssociationIdForNominee.Value = gvNominees.MasterTableView.DataKeyValues[gvr.ItemIndex]["AssociationId"].ToString();
                    strNomineeAssnId = strNomineeAssnId + hdnAssociationIdForNominee.Value + ",";
                }
            }
            if (!string.IsNullOrEmpty(strNomineeAssnId))
            {
                strNomineeAssnId = strNomineeAssnId.TrimEnd(',');
                string expression;
                expression = "AssociationId in" + "(" + strNomineeAssnId + ")";
                DataRow[] foundRows;
                foundRows = dtNominee.Select(expression);
                dtBindTableWithSelectedNominee.Rows.Clear();
                dtBindTableWithSelectedNominee.Columns.Add("MemberCustomerId");
                dtBindTableWithSelectedNominee.Columns.Add("AssociationId");
                dtBindTableWithSelectedNominee.Columns.Add("Name");
                dtBindTableWithSelectedNominee.Columns.Add("XR_Relationship");
                foreach (DataRow dr in foundRows)
                {
                    dr.BeginEdit();
                    dtBindTableWithSelectedNominee.Rows.Add(dr.ItemArray);
                    dtBindTableWithSelectedNominee.AcceptChanges();
                }

                gvNominee2.DataSource = dtBindTableWithSelectedNominee;
                gvNominee2.DataBind();
                gvNominee2.Visible = true;
            }
        }
        private void BindAssociates(CustomerAccountsVo AccountVo)
        {
            DataTable dtJoinHolder = new DataTable();
            DataTable dtJoinHolderGV = new DataTable();
            DataTable dtGuardian = new DataTable();
            DataTable dtNominees = new DataTable();
            DataTable dtNomineesGV = new DataTable();

            try
            {
                dsCustomerAssociates = customerTransactionBo.GetMFFolioAccountAssociates(AccountVo.AccountId, customerVo.CustomerId);
                dtJoinHolder = dsCustomerAssociates.Tables[2];
                dtNominees = dsCustomerAssociates.Tables[1];
                dtGuardian = dsCustomerAssociates.Tables[0];

                if (AccountVo.IsJointHolding == 1)
                {
                    //trAddJointHolder.Visible = true;
                    gvJoint2.Visible = true;
                    if (dtJoinHolder.Rows.Count > 0 && dtJoinHolder != null)
                    {
                        ViewState["JointHold"] = dtJoinHolder;
                        gvJoint2.DataSource = dtJoinHolder;
                        gvJoint2.DataBind();
                        gvJoint2.Visible = true;
                    }
                    else
                    {
                    }
                }

                if (dtNominees.Rows.Count > 0 && dtJoinHolder != null)
                {
                    ViewState["Nominees"] = dtNominees;
                    gvNominee2.DataSource = dtNominees;
                    gvNominee2.DataBind();
                    gvNominee2.Visible = true;
                }


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerMFAccountAdd.ascx:BindAssociates()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        protected void imgAddJointHolder_Click(object sender, EventArgs e)
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
                gvJointHoldersList.DataSource = dtCustomerAssociates;
                gvJointHoldersList.DataBind();
                gvJointHoldersList.Visible = true;

                Session["JointHolder"] = dtCustomerAssociates;
                //trJoint2Header.Visible = true;
                //trJoint2HeaderGrid.Visible = true;
            }
            else
            {
                //trJoint2Header.Visible = false;
                //trJoint2HeaderGrid.Visible = true;
                btnAddJointHolder.Visible = false;
                DivForJH.Visible = true;
            }


            radwindowForJointHolder.VisibleOnPageLoad = true;
        }
        protected void ddlRedeem_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRedeem.SelectedItem.Value == "Units")
            {
                trUnit.Visible = true;
                trAmt.Visible = false;
            }
            else
                trAmt.Visible = true;
            trUnit.Visible = false;
        }
        protected void btnAddJointHolder_Click(object sender, EventArgs e)
        {
            CheckBox chkbox = new CheckBox();
            hdnAssociationIdForJointHolder.Value = "";
            DataTable dtBindTableWithSelectedJointHolder = new DataTable();
            DataTable dtJointHolder = new DataTable();
            if (dtJointHolder != null)
                dtJointHolder = null;
            dtJointHolder = (DataTable)Session["JointHolder"];
            string strJointHolderAssnId = string.Empty;
            customerAccountsVo.AccountId = accountId;
            customerAccountAssociationVo.AccountId = accountId;
            customerAccountAssociationVo.CustomerId = customerVo.CustomerId;

            foreach (GridDataItem gvr in this.gvJointHoldersList.Items)
            {
                chkbox = (CheckBox)gvr.FindControl("chkId"); // accessing the CheckBox control
                if (chkbox.Checked == true)
                {
                    hdnAssociationIdForJointHolder.Value = gvJointHoldersList.MasterTableView.DataKeyValues[gvr.ItemIndex]["AssociationId"].ToString();
                    strJointHolderAssnId = strJointHolderAssnId + hdnAssociationIdForJointHolder.Value + ",";
                }
            }

            if (!string.IsNullOrEmpty(strJointHolderAssnId))
            {
                strJointHolderAssnId = strJointHolderAssnId.TrimEnd(',');
                string expression;
                expression = "AssociationId in" + "(" + strJointHolderAssnId + ")";
                DataRow[] foundRows;
                foundRows = dtJointHolder.Select(expression);
                dtBindTableWithSelectedJointHolder.Rows.Clear();
                dtBindTableWithSelectedJointHolder.Columns.Add("MemberCustomerId");
                dtBindTableWithSelectedJointHolder.Columns.Add("AssociationId");
                dtBindTableWithSelectedJointHolder.Columns.Add("Name");
                dtBindTableWithSelectedJointHolder.Columns.Add("XR_Relationship");
                foreach (DataRow dr in foundRows)
                {
                    dr.BeginEdit();
                    dtBindTableWithSelectedJointHolder.Rows.Add(dr.ItemArray);
                    dtBindTableWithSelectedJointHolder.AcceptChanges();
                }

                gvJoint2.DataSource = dtBindTableWithSelectedJointHolder;
                gvJoint2.DataBind();
                gvJoint2.Visible = true;
            }
        }


    }
}