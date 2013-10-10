using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCommon;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using VoUser;
using BoCustomerPortfolio;
using System.Configuration;
using BoProductMaster;


namespace WealthERP.OnlineOrderManagement
{
    public partial class MFOrderSIPTransType : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        int CustomerId;
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();

        DataSet dsCustomerAssociates;
        DataTable dtCustomerAssociatesRaw;
        DataTable dtCustomerAssociates;
        DataRow drCustomerAssociates;
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        string path;
        DataTable dtgetfolioNo;
        DataTable dtModeOfHolding;
        DataTable dtGetAllSIPDataForOrder;
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        ProductMFBo productMfBo = new ProductMFBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            rmVo = (RMVo)Session["rmVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];

            AmcBind();
            LoadNominees();
            BindCategory();


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

            }
        }
        public void ddlAmc_OnSelectedIndexChanged(object sender, EventArgs e)
        {            
            BindFolioNumber(Convert.ToInt32(ddlAmc.SelectedValue));
            SchemeBind(Convert.ToInt32(ddlAmc.SelectedValue), "ALL");
        }

        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SchemeBind(Convert.ToInt32(ddlAmc.SelectedValue), ddlCategory.SelectedValue);
        }

        private void BindCategory()
        {
            try
            {
                DataSet dsProductAssetCategory;
                dsProductAssetCategory = productMfBo.GetProductAssetCategory();
                DataTable dtCategory = dsProductAssetCategory.Tables[0];
                if (dtCategory != null)
                {
                    ddlCategory.DataSource = dtCategory;
                    ddlCategory.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                    ddlCategory.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                    ddlCategory.DataBind();
                }
                ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFManualSingleTran.ascx:BindBranchDropDown()");

                object[] objects = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
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

        protected void BindSIPUI()
        {

        }

        protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSIPUIONSchemeSelection();
        }

        protected void hidFolioNumber_ValueChanged(object sender, EventArgs e)
        {


        }

        protected void BindSIPUIONSchemeSelection()
        {
            dtGetAllSIPDataForOrder = commonLookupBo.GetAllSIPDataForOrder(Convert.ToInt32(ddlScheme.SelectedValue));
            BindFrequency();
            BindAllControlsWithSIPData();
        }

        protected void BindFrequency()
        {
            if (dtGetAllSIPDataForOrder != null)
            {
                ddlFrequency.DataSource = dtGetAllSIPDataForOrder;
                ddlFrequency.DataTextField = dtGetAllSIPDataForOrder.Columns["XF_Frequency"].ToString();
                ddlFrequency.DataValueField = dtGetAllSIPDataForOrder.Columns["XF_FrequencyCode"].ToString();
                ddlFrequency.DataBind();

            }
        }

        private void BindModeOfHolding()
        {
            dtModeOfHolding = XMLBo.GetModeOfHolding(path);
            ddlModeOfHolding.DataSource = dtModeOfHolding;
            ddlModeOfHolding.DataTextField = "ModeOfHolding";
            ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
            ddlModeOfHolding.DataBind();
            ddlModeOfHolding.Items.Insert(0, new ListItem("Select", "0"));
        }


        public void BindAllControlsWithSIPData()
        {
            if (dtGetAllSIPDataForOrder.Rows.Count>0)
            {              
                txtMinAmtReqd.Text = dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MinAmount"].ToString();
                txtMultiplesThereAfter.Text = dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MultipleAmount"].ToString();
                txtCutOffTime.Text = dtGetAllSIPDataForOrder.Rows[0]["PASPD_CutOffTime"].ToString();
                BindFrequency();
                ddlFrequency.SelectedValue = dtGetAllSIPDataForOrder.Rows[0]["XF_FrequencyCode"].ToString();
                ShowSipDates(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_StatingDates"].ToString());
            }

        }


        protected void ShowSipDates(string DelimitedDateVals)
        {
            string[] dates = DelimitedDateVals.Split(';');

            if (tdSipDates == null) return;
            int i = 0;
            foreach (string date in dates)
            {
                CheckBox chk = new CheckBox();
                chk.ID = "chk_Sip_" + i;
                chk.Text = date;
                chk.Visible = true;
                tdSipDates.Controls.Add(chk);
                i++;
            }
        }

        private void BindFolioNumber(int amcCode)
        {
            DataTable dtScheme = new DataTable();
            DataTable dtgetfolioNo;
            try
            {
                dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(Convert.ToInt32(ddlAmc.SelectedValue));

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

        protected void LoadNominees()
        {
            try
            {
                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];

                dtCustomerAssociatesRaw.Columns.Add("MemberCustomerId");
                //dtCustomerAssociates.Columns.Add("AssociationId");
                dtCustomerAssociatesRaw.Columns.Add("Name");
                //dtCustomerAssociates.Columns.Add("Relationship");


                foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
                {
                    drCustomerAssociates = dtCustomerAssociates.NewRow();
                    drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                    //drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                    drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                    //drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                    dtCustomerAssociates.Rows.Add(drCustomerAssociates);
                }

                if (dtCustomerAssociates != null)
                {
                    ddlNominee.DataSource = dtCustomerAssociates;
                    ddlNominee.DataValueField = dtCustomerAssociates.Columns["PASP_SchemePlanCode"].ToString();
                    ddlNominee.DataTextField = dtCustomerAssociates.Columns["PASP_SchemePlanName"].ToString();
                    ddlNominee.DataBind();

                    ddlHolder.DataSource = dtCustomerAssociates;
                    ddlHolder.DataValueField = dtCustomerAssociates.Columns["PASP_SchemePlanCode"].ToString();
                    ddlHolder.DataTextField = dtCustomerAssociates.Columns["PASP_SchemePlanName"].ToString();
                    ddlHolder.DataBind();
                }
                //else
                //{
                //    trNominees.Visible = false;
                //}

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioGeneralInsuranceAccountAdd.ascx:LoadNominees()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }


    }
}